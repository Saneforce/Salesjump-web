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
using ClosedXML.Excel;
using DBase_EReport;

public partial class Stockist_Sales_Rpt_Sales_By_Salesman_Details : System.Web.UI.Page
{
    public static string Stk_Code = string.Empty;
    public static string Fdt = string.Empty;
    public static string Tdt = string.Empty;
    public static string Stk_Name = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        Stk_Code = Request.QueryString["Stk_Code"].ToString();
        Fdt = Request.QueryString["FDate"].ToString();
        Tdt = Request.QueryString["TDate"].ToString();
        Stk_Name = Request.QueryString["Stk_Name"].ToString();

        DateTime dt = Convert.ToDateTime(Fdt);
        string fd = dt.ToString("dd/MM/yyyy");

        DateTime dt1 = Convert.ToDateTime(Tdt);
        string td = dt1.ToString("dd/MM/yyyy");


        date_details.Text = " From " + fd + " To " + td;
        Tit.Text = Stk_Name + " - Transactions";

    }
    [WebMethod(EnableSession = true)]
    public static string Get_saleman_details()
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        //Stockist_Sales ss = new Stockist_Sales();
        //ds = ss.Bind_Get_saleman_details(Stk_Code, Fdt, Tdt, Div_Code);
        ds = Bind_Get_saleman_details(Stk_Code, Fdt, Tdt, Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet Bind_Get_saleman_details(string Stockist_Code, string FDT, string TDT, string Div_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        string strQry = "EXEC Bind_salebysalesman_details '" + FDT + "','" + TDT + "','" + Div_Code + "','" + Stockist_Code + "'";
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