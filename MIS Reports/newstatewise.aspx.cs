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

public partial class MIS_Reports_newstatewise : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsTP = null;
    DataSet ds = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string state_cd = string.Empty;
    string[] statecd;
    string sState = string.Empty;
	
	 protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (!Page.IsPostBack)
        {
            FillState(div_code);
            
            FillYear();
        }
    }
    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getStateProd(state_cd);
            ddlstate.DataTextField = "statename";
            ddlstate.DataValueField = "state_code";
            ddlstate.DataSource = dsState;
            ddlstate.DataBind();
            ddlstate.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
	
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();

            }
        }

        ddlFYear.SelectedValue = DateTime.Now.Year.ToString();

    }
    private void fillddlmgr()
    {
        SalesForce sf = new SalesForce();
		ddlmgr.Items.Clear();
        ds = sf.state_SalesForceListMgrGet_MgrOnly(div_code, sf_code, ddlstate.SelectedValue);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlmgr.DataTextField = "Sf_Name";
            ddlmgr.DataValueField = "Sf_Code";
            ddlmgr.DataSource = ds;
            ddlmgr.DataBind();
            ddlmgr.Items.Insert(0, new ListItem("---Select Field Force---", "0"));
        }
    }
    private void fillddlfieldforce()
    {
        SalesForce sf = new SalesForce();
		 ddlfieldforce.Items.Clear();
          ds = sf.SalesForceList(div_code, ddlmgr.SelectedValue,"0","1", ddlstate.SelectedValue);
        if (ds.Tables[0].Rows.Count > 0)
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
    protected void ddlstate_SelectIndexchanged(object sender, EventArgs e)
    {
        fillddlmgr();
    }
}