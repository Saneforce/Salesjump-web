using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using Bus_EReport;

public partial class MasterFiles_MGR_TP_Calendar : System.Web.UI.Page
{
    Hashtable events = null;

    #region "Declaration"
    DataSet dsTP = null;
    DataSet dsWeek = null;
    DataSet dssf = null;
    DataSet dsUserList = null;
    DataSet dswt = null;
    DataSet dsHoliday = null;
    DataSet dsTerritory = null;
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string state_code = string.Empty;
    string TP_Date = string.Empty;
    string TP_Day = string.Empty;
    string TP_Terr = string.Empty;
    bool TP_Submit = false;
    int TP_Month = -1;
    int TP_Year = -1;
    DateTime TP_Submit_Date;
    DateTime TP_Tour_Date;
    string TP_Tour_Shedule = string.Empty;
    string TP_Objective = string.Empty;
    string sSelectedDate = string.Empty;
    DateTime dtSelectedDate;
    DateTime dt_TP_Active_Date;
    DateTime dt_TP_Current_Date;
    int i;
    int iWeek = -1;
    int iReturn = -1;
    string strCase = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        lblStatingDate.Visible = false;

        //txtFieldForce.Text = "Please Wait....";
        if (!Page.IsPostBack)
        {
            AdminSetup dv = new AdminSetup();
            DataSet dsadmin = new DataSet();
            dsadmin = dv.getAdminSetup();

            string strTPApprovalSys = dsadmin.Tables[0].Rows[0]["TpBased"].ToString();

            if (strTPApprovalSys != "1")
            {
                SalesForce sf = new SalesForce();
                TourPlan tp = new TourPlan();

                DataSet dsSalesForce = new DataSet();
                dsTP = tp.get_TP_Active_Date(sf_code);
                dsSalesForce = sf.SalesForceList_TP_StartingDt_Get(div_code, sf_code, dsTP.Tables[0].Rows[0][0].ToString().Substring(3, 2));
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
                    {
                        DataSet dsStatus = new DataSet();
                        TourPlan TP = new TourPlan();
                        dsStatus = TP.get_TP_ApprovalStatus(drFF["sf_code"].ToString(), dsTP.Tables[0].Rows[0][0].ToString().Substring(3, 2), dsTP.Tables[0].Rows[0][0].ToString().Substring(6, 4));

                        if (dsStatus.Tables[0].Rows.Count != 0)
                        {
                            string str1 = dsStatus.Tables[0].Rows[0][1].ToString();
                            if (str1 != "Completed")
                            {
                                Server.Transfer("~/MasterFiles/MGR/TPMGRApproval.aspx");
                            }
                        }
                        else
                        {
                            Server.Transfer("~/MasterFiles/MGR/TPMGRApproval.aspx");
                        }
                    }

                }
            }

