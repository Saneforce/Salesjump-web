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


public partial class MIS_Reports_rpt_orderwise_count : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string subdiv_code = string.Empty;
    int iTotLstCount1 = 0;
    int iTotLstCount = 0;
    int iTotLstCount2 = 0;
    string sMode = string.Empty;
    string Date = string.Empty;
    public string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string ChemistPOB_visit = string.Empty;
    string tot_Sub_days = string.Empty;
    string tot_dr = string.Empty;
    string Chemist_visit = string.Empty;
    string Stock_Visit = string.Empty;
    string tot_Stock_Calls_Seen = string.Empty;
    string tot_Dcr_Leave = string.Empty;
    string UnlistVisit = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string tot_doc_Unlstcalls_seen = string.Empty;
    string tot_CSH_calls_seen = string.Empty;
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;
    string MultiSf_Code = string.Empty;
    DataSet dsprd = new DataSet();
    string distributor_code = string.Empty;
    string Multi_Prod = string.Empty;
    string sCurrentDate = string.Empty;
    int currentId = 0;
    decimal subTotal = 0;
    decimal nettotal = 0;
    decimal nttotal = 0;
    decimal total = 0;
    int subTotalRowIndex = 0;
    int gridcnt = 0;
    DataSet dsPro = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        FYear = Request.QueryString["cur_year"].ToString();
        subdiv_code = Request.QueryString["subdivision"].ToString();

        lblHead.Text = "Secondary Order Unique Count for the Year " + FYear;
        Feild.Text = sfname;

        FillSF(divcode, sfCode, FYear);
    }

    private void FillSF(string divcode, string sfCode, string FYear)
    {
  
        Product p = new Product();
        dsPro = p.Get_Orderwise_count(divcode, sfCode, FYear);
        if (dsPro.Tables[0].Rows.Count > 0)
        {

            gridcnt = dsPro.Tables[0].Columns.Count;
            //dsGV.Tables[0].Columns[5].SetOrdinal(gridcnt-1);
            //dsGV.Tables[0].Columns[5].SetOrdinal(gridcnt-1);
            Order_Count_Grid.DataSource = dsPro;
            Order_Count_Grid.DataBind();
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //Session["ctrl"] = pnlContents;
        //Control ctrl = (Control)Session["ctrl"];
        //PrintWebControl(ctrl);
    }

    //public static void PrintWebControl(Control ControlToPrint)
    //{
    //    StringWriter stringWrite = new StringWriter();
    //    System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
    //    if (ControlToPrint is WebControl)
    //    {
    //        Unit w = new Unit(100, UnitType.Percentage);
    //        ((WebControl)ControlToPrint).Width = w;
    //    }
    //    Page pg = new Page();
    //    pg.EnableEventValidation = false;
    //    HtmlForm frm = new HtmlForm();
    //    pg.Controls.Add(frm);
    //    frm.Attributes.Add("runat", "server");
    //    frm.Controls.Add(ControlToPrint);
    //    pg.DesignerInitialize();
    //    pg.RenderControl(htmlWrite);
    //    string strHTML = stringWrite.ToString();
    //    HttpContext.Current.Response.Clear();
    //    HttpContext.Current.Response.Write(strHTML);
    //    HttpContext.Current.Response.Write("<script>window.print();</script>");
    //    HttpContext.Current.Response.End();

    //}


    protected void btnExport_Click(object sender, EventArgs e)
    {
        //string attachment = "attachment; filename=Secondary_OrderWise_Count.xls";
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

    protected void Order_Count_Grid_DataBound(object sender, EventArgs e)
    {

    }

    protected void Order_Count_Grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void Order_Count_Grid_DataBound1(object sender, EventArgs e)
    {

    }

    protected void Order_Count_Grid_RowDataBound1(object sender, GridViewRowEventArgs e)
    {

    }

    protected void Order_Count_Grid_RowCreated1(object sender, GridViewRowEventArgs e)
    {

       if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow0.ForeColor = System.Drawing.Color.Black;

            GridViewRow HeaderGridRow1 = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow1.ForeColor = System.Drawing.Color.Black;
            GridViewRow HeaderGridRow2 = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow2.ForeColor = System.Drawing.Color.Black;
            TableCell HeaderCell = new TableCell();

            //HeaderCell.Width = 25;
            //HeaderCell.Height = 35;
          //  HeaderCell.Font.Bold = true;
            //HeaderCell.ApplyStyle.
            //HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            //HeaderCell.Style.Add("Color", "White");
            //HeaderCell.ForeColor = System.Drawing.Color.White;
            //TableCell Distributor = new TableCell();

            //HeaderCell.Width = 10;
            //HeaderCell.Height = 35;
            //HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            //HeaderCell.Style.Add("Color", "White");
            //HeaderCell.Text = "S.No";
            //HeaderCell.RowSpan = 2;
            //HeaderGridRow0.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Width = 180;
            //HeaderCell.Height = 35;
            //HeaderCell.Attributes["style"] = "font: Andalus";
            //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderCell.RowSpan = 2;
            //HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            //HeaderCell.Style.Add("Color", "White");
            //HeaderCell.Text = "Field Force ID";
            ////HeaderCell. = "Andalus";
            //HeaderGridRow0.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Width = 180;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 1;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("text-align", "left");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "Field Force Name";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
          
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = " UPC Count Jan '" +FYear;
            HeaderCell.ColumnSpan = 1;
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = " UPC Count Feb '" + FYear;
            HeaderCell.ColumnSpan = 1;
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = " UPC Count Mar '" + FYear;
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = " UPC Count Apr '" + FYear;
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = " UPC Count May '" + FYear;
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = " UPC Count Jun '" + FYear; ;
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;

            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = " UPC Count Jul '" + FYear;
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.VerticalAlign = VerticalAlign.Middle;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = " UPC Count Aug '" + FYear;
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = " UPC Count Sep '" + FYear;
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = " UPC Count Oct '" + FYear;
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = " UPC Count Nov '" + FYear;
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = " UPC Count Dec '" + FYear;
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow0.Cells.Add(HeaderCell);
            Order_Count_Grid.Controls[0].Controls.AddAt(0, HeaderGridRow0);


      
        }
        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            string[] arr = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            TableRow tableRow = new TableRow();

            

            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;

           e.Row.Cells[0].Visible = false;
            string s_code = dt.Rows[e.Row.RowIndex]["SF_Code"].ToString();
            string s_name = dt.Rows[e.Row.RowIndex]["Sf_Name"].ToString();

            for (int a = 0; a < arr.Length; a++)
            {
                string Order_count = Convert.ToString(dt.Rows[e.Row.RowIndex][arr[a]]);

                e.Row.Cells[ Convert.ToInt32( 2+ a)].Attributes["style"] = "font: Andalus";
                e.Row.Cells[Convert.ToInt32( 2 +a)].Attributes["style"]= "font: Bold";
                e.Row.Cells[Convert.ToInt32(2 + a)].Attributes["Sf_Code"] = s_code;
                e.Row.Cells[Convert.ToInt32(2 + a)].Attributes["sf_name"] = s_name;
                e.Row.Cells[Convert.ToInt32(2 + a)].Attributes["month"] = Convert.ToInt32(a+1).ToString();
                e.Row.Cells[Convert.ToInt32(2 +a)].Attributes["Orders"] = Order_count;
                e.Row.Cells[Convert.ToInt32(2 + a)].Attributes["Month_Name"] = arr[a];
                //e.Row.Cells[Convert.ToInt32(3 + a)].Attributes["subdiv_code "] = sub;
                e.Row.Cells[Convert.ToInt32(2 + a)].CssClass = "retpro";

            }
        }
    }

}
