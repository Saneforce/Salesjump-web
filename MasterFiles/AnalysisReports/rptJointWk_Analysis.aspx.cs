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


public partial class MasterFiles_AnalysisReports_rptJointWk_Analysis : System.Web.UI.Page
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
    string str1 = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataSet dsSal = new DataSet();
    string strFieledForceName = string.Empty;
    string SfNameM = string.Empty;
    int totcount = 0;
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
        lblHead.Text = "Joint Work Analysis From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        CreateDynamicTable();
    }
    private void CreateDynamicTable()
    {
        int iCount = 0;

        SalesForce sf = new SalesForce();
        
        DataSet dssfName = new DataSet();
        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 30;
            tc_SNo.ForeColor = System.Drawing.Color.White;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.Controls.Add(lit_SNo);

            tc_SNo.Style.Add("font-family", "Calibri");
            tc_SNo.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_SNo);

            tr_header.Attributes.Add("Class", "tblCellFont");
            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>FieldForce Name/HQ/Designation</center>";
            tc_DR_Name.Style.Add("font-family", "Calibri");
            tc_DR_Name.Style.Add("font-size", "10pt");
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
                    lit_month.Text = "&nbsp;" + sf.getMonthName(cmonth.ToString()).Substring(0, 3) + "-" + cyear + "<br/>" + "JFW DAYS/DATE/Calls";

                    tc_month.Style.Add("font-family", "Calibri");
                    tc_month.Style.Add("font-size", "10pt");
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
            lit_DR_Total.Text = "<center>BaseLevel/HQ/Designation</center>";
            tc_DR_Total.Attributes.Add("Class", "tr_det_head");
            tc_DR_Total.Controls.Add(lit_DR_Total);
            //  tr_lst_det.Cells.Add(tc_DR_Total);

            tr_header.Cells.Add(tc_DR_Total);

            //  int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;

            //    tbl.Rows.Add(tr_header);


            if (months >= 0)
            {

                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    // tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                    //tc_month.RowSpan = 2;
                    Literal lit_month = new Literal();
                    lit_month.Text = "&nbsp;" + sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + "-" + cyear1;

                    tc_month.Style.Add("font-family", "Calibri");
                    tc_month.Style.Add("font-size", "10pt");
                    tc_month.Attributes.Add("Class", "tr_det_head");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;

                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    // tr_catg1.Cells.Add(tc_month);
                    cmonth1 = cmonth1 + 1;
                    if (cmonth1 == 13)
                    {
                        cmonth1 = 1;
                        cyear1 = cyear1 + 1;
                    }
                }
            }

            tbl.Rows.Add(tr_header);

            string sURL = string.Empty;

            string sTab = string.Empty;

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                int cmonth2 = Convert.ToInt32(FMonth);
                int cyear2 = Convert.ToInt32(FYear);
                DataSet dsWorkwith = new DataSet();
                DCR dcrNew1 = new DCR();
                int cmonthwork = Convert.ToInt32(FMonth);
                int cyearwork = Convert.ToInt32(FYear);

                ViewState["cmonth"] = cmonthwork;
                ViewState["cyear"] = cyearwork;

                //    tbl.Rows.Add(tr_header);
                string concat1 = string.Empty;
                string strwordsfname = string.Empty;
                string strwork1 = string.Empty;
                int max = 0;
                totcount = 0;
                if (months >= 0)
                {

                    for (int z = 1; z <= months + 1; z++)
                    {
                      
                        dsWorkwith = dcrNew1.DCR_workwithDay_dist(div_code, cmonthwork, cyearwork, drFF["sf_code"].ToString());
                    
                           // for (int l = 0; l < dsWorkwith.Tables[0].Rows.Count; l++)
                           // {
                           //     strwordsfname += dsWorkwith.Tables[0].Rows[l]["Worked_with_Code"].ToString() + ',';
                           // }
                        
                           //string[] strworkwith = strwordsfname.Split(',');
                           // max = Convert.ToInt16(strworkwith.Max());
                      
                        if (dsWorkwith.Tables[0].Rows.Count > 0)
                        {
                            // strwork = "";
                           
                            for (int k = 0; k < dsWorkwith.Tables[0].Rows.Count; k++)
                            {
                                strwork1 = dsWorkwith.Tables[0].Rows[k]["Worked_with_Code"].ToString();

                                string[] strworkwith = strwork1.Split('$');


                                SalesForce sal = new SalesForce();

                                string sf_nameMr = string.Empty;

                                string sf_name = string.Empty;
                                string sf_codeWith = string.Empty;
                                
                                foreach (string str1 in strworkwith)
                                {
                                    sf_nameMr += str1;
                                    string st = string.Empty;
                                    //string[] arr =  strworkwith ;
                                    //arr = arr.Where(s => s != str).ToArray();
                                    if (str1 != drFF["sf_code"].ToString() && str1 != "" && str1.Contains("MR"))
                                    {
                                        //Literal lit = new Literal();
                                        //lit.Text = str;

                                        if (concat1.Contains(str1))
                                        {
                                        }
                                        else
                                        {
                                            concat1 += str1; //+ ",";
                                            dsSal = sal.getSfName_Mr(str1);
                                          
                                            totcount += dsSal.Tables[0].Rows.Count;
                                        }
                                    }
                                }
                            }

                           
                        
                        }
                        cmonthwork = cmonthwork + 1;
                        if (cmonthwork == 13)
                        {
                            cmonthwork = 1;
                            cyearwork = cyearwork + 1;
                        }


                    }
                }

            
                       
                ViewState["cmonth"] = cmonth2;
                ViewState["cyear"] = cyear2;
                //if (drFF["sf_type"].ToString() == "2")
                //{
                //    tr_det.Attributes.Add("style", "background-color:LightBlue; font-weight:Bold; ");
                //}
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                if (totcount != 0)
                {
                    tc_det_SNo.RowSpan = totcount + 1;
                }
                else
                {
                    tc_det_SNo.RowSpan = 0;
                }
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);


                TableCell tc_det_user = new TableCell();
                Literal lit_det_user = new Literal();
                lit_det_user.Text = "&nbsp;" + sTab + drFF["sf_name"].ToString();
                tc_det_user.HorizontalAlign = HorizontalAlign.Left;
                tc_det_user.BorderStyle = BorderStyle.Solid;
                tc_det_user.BorderWidth = 1;
                if (totcount != 0)
                {
                    tc_det_user.RowSpan = totcount + 1;
                }
                else
                {
                    tc_det_user.RowSpan = 0;
                }
                
                tc_det_user.Controls.Add(lit_det_user);
                tr_det.Cells.Add(tc_det_user);

                if (months >= 0)
                {
                    for (int j = 1; j <= months + 1; j++)
                    {
                        DataSet ds = new DataSet();
                        DataSet dsDCR = new DataSet();
                        DCR dcr1 = new DCR();
                        string tot_fldwrk = "";
                        string totCalls = string.Empty;
                        string sActive = string.Empty;
                        ds = dcr1.DCR_workwithDay_JW(div_code, cmonth2, cyear2, drFF["sf_code"].ToString());

                        if (ds.Tables[0].Rows.Count > 0)
                            tot_fldwrk = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                        dsDCR = dcr1.DCR_workwithDate_JW(div_code, cmonth2, cyear2, drFF["sf_code"].ToString());

                        foreach (DataRow drSF in dsDCR.Tables[0].Rows)
                        {
                            sActive = sActive + drSF["Activity_Date"].ToString() + " , ";
                        }

                        if (sActive.Length > 0)
                            sActive = sActive.Substring(0, sActive.Length - 2);
                        //   itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);
                        //  string leave = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        DataSet dswork = new DataSet();
                        dswork = dcr1.DCR_workwithCalls_JW(div_code, cmonth2, cyear2, drFF["sf_code"].ToString());

                        if (dswork.Tables[0].Rows.Count > 0)
                            totCalls = dswork.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        tr_det.BackColor = System.Drawing.Color.White;
                        TableCell tc_det_FF = new TableCell();
                        Literal lit_det_FF = new Literal();

                        if (tot_fldwrk == "0")
                        {
                            lit_det_FF.Text = "&nbsp;" + " - ";
                        }
                        else
                        {
                            lit_det_FF.Text = "&nbsp" + tot_fldwrk + "<br/>" + "<span style='color:Red'>  " + sActive + "</span> " + "<br/>" + "(" + totCalls + ")";
                        }
                        tc_det_FF.BorderStyle = BorderStyle.Solid;
                        tc_det_FF.BorderWidth = 1;
                        if (totcount != 0)
                        {
                            tc_det_FF.RowSpan = totcount + 1;
                        }
                        else
                        {
                            tc_det_FF.RowSpan = 0;
                        }
 
                        tc_det_FF.Controls.Add(lit_det_FF);
                        tc_det_FF.Style.Add("text-align", "left");
                        tc_det_FF.Style.Add("font-family", "Calibri");
                        tc_det_FF.Style.Add("font-size", "10pt");
                        tr_det.Cells.Add(tc_det_FF);

                        cmonth2 = cmonth2 + 1;
                        if (cmonth2 == 13)
                        {
                            cmonth2 = 1;
                            cyear2 = cyear2 + 1;
                        }
                    }


                }
                tbl.Rows.Add(tr_det);



                DCR dcrNew = new DCR();
                int cmonth3 = Convert.ToInt32(FMonth);
                int cyear3 = Convert.ToInt32(FYear);

                ViewState["cmonth"] = cmonth3;
                ViewState["cyear"] = cyear3;
                string strwork = string.Empty;
                string concat = string.Empty;
                SfNameM = "";
                if (months >= 0)
                {
                    int h = 1;
                    for (h = 1; h <= months + 1; h++)
                    {
                        
                        dssfName = dcrNew.DCR_workwithCalls_SfName(div_code, cmonth3, cyear3, drFF["sf_code"].ToString());
                        for (int l = 0; l < dssfName.Tables[0].Rows.Count; l++)
                        {
                            SfNameM += dssfName.Tables[0].Rows[l]["Worked_with_Code"].ToString() + '$';
                        }
                        cmonth3 = cmonth3 + 1;
                        if (cmonth3 == 13)
                        {
                            cmonth3 = 1;
                            cyear3 = cyear3 + 1;
                        }


                    }



                }

                if(SfNameM != "")
                {
                //if (dssfName.Tables[0].Rows.Count > 0)
                //{
                //    // strwork = "";
                //    for (int k = 0; k < dssfName.Tables[0].Rows.Count; k++)
                //    {
                //        strwork = dssfName.Tables[0].Rows[k]["Worked_with_Code"].ToString();

                        string[] strworkwith = SfNameM.Split('$');


                        SalesForce sal = new SalesForce();

                        string sf_nameMr = string.Empty;

                        string sf_name = string.Empty;
                        string sf_codeWith = string.Empty;
                        foreach (string str in strworkwith)
                        {
                            sf_nameMr += str;
                            string st = string.Empty;
                            //string[] arr =  strworkwith ;
                            //arr = arr.Where(s => s != str).ToArray();
                            if (str != drFF["sf_code"].ToString() && str != "" && str.Contains("MR"))
                            {
                                //Literal lit = new Literal();
                                //lit.Text = str;

                                if (concat.Contains(str))
                                {
                                }
                                else
                                {
                                    concat += str + ",";
                                    dsSal = sal.getSfName_Mr(str);
                                    if (dsSal.Tables[0].Rows.Count > 0)
                                    {
                                        sf_name = dsSal.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                    sf_codeWith = dsSal.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                        }
                                    TableRow tr_det_sf = new TableRow();

                                    TableCell tc_det_sf = new TableCell();
                                    Literal lit_det_sf = new Literal();
                                    //lit_det_sf.Text = "&nbsp" + dr["Sf_Name"].ToString();
                                   
                                    lit_det_sf.Text = "&nbsp" + sf_name;
                                    

                                    tc_det_sf.BorderStyle = BorderStyle.Solid;
                                    tc_det_sf.BorderWidth = 1;
                                    tc_det_sf.Controls.Add(lit_det_sf);
                                    tc_det_sf.Style.Add("text-align", "left");
                                    tc_det_sf.Style.Add("font-family", "Calibri");
                                    tc_det_sf.Style.Add("font-size", "10pt");
                                    tr_det_sf.Cells.Add(tc_det_sf);

                                    int cmonth4 = Convert.ToInt32(FMonth);
                                    int cyear4 = Convert.ToInt32(FYear);

                                    ViewState["cmonth"] = cmonth4;
                                    ViewState["cyear"] = cyear4;
                                    if (months >= 0)
                                    {
                                        for (int j = 1; j <= months + 1; j++)
                                        {
                                            DataSet ds = new DataSet();
                                            DataSet dsDCR = new DataSet();
                                            DCR dcr1 = new DCR();
                                            string tot_fldwrk_MR = "";
                                            string totCalls_MR = string.Empty;
                                            string sActive_MR = string.Empty;
                                            ds = dcr1.DCR_workwithDay_JW_MR(div_code, cmonth4, cyear4, sf_codeWith, drFF["sf_code"].ToString());

                                            if (ds.Tables[0].Rows.Count > 0)
                                                tot_fldwrk_MR = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                                            dsDCR = dcr1.DCR_workwithDate_JW_MR(div_code, cmonth4, cyear4, sf_codeWith, drFF["sf_code"].ToString());

                                            foreach (DataRow drSF in dsDCR.Tables[0].Rows)
                                            {
                                                sActive_MR = sActive_MR + drSF["Activity_Date"].ToString() + " , ";
                                            }

                                            if (sActive_MR.Length > 0)
                                                sActive_MR = sActive_MR.Substring(0, sActive_MR.Length - 2);
                                            //   itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);
                                            //  string leave = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                            DataSet dswork = new DataSet();
                                            dswork = dcr1.DCR_workwithCalls_JW_MR(div_code, cmonth4, cyear4, sf_codeWith, drFF["sf_code"].ToString());

                                            if (dswork.Tables[0].Rows.Count > 0)
                                                totCalls_MR = dswork.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                            tr_det.BackColor = System.Drawing.Color.White;
                                            TableCell tc_det_date = new TableCell();
                                            Literal lit_det_date = new Literal();

                                            if (tot_fldwrk_MR == "0")
                                            {
                                                lit_det_date.Text = "&nbsp;" + " - ";
                                            }
                                            else
                                            {
                                                lit_det_date.Text = "&nbsp" + tot_fldwrk_MR + "<br/>" + "<span style='color:Red'>  " + sActive_MR + "</span> " + "<br/>" + "(" + totCalls_MR + ")";
                                            }
                                            tc_det_date.BorderStyle = BorderStyle.Solid;
                                            tc_det_date.BorderWidth = 1;

                                            tc_det_date.Controls.Add(lit_det_date);
                                            tc_det_date.Style.Add("text-align", "left");
                                            tc_det_date.Style.Add("font-family", "Calibri");
                                            tc_det_date.Style.Add("font-size", "10pt");
                                            tr_det_sf.Cells.Add(tc_det_date);

                                            cmonth4 = cmonth4 + 1;
                                            if (cmonth4 == 13)
                                            {
                                                cmonth4 = 1;
                                                cyear4 = cyear4 + 1;
                                            }
                                        }


                                    }

                                    tbl.Rows.Add(tr_det_sf);
                                }

                            }
                        }



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