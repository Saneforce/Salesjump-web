using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using Newtonsoft.Json;

public partial class Stockist_Credit_Note_List : System.Web.UI.Page
{
    public static DataSet ds;
    public static string Div_code = string.Empty;
    public static StockistMaster sm = new StockistMaster();
    public static string Stockist_Code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string Get_Credit_note(string FDt, string TDt)
    {
        Div_code = HttpContext.Current.Session["div_code"].ToString();
        Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        ds = sm.get_Credit_note_details(Div_code.Replace(",", ""), Stockist_Code, FDt, TDt);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}