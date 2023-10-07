using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Web.Mail;
using System.Web.Services;

public partial class MasterFiles_LeaveForm : System.Web.UI.Page
{
    #region "Declaration"
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsLeaveType = null;
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
        if (!Page.IsPostBack)
        {
            fillsubdivision();
            FillMRManagers("0");
            FillLeaveTypes();
            AvalaibleLeave("admin", "1999");
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


    private void FillLeaveTypes()
    {
        Holiday hod = new Holiday();
        dsLeaveType = hod.GetLeaveTypes(div_code);
        if (dsLeaveType.Tables[0].Rows.Count > 0)
        {
            ddlLeaveType.DataTextField = "Leave_Name";
            ddlLeaveType.DataValueField = "Leave_code";
            ddlLeaveType.DataSource = dsLeaveType;
            ddlLeaveType.DataBind();
            ddlLeaveType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            ddlLeaveType.DataSource = null;
            ddlLeaveType.DataBind();
            ddlLeaveType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    private void AvalaibleLeave(string SfCode, string FYear)
    {
        grdLeaves.DataSource = null;
        grdLeaves.DataBind();
        Holiday hd = new Holiday();
        dsSalesForce = hd.LeaveCheck(SfCode, FYear);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdLeaves.DataSource = dsSalesForce;
            grdLeaves.DataBind();
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
            ddlFieldForce.Items.Insert(0, new ListItem("---Select Manager ---", "0"));
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

    [WebMethod(EnableSession = true)]
    public static string saveLeave(string sfCode, string fdate, string tdate, string lCode, string reson, string lcount)
    {

        DateTime dtgrn = DateTime.ParseExact(fdate.ToString(), "dd/MM/yyyy", null);
        string FDate = dtgrn.ToString("yyyy-MM-dd");

        Holiday hod = new Holiday();
        dtgrn = DateTime.ParseExact(tdate.ToString(), "dd/MM/yyyy", null);
        string TDate = dtgrn.ToString("yyyy-MM-dd");

        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf = HttpContext.Current.Session["sf_code"].ToString();

        int iReturn = -1;
        string msg = "";
        try
        {

            iReturn = hod.AddLeavesFo(Div_code, sfCode, lCode, FDate, TDate, "", reson, lcount, sf);
            if (iReturn > 0)
            {
                msg = "Successfylly ";
            }
            else
            {
                msg = "Error";
            }
        }
        catch (Exception ex)
        {
            iReturn = -1;
            msg = "Error";
        }

        return msg;

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        //int iReturn = -1;

        //string sfcode = ddlFieldForce.SelectedValue.ToString();
        //string LType = ddlLeaveType.SelectedValue.ToString();
        //// string FDate = txtFrom.Text.ToString();
        //// string TDate = txtTo.Text.ToString();
        //string Reason = txtResaon.Text.ToString();
        //string lCount = Request.Form[txtCount.UniqueID]; // txtCount.Text.ToString();
        //Holiday hod = new Holiday();
        //string today = DateTime.Today.ToString("yyyy-MM-dd");



        //DateTime dtgrn = DateTime.ParseExact(txtFrom.Text.ToString(), "dd/MM/yyyy", null);
        //string FDate = dtgrn.ToString("yyyy-MM-dd");


        //dtgrn = DateTime.ParseExact(txtTo.Text.ToString(), "dd/MM/yyyy", null);
        //string TDate = dtgrn.ToString("yyyy-MM-dd");

        ////dtgrn = DateTime.ParseExact(DateTime.Today.ToString(), "dd/MM/yyyy", null);
        ////string today = dtgrn.ToString("MM-dd-yyyy");


        //try
        //{

        //    iReturn = hod.AddLeavesFo(div_code, sfcode, LType, txtFrom.Text.ToString(), txtTo.Text.ToString(), today, Reason, lCount);

        //    if (iReturn > 0)
        //    {

        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");


        //    }

        //    else if (iReturn == -2)
        //    {
        //        //menu1.Status = "Distributor already Exist!!";
        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exist');</script>");
        //    }
        //    AvalaibleLeave(sfcode, dtgrn.Year.ToString());
        //}
        //catch (Exception ex)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
        //}

    }

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlFieldForce.SelectedIndex > 0)
        {
            string sF = ddlFieldForce.SelectedValue;
            DateTime dTime = Convert.ToDateTime(txtFrom.Text);
            string fY = dTime.Year.ToString();
            AvalaibleLeave(sF, fY);
        }
        else
        {
            grdLeaves.DataSource = null;
            grdLeaves.DataBind();
        }

    }

    public class leaveCheck
    {
        public string flg { get; set; }
        public string msg { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static leaveCheck[] GetLeave_Check(string sfCode, string fYear, string tYear, string lCode)
    {

        Holiday nt = new Holiday();
        List<leaveCheck> FFD = new List<leaveCheck>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
         string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsAlowVal = null;

        DateTime d1 = DateTime.Parse(fYear);  //new DateTime(fYear);
        DateTime d2 = DateTime.Parse(tYear);



        dsAlowVal = nt.GetLvlValidate(sfCode, d1.ToString("yyyy-MM-dd"), d2.ToString("yyyy-MM-dd"), lCode, Sf_Code);
        foreach (DataRow row in dsAlowVal.Tables[0].Rows)
        {
            leaveCheck ffd = new leaveCheck();
            ffd.flg = row["Flg"].ToString();
            ffd.msg = row["Msg"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }
}