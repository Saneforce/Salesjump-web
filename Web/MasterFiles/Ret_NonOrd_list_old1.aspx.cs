using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Data;
using DBase_EReport;
using System.Web.Services;
using Bus_EReport;

public partial class MasterFiles_Ret_NonOrd_list : System.Web.UI.Page
{
    public static string Sf_Code = "";
    public static string Div_code = "";
    private static DataSet dsHry;

    protected void Page_Load(object sender, EventArgs e)
    {
        Div_code = Session["Div_code"].ToString();
        Sf_Code = Session["Sf_Code"].ToString();
    }
    [WebMethod]
    public static string Gethyr()
    {

        DB_EReporting db_ER = new DB_EReporting();
        string Qry = "";
        Qry = "Exec HyrSFList_All '" + Div_code + "','0','" + Sf_Code + "'";
        try
        {
            dsHry = db_ER.Exec_DataSet(Qry);
        }
        catch
        {

        }
        return JsonConvert.SerializeObject(dsHry.Tables[0]);
    }
    [WebMethod]
    public static string getStates(string divcode)
    {
        Territory SFD = new Territory();
        DataSet ds = getRo_States(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getRo_States(string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "select st.State_Code,StateName from mas_state st inner join Mas_Division md on charindex(','+cast(st.State_Code as varchar)+',',','+md.State_Code+',')>0 where Division_Code=" + divcode + " order by StateName";

        try
        {
            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }
    [WebMethod]
    public static string GetDist()
    {
        DataSet ds = null;
        string qry = "select State_Code,* from Mas_Stockist where  Division_Code='" + Div_code + "' and Stockist_Active_Flag=0";
        DB_EReporting db_ER = new DB_EReporting();
        try
        {
            ds = db_ER.Exec_DataSet(qry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetTerrCreation()
    {
        DataSet ds = null;
        string qry = "select Territory_Name,Territory_Code from Mas_Territory where Div_Code='" + Div_code + "'";
        DB_EReporting db_ER = new DB_EReporting();
        try
        {
            ds = db_ER.Exec_DataSet(qry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static int GetRetCnt()
    {
        string str = "select Count(ListedDrCode) ListedDrCode from Mas_ListedDr where ListedDr_Active_Flag=6 and Division_code =" + Div_code + "";
        int Result = 0;
        DataSet ds = new DataSet();
        DB_EReporting dB_ER = new DB_EReporting();
        try
        {
            ds = dB_ER.Exec_DataSet(str);
        }
        catch (Exception ex)
        {
            ds.Dispose();
            throw ex;
        }
        Result = Convert.ToInt32(ds.Tables[0].Rows[0]["ListedDrCode"]);
        ds.Dispose();
        return Result;
    }
    [WebMethod]
    public static string GetRetNonOrdList(string statecode,string Distcode,string Sfcode,string routecode)
    {
        string str = "Exec get_non_ordretList " + Div_code + ",'" + statecode + "','" + Distcode + "','" + routecode + "','" + Sfcode + "'";
        string Result = "";
        DataSet ds = new DataSet();
        DB_EReporting dB_ER = new DB_EReporting();
        try
        {
            ds = dB_ER.Exec_DataSet(str);
        }
        catch (Exception ex)
        {
            ds.Dispose();
            throw ex;
        }
        Result = JsonConvert.SerializeObject(ds.Tables[0]);
        ds.Dispose();
        return Result;
    }
    [WebMethod]
    public static string ChngRetsts(int RetCode,int Status)
    {
        string str = "Update Mas_ListedDr set ListedDr_Active_Flag=" + Status + " where Division_Code=" + Div_code + " and ListedDrCode=" + RetCode + "";
        int Result = 0;
        string SResult = "";        
        DB_EReporting dB_ER = new DB_EReporting();
        try
        {
            Result = dB_ER.ExecQry(str);
        }
        catch (Exception ex)
        {           
            throw ex;
        }
        if (Result > 0)
        {
            SResult = "Update successfully!!!";
        }
        else
        {
            SResult = "Error!!!";
        }
        return SResult;
    }
}