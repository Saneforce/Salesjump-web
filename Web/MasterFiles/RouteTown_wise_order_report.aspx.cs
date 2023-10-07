using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_RouteTown_wise_order_report : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    public static string sf_code = string.Empty;
    DataSet dsSalesForce = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = HttpContext.Current.Session["div_code"].ToString();
        sf_code = HttpContext.Current.Session["sf_code"].ToString();
        //sf_code = "MR2690";
        if (!Page.IsPostBack)
        {
            
            base.OnPreInit(e);

            fillsubdivision();
            FillMRManagers("0");
        }
    }
    [WebMethod]
    public static string getroutTown(string sf_code)
    {

        DataTable dt = null;
        string strQry = string.Empty;

        strQry = "Exec sp_get_salesforce_routTown '" + div_code + "','" + sf_code  + "'";
        dt = execQuery(strQry);

        return JsonConvert.SerializeObject(dt);
    }
    [WebMethod]
    public static string getroutTownOrderval(string sf_code,string year)
    {

        DataTable dt = null;
        string strQry = string.Empty;

        strQry = "Exec sp_get_salesforce_routTown_orderVal '" + div_code + "','" + sf_code + "','" + year + "'";
        dt = execQuery(strQry);

        return JsonConvert.SerializeObject(dt);
    }
    [WebMethod]
    public static string getroutTownplanDate(string sf_code,string year)
    {

        DataTable dt = null;
        string strQry = string.Empty;

        strQry = "Exec sp_get_salesforce_routTown_planDate '" + div_code + "','" + sf_code + "','" + year + "'";
        dt = execQuery(strQry);

        return JsonConvert.SerializeObject(dt);
    }
    
   
    [WebMethod]
    public static string getroutTownplanDateWtype(string sf_code,string year)
    {

        DataTable dt = null;
        string strQry = string.Empty;

        strQry = "Exec sp_get_salesforce_routTown_planDate_Wtype '" + div_code + "','" + sf_code + "','" + year + "'";
        dt = execQuery(strQry);

        return JsonConvert.SerializeObject(dt);
    }
    public static DataTable execQuery(string strQry)
    {
        DataTable dt = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        try
        {
            dt = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dt;
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
    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.SalesForceList(div_code, sf_code, Sub_Div_Code);
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
}