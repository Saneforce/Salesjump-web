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
using System.Transactions;

public partial class MIS_Reports_Monthly_Attendance_Report : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string sfCode = string.Empty;
    public static string sfname = string.Empty;
    public static string mnth = string.Empty;
    public static string yr = string.Empty;
    public static string subdiv_code = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
	 public static string statv = string.Empty;
    public static string stattext = string.Empty;
    public static DataSet dss = new DataSet();
    public static DataSet msf = new DataSet();
    public static string constr = Globals.ConnString;
    public static SqlConnection con = null;
    public static DataTable dtform = new DataTable();
    public static DateTime ldt;
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    public static string[] paydt;
    int j = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfCode"].ToString();
        sfname = Request.QueryString["sfname"].ToString();
        mnth = Request.QueryString["FMonth"].ToString();
        yr = Request.QueryString["FYear"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
		statv = Request.QueryString["stcode"].ToString();
        stattext = Request.QueryString["stval"].ToString();
    }
    [WebMethod]
    public static string GetDetails(string SF, string Div, int Mn, int Yr, string subdiv, string statecd)
    {
        sfCode = SF;
        divcode = Div;
        fdt = Mn.ToString();
        tdt = Yr.ToString();
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        if (Mn.ToString() != "" && Yr.ToString() != "")
        {
            SqlConnection con = new SqlConnection(Globals.ConnString);
            con.Open();
            SqlCommand cmd = new SqlCommand("exec New_Monthly_Salesforce '" + Div + "','" + SF + "','" + Mn.ToString() + "','" + Yr.ToString() + "','" + subdiv + "','" + statecd + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            msf = ds;
            MIS_Reports_Monthly_Attendance_Report frm25 = new MIS_Reports_Monthly_Attendance_Report();
            dtform = frm25.getMonthlyForm(SF, Div, Mn, Yr);
            return JsonConvert.SerializeObject(dtform);
        }
        else
        {
            return "";
        }
    }
    public DataTable getMonthlyForm(string SF, string Div, int Mn, int Yr)
    {
        SalesForce SFD = new SalesForce();
        DataTable ds = new DataTable();
        ds.Columns.Add("SlNo", typeof(string));
        ds.Columns.Add("Employee_ID", typeof(string));
        ds.Columns.Add("Employee_Name", typeof(string));
        ds.Columns.Add("Designation", typeof(string));
        ds.Columns.Add("Joining_Date", typeof(string));
        ds.Columns.Add("HQ", typeof(string));
        ds.Columns.Add("Reporting_Manger", typeof(string));
        ds.Columns.Add("State", typeof(string));

        DateTime tDt = new DateTime(Yr, Mn, 1);
        DateTime tTDt = tDt.AddMonths(1).AddDays(-1);
        DateTime curdt = DateTime.Now;
        int tdays = System.DateTime.DaysInMonth(Yr, Mn);
        double totDys = 0;
        int sundays = 0;
        while (tDt <= tTDt)
        {
            ds.Columns.Add(tDt.ToString("dd_MM"), typeof(string));
            if (tDt.DayOfWeek == DayOfWeek.Sunday)
            {
                sundays += 1;
            }
            tDt = tDt.AddDays(1);
            totDys++;
        }

        ds.Columns.Add("Total_Days", typeof(double));
        ds.Columns.Add("Working_days", typeof(double));
        ds.Columns.Add("Retailing_days", typeof(double));
        ds.Columns.Add("Otherofficialwork", typeof(double));
        ds.Columns.Add("Leave", typeof(double));
        ds.Columns.Add("Holiday", typeof(double));
        ds.Columns.Add("Absent", typeof(double));
        ds.Columns.Add("Weekly_Off", typeof(double));

        DataSet dt = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Exec getnewmonthlyattendance '" + SF + "','" + Div + "','" + Mn + "','" + Yr + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        con.Close();

        var dsfcodes = (from dtr in dt.Tables[0].AsEnumerable()
                        where dtr.Field<string>("Sf_Code") != ""
                        select dtr.Field<string>("Sf_Code")).Distinct();
        int i = 0;
        foreach (var str in dsfcodes)
        {
            int hecnt = 0;
            DataRow rw = ds.NewRow();
            DataRow[] drT = msf.Tables[0].Select("Sf_Code='" + str + "'");
            if (drT.Length > 0)
            {
                rw["SlNo"] = (i + 1).ToString();
                i++;
                rw["Employee_ID"] = drT[0]["Employee_ID"].ToString();
                rw["Employee_Name"] = drT[0]["SF_Name"].ToString();
                rw["Designation"] = drT[0]["Desig"].ToString();
                rw["Joining_Date"] = drT[0]["Sf_Joining_Date"].ToString();
                rw["HQ"] = drT[0]["HQ"].ToString();
                rw["Reporting_Manger"] = drT[0]["Reporting_To"].ToString();
                rw["State"] = drT[0]["StateName"].ToString();
                rw["Total_Days"] = totDys;
                double holidayct = 0;
                double wd = 0;
                double PDys = 0; double lcnt = 0; double oworktype = 0; double AbDy = 0; double weeklyoffct = 0; double NmDy = 0;
                tDt = new DateTime(Yr, Mn, 1);
                while (tDt <= tTDt)
                {
                    DataRow[] dr = dt.Tables[0].Select("Sf_Code='" + drT[0]["SF_Code"].ToString() + "' and Dt='" + tDt.ToString("yyyy-MM-dd") + "'");
                    string typ = "";
                    if (dr.Length > 0)
                    {
                        wd++;
                        typ = dr[0]["Wtype_Sname"].ToString();
                        NmDy++;
                        if (typ == "L")
                        {
                            lcnt += 1;
                        }
                        else if (typ != "L" && typ != "FW" && typ != "WO" && typ != "H")
                        {
                            oworktype += 1;
                        }
                        else if (typ == "H")
                        {
                            holidayct += 1;
                        }
                        else if (typ == "WO")
                        {
                            weeklyoffct += 1;
                        }
                    }
                    else if (tDt.DayOfWeek == DayOfWeek.Sunday)
                    {
                        typ = "S";
                    }
                    else if (tDt >= curdt)
                    {
                        typ = "";
                    }
                    else
                    {
                        typ = "A";
                        AbDy += 1;
                    }
                    rw[tDt.ToString("dd_MM")] = typ;
                    tDt = tDt.AddDays(1);
                }
                rw["Working_days"] = wd - (lcnt + holidayct + weeklyoffct);
                rw["Retailing_days"] = wd - (oworktype + lcnt + holidayct + weeklyoffct);
                rw["Otherofficialwork"] = oworktype;
                rw["Leave"] = lcnt;
                rw["Holiday"] = holidayct;
                rw["Absent"] = AbDy;
                rw["Weekly_Off"] = weeklyoffct;
                ds.Rows.Add(rw);
            }
        }
        return ds;
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dtform, "Monthly Attendance List");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Monthly_Attendance.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
	 [WebMethod]
    public static string gethints(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet ds = SFD.get_attend_hint(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}