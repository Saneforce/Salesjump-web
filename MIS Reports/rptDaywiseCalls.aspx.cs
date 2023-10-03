using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Bus_EReport;
using System.Data;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Collections;

public partial class MIS_Reports_rptDaywiseCalls : System.Web.UI.Page
{
    #region "Declaration"
    string DivCode = string.Empty;
    string SFCode = string.Empty;
    DataSet getweek = null;
    DataSet getweekno = null;
    DataSet getweekdays = null;
    DataSet gg = null;
    DataSet dsSalesForce = new DataSet();
    DataSet dsMGR = new DataSet();
    DataSet dsMr = new DataSet();

    DataSet dsDoc = null;
    string strFMonthName = string.Empty;
    string g = string.Empty;
    int gw = 0;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string sURL = string.Empty;
    string SFName = string.Empty;
    string monthname = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        DivCode = Session["Div_Code"].ToString();
        SFCode = Request.QueryString["SFCode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        SFName = Request.QueryString["SFName"].ToString();
        monthname = Request.QueryString["MnthName"].ToString();
        lblHead.Text = "PRODUCTIVE CALLS for Month of " + monthname + " - "+ FYear;
        lblsfname.Text = "Team :- " + SFName;
        HSFCode.Value = SFCode;
        loadData();
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

    private void loadData()
    {
        SalesForce SF = new SalesForce();
        dsSalesForce = SF.getDoctorCount_SFWise_new(DivCode, "0", SFCode);
        DataTable Target_Dt = new DataTable();

        Target_Dt.Columns.Add("sf_type", typeof(string));
        Target_Dt.Columns.Add("Year", typeof(string));
        Target_Dt.Columns.Add("Name", typeof(string));
        Target_Dt.Columns.Add("SF_CODE", typeof(string));

        int lastDayOfMonth = DateTime.DaysInMonth(Convert.ToInt32(FYear), Convert.ToInt32(FMonth));


        int[] oarr = new int[lastDayOfMonth];
        int[] oarrec = new int[lastDayOfMonth];
        int[] oarrqty = new int[lastDayOfMonth];
        decimal[] oarrval = new decimal[lastDayOfMonth];


        int[] arr = new int[lastDayOfMonth];
        int[] arrec = new int[lastDayOfMonth];
        int[] arrqty = new int[lastDayOfMonth];
        decimal[] arrval = new decimal[lastDayOfMonth];
        for (int k = 0; k < lastDayOfMonth; k++)
        {
            Target_Dt.Columns.Add((k + 1).ToString(), typeof(string));
        }
        Order od = new Order();
        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            DataSet dsTO = new DataSet();
            if (SFCode == row["sf_code"].ToString())
            {
                dsTO = od.GetIssuSlipDayTCECSFOnly(DivCode, row["sf_code"].ToString(), FYear, FMonth, "0");
            }
            else
            {
                dsTO = od.GetIssuSlipDayTCEC(DivCode, row["sf_code"].ToString(), FYear, FMonth, "0");
            }
            if (dsTO.Tables.Count > 0)
            {
                for (int i = 0; i < lastDayOfMonth; i++)
                {
                    DataRow[] ro = dsTO.Tables[0].Select("day1='" + (i + 1).ToString() + "'");
                    int totTC = 0;
                    int totEC = 0;
                    int toQTY = 0;
                    decimal toVal = 0;
                    if (ro.Length > 0)
                    {
                        foreach (DataRow r in ro)
                        {
                            totTC = totTC + (r["TC_Count"] == DBNull.Value ? 0 : Convert.ToInt32(r["TC_Count"]));
                            totEC = totEC + (r["EC_Count"] == DBNull.Value ? 0 : Convert.ToInt32(r["EC_Count"]));
                            toQTY = toQTY + (r["Quantity"] == DBNull.Value ? 0 : Convert.ToInt32(r["Quantity"]));
                            toVal = toVal + (r["order_value"] == DBNull.Value ? 0 : Convert.ToDecimal(r["order_value"]));
                        }
                    }
                    arr[i] = totTC;
                    arrec[i] = totEC;
                    arrqty[i] = toQTY;
                    arrval[i] = toVal;
                    oarr[i] += totTC;
                    oarrec[i] += totEC;
                    oarrqty[i] += toQTY;
                    oarrval[i] += toVal;

                }
                DataRow dr = Target_Dt.NewRow();
                dr["sf_type"] = row["sf_type"].ToString();
                dr["Year"] = row["Territory"].ToString(); 
                 dr["Name"] = row["sf_name"].ToString();
                dr["SF_CODE"] = row["sf_code"].ToString();
                for (int l = 0; l < arr.Length; l++)
                {
                    dr[(l + 1).ToString()] = arr[l].ToString();
                }
                Target_Dt.Rows.Add(dr);

                dr = Target_Dt.NewRow();
                dr["sf_type"] = "";
                dr["Year"] = row["Territory"].ToString();
                dr["Name"] = row["sf_name"].ToString();
                dr["SF_CODE"] = row["sf_code"].ToString();
                for (int l = 0; l < arrec.Length; l++)
                {
                    dr[(l + 1).ToString()] = arrec[l].ToString();
                }
                Target_Dt.Rows.Add(dr);

                dr = Target_Dt.NewRow();
                dr["sf_type"] = "";
                dr["Year"] = row["Territory"].ToString();
                dr["Name"] = row["sf_name"].ToString();
                dr["SF_CODE"] = row["sf_code"].ToString();
                for (int l = 0; l < arrqty.Length; l++)
                {
                    dr[(l + 1).ToString()] = arrqty[l].ToString();
                }
                Target_Dt.Rows.Add(dr);

                dr = Target_Dt.NewRow();
                dr["sf_type"] = "";
                dr["Year"] = row["Territory"].ToString();
                dr["Name"] = row["sf_name"].ToString();
                dr["SF_CODE"] = row["sf_code"].ToString();
                for (int l = 0; l < arrval.Length; l++)
                {
                    dr[(l + 1).ToString()] = arrval[l].ToString();
                }
                Target_Dt.Rows.Add(dr);
            }
        }

        //  GVData.DataSource = Target_Dt;
        //   GVData.DataBind();





        TableRow tr_det_head = new TableRow();
        tr_det_head.BorderStyle = BorderStyle.Solid;
        tr_det_head.BorderWidth = 1;
        tr_det_head.BackColor = System.Drawing.Color.FromName("#496a9a");
        tr_det_head.Style.Add("Color", "White");
        tr_det_head.BorderColor = System.Drawing.Color.White;


        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString();

        //getweek = SF.Secondary_sales_get_no_of_week(2018, strFMonthName);
		getweek = SF.Secondary_sales_get_no_of_week(Convert.ToInt16(FYear), strFMonthName);


        TableCell leave_utility = new TableCell();
        Literal lit_DR_utility = new Literal();
        leave_utility.BorderStyle = BorderStyle.Solid;
        leave_utility.BorderWidth = 1;
        leave_utility.Width = 300;
        leave_utility.ColumnSpan = 1;
        leave_utility.RowSpan = 2;
        leave_utility.HorizontalAlign = HorizontalAlign.Center;
        lit_DR_utility.Text = "Name";
        leave_utility.BorderColor = System.Drawing.Color.White;
        leave_utility.Attributes.Add("Class", "rptCellBorder");
        leave_utility.Style.Add("MIN-WIDTH", "250PX");
        leave_utility.Controls.Add(lit_DR_utility);
        tr_det_head.Cells.Add(leave_utility);

        leave_utility = new TableCell();
        lit_DR_utility = new Literal();
        leave_utility.BorderStyle = BorderStyle.Solid;
        leave_utility.BorderWidth = 1;
        leave_utility.Width = 300;
        leave_utility.ColumnSpan = 1;
        leave_utility.RowSpan = 2;
        leave_utility.HorizontalAlign = HorizontalAlign.Center;
        lit_DR_utility.Text = "Zone";
        leave_utility.Style.Add("MIN-WIDTH", "150PX");
        leave_utility.BorderColor = System.Drawing.Color.White;
        leave_utility.Attributes.Add("Class", "rptCellBorder");
        leave_utility.Controls.Add(lit_DR_utility);
        tr_det_head.Cells.Add(leave_utility);

        leave_utility = new TableCell();
        lit_DR_utility = new Literal();
        leave_utility.BorderStyle = BorderStyle.Solid;
        leave_utility.BorderWidth = 1;
        leave_utility.Width = 100;
        leave_utility.ColumnSpan = 1;
        leave_utility.RowSpan = 1;
        leave_utility.HorizontalAlign = HorizontalAlign.Center;
        lit_DR_utility.Text = "Week";
        leave_utility.BorderColor = System.Drawing.Color.White;
        leave_utility.Attributes.Add("Class", "rptCellBorder");
        leave_utility.Controls.Add(lit_DR_utility);
        tr_det_head.Cells.Add(leave_utility);


        int df = Convert.ToInt32(getweek.Tables[0].Rows[0].ItemArray.GetValue(0));
        //  tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");
        int wee = df + 1;

        if (getweek.Tables[0].Rows.Count > 0)
        {
            for (int i = 1; i < wee; i++)
            {

                mfi = new System.Globalization.DateTimeFormatInfo();
                strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString();
                DateTime dt = new DateTime(Convert.ToInt32(FYear), Convert.ToInt32(FMonth), 1); // create the start date of the month and year
               // int days12 = DateTime.DaysInMonth(Convert.ToInt32(FYear), Convert.ToInt32(FMonth));
                DayOfWeek firstDayOfWeekofMonth = dt.DayOfWeek; // Find out the day of week for that date
                int myWeekNumInMonth = i; // You want the 4th week, this may cross over to the following month!
                string myDayOfWeek = firstDayOfWeekofMonth.ToString();  // You want Wednesday
                int myDayOfWeekInt = day2Int(myDayOfWeek);
                int diff = myDayOfWeekInt - (int)firstDayOfWeekofMonth;

                //string g = (dt.AddDays(7 * (myWeekNumInMonth - 1) + diff)).ToString();

                int days12 = DateTime.DaysInMonth(Convert.ToInt32(FYear), Convert.ToInt32(FMonth));
                if ((7 * (myWeekNumInMonth - 1)) < days12)
                {
                    g = (dt.AddDays(7 * (myWeekNumInMonth - 1) + diff)).ToString();
                }
                else
                {
                    g = (dt.AddDays(days12 - 1)).ToString();
                }


                DateTime h = Convert.ToDateTime(g);
                string date = h.ToString("MM-dd-yyyy");
                SalesForce se = new SalesForce();
                getweekno = se.get_nth_week(date);
                gw = Convert.ToInt32(getweekno.Tables[0].Rows[0].ItemArray.GetValue(0));
                getweekdays = SF.get_weekdays(gw, Convert.ToInt32(FYear), Convert.ToInt32(FMonth));
                int wee12 = Convert.ToInt32(getweekdays.Tables[0].Rows[0].ItemArray.GetValue(0));
                TableCell tc_catg_namee = new TableCell();
                tc_catg_namee.BorderStyle = BorderStyle.Solid;
                tc_catg_namee.BorderWidth = 1;
                tc_catg_namee.ColumnSpan = wee12 + 1;
                HyperLink lit_catg_namee = new HyperLink();
                lit_catg_namee.Text = i.ToString();
                lit_catg_namee.Style.Add("Color", "White");
                tc_catg_namee.HorizontalAlign = HorizontalAlign.Center;
                tc_catg_namee.Controls.Add(lit_catg_namee);
                tr_det_head.Cells.Add(tc_catg_namee);
            }
        }
        leave_utility = new TableCell();
        lit_DR_utility = new Literal();
        leave_utility.BorderStyle = BorderStyle.Solid;
        leave_utility.BorderWidth = 1;
        leave_utility.Width = 100;
        leave_utility.ColumnSpan = 1;
        leave_utility.RowSpan = 2;
        leave_utility.HorizontalAlign = HorizontalAlign.Center;
        lit_DR_utility.Text = "TOTAL";
        leave_utility.BorderColor = System.Drawing.Color.White;
        leave_utility.Attributes.Add("Class", "rptCellBorder");
        leave_utility.Controls.Add(lit_DR_utility);
        tr_det_head.Cells.Add(leave_utility);

        DGVFFO.Rows.Add(tr_det_head);

        gg = SF.get_weekdays_date(1, Convert.ToInt32(FYear), Convert.ToInt32(FMonth));
        getweekdays = SF.get_weekdays(1, Convert.ToInt32(FYear), Convert.ToInt32(FMonth));
        int wee1 = Convert.ToInt32(getweekdays.Tables[0].Rows[0].ItemArray.GetValue(0));

        int df1 = wee1 + 1;

        TableRow tr_catg = new TableRow();
        tr_catg.BackColor = System.Drawing.Color.FromName("#496a9a");
        tr_catg.Style.Add("Color", "White");

        TableCell tc_catg_namee1 = new TableCell();
        Literal lit_catg_namee1 = new Literal();
        tc_catg_namee1.BorderStyle = BorderStyle.Solid;
        tc_catg_namee1.BorderWidth = 1;
        tc_catg_namee1.ColumnSpan = 1;
        lit_catg_namee1.Text = "DAY";
        tc_catg_namee1.Attributes.Add("Class", "rptCellBorder");
        tc_catg_namee1.HorizontalAlign = HorizontalAlign.Center;
        tc_catg_namee1.Controls.Add(lit_catg_namee1);
        tr_catg.Cells.Add(tc_catg_namee1);


        //tc_catg_namee1 = new TableCell();
        //lit_catg_namee1 = new Literal();
        //tc_catg_namee1.BorderStyle = BorderStyle.Solid;
        //tc_catg_namee1.BorderWidth = 1;
        //tc_catg_namee1.ColumnSpan = 1;
        //lit_catg_namee1.Text = "";
        //tc_catg_namee1.Attributes.Add("Class", "rptCellBorder");
        //tc_catg_namee1.HorizontalAlign = HorizontalAlign.Center;
        //tc_catg_namee1.Controls.Add(lit_catg_namee1);
        //tr_catg.Cells.Add(tc_catg_namee1);

        //tc_catg_namee1 = new TableCell();
        //lit_catg_namee1 = new Literal();
        //tc_catg_namee1.BorderStyle = BorderStyle.Solid;
        //tc_catg_namee1.BorderWidth = 1;
        //tc_catg_namee1.ColumnSpan = 1;
        //lit_catg_namee1.Text = "";
        //tc_catg_namee1.Attributes.Add("Class", "rptCellBorder");
        //tc_catg_namee1.HorizontalAlign = HorizontalAlign.Center;
        //tc_catg_namee1.Controls.Add(lit_catg_namee1);
        //tr_catg.Cells.Add(tc_catg_namee1);





        if (getweek.Tables[0].Rows.Count > 0)
        {
            int day = 1;
            for (int i = 1; i < wee; i++)
            {

                mfi = new System.Globalization.DateTimeFormatInfo();
                strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString();
                DateTime dt = new DateTime(Convert.ToInt32(FYear), Convert.ToInt32(FMonth), 1); // create the start date of the month and year

                int days12 = DateTime.DaysInMonth(Convert.ToInt32(FYear), Convert.ToInt32(FMonth));


                DayOfWeek firstDayOfWeekofMonth = dt.DayOfWeek; // Find out the day of week for that date
                int myWeekNumInMonth = i; // You want the 4th week, this may cross over to the following month!
                string myDayOfWeek = firstDayOfWeekofMonth.ToString();  // You want Wednesday
                int myDayOfWeekInt = day2Int(myDayOfWeek);
                int diff = myDayOfWeekInt - (int)firstDayOfWeekofMonth;
                 string g="";

                 if ((7 * (myWeekNumInMonth - 1)) < days12)
                {
                     g = (dt.AddDays(7 * (myWeekNumInMonth - 1) + diff)).ToString();
                }
                else
                {
                    g = (dt.AddDays(days12-1)).ToString();
                }


                DateTime h = Convert.ToDateTime(g);
                string date = h.ToString("MM-dd-yyyy");
                SalesForce se = new SalesForce();
                getweekno = se.get_nth_week(date);
                gw = Convert.ToInt32(getweekno.Tables[0].Rows[0].ItemArray.GetValue(0));
                getweekdays = SF.get_weekdays(gw, Convert.ToInt32(FYear), Convert.ToInt32(FMonth));
                int wee12 = Convert.ToInt32(getweekdays.Tables[0].Rows[0].ItemArray.GetValue(0));
                dt = new DateTime(Convert.ToInt32(FYear), Convert.ToInt32(FMonth), day);

                for (int l = 1; l <= wee12; l++)
                {
                    if (FMonth.ToString() == dt.Month.ToString())
                    {
                        tc_catg_namee1 = new TableCell();
                        lit_catg_namee1 = new Literal();
                        tc_catg_namee1.BorderStyle = BorderStyle.Solid;
                        tc_catg_namee1.BorderWidth = 1;
                        tc_catg_namee1.ColumnSpan = 1;
                        firstDayOfWeekofMonth = dt.DayOfWeek;
                        lit_catg_namee1.Text = "[ " + dt.Day.ToString() + " ] " + firstDayOfWeekofMonth.ToString().Substring(0, 3);
                        tc_catg_namee1.Attributes.Add("Class", "rptCellBorder");
                        tc_catg_namee1.Style.Add("MIN-WIDTH", "80PX");
                        tc_catg_namee1.HorizontalAlign = HorizontalAlign.Center;
                        tc_catg_namee1.Controls.Add(lit_catg_namee1);
                        tr_catg.Cells.Add(tc_catg_namee1);
                    }
                    dt = dt.AddDays(1);
                    day=dt.Day;
                }

                tc_catg_namee1 = new TableCell();
                lit_catg_namee1 = new Literal();
                tc_catg_namee1.BorderStyle = BorderStyle.Solid;
                tc_catg_namee1.BorderWidth = 1;
                tc_catg_namee1.ColumnSpan = 1;
                tc_catg_namee1.Style.Add("MIN-WIDTH", "80PX");
                lit_catg_namee1.Text = "TOT";
                tc_catg_namee1.Attributes.Add("Class", "rptCellBorder");
                tc_catg_namee1.HorizontalAlign = HorizontalAlign.Center;
                tc_catg_namee1.Controls.Add(lit_catg_namee1);
                tr_catg.Cells.Add(tc_catg_namee1);
                DGVFFO.Rows.Add(tr_catg);
            }

        }

        int oTC = 0;

        foreach (DataRow rr in dsSalesForce.Tables[0].Rows)
        {
            if (rr["sf_code"].ToString() != "admin")
            {

                TableRow tr_catg1 = new TableRow();
                TableRow tr_catg2 = new TableRow();
                TableRow tr_catg3 = new TableRow();
                TableRow tr_catg4 = new TableRow();
                TableRow tr_catg5 = new TableRow();



                TableCell tc_catg_namee11 = new TableCell();
                Literal lit_catg_namee11 = new Literal();

                tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                tc_catg_namee11.BorderWidth = 1;
                tc_catg_namee11.ColumnSpan = 1;
                tc_catg_namee11.RowSpan = 5;


                if (rr["sf_code"].ToString() == SFCode || rr["sf_type"].ToString() == "1")
                {
                    lit_catg_namee11 = new Literal();
                    lit_catg_namee11.Text = rr["sf_name"].ToString();
                    tc_catg_namee11.Controls.Add(lit_catg_namee11);
                }
                else
                {


                    HyperLink lit_catg_namee = new HyperLink();
                    lit_catg_namee.Text = rr["sf_name"].ToString();
                    sURL = "rptDaywiseCalls.aspx?&SFCode=" + rr["sf_code"].ToString() + "&FYear=" + FYear + "&FMonth=" + FMonth + "&MnthName=" + monthname + "&SFName=" + SFName;
                    lit_catg_namee.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'_blank','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=1100,height=700,left=0,top=0,zoom=50%');";
                    lit_catg_namee.NavigateUrl = "#";
                    tc_catg_namee11.Controls.Add(lit_catg_namee);
                }

                tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                tc_catg_namee11.HorizontalAlign = HorizontalAlign.Left;

                tr_catg1.Cells.Add(tc_catg_namee11);


                tc_catg_namee11 = new TableCell();
                lit_catg_namee11 = new Literal();
                tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                tc_catg_namee11.BorderWidth = 1;
                tc_catg_namee11.ColumnSpan = 1;
                tc_catg_namee11.RowSpan = 5;
                lit_catg_namee11.Text = rr["Territory"].ToString()== "--Select--" ? "" : rr["Territory"].ToString();
                tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                tc_catg_namee11.HorizontalAlign = HorizontalAlign.Left;
                tc_catg_namee11.Controls.Add(lit_catg_namee11);
                tr_catg1.Cells.Add(tc_catg_namee11);


                tc_catg_namee11 = new TableCell();
                lit_catg_namee11 = new Literal();
                tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                tc_catg_namee11.BorderWidth = 1;
                tc_catg_namee11.ColumnSpan = 1;
                tc_catg_namee11.RowSpan = 1;
                lit_catg_namee11.Text = "TC";
                tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                tc_catg_namee11.HorizontalAlign = HorizontalAlign.Left;
                tc_catg_namee11.Controls.Add(lit_catg_namee11);
                tr_catg1.Cells.Add(tc_catg_namee11);


                tc_catg_namee11 = new TableCell();
                lit_catg_namee11 = new Literal();
                tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                tc_catg_namee11.BorderWidth = 1;
                tc_catg_namee11.ColumnSpan = 1;
                tc_catg_namee11.RowSpan = 1;
                lit_catg_namee11.Text = "PC";
                tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                tc_catg_namee11.HorizontalAlign = HorizontalAlign.Left;
                tc_catg_namee11.Controls.Add(lit_catg_namee11);
                tr_catg2.Cells.Add(tc_catg_namee11);



                tc_catg_namee11 = new TableCell();
                lit_catg_namee11 = new Literal();
                tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                tc_catg_namee11.BorderWidth = 1;
                tc_catg_namee11.ColumnSpan = 1;
                tc_catg_namee11.RowSpan = 1;
                lit_catg_namee11.Text = "PER";
                tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                tc_catg_namee11.HorizontalAlign = HorizontalAlign.Left;
                tc_catg_namee11.Controls.Add(lit_catg_namee11);
                tr_catg3.Cells.Add(tc_catg_namee11);


                tc_catg_namee11 = new TableCell();
                lit_catg_namee11 = new Literal();
                tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                tc_catg_namee11.BorderWidth = 1;
                tc_catg_namee11.ColumnSpan = 1;
                tc_catg_namee11.RowSpan = 1;
                lit_catg_namee11.Text = "QTY";
                tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                tc_catg_namee11.HorizontalAlign = HorizontalAlign.Left;
                tc_catg_namee11.Controls.Add(lit_catg_namee11);
                tr_catg4.Cells.Add(tc_catg_namee11);



                tc_catg_namee11 = new TableCell();
                lit_catg_namee11 = new Literal();
                tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                tc_catg_namee11.BorderWidth = 1;
                tc_catg_namee11.ColumnSpan = 1;
                tc_catg_namee11.RowSpan = 1;
                lit_catg_namee11.Text = "VALUE";
                tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                tc_catg_namee11.HorizontalAlign = HorizontalAlign.Left;
                tc_catg_namee11.Controls.Add(lit_catg_namee11);
                tr_catg5.Cells.Add(tc_catg_namee11);

                DataRow[] ro = Target_Dt.Select("SF_CODE='" + rr["sf_code"].ToString() + "'");
                if (getweek.Tables[0].Rows.Count > 0)
                {
                    int day = 1;
                    int ntTC = 0;
                    int ntPC = 0;
                    int ntqty = 0;
                    decimal ntval = 0;
                    decimal ntper = 0;
                    DateTime dt = new DateTime(Convert.ToInt32(FYear), Convert.ToInt32(FMonth), 1); // create the start date of the month and year
                    dt = new DateTime(Convert.ToInt32(FYear), Convert.ToInt32(FMonth), 1);
                    for (int i = 1; i < wee; i++)
                    {

                        mfi = new System.Globalization.DateTimeFormatInfo();
                        strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString();
                        int days12 = DateTime.DaysInMonth(Convert.ToInt32(FYear), Convert.ToInt32(FMonth));
                        DateTime dt1 = new DateTime(Convert.ToInt32(FYear), Convert.ToInt32(FMonth), 1); // create the start date of the month and year
                        DayOfWeek firstDayOfWeekofMonth = dt1.DayOfWeek; // Find out the day of week for that date
                        int myWeekNumInMonth = i; // You want the 4th week, this may cross over to the following month!
                        string myDayOfWeek = firstDayOfWeekofMonth.ToString();  // You want Wednesday
                        int myDayOfWeekInt = day2Int(myDayOfWeek);
                        int diff = myDayOfWeekInt - (int)firstDayOfWeekofMonth;


                        string g = "";

                        if ((7 * (myWeekNumInMonth - 1)) < days12)
                        {
                            g = (dt1.AddDays(7 * (myWeekNumInMonth - 1) + diff)).ToString();
                        }
                        else
                        {
                            g = (dt1.AddDays(days12 - 1)).ToString();
                        }



                       // string g = (dt.AddDays(7 * (myWeekNumInMonth - 1) + diff)).ToString();
                        DateTime h = Convert.ToDateTime(g);
                        string date = h.ToString("MM-dd-yyyy");
                        SalesForce se = new SalesForce();
                        getweekno = se.get_nth_week(date);
                        gw = Convert.ToInt32(getweekno.Tables[0].Rows[0].ItemArray.GetValue(0));
                        getweekdays = SF.get_weekdays(gw, Convert.ToInt32(FYear), Convert.ToInt32(FMonth));
                        int wee12 = Convert.ToInt32(getweekdays.Tables[0].Rows[0].ItemArray.GetValue(0));


                        int tTC = 0;
                        int tPC = 0;
                        int tqty = 0;
                        decimal tval = 0;
                        decimal ttper = 0;
                        decimal tcper = 0;
                        for (int l = 1; l <= wee12; l++)
                        {
                            
                            tc_catg_namee11 = new TableCell();
                            lit_catg_namee11 = new Literal();
                            tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                            tc_catg_namee11.BorderWidth = 1;
                            tc_catg_namee11.ColumnSpan = 1;

                            if (ro.Length > 0)
                            {
                                lit_catg_namee11.Text = ro[0][day.ToString()].ToString();// arr[day].ToString();

                            }
                            else
                            {
                                lit_catg_namee11.Text = "0";// arr[day].ToString();
                            }

                            int Tc = Convert.ToInt32(lit_catg_namee11.Text);
                            tTC += Convert.ToInt32(lit_catg_namee11.Text);
                            ntTC += Convert.ToInt32(lit_catg_namee11.Text);
                            tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                            tc_catg_namee11.HorizontalAlign = HorizontalAlign.Right;
                            tc_catg_namee11.Controls.Add(lit_catg_namee11);
                            tr_catg1.Cells.Add(tc_catg_namee11);


                            tc_catg_namee11 = new TableCell();
                            lit_catg_namee11 = new Literal();
                            tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                            tc_catg_namee11.BorderWidth = 1;
                            tc_catg_namee11.ColumnSpan = 1;

                            if (ro.Length > 0)
                            {

                                lit_catg_namee11.Text = ro[1][day.ToString()].ToString();// arr[day].ToString();
                            }
                            else
                            {
                                lit_catg_namee11.Text = "0";// arr[day].ToString();
                            }
                            int Ec = Convert.ToInt32(lit_catg_namee11.Text);
                            tPC += Convert.ToInt32(lit_catg_namee11.Text);
                            ntPC += Convert.ToInt32(lit_catg_namee11.Text);
                            tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                            tc_catg_namee11.HorizontalAlign = HorizontalAlign.Right;
                            tc_catg_namee11.Controls.Add(lit_catg_namee11);
                            tr_catg2.Cells.Add(tc_catg_namee11);





                            tc_catg_namee11 = new TableCell();
                            lit_catg_namee11 = new Literal();
                            tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                            tc_catg_namee11.BorderWidth = 1;
                            tc_catg_namee11.ColumnSpan = 1;

                            decimal ch = (Tc == 0 ? 1 : Tc);
                            decimal per = (Ec / ch) * 100;

                            lit_catg_namee11.Text = per.ToString("0.00");// arr[day].ToString();
                                                                         // tper += Convert.ToDecimal(lit_catg_namee11.Text);
                                                                         // ntper += Convert.ToDecimal(lit_catg_namee11.Text);
                            tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                            tc_catg_namee11.HorizontalAlign = HorizontalAlign.Right;
                            tc_catg_namee11.Controls.Add(lit_catg_namee11);
                            tr_catg3.Cells.Add(tc_catg_namee11);




                            tc_catg_namee11 = new TableCell();
                            lit_catg_namee11 = new Literal();
                            tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                            tc_catg_namee11.BorderWidth = 1;
                            tc_catg_namee11.ColumnSpan = 1;

                            if (ro.Length > 0)
                            {

                                lit_catg_namee11.Text = ro[2][day.ToString()].ToString();// arr[day].ToString();
                            }
                            else
                            {
                                lit_catg_namee11.Text = "0";// arr[day].ToString();
                            }
                            tqty += Convert.ToInt32(lit_catg_namee11.Text);
                            ntqty += Convert.ToInt32(lit_catg_namee11.Text);

                            lit_catg_namee11.Text = lit_catg_namee11.Text;// arr[day].ToString();
                            tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                            tc_catg_namee11.HorizontalAlign = HorizontalAlign.Right;
                            tc_catg_namee11.Controls.Add(lit_catg_namee11);
                            tr_catg4.Cells.Add(tc_catg_namee11);

                            tc_catg_namee11 = new TableCell();
                            lit_catg_namee11 = new Literal();
                            tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                            tc_catg_namee11.BorderWidth = 1;
                            tc_catg_namee11.ColumnSpan = 1;

                            if (ro.Length > 0)
                            {

                                lit_catg_namee11.Text = ro[3][day.ToString()].ToString();// arr[day].ToString();
                            }
                            else
                            {
                                lit_catg_namee11.Text = "0";// arr[day].ToString();
                            }
                            tval += Convert.ToDecimal(lit_catg_namee11.Text);
                            ntval += Convert.ToDecimal(lit_catg_namee11.Text);
                            lit_catg_namee11.Text = lit_catg_namee11.Text;// arr[day].ToString();
                            tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                            tc_catg_namee11.HorizontalAlign = HorizontalAlign.Right;
                            tc_catg_namee11.Controls.Add(lit_catg_namee11);
                            tr_catg5.Cells.Add(tc_catg_namee11);
                            
                            dt = dt.AddDays(1);
                            day=dt.Day;
                        }

                        DGVFFO.Rows.Add(tr_catg1);
                        DGVFFO.Rows.Add(tr_catg2);
                        DGVFFO.Rows.Add(tr_catg3);
                        DGVFFO.Rows.Add(tr_catg4);
                        tc_catg_namee11 = new TableCell();
                        lit_catg_namee11 = new Literal();
                        tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                        tc_catg_namee11.BorderWidth = 1;
                        tc_catg_namee11.ColumnSpan = 1;
                        lit_catg_namee11.Text = tTC.ToString();
                        tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                        tc_catg_namee11.HorizontalAlign = HorizontalAlign.Right;
                        tc_catg_namee11.Controls.Add(lit_catg_namee11);
                        tr_catg1.Cells.Add(tc_catg_namee11);
                        DGVFFO.Rows.Add(tr_catg1);


                        tc_catg_namee11 = new TableCell();
                        lit_catg_namee11 = new Literal();
                        tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                        tc_catg_namee11.BorderWidth = 1;
                        tc_catg_namee11.ColumnSpan = 1;
                        lit_catg_namee11.Text = tPC.ToString();
                        tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                        tc_catg_namee11.HorizontalAlign = HorizontalAlign.Right;
                        tc_catg_namee11.Controls.Add(lit_catg_namee11);
                        tr_catg2.Cells.Add(tc_catg_namee11);
                        DGVFFO.Rows.Add(tr_catg2);

                        tc_catg_namee11 = new TableCell();
                        lit_catg_namee11 = new Literal();
                        tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                        tc_catg_namee11.BorderWidth = 1;
                        tc_catg_namee11.ColumnSpan = 1;
                        decimal ch1 = (tTC == 0 ? 1 : tTC);
                        lit_catg_namee11.Text = ((tPC / ch1) * 100).ToString("0.00");
                        tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                        tc_catg_namee11.HorizontalAlign = HorizontalAlign.Right;
                        tc_catg_namee11.Controls.Add(lit_catg_namee11);
                        tr_catg3.Cells.Add(tc_catg_namee11);
                        DGVFFO.Rows.Add(tr_catg3);

                        tc_catg_namee11 = new TableCell();
                        lit_catg_namee11 = new Literal();
                        tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                        tc_catg_namee11.BorderWidth = 1;
                        tc_catg_namee11.ColumnSpan = 1;
                        lit_catg_namee11.Text = tqty.ToString();
                        tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                        tc_catg_namee11.HorizontalAlign = HorizontalAlign.Right;
                        tc_catg_namee11.Controls.Add(lit_catg_namee11);
                        tr_catg4.Cells.Add(tc_catg_namee11);
                        DGVFFO.Rows.Add(tr_catg4);

                        tc_catg_namee11 = new TableCell();
                        lit_catg_namee11 = new Literal();
                        tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                        tc_catg_namee11.BorderWidth = 1;
                        tc_catg_namee11.ColumnSpan = 1;
                        lit_catg_namee11.Text = tval.ToString();
                        tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                        tc_catg_namee11.HorizontalAlign = HorizontalAlign.Right;
                        tc_catg_namee11.Controls.Add(lit_catg_namee11);
                        tr_catg5.Cells.Add(tc_catg_namee11);
                        DGVFFO.Rows.Add(tr_catg5);
                    }

                    tc_catg_namee11 = new TableCell();
                    lit_catg_namee11 = new Literal();
                    tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                    tc_catg_namee11.BorderWidth = 1;
                    tc_catg_namee11.ColumnSpan = 1;
                    lit_catg_namee11.Text = ntTC.ToString();
                    tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                    tc_catg_namee11.HorizontalAlign = HorizontalAlign.Right;
                    tc_catg_namee11.Controls.Add(lit_catg_namee11);
                    tr_catg1.Cells.Add(tc_catg_namee11);
                    DGVFFO.Rows.Add(tr_catg1);


                    tc_catg_namee11 = new TableCell();
                    lit_catg_namee11 = new Literal();
                    tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                    tc_catg_namee11.BorderWidth = 1;
                    tc_catg_namee11.ColumnSpan = 1;
                    lit_catg_namee11.Text = ntPC.ToString();
                    tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                    tc_catg_namee11.HorizontalAlign = HorizontalAlign.Right;
                    tc_catg_namee11.Controls.Add(lit_catg_namee11);
                    tr_catg2.Cells.Add(tc_catg_namee11);
                    DGVFFO.Rows.Add(tr_catg2);

                    tc_catg_namee11 = new TableCell();
                    lit_catg_namee11 = new Literal();
                    tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                    tc_catg_namee11.BorderWidth = 1;
                    tc_catg_namee11.ColumnSpan = 1;
                    decimal ch2 = (ntTC == 0 ? 1 : ntTC);
                    lit_catg_namee11.Text = ((ntPC / ch2) * 100).ToString("0.00");
                    tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                    tc_catg_namee11.HorizontalAlign = HorizontalAlign.Right;
                    tc_catg_namee11.Controls.Add(lit_catg_namee11);
                    tr_catg3.Cells.Add(tc_catg_namee11);
                    DGVFFO.Rows.Add(tr_catg3);

                    tc_catg_namee11 = new TableCell();
                    lit_catg_namee11 = new Literal();
                    tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                    tc_catg_namee11.BorderWidth = 1;
                    tc_catg_namee11.ColumnSpan = 1;
                    lit_catg_namee11.Text = ntqty.ToString();
                    tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                    tc_catg_namee11.HorizontalAlign = HorizontalAlign.Right;
                    tc_catg_namee11.Controls.Add(lit_catg_namee11);
                    tr_catg4.Cells.Add(tc_catg_namee11);
                    DGVFFO.Rows.Add(tr_catg4);

                    tc_catg_namee11 = new TableCell();
                    lit_catg_namee11 = new Literal();
                    tc_catg_namee11.BorderStyle = BorderStyle.Solid;
                    tc_catg_namee11.BorderWidth = 1;
                    tc_catg_namee11.ColumnSpan = 1;
                    lit_catg_namee11.Text = ntval.ToString();
                    tc_catg_namee11.Attributes.Add("Class", "rptCellBorder");
                    tc_catg_namee11.HorizontalAlign = HorizontalAlign.Right;
                    tc_catg_namee11.Controls.Add(lit_catg_namee11);
                    tr_catg5.Cells.Add(tc_catg_namee11);
                    DGVFFO.Rows.Add(tr_catg5);
                }
            }


            //for (int l = 1; l <= 31; l++)
            //{

            //    tc_catg_namee1 = new TableCell();
            //    tc_catg_namee1.BorderStyle = BorderStyle.Solid;
            //    tc_catg_namee1.BorderWidth = 1;
            //    //text-align: center;

            //    tc_catg_namee1.ColumnSpan = 1;
            //    lit_catg_namee1 = new HyperLink();
            //    //lit_catg_namee.Text = i.ToString() + "day" +gg["D"].ToString();
            //    lit_catg_namee1.Text = l.ToString();
            //    // DateTime h = Convert.ToDateTime(lit_catg_namee1.Text);
            //    // g = h.ToShortDateString();
            //    // lit_catg_namee1.Text = g;
            //    //Response.Write(g);

            //    tc_catg_namee1.Attributes.Add("Class", "rptCellBorder");
            //    tc_catg_namee1.HorizontalAlign = HorizontalAlign.Center;
            //    tc_catg_namee1.Controls.Add(lit_catg_namee1);

            //    tr_catg.Cells.Add(tc_catg_namee1);
            //    DGVFFO.Rows.Add(tr_catg);
            //}

        }

