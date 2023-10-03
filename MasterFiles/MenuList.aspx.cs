using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.IO;
using Bus_EReport;

public partial class MasterFiles_MenuList : System.Web.UI.Page
{
#region "Declaration"
DataSet dsSubDiv = null;
int subdivcode = 0;
int subdivision_code = 0;
string divcode = string.Empty;
string subdiv_sname = string.Empty;
string subdiv_name = string.Empty;
string state_name = string.Empty;
string Area_name = string.Empty;
string Area_code = string.Empty;
DataSet dsState = null;
DataSet dsDivision = null;
string state_cd = string.Empty;
DateTime ServerStartTime;
DateTime ServerEndTime;
string sState = string.Empty;
string[] statecd;
int time;
#endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillSubdiv();
            btnNew.Focus();
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
        Zone dv = new Zone();
        dsSubDiv = dv.Menuget(divcode);
        if (dsSubDiv.Tables[0].Rows.Count > 0)
        {
            grdMenu.Visible = true;
            grdMenu.DataSource = dsSubDiv;
            grdMenu.DataBind();

            //foreach (GridViewRow row in grdMenu.Rows)
            //{
            //    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
            //    Label lblimg = (Label)row.FindControl("lblimg");
            //    Label lblSubDiv_count = (Label)row.FindControl("lblSubDiv_count");
            //    if ((lblSubDiv_count.Text != "0"))
            //    {
            //        lnkdeact.Visible = false;
            //        lblimg.Visible = true;
            //    }
            //}
        }
        else
        {
            grdMenu.DataSource = dsSubDiv;
            grdMenu.DataBind();
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
        Zone dv = new Zone();
        dtGrid = dv.ZonegetSubDivisionlist_DataTable(divcode);
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
        grdMenu.DataSource = sortedView;
        grdMenu.DataBind();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("MenuCreation.aspx");
    }
    protected void grdSubDiv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdMenu.EditIndex = -1;
        //Fill the Grid
        FillSubdiv();
    }

    protected void grdSubDiv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdMenu.EditIndex = e.NewEditIndex;
        //Fill the  Grid
        FillSubdiv();
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)grdMenu.Rows[e.NewEditIndex].Cells[2].FindControl("txtShortName");
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
           //subdivcode = Convert.ToString(e.CommandArgument);
           //subdivision_code = e.CommandArgument.ToString();
            string Menu_code = e.CommandArgument.ToString();
            //Deactivate
            Zone dv = new Zone();
            int iReturn = dv.MenuDeActivate(Menu_code);
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
        grdMenu.EditIndex = -1;
        int iIndex = e.RowIndex;
        Area_code = (grdMenu.Rows[e.RowIndex].FindControl("ddlParent") as DropDownList).SelectedValue.ToString();
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
        grdMenu.PageIndex = e.NewPageIndex;
        FillSubdiv();
    }
    private void Update(int eIndex)
    {
        Label lblsubdivCode = (Label)grdMenu.Rows[eIndex].Cells[1].FindControl("lblsubdivCode");
        subdivcode = Convert.ToInt16(lblsubdivCode.Text);
        TextBox txtShortName = (TextBox)grdMenu.Rows[eIndex].Cells[2].FindControl("txtShortName");
        subdiv_sname = txtShortName.Text.Trim();
        TextBox txtSubDivName = (TextBox)grdMenu.Rows[eIndex].Cells[3].FindControl("txtSubDivName");
        subdiv_name = txtSubDivName.Text.Trim();
        DropDownList txt_State = (DropDownList)grdMenu.Rows[eIndex].Cells[4].FindControl("ddlParent");
       Area_name = txt_State.SelectedItem.Text;
       if (Area_name == "--Select--")
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Area Name');</script>");
        }
        else
        {
            // Update Sub Division
            Zone dv = new Zone();
            int iReturn = dv.ZoneRecordUpdate(subdivcode, subdiv_sname, subdiv_name, Area_name,divcode,Area_code);
            if (iReturn == 0)
            {
                //menu1.Status = "Sub Division Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            }
            else if (iReturn == -2)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Zone Name Already Exist');</script>");
                txtShortName.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Zone Code Already Exist');</script>");
                txtSubDivName.Focus();
            }
        }
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ZoneList.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            grdMenu.AllowPaging = false;
            this.FillSubdiv();

            grdMenu.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grdMenu.HeaderRow.Cells)
            {
                cell.BackColor = grdMenu.HeaderStyle.BackColor;
            }
            for (int i = 0; i < grdMenu.HeaderRow.Cells.Count; i++)
            {
                grdMenu.HeaderRow.Cells[i].Style.Add("background-color", "green");
            }

            foreach (GridViewRow row in grdMenu.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdMenu.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdMenu.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }

                grdMenu.Columns[5].Visible = false;
                grdMenu.Columns[6].Visible = false;
                grdMenu.Columns[7].Visible = false;
                grdMenu.Columns[8].Visible = false;

            }

            grdMenu.RenderControl(hw);

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
         Zone dv = new Zone();
         dsDivision = dv.getStatePerDivision(divcode);
         if (dsDivision.Tables[0].Rows.Count > 0)
         {
             //int i = 0;
             state_cd = string.Empty;
             state_cd = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
         }
         Zone st = new Zone();
         dsState = st.getStateProd(divcode);
         if ((e.Row.RowState & DataControlRowState.Edit) > 0)
         {

             DropDownList ddlQual = new DropDownList();
             ddlQual = (DropDownList)e.Row.FindControl("ddlState");

             if (ddlQual != null)
             {
                 ddlQual.DataSource = dsState;
                 ddlQual.DataTextField = "Areaname";
                 ddlQual.DataValueField = "Area_code";
                 ddlQual.DataBind();
                 if (e.Row.RowType == DataControlRowType.DataRow)
                 {


                     DropDownList State_Type = (DropDownList)e.Row.FindControl("ddlState");
                     if (State_Type != null)
                     {
                         DataRowView row = (DataRowView)e.Row.DataItem;

                         State_Type.SelectedIndex = State_Type.Items.IndexOf(State_Type.Items.FindByText(row["Area"].ToString()));

                     }
                 }
             }
         }
    }
}