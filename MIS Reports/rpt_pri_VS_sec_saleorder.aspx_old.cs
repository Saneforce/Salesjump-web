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

public partial class MIS_Reports_rpt_pri_VS_sec_saleorder : System.Web.UI.Page
{
    public static string sfcode = "";
    public static string reporting_sf = "";
    public static string frmdt = "";
    public static string todt = "";
    public static string PrdNm = "";
    public static string PrdGrp = "";
    public static string Prdcat = "";
    public static string state = "";
    public static string Sf_Code = "";
    public static string Div_code = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        reporting_sf = Request.QueryString["RSf"];
        sfcode = Request.QueryString["sfcode"];
        PrdNm = Request.QueryString["PrdNm"];
        PrdGrp = Request.QueryString["PrdGrp"];
        Prdcat = Request.QueryString["PrdCat"];
        frmdt = Request.QueryString["Frm_Dt"];
        todt = Request.QueryString["To_Dt"];
        state = Request.QueryString["state"];
        Div_code = HttpContext.Current.Session["div_code"].ToString();
        Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        reporting_sf = reporting_sf == "null" ? "" : reporting_sf;
        sfcode = sfcode == "null" ? "" : sfcode;
        state = state == "null" ? "" : state;
    }
    [WebMethod(EnableSession = true)]
    public static string Sec_Saleord()
    {        
        DataSet dsSalesForce = null;
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = "GetSec_SaleOrd_MonthWise '" + Div_code + "','" + Sf_Code + "','" + frmdt + "','" + todt + "','" + reporting_sf + "'," + state + ",'" + sfcode + "'";
        try
        {
            dsSalesForce = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Pri_Saleord()
    {
        
        DataSet dsSalesForce = null;
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = "GetPri_SaleOrd_MonthWise '" + Div_code + "','" + Sf_Code + "','" + frmdt + "','" + todt + "','" + reporting_sf + "'," + state + ",'" + sfcode + "'";
        try
        {
            dsSalesForce = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
    }
    [WebMethod]
    public static string productCat()
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "select * from Mas_Product_Category where Division_Code=" + Div_code + " and Product_Cat_Active_Flag = 0 order by Product_Cat_Name";

        try
        {
            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dsAdmin.Tables[0]);
    }
}