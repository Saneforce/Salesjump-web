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
using System.Configuration;
using System.Data.SqlClient;
using iTextSharp.tool.xml;


public partial class MIS_Reports_rptSales_Top10_Exception : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sf_type = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iTotLstCount1 = 0;
    int iTotLstCount = 0;
    int iTotLstCount2 = 0;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string viewby = string.Empty;
    string viewtop = string.Empty;   
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;   
    string tot_Sub_days = string.Empty;
    string tot_dr = string.Empty;    
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string tot_doc_Unlstcalls_seen = string.Empty;
    string tot_CSH_calls_seen = string.Empty;
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;
    string MultiSf_Code = string.Empty;
    DataSet dsprd = new DataSet();   
    string sCurrentDate = string.Empty;
    decimal value1 = 0;
    decimal value2 = 0;
    decimal ff = 0;
    string tot_value = string.Empty;
    decimal val = 0;
    DataSet dsState = null;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string[] statecd;
    DataSet dsDivision = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        FYear = Request.QueryString["FYear"].ToString();
        viewby = Request.QueryString["viewby"].ToString();
        viewtop = Request.QueryString["viewtop"].ToString();


        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();       



        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
        if (viewby == "DistributorWise")
        {
            lblHead.Text = "Top  " + viewtop + "  Exception  Distributor Wise for   " + FYear;
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
                FillSF_MR();
            }
        }
        else if (viewby == "CategoryWise")
        {
            lblHead.Text = "Top  " + viewtop + "  Exception  Category Wise for   " + FYear;
            FillCategorywise();
        }
        else if (viewby == "BrandWise")
        {
            lblHead.Text = "Top  " + viewtop + "  Exception  Brand Wise for   " + FYear;
            FillBrandwise();

        }
        else if (viewby == "ProductWise")
        {
            lblHead.Text = "Top  " + viewtop + "  Exception  Product Wise for   " + FYear;
            FillProductwise();

        }
        else if (viewby == "StateWise")
        {
            lblHead.Text = "Top  " + viewtop + "  Exception  StateWise for   " + FYear;
            FillSalesForce();

        }
        else if (viewby == "AreaWise")
        {
            lblHead.Text = "Top  " + viewtop + "  Exception  AreaWise for   " + FYear;
            FillSalesForce();

        }
        else if (viewby == "ZoneWise")
        {
            lblHead.Text = "Top  " + viewtop + "  Exception  ZoneWise for   " + FYear;
            FillSalesForce();

        }
        else if (viewby == "TerritoryWise")
        {
            lblHead.Text = "Top  " + viewtop + "  Exception  TerritoryWise for   " + FYear;
            FillSalesForce();

        }

    }

    private void FillSF_MR()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();

        if (sf_type == "1")
        {
            dsSalesForce = sf.Sales_Gettop10value_stockist_MR(divcode, FYear, sfCode);
        }
        else 
        {
            dsSalesForce = sf.Sales_Gettop10value_stockist_MGR(divcode, FYear, sfCode);
        }


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 80;

tc_SNo.Style.Add("text-align","center");
            tc_SNo.RowSpan = 1;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 150;
            tc_DR_Code.RowSpan = 1;
tc_DR_Code.Style.Add("text-align","center");
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "Distributor Code";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 150;
tc_DR_Name.Style.Add("text-align","center");
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Distributor Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_value = new TableCell();
            tc_DR_value.BorderStyle = BorderStyle.Solid;
            tc_DR_value.BorderWidth = 1;
            tc_DR_value.Width = 150;
            tc_DR_value.RowSpan = 1;
tc_DR_value.Style.Add("text-align","center");
            Literal lit_DR_value = new Literal();
            lit_DR_value.Text = "Value";
            tc_DR_value.BorderColor = System.Drawing.Color.Black;
            tc_DR_value.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value.Controls.Add(lit_DR_value);
            tr_header.Cells.Add(tc_DR_value);

            tbl.Controls.Add(tr_header);



            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;




            SalesForce sal = new SalesForce();


            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            StringBuilder output = new StringBuilder();
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                //if (Convert.ToInt32(drFF[7]).Value) < Convert.ToInt32(row.Cells[10].Value))
                //{
                //    drFF.DefaultCellStyle.BackColor = Color.Red;
                //}



                output.AppendLine();
                Response.Write(output);
                TableRow tr_det = new TableRow();
                iCount += 1;

                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text =  iCount.ToString() ;
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
tc_det_SNo.Style.Add("text-align","Right");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                tc_det_usr.Width = 150;
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
                tc_det_FF.Width = 150;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                //sURL = "rptPurchas_Register_Category_wise.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&sCurrentDate=" + sCurrentDate + "";
                //lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                //lit_det_FF.NavigateUrl = "#";            



                TableCell tc_lst_month = new TableCell();
                Literal hyp_lst_month = new Literal();

                tc_lst_month.Width = 150;

               // Decimal per1 = Math.Round(Convert.ToDecimal(drFF["VALUE"].ToString()), 2);
               // hyp_lst_month.Text = per1.ToString("0.00");

				try
                {
                     Decimal per1 = Math.Round(Convert.ToDecimal(drFF["VALUE"].ToString()), 2);
                   hyp_lst_month.Text = per1.ToString("0.00");
                }
                catch
                {
                    hyp_lst_month.Text =drFF["VALUE"] ==DBNull.Value? drFF["VALUE"].ToString():Convert.ToDecimal(drFF["VALUE"]).ToString("0.00");
                }




                //hyp_lst_month.Text = "&nbsp;" + drFF["value"].ToString();
                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;

                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 200;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Right;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);


                if (iCount <= int.Parse(viewtop))
                {


                 tc_det_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_FF.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_usr.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                }
                tbl.Controls.Add(tr_det);


            }


        }

    }

    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop10value_stockist(divcode, FYear);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 80;
            tc_SNo.RowSpan = 1;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
