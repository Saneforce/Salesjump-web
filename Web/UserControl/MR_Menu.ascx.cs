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
public partial class UserControl_MR_Menu : System.Web.UI.UserControl
{
    string _isErr;
    string _sURL;
    string div_code = string.Empty;
    
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsTerritory = null;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        LblUser.Text = "Welcome " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
        LblDiv.Text = Session["div_name"].ToString();
        if (Session["div_name"] != null)
        {
            //LblDiv.Text = Session["div_name"].ToString();
        }
        if (!Page.IsPostBack)
        {
         
            ServerStartTime = DateTime.Now;
            base.OnPreRender(e);
            Session["Reset"] = true;            
            Configuration conf = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            
            SessionStateSection section = (SessionStateSection)conf.GetSection("system.web/sessionState");
            int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
            string script = "<script language='JavaScript'>SessionExpireAlert(" + timeout + ");</script>";
            Page.RegisterStartupScript("myscript", script);
            DataSet dsTerritory = new DataSet();
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                //menu1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "List";
                //lblTerritory.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                //lblTerritory1.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
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
        System.Threading.Thread.Sleep(time);
        Response.Redirect(Session["backurl"].ToString());
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        AdminSetup adm = new AdminSetup();
        int iReturn = adm.AddQuery(ddlProb.SelectedItem.Text, txtQuery.Text, div_code, Session["sf_code"].ToString());

        if (iReturn > 0)
        {    
           
   
            
        }
        ddlProb.SelectedIndex = -1;
        txtQuery.Text = "";


    }

  
}