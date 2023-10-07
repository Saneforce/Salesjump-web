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

public partial class MasterFiles_secondary_order_edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
   

    [WebMethod]
    public static string Get_Product_unit(string divcode, string sfcode)
    {
        divcode = divcode.TrimEnd(',');
        secord sm = new secord();
        DataSet ds = sm.gets_Product_unit_details(divcode, sfcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string get_product_fedit(string divcode, string trans_slno,string sfcode)
    {
        DataSet ds = getDataSet("exec get_product_byorderno '" + trans_slno + "','" + divcode + "','"+ sfcode + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetOrders(string stk, string FDt, string TDt,string divcode,string sfcode)
    {
        DataSet ds = getDataSet("exec sec_order_detls '"+ stk + "','"+ FDt + "','"+ TDt + "','"+ divcode + "','"+ sfcode + "'");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string order_cancel(string orderno, string divcode)
    {
        secord sm = new secord();
        int ds = sm.cancelorder(orderno, divcode);
        if (ds > 0)
        {
            return "Success";
        }
        else
        {
            return "Fail";
        }
    }
    [WebMethod(EnableSession = true)]
    public static string Get_Distributor()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = getDataSet("select stockist_code,stockist_name from mas_stockist where Division_Code='"+ div_code+ "' and stockist_Active_flag=0 order by stockist_name");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
	[WebMethod(EnableSession = true)]
    public static string Get_Fieldforce()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = getDataSet("select Sf_Code,Sf_Name,Sf_Joining_Date,State_Code,SF_Status,sf_type,sf_emp_id from mas_salesforce where division_code='"+ div_code+", ' and sf_status=0 order by Sf_Name");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getDataSet(string qrystring)
    {

        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsSF = null;
        string strQry = qrystring;

        try
        {
            dsSF = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }
    public class secord
    {
        public DataSet gets_Product_unit_details(string Div_Code, string stk_code)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            string strQry = "Exec get_pro_unit '" + Div_Code + "','" + stk_code + "'";

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
        public int cancelorder(string orderno, string divcode)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                string strQry = "exec sp_cancel_secorder '" + orderno + "','" + divcode + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
    }
}