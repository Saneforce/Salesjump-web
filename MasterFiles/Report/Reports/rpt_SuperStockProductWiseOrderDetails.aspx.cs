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
using DBase_EReport;


public partial class MasterFiles_Reports_rpt_SuperStockProductWiseOrderDetails : System.Web.UI.Page
{

    #region Declaration
    public static string divcode = string.Empty;
    public static string Stockist_Code = string.Empty;
    public static string Stockist_Name = string.Empty;
    public static string order_no = string.Empty;
    public static string FMonth = string.Empty;
    public static string FYear = string.Empty;
    public static string Activity_date = string.Empty;
    public static string Tdate = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string Sf_Name = string.Empty;
    public static string date = string.Empty;
    public static string hdate = string.Empty;
    public static string hfdate = string.Empty;
    public static string hTdate = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsDoc = new DataSet();
    DataSet dsDrr = new DataSet();
    Decimal iTotLstCount = 0;
    Decimal iTotLstCountt;
	   Decimal iTotLstCounttax;
    Decimal iTotLstfree = 0;
    Decimal iTotLstdiscount = 0;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();


        Sf_Name = Request.QueryString["Sf_Name"].ToString();
        Activity_date = Request.QueryString["Activity_date"].ToString();
        hfdate = Request.QueryString["Fromdate"].ToString();
        hTdate = Request.QueryString["Todate"].ToString();
        Tdate = Request.QueryString["Todate"].ToString().Trim();
        Sf_Code = Request.QueryString["Sf_Code"].ToString();
        date = Activity_date.Trim();
        Stockist_Code = Request.QueryString["Stockist_Code"].ToString();
        Stockist_Name = Request.QueryString["Stockist_Name"].ToString();
        DateTime dt = Convert.ToDateTime(hfdate);
        date = dt.ToString("yyyy-MM-dd");

        DateTime dt1 = Convert.ToDateTime(hTdate);
        hdate = dt1.ToString("yyyy-MM-dd");

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        //string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);
        //lblHead.Text = " SuperStock Orders Product Detail View for  " + hdate + "";
        lblHead1.Text = " SuperStock Orders Product Detail View for  " + date + " to " + hdate + "";


