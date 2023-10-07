using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.Services;

public partial class MasterFiles_Leave_Admin_Approval : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsAdminSetup = null;
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string sf_type = string.Empty;
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
        sfCode = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {           
        }
    }

    public class LeaveData
    {
        public string Sf_Code { get; set; }
        public string Leave_Id { get; set; }
        public string FieldForceName { get; set; }
        public string Designation { get; set; }
        public string HQ { get; set; }
        public string EmpCode { get; set; }
        public string From_Date { get; set; }
        public string To_Date { get; set; }
        public string LeaveDays { get; set; }
        public string First_or_Second { get; set; }
        public string Reason { get; set; }


    }

    [WebMethod(EnableSession = true)]
    public static LeaveData[] getData()
    {
        List<LeaveData> LL = new List<LeaveData>();
        AdminSetup adm = new AdminSetup();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsPro = adm.getLeaveApproval(sf_code, Div_Code);
        foreach (DataRow row in dsPro.Tables[0].Rows)
        {
            LeaveData ll = new LeaveData();
            ll.Sf_Code = row["Sf_Code"].ToString();
            ll.Leave_Id = row["Leave_Id"].ToString();
            ll.FieldForceName = row["FieldForceName"].ToString();
            ll.Designation = row["Designation"].ToString();
            ll.HQ = row["HQ"].ToString();
            ll.EmpCode = row["EmpCode"].ToString();
            ll.From_Date = row["From_Date"].ToString();
            ll.To_Date = row["To_Date"].ToString();
            ll.LeaveDays = row["LeaveDays"].ToString();
	    ll.First_or_Second = row["First_or_Second"].ToString();
            ll.Reason = row["Reason"].ToString();
            LL.Add(ll);
        }
        return LL.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static string Approvaldata(string SF_Code, string LeaveCode)
    {
        string err = "";
        int iReturn = -1;

        try
        {
            Holiday hod = new Holiday();
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf = HttpContext.Current.Session["sf_code"].ToString();
            iReturn = hod.LeaveApprovalforMGR(div_code, sf, LeaveCode, "Appr", "");
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
    public static string RejectData(string SF_Code, string LeaveCode, string Reason)
    {
        string err = "";
        int iReturn = -1;
        Holiday hod = new Holiday();
        try
        {
        
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf = HttpContext.Current.Session["sf_code"].ToString();
            iReturn = hod.LeaveApprovalforMGR(div_code, sf, LeaveCode, "Rej", Reason);

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
}