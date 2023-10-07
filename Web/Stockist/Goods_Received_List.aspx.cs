using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using Bus_EReport;

using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;

public partial class MasterFiles_Goods_Received_List : System.Web.UI.Page
{
    string a = string.Empty;
    string b = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    public static string GetgrnDetails(string Stockist_Code, string FDT, string TDT)
    {
        string StkCode = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        ds = sm.getallgrnorderdetails(StkCode, Div_Code.TrimEnd(','), FDT, TDT);
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }

}
