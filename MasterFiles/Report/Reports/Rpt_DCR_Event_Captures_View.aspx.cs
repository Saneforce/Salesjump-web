using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using Bus_EReport;
using System.Text;


public partial class MasterFiles_Reports_Rpt_DCR_Event_Captures_View : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        Sf_Name = Request.QueryString["Sf_Name"].ToString();
        // cmonth = Convert.ToInt16(Request.QueryString["cur_month"].ToString());
        //cyear = Convert.ToInt16(Request.QueryString["cur_year"].ToString());
        FDate = Request.QueryString["FDate"].ToString();
        TDate = Request.QueryString["TDate"].ToString();
        strMode = Request.QueryString["Mode"].ToString();
        strMode = strMode.Trim();
        //distcode = Request.QueryString["Dst_code"].ToString();
        //distnm = Request.QueryString["Dst_name"].ToString();
        //stnm = Request.QueryString["st_name"].ToString();
        stcode = "0";
        modeidenty = Request.QueryString["Mode"].ToString();

        if (strMode == "Event Captures View")
        {
            lblTitle.Text = "Event Captures View For The Day Of <span style='color:Red'>" + "( " + FDate + " ) - ( " + TDate + ")" + "</span>";
            //lblFieldForceName.Text = Sf_Name;
            DataSet dsGV = new DataSet();
            DCR dc = new DCR();

            //dsGV = dc.GetEventCap_MGR(div_code, sf_code, FDate,TDate);
            dsGV = dc.GetEventCap_MGR(div_code, sf_code, FDate, TDate, stcode, modeidenty);
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

            //lblHead.Visible = false;
        }
        else if (strMode == "TSR Event Captures View")
        {
            lblTitle.Text = "Event Captures View For The Day Of <span style='color:Red'>" + "( " + FDate + " ) - ( " + TDate + ")" + "</span>";
            //lblFieldForceName.Text = Sf_Name;
            DataSet dsGV = new DataSet();
            DCR dc = new DCR();

            //dsGV = dc.GetEventCap_MGR(div_code, sf_code, FDate,TDate);
            dsGV = dc.GetEventCap_MGR(div_code, sf_code, FDate, TDate, stcode, modeidenty);
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

            //lblHead.Visible = false;
        }

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

        //string attachment = "attachment; filename=Event_Captures_" + DateTime.Now.ToString("dd/MM/yyyy") + ".xls";


        string attachment = "attachment; filename=Event_Captures_" + DateTime.Now.ToString("dd/MM/yyyy") + ".xls";
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

        //Response.ClearContent();
        //Response.AddHeader("content-disposition", attachment);
        //Response.ContentType = "application/ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        //HtmlForm frm = new HtmlForm();
        //form1.Parent.Controls.Add(frm);
        //frm.Attributes["runat"] = "server";
        //frm.Controls.Add(pnlContents);
        //frm.RenderControl(htw);
        //Response.Write(sw.ToString());
        //Response.End();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string html = "<table>";
        //add header row
        html += "<tr>";
        for (int i = 0; i < GridView1.Columns.Count; i++)
            html += "<td>" + GridView1.Columns[i].HeaderText + "</td>";
        html += "</tr>";

        //add rows
        for (int count = 0; count < GridView1.Rows.Count; count++)
        {

            string sno = ((Label)GridView1.Rows[count].FindControl("lblId")).Text.ToString();

        }

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            html += "<tr>";
            for (int j = 0; j < GridView1.Columns.Count; j++)
                html += "<td>" + ((Label)GridView1.Rows[i].Cells[j].FindControl("lblId")).Text.ToString() + "</td>";

            html += "</tr>";
        }
        html += "</table>";

        StringBuilder sb = new StringBuilder();
        sb.Append(html);


        string strFileName = "EventCaptures";

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        

        StringReader sr = new StringReader(sw.ToString());
        //StringReader sr = new StringReader(sb.ToString());
        Document pdfDoc = new Document(PageSize.A2, 7f, 7f, 7f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
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