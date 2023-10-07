using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class MIS_Reports_tsr_TsrAttendance_Rpt : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string sCurrentDate = string.Empty;
    public static string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    int mode = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet ds = null;
    string state_cd = string.Empty;
    string[] statecd;
    string sState = string.Empty;
    public static string sub_division = string.Empty;   
    
    public static string Mode = string.Empty;
    
    

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
        //Mode = Request.QueryString["Mode"].ToString();

        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        sub_division = Session["sub_division"].ToString();
        if (sf_type == "1")
        {
            sf_code = Session["sf_code_MR"].ToString();
            div_code = Session["div_code"].ToString();           
        }

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.FindControl("btnBack").Visible = false;
            
            string url = HttpContext.Current.Request.Url.AbsoluteUri.Replace("http://", "");
            string[] words = url.Split('.');
            string shortna = words[0];
            if (shortna == "www") shortna = words[1];
            if (Session["CmpIDKey"] != null && Session["CmpIDKey"].ToString() != "") { shortna = Session["CmpIDKey"].ToString(); }
            string filename = shortna + "_logo.png";
            string dynamicFolderPath = "../limg/";//which used to create                                       dynamic folder
            string path = dynamicFolderPath + filename.ToString();
            
        }



        //string div_code1 = Convert.ToString(Session["div_code"]);
        //string sf_code1 = Convert.ToString(Session["sf_code"]);
        //string sf_type1 = Convert.ToString(Session["sf_type"]);

        //if (div_code1 == "" || div_code1 == null)
        //    div_code = "";
        //else
        //    div_code = Convert.ToString(div_code1);

        //if (sf_code1 == "" || sf_code1 == null)
        //    sf_code = "";
        //else
        //    sf_code = Convert.ToString(sf_code1);

        //if (sf_type1 == "" || sf_type1 == null)
        //    sf_type = "";
        //else
        //    sf_type = Convert.ToString(sf_type1);
    }

    [WebMethod]
    public static string getstate(string divcode)
    {
        DataTable dt = new DataTable();
        SalesForce Ad = new SalesForce();
        dt = Ad.get_States(divcode, sf_code, "0");
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static string getZone(string divcode)
    {
        DataTable dt = new DataTable();
        SalesForce Ad = new SalesForce();
        dt = Ad.get_Fields(divcode);
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static string getSO(string divcode, string mgrid)
    {
        DataTable dt = new DataTable();
        SalesForce Ad = new SalesForce();
        dt = Ad.get_ddlFieldForce(divcode, mgrid);
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static string getDistributor(string divcode)
    {
        DataTable dt = new DataTable();
        SalesForce Ad = new SalesForce();
        dt = Ad.get_Distributor(divcode);
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static string getIdentification(string divcode)
    {
        DataTable dt = new DataTable();
        SalesForce Ad = new SalesForce();
        dt = Ad.get_Identification(divcode);

        return JsonConvert.SerializeObject(dt);
    }
}