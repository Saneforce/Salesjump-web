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

public partial class MIS_Reports_Rpt_HQwisePOB : System.Web.UI.Page
{
    public static string sf_type = string.Empty;
    public static string stcode = string.Empty;
    public static string stname = string.Empty;
    public static string subdiv = string.Empty;
    public static string Yr = string.Empty;
    public static string TDT = string.Empty;
    public static string mnthname = string.Empty;
    public static string mnth = string.Empty;
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
        stcode = Request.QueryString["stcode"].ToString();
        stname = Request.QueryString["stname"].ToString();
        mnth = Request.QueryString["mnth"].ToString();
        mnthname = Request.QueryString["mnthname"].ToString();
        Yr = Request.QueryString["ddlyr"].ToString();
        subdiv = Request.QueryString["subdiv"].ToString();
    }
    [WebMethod(EnableSession = true)]
    public static string pobdata(string div_code)
    {
        Product dv = new Product();
        DataSet dsProd = dv.getpobdata(div_code.TrimEnd(','), subdiv, mnth, Yr, stcode);
        ds = dsProd;
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(ds.Tables[0], "HQ Wise POB");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=HQ Wise POB - " + mnthname +" - "+ Yr + ".xlsx");
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