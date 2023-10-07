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

public partial class MIS_Reports_Expense_Rpt_15CallCnt : System.Web.UI.Page
{
    private static string div_code="";

    public static string SF="";

    protected void Page_Load(object sender, EventArgs e)
    {
        SF = Session["Sf_Code"].ToString();
        div_code = Session["div_code"].ToString();
    }
    [WebMethod]
    public static string GetCallCntRpt(string Frmdt, string Todt)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsSF = null;
        string strQry = "EXEC getCallCntDet '" + Frmdt + "','" + Todt + "','" + div_code + "'";

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
    public static string state()
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "select st.State_Code,StateName from mas_state st inner join Mas_Division md on charindex(','+cast(st.State_Code as varchar)+',',','+md.State_Code+',')>0 where Division_Code=" + div_code + " order by StateName";

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
    public static string SalesForceList()
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsSF = null;
        string strQry = "EXEC getHyrSFList '" + SF + "','" + div_code + "','0','1',0";

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
}