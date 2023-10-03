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
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using Bus_EReport;
using System.Net;

public partial class MasterFiles_rdlFile_Default : System.Web.UI.Page
{
    //private string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();

    string sf_code = string.Empty;
    int FMonth = -1;
    string div_code = string.Empty;
    int FYear = -1;
    DataSet dsSalesForce = null;
    DataSet dsDCR = null;
    DataSet dsTerritory = null;
    DataSet dsdoc = null;
    DataSet dssf = null;
  
    string strDelay = string.Empty;
   
    string Sf_Name = string.Empty;
    string sCurrentDate = string.Empty;
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

    private string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {

        FMonth = Convert.ToInt16(Request.QueryString["FMonth"].ToString());
        FYear = Convert.ToInt16(Request.QueryString["FYear"].ToString());
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        Sf_Name = Request.QueryString["SF_Name"].ToString();

       

        if (!Page.IsPostBack)
        {
            ReportViewer1.Visible = true;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;           
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/MIS Reports/rdlFile/Report.rdlc");
            Zafodo_NetDataSet dszofodo = new Zafodo_NetDataSet();           
            SqlConnection con = new SqlConnection(strConn);
            con.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("sp_Get_DCR_Analysis", con);
           
            command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.Add("@userid", txtUser.Text);
            command.Parameters.Add("@div_code", div_code);
            command.Parameters.Add("@sf_code", sf_code);
            command.Parameters.Add("@cmonth", FMonth);
            command.Parameters.Add("@cyear", FYear);
            command.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);

            if (FMonth == 12)
            {
                sCurrentDate = "01-01-" + (FYear + 1);
            }
            else
            {
                sCurrentDate = (FMonth + 1) + "-01-" + FYear;
            }

            command = new SqlCommand("DCR_TotalSubDaysQuery", con);
            command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.Add("@userid", txtUser.Text);
            command.Parameters.Add("@div_code", div_code);
            command.Parameters.Add("@sf_code", sf_code);
            command.Parameters.Add("@cmonth", FMonth);
            command.Parameters.Add("@cyear", FYear);
            command.Parameters.Add("@cdate", sCurrentDate);
            command.ExecuteNonQuery();
            DataSet ds1 = new DataSet();
            adapter = new SqlDataAdapter(command);
            adapter.Fill(ds1);

            command = new SqlCommand("DCR_Worked_With_Name", con);
            command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.Add("@userid", txtUser.Text);
            command.Parameters.Add("@div_code", div_code);
            command.Parameters.Add("@sf_code", sf_code);
            command.Parameters.Add("@cmonth", FMonth);
            command.Parameters.Add("@cyear", FYear);           
            command.ExecuteNonQuery();
            DataSet dsDCR = new DataSet();
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dsDCR);

