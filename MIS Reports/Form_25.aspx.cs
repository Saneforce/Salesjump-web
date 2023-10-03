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

public partial class MIS_Reports_Form_25 : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string sfcode = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    public static DataSet dss = new DataSet();
	string sf_type = string.Empty;
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
        if (!IsPostBack)
        {
            hffilter.Value = "AllFF";
            hfilter.Value = "All";
            hsfhq.Value = "All";
        }

    }
    [WebMethod]
    public static string GetDetails(string SF, string Div, string Mn, string Yr)
    {
        sfcode = SF;
        divcode = Div.TrimEnd(',');
        fdt = Mn;
        tdt = Yr;
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        DataTable dc = new DataTable();
        if (Mn != "" && Yr != "")
        {
            ds = SFD.GetMonthly_AttendanceSFDets(SF, Div.TrimEnd(','), Mn, Yr);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
        else
        {
            return JsonConvert.SerializeObject(dc);
        }
    }
    [WebMethod]
    public static string GetMonthlyAttendanceLeave(string SF, string Div, string Mn, string Yr)
    {
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        DataTable dc = new DataTable();
        if (Mn != "" && Yr != "")
        {
            ds = SFD.GetMonthlyLeaveAttendance(SF, Div.TrimEnd(','), Mn, Yr);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
        else
        {
            return JsonConvert.SerializeObject(dc);
        }
    }
    [WebMethod]
    public static string GetMonthlyAttendance(string SF, string Div, string Mn, string Yr)
    {
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        DataTable dc = new DataTable();
        if (Mn != "" && Yr != "")
        {
            ds = SFD.GetMonthlyAttendanceDets(SF, Div.TrimEnd(','), Mn, Yr);
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
        dss = SFD.GetSFMonthly_Attendance_Headers(Mn, Yr);
        return JsonConvert.SerializeObject(dss.Tables[0]);
    }
    [WebMethod]
    public static string getDepts(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getAllSF_Dept(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string getDivisions(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.Getsubdivisionwise(divcode.TrimEnd(','));
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string GetHQDetails(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getAllSFHQ(divcode.TrimEnd(','));
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        SalesForce SFD = new SalesForce();

        DataSet dt = new DataSet();
        dt = SFD.GetSFMonthly_Attendance(sfcode, divcode.TrimEnd(','), fdt, tdt);
        DataTable ds = dt.Tables[0];
        string months = dss.Tables[0].Rows[0][0].ToString();
        string[] mon = months.Split(',');
        ds.Columns.Add("Total Present Days", typeof(string));
        ds.Columns.Add("Absent", typeof(string));
        ds.Columns.Add("Leave", typeof(string));
        ds.Columns.Add("LOP", typeof(string));
        ds.Columns.Add("Total Days", typeof(string));
        for (int j = 0; j < ds.Rows.Count; j++)
        {
            double pres = 0;
            double abs = 0;
            double lev = 0;
            double lop = 0;
            for (int i = 0; i < mon.Length; i++)
            {
                var cellValue = ds.Rows[j][mon[i]].ToString();
                if (cellValue == "P" ||cellValue == "OD") 
                {
                    pres = pres + 1;
                }
                else if (cellValue == "HA") {
                    pres = pres + 0.5;
                    abs = abs + 0.5;
                }
                else if (cellValue == "/HAL"|| cellValue == "A/HAL"|| cellValue == "HA/HAL"|| cellValue == "P/HAL"|| cellValue == "OD/HAL") {
                    lev = lev + 0.5;
                    pres = pres + 0.5;
                }
                else if (cellValue == "/L" || cellValue == "A/L" || cellValue == "HA/L" || cellValue == "P/L"|| cellValue == "OD/L") {
                    lev = lev + 1;
                }
                else if (cellValue == "/LOP" || cellValue == "A/LOP" || cellValue == "HA/LOP" || cellValue == "P/LOP" || cellValue == "OD/LOP") {
                    lop = lev + 1;
                }
                else if (cellValue == "A") {
                    abs = abs + 1;
            }
            }
            ds.Rows[j]["Total Present Days"] = pres.ToString();
            ds.Rows[j]["Absent"] = abs.ToString();
            ds.Rows[j]["Leave"] = lev.ToString();
            ds.Rows[j]["LOP"] = lop.ToString();
            ds.Rows[j]["Total Days"] = (pres + abs).ToString();
        }
        DataTable dtfilter = ds;
        DataView dataView = dtfilter.DefaultView;
        //if (hfilter.Value != null && hfilter.Value != "" && hfilter.Value != "All")
        //{
        //    dataView.RowFilter = "Status = '" + hfilter.Value + "'";
        //    dtfilter = dataView.ToTable();
        //    dataView = dtfilter.DefaultView;
        //}
        if (hffilter.Value != null && hffilter.Value != "" && hffilter.Value != "AllFF")
        {
            dataView.RowFilter = "Approved_By = '" + hffilter.Value + "'";
            dtfilter = dataView.ToTable();
            dataView = dtfilter.DefaultView;
        }
        //if (hsfhq.Value != null && hsfhq.Value != "" && hsfhq.Value != "All")
        //{
        //    dataView.RowFilter = "HQ_Name = '" + hsfhq.Value + "'";
        //    dtfilter = dataView.ToTable();
        //    dataView = dtfilter.DefaultView;
        //}
        dtfilter.Columns.Remove("Approved_By");
        dtfilter.Columns.Remove("SF_Code");
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dtfilter, "Monthly Attendance List");
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