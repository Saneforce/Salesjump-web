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

public partial class MIS_Reports_rpt_secondary_sale_view_weekday : System.Web.UI.Page
{
    DataSet dsDr = null;
    DataSet dsDrr = null;
    DataSet getweek = null;
    DataSet getweekno = null;
    DataSet getweekdays = null;
    DataSet gg = null;
    
    string sCmd = string.Empty;
    string div_code = string.Empty;
    string distributor = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
    string stockist = string.Empty;
    string Year = string.Empty;
    string Month = string.Empty;
    string Stock_name = string.Empty;
    string sf_name = string.Empty;
    string type = string.Empty;
    string sCurrentDate = string.Empty;
    string Sf_Code_multiple = string.Empty;
    string MultiProd_Code = string.Empty;
    string tot_dr = string.Empty;
    string strFMonthName = string.Empty;
    string weeknumber = string.Empty;
    string dist_name = string.Empty;
    string subdivision = string.Empty;
    int weekno=0;
         int iCount = -1;
    DataSet dsDoc = null;
    string g = string.Empty;
   
    int FYear = 0;
    int FMonth = 0;
    int gw = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        Month = Request.QueryString["FMonth"].ToString();
        Year = Request.QueryString["FYear"].ToString();
        weeknumber=Request.QueryString["weeknumber"].ToString();
        weekno = Convert.ToInt16(weeknumber);
        distributor = Request.QueryString["dist_code"].ToString();
        dist_name = Request.QueryString["Distributor"].ToString();
        subdivision = Request.QueryString["subdivision"].ToString();
        FMonth = Convert.ToInt32(Month);
        FYear = Convert.ToInt32(Year);
        lblRegionName.Text = sf_name;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        strFMonthName = mfi.GetMonthName(Convert.ToInt16(Month)).ToString();
        DateTime dt = new DateTime(FYear, FMonth, 1); // create the start date of the month and year
        DayOfWeek firstDayOfWeekofMonth = dt.DayOfWeek; // Find out the day of week for that date
        int myWeekNumInMonth = weekno; // You want the 4th week, this may cross over to the following month!
        string myDayOfWeek = firstDayOfWeekofMonth.ToString();  // You want Wednesday
        int myDayOfWeekInt = day2Int(myDayOfWeek);
        int diff = myDayOfWeekInt - (int)firstDayOfWeekofMonth;
        //Response.Write(dt.AddDays(7 * (myWeekNumInMonth - 1) + diff));
       string g = (dt.AddDays(7 * (myWeekNumInMonth - 1) + diff)).ToString();
       DateTime h = Convert.ToDateTime(g);
       string date = h.ToString("MM-dd-yyyy");
       //date = DateTime.ParseExact(g, "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture);
         //=Convert.ToDateTime(g);
        SalesForce se = new SalesForce();
        getweekno = se.get_nth_week(date);
        gw = Convert.ToInt32(getweekno.Tables[0].Rows[0].ItemArray.GetValue(0));