tc_SNo.Style.Add("text-align","center");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 150;
            tc_DR_Code.RowSpan = 1;
tc_DR_Code.Style.Add("text-align","center");
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "Distributor Code";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 150;
            tc_DR_Name.RowSpan = 1;
tc_DR_Name.Style.Add("text-align","center");
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Distributor Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_value = new TableCell();
            tc_DR_value.BorderStyle = BorderStyle.Solid;
            tc_DR_value.BorderWidth = 1;
            tc_DR_value.Width = 150;
            tc_DR_value.RowSpan = 1;
tc_DR_value.Style.Add("text-align","center");
            Literal lit_DR_value = new Literal();
            lit_DR_value.Text = "Value";
            tc_DR_value.BorderColor = System.Drawing.Color.Black;
            tc_DR_value.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value.Controls.Add(lit_DR_value);
            tr_header.Cells.Add(tc_DR_value);



            TableCell contribution = new TableCell();
            contribution.BorderStyle = BorderStyle.Solid;
            contribution.BorderWidth = 1;
            contribution.Width = 150;
            contribution.RowSpan = 1;
contribution.Style.Add("text-align","center");
            Literal contribution_lit = new Literal();
            contribution_lit.Text = "%Contibution";
            contribution.BorderColor = System.Drawing.Color.Black;
            contribution.Attributes.Add("Class", "rptCellBorder");
            contribution.Controls.Add(contribution_lit);
            tr_header.Cells.Add(contribution);
            tbl.Controls.Add(tr_header);



            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;




            SalesForce sal = new SalesForce();


            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            StringBuilder output = new StringBuilder();
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                //if (Convert.ToInt32(drFF[7]).Value) < Convert.ToInt32(row.Cells[10].Value))
                //{
                //    drFF.DefaultCellStyle.BackColor = Color.Red;
                //}



                output.AppendLine();
                Response.Write(output);
                TableRow tr_det = new TableRow();
                iCount += 1;

                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text =  iCount.ToString();
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
tc_det_SNo.Style.Add("text-align","right");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                tc_det_usr.Width = 150;
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
                tc_det_FF.Width = 150;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                //sURL = "rptPurchas_Register_Category_wise.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&sCurrentDate=" + sCurrentDate + "";
                //lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                //lit_det_FF.NavigateUrl = "#";            







                TableCell tc_lst_month = new TableCell();
                Literal hyp_lst_month = new Literal();

                tc_lst_month.Width = 150;
                try
                {
                    Decimal per = Math.Round(Convert.ToDecimal(drFF["value"].ToString()), 2);
                    hyp_lst_month.Text = per.ToString("0.00");
                }
                catch
                {
                    hyp_lst_month.Text = drFF["value"] ==DBNull.Value ? drFF["value"].ToString():Convert.ToDecimal(drFF["value"]).ToString("0.00");
                }
                string hh = hyp_lst_month.Text;


                if (hh != "")
                {
                    ff = Convert.ToDecimal(hh);
                    value1 = ff;


                }
                else
                {
                    value1 = 0;

                }
                //hyp_lst_month.Text = "&nbsp;" + drFF["value"].ToString();
                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;

                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 200;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Right;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);


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

                dsDoc = sf.Sales_Gettop10value_category_total(divcode, FYear);


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_value = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_value != "" && tot_value != "0")
                {
                    value2 = (Decimal.Parse(tot_value));
                    val = (Convert.ToDecimal(value1 / value2) * 100);
                    decimal value = Math.Round(val, 2);
                    lit_det_mon.Text = value.ToString();

                }
                else
                {
                    lit_det_mon.Text = "";
                }


                if (iCount <= int.Parse(viewtop))
                {


                    tc_det_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_FF.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_usr.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
					tc_det_currentmonth.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                   
                }
                tbl.Controls.Add(tr_det);

            }


        }










    }
    private void FillCategorywise()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
     //   dsSalesForce = sf.Sales_Gettop10value_category(divcode, FYear);
 dsSalesForce = sf.Sales_Gettop10value_category(divcode, FYear,sfCode);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
