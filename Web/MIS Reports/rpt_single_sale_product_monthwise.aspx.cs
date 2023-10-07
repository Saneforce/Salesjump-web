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

public partial class rpt_single_sale_product_monthwise : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string Product_Brd_Code = string.Empty;
    string divcode = string.Empty;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    decimal iTotLstCount2 = 0;
    string Product_Cat_Code = string.Empty;
    string Product_Cat_Name = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Product_Brd_Name = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    decimal value1 = 0;
    decimal value2 = 0;
    string viewby = string.Empty;
    string tot_dr = string.Empty;
    string Monthsub = string.Empty;
    string Stockist_Name = string.Empty;
    string tot_value = string.Empty;
    decimal val = 0;
    DataSet dsprd = new DataSet();
    string subdivision = string.Empty;
    string Stockist_Code = string.Empty;
    string sCurrentDate = string.Empty;
    string product_code = string.Empty;
    string product_name = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();


        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        product_code = Request.QueryString["product_code"].ToString();
        product_name = Request.QueryString["product_name"].ToString();
        subdivision = Request.QueryString["subdivision"].ToString();

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        //string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Sale Trend Analysis Details for  " + strFMonthName + " " + FYear + "To" + " " + strTMonthName + " " + TYear;



        FillProductwise();



        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;


    }

    private void FillProductwise()
    {
        Random rndm = new Random();
        string t = "1";
        int mnth = Convert.ToInt32(FMonth), year = Convert.ToInt32(FYear);
        DateTime dt = Convert.ToDateTime(year + "-" + mnth.ToString() + "-" + t.ToString());
        string fromdate = dt.ToString("yyyy-MM-dd 00:00:00.000");
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd 23:59:59.999");
        string stCrtDtaPnt = string.Empty;
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.Getproductsubdivision_wise(divcode, subdivision);


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
tc_SNo.Style.Add("text-align","center");
        tc_SNo.RowSpan = 2;
        Literal lit_SNo =
            new Literal();
        lit_SNo.Text = "S.No";
        tc_SNo.BorderColor = System.Drawing.Color.Black;
        tc_SNo.Controls.Add(lit_SNo);
        tc_SNo.Attributes.Add("Class", "rptCellBorder");
        tr_header.Cells.Add(tc_SNo);



        TableCell tc_DR_Name = new TableCell();
        tc_DR_Name.BorderStyle = BorderStyle.Solid;
        tc_DR_Name.BorderWidth = 1;
        tc_DR_Name.Width = 250;
tc_DR_Name.Style.Add("text-align","center");
        tc_DR_Name.RowSpan = 2;
        Literal lit_DR_Name = new Literal();
        lit_DR_Name.Text = "Product Name";
        tc_DR_Name.BorderColor = System.Drawing.Color.Black;
        tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
        tc_DR_Name.Controls.Add(lit_DR_Name);
        tr_header.Cells.Add(tc_DR_Name);

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);


        ViewState["months"] = months;
        ViewState["cmonth"] = cmonth;
        ViewState["cyear"] = cyear;

        //SalesForce sal = new SalesForce();

        if (months >= 0)
        {
            for (int j = 1; j <= months + 1; j++)
            {
                TableCell tc_month = new TableCell();
                tc_month.ColumnSpan = 2;
                tc_month.RowSpan = 1;
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


                //tbl.Rows.Add(tr_catg); 

                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;

                }

            }


        }
        tbl.Rows.Add(tr_header);
        TableRow tr_catg = new TableRow();
        tr_catg.BorderStyle = BorderStyle.Solid;
        tr_catg.BorderWidth = 1;
        tr_catg.BackColor = System.Drawing.Color.FromName("#0097AC");
        tr_catg.Style.Add("Color", "White");
        tr_catg.BorderColor = System.Drawing.Color.Black;
        if (months >= 0)
        {
            for (int j = 1; j <= months + 1; j++)
            {

                TableCell tc_DR_Codee = new TableCell();
                tc_DR_Codee.BorderStyle = BorderStyle.Solid;
                tc_DR_Codee.BorderWidth = 1;
                tc_DR_Codee.Width = 200;
                //tc_DR_Code.RowSpan = 1;
tc_DR_Codee.Style.Add("text-align","center");
                Literal lit_DR_Codee = new Literal();
                lit_DR_Codee.Text = "Value";
                tc_DR_Codee.Controls.Add(lit_DR_Codee);
                tc_DR_Codee.Attributes.Add("Class", "rptCellBorder");
                tc_DR_Codee.BorderColor = System.Drawing.Color.Black;
                //tc_DR_Code.Visible = false;
                tr_catg.Cells.Add(tc_DR_Codee);
                TableCell tc_DR_Code_monthe = new TableCell();
                tc_DR_Code_monthe.BorderStyle = BorderStyle.Solid;
                tc_DR_Code_monthe.BorderWidth = 1;
                tc_DR_Code_monthe.Width = 200;
tc_DR_Code_monthe.Style.Add("text-align","center");
                //tc_DR_Code_month.RowSpan = 1;
                Literal lit_DR_Code_mone = new Literal();
                lit_DR_Code_mone.Text = "%Distribution";
                tc_DR_Code_monthe.Controls.Add(lit_DR_Code_mone);
                tc_DR_Code_monthe.Attributes.Add("Class", "rptCellBorder");
                tc_DR_Code_monthe.BorderColor = System.Drawing.Color.Black;
                //tc_DR_Code.Visible = false;
                tr_catg.Cells.Add(tc_DR_Code_monthe);

                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;

                }

            }

            tbl.Rows.Add(tr_catg);
        }
        SalesForce sal = new SalesForce();

        int iCount = 0;
        //string iTotLstCount ="0";


        TableRow tr_det = new TableRow();
        iCount += 1;


        //S.No
        TableCell tc_det_SNo = new TableCell();
        Literal lit_det_SNo = new Literal();
        lit_det_SNo.Text =  iCount.ToString() ;
        tc_det_SNo.BorderStyle = BorderStyle.Solid;
        tc_det_SNo.BorderWidth = 1;
