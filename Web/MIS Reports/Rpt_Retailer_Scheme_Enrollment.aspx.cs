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

public partial class MIS_Reports_Rpt_Retailer_Scheme_Enrollment : System.Web.UI.Page
{
    string divcode = string.Empty;
    string Sf_Code = string.Empty;
    string sfname = string.Empty;
    string subdiv_code = string.Empty;
    string FDate = string.Empty;
    string TDate = string.Empty;
    DataSet ff = new DataSet();
    DataSet ss = new DataSet();
    int gridcnt = 0;
    string sfID = string.Empty;
    string currentsfid = string.Empty;
    decimal subTotal1 = 0;
    decimal nettotal1 = 0;
    decimal total = 0;
    decimal nttotal = 0;
    int subTotalRowIndex = 0;
    decimal subTotal = 0;
    decimal nettotal = 0;
    Int64[] TQty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["SF_code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();

        Feild.Text = sfname;
        TDate = Request.QueryString["ToDate"].ToString();
        FDate = Request.QueryString["FromDate"].ToString();
        lblHead.Text = "Retailer Scheme Enrollment " + FDate + " to " + TDate;
        FillSF();
    }


    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        Sales dc = new Sales();

        string stCrtDtaPnt = string.Empty;
        //if (sf_type == "4")
        //{
        SalesForce SF = new SalesForce();
        ff = new DataSet();
        //ff = SF.GetProduct_Name(divcode, subdiv_code);

        //TQty = new Int64[ff.Tables[0].Rows.Count];

        ss = new DataSet();
        dsGV = dc.getRetailerSlabSummary_Dets(divcode, Sf_Code, FDate, TDate, subdiv_code);


        //ss = dc.primarReturnDetail(divcode, Sf_Code, FDate, TDate, subdiv_code);
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            dsGV.Tables[0].Columns.RemoveAt(0);
            gridcnt = dsGV.Tables[0].Columns.Count;
            gdprimary.DataSource = dsGV;
            gdprimary.DataBind();
        }
        else
        {
            gdprimary.DataSource = null;
            gdprimary.DataBind();
        }

    }

    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {

        subTotal1 = 0;
        nettotal1 = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            //total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Order_value"]);
            //nttotal += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["net_weight_value"]);
            //sfID = Convert.ToString(dt.Rows[e.Row.RowIndex]["Sf_Name"]);
        }



        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            GridViewRow HeaderGridRow1 = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow1.ForeColor = System.Drawing.Color.Black;
            TableCell HeaderCell = new TableCell();
            //HeaderCell.Width = 25;
            //HeaderCell.Height = 35;
            HeaderCell.Font.Bold = true;
            //HeaderCell.ApplyStyle.
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");




            HeaderCell = new TableCell();
            HeaderCell.Width = 15;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "SlNO";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 150;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Retailer Name";
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 150;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Mapped FieldForce";
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Route";
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Retailer Slab";
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Order Value";
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Achieved";
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderGridRow0.Cells.Add(HeaderCell);
            gdprimary.Controls[0].Controls.AddAt(0, HeaderGridRow0);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableRow tableRow = new TableRow();

            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            e.Row.Cells[5].Attributes["style"] = "text-align:right";
            //int jk = 0;
            //foreach (DataRow drdoctor in ff.Tables[0].Rows)
            //{
            //    try
            //    {
            //        DataRow[] drp = ss.Tables[0].Select("SF='" + Sfchk + "' and ProdCode='" + drdoctor["Product_Detail_Code"] + "'");

            //        if (drp.Length > 0)
            //        {
            //            subTotal2 += Convert.ToDecimal(drp[0]["Price"]);
            //            TableCell tableCell = new TableCell();
            //            tableCell.Attributes["style"] = "font: Andalus";
            //            tableCell.Attributes["style"] = "font: Bold";
            //            tableCell.Attributes["PCode"] = drp[0]["ProdCode"].ToString();
            //            tableCell.Attributes["Fdt"] = FDate;
            //            tableCell.Attributes["tdt"] = TDate;
            //            tableCell.Attributes["Pname"] = drdoctor["Product_Detail_Name"].ToString();
            //            tableCell.Attributes["SF"] = drp[0]["SF"].ToString();
            //            tableCell.CssClass = "retpro";
            //            tableCell.Text = drp[0]["Qty"].ToString();// +" ( " + drdoctor["Product_Detail_Code"] + ")";
            //            e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell);

            //            TQty[jk] += Convert.ToInt64(drp[0]["Qty"]);
            //        }
            //        else
            //        {

            //            TableCell tableCell = new TableCell();
            //            tableCell.Attributes["style"] = "font: Andalus";
            //            tableCell.Attributes["style"] = "font: Bold";
            //            tableCell.Text = "";
            //            e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell);
            //        }
            //    }
            //    catch { }
            //    jk++;
            //}
            //TableCell tableCel = new TableCell();
            //tableCel.Attributes["style"] = "font: Andalus";
            //tableCel.Attributes["style"] = "font: Bold";
            //tableCel.Text = subTotal2.ToString();
            //nettotal += subTotal2;
            //e.Row.Cells.AddAt(e.Row.Cells.Count, tableCel);
            //subTotal2 = 0;
        }
    }

    protected void OnDataBound(object sender, EventArgs e)
    {

        //GridViewRow rw = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

        //TableCell TotalCell = new TableCell();

        //TotalCell.Width = 55;
        //TotalCell.Height = 35;
        //TotalCell.BackColor = System.Drawing.Color.FromName("#ECF19F");
        //TotalCell.ForeColor = System.Drawing.Color.FromName("#000");
        //TotalCell.ColumnSpan = 1;
        //TotalCell.Text = "Total";
        //rw.Cells.Add(TotalCell);
        //TableCell HeaderCell = new TableCell();
        //for (int i = 0; i < TQty.Length; i++)
        //{
        //    TableCell HeaderCell1 = new TableCell();

        //    HeaderCell1.Width = 55;
        //    HeaderCell1.Height = 35;
        //    HeaderCell1.BackColor = System.Drawing.Color.FromName("#ECF19F");
        //    HeaderCell1.ForeColor = System.Drawing.Color.FromName("#000");
        //    HeaderCell1.Text = TQty[i].ToString();
        //    rw.Cells.Add(HeaderCell1);
        //}

        //HeaderCell.Width = 55;
        //HeaderCell.Height = 35;
        //HeaderCell.BackColor = System.Drawing.Color.FromName("#ECF19F");
        //HeaderCell.ForeColor = System.Drawing.Color.FromName("#000");
        //HeaderCell.Text = nettotal.ToString();
        //rw.Cells.Add(HeaderCell);

        //gdprimary.Controls[0].Controls.Add(rw);
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
        string attachment = "attachment; filename=Retailer_Scheme_Details.xls";
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