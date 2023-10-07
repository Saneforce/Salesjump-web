using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Globalization;
using Newtonsoft.Json;
using ClosedXML.Excel;
using System.IO;
using DBase_EReport;

public partial class SuperStockist_Reports_Sales_SS_Rpt_Sales_By_Item_Detail : System.Web.UI.Page
{
    public static string P_Code = string.Empty;
    public static string Fdt = string.Empty;
    public static string Tdt = string.Empty;
    public static string Pro_Name = string.Empty;
    public static DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        P_Code = Request.QueryString["Pro_Code"].ToString();
        Fdt = Request.QueryString["FDate"].ToString();
        Tdt = Request.QueryString["TDate"].ToString();
        Pro_Name = Request.QueryString["Pro_Name"].ToString();


        DateTime dt = Convert.ToDateTime(Fdt);
        string fd = dt.ToString("dd/MM/yyyy");

        DateTime dt1 = Convert.ToDateTime(Tdt);
        string td = dt1.ToString("dd/MM/yyyy");


        date_details.Text = " From " + fd + " To " + td;
        Tit.Text = "Sales by Item -" + Pro_Name;


    }
    [WebMethod(EnableSession = true)]
    public static string Get_salebyitem_details()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string sf_type = HttpContext.Current.Session["sf_type"].ToString();
        //Stockist_Sales ss = new Stockist_Sales();
        //ds = ss.Bind_salebyitem_details(Stockist_Code, Fdt, Tdt, Div_Code, P_Code, sf_type);
        ds = Bind_salebyitem_details(Stockist_Code, Fdt, Tdt, Div_Code, P_Code, sf_type);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Bind_salebyitem_details(string Stockist_Code, string FDT, string TDT, string Div_Code, string Pro_Code, string sf_type)
    {
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "EXEC Bind_SS_salebyItem_Details '" + FDT + "','" + TDT + "','" + Div_Code + "','" + Stockist_Code + "','" + Pro_Code + "','" + sf_type + "'";
        //string strQry = "EXEC Bind_salebyItem_Details '" + FDT + "','" + TDT + "','" + Div_Code + "','" + Stockist_Code + "','" + Pro_Code + "','" + sf_type + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(ds.Tables[0], "Sales By Item Details");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.AddHeader("content-disposition", "attachment;filename=Permission Status Report" + fdt + "_to_" + tdt + ".xlsx");
            Response.AddHeader("content-disposition", "attachment;filename=Sales By Item Details.xlsx");
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