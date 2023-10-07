﻿using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Bus_EReport;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data.SqlClient;

public partial class MasterFiles_Reports_Rpt_Retailer_Tag_Breakup : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string sfname = string.Empty;
    string subdiv_code = string.Empty;
    public static string ttype = string.Empty;
    public static string TDate = string.Empty;
    DataSet ff = new DataSet();
    DataSet ss = new DataSet();
    DataSet dsd = new DataSet();
    DataSet dsGV = new DataSet();
    int gridcnt;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["SF_Code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        subdiv_code = Request.QueryString["subDiv"].ToString();
        ttype= Request.QueryString["TagType"].ToString();
        FillSF();
    }
    private void FillSF()
    {
        string strQry = string.Empty;
        DataSet dsTerritory = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("exec getRetailer_Tag_Status '" + Sf_Code + "','" + divcode + "','" + ttype + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsTerritory);
        con.Close();
        gvMyDayPlan.DataSource = dsTerritory;
        gvMyDayPlan.DataBind();
    }
}