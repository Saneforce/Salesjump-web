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

public partial class MIS_Reports_rpt_TotalOrders_Chennel_wise : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string SubDiv = string.Empty;
    string Fdate = string.Empty;
    string Tdate = string.Empty;
    string chl = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
       sf_code = Request.QueryString["Sf_Code"].ToString();
        Fdate = Request.QueryString["FromDate"].ToString();
        Tdate = Request.QueryString["ToDate"].ToString();
       chl = Request.QueryString["Channel"].ToString();

       // DateTime dtfrom = DateTime.ParseExact(FYear, "dd/MM/yyyy", null);
        //string strfrom = dtfrom.ToString("yyyy-MM-dd");

       // DateTime dtto = DateTime.ParseExact(FMonth, "dd/MM/yyyy", null);
       // string strto = dtto.ToString("yyyy-MM-dd");

        Label2.Text = "Channel wise Order Details From : " + Fdate + " To " + Tdate;

       ddlFieldForce.Value = sf_code;
        ddlFYear.Value = Fdate;
        ddlFMonth.Value = Tdate;
        Channal.Value = chl;
       // Label1.Text = " <span style='color:blue'>Team Name :</span>" + Request.QueryString["SFName"].ToString();
    }
    public class Item
    {
        public string product_id { get; set; }
        public string product_name { get; set; }
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
    public static IssueDetai[] getIssuData(string sf_code,string FYera, string FMonth, string chl)
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

      //  sf_Code = HttpContext.Current.Session["SF_Code"].ToString();
        //chl = HttpContext.Current.Session["Chennel"].ToString();
        //   SubDiv = "0";

        Product pro = new Product();
        List<IssueDetai> empList = new List<IssueDetai>();
        SalesForce exp = new SalesForce();
        DataSet dsPro = exp.GET_RETAIL_product_CHANNEL_WISE(sf_code, div_code, FYera, FMonth, chl);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            IssueDetai emp = new IssueDetai();
            emp.SFCode = row["Sf_Code"].ToString();
            emp.SFName = row["Sf_Name"].ToString();
            emp.Product_Code = row["Product_Code"].ToString();
            emp.Product_Name = row["Product_Detail_Name"].ToString();
            emp.quantity = row["quantity"].ToString();
            emp.value = row["value"].ToString();
            // emp.TC_Count = row["TC_Count"].ToString();
            emp.net_weight_value = row["net_weight_value"].ToString();

            emp.ListedDrCode = row["ListedDrCode"].ToString();
            emp.Doc_Special_Name = row["ListedDr_Name"].ToString();
            emp.ListedDr_Name = row["ListedDr_Name"].ToString();
            emp.Territory_Name = row["Territory_Name"].ToString();
            emp.DistCode = row["Stockist_Code"].ToString();
            emp.DistName = row["Stockist_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class IssueDetai
    {

        public string SFCode { get; set; }
        public string SFName { get; set; }
        public string Product_Code { get; set; }
        public string Product_Name { get; set; }
        public string quantity { get; set; }
        public string value { get; set; }
        public string net_weight_value { get; set; }
        public string Doc_Special_Code { get; set; }
        public string Doc_Special_Name { get; set; }
        public string ListedDrCode { get; set; }
        public string ListedDr_Name { get; set; }
        public string Territory_Name { get; set; }
        public string DistCode { get; set; }
        public string DistName { get; set; }

    }

}