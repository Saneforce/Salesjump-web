using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing;
public partial class MasterFiles_MR_HolidayList_MR : System.Web.UI.Page
{
    DataSet dsHoliday = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string state_code = string.Empty;
    string Holiday_Date = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        UnListedDR LstDR = new UnListedDR();
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        dsHoliday = LstDR.getState(sf_code);
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            state_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            //   div_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
        }

        if (!Page.IsPostBack)
        {
            //FillState(div_code);
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }
            FillHoliday(state_code);
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
         //   menu1.Title = this.Page.Title;
         //   menu1.FindControl("btnBack").Visible = false;


        }
    }
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
    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        Holiday hol = new Holiday();
        dtGrid = hol.getHolidayslistMR_DataTable(state_code, div_code);
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
    private void FillHoliday(string state_code)
    {
        Holiday hol = new Holiday();
        dsHoliday = hol.GetHolidaysDataNew(div_code, ddlYear.SelectedValue, state_code);
        //hol.getHolidays_Mr(state_code, div_code, ddlYear.SelectedValue);
        //dsAlowtype =
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            grdHoliday.Visible = true;
            grdHoliday.DataSource = dsHoliday;
            grdHoliday.DataBind();
        }
        else
        {
            grdHoliday.DataSource = dsHoliday;
            grdHoliday.DataBind();
        }
    }
    protected void grdHoliday_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");

        }
    }

    protected void grdHoliday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //string Holiday_Date = Holiday_Date.Substring(3, 2) + "-" + Holiday_Date.Substring(0, 2) + "-" + Holiday_Date.Substring(6, 4);
        //string Holi_Date = Holiday_Date.Substring(3, 2) + "-" + Holiday_Date.Substring(0, 2) + "-" + Holiday_Date.Substring(6, 4);
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //foreach (GridViewRow gridRow in grdHoliday.Rows)
            //{
               Label HolidayDate = (Label)e.Row.FindControl("lblDate");
                //Added by Sridevi
                DateTime holiday =  Convert.ToDateTime(HolidayDate.Text.ToString());

                //string Holi_Month = HolidayDate.Text.Substring(3, 2);
                string Holi_Month = holiday.Month.ToString();
                string cur_Month = DateTime.Now.Month.ToString();

                if(Holi_Month == cur_Month)
                //if (Holi_Month == DateTime.Now.ToString().Substring(3,2))
                {
                    //gridRow.BackColor = Color.Red;
                    e.Row.BackColor = Color.LightBlue;
                }

//            }
        }
    }

    protected void grdHoliday_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdHoliday.PageIndex = e.NewPageIndex;
        FillHoliday(state_code);
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        FillHoliday(state_code);
    }

    protected void btnNormal_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("HolidayList_MR.aspx");
    }
    protected void btnCal_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("~/MasterFiles/MR/HolidayView.aspx");
    }
}