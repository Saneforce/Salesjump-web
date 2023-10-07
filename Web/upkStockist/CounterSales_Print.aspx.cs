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

public partial class Stockist_CounterSales_Print : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static DataSet dsListedDR = null;
    public static DataSet dsDivision1 = null;
    public static string sl_no = string.Empty;
    public static string sf_type = string.Empty;

    public static string Order_id;
    public static string Stockist_Code;
    public static string Div_Code;
    public static string dt;
    public static string cust;
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                Order_id = Request.QueryString["Order_id"].ToString();
                Stockist_Code = Request.QueryString["Stockist_Code"].ToString();
                Div_Code = Request.QueryString["Div_Code"].ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    [WebMethod]
    public static string GetcountersaleDetail()
    {
        //ListedDR lstDR = new ListedDR();
        dsListedDR = Getcountersale(Div_Code, Stockist_Code, Order_id);
        return JsonConvert.SerializeObject(dsListedDR.Tables[0]);
    }
    public static DataSet Getcountersale(string div_code, string stockist_code, string order_id)
    {
        string strQry = "";
        DataSet dsAdmin = new DataSet();
        DB_EReporting dbER = new DB_EReporting();

        //strQry = " exec GetCounterSale '" + div_code + "','" + stockist_code + "','" + order_id + "'";
        strQry = "select tch.Trans_Count_Slno,convert(varchar,tch.Order_Date,3)dt,tch.Tax_Total,tcd.Product_Code,tcd.Product_Name,tcd.Price,tcd.Quantity,tcd.Amount" +
                 ",ms.Stockist_Code,ms.Stockist_Name,ms.Stockist_Address,ms.Stockist_Mobile,isnull(ms.gstn, '')gstn,ms.ERP_Code,mp.HSN_Code from mas_stockist ms " +
                 " inner join Trans_CounterSales_Head tch on tch.Dis_Code = ms.Stockist_Code and tch.Order_No = '" + order_id + "' " +
                 "inner join Trans_CounterSales_details tcd on tcd.Trans_Count_Slno = tch.Trans_Count_Slno " +
                 "inner join mas_product_detail mp on mp.Product_Detail_Code = tcd.Product_Code " +
                 "where ms.Stockist_Active_Flag = 0 and ms.Division_Code = '" + div_code + "'  and ms.Stockist_Code = '" + stockist_code + "' order by tch.Order_Date desc ";
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