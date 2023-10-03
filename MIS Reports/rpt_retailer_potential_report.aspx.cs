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

public partial class MIS_Reports_rpt_retailer_potential_report : System.Web.UI.Page
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
    string feildforce = string.Empty;
    string route_name = string.Empty;
    DataSet dsSalesForce = new DataSet();  
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string sCurrentDate = string.Empty;
    string route_code = string.Empty;
    string tot_dr = string.Empty;
    string tot_value = string.Empty;
    string Monthsub = string.Empty;
    string dist_name = string.Empty;
    string dist_code = string.Empty;
    string date = string.Empty;
    string subdivision = string.Empty;
    DataSet dsprd = new DataSet();
    DataSet dsDoctor = new DataSet();
    string gg = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();

      feildforce= Request.QueryString["Feildforce"].ToString();
        route_code = Request.QueryString["Route_code"].ToString();
        dist_name = Request.QueryString["stockist_name"].ToString();
        distributor = Request.QueryString["stockist_code"].ToString();
        dist_code = distributor.Trim();
        route_name = Request.QueryString["Route_name"].ToString();
        gg = Request.QueryString["DATE"].ToString();
        date = gg.Trim();
		DateTime dt = Convert.ToDateTime(date);
        string hdate = dt.ToString("dd-MM-yyyy");
        subdivision = Request.QueryString["subdivision"].ToString();
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
      
        lblHead.Text = " Retailer Potential  for  " + hdate;

        distname.Text = dist_name;
        prdname.Text = route_name;

        feildforc.Text =feildforce;
        FillSF();

    }

    private void FillSF()
    {
        Random rndm = new Random();
        int t = rndm.Next(1, 28);
        
        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.retail_routewise_distributorwise_retailer(dist_code, divcode,route_code);


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
            tc_SNo.Width = 200;
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
            tc_DR_Name.RowSpan = 2;
			tc_DR_Name.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Retailer Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);
            
            TableCell tc_DR_Name_pot = new TableCell();
            tc_DR_Name_pot.BorderStyle = BorderStyle.Solid;
            tc_DR_Name_pot.BorderWidth = 1;
            tc_DR_Name_pot.Width = 250;
            tc_DR_Name_pot.RowSpan = 2;
			tc_DR_Name_pot.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Name_pot = new Literal();
            lit_DR_Name_pot.Text = "Potential";
            tc_DR_Name_pot.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name_pot.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name_pot.Controls.Add(lit_DR_Name_pot);
            tr_header.Cells.Add(tc_DR_Name_pot);
            dsDoctor = sf.Secondary_sales_productdetail(divcode, subdivision);
            TableCell tc_DR_Name_product_name = new TableCell();
            tc_DR_Name_product_name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name_product_name.BorderWidth = 1;
            tc_DR_Name_product_name.Width = 250;
			tc_DR_Name_product_name.HorizontalAlign = HorizontalAlign.Center;
            tc_DR_Name_product_name.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
            Literal lit_DR_Name_pot_name = new Literal();
            lit_DR_Name_pot_name.Text = "Product Name";
            tc_DR_Name_product_name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name_product_name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name_product_name.Controls.Add(lit_DR_Name_pot_name);
            tr_header.Cells.Add(tc_DR_Name_product_name);

            TableCell tc_DR_Name_pott = new TableCell();
            tc_DR_Name_pott.BorderStyle = BorderStyle.Solid;
            tc_DR_Name_pott.BorderWidth = 1;
            tc_DR_Name_pott.Width = 250;
            tc_DR_Name_pott.RowSpan = 2;
			tc_DR_Name_pott.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Name_pott = new Literal();
            lit_DR_Name_pott.Text = "Total Order Value";
            tc_DR_Name_pott.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name_pott.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name_pott.Controls.Add(lit_DR_Name_pott);
            tr_header.Cells.Add(tc_DR_Name_pott);







            TableCell tc_DR_Name_pottD = new TableCell();
            tc_DR_Name_pottD.BorderStyle = BorderStyle.Solid;
            tc_DR_Name_pottD.BorderWidth = 1;
            tc_DR_Name_pottD.Width = 250;
            tc_DR_Name_pottD.RowSpan = 2;
			tc_DR_Name_pottD.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Name_pottD = new Literal();
            lit_DR_Name_pottD.Text = "Net Value(Litres)";
            tc_DR_Name_pottD.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name_pottD.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name_pottD.Controls.Add(lit_DR_Name_pottD);
            tr_header.Cells.Add(tc_DR_Name_pottD);

            TableRow tr_catg = new TableRow();
            tr_catg.BorderStyle = BorderStyle.Solid;
            tr_catg.BorderWidth = 1;
            tr_catg.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_catg.Style.Add("Color", "White");
            tr_catg.BorderColor = System.Drawing.Color.Black;
            //  tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");
          

            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                {
                    TableCell tc_catg_namee = new TableCell();
                    tc_catg_namee.BorderStyle = BorderStyle.Solid;
                    tc_catg_namee.BorderWidth = 1;
                    //text-align: center;


                    Literal lit_catg_namee = new Literal();
                    lit_catg_namee.Text = dataRow["Product_Detail_Name"].ToString();
                    string code = dataRow["Product_Detail_Code"].ToString();
                 
                    tc_catg_namee.Attributes.Add("Class", "rptCellBorder");
                    tc_catg_namee.HorizontalAlign = HorizontalAlign.Center;
                    tc_catg_namee.Controls.Add(lit_catg_namee);
                    tr_catg.Cells.Add(tc_catg_namee);
                }

                //tbl.Rows.Add(tr_catg);
            }
           
                      
          
            tbl.Rows.Add(tr_header);
           
                tbl.Rows.Add(tr_catg);
            





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
                lit_det_SNo.Text = iCount.ToString() ;
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
				tc_det_SNo.HorizontalAlign = HorizontalAlign.Center;
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
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                TableCell tc_det_FF_milk = new TableCell();
                tc_det_FF_milk.Attributes.Add("style", "color:Blue;");
                tc_det_FF_milk.Width = 300;
				tc_det_FF_milk.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_det_FF_milk = new Literal();
                lit_det_FF_milk.Text = "&nbsp;" + drFF["Milk_Potential"].ToString();
                tc_det_FF_milk.BorderStyle = BorderStyle.Solid;
                tc_det_FF_milk.BorderWidth = 1;
                tc_det_FF_milk.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF_milk.Controls.Add(lit_det_FF_milk);
                tr_det.Cells.Add(tc_det_FF_milk);

              
                         if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                {
                  
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

                        dsDoc = sf.retail_routewise_distriretailer_quantity(dist_code, divcode, drFF["ListedDrCode"].ToString(), route_code, dataRow["Product_Detail_Code"].ToString(), date);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        lit_det_sum.Text = tot_dr.ToString();
                      

                       
                       

                    
                        tbl.Rows.Add(tr_det);
                        


                }}
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

                         dsDoc = sf.retail_routewise_distriretailer_value(dist_code, divcode, drFF["ListedDrCode"].ToString(), route_code,date);


                         if (dsDoc.Tables[0].Rows.Count > 0)
                             tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                         lit_det_mon.Text = tot_dr=="" ?tot_dr.ToString():Convert.ToDecimal(tot_dr).ToString("0.00");
						 TableCell tc_det_currentmonthc = new TableCell();
                         tc_det_currentmonthc.Width = 200;
                         tc_det_currentmonthc.HorizontalAlign = HorizontalAlign.Right;
                         HyperLink lit_det_monc = new HyperLink();
                         //lit_det_FF.Text = "&nbsp;" + drFF["Order_Value"].ToString();
                         tc_det_currentmonthc.BorderStyle = BorderStyle.Solid;
                         tc_det_currentmonthc.BorderWidth = 1;
                         tc_det_currentmonthc.Attributes.Add("Class", "rptCellBorder");
                         tc_det_currentmonthc.Controls.Add(lit_det_monc);
                         tr_det.Cells.Add(tc_det_currentmonthc);

                         dsDoc = sf.retail_routewise_distriretailer_netvalue(dist_code, divcode, drFF["ListedDrCode"].ToString(), route_code, date);


                         if (dsDoc.Tables[0].Rows.Count > 0)
                             tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                         lit_det_monc.Text = tot_dr=="" ?tot_dr.ToString():Convert.ToDecimal(tot_dr).ToString("0.00");
											




                

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