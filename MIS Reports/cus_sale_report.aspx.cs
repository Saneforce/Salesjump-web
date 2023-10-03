using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.IO;
using DBase_EReport;

public partial class MIS_Reports_cus_sale_report : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string sf_name = string.Empty;
    DataSet dsTP = null;
 protected override void OnPreInit(EventArgs e)
    {
		base.OnPreInit(e);
		sf_type = Session["sf_type"].ToString();
    	if (sf_type == "3")
		{
    		this.MasterPageFile = "~/Master.master";
    	}
    	else if(sf_type == "2")
		{
    		this.MasterPageFile = "~/Master_MGR.master";
  		}
 		else if(sf_type == "1")
    	{
    		this.MasterPageFile = "~/Master_MR.master";
	 	}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        sf_name = Session["sf_name"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillYear();
            fillsubdivision();
            FillMRManagers("0");
        }
    }
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();

            }

        }
        //ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
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
            ddlFieldForce.Items.Insert(0, new ListItem("---Select Manager ---", "0"));
        }
    }
    public DataTable generateSecondaryExcel()
    {
        string sfcode = ddlFieldForce.SelectedValue.ToString();
        string divcode = div_code;
        string years = ddlFYear.SelectedValue.ToString();
        //string[] mnths = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        //DataTable rotesDs = getsfcc("EXEC get_Route_Name_Excel '" + sfcode + "','" + divcode + "', '" + subdiv + "'");
        DataTable dsData = getsfcc("EXEC getCusDetails '" + sfcode + "','" + divcode + "', '" + subdiv + "','" + years + "'");
        //DataTable cusVstDs = getsfcc("EXEC getCuswiseVisitDets '" + sfcode + "','" + divcode + "', '" + subdiv + "','" + years + "'");
        //DataTable custOrders = getsfcc("EXEC getCuswiseYrOrder '" + sfcode + "','" + divcode + "', '" + subdiv + "','" + years + "'");

        //dsData.Columns.Add("S.No", typeof(string));
        //dsData.Columns.Add("Customer Code", typeof(string));
        //dsData.Columns.Add("Customer Name", typeof(string));
        //dsData.Columns.Add("Category", typeof(string));
        //dsData.Columns.Add("Channel", typeof(string));       
        ////dsData.Columns.Add("Phone", typeof(string));
        ////dsData.Columns.Add("Address", typeof(string));
        //dsData.Columns.Add("Route Name", typeof(string));
        //dsData.Columns.Add("HQ", typeof(string));

        //dsData.Columns.Add("Jan Visit", typeof(string));
        //dsData.Columns.Add("Feb Visit", typeof(string));
        //dsData.Columns.Add("Mar Visit", typeof(string));
        //dsData.Columns.Add("Apr Visit", typeof(string));
        //dsData.Columns.Add("May Visit", typeof(string));
        //dsData.Columns.Add("Jun Visit", typeof(string));
        //dsData.Columns.Add("Jul Visit", typeof(string));
        //dsData.Columns.Add("Aug Visit", typeof(string));
        //dsData.Columns.Add("Sep Visit", typeof(string));
        //dsData.Columns.Add("Oct Visit", typeof(string));
        //dsData.Columns.Add("Nov Visit", typeof(string));
        //dsData.Columns.Add("Dec Visit", typeof(string));

        //dsData.Columns.Add("Jan", typeof(double));        
        //dsData.Columns.Add("Feb", typeof(double));        
        //dsData.Columns.Add("Mar", typeof(double));        
        //dsData.Columns.Add("Apr", typeof(double));        
        //dsData.Columns.Add("May", typeof(double));        
        //dsData.Columns.Add("Jun", typeof(double));        
        //dsData.Columns.Add("Jul", typeof(double));        
        //dsData.Columns.Add("Aug", typeof(double));        
        //dsData.Columns.Add("Sep", typeof(double));        
        //dsData.Columns.Add("Oct", typeof(double));        
        //dsData.Columns.Add("Nov", typeof(double));        
        //dsData.Columns.Add("Dec", typeof(double));
        //dsData.Columns.Add("Total", typeof(double));
        int grantotal = 0;
        for (int i = 0; i < dsData.Rows.Count; i++)
        {
            //dsData.Rows[i]["S.No"] = i;

            grantotal += Convert.ToInt32(dsData.Rows[i]["Total"]);
            //DataRow[] drow = rotesDs.Select("Territory_Code = '" + dsData.Rows[i]["Route"].ToString() + "'");
            //if (drow.Length > 0)
            //{                
            //    dsData.Rows[i]["Route Name"] = drow[0]["Territory_Name"].ToString();
            //    dsData.Rows[i]["HQ"] = drow[0]["HQ"].ToString();
            //}
            //else
            //{
            //    dsData.Rows[i]["Route Name"] = "";
            //    dsData.Rows[i]["HQ"] = "";
            //}

            //DataRow[] monthv = cusVstDs.Select("Cust_Code ='" + dsData.Rows[i]["Outlet_Code"].ToString() + "'");

            //if(monthv.Length > 0)
            //{
            //    dsData.Rows[i]["JAN Visit"] = monthv[0]["Janv"].ToString();
            //    dsData.Rows[i]["Jan"] = monthv[0]["Jan"].ToString();
            //    dsData.Rows[i]["Febv Visit"] = monthv[0]["Febv"].ToString();
            //    dsData.Rows[i]["Feb"] = monthv[0]["Feb"].ToString();
            //    dsData.Rows[i]["Mar Visit"] = monthv[0]["Marv"].ToString();
            //    dsData.Rows[i]["Mar"] = monthv[0]["Mar"].ToString();
            //    dsData.Rows[i]["Apr Visit"] = monthv[0]["Aprv"].ToString();
            //    dsData.Rows[i]["Apr"] = monthv[0]["Apr"].ToString();
            //    dsData.Rows[i]["May Visit"] = monthv[0]["Mayv"].ToString();
            //    dsData.Rows[i]["May"] = monthv[0]["May"].ToString();
            //    dsData.Rows[i]["Jun Visit"] = monthv[0]["Junv"].ToString();
            //    dsData.Rows[i]["Jun"] = monthv[0]["Jun"].ToString();
            //    dsData.Rows[i]["Jul Visit"] = monthv[0]["Julv"].ToString();
            //    dsData.Rows[i]["Jul"] = monthv[0]["Jul"].ToString();
            //    dsData.Rows[i]["Jul Visit"] = monthv[0]["Julv"].ToString();
            //    dsData.Rows[i]["Jul"] = monthv[0]["Jul"].ToString();
            //}

            //for (int j = 0; j < cusVstDs.Rows.Count; j++)
            //{
            //    double mnthO = 0;
            //    string mnthV = "";

            //    DataRow[] monthv = cusVstDs.Select("Cust_Code ='" + dsData.Rows[i]["Outlet_Code"].ToString() + "'");


            //    //mnthV = (cusVstDs.Select("Trans_Detail_Info_Code='" + dsData.Rows[i]["Outlet_Code"].ToString() + "'")))
            //    DataRow[] dvsts = custOrders.Select("Cust_Code ='" + dsData.Rows[i]["Outlet_Code"].ToString() + "'");
            //    mnthO = dvsts.Length > 0 ? Convert.ToDouble(dvsts[0]["ord_val"]) : 0;

            //    dsData.Rows[i][mnths[j] + " Visit"] = mnthV;
            //    dsData.Rows[i][mnths[j]] = mnthO;
            //    dsData.Rows[i]["TOTAL"] = total;
            //}

        }
        return dsData;
    }

    protected void exceldld_Click(object sender, EventArgs e)
    {
        DataTable ot = generateSecondaryExcel();
        string filename = System.IO.Path.GetTempPath() + "Customer_Sales_Analysis" + div_code + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";
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
                Response.AddHeader("content-disposition", string.Format("attachment; filename ={0}", "Customer_Sales_Analysis.xlsx"));
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
    public static DataTable getsfcc(string qrystring)
    {

        DB_EReporting db_ER = new DB_EReporting();

        DataTable dsSF = null;

        string strQry = qrystring;

        try
        {
            dsSF = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }
}