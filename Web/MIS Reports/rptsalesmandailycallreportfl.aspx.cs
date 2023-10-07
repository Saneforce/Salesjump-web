
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Bus_EReport;
using System.Web.UI.HtmlControls;
using System.Drawing;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Rectangle = iTextSharp.text.Rectangle;
using iTextSharp.tool.xml;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;


public partial class MIS_Reports_rptsalesmandailycallreportfl : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string Year = string.Empty;
    string tdate = string.Empty;
    string date = string.Empty;
    string subdiv_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string tot_dr = string.Empty;
    string Monthsub = string.Empty;
    string Sf_Code = string.Empty;
    string MultiSf_Code = string.Empty;
    DataSet dsprd = new DataSet();
    string distributor_code = string.Empty;
    int subTotalRowIndex = 0;
    string sf_type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["SF_Code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        date = Request.QueryString["date"].ToString();
        tdate = Request.QueryString["tdate"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //string strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString().Substring(0, 3);
        lblHead.Text = "SalesMan Dailycall Report From  " + date + " and " + tdate + "";
        Feild.Text = sfname;
        FillSF();
    }

    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();

        string stCrtDtaPnt = string.Empty;
        dsGV = dc.salesmandailycallfl(Sf_Code, divcode, date, tdate, subdiv_code);
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            //dsGV.Tables[0].Columns.RemoveAt(0);
            //dsGV.Tables[0].Columns.RemoveAt(0);
            gvclosingstockanalysis.DataSource = dsGV;
            gvclosingstockanalysis.DataBind();
        }
        else
        {
            gvclosingstockanalysis.DataSource = null;
            gvclosingstockanalysis.DataBind();
        }

    }
    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        //SalesForce SF1 = new SalesForce();
        //DataSet ff1 = new DataSet();
        //ff1 = SF1.GetProduct_Name(divcode);
        //int cnt = ff1.Tables[0].Rows.Count;
        //foreach (DataRow drdoc in ff1.Tables[0].Rows)
        //{
        //    //for (int j = 0; j < cnt; j++)
        //    //{

        //    string prdt_code = drdoc["Product_Detail_Code"].ToString();
        //    string stock_code = Convert.ToString(orderId);
        //    DataSet dm = new DataSet();
        //    dm = SF1.GetDistNamewise1(divcode, stock_code);

        //    TableCell txt1 = new TableCell();
        //    Literal fflit = new Literal();
        //    fflit.Text = dm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        //    //txt1.ID = "txtquantity_";
        //    txt1.Controls.Add(fflit);
        //    //txt1.
        //    e.Row.Cells.Add(txt1);
        //    //}
        //}
        //Response.Write(gvtotalorder.Rows[0].Cells[2].Text);

        SalesForce SF = new SalesForce();
        DataSet ff = new DataSet();
        ff = SF.GetProduct_Name(divcode);
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);


            TableCell HeaderCell = new TableCell();
            //HeaderCell.Width = 25;
            //HeaderCell.Height = 35;
            HeaderCell.Font.Bold = true;
            //HeaderCell.ApplyStyle.
            HeaderCell.BackColor = System.Drawing.Color.FromName("#6B7794");

            TableCell Distributor = new TableCell();
            //HeaderCell.Width = 25;
            //HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#6B7794");
            HeaderCell.Text = "S.No";



            HeaderCell = new TableCell();
            //HeaderCell.Width = 110;
            //HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#6B7794");
            HeaderCell.Text = "Retailer";
            HeaderGridRow0.Cells.Add(HeaderCell);


            foreach (DataRow drdoctor in ff.Tables[0].Rows)
            {
                HeaderCell = new TableCell();
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#6B7794");

                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderCell.Attributes["style"] = "font: Bold";
                HeaderCell.Text = drdoctor["Product_Detail_Name"].ToString();
                HeaderGridRow0.Cells.Add(HeaderCell);
            }

            gvclosingstockanalysis.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }

    }

    public class GridDecorator
    {
        public static void MergeRows(GridView gridView)
        {
            for (int rowIndex = 0; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int i = row.Cells.Count - 1; i < row.Cells.Count; i++)
                {
                    if (row.Cells[i].Text == previousRow.Cells[i].Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                               previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
            }
        }
    }


    protected void ExportToExcel(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DCR dc = new DCR();

        string stCrtDtaPnt = string.Empty;
        ds = dc.salesmandailycallfl(Sf_Code, divcode, date, tdate, subdiv_code);
        string divcode1 = divcode.ToString();

        DataTable dtOrders = new DataTable();
        dtOrders.Columns.Add("S.No", typeof(string));
        dtOrders.Columns.Add("Sf Code", typeof(string));
        dtOrders.Columns.Add("SF Name", typeof(string));
        dtOrders.Columns.Add("Date", typeof(string));
        dtOrders.Columns.Add("Distributor", typeof(string));
        dtOrders.Columns.Add("Route", typeof(string));
        dtOrders.Columns.Add("Distributor Address", typeof(string));
        dtOrders.Columns.Add("Total Weight", typeof(string));
        dtOrders.Columns.Add("Total Calls", typeof(string));
        dtOrders.Columns.Add("Productive Calls", typeof(string));
        dtOrders.Columns.Add("Non-Productive Calls", typeof(string));
        dtOrders.Columns.Add("Order Value", typeof(string));
        dtOrders.Columns.Add("Start Time", typeof(string));
        dtOrders.Columns.Add("End Time", typeof(string));
        dtOrders.Columns.Add("Total Hour", typeof(string));

        int i = 1;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            DataRow rw = dtOrders.NewRow();

            rw["S.No"] = i.ToString();
            rw["Sf Code"] = dr["sf_code"].ToString();
            rw["Sf Name"] = dr["Fieldforce"].ToString();
            rw["Date"] = dr["Activity_Date"].ToString();
            rw["Distributor"] = dr["Distributor"].ToString();
            rw["Route"] = dr["Route"].ToString();
            rw["Distributor Address"] = dr["Distributor_Address"].ToString();
            rw["Total Weight"] = dr["Total_weight"].ToString();
            rw["Total Calls"] = dr["Total_Calls"].ToString();
            rw["Productive Calls"] = dr["Productive_calls"].ToString();
            rw["Non-Productive Calls"] = dr["NonProductive_calls"].ToString();
            rw["Order Value"] = dr["Order_Value"].ToString();
            rw["Start Time"] = dr["Start_time"].ToString();
            rw["End Time"] = dr["End_time"].ToString();
            rw["Total Hour"] = dr["TotalHoursWorked"].ToString();

            dtOrders.Rows.Add(rw);
            i++;
        }


        string filename = System.IO.Path.GetTempPath() + "SalesManDailycall_Report_" + divcode1 + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";
        if (File.Exists(filename))
        {
            filename = System.IO.Path.GetTempPath() + "SalesManDailycall_Report_" + divcode1 + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";
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
        foreach (DataColumn column in dtOrders.Columns)
        {
            var cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) };
            headerRow.AppendChild(cell);
        }
        sheetData.AppendChild(headerRow);
        foreach (DataRow row in dtOrders.Rows)
        {
            var newRow = new Row();
            foreach (DataColumn col in dtOrders.Columns)
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
                Response.AddHeader("content-disposition", string.Format("attachment; filename ={0}", "Daily_Call_Report_" + DateTime.Now.ToString() + ".xlsx"));
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


    //protected void ExportToExcel(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    DCR dc = new DCR();

    //    string stCrtDtaPnt = string.Empty;
    //    ds = dc.salesmandailycallfl(Sf_Code, divcode, date, tdate, subdiv_code);

    //    gvclosingstockanalysis.AllowPaging = false;
    //    gvclosingstockanalysis.FooterRow.Visible = false;

    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        gvclosingstockanalysis.DataSource = ds;
    //        gvclosingstockanalysis.DataBind();
    //    }
    //    else
    //    {
    //        gvclosingstockanalysis.DataSource = null;
    //        gvclosingstockanalysis.DataBind();
    //    }

    //    string strFileName = "SalesManDailycall_Report";
    //    string attachment = "attachment; filename='" + strFileName + "'.xls";


    //    Response.ClearContent();
    //    Response.Buffer = true;
    //    Response.AddHeader("content-disposition", attachment);
    //    Response.Charset = "";
    //    Response.ContentType = "application/ms-excel";
    //    using (StringWriter sw = new StringWriter())
    //    {
    //        HtmlTextWriter hw = new HtmlTextWriter(sw);

    //        //To Export all pages

    //        gvclosingstockanalysis.HeaderRow.BackColor = Color.White;
    //        foreach (TableCell cell in gvclosingstockanalysis.HeaderRow.Cells)
    //        {
    //            cell.BackColor = gvclosingstockanalysis.HeaderStyle.BackColor;
    //        }
    //        foreach (GridViewRow row in gvclosingstockanalysis.Rows)
    //        {
    //            row.BackColor = Color.White;
    //            foreach (TableCell cell in row.Cells)
    //            {
    //                if (row.RowIndex % 2 == 0)
    //                {
    //                    cell.BackColor = gvclosingstockanalysis.AlternatingRowStyle.BackColor;
    //                }
    //                else
    //                {
    //                    cell.BackColor = gvclosingstockanalysis.RowStyle.BackColor;
    //                }
    //                cell.CssClass = "textmode";
    //            }
    //        }

    //        gvclosingstockanalysis.RenderControl(hw);

    //        //style to format numbers to string
    //        string style = @"<style> .textmode { } </style>";
    //        Response.Write(style);
    //        Response.Output.Write(sw.ToString());
    //        Response.Flush();
    //        Response.End();

    //    }
    //}



    private void GeneratePDF()
    {
        try
        {

            DataSet ds = new DataSet();
            DCR dc = new DCR();

            string stCrtDtaPnt = string.Empty;
            ds = dc.salesmandailycallfl(Sf_Code, divcode, date, tdate, subdiv_code);
            string divcode1 = divcode.ToString();

            DataTable dtOrders = new DataTable();
            dtOrders.Columns.Add("S.No", typeof(string));
            dtOrders.Columns.Add("Sf Code", typeof(string));
            dtOrders.Columns.Add("SF Name", typeof(string));
            dtOrders.Columns.Add("Date", typeof(string));
            dtOrders.Columns.Add("Distributor", typeof(string));
            dtOrders.Columns.Add("Route", typeof(string));
            dtOrders.Columns.Add("Distributor Address", typeof(string));
            dtOrders.Columns.Add("Total Weight", typeof(string));
            dtOrders.Columns.Add("Total Calls", typeof(string));
            dtOrders.Columns.Add("Productive Calls", typeof(string));
            dtOrders.Columns.Add("Non-Productive Calls", typeof(string));
            dtOrders.Columns.Add("Order Value", typeof(string));
            dtOrders.Columns.Add("Start Time", typeof(string));
            dtOrders.Columns.Add("End Time", typeof(string));
            dtOrders.Columns.Add("Total Hour", typeof(string));

            int i = 1;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                DataRow rw = dtOrders.NewRow();

                rw["S.No"] = i.ToString();
                rw["Sf Code"] = dr["sf_code"].ToString();
                rw["Sf Name"] = dr["Fieldforce"].ToString();
                rw["Date"] = dr["Activity_Date"].ToString();
                rw["Distributor"] = dr["Distributor"].ToString();
                rw["Route"] = dr["Route"].ToString();
                rw["Distributor Address"] = dr["Distributor_Address"].ToString();
                rw["Total Weight"] = dr["Total_weight"].ToString();
                rw["Total Calls"] = dr["Total_Calls"].ToString();
                rw["Productive Calls"] = dr["Productive_calls"].ToString();
                rw["Non-Productive Calls"] = dr["NonProductive_calls"].ToString();
                rw["Order Value"] = dr["Order_Value"].ToString();
                rw["Start Time"] = dr["Start_time"].ToString();
                rw["End Time"] = dr["End_time"].ToString();
                rw["Total Hour"] = dr["TotalHoursWorked"].ToString();

                dtOrders.Rows.Add(rw);
                i++;
            }


            ExportToPdf(dtOrders);
        }
        catch (Exception ex)
        {

        }
    }

    public void ExportToPdf(DataTable myDataTable)
    {
        DataTable dt = myDataTable;
        Document pdfDoc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
        iTextSharp.text.Font font13 = iTextSharp.text.FontFactory.GetFont("ARIAL", 13);
        iTextSharp.text.Font font18 = iTextSharp.text.FontFactory.GetFont("ARIAL", 18);
        try
        {
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
            pdfDoc.Open();

            if (dt.Rows.Count > 0)
            {
                PdfPTable PdfTable = new PdfPTable(1);
                PdfTable.TotalWidth = 200f;
                PdfTable.LockedWidth = true;

                PdfPCell PdfPCell = new PdfPCell(new Phrase(new Chunk("Employee Details", font18)));
                PdfPCell.Border = Rectangle.NO_BORDER;
                PdfTable.AddCell(PdfPCell);
                DrawLine(writer, 25f, pdfDoc.Top - 30f, pdfDoc.PageSize.Width - 25f, pdfDoc.Top - 30f, new BaseColor(System.Drawing.Color.Red));
                pdfDoc.Add(PdfTable);

                PdfTable = new PdfPTable(dt.Columns.Count);
                PdfTable.SpacingBefore = 20f;
                for (int columns = 0; columns <= dt.Columns.Count - 1; columns++)
                {
                    PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Columns[columns].ColumnName, font18)));
                    PdfTable.AddCell(PdfPCell);
                }

                for (int rows = 0; rows <= dt.Rows.Count - 1; rows++)
                {
                    for (int column = 0; column <= dt.Columns.Count - 1; column++)
                    {
                        PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Rows[rows][column].ToString(), font13)));
                        PdfTable.AddCell(PdfPCell);
                    }
                }
                pdfDoc.Add(PdfTable);
            }
            pdfDoc.Close();

            string strFileName = "SalesManDailycall_Report_" + DateTime.Now.Date.Day.ToString();
            string attachment = "attachment; filename='" + strFileName + "'.pdf";
            Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment; filename=dsejReport_" + DateTime.Now.Date.Day.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Hour.ToString() + DateTime.Now.Date.Minute.ToString() + DateTime.Now.Date.Second.ToString() + DateTime.Now.Date.Millisecond.ToString() + ".pdf");
            Response.AddHeader("content-disposition", attachment);
            System.Web.HttpContext.Current.Response.Write(pdfDoc);
            Response.Flush();
            Response.End();
        }
        catch (DocumentException de)
        {
        }
        // System.Web.HttpContext.Current.Response.Write(de.Message)
        catch (IOException ioEx)
        {
        }
        // System.Web.HttpContext.Current.Response.Write(ioEx.Message)
        catch (Exception ex)
        {
        }
    }

    private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, BaseColor color)
    {
        PdfContentByte contentByte = writer.DirectContent;
        contentByte.SetColorStroke(color);
        contentByte.MoveTo(x1, y1);
        contentByte.LineTo(x2, y2);
        contentByte.Stroke();
    }
    protected void btnPdf_Click(object sender, EventArgs e)
    {
        GeneratePDF();
    }

    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        // Session["ctrl"] = pnlContents;
        //  Control ctrl = (Control)Session["ctrl"];
        //   PrintWebControl(ctrl);
    }
    public static void PrintWebControl(System.Web.UI.Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        System.Web.UI.Page pg = new System.Web.UI.Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }



    protected void gvclosingstockanalysis_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        this.gvclosingstockanalysis.PageIndex = e.NewPageIndex;
        this.FillSF();

        //gvclosingstockanalysis.PageIndex = e.NewPageIndex;
        //FillSF();
    }

}






