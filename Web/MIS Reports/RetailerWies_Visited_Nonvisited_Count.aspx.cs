using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data;
using DBase_EReport;

public partial class MIS_Reports_RetailerWies_Visited_Nonvisited_Count : System.Web.UI.Page
{
    #region "Declaration"
    static string div_code = string.Empty;
    static string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsTP = null;
    public static string sub_division = string.Empty;
    #endregion
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
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        sub_division = Session["sub_division"].ToString();
    }
    [WebMethod]
    public static string fillsubdivision()
    {
        DataSet dsSalesForce = null;
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code, sub_division);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            return JsonConvert.SerializeObject(dsSalesForce.Tables[0]);            
        }
        else
            return "No Sub-Division Avaliable";
    }
    [WebMethod]
    public static string fillFF()
    {
        string subdiv = "0";
        SalesForce sf = new SalesForce();
        DataSet dsSalesForce = null; DB_EReporting db_ER = new DB_EReporting();
        string Qry = "Exec HyrSFList_All '" + div_code + "','" + subdiv + "','" + sf_code + "'";
        try
        {
            dsSalesForce = db_ER.Exec_DataSet(Qry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(dsSalesForce.Tables[0]);        
    }
}