using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_HolidayView : System.Web.UI.Page
{
    DataSet dsHoliday = null;
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsTP = null;
    string sState = string.Empty;
    string div_code = string.Empty;
    string HSlno = string.Empty;
    string statecode = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string ddlYear = string.Empty;
    string divcode = string.Empty;
    string state_code = string.Empty;
    string sfCode = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataTable dtStateHoliday;
    UnListedDR LstDR = new UnListedDR();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
       
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sfCode = Session["sf_code"].ToString();
        }
        dsHoliday = LstDR.getState(sfCode);
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            state_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            //   div_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
        }
      
        if (!Page.IsPostBack)
        {
            //ddlYr.Items.Insert(0, new ListItem("---Select---"));
            //ddlYr.Items.Insert(1, new ListItem(DateTime.Now.AddYears(-1).ToString("yyyy"), DateTime.Now.AddYears(-1).ToString("yyyy")));
            //ddlYr.Items.Insert(2, new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            //ddlYr.Items.Insert(3, new ListItem(DateTime.Now.AddYears(1).ToString("yyyy"), DateTime.Now.AddYears(1).ToString("yyyy")));
            //ddlYr.SelectedValue = DateTime.Now.Year.ToString();
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYr.Items.Add(k.ToString());
                    ddlYr.SelectedValue = DateTime.Now.Year.ToString();
                }
            }

            //  menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            if (Session["sf_type"].ToString() == "1")
            {
           //     UserControl_MR_Menu c1 =
           //(UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
           //     Divid.Controls.Add(c1);
           //       c1.FindControl("btnBack").Visible = false;
           //     c1.Title = this.Page.Title;
           //     btnGo_Click(sender, e);
                // btnGo_Click(sender, e);

            }
            else if (Session["sf_type"].ToString() == "2")
            {
          //      UserControl_MGR_Menu c1 =
          //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
          //      Divid.Controls.Add(c1);
          //         c1.FindControl("btnBack").Visible = false;
          //      c1.Title = this.Page.Title;
                lblstate.Visible = true;
                ddlState.Visible = true;
                getStateName();
                btnGo_Click(sender, e);

            }
            else
            {
           //     UserControl_MenuUserControl c1 =
           //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
           //     Divid.Controls.Add(c1);              
           //     c1.FindControl("btnBack").Visible = false;
           //     c1.Title = this.Page.Title;
                lblstate.Visible = true;
                ddlState.Visible = true;
                bindstate(div_code);
                ddlState.SelectedIndex = 1;
                btnGo_Click(sender, e);

            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "1")
            {
         //       UserControl_MR_Menu c1 =
         //(UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
         //       Divid.Controls.Add(c1);
              
         //       c1.Title = this.Page.Title;
               
            }
            else if (Session["sf_type"].ToString() == "2")
            {
          //      UserControl_MGR_Menu c1 =
          //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
          //      Divid.Controls.Add(c1);
          //      //   c1.FindControl("btnBack").Visible = false;
          //      c1.Title = this.Page.Title;
                
            }
            else
            {
           //     UserControl_MenuUserControl c1 =
           //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
           //     Divid.Controls.Add(c1);
           //     c1.FindControl("btnBack").Visible = false;
           //     c1.Title = this.Page.Title;
                ddlState.Visible = true;
                //bindstate(div_code);
               // ddlState.SelectedIndex = 1;
            }
        }
    }
    private void getStateName()
    {
        DataSet dsStateName = new DataSet();
        SalesForce sf = new SalesForce();
        dsStateName = sf.SalesForce_State_Get(div_code, sfCode);

        if (dsStateName.Tables[0].Rows.Count > 0)
        {
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "State_Code";
           ddlState.DataSource = dsStateName.Tables[0].DefaultView.ToTable(true, "StateName","State_Code");
          //  ddlState.DataSource = dsStateName.Tables[0].DefaultView.ToTable(true, "State_Code");
          
            ddlState.DataBind();

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
    public void bindHolidayDdl()
    {
        dtStateHoliday = new DataTable();
        dtStateHoliday.Columns.Add("month");
        dtStateHoliday.Columns.Add("Holidaydate");
        dtStateHoliday.Columns.Add("HolidayName");
        for (int i = 0; i < dsHoliday.Tables[0].Rows.Count; i++)
        {
            DataRow drStateHoliday = null;
            drStateHoliday = dtStateHoliday.NewRow();
           
            drStateHoliday[0] = dsHoliday.Tables[0].Rows[i]["Month"].ToString();
            drStateHoliday[1] = dsHoliday.Tables[0].Rows[i]["holiday_date"].ToString();
            drStateHoliday[2] = dsHoliday.Tables[0].Rows[i]["Holiday_Name"].ToString();
            dtStateHoliday.Rows.Add(drStateHoliday);
            ViewState["StateHoliday"] = dtStateHoliday;
        }
        if (dtStateHoliday.Rows.Count == 0)
        {
            ViewState["StateHoliday"] = null;
        }
       

    }



    protected void CalFeb_DayRender(object sender, DayRenderEventArgs e)
    {
        CalendarDay day = (CalendarDay)e.Day;
        HolidaySelection(day, e, "2");
    }
    protected void Calmar_DayRender(object sender, DayRenderEventArgs e)
    {
        CalendarDay day = (CalendarDay)e.Day;
        HolidaySelection(day, e, "3");
    }
    protected void Calapr_DayRender(object sender, DayRenderEventArgs e)
    {
        CalendarDay day = (CalendarDay)e.Day;
        HolidaySelection(day, e, "4");
    }
    protected void Calmay_DayRender(object sender, DayRenderEventArgs e)
    {
        CalendarDay day = (CalendarDay)e.Day;
        HolidaySelection(day, e, "5");
    }
    protected void Caljune_DayRender(object sender, DayRenderEventArgs e)
    {
        CalendarDay day = (CalendarDay)e.Day;
        HolidaySelection(day, e, "6");
    }
    protected void Caljuly_DayRender(object sender, DayRenderEventArgs e)
    {
        CalendarDay day = (CalendarDay)e.Day;
        HolidaySelection(day, e, "7");
    }
    protected void Calaug_DayRender(object sender, DayRenderEventArgs e)
    {
        CalendarDay day = (CalendarDay)e.Day;
        HolidaySelection(day, e, "8");
    }
    protected void Calsep_DayRender(object sender, DayRenderEventArgs e)
    {
        CalendarDay day = (CalendarDay)e.Day;
        HolidaySelection(day, e, "9");
    }
    protected void Caloct_DayRender(object sender, DayRenderEventArgs e)
    {
        CalendarDay day = (CalendarDay)e.Day;
        HolidaySelection(day, e, "10");
    }
    protected void Calnov_DayRender(object sender, DayRenderEventArgs e)
    {
        CalendarDay day = (CalendarDay)e.Day;
        HolidaySelection(day, e, "11");
    }
    protected void Caldec_DayRender(object sender, DayRenderEventArgs e)
    {
        CalendarDay day = (CalendarDay)e.Day;
        HolidaySelection(day, e, "12");
    }

    protected void HolidaySelection(CalendarDay day, DayRenderEventArgs e, string month)
    {
        if (ViewState["StateHoliday"] != null)
        {
            dtStateHoliday = (DataTable)ViewState["StateHoliday"];

            //SqlCommand cmd = new SqlCommand("select HolidayName,HolidayDate from Holidays", con);
            //con.Open();
            //SqlDataReader dr = cmd.ExecuteReader();

            //while (dr.Read())
            //{
            //if (day.Date.ToString() == "01/04/1996")

            foreach (DataRow dr in dtStateHoliday.Rows)
            {
                if (dr.ItemArray.GetValue(0).ToString() == month)
                {
                    string s = dr["Holidaydate"].ToString();
                    DateTime dt = Convert.ToDateTime(s);
                    if (day.Date.ToLongDateString().Contains(dt.ToLongDateString()))
                    {
                        string s1 = dr["HolidayName"].ToString();
                        if (s1 != null)
                        {
                            TableCell cell = (TableCell)e.Cell;
                            cell.BackColor = System.Drawing.Color.Maroon;
                            cell.ForeColor = System.Drawing.Color.White;
                            cell.Width = 12;
                            cell.Height = 12;
                            cell.Controls.Add(new LiteralControl("<br>" + s1));

                        }
                    }
                }
            }
        }
    }
 
    protected void CalJan_DayRender(object sender, DayRenderEventArgs e)
    {
        CalendarDay day = (CalendarDay)e.Day;
        HolidaySelection(day, e, "1");

    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        CalJan.VisibleDate = Convert.ToDateTime(ddlYr.SelectedValue + "-01-10");
        CalFeb.VisibleDate = Convert.ToDateTime(ddlYr.SelectedValue + "-02-10");
        Calmar.VisibleDate = Convert.ToDateTime(ddlYr.SelectedValue + "-03-10");
        Calapr.VisibleDate = Convert.ToDateTime(ddlYr.SelectedValue + "-04-10");
        Calmay.VisibleDate = Convert.ToDateTime(ddlYr.SelectedValue + "-05-10");
        Caljune.VisibleDate = Convert.ToDateTime(ddlYr.SelectedValue + "-06-10");
        Caljuly.VisibleDate = Convert.ToDateTime(ddlYr.SelectedValue + "-07-10");
        Calaug.VisibleDate = Convert.ToDateTime(ddlYr.SelectedValue + "-08-10");
        Calsep.VisibleDate = Convert.ToDateTime(ddlYr.SelectedValue + "-09-10");
        Caloct.VisibleDate = Convert.ToDateTime(ddlYr.SelectedValue + "-10-10");
        Calnov.VisibleDate = Convert.ToDateTime(ddlYr.SelectedValue + "-11-10");
        Caldec.VisibleDate = Convert.ToDateTime(ddlYr.SelectedValue + "-12-10");
        FillHolidayYear(ddlState.SelectedValue, div_code, ddlYr.SelectedValue);
        
            bindHolidayDdl();
      

    }
    private void FillHolidayYear(string state_code, string divcode, string ddlYear)
    {
        Holiday hol = new Holiday();
        dsHoliday = LstDR.getState(sfCode);
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            state_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            //   div_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
        }
        if (Session["sf_type"].ToString() == "1")
        {
            dsHoliday = hol.getHoliday_View(state_code, div_code, ddlYear);
        }
        else
        {
            dsHoliday = hol.getHoliday_View(ddlState.SelectedValue, div_code, ddlYear);
        }
        
    }
    public void bindstate(string div_code)
    {
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
            dsState = st.getState(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
        }
        
        //Holiday st = new Holiday();
        //dsState = st.getState(div_code);
        //ddlState.DataTextField = "statename";
        //ddlState.DataValueField = "statecode";
        //ddlState.DataSource = dsState;
        //ddlState.DataBind();
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
      
        if (Session["sf_type"].ToString() == "1")
        {
            Response.Redirect("~/Masterfiles/MR/HolidayList_MR.aspx");

        }
        else if (Session["sf_type"].ToString() == "2")
        {
            Response.Redirect("~/Masterfiles/MGR/HolidayList_MGR.aspx");
        }
        else
        {
            Response.Redirect("HolidayList.aspx");
        }
    
    }
}