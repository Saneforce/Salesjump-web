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


public partial class MIS_Reports_rpt_Distribution_Width : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    decimal iTotLstCount2 = 0;
    decimal value1 = 0;
    decimal value2 = 0;
    decimal val = 0;
    string distributor = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string product_name = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
     string sCurrentDate = string.Empty;
    string product_code = string.Empty;
    string tot_dr = string.Empty;
    string tot_value = string.Empty;
    string Monthsub = string.Empty;
    string dist_name = string.Empty;
    string dist_code = string.Empty;
  string fieldforce=string.Empty;
    DataSet dsprd = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();

        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
          fieldforce= Request.QueryString["fieldforce"].ToString();
        product_code = Request.QueryString["product_code"].ToString();
        dist_name = Request.QueryString["stockist_name"].ToString();
        distributor  = Request.QueryString["stockist_code"].ToString();
        dist_code = distributor.Trim();
        product_name = Request.QueryString["product_name"].ToString();
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
       string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

       lblHead.Text = " Distribution Width  for  " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;
       
       distname.Text = dist_name;
       prdname.Text = product_name;
         fldfrce.Text=fieldforce;

       
        FillSF();

    }

    private void FillSF()
    {
        Random rndm = new Random();
        int t = rndm.Next(1, 28);
        int mnth = Convert.ToInt32(FMonth), year = Convert.ToInt32(FYear);
        //DateTime dt = Convert.ToDateTime(t.ToString() + "-" + mnth.ToString() + "-" + year);
        DateTime dt = Convert.ToDateTime(year + "-" + mnth.ToString() + "-" + t.ToString());
        //string s = dt.ToString();
        string st = dt.ToString("yyyy-MM-dd");
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.GetRouteName_Distributorwise(dist_code, divcode);
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
			tc_SNo.HorizontalAlign = HorizontalAlign.Center;
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
            tc_DR_Name.RowSpan =2;
			tc_DR_Name.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Route Name";
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
                    tc_month.ColumnSpan = 3;
                  
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
            tr_catg.BackColor = System.Drawing.Color.FromName("#496a9a");
            tr_catg.Style.Add("Color", "White");
            tr_catg.BorderColor = System.Drawing.Color.Black;
            

if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {
                
            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 200;
 tc_DR_Code.HorizontalAlign = HorizontalAlign.Center;
            //tc_DR_Code.RowSpan = 1;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "Available Retailer";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Code.Visible = false;
            tr_catg.Cells.Add(tc_DR_Code);
            TableCell tc_DR_Code_month = new TableCell();
            tc_DR_Code_month.BorderStyle = BorderStyle.Solid;
            tc_DR_Code_month.BorderWidth = 1;
            tc_DR_Code_month.Width = 200;
 tc_DR_Code_month.HorizontalAlign = HorizontalAlign.Center;
            //tc_DR_Code_month.RowSpan = 1;
            Literal lit_DR_Code_mon = new Literal();
            lit_DR_Code_mon.Text = "Non- Available Retailer";
            tc_DR_Code_month.Controls.Add(lit_DR_Code_mon);
            tc_DR_Code_month.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code_month.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Code.Visible = false;
            tr_catg.Cells.Add(tc_DR_Code_month);
            TableCell percentage = new TableCell();
            percentage.BorderStyle = BorderStyle.Solid;
            percentage.BorderWidth = 1;
            percentage.Width = 200;
 percentage.HorizontalAlign = HorizontalAlign.Center;
            //tc_DR_Code_month.RowSpan = 1;
            Literal lit_DR_Code_percent = new Literal();
            lit_DR_Code_percent.Text = "Available  Retailer %";
            percentage.Controls.Add(lit_DR_Code_percent);
            percentage.Attributes.Add("Class", "rptCellBorder");
            percentage.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Code.Visible = false;
            tr_catg.Cells.Add(percentage);
           
            cmonth = cmonth + 1;
            if (cmonth == 13)
            {
                cmonth = 1;
                cyear = cyear + 1;

            }

                }

                tbl.Rows.Add(tr_catg);
            }
          

    


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
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Territory_Code"].ToString();
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
                lit_det_FF.Text = "&nbsp;" + drFF["Territory_Name"].ToString();
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
                        HyperLink lit_det_sum = new HyperLink();
                        //lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                        tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                        tc_det_last6monthsum.BorderWidth = 1;
                        tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                        tc_det_last6monthsum.Controls.Add(lit_det_sum);
                        tr_det.Cells.Add(tc_det_last6monthsum);

                        dsDoc = sf.retail_routewise_available_retailer(dist_code, divcode, drFF["Territory_Code"].ToString(), product_code, cmonth, cyear);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        lit_det_sum.Text = tot_dr.ToString();
                        value1 = Decimal.Parse(tot_dr);
                        stURL = "rpt_avail_retailer_view.aspx?Territory_code=" + drFF["Territory_Code"] + "&territory_name=" + drFF["Territory_Name"].ToString() + " &FYear=" + cyear + "&FMonth=" + cmonth + "&product_code=" + product_code + "&distri_name=" + dist_name + "&distributor_code=" + dist_code + "";
                        lit_det_sum.Attributes["onclick"] = "javascript:window.open('" + stURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                        TableCell tc_det_currentmonth = new TableCell();
                        tc_det_currentmonth.Width = 200;
                        tc_det_currentmonth.HorizontalAlign = HorizontalAlign.Right;
                        HyperLink lit_det_mon = new HyperLink();
                        //lit_det_FF.Text = "&nbsp;" + drFF["Order_Value"].ToString();
                        tc_det_currentmonth.BorderStyle = BorderStyle.Solid;
                        tc_det_currentmonth.BorderWidth = 1;
                        tc_det_currentmonth.Attributes.Add("Class", "rptCellBorder");
                        tc_det_currentmonth.Controls.Add(lit_det_mon);
                        tr_det.Cells.Add(tc_det_currentmonth);


                        dsDoc = sf.retail_routewise_non_available_retailer(dist_code, divcode, drFF["Territory_Code"].ToString(), product_code, cmonth, cyear);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        lit_det_mon.Text = tot_dr.ToString();
                        sURL = "rpt_non_retailer_view.aspx?Territory_Code=" + drFF["Territory_Code"] + "&territory_name=" + drFF["Territory_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&product_code=" + product_code + "&distri_name=" + dist_name + "&distributor_code=" + dist_code + "";
                        lit_det_mon.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                        lit_det_mon.NavigateUrl = "#";

                        TableCell tc_det_contri = new TableCell();
                        tc_det_contri.Width = 200;
                        tc_det_contri.HorizontalAlign = HorizontalAlign.Right;
                        Literal lit_det_contri = new Literal();
                        //lit_det_FF.Text = "&nbsp;" + drFF["Order_Value"].ToString();
                        tc_det_contri.BorderStyle = BorderStyle.Solid;
                        tc_det_contri.BorderWidth = 1;
                        tc_det_contri.Attributes.Add("Class", "rptCellBorder");
                        tc_det_contri.Controls.Add(lit_det_contri);
                        tr_det.Cells.Add(tc_det_contri);


                        dsDoc = sf.Gettotal_retailers_per_poute(divcode, drFF["Territory_Code"].ToString());


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_value = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        if (tot_value != "" && tot_value !="0" )
                        {
                            value2 = (Decimal.Parse(tot_value));
                            val = (Convert.ToDecimal(value1 / value2)*100);
                            decimal value = Math.Round(val, 2);
                            lit_det_contri.Text = value.ToString();

                        }
                        else
                        {
                            lit_det_contri.Text = "";
                        }
                       tot_value = "0";
                       //tot_dr ="0";

                        tbl.Rows.Add(tr_det);
                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }


                    }


                }

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