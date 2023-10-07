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

public partial class SuperStockist_Sales_Orders_SS_Myorderview : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    public static string v;
    public static string stk;
    public string status;
    public string sf_type = string.Empty;

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
            //  this.MasterPageFile = "~/Master_DIS.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        StockistMaster sm = new StockistMaster();
        v = Request.QueryString["Order_No"].ToString();
        hid_Stockist.Value = Request.QueryString["Stockist_Code"].ToString();
        stk = Request.QueryString["Stockist_Code"].ToString();

        hid_div.Value = Request.QueryString["Div_Code"].ToString();
        status = Request.QueryString["Status"].ToString();
    }

    [WebMethod(EnableSession = true)]
    public static string GetSecOrderDetails(string Order_No)
    {
        // string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        ds = getallSecorderbystockist(stk, Div_Code.TrimEnd(','), Order_No);
       
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }
    public static DataSet getallSecorderbystockist(string Stockist_Code, string Div_Code, string Order_No)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        string strQry = "sp_getSS_Sec_orderby_ordno '" + Order_No + "','" + Stockist_Code + "','" + Div_Code + "'";

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