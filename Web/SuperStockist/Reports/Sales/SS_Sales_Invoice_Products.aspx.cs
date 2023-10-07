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

public partial class SuperStockist_Reports_Sales_SS_Sales_Invoice_Products : System.Web.UI.Page
{
    public static string order_id = string.Empty;
    public static DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        order_id = Request.QueryString["order_id"].ToString();
        hid_order_id.Value = order_id;
    }
    [WebMethod]
    public static string Get_sales_products(string order_iid)
    {
        string sfCode = HttpContext.Current.Session["Sf_Code"].ToString();
        ds = Get_Sales_invoiceProducts(order_id, sfCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet Get_Sales_invoiceProducts(string order_id, string sfCode)
    {
        string strQry = "";
        DataSet dsAdmin = new DataSet();
        DB_EReporting dbER = new DB_EReporting();

        strQry = "exec GetInvoiceProduct '" + order_id + "','" + sfCode + "'";
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

    [WebMethod(EnableSession = true)]
    public static string Get_Product_Tax()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string state_code = HttpContext.Current.Session["State"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        ds = gets_Product_Tax_details(div_code, Stockist_Code, state_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet gets_Product_Tax_details(string div, string stk, string state)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec get_tax_Details '" + div + "','" + stk + "','" + state + "'";

        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
}