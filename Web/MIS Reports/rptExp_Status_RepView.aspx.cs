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
using System.Globalization;
public partial class MIS_Reports_rptExp_Status_RepView : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
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
        modes = "";

        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        lblhead1.Text = "Manufacturing Status Report for " + mfi.GetAbbreviatedMonthName(Convert.ToInt32(FMonth)) + " - " + FYear;

        ddlFieldForce.Value = sf_code;
        ddlFYear.Value = FYear;
        ddlFMonth.Value = FMonth;
        SubDivCode.Value = SubDiv;
        hdnDate.Value = modes;
        Label1.Text = "Field Force :" + Request.QueryString["SFName"].ToString();
       
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
        DataSet dsPro = dcn.getExp_status_rep(div_code, SF_Code, FYera, FMonth, "0", ModeDt);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            GetDatas emp = new GetDatas();
            emp.StateName = row["StateName"].ToString();
            emp.stockist_name = row["stockist_name"].ToString();
            emp.Entered_Month = row["Entered_Month"].ToString();
            emp.sf_code = row["sf_code"].ToString();
            emp.Sf_Name = row["Sf_Name"].ToString();
            emp.SDP_Name = row["SDP_Name"].ToString();
            emp.ListedDrCode = row["ListedDrCode"].ToString();
            emp.ListedDr_Name = row["ListedDr_Name"].ToString();
            emp.Product_Detail_Name = row["Product_Detail_Name"].ToString();
            emp.Mf_Date = row["Mf_Date"].ToString();
            emp.Quantity = row["Quantity"].ToString();
            emp.Rate = row["Rate"].ToString();
            emp.value = row["value"].ToString();
            emp.net_weight = row["net_weight"].ToString();

            //emp.cnt = row["cnt"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class GetDatas
    {
        public string StateName { get; set; }
        public string stockist_name { get; set; }
        public string Entered_Month { get; set; }
        public string sf_code { get; set; }
        public string Sf_Name { get; set; }
        public string SDP_Name { get; set; }
        public string ListedDrCode { get; set; }
        public string ListedDr_Name { get; set; }
        public string Product_Detail_Name { get; set; }
        public string Mf_Date { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string value { get; set; }
        public string net_weight { get; set; }

    }
   
}