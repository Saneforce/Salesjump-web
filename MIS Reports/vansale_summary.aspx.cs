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

public partial class MIS_Reports_vansale_summary : System.Web.UI.Page
{
    public string sf_type = string.Empty;
    public static string div_code = string.Empty;
    public static string sf_code = string.Empty;
    public static string sub_division = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString().TrimEnd(',');
        sub_division = Session["sub_division"].ToString();
    }
    [WebMethod]
    public static string SalesForceListMgrGet_MgrOnly()
    {
        newvn sd = new newvn();
        DataSet ds = new DataSet();
        ds = sd.SalesForceListMgrGet_MgrOnly(div_code, sf_code, "0");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getFieldForce(string divcode, string sfcode)
    {
        newvn sd = new newvn();
        DataSet ds = new DataSet();
        ds = sd.UserList_getMR(divcode, sfcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public class newvn
    {
        public DataSet UserList_getMR(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            string strQry = "EXEC getHyrSFList_MR '" + sf_code + "', '" + divcode + "', '0', '0' ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
        public DataSet SalesForceListMgrGet_MgrOnly(string divcode, string sf_code, string Sub_Div_Code = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            string strQry = "EXEC getHyrSFList_MGR '" + divcode + "', '" + sf_code + "', '0' ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
    }
}