        lblMonth.Text = Sf_Name;
        lblYear.Text = Stockist_Name;
        FillSF();
    }

    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        DCR sf = new DCR();
        dsDrr = Gettransno(Sf_Code, Stockist_Code, divcode, Activity_date, hTdate);
        foreach (DataRow d in dsDrr.Tables[0].Rows)
        {
            order_no = d["Trans_Sl_No"].ToString();
            dsSalesForce = product_detaill(order_no, divcode);
            TableRow tr_headerr = new TableRow();

            tr_headerr.BackColor = System.Drawing.Color.FromName("#4aced6");
            tr_headerr.Style.Add("Color", "Black");
            tr_headerr.Style.Add("Font", "Bold");
            TableCell tc_SNod = new TableCell();
            tc_SNod.HorizontalAlign = HorizontalAlign.Right;

            tc_SNod.Width = 50;
            tc_SNod.RowSpan = 1;
            tc_SNod.ColumnSpan = 2;
            Literal lit_SNod =
                new Literal();
            lit_SNod.Text = "Order No -";

            tc_SNod.Controls.Add(lit_SNod);
            tc_SNod.Attributes.Add("Class", "rptCellBorder");
            tr_headerr.Cells.Add(tc_SNod);
            TableCell tc_SNodd = new TableCell();
            tc_SNodd.HorizontalAlign = HorizontalAlign.Left;

            tc_SNodd.Width = 50;
            tc_SNodd.RowSpan = 1;
            tc_SNodd.ColumnSpan = 7;
            Literal lit_SNodd =
                new Literal();
            lit_SNodd.Text = "ORD" + order_no;

            tc_SNodd.Controls.Add(lit_SNodd);
            tc_SNodd.Attributes.Add("Class", "rptCellBorder");
            tr_headerr.Cells.Add(tc_SNodd);

            tbl.Rows.Add(tr_headerr);
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
                Literal lit_SNo = new Literal();
                lit_SNo.Text = "S.No";
                tc_SNo.BorderColor = System.Drawing.Color.Black;
                tc_SNo.Controls.Add(lit_SNo);
                tc_SNo.Attributes.Add("Class", "rptCellBorder");
                tr_header.Cells.Add(tc_SNo);

                TableCell tc_DR_Code = new TableCell();
                tc_DR_Code.BorderStyle = BorderStyle.Solid;
                tc_DR_Code.BorderWidth = 1;
                tc_DR_Code.Width = 400;
                tc_DR_Code.RowSpan = 1;
                Literal lit_DR_Code = new Literal();
                lit_DR_Code.Text = "<center>Product Name</center>";
                tc_DR_Code.Controls.Add(lit_DR_Code);
                tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
                tc_DR_Code.BorderColor = System.Drawing.Color.Black;
                tr_header.Cells.Add(tc_DR_Code);

                TableCell tc_DR_Name = new TableCell();
                tc_DR_Name.BorderStyle = BorderStyle.Solid;
                tc_DR_Name.BorderWidth = 1;
                tc_DR_Name.Width = 250;
                tc_DR_Name.RowSpan = 1;
                Literal lit_DR_Name = new Literal();
                lit_DR_Name.Text = "<center>Quantity</center>";
                tc_DR_Name.BorderColor = System.Drawing.Color.Black;
                tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
                tc_DR_Name.Controls.Add(lit_DR_Name);
                tr_header.Cells.Add(tc_DR_Name);

                TableCell tc_det_head_Quamd = new TableCell();
                tc_det_head_Quamd.Width = 70;
                tc_det_head_Quamd.BorderStyle = BorderStyle.Solid;
                tc_det_head_Quamd.BorderWidth = 1;
                Literal lit_det_head_quamd = new Literal();
                lit_det_head_quamd.Text = "<b>Mfg.date</b>";
                tc_det_head_Quamd.Attributes.Add("Class", "tblHead");
                tc_det_head_Quamd.Controls.Add(lit_det_head_quamd);
                tc_det_head_Quamd.HorizontalAlign = HorizontalAlign.Center;
                tr_header.Cells.Add(tc_det_head_Quamd);
                tc_det_head_Quamd.Visible = false;

                TableCell tc_det_head_Rate = new TableCell();
                tc_det_head_Rate.Width = 70;
                tc_det_head_Rate.BorderStyle = BorderStyle.Solid;
                tc_det_head_Rate.BorderWidth = 1;
                Literal lit_det_head_rate = new Literal();
                lit_det_head_rate.Text = "<b>Rate</b>";
                tc_det_head_Rate.Attributes.Add("Class", "tblHead");
                tc_det_head_Rate.Controls.Add(lit_det_head_rate);
                tc_det_head_Rate.HorizontalAlign = HorizontalAlign.Center;
                tr_header.Cells.Add(tc_det_head_Rate);

                TableCell tc_det_head_free = new TableCell();
                tc_det_head_free.Width = 70;
                tc_det_head_free.BorderStyle = BorderStyle.Solid;
                tc_det_head_free.BorderWidth = 1;
                Literal lit_det_head_free = new Literal();
                lit_det_head_free.Text = "<b>Free</b>";
                tc_det_head_free.Attributes.Add("Class", "tblHead");
                tc_det_head_free.Controls.Add(lit_det_head_free);
                tc_det_head_free.HorizontalAlign = HorizontalAlign.Center;
                tr_header.Cells.Add(tc_det_head_free);

               

                TableCell tc_det_head_dis = new TableCell();
                tc_det_head_dis.Width = 70;
                tc_det_head_dis.BorderStyle = BorderStyle.Solid;
                tc_det_head_dis.BorderWidth = 1;
                Literal lit_det_head_dis = new Literal();
                lit_det_head_dis.Text = "<b>Discount</b>";
                tc_det_head_dis.Attributes.Add("Class", "tblHead");
                tc_det_head_dis.Controls.Add(lit_det_head_dis);
                tc_det_head_dis.HorizontalAlign = HorizontalAlign.Center;
                tr_header.Cells.Add(tc_det_head_dis);

                TableCell tc_det_head_tax = new TableCell();
                tc_det_head_tax.Width = 70;
                tc_det_head_tax.BorderStyle = BorderStyle.Solid;
                tc_det_head_tax.BorderWidth = 1;
                Literal lit_det_head_tax = new Literal();
                lit_det_head_tax.Text = "<b>Tax</b>";
                tc_det_head_tax.Attributes.Add("Class", "tblHead");
                tc_det_head_tax.Controls.Add(lit_det_head_tax);
                tc_det_head_tax.HorizontalAlign = HorizontalAlign.Center;
                tr_header.Cells.Add(tc_det_head_tax); 

                TableCell tc_det_head_Qua = new TableCell();
                tc_det_head_Qua.Width = 70;
                tc_det_head_Qua.BorderStyle = BorderStyle.Solid;
                tc_det_head_Qua.BorderWidth = 1;
                Literal lit_det_head_qua = new Literal();
                lit_det_head_qua.Text = "<b>Value</b>";
                tc_det_head_Qua.Attributes.Add("Class", "tblHead");
                tc_det_head_Qua.Controls.Add(lit_det_head_qua);
                tc_det_head_Qua.HorizontalAlign = HorizontalAlign.Center;
                tr_header.Cells.Add(tc_det_head_Qua);

                TableCell tc_det_head_min1 = new TableCell();
                tc_det_head_min1.BorderStyle = BorderStyle.Solid;
                tc_det_head_min1.BorderWidth = 1;
                Literal lit_det_head_min1 = new Literal();
                lit_det_head_min1.Text = "<b>Unit_Of_Mass</b>";
                tc_det_head_min1.Attributes.Add("Class", "tblHead");
                tc_det_head_min1.Controls.Add(lit_det_head_min1);
                tc_det_head_min1.HorizontalAlign = HorizontalAlign.Center;
                tr_header.Cells.Add(tc_det_head_min1);

                tbl.Rows.Add(tr_header);
                //Sub Header

                if (dsSalesForce.Tables[0].Rows.Count > 0)
                    ViewState["dsSalesForce"] = dsSalesForce;

                int iCount = 0;
                //string iTotLstCount ="0";
                dsSalesForce = (DataSet)ViewState["dsSalesForce"];

                foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                {
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
                    Literal lit_det_usr = new Literal();
                    lit_det_usr.Text = "&nbsp;" + drFF["Product_Name"].ToString();
                    tc_det_usr.BorderStyle = BorderStyle.Solid;
                    tc_det_usr.BorderWidth = 1;
                    tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                    tc_det_usr.Controls.Add(lit_det_usr);
                    tr_det.Cells.Add(tc_det_usr);

                    TableCell tc_det_usrr = new TableCell();
                    tc_det_usrr.Visible = false;
                    Literal lit_det_usrr = new Literal();
                    lit_det_usrr.Text = "&nbsp;" + drFF["Product_Code"].ToString();
                    tc_det_usrr.BorderStyle = BorderStyle.Solid;
                    tc_det_usrr.BorderWidth = 1;
                    tc_det_usrr.Attributes.Add("Class", "rptCellBorder");
                    tc_det_usrr.Controls.Add(lit_det_usrr);
                    tr_det.Cells.Add(tc_det_usrr);


                    //SF Name
                    TableCell tc_det_FF = new TableCell();
                    tc_det_FF.Width = 200;
                    Literal lit_det_FF = new Literal();
                    lit_det_FF.Text = drFF["Quantity"].ToString();

                    tc_det_FF.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                    tc_det_FF.Controls.Add(lit_det_FF);
                    tr_det.Cells.Add(tc_det_FF);

                    TableCell tc_det_dr_rate = new TableCell();
                    tc_det_dr_rate.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_dr_rate = new Literal();
                    lit_det_dr_rate.Text = drFF["Rate"].ToString();
                    tc_det_dr_rate.Attributes.Add("Class", "tblRow");
                    tc_det_dr_rate.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_rate.BorderWidth = 1;
                    tc_det_dr_rate.Controls.Add(lit_det_dr_rate);
                    tr_det.Cells.Add(tc_det_dr_rate);

                    TableCell tc_det_dr_free = new TableCell();
                    tc_det_dr_free.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_dr_free = new Literal();
                    lit_det_dr_free.Text = drFF["free"].ToString();
                    iTotLstfree += Decimal.Parse(drFF["free"].ToString());
                    tc_det_dr_free.Attributes.Add("Class", "tblRow");
                    tc_det_dr_free.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_free.BorderWidth = 1;
                    tc_det_dr_free.Controls.Add(lit_det_dr_free);
                    tr_det.Cells.Add(tc_det_dr_free);

                    TableCell tc_det_dr_dis = new TableCell();
                    tc_det_dr_dis.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_dr_dis = new Literal();
                    lit_det_dr_dis.Text = drFF["discount_price"].ToString();
                    iTotLstdiscount += Decimal.Parse(drFF["discount_price"].ToString());
                    tc_det_dr_dis.Attributes.Add("Class", "tblRow");
                    tc_det_dr_dis.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_dis.BorderWidth = 1;
                    tc_det_dr_dis.Controls.Add(lit_det_dr_dis);
                    tr_det.Cells.Add(tc_det_dr_dis);

                    string taxValue = drFF["tax"].ToString();
                    TableCell tctax = new TableCell();
                    tctax.HorizontalAlign = HorizontalAlign.Center;
                    Literal littax = new Literal();
                    littax.Text = drFF["Tax"].ToString();
                    iTotLstCounttax += Decimal.Parse(drFF["Tax"].ToString());
                    tctax.Attributes.Add("Class", "tblRow");
                    tctax.BorderStyle = BorderStyle.Solid;
                    tctax.BorderWidth = 1;
                    tctax.Controls.Add(littax);
                    tr_det.Cells.Add(tctax);

                    TableCell tc_det_dr_hq = new TableCell();
                    tc_det_dr_hq.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_dr_hq = new Literal();
                    lit_det_dr_hq.Text = drFF["VALUE"].ToString();
                    iTotLstCount += Decimal.Parse(drFF["VALUE"].ToString());
                    tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                    tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_hq.BorderWidth = 1;
                    tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                    tr_det.Cells.Add(tc_det_dr_hq);

                    TableCell tc_det_dr_min = new TableCell();
                    Literal lit_det_dr_min = new Literal();
                    lit_det_dr_min.Text = drFF["netpack"].ToString();
                    //iTotLstCountt += Decimal.Parse(drFF["netpack"].ToString());
                    tc_det_dr_min.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_dr_min.Attributes.Add("Class", "tblRow");
                    tc_det_dr_min.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_min.BorderWidth = 1;
                    tc_det_dr_min.Controls.Add(lit_det_dr_min);
                    tr_det.Cells.Add(tc_det_dr_min);

                    tbl.Rows.Add(tr_det);
                }

                TableRow tr_total = new TableRow();

                TableCell tc_Count_Total = new TableCell();
                tc_Count_Total.BorderStyle = BorderStyle.Solid;
                tc_Count_Total.BorderWidth = 1;

                Literal lit_Count_Total = new Literal();
                lit_Count_Total.Text = "<center>Total</center>";
                tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
                tc_Count_Total.Controls.Add(lit_Count_Total);
                tc_Count_Total.Font.Bold.ToString();
                tc_Count_Total.BackColor = System.Drawing.Color.White;
                tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
                tc_Count_Total.ColumnSpan = 4;
                tc_Count_Total.Style.Add("text-align", "left");
                tc_Count_Total.Style.Add("font-family", "Calibri");
                tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
                tc_Count_Total.Style.Add("font-size", "10pt");

                tr_total.Cells.Add(tc_Count_Total);

                TableCell tc_tot_free = new TableCell();
                HyperLink hyp_free = new HyperLink();

                hyp_free.Text = iTotLstfree.ToString();

                tc_tot_free.BorderStyle = BorderStyle.Solid;
                tc_tot_free.BorderWidth = 1;
                tc_tot_free.BackColor = System.Drawing.Color.White;
                tc_tot_free.Width = 200;
                tc_tot_free.Style.Add("font-family", "Calibri");
                tc_tot_free.Style.Add("font-size", "10pt");
                tc_tot_free.HorizontalAlign = HorizontalAlign.Center;
                tc_tot_free.VerticalAlign = VerticalAlign.Middle;
                tc_tot_free.Controls.Add(hyp_free);
                tc_tot_free.Attributes.Add("style", "font-weight:bold;");
                tc_tot_free.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_tot_free);
                iTotLstfree = 0;

                TableCell tc_tot_discount = new TableCell();
                HyperLink hyp_discount = new HyperLink();

                hyp_discount.Text = iTotLstdiscount.ToString();

                tc_tot_discount.BorderStyle = BorderStyle.Solid;
                tc_tot_discount.BorderWidth = 1;
                tc_tot_discount.BackColor = System.Drawing.Color.White;
                tc_tot_discount.Width = 200;
                tc_tot_discount.Style.Add("font-family", "Calibri");
                tc_tot_discount.Style.Add("font-size", "10pt");
                tc_tot_discount.HorizontalAlign = HorizontalAlign.Center;
                tc_tot_discount.VerticalAlign = VerticalAlign.Middle;
                tc_tot_discount.Controls.Add(hyp_discount);
                tc_tot_discount.Attributes.Add("style", "font-weight:bold;");
                tc_tot_discount.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_tot_discount);

                iTotLstdiscount = 0;

                TableCell tc_tot_tax = new TableCell();
                HyperLink hyp_tax = new HyperLink();

                hyp_tax.Text = iTotLstCounttax.ToString();

                tc_tot_tax.BorderStyle = BorderStyle.Solid;
                tc_tot_tax.BorderWidth = 1;
                tc_tot_tax.BackColor = System.Drawing.Color.White;
                tc_tot_tax.Width = 200;
                tc_tot_tax.Style.Add("font-family", "Calibri");
                tc_tot_tax.Style.Add("font-size", "10pt");
                tc_tot_tax.HorizontalAlign = HorizontalAlign.Center;
                tc_tot_tax.VerticalAlign = VerticalAlign.Middle;
                tc_tot_tax.Controls.Add(hyp_tax);
                tc_tot_tax.Attributes.Add("style", "font-weight:bold;");
                tc_tot_tax.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_tot_tax);

                iTotLstCounttax = 0;

                TableCell tc_tot_month = new TableCell();
                HyperLink hyp_month = new HyperLink();

                hyp_month.Text = iTotLstCount.ToString();

                tc_tot_month.BorderStyle = BorderStyle.Solid;
                tc_tot_month.BorderWidth = 1;
                tc_tot_month.BackColor = System.Drawing.Color.White;
                tc_tot_month.Width = 200;
                tc_tot_month.Style.Add("font-family", "Calibri");
                tc_tot_month.Style.Add("font-size", "10pt");
                tc_tot_month.HorizontalAlign = HorizontalAlign.Center;
                tc_tot_month.VerticalAlign = VerticalAlign.Middle;
                tc_tot_month.Controls.Add(hyp_month);
                tc_tot_month.Attributes.Add("style", "font-weight:bold;");
                tc_tot_month.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_tot_month);

                iTotLstCount = 0;

                tbl.Rows.Add(tr_total);

            }
        }
    }


    public DataSet Gettransno(string sfcode, string Stockist_Code, string DivCode, string activitydate, string todate)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsSF = null;
        string strQry = "SELECT Trans_Sl_No from Trans_SPriOrder_Head  where Sf_code='" + sfcode + "' and Stockist_Code='" + Stockist_Code + "'";
        strQry += " AND Division_Code = " + DivCode + " ";
        strQry += " AND CONVERT(date,Order_Date) BETWEEN '" + activitydate + "' AND '" + todate + "' ";
        strQry += "    ORDER BY Trans_Sl_No ASC   ";        
        try
        {
            dsSF = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }

    public DataSet product_detaill(string Trans_Sl_no, string DivCode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = " SELECT Product_Code,Product_Name ,SUM(Quantity)Quantity,SUM(Rate)Rate,SUM(free) free,SUM(discount_price)discount_price, ";
        strQry += " SUM([value])[value1],netpack,SUM(Order_Value) [value] ,sum(tax) Tax ";
        strQry += "  FROM (  SELECT d.Product_Code,d.Product_Name, ISNULL(d.CQty,0) Quantity,";        
        strQry += "  isnull((d.[value]),0)Order_Value,(h.Order_Value)[value], ";
        strQry += "  (d.free)free,(d.discount_price)discount_price,CAST((d.Rate) as float) Rate,  ";
        strQry += " (CAST(d.Qty AS VARCHAR(10)) + ' ' + d.product_Unit_Name)netpack,isnull(d.tax,0) tax    FROM Trans_SPriOrder_Head h WITH(NOLOCK)    ";
        strQry += " INNER  JOIN Trans_SPriOrder_Details d WITH(NOLOCK) ON h.Trans_Sl_No = d.Trans_Sl_No     ";
        strQry += " INNER JOIN Mas_Product_Detail pd WITH(NOLOCK) ON d.Product_Code = pd.Product_Detail_Code     ";
        strQry += "  WHERE h.Trans_Sl_no = '" + Trans_Sl_no + "'  AND h.Division_Code = " + DivCode + " ";
        strQry += "  ) as temptbl ";
        strQry += "  GROUP BY  Product_Code,Product_Name,netpack ";

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

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {

    }
}