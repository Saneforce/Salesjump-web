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
using iTextSharp.tool.xml;
using System.Text;
using Bus_EReport;
using System.Net;


public partial class MIS_Reports_rpt_primary_vs_secondary : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    decimal iTotLstCount2 = 0;
    string stype = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;   
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent; 
    string Stock_name = string.Empty;
    string stockist_code = string.Empty;
    string code = string.Empty;
    string tot_dr = string.Empty;
     decimal ilist=0;
    decimal ilistt=0;
    string tot_Drrr = string.Empty;
    string subdivision=string.Empty;
    string Monthsub = string.Empty; 
   string fdforce=string.Empty; 
    string sCurrentDate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
sfCode = Session["sf_code"].ToString();

        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();        
        Stock_name = Request.QueryString["stockist_name"];
         fdforce= Request.QueryString["Fieldforcename"];
        code=Request.QueryString["scode"];
        stockist_code = code.Trim();
        subdivision=Request.QueryString["subdivision"];
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Primary Vs Secondary Value  " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;
        dist.Text = Stock_name;
         fdname.Text=fdforce;
        FillSF();

    }

    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        string data=string.Empty;
        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Getproductsubdivision_wise(divcode,subdivision);



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
            tc_SNo.Width = 20;
            tc_SNo.RowSpan = 2;
