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

public partial class MIS_Reports_Rpt_distbeatwise_card : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv_code = string.Empty;
    public static string FDate = string.Empty;
    public static string TDate = string.Empty;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        TDate = Request.QueryString["ToDate"].ToString();
        FDate = Request.QueryString["FromDate"].ToString();

        hidn_sf_code.Value = Sf_Code;
        hFdate.Value = FDate;
        htdate.Value = TDate;
        subDiv.Value = subdiv_code;

        DateTime d1 = Convert.ToDateTime(FDate);
        DateTime d2 = Convert.ToDateTime(TDate);
        lblsf_name.Text = sfname + " "+ d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
        
    }
    [WebMethod(EnableSession = true)]
    public static string getSFdets(string divcode, string sfc, string fdate, string tdate,string sub)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec disscorecard_user '" + divcode + "','" + sfc + "','" + fdate + "','" + tdate + "','" + sub + "'", con);
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
        SqlCommand cmd = new SqlCommand("exec distcard_actc '" + sfc + "','" + divcode + "','" + fdate + "','" + tdate + "','" + sub + "'", con);
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
        SqlCommand cmd = new SqlCommand("exec distcard_newstorval '" + sfc + "','" + divcode + "','" + fdate + "','" + tdate + "','" + sub + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);

    }
    [WebMethod(EnableSession = true)]
    public static string getSFroute(string divcode, string sfc, string fdate, string tdate,string sub)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec distcard_beatnm '" + sfc + "','" + divcode + "','" + fdate + "','" + tdate + "','" + sub + "'", con);
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
        SqlCommand cmd = new SqlCommand("Exec distcard_tlsd '" + sfc + "','" + divcode + "','" + fdate + "','" + tdate + "','" + sub + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
}