using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Options_DCREdit : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsDCR = null;
    DataSet  dsSalesForce = null;
    string sfCode = string.Empty;
    DataSet dsTP = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
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

            FillSalesForce();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
        }
    }

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_New(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }

    private void FillTourPlan()
    {
        if ((ddlMonth.SelectedIndex > 0) && (ddlYear.SelectedIndex > 0))
        {
            DCR dc = new DCR();
            dsDCR = dc.getDCREdit(ddlFieldForce.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString());
            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                grdTP.Visible = true;
                grdTP.DataSource = dsDCR;
                grdTP.DataBind();
            }
            else
            {
                btnSubmit.Visible = false;
                grdTP.DataSource = null;
                grdTP.DataBind();
            }
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillTourPlan();
       // btnSubmit.Visible = true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdTP.Rows)
        {
            Label lblTrans_SlNo = (Label)gridRow.Cells[0].FindControl("lblTrans_SlNo");
            CheckBox chkDate = (CheckBox)gridRow.Cells[1].FindControl("chkDate");
            Label lblDate = (Label)gridRow.Cells[2].FindControl("lblDate");
            DateTime dtDCR = Convert.ToDateTime(lblDate.Text);
            string editdate = dtDCR.ToString("MM/dd/yyyy");
            if (chkDate.Checked)
            {
                DCR dcr = new DCR();
                iReturn = dcr.Option_EditDCRDates(ddlFieldForce.SelectedValue.ToString(), Convert.ToInt32(ddlMonth.SelectedValue.ToString()), Convert.ToInt16(ddlYear.SelectedValue.ToString()), Convert.ToInt32(lblTrans_SlNo.Text), editdate);
            }
        }

        if (iReturn > 0)
        {
            //Response.Write("DCR Edit Dates have been created successfully");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DCR Edit Dates have been created successfully');</script>");
            FillTourPlan();
        }
    }

}