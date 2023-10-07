using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using System.Web.UI.HtmlControls;
using System.Drawing;

public partial class MIS_Reports_rpt_van_stock_ledger : System.Web.UI.Page
{
    public string sf_code = string.Empty;
    public string divcode = string.Empty;
    public string sf_name = string.Empty;
    public string fdate = string.Empty;
    public string tdate = string.Empty;
    string subdivcode = string.Empty;
    DataSet dsgv = new DataSet();
    DataSet ff = new DataSet();
    DataSet ss = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sf_code = Request.QueryString["fieldforceval"].ToString();
        sf_name = Request.QueryString["Feildforce"].ToString();
        fdate = Request.QueryString["DATE"].ToString();
        tdate = Request.QueryString["TODATE"].ToString();
        lblHead.Text = "Van Stock Ledger for " + sf_name + " from " + fdate + " to " + tdate;
        Fillstock();
    }
    protected void Fillstock()
    {

        DCR dc = new DCR();
        divcode = Session["div_code"].ToString();

        //lblHead.Text = "Van Stock Ledger for " + sf_name + " from " + fdate + " to " + tdate + "-("+sf_code+", "+divcode+", "+fdate+", "+tdate+", "+subdivcode+")";
        dsgv = dc.vanstockledger(sf_code.Trim(), divcode, fdate, tdate, subdivcode);
        if (dsgv.Tables[0].Rows.Count > 0)
        {
            // dsGV.Tables[0].Columns.RemoveAt(1);
            //gridcnt = dsgv.Tables[0].Columns.Count;
            gdvan.DataSource = dsgv;
            gdvan.DataBind();
        }
        else
        {
            gdvan.DataSource = null;
            gdvan.DataBind();
        }
    }

    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {



        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    GridView HeaderGrid = (GridView)sender;
        //    GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

        //    TableRow tableRow = new TableRow();

        //    DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
        //    e.Row.Cells[0].Visible = false;
        //    gdvan.Controls[0].Controls.AddAt(1, HeaderGridRow0);
        //}
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
        string attachment = "attachment; filename=Van_Stock_Ledger " + sf_name + " from " + fdate + " to " + tdate + ".xls";
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
}