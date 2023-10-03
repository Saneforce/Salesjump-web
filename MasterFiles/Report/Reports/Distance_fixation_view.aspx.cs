using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Subdiv_Salesforcewise : System.Web.UI.Page
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

        if (Request.QueryString["sfCode"]!=null)
        {
            Distance_calculation Exp = new Distance_calculation();
            sfcode = Request.QueryString["sfCode"].ToString();
            divcode = Request.QueryString["divCode"].ToString();
            DataTable ds = Exp.getFieldForce(divcode, sfcode);
            hqId.InnerText = ds.Rows[0]["sf_hq"].ToString();

            populateGriddata(false);
            mainDiv.Visible = false;
            divHqId.Visible = true;
        }
        else
        {
            divcode = Convert.ToString(Session["div_code"]);
            sfcode = Convert.ToString(Session["Sf_Code"]);
            if (!Page.IsPostBack)
            {
                ServerStartTime = DateTime.Now;
                base.OnPreInit(e);
                menu1.Title = this.Page.Title;
                menu1.FindControl("btnBack").Visible = false;
                FillFieldForcediv(divcode);
                ddlSubdiv.Focus();
            }

        }
    }
   
    private void FillFieldForcediv(string divcode)
    {
        Distance_calculation dv = new Distance_calculation();
        dsSubDivision = dv.getFieldForce(divcode);
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            ddlSubdiv.DataTextField = "sf_name";
            ddlSubdiv.DataValueField = "sf_code";
            ddlSubdiv.DataSource = dsSubDivision;
            ddlSubdiv.DataBind();
        }
    }
    protected void btnSF_Click(object sender, EventArgs e)
    {
        populateGriddata(true);
    }

    private void populateGriddata(bool flag)
    {
        string sf_code = "";
        if (flag)
        {
            sf_code = ddlSubdiv.SelectedValue.ToString();
        }
        else
        {
            sf_code = sfcode;
        }
        Distance_calculation dv = new Distance_calculation();
        dsSubDivision = dv.getDistance(divcode, sf_code);
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