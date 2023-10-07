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
public partial class MIS_Reports_rpt_categorysale_view : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod(EnableSession = true)]
    public static string saledr(string fromdate, string todate, string stcode)
    {
        Product dv = new Product();
        string divcode = HttpContext.Current.Session["div_code"].ToString();

        DataSet dsProd = dv.dissaledr(divcode, fromdate, todate, stcode);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
	 [WebMethod(EnableSession = true)]
    public static string saledr_visitdate(string fromdate, string todate, string stcode)
    {
        Product dv = new Product();
        string divcode = HttpContext.Current.Session["div_code"].ToString();

        DataSet dsProd = dv.dissaledr_visdate(divcode, fromdate, todate, stcode);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
}