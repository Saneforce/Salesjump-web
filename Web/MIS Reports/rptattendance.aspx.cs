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
using iTextSharp.tool.xml;
using System.Net;
using System.Text.RegularExpressions;



public partial class MIS_Reports_rptattendance : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string sf_type = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty; 
    string type = string.Empty;
    string h = string.Empty;
  string wrktypename = string.Empty;
    int sum_time = 0;
    DataSet dsSalesForce = new DataSet();
    DataSet dsdatee = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    TimeSpan ff;
    int rowspan = 0;
    string sCurrentDate = string.Empty;
    string endTime = string.Empty;
    string startTime = string.Empty;
    string tot_dr = string.Empty;
    string tot_value = string.Empty;
    string con_qty = string.Empty;
    string ec = string.Empty;
    string Monthsub = string.Empty;   
    string date = string.Empty;
    string endd = string.Empty;
    DataSet dsDoctor = new DataSet();
    string gg = string.Empty;
string imagepath=string.Empty;
    int quantity2 = 0;
    protected void Page_Load(object sender, EventArgs e)
    
    
{
        divcode = Session["div_code"].ToString();

        sfname = Request.QueryString["sfname"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
       imagepath = Request.QueryString["imgpath"].ToString();

        logoo.ImageUrl = imagepath;
        //sfCode = "Admin";

        if (sfCode.Contains("MGR"))
        {
            sf_type = "2"; 
        }
        else if (sfCode.Contains("MR"))
        {
            sf_type = "1";
        }
        else
        {
            sf_type = "0";
        }
        type = Request.QueryString["type"].ToString();
       
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        lblHead.Text = "Attendance View for the Month of " + strFMonthName + " " + FYear;
       

        lblsf_name.Text = sfname;
        if (type == "Minimised")
        {
            Fillminisedview();
        }
        else
        {
            Fillmaximisedview();
        }
      
    }

    private void Fillminisedview()
    {
       
        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Hierarchy_attendance(divcode, sfCode);
        //dsSalesForce = sf.daily_Trans_Order_uu(divcode, sfCode, date,subdivision);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#819dfb");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;


            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
          
            tc_SNo.Style.Add("text-align", "center");
            tr_header.Cells.Add(tc_SNo);



            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;
            tc_DR_Name.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Fieldforce Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
           
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_Name_pot = new TableCell();
            tc_DR_Name_pot.BorderStyle = BorderStyle.Solid;
            tc_DR_Name_pot.BorderWidth = 1;
            tc_DR_Name_pot.Width = 100;
            tc_DR_Name_pot.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Name_pot = new Literal();
            lit_DR_Name_pot.Text = "HQ";
            tc_DR_Name_pot.BorderColor = System.Drawing.Color.Black;
       
            tc_DR_Name_pot.Controls.Add(lit_DR_Name_pot);
            tr_header.Cells.Add(tc_DR_Name_pot);



            TableCell activity = new TableCell();
            activity.BorderStyle = BorderStyle.Solid;
            activity.BorderWidth = 1;
            activity.Width = 100;
            activity.HorizontalAlign = HorizontalAlign.Center;
            Literal activitylit = new Literal();
            activitylit.Text = "Desig";
            activity.BorderColor = System.Drawing.Color.Black;
          
            activity.Controls.Add(activitylit);
            tr_header.Cells.Add(activity);




           

            TableCell tc_DR_Name_pott = new TableCell();
            tc_DR_Name_pott.BorderStyle = BorderStyle.Solid;
            tc_DR_Name_pott.BorderWidth = 1;
            tc_DR_Name_pott.Width = 100;
            
            tc_DR_Name_pott.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Name_pott = new Literal();
            lit_DR_Name_pott.Text = "Calls";
            tc_DR_Name_pott.BorderColor = System.Drawing.Color.Black;
        
            tc_DR_Name_pott.Controls.Add(lit_DR_Name_pott);
            tr_header.Cells.Add(tc_DR_Name_pott);


            


            Notice nt = new Notice();
            dsdatee = nt.GetDate_daywise(FMonth, FYear, divcode);
            foreach (DataRow drFF1 in dsdatee.Tables[0].Rows)
            {
                TableCell tc_DR_Name_pott1 = new TableCell();
                tc_DR_Name_pott1.BorderStyle = BorderStyle.Solid;
                tc_DR_Name_pott1.BorderWidth = 1;
                tc_DR_Name_pott1.Width = 250;

                tc_DR_Name_pott1.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_DR_Name_pott1 = new Literal();
                lit_DR_Name_pott1.Text = drFF1["DayPart"].ToString();
                tc_DR_Name_pott1.BorderColor = System.Drawing.Color.Black;

                tc_DR_Name_pott1.Controls.Add(lit_DR_Name_pott1);
                tr_header.Cells.Add(tc_DR_Name_pott1);
            }
           
          
         




            tbl.Rows.Add(tr_header);







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
                lit_det_SNo.Text = iCount.ToString();
                tc_det_SNo.RowSpan = 3;
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
            
                tc_det_SNo.HorizontalAlign = HorizontalAlign.Right;
                //tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code

                TableCell tc_det_currentmonth = new TableCell();
                tc_det_currentmonth.RowSpan = 3;
                tc_det_currentmonth.Width = 200;
                tc_det_currentmonth.HorizontalAlign = HorizontalAlign.Left;
                tc_det_currentmonth.VerticalAlign = VerticalAlign.Middle;
              
                HyperLink lit_det_mon = new HyperLink();

                lit_det_mon.Text = drFF["Sf_Name"].ToString();
               

                //tc_det_currentmonth.BorderWidth = 1;
                //tc_det_currentmonth.Attributes.Add("Class", "rptCellBorder");
                tc_det_currentmonth.Controls.Add(lit_det_mon);
                tr_det.Cells.Add(tc_det_currentmonth);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.RowSpan = 3;
                tc_det_FF.HorizontalAlign = HorizontalAlign.Left;
                tc_det_FF.Width = 300;
                Literal address = new Literal();
                address.Text = "&nbsp;" + drFF["sf_hq"].ToString();            
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_currentmonth.Height = 60;
                //tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(address);
                tr_det.Cells.Add(tc_det_FF);


                TableCell tc_det_usr = new TableCell();
                tc_det_usr.RowSpan = 3;
                tc_det_usr.Attributes.Add("style", "color:Blue;");
                Literal retailname = new Literal();
                tc_det_usr.HorizontalAlign = HorizontalAlign.Center;
                retailname.Text = drFF["Designation_Short_Name"].ToString();          
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;            
                //tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(retailname);
                tr_det.Cells.Add(tc_det_usr);


                TableCell tc_DR_Name_product_name = new TableCell();
               
                tc_DR_Name_product_name.RowSpan = 1;
                            
                Literal lit_DR_Name_pot_name = new Literal();
                lit_DR_Name_pot_name.Text = "AT";              
                tc_DR_Name_product_name.Controls.Add(lit_DR_Name_pot_name);
                tr_det.Cells.Add(tc_DR_Name_product_name);
               

                TableRow hh = new TableRow();

                TableCell tc_det_FF_milk = new TableCell();
                tc_det_FF_milk.RowSpan = 1;
                
                //tc_det_FF_milk.HorizontalAlign = HorizontalAlign.Right;
                //tc_det_FF_milk.Attributes.Add("style", "color:Blue;");
               
                Literal lit_det_FF_milk = new Literal();
                lit_det_FF_milk.Text = "TC";
                //tc_det_FF_milk.BorderStyle = BorderStyle.Solid;
                //tc_det_FF_milk.BorderWidth = 1;
                //tc_det_FF_milk.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF_milk.Controls.Add(lit_det_FF_milk);
                hh.Cells.Add(tc_det_FF_milk);
              TableRow hh1 = new TableRow();
                TableCell tc_DR_Name_product_wrktype = new TableCell();

                tc_DR_Name_product_wrktype.RowSpan = 1;

                Literal lit_DR_Name_wrktype = new Literal();
                lit_DR_Name_wrktype.Text = "EC";
                tc_DR_Name_product_wrktype.Controls.Add(lit_DR_Name_wrktype);
                hh1.Cells.Add(tc_DR_Name_product_wrktype);
                dsdatee = nt.GetDate_daywise(FMonth, FYear,divcode);
              
                foreach (DataRow drFF1 in dsdatee.Tables[0].Rows)
                {
                    string check = string.Empty;
                    dsDoc = nt.Getvalue_daywise(drFF["Sf_Code"].ToString(), drFF1["DayPart"].ToString(), FYear, drFF1["monthe"].ToString(), divcode,drFF["Sf_Type"].ToString());
                      

  if (dsDoc.Tables[0].Rows.Count > 0)
                    {
                        wrktypename = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    }
                    else
                    {
                        wrktypename = "";
                    }
                    TableCell tc_DR_Name_value_wrktype = new TableCell();

                    tc_DR_Name_value_wrktype.RowSpan = 1;

                    Literal lit_DR_value_wrktype = new Literal();
                    lit_DR_value_wrktype.Text = wrktypename;
                    tc_DR_Name_value_wrktype.Controls.Add(lit_DR_value_wrktype);
                    tr_det.Cells.Add(tc_DR_Name_value_wrktype);

               


                        TableCell tc_det_last6monthsum = new TableCell();
                        tc_det_last6monthsum.Width = 200;

                        tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Center;
                        Literal lit_det_sum = new Literal();
                        if (dsDoc.Tables[0].Rows.Count > 0)
                        {
                            con_qty = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                            bool isAllLetters = Regex.IsMatch(con_qty, @"^[a-zA-Z]+$");
                          check = Convert.ToString(isAllLetters);
                            if (check == "True") 
                            {
                                tc_det_last6monthsum.RowSpan = 2;  
                            }
                            lit_det_sum.Text = con_qty;



                        }
                        else
                        {
                            lit_det_sum.Text = "";
                        }
                        tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                        tc_det_last6monthsum.BorderWidth = 1;
                        tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                        tc_det_last6monthsum.Controls.Add(lit_det_sum);
                        hh.Cells.Add(tc_det_last6monthsum);


                        if (check == "True")
                        {

                        }
                        else
                        {

                            TableCell tc_det_FF_val = new TableCell();
                            tc_det_FF_val.RowSpan = 1;
                            tc_det_FF_val.HorizontalAlign = HorizontalAlign.Center;

                            Literal tc_det_FF_vale = new Literal();

                            if (dsDoc.Tables[0].Rows.Count > 0)
                            {
                                ec = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();



                                tc_det_FF_vale.Text = ec;




                            }
                            else
                            {
                                tc_det_FF_vale.Text = "";
                            }
                            tc_det_FF_val.BorderStyle = BorderStyle.Solid;
                            tc_det_FF_val.BorderWidth = 1;
                            tc_det_FF_val.Attributes.Add("Class", "rptCellBorder");
                            tc_det_FF_val.Controls.Add(tc_det_FF_vale);
                            hh1.Cells.Add(tc_det_FF_val);

                        }
                }
                //int diff = quantity1 - quantity2;
                ////double case_value = (diff / (Convert.ToDouble(con_qty)));

                //dsDoc = sf.get_case_qty_order_vs_primary(diff, con_qty);
                //if (dsDoc.Tables[0].Rows.Count > 0)
                //{
                //    diff_case = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //}
                //else
                //{
                //    diff_case = "0";
                //}
             



               
                tbl.Rows.Add(tr_det);
                tbl.Rows.Add(hh);
           tbl.Rows.Add(hh1);
             
            }
        }
        else
        {
            //norecordfound.Visible = true;

        }
    }


    private void Fillmaximisedview()
    {

        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Hierarchy_attendance(divcode, sfCode);
        //dsSalesForce = sf.daily_Trans_Order_uu(divcode, sfCode, date,subdivision);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#819dfb");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;


            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "S.No";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);

            tc_SNo.Style.Add("text-align", "center");
            tr_header.Cells.Add(tc_SNo);



            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;
            tc_DR_Name.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "Fieldforce Name";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;

            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_Name_pot = new TableCell();
            tc_DR_Name_pot.BorderStyle = BorderStyle.Solid;
            tc_DR_Name_pot.BorderWidth = 1;
            tc_DR_Name_pot.Width = 100;
            tc_DR_Name_pot.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Name_pot = new Literal();
            lit_DR_Name_pot.Text = "HQ";
            tc_DR_Name_pot.BorderColor = System.Drawing.Color.Black;

            tc_DR_Name_pot.Controls.Add(lit_DR_Name_pot);
            tr_header.Cells.Add(tc_DR_Name_pot);



            TableCell activity = new TableCell();
            activity.BorderStyle = BorderStyle.Solid;
            activity.BorderWidth = 1;
            activity.Width = 100;
            activity.HorizontalAlign = HorizontalAlign.Center;
            Literal activitylit = new Literal();
            activitylit.Text = "Desig";
            activity.BorderColor = System.Drawing.Color.Black;

            activity.Controls.Add(activitylit);
            tr_header.Cells.Add(activity);






            TableCell tc_DR_Name_pott = new TableCell();
            tc_DR_Name_pott.BorderStyle = BorderStyle.Solid;
            tc_DR_Name_pott.BorderWidth = 1;
            tc_DR_Name_pott.Width = 100;

            tc_DR_Name_pott.HorizontalAlign = HorizontalAlign.Center;
            Literal lit_DR_Name_pott = new Literal();
            lit_DR_Name_pott.Text = "Calls";
            tc_DR_Name_pott.BorderColor = System.Drawing.Color.Black;

            tc_DR_Name_pott.Controls.Add(lit_DR_Name_pott);
            tr_header.Cells.Add(tc_DR_Name_pott);





            Notice nt = new Notice();
            dsdatee = nt.GetDate_daywise(FMonth,FYear,divcode);
            foreach (DataRow drFF1 in dsdatee.Tables[0].Rows)
            {
                TableCell tc_DR_Name_pott1 = new TableCell();
                tc_DR_Name_pott1.BorderStyle = BorderStyle.Solid;
                tc_DR_Name_pott1.BorderWidth = 1;
                tc_DR_Name_pott1.Width = 250;

                tc_DR_Name_pott1.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_DR_Name_pott1 = new Literal();
                lit_DR_Name_pott1.Text = drFF1["DayPart"].ToString();
                tc_DR_Name_pott1.BorderColor = System.Drawing.Color.Black;

                tc_DR_Name_pott1.Controls.Add(lit_DR_Name_pott1);
                tr_header.Cells.Add(tc_DR_Name_pott1);
            }







            tbl.Rows.Add(tr_header);







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
                lit_det_SNo.Text = iCount.ToString();
                tc_det_SNo.RowSpan = 4;
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;

                tc_det_SNo.HorizontalAlign = HorizontalAlign.Right;
                //tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code

                TableCell tc_det_currentmonth = new TableCell();
                tc_det_currentmonth.RowSpan = 4;
                tc_det_currentmonth.Width = 200;
                tc_det_currentmonth.HorizontalAlign = HorizontalAlign.Left;
                tc_det_currentmonth.VerticalAlign = VerticalAlign.Middle;

                HyperLink lit_det_mon = new HyperLink();

                lit_det_mon.Text = drFF["Sf_Name"].ToString();


                //tc_det_currentmonth.BorderWidth = 1;
                //tc_det_currentmonth.Attributes.Add("Class", "rptCellBorder");
                tc_det_currentmonth.Controls.Add(lit_det_mon);
                tr_det.Cells.Add(tc_det_currentmonth);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                tc_det_FF.RowSpan = 4;
                tc_det_FF.HorizontalAlign = HorizontalAlign.Left;
                tc_det_FF.Width = 300;
                Literal address = new Literal();
                address.Text = "&nbsp;" + drFF["sf_hq"].ToString();
                tc_det_FF.BorderStyle = BorderStyle.Solid;
                tc_det_FF.BorderWidth = 1;
                tc_det_currentmonth.Height = 60;
                //tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(address);
                tr_det.Cells.Add(tc_det_FF);


                TableCell tc_det_usr = new TableCell();
                tc_det_usr.RowSpan =4;
                tc_det_usr.Attributes.Add("style", "color:Blue;");
                Literal retailname = new Literal();
                tc_det_usr.HorizontalAlign = HorizontalAlign.Center;
                retailname.Text = drFF["Designation_Short_Name"].ToString();
                tc_det_usr.BorderStyle = BorderStyle.Solid;
                tc_det_usr.BorderWidth = 1;
                //tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(retailname);
                tr_det.Cells.Add(tc_det_usr);


                TableCell tc_DR_Name_product_name = new TableCell();

                tc_DR_Name_product_name.RowSpan = 1;

                Literal lit_DR_Name_pot_name = new Literal();
                lit_DR_Name_pot_name.Text = "TC";
                tc_DR_Name_product_name.Controls.Add(lit_DR_Name_pot_name);
                tr_det.Cells.Add(tc_DR_Name_product_name);


                TableRow hh = new TableRow();

                TableCell tc_det_FF_milk = new TableCell();
                tc_det_FF_milk.RowSpan = 1;         
                Literal lit_det_FF_milk = new Literal();
                lit_det_FF_milk.Text = "EC";              
                tc_det_FF_milk.Controls.Add(lit_det_FF_milk);
                hh.Cells.Add(tc_det_FF_milk);
                
                TableRow hh1 = new TableRow();

                TableCell tc_det_NW = new TableCell();
                tc_det_NW.RowSpan = 1;
                Literal lit_tc_det_NW = new Literal();
                lit_tc_det_NW.Text = "NW";
                tc_det_NW.Controls.Add(lit_tc_det_NW);
                hh1.Cells.Add(tc_det_NW);


                TableRow hh2 = new TableRow();
                TableCell tc_det_Val = new TableCell();
                tc_det_Val.RowSpan = 1;
                Literal lit_tc_det_Val = new Literal();
                lit_tc_det_Val.Text = "Value";
                tc_det_Val.Controls.Add(lit_tc_det_Val);
                hh2.Cells.Add(tc_det_Val);

                dsdatee = nt.GetDate_daywise(FMonth,FYear,divcode);

                foreach (DataRow drFF1 in dsdatee.Tables[0].Rows)
                {
                    if (drFF1["DayPart"].ToString() == "21")
                    {
                    }
                    string check1 = string.Empty;
                    dsDoc = nt.Getvalue_daywise_maximised(drFF["Sf_Code"].ToString(), drFF1["DayPart"].ToString(), FYear, drFF1["monthe"].ToString(), divcode,drFF["Sf_Type"].ToString());
                    TableCell tc_det_last6monthsum = new TableCell();
                    tc_det_last6monthsum.Width = 200;

                    tc_det_last6monthsum.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_sum = new Literal();
                    if (dsDoc.Tables[0].Rows.Count > 0)
                    {
                        con_qty = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                        bool isAllLetters = Regex.IsMatch(con_qty, @"^[a-zA-Z]+$");
                        check1 = Convert.ToString(isAllLetters);
                        if (check1 == "True")
                        {
                            tc_det_last6monthsum.RowSpan = 4;
                        }
                        lit_det_sum.Text = con_qty;



                    }
                    else
                    {
                        lit_det_sum.Text = "";
                    }
                    tc_det_last6monthsum.BorderStyle = BorderStyle.Solid;
                    tc_det_last6monthsum.BorderWidth = 1;
                    tc_det_last6monthsum.Attributes.Add("Class", "rptCellBorder");
                    tc_det_last6monthsum.Controls.Add(lit_det_sum);
                    tr_det.Cells.Add(tc_det_last6monthsum);


                    if (check1 == "True")
                    {

                    }
                    else
                    {

                        TableCell tc_det_FF_val = new TableCell();
                        tc_det_FF_val.RowSpan = 1;
                        tc_det_FF_val.HorizontalAlign = HorizontalAlign.Center;

                        Literal tc_det_FF_vale = new Literal();

                        if (dsDoc.Tables[0].Rows.Count > 0)
                        {
                            ec = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();



                            tc_det_FF_vale.Text = ec;




                        }
                        else
                        {
                            tc_det_FF_vale.Text = "";
                        }
                        tc_det_FF_val.BorderStyle = BorderStyle.Solid;
                        tc_det_FF_val.BorderWidth = 1;
                        tc_det_FF_val.Attributes.Add("Class", "rptCellBorder");
                        tc_det_FF_val.Controls.Add(tc_det_FF_vale);
                        hh.Cells.Add(tc_det_FF_val);

                        TableCell tc_det_FF_val1 = new TableCell();
                        tc_det_FF_val1.RowSpan = 1;
                        tc_det_FF_val1.HorizontalAlign = HorizontalAlign.Center;

                        Literal tc_det_FF_vale1 = new Literal();

                        if (dsDoc.Tables[0].Rows.Count > 0)
                        {
                            ec = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();



                            tc_det_FF_vale1.Text = ec;




                        }
                        else
                        {
                            tc_det_FF_vale1.Text = "";
                        }
                        tc_det_FF_val1.BorderStyle = BorderStyle.Solid;
                        tc_det_FF_val1.BorderWidth = 1;
                        tc_det_FF_val1.Attributes.Add("Class", "rptCellBorder");
                        tc_det_FF_val1.Controls.Add(tc_det_FF_vale1);
                        hh1.Cells.Add(tc_det_FF_val1);
                        TableCell tc_det_FF_val2 = new TableCell();
                        tc_det_FF_val2.RowSpan = 1;
                        tc_det_FF_val2.HorizontalAlign = HorizontalAlign.Center;

                        Literal tc_det_FF_vale2 = new Literal();

                        if (dsDoc.Tables[0].Rows.Count > 0)
                        {
                            ec = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();



                            tc_det_FF_vale2.Text = ec;




                        }
                        else
                        {
                            tc_det_FF_vale2.Text = "";
                        }
                        tc_det_FF_val2.BorderStyle = BorderStyle.Solid;
                        tc_det_FF_val2.BorderWidth = 1;
                        tc_det_FF_val2.Attributes.Add("Class", "rptCellBorder");
                        tc_det_FF_val2.Controls.Add(tc_det_FF_vale2);
                        hh2.Cells.Add(tc_det_FF_val2);

                    }
                }
                //int diff = quantity1 - quantity2;
                ////double case_value = (diff / (Convert.ToDouble(con_qty)));

                //dsDoc = sf.get_case_qty_order_vs_primary(diff, con_qty);
                //if (dsDoc.Tables[0].Rows.Count > 0)
                //{
                //    diff_case = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //}
                //else
                //{
                //    diff_case = "0";
                //}





                tbl.Rows.Add(tr_det);
                tbl.Rows.Add(hh);
                tbl.Rows.Add(hh1);
                tbl.Rows.Add(hh2);

            }
        }
        else
        {
            //norecordfound.Visible = true;

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
                Document pdfDoc = new Document(PageSize.A4,
                    10f, 10f, 10f, 0f);
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