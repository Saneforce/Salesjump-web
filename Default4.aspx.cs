using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;
using System.Globalization;

public partial class Default4 : System.Web.UI.Page
{
    #region Declaration 
    public static string div_Code = string.Empty;
    public static string st = string.Empty;
    public static string route = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsdoc = null;
    DataSet dsDoctor = null;
    DataSet dsTP = null;
    string iPendingCount = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sto_code = string.Empty;
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
    public static string sf_type = string.Empty;
    Notice viewnoti = new Notice();
    string day = string.Empty;
    string type = string.Empty;
    string comment = string.Empty;
    DCR der = new DCR();
    DateTime dTime = DateTime.UtcNow.Date;
    public static string baseUrl = "";
    #endregion

    #region  Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["sf_type"]) != null || Convert.ToString(Session["sf_type"]) != ""))
        {
            sf_type = Session["sf_type"].ToString();
            sf_code = Session["sf_code"].ToString();
            //HO_ID = Session["HO_ID"].ToString();
            if (sf_type == "4")
            {
                div_code = Session["div_code"].ToString();
            }
            else
            {
                div_code = Session["div_code"].ToString();
            }

            div_code = div_code.Trim(",".ToCharArray());
            string scrpt = "arr=[" + Fillcate() + "];arr1=[" + Fillbrand() + "];arr2=[" + Fillpro() + "];arr3=[" + saleFillcate() + "];arr4=[" + saleFillbrand() + "];arr5=[" + saleFillpro() + "];arr6=[" + Fillcate() + "];arr7=[" + saleFillbrand() + "];arr8=[" + saleFillpro() + "];window.onload = function () {genChart('T10brand',arr,'Purchase Top 10 Categorys');genChart('T10Cate',arr1,'Purchase Top 10 Brands');genChart('T10Pro',arr2,'Purchase Top 10 Products');genChart('saleT10Cate',arr3,'Sale Top 10 Categorys');genChart('saleT10brand',arr4,'Sale Top 10 brands');genChart('saleT10Pro',arr5,'Sale Top 10 Products');genChart1('RetailerT10Cate',arr6,'Sale Top 10 Products');genChart('RetailerT10brand',arr6,'Retailer Top 10 Categorys');genChart('RetailerT10Pro',arr7,'Retailer Top 10 brands');genChart('RetailerT10',arr8,'Retailer Top 10 Products');}";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);
            if (!Page.IsPostBack)
            {
                DateTime dateTime1 = DateTime.UtcNow.Date;
                string todate1 = dateTime1.ToString("yyyy-MM-dd");
                HiddenField1.Value = todate1;
                load_fun(todate1);
            }

