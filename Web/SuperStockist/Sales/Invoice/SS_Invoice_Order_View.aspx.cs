using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;

public partial class SuperStockist_Sales_Invoice_SS_Invoice_Order_View : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    public string Order_No;
    public string stat;
    protected void Page_Load(object sender, EventArgs e)
    {
        StockistMaster sm = new StockistMaster();
        Order_No = Request.QueryString["Order_No"].ToString();
        hid_Stockist.Value = Request.QueryString["stockist_Code"].ToString();
        hid_div.Value = Request.QueryString["Div_Code"].ToString();
        stat = Request.QueryString["Status"].ToString();
    }
    [WebMethod(EnableSession = true)]
    public static string GetInvoiceOrderDetails(string Order_No)
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        ds = getallinvoiceorderbystockist(Stockist_Code, Div_Code.TrimEnd(','), Order_No);
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }

    public static DataSet getallinvoiceorderbystockist(string Stockist_Code, string Div_Code, string Order_No)
    {

        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        try
        {
            string strQry = "EXEC sp_getSS_InvoiceOrders '" + Order_No + "','" + Stockist_Code + "','" + Div_Code + "'";
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return ds;
    }
}