using Bus_EReport;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class UploadRouteNew : System.Web.UI.Page
{

    #region Declaration 
    DataSet dsProduct = null;
    DataSet dsRetailer = null;
    public static string Div_Code = string.Empty;
    public static string SF_Code = string.Empty;
    DataTable Dt = null;
    DataTable dt = null;
    public static string sf_type = string.Empty;
    public static string baseUrl = "";
    #endregion

    #region OnPreInit
    protected override void OnPreInit(EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["sf_type"]) != null || Convert.ToString(Session["sf_type"]) != ""))
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
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Convert.ToString(Session["sf_type"]) != null || Convert.ToString(Session["sf_type"]) != ""))
        {
            Div_Code = Session["div_code"].ToString();
            SF_Code = Session["sf_code"].ToString();
            if (!Page.IsPostBack)
            {
                DataSet ds = new DataSet();
                Territory tt = new Territory();
                ds = tt.get_Excel_Format("Route", "");
                chkFruits.DataSource = ds.Tables[0];
                chkFruits.DataTextField = "Alise_Name";
                chkFruits.DataValueField = "Mantatory";
                chkFruits.DataBind();

                //dt = new DataTable();
                //dt = ds.Tables[0];

                foreach (ListItem item in chkFruits.Items)
                {
                    if (item.Value == "1")
                    {
                        item.Selected = true;
                        item.Enabled = false;
                    }
                }
            }
        }
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion

    #region lnkDownload_Click
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        dt = new DataTable();
        foreach (ListItem item in chkFruits.Items)
        {
            if (item.Selected)
            {
                dt.Columns.Add(item.Text.ToString(), typeof(string));
            }
        }
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, "Route List");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Route_Upload_Format.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    #endregion

    #region btnUpload_Click
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ImporttoDatatable();

        DataSet ds = new DataSet();
        Territory tt = new Territory();
        ds = tt.get_Excel_Format("Route", "");
        //chkFruits.DataSource = ds.Tables[0];
        //chkFruits.DataTextField = "Alise_Name";
        //chkFruits.DataValueField = "Mantatory";
        //chkFruits.DataBind();

        dt = new DataTable();
        dt = ds.Tables[0];


        foreach (DataRow row in ds.Tables[0].Rows)
        {
            dt.Columns.Add(row["Alise_Name"].ToString(), typeof(string));
        }

        StringBuilder errorMessgae = new StringBuilder();
        StringBuilder colName = new StringBuilder();



        for (int j = 0; j < Dt.Columns.Count; j++)
        {
            bool match = false;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == Dt.Columns[j].ColumnName)
                {
                    match = true;

                }
            }
            if (!match)
            {
                errorMessgae.Append("<br/> Not Match Column Name  : " + Dt.Columns[j].ColumnName);
            }
        }

        DataSet dsNew = new DataSet();
        StringBuilder stb = new StringBuilder();
        StringBuilder ColnmName = new StringBuilder();
        for (int i = 0; i < Dt.Columns.Count; i++)
        {
            string alsName = Dt.Columns[i].ColumnName.ToString();
            DataRow[] row = dt.Select("Alise_Name='" + alsName + "'");
            if (row.Length > 0)
            {
                ColnmName.Append(row[0].Field<string>("Field_Name"));
                if (i != Dt.Columns.Count)
                {
                    ColnmName.Append(",");
                }
                if (row[0].Field<Byte>("Name_To_Code") == 1)
                {

                    string targetField = row[0].Field<string>("Target_Field");
                    string codeField = row[0].Field<string>("Code_Field");
                    string Where_Field = row[0].Field<string>("Where_Field");
                    string Master_Table_Name = row[0].Field<string>("Master_Table_Name");


                    string qry = "";

                    if (Master_Table_Name == "mas_salesforce")
                    {
                        qry = "select " + codeField + ",Territory_Code," + Where_Field + " from " + Master_Table_Name + " where  charindex(','+'" + Div_Code + "'+',',','+ division_code +',')>0  and  SF_Status=0";
                    }
                    else if (Master_Table_Name == "mas_stockist")
                    {
                        qry = "select " + codeField + ",Territory_Code," + Where_Field + " from " + Master_Table_Name + " where  charindex(','+'" + Div_Code + "'+',',','+ division_code +',')>0  and Stockist_Active_Flag=0";
                    }
                    else if (Master_Table_Name == "mas_Territory")
                    {
                        qry = "select " + codeField + ",Territory_Code," + Where_Field + " from " + Master_Table_Name + " where  charindex(','+'" + Div_Code + "'+',',','+ cast( div_code as varchar ) +',')>0  and Territory_Active_Flag=0";
                    }
                    //else
                    //{
                    //    qry = "select " + codeField + "," + Where_Field + " from " + Master_Table_Name + " where " + strCondtion;
                    //}


                    DataTable dtdyn = new DataTable(alsName);
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ToString());
                    con.Open();
                    SqlDataAdapter sqlAdp = new SqlDataAdapter(qry, con);
                    con.Close();
                    sqlAdp.Fill(dtdyn);
                    dsNew.Tables.Add(dtdyn);

                }
            }

        }

        ListedDR lstDR = new ListedDR();

        DataSet dslstSpec = lstDR.GetTerr_Sl_No(Div_Code);
        string Terr_Code = "0";
        if (dslstSpec.Tables[0].Rows.Count > 0)
        {
            Terr_Code = dslstSpec.Tables[0].Rows[0][0].ToString();
        }


        for (int l = 0; l < Dt.Rows.Count; l++)
        {
            StringBuilder inserQery = new StringBuilder();

            inserQery.Append("'" + (Convert.ToInt32(Terr_Code) + l).ToString() + "',");
            inserQery.Append("'" + Div_Code + "','0',getdate(),");

            for (int i = 0; i < Dt.Columns.Count; i++)
            {
                string alsName = Dt.Columns[i].ColumnName.ToString();
                DataRow[] row = dt.Select("Alise_Name='" + alsName + "'");

                if (row.Length > 0)
                {

                    if (row[0].Field<Byte>("Name_To_Code") == 1)
                    {

                        string targetField = row[0].Field<string>("Target_Field");
                        string codeField = row[0].Field<string>("Code_Field");
                        string Where_Field = row[0].Field<string>("Where_Field");

                        string strCondtion = "";

                        string _Field = Dt.Rows[l][i].ToString().TrimEnd(',');
                        string[] arr_F = _Field.Split(',');

                        for (int a = 0; a < arr_F.Length; a++)
                        {
                            strCondtion += "'" + arr_F[a].ToString().TrimEnd(',') + "',";
                        }
                        strCondtion = strCondtion.TrimEnd(',');

                        DataRow[] rr = dsNew.Tables[alsName].Select(Where_Field + " in ( " + strCondtion + " )");

                        if (arr_F.Length == rr.Length)
                        {

                            if (rr.Length > 0)
                            {
                                string sf = "";
                                int counts = 0;

                                string tmp = "";
                                if (alsName == "SF_Name" || alsName == "Distributor_Name")
                                {
                                    counts = rr.Length;
                                    tmp = rr[0][1].ToString();
                                }

                                for (int kk = 0; kk < rr.Length; kk++)
                                {
                                    if (alsName == "SF_Name" || alsName == "Distributor_Name")
                                    {
                                        if (tmp == rr[kk][1].ToString())
                                        {
                                            counts--;
                                        }

                                        if (kk == rr.Length - 1)
                                        {
                                            sf += rr[kk][0].ToString().Replace("'", "");
                                        }
                                        else
                                        {
                                            sf += rr[kk][0].ToString().Replace("'", "") + ",";
                                        }

                                        tmp = rr[kk][1].ToString();
                                    }
                                    else
                                    {
                                        if (kk == rr.Length - 1)
                                        {
                                            sf += rr[kk][0].ToString().Replace("'", "");
                                        }
                                        else
                                        {
                                            sf += rr[kk][0].ToString().Replace("'", "") + ",";
                                        }

                                    }
                                }

                                if (counts == 0)
                                {
                                    inserQery.Append("'" + sf + "',");
                                }
                                else
                                {
                                    errorMessgae.Append("<br/>  Row Number : " + (l + 1) + " : " + alsName + " : " + Dt.Rows[l][i].ToString() + " Territory Not Same..!");
                                }


                            }
                            else
                            {
                                errorMessgae.Append(" <br/> Row_Number : " + (l + 1) + " : " + alsName + " : " + Dt.Rows[l][i].ToString());
                            }
                        }
                        else
                        {
                            for (int z = 0; z < arr_F.Length; z++)
                            {
                                int countsd = 0;
                                for (int x = 0; x < rr.Length; x++)
                                {

                                    if (arr_F[z].ToString().ToLower() == rr[x][2].ToString().ToLower())
                                    {
                                        countsd++;
                                    }
                                }
                                if (countsd == 0)
                                {
                                    errorMessgae.Append("  <br/> Row_Number : " + (l + 1) + " , " + alsName + " : " + Dt.Rows[l][i].ToString() + ", incorrect " + alsName + " : " + arr_F[z].ToString() + "..!");
                                }
                            }
                        }
                    }
                    else
                    {
                        inserQery.Append("'" + Dt.Rows[l][i].ToString().Replace("'", "") + "',");
                    }
                }
            }

            stb.Append(" insert into Mas_Territory_Creation ( Territory_Code,Division_Code,Territory_Active_Flag,Created_date, " + ColnmName.ToString().TrimEnd(',') + ") values ( " + inserQery.ToString().TrimEnd(',') + " )");

        }

        //  Response.Write(stb.ToString());
        // Response.Write("<br/> Eoorr : " + errorMessgae.ToString());
        // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + errorMessgae.ToString() + "';</script>");


        if (errorMessgae.Length == 0)
        {
            int ch = -1;
            SqlConnection conn = new SqlConnection(Globals.ConnString);
            conn.Open();
            SqlTransaction objTrans = conn.BeginTransaction();
            string mg = "";
            try
            {
                SqlCommand CmdUpl = conn.CreateCommand();
                CmdUpl.Transaction = objTrans;
                CmdUpl.CommandText = stb.ToString();
                CmdUpl.ExecuteNonQuery();
                objTrans.Commit();
                ch = 1;

            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                ch = 2;
                mg = ex.ToString();
            }
            finally
            {
                conn.Close();
            }


            if (ch == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='<br/>Uploaded Successfully...';alert('Uploaded Successfully...');</script>");
            }
            else if (ch == 2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='<br/>" + mg + "';alert('" + mg + "');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='<br/>Uploaded Unsuccessfull...';alert('Uploaded Unsuccessfull...');</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='<br/> Errors </br>  " + errorMessgae.ToString() + "';</script>");
        }
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
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString().Trim();
                    DataTable dtExcelData = new DataTable();
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * from [" + sheet1 + "]", excel_con))
                    {
                        DataSet ds = new DataSet();
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
}