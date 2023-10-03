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
using iTextSharp.tool.xml;
public partial class rptroutedeviation : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iTotLstCount1 = 0;
    int iTotLstCount = 0;
    int iTotLstCount2 = 0;
    string sMode = string.Empty;
    string Date = string.Empty;
    string Date_year = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DataSet dsmap = null;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string ChemistPOB_visit = string.Empty;
    string tot_Sub_days = string.Empty;
    string tot_dr = string.Empty;
    string tot_dre = string.Empty;
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
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;
    string MultiSf_Code = string.Empty;
    DataSet dsprd = new DataSet();
    string distributor_code = string.Empty;
    string Multi_Prod = string.Empty;
    string sCurrentDate = string.Empty;
    string strMode = string.Empty;
    int tot_ret = 0;
    int eff = 0;
    int visit = 0;
    int novisit = 0;
    
    int tot_retv = 0;
    int tot_retv1= 0;
    string totmap = string.Empty;
    int totdr = 0;
    Int32 tot_map;
    Int32 tot_eff;
    Int32 tot_visit;
    Int32 tot_novisit;
    string FDate = string.Empty;
    string TDate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
       
        strMode = Request.QueryString["Mode"].ToString();


        //string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        //lblHead.Text = "Retail Lost-Purchase Details for  " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;


        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
        //FillSF();
        if (strMode.Trim() == "Year")
        {
            Date = Request.QueryString["select"].ToString();
            lblHead.Text = " <span style='color:Red'>" + "Retailer Visit Status" + "</span> for the Year of " + Date;
            FillSF_year(Date);
        }
        else if (strMode.Trim() == "Month")
        {
            FDate = Request.QueryString["startdate"].ToString();
            TDate = Request.QueryString["enddate"].ToString();
            FYear = Request.QueryString["year"].ToString();
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            lblHead.Text = " <span style='color:Red'>" + "Retailer Visit Status" + "</span> from " + FDate + " to " + TDate;
            Date_year = FYear + "-" + Date;
            FillSF_Month(FYear, FDate, TDate);
        }
        else if (strMode.Trim() == "Date")
        {
            Date = Request.QueryString["select"].ToString();
            lblHead.Text = " <span style='color:Red'>" + "Retailer Visit Status" + "</span> for the Day of " + Date;
            FillSF();
        }

    }
    private void FillSF_year(string Year)
    {


        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();
        DataSet dsGV = new DataSet();
        DCR dc = new DCR();
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.Getretailer_distributor(divcode, distributor_code);
        //if (sfCode.Contains("MR"))
        //{
        //    dsGV = dc.Getroutedeviation_MR(divcode, sfCode, Date);
        //}
        //else
        //{
        //    dsGV = dc.Getroutedeviation(divcode, sfCode, Date);
        //}
        dsGV = sf.UserList_getMR(divcode, sfCode);
        if (dsGV.Tables[0].Rows.Count > 0)
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





            //TableCell tc_det_head_SNo = new TableCell();
            //tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
            //tc_det_head_SNo.BorderWidth = 1;
            //Literal lit_det_head_SNo = new Literal();
            //lit_det_head_SNo.Text = "<b>SFcode</b>";
            //tc_det_head_SNo.Attributes.Add("Class", "tblHead");
            //tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
            //tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
            //tr_header.Cells.Add(tc_det_head_SNo);


            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>SF Name</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            //TableCell tc_det_head_hq = new TableCell();
            //tc_det_head_hq.Width = 70;
            //tc_det_head_hq.BorderStyle = BorderStyle.Solid;
            //tc_det_head_hq.BorderWidth = 1;
            //Literal lit_det_head_hq = new Literal();
            //lit_det_head_hq.Text = "<b>Route(Stockist)</b>";
            //tc_det_head_hq.Attributes.Add("Class", "tblHead");
            //tc_det_head_hq.Controls.Add(lit_det_head_hq);
            //tc_det_head_hq.HorizontalAlign = HorizontalAlign.Center;
            //tr_header.Cells.Add(tc_det_head_hq);


            TableCell tc_det_head_hqd = new TableCell();
            tc_det_head_hqd.Width = 70;
            tc_det_head_hqd.BorderStyle = BorderStyle.Solid;
            tc_det_head_hqd.BorderWidth = 1;
            Literal lit_det_head_hqd = new Literal();
            lit_det_head_hqd.Text = "<b>Total Retailers</b>";
            tc_det_head_hqd.Attributes.Add("Class", "tblHead");
            tc_det_head_hqd.Controls.Add(lit_det_head_hqd);
            tc_det_head_hqd.HorizontalAlign = HorizontalAlign.Center;
            tr_header.Cells.Add(tc_det_head_hqd);


            SalesForce sal = new SalesForce();

            TableCell tc_monthp = new TableCell();
            tc_monthp.ColumnSpan = 1;
            Literal lit_monthp = new Literal();
            lit_monthp.Text = "Effective Retailer";
            tc_monthp.Attributes.Add("Class", "rptCellBorder");
            tc_monthp.BorderStyle = BorderStyle.Solid;
            tc_monthp.BorderWidth = 1;
            tc_monthp.HorizontalAlign = HorizontalAlign.Center;
            //tc_month.Width = 200;
            tc_monthp.Controls.Add(lit_monthp);
            tr_header.Cells.Add(tc_monthp);

            TableCell tc_month = new TableCell();
            tc_month.ColumnSpan = 1;
            Literal lit_month = new Literal();
            lit_month.Text = "Visited Retailer";
            tc_month.Attributes.Add("Class", "rptCellBorder");
            tc_month.BorderStyle = BorderStyle.Solid;
            tc_month.BorderWidth = 1;
            tc_month.HorizontalAlign = HorizontalAlign.Center;
            //tc_month.Width = 200;
            tc_month.Controls.Add(lit_month);
            tr_header.Cells.Add(tc_month);


            TableCell tc_monthr = new TableCell();
            tc_monthr.ColumnSpan = 1;
            Literal lit_monthr = new Literal();
            lit_monthr.Text = "Non- Visited Retailer";
            tc_monthr.Attributes.Add("Class", "rptCellBorder");
            tc_monthr.BorderStyle = BorderStyle.Solid;
            tc_monthr.BorderWidth = 1;
            tc_monthr.HorizontalAlign = HorizontalAlign.Center;
            //tc_month.Width = 200;
            tc_monthr.Controls.Add(lit_monthr);
            tr_header.Cells.Add(tc_monthr);


            tbl.Rows.Add(tr_header);
            //Sub Header



            if (dsGV.Tables[0].Rows.Count > 0)
                ViewState["dsGV"] = dsGV;


            int iCount = 0;
            //string iTotLstCount ="0";


            foreach (DataRow drFF in dsGV.Tables[0].Rows)
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
                //TableCell tc_det_usr = new TableCell();
                //Literal lit_det_usr = new Literal();
                //lit_det_usr.Text = "&nbsp;" + drFF["Sf_Code"].ToString();
                //tc_det_usr.BorderStyle = BorderStyle.Solid;
                //tc_det_usr.BorderWidth = 1;

                //tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                //tc_det_usr.Controls.Add(lit_det_usr);
                //tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 200;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Sf_Name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);




                Order ad = new Order();


                //dsDoc = ad.View_Route_deviation_plans(divcode, drFF["Sf_Code"].ToString(), Date);

                //if (dsDoc.Tables[0].Rows.Count > 0)
                //    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                //TableCell tc_lst_month = new TableCell();
                //Literal hyp_lst_month = new Literal();

                //if (tot_dr != "0")
                //{
                //    hyp_lst_month.Text = tot_dr;


                //}

                //else
                //{
                //    hyp_lst_month.Text = "";
                //}

                //tc_lst_month.BorderStyle = BorderStyle.Solid;
                //tc_lst_month.BorderWidth = 1;

                //tc_lst_month.BackColor = System.Drawing.Color.White;

                //tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                //tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                //tc_lst_month.Controls.Add(hyp_lst_month);
                //tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                //tr_det.Cells.Add(tc_lst_month);







                TableCell tc_det_last6monthsum = new TableCell();
                tc_det_last6monthsum.Width = 200;

                tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_det_sum = new Literal();
                //lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                tc_det_last6monthsum.BorderWidth = 1;
                tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                tc_det_last6monthsum.Controls.Add(lit_det_sum);
                tr_det.Cells.Add(tc_det_last6monthsum);

                dsDoc = ad.View_total_Retailercount_Year(divcode, drFF["Sf_Code"].ToString(), Year);


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
 tot_ret = Convert.ToInt32(dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0));
                if (tot_dr != "0" && tot_dr != "")
                {

                    lit_det_sum.Text = tot_dr.ToString();



                }
                else
                {
                    lit_det_sum.Text = "";

                }


                dsDoc = ad.View_effective_Retailercount_year(divcode, drFF["Sf_Code"].ToString(), Year);
                TableCell tc_lst_monthfge = new TableCell();
                HyperLink hyp_lst_monthfge = new HyperLink();

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dre = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (tot_dre != "0" && tot_dre != "")
                {

                    hyp_lst_monthfge.Text = tot_dre.ToString();
                    sURL = "rptroutedeviationEffretview_y.aspx?Division_code=" + divcode + "&sf_code=" + drFF["Sf_Code"].ToString() + "&date=" + Date + "";
                    hyp_lst_monthfge.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                    hyp_lst_monthfge.NavigateUrl = "#";


                }
                else
                {
                    hyp_lst_monthfge.Text = "";

                }

                tc_lst_monthfge.BorderStyle = BorderStyle.Solid;
                tc_lst_monthfge.BorderWidth = 1;

                tc_lst_monthfge.BackColor = System.Drawing.Color.White;

                tc_lst_monthfge.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_monthfge.VerticalAlign = VerticalAlign.Middle;
                tc_lst_monthfge.Controls.Add(hyp_lst_monthfge);
                tc_lst_monthfge.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_monthfge);



                //dsDoc = sf.retail_lost_Purchase_value_stockist(drFF["Territory_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate,distributor_code);
                dsDoc = ad.View_Retailercount_year(divcode, drFF["Sf_Code"].ToString(), Year);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
 tot_retv = Convert.ToInt32(dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0));
                TableCell tc_lst_monthfg = new TableCell();
                HyperLink hyp_lst_monthfg = new HyperLink();

                if (tot_dr != "0")
                {
                    hyp_lst_monthfg.Text = tot_dr;
                    sURL = "rptroutedeviationretview_y.aspx?Division_code=" + divcode + "&sf_code=" + drFF["Sf_Code"].ToString() + "&date=" + Date + "";
                    hyp_lst_monthfg.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                    hyp_lst_monthfg.NavigateUrl = "#";


                }

                else
                {
                    hyp_lst_monthfg.Text = "";
                }

                tc_lst_monthfg.BorderStyle = BorderStyle.Solid;
                tc_lst_monthfg.BorderWidth = 1;

                tc_lst_monthfg.BackColor = System.Drawing.Color.White;

                tc_lst_monthfg.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_monthfg.VerticalAlign = VerticalAlign.Middle;
                tc_lst_monthfg.Controls.Add(hyp_lst_monthfg);
                tc_lst_monthfg.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_monthfg);




              //  tot_dr = "0";



               // dsDoc = ad.View_Non_Retailercount_year(divcode, drFF["Sf_Code"].ToString(), Year);

               // if (dsDoc.Tables[0].Rows.Count > 0)
                  //  tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
 tot_retv1 = tot_ret - tot_retv;
                TableCell tc_lst_monthf = new TableCell();
                HyperLink hyp_lst_monthf = new HyperLink();

                if (tot_retv1 != 0)
                {
                    hyp_lst_monthf.Text = Convert.ToString(tot_retv1);
                    sURL = "rptroutedeviationnonretview_y.aspx?Division_code=" + divcode + "&sf_code=" + drFF["Sf_Code"].ToString() + "&date=" + Date + "&Mode=" + strMode + "";
                    hyp_lst_monthf.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                    hyp_lst_monthf.NavigateUrl = "#";








                }

                else
                {
                    hyp_lst_monthf.Text = "";
                }

                tc_lst_monthf.BorderStyle = BorderStyle.Solid;
                tc_lst_monthf.BorderWidth = 1;

                tc_lst_monthf.BackColor = System.Drawing.Color.White;

                tc_lst_monthf.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_monthf.VerticalAlign = VerticalAlign.Middle;
                tc_lst_monthf.Controls.Add(hyp_lst_monthf);
                tc_lst_monthf.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_monthf);




                tot_dr = "0";





                tbl.Rows.Add(tr_det);

            }






        }


        else
        {
            lblResultMsg.Text = "There were no records found to match your search.";
            lblResultMsg.Visible = true;


        }

    }

    private void FillSF_Month(string year, string FDate, string TDate)
    {


        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();
        DataSet dsGV = new DataSet();
        DCR dc = new DCR();
        SalesForce sf = new SalesForce();
        //if (sfCode.Contains("MR"))
        //{
        //    dsGV = dc.Getroutedeviation_MR(divcode, sfCode, Date);
        //}
        //else
        //{
        dsGV = sf.UserList_getMR(divcode, sfCode);
        // dsGV = dc.Getroutedeviation(divcode, sfCode, Date);
        //}
        if (dsGV.Tables[0].Rows.Count > 0)
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





            TableCell tc_det_head_SNo = new TableCell();
            tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_head_SNo.BorderWidth = 1;
            Literal lit_det_head_SNo = new Literal();
            lit_det_head_SNo.Text = "<b>SFcode</b>";
            tc_det_head_SNo.Attributes.Add("Class", "tblHead");
            tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
            tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
            tr_header.Cells.Add(tc_det_head_SNo);


            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>SF Name</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_mappedDR_Name = new TableCell();
            tc_mappedDR_Name.BorderStyle = BorderStyle.Solid;
            tc_mappedDR_Name.BorderWidth = 1;
            tc_mappedDR_Name.Width = 250;
            tc_mappedDR_Name.RowSpan = 1;
            Literal lit_mappedDR_Name = new Literal();
            lit_mappedDR_Name.Text = "<center>Mapped Retailers</center>";
            tc_mappedDR_Name.BorderColor = System.Drawing.Color.Black;
            tc_mappedDR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_mappedDR_Name.Controls.Add(lit_mappedDR_Name);
            tr_header.Cells.Add(tc_mappedDR_Name);


            //TableCell tc_det_head_hq = new TableCell();
            //tc_det_head_hq.Width = 70;
            //tc_det_head_hq.BorderStyle = BorderStyle.Solid;
            //tc_det_head_hq.BorderWidth = 1;
            //Literal lit_det_head_hq = new Literal();
            //lit_det_head_hq.Text = "<b>Route(Stockist)</b>";
            //tc_det_head_hq.Attributes.Add("Class", "tblHead");
            //tc_det_head_hq.Controls.Add(lit_det_head_hq);
            //tc_det_head_hq.HorizontalAlign = HorizontalAlign.Center;
            //tr_header.Cells.Add(tc_det_head_hq);


            TableCell tc_det_head_hqd = new TableCell();
            tc_det_head_hqd.Width = 70;
            tc_det_head_hqd.BorderStyle = BorderStyle.Solid;
            tc_det_head_hqd.BorderWidth = 1;
            Literal lit_det_head_hqd = new Literal();
            lit_det_head_hqd.Text = "<b>Total Retailers</b>";
            tc_det_head_hqd.Attributes.Add("Class", "tblHead");
            tc_det_head_hqd.Controls.Add(lit_det_head_hqd);
            tc_det_head_hqd.HorizontalAlign = HorizontalAlign.Center;
            tr_header.Cells.Add(tc_det_head_hqd);


            SalesForce sal = new SalesForce();

            TableCell tc_monthp = new TableCell();
            tc_monthp.ColumnSpan = 1;
            Literal lit_monthp = new Literal();
            lit_monthp.Text = "Effective Retailer";
            tc_monthp.Attributes.Add("Class", "rptCellBorder");
            tc_monthp.BorderStyle = BorderStyle.Solid;
            tc_monthp.BorderWidth = 1;
            tc_monthp.HorizontalAlign = HorizontalAlign.Center;
            //tc_month.Width = 200;
            tc_monthp.Controls.Add(lit_monthp);
            tr_header.Cells.Add(tc_monthp);

            TableCell tc_month = new TableCell();
            tc_month.ColumnSpan = 1;
            Literal lit_month = new Literal();
            lit_month.Text = "Visited Retailer";
            tc_month.Attributes.Add("Class", "rptCellBorder");
            tc_month.BorderStyle = BorderStyle.Solid;
            tc_month.BorderWidth = 1;
            tc_month.HorizontalAlign = HorizontalAlign.Center;
            //tc_month.Width = 200;
            tc_month.Controls.Add(lit_month);
            tr_header.Cells.Add(tc_month);


            TableCell tc_monthr = new TableCell();
            tc_monthr.ColumnSpan = 1;
            Literal lit_monthr = new Literal();
            lit_monthr.Text = "Non- Visited Retailer";
            tc_monthr.Attributes.Add("Class", "rptCellBorder");
            tc_monthr.BorderStyle = BorderStyle.Solid;
            tc_monthr.BorderWidth = 1;
            tc_monthr.HorizontalAlign = HorizontalAlign.Center;
            //tc_month.Width = 200;
            tc_monthr.Controls.Add(lit_monthr);
            tr_header.Cells.Add(tc_monthr);


            tbl.Rows.Add(tr_header);
            //Sub Header



            if (dsGV.Tables[0].Rows.Count > 0)
                ViewState["dsGV"] = dsGV;


            int iCount = 0;
            //string iTotLstCount ="0";


            foreach (DataRow drFF in dsGV.Tables[0].Rows)
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
                lit_det_usr.Text = "&nbsp;" + drFF["Sf_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;

                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 200;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Sf_Name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);




                Order ad = new Order();


                //dsDoc = ad.View_Route_deviation_plans(divcode, drFF["Sf_Code"].ToString(), Date);

                //if (dsDoc.Tables[0].Rows.Count > 0)
                //    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                //TableCell tc_lst_month = new TableCell();
                //Literal hyp_lst_month = new Literal();

                //if (tot_dr != "0")
                //{
                //    hyp_lst_month.Text = tot_dr;


                //}

                //else
                //{
                //    hyp_lst_month.Text = "";
                //}

                //tc_lst_month.BorderStyle = BorderStyle.Solid;
                //tc_lst_month.BorderWidth = 1;

                //tc_lst_month.BackColor = System.Drawing.Color.White;

                //tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                //tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                //tc_lst_month.Controls.Add(hyp_lst_month);
                //tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                //tr_det.Cells.Add(tc_lst_month);


                TableCell tc_mapped_retailers = new TableCell();
                tc_mapped_retailers.Width = 200;

                tc_mapped_retailers.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_mapped = new Literal();
                tc_mapped_retailers.BorderStyle = BorderStyle.Solid;
                tc_mapped_retailers.BorderWidth = 1;
                tc_mapped_retailers.Attributes.Add("Class", "rptCellBorder");
                tc_mapped_retailers.Controls.Add(lit_mapped);
                tr_det.Cells.Add(tc_mapped_retailers);

                dsDoc = ad.View_mappedRetailers(divcode, drFF["Sf_Code"].ToString());

                if (dsDoc.Tables[0].Rows.Count > 0)
                    totmap = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                totdr = Convert.ToInt32(dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0));
                if (totmap != "0" && totmap != "")
                {
                    lit_mapped.Text = totmap.ToString();
                }
                else
                {
                    lit_mapped.Text = "";

                }




                TableCell tc_det_last6monthsum = new TableCell();
                tc_det_last6monthsum.Width = 200;

                tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_det_sum = new Literal();
                //lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                tc_det_last6monthsum.BorderWidth = 1;
                tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                tc_det_last6monthsum.Controls.Add(lit_det_sum);
                tr_det.Cells.Add(tc_det_last6monthsum);

                dsDoc = ad.View_total_Retailercount_mon(divcode, drFF["Sf_Code"].ToString(), year, FDate, TDate);


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                tot_ret = Convert.ToInt32(dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0));
                tot_map += tot_ret;
                if (tot_dr != "0" && tot_dr != "")
                {

                    lit_det_sum.Text = tot_dr.ToString();



                }
                else
                {
                    lit_det_sum.Text = "";

                }

                //TableCell tc_det_last6monthsume = new TableCell();
                //tc_det_last6monthsume.Width = 200;

                //tc_det_last6monthsume.HorizontalAlign = HorizontalAlign.Center;
                //Literal lit_det_sume = new Literal();
                ////lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                //tc_det_last6monthsume.BorderStyle = BorderStyle.Solid;
                //tc_det_last6monthsume.BorderWidth = 1;
                //tc_det_last6monthsume.Attributes.Add("Class", "rptCellBorder");
                //tc_det_last6monthsume.Controls.Add(lit_det_sume);
                //tr_det.Cells.Add(tc_det_last6monthsume);

                dsDoc = ad.View_effective_Retailercount_mon(divcode, drFF["Sf_Code"].ToString(), year, FDate, TDate);

                TableCell tc_lst_monthfge = new TableCell();
                HyperLink hyp_lst_monthfge = new HyperLink();

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dre = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                eff = Convert.ToInt32(dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0));
                tot_eff += eff;
                if (tot_dre != "0" && tot_dre != "")
                {

                    hyp_lst_monthfge.Text = tot_dre.ToString();
                    sURL = "rptroutedeviationEffretview_mon.aspx?Div_code=" + divcode + "&sf_code=" + drFF["Sf_Code"].ToString() + "&FDate=" + FDate + "&TDate=" + TDate + "";
                    hyp_lst_monthfge.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                    hyp_lst_monthfge.NavigateUrl = "#";


                }
                else
                {
                    hyp_lst_monthfge.Text = "";

                }

                tc_lst_monthfge.BorderStyle = BorderStyle.Solid;
                tc_lst_monthfge.BorderWidth = 1;

                tc_lst_monthfge.BackColor = System.Drawing.Color.White;

                tc_lst_monthfge.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_monthfge.VerticalAlign = VerticalAlign.Middle;
                tc_lst_monthfge.Controls.Add(hyp_lst_monthfge);
                tc_lst_monthfge.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_monthfge);

                //dsDoc = sf.retail_lost_Purchase_value_stockist(drFF["Territory_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate,distributor_code);
                dsDoc = ad.View_Retailercount(divcode, drFF["Sf_Code"].ToString(), FDate, TDate);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
 tot_retv = Convert.ToInt32(dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0));
                tot_visit += tot_retv;
                TableCell tc_lst_monthfg = new TableCell();
                HyperLink hyp_lst_monthfg = new HyperLink();

                if (tot_dr != "0")
                {
                    hyp_lst_monthfg.Text = tot_dr;
                    sURL = "rptroutedeviationretview_mon.aspx?Division_code=" + divcode + "&sf_code=" + drFF["Sf_Code"].ToString() + "&FDate=" + FDate + "&TDate=" + TDate + "";
                    hyp_lst_monthfg.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                    hyp_lst_monthfg.NavigateUrl = "#";








                }

                else
                {
                    hyp_lst_monthfg.Text = "";
                }

                tc_lst_monthfg.BorderStyle = BorderStyle.Solid;
                tc_lst_monthfg.BorderWidth = 1;

                tc_lst_monthfg.BackColor = System.Drawing.Color.White;

                tc_lst_monthfg.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_monthfg.VerticalAlign = VerticalAlign.Middle;
                tc_lst_monthfg.Controls.Add(hyp_lst_monthfg);
                tc_lst_monthfg.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_monthfg);




               // tot_dr = "0";



                //dsDoc = ad.View_Non_Retailercount_mon(divcode, drFF["Sf_Code"].ToString(), year, Month);

                //if (dsDoc.Tables[0].Rows.Count > 0)
                   // tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                tot_retv1 = tot_ret - tot_retv;
                tot_novisit += tot_retv1;
                TableCell tc_lst_monthf = new TableCell();
                HyperLink hyp_lst_monthf = new HyperLink();

                if (tot_retv1 != 0)
                {
                    hyp_lst_monthf.Text = Convert.ToString(tot_retv1);
                    sURL = "rptroutedeviationnonretview_mon.aspx?Division_code=" + divcode + "&sf_code=" + drFF["Sf_Code"].ToString() + "&TDate=" + TDate + "&FDate=" + FDate + "";
                    hyp_lst_monthf.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                    hyp_lst_monthf.NavigateUrl = "#";









                }

                else
                {
                    hyp_lst_monthf.Text = "";
                }

                tc_lst_monthf.BorderStyle = BorderStyle.Solid;
                tc_lst_monthf.BorderWidth = 1;

                tc_lst_monthf.BackColor = System.Drawing.Color.White;

                tc_lst_monthf.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_monthf.VerticalAlign = VerticalAlign.Middle;
                tc_lst_monthf.Controls.Add(hyp_lst_monthf);
                tc_lst_monthf.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_monthf);




                tot_dr = "0";





                tbl.Rows.Add(tr_det);

            }
            TableRow tot_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell emcell = new TableCell();
            tot_header.Cells.Add(emcell);

            TableCell tc_SNoo = new TableCell();
            tc_SNoo.ColumnSpan = 3;
            tc_SNoo.BorderStyle = BorderStyle.Solid;
            tc_SNoo.BorderWidth = 1;
            tc_SNoo.Width = 50;
            tc_SNoo.RowSpan = 1;
            tc_SNoo.HorizontalAlign = HorizontalAlign.Right;
            Literal lit_SNoo = new Literal();
            lit_SNoo.Text = "Total";
            tc_SNoo.BorderColor = System.Drawing.Color.Black;
            tc_SNoo.Controls.Add(lit_SNoo);
            tc_SNoo.Attributes.Add("Class", "rptCellBorder");
            tot_header.Cells.Add(tc_SNoo);


            TableCell tc_totret = new TableCell();
            tc_totret.BorderStyle = BorderStyle.Solid;
            tc_totret.BorderWidth = 1;
            tc_totret.Width = 50;
            tc_totret.RowSpan = 1;
            Literal lit_maptot = new Literal();
            lit_maptot.Text = tot_map.ToString();
            tc_totret.HorizontalAlign = HorizontalAlign.Center;
            tc_totret.BorderColor = System.Drawing.Color.Black;
            tc_totret.Controls.Add(lit_maptot);
            tc_totret.Attributes.Add("Class", "rptCellBorder");
            tot_header.Cells.Add(tc_totret);


            TableCell tc_toteff = new TableCell();
            tc_toteff.BorderStyle = BorderStyle.Solid;
            tc_toteff.BorderWidth = 1;
            tc_toteff.Width = 50;
            tc_toteff.RowSpan = 1;
            Literal lit_toteff = new Literal();
            lit_toteff.Text = tot_eff.ToString();
            tc_toteff.HorizontalAlign = HorizontalAlign.Center;
            tc_toteff.BorderColor = System.Drawing.Color.Black;
            tc_toteff.Controls.Add(lit_toteff);
            tc_toteff.Attributes.Add("Class", "rptCellBorder");
            tot_header.Cells.Add(tc_toteff);

            TableCell tc_totvisit = new TableCell();
            tc_totvisit.BorderStyle = BorderStyle.Solid;
            tc_totvisit.BorderWidth = 1;
            tc_totvisit.Width = 50;
            tc_totvisit.RowSpan = 1;
            Literal lit_totvisit = new Literal();
            lit_totvisit.Text = tot_visit.ToString();
            tc_totvisit.HorizontalAlign = HorizontalAlign.Center;
            tc_totvisit.BorderColor = System.Drawing.Color.Black;
            tc_totvisit.Controls.Add(lit_totvisit);
            tc_totvisit.Attributes.Add("Class", "rptCellBorder");
            tot_header.Cells.Add(tc_totvisit);

            TableCell tc_totnovisit = new TableCell();
            tc_totnovisit.BorderStyle = BorderStyle.Solid;
            tc_totnovisit.BorderWidth = 1;
            tc_totnovisit.Width = 50;
            tc_totnovisit.RowSpan = 1;
            Literal lit_totnovisit = new Literal();
            lit_totnovisit.Text = tot_novisit.ToString();
            tc_totnovisit.HorizontalAlign = HorizontalAlign.Center;
            tc_totnovisit.BorderColor = System.Drawing.Color.Black;
            tc_totnovisit.Controls.Add(lit_totnovisit);
            tc_totnovisit.Attributes.Add("Class", "rptCellBorder");
            tot_header.Cells.Add(tc_totnovisit);

            tbl.Rows.Add(tot_header);






        }


        else
        {
            lblResultMsg.Text = "There were no records found to match your search.";
            lblResultMsg.Visible = true;


        }

    }

    private void FillSF()
    {


        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();
        DataSet dsGV = new DataSet();
        DCR dc = new DCR();
        SalesForce sf = new SalesForce();
        //if (sfCode.Contains("MR"))
        //{
        //    dsGV = dc.Getroutedeviation_MR(divcode, sfCode, Date);

        //}
        //else
        //{
          // dsGV = dc.Getroutedeviation(divcode, sfCode, Date);
        dsGV = sf.UserList_getMR(divcode, sfCode);
        
       // }
        if (dsGV.Tables[0].Rows.Count > 0)
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





            TableCell tc_det_head_SNo = new TableCell();
            tc_det_head_SNo.BorderStyle = BorderStyle.Solid;
            tc_det_head_SNo.BorderWidth = 1;
            Literal lit_det_head_SNo = new Literal();
            lit_det_head_SNo.Text = "<b>SFcode</b>";
            tc_det_head_SNo.Attributes.Add("Class", "tblHead");
            tc_det_head_SNo.Controls.Add(lit_det_head_SNo);
            tc_det_head_SNo.HorizontalAlign = HorizontalAlign.Center;
            tr_header.Cells.Add(tc_det_head_SNo);


            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>SF Name</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_mappedDR_Name = new TableCell();
            tc_mappedDR_Name.BorderStyle = BorderStyle.Solid;
            tc_mappedDR_Name.BorderWidth = 1;
            tc_mappedDR_Name.Width = 250;
            tc_mappedDR_Name.RowSpan = 1;
            Literal lit_mappedDR_Name = new Literal();
            lit_mappedDR_Name.Text = "<center>Mapped Retailers</center>";
            tc_mappedDR_Name.BorderColor = System.Drawing.Color.Black;
            tc_mappedDR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_mappedDR_Name.Controls.Add(lit_mappedDR_Name);
            tr_header.Cells.Add(tc_mappedDR_Name);


            //TableCell tc_det_head_hq = new TableCell();
            //tc_det_head_hq.Width = 70;
            //tc_det_head_hq.BorderStyle = BorderStyle.Solid;
            //tc_det_head_hq.BorderWidth = 1;
            //Literal lit_det_head_hq = new Literal();
            //lit_det_head_hq.Text = "<b>Route(Stockist)</b>";
            //tc_det_head_hq.Attributes.Add("Class", "tblHead");
            //tc_det_head_hq.Controls.Add(lit_det_head_hq);
            //tc_det_head_hq.HorizontalAlign = HorizontalAlign.Center;
            //tr_header.Cells.Add(tc_det_head_hq);


            TableCell tc_det_head_hqd = new TableCell();
            tc_det_head_hqd.Width = 70;
            tc_det_head_hqd.BorderStyle = BorderStyle.Solid;
            tc_det_head_hqd.BorderWidth = 1;
            Literal lit_det_head_hqd = new Literal();
            lit_det_head_hqd.Text = "<b>Total Retailers</b>";
            tc_det_head_hqd.Attributes.Add("Class", "tblHead");
            tc_det_head_hqd.Controls.Add(lit_det_head_hqd);
            tc_det_head_hqd.HorizontalAlign = HorizontalAlign.Center;
            tr_header.Cells.Add(tc_det_head_hqd);


            SalesForce sal = new SalesForce();

            TableCell tc_monthp = new TableCell();
            tc_monthp.ColumnSpan = 1;
            Literal lit_monthp = new Literal();
            lit_monthp.Text = "Effective Retailer";
            tc_monthp.Attributes.Add("Class", "rptCellBorder");
            tc_monthp.BorderStyle = BorderStyle.Solid;
            tc_monthp.BorderWidth = 1;
            tc_monthp.HorizontalAlign = HorizontalAlign.Center;
            //tc_month.Width = 200;
            tc_monthp.Controls.Add(lit_monthp);
            tr_header.Cells.Add(tc_monthp);

            TableCell tc_month = new TableCell();
            tc_month.ColumnSpan = 1;
            Literal lit_month = new Literal();
            lit_month.Text = "Visited Retailer";
            tc_month.Attributes.Add("Class", "rptCellBorder");
            tc_month.BorderStyle = BorderStyle.Solid;
            tc_month.BorderWidth = 1;
            tc_month.HorizontalAlign = HorizontalAlign.Center;
            //tc_month.Width = 200;
            tc_month.Controls.Add(lit_month);
            tr_header.Cells.Add(tc_month);


            TableCell tc_monthr = new TableCell();
            tc_monthr.ColumnSpan = 1;
            Literal lit_monthr = new Literal();
            lit_monthr.Text = "Non- Visited Retailer";
            tc_monthr.Attributes.Add("Class", "rptCellBorder");
            tc_monthr.BorderStyle = BorderStyle.Solid;
            tc_monthr.BorderWidth = 1;
            tc_monthr.HorizontalAlign = HorizontalAlign.Center;
            //tc_month.Width = 200;
            tc_monthr.Controls.Add(lit_monthr);
            tr_header.Cells.Add(tc_monthr);


            tbl.Rows.Add(tr_header);
            //Sub Header



            if (dsGV.Tables[0].Rows.Count > 0)
                ViewState["dsGV"] = dsGV;


            int iCount = 0;
            //string iTotLstCount ="0";


            foreach (DataRow drFF in dsGV.Tables[0].Rows)
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
                lit_det_usr.Text = "&nbsp;" + drFF["Sf_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;

                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 200;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Sf_Name"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);




                Order ad = new Order();


                TableCell tc_mapped_retailers = new TableCell();
                tc_mapped_retailers.Width = 200;

                tc_mapped_retailers.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_mapped = new Literal();
                tc_mapped_retailers.BorderStyle = BorderStyle.Solid;
                tc_mapped_retailers.BorderWidth = 1;
                tc_mapped_retailers.Attributes.Add("Class", "rptCellBorder");
                tc_mapped_retailers.Controls.Add(lit_mapped);
                tr_det.Cells.Add(tc_mapped_retailers);

                dsDoc = ad.View_mappedRetailers(divcode, drFF["Sf_Code"].ToString());

                if (dsDoc.Tables[0].Rows.Count > 0)
                    totmap = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                totdr = Convert.ToInt32(dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0));
                if (totmap !="0" && totmap!="")
                {
                    lit_mapped.Text = totmap.ToString();
                }
                else
                {
                    lit_mapped.Text = "";

                }

                TableCell tc_det_last6monthsum = new TableCell();
                tc_det_last6monthsum.Width = 200;

                tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_det_sum = new Literal();
                //lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                tc_det_last6monthsum.BorderWidth = 1;
                tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                tc_det_last6monthsum.Controls.Add(lit_det_sum);
                tr_det.Cells.Add(tc_det_last6monthsum);

                dsDoc = ad.View_total_Retailercount_day(divcode, drFF["Sf_Code"].ToString(), Date);


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
tot_ret = Convert.ToInt32(dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0));
                tot_map += tot_ret;
                if (tot_dr != "0" && tot_dr != "")
                {

                    lit_det_sum.Text = tot_dr.ToString();



                }
                else
                {
                    lit_det_sum.Text = "";

                }

                //TableCell tc_det_last6monthsume = new TableCell();
                //tc_det_last6monthsume.Width = 200;

                //tc_det_last6monthsume.HorizontalAlign = HorizontalAlign.Center;
                //Literal lit_det_sume = new Literal();
                ////lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                //tc_det_last6monthsume.BorderStyle = BorderStyle.Solid;
                //tc_det_last6monthsume.BorderWidth = 1;
                //tc_det_last6monthsume.Attributes.Add("Class", "rptCellBorder");
                //tc_det_last6monthsume.Controls.Add(lit_det_sume);
                //tr_det.Cells.Add(tc_det_last6monthsume);

                dsDoc = ad.View_effective_Retailercount_day(divcode, drFF["Sf_Code"].ToString(), Date);

                TableCell tc_lst_monthfge = new TableCell();
                HyperLink hyp_lst_monthfge = new HyperLink();

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dre = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                eff = Convert.ToInt32(dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0));
                tot_eff += eff;
                if (tot_dre != "0" && tot_dre != "")
                {

                    hyp_lst_monthfge.Text = tot_dre.ToString();
                    sURL = "rptroutedeviationEffretview.aspx?Division_code=" + divcode + "&sf_code=" + drFF["Sf_Code"].ToString() + "&date=" + Date + "";
                    hyp_lst_monthfge.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                    hyp_lst_monthfge.NavigateUrl = "#";


                }
                else
                {
                    hyp_lst_monthfge.Text = "";

                }

                tc_lst_monthfge.BorderStyle = BorderStyle.Solid;
                tc_lst_monthfge.BorderWidth = 1;

                tc_lst_monthfge.BackColor = System.Drawing.Color.White;

                tc_lst_monthfge.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_monthfge.VerticalAlign = VerticalAlign.Middle;
                tc_lst_monthfge.Controls.Add(hyp_lst_monthfge);
                tc_lst_monthfge.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_monthfge);

                //dsDoc = sf.retail_lost_Purchase_value_stockist(drFF["Territory_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate,distributor_code);
                dsDoc = ad.View_Retailercount_day(divcode, drFF["Sf_Code"].ToString(), Date);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    tot_retv = Convert.ToInt32(dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0));
                tot_visit += tot_retv;
                TableCell tc_lst_monthfg = new TableCell();
                HyperLink hyp_lst_monthfg = new HyperLink();

                if (tot_dr != "0")
                {
                    hyp_lst_monthfg.Text = tot_dr;
                    sURL = "rptroutedeviationretview.aspx?Division_code=" + divcode + "&sf_code=" + drFF["Sf_Code"].ToString() + "&date=" + Date + "";
                    hyp_lst_monthfg.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                    hyp_lst_monthfg.NavigateUrl = "#";








                }

                else
                {
                    hyp_lst_monthfg.Text = "";
                }

                tc_lst_monthfg.BorderStyle = BorderStyle.Solid;
                tc_lst_monthfg.BorderWidth = 1;

                tc_lst_monthfg.BackColor = System.Drawing.Color.White;

                tc_lst_monthfg.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_monthfg.VerticalAlign = VerticalAlign.Middle;
                tc_lst_monthfg.Controls.Add(hyp_lst_monthfg);
                tc_lst_monthfg.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_monthfg);




               // tot_dr = "0";



               // dsDoc = ad.View_Non_Retailercount_day(divcode, drFF["Sf_Code"].ToString(), Date);

               // if (dsDoc.Tables[0].Rows.Count > 0)
                 //   tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                tot_retv1 = tot_ret - tot_retv;
                tot_novisit += tot_retv1;
                TableCell tc_lst_monthf = new TableCell();
                HyperLink hyp_lst_monthf = new HyperLink();

                if (tot_retv1 != 0)
                {
                    hyp_lst_monthf.Text =  Convert.ToString(tot_retv1);
                    sURL = "rptroutedeviationnonretview.aspx?Division_code=" + divcode + "&sf_code=" + drFF["Sf_Code"].ToString() + "&date=" + Date + "";
                    hyp_lst_monthf.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                    hyp_lst_monthf.NavigateUrl = "#";








                }

                else
                {
                    hyp_lst_monthf.Text = "";
                }

                tc_lst_monthf.BorderStyle = BorderStyle.Solid;
                tc_lst_monthf.BorderWidth = 1;

                tc_lst_monthf.BackColor = System.Drawing.Color.White;

                tc_lst_monthf.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_monthf.VerticalAlign = VerticalAlign.Middle;
                tc_lst_monthf.Controls.Add(hyp_lst_monthf);
                tc_lst_monthf.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_monthf);




                tot_dr = "0";





                tbl.Rows.Add(tr_det);

            }
            TableRow tot_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell emcell = new TableCell();
            tot_header.Cells.Add(emcell);

            TableCell tc_SNoo = new TableCell();
            tc_SNoo.ColumnSpan = 3;
            tc_SNoo.BorderStyle = BorderStyle.Solid;
            tc_SNoo.BorderWidth = 1;
            tc_SNoo.Width = 50;
            tc_SNoo.RowSpan = 1;
            tc_SNoo.HorizontalAlign = HorizontalAlign.Right;
            Literal lit_SNoo =  new Literal();
            lit_SNoo.Text = "Total";
            tc_SNoo.BorderColor = System.Drawing.Color.Black;
            tc_SNoo.Controls.Add(lit_SNoo);
            tc_SNoo.Attributes.Add("Class", "rptCellBorder");         
            tot_header.Cells.Add(tc_SNoo);
            

            TableCell tc_totret = new TableCell();
            tc_totret.BorderStyle = BorderStyle.Solid;
            tc_totret.BorderWidth = 1;
            tc_totret.Width = 50;
            tc_totret.RowSpan = 1;
            Literal lit_maptot = new Literal();
            lit_maptot.Text = tot_map.ToString();
            tc_totret.HorizontalAlign = HorizontalAlign.Center;
            tc_totret.BorderColor = System.Drawing.Color.Black;
            tc_totret.Controls.Add(lit_maptot);
            tc_totret.Attributes.Add("Class", "rptCellBorder");    
            tot_header.Cells.Add(tc_totret); 
            

            TableCell tc_toteff = new TableCell();
            tc_toteff.BorderStyle = BorderStyle.Solid;
            tc_toteff.BorderWidth = 1;
            tc_toteff.Width = 50;
            tc_toteff.RowSpan = 1;
            Literal lit_toteff = new Literal();
            lit_toteff.Text = tot_eff.ToString();
            tc_toteff.HorizontalAlign = HorizontalAlign.Center;
            tc_toteff.BorderColor = System.Drawing.Color.Black;
            tc_toteff.Controls.Add(lit_toteff);
            tc_toteff.Attributes.Add("Class", "rptCellBorder");
            tot_header.Cells.Add(tc_toteff);

            TableCell tc_totvisit = new TableCell();
            tc_totvisit.BorderStyle = BorderStyle.Solid;
            tc_totvisit.BorderWidth = 1;
            tc_totvisit.Width = 50;
            tc_totvisit.RowSpan = 1;
            Literal lit_totvisit = new Literal();
            lit_totvisit.Text = tot_visit.ToString();
            tc_totvisit.HorizontalAlign = HorizontalAlign.Center;
            tc_totvisit.BorderColor = System.Drawing.Color.Black;
            tc_totvisit.Controls.Add(lit_totvisit);
            tc_totvisit.Attributes.Add("Class", "rptCellBorder");
            tot_header.Cells.Add(tc_totvisit);

            TableCell tc_totnovisit = new TableCell();
            tc_totnovisit.BorderStyle = BorderStyle.Solid;
            tc_totnovisit.BorderWidth = 1;
            tc_totnovisit.Width = 50;
            tc_totnovisit.RowSpan = 1;
            Literal lit_totnovisit = new Literal();
            lit_totnovisit.Text = tot_novisit.ToString();
            tc_totnovisit.HorizontalAlign = HorizontalAlign.Center;
            tc_totnovisit.BorderColor = System.Drawing.Color.Black;
            tc_totnovisit.Controls.Add(lit_totnovisit);
            tc_totnovisit.Attributes.Add("Class", "rptCellBorder");
            tot_header.Cells.Add(tc_totnovisit);

            tbl.Rows.Add(tot_header);
        }


        else
        {
            lblResultMsg.Text = "There were no records found to match your search.";
            lblResultMsg.Visible = true;


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

    protected void btnClose_Click(object sender, EventArgs e)
    {

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

}