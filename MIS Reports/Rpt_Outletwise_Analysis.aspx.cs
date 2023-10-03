using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Bus_EReport;
using Newtonsoft.Json;
using System.Web.Services;
using System.Data.SqlClient;
using System.Globalization;

public partial class MIS_Reports_Rpt_Outletwise_Analysis : System.Web.UI.Page
{
    public static string divCode = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string Sf_Name = string.Empty;
    public static string FMonth = string.Empty;
    public static string TMonth = string.Empty;
    public static string FYear = string.Empty;
    public static string TYear = string.Empty;
    public static string FMName = string.Empty;
    public static string Subdiv = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divCode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["SF_code"].ToString();
        Sf_Name = Request.QueryString["SF_Name"].ToString();
        FMonth = Request.QueryString["Mnth"].ToString();
        FYear = Request.QueryString["Yr"].ToString();
        Subdiv = Request.QueryString["subdiv"].ToString();
        FMName = Request.QueryString["MnthName"].ToString();
        lblsf_name.Text = Sf_Name;
        Label2.Text = "Outletwise Analysis for " + FMName + " - " + FYear;
    }
    [WebMethod]
    public static string GEt_Calls()
    {
        Sales SFD = new Sales();
        DataSet ds = SFD.GetSFMonthlyVisitDetails(Sf_Code, divCode, FMonth, FYear);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GEt_Visit()
    {
        Sales SFD = new Sales();
        DataSet ds = SFD.GetSFMonthlyCallDetails(Sf_Code, divCode, FMonth, FYear);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GEt_Sales()
    {
        Sales SFD = new Sales();
        DataSet ds = SFD.GetSFMonthlySSaleDetails(Sf_Code, divCode, FMonth, FYear);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GEt_Products()
    {
        Product SFD = new Product();
        DataSet ds = SFD.getproductname(divCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}