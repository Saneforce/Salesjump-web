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
public partial class MasterFiles_Mas_Claim_Upload : System.Web.UI.Page
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
        string msg = string.Empty;
        string sheet2 = string.Empty;
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
                    sheet2 = sheet1;
                    DataTable dtExcelData = new DataTable();
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT ClaimId,Action,Pending_Status,Rejectreason,SchemePeriod,RaisedDate,Products,Quantity from[" + sheet1 + "]", excel_con))
                    {
                        ds = new DataSet();
                        oda.Fill(ds);
                        Dt = ds.Tables[0];
                    }
                    excel_con.Close();
                }
                string consString = Globals.ConnString;
                DateTime aDate = DateTime.Now;
                string today = aDate.ToString("yyyy-MM-dd HH:mm:ss");
                Dt.Columns.AddRange(new DataColumn[1] { new DataColumn("Date", typeof(string)) });
                foreach (DataRow dr in Dt.Rows)
                {
                    dr["Date"] = today;
                }
                if (sheet2 == "Pending$")
                {
                    using (SqlConnection con = new SqlConnection(consString))
                    {
                        try
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                sqlBulkCopy.DestinationTableName = "dbo.Claim_Excel_Backup";
                                sqlBulkCopy.ColumnMappings.Add("ClaimId", "ClaimId");
                                sqlBulkCopy.ColumnMappings.Add("Action", "Admin_Action");
                                sqlBulkCopy.ColumnMappings.Add("Pending_Status", "Pending_Status");
                                sqlBulkCopy.ColumnMappings.Add("Rejectreason", "Rejectreason");
                                sqlBulkCopy.ColumnMappings.Add("SchemePeriod", "SchemePeriod");
                                sqlBulkCopy.ColumnMappings.Add("RaisedDate", "RaisedDate");
                                sqlBulkCopy.ColumnMappings.Add("Products", "Products");
                                sqlBulkCopy.ColumnMappings.Add("Quantity", "Quantity");
                                sqlBulkCopy.ColumnMappings.Add("Date", "Created_Date");
                                con.Open();
                                sqlBulkCopy.WriteToServer(Dt);
                                con.Close();
                                msg = "Upload Success";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                    new SqlParameter("@Div",div_code),
                    new SqlParameter("@SF",sfCode)
                    };
                    if (msg == "Upload Success")
                    {
                        using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString()))
                        {
                            using (SqlCommand cmd = con.CreateCommand())
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "updateClaimStatus";
                                cmd.Parameters.AddRange(parameters);
                                try
                                {
                                    if (con.State != ConnectionState.Open)
                                    {
                                        con.Open();
                                    }
                                    cmd.ExecuteNonQuery();
                                    msg = "Success";
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                        }
                    }
                    if (msg == "Success")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully...');</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Pending Excel File...');</script>");
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
        }
    }
    private void InsertData()
    {

        //       string Str_Null = string.Empty;
        //       string Retail_Code = string.Empty;
        //       string User_Def = string.Empty;
        //       string StrTerr_Code = string.Empty;
        //       string StrTerritory_Code = string.Empty;
        //       string StrDis_Code = string.Empty;
        //       string StrDis_Name = string.Empty;
        //       string StrCls_Code = string.Empty;
        //       string StrSpec_SName = string.Empty;
        //       string StrTerr_Name = string.Empty;
        //       string StrCls_SName = string.Empty;
        //       string StrQua_SName = string.Empty;
        //       string StrQua_Code = string.Empty;
        //       string StrChannel_Name = string.Empty;
        //       string StrChannel_Code = string.Empty;
        //       string StrClass_Name = string.Empty;
        //       string StrClass_Code = string.Empty;
        //       string StrSt_Code = string.Empty;
        //       string StrPro_Con = string.Empty;
        //       try
        //       {
        //           conn.Open();
        //           SqlTransaction objTrans = conn.BeginTransaction();
        //           String sql = ""; String sMsg = "";
        //           String  column1 = ""; String column2 = "";
        //           try
        //           {
        //               for (int i = 0; i < Dt.Rows.Count; i++)
        //               {
        //                   DataRow row = Dt.Rows[i];
        //                   int columnCount = Dt.Columns.Count;
        //                   string[] columns = new string[columnCount];
        //                   for (int j = 0; j < columnCount; j++)
        //                   {
        //                       columns[j] = row[j].ToString();
        //                   }
        //                   int RowNo = i + 2;
        //                   CallPlan cplan = new CallPlan();
        //                   DataSet dslstDR = new DataSet();

        //                   switch (columns[1])
        //                   {
        //                       case "Pending":
        //                           column1 = "0";
        //                        break;
        //                       case "Approved":
        //                           column1 = "1";
        //                               break;
        //                       case "Rejected":
        //                           column1 = "2";
        //                           break;
        //                       default:
        //                           Console.WriteLine("Action Mismatch");
        //                           break;
        //                   }
        //                   switch (columns[3])
        //                   {
        //                       case "Pending":
        //                           column2 = "0";
        //                           break;
        //                       case "Approved":
        //                           column2 = "1";
        //                           break;
        //                       case "Rejected":
        //                           column2 = "2";
        //                           break;
        //                       default:
        //                           Console.WriteLine("Status Mismatch");
        //                           break;
        //                   }
        //                   if ((columns[1] == "Approved") && (columns[3] == "Pending") || (columns[1] == "Rejected") && (columns[3] == "Pending") || (columns[1] == "Approved") && (columns[3] == "Rejected"))
        //                   {
        //                       ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Action Denial...');</script>");

        //                   }
        //                   else
        //                   {
        //                       int iReturn = cplan.Getclaim_upload(columns[0], column1, columns[2], column2, columns[4], div_code);

        //                       if (iReturn > 0)
        //                       {
        //                           ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully...');</script>");
        //                       }
        //                   }
        //}

        //               if (sMsg == "")
        //               {
        //                   SqlCommand CmdUpl = conn.CreateCommand();
        //                   CmdUpl.Transaction = objTrans;

        //                   CmdUpl.CommandText = sql;
        //                   CmdUpl.ExecuteNonQuery();
        //                   objTrans.Commit();
        //                   ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully...');</script>");

        //               }
        //               else
        //               {
        //                   ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + sMsg + "';</script>");
        //               }


        //           }
        //           catch (Exception ex)
        //           {

        //               objTrans.Rollback();
        //               ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + sMsg + "';</script>");
        //           }
        //           finally
        //           {
        //               conn.Close();
        //           }
        //       }
        //       catch (Exception ex)
        //       {
        //           ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + ex.Message + "';</script>");

        //       }
    }


    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        //try
        //{

        //    Response.ContentType = "application/vnd.ms-excel";
        //    string fileName = Server.MapPath("~\\Document\\Mas_Claim_Upload.xlsx");
        //    Response.AppendHeader("Content-Disposition", "attachment; filename=Mas_Claim_Upload.xlsx");
        //    Response.TransmitFile(fileName);
        //    Response.End();

        //}

        //catch (Exception)
        //{



        //}

    }

    protected void btnUpload_Click(object sender, ImageClickEventArgs e)
    {
        //UpLoadFile();
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        ImporttoDatatable();
        InsertData();

    }
}