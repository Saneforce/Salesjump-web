using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FusionCharts.Charts;
using Newtonsoft.Json;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using System.Globalization;
using System.Web.Services;
using Bus_Objects;
using System.Text;
using DBase_EReport;

public partial class DashBoard : System.Web.UI.Page
{
    public static string tot_dr = string.Empty;
    public static decimal tot_dr1 = 0;
    public static decimal tot_dr2 = 0;
    public static string tot_Drrr = string.Empty;
    public static decimal ilist = 0;
    public static decimal ilistt = 0;
    public static decimal ilistt1 = 0;
    public static string Monthsub = string.Empty;
    DataSet dsDoc = null;
    DataSet dsDoc1 = null;
    DataSet dsSalesForce = null;
    DataSet dsdoc = null;
    DataSet dsDoctor = null;
    DataSet dsTP = null;
	DataSet dsf = null;					
    string iPendingCount = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    //DataSet dsState = null;
    public static string Month = string.Empty;
    public static string Year = string.Empty;
    public static int count_tot = 0;
    public static int count_tot1 = 0;
    DataSet dsDivision = null;
    DataSet dsState = null;
    public static string state_cd = string.Empty;
    public static string sState = string.Empty;
    string[] statecd;
    public static string stCrtDtaPnt = string.Empty;
    public static string iTotLstCount1 = string.Empty;
    public static string stCrtDtaPnt1 = string.Empty;
    public static string iTotLstCount2 = string.Empty;
    public static string iTotLstCount3 = string.Empty;
    public static string iTotLstCounts1 = string.Empty;
    public static string iTotLstCounts2 = string.Empty;
    public static string iTotLstCounts3 = string.Empty;
    public static string sf_type = string.Empty;
    Notice viewnoti = new Notice();
    public static string day = string.Empty;
    public static string type = string.Empty;
    public static string comment = string.Empty;
    public static string sCurrentDate = string.Empty;
    public string SecOrderCap = "Secondary Order";
    decimal d1 = 0;
    decimal d2 = 0;
    decimal MonthTot = 0;
    decimal TodayTot = 0;
    decimal PMonthTot = 0;
    decimal PTodayTot = 0;
    string priMTot = "0";
    decimal chMonth = 0;
    decimal chDay = 0;
    decimal brDay = 0;
    decimal brMonth = 0;
	public static string sub_divc = string.Empty;
    DateTime dtCurrent;
    SqlConnection con = new SqlConnection(Globals.ConnString);

