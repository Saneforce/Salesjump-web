using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Options_MailView : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    string sfCode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        //sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillSalesForce();
        
            if (ddlSearchBy.SelectedValue.ToString() == "1")
            {
                FillInbox();
            }
            if (ddlSearchBy.SelectedValue.ToString() == "2")
            {
                FillViewedMail();
            }
            if (ddlSearchBy.SelectedValue.ToString() == "3")
            {
                FillSentEMail();
            }
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            }            
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
        }
    }

    private void FillInbox()
    {
        gv1.DataSource = null;
        gv1.DataBind();

        AdminSetup adm = new AdminSetup();
        dsSalesForce = adm.getMailInbox(ddlFieldForce.SelectedValue.ToString(), div_code, "inbox", "", ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), "Mail_Sent_Time", "asc","");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            gv1.DataSource = dsSalesForce;
            gv1.DataBind();
        }
        else
        {
            gv1.DataSource = null;
            gv1.DataBind();
        }
    }

    private void FillSentEMail()
    {
        gv1.DataSource = null;
        gv1.DataBind();

        AdminSetup adm = new AdminSetup();
        dsSalesForce = adm.getMailInbox(ddlFieldForce.SelectedValue.ToString(), div_code, "sent", "", ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), "Mail_Sent_Time", "asc","");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            gv1.DataSource = dsSalesForce;
            gv1.DataBind();
        }
    }

    private void FillViewedMail()
    {
        gv1.DataSource = null;
        gv1.DataBind();

        AdminSetup adm = new AdminSetup();
        dsSalesForce = adm.getMailInbox(ddlFieldForce.SelectedValue.ToString(), div_code, "view", "", ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), "Mail_Sent_Time", "asc","");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            gv1.DataSource = dsSalesForce;
            gv1.DataBind();
        }
    }

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.UserListTP_Alpha(div_code, "admin");
        dsSalesForce = sf.getFieldForce_MailView(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedValue.ToString() == "1")
        {
            FillInbox();
        }
        if (ddlSearchBy.SelectedValue.ToString() == "2")
        {
            FillViewedMail();
        }
        if (ddlSearchBy.SelectedValue.ToString() == "3")
        {
            FillSentEMail();
        }

    }
}