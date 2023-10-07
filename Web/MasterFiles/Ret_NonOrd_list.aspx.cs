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
    protected void Page_Load(object sender, EventArgs e)
    {
        Div_code = Session["div_code"].ToString();
        Sf_Code = Session["Sf_Code"].ToString();
    }
    [WebMethod]
    public static string getStates(string divcode)
    {
        Territory SFD = new Territory();
        DataSet ds = getRo_States(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getRetailerRoutes(string Hq)
    {
        string strQry = string.Empty;
        DataTable dt = null;
        DB_EReporting dB_ER = new DB_EReporting();
        strQry = "declare   @divcode varchar(20)='" + Div_code + "',@hq varchar(20) ='" + Hq + "' " +
            " Begin " +
            " Select mt.Territory_Code Territory_Code, mt.Territory_name Territory_Name, mt.SF_Code,ms.Sf_Name  " +
            " FROM Mas_Territory_Creation mt With(nolock) Inner Join Mas_Salesforce ms With(nolock) On CHARINDEX(',' + Cast(mt.SF_Code as VarcHar) + ',',',' + ms.Sf_Code + ',')> 0  " +
            " and CHARINDEX(',' + Cast(@divcode as VarcHar) + ',',',' + ms.Division_Code + ',')> 0  and ms.SF_Status <> 1 " +
            " Where ms.Hq_Code = @hq and CHARINDEX(',' + cast(@divcode as varchar) + ',',',' + ms.Division_Code + ',')> 0 " +
            " group by mt.Territory_Code,mt.Territory_Name,mt.SF_Code ,   ms.Sf_Name " +
            " order by 2  " +
            " End";
        dt = dB_ER.Exec_DataTable(strQry);
        return JsonConvert.SerializeObject(dt);
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
    public static string getSFHQ(string statecode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "select Hq_Code,Sf_HQ from Mas_Salesforce where State_Code=" + statecode + " and CHARINDEX(','+cast(" + Div_code + " as varchar)+',',','+Division_Code+',')>0 and sf_type=1 and sf_Code<>'admin' group by Hq_Code,Sf_HQ order by Sf_HQ";

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
    public static string getSF(string Hq)
    {
        Territory SFD = new Territory();
        DataSet ds = SFD.getSF_HQ(Div_code, Hq);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static int GetRetCnt(string Sfcode, string routecode)
    {
        string str = "select Count(ListedDrCode) ListedDrCode from Mas_ListedDr where ListedDr_Active_Flag=6 and Division_code =" + Div_code + " and CHARINDEX(','+iif('" + Sfcode + "'='',Sf_Code,'" + Sfcode + "')+',',','+Sf_Code+',')>0  and iif('" + routecode + "'='',Territory_Code,'" + routecode + "')=Territory_Code ";
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
    public static string GetRetNonOrdList(string Offset, int FetchNxt, string Sfcode, string routecode)
    {
        string str = "Exec get_non_ord_retList " + Div_code + "," + (Convert.ToDouble(Offset) * FetchNxt) + "," + FetchNxt + ",'" + Sfcode + "','" + routecode + "'";
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
        for (int i = 0; i < ds.Tables.Count; i++)
        {
            if (ds.Tables[i].Rows.Count > 0)
                Result = JsonConvert.SerializeObject(ds.Tables[i]);
			else
				Result = JsonConvert.SerializeObject(ds.Tables[i]);
        }
        ds.Dispose();
        return Result;
    }
    [WebMethod]
    public static string ChngRetsts(int RetCode, int Status)
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