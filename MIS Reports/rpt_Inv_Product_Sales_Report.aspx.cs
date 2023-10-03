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



public partial class MIS_Reports_rpt_Inv_Product_Sales_Report : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    decimal iTotLstCount2 = 0;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string stype = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;

    DataSet dsSalesForce = new DataSet();

    DataSet dsMGR = new DataSet();
    DataSet dsMr = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string tot_dr = string.Empty;
    string tot_Drr = string.Empty;

    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;
    string product_name = string.Empty;
    string product_code = string.Empty;
    DataSet dsprd = new DataSet();
    string sCurrentDate = string.Empty;
    string stockist_code = string.Empty;
    string stURL = string.Empty;
    string Stock_name = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        //sfCode = Request.QueryString["SF_Code"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        product_code = Request.QueryString["ProductCode"].ToString();
        product_name = Request.QueryString["ProductName"].ToString();
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);
        Prod.Text = product_name;
        lblHead.Text = " INVOICE PRODUCT WISE SALES REPORT  " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;

        FillSF();
    }

    private void FillSF()
    {
        sfCode = "Admin";
        GV_DATA.DataSource = null;
        GV_DATA.DataBind();
        ListedDR ldrr = new ListedDR();
        DataSet DsRoute = ldrr.Get_sale_rep1(divcode, sfCode, FYear, FMonth, TYear, TMonth, product_code);

        ListedDR ldr = new ListedDR();
        DataSet DsRetailer = ldr.Get_sale_rep(divcode, sfCode, FYear, FMonth, TYear, TMonth, product_code);

        DataTable dsData = new DataTable();
        dsData.Columns.Add("codes", typeof(string));
        dsData.Columns.Add("Date", typeof(string));
        dsData.Columns.Add("Sale Execution", typeof(string));
        dsData.Columns.Add("Inv No:", typeof(string));
        dsData.Columns.Add("Qty", typeof(decimal));
        dsData.Columns.Add("Sample Stock", typeof(decimal));
        dsData.Columns.Add("Amount", typeof(decimal));

        int i = 1;
        //dsData.Rows.Add("1", "Order Given Customers");
        //dsData.Rows.Add("1", "Order Total");

        decimal Qty_ot = 0;
        decimal Sample_Stock_ot = 0;
        decimal Amount_ot = 0;


        int Qty_count = 0;
        int Sample_Stock_count = 0;
        int Amount_count = 0;


        foreach (DataRow dr in DsRoute.Tables[0].Rows)
        {
            decimal Qty_tot = 0;
            decimal Sample_Stock_tot = 0;
            decimal Amount_tot = 0;

            dsData.Rows.Add("0", "FieldForce:- " + (i++) + " " + dr["Sf_Name"].ToString());
            DataRow[] drow = DsRetailer.Tables[0].Select("SF_Code = '" + dr["SF_Code"].ToString() + "'");
            if (drow.Length > 0)
            {

                foreach (DataRow row in drow)
                {
                    decimal jan_val = row["qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["qty"]);
                    Qty_tot += jan_val;
                    if (jan_val > 0)
                    {
                        Qty_count++;
                    }

                    decimal feb_val = row["Sample_stk"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Sample_stk"]);
                    Sample_Stock_tot += feb_val;
                    if (feb_val > 0)
                    {
                        Sample_Stock_count++;
                    }

                    decimal mar_val = row["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Amount"]);
                    Amount_tot += mar_val;
                    if (mar_val > 0)
                    {
                        Amount_count++;
                    }


                    dsData.Rows.Add("", row["Date"].ToString(), row["Sf_Name"].ToString(), row["Trans_Inv_Slno"].ToString(), jan_val, feb_val, mar_val);

                }
            }
            Qty_ot += Qty_tot;
            Sample_Stock_ot += Sample_Stock_tot;
            Amount_ot += Amount_tot;

            dsData.Rows.Add("1", "", "Total", "", Qty_tot, Sample_Stock_tot, Amount_tot);
        }


        GV_DATA.DataSource = dsData;
        GV_DATA.DataBind();

    }

    protected void Dgv_SKU_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridViewRow grv = e.Row;


            if (grv.Cells[0].Text.Equals("0"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#39435C");
                e.Row.ForeColor = System.Drawing.Color.FromName("#fff");
                e.Row.Font.Bold = true;
                e.Row.Cells[1].ColumnSpan = 6;
                ////e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;


            }
            if (grv.Cells[0].Text.Equals("1"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#6b7794");
                e.Row.ForeColor = System.Drawing.Color.FromName("#fff");
                e.Row.Font.Bold = true;

            }
            if (grv.Cells[1].Text.Equals("Total"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#99FFFF");
                e.Row.Font.Bold = true;

            }

            e.Row.Cells[0].Visible = false;
            e.Row.Cells[0].Width = 250;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[1].Width = 250;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].Width = 250;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        }
        catch (Exception ex)
        { }

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
        // Response.Write("Purchase_Register_Distributor_wise.aspx");

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
                Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 0f);
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