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

public partial class MIS_Reports_rpt_primarySTKwise_targetVSsale : System.Web.UI.Page
{
    public static string sfcode1=string.Empty;
    public static string fmonth = string.Empty;
    public static string Tmonth = string.Empty;
    public static string FYear = string.Empty;
    public static string TYear = string.Empty;
    public static string subDiv = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        sfcode1 = Request.QueryString["code"].ToString();
        fmonth = Request.QueryString["fmonth"].ToString();
        Tmonth = Request.QueryString["Tmonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        subDiv = Request.QueryString["subDiv"].ToString();
        dsf.Value = Request.QueryString["code"].ToString(); 
   	txtsfnane.Text = Convert.ToString(Request.QueryString["SF_Name"]);      
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
         target = pro.distributertarget(div_code, sfcode1, fmonth, FYear, Tmonth, TYear);
        if (target.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in target.Tables[0].Rows)
            {
                targetclass tc = new targetclass();
                tc.Stockist_code = row["Stockist_code"].ToString();
               tc.Stockist_Name = row["Stockist_Name"].ToString();
               tc.qnty = row["qnty"].ToString();
               tc.targ = row["target"].ToString();
                tc.monyear = row["monyear"].ToString();
				tc.Sf_Name = row["Sf_Name"].ToString();
                tctarget.Add(tc);
            }
        }
        return tctarget.ToArray();
    }
    public class targetclass
    {
      public  string Stockist_code { get; set; }
        public string Stockist_Name { get; set; }
        public string qnty { get; set; }
        public string targ { get; set; }
        public string monyear { get; set; }
		public string Sf_Name { get; set;}
    }
    [WebMethod]
    public static stockist[] stockisttarget()
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
        List<stockist> targetmon = new List<stockist>();
  
            target = pro.get_stockist_target(div_code, sfcode1, fmonth, FYear, Tmonth, TYear);
        if (target.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in target.Tables[0].Rows)
            {
                stockist tc = new stockist();
                tc.Stockist_code = row["Stockist_code"].ToString();
                tc.Stockist_Name = row["Stockist_Name"].ToString();              
                tc.monye = row["mon"].ToString();
                targetmon.Add(tc);
            }
        }
        return targetmon.ToArray();
    }
    public class stockist
    {
        public string Stockist_code { get; set; }
        public string Stockist_Name { get; set; }
        public string qnty { get; set; }
        public string targ { get; set; }
        public string monye { get; set; }
    }
}