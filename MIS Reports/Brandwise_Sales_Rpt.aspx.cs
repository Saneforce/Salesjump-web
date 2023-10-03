using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.Xml;
using System.IO;
using System.Data.OleDb;

public partial class MIS_Reports_Brandwise_Sales_Rpt : System.Web.UI.Page
{
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public static string BrandCode = string.Empty;
    public string fdate = string.Empty;
    public string tdate = string.Empty;
    public DateTime rdt;
    public DateTime sdt;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfcode = Request.QueryString["sfcode"].ToString();
        sfname = Request.QueryString["sfname"].ToString();
        //BrandCode = Request.QueryString["state"].ToString();
        FDT = Request.QueryString["fdate"].ToString();
        TDT = Request.QueryString["tdate"].ToString();
        subdiv = Request.QueryString["subdiv"].ToString();
        rdt = Convert.ToDateTime(FDT);
        sdt = Convert.ToDateTime(TDT);
        Label1.Text = "Brandwise Sales from " + rdt.ToString("dd/MM/yy") + " to " + sdt.ToString("dd/MM/yy");
        Label2.Text = "FieldForce Name: " + sfname;
    }
    [WebMethod]
    public static string getBrandwiseSales(string Div)
    {
        Product SFD = new Product();
        DataSet ds = SFD.getAllBrd_Qty(Div, sfcode, FDT, TDT, subdiv, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getBrandwiseSalesUsr(string Div)
    {
        Product SFD = new Product();
        DataSet ds = SFD.getAllBrd_USR(Div, sfcode, FDT, TDT, subdiv, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getProductBrandlist(string Div)
    {
        Product SFD = new Product();
        DataTable ds = SFD.getProductBrandlist_DataTable(Div);
        return JsonConvert.SerializeObject(ds);
    }
}