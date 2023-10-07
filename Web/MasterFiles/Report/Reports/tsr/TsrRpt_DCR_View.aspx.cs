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

public partial class MasterFiles_Reports_tsr_TsrRpt_DCR_View : System.Web.UI.Page
{

   
    public DateTime rdt;
    public DateTime sdt;

    DataTable tbl1 = null;
    DataSet dsSalesForce = null;
    DataSet dcrcou = null;
    DataSet dsDCR = null;
    DataSet dsDrr = null;
    DataSet dsTerritory = null;
    DataSet dsTerritory1 = null;
    DataSet dsdoc = null;
    DataSet dsdoc1 = null;
    DataSet dssf = null;
    decimal detorderval = 0;
    decimal detnetval = 0;
    public static string div_code = string.Empty;
    string strDelay = string.Empty;
    public static string sf_code = string.Empty;
    string Sf_Name = string.Empty;
    string strMode = string.Empty;
    string sURL = string.Empty;
    string Sf_HQ = string.Empty;
    public static string Fdate = string.Empty;
    public static string Tdate = string.Empty;
    string stURL = string.Empty;
    int cmonth = -1;
    int cyear = -1;
    int tot_days = -1;
    int cday = 1;
    int iCount = -1;
    int iFieldWrkCount = -1;
    string sDCR = string.Empty;
    Decimal iTotLstCount = 0;
    Decimal iTotLstCountt;
    Decimal Tot_Sec = 0m;
    string dt = string.Empty;
    string dt1 = string.Empty;
    string dtt = string.Empty;
    string dtt1 = string.Empty;
    string sMonth = string.Empty;
    public static string distcode = string.Empty;
    public static string distnm = string.Empty;
    public static string stcode = string.Empty;
    public static string stnm = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        Sf_Name = Request.QueryString["Sf_Name"].ToString();
        cmonth = 0;
        cyear = 0;
        distcode = Request.QueryString["Dst_code"].ToString();
        distnm = Request.QueryString["Dst_name"].ToString();
        stnm = Request.QueryString["st_name"].ToString();
        stcode = Request.QueryString["st_code"].ToString();
        strMode = Request.QueryString["Mode"].ToString();
        strMode = strMode.Trim();
        //sMonth = getMonthName(cmonth) + " - " + cyear.ToString();
        Fdate = Request.QueryString["FDate"].ToString();
        Tdate = (Request.QueryString["TDate"].ToString() == "") ? Fdate : Request.QueryString["TDate"].ToString();       
    }

    [WebMethod]
    public static string getSFdets(string Div)
    {
        Product SFD = new Product();       
        DataSet ds = SFD.getTsrDCRUsers("156", sf_code, Fdate, Tdate, stcode, "0");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string getDayPlan()
    {
        Product SFD = new Product();
        DataSet ds = SFD.getTsrDayplan("156", sf_code, Fdate, Tdate, stcode, "0");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string getDaywiseCalls()
    {
        Product SFD = new Product();
        DataSet ds = SFD.getTsrPerDayCalls("156", sf_code, Fdate, Tdate, stcode, "0");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string getRetailerCount()
    {
        Product SFD = new Product();
        DataSet ds = SFD.getTsrRetailerCount("156");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string getAttendance()
    {
        Product SFD = new Product();
        DataSet ds = SFD.getTsrLoginTimes("156", sf_code, Fdate, Tdate, stcode, "0");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string GetTpDates()
    {
        Product SFD = new Product();
        DataSet ds = SFD.getTsrTPDates(Fdate, Tdate);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string getNewRetailerPOB()
    {
        Product SFD = new Product();
        DataSet ds = SFD.getTsrNRetailerPOB("156", sf_code, Fdate, Tdate, stcode, "0");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string getProfilePic()
    {
        Product SFD = new Product();
        DataSet ds = SFD.getTsrDayProfilePic("156");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }


    [WebMethod]
    public static string getOrders()
    {
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec get_TsrPerDayOrderDetails '" + div_code + "','" + sf_code + "','" + Fdate + "','" + Tdate + "','" + stcode + "','0'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }

   

    public class DynamicTemplateField : ITemplate
    {

        public void InstantiateIn(Control container)
        {
            string divcode = "8";

            SalesForce SF = new SalesForce();
            DataSet ff = new DataSet();
            ff = SF.GetProduct_Name(divcode);
            int cnt = ff.Tables[0].Rows.Count;
            //for (int j = 0; j < cnt; j++)
            //{

            //    //define the control to be added , i take text box as your need
            //    TableCell txt1 = new TableCell();
            //    txt1.ID = "txtquantity_" + j + "row_" + j + "";
            //    container.Controls.Add(txt1);
            //}
        }
    }
    public class DynamicTemplateField1 : ITemplate
    {

        public void InstantiateIn(Control container)
        {
            //string divcode = "8";

            //SalesForce SF = new SalesForce();
            //DataSet ff = new DataSet();
            //ff = SF.GetBrd_Name(divcode);
            //int cnt = ff.Tables[0].Rows.Count;
            //for (int j = 0; j < cnt; j++)
            //{

            //    //define the control to be added , i take text box as your need
            //    TableCell txt1 = new TableCell();
            //    txt1.ID = "txtquantity_" + j + "row_" + j + "";
            //    container.Controls.Add(txt1);
            //}
        }
    }
   
    

 


    
}