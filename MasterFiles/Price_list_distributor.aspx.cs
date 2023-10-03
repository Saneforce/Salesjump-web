using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Price_list_distributor : System.Web.UI.Page
{
    public static string Price_list_Code = string.Empty;
    public static string Price_list_Name= string.Empty;
    public static string cttype = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Price_list_Code = Request.QueryString["price_code"].ToString();
         Price_list_Name = Request.QueryString["price_name"].ToString();
        cttype = Request.QueryString["cttype"].ToString();
        ratelbl.Text = Price_list_Name;

    }
    [WebMethod]
    public static string Get_Product_list()
    {

        DataTable dt = null;
        string strQry = string.Empty;
		if(cttype=="dist")
		strQry = "select Stockist_Code, Stockist_Name,'DIST' AS CTYPE from mas_stockist where Stockist_Active_Flag = 0 and (Price_List_Name = cast(" + Price_list_Code + " as varchar) or special_price_list = cast(" + Price_list_Code + " as varchar) ) order by Stockist_Name ";
        else
         strQry = "select S_No, S_Name,'SUPSTK' AS CTYPE from Supplier_Master where Act_flg = 0 and Price_list_Sl_No = cast(" + Price_list_Code + " as varchar) order by S_Name ";
        //strQry = "select Stockist_Code,Stockist_Name from mas_stockist where Price_List_Name ="+ Price_list_Code + "  or special_price_list="+ Price_list_Code + "  and Stockist_Active_Flag=0 order by Stockist_Name ";


        dt = execQuery(strQry);

        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static string GetRatePeriods()
    {

        DataTable dt = null;
        string strQry = string.Empty;

        strQry = "Exec sp_GetRatePeriod '" + Price_list_Code + "'";
        //strQry = "select Period_Sl_No,convert(varchar,Effective_From_Date,103)Effective_From_Date,convert(varchar,Effective_To_Date,103)Effective_To_Date from Mas_Product_Wise_Bulk_rate_Period where Price_list_Sl_No = " + Price_list_Code + " and (convert(varchar,getdate(),23) < Effective_From_Date or convert(varchar,getdate(),23)<Effective_To_Date)";


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
