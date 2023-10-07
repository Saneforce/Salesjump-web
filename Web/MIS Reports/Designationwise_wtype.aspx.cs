using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using DBase_EReport;


public partial class MIS_Reports_Designationwise_wtype : System.Web.UI.Page
{
    public static string sfc = string.Empty;
    public static string mnth = string.Empty;
    public static string year = string.Empty;
    public static string sfnm = string.Empty;
    public static string dt = string.Empty;
    public static string divcode = string.Empty;
    public static string sf_type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
          sfc = Request.QueryString["sf_code"].ToString();
        mnth = Request.QueryString["cur_month"].ToString();
        year = Request.QueryString["cur_year"].ToString();
        sfnm = Request.QueryString["Sf_Name"].ToString();
        dt = Request.QueryString["Date"].ToString();
         divcode = Session["div_code"].ToString();
         DateTime d1 = Convert.ToDateTime(dt);
         lblHead.Text = "Designation Wise Worktype" + " " + d1.ToString("dd-MM-yyyy");
        sfcod.Value = sfc;
        mnthv.Value = mnth;
        yr.Value = year;
        sfn.Value = sfnm;
        dte.Value = dt;
    }
    [WebMethod(EnableSession = true)]
    public static string getdata()
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.get_design_wtype();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string getdatalist(string subdiv, string wtyp)
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.getdatalistwtype(subdiv, wtyp);
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string getdatalistnl(string subdiv, string wtyp)
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.getdatalistwtypenl(subdiv, wtyp);
        return JsonConvert.SerializeObject(ds);
    }
    public class desn
    {

        public DataTable get_design_wtype()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string  strQry = "exec [design_wtype] '" + sfc + "','" + divcode + "','" + dt + "'";
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataTable getdatalistwtype(string subdiv, string wtyp)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [design_wtype_list] '" + sfc + "','" + divcode + "','" + dt + "','" + wtyp + "','" + subdiv + "'";
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataTable getdatalistwtypenl(string subdiv, string wtyp)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [design_wtype_listnotlg] '" + sfc + "','" + divcode + "','" + dt + "','" + wtyp + "','" + subdiv + "'";
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
    }
}