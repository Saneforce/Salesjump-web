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

public partial class MasterFiles_Reports_tsr_rptSKU_Summarys_rep1 : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    public static string sf_code = string.Empty;
    string div_code = string.Empty;
    string SubDiv = string.Empty;
    string Fdate = string.Empty;
    string Tdate = string.Empty;
    string dt = string.Empty;
    string dt1 = string.Empty;
    public static string distcode = string.Empty;
    public static string distnm = string.Empty;
    public static string stcode = string.Empty;
    public static string stnm = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        Fdate = Request.QueryString["FDate"].ToString();
        Tdate = Request.QueryString["TDate"].ToString();
        distcode = Request.QueryString["Dst_code"].ToString();
        distnm = Request.QueryString["Dst_name"].ToString();
        stnm = Request.QueryString["st_name"].ToString();
        stcode = Request.QueryString["st_code"].ToString();
        //SubDiv = Request.QueryString["SubDiv"].ToString();

        ddlFieldForce.Value = sf_code;
        ddlFdate.Value = Fdate;
        ddlTdate.Value = Tdate;
        SubDivCode.Value = "0";
        DateTime result = DateTime.ParseExact(Fdate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        dt = result.ToString("dd/MM/yyyy");
        DateTime result1 = DateTime.ParseExact(Tdate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        dt1 = result1.ToString("dd/MM/yyyy");

        Label2.Text = "Daily Secondary Report - " + "FROM Date : <span style='color:Red'> " + dt + " </span> TO Date : <span style='color:Red'> " + dt1 + " </span>";
        Label1.Text = " Field Force :" + Request.QueryString["Sf_Name"].ToString();
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

    public class Item1
    {
        public string Brand_id { get; set; }
        public string Brand_name { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static Item1[] getdata1()
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
        List<Item1> empList = new List<Item1>();
        DataSet dsAccessmas = pro.getBrandname(div_code.TrimEnd(','));
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            Item1 emp = new Item1();
            emp.Brand_id = row["Product_Brd_Code"].ToString();
            emp.Brand_name = row["Product_Brd_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }



    [WebMethod(EnableSession = true)]
    public static IssueDetails[] getIssuData(string SF_Code, string Fdate, string Tdate, string SubDiv)
    {
        string div_code = "";
        string sf_Code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        sf_Code = HttpContext.Current.Session["SF_Code"].ToString();

        //   SubDiv = "0";

        Product pro = new Product();
        List<IssueDetails> empList = new List<IssueDetails>();
        Expense exp = new Expense();
        DataSet dsPro = exp.getrptIssueSlip_Month_PQ(div_code, SF_Code, Fdate, Tdate, SubDiv, stcode, distcode);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetails emp = new IssueDetails();
            emp.sfCode = row["SF_Code"].ToString();
            //emp.sfName = row["Sf_Name"].ToString();
            emp.proCode = row["Product_Detail_Code"].ToString();
            //emp.disName = row["Stockist_Name"].ToString();

            //emp.proName = row["Product_Short_Name"].ToString();
            emp.caseRate = row["Quantity"].ToString();
            emp.amount = row["order_val"].ToString();
            emp.Date = Convert.ToDateTime(row["Order_date"]).ToString("dd/MM/yyyy");
            // emp.TC_Count = row["TC_Count"].ToString();
            //  emp.EC_Count = row["EC_Count"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    //brand
    [WebMethod(EnableSession = true)]
    public static IssueDList[] IssuDatas(string SF_Code, string Fdate, string Tdate, string SubDiv)
    {
        string div_code = "";
        string sf_Code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        sf_Code = HttpContext.Current.Session["SF_Code"].ToString();

        //   SubDiv = "0";

        Product pro = new Product();
        List<IssueDList> eList = new List<IssueDList>();
        Expense exp = new Expense();
        DataSet dsPro = exp.getTSRrptIssueSlip_Month_brand1(div_code, SF_Code, Fdate, Tdate, SubDiv, stcode, distcode);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDList emp = new IssueDList();
            emp.sfCode_b = row["SF_Code"].ToString();
            emp.dt_b = Convert.ToDateTime(row["Order_date"]).ToString("dd/MM/yyyy");
            emp.BraCode = row["Product_Brd_Code"].ToString();
            emp.Braqty = row["Quantity"].ToString();
            emp.BrandEc = row["EC_Count"].ToString();
            eList.Add(emp);
        }
        return eList.ToArray();
    }

    public class IssueDList
    {
        public string sfCode_b { get; set; }
        public string dt_b { get; set; }
        public string BraCode { get; set; }
        public string Braqty { get; set; }
        public string BrandEc { get; set; }

    }



    [WebMethod(EnableSession = true)]
    public static TotEcTc[] getIssuDataTcEc(string SF_Code, string Fdate, string Tdate, string SubDiv)
    {
        string div_code = "";
        string sf_Code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        sf_Code = HttpContext.Current.Session["SF_Code"].ToString();

        //   SubDiv = "0";

        Product pro = new Product();
        List<TotEcTc> empList = new List<TotEcTc>();
        Expense exp = new Expense();
        DataSet dsPro = exp.getrptIssueSlip_MonthTCEC1_tsr(div_code, SF_Code, Fdate, Tdate, SubDiv, stcode, distcode);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            TotEcTc emp = new TotEcTc();
            emp.sfCode = row["SF_Code"].ToString();
            emp.dtt = Convert.ToDateTime(row["date1"]).ToString("dd/MM/yyyy");
            emp.TC_Count = row["TC_Count"].ToString();

            emp.EC_Count = row["EC_Count"].ToString();
            emp.TPS_Count = row["TPS"].ToString();
            emp.TLS_Count = row["TLS"].ToString();
            emp.FC_TM = row["FC_TM"].ToString();
            emp.LC_TM = row["LC_TM"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class TotEcTc
    {
        public string sfCode { get; set; }
        public string dtt { get; set; }
        public string TC_Count { get; set; }
        public string EC_Count { get; set; }
        public string TPS_Count { get; set; }
        public string TLS_Count { get; set; }
        public string FC_TM { get; set; }
        public string LC_TM { get; set; }
    }

    public class IssueDetails
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string proCode { get; set; }
        public string proName { get; set; }
        public string disCode { get; set; }
        public string disName { get; set; }
        public string caseRate { get; set; }
        public string piceRate { get; set; }
        public string amount { get; set; }
        public string TC_Count { get; set; }
        public string EC_Count { get; set; }
        public string Date { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static FFNames[] getSalesforce(string SF_Code, string SubDiv)
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
        DataSet dsAccessmas = sf.SalesForceList(div_code.TrimEnd(','), SF_Code);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            FFNames emp = new FFNames();
            emp.sfCode = row["Sf_Code"].ToString();
            emp.sfName = row["Sf_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class FFNames
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static IssueDetailsddtt[] getddtt(string SF_Code, string Fdate, string Tdate, string SubDiv)
    {
        string div_code = "";
        string sf_Code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        sf_Code = HttpContext.Current.Session["SF_Code"].ToString();

        //   SubDiv = "0";

        Product pro = new Product();
        List<IssueDetailsddtt> empList = new List<IssueDetailsddtt>();
        Expense exp = new Expense();
        DataSet dsPro = exp.getrptIssueSlip_Month_dd_tt1(div_code, SF_Code, Fdate, Tdate, SubDiv, stcode, distcode);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetailsddtt emp = new IssueDetailsddtt();
            emp.sfCode = row["SF_Code"].ToString();
            emp.sfName = row["Sf_Name"].ToString();
            //emp.dt = Convert.ToDateTime(row["dt"]).ToString("dd/MM/yyyy");
            emp.dt = row["dt"].ToString();
            emp.dd = row["dd"].ToString();
            emp.tt = row["tt"].ToString();
            emp.wt = row["WorkType_Name"].ToString();
            emp.retag = row["Retag_cnt"].ToString();
            emp.notl = row["NewOutlet"].ToString();
            emp.totl = row["totaloutlet"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class IssueDetailsddtt
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string dt { get; set; }
        public string dd { get; set; }
        public string tt { get; set; }
        public string wt { get; set; }
        public string retag { get; set; }
        public string notl { get; set; }
        public string totl { get; set; }
    }

    //new

    public class IssueDetails1
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string proCode { get; set; }
        public string proName { get; set; }
        public string disCode { get; set; }
        public string disName { get; set; }
        public string caseRate { get; set; }
        public string piceRate { get; set; }
        public string amount { get; set; }
        public string TC_Count { get; set; }
        public string EC_Count { get; set; }
        public string Date { get; set; }

    }
    [WebMethod(EnableSession = true)]
    public static IssueDetails1[] getIssuData1(string SF_Code, string Fdate, string Tdate, string SubDiv)
    {
        string div_code = "";
        string sf_Code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        sf_Code = HttpContext.Current.Session["SF_Code"].ToString();

        //   SubDiv = "0";

        Product pro = new Product();
        List<IssueDetails1> empList = new List<IssueDetails1>();
        Expense exp = new Expense();
        DataSet dsPro = exp.getrptIssueSlip_Month1(div_code, SF_Code, Fdate, Tdate, SubDiv, stcode, distcode);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetails1 emp = new IssueDetails1();
            emp.sfCode = row["SF_Code"].ToString();
            emp.sfName = row["Sf_Name"].ToString();
            //emp.sfName = row["Sf_Name"].ToString();
            //emp.proCode = row["Product_Detail_Code"].ToString();
            //emp.disName = row["Stockist_Name"].ToString();

            //emp.proName = row["Product_Short_Name"].ToString();
            emp.caseRate = row["Quantity"].ToString();
            emp.amount = row["order_val"].ToString();
            emp.Date = row["Order_date"].ToString();
            // emp.TC_Count = row["TC_Count"].ToString();
            //  emp.EC_Count = row["EC_Count"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }
}