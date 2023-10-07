using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_mgrscore_card : System.Web.UI.Page
{

    string div_code = string.Empty;
    DataSet dsmgrsf = new DataSet();
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsTerritory = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    DateTime dtCurrent;
    DataSet dsDoctor = null;
    DataSet dsCatg = null;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    DataSet dsSf = null;
    string tot_dcr_dr = string.Empty;
    int imissed_dr = -1;
    string sCurrentDate = string.Empty;
    string sf_type = string.Empty;

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

            fillsubdivision();
            FillMRManagers("0");
            

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

   
   

    private void FillMRManagers(string subdiv)
    {
        SalesForce sf = new SalesForce();
       dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code, subdiv);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlmgr.DataTextField = "Sf_Name";
            ddlmgr.DataValueField = "Sf_Code";
            ddlmgr.DataSource = dsSalesForce;
            ddlmgr.DataBind();
            ddlmgr.Items.Insert(0, new ListItem("---Select Manager---", "0"));
        }
    }

  
    

    

}