    DateTime dTime = DateTime.UtcNow.Date;
    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["division_code"].ToString();
        }
        div_code = div_code.Trim(",".ToCharArray());
		sub_divc = Session["sub_division"].ToString();
        if (div_code == "98") SecOrderCap = "Daily Calls Report";
        Year = DateTime.Now.Year.ToString();

        //mano hide
        if (div_code != "32" && div_code != "19")
        {
            string sJSON = Three_Tier();
           // Response.Write(sJSON.ToString());
            Chart sales = new Chart("scrollcolumn2d", "myChart", "720", "350", "json", sJSON.ToString());
            Literal1.Text = sales.Render();
        }
        else
        {
 
        }



        //string scrpt1 = "arr=[" + Fillcate() + "];ayy=[" + NewChart() + "];azz=[" + NewChart1() + "];arrt=[" + Fillproductiveorder() + "];window.onload = function () {genChart1('ChrtPrimSec',ayy,azz,'PRIMARY VS SECONDARY');gen('prodt',arrt,'');}";
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt1, true);
        //string scrpt = "ayy=[" + NewChart() + "];azz=[" + NewChart1() + "];arrt=[" + Fillproductiveorder() + "];arr1=[" + FillCategorywiseOrderDay() + "];arr2=[" + FillCategorywiseOrderMonth() + "];arr3=[" + FillTop5brand() + "];arr4=[" + FillB5brand() + "];arr5=[" + FillTop5pro() + "];arr6=[" + FillB5pro() + "];arr7=[" + saleFillTop5cate() + "];arr8=[" + saleFillB5cate() + "];arr9=[" + saleFillTop5brand() + "];arr10=[" + saleFillB5brand() + "];arr11=[" + saleFillTop5pro() + "];arr12=[" + saleFillB5pro() + "];arr13=[" + In_Time_St() + "];window.onload = function () {gen1('T5Cate',arr1,'Order Top 5 Categorys');gen1('B5Cate',arr2,'Order Bottom 5 Categorys');gen1('T5brand',arr3,'Order Top 5 brands');gen1('B5brand',arr4,'Order Bottom 5 brands');gen1('T5Pro',arr5,'Retailer Top 5 Products');gen1('B5Pro',arr6,'Retailer Bottom 5 Products');gen1('ST5Cate',arr7,'Retailer Bottom 5 Products');gen1('SB5Cate',arr8,'Retailer Bottom 5 Products');gen1('ST5brand',arr9,'Retailer Bottom 5 Products');gen1('SB5brand',arr10,'Retailer Bottom 5 Products');gen1('ST5Pro',arr11,'Retailer Bottom 5 Products');gen1('SB5Pro',arr12,'Retailer Bottom 5 Products');gen2('chartContainer',arr13,'Time');genChart1('ChrtPrimSec',ayy,azz,'PRIMARY VS SECONDARY');}";
        //string scrpt = "ayy=[" + NewChart() + "];azz=[" + NewChart1() + "];arrt=[" + Fillproductiveorder() + "];arr1=[" + FillCategorywiseOrderDay() + "];arr2=[" + FillCategorywiseOrderMonth() + "];arr3=[" + FillTop5brand() + "];arr4=[" + FillB5brand() + "];arr5=[" + FillTop5pro() + "];arr6=[" + FillB5pro() + "];arr7=[" + PrimaryCategorywiseOrderday() + "];arr8=[" + PrimaryCategorywiseOrderMonth() + "];arr9=[" + saleFillTop5brand() + "];arr10=[" + saleFillB5brand() + "];arr11=[" + saleFillTop5pro() + "];arr12=[" + saleFillB5pro() + "];arr13=[" + In_Time_St() + "];window.onload = function () {gen1('T5Cate',arr1,'" + TodayTot.ToString() + "','Today');gen1('B5Cate',arr2,'" + MonthTot.ToString() + "','Month');gen1('T5brand',arr3,'Order Top 5 brands','');gen1('B5brand',arr4,'Order Bottom 5 brands','');gen1('T5Pro',arr5,'Retailer Top 5 Products','');gen1('B5Pro',arr6,'Retailer Bottom 5 Products','');gen1('ST5Cate',arr7,'" + PTodayTot.ToString() + "','Today');gen1('SB5Cate',arr8,'" + priMTot + "','Month');gen1('ST5brand',arr9,'Retailer Bottom 5 Products','');gen1('SB5brand',arr10,'Retailer Bottom 5 Products','');gen1('ST5Pro',arr11,'Retailer Bottom 5 Products','');gen1('SB5Pro',arr12,'Retailer Bottom 5 Products','');gen2('chartContainer',arr13,'Time');genChart1('ChrtPrimSec',ayy,azz,'PRIMARY VS SECONDARY');}";
        // string scrpt = "ayy=[" + NewChart() + "];azz=[" + NewChart1() + "];arrt=[" + Fillproductiveorder() + "];arr1=[" + FillCategorywiseOrderDay() + "];arr2=[" + FillCategorywiseOrderMonth() + "];   arr3=[" + FillChannelwiseOrderDay() + "];  arr4=[" + FillChannelwiseOrderMonth() + "];   arr5=[" + FillStatewiseOrderMonth() + "];  arr6=[" + FillStatewisePRIOrderMonth() + "];  arr7=[" + PrimaryCategorywiseOrderday() + "];arr8=[" + PrimaryCategorywiseOrderMonth() + "];arr13=[" + In_Time_St() + "];window.onload = function () {gen1('T5Cate',arr1,'" + TodayTot.ToString() + "','Today');gen1('B5Cate',arr2,'" + MonthTot.ToString() + "','Month');gen1('T5Chen',arr3,'" + chDay.ToString() + "','Today');gen1('B5Chen',arr4,'" + chMonth.ToString() + "','Month');getmap('mapMonth',arr5,'0','0','" + div_code + "');getmap('mapMonthPRI',arr6,'0','0','" + div_code + "');gen1('ST5Cate',arr7,'" + PTodayTot.ToString() + "','Today');gen1('SB5Cate',arr8,'" + priMTot + "','Month');gen2('chartContainer',arr13,'Time');genChart1('ChrtPrimSec',ayy,azz,'PRIMARY VS SECONDARY');}";
        /* string scrpt = "";
         scrpt += "ayy=[" + NewChart() + "];";
         scrpt += "azz=[" + NewChart1() + "];";
         scrpt += "arr1=[" + In_Time_St() + "];";
         scrpt += "window.onload = function () {";
         scrpt += "gen2('chartContainer',arr1,'Time');";
         scrpt += "genChart1('ChrtPrimSec',ayy,azz,'PRIMARY VS SECONDARY');";
         scrpt +="}";
         ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);*/

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm1", "console.log('Time 1:" + DateTime.Now + "');", true);
        if (!Page.IsPostBack)
        {

            ViewState["dsSalesForce"] = null;
            ViewState["dsDoctor"] = null;
            DataSet ff = new DataSet();

            ff = viewnoti.countindashboard(div_code, sub_divc);
            if (ff.Tables[0].Rows.Count > 0)
            {
                retailer.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm2", "console.log('Time 2:" + DateTime.Now + "');", true);
            DateTime dateTime1 = DateTime.UtcNow.Date;
			
            string todate1 = dateTime1.ToString("yyyy-MM-dd");
            ff = viewnoti.countinpriorder(div_code, todate1, sub_divc);
            if (ff.Tables[0].Rows.Count > 0)
            {
                ordercount_p.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm3", "console.log('Time 3:" + DateTime.Now + "');", true);
            ff = ss_countinpriorder(div_code, todate1, sub_divc);
            if (ff.Tables[0].Rows.Count > 0)
            {
                lbl_ssp.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm4", "console.log('Time 4:" + DateTime.Now + "');", true);

            ff = viewnoti.countinDist(div_code, sub_divc);
            if (ff.Tables[0].Rows.Count > 0)
            {
                Dist_cou.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm5", "console.log('Time 5:" + DateTime.Now + "');", true);
            //string g= System.DateTime.Now.ToShortDateString();
            DateTime dateTime = DateTime.UtcNow.Date;
			string todate = dateTime.ToString("yyyy-MM-dd");
            DataSet dsGV = new DataSet();
            DataSet dsGc = new DataSet();
            DCR dc = new DCR();
            dsGV = dc.view_total_order_view(div_code, sf_code, todate, sub_divc);
            if (dsGV.Tables[0].Rows.Count > 0)
            {
                ordercount.Text = dsGV.Tables[0].Rows.Count.ToString();

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm6", "console.log('Time 6:" + DateTime.Now + "');", true);
            //superstockist secondary order count
            // dsGV = view_total_order_view(div_code, sf_code, todate, sub_divc);
            // if (dsGV.Tables[0].Rows.Count > 0)
            // {
            //   lbl_sss.Text = dsGV.Tables[0].Rows.Count.ToString();

            // }
            //ordercount.Text = "0";
            DataSet stk = viewnoti.Total_VacantUserdashboard(div_code, sub_divc);
            ff = viewnoti.Total_Userdashboard(div_code, todate, sub_divc);
            string vcn = "0";
            if (stk.Tables[0].Rows.Count > 0)
            {
                vcn = stk.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm7", "console.log('Time 7:" + DateTime.Now + "');", true);
            if (ff.Tables[0].Rows.Count > 0)
            {
                Lbl_Tot_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() +" / "+ vcn;
                //Lbl_Reg_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //Lbl_Lea.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                //Lbl_Oth.Text = "0";
                //Lbl_Inact_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

            }
            ff = viewnoti.Total_Userdashboard_Att(div_code, todate, sub_divc);
            if (ff.Tables[0].Rows.Count > 0)
            {
                //Lbl_Tot_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                Lbl_Reg_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                Lbl_Lea.Text = ff.Tables[0].Rows[2].ItemArray.GetValue(1).ToString();
                Lbl_Oth.Text = ff.Tables[0].Rows[3].ItemArray.GetValue(1).ToString();
                Lbl_Inact_User.Text = ff.Tables[0].Rows[1].ItemArray.GetValue(1).ToString();

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm8", "console.log('Time 8:" + DateTime.Now + "');", true);
            ff = viewnoti.Total_OutLetdashboard(div_code, todate, sub_divc);
            if(ff.Tables.Count>0){
				if (ff.Tables[0].Rows.Count > 0)
            {
                //Lbl_Outlets.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                Lbl_Outlets.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                d2 = Convert.ToDecimal(Lbl_Outlets.Text);
            }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm10", "console.log('Time 9:" + DateTime.Now + "');", true);
            ff = viewnoti.Total_Prodashboard(div_code, todate, sub_divc);
            if (ff.Tables[0].Rows.Count > 0)
            {
                Lbl_Prod.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                Lbl_Vist_Outlet.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                d1 = Convert.ToDecimal(Lbl_Prod.Text);
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm11", "console.log('Time 10:" + DateTime.Now + "');", true);
            decimal a = 0;

            if (d2 > 0) a = d1 / d2 * 100;
            Lbl_Prod_Outlet.Text = Math.Round(decimal.Parse(a.ToString()), 2).ToString() + "%";

            ff = viewnoti.countinRetailersToday(div_code, todate, sub_divc);
            if (ff.Tables[0].Rows.Count > 0)
            {
                Lbl_Sch_Call.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            else
            {
                Lbl_Sch_Call.Text = "0";
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm12", "console.log('Time 11:" + DateTime.Now + "');", true);
            BindData();



        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm13", "console.log('Time 12:" + DateTime.Now + "');", true);
        if (Page.IsPostBack)
        {
            string eventTarget = this.Request["__EVENTTARGET"];
            string eventArgument = this.Request["__EVENTARGUMENT"];
            
            if (eventTarget != string.Empty)
            {
                 dTime = DateTime.ParseExact(eventTarget, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }            

            ViewState["dsSalesForce"] = null;
            ViewState["dsDoctor"] = null;
            DataSet ff = new DataSet();

            //ff = viewnoti.countindashboard(div_code);
            //if (ff.Tables[0].Rows.Count > 0)
            //{
            //    retailer.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            //}
           // DateTime dateTime1 = DateTime.UtcNow.Date;
            string todate1 = dTime.ToString("yyyy-MM-dd");
            ff = viewnoti.countinpriorder(div_code, todate1, sub_divc);
            if (ff.Tables[0].Rows.Count > 0)
            {
                ordercount_p.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            }
            ff = ss_countinpriorder(div_code, todate1, sub_divc);
            if (ff.Tables[0].Rows.Count > 0)
            {
                lbl_ssp.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm14", "console.log('Time 13:" + DateTime.Now + "');", true);
            //ff = viewnoti.countinDist(div_code);
            //if (ff.Tables[0].Rows.Count > 0)
            //{
            //    Dist_cou.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            //}
            //string g= System.DateTime.Now.ToShortDateString();
            // DateTime dateTime = DateTime.UtcNow.Date;
            string todate = dTime.ToString("yyyy-MM-dd");
            DataSet dsGh = new DataSet();
            DataSet dsGc = new DataSet();
            DCR dc = new DCR();
            dsGh = dc.view_total_order_view(div_code, sf_code, todate, sub_divc);
            if (dsGh.Tables[0].Rows.Count > 0)
            {
                ordercount.Text = dsGh.Tables[0].Rows.Count.ToString();

            }
            else
            {
                ordercount.Text = dsGh.Tables[0].Rows.Count.ToString();
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm15", "console.log('Time 14:" + DateTime.Now + "');", true);
            DataSet stk = viewnoti.Total_VacantUserdashboard(div_code,sub_divc);
            ff = viewnoti.Total_Userdashboard(div_code, todate,sub_divc);
            string vcn = "0";
            if (stk.Tables[0].Rows.Count > 0)
            {
                vcn = stk.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm16", "console.log('Time 15:" + DateTime.Now + "');", true);
            if (ff.Tables[0].Rows.Count > 0)
            {
                Lbl_Tot_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + " / " + vcn;
                //Lbl_Reg_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //Lbl_Lea.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                //Lbl_Oth.Text = "0";
                //Lbl_Inact_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

            }
            ff = viewnoti.Total_Userdashboard_Att(div_code, todate,sub_divc);
            if (ff.Tables[0].Rows.Count > 0)
            {
                //Lbl_Tot_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                Lbl_Reg_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                Lbl_Lea.Text = ff.Tables[0].Rows[2].ItemArray.GetValue(1).ToString();
                Lbl_Oth.Text = ff.Tables[0].Rows[3].ItemArray.GetValue(1).ToString();
                Lbl_Inact_User.Text = ff.Tables[0].Rows[1].ItemArray.GetValue(1).ToString();

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm17", "console.log('Time 16:" + DateTime.Now + "');", true);
            ff = viewnoti.Total_OutLetdashboard(div_code, todate, sub_divc);
            if(ff.Tables.Count>0){
			if (ff.Tables[0].Rows.Count > 0)
            {
                //Lbl_Outlets.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                Lbl_Outlets.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                d2 = Convert.ToDecimal(Lbl_Outlets.Text);
            }
			}

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm18", "console.log('Time 17:" + DateTime.Now + "');", true);
            ff = viewnoti.Total_Prodashboard(div_code, todate, sub_divc);
            if (ff.Tables[0].Rows.Count > 0)
            {
                Lbl_Prod.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                Lbl_Vist_Outlet.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                d1 = Convert.ToDecimal(Lbl_Prod.Text);
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm19", "console.log('Time 18:" + DateTime.Now + "');", true);
            decimal a = 0;

            if (d2 > 0) a = d1 / d2 * 100;
            Lbl_Prod_Outlet.Text = Math.Round(decimal.Parse(a.ToString()), 2).ToString() + "%";

            ff = viewnoti.countinRetailersToday(div_code, todate, sub_divc);
            if (ff.Tables[0].Rows.Count > 0)
            {
                Lbl_Sch_Call.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            else
            {
                Lbl_Sch_Call.Text = "0";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Runtm20", "console.log('Time 19:" + DateTime.Now + "');", true);

            BindData();            

        }

    }
    protected void BindData()
    {

        DataSet ff = new DataSet();

        ff = viewnoti.Notification_view(div_code);


        if (ff.Tables.Count > 0)
        {
            if (ff.Tables[0].Rows.Count > 0)
            {


                DataList1.DataSource = ff;
                DataList1.DataBind();

            }
        }
    }

    public string Three_Tier()
    {
        string ScriptValues = "";

        string SFCode = dTime.Year.ToString();
        ChartBAL BL = new ChartBAL();
        ChatBO.HeadDatas SFDet = BL.get_SFDetails(SFCode);
        List<ChatBO.MainDatas> Setup = BL.getSetups(div_code, (Convert.ToInt32(dTime.Year) - 2).ToString(), (Convert.ToInt32(dTime.Year)).ToString());
        //ChatBO.HeadDatas SFDet1 = BL.get_Pur_Brands(SFCode);
        List<ChatBO.MainDatas> Setup1 = BL.get_Pur_Bra_item(div_code, (Convert.ToInt32(dTime.Year) - 2).ToString());
        List<ChatBO.MainDatas> Setup2 = BL.get_Pur_Bra_item(div_code, (Convert.ToInt32(dTime.Year) -1 ).ToString());// BL.get_Pur_Prod_item(div_code, SFCode);
        List<ChatBO.MainDatas> Setup3 = BL.get_Pur_Bra_item(div_code, dTime.Year.ToString());  //BL.get_Pur_Prod_itemX(div_code, SFCode);
        ScriptValues += "{\"chart\":" + JsonConvert.SerializeObject(SFDet) + ",\"categories\":[{\"category\":" + JsonConvert.SerializeObject(Setup) + "}],\"dataset\":[{\"seriesname\":\"" + (Convert.ToInt32(dTime.Year) - 2).ToString() + "\",\"data\":" + JsonConvert.SerializeObject(Setup1) + "},{\"seriesname\":\"" + (Convert.ToInt32(dTime.Year) - 1).ToString() + "\",\"data\":" + JsonConvert.SerializeObject(Setup2) + "},{\"seriesname\":\"" + (Convert.ToInt32(dTime.Year)).ToString() + "\",\"data\":" + JsonConvert.SerializeObject(Setup3) + "}]}";
     //   Response.Write(ScriptValues);
        return ScriptValues;
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        string FMonth = dTime.Month.ToString();
        string FYear = dTime.Year.ToString();
        DateTime dateTime = dTime.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string sURL = "MIS Reports/rpt_Total_Order_View.aspx?sf_code=" + sf_code + "&div_code=" + div_code + "&cur_month=" + FMonth + "&cur_year=" + FYear +
               "&Mode=" + "TPMYDayPlan" + "&Sf_Name=" + sf_code + "&Date=" + todate + "&Type=" + sf_type;
        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    }

    protected void Btn_order_P_Click(object sender, EventArgs e)
    {
        string FMonth = dTime.Month.ToString();
        string FYear = dTime.Year.ToString();
        DateTime dateTime = dTime.Date ;
        string todate = dateTime.ToString("yyyy-MM-dd");
if ((div_code == "78") || (div_code == "87") || (div_code == "89")) 
        {
            string sURL = "MIS Reports/rpt_stk_wise.aspx?sf_code=" + sf_code + "&div_code=" + div_code + "&cur_year=" + FYear;                              
            string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
            ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
        }
else
{
        string sURL = "MIS Reports/rpt_Pri_Order_View.aspx?sf_code=" + sf_code + "&div_code=" + div_code + "&cur_month=" + FMonth + "&cur_year=" + FYear +
               "&Mode=" + "TPMYDayPlan" + "&Sf_Name=" + sf_code + "&Date=" + todate + "&Type=" + sf_type;
        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    
}
}

    protected void view_btn_Click(object sender, EventArgs e)
    {
        string FMonth = dTime.Month.ToString();
        string FYear = dTime.Year.ToString();
        DateTime dateTime = dTime.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string sURL = "MasterFiles/Reports/Rpt_My_Day_Plan_View.aspx?sf_code=" + sf_code + "&div_code=" + div_code + "&cur_month=" + FMonth + "&cur_year=" + FYear +
               "&Mode=" + "TP MY Day Plan" + "&Sf_Name=" + sf_code + "&Date=" + todate + "&Type=" + sf_type + "&Designation_code=" + "" + "&Sub_Div=" + sub_divc;
        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    }
    //FillTop5brand
    private string FillTop5brand()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt1 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Gettop5value_Brand(div_code, Year);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount2 = drFF["value"].ToString();
            stCrtDtaPnt1 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
            stCrtDtaPnt1 += Convert.ToString(iTotLstCount2) + "},";
        }
        return stCrtDtaPnt1;
    }

    //FillB5brand
    private string FillB5brand()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt1 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.GetB5value_Brand(div_code, Year);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount2 = drFF["value"].ToString();
            stCrtDtaPnt1 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
            stCrtDtaPnt1 += Convert.ToString(iTotLstCount2) + "},";
        }
        return stCrtDtaPnt1;
    }
    //FillTop5pro
    private string FillTop5pro()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt2 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Gettop5value_Product(div_code, Year);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount3 = drFF["value"].ToString();
            stCrtDtaPnt2 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
            stCrtDtaPnt2 += Convert.ToString(iTotLstCount3) + "},";
        }
        return stCrtDtaPnt2;
    }

    //FillB5pro
    private string FillB5pro()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt2 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.GetB5value_Product(div_code, Year);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount3 = drFF["value"].ToString();
            stCrtDtaPnt2 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
            stCrtDtaPnt2 += Convert.ToString(iTotLstCount3) + "},";
        }
        return stCrtDtaPnt2;
    }

    //saleFillTop5cate
    private string saleFillTop5cate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts1 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop5value_category(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts1 = drFF["value"].ToString();

            stCrtDtaPnts1 += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";
            stCrtDtaPnts1 += Convert.ToString(iTotLstCounts1) + "},";


        }
        return stCrtDtaPnts1;

    }




    //saleFillB5cate
    private string saleFillB5cate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts1 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_GetB5value_category(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts1 = drFF["value"].ToString();

            stCrtDtaPnts1 += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";
            stCrtDtaPnts1 += Convert.ToString(iTotLstCounts1) + "},";


        }
        return stCrtDtaPnts1;

    }
    //saleFillTop5brand
    private string saleFillTop5brand()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts2 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop5value_Brand(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts2 = drFF["value"].ToString();
            stCrtDtaPnts2 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
            stCrtDtaPnts2 += Convert.ToString(iTotLstCounts2) + "},";
        }
        return stCrtDtaPnts2;
    }
    //saleFillB5brand
    private string saleFillB5brand()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts2 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_GetB5value_Brand(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts2 = drFF["value"].ToString();
            stCrtDtaPnts2 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
            stCrtDtaPnts2 += Convert.ToString(iTotLstCounts2) + "},";
        }
        return stCrtDtaPnts2;
    }
    //saleFillTop5pro
    private string saleFillTop5pro()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts3 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop5value_Product(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts3 = drFF["value"].ToString();
            stCrtDtaPnts3 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
            stCrtDtaPnts3 += Convert.ToString(iTotLstCounts3) + "},";
        }
        return stCrtDtaPnts3;
    }

    //saleFillB5pro
    private string saleFillB5pro()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts3 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_GetB5value_Product(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts3 = drFF["value"].ToString();
            stCrtDtaPnts3 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
            stCrtDtaPnts3 += Convert.ToString(iTotLstCounts3) + "},";
        }
        return stCrtDtaPnts3;
    }

    private string In_Time_St()
    {
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string sURL = string.Empty;
        string stCrtDtaPnts3 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.In_Time_Statistics(div_code, todate);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts3 = drFF["value"].ToString();
            stCrtDtaPnts3 += "{label:\"" + drFF["inTime"].ToString() + "\",y:";
            stCrtDtaPnts3 += Convert.ToString(iTotLstCounts3) + "},";
        }
        return stCrtDtaPnts3;
    }


    //FillT5cate
    private string FillT5cate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Gettop5value_category(div_code, Year);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount1 = drFF["value"].ToString();
            Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            stCrtDtaPnt += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";

            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";


        }
        return stCrtDtaPnt;

    }

    //FillB5cate
    private string FillB5cate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.GetB5value_category(div_code, Year);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount1 = drFF["value"].ToString();
            Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            stCrtDtaPnt += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";

            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";


        }
        return stCrtDtaPnt;

    }

    public string NewChart()
    {
        string data = string.Empty;
        stCrtDtaPnt = string.Empty;
        string FMonth = "1";
        string FYear = dTime.Year.ToString();
        string TMonth = "12";
        string TYear = dTime.Year.ToString();

        //int cyear = 2016;
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);


        ViewState["months"] = months;
        ViewState["cmonth"] = cmonth;
        ViewState["cyear"] = cyear;
        months = Convert.ToInt16(ViewState["months"].ToString());
        cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
        cyear = Convert.ToInt16(ViewState["cyear"].ToString());

        if (months >= 0)
        {


            for (int j = 1; j <= months + 1; j++)
            {
                SalesForce sf = new SalesForce();
                string cm = sf.getMonthName(cmonth.ToString()).Substring(0, 3) + "-" + cyear.ToString().Substring(2, 2);
                data += "{label:\"" + Convert.ToString(cm) + "\",y:";
                if (cmonth == 12)
                {
                    sCurrentDate = "01-01-" + (cyear + 1);
                }
                else
                {
                    sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                }
                stCrtDtaPnt += "{label:\"" + Convert.ToString(cm) + "\",y:";
                

                dtCurrent = Convert.ToDateTime(sCurrentDate);
                ilistt = 0; ilistt1 = 0;


                dsDoc = sf.primary_Order_View(div_code, cmonth, cyear, sCurrentDate);
                dsDoc1 = sf.Sec_Order_View(div_code, cmonth, cyear, sCurrentDate);

                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr1 = Convert.ToDecimal(dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0));
                if (dsDoc1.Tables[0].Rows.Count > 0)
                    tot_dr2 = Convert.ToDecimal(dsDoc1.Tables[0].Rows[0].ItemArray.GetValue(0));

                if (tot_dr1 != 0)
                {
                    ilistt += tot_dr1;

                }
                if (tot_dr2 != 0)
                {
                    ilistt1 += tot_dr2;
                }



                stCrtDtaPnt += ilistt + ",s:" + ilistt1 + "},";

                ilistt = 0;

                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }

            }

        }

        return stCrtDtaPnt;


    }





    private string PrimaryCategorywiseOrderMonth()
    {
        PMonthTot = 0;
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        DCR d = new DCR();
        DateTime dateTime = dTime.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string FMonth = DateTime.Now.Month.ToString();
        dsSalesForce = d.view_total_pri_order_view_categorywise_month(div_code, sf_code, Year, FMonth, sub_divc);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount1 = drFF["value"].ToString();
            PMonthTot += Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            stCrtDtaPnt += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";

            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
        }
        priMTot = String.Format(new CultureInfo("en-IN", false), "{0:n}", Convert.ToDouble(PMonthTot));
        return stCrtDtaPnt;

    }

    private string PrimaryCategorywiseOrderday()
    {
        PTodayTot = 0;
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        DCR d = new DCR();
        DateTime dateTime = dTime.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string FMonth = DateTime.Now.Month.ToString();
        dsSalesForce = d.view_total_pri_order_view_categorywise(div_code, sf_code, todate, sub_divc);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount1 = drFF["value"].ToString();
            PTodayTot += Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            stCrtDtaPnt += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";

            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
        }
        return stCrtDtaPnt;
    }

    private string PrimaryBrandOrderday()
    {
        PTodayTot = 0;
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order d = new Order();
        DateTime dateTime = dTime.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string FMonth = DateTime.Now.Month.ToString();
        dsSalesForce = d.PRIGetBrandwiseOrderDay(div_code, sf_code, todate, sub_divc);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount1 = drFF["value"].ToString();
            PTodayTot += Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            stCrtDtaPnt += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y: ";

            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
        }
        return stCrtDtaPnt;
    }

    private string FillCategorywiseOrderMonth()
    {
        MonthTot = 0;
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Product prd = new Product();
        DateTime dateTime = dTime.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string FMonth = DateTime.Now.Month.ToString();
        dsSalesForce = prd.GetCategorywiseOrderMonth(div_code, sf_code, Year, FMonth, sub_divc);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount1 = drFF["value"].ToString();
            MonthTot += Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            stCrtDtaPnt += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";
            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
        }
        return stCrtDtaPnt;
    }

    private string FillCategorywiseOrderDay()
    {
        TodayTot = 0;
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Product prd = new Product();
        DateTime dateTime = dTime.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        dsSalesForce = prd.GetCategorywiseOrderDay(div_code, sf_code, todate, sub_divc);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount1 = drFF["value"].ToString();
            TodayTot += Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            stCrtDtaPnt += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";
            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
        }
        return stCrtDtaPnt;
    }


    private string FillChannelwiseOrderDay()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order ord = new Order();
        DateTime dateTime = dTime.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        dsSalesForce = ord.GetChannewiseOrderDay(div_code, sf_code, todate, sub_divc);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount1 = drFF["value"].ToString();
            chDay += Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            stCrtDtaPnt += "{label:\"" + drFF["Doc_Special_Name"].ToString() + "\",y: ";
            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
        }
        return stCrtDtaPnt;
    }

    private string FillChannelwiseOrderMonth()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;      
        Order ord = new Order();
        DateTime dateTime = dTime.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");      
        string FMonth = DateTime.Now.Month.ToString();
        dsSalesForce = ord.GetChannewiseOrderMonth(div_code, sf_code, Year, FMonth, sub_divc);
       // dsSalesForce = ord.GetChannewiseOrderDay(div_code, sf_code, todate, "0");
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount1 = drFF["value"].ToString();
            chMonth += Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            stCrtDtaPnt += "{label:\"" + drFF["Doc_Special_Name"].ToString() + "\",y: ";
            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
        }
        return stCrtDtaPnt;
    }

    private string FillBrandwiseOrderDay()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order ord = new Order();
        DateTime dateTime = dTime.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        dsSalesForce = ord.GetBrandwiseOrderDay(div_code, sf_code, todate, "0");
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount1 = drFF["value"].ToString();
            brDay += Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            stCrtDtaPnt += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y: ";
            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
        }
        return stCrtDtaPnt;
    }

    private string FillBrandwiseOrderMonth()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty; Order ord = new Order();
        DateTime dateTime = dTime.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string FMonth = DateTime.Now.Month.ToString();
        dsSalesForce = ord.GetBrandwiseOrderMonth(div_code, sf_code, Year, FMonth, sub_divc);
        // dsSalesForce = ord.GetChannewiseOrderDay(div_code, sf_code, todate, "0");
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount1 = drFF["value"].ToString();
            brMonth += Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            stCrtDtaPnt += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y: ";
            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
        }
        return stCrtDtaPnt;
    }

    private string FillStatewiseOrderMonth()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order ord = new Order();
        DateTime dateTime = dTime.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string FMonth = DateTime.Now.Month.ToString();
        DataSet dsSalesMOnth = ord.GetStatewiseOrderMonth(div_code, "admin", Year, FMonth, sub_divc);
        DataSet dsSalesDay = ord.GetStatewiseOrderDay(div_code, "admin", todate, sub_divc);
        foreach (DataRow drFF in dsSalesMOnth.Tables[0].Rows)
        {




            DataRow[] drs = dsSalesDay.Tables[0].Select("StateName='" + drFF["StateName"].ToString() + "'");

            string tmp = "0";
            if (drs.Length > 0)
            {
                tmp = drs[0][0].ToString();

            }           
            stCrtDtaPnt += "{label:\"" + drFF["ShortName"].ToString() + "\",x:\"" + drFF["value"].ToString() + "\",y:";
            stCrtDtaPnt += Convert.ToString(tmp) + "},";
        }
        stCrtDtaPnt.TrimEnd(',');        
        return stCrtDtaPnt;
    }

    private string FillStatewisePRIOrderMonth()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order ord = new Order();
        DateTime dateTime = dTime.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string FMonth = DateTime.Now.Month.ToString();
        DataSet dsSalesMOnth = ord.PriGetStatewiseOrderMonth(div_code, "admin", Year, FMonth, sub_divc);
        DataSet dsSalesDay = ord.PriGetStatewiseOrderDay(div_code, "admin", todate, sub_divc);
        foreach (DataRow drFF in dsSalesMOnth.Tables[0].Rows)
        {




            DataRow[] drs = dsSalesDay.Tables[0].Select("StateName='" + drFF["StateName"].ToString() + "'");

            string tmp = "0";
            if (drs.Length > 0)
            {
                tmp = drs[0][0].ToString();

            }
            stCrtDtaPnt += "{label:\"" + drFF["ShortName"].ToString() + "\",x:\"" + drFF["value"].ToString() + "\",y:";
            stCrtDtaPnt += Convert.ToString(tmp) + "},";
        }
        stCrtDtaPnt.TrimEnd(',');
        return stCrtDtaPnt;
    }


    public string NewChart1()
    {
        string data = string.Empty;
        stCrtDtaPnt = string.Empty;
        string FMonth = "1";
        string FYear = dTime.Year.ToString();
        string TMonth = "12";
        string TYear = dTime.Year.ToString();

        //int cyear = 2016;
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);


        ViewState["months"] = months;
        ViewState["cmonth"] = cmonth;
        ViewState["cyear"] = cyear;
        months = Convert.ToInt16(ViewState["months"].ToString());
        cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
        cyear = Convert.ToInt16(ViewState["cyear"].ToString());

        if (months >= 0)
        {


            for (int j = 1; j <= months + 1; j++)
            {
                SalesForce sf = new SalesForce();
                string cm = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                data += "{label:\"" + Convert.ToString(cm) + "\",y:";
                if (cmonth == 12)
                {
                    sCurrentDate = "01-01-" + (cyear + 1);
                }
                else
                {
                    sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                }
                stCrtDtaPnt += "{label:\"" + Convert.ToString(cm) + "\",y:";

                dtCurrent = Convert.ToDateTime(sCurrentDate);
                ilistt = 0;

                dsDoc = sf.secondary_Purchase_Distributor(div_code, cmonth, cyear, sCurrentDate);


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr1 = Convert.ToDecimal(dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0));

                if (tot_dr1 != 0)
                {
                    ilistt += tot_dr1;

                }


                data += ilistt + "},";

                ilistt = 0;

                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }

            }

        }

        return data;


    }
    private string Fillproductiveorder()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt1 = string.Empty;
        Notice nt = new Notice();

        DateTime dateTime = dTime.Date;
        string datep = dateTime.ToString("yyyy-MM-dd");
        dsSalesForce = nt.get_call_count(div_code, datep);

        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount2 = drFF["tot_cals"].ToString();
            iTotLstCount1 = drFF["call_type"].ToString();
            //stCrtDtaPnt1 += "{label:\"" + drFF["call_type"].ToString() + "\",z:";
            stCrtDtaPnt1 += "{label:\"" + drFF["call_type"].ToString() + "\",y:";
            //stCrtDtaPnt1 += "{label:\"" + drFF["call_type"].ToString() + "\",z:";
            stCrtDtaPnt1 += Convert.ToString(iTotLstCount2) + ",";
            stCrtDtaPnt1 += "legendText:\"" + drFF["call_type"].ToString() + "\",";

            if (drFF["call_type"].ToString() == "Productive")
            {

                stCrtDtaPnt1 += "\"color\":\"#DCE775\"},";
            }
            else
            {
                stCrtDtaPnt1 += "\"color\":\"#81D4FA\"},";
            }
        }
        return stCrtDtaPnt1;

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
          server control at run time. */
    }
    protected void Item_Bound(Object sender, DataListItemEventArgs e)
    {
        foreach (DataListItem item in DataList1.Items)
        {

            day = (item.FindControl("daytime") as Label).Text;
            type = (item.FindControl("cmttype") as Label).Text;
            comment = (item.FindControl("lblInput") as Label).Text;
            if (type == "News")
            {
                item.BackColor = System.Drawing.Color.LightYellow;
            }
            if (type == "Wishes")
            {
                item.BackColor = System.Drawing.Color.FromName("#d9edf7");
            }
            if (type == "Messages")
            {
                item.BackColor = System.Drawing.Color.FromName("#fad5d5");
            }
            if (type == "Important")
            {
                item.BackColor = System.Drawing.Color.FromName("#cffabd");
            }


        }

    }



    public class ChartsValues
    {
        public string Title { get; set; }
        public string Tot { get; set; }
        public string Values { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static List<ChartsValues> GetCatSecondOrderDay(string cdate)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Product prd = new Product();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = cdate;
        DataSet dsCatOrderSecDay =new DataSet();
		dsCatOrderSecDay = prd.GetCategorywiseOrderDay(DDiv_code, "admin", todate, sub_divc);
      //  stCrtDtaPnt = "[";
        decimal chtot = 0;
        //foreach (DataRow drFF in dsCatOrderSecDay.Tables[0].Rows)
        //{
        //    string orderVal = drFF["value"].ToString();
        //    chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
        //    Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
        //    stCrtDtaPnt += "{\"label\":\"" + drFF["Product_Cat_Name"].ToString() + "\",\"y\": ";
        //    stCrtDtaPnt += Convert.ToString(orderVal) + "},";
        //}
        //stCrtDtaPnt = stCrtDtaPnt.TrimEnd(',');
        //stCrtDtaPnt += "]";





        StringBuilder sb = new StringBuilder();
        sb.Append("[");



        for (int i = 0; i < dsCatOrderSecDay.Tables[0].Rows.Count; i++)
        {
            string orderVal = dsCatOrderSecDay.Tables[0].Rows[i]["value"].ToString();
            chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            sb.Append("{\"label\":\"" + dsCatOrderSecDay.Tables[0].Rows[i]["Product_Cat_Name"].ToString() + "\",\"y\": ");
            sb.Append(Convert.ToString(orderVal) + "}");
            if (i != dsCatOrderSecDay.Tables[0].Rows.Count - 1)
            {
                sb.Append(",");
            }
        }
        sb.Append("]");




        List<ChartsValues> CV = new List<ChartsValues>();
        ChartsValues cv = new ChartsValues();
        cv.Title = "ToDay";
        cv.Tot = chtot.ToString();
        cv.Values = sb.ToString();
        CV.Add(cv);

        return CV;
    }

    [WebMethod(EnableSession = true)]
    public static List<ChartsValues> GetCatSecondOrderMonth(string cyear, string cmonth)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Product prd = new Product();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string FMonth = cmonth;//DateTime.Now.Month.ToString();
        string FYear = cyear;//DateTime.Now.Year.ToString();
        DataSet dsCatOrderSecDay = new DataSet();
		dsCatOrderSecDay=prd.GetCategorywiseOrderMonth(DDiv_code, "admin", FYear, FMonth, sub_divc);
       // stCrtDtaPnt = "[";
        decimal chtot = 0;
        //foreach (DataRow drFF in dsCatOrderSecDay.Tables[0].Rows)
        //{
        //    string orderVal = drFF["value"].ToString();
        //    chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
        //    Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
        //    stCrtDtaPnt += "{\"label\":\"" + drFF["Product_Cat_Name"].ToString() + "\",\"y\": ";
        //    stCrtDtaPnt += Convert.ToString(orderVal) + "},";
        //}
        //stCrtDtaPnt = stCrtDtaPnt.TrimEnd(',');
        //stCrtDtaPnt += "]";




        StringBuilder sb = new StringBuilder();
        sb.Append("[");



        for (int i = 0; i < dsCatOrderSecDay.Tables[0].Rows.Count; i++)
        {
            string orderVal = dsCatOrderSecDay.Tables[0].Rows[i]["value"].ToString();
            chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            sb.Append("{\"label\":\"" + dsCatOrderSecDay.Tables[0].Rows[i]["Product_Cat_Name"].ToString() + "\",\"y\": ");
            sb.Append(Convert.ToString(orderVal) + "}");
            if (i != dsCatOrderSecDay.Tables[0].Rows.Count - 1)
            {
                sb.Append(",");
            }
        }
        sb.Append("]");




        List<ChartsValues> CV = new List<ChartsValues>();
        ChartsValues cv = new ChartsValues();
        cv.Title = "Month";
        cv.Tot = chtot.ToString();
        cv.Values = sb.ToString();
        CV.Add(cv);

        return CV;
    }


    [WebMethod(EnableSession = true)]
    public static List<ChartsValues> GetChanelSecondOrderDay(string cdate)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order ord = new Order();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = cdate;
        DataSet dsCatOrderSecDay=new DataSet(); 
		dsCatOrderSecDay= ord.GetChannewiseOrderDay(DDiv_code, "admin", todate, sub_divc);
        //stCrtDtaPnt = "[";
        //decimal chtot = 0;


        //foreach (DataRow drFF in dsCatOrderSecDay.Tables[0].Rows)
        //{
        //    string orderVal = drFF["value"].ToString();
        //    chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
        //    Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
        //    stCrtDtaPnt += "{\"label\":\"" + drFF["Doc_Special_Name"].ToString() + "\",\"y\": ";
        //    stCrtDtaPnt += Convert.ToString(orderVal) + "},";
        //}
        //stCrtDtaPnt = stCrtDtaPnt.TrimEnd(',');
        //stCrtDtaPnt += "]";
        decimal chtot = 0;
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        


        for (int i = 0; i < dsCatOrderSecDay.Tables[0].Rows.Count; i++)
        {
            string orderVal = dsCatOrderSecDay.Tables[0].Rows[i]["value"].ToString();
            chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            sb.Append("{\"label\":\"" + dsCatOrderSecDay.Tables[0].Rows[i]["Doc_Special_Name"].ToString() + "\",\"y\": ");
            sb.Append(Convert.ToString(orderVal) + "}");
            if (i != dsCatOrderSecDay.Tables[0].Rows.Count - 1)
            {
                sb.Append(",");
            }
        }
        sb.Append("]");





        List<ChartsValues> CV = new List<ChartsValues>();
        ChartsValues cv = new ChartsValues();
        cv.Title = "ToDay";
        cv.Tot = chtot.ToString();
        cv.Values = sb.ToString();
        CV.Add(cv);

        return CV;
    }

    [WebMethod(EnableSession = true)]
    public static List<ChartsValues> GetChanelSecondOrderMonth(string cyear, string cmonth)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order ord = new Order();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string FMonth = cmonth;// DateTime.Now.Month.ToString();
        string FYear = cyear;//DateTime.Now.Year.ToString();
        DataSet dsCatOrderSecDay=new DataSet(); 
		dsCatOrderSecDay = ord.GetChannewiseOrderMonth(DDiv_code, "admin", FYear, FMonth, sub_divc);
        stCrtDtaPnt = "[";
        decimal chtot = 0;
        foreach (DataRow drFF in dsCatOrderSecDay.Tables[0].Rows)
        {
            string orderVal = drFF["value"].ToString();
            chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            stCrtDtaPnt += "{\"label\":\"" + drFF["Doc_Special_Name"].ToString() + "\",\"y\": ";
            stCrtDtaPnt += Convert.ToString(orderVal) + "},";
        }
        stCrtDtaPnt = stCrtDtaPnt.TrimEnd(',');
        stCrtDtaPnt += "]";

        List<ChartsValues> CV = new List<ChartsValues>();
        ChartsValues cv = new ChartsValues();
        cv.Title = "Month";
        cv.Tot = chtot.ToString();
        cv.Values = stCrtDtaPnt;
        CV.Add(cv);

        return CV;
    }



    [WebMethod(EnableSession = true)]
    public static List<ChartsValues> GetStateSecondOrder(string cdate, string cyear, string cmonth)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order ord = new Order();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = cdate;
        string FMonth = cmonth;
        string FYear = cyear;
        DataSet dsSalesMOnth = ord.GetStatewiseOrderMonth(DDiv_code, "admin", FYear, FMonth, sub_divc);
        DataSet dsSalesDay = ord.GetStatewiseOrderDay(DDiv_code, "admin", todate, sub_divc);
        //stCrtDtaPnt = "[";
        //foreach (DataRow drFF in dsSalesMOnth.Tables[0].Rows)
        //{
        //    DataRow[] drs = dsSalesDay.Tables[0].Select("StateName='" + drFF["StateName"].ToString() + "'");

        //    string tmp = "0";
        //    if (drs.Length > 0)
        //    {
        //        tmp = drs[0][0].ToString();

        //    }
        //    stCrtDtaPnt += "{\"label\":\"" + drFF["ShortName"].ToString() + "\",\"value\":" + drFF["value"].ToString() + ",\"y\":" + tmp + "},";
        //    // stCrtDtaPnt += Convert.ToString(tmp) + "\"},";
        //}
        //stCrtDtaPnt = stCrtDtaPnt.TrimEnd(',');
        //stCrtDtaPnt += "]";


        StringBuilder sb = new StringBuilder();
        sb.Append("[");

        for (int i = 0; i < dsSalesMOnth.Tables[0].Rows.Count; i++)
        {
            DataRow[] drs = dsSalesDay.Tables[0].Select("StateName='" + dsSalesMOnth.Tables[0].Rows[i]["StateName"].ToString() + "'");

            string tmp = "0";
            if (drs.Length > 0)
            {
                tmp = drs[0][0].ToString();
            }
            sb.Append("{\"label\":\"" + dsSalesMOnth.Tables[0].Rows[i]["ShortName"].ToString() + "\",\"value\":" + dsSalesMOnth.Tables[0].Rows[i]["value"].ToString() + ",\"y\":" + tmp + "}");
            if (i != dsSalesMOnth.Tables[0].Rows.Count-1)
            {
                sb.Append(",");
            }
        }
        sb.Append("]");
        // stCrtDtaPnt += "]";
        List<ChartsValues> CV = new List<ChartsValues>();
        ChartsValues cv = new ChartsValues();
        //cv.Title = "Month";
        //cv.Tot = chtot.ToString();
        cv.Values = sb.ToString();
        CV.Add(cv);
        return CV;
    }


    ////// tab Tow Functions 

    [WebMethod(EnableSession = true)]
    public static List<ChartsValues> GetCatPrimaryOrderDay(string cdate)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        DCR d = new DCR();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = cdate;// dateTime.ToString("yyyy-MM-dd");
        string FMonth = DateTime.Now.Month.ToString();
        DataSet dsCatOrderSecDay = d.view_total_pri_order_view_categorywise(DDiv_code, "admin", todate, sub_divc);      
        stCrtDtaPnt = "[";
        decimal chtot = 0;
        foreach (DataRow drFF in dsCatOrderSecDay.Tables[0].Rows)
        {
            string orderVal = drFF["value"].ToString();
            chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            stCrtDtaPnt += "{\"label\":\"" + drFF["Product_Cat_Name"].ToString() + "\",\"y\": ";
            stCrtDtaPnt += Convert.ToString(orderVal) + "},";
        }
        stCrtDtaPnt = stCrtDtaPnt.TrimEnd(',');
        stCrtDtaPnt += "]";

        List<ChartsValues> CV = new List<ChartsValues>();
        ChartsValues cv = new ChartsValues();
        cv.Title = "ToDay";
        cv.Tot = chtot.ToString();
        cv.Values = stCrtDtaPnt;
        CV.Add(cv);

        return CV;
    }


    [WebMethod(EnableSession = true)]
    public static List<ChartsValues> GetCatPrimaryOrderMonth(string cyear, string cmonth)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;


        DCR d = new DCR();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string FMonth = cmonth; // DateTime.Now.Month.ToString();
        string FYear = cyear;// DateTime.Now.Year.ToString();
        DataSet dsCatOrderSecDay = d.view_total_pri_order_view_categorywise_month(DDiv_code, "admin", FYear, FMonth, sub_divc);


      
        stCrtDtaPnt = "[";
        decimal chtot = 0;
        foreach (DataRow drFF in dsCatOrderSecDay.Tables[0].Rows)
        {
            string orderVal = drFF["value"].ToString();
            chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            stCrtDtaPnt += "{\"label\":\"" + drFF["Product_Cat_Name"].ToString() + "\",\"y\": ";
            stCrtDtaPnt += Convert.ToString(orderVal) + "},";
        }
        stCrtDtaPnt = stCrtDtaPnt.TrimEnd(',');
        stCrtDtaPnt += "]";

        List<ChartsValues> CV = new List<ChartsValues>();
        ChartsValues cv = new ChartsValues();
        cv.Title = "Month";
        cv.Tot = chtot.ToString("0.00");
        cv.Values = stCrtDtaPnt;
        CV.Add(cv);

        return CV;
    }




    [WebMethod(EnableSession = true)]
    public static List<ChartsValues> GetStatePrimaryOrder(string cdate, string cyear, string cmonth)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order ord = new Order();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = cdate;// dateTime.ToString("yyyy-MM-dd");
        string FMonth = cmonth;// DateTime.Now.Month.ToString();
        string FYear = cyear;// DateTime.Now.Year.ToString();
        DataSet dsSalesMOnth = ord.PriGetStatewiseOrderMonth(DDiv_code, "admin", FYear, FMonth, sub_divc);
        DataSet dsSalesDay = ord.PriGetStatewiseOrderDay(DDiv_code, "admin", todate, sub_divc);
        stCrtDtaPnt = "[";
        foreach (DataRow drFF in dsSalesMOnth.Tables[0].Rows)
        {
            DataRow[] drs = dsSalesDay.Tables[0].Select("StateName='" + drFF["StateName"].ToString() + "'");

            string tmp = "0";
            if (drs.Length > 0)
            {
                tmp = drs[0][0].ToString();

            }
            stCrtDtaPnt += "{\"label\":\"" + drFF["ShortName"].ToString() + "\",\"value\":" + drFF["value"].ToString() + ",\"y\":" + tmp + "},";
            // stCrtDtaPnt += Convert.ToString(tmp) + "\"},";
        }
        stCrtDtaPnt = stCrtDtaPnt.TrimEnd(',');
        stCrtDtaPnt += "]";
        // stCrtDtaPnt += "]";
        List<ChartsValues> CV = new List<ChartsValues>();
        ChartsValues cv = new ChartsValues();
        //cv.Title = "Month";
        //cv.Tot = chtot.ToString();
        cv.Values = stCrtDtaPnt;
        CV.Add(cv);
        return CV;
    }


    //// performance 

    [WebMethod(EnableSession = true)]
    public static List<ChartsValues> GetSecProdctOrderYear(string cyear)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order ord = new Order();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string FMonth = DateTime.Now.Month.ToString();
        string FYear = cyear;// DateTime.Now.Year.ToString();
        DataSet dsProOrderSecYear = ord.GetSecProductOrderYear(DDiv_code, "admin", FYear, sub_divc);
        stCrtDtaPnt = "[";
        decimal chtot = 0;
        foreach (DataRow drFF in dsProOrderSecYear.Tables[0].Rows)
        {
            string orderVal = drFF["value"].ToString();
			string quantity = drFF["quantity"].ToString();
            string stcode = drFF["State_Code"].ToString().TrimEnd(',');
            chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            stCrtDtaPnt += "{\"label\":\"" + drFF["Product_Detail_Name"].ToString() + "\",\"y\":";
			stCrtDtaPnt += Convert.ToString(orderVal) + ",\"x\":" + Convert.ToString(quantity) + ",\"state\":\"" + Convert.ToString(stcode) + "\"},";
            //stCrtDtaPnt += Convert.ToString(orderVal) + "},";
        }
        stCrtDtaPnt = stCrtDtaPnt.TrimEnd(',');
        stCrtDtaPnt += "]";

        List<ChartsValues> CV = new List<ChartsValues>();
        ChartsValues cv = new ChartsValues();
        cv.Title = "Month";
        cv.Tot = chtot.ToString();
        cv.Values = stCrtDtaPnt;
        CV.Add(cv);

        return CV;
    }

    [WebMethod(EnableSession = true)]
    public static List<ChartsValues> GetSecProdctOrderMonth(string cyear, string cmonth)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order ord = new Order();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string FMonth = cmonth;// DateTime.Now.Month.ToString();
        string FYear = cyear;// DateTime.Now.Year.ToString();
        DataSet dsProOrderSecYear = ord.GetSecProductOrderMonth(DDiv_code, "admin", FYear, FMonth, sub_divc);
        stCrtDtaPnt = "[";
        decimal chtot = 0;
        foreach (DataRow drFF in dsProOrderSecYear.Tables[0].Rows)
        {
            string orderVal = drFF["value"].ToString();
			string quantity = drFF["quantity"].ToString();
            string stcode = drFF["State_Code"].ToString().TrimEnd(',');
            chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            stCrtDtaPnt += "{\"label\":\"" + drFF["Product_Detail_Name"].ToString() + "\",\"y\":";
			stCrtDtaPnt += Convert.ToString(orderVal) + ",\"x\":" + Convert.ToString(quantity) + ",\"state\":\"" + Convert.ToString(stcode) + "\"},";
            //stCrtDtaPnt += Convert.ToString(orderVal) + "},";
        }
        stCrtDtaPnt = stCrtDtaPnt.TrimEnd(',');
        stCrtDtaPnt += "]";

        List<ChartsValues> CV = new List<ChartsValues>();
        ChartsValues cv = new ChartsValues();
        cv.Title = "Month";
        cv.Tot = chtot.ToString();
        cv.Values = stCrtDtaPnt;
        CV.Add(cv);

        return CV;
    }

    [WebMethod(EnableSession = true)]
    public static List<ChartsValues> GetSecProdctOrderDay(string cdate)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order ord = new Order();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = cdate;// dateTime.ToString("yyyy-MM-dd");
        string FMonth = DateTime.Now.Month.ToString();
        string FYear = DateTime.Now.Year.ToString();
        DataSet dsProOrderSecYear = ord.GetSecProductOrderDay(DDiv_code, "admin", todate, sub_divc);
        stCrtDtaPnt = "[";
        decimal chtot = 0;
        foreach (DataRow drFF in dsProOrderSecYear.Tables[0].Rows)
        {
            string orderVal = drFF["value"].ToString();
			string quantity = drFF["quantity"].ToString();
            string stcode = drFF["State_Code"].ToString().TrimEnd(',');
            chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            stCrtDtaPnt += "{\"label\":\"" + drFF["Product_Detail_Name"].ToString() + "\",\"y\":";
            //stCrtDtaPnt += Convert.ToString(orderVal) + "},";
			stCrtDtaPnt += Convert.ToString(orderVal) + ",\"x\":" + Convert.ToString(quantity) + ",\"state\":\"" + Convert.ToString(stcode) + "\"},";
        }
        stCrtDtaPnt = stCrtDtaPnt.TrimEnd(',');
        stCrtDtaPnt += "]";

        List<ChartsValues> CV = new List<ChartsValues>();
        ChartsValues cv = new ChartsValues();
        cv.Title = "Month";
        cv.Tot = chtot.ToString();
        cv.Values = stCrtDtaPnt;
        CV.Add(cv);

        return CV;
    }




    [WebMethod(EnableSession = true)]
    public static List<ChartsValues> GetPRIProdctOrderYear(string cyear)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order ord = new Order();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string FMonth = DateTime.Now.Month.ToString();
        string FYear = cyear;// DateTime.Now.Year.ToString();
        DataSet dsProOrderSecYear = ord.GetPRIProductOrderYear(DDiv_code, "admin", FYear, sub_divc);
        stCrtDtaPnt = "[";
        decimal chtot = 0;
        foreach (DataRow drFF in dsProOrderSecYear.Tables[0].Rows)
        {
            string orderVal = drFF["value"].ToString();
			string quantity = drFF["quantity"].ToString();
            string stcode= drFF["State_Code"].ToString().TrimEnd(',');
            chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            stCrtDtaPnt += "{\"label\":\"" + drFF["Product_Detail_Name"].ToString() + "\",\"y\":";
			stCrtDtaPnt += Convert.ToString(orderVal) + " ,\"x\":" + Convert.ToString(quantity) + ",\"state\":\"" + Convert.ToString(stcode) + "\"},";
            //stCrtDtaPnt += Convert.ToString(orderVal) + "},";
        }
        stCrtDtaPnt = stCrtDtaPnt.TrimEnd(',');
        stCrtDtaPnt += "]";

        List<ChartsValues> CV = new List<ChartsValues>();
        ChartsValues cv = new ChartsValues();
        cv.Title = "Month";
        cv.Tot = chtot.ToString();
        cv.Values = stCrtDtaPnt;
        CV.Add(cv);

        return CV;
    }

    [WebMethod(EnableSession = true)]
    public static List<ChartsValues> GetPRIProdctOrderMonth(string cyear, string cmonth)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order ord = new Order();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string FMonth =cmonth;// DateTime.Now.Month.ToString();
        string FYear = cyear;// DateTime.Now.Year.ToString();
        DataSet dsProOrderSecYear = ord.GetPRIProductOrderMonth(DDiv_code, "admin", FYear, FMonth, sub_divc);
        stCrtDtaPnt = "[";
        decimal chtot = 0;
        foreach (DataRow drFF in dsProOrderSecYear.Tables[0].Rows)
        {
            string orderVal = drFF["value"].ToString();
			string quantity = drFF["quantity"].ToString();
            string stcode = drFF["State_Code"].ToString().TrimEnd(',');
            chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            stCrtDtaPnt += "{\"label\":\"" + drFF["Product_Detail_Name"].ToString() + "\",\"y\":";
			stCrtDtaPnt += Convert.ToString(orderVal) + ",\"x\":" + Convert.ToString(quantity) + ",\"state\":\"" + Convert.ToString(stcode) + "\"},";
            //stCrtDtaPnt += Convert.ToString(orderVal) + "},";
        }
        stCrtDtaPnt = stCrtDtaPnt.TrimEnd(',');
        stCrtDtaPnt += "]";

        List<ChartsValues> CV = new List<ChartsValues>();
        ChartsValues cv = new ChartsValues();
        cv.Title = "Month";
        cv.Tot = chtot.ToString();
        cv.Values = stCrtDtaPnt;
        CV.Add(cv);

        return CV;
    }

    [WebMethod(EnableSession = true)]
    public static List<ChartsValues> GetPRiProdctOrderDay(string cdate)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order ord = new Order();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = cdate;// dateTime.ToString("yyyy-MM-dd");
        string FMonth = DateTime.Now.Month.ToString();
        string FYear = DateTime.Now.Year.ToString();
        DataSet dsProOrderSecYear = ord.GetPRIProductOrderDay(DDiv_code, "admin", todate, sub_divc);
        stCrtDtaPnt = "[";
        decimal chtot = 0;
        foreach (DataRow drFF in dsProOrderSecYear.Tables[0].Rows)
        {
            string orderVal = drFF["value"].ToString();
			string quantity = drFF["quantity"].ToString();
            string stcode = drFF["State_Code"].ToString().TrimEnd(',');
            chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            stCrtDtaPnt += "{\"label\":\"" + drFF["Product_Detail_Name"].ToString() + "\",\"y\":";
			stCrtDtaPnt += Convert.ToString(orderVal) + ",\"x\":" + Convert.ToString(quantity) + ",\"state\":\"" + Convert.ToString(stcode) + "\"},";
            //stCrtDtaPnt += Convert.ToString(orderVal) + "},";
        }
        stCrtDtaPnt = stCrtDtaPnt.TrimEnd(',');
        stCrtDtaPnt += "]";

        List<ChartsValues> CV = new List<ChartsValues>();
        ChartsValues cv = new ChartsValues();
        cv.Title = "Month";
        cv.Tot = chtot.ToString();
        cv.Values = stCrtDtaPnt;
        CV.Add(cv);

        return CV;
    }
	 [WebMethod(EnableSession = true)]
    public static List<ChartsValues> GetpgroupDay(string cdate)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order ord = new Order();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = cdate;
        DataSet dsCatOrderSecDay = ord.GetpgroupOrderDay(DDiv_code, "admin", todate);
        decimal chtot = 0;
        StringBuilder sb = new StringBuilder();
        sb.Append("[");



        for (int i = 0; i < dsCatOrderSecDay.Tables[0].Rows.Count; i++)
        {
            string orderVal = dsCatOrderSecDay.Tables[0].Rows[i]["value"].ToString();
            chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            sb.Append("{\"label\":\"" + dsCatOrderSecDay.Tables[0].Rows[i]["Product_Grp_Name"].ToString() + "\",\"y\": ");
            sb.Append(Convert.ToString(orderVal) + "}");
            if (i != dsCatOrderSecDay.Tables[0].Rows.Count - 1)
            {
                sb.Append(",");
            }
        }
        sb.Append("]");





        List<ChartsValues> CV = new List<ChartsValues>();
        ChartsValues cv = new ChartsValues();
        cv.Title = "ToDay";
        cv.Tot = chtot.ToString();
        cv.Values = sb.ToString();
        CV.Add(cv);

        return CV;
    }

    [WebMethod(EnableSession = true)]
    public static List<ChartsValues> GetpgroupMonth(string cyear, string cmonth)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        Order ord = new Order();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string FMonth = cmonth;// DateTime.Now.Month.ToString();
        string FYear = cyear;//DateTime.Now.Year.ToString();
        DataSet dsCatOrderSecDay = ord.GepgrpOrderMonth(DDiv_code, "admin", FYear, FMonth);
        stCrtDtaPnt = "[";
        decimal chtot = 0;
        foreach (DataRow drFF in dsCatOrderSecDay.Tables[0].Rows)
        {
            string orderVal = drFF["value"].ToString();
            chtot += Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            Convert.ToDecimal(orderVal = string.IsNullOrEmpty(orderVal) ? "0" : orderVal);
            stCrtDtaPnt += "{\"label\":\"" + drFF["Product_Grp_Name"].ToString() + "\",\"y\": ";
            stCrtDtaPnt += Convert.ToString(orderVal) + "},";
        }
        stCrtDtaPnt = stCrtDtaPnt.TrimEnd(',');
        stCrtDtaPnt += "]";

        List<ChartsValues> CV = new List<ChartsValues>();
        ChartsValues cv = new ChartsValues();
        cv.Title = "Month";
        cv.Tot = chtot.ToString();
        cv.Values = stCrtDtaPnt;
        CV.Add(cv);

        return CV;
    }

    public class State
    {
        public string stateName { get; set; }
        public string StateCode { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static State[] GetState()
    {
        string DDiv_code = HttpContext.Current.Session["division_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string DSub_DivCode = "0";
        List<State> distributor = new List<State>();
        DataSet dsDistributor = null;
        Bus_EReport.Stockist stk = new Bus_EReport.Stockist();
        dsDistributor = stk.GetState_subdivisionwise(divcode: DDiv_code.TrimEnd(','), subdivision: sub_divc, sf_code: DSf_Code);
        if (dsDistributor.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in dsDistributor.Tables[0].Rows)
            {
                State dis = new State();
                dis.StateCode = row["state_code"].ToString();
                dis.stateName = row["statename"].ToString();
                distributor.Add(dis);
            }
        }
        return distributor.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static string GetDataD(string State_Code, string State_name)
    {
        string div_code = "";
        string sf_Code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        sf_Code = HttpContext.Current.Session["SF_Code"].ToString();

        string st_code = State_Code;
        string st_Name = State_name;
        List<GetDatasD> empListD = new List<GetDatasD>();
        string ScriptValues = "";
        string SFCode = DateTime.Now.Year.ToString();
        ChartBAL BL = new ChartBAL();
        ChatBO.HeadDatas SFDet = BL.get_SFDetails1(st_Name);
        List<ChatBO.MainDatas> Setups = BL.getSetups1(div_code, (Convert.ToInt32(DateTime.Now.Year) - 2).ToString(), DateTime.Now.Year.ToString(), st_code);
        List<ChatBO.MainDatas> Setup1 = BL.get_Pur_Bra_item1(div_code, (Convert.ToInt32(DateTime.Now.Year) - 2).ToString(), st_code);
        List<ChatBO.MainDatas> Setup2 = BL.get_Pur_Bra_item1(div_code, (Convert.ToInt32(DateTime.Now.Year) - 1).ToString(), st_code); ;// BL.get_Pur_Prod_item1(div_code, SFCode, st_code);
        List<ChatBO.MainDatas> Setup3 = BL.get_Pur_Bra_item1(div_code, (Convert.ToInt32(DateTime.Now.Year)).ToString(), st_code); ;//  BL.get_Pur_Prod_itemX1(div_code, SFCode, st_code);

        ScriptValues += "{\"chart\":" + JsonConvert.SerializeObject(SFDet) + ",\"categories\":[{\"category\":" + JsonConvert.SerializeObject(Setups) + "}],\"dataset\":[{\"seriesname\":\"" + (Convert.ToInt32(DateTime.Now.Year) - 2).ToString() + "\",\"data\":" + JsonConvert.SerializeObject(Setup1) + "},{\"seriesname\":\"" + (Convert.ToInt32(DateTime.Now.Year) - 1).ToString() + "\",\"data\":" + JsonConvert.SerializeObject(Setup2) + "},{\"seriesname\":\"" + (Convert.ToInt32(DateTime.Now.Year)).ToString() + "\",\"data\":" + JsonConvert.SerializeObject(Setup3) + "}]}";
        //ScriptValues += "{\n  \"chart\": {\n    \"caption\": \"State wise Achievement For Last 3 Years – "+ st_Name + "\",\n    \"subcaption\": \"Licel / Nok / Aerosols\",\n    \"yaxisname\": \"Count\",\n    \"numvisibleplot\": \"8\",\n    \"labeldisplay\": \"auto\",\n    \"palettecolors\": \"#428bca,f2726f,#5cb85c\",\n    \"theme\": \"zune\"\n  },\n  \"categories\": [\n    {\n      \"category\": [\n        {\n          \"label\": \"USA\"\n        },\n        {\n          \"label\": \"GB\"\n        },\n        {\n          \"label\": \"China\"\n        },\n        {\n          \"label\": \"Russia\"\n        },\n        {\n          \"label\": \"Germany\"\n        },\n        {\n          \"label\": \"France\"\n        },\n        {\n          \"label\": \"Japan\"\n        },\n        {\n          \"label\": \"Australia\"\n        },\n        {\n          \"label\": \"Italy\"\n        },\n        {\n          \"label\": \"Canada\"\n        },\n        {\n          \"label\": \"South Korea\"\n        },\n        {\n          \"label\": \"Netherlands\"\n        },\n        {\n          \"label\": \"Brazil\"\n        },\n        {\n          \"label\": \"NZ\"\n        },\n        {\n          \"label\": \"Spain\"\n        },\n        {\n          \"label\": \"Hungary\"\n        },\n        {\n          \"label\": \"Kenya\"\n        },\n        {\n          \"label\": \"Jamaica\"\n        },\n        {\n          \"label\": \"Cuba\"\n        },\n        {\n          \"label\": \"Croatia\"\n        }\n      ]\n    }\n  ],\n  \"dataset\": [\n    {\n      \"seriesname\": \"2016-2017\",\n      \"data\": [\n        {\n          \"value\": \"20\"\n        },\n        {\n          \"value\": \"70\"\n        },\n        {\n          \"value\": \"65\"\n        },\n        {\n          \"value\": \"55\"\n        },\n        {\n          \"value\": \"44\"\n        },\n        {\n          \"value\": \"42\"\n        },\n        {\n          \"value\": \"41\"\n        },\n        {\n          \"value\": \"29\"\n        },\n        {\n          \"value\": \"28\"\n        },\n        {\n          \"value\": \"22\"\n        },\n        {\n          \"value\": \"21\"\n        },\n        {\n          \"value\": \"19\"\n        },\n        {\n          \"value\": \"19\"\n        },\n        {\n          \"value\": \"18\"\n        },\n        {\n          \"value\": \"17\"\n        },\n        {\n          \"value\": \"15\"\n        },\n        {\n          \"value\": \"13\"\n        },\n        {\n          \"value\": \"11\"\n        },\n        {\n          \"value\": \"11\"\n        },\n        {\n          \"value\": \"10\"\n        }\n      ]\n    },\n    {\n      \"seriesname\": \"2017-2018\",\n      \"data\": [\n        {\n          \"value\": \"123\"\n        },\n        {\n          \"value\": \"71\"\n        },\n        {\n          \"value\": \"59\"\n        },\n        {\n          \"value\": \"52\"\n        },\n        {\n          \"value\": \"34\"\n        },\n        {\n          \"value\": \"32\"\n        },\n        {\n          \"value\": \"29\"\n        },\n        {\n          \"value\": \"32\"\n        },\n        {\n          \"value\": \"25\"\n        },\n        {\n          \"value\": \"21\"\n        },\n        {\n          \"value\": \"24\"\n        },\n        {\n          \"value\": \"17\"\n        },\n        {\n          \"value\": \"20\"\n        },\n        {\n          \"value\": \"14\"\n        },\n        {\n          \"value\": \"13\"\n        },\n        {\n          \"value\": \"16\"\n        },\n        {\n          \"value\": \"14\"\n        },\n        {\n          \"value\": \"12\"\n        },\n        {\n          \"value\": \"11\"\n        },\n        {\n          \"value\": \"9\"\n        }\n      ]\n    }\n,{\n      \"seriesname\": \"2018-2019\",\n      \"data\": [\n        {\n          \"value\": \"123\"\n        },\n        {\n          \"value\": \"71\"\n        },\n        {\n          \"value\": \"59\"\n        },\n        {\n          \"value\": \"52\"\n        },\n        {\n          \"value\": \"34\"\n        },\n        {\n          \"value\": \"32\"\n        },\n        {\n          \"value\": \"29\"\n        },\n        {\n          \"value\": \"32\"\n        },\n        {\n          \"value\": \"25\"\n        },\n        {\n          \"value\": \"21\"\n        },\n        {\n          \"value\": \"24\"\n        },\n        {\n          \"value\": \"17\"\n        },\n        {\n          \"value\": \"20\"\n        },\n        {\n          \"value\": \"14\"\n        },\n        {\n          \"value\": \"13\"\n        },\n        {\n          \"value\": \"16\"\n        },\n        {\n          \"value\": \"14\"\n        },\n        {\n          \"value\": \"12\"\n        },\n        {\n          \"value\": \"11\"\n        },\n        {\n          \"value\": \"9\"\n        }\n      ]\n    }\n  ]\n}";
        
        GetDatasD emp = new GetDatasD();
        return ScriptValues;



    }

    public class GetDatasD
    {

        public string Cval { get; set; }

    }

    //zone
    [WebMethod(EnableSession = true)]
    public static string GetDataD1(string State_Code, string State_name)
    {
        string div_code = "";
        string sf_Code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        sf_Code = HttpContext.Current.Session["SF_Code"].ToString();

        string st_code = State_Code;
        string st_Name = State_name;
        List<GetDatasD> empListD = new List<GetDatasD>();
        string ScriptValues = "";
        string SFCode = DateTime.Now.Year.ToString();
        ChartBAL BL = new ChartBAL();
        ChatBO.HeadDatas SFDet = BL.get_SFDetails2(st_Name);
        List<ChatBO.MainDatas> Setup = BL.getSetups2(div_code,( Convert.ToInt32(DateTime.Now.Year) - 2).ToString(), DateTime.Now.Year.ToString(), st_code);
        List<ChatBO.MainDatas> Setup1 = BL.get_Pur_Bra_item2(div_code, (Convert.ToInt32(DateTime.Now.Year) - 2).ToString(), st_code);
        List<ChatBO.MainDatas> Setup2 = BL.get_Pur_Bra_item2(div_code, (Convert.ToInt32(DateTime.Now.Year) - 1).ToString(), st_code);  //BL.get_Pur_Prod_item2(div_code, SFCode, st_code);
        List<ChatBO.MainDatas> Setup3 = BL.get_Pur_Bra_item2(div_code, (Convert.ToInt32(DateTime.Now.Year) ).ToString(), st_code);  // BL.get_Pur_Prod_itemX2(div_code, SFCode, st_code);

        ScriptValues += "{\"chart\":" + JsonConvert.SerializeObject(SFDet) + ",\"categories\":[{\"category\":" + JsonConvert.SerializeObject(Setup) + "}],\"dataset\":[{\"seriesname\":\"" + (Convert.ToInt32(DateTime.Now.Year) - 2).ToString() + "\",\"data\":" + JsonConvert.SerializeObject(Setup1) + "},{\"seriesname\":\"" + (Convert.ToInt32(DateTime.Now.Year) - 1).ToString() + "\",\"data\":" + JsonConvert.SerializeObject(Setup2) + "},{\"seriesname\":\"" + (Convert.ToInt32(DateTime.Now.Year) ).ToString() + "\",\"data\":" + JsonConvert.SerializeObject(Setup3) + "}]}";
        //ScriptValues += "{\n  \"chart\": {\n    \"caption\": \"State wise Achievement For Last 3 Years – "+ st_Name + "\",\n    \"subcaption\": \"Licel / Nok / Aerosols\",\n    \"yaxisname\": \"Count\",\n    \"numvisibleplot\": \"8\",\n    \"labeldisplay\": \"auto\",\n    \"palettecolors\": \"#428bca,f2726f,#5cb85c\",\n    \"theme\": \"zune\"\n  },\n  \"categories\": [\n    {\n      \"category\": [\n        {\n          \"label\": \"USA\"\n        },\n        {\n          \"label\": \"GB\"\n        },\n        {\n          \"label\": \"China\"\n        },\n        {\n          \"label\": \"Russia\"\n        },\n        {\n          \"label\": \"Germany\"\n        },\n        {\n          \"label\": \"France\"\n        },\n        {\n          \"label\": \"Japan\"\n        },\n        {\n          \"label\": \"Australia\"\n        },\n        {\n          \"label\": \"Italy\"\n        },\n        {\n          \"label\": \"Canada\"\n        },\n        {\n          \"label\": \"South Korea\"\n        },\n        {\n          \"label\": \"Netherlands\"\n        },\n        {\n          \"label\": \"Brazil\"\n        },\n        {\n          \"label\": \"NZ\"\n        },\n        {\n          \"label\": \"Spain\"\n        },\n        {\n          \"label\": \"Hungary\"\n        },\n        {\n          \"label\": \"Kenya\"\n        },\n        {\n          \"label\": \"Jamaica\"\n        },\n        {\n          \"label\": \"Cuba\"\n        },\n        {\n          \"label\": \"Croatia\"\n        }\n      ]\n    }\n  ],\n  \"dataset\": [\n    {\n      \"seriesname\": \"2016-2017\",\n      \"data\": [\n        {\n          \"value\": \"20\"\n        },\n        {\n          \"value\": \"70\"\n        },\n        {\n          \"value\": \"65\"\n        },\n        {\n          \"value\": \"55\"\n        },\n        {\n          \"value\": \"44\"\n        },\n        {\n          \"value\": \"42\"\n        },\n        {\n          \"value\": \"41\"\n        },\n        {\n          \"value\": \"29\"\n        },\n        {\n          \"value\": \"28\"\n        },\n        {\n          \"value\": \"22\"\n        },\n        {\n          \"value\": \"21\"\n        },\n        {\n          \"value\": \"19\"\n        },\n        {\n          \"value\": \"19\"\n        },\n        {\n          \"value\": \"18\"\n        },\n        {\n          \"value\": \"17\"\n        },\n        {\n          \"value\": \"15\"\n        },\n        {\n          \"value\": \"13\"\n        },\n        {\n          \"value\": \"11\"\n        },\n        {\n          \"value\": \"11\"\n        },\n        {\n          \"value\": \"10\"\n        }\n      ]\n    },\n    {\n      \"seriesname\": \"2017-2018\",\n      \"data\": [\n        {\n          \"value\": \"123\"\n        },\n        {\n          \"value\": \"71\"\n        },\n        {\n          \"value\": \"59\"\n        },\n        {\n          \"value\": \"52\"\n        },\n        {\n          \"value\": \"34\"\n        },\n        {\n          \"value\": \"32\"\n        },\n        {\n          \"value\": \"29\"\n        },\n        {\n          \"value\": \"32\"\n        },\n        {\n          \"value\": \"25\"\n        },\n        {\n          \"value\": \"21\"\n        },\n        {\n          \"value\": \"24\"\n        },\n        {\n          \"value\": \"17\"\n        },\n        {\n          \"value\": \"20\"\n        },\n        {\n          \"value\": \"14\"\n        },\n        {\n          \"value\": \"13\"\n        },\n        {\n          \"value\": \"16\"\n        },\n        {\n          \"value\": \"14\"\n        },\n        {\n          \"value\": \"12\"\n        },\n        {\n          \"value\": \"11\"\n        },\n        {\n          \"value\": \"9\"\n        }\n      ]\n    }\n,{\n      \"seriesname\": \"2018-2019\",\n      \"data\": [\n        {\n          \"value\": \"123\"\n        },\n        {\n          \"value\": \"71\"\n        },\n        {\n          \"value\": \"59\"\n        },\n        {\n          \"value\": \"52\"\n        },\n        {\n          \"value\": \"34\"\n        },\n        {\n          \"value\": \"32\"\n        },\n        {\n          \"value\": \"29\"\n        },\n        {\n          \"value\": \"32\"\n        },\n        {\n          \"value\": \"25\"\n        },\n        {\n          \"value\": \"21\"\n        },\n        {\n          \"value\": \"24\"\n        },\n        {\n          \"value\": \"17\"\n        },\n        {\n          \"value\": \"20\"\n        },\n        {\n          \"value\": \"14\"\n        },\n        {\n          \"value\": \"13\"\n        },\n        {\n          \"value\": \"16\"\n        },\n        {\n          \"value\": \"14\"\n        },\n        {\n          \"value\": \"12\"\n        },\n        {\n          \"value\": \"11\"\n        },\n        {\n          \"value\": \"9\"\n        }\n      ]\n    }\n  ]\n}";
        GetDatasD emp = new GetDatasD();
        return ScriptValues;



    }

    public class GetDatasD1
    {

        public string Cval { get; set; }

    }

    public DataSet ss_countinpriorder(string div_code, string date, string subdiv = "0")
    {

        DataSet dsSF = null;
        DB_EReporting db = new DB_EReporting();
        //strQry = "select count(*)cou from Trans_PriOrder_Head where Order_Value!=0 and CONVERT(VARCHAR(25), Order_Date, 126) LIKE '" + date + "%' and Division_Code='" + div_code + "' and Order_Flag='0'";
        //string strQry = "exec [getSS_PrimarOrderCnt] '" + div_code + "','" + date + "','" + subdiv + "'";
        string strQry = "exec [SS_getPrimarOrderCnt] '" + div_code + "','" + date + "','" + subdiv + "'";
        try
        {
            dsSF = db.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;

    }


    public DataSet view_total_order_view(string div_code, string sf_code, string date, string subdiv = "0")
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;


        string strQry = "exec [SS_TODAY_ORDER_VIEW_DISCOUNT_FREEVAL] '" + sf_code + "','" + div_code + "','" + date + "','" + subdiv + "'";
        try
        {
            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }
    [WebMethod(EnableSession = true)]
    public static string Get_access_master()
    {
        DataSet ds = new DataSet();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        StockistMaster sm = new StockistMaster();
        ds = sm.get_access_master_details(div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
	[WebMethod(EnableSession = true)]
    public static string GetHyrUpdt()
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();        
        int Hyr = 0;
        DB_EReporting db_ER = new DB_EReporting();
        string Sqry = "HyrListCreate " + DDiv_code + "";
        try
        {
            Hyr = db_ER.ExecQry(Sqry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        if (Hyr > 0)
        {
            return "Record Updated";
        }
        else
        {
            return "No Record Updated";
        }        
    }
}