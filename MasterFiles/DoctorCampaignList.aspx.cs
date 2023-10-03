using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_DoctorCampaignList : System.Web.UI.Page
{

#region "Declaration"
DataSet dsDocSubCat = null;
int DocSubCatCode = 0;
string  divcode = string.Empty ;
string Doc_SubCat_SName = string.Empty;
string DocSubCatName = string.Empty;
DateTime ServerStartTime;
DateTime ServerEndTime;
int time;
#endregion
   
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            FillDocSubCat();
            btnNew.Focus();
            menu1.Title = this.Page.Title;      
            menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
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
    private void FillDocSubCat()
    {
        Doctor dv = new Doctor();
        dsDocSubCat = dv.getDocSubCat(divcode);
        if (dsDocSubCat.Tables[0].Rows.Count > 0)
        {
            grdDocSubCat.Visible = true;
            grdDocSubCat.DataSource = dsDocSubCat;
            grdDocSubCat.DataBind();
        }
        else
        {            
            grdDocSubCat.DataSource = dsDocSubCat;
            grdDocSubCat.DataBind();
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
        dtGrid = dv.getDocSubCatlist_DataTable(divcode);
        return dtGrid;
    }
    protected void grdDocSubCat_Sorting(object sender, GridViewSortEventArgs e)
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
        grdDocSubCat.DataSource = sortedView;
        grdDocSubCat.DataBind();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("DoctorCampaign.aspx");
    }
    protected void grdDocSubCat_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdDocSubCat.EditIndex = -1;
        //Fill the State Grid
        FillDocSubCat();
    }

    protected void grdDocSubCat_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdDocSubCat.EditIndex = e.NewEditIndex;
        //Fill the State Grid
        FillDocSubCat();
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)grdDocSubCat.Rows[e.NewEditIndex].Cells[2].FindControl("txtDoc_SubCat_SName");
        ctrl.Focus();
    }
    protected void grdDocSubCat_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblDocSubCatCode = (Label)grdDocSubCat.Rows[e.RowIndex].Cells[1].FindControl("lblDocSubCatCode");
        DocSubCatCode = Convert.ToInt16(lblDocSubCatCode.Text);

        // Delete Doctor Sub-Category
        Doctor dv = new Doctor();
        int iReturn = dv.RecordDeleteSubCat(DocSubCatCode);
         if (iReturn > 0 )
        {
            //menu1.Status = "Doctor Sub-Category Deleted Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        }
        else if (iReturn == -2)
        {
           // menu1.Status = "Doctor Sub-Category cant be deleted";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Delete');</script>");
        }
        FillDocSubCat();
    }
    protected void grdDocSubCat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            DocSubCatCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Doctor dv = new Doctor();
            int iReturn = dv.DeActivateSubCat(DocSubCatCode);
             if (iReturn > 0 )
            {
                //menu1.Status = "Doctor Sub-Category has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
               // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillDocSubCat();
        }
    }

    protected void grdDocSubCat_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdDocSubCat.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillDocSubCat();
    }
    protected void grdDocSubCat_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdDocSubCat_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDocSubCat.PageIndex = e.NewPageIndex;
        FillDocSubCat();
    }
    private void Update(int eIndex)
    {
        System.Threading.Thread.Sleep(time);
        Label lblDocSubCatCode = (Label)grdDocSubCat.Rows[eIndex].Cells[1].FindControl("lblDocSubCatCode");
        DocSubCatCode = Convert.ToInt16(lblDocSubCatCode.Text);
        TextBox txtDoc_SubCat_SName = (TextBox)grdDocSubCat.Rows[eIndex].Cells[2].FindControl("txtDoc_SubCat_SName");
        Doc_SubCat_SName = txtDoc_SubCat_SName.Text;
        TextBox txtDocSubCatName = (TextBox)grdDocSubCat.Rows[eIndex].Cells[3].FindControl("txtDocSubCatName");
        DocSubCatName = txtDocSubCatName.Text;

        // Update Doctor Sub-Category
        Doctor dv = new Doctor();
        int iReturn = dv.RecordUpdateSubCat(DocSubCatCode, Doc_SubCat_SName, DocSubCatName,divcode);
         if (iReturn > 0 )
        {
           // menu1.Status = "Doctor Sub-Category Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
           // menu1.Status = "Doctor Sub-Category already Exist";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Campaign Name Already Exist');</script>");
            txtDocSubCatName.Focus();
        }
        else if (iReturn == -3)
        {
             // menu1.Status = "Doctor Sub-Category already Exist";
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
             txtDoc_SubCat_SName.Focus();
        }
    }

    protected void btnBulkEdit_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEditDocCamp.aspx");
    }
    protected void btnSlNo_Gen_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("DocCamp_SlNo_Gen.aspx");
    }

}