using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_District_Salesforcewise : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDivision = null;
    int subdivcode = 0;
    int subdivision_code = 0;
    string divcode = string.Empty;
    string sfcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        sfcode = Convert.ToString(Session["Sf_Code"]); 
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            FillSubdiv(divcode);
            ddlSubdiv.Focus();
        }
    }
    private void FillSubdiv(string divcode)
    {
        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubdivision(divcode);
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            ddlSubdiv.DataTextField = "subdivision_name";
            ddlSubdiv.DataValueField = "subdivision_code";
            ddlSubdiv.DataSource = dsSubDivision;
            ddlSubdiv.DataBind();
        }
    }
    protected void btnSF_Click(object sender, EventArgs e)
    {
        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubdiv_Sales(divcode, ddlSubdiv.SelectedValue.ToString());
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            pnlprint.Visible = true;
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSubDivision;
            grdSalesForce.DataBind();

        }
        else
        {
            grdSalesForce.DataSource = dsSubDivision;
            grdSalesForce.DataBind();
        }
    }
}