tc_SNo.Style.Add("text-align","center");
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 150;
            tc_DR_Code.RowSpan = 1;
tc_DR_Code.Style.Add("text-align","center");
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "Category Code";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 150;
            tc_DR_Name.RowSpan = 1;
tc_DR_Name.Style.Add("text-align","center");
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Category Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_value = new TableCell();
            tc_DR_value.BorderStyle = BorderStyle.Solid;
            tc_DR_value.BorderWidth = 1;
            tc_DR_value.Width = 150;
            tc_DR_value.RowSpan = 1;
tc_DR_value.Style.Add("text-align","center");
            Literal lit_DR_value = new Literal();
            lit_DR_value.Text = "Value";
            tc_DR_value.BorderColor = System.Drawing.Color.Black;
            tc_DR_value.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value.Controls.Add(lit_DR_value);
            tr_header.Cells.Add(tc_DR_value);



            TableCell contribution = new TableCell();
            contribution.BorderStyle = BorderStyle.Solid;
            contribution.BorderWidth = 1;
            contribution.Width = 150;
            contribution.RowSpan = 1;
contribution.Style.Add("text-align","center");
            Literal contribution_lit = new Literal();
            contribution_lit.Text = "%Contibution";
            contribution.BorderColor = System.Drawing.Color.Black;
            contribution.Attributes.Add("Class", "rptCellBorder");
            contribution.Controls.Add(contribution_lit);
            tr_header.Cells.Add(contribution);
            tbl.Controls.Add(tr_header);



            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;




            SalesForce sal = new SalesForce();


            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            StringBuilder output = new StringBuilder();

    decimal tot = 0m;
            foreach (DataRow dr1 in dsSalesForce.Tables[0].Rows)
            {
                tot += dr1["value"] == DBNull.Value ? 0 : Convert.ToDecimal(dr1["value"].ToString());
            }

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {


                output.AppendLine();
                Response.Write(output);
                TableRow tr_det = new TableRow();
                iCount += 1;

                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text =  iCount.ToString() ;
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
tc_det_SNo.Style.Add("text-align","right");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                tc_det_usr.Width = 150;
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
                tc_det_FF.Width = 150;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Product_Cat_Name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                //sURL = "rptPurchas_Register_Category_wise.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&sCurrentDate=" + sCurrentDate + "";
                //lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                //lit_det_FF.NavigateUrl = "#";



                TableCell tc_lst_month = new TableCell();
                Literal hyp_lst_month = new Literal();
                try
                {
                    Decimal per = Math.Round(Convert.ToDecimal(drFF["value"].ToString()), 2);
                    hyp_lst_month.Text = per.ToString("0.00");
                }
                catch
                {
                    hyp_lst_month.Text =drFF["value"]==DBNull.Value ? drFF["value"].ToString(): Convert.ToDecimal(drFF["value"]).ToString("0.00");
                }
                string hh = hyp_lst_month.Text;


                if (hh != "")
                {
                    ff = Convert.ToDecimal(hh);
                    value1 = ff;


                }
                else
                {
                    value1 = 0;

                }
                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;

                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 150;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Right;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);

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

              //  dsDoc = sf.Sales_Gettop10value_category_total(divcode, FYear);


           //     if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_value = tot.ToString();//dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_value != "" && tot_value != "0")
                {
                    value2 = (Decimal.Parse(tot_value));
                    val = (Convert.ToDecimal(value1 / value2) * 100);
                    decimal value = Math.Round(val, 2);
                    lit_det_mon.Text = value.ToString("0.00");

                }
                else
                {
                    lit_det_mon.Text = "";
                }


                if (iCount <= int.Parse(viewtop))
                {


                   tc_det_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_FF.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_usr.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
tc_det_currentmonth.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                }
                tbl.Controls.Add(tr_det);




            }


        }










    }
    private void FillBrandwise()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
//        dsSalesForce = sf.Sales_Gettop10value_Brand(divcode, FYear);
 dsSalesForce = sf.Sales_Gettop10value_Brand(divcode, FYear,sfCode);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
tc_SNo.Style.Add("text-align","center");
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 150;
            tc_DR_Code.RowSpan = 1;
tc_DR_Code.Style.Add("text-align","center");
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "Brand Code";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 150;
            tc_DR_Name.RowSpan = 1;
tc_DR_Name.Style.Add("text-align","center");
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Brand Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_value = new TableCell();
            tc_DR_value.BorderStyle = BorderStyle.Solid;
            tc_DR_value.BorderWidth = 1;
            tc_DR_value.Width = 150;
            tc_DR_value.RowSpan = 1;
