using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Bus_EReport;

public partial class MasterFiles_MR_TourPlan : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsTP = null;
    DataSet dsTPC = null;
    DataSet dsWeek = null;
    DataSet dsHoliday = null;
    DataSet dsTerritory = new DataSet();
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
    DataSet dsWeekoff = null;
    int i;
    int iWeek = -1;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string sf_type = string.Empty;
    string MR_Code = string.Empty;
    string MR_Month = string.Empty;
    string MR_Year = string.Empty;
    string sQryStr = string.Empty;
    string Edit = string.Empty;
    string StrMonth = string.Empty;
    string ID = string.Empty;
    DataSet dsWorkTypeSettings = null;
    string dist_name = string.Empty;
    string dist_Code = string.Empty;
    string Route_code = string.Empty;
    string Route_Name = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        
        sQryStr = Request.QueryString["refer"];
        Edit = Request.QueryString["Edit"];
        if (IsPostBack)
        {
            ValidateGridValType();    
        }

        if (sQryStr != null && sQryStr != "")
        {
            MR_Code = sQryStr.Substring(0, sQryStr.IndexOf('-'));
            sQryStr = sQryStr.Substring(sQryStr.IndexOf('-') + 1, (sQryStr.Length - MR_Code.Length) - 1);
            MR_Month = sQryStr.Substring(0, sQryStr.IndexOf('-'));
            sQryStr = sQryStr.Substring(sQryStr.IndexOf('-') + 1, (sQryStr.Length - MR_Month.Length) - 1);
            MR_Year = sQryStr.Trim();

            if (sQryStr.Length > 0)
            {
                btnSave.Visible = false;
                btnSubmit.Visible = false;
                btnClear.Visible = false;
                btnReject.Visible = true;
                btnApprove.Visible = true;
                Page.Title = "TP - Approval";
                //menu1.Visible = false;
                //menu2.Visible = false;
            }
        }

        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        lblStatingDate.Visible = false;
        if (!Page.IsPostBack)
        {
            TP_New tp = new TP_New();
            dsWorkTypeSettings = tp.Tp_get_WorkType(div_code);
            if (dsWorkTypeSettings.Tables[0].Rows.Count > 0)
            {
                grdWorkType.DataSource = dsWorkTypeSettings;
                grdWorkType.DataBind();
                grdWorkType.Attributes.Add("style", "display:none");
            }
            LoadLstDocTerr();
            GetWeekOff();
            getHoliday_Wtype();
            getWeekOff_Div();
            GetSF_State();
            ViewState["Reject"] = "";

            if (sf_type == "2")
            {
                //menu1.Title = this.Page.Title;
                //menu2.Visible = false;
                //menu3.Visible = false;
                //menu1.Visible = true;
            }
            else if (sf_type == "1")
            {
                //menu2.Title = this.Page.Title;
                //menu1.Visible = false;
                //menu3.Visible = false;

            }

            if (Edit != null && Edit == "E")
            {
                FillTPEdit();
                lblHead.Visible = false;
                //menu2.Title = "";
                lblmon.Text = "Tour Plan - Edit " + "(<span style='font-size:11pt;color:Black;font-family:Verdana'> Before Approval </span>)" + " for the month of " + "<span style='font-size:11pt;color:Green;font-family:Verdana'>" + getMonth(Convert.ToInt16(dt_TP_Active_Date.Month)) + " " + dt_TP_Active_Date.Year + "</span>";
                lblFieldForce.Text = "FieldForce Name: " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
            }
            else if (sQryStr != null && sQryStr != "")
            {
                FillTPApprove();
                GetTitleApproval();
                lblFieldForce.Visible = false;
            }
            else
            {
                lblHead.Visible = true;
                GetTitle();
                FillTPDate();
                lblFieldForce.Visible = false;
            }
            TourPlanTerritory();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
        FillColor();
    }

    private void ValidateGridValType()
    {
        //GridView gv = (GridView)FindControl("grdTP");
        foreach (GridViewRow item in grdTP.Rows)
        {
            if (item.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddDis = (DropDownList)item.FindControl("ddldis");
                DropDownList ddWt = (DropDownList)item.FindControl("ddlWT");
                DropDownList ddRou = (DropDownList)item.FindControl("ddlRou");
                if (ddWt.SelectedItem.Text != "Field Work" && ddWt.SelectedIndex != 0)
                {
                    ddRou.Enabled = false;
                    ddRou.BackColor = System.Drawing.Color.LightGray;
                    ddRou.ToolTip = "Disabled!!";
                    ddDis.Enabled = false;
                    ddDis.BackColor = System.Drawing.Color.LightGray;
                    ddDis.ToolTip = "Disabled!!";
                    //ddRou.Attributes.Add("Disabled", "disabled");
                    //ddRou.Attributes.Add("style", "bgcolor:grey;");
                    //ddDis.Attributes.Add("Disabled", "disabled");
                    //ddDis.Attributes.Add("style", "bgcolor:grey;");
                }
                else
                {
                    ddRou.Enabled = true;
                    ddRou.BackColor = System.Drawing.Color.White;
                    ddDis.Enabled = true;
                    ddDis.BackColor = System.Drawing.Color.White;
                }
            }
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
                //if (strTPView == "3")
                //{
                //    grdTP.Columns[3].Visible = true;
                //    grdTP.Columns[4].Visible = true;
                //    grdTP.Columns[5].Visible = true;
                //    grdTP.Columns[6].Visible = true;
                //    grdTP.Columns[7].Visible = true;
                //    grdTP.Columns[8].Visible = true;
                //}
                //else if (strTPView == "2")
                //{
                //    grdTP.Columns[3].Visible = true;
                //    grdTP.Columns[4].Visible = true;
                //    grdTP.Columns[5].Visible = true;
                //    grdTP.Columns[6].Visible = true;
                //    grdTP.Columns[7].Visible = false;
                //    grdTP.Columns[8].Visible = false;
                //}
                //else if (strTPView == "1")
                //{
                //    grdTP.Columns[3].Visible = true;
                //    grdTP.Columns[4].Visible = true;
                //    grdTP.Columns[5].Visible = false;
                //    grdTP.Columns[6].Visible = false;
                //    grdTP.Columns[7].Visible = false;
                //    grdTP.Columns[8].Visible = false;
                //}
                //else if (strTPView == "0" || strTPView == "")
                //{
                //    grdTP.Columns[3].Visible = true;
                //    grdTP.Columns[4].Visible = true;
                //    grdTP.Columns[5].Visible = false;
                //    grdTP.Columns[6].Visible = false;
                //    grdTP.Columns[7].Visible = false;
                //    grdTP.Columns[8].Visible = false;

                //}
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
        TP_New tp = new TP_New();

        dsTP = tp.Get_TP_ApprovalTitle(sf_code);

        if (dsTP.Tables[0].Rows.Count > 0)
        {
            lblHead.Text = "Month Tour Plan - Entry For " + "<span style='color:Red;'>" + dsTP.Tables[0].Rows[0]["Sf_Name"] + "" +
                            " - " + dsTP.Tables[0].Rows[0]["sf_Designation_Short_Name"] + " - " + dsTP.Tables[0].Rows[0]["Sf_HQ"] + "</span>";
        }
    }

    protected void GetTitleApproval()
    {
        TP_New tp = new TP_New();
        dsTP = tp.Get_TP_ApprovalTitle(MR_Code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            lblHead.Text = "Month Tour Plan - Entry For " + "<span style='color:Red'>" + dsTP.Tables[0].Rows[0]["Sf_Name"] + " - " +
                            dsTP.Tables[0].Rows[0]["sf_Designation_Short_Name"] + " - " + dsTP.Tables[0].Rows[0]["Sf_HQ"] + "</span>";

            DataSet dsTPStart = new DataSet();
            dsTPStart = tp.Get_TP_Start_Title(MR_Code);
            if (dsTPStart.Tables[0].Rows.Count == 0)
            {
                lblHead.Text = "Month Tour Plan - Entry For " + "<span style='color:Red'>" + dsTP.Tables[0].Rows[0]["Sf_Name"] + " - " +
                    dsTP.Tables[0].Rows[0]["sf_Designation_Short_Name"] + " - " + dsTP.Tables[0].Rows[0]["Sf_HQ"] + "</span>" + " " +
                              "<span style='color:Green'>" + dt_TP_Active_Date.ToString("MMMM") + " " + dt_TP_Active_Date.Year + "</span>";
                lblmon.Visible = false;
                lblStatingDate.Visible = true;
                lblStatingDate.Text = "<span style='font-style:normal;color:Blue'> Joining Date : </span><span style='font-style:normal'>" + dt_TP_Active_Date.ToString("dd/MM/yyyy") + "</span>";
            }
        }
    }
    protected void GetWeekOff()
    {
        TP_New tp = new TP_New();
        dsWeekoff = tp.get_WeekOff_Divcode(div_code);
        if (dsWeekoff.Tables[0].Rows.Count > 0)
            ViewState["WeekOff_Wtype_Code"] = dsWeekoff.Tables[0].Rows[0]["WorkType_Code_B"].ToString();
    }

    protected void LoadLstDocTerr()
    {
        TP_New tp = new TP_New();
        DataSet ds = null;
        if (sQryStr != null && sQryStr != "")
        {
            ds = tp.GetTPWorkTypeFieldWork(MR_Code);
        }
        else
        {
            ds = tp.GetTPWorkTypeFieldWork(sf_code);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlAllTerr.DataTextField = "Territory_Name";
            ddlAllTerr.DataValueField = "Territory_Code";
            ddlAllTerr.DataSource = ds;
            ddlAllTerr.DataBind();
            ViewState["DocTerrLst"] = ds;
        }
    }
    protected DataSet FillDSM()
    {
        TP_New tp = new TP_New();
        if (sQryStr != null && sQryStr != "")
        {
            dsTP = tp.FetchDSM(MR_Code);
        }
        else
        {
            dsTP = tp.FetchDSM(sf_code);
        }
        if (dsTP.Tables[0].Rows.Count <= 1)
        {
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Distributor must be created prior to TP creation');window.location='../../MasterFiles/MR/Territory/TerritoryCreation.aspx';</script>");
        }
        ViewState["TerrList"] = dsTP;
        return dsTP;
    }
    protected void GetSF_State()
    {
        UnListedDR LstDR = new UnListedDR();
        if (sQryStr != null && sQryStr != "")
        {
            dsHoliday = LstDR.getState(MR_Code);
        }
        else
        {
            dsHoliday = LstDR.getState(sf_code);
        }
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            state_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
        ViewState["state_code"] = state_code;
    }

    protected void getHoliday_Wtype()
    {
        TP_New tp = new TP_New();
        dsWeekoff = tp.get_Holiday_DivCode(div_code);
        if (dsWeekoff.Tables[0].Rows.Count > 0)
            ViewState["Hol_Wtype_Code"] = dsWeekoff.Tables[0].Rows[0]["WorkType_Code_B"].ToString();
    }
    protected void getWeekOff_Div()
    {
        TP_New tp = new TP_New();
        TourPlan tpOld = new TourPlan();
        if (sQryStr != null && sQryStr != "")
        {
            dsWeek = tpOld.get_WeekOff(MR_Code);
        }
        else
        {
            dsWeek = tpOld.get_WeekOff(sf_code);
        }
        if (dsWeek.Tables[0].Rows.Count > 0)
            ViewState["Div_Week_Off"] = dsWeek.Tables[0].Rows[0]["WeekOff"].ToString();
    }
    private void FillTPDate()
    {
        TP_New tp = new TP_New();

        dsTP = tp.get_TP_Active_Date_New(sf_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            dt_TP_Active_Date = Convert.ToDateTime(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            ViewState["dt_TP_Active_Date"] = dt_TP_Active_Date;
            dsTPC = tp.checkmonth_new(sf_code, Convert.ToString(dt_TP_Active_Date.Month));

            if (dsTPC.Tables[0].Rows.Count == 0)
            {
                lblmon.Text = getMonth(Convert.ToInt16(dt_TP_Active_Date.Month));
                lblmon.Text = " - " + lblmon.Text + " - " + (Convert.ToInt16(dt_TP_Active_Date.Year));
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
                if (dsTPC.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "1")
                {
                    btnSave.Visible = false;
                    btnSubmit.Visible = false;
                    btnClear.Visible = false;
                    dsTP = tp.get_TP_Submission_Date_New(sf_code);
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
                else if (dsTPC.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "2")
                {
                    ViewState["Reject"] = "Yes";
                    FillTPEdit();
                    if (dsTPC.Tables[0].Rows[0]["Rejection_Reason"].ToString() != "")
                    {
                        lblmon.Text = getMonth(Convert.ToInt16(dt_TP_Active_Date.Month)) + " " + Convert.ToInt16(dt_TP_Active_Date.Year);
                        lblReason.Text = "Your TP has been Rejected  " + lblmon.Text + "<br> Rejected Reason: "
                                            + dsTPC.Tables[0].Rows[0]["Rejection_Reason"].ToString();
                        lblmon.Text = "";
                        lblmon.Text = " - " + getMonth(Convert.ToInt16(dt_TP_Active_Date.Month)) + " " + Convert.ToInt16(dt_TP_Active_Date.Year) + "<span style='color:Black'> ( Resubmit for Rejection ) </span>";
                        lblNote.Visible = true;
                        lblReason.Visible = true;
                    }
                }
            }
        }
    }

    private void FillTPEdit()
    {
        TP_New tp = new TP_New();

        dsTP = tp.get_TP_Active_Edit(sf_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            dt_TP_Active_Date = Convert.ToDateTime(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            ViewState["dt_TP_Active_Date"] = dt_TP_Active_Date;
            //lblmon.Text = getMonth(Convert.ToInt16(dt_TP_Active_Date.Month));
            //lblmon.Text = lblmon.Text + " - " + (Convert.ToInt16(dt_TP_Active_Date.Year));
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
    private void FillTPApprove()
    {
        TP_New tp = new TP_New();

        dsTP = tp.get_TP_Approval(MR_Code, Convert.ToInt32(MR_Month), Convert.ToInt32(MR_Year));

        if (dsTP.Tables[0].Rows.Count > 0)
        {
            dt_TP_Active_Date = Convert.ToDateTime(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            ViewState["dt_TP_Active_Date"] = dt_TP_Active_Date;
        }

        DataSet dsTPC = new DataSet();

        dsTP = tp.getEmptyTourPlan(dt_TP_Active_Date);
        //lblmon.Text =
        lblmon.Text = " - " + getMonth(Convert.ToInt16(dt_TP_Active_Date.Month)) + " - " + Convert.ToString(dt_TP_Active_Date.Year);

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

    protected void btnLnkbtn(object sender, EventArgs e)
    {
        Response.Redirect("Default2.aspx?Month=" + dt_TP_Active_Date.Month + "&Year=" + dt_TP_Active_Date.Year + "");
    }
    protected void select_indexchange(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        DropDownList ddldis = (DropDownList)row.FindControl("ddldis");
        Dropdownvalue1(ddldis, row);
    }
    protected void Dropdownvalue1(DropDownList ddldis, GridViewRow row)
    {
        TP_New tpRP = new TP_New();
        DataSet dsRPDisable = new DataSet();

        string dis = ddldis.SelectedValue;
        dsRPDisable = tpRP.getRouteByDist(dis);
        if (dsRPDisable.Tables[0].Rows.Count > 0)
        {
            DropDownList ddlRou = (DropDownList)row.FindControl("ddlRou");
            ddlRou.DataTextField = "Territory_Name";
            ddlRou.DataValueField = "Territory_Code";
            ddlRou.DataSource = dsRPDisable;
            ddlRou.DataBind();

        }


    }
    protected void GrdTP_ddlWTSelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        // int idx = row.RowIndex;
        DropDownList ddlWT = (DropDownList)row.FindControl("ddlWT");
        DropDownList ddlTerr = (DropDownList)row.FindControl("ddlTerr");
        DropDownList ddldis = (DropDownList)row.FindControl("ddldis");
        Dropdownvalue(ddlWT, ddlTerr);
    }

    protected void Dropdownvalue(DropDownList ddlWT, DropDownList ddlTerr)
    {
        TP_New tpRPDisable = new TP_New();
        DataSet dsRPDisable = new DataSet();
        if (ddlWT.SelectedIndex > 0)
        {
            if (ddlWT.SelectedItem.Text == "Field Work")
            {
                dsTP = (DataSet)ViewState["DocTerrLst"];
            }
            else
            {
                dsTP = (DataSet)ViewState["TerrList"];
            }
            ddlTerr.DataSource = dsTP;
            ddlTerr.DataValueField = "Territory_Code";
            ddlTerr.DataTextField = "Territory_Name";
            ddlTerr.DataBind();

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

        }
        else
        {
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
        //GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        GridViewRow row = (GridViewRow)ddl.NamingContainer;
        //row.Focus();
        DropDownList ddlWT = (DropDownList)row.FindControl("ddlWT2");
        DropDownList ddlTerr = (DropDownList)row.FindControl("ddlTerr2");
        Dropdownvalue(ddlWT, ddlTerr);
    }


    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        TP_New tp = new TP_New();

        dt_TP_Active_Date = Convert.ToDateTime(ViewState["dt_TP_Active_Date"].ToString());

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            Label lblDate = (Label)e.Row.FindControl("lblDate");
            Label lblDay = (Label)e.Row.FindControl("lblDay");

            DropDownList ddlWT = (DropDownList)e.Row.FindControl("ddlWT");
            DropDownList ddlWT1 = (DropDownList)e.Row.FindControl("ddlWT1");
            DropDownList ddlWT2 = (DropDownList)e.Row.FindControl("ddlWT2");
            DropDownList ddldis = (DropDownList)e.Row.FindControl("ddldis");
            DropDownList ddldis1 = (DropDownList)e.Row.FindControl("ddldis1");
            DropDownList ddldis2 = (DropDownList)e.Row.FindControl("ddldis2");
            DropDownList ddlRou = (DropDownList)e.Row.FindControl("ddlRou");
            DropDownList ddlTerr = (DropDownList)e.Row.FindControl("ddlTerr");
            DropDownList ddlTerr1 = (DropDownList)e.Row.FindControl("ddlTerr1");
            DropDownList ddlTerr2 = (DropDownList)e.Row.FindControl("ddlTerr2");

            TextBox txtObjective = (TextBox)e.Row.FindControl("txtObjective");

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

                    //ddlWT.Enabled = false;
                    //ddlWT1.Enabled = false;
                    //ddlWT2.Enabled = false;

                    //ddldis1.Enabled = false;
                    //ddldis2.Enabled = false;
                    //ddlTerr.Enabled = false;
                    //ddlTerr1.Enabled = false;
                    //dlTerr2.Enabled = false;

                    ddlWT.SelectedValue = ViewState["Hol_Wtype_Code"].ToString();
                    //txtObjective.Enabled = false;
                    txtObjective.Text = Convert.ToString(dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Replace("asdf", ",")).Trim();
                    //ddlTerr.SelectedValue = "0";
                    //ddlTerr1.SelectedValue = "0";
                    //ddlTerr2.SelectedValue = "0";
                }
                if (ViewState["Div_Week_Off"].ToString() != "")
                {
                    string[] strSplitWeek = ViewState["Div_Week_Off"].ToString().Split(',');
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
                                //ddlWT.Enabled = false;
                                //ddlWT1.Enabled = false;
                                //ddlWT2.Enabled = false;
                                //ddlTerr.Enabled = false;
                                //ddlTerr1.Enabled = false;
                                //ddlTerr2.Enabled = false;
                                //txtObjective.Enabled = false;
                                txtObjective.Text = "Weekly Off";
                                ddlWT.SelectedValue = ViewState["WeekOff_Wtype_Code"].ToString();
                                ddlWT1.SelectedValue = ViewState["WeekOff_Wtype_Code"].ToString();
                                ddlWT2.SelectedValue = ViewState["WeekOff_Wtype_Code"].ToString();
                            }
                        }
                    }
                }

                // Saved but not submitted
                if (Edit != null && Edit == "E")
                {
                    dsTP = tp.get_TP_Details_New(sf_code, lblDate.Text);
                }
                else if (sQryStr != null && sQryStr != "")
                {
                    dsTP = tp.get_TP_Details_Approve_New(MR_Code, lblDate.Text);
                }
                else if (ViewState["Reject"].ToString() == "Yes")
                {
                    dsTP = tp.get_TP_Reject(sf_code, lblDate.Text);
                }
                else
                {
                    dsTP = tp.get_TP_Draft_New(sf_code, lblDate.Text);
                }
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    txtObjective.Text = dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Replace("asdf", "'").Trim();

                    ddlWT.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(5).ToString());
                    ddlWT1.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(8).ToString());
                    ddlWT2.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(10).ToString());
                    ddldis.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                    Dropdownvalue1(ddldis, e.Row);
                    ddlRou.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                    ddlTerr.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
                    ddlTerr1.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(13).ToString());
                    ddlTerr2.SelectedValue = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(14).ToString());

                    //DataSet dsActiveFlag = new DataSet();
                    //Territory terr = new Territory();

                    //dsTerritory = terr.getWorkAreaName(div_code);



                    //dsActiveFlag = tp.FetchTerritory_Active_Flag(MR_Code, ddlTerr.SelectedValue);
                    //if (dsActiveFlag.Tables[0].Rows.Count > 0)
                    //{
                    //    if (dsActiveFlag.Tables[0].Rows[0][0].ToString() == "1")
                    //    {
                    //        for (int i = 0; i < ddlTerr.Items.Count; i++)
                    //        {
                    //            if (ddlTerr.Items[i].Value == ddlTerr.SelectedValue)
                    //            {
                    //                ddlTerr.Items[i].Attributes.CssStyle.Add("color", "red");
                    //                ddlTerr.ToolTip = "DeActivated!!";
                    //                lblDeactivate.Visible = true;
                    //                lblDeactivate.Text = "'Red Color indicates' Deleted " + "'" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "'" + " Before Preparing the TP Kindly Modify/Delete the " + "'" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "'" + " Reject the TP and submit once again for Approval.";
                    //            }
                    //        }
                    //        //dropdownlist1.selectedItem.Attribute.add("Style", "color:red");
                    //    }
                    //}








                    //TP_New tpRPDisable = new TP_New();
                    //DataSet dsRPDisable = new DataSet();

                    //if (ddlWT.SelectedIndex > 0)
                    //{
                    //    dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT.SelectedItem.Text, div_code);
                    //    if (dsRPDisable.Tables[0].Rows.Count > 0)
                    //    {
                    //        ddlRou.Enabled = false;
                    //        ddlRou.BackColor = System.Drawing.Color.LightGray;
                    //        ddlRou.ToolTip = "Disabled!!";
                    //    }
                    //    else
                    //    {
                    //        ddlRou.Enabled = true;
                    //        ddlRou.ToolTip = "Select";
                    //        ddlTerr.BackColor = System.Drawing.Color.White;
                    //    }
                    //}
                    //        if (ddlWT.SelectedIndex > 0)
                    //        {
                    //            dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT.SelectedItem.Text, div_code);
                    //            if (dsRPDisable.Tables[0].Rows.Count > 0)
                    //            {
                    //                ddlTerr.Enabled = false;
                    //                ddlTerr.BackColor = System.Drawing.Color.LightGray;
                    //                ddlTerr.ToolTip = "Disabled!!";
                    //            }
                    //            else
                    //            {
                    //                ddlTerr.Enabled = true;
                    //                ddlTerr.ToolTip = "Select";
                    //                ddlTerr.BackColor = System.Drawing.Color.White;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            ddlTerr.Enabled = false;
                    //            ddlTerr.ToolTip = "Disabled!!";
                    //            ddlTerr.BackColor = System.Drawing.Color.LightGray;
                    //        }
                    //        if (ddlWT1.SelectedIndex > 0)
                    //        {
                    //            dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT1.SelectedItem.Text, div_code);
                    //            if (dsRPDisable.Tables[0].Rows.Count > 0)
                    //            {
                    //                ddlTerr1.Enabled = false;
                    //                ddlTerr1.BackColor = System.Drawing.Color.LightGray;
                    //                ddlTerr1.ToolTip = "Disabled!!";
                    //            }
                    //            else
                    //            {
                    //                ddlTerr1.Enabled = true;
                    //                ddlTerr1.ToolTip = "Select";
                    //                ddlTerr1.BackColor = System.Drawing.Color.White;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            ddlTerr1.Enabled = false;
                    //            ddlTerr1.ToolTip = "Disabled!!";
                    //            ddlTerr1.BackColor = System.Drawing.Color.LightGray;
                    //        }
                    //        if (ddlWT2.SelectedIndex > 0)
                    //        {
                    //            dsRPDisable = tpRPDisable.GetTPWorkTypePlaceInvolved(ddlWT2.SelectedItem.Text, div_code);
                    //            if (dsRPDisable.Tables[0].Rows.Count > 0)
                    //            {
                    //                ddlTerr2.Enabled = false;
                    //                ddlTerr2.BackColor = System.Drawing.Color.LightGray;
                    //                ddlTerr2.ToolTip = "Disabled!!";
                    //            }
                    //            else
                    //            {
                    //                ddlTerr2.Enabled = true;
                    //                ddlTerr2.ToolTip = "Select";
                    //                ddlTerr2.BackColor = System.Drawing.Color.White;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            ddlTerr2.Enabled = false;
                    //            ddlTerr2.BackColor = System.Drawing.Color.LightGray;
                    //            ddlTerr2.ToolTip = "Disabled!!";
                    //        }

                    //        DataSet dsTPConfirmed = null;
                    //        dsTPConfirmed = tp.GetTPConfirmed_Date(dt_TP_Current_Date.Month.ToString(), dt_TP_Current_Date.Year.ToString(), sf_code);
                    //        string strConfirmed = dsTPConfirmed.Tables[0].Rows[0]["Confirmed_Date"].ToString();
                    //        if (strConfirmed != "0")
                    //        {
                    //            DataSet dsTPEdit = new DataSet();
                    //            dsTPEdit = tp.GetTPEdit(sf_code, dt_TP_Current_Date.Month.ToString(), dt_TP_Current_Date.Year.ToString(), lblDate.Text + ",");
                    //            if (dsTPEdit.Tables[0].Rows.Count > 0)
                    //            {
                    //                //ddlWT.Enabled = true;
                    //                //ddlWT1.Enabled = false;
                    //                // ddlWT2.Enabled = false;
                    //                //ddlTerr.Enabled = true;
                    //                //ddlTerr1.Enabled = false;
                    //                //ddlTerr2.Enabled = false;
                    //                //txtObjective.Enabled = true;

                    //                lblDay.BackColor = System.Drawing.Color.Orange;
                    //                lblSNo.BackColor = System.Drawing.Color.LightSteelBlue;
                    //                lblDate.BackColor = System.Drawing.Color.LightSteelBlue;
                    //            }
                    //            else
                    //            {
                    //                //ddlWT.Enabled = false;
                    //                //ddlWT1.Enabled = false;
                    //                //ddlWT2.Enabled = false;
                    //                //ddlTerr.Enabled = false;
                    //                //ddlTerr1.Enabled = false;
                    //                //ddlTerr2.Enabled = false;
                    //                //txtObjective.Enabled = false;
                    //            }
                    //        }


                    //    }

                    //    //ddlWT1.Enabled = false;
                    //    //ddlWT2.Enabled = false;

                    //    //ddldis1.Enabled = false;
                    //    //ddldis2.Enabled = false;
                    //    //ddlTerr1.Enabled = false;
                    //    //ddlTerr2.Enabled = false;
                    //}

                    //if (e.Row.RowType == DataControlRowType.Header)
                    //{
                    //    Territory terr = new Territory();
                    //    DataSet dsTerritory = new DataSet();
                    //    dsTerritory = terr.getWorkAreaName(div_code);
                    //    if (dsTerritory.Tables[0].Rows.Count > 0)
                    //    {

                    //        //for cap
                    //        //e.Row.Cells[4].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                    //        //e.Row.Cells[6].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                    //        //e.Row.Cells[8].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();




                    //    }

                    //}
                }
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
        TP_New tp = new TP_New();
        if (sQryStr != null && sQryStr != "")
        {
            dsTP = tp.FetchTerritory(MR_Code);
        }
        else
        {
            dsTP = tp.FetchTerritory(sf_code);
        }
        //if (dsTP.Tables[0].Rows.Count <= 1)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Territory must be created prior to TP creation');window.location='../../MasterFiles/MR/Territory/TerritoryCreation.aspx';</script>");
        //}
        ViewState["TerrList"] = dsTP;
        return dsTP;
    }
    protected DataSet FillWorkType()
    {
        TP_New tp = new TP_New();
        dsTP = tp.FetchWorkType_New(div_code);
       
        if (dsTP.Tables[0].Rows.Count <= 1)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Worktype must be loaded');</script>");
        }
        return dsTP;
    }
    protected void FillColor()
    {
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
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        TP_Submit = false;

        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            iReturn = CreateTP(TP_Submit);
            if (iReturn != -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Saved Successfully');</script>");
                if ((Edit != null && Edit == "E") || (ViewState["Reject"].ToString() == "Yes"))
                {
                    FillTPEdit();
                }
                else
                {
                    FillTPDate();
                }
            }
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;

        //string confirmValue = Request.Form["confirm_value"];
       // if (confirmValue == "Yes")
        //{
        iReturn = ApproveTP();
        if (iReturn != -1)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Approved Successfully');window.location='../MGR/MGR_Index.aspx'</script>");
        }
       // }
    }
    protected int ApproveTP()
    {
        TP_New tp = new TP_New();
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
            try
            {
                DropDownList ddldis = (DropDownList)gridRow.FindControl("ddldis");
                dist_name = ddldis.SelectedItem.Text;
                dist_Code = ddldis.SelectedValue.ToString();

                DropDownList ddlrou = (DropDownList)gridRow.FindControl("ddlRou");
                Route_Name = ddlrou.SelectedItem.Text;
                Route_code = ddlrou.SelectedValue.ToString();
            }
            catch
            {
            }
            DropDownList ddlWork_Type1 = (DropDownList)gridRow.Cells[5].FindControl("ddlWT1");
            ddlValueWT1 = ddlWork_Type1.SelectedValue.ToString();

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

            if (TP_Terr_Value == "0")
            {
                TP_Terr_Name = "0";
            }
            else
            {
                TP_Terr_Name = ddlTerritory_Type.SelectedItem.Text;
            }

            DropDownList ddlTerritory_Type1 = (DropDownList)gridRow.Cells[5].FindControl("ddlTerr1");
            TP_Terr1_Value = ddlTerritory_Type1.SelectedValue.ToString();

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

            //TP_Objective = txtObjective.Text.ToString();

            TP_Objective = txtObjective.Text.ToString().Replace("'", "asdf");
			iReturn = tp.RecordAdd_New(TP_Date, TP_Day, TP_Terr_Name, TP_Terr1_Name, TP_Terr2_Name, ddlWT, ddlWT1, TP_Objective, TP_Submit, MR_Code, TP_Terr_Value, TP_Terr1_Value, TP_Terr2_Value, ddlValueWT1, ddlTextWT1, ddlValueWT2, ddlTextWT2, Session["sf_name"].ToString(), dist_Code, dist_name, Route_code, Route_Name);
            iReturn = tp.Approve_New(MR_Code, MR_Month, MR_Year, Session["sf_code"].ToString(), div_code, "", TP_Terr_Name, TP_Terr1_Name, TP_Terr2_Name, lblDate.Text, TP_Terr_Value, TP_Terr1_Value, TP_Terr2_Value, "", ddlValueWT1, ddlTextWT1, ddlValueWT2, ddlTextWT2, Session["sf_name"].ToString(), dist_Code, dist_name, Route_code, Route_Name);

        }
        if (iReturn > 0)
        {
            int iretapp = tp.TP_Confirm(MR_Code, MR_Month, MR_Year);
            int iretdel = tp.TP_Delete(MR_Code, MR_Month, MR_Year);
        }
        return iReturn;
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        txtReason.Visible = true;
        grdTP.Enabled = false;
        btnApprove.Visible = false;
        btnSave.Visible = false;
        btnReject.Visible = false;
        btnSubmit.Visible = false;
        btnSendBack.Visible = true;
        lblRejectReason.Visible = true;

        txtReason.Focus();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        TP_New tp = new TP_New();
        DateTime dt_TourPlan = Convert.ToDateTime(ViewState["dt_TP_Active_Date"].ToString());
        int iret = tp.TP_Delete(sf_code, dt_TourPlan.Month.ToString(), dt_TourPlan.Year.ToString());
        if (iret >= 0)
            FillTPDate();
    }
    protected void btnSendBack_Click(object sender, EventArgs e)
    {
        if (txtReason.Text.Trim() != "")
        {
            int iReturn = -1;
            TP_New tp = new TP_New();

            txtReason.Text = txtReason.Text.ToString().Replace("'", "asdf");

            iReturn = tp.Reject_New(MR_Code, MR_Month, MR_Year, txtReason.Text, Session["sf_name"].ToString());

            if (iReturn != -1)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Rejected Successfully');window.location='../MGR/MGR_Index.aspx'</script>");
            }
        }
        else
        {
            txtReason.Focus();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter the Reason')</script>");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      
            //string confirmValue = Request.Form["confirm_value"];
            //if (confirmValue == "Yes")
            //{
                lblNote.Visible = false;
                lblReason.Visible = false;
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
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TourPlan Submitted for Approval');window.location='TourPlan.aspx'</script>");
                        grdTP.Visible = false;
                        btnSubmit.Visible = false;
                        btnSave.Visible = false;
                        btnClear.Visible = false;
                        lblFieldForce.Visible = false;
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
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Work Type. ');</script>");
                    }
                }
            }
       // }
    

    private int CreateTP(bool TP_Submit)
    {
        TP_New tp = new TP_New();
        bool isAutoApprove = false;
        DataSet dsadmin = new DataSet();

        Designation Desig = new Designation();
        dsadmin = Desig.getDesignation_Sys_Approval(Session["Designation_Short_Name"].ToString(), div_code);

        string strSession = Session["Designation_Short_Name"].ToString();
        if (dsadmin.Tables[0].Rows.Count > 0)
        {
            if (dsadmin.Tables[0].Rows[0]["Designation_Short_Name"].ToString() == strSession && dsadmin.Tables[0].Rows[0]["tp_approval_Sys"].ToString() == "1")
                isAutoApprove = true;
        }

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
            try
            {
                DropDownList ddldis = (DropDownList)gridRow.FindControl("ddldis");
                dist_name = ddldis.SelectedItem.Text;
                dist_Code = ddldis.SelectedValue.ToString();

                DropDownList ddlrou = (DropDownList)gridRow.FindControl("ddlRou");
                Route_Name = ddlrou.SelectedItem.Text;
                Route_code = ddlrou.SelectedValue.ToString();
            }
            catch
            { }

                //dist_Code = ddldis.SelectedValue.ToString();
                DropDownList ddlWork_Type1 = (DropDownList)gridRow.Cells[6].FindControl("ddlWT1");
                ddlValueWT1 = ddlWork_Type1.SelectedValue.ToString();


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


                if (ddlValueWT2 == "0")
                {
                    ddlTextWT2 = "0";
                }
                else
                {
                    ddlTextWT2 = ddlWork_Type2.SelectedItem.Text;
                }

                DropDownList ddlTerritory_Type = (DropDownList)gridRow.Cells[8].FindControl("ddlTerr");
                TP_Terr_Value = ddlTerritory_Type.SelectedValue.ToString();


                if (TP_Terr_Value == "0")
                {
                    TP_Terr_Name = "0";
                }
                else
                {
                    TP_Terr_Name = ddlTerritory_Type.SelectedItem.Text;
                }

                DropDownList ddlTerritory_Type1 = (DropDownList)gridRow.Cells[9].FindControl("ddlTerr1");
                TP_Terr1_Value = ddlTerritory_Type1.SelectedValue.ToString();

                if (TP_Terr1_Value == "0")
                {
                    TP_Terr1_Name = "0";
                }
                else
                {
                    TP_Terr1_Name = ddlTerritory_Type1.SelectedItem.Text;
                }

                DropDownList ddlTerritory_Type2 = (DropDownList)gridRow.Cells[10].FindControl("ddlTerr2");
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


                //TP_Objective = txtObjective.Text.ToString();

                TP_Objective = txtObjective.Text.ToString().Replace("'", "asdf");

                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);


                iReturn = tp.RecordAdd_New(TP_Date, TP_Day, TP_Terr_Name, TP_Terr1_Name, TP_Terr2_Name, ddlWT, ddlWT1, TP_Objective, TP_Submit, Session["sf_code"].ToString(), TP_Terr_Value, TP_Terr1_Value, TP_Terr2_Value, ddlValueWT1, ddlTextWT1, ddlValueWT2, ddlTextWT2, Session["sf_name"].ToString(), dist_Code, dist_name, Route_code, Route_Name);

                if (isAutoApprove == true)
                    iReturn = tp.Approve_New(sf_code, dt_TourPlan.Month.ToString(), dt_TourPlan.Year.ToString(), Session["sf_code"].ToString(), div_code, "", TP_Terr_Name, TP_Terr1_Name, TP_Terr2_Name, lblDate.Text, TP_Terr_Value, TP_Terr1_Value, TP_Terr2_Value, "", ddlValueWT1, ddlTextWT1, ddlValueWT2, ddlTextWT2, Session["sf_name"].ToString(), dist_Code, dist_name, Route_code, Route_Name);
           
        }

        if (isAutoApprove == true)
        {
            if (iReturn > 0)
            {
                DateTime dt_TourPlan = Convert.ToDateTime(ViewState["dt_TP_Active_Date"].ToString());

                int iretapp = tp.TP_Confirm(sf_code, dt_TourPlan.Month.ToString(), dt_TourPlan.Year.ToString());
                int iretdel = tp.TP_Delete(sf_code, dt_TourPlan.Month.ToString(), dt_TourPlan.Year.ToString());
            }
        }
        return iReturn;
    }


}