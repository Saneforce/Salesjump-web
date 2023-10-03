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

public partial class MasterFiles_View_Username_Password : System.Web.UI.Page
{

    #region Declaration
    public static string divcode = string.Empty;
    public static string baseUrl = "";
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if (Session["div_code"] != null)
        { divcode = Session["div_code"].ToString(); }
        else { Page.Response.Redirect(baseUrl, true); }
    }

    [WebMethod]
    public static string getCompanyList()
    {
        DataSet ds = new DataSet();
        ds = GetCompanyListMaster();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string getUSNmPassWrd(string div, string dbnm, string compnm)
    {
        if ((div == "" || div == null))
        { div = Convert.ToString(divcode); }

        DataSet ds = new DataSet();
        DataSet ins = new DataSet();
        ds = GetUserPasswords(div, dbnm);
        ins = InsertViewTable(div, dbnm, compnm);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet InsertViewTable(string div, string dbnm, string compnm)
    {
        if ((div == "" || div == null))
        { div = Convert.ToString(divcode); }

        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsSF = null;
        string strQry = "insert into password_viewed values('" + div + "','" + compnm + "','" + dbnm + "',getdate())";

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

    public static DataSet GetUserPasswords(string div, string dbnm)
    {
        if ((div == "" || div == null))
        { div = Convert.ToString(divcode); }

        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsSF = null;
        string strQry = "exec GET_USER_PASSWORD '" + div + "','" + dbnm + "'";

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

    public static DataSet GetCompanyListMaster()
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsSF = null;
        string strQry = "select Cust_Name,Cust_DBName,Cust_DivID from master.dbo.mas_customers";

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