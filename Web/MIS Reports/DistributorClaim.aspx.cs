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

public partial class MIS_Reports_DistributorClaim : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string sfcode1 = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    

    #region "Declaration"
    DataSet dsSalesForce = null;
    DataSet dsStockist = null;
    DataSet dsTerritory = null;
    string divcode1 = string.Empty;
    string stockist_code = string.Empty;
    string stockist_name = string.Empty;
    string stockist_Address = string.Empty;
    string stockist_ContactPerson = string.Empty;
    string stockist_Designation = string.Empty;
    string stockist_mobileno = string.Empty;
    string Territory = string.Empty;
    string sf_code = string.Empty;
    string sCmd = string.Empty;
    string sChkSalesforce = string.Empty;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string sf_code1 = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        sfcode1 = Session["sf_code"].ToString();
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {           
           // FillReporting();         
        }
    }
    [WebMethod]
    public static string GetDetails(string SF, string Div, string Mn, string Yr)
    {
        divcode = Div;
        //sfcode = SF;
        sfcode1 = SF;
        fdt = Mn;
        tdt = Yr;
        SalesForce SFD = new SalesForce();       
        DataSet ds = new DataSet();
        ds = SFD.GetLOGIN_Details(SF, Div, Mn, Yr);       
        return JsonConvert.SerializeObject(ds.Tables[0]);       
    }
    //private void FillReporting()
    //{
    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.GetDistributorName(divcode);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlDistributor.DataTextField = "Stockist_Name";
    //        ddlDistributor.DataValueField = "Stockist_Code";
    //        ddlDistributor.DataSource = dsSalesForce;
    //        ddlDistributor.DataBind();
    //        ddlDistributor.Items.Insert(0, new ListItem("--Select Distributor--", "0"));            
    //    }
    //    else
    //    {
    //        Response.Write("Values not added");
    //    }
    //}   
    [WebMethod]
    public static string getMGR(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.GetDistributorName(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    [WebMethod]
    public static string GetData(string SF, string Div, string Mn, string Yr)
    {
        divcode = Div;
        sfcode1 = SF;
        fdt = Mn;
        tdt = Yr;
       SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        ds = SFD.GetDistributorDetails(SF, Div.TrimEnd(','), Mn, Yr);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string GetContactDetails(string Stockist_Code)
    {
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        ds = SFD.GetContactAddressDetails(Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }    
}