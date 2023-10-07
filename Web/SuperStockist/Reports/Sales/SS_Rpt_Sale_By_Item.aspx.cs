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

public partial class SuperStockist_Reports_Sales_SS_Rpt_Sale_By_Item : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    public static string Get_salebyitem_Count(string FDT, string TDT)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet ds = new DataSet();
        //Stockist_Sales ss = new Stockist_Sales();
        //ds = ss.Bind_salebyitem_Count(Stockist_Code, FDT, TDT, Div_Code);
        ds = Bind_salebyitem_Count(Stockist_Code, FDT, TDT, Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet Bind_salebyitem_Count(string Stockist_Code, string FDT, string TDT, string Div_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        string strQry = "EXEC Bind_SS_salebyItem_count '" + FDT + "','" + TDT + "','" + Div_Code + "','" + Stockist_Code + "'";
        //string strQry = "EXEC Bind_salebyItem_count '" + FDT + "','" + TDT + "','" + Div_Code + "','" + Stockist_Code + "'";
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