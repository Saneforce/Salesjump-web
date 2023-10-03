using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Options_Delayed_Release : System.Web.UI.Page
{
    DataSet dsadmin = null;
    DataSet dsadm = null;
    DataSet dsTP = null;
    DataSet dsDCR = null;
    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                    ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                }
            }
            DCR dc = new DCR();
            dsDCR = dc.get_Release_Sf(div_code);
            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                ddlFieldForce.DataTextField = "sf_name";
                ddlFieldForce.DataValueField = "sf_code";
                ddlFieldForce.DataSource = dsDCR;
                ddlFieldForce.DataBind();
            }
           

        }
    }

    private void GetRelease()
    {
        if ((ddlMonth.SelectedIndex > 0) && (ddlYear.SelectedIndex > 0))
        {
            DCR dc = new DCR();
            dsDCR = dc.getReleaseDate(ddlFieldForce.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString());
            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                grdRelease.Visible = true;
                grdRelease.DataSource = dsDCR;
                grdRelease.DataBind();
            }
            else
            {
                btnSubmit.Visible = false;
                grdRelease.DataSource = dsDCR;
                grdRelease.DataBind();
            }

        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        GetRelease();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdRelease.Rows)
        {
            Label lblsf_code = (Label)gridRow.Cells[0].FindControl("lblsf_code");
            string lblsfcode = lblsf_code.Text.ToString();
            CheckBox chkRelease = (CheckBox)gridRow.Cells[1].FindControl("chkRelease");
            bool bCheck = chkRelease.Checked;
            Label lblDate = (Label)gridRow.Cells[2].FindControl("lblDate");
            Label lblMode = (Label)gridRow.Cells[2].FindControl("lblMode");
         
            if ((lblsfcode.Trim().Length > 0) && (bCheck == true))
            {
                DCR dcr = new DCR();
                iReturn = dcr.Update_Delayed(lblsf_code.Text, Convert.ToDateTime(lblDate.Text), lblMode.Text);
            }
            if (iReturn > 0)
            {
                //Response.Write("DCR Edit Dates have been created successfully");
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Released Dates successfully');</script>");
                GetRelease();

            }
        }
    }

    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblBackColor = (Label)e.Row.FindControl("lblMode");
            if (lblBackColor.Text == "A")
            {
                string bcolor = "#ffdd99";
                e.Row.BackColor = System.Drawing.Color.FromName(bcolor);
            }
        }
    }
}