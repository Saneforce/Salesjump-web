using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



using Bus_EReport;
using System.Data;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;

public partial class MIS_Reports_rptSummaryReport : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = new DataSet();
    DataSet dsDistributor = new DataSet();
    DataSet dsDisAllDtls = new DataSet();
    string DivCode = string.Empty;
    string SFCode = string.Empty;
    public static string Fdate = string.Empty;
    public static string Tdate = string.Empty;
    string sub_Div_Code = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        DivCode = Session["div_code"].ToString();
        SFCode = Request.QueryString["SF_Code"].ToString();
        Fdate = Request.QueryString["Fdate"].ToString();
        Tdate = Request.QueryString["Tdate"].ToString();
        sub_Div_Code = Request.QueryString["Sub_Div"].ToString();
        hdnMonth.Value = Fdate;
        hdnYear.Value = Tdate;
        hdnSFCode.Value = SFCode;
        hdnSubDivCode.Value = sub_Div_Code;
       // Label1.Text = "<b>Team </b>: " + Request.QueryString["SF_Name"].ToString();
        Label2.Text = "Loading Please Wait..!";
    }

    public class TPDates
    {
        public string tpdays { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static TPDates[] GetTpDates(string Fdate, string Tdate)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        SalesForce sf = new SalesForce();
        DataSet dsDistributor = new DataSet();
        dsDistributor = sf.GetTpMyDayPlan(div_code, Fdate, Tdate);
        List<TPDates> tpDay = new List<TPDates>();
        foreach (DataRow row in dsDistributor.Tables[0].Rows)
        {
            TPDates tpd = new TPDates();
            tpd.tpdays = Convert.ToDateTime(row["dtaes"]).ToString("dd/MM/yy");
            tpDay.Add(tpd);
        }
        return tpDay.ToArray();
    }

    public class FFList
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string RSFs { get; set; }
        public string sfHQ { get; set; }
        public string EmpID { get; set; }
        public string Desig { get; set; }
        public string sfMobile { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static FFList[] GetFieldForces(string SF_Code, string SubDiv)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }       
        Product sf = new Product();
        DataSet dsFF = new DataSet();
        dsFF = sf.getDCRUsers(SF_Code, div_code, Fdate, Tdate, SubDiv);
        List<FFList> FList = new List<FFList>();
        foreach (DataRow row in dsFF.Tables[0].Rows)
        {
            FFList ffl = new FFList();
            ffl.sfCode = row["SF_Code"].ToString();
            ffl.sfName = row["SF_Name"].ToString();
            ffl.EmpID = row["sf_emp_id"].ToString();
            ffl.Desig = row["Designation"].ToString();
            ffl.sfHQ = row["sf_hq"].ToString();
            ffl.RSFs = row["Reporting_To_SF"].ToString();
            ffl.sfMobile = row["SF_Mobile"].ToString();
            FList.Add(ffl);
        }
        return FList.ToArray();
    }

    public class TpMyDayList
    {
        public string sfCode { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public string workType { get; set; }
        public string Remarks { get; set; }
        public string tpDate { get; set; }
        public string ldrCount { get; set; }
		public string startTime { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static TpMyDayList[] GetTpMyDayDetails(string Fdate, string Tdate,string SF_Code)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        SalesForce sf = new SalesForce();
        DataSet dsTpPlan = new DataSet();
        dsTpPlan = sf.GetSummaryTpMyDayPlan(div_code, Fdate, Tdate, SF_Code);
        List<TpMyDayList> tpList = new List<TpMyDayList>();
        foreach (DataRow row in dsTpPlan.Tables[0].Rows)
        {
            TpMyDayList tl = new TpMyDayList();
            tl.sfCode = row["sf_code"].ToString();
            tl.RouteCode = row["cluster"].ToString();
            tl.RouteName = row["ClstrName"].ToString();
            tl.workType = row["Worktype_Name"].ToString();
            tl.Remarks = row["remarks"].ToString();
            tl.tpDate = Convert.ToDateTime(row["dates"]).ToString("dd/MM/yy");
            tl.ldrCount = row["cnt"].ToString();
			tl.startTime = row["pln"].ToString();
            tpList.Add(tl);
        }
        return tpList.ToArray();
    }

    public class TCECList
    {
        public string sfCode { get; set; }
        public string Activity_Date { get; set; }
        public string TVRC { get; set; }
        public string TPRC { get; set; }
        public string NRC { get; set; }
        public string NRVAL { get; set; }
        public string Order_Value { get; set; }
        public string phoneorder { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static TCECList[] GetTcEcDetails(string Fdate, string Tdate)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        SalesForce sf = new SalesForce();
        DataSet dsTcEc = new DataSet();
        dsTcEc = sf.GetSummaryTCECValues(div_code, Fdate, Tdate);
        List<TCECList> tpList = new List<TCECList>();
        foreach (DataRow row in dsTcEc.Tables[0].Rows)
        {
            TCECList tl = new TCECList();
            tl.sfCode = row["sf_code"].ToString();
            tl.Activity_Date = Convert.ToDateTime(row["Activity_Date"]).ToString("dd/MM/yy"); 
            tl.TVRC = row["TVRC"].ToString();
            tl.TPRC = row["TPRC"].ToString();
            tl.NRC = row["NRC"].ToString();
            tl.NRVAL = row["NRVAL"].ToString();
            tl.Order_Value = row["Order_Value"].ToString();
            tl.phoneorder = row["phoneorder"].ToString();
            tpList.Add(tl);
        }
        return tpList.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static SfCatOrderDetails[] GetCategoryHead()
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        Product prd = new Product();
        DataTable dtCategory = new DataTable();

        dtCategory = prd.getProductCategorylist_DataTable(div_code);
        List<SfCatOrderDetails> dsDtls = new List<SfCatOrderDetails>();
        foreach (DataRow row in dtCategory.Rows)
        {
            SfCatOrderDetails dtls = new SfCatOrderDetails();
            dtls.pCatCode = row["Product_Cat_Code"].ToString();
            dtls.pCatName = row["Product_Cat_Name"].ToString();
            dsDtls.Add(dtls);
        }
        return dsDtls.ToArray();
    }

    public class SfCatOrderDetails
    {
        public string sfCode { get; set; }
        public string Activity_Date { get; set; }
        public string pCatCode { get; set; }
        public string pCatName { get; set; }        
        public string ordQty { get; set; }
        public string ordVal { get; set; }        
    }
    [WebMethod(EnableSession = true)]
    public static SfCatOrderDetails[] GetCatOrderDetails(string Fdate, string Tdate)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        Product prd = new Product();
        DataSet dsCatDtls = new DataSet();
        dsCatDtls = prd.GetCategorywiseOrderFieldForce(div_code, Fdate, Tdate);
        List<SfCatOrderDetails> tpList = new List<SfCatOrderDetails>();
        foreach (DataRow row in dsCatDtls.Tables[0].Rows)
        {
            SfCatOrderDetails tl = new SfCatOrderDetails();
            tl.sfCode = row["sf_code"].ToString();
            tl.Activity_Date = Convert.ToDateTime(row["Activity_Date"]).ToString("dd/MM/yy");
            tl.pCatCode = row["Product_Cat_Code"].ToString();            
            tl.ordQty = row["Qty"].ToString();
            tl.ordVal = row["value"].ToString();            
            tpList.Add(tl);
        }
        return tpList.ToArray();
    }
    public class ReportingSF
    {
        public string Sf_Code { get; set; }
        public string sf_Name { get; set; }
        public string RSF_Code { get; set; }
        public string Designation { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static ReportingSF[] GetReportingToSF()
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        SalesForce sf = new SalesForce();
        DataSet dsSFHead = new DataSet();
        dsSFHead = sf.SalesForceListMgrGet_MgrOnly(div_code, "admin", "0");
        List<ReportingSF> dsDtls = new List<ReportingSF>();
        foreach (DataRow row in dsSFHead.Tables[0].Rows)
        {
            ReportingSF dtls = new ReportingSF();
            dtls.Sf_Code = row["Sf_Code"].ToString();
            dtls.sf_Name = row["Sf_Name"].ToString();
            dtls.RSF_Code = row["Reporting_To_SF"].ToString();
            dtls.Designation = row["Designation"].ToString();

            dsDtls.Add(dtls);
        }
        return dsDtls.ToArray();
    }


    public class OrderTimes
    {
        public string Sf_Code { get; set; }
        public string ordDate { get; set; }
        public string minTime { get; set; }
        public string maxTime { get; set; }
        public string Duration { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static OrderTimes[] GetOrderTimes(string Fdate, string Tdate)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        Order ord = new Order();
        DataSet dsOrdTime = new DataSet();
        dsOrdTime = ord.GetOrderTimesSFWise(div_code, Fdate, Tdate);
        List<OrderTimes> dsDtls = new List<OrderTimes>();
        foreach (DataRow row in dsOrdTime.Tables[0].Rows)
        {
            OrderTimes dtls = new OrderTimes();
            dtls.Sf_Code = row["sf_code"].ToString();
            dtls.ordDate = Convert.ToDateTime(row["adate"]).ToString("dd/MM/yy"); 
            dtls.minTime = row["minTime"].ToString();
            dtls.maxTime = row["maxTime"].ToString();
            TimeSpan duration = DateTime.Parse(row["maxTime"].ToString()).Subtract(DateTime.Parse(row["minTime"].ToString()));
            dtls.Duration = duration.ToString();
            dsDtls.Add(dtls);
        }
        return dsDtls.ToArray();
    }

    public class OrderCounts
    {
        public string Sf_Code { get; set; }
        public string ordDate { get; set; }
        public string cnt { get; set; }
      
    }

    [WebMethod(EnableSession = true)]
    public static OrderCounts[] GetOrderCounts(string Fdate, string Tdate)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        Order ord = new Order();
        DataSet dsOrdCounts = new DataSet();
        dsOrdCounts = ord.GetOrderCountSFWise(div_code, Fdate, Tdate);
        List<OrderCounts> dsDtls = new List<OrderCounts>();
        foreach (DataRow row in dsOrdCounts.Tables[0].Rows)
        {
            OrderCounts dtls = new OrderCounts();
            dtls.Sf_Code = row["sf_code"].ToString();
            dtls.ordDate = Convert.ToDateTime(row["aDate"]).ToString("dd/MM/yy");
            dtls.cnt = row["cnt"].ToString();           
            dsDtls.Add(dtls);
        }
        return dsDtls.ToArray();
    }


    public class StartTimes
    {
        public string Sf_Code { get; set; }
        public string login_date { get; set; }
        public string Start_Time { get; set; }
        public string End_Time { get; set; }
        public string Start_Lat { get; set; }
        public string Start_Long { get; set; }
        public string durations { get; set; }
        

    }

    [WebMethod(EnableSession = true)]
    public static StartTimes[] GetStartTimes(string Fdate, string Tdate)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        Order ord = new Order();
        DataSet dsOrdCounts = new DataSet();
        dsOrdCounts = ord.GetStartTimes(div_code, Fdate, Tdate);
        List<StartTimes> dsDtls = new List<StartTimes>();
        foreach (DataRow row in dsOrdCounts.Tables[0].Rows)
        {
            StartTimes dtls = new StartTimes();
            dtls.Sf_Code = row["Sf_Code"].ToString();
            dtls.login_date = Convert.ToDateTime(row["login_date"]).ToString("dd/MM/yy");
            dtls.Start_Time = row["Start_Time"].ToString();
            dtls.End_Time = row["End_Time"].ToString();
            string ST = row["Start_Time"].ToString();
            string ET = row["End_Time"] == DBNull.Value ? "0" : row["End_Time"].ToString();
            string dut ="";
            if (ET != "0")
            {
                TimeSpan duration = DateTime.Parse(ET).Subtract(DateTime.Parse(ST));
                dut = duration.ToString();
            }

            dtls.Start_Lat = row["Start_Lat"].ToString();
            dtls.Start_Long = row["Start_Long"].ToString();
            dtls.durations = dut;
            dsDtls.Add(dtls);
        }
        return dsDtls.ToArray();
    }



}