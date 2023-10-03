using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data.SqlClient;

public partial class MIS_Reports_Rpt_mgrscore_card : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv_code = string.Empty;
    public static string mnths = string.Empty;
    public static string years = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        mnths = Request.QueryString["Month"].ToString();
        years = Request.QueryString["Year"].ToString();

        hidn_sf_code.Value = Sf_Code;
        hFdate.Value = mnths;
        htdate.Value = years;
        subDiv.Value = subdiv_code;

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(mnths)).ToString().Substring(0, 3);
        lblsf_name.Text = sfname + " " + strFMonthName + "-" + years;


    }
    [WebMethod(EnableSession = true)]
    public static string getSFdets(string divcode, string sfc, string fdate, string tdate,string sub)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Exec mgrscorecard_user '" + divcode + "','" + sfc + "','" + fdate + "','" + tdate + "','" + sub + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string gettcpob(string divcode, string sfc, string fdate, string tdate,string sub)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Exec mgrscorecard_tcpc '" + divcode + "','" + sfc + "','" + fdate + "','" + tdate + "','" + sub + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string getnewstor(string divcode, string sfc, string fdate, string tdate,string sub)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Exec mgrcard_newstorval '" + sfc + "','" + divcode + "','" + fdate + "','" + tdate + "','" + sub + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    
    [WebMethod(EnableSession = true)]
    public static string gettlsdcount(string divcode, string sfc, string fdate, string tdate,string sub)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Exec mgrcard_tlsd '" + sfc + "','" + divcode + "','" + fdate + "','" + tdate + "','" + sub + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
}