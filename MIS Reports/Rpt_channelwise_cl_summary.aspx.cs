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
public partial class MIS_Reports_Rpt_channelwise_cl_summary : System.Web.UI.Page
{
    public string div = string.Empty;
    public string fdt = string.Empty;
    public static string sfname = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string div_code = string.Empty;
    public static string subdiv = string.Empty;
    public static string FDate = string.Empty;
    public static string TDate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfname = Request.QueryString["Sf_Name"].ToString();
        Sf_Code = Request.QueryString["sf_code"].ToString();
        div_code = Request.QueryString["div_code"].ToString();
        subdiv = Request.QueryString["subdiv"].ToString();
        TDate = Request.QueryString["ToDate"].ToString();
        FDate = Request.QueryString["FromDate"].ToString();
        DateTime d1 = Convert.ToDateTime(FDate);
        DateTime d2 = Convert.ToDateTime(TDate);
        lblHead.Text = "Channelwise Summary Report from " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
		 lblsf_name.Text = sfname;
    }
    [WebMethod(EnableSession = true)] 
    public static string Filltotcl()
    {
        chnl sf = new chnl();
        DataSet dsProd = sf.Get_Filltotcl();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Fillprodcl()
    {
        chnl sf = new chnl();
        DataSet dsProd = sf.Get_Fillprodcl();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Fillchannel()
    {
        SalesForce sf = new SalesForce();
        DataSet dsProd = sf.Getchannelwise(div_code);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Fillorderval()
    {
        chnl sf = new chnl();
        DataSet dsProd = sf.Getorderval();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    public class chnl
    {

        public DataSet Get_Filltotcl()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "exec [getclannelwise_totcl] '" + Sf_Code + "','" + div_code + "','" + FDate + "','" + TDate + "','" + subdiv + "'"; 
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
        public DataSet Getorderval()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "exec [getclannelwise_orderval] '" + Sf_Code + "','" + div_code + "','" + FDate + "','" + TDate + "','" + subdiv + "'";
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
        public DataSet Get_Fillprodcl()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "exec [getclannelwise_Retailer_Count] '" + Sf_Code + "','" + div_code + "','" + FDate + "','" + TDate + "','" + subdiv + "'";
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