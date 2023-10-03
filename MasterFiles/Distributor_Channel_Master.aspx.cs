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

public partial class MasterFiles_Distributor_Channel_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    public static string getAlldata()
    {
        DChnl ast = new DChnl();
        DataSet ds = ast.getdata();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string getPreDist( string scode)
    {
        DChnl dv = new DChnl();
        DataSet ds = dv.getPreDist(scode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static int SetNewStatus(string SF, string stus)
    {
        DChnl dv = new DChnl();
        int iReturn = dv.DChnl_DeActivate(SF, stus);
        return iReturn;
    }
    [WebMethod]
    public static string insertdata(string id, string dcname, string dccode)
    {
        DChnl dv = new DChnl();
        int ds = dv.savedetails(id, dcname, dccode);
        if (ds > 0)
        {
            return "save";
        }
        else
        {
            return "Failure";
        }
    }

    public class DChnl
    {

        public DataSet getdata()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            string strQry = "select CateId,CateNm,CateCode,flg,case flg when 0 then 'Active' else 'Deactivate' end Status from Mas_Stockist_Category order by CateNm ";
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
        public DataSet getPreDist(string scode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "Select * from Mas_Stockist_Category where CateId='" + scode + "'";
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
        public int DChnl_DeActivate(string plcode, string stus)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                string strQry = "update Mas_Stockist_Category set flg='" + stus + "' where CateId='" + plcode + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int savedetails(string id, string dcname, string dccode)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                string strQry = "exec save_Dist_Channel '" + id + "','" + dcname + "','" + dccode + "'";

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