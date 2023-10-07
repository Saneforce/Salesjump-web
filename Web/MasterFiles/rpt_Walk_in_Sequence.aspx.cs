using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;
using iTextSharp.tool.xml;

using System.Web.Services;
using System.Globalization;
using Newtonsoft.Json;


public partial class MasterFiles_rpt_Walk_in_Sequence : System.Web.UI.Page
{
    #region "Declaration"
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    decimal iTotLstCount2 = 0;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string stype = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;

    DataSet dsSalesForce = new DataSet();

    DataSet dsMGR = new DataSet();
    DataSet dsMr = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string tot_dr = string.Empty;
    string tot_Drr = string.Empty;

    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;

    DataSet dsprd = new DataSet();
    string sCurrentDate = string.Empty;
    string stockist_code = string.Empty;
    string stURL = string.Empty;
    string Stock_name = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["SF_Code"].ToString();
        // FYear = Request.QueryString["FYear"].ToString();
        hidn_sf_code.Value = sfCode;
        // hFYear.Value = Request.QueryString["FYear"].ToString();
        //  hFMonth.Value = Request.QueryString["FMonth"].ToString();
        //  hTYear.Value = Request.QueryString["TYear"].ToString();
        // hTMonth.Value = Request.QueryString["TMonth"].ToString();
        //   hTMonth.Value = Request.QueryString["TMonth"].ToString();
        lblsf_name.Text = Request.QueryString["SF_Name"].ToString();

        //DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        //string strFmonth = mfi.GetMonthName(Convert.ToInt32(Request.QueryString["FMonth"].ToString())).ToString(); //August

        //string strTmonth = mfi.GetMonthName(Convert.ToInt32(Request.QueryString["TMonth"].ToString())).ToString(); //August



        //lblyear.Text = strFmonth.Substring(0, 3) + " - " + Request.QueryString["FYear"].ToString() + " To " + strTmonth.Substring(0, 3) + " - " + Request.QueryString["TYear"].ToString(); ;
    }
    public class Routes
    {
        public string rName { get; set; }
        public string rCode { get; set; }

    }
    [WebMethod(EnableSession = true)]
    public static Routes[] getRoutes(string SF_Code)
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
        RoutePlan rop = new RoutePlan();
        DataSet DsRoute = rop.get_Route_Name(div_code, SF_Code);
        List<Routes> vList = new List<Routes>();
        foreach (DataRow row in DsRoute.Tables[0].Rows)
        {
            Routes vl = new Routes();
            vl.rName = row["Territory_Name"].ToString();
            vl.rCode = row["Territory_Code"].ToString();
            vList.Add(vl);
        }
        return vList.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static Retailertargets[] getCustomerSequance(string SF_Code)
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
        ListedDR ldr = new ListedDR();
        DataSet DsRetailer = ldr.Get_Retailer_Walk_in_Sequance(SF_Code);

        List<Retailertargets> vList = new List<Retailertargets>();
        foreach (DataRow row in DsRetailer.Tables[0].Rows)
        {
            Retailertargets vl = new Retailertargets();
            vl.rCode = row["Territory_Code"].ToString();
            vl.cName = row["ListedDr_Name"].ToString();
            vl.cCode = row["ListedDrCode"].ToString();
            vl.sequnaceNo = row["ListedDr_Sl_No"].ToString();
            vList.Add(vl);
        }
        return vList.ToArray();
    }

    public class Retailertargets
    {
        public string rCode { get; set; }
        public string cName { get; set; }
        public string cCode { get; set; }
        public string sequnaceNo { get; set; }

    }

    [WebMethod]
    public static string SaveSequence(string Data)
    {
        var items = JsonConvert.DeserializeObject<List<Retailertargets>>(Data);
        int count = 0;
        ListedDR drd = new ListedDR();
        int iReturn = -1;
        for (int i = 0; i < items.Count; i++)
        {
            iReturn = drd.Retailer_Walk_in_sequance_Update(items[i].cCode.ToString(), items[i].sequnaceNo.ToString());
            count++;
        }
        if (count == 0)
        {
            return "Fail";
        }
        else
        {
            return "Suucess";
        }

        return items.Count.ToString();
    }
}