//using System;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.IO;
//using System.Data;
//using Bus_EReport;
//using System.Web.UI.HtmlControls;
//using System.Drawing;

//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html.simpleparser;
//using Rectangle = iTextSharp.text.Rectangle;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Spreadsheet;

////using iTextSharp.tool.xml;
////using iTextSharp.text;
////using iTextSharp.text.html.simpleparser;
////using iTextSharp.text.pdf;
////using iTextSharp.text.html;

//public partial class MIS_Reports_rptsalesmandailycallreportfl : System.Web.UI.Page
//{
//    string sfCode = string.Empty;
//    string sfname = string.Empty;
//    string divcode = string.Empty;
//    string Year = string.Empty;
//    string tdate = string.Empty;
//    string date = string.Empty;
//    string subdiv_code = string.Empty;
//    DataSet dsSalesForce = new DataSet();
//    DataSet dsmgrsf = new DataSet();
//    DataSet dsDoc = null;
//    DateTime dtCurrent;
//    string tot_dr = string.Empty;
//    string Monthsub = string.Empty;
//    string Sf_Code = string.Empty;
//    string MultiSf_Code = string.Empty;
//    DataSet dsprd = new DataSet();
//    string distributor_code = string.Empty;
//    int subTotalRowIndex = 0;
//    string sf_type = string.Empty;
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        divcode = Session["div_code"].ToString();
//        Sf_Code = Request.QueryString["SF_Code"].ToString();
//        sfname = Request.QueryString["SF_Name"].ToString();
//        date = Request.QueryString["date"].ToString();
//        tdate = Request.QueryString["tdate"].ToString();
//        subdiv_code = Request.QueryString["subdiv"].ToString();
//        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
//        //string strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString().Substring(0, 3);
//        lblHead.Text = "SalesMan Dailycall Report From  " + date +" and "+ tdate +"";
//        Feild.Text = sfname;
//        FillSF();
//    }

