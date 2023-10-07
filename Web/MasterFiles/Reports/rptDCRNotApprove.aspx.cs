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
using System.Net;

public partial class Reports_rptDCRNotApprove : System.Web.UI.Page
{
    int cmonth = -1;
    int cyear = -1;
    DataSet dsSalesForce = null;
    DataSet dsDCR = null;
    string sPending = string.Empty;
    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        cmonth = Convert.ToInt16(Request.QueryString["cmon"].ToString());
        cyear = Convert.ToInt16(Request.QueryString["cyear"].ToString());

        FillSalesForce(cmonth, cyear);

        string sMonth = getMonthName(cmonth) + " - " + cyear.ToString();
        lblHead.Text = lblHead.Text + sMonth;
        ExportButton();
    }

    private void ExportButton()
    {
        btnPDF.Visible = false;
    }

    private void FillSalesForce(int cmonth, int cyear)
    {
        // Fetch the total rows for the table
        DCR dc = new DCR ();
        dsSalesForce = dc.get_dcr_ff_details(cmonth, cyear, div_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
            ViewState["dsSalesForce"] = dsSalesForce;

        CreateDynamicTable();
    }

    private void CreateDynamicTable()
    {

        if (ViewState["dsSalesForce"] != null)
        {

            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
           // tr_header.Attributes.Add("Class", "mGrid");
            tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tr_header.ForeColor = System.Drawing.Color.White;
            tr_header.BorderWidth = 1;


            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.Controls.Add(lit_SNo);
            tr_header.Cells.Add(tc_SNo);
            //tr_header.BackColor = System.Drawing.Color.Bisque;

            TableCell tc_FF = new TableCell();
            tc_FF.BorderStyle = BorderStyle.Solid;
            tc_FF.BorderWidth = 1;
            tc_FF.Width = 600;
            Literal lit_FF = new Literal();
            lit_FF.Text = "<center>Field Force Name</center>";
            tc_FF.Controls.Add(lit_FF);
            tr_header.Cells.Add(tc_FF);

            TableCell tc_pending = new TableCell();
            tc_pending.BorderStyle = BorderStyle.Solid;
            tc_pending.BorderWidth = 1;
            tc_pending.Width = 600;
            Literal lit_pending = new Literal();
            lit_pending.Text = "<center>Approval Pending Dates</center>";
            tc_pending.Controls.Add(lit_pending);
            tr_header.Cells.Add(tc_pending);

            TableCell tc_approve = new TableCell();
            tc_approve.BorderStyle = BorderStyle.Solid;
            tc_approve.BorderWidth = 1;
            tc_approve.Width = 600;
            Literal lit_approve = new Literal();
            lit_approve.Text = "<center>Approval by</center>";
            tc_approve.Controls.Add(lit_approve);
            tr_header.Cells.Add(tc_approve);

            tbl.Rows.Add(tr_header);

            // Details Section
            DCR dc = new DCR();

            string sURL = string.Empty;
            int iCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                sPending = "";

                TableRow tr_det = new TableRow();
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;

                tc_det_SNo.Controls.Add(lit_det_SNo);
                //tc_det_SNo.Height = 10;
                tr_det.Cells.Add(tc_det_SNo);
                //tr_det.Height = 10;

                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.HorizontalAlign = HorizontalAlign.Left;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                dsDCR = dc.get_dcr_pending_approval(drFF["sf_code"].ToString(), cmonth, cyear);

                foreach (DataRow drSF in dsDCR.Tables[0].Rows)
                {
                    sPending = sPending + drSF["pending_date"].ToString() + " , ";
                }

                if (sPending.Length > 0)
                    sPending = sPending.Substring(0, sPending.Length - 2);

                TableCell tc_det_pending = new TableCell();
                Literal lit_det_pending = new Literal();
                lit_det_pending.Text = "&nbsp;" + sPending;
                tc_det_pending.BorderStyle = BorderStyle.Solid;
                tc_det_pending.BorderWidth = 1;
                tc_det_pending.Controls.Add(lit_det_pending);
                tr_det.Cells.Add(tc_det_pending);

                TableCell tc_det_approve = new TableCell();
                Literal lit_det_approve = new Literal();
                lit_det_approve.Text = "&nbsp;" + drFF["approved_by"].ToString();
                tc_det_approve.BorderStyle = BorderStyle.Solid;
                tc_det_approve.HorizontalAlign = HorizontalAlign.Left;
                tc_det_approve.BorderWidth = 1;
                tc_det_approve.Controls.Add(lit_det_approve);
                tr_det.Cells.Add(tc_det_approve);

                tbl.Rows.Add(tr_det);                
            }
            lblNoRecord.Visible = false;
        }
        else
        {
            lblNoRecord.Visible = true;
        }
    }


    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
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
        string FileName = this.Page.Title;
        string attachment = "attachment; filename=" + FileName + ".xls";
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
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = this.Page.Title;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        form1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);
        iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }
}