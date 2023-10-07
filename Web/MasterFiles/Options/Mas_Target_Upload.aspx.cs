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
using ClosedXML.Excel;
using System.Web.Services;
using DBase_EReport;
using Newtonsoft.Json;


public partial class MasterFiles_Options_Mas_Target_Upload : System.Web.UI.Page
{

    static string div_code = string.Empty;
    DataSet dsdiv = null;
    DataSet dsSalesForce = new DataSet();
    DataSet dsListedDR = null;
    DataSet dslstSpec = new DataSet();
    DataSet dslstDeacod = new DataSet();
    DataSet dslstCat = new DataSet();
    DataSet dslstBra = new DataSet();
    DataSet dslstSt = new DataSet();
    DataSet dslstPBUOM = new DataSet();
    DataSet dslstBUOM = new DataSet();
    string sfCode = string.Empty;
    string DivshName = string.Empty;
    string SuDivcode = string.Empty;
    SqlConnection conn = new SqlConnection(Globals.ConnString);
    DataSet ds;
    DataTable Dt;
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
            FillSubDivision();
            
            dd1division_SelectedIndexChanged(sender, e);
          
        }
    }
    private void FillSubDivision()
    {
        Division dv = new Division();
     
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getSubDivisionHO(strdiv);
                    dd1division.DataSource = dsdiv;
                    dd1division.DataTextField = "subdivision_name";
                    dd1division.DataValueField = "subdivision_code";
                    dd1division.DataBind();
                }
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
					DataTable activityDataTable = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    var itemsOfWorksheet = new List<SelectListItem>();
                    if (activityDataTable != null)
                    {
                        //validate worksheet name.
                        string worksheetName;
                        for (int cnt = 0; cnt < activityDataTable.Rows.Count; cnt++)
                        {
                            worksheetName = activityDataTable.Rows[cnt]["TABLE_NAME"].ToString();

                            if (worksheetName.Contains('\''))
                            {
                                worksheetName = worksheetName.Replace('\'', ' ').Trim();
                            }
                            if (worksheetName.Trim().EndsWith("$"))
                                itemsOfWorksheet.Add(new SelectListItem { Text = worksheetName.TrimEnd('$'), Value = worksheetName });
                        }
                    }
                    string sheet1 = "Target";
                    DataTable dtExcelData = new DataTable();
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT SFCODE,PRODUCTCODE,TARGET,MONTH,YEAR from [" + itemsOfWorksheet[0].Value + "];", excel_con))                  													
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
			
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Upload the file in correct format');</script>");
        }
    }
    private void InsertData(string subdiv)
    {

        string Str_Null = string.Empty;
        string Retail_Code = string.Empty;
        string User_Def = string.Empty;
        string StrTerr_Code = string.Empty;
        string StrTerritory_Code = string.Empty;
        string StrDis_Code = string.Empty;
        string StrDis_Name = string.Empty;
        string StrCls_Code = string.Empty;
        string StrSpec_SName = string.Empty;
        string StrTerr_Name = string.Empty;
        string StrCls_SName = string.Empty;
        string StrQua_SName = string.Empty;
        string StrQua_Code = string.Empty;
        string StrChannel_Name = string.Empty;
        string StrChannel_Code = string.Empty;
        string StrClass_Name = string.Empty;
        string StrClass_Code = string.Empty;
        string StrSt_Code = string.Empty;
        string StrPro_Con = string.Empty;
        try
        {
            conn.Open();
            //SqlTransaction objTrans = conn.BeginTransaction();
            String sql = ""; String sMsg = "";
            try
            {
                int irtn = 0; string msg = "";
                for (int K = 0; K < Dt.Rows.Count; K++)
                {
                    DataRow row = Dt.Rows[K];
                    int columnCount = Dt.Columns.Count;
                    string[] columns = new string[columnCount];
                    for (int L = 0; L < columnCount; L++)
                    {
                        columns[L] = row[L].ToString();
                    }
                    int RowNo = K + 2;
                    irtn = GetCheck(columns[0], columns[1], div_code, subdiv);
                    if (irtn <= 0)
                    {
                        msg += K + 1 + ",";
                    }
                }
                if (msg == "")
                {
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
                        ListedDR lstDR = new ListedDR();
                        DataSet dslstDR = new DataSet();


                        int iReturn = lstDR.GetTarget_Check(columns[0], columns[1], columns[2], columns[3], columns[4], div_code);

                        if (iReturn > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully...');</script>");
                        }

                        else
                        {
                            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Here');</script>");
                            dslstSpec = lstDR.Gettarget_Sl_No(div_code);
                            if (dslstSpec.Tables[0].Rows.Count > 0)
                            {
                                Retail_Code = dslstSpec.Tables[0].Rows[0][0].ToString();
                                Retail_Code = (Convert.ToInt32(Retail_Code) + i).ToString();
                            }

                            if (columns[0].ToString().Contains("mr") || columns[0].ToString().Contains("MR"))
                            {
                                dslstBra = lstDR.GetSF_code(columns[0], div_code);

                                if (dslstBra.Tables[0].Rows.Count > 0)
                                {
                                    StrDis_Code = dslstBra.Tables[0].Rows[0][0].ToString();
                                    StrDis_Name = dslstBra.Tables[0].Rows[0][1].ToString();
                                }
                                else
                                {
                                    sMsg += "Unable to Upload SF_Code code:" + columns[0].ToString() + ". Excel Row No : " + (i + 2) + "</br>";

                                }
                            }
                            else
                            {
                                sMsg += "Unable to Upload MGR code:" + columns[0].ToString() + ". Excel Row No : " + (i + 2) + "</br>";
                            }

                            dslstCat = lstDR.GetProd_detail(columns[1], div_code);
                            if (dslstCat.Tables[0].Rows.Count > 0)
                            {
                                StrTerr_Code = dslstCat.Tables[0].Rows[0][0].ToString();
                                StrTerr_Name = dslstCat.Tables[0].Rows[0][1].ToString();

                            }
                            else
                            {
                                sMsg += "Unable to Upload Route Name:" + columns[1].ToString() + ". Excel Row No : " + (i + 2) + "</br>";

                            }

                            //dslstPBUOM = lstDR.GetRetailer_channel(columns[5], div_code);
                            //if (dslstPBUOM.Tables[0].Rows.Count > 0)
                            //{
                            //    StrChannel_Code = dslstPBUOM.Tables[0].Rows[0][0].ToString();
                            //    StrChannel_Name = dslstPBUOM.Tables[0].Rows[0][1].ToString();
                            //}
                            //else
                            //{
                            //    sMsg += "Unable to Upload Channel Name:" + columns[5].ToString() + ". Excel Row No : " + (i + 2) + "</br>";

                            //}



                            if ((dslstSpec.Tables[0].Rows.Count > 0) && (dslstBra.Tables[0].Rows.Count > 0) && (dslstCat.Tables[0].Rows.Count > 0))
                            {

                                sql += " insert into PRODUCT_TARGET_MONTHLY (SF_CODE,PRODUCT_CODE,TARGET,MONTH,YEAR,Division_Code) " +
                                        " VALUES('" + StrDis_Code + "', '" + StrTerr_Code + "', '" + columns[2] + "', '" + columns[3] + "', '" + columns[4] + "', '" + div_code + "')";


                            }
                        }
                    }


                    if (sMsg == "")
                    {
                        SqlCommand CmdUpl = conn.CreateCommand();
                        //CmdUpl.Transaction = objTrans;
                        CmdUpl.CommandType = CommandType.Text;
                        CmdUpl.CommandText = sql;
                        CmdUpl.ExecuteNonQuery();
                        //objTrans.Commit();
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully...');</script>");

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + sMsg + "';</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sf_code Or Product code not match to selected division row No(" + msg + ")');</script>");

                }


            }
            catch (Exception ex)
            {

                //objTrans.Rollback();
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
    public int GetCheck(string sfcode, string prod_code, string Div, string subdiv)
    {
        int iReturn = -1;
        try
        {

            DB_EReporting db_ER = new DB_EReporting();
            string strQry = "exec sp_target_upload_chk  '" + Div + "', '" + sfcode + "' , '" + prod_code + "','"+ subdiv + "'";
            iReturn = db_ER.Exec_Scalar(strQry);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return iReturn;

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
            dtExcelData.Columns.AddRange(new DataColumn[8] { new DataColumn("ERP_Code", typeof(string)),
                new DataColumn("Product_Name", typeof(string)),
                 new DataColumn("Product_Description", typeof(string)),
                  new DataColumn("Product_Category", typeof(string)),
                   new DataColumn("Product_Brand", typeof(string)),
                    new DataColumn("Product_BaseUOM", typeof(string)),
                     new DataColumn("Product_UOM", typeof(string)),
                new DataColumn("Product_Coversationfactor",typeof(string)) });

            using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT ERPCode,ProductName,ProductDescription,ProductCategory,ProductBrand,ProductBaseUOM,ProductUOM,ProductCoversationfactor,'" + div_code + "' from [" + sheet1 + "]", excel_con))
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
 					sqlBulkCopy.ColumnMappings.Add("Expr1008", "Division_Code");
                    con.Open();
                    sqlBulkCopy.WriteToServer(dtExcelData);
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Target Detail Upload Sucessfully');</script>");
                    con.Close();
                }
            }
        }
    }
  
    

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
			DataTable dtUplFile = new DataTable();
			dtUplFile.Columns.Add("SFCODE",typeof(string));
			dtUplFile.Columns.Add("PRODUCTCODE",typeof(string));
			dtUplFile.Columns.Add("TARGET",typeof(string));
			dtUplFile.Columns.Add("MONTH",typeof(string));
			dtUplFile.Columns.Add("YEAR",typeof(string));
			using (XLWorkbook wb = new XLWorkbook())
			{
				wb.Worksheets.Add(dtUplFile, "Target");
				Response.Clear();
				Response.Buffer = true;
				Response.Charset = "";
				Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
				Response.AddHeader("content-disposition", "attachment;filename=Target Upload.xlsx");
				using (MemoryStream MyMemoryStream = new MemoryStream())
				{
					wb.SaveAs(MyMemoryStream);
					MyMemoryStream.WriteTo(Response.OutputStream);
					Response.Flush();
					Response.End();
				}
			}

        }

        catch (Exception)
        {

          

        }

    }

    protected void btnUpload_Click(object sender, ImageClickEventArgs e)
    {
        //UpLoadFile();
        string subdiv = Request.Form["HiddenField1"];
        ImporttoDatatable();
        InsertData(subdiv);
    }
    protected void dd1division_SelectedIndexChanged(object sender, EventArgs e)
    {
        Division dv = new Division();
        dsdiv = dv.getSubDivision_list(dd1division.SelectedValue);
        if (dsdiv.Tables[0].Rows.Count > 0)
        {

            DivshName = dsdiv.Tables[0].Rows[0]["subdivision_sname"].ToString().TrimEnd();
            SuDivcode = dsdiv.Tables[0].Rows[0]["subdivision_code"].ToString();
        }
    }
    protected void Upload_Click(object sender, EventArgs e)

    {

       

    }
    public class SelectListItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static string getdiv()
    {

        DataSet ds = new DataSet();
        ds = getSubDivisionHO(div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet getSubDivisionHO(string div_code)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsDivision = null;

        string strQry = "SELECT subdivision_code,subdivision_name,subdivision_sname FROM mas_subdivision  WHERE Div_Code= '" + div_code + "'and SubDivision_Active_Flag=0";

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
}