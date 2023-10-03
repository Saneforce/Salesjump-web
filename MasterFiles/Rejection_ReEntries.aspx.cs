using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Rejection_ReEntries : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsAdmin = null;
    DataSet dsDoc1 = null;
    DataSet dsSalesForce = null;
    DataSet dsTP = new DataSet();
    DataSet dsAdm = null;
    DataSet dsDcr = new DataSet();
    DataSet dsAdmNB = null;
    DataSet dsAdminSetup = null;
    DataSet dsSecSales = null;
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        LblUser.Text = "Welcome " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
        sfCode = Session["sf_code"].ToString();
        if (Session["div_name"] != null)
        {
            //LblDiv.Text = Session["div_name"].ToString();

        }
        if (!Page.IsPostBack)
        {
            FillDoc();
            FillDcr();
            FillTourPlan();
            FillLeave();
            if (Session["sf_type"].ToString() == "2")
            {
                pnldoc.Visible = false;
            }
            Session["backurl"] = "~/MasterFiles/Rejection_ReEntries.aspx";
        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    }
    private void FillDoc()
    {
        grdListedDR.DataSource = null;
        grdListedDR.DataBind();

        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getListedDr_RejectList(sfCode);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {

            grdListedDR.Visible = true;
            grdListedDR.DataSource = dsDoc;
            grdListedDR.DataBind();
        }
        else
        {
            btnHome.Visible = true;
            grdListedDR.DataSource = dsDoc;
            grdListedDR.DataBind();
        }
    }
    private void FillDcr()
    {
        grdDCR.DataSource = null;
        grdDCR.DataBind();

        DCR dr = new DCR();

        dsDcr = dr.get_DCR_Rejected_Approval(sfCode);

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

    private void FillLeave()
    {
        AdminSetup adm = new AdminSetup();
        dsAdmin = adm.getLeave_Reject(sfCode, 1);
        if (dsAdmin.Tables[0].Rows.Count > 0)
        {
            grdLeave.Visible = true;
            grdLeave.DataSource = dsAdmin;
            grdLeave.DataBind();
        }
        else
        {
            grdLeave.DataSource = dsAdmin;
            grdLeave.DataBind();
        }
    }
    private void FillTourPlan()
    {
        TourPlan tp = new TourPlan();

        dsTP = tp.get_TP_Rejected_Approval(sfCode);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            string strGetMR = dsTP.Tables[0].Rows[0]["sf_code"].ToString();
            if (strGetMR.Substring(0, 2) != "MR")
            {
                //grdTP_Calander.Visible = true;
                //grdTP_Calander.DataSource = dsTP;
                //grdTP_Calander.DataBind();

            }
            else
            {

                grdTP.Visible = true;
                grdTP.DataSource = dsTP;
                grdTP.DataBind();
            }
        }
        else
        {
            btnHome.Visible = true;
            grdTP.Visible = true;
            grdTP.DataSource = dsTP;
            grdTP.DataBind();
        }


    }

    //Populate the Secondary Sales grid which are rejected  by manager
    private void FillSecSales()
    {
        grdSecSales.DataSource = null;
        grdSecSales.DataBind();
        SecSale ss = new SecSale();
        //Get the rejection list
        dsSecSales = ss.get_SecSales_Pending_Approval(sfCode, 3);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            grdSecSales.Visible = true;
            grdSecSales.DataSource = dsSecSales;
            grdSecSales.DataBind();
        }
        else
        {
            btnHome.Visible = true;
            grdSecSales.DataSource = dsSecSales;
            grdSecSales.DataBind();
        }
    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        if (sfCode.Substring(0, 2) != "MR")
        {
            System.Threading.Thread.Sleep(time);
            Response.Redirect("~/MGR_Home.aspx");
        }
        else
        {
            System.Threading.Thread.Sleep(time);
            Response.Redirect("~/Default_MR.aspx");
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("~/Index.aspx");
    }
}