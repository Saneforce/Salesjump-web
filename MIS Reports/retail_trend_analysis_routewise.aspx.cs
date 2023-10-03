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
using iTextSharp.tool.xml;
using Bus_EReport;
using System.Net;
public partial class MIS_Reports_retail_trend_analysis_routewise : System.Web.UI.Page
{

    decimal netsumtotal = 0;
    string sfCode = string.Empty;
    string sf_type = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    decimal sumtotal = 0;
    decimal catsumtotal = 0;
    decimal prosumtotal;
    decimal ntprosumtotal = 0;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    decimal iTotLstCount2 = 0;
    decimal ff = 0;
    decimal brandsumtotal = 0;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    decimal value1 = 0;
    decimal value2 = 0;
    string viewby = string.Empty;
    string tot_dr = string.Empty;
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;
    string tot_value = string.Empty;
    decimal val = 0;
    DataSet dsprd = new DataSet();
    string subdivision = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        subdivision = Request.QueryString["Subdivision"].ToString();
        viewby = Request.QueryString["viewby"].ToString();
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        //string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Retail Trend Analysis Details for  " + strFMonthName + " " + FYear;
        if (viewby == "DistributorWise")
        {

            if (sf_type == "1")
            {
                FillSF_MR();
            }
            else if (sf_type == "2")
            {
                FillSF_MR();
            }
            else if (sf_type == "3")
            {
                FillSF();
            }
        }
        else if (viewby == "CategoryWise")
        {

            FillCategorywise();
        }
        else if (viewby == "BrandWise")
        {

            FillBrandwise();

        }
        else if (viewby == "ProductWise")
        {

            FillProductwise();

        }

