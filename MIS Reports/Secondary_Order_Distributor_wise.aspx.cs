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
using iTextSharp.tool.xml;
using System.Text;
using Bus_EReport;
using System.Net;

public partial class MIS_Reports_Secondary_Order_Distributor_wise : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sf_type = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    decimal iTotLstCount1 = 0;
     decimal iTotLstCount = 0;  
    decimal iTotLstCount2 = 0;   
    string sMode = string.Empty;
    string FMonth = string.Empty;
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
	string Subdivision = string.Empty;
    string MultiSf_Code = string.Empty;
    DataSet dsprd = new DataSet();
    string MultiProd_Code = string.Empty;
    string Multi_Prod = string.Empty;
    string sCurrentDate = string.Empty;
    string sec_value = string.Empty;
    string sec_total = string.Empty;
    decimal iTotLstnetval = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfCode = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        divcode = Session["div_code"].ToString();
       
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
		Subdivision = Request.QueryString["Subdivision"].ToString();

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Secondary_Order_Distributor Wise for  " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;


       
        if (sf_type == "1")
        {
            FillSF_MR();
        }
        else if (sf_type == "2")
        {
            FillSF();
        }
        else if (sf_type == "3")
        {
            FillSF();
        }

    }
    private void FillSF_MR()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;
        string stCrtDtaPnt = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.GetDistNamewise_MR(divcode, Subdivision, sfCode);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#496a9a");
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

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 1;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>Distributor Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Distributor Name</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);








            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);


            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            SalesForce sal = new SalesForce();


            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = 1;
                    Literal lit_month = new Literal();
                    Monthsub = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    lit_month.Text = Monthsub.Substring(0, 3) + "-" + cyear;
                    tc_month.Attributes.Add("Class", "rptCellBorder");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
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

            TableCell tc_DR_Des1 = new TableCell();
            tc_DR_Des1.BorderStyle = BorderStyle.Solid;
            tc_DR_Des1.BorderWidth = 1;
            tc_DR_Des1.Width = 80;
            tc_DR_Des1.RowSpan = 1;
            Literal lit_DR_Des1 = new Literal();
            lit_DR_Des1.Text = "<center>Total</center>";
            tc_DR_Des1.BorderColor = System.Drawing.Color.Black;
            tc_DR_Des1.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Des1.Controls.Add(lit_DR_Des1);
            tr_header.Cells.Add(tc_DR_Des1);
            tbl.Rows.Add(tr_header);
            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());



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
                lit_det_usr.Text = "&nbsp;" + drFF["Stockist_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 250;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                stCrtDtaPnt += "{label:\"" + drFF["Stockist_Name"].ToString() + "\",y:";
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                sURL = "Secondary_Order_Route_wise.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + " &TYear=" + TYear + "&TMonth=" + TMonth + "&sCurrentDate=" + sCurrentDate + "";
                lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                lit_det_FF.NavigateUrl = "#";


                months = Convert.ToInt16(ViewState["months"].ToString());
                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                cyear = Convert.ToInt16(ViewState["cyear"].ToString());


                if (months >= 0)
                {

                    for (int j = 1; j <= months + 1; j++)
                    {

                        if (cmonth == 12)
                        {
                            sCurrentDate = "01-01-" + (cyear + 1);
                        }

                        else
                        {
                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                        }

                        dtCurrent = Convert.ToDateTime(sCurrentDate);


                        dsDoc = sf.Route_Distributor_value(drFF["Stockist_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        TableCell tc_lst_month = new TableCell();
                        HyperLink hyp_lst_month = new HyperLink();

                        if (tot_dr != "0")
                        {
                            hyp_lst_month.Text = tot_dr;
                            if (tot_dr != "")
                            {

                                iTotLstCount1 += (decimal.Parse(tot_dr));
                                iTotLstCount2 += (decimal.Parse(tot_dr));
                            }




                            stURL = "Secondary_Order_Distributor_wise_view.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_name=" + drFF["Stockist_Name"].ToString() + " &Year=" + cyear + "&Month=" + cmonth + " &sCurrentDate=" + sCurrentDate + "";
                            hyp_lst_month.Attributes["onclick"] = "javascript:window.open('" + stURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                            hyp_lst_month.NavigateUrl = "#";


                        }

                        else
                        {
                            hyp_lst_month.Text = "";
                        }

                        tc_lst_month.BorderStyle = BorderStyle.Solid;
                        tc_lst_month.BorderWidth = 1;

                        tc_lst_month.BackColor = System.Drawing.Color.White;
                        tc_lst_month.Width = 200;
                        tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                        tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                        tc_lst_month.Controls.Add(hyp_lst_month);
                        tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_lst_month);




                        tot_dr = "0";

                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }


                    }
                    if (iTotLstCount1 != 0)
                    {
                        TableCell tc_tot_month12 = new TableCell();
                        HyperLink hyp_lst_month12 = new HyperLink();
                        tc_tot_month12.BorderStyle = BorderStyle.Solid;
                        tc_tot_month12.BorderWidth = 1;
                        tc_tot_month12.BackColor = System.Drawing.Color.White;
                        tc_tot_month12.Width = 200;
                        tc_tot_month12.Style.Add("font-family", "Calibri");
                        hyp_lst_month12.Text = Convert.ToString(iTotLstCount1);
                        stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
                        tc_tot_month12.HorizontalAlign = HorizontalAlign.Center;
                        tc_tot_month12.VerticalAlign = VerticalAlign.Middle;
                        tc_tot_month12.Controls.Add(hyp_lst_month12);
                        tc_tot_month12.Attributes.Add("style", "font-weight:bold;");
                        tc_tot_month12.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_tot_month12);

                    }
                    else
                    {
                        TableCell tc_tot_month12 = new TableCell();
                        HyperLink hyp_lst_month12 = new HyperLink();
                        tc_tot_month12.BorderStyle = BorderStyle.Solid;
                        tc_tot_month12.BorderWidth = 1;
                        tc_tot_month12.BackColor = System.Drawing.Color.White;
                        tc_tot_month12.Width = 200;
                        tc_tot_month12.Style.Add("font-family", "Calibri");
                        hyp_lst_month12.Text = "";
                        stCrtDtaPnt += "0},";
                        tc_tot_month12.HorizontalAlign = HorizontalAlign.Center;
                        tc_tot_month12.VerticalAlign = VerticalAlign.Middle;
                        tc_tot_month12.Controls.Add(hyp_lst_month12);
                        tc_tot_month12.Attributes.Add("style", "font-weight:bold;");
                        tc_tot_month12.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_tot_month12);

                    }
                    iTotLstCount1 = 0;
                }

                tbl.Rows.Add(tr_det);

            }

        }
        string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);

    }

  

    private void FillSF()
    {
        Random rndm = new Random();
        string t = "1";
        int mnth = Convert.ToInt32(FMonth), year = Convert.ToInt32(FYear);
        DateTime dt = Convert.ToDateTime(year + "-" + mnth.ToString() + "-" + t.ToString());
        string fromdate = dt.ToString("yyyy-MM-dd 00:00:00.000");
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd 23:59:59.999");
        string stCrtDtaPnt = string.Empty;
        string sURL = string.Empty;
        string stURL = string.Empty;

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
         if (sf_type == "1" || sf_type == "2")
        {
            dsSalesForce = sf.GetStockName_Customer(divcode, Subdivision,sfCode);
        }
        else
        {
            dsSalesForce = sf.GetStockName_Customer(divcode, Subdivision);
        }
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#496a9a");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 2;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);



            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 250;
            tc_DR_Name.RowSpan = 2;
			tc_DR_Name.Style.Add("text-align","center");
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Stockist Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);


            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;



            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = 2;
                    tc_month.RowSpan = 1;
                    Literal lit_month = new Literal();
                    Monthsub = sf.getMonthName(cmonth.ToString()) + "-" + cyear;

                    lit_month.Text = Monthsub.Substring(0, 3) + "-" + cyear;
                    string month = lit_month.Text;



                    tc_month.Attributes.Add("Class", "rptCellBorder");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);


                    //tbl.Rows.Add(tr_catg); 

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;

                    }

                }


            }
            tbl.Rows.Add(tr_header);
            TableRow tr_catg = new TableRow();
            tr_catg.BorderStyle = BorderStyle.Solid;
            tr_catg.BorderWidth = 1;
            tr_catg.BackColor = System.Drawing.Color.FromName("#496a9a");
            tr_catg.Style.Add("Color", "White");
            tr_catg.BorderColor = System.Drawing.Color.Black;
            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {

                    TableCell tc_DR_Codee = new TableCell();
                    tc_DR_Codee.BorderStyle = BorderStyle.Solid;
                    tc_DR_Codee.BorderWidth = 1;
                    tc_DR_Codee.Width = 200;
                    //tc_DR_Code.RowSpan = 1;
                    Literal lit_DR_Codee = new Literal();
                    lit_DR_Codee.Text = "Value";
                    tc_DR_Codee.Controls.Add(lit_DR_Codee);
                    tc_DR_Codee.Attributes.Add("Class", "rptCellBorder");
					tc_DR_Codee.Style.Add("text-align","center");
                    tc_DR_Codee.BorderColor = System.Drawing.Color.Black;
                    //tc_DR_Code.Visible = false;
                    tr_catg.Cells.Add(tc_DR_Codee);


                    TableCell tc_DR_value1 = new TableCell();
                    tc_DR_value1.BorderStyle = BorderStyle.Solid;
                    tc_DR_value1.BorderWidth = 1;
                    tc_DR_value1.Width = 150;

                    Literal lit_DR_value1 = new Literal();
                    lit_DR_value1.Text = "Net Weight Value";
                    tc_DR_value1.BorderColor = System.Drawing.Color.Black;
                    tc_DR_value1.Attributes.Add("Class", "rptCellBorder");
					tc_DR_value1.Style.Add("text-align","center");

                    tc_DR_value1.Controls.Add(lit_DR_value1);
                    tr_catg.Cells.Add(tc_DR_value1);

                   

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;

                    }

                }

                tbl.Rows.Add(tr_catg);
            }





            SalesForce sal = new SalesForce();








            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            int iCount = 0;
            //string iTotLstCount ="0";

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text =  iCount.ToString();
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
				tc_det_SNo.Style.Add("text-align","right");
                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();

                lit_det_usr.Text = "&nbsp;" + drFF["Stockist_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                TableCell tc_det_FF = new TableCell();
                tc_det_FF.Width = 250;
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                stCrtDtaPnt += "{label:\"" + drFF["Stockist_Name"].ToString() + "\",y:";
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);


                sURL = "Secondary_Order_Route_wise.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_name=" + drFF["Stockist_Name"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + " &TYear=" + TYear + "&TMonth=" + TMonth + "&subdivision=" + Subdivision + "";
                lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                lit_det_FF.NavigateUrl = "#";

                months = Convert.ToInt16(ViewState["months"].ToString());
                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                cyear = Convert.ToInt16(ViewState["cyear"].ToString());


                if (months >= 0)
                {

                    for (int j = 1; j <= months + 1; j++)
                    {

                        if (cmonth == 12)
                        {
                            sCurrentDate = "01-01-" + (cyear + 1);
                        }

                        else
                        {
                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                        }

                        dtCurrent = Convert.ToDateTime(sCurrentDate);
                        //TableCell tc_det_last6monthsum = new TableCell();
                        //tc_det_last6monthsum.Width = 200;

                        //tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Center;
                        //Literal lit_det_sum = new Literal();
                        ////lit_det_FF.Text = "&nbsp;" + drFF["Stockist_Name"].ToString();
                        //tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                        //tc_det_last6monthsum.BorderWidth = 1;
                        //tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                        //tc_det_last6monthsum.Controls.Add(lit_det_sum);
                        //tr_det.Cells.Add(tc_det_last6monthsum);
                        Order pf = new Order();

                        dsDoc = sf.Route_Distributor_value(drFF["Stockist_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate);


                            TableCell tc_lst_month = new TableCell();
                            HyperLink hyp_lst_month = new HyperLink();
                        if (dsDoc.Tables[0].Rows.Count > 0)
                        {
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            sec_value = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

}
    

                            if (tot_dr != "0")
                            {
                                hyp_lst_month.Text = tot_dr =="" ?tot_dr.ToString():Convert.ToDecimal(tot_dr).ToString("0.00");
                                if (tot_dr != "")
                                {

                                    iTotLstCount1 += (decimal.Parse(tot_dr));
                                    iTotLstCount2 += (decimal.Parse(tot_dr));
                                }




                                stURL = "Sec_od_distweekwise.aspx?Stockist_Code=" + drFF["Stockist_Code"] + "&Stockist_name=" + drFF["Stockist_Name"].ToString() + " &Year=" + cyear + "&Month=" + cmonth + " &subdivision=" + Subdivision + "";
                                hyp_lst_month.Attributes["onclick"] = "javascript:window.open('" + stURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                                hyp_lst_month.NavigateUrl = "#";


                            }
                            else
                            {
                                hyp_lst_month.Text = "";
                            }

                            tc_lst_month.BorderStyle = BorderStyle.Solid;
                            tc_lst_month.BorderWidth = 1;
                            tc_lst_month.BackColor = System.Drawing.Color.White;
                            tc_lst_month.Width = 150;
                            tc_lst_month.HorizontalAlign = HorizontalAlign.Right;
                            tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                            tc_lst_month.Controls.Add(hyp_lst_month);
                            tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                            tr_det.Cells.Add(tc_lst_month);


                            tot_dr = "0";


                        TableCell tc_lst_month1 = new TableCell();
                        Literal hyp_lst_month1 = new Literal();
                        tc_lst_month1.Width = 150;



                        if (sec_value != "0")
                        {
                            hyp_lst_month1.Text = sec_value =="" ? sec_value.ToString(): Convert.ToDecimal(sec_value).ToString("0.00");
							//hyp_lst_month1.Text =sec_value;
                        }
                        else
                        {
                            hyp_lst_month1.Text = "";
                        }
                        tc_lst_month1.BorderStyle = BorderStyle.Solid;

                        tc_lst_month1.BorderStyle = BorderStyle.Solid;
                        tc_lst_month1.BorderWidth = 1;
                        tc_lst_month1.BackColor = System.Drawing.Color.White;
                        tc_lst_month1.Width = 200;
                        tc_lst_month1.HorizontalAlign = HorizontalAlign.Right;
                        tc_lst_month1.VerticalAlign = VerticalAlign.Middle;
                        tc_lst_month1.Controls.Add(hyp_lst_month1);
                        tc_lst_month1.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_lst_month1);

                    //val =tot_dr) / (tot_value);

                        tbl.Rows.Add(tr_det);
                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }

                        tot_dr = "0";
                        sec_value = "0";
                        if (iTotLstCount1 != 0)
                        {
                            
                            //hyp_lst_month12.Text = Convert.ToString(iTotLstCount1);
                            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
                          

                        }
                        else
                        {
                        
                            stCrtDtaPnt += "0},";
                         

                        }
                        iTotLstCount1 = 0;

                    }









                }


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
            tc_Count_Total.ColumnSpan = 2;
            tc_Count_Total.Style.Add("text-align", "center");
            tc_Count_Total.Style.Add("font-family", "Calibri");
            tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
            tc_Count_Total.Style.Add("font-size", "10pt");

            tr_total.Cells.Add(tc_Count_Total);




            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());

            if (months >= 0)
            {
                //Session["Test"] = "T";
                for (int j = 1; j <= months + 1; j++)
                {
                    Monthsub = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    if (cmonth == 12)
                    {
                        sCurrentDate = "01-01-" + (cyear + 1);
                    }
                    else
                    {
                        sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                    }
                    stCrtDtaPnt += "{label:\"" + Convert.ToString(Monthsub) + "\",y:";
                    dtCurrent = Convert.ToDateTime(sCurrentDate);

                    TableCell tc_tot_month = new TableCell();
                    tc_tot_month.ColumnSpan = 1;
                    HyperLink hyp_month = new HyperLink();
                    //int iTotLstCount = 0;
                    foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                    {


                        Order gg = new Order();
                        dsDoc = sf.Route_Distributor_value(drFF["Stockist_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                        {
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            sec_total = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                            if (sec_total != "")
                            {
                                iTotLstnetval += (Decimal.Parse(sec_total));
                            }

                            if (tot_dr != "")
                            {
                                iTotLstCount += (Decimal.Parse(tot_dr));
                                try
                                {
                                    Decimal per1 = Math.Round(Convert.ToDecimal(iTotLstCount.ToString()), 2);
                                    hyp_month.Text = per1.ToString("0.00");
                                }
                                catch
                                {
                                    hyp_month.Text = "&nbsp&nbsp&nbsp&nbsp&nbsp" + iTotLstCount.ToString("0.00");
                                }




                            }
                           
                        }
                    }
                    stCrtDtaPnt += Convert.ToString(iTotLstCount) + "},";

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
                    TableCell tc_tot_monthd = new TableCell();
                    HyperLink hyp_monthd = new HyperLink();
                    try
                    {
                        Decimal per1 = Math.Round(Convert.ToDecimal(iTotLstnetval.ToString()), 2);
                        hyp_monthd.Text = per1.ToString("0.00");
                    }
                    catch
                    {
                        hyp_monthd.Text = "&nbsp&nbsp&nbsp&nbsp&nbsp" + iTotLstnetval.ToString("0.00");
                    }
                    tc_tot_monthd.BorderStyle = BorderStyle.Solid;
                    tc_tot_monthd.BorderWidth = 1;
                    tc_tot_monthd.BackColor = System.Drawing.Color.White;
                    tc_tot_monthd.Width = 200;
                    tc_tot_monthd.Style.Add("font-family", "Calibri");
                    tc_tot_monthd.Style.Add("font-size", "10pt");
                    tc_tot_monthd.HorizontalAlign = HorizontalAlign.Right;
                    tc_tot_monthd.VerticalAlign = VerticalAlign.Middle;
                    tc_tot_monthd.Controls.Add(hyp_monthd);
                    tc_tot_monthd.Attributes.Add("style", "font-weight:bold;");
                    tc_tot_monthd.Attributes.Add("Class", "rptCellBorder");
                    tr_total.Cells.Add(tc_tot_monthd);
                    iTotLstnetval = 0;

                    iTotLstCount = 0;

                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                    

                }
               


            }

            tbl.Rows.Add(tr_total);

          


        }
        string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);


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

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}