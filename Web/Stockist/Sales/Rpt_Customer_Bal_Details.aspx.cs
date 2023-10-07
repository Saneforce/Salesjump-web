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

public partial class Stockist_Sales_Rpt_Customer_Bal_Details : System.Web.UI.Page
{

    public static string Cust_Code = string.Empty;
    public static string Cust_Name = string.Empty;
    public static string From_Year = string.Empty;
    public static string To_Year = string.Empty;
    public static string From_Month = string.Empty;
    public static string To_Month = string.Empty;
    public static string Typee = string.Empty;

    public static DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        Cust_Code = Request.QueryString["Cust_Code"].ToString();
        Cust_Name = Request.QueryString["Cust_Name"].ToString();
        From_Year = Request.QueryString["From_Year"].ToString();
        To_Year = Request.QueryString["To_Year"].ToString();
        From_Month = Request.QueryString["From_Month"].ToString();
        To_Month = Request.QueryString["To_month"].ToString();
        Typee = Request.QueryString["Tpe"].ToString(); 

        Tit.Text = "Customer Balance Details for -" + Cust_Name;
    }
    [WebMethod(EnableSession = true)]
    public static string Get_cust_bal_details()
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        //Stockist_Sales ss = new Stockist_Sales();
        //ds = ss.Bind_Cust_Bal_details(Cust_Code, Div_Code, From_Year, To_Year, From_Month, To_Month,Typee);
        ds = Bind_Cust_Bal_details(Cust_Code, Div_Code, From_Year, To_Year, From_Month, To_Month,Typee);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Bind_Cust_Bal_details(string Cust_Code, string Div_Code, string From_Year, string To_Year, string From_Month, string To_Month, string Typee)
    {

        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "EXEC sp_cust_bal_details '" + Cust_Code + "','" + Div_Code + "','" + From_Year + "','" + To_Year + "','" + From_Month + "','" + To_Month + "','" + Typee + "'";
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

    [WebMethod(EnableSession = true)]
    public static List<ListItem> bindretailer()
    {
        string Sf_Type = HttpContext.Current.Session["sf_type"].ToString();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string StockistCode = HttpContext.Current.Session["Sf_Code"].ToString();
        StockistMaster sm = new StockistMaster();
        List<ListItem> retailer = new List<ListItem>();
        DataSet ds = new DataSet();
        //ds = sm.getretailerdetails(StockistCode, Div_code.TrimEnd(','), Sf_Type);
        ds = getretailerdetails(StockistCode, Div_code.TrimEnd(','), Sf_Type);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            retailer.Add(new ListItem
            {
                Value = row["Code"].ToString(),
                Text = row["Name"].ToString(),

            });

        }
        return retailer;
    }
    public static  DataSet getretailerdetails(string scode, string divcode, string Sf_Type)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;
        try
        {
            // strQry = "select msd.ListedDr_Name,msd.ListedDrCode from Trans_Order_Head toh join mas_listeddr msd on toh.Cust_Code = msd.ListedDrCode where toh.Stockist_Code = '" + scode + "' and toh.Order_Flag = '0' and toh.Div_ID = '" + divcode + "' and Order_Value > 0  group by ListedDr_Name,ListedDrCode order by ListedDr_Name";
            string strQry = "EXEC Bind_sales_details '" + Sf_Type + "','" + divcode + "','" + scode + "'";
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

        decimal CollectedAmt = 0;
        decimal TotalCollectedAmt1 = 0;
        decimal TotalCollectedAmt = 0;

        decimal Balance = 0;
        decimal Balance1 = 0;
        decimal TotalBalance = 0;
		
		
		try
		{

        ds.Tables[0].Columns.Remove("Invoice_Date");
        ds.Tables[0].Columns.Remove("adv_amt");
        ds.Tables[0].Columns.Remove("adjust_amt");
        ds.Tables[0].Columns.Remove("Cus_Code");
        ds.Tables[0].Columns.Remove("Cus_Name");
        ds.Tables[0].Columns.Remove("Sf_Code");
        ds.Tables[0].Columns.Remove("Dis_Code");
        ds.Tables[0].Columns.Remove("Dis_Name");
		ds.Tables[0].Columns.Remove("dat1");
        ds.Tables[0].Columns.Remove("trans_inv_no");

        ds.Tables[0].Columns["Dat"].ColumnName = "Date";
        ds.Tables[0].Columns["Inv_No"].ColumnName = "Invoice No";
        ds.Tables[0].Columns["sts"].ColumnName = "Status";
        ds.Tables[0].Columns["Total_Bill_Value"].ColumnName = "Amount";
        ds.Tables[0].Columns["Coll_Amt"].ColumnName = "Collected Amt";
        ds.Tables[0].Columns["Bal_Amt"].ColumnName = "Balance Due";

        ds.Tables[0].Columns["Date"].SetOrdinal(0);
        ds.Tables[0].Columns["Invoice No"].SetOrdinal(1);
        ds.Tables[0].Columns["Type"].SetOrdinal(2);
        ds.Tables[0].Columns["Status"].SetOrdinal(3);
        ds.Tables[0].Columns["Amount"].SetOrdinal(4);
        ds.Tables[0].Columns["Collected Amt"].SetOrdinal(5);
        ds.Tables[0].Columns["Balance Due"].SetOrdinal(6);
		}
		catch(Exception ex)
		{
			 Response.Write(ex.Message);
		}       
        using (XLWorkbook wb = new XLWorkbook())
        {
            var ws = wb.Worksheets.Add("Customer Balance");
            var wsReportNameHeaderRange = ws.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(aCode + ds.Tables[0].Columns.Count - b)));
            wsReportNameHeaderRange.Style.Font.Bold = true;
            wsReportNameHeaderRange.Style.Font.FontSize = 20;
            wsReportNameHeaderRange.Merge();
            wsReportNameHeaderRange.Value = "Customer Balance Details for - "+Cust_Name ;
            wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);           
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
                CollectedAmt = Convert.ToDecimal(row["Collected Amt"]) + Convert.ToDecimal(CollectedAmt);
                Balance = Convert.ToDecimal(row["Balance Due"]) + Convert.ToDecimal(Balance);

            }

            TotalAmount = Amount - TotalAmount1;
            TotalCollectedAmt = CollectedAmt - TotalCollectedAmt1;
            TotalBalance = Balance - Balance1;
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

            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Value = TotalAmount;
            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Font.Bold = true;
            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Columns().AdjustToContents();

            valueCount1++;
           
            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Value = TotalCollectedAmt;
            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Font.Bold = true;
            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Columns().AdjustToContents();

            valueCount1++;

            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Value = TotalBalance;
            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Font.Bold = true;
            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Columns().AdjustToContents();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Customer Balance.xlsx");
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