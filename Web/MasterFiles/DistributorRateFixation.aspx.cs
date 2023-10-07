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


using System.Linq;
using System.Text;
using System.Dynamic;
using System.Xml.Linq;


public partial class MasterFiles_DistributorRateFixation : System.Web.UI.Page
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
    public static string sub_division = string.Empty;
    #endregion 
    protected void Page_PreInit(object sender, EventArgs e)
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
		sub_division = Session["sub_division"].ToString();

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
        dsDiv = sd.Getsubdivisionwise(DivCode,sub_division);
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

    [WebMethod(EnableSession = true)]
    public static masterClass.distributor[] GetDistributor(string sf_code)
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

        Stockist stk = new Stockist();

        List<masterClass.distributor> HDay = new List<masterClass.distributor>();
        DataSet dsAlowtype = null;
        dsAlowtype = stk.GetStockist_subdivisionwise(div_code, "0", sf_code);
        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            masterClass.distributor d = new masterClass.distributor();
            d.distCode = row["Stockist_code"].ToString();
            d.distName = row["Stockist_Name"].ToString();
            HDay.Add(d);
        }
        return HDay.ToArray();
    }


    [WebMethod(EnableSession = true)]
    public static string UploadXML(string data)
    {
        TargetFixation tf = new TargetFixation();
        DataSet dsT = tf.InsertDistTarget(data);
        if (dsT.Tables.Count > 0)
        {
            if (dsT.Tables[0].Rows[0][0].ToString() == "SUCCESS")
            {
                return "Successfully Add or Update Records..! ";
            }
            else
            {
                return "unable to Add or Update Check Records..!";
            }
        }
        else
        {
            return "unable to Add or Update Check Records..!";
        }
    }


    [WebMethod(EnableSession = true)]
    public static masterClass.distributor[] GetDistTargets(string sf_code, string FYear, string FMonth)
    {


        TargetFixation ldr = new TargetFixation();

        List<masterClass.distributor> HDay = new List<masterClass.distributor>();
        DataSet dsAlowtype = null;
        dsAlowtype = ldr.getTargets_for_distributor(sf_code, FYear, FMonth);
        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            masterClass.distributor d = new masterClass.distributor();
            d.distCode = row["distCode"].ToString();
            d.Amount = Convert.ToDecimal(row["camount"].ToString());
            HDay.Add(d);
        }
        return HDay.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static DistributorValues[] getDistributorSales(string SF_Code, string FYear, string FMonth, string TYear, string TMonth)
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
        Stockist stk = new Stockist();
        DataSet DsRetailer = stk.Get_Distributor_Target_vs_Sal(SF_Code, FYear, FMonth, TYear, TMonth);

        List<DistributorValues> vList = new List<DistributorValues>();
        foreach (DataRow row in DsRetailer.Tables[0].Rows)
        {
            DistributorValues vl = new DistributorValues();
            vl.dCode = row["Stockist_Code"].ToString();            
            vl.ordVal = row["ord_val"].ToString();
            vl.tarVal = row["camount"].ToString();
            vl.cMonth = row["cmonth"].ToString();
            vl.cYear = row["cyear"].ToString();
            vList.Add(vl);
        }
        return vList.ToArray();
    }

    public class DistributorValues
    {
        public string dCode { get; set; }
        public string ordVal { get; set; }
        public string tarVal { get; set; }
        public string cMonth { get; set; }
        public string cYear { get; set; }
    }
}