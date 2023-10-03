using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MIS_Reports_PrimaryOrder : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string sfcode1 = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    string mnthname = string.Empty;
    string Yr = string.Empty;

    DataSet dsSalesForce = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfcode1 = Session["sf_code"].ToString();
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {            
            //FillReporting();            
        }
       
    }
    //private void FillReporting()
    //{
    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.GetDistributorName(divcode);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlDistributor.DataTextField = "Stockist_Name";
    //        ddlDistributor.DataValueField = "Stockist_Code";
    //        ddlDistributor.DataSource = dsSalesForce;
    //        ddlDistributor.DataBind();
    //        ddlDistributor.Items.Insert(0, new ListItem("--Select Distributor--", "0"));
    //    }
    //    else
    //    {
    //        Response.Write("Values not added");
    //    }
    //}
    public class sfMGR
    {
        public string stockname { get; set; }
        public string stockcode { get; set; }
    }


    //public static sfMGR[] getMGR()
    //{
    //    SalesForce dsf = new SalesForce();
    //    DataSet dsSalesForce = dsf.GetDistributorName(divcode,sfcode1);
    //    List<sfMGR> sf = new List<sfMGR>();      
    //    foreach (DataRow rows in dsSalesForce.Tables[0].Rows)
    //    {
    //        sfMGR rt = new sfMGR();
    //        rt.stockcode = rows["Stockist_Code"].ToString();
    //        rt.stockname = rows["Stockist_Name"].ToString();
    //        sf.Add(rt);
    //    }
    //    return sf.ToArray();
    //}
    [WebMethod]
    public static string getMGR(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.GetDistributorName(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    [WebMethod]
    public static string GetData(string SF, string Div, string Mn, string Yr)
    {
        divcode = Div;
        sfcode1 = SF;
        fdt = Mn;
        tdt = Yr;
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        ds = SFD.GetDistributorPrimary_Report(SF, Div.TrimEnd(','), Mn, Yr);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetContactAddress(string Stockist_Code)
    {        
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        ds = SFD.GetContactAddressDetails(Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }   
}