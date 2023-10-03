using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Holiday_List : System.Web.UI.Page
{
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsHoliday = null;
    DataSet dsdiv = null;
    DataSet dsDivision = null;
    string sCmd = string.Empty;
    string div_code = string.Empty;
    string Holiday_Name = string.Empty;
    string sf_type = string.Empty;
    int time;
    int Holiday_Id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {         
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            Filldiv();
            Session["GetcmdArgChar"] = "All";
            FillHoliday();
            FillHoli_Alpha();
            btnNew.Focus();
            
        }
    }
    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        else
        {
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
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
    private void FillHoliday()
    {

        Holiday holi = new Holiday();
        dsHoliday = holi.get_Holidays(ddlDivision.SelectedValue.ToString());
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
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillHoliday();
    }
    //Sorting

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
        Holiday holi = new Holiday();
        sCmd = Session["GetcmdArgChar"].ToString();
       
        if (sCmd == "All")
        {
            dtGrid = holi.get_Holidays_sort(ddlDivision.SelectedValue.ToString());
        }
        else
        {
            dtGrid = holi.get_Holidays_sort(sCmd,ddlDivision.SelectedValue.ToString());
        }       
       
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
        Response.Redirect("HolidayCreation.aspx");
    }
    protected void grdHoliday_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdHoliday.EditIndex = -1;
        //Fill the State Grid
        sCmd = Session["GetcmdArgChar"].ToString();
        //Fill the State Grid
        if (sCmd == "All")
        {
            FillHoliday();
        }
        else
        {
            FillHoliday(sCmd);
        }       
       
    }

    protected void grdHoliday_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdHoliday.EditIndex = e.NewEditIndex;
        sCmd = Session["GetcmdArgChar"].ToString();
        //Fill the State Grid
        if (sCmd == "All")
        {
            FillHoliday();
        }
        else
        {
            FillHoliday(sCmd);
        }       
       
        TextBox HolidayName = (TextBox)grdHoliday.Rows[e.NewEditIndex].Cells[2].FindControl("txtHolidayName");
        HolidayName.Focus();
     
    }
    protected void grdHoliday_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            //  Label lblHoliday_Id = (Label)grdHoliday.Rows[e.RowIndex].Cells[1].FindControl("lblHoliday_Id");
            Holiday_Id = Convert.ToInt16(e.CommandArgument);
            Holiday holi = new Holiday();
            int iReturn = holi.DeActivateHoli(Holiday_Id);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            if (iReturn == -2)
            {
                
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Record Exists in Statewise Holiday Fixation');</script>");
            }
            FillHoliday();
        }
    }
    
    protected void grdHoliday_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdHoliday.EditIndex = -1;
        int iIndex = e.RowIndex;
        UpdateHoliday(iIndex);

        sCmd = Session["GetcmdArgChar"].ToString();
       
        if (sCmd == "All")
        {
            FillHoliday();
        }
        else
        {
            FillHoliday(sCmd);
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
    private void UpdateHoliday(int eIndex)
    {
        System.Threading.Thread.Sleep(time);
        Label lblHoliday_Id = (Label)grdHoliday.Rows[eIndex].Cells[1].FindControl("lblHoliday_Id");
        Holiday_Id = Convert.ToInt16(lblHoliday_Id.Text);
        TextBox txtHoliday_Name = (TextBox)grdHoliday.Rows[eIndex].Cells[1].FindControl("txtHolidayName");
        Holiday_Name = txtHoliday_Name.Text;
        TextBox txtMulti_Date = (TextBox)grdHoliday.Rows[eIndex].Cells[1].FindControl("txtMulti_Date");
 
        Holiday holi = new Holiday();
        int iReturn = holi.Update_Inline_Holiday(Holiday_Id, Holiday_Name, ddlDivision.SelectedValue.ToString());
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Holiday Name Already Exist');</script>");
        }
    }

    protected void grdHoliday_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdHoliday.PageIndex = e.NewPageIndex;
        sCmd = Session["GetcmdArgChar"].ToString();
        //Fill the State Grid
        if (sCmd == "All")
        {
            FillHoliday();
        }
        else
        {
            FillHoliday(sCmd);
        }
    }
    //alphabetical sorting

    private void FillHoli_Alpha()
    {
        Holiday holi = new Holiday();
        dsHoliday = holi.getHolidayName_Alphabet();
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsHoliday;
            dlAlpha.DataBind();
        }
    }
    private void FillHoliday(string sAlpha)
    {
        Holiday st = new Holiday();
        dsHoliday = st.getHolidayName_Alphabet(sAlpha, ddlDivision.SelectedValue.ToString());
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
    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        string sCmd = e.CommandArgument.ToString();
        Session["GetcmdArgChar"] = sCmd;

        if (sCmd == "All")
        {
            grdHoliday.PageIndex = 0;
            FillHoliday();
        }
        else
        {
            grdHoliday.PageIndex = 0;
            FillHoliday(sCmd);
        }   
    }


    protected void btnSlNO_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("HolidayList_SlNo.aspx");
    }
}