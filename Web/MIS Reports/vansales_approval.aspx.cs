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
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using DBase_EReport;

public partial class MIS_Reports_vansales_approval : System.Web.UI.Page
{
    public string sf_type = string.Empty;
    public static string div_code = string.Empty;
    public static string sf_code = string.Empty;
    public static string sub_division = string.Empty;
    protected void Page_PreInit(object sender, EventArgs e)
    {
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
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString().TrimEnd(',');
        sub_division = Session["sub_division"].ToString();
    }
    [WebMethod]
    public static string getDivision(string divcode)
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.Getsubdivisionwise(divcode, sub_division);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getFieldForce(string divcode, string sfcode)
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.SalesForceList(divcode, sfcode, sub_division);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod] 
    public static string getvanprod(string divcode, string ffc, string fdt, string tdt)
    {
        newvn sd = new newvn();
        DataSet ds = new DataSet();
        ds = sd.get_vanprod(divcode, ffc, fdt, tdt, "0");
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getcasepc(string ffc, string fdt)
    {
        newvn sd = new newvn();
        DataSet ds = new DataSet();
        ds = sd.get_casepc(ffc, fdt);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Approvaldata(string SF_Code, string dates)
    {
        string err = "";
        int iReturn = -1;

        try
        {
            newvn hod = new newvn();
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf = HttpContext.Current.Session["sf_code"].ToString();
            iReturn = hod.vansale_Approval(SF_Code, dates,"1");
            if (iReturn > 0)
            {
                err = "Sucess";
            }

        }
        catch (Exception ex)
        {
            err = "Error";
        }
        return err;
    }
    [WebMethod(EnableSession = true)]
    public static string RejectData(string SF_Code, string dates)
    {
        string err = "";
        int iReturn = -1;
        newvn hod = new newvn();
        try
        {

            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf = HttpContext.Current.Session["sf_code"].ToString();
            iReturn = hod.vansale_Approval(SF_Code, dates, "0");

            if (iReturn > 0)
            {
                err = "Sucess";
            }
            else
            {
                err = "Sucess";
            }

        }
        catch (Exception ex)
        {
            err = "Error";
        }
        return err;
    }
    public class proddtl
    {
        public string pCodes { get; set; }
        public string cqtys { get; set; }
        public string pqtys { get; set; }
    }
        public class newvn
    {
        public DataSet get_vanprod(string divcode, string ffc, string fdt, string tdt, string sub="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            string strQry = "EXEC van_stock_ledger '" + ffc + "','" + divcode + "','" + fdt + "','" + tdt + "'," + sub + "";

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
        public DataSet get_casepc(string ffc, string fdt)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            string strQry = "EXEC vs_getDailyBeginInv '" + ffc + "','" + fdt + "'";

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
        public int vansale_Approval(string SF_Code, string dates, string Mode)
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                DataSet dsDcrMAin = null;
                string strQry = "EXEC vansales_aprv_reject '" + div_code + "','" + SF_Code + "','" + dates + "','" + Mode + "'";
                try
                {
                    dsDcrMAin = db.Exec_DataSet(strQry);

                    if (dsDcrMAin.Tables[0].Rows.Count > 0)
                    {
                        iReturn = 1;
                    }
                    else
                    {
                        iReturn = 0;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
    }
}