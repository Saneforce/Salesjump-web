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
public partial class MIS_Reports_rptOrderDetailedView : System.Web.UI.Page
{
    #region "Declaration"      
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string SubDiv = string.Empty;
    string FYear = string.Empty;
    string FMonth = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Request.QueryString["SFCode"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        SubDiv = Request.QueryString["SubDiv"].ToString();
		
		string FDate = FYear.ToString();
        string TDate = FMonth.ToString();
        string subdiv_code = "0";

        if(FDate.Contains('/')&& TDate.Contains('/'))
        {
            string[] FDate1 = FDate.Split('/');
            string[] TDate1 = TDate.Split('/');

            FYear = Convert.ToString(FDate1[2] + "-" + FDate1[1] + "-" + FDate1[0]);
            FMonth = Convert.ToString(TDate1[2] + "-" + TDate1[1] + "-" + TDate1[0]);            
        }
        
		
        ddlFieldForce.Value = sf_code;
        ddlFYear.Value = FYear;
        ddlFMonth.Value = FMonth;
        SubDivCode.Value = SubDiv;
        Label1.Text="Team : " + Request.QueryString["SFName"].ToString();
    }
	
    [WebMethod(EnableSession = true)]
    public static GetDatas[] GetData(string SF_Code, string FYera, string FMonth,string SubDivCode)
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
        List<GetDatas> empList = new List<GetDatas>();
        Order od = new Order();
        DataSet dsOrder = od.GetOrderDetailsWithPrice(div_code, SF_Code, FYera, FMonth, "0");
        foreach (DataRow row in dsOrder.Tables[0].Rows)
        {
            GetDatas emp = new GetDatas();
            emp.sfCode = row["SF_Code"].ToString();
            emp.sfName = row["sf_name"].ToString();

            emp.sfHQ = row["sf_hq"].ToString();
            emp.sfEmpId = row["sf_emp_id"].ToString();

            emp.sfRSF = row["Reporting_To_SF"].ToString();
            emp.distCode = row["stockist_code"].ToString();
            emp.distName = row["stockist_name"].ToString();
            emp.routeName = row["SDP_Name"].ToString();
            emp.custCode = row["Trans_Detail_Info_Code"].ToString();
            emp.custName = row["Trans_Detail_Name"].ToString();
            emp.custType = row["Doc_Spec_ShortName"].ToString();
            emp.ordNum = row["Order_No"].ToString();
            emp.ordDate = Convert.ToDateTime(row["Activity_Date"]).ToString("dd/MM/yyyy");
            //emp.ordTime = row["tm"].ToString();
            emp.prdCode = row["Product_Code"].ToString();
            emp.prdName = row["Product_Name"].ToString();

            emp.CnvQty = row["CnvQty"].ToString();
            emp.cQTY = row["cQty"].ToString();
            emp.pQTY = row["pQty"].ToString();
            emp.QTY = row["Qty"].ToString();
            emp.Rate = row["Rate"].ToString();
            emp.discount = row["discount_price"].ToString();
            emp.Price = row["Price"].ToString();
            emp.DistributorPrice = row["MRP_Price"].ToString();
            emp.RetailerPrice = row["Retailor_Price"].ToString();
			emp.free = row["free"].ToString();
            emp.taxamout = row["taxamout"].ToString();
            emp.netvalue = row["netvalue"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class GetDatas
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string sfHQ { get; set; }
        public string sfEmpId { get; set; }

        public string sfRSF{ get; set; }
        public string distCode { get; set; }
        public string distName { get; set; }
        public string routeName { get; set; }
        public string custCode { get; set; }
        public string custName { get; set; }
        public string custType { get; set; }
        public string ordNum { get; set; }
        public string ordDate { get; set; }
        public string ordTime { get; set; }
        public string prdCode { get; set; }
        public string prdName { get; set; }

        public string CnvQty { get; set; }
        public string cQTY { get; set; }
        public string pQTY { get; set; }
        public string QTY { get; set; }
        public string Rate { get; set; }
        public string discount { get; set; }
        public string Price { get; set; }
        public string DistributorPrice { get; set; }
        public string RetailerPrice { get; set; }
        public string free { get; set; }
        public string taxamout { get; set; }
        public string netvalue { get; set; }
    }


    public class ReportingSF
    {
        public string Sf_Code { get; set; }
        public string sf_Name { get; set; }
        public string RSF_Code { get; set; }
        public string Designation { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static ReportingSF[] GetReportingToSF()
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
        DataSet dsSFHead = new DataSet();
        dsSFHead = sf.SalesForceListMgrGet_MgrOnly(div_code, "admin", "0");
        List<ReportingSF> dsDtls = new List<ReportingSF>();
        foreach (DataRow row in dsSFHead.Tables[0].Rows)
        {
            ReportingSF dtls = new ReportingSF();
            dtls.Sf_Code = row["Sf_Code"].ToString();
            dtls.sf_Name = row["Sf_Name"].ToString();
            dtls.RSF_Code = row["Reporting_To_SF"].ToString();
            dtls.Designation = row["Designation"].ToString();

            dsDtls.Add(dtls);
        }
        return dsDtls.ToArray();
    }

}