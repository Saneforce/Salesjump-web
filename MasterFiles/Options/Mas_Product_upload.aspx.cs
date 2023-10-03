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
using DBase_EReport;

public partial class MasterFiles_Options_Mas_Product_upload : System.Web.UI.Page
{

    string div_code = string.Empty;
    DataSet dsProduct = null;
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
    int output = 1;
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
            FillCat();
            FillBrand();
            FillUOM();
        }
        dd1division_SelectedIndexChanged1(sender, e);
    }
    private void FillCat()
    {
        Product prd = new Product();
        dsProduct = prd.getProductCategory_UP(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            rptCat.DataSource = dsProduct;
            rptCat.DataBind();
        }
        else
        {
            rptCat.DataSource = dsProduct;
            rptCat.DataBind();
        }

    }

    private void FillBrand()
    {
        Product prd = new Product();
        dsProduct = prd.getProductBrand_UP(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            rptGrp.DataSource = dsProduct;
            rptGrp.DataBind();
        }
        else
        {
            rptGrp.DataSource = dsProduct;
            rptGrp.DataBind();
        }
    }
    private void FillUOM()
    {
        Product prd = new Product();
        dsProduct = prd.getProductGroup_UP(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            rptUOM.DataSource = dsProduct;
            rptUOM.DataBind();
        }
        else
        {
            rptUOM.DataSource = dsProduct;
            rptUOM.DataBind();
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
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    DataTable dtExcelData = new DataTable();
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT ERPCode,ProductName,ProductDescription,ProductCategory,ProductBrand,ProductBaseUOM,ProductUOM,ProductCoversationfactor,GrossWeight,NetWeight,SubDivision,State from[" + sheet1 + "]", excel_con))
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
    private void InsertData()
    {

        string Prod_Null = string.Empty;
        string Prod_SlNo = string.Empty;
        string Prod_Detail_Code = string.Empty;
        string StrCat_Code = string.Empty;
        string StrTerritory_Code = string.Empty;
        string StrBra_Code = string.Empty;
        string StrBra_Name = string.Empty;
        string StrCls_Code = string.Empty;
        string StrSpec_SName = string.Empty;
        string StrCat_Name = string.Empty;
        string StrCls_SName = string.Empty;
        string StrQua_SName = string.Empty;
        string StrQua_Code = string.Empty;
        string StrBaseUOM_Name = string.Empty;
        string StrBaseUOM_Code = string.Empty;
        string StrUOM_Name = string.Empty;
        string StrUOM_Code = string.Empty;
        string StrSt_Code = string.Empty;
        string StrPro_Con = string.Empty;
        string Gweg = string.Empty;
        string Nweg = string.Empty;
        String sql = "";
        try
        {
            conn.Open();
            SqlTransaction objTrans = conn.BeginTransaction();
            String sMsg = "";
            try
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
                    dslstSpec = lstDR.GetProduct_Sl_No(div_code);
                    if (dslstSpec.Tables[0].Rows.Count > 0)
                    {
                        Prod_SlNo = dslstSpec.Tables[0].Rows[0][0].ToString();
                        Prod_SlNo = (Convert.ToInt32(Prod_SlNo) + i).ToString();
                    }

                    dslstDeacod = lstDR.GetProduct_Detail_Code(DivshName, SuDivcode);
                    if (dslstDeacod.Tables[0].Rows.Count > 0)
                    {
                        Prod_Detail_Code = dslstDeacod.Tables[0].Rows[0][0].ToString();
                        Prod_Detail_Code = DivshName + SuDivcode + (Convert.ToInt32(Prod_Detail_Code) + i).ToString();
                    }

                    dslstCat = lstDR.GetProduct_Category_Code(columns[3], div_code, SuDivcode);
                    if (dslstCat.Tables[0].Rows.Count > 0)
                    {
                        StrCat_Code = dslstCat.Tables[0].Rows[0][0].ToString();
                        StrCat_Name = dslstCat.Tables[0].Rows[0][1].ToString();

                    }
                    else
                    {
                        sMsg += "Unable to Upload Category Name:" + columns[3].ToString() + ". Excel Row No : " + (i + 2) + "</br>";

                    }

                    dslstBra = lstDR.GetProduct_Brand_Code(columns[4], div_code, StrCat_Code);
                    if (dslstBra.Tables[0].Rows.Count > 0)
                    {
                        StrBra_Code = dslstBra.Tables[0].Rows[0][0].ToString();
                        StrBra_Name = dslstBra.Tables[0].Rows[0][1].ToString();
                    }
                    else
                    {
                        sMsg += "Unable to Upload Brand Name:Category Name MissMatch" + columns[4].ToString() + ". Excel Row No : " + (i + 2) + "</br>";
                    }

                    dslstPBUOM = lstDR.GetProductBaseUOM(columns[5], div_code);
                    if (dslstPBUOM.Tables[0].Rows.Count > 0)
                    {
                        StrBaseUOM_Code = dslstPBUOM.Tables[0].Rows[0][0].ToString();
                        StrBaseUOM_Name = dslstPBUOM.Tables[0].Rows[0][1].ToString();
                    }
                    else
                    {
                        sMsg += "Unable to Upload BaseUOM Name:" + columns[5].ToString() + ". Excel Row No : " + (i + 2) + "</br>";
                    }

                    dslstBUOM = lstDR.GetProductBaseUOM(columns[6], div_code);
                    if (dslstBUOM.Tables[0].Rows.Count > 0)
                    {
                        StrUOM_Code = dslstBUOM.Tables[0].Rows[0][0].ToString();
                        StrUOM_Name = dslstBUOM.Tables[0].Rows[0][1].ToString();
                    }
                    else
                    {
                        sMsg += "Unable to Upload UOM Name:" + columns[5].ToString() + ". Excel Row No : " + (i + 2) + "</br>";
                    }

                    dslstSt = lstDR.GetProductState(div_code);
                    if (dslstSt.Tables[0].Rows.Count > 0)
                    {
                        StrSt_Code = dslstSt.Tables[0].Rows[0][0].ToString();
                        //StrSt_Code = (Convert.ToInt32(StrSt_Code) + i).ToString();
                    }
                    else
                    {
                        sMsg += "Unable to Upload State Name:" + columns[8].ToString() + ". Excel Row No : " + (i + 2) + "</br>";
                    }

                    StrPro_Con = (columns[5] == columns[6]) ? "1" : columns[7];

                    Gweg = (columns[8] == "") ? "0" : columns[8];

                    Nweg = (columns[9] == "") ? "0" : columns[9];
                    
                    string subdiv = string.Empty;
                    string subdivnms = "";
                    subdivnms = columns[10].ToString();
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Subdiv is" + (subdivnms.Equals(DBNull.Value) ? "NUll" : subdivnms) + "');</script>");
                    if (subdivnms != "" && !subdivnms.Equals(DBNull.Value))
                    {
                        DataTable ds = new DataTable();
                        ds = lstDR.getMultiSubdivision(subdivnms, div_code);
                        if (ds.Rows.Count > 0)
                        {
                            subdiv = ds.Rows[0]["subdiv"].ToString();
                        }
                        else
                        {
                            subdiv = SuDivcode;
                        }
                    }
                    else
                    {
                        subdiv = SuDivcode;
                    }
                    string states = string.Empty;
                    string statenames = "";
                    statenames = columns[11].ToString();
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Subdiv is" + (subdivnms.Equals(DBNull.Value) ? "NUll" : subdivnms) + "');</script>");
                    if (statenames != "" && !statenames.Equals(DBNull.Value))
                    {
                        DataTable ds = new DataTable();
                        ds = lstDR.getMultiStates(statenames);
                        if (ds.Rows.Count > 0)
                        {
                            states = ds.Rows[0]["states"].ToString();
                        }
                        else
                        {
                            states = StrSt_Code;
                        }
                    }
                    else
                    {
                        states = StrSt_Code;
                    }
                    if ((dslstDeacod.Tables[0].Rows.Count > 0) && (dslstBra.Tables[0].Rows.Count > 0) && (dslstCat.Tables[0].Rows.Count > 0) && (dslstBUOM.Tables[0].Rows.Count > 0) && (dslstPBUOM.Tables[0].Rows.Count > 0))
                    {
                        sql += "INSERT INTO Mas_Product_Detail(Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Base_Unit_code,product_unit,Unit_code, " +
                              " Product_Cat_Code,Product_Type_Code,Product_Description, " +
                              " Division_Code,Created_Date,Product_Active_Flag,Prod_Detail_Sl_No,Product_Grp_Code,LastUpdt_Date,state_code,subdivision_code,Product_Mode,Sample_Erp_Code,Sale_Erp_Code,Product_Brd_Code,product_code_slno,product_packsize,product_grosswt,product_netwt) " +
                              " VALUES('" + Prod_Detail_Code + "', N'" + columns[1] + "', '" + StrBaseUOM_Name + "','" + StrBaseUOM_Code + "','" + StrUOM_Name + "','" + StrUOM_Code + "','" + StrCat_Code + "', " +
                              " 'R', N'" + columns[2] + "', " + div_code + ", getdate(), 0, " + Prod_SlNo + ", '" + Prod_Null + "', getdate(),'" + states + "','" + subdiv + "','" + Prod_Null + "','" + StrPro_Con + "','" + columns[0] + "','" + StrBra_Code + "', " + Prod_SlNo + ",'" + Prod_Null + "','" + Gweg + "','" + Nweg + "')";
                        


                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Div:"+ ((dslstDeacod.Tables[0].Rows.Count).ToString()+",Brand:"+(dslstBra.Tables[0].Rows.Count).ToString() + ",Cat:" + (dslstCat.Tables[0].Rows.Count).ToString() + ",UOM:" + (dslstBUOM.Tables[0].Rows.Count).ToString() + ",BASEUOM" + (dslstPBUOM.Tables[0].Rows.Count).ToString()) + ", Excel Row No : " + (i + 2)+"');</script>");
                    }
                }

                if (sMsg == "")
                {
                    try
                    {
                        SqlCommand CmdUpl = conn.CreateCommand();
                        CmdUpl.Transaction = objTrans;

                        CmdUpl.CommandText = sql;
                        CmdUpl.ExecuteNonQuery();
                        objTrans.Commit();
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully...');</script>");
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + sql + "');</script>");
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + sMsg + "';</script>");
                }
            }
            catch (Exception ex)
            {

                objTrans.Rollback();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(" + ex.Message + ");</script>");
            }
            finally
            {
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + sql + "');</script>");

        }
    }

    public bool RecordExistdet(string Product_Detail_Code, string divcode)
    {

        bool bRecordExist = false;
        try
        {


            string strQuery = "SELECT COUNT(Product_Detail_Code) FROM Mas_Product_Detail WHERE Product_Detail_Code='" + Product_Detail_Code + "' AND Division_Code= '" + divcode + "' ";
            SqlCommand cmd = new SqlCommand(strQuery, conn);
            int iRecordExist = Convert.ToInt32(cmd.ExecuteScalar());

            if (iRecordExist > 0)
                bRecordExist = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bRecordExist;
    }
    public bool sRecordExistdet(string Product_Detail_Name, string divcode)
    {

        bool bRecordExist = false;
        try
        {


            string strQry = "SELECT COUNT(Product_Detail_Name) FROM Mas_Product_Detail WHERE Product_Detail_Name='" + Product_Detail_Name + "' AND Division_Code= '" + divcode + "' ";
            SqlCommand cmd = new SqlCommand(strQry, conn);
            int iRecordExist = Convert.ToInt32(cmd.ExecuteScalar());

            if (iRecordExist > 0)
                bRecordExist = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bRecordExist;
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
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Detail Upload Sucessfully');</script>");
                    con.Close();
                }
            }
        }
    }



    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            //Response.Clear();
            //Response.Buffer = true;
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Global.ServerPath + "Document\\Product_upload.xlsx";
            //string fileName = "\\\\10.0.2.30\\SalesJump\\E-Report_DotNet\\Document\\Product_upload.xlsx";
            //string fileName = Server.MapPath("~/Document/Product_upload.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Product_upload.xlsx");
            Response.TransmitFile(fileName);
            //Response.Flush();
            Response.End();

        }

        catch (Exception)
        {



        }

    }

    protected void btnUpload_Click(object sender, ImageClickEventArgs e)
    {
        //UpLoadFile();
        ImporttoDatatable();
        //CheckData();
        if (output > 0)
        {
            InsertData();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Upload!!!');</script>");
        }
    }

    protected void dd1division_SelectedIndexChanged1(object sender, EventArgs e)
    {
        Division dv = new Division();
        dsdiv = dv.getSubDivision_list(dd1division.SelectedValue);
        if (dsdiv.Tables[0].Rows.Count > 0)
        {

            DivshName = dsdiv.Tables[0].Rows[0]["subdivision_sname"].ToString().TrimEnd();
            SuDivcode = dsdiv.Tables[0].Rows[0]["subdivision_code"].ToString();
        }
    }
}