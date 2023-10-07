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

public partial class MasterFiles_view_taluk : System.Web.UI.Page
{  
       string sf_type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
	  protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    [WebMethod]
    public static string getfilldist(string divcode)
    {
        vtlk sf = new vtlk();
        DataSet ds = sf.gettalk_dtl(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string deactivatedist(string dtcode, string stat)
    {
        vtlk dv = new vtlk();
        int iReturn = dv.tlkDeActivate(dtcode, stat);
        if (iReturn > 0)
        {
            return "Success";
        }
        else
        {
            return "Failure";
        }
    }
    public class vtlk
    {
        public DataSet gettalk_dtl(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            string strQry = "exec [gettalukMaster] '"+ divcode + "'";
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
        public int tlkDeActivate(string dtcode, string stat)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                string strQry = "exec [taluk_Deactivate] '" + dtcode + "'," + stat + " ";
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