        TableRow tr_tottc = new TableRow();
        TableRow tr_totec = new TableRow();
        TableRow tr_totper = new TableRow();
        TableRow tr_totqty = new TableRow();
        TableRow tr_totval = new TableRow();

        TableCell tc_tot = new TableCell();
        Literal lit_tot = new Literal();


        tc_tot = new TableCell();
        lit_tot = new Literal();
        tc_tot.BorderStyle = BorderStyle.Solid;
        tc_tot.BorderWidth = 1;
        tc_tot.ColumnSpan = 2;
        tc_tot.RowSpan = 5;
        lit_tot.Text = "Total";
        tc_tot.Attributes.Add("Class", "rptCellBorder");
        tc_tot.HorizontalAlign = HorizontalAlign.Center;
        tc_tot.Controls.Add(lit_tot);
        tr_tottc.Cells.Add(tc_tot);

        tc_tot = new TableCell();
        lit_tot = new Literal();
        tc_tot.BorderStyle = BorderStyle.Solid;
        tc_tot.BorderWidth = 1;
        tc_tot.ColumnSpan = 1;
        lit_tot.Text = "TC";
        tc_tot.Attributes.Add("Class", "rptCellBorder");
        tc_tot.HorizontalAlign = HorizontalAlign.Left;
        tc_tot.Controls.Add(lit_tot);
        tr_tottc.Cells.Add(tc_tot);


