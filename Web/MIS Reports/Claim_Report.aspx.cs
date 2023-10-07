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

public partial class MIS_Reports_Claim_Report : System.Web.UI.Page
{
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public static string YR = string.Empty;
	 public static string SYR = string.Empty;
    public DateTime rdt;
    public DateTime sdt;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfcode = Request.QueryString["sfcode"].ToString();
        sfname = Request.QueryString["sfname"].ToString();
        subdiv = Request.QueryString["subdiv"].ToString();
        YR =  Request.QueryString["YR"].ToString();
		 SYR = Request.QueryString["syear"].ToString();
        Label1.Text = "Claim Analysis Report for " + YR;
        Label2.Text = "FieldForce Name: " + sfname;
    }
    [WebMethod]
    public static string getClaim(string Div)
    {
        ListedDR SFD = new ListedDR();
        DataSet ds = SFD.getRptClaim(sfcode, Div, YR, SYR);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getClaimUsr(string Div)
    {
        ListedDR SFD = new ListedDR();
        DataSet ds = SFD.getRptClaimDets(sfcode, Div, YR, SYR);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getSlab(string Div)
    {
        ListedDR SFD = new ListedDR();
        DataSet ds = SFD.getCliamSalb(Div, YR, SYR);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}