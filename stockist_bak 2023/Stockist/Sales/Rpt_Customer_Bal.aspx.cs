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

public partial class Stockist_Sales_Rpt_Customer_Bal : System.Web.UI.Page
{
     static string strQry = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    public static string Get_Cust_bal(string From_Year, string To_Year,string FM,string TM,string Type)
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        //Stockist_Sales ss = new Stockist_Sales();
        //ds = ss.Bind_cust_Bal(Stockist_Code, Div_Code, From_Year, To_Year, FM, TM, Type);
        ds = Bind_cust_Bal(Stockist_Code, Div_Code, From_Year, To_Year, FM, TM, Type);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Bind_cust_Bal(string Stockist_Code, string Div_Code, string From_Year, string To_Year, string from_mth, string to_mnth, string Type)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        strQry = "EXEC sp_bind_cust_bal '" + Stockist_Code + "','" + Div_Code + "','" + From_Year + "','" + To_Year + "','" + from_mth + "','" + to_mnth + "','" + Type + "'";
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
    //[WebMethod(EnableSession = true)]
    //public static string Get_Calender_Year()
    //{
    //    string Div_Code = HttpContext.Current.Session["div_code"].ToString();
    //    DataSet ds = new DataSet();
    //    TourPlan tp = new TourPlan();
    //    ds = tp.Get__company_wise_Year(Div_Code);
    //    for (int k = Convert.ToInt16(ds.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year; k++)
    //    {
    //        DataRow dr = ds.Tables[0].NewRow();
    //        if (ds.Tables[0].Rows[0]["Year"].ToString() != k.ToString())
    //        {
    //            dr["Year"] = k.ToString();
    //            ds.Tables[0].Rows.Add(dr);
    //        }
    //    }
    //    return JsonConvert.SerializeObject(ds.Tables[0]);
    //}

    [WebMethod(EnableSession = true)]
    public static string Get_Year_Data(string FM,string TM,string Type)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        //Stockist_Sales ss = new Stockist_Sales();
        //ds = ss.Get_year_details_Data(Div_Code,FM,TM,Type);
        ds = Get_year_details_Data(Div_Code, FM, TM, Type);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Get_year_details_Data(string Div_Code, string fm, string tm, string type)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        strQry = "EXEC get_financial_year '" + Div_Code + "','" + type + "','" + fm + "','" + tm + "'";
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