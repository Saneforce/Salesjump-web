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

public partial class MasterFiles_SalesForce_New : System.Web.UI.Page
{
    public string sf_code = string.Empty;
    public static string chksfcode = string.Empty;
	string sf_type = string.Empty;
    public static string Div = string.Empty;
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
        sf_code = Request.QueryString["sfcode"];
        chksfcode = sf_code;
        Div = Session["div_code"].ToString();
    }

    [WebMethod]
    public static string getDepts(string divcode, string subdiv)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getAllSF_Dept(divcode, subdiv.TrimEnd(','));
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string getDesignation(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getAllSF_Designation(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string getDivision(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.Getsubdivisionwise(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string getStates(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getAllSF_States(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string getDivisions(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.Getsubdivisionwise(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    
    [WebMethod]
    public static string getDivisionsname(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getDivisname(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    [WebMethod]
    public static string getDistrict(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getAllSF_District(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    [WebMethod]
    public static string getTerritory(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getAllSF_Territory(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string GetHQDetails(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getAllSFHQ(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string GetReportingDetails(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.getSFType(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }
    
    [WebMethod]
    public static string getNewSfCode(string typ)
    {
        SalesForce SFD = new SalesForce();
        string dds = SFD.getsfcode_new(typ, Div);
        return dds;
    }
    
    [WebMethod]
    public static string getEmployeeFieldSettings(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataTable dds = SFD.getEmployeeAdditionalFields(divcode);
        return JsonConvert.SerializeObject(dds);
    }
    [WebMethod]
    public static string saveFieldForce(string data, string Itype)
    {
        string msg = string.Empty;
        Bus_EReport.SalesForce.SaveSalesForce Data = JsonConvert.DeserializeObject<Bus_EReport.SalesForce.SaveSalesForce>(data);
        SalesForce dss = new SalesForce();
        msg = dss.saveNewSalesForce(Data, Itype);
        return msg;
    }

    public class SalesforceData
    {
        public string Sfcode { get; set; }
        public string SfName { get; set; }
        public string SfEmail { get; set; }
        public string divcode { get; set; }
        public string sfusrname { get; set; }
        public string sfpwd { get; set; }
        public string sfempid { get; set; }
        public string sfdob { get; set; }
        public string sfdesg { get; set; }
        public string sfdept { get; set; }
        public string sfhq { get; set; }
        public string sfstate { get; set; }
        public string sfterr { get; set; }
        public string sfmobile { get; set; }
        public string sfaddr { get; set; }
        public string sfarea { get; set; }
        public string sfcity { get; set; }
        public string sfpincode { get; set; }
        public string sfstatus { get; set; }
        public string sftype { get; set; }
        public string sfjdate { get; set; }
		public string resndate { get; set; }
        public string sfreport { get; set; }
        public string sfpimg { get; set; }
        public string pname { get; set; }
        public string sflvl { get; set; }
        public string pval { get; set; }
        public string cname { get; set; }
        public string cval { get; set; }
        public string sfselsdiv { get; set; }
        public string sfdepot { get; set; }
        public string sfdistrict { get; set; }
    }

    [WebMethod]
    public static SalesforceData[] getSfDets(string sfcode)
    {
        string msg = string.Empty;
        SalesForce dss = new SalesForce();
        DataSet dsm = dss.getNewSFDetails(sfcode);
        List<SalesforceData> ad = new List<SalesforceData>();
        foreach (DataRow row in dsm.Tables[0].Rows)
        {
            SalesforceData asd = new SalesforceData();
            asd.Sfcode = row["Sf_Code"].ToString();
            asd.SfName = row["Sf_Name"].ToString();
            asd.sfusrname = row["Sf_UserName"].ToString();
            asd.sfpwd = row["Sf_Password"].ToString();
            asd.sfdob = row["SfDob"].ToString();
            asd.sfdesg = row["Designation_Code"].ToString();
            asd.sfdept = row["sf_desgn"].ToString();
            asd.sfstate = row["State_Code"].ToString();
            asd.sfterr = row["Territory_Code"].ToString();
            asd.sfaddr = row["SfAddr"].ToString();
            asd.sfarea = row["SfArea"].ToString();
            asd.sfcity = row["SfCity"].ToString();
            asd.SfEmail = row["SF_Email"].ToString();
            asd.sflvl = row["sf_type"].ToString();
            asd.sfmobile = row["SfMobile"].ToString();
            asd.sfpimg = row["SfProPic"].ToString();
            asd.sfreport = row["Reporting_To_SF"].ToString();
            asd.sftype = row["Approved_By"].ToString();
            asd.sfstatus = row["SF_Status"].ToString();
            asd.sfempid = row["sf_emp_id"].ToString();
            asd.sfhq = row["Sf_HQ"].ToString();
            asd.sfjdate = row["SfJdate"].ToString();
            asd.sfdistrict = row["sfdistrict"].ToString();
			 asd.resndate = row["resndate"].ToString();
            asd.sfdepot = row["sfdepot"].ToString();
            asd.sfselsdiv = row["subdivision_code"].ToString().TrimEnd(',');
            ad.Add(asd);
        }
        foreach (DataRow row in dsm.Tables[1].Rows)
        {
            SalesforceData asd = new SalesforceData();
            if (row["Field_Type"].ToString() == "profile")
            {
                asd.pname = row["Field_Name"].ToString();
                asd.pval = row["Field_Value"].ToString();
                ad.Add(asd);
            }
            if (row["Field_Type"].ToString() == "contact")
            {
                asd.cname = row["Field_Name"].ToString();
                asd.cval = row["Field_Value"].ToString();
                ad.Add(asd);
            }

        }
        return ad.ToArray();;
    }
}