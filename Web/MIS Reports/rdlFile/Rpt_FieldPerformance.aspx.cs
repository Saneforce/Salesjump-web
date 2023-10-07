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

public partial class MIS_Reports_Rpt_FieldPerformance : System.Web.UI.Page
{
    public static string Div = string.Empty;
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public DateTime rdt;
    public DateTime sdt;
	private static string strQry;
    protected void Page_Load(object sender, EventArgs e)
    {
        Div = Session["div_code"].ToString();
        sfcode = Request.QueryString["SF_Code"].ToString();
        FDT = Request.QueryString["Fdate"].ToString();
        TDT = Request.QueryString["Tdate"].ToString();
        subdiv = Request.QueryString["Sub_Div"].ToString();
    }
    [WebMethod]
    public static string getSFdets(string Div)
    {
        Product SFD = new Product();
        DataSet ds = SFD.getDCRUsers(sfcode, Div, FDT, TDT, subdiv);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
     [WebMethod]
    public static string getDayPlan()
    {
        DCR SFD = new DCR();
        if (Div == "29")
        {
            DataSet ds = getDayplans(sfcode, Div, FDT, TDT, subdiv);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
        else
        {
            DataSet ds = SFD.getDayplan(sfcode, Div, FDT, TDT, subdiv);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
        
        //return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getDaywiseCalls()
    {
        DCR SFD = new DCR();
        DataSet ds = SFD.getPerDayCalls(sfcode, Div, FDT, TDT, subdiv);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getRetailerCount()
    {
        DCR SFD = new DCR();
        DataSet ds = SFD.getRetailerCount(sfcode, Div, FDT, TDT, subdiv);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getAttendance()
    {
        DCR SFD = new DCR();
        DataSet ds = SFD.getLoginTimes(sfcode, Div, FDT, TDT, subdiv);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string GetTpDates()
    {
        DCR SFD = new DCR();
        DataSet ds = SFD.getTPDates(FDT, TDT);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getNewRetailerPOB()
    {
        DCR SFD = new DCR();
        DataSet ds = SFD.getNRetailerPOB(sfcode, Div, FDT, TDT, subdiv);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getOrders()
    {
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec getPerDayOrderDetails '" + sfcode + "','" + Div + "','" + FDT + "','" + TDT + "','" + subdiv + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
	  public static DataSet getDayplans(string sfcode, string DivCode, string fdt, string tdt, string subdiv = "0")
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        strQry = "exec getPerDaySumaary_tbday '" + sfcode + "','" + DivCode.TrimEnd(',') + "','" + fdt + "','" + tdt + "','" + subdiv + "'";

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