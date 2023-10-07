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

public partial class MIS_Reports_Quiz_Result_Daywise : System.Web.UI.Page
{
    string divcode = string.Empty;
    string Sf_Code = string.Empty;
    string sfname = string.Empty;
    string subdiv_code = string.Empty;
    string Yr = string.Empty;
    string Mnth = string.Empty;
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
    string mnthname = string.Empty;
    Int64[] TQty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["SF_code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();

        Feild.Text = sfname;
         Mnth = Request.QueryString["fdt"].ToString();
        Yr = Request.QueryString["tdt"].ToString();
        lblHead.Text = "Quiz Result for " + Mnth + "To" + Yr;
       
        FillSF();
    }


    private void FillSF()
    {
        DataSet dsGV = new DataSet();
        SalesForce SF = new SalesForce();
        ff = new DataSet();
        ss = new DataSet();
        ff = SF.GetQuiz_Dates(Yr, Mnth);
        TQty = new Int64[ff.Tables[0].Rows.Count];
        dsGV = SF.getusrList_All(divcode, subdiv_code, Sf_Code);
        DataTable dfilt = dsGV.Tables[0];
        DataView dataView = dfilt.DefaultView;
        dataView.RowFilter = "Status='Active'";
        dfilt = dataView.ToTable();
        ss = SF.getQuizResult(divcode, Sf_Code, Mnth, Yr);
        if (dfilt.Rows.Count > 0)
        {
             dfilt.Columns.RemoveAt(2);
            dfilt.Columns.RemoveAt(2);
            dfilt.Columns.RemoveAt(2);
            dfilt.Columns.RemoveAt(2);
            dfilt.Columns.RemoveAt(3);
            dfilt.Columns.RemoveAt(3);
            dfilt.Columns.RemoveAt(2);
            dfilt.Columns.RemoveAt(2);
            dfilt.Columns.RemoveAt(2);
            dfilt.Columns.RemoveAt(2);
            gridcnt = dfilt.Columns.Count;
            gdprimary.DataSource = dfilt;
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
            HeaderCell.Width = 310;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "FieldForce Name";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            foreach (DataRow drdoctor in ff.Tables[0].Rows)
            {
                HeaderCell = new TableCell();
                HeaderCell.Width = 110;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");

                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderCell.Attributes["style"] = "font: Bold";
                HeaderCell.Text = drdoctor["ExpDt"].ToString();
                HeaderCell.ColumnSpan = 3;
                HeaderGridRow0.Cells.AddAt(HeaderGridRow0.Cells.Count, HeaderCell);
            }
            gdprimary.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            for (int i = 0; i < ff.Tables[0].Rows.Count; i++)
            {
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Total Questions";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gdprimary.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "No of Correct Answer";// (1st Attempt)";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gdprimary.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Marks in (%)";// (1st Attempt)";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gdprimary.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                //HeaderCell = new TableCell();

                //HeaderCell.Width = 55;
                //HeaderCell.Height = 35;
                //HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                //HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                //HeaderCell.Text = "No of Correct Answer (2nd Attempt)";
                //HeaderGridRow1.Cells.Add(HeaderCell);
                //gdprimary.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                //HeaderCell = new TableCell();

                //HeaderCell.Width = 55;
                //HeaderCell.Height = 35;
                //HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                //HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                //HeaderCell.Text = "Marks in (%) (2nd Attempt)";
                //HeaderGridRow1.Cells.Add(HeaderCell);
                //gdprimary.Controls[0].Controls.AddAt(1, HeaderGridRow1);
            }
        }

        decimal subTotal2 = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            subTotal2 = 0;
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableRow tableRow = new TableRow();

            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            string Sfchk = Convert.ToString(dt.Rows[e.Row.RowIndex]["Sf_Code"]);
            string Sf_name = Convert.ToString(dt.Rows[e.Row.RowIndex]["SF_Name"]);
            e.Row.Cells[0].Visible = false;
            int jk = 0;
            foreach (DataRow drdoctor in ff.Tables[0].Rows)
            {
                try
                {
                    DataRow[] drp = ss.Tables[0].Select("Sf_Code='" + Sfchk + "' and dt='" + drdoctor["ExpDt"] + "'");

                    if (drp.Length > 0)
                    {
                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = drp[0]["TOTQ"].ToString();
                        e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell);

                        TableCell tableCell2 = new TableCell();
                        tableCell2.Attributes["style"] = "font: Andalus";
                        tableCell2.Attributes["style"] = "font: Bold";
                        tableCell2.Attributes["sfcode"] = drp[0]["Sf_Code"].ToString();
                        tableCell2.Attributes["dt"] = drp[0]["edt"].ToString();
                        tableCell2.Attributes["sfname"] = Sf_name;
                        tableCell2.CssClass = "retpro";
                        tableCell2.Text = drp[0]["FAt"].ToString();
                        e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell2);

                        TableCell tableCell3 = new TableCell();
                        tableCell3.Attributes["style"] = "font: Andalus";
                        tableCell3.Attributes["style"] = "font: Bold";
                        tableCell3.Text = drp[0]["FAP"].ToString();
                        e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell3);
                    }
                    else
                    {

                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = "";
                        e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell);

                        TableCell tableCell2 = new TableCell();
                        tableCell2.Attributes["style"] = "font: Andalus";
                        tableCell2.Attributes["style"] = "font: Bold";
                        tableCell2.Text = "";
                        e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell2);

                        TableCell tableCell3 = new TableCell();
                        tableCell3.Attributes["style"] = "font: Andalus";
                        tableCell3.Attributes["style"] = "font: Bold";
                        tableCell3.Text = "";
                        e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell3);
                    }
                }
                catch { }
                jk++;
            }
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
        string attachment = "attachment; filename=Quiz_Result.xls";
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