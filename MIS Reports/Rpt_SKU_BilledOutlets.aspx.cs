using System;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using Newtonsoft.Json;

public partial class MIS_Reports_Rpt_SKU_BilledOutlets : System.Web.UI.Page
{
    public static string Div = string.Empty;
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string Osfcode = string.Empty;
    public static string OsfName = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public static string ProductCode = string.Empty;
    public static string ProductName = string.Empty;
    public static string TerritoryCode = string.Empty;
    public DateTime rdt;
    public DateTime sdt;
    protected void Page_Load(object sender, EventArgs e)
    {
        Div = Session["div_code"].ToString();
        sfcode = Request.QueryString["SF_Code"].ToString();
        Osfcode = Request.QueryString["OSfCode"].ToString();
        OsfName = Request.QueryString["OSfName"].ToString();
        FDT = Request.QueryString["Fdate"].ToString();
        TDT = Request.QueryString["Tdate"].ToString();
        subdiv = Request.QueryString["Sub_Div"].ToString();
        ProductName = Request.QueryString["ProductName"].ToString();
        ProductCode = Request.QueryString["ProductCode"].ToString();
        TerritoryCode = Request.QueryString["Territory_Code"].ToString();

        lblsf_name.Text = ProductName;
    }
    [WebMethod]
    public static string getDetails(string Div)
    {
        DCR dc = new DCR();
        DataTable dt = new DataTable();
        dt = dc.getDataTable("exec BilledOutletsDetails '" + sfcode + "','" + Osfcode + "','" + Div + "','" + FDT + "','" + TDT + "','" + ProductCode + "','" + TerritoryCode + "','" + subdiv + "'");
        return JsonConvert.SerializeObject(dt);
    }
}