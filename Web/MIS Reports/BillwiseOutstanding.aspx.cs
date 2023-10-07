using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MIS_Reports_BillwiseOutstanding : System.Web.UI.Page
{
    #region "Declaration"
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsTP = null;
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
        if (!Page.IsPostBack)
        {
            fillsubdivision();
            FillMRManagers("0");
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

        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code, Sub_Div_Code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("---Select Field Force---", "0"));

        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        Invoice iv = new Invoice();
        DataSet dsBillPending = iv.getPendingBillSF(div_code, sf_code, subdiv.SelectedValue.ToString());
        GVPendingBill.DataSource = dsBillPending;
        GVPendingBill.DataBind();

		
		if (dsBillPending.Tables[0].Rows.Count > 0)
        {

	        decimal total = dsBillPending.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BalAmt"));
	        GVPendingBill.FooterRow.Cells[0].Text = "Total";
	        GVPendingBill.FooterRow.Cells[3].Text = total.ToString("N2");
	        GVPendingBill.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
	        GVPendingBill.FooterRow.Cells[0].ColumnSpan = 3;
	        GVPendingBill.FooterRow.Cells[1].Visible = false;
	        GVPendingBill.FooterRow.Cells[2].Visible = false;
		}




    }
}