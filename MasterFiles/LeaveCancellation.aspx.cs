using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.Services;

public partial class MasterFiles_LeaveCancellation : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string sf_name = string.Empty;
    string MultiSf_Code = string.Empty;
    DateTime ServerStartTime;
    DataSet dsSalesForce = null;
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
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        sf_name = Session["sf_name"].ToString();
		sub_division = Session["sub_division"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            fillsubdivision();
            FillYear();
            FillMRManagers("0");
        }

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
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code,sub_division);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (subdiv.SelectedValue.ToString() != "0")
        {

            FillMRManagers(subdiv.SelectedValue.ToString());
        }
        else
        {
            FillMRManagers(subdiv.SelectedValue.ToString());
        }
    }
    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceList(div_code, sf_code, Sub_Div_Code);



        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("---Select Field Force---", "0"));

        }
        else
        {
            ddlFieldForce.DataSource = null;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("---Select Field Force---", "0"));
        }
    }
    public class sfLeaves
    {
        public string Leave_Id { get; set; }
        public string Sf_Code { get; set; }
        public string FieldForceName { get; set; }
        public string sf_Designation_Short_Name { get; set; }
        public string HQ { get; set; }
        public string EmpCode { get; set; }
        public string From_Date { get; set; }
        public string To_Date { get; set; }
        public string Leave_Name { get; set; }
        public string LeaveDays { get; set; }
        public string Reason { get; set; }


    }

    [WebMethod(EnableSession = true)]
    public static sfLeaves[] GetLeave(string SFCode, string FYear)
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }
        AdminSetup adm = new AdminSetup();
        DataSet dsSalesForce = adm.getLeave_Cancel(SFCode, FYear);
        List<sfLeaves> vList = new List<sfLeaves>();
        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            sfLeaves vl = new sfLeaves();
            vl.Leave_Id = row["Leave_Id"].ToString();
            vl.Sf_Code = row["Sf_Code"].ToString();
            vl.FieldForceName = row["FieldForceName"].ToString();
            vl.sf_Designation_Short_Name = row["sf_Designation_Short_Name"].ToString();
            vl.HQ = row["HQ"].ToString();
            vl.EmpCode = row["EmpCode"].ToString();
            vl.From_Date = row["From_Date"].ToString();
            vl.To_Date = row["To_Date"].ToString();
            vl.Leave_Name = row["Leave_Name"].ToString();
            vl.LeaveDays = row["LeaveDays"].ToString();
            vl.Reason = row["Reason"].ToString();
            vList.Add(vl);
        }
        return vList.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static string CancelApprLeave(string LeaveCode)
    {
        string err = "";
        int iReturn = -1;

        try
        {
            Holiday hod = new Holiday();
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf = HttpContext.Current.Session["sf_code"].ToString();
            iReturn = hod.LeaveApprovalCancel(LeaveCode);
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

}