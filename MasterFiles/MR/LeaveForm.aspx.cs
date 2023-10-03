using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MR_LeaveForm : System.Web.UI.Page
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
    string div_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        //  sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
      
        if (!Page.IsPostBack)
        {
            // menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            AdminSetup adm = new AdminSetup();
            dsAdminSetup = adm.FillLeave_Type(div_code);
            ddltype.DataTextField = "Leave_SName";
            ddltype.DataValueField = "Leave_code";
            ddltype.DataSource = dsAdminSetup;
            ddltype.DataBind();
            
            if (Session["sf_type"].ToString() == "1")
            {
                menu1.Visible = true;             
                menu1.Title = this.Page.Title;
                menu1.FindControl("btnBack").Visible = false;
                GetHQ();
                pnlmr.Visible = true;
                btnBack.Visible = false;
               
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                menu1.Visible = false;

                GetHQ();
                pnlmgr.Visible = true;
                btnBack.Visible = true;
                pnlHead.Visible = true;
               
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null)
            {
                menu1.Visible = false;

                GetHQ();
                pnlmgr.Visible = true;
                btnBack.Visible = true;
                pnlHead.Visible = true;
            }
            else if (Session["sf_type"].ToString() == "3")
            {
                menu1.Visible = false;

                GetHQ();
                pnlmgr.Visible = true;
                btnBack.Visible = true;
                pnlHead.Visible = true;
               
            }
            if (Request.QueryString["LeaveFrom"] != null)
            {
                menu1.Visible = false;
                pnlleaveback.Visible = true;
                pnltit.Visible = true;
                string Leavedate = Request.QueryString["LeaveFrom"];
                txtLeave.Text = Leavedate.Substring(3, 2) + "/" + Leavedate.Substring(0, 2) + "/" + Leavedate.Substring(6, 4);
                Leavedate = "";
                txtLeave.Enabled = false;
                imgPopup.Enabled = false;
            }
        
            if (Request.QueryString["sfcode"] != null)
            {
                sfcode = Request.QueryString["sfcode"].ToString();
            }
            if (Request.QueryString["Leave_Id"] != null)
            {
                Leave_Id = Request.QueryString["Leave_Id"].ToString();
            }
            if (sfcode != null)
            {
                AdminSetup adm1 = new AdminSetup();
                dsAdminSetup = adm1.getLeave(sfcode, Leave_Id);
                if (dsAdminSetup.Tables[0].Rows.Count > 0)
                {
                    GetHQ();
                    Leave_Id = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    ddltype.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtLeave.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    txtLeaveto.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    txtreason.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    txtAddr.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    lblDaysCount.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    chkmanager.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    lblValidreason.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    chkho.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();                   
                    ddltype.Enabled = false;
                    txtLeave.Enabled = false;
                    txtLeaveto.Enabled = false;
                    txtreason.Enabled = false;
                    txtAddr.Enabled = false;
                    lblDaysCount.Enabled = false;
                    imgPop.Enabled = false;
                    imgPopup.Enabled = false;
                    chkmanager.Enabled = false;
                    chkho.Enabled = false;
                    lblValidreason.Enabled = false;
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
    private void GetHQ()
    {
        if (Session["sf_type"].ToString() == "1")
        {
            sfcode = Session["sf_code"].ToString();
        }
        else
        {
            sfcode = Request.QueryString["sfcode"];
        }
        Territory terr = new Territory();
        dsTerritory = terr.getSfname_Desig(sfcode, div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            lblemp.Text =
                 "<span style='font-weight: bold;color:BlueViolet; font-size:12px; font-names:Calibri'>  " + dsTerritory.Tables[0].Rows[0]["Sf_Name"].ToString() + "</span>";
            //Session["Sf_Name"] = dsTerritory.Tables[0].Rows[0]["Sf_Name"].ToString();
            lbldesig.Text =
               "<span style='font-weight: bold;color:BlueViolet; font-size:12px; font-names:Calibri '>  " + dsTerritory.Tables[0].Rows[0]["Designation_Name"].ToString() + "</span>";
            lblSfhq.Text =
                "<span style='font-weight: bold;color:BlueViolet; font-size:12px; font-names:Calibri '>  " + dsTerritory.Tables[0].Rows[0]["Sf_HQ"].ToString() + "</span>";
            lblempcode.Text =
                   "<span style='font-weight: bold;color:BlueViolet; font-size:12px; font-names:Calibri'>  " + dsTerritory.Tables[0].Rows[0]["sf_emp_id"].ToString() + "</span>";

            lbldivi.Text =
                   "<span style='font-weight: bold;color:BlueViolet; font-size:12px; font-names:Calibri '>  " + dsTerritory.Tables[0].Rows[0]["Division_Name"].ToString() + "</span>";
                 

            
        }
    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        if (Session["sf_type"].ToString() == "1")
        {
            sfcode = Session["sf_code"].ToString();
        }
        else
        {
            sfcode = Request.QueryString["sfcode"];
        }      
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
        txtLeave.Text = "";
        txtLeaveto.Text = "";
        txtreason.Text = "";
        ddltype.SelectedIndex = -1;
        lblDaysCount.Text = "";
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
            lblDaysCount.Style.Add("color","Red");
            lblDaysCount.Style.Add("Font-Size", "16px");

        }
        if (Session["sf_type"].ToString() == "1")
        {
            sfcode = Session["sf_code"].ToString();
        }
        else
        {
            sfcode = Request.QueryString["sfcode"];
        }
        DCR dcr = new DCR();
        DataSet ds = new DataSet();
        if (txtLeaveto.Text != "")
        {
            ds = dcr.getLeave_Mr(sfcode, Convert.ToDateTime(txtLeaveto.Text));
            if (ds.Tables[0].Rows.Count > 0)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Leave already applied for the selected Date');</script>");
                txtLeaveto.Text = "";
                txtLeaveto.Focus();
            }
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
            lblDaysCount.Style.Add("Font-Size", "16px");
        }
        if (Session["sf_type"].ToString() == "1")
        {
            sfcode = Session["sf_code"].ToString();
        }
        else
        {
            sfcode = Request.QueryString["sfcode"];
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

    protected void btnApproved_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        sfcode = Request.QueryString["sfcode"];
        Leave_Id = Request.QueryString["Leave_Id"].ToString();
        if (sfcode != null)
        {
            AdminSetup adm = new AdminSetup();
            iReturn = adm.Leave_Appprove(sfcode, Leave_Id);
         
            if (iReturn > 0)
            {
           //     ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approved Successfully');window.location='../MGR/MGR_Index.aspx';</script>");
                if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approved Successfully');window.location='../../MasterFiles/Leave_Admin_Approval.aspx';</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approved Successfully');window.location='../../MasterFiles/MGR/MGR_Index.aspx';</script>");
                }
                


            }
        }
    }
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {

        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
        {
            Response.Redirect("~/BasicMaster.aspx");
        }
        else
        {
            Response.Redirect("~/MasterFiles/MGR/MGR_Index.aspx");
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            txtreject.Visible = true;
            btnSubmit.Visible = true;
            btnReject.Visible = false;
            btnApproved.Enabled = false;
            lblRejectReason.Visible = true;
            txtreject.Focus();
        }
        catch (Exception ex)
        {

        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        sfcode = Request.QueryString["sfcode"];
        Leave_Id = Request.QueryString["Leave_Id"].ToString();
        if (sfcode != null)
        {
            AdminSetup adm = new AdminSetup();
            iReturn = adm.Leave_Reject_Mgr(sfcode, Session["sf_Name"].ToString(), Leave_Id, txtreject.Text);
            if (iReturn > 0)
            {
                if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Rejected Successfully');window.location='../../MasterFiles/Leave_Admin_Approval.aspx';</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Rejected Successfully');window.location='../../MasterFiles/MGR/MGR_Index.aspx';</script>");
                }

            }
        }
    }
    protected void btnleavedcr_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/DCR/DCR_Entry.aspx");
    }
}