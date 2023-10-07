using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_CustomerPaymentDetail : System.Web.UI.Page
{
    #region "Declaration"
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsTP = null;
    #endregion

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
            fillsubdivision();
            FillMRManagers("0");
            FillMRManagers_MR();
            ddlMR.DataSource = null;
            ddlMR.Items.Clear();
            ddlMR.Items.Insert(0, new ListItem("---Select Base Level---", "0"));
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
    private void FillMRManagers_MR()
    {
        SalesForce sf = new SalesForce();
        //  ddlFFType.Visible = false;
        //ddlAlpha.Visible = false;        
        dsSalesForce = sf.UserList_getMR(div_code, ddlFieldForce.SelectedValue.ToString(), subdiv.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlMR.DataTextField = "sf_name";
            ddlMR.DataValueField = "sf_code";
            ddlMR.DataSource = dsSalesForce;
            ddlMR.DataBind();
            ddlMR.Items.Insert(0, new ListItem("---Select Base Level---", "0"));
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

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {



        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_getMR(div_code, ddlFieldForce.SelectedValue.ToString(), subdiv.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlMR.DataTextField = "sf_name";
            ddlMR.DataValueField = "sf_code";
            ddlMR.DataSource = dsSalesForce;
            ddlMR.DataBind();
            ddlMR.Items.Insert(0, new ListItem("---Select Base Level---", "0"));
        }
        else
        {
            ddlMR.DataSource = null;
            ddlMR.Items.Clear();
            ddlMR.Items.Insert(0, new ListItem("---Select Base Level---", "0"));

        }


    }

    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();


        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code, Sub_Div_Code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("---Select Field Force---", "0"));

        }
    }
}