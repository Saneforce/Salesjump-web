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

public partial class MasterFiles_AnalysisReports_rptMgr_Coverage : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    DataSet dsdcr = null;
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
    string tot_docSeen = "";
    string tot_fldwrkDays = "";
    string strFieledForceName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
       // div_code = Request.QueryString["div_Code"].ToString();
        div_code = Session["div_code"].ToString();
        //sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        TMonth = Request.QueryString["To_Month"].ToString();
        TYear = Request.QueryString["To_year"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();

        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth);
        string strToMonth = sf.getMonthName(TMonth);
        lblHead.Text = "Manager - HQ - Coverage From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        CreateDynamicTable();
    }

    private void CreateDynamicTable()
    {
        int iCount = 0;

        SalesForce sf = new SalesForce();

        dsSalesForce = sf.getmode(div_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tr_header.Attributes.Add("Class", "tblCellFont");


            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 500;

            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Parameter</center>";
            tc_DR_Name.Style.Add("font-family", "Calibri");
            tc_DR_Name.Style.Add("font-size", "10pt");
            tc_DR_Name.Style.Add("Color", "White");
            tc_DR_Name.Style.Add("border-color", "Black");
            tc_DR_Name.Attributes.Add("Class", "tr_det_head");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);

            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            //    tbl.Rows.Add(tr_header);

            TableRow tr_catg1 = new TableRow();
            if (months >= 0)
            {

                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    // tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                    //tc_month.RowSpan = 2;
                    Literal lit_month = new Literal();
                    lit_month.Text = "&nbsp;" + sf.getMonthName(cmonth.ToString()).Substring(0, 3) + "-" + cyear;
                    tc_month.Style.Add("font-family", "Calibri");
                    tc_month.Style.Add("font-size", "10pt");
                    tc_month.Style.Add("Color", "White");
                    tc_month.Style.Add("border-color", "Black");
                    tc_month.Attributes.Add("Class", "tr_det_head");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;

                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    // tr_catg1.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }

            //   tbl.Rows.Add(tr_catg1);

            TableRow tr_lst_det = new TableRow();
            TableCell tc_DR_Total = new TableCell();
            tc_DR_Total.BorderStyle = BorderStyle.Solid;
            tc_DR_Total.BorderWidth = 1;
            tc_DR_Total.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            //tc_DR_Total.Width = 50;
            Literal lit_DR_Total = new Literal();
            lit_DR_Total.Text = "<center>Total</center>";
            tc_DR_Total.Style.Add("Color", "White");
            tc_DR_Total.Style.Add("border-color", "Black");
            tc_DR_Total.Attributes.Add("Class", "tr_det_head");
            tc_DR_Total.Controls.Add(lit_DR_Total);
            //  tr_lst_det.Cells.Add(tc_DR_Total);


            tr_header.Cells.Add(tc_DR_Total);
            tbl.Rows.Add(tr_header);
            int cmonthact = Convert.ToInt32(FMonth);
            int tmonthact = Convert.ToInt32(TMonth);
            int cyearact = Convert.ToInt32(FYear);
            int tyearact = Convert.ToInt32(TYear);

            //TableRow tr_catg3 = new TableRow();
            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            ViewState["months"] = months1;
            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;

            foreach (DataRow drow in dsSalesForce.Tables[0].Rows)
            {
                DataSet dsCall = new DataSet();
                DataSet dsField = new DataSet();
                double dblaverage = 0.00;

                TableRow tr_det = new TableRow();
                if (drow["Doc_Cat_SName"].ToString() == "Total Days in Month")
                {



                    tr_det.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_FF1 = new Literal();
                    lit_det_FF1.Text = "&nbsp" + drow[0];
                    tc_det_FF1.BorderStyle = BorderStyle.Solid;
                    tc_det_FF1.BorderWidth = 1;
                    tc_det_FF1.Controls.Add(lit_det_FF1);
                    tc_det_FF1.Style.Add("text-align", "left");
                    tc_det_FF1.Style.Add("font-family", "Calibri");
                    tc_det_FF1.Style.Add("font-size", "10pt");
                    tr_det.Cells.Add(tc_det_FF1);

                    if (months1 >= 0)
                    {
                        string tot_fldwrk = string.Empty;
                        DCR dcr1 = new DCR();
                        DataSet ds = new DataSet();
                        tot_fldwrk = "";
                        int itotWorkType = 0;
                        int fldwrk_total = 0;

                        for (int j = 1; j <= months1 + 1; j++)
                        {

                            ds = dcr1.getMonth_Count(cmonth1, cyear1);
                            if (ds.Tables[0].Rows.Count > 0)
                                tot_fldwrk = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);
                            //itotWorkType += fldwrk_total;

                            tr_det.BackColor = System.Drawing.Color.White;
                            TableCell tc_det_FF = new TableCell();
                            Literal lit_det_FF = new Literal();
                            lit_det_FF.Text = "&nbsp;" + tot_fldwrk;
                            tc_det_FF.BorderStyle = BorderStyle.Solid;
                            tc_det_FF.BorderWidth = 1;
                            tc_det_FF.Controls.Add(lit_det_FF);
                            tc_det_FF.Style.Add("text-align", "left");
                            tc_det_FF.Style.Add("font-family", "Calibri");
                            tc_det_FF.Style.Add("font-size", "10pt");
                            tr_det.Cells.Add(tc_det_FF);


                            cmonth1 = cmonth1 + 1;
                            if (cmonth1 == 13)
                            {
                                cmonth1 = 1;
                                cyear1 = cyear1 + 1;
                            }

                        }

                        //int[] arrWorkType = new int[] { itotWorkType };

                        //for (int W = 0; W < arrWorkType.Length; W++)
                        //{
                        //    iSumLeave += arrWorkType[W];
                        //}

                        TableCell tc_det_sf_Tot = new TableCell();
                        Literal lit_det_sf_Tot = new Literal();

                        if (itotWorkType == 0)
                        {
                            lit_det_sf_Tot.Text = "&nbsp;" + "-";
                        }
                        else
                        {
                            lit_det_sf_Tot.Text = "&nbsp;" + itotWorkType;
                        }
                        tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_Tot.BorderWidth = 1;
                        //tc_det_sf_Tot.Width = 50;
                        tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_Tot.Style.Add("text-align", "left");
                        tc_det_sf_Tot.Style.Add("font-family", "Calibri");
                        tc_det_sf_Tot.Style.Add("font-size", "10pt");
                        tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
                        tr_det.Cells.Add(tc_det_sf_Tot);
                    }

                    tbl.Rows.Add(tr_det);


                }

                else if (drow["Doc_Cat_SName"].ToString() == "Working Days (Excl/Holidays & Sundays )")
                {
                    int months2 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
                    int cmonth2 = Convert.ToInt32(FMonth);
                    int cyear2 = Convert.ToInt32(FYear);

                    ViewState["months"] = months2;
                    ViewState["cmonth"] = cmonth2;
                    ViewState["cyear"] = cyear2;
                    TableRow tr_det1 = new TableRow();
                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_FF1 = new Literal();
                    lit_det_FF1.Text = "&nbsp" + drow[0];
                    tc_det_FF1.BorderStyle = BorderStyle.Solid;
                    tc_det_FF1.BorderWidth = 1;
                    tc_det_FF1.Controls.Add(lit_det_FF1);
                    tc_det_FF1.Style.Add("text-align", "left");
                    tc_det_FF1.Style.Add("font-family", "Calibri");
                    tc_det_FF1.Style.Add("font-size", "10pt");
                    tr_det1.Cells.Add(tc_det_FF1);

                    DCR dcr1 = new DCR();
                    string tot_fldwrk = "";
                    int itotWorkType = 0;
                    int fldwrk_total = 0;
                    // int iSumLeave = 0;
                    if (months2 >= 0)
                    {
                        for (int j = 1; j <= months2 + 1; j++)
                        {
                            DataSet ds = new DataSet();
                            ds = dcr1.getWorking_Days(sf_code, div_code, cmonth2, cyear2);
                            if (ds.Tables[0].Rows.Count > 0)
                                tot_fldwrk = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);

                            tr_det1.BackColor = System.Drawing.Color.White;
                            TableCell tc_det_FF = new TableCell();
                            Literal lit_det_FF = new Literal();
                            lit_det_FF.Text = "&nbsp" + ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            tc_det_FF.BorderStyle = BorderStyle.Solid;
                            tc_det_FF.BorderWidth = 1;
                            tc_det_FF.Controls.Add(lit_det_FF);
                            tc_det_FF.Style.Add("text-align", "left");
                            tc_det_FF.Style.Add("font-family", "Calibri");
                            tc_det_FF.Style.Add("font-size", "10pt");
                            tr_det1.Cells.Add(tc_det_FF);


                            cmonth2 = cmonth2 + 1;
                            if (cmonth2 == 13)
                            {
                                cmonth2 = 1;
                                cyear2 = cyear2 + 1;
                            }
                        }
                        TableCell tc_det_sf_Tot = new TableCell();
                        Literal lit_det_sf_Tot = new Literal();

                        if (itotWorkType == 0)
                        {
                            lit_det_sf_Tot.Text = "&nbsp;" + "-";
                        }
                        else
                        {
                            lit_det_sf_Tot.Text = "&nbsp;" + itotWorkType;
                        }
                        tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_Tot.Style.Add("text-align", "left");
                        tc_det_sf_Tot.Style.Add("font-family", "Calibri");
                        tc_det_sf_Tot.Style.Add("font-size", "10pt");
                        tc_det_sf_Tot.BorderWidth = 1;
                        //tc_det_sf_Tot.Width = 50;
                        tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
                        tr_det1.Cells.Add(tc_det_sf_Tot);
                    }

                    tbl.Rows.Add(tr_det1);
                }
                else if (drow["Doc_Cat_SName"].ToString() == "Fieldwork Days")
                {
                    TableRow tr_det1 = new TableRow();
                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_FF1 = new Literal();
                    lit_det_FF1.Text = "&nbsp" + drow[0];
                    tc_det_FF1.BorderStyle = BorderStyle.Solid;
                    tc_det_FF1.BorderWidth = 1;
                    tc_det_FF1.Controls.Add(lit_det_FF1);
                    tc_det_FF1.Style.Add("text-align", "left");
                    tc_det_FF1.Style.Add("font-family", "Calibri");
                    tc_det_FF1.Style.Add("font-size", "10pt");
                    tr_det1.Cells.Add(tc_det_FF1);

                    DCR dcr1 = new DCR();

                    int itotWorkType = 0;
                    int fldwrk_total = 0;

                    if (months >= 0)
                    {
                        for (int j = 1; j <= months + 1; j++)
                        {
                            // DataSet ds = new DataSet();
                            dsField = dcr1.getFieldwork_Days(sf_code, div_code, cmonthact, cyearact);

                            if (dsField.Tables[0].Rows.Count > 0)
                                tot_fldwrkDays = dsField.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrkDays);
                            tr_det1.BackColor = System.Drawing.Color.White;
                            TableCell tc_det_FF = new TableCell();
                            Literal lit_det_FF = new Literal();
                            lit_det_FF.Text = "&nbsp" + dsField.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            tc_det_FF.BorderStyle = BorderStyle.Solid;
                            tc_det_FF.BorderWidth = 1;
                            tc_det_FF.Controls.Add(lit_det_FF);
                            tc_det_FF.Style.Add("text-align", "left");
                            tc_det_FF.Style.Add("font-family", "Calibri");
                            tc_det_FF.Style.Add("font-size", "10pt");
                            tr_det1.Cells.Add(tc_det_FF);

                            cmonthact = cmonthact + 1;
                            if (cmonthact == 13)
                            {
                                cmonthact = 1;
                                cyearact = cyearact + 1;
                            }
                        }
                        TableCell tc_det_sf_Tot = new TableCell();
                        Literal lit_det_sf_Tot = new Literal();

                        if (itotWorkType == 0)
                        {
                            lit_det_sf_Tot.Text = "&nbsp;" + "-";
                        }
                        else
                        {
                            lit_det_sf_Tot.Text = "&nbsp;" + itotWorkType;
                        }
                        tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_Tot.Style.Add("text-align", "left");
                        tc_det_sf_Tot.Style.Add("font-family", "Calibri");
                        tc_det_sf_Tot.Style.Add("font-size", "10pt");
                        tc_det_sf_Tot.BorderWidth = 1;
                        //tc_det_sf_Tot.Width = 50;
                        tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
                        tr_det1.Cells.Add(tc_det_sf_Tot);

                    }

                    tbl.Rows.Add(tr_det1);
                }


                else if (drow["Doc_Cat_SName"].ToString() == "Leave")
                {
                    int months3 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
                    int cmonth3 = Convert.ToInt32(FMonth);
                    int cyear3 = Convert.ToInt32(FYear);

                    ViewState["months"] = months3;
                    ViewState["cmonth"] = cmonth3;
                    ViewState["cyear"] = cyear3;

                    TableRow tr_det1 = new TableRow();
                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_FF1 = new Literal();
                    lit_det_FF1.Text = "&nbsp" + drow[0];
                    tc_det_FF1.BorderStyle = BorderStyle.Solid;
                    tc_det_FF1.BorderWidth = 1;
                    tc_det_FF1.Controls.Add(lit_det_FF1);
                    tc_det_FF1.Style.Add("text-align", "left");
                    tc_det_FF1.Style.Add("font-family", "Calibri");
                    tc_det_FF1.Style.Add("font-size", "10pt");
                    tr_det1.Cells.Add(tc_det_FF1);

                    DCR dcr1 = new DCR();
                    string tot_fldwrk = "";
                    int itotWorkType = 0;
                    int fldwrk_total = 0;
                    if (months3 >= 0)
                    {
                        for (int j = 1; j <= months3 + 1; j++)
                        {
                            DataSet ds = new DataSet();
                            ds = dcr1.getLeave_Days(sf_code, div_code, cmonth3, cyear3);

                            if (ds.Tables[0].Rows.Count > 0)
                                tot_fldwrk = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);
                            //  string leave = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                            tr_det1.BackColor = System.Drawing.Color.White;
                            TableCell tc_det_FF = new TableCell();
                            Literal lit_det_FF = new Literal();

                            if (tot_fldwrk == "0")
                            {
                                lit_det_FF.Text = "&nbsp;" + " -";
                            }
                            else
                            {
                                lit_det_FF.Text = "&nbsp" + tot_fldwrk;
                            }
                            tc_det_FF.BorderStyle = BorderStyle.Solid;
                            tc_det_FF.BorderWidth = 1;
                            tc_det_FF.Controls.Add(lit_det_FF);
                            tc_det_FF.Style.Add("text-align", "left");
                            tc_det_FF.Style.Add("font-family", "Calibri");
                            tc_det_FF.Style.Add("font-size", "10pt");
                            tr_det1.Cells.Add(tc_det_FF);

                            cmonth3 = cmonth3 + 1;
                            if (cmonth3 == 13)
                            {
                                cmonth3 = 1;
                                cyear3 = cyear3 + 1;
                            }
                        }
                        TableCell tc_det_sf_Tot = new TableCell();
                        Literal lit_det_sf_Tot = new Literal();

                        if (itotWorkType == 0)
                        {
                            lit_det_sf_Tot.Text = "&nbsp;" + "-";
                        }
                        else
                        {
                            lit_det_sf_Tot.Text = "&nbsp;" + itotWorkType;
                        }
                        tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_Tot.BorderWidth = 1;
                        tc_det_sf_Tot.Style.Add("text-align", "left");
                        tc_det_sf_Tot.Style.Add("font-family", "Calibri");
                        tc_det_sf_Tot.Style.Add("font-size", "10pt");
                        //tc_det_sf_Tot.Width = 50;
                        tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
                        tr_det1.Cells.Add(tc_det_sf_Tot);

                    }
                    tbl.Rows.Add(tr_det1);
                }


                else if (drow["Doc_Cat_SName"].ToString() == "TP Deviation Days")
                {
                    int months4 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
                    int cmonth4 = Convert.ToInt32(FMonth);
                    int cyear4 = Convert.ToInt32(FYear);

                    ViewState["months"] = months4;
                    ViewState["cmonth"] = cmonth4;
                    ViewState["cyear"] = cyear4;
                    TableRow tr_det1 = new TableRow();
                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_FF1 = new Literal();
                    lit_det_FF1.Text = "&nbsp" + drow[0];
                    tc_det_FF1.BorderStyle = BorderStyle.Solid;
                    tc_det_FF1.BorderWidth = 1;
                    tc_det_FF1.Controls.Add(lit_det_FF1);
                    tc_det_FF1.Style.Add("text-align", "left");
                    tc_det_FF1.Style.Add("font-family", "Calibri");
                    tc_det_FF1.Style.Add("font-size", "10pt");
                    tr_det1.Cells.Add(tc_det_FF1);

                    DCR dcr1 = new DCR();

                    //DataSet ds = new DataSet();
                    //ds = dcr1.getLeave_Days(sf_code, div_code, cmonthact, cyearact);
                    if (months4 >= 0)
                    {
                        for (int j = 1; j <= months4 + 1; j++)
                        {

                            tr_det1.BackColor = System.Drawing.Color.White;
                            TableCell tc_det_FF = new TableCell();
                            Literal lit_det_FF = new Literal();
                            lit_det_FF.Text = "&nbsp - ";
                            tc_det_FF.BorderStyle = BorderStyle.Solid;
                            tc_det_FF.BorderWidth = 1;
                            tc_det_FF.Controls.Add(lit_det_FF);
                            tc_det_FF.Style.Add("text-align", "left");
                            tc_det_FF.Style.Add("font-family", "Calibri");
                            tc_det_FF.Style.Add("font-size", "10pt");
                            tr_det1.Cells.Add(tc_det_FF);

                            cmonth4 = cmonth4 + 1;
                            if (cmonth4 == 13)
                            {
                                cmonth4 = 1;
                                cyear4 = cyear4 + 1;
                            }
                        }
                    }
                    tbl.Rows.Add(tr_det1);
                }

                else if (drow["Doc_Cat_SName"].ToString() == "No of Listed Drs Met")
                {
                    int months5 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
                    int cmonth5 = Convert.ToInt32(FMonth);
                    int cyear5 = Convert.ToInt32(FYear);

                    ViewState["months"] = months5;
                    ViewState["cmonth"] = cmonth5;
                    ViewState["cyear"] = cyear5;
                    TableRow tr_det1 = new TableRow();
                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_FF1 = new Literal();
                    lit_det_FF1.Text = "&nbsp" + drow[0];
                    tc_det_FF1.BorderStyle = BorderStyle.Solid;
                    tc_det_FF1.BorderWidth = 1;
                    tc_det_FF1.Controls.Add(lit_det_FF1);
                    tc_det_FF1.Style.Add("text-align", "left");
                    tc_det_FF1.Style.Add("font-family", "Calibri");
                    tc_det_FF1.Style.Add("font-size", "10pt");
                    tr_det1.Cells.Add(tc_det_FF1);

                    DCR dcr1 = new DCR();
                    string tot_fldwrk = "";
                    int itotWorkType = 0;
                    int fldwrk_total = 0;
                    if (months5 >= 0)
                    {
                        for (int j = 1; j <= months5 + 1; j++)
                        {
                            DataSet ds = new DataSet();
                            ds = dcr1.DCR_Doc_Met_Self(sf_code, div_code, cmonth5, cyear5);
                            if (ds.Tables[0].Rows.Count > 0)
                                tot_fldwrk = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);

                            tr_det1.BackColor = System.Drawing.Color.White;
                            TableCell tc_det_FF = new TableCell();
                            Literal lit_det_FF = new Literal();
                            lit_det_FF.Text = "&nbsp" + ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            tc_det_FF.BorderStyle = BorderStyle.Solid;
                            tc_det_FF.BorderWidth = 1;
                            tc_det_FF.Controls.Add(lit_det_FF);
                            tc_det_FF.Style.Add("text-align", "left");
                            tc_det_FF.Style.Add("font-family", "Calibri");
                            tc_det_FF.Style.Add("font-size", "10pt");
                            tr_det1.Cells.Add(tc_det_FF);

                            cmonth5 = cmonth5 + 1;
                            if (cmonth5 == 13)
                            {
                                cmonth5 = 1;
                                cyear5 = cyear5 + 1;
                            }
                        }
                        TableCell tc_det_sf_Tot = new TableCell();
                        Literal lit_det_sf_Tot = new Literal();

                        if (itotWorkType == 0)
                        {
                            lit_det_sf_Tot.Text = "&nbsp;" + "-";
                        }
                        else
                        {
                            lit_det_sf_Tot.Text = "&nbsp;" + itotWorkType;
                        }
                        tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_Tot.Style.Add("text-align", "left");
                        tc_det_sf_Tot.Style.Add("font-family", "Calibri");
                        tc_det_sf_Tot.Style.Add("font-size", "10pt");
                        tc_det_sf_Tot.BorderWidth = 1;
                        //tc_det_sf_Tot.Width = 50;
                        tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
                        tr_det1.Cells.Add(tc_det_sf_Tot);
                    }
                    tbl.Rows.Add(tr_det1);
                }

                else if (drow["Doc_Cat_SName"].ToString() == "No of Listed Drs Seen")
                {
                    int months6 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
                    int cmonth6 = Convert.ToInt32(FMonth);
                    int cyear6 = Convert.ToInt32(FYear);

                    ViewState["months"] = months6;
                    ViewState["cmonth"] = cmonth6;
                    ViewState["cyear"] = cyear6;
                    TableRow tr_det1 = new TableRow();
                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_FF1 = new Literal();
                    lit_det_FF1.Text = "&nbsp" + drow[0];
                    tc_det_FF1.BorderStyle = BorderStyle.Solid;
                    tc_det_FF1.BorderWidth = 1;
                    tc_det_FF1.Controls.Add(lit_det_FF1);
                    tc_det_FF1.Style.Add("text-align", "left");
                    tc_det_FF1.Style.Add("font-family", "Calibri");
                    tc_det_FF1.Style.Add("font-size", "10pt");
                    tr_det1.Cells.Add(tc_det_FF1);

                    DCR dcr1 = new DCR();

                    int itotWorkType = 0;
                    int fldwrk_total = 0;
                    if (months >= 0)
                    {
                        for (int j = 1; j <= months + 1; j++)
                        {

                            dsCall = dcr1.DCR_Doc_Seen_Self(sf_code, div_code, cmonth6, cyear6);
                            if (dsCall.Tables[0].Rows.Count > 0)
                                tot_docSeen = dsCall.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            itotWorkType += fldwrk_total + Convert.ToInt16(tot_docSeen);

                            tr_det1.BackColor = System.Drawing.Color.White;
                            TableCell tc_det_FF = new TableCell();
                            Literal lit_det_FF = new Literal();
                            lit_det_FF.Text = "&nbsp" + dsCall.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            tc_det_FF.BorderStyle = BorderStyle.Solid;
                            tc_det_FF.BorderWidth = 1;
                            tc_det_FF.Controls.Add(lit_det_FF);
                            tc_det_FF.Style.Add("text-align", "left");
                            tc_det_FF.Style.Add("font-family", "Calibri");
                            tc_det_FF.Style.Add("font-size", "10pt");
                            tr_det1.Cells.Add(tc_det_FF);

                            cmonth6 = cmonth6 + 1;
                            if (cmonth6 == 13)
                            {
                                cmonth6 = 1;
                                cyear6 = cyear6 + 1;
                            }

                        }
                        TableCell tc_det_sf_Tot = new TableCell();
                        Literal lit_det_sf_Tot = new Literal();

                        if (itotWorkType == 0)
                        {
                            lit_det_sf_Tot.Text = "&nbsp;" + "-";
                        }
                        else
                        {
                            lit_det_sf_Tot.Text = "&nbsp;" + itotWorkType;
                        }
                        tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_Tot.BorderWidth = 1;
                        tc_det_sf_Tot.Style.Add("text-align", "left");
                        tc_det_sf_Tot.Style.Add("font-family", "Calibri");
                        tc_det_sf_Tot.Style.Add("font-size", "10pt");
                        //tc_det_sf_Tot.Width = 50;
                        tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
                        tr_det1.Cells.Add(tc_det_sf_Tot);
                    }
                    tbl.Rows.Add(tr_det1);
                }



                else if (drow["Doc_Cat_SName"].ToString() == "Call Average")
                {
                    int months7 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
                    int cmonth7 = Convert.ToInt32(FMonth);
                    int cyear7 = Convert.ToInt32(FYear);

                    ViewState["months"] = months7;
                    ViewState["cmonth"] = cmonth7;
                    ViewState["cyear"] = cyear7;

                    TableRow tr_det1 = new TableRow();
                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_FF1 = new Literal();
                    lit_det_FF1.Text = "&nbsp" + drow[0];
                    tc_det_FF1.BorderStyle = BorderStyle.Solid;
                    tc_det_FF1.BorderWidth = 1;
                    tc_det_FF1.Controls.Add(lit_det_FF1);
                    tc_det_FF1.Style.Add("text-align", "left");
                    tc_det_FF1.Style.Add("font-family", "Calibri");
                    tc_det_FF1.Style.Add("font-size", "10pt");
                    tr_det1.Cells.Add(tc_det_FF1);

                    double itotWorkType = 0.0;
                    double fldwrk_total = 0.0;

                    if (months7 >= 0)
                    {
                        for (int j = 1; j <= months7 + 1; j++)
                        {
                            decimal RoundLstCallAvg = new decimal();
                            if (Convert.ToDecimal(tot_fldwrkDays) != 0)

                                dblaverage = Convert.ToDouble((Convert.ToDecimal(tot_docSeen) / Convert.ToDecimal(tot_fldwrkDays)));
                            RoundLstCallAvg = Math.Round((decimal)dblaverage, 2);

                            itotWorkType += fldwrk_total + Convert.ToDouble(RoundLstCallAvg);
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            if (RoundLstCallAvg == 0)
                            {
                                lit_det_average.Text = "&nbsp;" + "-";
                            }
                            else
                            {

                                lit_det_average.Text = "&nbsp;" + RoundLstCallAvg;
                            }
                            tc_det_average.BorderStyle = BorderStyle.Solid;
                            tc_det_average.BorderWidth = 1;
                            tc_det_average.Style.Add("font-family", "Calibri");
                            tc_det_average.Style.Add("font-size", "10pt");
                            //  tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det1.Cells.Add(tc_det_average);

                            cmonth7 = cmonth7 + 1;
                            if (cmonth7 == 13)
                            {
                                cmonth7 = 1;
                                cyear7 = cyear7 + 1;
                            }
                        }
                        TableCell tc_det_sf_Tot = new TableCell();
                        Literal lit_det_sf_Tot = new Literal();

                        if (itotWorkType == 0)
                        {
                            lit_det_sf_Tot.Text = "&nbsp;" + "-";
                        }
                        else
                        {
                            lit_det_sf_Tot.Text = "&nbsp;" + itotWorkType;
                        }
                        tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_Tot.BorderWidth = 1;
                        tc_det_sf_Tot.Style.Add("text-align", "left");
                        tc_det_sf_Tot.Style.Add("font-family", "Calibri");
                        tc_det_sf_Tot.Style.Add("font-size", "10pt");
                        //tc_det_sf_Tot.Width = 50;
                        tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
                        tr_det1.Cells.Add(tc_det_sf_Tot);
                    }
                    tbl.Rows.Add(tr_det1);
                    //}
                }
                else
                {

                    int months8 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
                    int cmonth8 = Convert.ToInt32(FMonth);
                    int cyear8 = Convert.ToInt32(FYear);

                    ViewState["months"] = months8;
                    ViewState["cmonth"] = cmonth8;
                    ViewState["cyear"] = cyear8;

                    TableRow tr_det1 = new TableRow();
                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_FF1 = new Literal();
                    lit_det_FF1.Text = "&nbsp" + drow[0];
                    tc_det_FF1.BorderStyle = BorderStyle.Solid;
                    tc_det_FF1.BorderWidth = 1;
                    tc_det_FF1.Controls.Add(lit_det_FF1);
                    tc_det_FF1.Style.Add("text-align", "left");
                    tc_det_FF1.Style.Add("font-family", "Calibri");
                    tc_det_FF1.Style.Add("font-size", "10pt");
                    tc_det_FF1.Style.Add("color","Red");
                    tc_det_FF1.Style.Add("border-color", "Black");
                    tr_det.Cells.Add(tc_det_FF1);
                    string tot_dcr_dr = string.Empty;
                    DCR dcr1 = new DCR();

                    string tot_fldwrk = "";
                    int itotWorkType = 0;
                    int fldwrk_total = 0;
                    if (months8 >= 0)
                    {
                        for (int j = 1; j <= months8 + 1; j++)
                        {
                            DataSet dsDCR = new DataSet();

                            DCR dc = new DCR();
                            dsDCR = dc.Catg_Visit_Report1(sf_code, div_code, cmonth8, cyear8, Convert.ToInt32(drow["Doc_Cat_Code"].ToString()), 1);
                            //  if (dsDCR.Tables[0].Rows.Count > 0)
                            //  tot_dcr_dr = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            if (dsDCR.Tables[0].Rows.Count > 0)
                                tot_fldwrk = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);

                            tr_det1.BackColor = System.Drawing.Color.White;
                            TableCell tc_det_FF = new TableCell();
                            Literal lit_det_FF = new Literal();
                            lit_det_FF.Text = "&nbsp" + tot_fldwrk;
                            tc_det_FF.BorderStyle = BorderStyle.Solid;
                            tc_det_FF.BorderWidth = 1;
                            tc_det_FF.Controls.Add(lit_det_FF);
                            tc_det_FF.Style.Add("text-align", "left");
                            tc_det_FF.Style.Add("font-family", "Calibri");
                            tc_det_FF.Style.Add("font-size", "10pt");
                            tr_det.Cells.Add(tc_det_FF);

                            cmonth8 = cmonth8 + 1;
                            if (cmonth8 == 13)
                            {
                                cmonth8 = 1;
                                cyear8 = cyear8 + 1;
                            }

                        }
                        TableCell tc_det_sf_Tot = new TableCell();
                        Literal lit_det_sf_Tot = new Literal();

                        if (itotWorkType == 0)
                        {
                            lit_det_sf_Tot.Text = "&nbsp;" + "-";
                        }
                        else
                        {
                            lit_det_sf_Tot.Text = "&nbsp;" + itotWorkType;
                        }
                        tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_Tot.BorderWidth = 1;
                        tc_det_sf_Tot.Style.Add("text-align", "left");
                        tc_det_sf_Tot.Style.Add("font-family", "Calibri");
                        tc_det_sf_Tot.Style.Add("font-size", "10pt");
                        //tc_det_sf_Tot.Width = 50;
                        tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
                        tr_det.Cells.Add(tc_det_sf_Tot);
                    }
                    tbl.Rows.Add(tr_det);
                }

            }

        }
        DataSet dsDCR1 = new DataSet();
        DCR dc1 = new DCR();
        dsDCR1 = dc1.getHQ_Mgr(sf_code);
        if (dsDCR1.Tables[0].Rows.Count > 0)
        {

            TableRow tr_header_hq = new TableRow();
            tr_header_hq.BorderStyle = BorderStyle.Solid;
            tr_header_hq.BorderWidth = 1;
            tr_header_hq.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tr_header_hq.Attributes.Add("Class", "tblCellFont");


            TableCell tc_Hq_Name = new TableCell();
            tc_Hq_Name.BorderStyle = BorderStyle.Solid;
            tc_Hq_Name.BorderWidth = 1;
            tc_Hq_Name.RowSpan = 2;
            tc_Hq_Name.Width = 500;

            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>HQ Name</center>";
            tc_Hq_Name.Style.Add("font-weight", "bold");
            tc_Hq_Name.Attributes.Add("Class", "tr_det_head");
            tc_Hq_Name.Style.Add("Color", "White");
            tc_Hq_Name.Style.Add("border-color", "Black");
            tc_Hq_Name.Controls.Add(lit_DR_Name);
            tr_header_hq.Cells.Add(tc_Hq_Name);


            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);

            //    tbl.Rows.Add(tr_header);

            TableRow tr_catg1 = new TableRow();
            if (months >= 0)
            {

                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month_hq = new TableCell();
                    // tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                    tc_month_hq.ColumnSpan = 2;
                    Literal lit_month_hq = new Literal();
                    lit_month_hq.Text = "&nbsp;" + sf.getMonthName(cmonth.ToString()).Substring(0, 3) + "-" + cyear;
                    tc_month_hq.Style.Add("font-family", "Calibri");
                    tc_month_hq.Style.Add("font-size", "10pt");
                    tc_month_hq.Attributes.Add("Class", "tr_det_head");
                    tc_month_hq.Style.Add("Color", "White");
                    tc_month_hq.Style.Add("border-color", "Black");
                    tc_month_hq.BorderStyle = BorderStyle.Solid;
                    tc_month_hq.BorderWidth = 1;


                    tc_month_hq.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
                    tc_month_hq.Controls.Add(lit_month_hq);
                    tr_header_hq.Cells.Add(tc_month_hq);
                    // tr_catg1.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }
            tblhq.Rows.Add(tr_header_hq);
            TableRow tr_lst_det = new TableRow();
            if (months > 0)
            {
               
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_lst_month = new TableCell();
                    HyperLink lit_lst_month = new HyperLink();
                    lit_lst_month.Text = "No of Days Worked";
                    tc_lst_month.BorderStyle = BorderStyle.Solid;
                    //  tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month.BorderWidth = 1;

                    tc_lst_month.Style.Add("font-family", "Calibri");
                    tc_lst_month.Style.Add("font-size", "10pt");
                    tc_lst_month.Style.Add("Color", "White");
                    tc_lst_month.Style.Add("border-color", "Black");
                    tc_lst_month.Controls.Add(lit_lst_month);
                    tr_lst_det.Cells.Add(tc_lst_month);

                    TableCell tc_msd_month = new TableCell();
                    HyperLink lit_msd_month = new HyperLink();
                    lit_msd_month.Text = "Total Doctor Calls at HQ";
                    tc_msd_month.BorderStyle = BorderStyle.Solid;
                    tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                    //  tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_msd_month.BorderWidth = 1;
                    tc_msd_month.Style.Add("Color", "White");
                    tc_msd_month.Style.Add("border-color", "Black");
                    tc_msd_month.Style.Add("font-family", "Calibri");
                    tc_msd_month.Style.Add("font-size", "10pt");
                    tc_msd_month.Controls.Add(lit_msd_month);
                    tr_lst_det.Cells.Add(tc_msd_month);

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                    tblhq.Rows.Add(tr_lst_det);
                }




            }
         //   TableRow tr_lst_det = new TableRow();
            TableCell tc_DR_Total = new TableCell();
            tc_DR_Total.BorderStyle = BorderStyle.Solid;
            tc_DR_Total.BorderWidth = 1;
            tc_DR_Total.ColumnSpan = 2;
            tc_DR_Total.Style.Add("Color", "White");
            tc_DR_Total.Style.Add("border-color", "Black");
            tc_DR_Total.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            //tc_DR_Total.Width = 50;
            Literal lit_DR_Total = new Literal();
            lit_DR_Total.Text = "<center>Total</center>";
            tc_DR_Total.Attributes.Add("Class", "tr_det_head");
            tc_DR_Total.Controls.Add(lit_DR_Total);
            //  tr_lst_det.Cells.Add(tc_DR_Total);


            tr_header_hq.Cells.Add(tc_DR_Total);

            //  tblhq.Rows.Add(tr_header_hq);
           // TableRow tr_lstWorkdr = new TableRow();
            TableCell tc_lstWork = new TableCell();

            HyperLink lit_lstWork = new HyperLink();
            lit_lstWork.Text = "No of Days Worked";
            tc_lstWork.BorderStyle = BorderStyle.Solid;
            //  tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_lstWork.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_lstWork.HorizontalAlign = HorizontalAlign.Center;
            tc_lstWork.BorderWidth = 1;
            tc_lstWork.Style.Add("Color", "White");
            tc_lstWork.Style.Add("border-color", "Black");
            tc_lstWork.Style.Add("font-family", "Calibri");
            tc_lstWork.Style.Add("font-size", "10pt");
            tc_lstWork.Controls.Add(lit_lstWork);
            tr_lst_det.Cells.Add(tc_lstWork);

            TableCell tc_lstWork1 = new TableCell();
            HyperLink lit_Work1 = new HyperLink();
            lit_Work1.Text = "Total Doctor Calls at HQ";
            tc_lstWork1.BorderStyle = BorderStyle.Solid;
            tc_lstWork1.HorizontalAlign = HorizontalAlign.Center;
            //  tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_lstWork1.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_lstWork1.BorderWidth = 1;
            tc_lstWork1.Style.Add("Color", "White");
            tc_lstWork1.Style.Add("border-color", "Black");
            tc_lstWork1.Style.Add("font-family", "Calibri");
            tc_lstWork1.Style.Add("font-size", "10pt");
            tc_lstWork1.Controls.Add(lit_Work1);
            tr_lst_det.Cells.Add(tc_lstWork1);

            tblhq.Rows.Add(tr_catg1);

            foreach (DataRow drFF1 in dsDCR1.Tables[0].Rows)
            {

                TableRow tr_det_hq = new TableRow();

                //tc_det_SNo.Height = 10;

                //tr_det.Height = 10;
                tr_det_hq.BackColor = System.Drawing.Color.White;
                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp" + drFF1["sf_Name"];
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Controls.Add(lit_det_FF);
                tc_det_FF.Style.Add("text-align", "left");
                tc_det_FF.Style.Add("font-family", "Calibri");

                tc_det_FF.Style.Add("font-size", "10pt");
                tr_det_hq.Cells.Add(tc_det_FF);

                tblhq.Rows.Add(tr_det_hq);

             //   int itotWorkType = 0;
             //   
                string tot_fldwrk = "";
                int months5 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth5 = Convert.ToInt32(FMonth);
                int cyear5 = Convert.ToInt32(FYear);
              //  string tot_fldwrk = "";
                int itotWorkType1 = 0;
                int fldwrk_total = 0;
                int fldwrk_totalDay = 0;
                int itotWorkTypeDay = 0;
                if (months5 >= 0)
                {
                    for (int j = 1; j <= months5 + 1; j++)
                    {
                        DataSet ds = new DataSet();
                        DCR dcr1 = new DCR();
                        TableRow tr_det1 = new TableRow();
                        ds = dcr1.getDaysWorked(sf_code, cmonth5, cyear5, drFF1["sf_code"].ToString());
                        if (ds.Tables[0].Rows.Count > 0)
                            tot_fldwrk = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        itotWorkType1 += fldwrk_totalDay + Convert.ToInt16(tot_fldwrk);

                        tr_det1.BackColor = System.Drawing.Color.White;
                        TableCell tc_det_work = new TableCell();
                        Literal lit_det_work = new Literal();
                        lit_det_work.Text = "&nbsp" + ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        tc_det_work.BorderStyle = BorderStyle.Solid;
                        tc_det_work.BorderWidth = 1;
                        tc_det_work.Controls.Add(lit_det_work);
                        tc_det_work.Style.Add("text-align", "left");
                        tc_det_work.Style.Add("font-family", "Calibri");
                        tc_det_work.Style.Add("font-size", "10pt");
                        tr_det_hq.Cells.Add(tc_det_work);

                        TableCell tc_doc_call = new TableCell();
                        Literal lit_doc_call = new Literal();
                        DataSet dsCalls = new DataSet();
                        int fldwrk_totalDay1 = 0;
                     
                        DCR dcr2 = new DCR();
                       // dsCalls = dcr2.getHQCalls(drFF1["sf_code"].ToString(), cmonth5, cyear5, sf_code);
                        //if (dsCalls.Tables[0].Rows.Count > 0)
                        int docCalls = 0;
                      //  foreach (DataRow drcalls in dsCalls.Tables[0].Rows)
                      //  {
                            
                            DataSet dsCallsdoc = new DataSet();
                            DCR dcrnew = new DCR();
                          //  dsCallsdoc = dcrnew.getHQCalls_Doc(drFF1["sf_code"].ToString(), drcalls["dcrdate"].ToString(), cmonth5, cyear5);
                            dsCallsdoc = dcrnew.getHQCalls_Doc(drFF1["sf_code"].ToString(),cmonth5, cyear5, sf_code);
                            docCalls =  Convert.ToInt16(dsCallsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                           fldwrk_total = docCalls;
                         
                          //  itotWorkTypeDay += fldwrk_totalDay1 + fldwrk_total;
                       // }
                        itotWorkTypeDay += fldwrk_totalDay1 + fldwrk_total;
                        lit_doc_call.Text = "&nbsp" + fldwrk_total;
                        tc_doc_call.BorderStyle = BorderStyle.Solid;
                        tc_doc_call.HorizontalAlign = HorizontalAlign.Left;
                        //  tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                       
                        tc_doc_call.BorderWidth = 1;
                        tc_doc_call.Style.Add("font-family", "Calibri");
                        tc_doc_call.Style.Add("font-size", "10pt");
                        tc_doc_call.Controls.Add(lit_doc_call);
                        tr_det_hq.Cells.Add(tc_doc_call);

                        cmonth5 = cmonth5 + 1;
                        if (cmonth5 == 13)
                        {
                            cmonth5 = 1;
                            cyear5 = cyear5 + 1;
                        }
                    }

                    TableCell tc_det_sf_Tot = new TableCell();
                    Literal lit_det_sf_Tot = new Literal();

                    if (itotWorkType1 == 0)
                    {
                        lit_det_sf_Tot.Text = "&nbsp;" + "-";
                    }
                    else
                    {
                        lit_det_sf_Tot.Text = "&nbsp;" + itotWorkType1;
                    }
                    tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
                    tc_det_sf_Tot.BorderWidth = 1;
                    tc_det_sf_Tot.Style.Add("text-align", "left");
                    tc_det_sf_Tot.Style.Add("font-family", "Calibri");
                    tc_det_sf_Tot.Style.Add("font-size", "10pt");
                    //tc_det_sf_Tot.Width = 50;
                    tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
                    tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
                    tr_det_hq.Cells.Add(tc_det_sf_Tot);

                    TableCell tc_det_sf_Tot1 = new TableCell();
                    Literal lit_det_sf_Tot1 = new Literal();

                    if (itotWorkType1 == 0)
                    {
                        lit_det_sf_Tot1.Text = "&nbsp;" + "-";
                    }
                    else
                    {
                        lit_det_sf_Tot1.Text = "&nbsp;" + itotWorkTypeDay;
                    }
                    tc_det_sf_Tot1.BorderStyle = BorderStyle.Solid;
                    tc_det_sf_Tot1.BorderWidth = 1;
                    tc_det_sf_Tot1.Style.Add("text-align", "left");
                    tc_det_sf_Tot1.Style.Add("font-family", "Calibri");
                    tc_det_sf_Tot1.Style.Add("font-size", "10pt");
                    //tc_det_sf_Tot.Width = 50;
                    tc_det_sf_Tot1.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_sf_Tot1.VerticalAlign = VerticalAlign.Middle;
                    tc_det_sf_Tot1.Controls.Add(lit_det_sf_Tot1);
                    tr_det_hq.Cells.Add(tc_det_sf_Tot1);

                    tblhq.Rows.Add(tr_det_hq);



                }
             
            }
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