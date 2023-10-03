using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Stockist_GstInvoiceWithoutMaster : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static DataSet dsListedDR = null;
    public static DataSet dsDivision1 = null;
    public static string sl_no = string.Empty;
    public static string sf_type = string.Empty;
    public static string orderid;
    public static string stockist;
    public static string div;
    public static string cust;
    string sUSR = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!Page.IsPostBack)
        {
            try
            {
                orderid = Request.QueryString["Order_id"].ToString();
                hid_order_id.Value = Request.QueryString["Order_id"].ToString();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        sUSR = Request.Url.Host.ToLower().Replace("www.", "").Replace(".sanfmcg.com", "").Replace(".salesjump.in", "").ToLower();
        HiddenField1.Value = sUSR;
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
        DB_EReporting dbER = new DB_EReporting();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        //strQry = " exec spGetMultipleInvoiceProduct '" + order_id + "','" + Div_Code + "','" + Stockist_Code + "'";
        strQry = " exec [spSS_GetMultipleInvoiceProduct] '" + order_id + "','" + Div_Code + "','" + Stockist_Code + "'";
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
    public static string GetDistributor(string cust_code, string order_id)
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
        DB_EReporting dbER = new DB_EReporting();

        strQry = " exec [spSS_GeMultipletInvoiceDistRet] '" + div_code + "','" + stockist_code + "','" + cust_code + "','" + order_id + "'";
        //strQry = " exec spGeMultipletInvoiceDistRet '" + div_code + "','" + stockist_code + "','" + cust_code + "','" + order_id + "'";
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
    public static string GetDivision(string divcode)
    {
        //ListedDR lstDR = new ListedDR();
        dsListedDR = GetDivis(divcode);
        return JsonConvert.SerializeObject(dsListedDR.Tables[0]);
    }
    public static DataSet GetDivis(string divcode)
    {
        string strQry = "";
        DataSet dsAdmin = new DataSet();
        DB_EReporting dbER = new DB_EReporting();

        strQry = " Select Division_Code,Division_Name  from mas_Division where Division_code='" + divcode + "'";
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

}