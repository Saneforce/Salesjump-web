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

public partial class Stockist_Purchase_Purchase_by_Vendor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod(EnableSession = true)]
    public static string Get_Purchase_vendor_count(string FDT, string TDT)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        //Stockist_Sales ss = new Stockist_Sales();
        //DataSet ds = ss.Bind_Purchase_by_vendor_Count(Stockist_Code, FDT, TDT, Div_Code);
        DataSet ds = Bind_Purchase_by_vendor_Count(Stockist_Code, FDT, TDT, Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet Bind_Purchase_by_vendor_Count(string Stockist_Code, string FDT, string TDT, string Div_Code)
    {
        
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        string strQry = "EXEC sp_Bind_Purchase_by_vendor_Count '" + Stockist_Code + "','" + FDT + "','" + TDT + "','" + Div_Code + "'";
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
