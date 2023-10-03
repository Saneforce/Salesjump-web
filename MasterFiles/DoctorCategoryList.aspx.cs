using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_DoctorCategoryList : System.Web.UI.Page
{

#region "Declaration"
DataSet dsDocCat = null;
int DocCatCode = 0;
string divcode = string.Empty;
string Doc_Cat_SName = string.Empty;
string DocCatName = string.Empty;
string Docvisit = string.Empty;
DateTime ServerStartTime;
DateTime ServerEndTime;
int time;
#endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            FillDocCat();
            btnNew.Focus();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
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
    private void FillDocCat()
    {
        Doctor dv = new Doctor();
        dsDocCat = dv.getDocCat(divcode);
        if (dsDocCat.Tables[0].Rows.Count > 0)
        {
            grdDocCat.Visible = true;
            grdDocCat.DataSource = dsDocCat;
            grdDocCat.DataBind();
            foreach (GridViewRow row in grdDocCat.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                Label lblCount = (Label)row.FindControl("lblCount");
                //if (Convert.ToInt32(dsDocCat.Tables[0].Rows[row.RowIndex][4].ToString()) > 0)
                if(lblCount.Text != "0")
                {
                    // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
        }
        else
        {
            grdDocCat.DataSource = dsDocCat;
            grdDocCat.DataBind();
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
        Doctor dv = new Doctor();
        dtGrid = dv.getDoctorCategorylist_DataTable(divcode);
        return dtGrid;
    }
    protected void grdDocCat_Sorting(object sender, GridViewSortEventArgs e)
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
        DataTable dtGrid = new DataTable();
        dtGrid = sortedView.ToTable();
        grdDocCat.DataSource = dtGrid;
        grdDocCat.DataBind();      
       
        foreach (GridViewRow row in grdDocCat.Rows)
        {
            LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
            Label lblimg = (Label)row.FindControl("lblimg");
            if (Convert.ToInt32(dtGrid.Rows[row.RowIndex][4].ToString()) > 0)
            {                
                lnkdeact.Visible = false;
                lblimg.Visible = true;
            }
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Session["backurl"] = "DoctorCategoryList.aspx";
        Response.Redirect("DoctorCategory.aspx");
    }

    protected void grdDocCat_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdDocCat.EditIndex = -1;
        //Fill the State Grid
        FillDocCat();
    }

    protected void grdDocCat_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdDocCat.EditIndex = e.NewEditIndex;
        //Fill the State Grid
        FillDocCat();
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)grdDocCat.Rows[e.NewEditIndex].Cells[2].FindControl("txtDoc_Cat_SName");
        ctrl.Focus();
    }
    protected void grdDocCat_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblDocCatCode = (Label)grdDocCat.Rows[e.RowIndex].Cells[1].FindControl("lblDocCatCode");
        DocCatCode = Convert.ToInt16(lblDocCatCode.Text);

        // Delete Doctor Category
        Doctor dv = new Doctor();
        int iReturn = dv.RecordDelete(DocCatCode);
         if (iReturn > 0 )
        {
           // menu1.Status = "Doctor Category Deleted Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            //menu1.Status = "Doctor Category cant be deleted";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Delete');</script>");
        }
        FillDocCat();
    }
    protected void grdDocCat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            DocCatCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Doctor dv = new Doctor();
            int iReturn = dv.DeActivate(DocCatCode);
             if (iReturn > 0 )
            {
                //menu1.Status = "Doctor Category has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
               // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillDocCat();
        }
    }

    protected void grdDocCat_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdDocCat.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillDocCat();
    }
    protected void grdDocCat_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdDocCat_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDocCat.PageIndex = e.NewPageIndex;
        FillDocCat();
    }
    private void Update(int eIndex)
    {
        Label lblDocCatCode = (Label)grdDocCat.Rows[eIndex].Cells[1].FindControl("lblDocCatCode");
        DocCatCode = Convert.ToInt16(lblDocCatCode.Text);
        TextBox txtDoc_Cat_SName = (TextBox)grdDocCat.Rows[eIndex].Cells[2].FindControl("txtDoc_Cat_SName");
        Doc_Cat_SName = txtDoc_Cat_SName.Text;
        TextBox txtDocCatName = (TextBox)grdDocCat.Rows[eIndex].Cells[3].FindControl("txtDocCatName");
        DocCatName = txtDocCatName.Text;
        TextBox txtvisit = (TextBox)grdDocCat.Rows[eIndex].Cells[4].FindControl("txtvisit");
        Docvisit = txtvisit.Text;
        // Update Doctor Category
        Doctor dv = new Doctor();
        int iReturn = dv.RecordUpdate(DocCatCode, Doc_Cat_SName, DocCatName, Docvisit, divcode);
         if (iReturn > 0 )
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
            txtDocCatName.Focus();
        }
         else if (iReturn == -3)
         {
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
             txtDoc_Cat_SName.Focus();
         }
    }
    protected void btnBulkEdit_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEditDocCat.aspx");        
    }
    protected void btnSlNo_Gen_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("DocCat_SlNo_Gen.aspx");
    }
    protected void btnTransfer_Cat_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("Doc_Cat_Trans.aspx");
    }
    protected void btnReactivate_Onclick(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("Doc_Cat_React.aspx");
    }
}