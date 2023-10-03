using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_HO_ID_Creation : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsSales = null;
    DataSet dsHODivision = null;

    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iIndex = -1;
    int iLength = -1;
    string divcode = string.Empty;
    string HO_ID = string.Empty;
    string HO_div_code = string.Empty;
    string division_code = string.Empty;
    string sf_type = string.Empty;
    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
       // divcode = Convert.ToString(Session["div_code"]);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }        
        HO_ID = Request.QueryString["HO_ID"];

        if (!Page.IsPostBack)
        {
            txtName.Focus();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if ((HO_ID != "") && (HO_ID != null))
            {
                menu1.Title = this.Page.Title;
                Session["backurl"] = "HO_ID_View.aspx";

                SalesForce sale = new SalesForce();
                dsSales = sale.get_Ho_Id(HO_ID);

                if (dsSales.Tables[0].Rows.Count > 0)
                {
                    txtName.Text = dsSales.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtUserName.Text = dsSales.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtUserName.Enabled = false;
                    txtPassword.Text = dsSales.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    division_code = dsSales.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                }
                if (sf_type == "")
                {
                    FillCheckBoxList(HO_ID);
                }
                else if (sf_type == "3")
                {
                    FillHOEditDivision(HO_ID);
                    menu1.Title = "Change Password";
                }
            }
            else
            {      
               FillDiv();
            }
         }
    }

    private void FillHOEditDivision(string HO_ID)
    {
        Division dv = new Division();

        dsHODivision = dv.getHODivision(HO_ID);
        if (dsHODivision.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drHOdiv in dsHODivision.Tables[0].Rows)
            {
                if ((drHOdiv["division_code"] != null) && (drHOdiv["division_code"].ToString().Trim().Length > 0))
                    HO_div_code = HO_div_code + drHOdiv["division_code"].ToString();
            }

            iLength = HO_div_code.Trim().Length;
            if (iLength > 0)
            {
                HO_div_code = HO_div_code.Substring(0, iLength - 1);
            }
            else
                HO_div_code = "-1";
        }
        else
            HO_div_code = "-1";
        
        dsDivision = dv.getDivEdit(HO_div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            chkDivision.Visible = true;
            chkDivision.DataSource = dsDivision;
            chkDivision.DataBind();
        }
        else
        {
            chkDivision.Visible = false;
            lblDivision.Visible = false;
            chkDivision.DataSource = dsDivision;
            chkDivision.DataBind();
        }

        string[] div;

        if (division_code != "")
        {
            iIndex = -1;
            div = division_code.Split(',');
            foreach (string st in div)
            {
                for (iIndex = 0; iIndex < chkDivision.Items.Count; iIndex++)
                {
                    if (st == chkDivision.Items[iIndex].Value)
                    {
                        chkDivision.Items[iIndex].Selected = true;
                        chkDivision.Items[iIndex].Attributes.Add("style", "Color: #8A2BE2;font-weight:Bold");
                        if (sf_type == "3")
                        {
                            chkDivision.Items[iIndex].Enabled = false;
                        }
                    }
                }
            }
        }
    }
    private void FillCheckBoxList(string HO_ID)
    {
        Division dv = new Division();

        dsHODivision = dv.getHODivisionEdit(HO_ID);
        if (dsHODivision.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drHOdiv in dsHODivision.Tables[0].Rows)
            {
                if ((drHOdiv["division_code"] != null) && (drHOdiv["division_code"].ToString().Trim().Length > 0))
                    HO_div_code = HO_div_code + drHOdiv["division_code"].ToString();
            }

            iLength = HO_div_code.Trim().Length;
            if (iLength > 0)
            {
                HO_div_code = HO_div_code.Substring(0, iLength - 1);
            }
            else
                HO_div_code = "-1";
        }
        else
            HO_div_code = "-1";
        division_code = division_code.Substring(0, division_code.Length - 1);
        dsDivision = dv.getDiv(HO_div_code, division_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            chkDivision.Visible = true;
            chkDivision.DataSource = dsDivision;
            chkDivision.DataBind();
        }
        else
        {
            chkDivision.Visible = false;
            lblDivision.Visible = false;
            chkDivision.DataSource = dsDivision;
            chkDivision.DataBind();
        }
       
        string[] div;
   
        if (division_code != "")
        {
            iIndex = -1;
            div = division_code.Split(',');
            foreach (string st in div)
            {
                for (iIndex = 0; iIndex < chkDivision.Items.Count; iIndex++)
                {
                    if (st == chkDivision.Items[iIndex].Value)
                    {
                        chkDivision.Items[iIndex].Selected = true;
                        chkDivision.Items[iIndex].Attributes.Add("style", "Color: #8A2BE2;font-weight:Bold");
                        if (sf_type == "3")
                        {
                            chkDivision.Items[iIndex].Enabled = false ;
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

    private void FillDiv()
    {
        Division dv = new Division();

        dsHODivision = dv.getHODivision();
        if (dsHODivision.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drHOdiv in dsHODivision.Tables[0].Rows)
            {
                if ((drHOdiv["division_code"] != null) && (drHOdiv["division_code"].ToString().Trim().Length > 0))
                    HO_div_code = HO_div_code + drHOdiv["division_code"].ToString();
            }

            iLength = HO_div_code.Trim().Length;
            HO_div_code = HO_div_code.Substring(0, iLength - 1);
        }
        else
            HO_div_code = "-1";

        dsDivision = dv.getDiv(HO_div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            chkDivision.Visible = true;
            chkDivision.DataSource = dsDivision;
            chkDivision.DataBind();
        }
        else
        {
            chkDivision.Visible = false;
            lblDivision.Visible = false;
            chkDivision.DataSource = dsDivision;
            chkDivision.DataBind();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Create Division for the HO User');window.location='DivisionCreation.aspx'</script>");
        }
    }
    private void FillDivision()
    {
        Division dv = new Division();

        dsHODivision = dv.getHODivision(HO_ID);
        if (dsHODivision.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drHOdiv in dsHODivision.Tables[0].Rows)
            {
                if ((drHOdiv["division_code"] != null) && (drHOdiv["division_code"].ToString().Trim().Length > 0))
                    HO_div_code = HO_div_code + drHOdiv["division_code"].ToString();
            }

            iLength = HO_div_code.Trim().Length;
            HO_div_code = HO_div_code.Substring(0, iLength - 1);
        }
        else
            HO_div_code = "-1";

        dsDivision = dv.getDiv(HO_div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            chkDivision.Visible = true;
            chkDivision.DataSource = dsDivision;
            chkDivision.DataBind();
            
        }
        else
        {
            chkDivision.Visible = false;
            lblDivision.Visible = false;
            chkDivision.DataSource = dsDivision;
            chkDivision.DataBind();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Create Division for the HO User');window.location='DivisionCreation.aspx'</script>");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string div_code = string.Empty;
        int i;

        for (i = 0; i < chkDivision.Items.Count; i++)
        {
            if (chkDivision.Items[i].Selected)
                div_code = div_code + chkDivision.Items[i].Value.ToString() + ",";
        }


        if (HO_ID == null)
        {
            SalesForce sf = new SalesForce();
            int iReturn = sf.HO_ID_RecordAdd(txtUserName.Text.Trim(), txtPassword.Text.Trim(), txtName.Text.Trim(), "", div_code, "", "", "");
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('HO ID created successfully');</script>");
                Reset_Controls();
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('User Name Already Exist');</script>");
            }
        }

        else
        {
            SalesForce sf = new SalesForce();
            int HO_Id = Convert.ToInt16(HO_ID);
            int iReturn = sf.Update_HO_Id(HO_Id, txtName.Text, txtUserName.Text, txtPassword.Text, "", div_code,"");
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='HO_ID_View.aspx';</script>");
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('User Name Already Exist');</script>");
            }
        }
    }

    private void Reset_Controls()
    {
        txtName.Text = "";
        txtPassword.Text = "";
        txtUserName.Text = "";
      
        for (int i = 0; i < chkDivision.Items.Count; i++)
        {
            chkDivision.Items[i].Selected = false;            
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset_Controls();
    }
}