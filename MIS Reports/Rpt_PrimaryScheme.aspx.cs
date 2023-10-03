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
using System.Configuration;
using Newtonsoft.Json;
using DBase_EReport;

public partial class MIS_Reports_Rpt_PrimaryScheme : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string sfcode = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    public static string sf_type = string.Empty;
    public static string sf_code = string.Empty;
    public static DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        sf_code = HttpContext.Current.Session["Sf_Code"].ToString();
        divcode = HttpContext.Current.Session["div_Code"].ToString();
    }
    [WebMethod]
    public static string GetSF(string Div)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsSF = null;
        string strQry = "EXEC getHyrSFList_MGR_MR '" + Div + "','" + sf_code + "'";//'" + Sub_Div_Code + "','" + Alpha + "'," + stcode + "";

        try
        {
            dsSF = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dsSF.Tables[0]);
    }
    [WebMethod]
    public static string GetSprStk_Schm(string Fdt, string Tdt)
    {
        DB_EReporting db_ER = new DB_EReporting();
        ds = null;
        string strQry = "EXEC getPriOrderStk_Scheme '" + sf_code + "','" + divcode + "','" + Fdt + "','" + Tdt + "'";

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
}