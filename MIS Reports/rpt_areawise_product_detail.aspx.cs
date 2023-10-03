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
using iTextSharp.tool.xml;
using Bus_EReport;
using System.Net;


public partial class MIS_Reports_rpt_areawise_product_detail : System.Web.UI.Page
{
	Decimal iTotLstCount1 = 0;
    Decimal iTotLstCount = 0;
    DataSet dsDr = null;
    DataSet dsDrr = null;
    DataSet getweek = null;
    string sCmd = string.Empty;
    string div_code = string.Empty;
string sf_code = string.Empty;
    string ProdBrdCode = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
    string areacode = string.Empty;
    string Year = string.Empty;
    string Month = string.Empty;
    string area_name = string.Empty;
    string strFMonthName = string.Empty;
    string stype = string.Empty;
    string sCurrentDate = string.Empty; 
  string subdivision = string.Empty;
   string sURL = string.Empty;
    string tot_dr = string.Empty;
    int iCount = -1;
    int FYear = 0;
    int FMonth = 0;
 string dtype=string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
sf_code = Session["sf_code"].ToString();

        areacode = Request.QueryString["Area_Code"];
        area_name = Request.QueryString["Area_name"];
        Year = Request.QueryString["CYear"];
        Month = Request.QueryString["CMonth"];
       stype = Request.QueryString["stype"];
       FMonth = Convert.ToInt32(Month);
       FYear = Convert.ToInt32(Year);
  subdivision = Request.QueryString["subdivision"].ToString();

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
         strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString();
      if (stype == "1")
        {

            CreateDynamicTable();
        }
        else if (stype == "2")
        {
            Fillproduct_total();
        }



     
        if (!Page.IsPostBack)
        {

            lblProd.Text = "Purchase Register Areawise Product Detail  for the Period of " + strFMonthName + " " + Year + " ";
            lblProd.Font.Bold = true;
            lblname.Text = area_name;
           
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


private void Fillproduct_total()
    {
        TableRow tr_det_head = new TableRow();
        tr_det_head.BorderStyle = BorderStyle.Solid;
        tr_det_head.BorderWidth = 1;
        tr_det_head.BackColor = System.Drawing.Color.FromName("#496a9a");
        tr_det_head.Style.Add("Color", "White");
        tr_det_head.BorderColor = System.Drawing.Color.Black;

        SalesForce sf = new SalesForce();


        getweek = sf.Secondary_sales_get_no_of_week(FYear, strFMonthName);

        TableCell leave_utility = new TableCell();
        leave_utility.BorderStyle = BorderStyle.Solid;
        leave_utility.BorderWidth = 1;
        leave_utility.Width = 80;
leave_utility.HorizontalAlign = HorizontalAlign.Center;
        leave_utility.ColumnSpan = 3;
        Literal lit_DR_utility = new Literal();
        lit_DR_utility.Text = "Weekly View";
        leave_utility.BorderColor = System.Drawing.Color.Black;
        leave_utility.Attributes.Add("Class", "rptCellBorder");
        leave_utility.Controls.Add(lit_DR_utility);
        tr_det_head.Cells.Add(leave_utility);

        int df = Convert.ToInt32(getweek.Tables[0].Rows[0].ItemArray.GetValue(0));

        int wee = df + 1;

        if (getweek.Tables[0].Rows.Count > 0)
        {
            for (int i = 1; i < wee; i++)
            {
                TableCell tc_catg_namee = new TableCell();
                tc_catg_namee.BorderStyle = BorderStyle.Solid;
                tc_catg_namee.BorderWidth = 1;
                //text-align: center;

                tc_catg_namee.ColumnSpan = 2;
               HyperLink lit_catg_namee = new HyperLink();
                lit_catg_namee.Text = " Week-" + i.ToString();
 dtype="total";
      
                sURL = "rpt_area_daywise.aspx?weekno=" + i + " &FYear=" + FYear + "&FMonth=" + FMonth + "&Area_Code=" + areacode + "&Area_Name=" + area_name + "&Subdivision=" + subdivision + "&Dtype="+ dtype +"";
                lit_catg_namee.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                lit_catg_namee.Style.Add("Color", "Blue");

                lit_catg_namee.NavigateUrl = "#";
                tc_catg_namee.Attributes.Add("Class", "rptCellBorder");
                tc_catg_namee.HorizontalAlign = HorizontalAlign.Center;
                tc_catg_namee.Controls.Add(lit_catg_namee);
                tr_det_head.Cells.Add(tc_catg_namee);
            }


        }
        tbl.Rows.Add(tr_det_head);



        SalesForce dr = new SalesForce();
        dsDr = dr.Secondary_sales_productdetail(div_code,subdivision);


        if (dsDr.Tables[0].Rows.Count > 0)
        {
            TableRow tr_catg = new TableRow();
            tr_catg.BackColor = System.Drawing.Color.FromName("#496a9a");
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
            lit_det_head_hq.Text = "<b>UOM</b>";
            tc_det_head_hq.Attributes.Add("Class", "tblHead");
            tc_det_head_hq.Controls.Add(lit_det_head_hq);
            tc_det_head_hq.HorizontalAlign = HorizontalAlign.Center;
            tr_catg.Cells.Add(tc_det_head_hq);
            if (getweek.Tables[0].Rows.Count > 0)
            {

                for (int i = 1; i < wee; i++)
                {

                    TableCell tc_det_head_OP = new TableCell();
                    tc_det_head_OP.BorderStyle = BorderStyle.Solid;
                    tc_det_head_OP.BorderWidth = 1;
                    Literal lit_det_head_OP = new Literal();
                    lit_det_head_OP.Text = "<b>Quantity</b>";
                    tc_det_head_OP.Attributes.Add("Class", "tblHead");
                    tc_det_head_OP.Controls.Add(lit_det_head_OP);
                    tc_det_head_OP.HorizontalAlign = HorizontalAlign.Center;
                    tr_catg.Cells.Add(tc_det_head_OP);

                    TableCell tc_det_head_Qua = new TableCell();
                    tc_det_head_Qua.Width = 70;
                    tc_det_head_Qua.BorderStyle = BorderStyle.Solid;
                    tc_det_head_Qua.BorderWidth = 1;
                    Literal lit_det_head_qua = new Literal();
                    lit_det_head_qua.Text = "<b>Value</b>";
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
                lit_det_SNo.Text =  iCount.ToString() ;
                tc_det_SNo.Attributes.Add("Class", "tblRow");
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
tc_det_SNo.HorizontalAlign = HorizontalAlign.Right;
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
                string week = "4";

                if (getweek.Tables[0].Rows.Count > 0)
                {

                    for (int i = 1; i < wee; i++)
                    {
                        SalesForce sd = new SalesForce();
                     
                      //  dsDrr = sd.areawise_purchase_productdetail_total(div_code, FMonth, FYear, i, drdoctor["Product_Detail_Code"].ToString());
dsDrr = sd.areawise_purchase_value(div_code: div_code, cmonth: FMonth, cyear: FYear, subdivision: subdivision, sf_code: sf_code, product_code: drdoctor["Product_Detail_Code"].ToString(), week: i);
                        if (dsDrr.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drrdoctor in dsDrr.Tables[0].Rows)
                            {


                                TableCell tc_det_dr_name = new TableCell();
tc_det_dr_name.HorizontalAlign = HorizontalAlign.Right;
                                Literal lit_det_dr_name = new Literal();
                                lit_det_dr_name.Text =drrdoctor["Rec_Qty"]==DBNull.Value ? drrdoctor["Rec_Qty"].ToString():Convert.ToDecimal( drrdoctor["Rec_Qty"]).ToString("0.00");
                                tc_det_dr_name.Attributes.Add("Class", "tblRow");
                                tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                                tc_det_dr_name.BorderWidth = 1;
                                tc_det_dr_name.Controls.Add(lit_det_dr_name);
                                tr_det_sno.Cells.Add(tc_det_dr_name);


                                TableCell tc_det_dr_hq = new TableCell();
tc_det_dr_hq.HorizontalAlign = HorizontalAlign.Right;
                                Literal lit_det_dr_hq = new Literal();
                                lit_det_dr_hq.Text = drrdoctor["VALUE"]==DBNull.Value ? drrdoctor["VALUE"].ToString():Convert.ToDecimal( drrdoctor["VALUE"]).ToString("0.00");
                                tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                                tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                                tc_det_dr_hq.BorderWidth = 1;
                                tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                                tr_det_sno.Cells.Add(tc_det_dr_hq);
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


                            TableCell tc_det_dr_hq = new TableCell();
                            Literal lit_det_dr_hq = new Literal();
                            lit_det_dr_hq.Text = "";
                            tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                            tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_hq.BorderWidth = 1;
                            tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                            tr_det_sno.Cells.Add(tc_det_dr_hq);

                        }


                        tbl.Rows.Add(tr_det_sno);


                    }
                }

                tbl.Rows.Add(tr_det_sno);


            }

        }
	TableRow tr_total = new TableRow();
        TableCell tc_Count_Total = new TableCell();
        tc_Count_Total.BorderStyle = BorderStyle.Solid;
        tc_Count_Total.BorderWidth = 1;
        Literal lit_Count_Total = new Literal();
        lit_Count_Total.Text = "Total";
        tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
        tc_Count_Total.Controls.Add(lit_Count_Total);
        tc_Count_Total.Font.Bold.ToString();
        tc_Count_Total.BackColor = System.Drawing.Color.White;
        tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
        tc_Count_Total.ColumnSpan = 3;
        tc_Count_Total.Style.Add("text-align", "center");
        tc_Count_Total.Style.Add("font-family", "Calibri");
        tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
        tc_Count_Total.Style.Add("font-size", "10pt");
        tr_total.Cells.Add(tc_Count_Total);
        if (getweek.Tables[0].Rows.Count > 0)
        {
            for (int i = 1; i < wee; i++)
            {

                TableCell tc_tot_month = new TableCell();
                // tc_tot_month.ColumnSpan = 2;
                HyperLink hyp_month = new HyperLink();
                //int iTotLstCount = 0;

                foreach (DataRow drdoctor in dsDr.Tables[0].Rows)
                {
                    SalesForce sd = new SalesForce();
					
			dsDrr = sd.areawise_purchase_value(div_code: div_code, cmonth: FMonth, cyear: FYear, subdivision: subdivision, sf_code: sf_code, product_code: drdoctor["Product_Detail_Code"].ToString(), week: i);
                    if (dsDrr.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drr1doctor in dsDrr.Tables[0].Rows)
                        {
                            string quantot;
                            string valtot;

                            quantot = drr1doctor["Rec_Qty"].ToString();
                            if (quantot != "" && quantot != "0")
                            {
                                iTotLstCount1 += (decimal.Parse(quantot));

                            }
                            valtot = drr1doctor["VALUE"].ToString();
                            if (valtot != "" && valtot != "0")
                            {
                                iTotLstCount += (decimal.Parse(valtot));
                            }
                        }
                    }
                }

                if (iTotLstCount1 != 0)
                {
                    hyp_month.Text = iTotLstCount1.ToString("0.00");
                }
                else
                {
                    hyp_month.Text = "0.00";
                }
                tc_tot_month.BorderStyle = BorderStyle.Solid;
                tc_tot_month.BorderWidth = 1;
                tc_tot_month.BackColor = System.Drawing.Color.White;
                tc_tot_month.Width = 200;
                tc_tot_month.Style.Add("font-family", "Calibri");
                tc_tot_month.Style.Add("font-size", "10pt");
                tc_tot_month.HorizontalAlign = HorizontalAlign.Right;
                tc_tot_month.VerticalAlign = VerticalAlign.Middle;
                tc_tot_month.Controls.Add(hyp_month);
                tc_tot_month.Attributes.Add("style", "font-weight:bold;");
                tc_tot_month.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_tot_month);

                TableCell tc_tot_month2 = new TableCell();
                HyperLink hyp_month2 = new HyperLink();
                if (iTotLstCount != 0)
                {
                    hyp_month2.Text = iTotLstCount.ToString("0.00");
                }
                else
                {
                    hyp_month2.Text = "0.00";
                }
                tc_tot_month2.BorderStyle = BorderStyle.Solid;
                tc_tot_month2.BorderWidth = 1;
                tc_tot_month2.BackColor = System.Drawing.Color.White;
                tc_tot_month2.Width = 200;
                tc_tot_month2.Style.Add("font-family", "Calibri");
                tc_tot_month2.Style.Add("font-size", "10pt");
                tc_tot_month2.HorizontalAlign = HorizontalAlign.Right;
                tc_tot_month2.VerticalAlign = VerticalAlign.Middle;
                tc_tot_month2.Controls.Add(hyp_month2);
                tc_tot_month2.Attributes.Add("style", "font-weight:bold;");
                tc_tot_month2.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_tot_month2);
                iTotLstCount = 0;
                iTotLstCount1 = 0;
            }
        }
        tbl.Rows.Add(tr_total);


    }
   private void CreateDynamicTable()
    {
        TableRow tr_det_head = new TableRow();
        tr_det_head.BorderStyle = BorderStyle.Solid;
        tr_det_head.BorderWidth = 1;
        tr_det_head.BackColor = System.Drawing.Color.FromName("#496a9a");
        tr_det_head.Style.Add("Color", "White");
        tr_det_head.BorderColor = System.Drawing.Color.Black;

        SalesForce sf = new SalesForce();


        getweek = sf.Secondary_sales_get_no_of_week(FYear, strFMonthName);

        TableCell leave_utility = new TableCell();
        leave_utility.BorderStyle = BorderStyle.Solid;
        leave_utility.BorderWidth = 1;
        leave_utility.Width = 80;

        leave_utility.ColumnSpan = 3;
        Literal lit_DR_utility = new Literal();
        lit_DR_utility.Text = "Weekly View";
        leave_utility.BorderColor = System.Drawing.Color.Black;
        leave_utility.Attributes.Add("Class", "rptCellBorder");
leave_utility.HorizontalAlign = HorizontalAlign.Center;
        leave_utility.Controls.Add(lit_DR_utility);
        tr_det_head.Cells.Add(leave_utility);

        int df = Convert.ToInt32(getweek.Tables[0].Rows[0].ItemArray.GetValue(0));

        int wee = df + 1;

        if (getweek.Tables[0].Rows.Count > 0)
        {
            for (int i = 1; i < wee; i++)
            {
                TableCell tc_catg_namee = new TableCell();
                tc_catg_namee.BorderStyle = BorderStyle.Solid;
                tc_catg_namee.BorderWidth = 1;
                //text-align: center;

                tc_catg_namee.ColumnSpan = 2;
                HyperLink lit_catg_namee = new HyperLink();
                lit_catg_namee.Text = " Week-" + i.ToString();
 dtype="value";
      
                sURL = "rpt_area_daywise.aspx?weekno=" + i + " &FYear=" + FYear + "&FMonth=" + FMonth + "&Area_Code=" + areacode + "&Area_Name=" + area_name + "&Subdivision=" + subdivision + "&Dtype="+ dtype +"";
                lit_catg_namee.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                lit_catg_namee.Style.Add("Color", "Blue");

                lit_catg_namee.NavigateUrl = "#";


                tc_catg_namee.Attributes.Add("Class", "rptCellBorder");
                tc_catg_namee.HorizontalAlign = HorizontalAlign.Center;
                tc_catg_namee.Controls.Add(lit_catg_namee);
                tr_det_head.Cells.Add(tc_catg_namee);
            }


        }
        tbl.Rows.Add(tr_det_head);



        SalesForce dr = new SalesForce();
        dsDr = dr.Secondary_sales_productdetail(div_code,subdivision);


        if (dsDr.Tables[0].Rows.Count > 0)
        {
            TableRow tr_catg = new TableRow();
            tr_catg.BackColor = System.Drawing.Color.FromName("#496a9a");
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
            lit_det_head_hq.Text = "<b>UOM</b>";
            tc_det_head_hq.Attributes.Add("Class", "tblHead");
            tc_det_head_hq.Controls.Add(lit_det_head_hq);
            tc_det_head_hq.HorizontalAlign = HorizontalAlign.Center;
            tr_catg.Cells.Add(tc_det_head_hq);
            if (getweek.Tables[0].Rows.Count > 0)
            {

                for (int i = 1; i < wee; i++)
                {

                    TableCell tc_det_head_OP = new TableCell();
                    tc_det_head_OP.BorderStyle = BorderStyle.Solid;
                    tc_det_head_OP.BorderWidth = 1;
                    Literal lit_det_head_OP = new Literal();
                    lit_det_head_OP.Text = "<b>Quantity</b>";
                    tc_det_head_OP.Attributes.Add("Class", "tblHead");
                    tc_det_head_OP.Controls.Add(lit_det_head_OP);
                    tc_det_head_OP.HorizontalAlign = HorizontalAlign.Center;
                    tr_catg.Cells.Add(tc_det_head_OP);

                    TableCell tc_det_head_Qua = new TableCell();
                    tc_det_head_Qua.Width = 70;
                    tc_det_head_Qua.BorderStyle = BorderStyle.Solid;
                    tc_det_head_Qua.BorderWidth = 1;
                    Literal lit_det_head_qua = new Literal();
                    lit_det_head_qua.Text = "<b>Value</b>";
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
                lit_det_SNo.Text =  iCount.ToString();
                tc_det_SNo.Attributes.Add("Class", "tblRow");
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
tc_det_SNo.Style.Add("text-align","right");
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
                string week = "4";

                if (getweek.Tables[0].Rows.Count > 0)
                {

                    for (int i = 1; i < wee; i++)
                    {
                        SalesForce sd = new SalesForce();
                     
                      //  dsDrr = sd.areawise_purchase_productdetail(div_code, FMonth, FYear, i, drdoctor["Product_Detail_Code"].ToString(), areacode);
  dsDrr = sd.areawise_purchase_value(areacode: areacode, div_code: div_code, cmonth: FMonth, cyear: FYear, subdivision: subdivision, sf_code: sf_code, product_code: drdoctor["Product_Detail_Code"].ToString(), week: i);
                        if (dsDrr.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drrdoctor in dsDrr.Tables[0].Rows)
                            {


                                TableCell tc_det_dr_name = new TableCell();
tc_det_dr_name.HorizontalAlign = HorizontalAlign.Right;
                                Literal lit_det_dr_name = new Literal();
                                lit_det_dr_name.Text =drrdoctor["Rec_Qty"]==DBNull.Value ? drrdoctor["Rec_Qty"].ToString():Convert.ToDecimal( drrdoctor["Rec_Qty"]).ToString("0.00");
                                tc_det_dr_name.Attributes.Add("Class", "tblRow");
                                tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                                tc_det_dr_name.BorderWidth = 1;
                                tc_det_dr_name.Controls.Add(lit_det_dr_name);
                                tr_det_sno.Cells.Add(tc_det_dr_name);


                                TableCell tc_det_dr_hq = new TableCell();
tc_det_dr_hq.HorizontalAlign = HorizontalAlign.Right;
                                Literal lit_det_dr_hq = new Literal();
                                lit_det_dr_hq.Text =drrdoctor["VALUE"]==DBNull.Value ? drrdoctor["VALUE"].ToString():Convert.ToDecimal( drrdoctor["VALUE"]).ToString("0.00");
                                tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                                tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                                tc_det_dr_hq.BorderWidth = 1;
                                tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                                tr_det_sno.Cells.Add(tc_det_dr_hq);
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


                            TableCell tc_det_dr_hq = new TableCell();
                            Literal lit_det_dr_hq = new Literal();
                            lit_det_dr_hq.Text = "";
                            tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                            tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_hq.BorderWidth = 1;
                            tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                            tr_det_sno.Cells.Add(tc_det_dr_hq);

                        }


                        tbl.Rows.Add(tr_det_sno);


                    }
                }

                tbl.Rows.Add(tr_det_sno);


            }

        }
		TableRow tr_total = new TableRow();
        TableCell tc_Count_Total = new TableCell();
        tc_Count_Total.BorderStyle = BorderStyle.Solid;
        tc_Count_Total.BorderWidth = 1;
        Literal lit_Count_Total = new Literal();
        lit_Count_Total.Text = "Total";
        tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
        tc_Count_Total.Controls.Add(lit_Count_Total);
        tc_Count_Total.Font.Bold.ToString();
        tc_Count_Total.BackColor = System.Drawing.Color.White;
        tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
        tc_Count_Total.ColumnSpan = 3;
        tc_Count_Total.Style.Add("text-align", "center");
        tc_Count_Total.Style.Add("font-family", "Calibri");
        tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
        tc_Count_Total.Style.Add("font-size", "10pt");
        tr_total.Cells.Add(tc_Count_Total);
        if (getweek.Tables[0].Rows.Count > 0)
        {
            for (int i = 1; i < wee; i++)
            {

                TableCell tc_tot_month = new TableCell();
                // tc_tot_month.ColumnSpan = 2;
                HyperLink hyp_month = new HyperLink();
                //int iTotLstCount = 0;

                foreach (DataRow drdoctor in dsDr.Tables[0].Rows)
                {
                    SalesForce sd = new SalesForce();
					
					  dsDrr = sd.areawise_purchase_value(areacode: areacode, div_code: div_code, cmonth: FMonth, cyear: FYear, subdivision: subdivision, sf_code: sf_code, product_code: drdoctor["Product_Detail_Code"].ToString(), week: i);
                    if (dsDrr.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drr1doctor in dsDrr.Tables[0].Rows)
                        {
                            string quantot;
                            string valtot;

                            quantot = drr1doctor["Rec_Qty"].ToString();
                            if (quantot != "" && quantot != "0")
                            {
                                iTotLstCount1 += (decimal.Parse(quantot));

                            }
                            valtot = drr1doctor["VALUE"].ToString();
                            if (valtot != "" && valtot != "0")
                            {
                                iTotLstCount += (decimal.Parse(valtot));
                            }
                        }
                    }
                }

                if (iTotLstCount1 != 0)
                {
                    hyp_month.Text = iTotLstCount1.ToString("0.00");
                }
                else
                {
                    hyp_month.Text = "0.00";
                }
                tc_tot_month.BorderStyle = BorderStyle.Solid;
                tc_tot_month.BorderWidth = 1;
                tc_tot_month.BackColor = System.Drawing.Color.White;
                tc_tot_month.Width = 200;
                tc_tot_month.Style.Add("font-family", "Calibri");
                tc_tot_month.Style.Add("font-size", "10pt");
                tc_tot_month.HorizontalAlign = HorizontalAlign.Right;
                tc_tot_month.VerticalAlign = VerticalAlign.Middle;
                tc_tot_month.Controls.Add(hyp_month);
                tc_tot_month.Attributes.Add("style", "font-weight:bold;");
                tc_tot_month.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_tot_month);

                TableCell tc_tot_month2 = new TableCell();
                HyperLink hyp_month2 = new HyperLink();
                if (iTotLstCount != 0)
                {
                    hyp_month2.Text = iTotLstCount.ToString("0.00");
                }
                else
                {
                    hyp_month2.Text = "0.00";
                }
                tc_tot_month2.BorderStyle = BorderStyle.Solid;
                tc_tot_month2.BorderWidth = 1;
                tc_tot_month2.BackColor = System.Drawing.Color.White;
                tc_tot_month2.Width = 200;
                tc_tot_month2.Style.Add("font-family", "Calibri");
                tc_tot_month2.Style.Add("font-size", "10pt");
                tc_tot_month2.HorizontalAlign = HorizontalAlign.Right;
                tc_tot_month2.VerticalAlign = VerticalAlign.Middle;
                tc_tot_month2.Controls.Add(hyp_month2);
                tc_tot_month2.Attributes.Add("style", "font-weight:bold;");
                tc_tot_month2.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_tot_month2);
                iTotLstCount = 0;
                iTotLstCount1 = 0;
            }
        }
        tbl.Rows.Add(tr_total);


    }
}