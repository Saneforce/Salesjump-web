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
using System.Configuration;
using System.Data.SqlClient;
using iTextSharp.tool.xml;

public partial class MIS_Reports_rpt_retail_top10_exception : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string viewby = string.Empty;
    string viewtop = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    string tot_dr = string.Empty;
    string Monthsub = string.Empty;
    string route = string.Empty;
    DataSet dsprd = new DataSet();
    string sCurrentDate = string.Empty;
    string dist_name = string.Empty;
    string route_name = string.Empty;
    DataSet dsDoc = null;
    decimal value1 = 0;
    decimal value2 = 0;
    decimal ff = 0;
    string tot_value = string.Empty;
    decimal val = 0;
    string stockist_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();


        FYear = Request.QueryString["FYear"].ToString();
        viewtop = Request.QueryString["viewtop"].ToString();
        route = Request.QueryString["route"].ToString();
        stockist_code = Request.QueryString["stockist_code"].ToString();
        dist_name = Request.QueryString["stockist_name"].ToString();
        route_name = Request.QueryString["routee_name"].ToString();
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();



        distt.Text = dist_name;
        rout.Text = route_name;
        lblHead.Text = "Top  " + viewtop + "  Retail Exception Route Wise for   " + FYear;
        FillSF();





    }



    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.retail_Gettop10value_route(divcode, FYear, route, stockist_code);


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
            tc_SNo.Style.Add("text-align", "center");
            tc_SNo.RowSpan = 1;
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
            tc_DR_Name.Width = 150;
            tc_DR_Name.RowSpan = 1;
            tc_DR_Name.Style.Add("text-align", "center");
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Customer Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_add = new TableCell();
            tc_DR_add.BorderStyle = BorderStyle.Solid;
            tc_DR_add.BorderWidth = 1;
            tc_DR_add.Width = 150;
            tc_DR_add.RowSpan = 1;
            tc_DR_add.Style.Add("text-align", "center");
            Literal lit_DR_add = new Literal();
            lit_DR_add.Text = "Customer Address";
            tc_DR_add.BorderColor = System.Drawing.Color.Black;
            tc_DR_add.Attributes.Add("Class", "rptCellBorder");
            tc_DR_add.Controls.Add(lit_DR_add);
            tr_header.Cells.Add(tc_DR_add);

            TableCell tc_DR_value = new TableCell();
            tc_DR_value.BorderStyle = BorderStyle.Solid;
            tc_DR_value.BorderWidth = 1;
            tc_DR_value.Width = 150;
            tc_DR_value.RowSpan = 1;
            tc_DR_value.Style.Add("text-align", "center");
            Literal lit_DR_value = new Literal();
            lit_DR_value.Text = "Value";
            tc_DR_value.BorderColor = System.Drawing.Color.Black;
            tc_DR_value.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value.Controls.Add(lit_DR_value);
            tr_header.Cells.Add(tc_DR_value);

            TableCell tc_DR_value1 = new TableCell();
            tc_DR_value1.BorderStyle = BorderStyle.Solid;
            tc_DR_value1.BorderWidth = 1;
            tc_DR_value1.Width = 150;
            tc_DR_value1.RowSpan = 1;
            tc_DR_value1.Style.Add("text-align", "center");
            Literal lit_DR_value1 = new Literal();
            lit_DR_value1.Text = "Net Weight (Litres)";
            tc_DR_value1.BorderColor = System.Drawing.Color.Black;
            tc_DR_value1.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value1.Controls.Add(lit_DR_value1);
            tr_header.Cells.Add(tc_DR_value1);


            TableCell contribution = new TableCell();
            contribution.BorderStyle = BorderStyle.Solid;
            contribution.BorderWidth = 1;
            contribution.Width = 150;
            contribution.RowSpan = 1;
            contribution.Style.Add("text-align", "center");
            Literal contribution_lit = new Literal();
            contribution_lit.Text = "%Contibution";
            contribution.BorderColor = System.Drawing.Color.Black;
            contribution.Attributes.Add("Class", "rptCellBorder");
            contribution.Controls.Add(contribution_lit);
            tr_header.Cells.Add(contribution);


            tbl.Controls.Add(tr_header);



            SalesForce sal = new SalesForce();


            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            StringBuilder output = new StringBuilder();
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {


                output.AppendLine();
                Response.Write(output);
                TableRow tr_det = new TableRow();
                iCount += 1;

                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString();
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Style.Add("text-align", "right");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;


                //SF_code
                TableCell tc_det_usr = new TableCell();
                tc_det_usr.Width = 150;
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Cust_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 150;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["ListedDr_Name"].ToString();
                tc_det_FF.HorizontalAlign = HorizontalAlign.Left;
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                TableCell tc_det_add = new TableCell();
                Literal lit_det_add = new Literal();
                lit_det_add.Text = "&nbsp;" + drFF["ListedDr_Address1"].ToString();
                tc_det_add.BorderStyle = BorderStyle.Solid;
                tc_det_add.BorderWidth = 1;
                tc_det_add.Attributes.Add("Class", "rptCellBorder");
                tc_det_add.Controls.Add(lit_det_add);
                tr_det.Cells.Add(tc_det_add);


                TableCell tc_lst_month = new TableCell();
                Literal hyp_lst_month = new Literal();
                tc_lst_month.Width = 150;
                //hyp_lst_month.Text = "&nbsp;" + drFF["value"].ToString();
                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;
                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 200;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Right;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);


                try
                {
                    Decimal per = Math.Round(Convert.ToDecimal(drFF["value"].ToString()), 2);
                    hyp_lst_month.Text = per.ToString("0.00");
                }
                catch
                {
                    hyp_lst_month.Text = drFF["value"] == DBNull.Value ? drFF["value"].ToString() : Convert.ToDecimal(drFF["value"]).ToString("0.00");
                }
                string hh = hyp_lst_month.Text;


                if (hh != "")
                {
                    ff = Convert.ToDecimal(hh);
                    value1 = ff;


                }
                else
                {
                    value1 = 0;

                }


                TableCell tc_lst_month1 = new TableCell();
                Literal hyp_lst_month1 = new Literal();
                tc_lst_month1.Width = 150;
                if (drFF["net_weight_value"].ToString() != "")
                {
                    hyp_lst_month1.Text = "&nbsp;" + Convert.ToDecimal(drFF["net_weight_value"]).ToString("0.00");
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
                //lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                tc_det_currentmonth.BorderStyle = BorderStyle.Solid;
                tc_det_currentmonth.BorderWidth = 1;
                tc_det_currentmonth.Attributes.Add("Class", "rptCellBorder");
                tc_det_currentmonth.Controls.Add(lit_det_mon);
                tr_det.Cells.Add(tc_det_currentmonth);

                dsDoc = sf.retail_Gettop10value_route_total(divcode, FYear, route);


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_value = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_value != "" && tot_value != "0")
                {
                    value2 = (Decimal.Parse(tot_value));
                    val = (Convert.ToDecimal(value1 / value2) * 100);
                    decimal value = Math.Round(val, 2);
                    lit_det_mon.Text = value.ToString("0.00");

                }
                else
                {
                    lit_det_mon.Text = "";
                }








                if (iCount <= int.Parse(viewtop))
                {


                    tc_det_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd"); ;
                    tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd"); ;
                    tc_det_FF.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd"); ;
                    tc_det_add.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd"); ;
                    tc_det_usr.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd"); ;
                    tc_det_currentmonth.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_lst_month1.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                }
                tbl.Controls.Add(tr_det);




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




    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("viewreort.aspx");
    }
}