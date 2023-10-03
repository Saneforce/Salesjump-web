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


public partial class Reports_rptCallAverage : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode=string.Empty;   
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;    
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;   
    string strMode = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    SalesForce sf = new SalesForce();
    DCR dcc = new DCR();
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    int fldwrk_total = 0;
    int doctor_total = 0;   
    int Chemist_total = 0;
    int Stock_toatal = 0;
    int Stock_calls_Seen_Total = 0;
    int ChemistPOB_total = 0;
    int UnListDoc = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    int CSH_calls_seen_total = 0;
    int Dcr_Sub_days = 0;
    int Dcr_Leave = 0;
    double dblCoverage = 0.00;
    int UnLstdoc_calls_seen_total = 0;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string ChemistPOB_visit = string.Empty;
    
    string tot_Sub_days = string.Empty;
    string tot_dr = string.Empty;
    string Chemist_visit = string.Empty;
    string Stock_Visit = string.Empty;
    string tot_Stock_Calls_Seen = string.Empty;
    string tot_Dcr_Leave = string.Empty;
    string UnlistVisit = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string tot_doc_Unlstcalls_seen = string.Empty;
    string tot_CSH_calls_seen = string.Empty;
    
    string sCurrentDate = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
            divcode=Request.QueryString["div_Code"].ToString();
            sfCode = Request.QueryString["sf_code"].ToString();
            sfname = Request.QueryString["sf_name"].ToString(); 
            strMode = Request.QueryString["Mode"].ToString();

            if (Session["sf_type"].ToString() == "1")
            {                
                sfCode = Session["sf_code"].ToString();
                sfname = Session["sf_name"].ToString();
            }

            lblRegionName.Text = sfname;
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            //if (!Page.IsPostBack)
            //{
                //BindGrid();
                if (strMode == "MonthWise")
                {
                    iMonth = Convert.ToInt32(Request.QueryString["frm_month"].ToString());
                    iYear = Convert.ToInt32(Request.QueryString["frm_year"].ToString());
                    FillSF();
                    
                    string strMonthName = mfi.GetMonthName(iMonth).ToString();
                    lblMonth.Text = strMonthName;
                    lblYear.Text = iYear.ToString();
                }
                else if (strMode == "Periodically")
                {
                    FYear = Request.QueryString["frm_year"].ToString();
                    FMonth = Request.QueryString["frm_month"].ToString();
                    TYear = Request.QueryString["To_Year"].ToString();
                    TMonth = Request.QueryString["To_Month"].ToString();
                    FillPeriodically();
                   
                    string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);                    
                    string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

                    lblHead.Text = "Listed Doctor Call Average " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;
                    lblIDMonth.Visible = false;
                    lblIDYear.Visible = false;
                }
                else if (strMode == "Periodically All Field Force")
                {
                    FYear = Request.QueryString["frm_year"].ToString();
                    FMonth = Request.QueryString["frm_month"].ToString();
                    TYear = Request.QueryString["To_Year"].ToString();
                    TMonth = Request.QueryString["To_Month"].ToString();
                    FillPeriodically_FieldForce();
                    string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
                    string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

                    lblHead.Text = "Listed Doctor Call Average " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;
                    lblIDMonth.Visible = false;
                    lblIDYear.Visible = false;
                }

                ExportButton();
                
            //}

        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect(ex.Message);
        //}
    }
    private void ExportButton()
    {
        btnPDF.Visible = false;
    }
    private void BindSf_Code()
    {
        try
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                DataSet dsmgrsf = new DataSet();
                SalesForce ds = new SalesForce();
                DataSet DsAudit = ds.SF_Hierarchy(divcode, sfCode);
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = sf.getUserListReportingTo(divcode, sfCode, 0);
                    dsmgrsf.Tables.Add(dt);
                    dsSalesForce = dsmgrsf;
                }
                else
                {
                    DataTable dt = ds.getAuditManagerTeam(divcode, sfCode, 0);
                    dsmgrsf.Tables.Add(dt);
                    dsSalesForce = dsmgrsf;
                }
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                divcode = Session["div_code"].ToString();

                DataSet dsmgrsf = new DataSet();
                SalesForce ds = new SalesForce();
                DataSet DsAudit = ds.SF_Hierarchy(divcode, sfCode);
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = sf.getUserListReportingTo(divcode, sfCode, 0);
                    dsmgrsf.Tables.Add(dt);
                    dsSalesForce = dsmgrsf;
                }
                else
                {
                    DataTable dt = ds.getAuditManagerTeam(divcode, sfCode, 0);
                    dsmgrsf.Tables.Add(dt);
                    dsSalesForce = dsmgrsf;
                }
            }

            else if (Session["sf_type"].ToString() == "1")
            {
                divcode = Session["div_code"].ToString();
                dsSalesForce = sf.sp_UserMRLogin(divcode, sfCode);
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void FillSF()
    {
        tbl.Rows.Clear();
        doctor_total = 0;
        BindSf_Code(); 

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            
            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 2;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_SNo.Style.Add("color", "white");
            tc_SNo.Style.Add("font-weight", "bold");
            tc_SNo.Style.Add("border-color", "Black");
            tc_SNo.HorizontalAlign = HorizontalAlign.Center;           
            tc_SNo.Style.Add("font-family", "Calibri");
            tr_header.Cells.Add(tc_SNo);
            //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 40;
            tc_DR_Code.RowSpan = 2;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_DR_Code.Style.Add("color", "white");
            tc_DR_Code.Style.Add("font-weight", "bold");
            tc_DR_Code.Style.Add("font-family", "Calibri");
            tc_DR_Code.Style.Add("border-color", "Black");
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;
            tc_DR_Name.RowSpan = 2;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Field Force</center>";
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tc_DR_Name.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");            
            tc_DR_Name.Style.Add("color", "white");
            tc_DR_Name.Style.Add("font-weight", "bold");          
            tc_DR_Name.Style.Add("font-family", "Calibri");
            tc_DR_Name.Style.Add("border-color", "Black");
            tr_header.Cells.Add(tc_DR_Name);

            //TableCell tc_DR_HQ = new TableCell();
            //tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            //tc_DR_HQ.BorderWidth = 1;
            //tc_DR_HQ.Width = 50;
            //tc_DR_HQ.RowSpan = 2;
            //Literal lit_DR_HQ = new Literal();
            //lit_DR_HQ.Text = "<center>HQ</center>";
            //tc_DR_HQ.Controls.Add(lit_DR_HQ);
            //tc_DR_HQ.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            //tc_DR_HQ.Style.Add("color", "white");
            //tc_DR_HQ.Style.Add("font-weight", "bold");
            //tc_DR_HQ.Style.Add("font-family", "Calibri");
            //tc_DR_HQ.Style.Add("border-color", "Black");
            //tr_header.Cells.Add(tc_DR_HQ);

            //TableCell tc_DR_Des = new TableCell();
            //tc_DR_Des.BorderStyle = BorderStyle.Solid;
            //tc_DR_Des.BorderWidth = 1;
            //tc_DR_Des.Width = 50;
            //tc_DR_Des.RowSpan = 2;
            //Literal lit_DR_Des = new Literal();
            //lit_DR_Des.Text = "<center>Designation</center>";
            //tc_DR_Des.HorizontalAlign = HorizontalAlign.Center;
            //tc_DR_Des.Controls.Add(lit_DR_Des);
            //tc_DR_Des.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            //tc_DR_Des.Style.Add("color", "white");
            //tc_DR_Des.Style.Add("font-weight", "bold");
            //tc_DR_Des.Style.Add("font-family", "Calibri");
            //tc_DR_Des.Style.Add("border-color", "Black");
            //tr_header.Cells.Add(tc_DR_Des);

            TableCell tc_DR_DOJ = new TableCell();
            tc_DR_DOJ.BorderStyle = BorderStyle.Solid;
            tc_DR_DOJ.BorderWidth = 1;
            tc_DR_DOJ.Width = 100;
            tc_DR_DOJ.RowSpan = 2;
            Literal lit_DR_DOJ = new Literal();
            lit_DR_DOJ.Text = "<center>DOJ</center>";
            tc_DR_DOJ.Controls.Add(lit_DR_DOJ);
            tc_DR_DOJ.HorizontalAlign = HorizontalAlign.Center;
            tc_DR_DOJ.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_DR_DOJ.Style.Add("color", "white");
            tc_DR_DOJ.Style.Add("font-weight", "bold");
            tc_DR_DOJ.Style.Add("font-family", "Calibri");
            tc_DR_DOJ.Style.Add("border-color", "Black");
            tr_header.Cells.Add(tc_DR_DOJ);

            int months = Convert.ToInt32(iMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            

            ViewState["months"] = months;
            

            Doctor dr = new Doctor();
            dsDoctor = dr.getDocCat(divcode);


            tbl.Rows.Add(tr_header);

            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());

            if (months > 0)
            {
                    TableRow tr_lst_det = new TableRow();
                        
                    TableCell tc_DR_DCR_SubDays = new TableCell();
                    tc_DR_DCR_SubDays.BorderStyle = BorderStyle.Solid;
                    tc_DR_DCR_SubDays.BorderWidth = 1;                
                    tc_DR_DCR_SubDays.Width = 50;
                    
                    Literal lit_DR_DCR_SubDays = new Literal();
                    lit_DR_DCR_SubDays.Text = "<center>Dcr Sub.Days</center>";
                    tc_DR_DCR_SubDays.Controls.Add(lit_DR_DCR_SubDays);
                    tc_DR_DCR_SubDays.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_DR_DCR_SubDays.Style.Add("color", "white");                    
                    tc_DR_DCR_SubDays.Style.Add("font-weight", "bold");
                    tc_DR_DCR_SubDays.Style.Add("font-family", "Calibri");
                    tc_DR_DCR_SubDays.Style.Add("border-color", "Black");
                    tr_lst_det.Cells.Add(tc_DR_DCR_SubDays);
                    
                    TableCell tc_DR_FldWrk = new TableCell();
                    tc_DR_FldWrk.BorderStyle = BorderStyle.Solid;
                    tc_DR_FldWrk.BorderWidth = 1;
                    //tc_DR_FldWrk.BackColor = System.Drawing.Color.LavenderBlush;
                    tc_DR_FldWrk.Width = 50;
                    Literal lit_DR_FldWrk = new Literal();
                    lit_DR_FldWrk.Text = "<center>No.of FWD</center>";
                    tc_DR_FldWrk.Controls.Add(lit_DR_FldWrk);
                    tc_DR_FldWrk.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_DR_FldWrk.Style.Add("color", "white");
                    tc_DR_FldWrk.Style.Add("font-weight", "bold");
                    tc_DR_FldWrk.Style.Add("font-family", "Calibri");
                    tc_DR_FldWrk.Style.Add("border-color", "Black");
                    tr_lst_det.Cells.Add(tc_DR_FldWrk);

                    TableCell tc_DR_Leave = new TableCell();
                    tc_DR_Leave.BorderStyle = BorderStyle.Solid;
                    tc_DR_Leave.BorderWidth = 1;
                    //tc_DR_Leave.BackColor = System.Drawing.Color.LavenderBlush;
                    tc_DR_Leave.Width = 50;
                    Literal lit_DR_Leave = new Literal();
                    lit_DR_Leave.Text = "<center>Leave</center>";
                    tc_DR_Leave.Controls.Add(lit_DR_Leave);
                    tc_DR_Leave.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_DR_Leave.Style.Add("color", "white");
                    tc_DR_Leave.Style.Add("font-weight", "bold");
                    tc_DR_Leave.Style.Add("font-family", "Calibri");
                    tc_DR_Leave.Style.Add("border-color", "Black");
                    tr_lst_det.Cells.Add(tc_DR_Leave);

                    TableCell tc_DR_Total = new TableCell();
                    tc_DR_Total.BorderStyle = BorderStyle.Solid;
                    tc_DR_Total.BorderWidth = 1;
                    tc_DR_Total.Width = 50;
                    Literal lit_DR_Total = new Literal();
                    lit_DR_Total.Text = "<center>LDrs <br>Seen</center>";
                    tc_DR_Total.Controls.Add(lit_DR_Total);
                    tc_DR_Total.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_DR_Total.Style.Add("color", "white");
                    tc_DR_Total.Style.Add("font-weight", "bold");
                    tc_DR_Total.Style.Add("font-family", "Calibri");
                    tc_DR_Total.Style.Add("border-color", "Black");
                    tr_lst_det.Cells.Add(tc_DR_Total);

                    TableCell tc_average = new TableCell();
                    tc_average.BorderStyle = BorderStyle.Solid;
                    tc_average.BorderWidth = 1;
                    tc_average.Width = 50;
                    Literal lit_average = new Literal();
                    lit_average.Text = "<center>Call <br>Average </center>";
                    tc_average.Controls.Add(lit_average);
                    tc_average.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_average.Style.Add("color", "white");
                    tc_average.Style.Add("font-weight", "bold");
                    tc_average.Style.Add("font-family", "Calibri");
                    tc_average.Style.Add("border-color", "Black");
                    tr_lst_det.Cells.Add(tc_average);

                    TableCell tc_Docs_chemmet = new TableCell();
                    tc_Docs_chemmet.BorderStyle = BorderStyle.Solid;
                    tc_Docs_chemmet.BorderWidth = 1;
                    tc_Docs_chemmet.Width = 50;
                    Literal lit_Docs_Chemmet = new Literal();
                    lit_Docs_Chemmet.Text = "<center>Chmist <br> Seen</center>";
                    tc_Docs_chemmet.Controls.Add(lit_Docs_Chemmet);
                    tc_Docs_chemmet.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_Docs_chemmet.Style.Add("color", "white");
                    tc_Docs_chemmet.Style.Add("font-weight", "bold");
                    tc_Docs_chemmet.Style.Add("font-family", "Calibri");
                    tc_Docs_chemmet.Style.Add("border-color", "Black");
                    tr_lst_det.Cells.Add(tc_Docs_chemmet);

                    TableCell tc_Docs_CallAvg = new TableCell();
                    tc_Docs_CallAvg.BorderStyle = BorderStyle.Solid;
                    tc_Docs_CallAvg.BorderWidth = 1;
                    tc_Docs_CallAvg.Width = 50;
                    Literal lit_Docs_CallAvg = new Literal();
                    lit_Docs_CallAvg.Text = "<center>Call <br> Avg</center>";
                    tc_Docs_CallAvg.Controls.Add(lit_Docs_CallAvg);
                    tc_Docs_CallAvg.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_Docs_CallAvg.Style.Add("color", "white");
                    tc_Docs_CallAvg.Style.Add("font-weight", "bold");
                    tc_Docs_CallAvg.Style.Add("font-family", "Calibri");
                    tc_Docs_CallAvg.Style.Add("border-color", "Black");
                    tr_lst_det.Cells.Add(tc_Docs_CallAvg);

                    TableCell tc_Docs_ChemPOB = new TableCell();
                    tc_Docs_ChemPOB.BorderStyle = BorderStyle.Solid;
                    tc_Docs_ChemPOB.BorderWidth = 1;
                    tc_Docs_ChemPOB.Width = 50;
                    Literal lit_Docs_ChemPOB = new Literal();
                    lit_Docs_ChemPOB.Text = "<center>Chem.POB</center>";
                    tc_Docs_ChemPOB.Visible=false;
                    tc_Docs_ChemPOB.Controls.Add(lit_Docs_ChemPOB);
                    tc_Docs_ChemPOB.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_Docs_ChemPOB.Style.Add("color", "white");
                    tc_Docs_ChemPOB.Style.Add("font-weight", "bold");
                    tc_Docs_ChemPOB.Style.Add("font-family", "Calibri");
                    tc_Docs_ChemPOB.Style.Add("border-color", "Black");
                    tr_lst_det.Cells.Add(tc_Docs_ChemPOB);

                    TableCell tc_Docs_Stockmet = new TableCell();
                    tc_Docs_Stockmet.BorderStyle = BorderStyle.Solid;
                    tc_Docs_Stockmet.BorderWidth = 1;
                    tc_Docs_Stockmet.Width = 50;
                    Literal lit_Docs_Stockmet = new Literal();
                    lit_Docs_Stockmet.Text = "<center>Stockist <br> Seen</center>";
                    tc_Docs_Stockmet.Controls.Add(lit_Docs_Stockmet);
                    tc_Docs_Stockmet.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_Docs_Stockmet.Style.Add("color", "white");
                    tc_Docs_Stockmet.Style.Add("font-weight", "bold");
                    tc_Docs_Stockmet.Style.Add("font-family", "Calibri");
                    tc_Docs_Stockmet.Style.Add("border-color", "Black");
                    tr_lst_det.Cells.Add(tc_Docs_Stockmet);

                    TableCell tc_Docs_Stock_CallAvg = new TableCell();
                    tc_Docs_Stock_CallAvg.BorderStyle = BorderStyle.Solid;
                    tc_Docs_Stock_CallAvg.BorderWidth = 1;
                    tc_Docs_Stock_CallAvg.Width = 50;
                    Literal lit_Docs_Stock_CallAvg = new Literal();
                    lit_Docs_Stock_CallAvg.Text = "<center>Call <br> Avg</center>";
                    tc_Docs_Stock_CallAvg.Controls.Add(lit_Docs_Stock_CallAvg);
                    tc_Docs_Stock_CallAvg.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_Docs_Stock_CallAvg.Style.Add("color", "white");
                    tc_Docs_Stock_CallAvg.Style.Add("font-weight", "bold");
                    tc_Docs_Stock_CallAvg.Style.Add("font-family", "Calibri");
                    tc_Docs_Stock_CallAvg.Style.Add("border-color", "Black");
                    tr_lst_det.Cells.Add(tc_Docs_Stock_CallAvg);

                    TableCell tc_Docs_New_Drs_met = new TableCell();
                    tc_Docs_New_Drs_met.BorderStyle = BorderStyle.Solid;
                    tc_Docs_New_Drs_met.BorderWidth = 1;
                    tc_Docs_New_Drs_met.Width = 50;
                    Literal lit_Docs_New_Drs_met = new Literal();
                    lit_Docs_New_Drs_met.Text = "<center>UnLDrs <br> Seen</center>";
                    tc_Docs_New_Drs_met.Controls.Add(lit_Docs_New_Drs_met);
                    tc_Docs_New_Drs_met.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_Docs_New_Drs_met.Style.Add("color", "white");
                    tc_Docs_New_Drs_met.Style.Add("font-weight", "bold");
                    tc_Docs_New_Drs_met.Style.Add("font-family", "Calibri");
                    tc_Docs_New_Drs_met.Style.Add("border-color", "Black");
                    tr_lst_det.Cells.Add(tc_Docs_New_Drs_met);

                    TableCell tc_Docs_New_Call_Avg = new TableCell();
                    tc_Docs_New_Call_Avg.BorderStyle = BorderStyle.Solid;
                    tc_Docs_New_Call_Avg.BorderWidth = 1;
                    tc_Docs_New_Call_Avg.Width = 50;
                    Literal lit_Docs_New_Call_Avg = new Literal();
                    lit_Docs_New_Call_Avg.Text = "<center>Call <br> Avg</center>";
                    tc_Docs_New_Call_Avg.Controls.Add(lit_Docs_New_Call_Avg);
                    tc_Docs_New_Call_Avg.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_Docs_New_Call_Avg.Style.Add("color", "white");
                    tc_Docs_New_Call_Avg.Style.Add("font-weight", "bold");
                    tc_Docs_New_Call_Avg.Style.Add("font-family", "Calibri");
                    tc_Docs_New_Call_Avg.Style.Add("border-color", "Black");
                    tr_lst_det.Cells.Add(tc_Docs_New_Call_Avg);

                    TableCell tc_Docs_Remarks = new TableCell();
                    tc_Docs_Remarks.BorderStyle = BorderStyle.Solid;
                    tc_Docs_Remarks.BorderWidth = 1;
                    tc_Docs_Remarks.Width = 50;
                    Literal lit_Docs_Remarks = new Literal();
                    lit_Docs_Remarks.Text = "<center>Remarks</center>";
                    tc_Docs_Remarks.Controls.Add(lit_Docs_Remarks);
                    tc_Docs_Remarks.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_Docs_Remarks.Style.Add("color", "white");
                    tc_Docs_Remarks.Style.Add("font-weight", "bold");
                    tc_Docs_Remarks.Style.Add("font-family", "Calibri");
                    tc_Docs_Remarks.Style.Add("border-color", "Black");
                    tr_lst_det.Cells.Add(tc_Docs_Remarks);                   

                tbl.Rows.Add(tr_lst_det);
            }


            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            int iCnt = 0;
            int imonth = 0;
            int iyear = 0;
            DCR dcs = new DCR();

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                //ListedDR lstDR = new ListedDR();
                //iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                TableRow tr_det = new TableRow();
                if (Session["sf_type"].ToString() == "1")
                {
                    tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["Des_Color"].ToString());
                }
                else
                {
                    tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["Desig_Color"].ToString());
                }

                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Width = 50;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);

                TableCell tc_det_doc_code = new TableCell();
                Literal lit_det_doc_code = new Literal();
                lit_det_doc_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                tc_det_doc_code.BorderStyle = BorderStyle.Solid;
                tc_det_doc_code.BorderWidth = 1;
                tc_det_doc_code.Controls.Add(lit_det_doc_code);
                tc_det_doc_code.Visible = false;
                tr_det.Cells.Add(tc_det_doc_code);

                TableCell tc_det_doc_name = new TableCell();             
                HyperLink lit_det_doc_name = new HyperLink();
                //lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();

                DataSet dsSf = new DataSet();
                SalesForce sf1 = new SalesForce();
                dsSf = sf1.CheckSFNameVacant(drFF["sf_code"].ToString(), iMonth, iYear);
                if (dsSf.Tables[0].Rows.Count > 1)
                {
                    int i = dsSf.Tables[0].Rows.Count - 1;
                    string sf_name = dsSf.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    string[] str = sf_name.Split('(');
                    lit_det_doc_name.Text = str[1];
                    lit_det_doc_name.Text = str[0] + lit_det_doc_name.Text.Replace(str[1], "<span style='color:Red'>" + "(" + str[1] + "</span>");
                }
                else
                {
                    lit_det_doc_name.Text = "&nbsp;" + drFF["sf_name"].ToString();
                }

                tc_det_doc_name.HorizontalAlign = HorizontalAlign.Left;    
                tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                tc_det_doc_name.Style.Add("font-family", "Calibri");
                tc_det_doc_name.BorderWidth = 1;
                tc_det_doc_name.Width = 200;
                tc_det_doc_name.Controls.Add(lit_det_doc_name);
                tr_det.Cells.Add(tc_det_doc_name);

                //TableCell tc_det_sf_HQ = new TableCell();
                //Literal lit_det_sf_hq = new Literal();
                //lit_det_sf_hq.Text = "&nbsp;" + drFF["Sf_HQ"].ToString();
                //tc_det_sf_HQ.Style.Add("font-family", "Calibri");
                //tc_det_sf_HQ.Width = 50;
                //tc_det_sf_HQ.BorderStyle = BorderStyle.Solid;
                //tc_det_sf_HQ.BorderWidth = 1;
                //tc_det_sf_HQ.HorizontalAlign = HorizontalAlign.Left;
                //tc_det_sf_HQ.Controls.Add(lit_det_sf_hq);
                ////tc_det_sf_HQ.Visible = false;
                //tr_det.Cells.Add(tc_det_sf_HQ);


                //TableCell tc_det_sf_des = new TableCell();
                //Literal lit_det_sf_des = new Literal();
                ////lit_det_sf_des.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                //if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "2" || Session["sf_type"].ToString() == "3")
                //{
                //    lit_det_sf_des.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                //}
                //else
                //{
                //    lit_det_sf_des.Text = "&nbsp;" + drFF["sf_Designation_Short_Name"].ToString();
                //}
                //tc_det_sf_des.BorderStyle = BorderStyle.Solid;
                //tc_det_sf_des.BorderWidth = 1;
                //tc_det_sf_des.Style.Add("font-family", "Calibri");
                //tc_det_sf_des.Width = 50;
                //tc_det_sf_des.HorizontalAlign = HorizontalAlign.Center;
                //tc_det_sf_des.Controls.Add(lit_det_sf_des);
                ////tc_det_sf_HQ.Visible = false;
                //tr_det.Cells.Add(tc_det_sf_des);

                TableCell tc_det_sf_doj = new TableCell();
                Literal lit_det_sf_doj = new Literal();
                lit_det_sf_doj.Text = "&nbsp;" + drFF["Sf_Joining_Date"].ToString();
                tc_det_sf_doj.BorderStyle = BorderStyle.Solid;                
                tc_det_sf_doj.BorderWidth = 1;
                tc_det_sf_doj.Style.Add("font-family", "Calibri");
                tc_det_sf_doj.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_doj.Controls.Add(lit_det_sf_doj);
                tc_det_sf_doj.Width = 100;
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_sf_doj);

                months = Convert.ToInt16(ViewState["months"].ToString());

                if (months > 0)
                {
                    
                        tot_fldwrk = "";
                        tot_dr = "";
                        tot_doc_met = "";
                        tot_doc_calls_seen = "";
                        tot_CSH_calls_seen = "";
                        tot_Stock_Calls_Seen = "";
                        fldwrk_total = 0;
                        doctor_total = 0;
                        Chemist_total = 0;
                        Stock_toatal = 0;
                        Stock_calls_Seen_Total = 0;
                        Dcr_Leave = 0;
                        UnListDoc = 0;
                        Dcr_Sub_days = 0;
                        doc_met_total = 0;
                        UnLstdoc_calls_seen_total = 0;
                        doc_calls_seen_total = 0;
                        CSH_calls_seen_total = 0;
                        dblCoverage = 0.00;
                        dblaverage = 0.00;

                        // DCR_Sub_Days
                        // DCR_TotalSubDaysQuery   
                        dsDoc = dcs.DCR_TotalSubDaysQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_Sub_days = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        Dcr_Sub_days = Dcr_Sub_days + Convert.ToInt16(tot_Sub_days);

                        TableCell tc_det_sf_dsd = new TableCell();
                        Literal lit_det_sf_dsd = new Literal();
                        lit_det_sf_dsd.Text = "&nbsp;" + Dcr_Sub_days.ToString();
                        tc_det_sf_dsd.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_dsd.BorderWidth = 1;
                        tc_det_sf_dsd.Style.Add("font-family", "Calibri");
                        tc_det_sf_dsd.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_dsd.Controls.Add(lit_det_sf_dsd);
                        //tc_det_sf_HQ.Visible = false;
                        tr_det.Cells.Add(tc_det_sf_dsd);

                        // DCR_Sub_Days

                        // Field Work
                        if (drFF["sf_code"].ToString().Contains("MR"))
                        {
                            dsDoc = dcs.DCR_TotalFLDWRKQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);
                        }
                        else
                        {
                            dsDoc = dcs.DCR_TotalFLDWRKQuery_MGR(drFF["sf_code"].ToString(), divcode, iMonth, iYear);
                        }

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_fldwrk = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        fldwrk_total = fldwrk_total + Convert.ToInt16(tot_fldwrk);                                          

                        TableCell tc_det_sf_FLDWRK = new TableCell();
                        Literal lit_det_sf_FLDWRK = new Literal();
                        lit_det_sf_FLDWRK.Text = "&nbsp;" + fldwrk_total.ToString();
                        tc_det_sf_FLDWRK.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_FLDWRK.BorderWidth = 1;
                        tc_det_sf_FLDWRK.Style.Add("font-family", "Calibri");
                        tc_det_sf_FLDWRK.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_FLDWRK.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_FLDWRK.Controls.Add(lit_det_sf_FLDWRK);
                        tr_det.Cells.Add(tc_det_sf_FLDWRK);

                        // Field Work 

                        // Leave

                        dsDoc = dcs.DCR_TotalLeaveQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_Dcr_Leave = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        Dcr_Leave = Convert.ToInt16(tot_Dcr_Leave);

                        TableCell tc_det_sf_Leave = new TableCell();
                        Literal lit_det_sf_Leave = new Literal();
                        lit_det_sf_Leave.Text = "&nbsp;" + Dcr_Leave.ToString();
                        tc_det_sf_Leave.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_Leave.Style.Add("font-family", "Calibri");
                        tc_det_sf_Leave.BorderWidth = 1;
                        tc_det_sf_Leave.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_Leave.Controls.Add(lit_det_sf_Leave);
                        //tc_det_sf_HQ.Visible = false;
                        tr_det.Cells.Add(tc_det_sf_Leave);
                       
                        // Leave

                        // Total Doctors
                        sCurrentDate = months  + "-01-" + iYear;
                        dtCurrent = Convert.ToDateTime(sCurrentDate);

                        dsDoc = dcs.New_DCR_Visit_TotalDocQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);
                        
                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        doctor_total = doctor_total + Convert.ToInt16(tot_dr);  

                        // Total Doctors

                        ////DRs Calls Seen

                        //dsDoc = dcs.DCR_Doc_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_doc_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //doc_calls_seen_total = doc_calls_seen_total + Convert.ToInt16(tot_doc_calls_seen);

                        TableCell tc_det_sf_tot_doc = new TableCell();
                        Literal lit_det_sf_tot_doc = new Literal();
                      //  lit_det_sf_tot_doc.Text = "&nbsp;" + doc_calls_seen_total.ToString();
                        lit_det_sf_tot_doc.Text = "&nbsp;" + doctor_total.ToString();
                        tc_det_sf_tot_doc.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_tot_doc.BorderWidth = 1;
                        tc_det_sf_tot_doc.Style.Add("font-family", "Calibri");
                        tc_det_sf_tot_doc.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_doc.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_doc.Controls.Add(lit_det_sf_tot_doc);
                        tr_det.Cells.Add(tc_det_sf_tot_doc);

                        //DRs Calls Seen
                        
                        //Call Average

                        decimal RoundLstCallAvg=new decimal();
                        if (fldwrk_total > 0)
                        {
                            //dblaverage = Convert.ToDouble((Convert.ToDecimal(doc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                            dblaverage = Convert.ToDouble((Convert.ToDecimal(doctor_total) / Convert.ToDecimal(fldwrk_total)));
                            RoundLstCallAvg = Math.Round((decimal)dblaverage, 2);
                        }
                        else
                        {
                            dblaverage = 0;
                        }

                        if (dblaverage != 0.0)
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + RoundLstCallAvg;
                            tc_det_average.BorderStyle = BorderStyle.Solid;
                            tc_det_average.BorderWidth = 1;
                            tc_det_average.Style.Add("font-family", "Calibri");
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }
                        else
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + "0.0";
                            tc_det_average.BorderStyle = BorderStyle.Solid;
                            tc_det_average.BorderWidth = 1;
                            tc_det_average.Style.Add("font-family", "Calibri");
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }

                       // Call Average

                      // Chemist tot

                        dsDoc = dcs.New_DCR_TotalChemistQuery(drFF["sf_code"].ToString(), divcode,iMonth,iYear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            Chemist_visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        Chemist_total = Chemist_total + Convert.ToInt16(Chemist_visit);

                        

                    // Chemist tot

                    //Chemist Seen

                        //dsDoc = dcs.DCR_CSH_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_CSH_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //CSH_calls_seen_total = CSH_calls_seen_total + Convert.ToInt16(tot_CSH_calls_seen);

                        TableCell tc_det_sf_tot_Chemist = new TableCell();
                        Literal lit_det_sf_tot_Chemist = new Literal();
                        //lit_det_sf_tot_Chemist.Text = "&nbsp;" + CSH_calls_seen_total.ToString();
                        lit_det_sf_tot_Chemist.Text = "&nbsp;" + Chemist_total.ToString();

                        tc_det_sf_tot_Chemist.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_tot_Chemist.BorderWidth = 1;
                        tc_det_sf_tot_Chemist.Style.Add("font-family", "Calibri");
                        tc_det_sf_tot_Chemist.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_Chemist.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_Chemist.Controls.Add(lit_det_sf_tot_Chemist);
                        tr_det.Cells.Add(tc_det_sf_tot_Chemist);
                    //Chemist Seen

                     // Chemist Call Average    
                        decimal RoundChemCallAvg = new decimal();
                        if (fldwrk_total > 0)
                        {
                            //dblaverage = Convert.ToDouble((Convert.ToDecimal(CSH_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                            dblaverage = Convert.ToDouble((Convert.ToDecimal(Chemist_total) / Convert.ToDecimal(fldwrk_total)));
                            RoundChemCallAvg = Math.Round((decimal)dblaverage, 2);
                        }
                        else
                        {
                            dblaverage = 0;
                        }

                        if (dblaverage != 0.0)
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + RoundChemCallAvg;
                            tc_det_average.BorderStyle = BorderStyle.Solid;
                            tc_det_average.BorderWidth = 1;
                            tc_det_average.Style.Add("font-family", "Calibri");
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }
                        else
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + "0.0";
                            tc_det_average.BorderStyle = BorderStyle.Solid;
                            tc_det_average.BorderWidth = 1;
                            tc_det_sf_Leave.Style.Add("font-family", "Calibri");
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }

                        // Chemist Call Average
                        
                        // Chemist POB

                        //dsDoc = dcs.Get_Call_Total_Chemist_Visit_Report(drFF["sf_code"].ToString(), divcode);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    ChemistPOB_visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //ChemistPOB_total = ChemistPOB_total + Convert.ToInt16(ChemistPOB_visit);

                        TableCell tc_det_sf_Chemist_POB = new TableCell();
                        Literal lit_det_sf_tot_POB = new Literal();
                        lit_det_sf_tot_POB.Text = "&nbsp;" + 0;
                        tc_det_sf_Chemist_POB.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_Chemist_POB.BorderWidth = 1;
                        tc_det_sf_Chemist_POB.Style.Add("font-family", "Calibri");
                        tc_det_sf_Chemist_POB.Visible = false;
                        tc_det_sf_Chemist_POB.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_Chemist_POB.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_Chemist_POB.Controls.Add(lit_det_sf_tot_POB);
                        tr_det.Cells.Add(tc_det_sf_Chemist_POB);

                    // Chemist POB

                    // Stock tot

                        dsDoc = dcs.New_DCR_TotalStockistQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            Stock_Visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        Stock_toatal = Stock_toatal + Convert.ToInt16(Stock_Visit);
                      

                    // Stock tot
                    
                    //Stock Calls Seen                   


                        //dsDoc = dcs.DCR_Stock_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_Stock_Calls_Seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //Stock_calls_Seen_Total = Stock_calls_Seen_Total + Convert.ToInt16(tot_Stock_Calls_Seen);

                        TableCell tc_det_sf_tot_Stock = new TableCell();
                        Literal lit_det_sf_tot_Stock = new Literal();
                       // lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_calls_Seen_Total.ToString();
                        lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_toatal.ToString();
                        tc_det_sf_tot_Stock.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_tot_Stock.BorderWidth = 1;
                        tc_det_sf_tot_Stock.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_Stock.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_Stock.Controls.Add(lit_det_sf_tot_Stock);
                        tr_det.Cells.Add(tc_det_sf_tot_Stock);

                    //Stock Calls Seen

                   // Call Avg Stock

                        //dsDoc = dcs.Get_Call_Total_Stock_Visit_Report(drFF["sf_code"].ToString(), divcode);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    Stock_Visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //Stock_toatal = Stock_toatal + Convert.ToInt16(Stock_Visit);

                        decimal RoundStockCallAvg = new decimal();
                        if (fldwrk_total > 0)
                        {
                            //dblaverage = Convert.ToDouble((Convert.ToDecimal(Stock_calls_Seen_Total) / Convert.ToDecimal(fldwrk_total)));

                            dblaverage = Convert.ToDouble((Convert.ToDecimal(Stock_toatal) / Convert.ToDecimal(fldwrk_total)));
                            RoundStockCallAvg = Math.Round((decimal)dblaverage, 2);
                            
                        }
                        else
                        {
                            dblaverage = 0;
                        }

                        if (dblaverage != 0.0)
                        {
                            TableCell tc_det_sf_Call_Avg_Stock = new TableCell();
                            Literal lit_det_sf_Call_Avg_Stock = new Literal();
                            lit_det_sf_Call_Avg_Stock.Text = "&nbsp;" + RoundStockCallAvg.ToString();
                            tc_det_sf_Call_Avg_Stock.Style.Add("font-family", "Calibri");
                            tc_det_sf_Call_Avg_Stock.BorderStyle = BorderStyle.Solid;
                            tc_det_sf_Call_Avg_Stock.BorderWidth = 1;
                            tc_det_sf_Call_Avg_Stock.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_Call_Avg_Stock.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_Call_Avg_Stock.Controls.Add(lit_det_sf_Call_Avg_Stock);
                            tr_det.Cells.Add(tc_det_sf_Call_Avg_Stock);
                        }
                        else
                        {
                            TableCell tc_det_sf_Call_Avg_Stock = new TableCell();
                            Literal lit_det_sf_Call_Avg_Stock = new Literal();
                            lit_det_sf_Call_Avg_Stock.Text = "&nbsp;" + "0.0";
                            tc_det_sf_Call_Avg_Stock.BorderStyle = BorderStyle.Solid;
                            tc_det_sf_Call_Avg_Stock.Style.Add("font-family", "Calibri");
                            tc_det_sf_Call_Avg_Stock.BorderWidth = 1;
                            tc_det_sf_Call_Avg_Stock.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_Call_Avg_Stock.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_Call_Avg_Stock.Controls.Add(lit_det_sf_Call_Avg_Stock);
                            tr_det.Cells.Add(tc_det_sf_Call_Avg_Stock);
                        }

                        // Call Avg Stock

                     // Unlist Doc tot
                        
                        dsDoc = dcs.New_DCR_TotalUnlstDocQuery(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            UnlistVisit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        UnListDoc = UnListDoc + Convert.ToInt16(UnlistVisit);

                     // Unlist Doc tot

                     // UnLstDRs Calls Seen

                        //dsDoc = dcs.DCR_Unlst_Doc_Calls_Seen(drFF["sf_code"].ToString(), divcode, iMonth, iYear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_doc_Unlstcalls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //UnLstdoc_calls_seen_total = UnLstdoc_calls_seen_total + Convert.ToInt16(tot_doc_Unlstcalls_seen);

                        TableCell tc_det_sf_UnList_tot_Stock = new TableCell();
                        Literal lit_det_sf_UnList_tot_Stock = new Literal();
                        //lit_det_sf_UnList_tot_Stock.Text = "&nbsp;" + UnLstdoc_calls_seen_total.ToString();
                        lit_det_sf_UnList_tot_Stock.Text = "&nbsp;" + UnListDoc.ToString();
                        tc_det_sf_UnList_tot_Stock.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_UnList_tot_Stock.Style.Add("font-family", "Calibri");
                        tc_det_sf_UnList_tot_Stock.BorderWidth = 1;
                        tc_det_sf_UnList_tot_Stock.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_UnList_tot_Stock.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_UnList_tot_Stock.Controls.Add(lit_det_sf_UnList_tot_Stock);
                        tr_det.Cells.Add(tc_det_sf_UnList_tot_Stock);

                    // UnLstDRs Calls Seen

                    //Call Average
                        decimal RoundUnLstCallAvg = new decimal();
                        if (fldwrk_total > 0)
                        {
                            //dblaverage = Convert.ToDouble((Convert.ToDecimal(UnLstdoc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                            dblaverage = Convert.ToDouble((Convert.ToDecimal(UnListDoc) / Convert.ToDecimal(fldwrk_total)));
                            RoundUnLstCallAvg = Math.Round((decimal)dblaverage, 2);
                        }
                        else
                        {
                            dblaverage = 0;
                        }

                        if (dblaverage != 0.0 && dblaverage != 0)
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + RoundUnLstCallAvg.ToString();
                            tc_det_average.Style.Add("font-family", "Calibri");
                            tc_det_average.BorderStyle = BorderStyle.Solid;
                            tc_det_average.BorderWidth = 1;
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }
                        else
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + "0.0";
                            tc_det_average.Style.Add("font-family", "Calibri");
                            tc_det_average.BorderStyle = BorderStyle.Solid;
                            tc_det_average.BorderWidth = 1;
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }

                 // Call Average 

                 // Remarks

                        TableCell tc_det_doc_Remarks = new TableCell();                        
                        HyperLink lit_det_doc_Remarks = new HyperLink();
                        lit_det_doc_Remarks.Text = "&nbsp;" + "Click here";
                        sURL = "rptRemarks.aspx?sf_Name=" + drFF["SF_Name"].ToString() + "&sf_code=" + drFF["sf_code"].ToString() + "&Year=" + iYear + "&Month="+ iMonth+"";
                      
                        lit_det_doc_Remarks.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0');";
                            lit_det_doc_Remarks.NavigateUrl = "#";
                        
                            tc_det_doc_Remarks.BorderStyle = BorderStyle.Solid;
                            tc_det_doc_Remarks.Style.Add("font-family", "Calibri");
                            tc_det_doc_Remarks.BorderWidth = 1;
                            tc_det_doc_Remarks.Width = 50;
                        
                            tc_det_doc_Remarks.Controls.Add(lit_det_doc_Remarks);
                            tr_det.Cells.Add(tc_det_doc_Remarks);

                // Remarks

                }

                tbl.Rows.Add(tr_det);

            }
        }
    }

    private void FillPeriodically()
    {
        tbl.Rows.Clear();
        doctor_total = 0;
        BindSf_Code();

        
        TableRow tr_lst_det = new TableRow();

        TableCell tc_SNo = new TableCell();
        tc_SNo.BorderStyle = BorderStyle.Solid;
        tc_SNo.BorderWidth = 1;
        tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
        tc_SNo.Style.Add("color", "white");
        tc_SNo.Style.Add("font-weight", "bold");
        tc_SNo.Style.Add("border-color", "Black");
        tc_SNo.Width = 50;      
        Literal lit_SNo = new Literal();
        lit_SNo.Text = "S.No";
        tc_SNo.Controls.Add(lit_SNo);
        tr_lst_det.Cells.Add(tc_SNo);

        TableCell tc_DR_DCR_Month = new TableCell();
        tc_DR_DCR_Month.BorderStyle = BorderStyle.Solid;
        tc_DR_DCR_Month.BorderWidth = 1;
        //tc_DR_DCR_Month.BackColor = System.Drawing.Color.LightSteelBlue;
        tc_DR_DCR_Month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
        tc_DR_DCR_Month.Style.Add("color", "white");
        tc_DR_DCR_Month.Style.Add("font-weight", "bold");
        tc_DR_DCR_Month.Style.Add("border-color", "Black");
        tc_DR_DCR_Month.Style.Add("border-color", "Black");
        tc_DR_DCR_Month.Width = 500;
        Literal lit_DR_DCR_Month = new Literal();
        lit_DR_DCR_Month.Text = "<center>Month</center>";
        tc_DR_DCR_Month.Controls.Add(lit_DR_DCR_Month);
        tr_lst_det.Cells.Add(tc_DR_DCR_Month);

                    TableCell tc_DR_DCR_SubDays = new TableCell();
                    tc_DR_DCR_SubDays.BorderStyle = BorderStyle.Solid;
                    tc_DR_DCR_SubDays.BorderWidth = 1;
                    tc_DR_DCR_SubDays.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_DR_DCR_SubDays.Width = 500;
                    tc_DR_DCR_SubDays.Style.Add("color", "white");
                    tc_DR_DCR_SubDays.Style.Add("font-weight", "bold");
                    tc_DR_DCR_SubDays.Style.Add("border-color", "Black");
                    Literal lit_DR_DCR_SubDays = new Literal();
                    lit_DR_DCR_SubDays.Text = "<center>Dcr Sub.Days</center>";
                    tc_DR_DCR_SubDays.Controls.Add(lit_DR_DCR_SubDays);
                    tr_lst_det.Cells.Add(tc_DR_DCR_SubDays);

                    TableCell tc_DR_FldWrk = new TableCell();
                    tc_DR_FldWrk.BorderStyle = BorderStyle.Solid;
                    tc_DR_FldWrk.BorderWidth = 1;
                    tc_DR_FldWrk.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_DR_FldWrk.Style.Add("color", "white");
                    tc_DR_FldWrk.Style.Add("font-weight", "bold");
                    tc_DR_FldWrk.Style.Add("border-color", "Black");
                    tc_DR_FldWrk.Width = 500;
                    Literal lit_DR_FldWrk = new Literal();
                    lit_DR_FldWrk.Text = "<center>No.of FWD</center>";
                    tc_DR_FldWrk.Controls.Add(lit_DR_FldWrk);
                    tr_lst_det.Cells.Add(tc_DR_FldWrk);

                    TableCell tc_DR_Leave = new TableCell();
                    tc_DR_Leave.BorderStyle = BorderStyle.Solid;
                    tc_DR_Leave.BorderWidth = 1;
                    tc_DR_Leave.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_DR_Leave.Style.Add("color", "white");
                    tc_DR_Leave.Style.Add("font-weight", "bold");
                    tc_DR_Leave.Style.Add("border-color", "Black");
                    tc_DR_Leave.Width = 500;
                    Literal lit_DR_Leave = new Literal();
                    lit_DR_Leave.Text = "<center>Leave</center>";
                    tc_DR_Leave.Controls.Add(lit_DR_Leave);
                    tr_lst_det.Cells.Add(tc_DR_Leave);

                    TableCell tc_DR_Total = new TableCell();
                    tc_DR_Total.BorderStyle = BorderStyle.Solid;
                    tc_DR_Total.BorderWidth = 1;
                    tc_DR_Total.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_DR_Total.Style.Add("color", "white");
                    tc_DR_Total.Style.Add("font-weight", "bold");
                    tc_DR_Total.Style.Add("border-color", "Black");
                    tc_DR_Total.Width = 500;
                    Literal lit_DR_Total = new Literal();
                    lit_DR_Total.Text = "<center>LDrs <br>Seen</center>";
                    tc_DR_Total.Controls.Add(lit_DR_Total);
                    tr_lst_det.Cells.Add(tc_DR_Total);

                    TableCell tc_average = new TableCell();
                    tc_average.BorderStyle = BorderStyle.Solid;
                    tc_average.BorderWidth = 1;
                    tc_average.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_average.Style.Add("color", "white");
                    tc_average.Style.Add("font-weight", "bold");
                    tc_average.Style.Add("border-color", "Black");
                    tc_average.Width = 500;
                    Literal lit_average = new Literal();
                    lit_average.Text = "<center>Call <br>Avg </center>";
                    tc_average.Controls.Add(lit_average);
                    tr_lst_det.Cells.Add(tc_average);

                    TableCell tc_Docs_chemmet = new TableCell();
                    tc_Docs_chemmet.BorderStyle = BorderStyle.Solid;
                    tc_Docs_chemmet.BorderWidth = 1;
                    tc_Docs_chemmet.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_Docs_chemmet.Style.Add("color", "white");
                    tc_Docs_chemmet.Style.Add("font-weight", "bold");
                    tc_Docs_chemmet.Style.Add("border-color", "Black");
                    tc_Docs_chemmet.Width = 500;
                    Literal lit_Docs_Chemmet = new Literal();
                    lit_Docs_Chemmet.Text = "<center>Chmist <br> Seen</center>";
                    tc_Docs_chemmet.Controls.Add(lit_Docs_Chemmet);
                    tr_lst_det.Cells.Add(tc_Docs_chemmet);

                    TableCell tc_Docs_CallAvg = new TableCell();
                    tc_Docs_CallAvg.BorderStyle = BorderStyle.Solid;
                    tc_Docs_CallAvg.BorderWidth = 1;
                    tc_Docs_CallAvg.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_Docs_CallAvg.Style.Add("color", "white");
                    tc_Docs_CallAvg.Style.Add("font-weight", "bold");
                    tc_Docs_CallAvg.Style.Add("border-color", "Black");
                    tc_Docs_CallAvg.Width = 500;
                    Literal lit_Docs_CallAvg = new Literal();
                    lit_Docs_CallAvg.Text = "<center>Call <br> Avg</center>";
                    tc_Docs_CallAvg.Controls.Add(lit_Docs_CallAvg);
                    tr_lst_det.Cells.Add(tc_Docs_CallAvg);

                    TableCell tc_Docs_ChemPOB = new TableCell();
                    tc_Docs_ChemPOB.BorderStyle = BorderStyle.Solid;
                    tc_Docs_ChemPOB.BorderWidth = 1;
                    tc_Docs_ChemPOB.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_Docs_ChemPOB.Style.Add("color", "white");
                    tc_Docs_ChemPOB.Style.Add("font-weight", "bold");
                    tc_Docs_ChemPOB.Style.Add("border-color", "Black");
                    tc_Docs_ChemPOB.Width = 500;
                    Literal lit_Docs_ChemPOB = new Literal();
                    lit_Docs_ChemPOB.Text = "<center>Chem.POB</center>";
                    tc_Docs_ChemPOB.Visible = false;
                    tc_Docs_ChemPOB.Controls.Add(lit_Docs_ChemPOB);
                    tr_lst_det.Cells.Add(tc_Docs_ChemPOB);

                    TableCell tc_Docs_Stockmet = new TableCell();
                    tc_Docs_Stockmet.BorderStyle = BorderStyle.Solid;
                    tc_Docs_Stockmet.BorderWidth = 1;
                    tc_Docs_Stockmet.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_Docs_Stockmet.Style.Add("color", "white");
                    tc_Docs_Stockmet.Style.Add("font-weight", "bold");
                    tc_Docs_Stockmet.Style.Add("border-color", "Black");
                    tc_Docs_Stockmet.Width = 500;
                    Literal lit_Docs_Stockmet = new Literal();
                    lit_Docs_Stockmet.Text = "<center>Stockist <br> Seen</center>";
                    tc_Docs_Stockmet.Controls.Add(lit_Docs_Stockmet);
                    tr_lst_det.Cells.Add(tc_Docs_Stockmet);

                    TableCell tc_Docs_Stock_CallAvg = new TableCell();
                    tc_Docs_Stock_CallAvg.BorderStyle = BorderStyle.Solid;
                    tc_Docs_Stock_CallAvg.BorderWidth = 1;
                    tc_Docs_Stock_CallAvg.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_Docs_Stock_CallAvg.Style.Add("color", "white");
                    tc_Docs_Stock_CallAvg.Style.Add("font-weight", "bold");
                    tc_Docs_Stock_CallAvg.Style.Add("border-color", "Black");
                    tc_Docs_Stock_CallAvg.Width = 500;
                    Literal lit_Docs_Stock_CallAvg = new Literal();
                    lit_Docs_Stock_CallAvg.Text = "<center>Call <br> Avg</center>";
                    tc_Docs_Stock_CallAvg.Controls.Add(lit_Docs_Stock_CallAvg);
                    tr_lst_det.Cells.Add(tc_Docs_Stock_CallAvg);

                    TableCell tc_Docs_New_Drs_met = new TableCell();
                    tc_Docs_New_Drs_met.BorderStyle = BorderStyle.Solid;
                    tc_Docs_New_Drs_met.BorderWidth = 1;
                    tc_Docs_New_Drs_met.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_Docs_New_Drs_met.Style.Add("color", "white");
                    tc_Docs_New_Drs_met.Style.Add("font-weight", "bold");
                    tc_Docs_New_Drs_met.Style.Add("border-color", "Black");
                    tc_Docs_New_Drs_met.Width = 500;
                    Literal lit_Docs_New_Drs_met = new Literal();
                    lit_Docs_New_Drs_met.Text = "<center>UnLDrs <br> Seen</center>";
                    tc_Docs_New_Drs_met.Controls.Add(lit_Docs_New_Drs_met);
                    tr_lst_det.Cells.Add(tc_Docs_New_Drs_met);

                    TableCell tc_Docs_New_Call_Avg = new TableCell();
                    tc_Docs_New_Call_Avg.BorderStyle = BorderStyle.Solid;
                    tc_Docs_New_Call_Avg.BorderWidth = 1;
                    tc_Docs_New_Call_Avg.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_Docs_New_Call_Avg.Style.Add("color", "white");
                    tc_Docs_New_Call_Avg.Style.Add("font-weight", "bold");
                    tc_Docs_New_Call_Avg.Style.Add("border-color", "Black");
                    tc_Docs_New_Call_Avg.Width = 500;
                    Literal lit_Docs_New_Call_Avg = new Literal();
                    lit_Docs_New_Call_Avg.Text = "<center>Call <br> Avg</center>";
                    tc_Docs_New_Call_Avg.Controls.Add(lit_Docs_New_Call_Avg);
                    tr_lst_det.Cells.Add(tc_Docs_New_Call_Avg);

                    TableCell tc_Docs_Remarks = new TableCell();
                    tc_Docs_Remarks.BorderStyle = BorderStyle.Solid;
                    tc_Docs_Remarks.BorderWidth = 1;
                    tc_Docs_Remarks.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_Docs_Remarks.Style.Add("color", "white");
                    tc_Docs_Remarks.Style.Add("font-weight", "bold");
                    tc_Docs_Remarks.Style.Add("border-color", "Black");
                    tc_Docs_Remarks.Width = 500;
                    Literal lit_Docs_Remarks = new Literal();
                    lit_Docs_Remarks.Text = "<center>Remarks</center>";
                    tc_Docs_Remarks.Controls.Add(lit_Docs_Remarks);
                    tr_lst_det.Cells.Add(tc_Docs_Remarks);

                    tbl.Rows.Add(tr_lst_det);

            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            int iCnt = 0;
            int imonth = 0;
            int iyear = 0;
            DCR dcs = new DCR();

            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);

            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            
            int iTotCal = 0;
            int iTotLeave = 0;
            Dcr_Leave = 0;
            int iSumLeave = 0;
            double itotDocavg = 0;
            double itotChemavg = 0;
            int itotStockist = 0;
            double itotStockistavg = 0;
            int itotUnLstDoc = 0;
            double itotUnLstDocavg = 0;
            int iTotChem = 0;
            int iSumChem = 0;
            double iSumDocavg = 0;            
            double iSumChemavg = 0;
            int iSumStock = 0;
            double iSumStockavg = 0;
            int iSumUnLst = 0;
            double iSumUnLstavg = 0;
            int isum = 0;
           
            
            for (int j = 1; j <= months + 1; j++)
            {

             TableRow tr_det = new TableRow();

             if (months > 0)
              {
                   //Sub Header
                 
                  
                    ListedDR lstDR = new ListedDR();

                    iCount += 1;
                    TableCell tc_det_SNo = new TableCell();
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                    tc_det_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_SNo.BorderWidth = 1;
                    tc_det_SNo.Width = 50;
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    tr_det.Cells.Add(tc_det_SNo);

                        TableCell tc_month = new TableCell();
                        Literal lit_month = new Literal();
                        lit_month.Text = "&nbsp;" + sf.getMonthName(cmonth.ToString()).Substring(0,3);
                        tc_month.BorderStyle = BorderStyle.Solid;
                        tc_month.BorderWidth = 1;
                        tc_month.HorizontalAlign = HorizontalAlign.Center;
                        tc_month.Controls.Add(lit_month);
                        tr_det.Cells.Add(tc_month);
                      

                      
                        //months = Convert.ToInt16(ViewState["months"].ToString());
                        //cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                        //cyear = Convert.ToInt16(ViewState["cyear"].ToString());

                       // months = Convert.ToInt16(ViewState["months"].ToString());
                   
                        tot_fldwrk = "";
                        tot_dr = "";
                        tot_doc_met = "";
                        tot_doc_calls_seen = "";
                        tot_Dcr_Leave = "";
                        Chemist_visit = "";
                        Stock_Visit = "";
                        UnlistVisit = "";
                        tot_doc_Unlstcalls_seen = "";
                        tot_CSH_calls_seen = "";
                        tot_Stock_Calls_Seen = "";

                        fldwrk_total = 0;
                        doctor_total = 0;
                        Chemist_total = 0;
                        Stock_toatal = 0;
                        UnListDoc = 0;
                        Dcr_Sub_days = 0;
                        doc_met_total = 0;
                        doc_calls_seen_total = 0;
                        CSH_calls_seen_total = 0;
                        Stock_calls_Seen_Total = 0;
                        dblCoverage = 0.00;
                        dblaverage = 0.00;
                        tot_Sub_days = "";

                        // DCR_Sub_Days
                        
                        dsDoc = dcs.DCR_TotalSubDaysQuery(sfCode, divcode, cmonth, cyear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_Sub_days = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        Dcr_Sub_days = Dcr_Sub_days + Convert.ToInt16(tot_Sub_days);

                        TableCell tc_det_sf_dsd = new TableCell();
                        Literal lit_det_sf_dsd = new Literal();
                        lit_det_sf_dsd.Text = "&nbsp;" + Dcr_Sub_days.ToString();
                        tc_det_sf_dsd.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_dsd.BorderWidth = 1;
                        tc_det_sf_dsd.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_dsd.Controls.Add(lit_det_sf_dsd);
                        //tc_det_sf_HQ.Visible = false;
                        tr_det.Cells.Add(tc_det_sf_dsd);
                        
                        // DCR_Sub_Days

                        // Field Work

                        //dsDoc = dcs.DCR_TotalFLDWRKQuery(sfCode, divcode, cmonth, cyear);

                        if (sfCode.Contains("MR"))
                        {
                            dsDoc = dcs.DCR_TotalFLDWRKQuery(sfCode, divcode, cmonth, cyear);
                        }
                        else
                        {
                            dsDoc = dcs.DCR_TotalFLDWRKQuery_MGR(sfCode, divcode, cmonth, cyear);
                        }

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_fldwrk = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        fldwrk_total = fldwrk_total + Convert.ToInt16(tot_fldwrk);

                        TableCell tc_det_sf_FLDWRK = new TableCell();
                        Literal lit_det_sf_FLDWRK = new Literal();
                        lit_det_sf_FLDWRK.Text = "&nbsp;" + fldwrk_total.ToString();
                        tc_det_sf_FLDWRK.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_FLDWRK.BorderWidth = 1;
                        tc_det_sf_FLDWRK.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_FLDWRK.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_FLDWRK.Controls.Add(lit_det_sf_FLDWRK);
                        tr_det.Cells.Add(tc_det_sf_FLDWRK);

                        // Field Work 

                        // Leave
                        dsDoc = dcs.DCR_TotalLeaveQuery(sfCode, divcode, cmonth, cyear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_Dcr_Leave = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        Dcr_Leave =  Convert.ToInt16(tot_Dcr_Leave);
                        iTotLeave += Dcr_Leave;

                        TableCell tc_det_sf_Leave = new TableCell();
                        Literal lit_det_sf_Leave = new Literal();
                        lit_det_sf_Leave.Text = "&nbsp;" + Dcr_Leave.ToString();
                        tc_det_sf_Leave.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_Leave.BorderWidth = 1;
                        tc_det_sf_Leave.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_Leave.Controls.Add(lit_det_sf_Leave);
                        //tc_det_sf_HQ.Visible = false;
                        tr_det.Cells.Add(tc_det_sf_Leave);

                        // Leave

                        // Total Doctors
                       
                        sCurrentDate = cmonth + "-01-" + cyear;
                        dtCurrent = Convert.ToDateTime(sCurrentDate);
                        dsDoc = dcs.New_DCR_Visit_TotalDocQuery(sfCode, divcode, cmonth, cyear);
                       
                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        doctor_total = Convert.ToInt16(tot_dr);
                                              
                        
                                             
                      
                        // Total Doctors

                        //DRs Calls Seen

                        //dsDoc = dcs.DCR_Doc_Calls_Seen(sfCode, divcode, cmonth, cyear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_doc_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //doc_calls_seen_total = doc_calls_seen_total + Convert.ToInt16(tot_doc_calls_seen);
                        //iTotCal += doc_calls_seen_total;

                        TableCell tc_det_sf_tot_doc = new TableCell();
                        Literal lit_det_sf_tot_doc = new Literal();
                        //lit_det_sf_tot_doc.Text = doc_calls_seen_total.ToString();
                        lit_det_sf_tot_doc.Text = doctor_total.ToString();
                        tc_det_sf_tot_doc.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_tot_doc.BorderWidth = 1;
                        tc_det_sf_tot_doc.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_doc.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_doc.Controls.Add(lit_det_sf_tot_doc);
                        tr_det.Cells.Add(tc_det_sf_tot_doc);  

                        //DRs Calls Seen

                        //Call Average

                        decimal RoundLstCallAvg = new decimal();
                        if (fldwrk_total > 0)
                        {
                           //dblaverage = Convert.ToDouble((Convert.ToDecimal(doctor_total) / Convert.ToDecimal(doc_calls_seen_total)));
                            //dblaverage = Convert.ToDouble((Convert.ToDecimal(doc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                            dblaverage = Convert.ToDouble((Convert.ToDecimal(doctor_total) / Convert.ToDecimal(fldwrk_total)));
                            RoundLstCallAvg = Math.Round((decimal)dblaverage, 2);
                            itotDocavg += Convert.ToDouble(RoundLstCallAvg);
                        }
                        else
                        {
                            dblaverage = 0;
                        }

                        if (dblaverage != 0.0)
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + RoundLstCallAvg.ToString();
                            tc_det_average.BorderStyle = BorderStyle.Solid;
                            tc_det_average.BorderWidth = 1;
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }
                        else
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + "0.0";
                            tc_det_average.BorderStyle = BorderStyle.Solid;
                            tc_det_average.BorderWidth = 1;
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }

                        // Call Average

                        // Chemist tot

                        dsDoc = dcs.New_DCR_TotalChemistQuery(sfCode, divcode, cmonth, cyear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            Chemist_visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        Chemist_total = Chemist_total + Convert.ToInt16(Chemist_visit);
                         

                        // Chemist tot

                        //Chemist Seen

                        //dsDoc = dcs.DCR_CSH_Calls_Seen(sfCode, divcode, cmonth, cyear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_CSH_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //CSH_calls_seen_total = CSH_calls_seen_total + Convert.ToInt16(tot_CSH_calls_seen);
                        //iTotChem += CSH_calls_seen_total; 

                        TableCell tc_det_sf_tot_Chemist = new TableCell();
                        Literal lit_det_sf_tot_Chemist = new Literal();
                        //lit_det_sf_tot_Chemist.Text = "&nbsp;" + CSH_calls_seen_total.ToString();
                        lit_det_sf_tot_Chemist.Text = "&nbsp;" + Chemist_total.ToString();
                        tc_det_sf_tot_Chemist.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_tot_Chemist.BorderWidth = 1;
                        tc_det_sf_tot_Chemist.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_Chemist.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_Chemist.Controls.Add(lit_det_sf_tot_Chemist);
                        tr_det.Cells.Add(tc_det_sf_tot_Chemist);
                        
                        //Chemist Seen

                        // Chemist Call Average    
                        decimal RoundChemavg = new decimal();
                        if (fldwrk_total > 0)
                        {
                           //dblaverage = Convert.ToDouble((Convert.ToDecimal(CSH_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                            dblaverage = Convert.ToDouble((Convert.ToDecimal(Chemist_total) / Convert.ToDecimal(fldwrk_total)));
                           RoundChemavg = Math.Round((decimal)dblaverage, 2);
                           itotChemavg += Convert.ToDouble(RoundChemavg);
                        }
                        else
                        {
                            dblaverage = 0;
                        }

                        if (dblaverage != 0.0)
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + RoundChemavg.ToString();
                            tc_det_average.BorderStyle = BorderStyle.Solid;
                            tc_det_average.BorderWidth = 1;
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }
                        else
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + "0.0";
                            tc_det_average.BorderStyle = BorderStyle.Solid;
                            tc_det_average.BorderWidth = 1;
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }

                        // Chemist Call Average

                        // Chemist POB

                        //dsDoc = dcs.Get_Call_Total_Chemist_Visit_Report(sfCode, divcode);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    ChemistPOB_visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //ChemistPOB_total = ChemistPOB_total + Convert.ToInt16(ChemistPOB_visit);                        

                        TableCell tc_det_sf_Chemist_POB = new TableCell();
                        Literal lit_det_sf_tot_POB = new Literal();
                        lit_det_sf_tot_POB.Text = "&nbsp;" + 0;
                        tc_det_sf_Chemist_POB.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_Chemist_POB.BorderWidth = 1;
                        tc_det_sf_Chemist_POB.Visible = false;
                        tc_det_sf_Chemist_POB.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_Chemist_POB.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_Chemist_POB.Controls.Add(lit_det_sf_tot_POB);
                        tr_det.Cells.Add(tc_det_sf_Chemist_POB);

                        // Chemist POB


                        // Stock tot

                        dsDoc = dcs.New_DCR_TotalStockistQuery(sfCode, divcode, cmonth, cyear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            Stock_Visit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        Stock_toatal = Stock_toatal + Convert.ToInt16(Stock_Visit);
                        itotStockist = Stock_toatal;                       

                        // Stock tot

                        // Stock Calls Seen

                        //dsDoc = dcs.DCR_Stock_Calls_Seen(sfCode, divcode, cmonth, cyear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_Stock_Calls_Seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //Stock_calls_Seen_Total = Stock_calls_Seen_Total + Convert.ToInt16(tot_Stock_Calls_Seen);
                        //itotStockist += Stock_calls_Seen_Total;
                 
                        TableCell tc_det_sf_tot_Stock = new TableCell();
                        Literal lit_det_sf_tot_Stock = new Literal();
                        //lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_calls_Seen_Total.ToString();
                        lit_det_sf_tot_Stock.Text = "&nbsp;" + Stock_toatal.ToString();
                        tc_det_sf_tot_Stock.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_tot_Stock.BorderWidth = 1;
                        tc_det_sf_tot_Stock.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_tot_Stock.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_tot_Stock.Controls.Add(lit_det_sf_tot_Stock);
                        tr_det.Cells.Add(tc_det_sf_tot_Stock);                        

                        // Stock Calls Seen

                        // Call Avg Stock
                        
                        decimal RoundStockistavg = new decimal();
                        if (fldwrk_total > 0)
                        {
                            //dblaverage = Convert.ToDouble((Convert.ToDecimal(Stock_calls_Seen_Total) / Convert.ToDecimal(fldwrk_total)));
                            dblaverage = Convert.ToDouble((Convert.ToDecimal(Stock_toatal) / Convert.ToDecimal(fldwrk_total)));
                            RoundStockistavg += Math.Round((decimal)dblaverage, 2);
                            itotStockistavg += Convert.ToDouble(RoundStockistavg);
                        }
                        else
                        {
                            dblaverage = 0;
                        }

                        if (dblaverage != 0.0)
                        {
                            TableCell tc_det_sf_Call_Avg_Stock = new TableCell();
                            Literal lit_det_sf_Call_Avg_Stock = new Literal();
                            lit_det_sf_Call_Avg_Stock.Text = "&nbsp;" + RoundStockistavg.ToString();
                            tc_det_sf_Call_Avg_Stock.BorderStyle = BorderStyle.Solid;
                            tc_det_sf_Call_Avg_Stock.BorderWidth = 1;
                            tc_det_sf_Call_Avg_Stock.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_Call_Avg_Stock.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_Call_Avg_Stock.Controls.Add(lit_det_sf_Call_Avg_Stock);
                            tr_det.Cells.Add(tc_det_sf_Call_Avg_Stock);
                        }
                        else
                        {
                            TableCell tc_det_sf_Call_Avg_Stock = new TableCell();
                            Literal lit_det_sf_Call_Avg_Stock = new Literal();
                            lit_det_sf_Call_Avg_Stock.Text = "&nbsp;" + "0.0";
                            tc_det_sf_Call_Avg_Stock.BorderStyle = BorderStyle.Solid;
                            tc_det_sf_Call_Avg_Stock.BorderWidth = 1;
                            tc_det_sf_Call_Avg_Stock.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_Call_Avg_Stock.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_Call_Avg_Stock.Controls.Add(lit_det_sf_Call_Avg_Stock);
                            tr_det.Cells.Add(tc_det_sf_Call_Avg_Stock);
                        }

                        // Call Avg Stock

                        // Unlist Doc tot

                        dsDoc = dcs.New_DCR_TotalUnlstDocQuery(sfCode, divcode, cmonth, cyear);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                            UnlistVisit = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        UnListDoc = UnListDoc + Convert.ToInt16(UnlistVisit);
                        

                        // Unlist Doc tot

                        // UnLstDRs Calls Seen

                        //dsDoc = dcs.DCR_Unlst_Doc_Calls_Seen(sfCode, divcode, cmonth, cyear);

                        //if (dsDoc.Tables[0].Rows.Count > 0)
                        //    tot_doc_Unlstcalls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        //UnLstdoc_calls_seen_total = UnLstdoc_calls_seen_total + Convert.ToInt16(tot_doc_Unlstcalls_seen);
                       
                        //itotUnLstDoc += itotUnLstDoc + Convert.ToInt16(UnLstdoc_calls_seen_total);

                        TableCell tc_det_sf_UnList_tot_Stock = new TableCell();
                        Literal lit_det_sf_UnList_tot_Stock = new Literal();
                        //lit_det_sf_UnList_tot_Stock.Text = "&nbsp;" + UnLstdoc_calls_seen_total.ToString();
                        lit_det_sf_UnList_tot_Stock.Text = "&nbsp;" + UnListDoc.ToString();
                        tc_det_sf_UnList_tot_Stock.BorderStyle = BorderStyle.Solid;
                        tc_det_sf_UnList_tot_Stock.BorderWidth = 1;
                        tc_det_sf_UnList_tot_Stock.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_sf_UnList_tot_Stock.VerticalAlign = VerticalAlign.Middle;
                        tc_det_sf_UnList_tot_Stock.Controls.Add(lit_det_sf_UnList_tot_Stock);
                        tr_det.Cells.Add(tc_det_sf_UnList_tot_Stock);

                        // UnLstDRs Calls Seen

                        //Call Average
                        decimal RoundUnLstavg = new decimal();
                        if (fldwrk_total > 0)
                        {
                          //  dblaverage = Convert.ToDouble((Convert.ToDecimal(UnLstdoc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));

                            dblaverage = Convert.ToDouble((Convert.ToDecimal(UnListDoc) / Convert.ToDecimal(fldwrk_total)));

                            RoundUnLstavg += Math.Round((decimal)dblaverage, 2);                            
                            itotUnLstDocavg += Convert.ToDouble(RoundUnLstavg);
                        }
                        else
                        {
                            dblaverage = 0;
                        }

                        if (dblaverage != 0.0)
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + RoundUnLstavg.ToString();
                            tc_det_average.BorderStyle = BorderStyle.Solid;
                            tc_det_average.BorderWidth = 1;
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }
                        else
                        {
                            TableCell tc_det_average = new TableCell();
                            Literal lit_det_average = new Literal();
                            lit_det_average.Text = "&nbsp;" + "0.0";
                            tc_det_average.BorderStyle = BorderStyle.Solid;
                            tc_det_average.BorderWidth = 1;
                            tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_average.VerticalAlign = VerticalAlign.Middle;
                            tc_det_average.Controls.Add(lit_det_average);
                            tr_det.Cells.Add(tc_det_average);
                        }

                        // Call Average 

                        
                        // Remarks

                        TableCell tc_det_doc_Remarks = new TableCell();
                        HyperLink lit_det_doc_Remarks = new HyperLink();
                        lit_det_doc_Remarks.Text = "&nbsp;" + "Click here";
                        sURL = "rptRemarks.aspx?sf_Name=" + "&sf_code=" + sfCode + "&Year=" + cyear + "&Month=" + cmonth + "";
                        // sURL = "rptRemarks.aspx?&sf_code=" + sf_code+"";
                        //if (fldwrk_total > 0)
                        //{
                            lit_det_doc_Remarks.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,status=no,width=800,height=600,left=0,top=0');";

                            lit_det_doc_Remarks.NavigateUrl = "#";
                        //}
                        tc_det_doc_Remarks.BorderStyle = BorderStyle.Solid;
                        tc_det_doc_Remarks.BorderWidth = 1;
                        tc_det_doc_Remarks.Width = 500;
                        tc_det_doc_Remarks.Controls.Add(lit_det_doc_Remarks);
                        tr_det.Cells.Add(tc_det_doc_Remarks);

                        // Remarks

                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }

                }

               tbl.Rows.Add(tr_det);

            

            }

            TableRow tr_catg_total = new TableRow();

            TableCell tc_catg_Total = new TableCell();
            tc_catg_Total.BorderStyle = BorderStyle.Solid;
            tc_catg_Total.BorderWidth = 1;
            //tc_catg_Total.Width = 25;
            Literal lit_catg_Total = new Literal();
            lit_catg_Total.Text = "<center>Total</center>";
            tc_catg_Total.Controls.Add(lit_catg_Total);
            tc_catg_Total.Font.Bold.ToString();
            tc_catg_Total.BackColor = System.Drawing.Color.White;        
            tc_catg_Total.ColumnSpan = 4;
            tc_catg_Total.Style.Add("text-align", "left");
            tc_catg_Total.Style.Add("font-family", "Calibri");
            tc_catg_Total.Style.Add("font-size", "10pt");
            tr_catg_total.Cells.Add(tc_catg_Total);          

            //TableCell tc_Call_Total_FWD = new TableCell();
            //tc_Call_Total_FWD.BorderStyle = BorderStyle.Solid;
            //tc_Call_Total_FWD.BorderWidth = 1;
            ////tc_Call_Total_FWD.Width = 25;
            //Literal lit_Call_Total_FWD = new Literal();
            //lit_Call_Total_FWD.Text = "";
            //tc_Call_Total_FWD.HorizontalAlign = HorizontalAlign.Center;
            //tc_Call_Total_FWD.Controls.Add(lit_Call_Total_FWD);
            //tc_Call_Total_FWD.BackColor = System.Drawing.Color.White;
            //tc_Call_Total_FWD.ColumnSpan = 2;  
            //tc_Call_Total_FWD.Style.Add("text-align", "left");
            //tc_Call_Total_FWD.Style.Add("font-family", "Calibri");
            //tc_Call_Total_FWD.Style.Add("font-size", "10pt");
            //tr_catg_total.Cells.Add(tc_Call_Total_FWD);

            int[] arrLeave = new int[] { iTotLeave };

            for (int i = 0; i < arrLeave.Length; i++)
            {
                iSumLeave += arrLeave[i];
            }


            TableCell tc_Call_Total_Leave = new TableCell();
            tc_Call_Total_Leave.BorderStyle = BorderStyle.Solid;
            tc_Call_Total_Leave.BorderWidth = 1;
            //tc_Call_Total_Leave.Width = 25;
            Literal lit_Call_Total_Leave = new Literal();
            lit_Call_Total_Leave.Text = iSumLeave.ToString();
            tc_Call_Total_Leave.HorizontalAlign = HorizontalAlign.Center;
            tc_Call_Total_Leave.Controls.Add(lit_Call_Total_Leave);
            tc_Call_Total_Leave.BackColor = System.Drawing.Color.White;          
           
            tr_catg_total.Cells.Add(tc_Call_Total_Leave);

            int[] arrTotDoc = new int[] { doctor_total  };

            for (int i = 0; i < arrTotDoc.Length; i++)
            {
                isum += arrTotDoc[i];
            }           

            TableCell tc_Call_Total_LDrs = new TableCell();
            tc_Call_Total_LDrs.BorderStyle = BorderStyle.Solid;
            tc_Call_Total_LDrs.BorderWidth = 1;            
            Literal lit_Call_Total_LDrs = new Literal();
            lit_Call_Total_LDrs.Text = Convert.ToString(isum);
            tc_Call_Total_LDrs.HorizontalAlign = HorizontalAlign.Center;
            tc_Call_Total_LDrs.VerticalAlign = VerticalAlign.Middle;
            tc_Call_Total_LDrs.Controls.Add(lit_Call_Total_LDrs);
            tc_Call_Total_LDrs.BackColor = System.Drawing.Color.White;         
            tr_catg_total.Cells.Add(tc_Call_Total_LDrs);

            double[] arrDocavg = new double[] { itotDocavg };

            for (int i = 0; i < arrDocavg.Length; i++)
            {
                iSumDocavg += arrDocavg[i];
            }

            iSumDocavg= iSumDocavg / iCount;

            decimal decDocAvg = Math.Round((decimal)iSumDocavg, 2);

            TableCell tc_Call_Total_LDrs_CallAvg = new TableCell();
            tc_Call_Total_LDrs_CallAvg.BorderStyle = BorderStyle.Solid;
            tc_Call_Total_LDrs_CallAvg.BorderWidth = 1;
            //tc_Call_Total_LDrs_CallAvg.Width = 25;
            Literal lit_Call_Total_LDrs_CallAvg = new Literal();
            lit_Call_Total_LDrs_CallAvg.Text = Convert.ToString(decDocAvg);
            tc_Call_Total_LDrs_CallAvg.HorizontalAlign = HorizontalAlign.Center;
            tc_Call_Total_LDrs_CallAvg.VerticalAlign = VerticalAlign.Middle;
            tc_Call_Total_LDrs_CallAvg.Controls.Add(lit_Call_Total_LDrs_CallAvg);
            tc_Call_Total_LDrs_CallAvg.BackColor = System.Drawing.Color.White;
            //tc_Call_Total_LDrs_CallAvg.Style.Add("text-align", "left");
            //tc_Call_Total_LDrs_CallAvg.Style.Add("font-family", "Calibri");
            //tc_Call_Total_LDrs_CallAvg.Style.Add("font-size", "10pt");
            tr_catg_total.Cells.Add(tc_Call_Total_LDrs_CallAvg);

            int[] arrChem = new int[] { Chemist_total };

            for (int i = 0; i < arrChem.Length; i++)
            {
                iSumChem += arrChem[i];
            }

            TableCell tc_Call_Total_Chemist = new TableCell();
            tc_Call_Total_Chemist.BorderStyle = BorderStyle.Solid;
            tc_Call_Total_Chemist.BorderWidth = 1;
            //tc_Call_Total_Chemist.Width = 25;
            Literal lit_Call_Total_Chemist = new Literal();
            lit_Call_Total_Chemist.Text = Convert.ToString(iSumChem);
            tc_Call_Total_Chemist.HorizontalAlign = HorizontalAlign.Center;
            tc_Call_Total_Chemist.Controls.Add(lit_Call_Total_Chemist);
            tc_Call_Total_Chemist.BackColor = System.Drawing.Color.White;           
            tr_catg_total.Cells.Add(tc_Call_Total_Chemist);

            double[] arrChemavg = new double[] { itotChemavg };

            for (int i = 0; i < arrChemavg.Length; i++)
            {
                iSumChemavg += arrChemavg[i];
            }
            iSumChemavg = iSumChemavg / iCount;
            decimal decimalValue = Math.Round((decimal)iSumChemavg, 2);

            TableCell tc_Call_Total_Chemist_CallAvg = new TableCell();
            tc_Call_Total_Chemist_CallAvg.BorderStyle = BorderStyle.Solid;
            tc_Call_Total_Chemist_CallAvg.BorderWidth = 1;
            //tc_Call_Total_Chemist_CallAvg.Width = 25;
            Literal lit_Call_Total_Chemist_CallAvg = new Literal();
            lit_Call_Total_Chemist_CallAvg.Text = decimalValue.ToString();
            tc_Call_Total_Chemist_CallAvg.HorizontalAlign = HorizontalAlign.Center;
            tc_Call_Total_Chemist_CallAvg.Controls.Add(lit_Call_Total_Chemist_CallAvg);
            tc_Call_Total_Chemist_CallAvg.BackColor = System.Drawing.Color.White;           
            tr_catg_total.Cells.Add(tc_Call_Total_Chemist_CallAvg);

            int[] arrStockist = new int[] { Stock_toatal  };

            for (int i = 0; i < arrStockist.Length; i++)
            {
                iSumStock += arrStockist[i];
            }


            TableCell tc_Call_Total_Stockist = new TableCell();
            tc_Call_Total_Stockist.BorderStyle = BorderStyle.Solid;
            tc_Call_Total_Stockist.BorderWidth = 1;
            //tc_Call_Total_Stockist.Width = 25;
            Literal lit_Call_Total_Stockist = new Literal();
            lit_Call_Total_Stockist.Text = iSumStock.ToString();
            tc_Call_Total_Stockist.HorizontalAlign = HorizontalAlign.Center;
            tc_Call_Total_Stockist.Controls.Add(lit_Call_Total_Stockist);
            tc_Call_Total_Stockist.BackColor = System.Drawing.Color.White;
           
            tr_catg_total.Cells.Add(tc_Call_Total_Stockist);

            double[] arrStockavg = new double[] { itotStockistavg };

            for (int i = 0; i < arrStockavg.Length; i++)
            {
                iSumStockavg += arrStockavg[i];
            }
            iSumStockavg = iSumStockavg / iCount;

            decimal DecSumStockavg = Math.Round((decimal)iSumStockavg, 2);

            TableCell tc_Call_Total_Stockist_CallAvg = new TableCell();
            tc_Call_Total_Stockist_CallAvg.BorderStyle = BorderStyle.Solid;
            tc_Call_Total_Stockist_CallAvg.BorderWidth = 1;
            //tc_Call_Total_Stockist_CallAvg.Width = 25;
            Literal lit_Call_Total_Stockist_CallAvg = new Literal();
            lit_Call_Total_Stockist_CallAvg.Text = DecSumStockavg.ToString();
            tc_Call_Total_Stockist_CallAvg.HorizontalAlign = HorizontalAlign.Center;
            tc_Call_Total_Stockist_CallAvg.Controls.Add(lit_Call_Total_Stockist_CallAvg);
            tc_Call_Total_Stockist_CallAvg.BackColor = System.Drawing.Color.White;            
            tr_catg_total.Cells.Add(tc_Call_Total_Stockist_CallAvg);

            int[] arrUnDoc = new int[] { UnListDoc };

            for (int i = 0; i < arrUnDoc.Length; i++)
            {
                iSumUnLst += arrUnDoc[i];
            }

            TableCell tc_Call_Total_UnDrs = new TableCell();
            tc_Call_Total_UnDrs.BorderStyle = BorderStyle.Solid;
            tc_Call_Total_UnDrs.BorderWidth = 1;
            //tc_Call_Total_UnDrs.Width = 25;
            Literal lit_Call_Total_UnDrs = new Literal();
            lit_Call_Total_UnDrs.Text = iSumUnLst.ToString();
            tc_Call_Total_UnDrs.Controls.Add(lit_Call_Total_UnDrs);
            tc_Call_Total_UnDrs.HorizontalAlign = HorizontalAlign.Center;
            tc_Call_Total_UnDrs.BackColor = System.Drawing.Color.White;            
            tr_catg_total.Cells.Add(tc_Call_Total_UnDrs);

            double[] arrUnLstavg = new double[] { itotUnLstDocavg };

            for (int i = 0; i < arrUnLstavg.Length; i++)
            {
                iSumUnLstavg += arrUnLstavg[i];
            }
            iSumUnLstavg = iSumUnLstavg / iCount;
            decimal decSumUnLstavg = Math.Round((decimal)iSumUnLstavg, 2);

            TableCell tc_Call_Total_UnDrs_CallAvg = new TableCell();
            tc_Call_Total_UnDrs_CallAvg.BorderStyle = BorderStyle.Solid;
            tc_Call_Total_UnDrs_CallAvg.BorderWidth = 1;
            //tc_Call_Total_UnDrs_CallAvg.Width = 25;
            Literal lit_Call_Total_UnDrs_CallAvg = new Literal();
            lit_Call_Total_UnDrs_CallAvg.Text = decSumUnLstavg.ToString();
            tc_Call_Total_UnDrs_CallAvg.Controls.Add(lit_Call_Total_UnDrs_CallAvg);
            tc_Call_Total_UnDrs_CallAvg.HorizontalAlign = HorizontalAlign.Center;
            tc_Call_Total_UnDrs_CallAvg.BackColor = System.Drawing.Color.White;
            
            tr_catg_total.Cells.Add(tc_Call_Total_UnDrs_CallAvg);

            TableCell tc_Call_Total_Remarks = new TableCell();
            tc_Call_Total_Remarks.BorderStyle = BorderStyle.Solid;
            tc_Call_Total_Remarks.BorderWidth = 1;
            //tc_Call_Total_UnDrs_CallAvg.Width = 25;
            Literal lit_Call_Total_UnDrs_Remarks = new Literal();
            lit_Call_Total_UnDrs_Remarks.Text = "";
            tc_Call_Total_Remarks.Controls.Add(lit_Call_Total_UnDrs_Remarks);
            tc_Call_Total_Remarks.HorizontalAlign = HorizontalAlign.Center;
            tc_Call_Total_Remarks.BackColor = System.Drawing.Color.White;

            tr_catg_total.Cells.Add(tc_Call_Total_Remarks);

            tbl.Rows.Add(tr_catg_total);

    }

    private void FillPeriodically_FieldForce()
    {
        tbl.Rows.Clear();
        doctor_total = 0;
        BindSf_Code();
        
        dsSalesForce = sf.UserList_get_SelfMail(divcode, sfCode);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 1;            
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "S.No";            
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_SNo.Style.Add("color", "white");
            tc_SNo.Style.Add("font-weight", "bold");
            tc_SNo.Style.Add("border-color", "Black");
            tc_SNo.Width = 50;
            tr_header.Cells.Add(tc_SNo);
            //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");
            
            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;            
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Field Force</center>";
            tc_DR_Name.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_DR_Name.Style.Add("border-color", "Black");
            tc_DR_Name.Style.Add("color", "white");
            tc_DR_Name.Style.Add("font-weight", "bold");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);           

            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);
            int iCount = 0;

            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            for (int j = 1; j <= months + 1; j++)
            {
                TableRow tr_det = new TableRow();
                if (months > 0)
                {
                    //Sub Header
                    ListedDR lstDR = new ListedDR();

                    TableCell tc_month = new TableCell();
                    Literal lit_month = new Literal();
                    lit_month.Text = "&nbsp;" + sf.getMonthName(cmonth.ToString()).Substring(0,3);
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.Width=50;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_month.VerticalAlign = VerticalAlign.Middle;
                    tc_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                    tc_month.Style.Add("color", "white");
                    tc_month.Style.Add("font-weight", "bold");
                    tc_month.Style.Add("border-color", "Black");
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }

                }
            }

            Doctor dr = new Doctor();
            dsDoctor = dr.getDocCat(divcode);
            tbl.Rows.Add(tr_header);
            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());
            // Details Section
            string sURL = string.Empty;
            
            int iCnt = 0;
            int imonth = 0;
            int iyear = 0;
            DCR dcs = new DCR();

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                ListedDR lstDR = new ListedDR();
                iCnt = lstDR.RecordCount(drFF["sf_code"].ToString());

                TableRow tr_det = new TableRow();
                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Width = 50;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);                

                TableCell tc_det_doc_name = new TableCell();
                HyperLink lit_det_doc_name = new HyperLink();
                lit_det_doc_name.Text = "&nbsp;" + drFF["SF_Name"].ToString();
                tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                tc_det_doc_name.HorizontalAlign = HorizontalAlign.Left;
                tc_det_doc_name.BorderWidth = 1;
                tc_det_doc_name.Width = 200;
                tc_det_doc_name.Controls.Add(lit_det_doc_name);
                tr_det.Cells.Add(tc_det_doc_name);

                months = Convert.ToInt16(ViewState["months"].ToString());

                if (months > 0)
                {
                   

                    int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                    int cmonth1 = Convert.ToInt32(FMonth);
                    int cyear1 = Convert.ToInt32(FYear);
                    //int iCount = 0;

                    ViewState["months"] = months;
                    ViewState["cmonth"] = cmonth;
                    ViewState["cyear"] = cyear;
                    // Total Doctors
                    for (int j = 1; j <= months1 + 1; j++)
                    {
                        doctor_total = 0;
                        //TableRow tr_det = new TableRow();
                        if (months > 0)
                        {

                            tot_fldwrk = "";
                            tot_dr = "";
                            tot_doc_met = "";
                            tot_doc_calls_seen = "";

                            fldwrk_total = 0;
                            doctor_total = 0;
                            Chemist_total = 0;
                            Stock_toatal = 0;
                            UnListDoc = 0;
                            Dcr_Sub_days = 0;
                            doc_met_total = 0;
                            doc_calls_seen_total = 0;
                            dblCoverage = 0.00;
                            dblaverage = 0.00;

                            // Field Work
                            //dsDoc = dcs.DCR_TotalFLDWRKQuery(drFF["sf_code"].ToString(), divcode, cmonth1, cyear1);

                            if (drFF["sf_code"].ToString().Contains("MR"))
                            {
                                dsDoc = dcs.DCR_TotalFLDWRKQuery(drFF["sf_code"].ToString(), divcode, cmonth1, cyear1);
                            }
                            else
                            {
                                dsDoc = dcs.DCR_TotalFLDWRKQuery_MGR(drFF["sf_code"].ToString(), divcode, cmonth1, cyear1);
                            }

                            if (dsDoc.Tables[0].Rows.Count > 0)
                                tot_fldwrk = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                            fldwrk_total = Convert.ToInt16(tot_fldwrk);

                            TableCell tc_det_sf_FLDWRK = new TableCell();
                            Literal lit_det_sf_FLDWRK = new Literal();
                            lit_det_sf_FLDWRK.Text = "&nbsp;" + fldwrk_total.ToString();
                            tc_det_sf_FLDWRK.BorderStyle = BorderStyle.Solid;
                            tc_det_sf_FLDWRK.Visible = false;
                            tc_det_sf_FLDWRK.BorderWidth = 1;
                            tc_det_sf_FLDWRK.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_FLDWRK.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_FLDWRK.Controls.Add(lit_det_sf_FLDWRK);
                            tr_det.Cells.Add(tc_det_sf_FLDWRK);

                            

                            // Field Work 

                            sCurrentDate = months + "-01-" + cmonth;
                            dtCurrent = Convert.ToDateTime(sCurrentDate);
                            dsDoc = dcs.New_DCR_Visit_TotalDocQuery(drFF["sf_code"].ToString(), divcode, cmonth1, cyear1);

                            if (dsDoc.Tables[0].Rows.Count > 0)
                                tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            doctor_total = doctor_total + Convert.ToInt16(tot_dr);                          

                            // Total Doctors    

                            // Drs Calls

                            //dsDoc = dcs.DCR_Doc_Calls_Seen(drFF["sf_code"].ToString(), divcode, cmonth1, cyear1);

                            //if (dsDoc.Tables[0].Rows.Count > 0)
                            //    tot_doc_calls_seen = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                            //doc_calls_seen_total = doc_calls_seen_total + Convert.ToInt16(tot_doc_calls_seen);

                            TableCell tc_det_sf_tot_doc = new TableCell();
                            Literal lit_det_sf_tot_doc = new Literal();
                            //lit_det_sf_tot_doc.Text = "&nbsp;" + doc_calls_seen_total.ToString();
                            lit_det_sf_tot_doc.Text = "&nbsp;" + doctor_total.ToString();
                            tc_det_sf_tot_doc.BorderStyle = BorderStyle.Solid;
                            tc_det_sf_tot_doc.Visible = false;
                            tc_det_sf_tot_doc.Width = 5;
                            tc_det_sf_tot_doc.BorderWidth = 1;
                            tc_det_sf_tot_doc.HorizontalAlign = HorizontalAlign.Center;
                            tc_det_sf_tot_doc.VerticalAlign = VerticalAlign.Middle;
                            tc_det_sf_tot_doc.Controls.Add(lit_det_sf_tot_doc);
                            tr_det.Cells.Add(tc_det_sf_tot_doc);

                            // Drs Calls

                            //Call Average
                            decimal decDocAvg = new decimal();

                            if (fldwrk_total > 0)
                            {
                                //dblaverage = Convert.ToDouble((Convert.ToDecimal(doc_calls_seen_total) / Convert.ToDecimal(fldwrk_total)));
                                dblaverage = Convert.ToDouble((Convert.ToDecimal(doctor_total) / Convert.ToDecimal(fldwrk_total)));
                                decDocAvg = Math.Round((decimal)dblaverage, 2);
                            }
                            else
                            {
                                dblaverage = 0;
                            }

                            if (dblaverage != 0.0)
                            {
                                TableCell tc_det_average = new TableCell();
                                Literal lit_det_average = new Literal();
                                lit_det_average.Text = "&nbsp;" + decDocAvg;
                                tc_det_average.BorderStyle = BorderStyle.Solid;
                                tc_det_average.Width = 50;
                                tc_det_average.BorderWidth = 1;
                                tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                                tc_det_average.VerticalAlign = VerticalAlign.Middle;
                                tc_det_average.Controls.Add(lit_det_average);
                                tr_det.Cells.Add(tc_det_average);
                            }
                            else
                            {
                                TableCell tc_det_average = new TableCell();
                                Literal lit_det_average = new Literal();
                                lit_det_average.Text = "&nbsp;" + "0.0";
                                tc_det_average.BorderStyle = BorderStyle.Solid;
                                tc_det_average.Width = 50;
                                tc_det_average.BorderWidth = 1;
                                tc_det_average.HorizontalAlign = HorizontalAlign.Center;
                                tc_det_average.VerticalAlign = VerticalAlign.Middle;
                                tc_det_average.Controls.Add(lit_det_average);
                                tr_det.Cells.Add(tc_det_average);
                            }
                        }

                        cmonth1 = cmonth1 + 1;
                        if (cmonth1 == 13)
                        {
                            cmonth1 = 1;
                            cyear1 = cyear1 + 1;
                        }
                    }

                    // Call Average   

                }

                tbl.Rows.Add(tr_det);

            }
        }
    }

    private void BindGrid()
    {
        SalesForce sf = new SalesForce();
        DCR dcs = new DCR();

        DataSet dsSalesForce=new DataSet();
        DataSet dsDoc = new DataSet();

        dsDoc = dcs.DCR_Total_Call_Doc_Visit_Report(sfCode, divcode, Convert.ToDateTime("10-01-2015"));

        dsSalesForce = sf.SF_ReportingTo_TourPlan(divcode, sfCode);
        dsSalesForce.Merge(dsDoc);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            //rptAverage.DataSource = dsSalesForce;
            //rptAverage.DataBind();
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
        string Export = this.Page.Title;
        string attachment = "attachment; filename="+Export+".xls";
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

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        //string strFileName = Title;
        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //HtmlForm frm = new HtmlForm();
        //pnlContents.Parent.Controls.Add(frm);
        //frm.Attributes["runat"] = "server";
        //frm.Controls.Add(pnlContents);
        //frm.RenderControl(hw);
        //StringReader sr = new StringReader(sw.ToString());
        //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);
        //iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //pdfDoc.Open();
        
        //htmlparser.Parse(sr);
        //pdfDoc.Close();
        //Response.Write(pdfDoc);
        //Response.End();


        Response.ContentType = "application/pdf";

        Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);

        pnlContents.RenderControl(hw);

        StringReader sr = new StringReader(sw.ToString());

        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);

        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

        pdfDoc.Open();

        htmlparser.Parse(sr);

        pdfDoc.Close();

        Response.Write(pdfDoc);

        Response.End();      
        
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}