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

public partial class UserControl_pnlMenu : System.Web.UI.UserControl
{
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string sf_type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {      
        System.Threading.Thread.Sleep(time);
        if (sf_type == "3")
        {
            LblUser.Text = "Welcome " + Session["sf_name"];
        }
        else
        {
            LblUser.Text = "Welcome " + Session["sf_name"];
        }
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
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
        if (Session["sf_type"] == "3")
        {
          //  LiDiv.Visible = false;
            LblUser.Text = "Welcome " + Session["div_Name"];
            lblRoute.Text = "Change Password";
            Listate.Visible = false;
             Lique.Visible = false;
        }
        else
        {
            Limail.Visible = false;
            SubHo.Visible = false;
            lireports.Visible = false;
            lidash.Visible = false;
            liholi.Visible = false;
            lides.Visible = false;
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
        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + serverTimeDiff.Ticks + "');</script>");
        time = serverTimeDiff.Minutes;

    } 
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Session["backurl"].ToString().Trim() != "")
        {
            System.Threading.Thread.Sleep(time);
            Response.Redirect(Session["backurl"].ToString());
           
        }
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Index.aspx");
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Context.Session != null)
        {
            if (Session.IsNewSession)
            {
                HttpCookie newSessionIdCookie = Request.Cookies["div_code"];
                if (newSessionIdCookie != null)
                {
                    string newSessionIdCookieValue = newSessionIdCookie.Value;
                    if (newSessionIdCookieValue != string.Empty)
                    {
                        // This means Session was timed Out and New Session was started
                        Response.Redirect("~/Index.aspx");
                    }
                }
            }
        }
    }
}