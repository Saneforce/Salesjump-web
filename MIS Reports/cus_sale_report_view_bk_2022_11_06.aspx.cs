using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.Xml;
using System.IO;

public partial class MIS_Reports_cus_sale_report_view : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string subdivc = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv_code = string.Empty;
    public static string year = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["SfCode"].ToString();
        sfname = Request.QueryString["sfName"].ToString();
        subdiv_code = Request.QueryString["SubDiv"].ToString();
        year = Request.QueryString["yr"].ToString();
    }
    [WebMethod(EnableSession = true)]
    public static string getRoute_Name()
    {
        Product dv = new Product();
        DataSet dsProd = dv.getroute_dtls(Sf_Code, divcode, subdivc);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getOrderValue()
    {
        Product dv = new Product();
        string divcode = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsProd = dv.spCustomerOrders(Sf_Code, divcode, subdivc, year);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getVisitDetails()
    {
        Product dv = new Product();
        string divcode = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsProd = dv.spCustomerVisitDetails(Sf_Code, divcode, subdivc, year);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getRetailerData()
    {
        Product dv = new Product();
        string divcode = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsProd = dv.spCustomerDetails(Sf_Code, divcode, subdivc, year);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
}