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
using DBase_EReport;
using System.Xml;
using System.Xml.XPath;
using System.Net;
public partial class MIS_Reports_tsr_RptSchemeView : System.Web.UI.Page
{

    DataSet dsSalesForce = null;
    DataSet dsDCR = null;
    DataSet dsTerritory = null;
    DataSet dsdoc = null;
    DataSet dssf = null;
    string div_code = string.Empty;
    string strDelay = string.Empty;
    string sf_code = string.Empty;
    string Sf_Name = string.Empty;
    string strMode = string.Empty;
    string sURL = string.Empty;
    string Sf_HQ = string.Empty;
    string FDate = string.Empty;
    string TDate = string.Empty;
    int cmonth = -1;
    int cyear = -1;
    int tot_days = -1;
    int cday = 1;
    int iCount = -1;
    int iFieldWrkCount = -1;
    string sDCR = string.Empty;
    public static string distcode = string.Empty;
    public static string distnm = string.Empty;
    public static string stcode = string.Empty;
    public static string stnm = string.Empty;
    public static string modeidenty = string.Empty;
    public static string Zone_code = string.Empty;
    public static string Area_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        //Sf_Name = Request.QueryString["Sf_Name"].ToString();
        //Zone_code = Request.QueryString["Zone_code"].ToString();
        //Area_code = Request.QueryString["Area_code"].ToString();
        // cmonth = Convert.ToInt16(Request.QueryString["cur_month"].ToString());
        //cyear = Convert.ToInt16(Request.QueryString["cur_year"].ToString());
        FDate = Request.QueryString["FDate"].ToString();
        TDate = Request.QueryString["TDate"].ToString();
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        Sf_Name = Request.QueryString["Sf_Name"].ToString();
        cmonth = 0;
        cyear = 0;
        distcode = Request.QueryString["Dst_code"].ToString();
        distnm = Request.QueryString["Dst_name"].ToString();
        stnm = Request.QueryString["st_name"].ToString();
        stcode = Request.QueryString["st_code"].ToString();       
        strMode = Request.QueryString["Mode"].ToString();



        //strMode = Request.QueryString["Mode"].ToString();
        //strMode = strMode.Trim();
        //distcode = Request.QueryString["Dst_code"].ToString();
        //distnm = Request.QueryString["Dst_name"].ToString();
        //stnm = Request.QueryString["st_name"].ToString();
        //stcode = Request.QueryString["st_code"].ToString();
        //modeidenty = Request.QueryString["mode_identi"].ToString();

        //if (strMode == "Scheme View")
        //{
        lblTitle.Text = "Scheme View For The Day Of <span style='color:Red'>" + "( " + FDate + " ) - ( " + TDate + ")" + "</span>";
        //lblFieldForceName.Text = Sf_Name;
        DataSet dsGV = new DataSet();
        DCR dc = new DCR();

        //dsGV = dc.GetEventCap_MGR(div_code, sf_code, FDate,TDate);
        //dsGV = dc.GetEventCap_MGR(div_code, sf_code, FDate, TDate, stcode, modeidenty);


        int divcode = 0;

        if (div_code == "" || div_code == null)
        { divcode = 0; }
        else { divcode = Convert.ToInt32(divcode); }

        if (FDate == "" || FDate == null)
        { FDate = Convert.ToString(DateTime.Now.Date); }

        if (TDate == "" || TDate == null)
        { TDate = Convert.ToString(DateTime.Now.Date); }

        if (sf_code == null)
        { sf_code = "admin"; }

        if (Zone_code == null)
        { Zone_code = "0"; }

        if (Area_code == null)
        { Area_code = "0"; }

        //dsGV = dc.GetTsrSchemeReport(sf_code, Zone_code, Area_code, div_code, FDate, TDate);

        dsGV = dc.GetTsrSchemeReport(div_code, sf_code, FDate, TDate, stcode);

        if (dsGV.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = dsGV;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        lblHead.Visible = false;
        //}


        ExportButton();

    }


    private void ExportButton()
    {
        btnClose.Visible = true;
        btnPrint.Visible = true;
        btnExcel.Visible = true;
        //btnPDF.Visible = true;
    }



    protected void btnExcel_Click(object sender, EventArgs e)
    {

        string attachment = "attachment; filename=SchemeReport_" + DateTime.Now.ToString("dd/MM/yyyy") + ".xls";
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

        //Response.Clear();

        //Response.Buffer = true;

        //Response.AddHeader("content-disposition",

        // "attachment;filename=GridViewExport.xls");

        //Response.Charset = "";

        //Response.ContentType = "application/vnd.ms-excel";

        //StringWriter sw = new StringWriter();

        //HtmlTextWriter hw = new HtmlTextWriter(sw);

        //GridView1.AllowPaging = false;        GridView1.DataBind();
        //for (int i = 0; i < GridView1.Rows.Count; i++)
        //{

        //    GridViewRow row = GridView1.Rows[i];
        //    //Apply text style to each Row
        //    row.Attributes.Add("class", "textmode");
        //}
        //GridView1.RenderControl(hw);
        ////style to format numbers to string
        //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        //Response.Write(style);
        //Response.Output.Write(sw.ToString());
        //Response.Flush();
        //Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

        /* Verifies that the control is rendered */

    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "SchemeReport";
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);
        iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
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

}