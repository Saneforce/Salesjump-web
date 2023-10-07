using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_ChemistCategoryList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsChemCat = null;
    int ChemCatCode = 0;
    string divcode = string.Empty;
    string Chem_Cat_SName = string.Empty;
    string ChemCatName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);

        if (!Page.IsPostBack)
        {
            FillChemCat();
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
    private void FillChemCat()
    {
        Chemist chem = new Chemist();
        dsChemCat = chem.getChemCat(divcode);
        if (dsChemCat.Tables[0].Rows.Count > 0)
        {
            grdChemCat.Visible = true;
            grdChemCat.DataSource = dsChemCat;
            grdChemCat.DataBind();

            foreach (GridViewRow row in grdChemCat.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                Label lblCount = (Label)row.FindControl("lblCount");
                if (lblCount.Text != "0")
                {
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
        }
        else
        {
            grdChemCat.DataSource = dsChemCat;
            grdChemCat.DataBind();
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
        Chemist chem = new Chemist();
        dtGrid = chem.getChemistCategorylist_DataTable(divcode);
        return dtGrid;
    }

    protected void grdChemCat_Sorting(object sender, GridViewSortEventArgs e)
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
        grdChemCat.DataSource = dtGrid;
        grdChemCat.DataBind();

        foreach (GridViewRow row in grdChemCat.Rows)
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
   
    protected void grdChemCat_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdChemCat.EditIndex = -1;
        FillChemCat();
    }
    protected void grdChemCat_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdChemCat.EditIndex = e.NewEditIndex;
        FillChemCat();
        TextBox ctrl = (TextBox)grdChemCat.Rows[e.NewEditIndex].Cells[2].FindControl("txtChem_Cat_SName");
        ctrl.Focus();
    }
    protected void grdChemCat_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblChemCatCode = (Label)grdChemCat.Rows[e.RowIndex].Cells[1].FindControl("lblChemCatCode");
        ChemCatCode = Convert.ToInt16(lblChemCatCode.Text);

        Chemist chem = new Chemist();
        int iReturn = chem.RecordDeleteChem(ChemCatCode);
        if (iReturn > 0)
        {         
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        }
        else if (iReturn == -2)
        {          
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Delete');</script>");
        }
        FillChemCat();
    }
    protected void grdChemCat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            ChemCatCode = Convert.ToInt16(e.CommandArgument);

            Chemist chem = new Chemist();
            int iReturn = chem.DeActivateChem(ChemCatCode);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillChemCat();
        }

    }
    protected void grdChemCat_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdChemCat.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillChemCat();
    }
    protected void grdChemCat_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected void grdChemCat_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdChemCat.PageIndex = e.NewPageIndex;
        FillChemCat();
    }
    private void Update(int eIndex)
    {
        Label lblChemCatCode = (Label)grdChemCat.Rows[eIndex].Cells[1].FindControl("lblChemCatCode");
        ChemCatCode = Convert.ToInt16(lblChemCatCode.Text);
        TextBox txtChem_Cat_SName = (TextBox)grdChemCat.Rows[eIndex].Cells[1].FindControl("txtChem_Cat_SName");
        Chem_Cat_SName = txtChem_Cat_SName.Text;
        TextBox txtChemCatName = (TextBox)grdChemCat.Rows[eIndex].Cells[1].FindControl("txtChemCatName");
        ChemCatName = txtChemCatName.Text;

        Chemist chem = new Chemist();
        int iReturn = chem.RecordUpdate_Chem_code(ChemCatCode, Chem_Cat_SName, ChemCatName, divcode);
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
            txtChemCatName.Focus();
        }
        else if (iReturn == -3)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
            txtChemCatName.Focus();
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["backurl"] = "ChemistCategoryList.aspx";
        Response.Redirect("ChemistCategory.aspx");
    }
    protected void btnBulkEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEditChemCat.aspx");
    }
    protected void btnSlNo_Gen_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ChemCat_SlNo_Gen.aspx");
    }
    protected void btnReactivate_Onclick(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Chem_Cat_React.aspx");
    }
    protected void btnTransfer_Cat_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Chem_Cat_Trans.aspx");
    }
}