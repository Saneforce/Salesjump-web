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
using iTextSharp.tool.xml;
using System.Net;

public partial class MIS_Reports_rptdailyordervsprimary : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int productive_count = 0;
    string distributor = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string subdivision = string.Empty;
    string h = string.Empty;
    int sum_time = 0;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    TimeSpan ff;
    int rowspan = 0;
    string sCurrentDate = string.Empty;
    string endTime = string.Empty;
    string startTime = string.Empty;
    string tot_dr = string.Empty;
    string tot_value = string.Empty;
    string con_qty = string.Empty;
    string diff_case = string.Empty;
    string Monthsub = string.Empty;
    string dist_name = string.Empty;
    string dist_code = string.Empty;
    string date = string.Empty;
    string endd = string.Empty;
    DataSet dsDoctor = new DataSet();
    string gg = string.Empty;
    int quantity2 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();

        sfname = Request.QueryString["sfname"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        subdivision = Request.QueryString["subdivision"].ToString();
        dist_code = sfCode.Trim();

        gg = Request.QueryString["DATE"].ToString();
        date = gg.Trim();
		DateTime dt = Convert.ToDateTime(date);
        string hdate = dt.ToString("dd-MM-yyyy");
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

        lblHead.Text = "Daily Order Vs Primary  for   " + hdate;

        distname.Text = sfname;
        norecordfound.Visible = false;
        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
        FillSF();

    }

    private void FillSF()
    {
        Random rndm = new Random();
        int t = rndm.Next(1, 28);

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.daily_Trans_Order_vs_primary(divcode, sfCode, date, subdivision);
        //dsSalesForce = sf.daily_Trans_Order_uu(divcode, sfCode, date,subdivision);
      
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
         
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
			tc_SNo.Style.Add("text-align","center");
            tr_header.Cells.Add(tc_SNo);



            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
           tc_DR_Name.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Distributor";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_Name_pot = new TableCell();
            tc_DR_Name_pot.BorderStyle = BorderStyle.Solid;
            tc_DR_Name_pot.BorderWidth = 1;
            tc_DR_Name_pot.Width = 250;
            tc_DR_Name_pot.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Name_pot = new Literal();
            lit_DR_Name_pot.Text = "Item";
            tc_DR_Name_pot.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name_pot.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name_pot.Controls.Add(lit_DR_Name_pot);
            tr_header.Cells.Add(tc_DR_Name_pot);



            TableCell activity = new TableCell();
            activity.BorderStyle = BorderStyle.Solid;
            activity.BorderWidth = 1;
            activity.Width = 250;
	        activity.HorizontalAlign = HorizontalAlign.Center;
            Literal activitylit = new Literal();
            activitylit.Text = "Quantity";
            activity.BorderColor = System.Drawing.Color.Black;
            activity.Attributes.Add("Class", "rptCellBorder");
            activity.Controls.Add(activitylit);
            tr_header.Cells.Add(activity);




            TableCell tc_DR_Name_product_name = new TableCell();
            tc_DR_Name_product_name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name_product_name.BorderWidth = 1;
            
            tc_DR_Name_product_name.Width = 250;
			tc_DR_Name_product_name.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Name_pot_name = new Literal();
            lit_DR_Name_pot_name.Text = "Primary For Next Day";
            tc_DR_Name_product_name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name_product_name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name_product_name.Controls.Add(lit_DR_Name_pot_name);
            tr_header.Cells.Add(tc_DR_Name_product_name);

            TableCell tc_DR_Name_pott = new TableCell();
            tc_DR_Name_pott.BorderStyle = BorderStyle.Solid;
            tc_DR_Name_pott.BorderWidth = 1;
            tc_DR_Name_pott.Width = 250;
           
          tc_DR_Name_pott.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Name_pott = new Literal();
            lit_DR_Name_pott.Text = "Difference";
            tc_DR_Name_pott.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name_pott.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name_pott.Controls.Add(lit_DR_Name_pott);
            tr_header.Cells.Add(tc_DR_Name_pott);



          
       


            tbl.Rows.Add(tr_header);


          




            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;



            int iCount = 0;
            //string iTotLstCount ="0";

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;


                //S.No

                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text =  iCount.ToString();
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
				tc_det_SNo.HorizontalAlign = HorizontalAlign.Right;
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code

                TableCell tc_det_currentmonth = new TableCell();
                tc_det_currentmonth.Width = 200;
                tc_det_currentmonth.HorizontalAlign = HorizontalAlign.Left;
                tc_det_currentmonth.VerticalAlign = VerticalAlign.Middle;
                 
                HyperLink lit_det_mon = new HyperLink();

                lit_det_mon.Text = drFF["Stockist_Name"].ToString();
                string code = drFF["Stockist_Code"].ToString();
                if (h == lit_det_mon.Text)
                {
                    rowspan += 0;
                    lit_det_mon.Visible = false;
                    //tc_det_currentmonth
                    tc_det_currentmonth.BorderStyle = BorderStyle.None;

                }
                else
                {
                    tc_det_currentmonth.BorderStyle = BorderStyle.None;
                }
                 h = lit_det_mon.Text;
                 tc_det_currentmonth.RowSpan = rowspan;
              
                //tc_det_currentmonth.BorderWidth = 1;
                //tc_det_currentmonth.Attributes.Add("Class", "rptCellBorder");
                tc_det_currentmonth.Controls.Add(lit_det_mon);
                tr_det.Cells.Add(tc_det_currentmonth);
             
                //SF Name
                TableCell tc_det_FF = new TableCell();

                tc_det_FF.HorizontalAlign = HorizontalAlign.Left;
                tc_det_FF.Width = 300;
                Literal address = new Literal();
                address.Text = "&nbsp;" + drFF["Product_Name"].ToString();
                string f = drFF["Product_Code"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(address);
                tr_det.Cells.Add(tc_det_FF);


                TableCell tc_det_usr = new TableCell();
                tc_det_usr.Attributes.Add("style", "color:Blue;");
                Literal retailname = new Literal();
                tc_det_usr.HorizontalAlign = HorizontalAlign.Right;
                retailname.Text =  drFF["Quantity"].ToString();
                string d = retailname.Text;
                int quantity1 =Convert.ToInt32(d);
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;

                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(retailname);
                tr_det.Cells.Add(tc_det_usr);

                dsDoc = sf.daily_Trans_Order_primary_value(divcode, drFF["Product_Code"].ToString(), date, drFF["Stockist_Code"].ToString());
                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    tot_value = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                else
                {
                    tot_value = "0";
                }
            
                if (tot_value == "")
                {

                    quantity2 = 0;
                }
                else
                {
                    quantity2 = Convert.ToInt32(tot_value);
                }
                
              
                TableCell tc_det_FF_milk = new TableCell();
				 tc_det_FF_milk.HorizontalAlign = HorizontalAlign.Right;
                tc_det_FF_milk.Attributes.Add("style", "color:Blue;");
                tc_det_FF_milk.Width = 300;
                Literal lit_det_FF_milk = new Literal();
                lit_det_FF_milk.Text = Convert.ToDecimal(  tot_value).ToString("0.00");
                tc_det_FF_milk.BorderStyle = BorderStyle.Solid;
                tc_det_FF_milk.BorderWidth = 1;
                tc_det_FF_milk.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF_milk.Controls.Add(lit_det_FF_milk);
                tr_det.Cells.Add(tc_det_FF_milk);

                dsDoc = sf.get_Product_conversion_qty(divcode, drFF["Product_Code"].ToString());
                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    con_qty = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                int diff = quantity1 - quantity2;
                //double case_value = (diff / (Convert.ToDouble(con_qty)));

                dsDoc = sf.get_case_qty_order_vs_primary(diff, con_qty);
                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    diff_case = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                else
                {
                    diff_case = "0";
                }
                TableCell tc_det_last6monthsum = new TableCell();
                tc_det_last6monthsum.Width = 200;

                tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_det_sum = new Literal();
                lit_det_sum.Text = Convert.ToDecimal( diff_case).ToString("0.00");
                startTime = lit_det_sum.Text;
                tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                tc_det_last6monthsum.BorderWidth = 1;
                tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                tc_det_last6monthsum.Controls.Add(lit_det_sum);
                tr_det.Cells.Add(tc_det_last6monthsum);

                               


                tbl.Rows.Add(tr_det);
              

            }
        }
        else
        {
            norecordfound.Visible = true;
           
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
                Document pdfDoc = new Document(PageSize.A4,
                    10f, 10f, 10f, 0f);
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