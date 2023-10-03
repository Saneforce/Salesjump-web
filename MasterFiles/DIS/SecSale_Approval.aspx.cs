using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MGR_SecSale_Approval : System.Web.UI.Page
{
    #region "Declaration"
        DataSet dsSecSales = null;
        string sf_code = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            //Populate the Secondary Sales grid which are waiting for approval
            FillSecSales();
        }
    }

    //Populate the Secondary Sales grid which are waiting for approval
    private void FillSecSales()
    {
        grdSecSales.DataSource = null;
        grdSecSales.DataBind();
        SecSale ss = new SecSale();
        //Get the approval required list
        dsSecSales = ss.get_SecSales_Pending_Approval(sf_code, 1);
        if (dsSecSales.Tables[0].Rows.Count > 0)
        {
            grdSecSales.Visible = true;
            grdSecSales.DataSource = dsSecSales;
            grdSecSales.DataBind();
        }
    }

}