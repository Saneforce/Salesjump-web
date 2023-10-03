using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using Bus_EReport;

public partial class MasterFiles_MGR_TP_Calender_View : System.Web.UI.Page
{
    Hashtable schedule = new Hashtable();
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
    bool TP_Submit = false;
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
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        GetSchedule();
        Calendar1.Style.Add("position", "absolute");
        Calendar1.Style.Add("left", "300px");
        //Calendar1.Style.Add("top", "50px");
        //Calendar1.Caption = "Special Days";
        Calendar1.FirstDayOfWeek = FirstDayOfWeek.Sunday;
        Calendar1.NextPrevFormat = NextPrevFormat.ShortMonth;
        Calendar1.TitleFormat = TitleFormat.MonthYear;
        Calendar1.ShowGridLines = true;
        Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Center;
        Calendar1.DayStyle.VerticalAlign = VerticalAlign.Top;
        Calendar1.DayStyle.Height = new Unit(60);
        Calendar1.DayStyle.Width = new Unit(100);
        Calendar1.OtherMonthDayStyle.BackColor = System.Drawing.Color.WhiteSmoke;

        //Calendar1.TodaysDate = new DateTime(2008, 12, 1);
        //Calendar1.VisibleDate = Calendar1.TodaysDate;

        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "LstDoctorList.aspx";
            menu1.Title = this.Page.Title;
            FillTPDate();
            //Calendar1.Attributes.Add("onSelectionChanged", "return open_popup();");
            //Calendar1_SelectionChanged
        }        


    }

    private void FillTPDate()
    {
        TourPlan tp = new TourPlan();

        dsTP = tp.get_TP_Calendar(sf_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            dt_TP_Active_Date = Convert.ToDateTime(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
        }

        lblmon.Text = getMonth(Convert.ToInt16(dt_TP_Active_Date.Month));
        lblmon.Text = lblmon.Text + " - " + (Convert.ToInt16(dt_TP_Active_Date.Year));

        Calendar1.TodaysDate = new DateTime(Convert.ToInt16(dt_TP_Active_Date.Year), Convert.ToInt16(dt_TP_Active_Date.Month), Convert.ToInt16(dt_TP_Active_Date.Day));
        Calendar1.VisibleDate = Calendar1.TodaysDate;

        //dsTP = tp.getEmptyTourPlan(dt_TP_Active_Date);
        //if (dsTP.Tables[0].Rows.Count > 0)
        //{
        //    grdTP.Visible = true;
        //    grdTP.DataSource = dsTP;
        //    grdTP.DataBind();
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
        
        schedule["11/23/2008"] = "Thanksgiving";
        schedule["12/5/2008"] = "Birthday";
        schedule["12/16/2008"] = "First day of Chanukah";
        schedule["12/23/2008"] = "Last day of Chanukah";
        schedule["12/24/2008"] = "Christmas Eve";
        schedule["12/25/2008"] = "Christmas";
        schedule["12/26/2008"] = "Boxing Day";
        schedule["12/31/2008"] = "New Year's Eve";
        schedule["1/1/2009"] = "New Year's Day";
    }



    protected void Calendar1_PreRender(object sender, EventArgs e)
    {

    }
    protected void Calendar1_VisibleMonthChanged1(object sender, MonthChangedEventArgs e)
    {
        Response.Write("Month changed to: " + e.NewDate.ToShortDateString());
    }
    //protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    //{
    //    Response.Write("Selection changed to: "
    //    + Calendar1.SelectedDate.ToShortDateString());

    //    //string strScript = string.Format("window.open('TP_View.aspx'", '_blank');
    //    //string sCurrent = Calendar1.SelectedDate.ToShortDateString();
    //    //Page.RegisterStartupScript("popup", "open_window('" + sCurrent + "');");
    //    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "OpenPopUp", "open_popup()", true);
    //}
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        //Literal lit = new Literal();
        //lit.Visible = true;
        //lit.Text = "<br />";
        //e.Cell.Controls.Add(lit);
        //if (schedule[e.Day.Date.ToShortDateString()] != null)
        //{
        //    Label lbl = new Label();
        //    lbl.Visible = true;
        //    lbl.Text = (string)schedule[e.Day.Date.ToShortDateString()];
        //    e.Cell.Controls.Add(lbl);
        //}

        //e.Cell.Controls.Clear();
        //HyperLink hyp = new HyperLink();
        //hyp.Visible = true;
        //hyp.Target = "_blank";
        //hyp.Text = (string)schedule[e.Day.Date.ToShortDateString()];
        //hyp.NavigateUrl = "TPEntry.aspx";
        //e.Cell.Controls.Add(hyp);

        HyperLink hlnk = new HyperLink();
        hlnk.Text = ((LiteralControl)e.Cell.Controls[0]).Text;
        hlnk.Attributes.Add("href", "javascript:showModalPopUp('" +
        e.Day.Date.ToString("MM/dd/yyyy") + "')");
        e.Cell.Controls.Clear();
        e.Cell.Controls.Add(hlnk);

        TourPlan tp = new TourPlan();
        DateTime dtholiday = e.Day.Date;
        dsTP = tp.getHolidays_TP(sf_code, dtholiday.Month, dtholiday.Day, dtholiday.Year );
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            Label lbl = new Label();
            lbl.Visible = true;
            lbl.Text = dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            lbl.BackColor = System.Drawing.Color.FromName("#F454BF");
            e.Cell.Controls.Add(lbl);

        }
        string tp_schedule = string.Empty;
        string tp_ww = string.Empty;
        string tp_obj = string.Empty;
        string tp_cur_date = e.Day.Date.ToString("MM/dd/yyyy");
        
        string[] tp_cd;
        int i;
        //dsTP = tp.get_TourPlan_Details(sf_code, e.Day.Date.ToShortDateString());        
        dsTP = tp.get_TourPlan_Details(sf_code, tp_cur_date);        
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            tp_schedule = dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            tp_ww = dsTP.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            tp_obj = dsTP.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

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
            foreach (string tpcode in tp_cd)
            {
                dsWeek = tp.FetchWorkType(tpcode);
                if (dsWeek.Tables[0].Rows.Count > 0)
                {
                    tp_ww = "";
                    foreach (DataRow dataRow in dsWeek.Tables[0].Rows)
                    {
                        tp_ww = tp_ww + dataRow["Worktype_Name_B"].ToString() + ", ";
                    }
                }
            }
            Label lbl = new Label();
            lbl.Visible = true;
            lbl.Text = "Worked with :" + tp_schedule + ",Area :" + tp_ww + ",Objective : " + tp_obj;
            e.Cell.Controls.Add(lbl);

        }

    }

}