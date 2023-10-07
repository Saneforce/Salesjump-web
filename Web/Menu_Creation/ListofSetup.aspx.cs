using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Bus_EReport;
using System.Data;
using DBase_EReport;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Configuration;

public partial class MasterFiles_ListofSetup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    static string MasterCon = ConfigurationManager.ConnectionStrings["MasterDB"].ToString();
    SqlConnection con = new SqlConnection(MasterCon);
    static string qry = "";
    [WebMethod(EnableSession = true)]
    public static string SaveListofSetup(string Field_Name, string Setup_Name, string Setup_Desc, string Setup_Type, string field_Value, string SetUpValue, string Group_Setup, string FormType)
    {
        MasterFiles_ListofSetup Mls = new MasterFiles_ListofSetup();
        DataSet ds = null;
        ds = Mls.GetListOfSetUp(Field_Name, Setup_Name, Setup_Desc, Setup_Type, SetUpValue, field_Value, Group_Setup, FormType);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public DataSet Exec_DataSet(string strQry)
    {
        DataSet ds_EReport = new DataSet();
        try
        {


            SqlCommand selectCMD = new SqlCommand(strQry, con);
            selectCMD.CommandTimeout = 120;

            SqlDataAdapter da_EReport = new SqlDataAdapter();
            da_EReport.SelectCommand = selectCMD;

            con.Open();

            da_EReport.Fill(ds_EReport, "Customers");

            // _conn.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }
        return ds_EReport;
    }
    public int ExecQry(string sQry)
    {
        int iReturn = -1;

        try
        {
            //SqlConnection _conn = new SqlConnection(strConn);
            System.Data.SqlClient.SqlCommand cmd;
            cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sQry;
            con.Open();
            iReturn = cmd.ExecuteNonQuery();
            // _conn.Close();
        }
        catch (Exception ex)
        {
            //return 0;
            throw ex;
        }
        finally
        {
            con.Close();
        }
        return iReturn;
    }
    public DataSet GetListOfSetUp(string Field_Name, string Setup_Name, string Setup_Desc, string Setup_Type, string SetUpValue, string field_Value, string Group_Setup, string FormType)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null; string strQry = "";
        strQry = "Exec Data_ListOfSetUP '" + Field_Name + "','" + Setup_Name + "','" + Setup_Desc + "','" + Setup_Type + "','" + field_Value + "','" + SetUpValue + "','" + Group_Setup + "','" + FormType + "'";
        try
        {
            ds = Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    [WebMethod(EnableSession = true)]
    public static string Get_grp_data()
    {
        DataSet ds = null;
        MasterFiles_ListofSetup db_ElR = new MasterFiles_ListofSetup();
        qry = "SELECT * FROM Mas_Setting_Master";
        ds = db_ElR.Exec_DataSet(qry);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string InsertGroup(string Name)
    {
        int ds = 0; DataSet dt = new DataSet();
        int ID = 0;
        MasterFiles_ListofSetup db_ElR = new MasterFiles_ListofSetup();
        string Max_Cnt = "SELECT MAX(ID) ID FROM Mas_Setting_Master";
        dt = db_ElR.Exec_DataSet(Max_Cnt);
        ID = Convert.ToInt16(dt.Tables[0].Rows[0]["ID"]);
        ID += 1;
        qry = "INSERT INTO Mas_Setting_Master (ID,Setting_Field_Name) VALUES(" + ID + ",'" + Name + "')";
        ds = db_ElR.ExecQry(qry);
        if (ds > 0)
            return "Insert Successsfully";
        else
            return "Insert Failed";
    }
    [WebMethod]
    public static string UpdateListSetup(string Field_Name, string Setup_Name, string Setup_Desc, string Setup_Type, string field_Value, string Value, string Group_Setup, string FormType, string EditID)
    {
        int ds = 0;
        MasterFiles_ListofSetup db_ElR = new MasterFiles_ListofSetup();
        qry = "Update Master..Mas_Setting_Field Set Field_Name='" + Field_Name + "',Setup_Name='" + Setup_Name + "',Setup_Desc='" + Setup_Desc + "',Setup_Type='" + Setup_Type + "',Setup_Values='" + field_Value + "',Value='" + Value + "',Group_Setup='" + Group_Setup + "',FormType='" + FormType + "' Where SetUp_ID= '" + EditID + "'";
        ds = db_ElR.ExecQry(qry);
        if (ds > 0)
            return "Update Successfully";
        else
            return "Record Not Update";
    }
    [WebMethod(EnableSession = true)]
    public static string GetEmp_Setting(string FrmTyp)
    {
        DataSet ds = null;
        MasterFiles_ListofSetup db_ElR = new MasterFiles_ListofSetup();
        qry = "Select * from Mas_Setting_Field Where FormType='" + FrmTyp + "'";
        ds = db_ElR.Exec_DataSet(qry);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string EmpHeaderChK(string Chk, string ID)
    {
        int ds = 0;
        MasterFiles_ListofSetup db_ElR = new MasterFiles_ListofSetup();
        qry = "Update Mas_Setting_Field Set EmpChk=" + Chk + " Where SetUp_ID=" + ID + "";
        ds = db_ElR.ExecQry(qry);
        if (ds > 0)
            return "Successfully Check";
        else
            return "Record Not Check";
    }

    [WebMethod]
    public static string Update_Emp_Setup(string Field_Name, string Setup_Name, string Setup_Desc, string Setup_Type, string field_Value, string Value, string FormType, string EditID)
    {
        int ds = 0;
        MasterFiles_ListofSetup db_ElR = new MasterFiles_ListofSetup();
        qry = "Update Master..Mas_Setting_Field Set Field_Name='" + Field_Name + "',Setup_Name='" + Setup_Name + "',Setup_Desc='" + Setup_Desc + "',Setup_Type='" + Setup_Type + "',Setup_Values='" + field_Value + "',Value='" + Value + "',FormType='" + FormType + "' Where SetUp_ID= '" + EditID + "'";
        ds = db_ElR.ExecQry(qry);
        if (ds > 0)
            return "Update Successfully";
        else
            return "Record Not Update";
    }

    [WebMethod]
    public static string DeltListSetup(string ID)
    {
        int ds = 0;
        MasterFiles_ListofSetup db_ElR = new MasterFiles_ListofSetup();
        qry = "DELETE FROM Master..Mas_Setting_Field Where SetUp_ID= '" + ID + "'";
        ds = db_ElR.ExecQry(qry);
        if (ds > 0)
            return "Record Deleted Successfully";
        else
            return "Record Not Deleted";
    }
}