            if (Page.IsPostBack)
            {
                string eventTarget = this.Request["__EVENTTARGET"];
                string eventArgument = this.Request["__EVENTARGUMENT"];

                if (eventTarget != string.Empty)
                {
                    DateTime dt = Convert.ToDateTime(eventTarget);
                    string formateDate = dt.ToString("yyyy-MM-dd");
                    HiddenField1.Value = formateDate;
                    load_fun(formateDate);
                }


            }
        }
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion

    #region load_fun
    public void load_fun(string todate1)
    {
        ViewState["dsSalesForce"] = null;
        ViewState["dsDoctor"] = null;
        DataSet ff = new DataSet();

        ff = viewnoti.retailercount_mr(div_code, sf_code);
        if (ff.Tables[0].Rows.Count > 0)
        {
            retailer.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        }
        DSM dsm = new DSM();
        ff = dsm.DSM_Count(sf_code);
        if (ff.Tables[0].Rows.Count > 0)
        {
            DSMCnt.Text = ff.Tables[0].Rows.Count.ToString();

        }
        else
        {
            DSMCnt.Text = "0";
        }


        ff = dsm.Get_Pri_Order_Count(sf_code, todate1, div_code);
        if (ff.Tables[0].Rows.Count > 0)
        {
            Pri_order_count.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
        else
        {
            Pri_order_count.Text = "0";
        }

        ff = der.TODAY_ORDER_VIEWHAP(div_code, sf_code, todate1, sf_type);
        if (ff.Tables[0].Rows.Count > 0)
        {
            sec_order_count.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
        else
        {
            sec_order_count.Text = "0";
        }
        HiddenField1.Value = todate1;
    }
    #endregion

    #region GetCatPrimaryOrderDay
    [WebMethod(EnableSession = true)]
    public static string GetCatPrimaryOrderDay(string dt)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsCatOrderSecDay = new DataSet();
        string todate = dt;
        dsCatOrderSecDay = GetCategorywise_pri_OrderDay(DDiv_code, DSf_Code, todate);
        return JsonConvert.SerializeObject(dsCatOrderSecDay.Tables[0]);
    }
    #endregion

    #region GetCategorywise_pri_OrderDay
    public static DataSet GetCategorywise_pri_OrderDay(string div_code, string SF_Code, string Fdate)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsState = null;

        string strQry = "exec DMSpri_ORDER_CATEGORY_VIEW '" + div_code + "','" + SF_Code + "','" + Fdate + "'";
        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    #endregion

    #region GetCatPrimaryOrderDaymon
    [WebMethod(EnableSession = true)]
    public static string GetCatPrimaryOrderDaymon(string dt)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsCatOrderSecDay = new DataSet();
        string todate = dt;
        dsCatOrderSecDay = GetCategorywise_pri_OrderDaymon(DDiv_code, DSf_Code, todate, "");
        return JsonConvert.SerializeObject(dsCatOrderSecDay.Tables[0]);
    }
    #endregion

    #region GetCatPrimaryOrderDaymon_CUSTOM
    [WebMethod(EnableSession = true)]
    public static string GetCatPrimaryOrderDaymon_CUSTOM(string dt)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsCatOrderSecDay = new DataSet();
        string todate = dt;
        dsCatOrderSecDay = GetCategorywise_pri_OrderDaymon(DDiv_code, DSf_Code, todate, "CUSTOM");
        return JsonConvert.SerializeObject(dsCatOrderSecDay.Tables[0]);
    }
    #endregion

    #region  GetCategorywise_pri_OrderDaymon
    public static DataSet GetCategorywise_pri_OrderDaymon(string div_code, string SF_Code, string Fdate, string TYPE)
    {
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = "";
        DataSet dsState = null;
        if (TYPE == "CUSTOM")
            strQry = "exec [DMSpri_ORDER_CATEGORY_monthly_cust] '" + div_code + "','" + SF_Code + "','" + Fdate + "'";
        else
            strQry = "exec [DMSpri_ORDER_CATEGORY_monthly] '" + div_code + "','" + SF_Code + "','" + Fdate + "'";
        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    #endregion

    #region GetCatPrimaryOrder_YEARLY
    [WebMethod(EnableSession = true)]
    public static string GetCatPrimaryOrder_YEARLY(string dt)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsCatOrderSecDay = new DataSet();
        string todate = dt;
        dsCatOrderSecDay = GetCategorywise_pri_Order_YEAR(DDiv_code, DSf_Code, todate);
        return JsonConvert.SerializeObject(dsCatOrderSecDay.Tables[0]);
    }
    #endregion

    #region GetCategorywise_pri_Order_YEAR
    public static DataSet GetCategorywise_pri_Order_YEAR(string div_code, string SF_Code, string Fdate)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsState = null;

        string strQry = "exec [DMSpri_ORDER_CATEGORY_yearly] '" + div_code + "','" + SF_Code + "','" + Fdate + "'";
        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    #endregion

    #region GetCatSecondOrderDay
    [WebMethod(EnableSession = true)]
    public static string GetCatSecondOrderDay(string dt)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsCatOrderSecDay = new DataSet();
        string todate = dt;
        dsCatOrderSecDay = GetCategorywiseOrderDay(DDiv_code, DSf_Code, todate);
        return JsonConvert.SerializeObject(dsCatOrderSecDay.Tables[0]);
    }
    #endregion

    #region GetCategorywiseOrderDay
    public static DataSet GetCategorywiseOrderDay(string div_code, string SF_Code, string Fdate)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsState = null;

        string strQry = "exec DMSSEC_ORDER_CATEGORY_VIEW '" + div_code + "','" + SF_Code + "','" + Fdate + "'";
        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    #endregion

    #region GetretSecondOrderDay
    [WebMethod(EnableSession = true)]
    public static string GetretSecondOrderDay(string dt)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsCatOrderSecDay = new DataSet();
        string todate = dt;
        dsCatOrderSecDay = GetretwiseOrderDay(DDiv_code, DSf_Code, todate);
        return JsonConvert.SerializeObject(dsCatOrderSecDay.Tables[0]);
    }
    #endregion

    #region GetretwiseOrderDay
    public static DataSet GetretwiseOrderDay(string div_code, string SF_Code, string Fdate)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsState = null;

        string strQry = "exec DMSSEC_ORDER_ret_VIEW '" + div_code + "','" + SF_Code + "','" + Fdate + "'";
        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    #endregion

    #region GetretSecondOrderhead
    [WebMethod(EnableSession = true)]
    public static string GetretSecondOrderhead(string dt)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsCatOrderSecDay = new DataSet();
        string todate = dt;
        dsCatOrderSecDay = GetretwiseOrderhead(DDiv_code, DSf_Code, todate);
        return JsonConvert.SerializeObject(dsCatOrderSecDay.Tables[0]);
    }
    #endregion

    #region GetretwiseOrderhead
    public static DataSet GetretwiseOrderhead(string div_code, string SF_Code, string Fdate)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsState = null;

        string strQry = "exec DMSSEC_ORDER_ret_head '" + div_code + "','" + SF_Code + "','" + Fdate + "'";
        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    #endregion

    #region GetretSecondOrdertotalview
    [WebMethod(EnableSession = true)]
    public static string GetretSecondOrdertotalview(string dt)
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsCatOrderSecDay = new DataSet();
        string todate = dt;
        dsCatOrderSecDay = GetretwiseOrdertotalview(DDiv_code, DSf_Code, todate);
        return JsonConvert.SerializeObject(dsCatOrderSecDay.Tables[0]);
    }
    #endregion

    #region GetretwiseOrdertotalview
    public static DataSet GetretwiseOrdertotalview(string div_code, string SF_Code, string Fdate)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsState = null;

        string strQry = "exec DMSSEC_ORDER_ret_totalview '" + div_code + "','" + SF_Code + "','" + Fdate + "'";
        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    #endregion

    #region Get_Stock
    [WebMethod(EnableSession = true)]
    public static string Get_Stock(string Date)
    {
        DataSet ds = new DataSet();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string stk_Code = HttpContext.Current.Session["sf_code"].ToString();
        StockistMaster sm = new StockistMaster();
        ds = sm.get_Product_stock(Date, div_code, stk_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    #endregion

    #region Get_access_master
    [WebMethod(EnableSession = true)]
    public static string Get_access_master()
    {
        DataSet ds = new DataSet();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        StockistMaster sm = new StockistMaster();
        ds = sm.get_access_master_details(div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    #endregion

    #region getsto()
    public void getsto()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getsto(sf_code, div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            sto_code = drFF[0].ToString();
            //Fillcate();
        }
    }
    #endregion

    #region Fillcate()
    private string Fillcate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        //Year = viewdrop.SelectedItem.ToString();
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Gettop10value_category(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount1 = drFF["value"].ToString();

            stCrtDtaPnt += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";
            stCrtDtaPnt += Convert.ToString(iTotLstCount1) + "},";
        }
        return stCrtDtaPnt;
    }
    #endregion

    #region Fillbrand()
    private string Fillbrand()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt1 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Gettop10value_Brand(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount2 = drFF["value"].ToString();
            stCrtDtaPnt1 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
            stCrtDtaPnt1 += Convert.ToString(iTotLstCount2) + "},";
        }
        return stCrtDtaPnt1;
    }
    #endregion

    #region Fillpro()
    private string Fillpro()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt2 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Gettop10value_Product(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount3 = drFF["value"].ToString();
            stCrtDtaPnt2 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
            stCrtDtaPnt2 += Convert.ToString(iTotLstCount3) + "},";
        }
        return stCrtDtaPnt2;
    }
    #endregion

    #region saleFillcate()
    private string saleFillcate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts1 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop10value_category(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts1 = drFF["value"].ToString();

            stCrtDtaPnts1 += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";
            stCrtDtaPnts1 += Convert.ToString(iTotLstCounts1) + "},";
        }
        return stCrtDtaPnts1;
    }
    #endregion

    #region saleFillbrand()
    private string saleFillbrand()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts2 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop10value_Brand(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts2 = drFF["value"].ToString();
            stCrtDtaPnts2 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
            stCrtDtaPnts2 += Convert.ToString(iTotLstCounts2) + "},";
        }
        return stCrtDtaPnts2;
    }
    #endregion

    #region saleFillpro()
    private string saleFillpro()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts3 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop10value_Product(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts3 = drFF["value"].ToString();
            stCrtDtaPnts3 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
            stCrtDtaPnts3 += Convert.ToString(iTotLstCounts3) + "},";
        }
        return stCrtDtaPnts3;
    }
    #endregion

    #region RetailFillcate()
    private string RetailFillcate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts1 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop10value_category(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts1 = drFF["value"].ToString();

            stCrtDtaPnts1 += "{label:\"" + drFF["Product_Cat_Name"].ToString() + "\",y: ";
            stCrtDtaPnts1 += Convert.ToString(iTotLstCounts1) + "},";
        }
        return stCrtDtaPnts1;
    }
    #endregion

    #region RetailFillbrand()
    private string RetailFillbrand()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts2 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop10value_Brand(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts2 = drFF["value"].ToString();
            stCrtDtaPnts2 += "{label:\"" + drFF["Product_Brd_Name"].ToString() + "\",y:";
            stCrtDtaPnts2 += Convert.ToString(iTotLstCounts2) + "},";
        }
        return stCrtDtaPnts2;
    }
    #endregion

    #region ()
    private string RetailFillpro()
    {
        string sURL = string.Empty;
        string stCrtDtaPnts3 = string.Empty;

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Sales_Gettop10value_Product(div_code);
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCounts3 = drFF["value"].ToString();
            stCrtDtaPnts3 += "{label:\"" + drFF["Product_Detail_Name"].ToString() + "\",y:";
            stCrtDtaPnts3 += Convert.ToString(iTotLstCounts3) + "},";
        }
        return stCrtDtaPnts3;
    }
    #endregion
}