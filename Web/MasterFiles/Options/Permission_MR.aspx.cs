using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
public partial class MasterFiles_Options_Permission_MR : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSalesForce = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillManagers();
            FillColor();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            btnSumbit.Visible = false;
        }
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

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(div_code, "admin");
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

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
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
  
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFF.Text = "Field Force";
        FillManagers();
        FillColor();
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
        dsSalesForce = sf.UserList_getVacantMR(div_code, ddlFieldForce.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            gvDetails.Visible = true;
            btnSumbit.Visible = true;
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
    
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string cur_sf_code = string.Empty;
        string cur_sf_name = string.Empty;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblSFCode = (Label)e.Row.FindControl("lblDocCode");
            CheckBox chkLevel1 = (CheckBox)e.Row.FindControl("chkLevel1");
            CheckBox chkLevel2 = (CheckBox)e.Row.FindControl("chkLevel2");
            CheckBox chkLevel3 = (CheckBox)e.Row.FindControl("chkLevel3");

            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getReporting(lblSFCode.Text.Trim());

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drSF in dsSalesForce.Tables[0].Rows)
                {
                    cur_sf_code = drSF["sf_code"].ToString();
                    cur_sf_name = drSF["sf_name"].ToString();
                }
                chkLevel3.Text = cur_sf_name;
            }

            dsSalesForce = sf.getReporting(cur_sf_code);

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drSF in dsSalesForce.Tables[0].Rows)
                {
                    cur_sf_code = drSF["sf_code"].ToString();
                    cur_sf_name = drSF["sf_name"].ToString();
                }
                chkLevel2.Text = cur_sf_name;
            }

            dsSalesForce = sf.getReporting(cur_sf_code);

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drSF in dsSalesForce.Tables[0].Rows)
                {
                    cur_sf_code = drSF["sf_code"].ToString();
                    cur_sf_name = drSF["sf_name"].ToString();
                }
                chkLevel1.Text = cur_sf_name;
            }

        }
    }

    protected void btnSumbit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        int Level1 = 0;
        int Level2 = 0;
        int Level3 = 0;
        AdminSetup adsp = new AdminSetup();
        iReturn = adsp.Permission_MR_Delete(ddlFieldForce.SelectedValue.ToString().Trim(), div_code);

        foreach (GridViewRow gridRow in gvDetails.Rows)
        {
            Level1 = 0;
            Level2 = 0;
            Level3 = 0;

            Label lblDocCode = (Label)gridRow.Cells[1].FindControl("lblDocCode");
            CheckBox chkLevel1 = (CheckBox)gridRow.Cells[1].FindControl("chkLevel1");
            CheckBox chkLevel2 = (CheckBox)gridRow.Cells[1].FindControl("chkLevel2");
            CheckBox chkLevel3 = (CheckBox)gridRow.Cells[1].FindControl("chkLevel3");

            if (chkLevel1.Checked)
                Level1 = 1;

            if (chkLevel2.Checked)
                Level2 = 1;

            if (chkLevel3.Checked)
                Level3 = 1;

            iReturn = adsp.Permission_MR_Add(ddlFieldForce.SelectedValue.ToString().Trim(), lblDocCode.Text, div_code, Level1, Level2, Level3);

        }

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Permission for Vacant MR have been created Successfully');</script>");
            gvDetails.DataSource = null;
            gvDetails.DataBind();
            gvDetails.Visible = false;
            btnSumbit.Visible = false;
        }

    }
   
}   