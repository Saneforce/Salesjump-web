using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Services;
using Bus_EReport;
using System.Data;
using DBase_EReport;



public partial class MasterFiles_OutletsMerging : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsStockist = null;
    DataSet dsdistributor = null;
    DataSet dsTerritory = null;
    DataSet dsTerritory1 = null;
    DataSet dsTerritory2 = null;

    public static string sf_code = string.Empty;
    public static string sf_type = string.Empty;    
    public static string div_code = string.Empty;
    int iIndex = -1;
    int iReturn = -1;
    public static string baseUrl = "";
    #endregion

    #region OnPreInit
    protected override void OnPreInit(EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["sf_type"]) != null || Convert.ToString(Session["sf_type"]) != ""))
        {
            base.OnPreInit(e);
            sf_type = Session["sf_type"].ToString();
            if (sf_type == "3")
            {
                this.MasterPageFile = "~/Master.master";
            }
            else if (sf_type == "2")
            {
                this.MasterPageFile = "~/Master_MGR.master";
            }
            else if (sf_type == "1")
            {
                this.MasterPageFile = "~/Master_MR.master";
            }
        }
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["sf_code"]) != null || Convert.ToString(Session["sf_code"]) != ""))
        {
            sf_code = Session["sf_code"].ToString();
            div_code = Session["div_code"].ToString();


            if (!Page.IsPostBack)
            {
                sf_code = Session["sf_code"].ToString();
                div_code = Session["div_code"].ToString();
            }
            if (Session["sf_type"].ToString() == "1")
            {
                sf_code = Session["sf_code"].ToString();
            }
            else
            {
                sf_code = Session["sf_code"].ToString();
            }
        }
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion


    #region GetSalesForceDetails
    [WebMethod]
    public static string GetSalesForceDetails(string divcode)
    {
        ListedDR dv = new ListedDR();
        DataSet dsSl = dv.GetSalesForceDetails(div_code);

        DataTable dt = new DataTable();
        if (dsSl.Tables.Count > 0)
        {
            dt = dsSl.Tables[0];
        }
        return JsonConvert.SerializeObject(dt);
    }
    #endregion

    #region GetRouteDetails
    [WebMethod]
    public static string GetRouteDetails(string SfCode, string divcode)
    {
        ListedDR dv = new ListedDR();
        DataSet dsSl = dv.GetRouteDetails(SfCode, div_code);

        DataTable dt = new DataTable();
        if (dsSl.Tables.Count > 0)
        {
            dt = dsSl.Tables[0];
        }
        return JsonConvert.SerializeObject(dt);
    }
    #endregion

    #region GetRetailerList

    [WebMethod]
    public static string GetRetailerList(string Territory_Code, string divcode)
    {
        if (Territory_Code == null || Territory_Code == "")
        {
            Territory_Code = "0";
        }

        if (divcode == null || divcode == "")
        {
            divcode = div_code;
        }

        ListedDR sk = new ListedDR();
        //DataSet dsStockist = sk.GetRetailerList(Territory_Code, divcode);
        DataSet dsStockist = GetRetailerLists(Territory_Code, divcode);

        return JsonConvert.SerializeObject(dsStockist.Tables[0]);
    }
    #endregion

    #region  GetRetailerLists
    public static DataSet GetRetailerLists(string Territory_Code, string DivCode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsListedDR = new DataSet();

        string strQry = string.Empty;

        //if (SfCode == "0")
        //{
        //    //strQry = " SELECT ListedDrCode,ListedDr_Name FROM Mas_ListedDr  ";
        //    //strQry += " WHERE ListedDr_Active_Flag = 0 and Division_Code = '" + DivCode + "'  ";
        //    ////strQry += " AND  ( '" + SfCode + "' = '0' OR  CHARINDEX(','+cast('" + SfCode + "'  as varchar)+',',','+Sf_Code+',')>0 ) ";
        //    //strQry += " GROUP BY ListedDrCode,ListedDr_Name  ORDER BY ListedDr_Name ASC  ";

        //    strQry = " SELECT ListedDrCode,ListedDr_Name FROM Mas_ListedDr  ";
        //    strQry += " WHERE ListedDr_Active_Flag = 0 and Division_Code = '" + DivCode + "'  ";
        //    strQry += " AND Territory_Code = " + Territory_Code + "  ";
        //    strQry += " GROUP BY ListedDrCode,ListedDr_Name  ORDER BY ListedDr_Name ASC  ";
        //}
        //else
        //{
        //    strQry = " SELECT ListedDrCode,ListedDr_Name FROM Mas_ListedDr  ";
        //    strQry += " WHERE ListedDr_Active_Flag = 0 and Division_Code = '" + DivCode + "'  ";
        //    strQry += " AND   (CHARINDEX(','+cast('" + SfCode + "'  as varchar)+',',','+Sf_Code+',')>0 ) ";
        //    strQry += " GROUP BY ListedDrCode,ListedDr_Name  ORDER BY ListedDr_Name ASC  ";

        //}

        strQry = " SELECT ListedDrCode,ListedDr_Name FROM Mas_ListedDr  ";
        strQry += " WHERE ListedDr_Active_Flag IN(0,1) and Division_Code = '" + DivCode + "'  ";
        strQry += " AND Territory_Code = " + Territory_Code + "  ";
        strQry += " GROUP BY ListedDrCode,ListedDr_Name  ORDER BY ListedDr_Name ASC  ";

        try
        {
            dsListedDR = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsListedDR;
    }
    #endregion

    #region GetRetailerDetails
    [WebMethod]
    public static string GetRetailerDetails(string DivCode, string ListedDrCode)
    {
        if (ListedDrCode == null || ListedDrCode == "")
        {
            ListedDrCode = "0";
        }

        if (DivCode == null || DivCode == "")
        {
            DivCode = div_code;
        }

        ListedDR sk = new ListedDR();
        //DataSet dsStockist = sk.GetRetailerDetails(DivCode, ListedDrCode);

        DataSet dsStockist = GetRetailerDetail(DivCode, ListedDrCode);

        return JsonConvert.SerializeObject(dsStockist.Tables[0]);
    }
    #endregion

    #region GetRetailerDetail
    public static DataSet GetRetailerDetail(string DivCode, string ListedDrCode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsListedDR = new DataSet();

        string strQry = string.Empty;

        strQry = " SELECT ListedDrCode,ListedDr_Name,ListedDr_Address1, ";
        strQry += " Convert(varchar(10),Cast(ListedDr_Created_Date as date),103) ListedDr_Created_Date,";
        strQry += " IsNull(ListedDr_Mobile,'') ListedDr_Mobile , ";
        strQry += " (CASE WHEN ListedDr_Active_Flag =0 THEN 'Active' ELSE 'De-Active' END) AS  ListedDr_Active_Flag, ";
        strQry += " (COUNT(CASE WHEN ISNULL(Oh.Order_Value,0)=0 THEN 0 ELSE 1 END)) OrderCount ";
        strQry += " FROM Mas_ListedDr ld  WITH(NOLOCK)  ";
        strQry += " LEFT OUTER JOIN Trans_Order_Head OH WITH(NOLOCK) ON ld.ListedDrCode = OH.Cust_Code   ";
        strQry += " WHERE ListedDr_Active_Flag IN (1,0) and Division_Code = '" + DivCode + "'  ";
        strQry += " AND ListedDrCode = " + ListedDrCode + "  ";
        strQry += " GROUP BY ListedDrCode,ListedDr_Name,ListedDr_Address1,Convert(varchar(10), Cast(ListedDr_Created_Date as date), 103),";
        strQry += "  IsNull(ListedDr_Mobile, ''),(CASE WHEN ListedDr_Active_Flag = 0 THEN 'Active' ELSE 'De-Active' END) ";

        try
        {
            dsListedDR = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsListedDR;
    }
    #endregion

    #region saveOutletMerging

    [WebMethod]
    public static string saveOutletMerging(string divcode, string SfCode,string SfRoute, string fromretailer, string toretailers)
    {
        string msg = "";
        if (SfCode == null || SfCode == "0")
        {
            SfCode = "0";
        }

        ListedDR sk = new ListedDR();
        msg = sk.Outletsmerging(divcode, SfCode, SfRoute, fromretailer, toretailers);

        return msg;
    }
    #endregion
}