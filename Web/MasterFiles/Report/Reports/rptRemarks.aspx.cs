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

public partial class Reports_rptRemarks : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfName = string.Empty;
    string Year = string.Empty;
    int Month=-1;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sfName = Request.QueryString["SF_Name"].ToString();
            sfCode = Request.QueryString["sf_code"].ToString();
            Year = Request.QueryString["Year"].ToString();
            Month = Convert.ToInt32(Request.QueryString["Month"].ToString());

            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string strMonthName = mfi.GetMonthName(Month).ToString();

            lblHead.Text = "Daily Calls Report Remarks of " + sfName +" " + "for" +" " + strMonthName + " " + Year;
            BindGrid();
        }
        catch (Exception ex)
        {

        }
    }
    



    private void BindGrid()
    {
        DataSet dsRemarks = new DataSet();
        DCR Dcr=new DCR();
        dsRemarks = Dcr.get_DCRRemarks(sfCode,Month);
        if (dsRemarks.Tables[0].Rows.Count > 0)
        {
            grdRemarks.DataSource = dsRemarks;
            grdRemarks.DataBind();
            btnPrint.Visible = true;
            btnExcel.Visible = true;
            btnPDF.Visible = true;  
        }
        else
        {
            grdRemarks.DataSource = null;
            grdRemarks.DataBind();
            btnPrint.Visible = false;
            btnExcel.Visible = false;
            btnPDF.Visible = false;            
        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = Page.Title;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(hw);
        for (int col = 0; col < grdRemarks.HeaderRow.Controls.Count; col++)
        {
            TableCell tc = grdRemarks.HeaderRow.Cells[col];
            tc.Style.Add("color", "Black");           
        }
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
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strFileName = Page.Title;
        string attachment = "attachment; filename='"+ strFileName +"'.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
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
}