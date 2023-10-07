using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing;
public partial class MasterFiles_MGR_HolidayList_MGR : System.Web.UI.Page
{
    DataSet dsHoliday = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string state_code = string.Empty;
    string strMultiDiv = string.Empty;
    SalesForce sf = new SalesForce();
    DataSet dsUserList = new DataSet();
    Product prd = new Product();
    DataSet dsdiv = new DataSet();
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        UnListedDR LstDR = new UnListedDR();
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        dsHoliday  = LstDR.getState(sf_code);
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            state_code  = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();       
        }

       
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            getStateName();
            ddlDivision.SelectedIndex = 1;
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }
           
            Product prd = new Product();
            DataSet dsdiv = new DataSet();
            dsdiv = prd.getMultiDivsf_Name(sf_code);
             if (dsdiv.Tables[0].Rows.Count > 0)
             {
                 if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                 {
                     strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);
                     ddlDivision.Visible = true;
                     lblDivision.Visible = true;                    
                     getDivision();
                 }
                 else
                 {
                     ddlDivision.Visible = false;
                     lblDivision.Visible = false;                   
                     //BindUserList();
                 }
                 FillHoliday(state_code);
             }
            //FillState(div_code);
            
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            
           
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
    private void BindUserList()
    {

       
        dsUserList = sf.UserList_Self(div_code, sf_code);

        //if (dsUserList.Tables[0].Rows.Count > 0)
        //{
        //    grdHoliday.Visible = true;
        //    grdHoliday.DataSource = dsUserList;
        //    grdHoliday.DataBind();

        //}
        //else
        //{
        //    grdHoliday.DataSource = dsUserList;
        //    grdHoliday.DataBind();
        //}
    }

    private void getStateName()
    {
        DataSet dsStateName = new DataSet();
        dsStateName = sf.SalesForce_State_Get(div_code, sf_code);

        if (dsStateName.Tables[0].Rows.Count > 0)
        {
            ddlStateName.DataTextField = "StateName";
            ddlStateName.DataValueField = "StateName";
            ddlStateName.DataSource = dsStateName.Tables[0].DefaultView.ToTable(true, "StateName");
            ddlStateName.DataBind();            

        }
    }

    private void getDivision()
    {
        Division dv = new Division();
        DataSet dsDivision = new DataSet();
        dsDivision = dv.getMultiDivision(strMultiDiv);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_Code";
            ddlDivision.DataSource = dsDivision;
            ddlDivision.DataBind();
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
    private void FillHoliday(string state_code)
    {

        dsdiv = prd.getMultiDivsf_Name(sf_code);
        if (dsdiv.Tables[0].Rows.Count > 0)
        {
            if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
            {               
                div_code = ddlDivision.SelectedValue;
            }
        }
        Holiday holiday = new Holiday();
        DataSet dsholi = null;
        dsholi = holiday.getStateCode(ddlStateName.SelectedItem.Text);
        if (dsholi.Tables[0].Rows.Count > 0)
        {
            state_code = dsholi.Tables[0].Rows[0][0].ToString();
        }
        Holiday hol = new Holiday();
        dsHoliday = hol.getHolidaysMGR(state_code, div_code, ddlStateName.SelectedItem.Text, ddlYear.SelectedValue);
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
            //    Label HolidayDate = (Label)e.Row.FindControl("lblDate");
            //    string Holi_Month = HolidayDate.Text.Substring(3, 2);

            //    if (Holi_Month == DateTime.Now.ToString().Substring(3, 2))
            //    {
            //        //gridRow.BackColor = Color.Red;
            //        e.Row.BackColor = Color.LightBlue;
            //    }

            //}
            Label HolidayDate = (Label)e.Row.FindControl("lblDate");
            //Added by Sridevi
            DateTime holiday = Convert.ToDateTime(HolidayDate.Text.ToString());

            //string Holi_Month = HolidayDate.Text.Substring(3, 2);
            string Holi_Month = holiday.Month.ToString();
            string cur_Month = DateTime.Now.Month.ToString();

            if (Holi_Month == cur_Month)
            //if (Holi_Month == DateTime.Now.ToString().Substring(3,2))
            {
                //gridRow.BackColor = Color.Red;
                e.Row.BackColor = Color.LightBlue;
            }

        }
    }

    protected void grdHoliday_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdHoliday.PageIndex = e.NewPageIndex;
        FillHoliday(state_code);
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillHoliday(state_code);
    }
    protected void btnNormal_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("HolidayList_MGR.aspx");
    }
    protected void btnCal_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("~/MasterFiles/HolidayView.aspx");
    }
    protected void btnConsol_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("~/MasterFiles/Calendar_Consolidated.aspx");
    }
}