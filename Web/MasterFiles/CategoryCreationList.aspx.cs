using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_CategoryCreationList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocSpe = null;
    int DocSpeCode = 0;
    string divcode = string.Empty;
    string DocSpe_SName = string.Empty;
    string DocSpeName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            FillDocSpe();
            btnNew.Focus();
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
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
        dsDocSpe = dv.getDocCat(divcode);
        if (dsDocSpe.Tables[0].Rows.Count > 0)
        {
            grdDocSpe.Visible = true;
            grdDocSpe.DataSource = dsDocSpe;
            grdDocSpe.DataBind();

            //dist_Count   
            foreach (GridViewRow row in grdDocSpe.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                Label lblCount = (Label)row.FindControl("lblCount");
                Label lblDistCount = (Label)row.FindControl("lblDistCount");
                //if (Convert.ToInt32(dsDocSpe.Tables[0].Rows[row.RowIndex][3].ToString()) > 0)
                if (lblCount.Text != "0" || lblDistCount.Text != "0")
                {
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
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
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("CategoryCreation.aspx");
    }
}