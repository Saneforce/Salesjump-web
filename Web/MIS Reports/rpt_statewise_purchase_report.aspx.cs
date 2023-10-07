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

public partial class MIS_Reports_rpt_statewise_purchase_report : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
	string sfType = string.Empty;
    string divcode = string.Empty;
    Decimal iTotLstCount1 = 0;
    Decimal iTotLstCount = 0;   
    Decimal iTotLstCount2 = 0;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
  	string subdivision=string.Empty;
    string stype = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;  
    DateTime dtCurrent;    
    string tot_dr = string.Empty;   
    string Monthsub = string.Empty;    
    DataSet dsprd = new DataSet();    
    string sCurrentDate = string.Empty;
    DataSet dsState = null;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string[] statecd;
    DataSet dsDivision = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
		sfCode = Session["sf_code"].ToString();
        stype = Session["sf_type"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        subdivision= Request.QueryString["subdivision"].ToString();
		sfCode = Session["sf_code"].ToString();
        sfType = Session["sf_type"].ToString();
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Purchase Register -State Wise for  " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;


        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
       
        if (stype == "3")
        {
            FillSalesForce();
        }
        else
        {
            FillFO();
        }

    }

 	private void FillFO()
    {

        Division dv = new Division();
        dsDivision = dv.getStatePerSF(sfCode);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsSalesForce = st.getState_new(state_cd);

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {

                ViewState["dsSalesForce"] = dsSalesForce;
            }

            FillSF();
        }
    }

    private void FillSalesForce()
    {
              
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(divcode);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsSalesForce = st.getState_new(state_cd);

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                
                ViewState["dsSalesForce"] = dsSalesForce;
            }

            FillSF();
        }
    }
    private void FillSF()
    {
        string sURL = string.Empty;
        string stURL = string.Empty;
       string stCrtDtaPnt = string.Empty;
       string data = string.Empty;
        tbl.Rows.Clear();
       

        SalesForce sf = new SalesForce();    


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

           

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;
            tc_DR_Name.RowSpan = 1;
			
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "State Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
			tc_DR_Name.Style.Add("text-align", "center");
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
            lit_DR_Des1.Text = "Total";
            tc_DR_Des1.BorderColor = System.Drawing.Color.Black;
            tc_DR_Des1.Attributes.Add("Class", "rptCellBorder");
			tc_DR_Des1.Style.Add("text-align","center");
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
//            string iTotLstCount = "0";
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;
                //strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text =  iCount.ToString() ;
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
                lit_det_usr.Text = "&nbsp;" + drFF["State_code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["statename"].ToString();
				stCrtDtaPnt+="{label:\""+ drFF["statename"].ToString()+"\",y:";
				data+="{label:\""+ drFF["statename"].ToString()+"\",y:";
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                //hq

                //lit_det_FF.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["Stockist_Code"] + "', '" + drFF["Stockist_Name"].ToString() + "', '" + cyear + "', '" + cmonth + "','" + sCurrentDate + "','1')");
                //lit_det_FF.NavigateUrl = "#rptPurchas_Register_Category_wise.aspx"; //SF Designation Short Name
                sURL = "rptareawise_purchase_report.aspx?State_Code=" + drFF["State_Code"] + "&State_name=" + drFF["statename"].ToString() + " &FYear=" + FYear + "&FMonth=" + FMonth + " &TYear=" + TYear + "&TMonth=" + TMonth + "&subdivision=" + subdivision + "&sCurrentDate=" + sCurrentDate + "";
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


                      //  dsDoc = sf.Statewise_purchase_value(drFF["State_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate ,subdivision);
					//	if (sfType == "2" || sfType == "1")
                      //  {
                              dsDoc = sf.Statewise_purchase_value(drFF["State_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate, subdivision, sfCode);
                       // }
                       // else
                       // {
                       //     dsDoc = sf.Statewise_purchase_value(drFF["State_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate, subdivision);
                      //  }


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        TableCell tc_lst_month = new TableCell();
                        HyperLink hyp_lst_month = new HyperLink();

                        if (tot_dr != "0")
                        {
                            try
                            {
                                Decimal per = Math.Round(Convert.ToDecimal(tot_dr), 2);
                                hyp_lst_month.Text = per.ToString("0.00");
                            }
                            catch
                            {
                                hyp_lst_month.Text = tot_dr ==""? tot_dr:Convert.ToDecimal(tot_dr).ToString("0.00");
                            }
                            if (tot_dr != "")
                            {

                                iTotLstCount1 += (Decimal.Parse(tot_dr));
                                iTotLstCount2 += (Decimal.Parse(tot_dr));
                            }

                             stype = "1";
                            sURL = "rpt_statewise_product_detail.aspx?State_Code=" + drFF["State_Code"] + "&State_name=" + drFF["statename"].ToString() + " &CYear=" + cyear + "&CMonth=" + cmonth + "&subdivision=" + subdivision +"&stype=" + stype + "";
                            hyp_lst_month.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                            hyp_lst_month.NavigateUrl = "#";

                        


                        }

                        else
                        {
                            hyp_lst_month.Text = "0.00";
                        }

                        tc_lst_month.BorderStyle = BorderStyle.Solid;
                        tc_lst_month.BorderWidth = 1;
                        //hyp_lst_month.Text = Convert.ToString(iTotLstCount);
                        tc_lst_month.BackColor = System.Drawing.Color.White;
                        tc_lst_month.Width = 200;
                        tc_lst_month.HorizontalAlign = HorizontalAlign.Right;
                        tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                        tc_lst_month.Controls.Add(hyp_lst_month);
                        tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_lst_month);


                        //iTotLstCount = 0;

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
                        Decimal per1 = Math.Round(iTotLstCount1,2);
                        hyp_lst_month12.Text = per1.ToString("0.00");
                        //hyp_lst_month12.Text = Convert.ToString(iTotLstCount1);
                        stCrtDtaPnt+=Convert.ToString(iTotLstCount1) + "},";
                        decimal hh=(iTotLstCount1+200000);
                        data+=Convert.ToString(hh) + "},";
                        tc_tot_month12.HorizontalAlign = HorizontalAlign.Right;
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
                        hyp_lst_month12.Text = "0.00";
stCrtDtaPnt+= "0},";
data+= "0},";
                        tc_tot_month12.HorizontalAlign = HorizontalAlign.Right;
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
            TableRow tr_total = new TableRow();

            TableCell tc_Count_Total = new TableCell();
            tc_Count_Total.BorderStyle = BorderStyle.Solid;
            tc_Count_Total.BorderWidth = 1;
            //tc_catg_Total.Width = 25;
            HyperLink lit_Count_Total = new HyperLink();
            lit_Count_Total.Text = "Total";
            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_Count_Total.Controls.Add(lit_Count_Total);
            tc_Count_Total.Font.Bold.ToString();
            tc_Count_Total.BackColor = System.Drawing.Color.White;
            tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
            tc_Count_Total.ColumnSpan = 2;
            tc_Count_Total.Style.Add("text-align", "");
            tc_Count_Total.Style.Add("font-family", "Calibri");
            tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
            tc_Count_Total.Style.Add("font-size", "10pt");

            tr_total.Cells.Add(tc_Count_Total);


            
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

                    TableCell tc_tot_month = new TableCell();
                    HyperLink hyp_month = new HyperLink();                  
                    foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                    {

                       // dsDoc = sf.Statewise_purchase_value(drFF["State_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate,subdivision);
					//	if (sfType == "2" || sfType == "1")
                      //  {
                           // dsDoc = sf.Statewise_purchase_value(drFF["State_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate, subdivision, sfCode);
                     //   }
                      //  else
                      //  {
                       //     dsDoc = sf.Statewise_purchase_value(drFF["State_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate, subdivision);
                      //  }

						   
						   dsDoc = sf.Statewise_purchase_value(drFF["State_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate, subdivision, sfCode);
                           if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        if (tot_dr != "")
                        {
                            iTotLstCount += (Decimal.Parse(tot_dr));
                            try
                            {
                                Decimal per3 = Math.Round(iTotLstCount, 2);
                                hyp_month.Text = per3.ToString("0.00");
                            }
                            catch
                            {
                                hyp_month.Text = iTotLstCount.ToString("0.00");
                            }
                            stype = "2";
                            sURL = "rpt_statewise_product_detail.aspx?State_Code=" + drFF["State_Code"] + "&State_name=" + drFF["statename"].ToString() + " &CYear=" + cyear + "&CMonth=" + cmonth + "&subdivision=" + subdivision + "&stype=" + stype + "";
                            hyp_month.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                            hyp_month.NavigateUrl = "#";



                        }
                    }
                

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
                    iTotLstCount = 0;
                   // iTotLstCount2 = 0;
                    //Session["Test"] = "G";
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }

                }
             
                TableCell tc_tot_monthq = new TableCell();
                HyperLink hyp_monthq = new HyperLink();
                Decimal per4 = Math.Round(iTotLstCount2, 2);
                hyp_monthq.Text = per4.ToString("0.00");
                //hyp_monthq.Text = iTotLstCount2.ToString();
                tc_tot_monthq.BorderStyle = BorderStyle.Solid;
                tc_tot_monthq.BorderWidth = 1;
                tc_tot_monthq.BackColor = System.Drawing.Color.White;
                tc_tot_monthq.Width = 200;
                tc_tot_monthq.Style.Add("font-family", "Calibri");
                tc_tot_monthq.Style.Add("font-size", "10pt");
                tc_tot_monthq.HorizontalAlign = HorizontalAlign.Right;
                tc_tot_monthq.VerticalAlign = VerticalAlign.Middle;
                tc_tot_monthq.Controls.Add(hyp_monthq);
                tc_tot_monthq.Attributes.Add("style", "font-weight:bold;");
                tc_tot_monthq.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_tot_monthq);

            }

            tbl.Rows.Add(tr_total);
  string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
         ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);




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