using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;
using iTextSharp.tool.xml;

using System.Web.Services;
using System.Globalization;

public partial class MIS_Reports_rpt_Customer_target_VS_sales_analysis : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    decimal iTotLstCount2 = 0;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string stype = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;

    DataSet dsSalesForce = new DataSet();

    DataSet dsMGR = new DataSet();
    DataSet dsMr = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string tot_dr = string.Empty;
    string tot_Drr = string.Empty;

    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;

    DataSet dsprd = new DataSet();
    string sCurrentDate = string.Empty;
    string stockist_code = string.Empty;
    string stURL = string.Empty;
    string Stock_name = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["SF_Code"].ToString();
       // FYear = Request.QueryString["FYear"].ToString();
        hidn_sf_code.Value = sfCode;
        hFYear.Value = Request.QueryString["FYear"].ToString();
        hFMonth.Value = Request.QueryString["FMonth"].ToString();
        hTYear.Value = Request.QueryString["TYear"].ToString();
        hTMonth.Value = Request.QueryString["TMonth"].ToString();
        hTMonth.Value = Request.QueryString["TMonth"].ToString();
        lblsf_name.Text = Request.QueryString["SF_Name"].ToString();

        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        string strFmonth = mfi.GetMonthName( Convert.ToInt32(Request.QueryString["FMonth"].ToString())).ToString(); //August

        string strTmonth = mfi.GetMonthName( Convert.ToInt32(Request.QueryString["TMonth"].ToString())).ToString(); //August
       


        lblyear.Text = strFmonth.Substring(0,3)  + " - "+ Request.QueryString["FYear"].ToString() + " To "+ strTmonth.Substring(0,3) + " - "+ Request.QueryString["TYear"].ToString(); ;
    }

    public class Routes
    {
        public string rName { get; set; }
        public string rCode { get; set; }
        
    }
    [WebMethod(EnableSession = true)]
    public static Routes[] getRoutes(string SF_Code)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        RoutePlan rop = new RoutePlan();
        DataSet DsRoute = rop.get_Route_Name(div_code, SF_Code);
        List<Routes> vList = new List<Routes>();
        foreach (DataRow row in DsRoute.Tables[0].Rows)
        {
            Routes vl = new Routes();
            vl.rName = row["Territory_Name"].ToString();
            vl.rCode = row["Territory_Code"].ToString();          
            vList.Add(vl);
        }
        return vList.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static Retailertargets[] getCustomerSales(string SF_Code,string FYear,string FMonth,string TYear,string TMonth)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        ListedDR ldr = new ListedDR();
        DataSet DsRetailer = ldr.Get_Retailer_Target_vs_Sal(SF_Code, FYear, FMonth, TYear, TMonth);

        List<Retailertargets> vList = new List<Retailertargets>();
        foreach (DataRow row in DsRetailer.Tables[0].Rows)
        {
            Retailertargets vl = new Retailertargets();
            vl.rCode = row["Route"].ToString();
            vl.cName = row["ListedDr_Name"].ToString();
            vl.cCode = row["ListedDrCode"].ToString();
            vl.catName = row["Doc_cat_ShortName"].ToString();
            vl.splName = row["Doc_Special_Name"].ToString();
            vl.address = row["ListedDr_Address1"].ToString();
            vl.mobile = row["ListedDr_Mobile"].ToString();
            vl.ordVal = row["ord_val"].ToString();
            vl.tarVal = row["camount"].ToString();
            vl.cMonth = row["cmonth"].ToString();
            vl.cYear = row["cyear"].ToString();
            vList.Add(vl);
        }
        return vList.ToArray();
    }

    public class Retailertargets
    {        
        public string rCode { get; set; }
        public string cName { get; set; }
        public string cCode { get; set; }
        public string catName { get; set; }
        public string splName { get; set; }
        public string address { get; set; }
        public string mobile { get; set; }
        public string ordVal { get; set; }
        public string tarVal { get; set; }
        public string cMonth { get; set; }
        public string cYear { get; set; }
    }
}