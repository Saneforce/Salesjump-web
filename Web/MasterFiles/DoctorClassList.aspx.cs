using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;

public partial class MasterFiles_DoctorClassList : System.Web.UI.Page
{
    int DocClsCode = 0;
    string divcode = string.Empty;
    string sf_type = string.Empty;
    string div_code = string.Empty;
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
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod(EnableSession = true)]
    public static string GetDetails(string divcode)
    {
        classtst dv = new classtst();
        DataSet ds = dv.getDocCls(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string deactivate(int Doc_ClsCode, string stat)
    {
        classtst dv = new classtst();
        int iReturn = dv.DeActivateCls(Doc_ClsCode, stat);
        if (iReturn > 0)
        {
            return "Success";
        }
        else
        {
            return "Failure";
        }
    }
    public class classtst
    {
        public DataSet getDocCls(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            string strQry = " SELECT c.Doc_ClsCode,c.Doc_ClsSName,c.Doc_ClsName,c.Doc_Cls_ActiveFlag ," +
                     " (select count(d.Doc_ClsCode) from Mas_ListedDr d where d.Doc_ClsCode = c.Doc_ClsCode and ListedDr_Active_Flag=0) as Cls_Count " +
                     "  FROM  Mas_Doc_Class  c" +
                     " WHERE c.Division_Code=  '" + divcode + "' " +
                     " ORDER BY c.Doc_ClsSNo";
            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }
        public int DeActivateCls(int Doc_ClsCode,string stat)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                string strQry = "UPDATE Mas_Doc_Class " +
                            " SET Doc_Cls_ActiveFlag    ='" + stat + "', " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_ClsCode = '" + Doc_ClsCode + "' ";

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