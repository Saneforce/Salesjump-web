using System;
using Newtonsoft.Json;
using System.Data;
using System.Web.Services;
using DBase_EReport;

public partial class MGRDashboard : System.Web.UI.Page
{
    public static string tdate = string.Empty;
    public static string sfname = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        tdate = Request.QueryString["Date"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
    }
    [WebMethod(EnableSession = true)]
    public static string getMGRYear(string sf_code,string div)
    {
        DB_EReporting dv = new DB_EReporting();
        DataTable ds = new DataTable();
        ds = dv.Exec_DataTable("exec getMGRYearValues '" + sf_code + "','" + div + "','" + tdate + "'");
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string getDrReporting(string sf_code)
    {
        DB_EReporting dv = new DB_EReporting();
        DataTable ds = new DataTable();
        ds = dv.Exec_DataTable("select Sf_Code,sf_Designation_Short_Name Desig,Sf_Name+' - '+Sf_HQ SF from Mas_Salesforce where Reporting_To_SF='" + sf_code + "' order by Sf_Name");
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string getCategory( string div)
    {
        DB_EReporting dv = new DB_EReporting();
        DataTable ds = new DataTable();
        ds = dv.Exec_DataTable("select Product_Cat_Code,Product_Cat_Name from Mas_Product_Category where Division_Code='" + div + "' and Product_Cat_Active_Flag=0 order by Product_Cat_Name");
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string getMGRMonth(string sf_code,string div)
    {
        DB_EReporting dv = new DB_EReporting();
        DataTable ds = new DataTable();
        ds = dv.Exec_DataTable("exec getMGRMonthValues '" + sf_code + "','" + div + "','" + tdate + "'");
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string getMGRDay(string sf_code,string div)
    {
        DB_EReporting dv = new DB_EReporting();
        DataTable ds = new DataTable();
        ds = dv.Exec_DataTable("exec getMGRDayValues '" + sf_code + "','" + div + "','" + tdate + "'");
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string getMGRCatDay(string sf_code,string div)
    {
        DB_EReporting dv = new DB_EReporting();
        DataTable ds = new DataTable();
        ds = dv.Exec_DataTable("exec getMGRDashboardCatDay '" + sf_code + "','" + div + "','" + tdate + "'");
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string getMGRCatMonth(string sf_code,string div)
    {
        DB_EReporting dv = new DB_EReporting();
        DataTable ds = new DataTable();
        ds = dv.Exec_DataTable("exec getMGRDashboardCatMonth '" + sf_code + "','" + div + "','" + tdate + "'");
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string getMGRCatYear(string sf_code,string div)
    {
        DB_EReporting dv = new DB_EReporting();
        DataTable ds = new DataTable();
        ds = dv.Exec_DataTable("exec getMGRDashboardCatYear '" + sf_code + "','" + div + "','" + tdate + "'");
        return JsonConvert.SerializeObject(ds);
    }
}