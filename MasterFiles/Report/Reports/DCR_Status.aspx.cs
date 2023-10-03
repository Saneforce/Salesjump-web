using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Reports_DCR_Status : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    DataSet dsDivision = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
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

        if (Session["sf_type"].ToString() == "1")
        {
            //UserControl_MR_Menu c1 =
            //    (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            ViewState["sf_type"] = "";
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
            lblDivision.Visible = false;
            ddlDivision.Visible = false;
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            //UserControl_MGR_Menu c1 =
            //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            ViewState["sf_type"] = "";
            if (!Page.IsPostBack)
            {
                 DataSet dsmgrsf = new DataSet();
                SalesForce sf = new SalesForce();
                DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
                    FillMRManagers();
                    ddlFieldForce.SelectedValue = sf_code;
                }
                else
                {
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
            lblDivision.Visible = false;
            ddlDivision.Visible = false;
        }
        else
        {
            ViewState["sf_type"] = "admin";
            //UserControl_MenuUserControl c1 =
            //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            if (!Page.IsPostBack)
            {
                //Filldiv();
                FillManagers();
                ddlFieldForce.SelectedIndex = 1;
            }
        }

        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            //ddlFieldForce.SelectedIndex = 1;
            chkDetail.Checked = false;

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
        rdoType.SelectedValue = "1";
        txtEffFrom.Text = "";
        FillColor();
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
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["sf_type"].ToString() == "admin")
        {

            SalesForce sf = new SalesForce();
            dsSalesForce = sf.UserList_getMR(div_code, ddlFieldForce.SelectedValue.ToString());
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
               // lblMR.Visible = true;
               // ddlMR.Visible = true;
                //ddlMR.DataTextField = "sf_name";
                //ddlMR.DataValueField = "sf_code";
                //ddlMR.DataSource = dsSalesForce;
                //ddlMR.DataBind();
                //ddlMR.Items.Insert(0, new ListItem("---Select---", "0"));
            }
        }
    }
   
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
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
            //ddlFieldForce.Items[j].Selected = true;

            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }
   
    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Alphasearch(ddlDivision.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
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

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
       
        //Response.Redirect("rptDCRStatus.aspx?div_code=" + div_code  + "&sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&cmon=" + ddlMonth.SelectedValue.ToString() + "&cyear=" + ddlYear.SelectedItem.Text.Trim() );
        SalesForce sf = new SalesForce();
        dsSf = sf.CheckSFType(ddlFieldForce.SelectedValue.ToString());
        if (dsSf.Tables[0].Rows.Count > 0)
        {
            if (ViewState["sf_type"].ToString() != "admin")
                sf_type = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        //if (ViewState["sf_type"] == "admin")
        //{

        //    if (chkDetail.Checked == false)
        //    {
        //        string sURL = "rptDCRStatus.aspx?div_code=" + ddlDivision.SelectedValue + "&cmon=" + ddlMonth.SelectedValue.ToString() + "&cyear=" + ddlYear.SelectedItem.Text.Trim();
        //        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=800,left=0,top=0');";
        //        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
        //    }
        //    else
        //    {
        //        string sURL = "rptDCR_Status_Detailed.aspx?div_code=" + ddlDivision.SelectedValue + "&cmon=" + ddlMonth.SelectedValue.ToString() + "&cyear=" + ddlYear.SelectedItem.Text.Trim();
        //        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=800,left=0,top=0');";
        //        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
        //    }
        //}
        if (ViewState["sf_type"].ToString() == "admin")
        {
            if (chkDetail.Checked == false)
            {
                string sURL = "New_rpt_DCR_Status.aspx?div_code=" + div_code + "&sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.ToString() + "&cmon=" + ddlMonth.SelectedValue.ToString() + "&cyear=" + ddlYear.SelectedItem.Text.Trim();
                string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=800,left=0,top=0');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }
            else
            {
                string sURL = "New_rptDCR_Status_Detailed.aspx?div_code=" + div_code + "&sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.ToString() + "&cmon=" + ddlMonth.SelectedValue.ToString() + "&cyear=" + ddlYear.SelectedItem.Text.Trim();
                string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=800,left=0,top=0');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }
        }
        else 
        {
            if (chkDetail.Checked == false)
            {
                string sURL = "rptDCRStatus.aspx?div_code=" + div_code + "&sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.ToString() + "&cmon=" + ddlMonth.SelectedValue.ToString() + "&cyear=" + ddlYear.SelectedItem.Text.Trim();
                string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=800,left=0,top=0');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }
            else
            {
                string sURL = "rptDCRStatus.aspx?div_code=" + div_code + "&sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.ToString() + "&cmon=" + ddlMonth.SelectedValue.ToString() + "&cyear=" + ddlYear.SelectedItem.Text.Trim();
                string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=800,left=0,top=0');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }
        }
    }
}