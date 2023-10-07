using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_HolidayList : System.Web.UI.Page
{
    DataSet dsHoliday = null;
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsTP = null;
    string sState = string.Empty;
    string div_code = string.Empty;
    string HSlno = string.Empty;
    string statecode = string.Empty;
    string Holname = string.Empty;
    string HolDate = string.Empty;
    string[] statecd;
    string slno;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string state_cd = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        
        if (!Page.IsPostBack)
        {
            btnNew.Focus();
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());                    
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
            FillState(div_code);
            //FillYear(div_code);
           FillHoliday(ddlState.SelectedValue,ddlYear.SelectedValue);
          
            //FillHoliday(ddlYear.SelectedValue);                              
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
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
    private void FillHoliday(string state_code,string Year)
    {
        Holiday hol = new Holiday();
        dsHoliday = hol.getHoliday_List(state_code, div_code, Year);
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            grdHoliday.Visible = true;
            grdHoliday.DataSource = dsHoliday;
            grdHoliday.DataBind();
        }
        else
        {
            //grdHoliday.Visible = false;
            grdHoliday.DataSource = dsHoliday;
            grdHoliday.DataBind();
        }
    }
  
    //private void FillHolidayYear(string state_code,string Year)
    //{
    //    Holiday hol = new Holiday();
    //    dsHoliday = hol.getHolidayYear(state_code, div_code, Year);
    //    if (dsHoliday.Tables[0].Rows.Count > 0)
    //    {
    //        grdHoliday.Visible = true;
    //        grdHoliday.DataSource = dsHoliday;
    //        grdHoliday.DataBind();
    //    }
    //    else
    //    {
    //        //grdHoliday.Visible = false;
    //        grdHoliday.DataSource = dsHoliday;
    //        grdHoliday.DataBind();
    //    }
    //}
    //Change done by saravanan 07-08-2014

    private void FillState(string div_code)
    {
        //Holiday st = new Holiday();
        //dsState = st.getState(div_code);
        //ddlState.DataTextField = "statename";
        //ddlState.DataValueField = "statecode";
        //ddlState.DataSource = dsState;
        //ddlState.DataBind();
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getSt(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();

        }
    }
     //New
    //private void FillYear(string div_code)
    //{
    //    Holiday st = new Holiday();
    //    dsState = st.getYear(div_code);
    //    ddlYear.DataTextField = "Academic_Year";
    //    ddlYear.DataValueField = "statecode";
    //    ddlYear.DataSource = dsState;
    //    ddlYear.DataBind();

    //}

    // Sorting

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

    //Change getHolidaylist_DataTable done by saravanan 07-08-2014
    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        Holiday hol = new Holiday();
        //dtGrid = hol.getHolidaylist_DataTable(div_code);
        dtGrid = hol.getHolidaylist_DataTable(div_code,ddlYear.SelectedValue,ddlState.SelectedValue);
        return dtGrid;
    }

    protected void grdHoliday_Sorting(object sender, GridViewSortEventArgs e)
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
        grdHoliday.DataSource = sortedView;
        grdHoliday.DataBind();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("HolidayFixation.aspx");
    }
    protected void grdHoliday_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdHoliday.EditIndex = -1;
        //Fill the Grid
        FillHoliday(ddlState.SelectedValue,ddlYear.SelectedValue);
    }

    protected void grdHoliday_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdHoliday.EditIndex = e.NewEditIndex;
        //Fill the State Grid
        FillHoliday(ddlState.SelectedValue,ddlYear.SelectedValue);
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)grdHoliday.Rows[e.NewEditIndex].Cells[4].FindControl("txtDate");
        ctrl.Focus();
    }
    protected void grdHoliday_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblHSlno = (Label)grdHoliday.Rows[e.RowIndex].Cells[1].FindControl("lblHSlno");
        HSlno = lblHSlno.Text;

        // Delete State
        Holiday dv = new Holiday();
        int iReturn = dv.RecordDelete(HSlno, div_code);
         if (iReturn > 0 )
        {
            //menu1.Status = "Holiday details deleted Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            //menu1.Status = "Record cannot be deleted";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Record Exists in Division Master');</script>");
        }
         FillHoliday(ddlState.SelectedValue,ddlYear.SelectedValue);
    }
    protected void grdHoliday_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdHoliday.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillHoliday(ddlState.SelectedValue,ddlYear.SelectedValue);
    }
    protected void grdHoliday_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");            
        }
    }

    protected void grdHoliday_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdHoliday.PageIndex = e.NewPageIndex;
        FillHoliday(ddlState.SelectedValue,ddlYear.SelectedValue);
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillHoliday(ddlState.SelectedValue);
       // FillHolidayYear(ddlState.SelectedValue, ddlYear.SelectedValue);
        FillHoliday(ddlState.SelectedValue,ddlYear.SelectedValue);
    }
    private void Update(int eIndex)
    {
        Label lblHSlno = (Label)grdHoliday.Rows[eIndex].Cells[1].FindControl("lblHSlno");
        HSlno = lblHSlno.Text;
        Label lblHolidayName = (Label)grdHoliday.Rows[eIndex].Cells[5].FindControl("lblHolidayName");
        Holname = lblHolidayName.Text;
        Label lblStateCode = (Label)grdHoliday.Rows[eIndex].Cells[2].FindControl("lblStateCode");
        statecode= lblStateCode.Text;
        TextBox txtDate = (TextBox)grdHoliday.Rows[eIndex].Cells[6].FindControl("txtDate");
        HolDate = txtDate.Text;        
        // Update Holiday
        Holiday dv = new Holiday();
        int iReturn = dv.RecordUpdate(statecode, HSlno, Holname, HolDate, div_code);
         if (iReturn > 0 )
        {
            //menu1.Status = "Holiday Details updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            //menu1.Status = "Holiday details already Exist";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exist');</script>");
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("HolidayView.aspx");
    }
    protected void btnold_Click(object sender, EventArgs e)
    {
        Response.Redirect("HolidayFixation_old.aspx");
    }

    // New
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
   {
      // FillHolidayYear(ddlState.SelectedValue,ddlYear.SelectedValue);
       FillHoliday(ddlState.SelectedValue,ddlYear.SelectedValue);
   }
    protected void btnCons_Click(object sender, EventArgs e)
    {
        Response.Redirect("Calendar_Consolidated.aspx");
    }
}
