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
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using DBase_EReport;

public partial class MIS_Reports_Secondary_Order_Reports : System.Web.UI.Page
{
    public string sf_type = string.Empty;
    string div_code = string.Empty;
    public static string sf_code = string.Empty;
    public static string sub_division = string.Empty;
    protected void Page_PreInit(object sender, EventArgs e)
    {
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
        else if (sf_type == "4")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString().TrimEnd(',');
        sub_division = Session["sub_division"].ToString();
    }
    [WebMethod]
    public static string getStates(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet ds = SFD.getsubdiv_States(divcode, sf_code, sub_division);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getDivision(string divcode)
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.Getsubdivisionwise(divcode, sub_division);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getFieldForce(string divcode, string sfcode)
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.SalesForceList(divcode, sfcode, sub_division);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public DataTable generateSecondaryExcel(string divcode, string Sf_Code, string FDate, string TDate, string subdiv_code, string statecode)
    {
        DCR dc = new DCR();
		 loc dc1 = new loc();
        DataSet ss = dc.secordernewqty(divcode, Sf_Code, FDate, TDate, subdiv_code, statecode);
        DataSet dsGV = dc.secneworderview(divcode, Sf_Code, FDate, TDate, subdiv_code, statecode);
        DataTable dtOrderHead = new DataTable();
        DataTable dtOrderDetails = new DataTable();
        DataTable dtOrders = new DataTable();
        dtOrders.Columns.Add("Order Date", typeof(string));
        dtOrders.Columns.Add("Order Type", typeof(string));
        dtOrders.Columns.Add("Distributor Name", typeof(string));
        dtOrders.Columns.Add("ERP Code", typeof(string));
        dtOrders.Columns.Add("Order taken by", typeof(string));
        dtOrders.Columns.Add("Retailer Name", typeof(string));
        dtOrders.Columns.Add("Channel", typeof(string));
        dtOrders.Columns.Add("Route", typeof(string));

        var ProductNames = (from row in ss.Tables[0].AsEnumerable()
                            orderby row.Field<string>("Product_Detail_Name")
                            select new
                            {
                                Product_Code = row.Field<string>("Product_Code"),
                                Product_Name = row.Field<string>("Product_Detail_Name")
                            }).Distinct().ToList();

        foreach (var str in ProductNames)
        {
            dtOrders.Columns.Add(str.Product_Name.ToString(), typeof(double));
        }
        dtOrders.Columns.Add("Net Weight Value", typeof(double));
        dtOrders.Columns.Add("Order Value", typeof(double));
        foreach (DataRow dr in dsGV.Tables[0].Rows)
        {
            DataRow rw = dtOrders.NewRow();
            string transSlNO = dr["Trans_Sl_No"].ToString();
            rw["Order Date"] = dr["OrderDate"].ToString();
            rw["Order Type"] = dr["OrderType"].ToString();
            rw["Distributor Name"] = dr["Stockist_name"].ToString();
            rw["ERP Code"] = dr["ERP_Code"].ToString();
            rw["Order taken by"] = dr["SF_Name"].ToString();
            rw["Retailer Name"] = dr["retailername"].ToString();
            rw["Channel"] = dr["channel"].ToString();
            rw["Route"] = dr["routename"].ToString();
            rw["Net Weight Value"] = (dr["net_weight_value"]).Equals(DBNull.Value) ? 0 : Convert.ToDouble(dr["net_weight_value"]);
            rw["Order Value"] = (dr["Order_value"]).Equals(DBNull.Value) ? 0 : Convert.ToDouble(dr["Order_value"]);
            DataRow[] drp = ss.Tables[0].Select("Trans_Sl_No='" + transSlNO + "'");
            for (int i = 0; i < drp.Length; i++)
            {
                rw[(drp[i]["Product_Detail_Name"].ToString())] =
                    Convert.ToDouble((string.IsNullOrEmpty(Convert.ToString(rw[drp[i]["Product_Detail_Name"].ToString()]))) ? 0 : rw[(drp[i]["Product_Detail_Name"].ToString())]) + Convert.ToDouble(drp[i]["Quantity"]);
            }
            dtOrders.Rows.Add(rw);
        }
        return dtOrders;
    }

    protected void exceldld_Click(object sender, EventArgs e)
    {
        DataTable ot = generateSecondaryExcel(div_code, hsf.Value.ToString(), hfdt.Value.ToString(), htdt.Value.ToString(), hsubdiv.Value.ToString(), hstt.Value.ToString());
        string filename = System.IO.Path.GetTempPath() + "Secondary Order Report_" + div_code + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";
        if (File.Exists(filename))
        {
            filename = System.IO.Path.GetTempPath() + "Secondary Order Report_" + div_code + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";
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

                var cell = new Cell();
                if (col.ColumnName == "Order Value")
                {
                    cell = new Cell
                    {
                        DataType = CellValues.Number,
                        CellValue = new CellValue(row[col].ToString())
                    };
                }
                else
                {
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(row[col].ToString())
                    };
                }
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
                Response.AddHeader("content-disposition", string.Format("attachment; filename ={0}", "Secondary Order Report.xlsx"));
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
	
	public class loc
    {
        public DataSet secordernewqty(string div_code, string sfcode, string fdate, string tdate, string subdiv = "0", string statecode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;

            string strQry = "select * from detailsapr";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet secneworderview(string div_code, string sfcode, string fdate, string tdate, string subdiv = "0", string statecode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            string strQry = "select * from HeadApr";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
    }
}