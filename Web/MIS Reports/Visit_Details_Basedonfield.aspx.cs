using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MIS_Reports_Visit_Details_Basedonfield : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsmgrsf = new DataSet();
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    int mode = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
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
            ddlMode.SelectedIndex = 1;
            ddlMode.Enabled = false;
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            ViewState["sf_type"] = "";
            if (!Page.IsPostBack)
            {
                FillMRManagers();
                ddlFieldForce.SelectedValue = sf_code;
            }
        }
        else
        {
            ViewState["sf_type"] = "admin";
            UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            if (!Page.IsPostBack)
            {
                FillManagers();
            }
        }


        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlFYear.Items.Add(k.ToString());
                    ddlTYear.Items.Add(k.ToString());
                    ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
                    ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }

            ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
            ddlTMonth.SelectedValue = DateTime.Now.Month.ToString();
           
           
           
        }
        FillColor();

    }

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        ddlFFType.Visible = false;
        ddlAlpha.Visible = false;
         DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
         if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
         {
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

             ddlSF.DataTextField = "Des_Color";
             ddlSF.DataValueField = "sf_code";
             ddlSF.DataSource = dsTP;
             ddlSF.DataBind();

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
        int FYear = Convert.ToInt32(ddlFYear.SelectedValue);
        int TYear = Convert.ToInt32(ddlTYear.SelectedValue);
        int FMonth = Convert.ToInt32(ddlFMonth.SelectedValue);
        int TMonth = Convert.ToInt32(ddlTMonth.SelectedValue);
        if (FMonth > TMonth && TYear == FYear)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Month must be greater than From Month');</script>");
            ddlTMonth.Focus();
        }
        else if (FYear > TYear)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Year must be greater than From Year');</script>");
            ddlTYear.Focus();
        }
        else
        {
            if (FYear <= TYear)
            {
                string sURL = string.Empty;
                if (ddlMode.SelectedIndex == 1)
                {
                    if (ddlMR.SelectedIndex > 0)
                    {
                        sURL = "Visit_Details_Basedonfield_Level1.aspx?sfcode=" + ddlMR.SelectedValue.ToString() + "&FMonth=" + ddlFMonth.SelectedValue.ToString() + "&FYear=" + ddlFYear.SelectedValue.ToString() + "&TMonth=" + ddlTMonth.SelectedValue.ToString() + "&TYear=" + ddlTYear.SelectedValue.ToString() + "&Type=" + ddlType.SelectedValue.ToString() + "&cMode=" + ddlMode.SelectedValue.ToString();
                    }
                    else
                    {
                        sURL = "Visit_Details_Basedonfield_Level1.aspx?sfcode=" + ddlFieldForce.SelectedValue.ToString() + "&FMonth=" + ddlFMonth.SelectedValue.ToString() + "&FYear=" + ddlFYear.SelectedValue.ToString() + "&TMonth=" + ddlTMonth.SelectedValue.ToString() + "&TYear=" + ddlTYear.SelectedValue.ToString() + "&Type=" + ddlType.SelectedValue.ToString() + "&cMode=" + ddlMode.SelectedValue.ToString();
                    }
                }
                else
                {
                    sURL = "Visit_Details_Basedonfield_Level1.aspx?sfcode=" + ddlFieldForce.SelectedValue.ToString() + "&FMonth=" + ddlFMonth.SelectedValue.ToString() + "&FYear=" + ddlFYear.SelectedValue.ToString() + "&TMonth=" + ddlTMonth.SelectedValue.ToString() + "&TYear=" + ddlTYear.SelectedValue.ToString() + "&Type=" + ddlType.SelectedValue.ToString() + "&cMode=" + ddlMode.SelectedValue.ToString();
                }

                string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=900,height=600,left=0,top=0');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }
        }
    }

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        
         SalesForce sf = new SalesForce();
         if (ViewState["sf_type"].ToString() == "admin")
         {
              DataSet DsAudit = sf.SF_Hierarchy(div_code, ddlFieldForce.SelectedValue);
              if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
              {
                  dsSalesForce = sf.UserList_getMR(div_code, ddlFieldForce.SelectedValue.ToString());
                  if (dsSalesForce.Tables[0].Rows.Count > 0)
                  {
                      lblMR.Visible = true;
                      ddlMR.Visible = true;
                      ddlMR.DataTextField = "sf_name";
                      ddlMR.DataValueField = "sf_code";
                      ddlMR.DataSource = dsSalesForce;
                      ddlMR.DataBind();
                      ddlMR.Items.Insert(0, new ListItem("---Select---", "0"));
                  }
              }
              else
              {
                  // Fetch Managers Audit Team
                  DataTable dt = sf.getAuditManagerTeam(div_code, ddlFieldForce.SelectedValue.ToString(), 0);

                  dsmgrsf.Tables.Add(dt);
                  dsmgrsf.Tables[0].Rows[0].Delete();
                  dsTP = dsmgrsf;

                  lblMR.Visible = true;
                  ddlMR.Visible = true;

                  ddlMR.DataTextField = "sf_name";
                  ddlMR.DataValueField = "sf_code";
                  ddlMR.DataSource = dsTP;
                  ddlMR.DataBind();
                  ddlMR.Items.Insert(0, new ListItem("---Select---", "0"));
              }
         }
         dsSf = sf.CheckSFType(ddlFieldForce.SelectedValue.ToString());
         if (dsSf.Tables[0].Rows.Count > 0)
         {
             if (ViewState["sf_type"].ToString() != "admin")
                 sf_type = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
         }

         if ((ViewState["sf_type"].ToString() == "admin" && ddlMR.SelectedIndex > 0) || (sf_type == "1"))
         {
             ddlMode.SelectedIndex = 1;
             ddlMode.Enabled = false;
         }
         else
         {
             ddlMode.SelectedIndex = 0;
             ddlMode.Enabled = true;
         }
    }

    protected void ddlMR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMR.SelectedIndex > 0)
        {
            ddlMode.SelectedIndex = 1;
            ddlMode.Enabled = false;
        }
        else
        {
            ddlMode.SelectedIndex = 0;
            ddlMode.Enabled = true;
        }
    }
}