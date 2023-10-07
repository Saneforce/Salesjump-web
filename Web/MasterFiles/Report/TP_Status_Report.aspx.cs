using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Reports_TP_Status_Report : System.Web.UI.Page
{

    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsSalesForce = null;
    DataSet dsProd = null;
    string sState = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
	string sf_type = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    protected void Page_PreInit(object sender, EventArgs e)
        {
           sf_type = Session["sf_type"].ToString();
           if (sf_type == "3")
           {
               this.MasterPageFile = "~/Master.master";
           }
           else if(sf_type == "2")
           {
               this.MasterPageFile = "~/Master_MGR.master";
           }
 	   else if(sf_type == "1")
           {
               this.MasterPageFile = "~/Master_MR.master";
           }
        }
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            Filldiv();
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }

                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            }
            //menu1.Title = this.Page.Title;            
            FillState(div_code);
            DataSet dsmgrsf = new DataSet();
            SalesForce sf = new SalesForce();
            DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
            ddlFFType.Visible = true;
            //FillManagers();
            if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
            {

                if (Session["sf_type"].ToString() == "2" || Session["sf_type"].ToString() == "1")
                {
                    FillMRManagers();
                }

                if (rdoMGRState.SelectedValue.ToString() == "0")
                {
                    lblState.Text = "FieldForce Name";
                    if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
                    {
                        FillManagers();
                        FillColor();
                        //ddlDivision.SelectedIndex = 1;
                        ddlDivision_SelectedIndexChanged(sender, e);
                        ddlFieldForce.SelectedIndex = 0;
                    }
                }
                else
                {
                    lblState.Text = "State";
                    FillState(div_code);
                }

                if (Session["sf_type"].ToString() == "2")
                {
                    FillMGRLogin();
                }
            }
            else
            {
                // Fetch Managers Audit Team
                DataTable dt = sf.getAuditManagerTeam(div_code, sf_code, 0);
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
           
         
        }
        if (Session["sf_type"].ToString() == "2")
        {
            //UserControl_MGR_Menu c1 =
            //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = "TP Status";

            //ddlDivision.Visible = false;
            //lblDivision.Visible = false;
            Label2.Visible = false;
            rdoMGRState.Visible = false;
        }
        else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            //UserControl_MenuUserControl c1 =
            //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = "TP Status";
        }
        else if (Session["sf_type"].ToString() == "1")
        {
            //UserControl_MR_Menu c1 =
            //(UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = "TP Status";            
            ddlFFType.Visible = false;
            ddlAlpha.Visible = false;
            //ddlDivision.Visible = false;
            //lblDivision.Visible = false;
            Label2.Visible = false;
            rdoMGRState.Visible = false;
        }

        if (rdoMGRState.SelectedItem.Value == "0")
        {
            ddlFieldForce.Visible = true;
            lblFieldforce.Visible = true;
            ddlState.Visible = false;
            lblState.Visible = false;
        }
        else
        {
            ddlFieldForce.Visible = false;
            lblFieldforce.Visible = false;
            ddlFFType.Visible = false;
            ddlState.Visible = true;
            lblState.Visible = true;
        }
        FillColor();
    }

    private void Filldiv()
    {
        Division dv = new Division();
        dsDivision = dv.getDivision_Name();
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            //ddlDivision.DataTextField = "Division_Name";
            //ddlDivision.DataValueField = "Division_Code";
            //ddlDivision.DataSource = dsDivision;
            //ddlDivision.DataBind();
        }
    }

    private void FillMGRLogin()
    {
        SalesForce sf = new SalesForce();
        if (Session["sf_type"].ToString() == "2")
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        }
        if (dsSalesForce.Tables[0].Rows.Count > 1)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();


            ddlSF.DataTextField = "desig_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
            FillColor();
        }
        else
        {

            dsSalesForce = sf.sp_UserMGRLogin(div_code, sf_code);

            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();


            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
            FillColor();
            ddlFFType.Visible = false;

            ddlFFType.Visible = false;

        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();

        //FillSalesForce();
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
        dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
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
        //FillColor();


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
        dsSalesForce = sf.getSalesForcelist_TP_Alphabet(div_code);
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
        if (ddlAlpha.SelectedIndex == 0)
        {
            dsSalesForce = sf.UserListTP_Alpha(div_code, "admin");
        }
        else
        {
            dsSalesForce = sf.UserListTP_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
        }

        //dsSalesForce = sf.UserList_Alphasearch(ddlDivision.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
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

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoMGRState.SelectedValue.ToString() == "0")
        {
            lblState.Text = "Field Force";
            FillManagers();
            FillColor();
        }
        else
        {
            lblState.Text = "State";
            FillState(div_code);
        }

    }

    protected void rdoMGRState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoMGRState.SelectedValue.ToString() == "0")
        {
            lblState.Text = "Field Force";
            FillManagers();
            FillColor();
            ddlFFType.Visible = true;
        }
        else
        {
            lblState.Text = "State";
            FillState(div_code);
            ddlFFType.Visible = false;
            ddlAlpha.Visible = false;
        }

    }

    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getStateName(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string sURL = "rptTPStatus.aspx?state_code=" + ddlState.SelectedValue.ToString() + "&vacant=" + chkVacant.Checked + "&cur_month=" + ddlMonth.SelectedValue.ToString() + "&cur_year=" + ddlYear.SelectedValue.ToString() +"&Option="+rdoMGRState.SelectedValue+
            "&sf_code=" + ddlFieldForce.SelectedValue + "&sf_name="+ddlFieldForce.SelectedItem.Text;
        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    }
}