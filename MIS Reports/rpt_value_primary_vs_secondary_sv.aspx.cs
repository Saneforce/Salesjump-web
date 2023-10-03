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
using iTextSharp.tool.xml;

public partial class MIS_Reports_rpt_value_primary_vs_secondary_sv : System.Web.UI.Page
{
    DataSet dsProduct = null;
    DataSet dsDr = null;
    DataSet dsDrr = null;
    DataSet getweek = null;
    string sCmd = string.Empty;
    string div_code = string.Empty;
    string subdivision = string.Empty;
    string feild_force_name = string.Empty;
    string stockist = string.Empty;
    string Year = string.Empty;
    string Month = string.Empty;
    string Stock_name = string.Empty;
    string sf_name = string.Empty;
    string type = string.Empty;
    string sCurrentDate = string.Empty;
    string stype = string.Empty;
    string Product_Code = string.Empty;
    string tot_dr = string.Empty;
    string strFMonthName = string.Empty;
    int iCount = -1;
    DataSet dsDoc = null;
    decimal iTotLstCount = 0;
    int FYear = 0;
    int FMonth = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        Month = Request.QueryString["Month"].ToString();
        Year = Request.QueryString["Year"].ToString();
        Product_Code = Request.QueryString["Product_Code"].ToString();
        subdivision = Request.QueryString["subdivision"].ToString();
        stype = Request.QueryString["stype"].ToString();
        Stock_name = Request.QueryString["Stockist_name"].ToString();
        stockist = Request.QueryString["Stockist_Code"].ToString();
       feild_force_name = Request.QueryString["fldforcename"].ToString();
        FMonth = Convert.ToInt32(Month);
        FYear = Convert.ToInt32(Year);
        
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString();


        lblHead.Text = "Primary Vs Secondary for the Month of " + strFMonthName + " " + FYear;
        
