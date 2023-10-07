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

public partial class MasterFiles_Reports_tsr_Retailer_Report_Format_new : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    public static string sf_code = string.Empty;
    public static string Mode = string.Empty;
    public static string sf_type = string.Empty;
    public static string Sf_Zone = string.Empty;
    DataSet dsSf = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Mode = Request.QueryString["Mode"].ToString();
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();




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
        dt = Ad.get_Identification(divcode);

        return JsonConvert.SerializeObject(dt);
    }
}