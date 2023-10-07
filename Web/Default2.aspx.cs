using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Configuration;
using System.Web.Services;
using System.Data.SqlClient;
using FusionCharts.Charts;
using Bus_Objects;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

public partial class Default2 : System.Web.UI.Page
{
    #region Declaration
    string tot_dr = string.Empty;
    decimal tot_dr1 = 0;
    string tot_Drrr = string.Empty;
    decimal ilist = 0;
    decimal ilistt = 0;
    float ilistt1 = 0;
    string Monthsub = string.Empty;
    DataSet dsDoc = null;
    DataSet dsSalesForce = null;
    DataSet dsdoc = null;
    DataSet dsDoctor = null;
    DataSet dsTP = null;
    string iPendingCount = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
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
    string day = string.Empty;
    string type = string.Empty;
    string comment = string.Empty;
    string sCurrentDate = string.Empty;
    DateTime dtCurrent;
    SqlConnection con = new SqlConnection(Globals.ConnString);
    public static string baseUrl = "";
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["sf_code"]) != null || Convert.ToString(Session["sf_code"]) != ""))
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
            string scrpt1 = "arr=[" + Fillcate() + "];ayy=[" + NewChart() + "];azz=[" + NewChart1() + "];arrt=[" + Fillproductiveorder() + "];window.onload = function () {genChart1('ChrtPrimSec',ayy,azz,'PRIMARY VS SECONDARY');gen('prodt',arrt,'');}";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt1, true);
            string sJSON = Three_Tier();
            string sJSON1 = Three_Tier1();
            Year = DateTime.Now.Year.ToString();
            Chart sales = new Chart("column2d", "myChart", "360", "200", "json", sJSON.ToString());
            Literal1.Text = sales.Render();
            Chart sales1 = new Chart("column2d", "myChart1", "360", "200", "json", sJSON1.ToString());
            Literal2.Text = sales1.Render();
            //Chart sales2 = new Chart("stackedbar2d", "myChart2", "730", "280", "jsonurl", "Data/MasterPageData.json");
            //Literal3.Text = sales2.Render();

            if (!Page.IsPostBack)
            {

                ViewState["dsSalesForce"] = null;
                ViewState["dsDoctor"] = null;
                DataSet ff = new DataSet();

                ff = viewnoti.countindashboard(div_code);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    retailer.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                }

                ff = viewnoti.countinDist(div_code);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    Dist_cou.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Label3.Text = "Distributor";
                }

                //string g= System.DateTime.Now.ToShortDateString();
                DateTime dateTime = DateTime.UtcNow.Date;
                string todate = dateTime.ToString("yyyy-MM-dd");

                ff = viewnoti.orderdashboard(div_code, todate);
                if (ff.Tables[0].Rows.Count > 0)
                {
                    ordercount.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                }
                BindData();
                Fillfeildforce();

                BindGridviewData();


            }
        }
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion

    #region Fillfeildforce()
    private void Fillfeildforce()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceList(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }
    #endregion

    #region distributor
    public class distributor
    {
        public int stockistCode { get; set; }
        public string stockistname { get; set; }
    }
    #endregion

    #region filldistr
    [WebMethod]
    public static List<distributor> filldistr(string selectValue)
    {
        string div_code = string.Empty;
        string sf_type = string.Empty;
        string distcode = selectValue;
        sf_type = HttpContext.Current.Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = HttpContext.Current.Session["division_code"].ToString();
        }
        else
        {
            div_code = HttpContext.Current.Session["division_code"].ToString();
        }
        //div_code = "1";
        div_code = div_code.Trim(",".ToCharArray());
        DataTable dt = new DataTable();
        DataSet df = new DataSet();
        DataSet fg = new DataSet();
        string sff_code = string.Empty;

        List<distributor> objj = new List<distributor>();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand com = new SqlCommand("EXEC GET_DISTRIBUTOR '" + div_code + "','','" + selectValue + "' ", con);
        SqlDataAdapter da = new SqlDataAdapter(com);
        da.Fill(df);


        if (df.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < df.Tables[0].Rows.Count; i++)
            {
                objj.Add(new distributor

                {
                    stockistCode = Convert.ToInt16(df.Tables[0].Rows[i]["Stockist_Code"]),
                    stockistname = df.Tables[0].Rows[i]["Stockist_Name"].ToString()

                });

            }
        }
        return objj;
    }
    #endregion

    #region BindGridviewData()
    private void BindGridviewData()
    {
        DataSet dsSales = new DataSet();
        con.Open();
        SqlCommand cmd = new SqlCommand("select Doc_Special_Name,Doc_Special_Code,1.0 TVst,1.0 PVst,1.0 Pob," +
                                            "1.0 as Spec_Count " +
                                            " from Mas_Doctor_Speciality where Division_Code='" + div_code + "'", con);
        /*SqlCommand cmd = new SqlCommand("select s.Doc_Special_Name,a.Doc_Special_Code,COUNT(Trans_Detail_Info_Code) TVst,SUM(case when(POB_Value>0) then 1 else 0 End) PVst,round(SUM(POB_Value),2) Pob," +
                                            "(select count(d.Doc_Special_Code) from Mas_ListedDr d where d.Doc_Special_Code = a.Doc_Special_Code " +
                                            " and ListedDr_Active_Flag=0) as Spec_Count" +
                                            " from vwActivity_MSL_Details inner join Mas_ListedDr a on Trans_Detail_Info_Code=ListedDrCode " +
                                            " inner join  Mas_Doctor_Speciality s on a.Doc_Special_Code=s.Doc_Special_Code" +
                                            " where year(Time)='"+Year+"' and s.Division_Code='"+div_code+"' " +
                                            " group by  s.Doc_Special_Name,a.Doc_Special_Code", con);*/
        SqlDataAdapter sqda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sqda.Fill(dsSales);
        cmd.ExecuteNonQuery();
        GridView1.DataSource = dsSales;
        GridView1.DataBind();
        con.Close();

    }
    #endregion

    #region FillOrder()
    private string FillOrder()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        //Year = viewdrop.SelectedItem.ToString();
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Gettop10value_category(div_code);
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

    #region Three_Tier()
    public string Three_Tier()
    {
        string ScriptValues = "";
        string SFCode = DateTime.Now.Year.ToString();
        ChartBAL BL = new ChartBAL();
        ChatBO.HeadDatas SFDet = BL.get_SFDetails(SFCode);
        List<ChatBO.MainDatas> Setup = BL.getSetups(div_code, SFCode, "");
        ChatBO.HeadDatas SFDet1 = BL.get_Pur_Brands(SFCode);
        List<ChatBO.MainDatas> Setup1 = BL.get_Pur_Bra_item(div_code, SFCode);
        ChatBO.HeadDatas SFDet2 = BL.get_Pur_Prod(SFCode);
        List<ChatBO.MainDatas> Setup2 = BL.get_Pur_Prod_item(div_code, SFCode);
        ScriptValues = "{\"chart\":" + JsonConvert.SerializeObject(SFDet) + ",\"data\":" + JsonConvert.SerializeObject(Setup) + "," + "\"linkeddata\":[{" + "\"id\": \"2010Quarters\"," + "\"linkedchart\":{ \"chart\":" + JsonConvert.SerializeObject(SFDet1) + "," + "\"data\":" + JsonConvert.SerializeObject(Setup1) + "," + "\"linkeddata\": [{\"id\": \"2010Q1\",\"linkedchart\":{ \"chart\":" + JsonConvert.SerializeObject(SFDet2) + "," + "\"data\":" + JsonConvert.SerializeObject(Setup2) + "}} ]  }}]}";
        return ScriptValues;
    }
    #endregion

    #region Three_Tier1()
    public string Three_Tier1()
    {
        string ScriptValues = "";
        string SFCode = DateTime.Now.Year.ToString();
        ChartBAL BL = new ChartBAL();
        ChatBO.HeadDatas SFDet = BL.get_Sale_Cat(SFCode);
        List<ChatBO.MainDatas> Setup = BL.get_sale_Cat_item_MGR(div_code, Year, sf_code);
        ChatBO.HeadDatas SFDet1 = BL.get_Sale_Brands(SFCode);
        List<ChatBO.MainDatas> Setup1 = BL.get_Pur_Sale_item(div_code, SFCode);
        ChatBO.HeadDatas SFDet2 = BL.get_Sale_Prod(SFCode);
        List<ChatBO.MainDatas> Setup2 = BL.get_Sale_Prod_item(div_code, SFCode);
        ScriptValues += "{\"chart\":" + JsonConvert.SerializeObject(SFDet) + ",\"data\":" + JsonConvert.SerializeObject(Setup) + "," + "\"linkeddata\":[{" + "\"id\": \"2010Quarters\"," + "\"linkedchart\":{ \"chart\":" + JsonConvert.SerializeObject(SFDet1) + "," + "\"data\":" + JsonConvert.SerializeObject(Setup1) + "," + "\"linkeddata\": [{\"id\": \"2010Q1\",\"linkedchart\":{ \"chart\":" + JsonConvert.SerializeObject(SFDet2) + "," + "\"data\":" + JsonConvert.SerializeObject(Setup2) + "}} ]  }}]}";
        /*string json = JsonConvert.SerializeObject(ScriptValues);
        System.IO.File.WriteAllText(Server.MapPath(@"~\Data\Data.json"), ScriptValues);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", ScriptValues, true);*/
        return ScriptValues;
    }
    #endregion

    #region filldistributor()
    private void filldistributor()
    {

        Notice sd = new Notice();
        dsSalesForce = sd.view_stockist_Distributorwise(div_code, ddlFieldForce.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            Distributor.DataTextField = "Stockist_Name";
            Distributor.DataValueField = "Stockist_code";
            Distributor.DataSource = dsSalesForce;
            Distributor.DataBind();
            Distributor.Items.Insert(0, new ListItem("--Select--", "0"));
            Session["dist_code"] = Distributor.SelectedValue;
        }
    }
    #endregion

    #region Fillproductiveorder()
    private string Fillproductiveorder()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt1 = string.Empty;
        Notice nt = new Notice();

        DateTime dateTime = DateTime.UtcNow.Date;
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

    #region Fillcate()
    private string Fillcate()
    {
        string sURL = string.Empty;
        string stCrtDtaPnt = string.Empty;
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Gettop10value_category(div_code);
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

    #region Item_Bound
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

    #region Fillproductiveorderr
    private string Fillproductiveorderr(string distributor, string route)
    {
        string sURL = string.Empty;
        string stCrtDtaPnt1 = string.Empty;
        Notice nt = new Notice();

        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["division_code"].ToString();
        }

        div_code = div_code.Trim(",".ToCharArray());

        DateTime dateTime = DateTime.UtcNow.Date;
        string datep = dateTime.ToString("yyyy-MM-dd");
        dsSalesForce = nt.get_call_count_distributor_rotewise(div_code, distributor, route, datep);
        stCrtDtaPnt1 = "[";
        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {
            iTotLstCount2 = drFF["tot_cals"].ToString();

            iTotLstCount1 += drFF["tot_cals"].ToString();

            if (stCrtDtaPnt1 != "[") { stCrtDtaPnt1 += ","; }
            stCrtDtaPnt1 += "{\"label\":\"" + drFF["call_type"].ToString() + "\",\"y\":";
            stCrtDtaPnt1 += "\"" + Convert.ToString(iTotLstCount2) + "\",";
            stCrtDtaPnt1 += "\"legendText\":\"" + drFF["call_type"].ToString() + "\",";
            if (drFF["call_type"].ToString() == "Productive")
            {

                stCrtDtaPnt1 += "\"color\":\"#DCE775\"}";
            }
            else
            {
                stCrtDtaPnt1 += "\"color\":\"#81D4FA\"}";
            }
        }
        stCrtDtaPnt1 += "]";
        return stCrtDtaPnt1;
    }
    #endregion
    
    #region Fillproductiveorderr_total
    private string Fillproductiveorderr_total(string distributor, string route)
    {
        string sURL = string.Empty;
        string ilit_productive = string.Empty;
        string ilit_nonprod = string.Empty;
        decimal total = 0;
        decimal productive = 0;
        decimal non_prod = 0;
        Notice nt = new Notice();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["division_code"].ToString();
        }

        div_code = div_code.Trim(",".ToCharArray());
        DateTime dateTime = DateTime.UtcNow.Date;
        string datep = dateTime.ToString("yyyy-MM-dd");

        dsSalesForce = nt.get_call_count_distributor_rotewise(div_code, distributor, route, datep);

        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
        {

            iTotLstCount1 = drFF["tot_cals"].ToString();
            total += Convert.ToDecimal(iTotLstCount1);
            if (drFF["call_type"].ToString() == "Productive")
            {
                ilit_productive += drFF["tot_cals"].ToString();
                productive += Convert.ToDecimal(ilit_productive);
            }
            else
            {
                ilit_nonprod += drFF["tot_cals"].ToString();
                non_prod += Convert.ToDecimal(ilit_nonprod);
            }
        }
        string tot = Convert.ToString(total) + "&" + Convert.ToString(productive) + "&" + Convert.ToString(non_prod);
        return tot;
    }
    #endregion

    #region department
    public class department
    {
        public int TerritoryCode { get; set; }
        public string TerritoryName { get; set; }

    }
    #endregion

    #region fillrou
    [WebMethod]
    public static List<department> fillrou(string selectValue)
    {
        string div_code = string.Empty;
        string sf_type = string.Empty;
        string distcode = selectValue;
        sf_type = HttpContext.Current.Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = HttpContext.Current.Session["division_code"].ToString();
        }
        else
        {
            div_code = HttpContext.Current.Session["division_code"].ToString();
        }
        //div_code = "1";
        div_code = div_code.Trim(",".ToCharArray());
        DataTable dt = new DataTable();
        List<department> obj = new List<department>();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand com = new SqlCommand("select Territory_Code,Territory_Name from mas_territory_creation where Division_Code='" + div_code + "' and Dist_Name='" + distcode + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(com);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                obj.Add(new department

                {
                    TerritoryCode = Convert.ToInt16(dt.Rows[i]["Territory_Code"]),
                    TerritoryName = dt.Rows[i]["Territory_Name"].ToString()

                });

            }
        }
        return obj;
    }
    #endregion

    #region populatedropdownlist
    [WebMethod]
    public static string[] populatedropdownlist(string selectedValue, string routevalue)
    {
        //var callHistoryHandler = new Default2();
        //callHistoryHandler.Fillproductiveorder();
        string distcode = selectedValue;
        string routecode = routevalue;
        var thisPage = new Default2();
        string s = thisPage.Fillproductiveorderr(distcode, routecode);
        string p = thisPage.Fillproductiveorderr_total(distcode, routecode);
        string description = s + "&" + p;
        string[] a = description.Split('&');
        //string k = "jiji";
        return a;

    }
    #endregion

    #region  NewChart()
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

                dsDoc = sf.primary_Purchase_Distributor(div_code, cmonth, cyear, sCurrentDate);


                if (dsDoc.Tables[0].Rows.Count > 0)
                    tot_dr1 = Convert.ToDecimal(dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0));

                if (tot_dr1 != 0)
                {
                    ilistt += tot_dr1;
                }

                stCrtDtaPnt += ilistt + "},";

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
    #endregion

    #region VerifyRenderingInServerForm
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
          server control at run time. */
    }
    #endregion

    #region Submit_Click
    protected void Submit_Click(object sender, EventArgs e)
    {
        string FMonth = DateTime.Now.Month.ToString();
        string FYear = DateTime.Now.Year.ToString();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        string sURL = "MIS Reports/rpt_Total_Order_View.aspx?sf_code=" + sf_code + "&div_code=" + div_code + "&cur_month=" + FMonth + "&cur_year=" + FYear +
               "&Mode=" + "TPMYDayPlan" + "&Sf_Name=" + sf_code + "&Date=" + todate + "&Type=" + sf_type;
        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    }
    #endregion

    #region ddlFieldForce_SelectedIndexChanged
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldistributor();
        //FillRoute();
    }
    #endregion

    #region Distributor_SelectedIndexChanged
    protected void Distributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillRoute();
    }
    #endregion

    #region FillRoute()
    private void FillRoute()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.GetRouteName_Customer(Distributor.SelectedValue, div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            route.DataTextField = "Territory_Name";
            route.DataValueField = "Territory_Code";
            route.DataSource = dsSalesForce;
            route.DataBind();

        }

    }
    #endregion

}