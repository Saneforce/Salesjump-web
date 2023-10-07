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
using System.Xml;
using System.IO;
using System.Data.OleDb;

public partial class MasterFiles_Reports_tsr_viewpromotion : System.Web.UI.Page
{
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv = string.Empty;
    public static string FDTs = string.Empty;
    public static string TDTs = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public static string BrandCode = string.Empty;
    public static string distcode = string.Empty;
    public static string distnm = string.Empty;
    public static string stcode = string.Empty;
    public static string stnm = string.Empty;
    public static string sf_Zone = string.Empty;
    public DateTime rdt;
    public DateTime sdt;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfcode = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        FDTs = Request.QueryString["FDate"].ToString();
        TDTs = Request.QueryString["TDate"].ToString();
        distcode = Request.QueryString["Dst_code"].ToString();
        distnm = Request.QueryString["Dst_name"].ToString();
        stnm = Request.QueryString["st_name"].ToString();
        stcode = Request.QueryString["st_code"].ToString();
        
        DateTime d1 = Convert.ToDateTime(FDTs);
        DateTime d2 = Convert.ToDateTime(TDTs);

        Label1.Text = "Direct Marketing from " + d1.ToString("dd-MM-yy") + " to " + d2.ToString("dd-MM-yy");
        //Label1.Text = "Promotions from " + d1.ToString("dd-MM-yy") + " to " + d2.ToString("dd-MM-yy");
        Label2.Text = "FieldForce Name: " + sfname;
        Label3.Text = "State Name: " + stnm;

        string[] zone = null;
        if (sfname.Contains('-'))
        {
            zone = sfname.Split('-');
            Label4.Text = "Zone :" + zone[2];
        }
        else
        {
            Label4.Text = "Zone :";
        }


        
    }

    [WebMethod]
    public static string promotiondtl(string Div)
    {
        Product SFD = new Product();
        DataSet ds = SFD.fillpromotiondtl(Div, sfcode, FDTs, TDTs,stcode, distcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}