tc_det_SNo.Style.Add("text-align","right");
        tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
        tc_det_SNo.Controls.Add(lit_det_SNo);
        tr_det.Cells.Add(tc_det_SNo);
        tr_det.BackColor = System.Drawing.Color.White;

        //SF_code
        TableCell tc_det_usr = new TableCell();
        Literal lit_det_usr = new Literal();

        lit_det_usr.Text = product_code;
        tc_det_usr.BorderStyle = BorderStyle.Solid;
        tc_det_usr.BorderWidth = 1;
        tc_det_usr.Visible = false;
        tc_det_usr.Attributes.Add("Class", "rptCellBorder");
        tc_det_usr.Controls.Add(lit_det_usr);
        tr_det.Cells.Add(tc_det_usr);

        //SF Name
        TableCell tc_det_FF = new TableCell();
        tc_det_FF.Attributes.Add("style", "color:Blue;");
        tc_det_FF.Width = 300;
        Literal lit_det_FF = new Literal();
        lit_det_FF.Text = product_name;
        tc_det_FF.BorderStyle = BorderStyle.Solid;
        tc_det_FF.BorderWidth = 1;
        tc_det_FF.Attributes.Add("Class", "rptCellBorder");
        tc_det_FF.Controls.Add(lit_det_FF);
        tr_det.Cells.Add(tc_det_FF);



        months = Convert.ToInt16(ViewState["months"].ToString());
        cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
        cyear = Convert.ToInt16(ViewState["cyear"].ToString());


        if (months >= 0)
        {

            for (int j = 1; j <= months + 1; j++)
            {
   Monthsub = sf.getMonthName(cmonth.ToString()) + "-" + cyear;

                if (cmonth == 12)
                {
                    sCurrentDate = "01-01-" + (cyear + 1);
                }

                else
                {
                    sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                }
                stCrtDtaPnt += "{label:\"" + Convert.ToString(Monthsub) + "\",y:";

                dtCurrent = Convert.ToDateTime(sCurrentDate);
                TableCell tc_det_last6monthsum = new TableCell();
                tc_det_last6monthsum.Width = 200;

                tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_det_sum = new Literal();
                //lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                tc_det_last6monthsum.BorderWidth = 1;
                tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                tc_det_last6monthsum.Controls.Add(lit_det_sum);
                tr_det.Cells.Add(tc_det_last6monthsum);

                dsDoc = sf.Sales_Trend_analysis_Productwise_monthwise(product_code, divcode, cmonth, cyear);


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_dr != "" && tot_dr != "0")
                {
                    value1 = (Decimal.Parse(tot_dr));
                    lit_det_sum.Text = value1.ToString("0.00");
                    stCrtDtaPnt += Convert.ToString(value1) + "},";

                }
                else
                {
                    lit_det_sum.Text = "";
                    stCrtDtaPnt += "0},";
                }

                TableCell tc_det_currentmonth = new TableCell();
                tc_det_currentmonth.Width = 200;

                tc_det_currentmonth.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_det_mon = new Literal();
                //lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                tc_det_currentmonth.BorderStyle = BorderStyle.Solid;
                tc_det_currentmonth.BorderWidth = 1;
                tc_det_currentmonth.Attributes.Add("Class", "rptCellBorder");
                tc_det_currentmonth.Controls.Add(lit_det_mon);
                tr_det.Cells.Add(tc_det_currentmonth);

                dsDoc = sf.Sales_Trend_analysis_Productwise_monthwise_total(subdivision, divcode, cmonth, cyear);


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_value = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_value != "" && tot_value != "0")
                {
                    value2 = (Decimal.Parse(tot_value));
                    val = (Convert.ToDecimal(value1 / value2) * 100);
                    decimal value = Math.Round(val, 0);
                    lit_det_mon.Text = value.ToString("0.00");

                }
                else
                {
                    lit_det_mon.Text = "";
                }

                //val =tot_dr) / (tot_value);

                tbl.Rows.Add(tr_det);
                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }

                tot_dr = "0";
                tot_value = "0";
                value1 = 0;
                value2 = 0;
                //val = 0;

            }

        }
        string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);


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
        pnlContents.Parent.Controls.Add(frm);
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
                Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
            }

        }
    }



}