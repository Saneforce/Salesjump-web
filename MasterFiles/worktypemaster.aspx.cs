using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data;
using Bus_EReport;
using DBase_EReport;

public partial class MasterFiles_worktypemaster : System.Web.UI.Page
{
    public string divcode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
    }
    [WebMethod]
    
    public static string getwtypeall(string divcode)
    {

        DataSet ds = getwtypeal(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }


    [WebMethod(EnableSession = true)]
    public static int SetNewStatus(string SF, string stus)
    {
        
        int iReturn = setwtypeDeActivate(SF, stus);
        return iReturn;
    }
    public static DataSet getwtypeal(string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet ds = null;
      String  strQry = "exec getworktype_master '" + divcode + "'  ";
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
    public static int setwtypeDeActivate(string plcode, string stus)
    {
        int iReturn = -1;
        try
        {
            DB_EReporting db = new DB_EReporting();
            string  strQry = "update Mas_WorkType_BaseLevel set active_flag='" + stus + "' where WorkType_Code_B=" + plcode + "";
            iReturn = db.ExecQry(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return iReturn;
    }
}