//    private void FillSF()
//    {
//        string sURL = string.Empty;
//        string stURL = string.Empty;
//        DataSet dsGV = new DataSet();
//        DataSet dsGc = new DataSet();
//        DCR dc = new DCR();

//        string stCrtDtaPnt = string.Empty;
//        dsGV = dc.salesmandailycallfl(Sf_Code, divcode, date, tdate, subdiv_code);
//        if (dsGV.Tables[0].Rows.Count > 0)
//        {
//            //dsGV.Tables[0].Columns.RemoveAt(0);
//            //dsGV.Tables[0].Columns.RemoveAt(0);
//            gvclosingstockanalysis.DataSource = dsGV;
//            gvclosingstockanalysis.DataBind();
//        }
//        else
//        {
//            gvclosingstockanalysis.DataSource = null;
//            gvclosingstockanalysis.DataBind();
//        }

//    }
//    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
//    {
//        //SalesForce SF1 = new SalesForce();
//        //DataSet ff1 = new DataSet();
//        //ff1 = SF1.GetProduct_Name(divcode);
//        //int cnt = ff1.Tables[0].Rows.Count;
//        //foreach (DataRow drdoc in ff1.Tables[0].Rows)
//        //{
//        //    //for (int j = 0; j < cnt; j++)
//        //    //{

