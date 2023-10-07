using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Options_Screenwise_Lock : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    string sfCode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        btnGo.Focus();
        if (!Page.IsPostBack)
        {
            FillManagers();
            FillColor();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
        }
        FillColor();
    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);             

            j = j + 1;

        }
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserListTP_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }

    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserListTP_Hierarchy(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserListTP_Alpha(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(div_code, "admin");
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }



    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedValue.ToString().Trim().Length > 0)
        {
            FillSalesForce();
        }

    }

    private void FillSalesForce()
    {
        gvDetails.DataSource = null;
        gvDetails.DataBind();
     
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.FillSF_ScreenAccess(ddlFieldForce.SelectedValue.ToString().Trim(), div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            btnSumbit.Visible = true;
            gvDetails.Visible = true;
            gvDetails.DataSource = dsSalesForce;
            gvDetails.DataBind();
        }
        else
        {
            btnSumbit.Visible = false;
            gvDetails.DataSource = dsSalesForce;
            gvDetails.DataBind();
        }
    }

    //protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //        Label lblDocCode = (Label)e.Row.FindControl("lblDocCode");
    //        CheckBox chkLevel1 = (CheckBox)e.Row.FindControl("chkLevel1");
    //        CheckBox chkLevel2 = (CheckBox)e.Row.FindControl("chkLevel2");
    //        CheckBox chkLevel3 = (CheckBox)e.Row.FindControl("chkLevel3");
    //        CheckBox chkLevel4 = (CheckBox)e.Row.FindControl("chkLevel4");
    //    }
    //}
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_SName"] + " Lock";

            }
        }
    }
    protected void btnSumbit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        int DCR_Lock = 0;
        int TP_Lock = 0;
        int SDP_Lock = 0;
        int Camp_Lock = 0;
        int DR_Lock = 0;
        AdminSetup adsp = new AdminSetup();
        iReturn = adsp.Screen_Lock_Delete(ddlFieldForce.SelectedValue.ToString().Trim(), div_code);

        foreach (GridViewRow gridRow in gvDetails.Rows)
        {
            DCR_Lock = 0;
            TP_Lock = 0;
            SDP_Lock = 0;
            Camp_Lock = 0;
            DR_Lock = 0;

            Label lblDocCode = (Label)gridRow.Cells[1].FindControl("lblDocCode");
            CheckBox chkLevel1 = (CheckBox)gridRow.Cells[1].FindControl("chkLevel1");
            CheckBox chkLevel2 = (CheckBox)gridRow.Cells[1].FindControl("chkLevel2");
            CheckBox chkLevel3 = (CheckBox)gridRow.Cells[1].FindControl("chkLevel3");
            CheckBox chkLevel4 = (CheckBox)gridRow.Cells[1].FindControl("chkLevel4");
            CheckBox chkLevel5 = (CheckBox)gridRow.Cells[1].FindControl("chkLevel5");

            if (chkLevel1.Checked)
                DCR_Lock = 1;
                
            if (chkLevel2.Checked)
                TP_Lock = 1;

            if (chkLevel3.Checked)
                SDP_Lock = 1;

            if (chkLevel4.Checked)
                Camp_Lock = 1;

            if (chkLevel5.Checked)
                DR_Lock = 1;

            iReturn = adsp.Screen_Lock_Add(ddlFieldForce.SelectedValue.ToString().Trim(), lblDocCode.Text, div_code, DCR_Lock, TP_Lock, SDP_Lock, Camp_Lock,DR_Lock);

        }

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Screen Level Lock have been created Successfully');</script>");
            gvDetails.DataSource = null;
            gvDetails.DataBind();
            gvDetails.Visible = false;
            btnSumbit.Visible = false;
        }

    }
}