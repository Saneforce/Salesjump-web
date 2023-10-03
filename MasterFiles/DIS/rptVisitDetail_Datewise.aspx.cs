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

public partial class MIS_Reports_rptVisitDetail_Datewise : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DataSet dsLeave = null;
    string Monthsub = string.Empty;
    string tot_dr = string.Empty;
    string Days = string.Empty;
    string strSf_Code = string.Empty;
    string sCurrentDate = string.Empty;
    DateTime dtCurrent;
    int tot_days = -1;
    int cday = 1;
    string sDCR = string.Empty;
    int ddate = 0;
    DataSet dsDCR = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        lblRegionName.Text = sfname;

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Visit Detail - DateWise for the Month of " + strFMonthName + " " + FYear;

        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;

        FillDoctor();
    }

    private void FillDoctor()
    {

        DateTime dtCurrent;
        string sCurrentDate = string.Empty;

        if (Convert.ToInt16(FMonth) == 12)
        {
            sCurrentDate = "01-01-" + (Convert.ToInt16(FYear) + 1);
        }
        else
        {
            sCurrentDate = (Convert.ToInt16(FMonth) + 1) + "-01-" + Convert.ToInt16(FYear);
        }

        dtCurrent = Convert.ToDateTime(sCurrentDate);
        string sURL = string.Empty;
        tbl.Rows.Clear();

        Doctor dc = new Doctor();
        dsDoctor = dc.Visit_Doctor_DCR(divcode, sfCode, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent);

        if (dsDoctor.Tables[0].Rows.Count > 0)
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
            tc_SNo.RowSpan = 1;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "<center>S.No</center>";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Style.Add("font-family", "Calibri");
            tc_SNo.Style.Add("font-size", "10pt");
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 1;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>Listed Doctor Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Style.Add("font-family", "Calibri");
            tc_DR_Code.Style.Add("font-size", "10pt");
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 500;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Listed Doctor Name</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Style.Add("font-family", "Calibri");
            tc_DR_Name.Style.Add("font-size", "10pt");
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(divcode);

            TableCell tc_DR_Terr = new TableCell();
            tc_DR_Terr.BorderStyle = BorderStyle.Solid;
            tc_DR_Terr.BorderWidth = 1;
            tc_DR_Terr.Width = 200;
            tc_DR_Terr.RowSpan = 1;
            Literal lit_DR_Terr = new Literal();
            lit_DR_Terr.Text = "<center>" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "</center>";
            tc_DR_Terr.BorderColor = System.Drawing.Color.Black;
            tc_DR_Terr.Style.Add("font-family", "Calibri");
            tc_DR_Terr.Style.Add("font-size", "10pt");
            tc_DR_Terr.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Terr.Controls.Add(lit_DR_Terr);
            tr_header.Cells.Add(tc_DR_Terr);


            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 200;
            tc_DR_HQ.RowSpan = 1;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>Qualification</center>";
            tc_DR_HQ.BorderColor = System.Drawing.Color.Black;
            tc_DR_HQ.Style.Add("font-family", "Calibri");
            tc_DR_HQ.Style.Add("font-size", "10pt");
            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);


            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.BorderStyle = BorderStyle.Solid;
            tc_DR_Des.BorderWidth = 1;
            tc_DR_Des.Width = 120;
            tc_DR_Des.RowSpan = 1;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Category</center>";
            tc_DR_Des.BorderColor = System.Drawing.Color.Black;
            tc_DR_Des.Style.Add("font-family", "Calibri");
            tc_DR_Des.Style.Add("font-size", "10pt");
            tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            TableCell tc_DR_Spe = new TableCell();
            tc_DR_Spe.BorderStyle = BorderStyle.Solid;
            tc_DR_Spe.BorderWidth = 1;
            tc_DR_Spe.Width = 120;
            tc_DR_Spe.RowSpan = 1;
            Literal lit_DR_Spe = new Literal();
            lit_DR_Spe.Text = "<center>Specialty</center>";
            tc_DR_Spe.BorderColor = System.Drawing.Color.Black;
            tc_DR_Spe.Style.Add("font-family", "Calibri");
            tc_DR_Spe.Style.Add("font-size", "10pt");
            tc_DR_Spe.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Spe.Controls.Add(lit_DR_Spe);
            tr_header.Cells.Add(tc_DR_Spe);

            TableCell tc_DR_Class = new TableCell();
            tc_DR_Class.BorderStyle = BorderStyle.Solid;
            tc_DR_Class.BorderWidth = 1;
            tc_DR_Class.Width = 120;
            tc_DR_Class.RowSpan = 1;
            Literal lit_DR_Class = new Literal();
            lit_DR_Class.Text = "<center>Class</center>";
            tc_DR_Class.BorderColor = System.Drawing.Color.Black;
            tc_DR_Class.Style.Add("font-family", "Calibri");
            tc_DR_Class.Style.Add("font-size", "10pt");
            tc_DR_Class.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Class.Controls.Add(lit_DR_Class);
            tr_header.Cells.Add(tc_DR_Class);

            tbl.Rows.Add(tr_header);

            tot_days = getmaxdays_month(Convert.ToInt16(FMonth));

            TableRow tr_day_header = new TableRow();
            tr_day_header.BorderStyle = BorderStyle.Solid;
            tr_day_header.BorderWidth = 1;

            while (cday <= tot_days)
            {
                TableCell tc_day = new TableCell();
                tc_day.BorderStyle = BorderStyle.Solid;
                tc_day.BorderWidth = 1;
                tc_day.Width = 50;
                tc_day.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_day = new Literal();
                lit_day.Text = cday.ToString();
                tc_day.Controls.Add(lit_day);
                tr_header.Cells.Add(tc_day);

                cday = cday + 1;
            }

            if (dsDoctor.Tables[0].Rows.Count > 0)
                ViewState["dsDoctor"] = dsDoctor;

            int iCount = 0;
            int iTotLstCount = 0;
            dsDoctor = (DataSet)ViewState["dsDoctor"];

            foreach (DataRow drFF in dsDoctor.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;
                //strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString();
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                //tc_det_SNo.Style.Add("font-family", "Calibri");
                //tc_det_SNo.Style.Add("font-size", "10pt");
                tc_det_SNo.Style.Add("text-align", "left");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["ListedDrCode"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                //tc_det_usr.Style.Add("font-family", "Calibri");
                //tc_det_usr.Style.Add("font-size", "10pt");
                tc_det_usr.Style.Add("text-align", "left");
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);


                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp;" + drFF["ListedDr_Name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                //tc_det_FF.Style.Add("font-family", "Calibri");
                //tc_det_FF.Style.Add("font-size", "10pt");
                tc_det_FF.Style.Add("text-align", "left");
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                TableCell tc_det_terr = new TableCell();
                Literal lit_det_terr = new Literal();
                lit_det_terr.Text = "&nbsp;" + drFF["territory_Name"].ToString();
                tc_det_terr.BorderStyle = BorderStyle.Solid;
                tc_det_terr.BorderWidth = 1;
                //tc_det_hq.Style.Add("font-family", "Calibri");
                //tc_det_hq.Style.Add("font-size", "10pt");
                tc_det_terr.Style.Add("text-align", "left");
                tc_det_terr.Attributes.Add("Class", "rptCellBorder");
                tc_det_terr.Controls.Add(lit_det_terr);
                tr_det.Cells.Add(tc_det_terr);


                TableCell tc_det_hq = new TableCell();
                Literal lit_det_hq = new Literal();
                lit_det_hq.Text = "&nbsp;" + drFF["Doc_Qua_Name"].ToString();
                tc_det_hq.BorderStyle = BorderStyle.Solid;
                tc_det_hq.BorderWidth = 1;
                //tc_det_hq.Style.Add("font-family", "Calibri");
                //tc_det_hq.Style.Add("font-size", "10pt");
                tc_det_hq.Style.Add("text-align", "left");
                tc_det_hq.Attributes.Add("Class", "rptCellBorder");
                tc_det_hq.Controls.Add(lit_det_hq);
                tr_det.Cells.Add(tc_det_hq);


                TableCell tc_det_Designation = new TableCell();
                Literal lit_det_Designation = new Literal();
                lit_det_Designation.Text = "&nbsp;" + drFF["Doc_Cat_ShortName"].ToString();
                tc_det_Designation.BorderStyle = BorderStyle.Solid;
                tc_det_Designation.BorderWidth = 1;
                //tc_det_Designation.Style.Add("font-family", "Calibri");
                //tc_det_Designation.Style.Add("font-size", "10pt");
                tc_det_Designation.Style.Add("text-align", "left");
                tc_det_Designation.Controls.Add(lit_det_Designation);
                tc_det_Designation.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_det_Designation);

                TableCell tc_det_Spec = new TableCell();
                Literal lit_det_Spec = new Literal();
                lit_det_Spec.Text = "&nbsp;" + drFF["Doc_Spec_ShortName"].ToString();
                tc_det_Spec.BorderStyle = BorderStyle.Solid;
                tc_det_Spec.BorderWidth = 1;
                //tc_det_Spec.Style.Add("font-family", "Calibri");
                //tc_det_Spec.Style.Add("font-size", "10pt");
                tc_det_Spec.Style.Add("text-align", "left");
                tc_det_Spec.Controls.Add(lit_det_Spec);
                tc_det_Spec.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_det_Spec);

                TableCell tc_det_Class = new TableCell();
                Literal lit_det_Class = new Literal();
                lit_det_Class.Text = "&nbsp;" + drFF["Doc_Class_ShortName"].ToString();
                tc_det_Class.BorderStyle = BorderStyle.Solid;
                tc_det_Class.BorderWidth = 1;
                //tc_det_Class.Style.Add("font-family", "Calibri");
                //tc_det_Class.Style.Add("font-size", "10pt");
                tc_det_Class.Style.Add("text-align", "left");
                tc_det_Class.Controls.Add(lit_det_Class);
                tc_det_Class.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_det_Class);


                dsDCR = dc.Visit_Doctor_DCR_Dates(sfCode, divcode, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), drFF["ListedDrCode"].ToString());
                if (dsDCR.Tables[0].Rows.Count > 0)
                {
                    ddate = 1;
                    foreach (DataRow datarow in dsDCR.Tables[0].Rows)
                    {
                        while (ddate <= Convert.ToInt16(datarow["Activity_Date"].ToString()))
                        {
                            if (ddate == Convert.ToInt16(datarow["Activity_Date"].ToString()))
                            {

                                sDCR = "✔";
                            }
                            else
                            {
                                sDCR = "";
                            }
                            TableCell tc_det_day = new TableCell();
                            tc_det_day.BorderStyle = BorderStyle.Solid;
                            tc_det_day.BorderWidth = 1;
                            tc_det_day.Width = 50;
                            tc_det_day.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_day = new Literal();
                            lit_det_day.Text = sDCR;
                            tc_det_day.Controls.Add(lit_det_day);
                            tc_det_day.ToolTip = Convert.ToString(ddate);
                            tc_det_day.Attributes.Add("Class", "rptCellBorder");
                            //tc_det_day.ForeColor = System.Drawing.Color.Red;
                            tr_det.Cells.Add(tc_det_day);
                            ddate = ddate + 1;

                        }
                    }
                    sDCR = "";
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
                            tc_det_day.Attributes.Add("Class", "rptCellBorder");
                            tr_det.Cells.Add(tc_det_day);

                            cday = cday + 1;
                        }
                    }
                }
                else
                {
                    sDCR = "";
                    ddate = 1;
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
                            tc_det_day.Attributes.Add("Class", "rptCellBorder");
                            tr_det.Cells.Add(tc_det_day);

                            cday = cday + 1;
                        }
                    }
                }

                tbl.Rows.Add(tr_det);
            }

        }
        else
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            //tr_header.BackColor = System.Drawing.Color.FromName("#666699");
            tr_header.Style.Add("Color", "Black");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "<center>No Records Found</center>";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Style.Add("font-family", "Calibri");
            tc_SNo.Style.Add("font-size", "10pt");
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            tbl.Rows.Add(tr_header);
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
        form1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}