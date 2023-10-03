using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using Bus_EReport;

using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;


public partial class MasterFiles_Target_Settings : System.Web.UI.Page
{

    DataSet dsDivision = null;
    DataSet dsState = null;
    string state_cd = string.Empty;
    string[] statecd;
    string sState = string.Empty;
    string Div_Code = string.Empty;
    string SF_Type = string.Empty;
    string SF_Code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsTP = null;
    DateTime ServerStartTime;
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
     
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            Div_Code = Session["div_code"].ToString();
        }
        else
        {
            Div_Code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillState(Div_Code);
            FillYear();
        }
    }
    public class pro_years
    {
        public string years { get; set; }
    }
    
    [WebMethod(EnableSession = true)]
    public static Mas_Target_Name[] Get_Target_Name()
    {
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        AdminSetup ads = new AdminSetup();
        DataSet dsTarget = ads.Get_Target_Names(Div_code);
        List<Mas_Target_Name> mst = new List<Mas_Target_Name>();
        foreach (DataRow row in dsTarget.Tables[0].Rows)
        {
            Mas_Target_Name ms = new Mas_Target_Name();
            ms.Code = row["ID"].ToString();
            ms.Name = row["MS_NAME"].ToString();
            ms.Category = row["CATEGORY"].ToString();
            mst.Add(ms);
        }
        return mst.ToArray();
    }

    public class Mas_Target_Name
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string tvalues { get; set; }
        public string months { get; set; }
        public string years { get; set; }
    }
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(Div_Code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();

            }
        }

        ddlFYear.SelectedValue = DateTime.Now.Year.ToString();

    }
    [WebMethod]
    public static string savedata(string data)
    {
        MasterFiles_Target_Settings mts = new MasterFiles_Target_Settings();
        return mts.save(data);
    }
    private string save(string data)
    {

        SF_Type = Session["sf_type"].ToString();
        SF_Code = Session["sf_code"].ToString();

        if (SF_Type == "3")
        {
            Div_Code = Session["division_code"].ToString();
        }
        else
        {
            Div_Code = Session["div_code"].ToString();
        }
        var items = JsonConvert.DeserializeObject<List<Item>>(data);
        int co = 0;
        for (int i = 0; i < items.Count; i++)
        {
            AdminSetup admin = new AdminSetup();
            if (items[i].Code != null)
            {
                int iReturn = admin.insert_target_setting(Div_Code.TrimEnd(','), items[i].Code, items[i].values,items[i].months,items[i].years,items[i].state);
                co++;
            }

        }
        if (co > 0)
        {
            return "Sucess";
        }
        else
        {
            return "Error";
        }
    }

    public class Item
    {
        public string Code { get; set; }
        public string values { get; set; }
        public string months { get; set; }
        public string years { get; set; }

        public string state { get; set; }


    }

    [WebMethod(EnableSession = true)]
    public static Mas_Target_Name[] gettargetsetting( string data,string state)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }

        AdminSetup ads = new AdminSetup();
        List<Mas_Target_Name> mst = new List<Mas_Target_Name>();
        string year = data;
        DataSet dsAccessmas = ads.Get_target_setting_Values(div_code.TrimEnd(','),year,state);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            Mas_Target_Name ms = new Mas_Target_Name();
            ms.Code = row["id"].ToString();
            ms.tvalues = row["TVALUES"].ToString();
            ms.years = row["years"].ToString();
            ms.months = row["months"].ToString();
            mst.Add(ms);
        }
        return mst.ToArray();
    }
    private void FillState(string Div_Code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(Div_Code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getStateProd(state_cd);
            ddlstate.DataTextField = "statename";
            ddlstate.DataValueField = "state_code";
            ddlstate.DataSource = dsState;
            ddlstate.DataBind();
            ddlstate.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
}