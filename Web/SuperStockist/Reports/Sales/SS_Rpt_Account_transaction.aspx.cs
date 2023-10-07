using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Globalization;
using Newtonsoft.Json;
using ClosedXML.Excel;
using System.IO;
using DBase_EReport;

public partial class SuperStockist_Reports_Sales_SS_Rpt_Account_transaction : System.Web.UI.Page
{
    public static DataSet ds = new DataSet();
    public static string fd = string.Empty;
    public static string td = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod(EnableSession = true)]
    public static string Get_Acc_trans_details(string FDT, string TDT)
    {
        DateTime dt = Convert.ToDateTime(FDT);
        fd = dt.ToString("dd/MM/yyyy");
        DateTime dt1 = Convert.ToDateTime(TDT);
        td = dt1.ToString("dd/MM/yyyy");
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        // DataSet ds = new DataSet();

        //Stockist_Sales ss = new Stockist_Sales();
        //ds = ss.Bind_ac_trans_details(Stockist_Code, FDT, TDT, Div_Code);
        ds = Bind_ac_trans_details(Stockist_Code, FDT, TDT, Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Bind_ac_trans_details(string Stockist_Code, string FDT, string TDT, string Div_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "EXEC sp_SS_account_transaction '" + Stockist_Code + "','" + Div_Code + "','" + FDT + "','" + TDT + "'";
       // string strQry = "EXEC sp_account_transaction '" + Stockist_Code + "','" + Div_Code + "','" + FDT + "','" + TDT + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    [WebMethod]
    public static string bindretailer(string stk, string Div)
    {

        List<ListItem> retailer = new List<ListItem>();
        DataSet ds = new DataSet();
        //StockistMaster sm = new StockistMaster();
        //ds = sm.getretailerdetailsmyorder(stk, Div.TrimEnd(','));
        ds = getretailerdetailsmyorder(stk, Div.TrimEnd(','));
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getretailerdetailsmyorder(string scode, string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        try
        {
            string strQry = "EXEC spGetSS_distributorsName '" + scode + "','" + divcode + "'";
            //string strQry = "EXEC spGetRetailerName '" + scode + "','" + divcode + "'";
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
    {
        var aCode = 65;
        var b = 1;
        try
        {

            ds.Tables[0].Columns.Remove("Order_Date");
            ds.Tables[0].Columns.Remove("dat1");

            ds.Tables[0].Columns["dat"].ColumnName = "Date";
            ds.Tables[0].Columns["No"].ColumnName = "Trans No";

            ds.Tables[0].Columns["Date"].SetOrdinal(0);
            ds.Tables[0].Columns["Name"].SetOrdinal(1);
            ds.Tables[0].Columns["Type"].SetOrdinal(2);
            ds.Tables[0].Columns["Trans No"].SetOrdinal(3);
            ds.Tables[0].Columns["Reference"].SetOrdinal(4);
            ds.Tables[0].Columns["Debit"].SetOrdinal(5);
            ds.Tables[0].Columns["Credit"].SetOrdinal(6);
            ds.Tables[0].Columns["Amount"].SetOrdinal(7);

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        using (XLWorkbook wb = new XLWorkbook())
        {
            var ws = wb.Worksheets.Add("Account Transaction Details");
            //ws.Tables.FirstOrDefault().ShowAutoFilter = false;
            var wsReportNameHeaderRange = ws.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(aCode + ds.Tables[0].Columns.Count - b)));
            wsReportNameHeaderRange.Style.Font.Bold = true;
            wsReportNameHeaderRange.Style.Font.FontSize = 20;
            wsReportNameHeaderRange.Merge();
            wsReportNameHeaderRange.Value = "Account Transaction Details";
            wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            var wsReportDateRange = ws.Range(string.Format("A{0}:{1}{0}", 2, Char.ConvertFromUtf32(aCode + ds.Tables[0].Columns.Count - b)));
            wsReportDateRange.Style.Font.Bold = true;
            wsReportDateRange.Style.Font.FontSize = 15;
            wsReportDateRange.Merge();
            wsReportDateRange.Value = "From :" + fd + " To " + td + "";
            wsReportDateRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            int rowIndex = 3;
            int columnIndex = 0;
            foreach (DataColumn column in ds.Tables[0].Columns)
            {

                ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Value = column.ColumnName;
                //ws.Cell(string.Format("A1"))
                ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Font.Bold = true;
                ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Font.FontColor = XLColor.White;
                ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
                columnIndex++;
            }
            rowIndex++;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int valueCount = 0;
                foreach (object rowValue in row.ItemArray)
                {
                    ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Value = rowValue;
                    ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Columns().AdjustToContents();
                    valueCount++;
                }
                rowIndex++;
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Account Transaction Details.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {

                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
}