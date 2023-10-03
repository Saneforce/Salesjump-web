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

public partial class Reports_rptTPStatus : System.Web.UI.Page
{
    int state_code = -1;
    int iMonth = -1;
    int iYear = -1;
    bool isVacant;
    string Option = string.Empty;
    string div_code = string.Empty;
    string sf_name = string.Empty;
    string sf_code = string.Empty;
    DataSet dsFF = null;
    DataSet dsState = null;
    string entry_date;
    string confirm_date;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        state_code = Convert.ToInt32( Request.QueryString["state_code"].ToString());
        iMonth  = Convert.ToInt32(Request.QueryString["cur_month"].ToString());
        iYear  = Convert.ToInt32(Request.QueryString["cur_year"].ToString());
        //isVacant = Convert.ToBoolean(Request.QueryString["vacant"].ToString());
        Option = Request.QueryString["Option"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        sf_name = Request.QueryString["sf_name"].ToString();       
        
        FillSalesForce();

        string strMonth = getMonthName(iMonth);

        lblHead.Text = "TP - Status for the Month of " + strMonth + " " + iYear;

        lblFieldForce.Text = "Field Force Name : " + sf_name;
        lblMonth.Text = " Month : " + strMonth.Substring(0, 3);
        lblYear.Text = " Year : " + iYear;
        
    }

