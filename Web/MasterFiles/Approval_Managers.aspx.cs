using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport; 

public partial class MasterFiles_Approval_Managers : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsSF = null;
    string sftype = string.Empty;
    int sf_type;
    string div_code = string.Empty; 
    string sf_code = string.Empty;
    string SF_Code = string.Empty; 
    string sf_name = string.Empty;
    string reporting_to = string.Empty; 
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "SalesForceList.aspx";
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sfcode"];
        reporting_to = Request.QueryString["reporting_to"];
        if (!Page.IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            FillSalesForce();
            FillReporting();
            FillSF_Alpha(); 
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = true;
        }

    }    
    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce(); 
        dsSalesForce = sf.getApproval_Managers(div_code, sf_code);
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
       //dsSalesForce = sf.getsf_tp( div_code,  sReport, ReportingMGR);
       //dsSalesForce = sf.getSFType(div_code); 
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
                                              
        }
    }
   
    private void FillSalesForce_Reporting()
    {
        string sReport = ddlFilter.SelectedValue.ToString();
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Reporting(div_code, sReport);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
    }

    private void FillReporting()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getUserList_Reporting(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFilter.DataTextField = "Sf_Name";
            ddlFilter.DataValueField = "Sf_Code";
            ddlFilter.DataSource = dsSalesForce;
            ddlFilter.DataBind();
        }
    }
    // Search by Option in Filter the Managers
    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsSalesForce;
            dlAlpha.DataBind();
        }
    }
    private void FillReporting(string sAlpha)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getUserList_Reporting(div_code, sAlpha);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFilter.DataTextField = "Sf_Name";
            ddlFilter.DataValueField = "Sf_Code";
            ddlFilter.DataSource = dsSalesForce;
            ddlFilter.DataBind();
        }
    }
    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        string sCmd = e.CommandArgument.ToString();
        if (sCmd == "All")
        {
            FillReporting();
        }
        else
        {
            FillReporting(sCmd);
        }
        //grdSalesForce.EditIndex = -1;
        //Fill the SalesForce Grid
        //FillSalesForce();
    }
    protected void grdSalesForce_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSalesForce.PageIndex = e.NewPageIndex;
        FillSalesForce();
        FillSalesForce_Reporting();
    }

    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillSalesForce_Reporting();
    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        FillSalesForce();
    }
}