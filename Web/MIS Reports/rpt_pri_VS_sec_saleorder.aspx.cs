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
    public static string stateNm = "";    
    public static string reporting_sfNm = "";
    public static string sfNm = "";    
    public static string Divnm = "";
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
        stateNm = Request.QueryString["stateNm"];
        reporting_sfNm = Request.QueryString["RSfNm"].ToString();
        sfNm = Request.QueryString["sfNm"].ToString();
        Divnm = Request.QueryString["Divnm"].ToString();
        Div_code = HttpContext.Current.Session["div_code"].ToString();
        Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        reporting_sf = reporting_sf == "null" ? "" : reporting_sf;
        reporting_sfNm = reporting_sfNm == "null" ? "" : reporting_sfNm;
        Divnm = Divnm == "null" ? "" : Divnm;
        sfNm = sfNm == "null" ? "" : sfNm;
        sfcode = sfcode == "null" ? "" : sfcode;
        state = state == "null" ? "" : state;
        stateNm = stateNm == "null" ? "" : stateNm;
    }
    [WebMethod(EnableSession = true)]
    public static string Sec_Saleord()
    {        
        DataSet dsSalesForce = null;
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = "GetSec_SaleOrd_MonthWise '" + Div_code + "','" + Sf_Code + "','" + frmdt + "','" + todt + "','" + reporting_sf + "','" + state + "','" + sfcode + "'";
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
        string strQry = "GetPri_SaleOrd_MonthWise '" + Div_code + "','" + Sf_Code + "','" + frmdt + "','" + todt + "','" + reporting_sf + "','" + state + "','" + sfcode + "'";
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

        string strQry = "select * from Mas_Product_Category where Division_Code=" + Div_code + " and Product_Cat_Active_Flag = 0 and CHARINDEX(','+CAST(Product_Cat_Code AS varchar)+',',','+'" + Prdcat + "'+',')>0 order by Product_Cat_Name";

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
    [WebMethod]
    public static string AllproductCat()
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "select * from Mas_Product_Category where Division_Code=" + Div_code + " and Product_Cat_Active_Flag = 0 ";//and CHARINDEX(','+CAST(Product_Cat_Code AS varchar)+',',','+'" + Prdcat + "'+',')>0 order by Product_Cat_Name";

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
    [WebMethod]
    public static string productGrp()
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "select * from Mas_Product_Group where Division_Code=" + Div_code + " and Product_Grp_Active_Flag = 0 and CHARINDEX(','+CAST(Product_Grp_Code AS varchar)+',',','+'" + PrdGrp + "'+',')>0";

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
    [WebMethod]
    public static string productName()
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "select * from Mas_Product_Detail where Division_Code=" + Div_code + " and Product_Active_Flag = 0 and CHARINDEX(','+CAST(Product_Detail_Code AS varchar)+',',','+'" + PrdNm + "'+',')>0";

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