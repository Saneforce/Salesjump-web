using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;

public partial class Stockist_Puchase_Order_View : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    public string sf_code = string.Empty;
    public static string Stockist_Code;
    public static string Div_Code;
    public string v;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        if (sf_code == "admin")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
   
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        StockistMaster sm = new StockistMaster();
        v = Request.QueryString["Order_No"].ToString();
        Stockist_Code = Request.QueryString["stockist_Code"].ToString();
        Div_Code = Request.QueryString["Div_Code"].ToString();
    }

    [WebMethod(EnableSession = true)]
    public static string GetPriOrderDetails(string Order_No)
    {
        //string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        //string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        ds = sm.getallorderbystockist(Stockist_Code, Div_Code.TrimEnd(','), Order_No);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}
