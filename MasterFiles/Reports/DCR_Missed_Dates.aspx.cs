using Bus_EReport;
using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class MasterFiles_Reports_DCR_Missed_Dates : System.Web.UI.Page
{
    public static DataSet ds = new DataSet();
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

    }
    [WebMethod]
    public static string GetList(string divcode, string SF, string Mn, string Yr)
    {
        SalesForce sf = new SalesForce();
        ds = sf.getMissedDate_All(divcode, SF, Mn, Yr);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string FillMRManagers(string div_code, string sf_code)
    {
        SalesForce sf = new SalesForce();
        DataSet dsSalesForce = sf.SalesForceList(div_code, sf_code);
        return JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
    }
    public class bindyear
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static bindyear[] BindDate(string divcode)
    {
        //TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();
        //dsTP = tp.Get_TP_Edit_Year(divcode);
        dsTP = Get_TP_Edit_Year(divcode);
        List<bindyear> sf = new List<bindyear>();
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                bindyear rt = new bindyear();
                rt.value = k.ToString();
                rt.text = k.ToString();
                sf.Add(rt);
            }
        }
        return sf.ToArray();
    }

    public static DataSet Get_TP_Edit_Year(string div_code)
    {
        //DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = new DataSet();
        string strQry = String.Empty;

        //strQry = "select max([Year]-1) as Year from Mas_Division where Division_Code='" + div_code + "' ";
        strQry = " SELECT max([Year]-1) as Year FROM Mas_Division ";
        strQry += " WHERE Division_Code = @divcode  ";

        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@divcode", Convert.ToString(div_code));                    
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(dsAdmin);
                    con.Close();
                }
            }
            //dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;

    }

    protected void ExportToExcel(object sender, EventArgs e)
    {
        DataTable dtsummary = new DataTable();
        dtsummary = ds.Tables[0];
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dtsummary, "DCR_Missed_Dates");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=DCR_Missed_Dates.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

    }
}