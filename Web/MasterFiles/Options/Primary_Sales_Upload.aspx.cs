using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Reflection;
using System.Windows.Interop;
using System.Globalization;
using System.Web;
using System.Drawing;

public partial class MasterFiles_Options_Primary_Sales_Upload : System.Web.UI.Page
{
    #region declaration
    string div_code = string.Empty;
    DataSet ds = new DataSet();
    DataTable Dt = new DataTable();
    DataTable dslstst = new DataTable();
    DataTable dslstprod = new DataTable();
    SqlConnection conn = new SqlConnection(Globals.ConnString);
    string sf_type = string.Empty;
    public static string baseUrl = "";
    #endregion

    #region  OnPreInit
    protected override void OnPreInit(EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/Index.apx";

        if ((Convert.ToString(Session["div_code"]) != null || Convert.ToString(Session["div_code"]) != ""))
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
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/Index.apx";
        if ((Convert.ToString(Session["div_code"]) != null || Convert.ToString(Session["div_code"]) != ""))
        {
            div_code = Session["div_code"].ToString();
            filldivision();
        }
        else { Page.Response.Redirect(baseUrl, true); }

    }
    #endregion

    #region filldivision
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
    #endregion

    #region  lnkDownload_Click
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        //WindowsMediaPlayer myplayer = new WindowsMediaPlayer();

