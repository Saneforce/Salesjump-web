﻿using System;
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

public partial class MIS_Reports_Secondary_Order_Route_wise : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iTotLstCount1 = 0;
    int iTotLstCount = 0;
    //int iTotLstCount1 = 0;
    int iTotLstCount2 = 0;
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
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;   
    DateTime dtCurrent;   
    string tot_Sub_days = string.Empty;
    string tot_dr = string.Empty;
    string tot_Drr = string.Empty;  
       
    string tot_dcr_dr = string.Empty;   
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;  
    DataSet dsprd = new DataSet();   
    string sCurrentDate = string.Empty;
    string stockist_code = string.Empty;
    string stURL = string.Empty;
    string Stock_name = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
       
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        stockist_code=Request.QueryString["Stockist_Code"].ToString();
        Stock_name = Request.QueryString["Stockist_name"]; 
       
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Secondary_Order_Route_Wise for  " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;

        dist.Text = Stock_name;
        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
        FillSF();

    }

    private void FillSF()
    {
        string sURL = string.Empty;
string stCrtDtaPnt = string.Empty;

        tbl.Rows.Clear();
        

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.GetRouteName_Customer(stockist_code,divcode);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
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

            //TableCell tc_DR_Code = new TableCell();
            //tc_DR_Code.BorderStyle = BorderStyle.Solid;
            //tc_DR_Code.BorderWidth = 1;
            //tc_DR_Code.Width = 400;
            //tc_DR_Code.RowSpan = 1;
            //Literal lit_DR_Code = new Literal();
            //lit_DR_Code.Text = "<center>Category Code</center>";
            //tc_DR_Code.Controls.Add(lit_DR_Code);
            //tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            //tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Code.Visible = false;
            //tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Route Name</center>";
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
            //dsprd = sal.getProduct_Exp(divcode);

            //if (dsprd.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow drFF in dsprd.Tables[0].Rows)
            //    {
            //        Multi_Prod += "" + drFF["Product_Code_SlNo"].ToString() + "" + ",";                 

            //    }
            //}

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
                //strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";


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

             
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Territory_Code"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                HyperLink lit_det_FF = new HyperLink();
                lit_det_FF.Text = "&nbsp;" + drFF["Territory_Name"].ToString();
                stCrtDtaPnt += "{label:\"" + drFF["Territory_Name"].ToString() + "\",y:";
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                //hq

                //lit_det_FF.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["Product_Cat_Code"] + "', '" + drFF["Product_Cat_Name"].ToString() + "', '" + cyear + "', '" + cmonth + "','" + sCurrentDate + "','1')");
                //lit_det_FF.NavigateUrl = "#";


                stURL = "Secondary_Order_Retailer_wise.aspx?Stockist_Code=" + stockist_code + "&Stockist_name=" + Stock_name + " &cat_Code=" + drFF["Territory_Code"] + " &cat_name=" + drFF["Territory_Name"].ToString() + "&FYear=" + FYear + "&FMonth=" + FMonth + " &TYear=" + TYear + "&TMonth=" + TMonth + " &sCurrentDate=" + sCurrentDate + "";
                lit_det_FF.Attributes["onclick"] = "javascript:window.open('" + stURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                lit_det_FF.NavigateUrl = "#";//S
                //SF Designation Short Name
               


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
                        //dsSalesForce = sf.sp_UserList_getMR_Doc_List(divcode, sfCode);

                        dsDoc = sf.Route_wise_sale_value_stockist(drFF["Territory_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate, stockist_code);


                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        TableCell tc_lst_month = new TableCell();
                        HyperLink hyp_lst_month = new HyperLink();
                        
                        if (tot_dr != "0")
                        {
                            hyp_lst_month.Text = tot_dr;
                            if (tot_dr != "")
                            {

                                iTotLstCount1 +=(int.Parse(tot_dr));
                                iTotLstCount2 += (int.Parse(tot_dr));
                            }
                            stURL = "Secondary_Order_Route_wise_view.aspx?Stockist_Code=" + stockist_code + "&Stockist_name=" + Stock_name + " &cat_Code=" + drFF["Territory_Code"] + " &cat_name=" + drFF["Territory_Name"].ToString() + "&Year=" + cyear + "&Month=" + cmonth + " &sCurrentDate=" + sCurrentDate + "";
                            hyp_lst_month.Attributes["onclick"] = "javascript:window.open('" + stURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0,zoom=50%');";


                            hyp_lst_month.NavigateUrl = "#";
                            //hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["Product_Cat_Code"] + "', '" + drFF["Product_Cat_Name"].ToString() + "', '" + cyear + "', '" + cmonth + "','" + sCurrentDate + "','2')");
                     
                            ////hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp('" + sURL + "')";
                            //hyp_lst_month.NavigateUrl = "#";

                        }

                        else
                        {
                            hyp_lst_month.Text = "";
                        }

                        tc_lst_month.BorderStyle = BorderStyle.Solid;
                        tc_lst_month.BorderWidth = 1;
                        //hyp_lst_month.Text = Convert.ToString(iTotLstCount);
                        tc_lst_month.BackColor = System.Drawing.Color.White;
                        tc_lst_month.Width = 200;
                        tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
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
                        hyp_lst_month12.Text = Convert.ToString(iTotLstCount1);
                     stCrtDtaPnt+=Convert.ToString(iTotLstCount1) + "},";
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
                            stCrtDtaPnt+= "0},";
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
            TableRow tr_total = new TableRow();

            TableCell tc_Count_Total = new TableCell();
            tc_Count_Total.BorderStyle = BorderStyle.Solid;
            tc_Count_Total.BorderWidth = 1;
            //tc_catg_Total.Width = 25;
            Literal lit_Count_Total = new Literal();
            lit_Count_Total.Text = "<center>Total</center>";
            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_Count_Total.Controls.Add(lit_Count_Total);
            tc_Count_Total.Font.Bold.ToString();
            tc_Count_Total.BackColor = System.Drawing.Color.White;
            tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
            tc_Count_Total.ColumnSpan = 2;
            tc_Count_Total.Style.Add("text-align", "left");
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
                    //int iTotLstCount = 0;
                    foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                    {

                        //dsDoc = sf.getProduct_Exp_count(drFF["sf_code"].ToString(), divcode, cmonth, cyear, Convert.ToInt16(Prod), sCurrentDate);

                        dsDoc = sf.Route_wise_sale_value_stockist(drFF["Territory_Code"].ToString(), divcode, cmonth, cyear, sCurrentDate, stockist_code);
                        if (dsDoc.Tables[0].Rows.Count > 0)
                            tot_Drr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        if (tot_Drr != "")
                        {
                            iTotLstCount += (int.Parse(tot_Drr));
                            hyp_month.Text = iTotLstCount.ToString();
                            tot_Drr = "0";
                            //hyp_month.Attributes.Add("href", "javascript:showModalPopUp('" + 0 + "', '" + drFF["sf_name"] + "', '" + cyear + "', '" + cmonth + "', '" + Prod_Name + "', '" + Prod + "','" + sCurrentDate + "')");
                        }
                    }
                    
                    
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
                    //Session["Test"] = "G";
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                    //iTotLstCount = 0;
                    //tot_dr = "0";
                }
                TableCell tc_tot_monthq = new TableCell();
                HyperLink hyp_monthq = new HyperLink();
                hyp_monthq.Text = iTotLstCount2.ToString();
                tc_tot_monthq.BorderStyle = BorderStyle.Solid;
                tc_tot_monthq.BorderWidth = 1;
                tc_tot_monthq.BackColor = System.Drawing.Color.White;
                tc_tot_monthq.Width = 200;
                tc_tot_monthq.Style.Add("font-family", "Calibri");
                tc_tot_monthq.Style.Add("font-size", "10pt");
                tc_tot_monthq.HorizontalAlign = HorizontalAlign.Center;
                tc_tot_monthq.VerticalAlign = VerticalAlign.Middle;
                tc_tot_monthq.Controls.Add(hyp_monthq);
                tc_tot_monthq.Attributes.Add("style", "font-weight:bold;");
                tc_tot_monthq.Attributes.Add("Class", "rptCellBorder");
                tr_total.Cells.Add(tc_tot_monthq);
              
            }
  string scrpt = "arr=[" + stCrtDtaPnt + "];window.onload = function () {genChart(arr);}";
         ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);



            tbl.Rows.Add(tr_total);


        }
        //Session["MultiProd_Code"] = Multi_Prod.Remove(Multi_Prod.Length - 1);
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

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}