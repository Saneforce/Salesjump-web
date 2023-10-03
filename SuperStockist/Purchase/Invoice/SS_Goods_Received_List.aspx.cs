using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;
using DBase_EReport;

public partial class SuperStockist_Purchase_Invoice_SS_Goods_Received_List : System.Web.UI.Page
{
    string a = string.Empty;
    string b = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod(EnableSession = true)]
    public static string GetgrnDetails(string Stockist_Code, string FDT, string TDT)
    {
        string StkCode = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        ds = getallgrnorderdetails(StkCode, Div_Code.TrimEnd(','), FDT, TDT);
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }


    public static DataSet getallgrnorderdetails(string Stockist_Code, string Div_Code, string FDT, string TDT)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        try
        {
            string strQry = "exec sp_Get_SS_GRN_details '" + Stockist_Code + "','" + Div_Code + "','" + FDT + "','" + TDT + "'";
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
}