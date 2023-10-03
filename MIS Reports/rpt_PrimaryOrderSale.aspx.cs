using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;

public partial class MIS_Reports_rpt_PrimaryOrderSale : System.Web.UI.Page
{
    DataSet ds = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            fillddldiv();
            fillyear();
            fillddlmgr();
            fillddlfieldforce();
        }
    }
    private void fillddldiv()
    {
        SalesForce sf = new SalesForce();
        ds = sf.Getsubdivisionwise(div_code);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldiv.DataTextField = "subdivision_name";
            ddldiv.DataValueField = "subdivision_code";
            ddldiv.DataSource = ds;
            ddldiv.DataBind();
            ddldiv.Items.Insert(0, new ListItem("---Select ---", "0"));
        }
    }
    
    private void fillddlmgr()
    {
        SalesForce sf = new SalesForce();
        ds = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code);
        if (ds.Tables[0].Rows.Count > 0) { 
            ddlmgr.DataTextField = "Sf_Name";
        ddlmgr.DataValueField = "Sf_Code";
        ddlmgr.DataSource = ds;
        ddlmgr.DataBind();
            ddlmgr.Items.Insert(0, new ListItem("---Select Field Force---", "0"));
    }
    }
    private void fillyear()
    {
        TourPlan tp = new TourPlan();
        ds = tp.Get_TP_Edit_Year(div_code);
        if (ds.Tables[0].Rows.Count > 0)
        {
            for(int i=Convert.ToInt32(ds.Tables[0].Rows[0]["year"]);i<=DateTime.Now.Year;i++)
            {
                ddlyear.Items.Add(i.ToString());
                ddlyear.SelectedValue=DateTime.Now.Year.ToString();
            }
        }
        ddlmonth.SelectedValue = DateTime.Now.Month.ToString();
    }
    private void fillddlfieldforce()
    {
        SalesForce sf = new SalesForce();
        ds = sf.UserList_getMR(div_code, ddlmgr.SelectedValue);
        if(ds.Tables[0].Rows.Count>0)
        {
            ddlfieldforce.DataTextField = "Sf_Name";
            ddlfieldforce.DataValueField = "Sf_Code";
            ddlfieldforce.DataSource = ds;
            ddlfieldforce.DataBind();
            ddlfieldforce.Items.Insert(0, new ListItem("---Select Base Level---", "0"));
        }
    }
    protected void ddlmgr_SelectIndexchanged(object sender, EventArgs e)
    {
        fillddlfieldforce();
    }
    protected void ddldiv_SelectIndexchanged(object sender, EventArgs e)
    {
        fillddlmgr();
    }
   
}