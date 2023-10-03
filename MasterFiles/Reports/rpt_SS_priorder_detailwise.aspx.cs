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
using DBase_EReport;

public partial class MasterFiles_Reports_rpt_SS_priorder_detailwise : System.Web.UI.Page
{
    string divcode = string.Empty;
    string Stockist_Code = string.Empty;
    string Stockist_Name = string.Empty;
    string order_no = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string Activity_date = string.Empty;
    string Tdate = string.Empty;
    string TYear = string.Empty;
    string Monthsub = string.Empty;
    string sCurrentDate = string.Empty;
    string tot_dr = string.Empty;
    string Sf_Name = string.Empty;
    string subdivision = string.Empty;
    string Sf_Code = string.Empty;
    string date = string.Empty;
    string hdate = string.Empty;
    string hTdate = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsDoc = new DataSet();
    DataSet dsDrr = new DataSet();
    Decimal iTotLstCount = 0;
    Decimal iTotLstCountt;
    Decimal iTotLstfree = 0;
    Decimal iTotLstdiscount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();


        Sf_Name = Request.QueryString["Sf_Name"].ToString();
        Activity_date = Request.QueryString["Activity_date"].ToString();
        Tdate = Request.QueryString["To_date"].ToString().Trim();
        Sf_Code = Request.QueryString["Sf_Code"].ToString();
        date = Activity_date.Trim();
        Stockist_Code = Request.QueryString["Stockist_Code"].ToString();
        Stockist_Name = Request.QueryString["Stockist_Name"].ToString();
        DateTime dt = Convert.ToDateTime(date);
        hdate = dt.ToString("yyyy-MM-dd");
        DateTime dt1 = Convert.ToDateTime(Tdate);
        hTdate = dt1.ToString("yyyy-MM-dd");

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        //string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = " Primary Orders Product Detail View for  " + hdate + "";
        lblHead1.Text = " Primary Orders Product Detail View for  " + hdate + " to " + hTdate + "";


