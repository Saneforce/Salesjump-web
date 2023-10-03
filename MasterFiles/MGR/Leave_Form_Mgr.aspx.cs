using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MGR_Leave_Form_Mgr : System.Web.UI.Page
{
    DataSet dsListedDR = null;
    DataSet dsTerritory = null;
    DataSet dsAdminSetup = null;
    string sf_code = string.Empty;
    string sfcode = string.Empty;
    string div_code = string.Empty;
    int request_type = -1;
    string Leave_code = string.Empty;
    string Leave_Id = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
           div_code = Session["div_code"].ToString();
           sfcode = Session["sf_code"].ToString();
           if (!Page.IsPostBack)
           {
               menu1.Visible = true;
               menu1.Title = this.Page.Title;
               menu1.FindControl("btnBack").Visible = false;
               GetHQ();
               AdminSetup adm = new AdminSetup();
               dsAdminSetup = adm.FillLeave_Type(div_code);
               ddltype.DataTextField = "Leave_SName";
               ddltype.DataValueField = "Leave_code";
               ddltype.DataSource = dsAdminSetup;
               ddltype.DataBind();
           }
           if (Request.QueryString["LeaveFrom"] != null)
           {
              // menu1.Visible = false;
             //  pnlleaveback.Visible = true;
             //  pnltit.Visible = true;
               string Leavedate = Request.QueryString["LeaveFrom"];
               txtLeave.Text = Leavedate.Substring(3, 2) + "/" + Leavedate.Substring(0, 2) + "/" + Leavedate.Substring(6, 4);
               Leavedate = "";
               txtLeave.Enabled = false;
               imgPopup.Enabled = false;
           }
         
    }
    private void GetHQ()
    {
     
        Territory terr = new Territory();
        dsTerritory = terr.getSfname_Desig(sfcode, div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            lblemp.Text =
                 "<span style='font-weight: bold;color:BlueViolet; font-size:12px;font-names:Calibri'>  " + dsTerritory.Tables[0].Rows[0]["Sf_Name"].ToString() + "</span>";
            //Session["Sf_Name"] = dsTerritory.Tables[0].Rows[0]["Sf_Name"].ToString();
            lbldesig.Text =
               "<span style='font-weight: bold;color:BlueViolet; font-size:12px; font-names:Calibri'>  " + dsTerritory.Tables[0].Rows[0]["Designation_Name"].ToString() + "</span>";
            lblSfhq.Text =
                "<span style='font-weight: bold;color:BlueViolet; font-size:12px; font-names:Calibri '>  " + dsTerritory.Tables[0].Rows[0]["Sf_HQ"].ToString() + "</span>";
            lblempcode.Text =
                   "<span style='font-weight: bold;color:BlueViolet; font-size:12px; font-names:Calibri'>  " + dsTerritory.Tables[0].Rows[0]["sf_emp_id"].ToString() + "</span>";

            lbldivi.Text =
                   "<span style='font-weight: bold;color:BlueViolet; font-size:12px; font-names:Calibri'>  " + dsTerritory.Tables[0].Rows[0]["Division_Name"].ToString() + "</span>";


        }
    }
    protected void txtLeaveto_TextChanged(object sender, EventArgs e)
    {
        if (txtLeave.Text != "" && txtLeaveto.Text != "")
        {

            DateTime dold = Convert.ToDateTime(txtLeave.Text);
            DateTime dnew = Convert.ToDateTime(txtLeaveto.Text);
            TimeSpan daydif = (dnew - dold);
            double dayd = (daydif.TotalDays) + 1;
            if (dold > dnew)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Date must be greater than From date');</script>");
                txtLeaveto.Text = "";
                txtLeaveto.Focus();
                lblDaysCount.Text = "";
            }
            lblDaysCount.Text = dayd.ToString();
            lblDaysCount.Style.Add("color", "Red");
            lblDaysCount.Style.Add("Font-Size", "14px");
        }
        DCR dcr = new DCR();
        DataSet ds = new DataSet();
        ds = dcr.getLeave_Mr(sfcode, Convert.ToDateTime(txtLeaveto.Text));
        if (ds.Tables[0].Rows.Count > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Leave already applied for the selected Date');</script>");
            txtLeaveto.Text = "";
            txtLeaveto.Focus();
        }
    }

    protected void txtLeave_TextChanged(object sender, EventArgs e)
    {
        if (txtLeave.Text != "" && txtLeaveto.Text != "")
        {
            DateTime dold = Convert.ToDateTime(txtLeave.Text);
            DateTime dnew = Convert.ToDateTime(txtLeaveto.Text);
            TimeSpan daydif = (dnew - dold);
            double dayd = (daydif.TotalDays) + 1;
            if (dold > dnew)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Date must be greater than From date');</script>");
                txtLeaveto.Text = "";
                txtLeaveto.Focus();
                lblDaysCount.Text = "";
            }
            lblDaysCount.Text = dayd.ToString();
            lblDaysCount.Style.Add("color", "Red");
            lblDaysCount.Style.Add("Font-Size", "14px");

        }
        DCR dcr = new DCR();
        DataSet ds = new DataSet();
        ds = dcr.getLeave_Mr(sfcode, Convert.ToDateTime(txtLeave.Text));
        if (ds.Tables[0].Rows.Count > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Leave already applied for the selected Date');</script>");
            txtLeave.Text = "";
            txtLeave.Focus();
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
       
        AdminSetup adm = new AdminSetup();
        iReturn = adm.Insert_Leave(ddltype.SelectedValue, Convert.ToDateTime(txtLeave.Text), Convert.ToDateTime(txtLeaveto.Text), txtreason.Text, txtAddr.Text, lblDaysCount.Text, chkmanager.SelectedValue, lblValidreason.Text, sfcode, div_code, chkho.SelectedValue);
        if (iReturn > 0)
        {
            if (Request.QueryString["LeaveFrom"] != null)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location='../../DCR/DCR_Entry.aspx';</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
            }
        }
        txtAddr.Text = "";
        txtLeaveto.Text = "";
        txtLeave.Text = "";
        txtreason.Text = "";
        ddltype.SelectedIndex = -1;
        lblDaysCount.Text = "";
    }

}