using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using DBase_EReport;

public partial class MasterFiles_Options_Product_Tax_Upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataTable dt = null;
    DataSet ds = null;
    DataTable Dt;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    string[] stc;
    string[] stn;
    string stname = string.Empty;
    string stvalue = string.Empty;
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
    }

    public class GState
    {
        public string stCode { get; set; }
        public string stName { get; set; }
    }

    [WebMethod]
    public static List<GState> getState()
    {

        List<GState> Lists = new List<GState>();

        DataSet ds = new DataSet();
        Division dv = new Division();
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        ds = dv.getStatePerDivision(div_code);
        if (ds.Tables.Count > 0)
        {
            string sts = ds.Tables[0].Rows[0][0].ToString();
            State st = new State();
            ds = st.getState_new(sts.TrimEnd(','));


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                GState list = new GState();
                list.stCode = row["state_code"].ToString();
                list.stName = row["statename"].ToString();
                Lists.Add(list);
            }
        }
        return Lists.ToList();
    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        // string stname = ddlst.SelectedItem.ToString();
        //string stcode = hstc.Value;
        //stc = stcode.Split(',');
        //string stname = hstn.Value;
        //stn = stname.Split(',');
        Product pd = new Product();
        dt = getProdUpload(div_code);
        dt.Columns.AddRange(new DataColumn[1] { new DataColumn("TaxValue", typeof(string)) });
        dt.Columns.AddRange(new DataColumn[1] { new DataColumn("TaxName", typeof(string)) });

        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    dt.Rows[i]["State"] = hstn.Value;
        //}
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, "Product List");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Product_Tax_Upload.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }

    public static DataTable getProdUpload(string divcode)
    {
        DB_EReporting dber = new DB_EReporting();

        DataTable dsAdmin = null;
        string strQry = "declare @slno int " +
                 " select @slno=0 " +
                 " select @slno+ROW_NUMBER() over( order by Product_Detail_Name) SlNo,Product_Detail_Code ProductCode,Sale_Erp_Code Erp_Code,Product_Detail_Name ProductName from Mas_Product_Detail where Division_Code='" + divcode + "' and Product_Active_Flag=0 ";

        try
        {
            dsAdmin = dber.Exec_DataTable(strQry);
        }

        catch (Exception ex)
        {
            throw ex;
        }

        return dsAdmin;
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
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT SlNo,ProductCode,Erp_Code,ProductName,TaxValue,TaxName from [" + sheet1 + "]", excel_con))
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

    private void InsertData()
    {
        try
        {
            conn.Open();
            SqlTransaction objTrans = conn.BeginTransaction();
            try
            {
                string Tax_detils = "<ROOT>";
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    Tax_detils += "<Details PCode=\"" + Dt.Rows[i]["ProductCode"] + "\" Tax_Value=\"" + Dt.Rows[i]["TaxValue"] + "\" T_name=\"" + Dt.Rows[i]["TaxName"] + "\" />";
                }
                Tax_detils += "</ROOT>";

                string stateCode = hstc.Value;
                string[] split_Array = stateCode.Split(',');

                for (int t=0; t < split_Array.Length; t++) {

                    if (split_Array[t] != "")
                    {
                        tax tx = new tax();
                        DataSet dtx = tx.inserttax(div_code.Trim(), split_Array[t], Tax_detils);
                        //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
                        //{
                        //    using (SqlCommand cmd = con.CreateCommand())
                        //    {
                        //        cmd.CommandType = CommandType.StoredProcedure;
                        //        cmd.CommandText = "Sp_InsertTaxDetails";
                        //        cmd.Parameters.Add(new SqlParameter("@Div", div_code.Trim()));
                        //        cmd.Parameters.Add(new SqlParameter("@stcode", split_Array[t]));
                        //        cmd.Parameters.Add(new SqlParameter("@Tax_Details", Tax_detils));
                        //        try
                        //        {
                        //            if (con.State != ConnectionState.Open)
                        //            {
                        //                con.Open();
                        //            }
                        //            cmd.ExecuteNonQuery();
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            throw ex;
                        //        }
                        //    }
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Tax Uploaded Sucessfully...');</script>");
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
        }
    }

    protected void upbt_Click(object sender, EventArgs e)
    {
        ImporttoDatatable();
        InsertData();
    }

public class tax
    {
        public DataSet inserttax(string div, string stcode, string taxdet)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            string strQry = "exec Sp_InsertTaxDetails '"+ div + "','"+ stcode + "','"+ taxdet + "'";
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
    }
}