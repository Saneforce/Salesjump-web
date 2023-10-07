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


public partial class MIS_Reports_Rpt_OrderVSales : System.Web.UI.Page
{
    public static string fdt;
    public static string tdt;
    public static string sfcode;
    public static string sfname;
    public static string divcode;
    public static string subdiv;
    public static DataTable dtt = new DataTable();
    public static string statecode;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfcode = Request.QueryString["sf"].ToString();
        sfname = Request.QueryString["sfnm"].ToString();
        divcode = Session["div_code"].ToString();
        fdt = Request.QueryString["fdt"].ToString();
        tdt = Request.QueryString["tdt"].ToString();
        subdiv = Request.QueryString["subdiv"].ToString();
        statecode = Request.QueryString["state"].ToString();
    }
    [WebMethod]
    public static string getDetails()
    {
		dtt = new DataTable();
        string strQry = string.Empty;
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec getBillUploadDets '" + sfcode + "','" + divcode + "','" + fdt + "','" + tdt + "','0','0'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtt);
        con.Close();
        return JsonConvert.SerializeObject(dtt);
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dtt, "Invoice Upload Report");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Invoice_Upload_Report" + fdt + "_to_" + tdt + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
}