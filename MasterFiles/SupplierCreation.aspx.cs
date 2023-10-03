using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_SupplierCreation : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDiv = null;
    string Subdivision_Code = string.Empty;
    string divcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    string Area_name = string.Empty;
    string Username = string.Empty;
    string Password = string.Empty;
    string HqterrCode = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsDSM = null;
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsStockist = null;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string Area_cd = string.Empty;
    string Town_name = string.Empty;
    string Town_code = string.Empty;
    string Field_Name = string.Empty;
    string Field_Code = string.Empty;
    string[] statecd;
    #endregion
	string sf_type = string.Empty;
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "SupplierList.aspx";
        Subdivision_Code = Request.QueryString["Subdivision_Code"];

        if (!Page.IsPostBack)
        {
            //FieldForceLoad();
            GetTerritoryName();
            loadff();
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            txtSup_Sname.Focus();
            if (Subdivision_Code != "" && Subdivision_Code != null)
            {
                DSM sd = new DSM();
                dsSubDiv = sd.getSupplier(divcode, Subdivision_Code);
                if (dsSubDiv.Tables[0].Rows.Count > 0)
                {

                    txtSup_Sname.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim();
                    txtSupCon_Name.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim();
                    txtMob_No.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim();
                    txt_ERP.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim();
                    ddlTerritoryName.SelectedValue = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(6).ToString().Trim();                 
                    TextBox1.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(5).ToString().Trim();
                    string[] str_FO = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(4).ToString().Trim().Split(',');
                    txtusr.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(7).ToString().Trim();
                    txtpass.Attributes.Add("value", dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(8).ToString().Trim());
                    txtpass.TextMode = TextBoxMode.Password;
                    txtaddr.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(9).ToString().Trim();
                    loadff();
                    for (int i = 0; i < DDL_FO.Items.Count; i++)
                    {
                        for (int k = 0; k < str_FO.Length; k++)
                        {
                            if (DDL_FO.Items[i].Value.ToString() == str_FO[k])
                            {
                                DDL_FO.Items[i].Selected = true;
                            }
                        }
                    }


                }
            }

        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    }

    private void GetTerritoryName()
    {
        SalesForce SFD = new SalesForce();
        dsStockist = SFD.getAllSF_States(divcode);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlTerritoryName.DataTextField = "sname";
            ddlTerritoryName.DataValueField = "scode";
            ddlTerritoryName.DataSource = dsStockist;
            ddlTerritoryName.DataBind();
        }
    }

    private void FieldForceLoad()
    {
        SalesForce sk = new SalesForce();
        DataSet dsStockist = sk.Get_HyrSFList_All(divcode, "0", "admin");
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            DDL_FO.DataTextField = "Sf_Name";
            DDL_FO.DataValueField = "Sf_Code";
            DDL_FO.DataSource = dsStockist;
            DDL_FO.DataBind();
            DDL_FO.Enabled = true;
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        if (txtSup_Sname.Text.Length <= 100 && txtSupCon_Name.Text.Length <= 100)
        {
            subdiv_sname = txtSup_Sname.Text.Trim();
            subdiv_name = txtSupCon_Name.Text.Trim();
            Username = txtMob_No.Text.Trim();
            Password = txt_ERP.Text.Trim();
            HqterrCode = ddlTerritoryName.SelectedValue.ToString();
            string usrName = (txtusr.Text.Trim() != "") ? txtusr.Text.Trim() : "";
            string pwd = (txtpass.Text.Trim()!="")?txtpass.Text.Trim() : "";
            string addr = txtaddr.Text.Trim();
            for (int i = 0; i < DDL_FO.Items.Count; i++)
            {
                if (DDL_FO.Items[i].Selected)
                {
                    Field_Code += DDL_FO.Items[i].Value + ",";
                    Field_Name += DDL_FO.Items[i].Text + ",";
                }
            }


            DSM dv_code = new DSM();
            if (subdiv_sname != "" && subdiv_name != "" && Password != "" && addr != "" && usrName != "")
            {
                if (Subdivision_Code == null)
                {

                    // Add New Sub Division
                    DSM dv = new DSM();
                    int iReturn = dv.supplierRecordAdd(divcode, subdiv_sname, subdiv_name, Username, Password, Field_Code, Field_Name, HqterrCode, usrName, pwd, addr);
                    if (iReturn > 0)
                    {

                        // menu1.Status = "Sub Division created Successfully ";
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location.href='SupplierList.aspx';</script>");
                        Resetall();
                    }
                    else if (iReturn == -2)
                    {

                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Supplier Name Already Exist');</script>");
                        txtSup_Sname.Focus();
                    }

                }
                else
                {
                    // Update Sub Division
                    DSM dv = new DSM();
                    //int subdivcode = Convert.ToInt16(Subdivision_Code);
                    int iReturn = dv.SupplierRecordUpdate(subdiv_sname, subdiv_name, divcode, Username, Password, Subdivision_Code, Field_Code, Field_Name, HqterrCode, usrName, pwd, addr);
                    if (iReturn > 0)
                    {
                        // menu1.Status = "Sub Division Updated Successfully ";
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location.href='SupplierList.aspx';</script>");
                    }
                    else if (iReturn == -2)
                    {

                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Supplier Name Already Exist');</script>");
                        txtSup_Sname.Focus();
                    }

                }
            }
            else
            {
                if (subdiv_sname == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Supplier Name');</script>");
                    txtSup_Sname.Focus();
                }
                else if (subdiv_name == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Contact person...');</script>");
                    txtSupCon_Name.Focus();
                }
                else if (Password == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter ERP code..');</script>");
                    txt_ERP.Focus();
                }
                else if (addr == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter super stockist address..');</script>");
                    txtaddr.Focus();
                }
                else if (usrName == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Username..');</script>");
                    txtusr.Focus();
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('please Enter minimum length Value');</script>");
        }
    }
    private void Resetall()
    {
        txtSup_Sname.Text = "";
        txtSupCon_Name.Text = "";
        txtMob_No.Text = "";
        txt_ERP.Text = "";
        txtusr.Text = "";
        txtpass.Text = "";
    }
    protected void ddlTerritoryName_SelectedIndexChanged(object sender, EventArgs e)
    {

        loadff();
       


    }

    private void loadff()
    {
        Stockist sk = new Stockist();

       //  var terr = ddlTerritoryName.SelectedValue;
       // if (ddlTerritoryName.SelectedValue == "0")
       // {
       //     DDL_FO.SelectedIndex = 0;
            // lstProducts.SelectedIndex=0;
       // }
       // else
       // {

            dsStockist = sk.FOName(divcode);
            if (dsStockist.Tables[0].Rows.Count > 0)
            {
                DDL_FO.DataTextField = "Sf_Name";
                DDL_FO.DataValueField = "Sf_Code";
                DDL_FO.DataSource = dsStockist;
                DDL_FO.DataBind();
                DDL_FO.Enabled = true;



                // lstProducts.DataTextField = "Sf_Name";
                // lstProducts.DataValueField = "Sf_Code";
                // lstProducts.DataSource = dsStockist;
                //  lstProducts.DataBind();
                //  lstProducts.Enabled = true;
            }
            else
            {
                DDL_FO.Enabled = false;
                //  lstProducts.Enabled = false;
            }

       // }

    }





}