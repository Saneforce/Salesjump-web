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
using Newtonsoft.Json;
using System.Globalization;
using ClosedXML.Excel;
using System.IO;
using DBase_EReport;
using System.Net;

public partial class Stockist_Purchase_Order_List : System.Web.UI.Page
{

    string sf_type = string.Empty;
	 public static DataTable ds = new DataTable();
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
        else if (sf_type == "4")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string div_code = Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
    }

    [WebMethod(EnableSession = true)]
    public static string GetOrders(string Stockist_Code, string FDT, string TDT)
    {
        string StkCode = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();      
        ds = getallorderdetails("exec sp_getpriorderlist '" + Stockist_Code + "','" + Div_Code + "','" +FDT+ "','" +TDT+ "'");
        return JsonConvert.SerializeObject(ds);
    }
	
	public static DataTable getallorderdetails(string qrystring)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataTable dsSF = null;
        string strQry = qrystring;
        try
        {
            dsSF = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }
		
	 protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(ds, "Purchase Order Report");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Purchase Order.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }

    [WebMethod(EnableSession = true)]
    public static int cancelorder(string orderid, string stockist, string div,string cancelreason)
    {
        int iReturn = -1;
        string strQry;
        string IPAddress = GetLocalIPAddress();
        try
        {
            DB_EReporting db = new DB_EReporting();
            strQry = "exec Pri_Cancel_tran_order '" + orderid + "','" + stockist + "','" + div + "','"+ IPAddress + "','"+ cancelreason + "'";
            iReturn = db.ExecQry(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return iReturn;
    }
    public static string GetLocalIPAddress()
    {
        string hostName = Dns.GetHostName();
        IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
        IPAddress[] addresses = ipEntry.AddressList;

        foreach (IPAddress address in addresses)
        {
            if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return address.ToString();
            }
        }

        return "No local IP address found.";
    }
}