tc_DR_value.Style.Add("text-align","center");
            Literal lit_DR_value = new Literal();
            lit_DR_value.Text = "Value";
            tc_DR_value.BorderColor = System.Drawing.Color.Black;
            tc_DR_value.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value.Controls.Add(lit_DR_value);
            tr_header.Cells.Add(tc_DR_value);


TableCell contribution = new TableCell();
            contribution.BorderStyle = BorderStyle.Solid;
            contribution.BorderWidth = 1;
            contribution.Width = 150;
            contribution.RowSpan = 1;
contribution.Style.Add("text-align","center");
            Literal contribution_lit = new Literal();
            contribution_lit.Text = "%Contibution";
            contribution.BorderColor = System.Drawing.Color.Black;
            contribution.Attributes.Add("Class", "rptCellBorder");
            contribution.Controls.Add(contribution_lit);
            tr_header.Cells.Add(contribution);

            tbl.Controls.Add(tr_header);



            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;




            SalesForce sal = new SalesForce();


            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            StringBuilder output = new StringBuilder();

    decimal tot = 0m;
            foreach (DataRow dr1 in dsSalesForce.Tables[0].Rows)
            {
                tot += dr1["value"] == DBNull.Value ? 0 : Convert.ToDecimal(dr1["value"].ToString());
            }

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {



                output.AppendLine();
                Response.Write(output);
                TableRow tr_det = new TableRow();
                iCount += 1;

                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text =  iCount.ToString() ;
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
tc_det_SNo.Style.Add("text-align","center");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                tc_det_usr.Width = 150;
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
                tc_det_FF.Width = 150;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Product_Brd_Name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                //sURL = "rptPurchas_Register_Category_wise.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&sCurrentDate=" + sCurrentDate + "";
                //lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                //lit_det_FF.NavigateUrl = "#";





                TableCell tc_lst_month = new TableCell();
                Literal hyp_lst_month = new Literal();
                try
                {
                    Decimal per = Math.Round(Convert.ToDecimal(drFF["value"].ToString()), 2);
                    hyp_lst_month.Text = per.ToString("0.00");
                }
                catch
                {
                    hyp_lst_month.Text = drFF["value"]==DBNull.Value ? drFF["value"].ToString():Convert.ToDecimal( drFF["value"]).ToString("0.00");
                }
                string hh = hyp_lst_month.Text;


                if (hh != "")
                {
                    ff = Convert.ToDecimal(hh);
                    value1 = ff;


                }
                else
                {
                    value1 = 0;

                }
                //hyp_lst_month.Text = "&nbsp;" + drFF["value"].ToString();
                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;

                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 200;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Right;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);



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

             //   dsDoc = sf.Sales_Gettop10value_Brand_total(divcode, FYear);


              //  if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_value =tot.ToString(); //dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_value != "" && tot_value != "0")
                {
                    value2 = (Decimal.Parse(tot_value));
                    val = (Convert.ToDecimal(value1 / value2) * 100);
                    decimal value = Math.Round(val, 2);
                    lit_det_mon.Text = value.ToString("0.00");

                }
                else
                {
                    lit_det_mon.Text = "0";
                }


                if (iCount <= int.Parse(viewtop))
                {


               tc_det_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_FF.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_usr.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
tc_det_currentmonth.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                }
                tbl.Controls.Add(tr_det);




            }


        }










    }

    private void FillProductwise()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
//        dsSalesForce = sf.Sales_Gettop10value_Product(divcode, FYear);
 dsSalesForce = sf.Sales_Gettop10value_Product(divcode, FYear,sfCode);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
tc_SNo.Style.Add("text-align","center");
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 150;
            tc_DR_Code.RowSpan = 1;
tc_DR_Code.Style.Add("text-align","center");
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "Product Code";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 150;
            tc_DR_Name.RowSpan = 1;
tc_DR_Name.Style.Add("text-align","center");
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Product Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_value = new TableCell();
            tc_DR_value.BorderStyle = BorderStyle.Solid;
            tc_DR_value.BorderWidth = 1;
            tc_DR_value.Width = 150;
            tc_DR_value.RowSpan = 1;
tc_DR_value.Style.Add("text-align","center");
            Literal lit_DR_value = new Literal();
            lit_DR_value.Text = "Value";
            tc_DR_value.BorderColor = System.Drawing.Color.Black;
            tc_DR_value.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value.Controls.Add(lit_DR_value);
            tr_header.Cells.Add(tc_DR_value);

			TableCell contribution = new TableCell();
            contribution.BorderStyle = BorderStyle.Solid;
            contribution.BorderWidth = 1;
            contribution.Width = 150;
            contribution.RowSpan = 1;
