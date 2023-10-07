using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



using Bus_EReport;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data.Common;
using System.Globalization;

using System.Data;
using System.Collections;


public partial class MasterFiles_Options_Pending_Bills_Upload : System.Web.UI.Page
{
    DataSet ds;
    DataTable Dt;
    DataSet dsSalesForce = null;
    string Div_Code = string.Empty;
    string SF_Code = string.Empty;
    int output = 1;
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
		Label1.Visible=false;
        Div_Code = Session["div_code"].ToString();
        SF_Code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillMRManagers();
        }
    }


    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = new DataSet();
        dsSalesForce = sf.SalesForceListMgrGet(Div_Code, SF_Code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            //  DDLFO.DataTextField = "sf_name";
            //  DDLFO.DataValueField = "sf_code";
            // DDLFO.DataSource = dsSalesForce;
            // DDLFO.DataBind();
            // DDLFO.Items.Insert(0, new ListItem("--Select--", "0"));

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
			string fileName = Server.MapPath("~\\Document\\UPL_Pending_Bills.xlsx");
            //string fileName = "\\\\10.0.2.30\\SalesJump\\E-Report_DotNet\\Document\\UPL_Pending_Bills.xlsx";
            Response.AppendHeader("Content-Disposition", "attachment; filename=UPL_Pending_Bills.xlsx");
            Response.TransmitFile(fileName);
            Response.Flush();
            Response.End();



            //Response.ContentType = "application/vnd.ms-excel";
            //string fileName = Server.MapPath("~\\Document\\UPL_Pending_Bills.xlsx");
            //Response.AppendHeader("Content-Disposition", "attachment; filename=UPL_Pending_Bills.xlsx");
            //Response.TransmitFile(fileName);
            //Response.End();
        }

        catch (Exception ex)
        {
            // lblMessage.Text = ex.Message;
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
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
				if (conString == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Upload Only Excel File...');</script>");
                }
				else
				{
                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    excel_con.Open();
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    DataTable dtExcelData = new DataTable();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * from [" + sheet1 + "]", excel_con))
                    {
                        ds = new DataSet();
                        oda.Fill(ds);
                        Dt = new DataTable();
                        Dt = ds.Tables[0];
                    }
                    excel_con.Close();
                }
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
        string sf_Username = string.Empty;
        string strsfcode = string.Empty;
        string Strtype = string.Empty;
        string Pool_Name = string.Empty;
        string Pool_NameNew = string.Empty;
        string slNO = string.Empty;
		string ColumnName = string.Empty;
        string ColumnData = string.Empty;
        var arrayList = new ArrayList();
        var checklist = new ArrayList();
        int msg = 0;

        SqlConnection con = new SqlConnection(Globals.ConnString);



        // string sf_code = DDLFO.SelectedValue.ToString();

        if (Dt == null)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Execel File');</script>");
            return;
        }
		
		 foreach (DataRow row in Dt.Rows)
        {
            foreach (DataColumn column in Dt.Columns)
            {
                ColumnName = column.ColumnName;
                arrayList.Add(ColumnName);
                if (arrayList.Count == 7)
                    break;
            }
            break;
        }

        checklist.Add("DistributorCode");
        checklist.Add("DistributorName");
        checklist.Add("RetailerCode");
        checklist.Add("RetailerName");
        checklist.Add("BillNo");
        checklist.Add("BillDate");
        checklist.Add("BillAmount");


        for (int i = 0; i < arrayList.Count; i++)
        {
            if (arrayList[i].ToString() != checklist[i].ToString())
                msg++;
        }

        Dt.Columns.Add("Retailer_Code", typeof(System.String));
        Dt.Columns.Add("SF_Code", typeof(System.String));
        Dt.Columns.Add("Uploaded_Date", typeof(System.DateTime));
        Dt.Columns.Add("Coll_Amt", typeof(System.String));
        Dt.Columns.Add("Bal_Amt", typeof(System.String));
        Dt.Columns.Add("div_code", typeof(System.String));
        Dt.Columns.Add("Adv_amt", typeof(System.String));
        Dt.Columns.Add("Adv_adj", typeof(System.String));
        Dt.Columns.Add("inv_col", typeof(System.String));
        Dt.Columns.Add("pay_col", typeof(System.String));
        Dt.Columns.Add("cn_col", typeof(System.String));

        string err = string.Empty;
        int count = 1;
		if (msg ==0)
        {
        foreach (DataRow row in Dt.Rows)
        {
            //EmployeeID

            string sqlQry = "select stockist_code from mas_stockist where charindex(','+'" + Div_Code + "'+ ',',','+Division_Code+',')>0 and stockist_code='" + row["DistributorCode"].ToString() + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(sqlQry, con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtFo = new DataTable();
            da.Fill(dtFo);
            con.Close();

            if (dtFo.Rows.Count > 0)
            {
                sqlQry = "select ListedDrCode Retailer_Code,ListedDr_Name Customer_Name, Sf_Code as Sf_Code ,Code from Mas_ListedDr  where division_code='" + Div_Code + "' and charindex(','+'" + dtFo.Rows[0]["stockist_code"].ToString() + "'+ ',',','+Dist_name+',')>0  and ListedDrCode='" + row["RetailerCode"].ToString() + "'";
                cmd = new SqlCommand(sqlQry, con);
                con.Open();
                da = new SqlDataAdapter(cmd);
                DataTable dtR = new DataTable();
                da.Fill(dtR);
                con.Close();

                if (dtR.Rows.Count > 0)
                {

                    sqlQry = "select BIllNo from Mas_Pending_Bills where CHARINDEX(','+'" + dtFo.Rows[0]["stockist_code"].ToString() + "' +',',','+sf+',')>0  and CustCode='" + dtR.Rows[0]["Retailer_Code"].ToString() + "' and div_code='" + Div_Code + "'";
                    cmd = new SqlCommand(sqlQry, con);
                    con.Open();
                    da = new SqlDataAdapter(cmd);
                    DataTable dtPen = new DataTable();
                    da.Fill(dtPen);
                    con.Close();
                    foreach (DataRow rr in dtPen.Rows)
                    {
                        if (rr["BillNo"].ToString().Trim() == row["BillNo"].ToString().Trim())
                        {
                            err += "Row No. : " + count + " Retailer Code : " + row["RetailerCode"].ToString() + " and Stockist : " + dtFo.Rows[0]["stockist_code"].ToString() + " " + row["BillNo"].ToString() + " : Bill No. Exist  ";
                        }
                    }
                    foreach (DataRow r in dtR.Rows)
                    {

                        row["Retailer_Code"] = r["Retailer_Code"].ToString();
                        row["SF_Code"] =  dtFo.Rows[0]["stockist_code"].ToString();
                        //row["Uploaded_Date"] = DateTime.Now;
						//DateTime.ParseExact(row["BillDate"].ToString().Replace(",", ""), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //DateTime.ParseExact( row["BillDate"].ToString().Replace(",",""), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        row["Coll_Amt"] = "0";
                        row["Bal_Amt"] = "0";
						row["div_code"] = Div_Code;
                        row["Adv_amt"] = "0";
                        row["Adv_adj"] = "0";
                        row["inv_col"] = "0";
                        row["pay_col"] = "0";
                        row["cn_col"] = "0";
                    }

                }
                else
                {
                    err += "Row No. : " + count + " Retailer Code  : " + row["RetailerCode"].ToString() + " and Stockist : " + dtFo.Rows[0]["stockist_code"].ToString() + " Not Match ";
                }
            }
            else
            {
                err += " Row No. : " + count + " Distributor Id : " + row["DistributorCode"].ToString() + " Not Match ";
            }
            count++;
        }

        if (err != string.Empty)
        {
			Label1.Visible = true;

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + err + "';</script>");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error!');</script>");
            return;
        }
        else
        {


            int i = 1;
            if (Dt.Rows.Count > 0)
            {
                string consString = Globals.ConnString;
                using (SqlConnection conn = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(conn))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.Mas_Pending_Bills";
                        sqlBulkCopy.ColumnMappings.Add("SF_Code", "SF");
                        sqlBulkCopy.ColumnMappings.Add("Retailer_Code", "CustCode");
                        sqlBulkCopy.ColumnMappings.Add("BillNo", "BillNo");
                        sqlBulkCopy.ColumnMappings.Add("BillDate", "Inv_Dt");
                        sqlBulkCopy.ColumnMappings.Add("BillAmount", "BillAmt");
                        sqlBulkCopy.ColumnMappings.Add("Coll_Amt", "Coll_Amt");
                        sqlBulkCopy.ColumnMappings.Add("BillAmount", "Bal_Amt");
						sqlBulkCopy.ColumnMappings.Add("BillNo", "Invoice_No");
                        sqlBulkCopy.ColumnMappings.Add("div_code", "div_code");
                        sqlBulkCopy.ColumnMappings.Add("Adv_amt", "Adv_amt");
                        sqlBulkCopy.ColumnMappings.Add("Adv_adj", "Adv_adj");
                        sqlBulkCopy.ColumnMappings.Add("inv_col", "inv_col");
                        sqlBulkCopy.ColumnMappings.Add("pay_col", "pay_col");
                        sqlBulkCopy.ColumnMappings.Add("cn_col", "cn_col");


                        try
                        {
                            conn.Open();
                            sqlBulkCopy.WriteToServer(Dt);
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
                        }
                        finally
                        {

                        }
                        i++;
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error! Uploaded File is Empty');</script>");
            }

            if (i == 2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Upload Successful');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Upload Failed');</script>");
            }
        }
		 }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Upload valid ExcelFormat...');</script>");
        }
    }
}