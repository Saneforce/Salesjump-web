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
public partial class MIS_Reports_rptPrimvsSec : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string SubDiv = string.Empty;
    string FDate = string.Empty;
    string TDate = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FDate = Request.QueryString["FDate"].ToString();
        TDate = Request.QueryString["TDate"].ToString();
        SubDiv = "0";
        DateTime result4 = DateTime.ParseExact(FDate, "d/MM/yyyy", CultureInfo.InvariantCulture);
        FDate = result4.ToString("yyyy-MM-dd");

        DateTime result10 = DateTime.ParseExact(TDate, "d/MM/yyyy", CultureInfo.InvariantCulture);
        TDate = result10.ToString("yyyy-MM-dd");
        ddlFieldForce.Value = sf_code;
        ddlFYear.Value = FDate;
        ddlFMonth.Value = TDate;
        SubDivCode.Value = SubDiv;
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
            emp.product_name = row["Product_Short_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static IssueDetails[] getIssuData(string SF_Code, string FDate, string TDate, string SubDiv)
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
        DataSet dsPro = exp.getrptProVsSec(div_code, SF_Code, FDate, TDate, SubDiv);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetails emp = new IssueDetails();
            emp.sfCode = row["SF_Code"].ToString();
            //emp.sfName = row["Sf_Name"].ToString();
            emp.dy = row["dy"].ToString();
            emp.SDP = row["SDP_Name"].ToString();
            emp.Sub_Date = row["Submission_Date"].ToString();
            emp.wrk_Name = row["WorkType_Name"].ToString();
            emp.wrk_With = row["Worked_with_Name"].ToString();
            emp.rt_visit = row["Re_visit_Cou"].ToString();
            emp.order = row["order_val"].ToString();
            emp.TC_Count = row["TC_Count"].ToString();
            emp.EC_Count = row["EC_Count"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class IssueDetails
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string dy { get; set; }
        public string SDP { get; set; }
        public string Sub_Date { get; set; }
        public string wrk_Name { get; set; }
        public string wrk_With { get; set; }
        public string rt_visit { get; set; }
        public string order { get; set; }
        public string TC_Count { get; set; }
        public string EC_Count { get; set; }

    }


    [WebMethod(EnableSession = true)]
    public static IssueDetails_pro[] getIssuData_pro(string SF_Code, string FDate, string TDate, string SubDiv)
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
        List<IssueDetails_pro> empList = new List<IssueDetails_pro>();
        Expense exp = new Expense();
        DataSet dsPro = exp.getrptProVsSec_sec(div_code, SF_Code, FDate, TDate, SubDiv);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetails_pro emp = new IssueDetails_pro();
            emp.sfCode_p = row["SF_Code"].ToString();
            emp.dy_p = row["dy"].ToString();
            emp.proName = row["Product_Code"].ToString();
            emp.Proqty = row["Qty"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class IssueDetails_pro
    {
        public string sfCode_p { get; set; }
        public string dy_p { get; set; }
        public string proName { get; set; }
        public string Proqty { get; set; }

    }


    [WebMethod(EnableSession = true)]
    public static IssueDetails1[] getIssuData1(string SF_Code, string FDate, string TDate, string SubDiv)
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
        DataSet dsPro = exp.getrptProVsSec1(div_code, SF_Code, FDate, TDate, SubDiv);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetails1 emp = new IssueDetails1();
            emp.sfCode_pro = row["SF_Code"].ToString();
            //emp.sfName = row["Sf_Name"].ToString();
            emp.dy_pro = row["dy"].ToString();
            emp.SDP_pro = row["SDP"].ToString();
            emp.Sub_Date_pro = row["Submission_Date"].ToString();
            emp.wrk_Name_pro = row["WorkType_Name"].ToString();
            emp.dis_visit_pro = row["dis_visit_Cou"].ToString();
            emp.order_pro = row["order_val"].ToString();
            emp.TC_Count_pro = row["TC_Count"].ToString();
            emp.EC_Count_pro = row["EC_Count"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class IssueDetails1
    {
        public string sfCode_pro { get; set; }
        public string sfName_pro { get; set; }
        public string dy_pro { get; set; }
        public string SDP_pro { get; set; }
        public string Sub_Date_pro { get; set; }
        public string wrk_Name_pro { get; set; }
        public string wrk_With_pro { get; set; }
        public string dis_visit_pro { get; set; }
        public string order_pro { get; set; }
        public string TC_Count_pro { get; set; }
        public string EC_Count_pro { get; set; }

    }


    [WebMethod(EnableSession = true)]
    public static IssueDetails_pro1[] getIssuData_pro1(string SF_Code, string FDate, string TDate, string SubDiv)
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
        List<IssueDetails_pro1> empList = new List<IssueDetails_pro1>();
        Expense exp = new Expense();
        DataSet dsPro = exp.getrptProVsSec_pro(div_code, SF_Code, FDate, TDate, SubDiv);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetails_pro1 emp = new IssueDetails_pro1();
            emp.sfCode_p_pro = row["SF_Code"].ToString();
            emp.dy_p_pro = row["dy"].ToString();
            emp.proName_pro = row["Product_Code"].ToString();
            emp.Proqty_pro = row["Qty"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class IssueDetails_pro1
    {
        public string sfCode_p_pro { get; set; }
        public string dy_p_pro { get; set; }
        public string proName_pro { get; set; }
        public string Proqty_pro { get; set; }

    }

  
}