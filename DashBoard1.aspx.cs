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

public partial class DashBoard1 : System.Web.UI.Page
{
    #region Declaration
    string tot_dr = string.Empty;
    decimal tot_dr1 = 0;
    decimal tot_dr2 = 0;
    string tot_Drrr = string.Empty;
    decimal ilist = 0;
    decimal ilistt = 0;
    decimal ilistt1 = 0;
    string Monthsub = string.Empty;
    DataSet dsDoc = null;
    DataSet dsDoc1 = null;
    DataSet dsSalesForce = null;
    DataSet dsdoc = null;
    DataSet dsDoctor = null;
    DataSet dsTP = null;
    string iPendingCount = string.Empty;
    public static string div_code = string.Empty;
    public static string sf_code = string.Empty;
    //DataSet dsState = null;
    string Month = string.Empty;
    string Year = string.Empty;
    int count_tot = 0;
    int count_tot1 = 0;
    DataSet dsDivision = null;
    DataSet dsState = null;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string[] statecd;
    string stCrtDtaPnt = string.Empty;
    string iTotLstCount1 = string.Empty;
    string stCrtDtaPnt1 = string.Empty;
    string iTotLstCount2 = string.Empty;
    string iTotLstCount3 = string.Empty;
    string iTotLstCounts1 = string.Empty;
    string iTotLstCounts2 = string.Empty;
    string iTotLstCounts3 = string.Empty;
    string sf_type = string.Empty;
    Notice viewnoti = new Notice();
    dasb viewdasb = new dasb();
    string day = string.Empty;
    string type = string.Empty;
    string comment = string.Empty;
    string sCurrentDate = string.Empty;
    decimal d1 = 0;
    decimal d2 = 0;
    DateTime dtCurrent;
    SqlConnection con = new SqlConnection(Globals.ConnString);
    DateTime dTime = DateTime.UtcNow.Date;
	public static string sub_divc = string.Empty;
    public static string baseUrl = "";
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["sf_type"]) != null || Convert.ToString(Session["sf_type"]) != ""))
        {
            sf_type = Session["sf_type"].ToString();
            sf_code = Session["sf_code"].ToString();
            //HO_ID = Session["HO_ID"].ToString();
            //sub_divc = Session["sub_division"].ToString();
            if (sf_type == "3")
            {
                div_code = Session["division_code"].ToString();
            }
            else
            {
                div_code = Session["division_code"].ToString();
            }
            if (sf_type == "1")
            {
                div_code = Session["div_code"].ToString();
                Panel1.Visible = false;
            }
            else
            {
                div_code = Session["div_code"].ToString();
            }
            if (sf_type == "2")
            {
                div_code = Session["div_code"].ToString();
                Panel1.Visible = true;
            }
            else
            {
                div_code = Session["div_code"].ToString();

            }
            div_code = div_code.Trim(",".ToCharArray());
            Year = DateTime.Now.Year.ToString();
            //string scrpt1 = "arr=[" + Fillcate() + "];ayy=[" + NewChart() + "];azz=[" + NewChart1() + "];arrt=[" + Fillproductiveorder() + "];window.onload = function () {genChart1('ChrtPrimSec',ayy,azz,'PRIMARY VS SECONDARY');gen('prodt',arrt,'');}";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt1, true);
            //string scrpt = "ayy=[" + NewChart() + "];azz=[" + NewChart1() + "];arrt=[" + Fillproductiveorder() + "];arr1=[" + FillT5cate() + "];arr2=[" + FillB5cate() + "];arr3=[" + FillTop5brand() + "];arr4=[" + FillB5brand() + "];arr5=[" + FillTop5pro() + "];arr6=[" + FillB5pro() + "];arr7=[" + saleFillTop5cate() + "];arr8=[" + saleFillB5cate() + "];arr9=[" + saleFillTop5brand() + "];arr10=[" + saleFillB5brand() + "];arr11=[" + saleFillTop5pro() + "];arr12=[" + saleFillB5pro() + "];arr13=[" + In_Time_St() + "];window.onload = function () {gen1('T5Cate',arr1,'Order Top 5 Categorys');gen1('B5Cate',arr2,'Order Bottom 5 Categorys');gen1('T5brand',arr3,'Order Top 5 brands');gen1('B5brand',arr4,'Order Bottom 5 brands');gen1('T5Pro',arr5,'Retailer Top 5 Products');gen1('B5Pro',arr6,'Retailer Bottom 5 Products');gen1('ST5Cate',arr7,'Retailer Bottom 5 Products');gen1('SB5Cate',arr8,'Retailer Bottom 5 Products');gen1('ST5brand',arr9,'Retailer Bottom 5 Products');gen1('SB5brand',arr10,'Retailer Bottom 5 Products');gen1('ST5Pro',arr11,'Retailer Bottom 5 Products');gen1('SB5Pro',arr12,'Retailer Bottom 5 Products');gen2('chartContainer',arr13,'Time');genChart1('ChrtPrimSec',ayy,azz,'PRIMARY VS SECONDARY');}";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);
            if (!Page.IsPostBack)
            {

                ViewState["dsSalesForce"] = null;
                ViewState["dsDoctor"] = null;
                DataSet ff = new DataSet();

                ff = viewdasb.countindashboard_MGR(div_code, sf_code);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    retailer.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                }
                DateTime dateTime1 = DateTime.UtcNow.Date;
                string todate1 = dTime.ToString("yyyy-MM-dd");
                ff = viewnoti.countinDist_MGR(div_code, sf_code);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    Dist_cou.Text = ff.Tables[0].Rows.Count.ToString();

                }
                ff = viewdasb.countinrout_MGR();
                if (ff.Tables[0].Rows.Count > 0)
                {
                    rout_cou.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                }
                //string g= System.DateTime.Now.ToShortDateString();
                DateTime dateTime = DateTime.UtcNow.Date;
                string todate = dTime.ToString("yyyy-MM-dd");

                ff = viewnoti.orderdashboard(sf_code, div_code, todate);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    ordercount.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                }

                ff = viewnoti.view_total_order_view(div_code, sf_code, todate);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    Order_val.Text = "<a style='color:Red;font-size:Small'>" + "Rs." + ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "</a> - Today Orders";
                    //Order_val.Text = "Today Orders";
                }

                ff = viewnoti.GET_Total_Userdashboard(sf_code, div_code, todate);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    Lbl_Tot_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    //Lbl_Reg_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    //Lbl_Lea.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    //Lbl_Oth.Text = "0";
                    //Lbl_Inact_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

                }

                ff = viewnoti.GET_Total_Userdashboard_Att(sf_code, div_code, todate);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    //Lbl_Tot_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Lbl_Reg_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    Lbl_Lea.Text = ff.Tables[0].Rows[2].ItemArray.GetValue(1).ToString();
                    Lbl_Oth.Text = ff.Tables[0].Rows[3].ItemArray.GetValue(1).ToString(); ;
                    Lbl_Inact_User.Text = ff.Tables[0].Rows[1].ItemArray.GetValue(1).ToString();
                    if (ff.Tables[0].Rows.Count == 5)
                    {
                        Lbl_vsal.Text = ff.Tables[0].Rows[4].ItemArray.GetValue(1).ToString();
                    }

                }

                ff = viewnoti.GET_Total_OutLetdashboard(sf_code, div_code, todate);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    //Lbl_Outlets.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Lbl_Outlets.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    d2 = Convert.ToDecimal(Lbl_Outlets.Text);
                }
                ff = viewnoti.GET_Total_Prodashboard(sf_code, div_code, todate);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    Lbl_Prod.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Lbl_Vist_Outlet.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    d1 = Convert.ToDecimal(Lbl_Prod.Text);
                }
                decimal a = 0;

                if (d2 > 0) a = d1 / d2 * 100;
                Lbl_Prod_Outlet.Text = Math.Round(decimal.Parse(a.ToString()), 2).ToString() + "%";
                                             
                ff = viewnoti.GET_Total_NewRetailerdashboard(sf_code, div_code, todate);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    //Lbl_Outlets.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Lbl_Sch_Call.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                BindData();
            }
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
                string todate1 = dTime.ToString("yyyy-MM-dd");

                ff = viewdasb.countindashboard_MGR(div_code, sf_code);//
                if (ff.Tables[0].Rows.Count > 0)
                {
                    retailer.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                }

                string todate = dTime.ToString("yyyy-MM-dd");
                ff = viewnoti.countinDist_MGR(div_code, sf_code);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    Dist_cou.Text = ff.Tables[0].Rows.Count.ToString();

                }
                ff = viewdasb.countinrout_MGR();
                if (ff.Tables[0].Rows.Count > 0)
                {
                    rout_cou.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                }
                //string g= System.DateTime.Now.ToShortDateString();


                ff = viewnoti.orderdashboard(sf_code, div_code, todate);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    ordercount.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                }

                ff = viewnoti.view_total_order_view(div_code, sf_code, todate);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    Order_val.Text = "<a style='color:Red;font-size:Small'>" + "Rs." + ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "</a> - Today Orders";
                    //Order_val.Text = "Today Orders";
                }

                ff = viewnoti.GET_Total_Userdashboard(sf_code, div_code, todate);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    Lbl_Tot_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    //Lbl_Reg_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    //Lbl_Lea.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    //Lbl_Oth.Text = "0";
                    //Lbl_Inact_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

                }

                ff = viewnoti.GET_Total_Userdashboard_Att(sf_code, div_code, todate);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    //Lbl_Tot_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Lbl_Reg_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    Lbl_Lea.Text = ff.Tables[0].Rows[2].ItemArray.GetValue(1).ToString();
                    Lbl_Oth.Text = ff.Tables[0].Rows[3].ItemArray.GetValue(1).ToString(); ;
                    Lbl_Inact_User.Text = ff.Tables[0].Rows[1].ItemArray.GetValue(1).ToString();
                    if (ff.Tables[0].Rows.Count == 5)
                    {
                        Lbl_vsal.Text = ff.Tables[0].Rows[4].ItemArray.GetValue(1).ToString();
                    }
                }

                ff = viewnoti.GET_Total_OutLetdashboard(sf_code, div_code, todate);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    //Lbl_Outlets.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Lbl_Outlets.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    d2 = Convert.ToDecimal(Lbl_Outlets.Text);
                }
                ff = viewnoti.GET_Total_Prodashboard(sf_code, div_code, todate);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    Lbl_Prod.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Lbl_Vist_Outlet.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    d1 = Convert.ToDecimal(Lbl_Prod.Text);
                }
                decimal a = 0;

                if (d2 > 0) a = d1 / d2 * 100;
                Lbl_Prod_Outlet.Text = Math.Round(decimal.Parse(a.ToString()), 2).ToString() + "%";




                ff = viewnoti.GET_Total_NewRetailerdashboard(sf_code, div_code, todate);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    //Lbl_Outlets.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Lbl_Sch_Call.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                BindData();
            }
        }
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion

    #region BindData()
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
    #endregion

    #region Submit_Click
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
    #endregion

    #region FillT5cate()
    //FillT5cate
    private string FillT5cate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Gettop5value_category(div_code, Year, sf_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount1 = drFF["value"].ToString();
            Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            stCrtDtaPnt += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";

            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
        }
        return stCrtDtaPnt;
    }
    #endregion

    #region FillB5cate()
    //FillB5cate
    private string FillB5cate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.GetB5value_category(div_code, Year, sf_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount1 = drFF["value"].ToString();
            Convert.ToDecimal(iTotLstCount1 = string.IsNullOrEmpty(iTotLstCount1) ? "0" : iTotLstCount1);
            stCrtDtaPnt += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";

            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
        }
        return stCrtDtaPnt;
    }
    #endregion

    #region FillTop5brand()
    //FillTop5brand
    private string FillTop5brand()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt1 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Gettop5value_Brand(div_code, Year, sf_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount2 = drFF["value"].ToString();
            stCrtDtaPnt1 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
            stCrtDtaPnt1 += Convert.ToString(iTotLstCount2) + "},";
        }
        return stCrtDtaPnt1;
    }
    #endregion

    #region FillB5brand()
    //FillB5brand
    private string FillB5brand()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt1 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.GetB5value_Brand(div_code, Year, sf_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount2 = drFF["value"].ToString();
            stCrtDtaPnt1 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
            stCrtDtaPnt1 += Convert.ToString(iTotLstCount2) + "},";
        }
        return stCrtDtaPnt1;
    }
    #endregion

    #region FillTop5pro()
    //FillTop5pro
    private string FillTop5pro()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt2 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Gettop5value_Product(div_code, Year, sf_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount3 = drFF["value"].ToString();
            stCrtDtaPnt2 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
            stCrtDtaPnt2 += Convert.ToString(iTotLstCount3) + "},";
        }
        return stCrtDtaPnt2;
    }
    #endregion

    #region FillB5pro()
    //FillB5pro
    private string FillB5pro()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt2 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.GetB5value_Product(div_code, Year, sf_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount3 = drFF["value"].ToString();
            stCrtDtaPnt2 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
            stCrtDtaPnt2 += Convert.ToString(iTotLstCount3) + "},";
        }
        return stCrtDtaPnt2;
    }
    #endregion

    #region saleFillTop5cate()
    //saleFillTop5cate
    private string saleFillTop5cate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts1 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop5value_category(div_code, Year, sf_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts1 = drFF["value"].ToString();

            stCrtDtaPnts1 += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";
            stCrtDtaPnts1 += Convert.ToString(iTotLstCounts1) + "},";
        }
        return stCrtDtaPnts1;

    }
    #endregion

    #region saleFillB5cate()
    //saleFillB5cate
    private string saleFillB5cate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts1 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_GetB5value_category(div_code, Year, sf_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts1 = drFF["value"].ToString();

            stCrtDtaPnts1 += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";
            stCrtDtaPnts1 += Convert.ToString(iTotLstCounts1) + "},";
        }
        return stCrtDtaPnts1;

    }
    #endregion

    #region saleFillTop5brand()
    //saleFillTop5brand
    private string saleFillTop5brand()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts2 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop5value_Brand(div_code, Year, sf_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts2 = drFF["value"].ToString();
            stCrtDtaPnts2 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
            stCrtDtaPnts2 += Convert.ToString(iTotLstCounts2) + "},";
        }
        return stCrtDtaPnts2;
    }
    #endregion

    #region saleFillB5brand()
    //saleFillB5brand
    private string saleFillB5brand()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts2 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_GetB5value_Brand(div_code, Year, sf_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts2 = drFF["value"].ToString();
            stCrtDtaPnts2 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
            stCrtDtaPnts2 += Convert.ToString(iTotLstCounts2) + "},";
        }
        return stCrtDtaPnts2;
    }
    #endregion

    #region saleFillTop5pro()
    //saleFillTop5pro
    private string saleFillTop5pro()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts3 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop5value_Product(div_code, Year, sf_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts3 = drFF["value"].ToString();
            stCrtDtaPnts3 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
            stCrtDtaPnts3 += Convert.ToString(iTotLstCounts3) + "},";
        }
        return stCrtDtaPnts3;
    }
    #endregion

    #region saleFillB5pro()
    //saleFillB5pro
    private string saleFillB5pro()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts3 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_GetB5value_Product(div_code, Year, sf_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts3 = drFF["value"].ToString();
            stCrtDtaPnts3 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
            stCrtDtaPnts3 += Convert.ToString(iTotLstCounts3) + "},";
        }
        return stCrtDtaPnts3;
    }
    #endregion

    #region In_Time_St()
    //gp
    private string In_Time_St()
    {
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string sURL = string.Empty;
        string stCrtDtaPnts3 = string.Empty;

        Notice sf = new Notice();
        dsSalesForce = sf.GET_In_Time_Statistics(sf_code, div_code, todate); ;
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts3 = drFF["value"].ToString();
            stCrtDtaPnts3 += "{label:\"" + drFF["inTime"].ToString() + "\",y:";
            stCrtDtaPnts3 += Convert.ToString(iTotLstCounts3) + "},";
        }
        return stCrtDtaPnts3;
    }
    #endregion

    #region NewChart()
    public string NewChart()
    {
        string data = string.Empty;
        stCrtDtaPnt = string.Empty;
        string FMonth = "1";
        string FYear = DateTime.Now.Year.ToString();
        string TMonth = "12";
        string TYear = DateTime.Now.Year.ToString();

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


                dsDoc = sf.primary_Purchase_Distributor_MGR(div_code, cmonth, cyear, sf_code);
                dsDoc1 = sf.secondary_Purchase_Distributor_MGR(div_code, cmonth, cyear, sf_code);

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
    #endregion

    #region NewChart1()
    public string NewChart1()
    {
        string data = string.Empty;
        stCrtDtaPnt = string.Empty;
        string FMonth = "1";
        string FYear = DateTime.Now.Year.ToString();
        string TMonth = "12";
        string TYear = DateTime.Now.Year.ToString();

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

                dsDoc = sf.secondary_Purchase_Distributor_MGR(div_code, cmonth, cyear, sf_code);


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
    #endregion

    #region  Fillproductiveorder()
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
    #endregion

    #region VerifyRenderingInServerForm()
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
          server control at run time. */
    }
    #endregion

    #region Item_Bound()
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
    #endregion


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
    public static string getmanp()
    {
        dasb dv = new dasb();
        DataTable ds = new DataTable();
        ds = dv.get_manpower_user();
        return JsonConvert.SerializeObject(ds);
    }


    [WebMethod(EnableSession = true)]
    public static string getdayord(string date)
    {
        dasb dv = new dasb();
        DataTable ds = new DataTable();
        ds = dv.get_manpower_dayord(date);
        return JsonConvert.SerializeObject(ds);
    }

    [WebMethod(EnableSession = true)]
    public static string getmonthord(string date)
    {
        dasb dv = new dasb();
        DataTable ds = new DataTable();
        ds = dv.get_manpower_monthorder(date);
        return JsonConvert.SerializeObject(ds);
    }

    [WebMethod(EnableSession = true)]
    public static string getyearorder(string date)
    {
        dasb dv = new dasb();
        DataTable ds = new DataTable();
        ds = dv.get_manpower_yearorder(date);
        return JsonConvert.SerializeObject(ds);
    }

    public class dasb
    {
        public DataSet countindashboard_MGR(string div_code, string sf_code)
        {
            string sub = "";
            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            //strQry = "select count(ListedDr_Name) as retailercount from Mas_ListedDr where Division_Code='"+div_code+"' and ListedDr_Active_Flag='0'";
            string strQry = "exec [mgr_GET_Retailer_Count] '" + div_code + "','" + sub + "','" + sf_code + "'";
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
        public DataTable get_manpower_user()
        {
            DB_EReporting db_ER = new DB_EReporting();
            //string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec [manpower_user] '" + sf_code + "','" + div_code + "'";
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataTable get_manpower_dayord(string date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            //string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec manpower_dayorder '" + sf_code + "','" + div_code + "','" + date + "'";
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataTable get_manpower_monthorder(string date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            //string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec manpower_monthorder '" + sf_code + "','" + div_code + "','" + date + "'";
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataTable get_manpower_yearorder(string date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            //string sub = "";
            DataTable dsStockist = null;

            string strQry = "exec manpower_yearorder '" + sf_code + "','" + div_code + "','" + date + "'";
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
	
        public DataSet countinrout_MGR()
        {
            //string sub = "";
            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            //strQry = "select count(ListedDr_Name) as retailercount from Mas_ListedDr where Division_Code='"+div_code+"' and ListedDr_Active_Flag='0'";
            string strQry = "exec [GET_route_Cou] '" + div_code + "','" + sf_code + "'";
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
    }
}