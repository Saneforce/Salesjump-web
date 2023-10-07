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
using DBase_EReport;

public partial class MasterFiles_Reports_DCR : System.Web.UI.Page
{
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv = string.Empty;
    public static string FDTs = string.Empty;
    public static string TDTs = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public static string BrandCode = string.Empty;
    public string fdate = string.Empty;
    public string tdate = string.Empty;
    public string type = string.Empty;
    public DateTime rdt;
    public DateTime sdt;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfcode = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        FDTs = Request.QueryString["fdate"].ToString();
        TDTs = Request.QueryString["tdate"].ToString();
        type = Request.QueryString["typemod"].ToString();

        DateTime result4 = DateTime.ParseExact(FDTs, "d/MM/yyyy", CultureInfo.InvariantCulture);
        FDT = result4.ToString("yyyy-MM-dd");

        DateTime result10 = DateTime.ParseExact(TDTs, "d/MM/yyyy", CultureInfo.InvariantCulture);
        TDT = result10.ToString("yyyy-MM-dd");
        rdt = Convert.ToDateTime(FDTs);
        sdt = Convert.ToDateTime(TDTs);

        Label1.Text = "DCR Detailed View from " + rdt.ToString("dd/MM/yy") + " to " + sdt.ToString("dd/MM/yy");
        Label2.Text = "";
    }
    [WebMethod]
    public static string getSFdets(string Div)
    {
        Product SFD = new Product();
        DataSet ds = SFD.getDCRUsers(sfcode, Div, FDT, TDT, subdiv);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getDaywisePlan(string Div)
    {
        Product SFD = new Product();
        DataSet ds = SFD.getDaywiseDCRDets(sfcode, Div, FDT, TDT, subdiv);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getDaywiseOrder(string Div)
    {
        Product SFD = new Product();
        DataSet ds = SFD.getDaywiseDCROrders(sfcode, Div, FDT, TDT, subdiv);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }


    [WebMethod]
    public static string getmgrdets(string Div)
    {
        Product SFD = new Product();
        DataSet ds = getDCRUsers(sfcode, Div, FDT, TDT, subdiv);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getDCRUsers(string sfcode, string DivCode, string fdt, string tdt, string subdiv = "0")
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "exec getmgrUsers '" + sfcode + "'," + DivCode.TrimEnd(',') + ",'" + fdt + "','" + tdt + "','" + subdiv + "'";

        try
        {
            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }
    [WebMethod]
    public static string getmgrDaywisePlan(string Div)
    {
        Product SFD = new Product();
        DataSet ds = getDaywiseDCRDets(sfcode, Div, FDT, TDT, subdiv);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getDaywiseDCRDets(string sfcode, string DivCode, string fdt, string tdt, string subdiv = "0")
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "exec getmgrDaywiseDCR '" + sfcode + "','" + DivCode.TrimEnd(',') + "','" + fdt + "','" + tdt + "','" + subdiv + "'";

        try
        {

            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }
    [WebMethod]
    public static string getmgrDaywiseOrder(string Div)
    {
        Product SFD = new Product();
        DataSet ds = getDaywiseDCROrders(sfcode, Div, FDT, TDT, subdiv);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getDaywiseDCROrders(string sfcode, string DivCode, string fdt, string tdt, string subdiv = "0")
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "exec getmgrDaywiseDCROrders '" + sfcode + "','" + DivCode.TrimEnd(',') + "','" + fdt + "','" + tdt + "','" + subdiv + "'";

        try
        {
            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }
}