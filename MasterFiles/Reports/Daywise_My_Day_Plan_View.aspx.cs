using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using DBase_EReport;

public partial class MasterFiles_Reports_Daywise_My_Day_Plan_View : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsdiv = new DataSet();
    DataSet dsTP = null;
    DataSet dsDivision = null;
    string strMultiDiv = string.Empty;
    string div_code = string.Empty;
    public static string sf_code = string.Empty;
    DataSet dsSf = null;
    public string sf_type = string.Empty;
    public static string sub_division = string.Empty;
    protected void Page_PreInit(object sender, EventArgs e)
    {
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
        sub_division = Session["sub_division"].ToString();
        if (Session["sf_type"].ToString() == "1")
        {

            ViewState["sf_type"] = "";
            SalesForce sf = new SalesForce();
            dsSf = sf.getReportingTo(sf_code);
            if (dsSf.Tables[0].Rows.Count > 0)
            {
                sf_code = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            if (!Page.IsPostBack)
            {
                FillMRManagers();
            }
            ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
            ddlFieldForce.Enabled = false;

        }

        else if (Session["sf_type"].ToString() == "2")
        {
            ViewState["sf_type"] = "";
            if (!Page.IsPostBack)
            {
                DataSet dsmgrsf = new DataSet();
                SalesForce sf = new SalesForce();
                DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
                    fillsubdivision();
                    FillMRManagers();
                    ddlFieldForce.SelectedValue = sf_code;
                }
                else
                {
                    DataTable dt = sf.getAuditManagerTeam(div_code, sf_code, 0);
                    dsmgrsf.Tables.Add(dt);
                    dsTP = dsmgrsf;

                    ddlFieldForce.DataTextField = "sf_name";
                    ddlFieldForce.DataValueField = "sf_code";
                    ddlFieldForce.DataSource = dsTP;
                    ddlFieldForce.DataBind();

                }
            }

            // lblDivision.Visible = false;
            // ddlDivision.Visible = false;
        }
        else
        {
            ViewState["sf_type"] = "admin";
            //UserControl_MenuUserControl c1 =
            //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            if (!Page.IsPostBack)
            {
                fillsubdivision();

            }

            if (Session["div_code"] != null)
            {


            }
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
    protected void ddlstate_SelectIndexchanged(object sender, EventArgs e)
    {
        FillMRManagers();
    }
    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();
        // dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
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




    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        ddlFieldForce.Items.Clear();
        dsSalesForce = SalesForceList(div_code, sf_code, subdiv.SelectedValue, "1", ddlstate.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }
    public static DataSet SalesForceList(string divcode, string sf_code, string Sub_Div_Code = "0", string Alpha = "1", string stcode = "0")
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsSF = null;
        //string strQry = "EXEC getHyrSFList '" + sf_code + "','" + divcode + "','" + Sub_Div_Code + "','" + Alpha + "'," + stcode + "";
        string strQry = "Select Sf_code,Sf_name+'-'+sf_Designation_Short_Name+'-'+Sf_HQ Sf_name  from Mas_Salesforce Where charindex(',' + cast('" + divcode + "' as varchar) + ',', ',' + Division_Code + ',') > 0    and State_Code = '"+ stcode + "' and sf_TP_Active_Flag = 0 and SF_Status = 0 and Sf_Code<>'admin'";

        try
        {
            dsSF = db_ER.Exec_DataSet(strQry);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }

}