contribution.Style.Add("text-align","center");
            Literal contribution_lit = new Literal();
            contribution_lit.Text = "%Contibution";
            contribution.BorderColor = System.Drawing.Color.Black;
            contribution.Attributes.Add("Class", "rptCellBorder");
            contribution.Controls.Add(contribution_lit);
            tr_header.Cells.Add(contribution);

            tbl.Controls.Add(tr_header);



            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;




            SalesForce sal = new SalesForce();


            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            StringBuilder output = new StringBuilder();
    decimal tot = 0m;
            foreach (DataRow dr1 in dsSalesForce.Tables[0].Rows)
            {
                tot += dr1["value"] == DBNull.Value ? 0 : Convert.ToDecimal(dr1["value"].ToString());
            }

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {


                output.AppendLine();
                Response.Write(output);
                TableRow tr_det = new TableRow();
                iCount += 1;

                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text =  iCount.ToString() ;
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
tc_det_SNo.Style.Add("text-align","right");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                tc_det_usr.Width = 150;
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
                tc_det_FF.Width = 150;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Product_Detail_Name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                //sURL = "rptPurchas_Register_Category_wise.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&sCurrentDate=" + sCurrentDate + "";
                //lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                //lit_det_FF.NavigateUrl = "#";



                TableCell tc_lst_month = new TableCell();
                Literal hyp_lst_month = new Literal();


                try
                {
                    Decimal per = Math.Round(Convert.ToDecimal(drFF["value"].ToString()), 2);
                    hyp_lst_month.Text = per.ToString("0.00");
                }
                catch
                {
                    hyp_lst_month.Text = drFF["value"]==DBNull.Value ? drFF["value"].ToString(): Convert.ToDecimal(drFF["value"]).ToString("0.00");
                }
                string hh = hyp_lst_month.Text;


                if (hh != "")
                {
                    ff = Convert.ToDecimal(hh);
                    value1 = ff;


                }
                else
                {
                    value1 = 0;

                }
                
                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;

                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 150;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Right;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);

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

             //   dsDoc = sf.Sales_Gettop10value_Product_total(divcode, FYear);


             //   if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_value =tot.ToString(); //dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_value != "" && tot_value != "0")
                {
                    value2 = (Decimal.Parse(tot_value));
                    val = (Convert.ToDecimal(value1 / value2) * 100);
                    decimal value = Math.Round(val, 2);
                    lit_det_mon.Text = value.ToString("0.00");

                }
                else
                {
                    lit_det_mon.Text = "";
                }


                if (iCount <= int.Parse(viewtop))
                {


              tc_det_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_FF.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_usr.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
tc_det_currentmonth.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                }
                tbl.Controls.Add(tr_det);




            }


        }










    }

    private void FillSalesForce()
    {

        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(divcode);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsSalesForce = st.getState_new(state_cd);

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {

                ViewState["dsSalesForce"] = dsSalesForce;
            }

        }
        if (viewby == "StateWise")
        {
            Fillstatewise();
        }
        else if (viewby == "AreaWise")
        {
            Fillareawise();
        }
        else if (viewby == "ZoneWise")
        {
            Fillzonewise();
        }
        else if (viewby == "TerritoryWise")
        {
            Fillterritorywise();
        }

    }

    private void Fillstatewise()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        Sales sf = new Sales();
  //      dsSalesForce = sf.Sales_Gettopexception_statewise(divcode, FYear, state_cd);
   dsSalesForce = sf.Sales_Gettopexception_statewise(divcode, FYear, state_cd,sfCode);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
tc_SNo.Style.Add("text-align","center");
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
            tc_DR_Name.Width = 150;
            tc_DR_Name.RowSpan = 1;
tc_DR_Name.Style.Add("text-align","center");
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "State Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_value = new TableCell();
            tc_DR_value.BorderStyle = BorderStyle.Solid;
            tc_DR_value.BorderWidth = 1;
            tc_DR_value.Width = 150;
            tc_DR_value.RowSpan = 1;
tc_DR_value.Style.Add("text-align","center");
            Literal lit_DR_value = new Literal();
            lit_DR_value.Text = "Value";
            tc_DR_value.BorderColor = System.Drawing.Color.Black;
            tc_DR_value.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value.Controls.Add(lit_DR_value);
            tr_header.Cells.Add(tc_DR_value);


            TableCell contribution = new TableCell();
            contribution.BorderStyle = BorderStyle.Solid;
            contribution.BorderWidth = 1;
            contribution.Width = 150;
            contribution.RowSpan = 1;
