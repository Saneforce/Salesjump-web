using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using Bus_EReport;
using System.Globalization;

public partial class MIS_Reports_rptSalesReturnNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "Field Force Name : " + Request.QueryString["Sf_Name"].ToString();
        
        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        string strMonthName = mfi.GetAbbreviatedMonthName( Convert.ToInt16(Request.QueryString["cur_month"].ToString()));
        hsfcode.Value = Request.QueryString["sf_code"].ToString();
        hfyear.Value = Request.QueryString["cur_year"].ToString();
        hfmonth.Value = Request.QueryString["cur_month"].ToString();
        Label2.Text = "Sales Return Details of " + strMonthName + " - " + Request.QueryString["cur_year"].ToString();
    }
    public class productClass
    {
        public string pCode { get; set; }
        public string pName { get; set; }
    }

    [WebMethod]
    public static List<productClass> getProducts()
    {
        List<productClass> Lists = new List<productClass>();
        DataSet ds = new DataSet();
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        SalesForce SF = new SalesForce();
        ds = SF.GetProduct_Name(div_code);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            productClass list = new productClass();
            list.pCode = row["Product_Detail_Code"].ToString();
            list.pName = row["Product_Detail_Name"].ToString();
            Lists.Add(list);
        }
        return Lists.ToList();
    }
    public class HeaderList
    {
        public string RtlCode { get; set; }
        public string DisCode { get; set; }
        public string ProCode { get; set; }
        public string RouteName { get; set; }
        public string RtlName { get; set; }
        public string DisName { get; set; }
        public string CaseQty { get; set; }
        public string PecQty { get; set; }
        public string ReturnDate { get; set; }
    }

    [WebMethod]
    public static List<HeaderList> getReturnHeaderVal(string SFCode, string FYear, string FMonth)
    {
        List<HeaderList> Lists = new List<HeaderList>();
        DataSet ds = new DataSet();
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        Sales SF = new Sales();
        ds = SF.GetSalesReturnHeader(SFCode, FYear, FMonth);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            HeaderList list = new HeaderList();
            list.RtlCode = row["Dr_Code"].ToString();
            list.RtlName = row["ListedDr_Name"].ToString();
            list.RouteName = row["Territory_Name"].ToString();
            list.DisCode = row["Stk_Code"].ToString();
            list.DisName = row["Stockist_Name"].ToString();
            list.ProCode = row["ProdCode"].ToString();
            list.ReturnDate =row["RetDate"] == DBNull.Value ? "" : Convert.ToDateTime(row["RetDate"]).ToString("dd/MM/yyyy");
            list.CaseQty = row["CQTY"].ToString();
            list.PecQty = row["PQTY"].ToString();
            Lists.Add(list);
        }
        return Lists.ToList();
    }
    public class DetailsList
    {
        public string proName { get; set; }
        public string typeName { get; set; }
        public string batchNum { get; set; }
        public string batchDate { get; set; }
        public string remarks { get; set; }
        public string CaseQty { get; set; }
        public string PecQty { get; set; }
        public string orderNum { get; set; }

    }

    [WebMethod]
    public static List<DetailsList> getReturnDetailsVal(string rtCode, string distCode, string cDate)
    {
        List<DetailsList> Lists = new List<DetailsList>();
        DataSet ds = new DataSet();
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        DateTime dt = DateTime.ParseExact(cDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        Sales SF = new Sales();
        ds = SF.GetSalesReturnDetails(rtCode, distCode, dt.ToString("yyyy/MM/dd"));
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            DetailsList list = new DetailsList();
            list.proName = row["Product_Detail_Name"].ToString();
            list.typeName = row["name"].ToString();
            list.batchNum = row["damagebatch"].ToString();
            list.batchDate = row["damageorderdate"].ToString();
            list.remarks = row["Rem"].ToString();
            list.CaseQty = row["CQTY"].ToString();
            list.PecQty = row["PQTY"].ToString();
            list.orderNum = row["Trans_SlNo"].ToString();
            Lists.Add(list);
        }
        return Lists.ToList();
    }
}