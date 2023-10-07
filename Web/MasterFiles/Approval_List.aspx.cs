using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Approval_List : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsUserList = null;
    DataSet dsSF = null;
    DataSet dsReport = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string Reporting_To_SF = string.Empty;
    string search = string.Empty;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string[] statecd;
    string sCmd = string.Empty;
    string DCR = string.Empty;
    string TP = string.Empty;
    string Lst_Dr = string.Empty;
    string leave = string.Empty;
    string SS_AM = string.Empty;
    string Expense = string.Empty;
    string Otr_AM = string.Empty;
    DataSet dsState = null;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "SalesForceList.aspx";
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            ddlFilter.Focus();
            //FillSalesForce_Reporting();
            FillSalesForce();
            FillReporting();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = true;
            ddlFields.SelectedValue = "Sf_Name";
            txtsearch.Visible = true;
            btnSearch.Visible = true;
        }
        FillColor();

    }
    protected void grdSalesForce_PreRender(object sender, EventArgs e)
    {
        var gridView = (GridView)sender;
        //var header = (GridViewRow)gridView.Controls[0].Controls[1];

        //header.Cells[0].Visible = false;
        //header.Cells[4].ColumnSpan = 2;
        //header.Cells[6].ColumnSpan = 2;
        //header.Cells[0].RowSpan = 2;
        //header.Cells[1].RowSpan = 2;
        //header.Cells[2].RowSpan = 2;
        //header.Cells[3].RowSpan = 2;
        //header.Cells[1].Text = "Header";
    }

    protected void grdSalesForce_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0, "S.No", "#666699", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#666699", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation", "#666699", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#666699", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Reporting To", "#666699", true);
            AddMergedCells(objgridviewrow, objtablecell, 7, "Approved By Manager", "#666699", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Inline Edit", "#666699", true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, "Vacant", System.Drawing.Color.LightSkyBlue.Name, true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, "Blocked", System.Drawing.Color.LightSkyBlue.Name, true);

            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "DCR", "#666699", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "TP", "#666699", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Listed Customer", "#666699", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Leave", "#666699", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Secondary Sales", "#666699", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Expense", "#666699", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Other", "#666699", false);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            #endregion
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }


    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 2;
        }
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "White");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }
    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getView_AM_New(div_code);//changes in query by resh
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
    }
    // Function of Grid Values
    private void FillSalesForce_Reporting()
    {
        string sReport = ddlFilter.SelectedValue.ToString();
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.getView_AM(div_code, sReport);//changes in query by resh


        DataTable dtUserList = new DataTable();
        dtUserList = sf.getUserListReportingToNew_for_all_Approval_Changes(div_code, sReport, 0, Session["sf_type"].ToString()); // 28-Aug-15 -Sridevi       

        if (dtUserList.Rows.Count > 0)
        {
            if (sReport == "admin")
            {
                dtUserList.Rows[0].Delete();
                dtUserList.Rows[0].Delete();
            }
            else
            {
                dtUserList.Rows[0].Delete();
                dtUserList.Rows[0].Delete();
            }

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
    private void FillReporting()
    {
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.getUserList_Reporting(div_code);
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



    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        //btnApproval.Visible = true;
        // FillSalesForce_Reporting();
        if (ddlFilter.SelectedIndex > 0)
        {
            FillSalesForce_Reporting();
            Session["GetCmdArgChar"] = string.Empty;
        }
        else
        {
            FillSalesForce();
        }
    }

    protected void btnApproval_Click(object sender, EventArgs e)
    {
        Response.Redirect("Salesforce_Approval_Changes.aspx");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Approval_View.aspx");
    }
    protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
    {
        search = ddlFields.SelectedValue.ToString();
        txtsearch.Text = string.Empty;
        grdSalesForce.PageIndex = 0;

        if (search == "UsrDfd_UserName" || search == "Sf_Name" || search == "Sf_HQ" || search == "sf_emp_id")
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

        //if (search == "StateName")
        //{
        //    FillState(div_code);
        //}
        if (search == "Designation_Name")
        {
            FillDesignation();
            ddlSrc.Focus();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetCmdArgChar"] = string.Empty;
        grdSalesForce.PageIndex = 0;
        Search();
    }
    protected void ddlSrc_SelectedIndexChanged(object sender, EventArgs e)
    {

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
    private void Search()
    {
        search = ddlFields.SelectedValue.ToString();
        if (search == "UsrDfd_UserName" || search == "Sf_Name" || search == "Sf_HQ" || search == "sf_emp_id")
        {
            FindSalesForce(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }

        else if (search == "Designation_Name")
        {

            txtsearch.Text = string.Empty;
            //ddlSrc.SelectedIndex = 0;
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getSalesForce_desAp(div_code, ddlSrc.SelectedValue);
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
        sFind = " AND c." + sSearchBy + " like '" + sSearchText + "%' AND a.Division_Code = '" + div_code + "'  ";
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.FindSalesForcelistApp(sFind);
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

    protected DataSet FillSalesForce_Rep()
    {
        string sReport = ddlFilter.SelectedValue.ToString();
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.getSalesForcelist_Rep(div_code);
        dsSalesForce = sf.Change_Rep(sf_code);
        //dsSalesForce = sf.SalesForceList(div_code, sReport);//changes done by resh in procedure
        return dsSalesForce;
    }
    protected void grdSalesForce_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdSalesForce.EditIndex = e.NewEditIndex;
        //Fill the SalesForce Grid
        sCmd = Session["GetCmdArgChar"].ToString();

        Label lblReport = (Label)grdSalesForce.Rows[e.NewEditIndex].Cells[3].FindControl("lblReport");
        sf_code = lblReport.Text;

        if (sCmd == "All")
        {
            FillSalesForce();

        }
        //else if (sCmd != "")
        //{
        //    FillSalesForce(sCmd);
        //}
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
        //TextBox ctrl = (TextBox)grdSalesForce.Rows[e.NewEditIndex].Cells[2].FindControl("txtsfName");
        //ctrl.Focus();
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
        //else if (sCmd != "")
        //{
        //    FillSalesForce(sCmd);
        //}
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
        //else if (sCmd != "")
        //{
        //    FillSalesForce(sCmd);
        //}
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

    protected void grdSalesForce_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSalesForce.PageIndex = e.NewPageIndex;
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillSalesForce();
        }
        //else if (sCmd != "")
        //{
        //    FillSalesForce(sCmd);
        //}
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

    private void UpdateSalesForce(int eIndex)
    {
        System.Threading.Thread.Sleep(time);
        Label lblSF_Code = (Label)grdSalesForce.Rows[eIndex].Cells[1].FindControl("lblSF_Code");
        sf_code = lblSF_Code.Text;

        DropDownList ddlDCR = (DropDownList)grdSalesForce.Rows[eIndex].Cells[4].FindControl("ddlDCR");
        DCR = ddlDCR.SelectedValue;
        DropDownList ddlTP = (DropDownList)grdSalesForce.Rows[eIndex].Cells[4].FindControl("ddlTP");
        TP = ddlTP.SelectedValue;
        DropDownList ddlLstDr = (DropDownList)grdSalesForce.Rows[eIndex].Cells[4].FindControl("ddlLstDr");
        Lst_Dr = ddlLstDr.SelectedValue;
        DropDownList ddlLeave = (DropDownList)grdSalesForce.Rows[eIndex].Cells[4].FindControl("ddlLeave");
        leave = ddlLeave.SelectedValue;
        DropDownList ddlSS = (DropDownList)grdSalesForce.Rows[eIndex].Cells[4].FindControl("ddlSS");
        SS_AM = ddlSS.SelectedValue;
        DropDownList ddlExp = (DropDownList)grdSalesForce.Rows[eIndex].Cells[4].FindControl("ddlExp");
        Expense = ddlExp.SelectedValue;
        DropDownList ddlOtr = (DropDownList)grdSalesForce.Rows[eIndex].Cells[4].FindControl("ddlOtr");
        Otr_AM = ddlOtr.SelectedValue;


        // Update SalesForce
        SalesForce sf = new SalesForce();
        int iReturn = sf.RecordUpdate_App(sf_code, DCR, TP, Lst_Dr, leave, SS_AM, Expense, Otr_AM);
        if (iReturn > 0)
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

    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlDCR = (DropDownList)e.Row.FindControl("ddlDCR");
            if (ddlDCR != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlDCR.SelectedIndex = ddlDCR.Items.IndexOf(ddlDCR.Items.FindByText(row["DCR_AM"].ToString()));
            }
            DropDownList ddlTP = (DropDownList)e.Row.FindControl("ddlTP");
            if (ddlTP != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlTP.SelectedIndex = ddlTP.Items.IndexOf(ddlTP.Items.FindByText(row["TP_AM"].ToString()));
            }
            DropDownList ddlLstDr = (DropDownList)e.Row.FindControl("ddlLstDr");
            if (ddlLstDr != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlLstDr.SelectedIndex = ddlLstDr.Items.IndexOf(ddlLstDr.Items.FindByText(row["LstDr_AM"].ToString()));
            }

            DropDownList ddlLeave = (DropDownList)e.Row.FindControl("ddlLeave");
            if (ddlLeave != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlLeave.SelectedIndex = ddlLeave.Items.IndexOf(ddlLeave.Items.FindByText(row["Leave_AM"].ToString()));
            }
            DropDownList ddlSS = (DropDownList)e.Row.FindControl("ddlSS");
            if (ddlSS != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlSS.SelectedIndex = ddlSS.Items.IndexOf(ddlSS.Items.FindByText(row["SS_AM"].ToString()));
            }
            DropDownList ddlExp = (DropDownList)e.Row.FindControl("ddlExp");
            if (ddlExp != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlExp.SelectedIndex = ddlExp.Items.IndexOf(ddlExp.Items.FindByText(row["Expense_AM"].ToString()));
            }
            DropDownList ddlOtr = (DropDownList)e.Row.FindControl("ddlOtr");
            if (ddlOtr != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlOtr.SelectedIndex = ddlOtr.Items.IndexOf(ddlOtr.Items.FindByText(row["Otr_AM"].ToString()));
            }
        }

    }
}