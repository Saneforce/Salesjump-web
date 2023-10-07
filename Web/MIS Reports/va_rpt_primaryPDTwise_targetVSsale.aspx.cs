using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using Newtonsoft.Json;
using System.Web.Services;
using System.Data.SqlClient;
using System.Globalization;
public partial class MIS_Reports_va_rpt_primaryPDTwise_targetVSsale : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    public static string divcode = string.Empty;
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

    public static DateTime ldt { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["code"].ToString();
        hidn_sf_code.Value = sfCode;
        hFMonth.Value = Request.QueryString["fmonth"].ToString();
        hTYear.Value = Request.QueryString["TYear"].ToString();
        hTMonth.Value = Request.QueryString["Tmonth"].ToString();
        lblsf_name.Text = Request.QueryString["SF_Name"].ToString();

        subDiv.Value = Request.QueryString["subDiv"].ToString();

        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        string strFmonth = mfi.GetMonthName(Convert.ToInt32(Request.QueryString["FMonth"].ToString())).ToString(); //August

        string strTmonth = mfi.GetMonthName(Convert.ToInt32(Request.QueryString["TMonth"].ToString())).ToString(); //August



        //lblyear.Text = strFmonth.Substring(0, 3) + " - " + Request.QueryString["FYear"].ToString() + " To " + strTmonth.Substring(0, 3) + " - " + Request.QueryString["TYear"].ToString(); ;

    }
    public class Item
    {
        public string SF_Code { get; set; }
        public string sf_name { get; set; }
        public string Designation { get; set; }
        public string StateName { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static Item[] getdata(string sf_Code, string SubDiv, int fMonth, int tYear, int tMonth)
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
        var firstDayOfMonth = new DateTime(tYear, fMonth, 1);
        ldt = firstDayOfMonth.AddMonths(1).AddDays(-1);
       string fdt = firstDayOfMonth.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        string Tdt = ldt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        Product pro = new Product();
        List<Item> empList = new List<Item>();
        DataSet dsAccessmas = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec getSFUsers '" + sf_Code + "'," + div_code.TrimEnd(',') + ",'" + fdt + "','" + Tdt + "','" + SubDiv + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsAccessmas);
        con.Close();
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            Item emp = new Item();
            emp.SF_Code = row["SF_Code"].ToString();
            emp.sf_name = row["SF_Name"].ToString(); 
            emp.Designation = row["Designation"].ToString();
            emp.StateName = row["StateName"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static targetvsSales[] getProductTargetSale(string sf_Code, string fMonth, string tYear, string tMonth)
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

        TargetFixation pro = new TargetFixation();
        List<targetvsSales> prolist = new List<targetvsSales>();
        DataSet dsAccessmas = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Exec Get_PriProduct_Target_vs_Sal_vasu '" + sf_Code + "','" + tYear + "','" + fMonth + "','" + tMonth + "','" + div_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsAccessmas);
        con.Close();
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            targetvsSales pds = new targetvsSales();
            pds.pCode = row["Sf_Code"].ToString();
            pds.cyear = row["cyear"].ToString();
            pds.cmonth = row["cmonth"].ToString();
            pds.orderVal = row["ord_val"].ToString();
            pds.target = row["val"].ToString();
            prolist.Add(pds);
        }
        return prolist.ToArray();
    }
    public class targetvsSales
    {
        public string pCode { get; set; }
        public string cyear { get; set; }
        public string cmonth { get; set; }
        public string orderVal { get; set; }
        public string target { get; set; }
        
    }
}