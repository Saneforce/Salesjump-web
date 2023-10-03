using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;
using iTextSharp.tool.xml;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.Xml;
using DBase_EReport;

public partial class MIS_Reports_viewrcpa_report : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv_code = string.Empty;
    public static string FDate = string.Empty;
    public static string TDate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        TDate = Request.QueryString["ToDate"].ToString();
        FDate = Request.QueryString["FromDate"].ToString();
        DateTime d1 = Convert.ToDateTime(FDate);
        DateTime d2 = Convert.ToDateTime(TDate);
        lblHead.Text = "Competitor Analysis From " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
		   lblsf_name.Text = sfname;
     }
    [WebMethod(EnableSession = true)]
    public static string Filldtl()
    {
        viewrc sf = new viewrc();
        DataSet dsProd = sf.Get_rcpa_prod();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Fillqtydtl()
    {
        viewrc sf = new viewrc();
        DataSet dsProd = sf.Get_rcpa_prodqty();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    public class viewrc
    {
        public DataSet Get_rcpa_prod()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
           string strQry = "exec GetrcpaDetails '" + divcode + "','" + Sf_Code + "','" + FDate + "','" + TDate + "','" + subdiv_code + "'";
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
        public DataSet Get_rcpa_prodqty()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "exec Getrcpa_com_qty '" + divcode + "','" + Sf_Code + "','" + FDate + "','" + TDate + "','" + subdiv_code + "'";
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
    }
}