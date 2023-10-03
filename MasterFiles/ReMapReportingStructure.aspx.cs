using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_ReMapReportingStructure : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string reporting_to = string.Empty;
    string sf_code = string.Empty;
    string reporting_to_main = string.Empty;
    string reporting_sf = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        reporting_to = Request.QueryString["reporting_to"];
        reporting_to_main = Request.QueryString["reporting_to"];
        reporting_sf = Request.QueryString["reporting_sf"];
        Session["backurl"] = "SalesForceList.aspx";
        if (!Page.IsPostBack)
        {
          //  menu1.Status = "SalesForce made Active successfully!! Map the Reporting Strucure ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Active successfully!! Map the Reporting Strucure');</script>");
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
        dsSalesForce = sf.getSalesForce_ReMap_ReportingTo(div_code, reporting_to, reporting_sf);
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
        dsSalesForce = sf.getSalesForce_Active_ReportingTo(div_code, reporting_to, 1);
        return dsSalesForce;
    }

    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //int iReturn = -1;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSF_Code = (Label)e.Row.FindControl("lblSF_Code");
            DropDownList ddlReporting = (DropDownList)e.Row.FindControl("ddlReporting_To");
            if (ddlReporting != null)
            {
                //SalesForce sf = new SalesForce();
                //dsSalesForce = sf.getActiveReportingTo(reporting_to);
                //if (dsSalesForce.Tables[0].Rows.Count > 0)
                //{
                //    string sReportingTo = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                ddlReporting.SelectedIndex = ddlReporting.Items.IndexOf(ddlReporting.Items.FindByValue(reporting_to));

                    //iReturn = sf.RecordUpdate(lblSF_Code.Text.ToString(), ddlReporting.SelectedValue.ToString().Trim());

               // }
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
            SalesForce sf = new SalesForce();
            iReturn = sf.del_vac_team(reporting_to_main);

            //menu1.Status = "Reporting Structure has been successfully mapped ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reporting Structure has been successfully mapped');window.location='SalesForceList.aspx';</script>");
            //Response.Redirect("~/MasterFiles/SalesForceList.aspx");
        }
    }

}