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


public partial class MIS_Reports_Product_wise_salesanalysis : System.Web.UI.Page
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
    string FYear = string.Empty;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        FYear = Request.QueryString["cur_year"].ToString();
        subdiv_code = Request.QueryString["subdivision"].ToString();

        lblHead.Text = "Productwise Sales Analysis  for the Year " + FYear;
        Feild.Text = sfname;

        FillSF();

    }
    public class DynamicTemplateField : ITemplate
    {

        public void InstantiateIn(Control container)
        {
            string divcode = "8";

            SalesForce SF = new SalesForce();
            DataSet ff = new DataSet();
            ff = SF.GetProduct_Name(divcode);
            int cnt = ff.Tables[0].Rows.Count;
            //for (int j = 0; j < cnt; j++)
            //{

            //    //define the control to be added , i take text box as your need
            //    TableCell txt1 = new TableCell();
            //    txt1.ID = "txtquantity_" + j + "row_" + j + "";
            //    container.Controls.Add(txt1);
            //}
        }
    }
    private void FillSF()
    {


        string sURL = string.Empty;
        string stURL = string.Empty;


        string stCrtDtaPnt = string.Empty;

        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DCR dc = new DCR();
        //TemplateField temp1 = new TemplateField();  //Create instance of Template field
        //temp1.HeaderText = "New Dynamic Temp Field"; //Give the header text

        //temp1.ItemTemplate = new DynamicTemplateField(); //Set the properties **ItemTemplate** as the instance of DynamicTemplateField class.


        //gvsalesanalysis.Columns.Add(temp1);



        DataTable dsf = new DataTable();

        dsf = dc.sales_analysis_datatable(divcode, sfCode, FYear);
        if (dsf.Rows.Count > 0)
        {
            for (int i = 0; i < dsf.Rows.Count; i++)
            {
                for (int j = 0; j < dsf.Columns.Count; j++)
                {
                    if (string.IsNullOrEmpty(dsf.Rows[i][j].ToString()))
                    {
                        dsf.Rows[i][j] = 0;
                    }
                    else
                    {

                    }

                }
            }
        }
        decimal[] arr = new decimal[50];
        int p = 0;
        for (int i = 1; i < 13; i++)
        {
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string strMonthName = mfi.GetMonthName(Convert.ToInt16(i)).ToString().Substring(0, 3);            
            arr[p++] = dsf.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<String>((strMonthName + "_Val"))));
        }

        dsGV = dc.sales_analysis_data(divcode, sfCode, FYear, subdiv_code);
        DataTable dtPro = new DataTable();
        dtPro.Columns.Add("Sl.No", typeof(string));
        dtPro.Columns.Add("Product Name", typeof(string));
        for (int i = 2; i < dsGV.Tables[0].Columns.Count; i++)
        {
            if (i % 2 != 0)
            {
                dtPro.Columns.Add("VAL_" + i, typeof(string));
                dtPro.Columns.Add("AVG_" + i, typeof(string));
            }
            else
            {
                dtPro.Columns.Add("QTY_" + i, typeof(string));
            }
        }


        for (int k = 0; k < dsGV.Tables[0].Rows.Count; k++)
        {
            dtPro.Rows.Add();
            dtPro.Rows[k][0] = dsGV.Tables[0].Rows[k][0].ToString();
            dtPro.Rows[k][1] = dsGV.Tables[0].Rows[k][1].ToString();
            int kc = 2;
            int lc = kc;
            for (int i = 2; i < dsGV.Tables[0].Columns.Count; i++)
            {
                dtPro.Rows[k][kc] = dsGV.Tables[0].Rows[k][i].ToString();
                if ((i % 2) != 0)
                {
                    kc++;
                    lc++;
                    decimal tot = Convert.ToDecimal(dsGV.Tables[0].Rows[k][i] == DBNull.Value ? 0 : dsGV.Tables[0].Rows[k][i]) / (arr[i - lc] == 0 ? 1 : arr[i - lc]);
                    dtPro.Rows[k][kc] = Decimal.Round((tot * 100), 2).ToString()+"%";
                }
                kc++;
            }
        }




        DataSet dsPro = new DataSet();
        dsPro.Tables.Add(dtPro);
        if (dsGV.Tables[0].Rows.Count > 0)
        {

            gridcnt = dsGV.Tables[0].Columns.Count;
            //dsGV.Tables[0].Columns[5].SetOrdinal(gridcnt-1);
            //dsGV.Tables[0].Columns[5].SetOrdinal(gridcnt-1);
            gvsalesanalysis.DataSource = dsPro;
            gvsalesanalysis.DataBind();
        }
        else
        {
            gvsalesanalysis.DataSource = null;
            gvsalesanalysis.DataBind();
        }


    }
    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {

        //       subTotal = 0;
        //       nettotal = 0;
        //       if (e.Row.RowType == DataControlRowType.DataRow)
        //       {
        //           DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
        //           int orderId = Convert.ToInt32(dt.Rows[e.Row.RowIndex]["Stockist_Code"]);
        //           total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Order_value"]);
        //           nttotal += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["net_weight_value"]);


        //           //SalesForce SF1 = new SalesForce();
        //           //DataSet ff1 = new DataSet();
        //           //ff1 = SF1.GetProduct_Name(divcode);
        //           //int cnt = ff1.Tables[0].Rows.Count;
        //           //foreach (DataRow drdoc in ff1.Tables[0].Rows)
        //           //{
        //           ////for (int j = 0; j < cnt; j++)
        //           ////{

        //           //        string prdt_code = drdoc["Product_Detail_Code"].ToString();
        //           //        string stock_code = Convert.ToString(orderId);
        //           //        DataSet dm = new DataSet();
        //           //        dm = SF1.GetDistNamewise1(divcode, stock_code);

        //           //        TableCell txt1 = new TableCell();
        //           //        Literal fflit = new Literal();
        //           //        fflit.Text = dm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        //           //        //txt1.ID = "txtquantity_";
        //           //        txt1.Controls.Add(fflit);
        //           //        //txt1.
        //           //        e.Row.Cells.Add(txt1);
        //           //    //}
        //           //}
        //           //Response.Write(gvtotalorder.Rows[0].Cells[2].Text);
        //           if (orderId != currentId)
        //           {
        //               if (e.Row.RowIndex > 0)
        //               {
        //                   for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
        //                   {
        //                        subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt].Text);
        //                       nettotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt - 1].Text);
        //                     //  subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
        //                      // nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);

        //                   }
        //                   this.AddTotalRow("Sub Total", nettotal.ToString("N2"), subTotal.ToString("N2"));
        //                   subTotalRowIndex = e.Row.RowIndex;
        //               }
        //               currentId = orderId;
        //           }
        //       }

        //       SalesForce SF = new SalesForce();
        //       DataSet ff = new DataSet();
        //       ff = SF.GetProduct_Name(divcode);
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
            HeaderCell.Width = 25;
            HeaderCell.Height = 35;
            HeaderCell.Font.Bold = true;
            //HeaderCell.ApplyStyle.
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.ForeColor = System.Drawing.Color.White;
            TableCell Distributor = new TableCell();

            HeaderCell.Width = 25;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "S.No";
            HeaderCell.RowSpan = 2;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 180;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "Product Name";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.ColumnSpan = 3;
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            
            HeaderCell.Text = "January";
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "February";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.Attributes["style"] = "font: Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "March";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "April";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "May";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "June";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;

            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "July";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.VerticalAlign = VerticalAlign.Middle;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "August";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "September";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "October";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "November";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 110;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "December";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow0.Cells.Add(HeaderCell);
            gvsalesanalysis.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            for (int i = 1; i < 13; i++)
            {
                HeaderCell = new TableCell();
                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Style.Add("Color", "White");
                HeaderCell.Text = "Quantity";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gvsalesanalysis.Controls[0].Controls.AddAt(1, HeaderGridRow1);
                HeaderCell = new TableCell();

                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Style.Add("Color", "White");
                HeaderCell.Text = "Value";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gvsalesanalysis.Controls[0].Controls.AddAt(1, HeaderGridRow1);

                HeaderCell = new TableCell();
                HeaderCell.Width = 70;
                HeaderCell.Height = 35;
                HeaderCell.ForeColor = System.Drawing.Color.FromName("#fff");
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                
                HeaderCell.Attributes["style"] = "min-width: 50px";
                HeaderCell.Text = "Sales%";
                HeaderGridRow1.Cells.Add(HeaderCell);
                gvsalesanalysis.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            }
            HeaderCell = new TableCell();

            HeaderCell.Width = 55;
            HeaderCell.Height = 35;
            HeaderCell.ColumnSpan = 2;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
            HeaderCell.Style.Add("Color", "White");
            HeaderCell.Text = "Total Amount";
            HeaderGridRow2.Cells.Add(HeaderCell);
            gvsalesanalysis.Controls[0].Controls.AddAt(2, HeaderGridRow2);
            DataTable dsf = new DataTable();
            DCR dc = new DCR();

            dsf = dc.sales_analysis_datatable(divcode, sfCode, FYear);
            if (dsf.Rows.Count > 0)
            {
                for (int i = 0; i < dsf.Rows.Count; i++)
                {
                    for (int j = 0; j < dsf.Columns.Count; j++)
                    {
                        if (string.IsNullOrEmpty(dsf.Rows[i][j].ToString()))
                        {

                            dsf.Rows[i][j] = 0;

                        }
                        else
                        {

                        }

                    }
                }
            }
            for (int i = 1; i < 13; i++)
            {
                System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
                string strMonthName = mfi.GetMonthName(Convert.ToInt16(i)).ToString().Substring(0, 3);

                HeaderCell = new TableCell();
                string quantity = "0";
                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Style.Add("Color", "White");
                quantity = dsf.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<String>((strMonthName + "_Qty")))).ToString();
                // quantity = dsf.AsEnumerable().Sum(r => r.Field<double>(strMonthName +"_Qty"));
                HeaderCell.Text = quantity;
                HeaderGridRow2.Cells.Add(HeaderCell);
                gvsalesanalysis.Controls[0].Controls.AddAt(2, HeaderGridRow2);

                HeaderCell = new TableCell();
                string value = "0";
                HeaderCell.Width = 55;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Style.Add("Color", "White");
                value = dsf.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<String>((strMonthName + "_Val")))).ToString();
                HeaderCell.Text = value;
                HeaderGridRow2.Cells.Add(HeaderCell);
                gvsalesanalysis.Controls[0].Controls.AddAt(2, HeaderGridRow2);

                HeaderCell = new TableCell();
                string Per = "0";
                HeaderCell.Width = 70;
                HeaderCell.Height = 35;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#496a9a");
                HeaderCell.Style.Add("Color", "White");
                if (value != "0")
                {
                    Per = "100%";
                }
                HeaderCell.Text = Per;
                HeaderGridRow2.Cells.Add(HeaderCell);
                gvsalesanalysis.Controls[0].Controls.AddAt(2, HeaderGridRow2);

            }



        }

        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        //           foreach (DataRow drdoctor in ff.Tables[0].Rows)
        //           {
        //               HeaderCell = new TableCell();
        //               HeaderCell.Height = 35;
        //               HeaderCell.BackColor = System.Drawing.Color.FromName("#3f51b5eb");

        //               HeaderCell.Attributes["style"] = "font: Andalus";
        //   HeaderCell.Attributes["style"] = "font: Bold";
        //               HeaderCell.Text = drdoctor["Product_Detail_Name"].ToString();
        //               HeaderGridRow0.Cells.Add(HeaderCell);
        //           }
        //HeaderCell = new TableCell();
        //           HeaderCell.Width = 110;
        //           HeaderCell.Height = 35;
        //           HeaderCell.BackColor = System.Drawing.Color.FromName("#3f51b5eb");
        //           HeaderCell.Text = "Net weight";
        //           HeaderCell.Attributes["style"] = "font: Andalus";
        //           HeaderGridRow0.Cells.Add(HeaderCell);
        //             HeaderCell = new TableCell();
        //           HeaderCell.Width = 110;
        //           HeaderCell.Height = 35;
        //           HeaderCell.BackColor = System.Drawing.Color.FromName("#3f51b5eb");
        //           HeaderCell.Text = "Order Value";
        //           HeaderCell.Attributes["style"] = "font: Andalus";
        //           HeaderGridRow0.Cells.Add(HeaderCell);





    }

    //}

    private void AddTotalRow(string labelText, string netvalue, string value)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#dbf7d9");
        row.CssClass = "subTot";
        row.Cells.AddRange(new TableCell[4] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=gridcnt-3},
                                          new TableCell { Text = netvalue, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right } });

        //gvsalesanalysis.Controls[0].Controls.Add(row);
    }
    private void AddTotalRoww(string labelText, string ntvalue, string value)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#ecf19f");

        row.CssClass = "GrndTot";
        row.Cells.AddRange(new TableCell[4] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=gridcnt-3},
                                          new TableCell { Text = ntvalue, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right } });

        //  gvsalesanalysis.Controls[0].Controls.Add(row);
    }
    protected void PivotTable_ItemDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        //var dataItem = e.Row.DataItem;
        // For example get your CustomerID as you defined at your anonymous type :
        //string customerId = dataItem.CustomerID;

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //}
        //Repeater scores = (Repeater)e.Item.FindControl("Repeater1");
        //DataRowView row = (DataRowView)((ListViewDataItem)e.Item).DataItem;
        //scores.DataSource = row.CreateChildView("FK_Student_Scores");
        //scores.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //e.Row.Cells[2].Visible = false;
        //e.Row.Cells[2].Visible = false;
        //if header column we dont need multiplication
        if (e.Row.RowIndex != -1)
        {
            //taking values from first cell. my first cell contain value, you can change
            string id = e.Row.Cells[0].Text;
            //taking values from second cell. my second cell contain value, you can change
            string id2 = e.Row.Cells[1].Text;
            //multiplication
            //double mult = int.Parse(id) * int.Parse(id2);
            ////adding result to last column. coz we add new column in last.
            //e.Row.Cells[e.Row.Cells.Count - 1].Text = mult.ToString();
        }
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
        //    int orderId = Convert.ToInt32(dt.Rows[e.Row.RowIndex]["Stockist_Code"]);
        //}
        //    Repeater scores = (Repeater)e.Item.FindControl("pivotTableScores");
        //DataRowView row = (DataRowView)((ListViewDataItem)e.Item).DataItem;
        //scores.DataSource = row.CreateChildView("FK_Student_Scores");
        //scores.DataBind();
        //        protected void gv_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        //{


        //}
        //{
        //for (int i = subTotalRowIndex; i < gvsalesanalysis.Rows.Count; i++)
        //{
        //    //subTotal += Convert.ToDecimal(gvsalesanalysis.Rows[i].Cells[gridcnt].Text);
        //    nettotal += Convert.ToDecimal(gvsalesanalysis.Rows[i].Cells[gridcnt - 1].Text);
        //    // subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
        //    //subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
        //    //nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
        //}
        // this.AddTotalRow("Sub Total", nettotal.ToString("N2"), subTotal.ToString("N2"));
        // this.AddTotalRoww("Total", nttotal.ToString("N2"), total.ToString("N2"));
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

    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Productwise_sales_analysis.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            //gvclosingstockanalysis.AllowPaging = false;
            //this.BindGrid();

            gvsalesanalysis.HeaderRow.BackColor = Color.Blue;

            foreach (GridViewRow row in gvsalesanalysis.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = gvsalesanalysis.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = gvsalesanalysis.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            gvsalesanalysis.RenderControl(hw);

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

    protected void btnClose_Click(object sender, EventArgs e)
    {
    }
}