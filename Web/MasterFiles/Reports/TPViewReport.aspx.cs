using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Reports_TPViewReport : System.Web.UI.Page
{

    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        sf_code = Session["Sf_Code"].ToString();
        sfCode = Session["Sf_Code"].ToString();
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
        sf_code = Session["Sf_Code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (!Page.IsPostBack)
        {
            FillManagers();
            FillColor();
            hypConsolidate.Visible = false;
            chkConsolidate.Checked = false;

        }
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;

            if (ColorItems.Text == "Level1")
                //ColorItems.Attributes.Add("style", "background-color: Wheat");
                ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Wheat");

            if (ColorItems.Text == "Level2")
                //ColorItems.Attributes.Add("style", "background-color: Blue");
                ddlFieldForce.Items[j].Attributes.Add("style", "background-color: LightGreen");

            if (ColorItems.Text == "Level3")
                //ColorItems.Attributes.Add("style", "background-color: Cyan");
                ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Pink");

            if (ColorItems.Text == "Level4")
                //ColorItems.Attributes.Add("style", "background-color: Lavendar");
                ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Lavendar");

            j = j + 1;

        }
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            if (sf_type == "2")
            { dsSalesForce = sf.UserListTP_Hierarchy(div_code, sfCode); }
            else
            { dsSalesForce = sf.UserListTP_Hierarchy(div_code, "admin"); }
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;

            if (sf_type == "2")
            { dsSalesForce = sf.UserListTP_Alpha(div_code, sfCode); }
            else
            { dsSalesForce = sf.UserListTP_Alpha(div_code, "admin"); }


            //dsSalesForce = sf.UserListTP_Alpha(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            if (sf_type == "2")
            { dsSalesForce = sf.UserList_HQ(div_code, sfCode); }
            else
            { dsSalesForce = sf.UserList_HQ(div_code, "admin"); }

            //dsSalesForce = sf.UserList_HQ(div_code, "admin");
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "sf_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

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

    protected string LoadReport()
    {
        return "rptTPView.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&cur_month=" + ddlMonth.SelectedValue.ToString() + "&cur_year=" + ddlYear.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString();
       
    }
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();

        if (sf_type == "2")
        { dsSalesForce = sf.UserListTP_Alphasearch(div_code, sfCode, ddlAlpha.SelectedValue); }
        else
        { dsSalesForce = sf.UserListTP_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue); }

        //dsSalesForce = sf.UserListTP_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "sf_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //FillTourPlan();
        if (rdoHypLevel.SelectedIndex >= 0)
        {
            string sURL = "rptTPView.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&cur_month=" + ddlMonth.SelectedValue.ToString() + "&cur_year=" + ddlYear.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString() + "&level=" + rdoHypLevel.SelectedValue.ToString();
            string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=1000,height=800,left=100,top=100');";
            ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
        }
        else
        {
            string sURL = "rptTPView.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&cur_month=" + ddlMonth.SelectedValue.ToString() + "&cur_year=" + ddlYear.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString() + "&level=-1";
            string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=1000,height=800,left=100,top=100');";
            ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
        }
    }

    protected void chkConsolidate_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlSF.SelectedItem.Text.ToString() != "1")
        {
            if (chkConsolidate.Checked)
            {
                hypConsolidate.Visible = true;
            }
            else
            {
                hypConsolidate.Visible = false;
            }
        }
    }
}