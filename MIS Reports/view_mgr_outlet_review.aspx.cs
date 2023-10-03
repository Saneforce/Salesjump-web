using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using System.Text;
using Bus_EReport;
using System.Net;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;
public partial class MIS_Reports_view_mgr_outlet_review : System.Web.UI.Page
{
    public string stat = string.Empty;
    public string statv = string.Empty;
    public string div = string.Empty;
    public string fdt = string.Empty;
    public string sf_code = string.Empty;
    public static string sfname = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string div_code = string.Empty;
    public static string FDate = string.Empty;
    public static string TDate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfname = Request.QueryString["mgrnm"].ToString();
        Sf_Code = Request.QueryString["mgrval"].ToString();
        div_code = Request.QueryString["divcode"].ToString();
        TDate = Request.QueryString["tdt"].ToString();
        FDate = Request.QueryString["fdt"].ToString();
        DateTime d1 = Convert.ToDateTime(FDate);
        DateTime d2 = Convert.ToDateTime(TDate);
        lblHead.Text = "Manager Outlet Review from " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
        lblsf_name.Text = sfname;
    }
    [WebMethod(EnableSession = true)]
    public static string Filldtl()
    {
        mgrot sf = new mgrot();
        DataSet dsProd = sf.Get_mgroutlet();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Filldtltime()
    {
        mgrot sf = new mgrot();
        DataSet dsProd = sf.Get_mgroutlet_time();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {

        DataTable dsProd1 = null;
        mgrot LstDoc = new mgrot();
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "MGR_outlet_Rpt.xls"));
            Response.ContentType = "application/ms-excel";
            dsProd1 = LstDoc.Get_mgroutlet_excl();
            DataTable dt = dsProd1;

            string str = string.Empty;
            foreach (DataColumn dtcol in dt.Columns)
            {
                Response.Write(str + dtcol.ColumnName);
                str = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in dt.Rows)
            {
                str = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Response.Write(str + Convert.ToString(dr[j]));
                    str = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
        }
    }
    public class mgrot
    {
        public DataSet Get_mgroutlet()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "exec manager_outlet_rpt '" + Sf_Code + "','" + div_code + "','" + FDate + "','" + TDate + "'";
            //string strQry = "exec manager_wrking_rpt 'Sf_Code','100','2021-10-20' ,'2021-10-30'";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataSet Get_mgroutlet_time()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "exec manager_outlet_rpt_time '" + Sf_Code + "','" + div_code + "','" + FDate + "','" + TDate + "'";
            //string strQry = "exec manager_wrking_rpt 'Sf_Code','100','2021-10-20' ,'2021-10-30'";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataTable Get_mgroutlet_excl()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec manager_outlet_rpt_excl '" + Sf_Code + "','" + div_code + "','" + FDate + "','" + TDate + "'";
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