using Bus_EReport;
using DocumentFormat.OpenXml.Bibliography;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_rptPrimary_Order_Dtl_RepView : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsSalesForce = null;
    public static string sf_code = string.Empty;
    public static string div_code = string.Empty;
    public static string SubDiv = string.Empty;    
    public static string modes = string.Empty;
    public static string FromDt = string.Empty;
    public static string ToDt = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["SFCode"].ToString();
        SubDiv = Request.QueryString["SubDiv"].ToString();
        FromDt = Request.QueryString["FromDt"].ToString();       
        ToDt = Request.QueryString["ToDt"].ToString();

        DateTime result = DateTime.ParseExact(FromDt, "yyyy-MM-d", CultureInfo.InvariantCulture);
        string dt = result.ToString("dd/MM/yyyy");
        DateTime result1 = DateTime.ParseExact(ToDt, "yyyy-MM-d", CultureInfo.InvariantCulture);
        string dt2 = result1.ToString("dd/MM/yyyy");

        string mtext = "Product Wise Primary Order Report from " + dt + " to " + dt2;

        //lblhead1.Text = "Product Wise Primary Order Report from " + dt + " to " + dt2;

        ddlFieldForce.Value = sf_code;
        txtFromDate.Value = dt.ToString();
        txtToDate.Value = dt2.ToString();
        SubDivCode.Value = SubDiv;
        hdnDate.Value = FromDt;

        Label1.Text = "Field Force :" + Request.QueryString["SFName"].ToString();
    }

    [WebMethod(EnableSession = true)]
    public static GetDatasD[] GetDataD(string SF_Code)
    {
        string div_code = "";
        string sf_Code = "";
        try
        { div_code = HttpContext.Current.Session["div_code"].ToString(); }
        catch
        { div_code = HttpContext.Current.Session["Division_Code"].ToString(); }

        sf_Code = HttpContext.Current.Session["SF_Code"].ToString();

        if (SF_Code == "" || SF_Code == null)
        { SF_Code = sf_Code; }


        List<GetDatasD> empListD = new List<GetDatasD>();
        SalesForce dcn = new SalesForce();
        DataSet dsPro = dcn.SalesForceListMgrGet_MgrOnly(div_code, SF_Code, "0");
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            GetDatasD emp = new GetDatasD();

            emp.Sf_Name = row["Sf_Name"].ToString();
            emp.Sf_Code = row["Sf_Code"].ToString();
            emp.RSF_Code = row["Reporting_To_SF"].ToString();
            emp.Designation = row["Designation"].ToString();

            empListD.Add(emp);
        }
        return empListD.ToArray();
    }

    public class GetDatasD
    {
        public string Sf_Name { get; set; }
        public string Sf_Code { get; set; }
        public string RSF_Code { get; set; }
        public string Designation { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static GetDatas[] GetData(string SF_Code, string Fromdate, string Todate, string SubDivCode)
    {
        string div_code = "";
        string sf_Code = "";
        try
        { div_code = HttpContext.Current.Session["div_code"].ToString(); }
        catch
        { div_code = HttpContext.Current.Session["Division_Code"].ToString(); }

        sf_Code = HttpContext.Current.Session["SF_Code"].ToString();

        if (SF_Code == "" || SF_Code == null)
        { SF_Code = sf_Code; }

        List<GetDatas> empList = new List<GetDatas>();
        DCR_New dcn = new DCR_New();
        DataSet dsPro = dcn.getPrimary_sale_Val(div_code, SF_Code, Fromdate, Todate, "0");
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            GetDatas emp = new GetDatas();
            emp.order_no = row["Order_No"].ToString();
            emp.RSFs = row["Reporting_To_SF"].ToString();
            emp.EmpID = row["sf_emp_id"].ToString();
            emp.User_rank = row["sf_Designation_Short_Name"].ToString();
            emp.sf_hq = row["Sf_HQ"].ToString();
            emp.Dis_Code = row["Stockist_Code"].ToString();
            emp.Dis_Name = row["Stockist_Name"].ToString();
            emp.Region = row["Area"].ToString();
            emp.Zone = row["Zz"].ToString();
            emp.Terr = row["Territory"].ToString();
            emp.sfName = row["Sf_Name"].ToString();
            emp.sfCode = row["Sf_Code"].ToString();
            emp.StateName = row["StateName"].ToString();
            emp.Order_Date = row["Order_Date"].ToString();
            emp.Pro_cat = row["Product_Cat_Name"].ToString();
            emp.Product = row["Product_Detail_Name"].ToString();
            emp.Qty = row["Quantity"].ToString();
            emp.Unit = row["Product_Unit_Name"].ToString();
            emp.Discount = row["discount"].ToString();
            emp.Price = row["Rate"].ToString();
            emp.Sale_Value = row["Order_Value"].ToString();
            emp.Net_Value = row["net_weight"].ToString();
            emp.Remarks = row["Remarks"].ToString();            
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class GetDatas
    {
        public string order_no { get; set; }
        public string RSFs { get; set; }
        public string EmpID { get; set; }
        public string User_rank { get; set; }
        public string sf_hq { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Terr { get; set; }
        public string sfName { get; set; }
        public string sfCode { get; set; }
        public string Dis_Code { get; set; }
        public string Dis_Name { get; set; }
        public string StateName { get; set; }
        public string Order_Date { get; set; }
        public string Pro_cat { get; set; }
        public string Product { get; set; }
        public string Qty { get; set; }
        public string Unit { get; set; }
        public string Discount { get; set; }
        public string Price { get; set; }
        public string Sale_Value { get; set; }
        public string Net_Value { get; set; }
        public string Remarks { get; set; }

    }

}