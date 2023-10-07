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

public partial class MIS_Reports_rptFieldForceTracking : System.Web.UI.Page
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
        TDates = Request.QueryString["TDate"].ToString();
        SubDiv = Request.QueryString["SubDiv"].ToString();

        ddlFieldForce.Value = sf_code;
        ddlFDate.Value = FDates;
        ddlTDate.Value = TDates;
        SubDivCode.Value = SubDiv;
        Label1.Text = "Field Force Tracking of From :  " + FDates + " To  " + TDates;
        Label2.Text = "Field Force : " + Request.QueryString["SFName"].ToString();
    }

    [WebMethod(EnableSession = true)]
    public static LatLong[] GetEmployees(string SF_Code, string FDate, string TDate)
    {

        List<LatLong> LL = new List<LatLong>();
        SalesForce sf = new SalesForce();


        DateTime dtsd = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
        string stD = dtsd.ToString("yyyy-MM-dd");


        DateTime dted = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
        string etD = dted.ToString("yyyy-MM-dd");
         
        DataSet dsPro = GetSFTracking(SF_Code, stD, etD);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            LatLong ll = new LatLong();
            ll.sfCode = row["sf_Code"].ToString();
            ll.ROWNUMBER = row["ROWNUMBER"].ToString();
            ll.lat = row["lat"].ToString();
            ll.lng = row["lng"].ToString();
            ll.DtTm = Convert.ToDateTime(row["DtTm"]).ToString("dd-MM-yyyy hh:mm:ss tt "); // row["DtTm"].ToString();
            ll.chdate = Convert.ToDateTime(row["DtTm"]).ToString("yyyy-MM-dd hh:mm:ss");
            ll.rname = row["Trans_Detail_Name"].ToString();
            ll.addr = row["GeoAddrs"].ToString();
            LL.Add(ll);
        }
        return LL.ToArray();
    }

    [WebMethod]
    public static DataSet  GetSFTracking(string SF_Code, string stD, string etD)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = db.Exec_DataSet("exec SFvisit_details '" + SF_Code + "','" + stD + "','"+ etD + "'");        
        return ds;
    }

    public class LatLong
    {
        public string sfCode { get; set; }
        public string ROWNUMBER { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string DtTm { get; set; }
        public string chdate { get; set; }
        public string rname { get; set; }
        public string addr { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static LatsLong[] GetEmploydets(string SF_Code, string FDate, string TDate)
    {

        List<LatsLong> LL = new List<LatsLong>();
        SalesForce sf = new SalesForce();


        DateTime dtsd = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
        string stD = dtsd.ToString("yyyy-MM-dd");


        DateTime dted = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
        string etD = dted.ToString("yyyy-MM-dd");

        DataSet dsPro = sf.GetSFTracking(SF_Code, stD, etD);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            LatsLong ll = new LatsLong();
            ll.sfCode = row["sf_Code"].ToString();
            ll.ROWNUMBER = row["ROWNUMBER"].ToString();
            ll.lat = row["Lat"].ToString();
            ll.lng = row["Long"].ToString();
            ll.DtTm = Convert.ToDateTime(row["DtTm"]).ToString("dd-MM-yyyy hh:mm:ss tt "); // row["DtTm"].ToString();
            ll.chdate = Convert.ToDateTime(row["DtTm"]).ToString("yyyy-MM-dd hh:mm:ss");           
            LL.Add(ll);
        }
        return LL.ToArray();
    }
    public class LatsLong
    {
        public string sfCode { get; set; }
        public string ROWNUMBER { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
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
    public static battaryData[] GetBatteryData(string SF_Code, string FDate, string TDate)
    {

        List<battaryData> LL = new List<battaryData>();
        SalesForce sf = new SalesForce();

        DateTime dtsd = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
        string stD = dtsd.ToString("yyyy-MM-dd");


        DateTime dted = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
        string etD = dted.ToString("yyyy-MM-dd");

        DataSet dsPro = sf.GetBattaryStatus(SF_Code, stD, etD);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            battaryData ll = new battaryData();
            ll.sfCode = row["sf_code"].ToString();
            decimal stu =  row["Battery_Status"] == DBNull.Value  ? 0 : Convert.ToDecimal(row["Battery_Status"]) ;
            ll.Battery_Status = Math.Round((stu * 100)).ToString();
            ll.DateandTime = Convert.ToDateTime(row["DateandTime"]).ToString("yyyy-MM-dd hh:mm:ss");
            ll.dateDisplay = Convert.ToDateTime(row["DateandTime"]).ToString("dd-MM-yyyy hh:mm:ss tt");     
            LL.Add(ll);
        }
        return LL.ToArray();
    }
}
