using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using DocumentFormat.OpenXml.Drawing;
using System.IO;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Services;


public partial class MIS_Reports_monthlymaxattndce : System.Web.UI.Page
{
    public static string sfCode = string.Empty;
    public static string sfname = string.Empty;
    public static string divcode = string.Empty;
    public static string sf_type = string.Empty;
    public static string FMonth = string.Empty;
    public static string FYear = string.Empty;
    public static string type = string.Empty;
    DataSet ff = new DataSet();
    DataSet ss = new DataSet();
    DataSet dsSalesForce = new DataSet();
    DataSet dsdatee = new DataSet();
    DataSet dsDoc = null;
    DataSet dsGV = null;
    DateTime dtCurrent;
    int gridcnt = 0;
    int rowspan = 0;
    string sCurrentDate = string.Empty;
    string endTime = string.Empty;
    string startTime = string.Empty;
    string tot_dr = string.Empty;
    string tot_value = string.Empty;
    string con_qty = string.Empty;
    string ec = string.Empty;
    string Monthsub = string.Empty;
    string date = string.Empty;
    string endd = string.Empty;
    DataSet dsDoctor = new DataSet();
    string gg = string.Empty;
    string imagepath = string.Empty;
    public static string subdiv_code = string.Empty;
    int quantity2 = 0;
    string mode = string.Empty;
    public void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfCode"].ToString();
        sfname = Request.QueryString["sfname"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        logoo.ImageUrl = imagepath;
        if (sfCode.Contains("MGR"))
        {
            sf_type = "2";
        }
        else if (sfCode.Contains("MR"))
        {
            sf_type = "1";
        }
        else
        {
            sf_type = "0";
        }
        type = Request.QueryString["type"].ToString();
        hdnDiv.Value = divcode.TrimEnd(',');
        hdnYear.Value = FYear;
        hdnMonth.Value = FMonth;
        htype.Value = type;
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        lblHead.Text = "Attendance View for the Month of " + strFMonthName + " " + FYear;
        lblsf_name.Text = sfname;
    }
    [WebMethod(EnableSession = true)]
    public static string Filluser()
    {
        SalesForce dv = new SalesForce();
        //DataSet dsProd = dv.getmaxatnce_result(divcode, sfCode, FMonth, FYear, subdiv_code);
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec attendancemaximisedcfinal_dtwisedata '" + divcode + "','" + sfCode + "','" + FMonth + "','" + FYear + "','" + subdiv_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string Filldetail()
    {
        SalesForce dv = new SalesForce();
        //DataSet dsProd = dv.getmaxatnce_result(divcode, sfCode, FMonth, FYear, subdiv_code);
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Exec attendancemaximisedcfinal_emp '" + divcode + "','" + sfCode + "','" + FMonth + "','" + FYear + "','" + subdiv_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string Filldate()
    {
        SalesForce dv = new SalesForce();
        // DataSet dsProd = dv.maxattenview_Dates(FYear, FMonth);
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec getQuizDates '" + FYear + "','" + FMonth + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string Fillprod()
    {
        SalesForce dv = new SalesForce();
        //DataSet dsProd = dv.getmaxatnce_prods(divcode, sfCode, FMonth, FYear, subdiv_code);
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Exec attendancemaximisedcfinal_proddtl '" + divcode + "','" + sfCode + "','" + FMonth + "','" + FYear + "','" + subdiv_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
}