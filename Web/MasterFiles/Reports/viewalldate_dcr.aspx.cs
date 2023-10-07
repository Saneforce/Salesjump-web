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
using System.Data.OleDb;
using DBase_EReport;

public partial class MasterFiles_Reports_viewalldate_dcr : System.Web.UI.Page
{
    public static string sf_type = string.Empty;
    public static string sfcode = string.Empty;
    public static string sfname = string.Empty;
    public static string subdiv = string.Empty;
    public static string FDTs = string.Empty;
    public static string TDTs = string.Empty;
    public static string FDT = string.Empty;
    public static string TDT = string.Empty;
    public static string BrandCode = string.Empty;
    public string fdate = string.Empty;
    public string tdate = string.Empty;
    public DateTime rdt;
    public DateTime sdt;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfcode = Request.QueryString["sf_code"].ToString();
        sfname = Request.QueryString["Sf_Name"].ToString();
        //BrandCode = Request.QueryString["state"].ToString();
        FDTs = Request.QueryString["fdate"].ToString();
        TDTs = Request.QueryString["tdate"].ToString();
        //subdiv = Request.QueryString["subdiv"].ToString();

        DateTime result4 = DateTime.ParseExact(FDTs, "d/MM/yyyy", CultureInfo.InvariantCulture);
        FDT = result4.ToString("yyyy-MM-dd");

        DateTime result10 = DateTime.ParseExact(TDTs, "d/MM/yyyy", CultureInfo.InvariantCulture);
        TDT = result10.ToString("yyyy-MM-dd");
        rdt = Convert.ToDateTime(FDTs);
        sdt = Convert.ToDateTime(TDTs);

        Label1.Text = "Daily Call Report from " + rdt.ToString("dd/MM/yy") + " to " + sdt.ToString("dd/MM/yy");
        Label2.Text = "FieldForce Name: " + sfname;
    }
    [WebMethod]
    public static string getBrandwiseSales(string Div)
    {
        
        DataSet ds =dcrgetAllBrd_Qty(Div, sfcode, FDT, TDT, subdiv, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getBrandwiseSalesUsr(string Div)
    {
        
        DataSet ds = dcrgetAllBrd_USR(Div, sfcode, FDT, TDT, subdiv, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getProductBrandlist(string Div)
    {
        
        DataSet ds = dcrgetProductBrandlist_DataTable(Div, sfcode, FDT, TDT, subdiv, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string primgetBrandwiseSales(string Div)
    {
        
        DataSet ds = pridcrgetAllBrd_Qty(Div, sfcode, FDT, TDT, subdiv, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string primgetBrandwiseSalesUsr(string Div)
    {
        
        DataSet ds = pridcrgetAllBrd_USR(Div, sfcode, FDT, TDT, subdiv, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string primgetProductBrandlist(string Div)
    {
       
        DataSet ds =primdcrgetProductBrandlist_DataTable(Div, sfcode, FDT, TDT, subdiv, BrandCode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string getRemarksSF(string Div)
    {
        DataSet ds = null;
        string strQry = string.Empty;

        ds = getAllSFRemark(Div, sfcode, FDT, TDT, subdiv, BrandCode);

        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getAllSFRemark(string divcode, string sfcode, string FDT, string TDT, string subdiv = "0", string StateCode = "0")
    {
        string strQry = string.Empty;
        DataSet dsRemark = null;

        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "exec getRemarksAllDCRdtl_mangr'" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

        try
        {
            dsRemark = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsRemark;
    }
    public static  DataSet dcrgetAllBrd_Qty(string divcode, string sfcode, string FDT, string TDT, string subdiv = "0", string StateCode = "0")
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsState = null;
        string strQry = "exec getAllDCrdate_mgr '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    public static DataSet dcrgetAllBrd_USR(string divcode, string sfcode, string FDT, string TDT, string subdiv = "0", string StateCode = "0")
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsState = null;
        string strQry = "exec getAllDCRdatepro_mgr  '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    public static DataSet dcrgetProductBrandlist_DataTable(string divcode, string sfcode, string FDT, string TDT, string subdiv = "0", string StateCode = "0")
    {
        DB_EReporting db_ER = new DB_EReporting();//no change

        DataSet dsState = null;
        string strQry = "exec getSecAllDCR_PRODUCT  '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    public static DataSet pridcrgetAllBrd_Qty(string divcode, string sfcode, string FDT, string TDT, string subdiv = "0", string StateCode = "0")
    {
        DB_EReporting db_ER = new DB_EReporting();//no change

        DataSet dsState = null;
        string strQry = "exec getprimAllDCRdtl_mgrr '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    public static DataSet pridcrgetAllBrd_USR(string divcode, string sfcode, string FDT, string TDT, string subdiv = "0", string StateCode = "0")
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsState = null;
        string strQry = "exec getpriAllDCRdtl_mmgr  '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    public static DataSet primdcrgetProductBrandlist_DataTable(string divcode, string sfcode, string FDT, string TDT, string subdiv = "0", string StateCode = "0")
    {
        DB_EReporting db_ER = new DB_EReporting();//no change

        DataSet dsState = null;
        string strQry = "exec getprimAllDCR_PRODUCT  '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + subdiv + "','" + StateCode + "'";

        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    [WebMethod]
    public static string getdisthuntdet(string Div)
    {
        DataSet ds = null;
        string strQry = string.Empty;

        ds = getdisthuntdetail(Div, sfcode, FDT, TDT, BrandCode);

        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getdisthuntdetail(string divcode, string sfcode, string FDT, string TDT, string StateCode = "0")
    {
        string strQry = string.Empty;
        DataSet dsRemark = null;

        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "exec Distributor_huntmgr '" + sfcode + "','" + divcode + "','" + FDT + "','" + TDT + "','" + StateCode + "'";

        try
        {
            dsRemark = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsRemark;
    }
}