        //string fileName = Server.MapPath("~/MasterFile/Reports/AudoFile/bgm.mp3");
        //myplayer.URL = Server.MapPath("~/MasterFile/Reports/AudoFile/bgm.mp3");
        //string extension = Path.GetExtension(fileName);
        //myplayer.controls.play();

        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\Primary_Sales_Value_Upload.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Primary_Sales_Value_Upload.xlsx");
            Response.TransmitFile(fileName);
            Response.End();
        }
        catch (Exception)
        {

        }
    }
    #endregion


    #region upbt_Click
    protected void upbt_Click(object sender, EventArgs e)
    {
        ImporttoDatatable();       
    }
    #endregion

    #region ImporttoDatatable
    private void ImporttoDatatable()
    {
        String sMsg = "";
        try
        {
            bool extFlag = false;
            string connectionString = "";

            if (FlUploadcsv.HasFile)
            {
                string fileName = Path.GetFileName(FlUploadcsv.PostedFile.FileName);
                string fileExtension = Path.GetExtension(FlUploadcsv.PostedFile.FileName);
                string excelPath = HttpContext.Current.Server.MapPath("~/Upload_Document/");

                //Create the Directory.
                if (!Directory.Exists(excelPath))
                {
                    Directory.CreateDirectory(excelPath);
                }

                string fileLocation = HttpContext.Current.Server.MapPath("~/Upload_Document/" + fileName);
               
                
                if (fileExtension == ".xls")
                {
                    FlUploadcsv.SaveAs(fileLocation);

                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";                    
                    extFlag = true;
                }
                else if(fileExtension == ".xlsx")
                {

                    FlUploadcsv.SaveAs(fileLocation);

                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";                   
                    extFlag = true;
                }
                else
                {                    
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "alert('invalid file format!! you must upload a file having an extention of either (.xls) or (.xlsx)');", true);
                    extFlag = false;

                    Label6.Text = "invalid file format!! you must upload a file having an extention of either (.xls) or (.xlsx)";
                }   
            }
            if (extFlag == true)
            {

                OleDbConnection con = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;
                OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
                //DataSet ds = new DataSet();
                DataTable dtExcelRecords = new DataTable();
                con.Open();
                DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                cmd.CommandText = "SELECT [Contract_No],[Contract_Date],[Customer_Erp_Code],[Customer_Contract],[Item],[Item_ERP_Code],[Unit],[Pack],[Qty_In_Mt],[Rate],[Item_Amount] FROM [" + getExcelSheetName + "]";
                dAdapter.SelectCommand = cmd;
                dAdapter.Fill(dtExcelRecords);
                con.Close();

                ExecuteSqlTransaction(dtExcelRecords);

                if (dtExcelRecords.Rows.Count > 0)
                {
                   
                    ExecuteSqlTransaction(dtExcelRecords);
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "alert('Empty Excel File');", true);

                    Label6.Text = "Empty Excel File";
                }
            }
           

            //if (FlUploadcsv.HasFile)
            //{
            //    string excelPath = HttpContext.Current.Server.MapPath("~/Upload_Document/");

            //    //Create the Directory.
            //    if (!Directory.Exists(excelPath))
            //    {
            //        Directory.CreateDirectory(excelPath);
            //    }


            //    FlUploadcsv.SaveAs(excelPath + Path.GetFileName(FlUploadcsv.PostedFile.FileName));

            //    string conString = string.Empty;
            //    string extension = Path.GetExtension(FlUploadcsv.PostedFile.FileName);
            
            //    string sheet1 = ""; string squery = "";
              
            //    if ((extension == ".xls" || extension == ".xlsx"))
            //    {
            //        if ((extension == ".xls"))
            //        {
            //            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
            //        }
            //        else if ((extension == ".xlsx"))
            //        {
            //            conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
            //        }

            //        OleDbConnection excel_con = new OleDbConnection(conString);
                    
            //        excel_con.Open();
            //        sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    
            //        squery = " SELECT [Contract_No],[Contract_Date],[Customer_Erp_Code],[Customer_Contract],[Item],";
            //        squery += " [Item_ERP_Code],[Unit],[Pack],[Qty_In_Mt],[Rate],[Item_Amount]";
            //        squery += "  FROM [" + sheet1 + "]";

            //        //squery = "SELECT [Contract_No],[Contract_Date],[Customer_Erp_Code],[Customer_Contract],[Item],[Item_ERP_Code],[Unit],[Pack],[Qty_In_Mt],[Rate],[Item_Amount]  FROM [" + sheet1 + "]"
            //        OleDbCommand cmd = new OleDbCommand(squery, excel_con);
            //        cmd.CommandType = CommandType.Text;
            //        OleDbDataAdapter oda = new OleDbDataAdapter();
            //        oda.SelectCommand = cmd;
            //        oda.Fill(ds);
            //        excel_con.Close();
            //        excel_con.Dispose();

            //        if (ds.Tables.Count > 0)
            //        {
            //            Dt = ds.Tables[0];
            //            ExecuteSqlTransaction(Dt);
            //        }
            //        else
            //        {
            //            Label6.Text = "Empty Excel Sheet";
            //        }
            //    }
            //    else
            //    {
            //        sMsg += "Upload Only Excel File";

            //        Label6.Text = "Upload Only Excel File";
            //        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANG
            //        //UAGE='javascript'>alert('Upload Only Excel File'); return false;</script>");
            //    }
            //}
        }
        catch (Exception ex)
        {

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "alert('" + Convert.ToString(ex.Message.ToString()) + "');", true);

            //sMsg += "Error Message: " + Convert.ToString(ex.Message) + "";
            Label6.Text = ex.Message;

            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "'); return false;</script>");            
        }
    }
    #endregion

    #region ExecuteSqlTransaction
    private void ExecuteSqlTransaction(DataTable Dt)
    {
        string sf_Code = string.Empty;
        string sf_Name = string.Empty;
        string stk_Code = string.Empty;
        string stk_Name = string.Empty;
        string prod_Code = string.Empty;
        string prod_Name = string.Empty;

        String sMsg = ""; 
        lstdr dr = new lstdr();

        try
        {
            if (Dt.Rows.Count > 0)
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
                    if ((columns[0].ToString() != "" && columns[0].ToString() != null) &&
                               (columns[1].ToString() != "" && columns[1].ToString() != null) &&
                               (columns[2].ToString() != "" && columns[2].ToString() != null) &&
                               (columns[3].ToString() != "" && columns[3].ToString() != null) &&
                               (columns[4].ToString() != "" && columns[4].ToString() != null) &&
                               (columns[5].ToString() != "" && columns[5].ToString() != null) &&
                               (columns[6].ToString() != "" && columns[6].ToString() != null) &&
                               (columns[7].ToString() != "" && columns[7].ToString() != null) &&
                               (columns[8].ToString() != "" && columns[8].ToString() != null) &&
                               (columns[9].ToString() != "" && columns[9].ToString() != null) &&
                               (columns[10].ToString() != "" && columns[10].ToString() != null))
                    {

                        using (SqlConnection con = new SqlConnection(Globals.ConnString))
                        {

                            DataTable dslstst = dr.Getstk_code(Convert.ToString(columns[3].Trim()), Convert.ToString(columns[2].Trim()), div_code.Trim());

                            if (dslstst.Rows.Count > 0)
                            {
                                stk_Code = dslstst.Rows[0]["Stockist_Code"].ToString();
                                stk_Name = dslstst.Rows[0]["Stockist_Name"].ToString();
                            }
                            else
                            {
                                sMsg += "Unable to Upload Distributor :" + columns[3].ToString() + ". Excel Row No : " + (j + 2) + "</br>";

                            }
                            DataTable dslstprod = dr.GetProd_detail(Convert.ToString(columns[5].Trim()), Convert.ToString(columns[4]).Trim(), Convert.ToString(columns[6]).Trim(), div_code);

                            if (dslstprod.Rows.Count > 0)
                            {
                                prod_Code = dslstprod.Rows[0]["Product_Detail_Code"].ToString();
                                prod_Name = dslstprod.Rows[0]["Product_Detail_Name"].ToString();
                            }
                            else
                            {
                                sMsg += "Unable to Upload Product :" + columns[5].ToString() + ". Excel Row No : " + (j + 2) + "</br>";
                            }

                            if (dslstst.Rows.Count > 0 && dslstprod.Rows.Count > 0)
                            {
                                try
                                {

                                    string cd = Convert.ToString(columns[1].Trim());

                                    //DateTime ContractDate = DateTime.ParseExact(Convert.ToString(columns[1].Trim()), "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);                                   
                                    //DateTime date =  DateTime.ParseExact(columns[1].Trim(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                    DateTime date = Convert.ToDateTime(columns[1]);
                                    string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");


                                    con.Open();
                                    SqlCommand cmd = new SqlCommand("Insert_PrimarySalesValue", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@Div", div_code.ToString().Trim());
                                    cmd.Parameters.AddWithValue("@ContractNo", Convert.ToString(columns[0].Trim()));
                                    cmd.Parameters.AddWithValue("@ContractDate", Convert.ToString(formattedDate));
                                    cmd.Parameters.AddWithValue("@Erp_Cust_Code", Convert.ToString(columns[2].Trim()));
                                    cmd.Parameters.AddWithValue("@CustomerContract", Convert.ToString(columns[3].Trim()));
                                    cmd.Parameters.AddWithValue("@Item", Convert.ToString(columns[4].Trim()));
                                    cmd.Parameters.AddWithValue("@EDPItemCode", Convert.ToString(columns[5].Trim()));
                                    cmd.Parameters.AddWithValue("@Unit", Convert.ToString(columns[6].Trim()));
                                    cmd.Parameters.AddWithValue("@Pack", Convert.ToString(columns[7].Trim()));
                                    cmd.Parameters.AddWithValue("@QtyINMt", Convert.ToString(columns[8].Trim()));
                                    cmd.Parameters.AddWithValue("@Rate", Convert.ToString(columns[9].Trim()));
                                    cmd.Parameters.AddWithValue("@Sales_Value", Convert.ToDecimal(columns[10].Trim()));
                                    //cmd.Parameters.Add("@returnMessage", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
                                    cmd.Parameters.Add("@returnMessage", SqlDbType.NVarChar, 150);
                                    cmd.Parameters["@returnMessage"].Direction = ParameterDirection.Output;
                                    cmd.ExecuteNonQuery();
                                    string outmsg = Convert.ToString(cmd.Parameters["@returnMessage"].Value);
                                    con.Close();
                                                                        
                                    sMsg += Convert.ToString(outmsg);

                                    //    con.Open();
                                    //    SqlCommand cmd = new SqlCommand("Insert_PrimarySalesValue", con);
                                    //    cmd.CommandType = CommandType.StoredProcedure;
                                    //    SqlParameter[] parameters = new SqlParameter[]
                                    //    {
                                    //new SqlParameter("@Div", div_code.Trim()),
                                    //new SqlParameter("@ContractNo",Convert.ToString(columns[0].Trim()) ),
                                    //new SqlParameter("@ContractDate", Convert.ToDateTime(columns[1].Trim())),
                                    //new SqlParameter("@Erp_Cust_Code", Convert.ToString(columns[2].Trim())),
                                    //new SqlParameter("@CustomerContract", Convert.ToString(columns[3].Trim())),
                                    //new SqlParameter("@Item", Convert.ToString(columns[4]).Trim()),
                                    //new SqlParameter("@EDPItemCode", Convert.ToString(columns[5].Trim())),
                                    //new SqlParameter("@Unit", Convert.ToString(columns[6]).Trim()),
                                    //new SqlParameter("@Pack", Convert.ToString(columns[7]).Trim()),
                                    //new SqlParameter("@QtyINMt", Convert.ToString(columns[8]).Trim()),
                                    //new SqlParameter("@Rate", Convert.ToString(columns[9].Trim())),
                                    //new SqlParameter("@Sales_Value", Convert.ToDecimal(columns[10].Trim()))
                                    //    };
                                    //    cmd.Parameters.AddRange(parameters);

                                    //    int i = cmd.ExecuteNonQuery();

                                    //    con.Close();
                                    //    if (i > 0)
                                    //    {
                                    //        sMsg += " Product_Code : " + Convert.ToString(columns[5]).Trim() + "  AND  Distributor_Code : " + Convert.ToString(columns[2]).Trim() + "  Uploaded Successfully... " + (j + 2) + " <br /> ";
                                    //    }
                                }
                                catch (Exception ex)
                                {
                                    sMsg += " Error Message: " + Convert.ToString(ex.Message.ToString()) + " ";
                                }
                            }
                           
                        }
                    }
                    else
                    {
                        sMsg += " Error Message: Fill the All Coloumns In Excel , Row No " + j + 1 + " ";
                    }
                }

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>document.getElementById('dvStatus').innerHTML='" + sMsg + "';</script>");
            }
            else
            {
                //sMsg += " Error Message: No Data In Excel ";
                Label6.Text = "Error Message: No Data In Excel";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "alert('Error Message: No Data In Excel ');", true);
            }
        }
        catch (Exception ex)
        {
            //sMsg += Convert.ToString(ex.Message);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "alert('" + Convert.ToString(ex.Message) + "');", true);
            Label6.Text = Convert.ToString(ex.Message);
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "'); return false;</script>");
        }

       
    }
    #endregion

    #region lstdr
    public class lstdr
    {

        public DataTable GetProd_detail(string Sale_Erp_code, string Product_Detail_Name, string product_unit, string div_code)
        {

            DataTable dsAdmin = new DataTable();

            string strQry = " SELECT TOP(1) * FROM Mas_Product_Detail  Where Sale_Erp_code=@Sale_Erp_code AND Product_Detail_Name=@Product_Detail_Name   AND ";
            //strQry += " Product_Detail_Name=@Product_Detail_Name   AND Product_Active_Flag=0 AND product_unit=@product_unit AND Division_Code = @Division_Code ";
            strQry += "  Product_Active_Flag=0  AND  Division_Code = @Division_Code ";

            try
            {
                using (var con = new SqlConnection(Globals.ConnString))
                {
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = strQry;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Sale_Erp_code", Convert.ToString(Sale_Erp_code));
                        cmd.Parameters.AddWithValue("@Product_Detail_Name", Convert.ToString(Product_Detail_Name));
                        cmd.Parameters.AddWithValue("@Division_Code", Convert.ToInt32(div_code));
                        SqlDataAdapter dap = new SqlDataAdapter();
                        dap.SelectCommand = cmd;
                        con.Open();
                        dap.Fill(dsAdmin);
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return dsAdmin;
        }

        public DataTable Getstk_code(string stk_Name, string ERP_Code, string div_code)
        {

            DataTable dsAdmin = new DataTable();

            //string strQry = "SELECT TOP(1) * FROM Mas_Stockist  Where stockist_name=@stockist_name AND ";
            //strQry += " ERP_Code=@ERP_Code   AND Stockist_Active_Flag=0 AND Division_Code = @Division_Code ";

            string strQry = "SELECT TOP(1) * FROM Mas_Stockist  Where stockist_name=@stockist_name  AND ";
            strQry += " ERP_Code=@ERP_Code  AND Stockist_Active_Flag=0 AND Division_Code = @Division_Code  ";


            try
            {
                using (var con = new SqlConnection(Globals.ConnString))
                {
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = strQry;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@stockist_name", Convert.ToString(stk_Name));
                        cmd.Parameters.AddWithValue("@ERP_Code", Convert.ToString(ERP_Code));
                        cmd.Parameters.AddWithValue("@Division_Code", Convert.ToInt32(div_code));

                        SqlDataAdapter dap = new SqlDataAdapter();
                        dap.SelectCommand = cmd;
                        con.Open();
                        dap.Fill(dsAdmin);
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return dsAdmin;

        }

        public int insert_PrimarySalesValue(string div_code, string ContractNo, string ContractDate, string Erp_Cust_Code, string CustomerContract,
            string Item, string EDPItemCode, string Unit, string pack, string QtyINMt, string Rate, string Sales_Value)
        {
            int iReturn = -1;

            using (SqlConnection _conn = new SqlConnection(Globals.ConnString))
            {
                try
                {
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("Insert_PrimarySalesValue", _conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Div", div_code.Trim()),
                        new SqlParameter("@ContractNo", Convert.ToString(ContractNo.Trim())),
                        new SqlParameter("@ContractDate", Convert.ToDateTime(ContractDate.Trim())),
                        new SqlParameter("@Erp_Cust_Code", Convert.ToString(Erp_Cust_Code.Trim())),
                        new SqlParameter("@CustomerContract", Convert.ToString(CustomerContract.Trim())),
                        new SqlParameter("@Item", Convert.ToString(Item.Trim())),
                        new SqlParameter("@EDPItemCode", Convert.ToString(EDPItemCode.Trim())),
                        new SqlParameter("@Unit", Convert.ToString(Unit.Trim())),
                        new SqlParameter("@Pack", Convert.ToString(pack.Trim())),
                        new SqlParameter("@QtyINMt", Convert.ToString(QtyINMt.Trim())),
                        new SqlParameter("@Rate", Convert.ToString(Rate.Trim())),
                        new SqlParameter("@Sales_Value", Convert.ToDecimal(Sales_Value.Trim()))
                    };

                    cmd.Parameters.AddRange(parameters);

                    if (_conn.State == ConnectionState.Closed)
                    {
                        _conn.Open();
                    }

                    iReturn = cmd.ExecuteNonQuery();

                    _conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }
                    _conn.Dispose();
                }
            }

            return iReturn;
        }

    }
    #endregion
}