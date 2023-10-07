using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Options_Mgrwise_Core_Doc_Map : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    string div_code = string.Empty;
    string sLevel1_Code = string.Empty;
    string sLevel1_Name = string.Empty;
    string sLevel2_Code = string.Empty;
    string sLevel2_Name = string.Empty;
    string sLevel3_Code = string.Empty;
    string sLevel3_Name = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillSalesForce(div_code);
            FillSF_Alpha();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
        }

    }

    private void FillSalesForce(string div_code)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_getMR_New(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlMR.DataTextField = "sf_name";
            ddlMR.DataValueField = "sf_code";
            ddlMR.DataSource = dsSalesForce;
            ddlMR.DataBind();

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

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_getMR_Alpha(div_code, ddlAlpha.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlMR.DataTextField = "sf_name";
            ddlMR.DataValueField = "sf_code";
            ddlMR.DataSource = dsSalesForce;
            ddlMR.DataBind();
       }

    }
    protected void ddlMR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMR.SelectedValue.ToString().Trim().Length > 0)
            FillDoc();
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblDocCode = (Label)e.Row.FindControl("lblDocCode");
            CheckBox chkLevel1 = (CheckBox)e.Row.FindControl("chkLevel1");
            CheckBox chkLevel2 = (CheckBox)e.Row.FindControl("chkLevel2");
            CheckBox chkLevel3 = (CheckBox)e.Row.FindControl("chkLevel3");
            CheckBox chkLevel4 = (CheckBox)e.Row.FindControl("chkLevel4");

            AdminSetup adm = new AdminSetup();
            bool bRet = adm.Core_Doctor_Map_RecordExist(ViewState["sLevel1_Code"].ToString().Trim(), ddlMR.SelectedValue.ToString().Trim(), div_code, lblDocCode.Text.Trim());
            if (bRet)
            {
                chkLevel1.Checked = true;
            }
            else
            {
                chkLevel1.Checked = false;
            }

            bRet = adm.Core_Doctor_Map_RecordExist(ViewState["sLevel2_Code"].ToString().Trim(), ddlMR.SelectedValue.ToString().Trim(), div_code, lblDocCode.Text.Trim());
            if (bRet)
            {
                chkLevel2.Checked = true;
            }
            else
            {
                chkLevel2.Checked = false;
            }

            bRet = adm.Core_Doctor_Map_RecordExist(ViewState["sLevel3_Code"].ToString().Trim(), ddlMR.SelectedValue.ToString().Trim(), div_code, lblDocCode.Text.Trim());
            if (bRet)
            {
                chkLevel3.Checked = true;
            }
            else
            {
                chkLevel3.Checked = false;
            }

            bRet = adm.Core_Doctor_Map_RecordExist(ddlMR.SelectedValue.ToString().Trim(), ddlMR.SelectedValue.ToString().Trim(), div_code, lblDocCode.Text.Trim());
            if (bRet)
            {
                chkLevel4.Checked = true;
            }
            else
            {
                chkLevel4.Checked = false;
            }

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[5].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();           

            }
        }
    }

    private void FillDoc()
    {
        gvDetails.DataSource = null;
        gvDetails.DataBind();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getReporting(ddlMR.SelectedValue.ToString().Trim());

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drSF in dsSalesForce.Tables[0].Rows)
            {
                sLevel3_Code = drSF["sf_code"].ToString();
                ViewState["sLevel3_Code"] = drSF["sf_code"].ToString();
                sLevel3_Name = drSF["sf_name"].ToString();
            }
        }

        dsSalesForce = sf.getReporting(sLevel3_Code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drSF in dsSalesForce.Tables[0].Rows)
            {
                sLevel2_Code = drSF["sf_code"].ToString();
                ViewState["sLevel2_Code"] = drSF["sf_code"].ToString();
                sLevel2_Name = drSF["sf_name"].ToString();
            }
        }

        dsSalesForce = sf.getReporting(sLevel2_Code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drSF in dsSalesForce.Tables[0].Rows)
            {
                sLevel1_Code = drSF["sf_code"].ToString();
                ViewState["sLevel1_Code"] = drSF["sf_code"].ToString();
                sLevel1_Name = drSF["sf_name"].ToString();
            }
        }


        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getListedDr(ddlMR.SelectedValue.ToString().Trim());
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            gvDetails.Visible = true;
            gvDetails.DataSource = dsDoc;
            gvDetails.DataBind();

        }

        gvDetails.HeaderRow.Cells[6].Text = gvDetails.HeaderRow.Cells[6].Text + "(" + sLevel1_Name + ")";
        gvDetails.HeaderRow.Cells[7].Text = gvDetails.HeaderRow.Cells[7].Text + "(" + sLevel2_Name + ")";
        gvDetails.HeaderRow.Cells[8].Text = gvDetails.HeaderRow.Cells[8].Text + "(" + sLevel3_Name + ")";
        gvDetails.HeaderRow.Cells[9].Text = gvDetails.HeaderRow.Cells[9].Text + "(" + ddlMR.SelectedItem.Text + ")";
    
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        string cur_sf_code = string.Empty;
        AdminSetup adsp = new AdminSetup();
        iReturn = adsp.Core_Doctor_Map_Delete(ddlMR.SelectedValue.ToString().Trim(), div_code);

        if (iReturn != -1)
        {
            foreach (GridViewRow gridRow in gvDetails.Rows)
            {
                Label lblDocCode = (Label)gridRow.Cells[1].FindControl("lblDocCode");
                CheckBox chkLevel1 = (CheckBox)gridRow.Cells[1].FindControl("chkLevel1");
                CheckBox chkLevel2 = (CheckBox)gridRow.Cells[1].FindControl("chkLevel2");
                CheckBox chkLevel3 = (CheckBox)gridRow.Cells[1].FindControl("chkLevel3");
                CheckBox chkLevel4 = (CheckBox)gridRow.Cells[1].FindControl("chkLevel4");

                if (chkLevel1.Checked)
                    iReturn = adsp.Core_Doctor_Map_Add(ddlMR.SelectedValue.ToString().Trim(), div_code, ViewState["sLevel1_Code"].ToString(), lblDocCode.Text.Trim());

                if (chkLevel2.Checked)
                    iReturn = adsp.Core_Doctor_Map_Add(ddlMR.SelectedValue.ToString().Trim(), div_code, ViewState["sLevel2_Code"].ToString(), lblDocCode.Text.Trim());

                if (chkLevel3.Checked)
                    iReturn = adsp.Core_Doctor_Map_Add(ddlMR.SelectedValue.ToString().Trim(), div_code, ViewState["sLevel3_Code"].ToString(), lblDocCode.Text.Trim());

                if (chkLevel4.Checked)
                    iReturn = adsp.Core_Doctor_Map_Add(ddlMR.SelectedValue.ToString().Trim(), div_code, ddlMR.SelectedValue.ToString(), lblDocCode.Text.Trim());

            }

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Core Doctor(s) have been mapped Successfully');</script>");
            }
        }
    }
}