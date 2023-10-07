using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Drawing;
using System.IO;

public partial class MasterFiles_Leave_Type_List : System.Web.UI.Page
{
#region "Declaration"
DataSet dsSubDiv = null;
string subdivcodee = string.Empty;
int subdivcode = 0;
string subdivision_codee = string.Empty;
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
        DSM dv = new DSM();
        dsSubDiv = dv.LeavegetSubDiv(divcode);
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
                Label lblSubfield_count = (Label)row.FindControl("lblSubfield_count");
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
        grdSubDiv.DataSource = sortedView;
        grdSubDiv.DataBind();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Leave_Type_Creation.aspx");
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
          //  subdivision_code = Convert.ToInt16(e.CommandArgument);
              subdivision_codee =Convert.ToString(e.CommandArgument);
            //Deactivate
            DSM dv = new DSM();
            int iReturn = dv.DSMDeActivate(subdivision_codee,divcode);
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
       subdivcodee = Convert.ToString(lblsubdivCode.Text);
        TextBox txtShortName = (TextBox)grdSubDiv.Rows[eIndex].Cells[2].FindControl("txtShortName");
        subdiv_sname = txtShortName.Text.Trim();
        TextBox txtSubDivName = (TextBox)grdSubDiv.Rows[eIndex].Cells[3].FindControl("txtSubDivName");
        subdiv_name = txtSubDivName.Text.Trim();
        DropDownList txt_State = (DropDownList)grdSubDiv.Rows[eIndex].Cells[4].FindControl("ddlState");
        Area_name = txt_State.SelectedItem.Text;
	
       if (Area_name == "--Select--")
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Distributor Name');</script>");
        }
        else
        {
            // Update Sub Division
            DSM dv = new DSM();
            int iReturn = dv.RecordUpdate(subdivcodee, subdiv_sname, subdiv_name, Area_name,divcode,Area_code);
            if (iReturn > 0)
            {
                //menu1.Status = "Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            }
            else if (iReturn == -2)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DSM Name Already Exist');</script>");
                txtShortName.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DSM Code Already Exist');</script>");
                txtSubDivName.Focus();
            }
        }
    }
protected void ExportToExcel(object sender, EventArgs e)
    {
		try
		{
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=DSMList.xls");
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
                grdSubDiv.Columns[6].Visible = false;
                grdSubDiv.Columns[7].Visible = false;
                grdSubDiv.Columns[8].Visible = false;
                //grdDivision.Columns[7].Visible = false;


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
 		catch (Exception ex)

        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('"+ex.Message+"');</script>");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    
}