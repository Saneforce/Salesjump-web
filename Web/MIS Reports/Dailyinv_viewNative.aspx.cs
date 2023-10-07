using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_Dailyinv_viewNative : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsdiv = new DataSet();
    DataSet dsTP = null;
    DataSet dsDivision = null;
    string strMultiDiv = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
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
        if (sf_type == "4")
        {
            div_code = Session["Division_Code"].ToString().Replace(",", "");

            lblFF.Visible = false;
            ddlFFType.Visible = false;
            ddlFieldForce.Visible = false;

        }
        else
        {
            div_code = Session["div_code"].ToString();
            sf_code = Session["sf_code"].ToString();
            sf_type = Session["sf_type"].ToString();
        }
        if (Session["sf_type"].ToString() == "1")
        {
            ViewState["sf_type"] = "";
            SalesForce sf = new SalesForce();
            dsSf = sf.getReportingTo(sf_code);
            if (dsSf.Tables[0].Rows.Count > 0)
            {
                sf_code = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            if (!Page.IsPostBack)
            {
                fillsubdivision();
                FillMRManagers("0");
            }
            ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
            ddlFieldForce.Enabled = false;

            chkVacant.Visible = false;
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            ViewState["sf_type"] = "";
            if (!Page.IsPostBack)
            {
                DataSet dsmgrsf = new DataSet();
                SalesForce sf = new SalesForce();
                DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
                    fillsubdivision();
                    FillMRManagers("0");
                    ddlFieldForce.SelectedValue = sf_code;
                }
                else
                {
                    DataTable dt = sf.getAuditManagerTeam(div_code, sf_code, 0);
                    dsmgrsf.Tables.Add(dt);
                    dsTP = dsmgrsf;

                    ddlFieldForce.DataTextField = "sf_name";
                    ddlFieldForce.DataValueField = "sf_code";
                    ddlFieldForce.DataSource = dsTP;
                    ddlFieldForce.DataBind();

                    ddlSF.DataTextField = "desig_Color";
                    ddlSF.DataValueField = "sf_code";
                    ddlSF.DataSource = dsTP;
                    ddlSF.DataBind();
                    ddlFFType.Visible = false;
                }
            }
            chkVacant.Visible = false;
        }
        else
        {
            ViewState["sf_type"] = "admin";
            if (!Page.IsPostBack)
            {
                fillsubdivision();
                chkVacant.Visible = true;
                FillMRManagers("0");
            }

            if (Session["div_code"] != null)
            {

            }
        }

        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

            Product prd = new Product();
            dsdiv = prd.getMultiDivsf_Name(sf_code);
            if (dsdiv.Tables[0].Rows.Count > 0)
            {
                if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                {
                    strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);

                    fillsubdivision();
                }
                else
                {

                }
            }
        }
        FillColor();
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
            ddlFieldForce.Items.Insert(0, "---Select Field Force---");
			ddlFieldForce.Items.Remove(ddlFieldForce.Items.FindByValue("admin"));

        }
        else
        {
            ddlFieldForce.DataSource = null;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Clear();
            ddlFieldForce.Items.Insert(0, "---Select Field Force---");
			ddlFieldForce.Items.Remove(ddlFieldForce.Items.FindByValue("admin"));
        }
    }

    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        ddlFFType.Visible = false;
        ddlAlpha.Visible = false;
        dsSalesForce = sf.UserList_getMR(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }


    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }

    protected void chkVacant_CheckedChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            if (chkVacant.Checked == false)
            {
                ddlAlpha.Visible = false;
                dsSalesForce = sf.UserList_getMR_New(div_code, "admin");
            }
            else
            {
                dsSalesForce = sf.UserList_getMR_New(div_code, sf_code);
            }
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(div_code, "admin");
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }


    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {

            string bcolor = "#" + ColorItems.Text;

            j = j + 1;

        }
    }



    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_AlphaAll(subdiv.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }

    }

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        try
        {
            string StartDate = Convert.ToDateTime(Request.Form["txtFrom"]).ToString("yyyy-MM-dd");


            SalesForce sf = new SalesForce();
            dsSf = sf.CheckSFType(ddlFieldForce.SelectedValue.ToString());

            if (dsSf.Tables[0].Rows.Count > 0)
            {
                if (ViewState["sf_type"].ToString() != "admin")
                    sf_type = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            string sURL = "rpt_Dailyinv_viewNative.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
                "&Mode=" + rbnList.SelectedItem.Value + "&Sf_Name=" + ddlFieldForce.SelectedItem.Text + "&Date=" + StartDate + "&Type=" + sf_type + "&Sub_div=" + subdiv.SelectedValue;
            string newWin = "window.open('" + sURL + "','Dailyinv','resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
            ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);


        }
        catch (Exception)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Date!!!');</script>");
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
}