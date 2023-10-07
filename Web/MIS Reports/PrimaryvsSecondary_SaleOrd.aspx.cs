using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Services;

public partial class MIS_Reports_PrimaryvsSecondary_SaleOrd : System.Web.UI.Page
{
    static string div_code = string.Empty;
    static string SF = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        SF = Session["Sf_Code"].ToString();
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
    [WebMethod]
    public static string MGR()
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsSF = null;
        string strQry = "EXEC getHyrSFList_MGR '" + div_code + "', '" + SF + "'";
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
    public static string Subdivision_list()
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsDivision = null;

        string strQry = "SELECT subdivision_code,subdivision_name,subdivision_sname FROM mas_subdivision where Div_Code='" + div_code + "' and SubDivision_Active_Flag=0";
        try
        {
            dsDivision = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dsDivision.Tables[0]);
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
    public static string product()
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "select * from Mas_Product_Detail where Division_Code='" + div_code + "' and Product_Active_Flag=0 order by Product_Detail_Name";

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

        string strQry = " select * from Mas_Product_Group where Division_Code=" + div_code + " and Product_Grp_Active_Flag = 0 order by Product_Grp_Name";

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
    public static string productCat()
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "select * from Mas_Product_Category where Division_Code=" + div_code + " and Product_Cat_Active_Flag = 0 order by Product_Cat_Name";

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