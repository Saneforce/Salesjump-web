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

public partial class MIS_Reports_rptpurchaseview : System.Web.UI.Page
{
    DataSet dsProduct = null;
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
    string distri = string.Empty;
    string distributor = string.Empty;
    string stockist = string.Empty;
    string Year = string.Empty;
    string date = string.Empty;
    string Stock_name = string.Empty;
    string sf_name = string.Empty;
    string type = string.Empty;
    string sCurrentDate = string.Empty;
    string Sf_Code_multiple = string.Empty;
    string sURL = string.Empty;
    string tot_dr = string.Empty;
    string subdivision = string.Empty;
    string strFMonthName = string.Empty;
    int iCount = -1;
    DataSet dsDoc = null;
    int iTotLstCount = 0;
    int FYear = 0;
    int FMonth = 0;
     string gg = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        //date = Request.QueryString["Date"].ToString();
      
        distributor = Request.QueryString["Distributor"].ToString();
        distri = Request.QueryString["Dist_Name"];
        subdivision = Request.QueryString["subdivision"];
        lblRegionName.Text = sf_name;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString();
        gg = Request.QueryString["DATE"].ToString();
        date = gg.Trim();
        //date = "2016-09-19";

        lblHead.Text = "Purchase View for   " + date;

        //lblIDMonth.Visible = false;
        //lblIDYear.Visible = false;
        dist.Text = distri;