            sf_code = Session["sf_code"].ToString();
            div_code = Session["div_code"].ToString();
            Session["backurl"] = "TP_Calendar.aspx";
            menu1.FindControl("btnBack").Visible = false;
            //menu1.Title = this.Page.Title;            
            FillWorkType();
            FillSF();
            GetTitle();
            GetWorkName();
        }

        for (int i = 0; i < ddlWorkType.Items.Count; i++)
        {
            if (ddlWorkType.Items[i].Text == "Field Work")
            {
                ddlWorkType.Items[i].Attributes.Add("Class", "DropDown");
            }
        }

        FillTPDate();

        Calendar1.DayHeaderStyle.BorderColor = System.Drawing.ColorTranslator.FromHtml("#808080");
        Calendar1.DayHeaderStyle.BorderStyle = BorderStyle.Solid;
        Calendar1.DayHeaderStyle.BorderWidth = 1;


    }

    private void GetWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            lblTerritory.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;".ToString();
        }
    }
    protected void GetTitle()
    {

        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_ApprovalTitle(sf_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            Label2.Text = "Month Tour Plan - Entry For " + "<span style='color:Red'>" + dsTP.Tables[0].Rows[0]["Sf_Name"] + "</span>" + " - " +
                            "<span style='color:Green'>" + dt_TP_Active_Date.ToString("MMMM") + " " + dt_TP_Active_Date.Year + "</span>";


            DataSet dsTPStart = new DataSet();
            dsTPStart = tp.Get_TP_Start_Title(sf_code);
            if (dsTPStart.Tables[0].Rows.Count == 0)
            {
                Label2.Text = "Month Tour Plan - Entry For " + "<span style='color:Red'>" + dsTP.Tables[0].Rows[0]["Sf_Name"] + "</span>" + " - " +
                              "<span style='color:Green'>" + dt_TP_Active_Date.ToString("MMMM") + " " + dt_TP_Active_Date.Year + "</span>";

                lblStatingDate.Visible = true;
                lblStatingDate.Text = "<span style='font-style:normal;color:Blue'> Joining Date : </span><span style='font-style:normal'>" + dt_TP_Active_Date.ToString("dd/MM/yyyy") + "</span>";
            }


        }
    }

    private void Disable()
    {
        pnlCalendar.Enabled = false;
        pnlCalendar.CssClass = "modalBackground";
        pnlHidden.CssClass = "modalBackground";
        btnDraftSave.Enabled = false;
        btnSubmit.Enabled = false;
        btnClear.Enabled = false;
 
        UserControl_MGR_Menu UControl = (UserControl_MGR_Menu)Page.FindControl("Menu");
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        //txtFieldForce.Text = "";
    }

    private void Enable()
    {
        pnlCalendar.Enabled = true;
        btnDraftSave.Enabled = true;
        btnSubmit.Enabled = true;
        btnClear.Enabled = true;
        Server.Transfer("TP_Calendar.aspx");
    }

    private void FillTPDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTPC = new DataSet();

        dsTP = tp.get_TPCalender_Active_Date(sf_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            dt_TP_Active_Date = Convert.ToDateTime(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            dsTPC = tp.checkmonth(sf_code, Convert.ToString(dt_TP_Active_Date.Month));
            if (dsTPC.Tables[0].Rows.Count == 0)
            {
                Calendar1.TodaysDate = new DateTime(Convert.ToInt16(dt_TP_Active_Date.Year), Convert.ToInt16(dt_TP_Active_Date.Month), Convert.ToInt16(dt_TP_Active_Date.Day));
                Calendar1.VisibleDate = Calendar1.TodaysDate;
                GetTitle();
                dsTP = tp.get_TP_Draft(sf_code, dt_TP_Active_Date.ToString());
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    if (dsTP.Tables[0].Rows[0]["Rejection_Reason"].ToString() != "")
                    {
                        lblReason.Text = "Your TP has been Rejected " + dt_TP_Active_Date.ToString("MMMM") + " - " + dt_TP_Active_Date.Year + " Rejected <br> Reason: "
                                          + dsTP.Tables[0].Rows[0]["Rejection_Reason"].ToString();

                        Label2.Text = Label2.Text + " ( Resubmit for Rejection )";
                        lblNote.Visible = true;
                        lblReason.Visible = true;
                        btnDraftSave.Enabled = false;
                    }
                }
            }
            else
            {
                Calendar1.Visible = false;
                dsTP = tp.get_TP_Submission_Date(sf_code);
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    Label2.Text = "<span style='color:#FF3300'>" + dt_TP_Active_Date.ToString("MMM") + " - " + dt_TP_Active_Date.Year + "</span>" +
                                   " TP not yet approved by your manager (" + "<span style='color:Green'>" + dsTP.Tables[1].Rows[0].ItemArray.GetValue(0).ToString() + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"] + "</span>" + ")";

                    hylEdit.Text = "Yes";
                    lblLink.Text = "Before Approval by your Manager - Do you want to change your TP - ";
                    tblTitle.Style.Add("margin-top", "140px");

                    lblStatingDate.Visible = false;
                    btnDraftSave.Visible = false;
                    btnSubmit.Visible = false;
                    btnClear.Visible = false;
                    menu1.FindControl("btnBack").Visible = false;
                }
            }
        }

        //}

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

    private void GetSchedule()
    {

        //schedule["11/23/2008"] = "Thanksgiving";
        //schedule["12/5/2008"] = "Birthday";
        //schedule["12/16/2008"] = "First day of Chanukah";
        //schedule["12/23/2008"] = "Last day of Chanukah";
        //schedule["12/24/2008"] = "Christmas Eve";
        //schedule["12/25/2008"] = "Christmas";
        //schedule["12/26/2008"] = "Boxing Day";
        //schedule["12/31/2008"] = "New Year's Eve";
        //schedule["1/1/2009"] = "New Year's Day";
    }

    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {

        Literal lineBreak = new Literal();
        lineBreak.Text = "<BR />";
        e.Cell.Controls.Add(lineBreak);

        e.Cell.BorderColor = System.Drawing.ColorTranslator.FromHtml("#808080");
        //e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#f3f3f3");        
        e.Cell.BorderStyle = BorderStyle.Solid;
        e.Cell.BorderWidth = 1;
        Label lblSpace = new Label();

        Label b = new Label();
        b.Font.Size = 10;
        b.Font.Bold = true;
        b.ForeColor = System.Drawing.Color.Maroon;
        e.Cell.Controls.Add(b);

        ViewState["seldate"] = Calendar1.SelectedDate.ToString("MM/dd/yyyy"); //e.Day.Date.ToString("MM/dd/yyyy");


        TourPlan tp = new TourPlan();
        DateTime dtholiday = e.Day.Date;

        dsTP = tp.get_TPCalender_Active_Date(sf_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            if (dtholiday.Date >= Convert.ToDateTime(dsTP.Tables[0].Rows[0][0].ToString()))
            {
                e.Day.IsSelectable = true;

                lblSpace.Visible = true;
                lblSpace.Text = "<br>" + " ";
                lblSpace.Font.Size = 6;
                lblSpace.Font.Name = "Verdana";
                lblSpace.Font.Bold = false;
                e.Cell.Controls.Add(lblSpace);
            }
            else
            {
                e.Day.IsSelectable = false;
                e.Cell.Enabled = false;
                e.Cell.Style.Add("text-decoration", "line-through");
                lblSpace.Visible = true;
                lblSpace.Text = "<br>" + " ";
                lblSpace.Font.Size = 6;
                lblSpace.Font.Name = "Verdana";
                lblSpace.Font.Bold = false;
                e.Cell.Controls.Add(lblSpace);
            }
        }
        dsTP = tp.getHolidays_TP(sf_code, dtholiday.Month, dtholiday.Day, dtholiday.Year);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            Label lbl = new Label();
            lbl.Visible = true;
            lbl.Text = dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "<br>" + "Holiday" + "<br>";
            lbl.Font.Size = 7;
            lbl.Font.Name = "Verdana";
            lbl.Font.Bold = false;
            //lbl.Enable = false;

            e.Cell.Controls.Add(lbl);
            e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFD7D7");
            e.Cell.ForeColor = System.Drawing.Color.Red;
            e.Day.IsSelectable = false;
        }

        string tp_worktype = string.Empty;
        string wt = string.Empty;
        string tp_schedule = string.Empty;
        string tp_ww = string.Empty;
        string tp_obj = string.Empty;
        string tp_cur_date = e.Day.Date.ToString("yyyy/MM/dd");

        string[] tp_cd;
        int i;

        //dsTP = tp.get_TourPlan_Details(sf_code, e.Day.Date.ToShortDateString());       
        dsTP = tp.get_TourPlan_Details(sf_code, tp_cur_date);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            lblSpace.Visible = false;
            // btnDraftSave.Enabled = false;
            wt = dsTP.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            tp_schedule = dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            tp_ww = dsTP.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            tp_obj = dsTP.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

            dswt = tp.get_TourPlan_MGRWorkType(wt);
            if (dswt.Tables[0].Rows.Count > 0)
            {
                tp_worktype = dswt.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }

            tp_cd = tp_schedule.Split(',');
            foreach (string tpcode in tp_cd)
            {
                dsWeek = tp.get_sf_name_schedule(tpcode);
                if (dsWeek.Tables[0].Rows.Count > 0)
                {
                    tp_schedule = "";
                    foreach (DataRow dataRow in dsWeek.Tables[0].Rows)
                    {
                        tp_schedule = tp_schedule + dataRow["sf_name"].ToString() + ", ";
                    }
                }
            }

            tp_cd = tp_ww.Split(',');
            tp_ww = "";
            foreach (string tpcode in tp_cd)
            {
                dsWeek = tp.FetchSFName(tpcode);
                if (dsWeek != null)
                {
                    if (dsWeek.Tables[0].Rows.Count > 0)
                    {
                        tp_ww = tp_ww + dsWeek.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + ", ";
                    }
                }
            }

            Label lbl = new Label();
            lbl.Visible = true;
            lbl.Font.Size = 7;
            lbl.Font.Name = "Verdana";
            lbl.Font.Bold = false;

            if (tp_worktype == "Field Work" || tp_worktype == "Camp Work")
            {
                lbl.Text = tp_worktype + ',' + "<br>";
                lbl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#00F");
                lbl.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF00");
            }
            else
            {
                lbl.Text = tp_worktype + ',' + "<br>";
                lbl.ForeColor = System.Drawing.Color.Black;
            }

            e.Cell.Controls.Add(lbl);

            Label lblArea = new Label();
            lblArea.Visible = true;
            //lblArea.Text = "<BR> JointWork : ";
            lblArea.Font.Size = 7;
            lblArea.Font.Name = "Verdana";
            lblArea.Font.Bold = false;
            lblArea.ForeColor = System.Drawing.Color.Black;

            e.Cell.Controls.Add(lblArea);

            Label lblAreaWW = new Label();
            lblAreaWW.Visible = true;
            lblAreaWW.Text = tp_ww + "<br>";
            lblAreaWW.Font.Size = 6;
            lblAreaWW.Font.Name = "Verdana";
            lblAreaWW.Font.Bold = false;
            lblAreaWW.ForeColor = System.Drawing.Color.Black;
            e.Cell.Controls.Add(lblAreaWW);

            Label lblBrk = new Label();
            lblBrk.Visible = true;
            //lblBrk.Text = "<BR> Territory :";
            lblBrk.Font.Size = 6;
            lblBrk.Font.Name = "Verdana";
            lblBrk.Font.Bold = false;
            lblBrk.ForeColor = System.Drawing.Color.Black;
            e.Cell.Controls.Add(lblBrk);

            Label lblsched = new Label();
            lblsched.Visible = true;
            lblsched.Text = tp_schedule;
            lblsched.Font.Size = 6;
            lblsched.Font.Name = "Verdana";
            lblsched.Font.Bold = false;
            lblsched.ForeColor = System.Drawing.Color.Black;
            e.Cell.Controls.Add(lblsched);
        }


        int iMonth = Convert.ToInt16(dt_TP_Active_Date.Month);
        int cMonth = e.Day.Date.Month;



        if (iMonth == cMonth)
        {
            //Do Nothing

            DateTime dtCurDate = Convert.ToDateTime(tp_cur_date);

            string strDay = dtCurDate.DayOfWeek.ToString();

            dsWeek = tp.get_WeekOff(sf_code);
            if (dsWeek.Tables[0].Rows.Count > 0)
            {
                string[] strSplitWeek = dsWeek.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Split(',');
                foreach (string strWeek in strSplitWeek)
                {
                    if (strWeek != "")
                    {
                        iWeek = Convert.ToInt32(strWeek);
                        if (strDay == getDays(iWeek))
                        {
                            Label lblArea = new Label();
                            lblArea.Visible = true;
                            lblArea.Text = "<br>" + "Week Off";
                            lblArea.Font.Size = 7;
                            lblArea.Font.Name = "Verdana";
                            lblArea.Font.Bold = false;
                            lblArea.ForeColor = System.Drawing.Color.Maroon;
                            e.Cell.Controls.Add(lblArea);
                            e.Cell.Style.Add("text-decoration", "none");

                            e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
                            e.Cell.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }
        else
        {
            e.Cell.Text = "";
        }

        DataSet dsTPConfirmed = null;
        dsTPConfirmed = tp.GetTPEditConfirmed_Date(dtholiday.Month.ToString(), dtholiday.Year.ToString(), sf_code);
        string strConfirmed = dsTPConfirmed.Tables[0].Rows[0]["Confirmed_Date"].ToString();
        if (strConfirmed != Convert.ToString(0))
        {
            DataSet dsTPEdit = new DataSet();
            dsTPEdit = tp.GetTPEdit(sf_code, dtholiday.Month.ToString(), dtholiday.Year.ToString(), dtholiday.Date.ToShortDateString() + ",");
            if (dsTPEdit.Tables[0].Rows.Count > 0)
            {
                e.Day.IsSelectable = true;
                e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0E0B2");
            }
            else
            {
                e.Day.IsSelectable = false;
            }
        }
        //TourPlan tp = new TourPlan();
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

    private void FillSF()
    {
        if (txtFieldForce.Text.Trim().Length <= 0)
        {
            TourPlan tp = new TourPlan();
            SalesForce sf = new SalesForce();
            dsTP = sf.UserList_getMR(div_code, sf_code);
            chkFieldForce.Items.Clear();

            if (dsTP.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in dsTP.Tables[0].Rows)
                {
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dataRow["sf_code"].ToString();
                    liTerr.Text = dataRow["sf_name"].ToString();
                    //chkFieldForce.Items.Add(dataRow["sf_name"].ToString());
                    if (liTerr.Value != "0")
                    {
                        chkFieldForce.Items.Add(liTerr);

                    }
                }
            }
        }
    }

    private void FillWorkType()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.getWorkType_Select(div_code);

        if (dsTP.Tables[0].Rows.Count > 0)
        {
            ddlWorkType.DataTextField = "Worktype_Name_M";
            ddlWorkType.DataValueField = "WorkType_Code_M";
            ddlWorkType.DataSource = dsTP;
            ddlWorkType.DataBind();
            // ddlWorkType.SelectedIndex = 1;
        }

    }
    protected void ddlWorkType_SelectedIndexChanged(object sender, EventArgs e)
    {

        TourPlan tp = new TourPlan();
        // Added by Sridevi - TO Clear
       
        txtFieldForce.Enabled = true;
        txtFieldForce.Focus();
        txtTerritory.Enabled = true;
        dsTP = (DataSet)ViewState["TPData"];
        if (dsTP.Tables[0].Rows.Count == 0)
        {
            txtFieldForce.Text = string.Empty;
            for (int i = 0; i < chkFieldForce.Items.Count; i++)
            {
                chkFieldForce.Items[i].Selected = false;
            }

            txtTerritory.Text = string.Empty;
            lblMRTerritory.Text = "";
            string name = string.Empty;
            string name1 = string.Empty;
            for (int i = 0; i < chkTerritory.Items.Count; i++)
            {
                chkTerritory.Items[i].Selected = false;
            }
            Session["ChkTerritory"] = null;
        }
        //ENds
        dsTP = tp.FetchWorkCode_New(ddlWorkType.SelectedValue, div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
           

            //if (ddlWorkType.SelectedItem.Value != "FW" || ddlWorkType.SelectedItem.Value != "CW" || ddlWorkType.SelectedItem.Value.ToString() != "M")
            if (dsTP.Tables[0].Rows[0][0].ToString() == "N")
            {
                txtFieldForce.Text = string.Empty;
                txtTerritory.Text = string.Empty;
                txtFieldForce.Enabled = false;
                txtTerritory.Enabled = false;
                txtFieldForce.Text = "";
                txtTerritory.Text = "";

            }
            else
            {
                txtFieldForce.Enabled = true;
                txtTerritory.Enabled = true;


            }
        }

        //System.Threading.Thread.Sleep(2000);
    }
    protected void chkFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        TourPlan tp = new TourPlan();
        lblMRTerritory.Text = "";
        string name = string.Empty;
        string name1 = string.Empty;
        chkTerritory.Items.Clear();
        Session["ChkTerritory"] = null;


        for (int i = 0; i < chkFieldForce.Items.Count; i++)
        {
            if (chkFieldForce.Items[i].Selected)
            {
                Session["ChkTerritory"] = "Territory";
                SalesForce sf = new SalesForce();
                dssf = sf.CheckSFType(chkFieldForce.Items[i].Value.ToString());
                if (dssf.Tables[0].Rows.Count > 0)
                {
                    dsTP = tp.FetchTerritory_Chkbox(chkFieldForce.Items[i].Value.ToString());
                    string str = GetCaseColor(Convert.ToString(i));

                    if (dsTP.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dsTP.Tables[0].Rows)
                        {
                            ListItem liTerr = new ListItem();
                            liTerr.Value = dataRow["Territory_Code"].ToString();
                            liTerr.Text = dataRow["Territory_Name"].ToString();
                            chkTerritory.Items.Add(liTerr);

                            if (chkTerritory.Items.Contains(liTerr))
                            {

                            }
                            else
                            {
                                chkTerritory.Items.Add(liTerr);
                            }
                        }

                        for (int j = 0; j < chkTerritory.Items.Count; j++)
                        {
                            for (int k = 0; k < dsTP.Tables[0].Rows.Count; k++)
                            {
                                if (chkTerritory.Items[j].Text == dsTP.Tables[0].Rows[k]["Territory_Name"].ToString())
                                {
                                    chkTerritory.Items[j].Attributes.Add("style", "Color:" + str + "");
                                }
                            }
                        }
                    }
                }


                dsUserList = tp.get_TP_MR(chkFieldForce.Items[i].Value, ddlWorkType.SelectedItem.Text, Calendar1.SelectedDate.Date.ToString().Substring(0, 10));

                if (dsUserList.Tables[0].Rows.Count > 0)
                {
                    string[] strChk = chkFieldForce.Items[i].Text.Split('-');
                    //txtTerritory.Text = "";
                    lblMRTerritory.Text += "<span style='color:Blue;'>" + strChk[0] + "</span>" + " : " + dsUserList.Tables[0].Rows[0][0].ToString() + "</br>";

                    name = txtTerritory.Text + ",";

                    for (int chkTerr = 0; chkTerr < chkTerritory.Items.Count; chkTerr++)
                    {

                        if (chkTerritory.Items[chkTerr].Text == dsUserList.Tables[0].Rows[0][0].ToString())
                        {
                            chkTerritory.Items[chkTerr].Selected = true;
                            if (name.Contains(chkTerritory.Items[chkTerr].Text))
                            {
                                if (name.EndsWith(","))
                                {

                                }
                                else
                                {
                                    name = name + ",";
                                }
                            }
                            else
                            {
                                name += chkTerritory.Items[chkTerr].Text + ",";
                            }
                        }
                    }



                }
                else
                {
                    // lblMRTerritory.Text = "";
                    // BindSeletecedValue(sender, e);


                }


            }

        }

        txtTerritory.Text = name;
        string strSplitChk = "";
        string[] strTerr = txtTerritory.Text.Split(',');
        foreach (string strSplit in strTerr)
        {
            strSplitChk = strSplit.Trim();

            for (int chkTerr1 = 0; chkTerr1 < chkTerritory.Items.Count; chkTerr1++)
            {
                if (chkTerritory.Items[chkTerr1].Text == strSplitChk)
                {
                    chkTerritory.Items[chkTerr1].Selected = true;
                    //name1 += chkTerritory.Items[chkTerr1].Text + ",";
                }
            }
        }


        txtTerritory.Text = "";
        for (int chkTerr1 = 0; chkTerr1 < chkTerritory.Items.Count; chkTerr1++)
        {
            if (chkTerritory.Items[chkTerr1].Selected == true)
            {
                txtTerritory.Text += chkTerritory.Items[chkTerr1].Text + ",";
            }
        }
        if (Session["ChkTerritory"] == "" || Session["ChkTerritory"] == null)
        {
            txtTerritory.Text = "";
        }
    }

    private string GetCaseColor(string caseSwitch)
    {
        switch (caseSwitch)
        {
            case "1":
                strCase = "#0066FF";
                break;
            case "2":
                strCase = "#CC6600";
                break;
            case "3":
                strCase = "#246B24";
                break;
            case "4":
                strCase = "#FF9999";
                break;
            case "5":
                strCase = "#999966";
                break;
            case "6":
                strCase = "#B84D4D";
                break;
            case "7":
                strCase = "#CC9900";
                break;

            default:
                strCase = "#387399";
                break;
        }
        return strCase;
    }

    //protected void chkTerritory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string name = "";
    //    string svalue = "";
    //    for (int i = 0; i < chkTerritory.Items.Count; i++)
    //    {
    //        if (chkTerritory.Items[i].Selected)
    //        {
    //            name += chkTerritory.Items[i].Text + ",";
    //            svalue += chkTerritory.Items[i].Value + ",";
    //        }
    //    }
    //    txtTerritory.Text = name;
    //    txtTerr.Text = svalue;
    //}


    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        string TP_Date = ViewState["seldate"].ToString();
        string TP_Day = TP_Date.Substring(3, 2) + "-" + TP_Date.Substring(0, 2) + "-" + TP_Date.Substring(6, 4);
        string TP_Month = TP_Date.Substring(0, 2);
        int iReturn = -1;
        bool TP_Submit = true;
        string WorkType_Code = ddlWorkType.SelectedValue;
        string WorkType_Name = ddlWorkType.SelectedItem.Text;
        string TP_WW = txtFieldForce.Text; //txtSFCode.Text;
        string TP_WWName = string.Empty;
        string TP_Terr = txtTerritory.Text;// txtTerr.Text;
        string TP_Terr_Code = string.Empty;
        string TP_Objective = string.Empty; // txtObj.Text.ToString();
        string sDay = TP_Date.Substring(0, 2);

        string tcode = string.Empty;
        string tName = string.Empty;
        string[] terr;
        TourPlan tp = new TourPlan();

        if (TP_Terr != "")
        {
            //iIndex = -1;
            terr = TP_Terr.Split(',');
            TP_Terr = "";
            foreach (string terr_code in terr)
            {
                tcode = tp.FetchTerritoryName(terr_code.Trim());
                tName = tp.FetchTerritoryCode(terr_code.Trim());
                TP_Terr += tcode + ",";
                TP_Terr_Code += tName + ",";
            }
            TP_Terr = TP_Terr.Remove(TP_Terr.Length - 1);
            TP_Terr_Code = TP_Terr_Code.Remove(TP_Terr_Code.Length - 1);
        }

        if (TP_WW != "")
        {
            //iIndex = -1;
            terr = TP_WW.Split(',');
            TP_WW = "";
            foreach (string terr_code in terr)
            {
                string[] str = terr_code.Split('-');

                tcode = tp.FetchSFCode(str[0].Trim());
                tName = str[0].Trim();
                TP_WW += tcode + ",";
                TP_WWName += tName + ",";

            }
            TP_WW = TP_WW.Remove(TP_WW.Length - 1);
            TP_WWName = TP_WWName.Remove(TP_WWName.Length - 1);
        }

        if ((TP_Terr != "" || TP_Terr == ""))
        {
            // Add New Tour Plan
            TP_Objective = txtObjective.Text;
            iReturn = tp.RecordAddMGR_TP(TP_Day, TP_Terr, TP_WW, WorkType_Code, WorkType_Name, TP_Objective, TP_Submit, sf_code, TP_Terr_Code, TP_WWName, div_code);
            Enable();
        }

        if (iReturn != -1)
        {

            pnlpopup.Visible = false;

        }

    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {

        bool bHoliday = false;

        TourPlan tp = new TourPlan();
        DateTime dtholiday = Convert.ToDateTime(Calendar1.SelectedDate.ToString());

        ViewState["SelectedDate"] = Calendar1.SelectedDate.ToString();

        dsTP = tp.getHolidays_TP(sf_code, dtholiday.Month, dtholiday.Day, dtholiday.Year);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            bHoliday = true;
        }

        pnlpopup.Visible = true;
        Disable();
        if (!bHoliday)
        {
            ViewState["seldate"] = Calendar1.SelectedDate.ToString("MM/dd/yyyy");
            lblHead.Text = "Tour Plan Entry for " + Calendar1.SelectedDate.ToString("dd/MM/yyyy") + " - " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];

            txtFieldForce.Text = "";
            txtTerritory.Text = "";

            //Existing Records
            string tp_worktype = string.Empty;
            string wt = string.Empty;
            string tp_schedule = string.Empty;
            string tp_ww = string.Empty;
            string tp_obj = string.Empty;
            string name = "";
            string[] terr;
            string name1 = "";

            //dsTP = tp.get_TP_Details(sf_code, ViewState["seldate"].ToString());
            dsTP = tp.get_TourPlan_MGRDetails(sf_code, ViewState["seldate"].ToString());
            ViewState["TPData"] = dsTP;
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                wt = dsTP.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                tp_schedule = dsTP.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                tp_ww = dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                txtObjective.Text = dsTP.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();


                string[] str_Schedule = tp_schedule.Split(',');

                foreach (string terr_code1 in str_Schedule)
                {
                    ddlWorkType.SelectedValue = wt;
                    dswt = tp.get_TourPlan_MGRSF_Name(terr_code1);
                    tp_worktype = "";
                    if (dswt.Tables[0].Rows.Count > 0)
                    {
                        tp_worktype = dswt.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    }

                    string tcode = string.Empty;

                    tp_schedule = tp_worktype;
                    if (tp_schedule != "")
                    {
                        //iIndex = -1;

                        terr = tp_schedule.Split(',');
                        tp_schedule = "";
                        foreach (string terr_code in terr)
                        {
                            tcode = tp.FetchSFCode(terr_code.Trim());
                            for (int i = 0; i < chkFieldForce.Items.Count; i++)
                            {
                                if (tcode == chkFieldForce.Items[i].Value)
                                {
                                    chkFieldForce.Items[i].Selected = true;
                                    name += chkFieldForce.Items[i].Text + ",";
                                }
                            }

                            tp_schedule = tp_schedule + tcode + ",";
                        }
                    }
                }

                txtFieldForce.Text = name;
                txtFieldForce.ToolTip = name;
                chkFieldForce_SelectedIndexChanged(sender, e);

                string[] strtp_ww = tp_ww.Split(',');

                foreach (string terr_code1 in strtp_ww)
                {
                    string str = "";
                    dswt = tp.FetchTerritory_TourSchedule(terr_code1);
                    if (dswt.Tables[0].Rows.Count > 0)
                    {
                        str = dswt.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    }


                    tp_ww = str;
                    if (tp_ww != "")
                    {
                        terr = tp_ww.Split(',');
                        tp_ww = "";
                        foreach (string terr_code in terr)
                        {
                            //tcode = tp.FetchSFCode(terr_code.Trim());
                            for (int i = 0; i < chkTerritory.Items.Count; i++)
                            {
                                if (terr_code == chkTerritory.Items[i].Value)
                                {
                                    chkTerritory.Items[i].Selected = true;
                                    name1 += chkTerritory.Items[i].Text + ",";
                                }
                            }

                            tp_ww = tp_ww + terr_code + ",";
                        }

                    }
                    txtTerritory.Text = name1;
                    txtTerritory.ToolTip = name1;
                }
            }
            else
            {
                ddlWorkType.Focus();
                txtFieldForce.Enabled = false;
                txtTerritory.Enabled = false;
            }
        }
    //ddlWorkType_SelectedIndexChanged(sender, e);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlpopup.Visible = false;
        pnlCalendar.Enabled = true;
        Enable();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool bTourPlan = true;
        lblReason.Visible = false;
        Calendar1.TodaysDate = new DateTime(Convert.ToInt16(dt_TP_Active_Date.Year), Convert.ToInt16(dt_TP_Active_Date.Month), Convert.ToInt16(dt_TP_Active_Date.Day));
        //Calendar1.TodaysDate.
        int iLastDay = LastDay_Month(Calendar1.TodaysDate.Month, Calendar1.TodaysDate.Year);
        for (i = 1; i <= iLastDay; i++)
        {
            bool bHoliday = false;
            int iCount = -1;
            string cDate = string.Empty;

            TourPlan tp = new TourPlan();
            string sDate = i.ToString() + "-" + Calendar1.TodaysDate.Month.ToString() + "-" + Calendar1.TodaysDate.Year.ToString();

            DateTime dtholiday = Convert.ToDateTime(sDate);
            dsTP = tp.getHolidays_TP(sf_code, dtholiday.Month, dtholiday.Day, dtholiday.Year);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                bHoliday = true;
            }
            string strDay = dtholiday.DayOfWeek.ToString();

            dsWeek = tp.get_WeekOff(sf_code);
            if (dsWeek.Tables[0].Rows.Count > 0)
            {
                string[] strSplitWeek = dsWeek.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Split(',');
                foreach (string strWeek in strSplitWeek)
                {
                    if (strWeek != "")
                    {
                        iWeek = Convert.ToInt32(strWeek);
                        if (strDay == getDays(iWeek))
                        {
                            bHoliday = true;
                            dsTP = tp.get_TPCalender_Active_Date(sf_code);
                            if (dsTP.Tables[0].Rows.Count > 0)
                            {
                                if (Convert.ToDateTime(sDate) >= Convert.ToDateTime(dsTP.Tables[0].Rows[0][0].ToString()))
                                {

                                    tp.RecordAddMGR_TPWeekOff(sDate, TP_Terr, "", "", "", TP_Objective, TP_Submit, sf_code, "", "");
                                }
                            }
                        }
                    }
                }
            }


            DataSet dsTPCount = new DataSet();

            if (!bHoliday)
            {
                cDate = dtholiday.ToString("MM/dd/yyyy");
                dsTP = tp.get_TPCalender_Active_Date(sf_code);
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToDateTime(dtholiday.ToString("dd/MM/yyyy")) >= Convert.ToDateTime(dsTP.Tables[0].Rows[0][0].ToString()))
                    {
                        dsTPCount = tp.get_TP_Count_one(sf_code, cDate);

                        if (dsTPCount.Tables[0].Rows[0][0].ToString() == "0" && dsTPCount.Tables[0].Rows[1][0].ToString() == "0")
                        {
                            bTourPlan = false;
                        }
                    }
                }
                else
                {
                    dsTPCount = tp.get_TP_Count_one(sf_code, cDate);

                    if (dsTPCount.Tables[0].Rows.Count <= 0)
                    {
                        bTourPlan = false;
                    }
                }

            }
        }

        if (bTourPlan)
        {
            //Code for Save

            //Calendar1.TodaysDate = new DateTime(Convert.ToInt16(Calendar1.TodaysDate.Year), Convert.ToInt16(Calendar1.TodaysDate.Month)+1, 1);
            //Calendar1.VisibleDate = Calendar1.TodaysDate;

            int iReturn = -1;

            TourPlan tp = new TourPlan();
            iReturn = tp.ReadyforApproval(sf_code, Calendar1.TodaysDate.Month.ToString(), Calendar1.TodaysDate.Year.ToString(), Session["sf_name"].ToString());

            //-------- Admin - TP Approval System Based -----------
            Designation Desig = new Designation();
            DataSet dsadmin = new DataSet();

            dsadmin = Desig.getDesignation_Sys_Approval(Session["Designation_Short_Name"].ToString(), div_code);

            if (dsadmin.Tables[0].Rows.Count > 0)
            {
                if (dsadmin.Tables[0].Rows[0]["Designation_Short_Name"].ToString() == Session["Designation_Short_Name"] && dsadmin.Tables[0].Rows[i]["tp_approval_Sys"].ToString() == "1")
                {
                    iReturn = tp.ReadyforCalanderApproval(sf_code, Calendar1.TodaysDate.Month.ToString(), Calendar1.TodaysDate.Year.ToString(), "", Session["sf_name"].ToString());
                    tp.TP_Delete(sf_code, Calendar1.TodaysDate.Month.ToString(), Calendar1.TodaysDate.Year.ToString());
                }
            }

            if (iReturn != -1)
            {
                //menu1.Status = "TourPlan Submitted Successfully!!";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Submitted Successfully');window.location='TP_Calendar.aspx'</script>");

                //Response.Redirect("TP_Approve.aspx");
            }


        }
        else
        {
            //Display the message that cant to submit for approval as it doesnot have all the dates
            //menu1.Status = "Enter Tour Plan for all the dates for this Month for approval";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Tour Plan for all the dates for this Month for approval');</script>");

        }
    }

    protected void btnClr_Click(object sender, EventArgs e)
    {
        TourPlan tp = new TourPlan();
        Calendar1.TodaysDate = new DateTime(Convert.ToInt16(dt_TP_Active_Date.Year), Convert.ToInt16(dt_TP_Active_Date.Month), Convert.ToInt16(dt_TP_Active_Date.Day));
        iReturn = tp.ClearTPCalender(sf_code, Calendar1.TodaysDate.Month.ToString(), Calendar1.TodaysDate.Year.ToString());

        if (iReturn != -1)
        {

        }
    }
    private bool check_TP_Month(int sMonth, int sYear)
    {
        bool bTourPlan = true;

        int iLastDay = LastDay_Month(sMonth, sYear);
        for (i = 1; i <= iLastDay; i++)
        {
            bool bHoliday = false;
            int iCount = -1;
            string cDate = string.Empty;

            TourPlan tp = new TourPlan();
            string sDate = Calendar1.TodaysDate.Year + "-" + Calendar1.TodaysDate.Month.ToString() + "-" + i.ToString();

            DateTime dtholiday = Convert.ToDateTime(sDate);
            dsTP = tp.getHolidays_TP(sf_code, dtholiday.Month, dtholiday.Day, dtholiday.Year);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                bHoliday = true;
            }

            int iDay = Convert.ToInt16(dtholiday.DayOfWeek);

            if (iDay == 0)
            {
                bHoliday = true;
            }

            if (!bHoliday)
            {
                cDate = dtholiday.ToString("MM/dd/yyyy");
                iCount = tp.get_TP_Count(sf_code, cDate);
                if (iCount <= 0)
                {
                    bTourPlan = false;
                }

            }
        }

        return bTourPlan;

    }

    private int LastDay_Month(int iMonth, int iYear)
    {
        int iReturn = -1;

        if (iMonth == 1)
        {
            iReturn = 31;
        }
        else if (iMonth == 2)
        {
            iReturn = 28;
        }
        else if (iMonth == 3)
        {
            iReturn = 31;
        }
        else if (iMonth == 4)
        {
            iReturn = 30;
        }
        else if (iMonth == 5)
        {
            iReturn = 31;
        }
        else if (iMonth == 6)
        {
            iReturn = 30;
        }
        else if (iMonth == 7)
        {
            iReturn = 31;
        }
        else if (iMonth == 8)
        {
            iReturn = 31;
        }
        else if (iMonth == 9)
        {
            iReturn = 30;
        }
        else if (iMonth == 10)
        {
            iReturn = 31;
        }
        else if (iMonth == 11)
        {
            iReturn = 30;
        }
        else if (iMonth == 12)
        {
            iReturn = 31;
        }

        return iReturn;
    }


    protected void btnDraftSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool bTourPlan = true;
            Calendar1.TodaysDate = new DateTime(Convert.ToInt16(dt_TP_Active_Date.Year), Convert.ToInt16(dt_TP_Active_Date.Month), Convert.ToInt16(dt_TP_Active_Date.Day));
            //Calendar1.TodaysDate.
            int iLastDay = LastDay_Month(Calendar1.TodaysDate.Month, Calendar1.TodaysDate.Year);
            for (i = 1; i <= iLastDay; i++)
            {
                bool bHoliday = false;
                int iCount = -1;
                string cDate = string.Empty;

                TourPlan tp = new TourPlan();
                string sDate = i.ToString() + "-" + Calendar1.TodaysDate.Month.ToString() + "-" + Calendar1.TodaysDate.Year.ToString();

                DateTime dtholiday = Convert.ToDateTime(sDate);
                dsTP = tp.getHolidays_TP(sf_code, dtholiday.Month, dtholiday.Day, dtholiday.Year);
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    bHoliday = true;
                }

                //int iDay = Convert.ToInt16(dtholiday.DayOfWeek);

                //if (iDay == 0)
                //{
                //    bHoliday = true;
                //}

                string strDay = dtholiday.DayOfWeek.ToString();

                dsWeek = tp.get_WeekOff(sf_code);
                if (dsWeek.Tables[0].Rows.Count > 0)
                {
                    string[] strSplitWeek = dsWeek.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Split(',');
                    foreach (string strWeek in strSplitWeek)
                    {
                        iWeek = Convert.ToInt32(strWeek);
                        if (strDay == getDays(iWeek))
                        {
                            bHoliday = true;
                            dsTP = tp.get_TPCalender_Active_Date(sf_code);
                            if (dsTP.Tables[0].Rows.Count > 0)
                            {
                                if (Convert.ToDateTime(sDate) >= Convert.ToDateTime(dsTP.Tables[0].Rows[0][0].ToString()))
                                {

                                    tp.RecordAddMGR_TPWeekOff(sDate, TP_Terr, "", "", "", TP_Objective, TP_Submit, sf_code, "", "");
                                }
                            }
                        }
                    }
                }

                if (!bHoliday)
                {
                    cDate = dtholiday.ToString("MM/dd/yyyy");
                    dsTP = tp.get_TPCalender_Active_Date(sf_code);
                    if (dsTP.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToDateTime(dtholiday.ToString("dd/MM/yyyy")) >= Convert.ToDateTime(dsTP.Tables[0].Rows[0][0].ToString()))
                        {
                            iCount = tp.get_TP_Count(sf_code, cDate);
                            if (iCount <= 0)
                            {
                                bTourPlan = false;
                            }

                        }
                    }
                    else
                    {
                        iCount = tp.get_TP_Count(sf_code, cDate);
                        if (iCount <= 0)
                        {
                            bTourPlan = false;
                        }
                    }


                }
            }

            if (bTourPlan)
            {
                //Code for Save

                //Calendar1.TodaysDate = new DateTime(Convert.ToInt16(Calendar1.TodaysDate.Year), Convert.ToInt16(Calendar1.TodaysDate.Month)+1, 1);
                //Calendar1.VisibleDate = Calendar1.TodaysDate;

                int iReturn = -1;

                TourPlan tp = new TourPlan();
                iReturn = tp.ReadyforCalanderDraft(sf_code, Calendar1.TodaysDate.Month.ToString(), Calendar1.TodaysDate.Year.ToString());

                if (iReturn != -1)
                {
                    //menu1.Status = "TourPlan Submitted Successfully!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Submitted Successfully');window.location='TP_Calendar.aspx'</script>");
                    //Response.Redirect("TP_Approve.aspx");
                }


            }
            else
            {
                //Display the message that cant to submit for approval as it doesnot have all the dates
                //  menu1.Status = "Enter Tour Plan for all the dates for this Month for approval";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Tour Plan for all the dates for this Month for approval');</script>");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        chkFieldForce_SelectedIndexChanged(sender, e);
    }
}