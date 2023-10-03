using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Bus_EReport;
using ClosedXML.Excel;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;

public partial class MasterFiles_Sec_Order_Dtl_RepView : System.Web.UI.Page
{
    #region "Declaration"
    string div_code = string.Empty;
    public string sf_code = string.Empty;
    public string sf_type = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsTP = null;
    public static string sub_division = string.Empty;
    #endregion
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
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        sub_division = Session["sub_division"].ToString();
        if (!Page.IsPostBack)
        {
            fillsubdivision();
            FillMRManagers("0");
        }
    }
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code, sub_division);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (subdiv.SelectedValue.ToString() != "0")
        {
            FillMRManagers(subdiv.SelectedValue.ToString());
        }
        else
        {
            FillMRManagers(subdiv.SelectedValue.ToString());
        }
    }

    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();


        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code, Sub_Div_Code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("---Select Field Force---", "0"));

        }
    }

     protected void btnexl_Click(object sender, EventArgs e)
    {
        string sfc = HiddenField1.Value;
        string fdt = HiddenField3.Value;
        string tdt = HiddenField4.Value;
        string subdiv = HiddenField2.Value;
        div_code = Session["div_code"].ToString();

        DataTable dtUsers = new DataTable();
        DataTable dtOrders = new DataTable();
        DataTable dtCalls = new DataTable();
        DataTable dtProducts = new DataTable();
        DataTable dtRetailers = new DataTable();
        DataTable dtPOB = new DataTable();

        if(div_code=="98")
        {
            dtPOB.Columns.Add("Order_No", typeof(string));
            dtPOB.Columns.Add("Reporting_To_SF", typeof(string));
            dtPOB.Columns.Add("Employee_ID", typeof(string));
            dtPOB.Columns.Add("Designation_Short_Name", typeof(string));
            dtPOB.Columns.Add("HQ", typeof(string));
            dtPOB.Columns.Add("Territory", typeof(string));
            dtPOB.Columns.Add("SR_Name", typeof(string));
            dtPOB.Columns.Add("Retailer_Code", typeof(string));
            dtPOB.Columns.Add("Retailer_Name", typeof(string));
            dtPOB.Columns.Add("Retailer_Channel", typeof(string));
            dtPOB.Columns.Add("Retailer_Class", typeof(string));
            dtPOB.Columns.Add("Contact_Person_Name", typeof(string));
            dtPOB.Columns.Add("Mobile", typeof(string));
            dtPOB.Columns.Add("Email", typeof(string));
            dtPOB.Columns.Add("GST", typeof(string));
            dtPOB.Columns.Add("Address", typeof(string));
            dtPOB.Columns.Add("Route_Name", typeof(string));
            dtPOB.Columns.Add("City", typeof(string));
            dtPOB.Columns.Add("PIN_Code", typeof(string));
            dtPOB.Columns.Add("StateName", typeof(string));
            dtPOB.Columns.Add("Created_Date", typeof(string));
            dtPOB.Columns.Add("Distributor Code", typeof(string));
            dtPOB.Columns.Add("Distributor Name", typeof(string));
            dtPOB.Columns.Add("Order_Date", typeof(string));
            dtPOB.Columns.Add("Time", typeof(string));
            dtPOB.Columns.Add("Product Category", typeof(string));
            dtPOB.Columns.Add("Product Code", typeof(string));
            dtPOB.Columns.Add("Product Name", typeof(string));
            dtPOB.Columns.Add("Quantity", typeof(string));
            dtPOB.Columns.Add("Unit", typeof(string));
            dtPOB.Columns.Add("Discount", typeof(string));
            dtPOB.Columns.Add("Price", typeof(string));
            dtPOB.Columns.Add("Sale_Value", typeof(string));
            dtPOB.Columns.Add("Net_Value", typeof(string));
            dtPOB.Columns.Add("No_Sale_reason", typeof(string));
        }
        else
        {
            dtPOB.Columns.Add("Order_No", typeof(string));
            dtPOB.Columns.Add("Reporting_To_SF", typeof(string));
            dtPOB.Columns.Add("Employee_ID", typeof(string));
            dtPOB.Columns.Add("Designation_Short_Name", typeof(string));
            dtPOB.Columns.Add("HQ", typeof(string));
            dtPOB.Columns.Add("Territory", typeof(string));
            dtPOB.Columns.Add("SR_Name", typeof(string));
            dtPOB.Columns.Add("Retailer_Code", typeof(string));
            dtPOB.Columns.Add("Retailer_Name", typeof(string));
            dtPOB.Columns.Add("Retailer_Channel", typeof(string));
            dtPOB.Columns.Add("Retailer_Class", typeof(string));
            dtPOB.Columns.Add("Contact_Person_Name", typeof(string));
            dtPOB.Columns.Add("Mobile", typeof(string));
            dtPOB.Columns.Add("Email", typeof(string));
            dtPOB.Columns.Add("GST", typeof(string));
            dtPOB.Columns.Add("Address", typeof(string));
            dtPOB.Columns.Add("Route_Name", typeof(string));
            dtPOB.Columns.Add("City", typeof(string));
            dtPOB.Columns.Add("PIN_Code", typeof(string));
            dtPOB.Columns.Add("StateName", typeof(string));
            dtPOB.Columns.Add("Created_Date", typeof(string));

            dtPOB.Columns.Add("Distributor Name", typeof(string));
            dtPOB.Columns.Add("Order_Date", typeof(string));
            dtPOB.Columns.Add("Time", typeof(string));
            dtPOB.Columns.Add("Product Category", typeof(string));

            
            dtPOB.Columns.Add("Product Name", typeof(string));
            dtPOB.Columns.Add("Quantity", typeof(string));
            dtPOB.Columns.Add("Unit", typeof(string));
            dtPOB.Columns.Add("Discount", typeof(string));
            dtPOB.Columns.Add("Price", typeof(string));
            dtPOB.Columns.Add("Sale_Value", typeof(string));
            dtPOB.Columns.Add("Net_Value", typeof(string));
            dtPOB.Columns.Add("No_Sale_reason", typeof(string));
        }

        
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec getPOB_Users '" + sfc + "','" + div_code + "','" + subdiv + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtUsers);
        con.Close();
        con.Open();
        cmd = new SqlCommand("exec getPOB_Orders '" + sfc + "','" + div_code + "','" + fdt + "','" + tdt + "','" + subdiv + "'", con);
        da = new SqlDataAdapter(cmd);
        da.Fill(dtOrders);
        con.Close();
        con.Open();
        cmd = new SqlCommand("exec getPOB_Calls '" + sfc + "','" + div_code + "','" + fdt + "','" + tdt + "','" + subdiv + "'", con);
        da = new SqlDataAdapter(cmd);
        da.Fill(dtCalls);
        con.Close();
        con.Open();
        cmd = new SqlCommand("exec getPOB_Products '" + div_code + "','" + subdiv + "'", con);
        da = new SqlDataAdapter(cmd);
        da.Fill(dtProducts);
        con.Close();
        con.Open();
        cmd = new SqlCommand("exec getPOB_Retailers '" + sfc + "','" + div_code + "','" + fdt + "','" + tdt + "','" + subdiv + "'", con);
        da = new SqlDataAdapter(cmd);
        da.Fill(dtRetailers);
        con.Close();

        if (div_code == "98")
        {
            for (int i = 0; i < dtOrders.Rows.Count; i++)
            {
                DataRow dr = dtPOB.NewRow();
                DataRow[] drP = dtProducts.Select("Product_Detail_Code1='" + dtOrders.Rows[i]["Product_Detail_Code1"].ToString() + "'");
                DataRow[] drS = dtUsers.Select("Sf_Code='" + dtOrders.Rows[i]["Sf_Code"].ToString() + "'");
                DataRow[] drC = dtRetailers.Select("Retailer_Code='" + dtOrders.Rows[i]["Cust_Code"].ToString() + "'");
                DataRow[] drD = dtCalls.Select("Trans_Detail_Slno='" + dtOrders.Rows[i]["DCR_Code"].ToString() + "'");

                dr["Order_No"] = dtOrders.Rows[i]["Trans_Sl_No"].ToString();
                dr["Reporting_To_SF"] = drS[0]["Reporting_To"].ToString();
                dr["Employee_ID"] = drS[0]["Emp_ID"].ToString();
                dr["Designation_Short_Name"] = drS[0]["Desig"].ToString();
                dr["HQ"] = drS[0]["HQ"].ToString();
                dr["Territory"] = drS[0]["Territory"].ToString();
                dr["SR_Name"] = drS[0]["Emp_Name"].ToString();

                dr["Retailer_Code"] = drC[0]["Retailer_Code"].ToString();
                dr["Retailer_Name"] = drC[0]["Retailer_Name"].ToString();
                dr["Retailer_Channel"] = drC[0]["Channel"].ToString();
                dr["Retailer_Class"] = drC[0]["Class"].ToString();
                dr["Contact_Person_Name"] = drC[0]["Contact_Person"].ToString();
                dr["Mobile"] = drC[0]["Mobile"].ToString();
                dr["Email"] = drC[0]["Email"].ToString();
                dr["GST"] = drC[0]["GST"].ToString();
                dr["Address"] = drC[0]["Address"].ToString();
                dr["City"] = drC[0]["City"].ToString();
                dr["PIN_Code"] = drC[0]["Pin_Code"].ToString();
                dr["Created_Date"] = drC[0]["Created_Date"].ToString();
                dr["Route_Name"] = (drD.Length > 0) ? drD[0]["SDP_Name"].ToString() : "";

                dr["Distributor Code"] = (drD.Length > 0) ? drD[0]["stockist_code"].ToString() : "";

                dr["Distributor Name"] = (drD.Length > 0) ? drD[0]["stockist_name"].ToString() : "";
                dr["Order_Date"] = (drD.Length > 0) ? drD[0]["Order_Date"].ToString() : "";
                dr["Time"] = (drD.Length > 0) ? drD[0]["Time"].ToString() : "";
                dr["No_Sale_reason"] = (drD.Length > 0) ? drD[0]["No_Sale_reason"].ToString() : "";

                dr["Product Category"] = (drP.Length > 0) ? drP[0]["Product_Cat_Name"].ToString() : "";
                dr["Product Code"] = (drP.Length > 0) ? drP[0]["Erp_Code"].ToString() : "";
                dr["Product Name"] = (drP.Length > 0) ? drP[0]["Product_Detail_Name"].ToString() : "";

                dr["Quantity"] = dtOrders.Rows[i]["Quantity"].ToString();
                dr["Unit"] = (drP.Length > 0) ? drP[0]["product_unit"].ToString() : "";
                dr["Discount"] = dtOrders.Rows[i]["Discount"].ToString();
                dr["Price"] = dtOrders.Rows[i]["Price"].ToString();
                dr["Sale_Value"] = dtOrders.Rows[i]["Sale_Value"].ToString();
                dr["Net_Value"] = dtOrders.Rows[i]["Net_Value"].ToString();
                dtPOB.Rows.Add(dr);
            }
        }
        else
        {
            for (int i = 0; i < dtOrders.Rows.Count; i++)
            {
                DataRow dr = dtPOB.NewRow();
                DataRow[] drP = dtProducts.Select("Product_Detail_Code='" + dtOrders.Rows[i]["Product_Code"].ToString() + "'");
                DataRow[] drS = dtUsers.Select("Sf_Code='" + dtOrders.Rows[i]["Sf_Code"].ToString() + "'");
                DataRow[] drC = dtRetailers.Select("Retailer_Code='" + dtOrders.Rows[i]["Cust_Code"].ToString() + "'");
                DataRow[] drD = dtCalls.Select("Trans_Detail_Slno='" + dtOrders.Rows[i]["DCR_Code"].ToString() + "'");

                dr["Order_No"] = dtOrders.Rows[i]["Trans_Sl_No"].ToString();
                dr["Reporting_To_SF"] = drS[0]["Reporting_To"].ToString();
                dr["Employee_ID"] = drS[0]["Emp_ID"].ToString();
                dr["Designation_Short_Name"] = drS[0]["Desig"].ToString();
                dr["HQ"] = drS[0]["HQ"].ToString();
                dr["Territory"] = drS[0]["Territory"].ToString();
                dr["SR_Name"] = drS[0]["Emp_Name"].ToString();

                dr["Retailer_Code"] = drC[0]["Retailer_Code"].ToString();
                dr["Retailer_Name"] = drC[0]["Retailer_Name"].ToString();
                dr["Retailer_Channel"] = drC[0]["Channel"].ToString();
                dr["Retailer_Class"] = drC[0]["Class"].ToString();
                dr["Contact_Person_Name"] = drC[0]["Contact_Person"].ToString();
                dr["Mobile"] = drC[0]["Mobile"].ToString();
                dr["Email"] = drC[0]["Email"].ToString();
                dr["GST"] = drC[0]["GST"].ToString();
                dr["Address"] = drC[0]["Address"].ToString();
                dr["City"] = drC[0]["City"].ToString();
                dr["PIN_Code"] = drC[0]["Pin_Code"].ToString();
                dr["Created_Date"] = drC[0]["Created_Date"].ToString();

                dr["Route_Name"] = (drD.Length > 0) ? drD[0]["SDP_Name"].ToString() : "";

               
                dr["Distributor Name"] = (drD.Length > 0) ? drD[0]["stockist_name"].ToString() : "";
                dr["Order_Date"] = (drD.Length > 0) ? drD[0]["Order_Date"].ToString() : "";
                dr["Time"] = (drD.Length > 0) ? drD[0]["Time"].ToString() : "";
                dr["No_Sale_reason"] = (drD.Length > 0) ? drD[0]["No_Sale_reason"].ToString() : "";

                dr["Product Category"] = (drP.Length > 0) ? drP[0]["Product_Cat_Name"].ToString() : "";
                
                dr["Product Name"] = (drP.Length > 0) ? drP[0]["Product_Detail_Name"].ToString() : "";
                dr["Quantity"] = dtOrders.Rows[i]["Quantity"].ToString();
                dr["Unit"] = (drP.Length > 0) ? drP[0]["product_unit"].ToString() : "";
                dr["Discount"] = dtOrders.Rows[i]["Discount"].ToString();
                dr["Price"] = dtOrders.Rows[i]["Price"].ToString();
                dr["Sale_Value"] = dtOrders.Rows[i]["Sale_Value"].ToString();
                dr["Net_Value"] = dtOrders.Rows[i]["Net_Value"].ToString();
                dtPOB.Rows.Add(dr);
            }
        }

      
		string filename = System.IO.Path.GetTempPath() + "POB Secondary_"+ div_code + "_"+(System.DateTime.Now.ToString()).Replace(":","_").Replace("/","_").Replace(" ","_")+".xlsx";
        if (File.Exists(filename))
        {
            filename = System.IO.Path.GetTempPath() + "POB Secondary_"+ div_code + "_"+(System.DateTime.Now.ToString()).Replace(":","_").Replace("/","_").Replace(" ","_")+".xlsx";
        }
		uint sheetId = 1;
        SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filename, SpreadsheetDocumentType.Workbook);
        WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
        workbookpart.Workbook = new Workbook();
        var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
        var sheetData = new SheetData();
        worksheetPart.Worksheet = new Worksheet(sheetData);
        Sheets sheets;
        sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
        var sheet = new Sheet()
        {
            Id = spreadsheetDocument.WorkbookPart.
                       GetIdOfPart(worksheetPart),
            SheetId = sheetId,
            Name = "Sheet" + sheetId
        };
        sheets.Append(sheet);
        var headerRow = new Row();
        foreach (DataColumn column in dtPOB.Columns)
        {
            var cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) };
            headerRow.AppendChild(cell);
        }
        sheetData.AppendChild(headerRow);
        foreach (DataRow row in dtPOB.Rows)
        {
            var newRow = new Row();
            foreach (DataColumn col in dtPOB.Columns)
            {
                var cell = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(row[col].ToString())
                };
                newRow.AppendChild(cell);
            }

            sheetData.AppendChild(newRow);
        }
        workbookpart.Workbook.Save();
        spreadsheetDocument.Close();
        try
        {
            Response.ClearContent();
            using (FileStream objFileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                byte[] data1 = new byte[objFileStream.Length];
                objFileStream.Read(data1, 0, data1.Length);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", string.Format("attachment; filename ={0}", "POB Secondary.xlsx"));
                Response.BinaryWrite(data1);
            }
            FileInfo currentfile = new FileInfo(filename);
            currentfile.Delete();
        }
        catch (Exception ex)
        {
        }
        Response.End();
    }   
}