using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data;
using DBase_EReport;


public partial class MasterFiles_Group_Setting : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDiv = null;
    DataSet accessMas = null;
    int subdivcode = 0;
    int subdivision_code = 0;
    static string divcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    string state_name = string.Empty;
    string Area_name = string.Empty;
    string Area_code = string.Empty;
    DataSet dsState = null;
    DataSet dsDivision = null;
    string state_cd = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string sState = string.Empty;
    string[] statecd;
    int time;
    static string str = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
    }
    [WebMethod]
    public static string GetData_colrow(string Unpivot)
    {
        string[] q = Unpivot.Split(',');
        string castvar = "";
        for (int i = 0; i < q.Length; i++)
        {
            castvar += "ISNULL(CAST(" + q[i] + " AS VARCHAR(50)),'') AS " + q[i] + ",";
        }
        castvar = castvar.Remove(castvar.Length - 1, 1);
        DataSet ds = null;
        DB_EReporting db_ElR = new DB_EReporting();
        str = "Select u.Field_Name,iif(isnull(u.Value,'')<>'',u.value,M.Value) Value from(select " + castvar + " from Access_Master where Division_Code = '" + divcode + "') tt" +
            " unpivot(Value for Field_Name in (" + Unpivot + ")) u right join Master..Mas_setting_Field M on M.Field_Name = u.Field_Name Where FormType='Company'";
        ds = db_ElR.Exec_DataSet(str);
        
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetTabMenu()
    {
        DataSet ds = null;
        MasterFiles_Group_Setting mgs = new MasterFiles_Group_Setting();
        ds = mgs.GettabMenu();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public DataSet GettabMenu()
    {
        string strQry = "";
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();
        strQry = "select * from Master..Mas_Setting_Master";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    [WebMethod]
    public static string GetMenuValue()
    {
        DataSet ds = null; DB_EReporting dB_ERpt = new DB_EReporting();
        string strQry = "Select * from Master..Mas_Setting_Field Where FormType='Company'";
        try
        {
            ds = dB_ERpt.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string Submit_SettingDetails(string data)
    {
        MasterFiles_Group_Setting mgs = new MasterFiles_Group_Setting();
        return mgs.SubmitValues(data);
    }
    public string SubmitValues(string data)
    {
        DataTable str = JsonConvert.DeserializeObject<DataTable>(data);
        string strQry = ""; int iReturn = -1;
        DB_EReporting db = new DB_EReporting();
        divcode = Convert.ToString(Session["div_code"]);
        try
        {
            bool value = sRecordExistaccess_master(divcode);
            if (value == false)
            {
                for (int i = 0; i < str.Rows.Count; i++)
                {
                    strQry = "INSERT INTO Access_Master('" + str.Rows[i]["Field_Name"] + "') values ('" + str.Rows[i]["Value"] + "')";
                    iReturn = db.ExecQry(strQry);
                }
            }
            else
            {
                for (int i = 0; i < str.Rows.Count; i++)
                {
                    strQry = "UPDATE Access_Master SET " + str.Rows[i]["Field_Name"] + "='" + str.Rows[i]["Value"] + "' WHERE Division_Code='" + divcode + "'";
                    iReturn = db.ExecQry(strQry);
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        if (iReturn > 0)
        {
            return "Sucess";
        }
        else
        {
            return "Record Not Insert";
        }
    }
    public bool sRecordExistaccess_master(string Division_Code)
    {
        string strQry = "";
        bool bRecordExist = false;
        try
        {
            DB_EReporting db = new DB_EReporting();

            strQry = "Select count(*) from Access_Master where Division_Code = '" + Division_Code + "'";
            int iRecordExist = db.Exec_Scalar(strQry);

            if (iRecordExist > 0)
                bRecordExist = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bRecordExist;
    }
}