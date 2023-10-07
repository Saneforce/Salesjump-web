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

public partial class MIS_Reports_fieldforcecard : System.Web.UI.Page
{
    public string SFCodes = string.Empty;
    public string mnths = string.Empty;
    public string years = string.Empty;
    public string subDivs = string.Empty;
    public string SfName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        SFCodes = Request.QueryString["sf_code"].ToString();
        mnths = Request.QueryString["Month"].ToString();
        years = Request.QueryString["Year"].ToString();
        subDivs = Request.QueryString["subdiv"].ToString();
        SfName = Request.QueryString["Sf_Name"].ToString();

        hidn_sf_code.Value = SFCodes;
        hFYear.Value =  years;
        hFMonth.Value = mnths;
        subDiv.Value = subDivs;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(mnths)).ToString().Substring(0, 3);
        lblsf_name.Text = SfName + " " + strFMonthName + "-"+ years;
    }
    [WebMethod(EnableSession = true)]
    public static string getSFdets(string divcode, string sfc, string mnth, string year,string subd)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Exec ffscorecard_user '" + divcode + "','" + sfc + "','" + mnth + "','" + year + "','" + subd + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string gettcpob(string divcode, string sfc, string mnth, string year, string subd)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Exec GetffSummarycard_actc '" + sfc + "','" + divcode + "','" + mnth + "','" + year + "','" + subd + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string getnewstor(string divcode, string sfc, string mnth, string year, string subd)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Exec GetffSummarycard_newstorval '" + sfc + "','" + divcode + "','" + mnth + "','" + year + "','" + subd + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string gettlsdcount(string divcode, string sfc, string mnth, string year, string subd)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Exec GetffSummarycard_tlsd '" + sfc + "','" + divcode + "','" + mnth + "','" + year + "','" + subd + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    
}