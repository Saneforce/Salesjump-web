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
using System.Globalization;

public partial class MasterFiles_Superstockist_salesupload : System.Web.UI.Page
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
            string fileName = Server.MapPath("~\\Document\\Superstockist_Upload.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Superstockist_Upload.xlsx");
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
    public string getsscode(string erp)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = db.Exec_DataSet("Select S_No Numbers from Supplier_Master where Erp_Code='" + erp + "'");
        string routenum = (ds.Tables[0].Rows[0]["Numbers"]).ToString();
        return routenum;
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
                dtExcelData.Columns.AddRange(new DataColumn[11] {
                  new DataColumn("DATE", typeof(string)),
                   new DataColumn("STATE", typeof(string)),
                    new DataColumn("HQ", typeof(string)),
                     new DataColumn("EMP ID", typeof(string)),
                      new DataColumn("EMP NAME", typeof(string)),
                       new DataColumn("DESIGNATION", typeof(string)),
                        new DataColumn("SUPERSTOCKIST ERP CODE", typeof(string)),
                         new DataColumn("PRODUCT NAME", typeof(string)),
                          new DataColumn("PRODUCT CODE", typeof(string)),
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
                        con.Open();
                        string insertQuery = "INSERT INTO superstock_sales_upload (DATE,STATE,HQ,EMPID,EMPNAME,DESIGNATION,PRODUCTNAME,UNITS,QTY,DivisionCode,PRODUCT_ERP_CODE,SUPERSTOCKISTCODE) VALUES (@param1, @param2, @param3, @param4, @param5,@param6, @param7, @param8, @param9, @param10, @param11, @param12)";

                        DataTable dataTable = dtExcelData;

                        foreach (DataRow row in dataTable.Rows)
                        {
                            
                            string inputDateString = row["DATE"].ToString();
                            string value2 = row["STATE"].ToString();
                            string value3 = row["HQ"].ToString();
                            string value4 = row["EMP ID"].ToString();
                            string value5 = row["EMP NAME"].ToString();
                            string value6 = row["DESIGNATION"].ToString();
                            string value7 = row["PRODUCT NAME"].ToString();
                            string value8 = row["UNITS"].ToString();
                            string value9 = row["QTY"].ToString();
                            string value10 = row["Expr1000"].ToString();
                            string value11 = row["PRODUCT CODE"].ToString();
                            string value12 = getsscode(row["SUPERSTOCKIST ERP CODE"].ToString());
                            DateTime dateTime = DateTime.ParseExact(inputDateString, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                            string value1 = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            
                            using (SqlCommand command = new SqlCommand(insertQuery, con))
                            {
                                command.Parameters.AddWithValue("@param1", value1);
                                command.Parameters.AddWithValue("@param2", value2);
                                command.Parameters.AddWithValue("@param3", value3);
                                command.Parameters.AddWithValue("@param4", value4);
                                command.Parameters.AddWithValue("@param5", value5);
                                command.Parameters.AddWithValue("@param6", value6);
                                command.Parameters.AddWithValue("@param7", value7);
                                command.Parameters.AddWithValue("@param8", value8);
                                command.Parameters.AddWithValue("@param9", value9);
                                command.Parameters.AddWithValue("@param10", value10);
                                command.Parameters.AddWithValue("@param11", value11);
                                command.Parameters.AddWithValue("@param12", value12);

                               
                                command.ExecuteNonQuery();
                            }
                        }
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Successfully Inserted');</script>");
                        con.Close();


                        //using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(consString, SqlBulkCopyOptions.FireTriggers))
                        //{
                        //    //Set the database table name
                        //    sqlBulkCopy.DestinationTableName = "dbo.superstock_sales_upload";

                        //    //[OPTIONAL]: Map the Excel columns with that of the database table                                        
                        //    sqlBulkCopy.ColumnMappings.Add("DATE", "DATE");
                        //    sqlBulkCopy.ColumnMappings.Add("STATE", "STATE");
                        //    sqlBulkCopy.ColumnMappings.Add("HQ", "HQ");
                        //    sqlBulkCopy.ColumnMappings.Add("EMP ID", "EMPID");
                        //    sqlBulkCopy.ColumnMappings.Add("EMP NAME", "EMPNAME");
                        //    sqlBulkCopy.ColumnMappings.Add("SUPERSTOCKIST CODE", "SUPERSTOCKISTCODE");
                        //    sqlBulkCopy.ColumnMappings.Add("DESIGNATION", "DESIGNATION");
                        //    sqlBulkCopy.ColumnMappings.Add("PRODUCT NAME", "PRODUCTNAME");
                        //    sqlBulkCopy.ColumnMappings.Add("UNITS", "UNITS");
                        //    sqlBulkCopy.ColumnMappings.Add("QTY", "QTY");
                        //    sqlBulkCopy.ColumnMappings.Add("Expr1000", "DivisionCode");
                        //    sqlBulkCopy.ColumnMappings.Add("PRODUCT ERP", "PRODUCT_ERP_CODE");
                        //    con.Open();
                        //    sqlBulkCopy.WriteToServer(dtExcelData);
                        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Detail Upload Sucessfully');</script>");
                        //    con.Close();
                        //}

                    }
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex + "');</script>");
            //throw ex;
        }
    }

}