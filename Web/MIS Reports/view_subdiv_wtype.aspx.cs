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


public partial class MIS_Reports_view_subdiv_wtype : System.Web.UI.Page
{
    public static string sfc = string.Empty;
    public static string dt = string.Empty;
    public static string divcode = string.Empty;
    public static string sf_type = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
         sfc = Request.QueryString["SFCode"].ToString();
         dt = Request.QueryString["Dates"].ToString();
         divcode = Session["div_code"].ToString();
		 DateTime d1 = Convert.ToDateTime(dt);
         lblHead.Text="Division Wise Worktype" + " " + d1.ToString("dd-MM-yyyy");
    }
    [WebMethod(EnableSession = true)]
    public static string getdata()
    { 
        subd dv = new subd();
        DataTable ds = new DataTable();
        ds = dv.get_secordervalconfirmation();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string getdatalist(string subdiv, string wtyp)
    {
        subd dv = new subd();
        DataTable ds = new DataTable();
        ds = dv.getdatalistwtype(subdiv, wtyp);
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string getdatalistnl(string subdiv, string wtyp)
    {
        subd dv = new subd();
        DataTable ds = new DataTable();
        ds = dv.getdatalistwtypenl(subdiv, wtyp);
        return JsonConvert.SerializeObject(ds);
    }
    public class subd
    {
        
        public DataTable get_secordervalconfirmation()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            var strQry = "exec [subdiv_wtype] '" + sfc + "','" + divcode + "','" + dt + "'";
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

            var strQry = "exec [subdiv_wtype_list] '" + sfc + "','" + divcode + "','" + dt + "','" + wtyp + "','" + subdiv + "'";
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

            var strQry = "exec [subdiv_wtype_list_nlog] '" + sfc + "','" + divcode + "','" + dt + "','" + wtyp + "','" + subdiv + "'";
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