using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data.SqlClient;

public partial class MIS_Reports_labpharma_doa_view : System.Web.UI.Page
{
    public string SFCodes = string.Empty;
    public string mnths = string.Empty;
    public string subDivs = string.Empty;
    public string SfName = string.Empty;
    public  string typs = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        SFCodes = Request.QueryString["SfCode"].ToString();
        mnths = Request.QueryString["month"].ToString();
        subDivs = Request.QueryString["SubDiv"].ToString();
        SfName = Request.QueryString["sfName"].ToString();
        typs = Request.QueryString["typ"].ToString();

        hidn_sf_code.Value = SFCodes;
         hFMonth.Value = mnths;
        subDiv.Value = subDivs;
        type.Value = typs;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(mnths)).ToString().Substring(0, 3);
        lblsf_name.Text = SfName + " " + strFMonthName;

    }
    [WebMethod(EnableSession = true)]
    public static string getSFdets(string divcode, string sfc, string mnth,string typs)
    {
        SalesForce dv = new SalesForce();
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Exec labpharmadobdoa '" + sfc + "','" + divcode + "','" + mnth + "','" + typs + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);
    }
    [WebMethod(EnableSession = true)]
    public static string getStockist(string divcode)
    {
        string strQry = string.Empty;
        DataSet dsTerritory = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select Stockist_Code,Stockist_Name from Mas_Stockist where Division_Code=" + divcode.TrimEnd(',') + " and Stockist_Active_Flag=0", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsTerritory);
        con.Close();
        return JsonConvert.SerializeObject(dsTerritory.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getSalesforce(string divcode)
    {
        string strQry = string.Empty;
        DataSet dsTerritory = new DataSet();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select Sf_Code,Sf_Name from Mas_Salesforce where CHARINDEX('," + divcode.TrimEnd(',') + ",',','+Division_Code+',')>0", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsTerritory);
        con.Close();
        return JsonConvert.SerializeObject(dsTerritory.Tables[0]);
    }
}