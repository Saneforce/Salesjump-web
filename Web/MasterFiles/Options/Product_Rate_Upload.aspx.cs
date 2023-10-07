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

public partial class MasterFiles_Options_Product_Rate_Upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataTable dt = null;
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
        fillState();
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

    private void fillState()
    {
        DataSet ds = new DataSet();
        Division dv = new Division();
        ds = dv.getStatePerDivision(div_code);
        if (ds.Tables.Count > 0)
        {
            string sts = ds.Tables[0].Rows[0][0].ToString();
            State st = new State();
            ds = st.getState_new(sts.TrimEnd(','));

            if (ds.Tables.Count > 0)
            {
                ddlst.DataTextField = "statename";
                ddlst.DataValueField = "state_code";
                ddlst.DataSource = ds;
                ddlst.DataBind();
            }
        }

    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        string stname = ddlst.SelectedItem.ToString();
        //string stcode = hstc.Value;
        //stc = stcode.Split(',');
        //string stname = hstn.Value;
        //stn = stname.Split(',');
        Product pd = new Product();
        dt = pd.getProdUpload(div_code);
        dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Ret_Piece_Rate", typeof(string)),
                new DataColumn("Ret_Case_Rate", typeof(string)),
                new DataColumn("DB_Piece_Rate", typeof(string)),
                new DataColumn("DB_Case_Rate", typeof(string)),
				new DataColumn("SS_Piece_Rate",typeof(string)),
				new DataColumn("SS_Case_Rate",typeof(string)),
				new DataColumn("MRP_Rate", typeof(string)),
                new DataColumn("State", typeof(string)),
                new DataColumn("Effective_From_Date",typeof(DateTime))});
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["State"] = hstn.Value;
        }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Product List");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Product_Rate_Upload_Format.xlsx");
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
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT SlNo,ProductCode,ProductName,Ret_Piece_Rate,Ret_Case_Rate,DB_Piece_Rate,DB_Case_Rate,State,Effective_From_Date,SS_Piece_Rate,SS_Case_Rate,MRP_Rate from [" + sheet1 + "]", excel_con))
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

                       if ((columns[5].ToString() != "" && columns[5].ToString() != null) || (columns[6].ToString() != "" && columns[6].ToString() != null) || (columns[3].ToString() != "" && columns[3].ToString() != null) || (columns[4].ToString() != "" && columns[4].ToString() != null) ||(columns[9].ToString() != "" && columns[9].ToString() != null))
                        {

                            DateTime dt1 = Convert.ToDateTime(columns[8].ToString());
                            string dt2 = dt1.ToString("yyyy/MM/dd");
                            string consString = Globals.ConnString;
                            using (SqlConnection con = new SqlConnection(consString))
                            {
                                using (SqlCommand cmd = con.CreateCommand())
                                {

                                    cmd.CommandType = CommandType.StoredProcedure; ;
                                    cmd.CommandText = "insertMasProductRate";

                                    SqlParameter[] parameters = new SqlParameter[]
                            {
                                        new SqlParameter("@pcode", columns[1].ToString().Trim()),
                                        new SqlParameter("@stcode", hstc.Value.Trim()),
                                        new SqlParameter("@dcr", ((columns[6].ToString()=="")? "0":columns[6].ToString().Trim())),
                                        new SqlParameter("@dpr", ((columns[5].ToString()=="")? "0":columns[5].ToString().Trim())),
                                        new SqlParameter("@rcr", ((columns[4].ToString()=="")? "0":columns[4].ToString().Trim())),
                                        new SqlParameter("@rpr", ((columns[3].ToString()=="")? "0":columns[3].ToString().Trim())),
                                        new SqlParameter("@edt",dt2),
                                        new SqlParameter("@Div", div_code.Trim()),
										new SqlParameter("@sspr", ((columns[9].ToString()=="")? "0":columns[9].ToString().Trim())),
										new SqlParameter("@sscr", ((columns[10].ToString()=="")? "0":columns[10].ToString().Trim())),
										new SqlParameter("@mrp", ((columns[11].ToString()=="")? "0":columns[11].ToString().Trim()))

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
                                    catch(Exception ex)
                                    {
                                        throw ex;
                                    }


                                }
                            
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Rate Uploaded');</script>");
        }
        catch(Exception ex)
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