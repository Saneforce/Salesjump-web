using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Globalization;
using Newtonsoft.Json;


public partial class Stockist_Counter_Sales_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod(EnableSession = true)]
    public static string GetCounterDetails(string FDT, string TDT)
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        ds = sm.getallCounterSalesDetails(Stockist_Code, FDT, TDT, Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
  }
}