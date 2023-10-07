using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;
using System.Data;
using Bus_EReport;


public partial class UserControl_MGR_TP_Menu : System.Web.UI.UserControl
{
      string _isErr;
    string _sURL;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        LblUser.Text = "Welcome " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
        if (Session["div_name"] != null)
        {
            //LblDiv.Text = Session["div_name"].ToString();
        }
        if (!Page.IsPostBack)
        {

            ServerStartTime = DateTime.Now;
            base.OnPreRender(e);
            Session["Reset"] = true;
            //Configuration config = WebConfigurationManager.AppSettings.Get("");
            Configuration conf = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            SessionStateSection section = (SessionStateSection)conf.GetSection("system.web/sessionState");
            int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
            string script = "<script language='JavaScript'>SessionExpireAlert(" + timeout + ");</script>";
            Page.RegisterStartupScript("myscript", script);
        }
    }

    string _title;
    public string Title
    {
        get
        {
            return this._title;
        }
        set
        {
            this._title = value;
            lblHeading.Text = value;
        }
    }

    string _status;
    public string Status
    {
        get
        {
            return this._status;
        }
        set
        {
            this._status = value;
            lblStatus.Text = value;
            if (_isErr == "error")
                lblStatus.ForeColor = System.Drawing.Color.Red;
        }
    }


    public string isERR
    {
        get
        {
            return this._isErr;
        }
        set
        {
            this._isErr = value;
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Session["backurl"].ToString());
    }
  
}
