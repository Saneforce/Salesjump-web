using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_SF_MGR_Reporting : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string reporting_to = string.Empty;
    string sf_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sfcode"];
        reporting_to = Request.QueryString["reporting_to"];
        Session["backurl"] = "SalesForceList.aspx";
        if (!Page.IsPostBack)
        {
            //menu1.Status = "Manager has been created successfully!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Manager has been Created Successfully');</script>");
            FillSalesForce();
            menu1.Title = this.Page.Title;

            //menu1.FindControl("pnlHead").Visible = true;
        }

    }

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForce_ReportingTo_MGR(div_code, reporting_to, sf_code);
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
        dsSalesForce = sf.getSalesForce_ReportingTo_MGR(div_code, reporting_to, 2);
        return dsSalesForce;
    }

    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //int iReturn = -1;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSF_Code = (Label)e.Row.FindControl("lblSF_Code");
            DropDownList ddlReporting = (DropDownList)e.Row.FindControl("ddlReporting_To");
            ddlReporting.SelectedValue = sf_code;
            //if (ddlReporting != null)
            //{
            //    SalesForce sf = new SalesForce();
            //    dsSalesForce = sf.getActiveReportingTo(reporting_to);
            //    if (dsSalesForce.Tables[0].Rows.Count > 0)
            //    {
            //        string sReportingTo = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            //        ddlReporting.SelectedIndex = ddlReporting.Items.IndexOf(ddlReporting.Items.FindByValue(sReportingTo));

            //        //iReturn = sf.RecordUpdate(lblSF_Code.Text.ToString(), ddlReporting.SelectedValue.ToString().Trim());

            //    }
            //}
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
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
            //menu1.Status = "Reporting Structure has been successfully mapped ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reporting Structure has been successfully mapped');</script>");
            //Response.Redirect("~/MasterFiles/SalesForceList.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/SalesForceList.aspx");
    }

}