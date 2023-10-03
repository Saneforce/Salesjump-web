using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;
using iTextSharp.tool.xml;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.Xml;

public partial class MIS_Reports_view_point_sum_rep : System.Web.UI.Page
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
        DateTime d1 = Convert.ToDateTime(FDate);
        DateTime d2 = Convert.ToDateTime(TDate);
         lblHead.Text = "View Point Summary Report" + " " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
    }
    [WebMethod] 
    public static string getfilldata(string divcode)
    {
        Stockist dv = new Stockist();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec viewpointsummary '" + Sf_Code + "','" + divcode + "','" + FDate + "','" + TDate + "','" + subdiv_code + "'", con); 
         SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);

    }
    [WebMethod] 
    public static string getfillpoint(string divcode)
    {
        Stockist dv = new Stockist();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from Mas_Point_Setup where Point_type_name='Retailer' and Div_id='" + divcode + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);

    }
}