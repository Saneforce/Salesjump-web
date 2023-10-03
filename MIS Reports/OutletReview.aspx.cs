using System;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;
using ClosedXML.Excel;
using System.IO;
using DBase_EReport;
using System.Web.UI;

public partial class MIS_Reports_OutletReview : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string sfcode = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    string Division_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    public static DataTable ds = new DataTable();
    public static DataTable outletDT = new DataTable();

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
        if (!Page.IsPostBack)
        {
            ds = new DataTable();
            outletDT = new DataTable();
        }
    }
    [WebMethod]
    public static string GetDetails(string SF, string Div, string Mn, string Yr)
    {
        divcode = Div;
        sfcode = SF;
        fdt = Mn;
        tdt = Yr;
        ds = new DataTable();
        ds = getsfcc("exec getOutletReviewSummary '" + SF + "'," + Div + ",'" + Mn + "','" + Yr + "'");
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod]
    public static string GetOutletDetails(string SF, string dt, string calltyp)
    {
        outletDT = new DataTable();
        outletDT = getsfcc("exec getReviewOutletDetails '" + SF + "','" + dt + "','" + calltyp + "'");
        return JsonConvert.SerializeObject(outletDT);
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(ds, "Outelet Review");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Outelet Review Report" + fdt + "_to_" + tdt + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string mgrhqname = mgrhqn.Value.ToString();
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(outletDT, "Leave Details");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=OutletDetails_Details for " + mgrhqname + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    public static DataTable getsfcc(string qrystring)
    {

        DB_EReporting db_ER = new DB_EReporting();

        DataTable dsSF = null;

        string strQry = qrystring;

        try
        {
            dsSF = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }
}