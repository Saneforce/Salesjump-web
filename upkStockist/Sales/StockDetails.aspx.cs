using Bus_EReport;
using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Stockist_Sales_StockDetails : System.Web.UI.Page
{
    public static string fd = string.Empty;
    public static string td = string.Empty;
    public static DataSet ds = new DataSet();
	public static string sf_type = string.Empty;
	
	protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3" || sf_type == "2" || sf_type == "1")
        {
            this.MasterPageFile = "~/MasterForAll.master";
        }        
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    public static string Get_Stock(string Date,string dist_code)
    {
        //DataSet ds = new DataSet();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        //string stk_Code = HttpContext.Current.Session["sf_code"].ToString();
        StockistMaster sm = new StockistMaster();
       //ds = sm.get_Product_stock(Date, div_code, stk_Code);
        ds = get_Product_stock(Date, div_code, dist_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public  static DataSet get_Product_stock(string Date, string div_code, string stk_Code)
    {
        string strQry = string.Empty;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();
        DataSet ds = null;

        //strQry = "Exec get_stock_details_1 '" + Date + "','" + div_code + "','" + stk_Code + "','" + sf_type + "'";
        //strQry = "Exec get_stock_details '" + Date + "','" + div_code + "','" + stk_Code  + "'";
        strQry = "Exec getDistStockLedger '" + stk_Code + "','" + Date + "','" + Date + "'";

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
    public static string binddistributor(string sf_code, string Div)
    {
        DataSet ds = new DataSet();
        ds = getDistributordetails(sf_code, Div.TrimEnd(','));
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet getDistributordetails(string sf_code, string Div_Code)
    {
        string strQry = string.Empty;
        DataSet ds = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "select Stockist_Code,Stockist_Name,ERP_Code from mas_stockist ms inner join Customer_Hierarchy_Details ch on ch.SF_Code = '" + sf_code + "' and ch.Dist_Code = ms.stockist_code where Division_Code = '" + Div_Code + "' order by Stockist_Name";

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
        //try
        //{

        //    var aCode = 65;
        //    var b = 1;
        //    double In = 0;
        //    double InTotal1 = 0;
        //    double TotalInStock = 0;
        //    double Out = 0;
        //    double OutTotal = 0;
        //    double OutTotalStock = 0;

        //    try
        //    {

        //        ds.Tables[0].Columns.Remove("Product_Description");

        //        ds.Tables[0].Columns["Product_Detail_Code"].ColumnName = "Product Code";
        //        ds.Tables[0].Columns["Product_Detail_Name"].ColumnName = "Product Name";
        //        ds.Tables[0].Columns["IN_Stock"].ColumnName = "In(p)";
        //        ds.Tables[0].Columns["OUT_Stock"].ColumnName = "Out(p)";

        //        ds.Tables[0].Columns["Product Code"].SetOrdinal(0);
        //        ds.Tables[0].Columns["Product Name"].SetOrdinal(1);
        //        ds.Tables[0].Columns["In(p)"].SetOrdinal(2);
        //        ds.Tables[0].Columns["Out(p)"].SetOrdinal(3);

        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.Message);
        //    }



        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        var ws = wb.Worksheets.Add(ds.Tables[0], "Stock Details");
        //        var wsReportNameHeaderRange = ws.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(aCode + ds.Tables[0].Columns.Count - b)));
        //        wsReportNameHeaderRange.Style.Font.Bold = true;
        //        wsReportNameHeaderRange.Style.Font.FontSize = 20;
        //        wsReportNameHeaderRange.Merge();
        //        wsReportNameHeaderRange.Value = "ProductWise Stock Details";
        //        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //        var wsReportDateRange = ws.Range(string.Format("A{0}:{1}{0}", 2, Char.ConvertFromUtf32(aCode + ds.Tables[0].Columns.Count - b)));
        //        wsReportDateRange.Style.Font.Bold = true;
        //        wsReportDateRange.Style.Font.FontSize = 15;
        //        wsReportDateRange.Merge();
        //        wsReportDateRange.Value = "From :" + fd + " To " + td + "";
        //        wsReportDateRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //        int rowIndex = 3;
        //        int columnIndex = 0;
        //        foreach (DataColumn column in ds.Tables[0].Columns)
        //        {

        //            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Value = column.ColumnName;
        //            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Font.Bold = true;
        //            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Font.FontColor = XLColor.White;
        //            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
        //            columnIndex++;
        //        }
        //        rowIndex++;


        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {

        //            In = Convert.ToDouble(row["In(p)"]) + Convert.ToDouble(In);
        //            Out = Convert.ToDouble(row["Out(p)"]) + Convert.ToDouble(Out);
        //            int valueCount = 0;
        //            foreach (object rowValue in row.ItemArray)
        //            {
        //                ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Value = rowValue;
        //                ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //                ws.Columns().AdjustToContents();
        //                valueCount++;
        //            }
        //            rowIndex++;
        //        }
        //        TotalInStock = In - InTotal1;
        //        OutTotalStock = Out - OutTotal;
        //        int valueCount1 = 0;

        //        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Value = "Total";
        //        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Font.Bold = true;
        //        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        ws.Columns().AdjustToContents();

        //        valueCount1++;

        //        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Value = "";
        //        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        ws.Columns().AdjustToContents();

        //        valueCount1++;

        //        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Value = TotalInStock;
        //        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Font.Bold = true;
        //        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        ws.Columns().AdjustToContents();

        //        valueCount1++;

        //        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Value = OutTotalStock;
        //        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Font.Bold = true;
        //        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount1), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        ws.Columns().AdjustToContents();

        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.Charset = "";
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", "attachment;filename=ProductWise Stock Details.xlsx");
        //        using (MemoryStream MyMemoryStream = new MemoryStream())
        //        {
        //            wb.SaveAs(MyMemoryStream);
        //            MyMemoryStream.WriteTo(Response.OutputStream);
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex.Message);
        //}

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(ds.Tables[0], "Sales By Customer Details");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.AddHeader("content-disposition", "attachment;filename=Permission Status Report" + fdt + "_to_" + tdt + ".xlsx");
            Response.AddHeader("content-disposition", "attachment;filename=Sales By Customer Details.xlsx");
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