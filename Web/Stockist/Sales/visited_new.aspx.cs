using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Services;
public partial class Stockist_Sales_visited_new : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "1" || sf_type == "2")
        {
            sf_code = Session["Title_MR"].ToString();
            div_code = Session["div_code"].ToString();


        }
        else
        {


        }

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            BindStockiest();

        }
    }
    private void BindStockiest()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string DSub_DivCode = string.Empty;
        DataSet dspono = null;
        SalesForce sm = new SalesForce();
        dspono = sm.getSalesForce_BulkEdit(div_code.TrimEnd(','));
        if (dspono.Tables[0].Rows.Count > 0)
        {
            idpro.DataSource = dspono;
            idpro.DataTextField = "Sf_Name";
            idpro.DataValueField = "sf_code";
            idpro.DataBind();
            idpro.Items.Insert(0, new ListItem("---SELECT---", "0"));
        }
    }
    [WebMethod(EnableSession = true)]
    public static string getdatauser(string fdate, string tdate, string sf)
    {
        SalesForce dv = new SalesForce();
        DataSet dsProd = dv.get_visited_user(div_code, fdate, tdate, sf);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getclasdtl()
    {
        SalesForce dv = new SalesForce();
        DataSet dsProd = dv.get_visited_cls(div_code);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getshpdtl(string fdate, string tdate, string sf)
    {
        SalesForce dv = new SalesForce();
        DataSet dsProd = dv.get_visited_shp(div_code, fdate, tdate, sf);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getuserdates(string fdate, string tdate, string sf)
    {
        SalesForce dv = new SalesForce();
        DataSet dsProd = dv.get_visited_dates(div_code, fdate, tdate, sf);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
}