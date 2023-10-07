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


public partial class MIS_Reports_rptProduct_Exp_Detail : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    DataSet dsDoctr = null;
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
    int fldwrk_total = 0;
    int doctor_total = 0;
    int Chemist_total = 0;
    int Stock_toatal = 0;
    int Stock_calls_Seen_Total = 0;
    int ChemistPOB_total = 0;
    int UnListDoc = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    int CSH_calls_seen_total = 0;
    int Dcr_Sub_days = 0;
    int Dcr_Leave = 0;
    double dblCoverage = 0.00;
    int UnLstdoc_calls_seen_total = 0;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string ChemistPOB_visit = string.Empty;

    string tot_Sub_days = string.Empty;
    string tot_dr = string.Empty;
    string Chemist_visit = string.Empty;
    string Stock_Visit = string.Empty;
    string tot_Stock_Calls_Seen = string.Empty;
    string tot_Dcr_Leave = string.Empty;
    string UnlistVisit = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string tot_doc_Unlstcalls_seen = string.Empty;
    string tot_CSH_calls_seen = string.Empty;
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;

    string sCurrentDate = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        Prod = Request.QueryString["Prod"].ToString();
        Prod_Name = Request.QueryString["Prod_Name"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        lblRegionName.Text = sfname;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Product Exposure Analysis for the Period of " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;


        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;

        if (Prod == "-1")
        {
            lblprd_name.Visible = false;
            FillAll_Product();
        }
        else
        {
            lblname.Text = "<span style='color:red'> " + "" + Prod_Name + "" + "</span>";
            FillSF();
        }

    }
    private void FillSF()
    {
        string sURL = string.Empty;

        tbl.Rows.Clear();
        doctor_total = 0;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.sp_UserList_getMR_Doc_List(divcode, sfCode);


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
            tc_SNo.RowSpan = 2;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 2;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;
            tc_DR_Name.RowSpan = 2;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Fieldforce&nbspName / Month</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 100;
            tc_DR_HQ.RowSpan = 2;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.BorderColor = System.Drawing.Color.Black;
            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);


            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.BorderStyle = BorderStyle.Solid;
            tc_DR_Des.BorderWidth = 1;
            tc_DR_Des.Width = 80;
            tc_DR_Des.RowSpan = 2;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Designation</center>";
            tc_DR_Des.BorderColor = System.Drawing.Color.Black;
            tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);


            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = 1;
                    Literal lit_month = new Literal();
                    Monthsub = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    lit_month.Text = Monthsub.Substring(0, 3) + "-" + cyear;
                    tc_month.Attributes.Add("Class", "rptCellBorder");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
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
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_lst_month = new TableCell();
                    HyperLink lit_lst_month = new HyperLink();
                    lit_lst_month.Text = "No. of Drs (As Per DCR)";
                    tc_lst_month.BorderStyle = BorderStyle.Solid;
                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month.BorderWidth = 1;
                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                    tc_lst_month.Controls.Add(lit_lst_month);
                    tr_lst_det.Cells.Add(tc_lst_month);


                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
                tr_lst_det.BackColor = System.Drawing.Color.FromName("#0097AC");
                tr_lst_det.Style.Add("Color", "White");

                tr_lst_det.Attributes.Add("Class", "Backcolor");

                tbl.Rows.Add(tr_lst_det);
            }

            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            int iTotLstCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;
                strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Sf_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                //hq
                TableCell tc_det_hq = new TableCell();
                Literal lit_det_hq = new Literal();
                lit_det_hq.Text = "&nbsp;" + drFF["sf_hq"].ToString();
                tc_det_hq.BorderStyle = BorderStyle.Solid;
                tc_det_hq.BorderWidth = 1;
                tc_det_hq.Attributes.Add("Class", "rptCellBorder");
                tc_det_hq.Controls.Add(lit_det_hq);
                tr_det.Cells.Add(tc_det_hq);

                //SF Designation Short Name
                TableCell tc_det_Designation = new TableCell();
                Literal lit_det_Designation = new Literal();
                lit_det_Designation.Text = "&nbsp;" + drFF["sf_Designation_Short_Name"].ToString();
                tc_det_Designation.BorderStyle = BorderStyle.Solid;
                tc_det_Designation.BorderWidth = 1;
                tc_det_Designation.Controls.Add(lit_det_Designation);
                tc_det_Designation.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_det_Designation);

                months = Convert.ToInt16(ViewState["months"].ToString());
                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                cyear = Convert.ToInt16(ViewState["cyear"].ToString());


                if (months >= 0)
                {

                    for (int j = 1; j <= months + 1; j++)
                    {

                        if (cmonth == 12)
                        {
                            sCurrentDate = "01-01-" + (cyear + 1);
                        }
                        else
                        {
                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                        }

                        dtCurrent = Convert.ToDateTime(sCurrentDate);


                        dsDoc = sf.getProduct_Exp_count(drFF["sf_code"].ToString(), divcode, cmonth, cyear, Convert.ToInt16(Prod), sCurrentDate);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                        TableCell tc_lst_month = new TableCell();
                        HyperLink hyp_lst_month = new HyperLink();

                        if (tot_dr != "0")
                        {
                            //iTotLstCount += Convert.ToInt16(tot_dr);
                            hyp_lst_month.Text = tot_dr;

                            //sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&Year=" + cyear + "&Month=" + cmonth + "&Prod_Name=" + Prod_Name + "&Prod=" + Prod + "&sCurrentDate=" + sCurrentDate + "";

                            hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "', '" + cyear + "', '" + cmonth + "', '" + Prod_Name + "', '" + Prod + "','" + sCurrentDate + "')");
                            //hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp('" + sURL + "')";
                            hyp_lst_month.NavigateUrl = "#";



                        }

                        else
                        {
                            hyp_lst_month.Text = "";
                        }


                        tc_lst_month.BorderStyle = BorderStyle.Solid;
                        tc_lst_month.BorderWidth = 1;
                        tc_lst_month.BackColor = System.Drawing.Color.White;
                        tc_lst_month.Width = 200;
                        tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                        tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                        tc_lst_month.Controls.Add(hyp_lst_month);
                        tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_lst_month);

                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }

                    }
                    //

                }

                tbl.Rows.Add(tr_det);

            }



            TableRow tr_total = new TableRow();

            TableCell tc_Count_Total = new TableCell();
            tc_Count_Total.BorderStyle = BorderStyle.Solid;
            tc_Count_Total.BorderWidth = 1;
            //tc_catg_Total.Width = 25;
            Literal lit_Count_Total = new Literal();
            lit_Count_Total.Text = "<center>Total</center>";
            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_Count_Total.Controls.Add(lit_Count_Total);
            tc_Count_Total.Font.Bold.ToString();
            tc_Count_Total.BackColor = System.Drawing.Color.White;
            tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
            tc_Count_Total.ColumnSpan = 4;
            tc_Count_Total.Style.Add("text-align", "left");
            tc_Count_Total.Style.Add("font-family", "Calibri");
            tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
            tc_Count_Total.Style.Add("font-size", "10pt");

            tr_total.Cells.Add(tc_Count_Total);


            Session["Test"] = "";

            Session["Sf_Code_multiple"] = strSf_Code.Remove(strSf_Code.Length - 1);

            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());

            if (months >= 0)
            {
                Session["Test"] = "T";
                for (int j = 1; j <= months + 1; j++)
                {
                    if (cmonth == 12)
                    {
                        sCurrentDate = "01-01-" + (cyear + 1);
                    }
                    else
                    {
                        sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                    }

                    dtCurrent = Convert.ToDateTime(sCurrentDate);

                    TableCell tc_tot_month = new TableCell();
                    HyperLink hyp_month = new HyperLink();
                    iTotLstCount = 0;
                    foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                    {

                        dsDoc = sf.getProduct_Exp_count(drFF["sf_code"].ToString(), divcode, cmonth, cyear, Convert.ToInt16(Prod), sCurrentDate);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        if (tot_dr != "0")
                        {                          
                            iTotLstCount += Convert.ToInt16(tot_dr);
                            hyp_month.Text = iTotLstCount.ToString();

                            hyp_month.Attributes.Add("href", "javascript:showModalPopUp('" + 0 + "', '" + drFF["sf_name"] + "', '" + cyear + "', '" + cmonth + "', '" + Prod_Name + "', '" + Prod + "','" + sCurrentDate + "')");                         
                        }
                    }


                    tc_tot_month.BorderStyle = BorderStyle.Solid;
                    tc_tot_month.BorderWidth = 1;
                    tc_tot_month.BackColor = System.Drawing.Color.White;
                    tc_tot_month.Width = 200;
                    tc_tot_month.Style.Add("font-family", "Calibri");
                    tc_tot_month.Style.Add("font-size", "10pt");
                    tc_tot_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_tot_month.VerticalAlign = VerticalAlign.Middle;
                    tc_tot_month.Controls.Add(hyp_month);
                    tc_tot_month.Attributes.Add("style", "font-weight:bold;");
                    tc_tot_month.Attributes.Add("Class", "rptCellBorder");
                    tr_total.Cells.Add(tc_tot_month);
                    Session["Test"] = "G";
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }

                }
            }

            tbl.Rows.Add(tr_total);
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

    private void FillAll_Product()
    {
        string sURL = string.Empty;

        tbl.Rows.Clear();
        doctor_total = 0;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getProduct_Exp(divcode);


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
            tc_SNo.RowSpan = 2;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 2;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>Product Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;
            tc_DR_Name.RowSpan = 2;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Product Name / Month</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_prd = new TableCell();
            tc_DR_prd.BorderStyle = BorderStyle.Solid;
            tc_DR_prd.BorderWidth = 1;
            tc_DR_prd.Width = 80;
            tc_DR_prd.RowSpan = 2;
            Literal lit_DR_prd = new Literal();
            lit_DR_prd.Text = "<center>No of Doctors - Tagged</center>";
            tc_DR_prd.BorderColor = System.Drawing.Color.Black;
            tc_DR_prd.Attributes.Add("Class", "rptCellBorder");
            tc_DR_prd.Controls.Add(lit_DR_prd);
            tr_header.Cells.Add(tc_DR_prd);


            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);


            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = 1;
                    Literal lit_month = new Literal();
                    Monthsub = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    lit_month.Text = Monthsub.Substring(0, 3) + "-" + cyear;
                    tc_month.Attributes.Add("Class", "rptCellBorder");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
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
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_lst_month = new TableCell();
                    HyperLink lit_lst_month = new HyperLink();
                    lit_lst_month.Text = "No. of Drs (As Per DCR)";
                    tc_lst_month.BorderStyle = BorderStyle.Solid;
                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month.BorderWidth = 1;
                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                    tc_lst_month.Controls.Add(lit_lst_month);
                    tr_lst_det.Cells.Add(tc_lst_month);


                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
                tr_lst_det.BackColor = System.Drawing.Color.FromName("#0097AC");
                tr_lst_det.Style.Add("Color", "White");

                tr_lst_det.Attributes.Add("Class", "Backcolor");

                tbl.Rows.Add(tr_lst_det);
            }

            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            int iTotLstCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;

                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //Product Code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Product_Code_SlNo"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);


                //Product Name
                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp;" + drFF["Product_Detail_Name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);



                dsDoctr = sf.getprd_Dr_count(divcode, sfCode, Convert.ToInt16(drFF["Product_Code_SlNo"].ToString()));


                if (dsDoctr.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoctr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                TableCell tc_lst_mon = new TableCell();
                HyperLink hyp_lst_mon = new HyperLink();

                if (tot_dr != "0")
                {
                    //iTotLstCount += Convert.ToInt16(tot_dr);
                    hyp_lst_mon.Text = tot_dr;

                    //sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&Year=" + cyear + "&Month=" + cmonth + "&Prod_Name=" + Prod_Name + "&Prod=" + Prod + "&sCurrentDate=" + sCurrentDate + "";

                    //hyp_lst_mon.Attributes.Add("href", "javascript:showModalPopUp('" + sfCode + "', '" + sfname + "','" + drFF["Product_Detail_Name"] + "', '" + drFF["Product_Code_SlNo"] + "')");

                    hyp_lst_mon.Attributes.Add("href", "javascript:showModal('" + sfCode.ToString() + "', '" + sfname + "', '" + drFF["Product_Detail_Name"] + "', '" + drFF["Product_Code_SlNo"] + "')");

                    ////hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp('" + sURL + "')";
                    hyp_lst_mon.NavigateUrl = "#";

                }

                else
                {
                    hyp_lst_mon.Text = "";
                }


                tc_lst_mon.BorderStyle = BorderStyle.Solid;
                tc_lst_mon.BorderWidth = 1;
                tc_lst_mon.BackColor = System.Drawing.Color.White;
                tc_lst_mon.Width = 200;
                tc_lst_mon.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_mon.VerticalAlign = VerticalAlign.Middle;
                tc_lst_mon.Controls.Add(hyp_lst_mon);
                tc_lst_mon.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_mon);

                months = Convert.ToInt16(ViewState["months"].ToString());
                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                cyear = Convert.ToInt16(ViewState["cyear"].ToString());

                if (months >= 0)
                {
                    for (int j = 1; j <= months + 1; j++)
                    {
                        if (cmonth == 12)
                        {
                            sCurrentDate = "01-01-" + (cyear + 1);
                        }
                        else
                        {
                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                        }

                        dtCurrent = Convert.ToDateTime(sCurrentDate);


                        dsDoc = sf.getProduct_Exp_count(sfCode, divcode, cmonth, cyear, Convert.ToInt16(drFF["Product_Code_SlNo"].ToString()), sCurrentDate);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                        TableCell tc_lst_month = new TableCell();
                        HyperLink hyp_lst_month = new HyperLink();

                        if (tot_dr != "0")
                        {
                            hyp_lst_month.Text = tot_dr;

                            //sURL = "rptProduct_Exp_Detail1.aspx?sf_Name=" + "&sf_code=" + sfCode + "&sf_name=" + sfname + "&Year=" + cyear + "&Month=" + cmonth + "&Prod=" + drFF["Product_Code_SlNo"] + "&Prod_Name=" + drFF["Product_Detail_Name"] + "&sCurrentDate=" + sCurrentDate +  "";

                            hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + sfCode.ToString() + "', '" + sfname + "', '" + cyear + "', '" + cmonth + "', '" + drFF["Product_Detail_Name"] + "', '" + drFF["Product_Code_SlNo"] + "','" + sCurrentDate + "')");

                            //hyp_lst_month.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=700,height=600,left=0,top=0');";
                            hyp_lst_month.NavigateUrl = "#";


                        }

                        else
                        {
                            hyp_lst_month.Text = "";
                        }


                        tc_lst_month.BorderStyle = BorderStyle.Solid;
                        tc_lst_month.BorderWidth = 1;
                        tc_lst_month.BackColor = System.Drawing.Color.White;
                        tc_lst_month.Width = 200;
                        tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                        tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                        tc_lst_month.Controls.Add(hyp_lst_month);
                        tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_lst_month);

                        iTotLstCount += Convert.ToInt16(tot_dr);


                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }

                    }
                    //

                }

                tbl.Rows.Add(tr_det);

            }
            TableRow tr_total = new TableRow();

            TableCell tc_Count_Total = new TableCell();
            tc_Count_Total.BorderStyle = BorderStyle.Solid;
            tc_Count_Total.BorderWidth = 1;
            //tc_catg_Total.Width = 25;
            Literal lit_Count_Total = new Literal();
            lit_Count_Total.Text = "<center>Total</center>";
            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_Count_Total.Controls.Add(lit_Count_Total);
            tc_Count_Total.Font.Bold.ToString();
            tc_Count_Total.BackColor = System.Drawing.Color.White;
            tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
            tc_Count_Total.ColumnSpan = 3;
            tc_Count_Total.Style.Add("text-align", "left");
            tc_Count_Total.Style.Add("font-family", "Calibri");
            tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
            tc_Count_Total.Style.Add("font-size", "10pt");

            tr_total.Cells.Add(tc_Count_Total);


            Session["Test"] = "";

            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());

            if (months >= 0)
            {
                Session["Test"] = "T";
                for (int j = 1; j <= months + 1; j++)
                {
                    if (cmonth == 12)
                    {
                        sCurrentDate = "01-01-" + (cyear + 1);
                    }
                    else
                    {
                        sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                    }

                    dtCurrent = Convert.ToDateTime(sCurrentDate);

                    TableCell tc_tot_month = new TableCell();
                    HyperLink hyp_month = new HyperLink();
                    iTotLstCount = 0;
                    foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                    {

                        dsDoc = sf.getProduct_Exp_count(sfCode, divcode, cmonth, cyear, Convert.ToInt16(drFF["Product_Code_SlNo"].ToString()), sCurrentDate);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        if (tot_dr != "0")
                        {
                            //hyp_month.Text = tot_dr;

                            //if (Session["Test"] == "T")
                            //{
                            iTotLstCount += Convert.ToInt16(tot_dr);
                            hyp_month.Text = iTotLstCount.ToString();
                            //}
                        }
                    }


                    tc_tot_month.BorderStyle = BorderStyle.Solid;
                    tc_tot_month.BorderWidth = 1;
                    tc_tot_month.BackColor = System.Drawing.Color.White;
                    tc_tot_month.Width = 200;
                    tc_tot_month.Style.Add("font-family", "Calibri");
                    tc_tot_month.Style.Add("font-size", "10pt");
                    tc_tot_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_tot_month.VerticalAlign = VerticalAlign.Middle;
                    tc_tot_month.Controls.Add(hyp_month);
                    tc_tot_month.Attributes.Add("style", "font-weight:bold;");
                    tc_tot_month.Attributes.Add("Class", "rptCellBorder");
                    tr_total.Cells.Add(tc_tot_month);
                    Session["Test"] = "G";
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }

                }
            }

            tbl.Rows.Add(tr_total);
        }
    }
}