//        //    string prdt_code = drdoc["Product_Detail_Code"].ToString();
//        //    string stock_code = Convert.ToString(orderId);
//        //    DataSet dm = new DataSet();
//        //    dm = SF1.GetDistNamewise1(divcode, stock_code);

//        //    TableCell txt1 = new TableCell();
//        //    Literal fflit = new Literal();
//        //    fflit.Text = dm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
//        //    //txt1.ID = "txtquantity_";
//        //    txt1.Controls.Add(fflit);
//        //    //txt1.
//        //    e.Row.Cells.Add(txt1);
//        //    //}
//        //}
//        //Response.Write(gvtotalorder.Rows[0].Cells[2].Text);

//        SalesForce SF = new SalesForce();
//        DataSet ff = new DataSet();
//        ff = SF.GetProduct_Name(divcode);
//        if (e.Row.RowType == DataControlRowType.Header)
//        {
//            GridView HeaderGrid = (GridView)sender;
//            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);


//            TableCell HeaderCell = new TableCell();
//            //HeaderCell.Width = 25;
//            //HeaderCell.Height = 35;
//            HeaderCell.Font.Bold = true;
//            //HeaderCell.ApplyStyle.
//            HeaderCell.BackColor = System.Drawing.Color.FromName("#6B7794");

