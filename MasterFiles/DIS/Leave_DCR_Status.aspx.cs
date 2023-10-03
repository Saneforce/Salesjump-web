using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_Leave_DCR_Status : System.Web.UI.Page
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

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            menu1.FindControl("btnBack").Visible = false;
            FillManagers();
            FillYear();
        }
        FillColor();

    }

    private void FillYear()
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

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        //if (ddlFFType.SelectedValue.ToString() == "1")
        //{
        //    ddlAlpha.Visible = false;
            dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        //}
        //else if (ddlFFType.SelectedValue.ToString() == "0")
        //{
        //    FillSF_Alpha();
        //    ddlAlpha.Visible = true;
        //    dsSalesForce = sf.UserList_Alpha(div_code, "admin");
        //}

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

    //private void FillSF_Alpha()
    //{
    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlAlpha.DataTextField = "sf_name";
    //        ddlAlpha.DataValueField = "val";
    //        ddlAlpha.DataSource = dsSalesForce;
    //        ddlAlpha.DataBind();
    //        ddlAlpha.SelectedIndex = 0;
    //    }
    //}

    //protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.UserList_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlFieldForce.DataTextField = "sf_name";
    //        ddlFieldForce.DataValueField = "sf_code";
    //        ddlFieldForce.DataSource = dsSalesForce;
    //        ddlFieldForce.DataBind();

    //        ddlSF.DataTextField = "des_color";
    //        ddlSF.DataValueField = "sf_code";
    //        ddlSF.DataSource = dsSalesForce;
    //        ddlSF.DataBind();

    //    }
    //    FillColor();

    //}

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFF.Text = "Field Force";
        FillManagers();
        FillColor();
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
    //protected void btnGo_Click(object sender, EventArgs e)
    //{
    //    int FYear = Convert.ToInt32(ddlFYear.SelectedValue);
    //    int TYear = Convert.ToInt32(ddlTYear.SelectedValue);
    //    int FMonth = Convert.ToInt32(ddlFMonth.SelectedValue);
    //    int TMonth = Convert.ToInt32(ddlTMonth.SelectedValue);

    //    if (FMonth > TMonth && TYear == FYear)
    //    {
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Month must be greater than From Month');</script>");
    //        ddlTMonth.Focus();
    //    }
    //    else if (FYear > TYear)
    //    {
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Year must be greater than From Year');</script>");
    //        ddlTYear.Focus();
    //    }

    //    else
    //    {

    //        if (FYear <= TYear)
    //        {
    //            string sURL = string.Empty;
    //            if (ddlFieldForce.SelectedIndex > 0)
    //            {

    //                if (chkDetail.Checked == false)
    //                {

    //                    sURL = "rptLeave_DCR_Status.aspx?sfcode=" + ddlFieldForce.SelectedValue.ToString() + "&FMonth=" + ddlFMonth.SelectedValue.ToString() + "&FYear=" + ddlFYear.SelectedValue.ToString() + "&TMonth=" + ddlTMonth.SelectedValue.ToString() + "&TYear=" + ddlTYear.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString() + "&Detailed=" + "0";
    //                }
    //                else
    //                {
    //                    sURL = "rptLeave_DCR_Status.aspx?sfcode=" + ddlFieldForce.SelectedValue.ToString() + "&FMonth=" + ddlFMonth.SelectedValue.ToString() + "&FYear=" + ddlFYear.SelectedValue.ToString() + "&TMonth=" + ddlTMonth.SelectedValue.ToString() + "&TYear=" + ddlTYear.SelectedValue.ToString() + "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString() + "&Detailed=" + "1";
    //                }

    //            }

    //            string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
    //            ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    //        }
    //    }
    //}
}