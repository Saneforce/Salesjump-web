using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MGR_Leave_Approval : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsAdminSetup = null;
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        sfCode = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            FillLeave();
        }

    }
    private void FillLeave()
    {
        grdLeave.DataSource = null;
        grdLeave.DataBind();


        AdminSetup adm = new AdminSetup();
        dsAdminSetup = adm.getLeave_approve(sfCode, 2, div_code);
        if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
            grdLeave.Visible = true;
            grdLeave.DataSource = dsAdminSetup;
            grdLeave.DataBind();
        }
        else
        {
            grdLeave.DataSource = dsAdminSetup;
            grdLeave.DataBind();
        }
    }
}