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
using System.Data.SqlClient;



public partial class MIS_Reports_rpt_target_settings : System.Web.UI.Page
{
    #region "Declaration"
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
	 DataSet dsMGRtv = new DataSet();
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
    string sub_Div_Code = string.Empty;
    string Stock_name = string.Empty;
    string imagepath = string.Empty;
    string stcode = string.Empty;
    string stname = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        //  sfCode = Session["SF_code"].ToString();
        sfCode = Request.QueryString["SF_Code"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        stcode = Request.QueryString["StateCode"].ToString();
        stname = Request.QueryString["StateName"].ToString();
        sub_Div_Code = Request.QueryString["Sub_Div"].ToString();
        lblhead.Text = Request.QueryString["Month_Name"].ToString() + " - " + FYear;
        lblhead1.Text = "DSR Monitoring Report For " + stname + " For Month of <b>" + Request.QueryString["Month_Name"].ToString() + " - " + FYear + "</b>";
        lbl_SFname.Text = Request.QueryString["SF_Name"].ToString();
        string url = HttpContext.Current.Request.Url.AbsoluteUri.Replace("http://", "");
        string[] words = url.Split('.');
        string shortna = words[0];
        if (shortna == "www") shortna = words[1];
        if (Session["CmpIDKey"] != null && Session["CmpIDKey"].ToString() != "") { shortna = Session["CmpIDKey"].ToString(); }
        string filename = shortna + "_logo.png";
        string dynamicFolderPath = "../limg/";//which used to create                                       dynamic folder
        string path = dynamicFolderPath + filename.ToString();
        hyear.Value = FYear;
        hmonth.Value = FMonth;
        imagepath = path;
        logoo.ImageUrl = imagepath;
        FillSF();
    }
    private void FillSF()
    {
        Dgv_SKU.DataSource = null;
        Dgv_SKU.DataBind();
        decimal twdays = 0;
        decimal tot_twdays = 0;
        decimal tot_actdays = 0;
        decimal Target_Productive_Calls_Day = 0;
        decimal Target_Calls_day = 0;
        decimal Target_sold = 0;
        decimal target_drop_size = 0;
        decimal tot_target_calls = 0;
        decimal tot_target_pro_calls = 0;
        decimal Act_call = 0;
        //decimal Call_Rate = 0;
        decimal Act_Pro_Call = 0;
        decimal New_Doors = 0;
        decimal Rep_Pur = 0;
        decimal tot_convertion = 0;
        decimal tot_con = 0;
        decimal Tot_vol = 0;
        decimal Tot_val = 0;
        decimal TVALUES_val = 0;
        decimal TVALUES_vol = 0;



        decimal Grand_twd = 0;
        decimal Grand_awd = 0;
        decimal Grand_taper = 0;
        decimal Grand_tc = 0;
        decimal Grand_tpc = 0;
        decimal Grand_ac = 0;
        decimal Grand_cr = 0;
        decimal Grand_apc = 0;
        decimal Grand_conrte = 0;
        decimal Grand_nd = 0;
        decimal Grand_rp = 0;
        decimal Grand_rpr = 0;
        decimal Grand_vol = 0;
        decimal Grand_volach = 0;
        decimal Grand_val = 0;
        decimal Grand_talach = 0;
        decimal Grand_ads = 0;
        decimal Grand_adropsize = 0;
        decimal Grand_adp = 0;

        AdminSetup ams = new AdminSetup();

        //        DataSet dsadmin = ams.Get_Target_Names(divcode);

        DataSet dsAccessmas = ams.Get_target_setting_Values(divcode.TrimEnd(','), FYear,stcode, FMonth);
        if (dsAccessmas.Tables[0].Rows.Count > 0)
        {

            DataRow[] tar_pro_cal = dsAccessmas.Tables[0].Select("code='MT004'"); //Target Productive Calls Per Day
            if (tar_pro_cal.Length > 0)
            {
                DataTable dt = tar_pro_cal.CopyToDataTable();
                Target_Productive_Calls_Day = Convert.ToDecimal(dt.Rows[0][2]);
            }
            DataRow[] tar_cal = dsAccessmas.Tables[0].Select("code='MT003'"); //Target Calls Per Day
            if (tar_cal.Length > 0)
            {
                DataTable dt = tar_cal.CopyToDataTable();
                Target_Calls_day = Convert.ToDecimal(dt.Rows[0][2]);
            }

            DataRow[] tar_sold = dsAccessmas.Tables[0].Select("code='MT001'"); //Target String Sold Per Door
            if (tar_sold.Length > 0)
            {
                DataTable dt = tar_sold.CopyToDataTable();
                Target_sold = Convert.ToDecimal(dt.Rows[0][2]);
            }

            DataRow[] drop_size = dsAccessmas.Tables[0].Select("code='MT002'"); //Target String Sold Per Door
            if (drop_size.Length > 0)
            {
                DataTable dt = drop_size.CopyToDataTable();
                target_drop_size = Convert.ToDecimal(dt.Rows[0][2]);
            }


            DataRow[] tot_work_days = dsAccessmas.Tables[0].Select("code='MT006'"); //Target String Sold Per Door
            if (tot_work_days.Length > 0)
            {
                DataTable dt = tot_work_days.CopyToDataTable();
                twdays = Convert.ToDecimal(dt.Rows[0][2]);
            }


        }





        SalesForce SF = new SalesForce();
        dsMGR = SF.SalesForceListMgrGet_MgrOnly(divcode, sfCode, sub_Div_Code);
        //dsMGRtv = SF.dsrmon_targer_totv(divcode, sfCode, FMonth, FYear);
		SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec dsrtarget_setting_val '" + sfCode + "', '" + divcode + "','" + FMonth + "','" + FYear + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsMGRtv);
        con.Close();
        dsMr = SF.SalesForceList(divcode.TrimEnd(','), sfCode, sub_Div_Code);
       // dsMr = SF.UserList_getMR(divcode.TrimEnd(','), sfCode, sub_Div_Code);

        DataSet dsWorkType = SF.Get_WorkType(divcode.TrimEnd(','), FYear, FMonth);
        DCR dcr = new DCR();
        DataSet dsDcr = dcr.Get_DCR_Count(divcode, FYear, FMonth);

        Order od = new Order();
        DataSet sdOder = od.Get_Order_Count(divcode, FYear, FMonth);

        ListedDR lr = new ListedDR();
        DataSet dsDr = lr.Get_lstdr_Count(divcode, FYear, FMonth);

        DataSet order_vol = od.Get_Total_Vol(divcode, FYear, FMonth);

        DataSet order_val = od.Get_Total_Val(divcode, FYear, FMonth);

        //DataSet TVols = od.Get_TVolume(divcode);
        //if (TVols.Tables[0].Rows.Count > 0)
        //{
        //    TVALUES_vol = Convert.ToDecimal(TVols.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim());
        //}

        //DataSet TVALUES = od.Get_TVALUES(divcode);
        //if (TVALUES.Tables[0].Rows.Count > 0)
        //{
        //    TVALUES_val = Convert.ToDecimal(TVALUES.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim());
        //}



        DataTable dsData = new DataTable();
        dsData.Columns.Add("SF Code", typeof(string));
        dsData.Columns.Add("Name", typeof(string));
        dsData.Columns.Add("Target Working Days", typeof(Int64));
        dsData.Columns.Add("Actual Working Days", typeof(Int64));
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
        dsData.Columns.Add("Total Volume Strings", typeof(string));
        dsData.Columns.Add("Target Vol Achivemnt", typeof(string));
        dsData.Columns.Add("Total value", typeof(string));
        dsData.Columns.Add("Value Achivement", typeof(string));
        dsData.Columns.Add("Average Daily Sales", typeof(string));
        dsData.Columns.Add("Average Drop size", typeof(string));
        dsData.Columns.Add("Average Daily Productive Calls", typeof(string));
        //lbl_SFname.Text = dsMGR.Tables[0].Rows.Count.ToString();
        foreach (DataRow mgrow in dsMGR.Tables[0].Rows)
        {
            tot_twdays = 0;
            tot_actdays = 0;
            tot_target_calls = 0;
            tot_target_pro_calls = 0;
            Act_call = 0;
            //Call_Rate = 0;
            Act_Pro_Call = 0;
            New_Doors = 0;
            Rep_Pur = 0;
            tot_convertion = 0;
            tot_con = 0;
            Tot_vol = 0;
            Tot_val = 0;
            decimal tot_volume = 0;
            decimal tot_value = 0;

            DataRow[] drow = null;
            if (mgrow["SF_Code"].ToString() == sfCode)
            {
                drow = dsMr.Tables[0].Select("Reporting_To_SF = '" + mgrow["SF_Code"].ToString() + "' or sf_code='" + mgrow["SF_Code"].ToString() + "'");
            }
            else
            {
                drow = dsMr.Tables[0].Select("Reporting_To_SF = '" + mgrow["SF_Code"].ToString() + "'");
            }
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

                    if (wrow != null)
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


                    if (dcrrow != null)
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


                    if (orderrow != null)
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
                        rep_count = 0;
                    }


                    DataRow[] lrdrrow = null;
                    if (dsDr.Tables[0].Rows.Count > 0)
                    {
                        lrdrrow = dsDr.Tables[0].Select("sf_code ='" + row["SF_Code"].ToString() + "'");
                    }


                    if (lrdrrow != null)
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


                    if (tot_volrow != null)
                    {
                        foreach (DataRow r in tot_volrow)
                        {
                            tot_vol = Convert.ToDecimal(r["qty"]);
                            tot_volume += tot_vol;
                        }
                    }

                    else
                    {
                        tot_vol = 0;
                        tot_volume += tot_vol;
                    }

                    //Total value

                    DataRow[] tot_valuerow = null;
                    if (order_val.Tables[0].Rows.Count > 0)
                    {
                        tot_valuerow = order_val.Tables[0].Select("sf_code ='" + row["SF_Code"].ToString() + "'");
                    }


                    if (tot_valuerow != null)
                    {
                        foreach (DataRow r in tot_valuerow)
                        {
                            tot_val = Convert.ToDecimal(r["val"]);
                            tot_value += tot_val;
                        }
                    }

                    else
                    {
                        tot_val = 0;
                        tot_value += tot_val;
                    }


                    decimal tot_cal = Target_Calls_day * twdays;
                    decimal tot_pro_cal = Target_Productive_Calls_Day * twdays;
                    decimal Rep_pur_rate = 0;
                    decimal Avg_day_sale = 0;
                    decimal Avg_drop_size = 0;
                    decimal Avg_day_pro_call = 0;



                    decimal Tar_Vol_Ach = 0;
                    if ((tot_pro_cal * Target_sold) > 0)
                    {
                        Tar_Vol_Ach = tot_vol / (tot_pro_cal * Target_sold) * 100;
                    }
                   
                    decimal val_Ach = 0;
                  
                    DataRow[] val_Achr = null;
                    if (dsMGRtv.Tables[0].Rows.Count > 0)
                    {
                        val_Achr = dsMGRtv.Tables[0].Select("sf_code ='" + row["SF_Code"].ToString() + "'");
                    }


                    if (val_Achr != null)
                    {
                        foreach (DataRow r in val_Achr)
                        {
                            val_Ach = (Convert.ToDecimal(r["Target_Value"]) == 0) ? 0 : (tot_val / Convert.ToDecimal(r["Target_Value"])) * 100;
                            
                        }
                    }

                    else
                    {
                        val_Ach = 0;
                       
                    }
                    tot_target_calls += tot_cal;
                    tot_target_pro_calls += tot_pro_cal;
                    Act_call += dcr_count;
                    Act_Pro_Call += order_count;
                    New_Doors += lrdr_count;
                    Rep_Pur += rep_count;

                    if (dcr_count > 0)
                    {
                        tot_convertion = order_count / dcr_count;
                    }
                    else
                    {
                        tot_convertion = 0;
                    }
                    tot_con += tot_convertion;

                    if (order_count > 0)
                    {
                        Rep_pur_rate = rep_count / order_count;
                    }

                    if (awd > 0)
                    {
                        Avg_day_sale = tot_val / awd;
                    }

                    if (order_count > 0)
                    {
                        Avg_drop_size = tot_val / order_count;
                    }

                    if (awd > 0)
                    {
                        Avg_day_pro_call = order_count / awd;
                    }
                    decimal avg_pro_cal = 0;

                    if (tot_cal > 0)
                    {
                        avg_pro_cal = dcr_count / tot_cal * 100;
                    }

                    dsData.Rows.Add(row["SF_Code"].ToString(), row["Sf_Name"].ToString(), twdays, awd, twdays == 0 ? "0" : (awd / twdays * 100).ToString("0") + '%', tot_cal, tot_pro_cal, dcr_count, avg_pro_cal.ToString("0") + "%", order_count, (tot_convertion * 100).ToString("0") + "%", lrdr_count, rep_count, (Rep_pur_rate * 100).ToString("0") + "%", tot_vol, Tar_Vol_Ach.ToString("0") + "%", tot_val.ToString("0.00"), val_Ach.ToString("0") + "%", (Avg_day_sale).ToString("0"), (Avg_drop_size).ToString("0"), (Avg_day_pro_call).ToString("0"));
                    tot_twdays += twdays;
                }
                decimal avg_tot_pro_cal = 0;

