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


public partial class MIS_Reports_rpt_cuswsprod : System.Web.UI.Page
{
    string divcode = string.Empty;
    string date = null;
    string distcode = null;
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
    DataSet dsGV = new DataSet();
    DataSet ff = new DataSet();
    DataSet ss = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {

        
        divcode = Session["div_code"].ToString();
        date = Request.QueryString["Date"].ToString();
        distcode = Request.QueryString["Distcode"].ToString();

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //string strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString().Substring(0, 3);
        lblHead.Text = "Customerwise Product Details ";
        FillSF();


    }



    private void FillSF()
    {


        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();

        string stCrtDtaPnt = string.Empty;
        //if (sf_type == "4")
        //{     
        

        dsGV = dc.getcusproduct(date, distcode);
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            dsGV.Tables[0].Columns.RemoveAt(1);
            gvincentive.DataSource = dsGV;
            gvincentive.DataBind();
        }
        else
        {
            gvincentive.DataSource = null;
            gvincentive.DataBind();
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
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Retailer";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Address";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Mobile";
            HeaderGridRow0.Cells.Add(HeaderCell);

            
                for(int i=3;i<dsGV.Tables[0].Columns.Count;i++)
                {

                    HeaderCell = new TableCell();
                    HeaderCell.Height = 35;
                    HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                    HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");

                    HeaderCell.Attributes["style"] = "font: Andalus";
                    HeaderCell.Attributes["style"] = "font: Bold";
                    HeaderCell.Text = dsGV.Tables[0].Columns[i].ToString();
                    HeaderGridRow0.Cells.Add(HeaderCell);
                }
            

            gvincentive.Controls[0].Controls.AddAt(0, HeaderGridRow0);
            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }



       /* if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableRow tableRow = new TableRow();

            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            string Stkcode = Convert.ToString(dt.Rows[e.Row.RowIndex]["Stockist_Code"]);
            string Orderdate = Convert.ToString(dt.Rows[e.Row.RowIndex]["Order_Date"]);
            e.Row.Cells[1].Visible = false;
            int jk = 0;
            foreach (DataRow drdoctor in ff.Tables[0].Rows)
            {
                try
                {
                    DataRow[] drp = ss.Tables[0].Select("Order_Date='" + Orderdate + "' and Stockist_Code='" + Stkcode + "' and UOM_Weight='" + drdoctor["UOM_Weight"] + "'");


                    if (drp.Length > 0)
                    {

                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = drp[0]["QTY"].ToString();// +" ( " + drdoctor["Product_Detail_Code"] + ")";
                        e.Row.Cells.Add(tableCell);
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


            gvincentive.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        } */

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

    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=CustomerwiseProductDetails.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            //gvincentive.AllowPaging = false;
            //this.BindGrid();

            gvincentive.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvincentive.HeaderRow.Cells)
            {
                cell.BackColor = gvincentive.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in gvincentive.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = gvincentive.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = gvincentive.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            gvincentive.RenderControl(hw);

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