//            TableCell Distributor = new TableCell();
//            //HeaderCell.Width = 25;
//            //HeaderCell.Height = 35;
//            HeaderCell.BackColor = System.Drawing.Color.FromName("#6B7794");
//            HeaderCell.Text = "S.No";



//            HeaderCell = new TableCell();
//            //HeaderCell.Width = 110;
//            //HeaderCell.Height = 35;
//            HeaderCell.BackColor = System.Drawing.Color.FromName("#6B7794");
//            HeaderCell.Text = "Retailer";
//            HeaderGridRow0.Cells.Add(HeaderCell);


//            foreach (DataRow drdoctor in ff.Tables[0].Rows)
//            {
//                HeaderCell = new TableCell();
//                HeaderCell.Height = 35;
//                HeaderCell.BackColor = System.Drawing.Color.FromName("#6B7794");

//                HeaderCell.Attributes["style"] = "font: Andalus";
//                HeaderCell.Attributes["style"] = "font: Bold";
//                HeaderCell.Text = drdoctor["Product_Detail_Name"].ToString();
//                HeaderGridRow0.Cells.Add(HeaderCell);
//            }

//            gvclosingstockanalysis.Controls[0].Controls.AddAt(0, HeaderGridRow0);

//            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
//        }

//    }

//    public class GridDecorator
//    {
//        public static void MergeRows(GridView gridView)
//        {
//            for (int rowIndex = 0; rowIndex >= 0; rowIndex--)
//            {
//                GridViewRow row = gridView.Rows[rowIndex];
//                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