                if (tot_target_calls > 0)
                {
                    avg_tot_pro_cal = Act_call / tot_target_calls * 100;

                }
                decimal avg_conversion = 0;
                if (Act_call > 0)
                {
                    avg_conversion = Act_Pro_Call / Act_call * 100;
                }
                decimal avg_rept_pur_rate = 0;
                decimal avg_drop_size = 0;
                if (Act_Pro_Call > 0)
                {
                    avg_rept_pur_rate = Rep_Pur / Act_Pro_Call * 100;
                    avg_drop_size = tot_value / Act_Pro_Call;
                }
                decimal avg_tot_volume = 0;
                if ((Target_sold * tot_target_pro_calls) > 0)
                {
                    avg_tot_volume = tot_volume / (Target_sold * tot_target_pro_calls) * 100;
                }

                decimal avg_tot_value = 0;
                if ((target_drop_size * tot_target_pro_calls) > 0)
                {
                    avg_tot_value = tot_value / (target_drop_size * tot_target_pro_calls) * 100;
                }

                decimal avg_day_sal = 0;
                decimal avg_day_productive = 0;
                if (tot_actdays > 0)
                {
                    avg_day_sal = tot_value / tot_actdays;
                    avg_day_productive = Act_Pro_Call / tot_actdays;
                }
                Grand_twd += tot_twdays;
                Grand_awd += tot_actdays;
                Grand_tc += tot_target_calls;
                Grand_tpc += tot_target_pro_calls;
                Grand_ac += Act_call;
                Grand_apc += Act_Pro_Call;
                Grand_nd += New_Doors;
                Grand_rp += Rep_Pur;
                Grand_vol += tot_volume;
                Grand_val += tot_value;

