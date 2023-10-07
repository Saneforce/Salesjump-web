using System;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using Newtonsoft.Json;

public partial class MIS_Reports_RptTpVDayPlan : System.Web.UI.Page
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
        sfname = Request.QueryString["SF_Name"].ToString();
        FDT = Request.QueryString["Fdate"].ToString();
        TDT = Request.QueryString["Tdate"].ToString();
        subdiv = Request.QueryString["Sub_Div"].ToString();
        lblsf_name.Text = sfname;
    }
    [WebMethod]
    public static string GetSFdets(string Div)
    {
        Product SFD = new Product();
        DataSet ds = SFD.getDCRUsers(sfcode, Div, FDT, TDT, subdiv);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetDayPlan(string Div)
    {
        DCR SFD = new DCR();
        DataTable ds = SFD.getDataTable("exec getTPDateWorkedRoutes '" + sfcode + "','" + Div + "','" + FDT + "','" + TDT + "','" + subdiv + "'");
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod]
    public static string GetDates()
    {
        DCR SFD = new DCR();
        DataTable ds = SFD.getDataTable("exec getTPDatesForRpt '" + FDT + "','" + TDT + "'");
        return JsonConvert.SerializeObject(ds);
    }
}