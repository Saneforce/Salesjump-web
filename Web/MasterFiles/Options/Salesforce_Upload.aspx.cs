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

public partial class MasterFiles_Options_Salesforce_Upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsState = null; 
    DataSet dsDesignation = null;
    DataSet dsSf = null;
    string sfCode = string.Empty;
    string Username = string.Empty;
    string Password = string.Empty;
    string Designation = string.Empty;
    string sf_type = string.Empty;
    SqlConnection conn = new SqlConnection(Globals.ConnString);
    DataSet ds;
    DataTable Dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
      //  sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            //FillReporting();           
        }
    }
    private void InsertData()
    {
        try
        {
            if (FlUploadcsv.HasFile)
            {
                // string sf_code = string.Empty;
                string sf_Username = string.Empty;
                string strDesignation = string.Empty;
                string strState = string.Empty;
                string strReportingto = string.Empty;
                string strsfcode = string.Empty;
                string Strtype = string.Empty;
                string strDesigName = string.Empty;
                string subdivCode = string.Empty;
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    DataRow row = Dt.Rows[i];
                    int columnCount = Dt.Columns.Count;
                    string[] columns = new string[columnCount];
                    for (int j = 0; j < columnCount; j++)
                    {
                        columns[j] = row[j].ToString().Trim();

                    }

                    SalesForce sf = new SalesForce();
                    dsSalesForce = sf.GetSFCode_Upload(columns[4]);
                    if (dsSalesForce.Tables[0].Rows.Count > 0)
                    {
                        strsfcode = dsSalesForce.Tables[0].Rows[0][0].ToString();
                    }

                    State st = new State();
                    dsState = st.getState_Code(columns[5]);
                    if (dsState.Tables[0].Rows.Count > 0)
                    {
                        strState = dsState.Tables[0].Rows[0][0].ToString();
                    }

                    Designation des = new Designation();
                    dsDesignation = des.getDesig_SF(columns[6], div_code);
                    if (dsDesignation.Tables[0].Rows.Count > 0)
                    {
                        strDesignation = dsDesignation.Tables[0].Rows[0][0].ToString();
                        strDesigName = dsDesignation.Tables[0].Rows[0][1].ToString();

                    }

                    dsSf = sf.getReporting_Name(columns[7].Trim(), div_code);
                    if (dsSf.Tables[0].Rows.Count > 0)
                    {
                        strReportingto = dsSf.Tables[0].Rows[0][0].ToString();
                    }

                    SubDivision sub = new SubDivision();
                    DataSet dsSubdiv = new DataSet();
                    dsSubdiv = sub.GetSubdiv_Code(columns[11], div_code);
                    if (dsSubdiv.Tables[0].Rows.Count > 0)
                    {
                        subdivCode = dsSubdiv.Tables[0].Rows[0][0].ToString();
                    }
                    conn.Open();
                    string iCnt = strsfcode;
                    if (iCnt.ToString().Length == 1)
                    {
                        strsfcode = "000" + iCnt.ToString();
                    }
                    else if (iCnt.ToString().Length == 2)
                    {
                        strsfcode = "00" + iCnt.ToString();
                    }
                    else if (iCnt.ToString().Length == 3)
                    {
                        strsfcode = "0" + iCnt.ToString();
                    }
                    else
                    {
                        strsfcode = iCnt.ToString();
                    }
                    if (columns[4] == "1")
                    {
                        Strtype = "MR";
                    }
                    else
                    {
                        Strtype = "MGR";
                    }
                    strsfcode = Strtype + strsfcode;
                    div_code = Session["div_code"].ToString() + ",";
                    //Add SlNO
                    ListedDR objListedDR = new ListedDR();
                    int sf_sl_no = objListedDR.GetListedDrSlNO();
                    subdivCode = subdivCode + ",";
                    string sEmp_ID = "E" + sf_sl_no.ToString();
                    if ((dsSalesForce.Tables[0].Rows.Count > 0) && (dsState.Tables[0].Rows.Count > 0) && (dsDesignation.Tables[0].Rows.Count > 0) && (dsSf.Tables[0].Rows.Count > 0))
                    {
                        string sql = "insert into Mas_Salesforce (Sf_Code,Sf_UserName,Sf_Password,Sf_Name,Sf_HQ,sf_type,State_Code,Designation_Code,Reporting_To_SF,TP_Reporting_SF,Sf_Joining_Date,sf_Tp_Active_Dt,sf_emp_id,SF_Status,Division_Code,Created_Date,sf_Tp_Active_flag,sf_Designation_Short_Name,UsrDfd_UserName, Sf_TP_DCR_Active_Dt, Last_DCR_Date,sf_Sl_No, Employee_Id,Last_TP_Date, subdivision_code) ";
                        sql += "VALUES('" + strsfcode + "', '" + columns[0] + "','" + columns[1] + "', '" + columns[2] + "', '" + columns[3] + "','" + columns[4] + "','" + strState + "','" + strDesignation + "','" + strReportingto + "','" + strReportingto + "','" + columns[8] + "','" + columns[9] + "','" + columns[10] + "',0,'" + div_code + "',getdate(),'" + columns[11] + "','" + strDesigName + "' , '" + columns[0] + "','" + columns[9] + "','" + columns[9] + "', " + sf_sl_no + ", '" + sEmp_ID + "','" + columns[9] + "', '" + subdivCode + "')";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();

                        div_code = div_code.Remove(div_code.Length - 1);
                        string sql1 = "INSERT INTO Mas_Salesforce_AM(Sf_Code,Sf_Name,Sf_HQ,DCR_AM,TP_AM,LstDr_AM,Leave_AM,SS_AM,Expense_AM,Otr_AM,Reporting_To,Division_Code) " +
                              " VALUES ('" + strsfcode + "' , '" + columns[2] + "','" + columns[3] + "', '" + strReportingto + "', '" + strReportingto + "', '" + strReportingto + "', '" + strReportingto + "', " +
                              " '" + strReportingto + "' ,'" + strReportingto + "', '" + strReportingto + "' , '" + strReportingto + "' ,'" + div_code + "') ";
                        SqlCommand cmd1 = new SqlCommand(sql1, conn);
                        cmd1.ExecuteNonQuery();

                        int iReturn = -1;
                        if (columns[4] == "1")
                        {
                            AdminSetup adm = new AdminSetup();
                            iReturn = adm.Add_Admin_FieldForce_Setup(strsfcode, div_code, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
                        }
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Salesforce Uploaded Sucessfully');</script>");

                    }
                    else
                    {

                    }
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(ex.Message);
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

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * from [" + sheet1 + "]", excel_con))
                    {
                        ds = new DataSet();
                        oda.Fill(ds);
                        Dt = ds.Tables[0];
                        //objAdapter1.Fill(ds);
                        //Dt = ds.Tables[0];
                    }
                    excel_con.Close();
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {      

        ImporttoDatatable();
        InsertData();
    }
  
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\UPL_SalesForce.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=UPL_SalesForce.xlsx");            
            Response.TransmitFile(fileName);
            Response.End();

        }

        
        catch (Exception ex)
        {

            // lblMessage.Text = ex.Message;

        }
    }
}
        