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

public partial class MIS_Reports_Merchand_Activity : System.Web.UI.Page
{
    static string rt_code = string.Empty;
    static string sl_no = string.Empty;
    static string rt_name = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sl_no = Request.QueryString["slno"].ToString();
        rt_code = Request.QueryString["rtCode"].ToString();
        rt_name = Request.QueryString["rtnm"].ToString();
        lblHead.Text = "Order Details For :" + rt_name + " ";
    }
    [WebMethod]
    public static string Act_Count()
    {
        DataSet ds = new DataSet();
        ret sf = new ret();
        ds = sf.Acty_Count(rt_code,sl_no);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public class ret
    {
        public DataSet Acty_Count(string rtcode,string Sl_No)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            string strQry = "Exec Retactivity_Count '" + rtcode + "','" + Sl_No + "'";
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