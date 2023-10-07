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
public partial class MasterFiles_Options_Product_Upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsProduct = null;
    DataSet dslstSpec = new DataSet();
    DataSet dslstCat = new DataSet();
    DataSet dslstCls = new DataSet();
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
         
            FillGroup();
            FillCat();
            FillBrand();
        }
    }

    private void FillGroup()
    {
        Product prd = new Product();
        dsProduct = prd.getProductGroup_UP(div_code);
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
            rptBrd.DataSource = dsProduct;
            rptBrd.DataBind();
        }
        else
        {
            rptBrd.DataSource = dsProduct;
            rptBrd.DataBind();
        }
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {

            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\UPL_Product_Master.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=UPL_Product_Master.xlsx");
            Response.TransmitFile(fileName);
            Response.End();

        }

        catch (Exception ex)
        {

            // lblMessage.Text = ex.Message;

        }
    }
    protected void chkDeact_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (chkDeact.Checked == true)
        {
            Deactivate();
        }
        ImporttoDatatable();
        InsertData();
    }

    private void Deactivate()
    {
        if (chkDeact.Checked == true)
        {
            conn.Open();
            string sql = "update Mas_Product_Detail set Product_Active_Flag = 1,Product_Deactivate_dt = getdate() where Product_Active_Flag =0 and Division_Code = '" + div_code + "' ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
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
        string StrCat_Code = string.Empty;
        string StrGrp_Code = string.Empty;
        string StrBrd_Code = string.Empty;
        string strState = string.Empty;
        string stateName = string.Empty;
        string subdivCode = string.Empty;
        string SubName = string.Empty;
        DataSet dsprodcat = new DataSet();
        DataSet dsprodgrp = new DataSet();
        DataSet dsprodbrd = new DataSet();
        DataSet dsState = new DataSet();
        DataSet dsDesignation = new DataSet();
        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            DataRow row = Dt.Rows[i];
            int columnCount = Dt.Columns.Count;
            string[] columns = new string[columnCount];
            for (int j = 0; j < columnCount; j++)
            {
                columns[j] = row[j].ToString();
            }
            Product prod = new Product();
            int ProductSlNO = prod.GetProductCode();
           
            Product pro = new Product();
            
            dsprodcat = pro.GetProd_Cat_Code(columns[8].Trim(), div_code);
            if (dsprodcat.Tables[0].Rows.Count > 0)
            {
                StrCat_Code = dsprodcat.Tables[0].Rows[0][0].ToString();           

            }

            dsprodgrp = pro.GetProd_Grp_Code(columns[9].Trim(), div_code);
            if (dsprodgrp.Tables[0].Rows.Count > 0)
            {
                StrGrp_Code = dsprodgrp.Tables[0].Rows[0][0].ToString();
              
            }
            dsprodbrd = pro.GetProd_Brd_Code(columns[10].Trim(), div_code);
            if (dsprodbrd.Tables[0].Rows.Count > 0)
            {
                StrBrd_Code = dsprodbrd.Tables[0].Rows[0][0].ToString();
            
            }


            State st = new State();

            stateName = "";
            string[] strDivSplit = columns[15].Split('/');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsState = st.getState_Code(strdiv);
                    if (dsState.Tables[0].Rows.Count > 0)
                    {
                        strState = dsState.Tables[0].Rows[0][0].ToString();
                    }
                    stateName += strState + ',';
                }
            }



            SubDivision sub = new SubDivision();
            DataSet dsSubdiv = new DataSet();

            SubName = "";
            string[] strsubSplit = columns[16].Split('/');
            foreach (string strsub in strsubSplit)
            {
                if (strsub != "")
                {

                    dsSubdiv = sub.GetSubdiv_Code(strsub, div_code);
                    if (dsSubdiv.Tables[0].Rows.Count > 0)
                    {
                        subdivCode = dsSubdiv.Tables[0].Rows[0][0].ToString();
                    }
                    SubName += subdivCode + ',';
                }
            }
           
       
            conn.Open();
            if ((dsprodcat.Tables[0].Rows.Count > 0) && (dsprodgrp.Tables[0].Rows.Count > 0) && (dsprodbrd.Tables[0].Rows.Count > 0))
            {


                string sql = "INSERT INTO Mas_Product_Detail(Product_Detail_Code,Product_Detail_Name,Product_Description,Product_Sale_Unit,Product_Sample_Unit_One," +
                            " Product_Sample_Unit_Two,Product_Sample_Unit_Three,Product_Cat_Code,Product_Grp_Code,Product_Brd_Code,Product_Type_Code, " +
                            " Division_Code,Created_Date,Product_Active_Flag,Prod_Detail_Sl_No,LastUpdt_Date,state_code,subdivision_code,Product_Mode,Sample_Erp_Code,Sale_Erp_Code, Product_Code_SlNo) " +
                            " VALUES('" + columns[1].Trim() + "', '" + columns[2].Trim() + "', '" + columns[3] + "', '" + columns[4] + "', " +
                            " '" + columns[5] + "', '" + columns[6] + "', '" + columns[7] + "', " +
                            " '" + StrCat_Code + "', '" + StrGrp_Code + "','" + StrBrd_Code + "', '" + columns[11] + "', " + div_code + ", getdate(), 0, " + columns[17] + ",getdate(),'" + stateName + "','" + SubName + "','" + columns[12] + "','" + columns[13] + "','" + columns[14] + "','" + ProductSlNO + "') ";



              
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Uploaded Sucessfully');</script>");
                conn.Close();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Upload');</script>");
                conn.Close();
            }
        }
    }
}