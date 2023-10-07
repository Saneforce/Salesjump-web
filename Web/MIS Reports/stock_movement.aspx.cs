using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using DocumentFormat.OpenXml.Drawing;
using System.IO;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Services;
using DBase_EReport;

public partial class MIS_Reports_stock_movement : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DataSet dsSalesForce = null;
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
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            fillsubdivision();
            FillMRManagers("0");
            Fillstockist(sf_code);
        }
    }
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    
    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (subdiv.SelectedValue.ToString() != "0")
        {

            FillMRManagers(subdiv.SelectedValue.ToString());
        }
        else
        {
            FillMRManagers(subdiv.SelectedValue.ToString());
        }
    }
    protected void ff_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlFieldForce.SelectedValue.ToString() != "0")
        {

            Fillstockist(ddlFieldForce.SelectedValue.ToString());
        }
        else
        {
            Fillstockist(ddlFieldForce.SelectedValue.ToString());
        }
    }
    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceList(div_code, sf_code, Sub_Div_Code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, "---Select Field Force---");
        }
    }
    private void Fillstockist(string ffcode)
    {
        DB_EReporting db_ER = new DB_EReporting();
        dsSalesForce = db_ER.Exec_DataSet("EXEC getDistHierarchy '" + ffcode + "','"+ div_code + "'");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddldist.DataTextField = "Stockist_Name";
            ddldist.DataValueField = "Stockist_Code";
            ddldist.DataSource = dsSalesForce;
            ddldist.DataBind();
            ddldist.Items.Insert(0, "---Select Distributor---");
        }
    }
}