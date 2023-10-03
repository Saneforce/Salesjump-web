using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_Rpt_Distributor_stock_details : System.Web.UI.Page
{
    public static string Sf_code = string.Empty;
    string div_code = string.Empty;
    public static string Stocode = string.Empty;
    string Distri_name = string.Empty;
    public static DataSet ds = new DataSet();
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sf_code = Request.QueryString["sfCode"].ToString();
        Stocode = Request.QueryString["Dist_Code"].ToString();
        Distri_name = Request.QueryString["Distri_name"].ToString();
        FDT = Request.QueryString["fdate"].ToString();
        TDT = Request.QueryString["tdate"].ToString();
        div_code = Session["div_code"].ToString();
        DateTime d1 = Convert.ToDateTime(FDT);
        lblHead.Text = "Productwise Stock Details  for " + d1.ToString("dd-MM-yyyy") + "";
        lblsf_name.Text = Distri_name;
    }
    [WebMethod(EnableSession = true)]
    public static string Get_Stock(string divcode)
    {
        //DataSet ds = new DataSet();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        StockistMaster sm = new StockistMaster();       
        ds = get_Product_stock(Stocode, div_code, FDT, TDT);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet get_Product_stock(string Stocode, string div_code, string FDT,string TDT)
    {
        string strQry = string.Empty;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();
        DataSet ds = null;       
        strQry = "Exec getDistStockLedger '" + Stocode + "','" + FDT + "','" + TDT + "'";
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