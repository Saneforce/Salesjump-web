using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Bus_EReport;
using System.Web.Services;
using System.Data;
using DBase_EReport;

public partial class Menu_Creation_Employee_Settings_Dynamic : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDivision = null;
    DataSet dsEmployee = null;
    DataSet dsAccessmas = null;
    static string div_code = string.Empty;
    string div_name = string.Empty;
    string div_addr1 = string.Empty;
    string div_addr2 = string.Empty;
    string div_city = string.Empty;
    string div_pin = string.Empty;
    string div_state = string.Empty;
    string div_sname = string.Empty;
    string div_alias = string.Empty;
    string state_code = string.Empty;
    string sChkLocation = string.Empty;
    string txtNewSlNo = string.Empty;
    string sf_type = string.Empty;
    string sf_code = string.Empty;
    string dsdoc = string.Empty;
    string str = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iLength = -1;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();

        sf_code = Session["sf_code"].ToString();
       
        div_code = Session["div_code"].ToString();       
       
    }

    public class empdata
    {
        public string Reporting_To_SF { get; set; }
        public string sfcode { get; set; }
        public string Sf_name { get; set; }
        public Int16 geoneed { get; set; }//Location
        public Int16 Van_Sales { get; set; }//Van Sales
        public Int16 Geo_Track { get; set; }//Tracking
        public Int16 Geo_Fencing { get; set; }//Fencing
        public Int16 Eddy_Sumry { get; set; }//Edit Day Summary
        public Int16 DCR_Summary_ND { get; set; }// Channel Entry
        public string sf_type { get; set; }
    }
    [WebMethod]
    public static string getSettingsList()
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();
        string Qry = "select * from Master..Mas_Setting_Field where FormType = 'Employee'";
        try
        {
            ds = db_ER.Exec_DataSet(Qry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string getsf(string div_code, string sf_code)
    {
        DataSet dsEmployee = null;
        Menu_Creation_Employee_Settings_Dynamic SetReff = new Menu_Creation_Employee_Settings_Dynamic();
        dsEmployee = SetReff.GetEmployees_sp(div_code.TrimEnd(','), sf_code);
        return JsonConvert.SerializeObject(dsEmployee.Tables[0]);
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
        string str = "Select u.Field_Name,iif(isnull(CAST(u.Value as varchar),'')<>'',CAST(u.Value as varchar),M.Value) Value,u.sf_code from(select sf_code," + castvar + " from Access_Table where Div_Code = " + div_code + ") tt" +
            " unpivot(Value for Field_Name in (" + Unpivot + ")) u inner join Master..Mas_setting_Field M on M.Field_Name = u.Field_Name where FormType = 'Employee'";
        ds = db_ElR.Exec_DataSet(str);

        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getdata(string div_code, string sf_code)
    {
        AdminSetup adm = new AdminSetup();
        //if (sf_type == "3")
        //{
        Menu_Creation_Employee_Settings_Dynamic SetReff = new Menu_Creation_Employee_Settings_Dynamic();
        DB_EReporting db_ER = new DB_EReporting();
        DataTable dt = new DataTable();
		DataSet dsAccessmas = new DataSet();
        try
        {

            dt.Columns.Add("Reporting_To_SF", typeof(string));
            dt.Columns.Add("Sf_code", typeof(string));
            dt.Columns.Add("Sf_name", typeof(string));

            dt.Columns.Add("GeoNeed", typeof(int));
            dt.Columns.Add("Geo_Track", typeof(int));
            dt.Columns.Add("Geo_Fencing", typeof(int));
            dt.Columns.Add("Eddy_Sumry", typeof(int));
            dt.Columns.Add("Van_Sales", typeof(int));
            dt.Columns.Add("DCR_Summary_ND", typeof(string));
            dt.Columns.Add("sf_type", typeof(string));

            List<empdata> empList = new List<empdata>();

            string qry = "select * from Access_Table where Div_Code= '" + div_code.TrimEnd(',') + "'";

            DataSet dsEmployee = SetReff.GetEmployees_sp(div_code.TrimEnd(','), sf_code);
            dsAccessmas = db_ER.Exec_DataSet(qry);


            foreach (DataRow row in dsEmployee.Tables[0].Rows)
            {
                string str = row["Sf_code"].ToString().TrimEnd(',');

                empdata emp = new empdata();


                //GridView1.DataSource = dsAccessmas.Tables[0];
                //GridView1.DataBind();
                Int32 count = 0;
                if (!row["Sf_code"].ToString().TrimEnd(',').Equals("admin"))
                {
                    count = dsAccessmas.Tables[0].Select("sf_code = '" + row["Sf_code"].ToString().TrimEnd(',') + "'").Length;

                    if (count > 0)
                    {
                        foreach (DataRow rowa in dsAccessmas.Tables[0].Select("Sf_code = '" + row["Sf_code"].ToString() + "'"))
                        {
                            //rbtnnre.SelectedValue = row["UNLNeed"] == DBNull.Value ? "1" : row["UNLNeed"].ToString();// row["UNLNeed"].ToString(); 
                            //   bool geoneed = rowa["GeoNeed"] == DBNull.Value ? true : !Convert.ToBoolean(Convert.ToInt16(rowa["GeoNeed"]));
                            Int16 geoneed = rowa["GeoNeed"] == DBNull.Value ? Convert.ToInt16(0) : Convert.ToInt16(rowa["GeoNeed"]);
                            Int16 geotrack = rowa["geo_track"] == DBNull.Value ? Convert.ToInt16(0) : Convert.ToInt16(rowa["geo_track"]); // rowa["geo_track"] == DBNull.Value ? true : !Convert.ToBoolean(Convert.ToInt16(rowa["geo_track"]));
                            Int16 geofencing = rowa["geo_Fencing"] == DBNull.Value ? Convert.ToInt16(0) : Convert.ToInt16(rowa["geo_Fencing"]); // rowa["geo_Fencing"] == DBNull.Value ? false : Convert.ToBoolean(Convert.ToInt16(rowa["geo_Fencing"]));
                            Int16 eddysumry = rowa["Eddy_Sumry"] == DBNull.Value ? Convert.ToInt16(0) : Convert.ToInt16(rowa["Eddy_Sumry"]); // rowa["Eddy_Sumry"] == DBNull.Value ? true : !Convert.ToBoolean(Convert.ToInt16(rowa["Eddy_Sumry"]));
                            Int16 chnen = rowa["DCR_Summary_ND"] == DBNull.Value ? Convert.ToInt16(0) : Convert.ToInt16(rowa["DCR_Summary_ND"]);
                            Int16 vsal = rowa["Van_Sales"] == DBNull.Value ? Convert.ToInt16(0) : Convert.ToInt16(rowa["Van_Sales"]);
                            dt.Rows.Add(row["Reporting_To_SF"].ToString(), row["Sf_code"].ToString(), row["Sf_Name"].ToString(), geoneed, geotrack, geofencing, eddysumry, vsal, chnen, row["sf_type"].ToString());

                            emp.Reporting_To_SF = row["Reporting_To_SF"].ToString();
                            emp.sfcode = row["Sf_code"].ToString();
                            emp.Sf_name = row["Sf_Name"].ToString();
                            emp.geoneed = geoneed;
                            emp.Geo_Track = geotrack;
                            emp.Geo_Fencing = geofencing;
                            emp.Eddy_Sumry = eddysumry;
                            emp.DCR_Summary_ND = chnen;
                            emp.Van_Sales = vsal;
                            emp.sf_type = row["sf_type"].ToString();
                            empList.Add(emp);
                        }
                    }
                    else
                    {
                        Int16 geoneed = 0;
                        Int16 geotrack = 0;
                        Int16 geofencing = 0;
                        Int16 eddysumry = 0;
                        Int16 chnen = 0;
                        Int16 vsal = 0;
                        dt.Rows.Add(row["Reporting_To_SF"].ToString(), row["Sf_code"].ToString(), row["Sf_Name"].ToString(), geoneed, geotrack, geofencing, eddysumry, vsal, chnen, row["sf_type"].ToString());

                        emp.Reporting_To_SF = row["Reporting_To_SF"].ToString();
                        emp.sfcode = row["Sf_code"].ToString();
                        emp.Sf_name = row["Sf_Name"].ToString();
                        emp.geoneed = geoneed;
                        emp.Geo_Track = geotrack;
                        emp.Geo_Fencing = geofencing;
                        emp.Eddy_Sumry = eddysumry;
                        emp.DCR_Summary_ND = chnen;
                        emp.Van_Sales = vsal;
                        emp.sf_type = row["sf_type"].ToString();
                        empList.Add(emp);
                    }
                }
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dsAccessmas.Tables[0]);
    }
    public DataSet GetEmployees_sp(string div_code, string sf_code, string Sub_Div_Code = "0", string Alpha = "1", string statecode = "0")
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "exec [getHyrSFList] '" + sf_code + "','" + div_code + "','" + Sub_Div_Code + "','" + Alpha + "','" + statecode + "'";
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
    public static string HeaderChng(int ChngVal,int ID)
    {
        DataTable dt = new DataTable();
        //DataTable str = JsonConvert.DeserializeObject<DataTable>(ChngVal);
        DB_EReporting db_ER = new DB_EReporting();
        try
        {            
                string strQry = "update Master..Mas_Setting_Field set EmpChk = " + ChngVal + " where SetUp_ID = " + ID +"" +
                    " if (@@ROWCOUNT > 0)" +
                    " select * from Master..Mas_Setting_Field where FormType = 'Employee'";
                dt = db_ER.Exec_DataTable(strQry);                   
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dt);
    }
    [WebMethod]
    public static string savedata(string data)
    {
        Menu_Creation_Employee_Settings_Dynamic ms = new Menu_Creation_Employee_Settings_Dynamic();
        return ms.save(data);
    }
    public bool sRecordExistaccess_master(string SfCode)
    {
        string strQry = "";
        bool bRecordExist = false;
        try
        {
            DB_EReporting db = new DB_EReporting();

            strQry = "Select count(*) from Access_Table where sf_code = '" + SfCode + "'";
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
    private string save(string data)
    {
        /*sf_type = Session["sf_type"].ToString();
        sf_code = Session["sf_code"].ToString();*/
        DataTable str = JsonConvert.DeserializeObject<DataTable>(data);
        string strQry = ""; int iReturn = -1;
        DB_EReporting db = new DB_EReporting();
        div_code = Convert.ToString(Session["div_code"]);
        int co = 0;
        try
        {
            for (int i = 0; i < str.Rows.Count; i++)
            {
                strQry = "UPDATE Access_Table SET " + str.Rows[i]["Field_Name"] + "='" + str.Rows[i]["Value"] + "' WHERE Div_Code='" + div_code + "' and sf_code='" + str.Rows[i]["sf_code"].ToString() + "'";
                iReturn = db.ExecQry(strQry);
                co++;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        if (co > 0)
        {
            return "Sucess";
        }
        else
        {
            return "Record Not Insert";
        }
    }
  
    public int insert_acmas(string sf_code, string sf_name, byte geoneed, byte geotrack, byte geofencing, byte eddysumry, byte vansl, byte chnen, string div_code, Int16 sf_type)
    {
        DB_EReporting db_ER = new DB_EReporting();

        int iReturn = -1;

        string strQry = "exec [Insert_acctable] '" + sf_code + "','" + sf_name + "','" + geoneed + "','" + geotrack + "','" + geofencing + "','" + eddysumry + "','" + vansl + "','" + chnen + "','" + div_code + "','" + sf_type + "'";
        try
        {
            iReturn = db_ER.ExecQry(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return iReturn;



    }
    public class Item
    {
        public string sf_code { get; set; }
        public string sf_name { get; set; }
        public byte geoneed { get; set; }
        public byte geotrack { get; set; }
        public byte geofencing { get; set; }
        public byte eddysumry { get; set; }
        public byte vsal { get; set; }
        public byte chnen { get; set; }
        public string sf_type { get; set; }

    }
}