        tc_tot = new TableCell();
        lit_tot = new Literal();
        tc_tot.BorderStyle = BorderStyle.Solid;
        tc_tot.BorderWidth = 1;
        tc_tot.ColumnSpan = 1;
        lit_tot.Text = "PC";
        tc_tot.Attributes.Add("Class", "rptCellBorder");
        tc_tot.HorizontalAlign = HorizontalAlign.Left;
        tc_tot.Controls.Add(lit_tot);
        tr_totec.Cells.Add(tc_tot);



        tc_tot = new TableCell();
        lit_tot = new Literal();
        tc_tot.BorderStyle = BorderStyle.Solid;
        tc_tot.BorderWidth = 1;
        tc_tot.ColumnSpan = 1;
        lit_tot.Text = "PER";
        tc_tot.Attributes.Add("Class", "rptCellBorder");
        tc_tot.HorizontalAlign = HorizontalAlign.Left;
        tc_tot.Controls.Add(lit_tot);
        tr_totper.Cells.Add(tc_tot);


        tc_tot = new TableCell();
        lit_tot = new Literal();
        tc_tot.BorderStyle = BorderStyle.Solid;
        tc_tot.BorderWidth = 1;
        tc_tot.ColumnSpan = 1;
        lit_tot.Text = "QTY";
        tc_tot.Attributes.Add("Class", "rptCellBorder");
        tc_tot.HorizontalAlign = HorizontalAlign.Left;
        tc_tot.Controls.Add(lit_tot);
        tr_totqty.Cells.Add(tc_tot);

