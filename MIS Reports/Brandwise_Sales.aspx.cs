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

public partial class MIS_Reports_Brandwise_Sales : System.Web.UI.Page
{
    public static string sf_type = string.Empty;
    public static string sf_code = string.Empty;
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
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
        sf_type = Session["sf_type"].ToString();
        sf_code = HttpContext.Current.Session["Sf_Code"].ToString();
    }
    [WebMethod]
    public static string GetSF(string Div)
    {
        SalesForce sd = new SalesForce();
        DataSet dt = new DataSet();
        dt = sd.SalesForceList_Attendance(Div.TrimEnd(','), "admin");
        return JsonConvert.SerializeObject(dt.Tables[0]);
    }
    [WebMethod]
    public static string getProductGroup(string Div)
    {
        Product sd = new Product();
        DataSet dt = new DataSet();
        dt = sd.get_SNJ_Product_Group(Div.TrimEnd(','));
        return JsonConvert.SerializeObject(dt.Tables[0]);
    }
    [WebMethod]
    public static string getSalesStock(string Div, string SF, string FDt, string TDt, string custcode)
    {
        Product sd = new Product();
        DataSet dt = new DataSet();
        if (custcode != "") {
            dt = sd.get_SNJ_Brandwise_Sales(Div.TrimEnd(','), SF, FDt, TDt, custcode);
        }
        else
        {
            dt = sd.get_SNJ_Brandwise_Sales(Div.TrimEnd(','), SF, FDt, TDt);
        }
        return JsonConvert.SerializeObject(dt.Tables[0]);
    }
    [WebMethod]
    public static string getCust(string Div, string SF, string FDt, string TDt)
    {
        Product sd = new Product();
        DataSet dt = new DataSet();
        dt = sd.get_SNJ_Brandwise_Sales_Cust(Div.TrimEnd(','), SF, FDt, TDt);
        return JsonConvert.SerializeObject(dt.Tables[0]);
    }
    [WebMethod]
    public static string getUOM(string Div)
    {
        Product sd = new Product();
        DataSet dt = new DataSet();
        dt = sd.get_SNJ_Brandwise_Sales_UOM(Div.TrimEnd(','));
        return JsonConvert.SerializeObject(dt.Tables[0]);
    }
}