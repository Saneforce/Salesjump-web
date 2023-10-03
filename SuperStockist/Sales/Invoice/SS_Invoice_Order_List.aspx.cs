using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Globalization;
using Newtonsoft.Json;
using DBase_EReport;

public partial class SuperStockist_Sales_Invoice_SS_Invoice_Order_List : System.Web.UI.Page
{
    public static DataSet dsListedDR = null;
    string sUSR = string.Empty;
    string stit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string name = Session["Title_DIS"].ToString();
        string namess = Session["UserName"].ToString();
        sUSR = Request.Url.Host.ToLower().Replace("www.", "").Replace(".sanfmcg.com", "").Replace(".salesjump.in", "").ToLower();
        HiddenField1.Value = sUSR;
    }

    [WebMethod(EnableSession = true)]
    public static string GetInvoiceDetails(string FDT, string TDT)
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        ds = sm.getinvoicedetails(Stockist_Code, FDT, TDT);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string GetDistributor(string Order_Id, string Stockist, string Division, string Cust_code)
    {
        //ListedDR lstDR = new ListedDR();
        dsListedDR = GetDistributr(Division, Stockist, Cust_code, Order_Id);
        return JsonConvert.SerializeObject(dsListedDR.Tables[0]);
    }

    [WebMethod]
    public static string GetProductdetails(string Order_Id)
    {
        //ListedDR lstDR = new ListedDR();
        dsListedDR = GetProducts(Order_Id);
        return JsonConvert.SerializeObject(dsListedDR.Tables[0]);
    }
    public static DataSet GetDistributr(string div_code, string stockist_code, string cust_code, string order_id)
    {
        string strQry = "";
        DataSet dsAdmin = new DataSet();
        DB_EReporting dbER = new DB_EReporting();

        strQry = " exec GetSS_InvoiceDistRet '" + div_code + "','" + stockist_code + "','" + cust_code + "','" + order_id + "'";
       // strQry = " exec GetInvoiceDistRet '" + div_code + "','" + stockist_code + "','" + cust_code + "','" + order_id + "'";
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
    public static DataSet GetProducts(string order_id)
    {
        string strQry = "";
        DataSet dsAdmin = new DataSet();
        DB_EReporting dbER = new DB_EReporting();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        //strQry = " exec GetInvoiceProduct '" + order_id + "'";
        strQry = "exec GetSS_Product_Invoice'" + order_id + "','" + Stockist_Code + "'";
        //strQry = "exec GetProduct_Invoice'" + order_id + "','" + Stockist_Code + "'";
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
    [WebMethod]
    public static string GetDivision(string divcode)
    {
        //ListedDR lstDR = new ListedDR();
        dsListedDR = GetDivis(divcode);
        return JsonConvert.SerializeObject(dsListedDR.Tables[0]);
    }
    public static DataSet GetDivis(string divcode)
    {
        string strQry = "";
        DataSet dsAdmin = new DataSet();
        DB_EReporting dbER = new DB_EReporting();

        strQry = " Select Division_Code,Division_Name  from mas_Division where Division_code='" + divcode + "'";
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