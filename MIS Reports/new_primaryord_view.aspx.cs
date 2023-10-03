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
using System.Reflection;
using System.Data.SqlClient;
using Newtonsoft.Json;

public partial class MIS_Reports_new_primaryord_view : System.Web.UI.Page
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
    DataSet dsGVi = new DataSet();
    DataTable pn = new DataTable();
    DataTable sq = new DataTable();
    DataTable rs4 = new DataTable();
    DataSet rs4v = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();

        Feild.Text = sfname;
        if (divcode == "35")
        {

            TDate = Request.QueryString["ToDate"].ToString();
        }
        else
        {
            TDate = Request.QueryString["ToDate"].ToString();
            FDate = Request.QueryString["FromDate"].ToString();
        }
        lblHead.Text = "Primary Order View " + FDate +" To "+ TDate;
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
        SalesForce SF = new SalesForce();
        ff = new DataSet();
        ff = SF.GetProduct_Name(divcode);
        pn = ff.Tables[0];
        ss = new DataSet();
        ss = dc.Primaryorderclbal(divcode, Sf_Code, FDate, TDate, subdiv_code);
        sq = ss.Tables[0];

        dsGV = dc.Primaryorderview(divcode, Sf_Code, FDate, TDate, subdiv_code);

        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec PRIMARY_ORDER_VIEW_image '" + Sf_Code + "','" + divcode + "','" + FDate + "','" + TDate + "','" + subdiv_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsGVi);
        con.Close();

        var JoinResultProdDts = (from pnam in pn.AsEnumerable()
                                 join pqty in sq.AsEnumerable()
                                 on new
                                 {
                                     JoinProperty1 = pnam.Field<string>("Product_Detail_Code"),
                                 }
                                 equals
                                 new
                                 {
                                     JoinProperty1 = pqty.Field<string>("Product_Code"),

                                 }

                                 select new
                                 {
                                     pcode = pnam.Field<string>("Product_Detail_Code"),
                                     pname = pnam.Field<string>("Product_Detail_Name"),


                                 }).Distinct().ToList();

        rs4 = LINQResultToDataTable(JoinResultProdDts);
        rs4v.Tables.Add(rs4);
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            // dsGV.Tables[0].Columns.RemoveAt(1);
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
            total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Order_value"]);
            if (!(dt.Rows[e.Row.RowIndex]["net_weight_value"] is DBNull))
                 nttotal += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["net_weight_value"]);
            else
                nttotal += 0;
            sfID = Convert.ToString(dt.Rows[e.Row.RowIndex]["Sf_Name"]);
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
            HeaderCell.Text = "Date";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            //HeaderCell.Width = 110;
            //HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Distributor";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Order Taken By";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Territory";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Address";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "SKU Count";
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Image";
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Net weight";
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Order Value";
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);


            foreach (DataRow drdoctor in rs4v.Tables[0].Rows)
            {
                HeaderCell = new TableCell();
                HeaderCell.Width = 110;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");

                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderCell.Attributes["style"] = "font: Bold";
                HeaderCell.Text = drdoctor["pname"].ToString();
                HeaderCell.ColumnSpan = 3;
                HeaderGridRow0.Cells.AddAt(HeaderGridRow0.Cells.Count - 4, HeaderCell);
            }



            gdprimary.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            for (int i = 0; i < rs4v.Tables[0].Rows.Count; i++)
            {
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Case";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gdprimary.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Piece";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gdprimary.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Free";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gdprimary.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                HeaderCell = new TableCell();

               

            }

            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableRow tableRow = new TableRow();

            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            string Trans_Sl_No = Convert.ToString(dt.Rows[e.Row.RowIndex]["Trans_sl_No"]);
            DateTime Orderdate = Convert.ToDateTime(dt.Rows[e.Row.RowIndex]["Order_Date"]);
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;
            int jk = 0;
            

            foreach (DataRow drdoctor in rs4v.Tables[0].Rows)
            {
                try
                {
                    DataRow[] drp = ss.Tables[0].Select("Order_Date='" + Orderdate.ToString("yyyy-MM-dd") + "' and Trans_Sl_No='" + Trans_Sl_No + "' and Product_Code='" + drdoctor["pcode"] + "'");

                    if (drp.Length > 0)
                    {

                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = drp[0]["CQty"].ToString();// +" ( " + drdoctor["Product_Detail_Code"] + ")";
                        e.Row.Cells.AddAt(e.Row.Cells.Count - 2, tableCell);

                        TableCell tableCel2 = new TableCell();
                        tableCel2.Attributes["style"] = "font: Andalus";
                        tableCel2.Attributes["style"] = "font: Bold";
                        tableCel2.Text = drp[0]["Qty"].ToString();// +" ( " + drdoctor["Product_Detail_Code"] + ")";
                        e.Row.Cells.AddAt(e.Row.Cells.Count - 2, tableCel2);

                        TableCell tableCell1 = new TableCell();
                        tableCell1.Attributes["style"] = "font: Andalus";
                        tableCell1.Attributes["style"] = "font: Bold";
                        tableCell1.Text = drp[0]["Free"].ToString();// +" ( " + drdoctor["Product_Detail_Code"] + ")";
                        e.Row.Cells.AddAt(e.Row.Cells.Count - 2, tableCell1);


                       
                    }
                    else
                    {

                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = "";
                        e.Row.Cells.AddAt(e.Row.Cells.Count - 2, tableCell);

                        TableCell tableCel2 = new TableCell();
                        tableCel2.Attributes["style"] = "font: Andalus";
                        tableCel2.Attributes["style"] = "font: Bold";
                        tableCel2.Text = "";
                        e.Row.Cells.AddAt(e.Row.Cells.Count - 2, tableCel2);

                        TableCell tableCell1 = new TableCell();
                        tableCell1.Attributes["style"] = "font: Andalus";
                        tableCell1.Attributes["style"] = "font: Bold";
                        tableCell1.Text = "";
                        e.Row.Cells.AddAt(e.Row.Cells.Count - 2, tableCell1);

                       
                    }
                }
                catch { }
                jk++;
            }
            DataRow[] drpi = dsGVi.Tables[0].Select("Trans_sl_No='" + Trans_Sl_No + "'");
            
            DataRow[] drpp = ss.Tables[0].Select("Order_Date='" + Orderdate.ToString("yyyy-MM-dd") + "' and Trans_Sl_No='" + Trans_Sl_No + "'");
            TableCell tableCellsk = new TableCell();
            tableCellsk.Attributes["style"] = "font: Andalus";
            tableCellsk.Attributes["style"] = "font: Bold";
            tableCellsk.Text = drpp.Length.ToString();
            e.Row.Cells.AddAt(e.Row.Cells.Count - 2, tableCellsk);
            if (drpi.Length > 0)
            {

                TableCell tableCell = new TableCell();
                tableCell.Attributes["style"] = "font: Andalus";
                tableCell.Attributes["style"] = "font: Bold";
                System.Web.UI.WebControls.Image img1 = new System.Web.UI.WebControls.Image();
                string imageUrl = "http://fmcg.sanfmcg.com/photos/" + drpi[0]["sf_code"].ToString() + "_" + drpi[0]["imgurl"].ToString();
                img1.ImageUrl = imageUrl;
                img1.Style.Add(HtmlTextWriterStyle.MarginLeft, "10px");
                img1.Attributes.CssStyle.Add("height", "100px");
                img1.Attributes.CssStyle.Add("width", "100px");
                img1.Attributes.Add("Class", "tr_det_head view_image");

                tableCell.Controls.Add(img1);
                e.Row.Cells.AddAt(e.Row.Cells.Count - 2, tableCell);

            }
            else
            {

                TableCell tableCell = new TableCell();
                tableCell.Attributes["style"] = "font: Andalus";
                tableCell.Attributes["style"] = "font: Bold";
                tableCell.Text = "";
                e.Row.Cells.AddAt(e.Row.Cells.Count - 2, tableCell);

            }
           
        }

        if (sfID != currentsfid)
        {
            if (e.Row.RowIndex > 0)
            {
                for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                {
                    gridcnt = gdprimary.Rows[i].Cells.Count - 1;
                    subTotal1 += Convert.ToDecimal(gdprimary.Rows[i].Cells[gridcnt - 0].Text);
                    nettotal1 += Convert.ToDecimal(gdprimary.Rows[i].Cells[gridcnt - 1].Text);
                    // subTotal += Convert.ToDecimal(gdprimary.Rows[i].Cells[4].Text);
                    //subTotal += Convert.ToDecimal(((Label)gdprimary.Rows[i].FindControl("orderval")).Text);
                    //nettotal += Convert.ToDecimal(((Label)gdprimary.Rows[i].FindControl("netval")).Text);
                }
                this.AddTotalRow("Sub Total", "", nettotal1.ToString("N2"), subTotal1.ToString("N2"));
                subTotalRowIndex = e.Row.RowIndex;
            }
            // currentId = orderId;
            currentsfid = sfID;
        }

    }
    public static DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
    {
        DataTable dt = new DataTable();
        PropertyInfo[] columns = null;
        if (Linqlist == null) return dt;
        foreach (T Record in Linqlist)
        {
            if (columns == null)
            {
                columns = ((Type)Record.GetType()).GetProperties();
                foreach (PropertyInfo GetProperty in columns)
                {
                    Type IcolType = GetProperty.PropertyType;
                    if ((IcolType.IsGenericType) && (IcolType.GetGenericTypeDefinition()
                    == typeof(Nullable<>)))
                    {
                        IcolType = IcolType.GetGenericArguments()[0];
                    }
                    dt.Columns.Add(new DataColumn(GetProperty.Name, IcolType));
                }
            }
            DataRow dr = dt.NewRow();
            foreach (PropertyInfo p in columns)
            {
                dr[p.Name] = p.GetValue(Record, null) == null ? DBNull.Value : p.GetValue(Record, null);
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }
    private void AddTotalRow(string labelText, string netvalue, string value, string values)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#dbf7d9");
        row.CssClass = "subTot";
        row.Cells.AddRange(new TableCell[4] { new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,  CssClass="ff",ColumnSpan=gridcnt-5},
                                          new TableCell { Text = netvalue, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right },
                                         new TableCell { Text = values, HorizontalAlign = HorizontalAlign.Right }});

        gdprimary.Controls[0].Controls.Add(row);
    }
    private void AddTotalRoww(string labelText, string ntvalue, string value, string values)
    {

        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#ecf19f");

        row.CssClass = "GrndTot";
        row.Cells.AddRange(new TableCell[4] { new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right, CssClass="ff",ColumnSpan=gridcnt-5},
                                       new TableCell { Text = ntvalue, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right },
                                         new TableCell { Text = values, HorizontalAlign = HorizontalAlign.Right }});

        gdprimary.Controls[0].Controls.Add(row);
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        subTotal = 0;
        nettotal = 0;
        if (gdprimary.Rows.Count > 0)
        {
            for (int i = subTotalRowIndex; i < gdprimary.Rows.Count; i++)
            {
                gridcnt = gdprimary.Rows[i].Cells.Count - 1;
                subTotal += Convert.ToDecimal(gdprimary.Rows[i].Cells[gridcnt - 0].Text);
                nettotal += Convert.ToDecimal(gdprimary.Rows[i].Cells[gridcnt - 1].Text);
                // subTotal += Convert.ToDecimal(gdprimary.Rows[i].Cells[4].Text);
                //subTotal += Convert.ToDecimal(((Label)gdprimary.Rows[i].FindControl("orderval")).Text);
                //nettotal += Convert.ToDecimal(((Label)gdprimary.Rows[i].FindControl("netval")).Text);
            }
            this.AddTotalRow("Sub Total", "", nettotal.ToString("N2"), subTotal.ToString("N2"));
            this.AddTotalRoww("Total", "", nttotal.ToString("N2"), total.ToString("N2"));
        }



    }

    //protected void OnDataBound(object sender, EventArgs e)
    //{

    //    //GridViewRow rw = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

    //    //TableCell TotalCell = new TableCell();

    //    //TotalCell.Width = 55;
    //    //TotalCell.Height = 35;
    //    //TotalCell.BackColor = System.Drawing.Color.FromName("#496a9a");
    //    //TotalCell.ForeColor = System.Drawing.Color.FromName("#fff");
    //    //TotalCell.ColumnSpan = 3;
    //    //TotalCell.Text = "Total";
    //    //rw.Cells.Add(TotalCell);
    //    //for (int i = 0; i < TQty.Length; i++)
    //    //{
    //    //    TableCell HeaderCell = new TableCell();

    //    //    HeaderCell.Width = 55;
    //    //    HeaderCell.Height = 35;
    //    //    HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
    //    //    HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
    //    //    HeaderCell.Text = TQty[i].ToString();
    //    //    rw.Cells.Add(HeaderCell);

    //    //    TableCell HeaderCell1 = new TableCell();

    //    //    HeaderCell1.Width = 55;
    //    //    HeaderCell1.Height = 35;
    //    //    HeaderCell1.BackColor = System.Drawing.Color.FromName("#496a9a");
    //    //    HeaderCell1.ForeColor = System.Drawing.Color.FromName("#fff");
    //    //    HeaderCell1.Text = "";
    //    //    rw.Cells.Add(HeaderCell1);
    //    //}

    //    //gvincentive.Controls[0].Controls.Add(rw);
    //}

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
        string attachment = "attachment; filename=Primary Order View.xls";
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