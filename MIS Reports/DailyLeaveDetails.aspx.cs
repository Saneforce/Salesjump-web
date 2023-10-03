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
using DBase_EReport;

public partial class MIS_Reports_DailyLeaveDetails : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string sfcode = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    string Division_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    public static DataSet ds_ldets = new DataSet();

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
        lvest SFD = new lvest();
        //DataSet dchk = SFD.getSFSHQ(divcode, SF);
        ds_ldets = SFD.GetLeaveDetails(SF, Div, Mn, Yr);
        
        return JsonConvert.SerializeObject(ds_ldets.Tables[0]);
    }
	
    public class sfMGR
    {
        public string sfname { get; set; }
        public string sfcode { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static sfMGR[] getMGR(string divcode, string sfcode)
    {
        lvest dsf = new lvest();
        DataSet dsSalesForce = dsf.UserList_Hierarchy_filter(divcode, sfcode);
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

    [WebMethod]
    public static string GetHQDetails(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet ds = SFD.getAllSFHQ(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
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
    public static string shiftUpdate(string SF, string logdt, string nsftid, string psftid)
    {
        SalesForce SFD = new SalesForce();
        string msg = SFD.attendanceShiftUpdate(SF, logdt, nsftid, psftid);
        return msg;
    }


    [WebMethod(EnableSession = true)]
    public static string getShift(string divcode, string HQ, string deptcode)
    {
        lvest ast = new lvest();
        DataSet ds = ast.getAttendShift(divcode.TrimEnd(','), HQ,deptcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        DataTable dtsummary = new DataTable();
        dtsummary = ds_ldets.Tables[0];

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dtsummary, "Leave Details");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Leave_Details_" + fdt + "_to_" + tdt + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    
    }
    public class lvest
    {
        public DataSet GetLeaveDetails(string SF, string Div, string Mn, string Yr)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
           string strQry = "EXEC newgetLeaveDetails '" + SF + "','" + Mn + "','" + Yr + "','" + Div.TrimEnd(',') + "'";

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
        public DataSet UserList_Hierarchy_filter(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            string strQry = "EXEC getUserFilter '" + divcode + "', '" + sf_code + "' ";

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
        public DataSet getAttendShift(string divcode, string hqcode, string deptcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select * from Mas_Shift_Timings where CHARINDEX(','+'" + hqcode + "'+',',','+HQ_Code+',')>0  and ActiveFlag=0  and CHARINDEX(','+'" + deptcode + "'+',',','+dept_code+',')>0";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}