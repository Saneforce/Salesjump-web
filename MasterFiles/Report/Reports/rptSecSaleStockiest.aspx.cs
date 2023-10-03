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

    
public partial class MasterFiles_Reports_rptSecSaleStockiest : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSecSales = null;
    DataSet dsSale = null;
    DataSet dsState = new DataSet();
    DataSet dsReport = null;
    string state_code = string.Empty;
    string sf_code = string.Empty;
    int FMonth = -1;
    int FYear = -1;
    int stock_code = -1;
    int iDay = -1;
    DateTime SelDate;
    string sDate = string.Empty;
    string sf_name = string.Empty;
    DataSet dssf = null;
    DataSet dsStock = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Convert.ToInt16(Request.QueryString["cmon"].ToString());
        FYear = Convert.ToInt16(Request.QueryString["cyear"].ToString());

        SalesForce sf = new SalesForce();
        dssf = sf.getSfName(sf_code);
        if (dssf.Tables[0].Rows.Count > 0)
            sf_name = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

        lblText.Text = lblText.Text + sf_name + " of " + getMonthName(FMonth) + " - " + FYear ;

        GenerateReport();

    }

    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
    }

    private void GenerateReport()
    {
        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        if (Session["sf_type"].ToString() == "1")
        {
            dssf = sf.getSecSales_MR(div_code, "admin");
        }
        else
        {
            dssf = sf.getSecSales_MR(div_code, sf_code);
        }

        if (dssf.Tables[0].Rows.Count > 0)
        {
            tbl.BorderStyle = BorderStyle.None;
            //tbl.BorderWidth = 1;

            foreach (DataRow dRow in dssf.Tables[0].Rows)
            {
                TableRow tr_sf_header = new TableRow();
                tr_sf_header.BorderStyle = BorderStyle.None;
                //tr_stock_header.BorderWidth = 1;
                tr_sf_header.Attributes.Add("Class", "Backcolor");
                TableCell tc_sf_name = new TableCell();
                tc_sf_name.BorderStyle = BorderStyle.None;
                //tc_stock_name.BorderWidth = 1;
                tc_sf_name.Width = 500;
                tc_sf_name.ColumnSpan = 5;
                Literal lit_sf_name = new Literal();
                lit_sf_name.Text = " &nbsp; &nbsp; <b> " + dRow["Sf_Name"].ToString() + "</b>";
                tc_sf_name.Attributes.Add("Class", "Backcolor");
                tc_sf_name.Controls.Add(lit_sf_name);
                tr_sf_header.Cells.Add(tc_sf_name);
                tbl.Rows.Add(tr_sf_header);

                PopulateSecSales(dRow["Sf_code"].ToString());
            }
        }
    }

    private void PopulateSecSales(string ff_code)
    {
        SecSale ss = new SecSale();

        TableRow tr_header = new TableRow();
        tr_header.BorderStyle = BorderStyle.Solid;
        tr_header.BorderWidth = 1;
        tr_header.BackColor = System.Drawing.Color.Lavender;

        TableCell tc_SNo = new TableCell();
        tc_SNo.BorderStyle = BorderStyle.Solid;
        tc_SNo.BorderWidth = 1;
        tc_SNo.Width = 10;
        //tc_SNo.RowSpan = 2;
        Literal lit_SNo = new Literal();
        lit_SNo.Text = "<center>S.No</center>";
        //tc_SNo.HorizontalAlign = HorizontalAlign.Center;
        
        tc_SNo.Controls.Add(lit_SNo);
        tr_header.Cells.Add(tc_SNo);

        TableCell tc_Prod_Code = new TableCell();
        tc_Prod_Code.BorderStyle = BorderStyle.Solid;
        tc_Prod_Code.BorderWidth = 1;
        tc_Prod_Code.Width = 100;
        //tc_Prod_Code.RowSpan = 2;
        Literal lit_Prod_Code = new Literal();
        lit_Prod_Code.Text = "<center>Stock Code</center>";
        tc_Prod_Code.Attributes.Add("Class", "Backcolor");
        tc_Prod_Code.Controls.Add(lit_Prod_Code);
        tc_Prod_Code.Visible = false;
        tr_header.Cells.Add(tc_Prod_Code);

        TableCell tc_Prod_Name = new TableCell();
        tc_Prod_Name.BorderStyle = BorderStyle.Solid;
        tc_Prod_Name.BorderWidth = 1;
        tc_Prod_Name.Width = 600;
        //tc_Prod_Name.RowSpan = 2;
        Literal lit_Prod_Name = new Literal();
        lit_Prod_Name.Text = "<center>Stockiest Name</center>";
        tc_Prod_Name.Attributes.Add("Class", "Backcolor");
        tc_Prod_Name.Controls.Add(lit_Prod_Name);
        tr_header.Cells.Add(tc_Prod_Name);

        TableCell tc_Rate = new TableCell();
        tc_Rate.BorderStyle = BorderStyle.Solid;
        tc_Rate.BorderWidth = 1;
        tc_Rate.Width = 150;
        //tc_Rate.RowSpan = 2;
        Literal lit_Rate = new Literal();
        lit_Rate.Text = "<center>Entry Status</center>";
        tc_Rate.Attributes.Add("Class", "Backcolor");
        tc_Rate.Controls.Add(lit_Rate);
        tr_header.Cells.Add(tc_Rate);

        TableCell tc_Date = new TableCell();
        tc_Date.BorderStyle = BorderStyle.Solid;
        tc_Date.BorderWidth = 1;
        tc_Date.Width = 150;
        //tc_Rate.RowSpan = 2;
        Literal lit_Date = new Literal();
        lit_Date.Text = "<center>Submitted Date</center>";
        tc_Date.Controls.Add(lit_Date);
        tr_header.Cells.Add(tc_Date);

        tbl.Rows.Add(tr_header);

        //SecSale ss = new SecSale();
        if (Session["sf_type"].ToString() == "1")
        {
            dsStock = ss.getStockiestDet(ff_code);
        }
        else
        {
            dsStock = ss.getStockiestDet(ff_code);
        }


        int iCount = 0;
        string upd_date = string.Empty; 
        bool bentry = false;

        foreach (DataRow dataRow in dsStock.Tables[0].Rows)
        {
            upd_date = "";

            TableRow tr_det = new TableRow();
            tr_det.BackColor = System.Drawing.Color.White;
            tr_det.Attributes.Add("Class", "rptCellBorder");
            iCount += 1;

            TableCell tc_det_SNo = new TableCell();
            Literal lit_det_SNo = new Literal();
            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
            tc_det_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_SNo.BorderWidth = 1;
            tc_det_SNo.Width = 10;
            tc_det_SNo.Controls.Add(lit_det_SNo);
            tr_det.Cells.Add(tc_det_SNo);

            TableCell tc_det_prod_code = new TableCell();
            Literal lit_det_prod_code = new Literal();
            lit_det_prod_code.Text = "&nbsp;" + dataRow["Stockist_Code"].ToString();
            tc_det_prod_code.BorderStyle = BorderStyle.Solid;
            tc_det_prod_code.BorderWidth = 1;
            tc_det_prod_code.Controls.Add(lit_det_prod_code);
            tc_det_prod_code.Visible = false;
            tr_det.Cells.Add(tc_det_prod_code);

            TableCell tc_det_prod_name = new TableCell();
            Literal lit_det_prod_name = new Literal();
            lit_det_prod_name.Text = "&nbsp;" + dataRow["Stockist_Name"].ToString();
            tc_det_prod_name.BorderStyle = BorderStyle.Solid;
            tc_det_prod_name.BorderWidth = 1;
            tc_det_prod_name.Controls.Add(lit_det_prod_name);
            tr_det.Cells.Add(tc_det_prod_name);

            bentry = ss.getcount_ssentry(div_code, ff_code, dataRow["Stockist_Code"].ToString().Trim(), FMonth, FYear);

            TableCell tc_det_prod_rate = new TableCell();
            //Literal lit_det_prod_rate = new Literal();
            CheckBox chkSaleEntry = new CheckBox();
            if (bentry)
                chkSaleEntry.Checked = true;
            else
                chkSaleEntry.Checked = false;

            chkSaleEntry.Enabled = false;
            tc_det_prod_rate.HorizontalAlign = HorizontalAlign.Center;
            tc_det_prod_rate.BorderStyle = BorderStyle.Solid;
            tc_det_prod_rate.BorderWidth = 1;
            tc_det_prod_rate.Controls.Add(chkSaleEntry);
            tr_det.Cells.Add(tc_det_prod_rate);

            dsReport = ss.getSubmittedDate(div_code, ff_code, dataRow["Stockist_Code"].ToString().Trim(), FMonth, FYear);
            if (dsReport != null)
            {
                if (dsReport.Tables[0].Rows.Count > 0)
                    upd_date = dsReport.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }

            TableCell tc_det_date = new TableCell();
            Literal lit_det_date = new Literal();
            lit_det_date.Text = "&nbsp;" + upd_date ;
            tc_det_date.BorderStyle = BorderStyle.Solid;
            tc_det_date.BorderWidth = 1;
            tc_det_date.Controls.Add(lit_det_date);
            tr_det.Cells.Add(tc_det_date);

            tbl.Rows.Add(tr_det);

        }

        //Empty Row
        TableRow tr_empty = new TableRow();
        tr_empty.BorderStyle = BorderStyle.None;
        
        TableCell tc_empty = new TableCell();
        tc_empty.BorderStyle = BorderStyle.None;
        tc_empty.Width = 100;
        tc_empty.ColumnSpan = 4;
        Literal lit_empty = new Literal();
        lit_empty.Text = " &nbsp; &nbsp; ";
        
        tc_empty.Controls.Add(lit_empty);
        tr_empty.Height = 20;
        tr_empty.Cells.Add(tc_empty);
        tbl.Rows.Add(tr_empty);

    }


    //Get the last day for the given month & year
    private int GetLastDay(int cMonth, int cYear)
    {
        int cday = 0;

        if (cMonth == 1)
            cday = 31;
        else if (cMonth == 2)
        {
            if (cYear % 4 == 0)
                cday = 29;
            else
                cday = 28;
        }
        else if (cMonth == 3)
            cday = 31;
        else if (cMonth == 4)
            cday = 30;
        else if (cMonth == 5)
            cday = 31;
        else if (cMonth == 6)
            cday = 30;
        else if (cMonth == 7)
            cday = 31;
        else if (cMonth == 8)
            cday = 31;
        else if (cMonth == 9)
            cday = 30;
        else if (cMonth == 10)
            cday = 31;
        else if (cMonth == 11)
            cday = 30;
        else if (cMonth == 12)
            cday = 31;

        return cday;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Export.xls";
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


}