using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_State_Reactivation : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsState = null;
    int statecode = 0;
    string statename = string.Empty;
    string shortname = string.Empty;
    int State_Code = 0;
    int div_code = 0;
    string sCmd = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string divcode = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "StateLocationList.aspx";    
        if (!Page.IsPostBack)
        {
            //  Session["GetcmdArgChar"] = "All";
            FillState();         
            menu1.Title = this.Page.Title;
           
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
    private void FillState()
    {
        State dv = new State();
        dsState = dv.getState_Reactivate();
        if (dsState.Tables[0].Rows.Count > 0)
        {
            grdState.Visible = true;
            grdState.DataSource = dsState;
            grdState.DataBind();
        }
        else
        {
            grdState.DataSource = dsState;
            grdState.DataBind();
        }
    }
    protected void grdState_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            State_Code = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            State dv = new State();
            int iReturn = dv.ReActivate_State(State_Code);
            if (iReturn > 0)
            {               
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
            }
            else
            {             
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Reactivate');</script>");
            }
            FillState();
        }
    }
}