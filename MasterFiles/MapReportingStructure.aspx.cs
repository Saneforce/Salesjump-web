using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MapReportingStructure : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string reporting_to = string.Empty;
    string sf_code = string.Empty;
    string VacantMode = string.Empty;
    string type = string.Empty;
    string sfname = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string joiningdate = string.Empty;
    string UserDefin = string.Empty;
    string Sf_emp_id = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        reporting_to = Request.QueryString["reporting_to"];
        VacantMode = Request.QueryString["VacantMode"];
        type = Request.QueryString["type"];
        sfname = Request.QueryString["sfname"];
        Session["backurl"] = "SalesForceList.aspx";
        joiningdate = Request.QueryString["joiningdate"];
        UserDefin = Request.QueryString["UserDefin"];
        Sf_emp_id = Request.QueryString["Sf_emp_id"];

        if (!Page.IsPostBack)
        {
           // menu1.Status = "SalesForce made Vacant successfully!! Pls Verify the New Reporting Structure.. ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Vacant successfully!! Pls Verify the New Reporting Structure..');</script>");
            FillSalesForce();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.FindControl("pnlHead").Visible = true;
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

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForce_ReportingTo(div_code,reporting_to);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
    }

    protected DataSet Fill_Reporting_To()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForce_ReportingTo(div_code, reporting_to,2);
        return dsSalesForce;
    }

    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int iReturn = -1;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSF_Code = (Label)e.Row.FindControl("lblSF_Code");
            DropDownList ddlReporting = (DropDownList)e.Row.FindControl("ddlReporting_To");
            if (ddlReporting != null)
            {
                SalesForce sf = new SalesForce();
                dsSalesForce = sf.getReportingTo(reporting_to);
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    string sReportingTo = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    ddlReporting.SelectedIndex = ddlReporting.Items.IndexOf(ddlReporting.Items.FindByValue(sReportingTo));

                    //SalesForce sf = new SalesForce();
                    iReturn = sf.RecordUpdate(lblSF_Code.Text.ToString(), ddlReporting.SelectedValue.ToString().Trim());

                    int iReturn2 = sf.Vac_Team_RecordAdd(reporting_to, lblSF_Code.Text.ToString(),div_code);
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdSalesForce.Rows)
        {
            Label lblSF_Code = (Label)gridRow.Cells[1].FindControl("lblSF_Code");
            sf_code = lblSF_Code.Text;
            DropDownList ddlSF_Code = (DropDownList)gridRow.Cells[3].FindControl("ddlReporting_To");
            reporting_to = ddlSF_Code.SelectedValue;
            SalesForce sf = new SalesForce();
            iReturn = sf.RecordUpdate(sf_code, reporting_to);
        }
         if (iReturn > 0 )
        {
            //menu1.Status = "Reporting Structure Modified Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reporting Structure Modified Successfully');window.location='SalesForceList.aspx';</script>");

            //if (VacantMode == "T")
            //{
            //    Session["type"] = type;
            //    Response.Redirect("~/MasterFiles/SalesForce_Transfer.aspx?sfcode=" + reporting_to + "&sfname=" + sfname + "&joiningdate=" + joiningdate + "&UserDefin=" + UserDefin + "&Sf_emp_id=" + Sf_emp_id);
            //}

            //Response.Redirect("~/MasterFiles/SalesForceList.aspx");
        }
    }
}