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

public partial class MIS_Reports_rpt_Retailer_Wise_Produt_Trend_Analysis : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string SubDiv = string.Empty;
    string FYear = string.Empty;
    string FMonth = string.Empty;
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
  //  string FMonth = string.Empty;
  //  string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string stype = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
  //  DataSet dsSalesForce = new DataSet();
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
    #endregion
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

        subDiv.Value = Request.QueryString["subDiv"].ToString();

        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        string strFmonth = mfi.GetMonthName(Convert.ToInt32(Request.QueryString["FMonth"].ToString())).ToString(); //August

        string strTmonth = mfi.GetMonthName(Convert.ToInt32(Request.QueryString["TMonth"].ToString())).ToString(); //August

        Label1.Text = "Retailer Wise Product Trend Analysis "+  strFmonth.Substring(0, 3) + " - " + Request.QueryString["FYear"].ToString() + " To " + strTmonth.Substring(0, 3) + " - " + Request.QueryString["TYear"].ToString(); ;

        }

    public class Item
    {
        public string product_id { get; set; }
        public string product_name { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static Item[] getdata()
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

        Product pro = new Product();
        List<Item> empList = new List<Item>();
        DataSet dsAccessmas = pro.getproductname(div_code.TrimEnd(','));
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            Item emp = new Item();
            emp.product_id = row["Product_Detail_Code"].ToString();
            emp.product_name = row["Product_Detail_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static FFNames[] getretailer(string SF_Code, string FY, string FMON, string TY, string TMON)
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

        SalesForce sf = new SalesForce();
        List<FFNames> empList = new List<FFNames>();
        DataSet dsAccessmas = sf.get_Retailer(div_code.TrimEnd(','),SF_Code, FY,  FMON,  TY,  TMON);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            FFNames emp = new FFNames();
            emp.ListedDrCode = row["ListedDrCode"].ToString();
            emp.ListedDr_Name = row["ListedDr_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class FFNames
    {
        public string ListedDrCode { get; set; }
        public string ListedDr_Name { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static string GEt_Retailer_wise_product_analysis(string SF_Code, string FY,string FMON,string TY, string TMON)
    {
        string div_code = "";
        DataSet ds = new DataSet();
        div_code = HttpContext.Current.Session["div_code"].ToString();
        SalesForce sf = new SalesForce();
        ds = sf.GEt_Data_Retailer_wise_product_analysis(div_code.TrimEnd(','), SF_Code, FY, FMON, TY, TMON);
        return JsonConvert.SerializeObject(ds.Tables[0]);
      
    }



}