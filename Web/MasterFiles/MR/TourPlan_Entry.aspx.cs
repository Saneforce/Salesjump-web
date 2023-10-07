using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_TPEntry : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsTP = null;
    DataSet dsTPC = null;
    DataSet dsWeek = null;
    DataSet dsHoliday = null;
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string state_code = string.Empty;
    string TP_Date = string.Empty;
    string TP_Day = string.Empty;
    string TP_Terr_Value = string.Empty;
    string TP_Terr_Name = string.Empty;
    string strddlWT = string.Empty;
    string strddlFWText = string.Empty;
    string TP_Terr1_Value = string.Empty;
    string TP_Terr1_Name = string.Empty;
    string TP_Terr2_Value = string.Empty;
    string TP_Terr2_Name = string.Empty;
    bool TP_Submit = false;
    bool EmptyWT = false;
    bool EmptyTerr = false;
    string ddlWT = string.Empty;
    string ddlWT1 = string.Empty;
    string ddlValueWT1 = string.Empty;
    string ddlTextWT1 = string.Empty;
    string ddlValueWT2 = string.Empty;
    string ddlTextWT2 = string.Empty;
    string strTPView = string.Empty;
    int TP_Month = -1;
    int TP_Year = -1;
    DateTime TP_Submit_Date;
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
        lblStatingDate.Visible=false;
        if (!Page.IsPostBack)
        {
            binddropdown(sender, e);
            Session["backurl"] = "LstDoctorList.aspx";  
            menu1.FindControl("btnBack").Visible = false;
            GetTitle();
            FillTPDate();
            ServerStartTime = DateTime.Now;
            TourPlanTerritory();            
            base.OnPreInit(e);
        }
        FillWorkType();
    }

    protected void TourPlanTerritory()
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
            else
            {
                grdTP.Columns[6].Visible = false;
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void GetTitle()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_ApprovalTitle(sf_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            lblHead.Text = "Month Tour Plan - Entry For " + "<span style='color:Red;'>" + dsTP.Tables[0].Rows[0]["Sf_Name"] + "</span>";

            
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

    private void FillTPDate()
    {
        TourPlan tp = new TourPlan();

        dsTP = tp.get_TP_Active_Date(sf_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            dt_TP_Active_Date = Convert.ToDateTime(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            dsTPC = tp.checkmonth(sf_code, Convert.ToString(dt_TP_Active_Date.Month));

            if (dsTPC.Tables[0].Rows.Count == 0)
            {               
                lblmon.Text = getMonth(Convert.ToInt16(dt_TP_Active_Date.Month));
                lblmon.Text =" - "+ lblmon.Text + " - " + (Convert.ToInt16(dt_TP_Active_Date.Year));
                DataSet dsTPStart = new DataSet();
                dsTPStart = tp.Get_TP_Start_Title(sf_code);
                if (dsTPStart.Tables[0].Rows.Count == 0)
                {
                    //lblmon.Text = lblmon.Text + " - " + (Convert.ToInt16(dt_TP_Active_Date.Year))+ "<span style='font-style:normal'> ( Joining Date " + dt_TP_Active_Date.ToString("dd/MM/yyyy")+" )" + "</span>";
                    lblStatingDate.Visible = true;
                    lblStatingDate.Text = "<span style='font-style:normal;color:Blue'> Joining Date : </span><span style='font-style:normal'>" + dt_TP_Active_Date.ToString("dd/MM/yyyy") + "</span>";
                }
                dsTP = tp.getEmptyTourPlan(dt_TP_Active_Date);
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    grdTP.Visible = true;
                    grdTP.DataSource = dsTP;
                    grdTP.DataBind();
                }
            }
            else
            {             
                btnSave.Visible = false; 
                btnSubmit.Visible = false;              

                dsTP = tp.get_TP_Submission_Date(sf_code);
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    lblHead.Text = "Your " + "<span style='color:#FF3300'>" + dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "</span>" +
                                   " TP not yet approved by your manager (" + "<span style='color:Green'>" + dsTP.Tables[1].Rows[0].ItemArray.GetValue(0).ToString() + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"] + "</span>" + ")";
                    hylEdit.Text = "Yes";
                    lblLink.Text = "Before Approval by your Manager - Do you want to change your TP - ";
                    tblMargin.Style.Add("margin-top", "140px");                  
                   
                }
                else
                {
                    grdTP.Visible = true;
                    grdTP.DataSource = dsTP;
                    grdTP.DataBind();
                }

            }
        }
    }

    protected void GrdTP_ddlWTSelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        //row.Focus();
        DropDownList ddlWT = (DropDownList)row.FindControl("ddlWT");
        DropDownList ddlTerr = (DropDownList)row.FindControl("ddlTerr");
        Dropdownvalue(ddlWT, ddlTerr);
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

    protected void Dropdownvalue(DropDownList ddlWT, DropDownList ddlTerr)
    {
        
        TourPlan tpRPDisable = new TourPlan();
        DataSet dsRPDisable = new DataSet();
        //dsRPDisable = tpRPDisable.FetchTerritory_Rejected_Status(sf_code, dt_TP_Active_Date.Month, div_code);
        if (ddlWT.SelectedIndex > 0)
        {
            dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT.SelectedItem.Text, div_code);
            if (dsRPDisable.Tables[0].Rows.Count > 0)
            {
                ddlTerr.Enabled = false;
                ddlTerr.BackColor = System.Drawing.Color.LightGray;
                ddlTerr.ToolTip = "Disabled!!";
            }
            else
            {
                ddlTerr.Enabled = true;
                ddlTerr.ToolTip = "Enabled";
                ddlTerr.BackColor = System.Drawing.Color.White;
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
            ddlTerr.Enabled = false;
            ddlTerr.ToolTip = "Disabled!!";
            ddlTerr.BackColor = System.Drawing.Color.LightGray;
        }
    }



    protected void GrdTP1_ddlWT1SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        //row.Focus();
        DropDownList ddlWT = (DropDownList)row.FindControl("ddlWT1");
        DropDownList ddlTerr = (DropDownList)row.FindControl("ddlTerr1");
        Dropdownvalue(ddlWT, ddlTerr);       
    }

    protected void GrdTP2_ddlWT1SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        //row.Focus();
        DropDownList ddlWT = (DropDownList)row.FindControl("ddlWT2");
        DropDownList ddlTerr = (DropDownList)row.FindControl("ddlTerr2");
        Dropdownvalue(ddlWT, ddlTerr); 
    }


    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {       

        UnListedDR LstDR = new UnListedDR();
        sf_code = Session["sf_code"].ToString();
        dsHoliday = LstDR.getState(sf_code);
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            state_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            //div_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
        }

        TourPlan tp = new TourPlan();
        dsTP = tp.get_TP_Active_Date(sf_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            dt_TP_Active_Date = Convert.ToDateTime(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
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

            //Dropdownvalue(ddlWT, ddlTerr);
           // Dropdownvalue(ddlWT, ddlTerr1);
           // Dropdownvalue(ddlWT, ddlTerr2);

            if (lblSNo != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                dt_TP_Current_Date = dt_TP_Active_Date.AddDays(Convert.ToInt32(lblSNo.Text) - 1);
                lblDay.Text = dt_TP_Current_Date.DayOfWeek.ToString();
                lblDate.Text = dt_TP_Current_Date.ToString("dd/MM/yyyy");

                dsHoliday = tp.getHolidays(state_code, div_code, lblDate.Text);
                if (dsHoliday.Tables[0].Rows.Count > 0)
                {
                    lblDay.BackColor = System.Drawing.Color.LightGreen;
                    lblSNo.BackColor = System.Drawing.Color.LightGreen;
                    lblDate.BackColor = System.Drawing.Color.LightGreen;

                    ddlWT.Enabled = false;
                    ddlWT1.Enabled = false;
                    ddlWT.Enabled = false;
                    ddlTerr.Enabled = false;
                    ddlTerr1.Enabled = false;
                    ddlTerr2.Enabled = false;
                    DataSet dsWeekoff = null;
                    dsWeekoff = tp.get_Holiday_DivCode(div_code);
                    ddlWT.SelectedValue = dsWeekoff.Tables[0].Rows[0]["WorkType_Code_B"].ToString();
                    txtObjective.Enabled = false;
                    txtObjective.Text = Convert.ToString(dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                    ddlTerr.SelectedItem.Text = "0";
                    ddlTerr1.SelectedItem.Text = "0";
                    ddlTerr2.SelectedItem.Text = "0";
                }
                else
                {
                    //DataSet dsWeekoff = null;
                    //dsWeekoff = tp.get_FieldWork("Field Work");
                    //ddlWT.SelectedValue = dsWeekoff.Tables[0].Rows[0]["WorkType_Code_B"].ToString();  // Set Worktype as "Field Work" as a default value
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
                                ddlWT.Enabled = false;
                                ddlWT1.Enabled = false;
                                ddlWT2.Enabled = false;
                                ddlTerr.Enabled = false;
                                ddlTerr1.Enabled = false;
                                ddlTerr2.Enabled = false;
                                txtObjective.Enabled = false;
                                txtObjective.Text = "Weekly Off";
                                DataSet dsWeekoff = null;
                                dsWeekoff = tp.get_WeekOff_Divcode(div_code);
                                ddlWT.SelectedValue = dsWeekoff.Tables[0].Rows[0]["WorkType_Code_B"].ToString();
                                ddlWT1.SelectedValue = dsWeekoff.Tables[0].Rows[0]["WorkType_Code_B"].ToString();
                                ddlWT2.SelectedValue = dsWeekoff.Tables[0].Rows[0]["WorkType_Code_B"].ToString();
                                //ddlTerr.SelectedItem.Text = "0";
                                //ddlTerr1.SelectedItem.Text = "0";
                            }
                        }
                        //else
                        //{
                        //    //ddlWT.SelectedValue = "77";  // Set Worktype as "Field Work" as a default value                            
                        //}
                    }
                }

                // Saved but not submitted
                dsTP = tp.get_TP_Draft(sf_code, lblDate.Text);
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    string strTerr = dsTP.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    string strTerr1 = dsTP.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    string strTerr2 = dsTP.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    txtObjective.Text = dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    ddlWT.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(5).ToString());
                    ddlWT1.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(8).ToString());
                    ddlWT2.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(10).ToString());

                    ddlTerr.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
                    ddlTerr1.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(13).ToString());
                    ddlTerr2.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(14).ToString());

                    Territory terr = new Territory();
                    DataSet dsActiveFlag = new DataSet();
                    DataSet dsTerritory = new DataSet();

                    dsTerritory = terr.getWorkAreaName(div_code);                   
                    dsActiveFlag = tp.FetchTerritory_Active_Flag(sf_code, ddlTerr.SelectedValue);
                    if (dsActiveFlag.Tables[0].Rows.Count > 0)
                    {
                        if (dsActiveFlag.Tables[0].Rows[0][0].ToString() != "1")
                        {
                            if (strTerr == "0" || strTerr1 == "0" || strTerr2 == "0")
                            {
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

                                if (dsTP.Tables[0].Rows[0]["Rejection_Reason"].ToString() != "")
                                {
                                    lblmon.Text = getMonth(Convert.ToInt16(dt_TP_Active_Date.Month)) + " " + Convert.ToInt16(dt_TP_Active_Date.Year);
                                    lblReason.Text = "Your TP has been Rejected  " + lblmon.Text + "<br> Rejected Reason: "
                                                      + dsTP.Tables[0].Rows[0]["Rejection_Reason"].ToString();
                                    lblmon.Text = "";
                                    lblmon.Text = " - " + getMonth(Convert.ToInt16(dt_TP_Active_Date.Month)) + " " + Convert.ToInt16(dt_TP_Active_Date.Year) + "<span style='color:Black'> ( Resubmit for Rejection ) </span>";
                                    lblNote.Visible = true;
                                    lblReason.Visible = true;
                                    btnSave.Visible = false;
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
                    
                }

                DataSet dsTPConfirmed = null;
                dsTPConfirmed = tp.GetTPConfirmed_Date(dt_TP_Current_Date.Month.ToString(), dt_TP_Current_Date.Year.ToString(), sf_code);
                string strConfirmed = dsTPConfirmed.Tables[0].Rows[0]["Confirmed_Date"].ToString();
                if (strConfirmed != "0")
                {
                    DataSet dsTPEdit = new DataSet();
                    dsTPEdit = tp.GetTPEdit(sf_code, dt_TP_Current_Date.Month.ToString(), dt_TP_Current_Date.Year.ToString(), lblDate.Text + ",");
                    if (dsTPEdit.Tables[0].Rows.Count > 0)
                    {
                        ddlWT.Enabled = true;
                        ddlWT1.Enabled = true;
                        ddlWT2.Enabled = true;
                        ddlTerr.Enabled = true;
                        ddlTerr1.Enabled = true;
                        ddlTerr2.Enabled = true;
                        txtObjective.Enabled = true;
                    }
                    else
                    {
                        ddlWT.Enabled = false;
                        ddlWT1.Enabled = false;
                        ddlWT2.Enabled = false;
                        ddlTerr.Enabled = false;
                        ddlTerr1.Enabled = false;
                        ddlTerr2.Enabled = false;
                        txtObjective.Enabled = false;
                    }
                }

                TourPlan tpRPDisable = new TourPlan();
                DataSet dsRPDisable = new DataSet();

                //dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT.SelectedItem.Text, div_code);
                //if (dsRPDisable.Tables[0].Rows.Count > 0)
                //{
                //    ddlTerr.Enabled = false;
                //}
                //else
                //{
                //    ddlTerr.Enabled = true;
                //}
                if (ddlWT.SelectedIndex > 0)
                {
                    dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT.SelectedItem.Text, div_code);
                    if (dsRPDisable.Tables[0].Rows.Count > 0)
                    {
                        ddlTerr.Enabled = false;
                        ddlTerr.BackColor = System.Drawing.Color.LightGray;
                        ddlTerr.ToolTip = "Disabled!!";
                    }
                    else
                    {
                        ddlTerr.Enabled = true;
                        ddlTerr.ToolTip = "Select";
                        ddlTerr.BackColor = System.Drawing.Color.White;
                    }
                }
                else
                {
                    ddlTerr.Enabled = false;
                    ddlTerr.ToolTip = "Disabled!!";
                    ddlTerr.BackColor = System.Drawing.Color.LightGray;
                }
                if (ddlWT1.SelectedIndex > 0)
                {
                    dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT1.SelectedItem.Text, div_code);
                    if (dsRPDisable.Tables[0].Rows.Count > 0)
                    {
                        ddlTerr1.Enabled = false;
                        ddlTerr1.BackColor = System.Drawing.Color.LightGray;
                        ddlTerr1.ToolTip = "Disabled!!";
                    }
                    else
                    {
                        ddlTerr1.Enabled = true;
                        ddlTerr1.ToolTip = "Select";
                        ddlTerr1.BackColor = System.Drawing.Color.White;
                    }
                }
                else
                {
                    ddlTerr1.Enabled = false;
                    ddlTerr1.ToolTip = "Disabled!!";
                    ddlTerr1.BackColor = System.Drawing.Color.LightGray;
                }
                if (ddlWT2.SelectedIndex > 0)
                {

                    dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT2.SelectedItem.Text, div_code);
                    if (dsRPDisable.Tables[0].Rows.Count > 0)
                    {
                        ddlTerr2.Enabled = false;
                        ddlTerr2.BackColor = System.Drawing.Color.LightGray;
                        ddlTerr2.ToolTip = "Disabled!!";
                    }
                    else
                    {
                        ddlTerr2.Enabled = true;
                        ddlTerr2.ToolTip = "Select";
                        ddlTerr2.BackColor = System.Drawing.Color.White;
                    }
                }
                else
                {
                    ddlTerr2.Enabled = false;
                    ddlTerr2.BackColor = System.Drawing.Color.LightGray;
                    ddlTerr2.ToolTip = "Disabled!!";
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
                e.Row.Cells[6].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                e.Row.Cells[8].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
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

    protected DataSet FillTerritory()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.FetchTerritory(sf_code);
        if (dsTP.Tables[0].Rows.Count <= 1)
        {           
            Server.Transfer("Territory/TerritoryCreation.aspx");
            
            //menu1.Status = "Routeplan must be created prior to TP creation";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Routeplan must be created prior to TP creation');</script>");
        }
        return dsTP;
    }
    protected DataSet FillWorkType()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.FetchWorkType_New(div_code);

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
            //menu1.Status = "Worktype must be loaded";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Worktype must be loaded');</script>");
        }
        return dsTP;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        TP_Submit = false; 
  
         string confirmValue = Request.Form["confirm_value"];
         if (confirmValue == "Yes")
         {
             //foreach (GridViewRow gridRow in grdTP.Rows)
             //{
             //    DropDownList ddlWT = (DropDownList)gridRow.Cells[3].FindControl("ddlWT");
             //    strddlWT = ddlWT.SelectedValue.ToString();
             //    strddlFWText = ddlWT.SelectedItem.Text.ToString();
             //    DropDownList ddlTerritory_Type = (DropDownList)gridRow.Cells[4].FindControl("ddlTerr");
             //    TP_Terr_Value = ddlTerritory_Type.SelectedItem.Text.ToString();
             //    Label lblDay = (Label)gridRow.Cells[2].FindControl("lblDay");
             //    TP_Day = lblDay.Text.ToString();

             //    //if ((strddlFWText == "Field Work" && TP_Terr_Value == "---Select---") && ((lblDay.BackColor != System.Drawing.Color.LightPink) && (lblDay.BackColor != System.Drawing.Color.LightGreen)))
             //    if (strddlFWText == "---Select---")
             //    {
             //        EmptyTerr = true;
             //    }
             //}
            // if (EmptyWT == false && EmptyTerr == false)
            // {
                 iReturn = CreateTP(TP_Submit);
                 if (iReturn != -1)
                 {
                     // menu1.Status = "TourPlan Submitted for Approval!!";
                     ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Saved Successfully');window.location='TourPlan_Entry.aspx'</script>");
                     grdTP.Visible = false;
                     FillTerritory();
                 }
             //}
             //else
             //{
             //    if (EmptyWT == true)
             //    {
             //        // menu1.Status = "Select Work Type for all the dates!!";
             //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Work Type for all the dates');</script>");
             //    }
             //    if (EmptyTerr == true)
             //    {
             //        // menu1.Status = "Select atleast one Route plan for Worktype selected as FIELD WORK!!";
             //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Worktype. ');</script>");
             //        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select atleast one Route plan for Worktype selected as FIELD WORK');</script>");
             //    }
             //}
         }
        
       
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
                lblNote.Visible = false;        
                lblReason.Visible = false;
                //System.Threading.Thread.Sleep(time);
                int iReturn = -1;
                TP_Submit = true;
        
             foreach (GridViewRow gridRow in grdTP.Rows)
             {
                 DropDownList ddlWT = (DropDownList)gridRow.Cells[3].FindControl("ddlWT");
                 strddlWT = ddlWT.SelectedValue.ToString();
                 strddlFWText = ddlWT.SelectedItem.Text.ToString();
                 DropDownList ddlTerritory_Type = (DropDownList)gridRow.Cells[4].FindControl("ddlTerr");
                 TP_Terr_Value = ddlTerritory_Type.SelectedItem.Text.ToString();
                 Label lblDay = (Label)gridRow.Cells[2].FindControl("lblDay");
                 TP_Day = lblDay.Text.ToString();

                 if (strddlFWText == "---Select---")
                 {
                     EmptyTerr = true;
                 }
             }

             if (EmptyWT == false && EmptyTerr == false)
             {
                 iReturn = CreateTP(TP_Submit);
                 if (iReturn != -1)
                 {
                     // menu1.Status = "TourPlan Submitted for Approval!!";
                     ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Submitted for Approval');window.location='TourPlan_Entry.aspx'</script>");
                     grdTP.Visible = false;
                     FillTerritory();
                 }
             }
             else
             {
                 if (EmptyWT == true)
                 {                    
                     ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Work Type for all the dates');</script>");
                 }
                 if (EmptyTerr == true)
                 {
                     //menu1.Status = "Select atleast one Route plan for Worktype selected as FIELD WORK!!";
                     ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Work Type. ');</script>");
                     // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select atleast one Route plan for Worktype selected as FIELD WORK');</script>");
                 }
             }
         }
    }

    private int CreateTP(bool TP_Submit)
    {
        foreach (GridViewRow gridRow in grdTP.Rows)
        {
            Label lblDate = (Label)gridRow.Cells[1].FindControl("lblDate");
            TP_Date = lblDate.Text.ToString();
            Label lblDay = (Label)gridRow.Cells[2].FindControl("lblDay");
            TP_Day = lblDay.Text.ToString();

            DropDownList ddlWork_Type = (DropDownList)gridRow.Cells[3].FindControl("ddlWT");
            ddlWT = ddlWork_Type.SelectedValue.ToString();
            if (ddlWT == "0")
            {
                ddlWT1 = "0";
            }
            else
            {
                ddlWT1 = ddlWork_Type.SelectedItem.Text;
            }

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

            DropDownList ddlWork_Type2 = (DropDownList)gridRow.Cells[7].FindControl("ddlWT2");
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

            DropDownList ddlTerritory_Type = (DropDownList)gridRow.Cells[4].FindControl("ddlTerr");
            TP_Terr_Value = ddlTerritory_Type.SelectedValue.ToString();          
            //TP_Terr_Name = ddlTerritory_Type.SelectedItem.Text;

            if (TP_Terr_Value == "0")
            {
                TP_Terr_Name = "0";
            }
            else
            {
                TP_Terr_Name = ddlTerritory_Type.SelectedItem.Text;
            }

            DropDownList ddlTerritory_Type1 = (DropDownList)gridRow.Cells[5].FindControl("ddlTerr1");
            TP_Terr1_Value= ddlTerritory_Type1.SelectedValue.ToString();

            if (TP_Terr1_Value == "0")
            {
                TP_Terr1_Name = "0";
            }
            else
            {
                TP_Terr1_Name = ddlTerritory_Type1.SelectedItem.Text;
            }

            DropDownList ddlTerritory_Type2 = (DropDownList)gridRow.Cells[6].FindControl("ddlTerr2");
            TP_Terr2_Value = ddlTerritory_Type2.SelectedValue.ToString();

            if (TP_Terr2_Value == "0")
            {
                TP_Terr2_Name = "0";
            }
            else
            {
                TP_Terr2_Name = ddlTerritory_Type2.SelectedItem.Text;
            }

            TextBox txtObjective = (TextBox)gridRow.Cells[4].FindControl("txtObjective");
            TP_Objective = txtObjective.Text.ToString();

            //if (ddlWT != "0" && TP_Terr_Name != "---Select---" || TP_Terr1_Value != "0")
            //{
                TourPlan tp = new TourPlan();
                iReturn = tp.RecordAdd(TP_Date, TP_Day, TP_Terr_Name, TP_Terr1_Name, TP_Terr2_Name, ddlWT, ddlWT1, TP_Objective, TP_Submit, Session["sf_code"].ToString(), TP_Terr_Value, TP_Terr1_Value, TP_Terr2_Value, ddlValueWT1, ddlTextWT1, ddlValueWT2, ddlTextWT2, Session["sf_name"].ToString());
                DataSet dsadmin = new DataSet();

            Designation Desig = new Designation();
            dsadmin = Desig.getDesignation_Sys_Approval(Session["Designation_Short_Name"].ToString(), div_code);
            DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
            string strSession = Session["Designation_Short_Name"].ToString();

            if (dsadmin.Tables[0].Rows.Count > 0)
            {
                if (dsadmin.Tables[0].Rows[0]["Designation_Short_Name"].ToString() == strSession && dsadmin.Tables[0].Rows[0]["tp_approval_Sys"].ToString() == "1")                
                {
                    iReturn = tp.Approve(sf_code, dt_TourPlan.Month.ToString(), dt_TourPlan.Year.ToString(), Session["sf_code"].ToString(), div_code, "", TP_Terr_Name, TP_Terr1_Name, TP_Terr2_Name, lblDate.Text, TP_Terr_Value, TP_Terr1_Value, TP_Terr2_Value, "", ddlValueWT1, ddlTextWT1, ddlValueWT2, ddlTextWT2, Session["sf_name"].ToString());
                    tp.TP_Delete(sf_code, dt_TourPlan.Month.ToString(), dt_TourPlan.Year.ToString());

                }
            }
                       


            //}
        }
        return iReturn;
    }
}