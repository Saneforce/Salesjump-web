using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Reports_DCR_NotSubmit : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    string div_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        btnSubmit.Focus();
        if (!Page.IsPostBack)
        {
            Filldiv();
            FillManagers();
            FillColor();
            //menu1.FindControl("btnBack").Visible = false;
            //menu1.Title = this.Page.Title;
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
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

    private void Filldiv()
    {
        Division dv = new Division();
        dsDivision = dv.getDivision();
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_Code";
            ddlDivision.DataSource = dsDivision;
            ddlDivision.DataBind();
        }
    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
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
            //dsSalesForce = sf.UserList_Alpha(div_code, "admin");
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


    private void FillColor()
    {
        int j = 0;

         foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        if (ddlAlpha.SelectedItem.Text == "All")
        {
            dsSalesForce = sf.UserListTP_Alpha(div_code, "admin");
        }
        else
        {
            dsSalesForce = sf.UserList_Alphasearch(ddlDivision.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
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
        FillColor();

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string sURL = "rptDCRNotSubmit.aspx?cmon=" + ddlMonth.SelectedValue.ToString() + "&cyear=" + ddlYear.SelectedItem.Text.Trim();
        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=800,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    }



}