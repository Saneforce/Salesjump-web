using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Reports_Rpt_Daywise_My_Day_Plan_View : System.Web.UI.Page
{

    public static string subdiv = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string stcode = string.Empty;   
    public static string fdate = string.Empty;
    public static string tdate = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sfcode = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        subdiv = Request.QueryString["subdiv"].ToString();
        fdate = Request.QueryString["fdate"].ToString();
        tdate = Request.QueryString["tdate"].ToString();
        stcode = Request.QueryString["stcode"].ToString();

        Label1.Text = "Daywise My day Plan details";
        Label2.Text = "FieldForce Name: " + sfname;
    }
    [WebMethod]
    public static string getplandets(string Div)
    {
        DataSet ds = null;
        string strQry = string.Empty;
        ds = getplandetails(Div, sfcode, fdate, tdate, subdiv);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getplandetails(string divcode, string sfcode, string fdate, string tdate, string subdiv)
    {
        string strQry = string.Empty;
        DataSet dsRemark = null;

        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "exec get_daywise_mydaypaln '" + sfcode + "','" + divcode + "','" + fdate + "','" + tdate + "','" + subdiv + "'";

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
