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

public partial class MIS_Reports_rpt_skisalesanalysis : System.Web.UI.Page
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
        Sf_Code = Request.QueryString["SF_Code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        subdiv_code = Request.QueryString["subDiv"].ToString();
        mnths = Request.QueryString["FMonth"].ToString();
        years = Request.QueryString["FYear"].ToString();

        hidn_sf_code.Value = Sf_Code;
        hFdate.Value = mnths;
        htdate.Value = years;
        subDiv.Value = subdiv_code;

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(mnths)).ToString().Substring(0, 3);
        lblsf_name.Text = sfname + " " + strFMonthName + "-" + years;
    }
    [WebMethod(EnableSession = true)]
    public static string getSFdets(string divcode, string sfc, string mnth, string yr, string sdiv)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Exec skuwisesalesdtl '" + divcode + "','" + sfc + "','" + mnth + "','" + yr + "','" + sdiv + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
}