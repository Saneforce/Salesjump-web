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

public partial class MIS_Reports_rptView_CustomerWise_analysis : System.Web.UI.Page
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
    public static string result = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Div_code = HttpContext.Current.Session["div_code"].ToString();
        Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        Prdcat = Request.QueryString["PrdCat"];
        frmdt = Request.QueryString["Frm_Dt"];
        todt = Request.QueryString["To_Dt"];
        reporting_sf = Request.QueryString["RSf"];
        Prdcat = Prdcat == "null" ? "" : Prdcat;
    }
    [WebMethod(EnableSession = true)]
    public static string GetCustWise_Analysis()
    {
        DataSet dsSalesForce = null;
        result = "";
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = "GetCustWise_Analysis '" + Div_code + "','" + frmdt + "','" + todt + "','" + Prdcat + "','" + reporting_sf + "'";
        try
        {
            dsSalesForce = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        result = JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
        dsSalesForce.Dispose();
        return result;
    }
    [WebMethod(EnableSession = true)]
    public static string GetCat()
    {
        DataSet dsSalesForce = null;
        result = "";
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = "select * from Mas_Product_Category where Division_Code=" + Div_code + " and Product_Cat_Active_Flag=0";
        try
        {
            dsSalesForce = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        result = JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
        dsSalesForce.Dispose();
        return result;
    }
    [WebMethod(EnableSession = true)]
    public static string GetCustOvrlRate()
    {
        DataSet dsSalesForce = null;
        result = "";
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = "GetCustOvrlRate '" + Div_code + "','" + frmdt + "','" + todt + "','" + Prdcat + "','" + reporting_sf + "'";
        try
        {
            dsSalesForce = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        result = JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
        dsSalesForce.Dispose();
        return result;
    }
}