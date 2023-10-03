using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;

public partial class MIS_Reports_view_countofrepeatshop : System.Web.UI.Page
{
    public static string productcode = string.Empty;
    public static string year = string.Empty;
    public static string month = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        productcode = Request.QueryString["productcode"].ToString();
        year = Request.QueryString["year"].ToString();
        month = Request.QueryString["month"].ToString();
        if (month == "Jan") { month = "1"; }; if (month == "Jul") { month = "7"; };
        if (month == "Feb") { month = "2"; }; if (month == "Aug") { month = "8"; };
        if (month == "Mar") { month = "3"; }; if (month == "Sep") { month = "9"; };
        if (month == "Apr") { month = "4"; }; if (month == "Oct") { month = "10"; };
        if (month == "May") { month = "5"; }; if (month == "Nov") { month = "11"; };
        if (month == "Jun") { month = "6"; }; if (month == "Dec") { month = "12"; };
        lblHead.Text = "Repeated Billed Outlet View Details For " + Request.QueryString["month"].ToString() + "-" + year;
    }

    [WebMethod(EnableSession = true)]
    public static string getdatanshp()
    {
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sub_Div = "";//HttpContext.Current.Session["Sub_Div_code"].ToString();
        string SF_Code = HttpContext.Current.Session["SF_Code"].ToString();
        shopdt dv = new shopdt();
        DataSet dsProd = dv.Get_Product_shopcnt_rep_dtl(Div_code, Sub_Div, SF_Code, productcode, year);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    public class shopdt
    {
        public DataSet Get_Product_shopcnt_rep_dtl(string Div_Code, string Sub_Div, string SF_Code, string Product_Code, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;
            string strQry = "EXEC SKY_Analysis_shopcount_repeat_detail '" + Div_Code + "','" + Sub_Div + "','" + SF_Code + "','" + Product_Code + "','" + year + "','" + month + "'";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
    }

}