using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_TP_Deviation : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    int imissed_dr = -1;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
    int mode = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string strSf_Code = string.Empty;
    string Monthsub = string.Empty;
    DataSet dsSalesforce = null;
    DataSet dsDoctor = null;
    int tot_days = -1;
    int cday = 1;
    string sDCR = string.Empty;
    int ddate = 0;
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        if(!Page.IsPostBack)
        {
            //ServerStartTime = DateTime.Now;
            //base.OnPreInit(e);
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            //FillMR();
            //FillYear();
           
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
               // UserControl_MGR_Menu c1 =
               //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
               // Divid.Controls.Add(c1);
               // c1.Title = Page.Title;
               // c1.FindControl("btnBack").Visible = false;
                FillMRManagers("0");
                FillMRManagers_MR();
                FillYear();

            }

            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
               // UserControl_MR_Menu c1 =
               //(UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
               // Divid.Controls.Add(c1);
               // c1.Title = Page.Title;
               // c1.FindControl("btnBack").Visible = false;
               // txtNew.Visible = false;
                 FillMRManagers("0");
                FillMRManagers_MR();
                FillYear();
            }

            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
               // UserControl_MenuUserControl c1 =
               //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
               // Divid.Controls.Add(c1);
               // c1.Title = Page.Title;
               // c1.FindControl("btnBack").Visible = false;
                 FillMRManagers("0");
                FillMRManagers_MR();
                FillYear();
            }
      
        }

        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
               // UserControl_MGR_Menu c1 =
               //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
               // Divid.Controls.Add(c1);
               // c1.Title = Page.Title;
               // c1.FindControl("btnBack").Visible = false;

            }

            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
               // UserControl_MR_Menu c1 =
               //(UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
               // Divid.Controls.Add(c1);
               // c1.Title = Page.Title;
               // c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
               // UserControl_MenuUserControl c1 =
               //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
               // Divid.Controls.Add(c1);
               // c1.Title = Page.Title;
               // c1.FindControl("btnBack").Visible = false;

            }


        }
        FillColor();
    }

    private void FillYear()
    {
        //TourPlan tp = new TourPlan();
        //dsTP = tp.Get_TP_Edit_Year(div_code);
        //if (dsTP.Tables[0].Rows.Count > 0)
        //{
        //    for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
        //    {
        //        ddlYear.Items.Add(k.ToString());
        //        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        //    }
        //}
        //ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
    }

    private void FillMR()
    {
        SalesForce sf = new SalesForce();
        dsSalesforce = sf.SalesForceList_New_GetMr(div_code, sf_code);
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();

        }
    }
private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code, Sub_Div_Code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("---Select Manager ---", "0"));
        }
    }

    private void FillMRManagers_MR()
    {
        SalesForce sf = new SalesForce();
        //  ddlFFType.Visible = false;
        //ddlAlpha.Visible = false;        
        dsSalesForce = sf.UserList_getMR(div_code, ddlFieldForce.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlMR.DataTextField = "sf_name";
            ddlMR.DataValueField = "sf_code";
            ddlMR.DataSource = dsSalesForce;
            ddlMR.DataBind();
            ddlMR.Items.Insert(0, new ListItem("---Select Base Level---", "0"));



        }
    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_getMR(div_code, ddlFieldForce.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlMR.DataTextField = "sf_name";
            ddlMR.DataValueField = "sf_code";
            ddlMR.DataSource = dsSalesForce;
            ddlMR.DataBind();
            ddlMR.Items.Insert(0, new ListItem("---Select Base Level---", "0"));

        }
        else
        {
            ddlMR.DataSource = null;
            ddlMR.Items.Clear();
            ddlMR.Items.Insert(0, new ListItem("---Select Base Level---", "0"));

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

    protected void btnGo_Click(object sender, EventArgs e)
    {
        string sURL = string.Empty;

        sURL = "rptTP_Deviation.aspx?div_code=" + div_code.ToString() + "&sfcode=" + ddlFieldForce.SelectedValue + "&FDate=" + txtFromDate.ToString() + "&TDate=" + txtToDate.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.ToString();
        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    }
}