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

public partial class MIS_Reports_Monthly_Attendance : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string sfcode = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string GetDetails(string Div, string Mn, string Yr)
    {
        divcode = Div;
        fdt = Mn;
        tdt = Yr;
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        DataTable dc = new DataTable();
        if (Mn != "" && Yr != "")
        {
            ds = SFD.GetSFMonthly_Attendance(Div.TrimEnd(','), Mn, Yr);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
        else
        {
            return JsonConvert.SerializeObject(dc);
        }
    }
    [WebMethod]
    public static string GetDetails_Headers(string Mn, string Yr)
    {
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        ds = SFD.GetSFMonthly_Attendance_Headers(Mn, Yr);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        SalesForce SFD = new SalesForce();
        
        DataSet dt = new DataSet();
        dt = SFD.GetSFMonthly_Attendance(divcode.TrimEnd(','), fdt, tdt);
        DataTable ds = dt.Tables[0];
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(ds, "Monthly Attendance List");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Monthly_Attendance_List_" + fdt + "_to_" + tdt + ".xlsx");
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