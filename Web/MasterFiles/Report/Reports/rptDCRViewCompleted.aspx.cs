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


public partial class Reports_rptDCRViewCompleted : System.Web.UI.Page
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
    int imonth = -1;
    int iyear = -1;
    int tot_days = -1;
    int cday = 1;
    int iCount = -1;
    string strDateMatch = string.Empty;
    string strMonth = string.Empty;
    string strType = string.Empty;
    string sDCR = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sf_code = Request.QueryString["sf_code"].ToString();
            imonth = Convert.ToInt16(Request.QueryString["Month"].ToString());
            iyear = Convert.ToInt16(Request.QueryString["Year"].ToString());
            Day = Request.QueryString["Day"].ToString();
            strType = Request.QueryString["Type"].ToString();

            if (Day.Length != 2)
            {
                strDateMatch = "0" + Day;
            }
            else
            {
                strDateMatch = Day;
            }

            if (imonth.ToString().Length != 2)
            {
                strMonth = "0" + imonth;
            }
            else
            {
                strMonth = Convert.ToString(imonth);
            }

            StrDate = strDateMatch + "/" + strMonth + "/" + iyear;
            //lblHead.Text = "Daily Call Report for" +" " + StrDate;
            dynamicCreateTable();
        }
        catch (Exception ex)
        {

        }

    }

    private void dynamicCreateTable()
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
        TableRow tr_day = new TableRow();
        TableCell tc_day = new TableCell();
        tc_day.BorderStyle = BorderStyle.None;
        tc_day.ColumnSpan = 2;

        tc_day.HorizontalAlign = HorizontalAlign.Center;
        Literal lit_day = new Literal();
        // lit_day.Text = "<u>Daily Work Report on " + StrDate + "</u>";
        tc_day.Controls.Add(lit_day);
        tr_day.Cells.Add(tc_day);
        tbl.Rows.Add(tr_day);

        TableRow tr_ff = new TableRow();
        //TableCell tc_ff_name = new TableCell();
        //tc_ff_name.BorderStyle = BorderStyle.None;
        //tc_ff_name.Width = 200;
        //Literal lit_ff_name = new Literal();
        //lit_ff_name.Text = "<b>Field Force Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_Name.ToString();
        //tc_ff_name.Controls.Add(lit_ff_name);
        //tr_ff.Cells.Add(tc_ff_name);

        //TableCell tc_HQ = new TableCell();
        //tc_HQ.BorderStyle = BorderStyle.None;
        //tc_HQ.Width = 200;
        //Literal lit_HQ = new Literal();
        //lit_HQ.Text = "<b>Head Quarters</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_HQ.ToString();
        //tc_HQ.Controls.Add(lit_HQ);
        //tr_ff.Cells.Add(tc_HQ);

        tbl.Rows.Add(tr_ff);

        TableRow tr_dcr = new TableRow();
        TableCell tc_dcr_submit = new TableCell();
        tc_dcr_submit.BorderStyle = BorderStyle.None;
        tc_dcr_submit.Width = 200;
        Literal lit_dcr_submit = new Literal();
        // lit_dcr_submit.Text = "<b>DCR Submitted on</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoc["Submission_Date"].ToString();
        tc_dcr_submit.Controls.Add(lit_dcr_submit);
        tr_dcr.Cells.Add(tc_dcr_submit);

        TableCell tc_Terr = new TableCell();
        tc_Terr.BorderStyle = BorderStyle.None;
        tc_Terr.Width = 200;
        Literal lit_Terr = new Literal();
        //lit_Terr.Text = "<b>Territory Worked</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoc["Plan_Name"].ToString(); ;
        tc_Terr.Controls.Add(lit_Terr);
        tr_dcr.Cells.Add(tc_Terr);

        tbl.Rows.Add(tr_dcr);
        if (strType == "1")
        {
            //1-Listed Doctor
            lblHead.Text = "Retailer Details";
            TableRow tr_det = new TableRow();
            TableCell tc_det = new TableCell();
            tc_det.BorderStyle = BorderStyle.None;
            tc_det.Width = 400;
            tc_det.ColumnSpan = 2;

            Table tbldetail = new Table();
            tbldetail.BorderStyle = BorderStyle.Solid;
            tbldetail.BorderWidth = 1;
            tbldetail.Style.Add("border-collapse", "collapse");
            tbldetail.Style.Add("border", "solid 1px Black");
            tbldetail.GridLines = GridLines.Both;
            tbldetail.Width = 1000;
            dsdoc = dc.get_Temp_and_Approved_dcrLstDOC_details(sf_code, StrDate, 1); //1-Listed Doctor
            if (dsdoc.Tables[0].Rows.Count > 0)
            {
                TableRow tr_det_head = new TableRow();
                TableCell tc_det_head_SNo = new TableCell();
                tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_head_SNo.BorderWidth = 1;
                Literal lit_det_head_SNo = new Literal();
                lit_det_head_SNo.Text = "<b>S.No</b>";
                tc_det_head_SNo.Attributes.Add("Class", "tblHead");
                tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                tr_det_head.Cells.Add(tc_det_head_SNo);

                TableCell tc_det_head_Ses = new TableCell();
                tc_det_head_Ses.BorderStyle = BorderStyle.Solid;
                tc_det_head_Ses.BorderWidth = 1;
                tc_det_head_Ses.Visible = false;
                Literal lit_det_head_Ses = new Literal();
                lit_det_head_Ses.Text = "<b>SVL No</b>";
                tc_det_head_Ses.Attributes.Add("Class", "tblHead");
                tc_det_head_Ses.Controls.Add(lit_det_head_Ses);
                tr_det_head.Cells.Add(tc_det_head_Ses);

                TableCell tc_det_head_doc = new TableCell();
                tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                tc_det_head_doc.BorderWidth = 1;
                Literal lit_det_head_doc = new Literal();
                lit_det_head_doc.Text = "<b>Retailer Name</b>";
                tc_det_head_doc.Attributes.Add("Class", "tblHead");
                tc_det_head_doc.Controls.Add(lit_det_head_doc);
                tr_det_head.Cells.Add(tc_det_head_doc);

                TableCell tc_det_head_spec = new TableCell();
                tc_det_head_spec.BorderStyle = BorderStyle.Solid;
                tc_det_head_spec.BorderWidth = 1;
                Literal lit_det_head_spec = new Literal();
                lit_det_head_spec.Text = "<b>Channel</b>";
                tc_det_head_spec.Attributes.Add("Class", "tblHead");
                tc_det_head_spec.Controls.Add(lit_det_head_spec);
                tr_det_head.Cells.Add(tc_det_head_spec);

                TableCell tc_det_head_catg = new TableCell();
                tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                tc_det_head_catg.BorderWidth = 1;
                Literal lit_det_head_catg = new Literal();
                lit_det_head_catg.Text = "<b>Submission Date</b>";
                tc_det_head_catg.Attributes.Add("Class", "tblHead");
                tc_det_head_catg.Controls.Add(lit_det_head_catg);
                tr_det_head.Cells.Add(tc_det_head_catg);

                TableCell tc_det_head_Territory = new TableCell();
                tc_det_head_Territory.BorderStyle = BorderStyle.Solid;
                tc_det_head_Territory.BorderWidth = 1;
                Literal lit_det_head_Territory = new Literal();
                lit_det_head_Territory.Text = "<b>Route</b>";
                tc_det_head_Territory.Attributes.Add("Class", "tblHead");
                tc_det_head_Territory.Controls.Add(lit_det_head_Territory);
                tr_det_head.Cells.Add(tc_det_head_Territory);

                tbldetail.Rows.Add(tr_det_head);

                iCount = 0;
                foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                {
                    TableRow tr_det_sno = new TableRow();
                    TableCell tc_det_SNo = new TableCell();
                    iCount += 1;
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                    tc_det_SNo.Attributes.Add("Class", "tblRow");
                    tc_det_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_SNo.BorderWidth = 1;
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    tr_det_sno.Cells.Add(tc_det_SNo);

                    TableCell tc_det_Ses = new TableCell();
                    Literal lit_det_Ses = new Literal();
                    tc_det_Ses.Visible = false;
                    lit_det_Ses.Text = drdoctor["Trans_SlNo"].ToString();
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.Attributes.Add("Class", "tblRow");
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);
                    tr_det_sno.Cells.Add(tc_det_Ses);

                    TableCell tc_det_dr_name = new TableCell();
                    Literal lit_det_dr_name = new Literal();
                    lit_det_dr_name.Text = drdoctor["ListedDr_Name"].ToString();
                    tc_det_dr_name.Attributes.Add("Class", "tblRow");
                    tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_name.BorderWidth = 1;
                    tc_det_dr_name.Controls.Add(lit_det_dr_name);
                    tr_det_sno.Cells.Add(tc_det_dr_name);

                    TableCell tc_det_spec = new TableCell();
                    Literal lit_det_spec = new Literal();
                    lit_det_spec.Text = drdoctor["Doc_Special_Name"].ToString();
                    tc_det_spec.Attributes.Add("Class", "tblRow");
                    tc_det_spec.BorderStyle = BorderStyle.Solid;
                    tc_det_spec.BorderWidth = 1;
                    tc_det_spec.Controls.Add(lit_det_spec);
                    tr_det_sno.Cells.Add(tc_det_spec);

                    TableCell tc_det_catg = new TableCell();
                    Literal lit_det_catg = new Literal();
                    lit_det_catg.Text = drdoctor["Submission_Date"].ToString();
                    tc_det_catg.Attributes.Add("Class", "tblRow");
                    tc_det_catg.BorderStyle = BorderStyle.Solid;
                    tc_det_catg.BorderWidth = 1;
                    tc_det_catg.Controls.Add(lit_det_catg);
                    tr_det_sno.Cells.Add(tc_det_catg);

                    TableCell tc_det_Territory = new TableCell();
                    Literal lit_det_Territory = new Literal();
                    lit_det_Territory.Text = drdoctor["SDP_Name"].ToString();
                    tc_det_Territory.Attributes.Add("Class", "tblRow");
                    tc_det_Territory.BorderStyle = BorderStyle.Solid;
                    tc_det_Territory.BorderWidth = 1;
                    tc_det_Territory.Controls.Add(lit_det_Territory);
                    tr_det_sno.Cells.Add(tc_det_Territory);

                    tbldetail.Rows.Add(tr_det_sno);
                }

            }

            tc_det.HorizontalAlign = HorizontalAlign.Center;
            tc_day.Controls.Add(tbldetail);
            tr_day.Cells.Add(tc_det);
            tbl.Rows.Add(tr_day);
        }
        //2-Chemists
        else if (strType == "2")
        {
            lblHead.Text = "Chemist Details";
            TableRow tr_chedet = new TableRow();
            TableCell tc_chedet = new TableCell();
            //tc_chedet.BorderStyle = BorderStyle.Solid;
            //tc_chedet.BorderWidth = 1;
            tc_chedet.Width = 400;
            tc_chedet.ColumnSpan = 2;

            Table tbldetailChe = new Table();
            tbldetailChe.BorderStyle = BorderStyle.Solid;
            tbldetailChe.BorderWidth = 1;
            tbldetailChe.Style.Add("border-collapse", "collapse");
            tbldetailChe.Style.Add("border", "solid 1px Black");
            tbldetailChe.GridLines = GridLines.Both;
            tbldetailChe.Width = 1000;

            dsdoc = dc.get_Temp_and_Approved_dcr_che_details(sf_code, StrDate, 2); //2-Chemists
            if (dsdoc.Tables[0].Rows.Count > 0)
            {
                TableRow tr_det_head = new TableRow();
                TableCell tc_det_head_SNo = new TableCell();
                tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_head_SNo.BorderWidth = 1;
                Literal lit_det_head_SNo = new Literal();
                lit_det_head_SNo.Text = "<b>S.No</b>";
                tc_det_head_SNo.Attributes.Add("Class", "tblHead");
                tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                tr_det_head.Cells.Add(tc_det_head_SNo);

                TableCell tc_det_head_Chem = new TableCell();
                tc_det_head_Chem.BorderStyle = BorderStyle.Solid;
                tc_det_head_Chem.BorderWidth = 1;
                Literal lit_det_head_Chem = new Literal();
                lit_det_head_Chem.Text = "<b>Chemists Name</b>";
                tc_det_head_Chem.Attributes.Add("Class", "tblHead");
                tc_det_head_Chem.Controls.Add(lit_det_head_Chem);
                tr_det_head.Cells.Add(tc_det_head_Chem);

                TableCell tc_det_head_catg = new TableCell();
                tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                tc_det_head_catg.BorderWidth = 1;
                Literal lit_det_head_catg = new Literal();
                lit_det_head_catg.Text = "<b>Territory</b>";
                tc_det_head_catg.Attributes.Add("Class", "tblHead");
                tc_det_head_catg.Controls.Add(lit_det_head_catg);
                tr_det_head.Cells.Add(tc_det_head_catg);

                TableCell tc_det_head_POB = new TableCell();
                tc_det_head_POB.BorderStyle = BorderStyle.Solid;
                tc_det_head_POB.BorderWidth = 1;
                Literal lit_det_head_POB = new Literal();
                lit_det_head_POB.Text = "<b>POB</b>";
                tc_det_head_POB.Attributes.Add("Class", "tblHead");
                tc_det_head_POB.Controls.Add(lit_det_head_POB);
                tr_det_head.Cells.Add(tc_det_head_POB);


                tbldetailChe.Rows.Add(tr_det_head);

                iCount = 0;
                foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                {
                    TableRow tr_det_sno = new TableRow();
                    TableCell tc_det_SNo = new TableCell();
                    iCount += 1;
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                    tc_det_SNo.Attributes.Add("Class", "tblRow");
                    tc_det_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_SNo.BorderWidth = 1;
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    tr_det_sno.Cells.Add(tc_det_SNo);

                    TableCell tc_det_dr_name = new TableCell();
                    Literal lit_det_dr_name = new Literal();
                    lit_det_dr_name.Text = drdoctor["Chemists_Name"].ToString();
                    tc_det_dr_name.Attributes.Add("Class", "tblRow");
                    tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_name.BorderWidth = 1;
                    tc_det_dr_name.Controls.Add(lit_det_dr_name);
                    tr_det_sno.Cells.Add(tc_det_dr_name);

                    TableCell tc_det_spec = new TableCell();
                    Literal lit_det_spec = new Literal();
                    lit_det_spec.Text = drdoctor["SDP_Name"].ToString();
                    tc_det_spec.Attributes.Add("Class", "tblRow");
                    tc_det_spec.BorderStyle = BorderStyle.Solid;
                    tc_det_spec.BorderWidth = 1;
                    tc_det_spec.Controls.Add(lit_det_spec);
                    tr_det_sno.Cells.Add(tc_det_spec);

                    TableCell tc_det_POB = new TableCell();
                    Literal lit_det_POB = new Literal();
                    lit_det_POB.Text = drdoctor["POB"].ToString();
                    tc_det_POB.Attributes.Add("Class", "tblRow");
                    tc_det_POB.BorderStyle = BorderStyle.Solid;
                    tc_det_POB.BorderWidth = 1;
                    tc_det_POB.Controls.Add(lit_det_POB);
                    tr_det_sno.Cells.Add(tc_det_POB);

                    tbldetailChe.Rows.Add(tr_det_sno);
                }

            }

            tc_chedet.HorizontalAlign = HorizontalAlign.Center;
            tc_day.Controls.Add(tbldetailChe);
            tr_day.Cells.Add(tc_chedet);
            tbl.Rows.Add(tr_day);
        }
        //3-Stockist
        else if (strType == "3")
        {
            lblHead.Text = "Stockiest Details";
            TableRow tr_Stockdet = new TableRow();
            TableCell tc_Stockdet = new TableCell();
            tc_Stockdet.BorderStyle = BorderStyle.None;
            tc_Stockdet.Width = 400;
            tc_Stockdet.ColumnSpan = 2;

            Table tbldetailStock = new Table();
            tbldetailStock.BorderStyle = BorderStyle.Solid;
            tbldetailStock.BorderWidth = 1;
            tbldetailStock.Style.Add("border-collapse", "collapse");
            tbldetailStock.Style.Add("border", "solid 1px Black");
            tbldetailStock.GridLines = GridLines.Both;
            tbldetailStock.Width = 1000;

            dsdoc = dc.get_Temp_and_Approved_dcr_stk_detailsView(sf_code, StrDate, 3); //3-Stockist
            if (dsdoc.Tables[0].Rows.Count > 0)
            {
                TableRow tr_det_head_SNo = new TableRow();
                TableCell tc_det_head_SNo = new TableCell();
                tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_head_SNo.BorderWidth = 1;
                Literal lit_det_head_SNo = new Literal();
                lit_det_head_SNo.Text = "<b>S.No</b>";
                tc_det_head_SNo.Attributes.Add("Class", "tblHead");
                tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                tr_Stockdet.Cells.Add(tc_det_head_SNo);

                TableCell tc_det_head_doc = new TableCell();
                tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                tc_det_head_doc.BorderWidth = 1;
                Literal lit_det_head_doc = new Literal();
                lit_det_head_doc.Text = "<b>Stockiest Name</b>";
                tc_det_head_doc.Attributes.Add("Class", "tblHead");
                tc_det_head_doc.Controls.Add(lit_det_head_doc);
                tr_Stockdet.Cells.Add(tc_det_head_doc);

                TableCell tc_det_head_catg = new TableCell();
                tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                tc_det_head_catg.BorderWidth = 1;
                Literal lit_det_head_catg = new Literal();
                lit_det_head_catg.Text = "<b>Route</b>";
                tc_det_head_catg.Attributes.Add("Class", "tblHead");
                tc_det_head_catg.Controls.Add(lit_det_head_catg);
                tr_Stockdet.Cells.Add(tc_det_head_catg);


                tbldetailStock.Rows.Add(tr_Stockdet);

                iCount = 0;
                foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                {
                    TableRow tr_det_sno = new TableRow();
                    TableCell tc_det_SNo = new TableCell();
                    iCount += 1;
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                    tc_det_SNo.Attributes.Add("Class", "tblRow");
                    tc_det_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_SNo.BorderWidth = 1;
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    tr_det_sno.Cells.Add(tc_det_SNo);

                    TableCell tc_det_dr_name = new TableCell();
                    Literal lit_det_dr_name = new Literal();
                    lit_det_dr_name.Text = drdoctor["Stockist_Name"].ToString();
                    tc_det_dr_name.Attributes.Add("Class", "tblRow");
                    tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_name.BorderWidth = 1;
                    tc_det_dr_name.Controls.Add(lit_det_dr_name);
                    tr_det_sno.Cells.Add(tc_det_dr_name);

                    TableCell tc_det_spec = new TableCell();
                    Literal lit_det_spec = new Literal();
                    lit_det_spec.Text = drdoctor["SDP_Name"].ToString();
                    tc_det_spec.Attributes.Add("Class", "tblRow");
                    tc_det_spec.BorderStyle = BorderStyle.Solid;
                    tc_det_spec.BorderWidth = 1;
                    tc_det_spec.Controls.Add(lit_det_spec);
                    tr_det_sno.Cells.Add(tc_det_spec);

                    tbldetailStock.Rows.Add(tr_det_sno);
                }
            }

            tc_Stockdet.HorizontalAlign = HorizontalAlign.Center;
            tc_day.Controls.Add(tbldetailStock);
            tr_day.Cells.Add(tc_Stockdet);
            tbl.Rows.Add(tr_day);
        }
        //4-UnlstDoc
        else if (strType == "4")
        {
            lblHead.Text = "UnDoctor Details";
            TableRow tr_UnLstDocdet = new TableRow();
            TableCell tc_UnLstDocdet = new TableCell();
            tc_UnLstDocdet.BorderStyle = BorderStyle.None;            
            tc_UnLstDocdet.Width = 100;
            tc_UnLstDocdet.ColumnSpan = 2;

            Table tbldetailUnLstDoc = new Table();
            tbldetailUnLstDoc.BorderStyle = BorderStyle.Solid;
            tbldetailUnLstDoc.BorderWidth = 1;
            tbldetailUnLstDoc.Style.Add("border-collapse", "collapse");
            tbldetailUnLstDoc.Style.Add("border", "solid 1px Black");
            tbldetailUnLstDoc.GridLines = GridLines.Both;
            tbldetailUnLstDoc.Width = 1000;

            dsdoc = dc.get_Temp_and_Approved_unlst_doc_details(sf_code, StrDate, 4); //4-UnlistDoc
            if (dsdoc.Tables[0].Rows.Count > 0)
            {
                TableRow tr_det_head_SNo = new TableRow();
                TableCell tc_det_head_SNo = new TableCell();
                tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_head_SNo.BorderWidth = 1;
                Literal lit_det_head_SNo = new Literal();
                lit_det_head_SNo.Text = "<b>S.No</b>";
                tc_det_head_SNo.Attributes.Add("Class", "tblHead");
                tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                tr_UnLstDocdet.Cells.Add(tc_det_head_SNo);

                TableCell tc_det_head_doc = new TableCell();
                tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                tc_det_head_doc.BorderWidth = 1;
                Literal lit_det_head_doc = new Literal();
                lit_det_head_doc.Text = "<b>UnListed  Doctor Name</b>";
                tc_det_head_doc.Attributes.Add("Class", "tblHead");
                tc_det_head_doc.Controls.Add(lit_det_head_doc);
                tr_UnLstDocdet.Cells.Add(tc_det_head_doc);

                TableCell tc_det_head_Spec = new TableCell();
                tc_det_head_Spec.BorderStyle = BorderStyle.Solid;
                tc_det_head_Spec.BorderWidth = 1;
                Literal lit_det_head_Spec = new Literal();
                lit_det_head_Spec.Text = "<b>Speciality</b>";
                tc_det_head_Spec.Attributes.Add("Class", "tblHead");
                tc_det_head_Spec.Controls.Add(lit_det_head_Spec);
                tr_UnLstDocdet.Cells.Add(tc_det_head_Spec);

                TableCell tc_det_head_catg = new TableCell();
                tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                tc_det_head_catg.BorderWidth = 1;
                Literal lit_det_head_catg = new Literal();
                lit_det_head_catg.Text = "<b>Category</b>";
                tc_det_head_catg.Attributes.Add("Class", "tblHead");
                tc_det_head_catg.Controls.Add(lit_det_head_catg);
                tr_UnLstDocdet.Cells.Add(tc_det_head_catg);

                TableCell tc_det_head_Terr = new TableCell();
                tc_det_head_Terr.BorderStyle = BorderStyle.Solid;
                tc_det_head_Terr.BorderWidth = 1;
                Literal lit_det_head_Terr = new Literal();
                lit_det_head_Terr.Text = "<b>Territory</b>";
                tc_det_head_Terr.Attributes.Add("Class", "tblHead");
                tc_det_head_Terr.Controls.Add(lit_det_head_Terr);
                tr_UnLstDocdet.Cells.Add(tc_det_head_Terr);

                tbldetailUnLstDoc.Rows.Add(tr_UnLstDocdet);
                iCount = 0;
                foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                {
                    TableRow tr_det_sno = new TableRow();
                    TableCell tc_det_SNo = new TableCell();
                    iCount += 1;
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                    tc_det_SNo.Attributes.Add("Class", "tblRow");
                    tc_det_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_SNo.BorderWidth = 1;
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    tr_det_sno.Cells.Add(tc_det_SNo);

                    TableCell tc_det_dr_name = new TableCell();
                    Literal lit_det_dr_name = new Literal();
                    lit_det_dr_name.Text = drdoctor["UnListedDr_Name"].ToString();
                    tc_det_dr_name.Attributes.Add("Class", "tblRow");
                    tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_name.BorderWidth = 1;
                    tc_det_dr_name.Controls.Add(lit_det_dr_name);
                    tr_det_sno.Cells.Add(tc_det_dr_name);

                    TableCell tc_det_dr_Spec = new TableCell();
                    Literal lit_det_dr_Spec = new Literal();
                    lit_det_dr_Spec.Text = drdoctor["Doc_Special_Name"].ToString();
                    tc_det_dr_Spec.Attributes.Add("Class", "tblRow");
                    tc_det_dr_Spec.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_Spec.BorderWidth = 1;
                    tc_det_dr_Spec.Controls.Add(lit_det_dr_Spec);
                    tr_det_sno.Cells.Add(tc_det_dr_Spec);

                    TableCell tc_det_dr_Category = new TableCell();
                    Literal lit_det_dr_Category = new Literal();
                    lit_det_dr_Category.Text = drdoctor["Doc_Cat_Name"].ToString();
                    tc_det_dr_Category.Attributes.Add("Class", "tblRow");
                    tc_det_dr_Category.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_Category.BorderWidth = 1;
                    tc_det_dr_Category.Controls.Add(lit_det_dr_Category);
                    tr_det_sno.Cells.Add(tc_det_dr_Category);

                    TableCell tc_det_Plan = new TableCell();
                    Literal lit_det_Plan = new Literal();
                    lit_det_Plan.Text = drdoctor["SDP_Name"].ToString();
                    tc_det_Plan.Attributes.Add("Class", "tblRow");
                    tc_det_Plan.BorderStyle = BorderStyle.Solid;
                    tc_det_Plan.BorderWidth = 1;
                    tc_det_Plan.Controls.Add(lit_det_Plan);
                    tr_det_sno.Cells.Add(tc_det_Plan);                    

                    tbldetailUnLstDoc.Rows.Add(tr_det_sno);
                }
            }

            tc_UnLstDocdet.HorizontalAlign = HorizontalAlign.Center;
            tc_day.Controls.Add(tbldetailUnLstDoc);
            tr_day.Cells.Add(tc_UnLstDocdet);
            tbl.Rows.Add(tr_day);
        }
    }
}