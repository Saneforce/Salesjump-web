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
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.Xml;
using DBase_EReport;

public partial class MIS_Reports_samtotalorderview : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv_code = string.Empty;
    public static string FDate = string.Empty;
    public static string TDate = string.Empty;
    DataSet ff = new DataSet();
    DataSet ss = new DataSet();
    DataSet dsd = new DataSet();
    int gridcnt = 0;
    string sfID = string.Empty;
    string currentsfid = string.Empty;
    public static string statecode = "0";
    decimal subTotal1 = 0;
    decimal nettotal1 = 0;
    decimal total = 0;
    decimal nttotal = 0;
    int subTotalRowIndex = 0;
    decimal subTotal = 0;
    decimal nettotal = 0;
    decimal catnwgt = 0;
    decimal catval = 0;
    int catquan = 0;
 decimal[] tarr;
    decimal[] arr1;
    int n;
    int k;
    int r=0;
 string[] TransSlNo;

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        //Date = Request.QueryString["Date"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        statecode = Request.QueryString["state"].ToString();
        // HiddenField1.Value = divcode;
       // lblHead.Text = "Today Order View";
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
   DateTime d1 = Convert.ToDateTime(FDate);
   DateTime d2 = Convert.ToDateTime(TDate);
        //lblHead.Text = "Secondary Order View from" + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
		if(divcode=="101" || divcode=="29")
        {
            lblHead.Text = "Secondary Order View from" + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
            Label2.Text = "  *(All Quantity are in CARTON Only)";
        }
        else
        {
            lblHead.Text = "Secondary Order View from" + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
            Label2.Text = "";
        }
        FillSF();
    }

    [WebMethod]
    public static string GetRetDetails()
    {
        string msg = string.Empty;
        DataSet ds = null;
        DataTable dtt = new DataTable();
        DCR dc = new DCR();
        if (FDate == TDate)
        {
            ds = dc.getretdetsaa(Sf_Code, divcode, FDate, TDate,subdiv_code,statecode);
            dtt = ds.Tables[0];
        }
        return JsonConvert.SerializeObject(dtt);
    }
    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;
        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();
		loc dc1 = new loc () ;

        string stCrtDtaPnt = string.Empty;
        //if (sf_type == "4")
        //{
        SalesForce SF = new SalesForce();
        ff = new DataSet();
 if (divcode == "75")
        {
            ff = SF.GetProduct_total_forcompany(divcode, FDate, TDate, subdiv_code, statecode);
        }	
        else
        {
        ff = dc.getgrppdname(divcode, subdiv_code, statecode);
}
tarr = new decimal[ff.Tables[0].Rows.Count];
 //dsd = dc.getgrpname(divcode, subdiv_code, statecode);

        ss = new DataSet();
        ss = dc1.secordernewqty(divcode, Sf_Code, FDate, TDate, subdiv_code, statecode);


        dsGV = dc.secneworderview(divcode, Sf_Code, FDate, TDate, subdiv_code, statecode);
 TransSlNo = new string[dsGV.Tables[0].Rows.Count];
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            dsGV.Tables[0].Columns.RemoveAt(0);
            gridcnt = dsGV.Tables[0].Columns.Count;
            gvtotalorder.DataSource = dsGV;
            gvtotalorder.DataBind();
        }
        else
        {
            gvtotalorder.DataSource = null;
            gvtotalorder.DataBind();
        }

        dsGc = dc.view_total_order_view_categorywise1(divcode, Sf_Code, FDate, TDate, subdiv_code, statecode);

        if (dsGc.Tables[0].Rows.Count > 0)
        {
            //dsGc.Tables[0].Columns.RemoveAt(1);
            //dsGc.Tables[0].Columns.RemoveAt(3);
            //dsts.Tables[0].Columns.RemoveAt(1);
            GridViewcat.DataSource = dsGc;
            GridViewcat.DataBind();
        }
        else
        {
            GridViewcat.DataSource = null;
            GridViewcat.DataBind();
        }
        foreach (DataRow drFF in dsGc.Tables[0].Rows)
        {
            stCrtDtaPnt += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y:";

            stCrtDtaPnt += Convert.ToString(drFF["value"]) + "},";
        }
        string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);

    }

    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {

        subTotal1 = 0;
        nettotal1 = 0;     



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
            HeaderCell.Text = "Order Date";
            
            HeaderGridRow0.Cells.Add(HeaderCell);
            if (divcode == "32")
            {
                HeaderCell = new TableCell();
                HeaderCell.Width = 310;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Order Type";
                
                HeaderGridRow0.Cells.Add(HeaderCell);
            }
            HeaderCell = new TableCell();
            //HeaderCell.Width = 110;
            //HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Distributor Name";
            
            HeaderGridRow0.Cells.Add(HeaderCell);
 			HeaderCell = new TableCell();           
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Distributor ERP Code";
            
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Order Taken By";
            
            HeaderGridRow0.Cells.Add(HeaderCell);
 			HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Reporting Manager";
            
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
            HeaderCell.Text = "Channel";
            
            HeaderGridRow0.Cells.Add(HeaderCell);

            foreach (DataRow drdoctor in ff.Tables[0].Rows)
            {
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = drdoctor["Product_Detail_Name"].ToString();
                HeaderGridRow0.Cells.Add(HeaderCell);

            }
			
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Net weight";
            HeaderCell.Attributes["style"] = "font: Courier";
            
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Order Value";
            HeaderCell.Attributes["style"] = "font: Courier";
            
            HeaderGridRow0.Cells.Add(HeaderCell);
            gvtotalorder.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableRow tableRow = new TableRow();

            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            string Trans_Sl_No = Convert.ToString(dt.Rows[e.Row.RowIndex]["Trans_sl_No"]);
 TransSlNo[r] = Convert.ToString(dt.Rows[e.Row.RowIndex]["Trans_sl_No"]);
            string Orderdate = Convert.ToString(dt.Rows[e.Row.RowIndex]["Order_Date"]);
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
            e.Row.Cells[4].Wrap = false;
            e.Row.Cells[5].Wrap = false;
  			e.Row.Cells[6].Wrap = false;
            e.Row.Cells[7].Wrap = false;
            if (divcode != "32")
            {
                e.Row.Cells[2].Visible = false;
            }
            int jk = 0;
 k = 0;
            r++;
            foreach (DataRow drdoctor in ff.Tables[0].Rows)
            {
                try
                {
                    DataRow[] drp = ss.Tables[0].Select("Order_Date='" + Orderdate + "' and Trans_Sl_No='" + Trans_Sl_No + "' and Product_Code='" + drdoctor["Product_Detail_Code"] + "'");

                    if (drp.Length > 0)
                    {

                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = drp[0]["Quantity"].ToString();// +" ( " + drdoctor["Product_Detail_Code"] + ")";
                         tarr[k] += Convert.ToDecimal(drp[0]["Quantity"].ToString());
 e.Row.Cells.AddAt(e.Row.Cells.Count - 2, tableCell);


                        //TableCell tableCellBal = new TableCell();
                        //tableCellBal.Attributes["style"] = "font: Andalus";
                        //tableCellBal.Attributes["style"] = "font: Bold";
                        //tableCellBal.Text = drp[0]["Cl_Bal"].ToString();// +" ( " + drdoctor["Product_Detail_Code"] + ")";                        
                        //e.Row.Cells.AddAt(e.Row.Cells.Count - 2, tableCellBal);
                    }
                    else
                    {

                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = "";
  tarr[k] += 0;
                        e.Row.Cells.AddAt(e.Row.Cells.Count - 2, tableCell);

                        //TableCell tableCellBal = new TableCell();
                        //tableCellBal.Attributes["style"] = "font: Andalus";
                        //tableCellBal.Attributes["style"] = "font: Bold";
                        //tableCellBal.Text = "";
                        //e.Row.Cells.AddAt(e.Row.Cells.Count - 2, tableCellBal);
                    }
                }
                catch { }
                jk++;
                k++;
	
            }
            //if (sfID != currentsfid)
            //{
            //    if (e.Row.RowIndex > 0)
            //    {
            //        for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
            //        {
            //            //var chk = gvtotalorder.Rows[i].Cells[gridcnt].Text;
            //            gridcnt = gvtotalorder.Rows[i].Cells.Count-1;
            //            subTotal1 += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt].Text);
            //            nettotal1 += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt - 1].Text);
            //            // subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
            //            //subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
            //            //nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
            //        }
            //        this.AddTotalRow("Sub Total", nettotal1.ToString("N2"), subTotal1.ToString("N2"));
            //        subTotalRowIndex = e.Row.RowIndex;
            //    }
            //    // currentId = orderId;
            //    currentsfid = sfID;
            //}


            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
arr1 = new decimal[ff.Tables[0].Rows.Count];
            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Order_value"]);
            nttotal += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["net_weight_value"]);
            sfID = Convert.ToString(dt.Rows[e.Row.RowIndex]["Sf_Name"]);
 
        
        if (sfID != currentsfid)
        {
            if (e.Row.RowIndex > 0)
            {
                for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                {
                    gridcnt = gvtotalorder.Rows[i].Cells.Count - 1;
                    subTotal1 += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt].Text);
                    nettotal1 += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt - 1].Text);
                    // subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
                    //subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
                    //nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
 string Trans_Sl_No = Convert.ToString(dt.Rows[i]["Trans_sl_No"]);
                        string Orderdate = Convert.ToString(dt.Rows[i]["Order_Date"]);
