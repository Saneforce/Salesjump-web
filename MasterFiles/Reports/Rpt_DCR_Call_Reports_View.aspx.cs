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
public partial class Rpt_DCR_Call_Reports_View : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iTotLstCount1 = 0;
    int iTotLstCount = 0;
    int iTotLstCount2 = 0;
    Decimal iTotLstCount3 = 0;
    Decimal iTotLstCount4 = 0;
    int iTotLstCount5 = 0;
    int iTotLstCount6 = 0;
    DataSet dsLocs = null;
    string sMode = string.Empty;
    string Date = string.Empty;
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
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;
    string MultiSf_Code = string.Empty;
    DataSet dsprd = new DataSet();
    string distributor_code = string.Empty;
    string Multi_Prod = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dssf = null;
    string sf_code = string.Empty;
    string Sf_Name = string.Empty;
    string Sf_HQ = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        Date = Request.QueryString["Date"].ToString();


        //System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        //string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        //lblHead.Text = "Retail Lost-Purchase Details for  " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;
        lblHead.Text = " <span style='color:Red'>" + "Daily Call Report (Detail View)" + "</span> for the Day of " + Date;

        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
        FillSF();

    }

    private void FillSF()
    {



        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();
        //tbl1.Rows.Clear();
        DataSet dsGV = new DataSet();
        DCR dc = new DCR();
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.Getretailer_distributor(divcode, distributor_code);

        dsGV = dc.GetCallReport(divcode, sfCode, Date);
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

            TableCell tc_WT_Name = new TableCell();
            tc_WT_Name.BorderStyle = BorderStyle.Solid;
            tc_WT_Name.BorderWidth = 1;
            tc_WT_Name.Width = 250;
            tc_WT_Name.RowSpan = 1;
            Literal lit_WT_Name = new Literal();
            lit_WT_Name.Text = "<center>WorkType</center>";
            tc_WT_Name.BorderColor = System.Drawing.Color.Black;
            tc_WT_Name.Attributes.Add("Class", "tblHead");
            tc_WT_Name.Controls.Add(lit_WT_Name);
            tr_header.Cells.Add(tc_WT_Name);

            TableCell tc_det_head_hq = new TableCell();
            tc_det_head_hq.Width = 70;
            tc_det_head_hq.BorderStyle = BorderStyle.Solid;
            tc_det_head_hq.BorderWidth = 1;
            Literal lit_det_head_hq = new Literal();
            lit_det_head_hq.Text = "<b>Route</b>";
            tc_det_head_hq.Attributes.Add("Class", "tblHead");
            tc_det_head_hq.Controls.Add(lit_det_head_hq);
            tc_det_head_hq.HorizontalAlign = HorizontalAlign.Center;
            tr_header.Cells.Add(tc_det_head_hq);




            SalesForce sal = new SalesForce();

            TableCell tc_monthrr = new TableCell();
            tc_monthrr.ColumnSpan = 1;
            Literal lit_monthrr = new Literal();
            lit_monthrr.Text = "Total Retailers";
            tc_monthrr.Attributes.Add("Class", "rptCellBorder");
            tc_monthrr.BorderStyle = BorderStyle.Solid;
            tc_monthrr.BorderWidth = 1;
            tc_monthrr.HorizontalAlign = HorizontalAlign.Center;
            //tc_month.Width = 200;
            tc_monthrr.Controls.Add(lit_monthrr);
            tr_header.Cells.Add(tc_monthrr);

            TableCell tc_month = new TableCell();
            tc_month.ColumnSpan = 1;
            Literal lit_month = new Literal();
            lit_month.Text = "Visiter Retailers";
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
            lit_monthr.Text = "Non visited Retailers";
            tc_monthr.Attributes.Add("Class", "rptCellBorder");
            tc_monthr.BorderStyle = BorderStyle.Solid;
            tc_monthr.BorderWidth = 1;
            tc_monthr.HorizontalAlign = HorizontalAlign.Center;
            //tc_month.Width = 200;
            tc_monthr.Controls.Add(lit_monthr);
            tr_header.Cells.Add(tc_monthr);

            TableCell tc_monthrv = new TableCell();
            tc_monthrv.ColumnSpan = 1;
            Literal lit_monthrv = new Literal();
            lit_monthrv.Text = "Value";
            tc_monthrv.Attributes.Add("Class", "rptCellBorder");
            tc_monthrv.BorderStyle = BorderStyle.Solid;
            tc_monthrv.BorderWidth = 1;
            tc_monthrv.HorizontalAlign = HorizontalAlign.Center;
            //tc_month.Width = 200;
            tc_monthrv.Controls.Add(lit_monthrv);
            tr_header.Cells.Add(tc_monthrv);

            TableCell tc_monthrw = new TableCell();
            tc_monthrw.ColumnSpan = 1;
            Literal lit_monthrw = new Literal();
            lit_monthrw.Text = "Net Weight Value";
            tc_monthrw.Attributes.Add("Class", "rptCellBorder");
            tc_monthrw.BorderStyle = BorderStyle.Solid;
            tc_monthrw.BorderWidth = 1;
            tc_monthrw.HorizontalAlign = HorizontalAlign.Center;
            //tc_month.Width = 200;
            tc_monthrw.Controls.Add(lit_monthrw);
            tr_header.Cells.Add(tc_monthrw);


            TableCell tdistance = new TableCell();
            tdistance.ColumnSpan = 1;
            Literal lit_tdistance = new Literal();
            lit_tdistance.Text = "Distance Travelled";
            tdistance.Attributes.Add("Class", "rptCellBorder");
            tdistance.BorderStyle = BorderStyle.Solid;
            tdistance.BorderWidth = 1;
            tdistance.HorizontalAlign = HorizontalAlign.Center;
            //tc_month.Width = 200;
            tdistance.Controls.Add(lit_tdistance);
            tr_header.Cells.Add(tdistance);


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


                TableCell tc_det_WT = new TableCell();
                tc_det_WT.Width = 100;
                HyperLink lit_det_WT = new HyperLink();
                lit_det_WT.Text = "&nbsp;" + drFF["Worktype_Name_B"].ToString();
                tc_det_WT.BorderStyle = BorderStyle.Solid;
                tc_det_WT.BorderWidth = 1;
                tc_det_WT.Attributes.Add("Class", "rptCellBorder");
                tc_det_WT.Controls.Add(lit_det_WT);
                tr_det.Cells.Add(tc_det_WT);


                //sURL = "rptPurchas_Register_Category_wise.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + " &TYear=" + TYear + "&TMonth=" + TMonth + "&sCurrentDate=" + sCurrentDate + "";
                //lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                //lit_det_FF.NavigateUrl = "#";






                Order ad = new Order();


                //dsDoc = sf.retail_lost_Purchase_value_stockist(drFF["Territory_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate,distributor_code);
                dsDoc = ad.View_Route_deviation_plans(divcode, drFF["Sf_Code"].ToString(), Date);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                TableCell tc_lst_month = new TableCell();
                HyperLink hyp_lst_month = new HyperLink();

                if (tot_dr != "0")
                {

                    hyp_lst_month.Text = tot_dr;
                    // sURL = "rpt_retail_lost_sel_month.aspx?Territory_Code=" + drFF["Territory_Code"] + "&Territory_Name=" + drFF["Territory_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + " &TYear=" + TYear + "&TMonth=" + TMonth + "&distributor_code=" + distributor_code + "&tdate=" + tom + "&fdate=" + st + "";
                    //hyp_lst_month.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                    //hyp_lst_month.NavigateUrl = "#";








                }

                else
                {
                    hyp_lst_month.Text = "0";
                }

                tc_lst_month.BorderStyle = BorderStyle.Solid;
                tc_lst_month.BorderWidth = 1;

                tc_lst_month.BackColor = System.Drawing.Color.White;

                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_month);

                //total

                tot_dr = "0";





                //dsDoc = sf.retail_lost_Purchase_value_stockist(drFF["Territory_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate,distributor_code);
                dsDoc = ad.View_total_Retailercount(divcode, drFF["Sf_Code"].ToString(), Date);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                TableCell tc_lst_monthfgrr = new TableCell();
                HyperLink hyp_lst_monthfgrr = new HyperLink();

                if (tot_dr != "0")
                {
                    if (tot_dr == "")
                    {
                        tot_dr = "0";
                    }
                    hyp_lst_monthfgrr.Text = tot_dr;
                    iTotLstCount += Convert.ToInt32(tot_dr);
                    //sURL = "rptroutedeviationretview.aspx?Division_code=" + divcode + "&sf_code=" + drFF["Sf_Code"].ToString() + "&date=" + Date + "";
                    //hyp_lst_monthfg.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                    //hyp_lst_monthfg.NavigateUrl = "#";

                }

                else
                {
                    hyp_lst_monthfgrr.Text = "0";
                }

                tc_lst_monthfgrr.BorderStyle = BorderStyle.Solid;
                tc_lst_monthfgrr.BorderWidth = 1;

                tc_lst_monthfgrr.BackColor = System.Drawing.Color.White;

                tc_lst_monthfgrr.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_monthfgrr.VerticalAlign = VerticalAlign.Middle;
                tc_lst_monthfgrr.Controls.Add(hyp_lst_monthfgrr);
                tc_lst_monthfgrr.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_monthfgrr);


                tot_dr = "0";





                //dsDoc = sf.retail_lost_Purchase_value_stockist(drFF["Territory_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate,distributor_code);
                dsDoc = ad.View_Retailercount(divcode, drFF["Sf_Code"].ToString(), Date);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                TableCell tc_lst_monthfg = new TableCell();
                HyperLink hyp_lst_monthfg = new HyperLink();

                if (tot_dr != "0")
                {
                    hyp_lst_monthfg.Text = tot_dr;
                    iTotLstCount1 += Convert.ToInt32(tot_dr);
                    sURL = "rptroutedeviationretview.aspx?Division_code=" + divcode + "&sf_code=" + drFF["Sf_Code"].ToString() + "&date=" + Date + "";
                    hyp_lst_monthfg.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                    hyp_lst_monthfg.NavigateUrl = "#";








                }

                else
                {
                    hyp_lst_monthfg.Text = "0";
                }

                tc_lst_monthfg.BorderStyle = BorderStyle.Solid;
                tc_lst_monthfg.BorderWidth = 1;

                tc_lst_monthfg.BackColor = System.Drawing.Color.White;

                tc_lst_monthfg.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_monthfg.VerticalAlign = VerticalAlign.Middle;
                tc_lst_monthfg.Controls.Add(hyp_lst_monthfg);
                tc_lst_monthfg.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_monthfg);




                tot_dr = "0";



                dsDoc = ad.View_Non_Retailercount(divcode, drFF["Sf_Code"].ToString(), Date);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                TableCell tc_lst_monthf = new TableCell();
                HyperLink hyp_lst_monthf = new HyperLink();

                if (tot_dr != "0")
                {
                    if (tot_dr == "")
                    {
                        tot_dr = "0";
                    }
                    hyp_lst_monthf.Text = tot_dr;
                    iTotLstCount2 += Convert.ToInt32(tot_dr);
                    sURL = "rptroutedeviationnonretview.aspx?Division_code=" + divcode + "&sf_code=" + drFF["Sf_Code"].ToString() + "&date=" + Date + "";
                    hyp_lst_monthf.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                    hyp_lst_monthf.NavigateUrl = "#";








                }

                else
                {
                    hyp_lst_monthf.Text = "0";
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

                dsDoc = ad.Total_Retailervalue(divcode, drFF["Sf_Code"].ToString(), Date);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                TableCell tc_lst_monthfv = new TableCell();
                HyperLink hyp_lst_monthfv = new HyperLink();

                if (tot_dr != "0")
                {
                    if (tot_dr == "")
                    {
                        tot_dr = "0.00";
                    }
                    hyp_lst_monthfv.Text = tot_dr;
                    iTotLstCount3 += Decimal.Parse(tot_dr.ToString());
                    //sURL = "rptroutedeviationnonretview.aspx?Division_code=" + divcode + "&sf_code=" + drFF["Sf_Code"].ToString() + "&date=" + Date + "";
                    //hyp_lst_monthfw.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                    //hyp_lst_monthfw.NavigateUrl = "#";

                }

                else
                {
                    hyp_lst_monthfv.Text = "0";
                }

                tc_lst_monthfv.BorderStyle = BorderStyle.Solid;
                tc_lst_monthfv.BorderWidth = 1;

                tc_lst_monthfv.BackColor = System.Drawing.Color.White;

                tc_lst_monthfv.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_monthfv.VerticalAlign = VerticalAlign.Middle;
                tc_lst_monthfv.Controls.Add(hyp_lst_monthfv);

                tc_lst_monthfv.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_monthfv);




                tot_dr = "0";

                dsDoc = ad.Total_RetailerNetWgt(divcode, drFF["Sf_Code"].ToString(), Date);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                TableCell tc_lst_monthfw = new TableCell();
                HyperLink hyp_lst_monthfw = new HyperLink();

                if (tot_dr != "0")
                {
                    if (tot_dr == "")
                    {
                        tot_dr = "0.00";
                    }
                    hyp_lst_monthfw.Text = tot_dr;
                    iTotLstCount4 += Decimal.Parse(tot_dr);
                    //iTotLstCount1 += Convert.ToInt32(tot_dr);
                    //sURL = "rptroutedeviationnonretview.aspx?Division_code=" + divcode + "&sf_code=" + drFF["Sf_Code"].ToString() + "&date=" + Date + "";
                    //hyp_lst_monthfw.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";
                    //hyp_lst_monthfw.NavigateUrl = "#";

                }

                else
                {
                    hyp_lst_monthfw.Text = "0";
                }

                tc_lst_monthfw.BorderStyle = BorderStyle.Solid;
                tc_lst_monthfw.BorderWidth = 1;

                tc_lst_monthfw.BackColor = System.Drawing.Color.White;

                tc_lst_monthfw.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_monthfw.VerticalAlign = VerticalAlign.Middle;
                tc_lst_monthfw.Controls.Add(hyp_lst_monthfw);
                tc_lst_monthfw.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_lst_monthfw);


                tot_dr = "0";


                string JsonStr = string.Empty;
                dsLocs = ad.get_traveldistance_callreport(divcode, drFF["Sf_Code"].ToString(), Date);

                JsonStr = "[";
                int iflag = 0;
                foreach (DataRow dr in dsLocs.Tables[0].Rows)
                {
                    if (dr["lati"].ToString() != string.Empty)
                    {
                        if (iflag != 0) JsonStr += ",";
                        iflag = 1;
                        JsonStr += "{\"lat\":\"" + dr["lati"].ToString() + "\",\"lng\":\"" + dr["long"].ToString() + "\",\"tm\":\"" + dr["time"].ToString() + "\"}";
                    }
                }

                JsonStr += "]";

                TableCell travel_distance = new TableCell();


                if (JsonStr != "0")
                {

                    travel_distance.Attributes.Add("class", "distDisp");

                    tr_det.Attributes.Add("data-locs", JsonStr);



                }



                travel_distance.BorderStyle = BorderStyle.Solid;
                travel_distance.BorderWidth = 1;

                travel_distance.BackColor = System.Drawing.Color.White;

                travel_distance.HorizontalAlign = HorizontalAlign.Center;
                travel_distance.VerticalAlign = VerticalAlign.Middle;


                tr_det.Cells.Add(travel_distance);
                tbl.Rows.Add(tr_det);

            }

            TableRow tr_total = new TableRow();

            TableCell tc_Count_Total = new TableCell();
            tc_Count_Total.BorderStyle = BorderStyle.Solid;
            tc_Count_Total.BorderWidth = 1;

            Literal lit_Count_Total = new Literal();
            lit_Count_Total.Text = "Total";
            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_Count_Total.Controls.Add(lit_Count_Total);
            tc_Count_Total.Font.Bold.ToString();
            tc_Count_Total.BackColor = System.Drawing.Color.White;
            tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
            tc_Count_Total.ColumnSpan = 5;
            tc_Count_Total.Style.Add("text-align", "center");
            tc_Count_Total.Style.Add("font-family", "Calibri");
            tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
            tc_Count_Total.Style.Add("font-size", "10pt");

            tr_total.Cells.Add(tc_Count_Total);


            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;

            //tot_retailer

            TableCell tc_tot_month = new TableCell();
            HyperLink hyp_month = new HyperLink();
            //int iTotLstCount = 0;

            hyp_month.Text = iTotLstCount.ToString("0");


            tc_tot_month.BorderStyle = BorderStyle.Solid;
            tc_tot_month.BorderWidth = 1;
            tc_tot_month.BackColor = System.Drawing.Color.White;
            tc_tot_month.Width = 200;
            tc_tot_month.Style.Add("font-family", "Calibri");
            tc_tot_month.Style.Add("font-size", "10pt");
            tc_tot_month.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_month.VerticalAlign = VerticalAlign.Middle;
            tc_tot_month.Controls.Add(hyp_month);
            tc_tot_month.Attributes.Add("style", "font-weight:bold;");
            tc_tot_month.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_month);

            //tot_visit
            TableCell tc_tot_montht = new TableCell();
            HyperLink hyp_montht = new HyperLink();
            //int iTotLstCount = 0;

            hyp_montht.Text = iTotLstCount1.ToString("0");


            tc_tot_montht.BorderStyle = BorderStyle.Solid;
            tc_tot_montht.BorderWidth = 1;
            tc_tot_montht.BackColor = System.Drawing.Color.White;
            tc_tot_montht.Width = 200;
            tc_tot_montht.Style.Add("font-family", "Calibri");
            tc_tot_montht.Style.Add("font-size", "10pt");
            tc_tot_montht.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_montht.VerticalAlign = VerticalAlign.Middle;
            tc_tot_montht.Controls.Add(hyp_montht);
            tc_tot_montht.Attributes.Add("style", "font-weight:bold;");
            tc_tot_montht.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_montht);

            ////tot_Non_visit
            TableCell tc_tot_monthnt = new TableCell();
            HyperLink hyp_monthnt = new HyperLink();
            //int iTotLstCount = 0;

            hyp_monthnt.Text = iTotLstCount2.ToString("0");


            tc_tot_monthnt.BorderStyle = BorderStyle.Solid;
            tc_tot_monthnt.BorderWidth = 1;
            tc_tot_monthnt.BackColor = System.Drawing.Color.White;
            tc_tot_monthnt.Width = 200;
            tc_tot_monthnt.Style.Add("font-family", "Calibri");
            tc_tot_monthnt.Style.Add("font-size", "10pt");
            tc_tot_monthnt.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_monthnt.VerticalAlign = VerticalAlign.Middle;
            tc_tot_monthnt.Controls.Add(hyp_monthnt);
            tc_tot_monthnt.Attributes.Add("style", "font-weight:bold;");
            tc_tot_monthnt.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_monthnt);


            ////tot_Val
            TableCell tc_tot_monthntv = new TableCell();
            HyperLink hyp_monthntv = new HyperLink();
            //int iTotLstCount = 0;

            hyp_monthntv.Text = iTotLstCount3.ToString("0");


            tc_tot_monthntv.BorderStyle = BorderStyle.Solid;
            tc_tot_monthntv.BorderWidth = 1;
            tc_tot_monthntv.BackColor = System.Drawing.Color.White;
            tc_tot_monthntv.Width = 200;
            tc_tot_monthntv.Style.Add("font-family", "Calibri");
            tc_tot_monthntv.Style.Add("font-size", "10pt");
            tc_tot_monthntv.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_monthntv.VerticalAlign = VerticalAlign.Middle;
            tc_tot_monthntv.Controls.Add(hyp_monthntv);
            tc_tot_monthntv.Attributes.Add("style", "font-weight:bold;");
            tc_tot_monthntv.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_monthntv);


            ////tot_Net_wgt
            TableCell tc_tot_monthntw = new TableCell();
            HyperLink hyp_monthntw = new HyperLink();
            //int iTotLstCount = 0;

            hyp_monthntw.Text = iTotLstCount4.ToString("0");


            tc_tot_monthntw.BorderStyle = BorderStyle.Solid;
            tc_tot_monthntw.BorderWidth = 1;
            tc_tot_monthntw.BackColor = System.Drawing.Color.White;
            tc_tot_monthntw.Width = 200;
            tc_tot_monthntw.Style.Add("font-family", "Calibri");
            tc_tot_monthntw.Style.Add("font-size", "10pt");
            tc_tot_monthntw.HorizontalAlign = HorizontalAlign.Right;
            tc_tot_monthntw.VerticalAlign = VerticalAlign.Middle;
            tc_tot_monthntw.Controls.Add(hyp_monthntw);
            tc_tot_monthntw.Attributes.Add("style", "font-weight:bold;");
            tc_tot_monthntw.Attributes.Add("Class", "rptCellBorder");
            tr_total.Cells.Add(tc_tot_monthntw);
            TableCell tot_did_tra = new TableCell();
            tr_total.Cells.Add(tot_did_tra);

            tbl.Rows.Add(tr_total);




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