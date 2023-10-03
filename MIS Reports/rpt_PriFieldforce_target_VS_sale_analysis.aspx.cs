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
public partial class MIS_Reports_rpt_PriFieldforce_target_VS_sale_analysis : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string divcode = string.Empty;
    
 
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["SF_Code"].ToString();
        // FYear = Request.QueryString["FYear"].ToString();
        hidn_sf_code.Value = sfCode;
        hFYear.Value = Request.QueryString["FYear"].ToString();
        hFMonth.Value = Request.QueryString["FMonth"].ToString();
        hTYear.Value = Request.QueryString["TYear"].ToString();
        hTMonth.Value = Request.QueryString["TMonth"].ToString();
        hTMonth.Value = Request.QueryString["TMonth"].ToString();
        lblsf_name.Text = Request.QueryString["SF_Name"].ToString();

        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        string strFmonth = mfi.GetMonthName(Convert.ToInt32(Request.QueryString["FMonth"].ToString())).ToString(); //August

        string strTmonth = mfi.GetMonthName(Convert.ToInt32(Request.QueryString["TMonth"].ToString())).ToString(); //August



        lblyear.Text = strFmonth.Substring(0, 3) + " - " + Request.QueryString["FYear"].ToString() + " To " + strTmonth.Substring(0, 3) + " - " + Request.QueryString["TYear"].ToString(); 
    }
    [WebMethod(EnableSession = true)]
    public static Retailertargets[] getFieldForceSales(string SF_Code, string FYear, string FMonth, string TYear, string TMonth)
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
        DataSet DsRetailer = ldr.Get_FieldForce_Target_vs_Sal(SF_Code, FYear, FMonth, TYear, TMonth, div_code);

        List<Retailertargets> vList = new List<Retailertargets>();
        foreach (DataRow row in DsRetailer.Tables[0].Rows)
        {
            Retailertargets vl = new Retailertargets();
            vl.SF_Name = row["SF_Name"].ToString();
            vl.SF_Code = row["SF_Code"].ToString();
            vl.cmonth = row["cmonth"].ToString();
            vl.cyear = row["cyear"].ToString();
            vl.order_val = row["order_val"].ToString();
            vl.target_val = row["target_val"].ToString();           
            vList.Add(vl);
        }
        return vList.ToArray();
    }

    public class Retailertargets
    {
        public string SF_Name { get; set; }
        public string SF_Code { get; set; }
        public string cmonth { get; set; }
        public string cyear { get; set; }
        public string order_val { get; set; }
        public string target_val { get; set; }      
    }
    [WebMethod(EnableSession = true)]
    public static string GetFF(string ddlFieldForce)
    {
        string StkCode = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsFF = new DataSet();
        SalesForce sf = new SalesForce();
        dsFF = sf.UserList_getMR(Div_Code, ddlFieldForce);
        return Newtonsoft.Json.JsonConvert.SerializeObject(dsFF.Tables[0]);

    }
}