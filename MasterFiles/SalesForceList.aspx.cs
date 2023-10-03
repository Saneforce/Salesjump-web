using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using Bus_EReport;
using System.Drawing.Imaging;
public partial class MasterFiles_SalesForceList : System.Web.UI.Page
{
	DataTable dssalesforce1=null;
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsSF = null;
    DataSet dsState = null;
    DataSet dsDesignation = null;
    string sf_design = string.Empty;
    string sf_design_short_name = string.Empty;
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
    int sf_type = 0;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            Session["GetCmdArgChar"] = "All";
            FillReporting();
            FillSalesForce();
            FillSF_Alpha();
            btnNew.Focus();
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
       ddlFields.SelectedValue = "Sf_Name";
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
            sFind = " AND a." + ddlFields.SelectedValue + " like '" + txtsearch.Text + "%' AND (a.Division_Code like '" + div_code + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + div_code + ',' + "%') ";
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
         if (ddlFilter.SelectedItem.ToString() == "")
        {

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Any One');</script>");
        }
        else
        {
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
    }

    protected DataSet Fill_Design()
    {
        //Designation des = new Designation();
        //dsDesignation = des.getDesign();
        //return dsDesignation;
        SalesForce sf = new SalesForce();
        dsDesignation = sf.getDesignation_SNM(div_code, sf_type);
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
        dsSalesForce = sf.UserList_Hierarchy(div_code, "Admin");
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
        dsSalesForce = sf.getSalesForcelist_Alphabet_List(div_code);
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
            txtsearch.Visible = true;
            btnSearch.Visible = true;
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
        dsSalesForce = sf.getSalesForcelist(div_code, sAlpha);
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

        //dsSalesForce = sf.SalesForceList(div_code, sReport);

        DataTable dtUserList = new DataTable();
        dtUserList = sf.getUserListReportingToNew_for_all(div_code, sReport, 0, Session["sf_type"].ToString()); // 28-Aug-15 -Sridevi

        if (sReport == "admin")
        {
            dtUserList.Rows[0].Delete();
            dtUserList.Rows[0].Delete();
        }
        else
        {
            dtUserList.Rows[1].Delete();
        }

        if (dtUserList.Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dtUserList;
            grdSalesForce.DataBind();
        }
        else
        {
            grdSalesForce.DataSource = dtUserList;
            grdSalesForce.DataBind();
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("SalesForce.aspx");
    }
    protected void btnVac_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("VacantSFList.aspx");
    }

    protected void grdSalesForce_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdSalesForce.EditIndex = -1;
        //Fill the SalesForce Grid
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillSalesForce();
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
            btnSearch_Click(sender, e);
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

    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs  e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;        
        string sCmd = e.CommandArgument.ToString();
        Session["GetCmdArgChar"] = sCmd;

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
    
    protected void grdSalesForce_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdSalesForce.EditIndex = e.NewEditIndex;
        //Fill the SalesForce Grid
        sCmd = Session["GetCmdArgChar"].ToString();

        Label lblSFType = (Label)grdSalesForce.Rows[e.NewEditIndex].Cells[3].FindControl("lblSFType");//done by resh
        sf_type = Convert.ToInt32(lblSFType.Text);

        if (sCmd == "All")
        {
            FillSalesForce();

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
        //Setting the focus to the textbox "SF Name"        
        TextBox ctrl = (TextBox)grdSalesForce.Rows[e.NewEditIndex].Cells[2].FindControl("txtsfName");
        ctrl.Focus();
    }

    protected void grdSalesForce_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdSalesForce.EditIndex = -1;
        int iIndex = e.RowIndex;        
        UpdateSalesForce(iIndex);
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillSalesForce();
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

    protected void grdSalesForce_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            string sf_code = Convert.ToString(e.CommandArgument);            
            //Deactivate
            SalesForce sf = new SalesForce();
            int iReturn = sf.DeActivate(sf_code);
             if (iReturn > 0 )
            {
               // menu1.Status = "SalesForce has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
                //menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillSalesForce();
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
        sCmd=Session["GetCmdArgChar"].ToString();        

        if (sCmd == "All")
        {
            FillSalesForce();            
        }
        else if (sCmd != "")
        {
            FillSalesForce(sCmd);
        }
        else if (txtsearch.Text !="")
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

    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlStateCode = (DropDownList)e.Row.FindControl("ddlState");
            if (ddlStateCode != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlStateCode.SelectedIndex = ddlStateCode.Items.IndexOf(ddlStateCode.Items.FindByText(row["StateName"].ToString()));              
            }
            DropDownList ddlDesign = (DropDownList)e.Row.FindControl("ddlDesign");
            if (ddlDesign != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlDesign.SelectedIndex = ddlDesign.Items.IndexOf(ddlDesign.Items.FindByText(row["Designation_Name"].ToString()));
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.ToolTip = (e.Row.DataItem as DataRowView)["Reporting_To"].ToString();
        }
     
            
       }