n = 0;
                        foreach (DataRow drdoctor in ff.Tables[0].Rows)
                        {                        
                                try
                                {
                                    DataRow[] drp = ss.Tables[0].Select("Order_Date='" + Orderdate + "' and Trans_Sl_No='" + Trans_Sl_No + "' and Product_Code='" + drdoctor["Product_Detail_Code"] + "'");

                                    if (drp.Length > 0)
                                    {
                                        arr1[n] += Convert.ToDecimal(drp[0]["Quantity"].ToString());
                                    }
                                    else
                                    {
                                        arr1[n] += 0;
                                    }
                                }
                                catch { }
                                n++;
                            
                        }
                }
                this.AddTotalRow("Sub Total", arr1, nettotal1.ToString("N2"), subTotal1.ToString("N2"));
                subTotalRowIndex = e.Row.RowIndex;
            }
            // currentId = orderId;
            currentsfid = sfID;
        }
    }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowIndex != -1)
        {
            //e.Row.Cells[2].Visible = false;
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }


    }

    protected void catOnDataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < GridViewcat.Rows.Count; i++)
        {

            catnwgt += Convert.ToDecimal(((Label)GridViewcat.Rows[i].FindControl("lnetweight")).Text);
            catquan += Int32.Parse(((Label)GridViewcat.Rows[i].FindControl("lquantity")).Text);
            catval += Convert.ToDecimal(((Label)GridViewcat.Rows[i].FindControl("cval")).Text);

        }
        //this.AddTotalRow("Sub Total", ovtotal.ToString("N2"), disctprice.ToString("N2"), freetotall.ToString("N2"), nettotal.ToString("N2"), subTotal.ToString("N2"));
        this.AddTotalRowwcat("Total", catnwgt.ToString("N2"), catquan.ToString(""), catval.ToString("N2"));
    }
    private void AddTotalRowwcat(string labelText, string catnwgtt, string catquant, string catvalt)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#ecf19f");

        row.CssClass = "GrndTot";
        row.Cells.AddRange(new TableCell[5] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=1},
                                           new TableCell { Text = catnwgtt, HorizontalAlign = HorizontalAlign.Right },
                                            new TableCell { Text = catquant, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = catvalt, HorizontalAlign = HorizontalAlign.Right }});


        GridViewcat.Controls[0].Controls.Add(row);
    }
    private void AddTotalRow(string labelText,decimal[] arr1, string netvalue, string value)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#dbf7d9");
        row.CssClass = "subTot";
 int arrlength = (Convert.ToInt32(arr1.Length)) + 2;
 row.Cells.Add(new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right, CssClass = "ff", ColumnSpan = ((divcode == "32") ? 9 : 8) });
        for (int i = 0; i < arr1.Length; i++)
        {
            row.Cells.Add(new TableCell { Text = arr1[i].ToString(), HorizontalAlign = HorizontalAlign.Left });

        }
        row.Cells.Add(new TableCell { Text = netvalue, HorizontalAlign = HorizontalAlign.Left });
        row.Cells.Add(new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Left });

      //  row.Cells.AddRange(new TableCell[3] { new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,  CssClass="ff",ColumnSpan=gridcnt-2},
        //                                  new TableCell { Text = netvalue, HorizontalAlign = HorizontalAlign.Left },
        //                                new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Left } });

        gvtotalorder.Controls[0].Controls.Add(row);
    }
    private void AddTotalRoww(string labelText,decimal[] arr1, string ntvalue, string value)
    {

        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#ecf19f");

        row.CssClass = "GrndTot";
        row.Cells.Add(new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right, CssClass = "ff", ColumnSpan = ((divcode == "32") ? 9 : 8) });
        for (int i = 0; i < arr1.Length; i++)
        {
            row.Cells.Add(new TableCell { Text = arr1[i].ToString(), HorizontalAlign = HorizontalAlign.Left });

        }
        row.Cells.Add(new TableCell { Text = ntvalue, HorizontalAlign = HorizontalAlign.Left });
        row.Cells.Add(new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Left });

    //    row.Cells.AddRange(new TableCell[3] { new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right, CssClass="ff",ColumnSpan=gridcnt-2},
                              //            new TableCell { Text = ntvalue, HorizontalAlign = HorizontalAlign.Right },
                              //          new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right } });

        gvtotalorder.Controls[0].Controls.Add(row);
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
 decimal[] arr1 = new decimal[ff.Tables[0].Rows.Count];       
        string Trans_Sl_No;
        string Orderdate;
        int n;
        subTotal = 0;
        nettotal = 0;
        if (gvtotalorder.Rows.Count > 0)
        {
            for (int i = subTotalRowIndex; i < gvtotalorder.Rows.Count; i++)
            {
                gridcnt = gvtotalorder.Rows[i].Cells.Count - 1;
                subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt].Text);
                nettotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt - 1].Text);
 Trans_Sl_No = Convert.ToString(TransSlNo[i]);
                 Orderdate = Convert.ToString(gvtotalorder.Rows[i].Cells[1].Text);
                n = 0;
 foreach (DataRow drdoctor in ff.Tables[0].Rows)
                {
                    try
                    {
                        DataRow[] drp = ss.Tables[0].Select("Order_Date='" + Orderdate + "' and Trans_Sl_No='" + Trans_Sl_No + "' and Product_Code='" + drdoctor["Product_Detail_Code"] + "'");

                        if (drp.Length > 0)
                        {
                            arr1[n] += Convert.ToDecimal(drp[0]["Quantity"].ToString());
                        }
                        else
                        {
                            arr1[n] += 0;
                        }
                    }
                    catch { }
                    n++;
                }
                // subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
                //subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
                //nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
            }
            this.AddTotalRow("Sub Total",arr1.ToArray(), nettotal.ToString("N2"), subTotal.ToString("N2"));
            this.AddTotalRoww("Total",tarr, nttotal.ToString("N2"), total.ToString("N2"));
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
        string attachment = "attachment; filename=Secondary_Order_View.xls";
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

 public class loc
    {
        public DataSet secordernewqty(string div_code, string sfcode, string fdate, string tdate, string subdiv = "0", string statecode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            string strQry = string.Empty;
            if (div_code == "101" || div_code == "29")
            {
                strQry = " exec TODAY_ORDER_VIEW_sample_QTY51_BW '" + sfcode + "','" + div_code + "','" + fdate + "','" + tdate + "','" + subdiv + "'," + statecode + "";
            }
            else
            {
                strQry = " exec TODAY_ORDER_VIEW_sample_QTY51 '" + sfcode + "','" + div_code + "','" + fdate + "','" + tdate + "','" + subdiv + "'," + statecode + "";
            }
            
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
		public DataSet view_total_order_view_categorywise1(string div_code, string sf_code, string Fdate, string Tdate, string subdivcode, string statecode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            string strQry = string.Empty;
            if (div_code == "101" || div_code == "29")
            {
                strQry = "exec [] '" + sf_code + "','" + div_code + "','" + Fdate + "','" + Tdate + "','" + subdivcode + "'," + statecode + "";
            }
            else
            {
                strQry = "exec [TODAY_ORDER_CATEGORY_VIEW1] '" + sf_code + "','" + div_code + "','" + Fdate + "','" + Tdate + "','" + subdivcode + "'," + statecode + "";
            }
            
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
    }
}