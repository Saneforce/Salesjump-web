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
using iTextSharp.tool.xml;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;


public partial class MIS_Reports_rpt_purchase_lost_selmonth : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string sMode = string.Empty;
    string sf_type = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string Subdivision = string.Empty;
    string tot_dr = string.Empty;
    string Monthsub = string.Empty;
    DataSet dsprd = new DataSet();

    string sCurrentDate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfCode = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        divcode = Session["div_code"].ToString();

        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        Subdivision = Request.QueryString["Subdivision"].ToString();

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Lost Product  Details for  " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;


        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;

        if (sf_type == "1")
        {
            FillSF_MR();
        }
        else if (sf_type == "2")
        {
            FillSF();
        }
        else if (sf_type == "3")
        {
            FillSF();
        }

    }
    private void FillSF_MR()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.GetDistNamewise_MR(divcode, Subdivision, sfCode);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#496a9a");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
tc_SNo.Style.Add("text-align","center");
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
tc_DR_Code.Style.Add("text-align","center");
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "Distributor Code";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
tc_DR_Name.Style.Add("text-align","center");
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Distributor Name";
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

            SalesForce sal = new SalesForce();


            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = 1;
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
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }


            tbl.Rows.Add(tr_header);
            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());



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
                lit_det_SNo.Text =  iCount.ToString() ;
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
tc_det_SNo.Style.Add("text-align","right");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Stockist_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);


                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 200;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
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

                        if (cmonth == 12)
                        {
                            sCurrentDate = "01-01-" + (cyear + 1);
                        }

                        else
                        {
                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                        }

                        dtCurrent = Convert.ToDateTime(sCurrentDate);


                        dsDoc = sf.lost_Purchase_value_stockist(drFF["Stockist_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate, Subdivision);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        TableCell tc_lst_month = new TableCell();
                        HyperLink hyp_lst_month = new HyperLink();

                        if (tot_dr != "0")
                        {
                            hyp_lst_month.Text = tot_dr=="" ? tot_dr : Convert.ToDecimal(tot_dr).ToString("0.00");
                            sURL = "rpt_lost_purchase_view.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + cyear + "&FMonth=" + cmonth + " &TYear=" + TYear + "&TMonth=" + TMonth + "&subdivision=" + Subdivision + "&sCurrentDate=" + sCurrentDate + "";
                            hyp_lst_month.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                            hyp_lst_month.NavigateUrl = "#";
                            if (tot_dr != "")
                            {

                                //iTotLstCount1 += (int.Parse(tot_dr));
                                //iTotLstCount2 += (int.Parse(tot_dr));
                            }







                        }

                        else
                        {
                            hyp_lst_month.Text = "";
                        }

                        tc_lst_month.BorderStyle = BorderStyle.Solid;
                        tc_lst_month.BorderWidth = 1;

                        tc_lst_month.BackColor = System.Drawing.Color.White;
                        tc_lst_month.Width = 420;
                        tc_lst_month.HorizontalAlign = HorizontalAlign.Right;
                        tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                        tc_lst_month.Controls.Add(hyp_lst_month);
                        tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_lst_month);




                        tot_dr = "0";

                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }


                    }


                }

                tbl.Rows.Add(tr_det);

            }






        }

    }
    private void FillSF()
    {

        string t = "1";
        int mnth = Convert.ToInt32(FMonth), year = Convert.ToInt32(FYear);
        DateTime dt = Convert.ToDateTime(year + "-" + mnth.ToString() + "-" + t.ToString());

        string st = dt.ToString("yyyy-MM-dd ");

        DateTime now = DateTime.Now;
        var startDate = new DateTime(Convert.ToInt32(TYear), Convert.ToInt32(TMonth), 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);
       
       
       

        string tom = endDate.ToString("yyyy-MM-dd ");


        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        if (sf_type == "1" || sf_type == "2")
        {
            dsSalesForce = sf.GetStockName_Customer(divcode, Subdivision, sfCode);
        }
        else
        {
            dsSalesForce = sf.GetStockName_Customer(divcode, Subdivision);
        }


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#496a9a");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
tc_SNo.Style.Add("text-align","center");
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
tc_DR_Code.Style.Add("text-align","center");
            tc_DR_Code.RowSpan = 1;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "Distributor Code";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
            tc_DR_Name.RowSpan = 1;
tc_DR_Name.Style.Add("text-align","center");
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Distributor Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);







            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            //int cmonth = Convert.ToInt32(FMonth);
            //int cyear = Convert.ToInt32(FYear);


            //ViewState["months"] = months;
            //ViewState["cmonth"] = cmonth;
            //ViewState["cyear"] = cyear;

            //SalesForce sal = new SalesForce();


            //if (months >= 0)
            //{
            //    for (int j = 1; j <= months + 1; j++)
            //    {
                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = 1;
                    Literal lit_month = new Literal();
                   
                    lit_month.Text = "Non-Purchased Product";
                    tc_month.Attributes.Add("Class", "rptCellBorder");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                  
            //        if (cmonth == 13)
            //        {
            //            cmonth = 1;
            //            cyear = cyear + 1;
            //        }
            //    }
            //}


            tbl.Rows.Add(tr_header);
            //Sub Header
            //months = Convert.ToInt16(ViewState["months"].ToString());
            //cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            //cyear = Convert.ToInt16(ViewState["cyear"].ToString());



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
                lit_det_SNo.Text = iCount.ToString();
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
                lit_det_usr.Text = "&nbsp;" + drFF["Stockist_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);


                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 200;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);









               // dsDoc = sf.lost_Purchase_value_stockist_purchase(drFF["Stockist_Code"].ToString(), divcode, st, tom, Subdivision);
				  dsDoc = sf.lost_Purchase_value_stockist_purchase( stockistcode: drFF["Stockist_Code"].ToString(), div_code: divcode, fdate:st, todate: tom, subdivision: Subdivision, sf_code:sfCode);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr =dsDoc.Tables[0].Rows.Count.ToString(); // dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        TableCell tc_lst_month = new TableCell();
                        HyperLink hyp_lst_month = new HyperLink();

                        if (tot_dr != "0")
                        {
                            hyp_lst_month.Text = tot_dr=="" ? tot_dr:Convert.ToDecimal( tot_dr).ToString();

                            sURL = "rpt_purchase_sel_month_view.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + " &TYear=" + TYear + "&TMonth=" + TMonth + "&subdivision=" + Subdivision + "&tdate=" + tom + "&fdate=" + st + "";
                            hyp_lst_month.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                            hyp_lst_month.NavigateUrl = "#";
                            if (tot_dr != "")
                            {

                                //iTotLstCount1 += (int.Parse(tot_dr));
                                //iTotLstCount2 += (int.Parse(tot_dr));
                            }
                        }

                        else
                        {
                            hyp_lst_month.Text = "0";
                        }

                        tc_lst_month.BorderStyle = BorderStyle.Solid;
                        tc_lst_month.BorderWidth = 1;

                        tc_lst_month.BackColor = System.Drawing.Color.White;
                        tc_lst_month.Width = 420;
                        tc_lst_month.HorizontalAlign = HorizontalAlign.Right;
                        tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                        tc_lst_month.Controls.Add(hyp_lst_month);
                        tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_lst_month);




                        tot_dr = "0";

                      

                  

                

                tbl.Rows.Add(tr_det);

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



    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}