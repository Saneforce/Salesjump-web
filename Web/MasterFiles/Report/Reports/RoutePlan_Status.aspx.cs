using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Reports_RoutePlan_Status : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTerritory = null;
    DataSet dsSf = null;
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (Session["sf_type"].ToString() == "1")
        {     
            //UserControl_MR_Menu c1 =
               // (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;

            SalesForce sf = new SalesForce();
            dsSf = sf.getReportingTo(sf_code);
            if (dsSf.Tables[0].Rows.Count > 0)
            {
                sf_code = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            if (!Page.IsPostBack)
            {
                FillMRManagers();
            }
            ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
            ddlFieldForce.Enabled = false;
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                //c1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " Status";
            }

        }
        
         else if (Session["sf_type"].ToString() == "2")
         {     
           // UserControl_MGR_Menu c1 =
            //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            //Divid.Controls.Add(c1);
           // c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                //c1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " Status";
            }

            if (!Page.IsPostBack)
            {
                FillMRManagers();
                ddlFieldForce.SelectedValue = sf_code;
            }

         }
         else
         {
                //UserControl_MenuUserControl c1 =
                //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                //Divid.Controls.Add(c1);
                //c1.FindControl("btnBack").Visible = false;
                //c1.Title = this.Page.Title;
                if (dsTerritory.Tables[0].Rows.Count > 0)
                {
                    //c1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " Status";
                }

                if (!Page.IsPostBack)
                {
                    FillMRManagers();
                }
         }
        FillColor();
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

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        ddlFFType.Visible = false;
        ddlAlpha.Visible = false;

        // Added by Sridevi for Audit Manager
        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();

        // Check if the manager has a team
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        }
        else
        {
            // Fetch Managers Audit Team
            DataTable dt = ds.getAuditManagerTeam(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesForce = dsmgrsf;
        }
        //dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();


        }
        FillColor();


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


    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {


        string sURL = "rptRoutePlanStatus.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&sf_name=" + ddlFieldForce.SelectedItem.Text;

        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=800,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

    }

}