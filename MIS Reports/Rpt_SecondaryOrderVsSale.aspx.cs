using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;

public partial class MIS_Reports_Rpt_SecondaryOrderVsSale : System.Web.UI.Page
{
    DataSet ds = null;
    static string SF_Code = string.Empty;
    static string year = string.Empty;
    static string month = string.Empty;
    static  string Mgr_name = string.Empty;
    public static string div = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        year = Request.QueryString["year"].ToString();
        month = Request.QueryString["month"].ToString();      
            div = Session["div_code"].ToString();    
        Mgr_name = Request.QueryString["Mgr_name"].ToString();

        if (Convert.ToString(Request.QueryString["SF_Code"].ToString()) == "0") {
            SF_Code = Request.QueryString["Mgr_code"].ToString();
            txtsfname.Text = Request.QueryString["Mgr_name"].ToString();
        }
        else {
            SF_Code = Request.QueryString["SF_Code"].ToString();
            txtsfname.Text = Request.QueryString["mr_name"].ToString();
        }
        var N = int.Parse(month);
        lblHead.Text = "Secondary order vs Sales Analysis for the month " + getAbbreviatedName(N) + " year  " +  year;
        //fillgvdata();

    }
    static string getAbbreviatedName(int month)
    {
        DateTime date = new DateTime(2020, month, 1);

        return date.ToString("MMM");
    }
    //private void fillgvdata()
    //{
    //    SalesForce sf = new SalesForce();

    //    ds = sf.Secordercnf(div, SF_Code, month, year);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        gvdata.DataSource = ds;
    //        gvdata.DataBind();
    //    }
    //}
    [WebMethod]
    public static string gettotaldetSF(string Div)
    {
        DataSet ds = null;
        string strQry = string.Empty;

        ds = gettotaldetails(Div, SF_Code, month, year);

        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet gettotaldetails(string divcode, string SF_Code, string month, string year)
    {
        string strQry = string.Empty;
        DataSet dsRemark = null;

        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "exec  gettotaldetails'" + SF_Code + "','" + divcode + "','" + month + "','" + year + "'";

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