        lblMonth.Text = Sf_Name;
        lblYear.Text = Stockist_Name;
        FillSF();

    }

    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        DCR sf = new DCR();
        dsDrr = Gettransno1(Sf_Code, divcode, hdate, hTdate, Stockist_Code);
        foreach (DataRow d in dsDrr.Tables[0].Rows)
        {
            order_no = d["Trans_Sl_No"].ToString();
            dsSalesForce = product_detaill(order_no);
            TableRow tr_headerr = new TableRow();

            tr_headerr.BackColor = System.Drawing.Color.FromName("#4aced6");
            tr_headerr.Style.Add("Color", "Black");
            tr_headerr.Style.Add("Font", "Bold");
            TableCell tc_SNod = new TableCell();
            tc_SNod.HorizontalAlign = HorizontalAlign.Right;

            tc_SNod.Width = 50;
            tc_SNod.RowSpan = 1;
            tc_SNod.ColumnSpan = 2;
            Literal lit_SNod =
                new Literal();
            lit_SNod.Text = "Order No -";

            tc_SNod.Controls.Add(lit_SNod);
            tc_SNod.Attributes.Add("Class", "rptCellBorder");
            tr_headerr.Cells.Add(tc_SNod);
            TableCell tc_SNodd = new TableCell();
            tc_SNodd.HorizontalAlign = HorizontalAlign.Left;

            tc_SNodd.Width = 50;
            tc_SNodd.RowSpan = 1;
            tc_SNodd.ColumnSpan = 6;
            Literal lit_SNodd =
                new Literal();
            lit_SNodd.Text = "ORD" + order_no;

            tc_SNodd.Controls.Add(lit_SNodd);
            tc_SNodd.Attributes.Add("Class", "rptCellBorder");
            tr_headerr.Cells.Add(tc_SNodd);

            tbl.Rows.Add(tr_headerr);
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
                tc_SNo.RowSpan = 1;
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
                tc_DR_Code.RowSpan = 1;
                Literal lit_DR_Code = new Literal();
                lit_DR_Code.Text = "<center>Product Name</center>";
                tc_DR_Code.Controls.Add(lit_DR_Code);
                tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
                tc_DR_Code.BorderColor = System.Drawing.Color.Black;
                tr_header.Cells.Add(tc_DR_Code);




                TableCell tc_DR_Name = new TableCell();
                tc_DR_Name.BorderStyle = BorderStyle.Solid;
                tc_DR_Name.BorderWidth = 1;
                tc_DR_Name.Width = 250;
                tc_DR_Name.RowSpan = 1;
                Literal lit_DR_Name = new Literal();
                lit_DR_Name.Text = "<center>Quantity</center>";
                tc_DR_Name.BorderColor = System.Drawing.Color.Black;
                tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
                tc_DR_Name.Controls.Add(lit_DR_Name);
                tr_header.Cells.Add(tc_DR_Name);

                TableCell tc_det_head_Quamd = new TableCell();
                tc_det_head_Quamd.Width = 70;
                tc_det_head_Quamd.BorderStyle = BorderStyle.Solid;
                tc_det_head_Quamd.BorderWidth = 1;
                Literal lit_det_head_quamd = new Literal();
                lit_det_head_quamd.Text = "<b>Mfg.date</b>";
                tc_det_head_Quamd.Attributes.Add("Class", "tblHead");
                tc_det_head_Quamd.Controls.Add(lit_det_head_quamd);
                tc_det_head_Quamd.HorizontalAlign = HorizontalAlign.Center;
                tr_header.Cells.Add(tc_det_head_Quamd);
                tc_det_head_Quamd.Visible = false;

                TableCell tc_det_head_Rate = new TableCell();
                tc_det_head_Rate.Width = 70;
                tc_det_head_Rate.BorderStyle = BorderStyle.Solid;
                tc_det_head_Rate.BorderWidth = 1;
                Literal lit_det_head_rate = new Literal();
                lit_det_head_rate.Text = "<b>Rate</b>";
                tc_det_head_Rate.Attributes.Add("Class", "tblHead");
                tc_det_head_Rate.Controls.Add(lit_det_head_rate);
                tc_det_head_Rate.HorizontalAlign = HorizontalAlign.Center;
                tr_header.Cells.Add(tc_det_head_Rate);

                TableCell tc_det_head_free = new TableCell();
                tc_det_head_free.Width = 70;
                tc_det_head_free.BorderStyle = BorderStyle.Solid;
                tc_det_head_free.BorderWidth = 1;
                Literal lit_det_head_free = new Literal();
                lit_det_head_free.Text = "<b>Free</b>";
                tc_det_head_free.Attributes.Add("Class", "tblHead");
                tc_det_head_free.Controls.Add(lit_det_head_free);
                tc_det_head_free.HorizontalAlign = HorizontalAlign.Center;
                tr_header.Cells.Add(tc_det_head_free);

                TableCell tc_det_head_dis = new TableCell();
                tc_det_head_dis.Width = 70;
                tc_det_head_dis.BorderStyle = BorderStyle.Solid;
                tc_det_head_dis.BorderWidth = 1;
                Literal lit_det_head_dis = new Literal();
                lit_det_head_dis.Text = "<b>Discount</b>";
                tc_det_head_dis.Attributes.Add("Class", "tblHead");
                tc_det_head_dis.Controls.Add(lit_det_head_dis);
                tc_det_head_dis.HorizontalAlign = HorizontalAlign.Center;
                tr_header.Cells.Add(tc_det_head_dis);

                TableCell tc_det_head_Qua = new TableCell();
                tc_det_head_Qua.Width = 70;
                tc_det_head_Qua.BorderStyle = BorderStyle.Solid;
                tc_det_head_Qua.BorderWidth = 1;
                Literal lit_det_head_qua = new Literal();
                lit_det_head_qua.Text = "<b>Value</b>";
                tc_det_head_Qua.Attributes.Add("Class", "tblHead");
                tc_det_head_Qua.Controls.Add(lit_det_head_qua);
                tc_det_head_Qua.HorizontalAlign = HorizontalAlign.Center;
                tr_header.Cells.Add(tc_det_head_Qua);


                TableCell tc_det_head_min1 = new TableCell();
                tc_det_head_min1.BorderStyle = BorderStyle.Solid;
                tc_det_head_min1.BorderWidth = 1;
                Literal lit_det_head_min1 = new Literal();
                lit_det_head_min1.Text = "<b>Unit_Of_Mass</b>";
                tc_det_head_min1.Attributes.Add("Class", "tblHead");
                tc_det_head_min1.Controls.Add(lit_det_head_min1);
                tc_det_head_min1.HorizontalAlign = HorizontalAlign.Center;
                tr_header.Cells.Add(tc_det_head_min1);



                tbl.Rows.Add(tr_header);
                //Sub Header



                if (dsSalesForce.Tables[0].Rows.Count > 0)
                    ViewState["dsSalesForce"] = dsSalesForce;


                int iCount = 0;
                //string iTotLstCount ="0";
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

                    //SF_code
                    TableCell tc_det_usr = new TableCell();
                    Literal lit_det_usr = new Literal();
                    lit_det_usr.Text = "&nbsp;" + drFF["Product_Name"].ToString();
                    tc_det_usr.BorderStyle = BorderStyle.Solid;
                    tc_det_usr.BorderWidth = 1;
                    tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                    tc_det_usr.Controls.Add(lit_det_usr);
                    tr_det.Cells.Add(tc_det_usr);

                    TableCell tc_det_usrr = new TableCell();
                    tc_det_usrr.Visible = false;
                    Literal lit_det_usrr = new Literal();
                    lit_det_usrr.Text = "&nbsp;" + drFF["Product_Code"].ToString();
                    tc_det_usrr.BorderStyle = BorderStyle.Solid;
                    tc_det_usrr.BorderWidth = 1;
                    tc_det_usrr.Attributes.Add("Class", "rptCellBorder");
                    tc_det_usrr.Controls.Add(lit_det_usrr);
                    tr_det.Cells.Add(tc_det_usrr);


                    //SF Name
                    TableCell tc_det_FF = new TableCell();
                    tc_det_FF.Width = 200;
                    Literal lit_det_FF = new Literal();
                    lit_det_FF.Text = drFF["Quantity"].ToString();

                    tc_det_FF.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                    tc_det_FF.Controls.Add(lit_det_FF);
                    tr_det.Cells.Add(tc_det_FF);

                    //TableCell tc_det_dr_hqmg = new TableCell();
                    //tc_det_dr_hqmg.HorizontalAlign = HorizontalAlign.Center;
                    //Literal lit_det_dr_hqmg = new Literal();
                    //lit_det_dr_hqmg.Text = drFF["MfgDt"].ToString();

                    //tc_det_dr_hqmg.Attributes.Add("Class", "tblRow");
                    //tc_det_dr_hqmg.BorderStyle = BorderStyle.Solid;
                    //tc_det_dr_hqmg.BorderWidth = 1;
                    //tc_det_dr_hqmg.Controls.Add(lit_det_dr_hqmg);
                    //tr_det.Cells.Add(tc_det_dr_hqmg);
                    //tc_det_dr_hqmg.Visible = false;


                    TableCell tc_det_dr_rate = new TableCell();
                    tc_det_dr_rate.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_dr_rate = new Literal();
                    lit_det_dr_rate.Text = drFF["Rate"].ToString();
                    tc_det_dr_rate.Attributes.Add("Class", "tblRow");
                    tc_det_dr_rate.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_rate.BorderWidth = 1;
                    tc_det_dr_rate.Controls.Add(lit_det_dr_rate);
                    tr_det.Cells.Add(tc_det_dr_rate);


                    TableCell tc_det_dr_free = new TableCell();
                    tc_det_dr_free.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_dr_free = new Literal();
                    lit_det_dr_free.Text = drFF["free"].ToString();
                    iTotLstfree += Decimal.Parse(drFF["free"].ToString());
                    tc_det_dr_free.Attributes.Add("Class", "tblRow");
                    tc_det_dr_free.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_free.BorderWidth = 1;
                    tc_det_dr_free.Controls.Add(lit_det_dr_free);
                    tr_det.Cells.Add(tc_det_dr_free);

                    TableCell tc_det_dr_dis = new TableCell();
                    tc_det_dr_dis.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_dr_dis = new Literal();
                    lit_det_dr_dis.Text = drFF["discount_price"].ToString();
                    iTotLstdiscount += Decimal.Parse(drFF["discount_price"].ToString());
                    tc_det_dr_dis.Attributes.Add("Class", "tblRow");
                    tc_det_dr_dis.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_dis.BorderWidth = 1;
                    tc_det_dr_dis.Controls.Add(lit_det_dr_dis);
                    tr_det.Cells.Add(tc_det_dr_dis);



                    TableCell tc_det_dr_hq = new TableCell();
                    tc_det_dr_hq.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_dr_hq = new Literal();
                    lit_det_dr_hq.Text = drFF["VALUE"].ToString();
                    iTotLstCount += Decimal.Parse(drFF["VALUE"].ToString());
                    tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                    tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_hq.BorderWidth = 1;
                    tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                    tr_det.Cells.Add(tc_det_dr_hq);


                    TableCell tc_det_dr_min = new TableCell();
                    Literal lit_det_dr_min = new Literal();
                    lit_det_dr_min.Text = drFF["netpack"].ToString();
                    //iTotLstCountt += Decimal.Parse(drFF["netpack"].ToString());
                    tc_det_dr_min.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_dr_min.Attributes.Add("Class", "tblRow");
                    tc_det_dr_min.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_min.BorderWidth = 1;
                    tc_det_dr_min.Controls.Add(lit_det_dr_min);
                    tr_det.Cells.Add(tc_det_dr_min);




                    tbl.Rows.Add(tr_det);












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
                tc_Count_Total.ColumnSpan = 4;
                tc_Count_Total.Style.Add("text-align", "left");
                tc_Count_Total.Style.Add("font-family", "Calibri");
                tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
                tc_Count_Total.Style.Add("font-size", "10pt");

                tr_total.Cells.Add(tc_Count_Total);


                //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;

                TableCell tc_tot_free = new TableCell();
                HyperLink hyp_free = new HyperLink();
                //int iTotLstCount = 0;

                hyp_free.Text = iTotLstfree.ToString();


                tc_tot_free.BorderStyle = BorderStyle.Solid;
                tc_tot_free.BorderWidth = 1;
                tc_tot_free.BackColor = System.Drawing.Color.White;
                tc_tot_free.Width = 200;
                tc_tot_free.Style.Add("font-family", "Calibri");
                tc_tot_free.Style.Add("font-size", "10pt");
                tc_tot_free.HorizontalAlign = HorizontalAlign.Center;
                tc_tot_free.VerticalAlign = VerticalAlign.Middle;
                tc_tot_free.Controls.Add(hyp_free);
                tc_tot_free.Attributes.Add("style", "font-weight:bold;");
                tc_tot_free.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_tot_free);

                iTotLstfree = 0;

                TableCell tc_tot_discount = new TableCell();
                HyperLink hyp_discount = new HyperLink();
                //int iTotLstCount = 0;

                hyp_discount.Text = iTotLstdiscount.ToString();


                tc_tot_discount.BorderStyle = BorderStyle.Solid;
                tc_tot_discount.BorderWidth = 1;
                tc_tot_discount.BackColor = System.Drawing.Color.White;
                tc_tot_discount.Width = 200;
                tc_tot_discount.Style.Add("font-family", "Calibri");
                tc_tot_discount.Style.Add("font-size", "10pt");
                tc_tot_discount.HorizontalAlign = HorizontalAlign.Center;
                tc_tot_discount.VerticalAlign = VerticalAlign.Middle;
                tc_tot_discount.Controls.Add(hyp_discount);
                tc_tot_discount.Attributes.Add("style", "font-weight:bold;");
                tc_tot_discount.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_tot_discount);

                iTotLstdiscount = 0;

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

                iTotLstCount = 0;


                //TableCell tc_tot_montht = new TableCell();
                //HyperLink hyp_montht = new HyperLink();
                ////int iTotLstCount = 0;

                //hyp_montht.Text = iTotLstCountt.ToString();


                //tc_tot_montht.BorderStyle = BorderStyle.Solid;
                //tc_tot_montht.BorderWidth = 1;
                //tc_tot_montht.BackColor = System.Drawing.Color.White;
                //tc_tot_montht.Width = 200;
                //tc_tot_montht.Style.Add("font-family", "Calibri");
                //tc_tot_montht.Style.Add("font-size", "10pt");
                //tc_tot_montht.HorizontalAlign = HorizontalAlign.Center;
                //tc_tot_montht.VerticalAlign = VerticalAlign.Middle;
                //tc_tot_montht.Controls.Add(hyp_montht);
                //tc_tot_montht.Attributes.Add("style", "font-weight:bold;");
                //tc_tot_montht.Attributes.Add("Class", "rptCellBorder");
                //tr_total.Cells.Add(tc_tot_montht);



                //iTotLstCountt = 0;



                tbl.Rows.Add(tr_total);

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

    public DataSet Gettransno1(string sfcode, string div_code, string activitydate, string Tdate, string Stockist_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsSF = null;
        string strQry = "select Trans_Sl_No from Trans_priOrder_Head  where Sf_code='" + sfcode + "' and  (cast(Order_Date as date)  between '" + activitydate + "' and '" + Tdate + "') and Stockist_Code='" + Stockist_Code + "'";
        //strQry = "SELECT Product_Code,cast(sum(Quantity) as varchar) Qty FROM Trans_Order_Head H inner join Trans_Order_Details D on H.Trans_Sl_No=D.Trans_Sl_No where Sf_Code='" + sfcode + "' and Cust_Code='" + retailer_code + "' and Stockist_Code='" + DistCode + "' and cast(convert(varchar,Order_Date,101) as datetime)='" + activitydate + " 00:00:00.000' group by Product_Code";
        try
        {
            dsSF = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }
    public DataSet product_detaill(string Trans_Sl_no)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "select d.Product_Code,d.Product_Name,(d.Qty * Product_Unit_Value) Quantity,d.Rate,d.free,d.discount_price,d.value," +
                        "(CAST(d.Qty AS VARCHAR(10)) + ' ' + d.product_Unit_Name)netpack from Trans_priOrder_Head h " +
                        "inner join Trans_priOrder_Details d on h.Trans_Sl_No = d.Trans_Sl_No where h.Trans_Sl_No = '" + Trans_Sl_no + "'";

        try
        {
            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;

    }



    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}