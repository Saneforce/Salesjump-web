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

public partial class MIS_Reports_rpt_Order_Detail_View : System.Web.UI.Page
{
    Decimal iTotLstCount1 = 0;
     Decimal iTotLstCount = 0;  
    int iTotLstCount2 = 0;   
    DataSet dsProduct = null;
    DataSet dsDr = null;
    DataSet dsDrr = null;
    DataSet getweek = null;
    DateTime dtCurrent;
    string sCmd = string.Empty;
    string div_code = string.Empty;
    string ProdBrdCode = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string val = string.Empty;
    string distributor = string.Empty;
    string distributor_name = string.Empty;
    string FO_name = string.Empty;
    string stockist = string.Empty;
    string Year = string.Empty;
    string Month = string.Empty;
    string Stock_name = string.Empty;
    string sf_name = string.Empty;
    string type = string.Empty;
    string sCurrentDate = string.Empty;
    string Sf_Code_multiple = string.Empty;
    string route = string.Empty;
    string tot_dr = string.Empty;
    string strFMonthName = string.Empty;
    int iCount = -1;
    DataSet dsDoc = null;
    
    int FYear=0; 
    int FMonth=0 ;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["Division_Code"].ToString().Replace(",", "");    
        Month = Request.QueryString["FMonth"].ToString();
        Year = Request.QueryString["FYear"].ToString();       
        FMonth = Convert.ToInt32(Month);
        FYear  = Convert.ToInt32(Year);
        distributor = Request.QueryString["Distributor"].ToString();
        route = Request.QueryString["Route"].ToString();
        distributor_name = Request.QueryString["Dis"].ToString();
        FO_name = Request.QueryString["Fo_Name"].ToString();
        lblRegionName.Text = sf_name;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString();


       lblHead.Text = "Order Details - View for the Month of " + strFMonthName + " " + FYear;

        //lblIDMonth.Visible = false;
        //lblIDYear.Visible = false;
       Lbl_Ex_Name.Text = " " + "  Sales Executive Officer Name: " + " " + FO_name + " ";
       Lbl_Fo.Text = " " + " Distributor Name: " + " " + distributor_name + " ";
        
