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

using Newtonsoft.Json;
using System.Web.Services;
using System.Data.SqlClient;

public partial class MasterFiles_Product_Target_Achive : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    DataTable dsProd1 = null;
    DataSet dsProd = null;
    DataSet dsProduct = null;
    DataSet dsDivision = null;
    DataSet dsState = null;
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string sf_type = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string ProdCode = string.Empty;
    string ProdDescr = string.Empty;
    string ProdName = string.Empty;
    string ProdSaleUnit = string.Empty;
    string sCmd = string.Empty;
    string Char = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_PreInit(object sender, EventArgs e)
    {
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
        else if (sf_type == "4")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            fillsubdivision();
            FillYear();
            FillMRManagers("0");
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
        dsTP = tp.Get_TP_Edit_Year(div_code);
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
        DataSet dsDiv = sd.Getsubdivisionwise(div_code);
        if (dsDiv.Tables[0].Rows.Count > 0)
        {

            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsDiv;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));

        }
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
    private void FillMRManagers(string subDiv)
    {
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);


        ddlFieldForce.DataSource = null;
        ddlFieldForce.Items.Clear();
        ddlFieldForce.Items.Insert(0, "---Select Field Force---");

        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code, subDiv);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, "---Select Field Force---");

        }
    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {


    }
    [WebMethod(EnableSession = true)]
    public static Item[] getdata(string data)
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
        DataSet dsAccessmas = pro.getproductname(div_code.TrimEnd(','), data);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            Item emp = new Item();
            emp.product_id = row["Product_Detail_Code"].ToString();
            emp.product_name = row["Product_Short_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
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
    public class Item
    {
        public string product_id { get; set; }
        public string product_name { get; set; }
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

    public class user
    {
        public string sf_code { get; set; }
        public string sf_name { get; set; }
        public string stCode { get; set; }
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
        int co = 0;
        for (int i = 0; i < items.Count; i++)
        {
            Product PRO = new Product();
            int iReturn = PRO.insert_target(div_code, items[i].sf_code, items[i].target == "" ? "0" : items[i].target, items[i].pcode, items[i].years, items[i].months);
            co++;
        }
        if (co > 0)
        {
            return "Sucess";
        }
        else
        {
            return "Error";
        }
        // return items[0].week_name.ToString();

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


}