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


public partial class MIS_Reports_Rptretailerwiseofftake : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    string field_code = string.Empty;
    string field_name = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    Notice Addcomment = new Notice();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        field_code = Request.QueryString["feild_code"].ToString();
        field_name = Request.QueryString["feild_name"].ToString();
        lblretailer.Text = field_name;
        BindGridd();
    }
    protected void BindGridd()
    {

        Notice gg = new Notice();

        DataSet ff = new DataSet();
        DataSet ff1 = new DataSet();

        ff = gg.getretailerofftake_productwise(div_code,field_code);

        ff.Tables[0].Columns.RemoveAt(2);
        if (ff.Tables.Count > 0)
        {
            if (ff.Tables[0].Rows.Count > 0)
            {


                GridView1.DataSource = ff;
                GridView1.DataBind();
                GridView1.ControlStyle.Font.Size = 9;

            }
        }
    }
    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory SF = new Territory();
            DataSet ff = new DataSet();
            ff = SF.getProdName(div_code);
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                HeaderGridRow0.CssClass = "table-bordered";
                TableHeaderCell HeaderCell = new TableHeaderCell();
                HeaderCell.Width = 25;
                HeaderCell.Height = 35;
                HeaderCell.Font.Bold = true;
                //HeaderCell.ApplyStyle.
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");

                TableHeaderCell Distributor = new TableHeaderCell();
                HeaderCell.Width = 25;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Text = "S.No";

                HeaderGridRow0.Cells.Add(HeaderCell);
                HeaderCell = new TableHeaderCell();
                HeaderCell.Width = 230;
                HeaderCell.Height = 35;
                HeaderCell.Attributes["style"] = "font: Courier";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Text = "Retailer";
                //HeaderCell. = "Andalus";
                HeaderGridRow0.Cells.Add(HeaderCell);


                HeaderCell = new TableHeaderCell();
                HeaderCell.Width = 70;
                HeaderCell.Height = 35;
                HeaderCell.Attributes["style"] = "font: andalus";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Text = "Type";
                //HeaderCell. = "Andalus";
                HeaderGridRow0.Cells.Add(HeaderCell);

                foreach (DataRow drdoctor in ff.Tables[0].Rows)
                {
                    HeaderCell = new TableHeaderCell();
                    HeaderCell.Width = 90;
                    HeaderCell.Height = 35;
                    HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");

                    HeaderCell.Attributes["style"] = "font: Andalus";
                    HeaderCell.Attributes["style"] = "font: Bold";
                    HeaderCell.Text = drdoctor["Product_Short_Name"].ToString();
                    HeaderGridRow0.Cells.Add(HeaderCell);
                }
                GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow0);
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[2].Text == "OP") //Here is the condition!
                {
                    //   
                    //Change the cell color.
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                    //
                    //Change the back color.
                    e.Row.Cells[2].BackColor = Color.Yellow;

                }
            }
        }
    }
    protected void gridView_PreRender(object sender, EventArgs e)
    {
        GridDecorator.MergeRows(GridView1);
    }
    public class GridDecorator
    {
        DataSet dsGV = new DataSet();
        public static void MergeRows(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count -2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                // for (int i = 0; i < row.Cells.Count - 32; i++)
                // {
                if (row.Cells[1].Text == previousRow.Cells[1].Text)
                {
                    DataSet dsGV = new DataSet();
                    DCR dc = new DCR();
                    row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                           previousRow.Cells[0].RowSpan + 1;

                    previousRow.Cells[0].Visible = false;
                    row.Cells[1].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                           previousRow.Cells[0].RowSpan + 1;

                    previousRow.Cells[1].Visible = false;
                    //row.Cells[2].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                    //                       previousRow.Cells[0].RowSpan + 1;

                    //previousRow.Cells[2].Visible = false;
                }
                // }
            }
        }
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=retailerofftake.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            //gvclosingstockanalysis.AllowPaging = false;
            //this.BindGrid();

            GridView1.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in GridView1.HeaderRow.Cells)
            {
                cell.BackColor = GridView1.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView1.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
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



    protected void btnClose_Click(object sender, EventArgs e)
    {

    }


    protected void btsubmit_Click(object sender, EventArgs e)
    {

    }
}