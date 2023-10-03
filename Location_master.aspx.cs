using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Globalization;
using Newtonsoft.Json;
using System.Web.Services;
using DBase_EReport;

public partial class Location_master : System.Web.UI.Page
{
    #region Declaration
    static DataSet dsSalesForce = null;
    string SFCode = string.Empty;
    static string div_code = string.Empty;
    string sf_type = string.Empty;
    string sSFstr = string.Empty;
    public string selsfdet = string.Empty;
    public string seldtdet = string.Empty;
    public static string baseUrl = "";
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["div_code"]) != null || Convert.ToString(Session["div_code"]) != ""))
        {
            div_code = Session["div_code"].ToString();
            SFCode = Session["SF_Code"].ToString();
            selsfdet = Request.QueryString["SFCode"];
            seldtdet = Request.QueryString["FDate"];
            if (!Page.IsPostBack)
            {

            }
        }
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion

    #region GetSFTaggedRetailers
    [WebMethod]
    public static string GetSFTaggedRetailers(string SFCode, string div_code, string routcode)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = db.Exec_DataSet("exec AllRetailers_wise_Mapping '" + SFCode + "','" + div_code + "','" + routcode + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    #endregion

    #region GetSFTaggeddistr
    [WebMethod]
    public static string GetSFTaggeddistr(string SFCode, string div_code, string routcode)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = db.Exec_DataSet("exec AllDistribut_wise_Mapping '" + SFCode + "','" + div_code + "','" + routcode + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    #endregion

    #region loadallSf
    [WebMethod]
    public static string loadallSf(string SFcode, string div_code)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = db.Exec_DataSet("Select Sf_code,Sf_name+' - '+sf_Designation_Short_Name+' - '+Sf_HQ sf_name  from Mas_Salesforce where charindex(','+'" + div_code + "'+',',','+Division_Code+',')>0 and Reporting_To_SF='" + SFcode + "' and sf_TP_Active_Flag = 0 and Sf_Code like'%mr%' ");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    #endregion

    #region loadallmgr
    [WebMethod]
    public static string loadallmgr(string div_code)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = loadallmgrlist(div_code);
        return JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
    }
    #endregion

    #region loadallmgrlist
    public static DataSet loadallmgrlist(string div_code)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsState = null;

        string strQry = " Select '0' as Sf_code,'  Select Employee' sf_name " +
                        "union Select Sf_code,Sf_name+' - '+sf_Designation_Short_Name+' - '+Sf_HQ sf_name  from Mas_Salesforce where charindex(','+'" + div_code + "'+',',','+Division_Code+',')>0 and  sf_TP_Active_Flag=0  order by Sf_name";

        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    #endregion

    #region GetSFTaggOLd
    //[WebMethod]
    //public static string GetSFTagg(string SFCode, int Start,int limit, string div_code,string routsf)
    //{
    //    DB_EReporting db = new DB_EReporting();
    //    DataSet ds = db.Exec_DataSet("exec AllRetailers_map_card '"+ SFCode + "','" + div_code + "'," + Start + ","+ limit + ",'"+ routsf + "'");
    //    return JsonConvert.SerializeObject(ds.Tables[0]);
    //}
    #endregion

    #region SFAttentances
    [WebMethod]
    public static string SFAttentances(string SFCode, string Date, string div_code)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = db.Exec_DataSet("exec Allsf_map_attendance '" + SFCode + "','" + div_code + "','" + Date + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    #endregion

    #region SFordergetdets
    [WebMethod]
    public static string SFordergetdets(string SFCode, string Date, string div_code)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = db.Exec_DataSet("exec SFvisit_orderget_dets '" + SFCode + "','" + Date + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    #endregion

    #region rmallrSFTags
    [WebMethod]
    public static string rmallrSFTags(string SFCode, string roueco)
    {
        DB_EReporting db = new DB_EReporting();
        if (roueco != "0")
        {
            DataSet ds = db.Exec_DataSet("update c set StatFlag=1 from map_GEO_Customers c inner join (select ListedDrCode from Mas_ListedDr dr with(nolock) inner join Mas_Territory_Creation tc on tc.Territory_Code=dr.Territory_Code" +
                                    " where CHARINDEX(',' + '" + SFCode + "' + ',', ',' + dr.Sf_Code + ',') > 0 and dr.ListedDr_Active_Flag = 0 and dr.Territory_Code = '" + roueco + "' and tc.Division_Code='" + div_code + "')" +
                                    "d on Cust_Code = ListedDrCode ");
            return "{'success':'true'}";
        }
        else
        {
            DataSet ds = db.Exec_DataSet("update c set StatFlag=1 from map_GEO_Customers c inner join (select ListedDrCode from Mas_ListedDr dr with(nolock) inner join Mas_Territory_Creation tc on tc.Territory_Code=dr.Territory_Code" +
                                    " where CHARINDEX(',' + '" + SFCode + "' + ',', ',' + dr.Sf_Code + ',') > 0 and dr.ListedDr_Active_Flag = 0  and tc.Division_Code='" + div_code + "')" +
                                    "d on Cust_Code = ListedDrCode ");
            return "{'success':'true'}";
        }

    }
    #endregion

    #region untagRetailers
    [WebMethod(EnableSession = true)]
    public static string untagRetailers(string custCode)
    {
        string err = "";
        int iReturn = -1;
        ListedDR ldr = new ListedDR();
        try
        {

            string div_code = HttpContext.Current.Session["div_code"].ToString();
            iReturn = ldr.untagRetailers_confirm(custCode);

            if (iReturn > 0)
            {
                err = "Sucess";
            }
            else
            {
                err = "Error";
            }
        }
        catch (Exception ex)
        {
            err = "Error";
        }
        return err;
    }
    #endregion

    #region gettravelkms
    [WebMethod]
    public static string gettravelkms(string SFCode, string Date, string div_code)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = db.Exec_DataSet("Select Sf_Code,Pln_Date,Round((UDistance/1000),2) as UDistance from Distancetbl  where Sf_code = '" + SFCode + "'and Convert(date, Pln_Date) = '" + Date + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    #endregion

    #region allocateroute
    [WebMethod]
    public static string allocateroute(string SFcode, string div_code)
    {
        DB_EReporting db = new DB_EReporting();
        //DataSet ds = db.Exec_DataSet("Select Territory_Code,Territory_Name,Division_Code from Mas_Territory_Creation where charindex(','+cast('" + SFcode + "'  as varchar)+',',','+SF_Code+',')>0 and Division_Code='" + div_code + "'");
        DataSet ds = db.Exec_DataSet("exec AllRoutessfwise '" + SFcode + "','" + div_code + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    #endregion

}