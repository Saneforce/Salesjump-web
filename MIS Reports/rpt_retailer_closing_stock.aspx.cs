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

public partial class MIS_Reports_rpt_retailer_closing_stock : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string todate = string.Empty;
    string date = string.Empty;
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
    protected void Page_Load(object sender, EventArgs e)
    {


        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["SF_Code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        date = Request.QueryString["date"].ToString();
        todate = Request.QueryString["tdate"].ToString();
       
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //string strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString().Substring(0, 3);
        lblHead.Text = "Retailerwise Closing Stock between  " + date + " and  " + todate + "";
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
        dsGV = dc.retailer_closing_stock_value(Sf_Code, divcode, date, todate);
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            dsGV.Tables[0].Columns.RemoveAt(1);
            dsGV.Tables[0].Columns.RemoveAt(3);
            dsGV.Tables[0].Columns.RemoveAt(3);
            gvclosingstockanalysis.DataSource = dsGV;
            gvclosingstockanalysis.DataBind();
        }
        else
        {
            gvclosingstockanalysis.DataSource = null;
            gvclosingstockanalysis.DataBind();
        }
        //Label1.Visible = false;

        //}
        //else
        //{




        //dsGV = dc.view_total_order_view(divcode, sfCode, Date);
        //if (dsGV.Tables[0].Rows.Count > 0)
        //{
        //    gvtotalorder.DataSource = dsGV;
        //    gvtotalorder.DataBind();
        //}
        //else
        //{
        //    gvtotalorder.DataSource = null;
        //    gvtotalorder.DataBind();
        //}


        // }
    }
    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {

       

            //SalesForce SF1 = new SalesForce();
            //DataSet ff1 = new DataSet();
            //ff1 = SF1.GetProduct_Name(divcode);
            //int cnt = ff1.Tables[0].Rows.Count;
            //foreach (DataRow drdoc in ff1.Tables[0].Rows)
            //{
            ////for (int j = 0; j < cnt; j++)
            ////{

            //        string prdt_code = drdoc["Product_Detail_Code"].ToString();
            //        string stock_code = Convert.ToString(orderId);
            //        DataSet dm = new DataSet();
            //        dm = SF1.GetDistNamewise1(divcode, stock_code);

            //        TableCell txt1 = new TableCell();
            //        Literal fflit = new Literal();
            //        fflit.Text = dm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            //        //txt1.ID = "txtquantity_";
            //        txt1.Controls.Add(fflit);
            //        //txt1.
            //        e.Row.Cells.Add(txt1);
            //    //}
            //}
            //Response.Write(gvtotalorder.Rows[0].Cells[2].Text);
            
        SalesForce SF = new SalesForce();
        DataSet ff = new DataSet();
        ff = SF.GetProduct_Name(divcode);
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
            HeaderCell.BackColor = System.Drawing.Color.FromName("#6B7794");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Date";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
			HeaderCell = new TableCell();
            HeaderCell.Width = 310;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#6B7794");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Retailer Code";
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


            foreach (DataRow drdoctor in ff.Tables[0].Rows)
            {
                HeaderCell = new TableCell();
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");

                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderCell.Attributes["style"] = "font: Bold";
                HeaderCell.Text = drdoctor["Product_Detail_Name"].ToString();
                HeaderCell.ColumnSpan = 3;
                HeaderGridRow0.Cells.Add(HeaderCell);
            }



            gvclosingstockanalysis.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            for (int i = 0; i < ff.Tables[0].Rows.Count; i++)
            {
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "O.Qty";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gvclosingstockanalysis.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "CL.Qty";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gvclosingstockanalysis.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "CURR.Qty";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gvclosingstockanalysis.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            }
            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }

    }
    protected void gridView_PreRender(object sender, EventArgs e)
    {
      //  GridDecorator.MergeRows(gvclosingstockanalysis);
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

    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=RetailerClosingstock.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            //gvclosingstockanalysis.AllowPaging = false;
            //this.BindGrid();

            gvclosingstockanalysis.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvclosingstockanalysis.HeaderRow.Cells)
            {
                cell.BackColor = gvclosingstockanalysis.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in gvclosingstockanalysis.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = gvclosingstockanalysis.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = gvclosingstockanalysis.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            gvclosingstockanalysis.RenderControl(hw);

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



    protected void btnClose_Click(object sender, EventArgs e)
    {

    }


}