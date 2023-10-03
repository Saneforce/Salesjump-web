using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_StateLocationList : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
            Session["GetcmdArgChar"] = "All";
            FillState();
            FillSt_Alpha();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
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
        time = serverTimeDiff.Minutes;

    } 
    
    private void FillState()
    {
        State dv = new State();
        dsState = dv.getState_Division();
        if (dsState.Tables[0].Rows.Count > 0)
        {
            grdState.Visible = true;
            grdState.DataSource = dsState;
            grdState.DataBind();
            foreach (GridViewRow row in grdState.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                Label lbldivision_name = (Label)row.FindControl("lbldivision_name");
                if (lbldivision_name.Text != "")
                {
                    // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
        }
        else
        {
            grdState.DataSource = dsState;
            grdState.DataBind();
        }
    }
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        State dv = new State();
        dtGrid = dv.getStateLocationlist_DataTable();
        sCmd = Session["GetcmdArgChar"].ToString();
        if (sCmd == "All")
        {
            dtGrid = dv.getStateLocationlist_DataTable();
        }
        else if (sCmd != "")
        {

            dtGrid = dv.getStateLocationlist_DataTable(sCmd);
        }
        return dtGrid;
    }

    protected void grdState_Sorting(object sender, GridViewSortEventArgs e)
    {

        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }

        DataView sortedView = new DataView(BindGridView());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        DataTable dtGrid = new DataTable();
        dtGrid = sortedView.ToTable();
        grdState.DataSource = dtGrid;
        grdState.DataBind();
        foreach (GridViewRow row in grdState.Rows)
        {
            LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
            Label lblimg = (Label)row.FindControl("lblimg");         
            if (dtGrid.Rows[row.RowIndex][3].ToString() != "")
            {
                lnkdeact.Visible = false;
                lblimg.Visible = true;
            }
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("StateLocationCreation.aspx");
    }

    protected void grdState_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdState.EditIndex = -1;
        //Fill the State Grid
        sCmd = Session["GetcmdArgChar"].ToString();
        //Fill the State Grid
        if (sCmd == "All")
        {
            FillState();
        }
        else
        {
            FillState(sCmd);
        }
    }

    protected void grdState_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdState.EditIndex = e.NewEditIndex;

        sCmd = Session["GetcmdArgChar"].ToString();
        //Fill the State Grid
        if (sCmd == "All")
        {
            FillState();
        }
        else
        {
            FillState(sCmd);
        }
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)grdState.Rows[e.NewEditIndex].Cells[3].FindControl("txtStateName");
        ctrl.Focus();
        
    }
    //protected void grdState_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    Label lblStateCode = (Label)grdState.Rows[e.RowIndex].Cells[1].FindControl("lblStateCode");
    //    statecode = Convert.ToInt16(lblStateCode.Text);

    //    // Delete State
    //    State dv = new State();
    //    int iReturn = dv.RecordDelete(statecode);
    //     if (iReturn > 0 )
    //    {
    //       // menu1.Status = "State/Location deleted Successfully ";
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('State/Location deleted Successfully');</script>");
    //    }
    //    else if (iReturn == -2)
    //    {
    //       // menu1.Status = "State/Location already Exist";
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('State/Location already Exist');</script>");
    //    }
    //    FillState();
    //}

    //Changes done by priya
    //begin
    protected void grdState_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            LinkButton lnkdeact = (LinkButton)grdState.FindControl("lnkbutDeactivate");
            State_Code = Convert.ToInt16(e.CommandArgument);
            //Deactivate
            State st = new State();
            int iReturn = st.DeActivateNew(State_Code);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Deactivated Successfully.\');", true);
            }
            //else
            //{
            //    // menu1.Status ="Unable to Deactivate";
            //    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Unable to Deactivate.\');", true);
            //}
            //if (iReturn == -2)
            //{
            //    // menu1.Status = "State/Location already Exist";
                
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Record Exists in Division Master');</script>");
            //}
           
            FillState();
        }
    }
    //end
    protected void grdState_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdState.EditIndex = -1;
        int iIndex = e.RowIndex;
        UpdateState(iIndex);

        sCmd = Session["GetcmdArgChar"].ToString();
        //Fill the State Grid
        if (sCmd == "All")
        {
            FillState();
        }
        else
        {
            FillState(sCmd);
        }
    }

    protected void grdState_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdState_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdState.PageIndex = e.NewPageIndex;
        sCmd = Session["GetcmdArgChar"].ToString();
        //Fill the State Grid
        if (sCmd == "All")
        {
            FillState();
        }
        else
        {
            FillState(sCmd);
        }
    }
    private void UpdateState(int eIndex)
    {
        System.Threading.Thread.Sleep(time);
        Label lblStateCode = (Label)grdState.Rows[eIndex].Cells[1].FindControl("lblStateCode");
        statecode = Convert.ToInt16(lblStateCode.Text);
        //TextBox txtShortName = (TextBox)grdState.Rows[eIndex].Cells[2].FindControl("txtShortName");
        //shortname = txtShortName.Text;
        TextBox txtStateName = (TextBox)grdState.Rows[eIndex].Cells[3].FindControl("txtStateName");
        statename = txtStateName.Text;

        // Update State
        State dv = new State();
        int iReturn = dv.RecordUpdate(statecode, statename);
         if (iReturn > 0 )
        {
            //menu1.Status = "State/Location Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
         else if (iReturn == -2)
         {            
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('State Name Already Exist');</script>");
             txtStateName.Focus();
         }  
    }


    //alphabetical sorting

    private void FillSt_Alpha()
    {
        State st = new State();
        dsState = st.getState_Alphabet();
        if (dsState.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsState;
            dlAlpha.DataBind();
        }
    }
    private void FillState(string sAlpha)
    {
        State st = new State();
        dsState = st.getState_Alphabet(sAlpha);
        if (dsState.Tables[0].Rows.Count > 0)
        {
            grdState.Visible = true;
            grdState.DataSource = dsState;
            grdState.DataBind();
            foreach (GridViewRow row in grdState.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                Label lbldivision_name = (Label)row.FindControl("lbldivision_name");
                if (lbldivision_name.Text != "")
                {
                    // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
        }
        else
        {
            grdState.DataSource = dsState;
            grdState.DataBind();
        }
    }
    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        string sCmd = e.CommandArgument.ToString();
        Session["GetcmdArgChar"] = sCmd;

        if (sCmd == "All")
        {
            grdState.PageIndex = 0;
            FillState();
        }
        else
        {
            grdState.PageIndex = 0;
            FillState(sCmd);
        }
        //grdSalesForce.EditIndex = -1;
        //Fill the SalesForce Grid
        //FillSalesForce();
    }




    //end

    protected void btnReactivate_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.Sleep(time);
        Response.Redirect("State_Reactivation.aspx");
    }
}