﻿using System;
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

public partial class MIS_Reports_Secondary_Order_Distributor_wise_view : System.Web.UI.Page
{
    DataSet dsProduct = null;
    DataSet dsDr = null;
    string sCmd = string.Empty;
    string div_code = string.Empty;
    string ProdBrdCode = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
    string stockist = string.Empty;
    string Year = string.Empty;
    string Month = string.Empty;
    string Stock_name = string.Empty;
    string sf_name = string.Empty;
    string type = string.Empty;
    string sCurrentDate = string.Empty;
    string Sf_Code_multiple = string.Empty;
    string MultiProd_Code = string.Empty;
    string tot_dr = string.Empty;
    int iCount = -1;
    DataSet dsDoc = null;
    Decimal iTotLstCount = 0;  

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
      
        stockist = Request.QueryString["Stockist_Code"];
        Stock_name = Request.QueryString["Stockist_name"]; 
        Year = Request.QueryString["Year"];
        Month = Request.QueryString["Month"];   
       
        sCurrentDate = Request.QueryString["sCurrentDate"];
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString().Substring(0, 3);
        sf_name = sf_name.TrimStart(',');
        

        if (!Page.IsPostBack)
        {

            lblProd.Text = "Secondary Order Distributor wise view for the Period of " + strMonthName + " " + Year + " ";
            lblProd.Font.Bold = true;
            lblname.Text = Stock_name;
            CreateDynamicTable();
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
        string attachment = "attachment; filename=Secondary Order Distributor Wise View.xls";
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

    private void CreateDynamicTable()
    {
      
      
        SalesForce dr = new SalesForce();
       
            dsDr = dr.Sec_Order_Dis_wise(div_code, stockist, Convert.ToInt16(Year), Convert.ToInt16(Month), sCurrentDate);
        
        if (dsDr.Tables[0].Rows.Count > 0)
        {
            TableRow tr_det_head = new TableRow();
            tr_det_head.BorderStyle = BorderStyle.Solid;
            tr_det_head.BorderWidth = 1;
            tr_det_head.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_det_head.Style.Add("Color", "White");
            tr_det_head.BorderColor = System.Drawing.Color.Black;

            TableCell tc_det_head_SNo = new TableCell();
            tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_head_SNo.BorderWidth = 1;
            Literal lit_det_head_SNo = new Literal();
            lit_det_head_SNo.Text = "<b>S.No</b>";
            tc_det_head_SNo.Attributes.Add("Class", "tblHead");
            tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
            tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_SNo);


            TableCell tc_det_head_doc = new TableCell();
            tc_det_head_doc.BorderStyle = BorderStyle.Solid;
            tc_det_head_doc.BorderWidth = 1;
            Literal lit_det_head_doc = new Literal();
            lit_det_head_doc.Text = "<b>Product Name</b>";
            tc_det_head_doc.Attributes.Add("Class", "tblHead");
            tc_det_head_doc.Controls.Add(lit_det_head_doc);
            tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_doc);

            //TableCell tc_det_head_hq = new TableCell();
            //tc_det_head_hq.BorderStyle = BorderStyle.Solid;
            //tc_det_head_hq.BorderWidth = 1;
            //Literal lit_det_head_hq = new Literal();
            //lit_det_head_hq.Text = "<b>Product_Sale_Unit</b>";
            //tc_det_head_hq.Attributes.Add("Class", "tblHead");
            //tc_det_head_hq.Controls.Add(lit_det_head_hq);
            //tc_det_head_hq.HorizontalAlign = HorizontalAlign.Center;
            //tr_det_head.Cells.Add(tc_det_head_hq);

            TableCell tc_det_head_Qua = new TableCell();
            tc_det_head_Qua.BorderStyle = BorderStyle.Solid;
            tc_det_head_Qua.BorderWidth = 1;
            Literal lit_det_head_qua = new Literal();
            lit_det_head_qua.Text = "<b>Order_Qty</b>";
            tc_det_head_Qua.Attributes.Add("Class", "tblHead");
            tc_det_head_Qua.Controls.Add(lit_det_head_qua);
            tc_det_head_Qua.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_Qua);