            command = new SqlCommand("DCR_Delayed_Status_Date", con);
            command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.Add("@userid", txtUser.Text);
            command.Parameters.Add("@div_code", div_code);
            command.Parameters.Add("@sf_code", sf_code);
            command.Parameters.Add("@cmonth", FMonth);
            command.Parameters.Add("@cyear", FYear);
            command.ExecuteNonQuery();
            DataSet dsDelayDate = new DataSet();
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dsDelayDate);

            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
            ReportDataSource datasource1 = new ReportDataSource("DataSet2", ds1.Tables[0]);
            ReportDataSource datasourceDCR = new ReportDataSource("DataSet3", dsDCR.Tables[0]);
            ReportDataSource datasourceDCRDelay = new ReportDataSource("DataSet4", dsDelayDate.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.DataSources.Add(datasource1);
            ReportViewer1.LocalReport.DataSources.Add(datasourceDCR);
            ReportViewer1.LocalReport.DataSources.Add(datasourceDCRDelay);
          
            ReportViewer1.LocalReport.Refresh();
            con.Close();
           
        }

      
       // CreateDynamicDCRDetailedView(FMonth,FYear,sf_code);
    }


    private void CreateDynamicDCRDetailedView(int imonth, int iyear, string sf_code)
    {
        try
        {

            DCR dc = new DCR();
            // dsDCR = dc.get_dcr_date(sf_code, imonth, iyear);
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
                //tbldetail_main3.BorderStyle = BorderStyle.None;
                tbldetail_main3.Width = 1200;

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
                tbldetail_main.Width = 1200;
                tbldetail_main.Style.Add("margin-left", "100px");
                TableRow tr_det_head_main = new TableRow();
                TableCell tc_det_head_main2 = new TableCell();
                tc_det_head_main2.Width = 1200;

                Table tbldetail = new Table();
                tbldetail.BorderStyle = BorderStyle.Solid;
                tbldetail.BorderWidth = 1;
                tbldetail.GridLines = GridLines.Both;
                tbldetail.Width = 1200;
                tbldetail.Style.Add("border-collapse", "collapse");
                tbldetail.Style.Add("border", "solid 0px Black");

                if (sf_code.Contains("MR"))
                {
                    dsdoc = dc.get_DCRView_Approved_All_Dates(sf_code, FMonth.ToString(),FYear.ToString()); //1-Listed Doctor
                }
                else
                {
                    dsdoc = dc.get_DCRView_Approved_MGR_All_Dates(sf_code, FMonth.ToString(), FYear.ToString()); //1-Listed Doctor
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
                    tc_det_head_SNo.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
                    tr_det_head.Cells.Add(tc_det_head_SNo);

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
                    tc_det_head_ww.Width = 150;
                    tc_det_head_ww.Controls.Add(lit_det_head_ww);
                    tr_det_head.Cells.Add(tc_det_head_ww);

                    TableCell tc_det_head_JC = new TableCell();
                    tc_det_head_JC.BorderStyle = BorderStyle.Solid;
                    tc_det_head_JC.BorderWidth = 1;
                    tc_det_head_JC.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_JC = new Literal();
                    lit_det_head_JC.Text = "<b>Joint Calls</b>";
                    tc_det_head_JC.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_JC.Controls.Add(lit_det_head_JC);
                    tr_det_head.Cells.Add(tc_det_head_JC);

                    TableCell tc_det_head_Territ_Code = new TableCell();
                    tc_det_head_Territ_Code.BorderStyle = BorderStyle.Solid;
                    tc_det_head_Territ_Code.BorderWidth = 1;
                    tc_det_head_Territ_Code.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_Territ_Code = new Literal();
                    lit_det_head_Territ_Code.Text = "<b>" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() +" in TP </b>";
                    tc_det_head_Territ_Code.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_Territ_Code.Controls.Add(lit_det_head_Territ_Code);
                    tr_det_head.Cells.Add(tc_det_head_Territ_Code);

                    TableCell tc_det_head_Territ_Worked = new TableCell();
                    tc_det_head_Territ_Worked.BorderStyle = BorderStyle.Solid;
                    tc_det_head_Territ_Worked.BorderWidth = 1;
                    tc_det_head_Territ_Worked.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_Territ_Worked = new Literal();
                    lit_det_head_Territ_Worked.Text = "<b>"+ dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() +" Worked </b>";
                    tc_det_head_Territ_Worked.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_Territ_Worked.Controls.Add(lit_det_head_Territ_Worked);
                    tr_det_head.Cells.Add(tc_det_head_Territ_Worked);

                    TableCell tc_det_head_Territ_Dev = new TableCell();
                    tc_det_head_Territ_Dev.BorderStyle = BorderStyle.Solid;
                    tc_det_head_Territ_Dev.BorderWidth = 1;
                    tc_det_head_Territ_Dev.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_head_Territ_Dev = new Literal();
                    lit_det_head_Territ_Dev.Text = "<b> Dev </b>";
                    tc_det_head_Territ_Dev.Attributes.Add("Class", "tr_det_head");
                    tc_det_head_Territ_Dev.Controls.Add(lit_det_head_Territ_Dev);
                    tr_det_head.Cells.Add(tc_det_head_Territ_Dev);


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
                    //tc_det_head_POB.Visible = false;
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
                    int iTotChemPOB = 0;
                    int iTotStockCal = 0;
                    int iTotUnLstCal = 0;
                    int isum = 0;
                    int isumChem = 0;
                    int isumStock = 0;
                    int isumChemPOB = 0;
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

                        TableCell tc_det_time = new TableCell();
                        Literal lit_det_time = new Literal();
                        lit_det_time.Text = "&nbsp;&nbsp;" + drdoctor["Submission_Date"].ToString();
                        tc_det_time.BorderStyle = BorderStyle.Solid;
                        tc_det_time.Attributes.Add("Class", "tbldetail_main");
                        // tc_det_time.BorderWidth = 1;
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
                        if (dsDelay.Tables[0].Rows.Count == 0)
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
                                    strDelay = "<span style='color:red;font-family:Verdana'>( " + dsDelay.Tables[0].Rows[0][0].ToString() + " ) </span>";
                                }
                                if (drdoctor["Temp"].ToString() == "1")
                                {
                                    strDelay = "<span style='color:red;font-family:Verdana'> ( " + "Approval Pending" + " ) </span>";
                                }
                                else if (drdoctor["Temp"].ToString() == "2")
                                {
                                    strDelay = "<span style='color:red;font-family:Verdana'> ( " + "Disapproved" + " ) </span>";
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

                                TableCell tc_det_JointCall = new TableCell();
                                Literal lit_det_JointCall = new Literal();
                                lit_det_JointCall.Text = "0"; // drdoctor["lvisit"].ToString();
                                tc_det_JointCall.BorderStyle = BorderStyle.Solid;
                                tc_det_JointCall.Attributes.Add("Class", "tbldetail_main");
                                //tc_det_lvisit.BorderWidth = 1;
                                tc_det_JointCall.Controls.Add(lit_det_JointCall);
                                tr_det_sno.Cells.Add(tc_det_JointCall);

                                TableCell tc_det_Territ_TP = new TableCell();
                                Literal lit_det_Territ_TP = new Literal();
                                lit_det_Territ_TP.Text = "0"; // drdoctor["lvisit"].ToString();
                                tc_det_Territ_TP.BorderStyle = BorderStyle.Solid;
                                tc_det_Territ_TP.Attributes.Add("Class", "tbldetail_main");
                                //tc_det_lvisit.BorderWidth = 1;
                                tc_det_Territ_TP.Controls.Add(lit_det_Territ_TP);
                                tr_det_sno.Cells.Add(tc_det_Territ_TP);

                                TableCell tc_det_Territ_Worked = new TableCell();
                                Literal lit_det_Territ_Worked = new Literal();
                                lit_det_Territ_Worked.Text = "0"; // drdoctor["lvisit"].ToString();
                                tc_det_Territ_Worked.BorderStyle = BorderStyle.Solid;
                                tc_det_Territ_Worked.Attributes.Add("Class", "tbldetail_main");
                                //tc_det_lvisit.BorderWidth = 1;
                                tc_det_Territ_Worked.Controls.Add(lit_det_Territ_Worked);
                                tr_det_sno.Cells.Add(tc_det_Territ_Worked);

                                TableCell tc_det_Territ_Dev = new TableCell();
                                Literal lit_det_Territ_Dev = new Literal();
                                lit_det_Territ_Dev.Text = "0"; // drdoctor["lvisit"].ToString();
                                tc_det_Territ_Dev.BorderStyle = BorderStyle.Solid;
                                tc_det_Territ_Dev.Attributes.Add("Class", "tbldetail_main");
                                //tc_det_lvisit.BorderWidth = 1;
                                tc_det_Territ_Dev.Controls.Add(lit_det_Territ_Dev);
                                tr_det_sno.Cells.Add(tc_det_Territ_Dev);

                                TableCell tc_det_spec = new TableCell();
                                HyperLink Hyllit_det_spec = new HyperLink();
                                Hyllit_det_spec.Text = drdoctor["doc_cnt"].ToString();
                                if (Hyllit_det_spec.Text != "0")
                                {
                                    sURL = "rptDCRViewCompleted.aspx?sf_Name=" + Sf_Name.ToString() + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 1 + "&Sf_HQ=" + Sf_HQ + " ";

                                    Hyllit_det_spec.Attributes["onclick"] = "window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                    Hyllit_det_spec.NavigateUrl = "#";
                                }
                                tc_det_spec.BorderStyle = BorderStyle.Solid;
                                tc_det_spec.HorizontalAlign = HorizontalAlign.Center;
                                tc_det_spec.Attributes.Add("Class", "tbldetail_main");
                                // tc_det_spec.BorderWidth = 1;
                                tc_det_spec.Controls.Add(Hyllit_det_spec);
                                tr_det_sno.Cells.Add(tc_det_spec);

                                iTotLstCal += Convert.ToInt16(Hyllit_det_spec.Text);

                                TableCell tc_det_prod = new TableCell();
                                HyperLink hyllit_det_prod = new HyperLink();
                                hyllit_det_prod.Text = drdoctor["che_cnt"].ToString().ToString();
                                if (hyllit_det_prod.Text != "0")
                                {
                                    sURL = "rptDCRViewCompleted.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 2 + "";
                                    hyllit_det_prod.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                    hyllit_det_prod.NavigateUrl = "#";

                                }
                                tc_det_prod.BorderStyle = BorderStyle.Solid;
                                tc_det_prod.HorizontalAlign = HorizontalAlign.Center;
                                tc_det_prod.Attributes.Add("Class", "tbldetail_main");
                                //tc_det_prod.BorderWidth = 1;
                                tc_det_prod.Controls.Add(hyllit_det_prod);
                                tr_det_sno.Cells.Add(tc_det_prod);

                                iTotChemCal += Convert.ToInt16(hyllit_det_prod.Text);

                                TableCell tc_det_Che_POB = new TableCell();
                                Literal lit_det_Che_POB = new Literal();
                                if (drdoctor["che_POB"].ToString().ToString() != "")
                                {
                                    lit_det_Che_POB.Text = drdoctor["che_POB"].ToString().ToString();
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



                                iTotChemPOB += Convert.ToInt16(lit_det_Che_POB.Text);

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
                                //tc_det_gift.BorderWidth = 1;
                                tc_det_gift.Controls.Add(hyllit_det_gift);
                                tr_det_sno.Cells.Add(tc_det_gift);

                                iTotStockCal += Convert.ToInt16(hyllit_det_gift.Text);

                                TableCell tc_det_UnDoc = new TableCell();
                                HyperLink hyllit_det_UnDoc = new HyperLink();
                                hyllit_det_UnDoc.Text = drdoctor["Undoc_cnt"].ToString();
                                if (hyllit_det_UnDoc.Text != "0")
                                {
                                    sURL = "rptDCRViewCompleted.aspx?sf_Name=" + "&sf_code=" + sf_code + "&Year=" + cyear + "&Month=" + cmonth + "&Day=" + lit_det_Ses.Text + "&Type=" + 4 + "";

                                    hyllit_det_UnDoc.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                                    hyllit_det_UnDoc.NavigateUrl = "#";
                                }

                                tc_det_UnDoc.BorderStyle = BorderStyle.Solid;
                                tc_det_UnDoc.HorizontalAlign = HorizontalAlign.Center;
                                tc_det_UnDoc.Attributes.Add("Class", "tbldetail_main");
                                //tc_det_UnDoc.BorderWidth = 1;
                                tc_det_UnDoc.Controls.Add(hyllit_det_UnDoc);
                                tr_det_sno.Cells.Add(tc_det_UnDoc);
                                iTotUnLstCal += Convert.ToInt16(hyllit_det_UnDoc.Text);


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



                                if (sf_code.Contains("MR"))
                                {
                                    lit_det_NonFwk.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_B"].ToString() + strDelay;
                                }
                                else
                                {
                                    lit_det_NonFwk.Text = "&nbsp;&nbsp;" + drdoctor["Worktype_Name_M"].ToString() + strDelay;
                                }
                                tc_det_NonFwk.BorderStyle = BorderStyle.Solid;
                                tc_det_NonFwk.BackColor = System.Drawing.ColorTranslator.FromHtml("#c5e8ec");
                                tc_det_NonFwk.Attributes.Add("Class", "tbldetail_main");
                                tc_det_NonFwk.ColumnSpan = 11;
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
                            tc_det_NonFwk.BackColor = System.Drawing.ColorTranslator.FromHtml("#c5e8ec");
                            tc_det_NonFwk.Attributes.Add("Class", "tbldetail_main");
                            tc_det_NonFwk.ColumnSpan = 11;
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
                    tc_Count_Total.ColumnSpan = 8;
                    tc_Count_Total.Style.Add("text-align", "left");
                    tc_Count_Total.Style.Add("font-family", "Calibri");
                    tc_Count_Total.Style.Add("font-size", "10pt");
                    tc_Count_Total.Style.Add("background-color", "#ffe4b5");

                    tr_total.Cells.Add(tc_Count_Total);

                    int[] arrTotDoc = new int[] { iTotLstCal };

                    for (int i = 0; i < arrTotDoc.Length; i++)
                    {
                        isum += arrTotDoc[i];
                    }

                    

                    TableCell tc_Lst_Count_Total = new TableCell();
                    tc_Lst_Count_Total.BorderStyle = BorderStyle.Solid;
                    tc_Lst_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Lst_Count_Total = new Literal();
                    lit_Lst_Count_Total.Text = Convert.ToString(isum);
                    tc_Lst_Count_Total.Controls.Add(lit_Lst_Count_Total);
                    tc_Lst_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    tc_Lst_Count_Total.VerticalAlign = VerticalAlign.Middle;
                    tc_Lst_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    tc_Lst_Count_Total.Font.Bold.ToString();
                    //tc_Lst_Count_Total.BackColor = System.Drawing.Color.White;
                    tc_Lst_Count_Total.Style.Add("color", "Red");
                    tc_Lst_Count_Total.Style.Add("background-color", "#ffe4b5");
                    //tc_Lst_Count_Total.Style.Add("text-align", "left");
                    //tc_Lst_Count_Total.Style.Add("font-family", "Calibri");
                    //tc_Lst_Count_Total.Style.Add("font-size", "10pt");
                    tr_total.Cells.Add(tc_Lst_Count_Total);


                    int[] arrTotChem = new int[] { iTotChemCal };

                    for (int i = 0; i < arrTotChem.Length; i++)
                    {
                        isumChem += arrTotChem[i];
                    }

                    

                    TableCell tc_Chem_Count_Total = new TableCell();
                    tc_Chem_Count_Total.BorderStyle = BorderStyle.Solid;
                    tc_Chem_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Chem_Count_Total = new Literal();
                    lit_Chem_Count_Total.Text = Convert.ToString(isumChem);
                    tc_Chem_Count_Total.Controls.Add(lit_Chem_Count_Total);
                    tc_Chem_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    tc_Chem_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    tc_Chem_Count_Total.Font.Bold.ToString();
                    //tc_Chem_Count_Total.BackColor = System.Drawing.Color.White;
                    tc_Chem_Count_Total.Style.Add("color", "Red");
                    tc_Chem_Count_Total.Style.Add("background-color", "#ffe4b5");
                    //tc_Chem_Count_Total.Style.Add("text-align", "left");
                    //tc_Chem_Count_Total.Style.Add("font-family", "Calibri");
                    //tc_Chem_Count_Total.Style.Add("font-size", "10pt");
                    tr_total.Cells.Add(tc_Chem_Count_Total);

                    int[] arrtotChemPOB = new int[] { iTotChemPOB };

                    for (int i = 0; i < arrtotChemPOB.Length; i++)
                    {
                        isumChemPOB += arrtotChemPOB[i];
                    }

                    TableCell Chemist_POB_Count_Total = new TableCell();
                    Chemist_POB_Count_Total.BorderStyle = BorderStyle.Solid;
                    Chemist_POB_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Chem_POB_Count_Total = new Literal();
                    lit_Chem_POB_Count_Total.Text = Convert.ToString(isumChemPOB);
                    Chemist_POB_Count_Total.Controls.Add(lit_Chem_POB_Count_Total);
                    Chemist_POB_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    Chemist_POB_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    Chemist_POB_Count_Total.Font.Bold.ToString();
                    Chemist_POB_Count_Total.BackColor = System.Drawing.Color.White;
                    Chemist_POB_Count_Total.Style.Add("color", "Red");
                    Chemist_POB_Count_Total.Style.Add("background-color", "#ffe4b5");
                    //tc_Stock_Count_Total.Style.Add("text-align", "left");
                    //tc_Stock_Count_Total.Style.Add("font-family", "Calibri");
                    //tc_Stock_Count_Total.Style.Add("font-size", "10pt");
                    tr_total.Cells.Add(Chemist_POB_Count_Total);

                    int[] arrtotStock = new int[] { iTotStockCal };

                    for (int i = 0; i < arrtotStock.Length; i++)
                    {
                        isumStock += arrtotStock[i];
                    }

                    //decimal RoundStockCal = new decimal();

                    //double StockCal = (double)iTotStockCal / iFieldWrkCount;

                    //RoundStockCal = Math.Round((decimal)StockCal, 2);



                    TableCell tc_Stock_Count_Total = new TableCell();
                    tc_Stock_Count_Total.BorderStyle = BorderStyle.Solid;
                    tc_Stock_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_Stock_Count_Total = new Literal();
                    lit_Stock_Count_Total.Text = Convert.ToString(isumStock);
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

                    int[] arrtotUnLst = new int[] { iTotUnLstCal };

                    for (int i = 0; i < arrtotUnLst.Length; i++)
                    {
                        isumUnLst += arrtotUnLst[i];
                    }

                    //decimal RoundUnLstCal = new decimal();

                    //double UnLstCal = (double)iTotUnLstCal / iFieldWrkCount;

                    //RoundUnLstCal = Math.Round((decimal)UnLstCal, 2);

                    TableCell tc_UnLst_Count_Total = new TableCell();
                    tc_UnLst_Count_Total.BorderStyle = BorderStyle.Solid;
                    tc_UnLst_Count_Total.BorderWidth = 1;
                    //tc_catg_Total.Width = 25;
                    Literal lit_UnLst_Count_Total = new Literal();
                    lit_UnLst_Count_Total.Text = Convert.ToString(isumUnLst);
                    tc_UnLst_Count_Total.Controls.Add(lit_UnLst_Count_Total);
                    tc_UnLst_Count_Total.HorizontalAlign = HorizontalAlign.Center;
                    tc_UnLst_Count_Total.Attributes.Add("Class", "tbldetail_main");
                    tc_UnLst_Count_Total.Font.Bold.ToString();
                    // tc_UnLst_Count_Total.BackColor = System.Drawing.Color.White;
                    tc_UnLst_Count_Total.Style.Add("color", "Red");
                    tc_UnLst_Count_Total.Style.Add("background-color", "#ffe4b5");
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

                //pnlbutton.Visible = false;

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
}