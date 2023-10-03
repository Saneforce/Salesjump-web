using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;


public partial class MasterFiles_DoctorQualificationList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocQua = null;
    int DocQuaCode = 0;
    string divcode = string.Empty;
    string Doc_Qua_SName = string.Empty;
    string DocQuaName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            FillDocQua();
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
    private void FillDocQua()
    {
        Doctor dv = new Doctor();
        dsDocQua = dv.getDocQua(divcode);
        if (dsDocQua.Tables[0].Rows.Count > 0)
        {
            grdDocQua.Visible = true;
            grdDocQua.DataSource = dsDocQua;
            grdDocQua.DataBind();
            foreach (GridViewRow row in grdDocQua.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                Label lblCount = (Label)row.FindControl("lblCount");
               // if (Convert.ToInt32(dsDocQua.Tables[0].Rows[row.RowIndex][3].ToString()) > 0)
                if(lblCount.Text != "0")
                {
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
        }
        else
        {
            grdDocQua.DataSource = dsDocQua;
            grdDocQua.DataBind();
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
        dtGrid = dv.getDocQualist_DataTable(divcode);
        return dtGrid;
    }
    protected void grdDocQua_Sorting(object sender, GridViewSortEventArgs e)
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
        grdDocQua.DataSource = dtGrid;
        grdDocQua.DataBind();
        foreach (GridViewRow row in grdDocQua.Rows)
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
        Response.Redirect("DoctorQualification.aspx");
    }
    protected void grdDocQua_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks cancel link form "InLine Edit" link (i.e) we do not update,,we want to cancel Inline Edit
        grdDocQua.EditIndex = -1;
        //Fill the State Grid
        FillDocQua();
    }
    protected void grdDocQua_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdDocQua.EditIndex = e.NewEditIndex;
        //Fill the State Grid
        FillDocQua();
        //Setting the focus to the textbox "Short Name"
        TextBox ctrl = (TextBox)grdDocQua.Rows[e.NewEditIndex].Cells[3].FindControl("txtDocQuaName");
        ctrl.Focus();
    }
    protected void grdDocQua_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblDocQuaCode = (Label)grdDocQua.Rows[e.RowIndex].Cells[1].FindControl("lblDocQuaCode");
        DocQuaCode = Convert.ToInt16(lblDocQuaCode.Text);

        //Delete Doctor Qualification
        Doctor dv = new Doctor();
        int iReturn = dv.RecordDeleteQua(DocQuaCode);
        if (iReturn > 0)
        {
           // menu1.Status = "Doctor Qualification Deleted Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        }
        else if (iReturn == -2)
        {
           // menu1.Status = "Doctor Qualification could not be deleted";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Delete');</script>");
        }
        FillDocQua();
    }


    protected void grdDocQua_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            DocQuaCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Doctor dv = new Doctor();
            int iReturn = dv.DeActivateQua(DocQuaCode);
            if (iReturn > 0)
            {
                //menu1.Status = "Doctor Qualification has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
               // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillDocQua();
        }
    }


   
    protected void grdDocQua_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdDocQua.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillDocQua();
    }
    protected void grdDocQua_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.QualificationName='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.QualificationName='normal'");
        }
    }
    protected void grdDocQua_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDocQua.PageIndex = e.NewPageIndex;
        FillDocQua();
    }
    private void Update(int eIndex)
    {
        Label lblDocQuaCode = (Label)grdDocQua.Rows[eIndex].Cells[1].FindControl("lblDocQuaCode");
        DocQuaCode = Convert.ToInt16(lblDocQuaCode.Text);
       // TextBox txtDoc_Qua_SName = (TextBox)grdDocQua.Rows[eIndex].Cells[2].FindControl("txtDoc_Qua_SName");
      //  Doc_Qua_SName = txtDoc_Qua_SName.Text;
        TextBox txtDocQuaName = (TextBox)grdDocQua.Rows[eIndex].Cells[3].FindControl("txtDocQuaName");
        DocQuaName = txtDocQuaName.Text;

        // Update Doctor Qualification
        Doctor dv = new Doctor();
        int iReturn = dv.RecordUpdateQua(DocQuaCode, Doc_Qua_SName, DocQuaName,divcode);
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
        }
    }
    protected void btnBulkEdit_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEditDoc_Qua.aspx");
    }
    protected void btnSlNo_Gen_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("DocQualification_SlNo.aspx");
    }
    protected void btnTransfer_Qua_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("Doc_Qua_Trans.aspx");
    }
    protected void btnReactivate_Onclick(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("Doc_Qua_React.aspx");
    }
}