        FillSF_Leave_Type();
       

    }
    private void FillSF_Leave_Type()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;
        string stCrtDtaPnt = string.Empty;

        int tot = 0;
        SalesForce dr = new SalesForce();

        if (route == "0")
        {
            dsDr = dr.Getorderdetail_nilRount(distributor, div_code, FMonth, FYear);
            
        }
        else
        {
            dsDr = dr.Getorderdetail(distributor, route, div_code, FMonth, FYear);
           
        }


        if (dsDr.Tables[0].Rows.Count > 0)
        {

         


            TableRow tr_det = new TableRow();



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
            lit_det_head_doc.Text = "<b>Order Date</b>";
            tc_det_head_doc.Attributes.Add("Class", "tblHead");
            tc_det_head_doc.Controls.Add(lit_det_head_doc);
            tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_doc);


            TableCell tc_det_head_Qua = new TableCell();
            tc_det_head_Qua.BorderStyle = BorderStyle.Solid;
            tc_det_head_Qua.BorderWidth = 1;
            Literal lit_det_head_qua = new Literal();
            lit_det_head_qua.Text = "<b>Retailer Name</b>";
            tc_det_head_Qua.Attributes.Add("Class", "tblHead");
            tc_det_head_Qua.Controls.Add(lit_det_head_qua);
            tc_det_head_Qua.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_Qua);

            TableCell tc_det_head_Quai = new TableCell();
            tc_det_head_Quai.BorderStyle = BorderStyle.Solid;
            tc_det_head_Quai.BorderWidth = 1;
            Literal lit_det_head_quai = new Literal();
            lit_det_head_quai.Text = "<b>Route</b>";
            tc_det_head_Quai.Attributes.Add("Class", "tblHead");
            tc_det_head_Quai.Controls.Add(lit_det_head_quai);
            tc_det_head_Quai.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_Quai);

            TableCell tc_det_head_min = new TableCell();
            tc_det_head_min.BorderStyle = BorderStyle.Solid;
            tc_det_head_min.BorderWidth = 1;
            Literal lit_det_head_min = new Literal();
            lit_det_head_min.Text = "<b>Order Value</b>";
            tc_det_head_min.Attributes.Add("Class", "tblHead");
            tc_det_head_min.Controls.Add(lit_det_head_min);
            tc_det_head_min.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_min);

            TableCell tc_det_head_net = new TableCell();
            tc_det_head_net.BorderStyle = BorderStyle.Solid;
            tc_det_head_net.BorderWidth = 1;
            Literal lit_det_head_net = new Literal();
            lit_det_head_net.Text = "<b>Net Value</b>";
            tc_det_head_net.Attributes.Add("Class", "tblHead");
            tc_det_head_net.Controls.Add(lit_det_head_net);
            tc_det_head_net.HorizontalAlign = HorizontalAlign.Center;
            tr_det_head.Cells.Add(tc_det_head_net);




           
            tbl.Rows.Add(tr_det_head);
            


            int iCount = 0;
            foreach (DataRow drdoctor in dsDr.Tables[0].Rows)
            {





                TableRow tr_det_sno = new TableRow();
                TableCell tc_det_SNo = new TableCell();
                iCount += 1;
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString() ;
                tc_det_SNo.Attributes.Add("Class", "tblRow");
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
				tc_det_SNo.Style.Add("text-align","right");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det_sno.Cells.Add(tc_det_SNo);


                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drdoctor["ORDER_DATE"].ToString();
                stCrtDtaPnt += "{label:\"" + drdoctor["ORDER_DATE"].ToString() + "\",y:";
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det_sno.Cells.Add(tc_det_usr);


                TableCell tc_det_dr_name = new TableCell();
                Literal lit_det_dr_name = new Literal();
                lit_det_dr_name.Text = drdoctor["ListedDr_Name"].ToString();
                tc_det_dr_name.Attributes.Add("Class", "tblRow");
                tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                tc_det_dr_name.BorderWidth = 1;
                tc_det_dr_name.Controls.Add(lit_det_dr_name);
                tr_det_sno.Cells.Add(tc_det_dr_name);

                TableCell tc_det_dr_hq = new TableCell();
                Literal lit_det_dr_hq = new Literal();
                lit_det_dr_hq.Text = drdoctor["Territory_Name"].ToString();
                tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                tc_det_dr_hq.BorderWidth = 1;
                tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                tr_det_sno.Cells.Add(tc_det_dr_hq);

                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 250;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + Convert.ToDecimal( drdoctor["Order_Value"]).ToString("0.00");
                stCrtDtaPnt += "{label:\"" + drdoctor["Order_Value"].ToString() + "\",y:";
                iTotLstCount += Decimal.Parse(drdoctor["Order_Value"].ToString());
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
				tc_det_FF.Style.Add("text-align","right");
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det_sno.Cells.Add(tc_det_FF);

                TableCell tc_det_dr_min = new TableCell();
                Literal lit_det_dr_min = new Literal();
                lit_det_dr_min.Text = Convert.ToDecimal(drdoctor["net"]).ToString("0.00");
                iTotLstCount1 += Decimal.Parse(drdoctor["net"].ToString());
                tc_det_dr_min.Attributes.Add("Class", "tblRow");
				tc_det_dr_min.Style.Add("text-align","right");
                tc_det_dr_min.BorderStyle = BorderStyle.Solid;
                tc_det_dr_min.BorderWidth = 1;
                tc_det_dr_min.Controls.Add(lit_det_dr_min);
                tr_det_sno.Cells.Add(tc_det_dr_min);


                sURL = "rpt_Order_Detail_View1.aspx?&ORDER_DATE=" + drdoctor["ORDER_DATE"] + "&ListedDr_Name=" + drdoctor["ListedDr_Name"].ToString() + "&Territory_Name=" + drdoctor["Territory_Name"].ToString() + "&Order_Value=" + drdoctor["Order_Value"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&sCurrentDate=" + sCurrentDate + "&Distributor=" + distributor + "&DCR_code=" + drdoctor["DCR_Code"].ToString();
                lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                lit_det_FF.NavigateUrl = "#";


                tbl.Rows.Add(tr_det_sno);
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
                tc_Count_Total.ColumnSpan = 4;
                tc_Count_Total.Style.Add("text-align", "center");
                tc_Count_Total.Style.Add("font-family", "Calibri");
                tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
                tc_Count_Total.Style.Add("font-size", "10pt");

                tr_total.Cells.Add(tc_Count_Total);


                //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
               
               

                        TableCell tc_tot_month = new TableCell();
                        HyperLink hyp_month = new HyperLink();
                        //int iTotLstCount = 0;

                        hyp_month.Text = iTotLstCount.ToString("0.00");


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


                        TableCell tc_tot_montht = new TableCell();
                        HyperLink hyp_montht = new HyperLink();
                        //int iTotLstCount = 0;

                        hyp_montht.Text =  iTotLstCount1.ToString("0.00");


                        tc_tot_montht.BorderStyle = BorderStyle.Solid;
                        tc_tot_montht.BorderWidth = 1;
                        tc_tot_montht.BackColor = System.Drawing.Color.White;
                        tc_tot_montht.Width = 200;
                        tc_tot_montht.Style.Add("font-family", "Calibri");
                        tc_tot_montht.Style.Add("font-size", "10pt");
                        tc_tot_montht.HorizontalAlign = HorizontalAlign.Right;
                        tc_tot_montht.VerticalAlign = VerticalAlign.Middle;
                        tc_tot_montht.Controls.Add(hyp_montht);
                        tc_tot_montht.Attributes.Add("style", "font-weight:bold;");
                        tc_tot_montht.Attributes.Add("Class", "rptCellBorder");
                        tr_total.Cells.Add(tc_tot_montht);
              

                tbl.Rows.Add(tr_total);

               
            
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
        string attachment = "attachment; filename=DCRView.xls";
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