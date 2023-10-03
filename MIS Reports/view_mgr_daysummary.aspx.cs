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
public partial class MIS_Reports_view_mgr_daysummary : System.Web.UI.Page
{
   
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
        lblHead.Text = "Manager Day Summary Report from " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
		lblsf_name.Text = sfname;
    }
    [WebMethod(EnableSession = true)]
    public static string Filldtl()
    {
        mgrsum sf = new mgrsum();
        DataSet dsProd = sf.Get_mgrsum_report();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {

        DataTable dsProd1 = null;
        mgrsum LstDoc = new mgrsum();
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "MGR_Day_summary.xls"));
            Response.ContentType = "application/ms-excel";
            dsProd1 = LstDoc.Get_mgrsum_report_excl();
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
    public class mgrsum
    {
        public DataSet Get_mgrsum_report()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "exec manager_daysum_rpt '" + Sf_Code + "','" + div_code + "','" + FDate + "','" + TDate + "'";
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
        public DataTable Get_mgrsum_report_excl()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec manager_daysum_rpt_excel '" + Sf_Code + "','" + div_code + "','" + FDate + "','" + TDate + "'";
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