using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Promote : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsSalesForce1 = null;
    DataSet dsSalesForcemgr = null;
    string div_code = string.Empty;
    string sReportingTo = string.Empty;
    string reporting_to = string.Empty;
    string sf_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        reporting_to = Request.QueryString["reporting_to"];
        Session["backurl"] = "Salesforce_Promo_DePromo.aspx";
        SalesForce sf = new SalesForce();
        DataSet dssf = sf.getSfName(reporting_to);
        if (dssf.Tables[0].Rows.Count > 0)
        {
            lblHeader.Text = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) + " -  Reporting Team";
        }
        dsSalesForce1 = sf.getReportingTo(reporting_to);
        if (dsSalesForce1.Tables[0].Rows.Count > 0)
        {
            sReportingTo = dsSalesForce1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            DataSet dssf1 = sf.getSfName(sReportingTo);
            if (dssf1.Tables[0].Rows.Count > 0)
            {
                lblHeader_Mgr.Text = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) + "'s Manager  " + Convert.ToString(dssf1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) + " -  Reporting Team";
            }
        }
        FillSalesForce();
        if (!Page.IsPostBack)
        {
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
        dsSalesForce = sf.getSalesForce_ReportingTo(div_code, reporting_to);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            lblHeader.Visible = true;
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }


        dsSalesForcemgr = sf.getSalesForce_ReportingTo(div_code, sReportingTo);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            lblHeader_Mgr.Visible = true;
            grdSalesForce_Mgr.Visible = true;
            grdSalesForce_Mgr.DataSource = dsSalesForcemgr;
            grdSalesForce_Mgr.DataBind();
        }
        
    }

    protected DataSet Fill_Reporting_To()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForce_ReportingTo_MGR(div_code, reporting_to, 2);
        return dsSalesForce;
    }

    protected void grdSalesForce_Mgr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSF_Code = (Label)e.Row.FindControl("lblSF_Code");
            DropDownList ddlReporting = (DropDownList)e.Row.FindControl("ddlReporting_To");
            if (ddlReporting != null)
            {
                ddlReporting.SelectedIndex = ddlReporting.Items.IndexOf(ddlReporting.Items.FindByValue(sReportingTo));
            }
               
        }
    }

    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSF_Code = (Label)e.Row.FindControl("lblSF_Code");
            DropDownList ddlReporting = (DropDownList)e.Row.FindControl("ddlReporting_To");
            if (ddlReporting != null)
            {
                ddlReporting.SelectedIndex = ddlReporting.Items.IndexOf(ddlReporting.Items.FindByValue(reporting_to));
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
        if (iReturn > 0)
        {
            //menu1.Status = "Reporting Structure Modified Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reporting Structure Modified Successfully');window.location='Salesforce_Promo_DePromo.aspx'</script>");
            //Response.Redirect("~/MasterFiles/SalesForceList.aspx");
        }
    }
}