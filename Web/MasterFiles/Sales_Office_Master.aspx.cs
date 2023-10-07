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

public partial class MasterFiles_Sales_Office_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod(EnableSession = true)]
    public static string getSalesOffice(string divcode)
    {
        SOff ast = new SOff();
        DataSet ds = ast.getSOffice(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetRegionDetails(string divcode)
    {
        SOff dv = new SOff();
        DataSet ds = dv.getRegion(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getPreSOff(string divcode, string scode)
    {
        SOff dv = new SOff();
        DataSet ds = dv.getPreSOff(scode, divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static int SetNewStatus(string SF, string stus)
    {
        SOff dv = new SOff();
        int iReturn = dv.SOff_DeActivate(SF, stus);
        return iReturn;
    }
    [WebMethod]
    public static string insertdata(string id, string sname, string socode,string reg,string splant, string divcode)
    {
        SOff dv = new SOff();
        int ds = dv.savedetails(id, sname, socode, reg, splant, divcode);
        if (ds > 0)
        {
            return "save";
        }
        else
        {
            return "Failure";
        }
    }

    public class SOff
    {
        public DataSet getSOffice(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            
            string strQry = "select sOffID,sOffName,sOffCode,RegionId,PlantId,DivId,Act_Flg,case Act_Flg when 0 then 'Active' else 'Deactivate' end Status from Mas_SalesOffice where DivId='" + divcode + "' order by sOffName ";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataSet getRegion(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            
            string strQry = "select Area_code,Area_sname,Area_name,Div_Code,Area_Active_Flag from Mas_Area where Area_Active_Flag=0 and Div_Code='" + divcode + "'";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataSet getPreSOff(string scode, string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "Select * from Mas_SalesOffice where sOffID='" + scode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public int SOff_DeActivate(string plcode, string stus)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                string strQry = "update Mas_SalesOffice set Act_Flg='" + stus + "' where sOffID='" + plcode + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int savedetails(string id, string sname, string socode, string reg, string splant, string divcode)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                string strQry = "exec save_salesoffice '" + id + "','" + sname + "','" + socode + "','" + reg + "','" + splant + "','" + divcode + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
    }
}