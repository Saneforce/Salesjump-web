using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class MIS_Reports_mgr_working_report : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet ds = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (!Page.IsPostBack)
        {
            fillddlmgr();
        }
        
    }
    private void fillddlmgr()
    {
        SalesForce sf = new SalesForce();
        ds = sf.state_SalesForceListMgrGet_MgrOnly(div_code, sf_code);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlmgr.DataTextField = "Sf_Name";
            ddlmgr.DataValueField = "Sf_Code";
            ddlmgr.DataSource = ds;
            ddlmgr.DataBind();
            ddlmgr.Items.Insert(0, new ListItem("---Select Field Force---", "0"));
        }
    }
}