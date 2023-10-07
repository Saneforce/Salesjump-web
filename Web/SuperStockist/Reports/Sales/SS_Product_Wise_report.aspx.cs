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
using System.Transactions;
using DBase_EReport;
using System;

public partial class SuperStockist_Reports_Sales_SS_Product_Wise_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string Get_Report_Details(string FromDate, string ToDate)
    {
        string stk = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet ds = Get_Sales_invoice(FromDate, ToDate, stk);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet Get_Sales_invoice(string fdt, string tdt, string stk)
    {
        string strQry = "";
        DataSet dsAdmin = new DataSet();
        DB_EReporting dbER = new DB_EReporting();

        strQry = "select th.Dis_Code Stockist_Code,th.Dis_Name Stockist_Name,th.Division_Code Division_Code,th.Cus_Code ListedDrCode,th.Cus_Name ListedDr_Name,convert(varchar, th.Invoice_Date, 103) as Invoice_Date,th.Trans_Inv_Slno,th.Tax_Total tot_tax, th.Total,th.Dis_total as discount,th.Sub_Total as gross,td.Product_Code,td.Product_Name,td.Price,isnull(td.Discount,0) Discount,td.qty,td.Quantity,isnull(td.Amount,0) Amount,td.Unit,isnull(td.tax,0) Tax from Trans_Invoice_Head th with(nolock) inner join Trans_Invoice_Details td on th.Trans_Inv_Slno = td.Trans_Inv_Slno  where CONVERT(date, th.Invoice_Date) between '" + fdt + "' and '" + tdt + "' and th.Dis_Code = '" + stk + "'";
        try
        {
            dsAdmin = dbER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }
}