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


public partial class MIS_Reports_view_stock_movement : System.Web.UI.Page
{
    public static string Div = string.Empty;
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string discode = string.Empty;
    public static string disname = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public DateTime rdt;
    public DateTime sdt;

    protected void Page_Load(object sender, EventArgs e)
    {
        Div = Session["div_code"].ToString();
        sfcode = Request.QueryString["sfcode"].ToString(); 
        discode = Request.QueryString["stckcode"].ToString();
        disname = Request.QueryString["stckName"].ToString();
        FDT = Request.QueryString["Fdate"].ToString();
        TDT = Request.QueryString["Tdate"].ToString();
       
       // DateTime d1 = Convert.ToDateTime(FDT);
       // DateTime d2 = Convert.ToDateTime(TDT);
        lblHead.Text = "DB Stock Movement Report"; 
        lblsf_name.Text = disname;
    }
    [WebMethod]
    public static string getproduct()
    {
        stckm SFD = new stckm();
        DataSet ds = SFD.getproductall();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getsecdtl()
    {
        stckm SFD = new stckm();
        DataSet ds = SFD.get_secdtl();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getpridtl()
    {
        stckm SFD = new stckm();
        DataSet ds = SFD.get_pridtl();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public class stckm
    {
        public DataSet getproductall()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            string strQry = "exec [get_product] '" + sfcode + "'," + Div.TrimEnd(',') + "";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet get_secdtl()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            string strQry = "exec [stkmove_secorder] '" + Div + "','" + discode + "'";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet get_pridtl()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            string strQry = "exec [stkmove_priorder] '" + Div + "','" + discode + "'";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
    }
}