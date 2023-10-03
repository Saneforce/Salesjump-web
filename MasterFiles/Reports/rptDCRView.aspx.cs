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

public partial class Reports_rptDCRView : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDCR = null;
    DataSet dsdoc = null;
    DataSet dssf = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string Sf_Name = string.Empty;
    string Sf_HQ = string.Empty;
    int cmonth = -1;
    int cyear = -1;
    int tot_days = -1;
    int cday = 1;
    int iCount = -1;
    string sDCR = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Request.QueryString["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        cmonth = Convert.ToInt16(Request.QueryString["cur_month"].ToString());
        cyear = Convert.ToInt16(Request.QueryString["cur_year"].ToString());
        
        CreateDynamicTable(cmonth, cyear, sf_code);
        //FillSalesForce(sf_code, cmonth, cyear);

        string sMonth = getMonthName(cmonth) + " - " + cyear.ToString();
        lblHead.Text = lblHead.Text + sMonth;

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

    private void FillSalesForce(string div_code, string sf_code, int cmonth, int cyear)
    {
        // Fetch the total rows for the table
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_getMR(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
            ViewState["dsSalesForce"] = dsSalesForce;

        DCR dc = new DCR();
        int iret = dc.isDCR(div_code,cmonth,cyear);
        if(iret > 0)
            CreateDynamicTable(cmonth,cyear,sf_code);
        //FillWorkType();
    }

    private void CreateDynamicTable(int imonth, int iyear, string sf_code)
    {
            DCR dc = new DCR();

            dsDCR = dc.get_dcr_date(sf_code, imonth, iyear);
            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                DCR dcsf = new DCR();
                dssf = dcsf.getSfName_HQ(sf_code);
  
                if (dssf.Tables[0].Rows.Count > 0)
                {
                    Sf_Name = dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Sf_HQ = dssf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }
           
                foreach (DataRow drdoc in dsDCR.Tables[0].Rows)
                {
                    TableRow tr_day = new TableRow();
                    TableCell tc_day = new TableCell();
                    tc_day.BorderStyle = BorderStyle.None;
                    tc_day.ColumnSpan = 2;
                    tc_day.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_day = new Literal();
                    lit_day.Text = "<u>Daily Work Report on " + drdoc["Activity_Date"].ToString() + "</u>";
                    tc_day.Controls.Add(lit_day);
                    tr_day.Cells.Add(tc_day);
                    tbl.Rows.Add(tr_day);

                    TableRow tr_ff = new TableRow();
                    TableCell tc_ff_name = new TableCell();
                    tc_ff_name.BorderStyle = BorderStyle.None;
                    tc_ff_name.Width = 200;
                    Literal lit_ff_name = new Literal();
                    lit_ff_name.Text = "<b>Field Force Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_Name.ToString();
                    tc_ff_name.Controls.Add(lit_ff_name);
                    tr_ff.Cells.Add(tc_ff_name);

                    TableCell tc_HQ = new TableCell();
                    tc_HQ.BorderStyle = BorderStyle.None;
                    tc_HQ.Width = 200;
                    Literal lit_HQ = new Literal();
                    lit_HQ.Text = "<b>Head Quarters</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_HQ.ToString();
                    tc_HQ.Controls.Add(lit_HQ);
                    tr_ff.Cells.Add(tc_HQ);

                    tbl.Rows.Add(tr_ff);

                    TableRow tr_dcr = new TableRow();
                    TableCell tc_dcr_submit = new TableCell();
                    tc_dcr_submit.BorderStyle = BorderStyle.None;
                    tc_dcr_submit.Width = 200;
                    Literal lit_dcr_submit = new Literal();
                    lit_dcr_submit.Text = "<b>DCR Submitted on</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoc["Submission_Date"].ToString();
                    tc_dcr_submit.Controls.Add(lit_dcr_submit);
                    tr_dcr.Cells.Add(tc_dcr_submit);

                    TableCell tc_Terr = new TableCell();
                    tc_Terr.BorderStyle = BorderStyle.None;
                    tc_Terr.Width = 200;
                    Literal lit_Terr = new Literal();
                    lit_Terr.Text = "<b>Territory Worked</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoc["Plan_Name"].ToString(); ;
                    tc_Terr.Controls.Add(lit_Terr);
                    tr_dcr.Cells.Add(tc_Terr);

                    tbl.Rows.Add(tr_dcr);
                    
                    //1-Listed Doctor
                    TableRow tr_det = new TableRow();
                    TableCell tc_det = new TableCell();
                    tc_det.BorderStyle = BorderStyle.None;
                    tc_det.Width = 400;
                    tc_det.ColumnSpan = 2;

                    Table tbldetail = new Table();
                    tbldetail.BorderStyle = BorderStyle.Solid;
                    tbldetail.BorderWidth = 1;
                    tbldetail.GridLines = GridLines.Both;
                    tbldetail.Width = 1000;
                    dsdoc = dc.get_dcr_details(sf_code, drdoc["Activity_Date"].ToString(), 1); //1-Listed Doctor
                    if (dsdoc.Tables[0].Rows.Count > 0)
                    {
                        TableRow tr_det_head = new TableRow();
                        TableCell tc_det_head_SNo = new TableCell();
                        tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_head_SNo.BorderWidth = 1;
                        Literal lit_det_head_SNo = new Literal();
                        lit_det_head_SNo.Text = "<b>S.No</b>";
                        tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                        tr_det_head.Cells.Add(tc_det_head_SNo);

                        TableCell tc_det_head_Ses = new TableCell();
                        tc_det_head_Ses.BorderStyle = BorderStyle.Solid;
                        tc_det_head_Ses.BorderWidth = 1;
                        Literal lit_det_head_Ses = new Literal();
                        lit_det_head_Ses.Text = "<b>Ses</b>";
                        tc_det_head_Ses.Controls.Add(lit_det_head_Ses);
                        tr_det_head.Cells.Add(tc_det_head_Ses);

                        TableCell tc_det_head_doc = new TableCell();
                        tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                        tc_det_head_doc.BorderWidth = 1;
                        Literal lit_det_head_doc = new Literal();
                        lit_det_head_doc.Text = "<b>Listed  Doctor Name</b>";
                        tc_det_head_doc.Controls.Add(lit_det_head_doc);
                        tr_det_head.Cells.Add(tc_det_head_doc);

                        TableCell tc_det_head_time = new TableCell();
                        tc_det_head_time.BorderStyle = BorderStyle.Solid;
                        tc_det_head_time.BorderWidth = 1;
                        Literal lit_det_head_time = new Literal();
                        lit_det_head_time.Text = "<b>Time</b>";
                        tc_det_head_time.Controls.Add(lit_det_head_time);
                        tr_det_head.Cells.Add(tc_det_head_time);

                        TableCell tc_det_head_ww = new TableCell();
                        tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                        tc_det_head_ww.BorderWidth = 1;
                        Literal lit_det_head_ww = new Literal();
                        lit_det_head_ww.Text = "<b>Worked With</b>";
                        tc_det_head_ww.Controls.Add(lit_det_head_ww);
                        tr_det_head.Cells.Add(tc_det_head_ww);

                        TableCell tc_det_head_visit = new TableCell();
                        tc_det_head_visit.BorderStyle = BorderStyle.Solid;
                        tc_det_head_visit.BorderWidth = 1;
                        Literal lit_det_head_visit = new Literal();
                        lit_det_head_visit.Text = "<b>Latest Visit</b>";
                        tc_det_head_visit.Controls.Add(lit_det_head_visit);
                        tr_det_head.Cells.Add(tc_det_head_visit);

                        TableCell tc_det_head_catg = new TableCell();
                        tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                        tc_det_head_catg.BorderWidth = 1;
                        Literal lit_det_head_catg = new Literal();
                        lit_det_head_catg.Text = "<b>Category</b>";
                        tc_det_head_catg.Controls.Add(lit_det_head_catg);
                        tr_det_head.Cells.Add(tc_det_head_catg);

                        TableCell tc_det_head_spec = new TableCell();
                        tc_det_head_spec.BorderStyle = BorderStyle.Solid;
                        tc_det_head_spec.BorderWidth = 1;
                        Literal lit_det_head_spec = new Literal();
                        lit_det_head_spec.Text = "<b>Speciality</b>";
                        tc_det_head_spec.Controls.Add(lit_det_head_spec);
                        tr_det_head.Cells.Add(tc_det_head_spec);

                        TableCell tc_det_head_prod = new TableCell();
                        tc_det_head_prod.BorderStyle = BorderStyle.Solid;
                        tc_det_head_prod.BorderWidth = 1;
                        Literal lit_det_head_prod = new Literal();
                        lit_det_head_prod.Text = "<b>Product Sampled</b>";
                        tc_det_head_prod.Controls.Add(lit_det_head_prod);
                        tr_det_head.Cells.Add(tc_det_head_prod);

                        TableCell tc_det_head_gift = new TableCell();
                        tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                        tc_det_head_gift.BorderWidth = 1;
                        Literal lit_det_head_gift = new Literal();
                        lit_det_head_gift.Text = "<b>Gift</b>";
                        tc_det_head_gift.Controls.Add(lit_det_head_gift);
                        tr_det_head.Cells.Add(tc_det_head_gift);

                        tbldetail.Rows.Add(tr_det_head);

                        iCount = 0;
                        foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                        {                                
                            TableRow tr_det_sno = new TableRow();
                            TableCell tc_det_SNo = new TableCell();
                            iCount += 1;
                            Literal lit_det_SNo = new Literal();
                            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                            tc_det_SNo.BorderStyle = BorderStyle.Solid;
                            tc_det_SNo.BorderWidth = 1;
                            tc_det_SNo.Controls.Add(lit_det_SNo);
                            tr_det_sno.Cells.Add(tc_det_SNo);

                            TableCell tc_det_Ses = new TableCell();
                            Literal lit_det_Ses = new Literal();
                            lit_det_Ses.Text = ""; // drdoctor["ses"].ToString();
                            tc_det_Ses.BorderStyle = BorderStyle.Solid;
                            tc_det_Ses.BorderWidth = 1;
                            tc_det_Ses.Controls.Add(lit_det_Ses);
                            tr_det_sno.Cells.Add(tc_det_Ses);

                            TableCell tc_det_dr_name = new TableCell();
                            Literal lit_det_dr_name = new Literal();
                            lit_det_dr_name.Text = drdoctor["ListedDr_Name"].ToString();
                            tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_name.BorderWidth = 1;
                            tc_det_dr_name.Controls.Add(lit_det_dr_name);
                            tr_det_sno.Cells.Add(tc_det_dr_name);

                            TableCell tc_det_time = new TableCell();
                            Literal lit_det_time = new Literal();
                            lit_det_time.Text = ""; // drdoctor["time"].ToString();
                            tc_det_time.BorderStyle = BorderStyle.Solid;
                            tc_det_time.BorderWidth = 1;
                            tc_det_time.Controls.Add(lit_det_time);
                            tr_det_sno.Cells.Add(tc_det_time);

                            TableCell tc_det_work = new TableCell();
                            Literal lit_det_work = new Literal();
                            lit_det_work.Text = drdoctor["Worked_with_Name"].ToString();
                            tc_det_work.BorderStyle = BorderStyle.Solid;
                            tc_det_work.BorderWidth = 1;
                            tc_det_work.Controls.Add(lit_det_work);
                            tr_det_sno.Cells.Add(tc_det_work);

                            TableCell tc_det_lvisit = new TableCell();
                            Literal lit_det_lvisit = new Literal();
                            lit_det_lvisit.Text = ""; // drdoctor["lvisit"].ToString();
                            tc_det_lvisit.BorderStyle = BorderStyle.Solid;
                            tc_det_lvisit.BorderWidth = 1;
                            tc_det_lvisit.Controls.Add(lit_det_lvisit);
                            tr_det_sno.Cells.Add(tc_det_lvisit);

                            TableCell tc_det_catg = new TableCell();
                            Literal lit_det_catg = new Literal();
                            lit_det_catg.Text = drdoctor["Doc_Cat_Name"].ToString();
                            tc_det_catg.BorderStyle = BorderStyle.Solid;
                            tc_det_catg.BorderWidth = 1;
                            tc_det_catg.Controls.Add(lit_det_catg);
                            tr_det_sno.Cells.Add(tc_det_catg);

                            TableCell tc_det_spec = new TableCell();
                            Literal lit_det_spec = new Literal();
                            lit_det_spec.Text = drdoctor["Doc_Special_Name"].ToString();
                            tc_det_spec.BorderStyle = BorderStyle.Solid;
                            tc_det_spec.BorderWidth = 1;
                            tc_det_spec.Controls.Add(lit_det_spec);
                            tr_det_sno.Cells.Add(tc_det_spec);

                            TableCell tc_det_prod = new TableCell();
                            Literal lit_det_prod = new Literal();
                            lit_det_prod.Text = drdoctor["Product_Detail"].ToString().Replace("~","(").Trim();
                            lit_det_prod.Text = lit_det_prod.Text.Replace("$", ")").Trim();
                            lit_det_prod.Text = lit_det_prod.Text.Replace("#", "  ").Trim();
                            tc_det_prod.BorderStyle = BorderStyle.Solid;
                            tc_det_prod.BorderWidth = 1;
                            tc_det_prod.Controls.Add(lit_det_prod);
                            tr_det_sno.Cells.Add(tc_det_prod);

                            TableCell tc_det_gift = new TableCell();
                            Literal lit_det_gift = new Literal();
                            lit_det_gift.Text = drdoctor["Gift_Detail"].ToString().Replace("~","-").Trim();
                            tc_det_gift.BorderStyle = BorderStyle.Solid;
                            tc_det_gift.BorderWidth = 1;
                            tc_det_gift.Controls.Add(lit_det_gift);
                            tr_det_sno.Cells.Add(tc_det_gift);

                            tbldetail.Rows.Add(tr_det_sno);
                        }

                        //Empty Row
                        TableRow tr_det_empty = new TableRow();
                        TableCell tc_det_empty = new TableCell();
                        Literal lit_det_empty = new Literal();
                        lit_det_empty.Text = "<BR>";
                        //tc_det_empty.BorderStyle = BorderStyle.Solid;
                        //tc_det_empty.BorderWidth = 1;
                        tc_det_empty.ColumnSpan = 10;
                        tc_det_empty.Controls.Add(lit_det_empty);
                        tr_det_empty.Cells.Add(tc_det_empty);
                        tbldetail.Rows.Add(tr_det_empty);
                    
                    }

                    tc_det.HorizontalAlign = HorizontalAlign.Center;
                    tc_day.Controls.Add(tbldetail);
                    tr_day.Cells.Add(tc_det);
                    tbl.Rows.Add(tr_day);

                    //2-Chemists

                    TableRow tr_chedet = new TableRow();
                    TableCell tc_chedet = new TableCell();
                    //tc_chedet.BorderStyle = BorderStyle.Solid;
                    //tc_chedet.BorderWidth = 1;
                    tc_chedet.Width = 400;
                    tc_chedet.ColumnSpan = 2;

                    Table tbldetailChe = new Table();
                    tbldetailChe.BorderStyle = BorderStyle.Solid;
                    tbldetailChe.BorderWidth = 1;
                    tbldetailChe.GridLines = GridLines.Both;
                    tbldetailChe.Width = 1000;

                    dsdoc = dc.get_dcr_che_details(sf_code, drdoc["Activity_Date"].ToString(), 2); //2-Chemists
                    if (dsdoc.Tables[0].Rows.Count > 0)
                    {
                        TableRow tr_det_head = new TableRow();
                        TableCell tc_det_head_SNo = new TableCell();
                        tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_head_SNo.BorderWidth = 1;
                        Literal lit_det_head_SNo = new Literal();
                        lit_det_head_SNo.Text = "<b>S.No</b>";
                        tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                        tr_det_head.Cells.Add(tc_det_head_SNo);

                        TableCell tc_det_head_doc = new TableCell();
                        tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                        tc_det_head_doc.BorderWidth = 1;
                        Literal lit_det_head_doc = new Literal();
                        lit_det_head_doc.Text = "<b>Chemists Name</b>";
                        tc_det_head_doc.Controls.Add(lit_det_head_doc);
                        tr_det_head.Cells.Add(tc_det_head_doc);

                        TableCell tc_det_head_ww = new TableCell();
                        tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                        tc_det_head_ww.BorderWidth = 1;
                        Literal lit_det_head_ww = new Literal();
                        lit_det_head_ww.Text = "<b>Worked With</b>";
                        tc_det_head_ww.Controls.Add(lit_det_head_ww);
                        tr_det_head.Cells.Add(tc_det_head_ww);

                        TableCell tc_det_head_catg = new TableCell();
                        tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                        tc_det_head_catg.BorderWidth = 1;
                        Literal lit_det_head_catg = new Literal();
                        lit_det_head_catg.Text = "<b>POB</b>";
                        tc_det_head_catg.Controls.Add(lit_det_head_catg);
                        tr_det_head.Cells.Add(tc_det_head_catg);


                        tbldetailChe.Rows.Add(tr_det_head);

                        iCount = 0;
                        foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                        {
                            TableRow tr_det_sno = new TableRow();
                            TableCell tc_det_SNo = new TableCell();
                            iCount += 1;
                            Literal lit_det_SNo = new Literal();
                            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                            tc_det_SNo.BorderStyle = BorderStyle.Solid;
                            tc_det_SNo.BorderWidth = 1;
                            tc_det_SNo.Controls.Add(lit_det_SNo);
                            tr_det_sno.Cells.Add(tc_det_SNo);

                          
                            TableCell tc_det_dr_name = new TableCell();
                            Literal lit_det_dr_name = new Literal();
                            lit_det_dr_name.Text = drdoctor["Chemists_Name"].ToString();
                            tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_name.BorderWidth = 1;
                            tc_det_dr_name.Controls.Add(lit_det_dr_name);
                            tr_det_sno.Cells.Add(tc_det_dr_name);

                         
                            TableCell tc_det_work = new TableCell();
                            Literal lit_det_work = new Literal();
                            lit_det_work.Text = drdoctor["Worked_with_Name"].ToString();
                            tc_det_work.BorderStyle = BorderStyle.Solid;
                            tc_det_work.BorderWidth = 1;
                            tc_det_work.Controls.Add(lit_det_work);
                            tr_det_sno.Cells.Add(tc_det_work);

                          
                            TableCell tc_det_spec = new TableCell();
                            Literal lit_det_spec = new Literal();
                            lit_det_spec.Text = drdoctor["POB"].ToString();
                            tc_det_spec.BorderStyle = BorderStyle.Solid;
                            tc_det_spec.BorderWidth = 1;
                            tc_det_spec.Controls.Add(lit_det_spec);
                            tr_det_sno.Cells.Add(tc_det_spec);
                            
                            tbldetailChe.Rows.Add(tr_det_sno);
                        }

                        //Empty Row
                        TableRow tr_chem_det_empty = new TableRow();
                        TableCell tc_chem_det_empty = new TableCell();
                        Literal lit_chem_det_empty = new Literal();
                        lit_chem_det_empty.Text = "<BR>";
                        tc_chem_det_empty.ColumnSpan = 10;
                        tc_chem_det_empty.Controls.Add(lit_chem_det_empty);
                        tr_chem_det_empty.Cells.Add(tc_chem_det_empty);
                        tbldetailChe.Rows.Add(tr_chem_det_empty);
                    }

                    tc_chedet.HorizontalAlign = HorizontalAlign.Center;
                    tc_day.Controls.Add(tbldetailChe);
                    tr_day.Cells.Add(tc_chedet);
                    tbl.Rows.Add(tr_day);
                    
                    ////5-Hospitals

                    //TableRow tr_hosdet = new TableRow();
                    //TableCell tc_hosdet = new TableCell();
                    //tc_hosdet.BorderStyle = BorderStyle.None;
                    //tc_hosdet.Width = 400;
                    //tc_hosdet.ColumnSpan = 2;

                    //Table tbldetailhos = new Table();
                    //tbldetailhos.BorderStyle = BorderStyle.Solid;
                    //tbldetailhos.BorderWidth = 1;
                    //tbldetailhos.GridLines = GridLines.Both;
                    //tbldetailhos.Width = 1000;

                    //dsdoc = dc.get_dcr_hos_details(sf_code, imonth, iyear, 5); //5-Hospital
                    //if (dsdoc.Tables[0].Rows.Count > 0)
                    //{
                    //    TableRow tr_det_head = new TableRow();
                    //    TableCell tc_det_head_SNo = new TableCell();
                    //    tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                    //    tc_det_head_SNo.BorderWidth = 1;
                    //    Literal lit_det_head_SNo = new Literal();
                    //    lit_det_head_SNo.Text = "<b>S.No</b>";
                    //    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                    //    tr_det_head.Cells.Add(tc_det_head_SNo);

                    //    TableCell tc_det_head_doc = new TableCell();
                    //    tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                    //    tc_det_head_doc.BorderWidth = 1;
                    //    Literal lit_det_head_doc = new Literal();
                    //    lit_det_head_doc.Text = "<b>Hospital Name</b>";
                    //    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                    //    tr_det_head.Cells.Add(tc_det_head_doc);

                    //    TableCell tc_det_head_ww = new TableCell();
                    //    tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                    //    tc_det_head_ww.BorderWidth = 1;
                    //    Literal lit_det_head_ww = new Literal();
                    //    lit_det_head_ww.Text = "<b>Worked With</b>";
                    //    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                    //    tr_det_head.Cells.Add(tc_det_head_ww);

                    //    TableCell tc_det_head_catg = new TableCell();
                    //    tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                    //    tc_det_head_catg.BorderWidth = 1;
                    //    Literal lit_det_head_catg = new Literal();
                    //    lit_det_head_catg.Text = "<b>POB</b>";
                    //    tc_det_head_catg.Controls.Add(lit_det_head_catg);
                    //    tr_det_head.Cells.Add(tc_det_head_catg);


                    //    tbldetailhos.Rows.Add(tr_det_head);

                    //    iCount = 0;
                    //    foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                    //    {
                    //        TableRow tr_det_sno = new TableRow();
                    //        TableCell tc_det_SNo = new TableCell();
                    //        iCount += 1;
                    //        Literal lit_det_SNo = new Literal();
                    //        lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                    //        tc_det_SNo.BorderStyle = BorderStyle.Solid;
                    //        tc_det_SNo.BorderWidth = 1;
                    //        tc_det_SNo.Controls.Add(lit_det_SNo);
                    //        tr_det_sno.Cells.Add(tc_det_SNo);


                    //        TableCell tc_det_dr_name = new TableCell();
                    //        Literal lit_det_dr_name = new Literal();
                    //        lit_det_dr_name.Text = drdoctor["Hospital_Name"].ToString();
                    //        tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                    //        tc_det_dr_name.BorderWidth = 1;
                    //        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                    //        tr_det_sno.Cells.Add(tc_det_dr_name);


                    //        TableCell tc_det_work = new TableCell();
                    //        Literal lit_det_work = new Literal();
                    //        lit_det_work.Text = drdoctor["Worked_with_Name"].ToString();
                    //        tc_det_work.BorderStyle = BorderStyle.Solid;
                    //        tc_det_work.BorderWidth = 1;
                    //        tc_det_work.Controls.Add(lit_det_work);
                    //        tr_det_sno.Cells.Add(tc_det_work);


                    //        TableCell tc_det_spec = new TableCell();
                    //        Literal lit_det_spec = new Literal();
                    //        lit_det_spec.Text = drdoctor["POB"].ToString();
                    //        tc_det_spec.BorderStyle = BorderStyle.Solid;
                    //        tc_det_spec.BorderWidth = 1;
                    //        tc_det_spec.Controls.Add(lit_det_spec);
                    //        tr_det_sno.Cells.Add(tc_det_spec);

                    //        tbldetailhos.Rows.Add(tr_det_sno);
                    //    }
                    //}

                    //tc_hosdet.HorizontalAlign = HorizontalAlign.Center;
                    //tc_day.Controls.Add(tbldetailhos);
                    //tr_day.Cells.Add(tc_hosdet);
                    //tbl.Rows.Add(tr_day);
                }
        }
    }

    private void CreateDynamicNotApproval(int imonth, int iyear, string sf_code)
    {
        try
        {

        }
        catch (Exception ex)
        {

        }
    }

    private int getmaxdays_month(int imonth)
    {
        int idays = -1;

        if (imonth == 1)
            idays = 31;
        else if (imonth == 2)
            idays = 28;
        else if (imonth == 3)
            idays = 31;
        else if (imonth == 4)
            idays = 30;
        else if (imonth == 5)
            idays = 31;
        else if (imonth == 6)
            idays = 30;
        else if (imonth == 7)
            idays = 31;
        else if (imonth == 8)
            idays = 31;
        else if (imonth == 9)
            idays = 30;
        else if (imonth == 10)
            idays = 31;
        else if (imonth == 11)
            idays = 30;
        else if (imonth == 12)
            idays = 31;

        return idays;
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
        string attachment = "attachment; filename=DCRView.xls";
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
        string strFileName = "rptDCRView";
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