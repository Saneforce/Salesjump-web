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


public partial class MIS_Reports_RPT_RETAIL_RETAILER : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string Product_Brd_Code = string.Empty;
    string divcode = string.Empty;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    decimal iTotLstCount2 = 0;
  decimal iTotLstnetval = 0;
 
    string Product_Cat_Code = string.Empty;
    string Product_Cat_Name = string.Empty;
    string Month = string.Empty;
    string Year = string.Empty;
     string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string ROUTE = string.Empty;
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
  string sec_value = string.Empty;
string sec_total = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        divcode = Session["div_code"].ToString();
        

         FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        subdivision = Request.QueryString["Subdivision"].ToString();
        //Month = Request.QueryString["Month"].ToString();
        //Year = Request.QueryString["Year"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
          TYear = Request.QueryString["TYear"].ToString();
        ROUTE = Request.QueryString["Territory_code"].ToString();


        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        //string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);


        lblHead.Text = "Retail Trend Analysis Retailer Details  for  " + strFMonthName + " " + FYear + "To" + " " + strTMonthName + " " + TYear;


        Stockist_Code = Request.QueryString["Stockist_Code"].ToString();
            Stockist_Name = Request.QueryString["Stockist_Name"].ToString();
            FillSF();
        

       
        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;


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



      private void FillSF()
    {
        string stCrtDtaPnt = string.Empty;
        Random rndm = new Random();
        string t = "1";
        int mnth = Convert.ToInt32(FMonth), year = Convert.ToInt32(FYear);
        DateTime dt = Convert.ToDateTime(year + "-" + mnth.ToString() + "-" + t.ToString());
        string fromdate = dt.ToString("yyyy-MM-dd 00:00:00.000");
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd 23:59:59.999");

        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.GetRetailerName_Distributorwise(Stockist_Code,ROUTE,divcode);

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
            tc_SNo.RowSpan = 2;
tc_SNo.Style.Add("text-align","center");              
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
            tc_DR_Name.RowSpan = 2;
 tc_DR_Name.Style.Add("text-align","center");              
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Retailer Name";
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

          

            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = 3;
                    tc_month.RowSpan = 1;
                    Literal lit_month = new Literal();
                    Monthsub = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    lit_month.Text = Monthsub.Substring(0, 3) + "-" + cyear;
                    tc_month.Attributes.Add("Class", "rptCellBorder");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                  
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
            TableRow tr_catg = new TableRow();
            tr_catg.BorderStyle = BorderStyle.Solid;
            tr_catg.BorderWidth = 1;
            tr_catg.BackColor = System.Drawing.Color.FromName("#496a9a");
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
tc_DR_Codee .Style.Add("text-align","center");              
                    Literal lit_DR_Codee = new Literal();
                    lit_DR_Codee.Text = "Value(Rs)";
                    tc_DR_Codee.Controls.Add(lit_DR_Codee);
                    tc_DR_Codee.Attributes.Add("Class", "rptCellBorder");
                    tc_DR_Codee.BorderColor = System.Drawing.Color.Black;
                    //tc_DR_Code.Visible = false;
                    tr_catg.Cells.Add(tc_DR_Codee);



                    TableCell tc_DR_value1 = new TableCell();
            tc_DR_value1.BorderStyle = BorderStyle.Solid;
            tc_DR_value1.BorderWidth = 1;
            tc_DR_value1.Width = 150;
      tc_DR_value1 .Style.Add("text-align","center");
            Literal lit_DR_value1 = new Literal();
            lit_DR_value1.Text = "Net Weight Value";
            tc_DR_value1.BorderColor = System.Drawing.Color.Black;
            tc_DR_value1.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value1.Controls.Add(lit_DR_value1);
            tr_catg.Cells.Add(tc_DR_value1);
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


            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
         

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

                lit_det_usr.Text = "&nbsp;" + drFF["ListedDrCode"].ToString();
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
                lit_det_FF.Text = "&nbsp;" + drFF["ListedDr_Name"].ToString();


                stCrtDtaPnt += "{label:\"" + drFF["ListedDr_Name"].ToString() + "\",y:";
               
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





                        TableCell tc_det_last6monthsum = new TableCell();
                        tc_det_last6monthsum.Width = 200;

                        tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Right;
                        Literal lit_det_sum = new Literal();
                     
                        tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                        tc_det_last6monthsum.BorderWidth = 1;
                        tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                        tc_det_last6monthsum.Controls.Add(lit_det_sum);
                        tr_det.Cells.Add(tc_det_last6monthsum);

                        dsDoc = sf.Retail_Trend_analysis_retail_valuee(ROUTE, divcode, cmonth, cyear, Stockist_Code,drFF["ListedDrCode"].ToString());


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                          sec_value = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        if (tot_dr != "")
                        {
                            value1 = (Decimal.Parse(tot_dr));
                            lit_det_sum.Text = value1.ToString("0.00");
                            iTotLstCount2 += (Decimal.Parse(tot_dr));
                            

                        }
                        else
                        {
                            lit_det_sum.Text = "";
                           
                        }



TableCell tc_lst_month1 = new TableCell();
                Literal hyp_lst_month1 = new Literal();
                tc_lst_month1.Width = 150;



                if (sec_value != "")
                {
                    hyp_lst_month1.Text = Convert.ToDecimal(  sec_value).ToString("0.00");
                    
                }
                else
                {
                    hyp_lst_month1.Text = "";

                }


                tc_lst_month1.BorderStyle = BorderStyle.Solid;

                tc_lst_month1.BorderStyle = BorderStyle.Solid;
                tc_lst_month1.BorderWidth = 1;
                tc_lst_month1.BackColor = System.Drawing.Color.White;
                tc_lst_month1.Width = 200;
                tc_lst_month1.HorizontalAlign = HorizontalAlign.Right;
                tc_lst_month1.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month1.Controls.Add(hyp_lst_month1);
                tc_lst_month1.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month1);







                        TableCell tc_det_currentmonth = new TableCell();
                        tc_det_currentmonth.Width = 200;

                        tc_det_currentmonth.HorizontalAlign = HorizontalAlign.Right;
                        Literal lit_det_mon = new Literal();
                      
                        tc_det_currentmonth.BorderStyle = BorderStyle.Solid;
                        tc_det_currentmonth.BorderWidth = 1;
                        tc_det_currentmonth.Attributes.Add("Class", "rptCellBorder");
                        tc_det_currentmonth.Controls.Add(lit_det_mon);
                        tr_det.Cells.Add(tc_det_currentmonth);

                        dsDoc = sf.Retail_Trend_analysis_retailer_value_total(divcode, cmonth, cyear, Stockist_Code,ROUTE);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_value = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        if (tot_value != "")
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

                       


                    }


                }

                if (iTotLstCount2 != 0)
                {
                    stCrtDtaPnt += Convert.ToString(iTotLstCount2) + "},";

                }
                else
                {
                    stCrtDtaPnt += "0},";
                }

                iTotLstCount2 = 0;
                tot_dr = "";
                tot_value = "";
                value1 = 0;
                value2 = 0;
                val = 0;

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
                //Session["Test"] = "T";
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
                 
                    dtCurrent = Convert.ToDateTime(sCurrentDate);

                    TableCell tc_tot_month = new TableCell();
                    tc_tot_month.ColumnSpan = 1;
                    HyperLink hyp_month = new HyperLink();
                    //int iTotLstCount = 0;
                    foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                    {



                        dsDoc = sf.Retail_Trend_analysis_retail_valuee(ROUTE, divcode, cmonth, cyear, Stockist_Code, drFF["ListedDrCode"].ToString());
                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
     sec_total = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
 if (sec_total!= "")
                        {
                            iTotLstnetval += (Decimal.Parse(sec_total));
}
                        if (tot_dr != "")
                        {
                            iTotLstCount += (Decimal.Parse(tot_dr));
                            try
                            {
                                Decimal per1 = Math.Round(Convert.ToDecimal(iTotLstCount.ToString()), 2);
                                hyp_month.Text = per1.ToString("0.00");
                            }
                            catch
                            {
                                hyp_month.Text = "&nbsp&nbsp&nbsp&nbsp&nbsp" + iTotLstCount.ToString("0.00");
                            }




                        }
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



                      TableCell tc_tot_monthd = new TableCell();
                      HyperLink hyp_monthd = new HyperLink();


 
                           
                        
                            try
                            {
                                Decimal per1 = Math.Round(Convert.ToDecimal(iTotLstnetval.ToString()), 2);
                                hyp_monthd.Text = per1.ToString("0.00");
                            }
                            catch
                            {
                                hyp_monthd.Text = "&nbsp&nbsp&nbsp&nbsp&nbsp" + iTotLstnetval.ToString("0.00");
                            }



            tc_tot_monthd.BorderStyle = BorderStyle.Solid;
            tc_tot_monthd.BorderWidth = 1;
            tc_tot_monthd.BackColor = System.Drawing.Color.White;
            tc_tot_monthd.Width = 200;
            tc_tot_monthd.Style.Add("font-family", "Calibri");
            tc_tot_monthd.Style.Add("font-size", "10pt");
            tc_tot_monthd.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_monthd.VerticalAlign = VerticalAlign.Middle;
            tc_tot_monthd.Controls.Add(hyp_monthd);
            tc_tot_monthd.Attributes.Add("style", "font-weight:bold;");
            tc_tot_monthd.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_monthd);

 iTotLstnetval=0;
                    iTotLstCount = 0;
TableCell tc_tot_monthdg = new TableCell();
 tc_tot_monthdg.Attributes.Add("Class", "rptCellBorder");
   tr_total.Cells.Add(tc_tot_monthdg);

                
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

        string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);
    }


        }

  