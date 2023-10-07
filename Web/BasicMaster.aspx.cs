using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class BasicMaster : System.Web.UI.Page
{

    #region "Declaration"
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsDoc = null;
    DataSet dsTP = new DataSet();
    DataSet dsDcr = new DataSet();
    DataSet dsAdminSetup = null;
    public static string sfCode = string.Empty;
    public static string sf_code = string.Empty;
    public static string div_code = string.Empty;
    public static string baseUrl = "";
    #endregion

    #region "Page_Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        if ((Convert.ToString(Session["div_code"]) != null || Convert.ToString(Session["div_code"]) != ""))
        {
            div_code = Session["div_code"].ToString().Replace(",", "");
            menumas.FindControl("pnlHeader").Visible = false;
            sfCode = Session["sf_code"].ToString();
           
            if (!Page.IsPostBack)
            {
                ServerStartTime = DateTime.Now;
                base.OnPreInit(e);
                FillDoc();
                FillDcr();
                FillExp();
                FillDoc_Deact();
                FillTourPlan();
                FillLeave();
                Session["backurl"] = "~/BasicMaster.aspx";
            }
        }
        else { Page.Response.Redirect(baseUrl, true); }
    }
    #endregion

    #region "OnLoadComplete"
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    #endregion

    #region "TrackPageTime"
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    }
    #endregion

    #region FillDoc
    private void FillDoc()
    {
        grdListedDR.DataSource = null;
        grdListedDR.DataBind();

        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getListedDr_MGR(sfCode, 2, div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            if (sfCode == "admin")
            {
                grdListedDR.Visible = true;
                grdListedDR.DataSource = dsDoc;
                grdListedDR.DataBind();
            }
        }
        else
        {
            grdListedDR.DataSource = dsDoc;
            grdListedDR.DataBind();
        }
    }
    #endregion

    #region FillDcr
    private void FillDcr()
    {
        grdDCR.DataSource = null;
        grdDCR.DataBind();

        DCR dr = new DCR();
        if (div_code.Contains(','))
            div_code = div_code.Substring(0, div_code.Length - 1);
        dsDcr = dr.get_DCR_Pending_Approval(sfCode, div_code);
        if (dsDcr.Tables[0].Rows.Count > 0)
        {
            grdDCR.Visible = true;
            grdDCR.DataSource = dsDcr;
            grdDCR.DataBind();
        }
        else
        {
            grdDCR.DataSource = dsDcr;
            grdDCR.DataBind();
        }
    }
    #endregion

    #region FillDoc_Deact
    private void FillDoc_Deact()
    {
        grdListedDR_Dea.DataSource = null;
        grdListedDR_Dea.DataBind();

        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getListedDr_MGR(sfCode, 3, div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            if (sfCode == "admin")
            {
                grdListedDR_Dea.Visible = true;
                grdListedDR_Dea.DataSource = dsDoc;
                grdListedDR_Dea.DataBind();
            }
        }
        else
        {
            grdListedDR_Dea.DataSource = dsDoc;
            grdListedDR_Dea.DataBind();
        }
    }
    #endregion

    #region Old FillDoc_AddDeactivate
    //private void FillDoc_AddDeactivate()
    //{
    //    grdadddeactivate.DataSource = null;
    //    grdadddeactivate.DataBind();

    //    ListedDR lstAdd = new ListedDR();
    //    dsDoc = lstAdd.getListedDr_adddeact(sfCode, 2, 3, div_code);
    //    if (dsDoc.Tables[0].Rows.Count > 0)
    //    {
    //        grdadddeactivate.Visible = true;
    //        grdadddeactivate.DataSource = dsDoc;
    //        grdadddeactivate.DataBind();
    //    }
    //    else
    //    {
    //        grdadddeactivate.DataSource = dsDoc;
    //        grdadddeactivate.DataBind();
    //    }

    //}
    #endregion

    #region FillLeave
    private void FillLeave()
    {
        grdLeave.DataSource = null;
        grdLeave.DataBind();

        AdminSetup adm = new AdminSetup();
        dsAdminSetup = adm.getLeave_approve(sfCode, 2, div_code);
        if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
            grdLeave.Visible = true;
            grdLeave.DataSource = dsAdminSetup;
            grdLeave.DataBind();
        }
        else
        {
            grdLeave.DataSource = dsAdminSetup;
            grdLeave.DataBind();
        }
    }
    #endregion

    #region FillTourPlan
    private void FillTourPlan()
    {
        //TourPlan tp = new TourPlan();
        TP_New tp = new TP_New();

        dsTP = tp.get_TP_Pending_Approval(sfCode, div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            //string strGetMR = dsTP.Tables[0].Rows[0]["sf_code"].ToString();
            //if (strGetMR.Substring(0, 2) != "MR")
            //{
            grdTP_Calander.Visible = true;
            grdTP_Calander.DataSource = dsTP;
            grdTP_Calander.DataBind();

            //    }
            //    else
            //    {
            //        btnHome.Visible = true;
            //        grdTP.Visible = true;
            //        grdTP.DataSource = dsTP;
            //        grdTP.DataBind();
            //    }
        }
        else
        {

            grdTP.Visible = true;
            grdTP.DataSource = dsTP;
            grdTP.DataBind();
        }
    }
    #endregion

    #region FillExp
    private void FillExp()
    {
        //TourPlan tp = new TourPlan();
        TP_New tp = new TP_New();

        dsTP = tp.get_Exp_Approval_Admin(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            //string strGetMR = dsTP.Tables[0].Rows[0]["sf_code"].ToString();
            //if (strGetMR.Substring(0, 2) != "MR")
            //{
            GridView1.Visible = true;
            GridView1.DataSource = dsTP;
            GridView1.DataBind();

            //    }
            //    else
            //    {
            //        btnHome.Visible = true;
            //        grdTP.Visible = true;
            //        grdTP.DataSource = dsTP;
            //        grdTP.DataBind();
            //    }
        }
        else
        {
            //btnHome.Visible = true;
            GridView1.Visible = true;
            GridView1.DataSource = dsTP;
            GridView1.DataBind();
        }
    }
    #endregion

    #region btnLogout_Click
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }
    #endregion

    #region grdTP_RowDataBound
    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string ActTerrtotal = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblMonth = (Label)e.Row.FindControl("lblMonth");
            lblMonth.Text = getMonthName(lblMonth.Text);
            // e.Row.Cells[5].Text = "Click here to Approve " + lblMonth.Text + " "+ dsTP.Tables[0].Rows[0]["Tour_Year"].ToString();
            ActTerrtotal = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sf_code"));
            if (ActTerrtotal.Contains("MR"))
            {
                e.Row.Cells[8].Visible = false;
            }
            else
            {
                e.Row.Cells[7].Visible = false;
            }
        }

    }
    #endregion

    #region Old grdTP_RowDataBound
    //protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        Label lblMonth = (Label)e.Row.FindControl("lblMonth");
    //        lblMonth.Text = getMonthName(lblMonth.Text);
    //        // e.Row.Cells[5].Text = "Click here to Approve " + lblMonth.Text + " "+ dsTP.Tables[0].Rows[0]["Tour_Year"].ToString();
    //    }

    //}
    #endregion

    #region getMonthName
    private string getMonthName(string sMonth)
    {
        string sReturn = string.Empty;

        if (sMonth == "1")
        {
            sReturn = "January";
        }
        else if (sMonth == "2")
        {
            sReturn = "February";
        }
        else if (sMonth == "2")
        {
            sReturn = "February";
        }
        else if (sMonth == "3")
        {
            sReturn = "March";
        }
        else if (sMonth == "4")
        {
            sReturn = "April";
        }
        else if (sMonth == "5")
        {
            sReturn = "May";
        }
        else if (sMonth == "6")
        {
            sReturn = "June";
        }
        else if (sMonth == "7")
        {
            sReturn = "July";
        }
        else if (sMonth == "8")
        {
            sReturn = "August";
        }
        else if (sMonth == "9")
        {
            sReturn = "September";
        }
        else if (sMonth == "10")
        {
            sReturn = "October";
        }
        else if (sMonth == "11")
        {
            sReturn = "November";
        }
        else if (sMonth == "12")
        {
            sReturn = "December";
        }

        return sReturn;
    }
    #endregion
}