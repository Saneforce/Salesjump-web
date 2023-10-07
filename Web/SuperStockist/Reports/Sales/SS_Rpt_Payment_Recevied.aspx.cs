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
using System.IO;
using ClosedXML.Excel;
using DBase_EReport;

public partial class SuperStockist_Reports_Sales_SS_Rpt_Payment_Recevied : System.Web.UI.Page
{
    public static DataSet ds = new DataSet();
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public static string fd = string.Empty;
    public static string td = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod(EnableSession = true)]
    public static string Get_Payment_received(string FDT, string TDT)
    {
        DateTime dt = Convert.ToDateTime(FDT);
        fd = dt.ToString("dd/MM/yyyy");
        DateTime dt1 = Convert.ToDateTime(TDT);
        td = dt1.ToString("dd/MM/yyyy");
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        //Stockist_Sales ss = new Stockist_Sales();
        //DataSet ds = ss.Bind_Payment_received_details(Stockist_Code, FDT, TDT, Div_Code);
        ds = Bind_Payment_received_details(Stockist_Code, FDT, TDT, Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Bind_Payment_received_details(string Stockist_Code, string FDT, string TDT, string Div_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "EXEC sp_bind_SS_payment_received_details '" + Stockist_Code + "','" + FDT + "','" + TDT + "','" + Div_Code + "'";
       // string strQry = "EXEC sp_bind_payment_received_details '" + Stockist_Code + "','" + FDT + "','" + TDT + "','" + Div_Code + "'";
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
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        var aCode = 65;
        var b = 1;

        decimal Amount = 0;
        decimal TotalAmount1 = 0;
        decimal TotalAmount = 0;

        //if( ds.Tables[0].Columns.count >0){
        try
        {



            //ds.Tables[0].Columns.Remove("Pay_Date");
            ds.Tables[0].Columns.Remove("Sf_Code");
            ds.Tables[0].Columns.Remove("Sf_Name");
            ds.Tables[0].Columns.Remove("Cust_Id");
            //ds.Tables[0].Columns.Remove("Pay_Ref_No");
            ds.Tables[0].Columns.Remove("Distributor_Code");
            ds.Tables[0].Columns.Remove("PaymentName");
            ds.Tables[0].Columns.Remove("advance_pay");
            ds.Tables[0].Columns.Remove("div_code");
            //ds.Tables[0].Columns.Remove("Column1");
            ds.Tables[0].Columns.Remove("da");
            ds.Tables[0].Columns.Remove("dat1");
            ds.Tables[0].Columns.Remove("invoice_no");





            ds.Tables[0].Columns["dat"].ColumnName = "Date";
            ds.Tables[0].Columns["Cus_Name"].ColumnName = "Customer Name";
            ds.Tables[0].Columns["Pay_Mode"].ColumnName = "Payment Mode";
            ds.Tables[0].Columns["Pay_Ref_No"].ColumnName = "Reference No";
            ds.Tables[0].Columns["type"].ColumnName = "Type";


            ds.Tables[0].Columns["Date"].SetOrdinal(0);
            ds.Tables[0].Columns["Customer Name"].SetOrdinal(1);
            ds.Tables[0].Columns["Payment Mode"].SetOrdinal(2);
            ds.Tables[0].Columns["Reference No"].SetOrdinal(3);
            ds.Tables[0].Columns["Type"].SetOrdinal(4);
            ds.Tables[0].Columns["Amount"].SetOrdinal(5);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

        using (XLWorkbook wb = new XLWorkbook())
        {
            var ws = wb.Worksheets.Add("Payment Received Details");
            var wsReportNameHeaderRange = ws.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(aCode + ds.Tables[0].Columns.Count - b)));
            wsReportNameHeaderRange.Style.Font.Bold = true;
            wsReportNameHeaderRange.Style.Font.FontSize = 20;
            wsReportNameHeaderRange.Merge();
            wsReportNameHeaderRange.Value = "Payment Received Details ";
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

                Amount = Convert.ToDecimal(row["Amount"]) + Convert.ToDecimal(Amount);

            }

            TotalAmount = Amount - TotalAmount1;

            int valueCount1 = 0;

            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Value = "Total";
            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Font.Bold = true;
            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Columns().AdjustToContents();

            valueCount1++;

            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Value = "";
            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Font.Bold = true;
            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Columns().AdjustToContents();
            valueCount1++;

            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Value = "";
            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Columns().AdjustToContents();

            valueCount1++;

            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Value = "";
            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Columns().AdjustToContents();


            valueCount1++;

            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Value = "";
            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Columns().AdjustToContents();

            valueCount1++;


            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Value = TotalAmount;
            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Font.Bold = true;
            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Columns().AdjustToContents();


            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Payment Received Details.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
        //}
    }
}