using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;

public partial class Stockist_Delivery_Order : System.Web.UI.Page
{
    string divcode = string.Empty;
    DataSet dsDivision = null;
    DataSet dsDivision1 = null;
    string sl_no = string.Empty;
    public static string orderid;
    public static string stockist;
    public static string div;
    public static string cust;
    string sUSR = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            orderid = Request.QueryString["Trans_Sl_No"].ToString();
            div = Request.QueryString["Div_Code"].ToString();
            sUSR = Request.Url.Host.ToLower().Replace("www.", "").Replace(".sanfmcg.com", "").Replace(".salesjump.in", "").ToLower();
            HiddenField1.Value = sUSR;
        }
    }

    [WebMethod(EnableSession = true)]
    public static string BindgrnOrderDetails()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
       // ds = sm.getgrndetails(orderid, div);
        ds = getgrndetails(orderid, div);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static  DataSet getgrndetails(string Order_ID, string Div_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;
        string strQry = " select  Trans_Sl_No,CONVERT(varchar, GRN_Date, 103) as GRN_Date,CONVERT(varchar, Entry_Date, 103) as Entry_Date,LEFt(Supp_Name, 50) as Supp_Name,Po_No, CONVERT(varchar, Dispatch_Date, 103) as Dispatch_Date,gh.SF_Code,gh.Division_code,Net_Tot_Goods,Net_Tot_Tax,Net_Tot_Value,Division_Name,(Division_Add1 + ',' + Division_Add2 + ',' + Division_City + '-' + Division_Pincode)address,Stockist_Name,Stockist_Address,Stockist_Mobile, replace(isnull(ms.Stockist_Address1,''),'0','') as Stockist_Address1,replace(isnull(Stockist_Address1,'0'),'0','') as Sf_Email from trans_grn_head gh join mas_division md on gh.division_code = md.division_code join mas_stockist ms on ms.stockist_code = gh.SF_Code where po_no = '" + Order_ID + "' and gh.division_code = '" + Div_Code + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }


    [WebMethod(EnableSession = true)]
    public static string BindgrnProductdetails(string Order_ID)
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        //ds = sm.getallgrnorderbystockist(Stockist_Code, div.TrimEnd(','), Order_ID);
        ds = getallgrnorderbystockist(Stockist_Code, div.TrimEnd(','), Order_ID);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static  DataSet getallgrnorderbystockist(string Stockist_Code, string Div_Code, string Order_No)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        string strQry = "get_grnorder_ordno '" + Order_No + "','" + Stockist_Code + "','" + Div_Code + "'";

        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
}