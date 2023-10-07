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



public partial class MIS_Reports_rpt_target_order : System.Web.UI.Page
{
    string sf_code = string.Empty;
    string sfname = string.Empty;
    string div_code = string.Empty;
    decimal iTotLstCount1 = 0;
    decimal iTotLstCount = 0;
    string sf_type = string.Empty;
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
    string Year = string.Empty;


    DataSet dsprd = new DataSet();
    string sCurrentDate = string.Empty;
    string stockist_code = string.Empty;
    string stURL = string.Empty;
    string Stock_name = string.Empty;
    string Sub_Div_Code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["SF_Code"].ToString(); // Session["SF_code"].ToString();
        Year = Request.QueryString["Year"].ToString();
        Sub_Div_Code = Request.QueryString["Sub_Div"].ToString();
        hdSub_Div.Value = Sub_Div_Code;
        lblhead.Text = "   "+ Year;
        hd_sfcode.Value = sf_code;
        lblhead1.Text = "Target Achievement Report for Year of - <b>" + Year + "</b>";
        lblsf.Text = " " + Request.QueryString["SF_Name"].ToString(); 
        FillSF();
    }
    private void FillSF()
    {
        
        DataTable Target_Dt = new DataTable();
        Target_Dt.Columns.Add("sf_type", typeof(string));
        Target_Dt.Columns.Add("Year", typeof(string));
        Target_Dt.Columns.Add("Name", typeof(string));
        Target_Dt.Columns.Add("SF_CODE", typeof(string));
        Target_Dt.Columns.Add("JAN_TV", typeof(decimal));
        Target_Dt.Columns.Add("JAN_OV", typeof(decimal));
        Target_Dt.Columns.Add("JAN_ACH", typeof(decimal));

        Target_Dt.Columns.Add("FEB_TV", typeof(decimal));
        Target_Dt.Columns.Add("FEB_OV", typeof(decimal));
        Target_Dt.Columns.Add("FEB_ACH", typeof(decimal));

        Target_Dt.Columns.Add("MAR_TV", typeof(decimal));
        Target_Dt.Columns.Add("MAR_OV", typeof(decimal));
        Target_Dt.Columns.Add("MAR_ACH", typeof(decimal));

        Target_Dt.Columns.Add("APR_TV", typeof(decimal));
        Target_Dt.Columns.Add("APR_OV", typeof(decimal));
        Target_Dt.Columns.Add("APR_ACH", typeof(decimal));

        Target_Dt.Columns.Add("MAY_TV", typeof(decimal));
        Target_Dt.Columns.Add("MAY_OV", typeof(decimal));
        Target_Dt.Columns.Add("MAY_ACH", typeof(decimal));

        Target_Dt.Columns.Add("JUN_TV", typeof(decimal));
        Target_Dt.Columns.Add("JUN_OV", typeof(decimal));
        Target_Dt.Columns.Add("JUN_ACH", typeof(decimal));

        Target_Dt.Columns.Add("JUL_TV", typeof(decimal));
        Target_Dt.Columns.Add("JUL_OV", typeof(decimal));
        Target_Dt.Columns.Add("JUL_ACH", typeof(decimal));

        Target_Dt.Columns.Add("AUG_TV", typeof(decimal));
        Target_Dt.Columns.Add("AUG_oV", typeof(decimal));
        Target_Dt.Columns.Add("AUG_ACH", typeof(decimal));

        Target_Dt.Columns.Add("SEP_TV", typeof(decimal));
        Target_Dt.Columns.Add("SEP_OV", typeof(decimal));
        Target_Dt.Columns.Add("SEP_ACH", typeof(decimal));

        Target_Dt.Columns.Add("OCT_TV", typeof(decimal));
        Target_Dt.Columns.Add("OCT_OV", typeof(decimal));
        Target_Dt.Columns.Add("OCT_ACH", typeof(decimal));

        Target_Dt.Columns.Add("NOV_TV", typeof(decimal));
        Target_Dt.Columns.Add("NOV_OV", typeof(decimal));
        Target_Dt.Columns.Add("NOV_ACH", typeof(decimal));

        Target_Dt.Columns.Add("DEC_TV", typeof(decimal));
        Target_Dt.Columns.Add("DEC_OV", typeof(decimal));
        Target_Dt.Columns.Add("DEC_ACH", typeof(decimal));

        Target_Dt.Columns.Add("TOT_TV", typeof(decimal));
        Target_Dt.Columns.Add("TOT_OV", typeof(decimal));
        Target_Dt.Columns.Add("TOT_ACH", typeof(decimal));

        SalesForce SF = new SalesForce();
        dsSalesForce = SF.getDoctorCount_SFWise_new(div_code, Sub_Div_Code, sf_code);
        Order od = new Order();

		decimal ovr_tot_jan_tar = 0;
        decimal ovr_tot_jan_ord = 0;
        decimal ovr_tot_jan_ach = 0;

        decimal ovr_tot_feb_tar = 0;
        decimal ovr_tot_feb_ord = 0;
        decimal ovr_tot_feb_ach = 0;

        decimal ovr_tot_mar_tar = 0;
        decimal ovr_tot_mar_ord = 0;
        decimal ovr_tot_mar_ach = 0;

        decimal ovr_tot_apr_tar = 0;
        decimal ovr_tot_apr_ord = 0;
        decimal ovr_tot_apr_ach = 0;

        decimal ovr_tot_may_tar = 0;
        decimal ovr_tot_may_ord = 0;
        decimal ovr_tot_may_ach = 0;

        decimal ovr_tot_jun_tar = 0;
        decimal ovr_tot_jun_ord = 0;
        decimal ovr_tot_jun_ach = 0;

        decimal ovr_tot_jul_tar = 0;
        decimal ovr_tot_jul_ord = 0;
        decimal ovr_tot_jul_ach = 0;

        decimal ovr_tot_aug_tar = 0;
        decimal ovr_tot_aug_ord = 0;
        decimal ovr_tot_aug_ach = 0;

        decimal ovr_tot_sep_tar = 0;
        decimal ovr_tot_sep_ord = 0;
        decimal ovr_tot_sep_ach = 0;

        decimal ovr_tot_oct_tar = 0;
        decimal ovr_tot_oct_ord = 0;
        decimal ovr_tot_oct_ach = 0;

        decimal ovr_tot_nov_tar = 0;
        decimal ovr_tot_nov_ord = 0;
        decimal ovr_tot_nov_ach = 0;

        decimal ovr_tot_dec_tar = 0;
        decimal ovr_tot_dec_ord = 0;
        decimal ovr_tot_dec_ach = 0;


        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
           	DataSet dsTO = null;
            if (sf_code == row["sf_code"].ToString())
            {
                dsTO = od.get_target_order_mgr(div_code, row["sf_code"].ToString(), Year);
            }
            else
            {
                dsTO = od.get_target_order(div_code, Sub_Div_Code, Year, row["sf_code"].ToString());
            }
            decimal tot_jan_tar = 0;
            decimal tot_jan_ord = 0;
            decimal tot_jan_ach = 0;

            decimal tot_feb_tar = 0;
            decimal tot_feb_ord = 0;
            decimal tot_feb_ach = 0;

            decimal tot_mar_tar = 0;
            decimal tot_mar_ord = 0;
            decimal tot_mar_ach = 0;

            decimal tot_apr_tar = 0;
            decimal tot_apr_ord = 0;
            decimal tot_apr_ach = 0;
            
            decimal tot_may_tar = 0;
            decimal tot_may_ord = 0;
            decimal tot_may_ach = 0;
            
            decimal tot_jun_tar = 0;
            decimal tot_jun_ord = 0;
            decimal tot_jun_ach = 0;
            
            decimal tot_jul_tar = 0;
            decimal tot_jul_ord = 0;
            decimal tot_jul_ach = 0;

            decimal tot_aug_tar = 0;
            decimal tot_aug_ord = 0;
            decimal tot_aug_ach = 0;

            decimal tot_sep_tar = 0;
            decimal tot_sep_ord = 0;
            decimal tot_sep_ach = 0;

            decimal tot_oct_tar = 0;
            decimal tot_oct_ord = 0;
            decimal tot_oct_ach = 0;

            decimal tot_nov_tar = 0;
            decimal tot_nov_ord = 0;
            decimal tot_nov_ach = 0;

            decimal tot_dec_tar = 0;
            decimal tot_dec_ord = 0;
            decimal tot_dec_ach = 0;

          

            foreach (DataRow dr in dsTO.Tables[0].Rows)
            {

                decimal jant = dr["January_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["January_target_val"]);
                decimal jano = dr["January_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["January_order_val"]);
                decimal jana = jant > 0 ? (jano / jant * 100) : 0;


                decimal febt = dr["February_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["February_target_val"]);
                decimal febo = dr["February_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["February_order_val"]);
                decimal feba = febt > 0 ? (febo / febt * 100) : 0;

                decimal mart = dr["March_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["March_target_val"]);
                decimal maro = dr["March_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["March_order_val"]);
                decimal mara = mart > 0 ? (maro / mart * 100) : 0;

                decimal aprt = dr["April_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["April_target_val"]);
                decimal apro = dr["April_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["April_order_val"]);
                decimal apra = aprt > 0 ? (apro / aprt * 100) : 0;

                decimal mayt = dr["May_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["May_target_val"]);
                decimal mayo = dr["May_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["May_order_val"]);
                decimal maya = mayt > 0 ? (mayo / mayt * 100) : 0;

                decimal junt = dr["June_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["June_target_val"]);
                decimal juno = dr["June_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["June_order_val"]);
                decimal juna = junt > 0 ? (juno / junt * 100) : 0;

                decimal jult = dr["July_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["July_target_val"]);
                decimal julo = dr["July_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["July_order_val"]);
                decimal jula = jult > 0 ? (julo / jult * 100) : 0;

                decimal augt = dr["August_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["August_target_val"]);
                decimal augo = dr["August_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["August_order_val"]);
                decimal auga = augt > 0 ? (augo / augt * 100) : 0;

                decimal sept = dr["September_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["September_target_val"]);
                decimal sepo = dr["September_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["September_order_val"]);
                decimal sepa = sept > 0 ? (sepo / sept * 100) : 0;

                decimal octt = dr["October_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["October_target_val"]);
                decimal octo = dr["October_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["October_order_val"]);
                decimal octa = octt > 0 ? (octo / octt * 100) : 0;

                decimal novt = dr["November_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["November_target_val"]);
                decimal novo = dr["November_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["November_order_val"]);
                decimal nova = novt > 0 ? (novo / novt * 100) : 0;

                decimal dect = dr["December_target_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["December_target_val"]);
                decimal deco = dr["December_order_val"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["December_order_val"]);
                decimal deca = dect > 0 ? (deco / dect * 100) : 0;

                

                tot_jan_tar += jant;
                tot_jan_ord += jano;

                tot_feb_tar += febt;
                tot_feb_ord += febo;

                tot_mar_tar += mart;
                tot_mar_ord += maro;

                tot_apr_tar += aprt;
                tot_apr_ord += apro;

                tot_may_tar += mayt;
                tot_may_ord += mayo;

                tot_jun_tar += junt;
                tot_jun_ord += juno;

                tot_jul_tar += jult;
                tot_jul_ord += julo;

                tot_aug_tar += augt;
                tot_aug_ord += augo;

                tot_sep_tar += sept;
                tot_sep_ord += sepo;

                tot_oct_tar += octt;
                tot_oct_ord += octo;

                tot_nov_tar += novt;
                tot_nov_ord += novo;

                tot_dec_tar += dect;
                tot_dec_ord += deco;
                
                //Target_Dt.Rows.Add(Year, dr["sf_name"].ToString(), dr["sf_code"].ToString(), jant, jano.ToString("0.00"), jana.ToString("0.00"), febt, febo.ToString("0.00"), feba.ToString("0.00"), mart, maro.ToString("0.00"), mara.ToString("0.00"), aprt, apro.ToString("0.00"), apra.ToString("0.00"), mayt, mayo.ToString("0.00"), maya.ToString("0.00"), junt, juno.ToString("0.00"), juna.ToString("0.00"), jult, julo.ToString("0.00"), jula.ToString("0.00"), augt, augo.ToString("0.00"), auga.ToString("0.00"), sept, sepo.ToString("0.00"), sepa.ToString("0.00"), octt, octo.ToString("0.00"), octa.ToString("0.00"), novt, novo.ToString("0.00"), nova.ToString("0.00"), dect, deco.ToString("0.00"), deca.ToString("0.00"), TOT_T, TOT_O.ToString("0.00"), TOT_A.ToString("0.00"));
            }

            tot_jan_ach += tot_jan_tar > 0 ? (tot_jan_ord / tot_jan_tar * 100) : 0;

            tot_feb_ach += tot_feb_tar > 0 ? (tot_feb_ord / tot_feb_tar * 100) : 0;

            tot_mar_ach += tot_mar_tar > 0 ? (tot_mar_ord / tot_mar_tar * 100) : 0;

            tot_apr_ach += tot_apr_tar > 0 ? (tot_apr_ord / tot_apr_tar * 100) : 0;

            tot_may_ach += tot_may_tar > 0 ? (tot_may_ord / tot_may_tar * 100) : 0;

            tot_jun_ach += tot_jun_tar > 0 ? (tot_jun_ord / tot_jun_tar * 100) : 0;

            tot_jul_ach += tot_jul_tar > 0 ? (tot_jul_ord / tot_jul_tar * 100) : 0;

            tot_aug_ach += tot_aug_tar > 0 ? (tot_aug_ord / tot_aug_tar * 100) : 0;

            tot_sep_ach += tot_sep_tar > 0 ? (tot_sep_ord / tot_sep_tar * 100) : 0;

            tot_oct_ach += tot_oct_tar > 0 ? (tot_oct_ord / tot_oct_tar * 100) : 0;

            tot_nov_ach += tot_nov_tar > 0 ? (tot_nov_ord / tot_nov_tar * 100) : 0;

            tot_dec_ach += tot_dec_tar > 0 ? (tot_dec_ord / tot_dec_tar * 100) : 0;



            decimal TOT_T = tot_jan_tar + tot_feb_tar + tot_mar_tar + tot_apr_tar + tot_may_tar + tot_jun_tar + tot_jul_tar + tot_aug_tar + tot_sep_tar + tot_oct_tar + tot_nov_tar + tot_dec_tar ;
            decimal TOT_O = tot_jan_ord + tot_feb_ord + tot_mar_ord + tot_apr_ord + tot_may_ord + tot_jun_ord + tot_jul_ord + tot_aug_ord + tot_sep_ord + tot_oct_ord + tot_nov_ord + tot_dec_ord ;
            decimal TOT_A = TOT_T > 0 ? (TOT_O / TOT_T * 100) : 0;



            Target_Dt.Rows.Add(row["sf_type"].ToString(), Year, row["sf_name"].ToString(), row["sf_code"].ToString(), tot_jan_tar, tot_jan_ord, tot_jan_ach.ToString("0.00"), tot_feb_tar, tot_feb_ord, tot_feb_ach.ToString("0.00"), tot_mar_tar, tot_mar_ord, tot_mar_ach.ToString("0.00"), tot_apr_tar, tot_apr_ord, tot_apr_ach.ToString("0.00"), tot_may_tar, tot_may_ord, tot_may_ach.ToString("0.00"), tot_jun_tar, tot_jun_ord, tot_jun_ach.ToString("0.00"), tot_jul_tar, tot_jul_ord, tot_jul_ach.ToString("0.00"), tot_aug_tar, tot_aug_ord, tot_aug_ach.ToString("0.00"), tot_sep_tar, tot_sep_ord, tot_sep_ach.ToString("0.00"), tot_oct_tar, tot_oct_ord, tot_oct_ach.ToString("0.00"), tot_nov_tar, tot_nov_ord, tot_nov_ach.ToString("0.00"), tot_dec_tar, tot_dec_ord, tot_dec_ach.ToString("0.00"),TOT_T,TOT_O,TOT_A.ToString("0.00"));
 			
			ovr_tot_jan_tar += tot_jan_tar;
            ovr_tot_jan_ord += tot_jan_ord;


            ovr_tot_feb_tar += tot_feb_tar;
            ovr_tot_feb_ord += tot_feb_ord;


            ovr_tot_mar_tar += tot_mar_tar;
            ovr_tot_mar_ord += tot_mar_ord;


            ovr_tot_apr_tar += tot_apr_tar;
            ovr_tot_apr_ord += tot_apr_ord;


            ovr_tot_may_tar += tot_may_tar;
            ovr_tot_may_ord += tot_may_ord;


            ovr_tot_jun_tar += tot_jun_tar;
            ovr_tot_jun_ord += tot_jun_ord;


            ovr_tot_jul_tar += tot_jul_tar;
            ovr_tot_jul_ord += tot_jul_ord;


            ovr_tot_aug_tar += tot_aug_tar;
            ovr_tot_aug_ord += tot_aug_ord;


            ovr_tot_sep_tar += tot_sep_tar;
            ovr_tot_sep_ord += tot_sep_ord;


            ovr_tot_oct_tar += tot_oct_tar;
            ovr_tot_oct_ord += tot_oct_ord;


            ovr_tot_nov_tar += tot_nov_tar;
            ovr_tot_nov_ord += tot_nov_ord;


            ovr_tot_dec_tar += tot_dec_tar;
            ovr_tot_dec_ord += tot_dec_ord;

        }

		
        ovr_tot_jan_ach += ovr_tot_jan_tar > 0 ? (ovr_tot_jan_ord / ovr_tot_jan_tar * 100) : 0;

        ovr_tot_feb_ach += ovr_tot_feb_tar > 0 ? (ovr_tot_feb_ord / ovr_tot_feb_tar * 100) : 0;

        ovr_tot_mar_ach += ovr_tot_mar_tar > 0 ? (ovr_tot_mar_ord / ovr_tot_mar_tar * 100) : 0;

        ovr_tot_apr_ach += ovr_tot_apr_tar > 0 ? (ovr_tot_apr_ord / ovr_tot_apr_tar * 100) : 0;

        ovr_tot_may_ach += ovr_tot_may_tar > 0 ? (ovr_tot_may_ord / ovr_tot_may_tar * 100) : 0;

        ovr_tot_jun_ach += ovr_tot_jun_tar > 0 ? (ovr_tot_jun_ord / ovr_tot_jun_tar * 100) : 0;

        ovr_tot_jul_ach += ovr_tot_jul_tar > 0 ? (ovr_tot_jul_ord / ovr_tot_jul_tar * 100) : 0;

        ovr_tot_aug_ach += ovr_tot_aug_tar > 0 ? (ovr_tot_aug_ord / ovr_tot_aug_tar * 100) : 0;

        ovr_tot_sep_ach += ovr_tot_sep_tar > 0 ? (ovr_tot_sep_ord / ovr_tot_sep_tar * 100) : 0;

        ovr_tot_oct_ach += ovr_tot_oct_tar > 0 ? (ovr_tot_oct_ord / ovr_tot_oct_tar * 100) : 0;

        ovr_tot_nov_ach += ovr_tot_nov_tar > 0 ? (ovr_tot_nov_ord / ovr_tot_nov_tar * 100) : 0;

        ovr_tot_dec_ach += ovr_tot_dec_tar > 0 ? (ovr_tot_dec_ord / ovr_tot_dec_tar * 100) : 0;

        decimal OVR_TOT_T = ovr_tot_jan_tar + ovr_tot_feb_tar + ovr_tot_mar_tar + ovr_tot_apr_tar + ovr_tot_may_tar + ovr_tot_jun_tar + ovr_tot_jul_tar + ovr_tot_aug_tar + ovr_tot_sep_tar + ovr_tot_oct_tar + ovr_tot_nov_tar + ovr_tot_dec_tar;
        decimal OVR_TOT_O = ovr_tot_jan_ord + ovr_tot_feb_ord + ovr_tot_mar_ord + ovr_tot_apr_ord + ovr_tot_may_ord + ovr_tot_jun_ord + ovr_tot_jul_ord + ovr_tot_aug_ord + ovr_tot_sep_ord + ovr_tot_oct_ord + ovr_tot_nov_ord + ovr_tot_dec_ord;
        decimal OVR_TOT_A = OVR_TOT_T > 0 ? (OVR_TOT_O / OVR_TOT_T * 100) : 0;
        Target_Dt.Rows.Add("Total", Year, "Total", "Total", ovr_tot_jan_tar, ovr_tot_jan_ord, ovr_tot_jan_ach.ToString("0.00"), ovr_tot_feb_tar, ovr_tot_feb_ord, ovr_tot_feb_ach.ToString("0.00"), ovr_tot_mar_tar, ovr_tot_mar_ord, ovr_tot_mar_ach.ToString("0.00"), ovr_tot_apr_tar, ovr_tot_apr_ord, ovr_tot_apr_ach.ToString("0.00"), ovr_tot_may_tar, ovr_tot_may_ord, ovr_tot_may_ach.ToString("0.00"), ovr_tot_jun_tar, ovr_tot_jun_ord, ovr_tot_jun_ach.ToString("0.00"), ovr_tot_jul_tar, ovr_tot_jul_ord, ovr_tot_jul_ach.ToString("0.00"), ovr_tot_aug_tar, ovr_tot_aug_ord, ovr_tot_aug_ach.ToString("0.00"), ovr_tot_sep_tar, ovr_tot_sep_ord, ovr_tot_sep_ach.ToString("0.00"), ovr_tot_oct_tar, ovr_tot_oct_ord, ovr_tot_oct_ach.ToString("0.00"), ovr_tot_nov_tar, ovr_tot_nov_ord, ovr_tot_nov_ach.ToString("0.00"), ovr_tot_dec_tar, ovr_tot_dec_ord, ovr_tot_dec_ach.ToString("0.00"), OVR_TOT_T, OVR_TOT_O, OVR_TOT_A.ToString("0.00"));





        GVData.DataSource = Target_Dt;
        GVData.DataBind();

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