contribution.Style.Add("text-align","center");
            Literal contribution_lit = new Literal();
            contribution_lit.Text = "%Contibution";
            contribution.BorderColor = System.Drawing.Color.Black;
            contribution.Attributes.Add("Class", "rptCellBorder");
            contribution.Controls.Add(contribution_lit);
            tr_header.Cells.Add(contribution);


            tbl.Controls.Add(tr_header);



            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;




            SalesForce sal = new SalesForce();


            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            StringBuilder output = new StringBuilder();

    decimal tot = 0m;
            foreach (DataRow dr1 in dsSalesForce.Tables[0].Rows)
            {
                tot += dr1["value"] == DBNull.Value ? 0 : Convert.ToDecimal(dr1["value"].ToString());
            }

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {


                output.AppendLine();
                Response.Write(output);
                TableRow tr_det = new TableRow();
                iCount += 1;

                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString() ;
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
tc_det_SNo.Style.Add("text-align","right");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                tc_det_usr.Width = 150;
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["state_code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 150;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["statename"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                //sURL = "rptPurchas_Register_Category_wise.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&sCurrentDate=" + sCurrentDate + "";
                //lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                //lit_det_FF.NavigateUrl = "#";



                TableCell tc_lst_month = new TableCell();
                Literal hyp_lst_month = new Literal();


                try
                {
                    Decimal per = Math.Round(Convert.ToDecimal(drFF["value"].ToString()), 2);
                    hyp_lst_month.Text = per.ToString("0.00");
                }
                catch
                {
                    hyp_lst_month.Text =  drFF["value"]==DBNull.Value ? drFF["value"].ToString():Convert.ToDecimal( drFF["value"]).ToString("0.00");
                }
                string hh = hyp_lst_month.Text;


                if (hh != "")
                {
                    ff = Convert.ToDecimal(hh);
                    value1 = ff;


                }
                else
                {
                    value1 = 0;

                }

                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;

                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 150;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Right;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);


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

                Sales HH = new Sales();
              //  dsDoc = HH.Sales_Gettopexception_statewise_total(divcode, FYear, state_cd);


              //  if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_value = tot.ToString();//dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_value != "" && tot_value != "0")
                {
                    value2 = (Decimal.Parse(tot_value));
                    val = (Convert.ToDecimal(value1 / value2) * 100);
                    decimal value = Math.Round(val, 2);
                    lit_det_mon.Text = value.ToString("0.00");

                }
                else
                {
                    lit_det_mon.Text = "";
                }

                if (iCount <= int.Parse(viewtop))
                {

tc_det_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_FF.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_usr.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
tc_det_currentmonth.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                }
                tbl.Controls.Add(tr_det);




            }


        }










    }
    private void Fillareawise()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        Sales sf = new Sales();
      //  dsSalesForce = sf.Sales_Gettopexception_areawise(divcode, FYear, state_cd);
      dsSalesForce = sf.Sales_Gettopexception_areawise(divcode, FYear, state_cd,sfCode);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 80;
            tc_SNo.RowSpan = 1;
tc_SNo.Style.Add("text-align","center");
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
            tc_DR_Name.Width = 150;
            tc_DR_Name.RowSpan = 1;
tc_DR_Name.Style.Add("text-align","center");
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Area Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_value = new TableCell();
            tc_DR_value.BorderStyle = BorderStyle.Solid;
            tc_DR_value.BorderWidth = 1;
            tc_DR_value.Width = 150;
            tc_DR_value.RowSpan = 1;
tc_DR_value.Style.Add("text-align","center");
            Literal lit_DR_value = new Literal();
            lit_DR_value.Text = "Value";
            tc_DR_value.BorderColor = System.Drawing.Color.Black;
            tc_DR_value.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value.Controls.Add(lit_DR_value);
            tr_header.Cells.Add(tc_DR_value);


            TableCell contribution = new TableCell();
            contribution.BorderStyle = BorderStyle.Solid;
            contribution.BorderWidth = 1;
            contribution.Width = 150;
            contribution.RowSpan = 1;
