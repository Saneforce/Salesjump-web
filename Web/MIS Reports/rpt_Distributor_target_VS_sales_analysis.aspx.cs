using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_rpt_Distributor_target_VS_sales_analysis : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hidn_sf_code.Value = Request.QueryString["SF_Code"].ToString();
        hFYear.Value = Request.QueryString["FYear"].ToString();
        hFMonth.Value = Request.QueryString["FMonth"].ToString();
        hTYear.Value = Request.QueryString["TYear"].ToString();
        hTMonth.Value = Request.QueryString["TMonth"].ToString(); DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        string strFmonth = mfi.GetMonthName(Convert.ToInt32(Request.QueryString["FMonth"].ToString())).ToString(); //August

        string strTmonth = mfi.GetMonthName(Convert.ToInt32(Request.QueryString["TMonth"].ToString())).ToString(); //August



        lblyear.Text = strFmonth.Substring(0, 3) + " - " + Request.QueryString["FYear"].ToString() + " To " + strTmonth.Substring(0, 3) + " - " + Request.QueryString["TYear"].ToString(); ;

    }

    [WebMethod]
    public static  List<distributor> GetDistributor(string SF_Code)
    {


        Stockist stk = new Stockist();

        List<distributor> HDay = new List<distributor>();
        string divcode = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsAlowtype = null;
        dsAlowtype = stk.GetStockist_subdivisionwise(divcode, "0", SF_Code);
        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            distributor d = new distributor();
            d.distCode = row["Stockist_code"].ToString();
            d.distName = row["Stockist_Name"].ToString();
            HDay.Add(d);
        }
        return HDay;
    }
    public class distributor
    {
        public string distCode { get; set; }
        public string distName { get; set; }       
    }

    [WebMethod(EnableSession = true)]
    public static DistributorValues[] getDistributorSales(string SF_Code, string FYear, string FMonth, string TYear, string TMonth)
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
        Stockist stk = new Stockist();
        DataSet DsRetailer = stk.Get_Distributor_Target_vs_Sal(SF_Code, FYear, FMonth, TYear, TMonth);        

        List<DistributorValues> vList = new List<DistributorValues>();
        foreach (DataRow row in DsRetailer.Tables[0].Rows)
        {
            DistributorValues vl = new DistributorValues();
            vl.dCode = row["Stockist_Code"].ToString();
            vl.ordVal = row["ord_val"].ToString();
            vl.tarVal = row["camount"].ToString();
            vl.cMonth = row["cmonth"].ToString();
            vl.cYear = row["cyear"].ToString();
            vList.Add(vl);
        }
        return vList.ToArray();
    }

    public class DistributorValues
    {
        public string dCode { get; set; }
        public string ordVal { get; set; }
        public string tarVal { get; set; }
        public string cMonth { get; set; }
        public string cYear { get; set; }
    }
}