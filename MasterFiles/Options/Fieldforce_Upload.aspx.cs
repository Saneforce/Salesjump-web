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

public partial class MasterFiles_Options_Fieldforce_Upload : System.Web.UI.Page
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
    DataSet dslstBUOM1 = new DataSet();
    string stcode = string.Empty;
    string sfCode = string.Empty;
    string DivshName = string.Empty;
    string SuDivcode = string.Empty;
    SqlConnection conn = new SqlConnection(Globals.ConnString);
    SqlConnection conn2 = new SqlConnection(Globals.ConnString);
    DataSet ds;
    DataTable Dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            filldivision();
        }
    }
    public void filldivision()
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.Getsubdivisionwise(div_code);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldiv.DataTextField = "subdivision_name";
            ddldiv.DataValueField = "subdivision_code";
            ddldiv.DataSource = ds;
            ddldiv.DataBind();
            ddldiv.Items.Insert(0, new ListItem("--Select--", "0"));
        }
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
    protected void ddldiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillState();
    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        string stname = ddlst.SelectedItem.ToString();
        DataTable dtt = new DataTable();
        dtt.Columns.AddRange(new DataColumn[12] { new DataColumn("Emp_NO", typeof(string)),
                new DataColumn("Emp_Name", typeof(string)),
                new DataColumn("Designation", typeof(string)),
                new DataColumn("Email ", typeof(string)),
                new DataColumn("Mobile_no", typeof(string)),
                new DataColumn("Reporting_Manager_No",typeof(string)),
                new DataColumn("Reporting_manager_Name",typeof(string)),
                new DataColumn("Mail_Id",typeof(string)),
                new DataColumn("Emp_Type",typeof(string)),
                new DataColumn("HQ",typeof(string)),new DataColumn("Territory",typeof(string)),new DataColumn("State",typeof(string))});
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dtt, "Employee List");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Employee_Upload_Format.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    protected void upbt_Click(object sender, EventArgs e)
    {
        ImporttoDatatable();
        InsertData();
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
                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT  Emp_NO,Emp_Name,Designation,Email,Mobile_no,Reporting_Manager_No,Reporting_manager_Name,Mail_Id,Emp_Type,HQ,Territory,State from [" + sheet1 + "]", excel_con))
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
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "'); return false;</script>");
        }
    }
    private void InsertData()
    {
        bool uplstarted = false;
        string sf_type = string.Empty;
        string sf_code = string.Empty;
        string sf_desig = string.Empty;
        string sf_name = string.Empty;
        string desigcheck = string.Empty;
        string divsname = string.Empty;
        string usrname = string.Empty;
        string sftype = string.Empty;
        string pwd = string.Empty;
        string rtsf = string.Empty;
        string email = string.Empty;
        string mobile = string.Empty;
        string sf_sl_no = string.Empty;
        string empid = string.Empty;
        string subdiv = string.Empty;
        string sfhq = string.Empty;
        string rtmail = string.Empty;
        string depart = string.Empty;
        string divi = string.Empty;
        string terrcode = string.Empty;
        string terrname = string.Empty;
        string sfhqn = string.Empty;
        string desgname = string.Empty;
        stcode = ddlst.SelectedValue.ToString();
        string statename = string.Empty;
        try
        {
            conn2.Open();
            //SqlTransaction objTrans = conn2.BeginTransaction();
            String sql = " declare @slno varchar(10); "; String sMsg = "";
            try
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    DataRow row = Dt.Rows[i];
                    int columnCount = Dt.Columns.Count;
                    string[] columns = new string[columnCount];
                    for (int j = 0; j < columnCount; j++)
                    {
                        columns[j] = row[j].ToString();
                    }
                    int RowNo = i + 2;
                    sf_type = columns[8].ToString();
                    SalesForce slf = new SalesForce();
                    DataTable dslstDR = new DataTable();
                    DataTable dsl = new DataTable();
                    //sf_code = slf.getsfcode_new(sf_type);
                    sf_name = columns[1].ToString();
                    email = columns[3].ToString();
                    rtmail = columns[6].ToString();
                    mobile = columns[4].ToString();
                    empid = columns[0].ToString();
                    subdiv = ddldiv.SelectedValue.ToString();
                    desigcheck = columns[2].ToString();
                    sfhq = columns[9].ToString();
                    terrname = columns[10].ToString();
                    statename = columns[11].ToString();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("exec get_HQID '" + div_code + "','" + sfhq + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dslstDR);
                    conn.Close();
                    if (dslstDR.Rows.Count > 0)
                    {
                        sfhq = dslstDR.Rows[0].ItemArray.GetValue(0).ToString();
                        sfhqn = dslstDR.Rows[0].ItemArray.GetValue(1).ToString();
                    }
                    if (sfhq == "" || sfhq == null)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>console.log('" + columns[9].ToString() + "')</script>");
                        sMsg += "Unable to Get HeadQuarters Name:" + columns[9].ToString() + ". Excel Row No : " + (i + 2) + "</br>";
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + sMsg + "';</script>");
                    }
                    Designation de = new Designation();
                    DataTable desdt = new DataTable();
                    desdt = de.getDesigcodenew(div_code, desigcheck);
                    if (desdt.Rows.Count > 0)
                    {
                        sf_desig = desdt.Rows[0].ItemArray.GetValue(0).ToString();
                        desgname = desdt.Rows[0].ItemArray.GetValue(1).ToString();
                    }
                    if (sf_desig == "" || sf_desig == null)
                    {
                        sMsg += "Unable to Get Designation Name:" + columns[2].ToString() + " from Div : "+ div_code +". Excel Row No : " + (i + 2) + "</br>";
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>console.log('" + sMsg + " b')</script>");
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + sMsg + "';</script>");
                    }
                    conn.Open();
                    cmd = new SqlCommand("select Division_SName,Alias_Name from Mas_Division where Division_Code=" + div_code + "", conn);
                    dslstDR = new DataTable();
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dslstDR);
                    conn.Close();
                    divsname = dslstDR.Rows[0].ItemArray.GetValue(0).ToString();
                    pwd = dslstDR.Rows[0].ItemArray.GetValue(1).ToString();

                    conn.Open();
                    cmd = new SqlCommand("SELECT ISNULL(MAX(sf_Sl_No),0)+1 FROM mas_salesforce WHERE Sf_Code !='admin'", conn);
                    dslstDR = new DataTable();
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dslstDR);
                    conn.Close();

                    sf_sl_no = dslstDR.Rows[0].ItemArray.GetValue(0).ToString();

                    conn.Open();
                    if (sf_type == "1") sftype = "MR"; else sftype = "MGR";
                    /*{
                        sftype = "MR";
                        cmd = new SqlCommand("SELECT  isnull(max(Cast(SubString(Sf_Code,3, LEN(Sf_Code)) as int)),0)+1 FROM mas_salesforce where Sf_Code like '" + sftype + "%' ", conn);
                        da = new SqlDataAdapter(cmd);
                        dsl = new DataTable();
                        da.Fill(dsl);
                        conn.Close();
                    }
                    else
                    {
                        sftype = "MGR";
                        cmd = new SqlCommand("SELECT  isnull(max(Cast(SubString(Sf_Code,4, LEN(Sf_Code)) as int)),0)+1 FROM mas_salesforce where Sf_Code like '" + sftype + "%' ", conn);
                        da = new SqlDataAdapter(cmd);
                        dsl = new DataTable();
                        da.Fill(dsl);
                        conn.Close();
                    }
                    usrname = divsname + dsl.Rows[0].ItemArray.GetValue(0).ToString();*/

                    string DivSFTy = divsname + sftype;
                    string strQry = "select (case len(MXno) when 1 then '000' when 2 then '00' when 3 then '0' else '' end) +MXno from(select cast(isnull(max(cast(REPLACE(SF_Code, '" + DivSFTy + "', '') as numeric)), 0) + 1 as varchar) MXno from Mas_Salesforce where isnumeric(REPLACE(SF_Code, '" + DivSFTy + "', '')) = 1) as t";
                    cmd = new SqlCommand(strQry, conn);
                    da = new SqlDataAdapter(cmd);
                    dsl = new DataTable();
                    da.Fill(dsl);

                    sf_code = divsname + sftype+dsl.Rows[0].ItemArray.GetValue(0).ToString();
                    usrname = divsname + "-" + sftype+dsl.Rows[0].ItemArray.GetValue(0).ToString();

                    conn.Close();

                    conn.Open();
                    cmd = new SqlCommand("select Sf_Code from mas_salesforce where Sf_Name='" + columns[6].ToString() + "' and CHARINDEX(','+'" + div_code + "'+',',','+Division_Code+',')>0", conn);
                    da = new SqlDataAdapter(cmd);
                    dsl = new DataTable();
                    da.Fill(dsl);
                    conn.Close();
                    if (dsl.Rows.Count > 0)
                    {
                        rtsf = dsl.Rows[0].ItemArray.GetValue(0).ToString();
                    }
                    else
                    {
                        rtsf = "admin";
                    }
                    DataTable dterr = new DataTable();
                    conn.Open();
                    cmd = new SqlCommand("select Territory_code from Mas_Territory where Div_Code=" + div_code + " and Territory_Active_Flag=0 and Territory_name='" + terrname + "'", conn);
                    dslstDR = new DataTable();
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dterr);
                    conn.Close();
                    if (dterr.Rows.Count > 0)
                    {
                        terrcode = dterr.Rows[0].ItemArray.GetValue(0).ToString();
                    }
                    else
                    {
                        terrcode = "";
                    }
                    conn.Open();
                    cmd = new SqlCommand("select State_Code from Mas_State where statename='" + statename + "'", conn);
                    da = new SqlDataAdapter(cmd);
                    dsl = new DataTable();
                    da.Fill(dsl);
                    conn.Close();
                    if (dsl.Rows.Count > 0)
                    {
                        stcode = dsl.Rows[0].ItemArray.GetValue(0).ToString();
                    }
                    else
                    {
                        stcode = "0";
                    }
                    //conn.Open();
                    if (sMsg == "")
                    {
                        sql = "insert into Mas_Salesforce(Sf_Code,Sf_Name,Sf_UserName,UsrDfd_UserName,Sf_Password,Sf_Joining_Date,Reporting_To_SF,TP_Reporting_SF,State_Code,Sf_TP_DCR_Active_Dt,SF_Email,SF_Mobile,SF_Status,Hq_Code,sf_TP_Active_Dt,sf_TP_Active_Flag,Division_Code,Created_Date,sf_Sl_No,sf_type,sf_emp_id,subdivision_code,LastUpdt_Date,Employee_Id,Designation_Code,Last_Dcr_Date,Territory,Territory_Code,Sf_HQ,sf_Designation_Short_Name) " +
                              " values('" + sf_code + "','" + sf_name + "','" + usrname + "','" + usrname + "','" + pwd + "',GETDATE(),'" + rtsf + "','" + rtsf + "','" + stcode + "',GETDATE(),'" + email + "','" + mobile + "',0,'" + sfhq + "',GETDATE(),0,'" + div_code + ",',GETDATE()," + sf_sl_no + "," + sf_type + ",'" + empid + "','" + subdiv + ",',GETDATE(),'E" + sf_sl_no + "'," + sf_desig + ",GETDATE(),'" + terrname + "','" + terrcode + "','"+ sfhqn + "','"+ desgname + "')";


                        try
                        {
                            SqlCommand CmdUpl = conn2.CreateCommand();
                            //CmdUpl.Transaction = objTrans;
                            CmdUpl.CommandText = sql;
                            CmdUpl.ExecuteNonQuery();
                            uplstarted = true;
                        }
                        catch (Exception ex) 
                        {
                            //objTrans.Rollback();
                            conn2.Close();
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + ex.Message + "'; return false;</script>");
                            return;
                        }
                    }
                    else
                    {
                        if (uplstarted == true)
                        {
                            //objTrans.Rollback();
                            conn2.Close();
                        }
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + sMsg + "'; return false;</script>");
                        return;
                    }
                }
                //objTrans.Commit();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully...');</script>");
            }

            catch (Exception ex)
            {
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + ex.Message + "';</script>");
            }
        }
        catch (Exception ex)
        {
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + ex.Message + "';</script>");

        }
    }

}