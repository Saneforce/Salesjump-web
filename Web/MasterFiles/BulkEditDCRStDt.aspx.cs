using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_BulkEditDCRStDt : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsReport = null;
    string div_code = string.Empty;
    string reporting_to = string.Empty;
    string sf_code = string.Empty;
    string dcr = string.Empty;
    string ReportingMGR = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DateTime DcrDate;
    DateTime NewDCRDate;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "SalesForceList.aspx";
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            txtTPDCRStartDate.Focus();
            reporting_to = Request.QueryString["reporting_to"];
            FillReporting();
           // FillSalesForce();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            lblSelect.Visible = true;
            //menu1.FindControl("pnlHead").Visible = true;         
        }
        FillColor();
        FillgridColor();
    }
    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFilter.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;

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
    private void FillReporting()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFilter.DataTextField = "Sf_Name";
            ddlFilter.DataValueField = "Sf_Code";
            ddlFilter.DataSource = dsSalesForce;
            ddlFilter.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }
    }

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getsf(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
    }

    
    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblBackColor = (Label)e.Row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            e.Row.BackColor = System.Drawing.Color.FromName(bcolor);


            Label txtLastDCRStDt = (Label)e.Row.FindControl("txtLastDCRStDt");
            DcrDate = Convert.ToDateTime(txtLastDCRStDt.Text);
            DateTime ddate;
            ddate = DcrDate.AddDays(-1);
            txtLastDCRStDt.Text = ddate.ToString("dd/MM/yyyy");
        }
        FillgridColor();

    }
    private void FillgridColor()
    {

        foreach (GridViewRow grid_row in grdSalesForce.Rows)
        {

            Label lblBackColor = (Label)grid_row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            grid_row.BackColor = System.Drawing.Color.FromName(bcolor);


        }
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;

        DateTime dcrnewdate;
        DateTime dcrexistdate;
        bool err = false;

        foreach (GridViewRow gridRow in grdSalesForce.Rows)
        {
            Label lblSF_Code = (Label)gridRow.Cells[1].FindControl("lblSF_Code");
            sf_code = lblSF_Code.Text;

            Label txtExtDCRStDt = (Label)gridRow.Cells[3].FindControl("txtExtDCRStDt");
            dcrexistdate = Convert.ToDateTime(txtExtDCRStDt.Text.ToString());

            TextBox txtDCR = (TextBox)gridRow.Cells[3].FindControl("txtDCRStDt");

            if (txtDCR.Text != "")
            {

                dcrnewdate = Convert.ToDateTime(txtDCR.Text.ToString());

                if (Convert.ToString(dcrnewdate) != "")
                {

                    if (dcrexistdate > dcrnewdate)
                    {
                        SalesForce ds = new SalesForce();
                        bool isD = false;
                        isD = ds.IsDcrStarted(lblSF_Code.Text);
                        if (isD == true)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('New DCR Date Cannot be less than Existing DCR Date ');</script>");
                            //txtDCR.BackColor = System.Drawing.Color.BlueViolet;
                            txtDCR.Focus();
                            err = true;
                            break;
                        }
                    }

                }
            }
        }
        if (err == false)
        {
            foreach (GridViewRow gridRow in grdSalesForce.Rows)
            {
                Label lblSF_Code = (Label)gridRow.Cells[1].FindControl("lblSF_Code");
                sf_code = lblSF_Code.Text;
                TextBox txtDCR = (TextBox)gridRow.Cells[7].FindControl("txtDCRStDt");
                dcr = txtDCR.Text;
                if (dcr != "")
                {
                    SalesForce sf = new SalesForce();
                    iReturn = sf.BulkUpdateDCR(sf_code, Convert.ToDateTime(dcr));
                }
            }
            if (iReturn > 0)
            {
                // menu1.Status = "DCR Start Date have been updated successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated successfully');</script>");
                //Response.Redirect("~/MasterFiles/SalesForceList.aspx");
            }
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        grdSalesForce.Visible = true;
        btnSubmit.Visible = true;
        btnSave.Visible = true;
        lblSelect.Visible = false;
        System.Threading.Thread.Sleep(time);
        foreach (GridViewRow gridRow in grdSalesForce.Rows)
        {
            Label txtLastDCRStDt = (Label)gridRow.Cells[5].FindControl("txtLastDCRStDt");
            Label lblSF_Code = (Label)gridRow.Cells[1].FindControl("lblSF_Code");
            DcrDate = Convert.ToDateTime(txtLastDCRStDt.Text);
            NewDCRDate = Convert.ToDateTime(txtTPDCRStartDate.Text);
            TextBox txtDCRStDt = (TextBox)gridRow.Cells[7].FindControl("txtDCRStDt");
            if (NewDCRDate > DcrDate)
            {
                txtDCRStDt.Text = txtTPDCRStartDate.Text;
            }
            else
            {
                SalesForce ds = new SalesForce();
                bool isD = false;
                isD = ds.IsDcrStarted(lblSF_Code.Text);
                if(isD == false)
                    txtDCRStDt.Text = txtTPDCRStartDate.Text;
                else
                    txtDCRStDt.Text ="";
            }
        }

        FillgridColor();
    }
    protected void btnsrch_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        lblSelect.Visible = false;
        btnSave.Visible = true;
        btnSubmit.Visible = true;
        if (ddlFilter.SelectedIndex > 0)
        {
            FillSalesForce_Reporting();
        }
        else
        {
            FillSalesForce();
        }

        FillgridColor();
    }
    private void FillSalesForce_Reporting()
    {
        string sReport = ddlFilter.SelectedValue.ToString();
        SalesForce sf = new SalesForce();
        //dsReport = sf.getReportingTo(sReport);
        //ReportingMGR = dsReport.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(); //His Reporting Mananger 
        //dsSalesForce = sf.getsf(div_code, sReport, ReportingMGR);


        //dsSalesForce = sf.UserList_BulkEditStartDate(div_code, sReport);
        DataTable dtUserList = new DataTable();
        dtUserList = sf.getUserListReportingToNew_for_all(div_code, sReport, 0, Session["sf_type"].ToString()); // 28-Aug-15 -Sridevi
        if (sReport == "admin")
        {
            dtUserList.Rows[0].Delete();
            dtUserList.Rows[0].Delete();
        }
        else
        {
            dtUserList.Rows[1].Delete();
        }
        if (dtUserList.Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dtUserList;
            grdSalesForce.DataBind();
        }
        else
        {
            grdSalesForce.DataSource = dtUserList;
            grdSalesForce.DataBind();
        }
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
    }
}