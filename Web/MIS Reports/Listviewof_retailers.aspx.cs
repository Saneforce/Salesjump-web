using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;
using iTextSharp.tool.xml;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;

public partial class MIS_Reports_Listviewof_retailers : System.Web.UI.Page
{
    static string sfCode = string.Empty;
    static string sfname = string.Empty;
    static string divcode = string.Empty;
    static string FMonth = string.Empty;
    static string FYear = string.Empty;
    static string TMonth = string.Empty;
    static string TYear = string.Empty;
    static string Routename = string.Empty;
    static string Route = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        sfname = Request.QueryString["sfname"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        Route = Request.QueryString["Route"].ToString();
        Routename = Request.QueryString["Routename"].ToString();

        lblHead.Text = "Retailers Name for  " + Routename +" Route";
    }
    [WebMethod(EnableSession = true)]
    public static string getroutecnt()
    {
        Product SFD = new Product();
        DataSet ds = getDCRUsers(sfCode, divcode, FMonth, TMonth, FYear, TYear, Route);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getDCRUsers(string sfCode, string DivCode, string FMonth, string TMonth, string FYear, string TYear, string Route)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "exec getRoutedetsforcus '" + sfCode + "','" + DivCode + "','" + FMonth + "','" + TMonth + "','" + FYear + "','" + TYear + "','" + Route + "'";

        try
        {
            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }
}