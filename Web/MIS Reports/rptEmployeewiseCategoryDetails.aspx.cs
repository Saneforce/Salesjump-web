using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_rptEmployeewiseCategoryDetails : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string SubDiv = string.Empty;
    string FYear = string.Empty;
    string FMonth = string.Empty;
    public static string stcode = string.Empty;
    string stname = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["SFCode"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        SubDiv = Request.QueryString["SubDiv"].ToString();
        stcode = Request.QueryString["stcode"].ToString();
        stname = Request.QueryString["StName"].ToString();

        DateTime dtfrom = DateTime.ParseExact(FYear, "dd/MM/yyyy", null);
        string strfrom = dtfrom.ToString("yyyy-MM-dd");

        DateTime dtto = DateTime.ParseExact(FMonth, "dd/MM/yyyy", null);
        string strto = dtto.ToString("yyyy-MM-dd");

        Label2.Text = "Employee wise category Details From : " + FYear + " To " + FMonth;

        ddlFieldForce.Value = sf_code;
        ddlFYear.Value = strfrom;
        ddlFMonth.Value = strto;
        SubDivCode.Value = SubDiv;
        Label4.Text = " <span style='color:blue'>Team Name :</span>" + Request.QueryString["SFName"].ToString();
    }
    public class categorys
    {
        public string categoryId { get; set; }
        public string categoryName { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static categorys[] getcategorys()
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
        List<categorys> List = new List<categorys>();
        DataSet dsAccessmas = pro.getProdCate(div_code.TrimEnd(','));
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            if (row["Product_Cat_Code"].ToString() !="0")
            {
                categorys list = new categorys();
                list.categoryId = row["Product_Cat_Code"].ToString();
                list.categoryName = row["Product_Cat_Name"].ToString();
                List.Add(list);
            }
        }
        return List.ToArray();
    }



    [WebMethod(EnableSession = true)]
    public static IssueDetails[] getcetegoryorderData(string SF_Code, string FYera, string FMonth, string SubDiv)
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
        DataSet dsPro = exp.getCategorywiseorderDetails(div_code, SF_Code, FYera, FMonth, SubDiv);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetails emp = new IssueDetails();
            emp.sfCode = row["SF_Code"].ToString();            
            emp.categorycode = row["Product_Cat_Code"].ToString();            
            emp.Quantity = row["Quantity"].ToString();
            emp.amount = row["order_val"].ToString();            
            emp.netWeight = row["net_weight"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }


    //[WebMethod(EnableSession = true)]
    //public static TotEcTc[] getIssuDataTcEc(string SF_Code, string FYera, string FMonth, string SubDiv)
    //{
    //    string div_code = "";
    //    string sf_Code = "";
    //    try
    //    {
    //        div_code = HttpContext.Current.Session["div_code"].ToString();

    //    }
    //    catch
    //    {
    //        div_code = HttpContext.Current.Session["Division_Code"].ToString();
    //    }

    //    sf_Code = HttpContext.Current.Session["SF_Code"].ToString();

    //    //   SubDiv = "0";

    //    Product pro = new Product();
    //    List<TotEcTc> empList = new List<TotEcTc>();
    //    Expense exp = new Expense();
    //    DataSet dsPro = exp.getrptIssueSlip_MonthTCEC(div_code, SF_Code, FYera, FMonth, SubDiv);
    //    foreach (DataRow row in dsPro.Tables[0].Rows)
    //    {
    //        TotEcTc emp = new TotEcTc();
    //        emp.sfCode = row["SF_Code"].ToString();
    //        emp.TC_Count = row["TC_Count"].ToString();
    //        emp.EC_Count = row["EC_Count"].ToString();
    //        empList.Add(emp);
    //    }
    //    return empList.ToArray();
    //}

    //public class totectc
    //{
    //    public string sfcode { get; set; }
    //    public string tc_count { get; set; }
    //    public string ec_count { get; set; }

    //}

    public class IssueDetails
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string categorycode { get; set; }        
        public string Quantity { get; set; }        
        public string amount { get; set; }
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
        DataSet dsAccessmas = sf.SalesForceList(div_code.TrimEnd(','), SF_Code, SubDiv,"1",stcode);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            FFNames emp = new FFNames();
            emp.sfCode = row["Sf_Code"].ToString();
            emp.sfName = (row["Sf_Name"].ToString());
			emp.sf_emp_id = row["sf_emp_id"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class FFNames
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
		public string sf_emp_id { get; set; }
    }
}