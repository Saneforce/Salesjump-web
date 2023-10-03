using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Reports_rptRoutePlan : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTerritory = null;
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

            if (Session["sf_type"].ToString() == "2" || Session["sf_type"].ToString() == "1")
            {
                FillMRManagers();
                FillColorMR();
            }
        }
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (Session["sf_type"].ToString() == "2")
        {
            //UserControl_MGR_Menu Usc_MRG =
            //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            //DivMenu.Controls.Add(Usc_MRG);
            //Usc_MRG.FindControl("btnBack").Visible = false;
            //Usc_MRG.Title = this.Page.Title;
            ddlFieldForce.SelectedItem.Value = sfCode;
            ddlFieldForce.Visible = false;
            ddlFFType.Visible = false;
            ddlAlpha.Visible = false;
            ddlSF.Visible = false;
            lblFF.Visible = false;            
            lblMR.Visible = true;
            ddlMR.Visible = true;
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                //Usc_MRG.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " View";
            }

        }
        else if (Session["sf_type"].ToString() == "1")
        {
            //UserControl_MR_Menu Usc_MR =
            //(UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            //DivMenu.Controls.Add(Usc_MR);
            //Usc_MR.FindControl("btnBack").Visible = false;
            //Usc_MR.Title = this.Page.Title;
            ddlFieldForce.SelectedItem.Value = sfCode;
            ddlFieldForce.Visible = false;
            ddlFFType.Visible = false;
            ddlAlpha.Visible = false;
            ddlSF.Visible = false;
            lblFF.Visible = false;
            lblMR.Visible = true;
            ddlMR.Visible = true;
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                //Usc_MR.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " View";
            }

        }
        else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            //UserControl_MenuUserControl Usc_Menu =
           //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //DivMenu.Controls.Add(Usc_Menu);
            //Usc_Menu.Title = this.Page.Title;

            //Usc_Menu.FindControl("btnBack").Visible = false;
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                //Usc_Menu.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " View";
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

    private void FillColorMR()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF1.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlMR.Items[j].Attributes.Add("style", "background-color:" + bcolor);

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
        if (ddlAlpha.SelectedItem.Text == "All")
        {
            dsSalesForce = sf.UserList_Alpha(div_code, "admin");
        }
        else
        {
            dsSalesForce = sf.UserList_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
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
        string sURL = "rptRoutePlanView.aspx?sf_code=" + ddlMR.SelectedValue + "&sf_name=" + ddlMR.SelectedItem.Text ;

        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=800,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

    }

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();

        //if (Session["sf_type"].ToString() == "2")
        //{

        //    dsSalesForce = sf.SalesForceListMgrGet(div_code, sfCode);

        //    if (dsSalesForce.Tables[0].Rows.Count > 0)
        //    {
        //        ddlMR.DataTextField = "sf_name";
        //        ddlMR.DataValueField = "sf_code";
        //        ddlMR.DataSource = dsSalesForce;
        //        ddlMR.DataBind();

        //        ddlSF.DataTextField = "Desig_Color";
        //        ddlSF.DataValueField = "sf_code";
        //        ddlSF.DataSource = dsSalesForce;
        //        ddlSF.DataBind();

        //    }
        //}
        if (Session["sf_type"].ToString() == "1" || Session["sf_type"].ToString() == "2")
        {            
            ddlAlpha.Visible = false;
               DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();

        // Check if the manager has a team
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sfCode);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sfCode);
        }
        else
        {
            // Fetch Managers Audit Team
            DataTable dt = ds.getAuditManagerTeam(div_code, sfCode, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesForce = dsmgrsf;
        }
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                ddlMR.DataTextField = "sf_name";
                ddlMR.DataValueField = "sf_code";
                ddlMR.DataSource = dsSalesForce;
                ddlMR.DataBind();

                ddlSF1.DataTextField = "Desig_Color";
                ddlSF1.DataValueField = "sf_code";
                ddlSF1.DataSource = dsSalesForce;
                ddlSF1.DataBind();
            }
        }
        FillColor();

    }


    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        string sReport = ddlFieldForce.SelectedValue.ToString();
        dsSalesForce = sf.UserList_getMR(div_code, sReport);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            lblMR.Visible = true;
            ddlMR.Visible = true;
            ddlMR.DataTextField = "sf_name";
            ddlMR.DataValueField = "sf_code";
            ddlMR.DataSource = dsSalesForce;
            ddlMR.DataBind();

            ddlSF1.DataTextField = "des_color";
            ddlSF1.DataValueField = "sf_code";
            ddlSF1.DataSource = dsSalesForce;
            ddlSF1.DataBind();
        }

        FillColorMR();
        
    }
}