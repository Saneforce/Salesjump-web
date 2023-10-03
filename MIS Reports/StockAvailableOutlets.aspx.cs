using System;
using System.Web;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using ClosedXML.Excel;
using System.IO;

public partial class MIS_Reports_StockAvailableOutlets : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string sfcode = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    string Division_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    public static DataTable ds_ldets = new DataTable();

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/MasterForAll.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/MasterForAll.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    [WebMethod]
    public static string GetDetails(string Div, string Mn, string Yr)
    {
        sfcode = HttpContext.Current.Session["Sf_Code"].ToString();
        DCR SFD = new DCR();
        ds_ldets = SFD.getDataTable("exec getStockAvailableStores '" + Div.Replace(",", "") + "','"+sfcode+"','" + Mn + "','" + Yr + "'");
        return JsonConvert.SerializeObject(ds_ldets);
    }
    [WebMethod]
    public static string GetSFOutletsDetails(string Div)
    {
        sfcode = HttpContext.Current.Session["Sf_Code"].ToString();
        DCR SFD = new DCR();
        DataTable dtOutlets = SFD.getDataTable("exec getSFwiseOutlets '" + Div.Replace(",", "") + "','"+sfcode+"'");
        return JsonConvert.SerializeObject(dtOutlets);
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        DataTable dtsummary = new DataTable();
        dtsummary = ds_ldets.Copy();
        dtsummary.Columns.Remove("SF_code");
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dtsummary, "Product Availability");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Product Availability.xlsx");
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