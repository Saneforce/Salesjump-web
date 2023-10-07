using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Reports_StockiestSalesReport : System.Web.UI.Page
{
    #region "Variable Declarations"
    DataSet dsYear = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DataSet dsState = new DataSet();
    DataSet dsSecSale = new DataSet();
    DataSet dsSalesforce = new DataSet();
    int iErrReturn = -1;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Get the sf_code & div_code from session
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack) // Only on first time page load
        {
            //Populate Year dropdown
            FillYear();

            //Populate MR dropdown as per sf_code
            FillMR();
        }

        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;

            ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
            ddlFieldForce.Enabled = false;

        }

        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
        }
        else
        {

            UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
        }

    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillStockiest();
    }

    //Populate the Year dropdown
    private void FillYear()
    {
        try
        {
            TourPlan tp = new TourPlan();
            dsYear = tp.Get_TP_Edit_Year(div_code); // Get the Year for the Division
            if (dsYear.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsYear.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                }
            }
            ddlYear.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Report", "FillYear()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    private void FillMR()
    {
        SalesForce sf = new SalesForce();
        dsSalesforce = sf.getSecSales_MR(div_code, sf_code);

        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();


        }
    }

    private void FillStockiest()
    {
        SecSale ss = new SecSale();
        dsSecSale = ss.getStockiestDet(ddlFieldForce.SelectedValue.ToString().Trim());
        if (dsSecSale.Tables[0].Rows.Count > 0)
        {
            grdSecSales.DataSource = dsSecSale;
            grdSecSales.DataBind();
        }
    }

    protected void grdSecSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStockiestCode = (Label)e.Row.FindControl("lblStockiestCode");
            CheckBox chkSaleEntry = (CheckBox)e.Row.FindControl("chkSaleEntry");

            SecSale ss = new SecSale();
            bool bentry = ss.getcount_ssentry(div_code, ddlFieldForce.SelectedValue.ToString().Trim(), lblStockiestCode.Text.Trim(), Convert.ToInt16(ddlMonth.SelectedValue.ToString().Trim()), Convert.ToInt16(ddlYear.SelectedValue.ToString().Trim()));
            if (bentry)
                chkSaleEntry.Checked = true;
            else
                chkSaleEntry.Checked = false;
        }
    }

}