        tc_tot = new TableCell();
        lit_tot = new Literal();
        tc_tot.BorderStyle = BorderStyle.Solid;
        tc_tot.BorderWidth = 1;
        tc_tot.ColumnSpan = 1;
        lit_tot.Text = "VALUE";
        tc_tot.Attributes.Add("Class", "rptCellBorder");
        tc_tot.HorizontalAlign = HorizontalAlign.Left;
        tc_tot.Controls.Add(lit_tot);
        tr_totval.Cells.Add(tc_tot);

        int otottc = 0;
        int ototpc = 0;
        int ototqty = 0;
        decimal ototval = 0;
        int days = 0;
        for (int i = 1; i < wee; i++)
        {
            mfi = new System.Globalization.DateTimeFormatInfo();
            strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString();
            DateTime dt = new DateTime(Convert.ToInt32(FYear), Convert.ToInt32(FMonth), 1); // create the start date of the month and year

            int days12 = DateTime.DaysInMonth(Convert.ToInt32(FYear), Convert.ToInt32(FMonth)); 
            DayOfWeek firstDayOfWeekofMonth = dt.DayOfWeek; // Find out the day of week for that date
            int myWeekNumInMonth = i; // You want the 4th week, this may cross over to the following month!
            string myDayOfWeek = firstDayOfWeekofMonth.ToString();  // You want Wednesday
            int myDayOfWeekInt = day2Int(myDayOfWeek);
            int diff = myDayOfWeekInt - (int)firstDayOfWeekofMonth;
           // string g = (dt.AddDays(7 * (myWeekNumInMonth - 1) + diff)).ToString();



            string g = "";

            if ((7 * (myWeekNumInMonth - 1)) < days12)
            {
                g = (dt.AddDays(7 * (myWeekNumInMonth - 1) + diff)).ToString();
            }
            else
            {
                g = (dt.AddDays(days12 - 1)).ToString();
            }


            DateTime h = Convert.ToDateTime(g);
            string date = h.ToString("MM-dd-yyyy");
            SalesForce se = new SalesForce();
            getweekno = se.get_nth_week(date);
            gw = Convert.ToInt32(getweekno.Tables[0].Rows[0].ItemArray.GetValue(0));
            getweekdays = SF.get_weekdays(gw, Convert.ToInt32(FYear), Convert.ToInt32(FMonth));
            int wee12 = Convert.ToInt32(getweekdays.Tables[0].Rows[0].ItemArray.GetValue(0));
            int tTC = 0;
            int tPC = 0;
            int tqty = 0;
            decimal tval = 0;
            decimal ttper = 0;
            decimal tcper = 0; dt = new DateTime(Convert.ToInt32(FYear), Convert.ToInt32(FMonth), 1);
            for (int l = 1; l <= wee12; l++)
            {
                if (FMonth.ToString() == dt.Month.ToString())
                {
                    tc_tot = new TableCell();
                    lit_tot = new Literal();
                    tc_tot.BorderStyle = BorderStyle.Solid;
                    tc_tot.BorderWidth = 1;
                    tc_tot.ColumnSpan = 1;
                    if (days < oarr.Length)
                    {
                        lit_tot.Text = oarr[days].ToString();
                        tTC += oarr[days];
                        otottc += oarr[days];
                    }
                    else {

                        lit_tot.Text = "0";
                        tTC += 0;
                        otottc += 0;
                    }
                    tc_tot.Attributes.Add("Class", "rptCellBorder");
                    tc_tot.HorizontalAlign = HorizontalAlign.Right;
                    tc_tot.Controls.Add(lit_tot);
                    tr_tottc.Cells.Add(tc_tot);



                    tc_tot = new TableCell();
                    lit_tot = new Literal();
                    tc_tot.BorderStyle = BorderStyle.Solid;
                    tc_tot.BorderWidth = 1;
                    tc_tot.ColumnSpan = 1;
                    if (days < oarrec.Length)
                    {
                        lit_tot.Text = oarrec[days].ToString();
                        tPC += oarrec[days];
                        ototpc += oarrec[days];
                    }
                    else
                    {

                        lit_tot.Text = "0";
                        tPC += 0;
                        ototpc += 0;
                    }


                    tc_tot.Attributes.Add("Class", "rptCellBorder");
                    tc_tot.HorizontalAlign = HorizontalAlign.Right;
                    tc_tot.Controls.Add(lit_tot);
                    tr_totec.Cells.Add(tc_tot);

                    tc_tot = new TableCell();
                    lit_tot = new Literal();
                    tc_tot.BorderStyle = BorderStyle.Solid;
                    tc_tot.BorderWidth = 1;
                    tc_tot.ColumnSpan = 1;
                    if (days < oarrec.Length)
                    {
                        decimal thh = (oarr[days] == 0 ? 1 : oarr[days]);
                        lit_tot.Text = ((oarrec[days] / thh) * 100).ToString("0.00");
                    }
                    else {
                        lit_tot.Text = "0";
                    }
                    tc_tot.Attributes.Add("Class", "rptCellBorder");
                    tc_tot.HorizontalAlign = HorizontalAlign.Right;
                    tc_tot.Controls.Add(lit_tot);
                    tr_totper.Cells.Add(tc_tot);


                    tc_tot = new TableCell();
                    lit_tot = new Literal();
                    tc_tot.BorderStyle = BorderStyle.Solid;
                    tc_tot.BorderWidth = 1;
                    tc_tot.ColumnSpan = 1;
                    if (days < oarrqty.Length)
                    {
                        lit_tot.Text = oarrqty[days].ToString();

                        tqty += oarrqty[days];
                        ototqty += oarrqty[days];
                    }
                    else {

                        lit_tot.Text = "0";

                        tqty += 0;
                        ototqty += 0;
                    }
                    tc_tot.Attributes.Add("Class", "rptCellBorder");
                    tc_tot.HorizontalAlign = HorizontalAlign.Right;
                    tc_tot.Controls.Add(lit_tot);
                    tr_totqty.Cells.Add(tc_tot);


                    tc_tot = new TableCell();
                    lit_tot = new Literal();
                    tc_tot.BorderStyle = BorderStyle.Solid;
                    tc_tot.BorderWidth = 1;
                    tc_tot.ColumnSpan = 1;
                    if (days < oarrval.Length)
                    {
                        lit_tot.Text = oarrval[days].ToString();

                        tval += oarrval[days];
                        ototval += oarrval[days];
                    }
                    else
                    {

                        lit_tot.Text = "0";

                        tqty += 0;
                        ototqty += 0;
                    }
                    
                    tc_tot.Attributes.Add("Class", "rptCellBorder");
                    tc_tot.HorizontalAlign = HorizontalAlign.Right;
                    tc_tot.Controls.Add(lit_tot);
                    tr_totval.Cells.Add(tc_tot);
                }
                dt = dt.AddDays(1);
                days++;
            }

            tc_tot = new TableCell();
            lit_tot = new Literal();
            tc_tot.BorderStyle = BorderStyle.Solid;
            tc_tot.BorderWidth = 1;
            tc_tot.ColumnSpan = 1;
            lit_tot.Text = tTC.ToString();
            tc_tot.Attributes.Add("Class", "rptCellBorder");
            tc_tot.HorizontalAlign = HorizontalAlign.Right;
            tc_tot.Controls.Add(lit_tot);
            tr_tottc.Cells.Add(tc_tot);

            tc_tot = new TableCell();
            lit_tot = new Literal();
            tc_tot.BorderStyle = BorderStyle.Solid;
            tc_tot.BorderWidth = 1;
            tc_tot.ColumnSpan = 1;
            lit_tot.Text = tPC.ToString();
            tc_tot.Attributes.Add("Class", "rptCellBorder");
            tc_tot.HorizontalAlign = HorizontalAlign.Right;
            tc_tot.Controls.Add(lit_tot);
            tr_totec.Cells.Add(tc_tot);


            tc_tot = new TableCell();
            lit_tot = new Literal();
            tc_tot.BorderStyle = BorderStyle.Solid;
            tc_tot.BorderWidth = 1;
            tc_tot.ColumnSpan = 1;

            decimal hq = (tTC == 0 ? 1 : tTC);
            lit_tot.Text = ((tPC / hq) * 100).ToString("0.00");          
            tc_tot.Attributes.Add("Class", "rptCellBorder");
            tc_tot.HorizontalAlign = HorizontalAlign.Right;
            tc_tot.Controls.Add(lit_tot);
            tr_totper.Cells.Add(tc_tot);


            tc_tot = new TableCell();
            lit_tot = new Literal();
            tc_tot.BorderStyle = BorderStyle.Solid;
            tc_tot.BorderWidth = 1;
            tc_tot.ColumnSpan = 1;
            lit_tot.Text = tval.ToString();
            tc_tot.Attributes.Add("Class", "rptCellBorder");
            tc_tot.HorizontalAlign = HorizontalAlign.Right;
            tc_tot.Controls.Add(lit_tot);
            tr_totqty.Cells.Add(tc_tot);

            tc_tot = new TableCell();
            lit_tot = new Literal();
            tc_tot.BorderStyle = BorderStyle.Solid;
            tc_tot.BorderWidth = 1;
            tc_tot.ColumnSpan = 1;
            lit_tot.Text = tqty.ToString();
            tc_tot.Attributes.Add("Class", "rptCellBorder");
            tc_tot.HorizontalAlign = HorizontalAlign.Right;
            tc_tot.Controls.Add(lit_tot);
            tr_totval.Cells.Add(tc_tot);
        }


