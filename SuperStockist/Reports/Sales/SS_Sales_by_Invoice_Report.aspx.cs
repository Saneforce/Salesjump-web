using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SuperStockist_Reports_Sales_SS_Sales_by_Invoice_Report : System.Web.UI.Page
{
    public static string FDate = string.Empty;
    public static string ToDate = string.Empty;
    public static string Retailer_Code = string.Empty;
    public static string RetailerCode = string.Empty;
    public static string dist_code = string.Empty;
    public static string div_code = string.Empty;
    public static string sf_type = string.Empty;
    public static DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        //dist_code = Session["sf_code"].ToString();
        dist_code = Request.QueryString["stk"].ToString();
        div_code = Session["div_code"].ToString();
        FDate = Request.QueryString["Fdate"].ToString();
        ToDate = Request.QueryString["Tdate"].ToString();
        sf_type = Session["sf_type"].ToString();
        RetailerCode = Request.QueryString["retailer"].ToString();
    }
    [WebMethod]
    public static string Get_sales_data()
    {
        ds = Get_Sales_invoice(FDate, ToDate);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet Get_Sales_invoice(string fdt, string tdt)
    {
        string strQry = "";
        DataSet dsAdmin = new DataSet();
        DB_EReporting dbER = new DB_EReporting();

        strQry = " exec [Get_SS_InvoiceReport] '" + div_code + "','" + dist_code + "','" + fdt + "','" + tdt + "','" + sf_type + "','" + RetailerCode + "'";
        //strQry = " exec GetInvoiceReport '" + div_code + "','" + dist_code + "','" + fdt + "','" + tdt + "','" + sf_type + "','" + RetailerCode + "'";
        try
        {
            dsAdmin = dbER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }
}