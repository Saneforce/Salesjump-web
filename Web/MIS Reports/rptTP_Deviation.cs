using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;

using System.Web.Services;

public partial class MIS_Reports_rptTP_Deviation : System.Web.UI.Page
{

    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsSales = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DataSet dsLeave = null;
    string Monthsub = string.Empty;
    string tot_dr = string.Empty;
    string Days = string.Empty;
    string strSf_Code = string.Empty;
    string sCurrentDate = string.Empty;
    string strDayName = string.Empty;
    int count = 0;
    string type = string.Empty;
    int iIndex;


    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FDate"].ToString();
        FYear = Request.QueryString["TDate"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();

        hSfCode.Value = sfCode;
        hFyear.Value = FYear;
        hFMonth.Value = FMonth;
        hSFName.Value = sfname;
        lblRegionName.Text = sfname;
        //System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        //lblHead.Text = "TP - Deviation for the Month of " + strFMonthName + " " + FYear;

    }


    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strFileName = Page.Title;
        string attachment = "attachment; filename='" + strFileName + "'.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }

    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }


    [WebMethod(EnableSession = true)]
    public static GetDatas[] GetDataDCR(string SF_Code, string FYera, string FMonth)
    {
        List<GetDatas> empList = new List<GetDatas>();
        DCR_New dcn = new DCR_New();
        DataSet dsPro = dcn.GetDCRDeviation(SF_Code, FYera, FMonth);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            GetDatas emp = new GetDatas();
            emp.sfCode = row["sf_code"].ToString();
            emp.sfName = row["sf_name"].ToString();
            emp.tpDate = Convert.ToDateTime(row["Activity_Date"]).ToString("yyyy/MM/dd");
            emp.Work_Type = row["Work_Type"].ToString();
            emp.WorkType_Name = row["WorkType_Name"].ToString();
            emp.Plan_No = row["Plan_No"].ToString();
            emp.Plan_Name = row["Plan_Name"].ToString();
            emp.Distributor = row["StkName"].ToString();
            emp.workedwith = row["worked_with_name"].ToString();

            

            empList.Add(emp);
        }
        return empList.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static GetDatas[] GetDataTP(string SF_Code, string FYera, string FMonth)
    {
        List<GetDatas> empList = new List<GetDatas>();
        TP_New tpn = new TP_New();
        DataSet dsPro = tpn.GetTPDeviation(SF_Code, FYera, FMonth);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            GetDatas emp = new GetDatas();
            emp.sfCode = row["sf_code"].ToString();
            emp.sfName = row["TP_Sf_Name"].ToString();
            emp.tpDate = Convert.ToDateTime(row["Tour_Date"]).ToString("yyyy/MM/dd");
            emp.Work_Type = row["WorkType_Code_B"].ToString();
            emp.WorkType_Name = row["Worktype_Name_B"].ToString();
            emp.Plan_No = row["Tour_Schedule1"].ToString();
            emp.Plan_Name = row["Territory_Code1"].ToString();
            emp.Distributor = row["Distributor"].ToString();

            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class GetDatas
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string tpDate { get; set; }
        public string workedwith { get; set; }
        public string Work_Type { get; set; }
        public string WorkType_Name { get; set; }
        public string Plan_No { get; set; }
        public string Plan_Name { get; set; }
        public string Distributor { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static OrderTpEc[] GetDataTCEC(string SF_Code, string FYera, string FMonth)
    {
        List<OrderTpEc> empList = new List<OrderTpEc>();
        Order tpn = new Order();
        DataSet dsPro = tpn.GetTCECDeviation(SF_Code, FYera, FMonth);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            OrderTpEc emp = new OrderTpEc();
            emp.sfCode = row["sf_code"].ToString();
            emp.Activity_Date = Convert.ToDateTime(row["Activity_Date"]).ToString("yyyy/MM/dd");
            emp.Order_Value = row["Order_Value"].ToString();
            emp.WorkType_Name = row["Worked_with_Name"].ToString();
            emp.TC = row["TC"].ToString();
            emp.EC = row["EC"].ToString();
            emp.RNC = row["RTC"].ToString();
            emp.SplCode = row["Doc_Special_Code"].ToString();
            emp.SplName = row["Doc_Special_Name"].ToString();
            emp.Distributor = row["stockist_name"].ToString();
            emp.Address1 = row["ListedDr_Address1"].ToString();
            emp.Remarks1 = row["Activity_Remarks"].ToString();
            emp.phoneOrder = row["phoneOrder"].ToString();



            empList.Add(emp);
        }
        return empList.ToArray();
    }
    public class OrderTpEc
    {
        public string sfCode { get; set; }
        public string Activity_Date { get; set; }
        public string WorkType_Name { get; set; }
        public string TC { get; set; }
        public string EC { get; set; }
        public string RNC { get; set; }
        public string Order_Value { get; set; }
        public string SplCode { get; set; }
        public string SplName { get; set; }
        public string Distributor { get; set; }
        public string route { get; set; }
        public string Address1 { get; set; }
        public string Remarks1 { get; set; }
        public string phoneOrder { get; set; }
        
    }

    [WebMethod(EnableSession = true)]
    public static OrderTpEc[] GetPrimaryVal(string SF_Code, string FYera, string FMonth)
    {
        List<OrderTpEc> empList = new List<OrderTpEc>();
        Order tpn = new Order();
        string div_code = "";

        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        DataSet dsPro = tpn.GetPrmaryDevaition(SF_Code, FYera, FMonth, div_code);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            OrderTpEc emp = new OrderTpEc();
            emp.sfCode = row["sf_code"].ToString();
            emp.Activity_Date = Convert.ToDateTime(row["ModTime"]).ToString("yyyy/MM/dd");
            emp.Order_Value = row["POB_Value"].ToString();
            emp.WorkType_Name = row["Stockist_Name"].ToString();
            emp.SplCode = row["Trans_Detail_Info_Code"].ToString();
            emp.SplName = row["Worked_with_Name"].ToString();
            emp.route = row["SDP_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class OrderDistri
    {
        public string sfCode { get; set; }
        public string months { get; set; }
        public string cnts { get; set; }
        public string Order_Value { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static OrderDistri[] GetDataDistributo(string SF_Code, string FYera, string FMonth)
    {
        List<OrderDistri> empList = new List<OrderDistri>();
        Order tpn = new Order();
        DataSet dsPro = tpn.GetDistributorDeviation(SF_Code, FYera, FMonth);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            OrderDistri emp = new OrderDistri();
            emp.sfCode = row["sf_code"].ToString();
            emp.Order_Value = row["orderVal"].ToString();
            emp.months = row["mnth"].ToString();
            emp.cnts = row["cnt"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }
    public class SFList
    {
        public string sfCode { get; set; }
        public string sfname { get; set; }

    }


    [WebMethod(EnableSession = true)]
    public static SFList[] GetSFList(string SF_Code)
    {
        
        string DivCode = "";
        try
        {
            DivCode = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            DivCode = HttpContext.Current.Session["Division_Code"].ToString();
        }


        string SubDiv = "0";
        List<SFList> empList = new List<SFList>();
        SalesForce sf = new SalesForce();
        DataSet dsPro = sf.SalesForceList(DivCode, SF_Code, SubDiv);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            SFList emp = new SFList();
            emp.sfCode = row["sf_Code"].ToString();
            emp.sfname = row["sf_Name"].ToString();

            empList.Add(emp);
        }
        return empList.ToArray();
    }
}
