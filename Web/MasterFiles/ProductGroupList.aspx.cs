using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_ProductGroupList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsProGrp = null;
    int ProGrpCode = 0;
    string divcode = string.Empty;
    string Product_Grp_SName = string.Empty;
    string ProGrpName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            FillProGrp();
            btnNew.Focus();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
    }
    private void FillProGrp()
    {
        Product dv = new Product();
        dsProGrp = dv.getProGrp(divcode);
        if (dsProGrp.Tables[0].Rows.Count > 0)
        {
            grdProGrp.Visible = true;
            grdProGrp.DataSource = dsProGrp;
            grdProGrp.DataBind();
            foreach (GridViewRow row in grdProGrp.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                LinkButton lnkcount = (LinkButton)row.FindControl("lnkcount");
                //if (Convert.ToInt32(dsProGrp.Tables[0].Rows[row.RowIndex][3].ToString()) > 0)
                if(lnkcount.Text != "0")
                {
                    lnkdeact.Visible = false;
                   lblimg.Visible = true;
                }                
            }
        }
        else
        {           
            grdProGrp.DataSource = dsProGrp;
            grdProGrp.DataBind();
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
        dtGrid = dv.getProductGrouplist_DataTable(divcode);
       
        return dtGrid;
 
    }

    protected void grdProGrp_Sorting(object sender, GridViewSortEventArgs e)
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
        grdProGrp.DataSource = dtGrid;
        grdProGrp.DataBind();
        Product dv = new Product();
       // dtGrid = dv.getProductGrouplist_DataTable(divcode);
        foreach (GridViewRow row in grdProGrp.Rows)
        {
            LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
            Label lblimg = (Label)row.FindControl("lblimg");
            if (Convert.ToInt32(dtGrid.Rows[row.RowIndex][3].ToString()) > 0)
            {
                lnkdeact.Visible = false;
                lblimg.Visible = true;
            }
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductGroup.aspx");
    }
    protected void grdProGrp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdProGrp.EditIndex = -1;
        //Fill the State Grid
        FillProGrp();
    }

    protected void grdProGrp_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdProGrp.EditIndex = e.NewEditIndex;
        //Fill the State Grid
        FillProGrp();
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)grdProGrp.Rows[e.NewEditIndex].Cells[2].FindControl("txtProduct_Grp_SName");
        ctrl.Focus();
    }
    protected void grdProGrp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblProGrpCode = (Label)grdProGrp.Rows[e.RowIndex].Cells[1].FindControl("lblProGrpCode");
        ProGrpCode = Convert.ToInt16(lblProGrpCode.Text);

        // Delete Product Group
        Product dv = new Product();
        int iReturn = dv.RecordDeleteGrp(ProGrpCode);
         if (iReturn > 0 )
        {
           // menu1.Status = "Product Group Deleted Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        }
        else if (iReturn == -2)
        {
           // menu1.Status = "Product Group cant be deleted";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Delete');</script>");
        }
        FillProGrp();
    }
    protected void grdProGrp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            ProGrpCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Product dv = new Product();
            int iReturn = dv.DeActivateGrp(ProGrpCode);
             if (iReturn > 0 )
            {
               // menu1.Status = "Product Group has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
               // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillProGrp();
        }
    }

    protected void grdProGrp_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdProGrp.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillProGrp();
    }
    protected void grdProGrp_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdProGrp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdProGrp.PageIndex = e.NewPageIndex;
        FillProGrp();
    }
    private void Update(int eIndex)
    {
        System.Threading.Thread.Sleep(time);
        Label lblProGrpCode = (Label)grdProGrp.Rows[eIndex].Cells[1].FindControl("lblProGrpCode");
        ProGrpCode = Convert.ToInt16(lblProGrpCode.Text);
        TextBox txtProduct_Grp_SName = (TextBox)grdProGrp.Rows[eIndex].Cells[2].FindControl("txtProduct_Grp_SName");
        Product_Grp_SName = txtProduct_Grp_SName.Text;
        TextBox txtProGrpName = (TextBox)grdProGrp.Rows[eIndex].Cells[3].FindControl("txtProGrpName");
        ProGrpName = txtProGrpName.Text;

        // Update Product Group
        Product dv = new Product();
        int iReturn = dv.RecordUpdateGrp(ProGrpCode, Product_Grp_SName, ProGrpName, divcode);
         if (iReturn > 0 )
        {
            //menu1.Status = "Product Group Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
         else if (iReturn == -2)
         {
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Group Name Already Exist');</script>");
             txtProGrpName.Focus();
         }
         else if (iReturn == -3)
         {
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Group Code Already Exist');</script>");
             txtProduct_Grp_SName.Focus();
         }
    }

    protected void btnBulkEdit_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEditProd_Group.aspx");
    }
    protected void btnSlNo_Gen_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("ProdGroup_SlNo_Gen.aspx");
    }
    protected void btnReactivate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Prod_Grp_React.aspx");
    }
}