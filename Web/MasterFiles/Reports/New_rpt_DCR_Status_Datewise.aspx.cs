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

public partial class MasterFiles_Reports_New_rpt_DCR_Status_Datewise : System.Web.UI.Page
{
    DataSet dsSalesForce = new DataSet();
    DataSet Proc_dsSalesForce = new DataSet();
    DataSet dsDCR = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string fdate = string.Empty;
    string  todate = string.Empty;
    int tot_days = -1;
    int cday = 1;
    int ccsday = 0;
    int ddate = 0;
    string sDCR = string.Empty;
    string sDelay = string.Empty;
    DataSet dsDelay = new DataSet();
    string sf_type = string.Empty;
    DataSet dsSf = null;
    string sf_name = string.Empty;
    DateTime dtDCRfrom;
    DateTime fromdate;
    DateTime dtDCRto;
    string from_date = string.Empty;
    string to_date = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        sf_name = Request.QueryString["sf_name"].ToString();
        sf_type = Session["sf_type"].ToString();

        fdate = Request.QueryString["fdate"].ToString();
        todate = Request.QueryString["todate"].ToString();
        SalesForce sf = new SalesForce();
       
        dtDCRfrom = Convert.ToDateTime(fdate);
        fromdate = Convert.ToDateTime(fdate);
        dtDCRto = Convert.ToDateTime(todate);
        FillSalesForce(div_code, sf_code);

