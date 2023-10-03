using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using Bus_EReport;
using Newtonsoft.Json;

public partial class SuperStockist_Sales_Payment_SS_Payment_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    public static string Get_Payment_detail(string FDT, string TDT)
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        ds = sm.get_paymt_det(Stockist_Code, FDT, TDT);
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }
}