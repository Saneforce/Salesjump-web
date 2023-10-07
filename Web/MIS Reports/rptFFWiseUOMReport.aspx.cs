using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Bus_EReport;
using System.Globalization;
using DBase_EReport;

public partial class MIS_Reports_rptFFWiseUOMReport : System.Web.UI.Page
{
    //string mgronly = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        hFYear.Value = Request.QueryString["FYear"].ToString();
        hFMonth.Value = Request.QueryString["FMonth"].ToString();
        hSubDiv.Value = Request.QueryString["SubDiv"].ToString();
        hsfCode.Value = Request.QueryString["SfCode"].ToString();
         mgronly.Value = Request.QueryString["mgronly"].ToString();
        try
        {
            hdivCode.Value = Session["Div_Code"].ToString();
        }
        catch
        {
            hdivCode.Value = Session["Division_Code"].ToString();
        }
        Label1.Text = "Field Force Wise Work Analysis form :   " + Request.QueryString["FYear"].ToString()+"  To "+ Request.QueryString["FMonth"].ToString(); ;
        Label2.Text = "Team : " + Request.QueryString["sfName"].ToString();
        
    }
   

    public class FFO
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
    }

    [WebMethod]
    public static List<FFO> GetFieldForce(string sfCode)
    {
        List<FFO> Lists = new List<FFO>();
        SalesForce sf = new SalesForce();
        string divcode = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsSalesForce = sf.SalesForceList(divcode, sfCode, "0");

        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            FFO list = new FFO();
            list.sfCode = row["sf_code"].ToString();
            list.sfName = row["sf_name"].ToString();
            Lists.Add(list);
        }
        return Lists.ToList();
    }

    public class Order_Values
    {
        public string sfCode { get; set; }
        public string sfManager { get; set; }
        public string sfHQ { get; set; }
        public string sfName { get; set; }
        public string Order_Date { get; set; }
        public string RouteName { get; set; }
        public string Stockist { get; set; }
        public string WorkedWith { get; set; }
        public string newRTCount { get; set; }
        public string OrderVal_Rt { get; set; }
        public string OrderVal_Di { get; set; }
        public string net_weight { get; set; }

    }
    [WebMethod]
    public static List<Order_Values> GetOrderValues(string sfCode, string FDate, string TDate, string SubDiv)
    {
        List<Order_Values> Lists = new List<Order_Values>();
        Order sf = new Order();
        string divcode = HttpContext.Current.Session["div_code"].ToString();
        DateTime df = DateTime.ParseExact(FDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime dt = DateTime.ParseExact(TDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DataSet dsSalesForce = sf.get_OrderValues_UOMWise(divcode, sfCode, df.ToString("yyyy/MM/dd"), dt.ToString("yyyy/MM/dd"), SubDiv);

        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            Order_Values list = new Order_Values();
            list.sfCode = row["sf_code"].ToString();
            list.sfManager = row["rsf"].ToString();
            list.sfHQ = row["Sf_HQ"].ToString();
            list.sfName = row["sf_name"].ToString();
            list.Order_Date = row["Order_Date"].ToString();
            list.RouteName = row["RouteName"].ToString();
            list.Stockist = row["stockist_name"].ToString();
            list.WorkedWith = row["worked_with_name"].ToString();
            list.newRTCount = row["rtCount"].ToString();
            list.OrderVal_Rt = row["Ret_Order_Value"].ToString();
            list.OrderVal_Di = row["Dist_Order_Value"].ToString();
            list.net_weight = row["net_weight"].ToString();
            Lists.Add(list);
        }

        return Lists.ToList();
    }

    [WebMethod]
    public static TotEcTc[] getTcEc(string SF_Code, string FDate, string TDate, string SubDiv)
    {
        string div_code = "";
        //  string sf_Code = "";
        List<TotEcTc> empList = new List<TotEcTc>();
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        //sf_Code = HttpContext.Current.Session["SF_Code"].ToString();

        //   SubDiv = "0";
        DateTime df = DateTime.ParseExact(FDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime dt = DateTime.ParseExact(TDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        Product pro = new Product();

        Expense exp = new Expense();
        DataSet dsPro = exp.getrptIssueSlip_MonthTCEC11(div_code, SF_Code, df.ToString("yyyy/MM/dd"), dt.ToString("yyyy/MM/dd"), SubDiv);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            TotEcTc emp = new TotEcTc();
            emp.sf_Code = row["SF_Code"].ToString();
            emp.Order_Date = row["Order_Date"].ToString();
            emp.TC_Count = row["TC_Count"].ToString();
            emp.EC_Count = row["EC_Count"].ToString();
            empList.Add(emp);
        }


        return empList.ToArray();
    }

    public class TotEcTc
    {
        public string sf_Code { get; set; }
        public string Order_Date { get; set; }
        public string TC_Count { get; set; }
        public string EC_Count { get; set; }

    }


    [WebMethod]
    public static ProductCategory[] getProductCategory(string SubDiv)
    {
        string div_code = "";
        List<ProductCategory> empList = new List<ProductCategory>();
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        Product pro = new Product();
        DataSet dsPro = pro.getProductCategory_UP(div_code);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            ProductCategory emp = new ProductCategory();
            emp.pcatCode = row["Product_Cat_Code"].ToString();
            emp.pcatName = row["Product_Cat_Name"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class ProductCategory
    {
        public string pcatCode { get; set; }
        public string pcatName { get; set; }

    }


    [WebMethod]
    public static ProductUOM[] getUOMData(string SubDiv)
    {
        string div_code = "";
        List<ProductUOM> empList = new List<ProductUOM>();
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        Product pro = new Product();
        DataSet dsPro = pro.getProductUOM(div_code);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            ProductUOM emp = new ProductUOM();
            emp.catCode = row["Product_Cat_Code"].ToString();
            emp.uomName = row["UOM_Weight"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }

    public class ProductUOM
    {
        public string catCode { get; set; }
        public string uomName { get; set; }

    }


    [WebMethod]
    public static ProductUOM_Values[] getUOMProductData(string FDate, string TDate)
    {                                                       
        string div_code = "";
        List<ProductUOM_Values> empList = new List<ProductUOM_Values>();
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        Order pro = new Order();
        DateTime df = DateTime.ParseExact(FDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime dt = DateTime.ParseExact(TDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DataSet dsPro = pro.get_ProductValues_UOMWise(div_code, df.ToString("yyyy/MM/dd"), dt.ToString("yyyy/MM/dd"));
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            ProductUOM_Values emp = new ProductUOM_Values();
            emp.sf_code = row["sf_code"].ToString();
            emp.Order_Date = row["Order_Date"].ToString();
            emp.UOM_Weight = row["UOM_Weight"].ToString();
            emp.Quantity = row["Quantity"].ToString();
            emp.Product_Cat_Code = row["Product_Cat_Code"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }
    public class ProductUOM_Values
    {
        public string sf_code { get; set; }
        public string Order_Date { get; set; }
        public string UOM_Weight { get; set; }
        public string Quantity { get; set; }
        public string Product_Cat_Code { get; set; }
    }
    [WebMethod]
    public static ProductUOM_Values[] getUOMProductDatamgr(string FDate, string TDate)
    {
        string div_code = "";
        List<ProductUOM_Values> empList = new List<ProductUOM_Values>();
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();

        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        Order pro = new Order();
        DateTime df = DateTime.ParseExact(FDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime dt = DateTime.ParseExact(TDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DataSet dsPro = get_ProductValues_UOMWisemgr(div_code, df.ToString("yyyy/MM/dd"), dt.ToString("yyyy/MM/dd"));
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            ProductUOM_Values emp = new ProductUOM_Values();
            emp.sf_code = row["sf_code"].ToString();
            emp.Order_Date = row["Order_Date"].ToString();
            emp.UOM_Weight = row["UOM_Weight"].ToString();
            emp.Quantity = row["Quantity"].ToString();
            emp.Product_Cat_Code = row["Product_Cat_Code"].ToString();
            empList.Add(emp);
        }
        return empList.ToArray();
    }
    public static DataSet get_ProductValues_UOMWisemgr(string DivCode, string FDate, string TDate, string sfCode = "admin")
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsState = null;

        /*strQry = "select h.sf_code,cast(convert(varchar, ModTime, 101) as datetime) Order_Date ,UOM_Weight, sum(Quantity) Quantity,Product_Cat_Code from vwActivity_MSL_Details h inner join Trans_Order_Details d on d.Trans_Sl_No = h.Order_No inner join mas_product_detail p on p.Product_Detail_Code = d.Product_Code where cast(convert(varchar, ModTime,101) as datetime)>= '" + FDate + "'" +
            " and cast(convert(varchar, ModTime,101) as datetime)<= '" + TDate + "'  and h.division_code = '" + DivCode + "' group by h.sf_code,cast(convert(varchar, ModTime, 101) as datetime),UOM_Weight,Product_Cat_Code";
        */
        //string strQry = "exec mgrFldAnalysis '" + sfCode + "','" + DivCode + "','" + FDate + "','" + TDate + "'";
        string strQry = "exec SFFldAnalysis '" + sfCode + "','" + DivCode + "','" + FDate + "','" + TDate + "'";
        try


        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    [WebMethod]
    public static List<Order_Values> GetOrderValuesmgr(string sfCode, string FDate, string TDate, string SubDiv)
    {
        List<Order_Values> Lists = new List<Order_Values>();
        Order sf = new Order();
        string divcode = HttpContext.Current.Session["div_code"].ToString();
        DateTime df = DateTime.ParseExact(FDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime dt = DateTime.ParseExact(TDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DataSet dsSalesForce = get_OrderValues_UOMWisemgr(divcode, sfCode, df.ToString("yyyy/MM/dd"), dt.ToString("yyyy/MM/dd"), SubDiv);

        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            Order_Values list = new Order_Values();
            list.sfCode = row["sf_code"].ToString();
            list.sfManager = row["rsf"].ToString();
            list.sfHQ = row["Sf_HQ"].ToString();
            list.sfName = row["sf_name"].ToString();
            list.Order_Date = row["Order_Date"].ToString();
            list.RouteName = row["RouteName"].ToString();
            list.Stockist = row["stockist_name"].ToString();
            list.WorkedWith = row["worked_with_name"].ToString();
            list.newRTCount = row["rtCount"].ToString();
            list.OrderVal_Rt = row["Ret_Order_Value"].ToString();
            list.OrderVal_Di = row["Dist_Order_Value"].ToString();
            list.net_weight = row["net_weight"].ToString();
            Lists.Add(list);
        }

        return Lists.ToList();
    }
    public static DataSet get_OrderValues_UOMWisemgr(string DivCode, string SF_Code, string FDate, string TDate, string SubDiv)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsState = null;

        string strQry = "exec GET_FFWiseUOM_Ordermgr '" + DivCode + "','" + SF_Code + "','" + FDate + "','" + TDate + "','" + SubDiv + "'";
        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
}