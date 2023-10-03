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
public partial class MIS_Reports_RptAllenSuperStockList : System.Web.UI.Page
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
        //cmonth = Convert.ToInt16(Request.QueryString["cur_month"].ToString());
        //cyear = Convert.ToInt16(Request.QueryString["cur_year"].ToString());
        fdts = Request.QueryString["FDate"].ToString();
        tdts = (Request.QueryString["TDate"].ToString() == "") ? tdt : Request.QueryString["TDate"].ToString();

        //DateTime result4 = DateTime.ParseExact(fdts, "d/MM/yyyy", CultureInfo.InvariantCulture);

        DateTime result4 = DateTime.ParseExact(fdts, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        fdt = result4.ToString("yyyy-MM-dd");

        //DateTime result10 = DateTime.ParseExact(tdts, "d/MM/yyyy", CultureInfo.InvariantCulture);

        DateTime result10 = DateTime.ParseExact(tdts, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        tdt = result10.ToString("yyyy-MM-dd");
        hfmonth.Value = tdt.ToString();
        hfyear.Value = fdt.ToString();
        hsfcode.Value = sf_code;
        hsubdiv.Value = "0";
        //  subhead.Text = " Closing Stock View for the Month of" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(cmonth);
        subhead.Text = "Distributor Super Stock Report for the Month of  " + fdt + " To " + tdt;
    }

    [WebMethod(EnableSession = true)]
    public static string GetAllData(string SF_Code, string fdate, string tdate)
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
        DataSet dsSalesForce = sf.tGet_Super_stock_MR(SF_Code, div_code, fdate, tdate);
        return JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
    }
}