using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Bus_EReport;
using System.Data;
using Newtonsoft.Json;
using System.Globalization;

public partial class MasterFiles_Reports_rptClosingStock : System.Web.UI.Page
{

     string sf_code = string.Empty;
    string Sf_Name = string.Empty;
    string fdts = string.Empty;
    string tdts = string.Empty;
    string fdt = string.Empty;
    string tdt = string.Empty;
    Int16 cmonth = -1;
    Int16 cyear = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Request.QueryString["sf_code"].ToString();
        Sf_Name = Request.QueryString["Sf_Name"].ToString();
        cmonth = Convert.ToInt16(Request.QueryString["cur_month"].ToString());
        cyear = Convert.ToInt16(Request.QueryString["cur_year"].ToString());
        fdts = Request.QueryString["FDate"].ToString();
        tdts = (Request.QueryString["TDate"].ToString() == "") ? tdt : Request.QueryString["TDate"].ToString();

        DateTime result4 = DateTime.ParseExact(fdts, "d/MM/yyyy", CultureInfo.InvariantCulture);
        fdt = result4.ToString("yyyy-MM-dd");

        DateTime result10 = DateTime.ParseExact(tdts, "d/MM/yyyy", CultureInfo.InvariantCulture);
        tdt = result10.ToString("yyyy-MM-dd");
        hfmonth.Value = tdt.ToString();
        hfyear.Value = fdt.ToString();
        hsfcode.Value = sf_code;
        hsubdiv.Value = "0";
        //  subhead.Text = " Closing Stock View for the Month of" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(cmonth);
        subhead.Text = "Distributor Closing Stock Report for the Month of  " + fdt + " To " + tdt;
    }


    public class TotOrder
    {
        public string pCode { get; set; }
        public string pName { get; set; }
        public string distName { get; set; }
        public string distCode { get; set; }
        public string cQTY { get; set; }
        public string pQTY { get; set; }
        public string St_code { get; set; }
        public string Pln_Date { get; set; }

        public string TPieces { get; set; }
        public string StateName { get; set; }
        public string Sf_HQ { get; set; }
        public string SF_Name { get; set; }
        public string Stockist_Name { get; set; }
        public string Stockist_Erp_Code { get; set; }
        public string imgurl { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static string GetAllData(string SF_Code, string fdate, string tdate, string SubDiv)
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
        DCR sf = new DCR();
        DataSet dsSalesForce = sf.tGet_Close_stock_MR(SF_Code, div_code, fdate, tdate);
        return JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
    }

}