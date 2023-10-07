using Bus_EReport;
using DBase_EReport;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class MIS_Reports_retailer_visit_status : System.Web.UI.Page
{
    public static string sfCode = string.Empty;
    public static string sfname = string.Empty;
    public static string divcode = string.Empty;
    string strMode = string.Empty;
    public static string Date = string.Empty;
    public static string FDate = string.Empty;
    public static string TDate = string.Empty;
    public static string FYear = string.Empty;
    public static string Date_year = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        strMode = Request.QueryString["Mode"].ToString();

        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
        dv.Value = divcode;
        md.Value = strMode.Trim();
        if (strMode.Trim() == "Year")
        {
            Date = Request.QueryString["select"].ToString();
            lblHead.Text = " <span style='color:Red'>" + "Retailer Visit Status" + "</span> for the Year of " + Date;

        }
        else if (strMode.Trim() == "Month")
        {
            FDate = Request.QueryString["startdate"].ToString();
            TDate = Request.QueryString["enddate"].ToString();
            FYear = Request.QueryString["year"].ToString();
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            lblHead.Text = " <span style='color:Red'>" + "Retailer Visit Status" + "</span> from " + FDate + " to " + TDate;
            Date_year = FYear + "-" + Date;
            fdate.Value = FDate;
            tdate.Value = TDate;

        }
        else if (strMode.Trim() == "Date")
        {
            FDate = Request.QueryString["select"].ToString();
            TDate = Request.QueryString["select"].ToString();
            lblHead.Text = " <span style='color:Red'>" + "Retailer Visit Status" + "</span> for the Day of " + FDate;
            fdate.Value = FDate;
            tdate.Value = TDate;
        }
    }
    [WebMethod(EnableSession = true)]
    public static string Fillusers()
    {
        vis dv = new vis();
        DataTable ds = new DataTable();
        ds = dv.UserList_getMR();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string Fillvisiefec()
    {
        vis dv = new vis();
        DataTable ds = new DataTable();
        ds = dv.get_Fillvisiefec();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string Fillmapped()
    {
        vis dv = new vis();
        DataTable ds = new DataTable();
        ds = dv.getFillmapped();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string Filltot()
    {
        vis dv = new vis();
        DataTable ds = new DataTable();
        ds = dv.getFilltot();
        return JsonConvert.SerializeObject(ds);
    }
    public class vis
    {
        public DataTable get_Fillvisiefec()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [retailvisit_visi_effect] '" + sfCode + "','" + divcode + "','" + FDate + "','" + TDate + "'";  
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
        public DataTable getFillmapped()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [retailvisit_maped] '" + sfCode + "','" + divcode + "'";
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
        public DataTable getFilltot()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [retailvisit_tot] '" + sfCode + "','" + divcode + "','" + FDate + "','" + TDate + "'";
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
        public DataTable UserList_getMR()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSF = null;
            string strQry = "EXEC [all_getHyrSFList_MR] '" + sfCode + "', '" + divcode + "' ";

            try
            {
                dsSF = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
    }
}