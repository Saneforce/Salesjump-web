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
using System.Text;

public partial class MasterFiles_Options_sec_sale_upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsListedDR = null;
    DataSet dslstSpec = new DataSet();
    DataSet dslstCat = new DataSet();
    DataSet dslstCls = new DataSet();
    string sfCode = string.Empty;
    StringBuilder errMsg = new StringBuilder();
    SqlConnection conn = new SqlConnection(Globals.ConnString);
    DataSet ds;
    DataTable Dt;
    int output = 1;
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
            DataSet dslstDR = new DataSet();

            dslstSpec = lstDR.GetCategory_Special_Code(columns[1], div_code);
            if (dslstSpec.Tables[0].Rows.Count > 0)
            {
                StrSpec_Code = dslstSpec.Tables[0].Rows[0][0].ToString();
                StrSpec_SName = dslstSpec.Tables[0].Rows[0][1].ToString();

            }

            dslstCat = lstDR.GetDoc_Cat_Code(columns[2], div_code);
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
                new DataColumn("Qty",typeof(string)),
                new DataColumn("sfcode",typeof(string))});

            using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Year,Month,Day,DistributorCode,DistributorName,ItemCode,ItemName,Qty,sfcode,'" + div_code + "' from [" + sheet1 + "]", excel_con))
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
                    sqlBulkCopy.DestinationTableName = "dbo.UPL_Purchase";

                    //[OPTIONAL]: Map the Excel columns with that of the database table
                    sqlBulkCopy.ColumnMappings.Add("Year", "Year");
                    sqlBulkCopy.ColumnMappings.Add("Month", "Month");
                    sqlBulkCopy.ColumnMappings.Add("Day", "Day");
                    sqlBulkCopy.ColumnMappings.Add("DistributorCode", "DistributorCode");
                    sqlBulkCopy.ColumnMappings.Add("DistributorName", "DistributorName");
                    sqlBulkCopy.ColumnMappings.Add("ItemCode", "ItemCode");
                    sqlBulkCopy.ColumnMappings.Add("ItemName", "ItemName");
                    sqlBulkCopy.ColumnMappings.Add("Qty", "Qty");
                    sqlBulkCopy.ColumnMappings.Add("sfcode", "sfcode");
                    sqlBulkCopy.ColumnMappings.Add("Expr1009", "Division_Code");
                    con.Open();
                    sqlBulkCopy.WriteToServer(dtExcelData);
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Sucessfully');</script>");
                    con.Close();
                }
            }
        }
    }



    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        div_code = div_code.TrimEnd(',');
        if (div_code == "11")
        {
            try
            {
                Response.ContentType = "application/vnd.ms-excel";
                string fileName = Server.MapPath("~\\Document\\UPLPrimaryProductQty.xls");
                Response.AppendHeader("Content-Disposition", "attachment; filename=UPLPrimaryProductQty.xls");
                Response.TransmitFile(fileName);
                Response.End();
            }
            catch (Exception)
            {

            }
        }
        else
        {
            try
            {

                Response.ContentType = "application/vnd.ms-excel";
                string fileName = Server.MapPath("~\\Document\\UPL_Purchase_ Primary_Master.xlsx");
                Response.AppendHeader("Content-Disposition", "attachment; filename=UPL_Purchase_ Primary_Master.xlsx");
                Response.TransmitFile(fileName);
                Response.End();

            }

            catch (Exception)
            {



            }
        }

    }

    protected void btnUpload_Click(object sender, ImageClickEventArgs e)
    {
        if (div_code == "11")
        {
            ImporttoDatatabletsr();
            if (errMsg.Length == 0)
            {
                InsertDatatsr();

            }
            else
            {
                lblcount.Text = errMsg.ToString();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Errors Found');</script>");
                return;
            }

        }
        else
        {
            UpLoadFile();

            //ImporttoDatatable();
            //InsertData();
        }
    }
    private void ImporttoDatatabletsr()
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
                        Dt = new DataTable();
                        Dt = ds.Tables[0];
                    }
                    excel_con.Close();
                    // lblcount.Text = "TOT Row  : " + Dt.Rows.Count + "TOT Col : " + Dt.Columns.Count;
                    GridView1.DataSource = Dt;
                    GridView1.DataBind();

                    DataTable dp = new DataTable();
                    dp.Columns.Add("PDate", typeof(string));
                    dp.Columns.Add("PJCM", typeof(string));
                    dp.Columns.Add("DistCode", typeof(string));
                    dp.Columns.Add("DistName", typeof(string));
                    dp.Columns.Add("PrdCode", typeof(string));
                    dp.Columns.Add("PrdVal", typeof(string));



                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        DateTime pdate = new DateTime();
                        try
                        {
                            pdate = Convert.ToDateTime(Dt.Rows[i][0]);
                        }
                        catch
                        {
                            errMsg.Append("Row No. : " + (i + 1) + " Invalid Date Format <br/>");
                        }
                        if (Dt.Rows[i][2].ToString() != string.Empty)
                        {
                            for (int j = 4; j < Dt.Columns.Count; j++)
                            {
                                if (Dt.Rows[i][j].ToString() != string.Empty)
                                    dp.Rows.Add(pdate.ToString("yyyy/MM/dd"), Dt.Rows[i][1].ToString(), Dt.Rows[i][2].ToString(), Dt.Rows[i][3].ToString(), Dt.Columns[j].ToString(), Dt.Rows[i][j].ToString());
                            }
                        }
                        else
                        {
                            errMsg.Append("Row No. : " + (i + 1) + " Distributore Code Missing  <br/> ");
                        }

                    }
                    Dt.Rows.Clear();
                    Dt = dp;
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
    protected bool CheckDate(String date)
    {
        try
        {
            DateTime dt = DateTime.Parse(date);
            return true;
        }
        catch
        {
            return false;
        }
    }
    private void InsertDatatsr()
    {
        if (Dt == null)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Execel File');</script>");
            return;
        }

        string consString = Globals.ConnString;
        using (SqlConnection con = new SqlConnection(consString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdatePrimary"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@tablePrimary", Dt);
                con.Open();
                SqlDataReader drs = cmd.ExecuteReader();
                DataTable dts = new DataTable();
                dts.Load(drs);
                con.Close();
                if (dts.Rows.Count > 0)
                {

                    DataRow[] rD = dts.Select("Type='D'");

                    if (rD.Length > 0)
                    {
                        lblcount.Text = "Errors";
                        lblcount.ForeColor = System.Drawing.Color.Red;
                        GridView1.DataSource = rD.CopyToDataTable();
                        GridView1.DataBind();
                    }
                    DataRow[] rP = dts.Select("Type='P'");
                    if (rP.Length > 0)
                    {

                        lblcount.Text = "Errors";
                        lblcount.ForeColor = System.Drawing.Color.Red;
                        GridView2.DataSource = rP.CopyToDataTable();
                        GridView2.DataBind();
                    }
                    DataRow[] rS = dts.Select("Type='S'");
                    if (rS.Length > 0)
                    {
                        //    GridView1.DataSource = rS.CopyToDataTable();
                        //    GridView1.DataBind();
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Successfully Upload');</script>");
                    }
                }
            }
        }
    }
}