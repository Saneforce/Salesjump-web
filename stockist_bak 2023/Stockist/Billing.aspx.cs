using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Stockist_Billing : System.Web.UI.Page
{
    public static DataSet dsListedDR = null;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string GetInvoice(string FDt, string TDt,string Type,string Retailer)
    {
        DataSet ds = new DataSet();
        string sfcode = HttpContext.Current.Session["Sf_Code"].ToString();
        //SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        //con.Open();
        //SqlCommand cmd = new SqlCommand("EXEC get_PageWise_bill '" + FDt +"', '" + TDt + "' , '" + sfcode + "','"+ Type + "','" + Retailer + "' ", con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //da.Fill(ds);
        //con.Close();
        String SqlQry = " Select Trans_Inv_Slno from Trans_invoice_head  " +
                        "where convert(date, Invoice_Date) between '" + FDt + "' and '" + TDt + "'  and Sf_Code ='" + sfcode + "' " +
                        "and Cus_Code IN(select * from SplitString('" + Retailer + "', ','))";
        DBase_EReport.DB_EReporting db = new DBase_EReport.DB_EReporting();
        ds = db.Exec_DataSet(SqlQry);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string GetProductdetails(string orderid)
    {
        dsListedDR = GetProducts(orderid);
        return JsonConvert.SerializeObject(dsListedDR.Tables[0]);
    }

    public static DataSet GetProducts(string order_id)
    {
        string strQry = "";
        DataSet dsAdmin = new DataSet();
        DBase_EReport. DB_EReporting dbER = new DBase_EReport.DB_EReporting();

        strQry = " exec spGetMultipleInvoiceProduct '" + order_id + "'";
        try
        {
            dsAdmin = dbER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }

    [WebMethod]
    public static string GetDistributor(string cust_code,string order_id)
    {        
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        dsListedDR = GetDistributr(Div_Code, Stockist_Code, cust_code, order_id);
        return JsonConvert.SerializeObject(dsListedDR.Tables[0]);
    }

    public static DataSet GetDistributr(string div_code, string stockist_code, string cust_code, string order_id)
    {
        string strQry = "";
        DataSet dsAdmin = new DataSet();
        DBase_EReport.DB_EReporting dbER = new DBase_EReport.DB_EReporting();

        strQry = " exec spGeMultipletInvoiceDistRet '" + div_code + "','" + stockist_code + "','" + cust_code + "','" + order_id + "'";
        try
        {
            dsAdmin = dbER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }

    [WebMethod]
    public static string getRoute(string FDt, string TDt, string Type)
    {
        string strQry = "";
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsTerr = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "EXEC get_InvoiceRoutes '" + FDt + "','" + TDt + "','" + Stockist_Code + "','" + Type + "'";
        try
        {
            dsTerr = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dsTerr.Tables[0]);
    }

    [WebMethod]
    public static string getRetailer(string FDt, string TDt, string Type,string Territory)
    {
        string strQry = "";
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsRet = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "EXEC get_RetailerBasedOnRoute '" + FDt + "','" + TDt + "','" + Stockist_Code + "','" + Type + "','" + Territory + "' ";
        try
        {
            dsRet = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dsRet.Tables[0]);
    }
}