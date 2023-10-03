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

public partial class SuperStockist_Purchase_Invoice_SS_Good_Received_View : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    public string Order_ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        StockistMaster sm = new StockistMaster();
        Order_ID = Request.QueryString["Trans_Sl_No"].ToString();
        hid_Stockist.Value = Request.QueryString["Stockist_Code"].ToString();
        hid_div.Value = Request.QueryString["Div_Code"].ToString();
    }

    [WebMethod(EnableSession = true)]
    public static string GetPriOrderDetails(string Order_No)
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        //ds = sm.getallgrnorderbystockist(Stockist_Code, Div_Code.TrimEnd(','), Order_No);
        ds = getallgrnorderbystockist(Stockist_Code, Div_Code.TrimEnd(','), Order_No);
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }
    public static DataSet getallgrnorderbystockist(string Stockist_Code, string Div_Code, string Order_No)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        string strQry = "get_grnorder_ordno '" + Order_No + "','" + Stockist_Code + "','" + Div_Code + "'";

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