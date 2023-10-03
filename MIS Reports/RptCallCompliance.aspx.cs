using System;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

public partial class MIS_Reports_RptCallCompliance : System.Web.UI.Page
{
    public static string Div = string.Empty;
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public DateTime rdt;
    public DateTime sdt;
    protected void Page_Load(object sender, EventArgs e)
    {
        Div = Session["div_code"].ToString();
        sfcode = Request.QueryString["SF_Code"].ToString();
        FDT = Request.QueryString["Fdate"].ToString();
        TDT = Request.QueryString["Tdate"].ToString();
        subdiv = Request.QueryString["Sub_Div"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        lblsf_name.Text = sfname;
		
		if (!IsPostBack)
        {
            BindGidData();
        }
    }
	
	
	 public void BindGidData()
    {
        DCR dc = new DCR();
        DataTable dt = new DataTable();
        dt = dc.getDataTable("exec getCallComplianceReport '" + sfcode + "','" + Div + "','" + FDT + "','" + TDT + "','" + subdiv + "'");
        if(dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGidData();
    }
	
    [WebMethod]
    public static string getDetails(string Div)
    {
        DCR dc = new DCR();
        DataTable dt = new DataTable();
        dt = dc.getDataTable("exec getCallComplianceReport '" + sfcode + "','" + Div + "','" + FDT + "','" + TDT + "','" + subdiv + "'");
        return JsonConvert.SerializeObject(dt);
    }
	
	 private DataTable generateRetailerCloseingExcel(string sfcode, string Div, string FDT, string TDT, string subdiv)
    {
        DataTable dtOrders = new DataTable();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();
        DataTable dt = new DataTable();
        dt = dc.getDataTable("exec getCallComplianceReport '" + sfcode + "','" + Div + "','" + FDT + "','" + TDT + "','" + subdiv + "'");
        string stCrtDtaPnt = string.Empty;

        dtOrders.Columns.Add("SlNo.", typeof(string));
        dtOrders.Columns.Add("Date", typeof(string));

        dtOrders.Columns.Add("SF Code", typeof(string));
        dtOrders.Columns.Add("SF Name", typeof(string));

        dtOrders.Columns.Add("Territory", typeof(string));
        dtOrders.Columns.Add("Route Code", typeof(string));
        dtOrders.Columns.Add("Route Name", typeof(string));
        dtOrders.Columns.Add("Retailer Code", typeof(string));
        dtOrders.Columns.Add("Retailer Name", typeof(string));

        dtOrders.Columns.Add("Distributor", typeof(string));
        dtOrders.Columns.Add("Mobile", typeof(string));
        dtOrders.Columns.Add("Class", typeof(string));
        dtOrders.Columns.Add("Channel", typeof(string));

        dtOrders.Columns.Add("Start Time", typeof(string));
        dtOrders.Columns.Add("End Time", typeof(string));
        dtOrders.Columns.Add("In-Store Time", typeof(string));

        dtOrders.Columns.Add("Submitted Lat", typeof(string));
        dtOrders.Columns.Add("Submitted Long", typeof(string));

        dtOrders.Columns.Add("Actual Lat", typeof(string));
        dtOrders.Columns.Add("Actual Long", typeof(string));

        dtOrders.Columns.Add("Distance", typeof(string));

        dtOrders.Columns.Add("Order Value", typeof(string));

        dtOrders.Columns.Add("Reason", typeof(string));


        int i = 1;
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                DataRow rw = dtOrders.NewRow();
                //string transSlNO = dr["SlNo"].ToString();
                rw["SlNo."] = i.ToString();
                rw["Date"] = dr["Activity_Date"].ToString();
                rw["SF Code"] = dr["Sf_Code"].ToString();
                rw["SF Name"] = dr["SF_Name"].ToString();

                rw["Territory"] = dr["Territory"].ToString();
                rw["Route Code"] = dr["RouteCode"].ToString();
                rw["Route Name"] = dr["RouteName"].ToString();

                rw["Retailer Code"] = dr["RetailerCode"].ToString();
                rw["Retailer Name"] = dr["RetailerName"].ToString();

                rw["Distributor"] = dr["DistributorName"].ToString();
                rw["Mobile"] = dr["Mobile"].ToString();
                rw["Class"] = dr["Class"].ToString();
                rw["Channel"] = dr["Channel"].ToString();

                rw["Start Time"] = dr["StartTime"].ToString();
                rw["End Time"] = dr["EndTime"].ToString();
                rw["In-Store Time"] = dr["InStoreTime"].ToString();

                rw["Submitted Lat"] = dr["Call_lat"].ToString();
                rw["Submitted Long"] = dr["Call_lng"].ToString();

                rw["Actual Lat"] = dr["Retlat"].ToString();
                rw["Actual Long"] = dr["RetLng"].ToString();

                rw["Distance"] = dr["Distance"].ToString();

                rw["Order Value"] = (dr["OrderValue"]).Equals(DBNull.Value) ? 0 : Convert.ToDouble(dr["OrderValue"]);

                rw["Reason"] = dr["Remarks"].ToString();

                dtOrders.Rows.Add(rw);
                i++;
            }
        }


        return dtOrders;
    }
	
	protected void btnimgbuttonExcel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        DataTable ot = generateRetailerCloseingExcel(sfcode, Div, FDT, TDT, subdiv);
        string filename = System.IO.Path.GetTempPath() + "Call_Compliance_Report_" + Div + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";

        if (File.Exists(filename))
        {
            filename = System.IO.Path.GetTempPath() + "Call_Compliance_Report_" + Div + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";
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
        foreach (DataColumn column in ot.Columns)
        {
            var cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) };
            headerRow.AppendChild(cell);
        }
        sheetData.AppendChild(headerRow);
        foreach (DataRow row in ot.Rows)
        {
            var newRow = new Row();
            foreach (DataColumn col in ot.Columns)
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
                Response.AddHeader("content-disposition", string.Format("attachment; filename ={0}", "Call_Compliance_Report_" + DateTime.Now + ".xlsx"));
                //Response.AddHeader("content-disposition", string.Format("attachment; filename =" + filename.ToString() + ""));
                Response.BinaryWrite(data1);
            }
            FileInfo currentfile = new FileInfo(filename);
            currentfile.Delete();
        }
        catch (Exception ex)
        {
        }
        Response.End();



        //DCR dc = new DCR();
        //DataTable dt = new DataTable();
        //dt = dc.getDataTable("exec getCallComplianceReport '" + sfcode + "','" + Div + "','" + FDT + "','" + TDT + "','" + subdiv + "'");

        //try
        //{
            
        //    string attachment = "attachment; filename=Call_Compliance_" + DateTime.Now + ".xls";
        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", attachment);
        //    Response.ContentType = "application/vnd.ms-excel";
        //    string tab = "";
        //    foreach (DataColumn dcn in dt.Columns)
        //    {
        //        Response.Write(tab + dcn.ColumnName);
        //        tab = "\t";
        //    }
        //    Response.Write("\n");
        //    int i;
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        tab = "";
        //        for (i = 0; i < dt.Columns.Count; i++)
        //        {
        //            Response.Write(tab + dr[i].ToString());
        //            tab = "\t";
        //        }
        //        Response.Write("\n");
        //    }
        //    Response.End();
        //}
        //catch (Exception Ex)
        //{ }
        
    }
}