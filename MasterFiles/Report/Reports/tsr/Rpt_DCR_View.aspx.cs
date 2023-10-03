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
using System.Xml;
using System.Xml.XPath;
using System.Net;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Globalization;

public partial class MasterFiles_Reports_tsr_Rpt_DCR_View : System.Web.UI.Page
{
    DataTable tbl1 = null;
    DataSet dsSalesForce = null;
    DataSet dcrcou = null;
    DataSet dsDCR = null;
    DataSet dsDrr = null;
    DataSet dsTerritory = null;
    DataSet dsTerritory1 = null;
    DataSet dsdoc = null;
    DataSet dsdoc1 = null;
    DataSet dssf = null;
    decimal detorderval = 0;
    decimal detnetval = 0;
    string div_code = string.Empty;
    string strDelay = string.Empty;
    string sf_code = string.Empty;
    string Sf_Name = string.Empty;
    string strMode = string.Empty;
    string sURL = string.Empty;
    string Sf_HQ = string.Empty;
    string Fdate = string.Empty;
    string Tdate = string.Empty;
    string stURL = string.Empty;
    int cmonth = -1;
    int cyear = -1;
    int tot_days = -1;
    int cday = 1;
    int iCount = -1;
    int iFieldWrkCount = -1;
    string sDCR = string.Empty;
    Decimal iTotLstCount = 0;
    Decimal iTotLstCountt;
    Decimal Tot_Sec = 0m;
    string dt = string.Empty;
    string dt1 = string.Empty;
    string dtt = string.Empty;
    string dtt1 = string.Empty;
    string sMonth = string.Empty;
    public static string distcode = string.Empty;
    public static string distnm = string.Empty;
    public static string stcode = string.Empty;
    public static string stnm = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        Sf_Name = Request.QueryString["Sf_Name"].ToString();
        cmonth = 0;
        cyear = 0;
        distcode = Request.QueryString["Dst_code"].ToString();
        distnm = Request.QueryString["Dst_name"].ToString();
        stnm = Request.QueryString["st_name"].ToString();
        stcode = Request.QueryString["st_code"].ToString();
        strMode = Request.QueryString["Mode"].ToString();
        strMode = strMode.Trim();
        sMonth = getMonthName(cmonth) + " - " + cyear.ToString();
        if (strMode.Trim() == "New_View_All_DCR_Date(s)" && cmonth == 0 && cyear == 0)
        {
            Fdate = Request.QueryString["FDate"].ToString();
            Tdate = (Request.QueryString["TDate"].ToString() == "") ? Fdate : Request.QueryString["TDate"].ToString();

            //string sMonth = getMonthName(cmonth) + " - " + cyear.ToString();
            dt = Fdate;
            dt1 = Tdate;

            try
            {
                DateTime result = DateTime.ParseExact(Fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Fdate = result.ToString("yyyy-MM-dd");


                DateTime result1 = DateTime.ParseExact(Tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Tdate = result1.ToString("yyyy-MM-dd");


            }
            catch (Exception ex)
            {
            }

        }
        else
        {
            Fdate = Request.QueryString["FDate"].ToString();
            Tdate = (Request.QueryString["TDate"].ToString() == "") ? Fdate : Request.QueryString["TDate"].ToString();



            try
            {
                DateTime result = DateTime.ParseExact(Fdate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                dt = result.ToString("dd/MM/yyyy");

                DateTime result1 = DateTime.ParseExact(Tdate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                dt1 = result1.ToString("dd/MM/yyyy");


            }
            catch (Exception ex)
            {
            }

        }

        if (strMode.Trim() == "View All DCR Date(s)")
        {
            SalesForce sf1 = new SalesForce();
            DataSet dssf1 = sf1.getFW(sf_code, div_code);
            if (dssf1.Tables[0].Rows.Count > 0)
            {
                if ("NS" == dssf1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() || "DH" == dssf1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
                {
                    //DataSet dsGV = new DataSet();
                    //SalesForce dc = new SalesForce();
                    //Label1.Text = "Daily Call Report (<span style='color:Red'>" + "all Dates" + "</span>) view for the month of " + sMonth;
                    //dsGV = dc.GetTable(sf_code, div_code);
                    //if (dsGV.Tables[0].Rows.Count > 0)
                    //{
                    //    GridView1.DataSource = dsGV;
                    //    GridView1.DataBind();
                    //}
                    //else
                    //{
                    lblTitle.Text = "Daily Call Report (<span style='color:Red'>" + "From :'" + dt + "' To :'" + dt1 + "'" + "</span>) view ";
                    CreateDynamicTableDCRDate(Fdate, Tdate, sf_code);
                    //FillSalesForce(sf_code, cmonth, cyear);
                    SalesForce sf = new SalesForce();
                    DataSet dssf = sf.getSfName(sf_code);
                    if (dssf.Tables[0].Rows.Count > 0)
                    {
                        lblHead.Text = lblHead.Text + Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) + "  -  ";
                    }
                    //lblHead.Text = lblHead.Text + sMonth;
                    lblHead.Visible = false;
                    //}

                }

                else
                {
                    lblTitle.Text = "Daily Call Report (<span style='color:Red'>" + "From :'" + dt + "' To :'" + dt1 + "'" + "</span>) view ";
                    CreateDynamicTableDCRDate(Fdate, Tdate, sf_code);
                    //FillSalesForce(sf_code, cmonth, cyear);
                    SalesForce sf = new SalesForce();
                    DataSet dssf = sf.getSfName(sf_code);
                    if (dssf.Tables[0].Rows.Count > 0)
                    {
                        lblHead.Text = lblHead.Text + Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) + "  -  ";
                    }
                    //lblHead.Text = lblHead.Text + sMonth;
                    lblHead.Visible = false;
                }
            }
            else
            {
                lblTitle.Text = "Daily Call Report (<span style='color:Red'>" + "From :'" + dt + "' To :'" + dt1 + "'" + "</span>) view ";
                CreateDynamicTableDCRDate(Fdate, Tdate, sf_code);
                //FillSalesForce(sf_code, cmonth, cyear);
                SalesForce sf = new SalesForce();
                DataSet dssf = sf.getSfName(sf_code);
                if (dssf.Tables[0].Rows.Count > 0)
                {
                    lblHead.Text = lblHead.Text + Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) + "  -  ";
                }
                //lblHead.Text = lblHead.Text + sMonth;
                lblHead.Visible = false;
            }
        }
        else if (strMode == "View All Remark(s)")
        {

            //sURL = "rptRemarks.aspx?sf_Name=" + Sf_Name + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "";
            //string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=600,left=0,top=0');";
            //ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            SalesForce sf = new SalesForce();
            DataSet dssf = sf.getSfName(sf_code);
            if (dssf.Tables[0].Rows.Count > 0)
            {
                lblHead.Text = lblHead.Text + Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) + "  -  ";
            }
            lblHead.Text = lblHead.Text + sMonth;
            //ClientScript.RegisterStartupScript(GetType(), "Rpt_DCR_View.aspx", "<Script>self.close();</Script>");//code to close window
        }
        else if (strMode == "View All DCR Doctor(s)")
        {
            lblTitle.Text = "Daily Call Report (<span style='color:Red'>" + "Field Work only" + "</span>) view for the month of " + sMonth;
            CreateDynamicDCRDoctors(cmonth, cyear, sf_code);
        }
        else if (strMode == "Not Approved DCR Dates")
        {
            lblTitle.Text = "Daily Call Report (<span style='color:Red'>" + "Not Approval Days" + "</span>) view for the month of " + sMonth;
            CreateDynamicDCRPendingApproval(cmonth, cyear, sf_code);
            //lblHead.Text = lblHead.Text + sMonth;
            lblHead.Visible = false;
        }
        else if (strMode == "View All Listed Doctor Remark(s)")
        {
            lblTitle.Text = "Daily Call Report (<span style='color:Red'>" + "Listed Doctorwise remarks" + "</span>) view for the month of " + sMonth;
            CreateDynamicDCRViewListedDoctorRemarks(cmonth, cyear, sf_code);
            //lblHead.Text = "Listed Doctorwise remarks For The Month Of " + sMonth ;
            lblHead.Visible = false;
        }
        else if (strMode == "Detailed View")
        {
            lblTitle.Text = "Daily Call Report (<span style='color:Red'>" + "From :" + dt + " To :" + dt1 + "" + "</span>) Detail View ";
            CreateDynamicDCRDetailedView(Fdate, Tdate, sf_code);
            //lblHead.Text = lblHead.Text + sMonth;
            lblHead.Visible = false;
        }
        else if (strMode == "Primary Detailed View")
        {
            lblTitle.Text = "Daily Call Report (<span style='color:Red'>" + "From :" + dt + " To :" + dt1 + "" + "</span>) Primary Detail View ";
            CreateDynamicDCRDetailedView_primary(Fdate, Tdate, sf_code);
            //lblHead.Text = lblHead.Text + sMonth;
            lblHead.Visible = false;
        }
        else if (strMode == "TP MY Day Plan")
        {
            lblTitle.Text = "Daily Call Report (<span style='color:Red'>" + "From :" + dt + " To :" + dt1 + "" + "</span>) Plan View ";
            //lblFieldForceName.Text = Sf_Name;
            DataSet dsGV = new DataSet();
            DCR dc = new DCR();
            dsGV = dc.GetTPDayMap_MR(div_code, sf_code, Fdate, Tdate, stcode, distcode);
            if (dsGV.Tables[0].Rows.Count > 0)
            {
                gvMyDayPlan.DataSource = dsGV;
                gvMyDayPlan.DataBind();
            }
            else
            {
                gvMyDayPlan.DataSource = null;
                gvMyDayPlan.DataBind();
            }

            lblHead.Visible = false;
        }

        else if (strMode == "Closing Stock View")
        {
            lblTitle.Text = "Closing Report (<span style='color:Red'>" + "From :" + dt + " To :" + dt1 + "" + " Stock View" + "</span>)";
            //lblTitle.Text = "Closing Report (<span style='color:Red'>" + "Stock View" + "</span>) for the month of " + sMonth;
            //lblFieldForceName.Text = Sf_Name;
            Feild.Text = Sf_Name;
            DataSet dsGV = new DataSet();
            DCR dc = new DCR();

            TemplateField temp1 = new TemplateField();  //Create instance of Template field
            //temp1.HeaderText = "New Dynamic Temp Field"; //Give the header text

            temp1.ItemTemplate = new DynamicTemplateField(); //Set the properties **ItemTemplate** as the instance of DynamicTemplateField class.

            //dsGV = dc.Get_Close_stock_MR(sf_code, div_code, cmonth, cyear);
            dsGV = dc.Get_Close_stock_MR(sf_code, div_code, Fdate, Tdate, stcode, distcode);
            if (dsGV.Tables[0].Rows.Count > 0)
            {
                dsGV.Tables[0].Columns.RemoveAt(2);
                dsGV.Tables[0].Columns.RemoveAt(2);
                for (int i = 0; i < dsGV.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < dsGV.Tables[0].Columns.Count; j++)
                    {
                        if (string.IsNullOrEmpty(dsGV.Tables[0].Rows[i][j].ToString()))
                        {

                            dsGV.Tables[0].Rows[i][j] = "0";

                        }
                        else
                        {
                            if (j > 4 && j < dsGV.Tables[0].Columns.Count - 1)
                                dsGV.Tables[0].Rows[i][j] = Math.Round(Convert.ToDecimal(dsGV.Tables[0].Rows[i][j]), 4).ToString();


                        }

                    }
                }



                if (dsGV.Tables[0].Rows.Count > 0)
                {

                    //int kuy = dsGV.Tables[0].Rows.Count;
                    GridView2.Columns.Add(temp1);
                    GridView2.DataSource = dsGV;
                    GridView2.DataBind();
                    GridView2.FooterRow.Cells[3].Text = "TOTAL";
                    GridView2.FooterRow.Cells[3].Font.Bold = true;
                    GridView2.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Center;

                    for (int k = 4; k < dsGV.Tables[0].Columns.Count - 1; k++)
                    {
                        // Int32 td = (tbl1.Columns[k] == null ) ? 0 : Convert.ToInt32(tbl1.Columns[k].ToString());



                        string total = "0";

                        try
                        {

                            total = dsGV.Tables[0].AsEnumerable().Sum(x => Convert.ToDouble(x.Field<string>(dsGV.Tables[0].Columns[k].ToString()) == null ? 0 : Convert.ToDouble(x.Field<string>(dsGV.Tables[0].Columns[k].ToString())))).ToString();
                        }
                        catch
                        {
                            //total = dsGV.Tables[0].AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<Int32>(dsGV.Tables[0].Columns[k].ToString()) == null ? 0 : Convert.ToDecimal(x.Field<Int32>(dsGV.Tables[0].Columns[k].ToString())))).ToString();
                        }
                        //total = dsGV.Tables[0].AsEnumerable().Sum(x => Convert.ToDouble(x.Field<Double>(dsGV.Tables[0].Columns[k].ToString()) == null ? 0 : Convert.ToDouble(x.Field<Double>(dsGV.Tables[0].Columns[k].ToString())))).ToString();


                        //string total = tbl1.AsEnumerable().Sum(x => Convert.To(x.Field<string>(tbl1.Columns[k].ToString()))).ToString();

                        GridView2.FooterRow.Cells[k + 1].Font.Bold = true;
                        GridView2.FooterRow.Cells[k + 1].HorizontalAlign = HorizontalAlign.Left;
                        GridView2.FooterRow.Cells[k + 1].Text = total.ToString();
                        GridView2.FooterRow.Cells[k + 1].Font.Bold = true;
                        GridView2.FooterRow.BackColor = System.Drawing.Color.Beige;
                    }


                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }
            }



            lblHead.Visible = false;

        }


        else if (strMode.Trim() == "New_View_All_DCR_Date(s)")
        {
            Feild.Text = Sf_Name;
            SalesForce sf1 = new SalesForce();
            DataSet dssf1 = sf1.getFW(sf_code, div_code);
            if (dssf1.Tables[0].Rows.Count > 0)
            {
                if ("NS" == dssf1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
                {
                    DataSet dsGV = new DataSet();
                    SalesForce dc = new SalesForce();
                    Label1.Text = "Daily Call Report (<span style='color:Red'>" + "all Dates" + "</span>) view for the month of " + sMonth;
                    dsGV = dc.GetTable(sf_code, div_code);
                    if (dsGV.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = dsGV;
                        GridView1.DataBind();
                    }
                    else
                    {
                        lblTitle.Text = "Daily Call Report (<span style='color:Red'>" + " - From :'" + dt + "' To :'" + dt1 + "'" + "</span>)";
                        CreateDynamicTableDCRDate1(Fdate, Tdate, sf_code);
                        //FillSalesForce(sf_code, cmonth, cyear);
                        SalesForce sf = new SalesForce();
                        DataSet dssf = sf.getSfName(sf_code);
                        if (dssf.Tables[0].Rows.Count > 0)
                        {
                            lblHead.Text = lblHead.Text + Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) + "  -  ";
                        }
                        //lblHead.Text = lblHead.Text + sMonth;
                        lblHead.Visible = false;
                    }

                }

                else
                {
                    lblTitle.Text = "Daily Call Report -" + "From :<span style='color:Red'>" + dt + " To :" + dt1 + "" + "</span>)";
                    CreateDynamicTableDCRDate1(Fdate, Tdate, sf_code);
                    //FillSalesForce(sf_code, cmonth, cyear);
                    SalesForce sf = new SalesForce();
                    DataSet dssf = sf.getSfName(sf_code);
                    if (dssf.Tables[0].Rows.Count > 0)
                    {
                        lblHead.Text = lblHead.Text + Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) + "  -  ";
                    }
                    //lblHead.Text = lblHead.Text + sMonth;
                    lblHead.Visible = false;
                }
            }
            else
            {
                lblTitle.Text = "<h1>Daily Call Report - " + " From : <span style='color:Red'> " + dt + " </span> " + " To : <span style='color:Red'> " + dt1 + " </span> </h1>";
                CreateDynamicTableDCRDate1(Fdate, Tdate, sf_code);
                //FillSalesForce(sf_code, cmonth, cyear);
                SalesForce sf = new SalesForce();
                DataSet dssf = sf.getSfName(sf_code);
                if (dssf.Tables[0].Rows.Count > 0)
                {
                    lblHead.Text = lblHead.Text + Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) + "  -  ";
                }
                //lblHead.Text = lblHead.Text + sMonth;
                lblHead.Visible = false;
            }
        }
        //sky summery

        else if (strMode == "SKU Summary")
        {

            lblTitle.Text = "<h1>SKU Summary Report - " + " Date : <span style='color:Red'> " + dt + " </span>  </h1>";
            Feild.Text = Sf_Name;
            DCRdt.Value = Fdate.ToString();
            DataSet dsGV = new DataSet();
            DCR dc = new DCR();
            if (sf_code.Contains("MGR"))
            {

                //    DCR dcsf = new DCR();
                //dssf = dcsf.getSfName_HQ(sf_code);

                //if (dssf.Tables[0].Rows.Count > 0)
                //{
                //    Sf_Name = dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //    Sf_HQ = dssf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                //}

                //Table tbldetail_main3 = new Table();
                ////tbldetail_main3.BorderStyle = BorderStyle.None;
                ////tbldetail_main3.Width = 1100;
                //TableRow tr_det_head_main3 = new TableRow();
                ////TableCell tc_det_head_main3 = new TableCell();
                ////tc_det_head_main3.Width = 100;
                ////Literal lit_det_main3 = new Literal();
                ////lit_det_main3.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                ////tc_det_head_main3.Controls.Add(lit_det_main3);
                ////tr_det_head_main3.Cells.Add(tc_det_head_main3);

                //TableCell tc_det_head_main4 = new TableCell();
                ////tc_det_head_main4.Width = 1000;

                //Table tbl = new Table();
                ////tbl.Width = 1000;
                //tbl.Style.Add("width", "185%");

                //TableRow tr_day = new TableRow();
                //TableCell tc_day = new TableCell();
                //tc_day.BorderStyle = BorderStyle.None;
                //tc_day.ColumnSpan = 2;
                //tc_day.HorizontalAlign = HorizontalAlign.Center;
                //tc_day.Style.Add("font-name", "verdana;");
                //Literal lit_day = new Literal();
                //tc_day.Controls.Add(lit_day);
                //tr_day.Cells.Add(tc_day);
                //tbl.Rows.Add(tr_day);
                //tc_det_head_main4.Controls.Add(tbl);
                //tr_det_head_main3.Cells.Add(tc_det_head_main4);
                //tbldetail_main3.Rows.Add(tr_det_head_main3);

                //form1.Controls.Add(tbldetail_main3);

                //TableRow tr_ff = new TableRow();

                //TableCell tc_ff_name = new TableCell();
                //tc_ff_name.BorderStyle = BorderStyle.None;
                //tc_ff_name.Width = 500;
                //Literal lit_ff_name = new Literal();
                ////lit_ff_name.Text = "<b>Field Force Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "<b>" + Sf_Name.ToString() + "</b>";
                //TableCell1.Text = "<b>Field Force Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "<b>" + Sf_Name.ToString() + "</b>";
                //tc_ff_name.Controls.Add(lit_ff_name);
                //tr_ff.Cells.Add(tc_ff_name);

                //TableCell tc_HQ = new TableCell();
                //tc_HQ.BorderStyle = BorderStyle.None;
                //tc_HQ.Width = 500;

                //tc_HQ.HorizontalAlign = HorizontalAlign.Left;
                //Literal lit_HQ = new Literal();
                ////lit_HQ.Text = "<span style='margin-left:200px'><b>Head Quarters </b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + "<b>" + Sf_HQ.ToString() + "</b></span>";
                //TableCell2.Text = "<b>Head Quarters</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "<b>" + Sf_HQ.ToString() + "</b></span>";
                //tc_HQ.Controls.Add(lit_HQ);
                //tr_ff.Cells.Add(tc_HQ);
                //tbl.Rows.Add(tr_ff);

                //TableRow tr_ff1 = new TableRow();
                //TableCell tc_ff_name1 = new TableCell();
                //tc_ff_name1.BorderStyle = BorderStyle.None;
                //tc_ff_name1.Width = 500;
                //Literal lit_ff_name1 = new Literal();

                //// lit_ff_name1.Text = "<b>Daily Call Report</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<span style='color:Red'><b>" + drdoc["Activity_Date"].ToString() + "</b></span>";
                ////TableCell4.Text = "<b>Daily Call Report</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<span style='color:Red'><b>" + Fdate.ToString() + "</b></span>";
                //tc_ff_name1.Controls.Add(lit_ff_name1);
                //tr_ff1.Cells.Add(tc_ff_name1);

                //TableCell tc_HQ1 = new TableCell();
                //tc_HQ1.BorderStyle = BorderStyle.None;
                //tc_HQ1.Width = 500;

                //tc_HQ1.HorizontalAlign = HorizontalAlign.Left;
                //Literal lit_HQ1 = new Literal();
                ////lit_HQ1.Text = "<span style='margin-left:200px'><b>Submitted on </b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + "<b>" + drdoc["Submission_Date"].ToString() + "</b></span>";
                ////TableCell5.Text = "<b>Submitted on</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "<b>" + Fdate.ToString() + "</b></span>";
                //tc_HQ1.Controls.Add(lit_HQ1);
                //tr_ff1.Cells.Add(tc_HQ1);
                //tbl.Rows.Add(tr_ff1);

                //TableRow tr_ff2 = new TableRow();
                //TableCell tc_ff_name2 = new TableCell();
                //tc_ff_name2.BorderStyle = BorderStyle.None;
                //tc_ff_name2.Width = 500;
                //Literal lit_ff_name2 = new Literal();
                ////lit_ff_name2.Text = "<b>DB Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dsdoc.Tables[0].Rows[0]["stockist_name"].ToString();
                ////TableCell7.Text = "<b>DB Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";//+ dsdoc.Tables[0].Rows[0]["stockist_name"].ToString();
                //tc_ff_name2.Controls.Add(lit_ff_name2);
                //tr_ff2.Cells.Add(tc_ff_name2);

                //TableCell tc_HQ2 = new TableCell();
                //tc_HQ2.BorderStyle = BorderStyle.None;
                //tc_HQ2.Width = 500;

                //tc_HQ2.HorizontalAlign = HorizontalAlign.Left;
                //Literal lit_HQ2 = new Literal();
                ////lit_HQ2.Text = "<span style='margin-left:200px'><b>Route </b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + dsdoc.Tables[0].Rows[0]["che_POB_Name"].ToString();
                ////TableCell8.Text = "<b>Route</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" ; //+ dsdoc.Tables[0].Rows[0]["che_POB_Name"].ToString();
                //tc_HQ2.Controls.Add(lit_HQ2);
                //tr_ff2.Cells.Add(tc_HQ2);
                //tbl.Rows.Add(tr_ff2);

                //tc_det_head_main4.Controls.Add(tbl);
                //tr_det_head_main3.Cells.Add(tc_det_head_main4);
                //tbldetail_main3.Rows.Add(tr_det_head_main3);

                //form1.Controls.Add(tbldetail_main3);

                string stCrtDtaPnt = string.Empty;

                //DataSet dsGV = new DataSet();
                DataSet dsGc = new DataSet();
                DCR dc1 = new DCR();
                TemplateField temp1 = new TemplateField();  //Create instance of Template field
                //temp1.HeaderText = "New Dynamic Temp Field"; //Give the header text

                temp1.ItemTemplate = new DynamicTemplateField(); //Set the properties **ItemTemplate** as the instance of DynamicTemplateField class.


                gvtotalorder.Columns.Add(temp1);

                //dsGV = dc1.view_MGR_order_viewyy(div_code, sf_code, Fdate);
                tbl1 = dc1.view_MGR_order_viewyy_tbl(div_code, sf_code, Fdate);
                //   DataTable dt = new DataTable();

                //   dt = dsGV.Tables[0];

                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    for (int j = 0; j < tbl1.Columns.Count; j++)
                    {
                        if (j > 7)
                        {
                            if (string.IsNullOrEmpty(tbl1.Rows[i][j].ToString()))
                            {

                                tbl1.Rows[i][j] = "0";
                            }
                        }
                        else
                        {
                            if (j > 8)
                                tbl1.Rows[i][j] = Math.Round(Convert.ToDecimal(tbl1.Rows[i][j]), 8).ToString();
                        }

                    }
                }




                if (tbl1.Rows.Count > 0)
                {
                    //tbl1.Columns.Add("order_val", typeof(string));
                    gvtotalorder.DataSource = tbl1;
                    gvtotalorder.DataBind();
                    //here add code for column total sum and show in footer  
                    // decimal total = 0; ;
                    gvtotalorder.FooterRow.Cells[3].Text = "TOTAL";
                    gvtotalorder.FooterRow.Cells[3].Font.Bold = true;
                    gvtotalorder.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Center;

                    for (int k = 8; k < tbl1.Columns.Count; k++)
                    {
                        // Int32 td = (tbl1.Columns[k] == null ) ? 0 : Convert.ToInt32(tbl1.Columns[k].ToString());


                        string total = "0";
                        if (tbl1.Columns[k].ToString() == "order_val")
                        {

                            total = tbl1.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<double>(tbl1.Columns[k].ToString()) == null ? 0 : Convert.ToDecimal(x.Field<double>(tbl1.Columns[k].ToString())))).ToString();

                        }
                        else if (tbl1.Columns[k].ToString() == "TC")
                        {
                            total = tbl1.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString()) == null ? 0 : Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString())))).ToString();
                        }

                        else if (tbl1.Columns[k].ToString() == "EC")
                        {
                            total = tbl1.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString()) == null ? 0 : Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString())))).ToString();
                        }

                        else if (tbl1.Columns[k].ToString() == "TPS")
                        {
                            total = tbl1.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString()) == null ? 0 : Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString())))).ToString();
                        }

                        else if (tbl1.Columns[k].ToString() == "TLS")
                        {
                            total = tbl1.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString()) == null ? 0 : Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString())))).ToString();
                        }
                        else if (tbl1.Columns[k].ToString() == "BrandEC")
                        {
                            total = tbl1.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString()) == null ? 0 : Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString())))).ToString();
                        }
                        else if (tbl1.Columns[k].ToString() == "Brandval")
                        {
                            total = tbl1.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString()) == null ? 0 : Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString())))).ToString();
                        }
                        else
                        {
                            total = tbl1.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<string>(tbl1.Columns[k].ToString()) == null ? 0 : Convert.ToDecimal(x.Field<string>(tbl1.Columns[k].ToString())))).ToString();
                        }
                        //string total = tbl1.AsEnumerable().Sum(x => Convert.To(x.Field<string>(tbl1.Columns[k].ToString()))).ToString();

                        gvtotalorder.FooterRow.Cells[k + 1].Font.Bold = true;
                        gvtotalorder.FooterRow.Cells[k + 1].HorizontalAlign = HorizontalAlign.Left;
                        gvtotalorder.FooterRow.Cells[k + 1].Text = total.ToString();
                        gvtotalorder.FooterRow.Cells[k + 1].Font.Bold = true;
                        gvtotalorder.FooterRow.BackColor = System.Drawing.Color.Beige;
                    }
                    //CreateDynamicTableDCRDate1(Fdate, Tdate, sf_code);

                }
                else
                {
                    gvtotalorder.DataSource = null;
                    gvtotalorder.DataBind();
                }


            }

            else
            {
                pnlbutton.Visible = true;

                Table tbldetail_mainEmpty = new Table();
                tbldetail_mainEmpty.BorderStyle = BorderStyle.None;
                tbldetail_mainEmpty.Width = 1100;
                TableRow tr_det_head_mainEmpty = new TableRow();

                TableCell tc_det_head_mainEmpty = new TableCell();
                tc_det_head_mainEmpty.Width = 100;
                Literal lit_det_mainEmpty = new Literal();
                lit_det_mainEmpty.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tbldetail_mainEmpty.Style.Add("margin-top", "110px");
                tc_det_head_mainEmpty.Controls.Add(lit_det_mainEmpty);
                tr_det_head_mainEmpty.Cells.Add(tc_det_head_mainEmpty);

                TableCell tc_det_head_main = new TableCell();
                tc_det_head_main.Width = 800;

                Table tbldetailEmpty = new Table();
                tbldetailEmpty.BorderStyle = BorderStyle.Solid;
                tbldetailEmpty.BorderWidth = 1;
                tbldetailEmpty.GridLines = GridLines.Both;
                tbldetailEmpty.Width = 1000;
                tbldetailEmpty.Style.Add("border-collapse", "collapse");
                tbldetailEmpty.Style.Add("border", "solid 1px Black");
                tbldetailEmpty.Style.Add("margin-left", "200px");

                TableRow tr_det_Empty = new TableRow();
                TableCell tc_det_Empty = new TableCell();
                iCount += 1;
                Literal lit_det_Empty = new Literal();
                lit_det_Empty.Text = "No Record Found";
                tc_det_Empty.BorderStyle = BorderStyle.Solid;
                tc_det_Empty.Attributes.Add("Class", "NoRecord");

                tc_det_Empty.HorizontalAlign = HorizontalAlign.Center;
                tc_det_Empty.BorderWidth = 1;
                tc_det_Empty.BorderStyle = BorderStyle.None;
                tc_det_Empty.Controls.Add(lit_det_Empty);
                tr_det_Empty.Cells.Add(tc_det_Empty);

                tbldetailEmpty.Rows.Add(tr_det_Empty);

                tc_det_head_mainEmpty.Controls.Add(tbldetailEmpty);
                tr_det_head_mainEmpty.Cells.Add(tc_det_head_mainEmpty);
                tbldetail_mainEmpty.Rows.Add(tr_det_head_mainEmpty);

                form1.Controls.Add(tbldetail_mainEmpty);
            }

            lblHead.Visible = false;
        }
        else if (strMode == "Brand SKU Summary")
        {

            lblTitle.Text = "<h1>Brand SKU Summary Report - " + " Date : <span style='color:Red'> " + dt + " </span>  </h1>";
            Feild.Text = Sf_Name;
            DCRdt.Value = Fdate.ToString();
            DataSet dsGV = new DataSet();
            DCR dc = new DCR();
            if (sf_code.Contains("MGR"))
            {
                string stCrtDtaPnt = string.Empty;

                //DataSet dsGV = new DataSet();
                DataSet dsGc = new DataSet();
                DCR dc1 = new DCR();
                TemplateField temp1 = new TemplateField();  //Create instance of Template field
                //temp1.HeaderText = "New Dynamic Temp Field"; //Give the header text

                temp1.ItemTemplate = new DynamicTemplateField1(); //Set the properties **ItemTemplate** as the instance of DynamicTemplateField class.


                GridView4.Columns.Add(temp1);

                //dsGV = dc1.view_MGR_order_viewyy(div_code, sf_code, Fdate);
                tbl1 = dc1.view_MGR_order_viewyy_tbl_Brd(div_code, sf_code, Fdate);
                //   DataTable dt = new DataTable();

                //   dt = dsGV.Tables[0];

                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    for (int j = 0; j < tbl1.Columns.Count; j++)
                    {
                        if (j > 1)
                        {
                            if (string.IsNullOrEmpty(tbl1.Rows[i][j].ToString()))
                            {

                                tbl1.Rows[i][j] = "0";
                            }
                        }
                        else
                        {
                            if (j > 2)
                                tbl1.Rows[i][j] = Math.Round(Convert.ToDecimal(tbl1.Rows[i][j]), 2).ToString();
                        }

                    }
                }




                if (tbl1.Rows.Count > 0)
                {
                    //tbl1.Columns.Add("order_val", typeof(string));
                    GridView4.DataSource = tbl1;
                    GridView4.DataBind();
                    //here add code for column total sum and show in footer  
                    // decimal total = 0; ;
                    GridView4.FooterRow.Cells[1].Text = "TOTAL";
                    GridView4.FooterRow.Cells[1].Font.Bold = true;
                    GridView4.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                    for (int k = 2; k < tbl1.Columns.Count; k++)
                    {
                        //Int32 td = (tbl1.Columns[k] == null) ? 0 : Convert.ToInt32(tbl1.Columns[k].ToString());


                        string total = "0";
                        if (tbl1.Columns[k].ToString() == "total")
                        {

                            total = tbl1.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString()) == null ? 0 : Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString())))).ToString();

                        }


                        else if (tbl1.Columns[k].ToString() == "EC")
                        {
                            total = tbl1.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString()) == null ? 0 : Convert.ToDecimal(x.Field<Int32>(tbl1.Columns[k].ToString())))).ToString();
                        }


                        else
                        {

                            total = tbl1.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<string>(tbl1.Columns[k].ToString()) == null ? 0 : Convert.ToDecimal(x.Field<string>(tbl1.Columns[k].ToString())))).ToString();
                        }
                        //string total = tbl1.AsEnumerable().Sum(x => Convert.To(x.Field<string>(tbl1.Columns[k].ToString()))).ToString();

                        GridView4.FooterRow.Cells[k + 1].Font.Bold = true;
                        GridView4.FooterRow.Cells[k + 1].HorizontalAlign = HorizontalAlign.Left;
                        GridView4.FooterRow.Cells[k + 1].Text = total.ToString();
                        GridView4.FooterRow.Cells[k + 1].Font.Bold = true;
                        GridView4.FooterRow.BackColor = System.Drawing.Color.Beige;
                    }
                    //CreateDynamicTableDCRDate1(Fdate, Tdate, sf_code);

                }
                else
                {
                    GridView4.DataSource = null;
                    GridView4.DataBind();
                }


            }

            else
            {
                pnlbutton.Visible = true;

                Table tbldetail_mainEmpty = new Table();
                tbldetail_mainEmpty.BorderStyle = BorderStyle.None;
                tbldetail_mainEmpty.Width = 1100;
                TableRow tr_det_head_mainEmpty = new TableRow();

                TableCell tc_det_head_mainEmpty = new TableCell();
                tc_det_head_mainEmpty.Width = 100;
                Literal lit_det_mainEmpty = new Literal();
                lit_det_mainEmpty.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tbldetail_mainEmpty.Style.Add("margin-top", "110px");
                tc_det_head_mainEmpty.Controls.Add(lit_det_mainEmpty);
                tr_det_head_mainEmpty.Cells.Add(tc_det_head_mainEmpty);

                TableCell tc_det_head_main = new TableCell();
                tc_det_head_main.Width = 800;

                Table tbldetailEmpty = new Table();
                tbldetailEmpty.BorderStyle = BorderStyle.Solid;
                tbldetailEmpty.BorderWidth = 1;
                tbldetailEmpty.GridLines = GridLines.Both;
                tbldetailEmpty.Width = 1000;
                tbldetailEmpty.Style.Add("border-collapse", "collapse");
                tbldetailEmpty.Style.Add("border", "solid 1px Black");
                tbldetailEmpty.Style.Add("margin-left", "200px");

                TableRow tr_det_Empty = new TableRow();
                TableCell tc_det_Empty = new TableCell();
                iCount += 1;
                Literal lit_det_Empty = new Literal();
                lit_det_Empty.Text = "No Record Found";
                tc_det_Empty.BorderStyle = BorderStyle.Solid;
                tc_det_Empty.Attributes.Add("Class", "NoRecord");

                tc_det_Empty.HorizontalAlign = HorizontalAlign.Center;
                tc_det_Empty.BorderWidth = 1;
                tc_det_Empty.BorderStyle = BorderStyle.None;
                tc_det_Empty.Controls.Add(lit_det_Empty);
                tr_det_Empty.Cells.Add(tc_det_Empty);

                tbldetailEmpty.Rows.Add(tr_det_Empty);

                tc_det_head_mainEmpty.Controls.Add(tbldetailEmpty);
                tr_det_head_mainEmpty.Cells.Add(tc_det_head_mainEmpty);
                tbldetail_mainEmpty.Rows.Add(tr_det_head_mainEmpty);

                form1.Controls.Add(tbldetail_mainEmpty);
            }

            lblHead.Visible = false;
        }

        ExportButton();

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
    public class DynamicTemplateField1 : ITemplate
    {

        public void InstantiateIn(Control container)
        {
            //string divcode = "8";

            //SalesForce SF = new SalesForce();
            //DataSet ff = new DataSet();
            //ff = SF.GetBrd_Name(divcode);
            //int cnt = ff.Tables[0].Rows.Count;
            //for (int j = 0; j < cnt; j++)
            //{

            //    //define the control to be added , i take text box as your need
            //    TableCell txt1 = new TableCell();
            //    txt1.ID = "txtquantity_" + j + "row_" + j + "";
            //    container.Controls.Add(txt1);
            //}
        }
    }
    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {

        SalesForce SF = new SalesForce();
        DataSet ff = new DataSet();
        ff = SF.GetProduct_Name(div_code);

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableCell HeaderCell = new TableCell();
            HeaderCell.Width = 25;
            HeaderCell.Height = 35;
            HeaderCell.Font.Bold = true;
            //HeaderCell.ApplyStyle.
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");

            TableCell Distributor = new TableCell();
            HeaderCell.Width = 25;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "S.No";

            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 250;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "Field Force Name";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 250;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "Stockist Name";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 250;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "Route Name";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 80;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "FC_TM";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 80;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "LC_TM";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 250;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "SF_CODE";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 250;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "dis_code";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 250;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "Route_code";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            foreach (DataRow drdoctor in ff.Tables[0].Rows)
            {
                HeaderCell = new TableCell();
                HeaderCell.Height = 35;
                HeaderCell.Width = 80;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");

                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderCell.Attributes["style"] = "font: Bold";
                HeaderCell.Text = drdoctor["Product_Detail_Name"].ToString();
                HeaderGridRow0.Cells.Add(HeaderCell);
            }

            HeaderCell = new TableCell();
            HeaderCell.Width = 250;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "ORDER VALUE";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 80;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "TC";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 80;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "EC";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Width = 80;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "TPS";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 80;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "TLS";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 80;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "Brand EC";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 80;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "Brand Value";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderGridRow0.HorizontalAlign = HorizontalAlign.Center;
            gvtotalorder.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

        //}
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
            // double mult = int.Parse(id) * int.Parse(id2);
            //adding result to last column. coz we add new column in last.
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
        //for (int i = subTotalRowIndex; i < gvtotalorder.Rows.Count; i++)
        //{
        //    subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt].Text);
        //    nettotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt - 1].Text);
        //    // subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
        //    //subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
        //    //nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
        //}
        //this.AddTotalRow("Sub Total", nettotal.ToString("N2"), subTotal.ToString("N2"));
        //this.AddTotalRoww("Total", nttotal.ToString("N2"), total.ToString("N2"));
    }

    protected void OnRowCreated1(object sender, GridViewRowEventArgs e)
    {



        SalesForce SF = new SalesForce();
        DataSet ff = new DataSet();
        ff = SF.GetProduct_Name(div_code);
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableCell HeaderCell = new TableCell();
            HeaderCell.Width = 25;
            HeaderCell.Height = 35;
            HeaderCell.Font.Bold = true;
            //HeaderCell.ApplyStyle.
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");

            TableCell Distributor = new TableCell();
            HeaderCell.Width = 25;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "S.No";

            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 500;
            HeaderCell.Height = 40;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "FieldForce Name";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 500;
            HeaderCell.Height = 40;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "Closing By";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 250;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "Distributor Name";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 250;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "Closing Date";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            foreach (DataRow drdoctor in ff.Tables[0].Rows)
            {
                HeaderCell = new TableCell();
                HeaderCell.Height = 35;
                HeaderCell.Width = 80;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");

                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderCell.Attributes["style"] = "font: Bold";
                HeaderCell.Text = drdoctor["Product_Detail_Name"].ToString();
                HeaderGridRow0.Cells.Add(HeaderCell);
            }

            HeaderCell = new TableCell();
            HeaderCell.Width = 250;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "Image";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderGridRow0.HorizontalAlign = HorizontalAlign.Center;
            GridView2.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }

    }
    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {

        GridView grid = (GridView)sender;

        if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        {

            e.Row.Cells[0].Text = "" + ((((GridView)sender).PageIndex * ((GridView)sender).PageSize) + (e.Row.RowIndex + 1));


            int numColunas = e.Row.Cells.Count;

            //for (int coluna = 2; coluna < numColunas; coluna++)
            //{
            //if ((grid.HeaderRow.Cells[coluna].Text.Contains("Image")) && (e.Row.Cells[coluna].Text != " "))

            //   if (!grid.HeaderRow.Cells[coluna].Text.Contains("Image"))
            //   {
            //      string tipoSeta = e.Row.Cells[coluna].Text;
            //      Image imagem = new Image();
            //      imagem.ID = tipoSeta + coluna.ToString();
            //      imagem.ImageUrl = "http://tiesar.sanfmcg.com/photos/" + tipoSeta;
            //      e.Row.Cells[coluna].Controls.Add(imagem);
            //  }
            //}




        }
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

        //}
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



    protected void OnDataBound1(object sender, EventArgs e)
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
        //for (int i = subTotalRowIndex; i < gvtotalorder.Rows.Count; i++)
        //{
        //    subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt].Text);
        //    nettotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt - 1].Text);
        //    // subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
        //    //subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
        //    //nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
        //}
        //this.AddTotalRow("Sub Total", nettotal.ToString("N2"), subTotal.ToString("N2"));
        //this.AddTotalRoww("Total", nttotal.ToString("N2"), total.ToString("N2"));
    }
    protected void OnRowCreated2(object sender, GridViewRowEventArgs e)
    {



        SalesForce SF = new SalesForce();
        DataSet ff = new DataSet();
        ff = SF.GetProduct_Name(div_code);
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableCell HeaderCell = new TableCell();
            HeaderCell.Width = 25;
            HeaderCell.Height = 35;
            HeaderCell.Font.Bold = true;
            //HeaderCell.ApplyStyle.
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");

            TableCell Distributor = new TableCell();
            HeaderCell.Width = 25;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "S.No";

            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 250;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "SF_CODE";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Width = 250;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "Distributor Name";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Width = 250;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "Plan Date";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);
            foreach (DataRow drdoctor in ff.Tables[0].Rows)
            {
                HeaderCell = new TableCell();
                HeaderCell.Height = 35;
                HeaderCell.Width = 80;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");

                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderCell.Attributes["style"] = "font: Bold";
                HeaderCell.Text = drdoctor["Product_Detail_Name"].ToString();
                HeaderGridRow0.Cells.Add(HeaderCell);
            }



            HeaderGridRow0.HorizontalAlign = HorizontalAlign.Center;
            GridView3.Controls[0].Controls.AddAt(0, HeaderGridRow0);

            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        }

    }
    protected void GridView1_RowDataBound2(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        {

            e.Row.Cells[0].Text = "" + ((((GridView)sender).PageIndex * ((GridView)sender).PageSize) + (e.Row.RowIndex + 1));
        }
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

        //}
        //e.Row.Cells[1].Visible = false;
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

    protected void OnDataBound2(object sender, EventArgs e)
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
        //for (int i = subTotalRowIndex; i < gvtotalorder.Rows.Count; i++)
        //{
        //    subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt].Text);
        //    nettotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[gridcnt - 1].Text);
        //    // subTotal += Convert.ToDecimal(gvtotalorder.Rows[i].Cells[4].Text);
        //    //subTotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("orderval")).Text);
        //    //nettotal += Convert.ToDecimal(((Label)gvtotalorder.Rows[i].FindControl("netval")).Text);
        //}
        //this.AddTotalRow("Sub Total", nettotal.ToString("N2"), subTotal.ToString("N2"));
        //this.AddTotalRoww("Total", nttotal.ToString("N2"), total.ToString("N2"));
    }

    private void ExportButton()
    {
        btnClose.Visible = true;
        btnPrint.Visible = false;
        btnExcel.Visible = true;
        btnPDF.Visible = false;
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

    private void FillSalesForce1(string div_code, string sf_code, int cmonth, int cyear)
    {
        // Fetch the total rows for the table
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_getMR(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
            ViewState["dsSalesForce"] = dsSalesForce;

        DCR dc = new DCR();
        int iret = dc.isDCR(div_code, cmonth, cyear);
        if (iret > 0)
            CreateDynamicTableDCRDate1(Fdate, Tdate, sf_code);
        //FillWorkType();
    }

    private void CreateDynamicTableDCRDate1(string Fdate, string Tdate, string sf_code)
    {
        DCR dc = new DCR();

        //dsDCR = dc.get_dcr_date(sf_code, imonth, iyear);
        dsDCR = dc.get_dcr_DCRPendingdate_DCRDetail(sf_code, Fdate, Tdate);

        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            DCR dcsf = new DCR();
            dssf = dcsf.getSfName_HQ(sf_code);

            if (dssf.Tables[0].Rows.Count > 0)
            {
                Sf_Name = dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                Sf_HQ = dssf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            }

            foreach (DataRow drdoc in dsDCR.Tables[0].Rows)
            {


                //Pending Approval 

                Table tbldetail_mainPending = new Table();
                tbldetail_mainPending.BorderStyle = BorderStyle.None;
                tbldetail_mainPending.Width = 1100;
                TableRow tr_det_head_mainPending = new TableRow();
                TableCell tc_det_head_mainPending = new TableCell();
                tc_det_head_mainPending.Width = 100;
                Literal lit_det_mainPending = new Literal();
                lit_det_mainPending.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_mainPending.Controls.Add(lit_det_mainPending);
                tr_det_head_mainPending.Cells.Add(tc_det_head_mainPending);

                TableCell tc_det_head_mainPendingSub = new TableCell();
                tc_det_head_mainPendingSub.Width = 1000;


                Table tbldetailhosPending = new Table();
                tbldetailhosPending.BorderStyle = BorderStyle.Solid;
                tbldetailhosPending.BorderWidth = 1;
                tbldetailhosPending.GridLines = GridLines.Both;
                tbldetailhosPending.Width = 1000;
                tbldetailhosPending.Style.Add("border-collapse", "none");
                tbldetailhosPending.Style.Add("border", "none");


                dsdoc = dc.get_Pending_Single_Temp_Date(sf_code, drdoc["Activity_Date"].ToString()); //1-Listed Doctor

                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_det_Pending = new TableRow();
                    TableCell tc_det_Pending = new TableCell();
                    iCount += 1;
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center> <b> " + dsdoc.Tables[0].Rows[0]["Temp"].ToString() + " </b> </center>";
                    tc_det_Pending.Style.Add("color", "Red");
                    tc_det_Pending.Style.Add("border", "none");
                    tc_det_Pending.BorderStyle = BorderStyle.Solid;
                    tc_det_Pending.BorderWidth = 1;
                    tc_det_Pending.Controls.Add(lit_det_SNo);
                    tr_det_Pending.Cells.Add(tc_det_Pending);


                    tbldetailhosPending.Rows.Add(tr_det_Pending);
                }

                tc_det_head_mainPendingSub.Controls.Add(tbldetailhosPending);
                tr_det_head_mainPending.Cells.Add(tc_det_head_mainPendingSub);
                tbldetail_mainPending.Rows.Add(tr_det_head_mainPending);

                form1.Controls.Add(tbldetail_mainPending);


                //Pending Approval 

                // WeekOff 

                Table tbldetail_mainHoliday = new Table();
                tbldetail_mainHoliday.BorderStyle = BorderStyle.None;
                tbldetail_mainHoliday.Width = 1100;
                TableRow tr_det_head_mainHoliday = new TableRow();
                TableCell tc_det_head_mainHolday = new TableCell();
                tc_det_head_mainHolday.Width = 100;
                Literal lit_det_mainHoliday = new Literal();
                lit_det_mainHoliday.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_mainHolday.Controls.Add(lit_det_mainHoliday);
                tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHolday);

                TableCell tc_det_head_mainHoliday = new TableCell();
                tc_det_head_mainHoliday.Width = 1000;


                Table tbldetailHoliday = new Table();
                tbldetailHoliday.BorderStyle = BorderStyle.Solid;
                tbldetailHoliday.BorderWidth = 1;
                tbldetailHoliday.GridLines = GridLines.Both;
                tbldetailHoliday.Width = 1000;
                tbldetailHoliday.Style.Add("border-collapse", "none");
                tbldetailHoliday.Style.Add("border", "none");

                if (sf_code.Contains("MR"))
                {
                    //dsdoc = dc.get_DCRHoliday_Name_MR(sf_code, drdoc["Activity_Date"].ToString(), 5); //5-Hospital
                    dsdoc = dc.get_DCRHoliday_Name_MR_chk(sf_code, drdoc["Activity_Date"].ToString(), drdoc["dcr"].ToString()); //5-Hospital
                }
                else
                {
                    //dsdoc = dc.get_DCRHoliday_Name(sf_code, drdoc["Activity_Date"].ToString(), 5); //5-Hospital
                    dsdoc = dc.get_DCRHoliday_Name_chk(sf_code, drdoc["Activity_Date"].ToString(), drdoc["dcr"].ToString()); //5-Hospital
                }
                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_det_head = new TableRow();

                    Table tbl = new Table();

                    iCount = 0;
                    foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                    {
                        TableRow tr_det_sno = new TableRow();
                        TableCell tc_det_SNo = new TableCell();
                        iCount += 1;
                        Literal lit_det_SNo2 = new Literal();
                        if (sf_code.Contains("MR"))
                        {
                            //lit_det_SNo2.Text = "<center>" + drdoctor["Worktype_Name_B"].ToString() + "</center>";

                            TableRow tr_ff = new TableRow();
                            TableCell tc_ff_name = new TableCell();
                            tc_ff_name.BorderStyle = BorderStyle.None;
                            tc_ff_name.Width = 500;
                            Literal lit_ff_name = new Literal();
                            lit_ff_name.Text = "<b>Field Force Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_Name.ToString();
                            tc_ff_name.Controls.Add(lit_ff_name);
                            tr_ff.Cells.Add(tc_ff_name);

                            TableCell tc_HQ = new TableCell();
                            tc_HQ.BorderStyle = BorderStyle.None;
                            tc_HQ.Width = 500;

                            tc_HQ.HorizontalAlign = HorizontalAlign.Left;
                            Literal lit_HQ = new Literal();
                            lit_HQ.Text = "<span style='margin-left:200px'><b>Head Quarters </b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + Sf_HQ.ToString() + "</span>";
                            //lit_HQ.Text = "<b>Head Quarters</b>" +  Sf_HQ.ToString();
                            tc_HQ.Controls.Add(lit_HQ);
                            tr_ff.Cells.Add(tc_HQ);
                            tbl.Rows.Add(tr_ff);

                            TableRow tr_dcr = new TableRow();
                            TableCell tc_dcr_submit = new TableCell();
                            tc_dcr_submit.BorderStyle = BorderStyle.None;
                            tc_dcr_submit.Width = 500;
                            Literal lit_dcr_submit = new Literal();
                            lit_dcr_submit.Text = "<b>Daily Call Report</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoc["Submission_Date"].ToString();
                            tc_dcr_submit.Controls.Add(lit_dcr_submit);
                            tr_dcr.Cells.Add(tc_dcr_submit);

                            TableCell tc_Terr = new TableCell();
                            tc_Terr.BorderStyle = BorderStyle.None;
                            tc_Terr.HorizontalAlign = HorizontalAlign.Center;
                            tc_Terr.Width = 500;
                            Literal lit_Terr = new Literal();
                            //Territory terr = new Territory();
                            //dsTerritory = terr.getWorkAreaName(div_code);
                            //lit_Terr.Text = "<b>Work Type</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoctor["Worktype_Name_B"].ToString();

                            tc_Terr.Controls.Add(lit_Terr);
                            tr_dcr.Cells.Add(tc_Terr);

                            tbl.Rows.Add(tr_dcr);

                            //tc_det_head_main4.Controls.Add(tbl);
                            //tr_det_head_main3.Cells.Add(tc_det_head_main4);
                            //tbldetail_main3.Rows.Add(tr_det_head_main3);

                            //form1.Controls.Add(tbldetail_main3);

                            Table tbl_head_empty = new Table();
                            TableRow tr_head_empty = new TableRow();
                            TableCell tc_head_empty = new TableCell();
                            Literal lit_head_empty = new Literal();
                            lit_head_empty.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Work Type</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoctor["Worktype_Name_B"].ToString();
                            tc_head_empty.Controls.Add(lit_head_empty);
                            tr_head_empty.Cells.Add(tc_head_empty);
                            tbl_head_empty.Rows.Add(tr_head_empty);
                            form1.Controls.Add(tbl_head_empty);

                            Table tbldetail_main = new Table();
                            tbldetail_main.BorderStyle = BorderStyle.None;
                            tbldetail_main.Width = 1100;
                            TableRow tr_det_head_main = new TableRow();
                            TableCell tc_det_head_main = new TableCell();
                            tc_det_head_main.Width = 100;
                            Literal lit_det_main = new Literal();
                            lit_det_main.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            tc_det_head_main.Controls.Add(lit_det_main);
                            tr_det_head_main.Cells.Add(tc_det_head_main);

                            TableCell tc_det_head_main2 = new TableCell();
                            tc_det_head_main2.Width = 1000;

                            Table tbldetail = new Table();
                            tbldetail.BorderStyle = BorderStyle.Solid;
                            tbldetail.BorderWidth = 1;
                            tbldetail.GridLines = GridLines.Both;
                            tbldetail.Width = 1000;
                            tbldetail.Style.Add("border-collapse", "collapse");
                            tbldetail.Style.Add("border", "solid 1px Black");

                            dsdoc = dc.GetTable(sf_code, drdoc["Activity_Date"].ToString()); //1-Listed Doctor
                            iCount = 0;
                            if (dsdoc.Tables[0].Rows.Count > 0)
                            {


                                //lit_Terr.Text = "<span style='margin-left:200px'><b> Territory Worked </b></span>" + "<span style='margin-left:15px'></span>:" + "<span style='margin-left:21px;text-align:right'>" + dsdoc.Tables[0].Rows[0]["che_POB_Name"].ToString() + "</span>";
                                TableRow tr_det_head1 = new TableRow();
                                TableCell tc_det_head_SNo = new TableCell();
                                tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                                tc_det_head_SNo.BorderWidth = 1;
                                tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_head_SNo = new Literal();
                                tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                                lit_det_head_SNo.Text = "<b>S.No</b>";
                                tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                                tr_det_head1.Cells.Add(tc_det_head_SNo);

                                TableCell tc_det_head_Ses = new TableCell();
                                tc_det_head_Ses.BorderStyle = BorderStyle.Solid;
                                tc_det_head_Ses.BorderWidth = 1;
                                tc_det_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_head_Ses = new Literal();
                                tc_det_head_Ses.Attributes.Add("Class", "tr_det_head");
                                lit_det_head_Ses.Text = "<b>Date</b>";
                                tc_det_head_Ses.Controls.Add(lit_det_head_Ses);
                                tr_det_head1.Cells.Add(tc_det_head_Ses);

                                TableCell tc_det_head_doc = new TableCell();
                                tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                                tc_det_head_doc.BorderWidth = 1;
                                tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_head_doc = new Literal();
                                lit_det_head_doc.Text = "<b>Name Of the Distributor</b>";
                                tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                                tc_det_head_doc.Controls.Add(lit_det_head_doc);
                                tr_det_head1.Cells.Add(tc_det_head_doc);

                                TableCell tc_det_head_time = new TableCell();
                                tc_det_head_time.BorderStyle = BorderStyle.Solid;
                                tc_det_head_time.BorderWidth = 1;
                                tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_head_time = new Literal();
                                lit_det_head_time.Text = "<b>Person Met</b>";
                                tc_det_head_time.Attributes.Add("Class", "tr_det_head");
                                tc_det_head_time.Controls.Add(lit_det_head_time);
                                tr_det_head1.Cells.Add(tc_det_head_time);



                                TableCell tc_det_head_ww = new TableCell();
                                tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                                tc_det_head_ww.BorderWidth = 1;
                                tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_head_ww = new Literal();
                                lit_det_head_ww.Text = "<b>Contact No</b>";
                                tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                                tc_det_head_ww.Controls.Add(lit_det_head_ww);
                                tr_det_head1.Cells.Add(tc_det_head_ww);

                                TableCell tc_det_head_visit = new TableCell();
                                tc_det_head_visit.BorderStyle = BorderStyle.Solid;
                                tc_det_head_visit.BorderWidth = 1;
                                tc_det_head_visit.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_head_visit = new Literal();
                                lit_det_head_visit.Text = "<b>Address</b>";
                                tc_det_head_visit.Attributes.Add("Class", "tr_det_head");
                                tc_det_head_visit.Controls.Add(lit_det_head_visit);
                                tr_det_head1.Cells.Add(tc_det_head_visit);



                                TableCell tc_det_head_spec = new TableCell();
                                tc_det_head_spec.BorderStyle = BorderStyle.Solid;
                                tc_det_head_spec.BorderWidth = 1;
                                tc_det_head_spec.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_head_spec = new Literal();
                                lit_det_head_spec.Text = "<b>Remarks</b>";
                                tc_det_head_spec.Attributes.Add("Class", "tr_det_head");
                                tc_det_head_spec.Controls.Add(lit_det_head_spec);
                                tr_det_head1.Cells.Add(tc_det_head_spec);








                                tbldetail.Rows.Add(tr_det_head1);

                                string strlongname = "";
                                iCount = 0;

                                foreach (DataRow drdoctor1 in dsdoc.Tables[0].Rows)
                                {


                                    TableRow tr_det_sno1 = new TableRow();
                                    TableCell tc_det_SNo1 = new TableCell();
                                    iCount += 1;
                                    Literal lit_det_SNo1 = new Literal();
                                    lit_det_SNo1.Text = "<center>" + iCount.ToString() + "</center>";
                                    tc_det_SNo1.Attributes.Add("Class", "tbldetail_Data");
                                    tc_det_SNo1.BorderStyle = BorderStyle.Solid;
                                    tc_det_SNo1.BorderWidth = 1;
                                    tc_det_SNo1.Controls.Add(lit_det_SNo1);
                                    tr_det_sno1.Cells.Add(tc_det_SNo1);

                                    TableCell tc_det_Ses = new TableCell();
                                    Literal lit_det_Ses = new Literal();
                                    lit_det_Ses.Text = "&nbsp;&nbsp;" + drdoctor1["Date"].ToString();
                                    tc_det_Ses.Attributes.Add("Class", "tbldetail_Data");
                                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                                    tc_det_Ses.BorderWidth = 1;
                                    tc_det_Ses.Controls.Add(lit_det_Ses);
                                    tr_det_sno1.Cells.Add(tc_det_Ses);

                                    TableCell tc_det_dr_name = new TableCell();
                                    Literal lit_det_dr_name = new Literal();
                                    lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor1["Shop_Name"].ToString();
                                    tc_det_dr_name.Attributes.Add("Class", "tbldetail_Data");
                                    tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                                    tc_det_dr_name.BorderWidth = 1;
                                    tc_det_dr_name.Width = 150;
                                    tc_det_dr_name.Controls.Add(lit_det_dr_name);
                                    tr_det_sno1.Cells.Add(tc_det_dr_name);

                                    TableCell tc_det_time = new TableCell();
                                    Literal lit_det_time = new Literal();
                                    lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor1["Contact_Person"].ToString();
                                    tc_det_time.Attributes.Add("Class", "tbldetail_Data");
                                    tc_det_time.BorderStyle = BorderStyle.Solid;
                                    tc_det_time.BorderWidth = 1;
                                    tc_det_time.Controls.Add(lit_det_time);
                                    tr_det_sno1.Cells.Add(tc_det_time);

                                    TableCell tc_det_LastUpdate_Date = new TableCell();
                                    Literal lit_det_time_LastUpdate_Date = new Literal();
                                    lit_det_time_LastUpdate_Date.Text = "&nbsp;&nbsp;" + drdoctor1["Phone_Number"].ToString();
                                    tc_det_LastUpdate_Date.Attributes.Add("Class", "tbldetail_Data");
                                    tc_det_LastUpdate_Date.BorderStyle = BorderStyle.Solid;
                                    tc_det_LastUpdate_Date.BorderWidth = 1;
                                    tc_det_LastUpdate_Date.Width = 120;
                                    tc_det_LastUpdate_Date.Controls.Add(lit_det_time_LastUpdate_Date);
                                    tr_det_sno1.Cells.Add(tc_det_LastUpdate_Date);



                                    TableCell tc_det_spec = new TableCell();
                                    Literal lit_det_spec = new Literal();
                                    lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor1["address"].ToString();
                                    tc_det_spec.Attributes.Add("Class", "tbldetail_Data");
                                    tc_det_spec.BorderStyle = BorderStyle.Solid;
                                    tc_det_spec.BorderWidth = 1;
                                    tc_det_spec.Controls.Add(lit_det_spec);
                                    tr_det_sno1.Cells.Add(tc_det_spec);

                                    TableCell tc_det_catg = new TableCell();
                                    Literal lit_det_catg = new Literal();
                                    lit_det_catg.Text = "&nbsp;&nbsp;" + drdoctor1["Remarks"].ToString();
                                    tc_det_catg.Attributes.Add("Class", "tbldetail_Data");
                                    tc_det_catg.BorderStyle = BorderStyle.Solid;
                                    tc_det_catg.BorderWidth = 1;
                                    tc_det_catg.Controls.Add(lit_det_catg);
                                    tr_det_sno1.Cells.Add(tc_det_catg);


                                    tbldetail.Rows.Add(tr_det_sno1);




                                }





                            }


                            tc_det_head_main2.Controls.Add(tbldetail);
                            tr_det_head_main.Cells.Add(tc_det_head_main2);
                            tbldetail_main.Rows.Add(tr_det_head_main);

                            form1.Controls.Add(tbldetail_main);


                            if (iCount > 0)
                            {
                                Table tbl_doc_empty = new Table();
                                TableRow tr_doc_empty = new TableRow();
                                TableCell tc_doc_empty = new TableCell();
                                Literal lit_doc_empty = new Literal();
                                lit_doc_empty.Text = "<BR>";
                                tc_doc_empty.Controls.Add(lit_doc_empty);
                                tr_doc_empty.Cells.Add(tc_doc_empty);
                                tbl_doc_empty.Rows.Add(tr_doc_empty);
                                form1.Controls.Add(tbl_doc_empty);


                            }

                            //2-Chemists

                            Table tbldetail_main5 = new Table();
                            tbldetail_main5.BorderStyle = BorderStyle.None;
                            tbldetail_main5.Width = 1100;
                            TableRow tr_det_head_main5 = new TableRow();
                            TableCell tc_det_head_main5 = new TableCell();
                            tc_det_head_main5.Width = 100;
                            Literal lit_det_main5 = new Literal();
                            lit_det_main5.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            tc_det_head_main5.Controls.Add(lit_det_main5);
                            tr_det_head_main5.Cells.Add(tc_det_head_main5);

                            TableCell tc_det_head_main6 = new TableCell();
                            tc_det_head_main6.Width = 1000;


                            Table tbldetailChe = new Table();
                            tbldetailChe.BorderStyle = BorderStyle.Solid;
                            tbldetailChe.BorderWidth = 1;
                            tbldetailChe.GridLines = GridLines.Both;
                            tbldetailChe.Width = 1000;
                            tbldetailChe.Style.Add("border-collapse", "collapse");
                            tbldetailChe.Style.Add("border", "solid 1px Black");

                        }
                        else
                        {
                            lit_det_SNo2.Text = "<center>" + drdoctor["Worktype_Name_M"].ToString() + "</center>";
                        }
                        tc_det_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_SNo.Attributes.Add("Class", "Holiday");
                        tc_det_SNo.BorderWidth = 1;
                        tc_det_SNo.BorderStyle = BorderStyle.None;
                        tc_det_SNo.Controls.Add(lit_det_SNo2);
                        tr_det_sno.Cells.Add(tc_det_SNo);

                        tbldetailHoliday.Rows.Add(tr_det_sno);

                        tc_det_head_mainHoliday.Controls.Add(tbldetailHoliday);
                        tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHoliday);
                        tbldetail_mainHoliday.Rows.Add(tr_det_head_mainHoliday);

                        Table tbl_line = new Table();
                        tbl_line.BorderStyle = BorderStyle.None;
                        tbl_line.Width = 1000;
                        tbl_line.Style.Add("border-collapse", "collapse");
                        tbl_line.Style.Add("border-top", "none");
                        tbl_line.Style.Add("border-right", "none");
                        tbl_line.Style.Add("margin-left", "100px");
                        tbl_line.Style.Add("border-bottom ", "solid 1px Black");

                        form1.Controls.Add(tbldetail_mainHoliday);

                        TableRow tr_line = new TableRow();

                        TableCell tc_line0 = new TableCell();
                        tc_line0.Width = 100;
                        Literal lit_line0 = new Literal();
                        lit_line0.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        tc_line0.Controls.Add(lit_line0);
                        tr_line.Cells.Add(tc_line0);

                        TableCell tc_line = new TableCell();
                        tc_line.Width = 1000;
                        Literal lit_line = new Literal();
                        // lit_line.Text = "<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>";
                        tc_line.Controls.Add(lit_line);
                        tr_line.Cells.Add(tc_line);
                        tbl_line.Rows.Add(tr_line);
                        form1.Controls.Add(tbl_line);
                    }
                }

                else
                {

                    //Giri Editing
                    if (sf_code.Contains("MGR"))
                    {
                        dsdoc1 = dc.get_dcr_details_MGR(sf_code, drdoc["Activity_Date"].ToString(), 1); //1-Listed Doctor
                    }
                    else
                    {
                        dsdoc1 = dc.get_dcr_details(sf_code, drdoc["Activity_Date"].ToString(), 1); //1-Listed Doctor
                    }
                    iCount = 0;


                    foreach (DataRow drdoctor1 in dsdoc1.Tables[0].Rows)
                    {
                        //if (sf_code.Contains("MGR"))
                        //{
                        //    dsdoc = dc.get_dcr_details1_Mgr(drdoctor1["sf_code"].ToString(), drdoc["Activity_Date"].ToString(), 1, drdoctor1["stockist_code"].ToString()); //1-Listed Doctor
                        //}
                        //else
                        //{
                        dsdoc = dc.get_dcr_details1(drdoctor1["sf_code"].ToString(), drdoc["Activity_Date"].ToString(), 1, drdoctor1["stockist_code"].ToString()); //1-Listed Doctor
                        //}
                        iCount = 0;

                        //if (drdoctor1["sf_code"].ToString().Contains("MGR"))
                        //{
                        //    //Mgr

                        //    if (dsdoc.Tables[0].Rows.Count > 0)
                        //    {
                        //        Table tbldetail_main3 = new Table();
                        //        //tbldetail_main3.BorderStyle = BorderStyle.None;
                        //        //tbldetail_main3.Width = 1100;
                        //        TableRow tr_det_head_main3 = new TableRow();
                        //        //TableCell tc_det_head_main3 = new TableCell();
                        //        //tc_det_head_main3.Width = 100;
                        //        //Literal lit_det_main3 = new Literal();
                        //        //lit_det_main3.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                        //        //tc_det_head_main3.Controls.Add(lit_det_main3);
                        //        //tr_det_head_main3.Cells.Add(tc_det_head_main3);

                        //        TableCell tc_det_head_main4 = new TableCell();
                        //        //tc_det_head_main4.Width = 1000;

                        //        Table tbl = new Table();
                        //        //tbl.Width = 1000;
                        //        tbl.Style.Add("width", "185%");

                        //        TableRow tr_day = new TableRow();
                        //        TableCell tc_day = new TableCell();
                        //        tc_day.BorderStyle = BorderStyle.None;
                        //        tc_day.ColumnSpan = 2;
                        //        tc_day.HorizontalAlign = HorizontalAlign.Center;
                        //        tc_day.Style.Add("font-name", "verdana;");
                        //        Literal lit_day = new Literal();
                        //        tc_day.Controls.Add(lit_day);
                        //        tr_day.Cells.Add(tc_day);
                        //        tbl.Rows.Add(tr_day);
                        //        tc_det_head_main4.Controls.Add(tbl);
                        //        tr_det_head_main3.Cells.Add(tc_det_head_main4);
                        //        tbldetail_main3.Rows.Add(tr_det_head_main3);

                        //        form1.Controls.Add(tbldetail_main3);

                        //        TableRow tr_ff = new TableRow();

                        //        TableCell tc_ff_name = new TableCell();
                        //        tc_ff_name.BorderStyle = BorderStyle.None;
                        //        tc_ff_name.Width = 500;
                        //        Literal lit_ff_name = new Literal();
                        //        lit_ff_name.Text = "<b>Field Force Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "<b>" + Sf_Name.ToString() + "</b>";
                        //        tc_ff_name.Controls.Add(lit_ff_name);
                        //        tr_ff.Cells.Add(tc_ff_name);

                        //        TableCell tc_HQ = new TableCell();
                        //        tc_HQ.BorderStyle = BorderStyle.None;
                        //        tc_HQ.Width = 500;

                        //        tc_HQ.HorizontalAlign = HorizontalAlign.Left;
                        //        Literal lit_HQ = new Literal();
                        //        lit_HQ.Text = "<span style='margin-left:200px'><b>Head Quarters </b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + "<b>" + Sf_HQ.ToString() + "</b></span>";
                        //        tc_HQ.Controls.Add(lit_HQ);
                        //        tr_ff.Cells.Add(tc_HQ);
                        //        tbl.Rows.Add(tr_ff);

                        //        TableRow tr_ff1 = new TableRow();
                        //        TableCell tc_ff_name1 = new TableCell();
                        //        tc_ff_name1.BorderStyle = BorderStyle.None;
                        //        tc_ff_name1.Width = 500;
                        //        Literal lit_ff_name1 = new Literal();
                        //        lit_ff_name1.Text = "<b>Daily Call Report</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<span style='color:Red'><b>" + drdoc["Activity_Date"].ToString() + "</b></span>";
                        //        tc_ff_name1.Controls.Add(lit_ff_name1);
                        //        tr_ff1.Cells.Add(tc_ff_name1);

                        //        TableCell tc_HQ1 = new TableCell();
                        //        tc_HQ1.BorderStyle = BorderStyle.None;
                        //        tc_HQ1.Width = 500;

                        //        tc_HQ1.HorizontalAlign = HorizontalAlign.Left;
                        //        Literal lit_HQ1 = new Literal();
                        //        lit_HQ1.Text = "<span style='margin-left:200px'><b>Submitted on </b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + "<b>" + drdoc["Submission_Date"].ToString() + "</b></span>";
                        //        tc_HQ1.Controls.Add(lit_HQ1);
                        //        tr_ff1.Cells.Add(tc_HQ1);
                        //        tbl.Rows.Add(tr_ff1);

                        //        TableRow tr_ff2 = new TableRow();
                        //        TableCell tc_ff_name2 = new TableCell();
                        //        tc_ff_name2.BorderStyle = BorderStyle.None;
                        //        tc_ff_name2.Width = 500;
                        //        Literal lit_ff_name2 = new Literal();
                        //        lit_ff_name2.Text = "<b>DB Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dsdoc.Tables[0].Rows[0]["stockist_name"].ToString();
                        //        tc_ff_name2.Controls.Add(lit_ff_name2);
                        //        tr_ff2.Cells.Add(tc_ff_name2);

                        //        TableCell tc_HQ2 = new TableCell();
                        //        tc_HQ2.BorderStyle = BorderStyle.None;
                        //        tc_HQ2.Width = 500;

                        //        tc_HQ2.HorizontalAlign = HorizontalAlign.Left;
                        //        Literal lit_HQ2 = new Literal();
                        //        lit_HQ2.Text = "<span style='margin-left:200px'><b>Route </b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + dsdoc.Tables[0].Rows[0]["SDP_Name"].ToString(); ;
                        //        tc_HQ2.Controls.Add(lit_HQ2);
                        //        tr_ff2.Cells.Add(tc_HQ2);
                        //        tbl.Rows.Add(tr_ff2);




                        //        tc_det_head_main4.Controls.Add(tbl);
                        //        tr_det_head_main3.Cells.Add(tc_det_head_main4);
                        //        tbldetail_main3.Rows.Add(tr_det_head_main3);

                        //        form1.Controls.Add(tbldetail_main3);



                        //        Table tbldetail_main = new Table();
                        //        tbldetail_main.BorderStyle = BorderStyle.None;
                        //        //tbldetail_main.Width = 1100;
                        //        TableRow tr_det_head_main = new TableRow();
                        //        //TableCell tc_det_head_main = new TableCell();
                        //        //tc_det_head_main.Width = 100;
                        //        //Literal lit_det_main = new Literal();
                        //        //lit_det_main.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        //        //tc_det_head_main.Controls.Add(lit_det_main);
                        //        //tr_det_head_main.Cells.Add(tc_det_head_main);

                        //        TableCell tc_det_head_main2 = new TableCell();
                        //        //tc_det_head_main2.Width = 1000;

                        //        Table tbldetail = new Table();
                        //        tbldetail.BorderStyle = BorderStyle.Solid;
                        //        tbldetail.BorderWidth = 1;
                        //        tbldetail.GridLines = GridLines.Both;
                        //        //tbldetail.Width = 3500;
                        //        tbldetail.Style.Add("border-collapse", "collapse");
                        //        tbldetail.Style.Add("border", "solid 1px Black");
                        //        tbldetail.Style.Add("padding", "2px 5px");
                        //        tbldetail.Style.Add("white-space", "nowrap");
                        //        //tbldetail.Attributes.Add("Class", "table");

                        //        TableRow tr_det_head = new TableRow();
                        //        tr_det_head.Attributes.Add("Class", "table");
                        //        TableCell tc_det_head_SNo = new TableCell();
                        //        tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                        //        tc_det_head_SNo.BorderWidth = 1;
                        //        tc_det_head_SNo.RowSpan = 2;
                        //        tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                        //        Literal lit_det_head_SNo = new Literal();
                        //        tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                        //        lit_det_head_SNo.Text = "<b>S.No</b>";
                        //        tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                        //        tr_det_head.Cells.Add(tc_det_head_SNo);

                        //        TableCell tc_det_head_Ses = new TableCell();
                        //        tc_det_head_Ses.BorderStyle = BorderStyle.Solid;
                        //        tc_det_head_Ses.BorderWidth = 1;
                        //        tc_det_head_Ses.RowSpan = 2;
                        //        tc_det_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                        //        Literal lit_det_head_Ses = new Literal();
                        //        tc_det_head_Ses.Attributes.Add("Class", "tr_det_head");
                        //        lit_det_head_Ses.Text = "<b>Ses</b>";
                        //        tc_det_head_Ses.Controls.Add(lit_det_head_Ses);
                        //        tr_det_head.Cells.Add(tc_det_head_Ses);

                        //        TableCell tc_det_head_doc = new TableCell();
                        //        tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                        //        tc_det_head_doc.BorderWidth = 1;
                        //        tc_det_head_doc.RowSpan = 2;
                        //        tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                        //        Literal lit_det_head_doc = new Literal();
                        //        lit_det_head_doc.Text = "<b>Retailer Name</b>";
                        //        tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                        //        tc_det_head_doc.Controls.Add(lit_det_head_doc);
                        //        tr_det_head.Cells.Add(tc_det_head_doc);

                        //        TableCell tc_det_head_time = new TableCell();
                        //        tc_det_head_time.BorderStyle = BorderStyle.Solid;
                        //        tc_det_head_time.BorderWidth = 1;
                        //        tc_det_head_time.RowSpan = 2;
                        //        tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                        //        Literal lit_det_head_time = new Literal();
                        //        lit_det_head_time.Text = "<b>Time</b>";
                        //        tc_det_head_time.Attributes.Add("Class", "tr_det_head");
                        //        tc_det_head_time.Controls.Add(lit_det_head_time);
                        //        tr_det_head.Cells.Add(tc_det_head_time);



                        //        TableCell tc_det_head_ww = new TableCell();
                        //        tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                        //        tc_det_head_ww.BorderWidth = 1;
                        //        tc_det_head_ww.RowSpan = 2;
                        //        tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                        //        Literal lit_det_head_ww = new Literal();
                        //        lit_det_head_ww.Text = "<b>Worked With</b>";
                        //        tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                        //        tc_det_head_ww.Controls.Add(lit_det_head_ww);
                        //        tr_det_head.Cells.Add(tc_det_head_ww);


                        //        TableCell tc_det_head_class = new TableCell();
                        //        tc_det_head_class.BorderStyle = BorderStyle.Solid;
                        //        tc_det_head_class.BorderWidth = 1;
                        //        tc_det_head_class.RowSpan = 2;
                        //        tc_det_head_class.HorizontalAlign = HorizontalAlign.Center;
                        //        Literal lit_det_head_cla = new Literal();
                        //        lit_det_head_cla.Text = "<b>Class</b>";
                        //        tc_det_head_class.Attributes.Add("Class", "tr_det_head");
                        //        tc_det_head_class.Controls.Add(lit_det_head_cla);
                        //        tr_det_head.Cells.Add(tc_det_head_class);


                        //        TableCell tc_det_head_spec = new TableCell();
                        //        tc_det_head_spec.BorderStyle = BorderStyle.Solid;
                        //        tc_det_head_spec.BorderWidth = 1;
                        //        tc_det_head_spec.RowSpan = 2;
                        //        tc_det_head_spec.HorizontalAlign = HorizontalAlign.Center;
                        //        Literal lit_det_head_spec = new Literal();
                        //        lit_det_head_spec.Text = "<b>Channel</b>";
                        //        tc_det_head_spec.Attributes.Add("Class", "tr_det_head");
                        //        tc_det_head_spec.Controls.Add(lit_det_head_spec);
                        //        tr_det_head.Cells.Add(tc_det_head_spec);


                        //        TableCell tc_det_prod = new TableCell();
                        //        tc_det_prod.BorderStyle = BorderStyle.Solid;
                        //        Literal lit_det_prod = new Literal();
                        //        //
                        //        Territory terr = new Territory();
                        //        if (div_code == "11" || div_code == "13" || div_code == "8")
                        //        {
                        //            string adate = string.Empty;
                        //            adate = drdoc["Activity_Date"].ToString().Trim();

                        //            DateTime at = Convert.ToDateTime(adate);
                        //            adate = at.ToString("yyyy-MM-dd");
                        //            dsTerritory = terr.getProdName_dcrviewalldates(drdoctor1["sf_code"].ToString(), div_code, adate, dsdoc.Tables[0].Rows[0]["Plan_No"].ToString(), drdoctor1["stockist_code"].ToString());

                        //        }
                        //        else
                        //        {
                        //            dsTerritory = terr.getProdName(div_code);


                        //        }
                        //        foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                        //        {
                        //            TableHeaderCell tc = new TableHeaderCell();
                        //            tc.BorderStyle = BorderStyle.Solid;
                        //            tc.BorderWidth = 1;
                        //            tc.RowSpan = 2;
                        //            tc.HorizontalAlign = HorizontalAlign.Center;
                        //            Literal lic = new Literal();
                        //            if (div_code == "11" || div_code == "13" || div_code == "8")
                        //            {
                        //                lic.Text = "<b>" + pro1["Product_Detail_Name"].ToString() + "</b>";
                        //            }
                        //            else
                        //            {
                        //                lic.Text = "<b>" + pro1["Product_Short_Name"].ToString() + "</b>";
                        //            }
                        //            tc.Attributes.Add("Class", "tr_det_head");
                        //            tc.Controls.Add(lic);
                        //            tr_det_head.Cells.Add(tc);

                        //        }

                        //        //
                        //        //Territory terr = new Territory();
                        //        //dsTerritory = terr.getProdName(div_code);
                        //        //foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                        //        //{
                        //        //    TableCell tc = new TableCell();
                        //        //    tc.BorderStyle = BorderStyle.Solid;
                        //        //    tc.BorderWidth = 1;
                        //        //    tc.HorizontalAlign = HorizontalAlign.Center;
                        //        //    Literal lic = new Literal();
                        //        //    if (div_code == "11" || div_code == "13" || div_code == "8")
                        //        //    {
                        //        //        lic.Text = "<b>" + pro1["Product_Detail_Name"].ToString() + "</b>";
                        //        //    }
                        //        //    else
                        //        //    {
                        //        //        lic.Text = "<b>" + pro1["Product_Short_Name"].ToString() + "</b>";
                        //        //    }
                        //        //    tc.Attributes.Add("Class", "tr_det_head");
                        //        //    tc.Controls.Add(lic);
                        //        //    tr_det_head.Cells.Add(tc);

                        //        //}



                        //        TableCell tc_det_head_gift = new TableCell();
                        //        tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                        //        tc_det_head_gift.BorderWidth = 1;
                        //        tc_det_head_gift.RowSpan = 2;
                        //        tc_det_head_gift.HorizontalAlign = HorizontalAlign.Center;
                        //        Literal lit_det_head_gift = new Literal();
                        //        lit_det_head_gift.Text = "<b>Order Value</b>";
                        //        tc_det_head_gift.Attributes.Add("Class", "tr_det_head");
                        //        tc_det_head_gift.Controls.Add(lit_det_head_gift);
                        //        tr_det_head.Cells.Add(tc_det_head_gift);


                        //        TableCell tc_det_head_Quai1 = new TableCell();
                        //        tc_det_head_Quai1.BorderStyle = BorderStyle.Solid;
                        //        tc_det_head_Quai1.BorderWidth = 1;
                        //        tc_det_head_Quai1.RowSpan = 2;
                        //        Literal lit_det_head_quai1 = new Literal();
                        //        lit_det_head_quai1.Text = "<b>Net Weight</b>";
                        //        tc_det_head_Quai1.Attributes.Add("Class", "tr_det_head");
                        //        tc_det_head_Quai1.Controls.Add(lit_det_head_quai1);
                        //        tc_det_head_Quai1.HorizontalAlign = HorizontalAlign.Center;
                        //        tr_det_head.Cells.Add(tc_det_head_Quai1);


                        //        TableCell rehead = new TableCell();
                        //        rehead.BorderStyle = BorderStyle.Solid;
                        //        rehead.BorderWidth = 1;
                        //        rehead.RowSpan = 2;
                        //        Literal reheadlit = new Literal();
                        //        reheadlit.Text = "<b>Remarks</b>";
                        //        rehead.Attributes.Add("Class", "tr_det_head");
                        //        rehead.Controls.Add(reheadlit);
                        //        rehead.HorizontalAlign = HorizontalAlign.Center;
                        //        tr_det_head.Cells.Add(rehead);


                        //        TableCell tc_det_head_Last_Update_Date = new TableCell();
                        //        tc_det_head_Last_Update_Date.BorderStyle = BorderStyle.Solid;
                        //        tc_det_head_Last_Update_Date.BorderWidth = 1;
                        //        tc_det_head_Last_Update_Date.RowSpan = 2;
                        //        tc_det_head_Last_Update_Date.HorizontalAlign = HorizontalAlign.Center;
                        //        Literal lit_det_head_Last_Update_Date = new Literal();
                        //        lit_det_head_Last_Update_Date.Text = "<b>Last Updated</b>";
                        //        tc_det_head_Last_Update_Date.Attributes.Add("Class", "tr_det_head");
                        //        tc_det_head_Last_Update_Date.Controls.Add(lit_det_head_Last_Update_Date);
                        //        tr_det_head.Cells.Add(tc_det_head_Last_Update_Date);


                        //        //brand

                        //        TableCell tc_det_brd = new TableCell();
                        //        tc_det_brd.BorderStyle = BorderStyle.Solid;
                        //        Literal lit_det_brd = new Literal();
                        //        //
                        //        Territory terr1 = new Territory();
                        //        //if (div_code == "11" || div_code == "13" || div_code == "8")
                        //        //{
                        //        //    string adate = string.Empty;
                        //        //    adate = drdoc["Activity_Date"].ToString().Trim();

                        //        //    DateTime at = Convert.ToDateTime(adate);
                        //        //    adate = at.ToString("yyyy-MM-dd");
                        //        //    dsTerritory = terr1.getProdName_dcrviewalldates(drdoctor1["sf_code"].ToString(), div_code, adate, dsdoc.Tables[0].Rows[0]["Plan_No"].ToString(), drdoctor1["stockist_code"].ToString());

                        //        //}
                        //        //else
                        //        //{
                        //        dsTerritory1 = terr1.getBrandName(div_code);


                        //        //}
                        //        TableRow tr_catg = new TableRow();
                        //        tr_catg.BackColor = System.Drawing.Color.FromName("#0097AC");
                        //        tr_catg.Style.Add("Color", "White");

                        //        foreach (DataRow pro1 in dsTerritory1.Tables[0].Rows)
                        //        {
                        //            TableHeaderCell tc1 = new TableHeaderCell();
                        //            tc1.BorderStyle = BorderStyle.Solid;
                        //            tc1.BorderWidth = 1;
                        //            tc1.ColumnSpan = 2;
                        //            tc1.HorizontalAlign = HorizontalAlign.Center;
                        //            Literal lic1 = new Literal();
                        //            if (div_code == "11" || div_code == "13" || div_code == "8")
                        //            {
                        //                lic1.Text = "<b>" + pro1["Product_Brd_Name"].ToString() + "</b>";
                        //            }
                        //            else
                        //            {
                        //                lic1.Text = "<b>" + pro1["Product_Brd_SName"].ToString() + "</b>";
                        //            }
                        //            tc1.Attributes.Add("Class", "tr_det_head");
                        //            tc1.Controls.Add(lic1);
                        //            tr_det_head.Cells.Add(tc1);




                        //            TableCell tc_det_head_SNo2 = new TableCell();
                        //            tc_det_head_SNo2.BorderStyle = BorderStyle.Solid;
                        //            tc_det_head_SNo2.BorderWidth = 1;
                        //            tc_det_head_SNo2.Width = 20;
                        //            Literal lit_det_head_SNo2 = new Literal();
                        //            lit_det_head_SNo2.Text = "<b>AL</b>";
                        //            tc_det_head_SNo2.Attributes.Add("Class", "tr_det_head");
                        //            tc_det_head_SNo2.Controls.Add(lit_det_head_SNo2);
                        //            tc_det_head_SNo2.HorizontalAlign = HorizontalAlign.Center;
                        //            tr_catg.Cells.Add(tc_det_head_SNo2);

                        //            TableCell tc_det_head_hq1 = new TableCell();
                        //            tc_det_head_hq1.Width = 20;
                        //            tc_det_head_hq1.BorderStyle = BorderStyle.Solid;
                        //            tc_det_head_hq1.BorderWidth = 1;
                        //            Literal lit_det_head_hq1 = new Literal();
                        //            lit_det_head_hq1.Text = "<b>EC</b>";
                        //            tc_det_head_hq1.Attributes.Add("Class", "tr_det_head");
                        //            tc_det_head_hq1.Controls.Add(lit_det_head_hq1);
                        //            tc_det_head_hq1.HorizontalAlign = HorizontalAlign.Center;
                        //            tr_catg.Cells.Add(tc_det_head_hq1);




                        //        }

                        //        tbldetail.Rows.Add(tr_det_head);

                        //        tbldetail.Rows.Add(tr_catg);







                        //        string strlongname = "";
                        //        iCount = 0;
                        //        //loop

                        //        int[] na = new int[500];
                        //        int i = 0;

                        //        foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                        //        {

                        //            TableRow tr_det_sno = new TableRow();
                        //            TableCell tc_det_SNo = new TableCell();
                        //            iCount += 1;
                        //            Literal lit_det_SNo = new Literal();
                        //            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                        //            tc_det_SNo.Attributes.Add("Class", "tbldetail_Data");
                        //            tc_det_SNo.BorderStyle = BorderStyle.Solid;
                        //            tc_det_SNo.BorderWidth = 1;
                        //            tc_det_SNo.Controls.Add(lit_det_SNo);
                        //            tr_det_sno.Cells.Add(tc_det_SNo);

                        //            TableCell tc_det_Ses = new TableCell();
                        //            Literal lit_det_Ses = new Literal();
                        //            lit_det_Ses.Text = "&nbsp;&nbsp;" + drdoctor["Session"].ToString();
                        //            tc_det_Ses.Attributes.Add("Class", "tbldetail_Data");
                        //            tc_det_Ses.BorderStyle = BorderStyle.Solid;
                        //            tc_det_Ses.BorderWidth = 1;
                        //            tc_det_Ses.Controls.Add(lit_det_Ses);
                        //            tr_det_sno.Cells.Add(tc_det_Ses);

                        //            TableCell tc_det_dr_name = new TableCell();
                        //            Literal lit_det_dr_name = new Literal();
                        //            lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["ListedDr_Name"].ToString();
                        //            tc_det_dr_name.Attributes.Add("Class", "tbldetail_Data");
                        //            tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                        //            tc_det_dr_name.BorderWidth = 1;
                        //            tc_det_dr_name.Width = 500;
                        //            tc_det_dr_name.Controls.Add(lit_det_dr_name);
                        //            tr_det_sno.Cells.Add(tc_det_dr_name);

                        //            TableCell tc_det_time = new TableCell();
                        //            Literal lit_det_time = new Literal();
                        //            lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor["Time"].ToString();
                        //            tc_det_time.Attributes.Add("Class", "tbldetail_Data");
                        //            tc_det_time.BorderStyle = BorderStyle.Solid;
                        //            tc_det_time.BorderWidth = 1;
                        //            tc_det_time.Controls.Add(lit_det_time);
                        //            tr_det_sno.Cells.Add(tc_det_time);



                        //            TableCell tc_det_work = new TableCell();
                        //            Literal lit_det_work = new Literal();
                        //            lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                        //            tc_det_work.Attributes.Add("Class", "tbldetail_Data");
                        //            tc_det_work.BorderStyle = BorderStyle.Solid;
                        //            tc_det_work.BorderWidth = 1;
                        //            tc_det_work.Controls.Add(lit_det_work);
                        //            tr_det_sno.Cells.Add(tc_det_work);


                        //            TableCell tc_det_class = new TableCell();
                        //            Literal lit_det_class = new Literal();
                        //            lit_det_class.Text = "&nbsp;&nbsp;" + drdoctor["Doc_ClsName"].ToString();
                        //            tc_det_class.Attributes.Add("Class", "tbldetail_Data");
                        //            tc_det_class.BorderStyle = BorderStyle.Solid;
                        //            tc_det_class.BorderWidth = 1;
                        //            tc_det_class.Width = 500;
                        //            tc_det_class.Controls.Add(lit_det_class);
                        //            tr_det_sno.Cells.Add(tc_det_class);


                        //            TableCell tc_det_spec = new TableCell();
                        //            Literal lit_det_spec = new Literal();
                        //            lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Special_Name"].ToString();
                        //            tc_det_spec.Attributes.Add("Class", "tbldetail_Data");
                        //            tc_det_spec.BorderStyle = BorderStyle.Solid;
                        //            tc_det_spec.BorderWidth = 1;
                        //            tc_det_spec.Width = 500;
                        //            tc_det_spec.Controls.Add(lit_det_spec);
                        //            tr_det_sno.Cells.Add(tc_det_spec);




                        //            string Activity_date3 = string.Empty;
                        //            string datet3 = string.Empty;
                        //            string hdate3 = string.Empty;
                        //            Activity_date3 = drdoc["Activity_Date"].ToString();
                        //            datet3 = Activity_date3.Trim();
                        //            DateTime dtt3 = Convert.ToDateTime(datet3);
                        //            hdate3 = dtt3.ToString("yyyy-MM-dd");

                        //            DCR sf = new DCR();
                        //            dsDrr = sf.dcr_Gettransnox(sf_code, div_code, hdate3, drdoctor["Trans_Detail_Info_Code"].ToString(), drdoctor1["stockist_code"].ToString());
                        //            int[] fi = new int[500];
                        //            int j = 0;
                        //            foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                        //            {
                        //                string pCode = pro1["Product_Detail_Code"].ToString();
                        //                var DSRows = (from w in dsDrr.Tables[0].AsEnumerable() where w.Field<string>("Product_Code") == pCode select w);
                        //                string sQty = string.Empty;
                        //                foreach (var prd in DSRows)
                        //                {
                        //                    sQty = prd.Field<string>("Qty");
                        //                    //Tot_Sec += Decimal.Parse(prd.Field<string>("Qty"));
                        //                }
                        //                TableCell tc = new TableCell();
                        //                tc.BorderStyle = BorderStyle.Solid;
                        //                tc.BorderWidth = 1;
                        //                tc.HorizontalAlign = HorizontalAlign.Center;
                        //                Literal lic = new Literal();
                        //                lic.Text = sQty;

                        //                tc.Attributes.Add("Class", "tbldetail_Data");
                        //                tc.Controls.Add(lic);
                        //                tr_det_sno.Cells.Add(tc);
                        //                fi[j] += sQty == "" ? 0 : Convert.ToInt32(sQty);
                        //                j++;
                        //            }
                        //            for (int l = 0; l < dsTerritory.Tables[0].Rows.Count; l++)
                        //            {
                        //                na[l] += Convert.ToInt32(fi[l]);
                        //            }


                        //            Tot_Sec = 0;


                        //            TableCell tc_det_gift = new TableCell();
                        //            HyperLink lit_det_gift = new HyperLink();
                        //            lit_det_gift.Text = drdoctor["POB_Value"].ToString();
                        //            tc_det_gift.Attributes.Add("Class", "tbldetail_Data");
                        //            tc_det_gift.HorizontalAlign = HorizontalAlign.Center;
                        //            tc_det_gift.BorderStyle = BorderStyle.Solid;
                        //            tc_det_gift.BorderWidth = 1;
                        //            tc_det_gift.Controls.Add(lit_det_gift);
                        //            tr_det_sno.Cells.Add(tc_det_gift);
                        //            stURL = "rpt_dcrproductdetail.aspx?Sf_Code=" + sf_code + "&Activity_date=" + drdoc["Activity_Date"].ToString() + "&div_code=" + div_code + "&Sf_Name=" + Sf_Name + "&retailer_name=" + drdoctor["ListedDr_Name"].ToString() + "&retailer_code=" + drdoctor["Trans_Detail_Info_Code"].ToString() + "";
                        //            lit_det_gift.Attributes["onclick"] = "javascript:window.open('" + stURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                        //            iTotLstCount += Decimal.Parse(drdoctor["POB_Value"].ToString());

                        //            lit_det_gift.NavigateUrl = "#";
                        //            TableCell tc_det_CallFeedBack = new TableCell();
                        //            Literal lit_det_CallFeedBack = new Literal();
                        //            lit_det_CallFeedBack.Text = drdoctor["net_weight_value"].ToString();
                        //            iTotLstCountt += Decimal.Parse(drdoctor["net_weight_value"].ToString());
                        //            tc_det_CallFeedBack.HorizontalAlign = HorizontalAlign.Center;
                        //            tc_det_CallFeedBack.Attributes.Add("Class", "tbldetail_Data");
                        //            tc_det_CallFeedBack.BorderStyle = BorderStyle.Solid;
                        //            tc_det_CallFeedBack.BorderWidth = 1;
                        //            tc_det_CallFeedBack.Controls.Add(lit_det_CallFeedBack);
                        //            tr_det_sno.Cells.Add(tc_det_CallFeedBack);


                        //            TableCell remark = new TableCell();
                        //            Literal remarklit = new Literal();
                        //            remarklit.Text = drdoctor["Activity_Remarks"].ToString();
                        //            remark.HorizontalAlign = HorizontalAlign.Center;
                        //            remark.Attributes.Add("Class", "tbldetail_Data");
                        //            remark.BorderStyle = BorderStyle.Solid;
                        //            remark.BorderWidth = 1;
                        //            remark.Controls.Add(remarklit);
                        //            tr_det_sno.Cells.Add(remark);


                        //            TableCell tc_det_LastUpdate_Date = new TableCell();
                        //            Literal lit_det_time_LastUpdate_Date = new Literal();
                        //            lit_det_time_LastUpdate_Date.Text = "&nbsp;&nbsp;" + drdoctor["ModTime"].ToString();
                        //            tc_det_LastUpdate_Date.Attributes.Add("Class", "tbldetail_Data");
                        //            tc_det_LastUpdate_Date.BorderStyle = BorderStyle.Solid;
                        //            tc_det_LastUpdate_Date.BorderWidth = 1;
                        //            tc_det_LastUpdate_Date.Width = 120;
                        //            tc_det_LastUpdate_Date.Controls.Add(lit_det_time_LastUpdate_Date);
                        //            tr_det_sno.Cells.Add(tc_det_LastUpdate_Date);


                        //            //brand
                        //            string Activity_date31 = string.Empty;
                        //            string datet31 = string.Empty;
                        //            string hdate31 = string.Empty;
                        //            Activity_date3 = drdoc["Activity_Date"].ToString();
                        //            datet3 = Activity_date3.Trim();
                        //            DateTime dtt31 = Convert.ToDateTime(datet3);
                        //            hdate3 = dtt3.ToString("yyyy-MM-dd");

                        //            DCR sf1 = new DCR();
                        //            dsDrr = sf1.dcr_Gettransnox_Brand(sf_code, div_code, hdate3, drdoctor["Trans_Detail_Info_Code"].ToString(), drdoctor1["stockist_code"].ToString());
                        //            int[] fi1 = new int[500];
                        //            int j1 = 0;



                        //            foreach (DataRow pro2 in dsTerritory1.Tables[0].Rows)
                        //            {
                        //                string pCode1 = pro2["Product_Brd_Name"].ToString();
                        //                var DSRows1 = (from w in dsDrr.Tables[0].AsEnumerable() where w.Field<string>("name") == pCode1 select w);

                        //                string sQty = string.Empty;
                        //                string sQty1 = string.Empty;
                        //                foreach (var prd1 in DSRows1)
                        //                {

                        //                    sQty = prd1.Field<string>("al");
                        //                    if (sQty == "1")
                        //                    {

                        //                        sQty = "✓";
                        //                        //sQty = System.Drawing.Color.Green.ToString();
                        //                        //sQty = ((char)0x221A).ToString();
                        //                        //L1.Visible = true;
                        //                    }
                        //                    else
                        //                    {

                        //                        sQty = "X";
                        //                    }

                        //                    //Tot_Sec += Decimal.Parse(prd.Field<string>("Qty"));
                        //                }

                        //                foreach (var prd1 in DSRows1)
                        //                {
                        //                    sQty1 = prd1.Field<string>("ec");
                        //                    //Tot_Sec += Decimal.Parse(prd.Field<string>("Qty"));
                        //                }


                        //                //sQty = "0";
                        //                //sQty1 = "1";
                        //                TableCell tc = new TableCell();
                        //                tc.BorderStyle = BorderStyle.Solid;
                        //                tc.BorderWidth = 1;

                        //                tc.HorizontalAlign = HorizontalAlign.Center;
                        //                Literal lic = new Literal();
                        //                lic.Text = sQty;


                        //                tc.Attributes.Add("Class", "tbldetail_Data");
                        //                //tc.Attributes.Add("fore",)
                        //                tc.Controls.Add(lic);
                        //                tr_det_sno.Cells.Add(tc);
                        //                //2
                        //                 tc = new TableCell();
                        //                tc.BorderStyle = BorderStyle.Solid;
                        //                tc.BorderWidth = 1;
                        //                //tc.ColumnSpan = 2;
                        //                //tc.RowSpan = 2;
                        //                tc.HorizontalAlign = HorizontalAlign.Center;
                        //                lic = new Literal();
                        //                lic.Text = sQty1;
                        //                //lic.Text = sQty1;

                        //                tc.Attributes.Add("Class", "tbldetail_Data");
                        //                tc.Controls.Add(lic);
                        //                tr_det_sno.Cells.Add(tc);
                        //                //fi[j] += sQty == "" ? 0 : Convert.ToInt32(sQty);
                        //                //j++;
                        //            }
                        //            //for (int l = 0; l < dsTerritory1.Tables[0].Rows.Count; l++)
                        //            //{
                        //            //    na[l] += Convert.ToInt32(fi[l]);
                        //            //}







                        //            tbldetail.Rows.Add(tr_det_sno);


                        //        }



                        //        tc_det_head_main2.Controls.Add(tbldetail);
                        //        tr_det_head_main.Cells.Add(tc_det_head_main2);
                        //        tbldetail_main.Rows.Add(tr_det_head_main);

                        //        form1.Controls.Add(tbldetail_main);

                        //        //Total Secondary


                        //        TableRow tr_total = new TableRow();


                        //        TableCell tc_Count_Total = new TableCell();
                        //        tc_Count_Total.BorderStyle = BorderStyle.Solid;
                        //        tc_Count_Total.BorderWidth = 1;

                        //        Literal lit_Count_Total = new Literal();
                        //        lit_Count_Total.Text = "<center>Total Secondary</center>";
                        //        tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
                        //        tc_Count_Total.Controls.Add(lit_Count_Total);
                        //        tc_Count_Total.Font.Bold.ToString();
                        //        tc_Count_Total.BackColor = System.Drawing.Color.White;
                        //        tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
                        //        tc_Count_Total.ColumnSpan = 7;
                        //        tc_Count_Total.Style.Add("text-align", "left");
                        //        tc_Count_Total.Style.Add("font-family", "Calibri");
                        //        tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
                        //        tc_Count_Total.Style.Add("font-size", "10pt");

                        //        tr_total.Cells.Add(tc_Count_Total);




                        //        //prakash
                        //        int k = 0;
                        //        foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                        //        {

                        //            TableCell tc_tot = new TableCell();
                        //            HyperLink hyp_mon = new HyperLink();


                        //            hyp_mon.Text = na[k] == null ? "" : na[k].ToString() == "0" ? "" : na[k].ToString();// Tot_Sec.ToString();

                        //            k++;
                        //            tc_tot.BorderStyle = BorderStyle.Solid;
                        //            tc_tot.BorderWidth = 1;
                        //            tc_tot.BackColor = System.Drawing.Color.White;
                        //            tc_tot.Width = 200;
                        //            tc_tot.Style.Add("font-family", "Calibri");
                        //            tc_tot.Style.Add("font-size", "10pt");
                        //            tc_tot.HorizontalAlign = HorizontalAlign.Center;
                        //            tc_tot.VerticalAlign = VerticalAlign.Middle;
                        //            tc_tot.Controls.Add(hyp_mon);
                        //            tc_tot.Attributes.Add("style", "font-weight:bold;");
                        //            tc_tot.Attributes.Add("Class", "rptCellBorder");
                        //            tr_total.Cells.Add(tc_tot);
                        //        }

                        //        TableCell tc_tot_month = new TableCell();
                        //        HyperLink hyp_month = new HyperLink();


                        //        hyp_month.Text = iTotLstCount.ToString();


                        //        tc_tot_month.BorderStyle = BorderStyle.Solid;
                        //        tc_tot_month.BorderWidth = 1;
                        //        tc_tot_month.BackColor = System.Drawing.Color.White;
                        //        tc_tot_month.Width = 200;
                        //        tc_tot_month.Style.Add("font-family", "Calibri");
                        //        tc_tot_month.Style.Add("font-size", "10pt");
                        //        tc_tot_month.HorizontalAlign = HorizontalAlign.Center;
                        //        tc_tot_month.VerticalAlign = VerticalAlign.Middle;
                        //        tc_tot_month.Controls.Add(hyp_month);
                        //        tc_tot_month.Attributes.Add("style", "font-weight:bold;");
                        //        tc_tot_month.Attributes.Add("Class", "rptCellBorder");
                        //        tr_total.Cells.Add(tc_tot_month);

                        //        iTotLstCount = 0;


                        //        TableCell tc_tot_montht = new TableCell();
                        //        HyperLink hyp_montht = new HyperLink();


                        //        hyp_montht.Text = iTotLstCountt.ToString();


                        //        tc_tot_montht.BorderStyle = BorderStyle.Solid;
                        //        tc_tot_montht.BorderWidth = 1;
                        //        tc_tot_montht.BackColor = System.Drawing.Color.White;
                        //        tc_tot_montht.Width = 200;
                        //        tc_tot_montht.Style.Add("font-family", "Calibri");
                        //        tc_tot_montht.Style.Add("font-size", "10pt");
                        //        tc_tot_montht.HorizontalAlign = HorizontalAlign.Center;
                        //        tc_tot_montht.VerticalAlign = VerticalAlign.Middle;
                        //        tc_tot_montht.Controls.Add(hyp_montht);
                        //        tc_tot_montht.Attributes.Add("style", "font-weight:bold;");
                        //        tc_tot_montht.Attributes.Add("Class", "rptCellBorder");
                        //        tr_total.Cells.Add(tc_tot_montht);

                        //        for (int o = 0; o < 2; o++)
                        //        {

                        //            TableCell tc_tot = new TableCell();
                        //            HyperLink hyp_mon = new HyperLink();


                        //            //hyp_mon.Text = na[k] == null ? "0" : na[k].ToString();// Tot_Sec.ToString();

                        //            //k++;
                        //            tc_tot.BorderStyle = BorderStyle.Solid;
                        //            tc_tot.BorderWidth = 1;
                        //            tc_tot.BackColor = System.Drawing.Color.White;
                        //            tc_tot.Width = 200;
                        //            tc_tot.Style.Add("font-family", "Calibri");
                        //            tc_tot.Style.Add("font-size", "10pt");
                        //            tc_tot.HorizontalAlign = HorizontalAlign.Center;
                        //            tc_tot.VerticalAlign = VerticalAlign.Middle;
                        //            tc_tot.Controls.Add(hyp_mon);
                        //            tc_tot.Attributes.Add("style", "font-weight:bold;");
                        //            tc_tot.Attributes.Add("Class", "rptCellBorder");
                        //            tr_total.Cells.Add(tc_tot);
                        //        }
                        //        iTotLstCountt = 0;

                        //        //Closing Stock

                        //        //TableRow tr_total1 = new TableRow();

                        //        //TableCell tc_Count_Total1 = new TableCell();
                        //        //tc_Count_Total1.BorderStyle = BorderStyle.Solid;
                        //        //tc_Count_Total1.BorderWidth = 1;

                        //        //Literal lit_Count_Total1 = new Literal();
                        //        //lit_Count_Total1.Text = "<center>Closing Stock</center>";
                        //        //tc_Count_Total1.Attributes.Add("style", "color:Red;font-weight:bold;");
                        //        //tc_Count_Total1.Controls.Add(lit_Count_Total1);
                        //        //tc_Count_Total1.Font.Bold.ToString();
                        //        //tc_Count_Total1.BackColor = System.Drawing.Color.White;
                        //        //tc_Count_Total1.Attributes.Add("Class", "tbldetail_main");
                        //        //tc_Count_Total1.ColumnSpan = 7;
                        //        //tc_Count_Total1.Style.Add("text-align", "left");
                        //        //tc_Count_Total1.Style.Add("font-family", "Calibri");
                        //        //tc_Count_Total1.Attributes.Add("Class", "rptCellBorder");
                        //        //tc_Count_Total1.Style.Add("font-size", "10pt");

                        //        //tr_total1.Cells.Add(tc_Count_Total1);


                        //        ////prakash2
                        //        //foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                        //        //{

                        //        //    TableCell tc_tot1 = new TableCell();
                        //        //    HyperLink hyp_mon1 = new HyperLink();


                        //        //    //hyp_mon1.Text = Tot_Sec.ToString();


                        //        //    tc_tot1.BorderStyle = BorderStyle.Solid;
                        //        //    tc_tot1.BorderWidth = 1;
                        //        //    tc_tot1.BackColor = System.Drawing.Color.White;
                        //        //    tc_tot1.Width = 200;
                        //        //    tc_tot1.Style.Add("font-family", "Calibri");
                        //        //    tc_tot1.Style.Add("font-size", "10pt");
                        //        //    tc_tot1.HorizontalAlign = HorizontalAlign.Center;
                        //        //    tc_tot1.VerticalAlign = VerticalAlign.Middle;
                        //        //    tc_tot1.Controls.Add(hyp_mon1);
                        //        //    tc_tot1.Attributes.Add("style", "font-weight:bold;");
                        //        //    tc_tot1.Attributes.Add("Class", "rptCellBorder");
                        //        //    tr_total1.Cells.Add(tc_tot1);
                        //        //}


                        //        //TableCell tc_tot_month1 = new TableCell();
                        //        //HyperLink hyp_month1 = new HyperLink();


                        //        ////hyp_month1.Text = iTotLstCount1.ToString();


                        //        //tc_tot_month1.BorderStyle = BorderStyle.Solid;
                        //        //tc_tot_month1.BorderWidth = 1;
                        //        //tc_tot_month1.BackColor = System.Drawing.Color.White;
                        //        //tc_tot_month1.Width = 200;
                        //        //tc_tot_month1.Style.Add("font-family", "Calibri");
                        //        //tc_tot_month1.Style.Add("font-size", "10pt");
                        //        //tc_tot_month1.HorizontalAlign = HorizontalAlign.Center;
                        //        //tc_tot_month1.VerticalAlign = VerticalAlign.Middle;
                        //        //tc_tot_month1.Controls.Add(hyp_month1);
                        //        //tc_tot_month1.Attributes.Add("style", "font-weight:bold;");
                        //        //tc_tot_month1.Attributes.Add("Class", "rptCellBorder");
                        //        //tr_total1.Cells.Add(tc_tot_month1);

                        //        ////iTotLstCount1 = 0;


                        //        //TableCell tc_tot_montht1 = new TableCell();
                        //        //HyperLink hyp_montht1 = new HyperLink();





                        //        //tc_tot_montht1.BorderStyle = BorderStyle.Solid;
                        //        //tc_tot_montht1.BorderWidth = 1;
                        //        //tc_tot_montht1.BackColor = System.Drawing.Color.White;
                        //        //tc_tot_montht1.Width = 200;
                        //        //tc_tot_montht1.Style.Add("font-family", "Calibri");
                        //        //tc_tot_montht1.Style.Add("font-size", "10pt");
                        //        //tc_tot_montht1.HorizontalAlign = HorizontalAlign.Center;
                        //        //tc_tot_montht1.VerticalAlign = VerticalAlign.Middle;
                        //        //tc_tot_montht1.Controls.Add(hyp_montht1);
                        //        //tc_tot_montht1.Attributes.Add("style", "font-weight:bold;");
                        //        //tc_tot_montht1.Attributes.Add("Class", "rptCellBorder");
                        //        //tr_total1.Cells.Add(tc_tot_montht1);

                        //        //for (int p = 0; p < 2; p++)
                        //        //{

                        //        //    TableCell tc_tot1 = new TableCell();
                        //        //    HyperLink hyp_mon1 = new HyperLink();


                        //        //    //hyp_mon1.Text = Tot_Sec.ToString();


                        //        //    tc_tot1.BorderStyle = BorderStyle.Solid;
                        //        //    tc_tot1.BorderWidth = 1;
                        //        //    tc_tot1.BackColor = System.Drawing.Color.White;
                        //        //    tc_tot1.Width = 200;
                        //        //    tc_tot1.Style.Add("font-family", "Calibri");
                        //        //    tc_tot1.Style.Add("font-size", "10pt");
                        //        //    tc_tot1.HorizontalAlign = HorizontalAlign.Center;
                        //        //    tc_tot1.VerticalAlign = VerticalAlign.Middle;
                        //        //    tc_tot1.Controls.Add(hyp_mon1);
                        //        //    tc_tot1.Attributes.Add("style", "font-weight:bold;");
                        //        //    tc_tot1.Attributes.Add("Class", "rptCellBorder");
                        //        //    tr_total1.Cells.Add(tc_tot1);
                        //        //}


                        //        tbldetail.Rows.Add(tr_total);
                        //        //tbldetail.Rows.Add(tr_total1);

                        //    }
                        //}
                        //else
                        //{

                        if (dsdoc.Tables[0].Rows.Count > 0)
                        {
                            Table tbldetail_main3 = new Table();
                            //tbldetail_main3.BorderStyle = BorderStyle.None;
                            //tbldetail_main3.Width = 1100;
                            TableRow tr_det_head_main3 = new TableRow();
                            //TableCell tc_det_head_main3 = new TableCell();
                            //tc_det_head_main3.Width = 100;
                            //Literal lit_det_main3 = new Literal();
                            //lit_det_main3.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                            //tc_det_head_main3.Controls.Add(lit_det_main3);
                            //tr_det_head_main3.Cells.Add(tc_det_head_main3);

                            TableCell tc_det_head_main4 = new TableCell();
                            //tc_det_head_main4.Width = 1000;

                            Table tbl = new Table();
                            //tbl.Width = 1000;
                            tbl.Style.Add("width", "185%");

                            TableRow tr_day = new TableRow();
                            TableCell tc_day = new TableCell();
                            tc_day.BorderStyle = BorderStyle.None;
                            tc_day.ColumnSpan = 2;
                            tc_day.HorizontalAlign = HorizontalAlign.Center;
                            tc_day.Style.Add("font-name", "verdana;");
                            Literal lit_day = new Literal();
                            tc_day.Controls.Add(lit_day);
                            tr_day.Cells.Add(tc_day);
                            tbl.Rows.Add(tr_day);
                            tc_det_head_main4.Controls.Add(tbl);
                            tr_det_head_main3.Cells.Add(tc_det_head_main4);
                            tbldetail_main3.Rows.Add(tr_det_head_main3);

                            form1.Controls.Add(tbldetail_main3);

                            TableRow tr_ff = new TableRow();

                            TableCell tc_ff_name = new TableCell();
                            tc_ff_name.BorderStyle = BorderStyle.None;
                            tc_ff_name.Width = 500;
                            Literal lit_ff_name = new Literal();
                            lit_ff_name.Text = "<b>Field Force Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "<b>" + Sf_Name.ToString() + "</b>";
                            tc_ff_name.Controls.Add(lit_ff_name);
                            tr_ff.Cells.Add(tc_ff_name);

                            TableCell tc_HQ = new TableCell();
                            tc_HQ.BorderStyle = BorderStyle.None;
                            tc_HQ.Width = 500;

                            tc_HQ.HorizontalAlign = HorizontalAlign.Left;
                            Literal lit_HQ = new Literal();
                            lit_HQ.Text = "<span style='margin-left:200px'><b>Head Quarters </b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + "<b>" + Sf_HQ.ToString() + "</b></span>";
                            tc_HQ.Controls.Add(lit_HQ);
                            tr_ff.Cells.Add(tc_HQ);
                            tbl.Rows.Add(tr_ff);

                            TableRow tr_ff1 = new TableRow();
                            TableCell tc_ff_name1 = new TableCell();
                            tc_ff_name1.BorderStyle = BorderStyle.None;
                            tc_ff_name1.Width = 500;
                            Literal lit_ff_name1 = new Literal();
                            lit_ff_name1.Text = "<b>Daily Call Report</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<span style='color:Red'><b>" + drdoc["Activity_Date"].ToString() + "</b></span>";
                            tc_ff_name1.Controls.Add(lit_ff_name1);
                            tr_ff1.Cells.Add(tc_ff_name1);

                            TableCell tc_HQ1 = new TableCell();
                            tc_HQ1.BorderStyle = BorderStyle.None;
                            tc_HQ1.Width = 500;

                            tc_HQ1.HorizontalAlign = HorizontalAlign.Left;
                            Literal lit_HQ1 = new Literal();
                            lit_HQ1.Text = "<span style='margin-left:200px'><b>Submitted on </b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + "<b>" + drdoc["Submission_Date"].ToString() + "</b></span>";
                            tc_HQ1.Controls.Add(lit_HQ1);
                            tr_ff1.Cells.Add(tc_HQ1);
                            tbl.Rows.Add(tr_ff1);

                            TableRow tr_ff2 = new TableRow();
                            TableCell tc_ff_name2 = new TableCell();
                            tc_ff_name2.BorderStyle = BorderStyle.None;
                            tc_ff_name2.Width = 500;
                            Literal lit_ff_name2 = new Literal();
                            lit_ff_name2.Text = "<b>DB Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dsdoc.Tables[0].Rows[0]["stockist_name"].ToString();
                            tc_ff_name2.Controls.Add(lit_ff_name2);
                            tr_ff2.Cells.Add(tc_ff_name2);

                            TableCell tc_HQ2 = new TableCell();
                            tc_HQ2.BorderStyle = BorderStyle.None;
                            tc_HQ2.Width = 500;

                            tc_HQ2.HorizontalAlign = HorizontalAlign.Left;
                            Literal lit_HQ2 = new Literal();
                            lit_HQ2.Text = "<span style='margin-left:200px'><b>Route </b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + dsdoc.Tables[0].Rows[0]["SDP_Name"].ToString(); ;
                            tc_HQ2.Controls.Add(lit_HQ2);
                            tr_ff2.Cells.Add(tc_HQ2);
                            tbl.Rows.Add(tr_ff2);




                            tc_det_head_main4.Controls.Add(tbl);
                            tr_det_head_main3.Cells.Add(tc_det_head_main4);
                            tbldetail_main3.Rows.Add(tr_det_head_main3);

                            form1.Controls.Add(tbldetail_main3);



                            Table tbldetail_main = new Table();
                            tbldetail_main.BorderStyle = BorderStyle.None;
                            //tbldetail_main.Width = 1100;
                            TableRow tr_det_head_main = new TableRow();
                            //TableCell tc_det_head_main = new TableCell();
                            //tc_det_head_main.Width = 100;
                            //Literal lit_det_main = new Literal();
                            //lit_det_main.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            //tc_det_head_main.Controls.Add(lit_det_main);
                            //tr_det_head_main.Cells.Add(tc_det_head_main);

                            TableCell tc_det_head_main2 = new TableCell();
                            //tc_det_head_main2.Width = 1000;

                            Table tbldetail = new Table();
                            tbldetail.BorderStyle = BorderStyle.Solid;
                            tbldetail.BorderWidth = 1;
                            tbldetail.GridLines = GridLines.Both;
                            //tbldetail.Width = 3500;
                            tbldetail.Style.Add("border-collapse", "collapse");
                            tbldetail.Style.Add("border", "solid 1px Black");
                            tbldetail.Style.Add("padding", "2px 5px");
                            tbldetail.Style.Add("white-space", "nowrap");
                            //tbldetail.Attributes.Add("Class", "table");

                            TableRow tr_det_head = new TableRow();
                            tr_det_head.Attributes.Add("Class", "table");
                            TableCell tc_det_head_SNo = new TableCell();
                            tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                            tc_det_head_SNo.BorderWidth = 1;
                            //tc_det_head_SNo.RowSpan = 2;
                            tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_head_SNo = new Literal();
                            tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                            lit_det_head_SNo.Text = "<b>S.No</b>";
                            tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                            tr_det_head.Cells.Add(tc_det_head_SNo);

                            TableCell tc_det_head_Ses = new TableCell();
                            tc_det_head_Ses.BorderStyle = BorderStyle.Solid;
                            tc_det_head_Ses.BorderWidth = 1;
                            //tc_det_head_Ses.RowSpan = 2;
                            tc_det_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_head_Ses = new Literal();
                            tc_det_head_Ses.Attributes.Add("Class", "tr_det_head");
                            lit_det_head_Ses.Text = "<b>Ses</b>";
                            tc_det_head_Ses.Controls.Add(lit_det_head_Ses);
                            tr_det_head.Cells.Add(tc_det_head_Ses);

                            TableCell tc_det_head_doc = new TableCell();
                            tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                            tc_det_head_doc.BorderWidth = 1;
                            //tc_det_head_doc.RowSpan = 2;
                            tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_head_doc = new Literal();
                            lit_det_head_doc.Text = "<b>Retailer Name</b>";
                            tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                            tc_det_head_doc.Controls.Add(lit_det_head_doc);
                            tr_det_head.Cells.Add(tc_det_head_doc);

                            TableCell tc_det_head_time = new TableCell();
                            tc_det_head_time.BorderStyle = BorderStyle.Solid;
                            tc_det_head_time.BorderWidth = 1;
                            //tc_det_head_time.RowSpan = 2;
                            tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_head_time = new Literal();
                            lit_det_head_time.Text = "<b>Time</b>";
                            tc_det_head_time.Attributes.Add("Class", "tr_det_head");
                            tc_det_head_time.Controls.Add(lit_det_head_time);
                            tr_det_head.Cells.Add(tc_det_head_time);



                            TableCell tc_det_head_ww = new TableCell();
                            tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                            tc_det_head_ww.BorderWidth = 1;
                            //tc_det_head_ww.RowSpan = 2;
                            tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_head_ww = new Literal();
                            lit_det_head_ww.Text = "<b>Worked With</b>";
                            tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                            tc_det_head_ww.Controls.Add(lit_det_head_ww);
                            tr_det_head.Cells.Add(tc_det_head_ww);


                            TableCell tc_det_head_class = new TableCell();
                            tc_det_head_class.BorderStyle = BorderStyle.Solid;
                            tc_det_head_class.BorderWidth = 1;
                            //tc_det_head_class.RowSpan = 2;
                            tc_det_head_class.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_head_cla = new Literal();
                            lit_det_head_cla.Text = "<b>Class</b>";
                            tc_det_head_class.Attributes.Add("Class", "tr_det_head");
                            tc_det_head_class.Controls.Add(lit_det_head_cla);
                            tr_det_head.Cells.Add(tc_det_head_class);


                            TableCell tc_det_head_spec = new TableCell();
                            tc_det_head_spec.BorderStyle = BorderStyle.Solid;
                            tc_det_head_spec.BorderWidth = 1;
                            //tc_det_head_spec.RowSpan = 2;
                            tc_det_head_spec.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_head_spec = new Literal();
                            lit_det_head_spec.Text = "<b>Channel</b>";
                            tc_det_head_spec.Attributes.Add("Class", "tr_det_head");
                            tc_det_head_spec.Controls.Add(lit_det_head_spec);
                            tr_det_head.Cells.Add(tc_det_head_spec);


                            TableCell tc_det_prod = new TableCell();
                            tc_det_prod.BorderStyle = BorderStyle.Solid;
                            Literal lit_det_prod = new Literal();
                            //
                            Territory terr = new Territory();
                            if (div_code == "11" || div_code == "13" || div_code == "8")
                            {
                                string adate = string.Empty;
                                adate = drdoc["Activity_Date"].ToString().Trim();

                                DateTime at = Convert.ToDateTime(adate);
                                adate = at.ToString("yyyy-MM-dd");
                                dsTerritory = terr.getProdName_dcrviewalldates(drdoctor1["sf_code"].ToString(), div_code, adate, dsdoc.Tables[0].Rows[0]["Plan_No"].ToString(), drdoctor1["stockist_code"].ToString());

                            }
                            else
                            {
                                dsTerritory = terr.getProdName(div_code);


                            }
                            foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                            {
                                TableHeaderCell tc = new TableHeaderCell();
                                tc.BorderStyle = BorderStyle.Solid;
                                tc.BorderWidth = 1;
                                //tc.RowSpan = 2;
                                tc.HorizontalAlign = HorizontalAlign.Center;
                                Literal lic = new Literal();
                                if (div_code == "11" || div_code == "13" || div_code == "8")
                                {
                                    lic.Text = "<b>" + pro1["Product_Detail_Name"].ToString() + "</b>";
                                }
                                else
                                {
                                    lic.Text = "<b>" + pro1["Product_Short_Name"].ToString() + "</b>";
                                }
                                tc.Attributes.Add("Class", "tr_det_head");
                                tc.Controls.Add(lic);
                                tr_det_head.Cells.Add(tc);

                            }

                            //
                            //Territory terr = new Territory();
                            //dsTerritory = terr.getProdName(div_code);
                            //foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                            //{
                            //    TableCell tc = new TableCell();
                            //    tc.BorderStyle = BorderStyle.Solid;
                            //    tc.BorderWidth = 1;
                            //    tc.HorizontalAlign = HorizontalAlign.Center;
                            //    Literal lic = new Literal();
                            //    if (div_code == "11" || div_code == "13" || div_code == "8")
                            //    {
                            //        lic.Text = "<b>" + pro1["Product_Detail_Name"].ToString() + "</b>";
                            //    }
                            //    else
                            //    {
                            //        lic.Text = "<b>" + pro1["Product_Short_Name"].ToString() + "</b>";
                            //    }
                            //    tc.Attributes.Add("Class", "tr_det_head");
                            //    tc.Controls.Add(lic);
                            //    tr_det_head.Cells.Add(tc);

                            //}



                            TableCell tc_det_head_gift = new TableCell();
                            tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                            tc_det_head_gift.BorderWidth = 1;
                            //tc_det_head_gift.RowSpan = 2;
                            tc_det_head_gift.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_head_gift = new Literal();
                            lit_det_head_gift.Text = "<b>Order Value</b>";
                            tc_det_head_gift.Attributes.Add("Class", "tr_det_head");
                            tc_det_head_gift.Controls.Add(lit_det_head_gift);
                            tr_det_head.Cells.Add(tc_det_head_gift);


                            TableCell tc_det_head_Quai1 = new TableCell();
                            tc_det_head_Quai1.BorderStyle = BorderStyle.Solid;
                            tc_det_head_Quai1.BorderWidth = 1;
                            //tc_det_head_Quai1.RowSpan = 2;
                            Literal lit_det_head_quai1 = new Literal();
                            lit_det_head_quai1.Text = "<b>Net Weight</b>";
                            tc_det_head_Quai1.Attributes.Add("Class", "tr_det_head");
                            tc_det_head_Quai1.Controls.Add(lit_det_head_quai1);
                            tc_det_head_Quai1.HorizontalAlign = HorizontalAlign.Center;
                            tr_det_head.Cells.Add(tc_det_head_Quai1);


                            TableCell rehead = new TableCell();
                            rehead.BorderStyle = BorderStyle.Solid;
                            rehead.BorderWidth = 1;
                            //rehead.RowSpan = 2;
                            Literal reheadlit = new Literal();
                            reheadlit.Text = "<b>Remarks</b>";
                            rehead.Attributes.Add("Class", "tr_det_head");
                            rehead.Controls.Add(reheadlit);
                            rehead.HorizontalAlign = HorizontalAlign.Center;
                            tr_det_head.Cells.Add(rehead);


                            TableCell tc_det_head_Last_Update_Date = new TableCell();
                            tc_det_head_Last_Update_Date.BorderStyle = BorderStyle.Solid;
                            tc_det_head_Last_Update_Date.BorderWidth = 1;
                            //tc_det_head_Last_Update_Date.RowSpan = 2;
                            tc_det_head_Last_Update_Date.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_head_Last_Update_Date = new Literal();
                            lit_det_head_Last_Update_Date.Text = "<b>Last Updated</b>";
                            tc_det_head_Last_Update_Date.Attributes.Add("Class", "tr_det_head");
                            tc_det_head_Last_Update_Date.Controls.Add(lit_det_head_Last_Update_Date);
                            tr_det_head.Cells.Add(tc_det_head_Last_Update_Date);


                            //brand

                            //TableCell tc_det_brd = new TableCell();
                            //tc_det_brd.BorderStyle = BorderStyle.Solid;
                            //Literal lit_det_brd = new Literal();
                            ////
                            //Territory terr1 = new Territory();
                            ////if (div_code == "11" || div_code == "13" || div_code == "8")
                            ////{
                            ////    string adate = string.Empty;
                            ////    adate = drdoc["Activity_Date"].ToString().Trim();

                            ////    DateTime at = Convert.ToDateTime(adate);
                            ////    adate = at.ToString("yyyy-MM-dd");
                            ////    dsTerritory = terr1.getProdName_dcrviewalldates(drdoctor1["sf_code"].ToString(), div_code, adate, dsdoc.Tables[0].Rows[0]["Plan_No"].ToString(), drdoctor1["stockist_code"].ToString());

                            ////}
                            ////else
                            ////{
                            //dsTerritory1 = terr1.getBrandName(div_code);


                            ////}
                            //TableRow tr_catg = new TableRow();
                            //tr_catg.BackColor = System.Drawing.Color.FromName("#0097AC");
                            //tr_catg.Style.Add("Color", "White");

                            //foreach (DataRow pro1 in dsTerritory1.Tables[0].Rows)
                            //{
                            //    TableHeaderCell tc1 = new TableHeaderCell();
                            //    tc1.BorderStyle = BorderStyle.Solid;
                            //    tc1.BorderWidth = 1;
                            //    tc1.ColumnSpan = 2;
                            //    tc1.HorizontalAlign = HorizontalAlign.Center;
                            //    Literal lic1 = new Literal();
                            //    if (div_code == "11" || div_code == "13" || div_code == "8")
                            //    {
                            //        lic1.Text = "<b>" + pro1["Product_Brd_Name"].ToString() + "</b>";
                            //    }
                            //    else
                            //    {
                            //        lic1.Text = "<b>" + pro1["Product_Brd_SName"].ToString() + "</b>";
                            //    }
                            //    tc1.Attributes.Add("Class", "tr_det_head");
                            //    tc1.Controls.Add(lic1);
                            //    tr_det_head.Cells.Add(tc1);




                            //    TableCell tc_det_head_SNo2 = new TableCell();
                            //    tc_det_head_SNo2.BorderStyle = BorderStyle.Solid;
                            //    tc_det_head_SNo2.BorderWidth = 1;
                            //    tc_det_head_SNo2.Width = 20;
                            //    Literal lit_det_head_SNo2 = new Literal();
                            //    lit_det_head_SNo2.Text = "<b>AL</b>";
                            //    tc_det_head_SNo2.Attributes.Add("Class", "tr_det_head");
                            //    tc_det_head_SNo2.Controls.Add(lit_det_head_SNo2);
                            //    tc_det_head_SNo2.HorizontalAlign = HorizontalAlign.Center;
                            //    tr_catg.Cells.Add(tc_det_head_SNo2);

                            //    TableCell tc_det_head_hq1 = new TableCell();
                            //    tc_det_head_hq1.Width = 20;
                            //    tc_det_head_hq1.BorderStyle = BorderStyle.Solid;
                            //    tc_det_head_hq1.BorderWidth = 1;
                            //    Literal lit_det_head_hq1 = new Literal();
                            //    lit_det_head_hq1.Text = "<b>EC</b>";
                            //    tc_det_head_hq1.Attributes.Add("Class", "tr_det_head");
                            //    tc_det_head_hq1.Controls.Add(lit_det_head_hq1);
                            //    tc_det_head_hq1.HorizontalAlign = HorizontalAlign.Center;
                            //    tr_catg.Cells.Add(tc_det_head_hq1);




                            //}

                            tbldetail.Rows.Add(tr_det_head);

                            //tbldetail.Rows.Add(tr_catg);







                            string strlongname = "";
                            iCount = 0;
                            //loop

                            int[] na = new int[500];
                            int i = 0;

                            foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                            {

                                TableRow tr_det_sno = new TableRow();
                                TableCell tc_det_SNo = new TableCell();
                                iCount += 1;
                                Literal lit_det_SNo = new Literal();
                                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                                tc_det_SNo.Attributes.Add("Class", "tbldetail_Data");
                                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                                tc_det_SNo.BorderWidth = 1;
                                tc_det_SNo.Controls.Add(lit_det_SNo);
                                tr_det_sno.Cells.Add(tc_det_SNo);

                                TableCell tc_det_Ses = new TableCell();
                                Literal lit_det_Ses = new Literal();
                                lit_det_Ses.Text = "&nbsp;&nbsp;" + drdoctor["Session"].ToString();
                                tc_det_Ses.Attributes.Add("Class", "tbldetail_Data");
                                tc_det_Ses.BorderStyle = BorderStyle.Solid;
                                tc_det_Ses.BorderWidth = 1;
                                tc_det_Ses.Controls.Add(lit_det_Ses);
                                tr_det_sno.Cells.Add(tc_det_Ses);

                                TableCell tc_det_dr_name = new TableCell();
                                Literal lit_det_dr_name = new Literal();
                                lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["ListedDr_Name"].ToString();
                                tc_det_dr_name.Attributes.Add("Class", "tbldetail_Data");
                                tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                                tc_det_dr_name.BorderWidth = 1;
                                tc_det_dr_name.Width = 500;
                                tc_det_dr_name.Controls.Add(lit_det_dr_name);
                                tr_det_sno.Cells.Add(tc_det_dr_name);

                                TableCell tc_det_time = new TableCell();
                                Literal lit_det_time = new Literal();
                                lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor["Time"].ToString();
                                tc_det_time.Attributes.Add("Class", "tbldetail_Data");
                                tc_det_time.BorderStyle = BorderStyle.Solid;
                                tc_det_time.BorderWidth = 1;
                                tc_det_time.Controls.Add(lit_det_time);
                                tr_det_sno.Cells.Add(tc_det_time);



                                TableCell tc_det_work = new TableCell();
                                Literal lit_det_work = new Literal();
                                lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                                tc_det_work.Attributes.Add("Class", "tbldetail_Data");
                                tc_det_work.BorderStyle = BorderStyle.Solid;
                                tc_det_work.BorderWidth = 1;
                                tc_det_work.Controls.Add(lit_det_work);
                                tr_det_sno.Cells.Add(tc_det_work);


                                TableCell tc_det_class = new TableCell();
                                Literal lit_det_class = new Literal();
                                lit_det_class.Text = "&nbsp;&nbsp;" + drdoctor["Doc_ClsName"].ToString();
                                tc_det_class.Attributes.Add("Class", "tbldetail_Data");
                                tc_det_class.BorderStyle = BorderStyle.Solid;
                                tc_det_class.BorderWidth = 1;
                                tc_det_class.Width = 500;
                                tc_det_class.Controls.Add(lit_det_class);
                                tr_det_sno.Cells.Add(tc_det_class);


                                TableCell tc_det_spec = new TableCell();
                                Literal lit_det_spec = new Literal();
                                lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Special_Name"].ToString();
                                tc_det_spec.Attributes.Add("Class", "tbldetail_Data");
                                tc_det_spec.BorderStyle = BorderStyle.Solid;
                                tc_det_spec.BorderWidth = 1;
                                tc_det_spec.Width = 500;
                                tc_det_spec.Controls.Add(lit_det_spec);
                                tr_det_sno.Cells.Add(tc_det_spec);




                                string Activity_date3 = string.Empty;
                                string datet3 = string.Empty;
                                string hdate3 = string.Empty;
                                Activity_date3 = drdoc["Activity_Date"].ToString();
                                datet3 = Activity_date3.Trim();
                                DateTime dtt3 = Convert.ToDateTime(datet3);
                                hdate3 = dtt3.ToString("yyyy-MM-dd");

                                DCR sf = new DCR();
                                dsDrr = sf.dcr_Gettransnox(sf_code, div_code, hdate3, drdoctor["Trans_Detail_Info_Code"].ToString(), drdoctor1["stockist_code"].ToString());
                                int[] fi = new int[500];
                                int j = 0;
                                foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                                {
                                    string pCode = pro1["Product_Detail_Code"].ToString();
                                    var DSRows = (from w in dsDrr.Tables[0].AsEnumerable() where w.Field<string>("Product_Code") == pCode select w);
                                    string sQty = string.Empty;
                                    foreach (var prd in DSRows)
                                    {
                                        sQty = prd.Field<string>("Qty");
                                        //Tot_Sec += Decimal.Parse(prd.Field<string>("Qty"));
                                    }
                                    TableCell tc = new TableCell();
                                    tc.BorderStyle = BorderStyle.Solid;
                                    tc.BorderWidth = 1;
                                    tc.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lic = new Literal();
                                    lic.Text = sQty;

                                    tc.Attributes.Add("Class", "tbldetail_Data");
                                    tc.Controls.Add(lic);
                                    tr_det_sno.Cells.Add(tc);
                                    fi[j] += sQty == "" ? 0 : Convert.ToInt32(sQty);
                                    j++;
                                }
                                for (int l = 0; l < dsTerritory.Tables[0].Rows.Count; l++)
                                {
                                    na[l] += Convert.ToInt32(fi[l]);
                                }


                                Tot_Sec = 0;


                                TableCell tc_det_gift = new TableCell();
                                HyperLink lit_det_gift = new HyperLink();
                                lit_det_gift.Text = drdoctor["POB_Value"].ToString();
                                tc_det_gift.Attributes.Add("Class", "tbldetail_Data");
                                tc_det_gift.HorizontalAlign = HorizontalAlign.Center;
                                tc_det_gift.BorderStyle = BorderStyle.Solid;
                                tc_det_gift.BorderWidth = 1;
                                tc_det_gift.Controls.Add(lit_det_gift);
                                tr_det_sno.Cells.Add(tc_det_gift);
                                stURL = "rpt_dcrproductdetail.aspx?Sf_Code=" + sf_code + "&Activity_date=" + drdoc["Activity_Date"].ToString() + "&div_code=" + div_code + "&Sf_Name=" + Sf_Name + "&retailer_name=" + drdoctor["ListedDr_Name"].ToString() + "&retailer_code=" + drdoctor["Trans_Detail_Info_Code"].ToString() + "";
                                lit_det_gift.Attributes["onclick"] = "javascript:window.open('" + stURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                                iTotLstCount += Decimal.Parse(drdoctor["POB_Value"].ToString());

                                lit_det_gift.NavigateUrl = "#";
                                TableCell tc_det_CallFeedBack = new TableCell();
                                Literal lit_det_CallFeedBack = new Literal();
                                lit_det_CallFeedBack.Text = drdoctor["net_weight_value"].ToString();
                                iTotLstCountt += Decimal.Parse(drdoctor["net_weight_value"].ToString());
                                tc_det_CallFeedBack.HorizontalAlign = HorizontalAlign.Center;
                                tc_det_CallFeedBack.Attributes.Add("Class", "tbldetail_Data");
                                tc_det_CallFeedBack.BorderStyle = BorderStyle.Solid;
                                tc_det_CallFeedBack.BorderWidth = 1;
                                tc_det_CallFeedBack.Controls.Add(lit_det_CallFeedBack);
                                tr_det_sno.Cells.Add(tc_det_CallFeedBack);


                                TableCell remark = new TableCell();
                                Literal remarklit = new Literal();
                                remarklit.Text = drdoctor["Activity_Remarks"].ToString();
                                remark.HorizontalAlign = HorizontalAlign.Center;
                                remark.Attributes.Add("Class", "tbldetail_Data");
                                remark.BorderStyle = BorderStyle.Solid;
                                remark.BorderWidth = 1;
                                remark.Controls.Add(remarklit);
                                tr_det_sno.Cells.Add(remark);


                                TableCell tc_det_LastUpdate_Date = new TableCell();
                                Literal lit_det_time_LastUpdate_Date = new Literal();
                                lit_det_time_LastUpdate_Date.Text = "&nbsp;&nbsp;" + drdoctor["ModTime"].ToString();
                                tc_det_LastUpdate_Date.Attributes.Add("Class", "tbldetail_Data");
                                tc_det_LastUpdate_Date.BorderStyle = BorderStyle.Solid;
                                tc_det_LastUpdate_Date.BorderWidth = 1;
                                tc_det_LastUpdate_Date.Width = 120;
                                tc_det_LastUpdate_Date.Controls.Add(lit_det_time_LastUpdate_Date);
                                tr_det_sno.Cells.Add(tc_det_LastUpdate_Date);

                                tbldetail.Rows.Add(tr_det_sno);


                            }



                            tc_det_head_main2.Controls.Add(tbldetail);
                            tr_det_head_main.Cells.Add(tc_det_head_main2);
                            tbldetail_main.Rows.Add(tr_det_head_main);

                            form1.Controls.Add(tbldetail_main);

                            //Total Secondary


                            TableRow tr_total = new TableRow();


                            TableCell tc_Count_Total = new TableCell();
                            tc_Count_Total.BorderStyle = BorderStyle.Solid;
                            tc_Count_Total.BorderWidth = 1;

                            Literal lit_Count_Total = new Literal();
                            lit_Count_Total.Text = "<center>Total Secondary</center>";
                            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
                            tc_Count_Total.Controls.Add(lit_Count_Total);
                            tc_Count_Total.Font.Bold.ToString();
                            tc_Count_Total.BackColor = System.Drawing.Color.White;
                            tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
                            tc_Count_Total.ColumnSpan = 7;
                            tc_Count_Total.Style.Add("text-align", "left");
                            tc_Count_Total.Style.Add("font-family", "Calibri");
                            tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
                            tc_Count_Total.Style.Add("font-size", "10pt");

                            tr_total.Cells.Add(tc_Count_Total);




                            //prakash
                            int k = 0;
                            foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                            {

                                TableCell tc_tot = new TableCell();
                                HyperLink hyp_mon = new HyperLink();


                                hyp_mon.Text = na[k] == null ? "" : na[k].ToString() == "0" ? "" : na[k].ToString();// Tot_Sec.ToString();

                                k++;
                                tc_tot.BorderStyle = BorderStyle.Solid;
                                tc_tot.BorderWidth = 1;
                                tc_tot.BackColor = System.Drawing.Color.White;
                                tc_tot.Width = 200;
                                tc_tot.Style.Add("font-family", "Calibri");
                                tc_tot.Style.Add("font-size", "10pt");
                                tc_tot.HorizontalAlign = HorizontalAlign.Center;
                                tc_tot.VerticalAlign = VerticalAlign.Middle;
                                tc_tot.Controls.Add(hyp_mon);
                                tc_tot.Attributes.Add("style", "font-weight:bold;");
                                tc_tot.Attributes.Add("Class", "rptCellBorder");
                                tr_total.Cells.Add(tc_tot);
                            }

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


                            TableCell tc_tot_montht = new TableCell();
                            HyperLink hyp_montht = new HyperLink();


                            hyp_montht.Text = iTotLstCountt.ToString();


                            tc_tot_montht.BorderStyle = BorderStyle.Solid;
                            tc_tot_montht.BorderWidth = 1;
                            tc_tot_montht.BackColor = System.Drawing.Color.White;
                            tc_tot_montht.Width = 200;
                            tc_tot_montht.Style.Add("font-family", "Calibri");
                            tc_tot_montht.Style.Add("font-size", "10pt");
                            tc_tot_montht.HorizontalAlign = HorizontalAlign.Center;
                            tc_tot_montht.VerticalAlign = VerticalAlign.Middle;
                            tc_tot_montht.Controls.Add(hyp_montht);
                            tc_tot_montht.Attributes.Add("style", "font-weight:bold;");
                            tc_tot_montht.Attributes.Add("Class", "rptCellBorder");
                            tr_total.Cells.Add(tc_tot_montht);

                            for (int o = 0; o < 2; o++)
                            {

                                TableCell tc_tot = new TableCell();
                                HyperLink hyp_mon = new HyperLink();


                                //hyp_mon.Text = na[k] == null ? "0" : na[k].ToString();// Tot_Sec.ToString();

                                //k++;
                                tc_tot.BorderStyle = BorderStyle.Solid;
                                tc_tot.BorderWidth = 1;
                                tc_tot.BackColor = System.Drawing.Color.White;
                                tc_tot.Width = 200;
                                tc_tot.Style.Add("font-family", "Calibri");
                                tc_tot.Style.Add("font-size", "10pt");
                                tc_tot.HorizontalAlign = HorizontalAlign.Center;
                                tc_tot.VerticalAlign = VerticalAlign.Middle;
                                tc_tot.Controls.Add(hyp_mon);
                                tc_tot.Attributes.Add("style", "font-weight:bold;");
                                tc_tot.Attributes.Add("Class", "rptCellBorder");
                                tr_total.Cells.Add(tc_tot);
                            }
                            iTotLstCountt = 0;

                            //Closing Stock

                            //TableRow tr_total1 = new TableRow();

                            //TableCell tc_Count_Total1 = new TableCell();
                            //tc_Count_Total1.BorderStyle = BorderStyle.Solid;
                            //tc_Count_Total1.BorderWidth = 1;

                            //Literal lit_Count_Total1 = new Literal();
                            //lit_Count_Total1.Text = "<center>Closing Stock</center>";
                            //tc_Count_Total1.Attributes.Add("style", "color:Red;font-weight:bold;");
                            //tc_Count_Total1.Controls.Add(lit_Count_Total1);
                            //tc_Count_Total1.Font.Bold.ToString();
                            //tc_Count_Total1.BackColor = System.Drawing.Color.White;
                            //tc_Count_Total1.Attributes.Add("Class", "tbldetail_main");
                            //tc_Count_Total1.ColumnSpan = 7;
                            //tc_Count_Total1.Style.Add("text-align", "left");
                            //tc_Count_Total1.Style.Add("font-family", "Calibri");
                            //tc_Count_Total1.Attributes.Add("Class", "rptCellBorder");
                            //tc_Count_Total1.Style.Add("font-size", "10pt");

                            //tr_total1.Cells.Add(tc_Count_Total1);


                            ////prakash2
                            //foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                            //{

                            //    TableCell tc_tot1 = new TableCell();
                            //    HyperLink hyp_mon1 = new HyperLink();


                            //    //hyp_mon1.Text = Tot_Sec.ToString();


                            //    tc_tot1.BorderStyle = BorderStyle.Solid;
                            //    tc_tot1.BorderWidth = 1;
                            //    tc_tot1.BackColor = System.Drawing.Color.White;
                            //    tc_tot1.Width = 200;
                            //    tc_tot1.Style.Add("font-family", "Calibri");
                            //    tc_tot1.Style.Add("font-size", "10pt");
                            //    tc_tot1.HorizontalAlign = HorizontalAlign.Center;
                            //    tc_tot1.VerticalAlign = VerticalAlign.Middle;
                            //    tc_tot1.Controls.Add(hyp_mon1);
                            //    tc_tot1.Attributes.Add("style", "font-weight:bold;");
                            //    tc_tot1.Attributes.Add("Class", "rptCellBorder");
                            //    tr_total1.Cells.Add(tc_tot1);
                            //}


                            //TableCell tc_tot_month1 = new TableCell();
                            //HyperLink hyp_month1 = new HyperLink();


                            ////hyp_month1.Text = iTotLstCount1.ToString();


                            //tc_tot_month1.BorderStyle = BorderStyle.Solid;
                            //tc_tot_month1.BorderWidth = 1;
                            //tc_tot_month1.BackColor = System.Drawing.Color.White;
                            //tc_tot_month1.Width = 200;
                            //tc_tot_month1.Style.Add("font-family", "Calibri");
                            //tc_tot_month1.Style.Add("font-size", "10pt");
                            //tc_tot_month1.HorizontalAlign = HorizontalAlign.Center;
                            //tc_tot_month1.VerticalAlign = VerticalAlign.Middle;
                            //tc_tot_month1.Controls.Add(hyp_month1);
                            //tc_tot_month1.Attributes.Add("style", "font-weight:bold;");
                            //tc_tot_month1.Attributes.Add("Class", "rptCellBorder");
                            //tr_total1.Cells.Add(tc_tot_month1);

                            ////iTotLstCount1 = 0;


                            //TableCell tc_tot_montht1 = new TableCell();
                            //HyperLink hyp_montht1 = new HyperLink();





                            //tc_tot_montht1.BorderStyle = BorderStyle.Solid;
                            //tc_tot_montht1.BorderWidth = 1;
                            //tc_tot_montht1.BackColor = System.Drawing.Color.White;
                            //tc_tot_montht1.Width = 200;
                            //tc_tot_montht1.Style.Add("font-family", "Calibri");
                            //tc_tot_montht1.Style.Add("font-size", "10pt");
                            //tc_tot_montht1.HorizontalAlign = HorizontalAlign.Center;
                            //tc_tot_montht1.VerticalAlign = VerticalAlign.Middle;
                            //tc_tot_montht1.Controls.Add(hyp_montht1);
                            //tc_tot_montht1.Attributes.Add("style", "font-weight:bold;");
                            //tc_tot_montht1.Attributes.Add("Class", "rptCellBorder");
                            //tr_total1.Cells.Add(tc_tot_montht1);

                            //for (int p = 0; p < 2; p++)
                            //{

                            //    TableCell tc_tot1 = new TableCell();
                            //    HyperLink hyp_mon1 = new HyperLink();


                            //    //hyp_mon1.Text = Tot_Sec.ToString();


                            //    tc_tot1.BorderStyle = BorderStyle.Solid;
                            //    tc_tot1.BorderWidth = 1;
                            //    tc_tot1.BackColor = System.Drawing.Color.White;
                            //    tc_tot1.Width = 200;
                            //    tc_tot1.Style.Add("font-family", "Calibri");
                            //    tc_tot1.Style.Add("font-size", "10pt");
                            //    tc_tot1.HorizontalAlign = HorizontalAlign.Center;
                            //    tc_tot1.VerticalAlign = VerticalAlign.Middle;
                            //    tc_tot1.Controls.Add(hyp_mon1);
                            //    tc_tot1.Attributes.Add("style", "font-weight:bold;");
                            //    tc_tot1.Attributes.Add("Class", "rptCellBorder");
                            //    tr_total1.Cells.Add(tc_tot1);
                            //}


                            tbldetail.Rows.Add(tr_total);
                            //tbldetail.Rows.Add(tr_total1);

                        }

                        //}

                        //}
                        //end
                    }

                    //Table jan-9
                    //if (dsdoc1.Tables[0].Rows.Count >= 1)
                    //{
                    //    Table tbl_head_empty1 = new Table();
                    //    TableRow tr_head_empty1 = new TableRow();
                    //    TableCell tc_head_empty1 = new TableCell();
                    //    Literal lit_head_empty1 = new Literal();
                    //    lit_head_empty1.Text = "<BR>";
                    //    tc_head_empty1.Controls.Add(lit_head_empty1);
                    //    tr_head_empty1.Cells.Add(tc_head_empty1);
                    //    tbl_head_empty1.Rows.Add(tr_head_empty1);
                    //    form1.Controls.Add(tbl_head_empty1);

                    //    Table tbldetail_main1 = new Table();
                    //    tbldetail_main1.BorderStyle = BorderStyle.None;
                    //    //tbldetail_main1.Width = 1100;
                    //    TableRow tr_det_head_main1 = new TableRow();
                    //    TableCell tc_det_head_main1 = new TableCell();
                    //    tc_det_head_main1.Style.Add("width", "100%");
                    //    Literal lit_det_main1 = new Literal();
                    //    lit_det_main1.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    //    Table tbldetail1 = new Table();
                    //    tbldetail1.BorderStyle = BorderStyle.Solid;
                    //    tbldetail1.BorderWidth = 1;
                    //    tbldetail1.GridLines = GridLines.Both;
                    //    tbldetail1.Width = 500;
                    //    tbldetail1.Style.Add("border-collapse", "collapse");
                    //    tbldetail1.Style.Add("border", "solid 1px Black");

                    //    TableRow tr_det_head1 = new TableRow();
                    //    TableCell tc_det_head_SNo1 = new TableCell();
                    //    tc_det_head_SNo1.BorderStyle = BorderStyle.Solid;
                    //    tc_det_head_SNo1.BorderWidth = 1;
                    //    tc_det_head_SNo1.ColumnSpan = 10;
                    //    tc_det_head_SNo1.HorizontalAlign = HorizontalAlign.Center;
                    //    Literal lit_det_head_SNo1 = new Literal();
                    //    tc_det_head_SNo1.Attributes.Add("Class", "tr_det_head");
                    //    lit_det_head_SNo1.Text = "<b>Summary of the Day</b>";
                    //    tc_det_head_SNo1.Controls.Add(lit_det_head_SNo1);
                    //    tr_det_head1.Cells.Add(tc_det_head_SNo1);

                    //    DCR sf6 = new DCR();
                    //    dsDrr = sf6.dcr_Getcount(sf_code, div_code, drdoc["Activity_Date"].ToString());
                    //    foreach (DataRow coun in dsDrr.Tables[0].Rows)
                    //    {
                    //        TableRow tr_catg = new TableRow();
                    //        tr_catg.BackColor = System.Drawing.Color.FromName("#0097AC");
                    //        tr_catg.Style.Add("Color", "White");



                    //        TableCell tc_det_head_SNo2 = new TableCell();
                    //        tc_det_head_SNo2.BorderStyle = BorderStyle.Solid;
                    //        tc_det_head_SNo2.BorderWidth = 1;
                    //        tc_det_head_SNo2.Width = 20;
                    //        Literal lit_det_head_SNo2 = new Literal();
                    //        lit_det_head_SNo2.Text = "<b>TC</b>";
                    //        tc_det_head_SNo2.Attributes.Add("Class", "tblHead");
                    //        tc_det_head_SNo2.Controls.Add(lit_det_head_SNo2);
                    //        tc_det_head_SNo2.HorizontalAlign = HorizontalAlign.Center;
                    //        tr_catg.Cells.Add(tc_det_head_SNo2);


                    //        TableCell tc_det_head_doc1 = new TableCell();
                    //        tc_det_head_doc1.Width = 20;
                    //        tc_det_head_doc1.BorderStyle = BorderStyle.Solid;
                    //        tc_det_head_doc1.BorderWidth = 1;
                    //        Literal lit_det_head_doc1 = new Literal();
                    //        lit_det_head_doc1.Text = coun["Total_call_count"].ToString();
                    //        //tc_det_head_doc1.Attributes.Add("Class", "tbldetail_Data");
                    //        tc_det_head_doc1.Controls.Add(lit_det_head_doc1);
                    //        tc_det_head_doc1.HorizontalAlign = HorizontalAlign.Center;
                    //        tr_catg.Cells.Add(tc_det_head_doc1);

                    //        TableCell tc_det_head_hq1 = new TableCell();
                    //        tc_det_head_hq1.Width = 20;
                    //        tc_det_head_hq1.BorderStyle = BorderStyle.Solid;
                    //        tc_det_head_hq1.BorderWidth = 1;
                    //        Literal lit_det_head_hq1 = new Literal();
                    //        lit_det_head_hq1.Text = "<b>EC</b>";
                    //        tc_det_head_hq1.Attributes.Add("Class", "tblHead");
                    //        tc_det_head_hq1.Controls.Add(lit_det_head_hq1);
                    //        tc_det_head_hq1.HorizontalAlign = HorizontalAlign.Center;
                    //        tr_catg.Cells.Add(tc_det_head_hq1);

                    //        TableCell tc_det_head_hq2 = new TableCell();
                    //        tc_det_head_hq2.Width = 20;
                    //        tc_det_head_hq2.BorderStyle = BorderStyle.Solid;
                    //        tc_det_head_hq2.BorderWidth = 1;
                    //        Literal lit_det_head_hq2 = new Literal();
                    //        lit_det_head_hq2.Text = coun["Effictive_call_Count"].ToString();
                    //        tc_det_head_hq2.Attributes.Add("Class", "tblHead");
                    //        tc_det_head_hq2.Controls.Add(lit_det_head_hq2);
                    //        tc_det_head_hq2.HorizontalAlign = HorizontalAlign.Center;
                    //        tr_catg.Cells.Add(tc_det_head_hq2);

                    //        TableCell tc_det_head_hq3 = new TableCell();
                    //        tc_det_head_hq3.Width = 70;
                    //        tc_det_head_hq3.BorderStyle = BorderStyle.Solid;
                    //        tc_det_head_hq3.BorderWidth = 1;
                    //        tc_det_head_hq3.ColumnSpan = 10;
                    //        Literal lit_det_head_hq3 = new Literal();
                    //        lit_det_head_hq3.Text = "<b>Category Wise effective Calls</b>";
                    //        tc_det_head_hq3.Attributes.Add("Class", "tblHead");
                    //        tc_det_head_hq3.Controls.Add(lit_det_head_hq3);
                    //        tc_det_head_hq3.HorizontalAlign = HorizontalAlign.Center;

                    //        tr_catg.Cells.Add(tc_det_head_hq3);



                    //        tbldetail1.Rows.Add(tr_det_head1);
                    //        tbldetail1.Rows.Add(tr_catg);
                    //    }


                    //    TableRow tr_det_sno1 = new TableRow();

                    //    TableCell tc_det_SNo1 = new TableCell();
                    //    iCount += 1;
                    //    Literal lit_det_SNo1 = new Literal();
                    //    Chart _checkbox = new Chart();

                    //    Series series = new Series("Default");
                    //    series.ChartType = SeriesChartType.Doughnut;
                    //    _checkbox.Series.Add(series);

                    //    //Create chart legend
                    //    Legend legend = new Legend();
                    //    _checkbox.Legends.Add(legend);

                    //    // Define the chart area
                    //    ChartArea chartArea = new ChartArea();
                    //    ChartArea3DStyle areaStyle = new ChartArea3DStyle(chartArea);
                    //    areaStyle.Rotation = 0;
                    //    _checkbox.ChartAreas.Add(chartArea);
                    //    chartArea.Area3DStyle.Enable3D = true;
                    //    DataTable dt = new DataTable();

                    //    dt.Columns.Add("Total_call_count");
                    //    dt.Columns.Add("Effictive_call_Count");

                    //    foreach (DataRow coun in dsDrr.Tables[0].Rows)
                    //    {
                    //        // dt.Rows.Add(Convert.ToInt32(coun["Total_call_count"]) - Convert.ToInt32(coun["Effictive_call_Count"]) + "(" + Convert.ToInt32(coun["Total_call_count"]) + "-" + Convert.ToInt32(coun["Effictive_call_Count"]) + ")");
                    //        dt.Rows.Add(Convert.ToInt32(coun["Total_call_count"]) - Convert.ToInt32(coun["Effictive_call_Count"]));
                    //        dt.Rows.Add(coun["Effictive_call_Count"]);



                    //        _checkbox.Series["Default"].XValueMember = "Total_call_count";
                    //        _checkbox.Series["Default"].YValueMembers = "Total_call_count";
                    //        _checkbox.Series["Default"].IsValueShownAsLabel = true;
                    //        _checkbox.Series["Default"].IsVisibleInLegend = true;
                    //        // _checkbox.Series["Default"].LegendText = "("+Convert.ToInt32(coun["Total_call_count"])+"-"+Convert.ToInt32(coun["Effictive_call_Count"])+")";
                    //        // _checkbox.Series[0].Label= "#VALX (" + Convert.ToInt32(coun["Total_call_count"]) + "-" + Convert.ToInt32(coun["Effictive_call_Count"]) + ")";
                    //        // _checkbox.Series[0].Label = "#VALY";

                    //        _checkbox.Series["Default"].ChartType = SeriesChartType.Doughnut;
                    //        _checkbox.DataSource = dt;
                    //        _checkbox.DataBind();
                    //    }
                    //    for (int cnt = 0; cnt < _checkbox.Series["Default"].Points.Count; cnt++)
                    //    {
                    //        _checkbox.Series["Default"].Points[cnt].ToolTip = dt.Rows[cnt]["Total_call_count"].ToString();
                    //    };
                    //    tc_det_SNo1.Controls.Add(_checkbox);
                    //    tc_det_SNo1.Attributes.Add("Class", "tbldetail_Data");
                    //    tc_det_SNo1.BorderStyle = BorderStyle.Solid;
                    //    tc_det_SNo1.BorderWidth = 1;
                    //    tc_det_SNo1.ColumnSpan = 4;
                    //    tc_det_SNo1.Controls.Add(lit_det_SNo1);
                    //    tr_det_sno1.Cells.Add(tc_det_SNo1);


                    //    //barchar 2

                    //    TableCell tc_det_SNo12 = new TableCell();
                    //    iCount += 1;
                    //    Literal lit_det_SNo12 = new Literal();
                    //    Chart _checkbox2 = new Chart();
                    //    _checkbox2.Width = 600;
                    //    Series series2 = new Series("Default");
                    //    series2.ChartType = SeriesChartType.Bar;
                    //    series2.Label = "#VALY";
                    //    series2.LabelForeColor = Color.White;
                    //    series2["LabelStyle"] = "Bottom";
                    //    _checkbox2.Series.Add(series2);

                    //    //Create chart legend
                    //    Legend legend2 = new Legend();
                    //    _checkbox2.Legends.Add(legend2);

                    //    // Define the chart area
                    //    ChartArea chartArea2 = new ChartArea();
                    //    ChartArea3DStyle areaStyle2 = new ChartArea3DStyle(chartArea2);
                    //    areaStyle2.Rotation = 0;
                    //    _checkbox2.ChartAreas.Add(chartArea2);
                    //    chartArea2.Area3DStyle.Enable3D = true;
                    //    DataTable dt3 = new DataTable();

                    //    dt3.Columns.Add(new DataColumn("Product_Cat_Name"));
                    //    dt3.Columns.Add(new DataColumn("cou"));
                    //    DCR sf3 = new DCR();
                    //    dsDrr = sf3.dcr_Getcount1(sf_code, div_code, drdoc["Activity_Date"].ToString());
                    //    foreach (DataRow coun1 in dsDrr.Tables[0].Rows)
                    //    {
                    //        dt3.Rows.Add(coun1["Product_Cat_Name"], coun1["cou"]);
                    //    }

                    //    _checkbox2.Series["Default"].XValueMember = "Product_Cat_Name";
                    //    _checkbox2.Series["Default"].YValueMembers = "cou";
                    //    _checkbox2.Series["Default"].YValuesPerPoint = 5;
                    //    _checkbox2.Series["Default"].ChartType = SeriesChartType.Bar;
                    //    //Chart1.Series[0].ToolTip = "#VALY";
                    //    _checkbox2.DataSource = dt3;
                    //    _checkbox2.DataBind();

                    //    for (int cnt = 0; cnt < _checkbox2.Series["Default"].Points.Count; cnt++)
                    //    {
                    //        _checkbox2.Series["Default"].Points[cnt].ToolTip = dt3.Rows[cnt]["Product_Cat_Name"].ToString();
                    //    };
                    //    tc_det_SNo12.Controls.Add(_checkbox2);
                    //    tc_det_SNo12.Attributes.Add("Class", "tbldetail_Data");
                    //    tc_det_SNo12.BorderStyle = BorderStyle.Solid;
                    //    tc_det_SNo12.BorderWidth = 1;
                    //    tc_det_SNo12.ColumnSpan = 4;
                    //    tc_det_SNo12.Controls.Add(lit_det_SNo12);
                    //    tr_det_sno1.Cells.Add(tc_det_SNo12);


                    //    //bar char








                    //    tbldetail1.Rows.Add(tr_det_sno1);







                    //    tc_det_head_main1.Controls.Add(tbldetail1);

                    //    tc_det_head_main1.Controls.Add(lit_det_main1);
                    //    tr_det_head_main1.Cells.Add(tc_det_head_main1);


                    //    //Itemized Summary
                    //    TableCell tc_det_head_main21 = new TableCell();
                    //    tc_det_head_main21.Width = 1000;




                    //    Table tbl_head_empty14 = new Table();
                    //    TableRow tr_head_empty14 = new TableRow();
                    //    TableCell tc_head_empty14 = new TableCell();
                    //    Literal lit_head_empty14 = new Literal();
                    //    lit_head_empty14.Text = "<BR>";
                    //    tc_head_empty14.Controls.Add(lit_head_empty14);
                    //    tr_head_empty14.Cells.Add(tc_head_empty14);
                    //    tbl_head_empty14.Rows.Add(tr_head_empty14);
                    //    form1.Controls.Add(tbl_head_empty14);


                    //    Table tbldetail7 = new Table();
                    //    tbldetail7.BorderStyle = BorderStyle.Solid;
                    //    tbldetail7.BorderWidth = 1;
                    //    tbldetail7.CellSpacing = 2;
                    //    tbldetail7.GridLines = GridLines.Both;
                    //    tbldetail7.Width = 500;
                    //    tbldetail7.Style.Add("border-collapse", "collapse");
                    //    tbldetail7.Style.Add("border", "solid 1px Black");

                    //    string Activity_date1 = string.Empty;
                    //    string datet1 = string.Empty;
                    //    string hdate1 = string.Empty;
                    //    Activity_date1 = drdoc["Activity_Date"].ToString();
                    //    datet1 = Activity_date1.Trim();
                    //    DateTime dtt1 = Convert.ToDateTime(datet1);
                    //    hdate1 = dtt1.ToString("yyyy-MM-dd");
                    //    DCR sf8 = new DCR();
                    //    dsDrr = sf8.dcr_Item_sum(sf_code, div_code, hdate1);
                    //    TableRow tr_det_head15 = new TableRow();



                    //    TableCell tc_det_head_hq35 = new TableCell();
                    //    tc_det_head_hq35.Width = 300;
                    //    tc_det_head_hq35.BorderStyle = BorderStyle.Solid;
                    //    tc_det_head_hq35.BorderWidth = 1;
                    //    tc_det_head_hq35.ColumnSpan = 1;
                    //    Literal lit_det_head_hq35 = new Literal();
                    //    lit_det_head_hq35.Text = "<b>Item Name</b>";
                    //    tc_det_head_hq35.Attributes.Add("Class", "tr_det_head");
                    //    tc_det_head_hq35.Controls.Add(lit_det_head_hq35);
                    //    tc_det_head_hq35.HorizontalAlign = HorizontalAlign.Center;


                    //    tr_det_head15.Cells.Add(tc_det_head_hq35);

                    //    TableCell tc_det_head_hq35r = new TableCell();
                    //    tc_det_head_hq35r.Width = 70;
                    //    tc_det_head_hq35r.BorderStyle = BorderStyle.Solid;
                    //    tc_det_head_hq35r.BorderWidth = 1;
                    //    tc_det_head_hq35r.ColumnSpan = 1;
                    //    Literal lit_det_head_hq35r = new Literal();
                    //    lit_det_head_hq35r.Text = "<b>Qty</b>";
                    //    tc_det_head_hq35r.Attributes.Add("Class", "tr_det_head");
                    //    tc_det_head_hq35r.Controls.Add(lit_det_head_hq35r);
                    //    tc_det_head_hq35r.HorizontalAlign = HorizontalAlign.Center;


                    //    tr_det_head15.Cells.Add(tc_det_head_hq35r);


                    //    TableCell tc_det_head_hq35r1 = new TableCell();
                    //    tc_det_head_hq35r1.Width = 70;
                    //    tc_det_head_hq35r1.BorderStyle = BorderStyle.Solid;
                    //    tc_det_head_hq35r1.BorderWidth = 1;
                    //    tc_det_head_hq35r1.ColumnSpan = 1;
                    //    Literal lit_det_head_hq35r1 = new Literal();
                    //    lit_det_head_hq35r1.Text = "<b>Value</b>";
                    //    tc_det_head_hq35r1.Attributes.Add("Class", "tr_det_head");
                    //    tc_det_head_hq35r1.Controls.Add(lit_det_head_hq35r1);
                    //    tc_det_head_hq35r1.HorizontalAlign = HorizontalAlign.Center;


                    //    tr_det_head15.Cells.Add(tc_det_head_hq35r1);

                    //    TableCell tc_det_head_hq35r12 = new TableCell();
                    //    tc_det_head_hq35r12.Width = 300;
                    //    tc_det_head_hq35r12.BorderStyle = BorderStyle.Solid;
                    //    tc_det_head_hq35r12.BorderWidth = 1;
                    //    tc_det_head_hq35r12.ColumnSpan = 1;
                    //    Literal lit_det_head_hq35r12 = new Literal();
                    //    lit_det_head_hq35r12.Text = "<b>Net Weight</b>";
                    //    tc_det_head_hq35r12.Attributes.Add("Class", "tr_det_head");
                    //    tc_det_head_hq35r12.Controls.Add(lit_det_head_hq35r12);
                    //    tc_det_head_hq35r12.HorizontalAlign = HorizontalAlign.Center;

                    //    tr_det_head15.Cells.Add(tc_det_head_hq35r12);

                    //    Table new_tab = new Table();
                    //    TableRow tr_det_head_le = new TableRow();

                    //    TableCell tc_det_SNo15 = new TableCell();
                    //    tc_det_SNo15.BorderStyle = BorderStyle.Solid;
                    //    tc_det_SNo15.BorderWidth = 1;
                    //    tc_det_SNo15.RowSpan = 120;
                    //    tc_det_SNo15.HorizontalAlign = HorizontalAlign.Center;
                    //    Literal lit_det_head_SNo5 = new Literal();
                    //    tc_det_SNo15.Attributes.Add("Class", "tr_det_head");
                    //    lit_det_head_SNo5.Text = "<b>Itemized Summary</b>";
                    //    tc_det_SNo15.Controls.Add(lit_det_head_SNo5);

                    //    tr_det_head_le.Cells.Add(tc_det_SNo15);



                    //    iCount += 1;
                    //    Literal lit_det_SNo15 = new Literal();
                    //    Chart _checkbox5 = new Chart();

                    //    Series series5 = new Series("Default");
                    //    series5.ChartType = SeriesChartType.Pie;
                    //    _checkbox5.Series.Add(series5);

                    //    //Create chart legend
                    //    Legend legend5 = new Legend();
                    //    _checkbox5.Legends.Add(legend5);

                    //    // Define the chart area
                    //    ChartArea chartArea5 = new ChartArea();
                    //    ChartArea3DStyle areaStyle5 = new ChartArea3DStyle(chartArea5);
                    //    areaStyle5.Rotation = 0;
                    //    _checkbox5.ChartAreas.Add(chartArea5);
                    //    chartArea5.Area3DStyle.Enable3D = true;

                    //    DataTable dt5 = new DataTable();


                    //    dt5.Columns.Add("Product_Short_Name");
                    //    dt5.Columns.Add("Quantity");



                    //    foreach (DataRow dritem in dsDrr.Tables[0].Rows)
                    //    {

                    //        if (div_code == "11" || div_code == "13" || div_code == "8")
                    //        {
                    //            dt5.Rows.Add(dritem["Product_Detail_Name"], dritem["Quantity"]);
                    //        }
                    //        else
                    //        {
                    //            dt5.Rows.Add(dritem["Product_Short_Name"], dritem["Quantity"]);
                    //        }
                    //    }


                    //    _checkbox5.Series["Default"].XValueMember = "Product_Short_Name";
                    //    _checkbox5.Series["Default"].YValueMembers = "Quantity";
                    //    _checkbox5.Series["Default"].YValuesPerPoint = 5;
                    //    _checkbox5.Series["Default"].ChartType = SeriesChartType.Pie;
                    //    _checkbox5.Series["Default"].IsValueShownAsLabel = true;
                    //    //Chart1.Series[0].ToolTip = "#VALY";
                    //    _checkbox5.DataSource = dt5;
                    //    _checkbox5.DataBind();

                    //    for (int cnt = 0; cnt < _checkbox5.Series["Default"].Points.Count; cnt++)
                    //    {
                    //        _checkbox5.Series["Default"].Points[cnt].ToolTip = dt5.Rows[cnt]["Quantity"].ToString();
                    //    };
                    //    tc_det_SNo15.Controls.Add(_checkbox5);
                    //    tc_det_SNo15.Attributes.Add("Class", "tbldetail_Data");
                    //    tc_det_SNo15.BorderStyle = BorderStyle.Solid;
                    //    tc_det_SNo15.BorderWidth = 1;
                    //    tc_det_SNo15.Controls.Add(lit_det_SNo15);
                    //    tr_det_head15.Cells.Add(tc_det_SNo15);

                    //    tbldetail7.Controls.Add(tr_det_head15);




                    //    //tbldetail7.Rows.Add(tr_det_head15);
                    //    tbldetail7.Rows.Add(tr_det_head15);



                    //    foreach (DataRow dritem in dsDrr.Tables[0].Rows)
                    //    {


                    //        TableRow tr_det_sno15 = new TableRow();
                    //        TableCell tc_det_SNo2r = new TableCell();
                    //        tc_det_SNo2r.BorderWidth = 1;
                    //        Literal lit_det_SNo2r = new Literal();
                    //        tc_det_SNo2r.BorderStyle = BorderStyle.Solid;
                    //        if (div_code == "11" || div_code == "13" || div_code == "8")
                    //        {
                    //            lit_det_SNo2r.Text = dritem["Product_Detail_Name"].ToString();
                    //        }
                    //        else
                    //        {
                    //            lit_det_SNo2r.Text = dritem["Product_Short_Name"].ToString();
                    //        }
                    //        tc_det_SNo2r.Attributes.Add("Class", "tbldetail_Data");
                    //        tc_det_SNo2r.Controls.Add(lit_det_SNo2r);
                    //        tr_det_sno15.Cells.Add(tc_det_SNo2r);


                    //        TableCell tc_det_SNo2r1 = new TableCell();

                    //        tc_det_SNo2r1.BorderStyle = BorderStyle.Solid;
                    //        tc_det_SNo2r1.BorderWidth = 1;
                    //        Literal lit_det_SNo2r1 = new Literal();
                    //        lit_det_SNo2r1.Text = dritem["Quantity"].ToString();
                    //        tc_det_SNo2r1.Attributes.Add("Class", "tbldetail_Data");
                    //        tc_det_SNo2r1.Controls.Add(lit_det_SNo2r1);

                    //        tr_det_sno15.Cells.Add(tc_det_SNo2r1);


                    //        TableCell tc_det_SNo2r2 = new TableCell();

                    //        tc_det_SNo2r2.BorderStyle = BorderStyle.Solid;
                    //        tc_det_SNo2r2.BorderWidth = 1;
                    //        Literal lit_det_SNo2r2 = new Literal();

                    //        lit_det_SNo2r2.Text = dritem["Value"].ToString();
                    //        tc_det_SNo2r2.Attributes.Add("Class", "tbldetail_Data");
                    //        tc_det_SNo2r2.Controls.Add(lit_det_SNo2r2);

                    //        tr_det_sno15.Cells.Add(tc_det_SNo2r2);


                    //        TableCell tc_det_SNo2r3 = new TableCell();
                    //        tc_det_SNo2r3.BorderStyle = BorderStyle.Solid;
                    //        tc_det_SNo2r3.BorderWidth = 1;
                    //        Literal lit_det_SNo2r3 = new Literal();
                    //        lit_det_SNo2r3.Text = dritem["Net_Weight"].ToString();
                    //        tc_det_SNo2r3.Attributes.Add("Class", "tbldetail_Data");
                    //        tc_det_SNo2r3.Controls.Add(lit_det_SNo2r3);

                    //        tr_det_sno15.Cells.Add(tc_det_SNo2r3);

                    //        tbldetail7.Controls.Add(tr_det_sno15);
                    //    }






                    //    tc_det_head_main21.Controls.Add(tbldetail7);



                    //    tr_det_head_main1.Cells.Add(tc_det_head_main21);
                    //    tbldetail_main1.Rows.Add(tr_det_head_main1);


                    //    form1.Controls.Add(tbldetail_main1);
                    //    if (iCount > 0)
                    //    {
                    //        Table tbl_doc_empty = new Table();
                    //        TableRow tr_doc_empty = new TableRow();
                    //        TableCell tc_doc_empty = new TableCell();
                    //        Literal lit_doc_empty = new Literal();
                    //        lit_doc_empty.Text = "<BR>";
                    //        tc_doc_empty.Controls.Add(lit_doc_empty);
                    //        tr_doc_empty.Cells.Add(tc_doc_empty);
                    //        tbl_doc_empty.Rows.Add(tr_doc_empty);
                    //        form1.Controls.Add(tbl_doc_empty);


                    //    }
                    //}
                    //else
                    //{

                    //}

                    //2-Chemists

                    Table tbldetail_main5 = new Table();
                    tbldetail_main5.BorderStyle = BorderStyle.None;
                    tbldetail_main5.Width = 1100;
                    TableRow tr_det_head_main5 = new TableRow();
                    TableCell tc_det_head_main5 = new TableCell();
                    tc_det_head_main5.Width = 100;
                    Literal lit_det_main5 = new Literal();
                    lit_det_main5.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    tc_det_head_main5.Controls.Add(lit_det_main5);
                    tr_det_head_main5.Cells.Add(tc_det_head_main5);

                    TableCell tc_det_head_main6 = new TableCell();
                    tc_det_head_main6.Width = 1000;


                    Table tbldetailChe = new Table();
                    tbldetailChe.BorderStyle = BorderStyle.Solid;
                    tbldetailChe.BorderWidth = 1;
                    tbldetailChe.GridLines = GridLines.Both;
                    tbldetailChe.Width = 1000;
                    tbldetailChe.Style.Add("border-collapse", "collapse");
                    tbldetailChe.Style.Add("border", "solid 1px Black");

                    dsdoc = dc.get_dcr_che_details(sf_code, drdoc["Activity_Date"].ToString(), 2); //2-Chemists
                    iCount = 0;
                    if (dsdoc.Tables[0].Rows.Count > 0)
                    {
                        TableRow tr_det_head = new TableRow();
                        TableCell tc_det_head_SNo = new TableCell();
                        tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_SNo.BorderWidth = 1;
                        Literal lit_det_head_SNo = new Literal();
                        lit_det_head_SNo.Text = "<b>S.No</b>";
                        tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                        tr_det_head.Cells.Add(tc_det_head_SNo);

                        TableCell tc_det_head_doc = new TableCell();
                        tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                        tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_doc.BorderWidth = 1;
                        Literal lit_det_head_doc = new Literal();
                        lit_det_head_doc.Text = "<b>Chemists Name</b>";
                        tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_doc.Controls.Add(lit_det_head_doc);
                        tr_det_head.Cells.Add(tc_det_head_doc);

                        TableCell tc_det_head_Visit_Time = new TableCell();
                        tc_det_head_Visit_Time.BorderStyle = BorderStyle.Solid;
                        tc_det_head_Visit_Time.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_Visit_Time.BorderWidth = 1;
                        Literal lit_det_head_Visit_time = new Literal();
                        lit_det_head_Visit_time.Text = "<b>Visit Time</b>";
                        tc_det_head_Visit_Time.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_Visit_Time.Controls.Add(lit_det_head_Visit_time);
                        tr_det_head.Cells.Add(tc_det_head_Visit_Time);

                        TableCell tc_det_head_Last_Updated = new TableCell();
                        tc_det_head_Last_Updated.BorderStyle = BorderStyle.Solid;
                        tc_det_head_Last_Updated.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_Last_Updated.BorderWidth = 1;
                        Literal lit_det_head_Last_Updated = new Literal();
                        lit_det_head_Last_Updated.Text = "<b>Last Updated</b>";
                        tc_det_head_Last_Updated.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_Last_Updated.Controls.Add(lit_det_head_Last_Updated);
                        tr_det_head.Cells.Add(tc_det_head_Last_Updated);

                        TableCell tc_det_head_ww = new TableCell();
                        tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                        tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_ww.BorderWidth = 1;
                        Literal lit_det_head_ww = new Literal();
                        lit_det_head_ww.Text = "<b>Worked With</b>";
                        tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_ww.Controls.Add(lit_det_head_ww);
                        tr_det_head.Cells.Add(tc_det_head_ww);

                        TableCell tc_det_head_Act_Place_Worked = new TableCell();
                        tc_det_head_Act_Place_Worked.BorderStyle = BorderStyle.Solid;
                        tc_det_head_Act_Place_Worked.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_Act_Place_Worked.BorderWidth = 1;
                        Literal lit_det_head_Act_Place_Worked = new Literal();
                        lit_det_head_Act_Place_Worked.Text = "<b>Actual Place of Worked</b>";
                        tc_det_head_Act_Place_Worked.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_Act_Place_Worked.Controls.Add(lit_det_head_Act_Place_Worked);
                        tr_det_head.Cells.Add(tc_det_head_Act_Place_Worked);

                        //TableCell tc_det_head_CallFeedBack = new TableCell();
                        //tc_det_head_CallFeedBack.BorderStyle = BorderStyle.Solid;
                        //tc_det_head_CallFeedBack.HorizontalAlign = HorizontalAlign.Center;
                        //tc_det_head_CallFeedBack.BorderWidth = 1;
                        //Literal lit_det_head_CallFeedBack = new Literal();
                        //lit_det_head_CallFeedBack.Text = "<b>Call Feedback</b>";
                        //tc_det_head_CallFeedBack.Attributes.Add("Class", "tr_det_head");
                        //tc_det_head_CallFeedBack.Controls.Add(lit_det_head_CallFeedBack);
                        //tr_det_head.Cells.Add(tc_det_head_CallFeedBack);

                        TableCell tc_det_head_catg = new TableCell();
                        tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                        tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_catg.BorderWidth = 1;
                        Literal lit_det_head_catg = new Literal();
                        lit_det_head_catg.Text = "<b>POB</b>";
                        tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_catg.Controls.Add(lit_det_head_catg);
                        tr_det_head.Cells.Add(tc_det_head_catg);


                        tbldetailChe.Rows.Add(tr_det_head);

                        iCount = 0;
                        foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                        {
                            TableRow tr_det_sno = new TableRow();
                            TableCell tc_det_SNo = new TableCell();
                            iCount += 1;
                            Literal lit_det_SNo = new Literal();
                            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                            tc_det_SNo.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_SNo.BorderStyle = BorderStyle.Solid;
                            tc_det_SNo.BorderWidth = 1;
                            tc_det_SNo.Controls.Add(lit_det_SNo);
                            tr_det_sno.Cells.Add(tc_det_SNo);

                            TableCell tc_det_dr_name = new TableCell();
                            Literal lit_det_dr_name = new Literal();
                            lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["Chemists_Name"].ToString();
                            tc_det_dr_name.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_name.BorderWidth = 1;
                            tc_det_dr_name.Controls.Add(lit_det_dr_name);
                            tr_det_sno.Cells.Add(tc_det_dr_name);

                            TableCell tc_det_dr_VisitTime = new TableCell();
                            Literal lit_det_dr_VisitTime = new Literal();
                            lit_det_dr_VisitTime.Text = "&nbsp;&nbsp;" + drdoctor["vstTime"].ToString();
                            tc_det_dr_VisitTime.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_dr_VisitTime.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_VisitTime.BorderWidth = 1;
                            tc_det_dr_VisitTime.Controls.Add(lit_det_dr_VisitTime);
                            tr_det_sno.Cells.Add(tc_det_dr_VisitTime);

                            TableCell tc_det_dr_LastUpdated = new TableCell();
                            Literal lit_det_dr_LastUpdated = new Literal();
                            lit_det_dr_LastUpdated.Text = "&nbsp;&nbsp;" + drdoctor["ModTime"].ToString();
                            tc_det_dr_LastUpdated.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_dr_LastUpdated.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_LastUpdated.BorderWidth = 1;
                            tc_det_dr_LastUpdated.Controls.Add(lit_det_dr_LastUpdated);
                            tr_det_sno.Cells.Add(tc_det_dr_LastUpdated);

                            TableCell tc_det_work = new TableCell();
                            Literal lit_det_work = new Literal();
                            lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                            tc_det_work.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_work.BorderStyle = BorderStyle.Solid;
                            tc_det_work.BorderWidth = 1;
                            tc_det_work.Controls.Add(lit_det_work);
                            tr_det_sno.Cells.Add(tc_det_work);

                            TableCell tc_det_dr_Act_Place_Worked = new TableCell();
                            Literal lit_det_dr_Act_Place_Worked = new Literal();
                            if (drdoctor["GeoAddrs"].ToString().Trim() != "NA")
                            {
                                lit_det_dr_Act_Place_Worked.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                            }
                            else if (drdoctor["GeoAddrs"].ToString().Trim() == "NA")
                            {
                                lit_det_dr_Act_Place_Worked.Text = "";
                            }
                            // lit_det_dr_Act_Place_Worked.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                            tc_det_dr_Act_Place_Worked.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_dr_Act_Place_Worked.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_Act_Place_Worked.BorderWidth = 1;
                            tc_det_dr_Act_Place_Worked.Width = 250;
                            tc_det_dr_Act_Place_Worked.Controls.Add(lit_det_dr_Act_Place_Worked);
                            tr_det_sno.Cells.Add(tc_det_dr_Act_Place_Worked);

                            //TableCell tc_det_dr_CallFeedBack = new TableCell();
                            //Literal lit_det_dr_CallFeedBack = new Literal();
                            //lit_det_dr_CallFeedBack.Text = "&nbsp;&nbsp;" + drdoctor["Rx"].ToString();
                            //tc_det_dr_CallFeedBack.Attributes.Add("Class", "tbldetail_Data");
                            //tc_det_dr_CallFeedBack.BorderStyle = BorderStyle.Solid;
                            //tc_det_dr_CallFeedBack.BorderWidth = 1;
                            //tc_det_dr_CallFeedBack.Controls.Add(lit_det_dr_CallFeedBack);
                            //tr_det_sno.Cells.Add(tc_det_dr_CallFeedBack);

                            TableCell tc_det_spec = new TableCell();
                            Literal lit_det_spec = new Literal();
                            lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                            tc_det_spec.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_spec.BorderStyle = BorderStyle.Solid;
                            tc_det_spec.BorderWidth = 1;
                            tc_det_spec.Controls.Add(lit_det_spec);
                            tr_det_sno.Cells.Add(tc_det_spec);

                            tbldetailChe.Rows.Add(tr_det_sno);
                        }
                    }

                    //form1.Controls.Add(tbldetailChe);

                    tc_det_head_main6.Controls.Add(tbldetailChe);
                    tr_det_head_main5.Cells.Add(tc_det_head_main6);
                    tbldetail_main5.Rows.Add(tr_det_head_main5);

                    form1.Controls.Add(tbldetail_main5);


                    if (iCount > 0)
                    {
                        Table tbl_chem_empty = new Table();
                        TableRow tr_chem_empty = new TableRow();
                        TableCell tc_chem_empty = new TableCell();
                        Literal lit_chem_empty = new Literal();
                        lit_chem_empty.Text = "<BR>";
                        tc_chem_empty.Controls.Add(lit_chem_empty);
                        tr_chem_empty.Cells.Add(tc_chem_empty);
                        tbl_chem_empty.Rows.Add(tr_chem_empty);
                        form1.Controls.Add(tbl_chem_empty);
                    }

                    //4-UnListed Doctor

                    Table tbldetail_main7 = new Table();
                    tbldetail_main7.BorderStyle = BorderStyle.None;
                    tbldetail_main7.Width = 1100;
                    TableRow tr_det_head_main7 = new TableRow();
                    TableCell tc_det_head_main7 = new TableCell();
                    tc_det_head_main7.Width = 100;
                    Literal lit_det_main7 = new Literal();
                    lit_det_main7.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    tc_det_head_main7.Controls.Add(lit_det_main7);
                    tr_det_head_main7.Cells.Add(tc_det_head_main7);

                    TableCell tc_det_head_main8 = new TableCell();
                    tc_det_head_main8.Width = 1000;

                    Table tblUnLstDoc = new Table();
                    tblUnLstDoc.BorderStyle = BorderStyle.Solid;
                    tblUnLstDoc.BorderWidth = 1;
                    tblUnLstDoc.GridLines = GridLines.Both;
                    tblUnLstDoc.Width = 1000;
                    tblUnLstDoc.Style.Add("border-collapse", "collapse");
                    tblUnLstDoc.Style.Add("border", "solid 1px Black");

                    dsdoc = dc.get_unlst_doc_details(sf_code, drdoc["Activity_Date"].ToString(), 1); //1-Listed Doctor
                    iCount = 0;
                    if (dsdoc.Tables[0].Rows.Count > 0)
                    {
                        TableRow tr_UnLst_doc_head = new TableRow();
                        TableCell tc_UnLst_doc_head_SNo = new TableCell();
                        tc_UnLst_doc_head_SNo.BorderStyle = BorderStyle.Solid;
                        tc_UnLst_doc_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                        tc_UnLst_doc_head_SNo.BorderWidth = 1;
                        Literal lit_undet_head_SNo = new Literal();
                        lit_undet_head_SNo.Text = "<b>S.No</b>";
                        tc_UnLst_doc_head_SNo.Attributes.Add("Class", "tr_det_head");
                        tc_UnLst_doc_head_SNo.Controls.Add(lit_undet_head_SNo);
                        tr_UnLst_doc_head.Cells.Add(tc_UnLst_doc_head_SNo);

                        TableCell tc_undet_head_Ses = new TableCell();
                        tc_undet_head_Ses.BorderStyle = BorderStyle.Solid;
                        tc_undet_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                        tc_undet_head_Ses.BorderWidth = 1;
                        Literal lit_undet_head_Ses = new Literal();
                        lit_undet_head_Ses.Text = "<b>Ses</b>";
                        tc_undet_head_Ses.Attributes.Add("Class", "tr_det_head");
                        tc_undet_head_Ses.Controls.Add(lit_undet_head_Ses);
                        tr_UnLst_doc_head.Cells.Add(tc_undet_head_Ses);

                        TableCell tc_det_head_doc = new TableCell();
                        tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                        tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_doc.BorderWidth = 1;
                        Literal lit_det_head_doc = new Literal();
                        lit_det_head_doc.Text = "<b>UnListed  Doctor Name</b>";
                        tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_doc.Controls.Add(lit_det_head_doc);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_doc);

                        TableCell tc_det_head_time = new TableCell();
                        tc_det_head_time.BorderStyle = BorderStyle.Solid;
                        tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_time.BorderWidth = 1;
                        Literal lit_det_head_time = new Literal();
                        lit_det_head_time.Text = "<b>Time</b>";
                        tc_det_head_time.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_time.Controls.Add(lit_det_head_time);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_time);

                        TableCell tc_det_head_LastUpdated = new TableCell();
                        tc_det_head_LastUpdated.BorderStyle = BorderStyle.Solid;
                        tc_det_head_LastUpdated.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_LastUpdated.BorderWidth = 1;
                        Literal lit_det_head_LastUpdated = new Literal();
                        lit_det_head_LastUpdated.Text = "<b>Last Updated</b>";
                        tc_det_head_LastUpdated.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_LastUpdated.Controls.Add(lit_det_head_LastUpdated);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_LastUpdated);

                        TableCell tc_det_head_ww = new TableCell();
                        tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                        tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_ww.BorderWidth = 1;
                        Literal lit_det_head_ww = new Literal();
                        lit_det_head_ww.Text = "<b>Worked With</b>";
                        tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_ww.Controls.Add(lit_det_head_ww);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_ww);

                        //TableCell tc_det_head_visit = new TableCell();
                        //tc_det_head_visit.BorderStyle = BorderStyle.Solid;
                        //tc_det_head_visit.HorizontalAlign = HorizontalAlign.Center;
                        //tc_det_head_visit.BorderWidth = 1;
                        //Literal lit_det_head_visit = new Literal();
                        //lit_det_head_visit.Text = "<b>Latest Visit</b>";
                        //tc_det_head_visit.Attributes.Add("Class", "tr_det_head");
                        //tc_det_head_visit.Controls.Add(lit_det_head_visit);
                        //tr_UnLst_doc_head.Cells.Add(tc_det_head_visit);

                        TableCell tc_det_head_catg = new TableCell();
                        tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                        tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_catg.BorderWidth = 1;
                        Literal lit_det_head_catg = new Literal();
                        lit_det_head_catg.Text = "<b>Category</b>";
                        tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_catg.Controls.Add(lit_det_head_catg);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_catg);

                        TableCell tc_det_head_spec = new TableCell();
                        tc_det_head_spec.BorderStyle = BorderStyle.Solid;
                        tc_det_head_spec.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_spec.BorderWidth = 1;
                        Literal lit_det_head_spec = new Literal();
                        lit_det_head_spec.Text = "<b>Speciality</b>";
                        tc_det_head_spec.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_spec.Controls.Add(lit_det_head_spec);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_spec);

                        TableCell tc_det_head_SDP_Plan = new TableCell();
                        tc_det_head_SDP_Plan.BorderStyle = BorderStyle.Solid;
                        tc_det_head_SDP_Plan.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_SDP_Plan.BorderWidth = 1;
                        Literal lit_det_head_SDP_Plan = new Literal();
                        lit_det_head_SDP_Plan.Text = "<b>" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "</b>";
                        tc_det_head_SDP_Plan.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_SDP_Plan.Controls.Add(lit_det_head_SDP_Plan);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_SDP_Plan);

                        TableCell tc_det_dr_Act_Place_Worked = new TableCell();
                        Literal lit_det_dr_Act_Place_Worked = new Literal();
                        lit_det_dr_Act_Place_Worked.Text = "<b>Actual_Place_of_Worked</b>";
                        tc_det_dr_Act_Place_Worked.Attributes.Add("Class", "tr_det_head");
                        tc_det_dr_Act_Place_Worked.BorderStyle = BorderStyle.Solid;
                        tc_det_dr_Act_Place_Worked.BorderWidth = 1;
                        tc_det_dr_Act_Place_Worked.Controls.Add(lit_det_dr_Act_Place_Worked);
                        tr_UnLst_doc_head.Cells.Add(tc_det_dr_Act_Place_Worked);

                        //TableCell tc_det_dr_CallFeedBack = new TableCell();
                        //Literal lit_det_dr_CallFeedBack = new Literal();
                        //lit_det_dr_CallFeedBack.Text = "<b>Call_Feedback</b>";
                        //tc_det_dr_CallFeedBack.Attributes.Add("Class", "tr_det_head");
                        //tc_det_dr_CallFeedBack.BorderStyle = BorderStyle.Solid;
                        //tc_det_dr_CallFeedBack.BorderWidth = 1;
                        //tc_det_dr_CallFeedBack.Controls.Add(lit_det_dr_CallFeedBack);
                        //tr_UnLst_doc_head.Cells.Add(tc_det_dr_CallFeedBack);

                        TableCell tc_det_head_prod = new TableCell();
                        tc_det_head_prod.BorderStyle = BorderStyle.Solid;
                        tc_det_head_prod.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_prod.BorderWidth = 1;
                        Literal lit_det_head_prod = new Literal();
                        lit_det_head_prod.Text = "<b>Product Sampled</b>";
                        tc_det_head_prod.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_prod.Controls.Add(lit_det_head_prod);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_prod);

                        //TableCell tc_det_head_gift = new TableCell();
                        //tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                        //tc_det_head_gift.HorizontalAlign = HorizontalAlign.Center;
                        //tc_det_head_gift.BorderWidth = 1;
                        //Literal lit_det_head_gift = new Literal();
                        //lit_det_head_gift.Text = "<b>Gift</b>";
                        //tc_det_head_gift.Attributes.Add("Class", "tr_det_head");
                        //tc_det_head_gift.Controls.Add(lit_det_head_gift);
                        //tr_UnLst_doc_head.Cells.Add(tc_det_head_gift);

                        tblUnLstDoc.Rows.Add(tr_UnLst_doc_head);

                        iCount = 0;
                        foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                        {
                            TableRow tr_det_sno = new TableRow();
                            TableCell tc_det_SNo = new TableCell();
                            iCount += 1;
                            Literal lit_det_SNo = new Literal();
                            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                            tc_det_SNo.BorderStyle = BorderStyle.Solid;
                            tc_det_SNo.BorderWidth = 1;
                            tc_det_SNo.Controls.Add(lit_det_SNo);
                            tr_det_sno.Cells.Add(tc_det_SNo);

                            TableCell tc_det_Ses = new TableCell();
                            Literal lit_det_Ses = new Literal();
                            lit_det_Ses.Text = "&nbsp;&nbsp;" + drdoctor["Session"].ToString();
                            tc_det_Ses.BorderStyle = BorderStyle.Solid;
                            tc_det_Ses.BorderWidth = 1;
                            tc_det_Ses.Controls.Add(lit_det_Ses);
                            tr_det_sno.Cells.Add(tc_det_Ses);

                            TableCell tc_det_dr_name = new TableCell();
                            Literal lit_det_dr_name = new Literal();
                            lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["UnListedDr_Name"].ToString();
                            tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_name.BorderWidth = 1;
                            tc_det_dr_name.Controls.Add(lit_det_dr_name);
                            tr_det_sno.Cells.Add(tc_det_dr_name);

                            TableCell tc_det_time = new TableCell();
                            Literal lit_det_time = new Literal();
                            lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor["Time"].ToString();
                            tc_det_time.BorderStyle = BorderStyle.Solid;
                            tc_det_time.BorderWidth = 1;
                            tc_det_time.Controls.Add(lit_det_time);
                            tr_det_sno.Cells.Add(tc_det_time);

                            TableCell tc_det_LastUpdate = new TableCell();
                            Literal lit_det_LastUpdate = new Literal();
                            lit_det_LastUpdate.Text = "&nbsp;&nbsp;" + drdoctor["ModTime"].ToString();
                            tc_det_LastUpdate.BorderStyle = BorderStyle.Solid;
                            tc_det_LastUpdate.BorderWidth = 1;
                            tc_det_LastUpdate.Controls.Add(lit_det_LastUpdate);
                            tr_det_sno.Cells.Add(tc_det_LastUpdate);

                            TableCell tc_det_work = new TableCell();
                            Literal lit_det_work = new Literal();
                            lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                            tc_det_work.BorderStyle = BorderStyle.Solid;
                            tc_det_work.BorderWidth = 1;
                            tc_det_work.Controls.Add(lit_det_work);
                            tr_det_sno.Cells.Add(tc_det_work);

                            //TableCell tc_det_lvisit = new TableCell();
                            //Literal lit_det_lvisit = new Literal();
                            //lit_det_lvisit.Text = ""; // drdoctor["lvisit"].ToString();
                            //tc_det_lvisit.BorderStyle = BorderStyle.Solid;
                            //tc_det_lvisit.BorderWidth = 1;
                            //tc_det_lvisit.Controls.Add(lit_det_lvisit);
                            //tr_det_sno.Cells.Add(tc_det_lvisit);

                            TableCell tc_det_catg = new TableCell();
                            Literal lit_det_catg = new Literal();
                            lit_det_catg.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Cat_Name"].ToString();
                            tc_det_catg.BorderStyle = BorderStyle.Solid;
                            tc_det_catg.BorderWidth = 1;
                            tc_det_catg.Controls.Add(lit_det_catg);
                            tr_det_sno.Cells.Add(tc_det_catg);

                            TableCell tc_det_spec = new TableCell();
                            Literal lit_det_spec = new Literal();
                            lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Special_Name"].ToString();
                            tc_det_spec.BorderStyle = BorderStyle.Solid;
                            tc_det_spec.BorderWidth = 1;
                            tc_det_spec.Controls.Add(lit_det_spec);
                            tr_det_sno.Cells.Add(tc_det_spec);

                            TableCell tc_det_SDP_Plan = new TableCell();
                            Literal lit_det_SDP_Plan = new Literal();
                            lit_det_SDP_Plan.Text = "&nbsp;&nbsp;" + drdoctor["SDP_Name"].ToString();
                            tc_det_SDP_Plan.BorderStyle = BorderStyle.Solid;
                            tc_det_SDP_Plan.BorderWidth = 1;
                            tc_det_SDP_Plan.Controls.Add(lit_det_SDP_Plan);
                            tr_det_sno.Cells.Add(tc_det_SDP_Plan);

                            TableCell tc_det_Act_Place_Worked = new TableCell();
                            Literal lit_det_Act_Place_Worked = new Literal();
                            if (drdoctor["GeoAddrs"].ToString().Trim() != "NA")
                            {
                                lit_det_Act_Place_Worked.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                            }
                            else if (drdoctor["GeoAddrs"].ToString().Trim() == "NA")
                            {
                                lit_det_Act_Place_Worked.Text = "";
                            }
                            //lit_det_Act_Place_Worked.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                            tc_det_Act_Place_Worked.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_Act_Place_Worked.BorderStyle = BorderStyle.Solid;
                            tc_det_Act_Place_Worked.BorderWidth = 1;
                            tc_det_Act_Place_Worked.Width = 250;
                            tc_det_Act_Place_Worked.Controls.Add(lit_det_Act_Place_Worked);
                            tr_det_sno.Cells.Add(tc_det_Act_Place_Worked);

                            //TableCell tc_det_CallFeedBack = new TableCell();
                            //Literal lit_det_CallFeedBack = new Literal();
                            //lit_det_CallFeedBack.Text = "&nbsp;&nbsp;" + drdoctor["Rx"].ToString();
                            //tc_det_CallFeedBack.Attributes.Add("Class", "tbldetail_Data");
                            //tc_det_CallFeedBack.BorderStyle = BorderStyle.Solid;
                            //tc_det_CallFeedBack.BorderWidth = 1;
                            //tc_det_CallFeedBack.Controls.Add(lit_det_CallFeedBack);
                            //tr_det_sno.Cells.Add(tc_det_CallFeedBack);

                            TableCell tc_det_prod = new TableCell();
                            Literal lit_det_prod = new Literal();
                            lit_det_prod.Text = drdoctor["Product_Detail"].ToString().Replace("~", "(").Trim();
                            lit_det_prod.Text = lit_det_prod.Text.Replace("$", ")").Trim();
                            lit_det_prod.Text = "&nbsp;&nbsp;" + lit_det_prod.Text.Replace("#", "  ").Trim();
                            tc_det_prod.BorderStyle = BorderStyle.Solid;
                            tc_det_prod.BorderWidth = 1;
                            tc_det_prod.Controls.Add(lit_det_prod);
                            tr_det_sno.Cells.Add(tc_det_prod);

                            //TableCell tc_det_gift = new TableCell();
                            //Literal lit_det_gift = new Literal();
                            //lit_det_gift.Text = "&nbsp;&nbsp;" + drdoctor["Gift_Name"].ToString().Replace("~", " ").Trim();
                            //tc_det_gift.BorderStyle = BorderStyle.Solid;
                            //tc_det_gift.BorderWidth = 1;
                            //tc_det_gift.Controls.Add(lit_det_gift);
                            //tr_det_sno.Cells.Add(tc_det_gift);

                            tblUnLstDoc.Rows.Add(tr_det_sno);
                        }
                    }

                    //form1.Controls.Add(tblUnLstDoc);

                    tc_det_head_main8.Controls.Add(tblUnLstDoc);
                    tr_det_head_main7.Cells.Add(tc_det_head_main8);
                    tbldetail_main7.Rows.Add(tr_det_head_main7);

                    form1.Controls.Add(tbldetail_main7);


                    if (iCount > 0)
                    {
                        Table tbl_undoc_empty = new Table();
                        TableRow tr_undoc_empty = new TableRow();
                        TableCell tc_undoc_empty = new TableCell();
                        Literal lit_undoc_empty = new Literal();
                        lit_undoc_empty.Text = "<BR>";
                        tc_undoc_empty.Controls.Add(lit_undoc_empty);
                        tr_undoc_empty.Cells.Add(tc_undoc_empty);
                        tbl_undoc_empty.Rows.Add(tr_undoc_empty);
                        form1.Controls.Add(tbl_undoc_empty);
                    }

                    // 3- Stockist

                    //5-Hospitals

                    Table tbldetail_main11 = new Table();
                    tbldetail_main11.BorderStyle = BorderStyle.None;
                    tbldetail_main11.Width = 1100;
                    TableRow tr_det_head_main11 = new TableRow();
                    TableCell tc_det_head_main11 = new TableCell();
                    tr_det_head_main11.Width = 100;
                    Literal lit_det_main11 = new Literal();
                    lit_det_main11.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    tc_det_head_main11.Controls.Add(lit_det_main11);
                    tr_det_head_main11.Cells.Add(tc_det_head_main11);

                    TableCell tc_det_head_main12 = new TableCell();
                    tc_det_head_main12.Width = 1000;


                    Table tbldetailstk = new Table();
                    tbldetailstk.BorderStyle = BorderStyle.Solid;
                    tbldetailstk.BorderWidth = 1;
                    tbldetailstk.GridLines = GridLines.Both;
                    tbldetailstk.Width = 1000;
                    tbldetailstk.Style.Add("border-collapse", "collapse");
                    tbldetailstk.Style.Add("border", "solid 1px Black");

                    dsdoc = dc.get_dcr_stk_details(sf_code, drdoc["Activity_Date"].ToString(), 3); //3-Stockist
                    iCount = 0;
                    if (dsdoc.Tables[0].Rows.Count > 0)
                    {
                        TableRow tr_det_head = new TableRow();
                        TableCell tc_det_head_SNo = new TableCell();
                        tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_SNo.BorderWidth = 1;
                        Literal lit_det_head_SNo = new Literal();
                        lit_det_head_SNo.Text = "<b>S.No</b>";
                        tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                        tr_det_head.Cells.Add(tc_det_head_SNo);

                        TableCell tc_det_head_doc = new TableCell();
                        tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                        tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_doc.BorderWidth = 1;
                        Literal lit_det_head_doc = new Literal();
                        lit_det_head_doc.Text = "<b>Stockist Name</b>";
                        tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_doc.Controls.Add(lit_det_head_doc);
                        tr_det_head.Cells.Add(tc_det_head_doc);

                        TableCell tc_det_head_VistTime = new TableCell();
                        tc_det_head_VistTime.BorderStyle = BorderStyle.Solid;
                        tc_det_head_VistTime.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_VistTime.BorderWidth = 1;
                        Literal lit_det_head_VistTime = new Literal();
                        lit_det_head_VistTime.Text = "<b>Visit Time</b>";
                        tc_det_head_VistTime.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_VistTime.Controls.Add(lit_det_head_VistTime);
                        tr_det_head.Cells.Add(tc_det_head_VistTime);

                        TableCell tc_det_head_LastUpdate = new TableCell();
                        tc_det_head_LastUpdate.BorderStyle = BorderStyle.Solid;
                        tc_det_head_LastUpdate.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_LastUpdate.BorderWidth = 1;
                        Literal lit_det_head_LastUpdate = new Literal();
                        lit_det_head_LastUpdate.Text = "<b>Last Updated</b>";
                        tc_det_head_LastUpdate.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_LastUpdate.Controls.Add(lit_det_head_LastUpdate);
                        tr_det_head.Cells.Add(tc_det_head_LastUpdate);

                        TableCell tc_det_head_ww = new TableCell();
                        tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                        tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_ww.BorderWidth = 1;
                        Literal lit_det_head_ww = new Literal();
                        lit_det_head_ww.Text = "<b>Worked With</b>";
                        tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_ww.Controls.Add(lit_det_head_ww);
                        tr_det_head.Cells.Add(tc_det_head_ww);

                        TableCell tc_det_head_ActualPlace = new TableCell();
                        tc_det_head_ActualPlace.BorderStyle = BorderStyle.Solid;
                        tc_det_head_ActualPlace.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_ActualPlace.BorderWidth = 1;
                        Literal lit_det_head_ActualPlace = new Literal();
                        lit_det_head_ActualPlace.Text = "<b>Actual Place</b>";
                        tc_det_head_ActualPlace.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_ActualPlace.Controls.Add(lit_det_head_ActualPlace);
                        tr_det_head.Cells.Add(tc_det_head_ActualPlace);

                        //TableCell tc_det_head_CallFeedBack = new TableCell();
                        //tc_det_head_CallFeedBack.BorderStyle = BorderStyle.Solid;
                        //tc_det_head_CallFeedBack.HorizontalAlign = HorizontalAlign.Center;
                        //tc_det_head_CallFeedBack.BorderWidth = 1;
                        //Literal lit_det_head_CallFeedBack = new Literal();
                        //lit_det_head_CallFeedBack.Text = "<b>Call Feedback</b>";
                        //tc_det_head_CallFeedBack.Attributes.Add("Class", "tr_det_head");
                        //tc_det_head_CallFeedBack.Controls.Add(lit_det_head_CallFeedBack);
                        //tr_det_head.Cells.Add(tc_det_head_CallFeedBack);

                        TableCell tc_det_head_catg = new TableCell();
                        tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                        tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_catg.BorderWidth = 1;
                        Literal lit_det_head_catg = new Literal();
                        lit_det_head_catg.Text = "<b>POB</b>";
                        tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_catg.Controls.Add(lit_det_head_catg);
                        tr_det_head.Cells.Add(tc_det_head_catg);


                        tbldetailstk.Rows.Add(tr_det_head);

                        iCount = 0;
                        foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                        {
                            TableRow tr_det_sno = new TableRow();
                            TableCell tc_det_SNo = new TableCell();
                            iCount += 1;
                            Literal lit_det_SNo = new Literal();
                            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                            tc_det_SNo.BorderStyle = BorderStyle.Solid;
                            tc_det_SNo.BorderWidth = 1;
                            tc_det_SNo.Controls.Add(lit_det_SNo);
                            tr_det_sno.Cells.Add(tc_det_SNo);


                            TableCell tc_det_dr_name = new TableCell();
                            Literal lit_det_dr_name = new Literal();
                            lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["Stockist_Name"].ToString();
                            tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_name.BorderWidth = 1;
                            tc_det_dr_name.Controls.Add(lit_det_dr_name);
                            tr_det_sno.Cells.Add(tc_det_dr_name);


                            TableCell tc_det_work = new TableCell();
                            Literal lit_det_work = new Literal();
                            lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                            tc_det_work.BorderStyle = BorderStyle.Solid;
                            tc_det_work.BorderWidth = 1;
                            tc_det_work.Controls.Add(lit_det_work);
                            tr_det_sno.Cells.Add(tc_det_work);

                            TableCell tc_det_dr_VisitTime = new TableCell();
                            Literal lit_det_dr_VisitTime = new Literal();
                            lit_det_dr_VisitTime.Text = "&nbsp;&nbsp;" + drdoctor["vstTime"].ToString();
                            tc_det_dr_VisitTime.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_VisitTime.BorderWidth = 1;
                            tc_det_dr_VisitTime.Controls.Add(lit_det_dr_VisitTime);
                            tr_det_sno.Cells.Add(tc_det_dr_VisitTime);

                            TableCell tc_det_dr_LastUpdate = new TableCell();
                            Literal lit_det_dr_LastUpdate = new Literal();
                            lit_det_dr_LastUpdate.Text = "&nbsp;&nbsp;" + drdoctor["ModTime"].ToString();
                            tc_det_dr_LastUpdate.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_LastUpdate.BorderWidth = 1;
                            tc_det_dr_LastUpdate.Controls.Add(lit_det_dr_LastUpdate);
                            tr_det_sno.Cells.Add(tc_det_dr_LastUpdate);

                            TableCell tc_det_dr_Place_Worked = new TableCell();
                            Literal lit_det_dr_Place_Worked = new Literal();
                            if (drdoctor["GeoAddrs"].ToString().Trim() != "NA")
                            {
                                lit_det_dr_Place_Worked.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                            }
                            else if (drdoctor["GeoAddrs"].ToString().Trim() == "NA")
                            {
                                lit_det_dr_Place_Worked.Text = "";
                            }
                            //lit_det_dr_Place_Worked.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                            tc_det_dr_Place_Worked.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_Place_Worked.BorderWidth = 1;
                            tc_det_dr_Place_Worked.Width = 250;
                            tc_det_dr_Place_Worked.Controls.Add(lit_det_dr_Place_Worked);
                            tr_det_sno.Cells.Add(tc_det_dr_Place_Worked);

                            //TableCell tc_det_dr_Call_Feedback = new TableCell();
                            //Literal lit_det_dr_Call_Feedback = new Literal();
                            //lit_det_dr_Call_Feedback.Text = "&nbsp;&nbsp;" + drdoctor["Rx"].ToString();
                            //tc_det_dr_Call_Feedback.BorderStyle = BorderStyle.Solid;
                            //tc_det_dr_Call_Feedback.BorderWidth = 1;
                            //tc_det_dr_Call_Feedback.Controls.Add(lit_det_dr_Call_Feedback);
                            //tr_det_sno.Cells.Add(tc_det_dr_Call_Feedback);


                            TableCell tc_det_spec = new TableCell();
                            Literal lit_det_spec = new Literal();
                            lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                            tc_det_spec.BorderStyle = BorderStyle.Solid;
                            tc_det_spec.BorderWidth = 1;
                            tc_det_spec.Controls.Add(lit_det_spec);
                            tr_det_sno.Cells.Add(tc_det_spec);

                            tbldetailstk.Rows.Add(tr_det_sno);
                        }
                    }

                    //form1.Controls.Add(tbldetailhos);

                    tc_det_head_main12.Controls.Add(tbldetailstk);
                    tr_det_head_main11.Cells.Add(tc_det_head_main12);
                    tbldetail_main11.Rows.Add(tr_det_head_main11);

                    form1.Controls.Add(tbldetail_main11);


                    if (iCount > 0)
                    {
                        Table tbl_stk_empty = new Table();
                        TableRow tr_stk_empty = new TableRow();
                        TableCell tc_stk_empty = new TableCell();
                        Literal lit_stk_empty = new Literal();
                        lit_stk_empty.Text = "<BR>";
                        tc_stk_empty.Controls.Add(lit_stk_empty);
                        tr_stk_empty.Cells.Add(tc_stk_empty);
                        tbl_stk_empty.Rows.Add(tr_stk_empty);
                        form1.Controls.Add(tbl_stk_empty);
                    }

                    //5-Hospitals

                    Table tbldetail_main9 = new Table();
                    tbldetail_main9.BorderStyle = BorderStyle.None;
                    tbldetail_main9.Width = 1100;
                    TableRow tr_det_head_main9 = new TableRow();
                    TableCell tc_det_head_main9 = new TableCell();
                    tc_det_head_main9.Width = 100;
                    Literal lit_det_main9 = new Literal();
                    lit_det_main9.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    tc_det_head_main9.Controls.Add(lit_det_main9);
                    tr_det_head_main9.Cells.Add(tc_det_head_main9);

                    TableCell tc_det_head_main10 = new TableCell();
                    tc_det_head_main10.Width = 1000;


                    Table tbldetailhos = new Table();
                    tbldetailhos.BorderStyle = BorderStyle.Solid;
                    tbldetailhos.BorderWidth = 1;
                    tbldetailhos.GridLines = GridLines.Both;
                    tbldetailhos.Width = 1000;
                    tbldetailhos.Style.Add("border-collapse", "collapse");
                    tbldetailhos.Style.Add("border", "solid 1px Black");

                    dsdoc = dc.get_dcr_hos_details(sf_code, drdoc["Activity_Date"].ToString(), 5); //5-Hospital
                    iCount = 0;
                    if (dsdoc.Tables[0].Rows.Count > 0)
                    {
                        TableRow tr_det_head = new TableRow();
                        TableCell tc_det_head_SNo = new TableCell();
                        tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_SNo.BorderWidth = 1;
                        Literal lit_det_head_SNo = new Literal();
                        lit_det_head_SNo.Text = "<b>S.No</b>";
                        tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                        tr_det_head.Cells.Add(tc_det_head_SNo);

                        TableCell tc_det_head_doc = new TableCell();
                        tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                        tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_doc.BorderWidth = 1;
                        Literal lit_det_head_doc = new Literal();
                        lit_det_head_doc.Text = "<b>Hospital Name</b>";
                        tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_doc.Controls.Add(lit_det_head_doc);
                        tr_det_head.Cells.Add(tc_det_head_doc);

                        TableCell tc_det_head_ww = new TableCell();
                        tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                        tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_ww.BorderWidth = 1;
                        Literal lit_det_head_ww = new Literal();
                        lit_det_head_ww.Text = "<b>Worked With</b>";
                        tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_ww.Controls.Add(lit_det_head_ww);
                        tr_det_head.Cells.Add(tc_det_head_ww);

                        TableCell tc_det_head_catg = new TableCell();
                        tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                        tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_catg.BorderWidth = 1;
                        Literal lit_det_head_catg = new Literal();
                        lit_det_head_catg.Text = "<b>POB</b>";
                        tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_catg.Controls.Add(lit_det_head_catg);
                        tr_det_head.Cells.Add(tc_det_head_catg);


                        tbldetailhos.Rows.Add(tr_det_head);

                        iCount = 0;
                        foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                        {
                            TableRow tr_det_sno = new TableRow();
                            TableCell tc_det_SNo = new TableCell();
                            iCount += 1;
                            Literal lit_det_SNo = new Literal();
                            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                            tc_det_SNo.BorderStyle = BorderStyle.Solid;
                            tc_det_SNo.BorderWidth = 1;
                            tc_det_SNo.Controls.Add(lit_det_SNo);
                            tr_det_sno.Cells.Add(tc_det_SNo);


                            TableCell tc_det_dr_name = new TableCell();
                            Literal lit_det_dr_name = new Literal();
                            lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["Hospital_Name"].ToString();
                            tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_name.BorderWidth = 1;
                            tc_det_dr_name.Controls.Add(lit_det_dr_name);
                            tr_det_sno.Cells.Add(tc_det_dr_name);


                            TableCell tc_det_work = new TableCell();
                            Literal lit_det_work = new Literal();
                            lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                            tc_det_work.BorderStyle = BorderStyle.Solid;
                            tc_det_work.BorderWidth = 1;
                            tc_det_work.Controls.Add(lit_det_work);
                            tr_det_sno.Cells.Add(tc_det_work);


                            TableCell tc_det_spec = new TableCell();
                            Literal lit_det_spec = new Literal();
                            lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                            tc_det_spec.BorderStyle = BorderStyle.Solid;
                            tc_det_spec.BorderWidth = 1;
                            tc_det_spec.Controls.Add(lit_det_spec);
                            tr_det_sno.Cells.Add(tc_det_spec);

                            tbldetailhos.Rows.Add(tr_det_sno);
                        }
                    }

                    //form1.Controls.Add(tbldetailhos);

                    tc_det_head_main10.Controls.Add(tbldetailhos);
                    tr_det_head_main9.Cells.Add(tc_det_head_main10);
                    tbldetail_main9.Rows.Add(tr_det_head_main9);

                    form1.Controls.Add(tbldetail_main9);






                    if (iCount > 0)
                    {
                        Table tbl_hosp_empty = new Table();
                        TableRow tr_hosp_empty = new TableRow();
                        TableCell tc_hosp_empty = new TableCell();
                        Literal lit_hosp_empty = new Literal();
                        lit_hosp_empty.Text = "<BR>";
                        tc_hosp_empty.Controls.Add(lit_hosp_empty);
                        tr_hosp_empty.Cells.Add(tc_hosp_empty);
                        tbl_hosp_empty.Rows.Add(tr_hosp_empty);
                        form1.Controls.Add(tbl_hosp_empty);
                    }

                    Table tbl_line = new Table();
                    tbl_line.BorderStyle = BorderStyle.None;
                    tbl_line.Style.Add("width", "180%");
                    tbl_line.Style.Add("border-collapse", "collapse");
                    tbl_line.Style.Add("border-top", "none");
                    tbl_line.Style.Add("border-right", "none");
                    //tbl_line.Style.Add("margin-left", "100px");
                    tbl_line.Style.Add("border-bottom ", "solid 1px Black");

                    TableRow tr_line = new TableRow();

                    TableCell tc_line0 = new TableCell();
                    tc_line0.Width = 100;
                    Literal lit_line0 = new Literal();
                    lit_line0.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    tc_line0.Controls.Add(lit_line0);
                    tr_line.Cells.Add(tc_line0);

                    TableCell tc_line = new TableCell();
                    tc_line.Width = 1000;
                    Literal lit_line = new Literal();
                    // lit_line.Text = "<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>";
                    tc_line.Controls.Add(lit_line);
                    tr_line.Cells.Add(tc_line);
                    tbl_line.Rows.Add(tr_line);
                    form1.Controls.Add(tbl_line);

                }
            }

        }
        else
        {
            //lblHead.Visible = true;
            //lblHead.Style.Add("margin-top", "80px");
            //lblHead.Text = "No Record Found";

            pnlbutton.Visible = true;

            Table tbldetail_mainHoliday = new Table();
            tbldetail_mainHoliday.BorderStyle = BorderStyle.None;
            tbldetail_mainHoliday.Width = 1100;
            TableRow tr_det_head_mainHoliday = new TableRow();
            TableCell tc_det_head_mainHolday = new TableCell();
            tc_det_head_mainHolday.Width = 100;
            Literal lit_det_mainHoliday = new Literal();
            lit_det_mainHoliday.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            tbldetail_mainHoliday.Style.Add("margin-top", "110px");
            tc_det_head_mainHolday.Controls.Add(lit_det_mainHoliday);
            tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHolday);

            TableCell tc_det_head_mainHoliday = new TableCell();
            tc_det_head_mainHoliday.Width = 800;

            Table tbldetailHoliday = new Table();
            tbldetailHoliday.BorderStyle = BorderStyle.Solid;
            tbldetailHoliday.BorderWidth = 1;
            tbldetailHoliday.GridLines = GridLines.Both;
            tbldetailHoliday.Width = 1000;
            tbldetailHoliday.Style.Add("border-collapse", "collapse");
            tbldetailHoliday.Style.Add("border", "solid 1px Black");

            TableRow tr_det_sno = new TableRow();
            TableCell tc_det_SNo = new TableCell();
            iCount += 1;
            Literal lit_det_SNo = new Literal();
            lit_det_SNo.Text = "No Record Found";
            tc_det_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_SNo.Attributes.Add("Class", "NoRecord");

            tc_det_SNo.HorizontalAlign = HorizontalAlign.Center;
            tc_det_SNo.BorderWidth = 1;
            tc_det_SNo.BorderStyle = BorderStyle.None;
            tc_det_SNo.Controls.Add(lit_det_SNo);
            tr_det_sno.Cells.Add(tc_det_SNo);

            tbldetailHoliday.Rows.Add(tr_det_sno);

            tc_det_head_mainHoliday.Controls.Add(tbldetailHoliday);
            tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHoliday);
            tbldetail_mainHoliday.Rows.Add(tr_det_head_mainHoliday);

            form1.Controls.Add(tbldetail_mainHoliday);
        }


    }

    private void FillSalesForce(string div_code, string sf_code, int cmonth, int cyear)
    {
        // Fetch the total rows for the table
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_getMR(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
            ViewState["dsSalesForce"] = dsSalesForce;

        DCR dc = new DCR();
        int iret = dc.isDCR(div_code, cmonth, cyear);
        if (iret > 0)
            CreateDynamicTableDCRDate(Fdate, Tdate, sf_code);
        //FillWorkType();
    }

    private void CreateDynamicTableDCRDate(string Fdate, string Tdate, string sf_code)
    {
        DCR dc = new DCR();
        DataSet dsGV = new DataSet();
        SalesForce sdc = new SalesForce();
        Label1.Text = "Daily Call Report (<span style='color:Red'>" + "all Dates" + "</span>) view for the month of " + sMonth;
        dsGV = sdc.GetTable(sf_code, div_code, Fdate, Tdate);
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = dsGV;
            GridView1.DataBind();
        }
        dsDCR = dc.get_dcr_DCRPendingdate(sf_code, div_code, Fdate, Tdate);

        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            DCR dcsf = new DCR();
            dssf = dcsf.getSfName_HQ(sf_code);

            if (dssf.Tables[0].Rows.Count > 0)
            {
                Sf_Name = dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                Sf_HQ = dssf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            }

            foreach (DataRow drdoc in dsDCR.Tables[0].Rows)
            {

                Table tbldetail_main3 = new Table();
                tbldetail_main3.BorderStyle = BorderStyle.None;
                tbldetail_main3.Width = 1100;
                TableRow tr_det_head_main3 = new TableRow();
                TableCell tc_det_head_main3 = new TableCell();
                tc_det_head_main3.Width = 100;
                Literal lit_det_main3 = new Literal();
                lit_det_main3.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_main3.Controls.Add(lit_det_main3);
                tr_det_head_main3.Cells.Add(tc_det_head_main3);

                TableCell tc_det_head_main4 = new TableCell();
                tc_det_head_main4.Width = 1000;


                Table tbl = new Table();
                tbl.Width = 1000;
                tbl.Style.Add("Align", "Center");

                TableRow tr_day = new TableRow();
                TableCell tc_day = new TableCell();
                tc_day.BorderStyle = BorderStyle.None;
                tc_day.ColumnSpan = 2;
                tc_day.HorizontalAlign = HorizontalAlign.Center;
                tc_day.Style.Add("font-name", "verdana;");
                Literal lit_day = new Literal();
                lit_day.Text = "<u><b>Daily Call Report - " + "<span style='color:Red'>" + drdoc["Activity_Date"].ToString() + "</span>" + "</b></u>";



                tc_day.Controls.Add(lit_day);
                tr_day.Cells.Add(tc_day);
                tbl.Rows.Add(tr_day);

                tc_det_head_main4.Controls.Add(tbl);
                tr_det_head_main3.Cells.Add(tc_det_head_main4);
                tbldetail_main3.Rows.Add(tr_det_head_main3);

                form1.Controls.Add(tbldetail_main3);

                //Pending Approval 

                Table tbldetail_mainPending = new Table();
                tbldetail_mainPending.BorderStyle = BorderStyle.None;
                tbldetail_mainPending.Width = 1100;
                TableRow tr_det_head_mainPending = new TableRow();
                TableCell tc_det_head_mainPending = new TableCell();
                tc_det_head_mainPending.Width = 100;
                Literal lit_det_mainPending = new Literal();
                lit_det_mainPending.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_mainPending.Controls.Add(lit_det_mainPending);
                tr_det_head_mainPending.Cells.Add(tc_det_head_mainPending);

                TableCell tc_det_head_mainPendingSub = new TableCell();
                tc_det_head_mainPendingSub.Width = 1000;


                Table tbldetailhosPending = new Table();
                tbldetailhosPending.BorderStyle = BorderStyle.Solid;
                tbldetailhosPending.BorderWidth = 1;
                tbldetailhosPending.GridLines = GridLines.Both;
                tbldetailhosPending.Width = 1000;
                tbldetailhosPending.Style.Add("border-collapse", "none");
                tbldetailhosPending.Style.Add("border", "none");


                dsdoc = dc.get_Pending_Single_Temp_Date(sf_code, drdoc["Activity_Date"].ToString()); //1-Listed Doctor

                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_det_Pending = new TableRow();
                    TableCell tc_det_Pending = new TableCell();
                    iCount += 1;
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center> <b> " + dsdoc.Tables[0].Rows[0]["Temp"].ToString() + " </b> </center>";
                    tc_det_Pending.Style.Add("color", "Red");
                    tc_det_Pending.Style.Add("border", "none");
                    tc_det_Pending.BorderStyle = BorderStyle.Solid;
                    tc_det_Pending.BorderWidth = 1;
                    tc_det_Pending.Controls.Add(lit_det_SNo);
                    tr_det_Pending.Cells.Add(tc_det_Pending);


                    tbldetailhosPending.Rows.Add(tr_det_Pending);
                }

                tc_det_head_mainPendingSub.Controls.Add(tbldetailhosPending);
                tr_det_head_mainPending.Cells.Add(tc_det_head_mainPendingSub);
                tbldetail_mainPending.Rows.Add(tr_det_head_mainPending);

                form1.Controls.Add(tbldetail_mainPending);


                //Pending Approval 

                // WeekOff 

                Table tbldetail_mainHoliday = new Table();
                tbldetail_mainHoliday.BorderStyle = BorderStyle.None;
                tbldetail_mainHoliday.Width = 1100;
                TableRow tr_det_head_mainHoliday = new TableRow();
                TableCell tc_det_head_mainHolday = new TableCell();
                tc_det_head_mainHolday.Width = 100;
                Literal lit_det_mainHoliday = new Literal();
                lit_det_mainHoliday.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_mainHolday.Controls.Add(lit_det_mainHoliday);
                tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHolday);

                TableCell tc_det_head_mainHoliday = new TableCell();
                tc_det_head_mainHoliday.Width = 1000;


                Table tbldetailHoliday = new Table();
                tbldetailHoliday.BorderStyle = BorderStyle.Solid;
                tbldetailHoliday.BorderWidth = 1;
                tbldetailHoliday.GridLines = GridLines.Both;
                tbldetailHoliday.Width = 1000;
                tbldetailHoliday.Style.Add("border-collapse", "none");
                tbldetailHoliday.Style.Add("border", "none");

                if (sf_code.Contains("MR"))
                {
                    dsdoc = dc.get_DCRHoliday_Name_MR(sf_code, drdoc["Activity_Date"].ToString(), 5); //5-Hospital
                }
                else
                {
                    dsdoc = dc.get_DCRHoliday_Name(sf_code, drdoc["Activity_Date"].ToString(), 5); //5-Hospital
                }
                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_det_head = new TableRow();



                    iCount = 0;
                    foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                    {
                        TableRow tr_det_sno = new TableRow();
                        TableCell tc_det_SNo = new TableCell();
                        iCount += 1;
                        Literal lit_det_SNo2 = new Literal();
                        if (sf_code.Contains("MR"))
                        {
                            //lit_det_SNo2.Text = "<center>" + drdoctor["Worktype_Name_B"].ToString() + "</center>";

                            TableRow tr_ff = new TableRow();
                            TableCell tc_ff_name = new TableCell();
                            tc_ff_name.BorderStyle = BorderStyle.None;
                            tc_ff_name.Width = 500;
                            Literal lit_ff_name = new Literal();
                            lit_ff_name.Text = "<b>Field Force Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_Name.ToString();
                            tc_ff_name.Controls.Add(lit_ff_name);
                            tr_ff.Cells.Add(tc_ff_name);

                            TableCell tc_HQ = new TableCell();
                            tc_HQ.BorderStyle = BorderStyle.None;
                            tc_HQ.Width = 500;

                            tc_HQ.HorizontalAlign = HorizontalAlign.Left;
                            Literal lit_HQ = new Literal();
                            lit_HQ.Text = "<span style='margin-left:200px'><b>Head Quarters </b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + Sf_HQ.ToString() + "</span>";
                            //lit_HQ.Text = "<b>Head Quarters</b>" +  Sf_HQ.ToString();
                            tc_HQ.Controls.Add(lit_HQ);
                            tr_ff.Cells.Add(tc_HQ);
                            tbl.Rows.Add(tr_ff);

                            TableRow tr_dcr = new TableRow();
                            TableCell tc_dcr_submit = new TableCell();
                            tc_dcr_submit.BorderStyle = BorderStyle.None;
                            tc_dcr_submit.Width = 500;
                            Literal lit_dcr_submit = new Literal();
                            lit_dcr_submit.Text = "<b>DCR Submitted on</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoc["Submission_Date"].ToString();
                            tc_dcr_submit.Controls.Add(lit_dcr_submit);
                            tr_dcr.Cells.Add(tc_dcr_submit);

                            TableCell tc_Terr = new TableCell();
                            tc_Terr.BorderStyle = BorderStyle.None;
                            tc_Terr.HorizontalAlign = HorizontalAlign.Center;
                            tc_Terr.Width = 500;
                            Literal lit_Terr = new Literal();
                            //Territory terr = new Territory();
                            //dsTerritory = terr.getWorkAreaName(div_code);
                            //lit_Terr.Text = "<b>Work Type</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoctor["Worktype_Name_B"].ToString();

                            tc_Terr.Controls.Add(lit_Terr);
                            tr_dcr.Cells.Add(tc_Terr);

                            tbl.Rows.Add(tr_dcr);

                            tc_det_head_main4.Controls.Add(tbl);
                            tr_det_head_main3.Cells.Add(tc_det_head_main4);
                            tbldetail_main3.Rows.Add(tr_det_head_main3);

                            form1.Controls.Add(tbldetail_main3);

                            Table tbl_head_empty = new Table();
                            TableRow tr_head_empty = new TableRow();
                            TableCell tc_head_empty = new TableCell();
                            Literal lit_head_empty = new Literal();
                            lit_head_empty.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Work Type</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoctor["Worktype_Name_B"].ToString();
                            tc_head_empty.Controls.Add(lit_head_empty);
                            tr_head_empty.Cells.Add(tc_head_empty);
                            tbl_head_empty.Rows.Add(tr_head_empty);
                            form1.Controls.Add(tbl_head_empty);

                            Table tbldetail_main = new Table();
                            tbldetail_main.BorderStyle = BorderStyle.None;
                            tbldetail_main.Width = 1100;
                            TableRow tr_det_head_main = new TableRow();
                            TableCell tc_det_head_main = new TableCell();
                            tc_det_head_main.Width = 100;
                            Literal lit_det_main = new Literal();
                            lit_det_main.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            tc_det_head_main.Controls.Add(lit_det_main);
                            tr_det_head_main.Cells.Add(tc_det_head_main);

                            TableCell tc_det_head_main2 = new TableCell();
                            tc_det_head_main2.Width = 1000;

                            Table tbldetail = new Table();
                            tbldetail.BorderStyle = BorderStyle.Solid;
                            tbldetail.BorderWidth = 1;
                            tbldetail.GridLines = GridLines.Both;
                            tbldetail.Width = 1000;
                            tbldetail.Style.Add("border-collapse", "collapse");
                            tbldetail.Style.Add("border", "solid 1px Black");

                            dsdoc = dc.GetTable(sf_code, drdoc["Activity_Date"].ToString()); //1-Listed Doctor
                            iCount = 0;
                            if (dsdoc.Tables[0].Rows.Count > 0)
                            {


                                //lit_Terr.Text = "<span style='margin-left:200px'><b> Territory Worked </b></span>" + "<span style='margin-left:15px'></span>:" + "<span style='margin-left:21px;text-align:right'>" + dsdoc.Tables[0].Rows[0]["che_POB_Name"].ToString() + "</span>";
                                TableRow tr_det_head1 = new TableRow();
                                TableCell tc_det_head_SNo = new TableCell();
                                tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                                tc_det_head_SNo.BorderWidth = 1;
                                tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_head_SNo = new Literal();
                                tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                                lit_det_head_SNo.Text = "<b>S.No</b>";
                                tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                                tr_det_head1.Cells.Add(tc_det_head_SNo);

                                TableCell tc_det_head_Ses = new TableCell();
                                tc_det_head_Ses.BorderStyle = BorderStyle.Solid;
                                tc_det_head_Ses.BorderWidth = 1;
                                tc_det_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_head_Ses = new Literal();
                                tc_det_head_Ses.Attributes.Add("Class", "tr_det_head");
                                lit_det_head_Ses.Text = "<b>Date</b>";
                                tc_det_head_Ses.Controls.Add(lit_det_head_Ses);
                                tr_det_head1.Cells.Add(tc_det_head_Ses);

                                TableCell tc_det_head_doc = new TableCell();
                                tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                                tc_det_head_doc.BorderWidth = 1;
                                tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_head_doc = new Literal();
                                lit_det_head_doc.Text = "<b>Name Of the Distributor</b>";
                                tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                                tc_det_head_doc.Controls.Add(lit_det_head_doc);
                                tr_det_head1.Cells.Add(tc_det_head_doc);

                                TableCell tc_det_head_time = new TableCell();
                                tc_det_head_time.BorderStyle = BorderStyle.Solid;
                                tc_det_head_time.BorderWidth = 1;
                                tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_head_time = new Literal();
                                lit_det_head_time.Text = "<b>Person Met</b>";
                                tc_det_head_time.Attributes.Add("Class", "tr_det_head");
                                tc_det_head_time.Controls.Add(lit_det_head_time);
                                tr_det_head1.Cells.Add(tc_det_head_time);



                                TableCell tc_det_head_ww = new TableCell();
                                tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                                tc_det_head_ww.BorderWidth = 1;
                                tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_head_ww = new Literal();
                                lit_det_head_ww.Text = "<b>Contact No</b>";
                                tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                                tc_det_head_ww.Controls.Add(lit_det_head_ww);
                                tr_det_head1.Cells.Add(tc_det_head_ww);

                                TableCell tc_det_head_visit = new TableCell();
                                tc_det_head_visit.BorderStyle = BorderStyle.Solid;
                                tc_det_head_visit.BorderWidth = 1;
                                tc_det_head_visit.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_head_visit = new Literal();
                                lit_det_head_visit.Text = "<b>Address</b>";
                                tc_det_head_visit.Attributes.Add("Class", "tr_det_head");
                                tc_det_head_visit.Controls.Add(lit_det_head_visit);
                                tr_det_head1.Cells.Add(tc_det_head_visit);



                                TableCell tc_det_head_spec = new TableCell();
                                tc_det_head_spec.BorderStyle = BorderStyle.Solid;
                                tc_det_head_spec.BorderWidth = 1;
                                tc_det_head_spec.HorizontalAlign = HorizontalAlign.Center;
                                Literal lit_det_head_spec = new Literal();
                                lit_det_head_spec.Text = "<b>Remarks</b>";
                                tc_det_head_spec.Attributes.Add("Class", "tr_det_head");
                                tc_det_head_spec.Controls.Add(lit_det_head_spec);
                                tr_det_head1.Cells.Add(tc_det_head_spec);








                                tbldetail.Rows.Add(tr_det_head1);

                                string strlongname = "";
                                iCount = 0;

                                foreach (DataRow drdoctor1 in dsdoc.Tables[0].Rows)
                                {


                                    TableRow tr_det_sno1 = new TableRow();
                                    TableCell tc_det_SNo1 = new TableCell();
                                    iCount += 1;
                                    Literal lit_det_SNo1 = new Literal();
                                    lit_det_SNo1.Text = "<center>" + iCount.ToString() + "</center>";
                                    tc_det_SNo1.Attributes.Add("Class", "tbldetail_Data");
                                    tc_det_SNo1.BorderStyle = BorderStyle.Solid;
                                    tc_det_SNo1.BorderWidth = 1;
                                    tc_det_SNo1.Controls.Add(lit_det_SNo1);
                                    tr_det_sno1.Cells.Add(tc_det_SNo1);

                                    TableCell tc_det_Ses = new TableCell();
                                    Literal lit_det_Ses = new Literal();
                                    lit_det_Ses.Text = "&nbsp;&nbsp;" + drdoctor1["Date"].ToString();
                                    tc_det_Ses.Attributes.Add("Class", "tbldetail_Data");
                                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                                    tc_det_Ses.BorderWidth = 1;
                                    tc_det_Ses.Controls.Add(lit_det_Ses);
                                    tr_det_sno1.Cells.Add(tc_det_Ses);

                                    TableCell tc_det_dr_name = new TableCell();
                                    Literal lit_det_dr_name = new Literal();
                                    lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor1["Shop_Name"].ToString();
                                    tc_det_dr_name.Attributes.Add("Class", "tbldetail_Data");
                                    tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                                    tc_det_dr_name.BorderWidth = 1;
                                    tc_det_dr_name.Width = 150;
                                    tc_det_dr_name.Controls.Add(lit_det_dr_name);
                                    tr_det_sno1.Cells.Add(tc_det_dr_name);

                                    TableCell tc_det_time = new TableCell();
                                    Literal lit_det_time = new Literal();
                                    lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor1["Contact_Person"].ToString();
                                    tc_det_time.Attributes.Add("Class", "tbldetail_Data");
                                    tc_det_time.BorderStyle = BorderStyle.Solid;
                                    tc_det_time.BorderWidth = 1;
                                    tc_det_time.Controls.Add(lit_det_time);
                                    tr_det_sno1.Cells.Add(tc_det_time);

                                    TableCell tc_det_LastUpdate_Date = new TableCell();
                                    Literal lit_det_time_LastUpdate_Date = new Literal();
                                    lit_det_time_LastUpdate_Date.Text = "&nbsp;&nbsp;" + drdoctor1["Phone_Number"].ToString();
                                    tc_det_LastUpdate_Date.Attributes.Add("Class", "tbldetail_Data");
                                    tc_det_LastUpdate_Date.BorderStyle = BorderStyle.Solid;
                                    tc_det_LastUpdate_Date.BorderWidth = 1;
                                    tc_det_LastUpdate_Date.Width = 120;
                                    tc_det_LastUpdate_Date.Controls.Add(lit_det_time_LastUpdate_Date);
                                    tr_det_sno1.Cells.Add(tc_det_LastUpdate_Date);



                                    TableCell tc_det_spec = new TableCell();
                                    Literal lit_det_spec = new Literal();
                                    lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor1["address"].ToString();
                                    tc_det_spec.Attributes.Add("Class", "tbldetail_Data");
                                    tc_det_spec.BorderStyle = BorderStyle.Solid;
                                    tc_det_spec.BorderWidth = 1;
                                    tc_det_spec.Controls.Add(lit_det_spec);
                                    tr_det_sno1.Cells.Add(tc_det_spec);

                                    TableCell tc_det_catg = new TableCell();
                                    Literal lit_det_catg = new Literal();
                                    lit_det_catg.Text = "&nbsp;&nbsp;" + drdoctor1["Remarks"].ToString();
                                    tc_det_catg.Attributes.Add("Class", "tbldetail_Data");
                                    tc_det_catg.BorderStyle = BorderStyle.Solid;
                                    tc_det_catg.BorderWidth = 1;
                                    tc_det_catg.Controls.Add(lit_det_catg);
                                    tr_det_sno1.Cells.Add(tc_det_catg);


                                    tbldetail.Rows.Add(tr_det_sno1);




                                }





                            }


                            tc_det_head_main2.Controls.Add(tbldetail);
                            tr_det_head_main.Cells.Add(tc_det_head_main2);
                            tbldetail_main.Rows.Add(tr_det_head_main);

                            form1.Controls.Add(tbldetail_main);


                            if (iCount > 0)
                            {
                                Table tbl_doc_empty = new Table();
                                TableRow tr_doc_empty = new TableRow();
                                TableCell tc_doc_empty = new TableCell();
                                Literal lit_doc_empty = new Literal();
                                lit_doc_empty.Text = "<BR>";
                                tc_doc_empty.Controls.Add(lit_doc_empty);
                                tr_doc_empty.Cells.Add(tc_doc_empty);
                                tbl_doc_empty.Rows.Add(tr_doc_empty);
                                form1.Controls.Add(tbl_doc_empty);


                            }

                            //2-Chemists

                            Table tbldetail_main5 = new Table();
                            tbldetail_main5.BorderStyle = BorderStyle.None;
                            tbldetail_main5.Width = 1100;
                            TableRow tr_det_head_main5 = new TableRow();
                            TableCell tc_det_head_main5 = new TableCell();
                            tc_det_head_main5.Width = 100;
                            Literal lit_det_main5 = new Literal();
                            lit_det_main5.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            tc_det_head_main5.Controls.Add(lit_det_main5);
                            tr_det_head_main5.Cells.Add(tc_det_head_main5);

                            TableCell tc_det_head_main6 = new TableCell();
                            tc_det_head_main6.Width = 1000;


                            Table tbldetailChe = new Table();
                            tbldetailChe.BorderStyle = BorderStyle.Solid;
                            tbldetailChe.BorderWidth = 1;
                            tbldetailChe.GridLines = GridLines.Both;
                            tbldetailChe.Width = 1000;
                            tbldetailChe.Style.Add("border-collapse", "collapse");
                            tbldetailChe.Style.Add("border", "solid 1px Black");

                        }
                        else
                        {
                            lit_det_SNo2.Text = "<center>" + drdoctor["Worktype_Name_M"].ToString() + "</center>";
                        }
                        tc_det_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_SNo.Attributes.Add("Class", "Holiday");
                        tc_det_SNo.BorderWidth = 1;
                        tc_det_SNo.BorderStyle = BorderStyle.None;
                        tc_det_SNo.Controls.Add(lit_det_SNo2);
                        tr_det_sno.Cells.Add(tc_det_SNo);

                        tbldetailHoliday.Rows.Add(tr_det_sno);

                        tc_det_head_mainHoliday.Controls.Add(tbldetailHoliday);
                        tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHoliday);
                        tbldetail_mainHoliday.Rows.Add(tr_det_head_mainHoliday);

                        Table tbl_line = new Table();
                        tbl_line.BorderStyle = BorderStyle.None;
                        tbl_line.Width = 1000;
                        tbl_line.Style.Add("border-collapse", "collapse");
                        tbl_line.Style.Add("border-top", "none");
                        tbl_line.Style.Add("border-right", "none");
                        tbl_line.Style.Add("margin-left", "100px");
                        tbl_line.Style.Add("border-bottom ", "solid 1px Black");

                        form1.Controls.Add(tbldetail_mainHoliday);

                        TableRow tr_line = new TableRow();

                        TableCell tc_line0 = new TableCell();
                        tc_line0.Width = 100;
                        Literal lit_line0 = new Literal();
                        lit_line0.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        tc_line0.Controls.Add(lit_line0);
                        tr_line.Cells.Add(tc_line0);

                        TableCell tc_line = new TableCell();
                        tc_line.Width = 1000;
                        Literal lit_line = new Literal();
                        // lit_line.Text = "<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>";
                        tc_line.Controls.Add(lit_line);
                        tr_line.Cells.Add(tc_line);
                        tbl_line.Rows.Add(tr_line);
                        form1.Controls.Add(tbl_line);
                    }
                }
                else
                {
                    //form1.Controls.Add(tbldetailhos);

                    TableRow tr_ff = new TableRow();
                    TableCell tc_ff_name = new TableCell();
                    tc_ff_name.BorderStyle = BorderStyle.None;
                    tc_ff_name.Width = 500;
                    Literal lit_ff_name = new Literal();
                    lit_ff_name.Text = "<b>Field Force Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_Name.ToString();
                    tc_ff_name.Controls.Add(lit_ff_name);
                    tr_ff.Cells.Add(tc_ff_name);

                    TableCell tc_HQ = new TableCell();
                    tc_HQ.BorderStyle = BorderStyle.None;
                    tc_HQ.Width = 500;

                    tc_HQ.HorizontalAlign = HorizontalAlign.Left;
                    Literal lit_HQ = new Literal();
                    lit_HQ.Text = "<span style='margin-left:200px'><b>Head Quarters </b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + Sf_HQ.ToString() + "</span>";
                    //lit_HQ.Text = "<b>Head Quarters</b>" +  Sf_HQ.ToString();
                    tc_HQ.Controls.Add(lit_HQ);
                    tr_ff.Cells.Add(tc_HQ);
                    tbl.Rows.Add(tr_ff);

                    TableRow tr_dcr = new TableRow();
                    TableCell tc_dcr_submit = new TableCell();
                    tc_dcr_submit.BorderStyle = BorderStyle.None;
                    tc_dcr_submit.Width = 500;
                    Literal lit_dcr_submit = new Literal();
                    lit_dcr_submit.Text = "<b>DCR Submitted on</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoc["Submission_Date"].ToString();
                    tc_dcr_submit.Controls.Add(lit_dcr_submit);
                    tr_dcr.Cells.Add(tc_dcr_submit);

                    TableCell tc_Terr = new TableCell();
                    tc_Terr.BorderStyle = BorderStyle.None;
                    tc_Terr.HorizontalAlign = HorizontalAlign.Left;
                    tc_Terr.Width = 500;
                    Literal lit_Terr = new Literal();
                    Territory terr = new Territory();
                    dsTerritory = terr.getWorkAreaName(div_code);
                    //lit_Terr.Text = "<span style='margin-left:280px'><b>" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + " Worked </b></span>" + "<span style='margin-left:15px'></span>:" + "<span style='margin-left:21px;text-align:right'>" + drdoc["Plan_Name"].ToString() + "</span>";

                    tc_Terr.Controls.Add(lit_Terr);
                    tr_dcr.Cells.Add(tc_Terr);

                    tbl.Rows.Add(tr_dcr);

                    tc_det_head_main4.Controls.Add(tbl);
                    tr_det_head_main3.Cells.Add(tc_det_head_main4);
                    tbldetail_main3.Rows.Add(tr_det_head_main3);

                    form1.Controls.Add(tbldetail_main3);

                    Table tbl_head_empty = new Table();
                    TableRow tr_head_empty = new TableRow();
                    TableCell tc_head_empty = new TableCell();
                    Literal lit_head_empty = new Literal();
                    lit_head_empty.Text = "<BR>";
                    tc_head_empty.Controls.Add(lit_head_empty);
                    tr_head_empty.Cells.Add(tc_head_empty);
                    tbl_head_empty.Rows.Add(tr_head_empty);
                    form1.Controls.Add(tbl_head_empty);

                    Table tbldetail_main = new Table();
                    tbldetail_main.BorderStyle = BorderStyle.None;
                    tbldetail_main.Width = 1100;
                    TableRow tr_det_head_main = new TableRow();
                    TableCell tc_det_head_main = new TableCell();
                    tc_det_head_main.Width = 100;
                    Literal lit_det_main = new Literal();
                    lit_det_main.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    tc_det_head_main.Controls.Add(lit_det_main);
                    tr_det_head_main.Cells.Add(tc_det_head_main);

                    TableCell tc_det_head_main2 = new TableCell();
                    tc_det_head_main2.Width = 1000;

                    Table tbldetail = new Table();
                    tbldetail.BorderStyle = BorderStyle.Solid;
                    tbldetail.BorderWidth = 1;
                    tbldetail.GridLines = GridLines.Both;
                    tbldetail.Width = 1000;
                    tbldetail.Style.Add("border-collapse", "collapse");
                    tbldetail.Style.Add("border", "solid 1px Black");



                    dsdoc = dc.get_dcr_details1(sf_code, drdoc["Activity_Date"].ToString(), 1); //1-Listed Doctor
                    iCount = 0;
                    if (dsdoc.Tables[0].Rows.Count > 0)
                    {


                        lit_Terr.Text = "<span style='margin-left:200px'><b> Territory Worked </b></span>" + "<span style='margin-left:15px'></span>:" + "<span style='margin-left:21px;text-align:right'>" + dsdoc.Tables[0].Rows[0]["SDP_Name"].ToString() + "</span>";
                        TableRow tr_det_head = new TableRow();
                        TableCell tc_det_head_SNo = new TableCell();
                        tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_head_SNo.BorderWidth = 1;
                        tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_head_SNo = new Literal();
                        tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                        lit_det_head_SNo.Text = "<b>S.No</b>";
                        tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                        tr_det_head.Cells.Add(tc_det_head_SNo);

                        TableCell tc_det_head_Ses = new TableCell();
                        tc_det_head_Ses.BorderStyle = BorderStyle.Solid;
                        tc_det_head_Ses.BorderWidth = 1;
                        tc_det_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_head_Ses = new Literal();
                        tc_det_head_Ses.Attributes.Add("Class", "tr_det_head");
                        lit_det_head_Ses.Text = "<b>Ses</b>";
                        tc_det_head_Ses.Controls.Add(lit_det_head_Ses);
                        tr_det_head.Cells.Add(tc_det_head_Ses);

                        TableCell tc_det_head_doc = new TableCell();
                        tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                        tc_det_head_doc.BorderWidth = 1;
                        tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_head_doc = new Literal();
                        lit_det_head_doc.Text = "<b>Retailer Name</b>";
                        tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_doc.Controls.Add(lit_det_head_doc);
                        tr_det_head.Cells.Add(tc_det_head_doc);

                        TableCell tc_det_head_time = new TableCell();
                        tc_det_head_time.BorderStyle = BorderStyle.Solid;
                        tc_det_head_time.BorderWidth = 1;
                        tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_head_time = new Literal();
                        lit_det_head_time.Text = "<b>Time</b>";
                        tc_det_head_time.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_time.Controls.Add(lit_det_head_time);
                        tr_det_head.Cells.Add(tc_det_head_time);

                        TableCell tc_det_head_Last_Update_Date = new TableCell();
                        tc_det_head_Last_Update_Date.BorderStyle = BorderStyle.Solid;
                        tc_det_head_Last_Update_Date.BorderWidth = 1;
                        tc_det_head_Last_Update_Date.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_head_Last_Update_Date = new Literal();
                        lit_det_head_Last_Update_Date.Text = "<b>Last Updated</b>";
                        tc_det_head_Last_Update_Date.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_Last_Update_Date.Controls.Add(lit_det_head_Last_Update_Date);
                        tr_det_head.Cells.Add(tc_det_head_Last_Update_Date);

                        TableCell tc_det_head_ww = new TableCell();
                        tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                        tc_det_head_ww.BorderWidth = 1;
                        tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_head_ww = new Literal();
                        lit_det_head_ww.Text = "<b>Worked With</b>";
                        tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_ww.Controls.Add(lit_det_head_ww);
                        tr_det_head.Cells.Add(tc_det_head_ww);

                        TableCell tc_det_head_visit = new TableCell();
                        tc_det_head_visit.BorderStyle = BorderStyle.Solid;
                        tc_det_head_visit.BorderWidth = 1;
                        tc_det_head_visit.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_head_visit = new Literal();
                        lit_det_head_visit.Text = "<b>Latest Visit</b>";
                        tc_det_head_visit.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_visit.Controls.Add(lit_det_head_visit);
                        tr_det_head.Cells.Add(tc_det_head_visit);

                        TableCell tc_det_head_class = new TableCell();
                        tc_det_head_class.BorderStyle = BorderStyle.Solid;
                        tc_det_head_class.BorderWidth = 1;
                        tc_det_head_class.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_head_cla = new Literal();
                        lit_det_head_cla.Text = "<b>Class</b>";
                        tc_det_head_class.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_class.Controls.Add(lit_det_head_cla);
                        tr_det_head.Cells.Add(tc_det_head_class);

                        TableCell tc_det_head_spec = new TableCell();
                        tc_det_head_spec.BorderStyle = BorderStyle.Solid;
                        tc_det_head_spec.BorderWidth = 1;
                        tc_det_head_spec.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_head_spec = new Literal();
                        lit_det_head_spec.Text = "<b>Channel</b>";
                        tc_det_head_spec.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_spec.Controls.Add(lit_det_head_spec);
                        tr_det_head.Cells.Add(tc_det_head_spec);


                        TableCell tc_det_head_catg = new TableCell();
                        tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                        tc_det_head_catg.BorderWidth = 1;
                        tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_head_catg = new Literal();
                        lit_det_head_catg.Text = "<b>Stockist Name</b>";
                        tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_catg.Controls.Add(lit_det_head_catg);
                        tr_det_head.Cells.Add(tc_det_head_catg);




                        TableCell tc_det_head_SDP_Plan = new TableCell();
                        tc_det_head_SDP_Plan.BorderStyle = BorderStyle.Solid;
                        tc_det_head_SDP_Plan.BorderWidth = 1;
                        tc_det_head_SDP_Plan.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_head_SDP_Plan = new Literal();
                        lit_det_head_SDP_Plan.Text = "<b>" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "</b>";
                        tc_det_head_SDP_Plan.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_SDP_Plan.Controls.Add(lit_det_head_SDP_Plan);
                        tr_det_head.Cells.Add(tc_det_head_SDP_Plan);

                        TableCell tc_det_head_Actual_Place = new TableCell();
                        tc_det_head_Actual_Place.BorderStyle = BorderStyle.Solid;
                        tc_det_head_Actual_Place.BorderWidth = 1;
                        tc_det_head_Actual_Place.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_head_Actual_Place = new Literal();
                        lit_det_head_Actual_Place.Text = "<b>Actual Place of Worked</b>";
                        tc_det_head_Actual_Place.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_Actual_Place.Controls.Add(lit_det_head_Actual_Place);
                        tr_det_head.Cells.Add(tc_det_head_Actual_Place);

                        //TableCell tc_det_head_CallFeed_Back = new TableCell();
                        //tc_det_head_CallFeed_Back.BorderStyle = BorderStyle.Solid;
                        //tc_det_head_CallFeed_Back.BorderWidth = 1;
                        //tc_det_head_CallFeed_Back.HorizontalAlign = HorizontalAlign.Center;
                        //Literal lit_det_head_CallFeed_Back = new Literal();
                        //lit_det_head_CallFeed_Back.Text = "<b>Call Feedback</b>";
                        //tc_det_head_CallFeed_Back.Attributes.Add("Class", "tr_det_head");
                        //tc_det_head_CallFeed_Back.Controls.Add(lit_det_head_CallFeed_Back);
                        //tr_det_head.Cells.Add(tc_det_head_CallFeed_Back);

                        //TableCell tc_det_head_prod = new TableCell();
                        //tc_det_head_prod.BorderStyle = BorderStyle.Solid;
                        //tc_det_head_prod.BorderWidth = 1;
                        //tc_det_head_prod.HorizontalAlign = HorizontalAlign.Center;
                        // Literal lit_det_head_prod = new Literal();
                        // lit_det_head_prod.Text = "<b>Product Sampled</b>";
                        // tc_det_head_prod.Attributes.Add("Class", "tr_det_head");
                        // tc_det_head_prod.Controls.Add(lit_det_head_prod);
                        // tr_det_head.Cells.Add(tc_det_head_prod);

                        TableCell tc_det_head_gift = new TableCell();
                        tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                        tc_det_head_gift.BorderWidth = 1;
                        tc_det_head_gift.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_head_gift = new Literal();
                        lit_det_head_gift.Text = "<b>Order Value</b>";
                        tc_det_head_gift.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_gift.Controls.Add(lit_det_head_gift);
                        tr_det_head.Cells.Add(tc_det_head_gift);


                        TableCell tc_det_head_Quai1 = new TableCell();
                        tc_det_head_Quai1.BorderStyle = BorderStyle.Solid;
                        tc_det_head_Quai1.BorderWidth = 1;
                        Literal lit_det_head_quai1 = new Literal();
                        lit_det_head_quai1.Text = "<b>Net Weight</b>";
                        tc_det_head_Quai1.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_Quai1.Controls.Add(lit_det_head_quai1);
                        tc_det_head_Quai1.HorizontalAlign = HorizontalAlign.Center;
                        tr_det_head.Cells.Add(tc_det_head_Quai1);


                        TableCell rehead = new TableCell();
                        rehead.BorderStyle = BorderStyle.Solid;
                        rehead.BorderWidth = 1;
                        Literal reheadlit = new Literal();
                        reheadlit.Text = "<b>Remarks</b>";
                        rehead.Attributes.Add("Class", "tr_det_head");
                        rehead.Controls.Add(reheadlit);
                        rehead.HorizontalAlign = HorizontalAlign.Center;
                        tr_det_head.Cells.Add(rehead);






                        tbldetail.Rows.Add(tr_det_head);

                        string strlongname = "";
                        iCount = 0;

                        foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                        {


                            TableRow tr_det_sno = new TableRow();
                            TableCell tc_det_SNo = new TableCell();
                            iCount += 1;
                            Literal lit_det_SNo = new Literal();
                            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                            tc_det_SNo.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_SNo.BorderStyle = BorderStyle.Solid;
                            tc_det_SNo.BorderWidth = 1;
                            tc_det_SNo.Controls.Add(lit_det_SNo);
                            tr_det_sno.Cells.Add(tc_det_SNo);

                            TableCell tc_det_Ses = new TableCell();
                            Literal lit_det_Ses = new Literal();
                            lit_det_Ses.Text = "&nbsp;&nbsp;" + drdoctor["Session"].ToString();
                            tc_det_Ses.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_Ses.BorderStyle = BorderStyle.Solid;
                            tc_det_Ses.BorderWidth = 1;
                            tc_det_Ses.Controls.Add(lit_det_Ses);
                            tr_det_sno.Cells.Add(tc_det_Ses);

                            TableCell tc_det_dr_name = new TableCell();
                            Literal lit_det_dr_name = new Literal();
                            lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["ListedDr_Name"].ToString();
                            tc_det_dr_name.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_name.BorderWidth = 1;
                            tc_det_dr_name.Width = 150;
                            tc_det_dr_name.Controls.Add(lit_det_dr_name);
                            tr_det_sno.Cells.Add(tc_det_dr_name);

                            TableCell tc_det_time = new TableCell();
                            Literal lit_det_time = new Literal();
                            lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor["Time"].ToString();
                            tc_det_time.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_time.BorderStyle = BorderStyle.Solid;
                            tc_det_time.BorderWidth = 1;
                            tc_det_time.Controls.Add(lit_det_time);
                            tr_det_sno.Cells.Add(tc_det_time);

                            TableCell tc_det_LastUpdate_Date = new TableCell();
                            Literal lit_det_time_LastUpdate_Date = new Literal();
                            lit_det_time_LastUpdate_Date.Text = "&nbsp;&nbsp;" + drdoctor["ModTime"].ToString();
                            tc_det_LastUpdate_Date.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_LastUpdate_Date.BorderStyle = BorderStyle.Solid;
                            tc_det_LastUpdate_Date.BorderWidth = 1;
                            tc_det_LastUpdate_Date.Width = 120;
                            tc_det_LastUpdate_Date.Controls.Add(lit_det_time_LastUpdate_Date);
                            tr_det_sno.Cells.Add(tc_det_LastUpdate_Date);

                            TableCell tc_det_work = new TableCell();
                            Literal lit_det_work = new Literal();
                            lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                            tc_det_work.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_work.BorderStyle = BorderStyle.Solid;
                            tc_det_work.BorderWidth = 1;
                            tc_det_work.Controls.Add(lit_det_work);
                            tr_det_sno.Cells.Add(tc_det_work);

                            TableCell tc_det_lvisit = new TableCell();
                            Literal lit_det_lvisit = new Literal();
                            lit_det_lvisit.Text = ""; // drdoctor["lvisit"].ToString();
                            tc_det_lvisit.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_lvisit.BorderStyle = BorderStyle.Solid;
                            tc_det_lvisit.BorderWidth = 1;
                            tc_det_lvisit.Controls.Add(lit_det_lvisit);
                            tr_det_sno.Cells.Add(tc_det_lvisit);

                            TableCell tc_det_class = new TableCell();
                            Literal lit_det_class = new Literal();
                            lit_det_class.Text = "&nbsp;&nbsp;" + drdoctor["Doc_ClsName"].ToString();
                            tc_det_class.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_class.BorderStyle = BorderStyle.Solid;
                            tc_det_class.BorderWidth = 1;
                            tc_det_class.Controls.Add(lit_det_class);
                            tr_det_sno.Cells.Add(tc_det_class);

                            TableCell tc_det_spec = new TableCell();
                            Literal lit_det_spec = new Literal();
                            lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Special_Name"].ToString();
                            tc_det_spec.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_spec.BorderStyle = BorderStyle.Solid;
                            tc_det_spec.BorderWidth = 1;
                            tc_det_spec.Controls.Add(lit_det_spec);
                            tr_det_sno.Cells.Add(tc_det_spec);

                            TableCell tc_det_catg = new TableCell();
                            Literal lit_det_catg = new Literal();
                            lit_det_catg.Text = "&nbsp;&nbsp;" + drdoctor["stockist_name"].ToString();
                            tc_det_catg.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_catg.BorderStyle = BorderStyle.Solid;
                            tc_det_catg.BorderWidth = 1;
                            tc_det_catg.Controls.Add(lit_det_catg);
                            tr_det_sno.Cells.Add(tc_det_catg);

                            TableCell tc_det_SDP_Plan = new TableCell();
                            Literal lit_det_SDP_Plan = new Literal();
                            lit_det_SDP_Plan.Text = "&nbsp;&nbsp;" + drdoctor["SDP_Name"].ToString();
                            tc_det_SDP_Plan.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_SDP_Plan.BorderStyle = BorderStyle.Solid;
                            tc_det_SDP_Plan.BorderWidth = 1;
                            tc_det_SDP_Plan.Controls.Add(lit_det_SDP_Plan);
                            tr_det_sno.Cells.Add(tc_det_SDP_Plan);

                            TableCell tc_det_ActualPlace = new TableCell();
                            Literal lit_det_ActualPlace = new Literal();

                            if (drdoctor["GeoAddrs"].ToString().Trim() == "NA" && drdoctor["lati"] != "")
                            {
                                lit_det_ActualPlace.Text = strlongname;
                            }
                            else if (drdoctor["GeoAddrs"].ToString().Trim() != "NA")
                            {
                                lit_det_ActualPlace.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                            }
                            else if (drdoctor["GeoAddrs"].ToString().Trim() == "NA")
                            {
                                lit_det_ActualPlace.Text = "";
                            }

                            tc_det_ActualPlace.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_ActualPlace.BorderStyle = BorderStyle.Solid;
                            tc_det_ActualPlace.Width = 250;
                            tc_det_ActualPlace.BorderWidth = 1;
                            tc_det_ActualPlace.Controls.Add(lit_det_ActualPlace);
                            tr_det_sno.Cells.Add(tc_det_ActualPlace);


                            //TableCell tc_det_prod = new TableCell();
                            // Literal lit_det_prod = new Literal();
                            // lit_det_prod.Text = drdoctor["Product_Detail"].ToString().Replace("~", "(").Trim();
                            // tc_det_prod.Attributes.Add("Class", "tbldetail_Data");
                            // lit_det_prod.Text = lit_det_prod.Text.Replace("$", ")").Trim();
                            // lit_det_prod.Text = "&nbsp;&nbsp;" + lit_det_prod.Text.Replace("#", "  ").Trim();
                            // tc_det_prod.BorderStyle = BorderStyle.Solid;
                            //tc_det_CallFeedBack.Width = 150;
                            // tc_det_prod.BorderWidth = 1;
                            //tc_det_prod.Controls.Add(lit_det_prod);
                            // tr_det_sno.Cells.Add(tc_det_prod);

                            TableCell tc_det_gift = new TableCell();
                            HyperLink lit_det_gift = new HyperLink();
                            lit_det_gift.Text = drdoctor["POB_Value"].ToString();
                            tc_det_gift.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_gift.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_gift.BorderStyle = BorderStyle.Solid;
                            tc_det_gift.BorderWidth = 1;
                            tc_det_gift.Controls.Add(lit_det_gift);
                            tr_det_sno.Cells.Add(tc_det_gift);
                            stURL = "rpt_dcrproductdetail.aspx?Sf_Code=" + sf_code + "&Activity_date=" + drdoc["Activity_Date"].ToString() + "&div_code=" + div_code + "&Sf_Name=" + Sf_Name + "&retailer_name=" + drdoctor["ListedDr_Name"].ToString() + "&retailer_code=" + drdoctor["Trans_Detail_Info_Code"].ToString() + "";
                            lit_det_gift.Attributes["onclick"] = "javascript:window.open('" + stURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                            iTotLstCount += Decimal.Parse(drdoctor["POB_Value"].ToString());

                            lit_det_gift.NavigateUrl = "#";
                            TableCell tc_det_CallFeedBack = new TableCell();
                            Literal lit_det_CallFeedBack = new Literal();
                            lit_det_CallFeedBack.Text = drdoctor["net_weight_value"].ToString();
                            iTotLstCountt += Decimal.Parse(drdoctor["net_weight_value"].ToString());
                            tc_det_CallFeedBack.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_CallFeedBack.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_CallFeedBack.BorderStyle = BorderStyle.Solid;
                            tc_det_CallFeedBack.BorderWidth = 1;
                            tc_det_CallFeedBack.Controls.Add(lit_det_CallFeedBack);
                            tr_det_sno.Cells.Add(tc_det_CallFeedBack);


                            TableCell remark = new TableCell();
                            Literal remarklit = new Literal();
                            remarklit.Text = drdoctor["Activity_Remarks"].ToString();
                            remark.HorizontalAlign = HorizontalAlign.Center;
                            remark.Attributes.Add("Class", "tbldetail_Data");
                            remark.BorderStyle = BorderStyle.Solid;
                            remark.BorderWidth = 1;
                            remark.Controls.Add(remarklit);
                            tr_det_sno.Cells.Add(remark);

                            tbldetail.Rows.Add(tr_det_sno);




                        }





                    }


                    tc_det_head_main2.Controls.Add(tbldetail);
                    tr_det_head_main.Cells.Add(tc_det_head_main2);
                    tbldetail_main.Rows.Add(tr_det_head_main);

                    form1.Controls.Add(tbldetail_main);
                    //total
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
                    tc_Count_Total.ColumnSpan = 12;
                    tc_Count_Total.Style.Add("text-align", "left");
                    tc_Count_Total.Style.Add("font-family", "Calibri");
                    tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
                    tc_Count_Total.Style.Add("font-size", "10pt");

                    tr_total.Cells.Add(tc_Count_Total);






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


                    TableCell tc_tot_montht = new TableCell();
                    HyperLink hyp_montht = new HyperLink();


                    hyp_montht.Text = iTotLstCountt.ToString();


                    tc_tot_montht.BorderStyle = BorderStyle.Solid;
                    tc_tot_montht.BorderWidth = 1;
                    tc_tot_montht.BackColor = System.Drawing.Color.White;
                    tc_tot_montht.Width = 200;
                    tc_tot_montht.Style.Add("font-family", "Calibri");
                    tc_tot_montht.Style.Add("font-size", "10pt");
                    tc_tot_montht.HorizontalAlign = HorizontalAlign.Center;
                    tc_tot_montht.VerticalAlign = VerticalAlign.Middle;
                    tc_tot_montht.Controls.Add(hyp_montht);
                    tc_tot_montht.Attributes.Add("style", "font-weight:bold;");
                    tc_tot_montht.Attributes.Add("Class", "rptCellBorder");
                    tr_total.Cells.Add(tc_tot_montht);


                    iTotLstCountt = 0;




                    tbldetail.Rows.Add(tr_total);

                    if (iCount > 0)
                    {
                        Table tbl_doc_empty = new Table();
                        TableRow tr_doc_empty = new TableRow();
                        TableCell tc_doc_empty = new TableCell();
                        Literal lit_doc_empty = new Literal();
                        lit_doc_empty.Text = "<BR>";
                        tc_doc_empty.Controls.Add(lit_doc_empty);
                        tr_doc_empty.Cells.Add(tc_doc_empty);
                        tbl_doc_empty.Rows.Add(tr_doc_empty);
                        form1.Controls.Add(tbl_doc_empty);


                    }

                    //2-Chemists

                    Table tbldetail_main5 = new Table();
                    tbldetail_main5.BorderStyle = BorderStyle.None;
                    tbldetail_main5.Width = 1100;
                    TableRow tr_det_head_main5 = new TableRow();
                    TableCell tc_det_head_main5 = new TableCell();
                    tc_det_head_main5.Width = 100;
                    Literal lit_det_main5 = new Literal();
                    lit_det_main5.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    tc_det_head_main5.Controls.Add(lit_det_main5);
                    tr_det_head_main5.Cells.Add(tc_det_head_main5);

                    TableCell tc_det_head_main6 = new TableCell();
                    tc_det_head_main6.Width = 1000;


                    Table tbldetailChe = new Table();
                    tbldetailChe.BorderStyle = BorderStyle.Solid;
                    tbldetailChe.BorderWidth = 1;
                    tbldetailChe.GridLines = GridLines.Both;
                    tbldetailChe.Width = 1000;
                    tbldetailChe.Style.Add("border-collapse", "collapse");
                    tbldetailChe.Style.Add("border", "solid 1px Black");

                    dsdoc = dc.get_dcr_che_details(sf_code, drdoc["Activity_Date"].ToString(), 2); //2-Chemists
                    iCount = 0;
                    if (dsdoc.Tables[0].Rows.Count > 0)
                    {
                        TableRow tr_det_head = new TableRow();
                        TableCell tc_det_head_SNo = new TableCell();
                        tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_SNo.BorderWidth = 1;
                        Literal lit_det_head_SNo = new Literal();
                        lit_det_head_SNo.Text = "<b>S.No</b>";
                        tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                        tr_det_head.Cells.Add(tc_det_head_SNo);

                        TableCell tc_det_head_doc = new TableCell();
                        tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                        tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_doc.BorderWidth = 1;
                        Literal lit_det_head_doc = new Literal();
                        lit_det_head_doc.Text = "<b>Chemists Name</b>";
                        tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_doc.Controls.Add(lit_det_head_doc);
                        tr_det_head.Cells.Add(tc_det_head_doc);

                        TableCell tc_det_head_Visit_Time = new TableCell();
                        tc_det_head_Visit_Time.BorderStyle = BorderStyle.Solid;
                        tc_det_head_Visit_Time.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_Visit_Time.BorderWidth = 1;
                        Literal lit_det_head_Visit_time = new Literal();
                        lit_det_head_Visit_time.Text = "<b>Visit Time</b>";
                        tc_det_head_Visit_Time.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_Visit_Time.Controls.Add(lit_det_head_Visit_time);
                        tr_det_head.Cells.Add(tc_det_head_Visit_Time);

                        TableCell tc_det_head_Last_Updated = new TableCell();
                        tc_det_head_Last_Updated.BorderStyle = BorderStyle.Solid;
                        tc_det_head_Last_Updated.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_Last_Updated.BorderWidth = 1;
                        Literal lit_det_head_Last_Updated = new Literal();
                        lit_det_head_Last_Updated.Text = "<b>Last Updated</b>";
                        tc_det_head_Last_Updated.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_Last_Updated.Controls.Add(lit_det_head_Last_Updated);
                        tr_det_head.Cells.Add(tc_det_head_Last_Updated);

                        TableCell tc_det_head_ww = new TableCell();
                        tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                        tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_ww.BorderWidth = 1;
                        Literal lit_det_head_ww = new Literal();
                        lit_det_head_ww.Text = "<b>Worked With</b>";
                        tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_ww.Controls.Add(lit_det_head_ww);
                        tr_det_head.Cells.Add(tc_det_head_ww);

                        TableCell tc_det_head_Act_Place_Worked = new TableCell();
                        tc_det_head_Act_Place_Worked.BorderStyle = BorderStyle.Solid;
                        tc_det_head_Act_Place_Worked.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_Act_Place_Worked.BorderWidth = 1;
                        Literal lit_det_head_Act_Place_Worked = new Literal();
                        lit_det_head_Act_Place_Worked.Text = "<b>Actual Place of Worked</b>";
                        tc_det_head_Act_Place_Worked.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_Act_Place_Worked.Controls.Add(lit_det_head_Act_Place_Worked);
                        tr_det_head.Cells.Add(tc_det_head_Act_Place_Worked);

                        //TableCell tc_det_head_CallFeedBack = new TableCell();
                        //tc_det_head_CallFeedBack.BorderStyle = BorderStyle.Solid;
                        //tc_det_head_CallFeedBack.HorizontalAlign = HorizontalAlign.Center;
                        //tc_det_head_CallFeedBack.BorderWidth = 1;
                        //Literal lit_det_head_CallFeedBack = new Literal();
                        //lit_det_head_CallFeedBack.Text = "<b>Call Feedback</b>";
                        //tc_det_head_CallFeedBack.Attributes.Add("Class", "tr_det_head");
                        //tc_det_head_CallFeedBack.Controls.Add(lit_det_head_CallFeedBack);
                        //tr_det_head.Cells.Add(tc_det_head_CallFeedBack);

                        TableCell tc_det_head_catg = new TableCell();
                        tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                        tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_catg.BorderWidth = 1;
                        Literal lit_det_head_catg = new Literal();
                        lit_det_head_catg.Text = "<b>POB</b>";
                        tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_catg.Controls.Add(lit_det_head_catg);
                        tr_det_head.Cells.Add(tc_det_head_catg);


                        tbldetailChe.Rows.Add(tr_det_head);

                        iCount = 0;
                        foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                        {
                            TableRow tr_det_sno = new TableRow();
                            TableCell tc_det_SNo = new TableCell();
                            iCount += 1;
                            Literal lit_det_SNo = new Literal();
                            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                            tc_det_SNo.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_SNo.BorderStyle = BorderStyle.Solid;
                            tc_det_SNo.BorderWidth = 1;
                            tc_det_SNo.Controls.Add(lit_det_SNo);
                            tr_det_sno.Cells.Add(tc_det_SNo);

                            TableCell tc_det_dr_name = new TableCell();
                            Literal lit_det_dr_name = new Literal();
                            lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["Chemists_Name"].ToString();
                            tc_det_dr_name.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_name.BorderWidth = 1;
                            tc_det_dr_name.Controls.Add(lit_det_dr_name);
                            tr_det_sno.Cells.Add(tc_det_dr_name);

                            TableCell tc_det_dr_VisitTime = new TableCell();
                            Literal lit_det_dr_VisitTime = new Literal();
                            lit_det_dr_VisitTime.Text = "&nbsp;&nbsp;" + drdoctor["vstTime"].ToString();
                            tc_det_dr_VisitTime.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_dr_VisitTime.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_VisitTime.BorderWidth = 1;
                            tc_det_dr_VisitTime.Controls.Add(lit_det_dr_VisitTime);
                            tr_det_sno.Cells.Add(tc_det_dr_VisitTime);

                            TableCell tc_det_dr_LastUpdated = new TableCell();
                            Literal lit_det_dr_LastUpdated = new Literal();
                            lit_det_dr_LastUpdated.Text = "&nbsp;&nbsp;" + drdoctor["ModTime"].ToString();
                            tc_det_dr_LastUpdated.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_dr_LastUpdated.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_LastUpdated.BorderWidth = 1;
                            tc_det_dr_LastUpdated.Controls.Add(lit_det_dr_LastUpdated);
                            tr_det_sno.Cells.Add(tc_det_dr_LastUpdated);

                            TableCell tc_det_work = new TableCell();
                            Literal lit_det_work = new Literal();
                            lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                            tc_det_work.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_work.BorderStyle = BorderStyle.Solid;
                            tc_det_work.BorderWidth = 1;
                            tc_det_work.Controls.Add(lit_det_work);
                            tr_det_sno.Cells.Add(tc_det_work);

                            TableCell tc_det_dr_Act_Place_Worked = new TableCell();
                            Literal lit_det_dr_Act_Place_Worked = new Literal();
                            if (drdoctor["GeoAddrs"].ToString().Trim() != "NA")
                            {
                                lit_det_dr_Act_Place_Worked.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                            }
                            else if (drdoctor["GeoAddrs"].ToString().Trim() == "NA")
                            {
                                lit_det_dr_Act_Place_Worked.Text = "";
                            }
                            // lit_det_dr_Act_Place_Worked.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                            tc_det_dr_Act_Place_Worked.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_dr_Act_Place_Worked.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_Act_Place_Worked.BorderWidth = 1;
                            tc_det_dr_Act_Place_Worked.Width = 250;
                            tc_det_dr_Act_Place_Worked.Controls.Add(lit_det_dr_Act_Place_Worked);
                            tr_det_sno.Cells.Add(tc_det_dr_Act_Place_Worked);

                            //TableCell tc_det_dr_CallFeedBack = new TableCell();
                            //Literal lit_det_dr_CallFeedBack = new Literal();
                            //lit_det_dr_CallFeedBack.Text = "&nbsp;&nbsp;" + drdoctor["Rx"].ToString();
                            //tc_det_dr_CallFeedBack.Attributes.Add("Class", "tbldetail_Data");
                            //tc_det_dr_CallFeedBack.BorderStyle = BorderStyle.Solid;
                            //tc_det_dr_CallFeedBack.BorderWidth = 1;
                            //tc_det_dr_CallFeedBack.Controls.Add(lit_det_dr_CallFeedBack);
                            //tr_det_sno.Cells.Add(tc_det_dr_CallFeedBack);

                            TableCell tc_det_spec = new TableCell();
                            Literal lit_det_spec = new Literal();
                            lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                            tc_det_spec.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_spec.BorderStyle = BorderStyle.Solid;
                            tc_det_spec.BorderWidth = 1;
                            tc_det_spec.Controls.Add(lit_det_spec);
                            tr_det_sno.Cells.Add(tc_det_spec);

                            tbldetailChe.Rows.Add(tr_det_sno);
                        }
                    }

                    //form1.Controls.Add(tbldetailChe);

                    tc_det_head_main6.Controls.Add(tbldetailChe);
                    tr_det_head_main5.Cells.Add(tc_det_head_main6);
                    tbldetail_main5.Rows.Add(tr_det_head_main5);

                    form1.Controls.Add(tbldetail_main5);


                    if (iCount > 0)
                    {
                        Table tbl_chem_empty = new Table();
                        TableRow tr_chem_empty = new TableRow();
                        TableCell tc_chem_empty = new TableCell();
                        Literal lit_chem_empty = new Literal();
                        lit_chem_empty.Text = "<BR>";
                        tc_chem_empty.Controls.Add(lit_chem_empty);
                        tr_chem_empty.Cells.Add(tc_chem_empty);
                        tbl_chem_empty.Rows.Add(tr_chem_empty);
                        form1.Controls.Add(tbl_chem_empty);
                    }

                    //4-UnListed Doctor

                    Table tbldetail_main7 = new Table();
                    tbldetail_main7.BorderStyle = BorderStyle.None;
                    tbldetail_main7.Width = 1100;
                    TableRow tr_det_head_main7 = new TableRow();
                    TableCell tc_det_head_main7 = new TableCell();
                    tc_det_head_main7.Width = 100;
                    Literal lit_det_main7 = new Literal();
                    lit_det_main7.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    tc_det_head_main7.Controls.Add(lit_det_main7);
                    tr_det_head_main7.Cells.Add(tc_det_head_main7);

                    TableCell tc_det_head_main8 = new TableCell();
                    tc_det_head_main8.Width = 1000;

                    Table tblUnLstDoc = new Table();
                    tblUnLstDoc.BorderStyle = BorderStyle.Solid;
                    tblUnLstDoc.BorderWidth = 1;
                    tblUnLstDoc.GridLines = GridLines.Both;
                    tblUnLstDoc.Width = 1000;
                    tblUnLstDoc.Style.Add("border-collapse", "collapse");
                    tblUnLstDoc.Style.Add("border", "solid 1px Black");

                    dsdoc = dc.get_unlst_doc_details(sf_code, drdoc["Activity_Date"].ToString(), 1); //1-Listed Doctor
                    iCount = 0;
                    if (dsdoc.Tables[0].Rows.Count > 0)
                    {
                        TableRow tr_UnLst_doc_head = new TableRow();
                        TableCell tc_UnLst_doc_head_SNo = new TableCell();
                        tc_UnLst_doc_head_SNo.BorderStyle = BorderStyle.Solid;
                        tc_UnLst_doc_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                        tc_UnLst_doc_head_SNo.BorderWidth = 1;
                        Literal lit_undet_head_SNo = new Literal();
                        lit_undet_head_SNo.Text = "<b>S.No</b>";
                        tc_UnLst_doc_head_SNo.Attributes.Add("Class", "tr_det_head");
                        tc_UnLst_doc_head_SNo.Controls.Add(lit_undet_head_SNo);
                        tr_UnLst_doc_head.Cells.Add(tc_UnLst_doc_head_SNo);

                        TableCell tc_undet_head_Ses = new TableCell();
                        tc_undet_head_Ses.BorderStyle = BorderStyle.Solid;
                        tc_undet_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                        tc_undet_head_Ses.BorderWidth = 1;
                        Literal lit_undet_head_Ses = new Literal();
                        lit_undet_head_Ses.Text = "<b>Ses</b>";
                        tc_undet_head_Ses.Attributes.Add("Class", "tr_det_head");
                        tc_undet_head_Ses.Controls.Add(lit_undet_head_Ses);
                        tr_UnLst_doc_head.Cells.Add(tc_undet_head_Ses);

                        TableCell tc_det_head_doc = new TableCell();
                        tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                        tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_doc.BorderWidth = 1;
                        Literal lit_det_head_doc = new Literal();
                        lit_det_head_doc.Text = "<b>UnListed  Doctor Name</b>";
                        tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_doc.Controls.Add(lit_det_head_doc);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_doc);

                        TableCell tc_det_head_time = new TableCell();
                        tc_det_head_time.BorderStyle = BorderStyle.Solid;
                        tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_time.BorderWidth = 1;
                        Literal lit_det_head_time = new Literal();
                        lit_det_head_time.Text = "<b>Time</b>";
                        tc_det_head_time.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_time.Controls.Add(lit_det_head_time);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_time);

                        TableCell tc_det_head_LastUpdated = new TableCell();
                        tc_det_head_LastUpdated.BorderStyle = BorderStyle.Solid;
                        tc_det_head_LastUpdated.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_LastUpdated.BorderWidth = 1;
                        Literal lit_det_head_LastUpdated = new Literal();
                        lit_det_head_LastUpdated.Text = "<b>Last Updated</b>";
                        tc_det_head_LastUpdated.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_LastUpdated.Controls.Add(lit_det_head_LastUpdated);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_LastUpdated);

                        TableCell tc_det_head_ww = new TableCell();
                        tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                        tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_ww.BorderWidth = 1;
                        Literal lit_det_head_ww = new Literal();
                        lit_det_head_ww.Text = "<b>Worked With</b>";
                        tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_ww.Controls.Add(lit_det_head_ww);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_ww);

                        TableCell tc_det_head_visit = new TableCell();
                        tc_det_head_visit.BorderStyle = BorderStyle.Solid;
                        tc_det_head_visit.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_visit.BorderWidth = 1;
                        Literal lit_det_head_visit = new Literal();
                        lit_det_head_visit.Text = "<b>Latest Visit</b>";
                        tc_det_head_visit.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_visit.Controls.Add(lit_det_head_visit);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_visit);

                        TableCell tc_det_head_catg = new TableCell();
                        tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                        tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_catg.BorderWidth = 1;
                        Literal lit_det_head_catg = new Literal();
                        lit_det_head_catg.Text = "<b>Category</b>";
                        tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_catg.Controls.Add(lit_det_head_catg);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_catg);

                        TableCell tc_det_head_spec = new TableCell();
                        tc_det_head_spec.BorderStyle = BorderStyle.Solid;
                        tc_det_head_spec.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_spec.BorderWidth = 1;
                        Literal lit_det_head_spec = new Literal();
                        lit_det_head_spec.Text = "<b>Speciality</b>";
                        tc_det_head_spec.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_spec.Controls.Add(lit_det_head_spec);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_spec);

                        TableCell tc_det_head_SDP_Plan = new TableCell();
                        tc_det_head_SDP_Plan.BorderStyle = BorderStyle.Solid;
                        tc_det_head_SDP_Plan.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_SDP_Plan.BorderWidth = 1;
                        Literal lit_det_head_SDP_Plan = new Literal();
                        lit_det_head_SDP_Plan.Text = "<b>" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "</b>";
                        tc_det_head_SDP_Plan.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_SDP_Plan.Controls.Add(lit_det_head_SDP_Plan);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_SDP_Plan);

                        TableCell tc_det_dr_Act_Place_Worked = new TableCell();
                        Literal lit_det_dr_Act_Place_Worked = new Literal();
                        lit_det_dr_Act_Place_Worked.Text = "<b>Actual_Place_of_Worked</b>";
                        tc_det_dr_Act_Place_Worked.Attributes.Add("Class", "tr_det_head");
                        tc_det_dr_Act_Place_Worked.BorderStyle = BorderStyle.Solid;
                        tc_det_dr_Act_Place_Worked.BorderWidth = 1;
                        tc_det_dr_Act_Place_Worked.Controls.Add(lit_det_dr_Act_Place_Worked);
                        tr_UnLst_doc_head.Cells.Add(tc_det_dr_Act_Place_Worked);

                        //TableCell tc_det_dr_CallFeedBack = new TableCell();
                        //Literal lit_det_dr_CallFeedBack = new Literal();
                        //lit_det_dr_CallFeedBack.Text = "<b>Call_Feedback</b>";
                        //tc_det_dr_CallFeedBack.Attributes.Add("Class", "tr_det_head");
                        //tc_det_dr_CallFeedBack.BorderStyle = BorderStyle.Solid;
                        //tc_det_dr_CallFeedBack.BorderWidth = 1;
                        //tc_det_dr_CallFeedBack.Controls.Add(lit_det_dr_CallFeedBack);
                        //tr_UnLst_doc_head.Cells.Add(tc_det_dr_CallFeedBack);

                        TableCell tc_det_head_prod = new TableCell();
                        tc_det_head_prod.BorderStyle = BorderStyle.Solid;
                        tc_det_head_prod.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_prod.BorderWidth = 1;
                        Literal lit_det_head_prod = new Literal();
                        lit_det_head_prod.Text = "<b>Product Sampled</b>";
                        tc_det_head_prod.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_prod.Controls.Add(lit_det_head_prod);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_prod);

                        TableCell tc_det_head_gift = new TableCell();
                        tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                        tc_det_head_gift.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_gift.BorderWidth = 1;
                        Literal lit_det_head_gift = new Literal();
                        lit_det_head_gift.Text = "<b>Gift</b>";
                        tc_det_head_gift.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_gift.Controls.Add(lit_det_head_gift);
                        tr_UnLst_doc_head.Cells.Add(tc_det_head_gift);

                        tblUnLstDoc.Rows.Add(tr_UnLst_doc_head);

                        iCount = 0;
                        foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                        {
                            TableRow tr_det_sno = new TableRow();
                            TableCell tc_det_SNo = new TableCell();
                            iCount += 1;
                            Literal lit_det_SNo = new Literal();
                            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                            tc_det_SNo.BorderStyle = BorderStyle.Solid;
                            tc_det_SNo.BorderWidth = 1;
                            tc_det_SNo.Controls.Add(lit_det_SNo);
                            tr_det_sno.Cells.Add(tc_det_SNo);

                            TableCell tc_det_Ses = new TableCell();
                            Literal lit_det_Ses = new Literal();
                            lit_det_Ses.Text = "&nbsp;&nbsp;" + drdoctor["Session"].ToString();
                            tc_det_Ses.BorderStyle = BorderStyle.Solid;
                            tc_det_Ses.BorderWidth = 1;
                            tc_det_Ses.Controls.Add(lit_det_Ses);
                            tr_det_sno.Cells.Add(tc_det_Ses);

                            TableCell tc_det_dr_name = new TableCell();
                            Literal lit_det_dr_name = new Literal();
                            lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["UnListedDr_Name"].ToString();
                            tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_name.BorderWidth = 1;
                            tc_det_dr_name.Controls.Add(lit_det_dr_name);
                            tr_det_sno.Cells.Add(tc_det_dr_name);

                            TableCell tc_det_time = new TableCell();
                            Literal lit_det_time = new Literal();
                            lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor["Time"].ToString();
                            tc_det_time.BorderStyle = BorderStyle.Solid;
                            tc_det_time.BorderWidth = 1;
                            tc_det_time.Controls.Add(lit_det_time);
                            tr_det_sno.Cells.Add(tc_det_time);

                            TableCell tc_det_LastUpdate = new TableCell();
                            Literal lit_det_LastUpdate = new Literal();
                            lit_det_LastUpdate.Text = "&nbsp;&nbsp;" + drdoctor["ModTime"].ToString();
                            tc_det_LastUpdate.BorderStyle = BorderStyle.Solid;
                            tc_det_LastUpdate.BorderWidth = 1;
                            tc_det_LastUpdate.Controls.Add(lit_det_LastUpdate);
                            tr_det_sno.Cells.Add(tc_det_LastUpdate);

                            TableCell tc_det_work = new TableCell();
                            Literal lit_det_work = new Literal();
                            lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                            tc_det_work.BorderStyle = BorderStyle.Solid;
                            tc_det_work.BorderWidth = 1;
                            tc_det_work.Controls.Add(lit_det_work);
                            tr_det_sno.Cells.Add(tc_det_work);

                            TableCell tc_det_lvisit = new TableCell();
                            Literal lit_det_lvisit = new Literal();
                            lit_det_lvisit.Text = ""; // drdoctor["lvisit"].ToString();
                            tc_det_lvisit.BorderStyle = BorderStyle.Solid;
                            tc_det_lvisit.BorderWidth = 1;
                            tc_det_lvisit.Controls.Add(lit_det_lvisit);
                            tr_det_sno.Cells.Add(tc_det_lvisit);

                            TableCell tc_det_catg = new TableCell();
                            Literal lit_det_catg = new Literal();
                            lit_det_catg.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Cat_Name"].ToString();
                            tc_det_catg.BorderStyle = BorderStyle.Solid;
                            tc_det_catg.BorderWidth = 1;
                            tc_det_catg.Controls.Add(lit_det_catg);
                            tr_det_sno.Cells.Add(tc_det_catg);

                            TableCell tc_det_spec = new TableCell();
                            Literal lit_det_spec = new Literal();
                            lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Special_Name"].ToString();
                            tc_det_spec.BorderStyle = BorderStyle.Solid;
                            tc_det_spec.BorderWidth = 1;
                            tc_det_spec.Controls.Add(lit_det_spec);
                            tr_det_sno.Cells.Add(tc_det_spec);

                            TableCell tc_det_SDP_Plan = new TableCell();
                            Literal lit_det_SDP_Plan = new Literal();
                            lit_det_SDP_Plan.Text = "&nbsp;&nbsp;" + drdoctor["SDP_Name"].ToString();
                            tc_det_SDP_Plan.BorderStyle = BorderStyle.Solid;
                            tc_det_SDP_Plan.BorderWidth = 1;
                            tc_det_SDP_Plan.Controls.Add(lit_det_SDP_Plan);
                            tr_det_sno.Cells.Add(tc_det_SDP_Plan);

                            TableCell tc_det_Act_Place_Worked = new TableCell();
                            Literal lit_det_Act_Place_Worked = new Literal();
                            if (drdoctor["GeoAddrs"].ToString().Trim() != "NA")
                            {
                                lit_det_Act_Place_Worked.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                            }
                            else if (drdoctor["GeoAddrs"].ToString().Trim() == "NA")
                            {
                                lit_det_Act_Place_Worked.Text = "";
                            }
                            //lit_det_Act_Place_Worked.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                            tc_det_Act_Place_Worked.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_Act_Place_Worked.BorderStyle = BorderStyle.Solid;
                            tc_det_Act_Place_Worked.BorderWidth = 1;
                            tc_det_Act_Place_Worked.Width = 250;
                            tc_det_Act_Place_Worked.Controls.Add(lit_det_Act_Place_Worked);
                            tr_det_sno.Cells.Add(tc_det_Act_Place_Worked);

                            //TableCell tc_det_CallFeedBack = new TableCell();
                            //Literal lit_det_CallFeedBack = new Literal();
                            //lit_det_CallFeedBack.Text = "&nbsp;&nbsp;" + drdoctor["Rx"].ToString();
                            //tc_det_CallFeedBack.Attributes.Add("Class", "tbldetail_Data");
                            //tc_det_CallFeedBack.BorderStyle = BorderStyle.Solid;
                            //tc_det_CallFeedBack.BorderWidth = 1;
                            //tc_det_CallFeedBack.Controls.Add(lit_det_CallFeedBack);
                            //tr_det_sno.Cells.Add(tc_det_CallFeedBack);

                            TableCell tc_det_prod = new TableCell();
                            Literal lit_det_prod = new Literal();
                            lit_det_prod.Text = drdoctor["Product_Detail"].ToString().Replace("~", "(").Trim();
                            lit_det_prod.Text = lit_det_prod.Text.Replace("$", ")").Trim();
                            lit_det_prod.Text = "&nbsp;&nbsp;" + lit_det_prod.Text.Replace("#", "  ").Trim();
                            tc_det_prod.BorderStyle = BorderStyle.Solid;
                            tc_det_prod.BorderWidth = 1;
                            tc_det_prod.Controls.Add(lit_det_prod);
                            tr_det_sno.Cells.Add(tc_det_prod);

                            TableCell tc_det_gift = new TableCell();
                            Literal lit_det_gift = new Literal();
                            lit_det_gift.Text = "&nbsp;&nbsp;" + drdoctor["Gift_Name"].ToString().Replace("~", " ").Trim();
                            tc_det_gift.BorderStyle = BorderStyle.Solid;
                            tc_det_gift.BorderWidth = 1;
                            tc_det_gift.Controls.Add(lit_det_gift);
                            tr_det_sno.Cells.Add(tc_det_gift);

                            tblUnLstDoc.Rows.Add(tr_det_sno);
                        }
                    }

                    //form1.Controls.Add(tblUnLstDoc);

                    tc_det_head_main8.Controls.Add(tblUnLstDoc);
                    tr_det_head_main7.Cells.Add(tc_det_head_main8);
                    tbldetail_main7.Rows.Add(tr_det_head_main7);

                    form1.Controls.Add(tbldetail_main7);


                    if (iCount > 0)
                    {
                        Table tbl_undoc_empty = new Table();
                        TableRow tr_undoc_empty = new TableRow();
                        TableCell tc_undoc_empty = new TableCell();
                        Literal lit_undoc_empty = new Literal();
                        lit_undoc_empty.Text = "<BR>";
                        tc_undoc_empty.Controls.Add(lit_undoc_empty);
                        tr_undoc_empty.Cells.Add(tc_undoc_empty);
                        tbl_undoc_empty.Rows.Add(tr_undoc_empty);
                        form1.Controls.Add(tbl_undoc_empty);
                    }

                    // 3- Stockist

                    //5-Hospitals

                    Table tbldetail_main11 = new Table();
                    tbldetail_main11.BorderStyle = BorderStyle.None;
                    tbldetail_main11.Width = 1100;
                    TableRow tr_det_head_main11 = new TableRow();
                    TableCell tc_det_head_main11 = new TableCell();
                    tr_det_head_main11.Width = 100;
                    Literal lit_det_main11 = new Literal();
                    lit_det_main11.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    tc_det_head_main11.Controls.Add(lit_det_main11);
                    tr_det_head_main11.Cells.Add(tc_det_head_main11);

                    TableCell tc_det_head_main12 = new TableCell();
                    tc_det_head_main12.Width = 1000;


                    Table tbldetailstk = new Table();
                    tbldetailstk.BorderStyle = BorderStyle.Solid;
                    tbldetailstk.BorderWidth = 1;
                    tbldetailstk.GridLines = GridLines.Both;
                    tbldetailstk.Width = 1000;
                    tbldetailstk.Style.Add("border-collapse", "collapse");
                    tbldetailstk.Style.Add("border", "solid 1px Black");

                    dsdoc = dc.get_dcr_stk_details(sf_code, drdoc["Activity_Date"].ToString(), 3); //3-Stockist
                    iCount = 0;
                    if (dsdoc.Tables[0].Rows.Count > 0)
                    {
                        TableRow tr_det_head = new TableRow();
                        TableCell tc_det_head_SNo = new TableCell();
                        tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_SNo.BorderWidth = 1;
                        Literal lit_det_head_SNo = new Literal();
                        lit_det_head_SNo.Text = "<b>S.No</b>";
                        tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                        tr_det_head.Cells.Add(tc_det_head_SNo);

                        TableCell tc_det_head_doc = new TableCell();
                        tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                        tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_doc.BorderWidth = 1;
                        Literal lit_det_head_doc = new Literal();
                        lit_det_head_doc.Text = "<b>Stockist Name</b>";
                        tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_doc.Controls.Add(lit_det_head_doc);
                        tr_det_head.Cells.Add(tc_det_head_doc);

                        TableCell tc_det_head_VistTime = new TableCell();
                        tc_det_head_VistTime.BorderStyle = BorderStyle.Solid;
                        tc_det_head_VistTime.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_VistTime.BorderWidth = 1;
                        Literal lit_det_head_VistTime = new Literal();
                        lit_det_head_VistTime.Text = "<b>Visit Time</b>";
                        tc_det_head_VistTime.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_VistTime.Controls.Add(lit_det_head_VistTime);
                        tr_det_head.Cells.Add(tc_det_head_VistTime);

                        TableCell tc_det_head_LastUpdate = new TableCell();
                        tc_det_head_LastUpdate.BorderStyle = BorderStyle.Solid;
                        tc_det_head_LastUpdate.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_LastUpdate.BorderWidth = 1;
                        Literal lit_det_head_LastUpdate = new Literal();
                        lit_det_head_LastUpdate.Text = "<b>Last Updated</b>";
                        tc_det_head_LastUpdate.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_LastUpdate.Controls.Add(lit_det_head_LastUpdate);
                        tr_det_head.Cells.Add(tc_det_head_LastUpdate);

                        TableCell tc_det_head_ww = new TableCell();
                        tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                        tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_ww.BorderWidth = 1;
                        Literal lit_det_head_ww = new Literal();
                        lit_det_head_ww.Text = "<b>Worked With</b>";
                        tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_ww.Controls.Add(lit_det_head_ww);
                        tr_det_head.Cells.Add(tc_det_head_ww);

                        TableCell tc_det_head_ActualPlace = new TableCell();
                        tc_det_head_ActualPlace.BorderStyle = BorderStyle.Solid;
                        tc_det_head_ActualPlace.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_ActualPlace.BorderWidth = 1;
                        Literal lit_det_head_ActualPlace = new Literal();
                        lit_det_head_ActualPlace.Text = "<b>Actual Place</b>";
                        tc_det_head_ActualPlace.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_ActualPlace.Controls.Add(lit_det_head_ActualPlace);
                        tr_det_head.Cells.Add(tc_det_head_ActualPlace);

                        //TableCell tc_det_head_CallFeedBack = new TableCell();
                        //tc_det_head_CallFeedBack.BorderStyle = BorderStyle.Solid;
                        //tc_det_head_CallFeedBack.HorizontalAlign = HorizontalAlign.Center;
                        //tc_det_head_CallFeedBack.BorderWidth = 1;
                        //Literal lit_det_head_CallFeedBack = new Literal();
                        //lit_det_head_CallFeedBack.Text = "<b>Call Feedback</b>";
                        //tc_det_head_CallFeedBack.Attributes.Add("Class", "tr_det_head");
                        //tc_det_head_CallFeedBack.Controls.Add(lit_det_head_CallFeedBack);
                        //tr_det_head.Cells.Add(tc_det_head_CallFeedBack);

                        TableCell tc_det_head_catg = new TableCell();
                        tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                        tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_catg.BorderWidth = 1;
                        Literal lit_det_head_catg = new Literal();
                        lit_det_head_catg.Text = "<b>POB</b>";
                        tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_catg.Controls.Add(lit_det_head_catg);
                        tr_det_head.Cells.Add(tc_det_head_catg);


                        tbldetailstk.Rows.Add(tr_det_head);

                        iCount = 0;
                        foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                        {
                            TableRow tr_det_sno = new TableRow();
                            TableCell tc_det_SNo = new TableCell();
                            iCount += 1;
                            Literal lit_det_SNo = new Literal();
                            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                            tc_det_SNo.BorderStyle = BorderStyle.Solid;
                            tc_det_SNo.BorderWidth = 1;
                            tc_det_SNo.Controls.Add(lit_det_SNo);
                            tr_det_sno.Cells.Add(tc_det_SNo);


                            TableCell tc_det_dr_name = new TableCell();
                            Literal lit_det_dr_name = new Literal();
                            lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["Stockist_Name"].ToString();
                            tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_name.BorderWidth = 1;
                            tc_det_dr_name.Controls.Add(lit_det_dr_name);
                            tr_det_sno.Cells.Add(tc_det_dr_name);


                            TableCell tc_det_work = new TableCell();
                            Literal lit_det_work = new Literal();
                            lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                            tc_det_work.BorderStyle = BorderStyle.Solid;
                            tc_det_work.BorderWidth = 1;
                            tc_det_work.Controls.Add(lit_det_work);
                            tr_det_sno.Cells.Add(tc_det_work);

                            TableCell tc_det_dr_VisitTime = new TableCell();
                            Literal lit_det_dr_VisitTime = new Literal();
                            lit_det_dr_VisitTime.Text = "&nbsp;&nbsp;" + drdoctor["vstTime"].ToString();
                            tc_det_dr_VisitTime.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_VisitTime.BorderWidth = 1;
                            tc_det_dr_VisitTime.Controls.Add(lit_det_dr_VisitTime);
                            tr_det_sno.Cells.Add(tc_det_dr_VisitTime);

                            TableCell tc_det_dr_LastUpdate = new TableCell();
                            Literal lit_det_dr_LastUpdate = new Literal();
                            lit_det_dr_LastUpdate.Text = "&nbsp;&nbsp;" + drdoctor["ModTime"].ToString();
                            tc_det_dr_LastUpdate.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_LastUpdate.BorderWidth = 1;
                            tc_det_dr_LastUpdate.Controls.Add(lit_det_dr_LastUpdate);
                            tr_det_sno.Cells.Add(tc_det_dr_LastUpdate);

                            TableCell tc_det_dr_Place_Worked = new TableCell();
                            Literal lit_det_dr_Place_Worked = new Literal();
                            if (drdoctor["GeoAddrs"].ToString().Trim() != "NA")
                            {
                                lit_det_dr_Place_Worked.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                            }
                            else if (drdoctor["GeoAddrs"].ToString().Trim() == "NA")
                            {
                                lit_det_dr_Place_Worked.Text = "";
                            }
                            //lit_det_dr_Place_Worked.Text = "&nbsp;&nbsp;" + drdoctor["GeoAddrs"].ToString();
                            tc_det_dr_Place_Worked.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_Place_Worked.BorderWidth = 1;
                            tc_det_dr_Place_Worked.Width = 250;
                            tc_det_dr_Place_Worked.Controls.Add(lit_det_dr_Place_Worked);
                            tr_det_sno.Cells.Add(tc_det_dr_Place_Worked);

                            //TableCell tc_det_dr_Call_Feedback = new TableCell();
                            //Literal lit_det_dr_Call_Feedback = new Literal();
                            //lit_det_dr_Call_Feedback.Text = "&nbsp;&nbsp;" + drdoctor["Rx"].ToString();
                            //tc_det_dr_Call_Feedback.BorderStyle = BorderStyle.Solid;
                            //tc_det_dr_Call_Feedback.BorderWidth = 1;
                            //tc_det_dr_Call_Feedback.Controls.Add(lit_det_dr_Call_Feedback);
                            //tr_det_sno.Cells.Add(tc_det_dr_Call_Feedback);


                            TableCell tc_det_spec = new TableCell();
                            Literal lit_det_spec = new Literal();
                            lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                            tc_det_spec.BorderStyle = BorderStyle.Solid;
                            tc_det_spec.BorderWidth = 1;
                            tc_det_spec.Controls.Add(lit_det_spec);
                            tr_det_sno.Cells.Add(tc_det_spec);

                            tbldetailstk.Rows.Add(tr_det_sno);
                        }
                    }

                    //form1.Controls.Add(tbldetailhos);

                    tc_det_head_main12.Controls.Add(tbldetailstk);
                    tr_det_head_main11.Cells.Add(tc_det_head_main12);
                    tbldetail_main11.Rows.Add(tr_det_head_main11);

                    form1.Controls.Add(tbldetail_main11);


                    if (iCount > 0)
                    {
                        Table tbl_stk_empty = new Table();
                        TableRow tr_stk_empty = new TableRow();
                        TableCell tc_stk_empty = new TableCell();
                        Literal lit_stk_empty = new Literal();
                        lit_stk_empty.Text = "<BR>";
                        tc_stk_empty.Controls.Add(lit_stk_empty);
                        tr_stk_empty.Cells.Add(tc_stk_empty);
                        tbl_stk_empty.Rows.Add(tr_stk_empty);
                        form1.Controls.Add(tbl_stk_empty);
                    }

                    //5-Hospitals

                    Table tbldetail_main9 = new Table();
                    tbldetail_main9.BorderStyle = BorderStyle.None;
                    tbldetail_main9.Width = 1100;
                    TableRow tr_det_head_main9 = new TableRow();
                    TableCell tc_det_head_main9 = new TableCell();
                    tc_det_head_main9.Width = 100;
                    Literal lit_det_main9 = new Literal();
                    lit_det_main9.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    tc_det_head_main9.Controls.Add(lit_det_main9);
                    tr_det_head_main9.Cells.Add(tc_det_head_main9);

                    TableCell tc_det_head_main10 = new TableCell();
                    tc_det_head_main10.Width = 1000;


                    Table tbldetailhos = new Table();
                    tbldetailhos.BorderStyle = BorderStyle.Solid;
                    tbldetailhos.BorderWidth = 1;
                    tbldetailhos.GridLines = GridLines.Both;
                    tbldetailhos.Width = 1000;
                    tbldetailhos.Style.Add("border-collapse", "collapse");
                    tbldetailhos.Style.Add("border", "solid 1px Black");

                    dsdoc = dc.get_dcr_hos_details(sf_code, drdoc["Activity_Date"].ToString(), 5); //5-Hospital
                    iCount = 0;
                    if (dsdoc.Tables[0].Rows.Count > 0)
                    {
                        TableRow tr_det_head = new TableRow();
                        TableCell tc_det_head_SNo = new TableCell();
                        tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_SNo.BorderWidth = 1;
                        Literal lit_det_head_SNo = new Literal();
                        lit_det_head_SNo.Text = "<b>S.No</b>";
                        tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                        tr_det_head.Cells.Add(tc_det_head_SNo);

                        TableCell tc_det_head_doc = new TableCell();
                        tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                        tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_doc.BorderWidth = 1;
                        Literal lit_det_head_doc = new Literal();
                        lit_det_head_doc.Text = "<b>Hospital Name</b>";
                        tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_doc.Controls.Add(lit_det_head_doc);
                        tr_det_head.Cells.Add(tc_det_head_doc);

                        TableCell tc_det_head_ww = new TableCell();
                        tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                        tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_ww.BorderWidth = 1;
                        Literal lit_det_head_ww = new Literal();
                        lit_det_head_ww.Text = "<b>Worked With</b>";
                        tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_ww.Controls.Add(lit_det_head_ww);
                        tr_det_head.Cells.Add(tc_det_head_ww);

                        TableCell tc_det_head_catg = new TableCell();
                        tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                        tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_head_catg.BorderWidth = 1;
                        Literal lit_det_head_catg = new Literal();
                        lit_det_head_catg.Text = "<b>POB</b>";
                        tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_catg.Controls.Add(lit_det_head_catg);
                        tr_det_head.Cells.Add(tc_det_head_catg);


                        tbldetailhos.Rows.Add(tr_det_head);

                        iCount = 0;
                        foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                        {
                            TableRow tr_det_sno = new TableRow();
                            TableCell tc_det_SNo = new TableCell();
                            iCount += 1;
                            Literal lit_det_SNo = new Literal();
                            lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                            tc_det_SNo.BorderStyle = BorderStyle.Solid;
                            tc_det_SNo.BorderWidth = 1;
                            tc_det_SNo.Controls.Add(lit_det_SNo);
                            tr_det_sno.Cells.Add(tc_det_SNo);


                            TableCell tc_det_dr_name = new TableCell();
                            Literal lit_det_dr_name = new Literal();
                            lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["Hospital_Name"].ToString();
                            tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                            tc_det_dr_name.BorderWidth = 1;
                            tc_det_dr_name.Controls.Add(lit_det_dr_name);
                            tr_det_sno.Cells.Add(tc_det_dr_name);


                            TableCell tc_det_work = new TableCell();
                            Literal lit_det_work = new Literal();
                            lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                            tc_det_work.BorderStyle = BorderStyle.Solid;
                            tc_det_work.BorderWidth = 1;
                            tc_det_work.Controls.Add(lit_det_work);
                            tr_det_sno.Cells.Add(tc_det_work);


                            TableCell tc_det_spec = new TableCell();
                            Literal lit_det_spec = new Literal();
                            lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                            tc_det_spec.BorderStyle = BorderStyle.Solid;
                            tc_det_spec.BorderWidth = 1;
                            tc_det_spec.Controls.Add(lit_det_spec);
                            tr_det_sno.Cells.Add(tc_det_spec);

                            tbldetailhos.Rows.Add(tr_det_sno);
                        }
                    }

                    //form1.Controls.Add(tbldetailhos);

                    tc_det_head_main10.Controls.Add(tbldetailhos);
                    tr_det_head_main9.Cells.Add(tc_det_head_main10);
                    tbldetail_main9.Rows.Add(tr_det_head_main9);

                    form1.Controls.Add(tbldetail_main9);






                    if (iCount > 0)
                    {
                        Table tbl_hosp_empty = new Table();
                        TableRow tr_hosp_empty = new TableRow();
                        TableCell tc_hosp_empty = new TableCell();
                        Literal lit_hosp_empty = new Literal();
                        lit_hosp_empty.Text = "<BR>";
                        tc_hosp_empty.Controls.Add(lit_hosp_empty);
                        tr_hosp_empty.Cells.Add(tc_hosp_empty);
                        tbl_hosp_empty.Rows.Add(tr_hosp_empty);
                        form1.Controls.Add(tbl_hosp_empty);
                    }

                    Table tbl_line = new Table();
                    tbl_line.BorderStyle = BorderStyle.None;
                    tbl_line.Width = 1000;
                    tbl_line.Style.Add("border-collapse", "collapse");
                    tbl_line.Style.Add("border-top", "none");
                    tbl_line.Style.Add("border-right", "none");
                    tbl_line.Style.Add("margin-left", "100px");
                    tbl_line.Style.Add("border-bottom ", "solid 1px Black");

                    TableRow tr_line = new TableRow();

                    TableCell tc_line0 = new TableCell();
                    tc_line0.Width = 100;
                    Literal lit_line0 = new Literal();
                    lit_line0.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    tc_line0.Controls.Add(lit_line0);
                    tr_line.Cells.Add(tc_line0);

                    TableCell tc_line = new TableCell();
                    tc_line.Width = 1000;
                    Literal lit_line = new Literal();
                    // lit_line.Text = "<u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>";
                    tc_line.Controls.Add(lit_line);
                    tr_line.Cells.Add(tc_line);
                    tbl_line.Rows.Add(tr_line);
                    form1.Controls.Add(tbl_line);

                }
            }

        }
        else
        {
            //lblHead.Visible = true;
            //lblHead.Style.Add("margin-top", "80px");
            //lblHead.Text = "No Record Found";

            pnlbutton.Visible = true;

            Table tbldetail_mainHoliday = new Table();
            tbldetail_mainHoliday.BorderStyle = BorderStyle.None;
            tbldetail_mainHoliday.Width = 1100;
            TableRow tr_det_head_mainHoliday = new TableRow();
            TableCell tc_det_head_mainHolday = new TableCell();
            tc_det_head_mainHolday.Width = 100;
            Literal lit_det_mainHoliday = new Literal();
            lit_det_mainHoliday.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            tbldetail_mainHoliday.Style.Add("margin-top", "110px");
            tc_det_head_mainHolday.Controls.Add(lit_det_mainHoliday);
            tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHolday);

            TableCell tc_det_head_mainHoliday = new TableCell();
            tc_det_head_mainHoliday.Width = 800;

            Table tbldetailHoliday = new Table();
            tbldetailHoliday.BorderStyle = BorderStyle.Solid;
            tbldetailHoliday.BorderWidth = 1;
            tbldetailHoliday.GridLines = GridLines.Both;
            tbldetailHoliday.Width = 1000;
            tbldetailHoliday.Style.Add("border-collapse", "collapse");
            tbldetailHoliday.Style.Add("border", "solid 1px Black");

            TableRow tr_det_sno = new TableRow();
            TableCell tc_det_SNo = new TableCell();
            iCount += 1;
            Literal lit_det_SNo = new Literal();
            lit_det_SNo.Text = "No Record Found";
            tc_det_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_SNo.Attributes.Add("Class", "NoRecord");

            tc_det_SNo.HorizontalAlign = HorizontalAlign.Center;
            tc_det_SNo.BorderWidth = 1;
            tc_det_SNo.BorderStyle = BorderStyle.None;
            tc_det_SNo.Controls.Add(lit_det_SNo);
            tr_det_sno.Cells.Add(tc_det_SNo);

            tbldetailHoliday.Rows.Add(tr_det_sno);

            tc_det_head_mainHoliday.Controls.Add(tbldetailHoliday);
            tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHoliday);
            tbldetail_mainHoliday.Rows.Add(tr_det_head_mainHoliday);

            form1.Controls.Add(tbldetail_mainHoliday);
        }


    }

    private void CreateDynamicDCRDoctors(int imonth, int iyear, string sf_code)
    {
        DataSet dsget_dcr_dts = new DataSet();
        DataSet dsget_dcr_che = new DataSet();
        DataSet dsget_dcr_stk = new DataSet();
        DataSet dsget_dcr_hos = new DataSet();
        DataSet dsdoc_Pending = new DataSet();
        lblHead.Visible = false;

        DCR dc = new DCR();
        //dsDCR = dc.get_dcr_date(sf_code, imonth, iyear);
        //dsDCR = dc.get_dcr_DCRPendingdate(sf_code, imonth, iyear);
        if (sf_code.Contains("MR"))
        {
            dsDCR = dc.get_dcr_DCRPendingdate_MR(sf_code, imonth, iyear);
        }
        else
        {
            dsDCR = dc.get_dcr_DCRPendingdate_MGR(sf_code, imonth, iyear);
        }
        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            DCR dcsf = new DCR();
            dssf = dcsf.getSfName_HQ(sf_code);

            if (dssf.Tables[0].Rows.Count > 0)
            {
                Sf_Name = dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                Sf_HQ = dssf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            }

            foreach (DataRow drdoc in dsDCR.Tables[0].Rows)
            {

                Table tbldetail_main3 = new Table();
                tbldetail_main3.BorderStyle = BorderStyle.None;
                tbldetail_main3.Width = 1100;
                TableRow tr_det_head_main3 = new TableRow();
                TableCell tc_det_head_main3 = new TableCell();
                tc_det_head_main3.Width = 100;
                Literal lit_det_main3 = new Literal();
                lit_det_main3.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_main3.Controls.Add(lit_det_main3);
                tr_det_head_main3.Cells.Add(tc_det_head_main3);

                TableCell tc_det_head_main4 = new TableCell();
                tc_det_head_main4.Width = 1000;


                Table tbl = new Table();
                tbl.Width = 1000;
                //tbl.Style.Add("Align", "Center");
                TableRow tr_day = new TableRow();
                TableCell tc_day = new TableCell();
                tc_day.BorderStyle = BorderStyle.None;
                tc_day.ColumnSpan = 2;
                tc_day.HorizontalAlign = HorizontalAlign.Center;
                tc_day.Style.Add("font-name", "verdana;");
                Literal lit_day = new Literal();
                lit_day.Text = "<u><b>Daily Call Report - " + "<span style='color:Red'>" + drdoc["Activity_Date"].ToString() + "</span>" + "</b></u>";
                tc_day.Controls.Add(lit_day);
                tr_day.Cells.Add(tc_day);
                tbl.Rows.Add(tr_day);

                TableRow tr_ff = new TableRow();
                TableCell tc_ff_name = new TableCell();
                tc_ff_name.BorderStyle = BorderStyle.None;
                tc_ff_name.Width = 500;
                Literal lit_ff_name = new Literal();
                lit_ff_name.Text = "<b>Field Force Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_Name.ToString();
                tc_ff_name.Controls.Add(lit_ff_name);
                tr_ff.Cells.Add(tc_ff_name);

                TableCell tc_HQ = new TableCell();
                tc_HQ.BorderStyle = BorderStyle.None;
                tc_HQ.Width = 500;
                tc_HQ.HorizontalAlign = HorizontalAlign.Left;
                Literal lit_HQ = new Literal();
                lit_HQ.Text = "<span style='margin-left:200px'><b>Head Quarters </b></span>" + "<span style='margin-left:28px'></span>:" + "<span style='margin-left:22px'>" + Sf_HQ.ToString() + "</span>";
                tc_HQ.Controls.Add(lit_HQ);
                tr_ff.Cells.Add(tc_HQ);

                tbl.Rows.Add(tr_ff);

                TableRow tr_dcr = new TableRow();
                TableCell tc_dcr_submit = new TableCell();
                tc_dcr_submit.BorderStyle = BorderStyle.None;
                tc_dcr_submit.Width = 500;
                Literal lit_dcr_submit = new Literal();
                lit_dcr_submit.Text = "<b>DCR Submitted on</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoc["Submission_Date"].ToString();
                tc_dcr_submit.Controls.Add(lit_dcr_submit);
                tr_dcr.Cells.Add(tc_dcr_submit);

                TableCell tc_Terr = new TableCell();
                tc_Terr.BorderStyle = BorderStyle.None;
                tc_Terr.HorizontalAlign = HorizontalAlign.Left;
                tc_Terr.Width = 500;
                Literal lit_Terr = new Literal();
                // lit_Terr.Text = "<b>Territory Worked</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + drdoc["Plan_Name"].ToString(); ;
                Territory terr = new Territory();
                dsTerritory = terr.getWorkAreaName(div_code);

                //lit_Terr.Text = "<span style='margin-left:280px'><b>" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + " Worked </b></span>" + "<span style='margin-left:15px'></span>:" + "<span style='margin-left:21px;text-align:right'>" + drdoc["Plan_Name"].ToString() + "</span>";
                lit_Terr.Text = "<span style='margin-left:200px'><b> Territory Worked </b></span>" + "<span style='margin-left:15px'></span>:" + "<span style='margin-left:21px;text-align:right'>" + drdoc["Plan_Name"].ToString() + "</span>";

                tc_Terr.Controls.Add(lit_Terr);
                tr_dcr.Cells.Add(tc_Terr);

                tbl.Rows.Add(tr_dcr);

                tc_det_head_main4.Controls.Add(tbl);
                tr_det_head_main3.Cells.Add(tc_det_head_main4);
                tbldetail_main3.Rows.Add(tr_det_head_main3);

                dsdoc = dc.get_dcr_details(sf_code, drdoc["Activity_Date"].ToString(), 1);
                dsget_dcr_che = dc.get_dcr_che_details(sf_code, drdoc["Activity_Date"].ToString(), 2);
                dsget_dcr_stk = dc.get_dcr_stk_details(sf_code, drdoc["Activity_Date"].ToString(), 3);
                dsget_dcr_hos = dc.get_dcr_hos_details(sf_code, drdoc["Activity_Date"].ToString(), 5);
                dsdoc_Pending = dc.get_Pending_dcrLstDOC_details(sf_code, drdoc["Activity_Date"].ToString(), 1);

                if (dsdoc.Tables[0].Rows.Count > 0 || dsget_dcr_che.Tables[0].Rows.Count > 0 || dsget_dcr_stk.Tables[0].Rows.Count > 0 || dsget_dcr_hos.Tables[0].Rows.Count > 0 || dsdoc_Pending.Tables[0].Rows.Count > 0)
                {
                    form1.Controls.Add(tbldetail_main3);

                    Table tbl_head_empty = new Table();
                    TableRow tr_head_empty = new TableRow();
                    TableCell tc_head_empty = new TableCell();
                    Literal lit_head_empty = new Literal();
                    lit_head_empty.Text = "<BR>";
                    tc_head_empty.Controls.Add(lit_head_empty);
                    tr_head_empty.Cells.Add(tc_head_empty);
                    tbl_head_empty.Rows.Add(tr_head_empty);
                    form1.Controls.Add(tbl_head_empty);
                }



                Table tbldetail_main = new Table();
                tbldetail_main.BorderStyle = BorderStyle.None;
                tbldetail_main.Width = 1100;
                TableRow tr_det_head_main = new TableRow();
                TableCell tc_det_head_main = new TableCell();
                tc_det_head_main.Width = 100;
                Literal lit_det_main = new Literal();
                lit_det_main.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_main.Controls.Add(lit_det_main);
                tr_det_head_main.Cells.Add(tc_det_head_main);

                TableCell tc_det_head_main2 = new TableCell();
                tc_det_head_main2.Width = 1000;

                Table tbldetail = new Table();
                tbldetail.BorderStyle = BorderStyle.Solid;
                tbldetail.BorderWidth = 1;
                tbldetail.GridLines = GridLines.Both;
                tbldetail.Width = 1000;
                tbldetail.Style.Add("border-collapse", "collapse");
                tbldetail.Style.Add("border", "solid 1px Black");
                dsdoc = dc.get_dcr_details(sf_code, drdoc["Activity_Date"].ToString(), 1); //1-Listed Doctor
                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_det_head = new TableRow();
                    TableCell tc_det_head_SNo = new TableCell();
                    tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_head_SNo.BorderWidth = 1;
                    tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_SNo = new Literal();
                    lit_det_head_SNo.Text = "<b>S.No</b>";
                    tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                    tr_det_head.Cells.Add(tc_det_head_SNo);

                    TableCell tc_det_head_Ses = new TableCell();
                    tc_det_head_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_head_Ses.BorderWidth = 1;
                    tc_det_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_Ses = new Literal();
                    lit_det_head_Ses.Text = "<b>Ses</b>";
                    tc_det_head_Ses.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_Ses.Controls.Add(lit_det_head_Ses);
                    tr_det_head.Cells.Add(tc_det_head_Ses);

                    TableCell tc_det_head_doc = new TableCell();
                    tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                    tc_det_head_doc.BorderWidth = 1;
                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_doc = new Literal();
                    lit_det_head_doc.Text = "<b>Listed  Doctor Name</b>";
                    tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                    tr_det_head.Cells.Add(tc_det_head_doc);

                    TableCell tc_det_head_time = new TableCell();
                    tc_det_head_time.BorderStyle = BorderStyle.Solid;
                    tc_det_head_time.BorderWidth = 1;
                    tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_time = new Literal();
                    lit_det_head_time.Text = "<b>Time</b>";
                    tc_det_head_time.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_time.Controls.Add(lit_det_head_time);
                    tr_det_head.Cells.Add(tc_det_head_time);

                    TableCell tc_det_head_ww = new TableCell();
                    tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_head_ww.BorderWidth = 1;
                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_ww = new Literal();
                    lit_det_head_ww.Text = "<b>Worked With</b>";
                    tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                    tr_det_head.Cells.Add(tc_det_head_ww);

                    TableCell tc_det_head_visit = new TableCell();
                    tc_det_head_visit.BorderStyle = BorderStyle.Solid;
                    tc_det_head_visit.BorderWidth = 1;
                    tc_det_head_visit.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_visit = new Literal();
                    lit_det_head_visit.Text = "<b>Latest Visit</b>";
                    tc_det_head_visit.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_visit.Controls.Add(lit_det_head_visit);
                    tr_det_head.Cells.Add(tc_det_head_visit);

                    TableCell tc_det_head_catg = new TableCell();
                    tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                    tc_det_head_catg.BorderWidth = 1;
                    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_catg = new Literal();
                    lit_det_head_catg.Text = "<b>Category</b>";
                    tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_catg.Controls.Add(lit_det_head_catg);
                    tr_det_head.Cells.Add(tc_det_head_catg);

                    TableCell tc_det_head_spec = new TableCell();
                    tc_det_head_spec.BorderStyle = BorderStyle.Solid;
                    tc_det_head_spec.BorderWidth = 1;
                    tc_det_head_spec.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_spec = new Literal();
                    lit_det_head_spec.Text = "<b>Speciality</b>";
                    tc_det_head_spec.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_spec.Controls.Add(lit_det_head_spec);
                    tr_det_head.Cells.Add(tc_det_head_spec);

                    TableCell tc_det_head_prod = new TableCell();
                    tc_det_head_prod.BorderStyle = BorderStyle.Solid;
                    tc_det_head_prod.BorderWidth = 1;
                    tc_det_head_prod.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_prod = new Literal();
                    lit_det_head_prod.Text = "<b>Product Sampled</b>";
                    tc_det_head_prod.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_prod.Controls.Add(lit_det_head_prod);
                    tr_det_head.Cells.Add(tc_det_head_prod);

                    TableCell tc_det_head_gift = new TableCell();
                    tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                    tc_det_head_gift.BorderWidth = 1;
                    tc_det_head_gift.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_gift = new Literal();
                    lit_det_head_gift.Text = "<b>Gift</b>";
                    tc_det_head_gift.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_gift.Controls.Add(lit_det_head_gift);
                    tr_det_head.Cells.Add(tc_det_head_gift);

                    tbldetail.Rows.Add(tr_det_head);

                    iCount = 0;
                    foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                    {
                        TableRow tr_det_sno = new TableRow();
                        TableCell tc_det_SNo = new TableCell();
                        iCount += 1;
                        Literal lit_det_SNo = new Literal();
                        lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                        tc_det_SNo.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_SNo.BorderWidth = 1;
                        tc_det_SNo.Controls.Add(lit_det_SNo);
                        tr_det_sno.Cells.Add(tc_det_SNo);

                        TableCell tc_det_Ses = new TableCell();
                        Literal lit_det_Ses = new Literal();
                        lit_det_Ses.Text = "&nbsp;&nbsp;" + drdoctor["Session"].ToString();
                        tc_det_Ses.BorderStyle = BorderStyle.Solid;
                        tc_det_Ses.BorderWidth = 1;
                        tc_det_Ses.Controls.Add(lit_det_Ses);
                        tr_det_sno.Cells.Add(tc_det_Ses);

                        TableCell tc_det_dr_name = new TableCell();
                        Literal lit_det_dr_name = new Literal();
                        lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["ListedDr_Name"].ToString();
                        tc_det_dr_name.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                        tc_det_dr_name.BorderWidth = 1;
                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                        tr_det_sno.Cells.Add(tc_det_dr_name);

                        TableCell tc_det_time = new TableCell();
                        Literal lit_det_time = new Literal();
                        lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor["Time"].ToString();
                        tc_det_time.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_time.BorderStyle = BorderStyle.Solid;
                        tc_det_time.BorderWidth = 1;
                        tc_det_time.Controls.Add(lit_det_time);
                        tr_det_sno.Cells.Add(tc_det_time);

                        TableCell tc_det_work = new TableCell();
                        Literal lit_det_work = new Literal();
                        lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                        tc_det_work.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_work.BorderStyle = BorderStyle.Solid;
                        tc_det_work.BorderWidth = 1;
                        tc_det_work.Controls.Add(lit_det_work);
                        tr_det_sno.Cells.Add(tc_det_work);

                        TableCell tc_det_lvisit = new TableCell();
                        Literal lit_det_lvisit = new Literal();
                        lit_det_lvisit.Text = ""; // drdoctor["lvisit"].ToString();
                        tc_det_lvisit.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_lvisit.BorderStyle = BorderStyle.Solid;
                        tc_det_lvisit.BorderWidth = 1;
                        tc_det_lvisit.Controls.Add(lit_det_lvisit);
                        tr_det_sno.Cells.Add(tc_det_lvisit);

                        TableCell tc_det_catg = new TableCell();
                        Literal lit_det_catg = new Literal();
                        lit_det_catg.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Cat_Name"].ToString();
                        tc_det_catg.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_catg.BorderStyle = BorderStyle.Solid;
                        tc_det_catg.BorderWidth = 1;
                        tc_det_catg.Controls.Add(lit_det_catg);
                        tr_det_sno.Cells.Add(tc_det_catg);

                        TableCell tc_det_spec = new TableCell();
                        Literal lit_det_spec = new Literal();
                        lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Special_Name"].ToString();
                        tc_det_spec.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_spec.BorderStyle = BorderStyle.Solid;
                        tc_det_spec.BorderWidth = 1;
                        tc_det_spec.Controls.Add(lit_det_spec);
                        tr_det_sno.Cells.Add(tc_det_spec);

                        TableCell tc_det_prod = new TableCell();
                        Literal lit_det_prod = new Literal();
                        lit_det_prod.Text = drdoctor["Product_Detail"].ToString().Replace("~", "(").Trim();
                        lit_det_prod.Text = lit_det_prod.Text.Replace("$", ")").Trim();
                        lit_det_prod.Text = "&nbsp;&nbsp;" + lit_det_prod.Text.Replace("#", "  ").Trim();
                        tc_det_prod.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_prod.BorderStyle = BorderStyle.Solid;
                        tc_det_prod.BorderWidth = 1;
                        tc_det_prod.Controls.Add(lit_det_prod);
                        tr_det_sno.Cells.Add(tc_det_prod);

                        TableCell tc_det_gift = new TableCell();
                        Literal lit_det_gift = new Literal();
                        lit_det_gift.Text = "&nbsp;&nbsp;" + drdoctor["Gift_Name"].ToString().Replace("~", "").Trim();
                        tc_det_gift.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_gift.BorderStyle = BorderStyle.Solid;
                        tc_det_gift.BorderWidth = 1;
                        tc_det_gift.Controls.Add(lit_det_gift);
                        tr_det_sno.Cells.Add(tc_det_gift);

                        tbldetail.Rows.Add(tr_det_sno);


                        tc_det_head_main2.Controls.Add(tbldetail);
                        tr_det_head_main.Cells.Add(tc_det_head_main2);
                        tbldetail_main.Rows.Add(tr_det_head_main);

                        form1.Controls.Add(tbldetail_main);
                    }
                }

                //form1.Controls.Add(tbldetail);


                if (iCount > 0)
                {
                    Table tbl_doc_empty = new Table();
                    TableRow tr_doc_empty = new TableRow();
                    TableCell tc_doc_empty = new TableCell();
                    Literal lit_doc_empty = new Literal();
                    lit_doc_empty.Text = "<BR>";
                    tc_doc_empty.Controls.Add(lit_doc_empty);
                    tr_doc_empty.Cells.Add(tc_doc_empty);
                    tbl_doc_empty.Rows.Add(tr_doc_empty);
                    form1.Controls.Add(tbl_doc_empty);
                }

                //2-Chemists

                Table tbldetail_main5 = new Table();
                tbldetail_main5.BorderStyle = BorderStyle.None;
                tbldetail_main5.Width = 1100;
                TableRow tr_det_head_main5 = new TableRow();
                TableCell tc_det_head_main5 = new TableCell();
                tc_det_head_main5.Width = 100;
                Literal lit_det_main5 = new Literal();
                lit_det_main5.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_main5.Controls.Add(lit_det_main5);
                tr_det_head_main5.Cells.Add(tc_det_head_main5);

                TableCell tc_det_head_main6 = new TableCell();
                tc_det_head_main6.Width = 1000;


                Table tbldetailChe = new Table();
                tbldetailChe.BorderStyle = BorderStyle.Solid;
                tbldetailChe.BorderWidth = 1;
                tbldetailChe.GridLines = GridLines.Both;
                tbldetailChe.Width = 1000;
                tbldetailChe.Style.Add("border-collapse", "collapse");
                tbldetailChe.Style.Add("border", "solid 1px Black");

                dsdoc = dc.get_dcr_che_details(sf_code, drdoc["Activity_Date"].ToString(), 2); //2-Chemists
                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_det_head = new TableRow();
                    TableCell tc_det_head_SNo = new TableCell();
                    tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_SNo.BorderWidth = 1;
                    Literal lit_det_head_SNo = new Literal();
                    lit_det_head_SNo.Text = "<b>S.No</b>";
                    tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                    tr_det_head.Cells.Add(tc_det_head_SNo);

                    TableCell tc_det_head_doc = new TableCell();
                    tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_doc.BorderWidth = 1;
                    Literal lit_det_head_doc = new Literal();
                    lit_det_head_doc.Text = "<b>Chemists Name</b>";
                    tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                    tr_det_head.Cells.Add(tc_det_head_doc);

                    TableCell tc_det_head_ww = new TableCell();
                    tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_ww.BorderWidth = 1;
                    Literal lit_det_head_ww = new Literal();
                    lit_det_head_ww.Text = "<b>Worked With</b>";
                    tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                    tr_det_head.Cells.Add(tc_det_head_ww);

                    TableCell tc_det_head_catg = new TableCell();
                    tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_catg.BorderWidth = 1;
                    Literal lit_det_head_catg = new Literal();
                    lit_det_head_catg.Text = "<b>POB</b>";
                    tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_catg.Controls.Add(lit_det_head_catg);
                    tr_det_head.Cells.Add(tc_det_head_catg);


                    tbldetailChe.Rows.Add(tr_det_head);

                    iCount = 0;
                    foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                    {
                        TableRow tr_det_sno = new TableRow();
                        TableCell tc_det_SNo = new TableCell();
                        iCount += 1;
                        Literal lit_det_SNo = new Literal();
                        lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                        tc_det_SNo.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_SNo.BorderWidth = 1;
                        tc_det_SNo.Controls.Add(lit_det_SNo);
                        tr_det_sno.Cells.Add(tc_det_SNo);

                        TableCell tc_det_dr_name = new TableCell();
                        Literal lit_det_dr_name = new Literal();
                        lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["Chemists_Name"].ToString();
                        tc_det_dr_name.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                        tc_det_dr_name.BorderWidth = 1;
                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                        tr_det_sno.Cells.Add(tc_det_dr_name);

                        TableCell tc_det_work = new TableCell();
                        Literal lit_det_work = new Literal();
                        lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                        tc_det_work.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_work.BorderStyle = BorderStyle.Solid;
                        tc_det_work.BorderWidth = 1;
                        tc_det_work.Controls.Add(lit_det_work);
                        tr_det_sno.Cells.Add(tc_det_work);

                        TableCell tc_det_spec = new TableCell();
                        Literal lit_det_spec = new Literal();
                        lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                        tc_det_spec.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_spec.BorderStyle = BorderStyle.Solid;
                        tc_det_spec.BorderWidth = 1;
                        tc_det_spec.Controls.Add(lit_det_spec);
                        tr_det_sno.Cells.Add(tc_det_spec);

                        tbldetailChe.Rows.Add(tr_det_sno);

                        tc_det_head_main6.Controls.Add(tbldetailChe);
                        tr_det_head_main5.Cells.Add(tc_det_head_main6);
                        tbldetail_main5.Rows.Add(tr_det_head_main5);

                        form1.Controls.Add(tbldetail_main5);
                    }
                }

                //form1.Controls.Add(tbldetailChe);



                if (iCount > 0)
                {
                    Table tbl_chem_empty = new Table();
                    TableRow tr_chem_empty = new TableRow();
                    TableCell tc_chem_empty = new TableCell();
                    Literal lit_chem_empty = new Literal();
                    lit_chem_empty.Text = "<BR>";
                    tc_chem_empty.Controls.Add(lit_chem_empty);
                    tr_chem_empty.Cells.Add(tc_chem_empty);
                    tbl_chem_empty.Rows.Add(tr_chem_empty);
                    form1.Controls.Add(tbl_chem_empty);
                }

                //4-UnListed Doctor

                Table tbldetail_main7 = new Table();
                tbldetail_main7.BorderStyle = BorderStyle.None;
                tbldetail_main7.Width = 1100;
                TableRow tr_det_head_main7 = new TableRow();
                TableCell tc_det_head_main7 = new TableCell();
                tc_det_head_main7.Width = 100;
                Literal lit_det_main7 = new Literal();
                lit_det_main7.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_main7.Controls.Add(lit_det_main7);
                tr_det_head_main7.Cells.Add(tc_det_head_main7);

                TableCell tc_det_head_main8 = new TableCell();
                tc_det_head_main8.Width = 1000;

                Table tblUnLstDoc = new Table();
                tblUnLstDoc.BorderStyle = BorderStyle.Solid;
                tblUnLstDoc.BorderWidth = 1;
                tblUnLstDoc.GridLines = GridLines.Both;
                tblUnLstDoc.Width = 1000;
                dsdoc = dc.get_unlst_doc_details(sf_code, drdoc["Activity_Date"].ToString(), 1); //1-Listed Doctor
                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_UnLst_doc_head = new TableRow();
                    TableCell tc_UnLst_doc_head_SNo = new TableCell();
                    tc_UnLst_doc_head_SNo.BorderStyle = BorderStyle.Solid;
                    tc_UnLst_doc_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                    tc_UnLst_doc_head_SNo.BorderWidth = 1;
                    Literal lit_undet_head_SNo = new Literal();
                    lit_undet_head_SNo.Text = "<b>S.No</b>";
                    tc_UnLst_doc_head_SNo.Attributes.Add("Class", "tr_det_head");
                    tc_UnLst_doc_head_SNo.Controls.Add(lit_undet_head_SNo);
                    tr_UnLst_doc_head.Cells.Add(tc_UnLst_doc_head_SNo);

                    TableCell tc_undet_head_Ses = new TableCell();
                    tc_undet_head_Ses.BorderStyle = BorderStyle.Solid;
                    tc_undet_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                    tc_undet_head_Ses.BorderWidth = 1;
                    Literal lit_undet_head_Ses = new Literal();
                    lit_undet_head_Ses.Text = "<b>Ses</b>";
                    tc_undet_head_Ses.Attributes.Add("Class", "tr_det_head");
                    tc_undet_head_Ses.Controls.Add(lit_undet_head_Ses);
                    tr_UnLst_doc_head.Cells.Add(tc_undet_head_Ses);

                    TableCell tc_det_head_doc = new TableCell();
                    tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_doc.BorderWidth = 1;
                    Literal lit_det_head_doc = new Literal();
                    lit_det_head_doc.Text = "<b>UnListed  Doctor Name</b>";
                    tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                    tr_UnLst_doc_head.Cells.Add(tc_det_head_doc);

                    TableCell tc_det_head_time = new TableCell();
                    tc_det_head_time.BorderStyle = BorderStyle.Solid;
                    tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_time.BorderWidth = 1;
                    Literal lit_det_head_time = new Literal();
                    lit_det_head_time.Text = "<b>Time</b>";
                    tc_det_head_time.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_time.Controls.Add(lit_det_head_time);
                    tr_UnLst_doc_head.Cells.Add(tc_det_head_time);

                    TableCell tc_det_head_ww = new TableCell();
                    tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_ww.BorderWidth = 1;
                    Literal lit_det_head_ww = new Literal();
                    lit_det_head_ww.Text = "<b>Worked With</b>";
                    tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                    tr_UnLst_doc_head.Cells.Add(tc_det_head_ww);

                    TableCell tc_det_head_visit = new TableCell();
                    tc_det_head_visit.BorderStyle = BorderStyle.Solid;
                    tc_det_head_visit.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_visit.BorderWidth = 1;
                    Literal lit_det_head_visit = new Literal();
                    lit_det_head_visit.Text = "<b>Latest Visit</b>";
                    tc_det_head_visit.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_visit.Controls.Add(lit_det_head_visit);
                    tr_UnLst_doc_head.Cells.Add(tc_det_head_visit);

                    TableCell tc_det_head_catg = new TableCell();
                    tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_catg.BorderWidth = 1;
                    Literal lit_det_head_catg = new Literal();
                    lit_det_head_catg.Text = "<b>Category</b>";
                    tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_catg.Controls.Add(lit_det_head_catg);
                    tr_UnLst_doc_head.Cells.Add(tc_det_head_catg);

                    TableCell tc_det_head_spec = new TableCell();
                    tc_det_head_spec.BorderStyle = BorderStyle.Solid;
                    tc_det_head_spec.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_spec.BorderWidth = 1;
                    Literal lit_det_head_spec = new Literal();
                    lit_det_head_spec.Text = "<b>Speciality</b>";
                    tc_det_head_spec.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_spec.Controls.Add(lit_det_head_spec);
                    tr_UnLst_doc_head.Cells.Add(tc_det_head_spec);

                    TableCell tc_det_head_prod = new TableCell();
                    tc_det_head_prod.BorderStyle = BorderStyle.Solid;
                    tc_det_head_prod.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_prod.BorderWidth = 1;
                    Literal lit_det_head_prod = new Literal();
                    lit_det_head_prod.Text = "<b>Product Sampled</b>";
                    tc_det_head_prod.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_prod.Controls.Add(lit_det_head_prod);
                    tr_UnLst_doc_head.Cells.Add(tc_det_head_prod);

                    TableCell tc_det_head_gift = new TableCell();
                    tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                    tc_det_head_gift.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_gift.BorderWidth = 1;
                    Literal lit_det_head_gift = new Literal();
                    lit_det_head_gift.Text = "<b>Gift</b>";
                    tc_det_head_gift.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_gift.Controls.Add(lit_det_head_gift);
                    tr_UnLst_doc_head.Cells.Add(tc_det_head_gift);

                    tblUnLstDoc.Rows.Add(tr_UnLst_doc_head);

                    iCount = 0;
                    foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                    {
                        TableRow tr_det_sno = new TableRow();
                        TableCell tc_det_SNo = new TableCell();
                        iCount += 1;
                        Literal lit_det_SNo = new Literal();
                        lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                        tc_det_SNo.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_SNo.BorderWidth = 1;
                        tc_det_SNo.Controls.Add(lit_det_SNo);
                        tr_det_sno.Cells.Add(tc_det_SNo);

                        TableCell tc_det_Ses = new TableCell();
                        Literal lit_det_Ses = new Literal();
                        lit_det_Ses.Text = "&nbsp;&nbsp;" + drdoctor["Session"].ToString();
                        tc_det_Ses.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_Ses.BorderStyle = BorderStyle.Solid;
                        tc_det_Ses.BorderWidth = 1;
                        tc_det_Ses.Controls.Add(lit_det_Ses);
                        tr_det_sno.Cells.Add(tc_det_Ses);

                        TableCell tc_det_dr_name = new TableCell();
                        Literal lit_det_dr_name = new Literal();
                        lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["UnListedDr_Name"].ToString();
                        tc_det_dr_name.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                        tc_det_dr_name.BorderWidth = 1;
                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                        tr_det_sno.Cells.Add(tc_det_dr_name);

                        TableCell tc_det_time = new TableCell();
                        Literal lit_det_time = new Literal();
                        lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor["Time"].ToString();
                        tc_det_time.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_time.BorderStyle = BorderStyle.Solid;
                        tc_det_time.BorderWidth = 1;
                        tc_det_time.Controls.Add(lit_det_time);
                        tr_det_sno.Cells.Add(tc_det_time);

                        TableCell tc_det_work = new TableCell();
                        Literal lit_det_work = new Literal();
                        lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                        tc_det_work.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_work.BorderStyle = BorderStyle.Solid;
                        tc_det_work.BorderWidth = 1;
                        tc_det_work.Controls.Add(lit_det_work);
                        tr_det_sno.Cells.Add(tc_det_work);

                        TableCell tc_det_lvisit = new TableCell();
                        Literal lit_det_lvisit = new Literal();
                        lit_det_lvisit.Text = ""; // drdoctor["lvisit"].ToString();
                        tc_det_lvisit.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_lvisit.BorderStyle = BorderStyle.Solid;
                        tc_det_lvisit.BorderWidth = 1;
                        tc_det_lvisit.Controls.Add(lit_det_lvisit);
                        tr_det_sno.Cells.Add(tc_det_lvisit);

                        TableCell tc_det_catg = new TableCell();
                        Literal lit_det_catg = new Literal();
                        lit_det_catg.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Cat_Name"].ToString();
                        tc_det_catg.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_catg.BorderStyle = BorderStyle.Solid;
                        tc_det_catg.BorderWidth = 1;
                        tc_det_catg.Controls.Add(lit_det_catg);
                        tr_det_sno.Cells.Add(tc_det_catg);

                        TableCell tc_det_spec = new TableCell();
                        Literal lit_det_spec = new Literal();
                        lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Special_Name"].ToString();
                        tc_det_spec.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_spec.BorderStyle = BorderStyle.Solid;
                        tc_det_spec.BorderWidth = 1;
                        tc_det_spec.Controls.Add(lit_det_spec);
                        tr_det_sno.Cells.Add(tc_det_spec);

                        TableCell tc_det_prod = new TableCell();
                        Literal lit_det_prod = new Literal();
                        lit_det_prod.Text = drdoctor["Product_Detail"].ToString().Replace("~", "(").Trim();
                        tc_det_prod.Attributes.Add("Class", "tbldetail_Data");
                        lit_det_prod.Text = lit_det_prod.Text.Replace("$", ")").Trim();
                        lit_det_prod.Text = "&nbsp;&nbsp;" + lit_det_prod.Text.Replace("#", "  ").Trim();
                        tc_det_prod.BorderStyle = BorderStyle.Solid;
                        tc_det_prod.BorderWidth = 1;
                        tc_det_prod.Controls.Add(lit_det_prod);
                        tr_det_sno.Cells.Add(tc_det_prod);

                        TableCell tc_det_gift = new TableCell();
                        Literal lit_det_gift = new Literal();
                        lit_det_gift.Text = "&nbsp;&nbsp;" + drdoctor["Gift_Name"].ToString().Replace("~", " ").Trim();
                        tc_det_gift.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_gift.BorderStyle = BorderStyle.Solid;
                        tc_det_gift.BorderWidth = 1;
                        tc_det_gift.Controls.Add(lit_det_gift);
                        tr_det_sno.Cells.Add(tc_det_gift);

                        tblUnLstDoc.Rows.Add(tr_det_sno);

                        tc_det_head_main8.Controls.Add(tblUnLstDoc);
                        tr_det_head_main7.Cells.Add(tc_det_head_main8);
                        tbldetail_main7.Rows.Add(tr_det_head_main7);

                        form1.Controls.Add(tbldetail_main7);
                    }
                }

                //form1.Controls.Add(tblUnLstDoc);

                if (iCount > 0)
                {
                    Table tbl_undoc_empty = new Table();
                    TableRow tr_undoc_empty = new TableRow();
                    TableCell tc_undoc_empty = new TableCell();
                    Literal lit_undoc_empty = new Literal();
                    lit_undoc_empty.Text = "<BR>";
                    tc_undoc_empty.Controls.Add(lit_undoc_empty);
                    tr_undoc_empty.Cells.Add(tc_undoc_empty);
                    tbl_undoc_empty.Rows.Add(tr_undoc_empty);
                    form1.Controls.Add(tbl_undoc_empty);
                }

                // 3- Stockist

                //5-Hospitals

                Table tbldetail_main11 = new Table();
                tbldetail_main11.BorderStyle = BorderStyle.None;
                tbldetail_main11.Width = 1100;
                TableRow tr_det_head_main11 = new TableRow();
                TableCell tc_det_head_main11 = new TableCell();
                tr_det_head_main11.Width = 100;
                Literal lit_det_main11 = new Literal();
                lit_det_main11.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_main11.Controls.Add(lit_det_main11);
                tr_det_head_main11.Cells.Add(tc_det_head_main11);

                TableCell tc_det_head_main12 = new TableCell();
                tc_det_head_main12.Width = 1000;

                Table tbldetailstk = new Table();
                tbldetailstk.BorderStyle = BorderStyle.Solid;
                tbldetailstk.BorderWidth = 1;
                tbldetailstk.GridLines = GridLines.Both;
                tbldetailstk.Width = 1000;

                dsdoc = dc.get_dcr_stk_details(sf_code, drdoc["Activity_Date"].ToString(), 3); //3-Stockist
                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_det_head = new TableRow();
                    TableCell tc_det_head_SNo = new TableCell();
                    tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_SNo.BorderWidth = 1;
                    Literal lit_det_head_SNo = new Literal();
                    lit_det_head_SNo.Text = "<b>S.No</b>";
                    tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                    tr_det_head.Cells.Add(tc_det_head_SNo);

                    TableCell tc_det_head_doc = new TableCell();
                    tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_doc.BorderWidth = 1;
                    Literal lit_det_head_doc = new Literal();
                    lit_det_head_doc.Text = "<b>Stockist Name</b>";
                    tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                    tr_det_head.Cells.Add(tc_det_head_doc);

                    TableCell tc_det_head_ww = new TableCell();
                    tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_ww.BorderWidth = 1;
                    Literal lit_det_head_ww = new Literal();
                    lit_det_head_ww.Text = "<b>Worked With</b>";
                    tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                    tr_det_head.Cells.Add(tc_det_head_ww);

                    TableCell tc_det_head_catg = new TableCell();
                    tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_catg.BorderWidth = 1;
                    Literal lit_det_head_catg = new Literal();
                    lit_det_head_catg.Text = "<b>POB</b>";
                    tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_catg.Controls.Add(lit_det_head_catg);
                    tr_det_head.Cells.Add(tc_det_head_catg);

                    tbldetailstk.Rows.Add(tr_det_head);

                    iCount = 0;
                    foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                    {
                        TableRow tr_det_sno = new TableRow();
                        TableCell tc_det_SNo = new TableCell();
                        iCount += 1;
                        Literal lit_det_SNo = new Literal();
                        lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                        tc_det_SNo.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_SNo.BorderWidth = 1;
                        tc_det_SNo.Controls.Add(lit_det_SNo);
                        tr_det_sno.Cells.Add(tc_det_SNo);


                        TableCell tc_det_dr_name = new TableCell();
                        Literal lit_det_dr_name = new Literal();
                        lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["Stockist_Name"].ToString();
                        tc_det_dr_name.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                        tc_det_dr_name.BorderWidth = 1;
                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                        tr_det_sno.Cells.Add(tc_det_dr_name);


                        TableCell tc_det_work = new TableCell();
                        Literal lit_det_work = new Literal();
                        lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                        tc_det_work.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_work.BorderStyle = BorderStyle.Solid;
                        tc_det_work.BorderWidth = 1;
                        tc_det_work.Controls.Add(lit_det_work);
                        tr_det_sno.Cells.Add(tc_det_work);


                        TableCell tc_det_spec = new TableCell();
                        Literal lit_det_spec = new Literal();
                        lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                        tc_det_spec.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_spec.BorderStyle = BorderStyle.Solid;
                        tc_det_spec.BorderWidth = 1;
                        tc_det_spec.Controls.Add(lit_det_spec);
                        tr_det_sno.Cells.Add(tc_det_spec);

                        tbldetailstk.Rows.Add(tr_det_sno);


                        tc_det_head_main12.Controls.Add(tbldetailstk);
                        tr_det_head_main11.Cells.Add(tc_det_head_main12);
                        tbldetail_main11.Rows.Add(tr_det_head_main11);

                        form1.Controls.Add(tbldetail_main11);
                    }
                }

                //form1.Controls.Add(tbldetailhos);

                if (iCount > 0)
                {
                    Table tbl_stk_empty = new Table();
                    TableRow tr_stk_empty = new TableRow();
                    TableCell tc_stk_empty = new TableCell();
                    Literal lit_stk_empty = new Literal();
                    lit_stk_empty.Text = "<BR>";
                    tc_stk_empty.Controls.Add(lit_stk_empty);
                    tr_stk_empty.Cells.Add(tc_stk_empty);
                    tbl_stk_empty.Rows.Add(tr_stk_empty);
                    form1.Controls.Add(tbl_stk_empty);
                }

                //5-Hospitals

                Table tbldetail_main9 = new Table();
                tbldetail_main9.BorderStyle = BorderStyle.None;
                tbldetail_main9.Width = 1100;
                TableRow tr_det_head_main9 = new TableRow();
                TableCell tc_det_head_main9 = new TableCell();
                tc_det_head_main9.Width = 100;
                Literal lit_det_main9 = new Literal();
                lit_det_main9.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_main9.Controls.Add(lit_det_main9);
                tr_det_head_main9.Cells.Add(tc_det_head_main9);

                TableCell tc_det_head_main10 = new TableCell();
                tc_det_head_main10.Width = 1000;


                Table tbldetailhos = new Table();
                tbldetailhos.BorderStyle = BorderStyle.Solid;
                tbldetailhos.BorderWidth = 1;
                tbldetailhos.GridLines = GridLines.Both;
                tbldetailhos.Width = 1000;
                tbldetailhos.Style.Add("border-collapse", "collapse");
                tbldetailhos.Style.Add("border", "solid 1px Black");

                dsdoc = dc.get_dcr_hos_details(sf_code, drdoc["Activity_Date"].ToString(), 5); //5-Hospital
                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_det_head = new TableRow();
                    TableCell tc_det_head_SNo = new TableCell();
                    tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_SNo.BorderWidth = 1;
                    Literal lit_det_head_SNo = new Literal();
                    lit_det_head_SNo.Text = "<b>S.No</b>";
                    tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                    tr_det_head.Cells.Add(tc_det_head_SNo);

                    TableCell tc_det_head_doc = new TableCell();
                    tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_doc.BorderWidth = 1;
                    Literal lit_det_head_doc = new Literal();
                    lit_det_head_doc.Text = "<b>Hospital Name</b>";
                    tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                    tr_det_head.Cells.Add(tc_det_head_doc);

                    TableCell tc_det_head_ww = new TableCell();
                    tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_ww.BorderWidth = 1;
                    Literal lit_det_head_ww = new Literal();
                    lit_det_head_ww.Text = "<b>Worked With</b>";
                    tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                    tr_det_head.Cells.Add(tc_det_head_ww);

                    TableCell tc_det_head_catg = new TableCell();
                    tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_head_catg.BorderWidth = 1;
                    Literal lit_det_head_catg = new Literal();
                    lit_det_head_catg.Text = "<b>POB</b>";
                    tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_catg.Controls.Add(lit_det_head_catg);
                    tr_det_head.Cells.Add(tc_det_head_catg);


                    tbldetailhos.Rows.Add(tr_det_head);

                    iCount = 0;
                    foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                    {
                        TableRow tr_det_sno = new TableRow();
                        TableCell tc_det_SNo = new TableCell();
                        iCount += 1;
                        Literal lit_det_SNo = new Literal();
                        lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                        tc_det_SNo.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_SNo.BorderWidth = 1;
                        tc_det_SNo.Controls.Add(lit_det_SNo);
                        tr_det_sno.Cells.Add(tc_det_SNo);


                        TableCell tc_det_dr_name = new TableCell();
                        Literal lit_det_dr_name = new Literal();
                        lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["Hospital_Name"].ToString();
                        tc_det_dr_name.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                        tc_det_dr_name.BorderWidth = 1;
                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                        tr_det_sno.Cells.Add(tc_det_dr_name);


                        TableCell tc_det_work = new TableCell();
                        Literal lit_det_work = new Literal();
                        lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worked_with_Name"].ToString();
                        tc_det_work.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_work.BorderStyle = BorderStyle.Solid;
                        tc_det_work.BorderWidth = 1;
                        tc_det_work.Controls.Add(lit_det_work);
                        tr_det_sno.Cells.Add(tc_det_work);

                        TableCell tc_det_spec = new TableCell();
                        Literal lit_det_spec = new Literal();
                        lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["POB"].ToString();
                        tc_det_spec.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_spec.BorderStyle = BorderStyle.Solid;
                        tc_det_spec.BorderWidth = 1;
                        tc_det_spec.Controls.Add(lit_det_spec);
                        tr_det_sno.Cells.Add(tc_det_spec);

                        tbldetailhos.Rows.Add(tr_det_sno);
                        tc_det_head_main10.Controls.Add(tbldetailhos);
                        tr_det_head_main9.Cells.Add(tc_det_head_main10);
                        tbldetail_main9.Rows.Add(tr_det_head_main9);

                        form1.Controls.Add(tbldetail_main9);
                    }
                }

                //Pending Approval 

                Table tbldetail_mainPending = new Table();
                tbldetail_mainPending.BorderStyle = BorderStyle.None;
                tbldetail_mainPending.Width = 1100;
                TableRow tr_det_head_mainPending = new TableRow();
                TableCell tc_det_head_mainPending = new TableCell();
                tc_det_head_mainPending.Width = 100;
                Literal lit_det_mainPending = new Literal();
                lit_det_mainPending.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_mainPending.Controls.Add(lit_det_mainPending);
                tr_det_head_mainPending.Cells.Add(tc_det_head_mainPending);

                TableCell tc_det_head_mainPendingSub = new TableCell();
                tc_det_head_mainPendingSub.Width = 1000;


                Table tbldetailhosPending = new Table();
                tbldetailhosPending.BorderStyle = BorderStyle.Solid;
                tbldetailhosPending.BorderWidth = 1;
                tbldetailhosPending.GridLines = GridLines.Both;
                tbldetailhosPending.Width = 1000;
                tbldetailhosPending.Style.Add("border-collapse", "none");
                tbldetailhosPending.Style.Add("border", "none");


                dsdoc = dc.get_Pending_Single_Temp_Date(sf_code, drdoc["Activity_Date"].ToString()); //1-Listed Doctor

                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_det_Pending = new TableRow();
                    TableCell tc_det_Pending = new TableCell();
                    iCount += 1;
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center> <b> " + dsdoc.Tables[0].Rows[0]["Temp"] + " </b> </center>";
                    tc_det_Pending.Style.Add("color", "Red");
                    tc_det_Pending.Style.Add("border", "none");
                    tc_det_Pending.BorderStyle = BorderStyle.Solid;
                    tc_det_Pending.BorderWidth = 1;
                    tc_det_Pending.Controls.Add(lit_det_SNo);
                    tr_det_Pending.Cells.Add(tc_det_Pending);


                    tbldetailhosPending.Rows.Add(tr_det_Pending);
                }

                tc_det_head_mainPendingSub.Controls.Add(tbldetailhosPending);
                tr_det_head_mainPending.Cells.Add(tc_det_head_mainPendingSub);
                tbldetail_mainPending.Rows.Add(tr_det_head_mainPending);

                form1.Controls.Add(tbldetail_mainPending);


                //Pending Approval 

                //form1.Controls.Add(tbldetailhos);

                dsdoc = dc.get_dcr_details(sf_code, drdoc["Activity_Date"].ToString(), 1);
                dsget_dcr_che = dc.get_dcr_che_details(sf_code, drdoc["Activity_Date"].ToString(), 2);
                dsget_dcr_stk = dc.get_dcr_stk_details(sf_code, drdoc["Activity_Date"].ToString(), 3);
                dsget_dcr_hos = dc.get_dcr_hos_details(sf_code, drdoc["Activity_Date"].ToString(), 5);

                if (dsdoc.Tables[0].Rows.Count > 0 || dsget_dcr_che.Tables[0].Rows.Count > 0 || dsget_dcr_stk.Tables[0].Rows.Count > 0 || dsget_dcr_hos.Tables[0].Rows.Count > 0)
                {

                    if (iCount > 0)
                    {
                        Table tbl_hosp_empty = new Table();
                        TableRow tr_hosp_empty = new TableRow();
                        TableCell tc_hosp_empty = new TableCell();
                        Literal lit_hosp_empty = new Literal();
                        lit_hosp_empty.Text = "<BR>";
                        tc_hosp_empty.Controls.Add(lit_hosp_empty);
                        tr_hosp_empty.Cells.Add(tc_hosp_empty);
                        tbl_hosp_empty.Rows.Add(tr_hosp_empty);
                        form1.Controls.Add(tbl_hosp_empty);
                    }

                    Table tbl_line = new Table();
                    tbl_line.BorderStyle = BorderStyle.Solid;
                    tbl_line.Width = 1000;

                    tbl_line.Style.Add("border-collapse", "collapse");
                    tbl_line.Style.Add("border-top", "none");
                    tbl_line.Style.Add("border-right", "none");
                    tbl_line.Style.Add("margin-left", "100px");
                    tbl_line.Style.Add("border-bottom ", "solid 1px Black");

                    TableRow tr_line = new TableRow();
                    tr_line.BorderStyle = BorderStyle.None;
                    TableCell tc_line0 = new TableCell();
                    tc_line0.Width = 100;
                    Literal lit_line0 = new Literal();
                    tc_line0.Controls.Add(lit_line0);
                    tr_line.Cells.Add(tc_line0);

                    TableCell tc_line = new TableCell();
                    tc_line.BorderStyle = BorderStyle.None;
                    tc_line.Width = 1000;
                    Literal lit_line = new Literal();
                    tc_line.Controls.Add(lit_line);
                    tr_line.Cells.Add(tc_line);
                    tbl_line.Rows.Add(tr_line);
                    form1.Controls.Add(tbl_line);
                }
            }
        }
        else
        {
            //lblHead.Visible = true;
            //lblHead.Style.Add("margin-top", "80px");
            //lblHead.Text = "No Record Found";

            pnlbutton.Visible = true;

            Table tbldetail_mainHoliday = new Table();
            tbldetail_mainHoliday.BorderStyle = BorderStyle.None;
            tbldetail_mainHoliday.Width = 1100;
            TableRow tr_det_head_mainHoliday = new TableRow();
            TableCell tc_det_head_mainHolday = new TableCell();
            tc_det_head_mainHolday.Width = 100;
            Literal lit_det_mainHoliday = new Literal();
            lit_det_mainHoliday.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            tbldetail_mainHoliday.Style.Add("margin-top", "110px");
            tc_det_head_mainHolday.Controls.Add(lit_det_mainHoliday);
            tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHolday);

            TableCell tc_det_head_mainHoliday = new TableCell();
            tc_det_head_mainHoliday.Width = 800;

            Table tbldetailHoliday = new Table();
            tbldetailHoliday.BorderStyle = BorderStyle.Solid;
            tbldetailHoliday.BorderWidth = 1;
            tbldetailHoliday.GridLines = GridLines.Both;
            tbldetailHoliday.Width = 1000;
            tbldetailHoliday.Style.Add("border-collapse", "collapse");
            tbldetailHoliday.Style.Add("border", "solid 1px Black");

            TableRow tr_det_sno = new TableRow();
            TableCell tc_det_SNo = new TableCell();
            iCount += 1;
            Literal lit_det_SNo = new Literal();
            lit_det_SNo.Text = "No Record Found";
            tc_det_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_SNo.Attributes.Add("Class", "NoRecord");

            tc_det_SNo.HorizontalAlign = HorizontalAlign.Center;
            tc_det_SNo.BorderWidth = 1;
            tc_det_SNo.BorderStyle = BorderStyle.None;
            tc_det_SNo.Controls.Add(lit_det_SNo);
            tr_det_sno.Cells.Add(tc_det_SNo);

            tbldetailHoliday.Rows.Add(tr_det_sno);

            tc_det_head_mainHoliday.Controls.Add(tbldetailHoliday);
            tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHoliday);
            tbldetail_mainHoliday.Rows.Add(tr_det_head_mainHoliday);

            form1.Controls.Add(tbldetail_mainHoliday);
        }
    }

    private void CreateDynamicDCRPendingApproval(int imonth, int iyear, string sf_code)
    {
        DCR dc = new DCR();
        dsDCR = dc.get_dcr_Pending_date(sf_code, imonth, iyear);
        int iFiledWork = -1;
        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            DCR dcsf = new DCR();
            dssf = dcsf.getSfName_HQ(sf_code);

            if (dssf.Tables[0].Rows.Count > 0)
            {
                Sf_Name = dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                Sf_HQ = dssf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            }

            Table tbldetail_main3 = new Table();
            tbldetail_main3.BorderStyle = BorderStyle.None;
            tbldetail_main3.Width = 1100;

            TableRow tr_det_head_main3 = new TableRow();
            TableCell tc_det_head_main3 = new TableCell();
            tc_det_head_main3.Width = 100;
            Literal lit_det_main3 = new Literal();
            lit_det_main3.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            tc_det_head_main3.Controls.Add(lit_det_main3);
            tr_det_head_main3.Cells.Add(tc_det_head_main3);

            TableCell tc_det_head_main4 = new TableCell();
            tc_det_head_main4.Width = 1000;

            Table tbl = new Table();
            tbl.Width = 1000;

            TableRow tr_ff = new TableRow();
            TableCell tc_ff_name = new TableCell();
            tc_ff_name.BorderStyle = BorderStyle.None;
            tc_ff_name.Width = 500;
            Literal lit_ff_name = new Literal();
            lit_ff_name.Text = "<b>Field Force Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_Name.ToString();
            tc_ff_name.Controls.Add(lit_ff_name);
            tr_ff.Cells.Add(tc_ff_name);

            TableCell tc_HQ = new TableCell();
            tc_HQ.BorderStyle = BorderStyle.None;
            tc_HQ.Width = 500;
            tc_HQ.HorizontalAlign = HorizontalAlign.Right;
            Literal lit_HQ = new Literal();
            lit_HQ.Text = "<b>Head Quarters</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_HQ.ToString();
            tc_HQ.Controls.Add(lit_HQ);
            tr_ff.Cells.Add(tc_HQ);

            tbl.Rows.Add(tr_ff);

            TableRow tr_dcr = new TableRow();
            tbl.Rows.Add(tr_dcr);

            tc_det_head_main4.Controls.Add(tbl);
            tr_det_head_main3.Cells.Add(tc_det_head_main4);
            tbldetail_main3.Rows.Add(tr_det_head_main3);

            form1.Controls.Add(tbldetail_main3);

            Table tbl_head_empty = new Table();
            TableRow tr_head_empty = new TableRow();
            TableCell tc_head_empty = new TableCell();
            Literal lit_head_empty = new Literal();
            lit_head_empty.Text = "<BR>";
            tc_head_empty.Controls.Add(lit_head_empty);
            tr_head_empty.Cells.Add(tc_head_empty);
            tbl_head_empty.Rows.Add(tr_head_empty);
            form1.Controls.Add(tbl_head_empty);

            Table tbldetail_main = new Table();
            tbldetail_main.BorderStyle = BorderStyle.None;
            tbldetail_main.GridLines = GridLines.Both;
            tbldetail_main.Width = 1000;
            //tbldetail_main.Style.Add("border-collapse", "collapse");
            //tbldetail_main.Style.Add("border", "solid 1px Black");
            tbldetail_main.Style.Add("margin-left", "100px");
            TableRow tr_det_head_main = new TableRow();
            //TableCell tc_det_head_main = new TableCell();
            //tc_det_head_main.Width = 100;
            //Literal lit_det_main = new Literal();
            //lit_det_main.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            //tc_det_head_main.Controls.Add(lit_det_main);
            //tr_det_head_main.Cells.Add(tc_det_head_main);                
            TableCell tc_det_head_main2 = new TableCell();
            tc_det_head_main2.Width = 1000;

            Table tbldetail = new Table();
            tbldetail.BorderStyle = BorderStyle.Solid;
            tbldetail.BorderWidth = 1;
            tbldetail.GridLines = GridLines.Both;
            tbldetail.Width = 1000;
            tbldetail.Style.Add("border-collapse", "collapse");
            tbldetail.Style.Add("border", "solid 1px Black");

            if (sf_code.Contains("MR"))
            {
                dsdoc = dc.get_DCRView_Pending_Approval_All(sf_code, cmonth.ToString(), cyear.ToString()); //1-Listed Doctor
            }
            else
            {
                dsdoc = dc.get_DCRView_Pending_Approval_MGR_All(sf_code, cmonth.ToString(), cyear.ToString()); //1-Listed Doctor
            }
            iCount = 0;
            if (dsdoc.Tables[0].Rows.Count > 0)
            {
                TableRow tr_det_head = new TableRow();

                // TableRow tr_det_head_SNo = new TableRow();
                // TableCell tc_det_head_SNo = new TableCell();
                //tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                //tc_det_head_SNo.BorderWidth = 1;
                //tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                //Literal lit_det_head_SNo = new Literal();
                //lit_det_head_SNo.Text = "<b>S.No</b>";
                //tc_det_head_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#668D3C");
                //tc_det_head_SNo.Style.Add("color", "White");
                //tc_det_head_SNo.Style.Add("font-weight", "bold");
                //tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                //tr_det_head.Cells.Add(tc_det_head_SNo);

                TableCell tc_det_head_Date = new TableCell();
                tc_det_head_Date.BorderStyle = BorderStyle.Solid;
                tc_det_head_Date.BorderWidth = 0;
                tc_det_head_Date.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_det_head_Date = new Literal();
                lit_det_head_Date.Text = "<b>Date</b>";
                //tc_det_head_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#4DB8FF");
                //tc_det_head_SNo.Style.Add("color", "White");
                //tc_det_head_SNo.Style.Add("font-size", "10pt");
                //tc_det_head_SNo.Style.Add("font-weight", "bold");
                //tc_det_head_SNo.Style.Add("font-family", "Calibri");
                tc_det_head_Date.Attributes.Add("Class", "tr_det_head");
                tc_det_head_Date.Controls.Add(lit_det_head_Date);
                tr_det_head.Cells.Add(tc_det_head_Date);

                TableCell tc_det_head_Ses = new TableCell();
                tc_det_head_Ses.BorderStyle = BorderStyle.Solid;
                tc_det_head_Ses.BorderWidth = 1;
                tc_det_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_det_head_Ses = new Literal();
                // lit_det_head_Ses.Text = "<b>Territory Worked</b>";
                Territory terr = new Territory();
                dsTerritory = terr.getWorkAreaName(div_code);
                lit_det_head_Ses.Text = "<b>" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + " Worked</b>";
                tc_det_head_Ses.Attributes.Add("Class", "tr_det_head");
                tc_det_head_Ses.Controls.Add(lit_det_head_Ses);
                tr_det_head.Cells.Add(tc_det_head_Ses);

                TableCell tc_det_head_doc = new TableCell();
                tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                tc_det_head_doc.BorderWidth = 1;
                tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_det_head_doc = new Literal();
                lit_det_head_doc.Text = "<b>Sub.Date</b>";
                tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                tc_det_head_doc.Controls.Add(lit_det_head_doc);
                tr_det_head.Cells.Add(tc_det_head_doc);

                TableCell tc_det_head_time = new TableCell();
                tc_det_head_time.BorderStyle = BorderStyle.Solid;
                tc_det_head_time.BorderWidth = 1;
                tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_det_head_time = new Literal();
                lit_det_head_time.Text = "<b>Work Type</b>";
                tc_det_head_time.Attributes.Add("Class", "tr_det_head");
                tc_det_head_time.Controls.Add(lit_det_head_time);
                tr_det_head.Cells.Add(tc_det_head_time);

                TableCell tc_det_head_ww = new TableCell();
                tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                tc_det_head_ww.BorderWidth = 1;
                tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_det_head_ww = new Literal();
                lit_det_head_ww.Text = "<b>Worked With</b>";
                tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                tc_det_head_ww.Controls.Add(lit_det_head_ww);
                tr_det_head.Cells.Add(tc_det_head_ww);

                TableCell tc_det_head_visit = new TableCell();
                tc_det_head_visit.BorderStyle = BorderStyle.Solid;
                tc_det_head_visit.BorderWidth = 1;
                tc_det_head_visit.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_det_head_visit = new Literal();
                lit_det_head_visit.Text = "<b>Listed Dr(s) <br> Met</b>";
                tc_det_head_visit.Attributes.Add("Class", "tr_det_head");
                tc_det_head_visit.Controls.Add(lit_det_head_visit);
                tr_det_head.Cells.Add(tc_det_head_visit);

                TableCell tc_det_head_catg = new TableCell();
                tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                tc_det_head_catg.BorderWidth = 1;
                tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_det_head_catg = new Literal();
                lit_det_head_catg.Text = "<b>Chemist <br> Met</b>";
                tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                tc_det_head_catg.Controls.Add(lit_det_head_catg);
                tr_det_head.Cells.Add(tc_det_head_catg);

                TableCell tc_det_head_POB = new TableCell();
                tc_det_head_POB.BorderStyle = BorderStyle.Solid;
                tc_det_head_POB.BorderWidth = 1;
                tc_det_head_POB.HorizontalAlign = HorizontalAlign.Center;
                tc_det_head_POB.Visible = false;
                Literal lit_det_head_spec = new Literal();
                lit_det_head_spec.Text = "<b>Chemist <br> POB</b>";
                tc_det_head_POB.Attributes.Add("Class", "tr_det_head");
                tc_det_head_POB.Controls.Add(lit_det_head_spec);
                tr_det_head.Cells.Add(tc_det_head_POB);

                TableCell tc_det_head_prod = new TableCell();
                tc_det_head_prod.BorderStyle = BorderStyle.Solid;
                tc_det_head_prod.BorderWidth = 1;
                tc_det_head_prod.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_det_head_prod = new Literal();
                lit_det_head_prod.Text = "<b>Stockist <br> Met</b>";
                tc_det_head_prod.Attributes.Add("Class", "tr_det_head");
                tc_det_head_prod.Controls.Add(lit_det_head_prod);
                tr_det_head.Cells.Add(tc_det_head_prod);

                TableCell tc_det_head_gift = new TableCell();
                tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                tc_det_head_gift.BorderWidth = 1;
                tc_det_head_gift.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_det_head_gift = new Literal();
                lit_det_head_gift.Text = "<b>Non Listed <br> Dr(s)Met</b>";
                tc_det_head_gift.Attributes.Add("Class", "tr_det_head");
                tc_det_head_gift.Controls.Add(lit_det_head_gift);
                tr_det_head.Cells.Add(tc_det_head_gift);

                tbldetail.Rows.Add(tr_det_head);

                iCount = 0;
                iFieldWrkCount = 0;
                int iTotLstCal = 0;
                int iTotChemCal = 0;
                int iTotStockCal = 0;
                int iTotUnLstCal = 0;
                int isum = 0;
                int isumChem = 0;
                int isumStock = 0;
                int isumUnLst = 0;

                foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                {
                    TableRow tr_det_sno = new TableRow();
                    TableCell tc_det_SNo = new TableCell();
                    iCount += 1;
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                    tc_det_SNo.Visible = false;
                    tc_det_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_SNo.BorderWidth = 1;
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    tr_det_sno.Cells.Add(tc_det_SNo);

                    TableCell tc_det_Ses = new TableCell();
                    HyperLink lit_det_Ses = new HyperLink();
                    lit_det_Ses.Text = drdoctor["Activity_Date"].ToString();
                    tc_det_Ses.Attributes.Add("Class", "tbldetail_main");
                    sURL = "rptDcrViewDetails.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&div_code=" + div_code + " &Day=" + lit_det_Ses.Text + "";

                    lit_det_Ses.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                    lit_det_Ses.NavigateUrl = "#";
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_Ses.VerticalAlign = VerticalAlign.Middle;
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);
                    tr_det_sno.Cells.Add(tc_det_Ses);

                    TableCell tc_det_dr_name = new TableCell();
                    Literal lit_det_dr_name = new Literal();
                    lit_det_dr_name.Text = drdoctor["Plan_Name"].ToString();
                    tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                    tc_det_dr_name.Attributes.Add("Class", "tbldetail_main");
                    tc_det_dr_name.BorderWidth = 1;
                    tc_det_dr_name.Controls.Add(lit_det_dr_name);
                    tr_det_sno.Cells.Add(tc_det_dr_name);

                    TableCell tc_det_time = new TableCell();
                    Literal lit_det_time = new Literal();
                    lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor["Submission_Date"].ToString();
                    tc_det_time.BorderStyle = BorderStyle.Solid;
                    tc_det_time.Attributes.Add("Class", "tbldetail_main");
                    tc_det_time.BorderWidth = 1;
                    tc_det_time.Controls.Add(lit_det_time);
                    tr_det_sno.Cells.Add(tc_det_time);

                    if (lit_det_dr_name.Text != "")
                    {
                        iFieldWrkCount += 1;
                        TableCell tc_det_work = new TableCell();
                        Literal lit_det_work = new Literal();
                        if (drdoctor["Temp"].ToString() == "DisApproved")
                        {
                            strDelay = "<span style='color:red'>( " + drdoctor["Temp"].ToString() + "</span> )";
                        }

                        if (sf_code.Contains("MR"))
                        {
                            lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_B"].ToString() + strDelay;
                        }
                        else
                        {
                            lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_M"].ToString() + strDelay;
                        }
                        tc_det_work.BorderStyle = BorderStyle.Solid;
                        tc_det_work.Attributes.Add("Class", "tbldetail_main");
                        tc_det_work.BorderWidth = 1;
                        tc_det_work.Controls.Add(lit_det_work);
                        tr_det_sno.Cells.Add(tc_det_work);

                        TableCell tc_det_lvisit = new TableCell();
                        Literal lit_det_lvisit = new Literal();
                        lit_det_lvisit.Text = "0"; // drdoctor["lvisit"].ToString();
                        tc_det_lvisit.BorderStyle = BorderStyle.Solid;
                        tc_det_lvisit.Attributes.Add("Class", "tbldetail_main");
                        tc_det_lvisit.BorderWidth = 1;
                        tc_det_lvisit.Controls.Add(lit_det_lvisit);
                        tr_det_sno.Cells.Add(tc_det_lvisit);

                        TableCell tc_det_spec = new TableCell();
                        HyperLink Hyllit_det_spec = new HyperLink();
                        Hyllit_det_spec.Text = drdoctor["doc_cnt"].ToString();
                        if (Hyllit_det_spec.Text != "0")
                        {
                            sURL = "rptDCRViewPending.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 1 + "";

                            Hyllit_det_spec.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                            Hyllit_det_spec.NavigateUrl = "#";
                        }
                        tc_det_spec.BorderStyle = BorderStyle.Solid;
                        tc_det_spec.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_spec.Attributes.Add("Class", "tbldetail_main");
                        tc_det_spec.BorderWidth = 1;
                        tc_det_spec.Controls.Add(Hyllit_det_spec);
                        tr_det_sno.Cells.Add(tc_det_spec);

                        iTotLstCal += Convert.ToInt16(Hyllit_det_spec.Text);

                        TableCell tc_det_prod = new TableCell();
                        HyperLink hyllit_det_prod = new HyperLink();
                        hyllit_det_prod.Text = drdoctor["che_cnt"].ToString().ToString();
                        if (hyllit_det_prod.Text != "0")
                        {
                            sURL = "rptDCRViewPending.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 2 + "";
                            hyllit_det_prod.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                            hyllit_det_prod.NavigateUrl = "#";
                        }
                        tc_det_prod.BorderStyle = BorderStyle.Solid;
                        tc_det_prod.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_prod.Attributes.Add("Class", "tbldetail_main");
                        tc_det_prod.BorderWidth = 1;
                        tc_det_prod.Controls.Add(hyllit_det_prod);
                        tr_det_sno.Cells.Add(tc_det_prod);

                        iTotChemCal += Convert.ToInt16(hyllit_det_prod.Text);

                        //TableCell tc_det_Che_POB = new TableCell();
                        //Literal lit_det_Che_POB = new Literal();
                        //lit_det_Che_POB.Text = drdoctor["che_POB"].ToString().ToString();
                        //tc_det_Che_POB.BorderStyle = BorderStyle.Solid;
                        //tc_det_head_POB.Visible = false;
                        //tc_det_Che_POB.HorizontalAlign = HorizontalAlign.Center;
                        //tc_det_Che_POB.BorderWidth = 1;
                        //tc_det_Che_POB.Controls.Add(lit_det_Che_POB);
                        //tr_det_sno.Cells.Add(tc_det_Che_POB);

                        TableCell tc_det_gift = new TableCell();
                        HyperLink hyllit_det_gift = new HyperLink();
                        hyllit_det_gift.Text = drdoctor["stk_cnt"].ToString();
                        if (hyllit_det_gift.Text != "0")
                        {
                            sURL = "rptDCRViewPending.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 3 + "";

                            hyllit_det_gift.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                            hyllit_det_gift.NavigateUrl = "#";
                        }
                        tc_det_gift.BorderStyle = BorderStyle.Solid;
                        tc_det_gift.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_gift.Attributes.Add("Class", "tbldetail_main");
                        tc_det_gift.BorderWidth = 1;
                        tc_det_gift.Controls.Add(hyllit_det_gift);
                        tr_det_sno.Cells.Add(tc_det_gift);

                        iTotStockCal += Convert.ToInt16(hyllit_det_gift.Text);

                        TableCell tc_det_UnDoc = new TableCell();
                        HyperLink hyllit_det_UnDoc = new HyperLink();
                        hyllit_det_UnDoc.Text = drdoctor["Undoc_cnt"].ToString();
                        if (hyllit_det_UnDoc.Text != "0")
                        {
                            sURL = "rptDCRViewPending.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 4 + "";

                            hyllit_det_UnDoc.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                            hyllit_det_UnDoc.NavigateUrl = "#";
                        }

                        tc_det_UnDoc.BorderStyle = BorderStyle.Solid;
                        tc_det_UnDoc.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_UnDoc.Attributes.Add("Class", "tbldetail_main");
                        tc_det_UnDoc.BorderWidth = 1;
                        tc_det_UnDoc.Controls.Add(hyllit_det_UnDoc);
                        tr_det_sno.Cells.Add(tc_det_UnDoc);
                        iTotUnLstCal += Convert.ToInt16(hyllit_det_UnDoc.Text);

                    }
                    else
                    {
                        TableCell tc_det_NonFwk = new TableCell();
                        Literal lit_det_NonFwk = new Literal();
                        lit_det_NonFwk.Text = drdoctor["Worktype_Name_B"].ToString();
                        tc_det_NonFwk.BorderStyle = BorderStyle.Solid;
                        tc_det_NonFwk.BackColor = System.Drawing.ColorTranslator.FromHtml("#B2E0E6");
                        tc_det_NonFwk.Attributes.Add("Class", "tbldetail_main");
                        tc_det_NonFwk.ColumnSpan = 6;
                        tc_det_NonFwk.Controls.Add(lit_det_NonFwk);
                        tr_det_sno.Cells.Add(tc_det_NonFwk);
                    }

                    tbldetail.Rows.Add(tr_det_sno);

                    tc_det_head_main2.Controls.Add(tbldetail);
                    tr_det_head_main.Cells.Add(tc_det_head_main2);
                    tbldetail_main.Rows.Add(tr_det_head_main);

                    form1.Controls.Add(tbldetail_main);
                }

                TableRow tr_total = new TableRow();

                TableCell tc_Count_Total = new TableCell();
                tc_Count_Total.BorderStyle = BorderStyle.Solid;
                tc_Count_Total.BorderWidth = 1;
                //tc_catg_Total.Width = 25;
                Literal lit_Count_Total = new Literal();
                lit_Count_Total.Text = "<center>Total</center>";
                tc_Count_Total.Controls.Add(lit_Count_Total);
                tc_Count_Total.Font.Bold.ToString();
                tc_Count_Total.BackColor = System.Drawing.Color.White;
                tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
                tc_Count_Total.ColumnSpan = 5;
                tc_Count_Total.Style.Add("text-align", "left");
                tc_Count_Total.Style.Add("font-family", "Calibri");
                tc_Count_Total.Style.Add("font-size", "10pt");

                tr_total.Cells.Add(tc_Count_Total);

                int[] arrTotDoc = new int[] { iTotLstCal };

                for (int i = 0; i < arrTotDoc.Length; i++)
                {
                    isum += arrTotDoc[i];
                }

                decimal RoundUnLstCallAvg = new decimal();

                double Count = (double)iTotLstCal / iFieldWrkCount;
                if (iFieldWrkCount != 0)
                {
                    RoundUnLstCallAvg = Math.Round((decimal)Count, 2);
                }

                //double result = (double)150 / 100;

                TableCell tc_Lst_Count_Total = new TableCell();
                tc_Lst_Count_Total.BorderStyle = BorderStyle.Solid;
                tc_Lst_Count_Total.BorderWidth = 1;
                //tc_catg_Total.Width = 25;
                Literal lit_Lst_Count_Total = new Literal();
                lit_Lst_Count_Total.Text = Convert.ToString(RoundUnLstCallAvg);
                tc_Lst_Count_Total.Controls.Add(lit_Lst_Count_Total);
                tc_Lst_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                tc_Lst_Count_Total.VerticalAlign = VerticalAlign.Middle;
                tc_Lst_Count_Total.Attributes.Add("Class", "tbldetail_main");
                tc_Lst_Count_Total.Font.Bold.ToString();
                tc_Lst_Count_Total.BackColor = System.Drawing.Color.White;
                //tc_Lst_Count_Total.Style.Add("text-align", "left");
                //tc_Lst_Count_Total.Style.Add("font-family", "Calibri");
                //tc_Lst_Count_Total.Style.Add("font-size", "10pt");
                tr_total.Cells.Add(tc_Lst_Count_Total);

                int[] arrTotChem = new int[] { iTotChemCal };

                for (int i = 0; i < arrTotChem.Length; i++)
                {
                    isumChem += arrTotChem[i];
                }

                decimal RoundiTotChemCal = new decimal();

                double TotChemCalAvg = (double)iTotChemCal / iFieldWrkCount;
                if (iFieldWrkCount != 0)
                {
                    RoundiTotChemCal = Math.Round((decimal)TotChemCalAvg, 2);
                }

                TableCell tc_Chem_Count_Total = new TableCell();
                tc_Chem_Count_Total.BorderStyle = BorderStyle.Solid;
                tc_Chem_Count_Total.BorderWidth = 1;
                //tc_catg_Total.Width = 25;
                Literal lit_Chem_Count_Total = new Literal();
                lit_Chem_Count_Total.Text = Convert.ToString(RoundiTotChemCal);
                tc_Chem_Count_Total.Controls.Add(lit_Chem_Count_Total);
                tc_Chem_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                tc_Chem_Count_Total.Attributes.Add("Class", "tbldetail_main");
                tc_Chem_Count_Total.Font.Bold.ToString();
                tc_Chem_Count_Total.BackColor = System.Drawing.Color.White;
                //tc_Chem_Count_Total.Style.Add("text-align", "left");
                //tc_Chem_Count_Total.Style.Add("font-family", "Calibri");
                //tc_Chem_Count_Total.Style.Add("font-size", "10pt");
                tr_total.Cells.Add(tc_Chem_Count_Total);

                int[] arrtotStock = new int[] { iTotStockCal };

                for (int i = 0; i < arrtotStock.Length; i++)
                {
                    isumStock += arrtotStock[i];
                }

                decimal RoundiTotStockCal = new decimal();

                double TotStockCal = (double)iTotStockCal / iFieldWrkCount;
                if (iFieldWrkCount != 0)
                {
                    RoundiTotStockCal = Math.Round((decimal)TotStockCal, 2);
                }

                TableCell tc_Stock_Count_Total = new TableCell();
                tc_Stock_Count_Total.BorderStyle = BorderStyle.Solid;
                tc_Stock_Count_Total.BorderWidth = 1;
                //tc_catg_Total.Width = 25;
                Literal lit_Stock_Count_Total = new Literal();
                lit_Stock_Count_Total.Text = Convert.ToString(RoundiTotStockCal);
                tc_Stock_Count_Total.Controls.Add(lit_Stock_Count_Total);
                tc_Stock_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                tc_Stock_Count_Total.Attributes.Add("Class", "tbldetail_main");
                tc_Stock_Count_Total.Font.Bold.ToString();
                tc_Stock_Count_Total.BackColor = System.Drawing.Color.White;
                //tc_Stock_Count_Total.Style.Add("text-align", "left");
                //tc_Stock_Count_Total.Style.Add("font-family", "Calibri");
                //tc_Stock_Count_Total.Style.Add("font-size", "10pt");
                tr_total.Cells.Add(tc_Stock_Count_Total);

                int[] arrtotUnLst = new int[] { iTotUnLstCal };

                for (int i = 0; i < arrtotUnLst.Length; i++)
                {
                    isumUnLst += arrtotUnLst[i];
                }

                decimal RoundiTotUnLstCal = new decimal();

                double TotUnLstCal = (double)iTotUnLstCal / iFieldWrkCount;
                if (iFieldWrkCount != 0)
                {
                    RoundiTotUnLstCal = Math.Round((decimal)TotUnLstCal, 2);
                }

                TableCell tc_UnLst_Count_Total = new TableCell();
                tc_UnLst_Count_Total.BorderStyle = BorderStyle.Solid;
                tc_UnLst_Count_Total.BorderWidth = 1;
                //tc_catg_Total.Width = 25;
                Literal lit_UnLst_Count_Total = new Literal();
                lit_UnLst_Count_Total.Text = Convert.ToString(RoundiTotUnLstCal);
                tc_UnLst_Count_Total.Controls.Add(lit_UnLst_Count_Total);
                tc_UnLst_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                tc_UnLst_Count_Total.Attributes.Add("Class", "tbldetail_main");
                tc_UnLst_Count_Total.Font.Bold.ToString();
                tc_UnLst_Count_Total.BackColor = System.Drawing.Color.White;
                //tc_UnLst_Count_Total.Style.Add("text-align", "left");
                //tc_UnLst_Count_Total.Style.Add("font-family", "Calibri");
                //tc_UnLst_Count_Total.Style.Add("font-size", "10pt");
                tr_total.Cells.Add(tc_UnLst_Count_Total);

                tbldetail.Rows.Add(tr_total);

                tc_det_head_main2.Controls.Add(tbldetail);
                tr_det_head_main.Cells.Add(tc_det_head_main2);
                tbldetail_main.Rows.Add(tr_det_head_main);

                form1.Controls.Add(tbldetail_main);
            }
        }
        else
        {
            //lblHead.Visible = true;
            //lblHead.Style.Add("margin-top", "80px");
            //lblHead.Text = "No Record Found";

            pnlbutton.Visible = true;

            Table tbldetail_mainHoliday = new Table();
            tbldetail_mainHoliday.BorderStyle = BorderStyle.None;
            tbldetail_mainHoliday.Width = 1100;
            TableRow tr_det_head_mainHoliday = new TableRow();
            TableCell tc_det_head_mainHolday = new TableCell();
            tc_det_head_mainHolday.Width = 100;
            Literal lit_det_mainHoliday = new Literal();
            lit_det_mainHoliday.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            tbldetail_mainHoliday.Style.Add("margin-top", "110px");
            tc_det_head_mainHolday.Controls.Add(lit_det_mainHoliday);
            tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHolday);

            TableCell tc_det_head_mainHoliday = new TableCell();
            tc_det_head_mainHoliday.Width = 800;

            Table tbldetailHoliday = new Table();
            tbldetailHoliday.BorderStyle = BorderStyle.Solid;
            tbldetailHoliday.BorderWidth = 1;
            tbldetailHoliday.GridLines = GridLines.Both;
            tbldetailHoliday.Width = 1000;
            tbldetailHoliday.Style.Add("border-collapse", "collapse");
            tbldetailHoliday.Style.Add("border", "solid 1px Black");
            tbldetailHoliday.Style.Add("margin-left", "200px");

            TableRow tr_det_sno = new TableRow();
            TableCell tc_det_SNo = new TableCell();
            iCount += 1;
            Literal lit_det_SNo = new Literal();
            lit_det_SNo.Text = "No Record Found";
            tc_det_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_SNo.Attributes.Add("Class", "NoRecord");



            tc_det_SNo.HorizontalAlign = HorizontalAlign.Center;
            tc_det_SNo.BorderWidth = 1;
            tc_det_SNo.BorderStyle = BorderStyle.None;
            tc_det_SNo.Controls.Add(lit_det_SNo);
            tr_det_sno.Cells.Add(tc_det_SNo);

            tbldetailHoliday.Rows.Add(tr_det_sno);

            tc_det_head_mainHoliday.Controls.Add(tbldetailHoliday);
            tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHoliday);
            tbldetail_mainHoliday.Rows.Add(tr_det_head_mainHoliday);

            form1.Controls.Add(tbldetail_mainHoliday);
        }
    }



    private void CreateDynamicDCRDetailedView_primary(string Fdate, string Tdate, string sf_code)
    {
        try
        {

            DCR dc = new DCR();
            //dsDCR = dc.get_dcr_date(sf_code, imonth, iyear);
            dsDCR = dc.get_dcr_DCRPendingdate_DCRDetail(sf_code, Fdate, Tdate);
            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                DCR dcsf = new DCR();
                dssf = dcsf.getSfName_HQ(sf_code);

                if (dssf.Tables[0].Rows.Count > 0)
                {
                    Sf_Name = dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Sf_HQ = dssf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }

                Table tbldetail_main3 = new Table();
                tbldetail_main3.BorderStyle = BorderStyle.None;
                tbldetail_main3.Width = 1100;

                TableRow tr_det_head_main3 = new TableRow();
                TableCell tc_det_head_main3 = new TableCell();
                tc_det_head_main3.Width = 100;
                Literal lit_det_main3 = new Literal();
                lit_det_main3.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_main3.Controls.Add(lit_det_main3);
                tr_det_head_main3.Cells.Add(tc_det_head_main3);

                TableCell tc_det_head_main4 = new TableCell();
                tc_det_head_main4.Width = 1000;

                Table tbl = new Table();
                tbl.Width = 350;

                TableRow tr_ff = new TableRow();
                TableCell tc_ff_name = new TableCell();
                tc_ff_name.BorderStyle = BorderStyle.None;
                tr_ff.Font.Bold = true;
                tc_ff_name.Font.Underline = true;
                tc_ff_name.Width = 200;
                Literal lit_ff_name = new Literal();
                lit_ff_name.Text = "<b>FIELD FORCE NAME:</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_Name.ToString();
                tc_ff_name.Controls.Add(lit_ff_name);
                tr_ff.Cells.Add(tc_ff_name);

                TableCell tc_HQ = new TableCell();
                tc_HQ.BorderStyle = BorderStyle.None;
                tc_HQ.Width = 150;
                tc_HQ.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_HQ = new Literal();
                tc_HQ.Font.Underline = true;
                lit_HQ.Text = "<b>HEAD QUARTERS:</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_HQ.ToString();
                tc_HQ.Controls.Add(lit_HQ);
                tr_ff.Cells.Add(tc_HQ);

                tbl.Rows.Add(tr_ff);

                TableRow tr_dcr = new TableRow();
                tbl.Rows.Add(tr_dcr);

                tc_det_head_main4.Controls.Add(tbl);
                tr_det_head_main3.Cells.Add(tc_det_head_main4);
                tbldetail_main3.Rows.Add(tr_det_head_main3);

                form1.Controls.Add(tbldetail_main3);

                Table tbl_head_empty = new Table();
                TableRow tr_head_empty = new TableRow();
                TableCell tc_head_empty = new TableCell();
                Literal lit_head_empty = new Literal();
                lit_head_empty.Text = "<BR>";
                tc_head_empty.Controls.Add(lit_head_empty);
                tr_head_empty.Cells.Add(tc_head_empty);
                tbl_head_empty.Rows.Add(tr_head_empty);
                form1.Controls.Add(tbl_head_empty);

                Table tbldetail_main = new Table();
                tbldetail_main.BorderStyle = BorderStyle.None;
                tbldetail_main.GridLines = GridLines.Both;
                tbldetail_main.Width = 1100;
                //tbldetail_main.Style.Add("border-collapse", "collapse");
                //tbldetail_main.Style.Add("border", "solid 1px Black");
                tbldetail_main.Style.Add("margin-left", "100px");
                TableRow tr_det_head_main = new TableRow();
                //TableCell tc_det_head_main = new TableCell();
                //tc_det_head_main.Width = 100;
                //Literal lit_det_main = new Literal();
                //lit_det_main.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                //tc_det_head_main.Controls.Add(lit_det_main);
                //tr_det_head_main.Cells.Add(tc_det_head_main);                
                TableCell tc_det_head_main2 = new TableCell();
                tc_det_head_main2.Width = 1100;

                Table tbldetail = new Table();
                tbldetail.BorderStyle = BorderStyle.Solid;
                tbldetail.BorderWidth = 1;
                tbldetail.GridLines = GridLines.Both;
                tbldetail.Width = 1100;
                tbldetail.Style.Add("border-collapse", "collapse");
                tbldetail.Style.Add("border", "solid 1px Black");



                dsdoc = dc.get_DCRView_Approved_All_Dates_primary(sf_code, Fdate.ToString(), Tdate.ToString()); //1-Listed Doctor
                //if (sf_code.Contains("MR"))
                //{
                //    dsdoc = dc.get_DCRView_Approved_All_Dates(sf_code, Fdate.ToString(), Tdate.ToString()); //1-Listed Doctor
                //}
                //else
                //{
                //    dsdoc = dc.get_DCRView_Approved_MGR_All_Dates(sf_code, Fdate.ToString(), Tdate.ToString()); //1-Listed Doctor
                //}
                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_det_head = new TableRow();
                    TableCell tc_det_head_SNo = new TableCell();
                    tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_head_SNo.BorderWidth = 0;
                    tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_SNo = new Literal();
                    lit_det_head_SNo.Text = "<b>Date</b>";
                    //tc_det_head_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#4DB8FF");
                    //tc_det_head_SNo.Style.Add("color", "White");
                    //tc_det_head_SNo.Style.Add("font-size", "10pt");
                    //tc_det_head_SNo.Style.Add("font-weight", "bold");
                    //tc_det_head_SNo.Style.Add("font-family", "Calibri");
                    tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                    tr_det_head.Cells.Add(tc_det_head_SNo);

                    TableCell tc_det_head_Ses = new TableCell();
                    tc_det_head_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_head_Ses.BorderWidth = 1;
                    tc_det_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                    //tc_det_head_Ses.Visible = false;
                    Literal lit_det_head_Ses = new Literal();
                    // lit_det_head_Ses.Text = "<b>Territory Worked</b>";
                    Territory terr = new Territory();
                    dsTerritory = terr.getWorkAreaName(div_code);
                    lit_det_head_Ses.Text = "<b>Distributor Name</b>";
                    tc_det_head_Ses.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_Ses.Controls.Add(lit_det_head_Ses);
                    tr_det_head.Cells.Add(tc_det_head_Ses);

                    TableCell tc_det_head_doc = new TableCell();
                    tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                    tc_det_head_doc.BorderWidth = 1;
                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_doc = new Literal();
                    lit_det_head_doc.Text = "<b>Payment Type</b>";
                    tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                    tr_det_head.Cells.Add(tc_det_head_doc);

                    TableCell tc_det_head_time = new TableCell();
                    tc_det_head_time.BorderStyle = BorderStyle.Solid;
                    tc_det_head_time.BorderWidth = 1;
                    tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_time = new Literal();
                    lit_det_head_time.Text = "<b>Date of Payment</b>";
                    tc_det_head_time.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_time.Controls.Add(lit_det_head_time);
                    tr_det_head.Cells.Add(tc_det_head_time);

                    TableCell tc_det_head_ww = new TableCell();
                    tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_head_ww.BorderWidth = 1;
                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_ww = new Literal();
                    lit_det_head_ww.Text = "<b>Amount</b>";
                    tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                    tr_det_head.Cells.Add(tc_det_head_ww);

                    TableCell tc_det_head_visit = new TableCell();
                    tc_det_head_visit.BorderStyle = BorderStyle.Solid;
                    tc_det_head_visit.BorderWidth = 1;
                    tc_det_head_visit.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_visit = new Literal();
                    lit_det_head_visit.Text = "<b>Remarks</b>";
                    tc_det_head_visit.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_visit.Controls.Add(lit_det_head_visit);
                    tr_det_head.Cells.Add(tc_det_head_visit);

                    TableCell tc_det_prod = new TableCell();
                    tc_det_prod.BorderStyle = BorderStyle.Solid;
                    Literal lit_det_prod = new Literal();



                    //Territory terr = new Territory();
                    dsTerritory = terr.getProdName(div_code);
                    foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                    {
                        TableCell tc = new TableCell();
                        tc.Style.Add("width", "45px");
                        tc.BorderStyle = BorderStyle.Solid;
                        tc.BorderWidth = 1;
                        tc.HorizontalAlign = HorizontalAlign.Center;
                        Literal lic = new Literal();
                        if (div_code == "11" || div_code == "13" || div_code == "8")
                        {
                            lic.Text = "<b>" + pro1["Product_Detail_Name"].ToString() + "</b>";
                        }
                        else
                        {
                            lic.Text = "<b>" + pro1["Product_Short_Name"].ToString() + "</b>";
                        }
                        tc.Attributes.Add("Class", "tr_det_head");
                        tc.Controls.Add(lic);
                        tr_det_head.Cells.Add(tc);

                    }




                    TableCell tc_det_head_gift = new TableCell();
                    tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                    tc_det_head_gift.BorderWidth = 1;
                    tc_det_head_gift.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_gift = new Literal();
                    lit_det_head_gift.Text = "<b>Value</b>";
                    tc_det_head_gift.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_gift.Controls.Add(lit_det_head_gift);
                    tr_det_head.Cells.Add(tc_det_head_gift);

                    tbldetail.Rows.Add(tr_det_head);

                    iCount = 0;
                    iFieldWrkCount = 0;
                    int iTotLstCal = 0;
                    int iTotChemPOB = 0;
                    int iTotChemCal = 0;
                    int iTotStockCal = 0;
                    int iTotUnLstCal = 0;
                    int isum = 0;
                    int isumChemPOB = 0;
                    int isumChem = 0;
                    int isumStock = 0;
                    int isumUnLst = 0;
                    int[] na = new int[500];

                    foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                    {
                        strDelay = "";
                        TableRow tr_det_sno = new TableRow();
                        TableCell tc_det_SNo = new TableCell();
                        iCount += 1;
                        Literal lit_det_SNo = new Literal();
                        lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                        tc_det_SNo.Visible = false;
                        tc_det_SNo.BorderStyle = BorderStyle.Solid;
                        //tc_det_SNo.BorderWidth = 1;
                        tc_det_SNo.Controls.Add(lit_det_SNo);
                        tr_det_sno.Cells.Add(tc_det_SNo);

                        TableCell tc_det_Ses = new TableCell();
                        HyperLink lit_det_Ses = new HyperLink();

                        lit_det_Ses.Text = drdoctor["Activity_Date"].ToString();
                        tc_det_Ses.Attributes.Add("Class", "tbldetail_main");

                        tc_det_Ses.BorderStyle = BorderStyle.Solid;
                        tc_det_Ses.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_Ses.VerticalAlign = VerticalAlign.Middle;
                        //tc_det_Ses.BorderWidth = 1;
                        tc_det_Ses.Controls.Add(lit_det_Ses);
                        tr_det_sno.Cells.Add(tc_det_Ses);

                        TableCell tc_det_dr_name = new TableCell();
                        Literal lit_det_dr_name = new Literal();
                        tc_det_dr_name.Style.Add("width", "85px");
                        //tc_det_dr_name.Visible = false;
                        //if (drdoctor["che_POB_Name"].ToString() != "[]")
                        //{
                        lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["Trans_Detail_Name"].ToString();
                        //}
                        tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                        tc_det_dr_name.Attributes.Add("Class", "tbldetail_main");
                        tc_det_dr_name.BorderWidth = 1;
                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                        tr_det_sno.Cells.Add(tc_det_dr_name);

                        TableCell tc_det_time = new TableCell();
                        tc_det_time.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_time = new Literal();
                        lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor["instrument_type"].ToString();
                        tc_det_time.BorderStyle = BorderStyle.Solid;
                        tc_det_time.Attributes.Add("Class", "tbldetail_main");
                        //tc_det_time.BorderWidth = 1;
                        tc_det_time.Controls.Add(lit_det_time);
                        tr_det_sno.Cells.Add(tc_det_time);


                        TableCell tc_det_time1 = new TableCell();
                        tc_det_time1.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_time1 = new Literal();
                        lit_det_time1.Text = "&nbsp;&nbsp;" + drdoctor["date_of_instrument"].ToString();
                        tc_det_time1.BorderStyle = BorderStyle.Solid;
                        tc_det_time1.Attributes.Add("Class", "tbldetail_main");
                        //tc_det_time.BorderWidth = 1;
                        tc_det_time1.Controls.Add(lit_det_time1);
                        tr_det_sno.Cells.Add(tc_det_time1);


                        string strWorktypeName = "";


                        TableCell tc_det_lvisit = new TableCell();
                        tc_det_lvisit.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_lvisit = new Literal();
                        lit_det_lvisit.Text = drdoctor["POB"].ToString();
                        tc_det_lvisit.BorderStyle = BorderStyle.Solid;
                        tc_det_lvisit.Attributes.Add("Class", "tbldetail_main");
                        //tc_det_lvisit.BorderWidth = 1;
                        tc_det_lvisit.Controls.Add(lit_det_lvisit);
                        tr_det_sno.Cells.Add(tc_det_lvisit);

                        TableCell tc_det_spec = new TableCell();
                        HyperLink Hyllit_det_spec = new HyperLink();
                        Hyllit_det_spec.Text = drdoctor["Activity_Remarks"].ToString();
                        //if (Hyllit_det_spec.Text != "0")
                        //{
                        //    sURL = "rptDCRViewCompleted.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 1 + "";

                        //    Hyllit_det_spec.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                        //    Hyllit_det_spec.NavigateUrl = "#";
                        //}
                        tc_det_spec.BorderStyle = BorderStyle.Solid;
                        tc_det_spec.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_spec.Attributes.Add("Class", "tbldetail_main");
                        //tc_det_spec.BorderWidth = 1;
                        tc_det_spec.Controls.Add(Hyllit_det_spec);
                        tr_det_sno.Cells.Add(tc_det_spec);

                        //iTotLstCal += Convert.ToInt16(Hyllit_det_spec.Text);

                        //string Activity_date3 = string.Empty;
                        //string datet3 = string.Empty;
                        //string hdate3 = string.Empty;
                        //Activity_date3 = drdoctor["Activity_Date"].ToString();
                        //datet3 = Activity_date3.Trim();
                        //DateTime dtt3 = Convert.ToDateTime(datet3);
                        //hdate3 = dtt3.ToString("yyyy-MM-dd");

                        DCR sf = new DCR();
                        dsDrr = sf.dcr_Gettransno_primary(sf_code, div_code, drdoctor["trans_slno"].ToString(), drdoctor["Trans_Detail_Info_Code"].ToString());
                        int[] fi = new int[500];
                        int j = 0;
                        double qval = 0;
                        foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                        {
                            string pCode = pro1["Product_Detail_Code"].ToString();
                            var DSRows = (from w in dsDrr.Tables[0].AsEnumerable() where w.Field<string>("Product_Code") == pCode select w);
                            var DSRows1 = (from w in dsDrr.Tables[0].AsEnumerable() where w.Field<string>("Product_Code") == pCode select w);
                            string sQty = string.Empty;

                            foreach (var prd in DSRows)
                            {
                                sQty = prd.Field<string>("Quantity");

                                Tot_Sec += Decimal.Parse(prd.Field<string>("Quantity"));
                            }
                            foreach (var prd1 in DSRows1)
                            {

                                qval += prd1.Field<double>("Fdiscountval");

                            }
                            TableCell tc = new TableCell();
                            tc.BorderStyle = BorderStyle.Solid;
                            tc.BorderWidth = 1;
                            tc.HorizontalAlign = HorizontalAlign.Center;
                            Literal lic = new Literal();
                            lic.Text = sQty;

                            tc.Attributes.Add("Class", "tbldetail_Data");
                            tc.Controls.Add(lic);
                            tr_det_sno.Cells.Add(tc);
                            fi[j] += sQty == "" ? 0 : Convert.ToInt32(sQty);
                            j++;
                        }
                        for (int l = 0; l < dsTerritory.Tables[0].Rows.Count; l++)
                        {
                            na[l] += Convert.ToInt32(fi[l]);
                        }

                        Tot_Sec = 0;


                        tbldetail.Rows.Add(tr_det_sno);
                        TableCell tblvalue = new TableCell();
                        tblvalue.HorizontalAlign = HorizontalAlign.Center;
                        Literal lblvalue = new Literal();
                        string value = Convert.ToString(qval);

                        lblvalue.Text = value;
                        tblvalue.BorderStyle = BorderStyle.Solid;
                        tblvalue.Attributes.Add("Class", "tbldetail_main");
                        //tc_det_lvisit.BorderWidth = 1;
                        tblvalue.Controls.Add(lblvalue);
                        tr_det_sno.Cells.Add(tblvalue);
                        tbldetail.Rows.Add(tr_det_sno);



                        tc_det_head_main2.Controls.Add(tbldetail);
                        tr_det_head_main.Cells.Add(tc_det_head_main2);
                        tbldetail_main.Rows.Add(tr_det_head_main);






                        form1.Controls.Add(tbldetail_main);


                    }
                }
            }
        }
        //else
        //{
        //    //lblHead.Visible = true;
        //    //lblHead.Style.Add("margin-top", "80px");
        //    //lblHead.Text = "No Record Found";

        //    pnlbutton.Visible = true;

        //    Table tbldetail_mainHoliday = new Table();
        //    tbldetail_mainHoliday.BorderStyle = BorderStyle.None;
        //    tbldetail_mainHoliday.Width = 1100;
        //    TableRow tr_det_head_mainHoliday = new TableRow();
        //    TableCell tc_det_head_mainHolday = new TableCell();
        //    tc_det_head_mainHolday.Width = 100;
        //    Literal lit_det_mainHoliday = new Literal();
        //    lit_det_mainHoliday.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        //    tbldetail_mainHoliday.Style.Add("margin-top", "110px");
        //    tc_det_head_mainHolday.Controls.Add(lit_det_mainHoliday);
        //    tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHolday);

        //    TableCell tc_det_head_mainHoliday = new TableCell();
        //    tc_det_head_mainHoliday.Width = 800;

        //    Table tbldetailHoliday = new Table();
        //    tbldetailHoliday.BorderStyle = BorderStyle.Solid;
        //    tbldetailHoliday.BorderWidth = 1;
        //    tbldetailHoliday.GridLines = GridLines.Both;
        //    tbldetailHoliday.Width = 1000;
        //    tbldetailHoliday.Style.Add("border-collapse", "collapse");
        //    tbldetailHoliday.Style.Add("border", "solid 1px Black");

        //    TableRow tr_det_sno = new TableRow();
        //    TableCell tc_det_SNo = new TableCell();
        //    iCount += 1;
        //    Literal lit_det_SNo = new Literal();
        //    lit_det_SNo.Text = "No Record Found";
        //    tc_det_SNo.BorderStyle = BorderStyle.Solid;
        //    tc_det_SNo.Attributes.Add("Class", "NoRecord");

        //    tc_det_SNo.HorizontalAlign = HorizontalAlign.Center;
        //    tc_det_SNo.BorderWidth = 1;
        //    tc_det_SNo.BorderStyle = BorderStyle.None;
        //    tc_det_SNo.Controls.Add(lit_det_SNo);
        //    tr_det_sno.Cells.Add(tc_det_SNo);

        //    tbldetailHoliday.Rows.Add(tr_det_sno);

        //    tc_det_head_mainHoliday.Controls.Add(tbldetailHoliday);
        //    tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHoliday);
        //    tbldetail_mainHoliday.Rows.Add(tr_det_head_mainHoliday);

        //    form1.Controls.Add(tbldetail_mainHoliday);
        //}


        //lblHead.Visible = true;
        //lblHead.Style.Add("margin-top", "80px");
        //lblHead.Text = "No Record Found";



        //}
        catch (Exception ex)
        {

        }

    }

    private void CreateDynamicDCRViewListedDoctorRemarks(int imonth, int iyear, string sf_code)
    {
        DCR dc = new DCR();

        DCR dcsf = new DCR();
        dssf = dcsf.getSfName_HQ(sf_code);

        if (dssf.Tables[0].Rows.Count > 0)
        {
            Sf_Name = dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            Sf_HQ = dssf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
        }



        Table tbl_head_empty = new Table();
        TableRow tr_head_empty = new TableRow();
        TableCell tc_head_empty = new TableCell();
        Literal lit_head_empty = new Literal();
        lit_head_empty.Text = "<BR>";
        tc_head_empty.Controls.Add(lit_head_empty);
        tr_head_empty.Cells.Add(tc_head_empty);
        tbl_head_empty.Rows.Add(tr_head_empty);
        form1.Controls.Add(tbl_head_empty);

        Table tbldetail_main = new Table();
        tbldetail_main.BorderStyle = BorderStyle.None;
        tbldetail_main.GridLines = GridLines.Both;
        tbldetail_main.Width = 1000;
        tbldetail_main.Style.Add("margin-left", "100px");
        TableRow tr_det_head_main = new TableRow();
        TableCell tc_det_head_main2 = new TableCell();
        tc_det_head_main2.Width = 1000;

        Table tbldetail = new Table();
        tbldetail.BorderStyle = BorderStyle.Solid;
        tbldetail.BorderWidth = 1;
        tbldetail.GridLines = GridLines.Both;
        tbldetail.Width = 1000;
        tbldetail.Style.Add("border-collapse", "collapse");
        tbldetail.Style.Add("border", "solid 1px Black");

        dsdoc = dc.get_dcr_Doctor_Detail_View(sf_code, cmonth, cyear); //1-Listed Doctor
        iCount = 0;
        if (dsdoc.Tables[0].Rows.Count > 0)
        {

            //---------------------------------------------------

            Table tbldetail_main3 = new Table();
            tbldetail_main3.BorderStyle = BorderStyle.None;
            tbldetail_main3.Width = 1100;

            TableRow tr_det_head_main3 = new TableRow();
            TableCell tc_det_head_main3 = new TableCell();
            tc_det_head_main3.Width = 100;
            Literal lit_det_main3 = new Literal();
            lit_det_main3.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            tc_det_head_main3.Controls.Add(lit_det_main3);
            tr_det_head_main3.Cells.Add(tc_det_head_main3);

            TableCell tc_det_head_main4 = new TableCell();
            tc_det_head_main4.Width = 1000;

            Table tbl = new Table();
            tbl.Width = 1000;

            TableRow tr_ff = new TableRow();
            TableCell tc_ff_name = new TableCell();
            tc_ff_name.BorderStyle = BorderStyle.None;
            tc_ff_name.Width = 500;
            Literal lit_ff_name = new Literal();
            lit_ff_name.Text = "<b>Field Force Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_Name.ToString();
            tc_ff_name.Controls.Add(lit_ff_name);
            tr_ff.Cells.Add(tc_ff_name);

            TableCell tc_HQ = new TableCell();
            tc_HQ.BorderStyle = BorderStyle.None;
            tc_HQ.Width = 500;
            tc_HQ.HorizontalAlign = HorizontalAlign.Right;
            Literal lit_HQ = new Literal();
            lit_HQ.Text = "<b>Head Quarters</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_HQ.ToString();
            tc_HQ.Controls.Add(lit_HQ);
            tr_ff.Cells.Add(tc_HQ);

            tbl.Rows.Add(tr_ff);

            TableRow tr_dcr = new TableRow();
            tbl.Rows.Add(tr_dcr);

            tc_det_head_main4.Controls.Add(tbl);
            tr_det_head_main3.Cells.Add(tc_det_head_main4);
            tbldetail_main3.Rows.Add(tr_det_head_main3);

            form1.Controls.Add(tbldetail_main3);

            //-----------------------------------------------------------------------------

            TableRow tr_det_head = new TableRow();
            TableCell tc_det_head_SNo = new TableCell();
            tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_head_SNo.BorderWidth = 0;
            tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_det_head_SNo = new Literal();
            lit_det_head_SNo.Text = "<b>S.No</b>";
            tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
            tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
            tr_det_head.Cells.Add(tc_det_head_SNo);

            TableCell tc_det_head_Ses = new TableCell();
            tc_det_head_Ses.BorderStyle = BorderStyle.Solid;
            tc_det_head_Ses.BorderWidth = 1;
            tc_det_head_Ses.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_det_head_Ses = new Literal();
            lit_det_head_Ses.Text = "<b>Listed Doctor Name</b>";
            tc_det_head_Ses.Attributes.Add("Class", "tr_det_head");
            tc_det_head_Ses.Controls.Add(lit_det_head_Ses);
            tr_det_head.Cells.Add(tc_det_head_Ses);

            TableCell tc_det_head_doc = new TableCell();
            tc_det_head_doc.BorderStyle = BorderStyle.Solid;
            tc_det_head_doc.BorderWidth = 1;
            tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_det_head_doc = new Literal();
            lit_det_head_doc.Text = "<b>Specialty</b>";
            tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
            tc_det_head_doc.Controls.Add(lit_det_head_doc);
            tr_det_head.Cells.Add(tc_det_head_doc);

            TableCell tc_det_head_Category = new TableCell();
            tc_det_head_Category.BorderStyle = BorderStyle.Solid;
            tc_det_head_Category.BorderWidth = 1;
            tc_det_head_Category.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_det_head_Category = new Literal();
            lit_det_head_Category.Text = "<b>Category</b>";
            tc_det_head_Category.Attributes.Add("Class", "tr_det_head");
            tc_det_head_Category.Controls.Add(lit_det_head_Category);
            tr_det_head.Cells.Add(tc_det_head_Category);

            TableCell tc_det_head_Qual = new TableCell();
            tc_det_head_Qual.BorderStyle = BorderStyle.Solid;
            tc_det_head_Qual.BorderWidth = 1;
            tc_det_head_Qual.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_det_head_Qual = new Literal();
            lit_det_head_Qual.Text = "<b>Qualification</b>";
            tc_det_head_Qual.Attributes.Add("Class", "tr_det_head");
            tc_det_head_Qual.Controls.Add(lit_det_head_Qual);
            tr_det_head.Cells.Add(tc_det_head_Qual);

            TableCell tc_det_head_Class = new TableCell();
            tc_det_head_Class.BorderStyle = BorderStyle.Solid;
            tc_det_head_Class.BorderWidth = 1;
            tc_det_head_Class.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_det_head_Class = new Literal();
            lit_det_head_Class.Text = "<b>Class</b>";
            tc_det_head_Class.Attributes.Add("Class", "tr_det_head");
            tc_det_head_Class.Controls.Add(lit_det_head_Class);
            tr_det_head.Cells.Add(tc_det_head_Class);

            TableCell tc_det_head_ww = new TableCell();
            tc_det_head_ww.BorderStyle = BorderStyle.Solid;
            tc_det_head_ww.BorderWidth = 1;
            tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_det_head_ww = new Literal();
            // lit_det_head_ww.Text = "<b>Territory</b>";
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            lit_det_head_ww.Text = "<b>" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "</b>";
            tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
            tc_det_head_ww.Controls.Add(lit_det_head_ww);
            tr_det_head.Cells.Add(tc_det_head_ww);

            tbldetail.Rows.Add(tr_det_head);

            iCount = 0;

            foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
            {
                TableRow tr_det_sno = new TableRow();

                TableCell tc_det_SNo = new TableCell();
                iCount += 1;
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.Attributes.Add("Class", "tbldetail_main");
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det_sno.Cells.Add(tc_det_SNo);

                TableCell tc_det_dr_name = new TableCell();
                Literal lit_det_dr_name = new Literal();
                lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["ListedDr_Name"].ToString();
                tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                tc_det_dr_name.Attributes.Add("Class", "tbldetail_main");
                tc_det_dr_name.BorderWidth = 1;
                tc_det_dr_name.Controls.Add(lit_det_dr_name);
                tr_det_sno.Cells.Add(tc_det_dr_name);

                TableCell tc_det_time = new TableCell();
                Literal lit_det_time = new Literal();
                lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Special_Name"].ToString();
                tc_det_time.BorderStyle = BorderStyle.Solid;
                tc_det_time.Attributes.Add("Class", "tbldetail_main");
                tc_det_time.BorderWidth = 1;
                tc_det_time.Controls.Add(lit_det_time);
                tr_det_sno.Cells.Add(tc_det_time);

                TableCell tc_det_Category = new TableCell();
                Literal lit_det_Category = new Literal();
                lit_det_Category.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Cat_Name"].ToString();
                tc_det_Category.BorderStyle = BorderStyle.Solid;
                tc_det_Category.Attributes.Add("Class", "tbldetail_main");
                tc_det_Category.BorderWidth = 1;
                tc_det_Category.Controls.Add(lit_det_Category);
                tr_det_sno.Cells.Add(tc_det_Category);

                TableCell tc_det_Qual = new TableCell();
                Literal lit_det_Qual = new Literal();
                lit_det_Qual.Text = "&nbsp;&nbsp;" + drdoctor["Doc_QuaName"].ToString();
                tc_det_Qual.BorderStyle = BorderStyle.Solid;
                tc_det_Qual.Attributes.Add("Class", "tbldetail_main");
                tc_det_Qual.BorderWidth = 1;
                tc_det_Qual.Controls.Add(lit_det_Qual);
                tr_det_sno.Cells.Add(tc_det_Qual);

                TableCell tc_det_Class = new TableCell();
                HyperLink Hyllit_det_Class = new HyperLink();
                Hyllit_det_Class.Text = "&nbsp;&nbsp;" + drdoctor["Doc_ClsName"].ToString();
                tc_det_Class.BorderStyle = BorderStyle.Solid;
                tc_det_Class.HorizontalAlign = HorizontalAlign.Left;
                tc_det_Class.Attributes.Add("Class", "tbldetail_main");
                tc_det_Class.BorderWidth = 1;
                tc_det_Class.Controls.Add(Hyllit_det_Class);
                tr_det_sno.Cells.Add(tc_det_Class);

                TableCell tc_det_work = new TableCell();
                Literal lit_det_work = new Literal();
                lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Territory_Name"].ToString();
                tc_det_work.BorderStyle = BorderStyle.Solid;
                tc_det_work.Attributes.Add("Class", "tbldetail_main");
                tc_det_work.BorderWidth = 1;
                tc_det_work.Controls.Add(lit_det_work);
                tr_det_sno.Cells.Add(tc_det_work);

                tbldetail.Rows.Add(tr_det_sno);

                tc_det_head_main2.Controls.Add(tbldetail);
                tr_det_head_main.Cells.Add(tc_det_head_main2);
                tbldetail_main.Rows.Add(tr_det_head_main);

                form1.Controls.Add(tbldetail_main);

            }
        }
        else
        {
            pnlbutton.Visible = true;

            Table tbldetail_mainEmpty = new Table();
            tbldetail_mainEmpty.BorderStyle = BorderStyle.None;
            tbldetail_mainEmpty.Width = 1100;
            TableRow tr_det_head_mainEmpty = new TableRow();

            TableCell tc_det_head_mainEmpty = new TableCell();
            tc_det_head_mainEmpty.Width = 100;
            Literal lit_det_mainEmpty = new Literal();
            lit_det_mainEmpty.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            tbldetail_mainEmpty.Style.Add("margin-top", "110px");
            tc_det_head_mainEmpty.Controls.Add(lit_det_mainEmpty);
            tr_det_head_mainEmpty.Cells.Add(tc_det_head_mainEmpty);

            TableCell tc_det_head_main = new TableCell();
            tc_det_head_main.Width = 800;

            Table tbldetailEmpty = new Table();
            tbldetailEmpty.BorderStyle = BorderStyle.Solid;
            tbldetailEmpty.BorderWidth = 1;
            tbldetailEmpty.GridLines = GridLines.Both;
            tbldetailEmpty.Width = 1000;
            tbldetailEmpty.Style.Add("border-collapse", "collapse");
            tbldetailEmpty.Style.Add("border", "solid 1px Black");
            tbldetailEmpty.Style.Add("margin-left", "200px");

            TableRow tr_det_Empty = new TableRow();
            TableCell tc_det_Empty = new TableCell();
            iCount += 1;
            Literal lit_det_Empty = new Literal();
            lit_det_Empty.Text = "No Record Found";
            tc_det_Empty.BorderStyle = BorderStyle.Solid;
            tc_det_Empty.Attributes.Add("Class", "NoRecord");

            tc_det_Empty.HorizontalAlign = HorizontalAlign.Center;
            tc_det_Empty.BorderWidth = 1;
            tc_det_Empty.BorderStyle = BorderStyle.None;
            tc_det_Empty.Controls.Add(lit_det_Empty);
            tr_det_Empty.Cells.Add(tc_det_Empty);

            tbldetailEmpty.Rows.Add(tr_det_Empty);

            tc_det_head_mainEmpty.Controls.Add(tbldetailEmpty);
            tr_det_head_mainEmpty.Cells.Add(tc_det_head_mainEmpty);
            tbldetail_mainEmpty.Rows.Add(tr_det_head_mainEmpty);

            form1.Controls.Add(tbldetail_mainEmpty);
        }



    }
    //girip
    private void CreateDynamicDCRDetailedView(string Fdate, string Tdate, string sf_code)
    {
        try
        {

            DCR dc = new DCR();
            //dsDCR = dc.get_dcr_date(sf_code, imonth, iyear);
            dsDCR = dc.get_dcr_DCRPendingdate_DCRDetail(sf_code, Fdate, Tdate);
            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                DCR dcsf = new DCR();
                dssf = dcsf.getSfName_HQ(sf_code);

                if (dssf.Tables[0].Rows.Count > 0)
                {
                    Sf_Name = dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Sf_HQ = dssf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }

                Table tbldetail_main3 = new Table();
                tbldetail_main3.BorderStyle = BorderStyle.None;
                tbldetail_main3.Width = 1100;

                TableRow tr_det_head_main3 = new TableRow();
                TableCell tc_det_head_main3 = new TableCell();
                tc_det_head_main3.Width = 100;
                Literal lit_det_main3 = new Literal();
                lit_det_main3.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tc_det_head_main3.Controls.Add(lit_det_main3);
                tr_det_head_main3.Cells.Add(tc_det_head_main3);

                TableCell tc_det_head_main4 = new TableCell();
                tc_det_head_main4.Width = 1000;

                Table tbl = new Table();
                tbl.Width = 1000;

                TableRow tr_ff = new TableRow();
                TableCell tc_ff_name = new TableCell();
                tc_ff_name.BorderStyle = BorderStyle.None;
                tc_ff_name.Width = 500;
                Literal lit_ff_name = new Literal();
                lit_ff_name.Text = "<b>Field Force Name</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_Name.ToString();
                tc_ff_name.Controls.Add(lit_ff_name);
                tr_ff.Cells.Add(tc_ff_name);

                TableCell tc_HQ = new TableCell();
                tc_HQ.BorderStyle = BorderStyle.None;
                tc_HQ.Width = 500;
                tc_HQ.HorizontalAlign = HorizontalAlign.Right;
                Literal lit_HQ = new Literal();
                lit_HQ.Text = "<b>Head Quarters</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Sf_HQ.ToString();
                tc_HQ.Controls.Add(lit_HQ);
                tr_ff.Cells.Add(tc_HQ);

                tbl.Rows.Add(tr_ff);

                TableRow tr_dcr = new TableRow();
                tbl.Rows.Add(tr_dcr);

                tc_det_head_main4.Controls.Add(tbl);
                tr_det_head_main3.Cells.Add(tc_det_head_main4);
                tbldetail_main3.Rows.Add(tr_det_head_main3);

                form1.Controls.Add(tbldetail_main3);

                Table tbl_head_empty = new Table();
                TableRow tr_head_empty = new TableRow();
                TableCell tc_head_empty = new TableCell();
                Literal lit_head_empty = new Literal();
                lit_head_empty.Text = "<BR>";
                tc_head_empty.Controls.Add(lit_head_empty);
                tr_head_empty.Cells.Add(tc_head_empty);
                tbl_head_empty.Rows.Add(tr_head_empty);
                form1.Controls.Add(tbl_head_empty);

                Table tbldetail_main = new Table();
                tbldetail_main.BorderStyle = BorderStyle.None;
                tbldetail_main.GridLines = GridLines.Both;
                tbldetail_main.Width = 1000;
                //tbldetail_main.Style.Add("border-collapse", "collapse");
                //tbldetail_main.Style.Add("border", "solid 1px Black");
                tbldetail_main.Style.Add("margin-left", "100px");
                TableRow tr_det_head_main = new TableRow();
                //TableCell tc_det_head_main = new TableCell();
                //tc_det_head_main.Width = 100;
                //Literal lit_det_main = new Literal();
                //lit_det_main.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                //tc_det_head_main.Controls.Add(lit_det_main);
                //tr_det_head_main.Cells.Add(tc_det_head_main);                
                TableCell tc_det_head_main2 = new TableCell();
                tc_det_head_main2.Width = 1000;

                Table tbldetail = new Table();
                tbldetail.BorderStyle = BorderStyle.Solid;
                tbldetail.BorderWidth = 1;
                tbldetail.GridLines = GridLines.Both;
                tbldetail.Width = 1000;
                tbldetail.Style.Add("border-collapse", "collapse");
                tbldetail.Style.Add("border", "solid 1px Black");

                if (sf_code.Contains("MR"))
                {
                    dsdoc = dc.get_DCRView_Approved_All_Dates(sf_code, Fdate.ToString(), Tdate.ToString()); //1-Listed Doctor
                }
                else
                {
                    dsdoc = dc.get_DCRView_Approved_MGR_All_Dates(sf_code, Fdate.ToString(), Tdate.ToString()); //1-Listed Doctor
                }
                iCount = 0;
                if (dsdoc.Tables[0].Rows.Count > 0)
                {
                    TableRow tr_det_head = new TableRow();
                    TableCell tc_det_head_SNo = new TableCell();
                    tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_head_SNo.BorderWidth = 0;
                    tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_SNo = new Literal();
                    lit_det_head_SNo.Text = "<b>Date</b>";
                    //tc_det_head_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#4DB8FF");
                    //tc_det_head_SNo.Style.Add("color", "White");
                    //tc_det_head_SNo.Style.Add("font-size", "10pt");
                    //tc_det_head_SNo.Style.Add("font-weight", "bold");
                    //tc_det_head_SNo.Style.Add("font-family", "Calibri");
                    tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                    tr_det_head.Cells.Add(tc_det_head_SNo);

                    TableCell tc_det_head_Ses = new TableCell();
                    tc_det_head_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_head_Ses.BorderWidth = 1;
                    tc_det_head_Ses.HorizontalAlign = HorizontalAlign.Center;
                    //tc_det_head_Ses.Visible = false;
                    Literal lit_det_head_Ses = new Literal();
                    // lit_det_head_Ses.Text = "<b>Territory Worked</b>";
                    Territory terr = new Territory();
                    dsTerritory = terr.getWorkAreaName(div_code);
                    lit_det_head_Ses.Text = "<b>" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "</b>";
                    tc_det_head_Ses.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_Ses.Controls.Add(lit_det_head_Ses);
                    tr_det_head.Cells.Add(tc_det_head_Ses);

                    TableCell tc_det_head_doc = new TableCell();
                    tc_det_head_doc.BorderStyle = BorderStyle.Solid;
                    tc_det_head_doc.BorderWidth = 1;
                    tc_det_head_doc.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_doc = new Literal();
                    lit_det_head_doc.Text = "<b>Sub.Date</b>";
                    tc_det_head_doc.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_doc.Controls.Add(lit_det_head_doc);
                    tr_det_head.Cells.Add(tc_det_head_doc);

                    TableCell tc_det_head_time = new TableCell();
                    tc_det_head_time.BorderStyle = BorderStyle.Solid;
                    tc_det_head_time.BorderWidth = 1;
                    tc_det_head_time.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_time = new Literal();
                    lit_det_head_time.Text = "<b>Work Type</b>";
                    tc_det_head_time.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_time.Controls.Add(lit_det_head_time);
                    tr_det_head.Cells.Add(tc_det_head_time);

                    TableCell tc_det_head_ww = new TableCell();
                    tc_det_head_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_head_ww.BorderWidth = 1;
                    tc_det_head_ww.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_ww = new Literal();
                    lit_det_head_ww.Text = "<b>Worked With</b>";
                    tc_det_head_ww.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                    tr_det_head.Cells.Add(tc_det_head_ww);

                    TableCell tc_det_head_visit = new TableCell();
                    tc_det_head_visit.BorderStyle = BorderStyle.Solid;
                    tc_det_head_visit.BorderWidth = 1;
                    tc_det_head_visit.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_visit = new Literal();
                    lit_det_head_visit.Text = "<b>Retailer(s) <br> visited</b>";
                    tc_det_head_visit.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_visit.Controls.Add(lit_det_head_visit);
                    tr_det_head.Cells.Add(tc_det_head_visit);

                    TableCell tc_det_prod = new TableCell();
                    tc_det_prod.BorderStyle = BorderStyle.Solid;
                    Literal lit_det_prod = new Literal();



                    Territory terr1 = new Territory();
                    dsTerritory = terr1.getProdName(div_code);
                    foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                    {
                        TableCell tc = new TableCell();
                        tc.BorderStyle = BorderStyle.Solid;
                        tc.BorderWidth = 1;
                        tc.HorizontalAlign = HorizontalAlign.Center;
                        Literal lic = new Literal();
                        if (div_code == "11" || div_code == "13" || div_code == "8")
                        {
                            lic.Text = "<b>" + pro1["Product_Detail_Name"].ToString() + "</b>";
                        }
                        else
                        {
                            lic.Text = "<b>" + pro1["Product_Short_Name"].ToString() + "</b>";
                        }
                        tc.Attributes.Add("Class", "tr_det_head");
                        tc.Controls.Add(lic);
                        tr_det_head.Cells.Add(tc);

                    }




                    TableCell tc_det_head_prod = new TableCell();
                    tc_det_head_prod.BorderStyle = BorderStyle.Solid;
                    tc_det_head_prod.BorderWidth = 1;
                    tc_det_head_prod.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_prod = new Literal();
                    lit_det_head_prod.Text = "<b>Order Value</b>";
                    tc_det_head_prod.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_prod.Controls.Add(lit_det_head_prod);
                    tr_det_head.Cells.Add(tc_det_head_prod);

                    TableCell tc_det_head_gift = new TableCell();
                    tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                    tc_det_head_gift.BorderWidth = 1;
                    tc_det_head_gift.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_gift = new Literal();
                    lit_det_head_gift.Text = "<b>Net Weight</b>";
                    tc_det_head_gift.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_gift.Controls.Add(lit_det_head_gift);
                    tr_det_head.Cells.Add(tc_det_head_gift);

                    TableCell tc_det_head_catg = new TableCell();
                    tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                    tc_det_head_catg.BorderWidth = 1;
                    tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_catg = new Literal();
                    lit_det_head_catg.Text = "<b>TC</b>";
                    tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_catg.Controls.Add(lit_det_head_catg);
                    tr_det_head.Cells.Add(tc_det_head_catg);

                    TableCell tc_det_head_POB = new TableCell();
                    tc_det_head_POB.BorderStyle = BorderStyle.Solid;
                    tc_det_head_POB.BorderWidth = 1;
                    tc_det_head_POB.HorizontalAlign = HorizontalAlign.Center;
                    //tc_det_head_POB.Visible = false;
                    Literal lit_det_head_spec = new Literal();
                    lit_det_head_spec.Text = "<b>EC</b>";
                    tc_det_head_POB.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_POB.Controls.Add(lit_det_head_spec);
                    tr_det_head.Cells.Add(tc_det_head_POB);

                    TableCell tc_det_head_prod1 = new TableCell();
                    tc_det_head_prod1.BorderStyle = BorderStyle.Solid;
                    tc_det_head_prod1.BorderWidth = 1;
                    tc_det_head_prod1.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_prod1 = new Literal();
                    lit_det_head_prod1.Text = "<b>Stock</b>";
                    tc_det_head_prod1.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_prod1.Controls.Add(lit_det_head_prod1);
                    tr_det_head.Cells.Add(tc_det_head_prod1);

                    //TableCell tc_det_head_gift = new TableCell();
                    //tc_det_head_gift.BorderStyle = BorderStyle.Solid;
                    //tc_det_head_gift.BorderWidth = 1;
                    //tc_det_head_gift.HorizontalAlign = HorizontalAlign.Center;
                    //Literal lit_det_head_gift = new Literal();
                    //lit_det_head_gift.Text = "<b>Non Listed <br> Dr(s)Met</b>";
                    //tc_det_head_gift.Attributes.Add("Class", "tr_det_head");
                    //tc_det_head_gift.Controls.Add(lit_det_head_gift);
                    //tr_det_head.Cells.Add(tc_det_head_gift);

                    tbldetail.Rows.Add(tr_det_head);

                    iCount = 0;
                    iFieldWrkCount = 0;
                    int iTotLstCal = 0;
                    int iTotChemPOB = 0;
                    int iTotChemCal = 0;
                    int iTotStockCal = 0;
                    int iTotUnLstCal = 0;
                    int isum = 0;
                    int isumChemPOB = 0;
                    int isumChem = 0;
                    int isumStock = 0;
                    int isumUnLst = 0;


                    int[] na = new int[500];
                    //int i = 0;

                    foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                    {
                        strDelay = "";
                        TableRow tr_det_sno = new TableRow();
                        TableCell tc_det_SNo = new TableCell();
                        iCount += 1;
                        Literal lit_det_SNo = new Literal();
                        lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                        tc_det_SNo.Visible = false;
                        tc_det_SNo.BorderStyle = BorderStyle.Solid;
                        //tc_det_SNo.BorderWidth = 1;
                        tc_det_SNo.Controls.Add(lit_det_SNo);
                        tr_det_sno.Cells.Add(tc_det_SNo);

                        TableCell tc_det_Ses = new TableCell();
                        HyperLink lit_det_Ses = new HyperLink();
                        lit_det_Ses.Text = drdoctor["Activity_Date"].ToString();
                        tc_det_Ses.Attributes.Add("Class", "tbldetail_main");

                        tc_det_Ses.BorderStyle = BorderStyle.Solid;
                        tc_det_Ses.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_Ses.VerticalAlign = VerticalAlign.Middle;
                        //tc_det_Ses.BorderWidth = 1;
                        tc_det_Ses.Controls.Add(lit_det_Ses);
                        tr_det_sno.Cells.Add(tc_det_Ses);

                        TableCell tc_det_dr_name = new TableCell();
                        Literal lit_det_dr_name = new Literal();
                        //tc_det_dr_name.Visible = false;
                        if (drdoctor["che_POB_Name"].ToString() != "[]")
                        {
                            lit_det_dr_name.Text = "&nbsp;&nbsp;" + drdoctor["che_POB_Name"].ToString();
                        }
                        tc_det_dr_name.BorderStyle = BorderStyle.Solid;
                        tc_det_dr_name.Attributes.Add("Class", "tbldetail_main");
                        tc_det_dr_name.BorderWidth = 1;
                        tc_det_dr_name.Controls.Add(lit_det_dr_name);
                        tr_det_sno.Cells.Add(tc_det_dr_name);

                        TableCell tc_det_time = new TableCell();
                        Literal lit_det_time = new Literal();
                        lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor["Submission_Date"].ToString();
                        tc_det_time.BorderStyle = BorderStyle.Solid;
                        tc_det_time.Attributes.Add("Class", "tbldetail_main");
                        //tc_det_time.BorderWidth = 1;
                        tc_det_time.Controls.Add(lit_det_time);
                        tr_det_sno.Cells.Add(tc_det_time);

                        string strWorktypeName = "";

                        if (sf_code.Contains("MR"))
                        {
                            strWorktypeName = drdoctor["Worktype_Name_B"].ToString();
                        }
                        else
                        {
                            strWorktypeName = drdoctor["Worktype_Name_M"].ToString();
                        }

                        DataSet dsDelay = new DataSet();

                        dsDelay = dc.get_DCR_Status_Delay_DCRView(drdoctor["sf_code"].ToString(), drdoctor["Activity_Date"].ToString(), Fdate.ToString(), Tdate.ToString());
                        if (dsDelay.Tables[0].Rows.Count == 0 || strWorktypeName == "Field Work")
                        {
                            if ((strWorktypeName != "Holiday" && strWorktypeName != "Meeting" && strWorktypeName != "Weekly Off" && strWorktypeName != "Transit" && strWorktypeName != "Leave" && strWorktypeName != "Camp Work"))
                            {
                                iFieldWrkCount += 1;
                                sURL = "rptDCRViewApprovedDetails.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&div_code=" + div_code + " &Day=" + lit_det_Ses.Text + "";

                                lit_det_Ses.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                lit_det_Ses.NavigateUrl = "#";
                                lit_det_Ses.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0052cc");
                                TableCell tc_det_work = new TableCell();
                                Literal lit_det_work = new Literal();

                                dsDelay = dc.get_DCR_Status_Delay_DCRView(drdoctor["sf_code"].ToString(), drdoctor["Activity_Date"].ToString(), Fdate.ToString(), Tdate.ToString());
                                if (dsDelay.Tables[0].Rows.Count > 0)
                                {
                                    strDelay = "<span style='color:red;font-family:Verdana'> " + dsDelay.Tables[0].Rows[0][0].ToString() + " ";
                                }

                                if (drdoctor["Temp"].ToString() == "1")
                                {
                                    strDelay = "<span style='color:red;font-family:Verdana'> ( " + "Approval Pending" + " ) </span>";
                                }
                                else if (drdoctor["Temp"].ToString() == "2")
                                {
                                    strDelay = "<span style='color:red;font-family:Verdana'> ( " + "Disapproved" + " ) </span>";
                                }
                                else if (drdoctor["Temp"].ToString() == "3")
                                {
                                    strDelay = "<span style='color:red;font-family:Verdana'> ( " + "Edit - ReEntry" + " ) </span>";
                                }

                                if (sf_code.Contains("MR"))
                                {
                                    lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_B"].ToString() + strDelay;
                                }
                                else
                                {
                                    lit_det_work.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_M"].ToString() + strDelay;
                                }
                                tc_det_work.BorderStyle = BorderStyle.Solid;
                                tc_det_work.Attributes.Add("Class", "tbldetail_main");
                                tc_det_work.Width = 180;
                                //tc_det_work.BorderWidth = 1;
                                tc_det_work.Controls.Add(lit_det_work);
                                tr_det_sno.Cells.Add(tc_det_work);

                                TableCell tc_det_lvisit = new TableCell();
                                Literal lit_det_lvisit = new Literal();
                                lit_det_lvisit.Text = " "; // drdoctor["lvisit"].ToString();
                                tc_det_lvisit.BorderStyle = BorderStyle.Solid;
                                tc_det_lvisit.Attributes.Add("Class", "tbldetail_main");
                                //tc_det_lvisit.BorderWidth = 1;
                                tc_det_lvisit.Controls.Add(lit_det_lvisit);
                                tr_det_sno.Cells.Add(tc_det_lvisit);

                                TableCell tc_det_spec = new TableCell();
                                HyperLink Hyllit_det_spec = new HyperLink();
                                Hyllit_det_spec.Text = drdoctor["doc_cnt"].ToString();
                                if (Hyllit_det_spec.Text != "0")
                                {
                                    sURL = "rptDCRViewCompleted.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 1 + "";

                                    Hyllit_det_spec.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                    Hyllit_det_spec.NavigateUrl = "#";
                                }
                                tc_det_spec.BorderStyle = BorderStyle.Solid;
                                tc_det_spec.HorizontalAlign = HorizontalAlign.Center;
                                tc_det_spec.Attributes.Add("Class", "tbldetail_main");
                                //tc_det_spec.BorderWidth = 1;
                                tc_det_spec.Controls.Add(Hyllit_det_spec);
                                tr_det_sno.Cells.Add(tc_det_spec);

                                iTotLstCal += Convert.ToInt16(Hyllit_det_spec.Text);


                                string Activity_date3 = string.Empty;
                                string datet3 = string.Empty;
                                string hdate3 = string.Empty;
                                Activity_date3 = drdoctor["Submission_Date"].ToString();
                                datet3 = Activity_date3.Trim();
                                DateTime dtt3 = Convert.ToDateTime(datet3);
                                hdate3 = dtt3.ToString("yyyy-MM-dd");

                                DCR sf = new DCR();
                                dcrcou = sf.dcr_Gettransno_d(sf_code, div_code, hdate3);
                                int[] fi = new int[500];
                                int j = 0;
                                foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                                {
                                    string pCode = pro1["Product_Detail_Code"].ToString();
                                    var DSRows = (from w in dcrcou.Tables[0].AsEnumerable() where w.Field<string>("Product_Code") == pCode select w);
                                    string sQty = string.Empty;
                                    foreach (var prd in DSRows)
                                    {
                                        sQty = prd.Field<string>("Qty");
                                        //Tot_Sec += Decimal.Parse(prd.Field<string>("Qty"));
                                    }
                                    TableCell tc = new TableCell();
                                    tc.BorderStyle = BorderStyle.Solid;
                                    tc.BorderWidth = 1;
                                    tc.HorizontalAlign = HorizontalAlign.Center;
                                    Literal lic = new Literal();
                                    lic.Text = sQty;

                                    tc.Attributes.Add("Class", "tbldetail_Data");
                                    tc.Controls.Add(lic);
                                    tr_det_sno.Cells.Add(tc);
                                    fi[j] += sQty == "" ? 0 : Convert.ToInt32(sQty);
                                    j++;
                                }
                                for (int l = 0; l < dsTerritory.Tables[0].Rows.Count; l++)
                                {
                                    na[l] += Convert.ToInt32(fi[l]);
                                }

                                Tot_Sec = 0;



                                TableCell tc_tot_monthd1 = new TableCell();
                                Literal hyp_monthd1 = new Literal();
                                hyp_monthd1.Text = drdoctor["POB_Value"].ToString();
                                string ovv = hyp_monthd1.Text;
                                decimal ov;
                                if (ovv != "0" && ovv != "")
                                {
                                    ov = Convert.ToDecimal(ovv);
                                    detorderval += ov;
                                }
                                tc_tot_monthd1.BorderStyle = BorderStyle.Solid;
                                tc_tot_monthd1.BorderWidth = 1;
                                tc_tot_monthd1.BackColor = System.Drawing.Color.White;
                                tc_tot_monthd1.Width = 200;
                                tc_tot_monthd1.Style.Add("font-family", "Calibri");
                                tc_tot_monthd1.Style.Add("font-size", "10pt");
                                tc_tot_monthd1.HorizontalAlign = HorizontalAlign.Center;
                                tc_tot_monthd1.VerticalAlign = VerticalAlign.Middle;
                                tc_tot_monthd1.Controls.Add(hyp_monthd1);
                                tc_tot_monthd1.Attributes.Add("style", "font-weight:bold;");
                                tc_tot_monthd1.Attributes.Add("Class", "rptCellBorder");
                                tr_det_sno.Cells.Add(tc_tot_monthd1);



                                TableCell tc_tot_monthd = new TableCell();
                                Literal hyp_monthd = new Literal();
                                hyp_monthd.Text = drdoctor["net_weight_value"].ToString();
                                string nvv = hyp_monthd.Text;
                                decimal nv;
                                if (nvv != "0" && nvv != "")
                                {
                                    nv = Convert.ToDecimal(nvv);
                                    detnetval += nv;
                                }
                                tc_tot_monthd.BorderStyle = BorderStyle.Solid;
                                tc_tot_monthd.BorderWidth = 1;
                                tc_tot_monthd.BackColor = System.Drawing.Color.White;
                                tc_tot_monthd.Width = 200;
                                tc_tot_monthd.Style.Add("font-family", "Calibri");
                                tc_tot_monthd.Style.Add("font-size", "10pt");
                                tc_tot_monthd.HorizontalAlign = HorizontalAlign.Center;
                                tc_tot_monthd.VerticalAlign = VerticalAlign.Middle;
                                tc_tot_monthd.Controls.Add(hyp_monthd);
                                tc_tot_monthd.Attributes.Add("style", "font-weight:bold;");
                                tc_tot_monthd.Attributes.Add("Class", "rptCellBorder");
                                tr_det_sno.Cells.Add(tc_tot_monthd);

                                TableCell tc_det_prod1 = new TableCell();
                                HyperLink hyllit_det_prod = new HyperLink();
                                DCR sf6 = new DCR();
                                dsDrr = sf6.dcr_Getcount(sf_code, div_code, drdoctor["Submission_Date"].ToString());
                                foreach (DataRow coun in dsDrr.Tables[0].Rows)
                                {


                                    hyllit_det_prod.Text = coun["Total_call_count"].ToString();
                                    if (hyllit_det_prod.Text != "0")
                                    {
                                        //sURL = "rptDCRViewCompleted.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 2 + "";
                                        //hyllit_det_prod.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                        //hyllit_det_prod.NavigateUrl = "#";

                                    }
                                    tc_det_prod1.BorderStyle = BorderStyle.Solid;
                                    tc_det_prod1.HorizontalAlign = HorizontalAlign.Center;
                                    tc_det_prod1.Attributes.Add("Class", "tbldetail_main");
                                    tc_det_prod1.BorderWidth = 1;
                                    tc_det_prod1.Controls.Add(hyllit_det_prod);
                                    tr_det_sno.Cells.Add(tc_det_prod1);

                                    iTotChemCal += Convert.ToInt16(hyllit_det_prod.Text);

                                    TableCell tc_det_Che_POB = new TableCell();
                                    Literal lit_det_Che_POB = new Literal();

                                    if (coun["Effictive_call_Count"].ToString().ToString() != "")
                                    {
                                        lit_det_Che_POB.Text = coun["Effictive_call_Count"].ToString().ToString();
                                    }
                                    else
                                    {
                                        lit_det_Che_POB.Text = "0";
                                    }
                                    tc_det_Che_POB.BorderStyle = BorderStyle.Solid;
                                    tc_det_Che_POB.Attributes.Add("Class", "tbldetail_main");
                                    //tc_det_head_POB.Visible = false;
                                    tc_det_Che_POB.HorizontalAlign = HorizontalAlign.Center;
                                    tc_det_Che_POB.BorderWidth = 1;
                                    tc_det_Che_POB.Controls.Add(lit_det_Che_POB);
                                    tr_det_sno.Cells.Add(tc_det_Che_POB);

                                    iTotChemPOB += Convert.ToInt32(lit_det_Che_POB.Text);
                                }
                                TableCell tc_det_gift = new TableCell();
                                HyperLink hyllit_det_gift = new HyperLink();
                                hyllit_det_gift.Text = dcrcou.Tables[0].Rows.Count.ToString();
                                if (hyllit_det_gift.Text != "0")
                                {
                                    //sURL = "rptDCRViewCompleted.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 3 + "";

                                    //hyllit_det_gift.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                    //hyllit_det_gift.NavigateUrl = "#";
                                }
                                tc_det_gift.BorderStyle = BorderStyle.Solid;
                                tc_det_gift.HorizontalAlign = HorizontalAlign.Center;
                                tc_det_gift.Attributes.Add("Class", "tbldetail_main");
                                tc_det_gift.BorderWidth = 1;
                                tc_det_gift.Controls.Add(hyllit_det_gift);
                                tr_det_sno.Cells.Add(tc_det_gift);

                                iTotStockCal += Convert.ToInt16(hyllit_det_gift.Text);

                                //TableCell tc_det_UnDoc = new TableCell();
                                //HyperLink hyllit_det_UnDoc = new HyperLink();
                                //hyllit_det_UnDoc.Text = drdoctor["Undoc_cnt"].ToString();
                                //if (hyllit_det_UnDoc.Text != "0")
                                //{
                                //    sURL = "rptDCRViewCompleted.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 4 + "";

                                //    hyllit_det_UnDoc.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                //    hyllit_det_UnDoc.NavigateUrl = "#";
                                //}

                                //tc_det_UnDoc.BorderStyle = BorderStyle.Solid;
                                //tc_det_UnDoc.HorizontalAlign = HorizontalAlign.Center;
                                //tc_det_UnDoc.Attributes.Add("Class", "tbldetail_main");
                                ////tc_det_UnDoc.BorderWidth = 1;
                                //tc_det_UnDoc.Controls.Add(hyllit_det_UnDoc);
                                //tr_det_sno.Cells.Add(tc_det_UnDoc);
                                //iTotUnLstCal += Convert.ToInt16(hyllit_det_UnDoc.Text);

                            }
                            else
                            {
                                TableCell tc_det_NonFwk = new TableCell();
                                Literal lit_det_NonFwk = new Literal();

                                if (drdoctor["Temp"].ToString() == "1")
                                {
                                    strDelay = "<span style='color:red;font-family:Verdana'> ( " + "Approval Pending" + " ) </span>";
                                }
                                else if (drdoctor["Temp"].ToString() == "2")
                                {
                                    strDelay = "<span style='color:red;font-family:Verdana'> ( " + "Disapproved" + " ) </span>";
                                }
                                else if (drdoctor["Temp"].ToString() == "3")
                                {
                                    strDelay = "<span style='color:red;font-family:Verdana'> ( " + "Edit - ReEntry" + " ) </span>";
                                }

                                if (sf_code.Contains("MR"))
                                {
                                    lit_det_NonFwk.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_B"].ToString() + strDelay;
                                }
                                else
                                {
                                    lit_det_NonFwk.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_M"].ToString() + strDelay;
                                }
                                tc_det_NonFwk.BorderStyle = BorderStyle.Solid;
                                tc_det_NonFwk.BackColor = System.Drawing.ColorTranslator.FromHtml("#B2E0E6");
                                tc_det_NonFwk.Attributes.Add("Class", "tbldetail_main");
                                tc_det_NonFwk.ColumnSpan = 7;
                                tc_det_NonFwk.Controls.Add(lit_det_NonFwk);
                                tr_det_sno.Cells.Add(tc_det_NonFwk);
                            }

                            tbldetail.Rows.Add(tr_det_sno);

                            tc_det_head_main2.Controls.Add(tbldetail);
                            tr_det_head_main.Cells.Add(tc_det_head_main2);
                            tbldetail_main.Rows.Add(tr_det_head_main);

                            form1.Controls.Add(tbldetail_main);
                        }
                        else
                        {


                            if (dsDelay.Tables[0].Rows.Count > 0)
                            {
                                strDelay = "<span style='color:red'> " + dsDelay.Tables[0].Rows[0][0].ToString() + " ";
                            }

                            //if (drdoctor["Temp"].ToString() == "1")
                            //{
                            //    strDelay = "<span style='color:red;font-family:Verdana'> ( " + "Approval Pending" + " ) </span>";
                            //}
                            //else if (drdoctor["Temp"].ToString() == "2")
                            //{
                            //    strDelay = "<span style='color:red;font-family:Verdana'> ( " + "Disapproved" + " ) </span>";
                            //}

                            TableCell tc_det_NonFwk = new TableCell();
                            Literal lit_det_NonFwk = new Literal();

                            if (sf_code.Contains("MR"))
                            {
                                lit_det_NonFwk.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_B"].ToString() + strDelay;
                            }
                            else
                            {
                                lit_det_NonFwk.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_M"].ToString() + strDelay;
                            }
                            tc_det_NonFwk.BorderStyle = BorderStyle.Solid;
                            tc_det_NonFwk.BackColor = System.Drawing.ColorTranslator.FromHtml("#B2E0E6");
                            tc_det_NonFwk.Attributes.Add("Class", "tbldetail_main");
                            tc_det_NonFwk.ColumnSpan = 7;
                            tc_det_NonFwk.Controls.Add(lit_det_NonFwk);
                            tr_det_sno.Cells.Add(tc_det_NonFwk);
                        }

                        tbldetail.Rows.Add(tr_det_sno);

                        tc_det_head_main2.Controls.Add(tbldetail);
                        tr_det_head_main.Cells.Add(tc_det_head_main2);
                        tbldetail_main.Rows.Add(tr_det_head_main);

                        form1.Controls.Add(tbldetail_main);
                    }
                    TableRow tr_total = new TableRow();

                    TableCell tc_Count_Total = new TableCell();
                    tc_Count_Total.BorderStyle = BorderStyle.Solid;
                    tc_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Count_Total = new Literal();
                    lit_Count_Total.Text = "<center>Total</center>";
                    tc_Count_Total.Controls.Add(lit_Count_Total);
                    tc_Count_Total.Font.Bold.ToString();
                    tc_Count_Total.BackColor = System.Drawing.Color.White;
                    tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    tc_Count_Total.ColumnSpan = 5;
                    tc_Count_Total.Style.Add("text-align", "left");
                    tc_Count_Total.Style.Add("font-family", "Calibri");
                    tc_Count_Total.Style.Add("font-size", "10pt");

                    tr_total.Cells.Add(tc_Count_Total);

                    int[] arrTotDoc = new int[] { iTotLstCal };

                    for (int i = 0; i < arrTotDoc.Length; i++)
                    {
                        isum += arrTotDoc[i];
                    }

                    decimal RoundLstCal = new decimal();

                    int LstCal = (int)iTotLstCal / iFieldWrkCount;

                    RoundLstCal = Math.Round((decimal)LstCal, 2);

                    TableCell tc_Lst_Count_Total = new TableCell();
                    tc_Lst_Count_Total.BorderStyle = BorderStyle.Solid;
                    tc_Lst_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Lst_Count_Total = new Literal();
                    lit_Lst_Count_Total.Text = Convert.ToString(LstCal) + "(Avg)";
                    tc_Lst_Count_Total.Controls.Add(lit_Lst_Count_Total);
                    tc_Lst_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    tc_Lst_Count_Total.VerticalAlign = VerticalAlign.Middle;
                    tc_Lst_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    tc_Lst_Count_Total.Font.Bold.ToString();
                    tc_Lst_Count_Total.Style.Add("color", "Red");
                    tc_Lst_Count_Total.Style.Add("background-color", "#ffe4b5");
                    //tc_Lst_Count_Total.Style.Add("text-align", "left");
                    //tc_Lst_Count_Total.Style.Add("font-family", "Calibri");
                    //tc_Lst_Count_Total.Style.Add("font-size", "10pt");
                    tr_total.Cells.Add(tc_Lst_Count_Total);


                    int k = 0;
                    foreach (DataRow pro1 in dsTerritory.Tables[0].Rows)
                    {

                        TableCell tc_tot = new TableCell();
                        HyperLink hyp_mon = new HyperLink();


                        hyp_mon.Text = na[k] == null ? "" : na[k].ToString() == "0" ? "" : na[k].ToString();// Tot_Sec.ToString();

                        k++;
                        tc_tot.BorderStyle = BorderStyle.Solid;
                        tc_tot.BorderWidth = 1;
                        tc_tot.BackColor = System.Drawing.Color.White;
                        tc_tot.Width = 200;
                        tc_tot.Style.Add("font-family", "Calibri");
                        tc_tot.Style.Add("font-size", "10pt");
                        tc_tot.HorizontalAlign = HorizontalAlign.Center;
                        tc_tot.VerticalAlign = VerticalAlign.Middle;
                        tc_tot.Controls.Add(hyp_mon);
                        tc_tot.Attributes.Add("Class", "tbldetail_main");
                        tc_tot.Font.Bold.ToString();
                        tc_tot.Style.Add("color", "Red");
                        tc_tot.Style.Add("background-color", "#ffe4b5");
                        tr_total.Cells.Add(tc_tot);
                    }




                    TableCell tc_Lst_Count_Total1 = new TableCell();
                    tc_Lst_Count_Total1.BorderStyle = BorderStyle.Solid;
                    tc_Lst_Count_Total1.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Lst_Count_Total1 = new Literal();
                    lit_Lst_Count_Total1.Text = Convert.ToString(detorderval);
                    tc_Lst_Count_Total1.Controls.Add(lit_Lst_Count_Total1);
                    tc_Lst_Count_Total1.HorizontalAlign = HorizontalAlign.Center;
                    tc_Lst_Count_Total1.VerticalAlign = VerticalAlign.Middle;
                    tc_Lst_Count_Total1.Attributes.Add("Class", "tbldetail_main");
                    tc_Lst_Count_Total1.Font.Bold.ToString();
                    tc_Lst_Count_Total1.Style.Add("color", "Red");
                    tc_Lst_Count_Total1.Style.Add("background-color", "#ffe4b5");

                    tr_total.Cells.Add(tc_Lst_Count_Total1);

                    TableCell tc_Lst_Count_Total2 = new TableCell();
                    tc_Lst_Count_Total2.BorderStyle = BorderStyle.Solid;
                    tc_Lst_Count_Total2.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Lst_Count_Total2 = new Literal();
                    lit_Lst_Count_Total2.Text = Convert.ToString(detnetval);
                    tc_Lst_Count_Total2.Controls.Add(lit_Lst_Count_Total2);
                    tc_Lst_Count_Total2.HorizontalAlign = HorizontalAlign.Center;
                    tc_Lst_Count_Total2.VerticalAlign = VerticalAlign.Middle;
                    tc_Lst_Count_Total2.Attributes.Add("Class", "tbldetail_main");
                    tc_Lst_Count_Total2.Font.Bold.ToString();
                    tc_Lst_Count_Total2.Style.Add("color", "Red");
                    tc_Lst_Count_Total2.Style.Add("background-color", "#ffe4b5");

                    tr_total.Cells.Add(tc_Lst_Count_Total2);
                    //int[] arrTotChem = new int[] { iTotChemCal };

                    //for (int i = 0; i < arrTotChem.Length; i++)
                    //{
                    //    isumChem += arrTotChem[i];
                    //}

                    //decimal RoundChemCal = new decimal();

                    //double ChemCal = (double)iTotChemCal / iFieldWrkCount;

                    //RoundChemCal = Math.Round((decimal)ChemCal, 2);

                    TableCell tc_Chem_Count_Total = new TableCell();
                    tc_Chem_Count_Total.BorderStyle = BorderStyle.Solid;
                    tc_Chem_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Chem_Count_Total = new Literal();
                    lit_Chem_Count_Total.Text = Convert.ToString(iTotChemCal);
                    tc_Chem_Count_Total.Controls.Add(lit_Chem_Count_Total);
                    tc_Chem_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    tc_Chem_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    tc_Chem_Count_Total.Font.Bold.ToString();
                    tc_Chem_Count_Total.Style.Add("color", "Red");
                    tc_Chem_Count_Total.Style.Add("background-color", "#ffe4b5");
                    //tc_Chem_Count_Total.Style.Add("text-align", "left");
                    //tc_Chem_Count_Total.Style.Add("font-family", "Calibri");
                    //tc_Chem_Count_Total.Style.Add("font-size", "10pt");
                    tr_total.Cells.Add(tc_Chem_Count_Total);

                    //int[] arrtotChemPOB = new int[] { iTotChemPOB };

                    //for (int i = 0; i < arrtotChemPOB.Length; i++)
                    //{
                    //    isumChemPOB += arrtotChemPOB[i];
                    //}

                    TableCell Chemist_POB_Count_Total = new TableCell();
                    Chemist_POB_Count_Total.BorderStyle = BorderStyle.Solid;
                    Chemist_POB_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Chem_POB_Count_Total = new Literal();
                    lit_Chem_POB_Count_Total.Text = Convert.ToString(iTotChemPOB);
                    Chemist_POB_Count_Total.Controls.Add(lit_Chem_POB_Count_Total);
                    Chemist_POB_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    Chemist_POB_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    Chemist_POB_Count_Total.Font.Bold.ToString();
                    Chemist_POB_Count_Total.Style.Add("color", "Red");
                    Chemist_POB_Count_Total.Style.Add("background-color", "#ffe4b5");
                    //tc_Stock_Count_Total.Style.Add("text-align", "left");
                    //tc_Stock_Count_Total.Style.Add("font-family", "Calibri");
                    //tc_Stock_Count_Total.Style.Add("font-size", "10pt");
                    tr_total.Cells.Add(Chemist_POB_Count_Total);

                    //int[] arrtotStock = new int[] { iTotStockCal };

                    //for (int i = 0; i < arrtotStock.Length; i++)
                    //{
                    //isumStock += arrtotStock[i];
                    //}

                    //decimal RoundStockCal = new decimal();

                    //double StockCal = (double)iTotStockCal / iFieldWrkCount;

                    //RoundStockCal = Math.Round((decimal)StockCal, 2);

                    TableCell tc_Stock_Count_Total = new TableCell();
                    tc_Stock_Count_Total.BorderStyle = BorderStyle.Solid;
                    tc_Stock_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Stock_Count_Total = new Literal();
                    lit_Stock_Count_Total.Text = Convert.ToString(iTotStockCal);
                    tc_Stock_Count_Total.Controls.Add(lit_Stock_Count_Total);
                    tc_Stock_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    tc_Stock_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    tc_Stock_Count_Total.Font.Bold.ToString();
                    tc_Stock_Count_Total.BackColor = System.Drawing.Color.White;
                    tc_Stock_Count_Total.Style.Add("color", "Red");
                    tc_Stock_Count_Total.Style.Add("background-color", "#ffe4b5");
                    tc_Stock_Count_Total.Style.Add("text-align", "left");
                    tc_Stock_Count_Total.Style.Add("font-family", "Calibri");
                    tc_Stock_Count_Total.Style.Add("font-size", "10pt");
                    tr_total.Cells.Add(tc_Stock_Count_Total);

                    //int[] arrtotUnLst = new int[] { iTotUnLstCal };

                    //for (int i = 0; i < arrtotUnLst.Length; i++)
                    //{
                    //    isumUnLst += arrtotUnLst[i];
                    //}

                    //decimal RoundUnLstCal = new decimal();

                    //double UnLstCal = (double)iTotUnLstCal / iFieldWrkCount;

                    //RoundUnLstCal = Math.Round((decimal)UnLstCal, 2);

                    //TableCell tc_UnLst_Count_Total = new TableCell();
                    //tc_UnLst_Count_Total.BorderStyle = BorderStyle.Solid;
                    //tc_UnLst_Count_Total.BorderWidth = 1;
                    ////tc_catg_Total.Width = 25;
                    //Literal lit_UnLst_Count_Total = new Literal();
                    //lit_UnLst_Count_Total.Text = Convert.ToString(RoundUnLstCal);
                    //tc_UnLst_Count_Total.Controls.Add(lit_UnLst_Count_Total);
                    //tc_UnLst_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    //tc_UnLst_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    //tc_UnLst_Count_Total.Font.Bold.ToString();
                    ////tc_UnLst_Count_Total.BackColor = System.Drawing.Color.White;
                    //tc_UnLst_Count_Total.Style.Add("color", "Red");
                    //tc_UnLst_Count_Total.Style.Add("background-color", "#ffe4b5");
                    ////tc_UnLst_Count_Total.Style.Add("text-align", "left");
                    ////tc_UnLst_Count_Total.Style.Add("font-family", "Calibri");
                    ////tc_UnLst_Count_Total.Style.Add("font-size", "10pt");
                    //tr_total.Cells.Add(tc_UnLst_Count_Total);

                    tbldetail.Rows.Add(tr_total);

                    tc_det_head_main2.Controls.Add(tbldetail);
                    tr_det_head_main.Cells.Add(tc_det_head_main2);
                    tbldetail_main.Rows.Add(tr_det_head_main);

                    form1.Controls.Add(tbldetail_main);
                }
            }
            else
            {
                //lblHead.Visible = true;
                //lblHead.Style.Add("margin-top", "80px");
                //lblHead.Text = "No Record Found";

                pnlbutton.Visible = true;

                Table tbldetail_mainHoliday = new Table();
                tbldetail_mainHoliday.BorderStyle = BorderStyle.None;
                tbldetail_mainHoliday.Width = 1100;
                TableRow tr_det_head_mainHoliday = new TableRow();
                TableCell tc_det_head_mainHolday = new TableCell();
                tc_det_head_mainHolday.Width = 100;
                Literal lit_det_mainHoliday = new Literal();
                lit_det_mainHoliday.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                tbldetail_mainHoliday.Style.Add("margin-top", "110px");
                tc_det_head_mainHolday.Controls.Add(lit_det_mainHoliday);
                tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHolday);

                TableCell tc_det_head_mainHoliday = new TableCell();
                tc_det_head_mainHoliday.Width = 800;

                Table tbldetailHoliday = new Table();
                tbldetailHoliday.BorderStyle = BorderStyle.Solid;
                tbldetailHoliday.BorderWidth = 1;
                tbldetailHoliday.GridLines = GridLines.Both;
                tbldetailHoliday.Width = 1000;
                tbldetailHoliday.Style.Add("border-collapse", "collapse");
                tbldetailHoliday.Style.Add("border", "solid 1px Black");

                TableRow tr_det_sno = new TableRow();
                TableCell tc_det_SNo = new TableCell();
                iCount += 1;
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "No Record Found";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.Attributes.Add("Class", "NoRecord");

                tc_det_SNo.HorizontalAlign = HorizontalAlign.Center;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.BorderStyle = BorderStyle.None;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det_sno.Cells.Add(tc_det_SNo);

                tbldetailHoliday.Rows.Add(tr_det_sno);

                tc_det_head_mainHoliday.Controls.Add(tbldetailHoliday);
                tr_det_head_mainHoliday.Cells.Add(tc_det_head_mainHoliday);
                tbldetail_mainHoliday.Rows.Add(tr_det_head_mainHoliday);

                form1.Controls.Add(tbldetail_mainHoliday);
            }


            //lblHead.Visible = true;
            //lblHead.Style.Add("margin-top", "80px");
            //lblHead.Text = "No Record Found";



        }
        catch (Exception ex)
        {

        }
    }

    private int getmaxdays_month(int imonth)
    {
        int idays = -1;

        if (imonth == 1)
            idays = 31;
        else if (imonth == 2)
            idays = 28;
        else if (imonth == 3)
            idays = 31;
        else if (imonth == 4)
            idays = 30;
        else if (imonth == 5)
            idays = 31;
        else if (imonth == 6)
            idays = 30;
        else if (imonth == 7)
            idays = 31;
        else if (imonth == 8)
            idays = 31;
        else if (imonth == 9)
            idays = 30;
        else if (imonth == 10)
            idays = 31;
        else if (imonth == 11)
            idays = 30;
        else if (imonth == 12)
            idays = 31;

        return idays;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string filename = "DCRView.xls";
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

        //Get the HTML for the control.             
        form1.RenderControl(hw);
        //Write the HTML back to the browser.
        //Response.ContentType = application/vnd.ms-excel;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
        this.EnableViewState = false;
        Response.Write(tw.ToString());
        Response.End();

    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "rptDCRView";
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);
        iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
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

    protected void gvtotalorder_PreRender(object sender, EventArgs e)
    {
        GridDecorator.MergeRows(gvtotalorder);
    }

    public class GridDecorator
    {
        DataSet dsGV = new DataSet();
        public static void MergeRows(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                // for (int i = 0; i < row.Cells.Count - 32; i++)
                // {
                if (row.Cells[1].Text == previousRow.Cells[1].Text)
                {
                    DataSet dsGV = new DataSet();
                    DCR dc = new DCR();
                    row.Cells[1].RowSpan = previousRow.Cells[1].RowSpan < 2 ? 2 :
                                           previousRow.Cells[1].RowSpan + 1;

                    previousRow.Cells[1].Visible = false;

                    //row.Cells[4].RowSpan = previousRow.Cells[1].RowSpan < 2 ? 2 :
                    //                       previousRow.Cells[1].RowSpan + 1;

                    //previousRow.Cells[4].Visible = false;
                    //row.Cells[5].RowSpan = previousRow.Cells[1].RowSpan < 2 ? 2 :
                    //                       previousRow.Cells[1].RowSpan + 1;

                    //previousRow.Cells[5].Visible = false;

                }
                // }
            }
        }
    }
    protected void GridView4_RowCreated(object sender, GridViewRowEventArgs e)
    {
        SalesForce SF = new SalesForce();
        DataSet ff = new DataSet();
        ff = SF.GetBrd_Name(div_code, Fdate);
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow0 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            TableCell HeaderCell = new TableCell();
            HeaderCell.Width = 25;
            HeaderCell.Height = 35;
            HeaderCell.Font.Bold = true;
            //HeaderCell.ApplyStyle.
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");

            TableCell Distributor = new TableCell();
            HeaderCell.Width = 25;
            HeaderCell.Height = 35;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "S.No";

            HeaderGridRow0.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Width = 250;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "Field Force Name";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Width = 250;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "SF_CODE";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);



            foreach (DataRow drdoctor in ff.Tables[0].Rows)
            {
                HeaderCell = new TableCell();
                HeaderCell.Height = 35;
                HeaderCell.Width = 80;
                HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");

                HeaderCell.Attributes["style"] = "font: Andalus";
                HeaderCell.Attributes["style"] = "font: Bold";
                HeaderCell.Text = drdoctor["Product_Brd_Name"].ToString();
                HeaderGridRow0.Cells.Add(HeaderCell);
            }

            HeaderCell = new TableCell();
            HeaderCell.Width = 100;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "Total";
            //HeaderCell. = "Andalus";
            HeaderGridRow0.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Width = 80;
            HeaderCell.Height = 35;
            HeaderCell.Attributes["style"] = "font: Courier";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.BackColor = System.Drawing.Color.FromName("#33CCCC");
            HeaderCell.Text = "EC";
        }
    }
}