using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_DoctorSpecialityList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocSpe = null;
    int DocSpeCode = 0;
    string divcode = string.Empty;
    string DocSpe_SName = string.Empty;
    string DocSpeName = string.Empty;
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
            FillDocSpe();
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
    private void FillDocSpe()
    {

        Doctor dv = new Doctor();
        dsDocSpe = dv.getDocSpe(divcode);
        if (dsDocSpe.Tables[0].Rows.Count > 0)
        {
            grdDocSpe.Visible = true;
            grdDocSpe.DataSource = dsDocSpe;
            grdDocSpe.DataBind();
            foreach (GridViewRow row in grdDocSpe.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                Label lblCount = (Label)row.FindControl("lblCount");
                //if (Convert.ToInt32(dsDocSpe.Tables[0].Rows[row.RowIndex][3].ToString()) > 0)
                if(lblCount.Text != "0")
                {                    
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
        }
        else
        {
            grdDocSpe.DataSource = dsDocSpe;
            grdDocSpe.DataBind();
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
        dtGrid = dv.getDocSpecialitylist_DataTable(divcode);
        return dtGrid;
    }
    protected void grdDocSpe_Sorting(object sender, GridViewSortEventArgs e)
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
        grdDocSpe.DataSource = dtGrid;
        grdDocSpe.DataBind();
        foreach (GridViewRow row in grdDocSpe.Rows)
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
        Response.Redirect("DoctorSpeciality.aspx");
    }
    protected void grdDocSpe_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdDocSpe.EditIndex = -1;
        //Fill the State Grid
        FillDocSpe();
    }

    protected void grdDocSpe_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdDocSpe.EditIndex = e.NewEditIndex;
        //Fill the State Grid
        FillDocSpe();
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)grdDocSpe.Rows[e.NewEditIndex].Cells[2].FindControl("txtDoc_Spe_SName");
        ctrl.Focus();
    }
    protected void grdDocSpe_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblDocSpeCode = (Label)grdDocSpe.Rows[e.RowIndex].Cells[1].FindControl("lblDocSpeCode");
        DocSpeCode = Convert.ToInt16(lblDocSpeCode.Text);

        // Delete Doctor Speciality
        Doctor dv = new Doctor();
        int iReturn = dv.RecordDeleteDocSpl(DocSpeCode);
         if (iReturn > 0 )
        {
           // menu1.Status = "Doctor Speciality Deleted Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        }
        else if (iReturn == -2)
        {
          //  menu1.Status = "Doctor Speciality cant be deleted";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Delete');</script>");
        }
        FillDocSpe();
    }
    protected void grdDocSpe_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            DocSpeCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Doctor dv = new Doctor();
            int iReturn = dv.DeActivateDocSpl(DocSpeCode);
             if (iReturn > 0 )
            {
               // menu1.Status = "Doctor Speciality has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Doctor Speciality has been Deactivated Successfully');</script>");
            }
            else
            {
               // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillDocSpe();
        }
    }

    protected void grdDocSpe_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdDocSpe.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillDocSpe();
    }
    protected void grdDocSpe_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdDocSpe_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDocSpe.PageIndex = e.NewPageIndex;
        FillDocSpe();
    }
    private void Update(int eIndex)
    {
        Label lblDocSpeCode = (Label)grdDocSpe.Rows[eIndex].Cells[1].FindControl("lblDocSpeCode");
        DocSpeCode = Convert.ToInt16(lblDocSpeCode.Text);
        TextBox txtDocSpe_SName = (TextBox)grdDocSpe.Rows[eIndex].Cells[2].FindControl("txtDoc_Spe_SName");
        DocSpe_SName = txtDocSpe_SName.Text;
        TextBox txtDocSpeName = (TextBox)grdDocSpe.Rows[eIndex].Cells[3].FindControl("txtDocSpeName");
        DocSpeName = txtDocSpeName.Text;

        // Update Doctor Speciality
        Doctor dv = new Doctor();
        int iReturn = dv.RecordUpdateDocSpl(DocSpeCode, DocSpe_SName, DocSpeName, divcode);
         if (iReturn > 0 )
        {
            //menu1.Status = "Doctor Speciality Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
         else if (iReturn == -2)
         {
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
         }
         else if (iReturn == -3)
         {
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
         }
    }
    protected void btnBulkEdit_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEditDocSpec.aspx");
    }
    protected void btnSlNo_Gen_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("DocSpec_SlNo_Gen.aspx");
    }

    protected void btnTransfer_Spec_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("Doc_Spec_Trans.aspx");
    }
    protected void btnReactivate_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("Doc_Spec_React.aspx");
    }
}