            TableCell tc_det_head_Quai = new TableCell();
            tc_det_head_Quai.BorderStyle = BorderStyle.Solid;
            tc_det_head_Quai.BorderWidth = 1;
            Literal lit_det_head_quai = new Literal();
            lit_det_head_quai.Text = "<b>Value</b>";
            tc_det_head_Quai.Attributes.Add("Class", "tblHead");
            tc_det_head_Quai.Controls.Add(lit_det_head_quai);
            tc_det_head_Quai.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_Quai);


            tbl.Rows.Add(tr_det_head);


           

            
          

            iCount = 0;
            foreach (DataRow drdoctor in dsDr.Tables[0].Rows)
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

                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drdoctor["Product_Name"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;

                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det_sno.Cells.Add(tc_det_usr);

                //TableCell tc_det_sfcode = new TableCell();
                //Literal lit_det_sfcode = new Literal();
                //lit_det_sfcode.Text = "&nbsp;" + drdoctor["Quantity"].ToString();
                //tc_det_sfcode.BorderStyle = BorderStyle.Solid;
                //tc_det_sfcode.BorderWidth = 1;
                //tc_det_sfcode.Attributes.Add("Class", "rptCellBorder");
                //tc_det_sfcode.Controls.Add(lit_det_sfcode);
                //tr_det_sno.Cells.Add(tc_det_sfcode);


                TableCell tc_det_dr_name = new TableCell();
                Literal lit_det_dr_name = new Literal();
                lit_det_dr_name.Text = drdoctor["Quantity"].ToString();
                tc_det_dr_name.Attributes.Add("Class", "tblRow");
                tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                tc_det_dr_name.BorderWidth = 1;
                tc_det_dr_name.Controls.Add(lit_det_dr_name);
                tr_det_sno.Cells.Add(tc_det_dr_name);

                TableCell tc_det_dr_hq = new TableCell();
                Literal lit_det_dr_hq = new Literal();
                lit_det_dr_hq.Text = drdoctor["value"].ToString();
				iTotLstCount += Decimal.Parse(drdoctor["value"].ToString());
                tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                tc_det_dr_hq.BorderWidth = 1;
                tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                tr_det_sno.Cells.Add(tc_det_dr_hq);




               
                //TableCell tc_det_sample = new TableCell();
                //Literal lit_det_sample = new Literal();

              

              


                //tc_lst_month.BorderStyle = BorderStyle.Solid;
                //tc_lst_month.BorderWidth = 1;
                //hyp_lst_month.Text = drdoctor["Despatch_Qty"].ToString();
                //tc_lst_month.BackColor = System.Drawing.Color.White;
                //tc_lst_month.Width = 200;
                //tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                //tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                //tc_lst_month.Controls.Add(hyp_lst_month);
                //tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                //tr_det_sno.Cells.Add(tc_lst_month);

                //lit_det_sample.Text = drdoctor["Sample"].ToString();
                //tc_det_sample.Attributes.Add("Class", "tblRow");
                //tc_det_sample.BorderStyle = BorderStyle.Solid;
                //tc_det_sample.BorderWidth = 1;
                //tc_det_sample.Controls.Add(lit_det_sample);
                //tc_det_sample.HorizontalAlign = HorizontalAlign.Center;
                //tr_det_sno.Cells.Add(tc_det_sample);

                tbl.Rows.Add(tr_det_sno);
            }
 TableRow tr_total = new TableRow();

            TableCell tc_Count_Total = new TableCell();
            tc_Count_Total.BorderStyle = BorderStyle.Solid;
            tc_Count_Total.BorderWidth = 1;

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


            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;



            TableCell tc_tot_month = new TableCell();
            HyperLink hyp_month = new HyperLink();
            //int iTotLstCount = 0;

            hyp_month.Text = iTotLstCount.ToString();


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







            tbl.Rows.Add(tr_total);

        }


    }
}