contribution.Style.Add("text-align","center");
            Literal contribution_lit = new Literal();
            contribution_lit.Text = "%Contibution";
            contribution.BorderColor = System.Drawing.Color.Black;
            contribution.Attributes.Add("Class", "rptCellBorder");
            contribution.Controls.Add(contribution_lit);
            tr_header.Cells.Add(contribution);


            tbl.Controls.Add(tr_header);



            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;




            SalesForce sal = new SalesForce();


            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            StringBuilder output = new StringBuilder();
    decimal tot = 0m;
            foreach (DataRow dr1 in dsSalesForce.Tables[0].Rows)
            {
                tot += dr1["value"] == DBNull.Value ? 0 : Convert.ToDecimal(dr1["value"].ToString());
            }

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {


                output.AppendLine();
                Response.Write(output);
                TableRow tr_det = new TableRow();
                iCount += 1;

                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text =  iCount.ToString() ;
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
tc_det_SNo.Style.Add("text-align","right");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                tc_det_usr.Width = 150;
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Area_code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 150;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Area_name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                //sURL = "rptPurchas_Register_Category_wise.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&sCurrentDate=" + sCurrentDate + "";
                //lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                //lit_det_FF.NavigateUrl = "#";



                TableCell tc_lst_month = new TableCell();
                Literal hyp_lst_month = new Literal();


                try
                {
                    Decimal per = Math.Round(Convert.ToDecimal(drFF["value"].ToString()), 2);
                    hyp_lst_month.Text = per.ToString("0.00");
                }
                catch
                {
                    hyp_lst_month.Text =drFF["value"] ==DBNull.Value? drFF["value"].ToString():Convert.ToDecimal(drFF["value"]).ToString("0.00");
                }
                string hh = hyp_lst_month.Text;


                if (hh != "")
                {
                    ff = Convert.ToDecimal(hh);
                    value1 = ff;


                }
                else
                {
                    value1 = 0;

                }

                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;

                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 150;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Right;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);


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

                Sales HH = new Sales();
              //  dsDoc = HH.Sales_Gettopexception_areawise_total(divcode, FYear, state_cd);


              //  if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_value =tot.ToString(); //dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_value != "" && tot_value != "0")
                {
                    value2 = (Decimal.Parse(tot_value));
                    val = (Convert.ToDecimal(value1 / value2) * 100);
                    decimal value = Math.Round(val, 2);
                    lit_det_mon.Text = value.ToString("0.00");

                }
                else
                {
                    lit_det_mon.Text = "";
                }

                if (iCount <= int.Parse(viewtop))
                {


                  tc_det_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_FF.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_usr.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
tc_det_currentmonth.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                }
                tbl.Controls.Add(tr_det);




            }


        }


    }
    private void Fillzonewise()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        Sales sf = new Sales();
   //     dsSalesForce = sf.Sales_Gettopexception_zonewise(divcode, FYear, state_cd);
  dsSalesForce = sf.Sales_Gettopexception_zonewise(divcode, FYear, state_cd,sfCode);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
tc_SNo.Style.Add("text-align","center");
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
            tc_DR_Name.Width = 150;
tc_DR_Name.Style.Add("text-align","center");
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Zone Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_value = new TableCell();
            tc_DR_value.BorderStyle = BorderStyle.Solid;
            tc_DR_value.BorderWidth = 1;
            tc_DR_value.Width = 150;
tc_DR_value.Style.Add("text-align","center");
            tc_DR_value.RowSpan = 1;
            Literal lit_DR_value = new Literal();
            lit_DR_value.Text = "Value";
            tc_DR_value.BorderColor = System.Drawing.Color.Black;
            tc_DR_value.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value.Controls.Add(lit_DR_value);
            tr_header.Cells.Add(tc_DR_value);


            TableCell contribution = new TableCell();
            contribution.BorderStyle = BorderStyle.Solid;
            contribution.BorderWidth = 1;
            contribution.Width = 150;
            contribution.RowSpan = 1;
contribution.Style.Add("text-align","center");
            Literal contribution_lit = new Literal();
            contribution_lit.Text = "%Contibution";
            contribution.BorderColor = System.Drawing.Color.Black;
            contribution.Attributes.Add("Class", "rptCellBorder");
            contribution.Controls.Add(contribution_lit);
            tr_header.Cells.Add(contribution);


            tbl.Controls.Add(tr_header);



            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;




            SalesForce sal = new SalesForce();


            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            StringBuilder output = new StringBuilder();
    decimal tot = 0m;
            foreach (DataRow dr1 in dsSalesForce.Tables[0].Rows)
            {
                tot += dr1["value"] == DBNull.Value ? 0 : Convert.ToDecimal(dr1["value"].ToString());
            }

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {


                output.AppendLine();
                Response.Write(output);
                TableRow tr_det = new TableRow();
                iCount += 1;

                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text =  iCount.ToString() ;
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
tc_det_SNo.Style.Add("text-align","right");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                tc_det_usr.Width = 150;
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Zone_code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 150;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Zone_name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                //sURL = "rptPurchas_Register_Category_wise.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&sCurrentDate=" + sCurrentDate + "";
                //lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                //lit_det_FF.NavigateUrl = "#";



                TableCell tc_lst_month = new TableCell();
                Literal hyp_lst_month = new Literal();


                try
                {
                    Decimal per = Math.Round(Convert.ToDecimal(drFF["value"].ToString()), 2);
                    hyp_lst_month.Text = per.ToString("0.00");
                }
                catch
                {
                    hyp_lst_month.Text = drFF["value"]==DBNull.Value? drFF["value"].ToString():Convert.ToDecimal(drFF["value"]).ToString("0.00");
                }
                string hh = hyp_lst_month.Text;


                if (hh != "")
                {
                    ff = Convert.ToDecimal(hh);
                    value1 = ff;


                }
                else
                {
                    value1 = 0;

                }

                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;

                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 150;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Right;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);


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

                Sales HH = new Sales();
             //   dsDoc = HH.Sales_Gettopexception_zonewise_total(divcode, FYear, state_cd);


              //  if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_value =tot.ToString(); //dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_value != "" && tot_value != "0")
                {
                    value2 = (Decimal.Parse(tot_value));
                    val = (Convert.ToDecimal(value1 / value2) * 100);
                    decimal value = Math.Round(val, 2);
                    lit_det_mon.Text = value.ToString("0.00");

                }
                else
                {
                    lit_det_mon.Text = "";
                }

                if (iCount <= int.Parse(viewtop))
                {


                   tc_det_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_FF.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_usr.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
tc_det_currentmonth.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                }
                tbl.Controls.Add(tr_det);




            }


        }

    }
    private void Fillterritorywise()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        Sales sf = new Sales();
    //    dsSalesForce = sf.Sales_Gettopexception_territorywise(divcode, FYear, state_cd);
  dsSalesForce = sf.Sales_Gettopexception_territorywise(divcode, FYear, state_cd,sfCode);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
