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

public partial class MIS_Reports_rpt_vansale_summary : System.Web.UI.Page
{
    public static string sfCode = string.Empty;
    public static string sfname = string.Empty;
    public static string Fdate = string.Empty;
    public static string Tdate = string.Empty;
    public static string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString().TrimEnd(',');
        sfCode = Request.QueryString["SF_Code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        Fdate = Request.QueryString["fdate"].ToString();
        Tdate = Request.QueryString["tdate"].ToString();
    }
    [WebMethod]
    public static string getdata()
    {
        newvn sd = new newvn();
        DataSet ds = new DataSet();
        ds = sd.GetHeader(div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetDetail()
    {
        newvn sd = new newvn();
        DataSet ds = new DataSet();
        ds = sd.GetDetail();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public class newvn
    {
        public DataSet GetDetail()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            string strQry = "exec sp_van_collection '"+ div_code + "','"+ sfCode + "','"+ Fdate + "','"+ Tdate + "'";

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
        public DataSet GetHeader(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            string strQry = "select code,Name from mas_payment_type where Division_Code='"+ div_code + "' and Active_Flag=0  order by Name";

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