using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Options_TPDelete : System.Web.UI.Page
{

    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    string sfCode = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillManagers();
            FillColor();
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                    ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                }
            }

        }

    }

    private void FillTourPlan()
    {
        if ((ddlMonth.SelectedIndex > 0) && (ddlYear.SelectedIndex > 0))
        {
            TourPlan tp = new TourPlan();
            dsTP = tp.FillTourPlan_Delete(ddlFieldForce.SelectedValue.ToString(), div_code, Convert.ToInt16( ddlMonth.SelectedValue.ToString()), Convert.ToInt16( ddlYear.SelectedValue.ToString()));
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                grdTP.Visible = true;
                grdTP.DataSource = dsTP;
                grdTP.DataBind();
                btnSubmit.Visible = true;
            }
            else
            {
                grdTP.DataSource = null;
                grdTP.DataBind();
                
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TP Not entered for the selected month');</script>");
                btnSubmit.Visible = false;
            }
        }
    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }

    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSFCode = (Label)e.Row.FindControl("lblSFCode");
            Label lblFutureTP = (Label)e.Row.FindControl("lblFutureTP");            
            CheckBox chkTP = (CheckBox)e.Row.FindControl("chkTP");
            CheckBox chkSNo = (CheckBox)e.Row.FindControl("chkSNo");

            TourPlan tp = new TourPlan();
            DataSet dsTp = new DataSet();
            //int iReturn = tp.get_TP_Count_FieldForce(lblSFCode.Text, ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString());
            //if (iReturn > 0)
            //{
            //    chkTP.Checked = true;
            //}

            int iMonth = Convert.ToInt32( ddlMonth.SelectedValue.ToString());
            iMonth = iMonth + 1;
            dsTP = tp.FillFutureTourPlan(lblSFCode.Text, iMonth.ToString(), ddlYear.SelectedValue.ToString());
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in dsTP.Tables[0].Rows)
                {
                    lblFutureTP.Text = lblFutureTP.Text + getmonthname(Convert.ToInt32( dataRow["Tour_Month"].ToString())) + "-" + dataRow["Tour_Year"].ToString() + ",";                    
                }
                chkSNo.Enabled = false;
            }
        }
    }

    private string getmonthname(int iMonth)
    {
        string sReturn = string.Empty;


        if (iMonth == 1)
        {
            sReturn = "Jan";
        }
        else if (iMonth == 2)
        {
            sReturn = "Feb";
        }
        else if (iMonth == 3)
        {
            sReturn = "Mar";
        }
        else if (iMonth == 4)
        {
            sReturn = "Apr";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "Jun";
        }
        else if (iMonth == 7)
        {
            sReturn = "Jul";
        }
        else if (iMonth == 8)
        {
            sReturn = "Aug";
        }
        else if (iMonth == 9)
        {
            sReturn = "Sep";
        }
        else if (iMonth == 10)
        {
            sReturn = "Oct";
        }
        else if (iMonth == 11)
        {
            sReturn = "Nov";
        }
        else if (iMonth == 12)
        {
            sReturn = "Dec";
        }

        return sReturn;
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;

            if (ColorItems.Text == "Level1")
                //ColorItems.Attributes.Add("style", "background-color: Wheat");
                ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Wheat");

            if (ColorItems.Text == "Level2")
                //ColorItems.Attributes.Add("style", "background-color: Blue");
                ddlFieldForce.Items[j].Attributes.Add("style", "background-color: LightGreen");

            if (ColorItems.Text == "Level3")
                //ColorItems.Attributes.Add("style", "background-color: Cyan");
                ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Pink");

            if (ColorItems.Text == "Level4")
                //ColorItems.Attributes.Add("style", "background-color: Lavendar");
                ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Lavendar");

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

            ddlSF.DataTextField = "sf_color";
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

            ddlSF.DataTextField = "sf_color";
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
        FillTourPlan();
       // btnSubmit.Visible = true;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdTP.Rows)
        {
            CheckBox chkSNo = (CheckBox)gridRow.Cells[0].FindControl("chkSNo");
            if (chkSNo.Checked)
            {
                Label lblSFCode = (Label)gridRow.Cells[1].FindControl("lblSFCode");
                TourPlan tp = new TourPlan();
                iReturn = tp.DeleteTP(lblSFCode.Text, ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString());
            }
        }

        if (iReturn > 0)
        {
            //Response.Write("TP has been deleted successfully");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TP has been deleted successfully');</script>");
            FillTourPlan();
        }
    }
}