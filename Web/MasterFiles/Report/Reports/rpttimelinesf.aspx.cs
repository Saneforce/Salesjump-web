using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Reports_rpttimelinesf : System.Web.UI.Page
{
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string stcode = string.Empty;
    public static string FDTs = string.Empty;
    public static string TDTs = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public static string retcode = string.Empty;
    public string fdate = string.Empty;
    public string tdate = string.Empty;
    public DateTime rdt;
    public DateTime sdt;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfcode = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        retcode = Request.QueryString["retcode"].ToString();
        FDTs = Request.QueryString["date"].ToString();
        stcode = Request.QueryString["stcode"].ToString();
        //TDTs = Request.QueryString["tdate"].ToString();
        //subdiv = Request.QueryString["subdiv"].ToString();

        DateTime result4 = DateTime.ParseExact(FDTs, "d/MM/yyyy", CultureInfo.InvariantCulture);
        FDT = result4.ToString("yyyy-MM-dd");

       
        rdt = Convert.ToDateTime(FDTs);
        

        Label1.Text = "Daily timeline report of  " + rdt.ToString("dd/MM/yy");
        Label2.Text = "FieldForce Name: " + sfname;
    }
    [WebMethod]
    public static string getdistsfdet(string Div)
    {
        DataSet ds = null;
        string strQry = string.Empty;

        ds = getdistsfdetail(Div, sfcode, retcode, FDT);

        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getdistsfdetail(string divcode, string sfcode, string retcode, string FDT)
    {
        string strQry = string.Empty;
        DataSet dsRemark = null;

        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "exec sfloginret '" + sfcode + "','" + divcode + "','" + retcode + "','" + FDT + "'";

        try
        {
            dsRemark = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsRemark;
    }
    [WebMethod]
    public static string getretailsfdet(string Div)
    {
        DataSet ds = null;
        string strQry = string.Empty;

        ds = getretsfdetail(Div, sfcode, retcode, FDT);

        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getretsfdetail(string divcode, string sfcode, string retcode, string FDT)
    {
        string strQry = string.Empty;
        DataSet dsRemark = null;

        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "exec sfloginretailer '" + sfcode + "','" + divcode + "','" + retcode + "','" + FDT + "'";

        try
        {
            dsRemark = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsRemark;
    }
}