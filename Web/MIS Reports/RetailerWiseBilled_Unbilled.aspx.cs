using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using Bus_EReport;
using Newtonsoft.Json;
using DBase_EReport;

public partial class MIS_Reports_RetailerWiseBilled_Unbilled : System.Web.UI.Page
{
    #region "Declaration"
    public static string div_code = string.Empty;
    public static string sf_code = string.Empty;
    public static string FDate = string.Empty;
    public static string Tdate = string.Empty;
    public static string Type = string.Empty;
    public static DataSet ds = null;
    public static string sub_division = string.Empty;
    public static string baseUrl = "";
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["div_code"]) != null || Convert.ToString(Session["div_code"]) != ""))
        {
            div_code = Session["div_code"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FDate = Request.QueryString["FDate"].ToString();
            Tdate = Request.QueryString["Tdate"].ToString();
            //Type = Request.QueryString["Type"].ToString();
        }
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion

    #region GetRetailersBilled_Unbilled
    [WebMethod]
    public static string GetRetailersBilled_Unbilled()
    {
        ds = new DataSet();
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = "Exec getUnbilledRetailer '" + div_code + "','" + sf_code + "','" + FDate + "','" + Tdate + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    #endregion
}