using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Web.Services;

public partial class MasterFiles_RetailerApproval : System.Web.UI.Page
{
    #region "Declaration"
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsTP = null;
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
        sf_type = Session["sf_type"].ToString();
        if (!Page.IsPostBack)
        {

        }
    }

    public class newRetailer
    {
        public string cCode { get; set; }
        public string cName { get; set; }
        public string routeName { get; set; }
        public string createDate { get; set; }
        public string sfName { get; set; }
  public string picture { get; set; }
        public string Address { get; set; }
        public string Area_Name { get; set; }
        public string City_Name { get; set; }
        public string Landmark { get; set; }
        public string PIN_Code { get; set; }
        public string Contact_Person { get; set; }
        public string Designation { get; set; }
        public string Phone_No { get; set; }
 public string lat { get; set; }
        public string longn { get; set; }
    }

    [WebMethod]
    public static List<newRetailer> getNewRetailer()
    {

        List<newRetailer> Lists = new List<newRetailer>();

        DataSet ds = new DataSet();
        ListedDR ldr = new ListedDR();

        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        string SFCode = HttpContext.Current.Session["sf_code"].ToString();

        DateTime dts = new DateTime();

        if (SFCode.Contains("Admin") || SFCode.Contains("admin"))
        {
            ds = ldr.GetNewRetailers(div_code, SFCode, "", dts.Year.ToString(),"3");
        }
        else
        {
            ds = ldr.GetNewRetailers(div_code, SFCode, "", dts.Year.ToString(),"3");
        }

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            newRetailer list = new newRetailer();
            list.cCode = row["ListedDrCode"].ToString();
            list.cName = row["ListedDr_Name"].ToString();
 			list.picture = row["Visit_Hours"].ToString();
            list.routeName = row["Territory_Name"].ToString();
            list.createDate = row["ListedDr_Created_Date"] == DBNull.Value ? "" : Convert.ToDateTime(row["ListedDr_Created_Date"]).ToString("dd/MM/yyyy");
            list.sfName = row["SFName"].ToString();
 list.lat = row["ListedDr_Class_Patients"].ToString();
            list.longn = row["ListedDr_Consultation_Fee"].ToString();
            list.Address = row["ListedDr_Address1"].ToString();
            list.Area_Name = row["ListedDr_Address2"].ToString();
            list.City_Name = row["ListedDr_Address3"].ToString();
            list.Landmark = row["Land_Mark"].ToString();
            list.PIN_Code = row["PIN_Code"].ToString();
            list.Contact_Person = row["contactperson"].ToString();
            list.Designation = row["designation"].ToString();
            list.Phone_No = row["Mobile"].ToString();
            Lists.Add(list);
        }
        return Lists.ToList();
    }

    [WebMethod(EnableSession = true)]
    public static string UpdateApprove(string custCode)
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
        string SFCode = HttpContext.Current.Session["sf_code"].ToString();

        ListedDR adm = new ListedDR();
        int ret = -1;
        if (SFCode.Contains("Admin") || SFCode.Contains("admin"))
        {

             ret = adm.Retailer_Appprove_Manager(SFCode, custCode, "1", "",0);
        }
        else
        {
             ret = adm.Retailer_Appprove_Manager(SFCode, custCode, "1", "",2);
        }
        if (ret > 0)
        {
            return "Retailer Approved Successfully..!";
        }
        else
        {
            return "Retailer Approved UnSuccessfull..!";
        }
    }
    [WebMethod(EnableSession = true)]
    public static string UpdateReject(string custCode, string reasion)
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
        string SFCode = HttpContext.Current.Session["sf_code"].ToString();

        ListedDR adm = new ListedDR();
        int ret = adm.Retailer_Appprove_Manager(SFCode, custCode, "2", reasion,4);
        if (ret > 0)
        {
            return "Retailer Rejected Successfully..!";
        }
        else
        {
            return "Retailer Rejected UnSuccessfull..!";
        }
    }

}