        tc_tot = new TableCell();
        lit_tot = new Literal();
        tc_tot.BorderStyle = BorderStyle.Solid;
        tc_tot.BorderWidth = 1;
        tc_tot.ColumnSpan = 1;
        lit_tot.Text = otottc.ToString();
        tc_tot.Attributes.Add("Class", "rptCellBorder");
        tc_tot.HorizontalAlign = HorizontalAlign.Right;
        tc_tot.Controls.Add(lit_tot);
        tr_tottc.Cells.Add(tc_tot);

        tc_tot = new TableCell();
        lit_tot = new Literal();
        tc_tot.BorderStyle = BorderStyle.Solid;
        tc_tot.BorderWidth = 1;
        tc_tot.ColumnSpan = 1;
        lit_tot.Text = ototpc.ToString();
        tc_tot.Attributes.Add("Class", "rptCellBorder");
        tc_tot.HorizontalAlign = HorizontalAlign.Right;
        tc_tot.Controls.Add(lit_tot);
        tr_totec.Cells.Add(tc_tot);


        tc_tot = new TableCell();
        lit_tot = new Literal();
        tc_tot.BorderStyle = BorderStyle.Solid;
        tc_tot.BorderWidth = 1;
        tc_tot.ColumnSpan = 1;
        decimal hh = (otottc == 0 ? 1 : otottc);
        lit_tot.Text = ((ototpc / hh) * 100).ToString("0.00");
        tc_tot.Attributes.Add("Class", "rptCellBorder");
        tc_tot.HorizontalAlign = HorizontalAlign.Right;
        tc_tot.Controls.Add(lit_tot);
        tr_totper.Cells.Add(tc_tot);


