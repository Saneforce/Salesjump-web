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

public partial class MIS_Reports_View_Monthly_Order_Count_Details : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string Month = string.Empty;
    string Month_Name = string.Empty;
    DataSet ds = new DataSet();
	public string FYear = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["Sf_Code"].ToString();
        sfname = Request.QueryString["sfname"].ToString();
        Month = Request.QueryString["Month"].ToString();
		FYear = Request.QueryString["cur_year"].ToString();
        Month_Name = Request.QueryString["Month_Name"].ToString();
        lblHead.Text = "Secondary Order Unique Retailer Details - " + Month_Name;
        Feild.Text = sfname;

        filldetails(divcode,sfCode, Month);
    }

    public void filldetails(string divcode, string sfCode, string Month)
    {
        Product p = new Product();
        ds = p.Get_Orderwise_count_details(divcode,sfCode, Month, FYear);
        if (ds.Tables[0].Rows.Count > 0)
        {

            //gridcnt = ds.Tables[0].Columns.Count;
            //dsGV.Tables[0].Columns[5].SetOrdinal(gridcnt-1);
            //dsGV.Tables[0].Columns[5].SetOrdinal(gridcnt-1);
            Order_Count_Details.DataSource = ds;
            Order_Count_Details.DataBind();
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Secondary_OrderWise_Count.xls";
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

    protected void Order_Count_Details_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow0.ForeColor = System.Drawing.Color.Black;

            TableCell HeaderCell = new TableCell();

            HeaderCell.Width = 25;
            HeaderCell.Height = 35;
            HeaderCell.Font.Bold = true;
            //HeaderCell.ApplyStyle.
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.ForeColor = System.Drawing.Color.White;

            TableCell Distributor = new TableCell();

            //HeaderCell.Width = 25;
            //HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "Customer ID";
            // HeaderCell.RowSpan = 2;
            //HeaderCell.Visible = false;
            HeaderGridRow0.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Width = 180;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Andalus";
           // HeaderCell.HorizontalAlign = HorizontalAlign.Center;
           // HeaderCell.RowSpan = 2;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "Customer Name";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

           // HeaderCell = new TableCell();
           // HeaderCell.Width = 180;
           // HeaderCell.Height = 35;
           // HeaderCell.Attributes["style"] = "font: Andalus";
           //// HeaderCell.HorizontalAlign = HorizontalAlign.Center;
           //// HeaderCell.RowSpan = 2;
           // HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
           // HeaderCell.Style.Add("Color", "White");
           // HeaderCell.Text = "Stockist Name";
           // //HeaderCell. = "Andalus";
           // HeaderGridRow0.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Width = 180;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
           // HeaderCell.RowSpan = 2;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "Route_Name";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
          //  HeaderCell.ColumnSpan = 1;
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");

            HeaderCell.Text = "Order_Value";
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);
            Order_Count_Details.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }
    }
}