        dist.Text = Stock_name;
      lblfield.Text=feild_force_name;
        FillSF_Leave_Type();


    }
    private void FillSF_Leave_Type()
    {
        TableRow tr_det_head = new TableRow();
        tr_det_head.BorderStyle = BorderStyle.Solid;
        tr_det_head.BorderWidth = 1;
        tr_det_head.BackColor = System.Drawing.Color.FromName("#0097AC");
        tr_det_head.Style.Add("Color", "White");
        tr_det_head.BorderColor = System.Drawing.Color.Black;

        SalesForce sf = new SalesForce();


        getweek = sf.Secondary_sales_get_no_of_week(FYear, strFMonthName);

        TableCell leave_utility = new TableCell();
        leave_utility.BorderStyle = BorderStyle.Solid;
        leave_utility.BorderWidth = 1;
        leave_utility.Width = 80;
        //leave_utility.RowSpan = 4;

        //leave_utility.ColumnSpan = getweek.Tables[0].Rows.Count;
        leave_utility.ColumnSpan = 3;
        Literal lit_DR_utility = new Literal();
        //lit_DR_utility.Text = dataRow["Leave_SName"].ToString();
        lit_DR_utility.Text = "Weekly View";
leave_utility.HorizontalAlign = HorizontalAlign.Center;
        leave_utility.BorderColor = System.Drawing.Color.Black;
        leave_utility.Attributes.Add("Class", "rptCellBorder");
        leave_utility.Controls.Add(lit_DR_utility);
        tr_det_head.Cells.Add(leave_utility);

        int df = Convert.ToInt32(getweek.Tables[0].Rows[0].ItemArray.GetValue(0));
        //  tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");
        int wee = df + 1;

        if (getweek.Tables[0].Rows.Count > 0)
        {
            for (int i = 1; i < wee; i++)
            {
                TableCell tc_catg_namee = new TableCell();
                tc_catg_namee.BorderStyle = BorderStyle.Solid;
                tc_catg_namee.BorderWidth = 1;
                //text-align: center;

                tc_catg_namee.ColumnSpan = 1;
                Literal lit_catg_namee = new Literal();
                lit_catg_namee.Text = i.ToString() + " Week";

                tc_catg_namee.Attributes.Add("Class", "rptCellBorder");
                tc_catg_namee.HorizontalAlign = HorizontalAlign.Center;
                tc_catg_namee.Controls.Add(lit_catg_namee);
                tr_det_head.Cells.Add(tc_catg_namee);
            }

            //tbl.Rows.Add(tr_catg);
        }
        tbl.Rows.Add(tr_det_head);






        SalesForce dr = new SalesForce();
        dsDr = dr.Secondary_sales_productdetail_singleproduct(div_code, subdivision,Product_Code);


        if (dsDr.Tables[0].Rows.Count > 0)
        {
            TableRow tr_catg = new TableRow();
            tr_catg.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_catg.Style.Add("Color", "White");



            TableCell tc_det_head_SNo = new TableCell();
            tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_head_SNo.BorderWidth = 1;
            Literal lit_det_head_SNo = new Literal();
            lit_det_head_SNo.Text = "<b>S.No</b>";
            tc_det_head_SNo.Attributes.Add("Class", "tblHead");
            tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
            tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
            tr_catg.Cells.Add(tc_det_head_SNo);


            TableCell tc_det_head_doc = new TableCell();
            tc_det_head_doc.Width = 120;
            tc_det_head_doc.BorderStyle = BorderStyle.Solid;
            tc_det_head_doc.BorderWidth = 1;
            Literal lit_det_head_doc = new Literal();
            lit_det_head_doc.Text = "<b>Product Name</b>";
            tc_det_head_doc.Attributes.Add("Class", "tblHead");
            tc_det_head_doc.Controls.Add(lit_det_head_doc);
            tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
            tr_catg.Cells.Add(tc_det_head_doc);

            TableCell tc_det_head_hq = new TableCell();
            tc_det_head_hq.Width = 70;
            tc_det_head_hq.BorderStyle = BorderStyle.Solid;
            tc_det_head_hq.BorderWidth = 1;
            Literal lit_det_head_hq = new Literal();
            lit_det_head_hq.Text = "<b>Product_Sale_Unit</b>";
            tc_det_head_hq.Attributes.Add("Class", "tblHead");
            tc_det_head_hq.Controls.Add(lit_det_head_hq);
            tc_det_head_hq.HorizontalAlign = HorizontalAlign.Center;
            tr_catg.Cells.Add(tc_det_head_hq);
            if (getweek.Tables[0].Rows.Count > 0)
            {

                for (int i = 1; i < wee; i++)
                {


                    TableCell tc_det_head_Qua = new TableCell();
                    tc_det_head_Qua.Width = 70;
                    tc_det_head_Qua.BorderStyle = BorderStyle.Solid;
                    tc_det_head_Qua.BorderWidth = 1;
                    Literal lit_det_head_qua = new Literal();
                    lit_det_head_qua.Text = "<b>Value (Rs)</b>";
                    tc_det_head_Qua.Attributes.Add("Class", "tblHead");
                    tc_det_head_Qua.Controls.Add(lit_det_head_qua);
                    tc_det_head_Qua.HorizontalAlign = HorizontalAlign.Center;
                    tr_catg.Cells.Add(tc_det_head_Qua);






                    tbl.Rows.Add(tr_catg);



                }
            }



            iCount = 0;
            foreach (DataRow drdoctor in dsDr.Tables[0].Rows)
            {
                TableRow tr_det_sno = new TableRow();
                TableCell tc_det_SNo = new TableCell();
                iCount += 1;
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString() ;
                tc_det_SNo.Attributes.Add("Class", "tblRow");
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
tc_det_SNo.HorizontalAlign = HorizontalAlign.Center;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det_sno.Cells.Add(tc_det_SNo);

                TableCell tc_det_Sl_No = new TableCell();
                tc_det_Sl_No.Visible = false;
                Literal lit_det_Sl_No = new Literal();
                lit_det_Sl_No.Text = "&nbsp;" + drdoctor["Product_Detail_Code"].ToString();
                tc_det_Sl_No.BorderStyle = BorderStyle.Solid;
                tc_det_Sl_No.BorderWidth = 1;
                tc_det_Sl_No.Attributes.Add("Class", "rptCellBorder");
                tc_det_Sl_No.Controls.Add(lit_det_Sl_No);
                tr_det_sno.Cells.Add(tc_det_Sl_No);

                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drdoctor["Product_Detail_Name"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;

                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det_sno.Cells.Add(tc_det_usr);

                TableCell tc_det_sfcode = new TableCell();

                Literal lit_det_sfcode = new Literal();
                lit_det_sfcode.Text = "&nbsp;" + drdoctor["Product_Sale_Unit"].ToString();
                tc_det_sfcode.BorderStyle = BorderStyle.Solid;
                tc_det_sfcode.BorderWidth = 1;
                tc_det_sfcode.Attributes.Add("Class", "rptCellBorder");
                tc_det_sfcode.Controls.Add(lit_det_sfcode);
                tr_det_sno.Cells.Add(tc_det_sfcode);
                //string week = "4";

                if (getweek.Tables[0].Rows.Count > 0)
                {

                    for (int i = 1; i < wee; i++)
                    {
                        SalesForce sd = new SalesForce();
                        if (stype == "1")
                        {

                            if (Product_Code == drdoctor["Product_Detail_Code"].ToString())
                            {
                                dsDrr = sd.primary_Purchase_Distributor_value_single_prt(stockist, div_code, FMonth, FYear, i, Product_Code);
                                if (dsDrr.Tables[0].Rows.Count > 0)
                                {
                                    tot_dr = dsDrr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                    TableCell tc_det_dr_name = new TableCell();
                                    Literal lit_det_dr_name = new Literal();
tc_det_dr_name.HorizontalAlign = HorizontalAlign.Right;
                                    lit_det_dr_name.Text =tot_dr=="" ?  tot_dr.ToString() : Convert.ToDecimal( tot_dr).ToString("0.00");
                                    tc_det_dr_name.Attributes.Add("Class", "tblRow");
                                    tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                                    tc_det_dr_name.BorderWidth = 1;
                                    tc_det_dr_name.Controls.Add(lit_det_dr_name);
                                    tr_det_sno.Cells.Add(tc_det_dr_name);
                                }
                            }
                            else
                            {
                                TableCell tc_det_dr_name = new TableCell();
                                Literal lit_det_dr_name = new Literal();
                                lit_det_dr_name.Text = "";
                                tc_det_dr_name.Attributes.Add("Class", "tblRow");
                                tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                                tc_det_dr_name.BorderWidth = 1;
                                tc_det_dr_name.Controls.Add(lit_det_dr_name);
                                tr_det_sno.Cells.Add(tc_det_dr_name);
                            }

                                                  

                        }
                        if (stype == "2")
                        {
                            if (Product_Code == drdoctor["Product_Detail_Code"].ToString())
                            {
                                dsDrr = sd.secondary_Purchase_Distributor_value_single_prt(stockist, div_code, FMonth, FYear, i, Product_Code);
                                if (dsDrr.Tables[0].Rows.Count > 0)
                                {
                                    tot_dr = dsDrr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                    TableCell tc_det_dr_name = new TableCell();
                                    Literal lit_det_dr_name = new Literal();

                                    lit_det_dr_name.Text = tot_dr =="" ? tot_dr.ToString() : Convert.ToDecimal(tot_dr).ToString("0.00") ;
                                    tc_det_dr_name.Attributes.Add("Class", "tblRow");
                                    tc_det_dr_name.BorderStyle = BorderStyle.Solid;
tc_det_dr_name.HorizontalAlign = HorizontalAlign.Right;
                                    tc_det_dr_name.BorderWidth = 1;
                                    tc_det_dr_name.Controls.Add(lit_det_dr_name);
                                    tr_det_sno.Cells.Add(tc_det_dr_name);
                                }
                            }
                            else
                            {
                                TableCell tc_det_dr_name = new TableCell();
                                Literal lit_det_dr_name = new Literal();
                                lit_det_dr_name.Text = "";
                                tc_det_dr_name.Attributes.Add("Class", "tblRow");
                                tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                                tc_det_dr_name.BorderWidth = 1;
                                tc_det_dr_name.Controls.Add(lit_det_dr_name);
                                tr_det_sno.Cells.Add(tc_det_dr_name);
                            }
                        }

                    }
                }



                tbl.Rows.Add(tr_det_sno);
            }

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
        string strFileName = Page.Title;
        string attachment = "attachment; filename='" + strFileName + "'.xls";
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
    protected void btnExport_Click(object sender, EventArgs e)
    {

        string strFileName = Page.Title;

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                //this.Page.RenderControl(hw);
                this.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename= '" + strFileName + "'.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
            }

        }
    }



}