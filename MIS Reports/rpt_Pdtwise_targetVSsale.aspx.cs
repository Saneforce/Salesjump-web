using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Web.Services;
using System.Configuration;
using System.Data;
public partial class MIS_Reports_rpt_Pdtwise_targetVSsale : System.Web.UI.Page
{
    public static string sfcode = string.Empty;
    public static string fmonth = string.Empty;
    public static string Tmonth = string.Empty;
    public static string FYear = string.Empty;
    public static string TYear = string.Empty;
    public static string st_code = string.Empty;
    public static string subdiv = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        st_code = Request.QueryString["st_code"].ToString();
        sfcode = Request.QueryString["Sf_Code"].ToString();
        subdiv = Request.QueryString["subDiv"].ToString();
        fmonth = Request.QueryString["fmonth"].ToString();
        Tmonth = Request.QueryString["Tmonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        txtsfnane.Text = Convert.ToString(Request.QueryString["SF_Name"]);
        Lblstname.Text = Convert.ToString(Request.QueryString["st_Name"]);
        txtmonyear.Text = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt16(fmonth)) + " - " + FYear + "  To  " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt16(Tmonth)) + " - " + TYear;

    }
    [WebMethod]
    public static targetclass[] target_detail()
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
        Product pro = new Product();
        DataSet target = null;
        List<targetclass> tctarget = new List<targetclass>();
       
        target = pro.pro_target_detail(div_code, sfcode, fmonth, FYear, Tmonth, TYear, st_code);
        if (target.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in target.Tables[0].Rows)
            {
                targetclass tc = new targetclass();
                tc.Stockist_code = row["Stockist_code"].ToString();
                //tc.Stockist_Name = row["Stockist_Name"].ToString();
                tc.CQty = row["CQty"].ToString();
                tc.targ = row["Target_Qnty"].ToString();
                tc.tval = row["tqty"].ToString();
                tc.value = row["value"].ToString();
                tc.p_name = row["Product_Detail_Name"].ToString();
                tc.P_code = row["Product_code"].ToString();
                tc.monyear = row["monyear"].ToString();
                tctarget.Add(tc);
            }
        }
        return tctarget.ToArray();
    }
    public class targetclass
    {
        public string Stockist_code { get; set; }
        public string Stockist_Name { get; set; }
        public string tval { get; set; }
        public string targ { get; set; }
        public string value { get; set; }
        public string CQty { get; set; }
        public string p_name { get; set; }
        public string P_code { get; set; }
        public string monyear { get; set; }
    }
    [WebMethod]
    public static stockist[] monthtarget()
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
        Product pro = new Product();
         DataSet target=null;
        List<stockist> targetmon = new List<stockist>();
   
            target = pro.dis_ProMonth_detail(div_code, sfcode, fmonth, FYear, Tmonth, TYear, st_code);

            if (target.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in target.Tables[0].Rows)
            {
                stockist tc = new stockist();
                tc.p_code = row["Product_Detail_Code"].ToString();
                tc.p_Name = row["Product_Detail_Name"].ToString();
                tc.monye = row["monyear"].ToString();
                targetmon.Add(tc);
            }
        }
        return targetmon.ToArray();
    }
    public class stockist
    {
        public string p_code { get; set; }
        public string p_Name { get; set; }
        public string qnty { get; set; }
        public string targ { get; set; }
        public string monye { get; set; }
    }
}