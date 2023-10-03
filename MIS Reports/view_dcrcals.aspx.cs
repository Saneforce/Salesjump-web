using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Net;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

public partial class MIS_Reports_view_dcrcals : System.Web.UI.Page
{
    public string sfn = string.Empty;
    public string subd = string.Empty;
    public string div = string.Empty;
    public string sf_code = string.Empty;
    public string fdt = string.Empty;
    public string tdt = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfn = Request.QueryString["Sf_Name"].ToString();
        subd = Request.QueryString["subdiv"].ToString();
        div = Request.QueryString["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        fdt = Request.QueryString["FromDate"].ToString();
        tdt = Request.QueryString["ToDate"].ToString();
        DateTime d1 = Convert.ToDateTime(fdt);
        DateTime d2 = Convert.ToDateTime(tdt);
        lblsf_name.Text = Convert.ToString(sfn) + "  " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
        hidn_sf_code.Value = sf_code;
        divcd.Value = div;
        fdat.Value = fdt;
        tdat.Value = tdt;
    }
    [WebMethod(EnableSession = true)]
    public static string fillchannel(string div, string sf_code, string fdt, string tdt)
    {
        //SalesForce sf = new SalesForce();
        //DataSet dsProd = sf.Get_channel_value(div, sf_code, fdt, tdt);
        //return JsonConvert.SerializeObject(dsProd.Tables[0]);

        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec get_dcr_channel '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string fillcall(string div, string sf_code, string fdt, string tdt)
    {
        //SalesForce sf = new SalesForce();
        //DataSet dsProd = sf.Get_call_value(div, sf_code, fdt, tdt);
        //return JsonConvert.SerializeObject(dsProd.Tables[0]);

        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec get_dcr_call '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string fillretailer(string div, string sf_code, string fdt, string tdt)
    {
        //SalesForce sf = new SalesForce();
        //DataSet dsProd = sf.Get_dcr_value_retailer(div, sf_code, fdt, tdt);
        //return JsonConvert.SerializeObject(dsProd.Tables[0]);

        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec get_Trans_Dcr_Retailers_Values '" + div + "','" + sf_code + "','" + fdt + "','" + tdt + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
}