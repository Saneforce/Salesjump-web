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

public partial class Reports_DCRCount_rptDCRCount : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string Month = string.Empty;
    string Year = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;

    int fldwrk_total = 0;
    string strDate = string.Empty;
    string iPendingDate = string.Empty;
    int doctor_total = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    double dblCoverage = 0.00;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_DcrCount = string.Empty;
    string strLastDCRDate = string.Empty;
    string tot_DcrPendingDate = string.Empty;
    string tot_dr = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string StrVacant = string.Empty;
    string strFieledForceName = string.Empty;
    DataSet dsUserList = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Request.QueryString["div_Code"].ToString();
        //sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        Month = Request.QueryString["Month"].ToString().Trim();
        Year = Request.QueryString["Year"].ToString(); 
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        StrVacant = Request.QueryString["StrVacant"].ToString();

        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(Month);

        lblHead.Text = "DCR Count Report for the Period of " + strFrmMonth + " / " + Year;

        LblForceName.Text = "Field Force Name :  " + strFieledForceName;      

        FillSF();
        Exportbutton();
    }

    private void Exportbutton()
    {
        btnExcel.Visible = true;
        btnPDF.Visible = false;
        btnPrint.Visible = true;
        btnClose.Visible = true;
    }

    private void FillSF()
    {
        tbl.Rows.Clear();
        doctor_total = 0;

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();

        //dsSalesForce = sf.UserList(div_code, sf_code);
        dsSalesForce = sf.UserList_Self_Vacant(div_code, sf_code, StrVacant);

        if (sf_code != "admin")
        {
            dsSalesForce.Tables[0].Rows[0].Delete();
            dsSalesForce.Tables[0].Rows[1].Delete();
            dsSalesForce.AcceptChanges();
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.Attributes.Add("Class", "tblCellFont");
            tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.Style.Add("font-weight", "bold");
            tc_SNo.Attributes.Add("Class", "tr_det_head");
            tc_SNo.Controls.Add(lit_SNo);
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 1;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_UserName = new TableCell();
            tc_DR_UserName.BorderStyle = BorderStyle.Solid;
            tc_DR_UserName.BorderWidth = 1;
            tc_DR_UserName.Width = 100;
            tc_DR_UserName.RowSpan = 1;
            Literal lit_DR_UserName = new Literal();
            lit_DR_UserName.Text = "<center>User Name</center>";
            tc_DR_UserName.Style.Add("font-weight", "bold");
            tc_DR_UserName.Attributes.Add("Class", "tr_det_head");
            tc_DR_UserName.Controls.Add(lit_DR_UserName);
            tr_header.Cells.Add(tc_DR_UserName);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 500;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center> Field Force Name </center>";
            tc_DR_Name.Style.Add("font-weight", "bold");
            tc_DR_Name.Attributes.Add("Class", "tr_det_head");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 100;
            tc_DR_HQ.RowSpan = 1;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center> HQ </center>";
            tc_DR_HQ.Style.Add("font-weight", "bold");
            tc_DR_HQ.Attributes.Add("Class", "tr_det_head");
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_DR_Designation = new TableCell();
            tc_DR_Designation.BorderStyle = BorderStyle.Solid;
            tc_DR_Designation.BorderWidth = 1;
            tc_DR_Designation.Width = 100;
            tc_DR_Designation.RowSpan = 1;
            Literal lit_DR_Designation = new Literal();
            lit_DR_Designation.Text = "<center> Designation </center>";
            tc_DR_Designation.Style.Add("font-weight", "bold");
            tc_DR_Designation.Attributes.Add("Class", "tr_det_head");
            tc_DR_Designation.Controls.Add(lit_DR_Designation);
            tr_header.Cells.Add(tc_DR_Designation);

            TableCell tc_DR_LastDCR_Date = new TableCell();
            tc_DR_LastDCR_Date.BorderStyle = BorderStyle.Solid;
            tc_DR_LastDCR_Date.BorderWidth = 1;
            tc_DR_LastDCR_Date.Width = 100;
            tc_DR_LastDCR_Date.RowSpan = 1;
            Literal lit_DR_LastDCR_Date = new Literal();
            lit_DR_LastDCR_Date.Text = "<center> Last DCR Date </center>";
            tc_DR_LastDCR_Date.Style.Add("font-weight", "bold");
            tc_DR_LastDCR_Date.Attributes.Add("Class", "tr_det_head");
            tc_DR_LastDCR_Date.Controls.Add(lit_DR_LastDCR_Date);
            tr_header.Cells.Add(tc_DR_LastDCR_Date);

            TableCell tc_DR_DCRCount = new TableCell();
            tc_DR_DCRCount.BorderStyle = BorderStyle.Solid;
            tc_DR_DCRCount.BorderWidth = 1;
            tc_DR_DCRCount.Width = 100;
            tc_DR_DCRCount.RowSpan = 1;
            Literal lit_DR_DCRCount = new Literal();
            lit_DR_DCRCount.Text = "<center> DCR Count </center>";
            tc_DR_DCRCount.Style.Add("font-weight", "bold");
            tc_DR_DCRCount.Attributes.Add("Class", "tr_det_head");
            tc_DR_DCRCount.Controls.Add(lit_DR_DCRCount);
            tr_header.Cells.Add(tc_DR_DCRCount);

            TableCell tc_DR_DOJ = new TableCell();
            tc_DR_DOJ.BorderStyle = BorderStyle.Solid;
            tc_DR_DOJ.BorderWidth = 1;
            tc_DR_DOJ.Width = 100;
            tc_DR_DOJ.RowSpan = 1;
            Literal lit_DR_DOJ = new Literal();
            lit_DR_DOJ.Text = "<center>Approval Pending Dates</center>";
            tc_DR_DOJ.Style.Add("font-weight", "bold");
            tc_DR_DOJ.Attributes.Add("Class", "tr_det_head");
            tc_DR_DOJ.Controls.Add(lit_DR_DOJ);
            tr_header.Cells.Add(tc_DR_DOJ);

            TableCell tc_DR_ApprovalBy = new TableCell();
            tc_DR_ApprovalBy.BorderStyle = BorderStyle.Solid;
            tc_DR_ApprovalBy.BorderWidth = 1;
            tc_DR_ApprovalBy.Width = 300;
            tc_DR_ApprovalBy.RowSpan = 1;
            Literal lit_DR_Approvalby = new Literal();
            lit_DR_Approvalby.Text = "<center> Approval By </center>";
            tc_DR_ApprovalBy.Style.Add("font-weight", "bold");
            tc_DR_ApprovalBy.Attributes.Add("Class", "tr_det_head");
            tc_DR_ApprovalBy.Controls.Add(lit_DR_Approvalby);
            tr_header.Cells.Add(tc_DR_ApprovalBy);


            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(Year)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            //int cmonth = Convert.ToInt32(FMonth);
            //int cyear = Convert.ToInt32(Year);

            Doctor dr = new Doctor();
            dsDoctor = dr.getDocCat(div_code);

            DataSet dsWorkType = new DataSet();
            DCR dcr = new DCR();
            TableRow tr_lst_det = new TableRow();
            tr_lst_det.Attributes.Add("Class", "tblCellFont");
        
            tbl.Rows.Add(tr_header);
        }
            //Sub Header
            //months = Convert.ToInt16(ViewState["months"].ToString());
            //cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            //cyear = Convert.ToInt16(ViewState["cyear"].ToString());           

            // Details Section

            string sURL = string.Empty;
            int iCount = 0;
            int iCnt = 0;
            DCR dcs = new DCR();

            
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                ListedDR lstDR = new ListedDR();
                iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                TableRow tr_det = new TableRow();
                tr_det.Attributes.Add("Class", "tblCellFont");
                tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["des_color"].ToString());
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
               
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Width = 50;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);

                TableCell tc_det_doc_code = new TableCell();
                Literal lit_det_doc_code = new Literal();
                lit_det_doc_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                tc_det_doc_code.BorderStyle = BorderStyle.Solid;
                tc_det_doc_code.BorderWidth = 1;
                tc_det_doc_code.Controls.Add(lit_det_doc_code);
                tc_det_doc_code.Visible = false;
                tr_det.Cells.Add(tc_det_doc_code);

                TableCell tc_det_doc_name = new TableCell();
                //Literal lit_det_doc_name = new Literal();
                //lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                HyperLink lit_det_doc_name = new HyperLink();
                lit_det_doc_name.Text = "&nbsp;" + drFF["Sf_UserName"].ToString();
                tc_det_doc_name.HorizontalAlign = HorizontalAlign.Left;
                tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                tc_det_doc_name.BorderWidth = 1;
                tc_det_doc_name.Width = 100;
                tc_det_doc_name.Controls.Add(lit_det_doc_name);
                tr_det.Cells.Add(tc_det_doc_name);

                TableCell tc_det_SF_Name = new TableCell();
                //Literal lit_det_doc_name = new Literal();
                //lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                HyperLink lit_det_SF_Name = new HyperLink();
                lit_det_SF_Name.Text = "&nbsp;" + drFF["Sf_Name"].ToString();
                tc_det_SF_Name.HorizontalAlign = HorizontalAlign.Left;
                tc_det_SF_Name.BorderStyle = BorderStyle.Solid;
                tc_det_SF_Name.BorderWidth = 1;
                tc_det_SF_Name.Width = 500;
                tc_det_SF_Name.Controls.Add(lit_det_SF_Name);
                tr_det.Cells.Add(tc_det_SF_Name);

                TableCell tc_det_sf_HQ = new TableCell();
                Literal lit_det_sf_hq = new Literal();
                lit_det_sf_hq.Text = "&nbsp;" + drFF["Sf_HQ"].ToString();
                tc_det_sf_HQ.HorizontalAlign = HorizontalAlign.Left;
                tc_det_sf_HQ.Width = 100;
                tc_det_sf_HQ.BorderStyle = BorderStyle.Solid;
                tc_det_sf_HQ.BorderWidth = 1;
                tc_det_sf_HQ.Controls.Add(lit_det_sf_hq);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_HQ);

                TableCell tc_det_sf_des = new TableCell();
                Literal lit_det_sf_des = new Literal();
                lit_det_sf_des.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                tc_det_sf_des.BorderStyle = BorderStyle.Solid;
                tc_det_sf_des.Width = 100;
                tc_det_sf_des.BorderWidth = 1;
                tc_det_sf_des.Controls.Add(lit_det_sf_des);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_des);

                tot_dr = "";
                tot_doc_met = "";
                tot_doc_calls_seen = "";
                strDate = "";

                fldwrk_total = 0;
                doctor_total = 0;
                doc_met_total = 0;
                doc_calls_seen_total = 0;
                iPendingDate = "";
                dblCoverage = 0.00;
                dblaverage = 0.00;

                dsDoc = dcs.Get_LastDCRDate(drFF["sf_code"].ToString(), div_code);

                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    strLastDCRDate = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    strDate = strDate + strLastDCRDate;
                }

                TableCell tc_det_LastDCR = new TableCell();
                Literal lit_det_LastDCR = new Literal();
                lit_det_LastDCR.Text = "&nbsp;" + strDate;
                tc_det_LastDCR.BorderStyle = BorderStyle.Solid;
                tc_det_LastDCR.Width = 100;
                tc_det_LastDCR.BorderWidth = 1;
                tc_det_LastDCR.HorizontalAlign = HorizontalAlign.Center;
                tc_det_LastDCR.VerticalAlign = VerticalAlign.Middle;
                tc_det_LastDCR.Controls.Add(lit_det_LastDCR);
                tr_det.Cells.Add(tc_det_LastDCR);

                    dsDoc = dcs.DCR_TotalSubDaysQuery(drFF["sf_code"].ToString(), div_code, Convert.ToInt16(Month), Convert.ToInt16(Year));

                    if (dsDoc.Tables[0].Rows.Count > 0)
                        tot_DcrCount = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    fldwrk_total = fldwrk_total + Convert.ToInt16(tot_DcrCount);

                    TableCell tc_det_sf_FLDWRK = new TableCell();
                    Literal lit_det_sf_FLDWRK = new Literal();
                    lit_det_sf_FLDWRK.Text = "&nbsp;" + fldwrk_total.ToString();
                    tc_det_sf_FLDWRK.BorderStyle = BorderStyle.Solid;
                    tc_det_sf_FLDWRK.BorderWidth = 1;
                    tc_det_sf_FLDWRK.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_sf_FLDWRK.VerticalAlign = VerticalAlign.Middle;
                    tc_det_sf_FLDWRK.Controls.Add(lit_det_sf_FLDWRK);
                    tr_det.Cells.Add(tc_det_sf_FLDWRK);

                    dsDoc = dcs.get_dcr_Pending_date(drFF["sf_code"].ToString(), Convert.ToInt16(Month), Convert.ToInt16(Year));
                    for (int i=0;i<dsDoc.Tables[0].Rows.Count ;i++)
                    {
                        tot_DcrPendingDate = dsDoc.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                        int iPendingCount =0;
                        iPendingCount += 1;
                        lblDcrCount.Text ="DCR Count : " +iPendingCount;

                        iPendingDate += Convert.ToInt16(tot_DcrPendingDate.Substring(0, 2)) + ", ";
                    }

                    TableCell tc_det_Pending_date = new TableCell();
                    Literal lit_det_Pending_date = new Literal();
                    lit_det_Pending_date.Text = "&nbsp;" + iPendingDate;
                    tc_det_Pending_date.BorderStyle = BorderStyle.Solid;
                    tc_det_Pending_date.BorderWidth = 1;
                    //tc_det_Pending_date.Width = 50;
                    tc_det_Pending_date.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_Pending_date.VerticalAlign = VerticalAlign.Middle;
                    tc_det_Pending_date.Controls.Add(lit_det_Pending_date);
                    tr_det.Cells.Add(tc_det_Pending_date);                   

                    TableCell tc_det_Approvalby = new TableCell();
                    Literal lit_det_Approvalby = new Literal();
                    lit_det_Approvalby.Text = drFF["Reporting_To_SF"].ToString();
                    tc_det_Approvalby.BorderStyle = BorderStyle.Solid;
                    tc_det_Approvalby.HorizontalAlign = HorizontalAlign.Left;
                    tc_det_Approvalby.BorderWidth = 1;
                    //tc_det_Approvalby.Width = 50;                  
                 
                    tc_det_Approvalby.Controls.Add(lit_det_Approvalby);
                    tr_det.Cells.Add(tc_det_Approvalby);

                    tbl.Rows.Add(tr_det);               
            
        }
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
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