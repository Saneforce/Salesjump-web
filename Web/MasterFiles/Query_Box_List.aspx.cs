using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Query_Box_List : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsAdminSetup = null;
    DataSet dsdiv = null;
    DataSet dsDivision = null;
    string sf_type = string.Empty;
    int Query_Id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Filldiv();
          //  GetReporting();
         //   menu1.Title = this.Page.Title;
          //  menu1.FindControl("btnBack").Visible = false;
            ddlDivision.SelectedIndex = 0;
            btngo_Click(sender, e);
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
            dsDivision = dv.getDivision_Name_Queries();
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
        dsAdminSetup = adm.getQuery_List(ddlDivision.SelectedValue);
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
    //private void GetReporting()
    //{
    //    foreach (GridViewRow row in grdQuery.Rows)
    //    {
    //        Label sf_code = (Label)row.FindControl("lblsf_code");
    //        GridView grdreporting = row.FindControl("grdreporting") as GridView;
    //        AdminSetup adm = new AdminSetup();
    //        dsAdminSetup = adm.getQuery_Reportingto(sf_code.Text);
    //        if (dsAdminSetup.Tables[0].Rows.Count > 0)
    //        {
    //            grdreporting.DataSource = dsAdminSetup;
    //            grdreporting.DataBind();
    //        }
    //        else
    //        {
    //            grdreporting.DataSource = dsAdminSetup;
    //            grdreporting.DataBind();
    //        }
    //    }
    //}

    protected void btnComp_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Queries_Completed.aspx");
    }
    protected void grdQuery_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Status")
        {
            LinkButton lnkdeact = (LinkButton)grdQuery.FindControl("lnkbutPending");
            Query_Id = Convert.ToInt16(e.CommandArgument);
            //Deactivate
            AdminSetup adm = new AdminSetup();
            int iReturn = adm.Query_Com(Query_Id, ddlDivision.SelectedValue);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Completed Successfully.\');", true);
            }


            FillQueryList();
        }
    }
    protected void btnLog_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        FillQueryList();
    }

}