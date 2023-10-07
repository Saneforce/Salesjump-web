using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;



public partial class MIS_Reports_FieldForce_Incentive : System.Web.UI.Page
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
        if (Session["sf_type"].ToString() == "2")
        {

            ViewState["sf_type"] = "";
            if (!Page.IsPostBack)
            {

                // lblMR.Value = "Field Force";                
                lblMR.Visible = true;
                ddlMR.Visible = true;
                ddlFFType.Visible = false;
                ddlAlpha.Visible = false;
                lblFF.Visible = false;
                kk.Visible = false;
                ddlFieldForce.Visible = false;
                DataSet dsmgrsf = new DataSet();
                SalesForce sf = new SalesForce();
                DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
                    FillMRManagers_MR();
                    //FillYear();
                    ddlMR.SelectedValue = sf_code;

                }
                else
                {
                    DataTable dt = sf.getAuditManagerTeam(div_code, sf_code, 0);
                    dsmgrsf.Tables.Add(dt);
                    dsTP = dsmgrsf;

                    ddlMR.DataTextField = "sf_name";
                    ddlMR.DataValueField = "sf_code";
                    ddlMR.DataSource = dsTP;
                    ddlMR.DataBind();

                    ddlSF.DataTextField = "desig_Color";
                    ddlSF.DataValueField = "sf_code";
                    ddlSF.DataSource = dsTP;
                    ddlSF.DataBind();
                    ddlFFType.Visible = false;
                }
            }

        }
        else
        {
            ViewState["sf_type"] = "admin";

            if (!Page.IsPostBack)
            {

                FillManagers();
                // lblMR.Text = "Field Force";
                lblMR.Visible = true;
                ddlMR.Visible = true;
                kk.Visible = true;
                //FillMRManagers_MR();
                ddlMR.DataSource = null;
                ddlMR.Items.Clear();
                ddlMR.Items.Insert(0, new ListItem("---Select Fieldforce---", "0"));
                //FillYear();
            }


        }

    }

    //private void FillYear()
    //{
    //    TourPlan tp = new TourPlan();
    //    dsTP = tp.Get_TP_Edit_Year(div_code);
    //    if (dsTP.Tables[0].Rows.Count > 0)
    //    {
    //        for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
    //        {
    //            ddlFYear.Items.Add(k.ToString());

    //            ddlFYear.SelectedValue = DateTime.Now.Year.ToString();

    //        }
    //    }
    //}

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();

    }
    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_AlphaAll(div_code, "admin", ddlAlpha.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }

    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            try
            {
                ddlAlpha.Visible = false;
                dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
            }
            catch (Exception)
            {
                dsSalesForce = sf.sp_UserList_Hierarchy_Vacant(div_code, sf_code);
            }
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(div_code, "admin");
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("---Select Manager---", "0"));

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        ddlFFType.Visible = false;
        ddlAlpha.Visible = false;
        dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("---Select Manager---", "0"));

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();


        }
    }
    private void FillMRManagers_MR()
    {
        SalesForce sf = new SalesForce();
        //  ddlFFType.Visible = false;
        //ddlAlpha.Visible = false;
        dsSalesForce = sf.UserList_getMR_New(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlMR.DataTextField = "sf_name";
            ddlMR.DataValueField = "sf_code";
            ddlMR.DataSource = dsSalesForce;
            ddlMR.DataBind();
            ddlMR.Items.Insert(0, new ListItem("---Select Fieldforce---", "0"));
            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();


        }
    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ViewState["sf_type"].ToString() == "admin")
        {


            SalesForce sf = new SalesForce();
            dsSalesForce = sf.UserList_getMR_New(div_code, ddlFieldForce.SelectedValue.ToString());
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                lblMR.Visible = true;
                ddlMR.Visible = true;
                ddlMR.DataTextField = "sf_name";
                ddlMR.DataValueField = "sf_code";
                ddlMR.DataSource = dsSalesForce;
                ddlMR.DataBind();
                ddlMR.Items.Insert(0, new ListItem("---Select Fieldforce---", "0"));

            }
            else
            {
                ddlMR.DataSource = null;
                ddlMR.Items.Clear();
                ddlMR.Items.Insert(0, new ListItem("---Select Fieldforce---", "0"));

            }

        }
    }
}
