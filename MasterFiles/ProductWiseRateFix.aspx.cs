using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using Bus_EReport;
using Bus_Objects;
using System.Data.SqlClient;

using System.IO;
using System.Configuration;



using System.Text;
using System.Dynamic;
using System.Xml.Linq;
using Newtonsoft.Json;


public partial class MasterFiles_ProductWiseRateFix : System.Web.UI.Page
{

    #region "declaration"

    string sfType = string.Empty;
    string sfCode = string.Empty;
    string DivCode = string.Empty;
    string SubDiv = string.Empty;
    string custCode = string.Empty;

    DataSet dsDiv = null;
    DataSet dsFF = null;
    DataSet dsTeam = null;
    DataSet dsCustomer = null;
    DataSet dsTP = null;

    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;

    #endregion 
    protected override void OnPreInit(EventArgs e)
    {
        sfType = Session["sf_type"].ToString();
        if (sfType == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sfType == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sfType == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
        else if (sfType == "4")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        DivCode = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            FillYear();
            FillMRManagers("0");
            fillsubdivision();
            FillMRManagers_MR();
        }

    }
    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(DivCode);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());

                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();

            }
        }

        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();


    }

    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsDiv = sd.Getsubdivisionwise(DivCode);
        if (dsDiv.Tables[0].Rows.Count > 0)
        {

            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsDiv;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }

    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();

        dsTeam = sf.SalesForceListMgrGet_MgrOnly(DivCode, sfCode, Sub_Div_Code);
        if (dsTeam.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsTeam;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("---Select Manager ---", "0"));
        }
    }
    private void FillMRManagers_MR()
    {
        SalesForce sf = new SalesForce();
        //  ddlFFType.Visible = false;
        //ddlAlpha.Visible = false;        
        dsFF = sf.UserList_getMR(DivCode, ddlFieldForce.SelectedValue.ToString(), subdiv.SelectedValue.ToString());
        if (dsFF.Tables[0].Rows.Count > 0)
        {
            ddlMR.DataTextField = "sf_name";
            ddlMR.DataValueField = "sf_code";
            ddlMR.DataSource = dsFF;
            ddlMR.DataBind();
            ddlMR.Items.Insert(0, new ListItem("---Select Base Level---", "0"));
        }
        else
        {
            ddlMR.DataSource = null;
            ddlMR.Items.Clear();
            ddlMR.Items.Insert(0, new ListItem("---Select Base Level---", "0"));
        }
    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillMRManagers_MR();
    }
    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (subdiv.SelectedValue.ToString() != "0")
        {
            FillMRManagers(subdiv.SelectedValue.ToString());
        }
        else
        {
            FillMRManagers(subdiv.SelectedValue.ToString());
        }
    }


    public class Item
    {
        public string product_id { get; set; }
        public string product_name { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static Item[] GetProduct( string subDiv)
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

        Product pro = new Product();
        List<Item> empList = new List<Item>();
        DataSet dsAccessmas = pro.getproductname(div_code.TrimEnd(','), subDiv);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            Item emp = new Item();
            emp.product_id = row["Product_Detail_Code"].ToString();
            emp.product_name = row["Product_Detail_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }
    public class user
    {
        public string sf_code { get; set; }
        public string sf_name { get; set; }
        public string stCode { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static user[] getfo(string term)
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

        SalesForce sf = new SalesForce();



        //sp_UserList_getMR '1','MGR0001'   

        List<user> empList = new List<user>();
        DataSet dsAccessmas = sf.UserList_getMR(div_code.TrimEnd(','), term);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            user emp = new user();
            emp.sf_code = row["sf_code"].ToString();
            emp.sf_name = row["sf_name"].ToString();
            emp.stCode = row["State_Code"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }
    public class ItemRates
    {
        public string pCode { get; set; }
        public string pName { get; set; }
        public string stCode { get; set; }
        public string mRate { get; set; }
        public string rRate { get; set; }
        public string dRate { get; set; }

    }


    [WebMethod(EnableSession = true)]
    public static ItemRates[] getrates(string data)
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

        Product pro = new Product();
        List<ItemRates> empList = new List<ItemRates>();
        DataSet dsAccessmas = pro.getproductnamewithrate(div_code.TrimEnd(','), data);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            ItemRates emp = new ItemRates();
            emp.pCode = row["Product_Detail_Code"].ToString();
            emp.pName = row["Product_Short_Name"].ToString();
            emp.stCode = row["State_Code"].ToString();
            emp.rRate = row["Retailor_Price"].ToString();
            emp.dRate = row["Distributor_Price"].ToString();
            emp.mRate = row["MRP_Price"].ToString();

            empList.Add(emp);
        }
        return empList.ToArray();
    }


    [WebMethod(EnableSession = true)]
    public static PRODU[] gettarget(string sf_code, string years, string months)
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

        Product pro = new Product();
        List<PRODU> empList = new List<PRODU>();

        DataSet dsAccessmas = pro.get_pro_target(div_code.TrimEnd(','), sf_code, years, months);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            PRODU emp = new PRODU();
            emp.sf_code = row["sf_code"].ToString();
            emp.target = row["target"].ToString();
            emp.pcode = row["PRODUCT_CODE"].ToString();
            emp.years = row["year"].ToString();
            emp.months = row["month"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }



    public class PRODU
    {
        public string sf_code { get; set; }
        public string target { get; set; }
        public string pcode { get; set; }
        public string years { get; set; }
        public string months { get; set; }
        public string rsf { get; set; }
        public string rate { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static Producttargets[] getProductSales(string SF_Code, string FYear, string FMonth, string TYear, string TMonth)
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
        Product prd = new Product();
        DataSet DsRetailer = prd.Get_Product_Target_vs_Sal(SF_Code, FYear, FMonth, TYear, TMonth);

        List<Producttargets> vList = new List<Producttargets>();
        foreach (DataRow row in DsRetailer.Tables[0].Rows)
        {
            Producttargets vl = new Producttargets();
            vl.pCode = row["Product_Code"].ToString();
            vl.qty = row["Quantity"].ToString();            
            vl.cMonth = row["cmonth"].ToString();
            vl.cYear = row["cyear"].ToString();
            vList.Add(vl);
        }
        return vList.ToArray();
    }

    public class Producttargets
    {
        public string pCode { get; set; }
        public string qty { get; set; }        
        public string cMonth { get; set; }
        public string cYear { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static string savedata(string data)
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
        var items = JsonConvert.DeserializeObject<List<PRODU>>(data);
        int co = 0;string msg="";
        for (int i = 0; i < items.Count; i++)
        {
            Product PRO = new Product();
            int iReturn = PRO.insert_target(div_code, items[i].sf_code, ((items[i].target == "") ? "0" : items[i].target), items[i].pcode, items[i].years, items[i].months);
            //msg+="exec [Insert_target] '" + div_code + "','" + items[i].sf_code + "','" + items[i].pcode + "','" + ((items[i].target == "") ? "0" : items[i].target)+ "','" + items[i].months + "','" + items[i].years + "';";
			co++;
        }
        if (co > 0)
        {
            return "Sucess"+msg;
        }
        else
        {
            return "Error";
        }
        // return items[0].week_name.ToString();

    }

}