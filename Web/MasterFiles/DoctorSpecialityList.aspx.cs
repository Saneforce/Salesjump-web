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

public partial class MasterFiles_DoctorSpecialityList : System.Web.UI.Page
{
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
        Dctsptst dv = new Dctsptst();
        DataSet ds = dv.getDocSpe(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string deactivate(int Doc_Special_Code, string stat)
    {
        Dctsptst dv = new Dctsptst();
        int iReturn = dv.DeActivateDocSpl(Doc_Special_Code, stat);
        if (iReturn > 0)
        {
            return "Success";
        }
        else
        {
            return "Failure";
        }
    }
    public class Dctsptst
    {
        public DataSet getDocSpe(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocSpe = null;

            string strQry = " SELECT s.Doc_Special_Code,s.Doc_Special_SName,s.Doc_Special_Name,s.Doc_Special_Active_Flag, " +
                     " (select count(d.Doc_Special_Code) from Mas_ListedDr d where d.Doc_Special_Code = s.Doc_Special_Code and ListedDr_Active_Flag=0) as Spec_Count" +
                     " FROM  Mas_Doctor_Speciality s " +
                     " WHERE s.Division_Code= '" + divcode + "' " +
                     " ORDER BY s.Doc_Spec_Sl_No ";
            try
            {
                dsDocSpe = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocSpe;
        }
        public int DeActivateDocSpl(int Doc_Special_Code,string stat)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                string strQry = "UPDATE Mas_Doctor_Speciality " +
                            " SET Doc_Special_Active_Flag='" + stat + "', " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Doc_Special_Code = '" + Doc_Special_Code + "' ";

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