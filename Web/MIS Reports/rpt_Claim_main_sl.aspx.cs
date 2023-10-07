using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using Bus_EReport;
using Bus_Objects;
public partial class MIS_Reports_rpt_Claim_main_sl : System.Web.UI.Page
{
    string sfType = string.Empty;
    string sfCode = string.Empty;
    string DivCode = string.Empty;
    string SubDiv = string.Empty;
    string custCode = string.Empty;

    DataSet dsDiv = null;
    DataSet dsFF = null;
    DataSet dsTeam = null;
    DataSet dsCustomer = null;
    DataSet dsTP = null;

    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        sfType = Session["sf_type"].ToString();
        if (sfType == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sfType == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        sfCode = Session["sf_code"].ToString();
        DivCode = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillMRManagers("0");
            fillsubdivision();
            FillMRManagers_MR();
			FillYear();
            Filldescription(); filldist();
        }
    }
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsDiv = sd.Getsubdivisionwise(DivCode);
        if (dsDiv.Tables[0].Rows.Count > 0)
        {

            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsDiv;
            subdiv.DataBind();

        }
    }
    private void Filldescription()
    {
         ListedDR sf = new ListedDR();
        ddldes.Items.Clear();
        dsTeam = sf.getslab(ddlFYear.SelectedValue, DivCode);
        if (dsTeam.Tables[0].Rows.Count > 0)
        {
            ddldes.DataTextField = "SlabDesc";
            ddldes.DataValueField = "SlabDesc";
            ddldes.DataSource = dsTeam;
            ddldes.DataBind();
            ddldes.Items.Insert(0, new ListItem("--Select description--", "0"));
        }
    }
    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();

        dsTeam = sf.SalesForceListMgrGet_MgrOnly(DivCode, sfCode, Sub_Div_Code);
        if (dsTeam.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsTeam;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("---Select Manager ---", "0"));
        }
    }
	private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(DivCode);
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
    protected void ddlFYear_SelectIndexchanged(object sender, EventArgs e)
    {
        Filldescription();
    }
    private void FillMRManagers_MR()
    {
        //SalesForce sf = new SalesForce();
        //dsFF = sf.UserList_getMR(DivCode, ddlFieldForce.SelectedValue.ToString(), subdiv.SelectedValue.ToString());
        //if (dsFF.Tables[0].Rows.Count > 0)
        //{
        //    ddlMR.DataTextField = "sf_name";
        //    ddlMR.DataValueField = "sf_code";
        //    ddlMR.DataSource = dsFF;
        //    ddlMR.DataBind();
        //    ddlMR.Items.Insert(0, new ListItem("---Select Base Level---", "0"));
        //}
        //else
        //{
        //    ddlMR.DataSource = null;
        //    ddlMR.Items.Clear();
        //    ddlMR.Items.Insert(0, new ListItem("---Select Base Level---", "0"));
        //}
    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillMRManagers_MR();
    }
    public void filldist()
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.SalesForceList(DivCode, sfCode, subdiv.SelectedValue);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = ds;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    protected void ddldiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldist();
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

}