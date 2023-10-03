using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_State_Location_Creation : System.Web.UI.Page
{

#region "Declaration"
DataSet dsState = null;
int statecode = 0;
string statename = string.Empty;
string shortname = string.Empty;
#endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillState();
        }
    }
    private void FillState()
    {
        State dv = new State();
        dsState = dv.getState();
        if (dsState.Tables[0].Rows.Count > 0)
        {
            grdState.Visible = true;
            grdState.DataSource = dsState;
            grdState.DataBind();
        }
    }

    protected void grdState_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdState.EditIndex = -1;
        //Fill the State Grid
        FillState();
    }

    protected void grdState_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdState.EditIndex = e.NewEditIndex;
        //Fill the State Grid
        FillState();
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)grdState.Rows[e.NewEditIndex].Cells[2].FindControl("txtShortName");
        ctrl.Focus();
    }

    protected void grdState_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdState.EditIndex = -1;
        int iIndex = e.RowIndex;
        UpdateState(iIndex);
        FillState();  
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
        FillState();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        statename = txtStateName.Text;
        shortname = txtShortName.Text;
       
        // Add New State
        State dv = new State();
        int iReturn = dv.RecordAdd(shortname, statename);

         if (iReturn > 0 )
        {
            Response.Write("Record Added Successfully ");
        }
        else if (iReturn == -2)
        {
            Response.Write("Record already Exist");
        }
    }
    private void UpdateState(int eIndex)
    {
        Label lblStateCode = (Label)grdState.Rows[eIndex].Cells[1].FindControl("lblStateCode");
        statecode = Convert.ToInt16(lblStateCode.Text);
        //TextBox txtShortName = (TextBox)grdState.Rows[eIndex].Cells[2].FindControl("txtShortName");
        //shortname = txtShortName.Text;
        txtShortName.Enabled = false;
        TextBox txtStateName = (TextBox)grdState.Rows[eIndex].Cells[3].FindControl("txtStateName");
        statename = txtStateName.Text;
      
        // Update State
        State dv = new State();
        int iReturn = dv.RecordUpdate(statecode, statename);
         if (iReturn > 0 )
        {
            Response.Write("Record Updated Successfully ");
        }
        else if (iReturn == -2)
        {
            Response.Write("State Name Already Exist");
        }
    }
}