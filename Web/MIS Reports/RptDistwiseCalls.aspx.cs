using System;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using Newtonsoft.Json;

public partial class MIS_Reports_RptDistwiseCalls : System.Web.UI.Page
{
    public static string Div = string.Empty;
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public DateTime rdt;
    public DateTime sdt;
    protected void Page_Load(object sender, EventArgs e)
    {
        Div = Session["div_code"].ToString();
        sfcode = Request.QueryString["SF_Code"].ToString();
        FDT = Request.QueryString["Fdate"].ToString();
        TDT = Request.QueryString["Tdate"].ToString();
        subdiv = Request.QueryString["Sub_Div"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        lblsf_name.Text = sfname;
    }
    [WebMethod]
    public static string getDetails(string Div)
    {
        DCR dc = new DCR();
        DataTable dt = new DataTable();
        dt = dc.getDataTable("exec ProductivityReport '" + sfcode + "','" + Div + "','" + FDT + "','" + TDT + "','" + subdiv + "'");
        return JsonConvert.SerializeObject(dt);
    }
}