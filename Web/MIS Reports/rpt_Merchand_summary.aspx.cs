using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_rpt_Merchand_summary : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv_code = string.Empty;
    public static string fdate = string.Empty;
    public static string tdate = string.Empty;
    DataSet ff = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        fdate = Request.QueryString["Month"].ToString();
        tdate = Request.QueryString["Year"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        Sf_Code = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        lblHead.Text = "Merchand Summary " + fdate + " to " + tdate;
        Feild.Text = sfname;
    }

    [WebMethod]
    public static string getdatalist()
    {
        tst sd = new tst();
        DataSet ds = new DataSet();
        ds = sd.get_details(divcode, fdate, tdate, Sf_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public class tst
    {
        public DataSet get_details(string divcode, string month, string year, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            string strQry = "exec sp_merchand_rpt '" + Sf_Code + "','" + month + "','" + year + "','" + divcode + "'";

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