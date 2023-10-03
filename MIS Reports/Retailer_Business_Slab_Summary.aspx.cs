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

public partial class MIS_Reports_Retailer_Business_Slab_Summary : System.Web.UI.Page
{
    public static DataTable ds = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    public static string GetDetails_Summary(string SF, string Div)
    {
        ListedDR SFD = new ListedDR();
        DataTable ds_summary = SFD.getRetailerBusinessSlab(Div.TrimEnd(','),SF);
        return JsonConvert.SerializeObject(ds_summary);
    }

    [WebMethod(EnableSession = true)]
    public static string GetRetailerDetails(string SF, string Div, string typ)
    {
        ListedDR SFD = new ListedDR();
        ds = SFD.getRetailerBusiness(Div.TrimEnd(','), SF, typ);
        return JsonConvert.SerializeObject(ds);
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(ds, "Retailer Business Details");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Retailer Business Summary Details.xlsx");
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