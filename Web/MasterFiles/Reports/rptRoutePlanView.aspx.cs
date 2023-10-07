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

public partial class Reports_rptRoutePlanView : System.Web.UI.Page
{
    DataSet dsRP = null;
    DataSet dsSalesForce = null;
    DataSet dsTerritory = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DataTable dtRP;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        CreateDynamicTable();
        GetWorkName();
        ExportData();
    }


    private void GetWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            lblHead.Text = "Listed Doctor wise - " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + " View";
        }
    }
    private void CreateDynamicTable()
    {
        int iCount = 0;
        sf_code = Request.QueryString["sf_code"].ToString();
        RoutePlan rp = new RoutePlan();
        dsSalesForce = rp.FetchTerritoryName(sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {

            Table tbldetail_main31 = new Table();
            tbldetail_main31.BorderStyle = BorderStyle.None;
            tbldetail_main31.Width = 1000;
            TableRow tr_det_head_main31 = new TableRow();
            TableCell tc_det_head_main31 = new TableCell();
            tbldetail_main31.HorizontalAlign = HorizontalAlign.Center;
            tc_det_head_main31.Style.Add("font-family", "Calibri");
            tc_det_head_main31.Style.Add("font-size", "10pt");
            tc_det_head_main31.Width = 100;
            Literal lit_det_main31 = new Literal();
            lit_det_main31.Text = "<b>Field Force Name: " + "&nbsp;" + Request.QueryString["sf_name"].ToString() + "</b>";
            tc_det_head_main31.Controls.Add(lit_det_main31);
            tr_det_head_main31.Cells.Add(tc_det_head_main31);

            DataTable dt = rp.view_RoutePlan(Request.QueryString["sf_code"].ToString());
            foreach (DataRow dr in dt.Rows)
            {
                TableCell tc_det_head_main32 = new TableCell();
                tc_det_head_main32.Style.Add("font-family", "Calibri");
                tc_det_head_main32.Style.Add("font-size", "10pt");
                tc_det_head_main32.Width = 100;
                tc_det_head_main32.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_det_main32 = new Literal();
                lit_det_main32.Text = "<b>No. Of Plans: " + "&nbsp;" + dr["cnt"].ToString() + "</b>";
                tc_det_head_main32.Controls.Add(lit_det_main32);
                tr_det_head_main31.Cells.Add(tc_det_head_main32);

                TableCell tc_det_head_main34 = new TableCell();
                tc_det_head_main34.Style.Add("font-family", "Calibri");
                tc_det_head_main34.Style.Add("font-size", "10pt");
                tc_det_head_main34.Width = 100;
                tc_det_head_main34.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_det_main34 = new Literal();
                lit_det_main34.Text = "<b>HQ: " + "&nbsp;" + dr["Sf_HQ"].ToString() + "</b>";
                tc_det_head_main34.Controls.Add(lit_det_main34);
                tr_det_head_main31.Cells.Add(tc_det_head_main34);
            }
            
            tbldetail_main31.Rows.Add(tr_det_head_main31);
            form1.Controls.Add(tbldetail_main31);

            Table tbl_head_empty3 = new Table();
            TableRow tr_head_empty3 = new TableRow();
            TableCell tc_head_empty3 = new TableCell();

            tbl_head_empty3.HorizontalAlign = HorizontalAlign.Center;
            tc_head_empty3.Style.Add("font-family", "Calibri");
            tc_head_empty3.Style.Add("font-size", "10pt");
            Literal lit_head_empty3 = new Literal();
            lit_head_empty3.Text = "<BR>";
            tc_head_empty3.Controls.Add(lit_head_empty3);
            tr_head_empty3.Cells.Add(tc_head_empty3);
            tbl_head_empty3.Rows.Add(tr_head_empty3);
            form1.Controls.Add(tbl_head_empty3);

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                iCount = 0;

                Table tbldetail_main3 = new Table();
                tbldetail_main3.HorizontalAlign = HorizontalAlign.Center;
                tbldetail_main3.BorderStyle = BorderStyle.None;
                tbldetail_main3.Width = 1000;
                TableRow tr_det_head_main3 = new TableRow();
                TableCell tc_det_head_main3 = new TableCell();
                tc_det_head_main3.Style.Add("font-family", "Calibri");
                tc_det_head_main3.Style.Add("font-size", "10pt");
                tc_det_head_main3.Width = 100;
                Literal lit_det_main3 = new Literal();
                lit_det_main3.Text = "<b>Plan Name: " + "&nbsp;" + drFF["Territory_Name"].ToString() + "</b>";
                tc_det_head_main3.Controls.Add(lit_det_main3);
                tr_det_head_main3.Cells.Add(tc_det_head_main3);
                tr_det_head_main3.HorizontalAlign = HorizontalAlign.Center;
                tbldetail_main3.Rows.Add(tr_det_head_main3);
                form1.Controls.Add(tbldetail_main3);

                Table tbl_head_empty = new Table();
                tbl_head_empty.HorizontalAlign = HorizontalAlign.Center;
                TableRow tr_head_empty = new TableRow();
                TableCell tc_head_empty = new TableCell();
                tc_head_empty.Style.Add("font-family", "Calibri");
                tc_head_empty.Style.Add("font-size", "10pt");
                Literal lit_head_empty = new Literal();
                lit_head_empty.Text = "<BR>";
                tc_head_empty.Controls.Add(lit_head_empty);
                tr_head_empty.Cells.Add(tc_head_empty);
                tbl_head_empty.Rows.Add(tr_head_empty);
                form1.Controls.Add(tbl_head_empty);

                Table tbl = new Table();
                tbl.HorizontalAlign = HorizontalAlign.Center;
                tbl.BorderStyle = BorderStyle.Solid;
                tbl.BorderWidth = 1;
                tbl.GridLines = GridLines.Both;
                tbl.Width = 1000;               

                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;
                tr_header.Attributes.Add("Class", "mGrid");

                TableCell tc_SNo = new TableCell();
                tc_SNo.BorderStyle = BorderStyle.Solid;
                tc_SNo.BorderColor = System.Drawing.Color.Black;
                tc_SNo.BorderWidth = 1;
                tc_SNo.Style.Add("font-family", "Calibri");
                tc_SNo.Style.Add("font-size", "10pt");
                Literal lit_SNo = new Literal();
                lit_SNo.Text = "<center><b>S.No</b></center>";
                tc_SNo.Controls.Add(lit_SNo);
                tr_header.Cells.Add(tc_SNo);

                TableCell tc_doc = new TableCell();
                tc_doc.BorderStyle = BorderStyle.Solid;
                tc_doc.BorderWidth = 1;
               //tc_FF.Width = 400;
                tc_doc.BorderColor = System.Drawing.Color.Black;
                tc_doc.Style.Add("font-family", "Calibri");
                tc_doc.Style.Add("font-size", "10pt");

                Literal lit_doc = new Literal();
                lit_doc.Text = "<center><b>Listed DR Name</b></center>";
                tc_doc.Controls.Add(lit_doc);
                tr_header.Cells.Add(tc_doc);

                TableCell tc_spec = new TableCell();
                tc_spec.BorderStyle = BorderStyle.Solid;
                tc_spec.BorderColor = System.Drawing.Color.Black;
                tc_spec.BorderWidth = 1;
                tc_spec.Style.Add("font-family", "Calibri");
                tc_spec.Style.Add("font-size", "10pt");

                Literal lit_spec = new Literal();
                lit_spec.Text = "<center><b>Specialty</b></center>";
                tc_spec.Controls.Add(lit_spec);
                tr_header.Cells.Add(tc_spec);

                TableCell tc_catg = new TableCell();
                tc_catg.BorderStyle = BorderStyle.Solid;
                tc_catg.BorderColor = System.Drawing.Color.Black;
                tc_catg.BorderWidth = 1;
                tc_catg.Style.Add("font-family", "Calibri");
                tc_catg.Style.Add("font-size", "10pt");

                Literal lit_catg = new Literal();
                lit_catg.Text = "<center><b>Category</b></center>";
                tc_catg.Controls.Add(lit_catg);
                tr_header.Cells.Add(tc_catg);

                TableCell tc_qual = new TableCell();
                tc_qual.BorderStyle = BorderStyle.Solid;
                tc_qual.BorderColor = System.Drawing.Color.Black;
                tc_qual.BorderWidth = 1;
                tc_qual.Style.Add("font-family", "Calibri");
                tc_qual.Style.Add("font-size", "10pt");

                Literal lit_qual = new Literal();
                lit_qual.Text = "<center><b>Qual</b></center>";
                tc_qual.Controls.Add(lit_qual);
                tr_header.Cells.Add(tc_qual);

                TableCell tc_class = new TableCell();
                tc_class.BorderStyle = BorderStyle.Solid;
                tc_class.BorderColor = System.Drawing.Color.Black;
                tc_class.BorderWidth = 1;
                tc_class.Style.Add("font-family", "Calibri");
                tc_class.Style.Add("font-size", "10pt");

                Literal lit_class = new Literal();
                lit_class.Text = "<center><b>Class</b></center>";
                tc_class.Controls.Add(lit_class);
                tr_header.Cells.Add(tc_class);

                tbl.Rows.Add(tr_header);


                dtRP = rp.get_ListedDoctor_RoutePlan(sf_code, drFF["Territory_Code"].ToString());

                if (dtRP.Rows.Count > 0)
                {
                    foreach (DataRow drRP in dtRP.Rows)
                    {
                        iCount += 1;
                        TableRow tr_det = new TableRow();
                        //tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["des_color"].ToString());
                        
                        tr_header.BorderStyle = BorderStyle.Solid;
                        tr_header.BorderWidth = 1;

                        TableCell tc_SNo_det = new TableCell();
                        tc_SNo_det.BorderStyle = BorderStyle.Solid;
                        tc_SNo_det.BorderColor = System.Drawing.Color.Black;
                        tc_SNo_det.BorderWidth = 1;
                        tc_SNo_det.Style.Add("font-family", "Calibri");
                        tc_SNo_det.Style.Add("font-size", "10pt");

                        Literal lit_SNo_det = new Literal();
                        lit_SNo_det.Text = iCount.ToString();
                        tc_SNo_det.Controls.Add(lit_SNo_det);
                        tr_det.Cells.Add(tc_SNo_det);

                        TableCell tc_doc_det = new TableCell();
                        tc_doc_det.BorderStyle = BorderStyle.Solid;
                        tc_doc_det.BorderWidth = 1;
                        //tc_FF.Width = 400;
                        tc_doc_det.BorderColor = System.Drawing.Color.Black;
                        tc_doc_det.Style.Add("font-family", "Calibri");
                        tc_doc_det.Style.Add("font-size", "10pt");

                        Literal lit_doc_det = new Literal();
                        lit_doc_det.Text = "&nbsp;" + drRP["ListedDr_Name"].ToString();
                        tc_doc_det.Controls.Add(lit_doc_det);
                        tr_det.Cells.Add(tc_doc_det);

                        TableCell tc_spec_det = new TableCell();
                        tc_spec_det.BorderStyle = BorderStyle.Solid;
                        tc_spec_det.BorderColor = System.Drawing.Color.Black;
                        tc_spec_det.BorderWidth = 1;
                        tc_spec_det.Style.Add("font-family", "Calibri");
                        tc_spec_det.Style.Add("font-size", "10pt");

                        Literal lit_spec_det = new Literal();
                        lit_spec_det.Text = "&nbsp;" + drRP["Doc_Special_SName"].ToString();
                        tc_spec_det.Controls.Add(lit_spec_det);
                        tr_det.Cells.Add(tc_spec_det);

                        TableCell tc_catg_det = new TableCell();
                        tc_catg_det.BorderStyle = BorderStyle.Solid;
                        tc_catg_det.BorderColor = System.Drawing.Color.Black;
                        tc_catg_det.BorderWidth = 1;
                        tc_catg_det.Style.Add("font-family", "Calibri");
                        tc_catg_det.Style.Add("font-size", "10pt");

                        Literal lit_catg_det = new Literal();
                        lit_catg_det.Text = "&nbsp;" + drRP["Doc_Cat_SName"].ToString();
                        tc_catg_det.Controls.Add(lit_catg_det);
                        tr_det.Cells.Add(tc_catg_det);

                        TableCell tc_qual_det = new TableCell();
                        tc_qual_det.BorderStyle = BorderStyle.Solid;
                        tc_qual_det.BorderColor = System.Drawing.Color.Black;
                        tc_qual_det.BorderWidth = 1;
                        tc_qual_det.Style.Add("font-family", "Calibri");
                        tc_qual_det.Style.Add("font-size", "10pt");

                        Literal lit_qual_det = new Literal();
                        lit_qual_det.Text = "&nbsp;" + drRP["Doc_QuaName"].ToString();
                        tc_qual_det.Controls.Add(lit_qual_det);
                        tr_det.Cells.Add(tc_qual_det);

                        TableCell tc_class_det = new TableCell();
                        tc_class_det.BorderStyle = BorderStyle.Solid;
                        tc_class_det.BorderColor = System.Drawing.Color.Black;
                        tc_class_det.BorderWidth = 1;
                        tc_class_det.Style.Add("font-family", "Calibri");
                        tc_class_det.Style.Add("font-size", "10pt");

                        Literal lit_class_det = new Literal();
                        lit_class_det.Text = "&nbsp;" + drRP["Doc_ClsSName"].ToString();
                        tc_class_det.Controls.Add(lit_class_det);
                        tr_det.Cells.Add(tc_class_det);

                        //TableCell tc_terr_name_det = new TableCell();
                        //tc_terr_name_det.BorderStyle = BorderStyle.Solid;
                        //tc_terr_name_det.BorderColor = System.Drawing.Color.Black;
                        //tc_terr_name_det.BorderWidth = 1;
                        //tc_terr_name_det.Style.Add("font-family", "Calibri");
                        //tc_terr_name_det.Style.Add("font-size", "10pt");

                        //Literal lit_terr_name_det = new Literal();
                        //lit_terr_name_det.Text = "&nbsp;" + drFF["Territory_Name"].ToString();
                        //tc_terr_name_det.Controls.Add(lit_terr_name_det);
                        //tr_det.Cells.Add(tc_terr_name_det);

                        //TableCell tc_type_det = new TableCell();
                        //tc_type_det.BorderStyle = BorderStyle.Solid;
                        //tc_type_det.BorderColor = System.Drawing.Color.Black;
                        //tc_type_det.BorderWidth = 1;
                        //tc_type_det.Style.Add("font-family", "Calibri");
                        //tc_type_det.Style.Add("font-size", "10pt");

                        //Literal lit_type_det = new Literal();
                        //lit_type_det.Text = "&nbsp;" + drFF["Territory_Cat"].ToString();
                        //tc_type_det.Controls.Add(lit_type_det);
                        //tr_det.Cells.Add(tc_type_det);

                        tbl.Rows.Add(tr_det);

                    }
                }

                form1.Controls.Add(tbl);

                Table tbl_head_empty1 = new Table();
                TableRow tr_head_empty1 = new TableRow();
                TableCell tc_head_empty1 = new TableCell();
                tc_head_empty1.Style.Add("font-family", "Calibri");
                tc_head_empty1.Style.Add("font-size", "10pt");
                Literal lit_head_empty1 = new Literal();
                lit_head_empty1.Text = "<BR>";
                tc_head_empty1.Controls.Add(lit_head_empty1);
                tr_head_empty1.Cells.Add(tc_head_empty1);
                tbl_head_empty1.Rows.Add(tr_head_empty1);
                form1.Controls.Add(tbl_head_empty1);

            }

        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //Session["ctrl"] = pnlContents;
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
        //pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
       // frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    private void ExportData()
    {
        btnClose.Visible = false;
        btnPrint.Visible = false;
        btnPDF.Visible = false;
        btnExcel.Visible = false;
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";

        Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);

        //pnlContents.RenderControl(hw);

        StringReader sr = new StringReader(sw.ToString());

        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);

        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

        pdfDoc.Open();

        htmlparser.Parse(sr);

        pdfDoc.Close();

        Response.Write(pdfDoc);

        Response.End(); 
    }
}