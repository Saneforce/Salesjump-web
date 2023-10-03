using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Services;
using Bus_EReport;
using System.Data;
using DBase_EReport;

public partial class MIS_Reports_CustomerWise_analysis : System.Web.UI.Page
{
    string sf_type = string.Empty;
    static string div_code = string.Empty;
    static string sf_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
    }

    [WebMethod]
    public static string GetDivision()
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        string result = "";
        ds = sd.Getsubdivisionwise(div_code);
        result = JsonConvert.SerializeObject(ds.Tables[0]);
        ds.Dispose();
        return result;
    }
    [WebMethod]
    public static string GetMgr()
    {
        DB_EReporting db_ER = new DB_EReporting();
        string Sub_Div_Code = "0";
        string result = "";
        DataSet dsSF = null;
        string strQry = "EXEC getHyrSFList_MGR '" + div_code + "', '" + sf_code + "', '" + Sub_Div_Code + "' ";
        try
        {
            dsSF = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        result = JsonConvert.SerializeObject(dsSF.Tables[0]);
        dsSF.Dispose();
        return result;
    }
    [WebMethod]
    public static string GetCat()
    {
        DB_EReporting db_ER = new DB_EReporting();
        string result = "";
        DataSet dsSF = null;
        string strQry = "select Product_Cat_Code,Product_Cat_Name,Product_Cat_SName from Mas_Product_Category where Division_Code=" + div_code + " and Product_Cat_Active_Flag=0 group by Product_Cat_Code,Product_Cat_Name,Product_Cat_SName";
        try
        {
            dsSF = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        result = JsonConvert.SerializeObject(dsSF.Tables[0]);
        dsSF.Dispose();
        return result;
    }
}