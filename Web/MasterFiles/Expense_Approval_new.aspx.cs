using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



using System.Data;
using Bus_EReport;

using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;


public partial class MasterFiles_Expense_Approval : System.Web.UI.Page
{
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
        if (!Page.IsPostBack)
        {
           
        }
    }
    [WebMethod(EnableSession = true)]
    public static string FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(Div_code, Sf_Code);

        return JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
    }
    public class FieldForce_Details
    {
        public string sf_code { get; set; }
        public string sf_name { get; set; }
        public string designation_name { get; set; }
        public string sf_HQ { get; set; }
        public string Designation_Code { get; set; }
        public string exstatus { get; set; }
        public string Reporting_To_SF { get; set; }
        public string EmpID { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static FieldForce_Details[] Get_FieldForce(string exp_years, string exp_month)
    {

        Notice nt = new Notice();
        List<FieldForce_Details> FFD = new List<FieldForce_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Sf_Type = HttpContext.Current.Session["sf_type"].ToString();
        DataSet dsDetails = null;
        string exp_mode = string.Empty;
        if (Sf_Type == "1")
        {
            exp_mode = "0";
        }
        else if (Sf_Type == "2")
        {
            exp_mode = "1";
        }
        else
        {
            exp_mode = "2";
        }

        //dsDetails = nt.getSalesForce_Fare(Div_code, Sf_Code, exp_mode, exp_years, exp_month);
        dsDetails = nt.getSalesForce_Fare(Div_code, Sf_Code, exp_mode, exp_years, exp_month, Sf_Type);
        foreach (DataRow row in dsDetails.Tables[0].Rows)
        {
            FieldForce_Details ffd = new FieldForce_Details();
            ffd.sf_code = row["sf_code"].ToString();
            ffd.sf_name = row["sfname"].ToString();
            ffd.exstatus = row["exstatus"].ToString();
            ffd.Reporting_To_SF = row["Reporting_To_SF"].ToString();
            ffd.EmpID = row["EmpID"].ToString();
            // ffd.designation_name = row["designation_name"].ToString();
            //  ffd.sf_HQ = row["sf_HQ"].ToString();
            //  ffd.Designation_Code = row["Designation_Code"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }
    public class pro_years
    {
        public string years { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static pro_years[] Get_Year()
    {
        List<pro_years> product = new List<pro_years>();
        TourPlan tp = new TourPlan();
        DataSet dsTP = null;
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        dsTP = tp.Get_TP_Edit_Year(Div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                pro_years pd = new pro_years();
                pd.years = k.ToString();
                product.Add(pd);

            }
        }
        return product.ToArray();
    }
}
