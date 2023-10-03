 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data;
using Bus_EReport;
using DBase_EReport;

public partial class MasterFiles_Reports_Default : System.Web.UI.Page
{
    static string div_code;
    public static string sf_code;
    public string sf_type = string.Empty;
    static DataSet dsHry = null;
    static DataSet dsGetData = null;

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
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        try
        {
            div_code = Session["div_code"].ToString();
        }
        catch
        {
            div_code = Session["Division_Code"].ToString();
        }
    }
    [WebMethod]
    public static string GetBrandName()
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();
        string Qry = "";
        Qry = "select  (PB.Product_Brd_Name) BrandName,PB.Product_Brd_Code ProductBrndCode,subdivision_code from Mas_Product_Detail PD " +
            "inner join Mas_Product_Brand PB with(nolock) on PB.Product_Brd_Code = PD.Product_Brd_Code" +
            " Where PB.Division_Code = '" + div_code + "' AND Product_Brd_Active_Flag = 0" +
            "group by(PB.Product_Brd_Name),PB.Product_Brd_Code,subdivision_code order by PB.Product_Brd_Code";
        try
        {
            ds = db_ER.Exec_DataSet(Qry);
        }
        catch
        {

        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetAllBrandName()
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();
        string Qry = "";
        Qry = "select  (PB.Product_Brd_Name) BrandName,PB.Product_Brd_Code ProductBrndCode from Mas_Product_Brand PB with(nolock) " +
            " Where PB.Division_Code = '" + div_code + "' AND Product_Brd_Active_Flag = 0 " +
            "group by(PB.Product_Brd_Name),PB.Product_Brd_Code order by PB.Product_Brd_Code";
        try
        {
            ds = db_ER.Exec_DataSet(Qry);
        }
        catch
        {

        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string Gethyr()
    {

        DB_EReporting db_ER = new DB_EReporting();
        string Qry = "";
        Qry = "Exec HyrSFList_All '" + div_code + "','0','" + sf_code + "'";
        try
        {
            dsHry = db_ER.Exec_DataSet(Qry);
        }
        catch
        {

        }
        return JsonConvert.SerializeObject(dsHry.Tables[0]);
    }
    [WebMethod]
    public static string GetProduct_Wise_Qty(string FDt, string TDt)
    {
        DataSet ds = null;
        ds = GetProductWise_Qty(FDt, TDt, div_code, sf_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet GetProductWise_Qty(string Fdt, string Tdt, string div_code, string Sf_Code)
    {
        string Qry = "";
        DB_EReporting db_ER = new DB_EReporting();
        Qry = "Exec GetProduct_Wise_Qty '" + Fdt + "','" + Tdt + "','" + div_code + "','" + Sf_Code + "'";
        try
        {
            dsGetData = db_ER.Exec_DataSet(Qry);

        }
        catch
        {

        }
        return dsGetData;
    }
    [WebMethod]
    public static string getStates(string divcode)
    {
        Territory SFD = new Territory();
        DataSet ds = getRo_States(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getRo_States(string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        string strQry = "select st.State_Code,StateName from mas_state st inner join Mas_Division md on charindex(','+cast(st.State_Code as varchar)+',',','+md.State_Code+',')>0 where Division_Code=" + divcode + " order by StateName";

        try
        {
            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }
    [WebMethod]
    public static string fillsubdivision()
    {
        string Sub_Division = ""; string qry = "";
        DataSet dsSalesForce = null;
        SalesForce sd = new SalesForce();
        DB_EReporting db_ER = new DB_EReporting();
        qry = "select* from mas_subdivision where Div_Code = '" + div_code + "' and SubDivision_Active_Flag = 0 and (subdivision_code) > 0";
        try
        {
            dsSalesForce = db_ER.Exec_DataSet(qry);
        }
        catch
        {

        }
        return JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
    }
    [WebMethod]
    public static string getsubDiv(string terry_code)
    {
        DataSet dsSalesForce = null;
        DB_EReporting db_ER = new DB_EReporting();
        string qrysf = "select sf.subdivision_code from Mas_State ms inner join Mas_Territory mt on ms.State_Code = mt.State_Code " +
            " inner join mas_salesforce sf on sf.Territory_Code = mt.Territory_code Where mt.Territory_code = " + terry_code + " Group by subdivision_code";
        try
        {
            dsSalesForce = db_ER.Exec_DataSet(qrysf);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
    }
    [WebMethod]
    public static string getsfby_div(string SubDiv_Code)
    {
        DataSet dsSalesForce = null;
        DB_EReporting db_ER = new DB_EReporting();
        string qrysf = "select sf_name,sf_code from mas_salesforce where subdivision_code='" + SubDiv_Code + "'";
        try
        {
            dsSalesForce = db_ER.Exec_DataSet(qrysf);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
    }
    [WebMethod]
    public static string Getcluster(string stateCode)
    {
        DataSet ds = null;
        string qry = "select Territory_name, Territory_code from Mas_Territory mt inner join mas_state ms on mt.state_code = ms.state_code where mt.State_Code = '" + stateCode + "' and Territory_Active_Flag = 0";
        DB_EReporting db_ER = new DB_EReporting();
        try
        {
            ds = db_ER.Exec_DataSet(qry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string GetMGR()
    {
        DB_EReporting db_ER = new DB_EReporting();
        string Sub_Div_Code = "0";
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
        return JsonConvert.SerializeObject(dsSF.Tables[0]);
    }

    [WebMethod]
    public static string GetMGR_MR(string SF)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsSF = null;
        string strQry = "EXEC getHyrSFList_MGR '" + div_code + "', '" + SF + "'";
        try
        {
            dsSF = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dsSF.Tables[0]);
    }
}