    private void UpdateSalesForce(int eIndex)
    {
        System.Threading.Thread.Sleep(time);
        Label lblSF_Code = (Label)grdSalesForce.Rows[eIndex].Cells[1].FindControl("lblSF_Code");
        sf_code = lblSF_Code.Text;
        TextBox txtsfName = (TextBox)grdSalesForce.Rows[eIndex].Cells[2].FindControl("txtsfName");
        sf_name = txtsfName.Text;
        //TextBox txtUsrName = (TextBox)grdSalesForce.Rows[eIndex].Cells[3].FindControl("txtUsrName");
        //usr_name = txtUsrName.Text;
        DropDownList ddlState= (DropDownList)grdSalesForce.Rows[eIndex].Cells[4].FindControl("ddlState");
        state = Convert.ToInt32(ddlState.SelectedValue);
        TextBox txtHQ = (TextBox)grdSalesForce.Rows[eIndex].Cells[4].FindControl("txtHQ");
        hq = txtHQ.Text;
        DropDownList ddlDesign = (DropDownList)grdSalesForce.Rows[eIndex].Cells[5].FindControl("ddlDesign");
        sf_design = ddlDesign.SelectedValue;
        sf_design_short_name = ddlDesign.SelectedItem.Text;
        // Update SalesForce
        SalesForce sf = new SalesForce();
        int iReturn = sf.RecordUpdate(sf_code, sf_name, state, hq, sf_design, sf_design_short_name);
         if (iReturn > 0 )
        {
            //menu1.Status = "SalesForce Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            //menu1.Status = "SalesForce exist with the same  name!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Exist with the Same Name');</script>");
        }
    }

    protected void btnBulk_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEditDCRStDt.aspx");
    }
    protected void btnBulkTP_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEditTPStartDate.aspx");
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
        if (search == "UsrDfd_UserName" || search == "Sf_Name" ||   search == "sf_emp_id")
        {           
            FindSalesForce(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());            
        }
        else if (search == "StateName")
        {            
            txtsearch.Text = string.Empty;
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getSalesForce_st(div_code, ddlSrc.SelectedValue);
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
            dsSalesForce = sf.getSalesForce_des(div_code, ddlSrc.SelectedValue);
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
        else if (search == "Sf_HQ")
        {
            if (ddlSrc.SelectedValue.ToString() == "All")
            {
                FindSalesForce("Sf_HQ", "", Session["div_code"].ToString());
            }
            else
            {
                FindSalesForce("Sf_HQ", ddlSrc.SelectedValue.ToString(), Session["div_code"].ToString());
            }
        }
       
    }

    private void FindSalesForce(string sSearchBy, string sSearchText, string div_code)
    {
        string sFind = string.Empty;
        sFind = " AND (a." + sSearchBy + " like '" + sSearchText + "%' or a." + sSearchBy + " like '% " + sSearchText + "%') AND (a.Division_Code like '" + div_code + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + div_code + ',' + "%') ";
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.FindSalesForcelist(sFind);
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
    protected void btnBkEd_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEditSalesForce.aspx");
    }
    protected void btnBlk_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("BlockSFList.aspx");
    }
    protected void btnUser_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("User_List.aspx");
    }
    protected void btnStatus_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("SFStatus.aspx");
    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Approval_List.aspx");
    }
    protected void btnDivision_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("MultiDivision.aspx");
    }
    protected void btnPromoDePromo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Salesforce_Promo_DePromo.aspx");
    }
    protected void btnReactivate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("SalesForce_React.aspx");
       // Response.Redirect("MultiDivision.aspx");
    }
    protected void btnInterchan_Click(object sender, EventArgs e)
    {

    }
    protected void btnPromo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Salesforce_Promo_DePromo.aspx?Promote_to_Manager=" + "Promote_to_Manager");
    }

    protected void ddlSrc_SelectedIndexChanged(object sender, EventArgs e)
    {

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
    private void FillHQ()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.gethq(div_code);
        ddlSrc.DataTextField = "Sf_HQ";
        ddlSrc.DataValueField = "Sf_HQ";
        ddlSrc.DataSource = dsSalesForce;
        ddlSrc.DataBind();
        ddlSrc.Items.Insert(0, new ListItem("All", "All"));
       


    }
    private void FillDesignation()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getDesignation_SN(div_code);
        ddlSrc.DataTextField = "Designation_Name";
        ddlSrc.DataValueField = "Designation_Code";
        ddlSrc.DataSource = dsSalesForce;
        ddlSrc.DataBind();
    }
    protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
    {
       // search = Convert.ToInt32(ddlFields.SelectedValue);
        search = ddlFields.SelectedValue.ToString();
        txtsearch.Text = string.Empty;
        grdSalesForce.PageIndex = 0;

        if (search == "UsrDfd_UserName" || search == "Sf_Name" || search == "sf_emp_id")
        {
            txtsearch.Visible = true;
            btnSearch.Visible = true;
            ddlSrc.Visible = false;
            txtsearch.Focus();
        }
        else
        {
            txtsearch.Visible = false;
            ddlSrc.Visible = true;
            btnSearch.Visible = true;
        }
        if (search == "Sf_HQ")
        {
            FillHQ();
            ddlSrc.Focus();
        }
      
        if (search == "StateName")
        {
            FillState(div_code);
            ddlSrc.Focus();
        }
        if (search == "Designation_Name")
        {          
            FillDesignation();
            ddlSrc.Focus();
        }
    }
    protected void btnHo_Create_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Audit_Team.aspx");
    }
    protected void btninterchange_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("HQ_Interchage.aspx");
    }
 protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "FieldForceList.xls"));
        Response.ContentType = "application/ms-excel";
        SalesForce sf = new SalesForce();
        dssalesforce1 = sf.getSalesForcelistEX(div_code);
        DataTable dt = dssalesforce1;
        string str = string.Empty;
        foreach (DataColumn dtcol in dt.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            str = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
        
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
}