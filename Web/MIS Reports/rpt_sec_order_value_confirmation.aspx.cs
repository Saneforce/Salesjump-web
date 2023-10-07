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

public partial class MIS_Reports_rpt_sec_order_value_confirmation : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv_code = string.Empty;
    public static string FDate = string.Empty;
    public static string TDate = string.Empty;
    string sf_type = string.Empty;
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
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
    }
    [WebMethod]
    public static string getfilldtl(string divcode)
    {
        Stockist dv = new Stockist();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec secordervalconfirmation '" + Sf_Code + "','" + divcode + "','" + FDate + "','" + TDate + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);

    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        DataTable dsProd1 = null;
        exc LstDoc = new exc();
        dsProd1 = LstDoc.get_secordervalconfirmation();
        DataTable dt = dsProd1;
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, "Sec_Order_Value_Confirmation");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=secordervalconfirmation.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }

        }
    }
    public class exc
    {
        public DataTable get_secordervalconfirmation()
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;

           var strQry = "exec [excel_secordervalconfirmation] '" + Sf_Code + "','" + divcode + "','" + FDate + "','" + TDate + "'";
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