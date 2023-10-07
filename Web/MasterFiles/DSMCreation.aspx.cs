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

public partial class MasterFiles_DSMCreation : System.Web.UI.Page
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
    string[] statecd;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "DSMList.aspx";
        Subdivision_Code = Request.QueryString["Subdivision_Code"];

        if (!Page.IsPostBack)
        {
			 Lbl_Area.Enabled = false;
            ddlState.Enabled = false;
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            GetTownName();
            FillFO();
            FillState(divcode);
            txtSubDivision_Sname.Focus();
            if (Subdivision_Code != "" && Subdivision_Code != null)
            {
                DSM sd = new DSM();
                dsSubDiv = sd.getZone(divcode, Subdivision_Code);
                if (dsSubDiv.Tables[0].Rows.Count > 0)
                {
                    txtPassword.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(4).ToString().Trim();
                    string strPassword;
                    strPassword = txtPassword.Text;
                    txtPassword.Attributes.Add("value", strPassword);
                    txtSubDivision_Sname.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim();
                    txtSubDivision_Name.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim();
                    string st = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    txtUserName.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim();
                    txtPassword.Text= dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(4).ToString().Trim();
                    string st1 = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(5).ToString().Trim();
                    string st2 = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(6).ToString().Trim();

                    salesforcelist.DataSource = dsDivision;
                    salesforcelist.DataTextField = "Sf_Name";
                    salesforcelist.DataValueField = "Sf_Code";
                    salesforcelist.DataBind();
                    int iCount2 = 0, iIndex2;
                    foreach (ListItem item2 in salesforcelist.Items)
                    {
                        if (st2 == item2.ToString())
                        {
                            iIndex2 = iCount2;
                            salesforcelist.SelectedIndex = iIndex2;
                            break;
                        }
                        iCount2++;
                    }                    
					ddlState.DataSource = dsState;
                    ddlState.DataTextField = "Stockist_Name";
                    ddlState.DataValueField = "Distributor_Code";
                    ddlState.DataBind();
                    
                    int iCount = 0, iIndex;
                    foreach (ListItem item in ddlState.Items)
                    {
                        if (st == item.ToString())
                        {
                            iIndex = iCount;
                            ddlState.SelectedIndex = iIndex;
                            break;
                        }
                        iCount++;
                    }
                    ddlTown_Name.DataSource = dsStockist;
                    ddlTown_Name.DataTextField = "Town_Name";
                    ddlTown_Name.DataValueField = "Town_Code";
                    ddlTown_Name.DataBind();
                    int iCount1 = 0, iIndex1;
                    foreach (ListItem item1 in ddlTown_Name.Items)
                    {
                        if (st1 == item1.ToString())
                        {
                            iIndex1 = iCount1;
                            ddlTown_Name.SelectedIndex = iIndex1;
                            break;
                        }
                        iCount1++;
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
    private void GetTownName()
    {
        DSM sk = new DSM();
        dsStockist = sk.getPool_Name(divcode);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlTown_Name.DataTextField = "Town_Name";
            ddlTown_Name.DataValueField = "Town_Code";
            ddlTown_Name.DataSource = dsStockist;
            ddlTown_Name.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        //if (txtSubDivision_Sname.Text.Length <= 5 && txtSubDivision_Name.Text.Length <= 50)
        //{
            subdiv_sname = txtSubDivision_Sname.Text.Trim();
            subdiv_name = txtSubDivision_Name.Text.Trim();
            Area_name = ddlState.SelectedItem.ToString();
            Username = txtUserName.Text;
            Password = txtPassword.Text;
            Area_cd = ddlState.SelectedValue;
            Town_code = ddlTown_Name.SelectedValue;
            Town_name = ddlTown_Name.SelectedItem.ToString();
            DSM dv_code = new DSM();
            dsDivision = dv_code.area_code(Area_name);
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                //int i = 0;

                Area_cd = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            if (Subdivision_Code == null)
            {

                // Add New Sub Division
                DSM dv = new DSM();
                int iReturn = dv.ZoneRecordAdd(divcode, subdiv_sname, subdiv_name, Area_name, Area_cd,Username,Password,Town_name,Town_code,salesforcelist.SelectedValue,salesforcelist.SelectedItem.ToString());
                if (iReturn > 0)
                {

                    // menu1.Status = "Sub Division created Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    Resetall();
                }
                else if (iReturn == -2)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DSM Name Already Exist');</script>");
                    txtSubDivision_Name.Focus();
                }
                else if (iReturn == -3)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DSM Code Already Exist');</script>");
                    txtSubDivision_Sname.Focus();
                }
            }
            else
            {
                // Update Sub Division
                DSM dv = new DSM();
                //int subdivcode = Convert.ToInt16(Subdivision_Code);
          int iReturn = dv.ZoneRecordUpdate(subdiv_sname, subdiv_name, Area_name, divcode, Area_cd, Username, Password, Town_code, Town_name, salesforcelist.SelectedValue, salesforcelist.SelectedItem.ToString(),Subdivision_Code);
                if (iReturn > 0)
                {
                    // menu1.Status = "Sub Division Updated Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='DSMList.aspx';</script>");
                }
                else if (iReturn == -2)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DSM Name Already Exist');</script>");
                    txtSubDivision_Name.Focus();
                }
                else if (iReturn == -3)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DSM Code Already Exist');</script>");
                    txtSubDivision_Sname.Focus();
                }
            }
       //}
     //  else
      //  {
     //      ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('please Enter minimum length Value');</script>");
       // }
    }
    private void Resetall()
    {
        txtSubDivision_Sname.Text = "";
        txtSubDivision_Name.Text = "";
        txtUserName.Text = "";
        txtPassword.Text = "";
        ddlState.SelectedIndex = 0;
        salesforcelist.SelectedIndex = 0;
        ddlTown_Name.SelectedIndex = 0;
    }
    //AreaList Create by Giri 14-06-2016
     private void FillState(string div_code)
    {
        DSM dv = new DSM();
        dsDSM = dv.getStatePerDivision(div_code);
        if (dsDSM.Tables[0].Rows.Count > 0)
        {
            //int i = 0;
            state_cd = string.Empty;
            state_cd = dsDSM.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            DSM st = new DSM();
            dsState = st.getStateProd(div_code);
            ddlState.DataTextField = "Stockist_Name";
            ddlState.DataValueField = "Distributor_Code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
        }
    }
    public void Filldist()
    {
        SalesForce sf = new SalesForce();
        dsDivision = sf.GetDistNamewise(divcode, salesforcelist.SelectedValue);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {

            ddlState.DataTextField = "Stockist_Name";
            ddlState.DataValueField = "Stockist_Code";
            ddlState.DataSource = dsDivision;
            ddlState.DataBind();
           
        }
    }
    public void FillFO()
    {
        SalesForce sf = new SalesForce();
        dsDivision = sf.getSalesForcelist(divcode);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {

            salesforcelist.DataTextField = "Sf_Name";
            salesforcelist.DataValueField = "Sf_Code";
            salesforcelist.DataSource = dsDivision;
            salesforcelist.DataBind();
            salesforcelist.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    protected void salesforcelist_SelectedIndexChanged(object sender, EventArgs e)
    {
        Lbl_Area.Enabled = true;
        ddlState.Enabled = true;
        Filldist();
    }
    
}