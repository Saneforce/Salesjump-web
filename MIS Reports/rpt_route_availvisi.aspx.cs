using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.Services;
public partial class MIS_Reports_rpt_route_availvisi : System.Web.UI.Page
{
    string div_code = string.Empty;
    //string sf_code = string.Empty;
    public static string sf_code { get; set; }
    public static string startDate { get; set; }
   // public static string year { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Convert.ToString(Request.QueryString["sfcode"]);
        startDate = Convert.ToString( Request.QueryString["startDate"]);
        //year = Convert.ToString(Request.QueryString["year"]);
        txtsfnane.Text = Convert.ToString(Request.QueryString["sfName"]);
        txtmonyear.Text = Convert.ToString(Request.QueryString["monyear"]);
    }
    [WebMethod]
    public static List<Route_values> Routevisible()
    {
        SalesForce SF = new SalesForce();
        List<Route_values> expanse = new List<Route_values>();
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
       
        DataSet dsCounts = new DataSet();
        dsCounts = SF.get_route_view(div_code, sf_code, startDate);       
        foreach (DataRow row in dsCounts.Tables[0].Rows)        {
            Route_values ex = new Route_values();
            ex.Product_Code = row["Product_Code"].ToString();
            ex.Product_Name = row["Product_Name"].ToString();
            ex.Territory_Name = row["Territory_Name"].ToString();
            ex.Available = row["Available"].ToString();
            ex.Visible = row["Visible"].ToString();          
            expanse.Add(ex);
        }
        return expanse.ToList();
    }
    [WebMethod]
    public static List<Product_values> Productvisible()
    {
        SalesForce SF = new SalesForce();
        List<Product_values> expanse = new List<Product_values>();
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }

        DataSet dsCount = new DataSet();
        dsCount = SF.get_route_viewpro(div_code, sf_code, startDate);
        foreach (DataRow row in dsCount.Tables[0].Rows)
        {
            Product_values ex = new Product_values();
            ex.Product_Code = row["Product_Code"].ToString();
            ex.Product_Name = row["Product_Name"].ToString();
            expanse.Add(ex);
        }
        return expanse.ToList();
    }
    public class Product_values
   {     
        public string Product_Code { get; set; }
        public string Product_Name { get; set; }
    }
    public class Route_values
    {

        public string Territory_Name { get; set; }
        public string Available { get; set; }
        public string Visible { get; set; }
        public string Product_Code { get; set; }
        public string Product_Name { get; set; }
     
    }
}