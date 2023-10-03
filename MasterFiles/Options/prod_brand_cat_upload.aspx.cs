using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Options_prod_brand_cat_upload : System.Web.UI.Page
{
    DataSet dsB = null;
    DataTable DtB;
    DataSet dsC = null;
    DataTable DtC;
    int output = 1;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static List<GState> getDivision()
    {

        List<GState> Lists = new List<GState>();

        DataSet ds = new DataSet();
        Temp t = new Temp();
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        ds = t.getSubDivision(div_code);
        foreach (DataRow row in ds.Tables[0].Rows)
            {
                GState list = new GState();
                list.stCode = row["subdivision_code"].ToString();
                list.stName = row["subdivision_name"].ToString();
                Lists.Add(list);
            }
        return Lists.ToList();
    }
    public class GState
    {
        public string stCode { get; set; }
        public string stName { get; set; }
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
            string fileName = Server.MapPath("~\\Document\\brand_cat_upload.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=brand_cat_upload.xlsx");
            Response.TransmitFile(fileName);
            Response.Flush();
            Response.End();

        }

        catch (Exception)
        {

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
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Product_Brand_Code,Product_Brand_Name,Product_Cat_division from[" + sheet1 + "]", excel_con))
                    {
                        dsB = new DataSet();
                        oda.Fill(dsB);
                        DtB = dsB.Tables[0];
                    }
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Product_Cat_Code,Product_cat_Name,Product_Cat_division from[" + sheet1 + "]", excel_con))
                    {
                        dsC = new DataSet();
                        oda.Fill(dsC);
                        DtC = dsC.Tables[0];
                    }
                    excel_con.Close();
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
    private void InsertData()
    {
        Temp l = new Temp();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string branddet = "<ROOT>";
        for (int i = 0; i < DtB.Rows.Count; i++)
        {
            branddet += "<BDetails B_Code=\"" + DtB.Rows[i]["Product_Brand_Code"] + "\" B_name=\"" + DtB.Rows[i]["Product_Brand_Name"] + "\" />";
        }
        branddet += "</ROOT>";
        //string catdet = "<ROOT>";
        //for (int i = 0; i < DtC.Rows.Count; i++)
        //{
        //    catdet += "<CDetails C_Code=\"" + DtC.Rows[i]["Product_Cat_Code"] + "\" C_name=\"" + DtC.Rows[i]["Product_cat_Name"] + "\"/>";
        //}
        //catdet += "</ROOT>";
        string subdiv = hdivc.Value;

        DataSet dup = l.insertDetails(div_code.Trim(), branddet, "", subdiv);

    }

        protected void upbt_Click(object sender, EventArgs e)
    {
        ImporttoDatatable();
        if (output > 0)
        {
            InsertData();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Upload!!!');</script>");
        }
    }

    public class Temp
    {
        public DataSet insertDetails(string div, string stcode, string catdet,string subdiv)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            string strQry = "exec  sp_CatBrnd_insert '" + div + "','" + stcode + "','" + catdet + "','"+ subdiv + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet getSubDivision(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "select subdivision_code,subdivision_name from mas_subdivision where Div_Code='"+ div_code + "' and SubDivision_Active_Flag=0";
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
}