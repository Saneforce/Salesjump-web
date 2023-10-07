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
using DBase_EReport;

public partial class Stockist_Payment_View : System.Web.UI.Page
{
    public static string Order_No;
    protected void Page_Load(object sender, EventArgs e)
    {
        Order_No = Request.QueryString["Order_No"].ToString();
    }
    [WebMethod(EnableSession = true)]
    public static string Get_Payment_details_view()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        //ds = sm.Get_Payment_view(Stockist_Code, Div_Code.TrimEnd(','), Order_No);
        ds = Get_Payment_view(Stockist_Code, Div_Code.TrimEnd(','), Order_No);
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }

    public static  DataSet Get_Payment_view(string Stockist_Code, string Div_Code, string Order_No)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec Sp_Get_Payment_view '" + Stockist_Code + "','" + Div_Code + "','" + Order_No + "'";

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