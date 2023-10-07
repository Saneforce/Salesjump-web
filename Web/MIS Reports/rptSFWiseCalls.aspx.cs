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
using System.Collections;
public partial class MIS_Reports_rptSFWiseCalls : System.Web.UI.Page
{
    #region "Declaration"
    string DivCode = string.Empty;
    string SFCode = string.Empty;
    string SubDiceCode = string.Empty;
    string FDate = string.Empty;
    DataSet getweek = null;
    DataSet getweekno = null;
    DataSet getweekdays = null;
    DataSet gg = null;
    DataSet dsSalesForce = new DataSet();
    DataSet dsMGR = new DataSet();
    DataSet dsMr = new DataSet();

    DataSet dsDoc = null;
    string strFMonthName = string.Empty;
    string g = string.Empty;
    int gw = 0;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string sURL = string.Empty;
    string SFName = string.Empty;
    string monthname = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        DivCode = Session["Div_Code"].ToString();
        SFCode = Request.QueryString["SFCode"].ToString();
        SFName = Request.QueryString["SFName"].ToString();
        SubDiceCode = Request.QueryString["SubDiv"].ToString();
       
        FDate = Request.QueryString["SFName"].ToString();
        HFDate.Value = Request.QueryString["FDate"].ToString();
        lblsfname.Text = "Team :- " + SFName;
        HSFCode.Value = SFCode;
        HsTime.Value = Request.QueryString["sTime"].ToString();
        HeTime.Value = Request.QueryString["eTime"].ToString();
        HtSlot.Value = Request.QueryString["tSlot"].ToString();
        HSubDive.Value = SubDiceCode;
        lblHead.Text = "CALLS for Date " + Request.QueryString["FDate"].ToString();
    }

    public class sfNames
    {
        public string Sf_Code { get; set; }
        public string sf_Name { get; set; }
        public string TC { get; set; }
        public string EC { get; set; }
        public string Timestamp { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static sfNames[] GetSalesforce(string sfCode, string SubDiv)
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
        dsSFHead = sf.SalesForceList(div_code, sfCode, SubDiv);
        List<sfNames> dsDtls = new List<sfNames>();
        foreach (DataRow row in dsSFHead.Tables[0].Rows)
        {
            sfNames dtls = new sfNames();
            dtls.Sf_Code = row["sf_code"].ToString();
            dtls.sf_Name = row["sf_name"].ToString();         
            dsDtls.Add(dtls);
        }
        return dsDtls.ToArray();
    }
    [WebMethod(EnableSession = true)]
    public static sfNames[] GetTCData(string sfCode, string SubDiv,string FDates)
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
        Order rd = new Order();
        DataSet dsSFHead = new DataSet();
        dsSFHead = rd.GetSFCalls(div_code, sfCode, Convert.ToDateTime(FDates).ToString("yyyy/MM/dd"), SubDiv);
        List<sfNames> dsDtls = new List<sfNames>();
        foreach (DataRow row in dsSFHead.Tables[0].Rows)
        {
            sfNames dtls = new sfNames();
            dtls.Sf_Code = row["sf_code"].ToString();
            dtls.TC = row["TC"].ToString();
            dtls.EC = row["EC"].ToString();
            dtls.Timestamp = row["Timestamp"].ToString();            
            dsDtls.Add(dtls);
        }
        return dsDtls.ToArray();
    }
}