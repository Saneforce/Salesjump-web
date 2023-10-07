using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Options_Outlet_target_Upload : System.Web.UI.Page
{
    DataSet ds;
    DataTable Dt;
    int output = 1;
    SqlConnection conn = new SqlConnection(Globals.ConnString);
    DataSet dslst = new DataSet();
    DataSet prod = new DataSet();
    string Div_Code = string.Empty;
    string Strsf_Code = string.Empty;
    string Strsf_Name = string.Empty;
    string Strprod_cd = string.Empty;
    string Strprod_nm = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Div_Code = Session["div_code"].ToString();
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\Outlet_Target_Upload.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Outlet_target.xlsx");
            Response.TransmitFile(fileName);
            Response.End();
        }

        catch (Exception ex)
        {
            // lblMessage.Text = ex.Message;
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
                    messageLiteral.Text = "Please Upload Valid Excel File...";
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
            messageLiteral.Text = ex.Message;
            return;
        }
    }

    private void InsertData()
    {
        local lstDR = new local();
        if (Dt == null)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Execel File');</script>");
            messageLiteral.Text = "Please select a file.";
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
				if(Convert.ToString(Dt.Rows[i]["Employee_ID"])!="" && Convert.ToString(Dt.Rows[i]["Product_Code"])!="" && Convert.ToString(Dt.Rows[i]["Month"])!="" && Convert.ToString(Dt.Rows[i]["Year"])!="" && Convert.ToString(Dt.Rows[i]["Retailer_Target"])!="")
                Upld_detils += "<Details empid=\"" + Dt.Rows[i]["Employee_ID"] + "\" prodcode=\"" + Dt.Rows[i]["Product_Code"] + "\" prodnm=\"" + Dt.Rows[i]["Product_Name"] + "\" month=\"" + Dt.Rows[i]["Month"] + "\" year=\"" + Dt.Rows[i]["Year"] + "\" retcnt=\"" + ((Convert.ToInt32(Dt.Rows[i]["Retailer_Target"]) < 0) ? 0 : Convert.ToInt32(Dt.Rows[i]["Retailer_Target"])) + "\"/>";
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
                    dslst = lstDR.GetSF_code(columns[2], Div_Code);
                    if (dslst.Tables[0].Rows.Count > 0)
                    {
                        Strsf_Code = dslst.Tables[0].Rows[0][0].ToString();
                        Strsf_Name = dslst.Tables[0].Rows[0][1].ToString();
                    }
                    else
                    {
                        sMsg += "Unable to Upload Employee code:" + columns[2].ToString() + ". Excel Row No : " + (i + 2) + "</br>";

                    }
                    prod = lstDR.GetProd_det(columns[3], Div_Code);
                    if (prod.Tables[0].Rows.Count > 0)
                    {
                        Strprod_cd = prod.Tables[0].Rows[0][0].ToString();
                        Strprod_nm = prod.Tables[0].Rows[0][1].ToString();
                    }
                    else
                    {
                        sMsg += "Unable to Upload ProductName:" + columns[4].ToString() + ". Excel Row No : " + (i + 2) + "</br>";

                    }
                    if (dslst.Tables[0].Rows.Count > 0 && prod.Tables[0].Rows.Count > 0)
                    {
                        dtx = lstDR.insertupload(Div_Code.Trim(), Upld_detils);
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully...');</script>");
                    }
					else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Upload failed...');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + sMsg + "';</script>");
                messageLiteral.Text = sMsg;
            }
            finally
            {
                conn.Close();
				 if (sMsg != "")
                {
                    messageLiteral.Text = sMsg;
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + ex.Message + "';</script>");
            messageLiteral.Text = ex.Message;
        }
    }

        protected void Upload_Click(object sender, EventArgs e)
    {

        ImporttoDatatable();

        if (output > 0)
        {
            InsertData();
        }
    }

    public class local
    {
        public int insertupload(string div, string taxdet)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int iReturn = -1;
            string strQry = "exec sp_retailer_placement '" + div + "','" + taxdet + "'";
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

            string strQry = "select Sf_Code,Sf_Name from Mas_Salesforce where sf_emp_id='" + Doc_Special_Name + "'and  Division_Code = '" + div_code + "," + "'and SF_Status=0";

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
        public DataSet GetProd_det(string prodcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            string strQry = "select Product_detail_code,Product_detail_name from mas_product_detail where product_detail_code='"+ prodcode + "' and division_code='"+ div_code + "' and product_active_flag=0";

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