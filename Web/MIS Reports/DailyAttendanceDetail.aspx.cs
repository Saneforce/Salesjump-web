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

public partial class MIS_Reports_DailyAttendanceDetail : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string sfcode = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
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
    public static string GetDetails(string SF,string Div, string Mn, string Yr)
    {
        divcode = Div;
        sfcode = SF;
        fdt = Mn;
        tdt = Yr;
        SalesForce SFD = new SalesForce();
        //DataSet dchk = SFD.getSFSHQ(divcode, SF);
        DataSet ds = new DataSet();
        //if (dchk.Tables[0].Rows.Count == 0)
        //{
            ds = SFD.GetLOGIN_Details(SF, Div, Mn, Yr);
        //}
        //else
        //{
        //    ds = SFD.GetLOGIN_Details_HQ(SF, Div, Mn, Yr);
        //}
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public class sfMGR
    {
        public string sfname { get; set; }
        public string sfcode { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static sfMGR[] getMGR(string divcode, string sfcode)
    {
        SalesForce dsf = new SalesForce();
        DataSet dsSalesForce = dsf.UserList_Hierarchy(divcode, sfcode);
        List<sfMGR> sf = new List<sfMGR>();
        foreach (DataRow rows in dsSalesForce.Tables[0].Rows)
        {
            sfMGR rt = new sfMGR();
            rt.sfcode = rows["SF_Code"].ToString();
            rt.sfname = rows["Sf_Name"].ToString();
            sf.Add(rt);
        }
        return sf.ToArray();
    }

    //[WebMethod]
    //public static string GetHQDetails(string divcode)
    //{
    //    SalesForce SFD = new SalesForce();
    //    DataSet ds = SFD.getAllSFHQ(divcode);
    //    return JsonConvert.SerializeObject(ds.Tables[0]);
    //}
    [WebMethod]
    public static string GetSFModal(string sfcode, string logdate)
    {
        SalesForce SFD = new SalesForce();
        DataTable ds = SFD.getsftp_attendance(sfcode, logdate);
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod]
    public static string GetCHSFModal(string sfcode, string logdate)
    {
        SalesForce SFD = new SalesForce();
        DataTable ds = SFD.getsfchtp_attendance(sfcode, logdate);
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod]
    public static string shiftUpdate(string SF,string logdt,string nsftid, string psftid)
    {
        SalesForce SFD = new SalesForce();
        string msg = SFD.attendanceShiftUpdate(SF, logdt, nsftid, psftid);
        return msg;
    }


    [WebMethod(EnableSession = true)]
    public static string getShift(string divcode, string HQ)
    {
        CallPlan ast = new CallPlan();
        DataSet ds = ast.getAttendShift(divcode.TrimEnd(','), HQ);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }


    [WebMethod(EnableSession = true)]
    public static string updateShift(string divcode, string HQ)
    {
        CallPlan ast = new CallPlan();
        DataSet ds = ast.getAttendShift(divcode.TrimEnd(','), HQ);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        SalesForce SFD = new SalesForce();
        DataTable dt = new DataTable();
        dt = SFD.GetLOGIN_Details_Excel(sfcode, divcode, fdt, tdt);
        DataTable dtfilter = dt;
        DataView dataView = dtfilter.DefaultView;
        if (hfilter.Value != null && hfilter.Value != "" && hfilter.Value != "All")
        {
            dataView.RowFilter = "Status = '" + hfilter.Value + "'";
            dtfilter = dataView.ToTable();
            dataView = dtfilter.DefaultView;
        }
        if (hffilter.Value != null && hffilter.Value != "" && hffilter.Value != "AllFF")
        {
            dataView.RowFilter = "Approved_By = '" + hffilter.Value + "'";
            dtfilter = dataView.ToTable();
            dataView = dtfilter.DefaultView;
        }
        if (hsfhq.Value != null && hsfhq.Value != "" && hsfhq.Value != "All")
        {
            dataView.RowFilter = "HQ_Name = '" + hsfhq.Value + "'";
            dtfilter = dataView.ToTable();
            dataView = dtfilter.DefaultView;
        }
        //dtfilter.Columns.Remove("Division");
        dtfilter.Columns.Remove("Time_Deviation");
        dtfilter.Columns.Remove("Approved_By");
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dtfilter, "Daily Attendance List");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Daily_Attendance_List_" + fdt + "_to_" + tdt + ".xlsx");
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