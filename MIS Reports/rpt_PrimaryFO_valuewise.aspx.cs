using Bus_EReport;
using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;

public partial class MIS_Reports_rpt_PrimaryFO_valuewise : System.Web.UI.Page
{
    public static string sfcode1 = string.Empty;
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
    public static stockist[] Userstarget()
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
        stockist pro = new stockist();
        DataSet target = null;
        List<stockist> targetmon = new List<stockist>();

        target = pro.get_stockist_target(div_code, sfcode1, fmonth, FYear, Tmonth, TYear);
        if (target.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in target.Tables[0].Rows)
            {
                stockist tc = new stockist();
                tc.SF_Code = row["SF_Code"].ToString();
                tc.Sf_Name = row["Sf_Name"].ToString();
                tc.monye = row["mon"].ToString();
                targetmon.Add(tc);
            }
        }
        return targetmon.ToArray();
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
        targetclass pro = new targetclass();
        DataSet target = null;
        List<targetclass> tctarget = new List<targetclass>();
        target = pro.distributertarget(div_code, sfcode1, fmonth, FYear, Tmonth, TYear);
        if (target.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in target.Tables[0].Rows)
            {
                targetclass tc = new targetclass();
                tc.Sf_Code = row["Sf_Code"].ToString();
                tc.Sf_Name = row["Sf_Name"].ToString();
                tc.qnty = row["qnty"].ToString();
                tc.targ = row["target"].ToString();
                tc.monyear = row["monyear"].ToString();
                tctarget.Add(tc);
            }
        }
        return tctarget.ToArray();
    }//[FOvalwise_target_detail]
    public class targetclass
    {
        public string Sf_Code { get; set; }
        public string Sf_Name { get; set; }
        public string qnty { get; set; }
        public string targ { get; set; }
        public string monyear { get; set; }
        public DataSet distributertarget(string divcode, string sf_code, string fmonths, string fyear, string Tmonths, string Tyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            
            string strQry = "exec FOvalwise_target_detail '" + fmonths + "','" + fyear + "','" + Tmonths + "','" + Tyear + "','" + sf_code + "','" + divcode + "'";
            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
    }
    public class stockist
    {
        public string SF_Code { get; set; }
        public string Sf_Name { get; set; }
        public string qnty { get; set; }
        public string targ { get; set; }
        public string monye { get; set; }

        public DataSet get_stockist_target(string divcode, string sf_code, string fmonths, string fyear, string Tmonths, string Tyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            
            string strQry = "exec get_FieldForce_target '" + fmonths + "','" + fyear + "','" + Tmonths + "','" + Tyear + "','" + sf_code + "','" + divcode + "'";
            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
    }

}