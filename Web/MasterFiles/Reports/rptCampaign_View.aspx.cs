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
using System.Web.UI.DataVisualization.Charting;

public partial class Reports_rptCampaign_View : System.Web.UI.Page
{
    DataSet dsRP = null;
    DataSet dsSalesForce = null;
    DataSet dsListedDR = null;
    string sf_code = string.Empty;
    string sf_name = string.Empty;
    string camp_code = string.Empty;
    string strsf_code = string.Empty;
    string div_code = string.Empty;
    DataTable dtRP;
    DataSet dsSfMR = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        CreateDynamicTable();
    }

    private void CreateDynamicTable()
    {
        int iCount = 0;
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        //sf_code = Request.QueryString["strSf_Code"].ToString();
        sf_name = Request.QueryString["Sf_Name"].ToString();
        if (Session["Sf_Code_multiple"].ToString() != null)
        {
            sf_code = Session["Sf_Code_multiple"].ToString();
        }
        else
        {
            sf_code = "" + Request.QueryString["sf_code"].ToString() + "";
        }
        if (Request.QueryString["sf_code"] != null)
        {
            //sf_code = "'" + dataRow["sf_code"].ToString() + "'";
            sf_code = "" + Request.QueryString["sf_code"].ToString() + "";

        }
        else
        {
            //sf_code = "-1";
            sf_code = Session["Sf_Code_multiple"].ToString();
        }

        if (Request.QueryString["camp_code"] != null)
        {
            camp_code = Request.QueryString["camp_code"].ToString().Trim();

        }
        else
        {
            camp_code = "-1";
        }

        ListedDR lst = new ListedDR();
        if (camp_code == "-1")
        {
            dsListedDR = lst.FetchCampName(div_code);
        }
        else
        {
            dsListedDR = lst.FetchCamp_Name(camp_code, div_code);
        }
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {


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

            foreach (DataRow drFF in dsListedDR.Tables[0].Rows)
            {
                iCount = 0;

                Table tbldetail_main3 = new Table();
                tbldetail_main3.HorizontalAlign = HorizontalAlign.Center;
                tbldetail_main3.BorderStyle = BorderStyle.None;
                tbldetail_main3.Width = 600;
                TableRow tr_det_head_main3 = new TableRow();
                TableCell tc_det_head_main3 = new TableCell();
                tc_det_head_main3.Style.Add("font-family", "Calibri");
                tc_det_head_main3.Style.Add("font-size", "10pt");
                tc_det_head_main3.Width = 80;
                Literal lit_det_main3 = new Literal();
                lit_det_main3.Text = "<b>Campaign Name: " + "&nbsp;" + drFF["Doc_SubCatName"].ToString() + "</b>";
                tc_det_head_main3.Controls.Add(lit_det_main3);
                tr_det_head_main3.Cells.Add(tc_det_head_main3);
                tr_det_head_main3.HorizontalAlign = HorizontalAlign.Center;
                tbldetail_main3.Rows.Add(tr_det_head_main3);
                form1.Controls.Add(tbldetail_main3);

                //Table tbl_head_empty = new Table();
                //tbl_head_empty.HorizontalAlign = HorizontalAlign.Center;
                //TableRow tr_head_empty = new TableRow();
                //TableCell tc_head_empty = new TableCell();
                //tc_head_empty.Style.Add("font-family", "Calibri");
                //tc_head_empty.Style.Add("font-size", "10pt");
                //Literal lit_head_empty = new Literal();
                //lit_head_empty.Text = "<BR>";
                //tc_head_empty.Controls.Add(lit_head_empty);
                //tr_head_empty.Cells.Add(tc_head_empty);
                //tbl_head_empty.Rows.Add(tr_head_empty);
                //form1.Controls.Add(tbl_head_empty);

                Table tbl = new Table();
                tbl.HorizontalAlign = HorizontalAlign.Center;
                tbl.BorderStyle = BorderStyle.Solid;
                tbl.BorderWidth = 1;
                tbl.GridLines = GridLines.Both;
                tbl.Width = 600;

                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

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

                TableCell tc_sf = new TableCell();
                tc_sf.BorderStyle = BorderStyle.Solid;
                tc_sf.BorderWidth = 1;
                //tc_FF.Width = 400;
                tc_sf.BorderColor = System.Drawing.Color.Black;
                tc_sf.Style.Add("font-family", "Calibri");
                tc_sf.Style.Add("font-size", "10pt");

                Literal lit_sf = new Literal();
                lit_sf.Text = "<center><b>Field Force Name</b></center>";
                tc_sf.Controls.Add(lit_sf);
                tr_header.Cells.Add(tc_sf);

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
                SalesForce sf1 = new SalesForce();
                if (Session["Sf_Code_multiple"].ToString().Contains("MR"))
                {
                  //  dsSfMR = sf1.UserList_getMR_Multiple_camp(div_code, sf_code);
                 //   sf_code = Session["Sf_Code_multiple"].ToString();
                    dsSfMR = sf1.get_sf_code(Session["Sf_Code_multiple"].ToString());
                }
                 
                else  if (Session["Sf_Code_multiple"].ToString() != null )
                {
                    dsSfMR = sf1.UserList_getMR_Multiple_camp(div_code, sf_code);
                }
                else
                {
                    dsSfMR = sf1.SalesForceList_camp(div_code, sf_code);
                }

                Doctor dr_cat = new Doctor();

                foreach (DataRow dr in dsSfMR.Tables[0].Rows)
                {

                    Doctor camp = new Doctor();

                    dtRP = camp.getCamp_list(dr["sf_code"].ToString(), drFF["Doc_SubCatCode"].ToString());


                    if (dtRP.Rows.Count != 0)
                    {
                        foreach (DataRow drRP in dtRP.Rows)
                        {
                            iCount += 1;
                            TableRow tr_det = new TableRow();
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

                            TableCell tc_sf_det = new TableCell();
                            tc_sf_det.BorderStyle = BorderStyle.Solid;
                            tc_sf_det.BorderWidth = 1;
                            //tc_FF.Width = 400;
                            tc_sf_det.BorderColor = System.Drawing.Color.Black;
                            tc_sf_det.Style.Add("font-family", "Calibri");
                            tc_sf_det.Style.Add("font-size", "10pt");

                            Literal lit_sf_det = new Literal();
                            lit_sf_det.Text = "&nbsp;" + drRP["sf_name"].ToString();
                            tc_sf_det.Controls.Add(lit_sf_det);
                            tr_det.Cells.Add(tc_sf_det);


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

                   

                            tbl.Rows.Add(tr_det);

                        }
                    }

                    form1.Controls.Add(tbl);

                 

                }
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
        string attachment = "attachment; filename=MRStatus.xls";
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