//                for (int i = row.Cells.Count - 1; i < row.Cells.Count; i++)
//                {
//                    if (row.Cells[i].Text == previousRow.Cells[i].Text)
//                    {
//                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
//                                               previousRow.Cells[i].RowSpan + 1;
//                        previousRow.Cells[i].Visible = false;
//                    }
//                }
//            }
//        }
//    }
//    protected void ExportToExcel(object sender, EventArgs e)
//    {
//        DataSet ds = new DataSet();
//        DCR dc = new DCR();

//        string stCrtDtaPnt = string.Empty;
//        ds = dc.salesmandailycallfl(Sf_Code, divcode, date, tdate, subdiv_code);
//        string divcode1 = divcode.ToString();

//        DataTable dtOrders = new DataTable();
//        dtOrders.Columns.Add("S.No", typeof(string));
//        dtOrders.Columns.Add("Sf Code", typeof(string));
//        dtOrders.Columns.Add("SF Name", typeof(string));
//        dtOrders.Columns.Add("Date", typeof(string));
//        dtOrders.Columns.Add("Distributor", typeof(string));
//        dtOrders.Columns.Add("Route", typeof(string));
//        dtOrders.Columns.Add("Distributor Address", typeof(string));
//        dtOrders.Columns.Add("Total Weight", typeof(string));
//        dtOrders.Columns.Add("Total Calls", typeof(string));
//        dtOrders.Columns.Add("Productive Calls", typeof(string));
//        dtOrders.Columns.Add("Non-Productive Calls", typeof(string));
//        dtOrders.Columns.Add("Order Value", typeof(string));
//        dtOrders.Columns.Add("Start Time", typeof(string));
//        dtOrders.Columns.Add("End Time", typeof(string));
//        dtOrders.Columns.Add("Total Hour", typeof(string));

//        int i = 1;
//        foreach (DataRow dr in ds.Tables[0].Rows)
//        {

//            DataRow rw = dtOrders.NewRow();

//            rw["S.No"] = i.ToString();
//            rw["Sf Code"] = dr["sf_code"].ToString();
//            rw["Sf Name"] = dr["Fieldforce"].ToString();
//            rw["Date"] = dr["Activity_Date"].ToString();
//            rw["Distributor"] = dr["Distributor"].ToString();
//            rw["Route"] = dr["Route"].ToString();
//            rw["Distributor Address"] = dr["Distributor_Address"].ToString();
//            rw["Total Weight"] = dr["Total_weight"].ToString();
//            rw["Total Calls"] = dr["Total_Calls"].ToString();
//            rw["Productive Calls"] = dr["Productive_calls"].ToString();
//            rw["Non-Productive Calls"] = dr["NonProductive_calls"].ToString();
//            rw["Order Value"] = dr["Order_Value"].ToString();
//            rw["Start Time"] = dr["Start_time"].ToString();
//            rw["End Time"] = dr["End_time"].ToString();
//            rw["Total Hour"] = dr["TotalHoursWorked"].ToString();

//            dtOrders.Rows.Add(rw);
//            i++;
//        }


//        string filename = System.IO.Path.GetTempPath() + "SalesManDailycall_Report_" + divcode1 + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";
//        if (File.Exists(filename))
//        {
//            filename = System.IO.Path.GetTempPath() + "SalesManDailycall_Report_" + divcode1 + "_" + (System.DateTime.Now.ToString()).Replace(":", "_").Replace("/", "_").Replace(" ", "_") + ".xlsx";
//        }
//        uint sheetId = 1;
//        SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filename, SpreadsheetDocumentType.Workbook);
//        WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
//        workbookpart.Workbook = new Workbook();
//        var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
//        var sheetData = new SheetData();
//        worksheetPart.Worksheet = new Worksheet(sheetData);
//        Sheets sheets;
//        sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
//        var sheet = new Sheet()
//        {
//            Id = spreadsheetDocument.WorkbookPart.
//                       GetIdOfPart(worksheetPart),
//            SheetId = sheetId,
//            Name = "Sheet" + sheetId
//        };
//        sheets.Append(sheet);
//        var headerRow = new Row();
//        foreach (DataColumn column in dtOrders.Columns)
//        {
//            var cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) };
//            headerRow.AppendChild(cell);
//        }
//        sheetData.AppendChild(headerRow);
//        foreach (DataRow row in dtOrders.Rows)
//        {
//            var newRow = new Row();
//            foreach (DataColumn col in dtOrders.Columns)
//            {
//                var cell = new Cell
//                {
//                    DataType = CellValues.String,
//                    CellValue = new CellValue(row[col].ToString())
//                };
//                newRow.AppendChild(cell);
//            }