    private void FillSalesForce()
    {

        SalesForce sf = new SalesForce();
        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            DataSet dsmgrsf = new DataSet();
            SalesForce ds = new SalesForce();
            DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
            if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "3")
            {
                if (Option == "1")
                {
                    if (isVacant)
                    {
                        dsFF = sf.UserList_TP_Status(div_code, state_code);
                    }
                    else
                    {
                        dsFF = sf.UserList_TP_Status_All(div_code, state_code);
                    }
                }
                else
                {
                    //dsFF = sf.sp_UserList_TP_Hierarchy(div_code, sf_code);
                    dsFF = sf.UserList_get_SelfMail(div_code, sf_code);
                }
            }
            else
            {
                DataTable dt = ds.getAuditManagerTeam(div_code, sf_code, 0);
                dsmgrsf.Tables.Add(dt);
                dsFF = dsmgrsf;
            }
        }

        if (Session["sf_type"].ToString() == "1")
        {
            dsFF = sf.sp_UserMRLogin(div_code, sf_code);
        }

        if (Session["sf_type"].ToString() == "2")
        {
            //dsFF = sf.UserList_getMR(div_code, sf_code);
            DataSet dsmgrsf = new DataSet();
            SalesForce ds = new SalesForce();
            DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
            if (DsAudit.Tables[0].Rows.Count > 0)
            {
                dsFF = sf.UserList_get_SelfMail(div_code, sf_code);
            }
            else
            {
                DataTable dt = ds.getAuditManagerTeam(div_code, sf_code, 0);
                dsmgrsf.Tables.Add(dt);
                dsFF = dsmgrsf;
            }
        }

        if (dsFF.Tables[0].Rows.Count > 0)
        {
            btnPrint.Visible = true;
            btnExcel.Visible = true;
            btnPDF.Visible = false;
            btnClose.Visible = true;
            CreateDynamicTable(dsFF);
        }
        else
        {
            TableRow tr_det = new TableRow();

            TableCell tc_det_user = new TableCell();
            Literal lit_det_user = new Literal();
            lit_det_user.Text = "No Data found for Status";
            tc_det_user.BorderStyle = BorderStyle.Solid;
            tc_det_user.Style.Add("font-weight", "bold");
            tc_det_user.Style.Add("font-size", "10pt");
            tc_det_user.BorderWidth = 1;
            tc_det_user.Controls.Add(lit_det_user);
            tr_det.Cells.Add(tc_det_user);

            tbl.Rows.Add(tr_det);
            btnPrint.Visible = false;
            btnExcel.Visible = false;
            btnPDF.Visible = false;
            btnClose.Visible = false;

        }
    }

    private void CreateDynamicTable(DataSet dsFF)
    {
        if (dsFF != null)
        {

            TourPlan tp = new TourPlan();
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
            tc_SNo.RowSpan = 2;
            tr_header.Cells.Add(tc_SNo);

            //TableCell tc_user = new TableCell();
            //tc_user.BorderStyle = BorderStyle.Solid;
            //tc_user.BorderWidth = 1;
            //tc_user.Width = 100;
            //Literal lit_user = new Literal();
            //lit_user.Text = "<center><b>User Name</b></center>";
            //tc_user.Style.Add("border-color", "Black");
            //tc_user.Controls.Add(lit_user);
            //tc_user.RowSpan = 2;
            //tr_header.Cells.Add(tc_user);

            TableCell tc_FF = new TableCell();
            tc_FF.BorderStyle = BorderStyle.Solid;
            tc_FF.BorderWidth = 1;
            tc_FF.Width = 400;
            Literal lit_FF = new Literal();
            lit_FF.Text = "<center><b>Field Force Name</b></center>";
            tc_FF.Style.Add("border-color", "Black");
            tc_FF.Controls.Add(lit_FF);
            tc_FF.RowSpan = 2;
            tr_header.Cells.Add(tc_FF);

            TableCell tc_Designation = new TableCell();
            tc_Designation.BorderStyle = BorderStyle.Solid;
            tc_Designation.BorderWidth = 1;
            tc_Designation.Width = 300;
            Literal lit_Designation = new Literal();
            lit_Designation.Text = "<center><b>Designation</b></center>";
            tc_Designation.Style.Add("border-color", "Black");
            tc_Designation.Controls.Add(lit_Designation);
            tc_Designation.RowSpan = 2;
            tr_header.Cells.Add(tc_Designation);

            TableCell tc_HQ = new TableCell();
            tc_HQ.BorderStyle = BorderStyle.Solid;
            tc_HQ.BorderWidth = 1;
            tc_HQ.Width = 300;
            Literal lit_HQ = new Literal();
            lit_HQ.Text = "<center><b>HQ</b></center>";
            tc_HQ.Style.Add("border-color", "Black");
            tc_HQ.Controls.Add(lit_HQ);
            tc_HQ.RowSpan = 2;
            tr_header.Cells.Add(tc_HQ);

            TableCell tc_tp = new TableCell();
            Literal lit_tp = new Literal();
            lit_tp.Text = "<center><b>Tour Plan</b></center>";
            tc_tp.Style.Add("border-color", "Black");
            tc_tp.Controls.Add(lit_tp);
            tc_tp.BorderStyle = BorderStyle.Solid;
            tc_tp.BorderWidth = 1;
            tc_tp.ColumnSpan = 2;
            tr_header.Cells.Add(tc_tp);

            tbl.Rows.Add(tr_header);

            TableRow tr_tp = new TableRow();

            TableCell tc_entry = new TableCell();
            tc_entry.BorderStyle = BorderStyle.Solid;
            tc_entry.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_entry.ForeColor = System.Drawing.Color.White;
            tc_entry.BorderWidth = 1;
            tc_entry.Width = 100;
            Literal lit_entry = new Literal();
            lit_entry.Text = "<center><b>Entry Date</b></center>";
            tc_entry.Style.Add("border-color", "Black");
            tc_entry.Controls.Add(lit_entry);
            tr_tp.Cells.Add(tc_entry);

            TableCell tc_confirm = new TableCell();
            tc_confirm.BorderStyle = BorderStyle.Solid;
            tc_confirm.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_confirm.ForeColor = System.Drawing.Color.White;
            tc_confirm.BorderWidth = 1;
            tc_confirm.Width = 100;
            Literal lit_confirm = new Literal();
            lit_confirm.Text = "<center><b>Confirmed Date</b></center>";
            tc_confirm.Style.Add("border-color", "Black");
            tc_confirm.Controls.Add(lit_confirm);
            tr_tp.Cells.Add(tc_confirm);

            tbl.Rows.Add(tr_tp);

            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            string sTab = string.Empty;

            foreach (DataRow drFF in dsFF.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["des_color"].ToString());
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);

                //TableCell tc_det_FF = new TableCell();
                //Literal lit_det_FF = new Literal();

                //if (drFF["sf_color"].ToString().Trim() == "Level1")
                //{
                //    //sTab = "&nbsp;";
                //}
                //else if (drFF["sf_color"].ToString().Trim() == "Level2")
                //{
                //    //sTab = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                //}
                //else if (drFF["sf_color"].ToString().Trim() == "Level3")
                //{
                //    //sTab = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                //}
                //else if (drFF["sf_color"].ToString().Trim() == "Level4")
                //{
                //    //sTab = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                //}

                //lit_det_FF.Text = "&nbsp;" + drFF["sf_username"].ToString();
                //tc_det_FF.BorderStyle = BorderStyle.Solid;
                //tc_det_FF.BorderWidth = 1;
                //tc_det_FF.Controls.Add(lit_det_FF);
                //tr_det.Cells.Add(tc_det_FF);

                //tc_det_SNo.Height = 10;

                //tr_det.Height = 10;

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
                if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == "2")
                {
                    lit_det_Designation.Text = "&nbsp;" + sTab + drFF["Designation_Short_Name"].ToString();
                }
                else
                {
                    lit_det_Designation.Text = "&nbsp;" + sTab + drFF["sf_Designation_Short_Name"].ToString();
                }
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

                TableCell tc_det_entry = new TableCell();
                Literal lit_det_entry = new Literal();
                lit_det_entry.Text = "";
                tc_det_entry.BorderStyle = BorderStyle.Solid;
                tc_det_entry.BorderWidth = 1;
                tc_det_entry.HorizontalAlign = HorizontalAlign.Center;

                TableCell tc_det_confirm = new TableCell();
                Literal lit_det_confirm = new Literal();
                lit_det_confirm.Text = "";
                tc_det_confirm.BorderStyle = BorderStyle.Solid;
                tc_det_confirm.BorderWidth = 1;
                tc_det_confirm.HorizontalAlign = HorizontalAlign.Center;


                dsState = tp.get_TP_Entry_Confirm(drFF["sf_code"].ToString(), iMonth, iYear);
                if (dsState.Tables[0].Rows.Count > 0)
                {
                    if (dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Length > 0)
                        entry_date = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    if (dsState.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Length > 0)
                        confirm_date = dsState.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

                    
                    if (entry_date.Trim() != "0")
                    {
                        lit_det_entry.Text = entry_date.ToString();
                    }
                    if (confirm_date.Trim() != "0")
                    {
                        lit_det_confirm.Text = confirm_date.ToString();
                    }

                }

                tc_det_entry.Controls.Add(lit_det_entry);
                tr_det.Cells.Add(tc_det_entry);

                tc_det_confirm.Controls.Add(lit_det_confirm);
                tr_det.Cells.Add(tc_det_confirm);

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
        string attachment = "attachment; filename=Export.xls";
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
}