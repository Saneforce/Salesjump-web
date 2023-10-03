using System;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using ClosedXML.Excel;
using System.IO;

public partial class MIS_Reports_New_Retailers_Report : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string sfcode = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    string Division_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    public static DataSet ds = new DataSet();

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
            this.MasterPageFile = "~/MasterMGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/MasterMR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string GetDetails(string SF, string Div, string Mn, string Yr)
    {
        divcode = Div;
        sfcode = SF;
        fdt = Mn;
        tdt = Yr;
        ListedDR SFD = new ListedDR();
        ds = SFD.getNew_Retailers(SF, Div, Mn, Yr);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetSFDetails(string SF, string Div)
    {
        SalesForce sf = new SalesForce();
        string sd = null;
        DataSet dsf = sf.getusrList_All(Div, sd, SF);
        return JsonConvert.SerializeObject(dsf.Tables[0]);
    }
    [WebMethod]
    public static string GetStkDetails(string SF, string Div)
    {
        ListedDR SFD = new ListedDR();
        ds = SFD.getStockist_Details(SF, Div);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        ListedDR SFD = new ListedDR();
        ds = SFD.getNew_Retailers_Excel(sfcode, divcode, fdt, tdt);
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(ds.Tables[0], "New Retailer Report");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=New Retailers Report" + fdt + "_to_" + tdt + ".xlsx");
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