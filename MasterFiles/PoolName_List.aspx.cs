using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_PoolName_List : System.Web.UI.Page
{
    string divcode = string.Empty;
    string Pool_sname = string.Empty;
    string Pool_name = string.Empty;
    DataSet dsStockist = null;
        
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);          
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            GetPoolArea();
            btnNew.Focus();
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
        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + serverTimeDiff.Ticks + "');</script>");
        time = serverTimeDiff.Minutes;

    } 
    protected void btnNew_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
         Response.Redirect("PoolName_Creation.aspx");
    }

    private void GetPoolArea()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getPoolName_List(divcode);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {

            grdPoolName.DataSource = dsStockist;
            grdPoolName.DataBind();
        }
        else
        {
            grdPoolName.DataSource = dsStockist;
            grdPoolName.DataBind();
        }
    }
    protected void grdPoolName_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdPoolName.EditIndex = -1;
        GetPoolArea();
    }

    protected void grdPoolName_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdPoolName.EditIndex = e.NewEditIndex;
        GetPoolArea();
        TextBox ctrl = (TextBox)grdPoolName.Rows[e.NewEditIndex].Cells[2].FindControl("txtShortName");
        ctrl.Focus();
    }
}