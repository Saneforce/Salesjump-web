using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SuperStockist_Reports_Sales_SS_Rpt_Retailer_Payment_Pending : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string Getpending_summary(string stk, string Div, string splited_form_year, string splited_to_year, string From_month, string To_month)
    {
        DataSet ds = new DataSet();
        ds = getretailerpendingdetails_summary(stk, Div, splited_form_year, splited_to_year, From_month, To_month);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getretailerpendingdetails_summary(string stk, string Div, string splited_form_year, string splited_to_year, string From_month, string To_month)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        try
        {

            string strQry = "EXEC GetSS_pending_summary '" + stk + "','" + Div + "','" + splited_form_year + "','" + splited_to_year + "','" + From_month + "','" + To_month + "'";
            //string strQry = "EXEC Getpending_summary '" + stk + "','" + Div + "','" + splited_form_year + "','" + splited_to_year + "','" + From_month + "','" + To_month + "'";

            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    [WebMethod]
    public static string Getpending_details(string stk, string Div, string custcode, string splited_form_year, string splited_to_year, string From_month, string To_month)
    {
        DataSet ds = new DataSet();
        ds = Getpending_detail(stk, Div, custcode, splited_form_year, splited_to_year, From_month, To_month);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Getpending_detail(string stk, string Div, string custcode, string splited_form_year, string splited_to_year, string From_month, string To_month)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        try
        {
            string strQry = "EXEC Get_SS_pending_detail '" + custcode + "','" + stk + "','" + Div + "','" + splited_form_year + "','" + splited_to_year + "','" + From_month + "','" + To_month + "'";
            //string strQry = "EXEC Getpending_detail '" + custcode + "','" + stk + "','" + Div + "','" + splited_form_year + "','" + splited_to_year + "','" + From_month + "','" + To_month + "'";
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    [WebMethod]
    public static string Getorder_details(string stk, string Div, string invno)
    {
        DataSet ds = new DataSet();
        ds = Getorder_detail(stk, Div, invno);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Getorder_detail(string stk, string Div, string invno)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        try
        {
            string strQry = "select Order_No,CONVERT(VARCHAR(10),Order_Date,111) as Order_Date,Product_Name,Unit,Amount ,ISNULL(Quantity,qty)/ISNULL(Con_Fac,1) AS QTY from Trans_Invoice_Head TH " +
                            "INNER JOIN  Trans_Invoice_Details TD ON TH.Trans_Inv_Slno = TD.Trans_Inv_Slno WHERE TH.Trans_Inv_Slno = '" + invno + "' AND  Division_Code= '" + Div + "'";
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    [WebMethod(EnableSession = true)]
    public static string Get_Year_Data(string FM, string TM, string Type)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        ds = Get_year_details_Data(Div_Code, FM, TM, Type);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Get_year_details_Data(string Div_Code, string fm, string tm, string type)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = new DataSet();
        string strQry = "EXEC get_financial_year '" + Div_Code + "','" + type + "','" + fm + "','" + tm + "'";
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
}