        tc_tot = new TableCell();
        lit_tot = new Literal();
        tc_tot.BorderStyle = BorderStyle.Solid;
        tc_tot.BorderWidth = 1;
        tc_tot.ColumnSpan = 1;
        lit_tot.Text = ototqty.ToString();
        tc_tot.Attributes.Add("Class", "rptCellBorder");
        tc_tot.HorizontalAlign = HorizontalAlign.Right;
        tc_tot.Controls.Add(lit_tot);
        tr_totqty.Cells.Add(tc_tot);


        tc_tot = new TableCell();
        lit_tot = new Literal();
        tc_tot.BorderStyle = BorderStyle.Solid;
        tc_tot.BorderWidth = 1;
        tc_tot.ColumnSpan = 1;
        lit_tot.Text = ototval.ToString();
        tc_tot.Attributes.Add("Class", "rptCellBorder");
        tc_tot.HorizontalAlign = HorizontalAlign.Right;
        tc_tot.Controls.Add(lit_tot);
        tr_totval.Cells.Add(tc_tot);

        DGVFFO.Rows.Add(tr_tottc);
        DGVFFO.Rows.Add(tr_totec);
        DGVFFO.Rows.Add(tr_totper);
        DGVFFO.Rows.Add(tr_totqty);
        DGVFFO.Rows.Add(tr_totval);

    }

    public class ReportingSF
    {
        public string Sf_Code { get; set; }
        public string sf_Name { get; set; }
        public string RSF_Code { get; set; }
        public string Designation { get; set; }
        public string tCount { get; set; }
        public string eCount { get; set; }
        public string tDay { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static ReportingSF[] GetReportingToSF(string sfCode)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        SalesForce sf = new SalesForce();
        DataSet dsSFHead = new DataSet();
        dsSFHead = sf.SalesForceListMgrGet_MgrOnly(div_code, sfCode, "0");
        List<ReportingSF> dsDtls = new List<ReportingSF>();
        foreach (DataRow row in dsSFHead.Tables[0].Rows)
        {
            ReportingSF dtls = new ReportingSF();
            dtls.Sf_Code = row["Sf_Code"].ToString();
            dtls.sf_Name = row["Sf_Name"].ToString();
            dtls.RSF_Code = row["Reporting_To_SF"].ToString();
            dtls.Designation = row["Designation"].ToString();

            dsDtls.Add(dtls);
        }
        return dsDtls.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static ReportingSF[] GetTCECdayCall(string sfCode, string FYear, string FMonth, string SubDivCode)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        Order ord = new Order();
        DataSet dsSFHead = new DataSet();
        dsSFHead = ord.GetIssuSlipDayTCEC(div_code, sfCode, FYear, FMonth, "0");
        List<ReportingSF> dsDtls = new List<ReportingSF>();
        foreach (DataRow row in dsSFHead.Tables[0].Rows)
        {
            ReportingSF dtls = new ReportingSF();
            dtls.Sf_Code = row["sf"].ToString();
            dtls.sf_Name = row["sf_name"].ToString();
            dtls.RSF_Code = row["Reporting_To_SF"].ToString();
            dtls.tCount = row["tc_count"].ToString();
            dtls.eCount = row["ec_count"].ToString();
            dtls.tDay = row["day1"].ToString();
            dsDtls.Add(dtls);
        }
        return dsDtls.ToArray();
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
}