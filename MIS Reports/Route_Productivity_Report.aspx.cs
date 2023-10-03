using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;


public partial class MIS_Reports_Route_Productivity_Report : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    int imissed_dr = -1;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
    int mode = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
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
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            // menu1.FindControl("btnBack").Visible = false;
            fillsalesforce();
            fillsubdivision();
            FillYear();
            // Label1.Visible = false;
            //  DDL_Dist.Visible = false;
            Label2.Visible = false;
            salesforcelist.Visible = false;


            if (subdiv.Items.Count > 0)
            {
                subdiv.SelectedIndex = 1;
                subdiv_SelectedIndexChanged(sender, e);
            }

        }

    }
    private void fillsalesforce()
    {
        salesforcelist.DataSource = null;
        salesforcelist.Items.Clear();
        salesforcelist.Items.Insert(0, new ListItem("--Select--", "0"));
        SalesForce sd = new SalesForce();

        dsSalesForce = sd.feildforceelist(div_code,sub_division);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            salesforcelist.DataTextField = "Sf_Name";
            salesforcelist.DataValueField = "Sf_Code";
            salesforcelist.DataSource = dsSalesForce;
            salesforcelist.DataBind();
            salesforcelist.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    public void FillFO()
    {

        salesforcelist.DataSource = null;
        salesforcelist.Items.Clear();
        salesforcelist.Items.Insert(0, new ListItem("--Select--", "0"));

        SalesForce sf = new SalesForce();

        //   dsSalesForce = sf.feildforceelist_SF(div_code, subdiv.SelectedValue);
        if (sf_type == "3")
            dsSalesForce = sf.feildforceelist_SF(div_code, subdiv.SelectedValue);
        else
            dsSalesForce = sf.SalesForceList(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {

            salesforcelist.DataTextField = "Sf_Name";
            salesforcelist.DataValueField = "Sf_Code";
            salesforcelist.DataSource = dsSalesForce;
            salesforcelist.DataBind();
            salesforcelist.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        //        dsSalesForce = sd.Getsubdivisionwise(div_code);
        if (sf_type == "3")
            dsSalesForce = sd.Getsubdivisionwise(div_code);
        else
            dsSalesForce = sd.Getsubdivisionwise_sfcode(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));

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
                ddlTYear.Items.Add(k.ToString());
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }

        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
        ddlTMonth.SelectedValue = DateTime.Now.Month.ToString();
    }
   
    protected void btnGo_Click(object sender, EventArgs e)
    {
        int FYear = Convert.ToInt32(ddlFYear.SelectedValue);
        int TYear = Convert.ToInt32(ddlTYear.SelectedValue);
        int FMonth = Convert.ToInt32(ddlFMonth.SelectedValue);
        int TMonth = Convert.ToInt32(ddlTMonth.SelectedValue);
        if (FMonth > TMonth && TYear == FYear)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Month must be greater than From Month');</script>");
            ddlTMonth.Focus();
        }
        else if (FYear > TYear)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Year must be greater than From Year');</script>");
            ddlTYear.Focus();
        }
        else
        {
            if (FYear <= TYear)
            {
                string sURL = string.Empty;
                sURL = "Route_Productivity.aspx?&FMonth=" + ddlFMonth.SelectedValue.ToString() + "&FYear=" + ddlFYear.SelectedValue.ToString() + "&TMonth=" + ddlTMonth.SelectedValue.ToString() + "&TYear=" + ddlTYear.SelectedValue.ToString() + "&sfcode=" + salesforcelist.SelectedValue.ToString();
                string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "pop", newWin, true);
            }
        }
    }
    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {

        Label2.Visible = true;
        salesforcelist.Visible = true;
        if (subdiv.SelectedValue != "0")
        {
            FillFO();
        }
        else
        {
            salesforcelist.DataSource = null;
            salesforcelist.Items.Clear();
            salesforcelist.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }

}