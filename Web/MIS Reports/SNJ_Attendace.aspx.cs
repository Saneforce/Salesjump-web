using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_SNJ_Attendace : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    public string sf_code = string.Empty;
    public string sf_type = string.Empty;
    public static string sub_division = string.Empty;
    DateTime ServerStartTime;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        sub_division = Session["sub_division"].ToString();
        if (sf_type == "1")
        {
            sf_code = Session["sf_code_MR"].ToString();
            div_code = Session["div_code"].ToString();
            fillsubdivision();
            //fillsubdivision();

        }

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
            string url = HttpContext.Current.Request.Url.AbsoluteUri.Replace("http://", "");
            string[] words = url.Split('.');
            string shortna = words[0];
            if (shortna == "www") shortna = words[1];
            if (Session["CmpIDKey"] != null && Session["CmpIDKey"].ToString() != "") { shortna = Session["CmpIDKey"].ToString(); }
            string filename = shortna + "_logo.png";
            string dynamicFolderPath = "../limg/";//which used to create                                       dynamic folder
            string path = dynamicFolderPath + filename.ToString();
            lblpath.Text = path;
            fillsubdivision();
            Fillfeildforce();
        }

    }
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code, sub_division);
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
            FillState(div_code);
            FillMRManagers();
        }
        else
        {
            FillMRManagers();
        }
    }
    private void Fillfeildforce()
    {

        ddlFieldForce.DataSource = null;
        ddlFieldForce.Items.Clear();
        ddlFieldForce.Items.Insert(0, new ListItem("--Select--", "0"));

        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceList(div_code, sf_code, sub_division);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", "0"));

        }

    }
    private void FillState(string div_code)
    {
        SalesForce dv = new SalesForce();
        ddlstate.Items.Clear();
        dsDivision = dv.getsubdiv_States(div_code, sf_code, subdiv.SelectedValue);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlstate.DataTextField = "StateName";
            ddlstate.DataValueField = "State_code";
            ddlstate.DataSource = dsDivision;
            ddlstate.DataBind();
            ddlstate.Items.Insert(0, new ListItem("--Select--", "0"));

        }

    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        ddlFieldForce.Items.Clear();
        dsSalesForce = sf.SalesForceList(div_code, sf_code, subdiv.SelectedValue, "1", ddlstate.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            //ddlSF.DataTextField = "Desig_Color";
            //ddlSF.DataValueField = "sf_code";
            //ddlSF.DataSource = dsSalesForce;
            //ddlSF.DataBind(); 


        }
    }
    protected void ddlstate_SelectIndexchanged(object sender, EventArgs e)
    {
        FillMRManagers();
    }
}