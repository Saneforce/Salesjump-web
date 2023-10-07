using Bus_EReport;
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

public partial class MasterFiles_POBMaterial_List : System.Web.UI.Page
{
    #region "Declaration"
    DataTable dsProd1 = null;
    DataSet dsProd = null;
    DataSet dsProduct = null;
    DataSet dsDivision = null;
    DataSet dsState = null;
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string div_code = string.Empty;
    string ProdCode = string.Empty;
    string ProdDescr = string.Empty;
    string ProdName = string.Empty;
    string ProdSaleUnit = string.Empty;
    string sCmd = string.Empty;
    string Char = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    public static string sUSR = string.Empty;
    #endregion  
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            div_code = Session["div_code"].ToString();
        }
        catch
        {
            div_code = Session["Division_Code"].ToString();
        }
    }

    [WebMethod]
    public static string GetDetails(string divcode)
    {
        DCR dv = new DCR();
        DataTable ds = new DataTable();
        ds = dv.getDataTable("exec getmatdetailMaster '" + divcode + "'");
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod]
    public static string getdivision(string divcode)
    {

        DataSet dds = getAlldivision(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    public static DataSet getAlldivision(string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsState = null;

        string strQry = "Select subdivision_code,subdivision_name from mas_subdivision where Div_Code='" + divcode + "' and SubDivision_Active_Flag=0 ";

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
    [WebMethod]
    public static string GetUOM(string divcode)
    {
        DataSet dds = getUOMBox(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    public static DataSet getUOMBox(string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsState = null;

        string strQry = "SELECT 0 as Move_MailFolder_Id,'---Select---' as Move_MailFolder_Name " +
                 " UNION " +
                 " SELECT Move_MailFolder_Id,Move_MailFolder_Name " +
                 " FROM Mas_Multi_Unit_Entry " +
                 " WHERE Division_Code in  (" + divcode + ") and Folder_Act_flag=0 ";

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
    [WebMethod]
    public static string getdets(string divcode, string mcode)
    {
        DataSet dds = getalldets(divcode, mcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    public static DataSet getalldets(string divcode, string mcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsState = null;

        string strQry = "Select POP_Code,POP_Name, Sub_Division_Code, POP_UOM,Move_MailFolder_Id from POP_Material_master mm " +
                        "left join Mas_Multi_Unit_Entry ms on ms.Move_MailFolder_Name = mm.POP_UOM and ms.Division_Code = mm.Division_Code " +
                        "where mm.Division_Code = '" + divcode + "' and POP_Code = '" + mcode + "'";

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
    [WebMethod]
    public static string savedets(string divcode, string name, string divi, string uomv, string pcode)
    {
        DCR dv = new DCR();
        DataTable ds = new DataTable();
        ds = dv.getDataTable("exec  savematdetailMaster '" + divcode + "','" + name + "','" + divi + "','" + uomv + "','" + pcode + "'");
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod]
    public static string deactivateprodtl(string arcode, string stat,string divcode)
    {
        Territory Terr = new Territory();
        int iReturn = DeActivate(arcode, stat, divcode);
        if (iReturn > 0)
        {
            return "Success";
        }
        else
        {
            return "Failure";
        }
    }

    public  static int DeActivate(string arcode, string stat,string divcode)
    {
        
        int iReturn = -1;
        try
        {
            DB_EReporting db = new DB_EReporting();

            string strQry = "Update POP_Material_master set Active_flag= " + stat + " where Division_code='" + divcode + "' and POP_Code='" + arcode + "'";
            iReturn = db.ExecQry(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return iReturn;
    }
}