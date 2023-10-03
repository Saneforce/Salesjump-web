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

public partial class MIS_Reports_Visit_Details_Consolidated_Level3 : System.Web.UI.Page
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

    int fldwrk_total = 0;
    int doctor_total = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    double dblCoverage = 0.00;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string tot_dr = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;


    string sCurrentDate = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();

        string sf_name = string.Empty;

        SalesForce sf = new SalesForce();
        DataSet dssf = sf.getSfName(sf_code);
        if (dssf.Tables[0].Rows.Count > 0)
            sf_name = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

        string sMonth = getMonthName(Convert.ToInt16(FMonth)) + FYear.ToString();
        if (!Page.IsPostBack)
        {
            lblHead.Text = lblHead.Text + sMonth + " - " + sf_name;
        }

        //FillDoctor();
        FillSF();
        ExportButton();
    }

    private void ExportButton()
    {
        btnPDF.Visible = false;
    }

    private void FillSF()
    {
        tbl.Rows.Clear();
        doctor_total = 0;


        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        dsSalesForce = dcc.SF_ReportingTo_TourPlan(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.ForeColor = System.Drawing.Color.White;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 2;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.Controls.Add(lit_SNo);
            tr_header.Cells.Add(tc_SNo);
            //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 200;
            tc_DR_Code.RowSpan = 2;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 150;
            tc_DR_Name.RowSpan = 2;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Field&nbspForce&nbspName</center>";
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 150;
            tc_DR_HQ.RowSpan = 2;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.BorderStyle = BorderStyle.Solid;
            tc_DR_Des.BorderWidth = 1;
            tc_DR_Des.Width = 150;
            tc_DR_Des.RowSpan = 2;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Designation</center>";
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);

            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            Doctor dr = new Doctor();
            dsDoctor = dr.getDocCat(div_code);

            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = 8;
                    Literal lit_month = new Literal();
                    lit_month.Text = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }

            tbl.Rows.Add(tr_header);

            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());


            if (months >= 0)
            {
                TableRow tr_lst_det = new TableRow();
                tr_lst_det.BackColor = System.Drawing.Color.FromName("#0097AC");
                tr_lst_det.ForeColor = System.Drawing.Color.White;

                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_DR_FldWrk = new TableCell();
                    tc_DR_FldWrk.BorderStyle = BorderStyle.Solid;
                    tc_DR_FldWrk.BorderWidth = 1;
                    //tc_DR_FldWrk.BackColor = System.Drawing.Color.LavenderBlush;
                    tc_DR_FldWrk.Width = 90;
                    Literal lit_DR_FldWrk = new Literal();
                    lit_DR_FldWrk.Text = "<center>No of.<br> FWD</center>";
                    tc_DR_FldWrk.Controls.Add(lit_DR_FldWrk);
                    tr_lst_det.Cells.Add(tc_DR_FldWrk);

                    TableCell tc_DR_Total = new TableCell();
                    tc_DR_Total.BorderStyle = BorderStyle.Solid;
                    tc_DR_Total.BorderWidth = 1;
                    tc_DR_Total.Width = 90;
                    Literal lit_DR_Total = new Literal();
                    lit_DR_Total.Text = "<center>Total <br> Drs</center>";
                    tc_DR_Total.Controls.Add(lit_DR_Total);
                    tr_lst_det.Cells.Add(tc_DR_Total);

                    TableCell tc_Docs_met = new TableCell();
                    tc_Docs_met.BorderStyle = BorderStyle.Solid;
                    tc_Docs_met.BorderWidth = 1;
                    tc_Docs_met.Width = 90;
                    Literal lit_Docs_met = new Literal();
                    lit_Docs_met.Text = "<center>Drs <br> Met</center>";
                    tc_Docs_met.Controls.Add(lit_Docs_met);
                    tr_lst_det.Cells.Add(tc_Docs_met);

                    TableCell tc_Docs_call_seen = new TableCell();
                    tc_Docs_call_seen.BorderStyle = BorderStyle.Solid;
                    tc_Docs_call_seen.BorderWidth = 1;
                    tc_Docs_call_seen.Width = 90;
                    Literal lit_Docs_calls_seen = new Literal();
                    lit_Docs_calls_seen.Text = "<center>Dr Calls<br> Seen</center>";
                    tc_Docs_call_seen.Controls.Add(lit_Docs_calls_seen);
                    tr_lst_det.Cells.Add(tc_Docs_call_seen);

                    TableCell tc_Docs_Missed = new TableCell();
                    tc_Docs_Missed.BorderStyle = BorderStyle.Solid;
                    tc_Docs_Missed.BorderWidth = 1;
                    tc_Docs_Missed.Width = 90;
                    Literal lit_Docs_Missed = new Literal();
                    lit_Docs_Missed.Text = "<center>Missed</center>";
                    tc_Docs_Missed.Controls.Add(lit_Docs_Missed);
                    tr_lst_det.Cells.Add(tc_Docs_Missed);

                    TableCell tc_coverage = new TableCell();
                    tc_coverage.BorderStyle = BorderStyle.Solid;
                    tc_coverage.BorderWidth = 1;
                    tc_coverage.Width = 90;
                    Literal lit_coverage = new Literal();
                    lit_coverage.Text = "<center>Coverage<br> % </center>";
                    tc_coverage.Controls.Add(lit_coverage);
                    tr_lst_det.Cells.Add(tc_coverage);

                    TableCell tc_average = new TableCell();
                    tc_average.BorderStyle = BorderStyle.Solid;
                    tc_average.BorderWidth = 1;
                    tc_average.Width = 90;
                    Literal lit_average = new Literal();
                    lit_average.Text = "<center>Call <br>Average </center>";
                    tc_average.Controls.Add(lit_average);
                    tr_lst_det.Cells.Add(tc_average);

                    TableCell tc_Missed = new TableCell();
                    tc_Missed.BorderStyle = BorderStyle.Solid;
                    tc_Missed.BorderWidth = 1;
                    tc_Missed.Width = 90;
                    Literal lit_Missed = new Literal();
                    lit_Missed.Text = "<center>Missed <br>Average </center>";
                    tc_Missed.Controls.Add(lit_Missed);
                    tr_lst_det.Cells.Add(tc_Missed);

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }

                //tr_lst_det.BackColor = System.Drawing.Color.FromName("#A6A6D2");
                tbl.Rows.Add(tr_lst_det);
            }


            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            int iCnt = 0;
            int imonth = 0;
            int iyear = 0;
            DCR dcs = new DCR();

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                ListedDR lstDR = new ListedDR();
                iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                TableRow tr_det = new TableRow();
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
                HyperLink lit_det_doc_name = new HyperLink();
                lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                tc_det_doc_name.HorizontalAlign = HorizontalAlign.Left;
                if (drFF["SF_Type"].ToString() == "2")
                {
                    lit_det_doc_name.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "','" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "', '1')");
                }
                tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                tc_det_doc_name.BorderWidth = 1;               
                tc_det_doc_name.Controls.Add(lit_det_doc_name);
                tr_det.Cells.Add(tc_det_doc_name);

                TableCell tc_det_sf_HQ = new TableCell();
                Literal lit_det_sf_hq = new Literal();
                lit_det_sf_hq.Text = "&nbsp;" + drFF["Sf_HQ"].ToString();
                tc_det_sf_HQ.BorderStyle = BorderStyle.Solid;
                tc_det_sf_HQ.BorderWidth = 1;
                tc_det_sf_HQ.Controls.Add(lit_det_sf_hq);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_HQ);


                TableCell tc_det_sf_des = new TableCell();
                Literal lit_det_sf_des = new Literal();
                lit_det_sf_des.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                tc_det_sf_des.BorderStyle = BorderStyle.Solid;
                tc_det_sf_des.BorderWidth = 1;
                tc_det_sf_des.Controls.Add(lit_det_sf_des);
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_des);


                months = Convert.ToInt16(ViewState["months"].ToString());
                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                cyear = Convert.ToInt16(ViewState["cyear"].ToString());

                if (months >= 0)
                {
                    imonth = cmonth;
                    iyear = cyear;


                    for (int j = 1; j <= months + 1; j++)
                    {
                        tot_fldwrk = "";
                        tot_dr = "";
                        tot_doc_met = "";
                        tot_doc_calls_seen = "";


                        fldwrk_total = 0;
                        doctor_total = 0;
                        doc_met_total = 0;
                        doc_calls_seen_total = 0;
                        dblCoverage = 0.00;
                        dblaverage = 0.00;

                        // Field Work
                        dsDoc = dcs.DCR_Visit_FLDWRK(drFF["sf_code"].ToString(), div_code, cmonth, iyear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_fldwrk = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        fldwrk_total = fldwrk_total + Convert.ToInt16(tot_fldwrk);                        

                        TableCell tc_det_sf_FLDWRK = new TableCell();
                        Literal lit_det_sf_FLDWRK = new Literal();
                        lit_det_sf_FLDWRK.Text = "&nbsp;" + fldwrk_total.ToString();
                        tc_det_sf_FLDWRK.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_FLDWRK.BorderWidth = 1;
                        tc_det_sf_FLDWRK.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_FLDWRK.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_FLDWRK.Controls.Add(lit_det_sf_FLDWRK);
                        tr_det.Cells.Add(tc_det_sf_FLDWRK);

                        // Field Work


                        // Total Doctors

                        if (cmonth == 12)
                        {
                            sCurrentDate = "01-01-" + (cyear + 1);
                        }
                        else
                        {
                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                        }

                        dtCurrent = Convert.ToDateTime(sCurrentDate);

                        dsDoc = dcs.DCR_Total_Doc_Visit_Report(drFF["sf_code"].ToString(), div_code, dtCurrent, cmonth, cyear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        doctor_total = doctor_total + Convert.ToInt16(tot_dr);

                        TableCell tc_det_sf_tot_doc = new TableCell();
                        Literal lit_det_sf_tot_doc = new Literal();
                        lit_det_sf_tot_doc.Text = "&nbsp;" + doctor_total.ToString();
                        tc_det_sf_tot_doc.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_tot_doc.BorderWidth = 1;
                        tc_det_sf_tot_doc.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_doc.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_doc.Controls.Add(lit_det_sf_tot_doc);
                        tr_det.Cells.Add(tc_det_sf_tot_doc);

                        // Total Doctors


                        //DRs Met

                        dsDoc = dcs.DCR_Doc_Met(drFF["sf_code"].ToString(), div_code, cmonth, iyear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_doc_met = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        doc_met_total = doc_met_total + Convert.ToInt16(tot_doc_met);

                     

                        TableCell tc_det_doc_met = new TableCell();
                        Literal lit_det_doc_met = new Literal();
                        lit_det_doc_met.Text = "&nbsp;" + doc_met_total.ToString();
                        tc_det_doc_met.BorderStyle = BorderStyle.Solid;
                        tc_det_doc_met.BorderWidth = 1;
                        tc_det_doc_met.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_doc_met.VerticalAlign = VerticalAlign.Middle;
                        tc_det_doc_met.Controls.Add(lit_det_doc_met);
                        tr_det.Cells.Add(tc_det_doc_met);

                        //DRS Met


                        //DRs Calls Seen

                        dsDoc = dcs.DCR_Doc_Calls_Seen(drFF["sf_code"].ToString(), div_code, cmonth, iyear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_doc_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        doc_calls_seen_total = doc_calls_seen_total + Convert.ToInt16(tot_doc_calls_seen);

                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }

                        TableCell tc_det_doc_calls_seen = new TableCell();
                        Literal lit_det_doc_calls_seen = new Literal();
                        lit_det_doc_calls_seen.Text = "&nbsp;" + doc_calls_seen_total.ToString();
                        tc_det_doc_calls_seen.BorderStyle = BorderStyle.Solid;
                        tc_det_doc_calls_seen.BorderWidth = 1;
                        tc_det_doc_calls_seen.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_doc_calls_seen.VerticalAlign = VerticalAlign.Middle;
                        tc_det_doc_calls_seen.Controls.Add(lit_det_doc_calls_seen);
                        tr_det.Cells.Add(tc_det_doc_calls_seen);

                        //DRs Calls Seen

                        //Missed

                        TableCell tc_det_Missed = new TableCell();
                        Literal lit_det_Missed = new Literal();
                        lit_det_Missed.Text = Convert.ToString(doctor_total - doc_met_total);
                        tc_det_Missed.BorderStyle = BorderStyle.Solid;
                        tc_det_Missed.BorderWidth = 1;
                        tc_det_Missed.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_Missed.VerticalAlign = VerticalAlign.Middle;
                        tc_det_Missed.Controls.Add(lit_det_Missed);
                        tr_det.Cells.Add(tc_det_Missed);

                        //Missed

                        //Coverage
                        decimal RoundCoverage = new decimal();
                        if (doctor_total > 0)
                            dblCoverage = Convert.ToDouble(((Convert.ToDecimal(doc_met_total) / Convert.ToDecimal(doctor_total)) * 100));
                        RoundCoverage = Math.Round((decimal)dblaverage, 2);

                        TableCell tc_det_coverage = new TableCell();
                        Literal lit_det_coverage = new Literal();
                        lit_det_coverage.Text = "&nbsp;" + RoundCoverage.ToString();
                        tc_det_coverage.BorderStyle = BorderStyle.Solid;
                        tc_det_coverage.BorderWidth = 1;
                        tc_det_coverage.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_coverage.VerticalAlign = VerticalAlign.Middle;
                        tc_det_coverage.Controls.Add(lit_det_coverage);
                        tr_det.Cells.Add(tc_det_coverage);

                        //Coverage

                        //Call Average
                        decimal RoundCallCoverage = new decimal();
                        if (fldwrk_total > 0)
                            dblaverage = Convert.ToDouble((Convert.ToDecimal(doc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                        RoundCallCoverage = Math.Round((decimal)dblaverage, 2);

                        TableCell tc_det_average = new TableCell();
                        Literal lit_det_average = new Literal();
                        lit_det_average.Text = "&nbsp;" + RoundCallCoverage.ToString();
                        tc_det_average.BorderStyle = BorderStyle.Solid;
                        tc_det_average.BorderWidth = 1;
                        tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_average.VerticalAlign = VerticalAlign.Middle;
                        tc_det_average.Controls.Add(lit_det_average);
                        tr_det.Cells.Add(tc_det_average);

                        //Call Average

                        //Missed Average

                        decimal RoundMissedAverage = new decimal();
                        if (fldwrk_total > 0)
                            dblaverage = Convert.ToDouble((Convert.ToDecimal(lit_det_Missed.Text) / Convert.ToDecimal(doctor_total)) * 100);
                        RoundMissedAverage = Math.Round((decimal)dblaverage, 2);

                        TableCell tc_det_Missed_average = new TableCell();
                        Literal lit_det_Missed_average = new Literal();
                        lit_det_Missed_average.Text = "&nbsp;" + RoundMissedAverage.ToString();
                        tc_det_Missed_average.BorderStyle = BorderStyle.Solid;
                        tc_det_Missed_average.BorderWidth = 1;
                        tc_det_Missed_average.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_Missed_average.VerticalAlign = VerticalAlign.Middle;
                        tc_det_Missed_average.Controls.Add(lit_det_Missed_average);
                        tr_det.Cells.Add(tc_det_Missed_average);

                        //Missed Average

                    }
                }

                tbl.Rows.Add(tr_det);

            }
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
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strFileName = this.Page.Title;
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