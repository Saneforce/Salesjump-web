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

public partial class MasterFiles_Expense_Approval_SingleDay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    public static string GetSingle_Expense(string exp_years, string exp_month)
    {
        DB_EReporting db_ER = new DB_EReporting();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Result = "";
        DataSet dsSalesForce = null;
        string strQry = "Exec getExpenseDet_SingleDay '" + Sf_Code + "'," + exp_month + " ," + exp_years + ",'" + Div_code + "'";
        try
        {
            dsSalesForce = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        Result = JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
        return Result;
    }
    [WebMethod(EnableSession = true)]
    public static string SaveSingle_Expense(string SF,string exp_dt)
    {
        DB_EReporting db_ER = new DB_EReporting();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Result = "";
        int dsSalesForce = 0;
        string strQry = "Exec update_daily_expense_hyr '"+SF+"','" + exp_dt + "' ,'" + Sf_Code + "'";
        try
        {
            dsSalesForce = db_ER.ExecQry(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        if (dsSalesForce > 1)
            Result = "Updated";
        return Result;
    }    
    [WebMethod(EnableSession = true)]
    public static string rejectExpense(string SF, string rejdt, string Rtype,  string Remarks)
    {
        DB_EReporting db_ER = new DB_EReporting();

        string dsDivision = string.Empty;
        string strQry = "exec Reject_DailyExpense_Native '" + SF + "','" + rejdt + "'," + Rtype + ",'" + Remarks + "', " + 0 + "";
        try
        {
            dsDivision = db_ER.Exec_Scalar_s(strQry);
            dsDivision = "Expense Rejected";
        }
        catch (Exception ex)
        {
            dsDivision = ex.Message;
            throw ex;
        }
        return dsDivision;
    }
    [WebMethod(EnableSession = true)]
    public static string GetUserExpense(string SF, string date)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsDivision = null;
        string strQry = "exec GetDailyUserExpenseDet '" + SF + "','" + date + "'";
        try
        {
            dsDivision = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        string res = JsonConvert.SerializeObject(dsDivision.Tables[0]);
        return res;
    }
}