tc_SNo.Style.Add("text-align","center");
            tc_SNo.RowSpan = 1;
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
            tc_DR_Name.Width = 150;
            tc_DR_Name.RowSpan = 1;
tc_DR_Name.Style.Add("text-align","center");
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Territory Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_value = new TableCell();
            tc_DR_value.BorderStyle = BorderStyle.Solid;
            tc_DR_value.BorderWidth = 1;
            tc_DR_value.Width = 150;
            tc_DR_value.RowSpan = 1;
tc_DR_value.Style.Add("text-align","center");
            Literal lit_DR_value = new Literal();
            lit_DR_value.Text = "Value";
            tc_DR_value.BorderColor = System.Drawing.Color.Black;
            tc_DR_value.Attributes.Add("Class", "rptCellBorder");
            tc_DR_value.Controls.Add(lit_DR_value);
            tr_header.Cells.Add(tc_DR_value);


            TableCell contribution = new TableCell();
            contribution.BorderStyle = BorderStyle.Solid;
            contribution.BorderWidth = 1;
            contribution.Width = 150;
            contribution.RowSpan = 1;
contribution.Style.Add("text-align","center");
            Literal contribution_lit = new Literal();
            contribution_lit.Text = "%Contibution";
            contribution.BorderColor = System.Drawing.Color.Black;
            contribution.Attributes.Add("Class", "rptCellBorder");
            contribution.Controls.Add(contribution_lit);
            tr_header.Cells.Add(contribution);


            tbl.Controls.Add(tr_header);



            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;




            SalesForce sal = new SalesForce();


            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            StringBuilder output = new StringBuilder();

    decimal tot = 0m;
            foreach (DataRow dr1 in dsSalesForce.Tables[0].Rows)
            {
                tot += dr1["value"] == DBNull.Value ? 0 : Convert.ToDecimal(dr1["value"].ToString());
            }

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {


                output.AppendLine();
                Response.Write(output);
                TableRow tr_det = new TableRow();
                iCount += 1;

                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text =  iCount.ToString() ;
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
tc_det_SNo.Style.Add("text-align","right");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                tc_det_usr.Width = 150;
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Territory_code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 150;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Territory_Name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                //sURL = "rptPurchas_Register_Category_wise.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + "&sCurrentDate=" + sCurrentDate + "";
                //lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                //lit_det_FF.NavigateUrl = "#";



                TableCell tc_lst_month = new TableCell();
                Literal hyp_lst_month = new Literal();


                try
                {
                    Decimal per = Math.Round(Convert.ToDecimal(drFF["value"].ToString()), 2);
                    hyp_lst_month.Text = per.ToString("0.00");
                }
                catch
                {
                    hyp_lst_month.Text = drFF["value"]==DBNull.Value? drFF["value"].ToString(): Convert.ToDecimal( drFF["value"]).ToString("0.00");
                }
                string hh = hyp_lst_month.Text;


                if (hh != "")
                {
                    ff = Convert.ToDecimal(hh);
                    value1 = ff;


                }
                else
                {
                    value1 = 0;

                }

                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;

                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 150;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Right;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);


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

                Sales HH = new Sales();
             //   dsDoc = HH.Sales_Gettopexception_territorywise_total(divcode, FYear, state_cd);


              //  if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_value =tot.ToString(); //dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_value != "" && tot_value != "0")
                {
                    value2 = (Decimal.Parse(tot_value));
                    val = (Convert.ToDecimal(value1 / value2) * 100);
                    decimal value = Math.Round(val, 2);
                    lit_det_mon.Text = value.ToString("0.00");

                }
                else
                {
                    lit_det_mon.Text = "";
                }

                if (iCount <= int.Parse(viewtop))
                {
                   	tc_det_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_FF.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                    tc_det_usr.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
					tc_det_currentmonth.BackColor = System.Drawing.ColorTranslator.FromHtml("#cffabd");
                }
                tbl.Controls.Add(tr_det);
            }
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