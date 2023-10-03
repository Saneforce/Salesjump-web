using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_DCR_Admin_Approval : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDcr = null;
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
            FillDoc();
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
        }
    }
    private void FillDoc()
    {
        grdDCR.DataSource = null;
        grdDCR.DataBind();

        DCR dr = new DCR();
        if (div_code.Contains(','))
            div_code = div_code.Substring(0, div_code.Length - 1);
        dsDcr = dr.get_DCR_Pending_Approval(sfCode, div_code);
        if (dsDcr.Tables[0].Rows.Count > 0)
        {
            grdDCR.Visible = true;
            grdDCR.DataSource = dsDcr;
            grdDCR.DataBind();
        }
        else
        {
            grdDCR.DataSource = dsDcr;
            grdDCR.DataBind();
        }
    }
}