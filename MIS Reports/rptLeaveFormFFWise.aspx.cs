using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_rptLeaveFormFFWise : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string fYear = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            sfCode = Request.QueryString["sfCode"].ToString();
            fYear = Request.QueryString["fYear"].ToString();

            lblhead.Text = "Leave Card for Apr-" + fYear + " to Mar-" + (Convert.ToInt32(fYear) + 1);
            hsfCode.Value = sfCode;
            hfYear.Value = fYear;
        }
    }
    public class FieldForce
    {
        public string sfName { get; set; }
        public string empCode { get; set; }
        public string dob { get; set; }
        public string doj { get; set; }
        public string hq { get; set; }
        public string desig { get; set; }
        public string status { get; set; }
        public string doc { get; set; }
        public string DivName { get; set; }
        public string statename { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static FieldForce[] GetFieldForce(string SF_Code)
    {
        SalesForce sf = new SalesForce();
        DataSet dsSalesForce = sf.GetSalFFoDetails(SF_Code);
        List<FieldForce> FFList = new List<FieldForce>();
        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            FieldForce fl = new FieldForce();
            fl.sfName = row["sf_name"].ToString();
            fl.empCode = row["Employee_Id"].ToString();
            fl.dob = row["DOB"].ToString();
            fl.doj = row["DOJ"].ToString();
            fl.hq = row["Sf_HQ"].ToString();
            fl.desig = row["Designation_Name"].ToString();
            fl.status = row["EmpType"].ToString();
            fl.doc = row["DOC"].ToString();
            fl.DivName = row["DivName"].ToString();
            fl.statename = row["StateName"].ToString();
            FFList.Add(fl);
        }
        return FFList.ToArray();
    }

    public class LeavesDt
    {
        public string SFCode { get; set; }
        public string LeaveCode { get; set; }
        public string LeaveValue { get; set; }
        public string LeaveAvailability { get; set; }
        public string LeaveTaken { get; set; }
        public string Leave_SName { get; set; }
        public string Leave_Name { get; set; }



    }

    [WebMethod(EnableSession = true)]
    public static LeavesDt[] GetleaveValues(string SF_Code, string FYear)
    {

        Holiday hd = new Holiday();
        DataSet dsSalesForce = hd.LeaveCheck(SF_Code, FYear);
        List<LeavesDt> FFList = new List<LeavesDt>();
        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            LeavesDt fl = new LeavesDt();
            fl.SFCode = row["SFCode"].ToString();
            fl.LeaveCode = row["LeaveCode"].ToString();
            fl.LeaveValue = row["LeaveValue"].ToString();
            fl.LeaveAvailability = row["LeaveAvailability"].ToString();
            fl.LeaveTaken = row["LeaveTaken"].ToString();
            fl.Leave_SName = row["Leave_SName"].ToString();
            fl.Leave_Name = row["Leave_Name"].ToString();
            FFList.Add(fl);
        }
        return FFList.ToArray();
    }

    public class LeavesData
    {
        public string SFCode { get; set; }
        public string LeaveCode { get; set; }
        public string Created_Date { get; set; }
        public string From_Date { get; set; }
        public string To_Date { get; set; }
        public string Reason { get; set; }
        public string No_of_Days { get; set; }
        public string LastUpdt_Date { get; set; }
        public string Sf_Name { get; set; }
        public string Remark { get; set; }
   



    }

    [WebMethod(EnableSession = true)]
    public static LeavesData[] GetleaveDetails(string SF_Code, string FYear)
    {

        AdminSetup hd = new AdminSetup();
        DataSet dsSalesForce = hd.getLeave_Details(SF_Code, FYear, (Convert.ToInt32(FYear) + 1).ToString());
        List<LeavesData> FFList = new List<LeavesData>();
        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            LeavesData fl = new LeavesData();
            fl.SFCode = row["sf_code"].ToString();           
            fl.LeaveCode = row["Leave_Type"].ToString();
            fl.Created_Date = row["Created_Date"].ToString();
            fl.From_Date = row["From_Date"].ToString();
            fl.To_Date = row["To_Date"].ToString();
            fl.Reason = row["Reason"].ToString();
            fl.No_of_Days = row["No_of_Days"].ToString();
            fl.LastUpdt_Date = row["LastUpdt_Date"].ToString();
            fl.Sf_Name = row["Sf_Name"].ToString();
            fl.Remark = row["Remark"].ToString();
            FFList.Add(fl);
        }
        return FFList.ToArray();
    }
}