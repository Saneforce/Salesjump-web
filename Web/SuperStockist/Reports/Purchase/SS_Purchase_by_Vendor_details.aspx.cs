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

public partial class SuperStockist_Reports_Purchase_SS_Purchase_by_Vendor_details : System.Web.UI.Page
{
    public static string Vendor_Id = string.Empty;
    public static string Fdt = string.Empty;
    public static string Tdt = string.Empty;
    public static string Vendor_Name = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        Vendor_Id = Request.QueryString["Vendor_Id"].ToString();
        Fdt = Request.QueryString["FDate"].ToString();
        Tdt = Request.QueryString["TDate"].ToString();
        Vendor_Name = Request.QueryString["Vendor_Name"].ToString();

        DateTime dt = Convert.ToDateTime(Fdt);
        string fd = dt.ToString("dd/MM/yyyy");

        DateTime dt1 = Convert.ToDateTime(Tdt);
        string td = dt1.ToString("dd/MM/yyyy");

        date_details.Text = " From " + fd + " To " + td;
        Tit.Text = Vendor_Name + " - Transactions";
    }

    [WebMethod(EnableSession = true)]
    public static string Get_Purchase_details()
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        // Stockist_Sales ss = new Stockist_Sales();
        //DataSet ds = ss.Bind_Purchase_by_vendor_Details(Stockist_Code, Fdt, Tdt, Div_Code,Vendor_Id);
        DataSet ds = Bind_Purchase_by_vendor_Details(Stockist_Code, Fdt, Tdt, Div_Code, Vendor_Id);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet Bind_Purchase_by_vendor_Details(string Stockist_Code, string FDT, string TDT, string Div_Code, string Vendor_Id)
    {
        DataSet ds = new DataSet();
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "EXEC sp_Bind_Purchase_by_vendor_Details '" + Stockist_Code + "','" + FDT + "','" + TDT + "','" + Div_Code + "','" + Vendor_Id + "'";
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