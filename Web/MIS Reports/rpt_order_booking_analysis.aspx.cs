using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using iTextSharp.tool.xml;
using System.Net;
using System.Text.RegularExpressions;


public partial class MIS_Reports_rpt_order_booking_analysis : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string sf_type = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string type = string.Empty;
    string h = string.Empty;
    string wrktypename = string.Empty;
    int sum_time = 0;
    DataSet dsSalesForce = new DataSet();
    DataSet dsdatee = new DataSet();
    DataSet dsDoc = null;
    DataSet dsGV = null;
    DateTime dtCurrent;
    TimeSpan ff;
    int rowspan = 0;
    string sCurrentDate = string.Empty;
    string endTime = string.Empty;
    string startTime = string.Empty;
    string tot_dr = string.Empty;
    string tot_value = string.Empty;
    string con_qty = string.Empty;
    string ec = string.Empty;
    string Monthsub = string.Empty;
    string date = string.Empty;
    string endd = string.Empty;
    DataSet dsDoctor = new DataSet();
    string gg = string.Empty;
    string imagepath = string.Empty;
    int quantity2 = 0;
    string mode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();

        sfname = Request.QueryString["sfname"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        imagepath = Request.QueryString["imgpath"].ToString();

        logoo.ImageUrl = imagepath;
        //sfCode = "Admin";

        if (sfCode.Contains("MGR"))
        {
            sf_type = "2";
        }
        else if (sfCode.Contains("MR"))
        {
            sf_type = "1";
        }
        else
        {
            sf_type = "0";
        }


        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        lblHead.Text = "Order Booking Analysis View for the Month of " + strFMonthName + " " + FYear;


        lblsf_name.Text = sfname;


        Fillvaluewise();



    }
    protected void gridView_PreRender(object sender, EventArgs e)
    {
        GridDecorator.MergeRows(gvtotalorder);
    }



    public class GridDecorator
    {
        DataSet dsGV = new DataSet();
        public static void MergeRows(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                // for (int i = 0; i < row.Cells.Count - 32; i++)
                // {
                if (row.Cells[0].Text == previousRow.Cells[0].Text)
                {
                    DataSet dsGV = new DataSet();
                    DCR dc = new DCR();
                    row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                           previousRow.Cells[0].RowSpan + 1;

                    previousRow.Cells[0].Visible = false;
                    row.Cells[1].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                           previousRow.Cells[0].RowSpan + 1;

                    previousRow.Cells[1].Visible = false;
                    row.Cells[2].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                           previousRow.Cells[0].RowSpan + 1;

                    previousRow.Cells[2].Visible = false;
                }
                // }
            }
        }
    }




    private void Fillvaluewise()
    {


        DataTable dsGV = new DataTable();

        DCR dc = new DCR();
        mode = "valuewise";
        dsGV = dc.attendance_view_tb(sfCode, divcode, FMonth, FYear, mode);
        DataColumn dcol2 = new DataColumn("Total", typeof(string));
        dsGV.Columns.Add(dcol2);
        if (dsGV.Rows.Count > 0)
        {
            GridView1.DataSource = dsGV;
            GridView1.DataBind();

            if (dsGV.Rows.Count > 0)
            {
                for (int i = 0; i < dsGV.Rows.Count; i++)
                {
                    for (int j = 0; j < dsGV.Columns.Count; j++)
                    {
                        if (string.IsNullOrEmpty(dsGV.Rows[i][j].ToString()))
                        {

                            dsGV.Rows[i][j] = 0;

                        }
                        else
                        {
                            if (j > 3)
                            {
                                dsGV.Rows[i][j] = Convert.ToDecimal(dsGV.Rows[i][j]).ToString();
                            }
                        }

                    }
                }

                GridView1.FooterRow.Cells[1].Text = "TOTAL";
                GridView1.FooterRow.Cells[1].ColumnSpan = 1;
                decimal Gtot = 0;
                for (int h = 0; h < dsGV.Rows.Count; h++)
                {
                    decimal rtot = 0;

                    for (int k = 3; k < dsGV.Columns.Count; k++)
                    {
                        object o = dsGV.Rows[h].ItemArray[k];
                        rtot += Convert.ToDecimal(o);
                    }
                    GridView1.Rows[h].Cells[dsGV.Columns.Count - 1].Text = Convert.ToString(rtot);
                    GridView1.Rows[h].Cells[dsGV.Columns.Count - 1].Font.Bold = true;
                    GridView1.Rows[h].Cells[dsGV.Columns.Count - 1].BackColor = System.Drawing.Color.FromName("#D0ECE7");
                    Gtot += rtot;
                }
                int kk = 0;
                for (int k = 3; k < GridView1.Rows[0].Cells.Count; k++)
                {
                    kk = k;
                    string total = "0";
                    total = dsGV.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<String>((dsGV.Columns[k].ToString())))).ToString();
                    GridView1.FooterRow.Cells[k].Text = total.ToString();
                    GridView1.FooterRow.Cells[k].Font.Bold = true;
                    GridView1.FooterRow.BackColor = System.Drawing.Color.FromName("#D0ECE7");
                }

                GridView1.FooterRow.Cells[kk].Text = Gtot.ToString();
                GridView1.FooterRow.Cells[kk].Font.Bold = true;


            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }
    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
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

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strFileName = Page.Title;
        string attachment = "attachment; filename='" + strFileName + "'.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        form1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {

        string strFileName = Page.Title;

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                //this.Page.RenderControl(hw);
                this.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4,
                    10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename= '" + strFileName + "'.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
            }

        }
    }



    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    protected void gvtotalorder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //e.Row.Cells[2].Visible = false;
        //e.Row.Cells[3].Visible = false;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
        e.Row.Cells[2].Visible = false;
        //e.Row.Cells[4].Visible = false;
    }
}