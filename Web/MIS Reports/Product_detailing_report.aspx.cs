using Bus_EReport;
using ClosedXML.Excel;
using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_Product_detailing_report : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    public static string sub_division = string.Empty;
    public static string sf_code = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    public static string fieldnm = string.Empty;
    DataTable dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sub_division = Session["sub_division"].ToString();
    }

    [WebMethod]
    public static string getDetailing(string fdate, string tdate, string ffnam,string pnam)
    {
        loc sd = new loc();
        DataSet ds = new DataSet();
        fdt = fdate;
        tdt = tdate;
        fieldnm = ffnam;
        ds = sd.getDetailing(div_code, fdate, tdate, ffnam, pnam);
        //dt = ds.Tables[0];
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string detailing_view(string grpcode, string fdat, string tdat, string sfcode)
    {
        loc sd = new loc();
        DataSet ds = new DataSet();
        ds = sd.detailing_view(div_code, grpcode, fdat, tdat, sfcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getFieldForce(string Div)
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.SalesForceList(Div, sf_code, sub_division);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string getProductMaster(string Div)
    {
        loc sd = new loc();
        DataSet ds = new DataSet();
        ds = sd.getProductMaster(Div);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    protected void ExportToExcel(object sender, EventArgs e)
    {
        DB_EReporting db = new DB_EReporting();
        dt = db.Exec_DataTable("exec sp_prodcut_detailing '" + fieldnm + "','" + fdt + "','" + div_code + "','','" + tdt + "'");
        dt.Columns.Remove("row_num");
        dt.Columns.Remove("Trans_SlNo");
        dt.Columns.Remove("SF_Code");
        dt.Columns.Remove("TransDG_SlNo");
        dt.Columns.Remove("GroupID");
        dt.Columns.Remove("RetailerCode");
        dt.Columns["Sf_Name1"].ColumnName = "FieldForce Name";
        dt.Columns["GroupName"].ColumnName = "Product Name";
        dt.Columns["GPStartTime"].ColumnName = "StartTime";
        dt.Columns["GPEndTime"].ColumnName = "EndTime";
        dt.Columns["GPSpendTime"].ColumnName = "SpendTime";
        dt.Columns["GPFeedbk"].ColumnName = "FeedBack";
        dt.Columns["value"].ColumnName = "Order Value";
        dt.Columns["Territory_Name"].ColumnName = "Route";
        try
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "ProductDetailing");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=ProductDetailing.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
        }
    }

    public class loc
    {
        public DataSet detailing_view(string div, string grpcode,string fdat, string tdat, string sfcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "exec sp_detailing_view '" + div + "','" + sfcode + "','" + fdat + "','" + tdat + "','" + grpcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet getDetailing(string div,string fdate,string tdate, string ffnam, string pnam)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "exec sp_prodcut_detailing '" + ffnam + "','"+ fdate + "','" + div + "','" + pnam + "','"+ tdate+"'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getProductMaster(string div)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select Product_Detail_Code,Product_Detail_Name from mas_product_detail where product_active_flag=0 and division_code='" + div + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}