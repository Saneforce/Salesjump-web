using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_DoctorClassList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocCls = null;
    int DocClsCode = 0;
    string divcode = string.Empty;
    string Doc_Cls_SName = string.Empty;
    string DocClsName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
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
            FillDocCls();
            btnNew.Focus();
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
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
    private void FillDocCls()
    {
        Doctor dv = new Doctor();
        dsDocCls = dv.getDocCls(divcode);
        if (dsDocCls.Tables[0].Rows.Count > 0)
        {
            grdDocCls.Visible = true;
            grdDocCls.DataSource = dsDocCls;
            grdDocCls.DataBind();
            foreach (GridViewRow row in grdDocCls.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                Label lblCount = (Label)row.FindControl("lblCount");
               // if (Convert.ToInt32(dsDocCls.Tables[0].Rows[row.RowIndex][3].ToString()) > 0)
                if(lblCount.Text != "0")
                {
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
        }
        else
        {
            grdDocCls.DataSource = dsDocCls;
            grdDocCls.DataBind();
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
        dtGrid = dv.getDocClslist_DataTable(divcode);
        return dtGrid;
    }
    protected void grdDocCls_Sorting(object sender, GridViewSortEventArgs e)
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
        grdDocCls.DataSource = dtGrid;
        grdDocCls.DataBind();
        foreach (GridViewRow row in grdDocCls.Rows)
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
        Response.Redirect("DoctorClass.aspx");
    }
    protected void grdDocCls_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdDocCls.EditIndex = -1;
        //Fill the State Grid
        FillDocCls();
    }
    protected void grdDocCls_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdDocCls.EditIndex = e.NewEditIndex;
        //Fill the State Grid
        FillDocCls();
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)grdDocCls.Rows[e.NewEditIndex].Cells[2].FindControl("txtDoc_Cls_SName");
        ctrl.Focus();
    }
    protected void grdDocCls_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblDocClsCode = (Label)grdDocCls.Rows[e.RowIndex].Cells[1].FindControl("lblDocClsCode");
        DocClsCode = Convert.ToInt16(lblDocClsCode.Text);

        // Delete Doctor Class
        Doctor dv = new Doctor();
        int iReturn = dv.RecordDeleteCls(DocClsCode);
         if (iReturn > 0 )
        {
           // menu1.Status = "Doctor Class Deleted Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        }
        else if (iReturn == -2)
        {
          //  menu1.Status = "Doctor Class cant be deleted";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(Unable to Delete');</script>");
        }
        FillDocCls();
    }
    protected void grdDocCls_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            DocClsCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Doctor dv = new Doctor();
            int iReturn = dv.DeActivateCls(DocClsCode);
             if (iReturn > 0 )
            {
               // menu1.Status = "Doctor Class has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
               // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillDocCls();
        }
    }
    protected void grdDocCls_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdDocCls.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillDocCls();
    }
    protected void grdDocCls_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected void grdDocCls_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDocCls.PageIndex = e.NewPageIndex;
        FillDocCls();
    }
    private void Update(int eIndex)
    {
        Label lblDocClsCode = (Label)grdDocCls.Rows[eIndex].Cells[1].FindControl("lblDocClsCode");
        DocClsCode = Convert.ToInt16(lblDocClsCode.Text);
        TextBox txtDoc_Cls_SName = (TextBox)grdDocCls.Rows[eIndex].Cells[2].FindControl("txtDoc_Cls_SName");
        Doc_Cls_SName = txtDoc_Cls_SName.Text;
        TextBox txtDocClsName = (TextBox)grdDocCls.Rows[eIndex].Cells[3].FindControl("txtDocClsName");
        DocClsName = txtDocClsName.Text;

        // Update Doctor Class
        Doctor dv = new Doctor();
        int iReturn = dv.RecordUpdateCls(DocClsCode, Doc_Cls_SName, DocClsName, divcode);
         if (iReturn > 0 )
        {
           // menu1.Status = "Doctor Class Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
         else if (iReturn == -2)
         {
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
         }
         else if (iReturn == -3)
         {
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Class Code Already Exist');</script>");
         }
    }

    protected void btnBulkEdit_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEditDocClass.aspx");
    }
    protected void btnSlNo_Gen_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("DocClass_SlNo.aspx");
    }

    protected void btnDoc_Class_Trans_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("Doc_Class_Trans.aspx");
    }
    protected void btnReactivate_Onclick(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("Doc_Class_React.aspx");
    }
}