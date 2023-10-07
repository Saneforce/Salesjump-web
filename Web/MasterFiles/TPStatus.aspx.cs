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
using System.Web.UI.HtmlControls;
using DBase_EReport;
using ClosedXML.Excel;

public partial class MIS_Reports_TPStatus : System.Web.UI.Page
{

    #region "Declaration"
    DateTime ServerStartTime;
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    DataSet dsTP = null;
    DateTime ServerEndTime;
    string div_code = string.Empty;
    int time;
    #endregion
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
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillYear();
            FillMRManagers("0"); FillDivision();

            FillDepot();
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

    private void FillDivision()
    {
        DataSet dsDivision = new DataSet();
        Division dv = new Division();
        dsDivision = dv.getSubDivisionHO(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlDivision.DataSource = dsDivision;
            ddlDivision.DataTextField = "subdivision_name";
            ddlDivision.DataValueField = "subdivision_code";            
            ddlDivision.DataBind();
            
            ddlDivision.Items.Insert(0, new ListItem("All", "0"));
            ddlDivision.SelectedIndex = 0;
        }

    }
	
	protected void exceldld_Click(object sender, EventArgs e)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        ds = db_ER.Exec_DataSet("exec sp_excel_tourplan '"+ ddlFieldForce.SelectedValue + "','"+ div_code + "','"+ ddlFMonth.SelectedValue + "','"+ ddlFYear.SelectedValue + "'");
        DataTable dt = ds.Tables[0];
        //DataSet ds = getDataSetExportToExcel();

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(ds);
            wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wb.Style.Font.Bold = true;

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Tour_Plan.xlsx");

            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);

                Response.Flush();
                Response.End();
            }
        }
    }


    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        string subdiv = ddlDivision.SelectedValue.ToString();

        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code, subdiv);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {

           
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";            
            ddlFieldForce.DataBind();
            
            ddlFieldForce.Items.Insert(0, "---Select Field Force---");
            ddlFieldForce.SelectedIndex = 0;
        }
        else
        {
            ddlFieldForce.Items.Clear();
            
            ddlFieldForce.Items.Insert(0, "---Select Field Force---");
            ddlFieldForce.SelectedIndex = 0;
        }
    }


    private void FillDepot()
    {
        TourPlan sf = new TourPlan();
        // dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);		  
        dsSalesForce = sf.getDepot(div_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddldepo.DataTextField = "Depot";
            ddldepo.DataValueField = "Depot";
            ddldepo.DataSource = dsSalesForce;
            ddldepo.DataBind();
            ddldepo.Items.Insert(0, new ListItem("---Select Depot---", "0"));

        }
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
        
        // dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);		  
        //dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code, ddlFMonth.SelectedValue,ddlFYear.SelectedValue);

        string subdiv = ddlDivision.SelectedValue.ToString();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code, subdiv);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";            
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
    public static IssueDetails[] getIssuData(string SF_Code, string FYera, string FMonth,string SFDepot,string SubDiv)
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
        DataSet dsPro = exp.getrptIssueSlip_Month_tp(div_code, SF_Code, FMonth, FYera, SFDepot);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetails emp = new IssueDetails();
            emp.sfCode = row["SF_Code"].ToString();
            emp.sfName = row["Sf_Name"].ToString();
            emp.Month = FMonth;
            emp.Year = FYera;
            emp.caseRate = row["TP_Entry"].ToString();
            emp.amount = row["dates"].ToString();
            emp.TC_Count = row["TP_App"].ToString();
            emp.EC_Count = row["TP_Plan"].ToString();
            emp.tDate = row["Edate"] == DBNull.Value ? "" : Convert.ToDateTime(row["Edate"]).ToString("dd/MM/yyyy");
            emp.adate = row["Adate"] == DBNull.Value ? "" : Convert.ToDateTime(row["Adate"]).ToString("dd/MM/yyyy");

            

            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class IssueDetails
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string caseRate { get; set; }
      
        public string amount { get; set; }
        public string TC_Count { get; set; }
        public string EC_Count { get; set; }

        public string tDate { get; set; }
        public string adate { get; set; }

    }
   

   
}