        lblHead.Text = "Closing Stock View for the Month of " + strFMonthName + " " + FYear;

        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;

        
        FillSF_Leave_Type();
      

    }
    public static int day2Int(string dayOfWeek)
    {
        switch (dayOfWeek)
        {
            case "Sunday":
                return 0;
            case "Monday":
                return 1;
            case "Tuesday":
                return 2;
            case "Wednesday":
                return 3;
            case "Thursday":
                return 4;
            case "Friday":
                return 5;
            case "Saturday":
                return 6;
          
            default:
                return -1; // Do error checking
        }
    }
    private void FillSF_Leave_Type()
    {
        TableRow tr_det_head = new TableRow();
        tr_det_head.BorderStyle = BorderStyle.Solid;
        tr_det_head.BorderWidth = 1;
        tr_det_head.BackColor = System.Drawing.Color.FromName("#0097AC");
        tr_det_head.Style.Add("Color", "White");
        tr_det_head.BorderColor = System.Drawing.Color.Black;

        SalesForce sf = new SalesForce();


        //getweek = sf.Secondary_sales_get_no_of_week(FYear, strFMonthName);
        gg = sf.get_weekdays_date(gw, FYear, FMonth);
        getweekdays = sf.get_weekdays(gw, FYear, FMonth); 
        TableCell leave_utility = new TableCell();
        leave_utility.BorderStyle = BorderStyle.Solid;
        leave_utility.BorderWidth = 1;
        leave_utility.Width = 80;

        leave_utility.ColumnSpan = 3;
        Literal lit_DR_utility = new Literal();
        lit_DR_utility.Text = "<center>Weekly View</center>";
        leave_utility.BorderColor = System.Drawing.Color.Black;
        leave_utility.Attributes.Add("Class", "rptCellBorder");
        leave_utility.Controls.Add(lit_DR_utility);
        tr_det_head.Cells.Add(leave_utility);

        int wee = Convert.ToInt32(getweekdays.Tables[0].Rows[0].ItemArray.GetValue(0));
     
        int df = wee + 1;

         foreach (DataRow drdoc in gg.Tables[0].Rows)
            {
               
                TableCell tc_catg_namee = new TableCell();
                tc_catg_namee.BorderStyle = BorderStyle.Solid;
                tc_catg_namee.BorderWidth = 1;
                //text-align: center;

                tc_catg_namee.ColumnSpan = 5;
                HyperLink lit_catg_namee = new HyperLink();
                //lit_catg_namee.Text = i.ToString() + "day" +gg["D"].ToString();
                lit_catg_namee.Text =drdoc["DATE"].ToString();
                DateTime h = Convert.ToDateTime( lit_catg_namee.Text);
                 g = h.ToShortDateString();
                lit_catg_namee.Text = g;
                //Response.Write(g);
              
                tc_catg_namee.Attributes.Add("Class", "rptCellBorder");
                tc_catg_namee.HorizontalAlign = HorizontalAlign.Center;
                tc_catg_namee.Controls.Add(lit_catg_namee);
                tr_det_head.Cells.Add(tc_catg_namee);


            }


        //}
        tbl.Rows.Add(tr_det_head);



        SalesForce dr = new SalesForce();
        dsDr = dr.Secondary_sales_productdetail(div_code, subdivision);       

        if (dsDr.Tables[0].Rows.Count > 0)
        {
            TableRow tr_catg = new TableRow();
            tr_catg.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_catg.Style.Add("Color", "White");



            TableCell tc_det_head_SNo = new TableCell();
            tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_head_SNo.BorderWidth = 1;
   tc_det_head_SNo.Width = 60;
            Literal lit_det_head_SNo = new Literal();
            lit_det_head_SNo.Text = "<b>S.No</b>";
            tc_det_head_SNo.Attributes.Add("Class", "tblHead");
            tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
            tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
            tr_catg.Cells.Add(tc_det_head_SNo);


            TableCell tc_det_head_doc = new TableCell();
            tc_det_head_doc.Width = 120;
            tc_det_head_doc.BorderStyle = BorderStyle.Solid;
            tc_det_head_doc.BorderWidth = 1;
            Literal lit_det_head_doc = new Literal();
            lit_det_head_doc.Text = "<b>Product Name</b>";
            tc_det_head_doc.Attributes.Add("Class", "tblHead");
            tc_det_head_doc.Controls.Add(lit_det_head_doc);
            tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
            tr_catg.Cells.Add(tc_det_head_doc);

            TableCell tc_det_head_hq = new TableCell();
            tc_det_head_hq.Width = 70;
            tc_det_head_hq.BorderStyle = BorderStyle.Solid;
            tc_det_head_hq.BorderWidth = 1;
            Literal lit_det_head_hq = new Literal();
            lit_det_head_hq.Text = "<b>UOM</b>";
            tc_det_head_hq.Attributes.Add("Class", "tblHead");
            tc_det_head_hq.Controls.Add(lit_det_head_hq);
            tc_det_head_hq.HorizontalAlign = HorizontalAlign.Center;
            tr_catg.Cells.Add(tc_det_head_hq);
            if (getweekdays.Tables[0].Rows.Count > 0)
            {

                for (int i = 1; i < df; i++)
                {

                    TableCell tc_det_head_OP = new TableCell();
                    tc_det_head_OP.BorderStyle = BorderStyle.Solid;
                    tc_det_head_OP.BorderWidth = 1;
                    Literal lit_det_head_OP = new Literal();
                    lit_det_head_OP.Text = "<b>OP</b>";
                    tc_det_head_OP.Attributes.Add("Class", "tblHead");
                    tc_det_head_OP.Controls.Add(lit_det_head_OP);
                    tc_det_head_OP.HorizontalAlign = HorizontalAlign.Center;
                    tr_catg.Cells.Add(tc_det_head_OP);

                    TableCell tc_det_head_Qua = new TableCell();
                    //tc_det_head_Qua.Width = 70;
                    tc_det_head_Qua.BorderStyle = BorderStyle.Solid;
                    tc_det_head_Qua.BorderWidth = 1;
                    Literal lit_det_head_qua = new Literal();
                    lit_det_head_qua.Text = "<b>Rec_Qty</b>";
                    tc_det_head_Qua.Attributes.Add("Class", "tblHead");
                    tc_det_head_Qua.Controls.Add(lit_det_head_qua);
                    tc_det_head_Qua.HorizontalAlign = HorizontalAlign.Center;
                    tr_catg.Cells.Add(tc_det_head_Qua);

                    TableCell tc_det_head_Quai = new TableCell();
                    tc_det_head_Quai.BorderStyle = BorderStyle.Solid;
                    tc_det_head_Quai.BorderWidth = 1;
                    Literal lit_det_head_quai = new Literal();
                    lit_det_head_quai.Text = "<b>CB</b>";
                    tc_det_head_Quai.Attributes.Add("Class", "tblHead");
                    tc_det_head_Quai.Controls.Add(lit_det_head_quai);
                    tc_det_head_Quai.HorizontalAlign = HorizontalAlign.Center;
                    tr_catg.Cells.Add(tc_det_head_Quai);

                    TableCell tc_det_head_CB = new TableCell();
                    tc_det_head_CB.BorderStyle = BorderStyle.Solid;
                    tc_det_head_CB.BorderWidth = 1;
                    Literal lit_det_head_CB = new Literal();
                    lit_det_head_CB.Text = "<b>Sale</b>";
                    tc_det_head_CB.Attributes.Add("Class", "tblHead");
                    tc_det_head_CB.Controls.Add(lit_det_head_CB);
                    tc_det_head_CB.HorizontalAlign = HorizontalAlign.Center;
                    tr_catg.Cells.Add(tc_det_head_CB);


                    TableCell tc_det_head_CBe = new TableCell();
                    tc_det_head_CBe.BorderStyle = BorderStyle.Solid;
                    tc_det_head_CBe.BorderWidth = 1;
                    Literal lit_det_head_CBe = new Literal();
                    lit_det_head_CBe.Text = "<b>Sale Value(Rs)</b>";
                    tc_det_head_CBe.Attributes.Add("Class", "tblHead");
                    tc_det_head_CBe.Controls.Add(lit_det_head_CBe);
                    tc_det_head_CBe.HorizontalAlign = HorizontalAlign.Center;
                    tr_catg.Cells.Add(tc_det_head_CBe);

                    tbl.Rows.Add(tr_catg);


                }
            }



            iCount = 0;
            foreach (DataRow drdoctor in dsDr.Tables[0].Rows)
            {
                TableRow tr_det_sno = new TableRow();
                TableCell tc_det_SNo = new TableCell();
                iCount += 1;
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.Attributes.Add("Class", "tblRow");
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det_sno.Cells.Add(tc_det_SNo);

                TableCell tc_det_Sl_No = new TableCell();
                tc_det_Sl_No.Visible = false;
                Literal lit_det_Sl_No = new Literal();
                lit_det_Sl_No.Text = "&nbsp;" + drdoctor["Product_Detail_Code"].ToString();
                tc_det_Sl_No.BorderStyle = BorderStyle.Solid;
                tc_det_Sl_No.BorderWidth = 1;
                tc_det_Sl_No.Attributes.Add("Class", "rptCellBorder");
                tc_det_Sl_No.Controls.Add(lit_det_Sl_No);
                tr_det_sno.Cells.Add(tc_det_Sl_No);

                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drdoctor["Product_Detail_Name"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;

                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det_sno.Cells.Add(tc_det_usr);

                TableCell tc_det_sfcode = new TableCell();

                Literal lit_det_sfcode = new Literal();
                lit_det_sfcode.Text = "&nbsp;" + drdoctor["Product_Sale_Unit"].ToString();
                tc_det_sfcode.BorderStyle = BorderStyle.Solid;
                tc_det_sfcode.BorderWidth = 1;
                tc_det_sfcode.Attributes.Add("Class", "rptCellBorder");
                tc_det_sfcode.Controls.Add(lit_det_sfcode);
                tr_det_sno.Cells.Add(tc_det_sfcode);
              
                if (getweekdays.Tables[0].Rows.Count > 0)
                {
                    gg = sf.get_weekdays_date(gw, FYear, FMonth);
                    foreach (DataRow drdo in gg.Tables[0].Rows)
                    {
                        SalesForce sd = new SalesForce();
                        string f=drdo["DATE"].ToString();
                        DateTime ht = Convert.ToDateTime(f);
                        string jj = ht.ToString("yyyy-MM-dd");
                        dsDrr = sd.Secondary_sales_product_value_daywisee(div_code, FMonth, FYear,jj , drdoctor["Product_Detail_Code"].ToString(), distributor,subdivision);
                        foreach (DataRow drrdoctor in dsDrr.Tables[0].Rows)
                        {
                            TableCell tc_det_dr_name = new TableCell();
                            tc_det_dr_name.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_dr_name = new Literal();
                            lit_det_dr_name.Text = drrdoctor["opening"].ToString();
                            tc_det_dr_name.Width = 20;
                            // tc_det_dr_name.Attributes.Add("Class", "tblRow");
                            tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_name.BorderWidth = 1;
                            tc_det_dr_name.Controls.Add(lit_det_dr_name);
                            tr_det_sno.Cells.Add(tc_det_dr_name);

                            TableCell tc_det_dr_hq = new TableCell();
                            tc_det_dr_hq.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_dr_hq.Width = 20;
                            Literal lit_det_dr_hq = new Literal();
                            lit_det_dr_hq.Text = drrdoctor["receipt_qty"].ToString();
                            // tc_det_dr_hq.Attributes.Add("Class", "tblRow");
                            tc_det_dr_hq.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_hq.BorderWidth = 1;
                            tc_det_dr_hq.Controls.Add(lit_det_dr_hq);
                            tr_det_sno.Cells.Add(tc_det_dr_hq);

                            TableCell tc_det_Closing = new TableCell();
                            tc_det_Closing.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_Closing.Width = 20;
                            Literal lit_det_dr_closing = new Literal();
                            lit_det_dr_closing.Text = drrdoctor["closing"].ToString();
                            // tc_det_Closing.Attributes.Add("Class", "tblRow");
                            tc_det_Closing.BorderStyle = BorderStyle.Solid;
                            tc_det_Closing.BorderWidth = 1;
                            tc_det_Closing.Controls.Add(lit_det_dr_closing);
                            tr_det_sno.Cells.Add(tc_det_Closing);

                            TableCell tc_det_sale_value = new TableCell();
                            tc_det_sale_value.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sale_value.Width = 20;
                            Literal lit_det_sale_value = new Literal();
                            lit_det_sale_value.Text = drrdoctor["sale"].ToString();
                            // tc_det_sale_value.Attributes.Add("Class", "tblRow");
                            tc_det_sale_value.BorderStyle = BorderStyle.Solid;
                            tc_det_sale_value.BorderWidth = 1;
                            tc_det_sale_value.Controls.Add(lit_det_sale_value);
                            tr_det_sno.Cells.Add(tc_det_sale_value);
                            TableCell tc_det_tot_value = new TableCell();
                            tc_det_tot_value.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_tot_value.Width = 20;
                            Literal lit_det_tot_value = new Literal();
                            lit_det_tot_value.Text = drrdoctor["value"].ToString();
                            // tc_det_sale_value.Attributes.Add("Class", "tblRow");
                            tc_det_tot_value.BorderStyle = BorderStyle.Solid;
                            tc_det_tot_value.BorderWidth = 1;
                            tc_det_tot_value.Controls.Add(lit_det_tot_value);
                            tr_det_sno.Cells.Add(tc_det_tot_value);

                        }
                    }
                }



                tbl.Rows.Add(tr_det_sno);
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
}