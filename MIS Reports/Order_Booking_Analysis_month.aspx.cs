using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Configuration;
using Bus_EReport;
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
using System.Text.RegularExpressions;

using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;

public partial class MasterFiles_Order_Booking_Analysis_month : System.Web.UI.Page
{

    #region "Declaration"
    string divcode = string.Empty;
    string subdiv_code = string.Empty;
    string sfCode = string.Empty;
    string sf_type = string.Empty;
    string FYear = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    string imagepath = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        subdiv_code = Request.QueryString["subdiv"].ToString();
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["SF_Code"].ToString(); ;
        FYear = Request.QueryString["FYear"].ToString();
        lblyear.Value = FYear;
		lblHead.Text = "Order Booking Analysis View for " + FYear;
        lblsf_name.Text = Request.QueryString["sfname"].ToString();
        imgpath.Value = Request.QueryString["imgpath"].ToString();
		imagepath = Request.QueryString["imgpath"].ToString();
        logoo.ImageUrl = imagepath;
        hidn_sf_code.Value = sfCode;
        if (!Page.IsPostBack)
        {
            FillSF();
        }
    }

    private void FillSF()
    {
       GV_DATA.DataSource = null;
        GV_DATA.DataBind();
        SalesForce sf = new SalesForce();
        DataSet DsRoute = sf.Get_Order_Booking_year(divcode,sfCode,FYear,subdiv_code);
        DataTable dsData = new DataTable();
        dsData.Columns.Add("Code", typeof(string));
        dsData.Columns.Add("Name", typeof(string));        
        dsData.Columns.Add("JAN", typeof(decimal));
        dsData.Columns.Add("FEB", typeof(decimal));
        dsData.Columns.Add("MAR", typeof(decimal));
        dsData.Columns.Add("APR", typeof(decimal));
        dsData.Columns.Add("MAY", typeof(decimal));
        dsData.Columns.Add("JUN", typeof(decimal));
        dsData.Columns.Add("JUL", typeof(decimal));
        dsData.Columns.Add("AUG", typeof(decimal));
        dsData.Columns.Add("SEP", typeof(decimal));
        dsData.Columns.Add("OCT", typeof(decimal));
        dsData.Columns.Add("NOV", typeof(decimal));
        dsData.Columns.Add("DEC", typeof(decimal));
        dsData.Columns.Add("TOTAL", typeof(decimal));
        
      
        decimal jan_tot = 0;
        decimal feb_tot = 0;
        decimal mar_tot = 0;
        decimal apr_tot = 0;
        decimal may_tot = 0;
        decimal jun_tot = 0;
        decimal jul_tot = 0;
        decimal aug_tot = 0;
        decimal sep_tot = 0;
        decimal oct_tot = 0;
        decimal nov_tot = 0;
        decimal dec_tot = 0;

        foreach (DataRow row in DsRoute.Tables[0].Rows)
        {
          
          
                    decimal jan_val = row["jan"] == DBNull.Value ? 0 : Convert.ToDecimal(row["jan"]);
                    jan_tot += jan_val;
                   
                    decimal feb_val = row["feb"] == DBNull.Value ? 0 : Convert.ToDecimal(row["feb"]);
                    feb_tot += feb_val;
                   

                    decimal mar_val = row["mar"] == DBNull.Value ? 0 : Convert.ToDecimal(row["mar"]);
                    mar_tot += mar_val;
                  
                    decimal apr_val = row["apr"] == DBNull.Value ? 0 : Convert.ToDecimal(row["apr"]);
                    apr_tot += apr_val;
                   
                    decimal may_val = row["may"] == DBNull.Value ? 0 : Convert.ToDecimal(row["may"]);
                    may_tot += may_val;
                  
                    decimal jun_val = row["jun"] == DBNull.Value ? 0 : Convert.ToDecimal(row["jun"]);
                    jun_tot += jun_val;
                   
                    decimal jul_val = row["jul"] == DBNull.Value ? 0 : Convert.ToDecimal(row["jul"]);
                    jul_tot += jul_val;
                    
                    decimal aug_val = row["aug"] == DBNull.Value ? 0 : Convert.ToDecimal(row["aug"]);
                    aug_tot += aug_val;
                   
                    decimal sep_val = row["sep"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sep"]);
                    sep_tot += sep_val;
                    
                    decimal oct_val = row["oct"] == DBNull.Value ? 0 : Convert.ToDecimal(row["oct"]);
                    oct_tot += oct_val;
                  
                    decimal nov_val = row["nov"] == DBNull.Value ? 0 : Convert.ToDecimal(row["nov"]);
                    nov_tot += nov_val;
                    
                    decimal dec_val = row["dec"] == DBNull.Value ? 0 : Convert.ToDecimal(row["dec"]);
                    dec_tot += dec_val;
                   
                    decimal tot = jan_val + feb_val + mar_val + apr_val + may_val + jun_val + jul_val + aug_val + sep_val + oct_val + nov_val + dec_val;
                    dsData.Rows.Add(row["sf"].ToString(), row["sf_name"].ToString(), jan_val, feb_val, mar_val, apr_val, may_val, jun_val, jul_val, aug_val, sep_val, oct_val, nov_val, dec_val,tot);

                }
           
           
        decimal tot_ov = jan_tot + feb_tot + mar_tot + apr_tot + may_tot + jun_tot + jul_tot + aug_tot + sep_tot + oct_tot + nov_tot + dec_tot;
            dsData.Rows.Add("", "TOTAL", jan_tot, feb_tot, mar_tot, apr_tot, may_tot, jun_tot, jul_tot, aug_tot, sep_tot, oct_tot, nov_tot, dec_tot,tot_ov);
    

        GV_DATA.DataSource = dsData;
        GV_DATA.DataBind();

    }

 protected void Dgv_SKU_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridViewRow grv = e.Row;
            if (grv.Cells[0].Text.Equals("0"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#39435C");
                e.Row.ForeColor = System.Drawing.Color.FromName("#fff");
                e.Row.Font.Bold = true;
            }
            if (grv.Cells[1].Text.Equals("TOTAL"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#D0ECE7");
                e.Row.Font.Bold = true;
            }
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[0].Width = 250;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[1].Width = 300;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            //e.Row.Cells[2].Width = 200;
            //e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        }
        catch (Exception ex)
        { }

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
        string attachment = "attachment; filename=" + strFileName + ".xls";
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