//            sheetData.AppendChild(newRow);
//        }
//        workbookpart.Workbook.Save();
//        spreadsheetDocument.Close();
//        try
//        {
//            Response.ClearContent();
//            using (FileStream objFileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
//            {
//                byte[] data1 = new byte[objFileStream.Length];
//                objFileStream.Read(data1, 0, data1.Length);
//                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
//                Response.AddHeader("content-disposition", string.Format("attachment; filename ={0}", "Daily_Call_Report_" + DateTime.Now.ToString() + ".xlsx"));
//                Response.BinaryWrite(data1);
//            }
//            FileInfo currentfile = new FileInfo(filename);
//            currentfile.Delete();
//        }
//        catch (Exception ex)
//        {
//        }
//        Response.End();



//        //gvclosingstockanalysis.AllowPaging = false;
//        //gvclosingstockanalysis.FooterRow.Visible = false;

//        //if (ds.Tables[0].Rows.Count > 0)
//        //{            
//        //    gvclosingstockanalysis.DataSource = ds;
//        //    gvclosingstockanalysis.DataBind();
//        //}
//        //else
//        //{
//        //    gvclosingstockanalysis.DataSource = null;
//        //    gvclosingstockanalysis.DataBind();
//        //}

//        //string strFileName = "SalesManDailycall_Report";
//        //string attachment = "attachment; filename='" + strFileName + "'.xls";


//        //Response.ClearContent();
//        //Response.Buffer = true;
//        //Response.AddHeader("content-disposition", attachment);
//        //Response.Charset = "";
//        //Response.ContentType = "application/ms-excel";
//        //using (StringWriter sw = new StringWriter())
//        //{
//        //    HtmlTextWriter hw = new HtmlTextWriter(sw);

//        //    //To Export all pages

//        //    gvclosingstockanalysis.HeaderRow.BackColor = Color.White;



//        //    foreach (TableCell cell in gvclosingstockanalysis.HeaderRow.Cells)
//        //    {
//        //        cell.BackColor = gvclosingstockanalysis.HeaderStyle.BackColor;
//        //    }
//        //    foreach (GridViewRow row in ds.Tables[0].Rows)
//        //    {
//        //        row.BackColor = Color.White;
//        //        foreach (TableCell cell in row.Cells)
//        //        {
//        //            if (row.RowIndex % 2 == 0)
//        //            {
//        //                cell.BackColor = gvclosingstockanalysis.AlternatingRowStyle.BackColor;
//        //            }
//        //            else
//        //            {
//        //                cell.BackColor = gvclosingstockanalysis.RowStyle.BackColor;
//        //            }
//        //            cell.CssClass = "textmode";
//        //        }
//        //    }

//        //    gvclosingstockanalysis.RenderControl(hw);

//        //    //style to format numbers to string
//        //    string style = @"<style> .textmode { } </style>";
//        //    Response.Write(style);
//        //    Response.Output.Write(sw.ToString());
//        //    Response.Flush();
//        //    Response.End();

//        //}
//    }

//    protected void btnPdf_Click(object sender, EventArgs e)
//    {
//        string strFileName = "SalesManDailycall_Report";
//        string attachment = "attachment; filename='" + strFileName + "'.pdf";


//        //Set the Size of PDF document.



//        //Rectangle rect = new System.Drawing.Rectangle(500 , 100);
//        //Document pdfDoc = new Document(rect, 10f, 10f, 10f, 0f);

//        //Initialize the PDF document object.
//        //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
//        //pdfDoc.Open();

