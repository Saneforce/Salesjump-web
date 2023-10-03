using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;

public partial class Reports_CallAverage : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;

    DataSet dsSalesForce = new DataSet();
    DataSet dsdiv = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                Filldiv();
                //Menu1.Title = Page.Title;
                //Menu1.FindControl("btnBack").Visible = false;
                //string str = DateTime.Now.AddMonths(-1).Month.ToString();
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();                
                ddlFrmMonth.SelectedValue = DateTime.Now.AddMonths(-1).Month.ToString();
                ddlToMonth.SelectedValue = DateTime.Now.Month.ToString();
              //  ddlDivision.SelectedIndex = 1;               
                ddlOption.SelectedIndex = 1;
                ddlOption_SelectedIndexChanged1(sender, e);
                ddlDivision_SelectedIndexChanged(sender, e);
                ddlFieldForce.SelectedIndex = 1;

                DataSet dsTP = new DataSet();
                DataSet dsmgrsf = new DataSet();
                SalesForce sf = new SalesForce();
                if (Session["sf_type"].ToString() == "3")
                {
                    FillManagers();
                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
                    if (DsAudit.Tables[0].Rows.Count > 0)
                    {
                        FillMGRLogin();
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

                    }
                }
                else if (Session["sf_type"].ToString() == "1")
                {
                    FillManagers();
                }
            }           

            if (Session["sf_type"].ToString() == "1")
            {
                //UserControl_MR_Menu c1 =
                //(UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                //Divid.Controls.Add(c1);
                //c1.FindControl("btnBack").Visible = false;
                //c1.Title = Page.Title;
                ddlDivision.Visible = false;
                lblDivision.Visible = false;
                ddlFieldForce.Visible = false;
                lblFilter.Visible = false;               
            }
            else if (Session["sf_type"].ToString() == "")
            {
                //UserControl_pnlMenu c1 =
               //(UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
                //Divid.Controls.Add(c1);
                //c1.FindControl("btnBack").Visible = false;
                //c1.Title = Page.Title;
            }
            else if (Session["sf_type"].ToString() == "3")
            {
                //UserControl_pnlMenu c1 =
                    //(UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
                //Divid.Controls.Add(c1);
                //c1.FindControl("btnBack").Visible = false;
                //c1.Title = Page.Title;
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                //UserControl_MGR_Menu c1 =
                //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                //Divid.Controls.Add(c1);
                //c1.FindControl("btnBack").Visible = false;
                //c1.Title = Page.Title;
                ddlDivision.Visible = false;
                lblDivision.Visible = false;
                      
            }

            FillColor();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('"+ ex.Message +"') </script>");
        }
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

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.getUserList_Reporting(ddlDivision.SelectedValue.ToString());
        if (ddlOption.Text == "MonthWise")
        {
            dsSalesForce = sf.UserList_Hierarchy(ddlDivision.SelectedValue.ToString(), "admin");
        }
        else if (ddlOption.Text == "Periodically")
        {           
            dsSalesForce = sf.sp_UserList_NameHierarchy(ddlDivision.SelectedValue.ToString(), "admin");
        }
        else if (ddlOption.Text == "Periodically All Field Force")
        {
            dsSalesForce = sf.sp_UserList_NameHierarchy(ddlDivision.SelectedValue.ToString(), "admin");
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
        try
        {
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();

            dsTP = tp.Get_TP_Edit_Year(ddlDivision.SelectedValue.ToString());
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlFrmYear.Items.Add(k.ToString());
                    ddlToYear.Items.Add(k.ToString());
                }
                ddlYear.Text = DateTime.Now.Year.ToString();
                ddlFrmYear.Text = DateTime.Now.Year.ToString();
                ddlToYear.Text = DateTime.Now.Year.ToString();
            }
            FillManagers();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string sURL = "";

            if (ddlOption.Text == "MonthWise")
            {
                sURL = "rptCallAverage.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&cur_month=" + ddlMonth.SelectedValue.ToString() + "&cur_year=" + ddlYear.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString()
                             + "&level=" + ddlOption.SelectedValue.ToString() + "&div_Code=" + ddlDivision.SelectedValue.ToString() + "&Mode=" + ddlOption.Text + "";
            }
            else if (ddlOption.Text == "Periodically")
            {
                sURL = "rptCallAverage.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&frm_month=" + ddlFrmMonth.SelectedValue.ToString() + "&frm_year=" + ddlFrmYear.SelectedValue.ToString()
                      + "&To_Month=" + ddlToMonth.SelectedValue.ToString() + "&To_Year=" + ddlToYear.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString()
                      + "&level=" + ddlOption.SelectedValue.ToString() + "&div_Code=" + ddlDivision.SelectedValue.ToString() + "&Mode=" + ddlOption.Text + "";
            }
            else if (ddlOption.Text == "Periodically All Field Force")
            {
                sURL = "rptCallAverage.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() + "&frm_month=" + ddlFrmMonth.SelectedValue.ToString() + "&frm_year=" + ddlFrmYear.SelectedValue.ToString()
                      + "&To_Month=" + ddlToMonth.SelectedValue.ToString() + "&To_Year=" + ddlToYear.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString()
                      + "&level=" + ddlOption.SelectedValue.ToString() + "&div_Code=" + ddlDivision.SelectedValue.ToString() + "&Mode=" + ddlOption.Text + "";
            }

            string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=600,left=0,top=0');";
            ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlOption_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlOption_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ddlOption.Text == "MonthWise")
            {
                pnlMonthly.Visible = true;
                pnlPeriodically.Visible = false;
            }
            else if (ddlOption.Text == "Periodically")
            {
                pnlMonthly.Visible = false;
                pnlPeriodically.Visible = true;
            }
            else if (ddlOption.Text == "Periodically All Field Force")
            {
                pnlMonthly.Visible = false;
                pnlPeriodically.Visible = true;
            }


            //if (Session["sf_type"].ToString() == "2")
            //{
            //    FillMGRLogin();
            //}
            //else
            //{
            //    FillManagers();
            //}
        }
        catch (Exception ex)
        {

        }
    }
}