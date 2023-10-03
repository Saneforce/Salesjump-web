using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Bus_EReport;
using Newtonsoft.Json;
using System.Web.Services;
using System.Data.SqlClient;
using DBase_EReport;

public partial class MIS_Reports_rpt_FieldForceTracking : System.Web.UI.Page
{
    #region "Declaration"      
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string SubDiv = string.Empty;
    string FDates = string.Empty;
    string TDates = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["SFCode"].ToString();
        FDates = Request.QueryString["FDate"].ToString();
        //TDates = Request.QueryString["TDate"].ToString();
        SubDiv = Request.QueryString["SubDiv"].ToString();

        ddlFieldForce.Value = sf_code;
        ddlFDate.Value = FDates;
        ddlTDate.Value = TDates;
        SubDivCode.Value = SubDiv;
        Label1.Text = "Field Force Tracking of From :  " + FDates + " To  " + TDates;
        Label2.Text = "Field Force : " + Request.QueryString["SFName"].ToString();
    }
    [WebMethod(EnableSession = true)]
    public static LatLong[] GetEmployees(string SF_Code, string FDate,string divcode)
    {

        List<LatLong> LL = new List<LatLong>();
        SalesForce sf = new SalesForce();


        DateTime dtsd = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
        string stD = dtsd.ToString("yyyy-MM-dd");

        DataSet dsPro = GetSFTracking(SF_Code, stD, divcode);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            LatLong ll = new LatLong();
            ll.sfCode = row["sf_Code"].ToString();
            ll.ROWNUMBER = row["ROWNUMBER"].ToString();
            ll.Sf_Name = row["Sf_name"].ToString();
            ll.Lat = row["Lat"].ToString();
            ll.Long = row["Long"].ToString();
            ll.DtTm = Convert.ToDateTime(row["DtTm"]).ToString("dd-MM-yyyy hh:mm:ss tt "); // row["DtTm"].ToString();
            ll.chdate = Convert.ToDateTime(row["DtTm"]).ToString("yyyy-MM-dd hh:mm:ss");
            LL.Add(ll);
        }
        return LL.ToArray();
    }

    public class LatLong
    {
        public string sfCode { get; set; }
        public string ROWNUMBER { get; set; }
        public string Sf_Name { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string DtTm { get; set; }
        public string chdate { get; set; }
    }
    public class battaryData
    {
        public string sfCode { get; set; }
        public string Battery_Status { get; set; }
        public string DateandTime { get; set; }
        public string dateDisplay { get; set; }


    }

    [WebMethod(EnableSession = true)]
    public static battaryData[] GetBatteryData(string SF_Code, string FDate, string divcode)
    {

        List<battaryData> LL = new List<battaryData>();
        SalesForce sf = new SalesForce();

        DateTime dtsd = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
        string stD = dtsd.ToString("yyyy-MM-dd");




        DataSet dsPro = GetBattaryStatus(SF_Code, stD, divcode);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            battaryData ll = new battaryData();
            ll.sfCode = row["sf_code"].ToString();
            decimal stu = row["Battery_Status"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Battery_Status"]);
            ll.Battery_Status = Math.Round((stu * 100)).ToString();
            ll.DateandTime = Convert.ToDateTime(row["DateandTime"]).ToString("yyyy-MM-dd hh:mm:ss");
            ll.dateDisplay = Convert.ToDateTime(row["DateandTime"]).ToString("dd-MM-yyyy hh:mm:ss tt");
            LL.Add(ll);
        }
        return LL.ToArray();
    }
    public static DataSet GetSFTracking(string SFCode, string FDate, string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsSF = null;
        string strQry = "exec TrackLoctionadsfwise '" + SFCode + "','" + FDate + "','" + divcode + "'";
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
    public static DataSet GetBattaryStatus(string SFCode, string FDate, string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsSF = null;
        string strQry = "exec trackbatterysfwise  '" + SFCode + "','" + FDate + "','" + divcode + "' ";
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
}