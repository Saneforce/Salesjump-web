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

public partial class SuperStockist_Sales_CounterSales_SS_Counter_Order_View : System.Web.UI.Page
{
    public static string Order_ID;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Order_No"]))
            {
                Order_ID = Request.QueryString["Order_No"].ToString();
            }
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Get_Counter_Order_Details()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        //ds = sm.Get_Counter_sales_details(Stockist_Code, Div_Code, Order_ID);
        ds = Get_Counter_sales_details(Stockist_Code, Div_Code, Order_ID);
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }

    public static DataSet Get_Counter_sales_details(string stockist_Code, string Div_Code, string Order_ID)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;
        string strQry = "EXEC get_counter_sales_details '" + Order_ID + "','" + Div_Code + "','" + stockist_Code + "'";
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