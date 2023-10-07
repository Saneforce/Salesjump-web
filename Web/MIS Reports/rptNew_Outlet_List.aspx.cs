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
public partial class MIS_Reports_rptNew_Outlet_List : System.Web.UI.Page
{
    #region "Declaration"      
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    public static string div_code = string.Empty;
    string SubDiv = string.Empty;
    string FYear = string.Empty;
    string FMonth = string.Empty;
    string modes = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["SFCode"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        SubDiv = Request.QueryString["SubDiv"].ToString();
        try
        {
            modes = Request.QueryString["Dates"].ToString();

        }

 
        catch (Exception ex)
        {
        }

        ddlFieldForce.Value = sf_code;
        ddlFYear.Value = FYear;
        ddlFMonth.Value = FMonth;
        SubDivCode.Value = SubDiv;
        hdnDate.Value = modes;
        if (modes == string.Empty)
        {
            Label1.Visible = true;
            Label1.Text = "Field Force :" + Request.QueryString["SFName"].ToString();
        }
        else
        {
            Label1.Visible = false;
        }
    }


    [WebMethod(EnableSession = true)]
    public static GetDatas[] GetData(string SF_Code, string FYera, string FMonth, string SubDivCode, string ModeDt)
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

        List<GetDatas> empList = new List<GetDatas>();
        DCR_New dcn = new DCR_New();
        DataSet dsPro = dcn.getNew_outlet_List(div_code, SF_Code, FYera, FMonth, SubDivCode, ModeDt);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            GetDatas emp = new GetDatas();
            emp.ListedDrCode = row["ListedDrCode"].ToString();
            emp.ListedDr_Name = row["ListedDr_Name"].ToString();
            emp.Sf_Code = row["Sf_Code"].ToString();
            emp.RouteName = row["RouteName"].ToString();
            emp.Dist_Name = row["Dist_Name"].ToString();
            emp.Retailer_Channel = row["Retailer_Channel"].ToString();
            emp.Mobile_No = row["Mobile_No"].ToString();
            emp.Retailer_Class = row["Retailer_Class"].ToString();
            emp.ContactPerson = row["ContactPerson"].ToString();
            emp.GSTNO = row["GSTNO"].ToString();
            emp.ListedDr_Email = row["Email"].ToString();
            emp.Address = row["Address"].ToString();
            emp.City = row["City"].ToString();
            emp.PinCode = row["PinCode"].ToString();
            emp.order_val = row["Order_Value"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class GetDatas
    {
        public string ListedDrCode { get; set; }
        public string ListedDr_Name { get; set; }
        public string Sf_Code { get; set; }
        public string Dist_Name { get; set; }
        public string Retailer_Channel { get; set; }
        public string Mobile_No { get; set; }
        public string Retailer_Class { get; set; }
        public string ContactPerson { get; set; }
        public string GSTNO { get; set; }
        public string ListedDr_Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string order_val { get; set; }
        public string RouteName { get; set; }

    }
    [WebMethod(EnableSession = true)]
    public static string getSalesforce()
    {
        string strQry = string.Empty;
        DataSet dsTerritory = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select Sf_Code,Sf_Name from Mas_Salesforce where CHARINDEX('," + div_code.TrimEnd(',') + ",',','+Division_Code+',')>0 and SF_Status<>2", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsTerritory);
        con.Close();
        return JsonConvert.SerializeObject(dsTerritory.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getStockist()
    {
        string strQry = string.Empty;
        DataSet dsTerritory = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select Stockist_Code,Stockist_Name from Mas_Stockist where Division_Code=" + div_code.TrimEnd(',') + " and Stockist_Active_Flag=0", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsTerritory);
        con.Close();
        return JsonConvert.SerializeObject(dsTerritory.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string chkRetailers()
    {
        string strQry = string.Empty;
        DataSet dsTerritory = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec getMGRRetailerCount '109','',''", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsTerritory);
        con.Close();
        return JsonConvert.SerializeObject(dsTerritory.Tables[0]);
    }
}