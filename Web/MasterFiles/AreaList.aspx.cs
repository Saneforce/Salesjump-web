using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Drawing;
using Bus_EReport;

public partial class MasterFiles_AreaList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDiv = null;
    int subdivcode = 0;
    int subdivision_code = 0;
    string divcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    string state_name = string.Empty;
    DataSet dsState = null;
    DataSet dsDivision = null;
    string state_cd = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string sState = string.Empty;
    string[] statecd;
    int time;
    #endregion
string sf_type = string.Empty;
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillSubdiv();
            btnNew.Focus();
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
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
        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + serverTimeDiff.Ticks + "');</script>");
        time = serverTimeDiff.Minutes;

    }
    private void FillSubdiv()
    {
        SubDivision dv = new SubDivision();
        dsSubDiv = dv.AreagetSubDiv(divcode);
        if (dsSubDiv.Tables[0].Rows.Count > 0)
        {
            grdSubDiv.Visible = true;
            grdSubDiv.DataSource = dsSubDiv;
            grdSubDiv.DataBind();

            foreach (GridViewRow row in grdSubDiv.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                Label lblSubDiv_count = (Label)row.FindControl("lblSubDiv_count");
                //Label lblSubfield_count = (Label)row.FindControl("lblSubfield_count");
                if ((lblSubDiv_count.Text != "0"))
                {
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
        }
        else
        {
            grdSubDiv.DataSource = dsSubDiv;
            grdSubDiv.DataBind();
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
        SubDivision dv = new SubDivision();
        dtGrid = dv.AreagetSubDivisionlist_DataTable(divcode);
        return dtGrid;
    }
    protected void grdSubDiv_Sorting(object sender, GridViewSortEventArgs e)
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
        grdSubDiv.DataSource = sortedView;
        grdSubDiv.DataBind();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("AreaCreation.aspx");
    }
    protected void grdSubDiv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdSubDiv.EditIndex = -1;
        //Fill the Grid
        FillSubdiv();
    }

    protected void grdSubDiv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdSubDiv.EditIndex = e.NewEditIndex;
        //Fill the  Grid
        FillSubdiv();
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)grdSubDiv.Rows[e.NewEditIndex].Cells[2].FindControl("txtShortName");
        ctrl.Focus();
    }
    //protected void grdSubDiv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    Label lblsubdivCode = (Label)grdSubDiv.Rows[e.RowIndex].Cells[1].FindControl("lblsubdivCode");
    //    subdivcode = Convert.ToInt16(lblsubdivCode.Text);

    //    // Delete SubDivision
    //    SubDivision dv = new SubDivision();
    //    int iReturn = dv.RecordDelete(subdivcode);
    //     if (iReturn > 0 )
    //    {
    //        //menu1.Status = "Record Deleted Successfully ";
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Record Deleted Successfully');</script>");
    //    }
    //    else if (iReturn == -2)
    //    {
    //        //menu1.Status = "Record already Exist";
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Record already Exist');</script>");
    //    }
    //    FillSubdiv();
    //}

    //Changes done by priya
    //begin
    protected void grdSubDiv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            // subdivcode = Convert.ToString(e.CommandArgument);
            subdivision_code = Convert.ToInt16(e.CommandArgument);
            //Deactivate
            SubDivision dv = new SubDivision();
            int iReturn = dv.AreaDeActivate(subdivision_code);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Deactivated Successfully.\');", true);
            }
            else
            {
                // menu1.Status ="Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Unable to Deactivate.\');", true);
            }
            FillSubdiv();
        }
    }
    //end
    protected void grdSubDiv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdSubDiv.EditIndex = -1;
        int iIndex = e.RowIndex;
        state_cd = (grdSubDiv.Rows[e.RowIndex].FindControl("ddlState") as DropDownList).SelectedValue.ToString();
        Update(iIndex);
        FillSubdiv();
    }
    protected void grdSubDiv_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdSubDiv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSubDiv.PageIndex = e.NewPageIndex;
        FillSubdiv();
    }
    private void Update(int eIndex)
    {
        Label lblsubdivCode = (Label)grdSubDiv.Rows[eIndex].Cells[1].FindControl("lblsubdivCode");
        subdivcode = Convert.ToInt16(lblsubdivCode.Text);
        TextBox txtShortName = (TextBox)grdSubDiv.Rows[eIndex].Cells[1].FindControl("txtShortName");
        subdiv_sname = txtShortName.Text.Trim();
        TextBox txtSubDivName = (TextBox)grdSubDiv.Rows[eIndex].Cells[2].FindControl("txtSubDivName");
        subdiv_name = txtSubDivName.Text.Trim();
        DropDownList txt_State = (DropDownList)grdSubDiv.Rows[eIndex].Cells[3].FindControl("ddlState");
        state_name = txt_State.SelectedItem.Text;
        if (state_name == "--Select--")
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select State Name');</script>");
        }
        else
        {
            // Update Sub Division
            SubDivision dv = new SubDivision();
            int iReturn = dv.AreaRecordUpdate(subdivcode, subdiv_sname, subdiv_name, state_name, divcode,state_cd);
            if (iReturn > 0)
            {
                //menu1.Status = "Sub Division Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            }
            else if (iReturn == -2)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Area Name Already Exist');</script>");
                txtShortName.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Area Code Already Exist');</script>");
                txtSubDivName.Focus();
            }
        }
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AreaList.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            grdSubDiv.AllowPaging = false;
            this.FillSubdiv();

            grdSubDiv.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grdSubDiv.HeaderRow.Cells)
            {
                cell.BackColor = grdSubDiv.HeaderStyle.BackColor;
            }
            for (int i = 0; i < grdSubDiv.HeaderRow.Cells.Count; i++)
            {
                grdSubDiv.HeaderRow.Cells[i].Style.Add("background-color", "green");
            }

            foreach (GridViewRow row in grdSubDiv.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdSubDiv.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdSubDiv.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
              
                grdSubDiv.Columns[5].Visible = false;
                grdSubDiv.Columns[6].Visible = false;
                grdSubDiv.Columns[7].Visible = false;
                grdSubDiv.Columns[8].Visible = false;

            }

            grdSubDiv.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void grdSubDiv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(divcode);
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
            dsState = st.getStateProd(state_cd);
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                
                DropDownList ddlQual = new DropDownList();
                ddlQual = (DropDownList)e.Row.FindControl("ddlState");

                if (ddlQual != null)
                {
                    ddlQual.DataSource = dsState;
                    ddlQual.DataTextField = "statename";
                    ddlQual.DataValueField = "state_code";
                    ddlQual.DataBind();


                   
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {

                        
                        DropDownList State_Type = (DropDownList)e.Row.FindControl("ddlState");
                        if (State_Type != null)
                        {
                            DataRowView row = (DataRowView)e.Row.DataItem;

                            State_Type.SelectedIndex = State_Type.Items.IndexOf(State_Type.Items.FindByText(row["State"].ToString()));
                          
                        }
                    }
                }
            }
        }
    }
}