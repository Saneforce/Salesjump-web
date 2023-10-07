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

public partial class Reports_DCR_Status_Detailed : System.Web.UI.Page
{
    DataSet dsSalesForce = new DataSet();
    DataSet dsDCR = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    int cmonth = -1;
    int cyear = -1;
    int tot_days = -1;
    int cday = 1;
    int tot = 0;
    int ddate = 0;
    int docdate = 0;
    string sDCR = string.Empty;
    string sDelay = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSf = null;
    DataSet dsDelay = new DataSet();
    string strCase = string.Empty;
    string sf_name = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        sf_name = Request.QueryString["sf_name"].ToString();
        cmonth = Convert.ToInt16(Request.QueryString["cmon"].ToString());
        cyear = Convert.ToInt16(Request.QueryString["cyear"].ToString());
        sf_type = Session["sf_type"].ToString();

        SalesForce sf = new SalesForce();
                 
       
        FillSalesForce(div_code, sf_code, cmonth, cyear);

        string sMonth = getMonthName(cmonth) + " - " + cyear.ToString();
        lblHead.Text = lblHead.Text + sMonth  + " :  " + sf_name;
        btnPDF.Visible = false;
    }

    //private void FillUserList()
    //{

    //    string sMgr = "admin";
    //    SalesForce sf = new SalesForce();
    //    string strVacant = "1";
    //    if (chkVacant.Checked == true)
    //    {
    //        strVacant = "0";
    //    }
    //    if (ddlFieldForce.SelectedIndex > 0)
    //    {
    //        sMgr = ddlFieldForce.SelectedValue;
    //    }

    //    //// To fetch UserList by using DataSet & DataTable by Recursive call - Sridevi on 07/23/15

    //    DataTable dtUserList = new DataTable();
    //    if (chkVacant.Checked == true)
    //    {
    //        dtUserList = sf.getUserListReportingTo(ddlDivision.SelectedValue, sMgr, 0); // 28-Aug-15 -Sridevi
    //    }
    //    else
    //    {
    //        dtUserList = sf.getUserListReportingToAll(ddlDivision.SelectedValue, sMgr, 0); // 28-Aug-15 -Sridevi
    //    }

    //    if (dtUserList.Rows.Count > 0)
    //    {
    //        //grdSalesForce.Visible = true;
    //        //grdSalesForce.DataSource = dtUserList;
    //        //grdSalesForce.DataBind();
    //    }
    //    else
    //    {
    //        //grdSalesForce.DataSource = dtUserList;
    //        //grdSalesForce.DataBind();
    //    }
    //}

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
        DataTable dtSalesforce = new DataTable();
        if (sf_type == "2")
        {
            if (sf_code.Contains("MR"))
            {
                dsSalesForce = sf.getDCRStatus_MRStatus(sf_code);
            }
            else
            {
                dsSalesForce = sf.UserList_get_SelfMail(div_code, sf_code);
            }   
        }
        else if (sf_type == "1")
        {
            dsSalesForce = sf.getSfName_HQ(sf_code);
        }
        else if (sf_type == "3" || sf_type == "")
        {
            dtSalesforce = sf.getUserListReportingTo(div_code, sf_code, 0);
            dsSalesForce.Tables.Add(dtSalesforce);
        }    
        
        if (dsSalesForce.Tables[0].Rows.Count > 0)
            ViewState["dsSalesForce"] = dsSalesForce;

        CreateDynamicTable();
        FillWorkType();
    }

    private void FillWorkType()
    {
        int j = 1;

        DCR dc = new DCR();

        dsDCR = dc.DCR_get_WorkType(div_code);

        TableCell tc_wt = new TableCell();
        Literal lit_wt = new Literal();
        TableRow tr_wt = new TableRow();
        TableRow tr_wt2 = new TableRow();

        foreach (DataRow drFF in dsDCR.Tables[0].Rows)
        {
            if (j <= 5)
            {
                
                
                lit_wt.Text = "<b>" + drFF["WType_SName"].ToString() + " - " + drFF["Worktype_Name_B"].ToString() + "</b>";

                tc_wt.Controls.Add(lit_wt);
                tr_wt.Cells.Add(tc_wt);
            }
            else
            {
                TableCell tc_wt2 = new TableCell();
                Literal lit_wt2 = new Literal();
                lit_wt2.Text = "<b>" + drFF["WType_SName"].ToString() + " - " + drFF["Worktype_Name_B"].ToString() + "</b>";
                tc_wt2.Controls.Add(lit_wt2);
                tr_wt2.Cells.Add(tc_wt2);
            }

            j = j + 1;
        }

        lit_wt.Text = "LP - Leave Approval Pending";
        tc_wt.Style.Add("font-weight", "bold");
        tc_wt.Controls.Add(lit_wt);
        tr_wt.Cells.Add(tc_wt);
        tblworktype.Rows.Add(tr_wt);
        tblworktype.Rows.Add(tr_wt2);
       
        

    }
   
    private void CreateDynamicTable()
    {
        int iColorCount=0 ;
        if (ViewState["dsSalesForce"] != null)
        {

            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.Controls.Add(lit_SNo);
            tr_header.Cells.Add(tc_SNo);
            tr_header.BackColor = System.Drawing.Color.Bisque;

            TableCell tc_User_Name = new TableCell();
            tc_User_Name.BorderStyle = BorderStyle.Solid;
            tc_User_Name.BorderWidth = 1;
            tc_User_Name.Width = 50;
            Literal lit_User_Name = new Literal();
            lit_User_Name.Text = "<center>User Name</center>";
            tc_User_Name.Controls.Add(lit_User_Name);
            tr_header.Cells.Add(tc_User_Name);
            tr_header.BackColor = System.Drawing.Color.Bisque;

            TableCell tc_FF = new TableCell();
            tc_FF.BorderStyle = BorderStyle.Solid;
            tc_FF.BorderWidth = 1;
            tc_FF.Width = 600;
            Literal lit_FF = new Literal();
            lit_FF.Text = "<center>Field Force Name</center>";
            tc_FF.Controls.Add(lit_FF);
            tr_header.Cells.Add(tc_FF);

            TableCell tc_day_hr = new TableCell();
            tc_day_hr.BorderStyle = BorderStyle.Solid;
            tc_day_hr.BorderWidth = 1;
            tc_day_hr.Width = 80;
            Literal lit_day_hr = new Literal();
            lit_day_hr.Text = "<center>Day</center>";
            tc_day_hr.Controls.Add(lit_day_hr);
            tr_header.Cells.Add(tc_day_hr);

            tbl.Rows.Add(tr_header);

            tot_days = getmaxdays_month(cmonth);

            TableRow tr_day_header = new TableRow();
            tr_day_header.BorderStyle = BorderStyle.Solid;
            tr_day_header.BorderWidth = 1;

            while (cday <= tot_days)
            {
                TableCell tc_day = new TableCell();
                tc_day.BorderStyle = BorderStyle.Solid;
                tc_day.BorderWidth = 1;
                tc_day.Width = 50;
                tc_day.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_day = new Literal();
                lit_day.Text = cday.ToString();
                tc_day.Controls.Add(lit_day);
                tr_header.Cells.Add(tc_day);

                cday = cday + 1;
            }

            TableCell tc_total = new TableCell();
            tc_total.BorderStyle = BorderStyle.Solid;
            tc_total.BorderWidth = 1;
            tc_total.Width = 100;
            Literal lit_total = new Literal();
            lit_total.Text = "<center>Total</center>";
            tc_total.Controls.Add(lit_total);
            tr_header.Cells.Add(tc_total);

            tbl.Rows.Add(tr_day_header);

            // Details Section
            DCR dc = new DCR();

            string sURL = string.Empty;
            int iCount = 0;

            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                if (ViewState["HQ_Det"] != null)
                {
                    if (drFF["SF_Code"].ToString().Contains("MGR"))
                    {
                        iColorCount += 1;
                        string StrColor = GetCaseColor(Convert.ToString(iColorCount));
                        TableRow tr_HQ_det = new TableRow();
                        TableCell tc_HQ_det = new TableCell();
                        Literal lit_HQ_det = new Literal();
                        lit_HQ_det.Text = drFF["sf_hq"].ToString();
                        tc_HQ_det.BorderStyle = BorderStyle.Solid;
                        tc_HQ_det.BorderWidth = 1;
                        tc_HQ_det.Controls.Add(lit_HQ_det);
                        tc_HQ_det.HorizontalAlign = HorizontalAlign.Left;
                        tc_HQ_det.ColumnSpan = 35;
                        tc_HQ_det.BackColor = System.Drawing.ColorTranslator.FromHtml(StrColor);
                        tr_HQ_det.Cells.Add(tc_HQ_det);
                        tbl.Rows.Add(tr_HQ_det);
                        ViewState["HQ_Det"] = drFF["sf_hq"].ToString();
                    }
                }
                else
                {
                    TableRow tr_HQ_det = new TableRow();
                    TableCell tc_HQ_det = new TableCell();
                    Literal lit_HQ_det = new Literal();
                    lit_HQ_det.Text = drFF["sf_hq"].ToString();
                    tc_HQ_det.BorderStyle = BorderStyle.Solid;
                    tc_HQ_det.BorderWidth = 1;
                    tc_HQ_det.Controls.Add(lit_HQ_det);
                    tc_HQ_det.ColumnSpan = 35;
                    tc_HQ_det.HorizontalAlign = HorizontalAlign.Left;
                    tc_HQ_det.BackColor = System.Drawing.Color.LightYellow;
                    tr_HQ_det.Cells.Add(tc_HQ_det);
                    tbl.Rows.Add(tr_HQ_det);
                    ViewState["HQ_Det"] = drFF["sf_hq"].ToString();
                }

                ViewState["HQ_Det"] = drFF["sf_hq"].ToString();

                TableRow tr_det = new TableRow();
                TableRow tr_det2 = new TableRow();
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.RowSpan = 2;
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                //tc_det_SNo.Height = 10;
                tr_det.Cells.Add(tc_det_SNo);
                //tr_det.Height = 10;

                TableCell tc_det_User_Name = new TableCell();
                Literal lit_det_User_Name = new Literal();
                lit_det_User_Name.Text = "&nbsp;" + drFF["UsrDfd_UserName"].ToString();
                tc_det_User_Name.RowSpan = 2;
                tc_det_User_Name.Width = 110;
                tc_det_User_Name.BorderStyle = BorderStyle.Solid;
                tc_det_User_Name.BorderWidth = 1;
                tc_det_User_Name.Controls.Add(lit_det_User_Name);
                //tc_det_SNo.Height = 10;
                tr_det.Cells.Add(tc_det_User_Name);
                //tr_det.Height = 10;

                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();

                SalesForce sf1 = new SalesForce();
                dsSf = sf1.CheckSFNameVacant(drFF["sf_code"].ToString(), cmonth, cyear);
                if (dsSf.Tables[0].Rows.Count > 0)
                {
                    int i = dsSf.Tables[0].Rows.Count - 1;
                    string sf_name = dsSf.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    //lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString() + "( " + sf_name + " )" + " - " + drFF["sf_hq"].ToString() + " - " + drFF["Designation_Short_Name"].ToString();
                    lit_det_FF.Text = "&nbsp;" +  sf_name +  " - " + drFF["sf_hq"].ToString() + " - " + drFF["Designation_Short_Name"].ToString();
                }
                else
                {
                    lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString() + " - " + drFF["sf_hq"].ToString() + " - " + drFF["Designation_Short_Name"].ToString();
                }

                //if (sf_type == "1")
                //{
                //    lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString() + " - " + drFF["sf_hq"].ToString() + " - " + drFF["sf_Designation_Short_Name"].ToString();
                //}
                //else
                //{
                //lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString() + " - " + drFF["sf_hq"].ToString() + " - " + drFF["Designation_Short_Name"].ToString();
                //}
                tc_det_FF.HorizontalAlign = HorizontalAlign.Left;
                tc_det_FF.RowSpan = 2;
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                TableCell tc_det_sub = new TableCell();
                Literal lit_det_sub = new Literal();

                lit_det_sub.Text = "&nbsp; Sub.On";
                tc_det_sub.BorderStyle = BorderStyle.Solid;
                tc_det_sub.BorderWidth = 1;
                tc_det_sub.Controls.Add(lit_det_sub);
                tr_det.Cells.Add(tc_det_sub);

                TableCell tc_det_DR = new TableCell();
                Literal lit_det_DR = new Literal();
                lit_det_DR.Text = "&nbsp; Drs";
                tc_det_DR.BorderStyle = BorderStyle.Solid;
                tc_det_DR.BorderWidth = 1;
                tc_det_DR.Controls.Add(lit_det_DR);
                tr_det2.Cells.Add(tc_det_DR);

                sDCR = " - ";

                SalesForce sf = new SalesForce();
                dsSf = sf.CheckSFType(drFF["sf_code"].ToString());
                if (dsSf.Tables[0].Rows.Count > 0)
                {
                    sf_type = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }

                dsDCR = dc.getDCR_Report_Det_New(drFF["sf_code"].ToString(), div_code,cmonth, cyear, sf_type);
                if (dsDCR.Tables[0].Rows.Count > 0)
                {
                    ddate = 1;
                    foreach (DataRow datarow in dsDCR.Tables[0].Rows)
                    {
                        while (ddate <= Convert.ToInt16(datarow["Activity_Date"].ToString()))
                        {
                            if (ddate == Convert.ToInt16(datarow["Activity_Date"].ToString()))
                            {
                                sDCR = datarow["Submission_Date"].ToString();
                            }
                            else
                            {
                                sDCR = " - ";
                            }

                            TableCell tc_det_day = new TableCell();
                            tc_det_day.BorderStyle = BorderStyle.Solid;
                            tc_det_day.BorderWidth = 1;
                            tc_det_day.Width = 50;
                            tc_det_day.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_day = new Literal();
                            lit_det_day.Text = sDCR;
                            tc_det_day.Controls.Add(lit_det_day);
                            tr_det.Cells.Add(tc_det_day);

                            dsDelay = dc.get_DCR_Status_Delay(drFF["sf_code"].ToString(), datarow["Activity_Date"].ToString(), cmonth, cyear);
                            if (dsDelay.Tables[0].Rows.Count > 0)
                            {
                                tc_det_day.BackColor = System.Drawing.ColorTranslator.FromHtml("#eee8aa");
                            }
                            ddate = ddate + 1;
                        }
                    }

                    sDCR = " - ";
                    if (tot_days >= ddate)
                    {
                        cday = ddate;
                        while (cday <= tot_days)
                        {
                            TableCell tc_det_day = new TableCell();
                            tc_det_day.BorderStyle = BorderStyle.Solid;
                            tc_det_day.BorderWidth = 1;
                            tc_det_day.Width = 50;
                            tc_det_day.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_day = new Literal();
                            lit_det_day.Text = sDCR;
                            tc_det_day.Controls.Add(lit_det_day);
                            tr_det.Cells.Add(tc_det_day);

                            cday = cday + 1;
                        }
                    }
                }
                else
                {
                    sDCR = " - ";
                    ddate = 1;
                    if (tot_days >= ddate)
                    {
                        cday = ddate;
                        while (cday <= tot_days)
                        {
                            TableCell tc_det_day = new TableCell();
                            tc_det_day.BorderStyle = BorderStyle.Solid;
                            tc_det_day.BorderWidth = 1;
                            tc_det_day.Width = 50;
                            tc_det_day.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_day = new Literal();
                            lit_det_day.Text = sDCR;
                            tc_det_day.Controls.Add(lit_det_day);
                            tr_det.Cells.Add(tc_det_day);

                            cday = cday + 1;
                        }
                    }
                }
              
                tbl.Rows.Add(tr_det);
                tbl.Visible = true;

                tot = 0;
                if (dsDCR.Tables[0].Rows.Count > 0)
                {
                    docdate = 1;
                    foreach (DataRow datarow in dsDCR.Tables[0].Rows)
                    {
                       
                        while (docdate <= Convert.ToInt16(datarow["Activity_Date"].ToString()))
                        {
                            sDCR = "";
                            if (docdate == Convert.ToInt16(datarow["Activity_Date"].ToString()))
                            {
                                if (datarow["Doc_count"].ToString() != "0")
                                    sDCR = datarow["Doc_count"].ToString();
                                else
                                    sDCR = datarow["WType_SName"].ToString();
                            }


                            TableCell tc_det_day = new TableCell();
                            tc_det_day.BorderStyle = BorderStyle.Solid;
                            tc_det_day.BorderWidth = 1;
                            tc_det_day.Width = 50;
                            tc_det_day.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_day.BackColor = System.Drawing.Color.White;
                            Literal lit_det_day = new Literal();
                            lit_det_day.Text = sDCR;
                            tc_det_day.Controls.Add(lit_det_day);
                            tr_det2.Cells.Add(tc_det_day);

                            // Changes done by saro
                            if (datarow["Doc_count"].ToString() != "0" && sDCR.Trim() != "-" && sDCR.Trim() != "")  
                                tot = tot + Convert.ToInt32(sDCR);

                            dsDelay = dc.get_DCR_Status_Delay(drFF["sf_code"].ToString(), datarow["Activity_Date"].ToString(), cmonth, cyear);
                            if (dsDelay.Tables[0].Rows.Count > 0)
                            {
                                tc_det_day.BackColor = System.Drawing.ColorTranslator.FromHtml("#eee8aa");
                            }
                            docdate = docdate + 1;
                        }
                    }
                    sDCR = " - ";
                    if (tot_days >= docdate)
                    {
                        cday = docdate;
                        while (cday <= tot_days)
                        {
                            TableCell tc_det_day = new TableCell();
                            tc_det_day.BorderStyle = BorderStyle.Solid;
                            tc_det_day.BorderWidth = 1;
                            tc_det_day.Width = 50;
                            tc_det_day.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_day = new Literal();
                            lit_det_day.Text = sDCR;
                            tc_det_day.Controls.Add(lit_det_day);
                            tr_det2.Cells.Add(tc_det_day);
                            cday = cday + 1;
                        }
                    }
                }
                else
                {
                    sDCR = " - ";
                    docdate = 1;
                    if (tot_days >= docdate)
                    {
                        cday = docdate;
                        while (cday <= tot_days)
                        {
                            TableCell tc_det_day = new TableCell();
                            tc_det_day.BorderStyle = BorderStyle.Solid;
                            tc_det_day.BorderWidth = 1;
                            tc_det_day.Width = 50;
                            tc_det_day.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_day = new Literal();
                            lit_det_day.Text = sDCR;
                            tc_det_day.Controls.Add(lit_det_day);
                            tr_det2.Cells.Add(tc_det_day);

                            cday = cday + 1;
                        }
                    }
                }

                TableCell tc_tot = new TableCell();
                tc_tot.BorderStyle = BorderStyle.Solid;
                tc_tot.BorderWidth = 1;
                tc_tot.Width = 50;
                tc_tot.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_det_tot = new Literal();
                lit_det_tot.Text = tot.ToString();
                tc_tot.RowSpan = 2;
                tc_tot.Controls.Add(lit_det_tot);
                tr_det.Cells.Add(tc_tot);

                tbl.Rows.Add(tr_det2);
                ViewState["dynamictable"] = true;
            }
        }
    }

    private string GetCaseColor(string strCount)
    {
        switch (strCount)
        {
            case "1":
                strCase = "#f0fff0";
                break;
            case "2":
                strCase = "#f0ffff";
                break;
            case "3":
                strCase = "#f5fffa";
                break;
            case "4":
                strCase = "#f8f8ff";
                break;
            case "5":
                strCase = "#faebd7";
                break;
            case "6":
                strCase = "#faf0e6";
                break;
            case "7":
                strCase = "#fafad2";
                break;
            case "8":
                strCase = "#e0ffff";
                break;
            case "9":
                strCase = "#e6e6fa";
                break;

            default:
                strCase = "#e0ffff";
                break;
        }
        return strCase;
    }

    private int getmaxdays_month(int imonth)
    {
        int idays = -1;

        if (imonth == 1)
            idays = 31;
        else if (imonth == 2)
            idays = 29;
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
        string attachment = "attachment; filename=DCR_Status_Detailed.xls";
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
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "DCR_Status_Detailed";
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
}