using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Reports_tsr_ExpenseClaimRpt : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    public static string sf_code = string.Empty;
    public static string Mode = string.Empty;
    public static string sf_type = string.Empty;
    DataSet dsSf = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Mode = Request.QueryString["Mode"].ToString();

        string div_code1 = Convert.ToString(Session["div_code"]);
        string sf_code1 = Convert.ToString(Session["sf_code"]);
        string sf_type1 = Convert.ToString(Session["sf_type"]);

        if (div_code1 == "" || div_code1 == null)
            div_code = "";
        else
            div_code = Convert.ToString(div_code1);

        if (sf_code1 == "" || sf_code1 == null)
            sf_code = "";
        else
            sf_code = Convert.ToString(sf_code1);

        if (sf_type1 == "" || sf_type1 == null)
            sf_type = "";
        else
            sf_type = Convert.ToString(sf_type1);
    }

    [WebMethod]
    public static string getstate(string divcode)
    {
        DataTable dt = new DataTable();
        SalesForce Ad = new SalesForce();
        dt = Ad.get_States(divcode, sf_code, "0");
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static string getZone(string divcode)
    {
        DataTable dt = new DataTable();
        SalesForce Ad = new SalesForce();
        dt = Ad.get_Fields(divcode);
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static string getSO(string divcode, string mgrid)
    {
        DataTable dt = new DataTable();
        SalesForce Ad = new SalesForce();
        dt = Ad.get_ddlFieldForce(divcode, mgrid);
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static string getDistributor(string divcode)
    {
        DataTable dt = new DataTable();
        SalesForce Ad = new SalesForce();
        dt = Ad.get_Distributor(div_code);
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static string getIdentification(string divcode)
    {
        DataTable dt = new DataTable();
        SalesForce Ad = new SalesForce();
        dt = Ad.get_Identification(div_code);

        return JsonConvert.SerializeObject(dt);
    }
}