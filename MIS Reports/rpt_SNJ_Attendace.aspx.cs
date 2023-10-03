using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_rpt_SNJ_Attendace : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    public static string sfCode = string.Empty;
    string sfname = string.Empty;
    public static string Fdtate = string.Empty;
    public static string Tdate = string.Empty;
    string subdiv_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfname = Request.QueryString["sfname"].ToString();
        sfCode = Request.QueryString["sfCode"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        Fdtate = Request.QueryString["FDate"].ToString();
        Tdate = Request.QueryString["TDate"].ToString();
        
        lblHead.Text = "Attendance for the Month of " + GetFormatedDate(Fdtate) + " To " + GetFormatedDate(Tdate);
        lblsf_name.Text = sfname;
    }
    static string GetFormatedDate(string dtate)
    {
        string thisMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToDateTime(dtate).Month);
        string thisDay = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(Convert.ToDateTime(dtate).DayOfWeek);
        DateTime thday = DateTime.ParseExact(dtate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        string thisyear = Convert.ToString(Convert.ToDateTime(dtate).Year);
        return Convert.ToString(thday.Day) + '-'+ thisMonth+'-'+ thisyear;
    }
    [WebMethod]
    public static string getUserdata()
    {
        loc SFD = new loc();
        DataSet ds = SFD.get_User_data();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getUserWTdata()
    {
        loc SFD = new loc();
        DataSet ds = SFD.get_Userwt_data();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public class loc
    {
        public DataSet get_User_data()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string strQry = string.Empty;
            DataSet dsDivision = null;

            strQry = "exec [sp_getUser_atten_SNJ] '" + div_code + "','" + sfCode + "'";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataSet get_Userwt_data()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string strQry = string.Empty;
            DataSet dsDivision = null;

            strQry = "exec [sp_AttendSNJ_WT] '" + div_code + "','" + Fdtate + "','" + Tdate + "'";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
    }
}