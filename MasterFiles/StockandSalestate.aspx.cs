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

public partial class MasterFiles_StockandSalestate : System.Web.UI.Page
{
    string divcode = string.Empty;
    public static string baseUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["div_code"]) != null || Convert.ToString(Session["div_code"]) != ""))
        { divcode = Session["div_code"].ToString(); }
        else { Page.Response.Redirect(baseUrl, true); }
    }
    [WebMethod]
    public static string loadstockist(string divcode)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = db.Exec_DataSet("Select  Stockist_Code,Stockist_Name,Stockist_Address,Territory,isnull(Taluk_Name,'') Dist_Name from Mas_Stockist Where Division_Code='" + divcode + "' and Stockist_Active_Flag=0  order by Stockist_Name");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string loaddetails(string dist, string date, string divcode)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = db.Exec_DataSet("exec product_Saleunits '" + dist + "','" + date + "','" + divcode + "' ");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

}