using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.Xml;
using System.IO;
using System.Data.OleDb;
public partial class MasterFiles_newuser_list : System.Web.UI.Page
{
 
     public static string sf_code = string.Empty;
    public string sf_type = string.Empty;
    public static string sub_division = string.Empty;
	 protected void Page_PreInit(object sender, EventArgs e)
    {
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
        else if (sf_type == "4")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       // div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
		 sub_division = Session["sub_division"].ToString();
    }
    [WebMethod]
    public static string getDivision(string divcode)
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.Getsubdivisionwise(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getcomDivision(string divcode)
    {
        Division dv = new Division();
        DataSet ds = new DataSet();
        ds = dv.getMultiDivision(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string fillddlmgr(string divcode, string subdiv)
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.SalesForceListMgrGet_MgrOnly(divcode, sf_code, subdiv);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string filldata(string comp, string divi, string fforce)
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.getuserlistdata(comp, divi, fforce);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string fillccont(string comp, string divi, string fforce)
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.getempcount(comp, divi, fforce);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}