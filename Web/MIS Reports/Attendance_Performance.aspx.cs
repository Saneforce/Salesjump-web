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

public partial class MIS_Reports_Attendance_Performance : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string sfcode = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    public static string sf_type = string.Empty;
    public static string sf_code = string.Empty;
    public static DataTable ds = new DataTable();

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
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        sf_code = HttpContext.Current.Session["Sf_Code"].ToString();

    }
    [WebMethod]
    public static string GetDetails(string SF, string Div, string Mn, string Yr)
    {
        divcode = Div;
        sfcode = SF;
        fdt = Mn;
        tdt = Yr;
        SalesForce SFD = new SalesForce();
        ds = SFD.GetSFLOGIN_Details_ALL(SF, Div, Mn, Yr);
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod]
    public static string GetSF(string Div)
    {
        SalesForce sd = new SalesForce();
        DataSet dt = new DataSet();
        if (sf_type == "3")
        {
            dt = sd.SalesForceList_Attendance(Div.TrimEnd(','), "admin");
        }
        else
        {
            dt = sd.SalesForceList(Div.TrimEnd(','), sf_code);
        }
        return JsonConvert.SerializeObject(dt.Tables[0]);
    }
    [WebMethod]
    public static string getDivisions(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.Getsubdivisionwise(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string GetHQDetails(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getAllSFHQ(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        SalesForce SFD = new SalesForce();
        DataTable dss = new DataTable();
        dss = ds;
        dss.Columns.Remove("Sf_Code");
        dss.Columns.Remove("Reporting_To");
        dss.Columns.Remove("WrkType");
        dss.Columns.Remove("Ady");
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dss, "Daily Attendance List");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Attendance_Performance_" + fdt + "_to_" + tdt + ".xlsx");
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