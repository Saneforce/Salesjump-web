using Bus_EReport;
using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Stockist_PriProduct_Upload : System.Web.UI.Page
{

    #region Declaration
    string div_code = string.Empty;
    string sfCode = string.Empty;
    DataSet dsdiv = null;
    string DivshName = string.Empty;
    string SuDivcode = string.Empty;
    DataSet ds;
    DataTable Dt;
    DataSet dslst = new DataSet();
    DataSet dslstsf = new DataSet();
    DataSet dslstst = new DataSet();
    DataSet dslstprod = new DataSet();
    SqlConnection conn = new SqlConnection(Globals.ConnString);
    public static string baseUrl = "";
    #endregion

    #region  Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["div_code"]) != null || Convert.ToString(Session["div_code"]) != ""))
        {
            div_code = Session["div_code"].ToString();

            sfCode = Session["sf_code"].ToString();
            if (!Page.IsPostBack)
            {
                FillSubDivision();

                dd1division_SelectedIndexChanged(sender, e);

            }
        }
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion

    #region dd1division_SelectedIndexChanged
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
    #endregion

    #region FillSubDivision
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
    #endregion

    #region btnUpload_Click
    protected void btnUpload_Click(object sender, ImageClickEventArgs e)
    {
        ImporttoDatatable();
        InsertData();
    }
    #endregion

    #region ImporttoDatatable
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
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT SF_CODE,DistributorCode,PRODUCTCODE,TARGET,MONTH,YEAR from [" + sheet1 + "]", excel_con))


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
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
        }
    }
    #endregion

    #region InsertData
    private void InsertData()
    {
        string sf_Code = string.Empty;
        string sf_Name = string.Empty;
        string stk_Code = string.Empty;
        string stk_Name = string.Empty;
        string prod_Code = string.Empty;
        string prod_Name = string.Empty;
        try
        {
            conn.Open();
            SqlTransaction objTrans = conn.BeginTransaction();
            lstdr dr = new lstdr();
            String sMsg = "";
            String sql = "";
            for (int i = 0; i < Dt.Rows.Count; i++)
            {

                DataRow row = Dt.Rows[i];
                int columnCount = Dt.Columns.Count;
                string[] columns = new string[columnCount];
                for (int j = 0; j < columnCount; j++)
                {
                    columns[j] = row[j].ToString();
                }
                dslstsf = dr.GetSF_code(columns[0], div_code);

                if (dslstsf.Tables[0].Rows.Count > 0)
                {
                    sf_Code = dslstsf.Tables[0].Rows[0][0].ToString();
                    sf_Name = dslstsf.Tables[0].Rows[0][1].ToString();
                }
                else
                {
                    sMsg += "Unable to Upload Filedforce :" + columns[0].ToString() + ". Excel Row No : " + (i + 2) + "</br>";

                }
                dslstst = dr.Getstk_code(columns[1], div_code);

                if (dslstst.Tables[0].Rows.Count > 0)
                {
                    stk_Code = dslstst.Tables[0].Rows[0][1].ToString();
                    stk_Name = dslstst.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    sMsg += "Unable to Upload Distributor :" + columns[1].ToString() + ". Excel Row No : " + (i + 2) + "</br>";

                }
                dslstprod = dr.GetProd_detail(columns[2], div_code);
                if (dslstprod.Tables[0].Rows.Count > 0)
                {
                    prod_Code = dslstprod.Tables[0].Rows[0][0].ToString();
                    prod_Name = dslstprod.Tables[0].Rows[0][1].ToString();
                }
                else
                {
                    sMsg += "Unable to Upload Product :" + columns[2].ToString() + ". Excel Row No : " + (i + 2) + "</br>";

                }
                if (dslstsf.Tables[0].Rows.Count > 0 && dslstst.Tables[0].Rows.Count > 0 && dslstprod.Tables[0].Rows.Count > 0)
                {
                    dslst = dr.insert_primtarstockist(sf_Code, stk_Code, prod_Code, (columns[3] == "") ? "0" : columns[3], columns[4], columns[5], div_code);
                }

            }

            if (sMsg == "")
            {
				ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully...');</script>");
                //SqlCommand CmdUpl = conn.CreateCommand();
                //CmdUpl.Transaction = objTrans;

                //CmdUpl.CommandText = sql;
                //CmdUpl.ExecuteNonQuery();
                //objTrans.Commit();
                

            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + sMsg + "';</script>");
            }

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + ex.Message + "';</script>");

        }
    }
    #endregion

    #region lnkDownload_Click
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);


            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\Stockist_PriTarget_Upload.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Mas_stockistTarget_Upload.xlsx");
            Response.TransmitFile(fileName);
            Response.Flush();
            Response.End();

        }

        catch (Exception)
        {

        }
    }
    #endregion

    #region lstdr
    public class lstdr
    {
        public DataSet GetProd_detail(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            string strQry = "select Product_Detail_Code,Product_Detail_Name from Mas_Product_Detail where Product_Detail_Code='" + Doc_Special_Name + "'and Division_Code = '" + div_code + "'and Product_Active_Flag=0";

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
        public DataSet Getstk_code(string stk_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            string strQry = "select stockist_name,stockist_code from mas_stockist where division_code='" + div_code + "' and stockist_code='" + stk_Name + "' and stockist_active_flag=0";

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
        public DataSet GetSF_code(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            string strQry = "select Sf_Code,Sf_Name from Mas_Salesforce where Sf_Code='" + Doc_Special_Name + "'and  Division_Code = '" + div_code + "," + "'and SF_Status=0";

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
        public DataSet insert_primtarstockist(string sfcode, string stk_code, string prod_code, string PTarget, string month, string year, string Div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            string strQry = "exec sp_stockist_pritarget '" + sfcode + "','" + stk_code + "','" + prod_code + "','" + PTarget + "','" + month + "','" + year + "','" + Div + "'";

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
    #endregion
}
