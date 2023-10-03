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

public partial class MasterFiles_Options_sec_sale_upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsListedDR = null;
    DataSet dslstSpec = new DataSet();
    DataSet dslstCat = new DataSet();
    DataSet dslstCls = new DataSet();
    string sfCode = string.Empty;    
    SqlConnection conn = new SqlConnection(Globals.ConnString);
    DataSet ds;
    DataTable Dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
      
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            //FillReporting();
            //FillSpl();
            //FillCat();
            //FillCls();
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
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Sucessfully');</script>");
            }
        }
        catch (Exception)
        {

        }
    }
    private void InsertData()
    {
        string StrSpec_Code = string.Empty;
        string StrTerritory_Code = string.Empty;
        string StrCat_Code = string.Empty;
        string StrCls_Code = string.Empty;
        string StrSpec_SName = string.Empty;
        string StrCat_SName = string.Empty;
        string StrCls_SName = string.Empty;
        string StrQua_SName = string.Empty;
        string StrQua_Code = string.Empty;
        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            DataRow row = Dt.Rows[i];
            int columnCount = Dt.Columns.Count;
            string[] columns = new string[columnCount];
            for (int j = 0; j < columnCount; j++)
            {
                columns[j] = row[j].ToString();
            }
            ListedDR objListedDR = new ListedDR();
            int ListerDrCode = objListedDR.GetListedDrCode();
            //Territory terr = new Territory();
            //int terrcode = terr.Getterr_Code();
            ListedDR lstDR = new ListedDR();
            DataSet dslstDR=new DataSet();
        
            dslstSpec = lstDR.GetCategory_Special_Code(columns[1],div_code);
            if (dslstSpec.Tables[0].Rows.Count > 0)
            {
                StrSpec_Code = dslstSpec.Tables[0].Rows[0][0].ToString();
                StrSpec_SName = dslstSpec.Tables[0].Rows[0][1].ToString();
             
            }

            dslstCat = lstDR.GetDoc_Cat_Code(columns[2],div_code);
            if (dslstCat.Tables[0].Rows.Count > 0)
           {
               StrCat_Code = dslstCat.Tables[0].Rows[0][0].ToString();
               StrCat_SName = dslstCat.Tables[0].Rows[0][1].ToString();
            }
            
            conn.Open();
            if ((dslstSpec.Tables[0].Rows.Count > 0) && (dslstCat.Tables[0].Rows.Count > 0))
            {

                string sql = "insert into Trans_Stock_Updation_Details (Tran_Slno,Stockist_code,Product_Code,Rec_Qty,Cb_Qty,Division_Code,Product_Name,pieces, Distributer_Rate,Retailor_Rate,Purchase_Date,Conversion_Qty,SfCode) ";
                sql += "VALUES('" + ListerDrCode + "', '" + StrSpec_Code + "','" + StrCat_Code + "', '" + columns[3] + "','" + columns[4] + "','" + columns[5] + "','" + StrCat_SName + "','" + columns[7] + "','" + columns[8] + "','" + columns[9] + "','" + columns[10] + "', '', '" + StrSpec_SName + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Sucessfully');</script>");
                conn.Close();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Upload');</script>");
                conn.Close();
            }
        }
    }
   
    private void Deactivate()
    {
        //if (chkDeact.Checked == true)
        //{
            conn.Open();
            string sql = "update Mas_ListedDr set ListedDr_Active_Flag = 1,listeddr_deactivate_date = getdate() where ListedDr_Active_Flag =0 and Division_Code = '" + div_code + "' and SF_Code = '" + sfCode + "' ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
       // }
    }
    private void UpLoadFile()
    {
        //Upload and save the file
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

            //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
            dtExcelData.Columns.AddRange(new DataColumn[9] { new DataColumn("Year", typeof(int)),
                new DataColumn("Month", typeof(string)),
                 new DataColumn("Day", typeof(string)),
                  new DataColumn("DistributorCode", typeof(string)),
                   new DataColumn("DistributorName", typeof(string)),
                    new DataColumn("ItemCode", typeof(string)),
                     new DataColumn("ItemName", typeof(string)),
                new DataColumn("Qty",typeof(string)) ,
              new DataColumn("Pieces",typeof(string))});

              using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Year,Month,Day,DistributorCode,DistributorName,ItemCode,ItemName,Qty,Pieces,'"+div_code+"' from [" + sheet1 + "]", excel_con))
			{
                oda.Fill(dtExcelData);
            }
            excel_con.Close();

            string consString = Globals.ConnString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(consString, SqlBulkCopyOptions.FireTriggers))
                {
                    //Set the database table name
                    sqlBulkCopy.DestinationTableName = "dbo.UPL_Closing_Purchase";

                    //[OPTIONAL]: Map the Excel columns with that of the database table
                    sqlBulkCopy.ColumnMappings.Add("Year", "Year");
                    sqlBulkCopy.ColumnMappings.Add("Month", "Month");
                    sqlBulkCopy.ColumnMappings.Add("Day", "Day");
                    sqlBulkCopy.ColumnMappings.Add("DistributorCode", "DistributorCode");
                    sqlBulkCopy.ColumnMappings.Add("DistributorName", "DistributorName");
                    sqlBulkCopy.ColumnMappings.Add("ItemCode", "ItemCode");
                    sqlBulkCopy.ColumnMappings.Add("ItemName", "ItemName");
                    sqlBulkCopy.ColumnMappings.Add("Qty", "Qty");
                    sqlBulkCopy.ColumnMappings.Add("Pieces", "Pieces");
 					sqlBulkCopy.ColumnMappings.Add("Expr1009", "Division_Code");
                    con.Open();
                    sqlBulkCopy.WriteToServer(dtExcelData);
 					ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Closing Upload Sucessfully');</script>");
                    con.Close();
                }
            }
        }
    }
  
    

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {

            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\UPL_Closing_ Upload_Master.xls");
            Response.AppendHeader("Content-Disposition", "attachment; filename=UPL_Closing_ Upload_Master.xls");            
            Response.TransmitFile(fileName);
            Response.End();

        }

        catch (Exception)
        {

          

        }

    }

    protected void btnUpload_Click(object sender, ImageClickEventArgs e)
    {
        UpLoadFile();
        //ImporttoDatatable();
        //InsertData();
    }
}