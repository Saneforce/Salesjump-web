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
using DBase_EReport;

public partial class MIS_Reports_Inshop_Retailer : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string sfcode = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    string Division_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    private string strQry;

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
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
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hffilter.Value = "AllFF";
            hfilter.Value = "All";
            hsfhq.Value = "All";
        }
    }

    [WebMethod]
    public static string GetDetails(string SF, string Div, string fdt, string tdt)
    {
        divcode = Div;
        sfcode = SF;
      
        DataSet ds = new DataSet();
        string ConnectionString = Globals.ConnString;
        SqlConnection con = new SqlConnection(ConnectionString);       
        con.Open();
        SqlCommand cmd = new SqlCommand("EXEC getInshopRetailer '" + SF + "','" + Div + "','" + fdt + "','" + tdt + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        

        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    
}

