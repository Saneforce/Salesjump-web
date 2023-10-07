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

public partial class MIS_Reports_rpt_new_retailer_closing_stock : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string todate = string.Empty;
    string date = null;
    string subdivcode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string tot_dr = string.Empty;
    string Monthsub = string.Empty;
    string Sf_Code = string.Empty;
    string MultiSf_Code = string.Empty;
    DataSet dsprd = new DataSet();
    string distributor_code = string.Empty;
    int subTotalRowIndex = 0;
    string sf_type = string.Empty;
    DataSet ff = new DataSet();
    DataSet ss = new DataSet();
    Int64[] TQty;

    decimal Total;
    protected void Page_Load(object sender, EventArgs e)
    {


        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["SF_Code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        date = Request.QueryString["date"].ToString();
        //todate = Request.QueryString["tdate"].ToString();

        string fromdate = date;
        DateTime d1 = Convert.ToDateTime(fromdate);

        lblHead.Text = "Date : " + d1.ToString("dd-MM-yyyy");

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //string strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString().Substring(0, 3);
        lblHead.Text = "Retailer Closing Stock on " + date;
        Feild.Text = sfname;
        FillSF();


    }



    private void FillSF()
    {


        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();

        string stCrtDtaPnt = string.Empty;
        //if (sf_type == "4")
        //{
        Order SF = new Order();
        ff = new DataSet();
        ff = SF.getclosstk(Sf_Code, divcode, date);

        ss = new DataSet();
        ss = SF.getclosstk_qty(Sf_Code, divcode, date);

        TQty = new Int64[ff.Tables[0].Rows.Count];

        //dsGV = dc.ffincentive(divcode, Sf_Code, date, todate);
        if (ff.Tables[0].Rows.Count > 0)
        {
            // dsGV.Tables[0].Columns.RemoveAt(1);
            gdclstk.DataSource = ff;
            gdclstk.DataBind();
        }
        else
        {
            gdclstk.DataSource = null;
            gdclstk.DataBind();
        }

    }
    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {




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
            HeaderCell.Width = 310;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Date";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            //HeaderCell.Width = 110;
            //HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Retailer";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            //HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            //HeaderCell.Text = "Territory";
            //HeaderCell.RowSpan = 2;
            //HeaderGridRow0.Cells.Add(HeaderCell);


            foreach (DataRow drdoctor in ff.Tables[1].Rows)
            {
                HeaderCell = new TableCell();
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");

                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderCell.Attributes["style"] = "font: Bold";
                HeaderCell.Text = drdoctor["Product_Name"].ToString();
                HeaderCell.ColumnSpan = 2;
                HeaderGridRow0.Cells.Add(HeaderCell);
            }



            gdclstk.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            for (int i = 0; i < ff.Tables[1].Rows.Count; i++)
            {
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Case";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gdclstk.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Piece";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gdclstk.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            }
            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableRow tableRow = new TableRow();

            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            string tslno = Convert.ToString(dt.Rows[e.Row.RowIndex]["Trans_Sl_No"]);
            string Orderdate = Convert.ToString(dt.Rows[e.Row.RowIndex]["Order_Date"]);
            e.Row.Cells[0].Visible = false;
            int jk = 0;
            foreach (DataRow drdoctor in ff.Tables[1].Rows)
            {
                try
                {
                    DataRow[] drp = ss.Tables[0].Select("Trans_Sl_No='" + tslno + "' and Product_Code='" + ff.Tables[1].Rows[jk]["Product_Code"] + "'");

                    if (drp.Length > 0)
                    {

                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = drp[0]["CClStock"].ToString();// +" ( " + drdoctor["Product_Detail_Code"] + ")";
                        e.Row.Cells.Add(tableCell);

                        //TQty[jk] += Convert.ToInt64(drp[0]["QTY"]);
                        TableCell tableCellBal = new TableCell();
                        tableCellBal.Attributes["style"] = "font: Andalus";
                        tableCellBal.Attributes["style"] = "font: Bold";
                        //tableCellBal.Text = (Convert.ToInt64(drdoctor["ClStock"])).ToString();// +" ( " + drdoctor["Product_Detail_Code"] + ")";                        
                        tableCellBal.Text = drp[0]["ClStock"].ToString();
                        e.Row.Cells.Add(tableCellBal);
                    }
                    else
                    {

                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = "0";
                        e.Row.Cells.Add(tableCell);

                        TableCell tableCellBal = new TableCell();
                        tableCellBal.Attributes["style"] = "font: Andalus";
                        tableCellBal.Attributes["style"] = "font: Bold";
                        tableCellBal.Text = "0";
                        e.Row.Cells.Add(tableCellBal);
                    }
                }
                catch { }
                jk++;
            }


            gdclstk.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }

    }

    protected void OnDataBound(object sender, EventArgs e)
    {

        //GridViewRow rw = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

        //TableCell TotalCell = new TableCell();

        //TotalCell.Width = 55;
        //TotalCell.Height = 35;
        //TotalCell.BackColor = System.Drawing.Color.FromName("#496a9a");
        //TotalCell.ForeColor = System.Drawing.Color.FromName("#fff");
        //TotalCell.ColumnSpan = 3;
        //TotalCell.Text = "Total";
        //rw.Cells.Add(TotalCell);
        //for (int i = 0; i < TQty.Length; i++)
        //{
        //    TableCell HeaderCell = new TableCell();

        //    HeaderCell.Width = 55;
        //    HeaderCell.Height = 35;
        //    HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
        //    HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
        //    HeaderCell.Text = TQty[i].ToString();
        //    rw.Cells.Add(HeaderCell);

        //    TableCell HeaderCell1 = new TableCell();

        //    HeaderCell1.Width = 55;
        //    HeaderCell1.Height = 35;
        //    HeaderCell1.BackColor = System.Drawing.Color.FromName("#496a9a");
        //    HeaderCell1.ForeColor = System.Drawing.Color.FromName("#fff");
        //    HeaderCell1.Text = "";
        //    rw.Cells.Add(HeaderCell1);
        //}

        //gdclstk.Controls[0].Controls.Add(rw);
    }


    protected void gridView_PreRender(object sender, EventArgs e)
    {
        //  GridDecorator.MergeRows(gdclstk);
    }





    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        // Session["ctrl"] = pnlContents;
        //  Control ctrl = (Control)Session["ctrl"];
        //   PrintWebControl(ctrl);
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
        string attachment = "attachment; filename=Retailer_Closing_Stock on "+ date +".xls";
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