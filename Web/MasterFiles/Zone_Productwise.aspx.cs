using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Zone_Productwise : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDivision = null;
    int subdivcode = 0;
    int subdivision_code = 0;
    string divcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);       
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            FillSubdiv(divcode);
            ddlProduct.Focus();
        }

    }
    private void FillSubdiv(string divcode)
    {
        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubdivision(divcode);
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            ddlProduct.DataTextField = "subdivision_name";
            ddlProduct.DataValueField = "subdivision_code";
            ddlProduct.DataSource = dsSubDivision;
            ddlProduct.DataBind();
        }
    }
    protected void btnProduct_Click(object sender, EventArgs e)
    {
        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubdiv_Prod(divcode, ddlProduct.SelectedValue.ToString());
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            pnlprint.Visible = true;
            grdProduct.Visible = true;
            grdProduct.DataSource = dsSubDivision;
            grdProduct.DataBind();

        }
        else
        {
            grdProduct.DataSource = dsSubDivision;
            grdProduct.DataBind();
        }
    }
}