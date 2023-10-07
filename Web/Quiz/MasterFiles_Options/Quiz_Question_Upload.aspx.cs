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
using System.Net;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Text;



public partial class MasterFiles_Options_Quiz_Question_Upload : System.Web.UI.Page
{
    string surveyID = string.Empty;
    string div_code = string.Empty;

    SqlConnection con = new SqlConnection(Globals.ConnString);
    SqlCommand cmd;
    DataSet ds;
    DataTable Dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!IsPostBack)
        {
            if (Request.QueryString["Survey_Id"] != "" || Request.QueryString["Survey_Id"] != null)
            {
                surveyID = Request.QueryString["Survey_Id"].ToString();

                Session["surveyID"] = surveyID;
            }
            //menu1.Title = this.Page.Title;
        }
    }


    private void ImporttoDatatable()
    {
        try
        {
            if (FlUploadcsv.HasFile)
            {

                string excelPath = Server.MapPath("~/Upload_Document/") + Path.GetFileName(FlUploadcsv.PostedFile.FileName);

                // if (!System.IO.File.Exists(excelPath))
                //  {
                FlUploadcsv.SaveAs(excelPath);
                //  }
                //  else
                //  {
                // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Selected File is Already Exists')</script>");
                // }

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

                //string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties=Excel 12.0;";

                string connStr = "";

                    if (Path.GetExtension(excelPath) == ".xls")
                    {
                         connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (Path.GetExtension(excelPath) == ".xlsx")
                    {
                         connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';";
                    }


                using (OleDbConnection excel_con = new OleDbConnection(connStr))
                {
                    excel_con.Open();
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    DataTable dtExcelData = new DataTable();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * from [" + sheet1 + "]", excel_con))
                    {
                        ds = new DataSet();
                        oda.Fill(ds);
                        Dt = ds.Tables[0];

                        for (int j = 0; j < Dt.Columns.Count; j++)
                        {
                            string ColName = Dt.Columns[j].ColumnName;
                            if (j == Dt.Columns.Count - 1)
                            {
                                if (ColName.Contains("CorrectAns"))
                                {

                                }
                                else
                                {
                                    Dt.Columns.RemoveAt(j);
                                }
                            }

                        }
                        for (int i = Dt.Rows.Count - 1; i >= 0; i--)
                        {
                            if (Dt.Rows[i][1] == DBNull.Value)
                                Dt.Rows[i].Delete();
                        }
                        Dt.AcceptChanges();

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
    private bool ContainsSpecialChars(string value)
    {
        var list = new[] { "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "+", "=", "\"" };

        if (list.Any(value.Contains) == true)
        {

        }
        else
        {

        }

        return list.Any(value.Contains);
    }


    public string RemoveSpecialChars(string str)
    {

        string[] chars = new string[] { ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]" };

        for (int i = 0; i < chars.Length; i++)
        {
            if (str.Contains(chars[i]))
            {
                str = str.Replace(chars[i], "");
            }
        }

        return str;
    }



    private void InsertData()
    {

        string AnsOpt = string.Empty;
        string CorrectAns = string.Empty;
        int C_Ans;
        string Q_Type = string.Empty;

        StringBuilder Sb_Qus = new StringBuilder();
        Sb_Qus.Append("<root>");

        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            DataRow row = Dt.Rows[i];
            int columnCount = Dt.Columns.Count;
            string[] columns = new string[columnCount];

            AnsOpt = string.Empty;
            CorrectAns = string.Empty;

            CorrectAns = row[columnCount - 1].ToString();

            for (int j = 0; j < columnCount; j++)
            {
                columns[j] = row[j].ToString();

                string ColName = Dt.Columns[j].ColumnName;

                if (ColName.Contains("Answer"))
                {
                    if (columns[j] != "")
                    {
                        AnsOpt += columns[j].Trim() + "***";

                        int ColCnt = Dt.Columns.Count - 1;

                        if (row[ColCnt].ToString() == columns[j])
                        {
                            C_Ans = 1;
                        }
                        else
                        {
                            C_Ans = 0;
                        }

                        //CorrectAns += C_Ans + "***";
                    }

                }

                //ContainsSpecialChars(columns[j]);
            }

            string InputOption = columns[2] + "#" + columns[3] + "#" + columns[4] + "#" + columns[5];

            string SurveyID = Session["surveyID"].ToString();

            Q_Type = columns[1];

            string Question_Text = columns[2].Replace("<", "&lt;")
                                                   .Replace("&", "&amp;")
                                                   .Replace(">", "&gt;")
                                                   .Replace("\"", "&quot;")
                                                   .Replace("'", "&apos;");
            string Input_Option = AnsOpt.Replace("<", "&lt;")
                                                   .Replace("&", "&amp;")
                                                   .Replace(">", "&gt;")
                                                   .Replace("\"", "&quot;")
                                                   .Replace("'", "&apos;");
            string Correct_Ans = CorrectAns.Replace("<", "&lt;")
                                                   .Replace("&", "&amp;")
                                                   .Replace(">", "&gt;")
                                                   .Replace("\"", "&quot;")
                                                   .Replace("'", "&apos;");

            Sb_Qus.Append("<QuestionData  Survey_ID='" + SurveyID.Trim() + "'  Question_Text='" + Question_Text.Trim() + "' inputOptions='" + Input_Option.Trim() + "' Correct_Ans='" + Correct_Ans.Trim() + "'  />");

           
        }

        Sb_Qus.Append("</root>");

        // string parsedString = WebUtility.HtmlEncode(Sb_Qus).ToString();

        con.Open();
        SqlCommand cmd = new SqlCommand("AddSurveyQuestions_Answer_Excel_Upload", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Div_Code", SqlDbType.VarChar);
        cmd.Parameters[0].Value = div_code;
        cmd.Parameters.Add("@Question_Type_Name", SqlDbType.VarChar);
        cmd.Parameters[1].Value = Q_Type;
        cmd.Parameters.Add("@XMLQuestion_Det", SqlDbType.VarChar);
        cmd.Parameters[2].Value = Sb_Qus.ToString();
        int iReturn = Convert.ToInt32(cmd.ExecuteNonQuery());
        con.Close();
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ImporttoDatatable();
        InsertData();
    }
    protected void btnDownLoad_Click(object sender, EventArgs e)
    {
        try
        {

            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\QuizQuestion_Excel.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=QuizQuestion_Excel.xlsx");
            Response.TransmitFile(fileName);
            Response.End();

        }
        catch (Exception ex)
        {
            // lblMessage.Text = ex.Message;
        }
    }
    

    private void Download_File(string FilePath)
    {
        try
        {
            
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(FilePath));
            HttpContext.Current.Response.WriteFile(FilePath);
          
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.           
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
          

        }
        catch (Exception ex)
        {

        }
    }


    protected void Img_Click(object sender, EventArgs e)
    {

        string strFileType, strFilePath;
        strFileType = "excelfile";
        switch (strFileType)
        {
            case "wordfile":
                strFilePath = "~\\All_Files\\Demo_WORD.doc";
                Download_File(strFilePath);
                break;
            case "excelfile":
                strFilePath = "~\\Document\\QuizQuestion_Excel.xlsx";
                Download_File(strFilePath);
                break;
            case "pdffile":
                strFilePath = "~\\All_Files\\Demo_PDF.pdf";
                Download_File(strFilePath);
                break;
            case "xmlfile":
                strFilePath = "~\\All_Files\\Demo_XML.xml";
                Download_File(strFilePath);
                break;
            case "htmlfile":
                strFilePath = "~\\All_Files\\Demo_HTML.html";
                Download_File(strFilePath);
                break;
            case "csvfile":
                strFilePath = "~\\All_Files\\Demo_CSV.csv";
                Download_File(strFilePath);
                break;
            case "textfile":
                strFilePath = "~\\All_Files\\Demo_TEXT.txt";
                Download_File(strFilePath);
                break;
        }
    }
}