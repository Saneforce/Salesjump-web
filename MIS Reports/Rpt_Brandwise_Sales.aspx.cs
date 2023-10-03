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

public partial class MIS_Reports_Rpt_Brandwise_Sales : System.Web.UI.Page
{
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public static string Brandname = string.Empty;
    public static string BrandCode = string.Empty;
    public string fdate = string.Empty;
    public string tdate = string.Empty;
    public DateTime rdt;
    public DateTime sdt;
    public static DataSet ds;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
        else if (sf_type == "4")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        sfcode = Request.QueryString["sfcode"].ToString();
        sfname = Request.QueryString["sfname"].ToString();
        BrandCode = Request.QueryString["brand"].ToString();
        Brandname = Request.QueryString["brandname"].ToString();
        FDT = Request.QueryString["fdate"].ToString();
        TDT = Request.QueryString["tdate"].ToString();
        subdiv = Request.QueryString["subdiv"].ToString();
        rdt = Convert.ToDateTime(FDT);
        sdt = Convert.ToDateTime(TDT);
    }
    [WebMethod]
    public static string getBrandwiseSales(string Div)
    {
        Product SFD = new Product();
        ds = SFD.getBrandwise_Sales(Div.TrimEnd(','), sfcode, subdiv, FDT, TDT, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(ds.Tables[0], "Brandwise Sales");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Brandwise Sales - " + Brandname + "-" + rdt.ToString().Replace("00:00:00", "") + "_to_" + sdt.ToString().Replace("00:00:00", "") + ".xlsx");
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