using Bus_EReport;
using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;

public partial class MasterFiles_Reports_TP_Deviation_Approval : System.Web.UI.Page
{
    public static DataTable ds = new DataTable();
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


    [WebMethod]
    public static string GetList(string divcode, string SF, string Mn, string Yr)
    {
        DCR sf = new DCR();
        ds = sf.getDataTable("exec getTPDeviationDates '" + divcode + "','" + SF + "','" + Mn + "','" + Yr + "'");
        return JsonConvert.SerializeObject(ds);
    }


    [WebMethod]
    public static string GetListNew(string divcode, string SF, string Fdt, string Tdt)
    {
        DCR sf = new DCR();
        ds = sf.getDataTable("exec getTPDeviationDates_New '" + divcode + "','" + SF + "','" + Fdt + "','" + Tdt + "'");
        return JsonConvert.SerializeObject(ds);
    }


    [WebMethod]
    public static string FillMRManagers(string div_code, string sf_code)
    {
        SalesForce sf = new SalesForce();
        DataSet dsSalesForce = sf.SalesForceList(div_code, sf_code);
        return JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
    }
    public class bindyear
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static bindyear[] BindDate(string divcode)
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();
        dsTP = tp.Get_TP_Edit_Year(divcode);
        List<bindyear> sf = new List<bindyear>();
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                bindyear rt = new bindyear();
                rt.value = k.ToString();
                rt.text = k.ToString();
                sf.Add(rt);
            }
        }
        return sf.ToArray();
    }
    [WebMethod]
    public static string saveApproval(string sXML)
    {
        DCR sf = new DCR();
        int iReturn = -1;
        iReturn = sf.ApproveDeviation("exec ApproveTPDeviation '" + sXML + "'");
        if (iReturn > 0)
        {
            return "Deviation Dates Approved";
        }
        else
        {
            return "Failure";
        }
    }
    [WebMethod]
    public static string RejectApproval(string sXML)
    {
        DCR sf = new DCR();
        int iReturn = -1;
        //iReturn = sf.ApproveDeviation("exec RejectTPDeviation '" + sXML + "'");
        DB_EReporting db_ER = new DB_EReporting();        
        string strQry = "exec RejectTPDeviation '" + sXML + "'";
        try
        {
            iReturn = db_ER.ExecQry(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        if (iReturn > 0)
        {
            return "Deviation Dates Rejected";
        }
        else
        {
            return "Failure";
        }
    }
}