        lblHead.Text = lblHead.Text + fdate + " and " + todate + ": " + sf_name;
        ExportButton();
    }
    private void ExportButton()
    {
        btnPDF.Visible = false;
    }

    private void FillSalesForce(string div_code, string sf_code)
    {
        // Fetch the total rows for the table

        SalesForce sf = new SalesForce();
        DataTable dtSalesforce = new DataTable();
        if (sf_type == "2")
        {
            if (sf_code.Contains("MR"))
            {
                dsSalesForce = sf.getDCRStatus_MRStatus(sf_code);
            }
            else
            {
                dsSalesForce = sf.UserList_get_SelfMail(div_code, sf_code);
            }
        }
        else if (sf_type == "1")
        {

            dsSalesForce = sf.getSfName_HQ(sf_code);
        }
        else if (sf_type == "3")
        {
            dtSalesforce = sf.getUserListReportingTo(div_code, sf_code, 0);
            dsSalesForce.Tables.Add(dtSalesforce);
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
            ViewState["dsSalesForce"] = dsSalesForce;

        CreateDynamicTable();
        FillWorkType();
    }

    private void FillWorkType()
    {
        int j = 1;

        DCR dc = new DCR();

        dsDCR = dc.DCR_get_WorkType(div_code);

        TableCell tc_wt = new TableCell();
        Literal lit_wt = new Literal();
        TableRow tr_wt = new TableRow();
        TableRow tr_wt2 = new TableRow();

        foreach (DataRow drFF in dsDCR.Tables[0].Rows)
        {
            if (j <= 5)
            {

                lit_wt.Text = "<b>" + drFF["WType_SName"].ToString() + " - " + drFF["Worktype_Name_B"].ToString() + "</b>";

                tc_wt.Controls.Add(lit_wt);
                tr_wt.Cells.Add(tc_wt);
            }
            else
            {
                TableCell tc_wt2 = new TableCell();
                Literal lit_wt2 = new Literal();
                lit_wt2.Text = "<b>" + drFF["WType_SName"].ToString() + " - " + drFF["Worktype_Name_B"].ToString() + "</b>";
                tc_wt2.Controls.Add(lit_wt2);
                tr_wt2.Cells.Add(tc_wt2);
            }

            j = j + 1;
        }

        lit_wt.Text = "LP - Leave Approval Pending";
        tc_wt.Style.Add("font-weight", "bold");
        tc_wt.Controls.Add(lit_wt);
        tr_wt.Cells.Add(tc_wt);
        tblworktype.Rows.Add(tr_wt);
        tblworktype.Rows.Add(tr_wt2);

    }

    private void CreateDynamicTable()
    {

        if (ViewState["dsSalesForce"] != null)
        {

            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tr_header.ForeColor = System.Drawing.Color.White;
            tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.Controls.Add(lit_SNo);
            tr_header.Cells.Add(tc_SNo);
            //tr_header.BackColor = System.Drawing.Color.Bisque;

            TableCell tc_UserName = new TableCell();
            tc_UserName.BorderStyle = BorderStyle.Solid;
            tc_UserName.BorderWidth = 1;
            tc_UserName.Width = 110;
            Literal lit_UserName = new Literal();
            lit_UserName.Text = "<center>User Name</center>";
            tc_UserName.Controls.Add(lit_UserName);
            tr_header.Cells.Add(tc_UserName);
            //tr_header.BackColor = System.Drawing.Color.Bisque;

            TableCell tc_FF = new TableCell();
            tc_FF.BorderStyle = BorderStyle.Solid;
            tc_FF.BorderWidth = 1;
            tc_FF.Width = 600;
            Literal lit_FF = new Literal();
            lit_FF.Text = "<center>Field Force Name</center>";
            tc_FF.Controls.Add(lit_FF);
            tr_header.Cells.Add(tc_FF);

            tbl.Rows.Add(tr_header);

            //tot_days = getmaxdays_month(cmonth);
            TimeSpan t = dtDCRto - dtDCRfrom;
            tot_days = (int)t.TotalDays;
            

            TableRow tr_day_header = new TableRow();
            tr_day_header.BorderStyle = BorderStyle.Solid;
            tr_day_header.BorderWidth = 1;

            while (ccsday <= tot_days)
            {
                TableCell tc_day = new TableCell();
                tc_day.BorderStyle = BorderStyle.Solid;
                tc_day.BorderWidth = 1;
                tc_day.Width = 50;
                tc_day.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_day = new Literal();
                int ffday = fromdate.Day;
                lit_day.Text = ffday.ToString();
                tc_day.Controls.Add(lit_day);
                tr_header.Cells.Add(tc_day);
                fromdate = fromdate.AddDays(1);
                ccsday = ccsday + 1;
            }

            tbl.Rows.Add(tr_day_header);

            // Details Section
            DCR dc = new DCR();

            string sURL = string.Empty;
            int iCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {

                if (ViewState["HQ_Det"] != null)
                {
                    if (drFF["SF_Code"].ToString().Contains("MGR"))
                    {
                        TableRow tr_HQ_det = new TableRow();
                        TableCell tc_HQ_det = new TableCell();
                        Literal lit_HQ_det = new Literal();
                        lit_HQ_det.Text = drFF["sf_hq"].ToString();
                        tc_HQ_det.BorderStyle = BorderStyle.Solid;
                        tc_HQ_det.BorderWidth = 1;
                        tc_HQ_det.Controls.Add(lit_HQ_det);
                        tc_HQ_det.ColumnSpan = 33;
                        //tc_HQ_det.BackColor = System.Drawing.Color.LightYellow;
                        tr_HQ_det.Cells.Add(tc_HQ_det);
                        tbl.Rows.Add(tr_HQ_det);
                        ViewState["HQ_Det"] = drFF["sf_hq"].ToString();
                    }
                }
                else
                {
                    TableRow tr_HQ_det = new TableRow();
                    TableCell tc_HQ_det = new TableCell();
                    Literal lit_HQ_det = new Literal();
                    lit_HQ_det.Text = drFF["sf_hq"].ToString();
                    tc_HQ_det.BorderStyle = BorderStyle.Solid;
                    tc_HQ_det.BorderWidth = 1;
                    tc_HQ_det.Controls.Add(lit_HQ_det);
                    tc_HQ_det.ColumnSpan = 33;
                    //tc_HQ_det.BackColor = System.Drawing.Color.LightYellow;
                    tr_HQ_det.Cells.Add(tc_HQ_det);
                    tbl.Rows.Add(tr_HQ_det);
                    ViewState["HQ_Det"] = drFF["sf_hq"].ToString();
                }

                ViewState["HQ_Det"] = drFF["sf_hq"].ToString();

                TableRow tr_det = new TableRow();
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;

                tc_det_SNo.Controls.Add(lit_det_SNo);
                //tc_det_SNo.Height = 10;
                tr_det.Cells.Add(tc_det_SNo);
                //tr_det.Height = 10;

                TableCell tc_det_User_Name = new TableCell();
                Literal lit_det_User_name = new Literal();
                lit_det_User_name.Text = "&nbsp;" + drFF["UsrDfd_UserName"].ToString();
                tc_det_User_Name.BorderStyle = BorderStyle.Solid;
                tc_det_User_Name.BorderWidth = 1;

                tc_det_User_Name.Controls.Add(lit_det_User_name);
                //tc_det_SNo.Height = 10;
                tr_det.Cells.Add(tc_det_User_Name);
                //tr_det.Height = 10;

                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();

                SalesForce sf1 = new SalesForce();
                dsSf = sf1.CheckSFNameVacant(drFF["sf_code"].ToString(), dtDCRfrom.Month, dtDCRfrom.Year);
                if (dsSf.Tables[0].Rows.Count > 0)
                {
                    int i = dsSf.Tables[0].Rows.Count - 1;
                    string sf_name = dsSf.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    lit_det_FF.Text = "&nbsp;" + sf_name + " - " + drFF["sf_hq"].ToString() + " - " + drFF["Designation_Short_Name"].ToString();
                }
                else
                {
                    lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString() + " - " + drFF["sf_hq"].ToString() + " - " + drFF["Designation_Short_Name"].ToString();
                }

                //if (sf_type == "2")
                //{
                //    lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString() + " - " + drFF["sf_hq"].ToString()  + " - " + drFF["sf_Designation_Short_Name"].ToString();
                //}
                //else
                //{
                //lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString() + " - " + drFF["sf_hq"].ToString() + " - " + drFF["Designation_Short_Name"].ToString();
                // }
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.HorizontalAlign = HorizontalAlign.Left;
                tc_det_FF.BorderWidth = 1;

                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                //DataTable dtproc = null;
                //DataSet dssf_proc = new DataSet();
                ////Filter the Dataset based on the FieldForce
                //Proc_dsSalesForce.Tables[0].DefaultView.RowFilter = "sf_Code = '" + drFF["sf_code"].ToString() + "' ";
                //DataView dvproc = Proc_dsSalesForce.Tables[0].DefaultView;
                //dtproc = dvproc.ToTable();
                //dssf_proc.Tables.Add(dtproc);

                SalesForce sf = new SalesForce();
                dsSf = sf.CheckSFType(drFF["sf_code"].ToString());
                if (dsSf.Tables[0].Rows.Count > 0)
                {
                    sf_type = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                DateTime dtfrom = dtDCRfrom;
                dsDCR = dc.getDCR_Report_Det_New_per_withoutdoccnt(drFF["sf_code"].ToString(), div_code, dtDCRfrom.ToString("MM/dd/yyyy"), dtDCRto.ToString("MM/dd/yyyy"), sf_type);
                if (dsDCR.Tables[0].Rows.Count > 0)
                {
                    if (dsDCR.Tables[0].Rows.Count > 1)
                    {
                        ddate = 1;
                        int chkdate = dtfrom.Day;
                        foreach (DataRow datarow in dsDCR.Tables[0].Rows)
                        {
                            //while (chkdate <= Convert.ToInt16(datarow["Activity_Date"].ToString()))
                            //{
                            do
                            {
                                if (chkdate == Convert.ToInt16(datarow["Activity_Date"].ToString()))
                                {
                                    sDCR = datarow["WType_SName"].ToString();
                                }
                                else
                                {
                                    sDCR = " - ";
                                }
                                TableCell tc_det_day = new TableCell();
                                tc_det_day.BorderStyle = BorderStyle.Solid;
                                tc_det_day.BorderWidth = 1;
                                tc_det_day.Width = 50;
                                tc_det_day.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_day = new Literal();
                                lit_det_day.Text = sDCR;
                                tc_det_day.Controls.Add(lit_det_day);
                                tr_det.Cells.Add(tc_det_day);

                                dsDelay = dc.get_DCR_Status_Delay(drFF["sf_code"].ToString(), datarow["Activity_Date"].ToString(), dtDCRfrom.Month, dtDCRfrom.Year);
                                if (dsDelay.Tables[0].Rows.Count > 0)
                                {
                                    tc_det_day.BackColor = System.Drawing.ColorTranslator.FromHtml("#eee8aa");
                                }
                                ddate = ddate + 1;
                                dtfrom = dtfrom.AddDays(1);
                                chkdate = dtfrom.Day;
                            } while (chkdate == Convert.ToInt16(datarow["Activity_Date"].ToString()));
                        }
                        sDCR = " - ";
                        if ((tot_days + 1) >= ddate)
                        {
                            cday = ddate;
                            while (cday <= (tot_days + 1))
                            {
                                TableCell tc_det_day = new TableCell();
                                tc_det_day.BorderStyle = BorderStyle.Solid;
                                tc_det_day.BorderWidth = 1;
                                tc_det_day.Width = 50;
                                tc_det_day.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_day = new Literal();
                                lit_det_day.Text = sDCR;
                                tc_det_day.Controls.Add(lit_det_day);
                                tr_det.Cells.Add(tc_det_day);

                                cday = cday + 1;
                            }
                        }
                    }
                    else
                    {
                        sDCR = " - ";
                        ddate = 0;
                        if (tot_days  >= ddate)
                        {
                            cday = ddate;
                            while (cday <= tot_days)
                            {
                                TableCell tc_det_day = new TableCell();
                                tc_det_day.BorderStyle = BorderStyle.Solid;
                                tc_det_day.BorderWidth = 1;
                                tc_det_day.Width = 50;
                                tc_det_day.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_day = new Literal();
                                lit_det_day.Text = sDCR;
                                tc_det_day.Controls.Add(lit_det_day);
                                tr_det.Cells.Add(tc_det_day);

                                cday = cday + 1;
                            }
                        }
                    }
                }
                else
                {
                    sDCR = " - ";
                    ddate = 0;
                    if (tot_days >= ddate)
                    {
                        cday = ddate;
                        while (cday <= tot_days)
                        {
                            TableCell tc_det_day = new TableCell();
                            tc_det_day.BorderStyle = BorderStyle.Solid;
                            tc_det_day.BorderWidth = 1;
                            tc_det_day.Width = 50;
                            tc_det_day.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_day = new Literal();
                            lit_det_day.Text = sDCR;
                            tc_det_day.Controls.Add(lit_det_day);
                            tr_det.Cells.Add(tc_det_day);

                            cday = cday + 1;
                        }
                    }
                }

                tbl.Rows.Add(tr_det);
            }

            ViewState["dynamictable"] = true;
        }
    }


    private int getmaxdays_month(int imonth)
    {
        int idays = -1;

        if (imonth == 1)
            idays = 31;
        else if (imonth == 2)
            idays = 29;
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
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=DCRStatus.xls";
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
        string strFileName = "rptDCRStatus";
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

}