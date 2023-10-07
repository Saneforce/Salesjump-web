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


public partial class MIS_Reports_rptSales_Top10_Exception : System.Web.UI.Page
{
    string sfCode = string.Empty;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();


        FYear = Request.QueryString["FYear"].ToString();
        viewby = Request.QueryString["viewby"].ToString();
        viewtop = Request.QueryString["viewtop"].ToString();


        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();       



        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
        if (viewby == "DistributorWise")
        {
            lblHead.Text = "Top  " + viewtop + "  Exception  Distributor Wise for   " + FYear;
            FillSF();
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
            tc_SNo.Width = 50;
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
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>Distributor Code</center>";
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
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Distributor Name</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_value = new TableCell();
            tc_DR_value.BorderStyle = BorderStyle.Solid;
            tc_DR_value.BorderWidth = 1;
            tc_DR_value.Width = 150;
            tc_DR_value.RowSpan = 1;
            Literal lit_DR_value = new Literal();
            lit_DR_value.Text = "<center>Value</center>";
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
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
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
                hyp_lst_month.Text = "&nbsp;" + drFF["value"].ToString();
                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;

                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 200;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);


                if (iCount <= int.Parse(viewtop))
                {


                    tc_det_SNo.BackColor = System.Drawing.Color.Beige;
                    tc_lst_month.BackColor = System.Drawing.Color.Beige;
                    tc_det_FF.BackColor = System.Drawing.Color.Beige;
                    tc_det_usr.BackColor = System.Drawing.Color.Beige;
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
        dsSalesForce = sf.Sales_Gettop10value_category(divcode, FYear);


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
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>Category Code</center>";
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
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Category Name</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_value = new TableCell();
            tc_DR_value.BorderStyle = BorderStyle.Solid;
            tc_DR_value.BorderWidth = 1;
            tc_DR_value.Width = 150;
            tc_DR_value.RowSpan = 1;
            Literal lit_DR_value = new Literal();
            lit_DR_value.Text = "<center>Value</center>";
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


                output.AppendLine();
                Response.Write(output);
                TableRow tr_det = new TableRow();
                iCount += 1;

                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
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


                hyp_lst_month.Text = "&nbsp;" + drFF["value"].ToString();
                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;

                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 150;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);


                if (iCount <= int.Parse(viewtop))
                {


                    tc_det_SNo.BackColor = System.Drawing.Color.Beige;
                    tc_lst_month.BackColor = System.Drawing.Color.Beige;
                    tc_det_FF.BackColor = System.Drawing.Color.Beige;
                    tc_det_usr.BackColor = System.Drawing.Color.Beige;
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
        dsSalesForce = sf.Sales_Gettop10value_Brand(divcode, FYear);


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
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>Brand Code</center>";
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
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Brand Name</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_value = new TableCell();
            tc_DR_value.BorderStyle = BorderStyle.Solid;
            tc_DR_value.BorderWidth = 1;
            tc_DR_value.Width = 150;
            tc_DR_value.RowSpan = 1;
            Literal lit_DR_value = new Literal();
            lit_DR_value.Text = "<center>Value</center>";
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



                output.AppendLine();
                Response.Write(output);
                TableRow tr_det = new TableRow();
                iCount += 1;

                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
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


                hyp_lst_month.Text = "&nbsp;" + drFF["value"].ToString();
                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;

                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 200;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);


                if (iCount <= int.Parse(viewtop))
                {


                    tc_det_SNo.BackColor = System.Drawing.Color.Beige;
                    tc_lst_month.BackColor = System.Drawing.Color.Beige;
                    tc_det_FF.BackColor = System.Drawing.Color.Beige;
                    tc_det_usr.BackColor = System.Drawing.Color.Beige;
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
        dsSalesForce = sf.Sales_Gettop10value_Product(divcode, FYear);


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
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>Product Code</center>";
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
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Product Name</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_value = new TableCell();
            tc_DR_value.BorderStyle = BorderStyle.Solid;
            tc_DR_value.BorderWidth = 1;
            tc_DR_value.Width = 150;
            tc_DR_value.RowSpan = 1;
            Literal lit_DR_value = new Literal();
            lit_DR_value.Text = "<center>Value</center>";
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


                output.AppendLine();
                Response.Write(output);
                TableRow tr_det = new TableRow();
                iCount += 1;

                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
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


                hyp_lst_month.Text = "&nbsp;" + drFF["value"].ToString();
                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;

                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 150;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);


                if (iCount <= int.Parse(viewtop))
                {


                    tc_det_SNo.BackColor = System.Drawing.Color.Beige;
                    tc_lst_month.BackColor = System.Drawing.Color.Beige;
                    tc_det_FF.BackColor = System.Drawing.Color.Beige;
                    tc_det_usr.BackColor = System.Drawing.Color.Beige;
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
        string attachment = "attachment; filename=DCRView.xls";
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("viewreort.aspx");
    }
}