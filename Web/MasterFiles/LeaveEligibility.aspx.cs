using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Data.SqlClient;
using Newtonsoft.Json;

public partial class MasterFiles_LeaveEligibility : System.Web.UI.Page
{ string div_code = string.Empty;
    DataSet dsTP = null;
	string sf_type = string.Empty;
	public static string sub_division = string.Empty;
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
	  sub_division = Session["sub_division"].ToString();
        if (!Page.IsPostBack)
        {
            FillYear();
        }
    }

    public class Designation_Details
    {
        public string Designation_Code { get; set; }
        public string Designation_Short_Name { get; set; }
        public string Designation_Name { get; set; }
        public string Name { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static Designation_Details[] Get_Details()
    {

        Notice nt = new Notice();
        List<Designation_Details> Alwd = new List<Designation_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
		string Sf_Type = HttpContext.Current.Session["Sf_Type"].ToString();
        DataSet dsDetails = null;

        if (Sf_Type == "2")
        { dsDetails = nt.getDesignationByUser(Div_code, Sf_Type, Sf_Code); }
        else
        { dsDetails = nt.getDesignation_div(Div_code); }
		        
        //dsDetails = nt.getDesignation_div(Div_code);

        foreach (DataRow row in dsDetails.Tables[0].Rows)
        {
            Designation_Details ald = new Designation_Details();
            ald.Designation_Code = row["Designation_Code"].ToString();
            ald.Designation_Name = row["Designation_Short_Name"].ToString();
            ald.Designation_Short_Name = row["Designation_Name"].ToString();
            ald.Name = row["Name"].ToString();
            Alwd.Add(ald);
        }
        return Alwd.ToArray();
    }

    public class FieldForce_Details
    {
        public string sf_code { get; set; }
        public string sf_name { get; set; }
        public string designation_name { get; set; }
        public string sf_HQ { get; set; }
        public string Designation_Code { get; set; }
        public string EmpCode { get; set; }
        public string JoinDT { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static FieldForce_Details[] Get_FieldForce()
    {

        Notice nt = new Notice();
        List<FieldForce_Details> FFD = new List<FieldForce_Details>();
		
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();		
        string Sf_Type = HttpContext.Current.Session["Sf_Type"].ToString();
        
        DataSet dsDetails = null;

        if (Sf_Type == "2")
        { dsDetails = nt.getSalesForceByRMFare(Div_code, Sf_Type, Sf_Code); }
        else
        { dsDetails = nt.getSalesForce_Fare(Div_code); }
			
        
        //dsDetails = nt.getSalesForce_Fare(Div_code);

        foreach (DataRow row in dsDetails.Tables[0].Rows)
        {
            FieldForce_Details ffd = new FieldForce_Details();
            ffd.sf_code = row["sf_code"].ToString();
            ffd.sf_name = row["sf_name"].ToString();
            ffd.designation_name = row["designation_name"].ToString();
            ffd.sf_HQ = row["sf_HQ"].ToString();
            ffd.Designation_Code = row["Designation_Code"].ToString();
            ffd.EmpCode = row["sf_emp_id"].ToString();
            ffd.JoinDT = row["JDT"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }


    public class LeaveType
    {
        public string LCode { get; set; }
        public string LName { get; set; }
        public string LSName { get; set; }

    }
	private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();

            }
        }

    

    }

    [WebMethod(EnableSession = true)]
    public static LeaveType[] GetLeaveType()
    {

        Holiday ho = new Holiday();
        List<LeaveType> lT = new List<LeaveType>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();

        DataSet dsAlowtype = null;
        dsAlowtype = ho.GetLeaveType(Div_code);
        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            LeaveType l = new LeaveType();
            l.LCode = row["Leave_code"].ToString();
            l.LName = row["Leave_Name"].ToString();
            l.LSName = row["Leave_SName"].ToString();
            lT.Add(l);
        }
        return lT.ToArray();
    }





    [WebMethod(EnableSession = true)]
    public static Leave_Data[] GetLeave_Values()
    {

        Holiday nt = new Holiday();
        List<Leave_Data> FFD = new List<Leave_Data>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();

        DataSet dsAlowVal = null;
        dsAlowVal = nt.get_Leave_Values(Div_code);

        foreach (DataRow row in dsAlowVal.Tables[0].Rows)
        {
            Leave_Data ffd = new Leave_Data();
            ffd.SF_Code = row["SFCode"].ToString();
            ffd.Des_code = row["DesgCode"].ToString();
            ffd.LeaveCode = row["LeaveCode"].ToString();
            ffd.LeaveValues = row["LeaveValue"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }


    public class Leave_Data
    {
        public string SF_Code { get; set; }
        public string LeaveValues { get; set; }
        public string Des_code { get; set; }
        public string LeaveCode { get; set; }
    }

    [WebMethod]
    public static string savedata(string data,string year)
    {
        MasterFiles_LeaveEligibility mle = new MasterFiles_LeaveEligibility();
        return mle.save(data,year);
    }
    private string save(string data,string year)
    {


        string sf_type = Session["sf_type"].ToString();
        string Division_code = "";
        if (sf_type == "3")
        {
            Division_code = Session["div_code"].ToString();
        }
        else
        {
            Division_code = Session["div_code"].ToString();
        }

        var items = JsonConvert.DeserializeObject<List<Leave_Data>>(data);
        int co = 0;
        Holiday nt = new Holiday();
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].LeaveValues != string.Empty)
            {
                int iReturn = nt.Insert_LeaveEligibility_Data(Division_code.TrimEnd(',').ToString(), items[i].SF_Code.ToString(), items[i].Des_code.ToString(), items[i].LeaveCode.ToString(), items[i].LeaveValues.ToString(),year);
                co++;
            }

        }
        if (co > 0)
        {
            return "Sucess";
        }
        else
        {
            return "Error";
        }
    }


}