        FillSF_Leave_Type();


    }
    private void FillSF_Leave_Type()
    {
        TableRow tr_det_head = new TableRow();
        tr_det_head.BorderStyle = BorderStyle.Solid;
        tr_det_head.BorderWidth = 1;
        tr_det_head.BackColor = System.Drawing.Color.FromName("#496a9a");
        tr_det_head.Style.Add("Color", "White");
        tr_det_head.BorderColor = System.Drawing.Color.Black;

        SalesForce sf = new SalesForce();


        //getweek = sf.Secondary_sales_get_no_of_week(FYear, strFMonthName);

        //TableCell leave_utility = new TableCell();
        //leave_utility.BorderStyle = BorderStyle.Solid;
        //leave_utility.BorderWidth = 1;
        //leave_utility.Width = 300;

        //leave_utility.ColumnSpan = 3;
        //Literal lit_DR_utility = new Literal();

        //lit_DR_utility.Text = "<center>Weekly View</center>";
        //leave_utility.BorderColor = System.Drawing.Color.Black;
        //leave_utility.Attributes.Add("Class", "rptCellBorder");
        //leave_utility.Controls.Add(lit_DR_utility);
        //tr_det_head.Cells.Add(leave_utility);

        //int df = Convert.ToInt32(getweek.Tables[0].Rows[0].ItemArray.GetValue(0));
        ////  tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");
        //int wee = df + 1;

        //if (getweek.Tables[0].Rows.Count > 0)
        //{
        //    for (int i = 1; i < wee; i++)
        //    {
              
            //}

            //tbl.Rows.Add(tr_catg);
        //}
        tbl.Rows.Add(tr_det_head);






        SalesForce dr = new SalesForce();
        dsDr = dr.Secondary_sales_productdetail(div_code, subdivision);

        if (dsDr.Tables[0].Rows.Count > 0)
        {
            TableRow tr_catg = new TableRow();
            tr_catg.BackColor = System.Drawing.Color.FromName("#496a9a");
            tr_catg.Style.Add("Color", "White");



            TableCell tc_det_head_SNo = new TableCell();
            tc_det_head_SNo.Width = 50;
            tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_head_SNo.BorderWidth = 1;
            Literal lit_det_head_SNo = new Literal();
            lit_det_head_SNo.Text = "<b>S.No</b>";
            tc_det_head_SNo.Attributes.Add("Class", "tblHead");
            tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
            tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
            tr_catg.Cells.Add(tc_det_head_SNo);


            TableCell tc_det_head_doc = new TableCell();
            tc_det_head_doc.Width = 180;
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
            //if (getweek.Tables[0].Rows.Count > 0)
            //{

                //for (int i = 1; i < wee; i++)
                //{


                    TableCell tc_det_head_Qua = new TableCell();

                    tc_det_head_Qua.BorderStyle = BorderStyle.Solid;
                    tc_det_head_Qua.BorderWidth = 1;
                    Literal lit_det_head_qua = new Literal();
                    lit_det_head_qua.Text = "<b>Purchase Qty</b>";
                    tc_det_head_Qua.Attributes.Add("Class", "tblHead");
                    tc_det_head_Qua.Controls.Add(lit_det_head_qua);
                    tc_det_head_Qua.HorizontalAlign = HorizontalAlign.Center;
                    tr_catg.Cells.Add(tc_det_head_Qua);

                                   


                 

                    tbl.Rows.Add(tr_catg);



                //}
            //}



            iCount = 0;
            foreach (DataRow drdoctor in dsDr.Tables[0].Rows)
            {
                TableRow tr_det_sno = new TableRow();
                TableCell tc_det_SNo = new TableCell();
                iCount += 1;
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text =  iCount.ToString() ;
                if (iCount == 90)
                {
                }
                //   tc_det_SNo.Attributes.Add("Class", "tblRow");
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
                //  tc_det_Sl_No.Attributes.Add("Class", "rptCellBorder");
                tc_det_Sl_No.Controls.Add(lit_det_Sl_No);
                tr_det_sno.Cells.Add(tc_det_Sl_No);

                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();

                lit_det_usr.Text = "&nbsp;" + drdoctor["Product_Detail_Name"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Width = 200;
                //  tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det_sno.Cells.Add(tc_det_usr);

                TableCell tc_det_sfcode = new TableCell();

                Literal lit_det_sfcode = new Literal();
                tc_det_sfcode.Width = 80;
                lit_det_sfcode.Text = "&nbsp;" + drdoctor["Product_Sale_Unit"].ToString();
                tc_det_sfcode.BorderStyle = BorderStyle.Solid;
                tc_det_sfcode.BorderWidth = 1;
                tc_det_sfcode.HorizontalAlign = HorizontalAlign.Left;
                // tc_det_sfcode.Attributes.Add("Class", "rptCellBorder");
                tc_det_sfcode.Controls.Add(lit_det_sfcode);
                tr_det_sno.Cells.Add(tc_det_sfcode);
                //string week="4";

                //if (getweek.Tables[0].Rows.Count > 0)
                //{

                //    for (int i = 1; i < wee; i++)
                //    {
                        SalesForce sd = new SalesForce();

//                        dsDrr = sd.purchase_view_detailss(div_code, date, distributor, drdoctor["Product_Detail_Code"].ToString());
 					    dsDrr = sd.purchase_view_detailss(div_code, date, distributor, drdoctor["Product_Detail_Code"].ToString(),sf_code);
                        if (dsDrr.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drrdoctor in dsDrr.Tables[0].Rows)
                            {
                                
                                TableCell tc_det_dr_hq = new TableCell();
                                tc_det_dr_hq.HorizontalAlign = HorizontalAlign.Right;
                                tc_det_dr_hq.Width = 20;
                                Literal lit_det_dr_hq = new Literal();
                                lit_det_dr_hq.Text =  drrdoctor["receipt_qty"] == DBNull.Value ? drrdoctor["receipt_qty"].ToString() : Convert.ToDecimal( drrdoctor["receipt_qty"]).ToString("0.00");
                                // tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                                tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                                tc_det_dr_hq.BorderWidth = 1;
                                tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                                tr_det_sno.Cells.Add(tc_det_dr_hq);

                             
                             
                                

                            }
                        }
                        else
                        {
                            

                            TableCell tc_det_dr_hq = new TableCell();
                            tc_det_dr_hq.HorizontalAlign = HorizontalAlign.Right;
                            tc_det_dr_hq.Width = 20;
                            Literal lit_det_dr_hq = new Literal();
                            lit_det_dr_hq.Text = "0.00";
                            // tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                            tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_hq.BorderWidth = 1;
                            tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                            tr_det_sno.Cells.Add(tc_det_dr_hq);

                           

                            
                        }


                //    }
                //}



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