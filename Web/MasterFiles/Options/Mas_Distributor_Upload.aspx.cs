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
using System;
using System.IO;
using System.Configuration;
using System.Data.Common;
using DBase_EReport;

public partial class MasterFiles_Options_Mas_Distributor_Upload : System.Web.UI.Page
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
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT ERPCode,DistributorName,DistributorTerritory,DistributorDistrict,DistributorState,ExecutiveName,Address,Phone,CONTACTPERSON,EMAILID,GSTIN from [" + sheet1 + "]", excel_con))
                    {
                        ds = new DataSet();
                        oda.Fill(ds);                        
                        Dt = ds.Tables[0];
                       // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>console.log('" + ds.Tables[0].Rows.Count + "');</script>");
                       
                    }
                    excel_con.Close();
                    excel_con.Dispose();
                }

                
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Sucessfully');</script>");
            }
        }
        catch (Exception ex)
        {
           // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
			throw ex;
        }
    }
    private void InsertData()
    {

        string Str_Null = string.Empty;
        string Dist_Code = string.Empty;
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
        string StrDis_Sta_Name = string.Empty;
        string StrDis_Sta_Code = string.Empty;
        string StrEx_Name = string.Empty;
        string StrEx_Code = string.Empty;
        string StrSt_Code = string.Empty;
        string StrPro_Con = string.Empty;
        try
        {
            conn.Open();
            SqlTransaction objTrans = conn.BeginTransaction();
            String sql = ""; String sMsg = "";
            try {

                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    DataRow row = Dt.Rows[i];
                    int columnCount = Dt.Columns.Count;
                    string[] columns = new string[columnCount];
                    for (int j = 0; j < columnCount; j++)
                    {
                        columns[j] = row[j].ToString();
                    }
                    ListedDR lstDR = new ListedDR();
                    DataSet dslstDR = new DataSet();
                    dslstSpec = lstDR.GetDist_Sl_No(div_code);
                    if (dslstSpec.Tables[0].Rows.Count > 0)
                    {
                        Dist_Code = dslstSpec.Tables[0].Rows[0][0].ToString();
                        Dist_Code = (Convert.ToInt32(Dist_Code) + i).ToString();
                    }
                    dslstDeacod = lstDR.GetDist_Detail_Code(DivshName, SuDivcode);
                    if (dslstDeacod.Tables[0].Rows.Count > 0)
                    {
                        User_Def = dslstDeacod.Tables[0].Rows[0][0].ToString();
                        User_Def = DivshName+(Convert.ToInt32(User_Def) + i).ToString();
                    }

                    dslstCat = lstDR.GetDist_Territory(columns[2], div_code);
                    if (dslstCat.Tables[0].Rows.Count > 0)
                    {
                        StrTerr_Code = dslstCat.Tables[0].Rows[0][0].ToString();
                        StrTerr_Name = dslstCat.Tables[0].Rows[0][1].ToString();

                    }
                    else
                    {
                        sMsg += "Unable to Upload Territory Name:" + columns[2].ToString() + ". Excel Row No : " + (i + 2) + "</br>";
                        ///ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Upload Territory Name:" + columns[2].ToString() + ". Row : " + (i + 2) + "');</script>");
                        //return;
                    }

                    dslstBra = lstDR.GetDist_Dis_Code(columns[3], div_code);
                    if (dslstBra.Tables[0].Rows.Count > 0)
                    {
                        StrDis_Code = dslstBra.Tables[0].Rows[0][0].ToString();
                        StrDis_Name = dslstBra.Tables[0].Rows[0][1].ToString();
                    }
                    else
                    {
                        sMsg += "Unable to Upload District Name:" + columns[3].ToString() + ". Excel Row No : " + (i + 2) + "</br>";
                       // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Upload District Name:" + columns[3].ToString() + ". Row : " + (i + 2) + "');</script>");
                        //return;
                    }

                    dslstPBUOM = lstDR.GetDis_Sta(columns[4], div_code);
                    if (dslstPBUOM.Tables[0].Rows.Count > 0)
                    {
                        StrDis_Sta_Code = dslstPBUOM.Tables[0].Rows[0][0].ToString();
                        StrDis_Sta_Name = dslstPBUOM.Tables[0].Rows[0][1].ToString();
                    }
                    else
                    {
                        sMsg += "Unable to Upload State Name:" + columns[4].ToString() + ". Excel Row No : " + (i + 2) + "</br>";
                       // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Upload State Name:" + columns[4].ToString() + ". Row : " + (i + 2) + "');</script>");
                        //return;
                    }

                    dslstBUOM = lstDR.GetDis_Ex_name(columns[5], div_code, StrTerr_Code);
                    if (dslstBUOM.Tables[0].Rows.Count > 0)
                    {
                        StrEx_Code = dslstBUOM.Tables[0].Rows[0][0].ToString();
                        StrEx_Name = dslstBUOM.Tables[0].Rows[0][1].ToString();
                    }
                    else
                    {
                        sMsg += "Unable to Upload Salesforce Name:" + columns[5].ToString() + ". Excel Row No : " + (i + 2) + "</br>";
                       // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Upload Salesforce Name:" + columns[5].ToString() + ". Row : " + (i+2) + "');</script>");
                        //return;
                    }
                    if ((dslstSpec.Tables[0].Rows.Count > 0) && (dslstDeacod.Tables[0].Rows.Count > 0) && (dslstBra.Tables[0].Rows.Count > 0) && (dslstCat.Tables[0].Rows.Count > 0) && (dslstPBUOM.Tables[0].Rows.Count > 0) && (dslstBUOM.Tables[0].Rows.Count > 0))
                    {

                        sql += "INSERT INTO mas_stockist(Division_Code,Stockist_Code, SF_Code, Stockist_Name, Stockist_Address, Stockist_ContactPerson, Stockist_Designation, Stockist_Active_Flag, Stockist_Mobile, Territory, Created_Date,ERP_Code,Dist_Name,Dist_Code,Username,Password,Territory_Code,subdivision_code,Distributor_Code,Norm_Val,Field_Name,Field_Code,State_Code,User_Entry_Code,Stockist_Address1,gstn) " +
                        " values('" + div_code + "', '" + Dist_Code + "', '" + Dist_Code + "','" + columns[1] + "', '" + columns[6] + "', '" + columns[8].ToString() + "', '" + Str_Null + "', '0' ,'" + columns[7] + "','" + StrTerr_Name + "',getdate(),'" + columns[0] + "','" + StrDis_Name + "','" + StrDis_Code + "','" + Str_Null + "','" + Str_Null + "','" + StrTerr_Code + "','" + SuDivcode + "','" + Dist_Code + "','" + Str_Null + "','" + StrEx_Name + "','" + StrEx_Code + "','" + StrDis_Sta_Code + "','" + User_Def + "','" + columns[9].ToString() + "','" + columns[10].ToString() + "') ";


                       // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>console.log(\"" + sql + "\")</script>");
                        
                    }
                }
                if (sMsg=="")
                {
                    SqlCommand CmdUpl = conn.CreateCommand();
                    CmdUpl.Transaction = objTrans;

                    CmdUpl.CommandText = sql;
                    CmdUpl.ExecuteNonQuery();
                    objTrans.Commit();
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully...');</script>");

                }
                else{
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + sMsg + "';</script>");               
                }   
                   
                }
                catch (Exception ex)
                {
                
                        objTrans.Rollback();
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
            }        
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
          
        }
    }
   
  

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.ContentType = "application/vnd.ms-excel";
            //string fileName = Global.ServerPath + "\\Document\\Mas_Distributor_Upload.xlsx";
            string fileName = Server.MapPath("~/Document/Mas_Distributor_Upload.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Mas_Distributor_Upload.xlsx");            
            Response.TransmitFile(fileName);
            Response.Flush();
            Response.End();
        
        }

        catch (Exception)
        {

          

        }

    }

    protected void btnUpload_Click(object sender, ImageClickEventArgs e)
    {
        
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
}