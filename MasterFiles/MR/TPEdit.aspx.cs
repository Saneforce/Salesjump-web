using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_TPEdit : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsTP = null;
    DataSet dsWeek = null;
    DataSet dsHoliday = null;
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string state_code = string.Empty;
    string TP_Date = string.Empty;
    string TP_Day = string.Empty;
    string TP_Terr = string.Empty;
    string TP_Terr_Name = string.Empty;
    string TP_Terr1 = string.Empty;
    string TP_Terr1_Name = string.Empty;
    string TP_Terr2 = string.Empty;
    string TP_Terr2_Name = string.Empty;
    string ddlWT_Name = string.Empty;
    string ddlValueWT1 = string.Empty;
    string ddlTextWT1 = string.Empty;
    string ddlValueWT2 = string.Empty;
    string ddlTextWT2 = string.Empty;
    bool TP_Submit = false;
    string ddlWT = string.Empty;
    int TP_Month = -1;
    int TP_Year = -1;
    DateTime TP_Submit_Date;
    string strTPView = string.Empty;
    DateTime TP_Tour_Date;
    string TP_Tour_Shedule = string.Empty;
    string TP_Objective = string.Empty;

    DateTime dt_TP_Active_Date;
    DateTime dt_TP_Current_Date;
    int i;
    int iWeek = -1;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                  
                    dsTP = tp.Get_TP_Entry_Edit_Year(div_code, sf_code);
                    if (dsTP.Tables[0].Rows.Count > 0)
                    {
                        ddlMonth.SelectedValue = dsTP.Tables[0].Rows[0]["Tour_Month"].ToString();
                        ddlYear.SelectedValue = dsTP.Tables[0].Rows[0]["Tour_Year"].ToString();
                    }
                    else
                    {
                        ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                    }
                }
            }

            dsTP = tp.get_TP_Edit_MonthDDL(sf_code, ddlYear.SelectedValue.ToString());

            if (dsTP.Tables[0].Rows.Count > 0)
            {
                ddlMonth.Items.Add(new ListItem("---Select---", "0", true));
                for (int m = 0; m < dsTP.Tables[0].Rows.Count;m++)
                {
                    string str = getMonth(Convert.ToInt16(dsTP.Tables[0].Rows[m]["Tour_Month"].ToString()));
                   
                    ddlMonth.Items.Add(new ListItem(str, dsTP.Tables[0].Rows[m]["Tour_Month"].ToString(), true));
                } 
            }
            else
            {
                ddlMonth.Items.Add(new ListItem("---Select---", "0", true));
            }
            
            menu1.FindControl("btnBack").Visible = false;
            lblFieldForce.Text =  Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
            menu1.Title = "Tour Plan - Edit " + "(<span style='font-size:8pt;color:Black'> Before Approval </span>)";                       
          
            TourPlan();
            btnGo_Click(sender, e);
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
        FillWorkType();
    }

    private string getMonth(int iMonth)
    {
        string sMonth = string.Empty;

        if (iMonth == 1)
        {
            sMonth = "January";
        }
        else if (iMonth == 2)
        {
            sMonth = "Febraury";
        }
        else if (iMonth == 3)
        {
            sMonth = "March";
        }
        else if (iMonth == 4)
        {
            sMonth = "April";
        }
        else if (iMonth == 5)
        {
            sMonth = "May";
        }
        else if (iMonth == 6)
        {
            sMonth = "June";
        }
        else if (iMonth == 7)
        {
            sMonth = "July";
        }
        else if (iMonth == 8)
        {
            sMonth = "August";
        }
        else if (iMonth == 9)
        {
            sMonth = "September";
        }
        else if (iMonth == 10)
        {
            sMonth = "October";
        }
        else if (iMonth == 11)
        {
            sMonth = "November";
        }
        else if (iMonth == 12)
        {
            sMonth = "December";
        }
        return sMonth;
    }

    
    protected void TourPlan()
    {
        try
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                strTPView = dsTerritory.Tables[0].Rows[0]["No_of_TP_View"].ToString();
                if (strTPView == "3")
                {
                    grdTP.Columns[3].Visible = true;
                    grdTP.Columns[4].Visible = true;
                    grdTP.Columns[5].Visible = true;
                    grdTP.Columns[6].Visible = true;
                    grdTP.Columns[7].Visible = true;
                    grdTP.Columns[8].Visible = true;

                }
                else if (strTPView == "2")
                {

                    grdTP.Columns[3].Visible = true;
                    grdTP.Columns[4].Visible = true;
                    grdTP.Columns[5].Visible = true;
                    grdTP.Columns[6].Visible = true;
                    grdTP.Columns[7].Visible = false;
                    grdTP.Columns[8].Visible = false;
                }
                else if (strTPView == "1")
                {

                    grdTP.Columns[3].Visible = true;
                    grdTP.Columns[4].Visible = true;
                    grdTP.Columns[5].Visible = false;
                    grdTP.Columns[6].Visible = false;
                    grdTP.Columns[7].Visible = false;
                    grdTP.Columns[8].Visible = false;
                }
                else if (strTPView == "0" || strTPView == "")
                {
                    grdTP.Columns[3].Visible = true;
                    grdTP.Columns[4].Visible = true;
                    grdTP.Columns[5].Visible = false;
                    grdTP.Columns[6].Visible = false;
                    grdTP.Columns[7].Visible = false;
                    grdTP.Columns[8].Visible = false;

                }
            }

        }
        catch (Exception ex)
        {

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

    protected void binddropdown(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdTP.Rows)
        {
            DropDownList ddlWT = (DropDownList)row.FindControl("ddlWT");
            DropDownList ddlTerr = (DropDownList)row.FindControl("ddlTerr");
            TourPlan tp = new TourPlan();
            dsTP = tp.FetchTerritory(sf_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                ddlTerr.DataSource = dsTP;
                ddlTerr.DataValueField = "Territory_Code";
                ddlTerr.DataTextField = "Territory_Name";
                ddlTerr.DataBind();
            }

        }
    }
    protected void ddlWT_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        //row.Focus();
        DropDownList ddlWT = (DropDownList)row.FindControl("ddlWT");
        DropDownList ddlTerr = (DropDownList)row.FindControl("ddlTerr");
        Dropdownvalue(ddlWT, ddlTerr);
    }

    protected void ddlWT1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        //row.Focus();
        DropDownList ddlWT = (DropDownList)row.FindControl("ddlWT1");
        DropDownList ddlTerr = (DropDownList)row.FindControl("ddlTerr1");
        Dropdownvalue(ddlWT, ddlTerr);
    }

    protected void ddlWT2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        //row.Focus();
        DropDownList ddlWT = (DropDownList)row.FindControl("ddlWT2");
        DropDownList ddlTerr = (DropDownList)row.FindControl("ddlTerr2");
        Dropdownvalue(ddlWT, ddlTerr);
    }

    protected void Dropdownvalue(DropDownList ddlWT, DropDownList ddlTerr)
    {
        TourPlan tpRPDisable = new TourPlan();
        DataSet dsRPDisable = new DataSet();

        dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT.SelectedItem.Text,div_code);
        if (dsRPDisable.Tables[0].Rows.Count > 0)
        {
            ddlTerr.Enabled = false;
        }
        else
        {
            ddlTerr.Enabled = true;
        }

        if (ddlWT.SelectedItem.Text == "Field Work")
        {
            dsRPDisable = tpRPDisable.GetTPWorkTypeFieldWork(sf_code);
            if (dsRPDisable.Tables[0].Rows.Count > 0)
            {
                ddlTerr.DataSource = dsRPDisable;
                ddlTerr.DataValueField = "Territory_Code";
                ddlTerr.DataTextField = "Territory_Name";
                ddlTerr.DataBind();
            }
        }
        else
        {
            TourPlan tp = new TourPlan();
            dsTP = tp.FetchTerritory(sf_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                ddlTerr.DataSource = dsTP;
                ddlTerr.DataValueField = "Territory_Code";
                ddlTerr.DataTextField = "Territory_Name";
                ddlTerr.DataBind();
            }
        }
    }
    private void FillTPDate()
    {
        TourPlan tp = new TourPlan();

        DataSet dsCount = tp.Find_TP_MMYYYY(sf_code, Convert.ToInt32(ddlMonth.SelectedValue.ToString()), Convert.ToInt32(ddlYear.SelectedValue.ToString()));

        if (dsCount.Tables[0].Rows.Count <= 0)
        {
            btnClear.Visible = false;
            btnSave.Visible = false;

            //lblComment.Visible = true;
            //grdTP.Visible = false;

            //lblComment.Text = "TP No Found for Edit";
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('There is no Tour Plan available for this month!!');</script>");                                            
        }
        else
        {
            //lblComment.Visible = false;
            dsTP = tp.get_TP_Active_Date(sf_code, Convert.ToInt32(ddlMonth.SelectedValue.ToString()), Convert.ToInt32(ddlYear.SelectedValue.ToString()));
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                dt_TP_Active_Date = Convert.ToDateTime(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            }
            
            dsTP = tp.getEmptyTourPlan(dt_TP_Active_Date);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                grdTP.Visible = true;
                grdTP.DataSource = dsTP;
                grdTP.DataBind();
            }
            else
            {
                grdTP.DataSource = dsTP;
                grdTP.DataBind();
            }

        }
        
    }


    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        

        string strDate = string.Empty;
        UnListedDR LstDR = new UnListedDR();
        sf_code = Session["sf_code"].ToString();
        dsHoliday = LstDR.getState(sf_code);
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            state_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            //div_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
        }

        TourPlan tp = new TourPlan();
        dsTP = tp.get_TP_Active_Date(sf_code, Convert.ToInt32(ddlMonth.SelectedValue.ToString()), Convert.ToInt32(ddlYear.SelectedValue.ToString()));
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            if (dsTP.Tables[0].Rows[0]["tour_date"].ToString() != "")
            {
                dt_TP_Active_Date = Convert.ToDateTime(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            Label lblDate = (Label)e.Row.FindControl("lblDate");
            Label lblDay = (Label)e.Row.FindControl("lblDay");
            DropDownList ddlWT = (DropDownList)e.Row.FindControl("ddlWT");
            DropDownList ddlWT1 = (DropDownList)e.Row.FindControl("ddlWT1");
            DropDownList ddlWT2 = (DropDownList)e.Row.FindControl("ddlWT2");
            DropDownList ddlTerr = (DropDownList)e.Row.FindControl("ddlTerr");
            DropDownList ddlTerr1 = (DropDownList)e.Row.FindControl("ddlTerr1");
            DropDownList ddlTerr2 = (DropDownList)e.Row.FindControl("ddlTerr2");            
            TextBox txtObjective = (TextBox)e.Row.FindControl("txtObjective");

            Dropdownvalue(ddlWT, ddlTerr);
            Dropdownvalue(ddlWT, ddlTerr1);
            Dropdownvalue(ddlWT, ddlTerr2);

            if (lblSNo != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                dt_TP_Current_Date = dt_TP_Active_Date.AddDays(Convert.ToInt32(lblSNo.Text) - 1);
                lblDay.Text = dt_TP_Current_Date.DayOfWeek.ToString();
                //lblDate.Text = dt_TP_Current_Date.ToString("MM/dd/yyyy");
                lblDate.Text = dt_TP_Current_Date.ToString("dd/MM/yyyy");

                dsHoliday = tp.getHolidays(state_code, div_code, lblDate.Text);
                if (dsHoliday.Tables[0].Rows.Count > 0)
                {
                    lblDay.BackColor = System.Drawing.Color.LightGreen;
                    lblSNo.BackColor = System.Drawing.Color.LightGreen;
                    lblDate.BackColor = System.Drawing.Color.LightGreen;

                    ddlWT.Enabled = true;
                    ddlWT1.Enabled = false;
                    ddlWT.Enabled = false;
                    ddlTerr.Enabled = true;
                    ddlTerr1.Enabled = true;
                    ddlTerr2.Enabled = true;
                    txtObjective.Enabled = true;
                    DataSet dsWeekoff = null;
                    dsWeekoff = tp.get_Holiday_DivCode(div_code);
                    ddlWT.SelectedValue = dsWeekoff.Tables[0].Rows[0]["WorkType_Code_B"].ToString();
                    txtObjective.Enabled = false;
                    txtObjective.Text = Convert.ToString(dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                    ddlTerr.SelectedItem.Text = "0";
                    ddlTerr1.SelectedItem.Text = "0";
                    ddlTerr2.SelectedItem.Text = "0";
                }

                dsWeek = tp.get_WeekOff(sf_code);
                if (dsWeek.Tables[0].Rows.Count > 0)
                {
                    string[] strSplitWeek = dsWeek.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Split(',');
                    foreach (string strWeek in strSplitWeek)
                    {
                        if (strWeek != "")
                        {
                            iWeek = Convert.ToInt32(strWeek);
                            if (lblDay.Text.Trim() == getDays(iWeek))
                            {
                                lblDay.BackColor = System.Drawing.Color.LightPink;
                                lblSNo.BackColor = System.Drawing.Color.LightPink;
                                lblDate.BackColor = System.Drawing.Color.LightPink;
                                ddlWT.Enabled = true;
                                ddlTerr.Enabled = false;
                                ddlTerr1.Enabled = false;
                                ddlTerr2.Enabled = false;
                                DataSet dsWeekoff = null;
                                dsWeekoff = tp.get_WeekOff_Divcode(div_code);
                                ddlWT.SelectedValue = dsWeekoff.Tables[0].Rows[0]["WorkType_Code_B"].ToString();
                                txtObjective.Enabled = false;
                                txtObjective.Text = txtObjective.Text + " Weekly Off";
                                ddlWT.SelectedValue = dsWeekoff.Tables[0].Rows[0]["WorkType_Code_B"].ToString();
                                ddlWT1.SelectedValue = dsWeekoff.Tables[0].Rows[0]["WorkType_Code_B"].ToString();
                                ddlWT2.SelectedValue = dsWeekoff.Tables[0].Rows[0]["WorkType_Code_B"].ToString();
                            }
                        }
                    }
                }

                dsTP = tp.get_TP_Details(sf_code, lblDate.Text);
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    string strTerr = dsTP.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    string strTerr1 = dsTP.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    string strTerr2 = dsTP.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                    ddlWT.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(5).ToString());
                    ddlWT1.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(7).ToString());
                    ddlWT2.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(9).ToString());

                    ddlTerr.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(11).ToString());
                    ddlTerr1.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
                    ddlTerr2.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(13).ToString());

                    Territory terr = new Territory();
                    DataSet dsActiveFlag = new DataSet();
                    DataSet dsTerritory = new DataSet();

                    dsTerritory = terr.getWorkAreaName(div_code);                   
                    dsActiveFlag = tp.FetchTerritory_Active_Flag(sf_code, ddlTerr.SelectedValue);
                    if (dsActiveFlag.Tables[0].Rows.Count > 0)
                    {
                        if (dsActiveFlag.Tables[0].Rows[0][0].ToString() != "1")
                        {
                            if (strTerr != "0" || strTerr1 != "0" || strTerr2 != "0")
                            {
                                txtObjective.Text = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                                if (strTerr != "0")
                                {
                                    ddlTerr.Items.FindByText(dsTP.Tables[0].Rows[0].ItemArray.GetValue(2).ToString()).Selected = true;
                                }
                                if (strTerr1 != "0")
                                {
                                    ddlTerr1.Items.FindByText(dsTP.Tables[0].Rows[0].ItemArray.GetValue(3).ToString()).Selected = true;
                                }

                                if (strTerr2 != "0")
                                {
                                    ddlTerr2.Items.FindByText(dsTP.Tables[0].Rows[0].ItemArray.GetValue(4).ToString()).Selected = true;
                                }
                            }
                        }
                        else
                        {
                            ddlTerr.SelectedValue = "0";
                            ddlTerr1.SelectedValue = "0";
                            ddlTerr2.SelectedValue = "0";
                        }
                    }

                    TourPlan tpRPDisable = new TourPlan();
                    DataSet dsRPDisable = new DataSet();

                    dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT.SelectedItem.Text, div_code);
                    if (dsRPDisable.Tables[0].Rows.Count > 0)
                    {
                        ddlTerr.Enabled = false;
                    }
                    else
                    {
                        ddlTerr.Enabled = true;
                    }

                    dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT1.SelectedItem.Text,div_code);
                    if (dsRPDisable.Tables[0].Rows.Count > 0)
                    {
                        ddlTerr1.Enabled = false;
                    }
                    else
                    {
                        ddlTerr1.Enabled = true;
                    }

                    dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT2.SelectedItem.Text,div_code);
                    if (dsRPDisable.Tables[0].Rows.Count > 0)
                    {
                        ddlTerr2.Enabled = false;
                    }
                    else
                    {
                        ddlTerr2.Enabled = true;
                    }
                }

                DataSet dsTPConfirmed = null;
                dsTPConfirmed = tp.GetTPConfirmed_Date(ddlMonth.SelectedValue, ddlYear.SelectedValue,sf_code);
                string strConfirmed = dsTPConfirmed.Tables[0].Rows[0]["Confirmed_Date"].ToString();
                if (strConfirmed != "0")
                {                    
                    DataSet dsTPEdit = new DataSet();
                    dsTPEdit = tp.GetTPEdit(sf_code, ddlMonth.SelectedValue, ddlYear.SelectedValue, lblDate.Text + ",");
                       if(dsTPEdit.Tables[0].Rows.Count>0)
                       {
                            ddlWT.Enabled =true;
                            ddlTerr.Enabled = true;
                            ddlTerr1.Enabled = true;
                            ddlTerr2.Enabled = true;
                            txtObjective.Enabled = true;
                            ddlWT.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0E0B2");
                            ddlTerr.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0E0B2");
                            ddlTerr1.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0E0B2");
                            ddlTerr2.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0E0B2");
                        }
                       else
                       {
                           ddlWT.Enabled = false;
                           ddlTerr.Enabled = false;
                           ddlTerr1.Enabled = false;
                           ddlTerr2.Enabled = false;
                           txtObjective.Enabled = false;
                       }
                    }  
            }
            
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[4].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                e.Row.Cells[5].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                e.Row.Cells[6].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        }

        

    }

    private string getDays(int iDay)
    {
        string sWeek = string.Empty;

        if (iDay == 0)
        {
            sWeek = "Sunday";
        }
        else if (iDay == 1)
        {
            sWeek = "Monday";
        }
        else if (iDay == 2)
        {
            sWeek = "Tuesday";
        }
        else if (iDay == 3)
        {
            sWeek = "Wednesday";
        }
        else if (iDay == 4)
        {
            sWeek = "Thursday";
        }
        else if (iDay == 5)
        {
            sWeek = "Friday";
        }
        else if (iDay == 6)
        {
            sWeek = "Saturday";
        }

        return sWeek;
    }

    protected DataSet FillTerritory()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.FetchTerritory(sf_code);
        foreach (GridViewRow gridRow in grdTP.Rows)
        {
            DropDownList ddlWT = (DropDownList)gridRow.Cells[3].FindControl("ddlWT");
            for (int i = 0; i < ddlWT.Items.Count; i++)
            {
                if (ddlWT.Items[i].Text == "Field Work")
                {
                    ddlWT.Items[i].Attributes.Add("Class", "DropDown");
                }
            }

        }
        if (dsTP.Tables[0].Rows.Count <= 1)
        {
            //Response.Redirect("../Territory/TerritoryCreation.aspx");
            Server.Transfer("TerritoryCreation.aspx");
            //menu1.Status = "Territory must be created prior to Doctor creation";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Territory must be created prior to Doctor creation');</script>");
        }
        return dsTP;
    }
    protected DataSet FillWorkType()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.FetchWorkType_New(div_code);
        if (dsTP.Tables[0].Rows.Count <= 1)
        {
           // menu1.Status = "Worktype must be loaded";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Worktype must be loaded');</script>");
        }
        return dsTP;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
         string confirmValue = Request.Form["confirm_value"];
         if (confirmValue == "Yes")
         {
             System.Threading.Thread.Sleep(time);
             int iReturn = -1;
             TP_Submit = true;

             foreach (GridViewRow gridRow in grdTP.Rows)
             {
                 Label lblDate = (Label)gridRow.Cells[1].FindControl("lblDate");
                 TP_Date = lblDate.Text.ToString();
                 Label lblDay = (Label)gridRow.Cells[2].FindControl("lblDay");
                 TP_Day = lblDay.Text.ToString();

                 DropDownList ddlWork_Type = (DropDownList)gridRow.Cells[3].FindControl("ddlWT");
                 ddlWT = ddlWork_Type.SelectedValue.ToString();
                 ddlWT_Name = ddlWork_Type.SelectedItem.Text;

                 DropDownList ddlWork_Type1 = (DropDownList)gridRow.Cells[5].FindControl("ddlWT1");
                 ddlValueWT1 = ddlWork_Type1.SelectedValue.ToString();
                 //ddlTextWT1 = ddlWork_Type1.SelectedItem.Text;

                 if (ddlValueWT1 == "0")
                 {
                     ddlTextWT1 = "0";
                 }
                 else
                 {
                     ddlTextWT1 = ddlWork_Type1.SelectedItem.Text;
                 }

                 DropDownList ddlWork_Type2 = (DropDownList)gridRow.Cells[5].FindControl("ddlWT2");
                 ddlValueWT2 = ddlWork_Type2.SelectedValue.ToString();
                 //ddlTextWT2 = ddlWork_Type2.SelectedItem.Text;

                 if (ddlValueWT2 == "0")
                 {
                     ddlTextWT2 = "0";
                 }
                 else
                 {
                     ddlTextWT2 = ddlWork_Type2.SelectedItem.Text;
                 }

                 DropDownList ddlTerritory_Type = (DropDownList)gridRow.Cells[3].FindControl("ddlTerr");
                 TP_Terr = ddlTerritory_Type.SelectedValue.ToString();
                 //TP_Terr_Name = ddlTerritory_Type.SelectedItem.Text;

                 if (TP_Terr == "0")
                 {
                     TP_Terr_Name = "0";
                 }
                 else
                 {
                     TP_Terr_Name = ddlTerritory_Type.SelectedItem.Text;
                 }

                 DropDownList ddlTerritory_Type1 = (DropDownList)gridRow.Cells[4].FindControl("ddlTerr1");
                 TP_Terr1 = ddlTerritory_Type1.SelectedValue.ToString();

                 if (TP_Terr1 == "0")
                 {
                     TP_Terr1_Name = "0";
                 }
                 else
                 {
                     TP_Terr1_Name = ddlTerritory_Type1.SelectedItem.Text;
                 }

                 DropDownList ddlTerritory_Type2 = (DropDownList)gridRow.Cells[5].FindControl("ddlTerr2");
                 TP_Terr2 = ddlTerritory_Type2.SelectedValue.ToString();


                 if (TP_Terr2 == "0")
                 {
                     TP_Terr2_Name = "0";
                 }
                 else
                 {
                     TP_Terr2_Name = ddlTerritory_Type2.SelectedItem.Text;
                 }

                 TextBox txtObjective = (TextBox)gridRow.Cells[4].FindControl("txtObjective");
                 TP_Objective = txtObjective.Text.Trim();

                 
                 // Add New Tour Plan
                 TourPlan tp = new TourPlan();
                 iReturn = tp.RecordAdd(TP_Date, TP_Day, TP_Terr_Name, TP_Terr1_Name, TP_Terr2_Name, ddlWT, ddlWT_Name, TP_Objective, TP_Submit, Session["sf_code"].ToString(), TP_Terr, TP_Terr1, TP_Terr2, ddlValueWT1, ddlTextWT1, ddlValueWT2, ddlTextWT2, Session["sf_name"].ToString());
                
             }

             if (iReturn != -1)
             {
                 if (TP_Submit == false)
                 {
                     // menu1.Status = "TourPlan Created Successfully!!";
                     ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                 }
                 else
                 {
                     //menu1.Status = "TourPlan submitted Successfully!!";
                     ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submitted Successfully');window.location='TourPlan_Entry.aspx'</script>");
                 }
                 FillTerritory();

             }
         }
        
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        btnSave.Visible = true;
        //btnClear.Visible = true;
        FillTPDate();
    }
}