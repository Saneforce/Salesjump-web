    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;

public partial class Reports_frmWorkTypeStatusView : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
     
        if (!Page.IsPostBack)
        {
            //Menu1.Title = Page.Title;
            //Menu1.FindControl("btnBack").Visible = false;
            
           
            Filldiv();            
            FillMRManagers();            
            
            ddlFieldForce.SelectedIndex = 1;
            ddlDivision_SelectedIndexChanged(sender, e);
            BindDate();
            chkWorkType.Focus();

            if (Session["sf_type"].ToString() == "1")
            {
                FillWorkType(div_code);
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                 DataSet dsmgrsf = new DataSet();
                SalesForce ds = new SalesForce();
                FillWorkType(div_code);
                DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
                if (DsAudit.Tables[0].Rows.Count > 0)
                {
                    FillMGRLogin();
                }
                else
                {
                    DataTable dt = ds.getAuditManagerTeam(div_code, sf_code, 0);
                    dsmgrsf.Tables.Add(dt);
                    dsSalesForce = dsmgrsf;

                    ddlFieldForce.DataTextField = "sf_name";
                    ddlFieldForce.DataValueField = "sf_code";
                    ddlFieldForce.DataSource = dsSalesForce;
                    ddlFieldForce.DataBind();

                    ddlSF.DataTextField = "desig_Color";
                    ddlSF.DataValueField = "sf_code";
                    ddlSF.DataSource = dsSalesForce;
                    ddlSF.DataBind();
                }
            }
            else if (Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == "")
            {
                FillWorkType(ddlDivision.SelectedValue);
            }
        }

        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
            (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = Page.Title;
            ddlDivision.Visible = false;
            lblDivision.Visible = false;          
            ddlFieldForce.Visible = false;
            lblFilter.Visible = false;
            ddlDivision.SelectedIndex = 1;
            
            //btnSubmit_Click(sender, e);
            //lblFF.Visible = false;
        }
        else if (Session["sf_type"].ToString() == "")
        {
            UserControl_pnlMenu c1 =
           (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = Page.Title;
           
        }
        else if (Session["sf_type"].ToString() == "3")
        {
            UserControl_pnlMenu c1 =
                (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = Page.Title;
            
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = Page.Title;         
            ddlDivision.Visible = false;
            lblDivision.Visible = false;
            

        }
        FillColor();
    }

    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }

            }

        }
        else
        {
            DataSet dsDivision = new DataSet();

            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
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
           

        }
    }

    private void FillWorkType(string div_Code)
    {
        DCR dcrWorkType = new DCR();
        DataSet dsChkWT = new DataSet();
        dsChkWT = dcrWorkType.DCR_get_WorkType(div_Code);
        chkWorkType.DataSource = dsChkWT;
        chkWorkType.DataTextField = "Worktype_Name_B";
        chkWorkType.DataValueField = "WorkType_Code_B";
        chkWorkType.DataBind();
    }

   

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Hierarchy(ddlDivision.SelectedValue.ToString(), "admin");
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
    

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillMRManagers();
    }
    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(ddlDivision.SelectedValue.ToString());
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFrmYear.Items.Add(k.ToString());
                ddlToYear.Items.Add(k.ToString());
            }

            ddlFrmYear.Text = DateTime.Now.Year.ToString();
            ddlToYear.Text = DateTime.Now.Year.ToString();

            ddlFrmMonth.SelectedValue = DateTime.Today.AddMonths(-1).Month.ToString();
            ddlToMonth.SelectedValue = DateTime.Now.Month.ToString();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string strWorkTypeName = "";

            for (int iIndex = 0; iIndex < chkWorkType.Items.Count; iIndex++)
            {
                //chkWorkType.Text = "";
                //Chkweek.Items[iIndex].Selected = true;

                if (chkWorkType.Items[iIndex].Selected == true)
                {
                    //strWorkTypeName += "'" + chkWorkType.Items[iIndex].Text + "'" + ",";
                    strWorkTypeName += chkWorkType.Items[iIndex].Value + ",";
                }
            }

            strWorkTypeName = strWorkTypeName.Remove(strWorkTypeName.Length - 1);

            string sURL = "";

            sURL = "rptWrkTypeViewStatus.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() +
                   "&Frm_year=" + ddlFrmYear.SelectedValue.ToString() + " &Frm_Month=" + ddlFrmMonth.SelectedValue.ToString() +
                   "&To_year=" + ddlToYear.SelectedValue.ToString() + " &To_Month=" + ddlToMonth.SelectedValue.ToString() +
                   "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString() +
                   "&ChkWorkType=" + strWorkTypeName +
                   "&div_Code=" + ddlDivision.SelectedValue.ToString() + "";

            string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=600,left=0,top=0');";
            ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        for (int iIndex = 0; iIndex < chkWorkType.Items.Count; iIndex++)
        {
            chkWorkType.Items[iIndex].Selected = false;
        }

        ddlFieldForce.SelectedIndex = -1;
        ddlFrmYear.SelectedIndex = -1;
        ddlToYear.SelectedIndex = -1;
        ddlFrmMonth.SelectedIndex = -1;
        ddlToMonth.SelectedIndex = -1;
    }
}