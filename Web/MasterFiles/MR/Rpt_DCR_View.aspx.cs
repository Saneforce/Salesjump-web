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

public partial class Reports_Rpt_DCR_View : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDCR = null;
    DataSet dsTerritory = null;
    DataSet dsdoc = null;
    DataSet dssf = null;
    string div_code = string.Empty;
    string strDelay = string.Empty;
    string sf_code = string.Empty;
    string Sf_Name = string.Empty;
    string strMode = string.Empty;
    string sURL = string.Empty;
    string Sf_HQ = string.Empty;
    int cmonth = -1;
    int cyear = -1;
    int tot_days = -1;
    int cday = 1;
    int iCount = -1;
    int iFieldWrkCount = -1;
    string sDCR = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        Sf_Name = Request.QueryString["Sf_Name"].ToString();
        cmonth = Convert.ToInt16(Request.QueryString["cur_month"].ToString());
        cyear = Convert.ToInt16(Request.QueryString["cur_year"].ToString());
        strMode = Request.QueryString["Mode"].ToString();
        strMode = strMode.Trim();

        string sMonth = getMonthName(cmonth) + " - " + cyear.ToString();       

        if (strMode.Trim() == "View All DCR Date(s)")
        {
            lblTitle.Text = "Daily Call Report (<span style='color:Red'>" + "all Dates" + "</span>) view for the month of " + sMonth;
            CreateDynamicTableDCRDate(cmonth, cyear, sf_code);
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
        else if(strMode=="Detailed View")
        {
            lblTitle.Text = "Daily Call Report (<span style='color:Red'>" + "Detail View" + "</span>) for the month of " + sMonth;
            CreateDynamicDCRDetailedView(cmonth, cyear, sf_code);
            //lblHead.Text = lblHead.Text + sMonth;
            lblHead.Visible = false;
        }
        else if (strMode == "TP MY Day Plan")
        {
            lblTitle.Text = "Daily Call Report (<span style='color:Red'>" + "Plan View" + "</span>) for the month of " + sMonth;
            //lblFieldForceName.Text = Sf_Name;
            DataSet dsGV = new DataSet();
            DCR dc = new DCR();
            if (sf_code.Contains("MR"))
            {
                dsGV = dc.GetTPDayMap_MR(div_code, sf_code, cmonth, cyear);
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
            }
            else
            {
                dsGV = dc.GetTPDayMap_MGR(div_code, sf_code, cmonth, cyear);
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
            }

            lblHead.Visible = false;
        }


        ExportButton();

    }
    private void ExportButton()
    {
        btnClose.Visible = false;
        btnPrint.Visible = false;
        btnExcel.Visible = false;
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
            CreateDynamicTableDCRDate(cmonth, cyear, sf_code);
        //FillWorkType();
    }

    private void CreateDynamicTableDCRDate(int imonth, int iyear, string sf_code)
    {
        DCR dc = new DCR();

        //dsDCR = dc.get_dcr_date(sf_code, imonth, iyear);
        dsDCR = dc.get_dcr_DCRPendingdate(sf_code, imonth, iyear);

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
                HyperLink lit_day = new HyperLink();
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

                if(sf_code.Contains("MR"))
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
                        Literal lit_det_SNo = new Literal();
                        if (sf_code.Contains("MR"))
                        {
                            lit_det_SNo.Text = "<center>" + drdoctor["Worktype_Name_B"].ToString() + "</center>";
                        }
                        else
                        {
                            lit_det_SNo.Text = "<center>" + drdoctor["Worktype_Name_M"].ToString() + "</center>";
                        }
                        tc_det_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_SNo.Attributes.Add("Class", "Holiday");
                        tc_det_SNo.BorderWidth = 1;
                        tc_det_SNo.BorderStyle = BorderStyle.None;
                        tc_det_SNo.Controls.Add(lit_det_SNo);
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

                    

                    dsdoc = dc.get_dcr_details(sf_code, drdoc["Activity_Date"].ToString(), 1); //1-Listed Doctor
                    iCount = 0;
                    if (dsdoc.Tables[0].Rows.Count > 0)
                    {
                        

                        lit_Terr.Text = "<span style='margin-left:200px'><b> Territory Worked </b></span>" + "<span style='margin-left:15px'></span>:" + "<span style='margin-left:21px;text-align:right'>" + dsdoc.Tables[0].Rows[0]["che_POB_Name"].ToString() + "</span>";
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

                        //TableCell tc_det_head_catg = new TableCell();
                        //tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                        //tc_det_head_catg.BorderWidth = 1;
                        //tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                        //Literal lit_det_head_catg = new Literal();
                        //lit_det_head_catg.Text = "<b>Category</b>";
                        //tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                        //tc_det_head_catg.Controls.Add(lit_det_head_catg);
                        //tr_det_head.Cells.Add(tc_det_head_catg);

                        TableCell tc_det_head_spec = new TableCell();
                        tc_det_head_spec.BorderStyle = BorderStyle.Solid;
                        tc_det_head_spec.BorderWidth = 1;
                        tc_det_head_spec.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_head_spec = new Literal();
                        lit_det_head_spec.Text = "<b>Channel</b>";
                        tc_det_head_spec.Attributes.Add("Class", "tr_det_head");
                        tc_det_head_spec.Controls.Add(lit_det_head_spec);
                        tr_det_head.Cells.Add(tc_det_head_spec);

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

                        string strlongname = "";
                        iCount = 0;
                        
                        foreach (DataRow drdoctor in dsdoc.Tables[0].Rows)
                        {
                            if ((drdoctor["GeoAddrs"].ToString().Trim() == "NA" || drdoctor["GeoAddrs"].ToString().Trim() != "") && drdoctor["lati"] != "")
                            {
                                sURL = "DCR_ShowMap.aspx?sf_Code=" + sf_code + " &strDate=" + drdoc["Activity_Date"].ToString() + " ";
                                lit_day.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                lit_day.NavigateUrl = "#";

                                 int i=0;
                                XmlDocument doc = new XmlDocument();
                                doc.Load("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + drdoctor["lati"] + "," + drdoctor["long"] + "&sensor=false");
                                XmlNode element = doc.SelectSingleNode("//GeocodeResponse/status");
                                XmlNodeList xnList = doc.SelectNodes("//GeocodeResponse/result/address_component");

                                foreach (XmlNode xn in xnList)
                                {
                                    i+=1;
                                    if (i < 8)
                                    {
                                        strlongname += xn["long_name"].InnerText + ",";
                                    }
                                    
                                }

                                if (strlongname != "")
                                {
                                    strlongname = strlongname.Remove(strlongname.Length - 1);
                                }


                            }

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

                            //TableCell tc_det_catg = new TableCell();
                            //Literal lit_det_catg = new Literal();
                            //lit_det_catg.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Cat_Name"].ToString();
                            //tc_det_catg.Attributes.Add("Class", "tbldetail_Data");
                            //tc_det_catg.BorderStyle = BorderStyle.Solid;
                            //tc_det_catg.BorderWidth = 1;
                            //tc_det_catg.Controls.Add(lit_det_catg);
                            //tr_det_sno.Cells.Add(tc_det_catg);

                            TableCell tc_det_spec = new TableCell();
                            Literal lit_det_spec = new Literal();
                            lit_det_spec.Text = "&nbsp;&nbsp;" + drdoctor["Doc_Special_Name"].ToString();
                            tc_det_spec.Attributes.Add("Class", "tbldetail_Data");
                            tc_det_spec.BorderStyle = BorderStyle.Solid;
                            tc_det_spec.BorderWidth = 1;
                            tc_det_spec.Controls.Add(lit_det_spec);
                            tr_det_sno.Cells.Add(tc_det_spec);

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

                            //TableCell tc_det_CallFeedBack = new TableCell();
                            //Literal lit_det_CallFeedBack = new Literal();
                            //lit_det_CallFeedBack.Text = "&nbsp;&nbsp;" + drdoctor["Rx"].ToString();
                            //tc_det_CallFeedBack.Attributes.Add("Class", "tbldetail_Data");
                            //tc_det_CallFeedBack.BorderStyle = BorderStyle.Solid;
                            //tc_det_CallFeedBack.Width = 200;
                            //tc_det_CallFeedBack.BorderWidth = 1;
                            //tc_det_CallFeedBack.Controls.Add(lit_det_CallFeedBack);
                            //tr_det_sno.Cells.Add(tc_det_CallFeedBack);

                            TableCell tc_det_prod = new TableCell();
                            Literal lit_det_prod = new Literal();
                            lit_det_prod.Text = drdoctor["Product_Detail"].ToString().Replace("~", "(").Trim();
                            tc_det_prod.Attributes.Add("Class", "tbldetail_Data");
                            lit_det_prod.Text = lit_det_prod.Text.Replace("$", ")").Trim();
                            lit_det_prod.Text = "&nbsp;&nbsp;" + lit_det_prod.Text.Replace("#", "  ").Trim();
                            tc_det_prod.BorderStyle = BorderStyle.Solid;
                            //tc_det_CallFeedBack.Width = 150;
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
                        }
                    }

                    //form1.Controls.Add(tbldetail);

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

            pnlbutton.Visible = false;

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

                if (dsdoc.Tables[0].Rows.Count > 0 || dsget_dcr_che.Tables[0].Rows.Count > 0 || dsget_dcr_stk.Tables[0].Rows.Count > 0 || dsget_dcr_hos.Tables[0].Rows.Count > 0 || dsdoc_Pending.Tables[0].Rows.Count >0)
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
                    lit_det_SNo.Text = "<center> <b> "+dsdoc.Tables[0].Rows[0]["Temp"]+" </b> </center>";
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

            pnlbutton.Visible = false;

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
                        lit_det_dr_name.Text =  drdoctor["Plan_Name"].ToString();
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

            pnlbutton.Visible = false;

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

            dsdoc = dc.get_dcr_Doctor_Detail_View(sf_code,cmonth,cyear); //1-Listed Doctor
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
                pnlbutton.Visible = false;

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

    private void CreateDynamicDCRDetailedView(int imonth, int iyear, string sf_code)
    {
        try
        {

            DCR dc = new DCR();
            //dsDCR = dc.get_dcr_date(sf_code, imonth, iyear);
            dsDCR = dc.get_dcr_DCRPendingdate_DCRDetail(sf_code, imonth, iyear);
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
                    dsdoc = dc.get_DCRView_Approved_All_Dates(sf_code, cmonth.ToString(), cyear.ToString()); //1-Listed Doctor
                }
                else
                {
                    dsdoc = dc.get_DCRView_Approved_MGR_All_Dates(sf_code, cmonth.ToString(), cyear.ToString()); //1-Listed Doctor
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
                    lit_det_head_visit.Text = "<b>Retailer(s) <br> Met</b>";
                    tc_det_head_visit.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_visit.Controls.Add(lit_det_head_visit);
                    tr_det_head.Cells.Add(tc_det_head_visit);

                    //TableCell tc_det_head_catg = new TableCell();
                    //tc_det_head_catg.BorderStyle = BorderStyle.Solid;
                    //tc_det_head_catg.BorderWidth = 1;
                    //tc_det_head_catg.HorizontalAlign = HorizontalAlign.Center;
                    //Literal lit_det_head_catg = new Literal();
                    //lit_det_head_catg.Text = "<b>Chemist <br> Met</b>";
                    //tc_det_head_catg.Attributes.Add("Class", "tr_det_head");
                    //tc_det_head_catg.Controls.Add(lit_det_head_catg);
                    //tr_det_head.Cells.Add(tc_det_head_catg);

                    //TableCell tc_det_head_POB = new TableCell();
                    //tc_det_head_POB.BorderStyle = BorderStyle.Solid;
                    //tc_det_head_POB.BorderWidth = 1;
                    //tc_det_head_POB.HorizontalAlign = HorizontalAlign.Center;
                    ////tc_det_head_POB.Visible = false;
                    //Literal lit_det_head_spec = new Literal();
                    //lit_det_head_spec.Text = "<b>Chemist <br> POB</b>";
                    //tc_det_head_POB.Attributes.Add("Class", "tr_det_head");
                    //tc_det_head_POB.Controls.Add(lit_det_head_spec);
                    //tr_det_head.Cells.Add(tc_det_head_POB);

                    TableCell tc_det_head_prod = new TableCell();
                    tc_det_head_prod.BorderStyle = BorderStyle.Solid;
                    tc_det_head_prod.BorderWidth = 1;
                    tc_det_head_prod.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_prod = new Literal();
                    lit_det_head_prod.Text = "<b>Stockist <br> Met</b>";
                    tc_det_head_prod.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_prod.Controls.Add(lit_det_head_prod);
                    tr_det_head.Cells.Add(tc_det_head_prod);

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

                        dsDelay = dc.get_DCR_Status_Delay_DCRView(drdoctor["sf_code"].ToString(), drdoctor["Activity_Date"].ToString(), cmonth, cyear);
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

                                dsDelay = dc.get_DCR_Status_Delay_DCRView(drdoctor["sf_code"].ToString(), drdoctor["Activity_Date"].ToString(), cmonth, cyear);
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
                                lit_det_lvisit.Text = "0"; // drdoctor["lvisit"].ToString();
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

                                //TableCell tc_det_prod = new TableCell();
                                //HyperLink hyllit_det_prod = new HyperLink();
                                //hyllit_det_prod.Text = drdoctor["che_cnt"].ToString().ToString();
                                //if (hyllit_det_prod.Text != "0")
                                //{
                                //    sURL = "rptDCRViewCompleted.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 2 + "";
                                //    hyllit_det_prod.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                //    hyllit_det_prod.NavigateUrl = "#";

                                //}
                                //tc_det_prod.BorderStyle = BorderStyle.Solid;
                                //tc_det_prod.HorizontalAlign = HorizontalAlign.Center;
                                //tc_det_prod.Attributes.Add("Class", "tbldetail_main");
                                //tc_det_prod.BorderWidth = 1;
                                //tc_det_prod.Controls.Add(hyllit_det_prod);
                                //tr_det_sno.Cells.Add(tc_det_prod);

                                //iTotChemCal += Convert.ToInt16(hyllit_det_prod.Text);

                                //TableCell tc_det_Che_POB = new TableCell();
                                //Literal lit_det_Che_POB = new Literal();

                                //if (drdoctor["che_POB"].ToString().ToString() != "")
                                //{
                                //    lit_det_Che_POB.Text = drdoctor["che_POB"].ToString().ToString();
                                //}
                                //else
                                //{
                                //    lit_det_Che_POB.Text = "0";
                                //}
                                //tc_det_Che_POB.BorderStyle = BorderStyle.Solid;
                                //tc_det_Che_POB.Attributes.Add("Class", "tbldetail_main");
                                ////tc_det_head_POB.Visible = false;
                                //tc_det_Che_POB.HorizontalAlign = HorizontalAlign.Center;
                                //tc_det_Che_POB.BorderWidth = 1;
                                //tc_det_Che_POB.Controls.Add(lit_det_Che_POB);
                                //tr_det_sno.Cells.Add(tc_det_Che_POB);

                                //iTotChemPOB += Convert.ToInt32(lit_det_Che_POB.Text);

                                TableCell tc_det_gift = new TableCell();
                                HyperLink hyllit_det_gift = new HyperLink();
                                hyllit_det_gift.Text = drdoctor["stk_cnt"].ToString();
                                if (hyllit_det_gift.Text != "0")
                                {
                                    sURL = "rptDCRViewCompleted.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 3 + "";

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

                    double LstCal = (double)iTotLstCal / iFieldWrkCount;

                    RoundLstCal = Math.Round((decimal)LstCal, 2);

                    TableCell tc_Lst_Count_Total = new TableCell();
                    tc_Lst_Count_Total.BorderStyle = BorderStyle.Solid;
                    tc_Lst_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Lst_Count_Total = new Literal();
                    lit_Lst_Count_Total.Text = Convert.ToString(RoundLstCal);
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

                    //int[] arrTotChem = new int[] { iTotChemCal };

                    //for (int i = 0; i < arrTotChem.Length; i++)
                    //{
                    //    isumChem += arrTotChem[i];
                    //}

                    //decimal RoundChemCal = new decimal();

                    //double ChemCal = (double)iTotChemCal / iFieldWrkCount;

                    //RoundChemCal = Math.Round((decimal)ChemCal, 2);

                    //TableCell tc_Chem_Count_Total = new TableCell();
                    //tc_Chem_Count_Total.BorderStyle = BorderStyle.Solid;
                    //tc_Chem_Count_Total.BorderWidth = 1;
                    ////tc_catg_Total.Width = 25;
                    //Literal lit_Chem_Count_Total = new Literal();
                    //lit_Chem_Count_Total.Text = Convert.ToString(RoundChemCal);
                    //tc_Chem_Count_Total.Controls.Add(lit_Chem_Count_Total);
                    //tc_Chem_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    //tc_Chem_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    //tc_Chem_Count_Total.Font.Bold.ToString();
                    //tc_Chem_Count_Total.Style.Add("color", "Red");
                    //tc_Chem_Count_Total.Style.Add("background-color", "#ffe4b5");
                    ////tc_Chem_Count_Total.Style.Add("text-align", "left");
                    ////tc_Chem_Count_Total.Style.Add("font-family", "Calibri");
                    ////tc_Chem_Count_Total.Style.Add("font-size", "10pt");
                    //tr_total.Cells.Add(tc_Chem_Count_Total);

                    //int[] arrtotChemPOB = new int[] { iTotChemPOB };

                    //for (int i = 0; i < arrtotChemPOB.Length; i++)
                    //{
                    //    isumChemPOB += arrtotChemPOB[i];
                    //}

                    //TableCell Chemist_POB_Count_Total = new TableCell();
                    //Chemist_POB_Count_Total.BorderStyle = BorderStyle.Solid;
                    //Chemist_POB_Count_Total.BorderWidth = 1;
                    ////tc_catg_Total.Width = 25;
                    //Literal lit_Chem_POB_Count_Total = new Literal();
                    //lit_Chem_POB_Count_Total.Text = Convert.ToString(isumChemPOB);
                    //Chemist_POB_Count_Total.Controls.Add(lit_Chem_POB_Count_Total);
                    //Chemist_POB_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    //Chemist_POB_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    //Chemist_POB_Count_Total.Font.Bold.ToString();
                    //Chemist_POB_Count_Total.Style.Add("color", "Red");
                    //Chemist_POB_Count_Total.Style.Add("background-color", "#ffe4b5");
                    ////tc_Stock_Count_Total.Style.Add("text-align", "left");
                    ////tc_Stock_Count_Total.Style.Add("font-family", "Calibri");
                    ////tc_Stock_Count_Total.Style.Add("font-size", "10pt");
                    //tr_total.Cells.Add(Chemist_POB_Count_Total);

                    int[] arrtotStock = new int[] { iTotStockCal };

                    for (int i = 0; i < arrtotStock.Length; i++)
                    {
                        isumStock += arrtotStock[i];
                    }

                    decimal RoundStockCal = new decimal();

                    double StockCal = (double)iTotStockCal / iFieldWrkCount;

                    RoundStockCal = Math.Round((decimal)StockCal, 2);

                    TableCell tc_Stock_Count_Total = new TableCell();
                    tc_Stock_Count_Total.BorderStyle = BorderStyle.Solid;
                    tc_Stock_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Stock_Count_Total = new Literal();
                    lit_Stock_Count_Total.Text = Convert.ToString(RoundStockCal);
                    tc_Stock_Count_Total.Controls.Add(lit_Stock_Count_Total);
                    tc_Stock_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    tc_Stock_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    tc_Stock_Count_Total.Font.Bold.ToString();
                    //tc_Stock_Count_Total.BackColor = System.Drawing.Color.White;
                    tc_Stock_Count_Total.Style.Add("color", "Red");
                    tc_Stock_Count_Total.Style.Add("background-color", "#ffe4b5");  
                    //tc_Stock_Count_Total.Style.Add("text-align", "left");
                    //tc_Stock_Count_Total.Style.Add("font-family", "Calibri");
                    //tc_Stock_Count_Total.Style.Add("font-size", "10pt");
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

                pnlbutton.Visible = false;

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
   
}