tc_SNo.HorizontalAlign = HorizontalAlign.Center;
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
tc_DR_Code.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "Product Code";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
            tc_DR_Name.RowSpan = 2;
			tc_DR_Name.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Product Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);
            tbl.Rows.Add(tr_header);



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
                    tc_month.ColumnSpan = 2;
                    //tc_month.RowSpan = ;
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
                }            }

            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());



            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;
            TableRow tb = new TableRow();
            tb.BackColor = System.Drawing.Color.FromName("#0097AC");
            tb.Style.Add("Color", "White");
            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {
          
            TableCell tc_catg_namee = new TableCell();
            tc_catg_namee.BorderStyle = BorderStyle.Solid;
            tc_catg_namee.BorderWidth = 1;
            //text-align: center;


            Literal lit_catg_namee = new Literal();
            lit_catg_namee.Text = "Purchase (Rs)";

            tc_catg_namee.Attributes.Add("Class", "rptCellBorder");
            tc_catg_namee.HorizontalAlign = HorizontalAlign.Center;
            tc_catg_namee.Controls.Add(lit_catg_namee);
            tb.Cells.Add(tc_catg_namee);
            //tr_header.Cells.Add(tc_catg_namee);
            TableCell tc_catg_sale = new TableCell();
            tc_catg_sale.BorderStyle = BorderStyle.Solid;
            tc_catg_sale.BorderWidth = 1;
            //text-align: center;


            Literal lit_catg_sale = new Literal();
            lit_catg_sale.Text = "Sale (Rs)";

            tc_catg_sale.Attributes.Add("Class", "rptCellBorder");
            tc_catg_sale.HorizontalAlign = HorizontalAlign.Center;
            tc_catg_sale.Controls.Add(lit_catg_sale);
            tb.Cells.Add(tc_catg_sale);
            //tr_catg.Cells.Add(tc_catg_namee);
            //tr_header.Cells.Add(tc_catg_sale);
            //tbl.Rows.Add(tb);
            tbl.Rows.Add(tb);
            cmonth = cmonth + 1;
            if (cmonth == 13)
            {
                cmonth = 1;
                cyear = cyear + 1;
            }
                }
            }
            int iCount = 0;
            //string iTotLstCount ="0";
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;


                //S.No
                TableCell tc_det_SNo = new TableCell();
                tc_det_SNo.Width = 20;
 tc_det_SNo.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString();
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Product_Detail_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 250;

                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Product_Detail_Name"].ToString();
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


                        dsDoc = sf.primary_Purchase_Distributor_value(stockist_code, divcode, cmonth, cyear, sCurrentDate, drFF["Product_Detail_Code"].ToString(), subdivision);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        TableCell tc_lst_month = new TableCell();
                        HyperLink hyp_lst_month = new HyperLink();

                        if (tot_dr != "0")
                        {
                            try 
                            {
                                Decimal per = Math.Round(Convert.ToDecimal(tot_dr), 2);
                                hyp_lst_month.Text = per.ToString("0.00");
                            }
                            catch
                            {
                            hyp_lst_month.Text =tot_dr=="" ? tot_dr:Convert.ToDecimal(tot_dr).ToString("0.00");
                            }
                            if (tot_dr != "")
                            {

                                iTotLstCount1 += (decimal.Parse(tot_dr));
                                iTotLstCount2 += (decimal.Parse(tot_dr));
                            }



                            stype = "1";
                            stURL = "rpt_value_primary_vs_secondary_sv.aspx?Stockist_Code=" + stockist_code + "&Product_Code=" + drFF["Product_Detail_Code"] + "&Stockist_name=" + Stock_name + " &Year=" + cyear + "&Month=" + cmonth + "&fldforcename="+fdforce+"&subdivision=" + subdivision+ "&stype=" + stype + "";
                            hyp_lst_month.Attributes["onclick"] = "javascript:window.open('" + stURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                            hyp_lst_month.NavigateUrl = "#";


                        }

                        else
                        {
                            hyp_lst_month.Text = "0.00";
                        }

                        tc_lst_month.BorderStyle = BorderStyle.Solid;
                        tc_lst_month.BorderWidth = 1;

                        tc_lst_month.BackColor = System.Drawing.Color.White;
                        tc_lst_month.Width = 200;
                        tc_lst_month.HorizontalAlign = HorizontalAlign.Right;
                        tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                        tc_lst_month.Controls.Add(hyp_lst_month);
                        tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_lst_month);
                        dsDoc = sf.secondary_Purchase_Distributor_value(stockist_code, divcode, cmonth, cyear, sCurrentDate, drFF["Product_Detail_Code"].ToString(),subdivision);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_Drrr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        TableCell sale_unit = new TableCell();
                        HyperLink sale_unit_value = new HyperLink();

                        if (tot_Drrr != "0")
                        {
                            try
                            {
                                Decimal per = Math.Round(Convert.ToDecimal(tot_Drrr), 2);
                                sale_unit_value.Text = per.ToString("0.00");
                            }
                            catch
                            {
                                sale_unit_value.Text = tot_dr=="" ?tot_dr:Convert.ToDecimal(tot_dr).ToString("0.00");
                            }
                            //sale_unit_value.Text = tot_Drrr;
                            if (tot_Drrr != "")
                            {

                                iTotLstCount1 += (decimal.Parse(tot_Drrr));
                               // iTotLstCount2 += (decimal.Parse(tot_Drrr));
                            }



                            stype = "2";
                            stURL = "rpt_value_primary_vs_secondary_sv.aspx?Stockist_Code=" + stockist_code + "&Product_Code=" + drFF["Product_Detail_Code"] + "&fldforcename="+ fdforce +"&Stockist_name=" + Stock_name + " &Year=" + cyear + "&Month=" + cmonth + "&subdivision=" + subdivision+ "&stype=" + stype + "";
                            sale_unit_value.Attributes["onclick"] = "javascript:window.open('" + stURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                            sale_unit_value.NavigateUrl = "#";


                        }

                        else
                        {
                            sale_unit_value.Text = "0.00";
                        }

                        sale_unit.BorderStyle = BorderStyle.Solid;
                        sale_unit.BorderWidth = 1;

                        sale_unit.BackColor = System.Drawing.Color.White;
                        sale_unit.Width = 200;
                        sale_unit.HorizontalAlign = HorizontalAlign.Right;
                        sale_unit.VerticalAlign = VerticalAlign.Middle;
                        sale_unit.Controls.Add(sale_unit_value);
                        sale_unit.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(sale_unit);




                        //tot_dr = "0";

                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }


                    }
                   
                    iTotLstCount1 = 0;
                }

                tbl.Rows.Add(tr_det);

            }

            TableRow tr_total = new TableRow();

            TableCell tc_Count_Total = new TableCell();
            tc_Count_Total.BorderStyle = BorderStyle.Solid;
            tc_Count_Total.BorderWidth = 1;
            HyperLink lit_Count_Total = new HyperLink();
            lit_Count_Total.Text = "Total";
            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_Count_Total.Controls.Add(lit_Count_Total);
            tc_Count_Total.Font.Bold.ToString();
            tc_Count_Total.BackColor = System.Drawing.Color.White;
            tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
            tc_Count_Total.ColumnSpan = 2;
            tc_Count_Total.Style.Add("text-align", "center");
            tc_Count_Total.Style.Add("font-family", "Calibri");
            tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
            tc_Count_Total.Style.Add("font-size", "10pt");

            tr_total.Cells.Add(tc_Count_Total);



            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());

            if (months >= 0)
            {


                for (int j = 1; j <= months + 1; j++)
                {
 string cm = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
 data += "{label:\"" + Convert.ToString(cm) + "\",y:";
                    if (cmonth == 12)
                    {
                        sCurrentDate = "01-01-" + (cyear + 1);
                    }
                    else
                    {
                        sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                    }
          stCrtDtaPnt += "{label:\"" + Convert.ToString(cm) + "\",y:";
            
                    dtCurrent = Convert.ToDateTime(sCurrentDate);

                    TableCell tc_tot_month = new TableCell();
                    HyperLink hyp_month = new HyperLink();

                    foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                    {

                           dsDoc = sf.primary_Purchase_Distributor_value(stockist_code, divcode, cmonth, cyear, sCurrentDate, drFF["Product_Detail_Code"].ToString(),subdivision);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        if (tot_dr != "")
                        {
                            ilistt += (Decimal.Parse(tot_dr));
                            Decimal per = Math.Round(Convert.ToDecimal(ilistt.ToString()), 2);
                            hyp_month.Text = per.ToString("0.00");
                            //hyp_month.Text = ilistt.ToString();
                 
                            stype = "3";
                            stURL = "rpt_value_primary_vs_secondary.aspx?Stockist_Code=" + stockist_code + "&Product_Code=" + drFF["Product_Detail_Code"] + "&Stockist_name=" + Stock_name + " &Year=" + cyear + "&Month=" + cmonth + "&subdivision=" + subdivision + "&stype=" + stype + "";
                            hyp_month.Attributes["onclick"] = "javascript:window.open('" + stURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                            hyp_month.NavigateUrl = "#";


                        }
  
                    }

  stCrtDtaPnt+=Convert.ToString(ilistt) + "},";
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
                    //iTotLstCount = "0";
                    ilistt = 0;


 TableCell tc_tot_month_sale = new TableCell();
                    HyperLink hyp_month_sale = new HyperLink();

                    foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                    {

                           dsDoc = sf.secondary_Purchase_Distributor_value(stockist_code, divcode, cmonth, cyear, sCurrentDate, drFF["Product_Detail_Code"].ToString(),subdivision);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        if (tot_dr != "")
                        {
                            ilist += (Decimal.Parse(tot_dr));
                            Decimal per = Math.Round(Convert.ToDecimal(ilist.ToString()), 2);
                            hyp_month_sale.Text = per.ToString("0.00");
                            //hyp_month_sale.Text = ilist.ToString();

stype = "4";
                            stURL = "rpt_value_primary_vs_secondary.aspx?Stockist_Code=" + stockist_code + "&Product_Code=" + drFF["Product_Detail_Code"] + "&Stockist_name=" + Stock_name + " &Year=" + cyear + "&Month=" + cmonth + "&subdivision=" + subdivision + "&stype=" + stype + "";
                            hyp_month_sale.Attributes["onclick"] = "javascript:window.open('" + stURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                            hyp_month_sale.NavigateUrl = "#";



                        }

                    }

                    data+=Convert.ToString(ilist) + "},";
                    tc_tot_month_sale.BorderStyle = BorderStyle.Solid;
                    tc_tot_month_sale.BorderWidth = 1;
                    tc_tot_month_sale.BackColor = System.Drawing.Color.White;
                    tc_tot_month_sale.Width = 200;
                    tc_tot_month_sale.Style.Add("font-family", "Calibri");
                    tc_tot_month_sale.Style.Add("font-size", "10pt");
                    tc_tot_month_sale.HorizontalAlign = HorizontalAlign.Right;
                    tc_tot_month_sale.VerticalAlign = VerticalAlign.Middle;
                    tc_tot_month_sale.Controls.Add(hyp_month_sale);
                    tc_tot_month_sale.Attributes.Add("style", "font-weight:bold;");
                    tc_tot_month_sale.Attributes.Add("Class", "rptCellBorder");
                    tr_total.Cells.Add(tc_tot_month_sale);
                    //iTotLstCount = "0";
                    ilist = 0;







            cmonth = cmonth + 1;
            if (cmonth == 13)
            {
                cmonth = 1;
                cyear = cyear + 1;
            }

        }

 }

            tbl.Rows.Add(tr_total);


        }
string scrpt = "arr=[" + stCrtDtaPnt + "];arr1=[" + data + "];window.onload = function () {genChart(arr,arr1);}";
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