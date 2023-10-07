using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Bus_EReport;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data.Common;
using System.Globalization;

using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;

using System.Data;
public partial class MasterFiles_Options_MOQUpload : System.Web.UI.Page
{
    DataSet ds;
    DataTable Dt;
    DataSet dsSalesForce = null;
    string Div_Code = string.Empty;
    string SF_Code = string.Empty;
    int output = 1;
    DataSet dsProduct = null;
    DataSet dsRetailer = null;
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
        Div_Code = Session["div_code"].ToString();
        SF_Code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillMRManagers();
        }
    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = new DataSet();
        dsSalesForce = sf.SalesForceListMgrGet(Div_Code, SF_Code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            //  DDLFO.DataTextField = "sf_name";
            //  DDLFO.DataValueField = "sf_code";
            // DDLFO.DataSource = dsSalesForce;
            // DDLFO.DataBind();
            // DDLFO.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {

        Product prd = new Product();
        dsProduct = prd.getProd(Div_Code);
        ListedDR ldr = new ListedDR();
        dsRetailer = ldr.GetRetailers(Div_Code);
        DataTable dt = new DataTable();
        dt.Columns.Add("SF_Code", typeof(string));
        dt.Columns.Add("Retailer_Code", typeof(string));
        dt.Columns.Add("Retailer_Name", typeof(string));
        dt.Columns.Add("Product_Code", typeof(string));
        dt.Columns.Add("Product_Name", typeof(string));
        dt.Columns.Add("MOQ", typeof(string));
        foreach (DataRow row in dsRetailer.Tables[0].Rows)
        {
            foreach (DataRow dr in dsProduct.Tables[0].Rows)
            {
                dt.Rows.Add(row["Sf_Code"].ToString(), row["ListedDrCode"].ToString(), row["ListedDr_Name"].ToString(), dr["Product_Detail_Code"].ToString(), dr["Product_Detail_Name"].ToString());
            }
        }

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, "Customers");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=MOQUpload.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ImporttoDatatable();

        if (output > 0)
        {
            InsertData();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Sucessfully!!');</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Upload!!');</script>");
        }
    }
    private void ImporttoDatatable()
    {
        try
        {
            if (FlUploadcsv.HasFile)
            {

                string excelPath = Server.MapPath("~/Upload_Document/") + Path.GetFileName(FlUploadcsv.PostedFile.FileName);
                FlUploadcsv.SaveAs(excelPath);

                string conString = string.Empty;
                string extension = Path.GetExtension(FlUploadcsv.PostedFile.FileName);
                switch (extension)
                {
                    case ".xls": //Excel 97-03
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 or higher
                        conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                        break;

                }
                conString = string.Format(conString, excelPath);
                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    excel_con.Open();
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    DataTable dtExcelData = new DataTable();
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * from [" + sheet1 + "]", excel_con))
                    {
                        ds = new DataSet();
                        oda.Fill(ds);
                        Dt = ds.Tables[0];
                    }
                    excel_con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
        }
    }
    private void InsertData()
    {
        if (Dt == null)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Execel File');</script>");
            return;
        }

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[6] { new DataColumn("SFCode", typeof(string)),
                new DataColumn("RetCode", typeof(string)),
                new DataColumn("RetName", typeof(string)),
                new DataColumn("ProCode", typeof(string)),
                new DataColumn("ProName", typeof(string)),
                new DataColumn("MOQ",typeof(string)) });

        foreach (DataRow row in Dt.Rows)
        {
            string sf = row[0].ToString();
            string rcode = row[1].ToString();
            string rname = row[2].ToString();
            string pcode = row[3].ToString();
            string pName = row[4].ToString();
            string pmoq = row[5] == DBNull.Value ? "0" : row[5].ToString();
            dt.Rows.Add(sf, rcode, rname, pcode, pName, pmoq);
        }
        string consString = Globals.ConnString;
        using (SqlConnection con = new SqlConnection(consString))
        {
            using (SqlCommand cmd = new SqlCommand("Update_MOQ"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@tableMOQ", dt);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }

}
