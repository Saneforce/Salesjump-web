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

public partial class Reports_rptDcrViewDetails : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDCR = null;
    DataSet dsdoc = null;
    DataSet dssf = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string Sf_Name = string.Empty;
    string strMode = string.Empty;
    string sURL = string.Empty;
    string Day = string.Empty;
    string StrDate = string.Empty;
    string Sf_HQ = string.Empty;
    int cmonth = -1;
    int cyear = -1;
    int tot_days = -1;
    int cday = 1;
    int iCount = -1;
    string sDCR = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string strDateMatch = string.Empty;
            string strMonth = string.Empty;
            div_code = Request.QueryString["div_code"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            cmonth = Convert.ToInt16(Request.QueryString["Month"].ToString());
            cyear = Convert.ToInt16(Request.QueryString["Year"].ToString());
            Day = Request.QueryString["Day"].ToString();
            if (Day.Length != 2)
            {
                strDateMatch = "0" + Day;
            }
            else
            {
                strDateMatch = Day;
            }

            if (cmonth.ToString().Length != 2)
            {
                strMonth = "0" + cmonth;
            }
            else
            {
                strMonth = Convert.ToString(cmonth);
            }

            StrDate = strDateMatch + "/" + strMonth + "/" + cyear;


            CreateDynamicTable(cmonth, cyear, sf_code);

        }
        catch (Exception ex)
        {
            Response.Redirect(ex.Message);
        }
    }

    private void CreateDynamicTable(int imonth, int iyear, string sf_code)
    {
        DCR dc = new DCR();

        //dsDCR = dc.get_dcr_date(sf_code, imonth, iyear);
        //if (dsDCR.Tables[0].Rows.Count > 0)
        //{
            DCR dcsf = new DCR();
            dssf = dcsf.getSfName_HQ(sf_code);

            if (dssf.Tables[0].Rows.Count > 0)
            {
                Sf_Name = dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                Sf_HQ = dssf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            }

            //foreach (DataRow drdoc in dsDCR.Tables[0].Rows)
            //{

                Table tbldetail_main3 = new Table();
                tbldetail_main3.BorderStyle = BorderStyle.None;
                tbldetail_main3.Width = 1100;
                TableRow tr_det_head_main3 = new TableRow();
                TableCell tc_det_head_main3 = new TableCell();
                tc_det_head_main3.Width = 100;
                Literal lit_det_main3 = new Literal();
                lit_det_main3.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_main3.Controls.Add(lit_det_main3);
                tr_det_head_main3.Cells.Add(tc_det_head_main3);

                TableCell tc_det_head_main4 = new TableCell();
                tc_det_head_main4.Width = 1000;


                Table tbl = new Table();
                tbl.Width = 1000;
                //tbl.Style.Add("Align", "Center");
                TableRow tr_day = new TableRow();
                TableCell tc_day = new TableCell();
                tc_day.BorderStyle = BorderStyle.None;
                tc_day.ColumnSpan = 2;
                tc_day.HorizontalAlign = HorizontalAlign.Center;
                tc_day.Style.Add("font-name", "verdana;");
                Literal lit_day = new Literal();
                lit_day.Text = "<u><b>Daily Call Report - " + "<span style='color:Red'>" + StrDate + "</span>" + "</b></u>";
                tc_day.Controls.Add(lit_day);
                tr_day.Cells.Add(tc_day);
                tbl.Rows.Add(tr_day);

                TableRow tr_ff = new TableRow();
                TableCell tc_ff_name = new TableCell();
                tc_ff_name.BorderStyle = BorderStyle.None;
                tc_ff_name.Width = 500;
                Literal lit_ff_name = new Literal();
                lit_ff_name.Text = "<b>Field Force Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_Name.ToString();
                tc_ff_name.Controls.Add(lit_ff_name);
                tr_ff.Cells.Add(tc_ff_name);

                TableCell tc_HQ = new TableCell();
                tc_HQ.BorderStyle = BorderStyle.None;
                tc_HQ.Width = 500;
                tc_HQ.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_HQ = new Literal();
                lit_HQ.Text = "<b   >Head Quarters</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_HQ.ToString();
                tc_HQ.Controls.Add(lit_HQ);
                tr_ff.Cells.Add(tc_HQ);

                tbl.Rows.Add(tr_ff);

                TableRow tr_dcr = new TableRow();
                //TableCell tc_dcr_submit = new TableCell();
                //tc_dcr_submit.BorderStyle = BorderStyle.None;
                //tc_dcr_submit.Width = 500;
                //Literal lit_dcr_submit = new Literal();
                //lit_dcr_submit.Text = "<b>DCR Submitted on</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoc["Submission_Date"].ToString();
                //tc_dcr_submit.Controls.Add(lit_dcr_submit);
                //tr_dcr.Cells.Add(tc_dcr_submit);

                //TableCell tc_Terr = new TableCell();
                //tc_Terr.BorderStyle = BorderStyle.None;
                //tc_Terr.HorizontalAlign = HorizontalAlign.Right;
                //tc_Terr.Width = 500;
                //Literal lit_Terr = new Literal();
                //lit_Terr.Text = "<b>Territory Worked</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoc["Plan_Name"].ToString(); ;
                //tc_Terr.Controls.Add(lit_Terr);
                //tr_dcr.Cells.Add(tc_Terr);

                tbl.Rows.Add(tr_dcr);

                tc_det_head_main4.Controls.Add(tbl);
                tr_det_head_main3.Cells.Add(tc_det_head_main4);
                tbldetail_main3.Rows.Add(tr_det_head_main3);

                form1.Controls.Add(tbldetail_main3);

                Table tbl_head_empty = new Table();
                TableRow tr_head_empty = new TableRow();
                TableCell tc_head_empty = new TableCell();
                Literal lit_head_empty = new Literal();
                lit_head_empty.Text = "<BR>";
                tc_head_empty.Controls.Add(lit_head_empty);
                tr_head_empty.Cells.Add(tc_head_empty);
                tbl_head_empty.Rows.Add(tr_head_empty);
                form1.Controls.Add(tbl_head_empty);

                Table tbldetail_main = new Table();
                tbldetail_main.BorderStyle = BorderStyle.None;
                tbldetail_main.Width = 1100;
                TableRow tr_det_head_main = new TableRow();
                TableCell tc_det_head_main = new TableCell();
                tc_det_head_main.Width = 100;
                Literal lit_det_main = new Literal();
                lit_det_main.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_main.Controls.Add(lit_det_main);
                tr_det_head_main.Cells.Add(tc_det_head_main);

                TableCell tc_det_head_main2 = new TableCell();
                tc_det_head_main2.Width = 1000;

                Table tbldetail = new Table();
                tbldetail.BorderStyle = BorderStyle.Solid;
                tbldetail.BorderWidth = 1;
                tbldetail.GridLines = GridLines.Both;
                tbldetail.Width = 1000;
                tbldetail.Style.Add("border-collapse", "collapse");
                tbldetail.Style.Add("border", "solid 1px Black");
                

                dsdoc = dc.get_Pending_dcrLstDOC_details(sf_code, StrDate, 1); //1-Listed Doctor
                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_det_head = new TableRow();
                    TableCell tc_det_head_SNo = new TableCell();
                    tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_head_SNo.BorderWidth = 1;
                    tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_SNo = new Literal();                    
                    tc_det_head_SNo.Attributes.Add("Class", "tblHead");
                    lit_det_head_SNo.Text = "<b>S.No</b>";
                    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                    tr_det_head.Cells.Add(tc_det_head_SNo);

                    TableCell tc_det_head_Ses = new TableCell();
                    tc_det_head_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_head_Ses.BorderWidth = 1;
                    tc_det_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_Ses = new Literal();
                    tc_det_head_Ses.Attributes.Add("Class", "tblHead");
                    lit_det_head_Ses.Text = "<b>Ses</b>";
                    tc_det_head_Ses.Controls.Add(lit_det_head_Ses);
                    tr_det_head.Cells.Add(tc_det_head_Ses);

                    TableCell tc_det_head_doc = new TableCell();
                    tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                    tc_det_head_doc.BorderWidth = 1;
                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_doc = new Literal();
                    lit_det_head_doc.Text = "<b>Listed  Doctor Name</b>";
                    tc_det_head_doc.Attributes.Add("Class", "tblHead");
                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                    tr_det_head.Cells.Add(tc_det_head_doc);

                    TableCell tc_det_head_time = new TableCell();
                    tc_det_head_time.BorderStyle = BorderStyle.Solid;
                    tc_det_head_time.BorderWidth = 1;
                    tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_time = new Literal();
                    lit_det_head_time.Text = "<b>Time</b>";
                    tc_det_head_time.Attributes.Add("Class", "tblHead");
                    tc_det_head_time.Controls.Add(lit_det_head_time);
                    tr_det_head.Cells.Add(tc_det_head_time);

                    TableCell tc_det_head_ww = new TableCell();
                    tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_head_ww.BorderWidth = 1;
                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_ww = new Literal();
                    lit_det_head_ww.Text = "<b>Worked With</b>";
                    tc_det_head_ww.Attributes.Add("Class", "tblHead");
                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                    tr_det_head.Cells.Add(tc_det_head_ww);

                    TableCell tc_det_head_visit = new TableCell();
                    tc_det_head_visit.BorderStyle = BorderStyle.Solid;
                    tc_det_head_visit.BorderWidth = 1;
                    tc_det_head_visit.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_visit = new Literal();
                    lit_det_head_visit.Text = "<b>Latest Visit</b>";
                    tc_det_head_visit.Attributes.Add("Class", "tblHead");
                    tc_det_head_visit.Controls.Add(lit_det_head_visit);
                    tr_det_head.Cells.Add(tc_det_head_visit);

                    TableCell tc_det_head_catg = new TableCell();
                    tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                    tc_det_head_catg.BorderWidth = 1;
                    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_catg = new Literal();
                    lit_det_head_catg.Text = "<b>Category</b>";
                    tc_det_head_catg.Attributes.Add("Class", "tblHead");
                    tc_det_head_catg.Controls.Add(lit_det_head_catg);
                    tr_det_head.Cells.Add(tc_det_head_catg);

                    TableCell tc_det_head_spec = new TableCell();
                    tc_det_head_spec.BorderStyle = BorderStyle.Solid;
                    tc_det_head_spec.BorderWidth = 1;
                    tc_det_head_spec.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_spec = new Literal();
                    lit_det_head_spec.Text = "<b>Speciality</b>";
                    tc_det_head_spec.Attributes.Add("Class", "tblHead");
                    tc_det_head_spec.Controls.Add(lit_det_head_spec);
                    tr_det_head.Cells.Add(tc_det_head_spec);

                    TableCell tc_det_head_prod = new TableCell();
                    tc_det_head_prod.BorderStyle = BorderStyle.Solid;
                    tc_det_head_prod.BorderWidth = 1;
                    tc_det_head_prod.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_prod = new Literal();
                    lit_det_head_prod.Text = "<b>Product Sampled</b>";
                    tc_det_head_prod.Attributes.Add("Class", "tblHead");
                    tc_det_head_prod.Controls.Add(lit_det_head_prod);
                    tr_det_head.Cells.Add(tc_det_head_prod);

                    TableCell tc_det_head_gift = new TableCell();
                    tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                    tc_det_head_gift.BorderWidth = 1;
                    tc_det_head_gift.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_gift = new Literal();
                    lit_det_head_gift.Text = "<b>Gift</b>";
                    tc_det_head_gift.Attributes.Add("Class", "tblHead");
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
                        lit_det_Ses.Text = "&nbsp;&nbsp;" + drdoctor["Session"].ToString();
                        tc_det_Ses.BorderStyle = BorderStyle.Solid;
                        tc_det_Ses.BorderWidth = 1;
                        tc_det_Ses.Controls.Add(lit_det_Ses);
                        tr_det_sno.Cells.Add(tc_det_Ses);

                        TableCell tc_det_dr_name = new TableCell();
                        Literal lit_det_dr_name = new Literal();
                        lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["ListedDr_Name"].ToString();
                        tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                        tc_det_dr_name.BorderWidth = 1;
                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                        tr_det_sno.Cells.Add(tc_det_dr_name);

                        TableCell tc_det_time = new TableCell();
                        Literal lit_det_time = new Literal();
                        lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor["Time"].ToString();
                        tc_det_time.BorderStyle = BorderStyle.Solid;
                        tc_det_time.BorderWidth = 1;
                        tc_det_time.Controls.Add(lit_det_time);
                        tr_det_sno.Cells.Add(tc_det_time);

                        TableCell tc_det_work = new TableCell();
                        Literal lit_det_work = new Literal();
                        lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
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
                        lit_det_catg.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Cat_Name"].ToString();
                        tc_det_catg.BorderStyle = BorderStyle.Solid;
                        tc_det_catg.BorderWidth = 1;
                        tc_det_catg.Controls.Add(lit_det_catg);
                        tr_det_sno.Cells.Add(tc_det_catg);

                        TableCell tc_det_spec = new TableCell();
                        Literal lit_det_spec = new Literal();
                        lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Special_Name"].ToString();
                        tc_det_spec.BorderStyle = BorderStyle.Solid;
                        tc_det_spec.BorderWidth = 1;
                        tc_det_spec.Controls.Add(lit_det_spec);
                        tr_det_sno.Cells.Add(tc_det_spec);

                        TableCell tc_det_prod = new TableCell();
                        Literal lit_det_prod = new Literal();
                        lit_det_prod.Text = drdoctor["Product_Detail"].ToString().Replace("~", "(").Trim();
                        lit_det_prod.Text = lit_det_prod.Text.Replace("$", ")").Trim();
                        lit_det_prod.Text = "&nbsp;&nbsp;" + lit_det_prod.Text.Replace("#", "  ").Trim();
                        tc_det_prod.BorderStyle = BorderStyle.Solid;
                        tc_det_prod.BorderWidth = 1;
                        tc_det_prod.Controls.Add(lit_det_prod);
                        tr_det_sno.Cells.Add(tc_det_prod);

                        TableCell tc_det_gift = new TableCell();
                        Literal lit_det_gift = new Literal();
                        //lit_det_gift.Text = "&nbsp;&nbsp;" + drdoctor["Gift_Detail"].ToString().Replace("~", "-").Trim();
                        lit_det_gift.Text = "&nbsp;&nbsp;" + drdoctor["Gift_Name"].ToString().Replace("~", "-").Trim();
                        tc_det_gift.BorderStyle = BorderStyle.Solid;
                        tc_det_gift.BorderWidth = 1;
                        tc_det_gift.Controls.Add(lit_det_gift);
                        tr_det_sno.Cells.Add(tc_det_gift);

                        tbldetail.Rows.Add(tr_det_sno);
                    }
                }

                //form1.Controls.Add(tbldetail);

                tc_det_head_main2.Controls.Add(tbldetail);
                tr_det_head_main.Cells.Add(tc_det_head_main2);
                tbldetail_main.Rows.Add(tr_det_head_main);

                form1.Controls.Add(tbldetail_main);

                if (iCount > 0)
                {
                    Table tbl_doc_empty = new Table();
                    TableRow tr_doc_empty = new TableRow();
                    TableCell tc_doc_empty = new TableCell();
                    Literal lit_doc_empty = new Literal();
                    lit_doc_empty.Text = "<BR>";
                    tc_doc_empty.Controls.Add(lit_doc_empty);
                    tr_doc_empty.Cells.Add(tc_doc_empty);
                    tbl_doc_empty.Rows.Add(tr_doc_empty);
                    form1.Controls.Add(tbl_doc_empty);
                }

                //2-Chemists

                Table tbldetail_main5 = new Table();
                tbldetail_main5.BorderStyle = BorderStyle.None;
                tbldetail_main5.Width = 1100;
                tbldetail.Style.Add("border-collapse", "collapse");
                tbldetail.Style.Add("border", "solid 1px Black");
                TableRow tr_det_head_main5 = new TableRow();
                TableCell tc_det_head_main5 = new TableCell();
                tc_det_head_main5.Width = 100;
                Literal lit_det_main5 = new Literal();
                lit_det_main5.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_main5.Controls.Add(lit_det_main5);
                tr_det_head_main5.Cells.Add(tc_det_head_main5);

                TableCell tc_det_head_main6 = new TableCell();
                tc_det_head_main6.Width = 1000;


                Table tbldetailChe = new Table();
                tbldetailChe.BorderStyle = BorderStyle.Solid;
                tbldetailChe.BorderWidth = 1;
                tbldetailChe.Style.Add("border-collapse", "collapse");
                tbldetailChe.Style.Add("border", "solid 1px Black");
                tbldetailChe.GridLines = GridLines.Both;
                tbldetailChe.Width = 600;

                dsdoc = dc.get_Pending_dcr_che_details(sf_code, StrDate, 2); //2-Chemists
                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_det_head = new TableRow();
                    TableCell tc_det_head_SNo = new TableCell();
                    tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_SNo.BorderWidth = 1;
                    Literal lit_det_head_SNo = new Literal();
                    lit_det_head_SNo.Text = "<b>S.No</b>";
                    tc_det_head_SNo.Attributes.Add("Class", "tblHead");
                    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                    tr_det_head.Cells.Add(tc_det_head_SNo);

                    TableCell tc_det_head_doc = new TableCell();
                    tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_doc.BorderWidth = 1;
                    Literal lit_det_head_doc = new Literal();
                    lit_det_head_doc.Text = "<b>Chemists Name</b>";
                    tc_det_head_doc.Attributes.Add("Class", "tblHead");
                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                    tr_det_head.Cells.Add(tc_det_head_doc);

                    TableCell tc_det_head_ww = new TableCell();
                    tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_ww.BorderWidth = 1;
                    Literal lit_det_head_ww = new Literal();
                    lit_det_head_ww.Text = "<b>Worked With</b>";
                    tc_det_head_ww.Attributes.Add("Class", "tblHead");
                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                    tr_det_head.Cells.Add(tc_det_head_ww);

                    TableCell tc_det_head_catg = new TableCell();
                    tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_catg.BorderWidth = 1;
                    Literal lit_det_head_catg = new Literal();
                    lit_det_head_catg.Text = "<b>POB</b>";
                    tc_det_head_catg.Attributes.Add("Class", "tblHead");
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
                        lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["Chemists_Name"].ToString();
                        tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                        tc_det_dr_name.BorderWidth = 1;
                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                        tr_det_sno.Cells.Add(tc_det_dr_name);

                        TableCell tc_det_work = new TableCell();
                        Literal lit_det_work = new Literal();
                        lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                        tc_det_work.BorderStyle = BorderStyle.Solid;
                        tc_det_work.BorderWidth = 1;
                        tc_det_work.Controls.Add(lit_det_work);
                        tr_det_sno.Cells.Add(tc_det_work);

                        TableCell tc_det_spec = new TableCell();
                        Literal lit_det_spec = new Literal();
                        lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                        tc_det_spec.BorderStyle = BorderStyle.Solid;
                        tc_det_spec.BorderWidth = 1;
                        tc_det_spec.Controls.Add(lit_det_spec);
                        tr_det_sno.Cells.Add(tc_det_spec);

                        tbldetailChe.Rows.Add(tr_det_sno);
                    }
                }

                //form1.Controls.Add(tbldetailChe);

                tc_det_head_main6.Controls.Add(tbldetailChe);
                tr_det_head_main5.Cells.Add(tc_det_head_main6);
                tbldetail_main5.Rows.Add(tr_det_head_main5);

                form1.Controls.Add(tbldetail_main5);


                if (iCount > 0)
                {
                    Table tbl_chem_empty = new Table();
                    TableRow tr_chem_empty = new TableRow();
                    TableCell tc_chem_empty = new TableCell();
                    Literal lit_chem_empty = new Literal();
                    lit_chem_empty.Text = "<BR>";
                    tc_chem_empty.Controls.Add(lit_chem_empty);
                    tr_chem_empty.Cells.Add(tc_chem_empty);
                    tbl_chem_empty.Rows.Add(tr_chem_empty);
                    form1.Controls.Add(tbl_chem_empty);
                }

                //4-UnListed Doctor

                Table tbldetail_main7 = new Table();
                tbldetail_main7.BorderStyle = BorderStyle.None;
                tbldetail_main7.Width = 1100;

                TableRow tr_det_head_main7 = new TableRow();
                TableCell tc_det_head_main7 = new TableCell();
                tc_det_head_main7.Width = 100;
                Literal lit_det_main7 = new Literal();
                lit_det_main7.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_main7.Controls.Add(lit_det_main7);
                tr_det_head_main7.Cells.Add(tc_det_head_main7);

                TableCell tc_det_head_main8 = new TableCell();
                tc_det_head_main8.Width = 1000;

                Table tblUnLstDoc = new Table();
                tblUnLstDoc.BorderStyle = BorderStyle.Solid;
                tblUnLstDoc.BorderWidth = 1;
                tblUnLstDoc.Style.Add("border-collapse", "collapse");
                tblUnLstDoc.Style.Add("border", "solid 1px Black");
                tblUnLstDoc.GridLines = GridLines.Both;
                tblUnLstDoc.Width = 1000;
                dsdoc = dc.get_Pending_unlst_doc_details(sf_code, StrDate, 4); //1-Listed Doctor
                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_UnLst_doc_head = new TableRow();
                    TableCell tc_UnLst_doc_head_SNo = new TableCell();
                    tc_UnLst_doc_head_SNo.BorderStyle = BorderStyle.Solid;
                    tc_UnLst_doc_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                    tc_UnLst_doc_head_SNo.BorderWidth = 1;
                    Literal lit_undet_head_SNo = new Literal();
                    lit_undet_head_SNo.Text = "<b>S.No</b>";
                    tc_UnLst_doc_head_SNo.Attributes.Add("Class", "tblHead");
                    tc_UnLst_doc_head_SNo.Controls.Add(lit_undet_head_SNo);
                    tr_UnLst_doc_head.Cells.Add(tc_UnLst_doc_head_SNo);

                    TableCell tc_undet_head_Ses = new TableCell();
                    tc_undet_head_Ses.BorderStyle = BorderStyle.Solid;
                    tc_undet_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                    tc_undet_head_Ses.BorderWidth = 1;
                    Literal lit_undet_head_Ses = new Literal();
                    lit_undet_head_Ses.Text = "<b>Ses</b>";
                    tc_undet_head_Ses.Attributes.Add("Class", "tblHead");
                    tc_undet_head_Ses.Controls.Add(lit_undet_head_Ses);
                    tr_UnLst_doc_head.Cells.Add(tc_undet_head_Ses);

                    TableCell tc_det_head_doc = new TableCell();
                    tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_doc.BorderWidth = 1;
                    Literal lit_det_head_doc = new Literal();
                    lit_det_head_doc.Text = "<b>UnListed  Doctor Name</b>";
                    tc_det_head_doc.Attributes.Add("Class", "tblHead");
                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                    tr_UnLst_doc_head.Cells.Add(tc_det_head_doc);

                    TableCell tc_det_head_time = new TableCell();
                    tc_det_head_time.BorderStyle = BorderStyle.Solid;
                    tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_time.BorderWidth = 1;
                    Literal lit_det_head_time = new Literal();
                    tc_det_head_time.Text = "<b>Time</b>";
                    tc_det_head_time.Attributes.Add("Class", "tblHead");
                    tc_det_head_time.Controls.Add(lit_det_head_time);
                    tr_UnLst_doc_head.Cells.Add(tc_det_head_time);

                    TableCell tc_det_head_ww = new TableCell();
                    tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_ww.BorderWidth = 1;
                    Literal lit_det_head_ww = new Literal();
                    lit_det_head_ww.Text = "<b>Worked With</b>";
                    tc_det_head_ww.Attributes.Add("Class", "tblHead");
                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                    tr_UnLst_doc_head.Cells.Add(tc_det_head_ww);

                    TableCell tc_det_head_visit = new TableCell();
                    tc_det_head_visit.BorderStyle = BorderStyle.Solid;
                    tc_det_head_visit.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_visit.BorderWidth = 1;
                    Literal lit_det_head_visit = new Literal();
                    lit_det_head_visit.Text = "<b>Latest Visit</b>";
                    tc_det_head_visit.Attributes.Add("Class", "tblHead");
                    tc_det_head_visit.Controls.Add(lit_det_head_visit);
                    tr_UnLst_doc_head.Cells.Add(tc_det_head_visit);

                    TableCell tc_det_head_catg = new TableCell();
                    tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_catg.BorderWidth = 1;
                    Literal lit_det_head_catg = new Literal();
                    lit_det_head_catg.Text = "<b>Category</b>";
                    tc_det_head_catg.Attributes.Add("Class", "tblHead");
                    tc_det_head_catg.Controls.Add(lit_det_head_catg);
                    tr_UnLst_doc_head.Cells.Add(tc_det_head_catg);

                    TableCell tc_det_head_spec = new TableCell();
                    tc_det_head_spec.BorderStyle = BorderStyle.Solid;
                    tc_det_head_spec.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_spec.BorderWidth = 1;
                    Literal lit_det_head_spec = new Literal();
                    lit_det_head_spec.Text = "<b>Speciality</b>";
                    tc_det_head_spec.Attributes.Add("Class", "tblHead");
                    tc_det_head_spec.Controls.Add(lit_det_head_spec);
                    tr_UnLst_doc_head.Cells.Add(tc_det_head_spec);

                    TableCell tc_det_head_prod = new TableCell();
                    tc_det_head_prod.BorderStyle = BorderStyle.Solid;
                    tc_det_head_prod.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_prod.BorderWidth = 1;
                    Literal lit_det_head_prod = new Literal();
                    lit_det_head_prod.Text = "<b>Product Sampled</b>";
                    tc_det_head_prod.Attributes.Add("Class", "tblHead");
                    tc_det_head_prod.Controls.Add(lit_det_head_prod);
                    tr_UnLst_doc_head.Cells.Add(tc_det_head_prod);

                    TableCell tc_det_head_gift = new TableCell();
                    tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                    tc_det_head_gift.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_gift.BorderWidth = 1;
                    Literal lit_det_head_gift = new Literal();
                    lit_det_head_gift.Text = "<b>Gift</b>";
                    tc_det_head_gift.Attributes.Add("Class", "tblHead");
                    tc_det_head_gift.Controls.Add(lit_det_head_gift);
                    tr_UnLst_doc_head.Cells.Add(tc_det_head_gift);

                    tblUnLstDoc.Rows.Add(tr_UnLst_doc_head);

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
                        lit_det_Ses.Text = "&nbsp;&nbsp;" + drdoctor["Session"].ToString();
                        tc_det_Ses.BorderStyle = BorderStyle.Solid;
                        tc_det_Ses.BorderWidth = 1;
                        tc_det_Ses.Controls.Add(lit_det_Ses);
                        tr_det_sno.Cells.Add(tc_det_Ses);

                        TableCell tc_det_dr_name = new TableCell();
                        Literal lit_det_dr_name = new Literal();
                        lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["UnListedDr_Name"].ToString();
                        tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                        tc_det_dr_name.BorderWidth = 1;
                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                        tr_det_sno.Cells.Add(tc_det_dr_name);

                        TableCell tc_det_time = new TableCell();
                        Literal lit_det_time = new Literal();
                        lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor["Time"].ToString();
                        tc_det_time.BorderStyle = BorderStyle.Solid;
                        tc_det_time.BorderWidth = 1;
                        tc_det_time.Controls.Add(lit_det_time);
                        tr_det_sno.Cells.Add(tc_det_time);

                        TableCell tc_det_work = new TableCell();
                        Literal lit_det_work = new Literal();
                        lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
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
                        lit_det_catg.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Cat_Name"].ToString();
                        tc_det_catg.BorderStyle = BorderStyle.Solid;
                        tc_det_catg.BorderWidth = 1;
                        tc_det_catg.Controls.Add(lit_det_catg);
                        tr_det_sno.Cells.Add(tc_det_catg);

                        TableCell tc_det_spec = new TableCell();
                        Literal lit_det_spec = new Literal();
                        lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Special_Name"].ToString();
                        tc_det_spec.BorderStyle = BorderStyle.Solid;
                        tc_det_spec.BorderWidth = 1;
                        tc_det_spec.Controls.Add(lit_det_spec);
                        tr_det_sno.Cells.Add(tc_det_spec);

                        TableCell tc_det_prod = new TableCell();
                        Literal lit_det_prod = new Literal();
                        lit_det_prod.Text = drdoctor["Product_Detail"].ToString().Replace("~", "(").Trim();
                        lit_det_prod.Text = lit_det_prod.Text.Replace("$", ")").Trim();
                        lit_det_prod.Text = "&nbsp;&nbsp;" + lit_det_prod.Text.Replace("#", "  ").Trim();
                        tc_det_prod.BorderStyle = BorderStyle.Solid;
                        tc_det_prod.BorderWidth = 1;
                        tc_det_prod.Controls.Add(lit_det_prod);
                        tr_det_sno.Cells.Add(tc_det_prod);

                        TableCell tc_det_gift = new TableCell();
                        Literal lit_det_gift = new Literal();
                        //lit_det_gift.Text = "&nbsp;&nbsp;" + drdoctor["Gift_Detail"].ToString().Replace("~", "-").Trim();
                        lit_det_gift.Text = "&nbsp;&nbsp;" + drdoctor["Gift_Name"].ToString().Replace("~", "-").Trim();
                        tc_det_gift.BorderStyle = BorderStyle.Solid;
                        tc_det_gift.BorderWidth = 1;
                        tc_det_gift.Controls.Add(lit_det_gift);
                        tr_det_sno.Cells.Add(tc_det_gift);

                        tblUnLstDoc.Rows.Add(tr_det_sno);
                    }
                }

                //form1.Controls.Add(tblUnLstDoc);

                tc_det_head_main8.Controls.Add(tblUnLstDoc);
                tr_det_head_main7.Cells.Add(tc_det_head_main8);
                tbldetail_main7.Rows.Add(tr_det_head_main7);

                form1.Controls.Add(tbldetail_main7);


                if (iCount > 0)
                {
                    Table tbl_undoc_empty = new Table();
                    TableRow tr_undoc_empty = new TableRow();
                    TableCell tc_undoc_empty = new TableCell();
                    Literal lit_undoc_empty = new Literal();
                    lit_undoc_empty.Text = "<BR>";
                    tc_undoc_empty.Controls.Add(lit_undoc_empty);
                    tr_undoc_empty.Cells.Add(tc_undoc_empty);
                    tbl_undoc_empty.Rows.Add(tr_undoc_empty);
                    form1.Controls.Add(tbl_undoc_empty);
                }

                // 3- Stockist

                //5-Hospitals

                Table tbldetail_main11 = new Table();
                tbldetail_main11.BorderStyle = BorderStyle.None;
                tbldetail_main11.Width = 1100;               
                TableRow tr_det_head_main11 = new TableRow();
                TableCell tc_det_head_main11 = new TableCell();
                tr_det_head_main11.Width = 100;
                Literal lit_det_main11 = new Literal();
                lit_det_main11.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_main11.Controls.Add(lit_det_main11);
                tr_det_head_main11.Cells.Add(tc_det_head_main11);

                TableCell tc_det_head_main12 = new TableCell();
                tc_det_head_main12.Width = 1000;
                Table tbldetailstk = new Table();
                tbldetailstk.BorderStyle = BorderStyle.Solid;
                tbldetailstk.BorderWidth = 1;
                tbldetailstk.Style.Add("border-collapse", "collapse");
                tbldetailstk.Style.Add("border", "solid 1px Black");
                tbldetailstk.GridLines = GridLines.Both;
                tbldetailstk.Width = 1000;

                dsdoc = dc.get_Pending_dcr_stk_details(sf_code, StrDate, 3); //3-Stockist
                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_det_head = new TableRow();
                    TableCell tc_det_head_SNo = new TableCell();
                    tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_SNo.BorderWidth = 1;
                    Literal lit_det_head_SNo = new Literal();
                    lit_det_head_SNo.Text = "<b>S.No</b>";
                    tc_det_head_SNo.Attributes.Add("Class", "tblHead");
                    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                    tr_det_head.Cells.Add(tc_det_head_SNo);

                    TableCell tc_det_head_doc = new TableCell();
                    tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_doc.BorderWidth = 1;
                    Literal lit_det_head_doc = new Literal();
                    lit_det_head_doc.Text = "<b>Stockist Name</b>";
                    tc_det_head_doc.Attributes.Add("Class", "tblHead");
                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                    tr_det_head.Cells.Add(tc_det_head_doc);

                    TableCell tc_det_head_ww = new TableCell();
                    tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_ww.BorderWidth = 1;
                    Literal lit_det_head_ww = new Literal();
                    lit_det_head_ww.Text = "<b>Worked With</b>";
                    tc_det_head_ww.Attributes.Add("Class", "tblHead");
                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                    tr_det_head.Cells.Add(tc_det_head_ww);

                    TableCell tc_det_head_catg = new TableCell();
                    tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_catg.BorderWidth = 1;
                    Literal lit_det_head_catg = new Literal();
                    lit_det_head_catg.Text = "<b>POB</b>";
                    tc_det_head_catg.Attributes.Add("Class", "tblHead");
                    tc_det_head_catg.Controls.Add(lit_det_head_catg);
                    tr_det_head.Cells.Add(tc_det_head_catg);


                    tbldetailstk.Rows.Add(tr_det_head);

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
                        lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["Stockist_Name"].ToString();
                        tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                        tc_det_dr_name.BorderWidth = 1;
                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                        tr_det_sno.Cells.Add(tc_det_dr_name);


                        TableCell tc_det_work = new TableCell();
                        Literal lit_det_work = new Literal();
                        lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                        tc_det_work.BorderStyle = BorderStyle.Solid;
                        tc_det_work.BorderWidth = 1;
                        tc_det_work.Controls.Add(lit_det_work);
                        tr_det_sno.Cells.Add(tc_det_work);


                        TableCell tc_det_spec = new TableCell();
                        Literal lit_det_spec = new Literal();
                        lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                        tc_det_spec.BorderStyle = BorderStyle.Solid;
                        tc_det_spec.BorderWidth = 1;
                        tc_det_spec.Controls.Add(lit_det_spec);
                        tr_det_sno.Cells.Add(tc_det_spec);

                        tbldetailstk.Rows.Add(tr_det_sno);
                    }
                }

                //form1.Controls.Add(tbldetailhos);

                tc_det_head_main12.Controls.Add(tbldetailstk);
                tr_det_head_main11.Cells.Add(tc_det_head_main12);
                tbldetail_main11.Rows.Add(tr_det_head_main11);

                form1.Controls.Add(tbldetail_main11);


                if (iCount > 0)
                {
                    Table tbl_stk_empty = new Table();
                    TableRow tr_stk_empty = new TableRow();
                    TableCell tc_stk_empty = new TableCell();
                    Literal lit_stk_empty = new Literal();
                    lit_stk_empty.Text = "<BR>";
                    tc_stk_empty.Controls.Add(lit_stk_empty);
                    tr_stk_empty.Cells.Add(tc_stk_empty);
                    tbl_stk_empty.Rows.Add(tr_stk_empty);
                    form1.Controls.Add(tbl_stk_empty);
                }

                //5-Hospitals

                Table tbldetail_main9 = new Table();
                tbldetail_main9.BorderStyle = BorderStyle.None;
                tbldetail_main9.Width = 1100;
                TableRow tr_det_head_main9 = new TableRow();
                TableCell tc_det_head_main9 = new TableCell();
                tc_det_head_main9.Width = 100;
                Literal lit_det_main9 = new Literal();
                lit_det_main9.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_main9.Controls.Add(lit_det_main9);
                tr_det_head_main9.Cells.Add(tc_det_head_main9);

                //TableCell tc_det_head_main10 = new TableCell();
                //tc_det_head_main10.Width = 1000;


                //Table tbldetailhos = new Table();
                //tbldetailhos.BorderStyle = BorderStyle.Solid;
                //tbldetailhos.BorderWidth = 1;
                //tbldetailhos.GridLines = GridLines.Both;
                //tbldetailhos.Width = 1000;

                //dsdoc = dc.get_dcr_hos_details(sf_code,StrDate, 5); //5-Hospital
                //iCount = 0;
                //if (dsdoc.Tables[0].Rows.Count > 0)
                //{
                //    TableRow tr_det_head = new TableRow();
                //    TableCell tc_det_head_SNo = new TableCell();
                //    tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                //    tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                //    tc_det_head_SNo.BorderWidth = 1;
                //    Literal lit_det_head_SNo = new Literal();
                //    lit_det_head_SNo.Text = "<b>S.No</b>";
                //    tc_det_head_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#80CCFF");
                //    tc_det_head_SNo.Style.Add("color", "White");
                //    tc_det_head_SNo.Style.Add("font-weight", "bold");
                //    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                //    tr_det_head.Cells.Add(tc_det_head_SNo);

                //    TableCell tc_det_head_doc = new TableCell();
                //    tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                //    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                //    tc_det_head_doc.BorderWidth = 1;
                //    Literal lit_det_head_doc = new Literal();
                //    lit_det_head_doc.Text = "<b>Hospital Name</b>";
                //    tc_det_head_doc.BackColor = System.Drawing.ColorTranslator.FromHtml("#80CCFF");
                //    tc_det_head_doc.Style.Add("color", "White");
                //    tc_det_head_doc.Style.Add("font-weight", "bold");
                //    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                //    tr_det_head.Cells.Add(tc_det_head_doc);

                //    TableCell tc_det_head_ww = new TableCell();
                //    tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                //    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                //    tc_det_head_ww.BorderWidth = 1;
                //    Literal lit_det_head_ww = new Literal();
                //    lit_det_head_ww.Text = "<b>Worked With</b>";
                //    tc_det_head_ww.BackColor = System.Drawing.ColorTranslator.FromHtml("#80CCFF");
                //    tc_det_head_ww.Style.Add("color", "White");
                //    tc_det_head_ww.Style.Add("font-weight", "bold");
                //    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                //    tr_det_head.Cells.Add(tc_det_head_ww);

                //    TableCell tc_det_head_catg = new TableCell();
                //    tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                //    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                //    tc_det_head_catg.BorderWidth = 1;
                //    Literal lit_det_head_catg = new Literal();
                //    lit_det_head_catg.Text = "<b>POB</b>";
                //    tc_det_head_catg.BackColor = System.Drawing.ColorTranslator.FromHtml("#80CCFF");
                //    tc_det_head_catg.Style.Add("color", "White");
                //    tc_det_head_catg.Style.Add("font-weight", "bold");
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
                //        lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["Hospital_Name"].ToString();
                //        tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                //        tc_det_dr_name.BorderWidth = 1;
                //        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                //        tr_det_sno.Cells.Add(tc_det_dr_name);


                //        TableCell tc_det_work = new TableCell();
                //        Literal lit_det_work = new Literal();
                //        lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                //        tc_det_work.BorderStyle = BorderStyle.Solid;
                //        tc_det_work.BorderWidth = 1;
                //        tc_det_work.Controls.Add(lit_det_work);
                //        tr_det_sno.Cells.Add(tc_det_work);


                //        TableCell tc_det_spec = new TableCell();
                //        Literal lit_det_spec = new Literal();
                //        lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                //        tc_det_spec.BorderStyle = BorderStyle.Solid;
                //        tc_det_spec.BorderWidth = 1;
                //        tc_det_spec.Controls.Add(lit_det_spec);
                //        tr_det_sno.Cells.Add(tc_det_spec);

                //        tbldetailhos.Rows.Add(tr_det_sno);
                //    }
                //}

                ////form1.Controls.Add(tbldetailhos);

                //tc_det_head_main10.Controls.Add(tbldetailhos);
                //tr_det_head_main9.Cells.Add(tc_det_head_main10);
                //tbldetail_main9.Rows.Add(tr_det_head_main9);

                //form1.Controls.Add(tbldetail_main9);


                if (iCount > 0)
                {
                    Table tbl_hosp_empty = new Table();
                    TableRow tr_hosp_empty = new TableRow();
                    TableCell tc_hosp_empty = new TableCell();
                    Literal lit_hosp_empty = new Literal();
                    lit_hosp_empty.Text = "<BR>";
                    tc_hosp_empty.Controls.Add(lit_hosp_empty);
                    tr_hosp_empty.Cells.Add(tc_hosp_empty);
                    tbl_hosp_empty.Rows.Add(tr_hosp_empty);
                    form1.Controls.Add(tbl_hosp_empty);
                }

                Table tbl_line = new Table();
                tbl_line.BorderStyle = BorderStyle.None;
                tbl_line.Width = 1100;

                TableRow tr_line = new TableRow();

                TableCell tc_line0 = new TableCell();
                tc_line0.Width = 100;
                Literal lit_line0 = new Literal();
                lit_line0.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_line0.Controls.Add(lit_line0);
                tr_line.Cells.Add(tc_line0);

                TableCell tc_line = new TableCell();
                tc_line.Width = 1000;
                Literal lit_line = new Literal();
                lit_line.Text = "<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>";
                tc_line.Controls.Add(lit_line);
                tr_line.Cells.Add(tc_line);
                tbl_line.Rows.Add(tr_line);
                form1.Controls.Add(tbl_line);

            }
        //}
    //}
    
}