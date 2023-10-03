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
using System.Web.Services;
using Newtonsoft.Json;

public partial class MasterFiles_CategoryCreationList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocSpe = null;
    int DocSpeCode = 0;
    static string divcode = string.Empty;
    string DocSpe_SName = string.Empty;
    string DocSpeName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        //if (!Page.IsPostBack)
        //{
        //    FillDocSpe();
        //    btnNew.Focus();
        //    //menu1.Title = this.Page.Title;
        //    //menu1.FindControl("btnBack").Visible = false;
        //    ServerStartTime = DateTime.Now;
        //    base.OnPreInit(e);
        //}
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
    [WebMethod]
    public static string FillDocSpeS()
    {

        Doctor dv = new Doctor();
        DataTable dt = new DataTable();
        dt = dv.getDocCat(divcode.TrimEnd(',')).Tables[0];
        return JsonConvert.SerializeObject(dt);

    }
   
    
    //protected void btnNew_Click(object sender, EventArgs e)
    //{
    //    System.Threading.Thread.Sleep(time);
    //    Response.Redirect("/Stockist/CategoryCreation.aspx");
    //}
}