                dsData.Rows.Add("sub_tot", "SUB TOTAL (" + mgrow["Sf_Name"].ToString() + " )", tot_twdays, tot_actdays, tot_twdays == 0 ? "0" : (tot_actdays / tot_twdays * 100).ToString("0") + '%', tot_target_calls, tot_target_pro_calls, Act_call, avg_tot_pro_cal.ToString("0") + "%", Act_Pro_Call, avg_conversion.ToString("0") + "%", New_Doors, Rep_Pur, avg_rept_pur_rate.ToString("0") + "%", tot_volume, avg_tot_volume.ToString("0") + "%", tot_value.ToString("0.00"), "", avg_day_sal.ToString("0"), avg_drop_size.ToString("0"), avg_day_productive.ToString("0"));
            }
        }
        if (Grand_twd > 0)
        {
            Grand_taper = Grand_awd / Grand_twd * 100;
        }
        if (Grand_tc > 0)
        {
            Grand_cr = Grand_ac / Grand_tc * 100;
        }
        if (Grand_ac > 0)
        {
            Grand_conrte = Grand_apc / Grand_ac * 100;
        }
        if (Grand_apc > 0)
        {
            Grand_rpr = Grand_rp / Grand_apc * 100;
        }
        if (Grand_tpc * target_drop_size > 0)
        {
            Grand_volach = Grand_vol / (Grand_tpc * target_drop_size) * 100;
        }
        if (Grand_tpc * target_drop_size > 0)
        {
            Grand_talach = Grand_val / (Grand_tpc * target_drop_size) * 100;
        }

        if (Grand_awd > 0)
        {
            Grand_ads = Grand_val / Grand_awd;
        }
        if (Grand_apc > 0)
        {
            Grand_adropsize = Grand_val / Grand_apc;
        }
        if (Grand_awd > 0)
        {
            Grand_adp = Grand_apc / Grand_awd;
        }
        dsData.Rows.Add("GRAND_TOTAL", "GRAND TOTAL", Grand_twd, Grand_awd, Grand_taper.ToString("0") + '%', Grand_tc, Grand_tpc, Grand_ac, Grand_cr.ToString("0") + "%", Grand_apc, Grand_conrte.ToString("0") + "%", Grand_nd, Grand_rp, Grand_rpr.ToString("0") + "%", Grand_vol, Grand_volach.ToString("0") + "%", Grand_val.ToString("0.00"), "", Grand_ads.ToString("0.00"), Grand_adropsize.ToString("0.00"), Grand_adp.ToString("0"));

        Dgv_SKU.DataSource = dsData;
        Dgv_SKU.DataBind();
        Dgv_SKU.HeaderStyle.BackColor = System.Drawing.Color.FromName("#496a9a");
        Dgv_SKU.HeaderStyle.ForeColor = System.Drawing.Color.FromName("#ffffff");

    }


    protected void Dgv_SKU_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridViewRow grv = e.Row;
            if (grv.Cells[0].Text.Equals("sub_tot"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#607d8b");
                e.Row.ForeColor = System.Drawing.Color.FromName("#ffffff");
                e.Row.Font.Bold = true;
            }
            if (grv.Cells[1].Text.Equals("GRAND TOTAL"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#496a9a");
                e.Row.ForeColor = System.Drawing.Color.FromName("#ffffff");
                e.Row.Font.Bold = true;
            }
            //e.Row.Cells[0].Visible = false;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

            //e.Row.Cells[1].Width = 300;

        }
        catch (Exception ex)
        { }

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
        lblhead1.Visible = true;
        string strFileName = Page.Title;
        string attachment = "attachment; filename=" + strFileName + ".xls";

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
        // Response.Write("Purchase_Register_Distributor_wise.aspx");

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

        string strFileName = Page.Title;
        lblhead1.Visible = false;

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {

                //this.Page.RenderControl(hw);
                this.RenderControl(hw);

                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A1, 10f, 10f, 10f, 0f);
                //  Document pdfDoc = new Document(new Rectangle(200f, 300f));
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