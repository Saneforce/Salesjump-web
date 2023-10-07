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
using System.Data.SqlClient;

public partial class MIS_Reports_Rpt_Stock_Sales_Progressive : System.Web.UI.Page
{
    string divcode = string.Empty;
    string Sf_Code = string.Empty;
    string sfname = string.Empty;
    string subdiv_code = string.Empty;
    string FYr = string.Empty;
    string TYr = string.Empty;
    string FMnth = string.Empty;
    string distcode = string.Empty;
    string distname = string.Empty;
    string TMnth = string.Empty;
    DataSet ff = new DataSet();
    DataSet mfgdt = new DataSet();
    DataSet sspri = new DataSet();
    DataSet sssec = new DataSet();
    DataSet dsstkphone = new DataSet();
    int gridcnt = 0;
    string Fmnthname = string.Empty;
    List<String> list = new List<String>();
    Decimal[] Sallit; 
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        subdiv_code = Request.QueryString["subdivision"].ToString();

        Sf_Code = Request.QueryString["SFCode"].ToString();
        sfname = Request.QueryString["SFName"].ToString();

        distname = Request.QueryString["Dist_Name"].ToString();
        distcode = Request.QueryString["Dist_Code"].ToString();

        FMnth = Request.QueryString["FMonth"].ToString();
        TMnth = Request.QueryString["TMonth"].ToString();

        FYr = Request.QueryString["FYear"].ToString();

