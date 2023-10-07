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

public partial class MasterFiles_Options_Stockiest_Upload : System.Web.UI.Page
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
        //  sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            //FillReporting();           
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
            int Stockist_Code = objStock.GetStockistCode();
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.GetSFCode_Upload(columns[4]);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                strsfcode = dsSalesForce.Tables[0].Rows[0][0].ToString();
            }
            Username = "";
            string[] strDivSplit = columns[1].Split('/');
               foreach (string strdiv in strDivSplit)
               {
                   if (strdiv != "")
                   {
                       dsSalesForce = sf.getSfCode(strdiv);
                       if (dsSalesForce.Tables[0].Rows.Count > 0)
                       {
                           sf_Username = dsSalesForce.Tables[0].Rows[0][0].ToString();
                       }
                       Username += sf_Username + ',';
                   }
               }

               DataSet dspool = new DataSet();
               Stockist stk = new Stockist();
               dspool = stk.GetPool(columns[8], div_code);

               if (dspool.Tables[0].Rows.Count > 0)
               {
                   Pool_NameNew = dspool.Tables[0].Rows[0][0].ToString();
               }
            conn.Open();
      
            div_code = Session["div_code"].ToString();
            if (dspool.Tables[0].Rows.Count == 0 && columns[8] !="")
            {
                string strQry = "INSERT INTO Mas_Pool_Area_Name(Division_Code,Pool_SName,Pool_Name,created_Date,LastUpdt_Date)" +
                                "values('" + div_code + "','" + columns[8] + "', '" + columns[8] + "',getdate(),getdate()) ";
                SqlCommand cmd1 = new SqlCommand(strQry, conn);
                cmd1.ExecuteNonQuery();
            }

            if (dspool.Tables[0].Rows.Count > 0)
            {
                Pool_Name = dspool.Tables[0].Rows[0][0].ToString();
            }
                string sql = "INSERT INTO mas_stockist(Division_Code,Stockist_Code, SF_Code, Stockist_Name, Stockist_Address, Stockist_ContactPerson, Stockist_Designation, Stockist_Active_Flag, Stockist_Mobile, Territory,PoolStatus, Created_Date, Stockist_Address1) " +
                " values('" + div_code + "', '" + Stockist_Code + "', '" + Username + "','" + columns[2] + "', '" + columns[3] + "', '" + columns[5] + "', '', 0 ,'" + columns[6] + "','" + columns[8] + "','" + columns[7] + "',getdate(), '" + columns[4] + "' )";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
         
              
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Stockist Uploaded Sucessfully');</script>");

            
         
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
    protected void chkDeact_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\UPL_Stockist_Master.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=UPL_Stockist_Master.xlsx");
            Response.TransmitFile(fileName);
            Response.End();

        }


        catch (Exception ex)
        {

            // lblMessage.Text = ex.Message;

        }
    }
}