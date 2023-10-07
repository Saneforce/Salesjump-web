using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Salesforce_Promo_DePromo : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsSF = null;
    DataSet dsState = null;
    DataSet dsDesignation = null;
    string sf_design = string.Empty;
    string sState = string.Empty;
    string div_code = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string sf_code = string.Empty;
    string sf_name = string.Empty;
    string usr_name = string.Empty;
    int state = -1;
    string sCmd = string.Empty;
    string hq = string.Empty;
    string search = string.Empty;
    string state_code = string.Empty;
    string Promote_to_Manager = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        Promote_to_Manager = Request.QueryString["Promote_to_Manager"];
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "SalesForceList.aspx";
            Session["GetCmdArgChar"] = "All";
            ddlFields.SelectedValue = "Sf_Name";
            txtsearch.Visible = true;
            btnSearch.Visible = true;

            if ((Promote_to_Manager != "") && (Promote_to_Manager != null))
            {
                menu1.Title = "Base Level to Manager Promotion";
                lblFilter.Visible = false;
                ddlFilter.Visible = false;
                btnGo.Visible = false;
                FillOnlyBaselevel();
                FillSF_Alpha();
            }
            else
            {

                FillReporting();
                FillSalesForce();
                FillSF_Alpha();
                menu1.Title = this.Page.Title;
                // menu1.FindControl("btnBack").Visible = false;
                ServerStartTime = DateTime.Now;
                base.OnPreInit(e);
            }
        }
        FillColor();
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
        SalesForce sf = new SalesForce();
        dtGrid = sf.getSalesForcelist_Sort(div_code);
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            dtGrid = sf.getSalesForcelist_Sort(div_code);
        }
        else if (sCmd != "")
        {
            dtGrid = sf.getDTSalesForcelist(div_code, sCmd);
        }
        else if (txtsearch.Text != "")
        {
            string sFind = string.Empty;
            sFind = " AND a." + ddlFields.SelectedValue + " like '" + txtsearch.Text + "%' AND a.Division_Code = '" + div_code + "' ";
            dtGrid = sf.FindDTSalesForcelist(sFind);
        }
        else if (ddlSrc.SelectedIndex > 0)
        {
            search = ddlFields.SelectedValue.ToString();

            if (search == "StateName")
            {
                dtGrid = sf.getDTSalesForce_st(div_code, ddlSrc.SelectedValue);
            }
            else if (search == "Designation_Name")
            {
                dtGrid = sf.getDTSalesForce_des(div_code, ddlSrc.SelectedValue);
            }
        }
        else if (ddlFilter.SelectedIndex > 0)
        {

            dtGrid = sf.getDTSalesForcelist_Reporting(div_code, ddlFilter.SelectedValue);
        }

        return dtGrid;
    }

    protected void grdSalesForce_Sorting(object sender, GridViewSortEventArgs e)
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
        grdSalesForce.DataSource = sortedView;
        grdSalesForce.DataBind();

    }
    //end
    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        if (ddlFilter.SelectedIndex > 0)
        {
            grdSalesForce.PageIndex = 0;
            FillSalesForce_Reporting();
            txtsearch.Text = string.Empty;
            Session["GetCmdArgChar"] = string.Empty;
            if (ddlSrc.SelectedIndex != -1)
            {
                ddlSrc.SelectedIndex = 0;
            }
        }
        else
        {
            FillSalesForce();
        }
    }

    protected DataSet Fill_Design()
    {
        //Designation des = new Designation();
        //dsDesignation = des.getDesign();
        //return dsDesignation;
        SalesForce sf = new SalesForce();
        dsDesignation = sf.getDesignation_SN(div_code);
        return dsDesignation;

    }
    protected DataSet FillState()
    {
        string div_code = string.Empty;
        div_code = Session["div_code"].ToString();
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
        }
        return dsState;
    }

    private void FillReporting()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFilter.DataTextField = "Sf_Name";
            ddlFilter.DataValueField = "Sf_Code";
            ddlFilter.DataSource = dsSalesForce;
            ddlFilter.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }
    }
    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFilter.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;

        }
    }

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();

        if ((Promote_to_Manager != "") && (Promote_to_Manager != null))
        {
            dsSalesForce = sf.getSalesForcelist_Alphabet_List_Promo(div_code);
        }
        else
        {
            dsSalesForce = sf.getSalesForcelist_Alphabet_List(div_code);
        }
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsSalesForce;
            dlAlpha.DataBind();
        }
    }

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
    }


    private void FillSalesForce(string sAlpha)
    {
        SalesForce sf = new SalesForce();

        if ((Promote_to_Manager != "") && (Promote_to_Manager != null))
        {
            dsSalesForce = sf.getSalesForcelist_Pro(div_code, sAlpha);
        }
        else
        {
            dsSalesForce = sf.getSalesForcelist(div_code, sAlpha);
        }
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
    }

    private void FillSalesForce_Reporting()
    {
        string sReport = ddlFilter.SelectedValue.ToString();
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.getSalesForcelist_Reporting(div_code, sReport);
        dsSalesForce = sf.SalesForceList(div_code, sReport);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
    }


    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        string sCmd = e.CommandArgument.ToString();
        Session["GetCmdArgChar"] = sCmd;


        if ((Promote_to_Manager != "") && (Promote_to_Manager != null))
        {
            if (sCmd == "All")
            {
                grdSalesForce.PageIndex = 0;
                FillOnlyBaselevel();
            }
            else
            {
                grdSalesForce.PageIndex = 0;
                FillSalesForce(sCmd);
            }
        }
        else
        {

            if (sCmd == "All")
            {
                grdSalesForce.PageIndex = 0;
                FillSalesForce();
            }
            else
            {
                grdSalesForce.PageIndex = 0;
                FillSalesForce(sCmd);
            }
        }
    }



    protected void grdSalesForce_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdSalesForce_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSalesForce.PageIndex = e.NewPageIndex;
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            if ((Promote_to_Manager != "") && (Promote_to_Manager != null))
            {
                FillOnlyBaselevel();
            }
            else
            {

                FillSalesForce();
            }
        }

        else if (sCmd != "")
        {
            FillSalesForce(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            FindSalesForce(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }
        else if (ddlSrc.SelectedIndex > 0)
        {
            Search();
        }
        else if (ddlFilter.SelectedIndex > 0)
        {
            FillSalesForce_Reporting();
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetCmdArgChar"] = string.Empty;
        grdSalesForce.PageIndex = 0;
        Search();

    }

    private void Search()
    {
        search = ddlFields.SelectedValue.ToString();
        if (search == "UsrDfd_UserName" || search == "Sf_Name" || search == "Sf_HQ" || search == "sf_emp_id")
        {
            FindSalesForce(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }
        else if (search == "StateName")
        {
            txtsearch.Text = string.Empty;
            SalesForce sf = new SalesForce();
            if ((Promote_to_Manager != "") && (Promote_to_Manager != null))
            {
                dsSalesForce = sf.getSalesForce_st_promo(div_code, ddlSrc.SelectedValue);
            }
            else
            {
                dsSalesForce = sf.getSalesForce_st(div_code, ddlSrc.SelectedValue);
            }
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();

            }
            else
            {
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
        }
        else if (search == "Designation_Name")
        {

            txtsearch.Text = string.Empty;
            //ddlSrc.SelectedIndex = 0;
            SalesForce sf = new SalesForce();
            if ((Promote_to_Manager != "") && (Promote_to_Manager != null))
            {
                dsSalesForce = sf.getSalesForce_des_promo(div_code, ddlSrc.SelectedValue);
            }
            else
            {
                dsSalesForce = sf.getSalesForce_des(div_code, ddlSrc.SelectedValue);
            }
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();

            }
            else
            {
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
        }
    }

    private void FindSalesForce(string sSearchBy, string sSearchText, string div_code)
    {
        string sFind = string.Empty;
        sFind = " AND a." + sSearchBy + " like '" + sSearchText + "%' AND  (a.Division_Code like '" + div_code + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + div_code + ',' + "%') ";
        SalesForce sf = new SalesForce();
        if ((Promote_to_Manager != "") && (Promote_to_Manager != null))
        {
            dsSalesForce = sf.FindSalesForcelist_Promo(sFind);
        }
        else
        {
            dsSalesForce = sf.FindSalesForcelist(sFind);
        }
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }

    }

    private void FillSF_State()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getUserList_Reporting(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlSrc.DataTextField = "StateName";
            ddlSrc.DataValueField = "State_Code";
            ddlSrc.DataSource = dsSalesForce;
            ddlSrc.DataBind();
        }
    }
    private void FillState(string div_code)
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
            ddlSrc.DataTextField = "statename";
            ddlSrc.DataValueField = "state_code";
            ddlSrc.DataSource = dsState;
            ddlSrc.DataBind();
        }
    }
    private void FillDesignation()
    {
        SalesForce sf = new SalesForce();
        if ((Promote_to_Manager != "") && (Promote_to_Manager != null))
        {
            dsSalesForce = sf.getDesignation_SN_prom(div_code);
        }
        else
        {
            dsSalesForce = sf.getDesignation_SN(div_code);
        }
        ddlSrc.DataTextField = "Designation_Name";
        ddlSrc.DataValueField = "Designation_Code";
        ddlSrc.DataSource = dsSalesForce;
        ddlSrc.DataBind();
    }
    protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
    {
        // search = Convert.ToInt32(ddlFields.SelectedValue);
        search = ddlFields.SelectedValue.ToString();
        grdSalesForce.PageIndex = 0;

        if (search == "UsrDfd_UserName" || search == "Sf_Name" || search == "Sf_HQ" || search == "sf_emp_id")
        {
            txtsearch.Visible = true;
            btnSearch.Visible = true;
            ddlSrc.Visible = false;
        }
        else
        {
            txtsearch.Visible = false;
            ddlSrc.Visible = true;
            btnSearch.Visible = true;
        }

        if (search == "StateName")
        {
            FillState(div_code);
        }
        if (search == "Designation_Name")
        {
            FillDesignation();
        }
    }
    private void FillOnlyBaselevel()
    {
        SalesForce sal = new SalesForce();
        dsSalesForce = sal.getOnlyBaselevel(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Columns[7].Visible = false;
            grdSalesForce.Columns[8].Visible = false;
            grdSalesForce.Columns[9].Visible = true;
            grdSalesForce.AllowSorting = false;
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }

    }
}