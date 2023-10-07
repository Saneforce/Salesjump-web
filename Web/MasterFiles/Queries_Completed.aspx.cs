using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Queries_Completed : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsAdminSetup = null;
    DataSet dsdiv = null;
    DataSet dsDivision = null;
    string sf_type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Filldiv();          
            ddlDivision.SelectedIndex = 0;
        }

    }
    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        else
        {
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
        }
    }
    private void FillQueryList()
    {
        AdminSetup adm = new AdminSetup();
        dsAdminSetup = adm.getQuery_List_com(ddlDivision.SelectedValue);
        if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
            grdQuery.DataSource = dsAdminSetup;
            grdQuery.DataBind();
        }
        else
        {
            grdQuery.DataSource = dsAdminSetup;
            grdQuery.DataBind();
        }

    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillQueryList();
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender as GridView, "Select$" + e.Row.RowIndex.ToString()));


            GridView grdreporting = (GridView)e.Row.FindControl("grdreporting");

            Label sf_code = (Label)e.Row.FindControl("lblsf_code");
            Label lblQuery = (Label)e.Row.FindControl("lblQuery");
            //   GridView grdreporting = row.FindControl("grdreporting") as GridView;
            AdminSetup adm = new AdminSetup();
            dsAdminSetup = adm.getQuery_Reportingto(sf_code.Text, lblQuery.Text);
            if (dsAdminSetup.Tables[0].Rows.Count > 0)
            {
                grdreporting.DataSource = dsAdminSetup;
                grdreporting.DataBind();
            }
            else
            {
                grdreporting.DataSource = dsAdminSetup;
                grdreporting.DataBind();
            }
        }


    }
    protected void Show_Hide_OrdersGrid(object sender, EventArgs e)
    {
        ImageButton imgShowHide = (sender as ImageButton);
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
        if (imgShowHide.CommandArgument == "Show")
        {
            row.FindControl("pnlOrders").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "~/images/minus.png";
            row.Focus();
        }
        else
        {
            row.FindControl("pnlOrders").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "~/images/plus.png";

        }
    }
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Masterfiles/Query_Box_List.aspx");
    }
}