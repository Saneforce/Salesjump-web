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

public partial class MIS_Reports_POB_Report : System.Web.UI.Page
{
    string sf_type = string.Empty;
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

    }
    [WebMethod]
    public static string getStates(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet ds = SFD.getAllSF_States(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getDivision(string divcode)
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.Getsubdivisionwise(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
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
    public class bindyear
    {
        public string value { get; set; }
        public string text { get; set; }
    }
}