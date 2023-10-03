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
using System.Transactions;
using DBase_EReport;

public partial class Stockist_Purchase_PurchaseOrder_ByProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string Get_Primary_Details(string FromDate, string ToDate)
    {
        string stk = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet ds = Get_PrimaryReport_Details(FromDate, ToDate, stk);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet Get_PrimaryReport_Details(string fdt, string tdt, string stk)
    {
        string strQry = "";
        DataSet dsAdmin = new DataSet();
        DB_EReporting dbER = new DB_EReporting();

        strQry = "	select h.Trans_Sl_No,h.Stockist_Code,h.ERPCode,h.CustomerName,h.Sup_Name,h.company_add,convert(varchar,h.Order_Date, 103) as Order_Date,h.Order_Value,h.Division_Code,d.Product_Code,d.Product_Name,mu.Move_MailFolder_Name Unit,d.CQty,d.Rate,(d.CQty * d.Rate) gross,d.Cl_bal,d.tax,d.value,d.discount from Trans_PriOrder_Head h inner join Trans_PriOrder_Details d on h.Trans_Sl_No = d.Trans_Sl_No inner join Mas_Multi_Unit_Entry mu on cast(mu.Move_MailFolder_Id as varchar)=d.Product_Unit_Name where CONVERT(date, h.Order_Date) between '" + fdt + "' and '" + tdt + "' and h.Stockist_Code = '" + stk + "'";
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