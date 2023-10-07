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

public partial class Stockist_Sales_Rpt_Sales_By_Salesman : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod(EnableSession = true)]
    public static string Get_salebysalesman_Count(string FDT, string TDT)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet ds = new DataSet();
        //Stockist_Sales ss = new Stockist_Sales();
        //ds = ss.Bind_salebysalesman_Count(Stockist_Code, FDT, TDT, Div_Code);
        ds = Bind_salebysalesman_Count(Stockist_Code, FDT, TDT, Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static  DataSet Bind_salebysalesman_Count(string Stockist_Code, string FDT, string TDT, string Div_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        string strQry = "EXEC Bind_salebysalesman '" + FDT + "','" + TDT + "','" + Div_Code + "','" + Stockist_Code + "'";
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