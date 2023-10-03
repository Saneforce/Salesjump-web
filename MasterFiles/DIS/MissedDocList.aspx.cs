﻿using System;
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

public partial class MIS_Reports_MissedDocList : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string vMode = string.Empty;
    string sf_name = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        vMode = Request.QueryString["vMode"].ToString();

         if (vMode != "")
        {
            if (sMode == "4")
            {
                lblsubhead.Text = "Category: ";
                Doctor dc = new Doctor();
                DataSet dsdc = dc.getCatgName(vMode, div_code);
                if (dsdc.Tables[0].Rows.Count > 0)
                    lblsubhead.Text = lblsubhead.Text + Convert.ToString(dsdc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                lblsubhead.Visible = true;

            }
            if (sMode == "5")
            {
                lblsubhead.Text = "Speciality: ";
                Doctor dc = new Doctor();
                DataSet dsdc = dc.getSpecName(vMode, div_code);
                if (dsdc.Tables[0].Rows.Count > 0)
                    lblsubhead.Text = lblsubhead.Text + Convert.ToString(dsdc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                lblsubhead.Visible = true;

            }
            if (sMode == "6")
            {
                lblsubhead.Text = "Class: ";
                Doctor dc = new Doctor();
                DataSet dsdc = dc.getClassName(vMode, div_code);
                if (dsdc.Tables[0].Rows.Count > 0)
                    lblsubhead.Text = lblsubhead.Text + Convert.ToString(dsdc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                lblsubhead.Visible = true;

            }
        }
        SalesForce sf = new SalesForce();
        DataSet dssf = sf.getSfName(sf_code);
        if (dssf.Tables[0].Rows.Count > 0)
            sf_name = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

        string sMonth = getMonthName(Convert.ToInt16(FMonth)) + FYear.ToString();
        if (!Page.IsPostBack)
        {
            lblHead.Text = lblHead.Text + sMonth + " - " + sf_name;
        }
        FillDoctor();
        ExportButton();
    }

    private void ExportButton()
    {
        btnPDF.Visible = false;
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
    private void FillDoctor()
    {
        DateTime dtCurrent;
        string sCurrentDate = string.Empty;

        if (Convert.ToInt16(FMonth) == 12)
        {
            sCurrentDate = "01-01-" + (Convert.ToInt16(FYear) + 1);
        }
        else
        {
            sCurrentDate = (Convert.ToInt16(FMonth) + 1) + "-01-" + Convert.ToInt16(FYear);
        }

        dtCurrent = Convert.ToDateTime(sCurrentDate);
        Doctor dc = new Doctor();
        if (vMode != "")
        {
            dsDoctor = dc.Missed_Doc(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent, sMode, vMode);
        }
        else
        {
            dsDoctor = dc.Missed_Doc(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent, sMode);
            //dsDoctor = dc.Missed_Doc(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent, sMode, vMode);
        }
      

        if (dsDoctor.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsDoctor;
            grdDoctor.DataBind();
        }

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strFileName=this.Page.Title;
        string attachment = "attachment; filename=" + strFileName + ".xls";
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
        string strFileName = "rptDCRView";
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