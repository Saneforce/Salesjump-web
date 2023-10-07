using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;
using ClosedXML.Excel;
using System.IO;

public partial class rpt_Retailer_Score_Card : System.Web.UI.Page
{
    public static DataSet ds = new DataSet();
    public static string sf_code = string.Empty;
    public static string Year;
    public static string Month;
    public static string SfCode;
    public static string SfName;
    public static string Sub_Div;
    public static string sf_type = string.Empty;
    string strFMonthName = string.Empty;
    int FYear = 0;
    int FMonth = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        Year = Request.QueryString["Year"].ToString();
        Month = Request.QueryString["Month"].ToString();
        SfCode = Request.QueryString["SF_Code"].ToString();
        SfName = Request.QueryString["SF_Name"].ToString();
        Sub_Div = Request.QueryString["Sub_Div"].ToString();
        FMonth = Convert.ToInt32(Month);
        FYear = Convert.ToInt32(Year);
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString();
        lblHead.Text = "Retailer Score Card Detail for the Period of " + strFMonthName + " " + Year + " ";
    }

    [WebMethod(EnableSession = true)]
    public static string Retailer_Score_Card()
    {
        DataSet ds = new DataSet();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        ds = getDataSet("EXEC Sp_Retailer_Score_Card '" + div_code + "','" + SfCode + "','" + Month + "','" + Year + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Retailer_Score_Card_Visit()
    {
        DataSet ds = new DataSet();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        ds = getDataSet("EXEC Sp_Retailer_Score_Card_Visti '" + div_code + "','" + SfCode + "','" + Month + "','" + Year + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet getDataSet(string qrystring)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsSF = null;
        string strQry = qrystring;

        try
        {
            dsSF = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {

        DataTable dsProd1 = null;
        DataSet ds = new DataSet();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        ds = getDataSet("EXEC Sp_Retailer_Score_Card '" + div_code + "','" + SfCode + "','" + Month + "','" + Year + "'");
        dsProd1 = ds.Tables[0];
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dsProd1, "Retailer Score Card");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=RetailerScoreCard.xlsx");
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