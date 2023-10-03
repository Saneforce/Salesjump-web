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

public partial class MIS_Reports_tpdeviation_maploc : System.Web.UI.Page
{
    string Sf_code = string.Empty;
    string month = string.Empty;
    string month1 = string.Empty;
    string year = string.Empty;
    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = div_code = Session["div_code"].ToString();
        Sf_code = Request.QueryString["SFCode"].ToString();
        month1 = Request.QueryString["FDate"].ToString();
        string abbreviatedMonth = month1;
        DateTime date = DateTime.ParseExact(abbreviatedMonth, "MMM", CultureInfo.InvariantCulture);
        string month = date.ToString("MM");
        year = Request.QueryString["FYear"].ToString();
       // month = "08";
        hSfCode.Value = Sf_code;
        hFmonth.Value = month;
        hFyear.Value = year;
    }
    [WebMethod]
    public static string GetTaggedRetdate(string SFCode,string div_code,string month ,string year)
    {
        DB_EReporting db = new DB_EReporting();
       // string Sf_code = Sf_code;
        DataSet ds = db.Exec_DataSet("exec Taggedret_date_vis  '" + SFCode + "','" + div_code + "','" + month + "','"+ year + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}