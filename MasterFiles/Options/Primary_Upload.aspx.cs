using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data.Common;


public partial class MasterFiles_Options_Primary_Upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();

    string sfCode = string.Empty;
    string Username = string.Empty;
    string Password = string.Empty;
    string Designation = string.Empty;
    string sf_type = string.Empty;

    SqlConnection conn = new SqlConnection(Globals.ConnString);
    DataSet ds;
    DataTable Dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();       
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;

            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();

            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                }

                ddlYear.Text = DateTime.Now.Year.ToString();                
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

            }
        }
    }
    private void InsertData()
    {
        // string sf_code = string.Empty;
        string sf_Username = string.Empty;
        string strsfcode = string.Empty;
        string Strtype = string.Empty;
        string Pool_Name = string.Empty;
        string Pool_NameNew = string.Empty;
        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            DataRow row = Dt.Rows[i];
            int columnCount = Dt.Columns.Count;
            string[] columns = new string[columnCount];
            for (int j = 0; j < columnCount; j++)
            {
                columns[j] = row[j].ToString().Trim();

            }
            Stockist objStock = new Stockist();
            int SlNo = objStock.GetPrimary();      
        

        
            conn.Open();

            div_code = Session["div_code"].ToString();

            string sql = "INSERT INTO JS_Primary_Upload(SNo, Stockist_ERP_Code, Stockist_Name, Product_ERP_Code, Product_ERP_Name, Pack, Qty, Value, Pmonth,Pyear, Division_Code, Created_Date) " +
            " values('" + SlNo + "', '" + columns[1] + "', '" + columns[2] + "','" + columns[3] + "', '" + columns[4] + "', '" + columns[5] + "', '" + columns[6] + "', '" + columns[7] + "', '" + ddlMonth.SelectedValue + "' ,'" + ddlYear.SelectedValue + "','" + div_code + "',getdate() )";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Primary Uploaded Sucessfully');</script>");

            conn.Close();
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
                        //objAdapter1.Fill(ds);
                        //Dt = ds.Tables[0];
                    }
                    excel_con.Close();
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {

        ImporttoDatatable();
        InsertData();
    }
    //protected void chkDeact_CheckedChanged(object sender, EventArgs e)
    //{

    //}
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\UPL_Primary_Master.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=UPL_Primary_Master.xlsx");
            Response.TransmitFile(fileName);
            Response.End();

        }


        catch (Exception ex)
        {

            // lblMessage.Text = ex.Message;

        }
    }
}