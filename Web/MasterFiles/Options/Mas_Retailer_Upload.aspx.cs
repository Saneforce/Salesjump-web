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


public partial class MasterFiles_Options_Mas_Retailer_Upload : System.Web.UI.Page
{

    string div_code = string.Empty;
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
    DataSet dslstBUOM1 = new DataSet();
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
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    DataTable dtExcelData = new DataTable();
                    //if (Convert.ToString(div_code) == "70")
                    //{
                    //    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT CustomerName,Customerroute,Customeraddress1,Customeraddress2,CustomerMobile,CustomerCategory,CustomerClassname,Territory,CustomerContactPerson,DFDairyMP,MonthlyAI,CurrentCompetitor,AITMP,MCCNFPM,MCCMilkColDaily,Breed,NoOfAnimal,VetsMP,PotentialFSD,FrequencyOfVisit from [" + sheet1 + "]", excel_con))
                    //    {
                    //        ds = new DataSet();
                    //        oda.Fill(ds);
                    //        Dt = ds.Tables[0];
                    //    }
                    //    excel_con.Close();
                    //}
                  //  else
//{

                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Retailername,Retailerroute,Retaileraddress1,Retaileraddress2,RetailerMobile,Retailerchannelname,RetailerClassname,Territory,RetailerContactPerson,GST_NO from [" + sheet1 + "]", excel_con))
                        {
                            ds = new DataSet();
                            oda.Fill(ds);
                            Dt = ds.Tables[0];
                        }
                        excel_con.Close();
                  //  }
                }
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Sucessfully');</script>");
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
        }
    }
    private void InsertData()
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
        string StrHq_Name = string.Empty;
        string StrHq_Code = string.Empty;
        string currentcom = string.Empty;
        string fzyofvisit = string.Empty;
        DataSet dsCC = new DataSet();
        DataSet dsfzy = new DataSet();
        string sfcode = "";
        string distname = "";

        try
        {
            conn.Open();
            SqlTransaction objTrans = conn.BeginTransaction();
            String sql = " declare @slno varchar(10);declare @ukey varchar(100);"; String sMsg = "";
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
					loc lstDR1=new loc();
                    DataSet dslstDR = new DataSet();
                    dslstSpec = lstDR.GetRetail_Sl_No(div_code);
                    if (dslstSpec.Tables[0].Rows.Count > 0)
                    {
                        Retail_Code = dslstSpec.Tables[0].Rows[0][0].ToString();
                        Retail_Code = (Convert.ToInt32(Retail_Code) + i).ToString();
                    }
                    dslstDeacod = lstDR.GetRetailer_Detail_Code(DivshName, SuDivcode);
                    if (dslstDeacod.Tables[0].Rows.Count > 0)
                    {
                        User_Def = dslstDeacod.Tables[0].Rows[0][0].ToString();
                        User_Def = (Convert.ToInt32(User_Def) + i).ToString();
                    }



                    dslstBra = lstDR.GetRetailer_Hq(columns[7], div_code);
                    if (dslstBra.Tables[0].Rows.Count > 0)
                    {
                        StrHq_Code = dslstBra.Tables[0].Rows[0][0].ToString();
                        StrHq_Name = dslstBra.Tables[0].Rows[0][1].ToString();
                    }
                    else
                    {
                        sMsg += "Unable to Upload Distribution Name:" + columns[7].ToString() + ". Excel Row No : " + (i + 2) + "</br>";
                        // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Upload Salesforce Name:" + columns[5].ToString() + ". Row : " + (i+2) + "');</script>");
                        //return;
                    }

                    dslstCat = lstDR.GetRetailer_Route(columns[1], StrHq_Code, div_code, SuDivcode);
                    if (dslstCat.Tables[0].Rows.Count > 0)
                    {
                        StrTerr_Code = dslstCat.Tables[0].Rows[0][0].ToString();
                        StrTerr_Name = dslstCat.Tables[0].Rows[0][1].ToString();

                    }
                    else
                    {
                        sMsg += "Unable to Upload Route Name:" + columns[1].ToString() + ". Excel Row No : " + (i + 2) + "</br>";
                        // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Upload Salesforce Name:" + columns[5].ToString() + ". Row : " + (i+2) + "');</script>");
                        //return;
                    }

                    dslstPBUOM = lstDR.GetRetailer_channel(columns[5], div_code);
                    if (dslstPBUOM.Tables[0].Rows.Count > 0)
                    {
                        StrChannel_Code = dslstPBUOM.Tables[0].Rows[0][0].ToString();
                        StrChannel_Name = dslstPBUOM.Tables[0].Rows[0][1].ToString();
                    }
                    else
                    {
                        sMsg += "Unable to Upload Channel Name:" + columns[5].ToString() + ". Excel Row No : " + (i + 2) + "</br>";
                        // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Upload Salesforce Name:" + columns[5].ToString() + ". Row : " + (i+2) + "');</script>");
                        //return;
                    }

                    dslstBUOM = lstDR.GetRetailer_Class(columns[6], div_code);
                    if (dslstBUOM.Tables[0].Rows.Count > 0)
                    {
                        StrClass_Code = dslstBUOM.Tables[0].Rows[0][0].ToString();
                        StrClass_Name = dslstBUOM.Tables[0].Rows[0][1].ToString();
                    }
                    else
                    {
                        sMsg += "Unable to Upload Class Name:" + columns[6].ToString() + ". Excel Row No : " + (i + 2) + "</br>";
                        // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Upload Salesforce Name:" + columns[5].ToString() + ". Row : " + (i+2) + "');</script>");
                        //return;
                    }

                    dslstBUOM1 = lstDR1.GetRetailer_sf(StrTerr_Code, div_code);

                    foreach (DataRow dd in dslstBUOM1.Tables[0].Rows)
                    {
                        sfcode = dd["sf_code"].ToString();
                        distname = dd["Dist_Name"].ToString();
                        div_code = dd["Division_Code"].ToString();
                    }
                    //if (Convert.ToString(div_code) == "70")
                    //{
                    //    dsCC = lstDR.getCurrentcom(columns[11]);
                    //    if (dsCC.Tables[0].Rows.Count > 0)
                    //    {
                    //        currentcom = dsCC.Tables[0].Rows[0][0].ToString();
                    //        // User_Def = (Convert.ToInt32(User_Def) + i).ToString();
                    //    }
                    //    dsfzy = lstDR.getFryidforxl(columns[19]);
                    //    if (dsfzy.Tables[0].Rows.Count > 0)
                    //    {
                    //        fzyofvisit = dsfzy.Tables[0].Rows[0][0].ToString();
                    //        //  User_Def = (Convert.ToInt32(User_Def) + i).ToString();
                    //    }
                    //    if ((dslstDeacod.Tables[0].Rows.Count > 0) && (dslstBra.Tables[0].Rows.Count > 0) && (dslstCat.Tables[0].Rows.Count > 0) && (dslstPBUOM.Tables[0].Rows.Count > 0) && (dslstPBUOM.Tables[0].Rows.Count > 0) && (dslstBUOM.Tables[0].Rows.Count > 0))
                    //    {
                    //        //strQry = "select   'EK" + sfcode + "-'+  replace(convert(varchar, getdate(),101),'/','') + replace(convert(varchar, getdate(),108),':','') as ukey ";
                    //        //string UKey = db.Exec_Scalar_s(strQry).ToString();
                    //        sql += " SELECT @slno=ISNULL(max(ISNULL(ListedDr_Sl_No, 0)),0) + 1 from  Mas_listeddr where division_code = '" + div_code + "' and Territory_Code = '" + StrTerr_Code + "'; select @ukey='EK" + sfcode + "-'+  replace(convert(varchar, getdate(),101),'/','') + replace(convert(varchar, getdate(),108),':','');" +
                    //            " insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Mobile,Code,Contact_Person_Name,Doc_Special_Code,Doc_Spec_ShortName, " +
                    //                                               " Tin_No,sales_Taxno,Territory_Code,Credit_Days,Doc_ClsCode,Doc_Class_ShortName,Advance_amount,ListedDr_Address1,ListedDr_Address2,Division_Code, ListedDr_Active_Flag, ListedDr_Created_Date, " +
                    //                                               " LastUpdt_Date,Milk_Potential,UOM,UOM_Name,TERRCODE,DIST_NAME,contactperson,ListedDr_Sl_No,Breed,NoOfAnimal,UKey,Entry_Mode) " +
                    //                                               " VALUES('" + Retail_Code + "', '" + sfcode + "', '" + columns[0] + "', '" + columns[4] + "', '" + User_Def + "', '" + columns[8] + "', " +
                    //                                               " '" + StrChannel_Code + "','" + StrChannel_Name + "','" + Str_Null + "','" + Str_Null + "','" + StrTerr_Code + "','" + Str_Null + "','" + StrClass_Code + "', " +
                    //                                               " '" + StrClass_Name + "','" + Str_Null + "','" + columns[2] + "','" + columns[3] + "', '" + div_code + "',0,getdate(),getdate(),'" + Str_Null + "','" + Str_Null + "','" + Str_Null + "'," +
                    //                                               "'" + StrHq_Code + "','" + distname + "','" + columns[8] + "',@slno,'" + columns[15] + "','" + columns[16] + "',@ukey,'Web')";
                    //        sql += "insert into NewContact_Dr(Ukey, FormarName, DFDairyMP, MonthlyAI, AITCU, AITMP, MCCNFPM, MCCMilkColDaily,  VetsMP, CreatedDate, PotentialFSD, ListedDrCode, CustomerCategory,  CurrentlyUFSD, FrequencyOfVisit) " +
                    //            "values( @ukey,'" + columns[0] + "','" + columns[9] + "','" + columns[10] + "','" + columns[11] + "','" + columns[12] + "','" + columns[13] + "','" + columns[14] + "','" + columns[17] + "',getdate(),'" + columns[18] + "','" + Retail_Code + "','" + StrChannel_Code + "','" + currentcom + "','" + fzyofvisit + "')";

                    //    }
                    //}
                    //else
                    //{

                    //conn.Open();

                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('"+ dslstDeacod.Tables[0].Rows.Count+" && "+dslstBra.Tables[0].Rows.Count+" && "+dslstCat.Tables[0].Rows.Count+" && "+dslstPBUOM.Tables[0].Rows.Count+" && "+dslstPBUOM.Tables[0].Rows.Count+" && "+dslstBUOM.Tables[0].Rows.Count+"');</script>");
                    if ((dslstDeacod.Tables[0].Rows.Count > 0) && (dslstBra.Tables[0].Rows.Count > 0) && (dslstCat.Tables[0].Rows.Count > 0) && (dslstPBUOM.Tables[0].Rows.Count > 0) && (dslstPBUOM.Tables[0].Rows.Count > 0) && (dslstBUOM.Tables[0].Rows.Count > 0))
                        {

                            sql += " SELECT @slno=ISNULL(max(ISNULL(ListedDr_Sl_No, 0)),0) + 1 from  Mas_listeddr where division_code = '" + div_code + "' and Territory_Code = '" + StrTerr_Code + "';  insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Mobile,Code,Contact_Person_Name,Doc_Special_Code,Doc_Spec_ShortName, " +
                                        " Tin_No,sales_Taxno,Territory_Code,Credit_Days,Doc_ClsCode,Doc_Class_ShortName,Advance_amount,ListedDr_Address1,ListedDr_Address2,Division_Code, ListedDr_Active_Flag, ListedDr_Created_Date, " +
                                        " LastUpdt_Date,Milk_Potential,UOM,UOM_Name,TERRCODE,DIST_NAME,contactperson,ListedDr_Sl_No) " +
                                        " VALUES('" + Retail_Code + "', '" + sfcode + "', '" + columns[0] + "', '" + columns[4] + "', '" + User_Def + "', '" + columns[8] + "', " +
                                        " '" + StrChannel_Code + "','" + StrChannel_Name + "','" + Str_Null + "','" + columns[9] + "','" + StrTerr_Code + "','" + Str_Null + "','" + StrClass_Code + "', " +
                                        " '" + StrClass_Name + "','" + Str_Null + "','" + columns[2] + "','" + columns[3] + "', '" + div_code + "',0,getdate(),getdate(),'" + Str_Null + "','" + Str_Null + "','" + Str_Null + "','" + StrHq_Code + "','" + distname + "','" + columns[8] + "',@slno)";



                        }
                   // }
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
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + ex.Message + "';</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + sMsg + "';</script>");
                }

            }
            catch (Exception ex)
            {

                objTrans.Rollback();
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + ex.Message + "';</script>");
            }
            finally
            {
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + ex.Message + "';</script>");

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
			 if (Convert.ToString(div_code) == "70")
            {
				 Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.ContentType = "application/vnd.ms-excel";
            //string fileName = Global.ServerPath + "\\Document\\Document\\Mas_Customer_Upload.xlsx";
            string fileName = Server.MapPath("~/Document/Mas_Customer_Upload.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Mas_Customer_Upload.xlsx");            
            Response.TransmitFile(fileName);
            Response.Flush();
            Response.End();
			}
			else{
			
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.ContentType = "application/vnd.ms-excel";
            //string fileName = Global.ServerPath + "\\Document\\Document\\Retailer_Upload.xlsx";
            string fileName = Server.MapPath("~/Document/Mas_Retailer_Upload.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Mas_Retailer_Upload.xlsx");            
            Response.TransmitFile(fileName);
            Response.Flush();
            Response.End();
			}

        }

        catch (Exception)
        {

          

        }
		
		
		
        //try
        //{
         //   if (Convert.ToString(div_code) == "70")
         //   {
               // Response.ContentType = "application/vnd.ms-excel";
               // string fileName = Server.MapPath("~\\Document\\Mas_Customer_Upload.xlsx");
              //  Response.AppendHeader("Content-Disposition", "attachment; filename=Mas_Retailer_Upload.xlsx");
              //  Response.TransmitFile(fileName);
              //  Response.End();

           // }
           // else
           // {	

                //Response.ContentType = "application/vnd.ms-excel";
				//Response.AppendHeader("Content-Disposition", "attachment; filename=Retailer_Upload.xlsx");
				//Response.TransmitFile(Server.MapPath("~/Document/Retailer_Upload.xlsx"));
				//Response.End();
		
                //Response.ContentType = "application/vnd.ms-excel";
                //string fileName = Global.ServerPath + "\\Document\\Document\\Retailer_Upload.xlsx";
                //Response.AppendHeader("Content-Disposition", "attachment; filename=Retailer_Upload.xlsx");
                //Response.TransmitFile(fileName);
                //Response.End();

          //  }
        //}
        //catch (Exception)
        //{



        //}

    }

    protected void btnUpload_Click(object sender, ImageClickEventArgs e)
    {
        //UpLoadFile();
        Button1.Visible = false;
        ImporttoDatatable();
        InsertData();
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
	
	  public class loc
    {
        public DataSet GetRetailer_sf(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            string strQry = "select sf_code,Dist_Name,Division_Code from  Mas_Territory_Creation where cast(Territory_Code as varchar) ='" + Doc_Special_Name + "' and Territory_Active_Flag=0";

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