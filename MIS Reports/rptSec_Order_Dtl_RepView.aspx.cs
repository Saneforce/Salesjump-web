﻿using System;
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
public partial class MIS_Reports_rptSec_Order_Dtl_RepView : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string SubDiv = string.Empty;
    string FYear = string.Empty;
    string FMonth = string.Empty;
    string modes = string.Empty;
    public static string ToDt = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["SFCode"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        SubDiv = Request.QueryString["SubDiv"].ToString();
        modes = Request.QueryString["FromDt"].ToString();
        ToDt = Request.QueryString["ToDt"].ToString();

        DateTime result = DateTime.ParseExact(modes, "yyyy-MM-d", CultureInfo.InvariantCulture);
        string dt = result.ToString("dd/MM/yyyy");
        DateTime result1 = DateTime.ParseExact(ToDt, "yyyy-MM-d", CultureInfo.InvariantCulture);
        string dt2 = result1.ToString("dd/MM/yyyy");


        lblhead1.Text = "Product Wise Secondary Order Report from " + dt + " to " + dt2;
        
       

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
        DataSet dsPro = dcn.getSec_sale_Val(div_code, SF_Code, FYera, FMonth, "0", ModeDt, ToDt);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            GetDatas emp = new GetDatas();
            emp.order_no = row["Order_No"].ToString();
            emp.RSFs = row["Reporting_To_SF"].ToString();
            emp.EmpID = row["sf_emp_id"].ToString();
            emp.User_rank = row["sf_Designation_Short_Name"].ToString();
            emp.sf_hq = row["Sf_HQ"].ToString();
            emp.Dis_Code = row["stockist_code"].ToString();
            emp.Dis_name = row["stockist_name"].ToString();
            emp.Region = row["Area"].ToString();
            emp.Zone = row["Zz"].ToString();
            emp.Terr = row["Territory"].ToString();
            emp.Beats = row["Route_Name"].ToString();
            emp.sfName = row["Sf_Name"].ToString();
            emp.sfCode = row["Sf_Code"].ToString();
            emp.ListedDr_Name = row["ListedDr_Name"].ToString();
            emp.channel = row["Retailer_Channel"].ToString();
            emp.class_name = row["Retailer_Class"].ToString();
            emp.ListedDr_code = row["ListedDrCode"].ToString();
            emp.ListedDr_Name_per = row["Contact_Person_Name"].ToString();
            emp.ListedDr_Mobile = row["ListedDr_Mobile"].ToString();
            emp.ListedDr_Email = row["ListedDr_Email"].ToString();
            emp.GST = row["GST"].ToString();
            emp.Address = row["Address"].ToString();
            emp.Market = row["Route_Name"].ToString();
            emp.cityname = row["cityname"].ToString();
            emp.PIN_Code = row["PIN_Code"].ToString();
            emp.StateName = row["StateName"].ToString();
            emp.Outlets_Created = row["Created_Date"].ToString();
            emp.Order_Date = row["Order_Date"].ToString();
            emp.Time = row["tm"].ToString();
            emp.Sec_cat = row["Product_Cat_Name"].ToString();
            emp.ProductCode = row["Product_Detail_Code"].ToString();
            emp.Product = row["Product_Detail_Name"].ToString();
            emp.Qty = row["Quantity"].ToString();
            emp.Unit = row["product_unit"].ToString();
            emp.Discount = row["discount"].ToString();
            emp.Price = row["Rate"].ToString();
            emp.Sale_Value = row["value"].ToString();
            emp.Net_Value = row["net_weight"].ToString();
            emp.Remarks = row["Activity_Remarks"].ToString();

            //emp.cnt = row["cnt"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();


    }


    [WebMethod(EnableSession = true)]
    public static GetDatas1[] GetData1(string SF_Code, string FYera, string FMonth, string SubDivCode, string ModeDt)
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


        List<GetDatas1> empData = new List<GetDatas1>();
        DCR_New dcn = new DCR_New();
        DataSet dsPro = dcn.getSec_sale_Val(div_code, SF_Code, FYera, FMonth, "0", ModeDt, ToDt);

        empData = new List<GetDatas1>();

        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            GetDatas1 emp = new GetDatas1();
            emp.order_no = row["Order_No"].ToString();
            emp.RSFs = row["Reporting_To_SF"].ToString();
            emp.EmpID = row["sf_emp_id"].ToString();
            emp.User_rank = row["sf_Designation_Short_Name"].ToString();
            emp.sf_hq = row["Sf_HQ"].ToString();
            //emp.Dis_Code = row["stockist_code"].ToString();
            emp.Dis_name = row["stockist_name"].ToString();
            emp.Region = row["Area"].ToString();
            emp.Zone = row["Zz"].ToString();
            emp.Terr = row["Territory"].ToString();
            emp.Beats = row["Route_Name"].ToString();
            emp.sfName = row["Sf_Name"].ToString();
            emp.sfCode = row["Sf_Code"].ToString();
            emp.ListedDr_Name = row["ListedDr_Name"].ToString();
            emp.channel = row["Retailer_Channel"].ToString();
            emp.class_name = row["Retailer_Class"].ToString();
            emp.ListedDr_code = row["ListedDrCode"].ToString();
            emp.ListedDr_Name_per = row["Contact_Person_Name"].ToString();
            emp.ListedDr_Mobile = row["ListedDr_Mobile"].ToString();
            emp.ListedDr_Email = row["ListedDr_Email"].ToString();
            emp.GST = row["GST"].ToString();
            emp.Address = row["Address"].ToString();
            emp.Market = row["Route_Name"].ToString();
            emp.cityname = row["cityname"].ToString();
            emp.PIN_Code = row["PIN_Code"].ToString();
            emp.StateName = row["StateName"].ToString();
            emp.Outlets_Created = row["Created_Date"].ToString();
            emp.Order_Date = row["Order_Date"].ToString();
            emp.Time = row["tm"].ToString();
            emp.Sec_cat = row["Product_Cat_Name"].ToString();
            //emp.ProductCode = row["Product_Detail_Code"].ToString();
            emp.Product = row["Product_Detail_Name"].ToString();
            emp.Qty = row["Quantity"].ToString();
            emp.Unit = row["product_unit"].ToString();
            emp.Discount = row["discount"].ToString();
            emp.Price = row["Rate"].ToString();
            emp.Sale_Value = row["value"].ToString();
            emp.Net_Value = row["net_weight"].ToString();
            emp.Remarks = row["Activity_Remarks"].ToString();

            //emp.cnt = row["cnt"].ToString();
            empData.Add(emp);
        }

        return empData.ToArray();
     }
	
    public class GetDatas
    {
        public string order_no { get; set; }
        public string RSFs { get; set; }
        public string EmpID { get; set; }
        public string User_rank { get; set; }
        public string sf_hq { get; set; }

        public string Dis_Code { get; set; }

        public string Dis_name { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Beats { get; set; }
        public string Terr { get; set; }
        public string sfName { get; set; }
        public string sfCode { get; set; }
        public string ListedDr_Name { get; set; }
        public string channel { get; set; }
        public string class_name { get; set; }
        public string ListedDr_code { get; set; }
        public string ListedDr_Name_per { get; set; }
        public string ListedDr_Mobile { get; set; }
        public string ListedDr_Email { get; set; }
        public string GST { get; set; }
        public string Address { get; set; }
        public string Market { get; set; }
        public string cityname { get; set; }
        public string PIN_Code { get; set; }
        public string StateName { get; set; }
        public string Outlets_Created { get; set; }
        public string Order_Date { get; set; }
        public string Time { get; set; }
        public string Sec_cat { get; set; }

        public string ProductCode { get; set; }
        public string Product { get; set; }
        public string Qty { get; set; }
        public string Unit { get; set; }
        public string Discount { get; set; }
        public string Price { get; set; }
        public string Sale_Value { get; set; }
        public string Net_Value { get; set; }
        public string Remarks { get; set; }
      

    }

    public class GetDatas1
    {
        public string order_no { get; set; }
        public string RSFs { get; set; }
        public string EmpID { get; set; }
        public string User_rank { get; set; }
        public string sf_hq { get; set; }

        //public string Dis_Code { get; set; }

        public string Dis_name { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Beats { get; set; }
        public string Terr { get; set; }
        public string sfName { get; set; }
        public string sfCode { get; set; }
        public string ListedDr_Name { get; set; }
        public string channel { get; set; }
        public string class_name { get; set; }
        public string ListedDr_code { get; set; }
        public string ListedDr_Name_per { get; set; }
        public string ListedDr_Mobile { get; set; }
        public string ListedDr_Email { get; set; }
        public string GST { get; set; }
        public string Address { get; set; }
        public string Market { get; set; }
        public string cityname { get; set; }
        public string PIN_Code { get; set; }
        public string StateName { get; set; }
        public string Outlets_Created { get; set; }
        public string Order_Date { get; set; }
        public string Time { get; set; }
        public string Sec_cat { get; set; }

        //public string ProductCode { get; set; }
        public string Product { get; set; }
        public string Qty { get; set; }
        public string Unit { get; set; }
        public string Discount { get; set; }
        public string Price { get; set; }
        public string Sale_Value { get; set; }
        public string Net_Value { get; set; }
        public string Remarks { get; set; }


    }
	
    [WebMethod(EnableSession = true)]
    public static GetDatasD[] GetDataD(string SF_Code, string FYera, string FMonth)
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



        List<GetDatasD> empListD = new List<GetDatasD>();
        SalesForce dcn = new SalesForce();
        DataSet dsPro = dcn.SalesForceListMgrGet_MgrOnly(div_code, "admin", "0");
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            GetDatasD emp = new GetDatasD();

            emp.Sf_Name = row["Sf_Name"].ToString();
            emp.Sf_Code = row["Sf_Code"].ToString();
            emp.RSF_Code = row["Reporting_To_SF"].ToString();
            emp.Designation = row["Designation"].ToString();

            empListD.Add(emp);
        }
        return empListD.ToArray();
    }

    public class GetDatasD
    {

        public string Sf_Name { get; set; }
        public string Sf_Code { get; set; }
        public string RSF_Code { get; set; }
        public string Designation { get; set; }

    }

    //[WebMethod(EnableSession = true)]
    //public static GetDatas_no[] GetData_no(string SF_Code, string FYera, string FMonth, string SubDivCode, string ModeDt)
    //{
    //    string div_code = "";
    //    string sf_Code = "";
    //    try
    //    {
    //        div_code = HttpContext.Current.Session["div_code"].ToString();

    //    }
    //    catch
    //    {
    //        div_code = HttpContext.Current.Session["Division_Code"].ToString();
    //    }

    //    sf_Code = HttpContext.Current.Session["SF_Code"].ToString();



    //    List<GetDatas_no> empList = new List<GetDatas_no>();
    //    DCR_New dcn = new DCR_New();
    //    DataSet dsPro = dcn.getNew_outlet_List_no(div_code, SF_Code, FYera, FMonth, "0", ModeDt);
    //    foreach (DataRow row in dsPro.Tables[0].Rows)
    //    {
    //        GetDatas_no emp = new GetDatas_no();
    //        emp.ListedDrCode = row["ListedDrCode"].ToString();
    //        emp.ListedDr_Name = row["ListedDr_Name"].ToString();
    //        emp.Sf_Code = row["Sf_Code"].ToString();
    //        emp.sf_name = row["sf_name"].ToString();
    //        emp.Retailer_Channel = row["Retailer_Channel"].ToString();
    //        emp.Mobile_No = row["Mobile_No"].ToString();
    //        emp.Retailer_Class = row["Retailer_Class"].ToString();
    //        emp.ContactPerson = row["ContactPerson"].ToString();
    //        emp.GSTNO = row["GSTNO"].ToString();
    //        emp.ListedDr_Email = row["Email"].ToString();
    //        emp.Address = row["Address"].ToString();
    //        emp.City = row["City"].ToString();
    //        emp.PinCode = row["PinCode"].ToString();
           
    //        empList.Add(emp);
    //    }
    //    return empList.ToArray();
    //}

    //public class GetDatas_no
    //{
    //    public string ListedDrCode { get; set; }
    //    public string ListedDr_Name { get; set; }
    //    public string Sf_Code { get; set; }
    //    public string sf_name { get; set; }
    //    public string Retailer_Channel { get; set; }
    //    public string Mobile_No { get; set; }
    //    public string Retailer_Class { get; set; }
    //    public string ContactPerson { get; set; }
    //    public string GSTNO { get; set; }
    //    public string ListedDr_Email { get; set; }
    //    public string Address { get; set; }
    //    public string City { get; set; }
    //    public string PinCode { get; set; }
       

    //}



}