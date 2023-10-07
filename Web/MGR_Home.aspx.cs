using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MGR_MGR_Home : System.Web.UI.Page
{
    DataSet dsAdmin = null;
    DataSet dsAdmin1 = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        menu1.FindControl("pnlHeader").Visible = false;
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreRender(e);
            AdminSetup adm1 = new AdminSetup();
            dsAdmin1 = adm1.Get_Flash_News(div_code);
            
            if (dsAdmin1.Tables[0].Rows.Count > 0)
            {
                
                lblFlash.Text = dsAdmin1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                lblFlash.Text = lblFlash.Text.Replace("asdf", "'");
            }
            gettalk();
        }

    }
    protected void CalMgr_DayRender(object sender, DayRenderEventArgs e)
    {

        if (e.Day.IsOtherMonth)
        {
            e.Cell.Controls.Clear();
            e.Cell.Text = string.Empty;
        }
        else
        {
            DataSet dsDCR = new DataSet();
            DCR dc = new DCR();
            dsDCR = dc.getDCR_Report_MGR_Calendar(sf_code, e.Day.Date.Day, e.Day.Date.Month, e.Day.Date.Year);
            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                Label lblArea = new Label();
                lblArea.Visible = true;
                lblArea.Text = "<br>" + dsDCR.Tables[0].Rows[0][0].ToString();
                lblArea.Font.Size = 7;
                lblArea.Font.Name = "Verdana";
                lblArea.Font.Bold = false;
                lblArea.ForeColor = System.Drawing.Color.Red;

                e.Cell.Controls.Add(lblArea);
            }
        }


    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    }

    private void gettalk()
    {
        AdminSetup adm = new AdminSetup();
        dsAdmin = adm.Get_talktous(div_code);

        if (dsAdmin.Tables[0].Rows.Count > 0)
        {
            lblsup.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

        }
        else
        {
            lblsup.Text = "---> 9841737722 (Hindi,English,Gujarthi), 9841316719(Hindi,English,Tamil), 9841266611(Marathi,Hindi,English,Malayalam,Telugu), 9841633008(Tamil,English)";
        }
    }
    
    protected void btntp_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/MGR/TourPlan_Calen.aspx");
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Report/TP_View_Report.aspx");
    }
    protected void btndcr_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/DCR/DCR_Entry.aspx");
    }
    protected void btndcrview_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Reports/DCR_View.aspx");
    }
    protected void btnmail_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Mails/Mail_Head.aspx");
    }
    protected void lnkreject_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/MasterFiles/Rejection_ReEntries.aspx");

    }
    protected void lnkfile_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/MasterFiles/MR/Usermanual_View.aspx");
    }

    protected void btnmain_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default_MGR.aspx");
    }
 protected void btndExp_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/MasterFiles/MGR/WrkTypeWise_Allowance_date.aspx");

    }
    protected void BtnExp1_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/MasterFiles/Reports/rptAutoexpense_Approve_View.aspx");

    }
}