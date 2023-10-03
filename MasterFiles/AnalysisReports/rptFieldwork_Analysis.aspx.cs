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
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using Bus_EReport;
using System.Net;
public partial class MasterFiles_AnalysisReports_rptFieldwork_Analysis : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    DataSet dsListedDR = null;
    DataSet dsworkwith = null;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    string strCampaign = string.Empty;
    string strCamp_code = string.Empty;
    string strFrmMonth = string.Empty;
    string strToMonth = string.Empty;
    string strdate = string.Empty;
    string strwork = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataTable dtRP = null;
    DataSet dsdes = null;
    string strFieledForceName = string.Empty;
    AdminSetup adm = new AdminSetup();
    protected void Page_Load(object sender, EventArgs e)
    {
        //div_code = Request.QueryString["div_Code"].ToString();

        div_code = Session["div_Code"].ToString();
        //sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();     
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        if (Session["sf_type"].ToString() == "1")
        {
            div_code = Session["div_code"].ToString();
            sf_code = Session["sf_code"].ToString();
        }

        //sType = Request.QueryString["Type"].ToString();
        SalesForce sf = new SalesForce();
        strFrmMonth = sf.getMonthName(FMonth);
        strToMonth = sf.getMonthName(TMonth);

        lblHead.Text = "Field Manager Work Analysis - View For " + strFrmMonth + " " + FYear;
        LblForceName.Text = "FieldForce Name : " + "<span style='color:Red'>" + strFieledForceName + "</span>";
     //   PageId.Attributes.Add("style", "background-color:Lightgrey");
      FillSalesForce();
        // FillSF();
      //  CreateDynamicTable();
      //  ExportButton();
    }
    private void FillSalesForce()
    {    

        DataSet dsmgrsf = new DataSet();
    
        DataSet dsFF = new DataSet();
        dsFF = adm.getMR_MGR_New(sf_code, div_code);
           
        CreateDynamicTable(dsFF);


    }
    private void CreateDynamicTable(DataSet dsFF)
    {
        if (dsFF != null)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tr_header.ForeColor = System.Drawing.Color.White;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "<center><b>S.No</b></center>";
            tc_SNo.Style.Add("border-color", "Black");
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.RowSpan = 1;
            tr_header.Cells.Add(tc_SNo);
         

            TableCell tc_FF = new TableCell();
            tc_FF.BorderStyle = BorderStyle.Solid;
            tc_FF.BorderWidth = 1;
            tc_FF.Width = 250;
            Literal lit_FF = new Literal();
            lit_FF.Text = "<center><b>Field Force Name</b></center>";
            tc_FF.Style.Add("border-color", "Black");
            tc_FF.Controls.Add(lit_FF);
            tc_FF.RowSpan = 1;
            tr_header.Cells.Add(tc_FF);

            TableCell tc_Designation = new TableCell();
            tc_Designation.BorderStyle = BorderStyle.Solid;
            tc_Designation.BorderWidth = 1;
            tc_Designation.Width = 120;
            Literal lit_Designation = new Literal();
            lit_Designation.Text = "<center><b>Designation</b></center>";
            tc_Designation.Style.Add("border-color", "Black");
            tc_Designation.Controls.Add(lit_Designation);
            tc_Designation.RowSpan = 1;
            tr_header.Cells.Add(tc_Designation);

            TableCell tc_HQ = new TableCell();
            tc_HQ.BorderStyle = BorderStyle.Solid;
            tc_HQ.BorderWidth = 1;
            tc_HQ.Width = 180;
            Literal lit_HQ = new Literal();
            lit_HQ.Text = "<center><b>HQ</b></center>";
            tc_HQ.Style.Add("border-color", "Black");
            tc_HQ.Controls.Add(lit_HQ);
            tc_HQ.RowSpan = 1;
            tr_header.Cells.Add(tc_HQ);


            Doctor dr = new Doctor();
            DataSet dsmgrsf = new DataSet();
            // DataTable dsDoctor = new DataTable();
            SalesForce sf1 = new SalesForce();
            //if (sf_code.Contains("MR"))
            //{
            //foreach (DataRow ds in dsFF.Tables[0].Rows)
            //{
            Designation des = new Designation();
            dsDoctor = des.getDesig_analysis(div_code);

                //dsmgrsf.Tables.Add(dt);
                //dsDoctor = dsmgrsf;
          //  }
            //}
            //else
            //{
            //    DataTable dt = sf1.getMRJointWork_camp(div_code, dsFF["sf_code"].ToString(), 0);
            //    dsmgrsf.Tables.Add(dt);
            //    dsDoctor = dsmgrsf;
            //}

                if (dsDoctor.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                    {
                        TableCell tc_catg_name = new TableCell();
                        tc_catg_name.BorderStyle = BorderStyle.Solid;
                        tc_catg_name.BorderWidth = 1;
                   
                        tc_catg_name.Width = 30;
                        tc_catg_name.Style.Add("font-family", "Calibri");
                        tc_catg_name.Style.Add("font-size", "10pt");
                        tc_catg_name.Attributes.Add("Class", "tr_det_head");
                        Literal lit_catg_name = new Literal();
                        lit_catg_name.Text = "<center>" + dataRow["Designation_Short_Name"].ToString() + "</center>";
                        //   tc_catg_name.Attributes.Add("Class", "Backcolor");
                        tc_catg_name.Controls.Add(lit_catg_name);
                        tr_header.Cells.Add(tc_catg_name);
                    }

                  //  tbl.Rows.Add(tr_catg2);
                }

            tbl.Rows.Add(tr_header);

              int iCount = 0;
            string sTab = string.Empty;
            SalesForce desig = new SalesForce ();

            foreach (DataRow drFF in dsFF.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                int cmonthdoc = Convert.ToInt32(FMonth);
                int cyeardoc = Convert.ToInt32(FYear);

                dsdes = desig.Change_Rep(drFF["sf_code"].ToString());

                if (drFF["sf_type"].ToString() == "2")
                {
                    tr_det.Attributes.Add("style", "background-color:LightBlue; font-weight:Bold; ");
                }
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);


                TableCell tc_det_user = new TableCell();
                Literal lit_det_user = new Literal();
                lit_det_user.Text = "&nbsp;" + sTab + drFF["sf_name"].ToString();
                tc_det_user.HorizontalAlign = HorizontalAlign.Left;
                tc_det_user.BorderStyle = BorderStyle.Solid;
                tc_det_user.BorderWidth = 1;
                tc_det_user.Controls.Add(lit_det_user);
                tr_det.Cells.Add(tc_det_user);

                TableCell tc_det_Designation = new TableCell();
                Literal lit_det_Designation = new Literal();

                lit_det_Designation.Text = "&nbsp;" + sTab + drFF["sf_Designation_Short_Name"].ToString();

                tc_det_Designation.HorizontalAlign = HorizontalAlign.Left;
                tc_det_Designation.BorderStyle = BorderStyle.Solid;
                tc_det_Designation.BorderWidth = 1;
                tc_det_Designation.Controls.Add(lit_det_Designation);
                tr_det.Cells.Add(tc_det_Designation);

                TableCell tc_det_HQ = new TableCell();
                Literal lit_det_HQ = new Literal();
                lit_det_HQ.Text = "&nbsp;" + sTab + drFF["sf_hq"].ToString();
                tc_det_HQ.HorizontalAlign = HorizontalAlign.Left;
                tc_det_HQ.BorderStyle = BorderStyle.Solid;
                tc_det_HQ.BorderWidth = 1;
                tc_det_HQ.Controls.Add(lit_det_HQ);
                tr_det.Cells.Add(tc_det_HQ);

                TableCell tc_det_JW = new TableCell();
                Literal lit_det_JW = new Literal();
                string sActive = string.Empty;
                DCR dcr1 = new DCR();
                //   lit_det_JW.Text = "&nbsp;" + sTab + drFF["sf_hq"].ToString();
                sActive = "";
                //if (dsDoctor.Tables[0].Rows.Count > 0)
                //{

                foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                {
                    TableCell tc_class_MR = new TableCell();
                    tc_class_MR.BorderStyle = BorderStyle.Solid;
                    tc_class_MR.BorderColor = System.Drawing.Color.Black;
                    tc_class_MR.BorderWidth = 1;
                    tc_class_MR.Style.Add("font-family", "Calibri");
                    tc_class_MR.Style.Add("font-size", "8pt");
                    tc_class_MR.Style.Add("Color", "Red");
                    Literal lit_class_MR = new Literal();
                    foreach (DataRow drow in dsdes.Tables[0].Rows)
                    {
                        if (drow["sf_code"].ToString().Contains("MGR"))
                        {
                            if (drow["sf_Designation_Short_Name"].ToString().Trim() == dataRow["Designation_Short_Name"].ToString().Trim())
                            {
                                dsworkwith = dcr1.DCR_work_JW(div_code, cmonthdoc, cyeardoc, drFF["sf_code"].ToString(), drow["sf_code"].ToString());

                              
                                
                                strdate = "";
                                if (dsworkwith.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < dsworkwith.Tables[0].Rows.Count; i++)
                                    {
                                        strdate += dsworkwith.Tables[0].Rows[i]["Activity_Date"].ToString() + ",";
                                        lit_class_MR.Text = strdate;
                                    }
                                }

                             

                            }
                            tc_class_MR.Controls.Add(lit_class_MR);
                            tr_det.Cells.Add(tc_class_MR);
                            //}
                            tbl.Rows.Add(tr_det);
                            // tbl.Rows.Add(tr_det);  
                        }
                    }
                }
            }
        }
    }
    private void ExportButton()
    {
        btnPDF.Visible = false;
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "rptTPView";
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

    public override void VerifyRenderingInServerForm(Control txt_salutaion)
    {
        /* Verifies that the control is rendered */
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
        this.FillSalesForce();
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        this.FillSalesForce();
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }

    //protected void TimerTick(object sender, EventArgs e)
    //{
    //    this.FillSalesForce();
    //    Timer1.Enabled = false;
    //    ShowProgressDiv.Visible = false;
    //    PageId.Attributes.Add("style", "background-color:#fff");
    //}
}