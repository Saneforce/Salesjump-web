using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;



public partial class MIS_Reports_CallCompliance : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    
    DateTime ServerStartTime;
    DataSet dsSalesForce = null;

    public static string Div = string.Empty;
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string subdivcode = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public DateTime rdt;
    public DateTime sdt;

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
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
            Div = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
            Div = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            fillsubdivision();
            FillMRManagers("0");

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
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceList(div_code, sf_code, Sub_Div_Code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, "---Select Field Force---");
        }
    }

     protected void exceldld_Click(object sender, EventArgs e)
    {
        div_code = div_code.ToString();
        sfcode = ddlFieldForce.SelectedValue.ToString();
        subdivcode = subdiv.SelectedValue.ToString();

        if (sfcode == "" || sfcode == null)
        {
            sfcode = "admin";
        }

        if (subdivcode == "" || subdivcode == null)
        {
            subdivcode = "";
        }

        FDT = txtfdatee.Text.ToString();
        TDT = txttdatee.Text.ToString();
        

        //string FMonth = ddlMonth.SelectedValue.ToString();
        //string FYear = ddlYear.SelectedValue.ToString();
        string sf_name = ddlFieldForce.SelectedItem.ToString();

       
        DataTable ot = generateRetailerCloseingExcel(sfcode, Div, FDT, TDT, subdivcode);
        string filename = System.IO.Path.GetTempPath() + "Call_Compliance_Report_" + div_code + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";

        if (File.Exists(filename))
        {
            filename = System.IO.Path.GetTempPath() + "Call_Compliance_Report_" + div_code + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";
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
}