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

public partial class MasterFiles_Options_Upload_Brand : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataTable dt = new DataTable();
    DataSet ds = null;
    DataTable Dt;
    SqlConnection conn = new SqlConnection(Globals.ConnString);
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
        fillsubdivision();
    }


    private void fillsubdivision()
    {
        DataSet dsSalesForce = new DataSet();
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlst.DataTextField = "subdivision_name";
            ddlst.DataValueField = "subdivision_code";
            ddlst.DataSource = dsSalesForce;
            ddlst.DataBind();
            ddlst.Items.Insert(0, new ListItem("--Select--", "0"));

        }
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
        //ds = dv.getStatePerDivision(div_code);
        //if (ds.Tables.Count > 0)
        //{
        //    string sts = ds.Tables[0].Rows[0][0].ToString();
        //    State st = new State();
        //    ds = st.getState_new(sts.TrimEnd(','));


        //    foreach (DataRow row in ds.Tables[0].Rows)
        //    {
        //        GState list = new GState();
        //        list.stCode = row["state_code"].ToString();
        //        list.stName = row["statename"].ToString();
        //        Lists.Add(list);
        //    }
        //}
        return Lists.ToList();
    }

    private void fillState()
    {
        //DataSet ds = new DataSet();
        //Division dv = new Division();
        //ds = dv.getStatePerDivision(div_code);
        //if (ds.Tables.Count > 0)
        //{
        //    string sts = ds.Tables[0].Rows[0][0].ToString();
        //    State st = new State();
        //    ds = st.getState_new(sts.TrimEnd(','));

        //    if (ds.Tables.Count > 0)
        //    {
        //        ddlst.DataTextField = "statename";
        //        ddlst.DataValueField = "state_code";
        //        ddlst.DataSource = ds;
        //        ddlst.DataBind();
        //    }
        //}

    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        //string stcode = hstc.Value;
        //stc = stcode.Split(',');
        //string stname = hstn.Value;
        //stn = stname.Split(',');
        Product pd = new Product();
        dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Sl_NO", typeof(string)),
                new DataColumn("Product_Group_Name", typeof(string)),
                new DataColumn("Product_Group_SName", typeof(string))});
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, "Product Group List");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Product_Group_Upload_Format.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
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
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Sl_NO,Product_Group_Name,Product_Group_SName from [" + sheet1 + "]", excel_con))
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

                for (int j = 0; j < Dt.Rows.Count; j++)
                {
                    DataRow row = Dt.Rows[j];
                    int columnCount = Dt.Columns.Count;
                    string[] columns = new string[columnCount];
                    for (int k = 0; k < columnCount; k++)
                    {
                        columns[k] = row[k].ToString();
                    }

                    if ((columns[2].ToString() != "" && columns[2].ToString() != null) && (columns[1].ToString() != "" && columns[1].ToString() != null))
                    {

                        string consString = Globals.ConnString;
                        using (SqlConnection con = new SqlConnection(consString))
                        {
                            using (SqlCommand cmd = con.CreateCommand())
                            {

                                cmd.CommandType = CommandType.StoredProcedure; ;
                                cmd.CommandText = "insertMasProductGroup";

                                SqlParameter[] parameters = new SqlParameter[]
                            {
                                        new SqlParameter("@grpname", columns[1].ToString()),
                                        new SqlParameter("@grpsname", columns[2].ToString()),
                                        new SqlParameter("@Div", div_code)

                            };
                                cmd.Parameters.AddRange(parameters);

                                try
                                {
                                    if (con.State != ConnectionState.Open)
                                    {
                                        con.Open();
                                    }
                                    cmd.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }


                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
}