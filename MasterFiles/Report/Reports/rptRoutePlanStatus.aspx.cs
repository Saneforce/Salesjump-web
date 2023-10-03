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

public partial class Reports_rptRoutePlanStatus : System.Web.UI.Page
{
    string sf_code = string.Empty;
    string sf_name = string.Empty;
    string div_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsrp = null;
    DataSet dsSf = null;
    DataSet dsSalesForce = null;
    DataSet dsTerritory = null;
    int iCount = 0;
    int iCntTerr =0;
    int iCnt = 0;
    int iCntMissDrs = 0;
    int iCntAllDrs = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        sf_name = Request.QueryString["sf_name"].ToString();
        SalesForce sf = new SalesForce();
        
        dsSf = sf.CheckSFType(sf_code);
        if (dsSf.Tables[0].Rows.Count > 0)
        {
            sf_type = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
          
        lblHead.Text = lblHead.Text  + sf_name;
        FillPlan();
        GetWorkName();
        Exportbutton();
    }

    private void Exportbutton()
    {
        btnPDF.Visible = false;
    }
    private void GetWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            lblHead.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " Status for  " + sf_name;
        }

    }
    private void FillPlan()
    {
        tbl.HorizontalAlign = HorizontalAlign.Center;
        tbl.Style.Add("font-family", "Calibri");
        tbl.Style.Add("font-size", "10pt");
        tbl.Width = 1000;
        SalesForce sf = new SalesForce();
        if (sf_type == "1")
        {
            
            dsSalesForce = sf.getSfName_HQ(sf_code);
        }
        else
        {
            dsSalesForce = sf.UserList_getMR(div_code, sf_code);
        }
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {

            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            //tr_header.Attributes.Add("Class", "mGrid");
            tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");   
            tr_header.ForeColor = System.Drawing.Color.White;
            tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.Style.Add("font-family", "Calibri");
            tc_SNo.Style.Add("font-size", "10pt");       
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "<center><b>S.No</b></center>";
            tc_SNo.Controls.Add(lit_SNo);
            tr_header.Cells.Add(tc_SNo);


            TableCell tc_SF_Name = new TableCell();
            tc_SF_Name.BorderStyle = BorderStyle.Solid;
            tc_SF_Name.BorderWidth = 1;
            tc_SF_Name.Width = 200;
            tc_SF_Name.Style.Add("font-family", "Calibri");
            tc_SF_Name.Style.Add("font-size", "10pt");            
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center><b>Field Force</b></center>";
            tc_SF_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_SF_Name);

            TableCell tc_SF_HQ = new TableCell();
            tc_SF_HQ.BorderStyle = BorderStyle.Solid;
            tc_SF_HQ.BorderWidth = 1;
            tc_SF_HQ.Width = 150;
            tc_SF_HQ.Style.Add("font-family", "Calibri");
            tc_SF_HQ.Style.Add("font-size", "10pt");            
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center><b>HQ</b></center>";
            tc_SF_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_SF_HQ);
            tc_SF_HQ.Style.Add("font-family", "Calibri");
            tc_SF_HQ.Style.Add("font-size", "10pt");
       
            TableCell tc_SF_TotDr = new TableCell();
            tc_SF_TotDr.BorderStyle = BorderStyle.Solid;
            tc_SF_TotDr.BorderWidth = 1;
            tc_SF_TotDr.Width = 90;
            tc_SF_TotDr.Style.Add("font-family", "Calibri");
            tc_SF_TotDr.Style.Add("font-size", "10pt");            
            Literal lit_DR_TotDr = new Literal();
            lit_DR_TotDr.Text = "<center><b>Total Drs</b></center>";
            tc_SF_TotDr.Controls.Add(lit_DR_TotDr);
            tr_header.Cells.Add(tc_SF_TotDr);

            TableCell tc_SF_NoPlans = new TableCell();
            tc_SF_NoPlans.BorderStyle = BorderStyle.Solid;
            tc_SF_NoPlans.BorderWidth = 1;
            tc_SF_NoPlans.Width = 90;
            tc_SF_NoPlans.Style.Add("font-family", "Calibri");
            tc_SF_NoPlans.Style.Add("font-size", "10pt");
            tc_SF_NoPlans.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_NoPlans = new Literal();
            lit_DR_NoPlans.Text = "<center><b>No of Plans</b></center>";
            tc_SF_NoPlans.Controls.Add(lit_DR_NoPlans);
            tr_header.Cells.Add(tc_SF_NoPlans);
            tc_SF_NoPlans.Style.Add("font-family", "Calibri");
            tc_SF_NoPlans.Style.Add("font-size", "10pt");
       
            TableCell tc_SF_AlCDrs = new TableCell();
            tc_SF_AlCDrs.BorderStyle = BorderStyle.Solid;
            tc_SF_AlCDrs.BorderWidth = 1;
            tc_SF_AlCDrs.Width = 90;
            tc_SF_AlCDrs.Style.Add("font-family", "Calibri");
            tc_SF_AlCDrs.Style.Add("font-size", "10pt");
            tc_SF_AlCDrs.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_AlCDrs = new Literal();
            lit_DR_AlCDrs.Text = "<center><b>Allocated Drs</b></center>";
            tc_SF_AlCDrs.Controls.Add(lit_DR_AlCDrs);
            tr_header.Cells.Add(tc_SF_AlCDrs);
           
            TableCell tc_SF_NotAlCDrs = new TableCell();
            tc_SF_NotAlCDrs.BorderStyle = BorderStyle.Solid;
            tc_SF_NotAlCDrs.BorderWidth = 1;
            tc_SF_NotAlCDrs.Width = 90;
            tc_SF_NotAlCDrs.Style.Add("font-family", "Calibri");
            tc_SF_NotAlCDrs.Style.Add("font-size", "10pt");
            tc_SF_NotAlCDrs.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_NotAlCDrs = new Literal();
            lit_DR_NotAlCDrs.Text = "<center><b>Not Allocated Drs</b></center>";
            tc_SF_NotAlCDrs.Controls.Add(lit_DR_NotAlCDrs);
            tr_header.Cells.Add(tc_SF_NotAlCDrs);

            tbl.Rows.Add(tr_header);
          
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Width = 50;
                tc_det_SNo.Style.Add("font-family", "Calibri");
                tc_det_SNo.Style.Add("font-size", "10pt");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
               
                TableCell tc_det_doc_name = new TableCell();
                Literal lit_det_doc_name = new Literal();
                lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                tc_det_doc_name.HorizontalAlign = HorizontalAlign.Left;
                tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                tc_det_doc_name.BorderWidth = 1;
                //tc_det_doc_name.Width = 700;
                tc_det_SNo.Style.Add("font-family", "Calibri");
                tc_det_SNo.Style.Add("font-size", "10pt");
                tc_det_doc_name.Controls.Add(lit_det_doc_name);
                tr_det.Cells.Add(tc_det_doc_name);

                TableCell tc_det_sf_HQ = new TableCell();
                Literal lit_det_sf_hq = new Literal();
                lit_det_sf_hq.Text = "&nbsp;" + drFF["Sf_HQ"].ToString();
                tc_det_sf_HQ.HorizontalAlign = HorizontalAlign.Left;
                tc_det_sf_HQ.BorderStyle = BorderStyle.Solid;
                tc_det_sf_HQ.BorderWidth = 1;
                tc_det_sf_HQ.Style.Add("font-family", "Calibri");
                tc_det_sf_HQ.Style.Add("font-size", "10pt");
                tc_det_sf_HQ.Controls.Add(lit_det_sf_hq);
                tr_det.Cells.Add(tc_det_sf_HQ);

                // Total Drs
                ListedDR lstDR = new ListedDR();
                iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                TableCell tc_det_totdrs = new TableCell();
                Literal lit_det_totdrs = new Literal();
                lit_det_totdrs.Text = "<center>" + iCnt.ToString() + "</center>";
                tc_det_totdrs.HorizontalAlign = HorizontalAlign.Center;
                tc_det_totdrs.BorderStyle = BorderStyle.Solid;
                tc_det_totdrs.BorderWidth = 1;
                tc_det_totdrs.Width = 50;
                tc_det_totdrs.Style.Add("font-family", "Calibri");
                tc_det_totdrs.Style.Add("font-size", "10pt");
                tc_det_totdrs.Controls.Add(lit_det_totdrs);
                tr_det.Cells.Add(tc_det_totdrs);

                //No of Plans 
                Territory Ter = new Territory();
                iCntTerr = Ter.RecordCount(drFF["sf_code"].ToString());
                TableCell tc_det_terr = new TableCell();
                Literal lit_det_totterr = new Literal();
                lit_det_totterr.Text = "<center>" + iCntTerr.ToString() + "</center>";
                tc_det_terr.HorizontalAlign = HorizontalAlign.Center;
                tc_det_terr.BorderStyle = BorderStyle.Solid;
                tc_det_terr.BorderWidth = 1;
                tc_det_terr.Width = 50;
                tc_det_terr.Style.Add("font-family", "Calibri");
                tc_det_terr.Style.Add("font-size", "10pt");
                tc_det_terr.Controls.Add(lit_det_totterr);
                tr_det.Cells.Add(tc_det_terr);

                //Allocated Drs
                RoutePlan rc = new RoutePlan();
                iCntAllDrs = rc.getAllocatedDrCnt(drFF["sf_code"].ToString());

                TableCell tc_det_AlloDrs = new TableCell();
                Literal lit_det_AlloDrs = new Literal();
                lit_det_AlloDrs.Text = "<center>" + iCntAllDrs.ToString() + "</center>";
                tc_det_AlloDrs.HorizontalAlign = HorizontalAlign.Center;
                tc_det_AlloDrs.BorderStyle = BorderStyle.Solid;
                tc_det_AlloDrs.BorderWidth = 1;
                tc_det_AlloDrs.Width = 50;
                tc_det_AlloDrs.Style.Add("font-family", "Calibri");
                tc_det_AlloDrs.Style.Add("font-size", "10pt");
                tc_det_AlloDrs.Controls.Add(lit_det_AlloDrs);
                tr_det.Cells.Add(tc_det_AlloDrs);

                // Not Allocated Drs
                RoutePlan rp = new RoutePlan();
                iCntMissDrs = rp.getMissedDr(drFF["sf_code"].ToString());

                TableCell tc_det_MissDrs = new TableCell();
                Literal lit_det_MissDrs = new Literal();
                lit_det_MissDrs.Text = "<center>" + iCntMissDrs.ToString() + "</center>";
                tc_det_MissDrs.HorizontalAlign = HorizontalAlign.Center;
                tc_det_MissDrs.BorderStyle = BorderStyle.Solid;
                tc_det_MissDrs.BorderWidth = 1;
                tc_det_MissDrs.Width = 50;
                tc_det_MissDrs.Style.Add("font-family", "Calibri");
                tc_det_MissDrs.Style.Add("font-size", "10pt");
                tc_det_MissDrs.Controls.Add(lit_det_MissDrs);
                tr_det.Cells.Add(tc_det_MissDrs);

                tbl.Rows.Add(tr_det);
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
        string Export = this.Page.Title;
        string attachment = "attachment; filename=" + Export + ".xls";
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
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "rptMRStatusView";
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