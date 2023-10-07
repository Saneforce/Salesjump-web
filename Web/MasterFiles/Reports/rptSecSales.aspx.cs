using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;

public partial class MasterFiles_Reports_rptSecSales : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSecSales = null;
    DataSet dsSale = null;
    DataSet dsState = new DataSet();
    DataSet dsReport = null;
    string state_code = string.Empty;
    string sf_code = string.Empty;
    int FMonth = -1;
    int FYear = -1;
    int TMonth = -1;
    int TYear = -1;
    int stock_code = -1;
    int iDay = -1;
    DateTime SelDate;
    string sDate = string.Empty;
    string sf_name = string.Empty;
    int rpttype = -1;
    DataSet dssf = null;
    DataSet dsStock = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Convert.ToInt16( Request.QueryString["FMonth"].ToString());
        FYear = Convert.ToInt16(Request.QueryString["FYear"].ToString());
        TMonth = Convert.ToInt16(Request.QueryString["TMonth"].ToString());
        TYear = Convert.ToInt16(Request.QueryString["TYear"].ToString());
        stock_code = Convert.ToInt16(Request.QueryString["stockcode"].ToString());
        rpttype = Convert.ToInt16(Request.QueryString["type"].ToString());

        SalesForce sf = new SalesForce();
        dssf = sf.getSfName(sf_code);
        if (dssf.Tables[0].Rows.Count > 0)
            sf_name = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) ;

        lblText.Text = lblText.Text + sf_name + " between " + getMonthName(FMonth) + " - " + FYear + " and " + getMonthName(TMonth) + " - " + TYear;
        
        GenerateReport();
    }

    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
    }


    private void GenerateReport()
    {
        tbl.Rows.Clear();

        //Get the state for the MR
        UnListedDR LstDR = new UnListedDR();
        dsState = LstDR.getState(sf_code);
        if (dsState.Tables[0].Rows.Count > 0)
            state_code = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        //Get the last date of the selected month for the year
        if ((FMonth > 0) && (FYear > 0))
            iDay = GetLastDay(FMonth, FYear);

        sDate = iDay.ToString().Trim() + "-" + FMonth.ToString().Trim() + "-" + FYear.ToString().Trim();
        SelDate = Convert.ToDateTime(sDate);

        //Get Product master data from DB and bind it with Product Repeater
        SecSale ss = new SecSale();
        DataSet dsProd = ss.getProduct(div_code, state_code, SelDate);

        if (dsProd.Tables[0].Rows.Count > 0)
        {
            if (stock_code != 999)
            {
                tbl.BorderStyle = BorderStyle.None;
                //tbl.BorderWidth = 1;

                TableRow tr_stock_header = new TableRow();
                tr_stock_header.BorderStyle = BorderStyle.None;
                //tr_stock_header.BorderWidth = 1;
                tr_stock_header.Attributes.Add("Class", "Backcolor");
                TableCell tc_stock_name = new TableCell();
                tc_stock_name.BorderStyle = BorderStyle.None;
                //tc_stock_name.BorderWidth = 1;
                tc_stock_name.Width = 100;
                tc_stock_name.ColumnSpan = 4;
                Literal lit_stock_name = new Literal();
                Stockist stk = new Stockist();
                
                dsStock = stk.getStockistCreate_StockistName(div_code, stock_code.ToString());
                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    lit_stock_name.Text = " &nbsp; &nbsp; <b> Stockiest Name : " + dsStock.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "</b>";
                }

                //lit_stock_name.Text = " &nbsp; &nbsp; <b> Stockiest Name : " + dstRow["Stockist_Name"].ToString() + "</b>";
                tc_stock_name.Attributes.Add("Class", "Backcolor");
                tc_stock_name.Controls.Add(lit_stock_name);
                tr_stock_header.Cells.Add(tc_stock_name);
                tbl.Rows.Add(tr_stock_header);

                if(rpttype == 1)
                    PopulateSecSales(dsProd, stock_code);
                else
                    PopulateSecSales_Month(dsProd, stock_code);
            }
            else
            {
                tbl.BorderStyle = BorderStyle.None;

                //Stockist stk = new Stockist();
                dsStock = ss.getStockiestDet(sf_code);
                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dstRow in dsStock.Tables[0].Rows)
                    {
                        TableRow tr_stock_header = new TableRow();
                        tr_stock_header.BorderStyle = BorderStyle.None;
                        //tr_stock_header.BorderWidth = 1;
                        tr_stock_header.Attributes.Add("Class", "Backcolor");
                        TableCell tc_stock_name = new TableCell();
                        tc_stock_name.BorderStyle = BorderStyle.None;
                        //tc_stock_name.BorderWidth = 1;
                        tc_stock_name.Width = 100;
                        tc_stock_name.ColumnSpan = 4;
                        Literal lit_stock_name = new Literal();
                        lit_stock_name.Text = " &nbsp; &nbsp; <b> Stockiest Name : " + dstRow["Stockist_Name"].ToString() + "</b>" ;
                        tc_stock_name.Attributes.Add("Class", "Backcolor");
                        tc_stock_name.Controls.Add(lit_stock_name);
                        tr_stock_header.Cells.Add(tc_stock_name);
                        tbl.Rows.Add(tr_stock_header);

                        if (rpttype == 1)
                            //PopulateSecSales(dsProd, stock_code);
                            PopulateSecSales(dsProd, Convert.ToInt16(dstRow["Stockist_Code"].ToString()));
                        else
                            //PopulateSecSales_Month(dsProd, stock_code);
                            PopulateSecSales_Month(dsProd, Convert.ToInt16(dstRow["Stockist_Code"].ToString()));
                        

                        //Empty Row
                        TableRow tr_empty = new TableRow();
                        tr_empty.BorderStyle = BorderStyle.None;
                        tr_empty.Attributes.Add("Class", "Backcolor");
                        TableCell tc_empty = new TableCell();
                        tc_empty.BorderStyle = BorderStyle.None;
                        tc_empty.Width = 100;
                        tc_empty.ColumnSpan = 4;
                        Literal lit_empty = new Literal();
                        lit_empty.Text = " &nbsp; &nbsp; ";
                        tc_empty.Attributes.Add("Class", "Backcolor");
                        tc_empty.Controls.Add(lit_empty);
                        tr_empty.Height = 40;
                        tr_empty.Cells.Add(tc_empty);
                        tbl.Rows.Add(tr_empty);
                    }


                }
            }
        }   
    }


    private void PopulateSecSales(DataSet dsProd, int stock_code)
    {
        SecSale ss = new SecSale();

        TableRow tr_header = new TableRow();
        tr_header.BorderStyle = BorderStyle.Solid;
        tr_header.BorderWidth = 1;
        tr_header.Attributes.Add("Class", "Backcolor");

        TableCell tc_SNo = new TableCell();
        tc_SNo.BorderStyle = BorderStyle.Solid;
        tc_SNo.BorderWidth = 1;
        tc_SNo.Width = 50;
        tc_SNo.RowSpan = 2;
        Literal lit_SNo = new Literal();
        lit_SNo.Text = "S.No";
        tc_SNo.HorizontalAlign = HorizontalAlign.Center;
        tc_SNo.Attributes.Add("Class", "Backcolor");
        tc_SNo.Controls.Add(lit_SNo);
        tr_header.Cells.Add(tc_SNo);

        TableCell tc_Prod_Code = new TableCell();
        tc_Prod_Code.BorderStyle = BorderStyle.Solid;
        tc_Prod_Code.BorderWidth = 1;
        tc_Prod_Code.Width = 100;
        tc_Prod_Code.RowSpan = 2;
        Literal lit_Prod_Code = new Literal();
        lit_Prod_Code.Text = "<center>Prod Code</center>";
        tc_Prod_Code.Attributes.Add("Class", "Backcolor");
        tc_Prod_Code.Controls.Add(lit_Prod_Code);
        tc_Prod_Code.Visible = false;
        tr_header.Cells.Add(tc_Prod_Code);

        TableCell tc_Prod_Name = new TableCell();
        tc_Prod_Name.BorderStyle = BorderStyle.Solid;
        tc_Prod_Name.BorderWidth = 1;
        tc_Prod_Name.Width = 200;
        tc_Prod_Name.RowSpan = 2;
        Literal lit_Prod_Name = new Literal();
        lit_Prod_Name.Text = "<center>Product Name</center>";
        tc_Prod_Name.Attributes.Add("Class", "Backcolor");
        tc_Prod_Name.Controls.Add(lit_Prod_Name);
        tr_header.Cells.Add(tc_Prod_Name);

        TableCell tc_Rate = new TableCell();
        tc_Rate.BorderStyle = BorderStyle.Solid;
        tc_Rate.BorderWidth = 1;
        tc_Rate.Width = 70;
        tc_Rate.RowSpan = 2;
        Literal lit_Rate = new Literal();
        lit_Rate.Text = "<center>Rate</center>";
        tc_Rate.Attributes.Add("Class", "Backcolor");
        tc_Rate.Controls.Add(lit_Rate);
        tr_header.Cells.Add(tc_Rate);

        TableRow tr_sub_header = new TableRow();
        //Get Secondary Sales master data from DB and bind it with Secondary Sales Header Repeater            
        dsSecSales = ss.getrptfield(div_code);
        if (dsSecSales.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dRow in dsSecSales.Tables[0].Rows)
            {
                TableCell tc_sec_name = new TableCell();
                tc_sec_name.BorderStyle = BorderStyle.Solid;
                tc_sec_name.BorderWidth = 1;
                tc_sec_name.Width = 100;
                tc_sec_name.ColumnSpan = 2;
                Literal lit_sec_name = new Literal();
                lit_sec_name.Text = "<center>" + dRow["Sec_Sale_Name"].ToString() + "</center>";
                tc_sec_name.Attributes.Add("Class", "Backcolor");
                tc_sec_name.Controls.Add(lit_sec_name);
                tr_header.Cells.Add(tc_sec_name);
            }


            tr_sub_header.BackColor = System.Drawing.Color.White;
            tr_sub_header.Attributes.Add("Class", "rptCellBorder");
            foreach (DataRow dRow in dsSecSales.Tables[0].Rows)
            {
                TableCell tc_qty = new TableCell();
                tc_qty.BorderStyle = BorderStyle.Solid;
                tc_qty.BorderWidth = 1;
                tc_qty.Width = 50;
                Literal lit_qty = new Literal();
                lit_qty.Text = "<center> Qty </center>";
                tc_qty.Attributes.Add("Class", "Backcolor");
                tc_qty.Controls.Add(lit_qty);
                tr_sub_header.Cells.Add(tc_qty);

                TableCell tc_val = new TableCell();
                tc_val.BorderStyle = BorderStyle.Solid;
                tc_val.BorderWidth = 1;
                tc_val.Width = 50;
                Literal lit_val = new Literal();
                lit_val.Text = "<center> Value </center>";
                tc_val.Attributes.Add("Class", "Backcolor");
                tc_val.Controls.Add(lit_val);
                tr_sub_header.Cells.Add(tc_val);
            }
        } // End of Sec Sales If condn

        tbl.Rows.Add(tr_header);
        tbl.Rows.Add(tr_sub_header);

        int iCount = 0; 
        int sqty = 0;
        double sval = 0.00;
        string[] arrqty = new string[10];
        string[] arrval = new string[10];

        foreach (DataRow dataRow in dsProd.Tables[0].Rows)
        {
            TableRow tr_det = new TableRow();
            tr_det.BackColor = System.Drawing.Color.White;
            tr_det.Attributes.Add("Class", "rptCellBorder");
            iCount += 1;

            TableCell tc_det_SNo = new TableCell();
            Literal lit_det_SNo = new Literal();
            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
            tc_det_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_SNo.BorderWidth = 1;
            tc_det_SNo.Width = 50;
            tc_det_SNo.Controls.Add(lit_det_SNo);
            tr_det.Cells.Add(tc_det_SNo);

            TableCell tc_det_prod_code = new TableCell();
            Literal lit_det_prod_code = new Literal();
            lit_det_prod_code.Text = "&nbsp;" + dataRow["Product_Detail_Code"].ToString();
            tc_det_prod_code.BorderStyle = BorderStyle.Solid;
            tc_det_prod_code.BorderWidth = 1;
            tc_det_prod_code.Controls.Add(lit_det_prod_code);
            tc_det_prod_code.Visible = false;
            tr_det.Cells.Add(tc_det_prod_code);

            TableCell tc_det_prod_name = new TableCell();
            Literal lit_det_prod_name = new Literal();
            lit_det_prod_name.Text = "&nbsp;" + dataRow["Product_Detail_Name"].ToString();
            tc_det_prod_name.BorderStyle = BorderStyle.Solid;
            tc_det_prod_name.BorderWidth = 1;
            tc_det_prod_name.Controls.Add(lit_det_prod_name);
            tr_det.Cells.Add(tc_det_prod_name);

            TableCell tc_det_prod_rate = new TableCell();
            Literal lit_det_prod_rate = new Literal();
            lit_det_prod_rate.Text = "&nbsp;" + dataRow["Distributor_Price"].ToString();
            tc_det_prod_rate.BorderStyle = BorderStyle.Solid;
            tc_det_prod_rate.BorderWidth = 1;
            tc_det_prod_rate.Controls.Add(lit_det_prod_rate);
            tr_det.Cells.Add(tc_det_prod_rate);

            sqty = 0;

            //Actual secondary sales values
            foreach (DataRow dRow in dsSecSales.Tables[0].Rows)
            {
                if (dRow["Sel_Sale_Operator"].ToString() == "C")
                {
                    dsReport = ss.getrptvalues_clbal(div_code, sf_code, stock_code, TMonth, TYear, dataRow["Product_Detail_Code"].ToString(), Convert.ToDouble(dataRow["Distributor_Price"].ToString()), Convert.ToInt16(dRow["Sec_Sale_Code"].ToString()));
                }
                else
                {
                    dsReport = ss.getrptvalues(div_code, sf_code, stock_code, FMonth, FYear, TMonth, TYear, dataRow["Product_Detail_Code"].ToString(), Convert.ToDouble(dataRow["Distributor_Price"].ToString()), Convert.ToInt16(dRow["Sec_Sale_Code"].ToString()));
                }
                TableCell tc_det_qty = new TableCell();
                tc_det_qty.BorderStyle = BorderStyle.Solid;
                tc_det_qty.BorderWidth = 1;
                tc_det_qty.HorizontalAlign = HorizontalAlign.Right;
                tc_det_qty.Width = 50;
                Literal lit_det_qty = new Literal();
                if (dsReport != null)
                {
                    lit_det_qty.Text = dsReport.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "&nbsp;";
                    arrqty[sqty] = lit_det_qty.Text;
                }
                else
                {
                    lit_det_qty.Text = "<center> &nbsp; </center>";
                }
                tc_det_qty.Attributes.Add("Class", "Backcolor");
                tc_det_qty.Controls.Add(lit_det_qty);
                tr_det.Cells.Add(tc_det_qty);

                TableCell tc_det_val = new TableCell();
                tc_det_val.BorderStyle = BorderStyle.Solid;
                tc_det_val.BorderWidth = 1;
                tc_det_val.HorizontalAlign = HorizontalAlign.Right;
                tc_det_val.Width = 50;
                Literal lit_det_val = new Literal();
                if (dsReport != null)
                {
                    //lit_det_val.Text = "<center>" + dsReport.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "</center>";
                    lit_det_val.Text = dsReport.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "&nbsp;";
                    if (lit_det_val.Text.Trim().Length > 0)
                    {
                        if (arrval[sqty] != null)
                        {
                            arrval[sqty] = Convert.ToString(Convert.ToDouble(arrval[sqty]) + Convert.ToDouble(dsReport.Tables[0].Rows[0].ItemArray.GetValue(1).ToString()));
                        }
                        else
                        {
                            arrval[sqty] = Convert.ToString(Convert.ToDouble(dsReport.Tables[0].Rows[0].ItemArray.GetValue(1).ToString()));
                        }
                    }
                }
                else
                {
                    lit_det_val.Text = "<center> &nbsp; </center>";
                }
                tc_det_val.Attributes.Add("Class", "Backcolor");
                tc_det_val.Controls.Add(lit_det_val);
                tr_det.Cells.Add(tc_det_val);

                sqty += 1;
            }

            tbl.Rows.Add(tr_det);

        }


        //rptProduct.DataSource = dsProd;
        //rptProduct.DataBind();

        TableRow tr_tot = new TableRow();
        tr_tot.BackColor = System.Drawing.Color.White;
        tr_tot.Attributes.Add("Class", "rptCellBorder");

        TableCell tc_tot = new TableCell();
        Literal lit_tot = new Literal();
        lit_tot.Text = "Total";
        tc_tot.BorderStyle = BorderStyle.Solid;
        tc_tot.ColumnSpan = 3;
        tc_tot.BorderWidth = 1;
        tc_tot.HorizontalAlign = HorizontalAlign.Center;
        tc_tot.Width = 50;
        tc_tot.Controls.Add(lit_tot);
        tr_tot.Cells.Add(tc_tot);

        sqty = 0;

        //Actual secondary sales values
        sqty = 0;
        foreach (DataRow dRow in dsSecSales.Tables[0].Rows)
        {
            TableCell tc_det_tot = new TableCell();
            tc_det_tot.BorderStyle = BorderStyle.Solid;
            tc_det_tot.BorderWidth = 1;
            Literal lit_det_tot = new Literal();
            lit_det_tot.Text = "<center> &nbsp; </center>";
            tc_det_tot.Attributes.Add("Class", "Backcolor");
            tc_det_tot.Controls.Add(lit_det_tot);
            tr_tot.Cells.Add(tc_det_tot);

            TableCell tc_det_val_tot = new TableCell();
            tc_det_val_tot.BorderStyle = BorderStyle.Solid;
            tc_det_val_tot.BorderWidth = 1;
            tc_det_val_tot.Width = 50;
            tc_det_val_tot.HorizontalAlign = HorizontalAlign.Right;
            Literal lit_det_val_tot = new Literal();
            if (arrval[sqty] != null)
            {
                if (arrval[sqty].Length > 0)
                {
                    //lit_det_val_tot.Text = "<center>" + arrval[sqty].ToString() + "</center>";
                    lit_det_val_tot.Text = arrval[sqty].ToString() + "&nbsp;";
                }
                else
                {
                    lit_det_val_tot.Text = "<center> &nbsp; </center>";
                }
            }

            tc_det_val_tot.Attributes.Add("Class", "Backcolor");
            tc_det_val_tot.Controls.Add(lit_det_val_tot);
            tr_tot.Cells.Add(tc_det_val_tot);

            sqty += 1;
        }

        tbl.Rows.Add(tr_tot);
    }


    private void PopulateSecSales_Month(DataSet dsProd, int stock_code)
    {
        int monthdiff = -1;
        int curmonth = -1;
        int curyear = -1;
        int curindex = -1;

        SecSale ss = new SecSale();

        TableRow tr_header = new TableRow();
        tr_header.BorderStyle = BorderStyle.Solid;
        tr_header.BorderWidth = 1;
        tr_header.Attributes.Add("Class", "Backcolor");

        TableCell tc_SNo = new TableCell();
        tc_SNo.BorderStyle = BorderStyle.Solid;
        tc_SNo.BorderWidth = 1;
        tc_SNo.Width = 50;
        tc_SNo.RowSpan = 3;
        Literal lit_SNo = new Literal();
        lit_SNo.Text = "S.No";
        tc_SNo.HorizontalAlign = HorizontalAlign.Center;
        tc_SNo.Attributes.Add("Class", "Backcolor");
        tc_SNo.Controls.Add(lit_SNo);
        tr_header.Cells.Add(tc_SNo);

        TableCell tc_Prod_Code = new TableCell();
        tc_Prod_Code.BorderStyle = BorderStyle.Solid;
        tc_Prod_Code.BorderWidth = 1;
        tc_Prod_Code.Width = 100;
        tc_Prod_Code.RowSpan = 3;
        Literal lit_Prod_Code = new Literal();
        lit_Prod_Code.Text = "<center>Prod Code</center>";
        tc_Prod_Code.Attributes.Add("Class", "Backcolor");
        tc_Prod_Code.Controls.Add(lit_Prod_Code);
        tc_Prod_Code.Visible = false;
        tr_header.Cells.Add(tc_Prod_Code);

        TableCell tc_Prod_Name = new TableCell();
        tc_Prod_Name.BorderStyle = BorderStyle.Solid;
        tc_Prod_Name.BorderWidth = 1;
        tc_Prod_Name.Width = 200;
        tc_Prod_Name.RowSpan = 3;
        Literal lit_Prod_Name = new Literal();
        lit_Prod_Name.Text = "<center>Product Name</center>";
        tc_Prod_Name.Attributes.Add("Class", "Backcolor");
        tc_Prod_Name.Controls.Add(lit_Prod_Name);
        tr_header.Cells.Add(tc_Prod_Name);

        TableCell tc_Rate = new TableCell();
        tc_Rate.BorderStyle = BorderStyle.Solid;
        tc_Rate.BorderWidth = 1;
        tc_Rate.Width = 70;
        tc_Rate.RowSpan = 3;
        Literal lit_Rate = new Literal();
        lit_Rate.Text = "<center>Rate</center>";
        tc_Rate.Attributes.Add("Class", "Backcolor");
        tc_Rate.Controls.Add(lit_Rate);
        tr_header.Cells.Add(tc_Rate);

        if (FYear == TYear)
        {
            if (TMonth >= FMonth)
            {
                monthdiff = (TMonth - FMonth) + 1;
            }
        }
        else
        {
            monthdiff = (12 - FMonth) + 1;
            monthdiff = monthdiff + (TMonth - 0);
        }

        dsSecSales = ss.getrptfield(div_code);
        TableRow tr_sub_header = new TableRow();

        //Month column
        TableRow tr_mth_header = new TableRow();
        
        curmonth = FMonth;
        curyear = FYear;

        for (curindex = 0; curindex < monthdiff; curindex++)
        {
            TableCell tc_mth = new TableCell();
            tc_mth.BorderStyle = BorderStyle.Solid;
            tc_mth.BorderWidth = 1;
            tc_mth.Width = 200;
            tc_mth.ColumnSpan = (2 * (dsSecSales.Tables[0].Rows.Count));
            Literal lit_mth = new Literal();
            lit_mth.Text = "<center>" + getMonthName(curmonth) + "</center>";
            tc_mth.Attributes.Add("Class", "Backcolor");
            tc_mth.Controls.Add(lit_mth);
            //tr_mth_header.Cells.Add(tc_mth);
            tr_header.Cells.Add(tc_mth);
            curmonth += 1;

            //If To Year > From Year
            if (curmonth == 13)
            {
                curmonth = 1;
                curyear += 1;
            }
            //}


            //TableRow tr_sub_header = new TableRow();
            //Get Secondary Sales master data from DB and bind it with Secondary Sales Header Repeater            

            if (dsSecSales.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dRow in dsSecSales.Tables[0].Rows)
                {
                    TableCell tc_sec_name = new TableCell();
                    tc_sec_name.BorderStyle = BorderStyle.Solid;
                    tc_sec_name.BorderWidth = 1;
                    tc_sec_name.Width = 100;
                    tc_sec_name.ColumnSpan = 2;
                    Literal lit_sec_name = new Literal();
                    lit_sec_name.Text = "<center>" + dRow["Sec_Sale_Name"].ToString() + "</center>";
                    tc_sec_name.Attributes.Add("Class", "Backcolor");
                    tc_sec_name.Controls.Add(lit_sec_name);
                    //tr_header.Cells.Add(tc_sec_name);
                    tr_mth_header.Cells.Add(tc_sec_name);
                }


                tr_sub_header.BackColor = System.Drawing.Color.White;
                tr_sub_header.Attributes.Add("Class", "rptCellBorder");
                foreach (DataRow dRow in dsSecSales.Tables[0].Rows)
                {
                    TableCell tc_qty = new TableCell();
                    tc_qty.BorderStyle = BorderStyle.Solid;
                    tc_qty.BorderWidth = 1;
                    tc_qty.Width = 50;
                    Literal lit_qty = new Literal();
                    lit_qty.Text = "<center> Qty </center>";
                    tc_qty.Attributes.Add("Class", "Backcolor");
                    tc_qty.Controls.Add(lit_qty);
                    tr_sub_header.Cells.Add(tc_qty);

                    TableCell tc_val = new TableCell();
                    tc_val.BorderStyle = BorderStyle.Solid;
                    tc_val.BorderWidth = 1;
                    tc_val.Width = 50;
                    Literal lit_val = new Literal();
                    lit_val.Text = "<center> Value </center>";
                    tc_val.Attributes.Add("Class", "Backcolor");
                    tc_val.Controls.Add(lit_val);
                    tr_sub_header.Cells.Add(tc_val);
                }
            } // End of Sec Sales If condn

        }

        tbl.Rows.Add(tr_header);
        tbl.Rows.Add(tr_mth_header);
        tbl.Rows.Add(tr_sub_header);

        int iCount = 0;
        int sqty = 0;
        double sval = 0.00;
        string[] arrqty = new string[50];
        string[] arrval = new string[50];

        foreach (DataRow dataRow in dsProd.Tables[0].Rows)
        {
            TableRow tr_det = new TableRow();
            tr_det.BackColor = System.Drawing.Color.White;
            tr_det.Attributes.Add("Class", "rptCellBorder");
            iCount += 1;

            TableCell tc_det_SNo = new TableCell();
            Literal lit_det_SNo = new Literal();
            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
            tc_det_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_SNo.BorderWidth = 1;
            tc_det_SNo.Width = 50;
            tc_det_SNo.Controls.Add(lit_det_SNo);
            tr_det.Cells.Add(tc_det_SNo);

            TableCell tc_det_prod_code = new TableCell();
            Literal lit_det_prod_code = new Literal();
            lit_det_prod_code.Text = "&nbsp;" + dataRow["Product_Detail_Code"].ToString();
            tc_det_prod_code.BorderStyle = BorderStyle.Solid;
            tc_det_prod_code.BorderWidth = 1;
            tc_det_prod_code.Controls.Add(lit_det_prod_code);
            tc_det_prod_code.Visible = false;
            tr_det.Cells.Add(tc_det_prod_code);

            TableCell tc_det_prod_name = new TableCell();
            Literal lit_det_prod_name = new Literal();
            lit_det_prod_name.Text = "&nbsp;" + dataRow["Product_Detail_Name"].ToString();
            tc_det_prod_name.BorderStyle = BorderStyle.Solid;
            tc_det_prod_name.BorderWidth = 1;
            tc_det_prod_name.Controls.Add(lit_det_prod_name);
            tr_det.Cells.Add(tc_det_prod_name);

            TableCell tc_det_prod_rate = new TableCell();
            Literal lit_det_prod_rate = new Literal();
            lit_det_prod_rate.Text = "&nbsp;" + dataRow["Distributor_Price"].ToString();
            tc_det_prod_rate.BorderStyle = BorderStyle.Solid;
            tc_det_prod_rate.BorderWidth = 1;
            tc_det_prod_rate.Controls.Add(lit_det_prod_rate);
            tr_det.Cells.Add(tc_det_prod_rate);

            sqty = 0;

            curmonth = FMonth;
            curyear = FYear;

            for (curindex = 0; curindex < monthdiff; curindex++)
            {

                //Actual secondary sales values
                foreach (DataRow dRow in dsSecSales.Tables[0].Rows)
                {
                    //dsReport = ss.getrptvalues(div_code, sf_code, stock_code, FMonth, FYear, TMonth, TYear, dataRow["Product_Detail_Code"].ToString(), Convert.ToDouble(dataRow["Distributor_Price"].ToString()), Convert.ToInt16(dRow["Sec_Sale_Code"].ToString()));
                    dsReport = ss.getrptvalues(div_code, sf_code, stock_code, curmonth, curyear, curmonth, curyear, dataRow["Product_Detail_Code"].ToString(), Convert.ToDouble(dataRow["Distributor_Price"].ToString()), Convert.ToInt16(dRow["Sec_Sale_Code"].ToString()));
                    TableCell tc_det_qty = new TableCell();
                    tc_det_qty.BorderStyle = BorderStyle.Solid;
                    tc_det_qty.BorderWidth = 1;
                    tc_det_qty.HorizontalAlign = HorizontalAlign.Right;
                    tc_det_qty.Width = 50;
                    Literal lit_det_qty = new Literal();
                    if (dsReport != null)
                    {
                        //lit_det_qty.Text = "<center>" + dsReport.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "</center>";
                        lit_det_qty.Text = dsReport.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "&nbsp;";
                        arrqty[sqty] = lit_det_qty.Text;
                    }
                    else
                    {
                        lit_det_qty.Text = "<center> &nbsp; </center>";
                    }
                    tc_det_qty.Attributes.Add("Class", "Backcolor");
                    tc_det_qty.Controls.Add(lit_det_qty);
                    tr_det.Cells.Add(tc_det_qty);

                    TableCell tc_det_val = new TableCell();
                    tc_det_val.BorderStyle = BorderStyle.Solid;
                    tc_det_val.BorderWidth = 1;
                    tc_det_val.HorizontalAlign = HorizontalAlign.Right;
                    tc_det_val.Width = 50;
                    Literal lit_det_val = new Literal();
                    if (dsReport != null)
                    {
                        //lit_det_val.Text = "<center>" + dsReport.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "</center>";
                        lit_det_val.Text = dsReport.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "&nbsp;";
                        if (lit_det_val.Text.Trim().Length > 0)
                        {
                            if (arrval[sqty] != null)
                            {
                                arrval[sqty] = Convert.ToString(Convert.ToDouble(arrval[sqty]) + Convert.ToDouble(dsReport.Tables[0].Rows[0].ItemArray.GetValue(1).ToString()));
                            }
                            else
                            {
                                arrval[sqty] = Convert.ToString(Convert.ToDouble(dsReport.Tables[0].Rows[0].ItemArray.GetValue(1).ToString()));
                            }
                        }
                    }
                    else
                    {
                        lit_det_val.Text = "<center> &nbsp; </center>";
                    }
                    tc_det_val.Attributes.Add("Class", "Backcolor");
                    tc_det_val.Controls.Add(lit_det_val);
                    tr_det.Cells.Add(tc_det_val);

                    sqty += 1;
                }
            

            tbl.Rows.Add(tr_det);
            curmonth += 1;
            //If To Year > From Year
            if (curmonth == 13)
            {
                curmonth = 1;
                curyear += 1;
            }

            }
        }


        //rptProduct.DataSource = dsProd;
        //rptProduct.DataBind();

        TableRow tr_tot = new TableRow();
        tr_tot.BackColor = System.Drawing.Color.White;
        tr_tot.Attributes.Add("Class", "rptCellBorder");

        TableCell tc_tot = new TableCell();
        Literal lit_tot = new Literal();
        lit_tot.Text = "Total";
        tc_tot.BorderStyle = BorderStyle.Solid;
        tc_tot.ColumnSpan = 3;
        tc_tot.BorderWidth = 1;
        tc_tot.HorizontalAlign = HorizontalAlign.Center;
        tc_tot.Width = 50;
        tc_tot.Controls.Add(lit_tot);
        tr_tot.Cells.Add(tc_tot);

        sqty = 0;

        //Actual secondary sales values
        sqty = 0;
        curmonth = FMonth;
        curyear = FYear;

        for (curindex = 0; curindex < monthdiff; curindex++)
        {

            foreach (DataRow dRow in dsSecSales.Tables[0].Rows)
            {
                TableCell tc_det_tot = new TableCell();
                tc_det_tot.BorderStyle = BorderStyle.Solid;
                tc_det_tot.BorderWidth = 1;
                Literal lit_det_tot = new Literal();
                lit_det_tot.Text = "<center> &nbsp; </center>";
                tc_det_tot.Attributes.Add("Class", "Backcolor");
                tc_det_tot.Controls.Add(lit_det_tot);
                tr_tot.Cells.Add(tc_det_tot);

                TableCell tc_det_val_tot = new TableCell();
                tc_det_val_tot.BorderStyle = BorderStyle.Solid;
                tc_det_val_tot.BorderWidth = 1;
                tc_det_val_tot.Width = 50;
                tc_det_val_tot.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_det_val_tot = new Literal();
                if (arrval[sqty].Length > 0)
                {
                    //lit_det_val_tot.Text = "<center>" + arrval[sqty].ToString() + "</center>";
                    lit_det_val_tot.Text = arrval[sqty].ToString() + "&nbsp;";
                }
                else
                {
                    lit_det_val_tot.Text = "<center> &nbsp; </center>";
                }
                tc_det_val_tot.Attributes.Add("Class", "Backcolor");
                tc_det_val_tot.Controls.Add(lit_det_val_tot);
                tr_tot.Cells.Add(tc_det_val_tot);

                sqty += 1;
            }

            tbl.Rows.Add(tr_tot);
            curmonth += 1;
            //If To Year > From Year
            if (curmonth == 13)
            {
                curmonth = 1;
                curyear += 1;
            }

        }
    }
    

    //Get the last day for the given month & year
    private int GetLastDay(int cMonth, int cYear)
    {
        int cday = 0;

        if (cMonth == 1)
            cday = 31;
        else if (cMonth == 2)
        {
            if (cYear % 4 == 0)
                cday = 29;
            else
                cday = 28;
        }
        else if (cMonth == 3)
            cday = 31;
        else if (cMonth == 4)
            cday = 30;
        else if (cMonth == 5)
            cday = 31;
        else if (cMonth == 6)
            cday = 30;
        else if (cMonth == 7)
            cday = 31;
        else if (cMonth == 8)
            cday = 31;
        else if (cMonth == 9)
            cday = 30;
        else if (cMonth == 10)
            cday = 31;
        else if (cMonth == 11)
            cday = 30;
        else if (cMonth == 12)
            cday = 31;

        return cday;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Export.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

}

