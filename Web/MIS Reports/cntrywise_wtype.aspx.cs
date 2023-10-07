using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;


public partial class MIS_Reports_cntrywise_wtype : System.Web.UI.Page
{
    DataSet dscountry = null;
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    Notice viewnoti = new Notice();
     DataSet ff = new DataSet();
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsTP = null;
    DataSet ds = null;
    string state_cd = string.Empty;
    string[] statecd;
    string sState = string.Empty;
	 string sf_type = string.Empty;
	 protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillCountry();
            FillState();
            DateTime dateTime = DateTime.UtcNow.Date;
            string todate = dateTime.ToString("yyyy-MM-dd");
            DataSet stk = viewnoti.Total_VacantUserdashboard(div_code);
            ff = viewnoti.Total_Userdashboard(div_code, todate);
            string vcn = "0";
            if (stk.Tables[0].Rows.Count > 0)
            {
                vcn = stk.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }

            if (ff.Tables[0].Rows.Count > 0)
            {
                Lbl_Tot_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + " / " + vcn;
            }
        }
    }
    protected void ddlcntry_SelectIndexchanged(object sender, EventArgs e)
    {
        FillState();
        Filldesination();
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        DataSet stk = viewnoti.Total_VacantUserdashboard_country(div_code, ddlcntry.SelectedValue);
        ff = viewnoti.Total_Userdashboard_country(div_code, ddlcntry.SelectedValue, todate);
        string vcn = "0";
        if (stk.Tables[0].Rows.Count > 0)
        {
            vcn = stk.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        if (ff.Tables[0].Rows.Count > 0)
        {
            Lbl_Tot_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + " / " + vcn;
        }
    }
    protected void ddlstate_SelectIndexchanged(object sender, EventArgs e)
    {
        
        DateTime dateTime = DateTime.UtcNow.Date;
        string todate = dateTime.ToString("yyyy-MM-dd");
        DataSet stk = viewnoti.Total_VacantUserdashboard_country_st(div_code, ddlcntry.SelectedValue, ddlstate.SelectedValue);
        ff = viewnoti.Total_Userdashboard_country_st(div_code, ddlcntry.SelectedValue, ddlstate.SelectedValue, todate);
        string vcn = "0";
        if (stk.Tables[0].Rows.Count > 0)
        {
            vcn = stk.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        if (ff.Tables[0].Rows.Count > 0)
        {
            Lbl_Tot_User.Text = ff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + " / " + vcn;
        }
    }
    private void FillCountry()
    {
            Division dv = new Division();
            State st = new State();
            dscountry = st.getcountry(div_code);
            ddlcntry.DataTextField = "Country_name";
            ddlcntry.DataValueField = "Country_code";
            ddlcntry.DataSource = dscountry;
            ddlcntry.DataBind();
            ddlcntry.Items.Insert(0, new ListItem("--Select--", "0"));
       
    }
    private void FillState()
    {
        SalesForce sf = new SalesForce();
        dsState = sf.getAllSF_cntry_States(div_code, ddlcntry.SelectedValue);
       
            ddlstate.DataTextField = "sname";
            ddlstate.DataValueField = "scode";
            ddlstate.DataSource = dsState;
            ddlstate.DataBind();
            ddlstate.Items.Insert(0, new ListItem("--Select--", "0"));
       
    }
    private void Filldesination()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getsfDesignation_cntry(div_code, ddlcntry.SelectedValue);
      
            Dropdesignation.DataTextField = "Designation_Name";
            Dropdesignation.DataValueField = "Designation_Code";
            Dropdesignation.DataSource = dsSalesForce;
            Dropdesignation.DataBind();
            Dropdesignation.Items.Insert(0, new ListItem("--Select--", "0"));
       
    }
    [WebMethod(EnableSession = true)]
    public static string getdata(string divc, string contry,string desgn, string fdate, string tdate,string state)
    {
        SalesForce dv = new SalesForce();
        DataSet dsProd = dv.get_wtype_country(divc, contry, desgn, fdate, tdate, state);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getdatanl(string divc, string contry, string desgn, string fdate, string tdate, string state)
    {
        SalesForce dv = new SalesForce();
        DataSet dsProd = dv.get_wtype_country_nl(divc, contry, desgn, fdate, tdate, state);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getdatasf(string divc, string contry, string desgn, string fdate, string tdate, string wtps, string state)
    {
        SalesForce dv = new SalesForce();
        DataSet dsProd = dv.getworktypecntrywise_sflist(divc, contry, desgn, fdate, tdate, wtps, state);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getdatanlsf(string divc, string contry, string desgn, string fdate, string tdate, string state)
    {
        SalesForce dv = new SalesForce();
        DataSet dsProd = dv.getworktypecntrywisenotlogin__sflist(divc, contry, desgn, fdate, tdate, state);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
}