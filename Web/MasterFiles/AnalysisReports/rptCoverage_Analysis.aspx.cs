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

public partial class MasterFiles_AnalysisReports_rptCoverage_Analysis : System.Web.UI.Page
{
    DataSet dsDoctor = null;
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
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    string strmode = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;

   
    protected void Page_Load(object sender, EventArgs e)
    {
       // div_code = Request.QueryString["div_Code"].ToString();
        div_code = Session["div_code"].ToString();
        //sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["Fyear"].ToString();
    
        strFieledForceName = Request.QueryString["sf_name"].ToString();        
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth);
        //string strToMonth = sf.getMonthName(TMonth);
        lblHead.Text = "Coverage Analysis - " + strFrmMonth + " " + FYear ;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        FillSalesForce();
    }

     // Fetch the total rows for the table
       private void FillSalesForce()
        {

        SalesForce sf = new SalesForce();
      
            DataSet dsmgrsf = new DataSet();
            SalesForce ds = new SalesForce();
            DataSet dsFF = new DataSet();
            DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
            if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "3")
            {
                dsFF = sf.UserList_Self_Call(div_code, sf_code);               
            }
            else
            {
                DataTable dt = ds.getAuditManagerTeam(div_code, sf_code, 0);
                dsmgrsf.Tables.Add(dt);
                dsFF = dsmgrsf;
            }
            CreateDynamicTable(dsFF);
       
       
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
            lit_tp.Text = "<center><b>Call Details</b></center>";
            tc_tp.Style.Add("border-color", "Black");
            tc_tp.Controls.Add(lit_tp);
            tc_tp.BorderStyle = BorderStyle.Solid;
            tc_HQ.Width = 300;
            tc_tp.BorderWidth = 1;
            tc_tp.ColumnSpan = 5;
            tr_header.Cells.Add(tc_tp);

            TableCell tc_atten = new TableCell();
            Literal lit_atten = new Literal();
            lit_atten.Text = "<center><b>Attendance</b></center>";
            tc_atten.Style.Add("border-color", "Black");
            tc_atten.Controls.Add(lit_atten);
            tc_atten.BorderStyle = BorderStyle.Solid;
            tc_atten.Width = 300;
            tc_atten.BorderWidth = 1;
            tc_atten.ColumnSpan = 4;
            tr_header.Cells.Add(tc_atten);

            TableCell tc_Sum = new TableCell();
            Literal lit_Sum = new Literal();
            lit_Sum.Text = "<center><b>Summary</b></center>";
            tc_Sum.Style.Add("border-color", "Black");
            tc_Sum.Controls.Add(lit_Sum);
            tc_Sum.BorderStyle = BorderStyle.Solid;
            tc_Sum.Width = 300;
            tc_Sum.BorderWidth = 1;
            tc_Sum.ColumnSpan = 2;
            tr_header.Cells.Add(tc_Sum);


            TableCell tc_JWork = new TableCell();
            Literal lit_JWork = new Literal();
            lit_JWork.Text = "<center><b>Joint Work</b></center>";
            tc_JWork.Style.Add("border-color", "Black");
            tc_JWork.Controls.Add(lit_JWork);
            tc_JWork.BorderStyle = BorderStyle.Solid;
            tc_JWork.Width = 300;
            tc_JWork.BorderWidth = 1;
            tc_JWork.ColumnSpan =4;
            tr_header.Cells.Add(tc_JWork);


            tbl.Rows.Add(tr_header);

            TableRow tr_tp = new TableRow();

            TableCell tc_entry = new TableCell();
            tc_entry.BorderStyle = BorderStyle.Solid;
            tc_entry.BorderWidth = 1;
            tc_entry.Width = 100;
            Literal lit_entry = new Literal();
            lit_entry.Text = "<center><b>Master List Doctors</b></center>";
            tc_entry.Style.Add("border-color", "Black");
            tc_entry.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_entry.ForeColor = System.Drawing.Color.White;
            tc_entry.Controls.Add(lit_entry);
            tr_tp.Cells.Add(tc_entry);

            TableCell tc_confirm = new TableCell();
            tc_confirm.BorderStyle = BorderStyle.Solid;
            tc_confirm.BorderWidth = 1;
            tc_confirm.Width = 100;
            Literal lit_confirm = new Literal();
            lit_confirm.Text = "<center><b>Doctors Met</b></center>";
            tc_confirm.Style.Add("border-color", "Black");
            tc_confirm.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_confirm.ForeColor = System.Drawing.Color.White;
            tc_confirm.Controls.Add(lit_confirm);
            tr_tp.Cells.Add(tc_confirm);

            TableCell tc_cov = new TableCell();
            tc_cov.BorderStyle = BorderStyle.Solid;
            tc_cov.BorderWidth = 1;
            tc_cov.Width = 100;
            Literal lit_cov = new Literal();
            lit_cov.Text = "<center><b>Coverage (%)</b></center>";
            tc_cov.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_cov.ForeColor = System.Drawing.Color.White;
            tc_cov.Style.Add("border-color", "Black");
            tc_cov.Controls.Add(lit_cov);
            tr_tp.Cells.Add(tc_cov);

            TableCell tc_listed = new TableCell();
            tc_listed.BorderStyle = BorderStyle.Solid;
            tc_listed.BorderWidth = 1;
            tc_listed.Width = 100;
            Literal lit_listed = new Literal();
            lit_listed.Text = "<center><b>Listed Drs Missed</b></center>";
            tc_listed.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_listed.ForeColor = System.Drawing.Color.White;
            tc_listed.Style.Add("border-color", "Black");
            tc_listed.Controls.Add(lit_listed);
            tr_tp.Cells.Add(tc_listed);

            TableCell tc_Unlisted = new TableCell();
            tc_Unlisted.BorderStyle = BorderStyle.Solid;
            tc_Unlisted.BorderWidth = 1;
            tc_Unlisted.Width = 100;
            Literal lit_Unlisted = new Literal();
            lit_Unlisted.Text = "<center><b>UnListed Drs Met</b></center>";
            tc_Unlisted.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_Unlisted.ForeColor = System.Drawing.Color.White;
            tc_Unlisted.Style.Add("border-color", "Black");
            tc_Unlisted.Controls.Add(lit_Unlisted);
            tr_tp.Cells.Add(tc_Unlisted);

            TableCell tc_Days = new TableCell();
            tc_Days.BorderStyle = BorderStyle.Solid;
            tc_Days.BorderWidth = 1;
            tc_Days.Width = 100;
            Literal lit_Days = new Literal();
            lit_Days.Text = "<center><b>Days Worked</b></center>";
            tc_Days.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_Days.ForeColor = System.Drawing.Color.White;
            tc_Days.Style.Add("border-color", "Black");
            tc_Days.Controls.Add(lit_Days);
            tr_tp.Cells.Add(tc_Days);

            TableCell tc_Field = new TableCell();
            tc_Field.BorderStyle = BorderStyle.Solid;
            tc_Field.BorderWidth = 1;
            tc_Field.Width = 100;
            Literal lit_Field = new Literal();
            lit_Field.Text = "<center><b>Days Field</b></center>";
            tc_Field.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_Field.ForeColor = System.Drawing.Color.White;
            tc_Field.Style.Add("border-color", "Black");
            tc_Field.Controls.Add(lit_Field);
            tr_tp.Cells.Add(tc_Field);

            TableCell tc_Non = new TableCell();
            tc_Non.BorderStyle = BorderStyle.Solid;
            tc_Non.BorderWidth = 1;
            tc_Non.Width = 100;
            Literal lit_Non = new Literal();
            lit_Non.Text = "<center><b>Days Non Field</b></center>";
            tc_Non.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_Non.ForeColor = System.Drawing.Color.White;
            tc_Non.Style.Add("border-color", "Black");
            tc_Non.Controls.Add(lit_Non);
            tr_tp.Cells.Add(tc_Non);

            TableCell tc_Leave = new TableCell();
            tc_Leave.BorderStyle = BorderStyle.Solid;
            tc_Leave.BorderWidth = 1;
            tc_Leave.Width = 100;
            Literal lit_Leave = new Literal();
            lit_Leave.Text = "<center><b>Days On Leave</b></center>";
            tc_Leave.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_Leave.ForeColor = System.Drawing.Color.White;
            tc_Leave.Style.Add("border-color", "Black");
            tc_Leave.Controls.Add(lit_Leave);
            tr_tp.Cells.Add(tc_Leave);

            TableCell tc_Call = new TableCell();
            tc_Call.BorderStyle = BorderStyle.Solid;
            tc_Call.BorderWidth = 1;
            tc_Call.Width = 100;
            Literal lit_Call = new Literal();
            lit_Call.Text = "<center><b>Total Calls Seen</b></center>";
            tc_Call.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_Call.ForeColor = System.Drawing.Color.White;
            tc_Call.Style.Add("border-color", "Black");
            tc_Call.Controls.Add(lit_Call);
            tr_tp.Cells.Add(tc_Call);


            TableCell tc_CallAvg = new TableCell();
            tc_CallAvg.BorderStyle = BorderStyle.Solid;
            tc_CallAvg.BorderWidth = 1;
            tc_CallAvg.Width = 100;
            Literal lit_CallAvg = new Literal();
            lit_CallAvg.Text = "<center><b>Call Average</b></center>";
            tc_CallAvg.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_CallAvg.ForeColor = System.Drawing.Color.White;
            tc_CallAvg.Style.Add("border-color", "Black");
            tc_CallAvg.Controls.Add(lit_CallAvg);
            tr_tp.Cells.Add(tc_CallAvg);

            TableCell tc_Dayss = new TableCell();
            tc_Dayss.BorderStyle = BorderStyle.Solid;
            tc_Dayss.BorderWidth = 1;
            tc_Dayss.Width = 100;
            Literal lit_Dayss = new Literal();
            lit_Dayss.Text = "<center><b>Days</b></center>";
            tc_Dayss.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_Dayss.ForeColor = System.Drawing.Color.White;
            tc_Dayss.Style.Add("border-color", "Black");
            tc_Dayss.Controls.Add(lit_Dayss);
            tr_tp.Cells.Add(tc_Dayss);

            TableCell tc_Met = new TableCell();
            tc_Met.BorderStyle = BorderStyle.Solid;
            tc_Met.BorderWidth = 1;
            tc_Met.Width = 100;
            Literal lit_Met = new Literal();
            lit_Met.Text = "<center><b>Calls Met</b></center>";
            tc_Met.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_Met.ForeColor = System.Drawing.Color.White;
            tc_Met.Style.Add("border-color", "Black");
            tc_Met.Controls.Add(lit_Met);
            tr_tp.Cells.Add(tc_Met);

            TableCell tc_Seen = new TableCell();
            tc_Seen.BorderStyle = BorderStyle.Solid;
            tc_Seen.BorderWidth = 1;
            tc_Seen.Width = 100;
            Literal lit_Seen = new Literal();
            lit_Seen.Text = "<center><b>Calls Seen</b></center>";
            tc_Seen.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_Seen.ForeColor = System.Drawing.Color.White;
            tc_Seen.Style.Add("border-color", "Black");
            tc_Seen.Controls.Add(lit_Seen);
            tr_tp.Cells.Add(tc_Seen);

            TableCell tc_CalAvg = new TableCell();
            tc_CalAvg.BorderStyle = BorderStyle.Solid;
            tc_CalAvg.BorderWidth = 1;
            tc_CalAvg.Width = 100;
            Literal lit_CalAvg = new Literal();
            lit_CalAvg.Text = "<center><b>Call Average</b></center>";
            tc_CalAvg.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_CalAvg.ForeColor = System.Drawing.Color.White;
            tc_CalAvg.Style.Add("border-color", "Black");
            tc_CalAvg.Controls.Add(lit_CalAvg);
            tr_tp.Cells.Add(tc_CalAvg);

            

            tbl.Rows.Add(tr_tp);
          
            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            string sTab = string.Empty;

            foreach (DataRow drFF in dsFF.Tables[0].Rows)
            {
                if (drFF["sf_code"].ToString() != "admin")
                {
                    TableRow tr_det = new TableRow();

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

                    //TableCell tc_det_entry = new TableCell();
                    //Literal lit_det_entry = new Literal();
                    //lit_det_entry.Text = "";
                    //tc_det_entry.BorderStyle = BorderStyle.Solid;
                    //tc_det_entry.BorderWidth = 1;
                    //tc_det_entry.HorizontalAlign = HorizontalAlign.Center;
                    int cmonthdoc = Convert.ToInt32(FMonth);
                    int cyeardoc = Convert.ToInt32(FYear);
                    DateTime dtCurrent;
                    if (cmonthdoc == 12)
                    {
                        sCurrentDate = "01-01-" + (cyeardoc + 1);
                    }
                    else
                    {
                        sCurrentDate = (cmonthdoc + 1) + "-01-" + cyeardoc;
                        //sCurrentDate = cmonth  + "-01-" + cyear;
                    }

                    dtCurrent = Convert.ToDateTime(sCurrentDate);
                    DataSet dsdoc = new DataSet();
                    SalesForce dcrdoc = new SalesForce();
                    string tot_fldwrkodc = "";
                    //int itotWorkType = 0;
                    //int fldwrk_total = 0;
                    dsdoc = dcrdoc.MissedCallReport(div_code, drFF["sf_code"].ToString(), cmonthdoc, cyeardoc, dtCurrent);
                    if (dsdoc.Tables[0].Rows.Count > 0)
                        tot_fldwrkodc = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    TableCell tc_det_confirm = new TableCell();
                    Literal lit_det_confirm = new Literal();
                    lit_det_confirm.Text = "&nbsp;" + tot_fldwrkodc;
                    tc_det_confirm.BorderStyle = BorderStyle.Solid;
                    tc_det_confirm.BorderWidth = 1;
                    tc_det_confirm.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_confirm.Controls.Add(lit_det_confirm);
                    tr_det.Cells.Add(tc_det_confirm);

                    //  int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
                    int cmonth1 = Convert.ToInt32(FMonth);
                    int cyear1 = Convert.ToInt32(FYear);

                    //    ViewState["months"] = months1;
                    ViewState["cmonth"] = cmonth1;
                    ViewState["cyear"] = cyear1;
                    DataSet ds = new DataSet();
                    DCR dcr1 = new DCR();
                    string tot_fldwrk = "";
                    int itotWorkType = 0;
                    int fldwrk_total = 0;
                    ds = dcr1.DCR_Doc_Met_Team(drFF["sf_code"].ToString(), div_code, cmonth1, cyear1);
                    if (ds.Tables[0].Rows.Count > 0)
                        tot_fldwrk = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);

                    tr_det.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF = new TableCell();
                    Literal lit_det_FF = new Literal();
                    lit_det_FF.Text = "&nbsp" + ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Controls.Add(lit_det_FF);
                    tc_det_FF.Style.Add("text-align", "left");

                    tr_det.Cells.Add(tc_det_FF);

                    int imissed_dr = 0;
                    TableCell tc_det_cov = new TableCell();
                    Literal lit_det_cov = new Literal();
                    decimal RoundLstCallAvg3 = new decimal();
                    double dblaverage3 = 0.00;
                    // if (tot_fldwrk != "0")
                    if (Convert.ToDecimal(tot_fldwrk) != 0)
                        //   imissed_dr = Convert.ToInt16(tot_fldwrkodc) / Convert.ToInt16(tot_fldwrk);
                        dblaverage3 = Convert.ToDouble((Convert.ToDecimal(tot_fldwrk) / Convert.ToDecimal(tot_fldwrkodc))) * 100;
                    RoundLstCallAvg3 = Math.Round((decimal)dblaverage3, 2);



                    lit_det_cov.Text = "&nbsp;" + RoundLstCallAvg3;
                    tc_det_cov.BorderStyle = BorderStyle.Solid;
                    tc_det_cov.BorderWidth = 1;
                    tc_det_cov.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_cov.Controls.Add(lit_det_cov);
                    tr_det.Cells.Add(tc_det_cov);

                    TableCell tc_det_mis = new TableCell();
                    Literal lit_det_mis = new Literal();
                    if (tot_fldwrkodc != "0")
                    {
                        imissed_dr = Convert.ToInt16(tot_fldwrkodc) - Convert.ToInt16(tot_fldwrk);
                    }
                    lit_det_mis.Text = "&nbsp;" + imissed_dr;
                    tc_det_mis.BorderStyle = BorderStyle.Solid;
                    tc_det_mis.BorderWidth = 1;
                    tc_det_mis.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_mis.Controls.Add(lit_det_mis);
                    tr_det.Cells.Add(tc_det_mis);

                    TableCell tc_det_Unmis = new TableCell();
                    Literal lit_det_Unmis = new Literal();
                    //lit_det_mis.Text = "";
                    DataSet dsUndoc = new DataSet();
                    DCR dcr = new DCR();
                    string tot_fldwrk_undoc = "";
                    //    int itotUndoc = 0;
                    //  int Undocmet = 0;
                    dsUndoc = dcr.DCR_UnDoc_Met(drFF["sf_code"].ToString(), div_code, cmonth1, cyear1);
                    if (dsUndoc.Tables[0].Rows.Count > 0)
                        tot_fldwrk_undoc = dsUndoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    lit_det_Unmis.Text = "&nbsp;" + tot_fldwrk_undoc;
                    tc_det_Unmis.BorderStyle = BorderStyle.Solid;
                    tc_det_Unmis.BorderWidth = 1;
                    tc_det_Unmis.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_Unmis.Controls.Add(lit_det_Unmis);
                    tr_det.Cells.Add(tc_det_Unmis);


                    DataSet dsdays = new DataSet();
                    dsdays = dcr1.Get_WorkDaysField(drFF["sf_code"].ToString(), div_code, cmonth1, cyear1);
                    if (dsdays.Tables[0].Rows.Count > 0)
                        tot_fldwrk = dsdays.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    TableCell tc_det_Days = new TableCell();
                    Literal lit_det_Days = new Literal();
                    lit_det_Days.Text = "&nbsp;" + tot_fldwrk;
                    tc_det_Days.BorderStyle = BorderStyle.Solid;
                    tc_det_Days.BorderWidth = 1;
                    tc_det_Days.Controls.Add(lit_det_Days);
                    tc_det_Days.HorizontalAlign = HorizontalAlign.Center;

                    tr_det.Cells.Add(tc_det_Days);

                    DataSet dsField = new DataSet();
                    string tot_fldwrkDays = "";
                    dsField = dcr1.Get_workDays(drFF["sf_code"].ToString(), div_code, cmonth1, cyear1, "F");
                    if (dsField.Tables[0].Rows.Count > 0)
                        tot_fldwrkDays = dsField.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    TableCell tc_det_wk = new TableCell();
                    Literal lit_det_wk = new Literal();
                    lit_det_wk.Text = "&nbsp;" + tot_fldwrkDays;
                    tc_det_wk.BorderStyle = BorderStyle.Solid;
                    tc_det_wk.BorderWidth = 1;
                    tc_det_wk.Controls.Add(lit_det_wk);
                    tc_det_wk.HorizontalAlign = HorizontalAlign.Center;

                    tr_det.Cells.Add(tc_det_wk);


                    DataSet dsNonField = new DataSet();
                    string tot_Nonfldwrk = "";
                    dsNonField = dcr1.Get_workDays(drFF["sf_code"].ToString(), div_code, cmonth1, cyear1, "N");
                    if (dsNonField.Tables[0].Rows.Count > 0)
                        tot_Nonfldwrk = dsNonField.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    TableCell tc_det_Non = new TableCell();
                    Literal lit_det_Non = new Literal();
                    lit_det_Non.Text = "&nbsp;" + tot_Nonfldwrk;
                    tc_det_Non.BorderStyle = BorderStyle.Solid;
                    tc_det_Non.BorderWidth = 1;
                    tc_det_Non.Controls.Add(lit_det_Non);
                    tc_det_Non.HorizontalAlign = HorizontalAlign.Center;

                    tr_det.Cells.Add(tc_det_Non);

                    //   DataSet ds = new DataSet();
                    ds = dcr1.Get_workDays(drFF["sf_code"].ToString(), div_code, cmonth1, cyear1, "L");

                    if (ds.Tables[0].Rows.Count > 0)
                        tot_fldwrk = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    //  itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);
                    //  string leave = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    tr_det.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_Leave = new TableCell();
                    Literal lit_det_Leave = new Literal();

                    if (tot_fldwrk == "0")
                    {
                        lit_det_Leave.Text = "&nbsp;" + " -";
                    }
                    else
                    {
                        lit_det_Leave.Text = "&nbsp" + tot_fldwrk;
                    }
                    tc_det_Leave.BorderStyle = BorderStyle.Solid;
                    tc_det_Leave.BorderWidth = 1;
                    tc_det_Leave.Controls.Add(lit_det_Leave);
                    tc_det_Leave.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_Leave);

                    DataSet dsCall = new DataSet();
                    string tot_docSeen = string.Empty;
                    int cmonthcall = Convert.ToInt32(FMonth);
                    int cyearcall = Convert.ToInt32(FYear);
                    TableCell tc_det_call = new TableCell();
                    Literal lit_det_call = new Literal();
                    dsCall = dcr1.DCR_Doc_Calls_Seen(drFF["sf_code"].ToString(), div_code, cmonthcall, cyearcall);
                    if (dsCall.Tables[0].Rows.Count > 0)
                        tot_docSeen = dsCall.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    lit_det_call.Text = "&nbsp" + tot_docSeen;
                    tc_det_call.BorderStyle = BorderStyle.Solid;
                    tc_det_call.BorderWidth = 1;
                    tc_det_call.Controls.Add(lit_det_call);
                    tc_det_call.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_call);

                    TableCell tc_det_callavg = new TableCell();
                    Literal lit_det_callavg = new Literal();
                    decimal RoundLstCallAvg = new decimal();
                    double dblaverage = 0.00;
                    if (Convert.ToDecimal(tot_fldwrkDays) != 0)

                        dblaverage = Convert.ToDouble((Convert.ToDecimal(tot_docSeen) / Convert.ToDecimal(tot_fldwrkDays)));
                    RoundLstCallAvg = Math.Round((decimal)dblaverage, 2);
                    lit_det_callavg.Text = "&nbsp" + RoundLstCallAvg;
                    tc_det_callavg.BorderStyle = BorderStyle.Solid;
                    tc_det_callavg.BorderWidth = 1;
                    tc_det_callavg.Controls.Add(lit_det_callavg);
                    tc_det_callavg.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_callavg);

                    DataSet dsworkday = new DataSet();
                    string tot_wrkdays = "";
                    dsworkday = dcr1.DCR_workwithDay(sf_code, div_code, drFF["sf_code"].ToString(), cmonth1, cyear1);
                    if (dsworkday.Tables[0].Rows.Count > 0)
                        tot_wrkdays = dsworkday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    TableCell tc_det_days = new TableCell();
                    Literal lit_det_days = new Literal();
                    lit_det_days.Text = "&nbsp" + tot_wrkdays;
                    tc_det_days.BorderStyle = BorderStyle.Solid;
                    tc_det_days.BorderWidth = 1;
                    tc_det_days.Controls.Add(lit_det_days);
                    tc_det_days.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_days);

                    TableCell tc_det_callmet = new TableCell();
                    Literal lit_det_callmet = new Literal();

                    string tot_wrkdoc = "";
                    DataSet dsworkmet = new DataSet();
                    dsworkmet = dcr1.DCR_workwithDocMet(sf_code, div_code, drFF["sf_code"].ToString(), cmonth1, cyear1);
                    if (dsworkmet.Tables[0].Rows.Count > 0)
                        tot_wrkdoc = dsworkmet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    lit_det_callmet.Text = "&nbsp" + tot_wrkdoc;
                    tc_det_callmet.BorderStyle = BorderStyle.Solid;
                    tc_det_callmet.BorderWidth = 1;
                    tc_det_callmet.Controls.Add(lit_det_callmet);
                    tc_det_callmet.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_callmet);

                    TableCell tc_det_callseen = new TableCell();
                    Literal lit_det_callseen = new Literal();
                    string tot_wrkseen = "";
                    DataSet dsworkseen = new DataSet();
                    dsworkseen = dcr1.DCR_workwithDocSeen(sf_code, div_code, drFF["sf_code"].ToString(), cmonth1, cyear1);
                    if (dsworkseen.Tables[0].Rows.Count > 0)
                        tot_wrkseen = dsworkseen.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    lit_det_callseen.Text = "&nbsp" + tot_wrkseen;
                    tc_det_callseen.BorderStyle = BorderStyle.Solid;
                    tc_det_callseen.BorderWidth = 1;
                    tc_det_callseen.Controls.Add(lit_det_callseen);
                    tc_det_callseen.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_callseen);

                    TableCell tc_det_Jcallavg = new TableCell();
                    Literal lit_det_Jcallavg = new Literal();
                    decimal RoundLstCallAvg5 = new decimal();
                    double dblaverage5 = 0.00;
                    if (Convert.ToDecimal(tot_wrkdays) != 0)

                        dblaverage5 = Convert.ToDouble((Convert.ToDecimal(tot_wrkseen) / Convert.ToDecimal(tot_wrkdays)));
                    RoundLstCallAvg5 = Math.Round((decimal)dblaverage5, 2);
                    lit_det_Jcallavg.Text = "&nbsp" + RoundLstCallAvg5;
                    tc_det_Jcallavg.BorderStyle = BorderStyle.Solid;
                    tc_det_Jcallavg.BorderWidth = 1;
                    tc_det_Jcallavg.Controls.Add(lit_det_Jcallavg);
                    tc_det_Jcallavg.HorizontalAlign = HorizontalAlign.Center;
                    tr_det.Cells.Add(tc_det_Jcallavg);


                    tbl.Rows.Add(tr_det);
                }

            }
        }
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

}