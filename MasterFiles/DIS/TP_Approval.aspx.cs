using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MGR_TP_Approval : System.Web.UI.Page
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
    string strTPView = string.Empty;
    string TP_Day = string.Empty;
    string TP_Terr = string.Empty;
    string TP_Terr_Name = string.Empty;
    string TP_Terr1_Name = string.Empty;
    string TP_Terr2_Name = string.Empty;
    string ddlValueWT1 = string.Empty;
    string ddlTextWT1 = string.Empty;
    string ddlValueWT2 = string.Empty;
    string ddlTextWT2 = string.Empty;
    bool TP_Submit = false;
    int TP_Month = -1;
    int TP_Year = -1;
    DateTime TP_Submit_Date;
    DateTime TP_Tour_Date;
    DataSet dsTerritory = new DataSet();
    string TP_Tour_Shedule = string.Empty;
    string TP_Objective = string.Empty;

    DateTime dt_TP_Active_Date;
    DateTime dt_TP_Current_Date;
    int i;
    int iWeek = -1;
    int iReturn = -1;
    string MR_Code = string.Empty;
    string MR_Month = string.Empty;
    string MR_Year = string.Empty;
    string sQryStr = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        sQryStr = Request.QueryString["refer"].ToString();
        MR_Code = sQryStr.Substring(0, sQryStr.IndexOf('-'));
        sQryStr = sQryStr.Substring(sQryStr.IndexOf('-') + 1, (sQryStr.Length - MR_Code.Length) -1);
        MR_Month = sQryStr.Substring(0, sQryStr.IndexOf('-'));
        sQryStr = sQryStr.Substring(sQryStr.IndexOf('-') + 1, (sQryStr.Length - MR_Month.Length) - 1);
        MR_Year = sQryStr.Trim();
        
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        lblStatingDate.Visible = false;
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "TP_Approve.aspx";
            //menu1.Title = "Tour Plan - Approval";
            menu1.Visible = false;
            FillTPDate();
            TourPlan();
            GetTitle();
        }

        
        FillWorkType();
    }

    private void FillTPDate()
    {
        TourPlan tp = new TourPlan();

        dsTP = tp.get_TP_Active_Date(MR_Code , Convert.ToInt32(MR_Month), Convert.ToInt32(MR_Year));

        if (dsTP.Tables[0].Rows.Count > 0)
        {
            dt_TP_Active_Date = Convert.ToDateTime(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
        }

        DataSet dsTPC = new DataSet();

        dsTP = tp.getEmptyTourPlan(dt_TP_Active_Date);
         lblmon.Text = getMonth(Convert.ToInt16(dt_TP_Active_Date.Month));
         lblmon.Text = " - "+ lblmon.Text +" - "+ Convert.ToString(dt_TP_Active_Date.Year);
        dsTPC = tp.checkmonth(MR_Code, Convert.ToString(dt_TP_Active_Date.Month));
       
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            grdTP.Visible = true;
            grdTP.DataSource = dsTP;
            grdTP.DataBind();
        }
        else
        {

        }

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

    protected void GetTitle()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_ApprovalTitle(MR_Code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            lblHead.Text = "Month Tour Plan - Entry For " + "<span style='color:Red'>" + dsTP.Tables[0].Rows[0]["Sf_Name"] + "</span>";

            DataSet dsTPStart = new DataSet();
            dsTPStart = tp.Get_TP_Start_Title(MR_Code);
            if (dsTPStart.Tables[0].Rows.Count == 0)
            {
                lblHead.Text = "Month Tour Plan - Entry For " + "<span style='color:Red'>" + dsTP.Tables[0].Rows[0]["Sf_Name"] + "</span>" + " " +
                              "<span style='color:Green'>" + dt_TP_Active_Date.ToString("MMMM") + " " + dt_TP_Active_Date.Year + "</span>";

                lblStatingDate.Visible = true;
                lblStatingDate.Text = "<span style='font-style:normal;color:Blue'> Joining Date : </span><span style='font-style:normal'>" + dt_TP_Active_Date.ToString("dd/MM/yyyy") + "</span>";
            }
        }
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
        dsTP = tp.get_TP_Active_Date(MR_Code , Convert.ToInt32(MR_Month ), Convert.ToInt32(MR_Year ));
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

                    ddlWT.Enabled = false;
                    ddlWT1.Enabled = false;
                    ddlWT2.Enabled = false;
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

                    //dsWeekoff = tp.get_FieldWork(dsTP.Tables[0].Rows[0][6].ToString());
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
                            }
                        }
                        //else
                        //{
                        //    //ddlWT.SelectedValue = "77";  // Set Worktype as "Field Work" as a default value                            
                        //}
                    }
                }

                dsTP = tp.get_TP_Details(MR_Code , lblDate.Text);
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    string strObjective = dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    string strTerr = dsTP.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    string strtTerr1 = dsTP.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    string strtTerr2 = dsTP.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                    ddlWT.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(5).ToString());
                    ddlWT1.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(7).ToString());
                    ddlWT2.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(9).ToString());

                    ddlTerr.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(11).ToString());
                    ddlTerr1.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
                    ddlTerr2.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(13).ToString());

                    DataSet dsActiveFlag = new DataSet();


                    //if (strObjective != "0" || strTerr != "0" || strtTerr1 != "0" || strtTerr2 != "0")
                    //{
                    //    txtObjective.Text = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    //    if (strTerr != "0")
                    //    {
                    //        ddlTerr.Items.FindByValue(dsTP.Tables[0].Rows[0].ItemArray.GetValue(2).ToString()).Selected = true;
                    //    }

                    //    if (strtTerr1 != "0")
                    //    {
                    //        ddlTerr1.Items.FindByText(dsTP.Tables[0].Rows[0].ItemArray.GetValue(3).ToString()).Selected = true;
                    //    }
                    //    if (strtTerr2 != "0")
                    //    {
                    //        ddlTerr2.Items.FindByText(dsTP.Tables[0].Rows[0].ItemArray.GetValue(4).ToString()).Selected = true;
                    //    }

                    //}

                    //dsActiveFlag = tp.FetchTerritory_Active_Flag(MR_Code, ddlTerr.SelectedItem.Value);

                   
        
                    Territory terr = new Territory();
                    
                    dsTerritory = terr.getWorkAreaName(div_code);
            
         

                    dsActiveFlag = tp.FetchTerritory_Active_Flag(MR_Code, ddlTerr.SelectedValue);
                    if (dsActiveFlag.Tables[0].Rows.Count > 0)
                    {
                        if (dsActiveFlag.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            for (int i = 0; i < ddlTerr.Items.Count; i++)
                            {
                                if (ddlTerr.Items[i].Value == ddlTerr.SelectedValue)
                                {
                                    ddlTerr.Items[i].Attributes.CssStyle.Add("color", "red");
                                    ddlTerr.ToolTip = "DeActivated!!";
                                    lblDeactivate.Visible=true;
                                    lblDeactivate.Text = "'Red Color indicates' Deleted " + "'" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "'" + " Before Preparing the TP Kindly Modify/Delete the " + "'" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "'" + " Reject the TP and submit once again for Approval.";
                                }
                            }
                            //dropdownlist1.selectedItem.Attribute.add("Style", "color:red");
                        }
                    }

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
                dsTPConfirmed = tp.GetTPConfirmed_Date(MR_Month, MR_Year,MR_Code);
                string strConfirmed = dsTPConfirmed.Tables[0].Rows[0]["Confirmed_Date"].ToString();
                if (strConfirmed != "0")
                {
                    DataSet dsTPEdit = new DataSet();
                    dsTPEdit = tp.GetTPEdit(MR_Code, MR_Month, MR_Year, lblDate.Text + ",");
                    if (dsTPEdit.Tables[0].Rows.Count > 0)
                    {
                        ddlWT.Enabled = true;
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
           // menu1.Status = "Worktype must be loaded";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Worktype must be loaded');</script>");
        }
        return dsTP;
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
       // dsTP = tp.FetchTerritory(MR_Code);
        dsTP = tp.FetchTerritory_Approval(MR_Code);
        if (dsTP.Tables[0].Rows.Count <= 1)
        {
            Response.Redirect("../Territory/TerritoryCreation.aspx");
            // menu1.Status = "Territory must be created prior to Doctor creation";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Territory must be created prior to Doctor creation');</script>");
        }
        return dsTP;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            int iReturn = -1;
            TourPlan tp = new TourPlan();
            foreach (GridViewRow gridRow in grdTP.Rows)
            {
                Label lblDate = (Label)gridRow.Cells[1].FindControl("lblDate");
                DropDownList ddlWork_Type = (DropDownList)gridRow.Cells[1].FindControl("ddlWT");
                string ddlWT_Value = ddlWork_Type.SelectedValue;
                string ddlWT_Name = ddlWork_Type.SelectedItem.Text;

                DropDownList ddlWork_Type1 = (DropDownList)gridRow.Cells[5].FindControl("ddlWT1");
                ddlValueWT1 = ddlWork_Type1.SelectedValue.ToString();
                ddlTextWT1 = ddlWork_Type1.SelectedItem.Text;

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
                ddlTextWT2 = ddlWork_Type2.SelectedItem.Text;

                if (ddlValueWT2 == "0")
                {
                    ddlTextWT2 = "0";
                }
                else
                {
                    ddlTextWT2 = ddlWork_Type2.SelectedItem.Text;
                }

                DropDownList ddlTerr = (DropDownList)gridRow.Cells[1].FindControl("ddlTerr");
                string TP_Terr_Value = ddlTerr.SelectedValue;
                //TP_Terr_Name = ddlTerr.SelectedItem.Text;

                if (TP_Terr_Value == "0")
                {
                    TP_Terr_Name = "0";
                }
                else
                {
                    TP_Terr_Name = ddlTerr.SelectedItem.Text;
                }


                DropDownList ddlTerr1 = (DropDownList)gridRow.Cells[1].FindControl("ddlTerr1");
                string TP_Terr1_Value = ddlTerr1.SelectedValue;

                if (TP_Terr1_Value == "0")
                {
                    TP_Terr1_Name = "0";
                }
                else
                {
                    TP_Terr1_Name = ddlTerr1.SelectedItem.Text;
                }

                DropDownList ddlTerr2 = (DropDownList)gridRow.Cells[1].FindControl("ddlTerr2");
                string TP_Terr2_Value = ddlTerr2.SelectedValue;

                if (TP_Terr2_Value == "0")
                {
                    TP_Terr2_Name = "0";
                }
                else
                {
                    TP_Terr2_Name = ddlTerr2.SelectedItem.Text;
                }

                //if (ddlWT_Value != "0" && TP_Terr_Name != "---Select---")
                //{
                iReturn = tp.Approve(MR_Code, MR_Month, MR_Year, Session["sf_code"].ToString(), div_code, ddlWT_Name, TP_Terr_Name, TP_Terr1_Name, TP_Terr2_Name, lblDate.Text, TP_Terr_Value, TP_Terr1_Value, TP_Terr2_Value, ddlWT_Value, ddlValueWT1, ddlTextWT1, ddlValueWT2, ddlTextWT2, Session["sf_name"].ToString());
                //}

            }

            tp.TP_Delete(MR_Code, MR_Month, MR_Year);

            if (iReturn != -1)
            {
                //menu1.Status = "TourPlan Approved Successfully!!";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Approved Successfully');window.location='TP_Approve.aspx'</script>");
                Response.Redirect("~/MasterFiles/MGR/MGR_Index.aspx");

            }
        }
       
    }

    

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            txtReason.Visible = true;
            grdTP.Enabled = false;
            btnSave.Visible = false;
            btnReject.Visible = false;
            btnSubmit.Visible = true;
            lblRejectReason.Visible = true;

            txtReason.Focus();
        }
        catch (Exception ex)
        {

        }
    }
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            if (txtReason.Text.Trim() != "")
            {
                int iReturn = -1;
                TourPlan tp = new TourPlan();
                foreach (GridViewRow gridRow in grdTP.Rows)
                {
                    Label lblDate = (Label)gridRow.Cells[1].FindControl("lblDate");
                    DropDownList ddlTerr = (DropDownList)gridRow.Cells[1].FindControl("ddlTerr");
                    string TP_Terr_Value = ddlTerr.SelectedValue;
                    //TP_Terr_Name = ddlTerr.SelectedItem.Text;

                    if (TP_Terr_Value == "0")
                    {
                        TP_Terr_Name = "0";
                    }
                    else
                    {
                        TP_Terr_Name = ddlTerr.SelectedItem.Text;
                    }
                    string strMonth = lblDate.Text.Substring(3, 2);

                    DropDownList ddlTerr1 = (DropDownList)gridRow.Cells[1].FindControl("ddlTerr1");
                    string TP_Terr1_Value = ddlTerr1.SelectedValue;

                    if (TP_Terr1_Value == "0")
                    {
                        TP_Terr1_Name = "0";
                    }
                    else
                    {
                        TP_Terr1_Name = ddlTerr1.SelectedItem.Text;
                    }

                    DropDownList ddlTerr2 = (DropDownList)gridRow.Cells[1].FindControl("ddlTerr2");
                    string TP_Terr2_Value = ddlTerr2.SelectedValue;

                    if (TP_Terr2_Value == "0")
                    {
                        TP_Terr2_Name = "0";
                    }
                    else
                    {
                        TP_Terr2_Name = ddlTerr2.SelectedItem.Text;
                    }

                    iReturn = tp.Reject(MR_Code, txtReason.Text, TP_Terr_Name, TP_Terr1_Name, TP_Terr2_Name, lblDate.Text, txtReason.Text, strMonth, Session["sf_name"].ToString());

                }
                //iReturn = tp.Reject(MR_Code, txtReason.Text);

                if (iReturn != -1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Rejected Successfully');window.location='MGR_Index.aspx'</script>");
                }
            }
            else
            {
                txtReason.Focus();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter the Reason')</script>");
            }
        }
    }
    protected void grdTP_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlWT_TextChanged(object sender, EventArgs e)
    {
        btnReject.Visible = false;
    }

    protected void ddlWT_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.Parent.Parent;
            row.Focus();
            DropDownList ddlWT = (DropDownList)row.FindControl("ddlWT");
            DropDownList ddlTerr = (DropDownList)row.FindControl("ddlTerr");
            Dropdownvalue(ddlWT, ddlTerr);
        }
        catch(Exception ex)
        {

        }
    }

    protected void ddlWT1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        row.Focus();
        DropDownList ddlWT = (DropDownList)row.FindControl("ddlWT1");
        DropDownList ddlTerr = (DropDownList)row.FindControl("ddlTerr1");
        Dropdownvalue(ddlWT, ddlTerr);
    }

    protected void ddlWT2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        row.Focus();
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
            dsRPDisable = tpRPDisable.GetTPWorkTypeFieldWork_Approval(sf_code);
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
           // dsTP = tp.FetchTerritory(sf_code);
            dsTP = tp.FetchTerritory_Approval(MR_Code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                ddlTerr.DataSource = dsTP;
                ddlTerr.DataValueField = "Territory_Code";
                ddlTerr.DataTextField = "Territory_Name";
                ddlTerr.DataBind();
            }
        }
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/MasterFiles/MGR/MGR_Index.aspx");
    }
}