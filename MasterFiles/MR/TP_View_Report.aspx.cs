using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Temp_TP_View_Report : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    string strTPView = string.Empty;
    string sfCode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        chkConsolidate.Visible = false;
        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                }
            }

            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
             DataSet dsmgrsf = new DataSet();
            SalesForce sf = new SalesForce();
            DataSet DsAudit = sf.SF_Hierarchy(div_code, sfCode);
            if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
            {
                FillReporting();
                FillManagers();
                FillDivision();
                ddlFilterby.SelectedIndex = 1;
                ddlFieldForce.SelectedIndex = 0;
                ddlFilterBy_SelectedChange(sender, e);

                if (Session["sf_type"].ToString() == "2" || Session["sf_type"].ToString() == "1")
                {
                    FillMRManagers();
                }
            }
            else
            {
                // Fetch Managers Audit Team
                DataTable dt = sf.getAuditManagerTeam(div_code, sfCode, 0);
                dsmgrsf.Tables.Add(dt);
                dsTP = dsmgrsf;

                ddlFieldForce.DataTextField = "sf_name";
                ddlFieldForce.DataValueField = "sf_code";
                ddlFieldForce.DataSource = dsTP;
                ddlFieldForce.DataBind();

                ddlSF.DataTextField = "desig_Color";
                ddlSF.DataValueField = "sf_code";
                ddlSF.DataSource = dsTP;
                ddlSF.DataBind();
                ddlFFType.Visible = false;
            }
            hypConsolidate.Visible = false;
            chkConsolidate.Checked = false;          

        }

        if (Session["sf_type"].ToString() == "2")
        {
            //UserControl_MGR_Menu c1 =
            //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //c1.Title = "TP View";
            //c1.FindControl("btnBack").Visible = false;
            //ddlFilterby.SelectedItem.Value = sfCode;
            ddlFilterby.Visible = false;
            lblFilterby.Visible = false;           
        }
        else if (Session["sf_type"].ToString() == "1")
        {
            //UserControl_MR_Menu c1 =
            //(UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = "TP View";
            ddlFilterby.SelectedItem.Value = sfCode;
            ddlFilterby.Visible = false;
            lblFilterby.Visible = false;
            ddlFFType.Visible = false;
            ddlAlpha.Visible = false;
        }
        else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
           // UserControl_MenuUserControl c1 =
           //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
           // Divid.Controls.Add(c1);
           // c1.Title = "TP View";
           // c1.FindControl("btnBack").Visible = false;
             sfCode=ddlFilterby.SelectedItem.Value;
        }
        FillColor();
       
    }

    protected void TourPlan()
    {
        try
        {

           

            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                strTPView = dsTerritory.Tables[0].Rows[0]["No_of_TP_View"].ToString();

                if (strTPView == "2")
                {
                    // grdTP.Columns[5].Visible = false;
                }
                else if (strTPView == "1")
                {
                    // grdTP.Columns[4].Visible = false;
                    // grdTP.Columns[5].Visible = false;
                }
                else if (strTPView == "0" || strTPView == "")
                {
                    // grdTP.Columns[3].Visible = false;
                    // grdTP.Columns[4].Visible = false;
                    // grdTP.Columns[5].Visible = false;
                }
            }

        }
        catch (Exception ex)
        {

        }
    }

    private void FillReporting()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getUserList_Reporting(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFilterby.DataTextField = "Sf_Name";
            ddlFilterby.DataValueField = "Sf_Code";
            ddlFilterby.DataSource = dsSalesForce;
            ddlFilterby.DataBind();

            //ddlFilterbyColor.DataTextField = "des_color";
            //ddlFilterbyColor.DataValueField = "sf_code";
            //ddlFilterbyColor.DataSource = dsSalesForce;
            //ddlFilterbyColor.DataBind();

        }
    }



    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
           // ddlFilterby.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            //ddlFieldForce.Items[j].Selected = true;

            //if (ColorItems.Text == "Level1")
            //    //ColorItems.Attributes.Add("style", "background-color: Wheat");
            //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Wheat");

            //if (ColorItems.Text == "Level2")
            //    //ColorItems.Attributes.Add("style", "background-color: Blue");
            //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color: LightGreen");

            //if (ColorItems.Text == "Level3")
            //    //ColorItems.Attributes.Add("style", "background-color: Cyan");
            //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Pink");

            //if (ColorItems.Text == "Level4")
            //    //ColorItems.Attributes.Add("style", "background-color: Lavendar");
            //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Lavendar");

            j = j + 1;

        }
    }


    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "0")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserListTP_Hierarchy(div_code, "admin");
            ddlSF.DataTextField = "sf_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }
        else if(ddlFFType.SelectedValue.ToString() == "1")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserListTP_Alpha(div_code, ddlFilterby.SelectedValue.ToString());
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
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            //dsSalesForce = sf.UserList_HQ(div_code, "admin");
        }


    }

    private void FillSFMGR_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.TPviewGetAlphapetMgr(div_code, sfCode);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "sf_name";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }
    private void FillSF_Alpha()
    {

        string sReport = ddlFilterby.SelectedValue.ToString();
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.getSalesForcelist_Reporting(div_code, sReport);
        dsSalesForce = sf.TPviewGetAlphapet(div_code, sReport);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "FirstName";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            FillSFMGR_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.SalesForceList(div_code, sfCode);

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
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            //FillSF_Alpha();
            //ddlAlpha.Visible = true;
            //dsSalesForce = sf.UserListTP_Alpha(div_code, "admin");
            ddlAlpha.Visible = false;
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sfCode);
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
        }
        FillColor();
        
    }

    private void FillDivision()
    {
        DataSet dsDivision = new DataSet();
        Division dv = new Division();
        dsDivision = dv.getTPviewDivision();
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            //ddlDivision.DataTextField = "Division_Name";
            //ddlDivision.DataValueField = "Division_Code";
            //ddlDivision.DataSource = dsDivision;
            //ddlDivision.DataBind();
            //ddlDivision.SelectedIndex = 0;
        }

    }

    protected string LoadReport()
    {
        return "rptTPView.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&cur_month=" + ddlMonth.SelectedValue.ToString() + "&cur_year=" + ddlYear.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString();
        //string sURL = "rptTPView.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&cur_month=" + ddlMonth.SelectedValue.ToString() + "&cur_year=" + ddlYear.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString();
        //string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=800,left=100,top=100');";
        //ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);        
    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillManagers();
        FillMRManagers();
        //FillColor();
    }
    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Session["sf_type"].ToString() == "")
        {
            SalesForce sf = new SalesForce();
            if (ddlAlpha.SelectedItem.Text != "All")
            {
                dsSalesForce = sf.UserListTP_Alphasearch(div_code, ddlFilterby.SelectedValue.ToString(), ddlAlpha.SelectedValue);
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
                    FillColor();
                }
            }
            else
            {
                ddlFilterBy_SelectedChange(sender, e);
            }
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            SalesForce sf = new SalesForce();
            if (ddlAlpha.SelectedItem.Text != "All")
            {
                dsSalesForce = sf.UserListTP_AlphasearchMgr(div_code, sfCode, ddlAlpha.SelectedValue);
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
                    FillColor();

                }
                
            }
            else
            {
                ddlFilterBy_SelectedChange(sender, e);
            }
        }
 

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //FillTourPlan();
        FillColor();
        if (rdoHypLevel.SelectedIndex >= 0)
        {
            string sURL = "rptTPView.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&cur_month=" + ddlMonth.SelectedValue.ToString() + "&cur_year=" + ddlYear.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString()
                          + "&level=" + rdoHypLevel.SelectedValue.ToString() + "&div_Code=" + div_code;
            string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "openwindow", newWin, true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}', 'Cliente', 'toolbar=no,directories=no,status=no,menubar=no, scrollbars=yes,resizable=no', '800', '600','0','0', 'true'); </script>", sURL));
        }
       // else
       // {
        //    string sURL = "rptTPView.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&cur_month=" + ddlMonth.SelectedValue.ToString() + "&cur_year=" + ddlYear.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString() + "&level=-1"
          //                + "&div_Code=" + div_code;
         //   string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
         //   Page.ClientScript.RegisterStartupScript(this.GetType(), "openwindow", newWin, true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}', 'Cliente', 'toolbar=no,directories=no,status=no,menubar=no, scrollbars=yes,resizable=no', '800', '600','0','0', 'true'); </script>", sURL));
        //}
    }

    protected void chkConsolidate_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlSF.SelectedItem.Text.ToString() != "1")
        {
            if (chkConsolidate.Checked)
            {
                hypConsolidate.Visible = true;
            }
            else
            {
                hypConsolidate.Visible = false;
            }
        }
    }
    protected void ddlFilterBy_SelectedChange(object sender, EventArgs e)
    {
        ddlAlpha.Visible = false;
        ddlFFType.SelectedIndex = 0;
        string sReport = ddlFilterby.SelectedValue.ToString();
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceList(div_code, sReport);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.Visible = true;
            ddlFieldForce.DataTextField = "sf_Name";
            ddlFieldForce.DataValueField = "sf_Code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
            FillColor();
            
        }
        else
        {
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }
}