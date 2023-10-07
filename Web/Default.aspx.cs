using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class _Default : System.Web.UI.Page
{
    #region Declaration 
    DataSet dsDivision = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsAdmin = null;
    DataSet dsAdmin1 = null;
    DataSet dsdiv = null;

    DataSet dsFlash = null;
    //added by sri for HO Users
    string sf_type = string.Empty;
    string HO_ID = string.Empty;
    string division_code = string.Empty;
    string div_codeadm = string.Empty;
    string Flash = string.Empty;
    string FlashNews = string.Empty;
    int time;
    string Support = string.Empty;
    string SupportName = string.Empty;
    int div_code;
    public static string baseUrl = "";
    #endregion

    #region Page_Load 
    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["sf_type"]) != null || Convert.ToString(Session["sf_type"]) != ""))
        {
            System.Threading.Thread.Sleep(time);
            //added by sri for HO Users
            sf_type = Session["sf_type"].ToString();
            HO_ID = Session["HO_ID"].ToString();
            if (sf_type == "3")
            {
                division_code = Session["division_code"].ToString();
            }
            else
            {
                division_code = Session["div_code"].ToString();
            }

            if (!Page.IsPostBack)
            {
                btnSelect.Focus();
                MailCount();
                FillDivision();
                menu.FindControl("pnlHeader").Visible = false;
                dd1division.SelectedIndex = 0;
                ServerStartTime = DateTime.Now;
                base.OnPreInit(e);
                string script = "$(document).ready(function () { $('[id*= menu]').click(); });";
                //  ddldivision_DataBound(sender, e);
                gettalk();
                AdminSetup adm1 = new AdminSetup();

                string[] strDivSplit1 = division_code.Split(',');
                foreach (string strdiv1 in strDivSplit1)
                {
                    if (strdiv1 != "")
                    {
                        dsFlash = adm1.Get_Flash_News_adm(strdiv1);

                        if (dsFlash.Tables[0].Rows.Count > 0)
                        {
                            Flash = dsFlash.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                            FlashNews = Flash;
                            lblFlash.Text += FlashNews + "&nbsp;&nbsp;| &nbsp; &nbsp;";
                        }

                    }
                }
            }
        }
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion

    #region FillDivision 
    private void FillDivision()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = division_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    dd1division.Items.Add(liTerr);
                }
            }
        }
        else if (sf_type == "" || sf_type == null || sf_type == "0")
        {
            dsDivision = dv.getDivision_list();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                dd1division.DataValueField = "Division_Code";
                dd1division.DataTextField = "Division_Name";
                dd1division.SelectedIndex = 0;

                dd1division.DataSource = dsDivision;
                dd1division.DataBind();
                //btnSelect.Visible = false;
                lblenter.Visible = false;
            }
        }
    }
    #endregion

    #region gettalk
    private void gettalk()
    {
        AdminSetup adm = new AdminSetup();
        string[] strDivSplit = division_code.Split(',');
        foreach (string strdiv in strDivSplit)
        {
            if (strdiv != "")
            {
                dsAdmin = adm.Get_talktous(strdiv);

                if (dsAdmin.Tables[0].Rows.Count > 0)
                {
                    Support = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    SupportName = Support;
                    lblSupport.Text += SupportName + "&nbsp;&nbsp;| &nbsp; &nbsp;";
                }

            }
        }
    }
    #endregion

    #region MailCount
    private void MailCount()
    {
        AdminSetup ad = new AdminSetup();
        DataSet dsMailCount = new DataSet();

        int count;
        count = ad.get_MailCount(division_code);
        if (count != 0)
        {
            lblNoMail.Visible = false;
            LnkNoMail.Visible = true;
            lblimg.Visible = true;
            LnkNoMail.Text = "You have " + count + " New Mail(s) in your Mail Box";
        }
        else
        {
            lblNoMail.Visible = true;
        }
    }
    #endregion

    #region ddldivision_DataBound
    protected void ddldivision_DataBound(object sender, EventArgs e)
    {
        ListBox list = sender as ListBox;

        if (list != null)
        {

            foreach (ListItem li in list.Items)
            {
                Division dv1 = new Division();
                dsDivision = dv1.Division_State(Convert.ToInt16(li.Value));
                li.Attributes["title"] = dsDivision.Tables[0].Rows[0]["statename"].ToString();
            }
        }
    }
    #endregion

    #region Page_PreRender
    protected void Page_PreRender(object sender, EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    #endregion

    #region TrackPageTime
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + serverTimeDiff.Ticks + "');</script>");
        time = serverTimeDiff.Minutes;

    }
    #endregion

    #region btnSelect_Click
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        // string[] str = dd1division.SelectedItem.Text.ToString().Split('.');
        Session["div_code"] = dd1division.SelectedItem.Value.ToString();
        Session["div_name"] = dd1division.SelectedItem.Text.ToString();
        Server.Transfer("BasicMaster.aspx");
    }
    #endregion

    #region ddldivision_OnSelectedIndexChanged
    protected void ddldivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion

}