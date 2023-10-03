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
public partial class MIS_Reports_rptPro_val_sum_re_da : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    string cus = string.Empty;
    string div_code = string.Empty;
    string SubDiv = string.Empty;
    string FYear = string.Empty;
    string FMonth = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        cus = Request.QueryString["Cus_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        SubDiv = Request.QueryString["SubDiv"].ToString();

        ddlFieldForce.Value = sf_code;
        ddlFYear.Value = FYear;
        ddlFMonth.Value = FMonth;
        SubDivCode.Value = SubDiv;
        CusCode.Value = cus;
        Label1.Text = " <span style='color:blue'>Retailer Name :</span>" + Request.QueryString["CusName"].ToString();
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
        DataSet dsAccessmas = pro.getrptDay(SF_Code, div_code, FMonth, FYera, SubDiv);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            Item emp = new Item();
            emp.Day_id = row["dda"].ToString();

            empList.Add(emp);
        }
        return empList.ToArray();
    }




    [WebMethod(EnableSession = true)]
    public static IssueDetails[] getIssuData(string SF_Code,string cus, string FYera, string FMonth, string SubDiv)
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
        DataSet dsPro = exp.getrptIssueSlip_Month2_cus_dy(div_code, SF_Code,cus, FYera, FMonth, SubDiv);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetails emp = new IssueDetails();
            emp.sfCode = row["Trans_Detail_Info_Code"].ToString();
            emp.sfName = row["Trans_Detail_Name"].ToString();
            emp.proCode = row["dy"].ToString();
            emp.caseRate = row["ProQuantity"].ToString();
            emp.amount = row["order_val"].ToString();
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
    public static FFNames[] getIssuData_dis(string SF_Code,string cus,string FYera, string FMonth, string SubDiv)
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
        List<FFNames> empList = new List<FFNames>();
        Expense exp = new Expense();
        DataSet dsPro = exp.getrptIssueSlip_Month2_cus_dy_dis(div_code, SF_Code,cus, FYera, FMonth, SubDiv);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            FFNames emp = new FFNames();
            emp.sfCode = row["Trans_Detail_Info_Code"].ToString();
            emp.sfName = row["Trans_Detail_Name"].ToString();

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