        else if (viewby == "ChannelWise")
        {
            FillChannelwise();
        }


        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;


    }

    private void FillSF_MR()
    {
        Random rndm = new Random();
        string t = "1";
        string stCrtDtaPnt = string.Empty;
        int mnth = Convert.ToInt32(FMonth), year = Convert.ToInt32(FYear);
        DateTime dt = Convert.ToDateTime(year + "-" + mnth.ToString() + "-" + t.ToString());
        string fromdate = dt.ToString("yyyy-MM-dd 00:00:00.000");
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd 23:59:59.999");
        string TYear = DateTime.Now.Year.ToString();
        string TMonth = DateTime.Now.Month.ToString();
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.GetStockName_Customer(divcode, subdivision);

        if (sf_type == "1")
        {

            dsSalesForce = sf.rretail_Trend_analysis_stockist_MR(divcode, fromdate, todate, subdivision, sfCode);

        }
        else
        {

            dsSalesForce = sf.rretail_Trend_analysis_stockist_MGR(divcode, fromdate, todate, subdivision, sfCode);

        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#496a9a");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            tc_SNo.Style.Add("text-align", "center");
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);



            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
            tc_DR_Name.RowSpan = 1;
            tc_DR_Name.Style.Add("text-align", "center");
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Distributor Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 200;
            tc_DR_Code.RowSpan = 1;
            tc_DR_Code.Style.Add("text-align", "center");
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "Total";
            tc_DR_Code.Controls.Add(lit_DR_Code);

            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);
            TableCell tc_DR_Code_month = new TableCell();
            tc_DR_Code_month.BorderStyle = BorderStyle.Solid;
            tc_DR_Code_month.BorderWidth = 1;
            tc_DR_Code_month.Width = 200;
            tc_DR_Code_month.RowSpan = 1;
            tc_DR_Code_month.Style.Add("text-align", "center");
            Literal lit_DR_Code_mon = new Literal();
            lit_DR_Code_mon.Text = "% Distribution";
            tc_DR_Code_month.Controls.Add(lit_DR_Code_mon);
            tc_DR_Code_month.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code_month.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code_month);


            tbl.Rows.Add(tr_header);



            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString();

                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tc_det_SNo.Style.Add("text-align", "right");
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();

                lit_det_usr.Text = "&nbsp;" + drFF["Stockist_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Attributes.Add("style", "color:Blue;");
                tc_det_FF.Width = 300;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                stCrtDtaPnt += "{label:\"" + drFF["Stockist_Name"].ToString() + "\",y:";
                sURL = "rpt_retail_routemonth.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_Name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&TMonth=" + TMonth + "&TYear=" + TYear + "&Subdivision=" + subdivision + "&viewby=" + viewby + "";
                lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                lit_det_FF.NavigateUrl = "#";
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);



                TableCell tc_det_last6monthsum = new TableCell();
                tc_det_last6monthsum.Width = 200;

                tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Right;
                HyperLink lit_det_sum = new HyperLink();
                lit_det_sum.Text = drFF["value"] == DBNull.Value ? drFF["value"].ToString() : Convert.ToDecimal(drFF["value"]).ToString("0.00");
                string hh = lit_det_sum.Text;


                if (hh != "")
                {
                    ff = Convert.ToDecimal(hh);
                    value1 = ff;
                    sumtotal += Convert.ToDecimal(value1);
                    stCrtDtaPnt += Convert.ToString(value1) + "},";
                }
                else
                {
                    value1 = 0;
                    stCrtDtaPnt += "0},";
                }
                tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                tc_det_last6monthsum.BorderWidth = 1;
                tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                tc_det_last6monthsum.Controls.Add(lit_det_sum);
                tr_det.Cells.Add(tc_det_last6monthsum);



                sumtotal += Convert.ToDecimal(value1);
                sURL = "rpt_retailtrend_monthwise.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_Name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&TMonth=" + TMonth + "&TYear=" + TYear + "&Subdivision=" + subdivision + "&viewby=" + viewby + "";
                lit_det_sum.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                lit_det_sum.NavigateUrl = "#";

                TableCell tc_det_currentmonth = new TableCell();
                tc_det_currentmonth.Width = 200;
                tc_det_currentmonth.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_det_mon = new Literal();
                //lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                tc_det_currentmonth.BorderStyle = BorderStyle.Solid;
                tc_det_currentmonth.BorderWidth = 1;
                tc_det_currentmonth.Attributes.Add("Class", "rptCellBorder");
                tc_det_currentmonth.Controls.Add(lit_det_mon);
                tr_det.Cells.Add(tc_det_currentmonth);

                dsDoc = sf.Retail_Trend_analysis_totalvalue(divcode, fromdate, todate, subdivision);


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_value = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_value != "" && tot_value != "0")
                {
                    value2 = (Decimal.Parse(tot_value));

                    val = (Convert.ToDecimal(value1 / value2) * 100);

                    decimal value = Math.Round(val, 0);
                    lit_det_mon.Text = value.ToString("0.00");

                }
                else
                {
                    lit_det_mon.Text = "";
                }

                //val =tot_dr) / (tot_value);




                tot_dr = "0";
                tot_value = "0";
                value1 = 0;
                value2 = 0;
                val = 0;

                tbl.Rows.Add(tr_det);


            }
            TableRow tr_total = new TableRow();

            TableCell tc_Count_Total = new TableCell();
            tc_Count_Total.BorderStyle = BorderStyle.Solid;
            tc_Count_Total.BorderWidth = 1;
            //tc_catg_Total.Width = 25;
            Literal lit_Count_Total = new Literal();
            lit_Count_Total.Text = "Total";
            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_Count_Total.Controls.Add(lit_Count_Total);
            tc_Count_Total.Font.Bold.ToString();
            tc_Count_Total.BackColor = System.Drawing.Color.White;
            tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
            tc_Count_Total.ColumnSpan = 2;
            tc_Count_Total.Style.Add("text-align", "center");
            tc_Count_Total.Style.Add("font-family", "Calibri");
            tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
            tc_Count_Total.Style.Add("font-size", "10pt");

            tr_total.Cells.Add(tc_Count_Total);
            TableCell tc_tot_month = new TableCell();
            HyperLink hyp_month = new HyperLink();
            hyp_month.Text = sumtotal.ToString("0.00");
            string total = "Total";
            sURL = "rpt_retail_routemonth.aspx?FYear=" + FYear + "&FMonth=" + FMonth + "&TMonth=" + TMonth + "&TYear=" + TYear + "&Subdivision=" + subdivision + "&viewby=" + total + "";
            hyp_month.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


            hyp_month.NavigateUrl = "#";
            tc_tot_month.BorderStyle = BorderStyle.Solid;
            tc_tot_month.BorderWidth = 1;
            tc_tot_month.BackColor = System.Drawing.Color.White;
            tc_tot_month.Width = 200;
            tc_tot_month.Style.Add("font-family", "Calibri");
            tc_tot_month.Style.Add("font-size", "10pt");
            tc_tot_month.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_month.VerticalAlign = VerticalAlign.Middle;
            tc_tot_month.Controls.Add(hyp_month);
            tc_tot_month.Attributes.Add("style", "font-weight:bold;");
            tc_tot_month.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_month);
            tbl.Rows.Add(tr_total);

            string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);





        }

    }
    private void FillSF()
    {
        Random rndm = new Random();
        string t = "1";
        string stCrtDtaPnt = string.Empty;
        int mnth = Convert.ToInt32(FMonth), year = Convert.ToInt32(FYear);
        DateTime dt = Convert.ToDateTime(year + "-" + mnth.ToString() + "-" + t.ToString());
        string fromdate = dt.ToString("yyyy-MM-dd 00:00:00.000");
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd 23:59:59.999");
        string TYear = DateTime.Now.Year.ToString();
        string TMonth = DateTime.Now.Month.ToString();
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.GetStockName_Customer(divcode, subdivision);

        dsSalesForce = sf.rretail_Trend_analysis_stockist(divcode, fromdate, todate, subdivision);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#496a9a");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            tc_SNo.Style.Add("text-align", "center");
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);



            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
            tc_DR_Name.RowSpan = 1;
            tc_DR_Name.Style.Add("text-align", "center");
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Distributor Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);
            TableCell tc_DR_value1 = new TableCell();
            tc_DR_value1.BorderStyle = BorderStyle.Solid;
            tc_DR_value1.BorderWidth = 1;
            tc_DR_value1.Width = 150;
            tc_DR_value1.RowSpan = 1;
            tc_DR_value1.Style.Add("text-align", "center");
            Literal lit_DR_value1 = new Literal();
            lit_DR_value1.Text = "Net Weight Value";
            tc_DR_value1.BorderColor = System.Drawing.Color.Black;
            tc_DR_value1.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value1.Controls.Add(lit_DR_value1);
            tr_header.Cells.Add(tc_DR_value1);


            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 200;
            tc_DR_Code.RowSpan = 1;
            tc_DR_Code.Style.Add("text-align", "center");
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "Total";
            tc_DR_Code.Controls.Add(lit_DR_Code);

            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);
            TableCell tc_DR_Code_month = new TableCell();
            tc_DR_Code_month.BorderStyle = BorderStyle.Solid;
            tc_DR_Code_month.BorderWidth = 1;
            tc_DR_Code_month.Width = 200;
            tc_DR_Code_month.RowSpan = 1;
            tc_DR_Code_month.Style.Add("text-align", "center");
            Literal lit_DR_Code_mon = new Literal();
            lit_DR_Code_mon.Text = "% Distribution";
            tc_DR_Code_month.Controls.Add(lit_DR_Code_mon);
            tc_DR_Code_month.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code_month.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code_month);









            tbl.Rows.Add(tr_header);



            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString();
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Style.Add("text-align", "right");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();

                lit_det_usr.Text = "&nbsp;" + drFF["Stockist_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Attributes.Add("style", "color:Blue;");
                tc_det_FF.Width = 300;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                stCrtDtaPnt += "{label:\"" + drFF["Stockist_Name"].ToString() + "\",y:";
                sURL = "rpt_retail_routemonth.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_Name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&TMonth=" + TMonth + "&TYear=" + TYear + "&Subdivision=" + subdivision + "&viewby=" + viewby + "";
                lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                lit_det_FF.NavigateUrl = "#";
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                TableCell tc_lst_month1 = new TableCell();
                HyperLink hyp_lst_month1 = new HyperLink();
                tc_lst_month1.Width = 150;
                if (drFF["net_weight_value"].ToString() != "")
                {
                    hyp_lst_month1.Text = drFF["net_weight_value"] == DBNull.Value ? drFF["net_weight_value"].ToString() : Convert.ToDecimal(drFF["net_weight_value"]).ToString("0.00");
                    string lk = hyp_lst_month1.Text;
                    decimal gg = Convert.ToDecimal(lk);
                    netsumtotal += gg;
                }
                else
                {
                    hyp_lst_month1.Text = "";

                }


                tc_lst_month1.BorderStyle = BorderStyle.Solid;

                tc_lst_month1.BorderStyle = BorderStyle.Solid;
                tc_lst_month1.BorderWidth = 1;
                tc_lst_month1.BackColor = System.Drawing.Color.White;
                tc_lst_month1.Width = 200;
                tc_lst_month1.HorizontalAlign = HorizontalAlign.Right;
                tc_lst_month1.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month1.Controls.Add(hyp_lst_month1);
                tc_lst_month1.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month1);


                sURL = "rpt_retailtrend_monthwise.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_Name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&TMonth=" + TMonth + "&TYear=" + TYear + "&Subdivision=" + subdivision + "&viewby=" + viewby + "";
                hyp_lst_month1.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                hyp_lst_month1.NavigateUrl = "#";


                TableCell tc_det_last6monthsum = new TableCell();
                tc_det_last6monthsum.Width = 200;

                tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Right;
                HyperLink lit_det_sum = new HyperLink();
                lit_det_sum.Text = drFF["value"] == DBNull.Value ? drFF["value"].ToString() : Convert.ToDecimal(drFF["value"]).ToString("0.00");
                string hh = lit_det_sum.Text;


                if (hh != "")
                {
                    ff = Convert.ToDecimal(hh);
                    value1 = ff;
                    sumtotal += Convert.ToDecimal(value1);
                    stCrtDtaPnt += Convert.ToString(value1) + "},";
                }
                else
                {
                    value1 = 0;
                    stCrtDtaPnt += "0},";
                }
                tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                tc_det_last6monthsum.BorderWidth = 1;
                tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                tc_det_last6monthsum.Controls.Add(lit_det_sum);
                tr_det.Cells.Add(tc_det_last6monthsum);




                sURL = "rpt_retailtrend_monthwise.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_Name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&TMonth=" + TMonth + "&TYear=" + TYear + "&Subdivision=" + subdivision + "&viewby=" + viewby + "";
                lit_det_sum.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                lit_det_sum.NavigateUrl = "#";

                TableCell tc_det_currentmonth = new TableCell();
                tc_det_currentmonth.Width = 200;
                tc_det_currentmonth.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_det_mon = new Literal();
                //lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                tc_det_currentmonth.BorderStyle = BorderStyle.Solid;
                tc_det_currentmonth.BorderWidth = 1;
                tc_det_currentmonth.Attributes.Add("Class", "rptCellBorder");
                tc_det_currentmonth.Controls.Add(lit_det_mon);
                tr_det.Cells.Add(tc_det_currentmonth);

                dsDoc = sf.Retail_Trend_analysis_totalvalue(divcode, fromdate, todate, subdivision);


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_value = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_value != "" && tot_value != "0")
                {
                    value2 = (Decimal.Parse(tot_value));
                    val = (Convert.ToDecimal(value1 / value2) * 100);

                    decimal value = Math.Round(val, 0);
                    lit_det_mon.Text = value.ToString("0.00");

                }
                else
                {
                    lit_det_mon.Text = "";
                }

                //val =tot_dr) / (tot_value);




                tot_dr = "0";
                tot_value = "0";
                value1 = 0;
                value2 = 0;
                val = 0;

                tbl.Rows.Add(tr_det);


            }
            TableRow tr_total = new TableRow();

            TableCell tc_Count_Total = new TableCell();
            tc_Count_Total.BorderStyle = BorderStyle.Solid;
            tc_Count_Total.BorderWidth = 1;
            //tc_catg_Total.Width = 25;
            Literal lit_Count_Total = new Literal();
            lit_Count_Total.Text = "Total";
            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_Count_Total.Controls.Add(lit_Count_Total);
            tc_Count_Total.Font.Bold.ToString();
            tc_Count_Total.BackColor = System.Drawing.Color.White;
            tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
            tc_Count_Total.ColumnSpan = 2;
            tc_Count_Total.Style.Add("text-align", "center");
            tc_Count_Total.Style.Add("font-family", "Calibri");
            tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
            tc_Count_Total.Style.Add("font-size", "10pt");

            tr_total.Cells.Add(tc_Count_Total);



            TableCell tc_tot_monthd = new TableCell();
            HyperLink hyp_monthd = new HyperLink();
            hyp_monthd.Text = netsumtotal.ToString("0.00");
            tc_tot_monthd.BorderStyle = BorderStyle.Solid;
            tc_tot_monthd.BorderWidth = 1;
            tc_tot_monthd.BackColor = System.Drawing.Color.White;
            tc_tot_monthd.Width = 200;
            tc_tot_monthd.Style.Add("font-family", "Calibri");
            tc_tot_monthd.Style.Add("font-size", "10pt");
            tc_tot_monthd.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_monthd.VerticalAlign = VerticalAlign.Middle;
            tc_tot_monthd.Controls.Add(hyp_monthd);
            tc_tot_monthd.Attributes.Add("style", "font-weight:bold;");
            tc_tot_monthd.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_monthd);






            TableCell tc_tot_month = new TableCell();
            HyperLink hyp_month = new HyperLink();
            hyp_month.Text = sumtotal.ToString("0.00");
            string total = "Total";
            sURL = "rpt_retail_routemonth.aspx?FYear=" + FYear + "&FMonth=" + FMonth + "&TMonth=" + TMonth + "&TYear=" + TYear + "&Subdivision=" + subdivision + "&viewby=" + total + "";
            hyp_month.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


            hyp_month.NavigateUrl = "#";
            tc_tot_month.BorderStyle = BorderStyle.Solid;
            tc_tot_month.BorderWidth = 1;
            tc_tot_month.BackColor = System.Drawing.Color.White;
            tc_tot_month.Width = 200;
            tc_tot_month.Style.Add("font-family", "Calibri");
            tc_tot_month.Style.Add("font-size", "10pt");
            tc_tot_month.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_month.VerticalAlign = VerticalAlign.Middle;
            tc_tot_month.Controls.Add(hyp_month);
            tc_tot_month.Attributes.Add("style", "font-weight:bold;");
            tc_tot_month.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_month);
            tbl.Rows.Add(tr_total);

            string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);





        }

    }
    private void FillCategorywise()
    {
        Random rndm = new Random();
        string t = "1";
        string stCrtDtaPnt = string.Empty;
        int mnth = Convert.ToInt32(FMonth), year = Convert.ToInt32(FYear);
        DateTime dt = Convert.ToDateTime(year + "-" + mnth.ToString() + "-" + t.ToString());
        string fromdate = dt.ToString("yyyy-MM-dd 00:00:00.000");
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd 23:59:59.999");
        string TYear = DateTime.Now.Year.ToString();
        string TMonth = DateTime.Now.Month.ToString();
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Retail_Trend_analysis_categorywise(divcode, fromdate, todate, subdivision);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#496a9a");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            tc_SNo.Style.Add("text-align", "center");
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);



            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
            tc_DR_Name.RowSpan = 1;
            tc_DR_Name.Style.Add("text-align", "center");
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Category Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);




            TableCell tc_DR_value1 = new TableCell();
            tc_DR_value1.BorderStyle = BorderStyle.Solid;
            tc_DR_value1.BorderWidth = 1;
            tc_DR_value1.Width = 150;
            tc_DR_value1.RowSpan = 1;
            tc_DR_value1.Style.Add("text-align", "center");
            Literal lit_DR_value1 = new Literal();
            lit_DR_value1.Text = "Net Weight Value";
            tc_DR_value1.BorderColor = System.Drawing.Color.Black;
            tc_DR_value1.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value1.Controls.Add(lit_DR_value1);
            tr_header.Cells.Add(tc_DR_value1);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 200;
            tc_DR_Code.RowSpan = 1;
            tc_DR_Code.Style.Add("text-align", "center");
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "Total";
            tc_DR_Code.Controls.Add(lit_DR_Code);

            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);
            TableCell tc_DR_Code_month = new TableCell();
            tc_DR_Code_month.BorderStyle = BorderStyle.Solid;
            tc_DR_Code_month.BorderWidth = 1;
            tc_DR_Code_month.Width = 200;
            tc_DR_Code_month.RowSpan = 1;
            tc_DR_Code_month.Style.Add("text-align", "center");
            Literal lit_DR_Code_mon = new Literal();
            lit_DR_Code_mon.Text = "% Distribution";
            tc_DR_Code_month.Controls.Add(lit_DR_Code_mon);
            tc_DR_Code_month.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code_month.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code_month);




            SalesForce sal = new SalesForce();




            tbl.Rows.Add(tr_header);



            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString();
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Style.Add("text-align", "right");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();

                lit_det_usr.Text = "&nbsp;" + drFF["Product_Cat_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Attributes.Add("style", "color:Blue;");
                tc_det_FF.Width = 300;
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp;" + drFF["Product_Cat_Name"].ToString();
                stCrtDtaPnt += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y:";
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                TableCell tc_lst_month1 = new TableCell();
                HyperLink hyp_lst_month1 = new HyperLink();
                tc_lst_month1.Width = 150;
                if (drFF["net_weight_value"].ToString() != "")
                {
                    hyp_lst_month1.Text = drFF["net_weight_value"] == DBNull.Value ? drFF["net_weight_value"].ToString() : Convert.ToDecimal(drFF["net_weight_value"]).ToString("0.00");
                    string lk = hyp_lst_month1.Text;
                    decimal gg = Convert.ToDecimal(lk);
                    netsumtotal += gg;
                }
                else
                {
                    hyp_lst_month1.Text = "";

                }


                tc_lst_month1.BorderStyle = BorderStyle.Solid;

                tc_lst_month1.BorderStyle = BorderStyle.Solid;
                tc_lst_month1.BorderWidth = 1;
                tc_lst_month1.BackColor = System.Drawing.Color.White;
                tc_lst_month1.Width = 200;
                tc_lst_month1.HorizontalAlign = HorizontalAlign.Right;
                tc_lst_month1.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month1.Controls.Add(hyp_lst_month1);
                tc_lst_month1.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month1);


                sURL = "rpt_retailtrend_monthwise.aspx?Product_Cat_Code=" + drFF["Product_Cat_Code"] + "&Product_Cat_Name=" + drFF["Product_Cat_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&TMonth=" + TMonth + "&TYear=" + TYear + "&Subdivision=" + subdivision + "&viewby=" + viewby + "";
                hyp_lst_month1.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";

                hyp_lst_month1.NavigateUrl = "#";

                TableCell tc_det_last6monthsum = new TableCell();
                tc_det_last6monthsum.Width = 200;

                tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Right;
                HyperLink lit_det_sum = new HyperLink();
                lit_det_sum.Text = drFF["value"] == DBNull.Value ? drFF["value"].ToString() : Convert.ToDecimal(drFF["value"]).ToString("0.00");
                string hh = lit_det_sum.Text;


                if (hh != "")
                {
                    ff = Convert.ToDecimal(hh);
                    value1 = ff;
                    catsumtotal += value1;
                    stCrtDtaPnt += Convert.ToString(value1) + "},";
                }
                else
                {
                    value1 = 0;
                    stCrtDtaPnt += "0},";
                }
                tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                tc_det_last6monthsum.BorderWidth = 1;
                tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                tc_det_last6monthsum.Controls.Add(lit_det_sum);
                tr_det.Cells.Add(tc_det_last6monthsum);


                sURL = "rpt_retailtrend_monthwise.aspx?Product_Cat_Code=" + drFF["Product_Cat_Code"] + "&Product_Cat_Name=" + drFF["Product_Cat_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&TMonth=" + TMonth + "&TYear=" + TYear + "&Subdivision=" + subdivision + "&viewby=" + viewby + "";
                lit_det_sum.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                lit_det_sum.NavigateUrl = "#";


                TableCell tc_det_currentmonth = new TableCell();
                tc_det_currentmonth.Width = 200;
                tc_det_currentmonth.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_det_mon = new Literal();
                //lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                tc_det_currentmonth.BorderStyle = BorderStyle.Solid;
                tc_det_currentmonth.BorderWidth = 1;
                tc_det_currentmonth.Attributes.Add("Class", "rptCellBorder");
                tc_det_currentmonth.Controls.Add(lit_det_mon);
                tr_det.Cells.Add(tc_det_currentmonth);

                dsDoc = sf.Retail_Trend_analysis_categorywise_total_value(divcode, fromdate, todate, subdivision);


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_value = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_value != "" && tot_value != "0")
                {
                    value2 = (Decimal.Parse(tot_value));
                    val = (Convert.ToDecimal(value1 / value2) * 100);
                    decimal value = Math.Round(val, 0);
                    lit_det_mon.Text = value.ToString("0.00");

                }
                else
                {
                    lit_det_mon.Text = "";
                }

                //val =tot_dr) / (tot_value);





                tot_dr = "0";
                tot_value = "0";
                value1 = 0;
                value2 = 0;
                val = 0;

                tbl.Rows.Add(tr_det);


            }

            TableRow tr_total = new TableRow();

            TableCell tc_Count_Total = new TableCell();
            tc_Count_Total.BorderStyle = BorderStyle.Solid;
            tc_Count_Total.BorderWidth = 1;
            //tc_catg_Total.Width = 25;
            Literal lit_Count_Total = new Literal();
            lit_Count_Total.Text = "Total";
            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_Count_Total.Controls.Add(lit_Count_Total);
            tc_Count_Total.Font.Bold.ToString();
            tc_Count_Total.BackColor = System.Drawing.Color.White;
            tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
            tc_Count_Total.ColumnSpan = 2;
            tc_Count_Total.Style.Add("text-align", "center");
            tc_Count_Total.Style.Add("font-family", "Calibri");
            tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
            tc_Count_Total.Style.Add("font-size", "10pt");

            tr_total.Cells.Add(tc_Count_Total);





            TableCell tc_tot_monthd = new TableCell();
            HyperLink hyp_monthd = new HyperLink();
            hyp_monthd.Text = netsumtotal.ToString("0.00");
            tc_tot_monthd.BorderStyle = BorderStyle.Solid;
            tc_tot_monthd.BorderWidth = 1;
            tc_tot_monthd.BackColor = System.Drawing.Color.White;
            tc_tot_monthd.Width = 200;
            tc_tot_monthd.Style.Add("font-family", "Calibri");
            tc_tot_monthd.Style.Add("font-size", "10pt");
            tc_tot_monthd.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_monthd.VerticalAlign = VerticalAlign.Middle;
            tc_tot_monthd.Controls.Add(hyp_monthd);
            tc_tot_monthd.Attributes.Add("style", "font-weight:bold;");
            tc_tot_monthd.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_monthd);


            TableCell tc_tot_month = new TableCell();
            Literal hyp_month = new Literal();
            hyp_month.Text = catsumtotal.ToString("0.00");

            tc_tot_month.BorderStyle = BorderStyle.Solid;
            tc_tot_month.BorderWidth = 1;
            tc_tot_month.BackColor = System.Drawing.Color.White;
            tc_tot_month.Width = 200;
            tc_tot_month.Style.Add("font-family", "Calibri");
            tc_tot_month.Style.Add("font-size", "10pt");
            tc_tot_month.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_month.VerticalAlign = VerticalAlign.Middle;
            tc_tot_month.Controls.Add(hyp_month);
            tc_tot_month.Attributes.Add("style", "font-weight:bold;");
            tc_tot_month.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_month);
            tbl.Rows.Add(tr_total);
            string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);





        }

    }
    private void FillBrandwise()
    {
        Random rndm = new Random();
        string stCrtDtaPnt = string.Empty;
        string t = "1";
        int mnth = Convert.ToInt32(FMonth), year = Convert.ToInt32(FYear);
        DateTime dt = Convert.ToDateTime(year + "-" + mnth.ToString() + "-" + t.ToString());
        string fromdate = dt.ToString("yyyy-MM-dd 00:00:00.000");
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd 23:59:59.999");
        string TYear = DateTime.Now.Year.ToString();
        string TMonth = DateTime.Now.Month.ToString();
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Retail_Trend_analysis_Brandwise(divcode, fromdate, todate, subdivision);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#496a9a");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            tc_SNo.Style.Add("text-align", "center");
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);



            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
            tc_DR_Name.RowSpan = 1;
            tc_DR_Name.Style.Add("text-align", "center");
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Brand Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_value1 = new TableCell();
            tc_DR_value1.BorderStyle = BorderStyle.Solid;
            tc_DR_value1.BorderWidth = 1;
            tc_DR_value1.Width = 150;
            tc_DR_value1.RowSpan = 1;
            tc_DR_value1.Style.Add("text-align", "center");
            Literal lit_DR_value1 = new Literal();
            lit_DR_value1.Text = "Net Weight Value";
            tc_DR_value1.BorderColor = System.Drawing.Color.Black;
            tc_DR_value1.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value1.Controls.Add(lit_DR_value1);
            tr_header.Cells.Add(tc_DR_value1);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 200;
            tc_DR_Code.RowSpan = 1;
            tc_DR_Code.Style.Add("text-align", "center");
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "Total";
            tc_DR_Code.Controls.Add(lit_DR_Code);

            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);
            TableCell tc_DR_Code_month = new TableCell();
            tc_DR_Code_month.BorderStyle = BorderStyle.Solid;
            tc_DR_Code_month.BorderWidth = 1;
            tc_DR_Code_month.Width = 200;
            tc_DR_Code_month.RowSpan = 1;
            tc_DR_Code_month.Style.Add("text-align", "center");
            Literal lit_DR_Code_mon = new Literal();
            lit_DR_Code_mon.Text = "% Distribution";
            tc_DR_Code_month.Controls.Add(lit_DR_Code_mon);
            tc_DR_Code_month.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code_month.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code_month);




            SalesForce sal = new SalesForce();




            tbl.Rows.Add(tr_header);



            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString();
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Style.Add("text-align", "right");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();

                lit_det_usr.Text = "&nbsp;" + drFF["Product_Brd_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Attributes.Add("style", "color:Blue;");
                tc_det_FF.Width = 300;
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp;" + drFF["Product_Brd_Name"].ToString();
                stCrtDtaPnt += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                TableCell tc_lst_month1 = new TableCell();
                HyperLink hyp_lst_month1 = new HyperLink();
                tc_lst_month1.Width = 150;
                if (drFF["net_weight_value"].ToString() != "")
                {
                    hyp_lst_month1.Text = drFF["net_weight_value"] == DBNull.Value ? drFF["net_weight_value"].ToString() : Convert.ToDecimal(drFF["net_weight_value"]).ToString("0.00");
                    string lk = hyp_lst_month1.Text;
                    decimal gg = Convert.ToDecimal(lk);
                    netsumtotal += gg;
                }
                else
                {
                    hyp_lst_month1.Text = "";

                }


                tc_lst_month1.BorderStyle = BorderStyle.Solid;

                tc_lst_month1.BorderStyle = BorderStyle.Solid;
                tc_lst_month1.BorderWidth = 1;
                tc_lst_month1.BackColor = System.Drawing.Color.White;
                tc_lst_month1.Width = 200;
                tc_lst_month1.HorizontalAlign = HorizontalAlign.Right;
                tc_lst_month1.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month1.Controls.Add(hyp_lst_month1);
                tc_lst_month1.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month1);

                sURL = "rpt_retailtrend_monthwise.aspx?Product_Brd_Code=" + drFF["Product_Brd_Code"] + "&Product_Brd_Name=" + drFF["Product_Brd_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&TMonth=" + TMonth + "&TYear=" + TYear + "&Subdivision=" + subdivision + "&viewby=" + viewby + "";
                hyp_lst_month1.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                hyp_lst_month1.NavigateUrl = "#";


                TableCell tc_det_last6monthsum = new TableCell();
                tc_det_last6monthsum.Width = 200;

                tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Right;
                HyperLink lit_det_sum = new HyperLink();
                lit_det_sum.Text = drFF["value"] == DBNull.Value ? drFF["value"].ToString() : Convert.ToDecimal(drFF["value"]).ToString("0.00");
                string hh = lit_det_sum.Text;


                if (hh != "")
                {
                    ff = Convert.ToDecimal(hh);
                    value1 = ff;
                    brandsumtotal += value1;
                    stCrtDtaPnt += Convert.ToString(value1) + "},";
                }
                else
                {
                    value1 = 0;
                    stCrtDtaPnt += "0},";
                }
                tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                tc_det_last6monthsum.BorderWidth = 1;
                tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                tc_det_last6monthsum.Controls.Add(lit_det_sum);
                tr_det.Cells.Add(tc_det_last6monthsum);


                sURL = "rpt_retailtrend_monthwise.aspx?Product_Brd_Code=" + drFF["Product_Brd_Code"] + "&Product_Brd_Name=" + drFF["Product_Brd_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&TMonth=" + TMonth + "&TYear=" + TYear + "&Subdivision=" + subdivision + "&viewby=" + viewby + "";
                lit_det_sum.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                lit_det_sum.NavigateUrl = "#";



                TableCell tc_det_currentmonth = new TableCell();
                tc_det_currentmonth.Width = 200;
                tc_det_currentmonth.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_det_mon = new Literal();
                //lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                tc_det_currentmonth.BorderStyle = BorderStyle.Solid;
                tc_det_currentmonth.BorderWidth = 1;
                tc_det_currentmonth.Attributes.Add("Class", "rptCellBorder");
                tc_det_currentmonth.Controls.Add(lit_det_mon);
                tr_det.Cells.Add(tc_det_currentmonth);

                dsDoc = sf.Retail_Trend_analysis_brandwise_total_value(divcode, fromdate, todate, subdivision);


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_value = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_value != "" && tot_value != "0")
                {
                    value2 = (Decimal.Parse(tot_value));
                    val = (Convert.ToDecimal(value1 / value2) * 100);
                    decimal value = Math.Round(val, 0);
                    lit_det_mon.Text = value.ToString("0.00");

                }
                else
                {
                    lit_det_mon.Text = "";
                }

                //val =tot_dr) / (tot_value);





                tot_dr = "0";
                tot_value = "0";
                value1 = 0;
                value2 = 0;
                val = 0;

                tbl.Rows.Add(tr_det);

            }
            TableRow tr_total = new TableRow();

            TableCell tc_Count_Total = new TableCell();
            tc_Count_Total.BorderStyle = BorderStyle.Solid;
            tc_Count_Total.BorderWidth = 1;
            //tc_catg_Total.Width = 25;
            Literal lit_Count_Total = new Literal();
            lit_Count_Total.Text = "Total";
            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_Count_Total.Controls.Add(lit_Count_Total);
            tc_Count_Total.Font.Bold.ToString();
            tc_Count_Total.BackColor = System.Drawing.Color.White;
            tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
            tc_Count_Total.ColumnSpan = 2;
            tc_Count_Total.Style.Add("text-align", "center");
            tc_Count_Total.Style.Add("font-family", "Calibri");
            tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
            tc_Count_Total.Style.Add("font-size", "10pt");

            tr_total.Cells.Add(tc_Count_Total);


            TableCell tc_tot_monthd = new TableCell();
            HyperLink hyp_monthd = new HyperLink();
            hyp_monthd.Text = netsumtotal.ToString("0.00");
            tc_tot_monthd.BorderStyle = BorderStyle.Solid;
            tc_tot_monthd.BorderWidth = 1;
            tc_tot_monthd.BackColor = System.Drawing.Color.White;
            tc_tot_monthd.Width = 200;
            tc_tot_monthd.Style.Add("font-family", "Calibri");
            tc_tot_monthd.Style.Add("font-size", "10pt");
            tc_tot_monthd.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_monthd.VerticalAlign = VerticalAlign.Middle;
            tc_tot_monthd.Controls.Add(hyp_monthd);
            tc_tot_monthd.Attributes.Add("style", "font-weight:bold;");
            tc_tot_monthd.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_monthd);



            TableCell tc_tot_month = new TableCell();
            Literal hyp_month = new Literal();
            hyp_month.Text = brandsumtotal.ToString("0.00");
            string total = "BrandTotal";
            tc_tot_month.BorderStyle = BorderStyle.Solid;
            tc_tot_month.BorderWidth = 1;
            tc_tot_month.BackColor = System.Drawing.Color.White;
            tc_tot_month.Width = 200;
            tc_tot_month.Style.Add("font-family", "Calibri");
            tc_tot_month.Style.Add("font-size", "10pt");
            tc_tot_month.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_month.VerticalAlign = VerticalAlign.Middle;
            tc_tot_month.Controls.Add(hyp_month);
            tc_tot_month.Attributes.Add("style", "font-weight:bold;");
            tc_tot_month.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_month);
            tbl.Rows.Add(tr_total);


            string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);

        }

    }
    private void FillProductwise()
    {
        Random rndm = new Random();
        string t = "1";
        string stCrtDtaPnt = string.Empty;
        int mnth = Convert.ToInt32(FMonth), year = Convert.ToInt32(FYear);
        DateTime dt = Convert.ToDateTime(year + "-" + mnth.ToString() + "-" + t.ToString());
        string fromdate = dt.ToString("yyyy-MM-dd 00:00:00.000");
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd 23:59:59.999");
        string TYear = DateTime.Now.Year.ToString();
        string TMonth = DateTime.Now.Month.ToString();
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Retail_Trend_analysis_Productwise_full_detail(subdivision, divcode, fromdate, todate);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#496a9a");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            tc_SNo.Style.Add("text-align", "center");
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);



            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
            tc_DR_Name.RowSpan = 1;
            tc_DR_Name.Style.Add("text-align", "center");
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Product Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);




            TableCell tc_DR_value1 = new TableCell();
            tc_DR_value1.BorderStyle = BorderStyle.Solid;
            tc_DR_value1.BorderWidth = 1;
            tc_DR_value1.Width = 150;
            tc_DR_value1.RowSpan = 1;
            tc_DR_value1.Style.Add("text-align", "center");
            Literal lit_DR_value1 = new Literal();
            lit_DR_value1.Text = "Net Weight Value";
            tc_DR_value1.BorderColor = System.Drawing.Color.Black;
            tc_DR_value1.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value1.Controls.Add(lit_DR_value1);
            tr_header.Cells.Add(tc_DR_value1);


            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 200;
            tc_DR_Code.RowSpan = 1;
            tc_DR_Code.Style.Add("text-align", "center");
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "Total";
            tc_DR_Code.Controls.Add(lit_DR_Code);

            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);
            TableCell tc_DR_Code_month = new TableCell();
            tc_DR_Code_month.BorderStyle = BorderStyle.Solid;
            tc_DR_Code_month.BorderWidth = 1;
            tc_DR_Code_month.Width = 200;
            tc_DR_Code_month.RowSpan = 1;
            tc_DR_Code_month.Style.Add("text-align", "center");
            Literal lit_DR_Code_mon = new Literal();
            lit_DR_Code_mon.Text = "% Distribution";
            tc_DR_Code_month.Controls.Add(lit_DR_Code_mon);
            tc_DR_Code_month.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code_month.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code_month);




            SalesForce sal = new SalesForce();




            tbl.Rows.Add(tr_header);



            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString();
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Style.Add("text-align", "right");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();

                lit_det_usr.Text = "&nbsp;" + drFF["Product_Detail_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Attributes.Add("style", "color:Blue;");
                tc_det_FF.Width = 300;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Product_Detail_Name"].ToString();
                stCrtDtaPnt += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
                sURL = "rpt_single_retail_product_monthwise.aspx?FYear=" + FYear + "&FMonth=" + FMonth + "&TMonth=" + TMonth + "&TYear=" + TYear + "&subdivision=" + subdivision + "&product_name=" + drFF["Product_Detail_Name"].ToString() + "&product_code=" + drFF["Product_Detail_Code"].ToString() + "";
                lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                lit_det_FF.NavigateUrl = "#";

                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);





                TableCell tc_lst_month1 = new TableCell();
                HyperLink hyp_lst_month1 = new HyperLink();
                tc_lst_month1.Width = 150;
                if (drFF["net_weight_value"].ToString() != "")
                {
                    hyp_lst_month1.Text = drFF["net_weight_value"] == DBNull.Value ? drFF["net_weight_value"].ToString() : Convert.ToDecimal(drFF["net_weight_value"]).ToString("0.00");
                    string lk = hyp_lst_month1.Text;
                    decimal gg = Convert.ToDecimal(lk);
                    netsumtotal += gg;
                }
                else
                {
                    hyp_lst_month1.Text = "";

                }


                tc_lst_month1.BorderStyle = BorderStyle.Solid;

                tc_lst_month1.BorderStyle = BorderStyle.Solid;
                tc_lst_month1.BorderWidth = 1;
                tc_lst_month1.BackColor = System.Drawing.Color.White;
                tc_lst_month1.Width = 200;
                tc_lst_month1.HorizontalAlign = HorizontalAlign.Right;
                tc_lst_month1.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month1.Controls.Add(hyp_lst_month1);
                tc_lst_month1.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month1);


                TableCell tc_det_last6monthsum = new TableCell();
                tc_det_last6monthsum.Width = 200;

                tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Right;
                HyperLink lit_det_sum = new HyperLink();
                lit_det_sum.Text = drFF["value"] == DBNull.Value ? drFF["value"].ToString() : Convert.ToDecimal(drFF["value"]).ToString("0.00");
                tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                tc_det_last6monthsum.BorderWidth = 1;
                tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                tc_det_last6monthsum.Controls.Add(lit_det_sum);
                tr_det.Cells.Add(tc_det_last6monthsum);

                dsDoc = sf.Retail_Trend_analysis_Productwise(drFF["Product_Detail_Code"].ToString(), divcode, fromdate, todate);


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_dr != "")
                {
                    value1 = (Decimal.Parse(tot_dr));
                    stCrtDtaPnt += Convert.ToString(value1) + "},";
                    prosumtotal += Convert.ToDecimal(value1);

                }
                else
                {
                    lit_det_sum.Text = "";
                    stCrtDtaPnt += "0},";
                }

                TableCell tc_det_currentmonth = new TableCell();
                tc_det_currentmonth.Width = 200;
                tc_det_currentmonth.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_det_mon = new Literal();
                //lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                tc_det_currentmonth.BorderStyle = BorderStyle.Solid;
                tc_det_currentmonth.BorderWidth = 1;
                tc_det_currentmonth.Attributes.Add("Class", "rptCellBorder");
                tc_det_currentmonth.Controls.Add(lit_det_mon);
                tr_det.Cells.Add(tc_det_currentmonth);

                dsDoc = sf.Retail_Trend_analysis_productwise_total_value(divcode, fromdate, todate, subdivision);


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_value = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_value != "")
                {
                    value2 = (Decimal.Parse(tot_value));
                    val = (Convert.ToDecimal(value1 / value2) * 100);
                    decimal value = Math.Round(val, 0);
                    lit_det_mon.Text = value.ToString("0.00");

                }
                else
                {
                    lit_det_mon.Text = "";
                }



                tot_dr = "0";
                tot_value = "0";
                value1 = 0;
                value2 = 0;
                val = 0;

                tbl.Rows.Add(tr_det);



            }

            TableRow tr_total = new TableRow();
            TableCell tc_Count_Total = new TableCell();
            tc_Count_Total.BorderStyle = BorderStyle.Solid;
            tc_Count_Total.BorderWidth = 1;
            //tc_catg_Total.Width = 25;
            Literal lit_Count_Total = new Literal();
            lit_Count_Total.Text = "Total";
            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_Count_Total.Controls.Add(lit_Count_Total);
            tc_Count_Total.Font.Bold.ToString();
            tc_Count_Total.BackColor = System.Drawing.Color.White;
            tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
            tc_Count_Total.ColumnSpan = 2;
            tc_Count_Total.Style.Add("text-align", "center");
            tc_Count_Total.Style.Add("font-family", "Calibri");
            tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
            tc_Count_Total.Style.Add("font-size", "10pt");

            tr_total.Cells.Add(tc_Count_Total);
            TableCell tc_tot_monthd = new TableCell();
            Literal hyp_monthd = new Literal();
            hyp_monthd.Text = netsumtotal.ToString("0.00");

            tc_tot_monthd.BorderStyle = BorderStyle.Solid;
            tc_tot_monthd.BorderWidth = 1;
            tc_tot_monthd.BackColor = System.Drawing.Color.White;
            tc_tot_monthd.Width = 200;
            tc_tot_monthd.Style.Add("font-family", "Calibri");
            tc_tot_monthd.Style.Add("font-size", "10pt");
            tc_tot_monthd.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_monthd.VerticalAlign = VerticalAlign.Middle;
            tc_tot_monthd.Controls.Add(hyp_monthd);
            tc_tot_monthd.Attributes.Add("style", "font-weight:bold;");
            tc_tot_monthd.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_monthd);
            tbl.Rows.Add(tr_total);

            TableCell tc_tot_month = new TableCell();
            Literal hyp_month = new Literal();
            hyp_month.Text = prosumtotal.ToString("0.00");

            tc_tot_month.BorderStyle = BorderStyle.Solid;
            tc_tot_month.BorderWidth = 1;
            tc_tot_month.BackColor = System.Drawing.Color.White;
            tc_tot_month.Width = 200;
            tc_tot_month.Style.Add("font-family", "Calibri");
            tc_tot_month.Style.Add("font-size", "10pt");
            tc_tot_month.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_month.VerticalAlign = VerticalAlign.Middle;
            tc_tot_month.Controls.Add(hyp_month);
            tc_tot_month.Attributes.Add("style", "font-weight:bold;");
            tc_tot_month.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_month);
            tbl.Rows.Add(tr_total);

            string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);




        }

    }



    private void FillChannelwise()
    {
        Random rndm = new Random();
        string t = "1";
        string stCrtDtaPnt = string.Empty;
        int mnth = Convert.ToInt32(FMonth), year = Convert.ToInt32(FYear);
        DateTime dt = Convert.ToDateTime(year + "-" + mnth.ToString() + "-" + t.ToString());
        string fromdate = dt.ToString("yyyy-MM-dd 00:00:00.000");
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd 23:59:59.999");
        string TYear = DateTime.Now.Year.ToString();
        string TMonth = DateTime.Now.Month.ToString();
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        prosumtotal = 0;
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Retail_Trend_analysis_Channelwise_full_detail(subdivision, divcode, fromdate, todate);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#496a9a");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            tc_SNo.Style.Add("text-align", "center");
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);



            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
            tc_DR_Name.RowSpan = 1;
            tc_DR_Name.Style.Add("text-align", "center");
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Channel Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);




            TableCell tc_DR_value1 = new TableCell();
            tc_DR_value1.BorderStyle = BorderStyle.Solid;
            tc_DR_value1.BorderWidth = 1;
            tc_DR_value1.Width = 150;
            tc_DR_value1.RowSpan = 1;
            tc_DR_value1.Style.Add("text-align", "center");
            Literal lit_DR_value1 = new Literal();
            lit_DR_value1.Text = "Net Weight Value";
            tc_DR_value1.BorderColor = System.Drawing.Color.Black;
            tc_DR_value1.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value1.Controls.Add(lit_DR_value1);
            tr_header.Cells.Add(tc_DR_value1);


            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 200;
            tc_DR_Code.RowSpan = 1;
            tc_DR_Code.Style.Add("text-align", "center");
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "Total";
            tc_DR_Code.Controls.Add(lit_DR_Code);

            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);
            TableCell tc_DR_Code_month = new TableCell();
            tc_DR_Code_month.BorderStyle = BorderStyle.Solid;
            tc_DR_Code_month.BorderWidth = 1;
            tc_DR_Code_month.Width = 200;
            tc_DR_Code_month.RowSpan = 1;
            tc_DR_Code_month.Style.Add("text-align", "center");
            Literal lit_DR_Code_mon = new Literal();
            lit_DR_Code_mon.Text = "% Distribution";
            tc_DR_Code_month.Controls.Add(lit_DR_Code_mon);
            tc_DR_Code_month.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code_month.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code_month);




            SalesForce sal = new SalesForce();




            tbl.Rows.Add(tr_header);



            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";


            decimal tot = 0m;
            foreach (DataRow row in dsSalesForce.Tables[0].Rows)
            {
                tot += row["value"] == DBNull.Value ? 0 : Convert.ToDecimal(row["value"]);
            }


            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString();
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Style.Add("text-align", "right");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();

                lit_det_usr.Text = "&nbsp;" + drFF["Doc_Special_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                //  tc_det_FF.Attributes.Add("style", "color:Blue;");
                tc_det_FF.Width = 300;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Doc_Special_Name"].ToString();
                stCrtDtaPnt += "{label:\"" + drFF["Doc_Special_Name"].ToString() + "\",y:";
                //  sURL = "rpt_single_retail_product_monthwise.aspx?FYear=" + FYear + "&FMonth=" + FMonth + "&TMonth=" + TMonth + "&TYear=" + TYear + "&subdivision=" + subdivision + "&product_name=" + drFF["Doc_Special_Name"].ToString() + "&product_code=" + drFF["Doc_Special_Code"].ToString() + "";
                //   lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                lit_det_FF.NavigateUrl = "#";

                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);





                TableCell tc_lst_month1 = new TableCell();
                HyperLink hyp_lst_month1 = new HyperLink();
                tc_lst_month1.Width = 150;
                if (drFF["net_weight_value"].ToString() != "")
                {
                    hyp_lst_month1.Text = drFF["net_weight_value"] == DBNull.Value ? drFF["net_weight_value"].ToString() : Convert.ToDecimal(drFF["net_weight_value"]).ToString("0.00");
                    string lk = hyp_lst_month1.Text;
                    decimal gg = Convert.ToDecimal(lk);
                    netsumtotal += gg;
                }
                else
                {
                    hyp_lst_month1.Text = "";

                }


                tc_lst_month1.BorderStyle = BorderStyle.Solid;

                tc_lst_month1.BorderStyle = BorderStyle.Solid;
                tc_lst_month1.BorderWidth = 1;
                tc_lst_month1.BackColor = System.Drawing.Color.White;
                tc_lst_month1.Width = 200;
                tc_lst_month1.HorizontalAlign = HorizontalAlign.Right;
                tc_lst_month1.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month1.Controls.Add(hyp_lst_month1);
                tc_lst_month1.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month1);


                TableCell tc_det_last6monthsum = new TableCell();
                tc_det_last6monthsum.Width = 200;

                tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Right;
                HyperLink lit_det_sum = new HyperLink();
                lit_det_sum.Text = drFF["value"] == DBNull.Value ? drFF["value"].ToString() : Convert.ToDecimal(drFF["value"]).ToString("0.00");


                sURL = "rpt_single_retail_channel.aspx?FYear=" + FYear + "&FMonth=" + FMonth + "&TMonth=" + TMonth + "&TYear=" + TYear + "&subdivision=" + subdivision + "&product_name=" + drFF["Doc_Special_Name"].ToString() + "&product_code=" + drFF["Doc_Special_Code"].ToString() + "";
                lit_det_sum.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                lit_det_sum.NavigateUrl = "#";


                tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                tc_det_last6monthsum.BorderWidth = 1;
                tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                tc_det_last6monthsum.Controls.Add(lit_det_sum);
                tr_det.Cells.Add(tc_det_last6monthsum);

                value1 = drFF["value"] == DBNull.Value ? 0 : Convert.ToDecimal(drFF["value"]);
                stCrtDtaPnt += Convert.ToString(value1) + "},";
                prosumtotal += Convert.ToDecimal(value1);

                TableCell tc_det_currentmonth = new TableCell();
                tc_det_currentmonth.Width = 200;
                tc_det_currentmonth.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_det_mon = new Literal();
                tc_det_currentmonth.BorderStyle = BorderStyle.Solid;
                tc_det_currentmonth.BorderWidth = 1;
                tc_det_currentmonth.Attributes.Add("Class", "rptCellBorder");
                tc_det_currentmonth.Controls.Add(lit_det_mon);
                tr_det.Cells.Add(tc_det_currentmonth);
                tot_value = tot.ToString("0.00");
                if (tot_value != "")
                {
                    value2 = (Decimal.Parse(tot_value));
                    if (value2 > 0)
                        val = (Convert.ToDecimal(value1 / value2) * 100);
                    //  decimal value = Math.Round(val, 0);
                    lit_det_mon.Text = val.ToString("0.00");
                }
                else
                {
                    lit_det_mon.Text = "";
                }



                tot_dr = "0";
                tot_value = "0";
                value1 = 0;
                value2 = 0;
                val = 0;

                tbl.Rows.Add(tr_det);



            }

            TableRow tr_total = new TableRow();
            TableCell tc_Count_Total = new TableCell();
            tc_Count_Total.BorderStyle = BorderStyle.Solid;
            tc_Count_Total.BorderWidth = 1;
            //tc_catg_Total.Width = 25;
            Literal lit_Count_Total = new Literal();
            lit_Count_Total.Text = "Total";
            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_Count_Total.Controls.Add(lit_Count_Total);
            tc_Count_Total.Font.Bold.ToString();
            tc_Count_Total.BackColor = System.Drawing.Color.White;
            tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
            tc_Count_Total.ColumnSpan = 2;
            tc_Count_Total.Style.Add("text-align", "center");
            tc_Count_Total.Style.Add("font-family", "Calibri");
            tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
            tc_Count_Total.Style.Add("font-size", "10pt");

            tr_total.Cells.Add(tc_Count_Total);
            TableCell tc_tot_monthd = new TableCell();
            Literal hyp_monthd = new Literal();
            hyp_monthd.Text = netsumtotal.ToString("0.00");

            tc_tot_monthd.BorderStyle = BorderStyle.Solid;
            tc_tot_monthd.BorderWidth = 1;
            tc_tot_monthd.BackColor = System.Drawing.Color.White;
            tc_tot_monthd.Width = 200;
            tc_tot_monthd.Style.Add("font-family", "Calibri");
            tc_tot_monthd.Style.Add("font-size", "10pt");
            tc_tot_monthd.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_monthd.VerticalAlign = VerticalAlign.Middle;
            tc_tot_monthd.Controls.Add(hyp_monthd);
            tc_tot_monthd.Attributes.Add("style", "font-weight:bold;");
            tc_tot_monthd.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_monthd);
            tbl.Rows.Add(tr_total);

            TableCell tc_tot_month = new TableCell();
            Literal hyp_month = new Literal();
            hyp_month.Text = prosumtotal.ToString("0.00");

            tc_tot_month.BorderStyle = BorderStyle.Solid;
            tc_tot_month.BorderWidth = 1;
            tc_tot_month.BackColor = System.Drawing.Color.White;
            tc_tot_month.Width = 200;
            tc_tot_month.Style.Add("font-family", "Calibri");
            tc_tot_month.Style.Add("font-size", "10pt");
            tc_tot_month.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_month.VerticalAlign = VerticalAlign.Middle;
            tc_tot_month.Controls.Add(hyp_month);
            tc_tot_month.Attributes.Add("style", "font-weight:bold;");
            tc_tot_month.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_month);
            tbl.Rows.Add(tr_total);


            TableCell tc_tot_per = new TableCell();
            Literal hyp_tot_per = new Literal();

            decimal totper = 0;
            if (prosumtotal > 0)
                totper = (tot / prosumtotal * 100);
            hyp_tot_per.Text = totper.ToString("0.00");

            tc_tot_per.BorderStyle = BorderStyle.Solid;
            tc_tot_per.BorderWidth = 1;
            tc_tot_per.BackColor = System.Drawing.Color.White;
            tc_tot_per.Width = 200;
            tc_tot_per.Style.Add("font-family", "Calibri");
            tc_tot_per.Style.Add("font-size", "10pt");
            tc_tot_per.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_per.VerticalAlign = VerticalAlign.Middle;
            tc_tot_per.Controls.Add(hyp_tot_per);
            tc_tot_per.Attributes.Add("style", "font-weight:bold;");
            tc_tot_per.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_per);
            tbl.Rows.Add(tr_total);



            string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);




        }





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
        string strFileName = Page.Title;
        string attachment = "attachment; filename='" + strFileName + "'.xls";
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
    protected void btnExport_Click(object sender, EventArgs e)
    {

        string strFileName = Page.Title;

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                //this.Page.RenderControl(hw);
                this.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename= '" + strFileName + "'.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
            }

        }
    }



    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}