        Fmnthname = Request.QueryString["FMnthName"].ToString();
        TYr = Request.QueryString["TMnthName"].ToString();
        lblHead.Text = "Stock and Sales Progressive from " + Fmnthname + "-" + FYr + " to " + TYr + "-" + FYr;
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select Stockist_Mobile from Mas_Stockist where Stockist_Code='" + distcode + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsstkphone);
        Feild.Text = "Distributor Name: " + distname; Label2.Text = "Phone No: " + dsstkphone.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        Label1.Text = "Fieldforce Name: " + sfname;
        FillSF();
    }


    private void FillSF()
    {
        list.Add("January");
        list.Add("Febuary");
        list.Add("March");
        list.Add("April");
        list.Add("May");
        list.Add("June");
        list.Add("July");
        list.Add("August");
        list.Add("September");
        list.Add("October");
        list.Add("November");
        list.Add("December");
        Sallit = new Decimal[list.Count];
        DataSet dsGV = new DataSet();
        Product pro = new Product();
        dsGV = pro.stksecsalesprod(divcode, subdiv_code);
        ff = new DataSet();
        ff = pro.stksecsalesprog(divcode, distcode, FMnth, TMnth, FYr);
        mfgdt = new DataSet();
        mfgdt = pro.stksecsalesmfgdt(distcode, FMnth, TMnth, FYr);
        sspri = pro.getSecSalesPriamry(distcode, FMnth, TMnth, FYr);
        //sssec = pro.getSecSalesSecondary(distcode, FMnth, TMnth, FYr);
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            //if (divcode == "100") { dsGV.Tables[0].Columns.RemoveAt(10); } else { dsGV.Tables[0].Columns.RemoveAt(1); };
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
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            GridViewRow HeaderGridRow1 = new GridViewRow(0, 1, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow1.ForeColor = System.Drawing.Color.Black;
            TableCell HeaderCell = new TableCell();
            HeaderCell.Font.Bold = true;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");

            HeaderCell = new TableCell();
            HeaderCell.Width = 59;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Product Name";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            for (int j = (Convert.ToInt16(FMnth)-1); j < Convert.ToInt16(TMnth); j++)
            {
                HeaderCell = new TableCell();
                HeaderCell.Width = 110;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");

                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderCell.Attributes["style"] = "font: Bold";
                HeaderCell.Text = list[j].ToString();
                HeaderCell.ColumnSpan = 6;
                HeaderGridRow0.Cells.AddAt(HeaderGridRow0.Cells.Count, HeaderCell);
            }
            gdprimary.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            for (int j = (Convert.ToInt16(FMnth) - 1); j < Convert.ToInt16(TMnth); j++)
            {
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "OP.STK";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gdprimary.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "PURCH";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gdprimary.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Total Stock";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gdprimary.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Sale";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gdprimary.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Cl.stock";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gdprimary.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "MFG Date";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gdprimary.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableRow tableRow = new TableRow();

            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            string Product_Detail_Code = Convert.ToString(dt.Rows[e.Row.RowIndex]["Product_Detail_Code"]);
            e.Row.Cells[0].Visible = false;
            int kj = 0;
            for (int j = (Convert.ToInt16(FMnth) - 1); j < Convert.ToInt16(TMnth); j++)
            {
                var jj = j - 1;
                if (jj < 0) { jj = 11; }
                try
                {
                    DataRow[] drp1 = ff.Tables[0].Select("Product_Code='" + Product_Detail_Code + "' and Mnth='" + list[j].ToString() + "'");
                    DataRow[] drp4 = ff.Tables[0].Select("Product_Code='" + Product_Detail_Code + "' and Mnth='" + list[jj].ToString() + "'");
                    DataRow[] drp2 = mfgdt.Tables[0].Select("Product_Code='" + Product_Detail_Code + "' and MN='" + list[j].ToString() + "'");
                    DataRow[] drp3 = sspri.Tables[0].Select("Pcode='" + Product_Detail_Code + "' and Mn='" + list[j].ToString() + "'");
                    //DataRow[] drp4 = sssec.Tables[0].Select("Pcode='" + Product_Detail_Code + "' and Mn='" + list[j].ToString() + "'");
                    decimal totstock = 0;
                    decimal sales = 0;
                    decimal saleslit = 0;
                    kj += ((drp1.Length > 0) ? drp1.Length : drp4.Length) + drp2.Length + drp3.Length;
                    if (drp1.Length > 0 || drp4.Length > 0)
                    {
                        totstock = Convert.ToDecimal((drp1.Length > 0) ? drp1[0]["OP"] : ((drp4.Length > 0) ? drp4[0]["ClStk"] : 0));
                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = ((drp1.Length > 0) ? drp1[0]["OP"].ToString() : ((drp4.Length > 0) ? drp4[0]["ClStk"].ToString() : ""));
                        e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell);
                    }
                    else
                    {
                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = "";
                        e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell);
                    }
                    if (drp3.Length > 0)
                    {
                        totstock += Convert.ToDecimal(drp3[0]["Pri"]);
                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = drp3[0]["Pri"].ToString();
                        e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell);

                    }
                    else
                    {
                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = "";
                        e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell);

                    }
                    TableCell tableCell1 = new TableCell();
                    tableCell1.Attributes["style"] = "font: Andalus";
                    tableCell1.Attributes["style"] = "font: Bold";
                    tableCell1.Text = (totstock != 0) ? totstock.ToString() : "";
                    e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell1);

                    sales = totstock - ((drp1.Length > 0) ? (Convert.ToDecimal(drp1[0]["ClStk"])) : 0);
                    saleslit = sales * ((drp1.Length > 0) ? (Convert.ToDecimal(drp1[0]["product_netwt"])) : ((drp4.Length > 0) ? Convert.ToDecimal(drp4[0]["product_netwt"]) : 0));
                    Sallit[j] += saleslit;

                    TableCell tableCell2 = new TableCell();
                    tableCell2.Attributes["style"] = "font: Andalus";
                    tableCell2.Attributes["style"] = "font: Bold";
                    tableCell2.Text = ((sales <= 0) ? "0" : sales.ToString());
                    e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell2);

                    if (drp1.Length > 0)
                    {
                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = drp1[0]["ClStk"].ToString();
                        e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell);

                    }
                    else
                    {
                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = "";
                        e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell);

                    }
                    if (drp2.Length > 0)
                    {
                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = drp2[0]["Mgf_date"].ToString();
                        e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell);

                    }
                    else
                    {
                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = "";
                        e.Row.Cells.AddAt(e.Row.Cells.Count, tableCell);

                    }
                }
                catch (Exception ex) { throw ex; }
            }
            if (kj == 0)
            {
                e.Row.Visible = false;
            }
        }
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow rw = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

        TableCell TotalCell = new TableCell();

        TotalCell.Width = 55;
        TotalCell.Height = 35;
        TotalCell.BackColor = System.Drawing.Color.FromName("#496a9a");
        TotalCell.ForeColor = System.Drawing.Color.FromName("#fff");
        TotalCell.ColumnSpan = 1;
        TotalCell.Text = "Total Litres";
        rw.Cells.Add(TotalCell);
        for (int j = (Convert.ToInt16(FMnth) - 1); j < Convert.ToInt16(TMnth); j++)
        {
            TableCell HeaderCell = new TableCell();

            HeaderCell.Width = 55;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "";
            rw.Cells.Add(HeaderCell);

            TableCell HeaderCell2 = new TableCell();

            HeaderCell2.Width = 55;
            HeaderCell2.Height = 35;
            HeaderCell2.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell2.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell2.Text = "";
            rw.Cells.Add(HeaderCell2);
            TableCell HeaderCell1 = new TableCell();

            HeaderCell1.Width = 55;
            HeaderCell1.Height = 35;
            HeaderCell1.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell1.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell1.Text = "";
            rw.Cells.Add(HeaderCell1);
            TableCell HeaderCell3 = new TableCell();

            HeaderCell3.Width = 55;
            HeaderCell3.Height = 35;
            HeaderCell3.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell3.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell3.Text = Sallit[j].ToString();
            rw.Cells.Add(HeaderCell3);
            TableCell HeaderCell4 = new TableCell();

            HeaderCell4.Width = 55;
            HeaderCell4.Height = 35;
            HeaderCell4.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell4.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell4.Text = "";
            rw.Cells.Add(HeaderCell4);

            TableCell HeaderCell5 = new TableCell();

            HeaderCell5.Width = 55;
            HeaderCell5.Height = 35;
            HeaderCell5.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell5.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell5.Text = "";
            rw.Cells.Add(HeaderCell5);
        }

        gdprimary.Controls[0].Controls.Add(rw);
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
        string attachment = "attachment; filename=Stock_and_Sales_Progressive from " + Fmnthname + "-" + FYr + " to " + TYr + "-" + FYr + ".xls";
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