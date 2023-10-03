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


public partial class MIS_Reports_manpower_dashb : System.Web.UI.Page
{
    public static string sfc = string.Empty;
    public static string Sf_Name = string.Empty;
    public static string dt = string.Empty;
    public static string divcode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfc = Request.QueryString["sf_code"].ToString();
        Sf_Name = Request.QueryString["Sf_Name"].ToString();
        dt = Request.QueryString["Date"].ToString();
        divcode = Session["div_code"].ToString();
        lblHead.Text = Sf_Name;
    }
    [WebMethod(EnableSession = true)]
    public static string actcday()
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.get_actcday();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string actcmonth()
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.get_actcmonth();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string actcyear()
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.get_actcyear();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string uniactcmnth()
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.get_unimonth();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string uniactcyear()
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.get_uniyear();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string jwday()
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.get_jwday();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string jwmonth()
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.get_jwmonth();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string jwyear()
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.get_jwyear();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string vsday()
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.get_vsday();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string vsmonth()
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.get_vsmonth();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string vsyear()
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.get_vsyear();
        return JsonConvert.SerializeObject(ds);
    }
      
 [WebMethod(EnableSession = true)]
    public static string catday()
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.get_catday();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string catmonth()
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.get_catmonth();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string catyear()
    {
        desn dv = new desn();
        DataTable ds = new DataTable();
        ds = dv.get_catyear();
        return JsonConvert.SerializeObject(ds);
    }
    public class desn
    {

        public DataTable get_actcday()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [mandash_actc] '" + sfc + "','" + divcode + "','" + dt + "'";
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
        public DataTable get_actcmonth()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [mandash_actc_month] '" + sfc + "','" + divcode + "','" + dt + "'";
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
        public DataTable get_actcyear()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [mandash_actc_year] '" + sfc + "','" + divcode + "','" + dt + "'";
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
        public DataTable get_unimonth()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [unique_actc_mnth] '" + sfc + "','" + divcode + "','" + dt + "'";
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
        public DataTable get_uniyear()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [unique_actc_year] '" + sfc + "','" + divcode + "','" + dt + "'";
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
        public DataTable get_jwday()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [jointwrk_daycall] '" + sfc + "','" + divcode + "','" + dt + "'";
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
        public DataTable get_jwmonth()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [jointwrk_monthcall] '" + sfc + "','" + divcode + "','" + dt + "'";
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
        public DataTable get_jwyear()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [jointwrk_yearcall] '" + sfc + "','" + divcode + "','" + dt + "'";
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
        public DataTable get_vsday()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [vansales_dayval] '" + sfc + "','" + divcode + "','" + dt + "'";
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
        public DataTable get_vsmonth()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [vansales_mnthval] '" + sfc + "','" + divcode + "','" + dt + "'";
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
        public DataTable get_vsyear()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [vansales_yearval] '" + sfc + "','" + divcode + "','" + dt + "'";
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
        public DataTable get_catday()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [prod_dayval] '" + sfc + "','" + divcode + "','" + dt + "'";
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
        public DataTable get_catmonth()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [prod_mnthval] '" + sfc + "','" + divcode + "','" + dt + "'";
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
        public DataTable get_catyear()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [prod_yearval] '" + sfc + "','" + divcode + "','" + dt + "'";
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