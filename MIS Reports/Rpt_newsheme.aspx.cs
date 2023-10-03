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

public partial class MIS_Reports_Rpt_newsheme : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv_code = string.Empty;
    public static string FDate = string.Empty;
    public static string TDate = string.Empty;
    public static string gropc = string.Empty;
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
    decimal nsbxcase = 0;
    decimal nspiece = 0;
    decimal nttbxv = 0;
    decimal ntbxtot = 0;
    int subTotalRowIndex = 0;
    decimal subTotal = 0;
    decimal nettotal = 0;
    decimal ntbxtotal = 0;
    decimal catnwgt = 0;
    decimal catval = 0;
    int catquan = 0;
    decimal[] tarr;
    decimal bxv=0;
    decimal[] arr1;
    decimal[] bxv1;
    int n;
    int k;
    int b;
    int r = 0;
    decimal rwbxqty;
    int rwpcqty;
    decimal rwbxqtytot=0;
    int rwpcqtytot=0;
    decimal totbx = 0;
    int totcs = 0;
    decimal totbxf = 0;
    int totcsf = 0;
    decimal totalbxv = 0;
    int totalcsv = 0;
    string[] TransSlNo;

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        //Date = Request.QueryString["Date"].ToString();
        subdiv_code = Request.QueryString["subdiv"].ToString();
        gropc = Request.QueryString["grop"].ToString();
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
        lblHead.Text = "New Scheme from" + d1.ToString("dd-MM-yyyy") + " to " + d2.ToString("dd-MM-yyyy");
        FillSF();
    }

    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();

        string stCrtDtaPnt = string.Empty;
     
        SalesForce SF = new SalesForce();
        ff = new DataSet();

        SqlCommand cmd = new SqlCommand("select pd.Product_Detail_Code,case when isnull(pd.UOM_Weight,'')='' or pd.division_Code<>98 then pd.Product_Detail_Name else pd.UOM_Weight End Product_Detail_Name from Mas_Product_Detail pd inner join Mas_Product_Group gp on gp.Product_Grp_Code=pd.Product_Grp_Code where pd.Division_Code='" + divcode + "' and CHARINDEX(','+CAST(" + subdiv_code + " as varchar)+',',','+pd.subdivision_code+',')>0 "+
           " and pd.Product_Active_Flag = 0 and  pd.Product_Grp_Code = '" + gropc + "'  order by gp.Product_Grp_Code, Prod_Detail_Sl_No", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ff);

        //ff = dc.getnewschmgrpname(divcode, subdiv_code, gropc);
     
        tarr = new decimal[ff.Tables[0].Rows.Count];
        //bxv = new decimal[ff.Tables[0].Rows.Count];
        //dsd = dc.newschmgetgrpname(divcode, subdiv_code, gropc);

        SqlCommand cmd1 = new SqlCommand("select * from (select Product_Grp_Code,Product_Grp_Name,(select COUNT(product_detail_code) from Mas_Product_Detail where Division_Code='" + divcode + "' and Product_Grp_Code=gp.Product_Grp_Code and CHARINDEX(','+CAST(" + subdiv_code + " as varchar)+',',','+subdivision_code+',')>0 and Product_Grp_Code='" + gropc + "' and Product_Active_Flag=0)as t from Mas_Product_Group gp where Division_Code='" + divcode + "' and  Product_Grp_Active_Flag=0) as gr where t<>0", con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        da1.Fill(dsd);

        ss = new DataSet();
        //ss = dc.newschemenewqty(divcode, Sf_Code, FDate, TDate, gropc, subdiv_code);

        SqlCommand cmd2 = new SqlCommand("exec TODAY_ORDER_VIEW_newscheme_QTY51 '" + Sf_Code + "','" + divcode + "','" + FDate + "','" + TDate + "','" + subdiv_code + "'," + gropc + "", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        da2.Fill(ss);

       // dsGV = dc.newschemorderview(divcode, Sf_Code, FDate, TDate,gropc,subdiv_code);

        SqlCommand cmd3 = new SqlCommand("exec TODAY_ORDER_VIEW_newschm '" + Sf_Code + "','" + divcode + "','" + FDate + "','" + TDate + "','" + subdiv_code + "'," + gropc + "", con);
        SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
        da3.Fill(dsGV);

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

        
        
    }

    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {

        subTotal1 = 0;
        nettotal1 = 0;
        rwbxqtytot = 0;
        rwpcqtytot = 0;
        ntbxtotal = 0;
        totcs = 0;
        totbx = 0;
       
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
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            if (divcode == "32")
            {
                HeaderCell = new TableCell();
                HeaderCell.Width = 310;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = "Order Type";
                HeaderCell.RowSpan = 2;
                HeaderGridRow0.Cells.Add(HeaderCell);
            }
            HeaderCell = new TableCell();
            //HeaderCell.Width = 110;
            //HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Distributor Name";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Distributor ERP Code";
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
            HeaderCell.Text = "Reporting Manager";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Route";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Retailer Name";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.Text = "Channel";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableHeaderCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Text = "Box";
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


            foreach (DataRow drdoctor in dsd.Tables[0].Rows)
            {
                HeaderCell = new TableCell();
                HeaderCell.Width = 110;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");

                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderCell.Attributes["style"] = "font: Bold";
                HeaderCell.Text = drdoctor["Product_Grp_Name"].ToString();
                if (divcode == "75")
                {
                    HeaderCell.ColumnSpan = Convert.ToInt32(ff.Tables[0].Rows.Count);
                }
                else
                {
                    HeaderCell.ColumnSpan = Convert.ToInt32(drdoctor["t"]);
                }
                HeaderGridRow0.Cells.AddAt(HeaderGridRow0.Cells.Count - 3, HeaderCell);
            }
            gvtotalorder.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            foreach (DataRow drdoctor in ff.Tables[0].Rows)
            {
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.Text = drdoctor["Product_Detail_Name"].ToString();
                HeaderGridRow1.Cells.Add(HeaderCell);
                gvtotalorder.Controls[0].Controls.AddAt(1, HeaderGridRow1);

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

                        rwbxqty = Convert.ToDecimal(drp[0]["Quantity"].ToString()) / Convert.ToDecimal(drp[0]["Sample_Erp_Code"].ToString());
                        //rwpcqty = (int)((Convert.ToDecimal(drp[0]["Quantity"].ToString())) - (Convert.ToDecimal(rwbxqty) * Convert.ToDecimal(drp[0]["Sample_Erp_Code"].ToString())));
                        rwbxqtytot += rwbxqty;
                        //rwpcqtytot += rwpcqty;
                        totalbxv += rwbxqty;
                        //totalcsv += rwpcqty;

                    }
                    else
                    {
                        TableCell tableCell = new TableCell();
                        tableCell.Attributes["style"] = "font: Andalus";
                        tableCell.Attributes["style"] = "font: Bold";
                        tableCell.Text = "";
                        tarr[k] += 0;
                        e.Row.Cells.AddAt(e.Row.Cells.Count - 2, tableCell);
                    }
                }
                catch { }
                jk++;
                k++;

            }

            TableCell tableCellb = new TableCell();
            tableCellb.Attributes["style"] = "font: Andalus";
            tableCellb.Attributes["style"] = "font: Bold";
            tableCellb.Text = Math.Round(rwbxqtytot,2, MidpointRounding.ToEven).ToString();// +" ( " + drdoctor["Product_Detail_Code"] + ")";
            e.Row.Cells.AddAt(e.Row.Cells.Count - 2, tableCellb);
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
            //nttbxqty += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["boxv"]);
            //ntbxqty += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["boxv"]);
            sfID = Convert.ToString(dt.Rows[e.Row.RowIndex]["Sf_Name"]);
          

                //TableCell tableCellb = new TableCell();
                //tableCellb.Attributes["style"] = "font: Andalus";
                //tableCellb.Attributes["style"] = "font: Bold";
                //tableCellb.Text = nttbxv.ToString();// +" ( " + drdoctor["Product_Detail_Code"] + ")";
                //e.Row.Cells.AddAt(e.Row.Cells.Count - 2, tableCellb);

           
            if (sfID != currentsfid)
            {
                if (e.Row.RowIndex > 0)
                {
                    for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                    {
                        gridcnt = gvtotalorder.Rows[i].Cells.Count - 1;
                        subTotal1 += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt].Text);
                        nettotal1 += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt - 1].Text);
                        //string[] arr= (gvtotalorder.Rows[i].Cells[gridcnt - 2].Text.Split(','));
                        totbx += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt - 2].Text);
                        //totcs += Convert.ToInt32(arr[1].ToString());
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
                    this.AddTotalRow("Sub Total", arr1, totbx.ToString(), nettotal1.ToString("N2"), subTotal1.ToString("N2"));
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
        
    }
    private void AddTotalRowwcat(string labelText, string catnwgtt, string catquant, string catvalt)
    {
       
    }
    private void AddTotalRow(string labelText, decimal[] arr1, string ntbxtotal,string netvalue, string value)
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
        row.Cells.Add(new TableCell { Text = ntbxtotal, HorizontalAlign = HorizontalAlign.Left });
        row.Cells.Add(new TableCell { Text = netvalue, HorizontalAlign = HorizontalAlign.Left });
        row.Cells.Add(new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Left });

        //  row.Cells.AddRange(new TableCell[3] { new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,  CssClass="ff",ColumnSpan=gridcnt-2},
        //                                  new TableCell { Text = netvalue, HorizontalAlign = HorizontalAlign.Left },
        //                                new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Left } });

        gvtotalorder.Controls[0].Controls.Add(row);
    }
    private void AddTotalRoww(string labelText, decimal[] arr1, string bxv1, string ntvalue, string value)
    {

        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#ecf19f");

        row.CssClass = "GrndTot";
        row.Cells.Add(new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right, CssClass = "ff", ColumnSpan = ((divcode == "32") ? 9 : 8) });
        for (int i = 0; i < arr1.Length; i++)
        {
            row.Cells.Add(new TableCell { Text = arr1[i].ToString(), HorizontalAlign = HorizontalAlign.Left });

        }
        row.Cells.Add(new TableCell { Text = bxv1, HorizontalAlign = HorizontalAlign.Left });
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
        decimal[] bxv1 = new decimal[ff.Tables[0].Rows.Count];
        string Trans_Sl_No;
        string Orderdate;
        int n;
        subTotal = 0;
        nettotal = 0;
        totbxf = 0;
        totcsf = 0;
        if (gvtotalorder.Rows.Count > 0)
        {
            for (int i = subTotalRowIndex; i < gvtotalorder.Rows.Count; i++)
            {
                gridcnt = gvtotalorder.Rows[i].Cells.Count - 1;
                subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt].Text);
                nettotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt - 1].Text);
                //string[] arr = (gvtotalorder.Rows[i].Cells[gridcnt - 2].Text.Split(','));
                totbxf += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt - 2].Text);
                //totcsf += Convert.ToInt32(arr[1].ToString());
                

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
            this.AddTotalRow("Sub Total",arr1.ToArray(),totbxf.ToString(), nettotal.ToString("N2"), subTotal.ToString("N2"));
            this.AddTotalRoww("Total",tarr, Math.Round(totalbxv,2, MidpointRounding.ToEven).ToString(), nttotal.ToString("N2"), total.ToString("N2"));
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
        string attachment = "attachment; filename=NewScheme.xls";
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