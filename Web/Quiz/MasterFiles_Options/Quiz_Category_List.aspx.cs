using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Options_Quiz_Category_List : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsCategory = null;
   
    string divcode = string.Empty;
	 string sf_type = string.Empty;
    int CategoryId;
    string Category_Sname = string.Empty;
    string Category_Name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
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
            FillCategory();
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

    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Quiz_Category_Creation.aspx");
    }

    private void FillCategory()
    {
        Product dv = new Product();

        dsCategory = dv.Quiz_Category_List(divcode);
        if (dsCategory.Tables[0].Rows.Count > 0)
        {
            gvCategoryList.Visible = true;
            gvCategoryList.DataSource = dsCategory;
            gvCategoryList.DataBind();

            //foreach (GridViewRow row in gvCategoryList.Rows)
            //{
            //    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
            //    Label lblimg = (Label)row.FindControl("lblimg");

            //    lnkdeact.Visible = false;
            //    lblimg.Visible = true;
            //}

        }
        else
        {
            gvCategoryList.DataSource = dsCategory;
            gvCategoryList.DataBind();
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
        Product dv = new Product();
        dtGrid = dv.Quiz_Category_List_Sorting(divcode);
        return dtGrid;
    }


    protected void gvCategory_Sorting(object sender, GridViewSortEventArgs e)
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
        gvCategoryList.DataSource = dtGrid;
        gvCategoryList.DataBind();      


        //foreach (GridViewRow row in gvCategoryList.Rows)
        //{
        //    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
        //    Label lblimg = (Label)row.FindControl("lblimg");
          
        //        lnkdeact.Visible = false;
        //        lblimg.Visible = true;
           
        //}

    }

    protected void gvCategoryList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        gvCategoryList.EditIndex = e.NewEditIndex;
        //Fill the  Grid
        FillCategory();
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)gvCategoryList.Rows[e.NewEditIndex].Cells[2].FindControl("txtShortName");
        ctrl.Focus();
    }

    protected void gvCategoryList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gvCategoryList.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillCategory();
    }

    protected void gvCategoryList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        gvCategoryList.EditIndex = -1;
        //Fill the Grid
        FillCategory();
    }

    protected void gvCategoryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            // subdivcode = Convert.ToString(e.CommandArgument);
            CategoryId = Convert.ToInt16(e.CommandArgument);
            //Deactivate
            Product dv = new Product();
            int iReturn = dv.DeActivate_Category(CategoryId);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Deactivated Successfully.\');", true);
            }
            else
            {
                // menu1.Status ="Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "createCustomAlert(\'Unable to Deactivate.\');", true);
            }

            FillCategory();
        }
    }

    protected void gvCategoryList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    private void Update(int eIndex)
    {
        Label lblCategoryId = (Label)gvCategoryList.Rows[eIndex].Cells[1].FindControl("lblCategoryId");
        CategoryId = Convert.ToInt32(lblCategoryId.Text);
        TextBox txtShortName = (TextBox)gvCategoryList.Rows[eIndex].Cells[2].FindControl("txtShortName");
        Category_Sname = txtShortName.Text;
        TextBox txtCategoryName = (TextBox)gvCategoryList.Rows[eIndex].Cells[3].FindControl("txtCategoryName");
        Category_Name = txtCategoryName.Text;

        // Update Category

        Product dv = new Product();
        int iReturn = dv.Update_Quiz_Category(Category_Sname, Category_Name, divcode, CategoryId);
        if (iReturn > 0)
        {
            //menu1.Status = "Sub Division Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Category Name Already Exist');</script>");
           
            txtCategoryName.Focus();
        }
        else if (iReturn == -3)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Category Short Name Already Exist');</script>");
            txtShortName.Focus();
        }
    }

}