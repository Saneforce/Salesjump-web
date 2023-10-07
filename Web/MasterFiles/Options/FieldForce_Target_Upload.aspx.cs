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

public partial class MasterFiles_Options_FieldForce_Target_Upload : System.Web.UI.Page
{
    DataSet ds;
    DataTable Dt;
    string Div_Code = string.Empty;
    string SF_Code = string.Empty;
    int output = 1;
    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet dslst = new DataSet();
    string Strsf_Code = string.Empty;
    string Strsf_Name = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Div_Code = Session["div_code"].ToString();
        SF_Code = Session["sf_code"].ToString();
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
            string fileName = Server.MapPath("~\\Document\\FieldForce_Target_Upload.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=FieldForce_Value_Upload.xlsx");
            Response.TransmitFile(fileName);
            Response.Flush();
            Response.End();
        }

        catch (Exception ex)
        {
            // lblMessage.Text = ex.Message;
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ImporttoDatatable();

        if (output > 0)
        {
            InsertData();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Upload!!!');</script>");
        }
    }
    private void ImporttoDatatable()
    {
        try
        {
            if (FlUploadcsv.HasFile)
            {
                string excelPath = Server.MapPath("~/Document/") + Path.GetFileName(FlUploadcsv.PostedFile.FileName);
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
                if (conString == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Upload Valid Excel File...');</script>");
                }
                else
                {
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                        DataTable dtExcelData = new DataTable();

                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * from [" + sheet1 + "]", excel_con))
                        {
                            ds = new DataSet();
                            oda.Fill(ds);
                            Dt = new DataTable();
                            Dt = ds.Tables[0];
                        }
                        excel_con.Close();
                    }
                }

            }
        }
        catch (Exception ex)
        {
            output = 0;
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
            return;
        }
    }
    private void InsertData()
    {
        local lstDR = new local();
        //SqlConnection con = new SqlConnection(Globals.ConnString);
        if (Dt == null)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Execel File');</script>");
            return;
        }
        try
        {
            conn.Open();
            SqlTransaction objTrans = conn.BeginTransaction();
            String sMsg = ""; String sql = "";
            string Upld_detils = "<ROOT>";
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Upld_detils += "<Details SFode=\"" + Dt.Rows[i]["SF_Code"] + "\" month=\"" + Dt.Rows[i]["Month"] + "\" year=\"" + Dt.Rows[i]["Year"] + "\" value=\"" + Dt.Rows[i]["TargetValue"] + "\"/>";
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
                    
                    dslst = lstDR.GetSF_code(columns[0], Div_Code);
                    if (dslst.Tables[0].Rows.Count > 0)
                    {
                        Strsf_Code = dslst.Tables[0].Rows[0][0].ToString();
                        Strsf_Name = dslst.Tables[0].Rows[0][1].ToString();
                    }
                    else
                    {
                        sMsg += "Unable to Upload SF_Code code:" + columns[0].ToString() + ". Excel Row No : " + (i + 2) + "</br>";

                    }
                    if (dslst.Tables[0].Rows.Count > 0)
                    {
                        dtx = lstDR.insertupload(Div_Code.Trim(), Upld_detils);
                        //sql += "delete from Trans_Field_Target_details where TransFTslno in (select h.TransFTslno from Trans_Field_Target_details d inner join Trans_Field_Target_head h on  h.SF_Code='" + Strsf_Code + "' " +
                        //       "where h.SF_Code = '" + Strsf_Code + "' and h.Div_ID = '" + Div_Code + "'  and year = '" + columns[2].ToString() + "')" +
                        //       "delete from Trans_Field_Target_head where year = '" + columns[2].ToString() + "'  AND Div_ID = '" + Div_Code + "' and SF_Code = '" + Strsf_Code + "'";
                        

                    }
                }
                if(dtx>0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully...');</script>");
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

    public class local
    {
        public int insertupload(string div,string taxdet)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int iReturn = -1;
            string strQry = "exec sp_insert_target '" + div + "','" + taxdet + "'";
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
        public DataSet GetSF_code(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            string strQry = "select Sf_Code,Sf_Name from Mas_Salesforce where sf_code='" + Doc_Special_Name + "'and  Division_Code = '" + div_code + "," + "'and SF_Status=0";

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