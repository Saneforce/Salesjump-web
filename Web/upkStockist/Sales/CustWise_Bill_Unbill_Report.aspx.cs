using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Bus_EReport;
using System.Data;
using System.Web.Services;
using DBase_EReport;

public partial class Stockist_Sales_CustWise_Bill_Unbill_Report : System.Web.UI.Page
{
    public static DataSet ds = new DataSet();
    //public static Stockist_Sales ss = new Stockist_Sales();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string Get_CustWise_billed(string From_Date, string To_Date)
    {
        string StkCode = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string sf_type = HttpContext.Current.Session["sf_type"].ToString();
        //ds = ss.Get_CustWise_billed(StkCode, Div_Code, From_Date, To_Date, sf_type);
        ds = Get_CustWise_billed(StkCode, Div_Code, From_Date, To_Date, sf_type);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Get_CustWise_billed(string stk, string div, string fromdate, string todate, string sf_type)
    {
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "EXEC sp_CustWise_billed_unbilled '" + stk + "','" + div + "','" + fromdate + "','" + todate + "','" + sf_type + "'";
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
    [WebMethod]
    public static string Get_CustWise_billed_Details(string From_Date, string To_Date,string Type)
    {
        string StkCode = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        //ds = ss.Get_CustWise_billed_Details(StkCode, Div_Code, From_Date, To_Date,Type);
        ds = Get_CustWise_billed_Details(StkCode, Div_Code, From_Date, To_Date, Type);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Get_CustWise_billed_Details(string stk, string div, string fromdate, string todate, string Type)
    {
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "EXEC sp_CustWise_billed_unbilled_Details '" + stk + "','" + div + "','" + fromdate + "','" + todate + "','" + Type + "'";
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