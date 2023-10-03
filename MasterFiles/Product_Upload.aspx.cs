using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using DBase_EReport;
using Newtonsoft.Json;
using System.IO;
using System.Configuration;
using System.Data.OleDb;


public partial class MasterFiles_Product_Upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sfCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        sfCode = Session["sf_code"].ToString();
    }

    [WebMethod]
    public static string dd1divisionChanged()
    {
        DataSet dsdiv = null;
        string Divi_code = HttpContext.Current.Session["div_code"].ToString();
        dsdiv = getSubDivision_list(Divi_code);
        return JsonConvert.SerializeObject(dsdiv.Tables[0]);
    }
    public static DataSet getSubDivision_list(string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsDivision = null;

        string strQry = "SELECT subdivision_code,subdivision_name,subdivision_sname FROM mas_subdivision where Div_Code='" + divcode + "' and SubDivision_Active_Flag=0";
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
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
			Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\Product_Upload_Sf_Wise.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Product_Upload_Sf_Wise.xlsx");
            Response.TransmitFile(fileName);
            Response.Flush();
            Response.End();

        }

        catch (Exception)
        {



        }

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {

        UpLoadFile();
    }

    private void UpLoadFile()
    {
	 try
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
            System.Data.DataTable dtExcelData = new System.Data.DataTable();
            //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
            dtExcelData.Columns.AddRange(new DataColumn[11] { new DataColumn("MONTH", typeof(string)),
               new DataColumn("STATE", typeof(string)),
                new DataColumn("HQ", typeof(string)),
                 new DataColumn("EMP ID", typeof(string)),
                  new DataColumn("EMP NAME", typeof(string)),
                   new DataColumn("DISTRIBUTOR CODE", typeof(string)),
					new DataColumn("DESIGNATION", typeof(string)),
					 new DataColumn("PRODUCT NAME", typeof(string)),
                      new DataColumn("PRODUCT ERP", typeof(string)),
                       new DataColumn("UNITS", typeof(string)),
                        new DataColumn("QTY",typeof(string))});
            using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT *,'" + div_code + "' from [" + sheet1 + "]", excel_con))
            {
                oda.Fill(dtExcelData);
            }
            excel_con.Close();
		 bool hasEmptyRow = dtExcelData.AsEnumerable().Any(row =>
            row.ItemArray.Any(field => string.IsNullOrEmpty(field.ToString())));

                // Display the result
                if (hasEmptyRow)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('The Excel contains an empty row.');</script>");                    
                }
                else
                {
            		string consString = Globals.ConnString;
            		using (SqlConnection con = new SqlConnection(consString))
            		{
                		using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(consString, SqlBulkCopyOptions.FireTriggers))
                		{
                    		//Set the database table name
                    		sqlBulkCopy.DestinationTableName = "dbo.Mas_PriOrd_Upload";

                    		//[OPTIONAL]: Map the Excel columns with that of the database table                                        
                    		sqlBulkCopy.ColumnMappings.Add("Month", "Month");
                    		sqlBulkCopy.ColumnMappings.Add("STATE", "STATE");
                    		sqlBulkCopy.ColumnMappings.Add("HQ", "HQ");
                    		sqlBulkCopy.ColumnMappings.Add("EMP ID", "EMPID");
                    		sqlBulkCopy.ColumnMappings.Add("EMP NAME", "EMPNAME");
				sqlBulkCopy.ColumnMappings.Add("DISTRIBUTOR CODE", "DISTRIBUTORCODE");
                    		sqlBulkCopy.ColumnMappings.Add("DESIGNATION", "DESIGNATION");					
                    		sqlBulkCopy.ColumnMappings.Add("PRODUCT NAME", "PRODUCTNAME");
                    		sqlBulkCopy.ColumnMappings.Add("UNITS", "UNITS");
                    		sqlBulkCopy.ColumnMappings.Add("QTY", "QTY");
                    		sqlBulkCopy.ColumnMappings.Add("Expr1000", "DivisionCode");
                    		sqlBulkCopy.ColumnMappings.Add("PRODUCT ERP", "PRODUCT_ERP_CODE");
                    		con.Open();
                    		sqlBulkCopy.WriteToServer(dtExcelData);
                    		ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Detail Upload Sucessfully');</script>");
                    		con.Close();
                		}
            		}
	 	}
        }
	}
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('"+ ex + "');</script>");
            //throw ex;
        }
    }

}