//        ////Loop through GridView Pages.
//        //for (int i = 0; i < gvclosingstockanalysis.PageCount; i++)
//        //{
//        //    //Set the Page Index.
//        //    gvclosingstockanalysis.PageIndex = i;

//        //    //Hide Page as not needed in PDF.
//        //    gvclosingstockanalysis.PagerSettings.Visible = false;

//        //    //Populate the GridView with records for the Page Index.
//        //    this.FillSF();

//        //    //Render the GridView as HTML and add to PDF.
//        //    using (StringWriter sw = new StringWriter())
//        //    {
//        //        using (HtmlTextWriter hw = new HtmlTextWriter(sw))
//        //        {
//        //            gvclosingstockanalysis.RenderControl(hw);
//        //            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
//        //            StringReader sr = new StringReader(sw.ToString());
//        //            htmlparser.Parse(sr);
//        //        }
//        //    }

//        //    //Add a new Page to PDF document.
//        //    pdfDoc.NewPage();
//        //}
//        ////Close the PDF document.
//        //pdfDoc.Close();

//        ////Download the PDF file.
//        //Response.ContentType = "application/pdf";
//        //Response.AddHeader("content-disposition", attachment);
//        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
//        //Response.Write(pdfDoc);
//        //Response.End();

//        //Response.AddHeader("content-disposition", attachment);
//        //Response.ContentType = "application/pdf";

//        Response.Cache.SetCacheability(HttpCacheability.NoCache);
//        StringWriter sw = new StringWriter();
//        HtmlTextWriter hw = new HtmlTextWriter(sw);
//        gvclosingstockanalysis.AllowPaging = false;
//        gvclosingstockanalysis.DataBind();
//        gvclosingstockanalysis.RenderControl(hw);
//        gvclosingstockanalysis.HeaderRow.Style.Add("width", "15%");
//        gvclosingstockanalysis.HeaderRow.Style.Add("font-size", "10px");
//        gvclosingstockanalysis.ShowHeader = true;
//        // gvclosingstockanalysis.Style.Add("text-decoration", "none");
//        gvclosingstockanalysis.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
//        gvclosingstockanalysis.Style.Add("font-size", "8px");

//        gvclosingstockanalysis.HeaderRow.BackColor = System.Drawing.Color.White;

//        StringReader sr = new StringReader(sw.ToString());
//        Document pdfDoc = new Document(PageSize.A2, 7f, 7f, 7f, 0f);
//        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
//        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
//        pdfDoc.Open();
//        htmlparser.Parse(sr);
//        pdfDoc.Close();
//        Response.Write(pdfDoc);
//        Response.End();
//    }

//    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
//    {
//        /* Verifies that the control is rendered */
//    }

//    protected void btnPrint_Click(object sender, EventArgs e)
//    {
//        // Session["ctrl"] = pnlContents;
//        //  Control ctrl = (Control)Session["ctrl"];
//        //   PrintWebControl(ctrl);
//    }
//    public static void PrintWebControl(System.Web.UI.Control ControlToPrint)
//    {
//        StringWriter stringWrite = new StringWriter();
//        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
//        if (ControlToPrint is WebControl)
//        {
//            Unit w = new Unit(100, UnitType.Percentage);
//            ((WebControl)ControlToPrint).Width = w;
//        }
//        Page pg = new Page();
//        pg.EnableEventValidation = false;
//        HtmlForm frm = new HtmlForm();
//        pg.Controls.Add(frm);
//        frm.Attributes.Add("runat", "server");
//        frm.Controls.Add(ControlToPrint);
//        pg.DesignerInitialize();
//        pg.RenderControl(htmlWrite);
//        string strHTML = stringWrite.ToString();
//        HttpContext.Current.Response.Clear();
//        HttpContext.Current.Response.Write(strHTML);
//        HttpContext.Current.Response.Write("<script>window.print();</script>");
//        HttpContext.Current.Response.End();

//    }



//    protected void btnClose_Click(object sender, EventArgs e)
//    {

//    }



//    protected void gvclosingstockanalysis_PageIndexChanging(object sender, GridViewPageEventArgs e)
//    {

//        this.gvclosingstockanalysis.PageIndex = e.NewPageIndex;
//        this.FillSF();

//        //gvclosingstockanalysis.PageIndex = e.NewPageIndex;
//        //FillSF();
//    }

//}
