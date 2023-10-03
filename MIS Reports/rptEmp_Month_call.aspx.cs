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
public partial class MIS_Reports_rptEmp_Month_call : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string SubDiv = string.Empty;
    string FYear = string.Empty;
    string FMonth = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["SFCode"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        SubDiv = Request.QueryString["SubDiv"].ToString();

        ddlFieldForce.Value = sf_code;
        ddlFYear.Value = FYear;
        ddlFMonth.Value = FMonth;
        SubDivCode.Value = SubDiv;
        Label1.Text = " Field Force :" + Request.QueryString["SFName"].ToString();
    }
    public class Item
    {
        public string Day_id { get; set; }
       
    }

    [WebMethod(EnableSession = true)]
    public static Item[] getdata(string SF_Code, string FYera, string FMonth, string SubDiv)
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

        Expense pro = new Expense();
        List<Item> empList = new List<Item>();
        DataSet dsAccessmas = pro.getrptDay(SF_Code, div_code,FMonth, FYera,SubDiv);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            Item emp = new Item();
            emp.Day_id = row["dda"].ToString();
           
            empList.Add(emp);
        }
        return empList.ToArray();
    }



    [WebMethod(EnableSession = true)]
    public static IssueDetails[] getIssuData(string SF_Code, string FYera, string FMonth, string SubDiv)
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
        DataSet dsPro = exp.getrptIssueSlip_Month_sec(div_code, SF_Code, FYera, FMonth, SubDiv);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetails emp = new IssueDetails();
            emp.sfCode = row["SF_Code"].ToString();
            //emp.sfName = row["Sf_Name"].ToString();
            emp.proCode = row["dy"].ToString();
            //emp.proName = row["Product_Short_Name"].ToString();
            //emp.caseRate = row["Quantity"].ToString();
            emp.amount = row["order_val"].ToString();
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
        public string proCode { get; set; }
        public string proName { get; set; }
        public string caseRate { get; set; }
        public string piceRate { get; set; }
        public string amount { get; set; }
        public string TC_Count { get; set; }
        public string EC_Count { get; set; }

    }


    [WebMethod(EnableSession = true)]
    public static IssueDetails_pro[] getIssuData_pro(string SF_Code, string FYera, string FMonth, string SubDiv)
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
        DataSet dsPro = exp.getrptIssueSlip_Month_pro(div_code, SF_Code, FYera, FMonth, SubDiv);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetails_pro emp = new IssueDetails_pro();
            emp.sfCode_p = row["Sf_Code"].ToString();
            //emp.sfName = row["Sf_Name"].ToString();
            emp.dy_p = row["dy"].ToString();
            //emp.proName = row["Product_Short_Name"].ToString();
            //emp.caseRate = row["Quantity"].ToString();
            emp.amount_p = row["order_val"].ToString();
            emp.TC_Count_p = row["TC_Count"].ToString();
            emp.EC_Count_p = row["EC_Count"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class IssueDetails_pro
    {
        public string sfCode_p { get; set; }
        public string dy_p { get; set; }
        public string amount_p { get; set; }
        public string TC_Count_p { get; set; }
        public string EC_Count_p { get; set; }

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
        DataSet dsAccessmas = sf.SalesForceList(div_code.TrimEnd(','), SF_Code, SubDiv);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            FFNames emp = new FFNames();
            emp.sfCode = row["Sf_Code"].ToString();
            emp.sfName = row["Sf_Name"].ToString();
            emp.empid = row["sf_emp_id"].ToString();
            emp.sfhq = row["Sf_HQ"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class FFNames
    {
        public string sfCode { get; set; }
        public string empid { get; set; }
        public string sfName { get; set; }
        public string sfhq { get; set; }
    }
}