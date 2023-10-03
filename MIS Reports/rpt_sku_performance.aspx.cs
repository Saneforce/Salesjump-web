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



public partial class MIS_Reports_rpt_sku_performance : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    decimal iTotLstCount2 = 0;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string stype = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;

    DataSet dsSalesForce = new DataSet();

    DataSet dsMGR = new DataSet();
    DataSet dsMr = new DataSet();
    DataSet dsDoc = null;
    DateTime dtCurrent;
    string tot_dr = string.Empty;
    string tot_Drr = string.Empty;

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
        sfCode = Session["SF_code"].ToString();
        //sfCode = Request.QueryString["FMonth"].ToString();
        //FYear = Request.QueryString["FYear"].ToString();
        //TMonth = Request.QueryString["TMonth"].ToString();
        //TYear = Request.QueryString["TYear"].ToString();
        //stockist_code = Request.QueryString["Stockist_Code"].ToString();
        //Stock_name = Request.QueryString["Stockist_name"];
        //System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        //string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);
        FillSF();
    }
    private void FillSF()
    {
        Dgv_SKU.DataSource = null;
        Dgv_SKU.DataBind();
        Int64 twdays = 24;
        Int64 tot_twdays = 0;
        decimal tot_actdays = 0;
        decimal Target_Productive_Calls_Day = 0;
        decimal Target_Calls_day = 0;
        decimal tot_target_calls = 0;
        decimal tot_target_pro_calls = 0;
        decimal Act_call = 0;
        //decimal Call_Rate = 0;
        decimal Act_Pro_Call = 0;
        decimal New_Doors=0;
        decimal Rep_Pur=0;
        decimal tot_convertion = 0;
        decimal tot_con = 0;
        decimal Tot_vol = 0;
        decimal Tot_val = 0;
        decimal TVALUES_val=0;
        decimal TVALUES_vol = 0;

        AdminSetup ams = new AdminSetup();

        //        DataSet dsadmin = ams.Get_Target_Names(divcode);

        DataSet dsAccessmas = ams.Get_target_setting_Values(divcode.TrimEnd(','));
        if (dsAccessmas.Tables[0].Rows.Count > 0)
        {
        
            DataRow[] tar_pro_cal = dsAccessmas.Tables[0].Select("id=3");
            if (tar_pro_cal.Length > 0)
            {
                DataTable dt = tar_pro_cal.CopyToDataTable();
                Target_Productive_Calls_Day = Convert.ToDecimal(dt.Rows[0][1]);
            }
            DataRow[] tar_cal = dsAccessmas.Tables[0].Select("id=4");
            if (tar_cal.Length > 0)
            {
                DataTable dt = tar_cal.CopyToDataTable();
                Target_Calls_day = Convert.ToDecimal(dt.Rows[0][1]);
            }
        }





        SalesForce SF = new SalesForce();
        dsMGR = SF.SalesForceListMgrGet_MgrOnly(divcode, sfCode);
        dsMr = SF.Get_all_Mr(divcode.TrimEnd(','));
        DataSet dsWorkType = SF.Get_WorkType(divcode.TrimEnd(','));


        DCR dcr = new DCR();
        DataSet dsDcr = dcr.Get_DCR_Count(divcode);

        Order od = new Order();
        DataSet sdOder = od.Get_Order_Count(divcode);

        ListedDR lr = new ListedDR();
        DataSet dsDr = lr.Get_lstdr_Count(divcode);

        DataSet order_vol = od.Get_Total_Vol(divcode);

        DataSet order_val = od.Get_Total_Val(divcode);

        DataSet TVols = od.Get_TVolume(divcode);
        if (TVols.Tables[0].Rows.Count > 0)
        {
            TVALUES_vol = Convert.ToDecimal(TVols.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim());
        }

        DataSet TVALUES = od.Get_TVALUES(divcode);
        if (TVALUES.Tables[0].Rows.Count > 0)
        {
            TVALUES_val = Convert.ToDecimal(TVALUES.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim());
        }



        DataTable dsData = new DataTable();
        dsData.Columns.Add("SF Code", typeof(string));
        dsData.Columns.Add("SF Name", typeof(string));
        dsData.Columns.Add("Target Working Days", typeof(Int64));
        dsData.Columns.Add("Actual  Working Days", typeof(Int64));
        dsData.Columns.Add("% Actual vs Target Working Days", typeof(string));
        dsData.Columns.Add("Target Calls", typeof(string));
        dsData.Columns.Add("Target Productive Calls", typeof(string));
        dsData.Columns.Add("Actual Calls", typeof(string));
        dsData.Columns.Add("Call Rate", typeof(string));
        dsData.Columns.Add("Actual Productive Calls", typeof(string));
        dsData.Columns.Add("Conversion Rate", typeof(string));
        dsData.Columns.Add("New Doors", typeof(string));
        dsData.Columns.Add("Repeat Purchase", typeof(string));
        dsData.Columns.Add("Repeat Purchase Rate", typeof(string));
        dsData.Columns.Add("Total Volume (strings)", typeof(string));
        dsData.Columns.Add("Target Vol Achivemnt", typeof(string));
        dsData.Columns.Add("Total value", typeof(string));
        dsData.Columns.Add("Value Achivement", typeof(string));
        dsData.Columns.Add("Average Daily Sales", typeof(string));
        dsData.Columns.Add("Average Drop size", typeof(string));
        dsData.Columns.Add("Average Daily Productive Calls", typeof(string));
       
        foreach (DataRow mgrow in dsMGR.Tables[0].Rows)
        {
            tot_twdays = 0;
            tot_actdays = 0;
            tot_target_calls = 0;
            tot_target_pro_calls = 0;
            Act_call = 0;
            //Call_Rate = 0;
            Act_Pro_Call = 0;
            New_Doors=0;
            Rep_Pur=0;
            tot_convertion = 0;
            tot_con = 0;
            Tot_vol = 0;
            Tot_val = 0;
            DataRow[] drow = dsMr.Tables[0].Select("Reporting_To_SF = '" + mgrow["SF_Code"].ToString() + "'");
            if (drow.Length > 0)
            {
                foreach (DataRow row in drow)
                {

                    decimal awd = 0;
                    decimal dcr_count = 0;
                    decimal order_count = 0;
                    decimal lrdr_count = 0;
                    decimal rep_count = 0;
                    decimal tot_vol = 0;
                    decimal tot_val = 0;
                    DataRow[] wrow = null;
                    if (dsWorkType.Tables[0].Rows.Count > 0)
                    {
                        wrow = dsWorkType.Tables[0].Select("sf_code ='" + row["SF_Code"].ToString() + "'");
                    }

                    if (wrow.Length > 0)
                    {
                        foreach (DataRow r in wrow)
                        {
                            awd = Convert.ToDecimal(r["count_date"]);
                            tot_actdays += awd;
                        }
                    }

                    else
                    {
                        awd = 0;
                        tot_actdays += awd;
                    }

                    DataRow[] dcrrow = null;
                    if (dsDcr.Tables[0].Rows.Count > 0)
                    {
                        dcrrow = dsDcr.Tables[0].Select("sf_code ='" + row["SF_Code"].ToString() + "'");
                    }


                    if (dcrrow.Length > 0)
                    {
                        foreach (DataRow r in dcrrow)
                        {
                            dcr_count = Convert.ToDecimal(r["dcr_count"]);                        
                        }
                    }

                    else
                    {
                        dcr_count = 0;                        
                    }



                    DataRow[] orderrow = null;
                    if (sdOder.Tables[0].Rows.Count > 0)
                    {
                        orderrow = sdOder.Tables[0].Select("sf_code ='" + row["SF_Code"].ToString() + "'");
                    }


                    if (orderrow.Length > 0)
                    {
                        foreach (DataRow r in orderrow)
                        {
                            order_count = Convert.ToDecimal(r["ec"]);
                            rep_count = Convert.ToDecimal(r["rc"]);
                        }
                    }

                    else
                    {
                        order_count = 0;
                        rep_count=0;
                    }


                    DataRow[] lrdrrow = null;
                    if (dsDr.Tables[0].Rows.Count > 0)
                    {
                        lrdrrow = dsDr.Tables[0].Select("sf_code ='" + row["SF_Code"].ToString() + "'");
                    }


                    if (lrdrrow.Length > 0)
                    {
                        foreach (DataRow r in lrdrrow)
                        {
                            lrdr_count = Convert.ToDecimal(r["lrdr_count"]);
                        }
                    }

                    else
                    {
                        lrdr_count = 0;
                    }

                    DataRow[] tot_volrow = null;
                    if (order_vol.Tables[0].Rows.Count > 0)
                    {
                        tot_volrow = order_vol.Tables[0].Select("sf_code ='" + row["SF_Code"].ToString() + "'");
                    }


                    if (tot_volrow.Length > 0)
                    {
                        foreach (DataRow r in tot_volrow)
                        {
                            tot_vol = Convert.ToDecimal(r["net"]);
                        }
                    }

                    else
                    {
                        tot_vol = 0;
                    }

                    //Total value

                    DataRow[] tot_valuerow = null;
                    if (order_val.Tables[0].Rows.Count > 0)
                    {
                        tot_valuerow = order_val.Tables[0].Select("sf_code ='" + row["SF_Code"].ToString() + "'");
                    }


                    if (tot_valuerow.Length > 0)
                    {
                        foreach (DataRow r in tot_valuerow)
                        {
                            tot_val = Convert.ToDecimal(r["val"]);
                        }
                    }

                    else
                    {
                        tot_val = 0;
                    }





                    decimal tot_pro_cal = Target_Productive_Calls_Day * twdays;
                    decimal tot_cal = Target_Calls_day * twdays;
                    decimal Rep_pur_rate = 0;
                    decimal Avg_day_sale = 0;
                    decimal Avg_drop_size = 0;
                    decimal Avg_day_pro_call = 0;
                    decimal val_Ach = tot_val / (tot_cal * TVALUES_val);
                    decimal Tar_Vol_Ach = tot_vol / (tot_cal * TVALUES_vol);

                    tot_target_calls += tot_cal;
                    tot_target_pro_calls += tot_pro_cal;
                    Act_call += dcr_count;
                    Act_Pro_Call += order_count;
                    New_Doors +=lrdr_count;
                    Rep_Pur +=rep_count;
                    
                    if (dcr_count > 0)
                    {
                        tot_convertion = order_count / dcr_count;
                    }
                    else
                    {
                        tot_convertion = 0;
                    }
                    tot_con += tot_convertion;

                    if (rep_count > 0)
                    {
                        Rep_pur_rate = rep_count / order_count;
                    }

                    if (tot_val > 0)
                    {
                        Avg_day_sale = tot_val / awd;
                    }

                    if (tot_val > 0)
                    {
                        Avg_drop_size = tot_val / order_count;
                    }

                    if (order_count > 0)
                    {
                        Avg_day_pro_call = order_count / awd;
                    }

                    dsData.Rows.Add(row["SF_Code"].ToString(), row["Sf_Name"].ToString(), twdays, awd, (awd / twdays * 100).ToString("0") + '%', tot_pro_cal, tot_cal, dcr_count, (dcr_count / tot_pro_cal * 100).ToString("0") + "%", order_count, (tot_convertion * 100).ToString("0") + "%", lrdr_count, rep_count, (Rep_pur_rate * 100).ToString("0") + "%", tot_vol, (Tar_Vol_Ach * 100).ToString("0") + "%", tot_val, (val_Ach * 100).ToString("0") + "%", (Avg_day_sale).ToString("0"), (Avg_drop_size).ToString("0"), (Avg_day_pro_call).ToString("0"));
                    tot_twdays += twdays;
                }
                dsData.Rows.Add("", "SUB TOTAL", tot_twdays, tot_actdays, (tot_actdays / tot_twdays * 100).ToString("0") + '%', tot_target_pro_calls, tot_target_calls, Act_call, (Act_call / tot_target_pro_calls * 100).ToString("0") + "%", Act_Pro_Call, (tot_con * 100).ToString("0") + "%", New_Doors, Rep_Pur);
            }
        }

        Dgv_SKU.DataSource = dsData;
        Dgv_SKU.DataBind();
    }


    protected void Dgv_SKU_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridViewRow grv = e.Row;
            if (grv.Cells[1].Text.Equals("SUB TOTAL"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#99FFFF");
            }
        }
        catch (Exception ex)
        { }

    }
}