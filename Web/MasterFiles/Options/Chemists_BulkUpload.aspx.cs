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

public partial class MasterFiles_Options_Chemists_BulkUpload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsListedDR = null;
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
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
        }

    }

    private void ImportData()
    {
        try
        {
            if (FlUploadcsv.HasFile)
            {
                string FileName = FlUploadcsv.FileName;
                string path = string.Concat(Server.MapPath("~/Upload_Document/" + FlUploadcsv.FileName));
                OleDbConnection OleDbcon = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;");

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", OleDbcon);
                OleDbDataAdapter objAdapter1 = new OleDbDataAdapter(cmd);
                ds = new DataSet();
                objAdapter1.Fill(ds);
                Dt = ds.Tables[0];
            }
        }
        catch (Exception ex)
        {

        }
    }
        //private void Deactivate()
        //{
        //    if (chkDeact.Checked == true)
        //    {
        //        conn.Open();
        //        string sql = "update Mas_Chemists set Chemists_Active_Flag = 1 where Chemists_Active_Flag =0 and Division_Code = '" + div_code + "' and SF_Code = '" + ddlFilter.SelectedValue + "'";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.ExecuteNonQuery();
        //        conn.Close();
        //    }

        //}
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ImporttoDatatable();
            InsertData();
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
            }
            catch (Exception ex)
            {

            }
        }

        private void InsertData()
        {
            string StrSpec_Code = string.Empty;
            string StrTerritory_Code = string.Empty;
            string StrCat_Code = string.Empty;
            string StrCls_Code = string.Empty;
            string strUsername = string.Empty;
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                DataRow row = Dt.Rows[i];
                int columnCount = Dt.Columns.Count;
                string[] columns = new string[columnCount];
                for (int j = 0; j < columnCount; j++)
                {
                    columns[j] = row[j].ToString();
                }
                SalesForce sf = new SalesForce();
                dsSalesForce = sf.getSfCode(columns[1]);
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    strUsername = dsSalesForce.Tables[0].Rows[0][0].ToString();
                }
                Territory terr = new Territory();
                int terrcode = terr.Getterr_Code();
                Chemist chem = new Chemist();
                int Chemists_Code = chem.GetChemistCode();
                ListedDR lstDR = new ListedDR();
                //DataSet dslstDR = new DataSet();
                //dslstDR = lstDR.GetTerritory_Code(columns[8]);
                //if (dslstDR.Tables[0].Rows.Count > 0)
                //{
                //    StrTerritory_Code = dslstDR.Tables[0].Rows[0][0].ToString();
                //}
                DataSet dslstDR = new DataSet();
                dslstDR = lstDR.GetTerritory_Upload(columns[8].Trim(), strUsername, div_code);
                if (dslstDR.Tables[0].Rows.Count > 0)
                {
                    StrTerritory_Code = dslstDR.Tables[0].Rows[0][0].ToString();
                }
                conn.Open();
                if ((dsSalesForce.Tables[0].Rows.Count > 0))
                {
                    if (dslstDR.Tables[0].Rows.Count == 0)
                    {
                        string strQry = "insert into Mas_Territory_Creation (Territory_Code,Territory_Name,Territory_Cat,Territory_Sname,division_code, " +
                            " SF_Code,Territory_Active_Flag,Created_date) " +
                            " VALUES('" + terrcode + "', '" + columns[8].Trim() + "' ,'1', null, " +
                            " '" + div_code + "', '" + strUsername + "', 0, getdate())";
                        SqlCommand cmd1 = new SqlCommand(strQry, conn);
                        cmd1.ExecuteNonQuery();
                    }

                    dslstDR = lstDR.GetTerritory_Upload(columns[8].Trim(), strUsername, div_code);
                    if (dslstDR.Tables[0].Rows.Count > 0)
                    {
                        StrTerritory_Code = dslstDR.Tables[0].Rows[0][0].ToString();
                    }
                    columns[2] = columns[2].Replace("  ", " ");
                    string sql = "insert into Mas_Chemists (Chemists_Code,SF_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Mobile,Chemists_Contact,Cat_Code,Territory_Code,Chemists_Active_Flag, division_code,Created_Date) ";
                    sql += "VALUES('" + Chemists_Code + "','" + strUsername + "','" + columns[2].Trim() + "', '" + columns[3].Trim() + "', '" + columns[4] + "','" + columns[5] + "','" + columns[6] + "',0, '" + StrTerritory_Code + "',0,'" + div_code + "',getdate())";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Chemists Uploaded Sucessfully');</script>");
                  
                }
                conn.Close();
            }
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {

                Response.ContentType = "application/vnd.ms-excel";
                string fileName = Server.MapPath("~\\Document\\UPL_Chemist_Bulk.xlsx");
                Response.AppendHeader("Content-Disposition", "attachment; filename=UPL_Chemist_Bulk.xlsx");
                Response.TransmitFile(fileName);
                Response.End();

            }

            catch (Exception ex)
            {

                // lblMessage.Text = ex.Message;

            }

        }
        //private void FillTerr()
        //{
        //    ListedDR lstDR = new ListedDR();
        //    dsListedDR = lstDR.Terr_doc(ddlFilter.SelectedValue.ToString(), div_code);
        //    if (dsListedDR.Tables[0].Rows.Count > 0)
        //    {
        //        rptTerr.DataSource = dsListedDR;
        //        rptTerr.DataBind();

        //    }

        //    else
        //    {
        //        rptTerr.DataSource = dsListedDR;
        //        rptTerr.DataBind();

        //    }
        //}
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           
        }
        protected void chkDeact_CheckedChanged(object sender, EventArgs e)
        {
           
        }
    }
