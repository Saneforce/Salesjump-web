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
using System.Web.UI.DataVisualization.Charting;

public partial class MIS_Reports_Visit_Details_Field2 : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    DataSet dsDCR = null;

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
    DataSet dsmgrsf = new DataSet();
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;

    int tot_miss = 0;
    int fldwrk_total = 0;
    int doctor_total = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    double dblCoverage = 0.00;
    double dblCatg = 0.00;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string tot_dr = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string sCurrentDate = string.Empty;
    string sType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        sType = Request.QueryString["Type"].ToString();

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

        if (sMode == "1")
        {
            dsSalesForce = dcc.SF_Self_Report(div_code, sf_code);
        }
        else
        {
             DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
             if (DsAudit.Tables[0].Rows.Count > 0)
             {
              dsSalesForce = dcc.SF_ReportingTo_TourPlan(div_code, sf_code);
             }
             else
             {
                 DataTable dt = sf.getAuditManagerTeam_GetMGR(div_code, sf_code, 0);
                 dsmgrsf.Tables.Add(dt);
                 dsmgrsf.Tables[0].Rows[0].Delete();
                 dsSalesForce = dsmgrsf;
             }

        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 3;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tr_header.Cells.Add(tc_SNo);
            //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 3;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;
            tc_DR_Name.RowSpan = 3;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Field&nbspForce&nbspName</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 150;
            tc_DR_HQ.RowSpan = 3;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.BorderColor = System.Drawing.Color.Black;
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.BorderStyle = BorderStyle.Solid;
            tc_DR_Des.BorderWidth = 1;
            tc_DR_Des.Width = 150;
            tc_DR_Des.RowSpan = 3;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Designation</center>";
            tc_DR_Des.BorderColor = System.Drawing.Color.Black;
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);

            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            Doctor dr = new Doctor();
            int Count = new int();

            if (sType == "1")
            {
                dsDoctor = dr.getDocCat(div_code);
                Count = dsDoctor.Tables[0].Rows.Count * 4;
            }
            else if (sType == "2")
            {
                dsDoctor = dr.getDocSpec(div_code);
                Count = dsDoctor.Tables[0].Rows.Count * 4;
            }
            else if (sType == "3")
            {
                dsDoctor = dr.getDocClass(div_code);
                Count = dsDoctor.Tables[0].Rows.Count * 4;
            }
            else if (sType == "4")
            {
                dsDoctor = dr.getDocSpec(div_code);
                Count = dsDoctor.Tables[0].Rows.Count * 4;
            }

            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();

                    tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count + Count - 1;
                    //tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                    Literal lit_month = new Literal();
                    lit_month.Text = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_month.BorderColor = System.Drawing.Color.Black;
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
                tr_lst_det.BorderColor = System.Drawing.Color.Black;

                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_coverage = new TableCell();
                    tc_coverage.BorderStyle = BorderStyle.Solid;
                    tc_coverage.BorderWidth = 1;
                    tc_coverage.Width = 150;
                    tc_coverage.ColumnSpan = 3;
                    Literal lit_coverage = new Literal();
                    lit_coverage.Text = "<center>Coverage</center>";
                    tc_coverage.Controls.Add(lit_coverage);
                    tc_coverage.BorderColor = System.Drawing.Color.Black;
                    tr_lst_det.Cells.Add(tc_coverage);

                    if (dsDoctor.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                        {
                            TableCell tc_catg_name = new TableCell();
                            tc_catg_name.BorderStyle = BorderStyle.Solid;
                            tc_catg_name.BorderWidth = 1;
                            tc_catg_name.Width = 300;
                            tc_catg_name.ColumnSpan = 3;
                            Literal lit_catg_name = new Literal();
                            lit_catg_name.Text = "<center>" + dataRow[1].ToString() + "</center>";
                            tc_catg_name.BorderColor = System.Drawing.Color.Black;
                            tc_catg_name.Controls.Add(lit_catg_name);
                            tr_lst_det.Cells.Add(tc_catg_name);
                        }
                    }

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

            if (months >= 0)
            {
                TableRow tr_Total_Drs = new TableRow();
                tr_Total_Drs.BackColor = System.Drawing.Color.FromName("#0097AC");
                tr_Total_Drs.ForeColor = System.Drawing.Color.White;

                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_TotalDrs = new TableCell();
                    tc_TotalDrs.BorderStyle = BorderStyle.Solid;
                    tc_TotalDrs.BorderWidth = 1;
                    tc_TotalDrs.Width = 90;
                    Literal lit_TotalDrs = new Literal();
                    lit_TotalDrs.Text = "<center>Total <br> Drs</center>";
                    tc_TotalDrs.BorderColor = System.Drawing.Color.Black;
                    tc_TotalDrs.Controls.Add(lit_TotalDrs);
                    tr_Total_Drs.Cells.Add(tc_TotalDrs);

                    TableCell tc_DrsMet = new TableCell();
                    tc_DrsMet.BorderStyle = BorderStyle.Solid;
                    tc_DrsMet.BorderWidth = 1;
                    tc_DrsMet.Width = 90;
                    Literal lit_DrsMet = new Literal();
                    lit_DrsMet.Text = "<center>Drs <br> Met</center>";
                    tc_DrsMet.Controls.Add(lit_DrsMet);
                    tc_DrsMet.BorderColor = System.Drawing.Color.Black;
                    tr_Total_Drs.Cells.Add(tc_DrsMet);

                    TableCell tc_DrsCoverage = new TableCell();
                    tc_DrsCoverage.BorderStyle = BorderStyle.Solid;
                    tc_DrsCoverage.BorderWidth = 1;
                    tc_DrsCoverage.Width = 90;
                    Literal lit_DrsCoverage = new Literal();
                    lit_DrsCoverage.Text = "<center>Coverage</center>";
                    tc_DrsCoverage.BorderColor = System.Drawing.Color.Black;
                    tc_DrsCoverage.Controls.Add(lit_DrsCoverage);
                    tr_Total_Drs.Cells.Add(tc_DrsCoverage);


                    if (dsDoctor.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                        {
                            TableCell tc_TotalCate = new TableCell();
                            tc_TotalCate.BorderStyle = BorderStyle.Solid;
                            tc_TotalCate.BorderWidth = 1;
                            tc_TotalCate.Width = 90;
                            Literal lit_TotalCate = new Literal();
                            lit_TotalCate.Text = "<center>Total <br>" + dataRow[1].ToString() + "</center>";
                            tc_TotalCate.BorderColor = System.Drawing.Color.Black;
                            tc_TotalCate.Controls.Add(lit_TotalCate);
                            tr_Total_Drs.Cells.Add(tc_TotalCate);

                            TableCell tc_CateMissed = new TableCell();
                            tc_CateMissed.BorderStyle = BorderStyle.Solid;
                            tc_CateMissed.BorderWidth = 1;
                            tc_CateMissed.Width = 90;
                            Literal lit_CateMissed = new Literal();
                            lit_CateMissed.Text = "<center>Missed</center>";
                            tc_CateMissed.BorderColor = System.Drawing.Color.Black;
                            tc_CateMissed.Controls.Add(lit_CateMissed);
                            tr_Total_Drs.Cells.Add(tc_CateMissed);

                            TableCell tc_CateAvg = new TableCell();
                            tc_CateAvg.BorderStyle = BorderStyle.Solid;
                            tc_CateAvg.BorderWidth = 1;
                            tc_CateAvg.Width = 90;
                            Literal lit_CateAvg = new Literal();
                            lit_CateAvg.Text = "<center>Coverage</center>";
                            tc_CateAvg.BorderColor = System.Drawing.Color.Black;
                            tc_CateAvg.Controls.Add(lit_CateAvg);
                            tr_Total_Drs.Cells.Add(tc_CateAvg);
                        }

                    }

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }

                }

                tbl.Rows.Add(tr_Total_Drs);
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
                if ((drFF["SF_Type"].ToString() == "2") && (sMode == "2"))
                {
                    lit_det_doc_name.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "', '" + sMode + "','" + sType + "')");
                }
                tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                tc_det_doc_name.BorderWidth = 1;
                tc_det_doc_name.Width = 90;
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

                        if (cmonth == 12)
                        {
                            sCurrentDate = "01-01-" + (cyear + 1);
                        }
                        else
                        {
                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                        }

                        dtCurrent = Convert.ToDateTime(sCurrentDate);

                        dsDoc = dcs.DCR_Total_Doc_Visit_Report(drFF["sf_code"].ToString(), div_code, dtCurrent,cmonth,cyear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        doctor_total = doctor_total + Convert.ToInt16(tot_dr);

                        TableCell tc_totDrs = new TableCell();
                        Literal lit_totDrs = new Literal();
                        lit_totDrs.Text = "<center>" + doctor_total.ToString() + "</center>";
                        tc_totDrs.BorderStyle = BorderStyle.Solid;
                        tc_totDrs.BorderWidth = 1;
                        tc_totDrs.Width = 50;
                        tc_totDrs.Controls.Add(lit_totDrs);
                        tr_det.Cells.Add(tc_totDrs);

                        dsDoc = dcs.DCR_Doc_Met(drFF["sf_code"].ToString(), div_code, cmonth, iyear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_doc_met = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        doc_met_total = doc_met_total + Convert.ToInt16(tot_doc_met);

                        TableCell tc_DrsMet = new TableCell();
                        Literal lit_DrsMet = new Literal();
                        lit_DrsMet.Text = "<center>" + doc_met_total.ToString() + "</center>";
                        tc_DrsMet.BorderStyle = BorderStyle.Solid;
                        tc_DrsMet.BorderWidth = 1;
                        tc_DrsMet.Width = 50;
                        tc_DrsMet.Controls.Add(lit_DrsMet);
                        tr_det.Cells.Add(tc_DrsMet);

                        if (doctor_total > 0)
                            dblCoverage = Convert.ToDouble(((Convert.ToDecimal(doc_met_total) / Convert.ToDecimal(doctor_total)) * 100));

                        //dblCoverage = Convert.ToDouble(((Convert.ToDecimal(doc_met_total) / Convert.ToDecimal(doctor_total))));

                        TableCell tc_det_coverage = new TableCell();
                        Literal lit_det_coverage = new Literal();
                        if (dblCoverage > 0)
                            lit_det_coverage.Text = "&nbsp;" + dblCoverage.ToString("#.##") + "%";
                        else
                            lit_det_coverage.Text = "&nbsp;" + dblCoverage.ToString() + "%";
                        tc_det_coverage.BorderStyle = BorderStyle.Solid;
                        tc_det_coverage.BorderWidth = 1;
                        tc_det_coverage.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_coverage.VerticalAlign = VerticalAlign.Middle;
                        tc_det_coverage.Controls.Add(lit_det_coverage);
                        tr_det.Cells.Add(tc_det_coverage);

                        if (dsDoctor.Tables[0].Rows.Count > 0)
                        {


                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                            {
                                tot_dcr_dr = "";
                                tot_dr = "";
                                dblCatg = 0;

                                dsDoc = sf.Catg_LST_Report(drFF["sf_code"].ToString(), div_code, dtCurrent,cmonth,cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));

                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                TableCell tc_totCore = new TableCell();
                                Literal lit_totCore = new Literal();
                                lit_totCore.Text = "<center>" + tot_dr.ToString() + "</center>";
                                tc_totCore.BorderStyle = BorderStyle.Solid;
                                tc_totCore.BorderWidth = 1;
                                tc_totCore.Width = 50;
                                tc_totCore.Controls.Add(lit_totCore);
                                tr_det.Cells.Add(tc_totCore);

                                if (sType == "1")
                                {
                                    dsDCR = dcs.Catg_Visit_Report(drFF["sf_code"].ToString(), div_code, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));
                                }
                                else if (sType == "2")
                                {
                                    dsDCR = dcs.Special_Visit_Report(drFF["sf_code"].ToString(), div_code, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));
                                }
                                else if (sType == "3")
                                {
                                    dsDCR = dcs.Class_Visit_Report(drFF["sf_code"].ToString(), div_code, cmonth, cyear, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));
                                }

                                if (dsDCR.Tables[0].Rows.Count > 0)
                                    tot_dcr_dr = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                TableCell tc_totMissed = new TableCell();
                                Literal lit_totMissed = new Literal();
                                lit_totMissed.Text = "<center>" + tot_dcr_dr.ToString() + "</center>";
                                tc_totMissed.BorderStyle = BorderStyle.Solid;
                                tc_totMissed.BorderWidth = 1;
                                tc_totMissed.Width = 50;
                                tc_totMissed.Controls.Add(lit_totMissed);
                                tr_det.Cells.Add(tc_totMissed);

                                if (Convert.ToInt32(tot_dr) > 0)
                                    dblCatg = Convert.ToDouble(((Convert.ToDecimal(tot_dcr_dr) / Convert.ToDecimal(tot_dr)) * 100));

                                //dblCatg = Convert.ToDouble(((Convert.ToDecimal(tot_dcr_dr) / Convert.ToDecimal(tot_dr))));

                                TableCell tc_catg_name = new TableCell();
                                tc_catg_name.BorderStyle = BorderStyle.Solid;
                                tc_catg_name.BorderWidth = 1;
                                Literal lit_catg_name = new Literal();

                                if (dblCatg > 0)
                                    lit_catg_name.Text = "<center>" + dblCatg.ToString("#.##") + "%" + "</center>";
                                else
                                    lit_catg_name.Text = "<center>" + dblCatg.ToString() + "%" + "</center>";

                                tc_catg_name.Controls.Add(lit_catg_name);
                                tr_det.Cells.Add(tc_catg_name);
                            }
                        }

                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }
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