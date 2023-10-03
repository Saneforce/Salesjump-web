using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SuperStockist_Reports_Sales_SS_Sales_by_Invoice : System.Web.UI.Page
{
    public static string sf_code = string.Empty;
    string sf_type = string.Empty;
    string div_code = string.Empty;

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
        else if (sf_type == "5")
        {
            this.MasterPageFile = "~/Master_SS.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        div_code = Session["div_code"].ToString();
    }
    [WebMethod]
    public static string getRetailer(string Fdt, string Tdt)
    {
        string strQry = string.Empty;
        DataTable dt = null;

        strQry = "select ml.Stockist_Code as ListedDrCode,ml.Stockist_Name as ListedDr_Name from trans_invoice_head th inner join Mas_Stockist ml on ml.Stockist_Code = th.Cus_Code where th.Dis_Code = '" + sf_code + "' and CONVERT(date, th.Invoice_Date) between '" + Fdt + "' and '" + Tdt + "' group by ml.Stockist_Code,ml.Stockist_Name order by ml.Stockist_Name ";

        dt = execQuery(strQry);
        return JsonConvert.SerializeObject(dt);
    }
    public static DataTable execQuery(string strQry)
    {
        DataTable dt = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        try
        {
            dt = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dt;
    }
}