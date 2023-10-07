using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Price_List_view : System.Web.UI.Page
{
    public static string Stockist_code = string.Empty;
    public static string Price_list_Code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Stockist_code = Request.QueryString["stockist_code"].ToString();      
        Price_list_Code = Request.QueryString["price_code"].ToString();

    }
    [WebMethod]
    public static string Get_Product_list(string PeriodSl_No)
    {

        DataTable dt = null;
        string strQry = string.Empty;
		
		strQry = "Exec sp_Get_Dist_Price_ratecard '" + Stockist_code + "','" + Price_list_Code + "','" + PeriodSl_No + "'";
	   //strQry = "select Product_Code,Product_Name,Stockist_Name,isnull(Rate,0) as retailer_price,isnull(dis_rate,0) as distributor_price,isnull(Unit,'') as UOM,* from Mas_Product_Wise_Bulk_rate_details mp inner join Mas_Stockist ms on ms.Price_List_Name = mp.Price_list_Sl_No and Stockist_Code = " + Stockist_code + " where Price_list_Sl_No = " + Price_list_Code +" and Unit != 'Select Unit'  order by mp.Product_Name";
         
        dt = execQuery(strQry);         
        return JsonConvert.SerializeObject(dt);
    }
    
    [WebMethod]
    public static string GetRatePeriods()
    {

        DataTable dt = null;
        string strQry = string.Empty;
		
		strQry = "Exec sp_GetRatePeriod '" + Price_list_Code + "'";
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
