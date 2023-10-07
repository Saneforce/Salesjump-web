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

public partial class MIS_Reports_cus_sale_report_view : System.Web.UI.Page
{
    public static string divcode = string.Empty;
    public static string subdivc = string.Empty;
    public static string Sf_Code = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv_code = string.Empty;
    public static string year = string.Empty;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        Sf_Code = Request.QueryString["SfCode"].ToString();
        sfname = Request.QueryString["sfName"].ToString();
        subdiv_code = Request.QueryString["SubDiv"].ToString();
        year = Request.QueryString["yr"].ToString();
		
		hfdivcode.Value = Convert.ToString(divcode);
        hfSfCode.Value = Convert.ToString(Sf_Code);
        hfsfname.Value = Convert.ToString(sfname);
        hfsubdiv_code.Value = Convert.ToString(subdiv_code);
        hfyear.Value = Convert.ToString(year);

        if (Sf_Code == "" || Sf_Code == null)
        { Session["hfSfCode"] = "admin"; }
        else
        { Session["hfSfCode"] = Sf_Code.ToString(); }

        if (subdiv_code == "" || subdiv_code == null)
        { Session["hfsubdivcode"] = "0"; }
        else
        { Session["hfsubdivcode"] = subdiv_code.ToString(); }

        if (year == "" || year == null)
        { Session["hfyear"] = Convert.ToString(DateTime.Now.Year); }
        else
        { Session["hfyear"] = year.ToString(); }

       
    }
	
    [WebMethod(EnableSession = true)]
    public static string getRoute_Name()
    {
        Product dv = new Product();
        DataSet dsProd = dv.getroute_dtls(Sf_Code, divcode, subdivc);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
	
    [WebMethod(EnableSession = true)]
    public static string getOrderValue()
    {
        Product dv = new Product();
        string divcode = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsProd = dv.spCustomerOrders(Sf_Code, divcode, subdivc, year);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
	
    [WebMethod(EnableSession = true)]
    public static string getVisitDetails()
    {
        Product dv = new Product();
        string divcode = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsProd = dv.spCustomerVisitDetails(Sf_Code, divcode, subdivc, year);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
	
    [WebMethod(EnableSession = true)]
    public static string getRetailerData()
    {
        Product dv = new Product();
        string divcode = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsProd = dv.spCustomerDetails(Sf_Code, divcode, subdivc, year);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {

        DataTable dsProd1 = null;
        Product dv = new Product();
        string divcode = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsProd = dv.spCustomerDetails(Sf_Code, divcode, subdivc, year);
        dsProd1 = dsProd.Tables[0]; 
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dsProd1, "Customer Analysis Report");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Customer_Analysis.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
}