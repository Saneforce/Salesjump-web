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

public partial class MIS_Reports_rpt_Retailer_Distributor_Wise : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    decimal iTotLstCount2 = 0;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FromDate= string.Empty;
    string ToDate = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string stype = string.Empty;
    string tot_dr = string.Empty;

    string subdivision_code = string.Empty;
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;

    DataSet dsprd = new DataSet();
    string sCurrentDate = string.Empty;
    string stcode = string.Empty;
    string stname = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfCode = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        divcode = Session["div_code"].ToString();

        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        subdivision_code = Request.QueryString["subdivision"].ToString();
        vMode = Request.QueryString["Mode"].ToString();
        stcode = Request.QueryString["state"].ToString();
        stname = Request.QueryString["vstate"].ToString();
        FromDate = Request.QueryString["Fdates"].ToString();
        ToDate = Request.QueryString["Tdates"].ToString();

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        //string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);




        SalesForce sf = new SalesForce();
        if (vMode == "1")
        {
            lblHead.Text = "Field Force Wise Order Value form  " + FromDate + " To " + ToDate + "";
            dsSalesForce = sf.Get_Retailer_FieldForce_wise(divcode, sfCode, FromDate, ToDate, FYear, TYear, FMonth, TMonth, subdivision_code, stcode);
            DataTable dt = new DataTable();
            dt.Columns.Add("Sl.No.", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Values", typeof(string));
            dt.Columns.Add("Net Wight", typeof(string));

            int i = 1;
            decimal totVAl = 0;
            decimal totNet = 0;
            foreach (DataRow row in dsSalesForce.Tables[0].Rows)
            {
                decimal Val = row["value"] == DBNull.Value ? 0 : Convert.ToDecimal(row["value"]);
                decimal Net = row["net_weight_value"] == DBNull.Value ? 0 : Convert.ToDecimal(row["net_weight_value"]);
                dt.Rows.Add(i.ToString(),row["sf_name"].ToString(), Val.ToString("0.00"), Net.ToString("0.00"));
                totVAl += Val;
                totNet += Net;
                i++;
            }
            dt.Rows.Add("", "Total", totVAl.ToString("0.00"), totNet.ToString("0.00"));
            DGVFFO.DataSource = dt;
            DGVFFO.DataBind();
        }
        else
        {
            lblHead.Text = "Distributor Wise Order Value form  " + FromDate + " To " + ToDate + "";
            dsSalesForce = sf.Get_Retailer_Distributor_wise(divcode, sfCode, FromDate, ToDate, FYear, TYear, FMonth, TMonth, subdivision_code, stcode);
            DataTable dt = new DataTable();
            dt.Columns.Add("Sl.No.", typeof(string));
            dt.Columns.Add("District", typeof(string));
            dt.Columns.Add("Taluk", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Values", typeof(string));
            dt.Columns.Add("Net Wight", typeof(string));

            int i = 1;
            decimal totVAl = 0;
            decimal totNet = 0;
            foreach (DataRow row in dsSalesForce.Tables[0].Rows)
            {
                decimal Val = row["value"] == DBNull.Value ? 0 : Convert.ToDecimal(row["value"]);
                decimal Net = row["net_weight_value"] == DBNull.Value ? 0 : Convert.ToDecimal(row["net_weight_value"]);
                dt.Rows.Add(i.ToString(), row["Dist_Name"].ToString(), row["Taluk_Name"].ToString(), row["sf_name"].ToString(), Val.ToString("0.00"), Net.ToString("0.00"));
                totVAl += Val;
                totNet += Net;
                i++;
            }
            dt.Rows.Add("", "", "", "Total", totVAl.ToString("0.00"), totNet.ToString("0.00"));
            DGVFFO.DataSource = dt;
            DGVFFO.DataBind();
        }

       

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



    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = DGVFFO;
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
        frm.Controls.Add(DGVFFO);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

}