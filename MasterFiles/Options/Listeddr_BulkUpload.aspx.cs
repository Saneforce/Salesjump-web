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
public partial class MasterFiles_Options_Listeddr_BulkUpload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    string strSf_Code = string.Empty;
    DataTable dtListed = null;
    DataSet dsListedDR = null;
    string sfCode = string.Empty;
   
    SqlConnection conn = new SqlConnection(Globals.ConnString);
    DataSet ds=new DataSet();
    DataTable Dt=new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            //FillReporting();
            FillSpl();
            FillCat();
            FillCls();
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

                    //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                    //    dtExcelData.Columns.AddRange(new DataColumn[3] { new DataColumn("SLNo", typeof(int)),
                    //new DataColumn("DoctorName", typeof(string)),
                    //new DataColumn("Addres",typeof(string)) });

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * from [" + sheet1 + "]", excel_con))
                    {
                        //ds = new DataSet();
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
        try
        {
            if (FlUploadcsv.HasFile)
            {
                string StrSpec_Code = string.Empty;
                string StrTerritory_Code = string.Empty;
                string StrCat_Code = string.Empty;
                string StrCls_Code = string.Empty;
                string strUsername = string.Empty;
                string Doc_Type = string.Empty;
                string StrQua_Code = string.Empty;
                string StrSpec_SName = string.Empty;
                string StrCat_SName = string.Empty;
                string StrCls_SName = string.Empty;
                string StrQua_SName = string.Empty;
                string strUploadMessage = string.Empty;
                int ListerDrCode;

                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    DataRow row = Dt.Rows[i];
                    int columnCount = Dt.Columns.Count;
                    string[] columns = new string[columnCount];
                 //   strSf_Code=columns[2].Trim();
                    for (int j = 0; j < columnCount; j++)
                    {
                        columns[j] = row[j].ToString().Trim();
                    }
                    ListedDR objListedDR = new ListedDR();
                    ListerDrCode = objListedDR.GetListedDrCode();
                    Territory terr = new Territory();
                    int terrcode = terr.Getterr_Code();
                    SalesForce sf = new SalesForce();

                    DataSet dsGetLstDocName = new DataSet();
                    Doctor doc = new Doctor();
                    ListedDR lstDR = new ListedDR();

                    dsSalesForce = sf.getSfCode(columns[1]);
                    if (dsSalesForce.Tables[0].Rows.Count > 0)
                    {
                        strUsername = dsSalesForce.Tables[0].Rows[0][0].ToString();
                    }

                    DataSet dslstDR = new DataSet();
                    dslstDR = lstDR.GetTerritory_Upload(columns[12], strUsername, div_code);

                    if (dslstDR.Tables[0].Rows.Count > 0)
                    {
                        StrTerritory_Code = dslstDR.Tables[0].Rows[0][0].ToString();
                    }
                    DataSet dslstCat = new DataSet();
                    dslstCat = lstDR.GetDoc_Cat_Code(columns[15], div_code);
                    if (dslstCat.Tables[0].Rows.Count > 0)
                    {
                        StrCat_Code = dslstCat.Tables[0].Rows[0][0].ToString();
                    }

                    DataSet dsSpec = new DataSet();
                    dsSpec = lstDR.GetCategory_Special_Code(columns[14], div_code);
                    if (dsSpec.Tables[0].Rows.Count > 0)
                    {
                        StrSpec_Code = dsSpec.Tables[0].Rows[0][0].ToString();
                    }

                    DataSet dslstCls = new DataSet();
                    dslstCls = lstDR.GetClass_Code(columns[16], div_code);
                    if (dslstCls.Tables[0].Rows.Count > 0)
                    {
                        StrCls_Code = dslstCls.Tables[0].Rows[0][0].ToString();
                    }
                    conn.Open();
                    if (dslstDR.Tables[0].Rows.Count == 0)
                    {

                        string strQry = "insert into Mas_Territory_Creation (Territory_Code,Territory_Name,Territory_Cat,Territory_Sname,division_code, " +
                            " SF_Code,Territory_Active_Flag,Created_date) " +
                            " VALUES('" + terrcode + "', '" + columns[12].Trim() + "' , '" + columns[13] + "' , null, " +
                            " '" + div_code + "', '" + strUsername + "', 0, getdate())";
                        SqlCommand cmd1 = new SqlCommand(strQry, conn);
                        cmd1.ExecuteNonQuery();
                    }

                    DataSet dsTerritory = new DataSet();
                    string strTerritoryName = "";
                    dsTerritory = lstDR.getListedDr_TerritoryName(columns[12].Trim(), strUsername);
                    if (dsTerritory.Tables[0].Rows.Count > 0)
                    {
                        strTerritoryName = dsTerritory.Tables[0].Rows[0][0].ToString();
                    }

                    //if (chkDeact.Checked == true)
                    //{
                    //    conn.Open();
                    //    string sql = "update Mas_ListedDr set ListedDr_Active_Flag = 1,listeddr_deactivate_date = getdate() where ListedDr_Active_Flag =0 and Division_Code = '" + div_code + "' and SF_Code = '" + strUsername + "' ";
                    //    SqlCommand cmd = new SqlCommand(sql, conn);
                    //    cmd.ExecuteNonQuery();
                    //    conn.Close();
                    //}
                    
                    dsGetLstDocName = doc.getDocLstName(columns[2], div_code, columns[3], strUsername, strTerritoryName, columns[9].Trim(), StrSpec_Code, StrCat_Code, StrCls_Code);
                    if (dsGetLstDocName.Tables[0].Rows.Count == 0 && columns[1] != "" && columns[1] != "0")
                    {


                        DataSet dslstSpec = new DataSet();
                        dslstSpec = lstDR.GetCategory_Special_Code(columns[14], div_code);
                        if (dslstSpec.Tables[0].Rows.Count > 0)
                        {
                            StrSpec_Code = dslstSpec.Tables[0].Rows[0][0].ToString();
                            StrSpec_SName = dslstSpec.Tables[0].Rows[0][1].ToString();
                        }
                      //  DataSet dslstCat = new DataSet();
                        dslstCat = lstDR.GetDoc_Cat_Code(columns[15], div_code);
                        if (dslstCat.Tables[0].Rows.Count > 0)
                        {
                            StrCat_Code = dslstCat.Tables[0].Rows[0][0].ToString();
                            StrCat_SName = dslstCat.Tables[0].Rows[0][1].ToString();
                        }


                       // DataSet dslstCls = new DataSet();
                        dslstCls = lstDR.GetClass_Code(columns[16], div_code);
                        if (dslstCls.Tables[0].Rows.Count > 0)
                        {
                            StrCls_Code = dslstCls.Tables[0].Rows[0][0].ToString();
                            StrCls_SName = dslstCls.Tables[0].Rows[0][1].ToString();
                        }

                        DataSet dslstQua = new DataSet();
                        dslstQua = lstDR.GetQua_Upload(columns[9].Trim(), div_code);
                        if (dslstQua.Tables[0].Rows.Count > 0)
                        {
                            StrQua_Code = dslstQua.Tables[0].Rows[0][0].ToString();
                            StrQua_SName = dslstQua.Tables[0].Rows[0][1].ToString();
                        }



                        if ((dsSalesForce.Tables[0].Rows.Count > 0) && (dslstSpec.Tables[0].Rows.Count > 0) && (dslstCat.Tables[0].Rows.Count > 0) && (dslstCls.Tables[0].Rows.Count > 0))
                        {
                            if (dslstQua.Tables[0].Rows.Count == 0)
                            {
                                string strQry1 = "INSERT INTO Mas_Doc_Qualification(Division_Code,Doc_QuaSName,Doc_QuaName,Doc_Qua_ActiveFlag,Created_Date,LastUpdt_Date)" +
                                         "values('" + div_code + "', '" + columns[9].Trim() + "' , '" + columns[9].Trim() + "' ,0,getdate(),getdate())";
                                SqlCommand cmd2 = new SqlCommand(strQry1, conn);
                                cmd2.ExecuteNonQuery();

                            }
                            dslstQua = lstDR.GetQua_Upload(columns[9].Trim(), div_code);
                            if (dslstQua.Tables[0].Rows.Count > 0)
                            {
                                StrQua_Code = dslstQua.Tables[0].Rows[0][0].ToString();
                                StrQua_SName = dslstQua.Tables[0].Rows[0][1].ToString();
                            }



                            dslstDR = lstDR.GetTerritory_Upload(columns[12], strUsername, div_code);
                            if (dslstDR.Tables[0].Rows.Count > 0)
                            {
                                StrTerritory_Code = dslstDR.Tables[0].Rows[0][0].ToString();
                            }
                             columns[2] = columns[2].Replace("  ", " ");
                             DateTime dtDob =new DateTime();
                            string dtDocDow ="";
                            string dtDocDob="";

                             if (columns[6] != "")
                             {
                                 dtDob = Convert.ToDateTime(columns[6]);
                                  dtDocDob= dtDob.Month + "-" + dtDob.Day + "-" + dtDob.Year;
                             }
                             if (columns[7] != "")
                             {
                                 DateTime dtDow = new DateTime();
                                 dtDow = Convert.ToDateTime(columns[7]);
                                 dtDocDow = dtDow.Month + "-" + dtDow.Day + "-" + dtDow.Year;
                             }
                             string sql = "insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,ListedDr_Hospital,Hospital_Address,ListedDr_DOB,ListedDr_DOW, ListedDr_Email,Doc_QuaCode,ListedDr_Phone,ListedDr_Mobile,Territory_Code,Doc_Special_Code,Doc_Cat_Code,Doc_ClsCode, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,Visit_Hours,visit_days,ListedDr_Sl_No,SLVNo,LastUpdt_Date, Doc_Type,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Qua_Name,Doc_Class_ShortName, Doctor_POB, ListedDr_Sex, No_of_Visit, State_Code, visit_Session,Visit_Freq, ListedDr_Fax,ListedDr_website ) ";
                             sql += "VALUES('" + ListerDrCode + "', '" + strUsername + "', '" + columns[2].Trim() + "', '" + columns[3].Trim().Replace(" ", "") + "','" + columns[4] + "','" + columns[5] + "','" + dtDocDob + "','" + dtDocDow + "','" + columns[8] + "','" + StrQua_Code + "','" + columns[10] + "','" + columns[11] + "', '" + StrTerritory_Code + "', '" + StrSpec_Code + "','" + StrCat_Code + "', '" + StrCls_Code + "', 0, getdate(),'" + div_code + "', '','','" + ListerDrCode + "' ,'" + ListerDrCode + "',getdate(), '','" + StrCat_SName.Trim() + "','" + StrSpec_SName.Trim() + "','" + StrQua_SName.Trim() + "','" + StrCls_SName.Trim() + "', '" + columns[17] + "', '" + columns[18] + "', '" + columns[19] + "', '" + columns[20] + "', '" + columns[21] + "', '" + columns[22] + "', '" + columns[23] + "', '" + columns[24] + "')";
                            SqlCommand cmd = new SqlCommand(sql, conn);
                            cmd.ExecuteNonQuery();

                            strUploadMessage = "Doctor Uploaded Sucessfully";
                            conn.Close();
                        }
                        else
                        {


                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Doctor List Invalid');</script>");

                            strUploadMessage = "Doctor List Invalid";
                            conn.Close();
                            break;
                        }


                    }
                    conn.Close();

                }
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Doctor Uploaded Sucessfully');</script>");
            }
            
        }
        catch (Exception ex)
        {
            Response.Redirect(ex.Message);
        }
    }
    protected void chkDeact_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        
        ImporttoDatatable();
        InsertData();
        //if (chkDeact.Checked == true)
        //{
        //    Deactivate();
        //}
      
    }
    //private void Deactivate()
    //{
    //    if (chkDeact.Checked == true)
    //    {
    //        conn.Open();
    //        string sql = "update Mas_ListedDr set ListedDr_Active_Flag = 1,listeddr_deactivate_date = getdate() where ListedDr_Active_Flag =0 and Division_Code = '" + div_code + "' and SF_Code = '" + strSf_Code + "' ";
    //        SqlCommand cmd = new SqlCommand(sql, conn);
    //        cmd.ExecuteNonQuery();
    //        conn.Close();
    //    }
    //}
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {

            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\UPL_Doctor_Bulk.xls");
            Response.AppendHeader("Content-Disposition", "attachment; filename=UPL_Doctor_Bulk.xls");
            Response.TransmitFile(fileName);
            Response.End();

        }

        catch (Exception ex)
        {

            // lblMessage.Text = ex.Message;

        }
    }

   
    private void FillSpl()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.Speciality_doc(div_code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            rptSpec.DataSource = dsListedDR;
            rptSpec.DataBind();
        }
        else
        {
            rptSpec.DataSource = dsListedDR;
            rptSpec.DataBind();
        }

    }
    private void FillCls()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.Class_doc(div_code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            rptcls.DataSource = dsListedDR;
            rptcls.DataBind();
        }
        else
        {
            rptcls.DataSource = dsListedDR;
            rptcls.DataBind();
        }

    }

    private void FillCat()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.Category_doc(div_code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            rptCat.DataSource = dsListedDR;
            rptCat.DataBind();
        }

    }
}