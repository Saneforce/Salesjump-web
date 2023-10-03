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

public partial class MIS_Reports_rpt_employee_wise_primary_order : System.Web.UI.Page
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

        DateTime dtfrom = DateTime.ParseExact(FYear, "dd/MM/yyyy", null);
        string strfrom = dtfrom.ToString("yyyy-MM-dd");

        DateTime dtto = DateTime.ParseExact(FMonth, "dd/MM/yyyy", null);
        string strto = dtto.ToString("yyyy-MM-dd");

        Label2.Text = "Employeewise Primary Order Details From : " + FYear + " To " + FMonth;

        ddlFieldForce.Value = sf_code;
        ddlFYear.Value = strfrom;
        ddlFMonth.Value = strto;
        SubDivCode.Value = SubDiv;
        Label1.Text = " <span style='color:blue'>Team Name :</span>" + Request.QueryString["SFName"].ToString();
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
        DataSet dsPro = exp.getrptPrimaryIssueSlip_Month(div_code, SF_Code, FYera, FMonth, SubDiv);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetails emp = new IssueDetails();
            emp.sfCode = row["SF_Code"].ToString();
            //emp.sfName = row["Sf_Name"].ToString();
            emp.proCode = row["Product_Code"].ToString();
            //emp.proName = row["Product_Short_Name"].ToString();
            emp.caseRate = row["Quantity"].ToString();
            emp.amount = row["order_val"].ToString();
            // emp.TC_Count = row["TC_Count"].ToString();
            emp.netWeight = row["net_weight"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }


    [WebMethod(EnableSession = true)]
    public static TotEcTc[] getIssuDataTcEc(string SF_Code, string FYera, string FMonth, string SubDiv)
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
        DataSet dsPro = exp.getrptPrimaryIssueSlip_MonthTCEC(div_code, SF_Code, FYera, FMonth, SubDiv);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            TotEcTc emp = new TotEcTc();
            emp.sfCode = row["SF_Code"].ToString();
            emp.TC_Count = row["TC_Count"].ToString();
            emp.EC_Count = row["EC_Count"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class TotEcTc
    {
        public string sfCode { get; set; }
        public string TC_Count { get; set; }
        public string EC_Count { get; set; }

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
        public string netWeight { get; set; }

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
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class FFNames
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
    }
}