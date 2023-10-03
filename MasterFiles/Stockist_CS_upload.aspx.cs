using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Stockist_CS_upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet ds;
    DataTable Dt;
    int output = 1;
    DataSet dslst = new DataSet();
    DataSet dsplst = new DataSet();
    string Div_Code = string.Empty;

    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        Div_Code = Session["div_code"].ToString();
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
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Sucessfully');</script>");
            }
        }
        catch (Exception ex)
        {
            output = 0;
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
            return;

        }
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {

            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\Stockist_Closing_stock_Upload.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Stockist_Closing_stock_Upload.xlsx");
            Response.TransmitFile(fileName);
            Response.End();

        }

        catch (Exception)
        {

        }

    }

    protected void upbt_Click(object sender, EventArgs e)
    {
        ImporttoDatatable();
        InsertData();
    }

    private void InsertData()
    {
        string Strsf_Code = string.Empty;
        string Strsf_Name = string.Empty;
        string Strst_Code = string.Empty;
        string Strst_Name = string.Empty;
        string prod_code = string.Empty;
        string prod_Name = string.Empty;
        stock lstDR = new stock();
        if (Dt == null)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Execel File');</script>");
            return;
        }
        try
        {
            conn.Open();
            String sMsg = ""; String sql = "";
            string Upld_detils = "<ROOT>";
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Upld_detils += "<Details SFode=\"" + Dt.Rows[i]["sfCode"] + "\" month=\"" + Dt.Rows[i]["Month"] + "\" year=\"" + Dt.Rows[i]["Year"] + "\" cqty=\"" + Dt.Rows[i]["ClosingQty"] + "\" stkcode=\"" + Dt.Rows[i]["DistributorCode"] + "\" prodcode=\"" + Dt.Rows[i]["ProductCode"] + "\" />";
            }
            Upld_detils += "</ROOT>";
            try
            {
                int dtx = -1;
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    DataRow row = Dt.Rows[i];
                    int columnCount = Dt.Columns.Count;
                    string[] columns = new string[columnCount];
                    for (int j = 0; j < columnCount; j++)
                    {
                        columns[j] = row[j].ToString();
                    }
                    int RowNo = i + 2;
                    dslst = lstDR.GetSF_code(columns[2], Div_Code, columns[3]);
                    if (dslst.Tables[0].Rows.Count > 0)
                    {
                        Strsf_Code = dslst.Tables[0].Rows[0][0].ToString();
                        Strsf_Name = dslst.Tables[0].Rows[0][1].ToString();
                        Strst_Code = dslst.Tables[0].Rows[0][2].ToString();
                        Strst_Name = dslst.Tables[0].Rows[0][3].ToString();
                    }
                    else
                    {
                        sMsg += "Unable to Upload This employee :" + columns[2].ToString() + " is not Mapped with this Distributor :"+ columns[3].ToString() + " . Excel Row No : " + (i + 2) + "</br>";

                    }
                    dsplst = lstDR.GetProd_code(columns[4], Div_Code);
                    if (dsplst.Tables[0].Rows.Count > 0)
                    {
                        prod_code= dsplst.Tables[0].Rows[0][0].ToString();
                        prod_Name = dsplst.Tables[0].Rows[0][1].ToString();
                    }
                    else
                    {
                        sMsg += "Please Check ProductCode :" + columns[4].ToString() + " . Excel Row No : " + (i + 2) + "</br>";

                    }
                    
                }
                if (sMsg=="")
                {
                    dtx = lstDR.insertupload(Div_Code.Trim(), Upld_detils);
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully...');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + sMsg + "';</script>");
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + sMsg + "';</script>");
            }
            finally
            {
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + ex.Message + "';</script>");

        }
    }

    public class stock
    {
        public int insertupload(string div, string Upld_detils)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int iReturn = -1;
            string strQry = "exec sp_stockistCS_upload '" + div + "','" + Upld_detils + "'";
            try
            {
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet GetSF_code(string Doc_Special_Name, string div_code,string stkcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            string strQry = "select ms.Sf_Code,Sf_Name,stockist_code,stockist_name from Mas_Salesforce ms inner join mas_stockist st on charindex(','+ms.sf_code+',',','+st.field_code+',')>0 where ms.sf_code='"+ Doc_Special_Name + "'and  ms.Division_Code = '"+ div_code + ",'and SF_Status=0 and st.stockist_code='"+ stkcode + "' group by ms.Sf_Code,Sf_Name,stockist_code,stockist_name";

            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }
        public DataSet GetProd_code(string prodcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            string strQry = "select product_detail_code,product_detail_name from mas_product_detail where division_code='"+ div_code + "' and product_detail_code='"+ prodcode + "' and product_active_flag=0";

            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }
    }
}