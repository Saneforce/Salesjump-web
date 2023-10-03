using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.IO;
using System.Data;
using Bus_EReport;
using Newtonsoft.Json;
using System.Web.Services;
using System.Data.SqlClient;
public partial class MIS_Reports_Pro_val_sum : System.Web.UI.Page
{

    #region "Declaration"
    DateTime ServerStartTime;
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    DataSet dsTP = null;
    DateTime ServerEndTime;
    string div_code = string.Empty;
    string sf_type = string.Empty;
    int time;
    #endregion
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
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {

            FillYear();
            fillsubdivision();
            FillMRManagers("0");
            
        }
    }
    public class Item
    {
        public string product_id { get; set; }
        public string product_name { get; set; }
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



    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();
        // dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);		  
        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code, Sub_Div_Code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, "---Select Field Force---");

        }
    }
    [WebMethod(EnableSession = true)]
    public static Item[] getdata()
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
        DataSet dsAccessmas = pro.getproductname(div_code.TrimEnd(','));
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
    public static IssueDetails[] getIssuData(string SF_Code, string FYera, string FMonth)
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


        Product pro = new Product();
        List<IssueDetails> empList = new List<IssueDetails>();
        Expense exp = new Expense();
        DataSet dsPro = exp.getrptIssueSlip_Month(div_code, SF_Code, FYera, FMonth, "0");
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetails emp = new IssueDetails();
            emp.sfCode = row["SF_Code"].ToString();
            //emp.sfName = row["Sf_Name"].ToString();
            emp.proCode = row["Product_Detail_Code"].ToString();
            //emp.proName = row["Product_Short_Name"].ToString();
            emp.caseRate = row["Quantity"].ToString();
            emp.amount = row["order_val"].ToString();
            emp.TC_Count = row["TC_Count"].ToString();
            emp.EC_Count = row["EC_Count"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class IssueDetails
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string proCode { get; set; }
        public string proName { get; set; }
        public string caseRate { get; set; }
        public string piceRate { get; set; }
        public string amount { get; set; }
        public string TC_Count { get; set; }
        public string EC_Count { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static FFNames[] getSalesforce(string SF_Code)
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
        List<FFNames> empList = new List<FFNames>();
        DataSet dsAccessmas = sf.SalesForceList(div_code.TrimEnd(','), SF_Code);
        foreach (DataRow row in dsAccessmas.Tables[0].Rows)
        {
            FFNames emp = new FFNames();
            emp.sfCode = row["Sf_Code"].ToString();
            emp.sfName = row["Sf_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class FFNames
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
    }

    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
      //  if (sf_type == "3")
            dsSalesForce = sd.Getsubdivisionwise(div_code);
       // else
         //   dsSalesForce = sd.Getsubdivisionwise_sfcode(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (subdiv.SelectedValue != "0")
        {
            FillMRManagers(subdiv.SelectedValue.ToString());
        }
        else
        {
            FillMRManagers("0");
        }
    }
}