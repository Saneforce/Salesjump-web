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
public partial class MIS_Reports_rpt_retailer_remark_count : System.Web.UI.Page
{
    string divcode = string.Empty;
    string remarks = string.Empty;
    string remarks_id = string.Empty;
    string sf_code = string.Empty;
    string fdate = string.Empty;
    string todate = string.Empty;
    string pcode = string.Empty;
    string pname = string.Empty;
    string currentremark = string.Empty;

    DataSet ss = new DataSet();
    DataSet ff = new DataSet();

    DataSet dsGV = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        remarks_id = Request.QueryString["remarks_id"].ToString();
        remarks = Request.QueryString["remarks"].ToString();
        fdate = Request.QueryString["fdate"].ToString();
        todate = Request.QueryString["todate"].ToString();
        pcode = Request.QueryString["pcode"].ToString();
        pname = Request.QueryString["pname"].ToString();
        FillRetailer();

        DCR dr = new DCR();

        DateTime d1 = Convert.ToDateTime(fdate);
        DateTime d2 = Convert.ToDateTime(todate);

        lblHead.Text = "Reasonwise Retailer List from " + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");

        Field.Text = "Product: " + pname + " and " + " Reason: " + remarks;
    }
    private void FillRetailer()
    {


        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();

        string stCrtDtaPnt = string.Empty; 


        ff = dc.retailerreason(divcode, remarks_id, sf_code, fdate, todate,pcode);
        if (ff.Tables[0].Rows.Count > 0)
        {
            // dsGV.Tables[0].Columns.RemoveAt(1);
            gvincentive.DataSource = ff;
            gvincentive.DataBind();
        }
        else
        {
            gvincentive.DataSource = null;
            gvincentive.DataBind();
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
            //HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");


            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Date";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Route";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Retailer Name";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Retailer Address";
            HeaderGridRow0.Cells.Add(HeaderCell);



            gvincentive.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }
         if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableRow tableRow = new TableRow();
            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
                try
                {
                if (currentremark != dt.Rows[e.Row.RowIndex].ItemArray[4].ToString())
                    {
                            TableCell tableCell = new TableCell();
                            tableCell.Attributes["style"] = "font: Andalus";
                            tableCell.Attributes["style"] = "font: Bold";
                            tableCell.CssClass = "remark";
                        DataRow[] drp = ff.Tables[0].Select("Remarks_Content='" + dt.Rows[e.Row.RowIndex].ItemArray[4].ToString() + "'");
                        if (drp.Length > 0)
                        {

                            tableCell.RowSpan = drp.Length;
                            tableCell.Text = drp[0]["Remarks_Content"].ToString();
                        }
                    else {
                        tableCell.Text = "";
                    }
                            e.Row.Cells.AddAt(0, tableCell);
                        currentremark = dt.Rows[e.Row.RowIndex].ItemArray[4].ToString();
                    }
                }
                catch { }

                

            gvincentive.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }
    }
    

    protected void gridView_PreRender(object sender, EventArgs e)
    {
        //  GridDecorator.MergeRows(gvincentive);
    }



    public class GridDecorator
    {
        public static void MergeRows(GridView gridView)
        {
            for (int rowIndex = 0; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int i = row.Cells.Count - 1; i < row.Cells.Count; i++)
                {
                    if (row.Cells[i].Text == previousRow.Cells[i].Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                               previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
            }
        }
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
        string attachment = "attachment; filename=Reasonwise Retailer List(" + remarks + ")" + fdate + "to" + todate + ".xls";
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