using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using DocumentFormat.OpenXml.Drawing;
using System.IO;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Services;

public partial class MIS_Reports_rptTotalRetailSal : System.Web.UI.Page
{

    public string sfCode = string.Empty;
    public string sfName = string.Empty;
    public string fYear = string.Empty;
    public string fMonth = string.Empty;
    public string SubDiv = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfCode = Request.QueryString["sfCode"].ToString();
        sfName = Request.QueryString["sfName"].ToString();
        fYear = Request.QueryString["fYear"].ToString();
        fMonth = Request.QueryString["fMonth"].ToString();
        SubDiv = Request.QueryString["SubDiv"].ToString();

        hsfcode.Value = sfCode;
        hfyear.Value = fYear;
        hfmonth.Value = fMonth;
        hsubdiv.Value = SubDiv;

        lblhead.Text = "Retailer Visit Analysis for month of  " + Request.QueryString["mName"].ToString() + " " + fYear;
        subhead.Text = "Team : " + sfName;

    }
    public class Routes
    {
        public string rName { get; set; }
        public string rCode { get; set; }
        public string sfCode { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static Routes[] getRoutes(string SF_Code)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        RoutePlan rop = new RoutePlan();
        DataSet DsRoute = rop.get_Route_Name(div_code, SF_Code);
        List<Routes> vList = new List<Routes>();
        foreach (DataRow row in DsRoute.Tables[0].Rows)
        {
            Routes vl = new Routes();
            vl.rName = row["Territory_Name"].ToString();
            vl.rCode = row["Territory_Code"].ToString();
            vl.sfCode = row["SF_Code"].ToString();
            vList.Add(vl);
        }
        return vList.ToArray();
    }

    public class mrFieldForce
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static mrFieldForce[] getMRList(string SF_Code)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        SalesForce sf = new SalesForce();
        DataSet dsSalesForce = sf.UserList_getMR(div_code, SF_Code, "0","0");
        List<mrFieldForce> vList = new List<mrFieldForce>();
        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            mrFieldForce vl = new mrFieldForce();
            vl.sfCode = row["sf_code"].ToString();
            vl.sfName = row["sf_name"].ToString();
            vList.Add(vl);
        }
        return vList.ToArray();
    }


    public class TotOrder
    {
        public string RowNum { get; set; }
        public string rCode { get; set; }
        public string CustCode { get; set; }
		public string ListedDrCode { get; set; }
        public string custName { get; set; }
        public string orderVal { get; set; }
        public string CrDate { get; set; }
		 public string disname { get; set; }
        public string lastordDate { get; set; }
        public string Activity_Date { get; set; }
        public string Doc_Class_ShortName { get; set; }
        public string Doc_Spec_ShortName { get; set; }
        public string visitcount { get; set; }
        public string visitdate { get; set;}


    }
	  [WebMethod(EnableSession = true)]
    public static string getdistributr(string divcode)
    {
  
        SalesForce dv = new SalesForce();
        //DataSet dsProd = dv.getmaxatnce_prods(divcode, sfCode, FMonth, FYear, subdiv_code);
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select Stockist_Code,Stockist_Name from mas_stockist where Division_Code='" + divcode + "'and Stockist_Active_Flag=0", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return JsonConvert.SerializeObject(ds);

    }
    [WebMethod(EnableSession = true)]
    public static TotOrder[] getOrderDetails(string SF_Code, string FYear, string FMonth, string SubDiv)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        Order sf = new Order();
        DataSet dsSalesForce = sf.GetTotalRetailSal(div_code, SF_Code, FYear, FMonth, "0");
        List<TotOrder> vList = new List<TotOrder>();
        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            TotOrder vl = new TotOrder();
            
              //  vl.RowNum = row["roNum"].ToString();
			vl.ListedDrCode = row["ListedDrCode"].ToString();
            vl.rCode = row["sf_name"].ToString();
            vl.CustCode = row["Territory_Name"].ToString();
            vl.custName = row["ListedDr_Name"].ToString();
			vl.disname = row["dist_name"].ToString();
            vl.orderVal = row["Order_Value"].ToString();
            vl.CrDate = row["Created_Date"].ToString();
            vl.lastordDate = row["ListedDr_Phone"].ToString();
            vl.Activity_Date = row["Activity_Date"] == DBNull.Value ? " " : Convert.ToDateTime(row["Activity_Date"]).ToString("dd/MM/yyyy");
            vl.Doc_Class_ShortName = row["Doc_Class_ShortName"].ToString();
            vl.Doc_Spec_ShortName = row["Doc_Spec_ShortName"].ToString();
            vl.visitcount = row["vscount"].ToString();
            vl.visitdate = row["vsdate"].ToString();
            vList.Add(vl);
        }
        return vList.ToArray();
    }
}


//

