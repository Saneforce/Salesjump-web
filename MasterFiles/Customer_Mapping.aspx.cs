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
using DBase_EReport;

public partial class MasterFiles_Customer_Mapping : System.Web.UI.Page
{
	string sf_type = string.Empty;
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

    }
    [WebMethod(EnableSession=true)]
    public static string GetSStockist(string divcode)
    {
        DataSet sd = null;
        DSM st = new DSM();
        sd = st.SuppliergetSubDiv(divcode);
        return JsonConvert.SerializeObject(sd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string savecus(string data, string supcode, string Supname)
    {        
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        DataSet sd = null;
        DSM st = new DSM();
        sd = st.savcusmap(data, div_code, supcode, Supname);
        return "Success";
    }
    [WebMethod(EnableSession = true)]
    public static string GetSSdist(string divcode)
    {
        DataSet sd = null;
        StockistMaster st = new StockistMaster();
        sd = getSSDist(divcode);
        return JsonConvert.SerializeObject(sd.Tables[0]);
    }
    public static DataSet getSSDist(string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;
        string strQry = "Select Stockist_Code,Stockist_Name,ERP_Code,Dist_Name from mas_stockist where stockist_active_flag=0 and division_code='" + divcode + "' ";
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
    [WebMethod(EnableSession = true)]
    public static string Getmapdist(string divcode, string supcode)
    {
        DataSet sd = null;
        StockistMaster st = new StockistMaster();
        sd = st.getmpdist(divcode, supcode);
        return JsonConvert.SerializeObject(sd.Tables[0]);
    }


    [WebMethod(EnableSession = true)]
    public static string GetSSupplier(string divcode)
    {
        DataSet sd = null;
        DSM st = new DSM();
        sd = st.SuppliergetSubDiv(divcode);
        return JsonConvert.SerializeObject(sd.Tables[0]);
    }
}