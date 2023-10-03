using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_MR_Doctor_Spec_List : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocSpe = null;
    int DocSpeCode = 0;
    string divcode = string.Empty;
    string DocSpe_SName = string.Empty;
    string DocSpeName = string.Empty;
    string sfCode = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillDocSpe();
           // menu1.Title = this.Page.Title;
           // menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
        if (Session["sf_type"].ToString() == "1")
        {
            
            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
          
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
        

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
    private void FillDocSpe()
    {

        Doctor dv = new Doctor();
        dsDocSpe = dv.getDocSpe(divcode);
        if (dsDocSpe.Tables[0].Rows.Count > 0)
        {
            grdDocSpe.Visible = true;
            grdDocSpe.DataSource = dsDocSpe;
            grdDocSpe.DataBind();
        }
        else
        {
            grdDocSpe.DataSource = dsDocSpe;
            grdDocSpe.DataBind();
        }
    }
    protected void grdDocSpe_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDocSpe.PageIndex = e.NewPageIndex;
        FillDocSpe();
    }
}