using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MGR_Listeddr_adm_deAct_Approval : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsListedDR = null;
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    string ListedDrCode = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string sQryStr = string.Empty;
    string sfcode = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        sfCode = Session["sf_code"].ToString();
        sQryStr = Request.QueryString["sfcode"].ToString();
        // MR_Code = sQryStr.Substring(0, sQryStr.IndexOf('-'));
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "ListedDR_DeActivate_Approve.aspx";
            //  menu1.Title = this.Page.Title;          
            FillDoc();
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
    private void FillDoc()
    {
        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getListedDr_React_approve(sQryStr);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            btnApprove.Visible = true;
            btnReject.Visible = true;
            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
        }
        else
        {
            btnApprove.Visible = false;
            btnReject.Visible = false;
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
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
        ListedDR LstDoc = new ListedDR();
        dtGrid = LstDoc.getListedDoctorList_DataTable_React(sfCode);
        return dtGrid;
    }
    protected void grdDoctor_Sorting(object sender, GridViewSortEventArgs e)
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
        grdDoctor.DataSource = sortedView;
        grdDoctor.DataBind();
    }
    protected void grdDoctor_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.ClassName='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.ClassName='Normal'");
        }
    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdDoctor.Rows)
        {
            CheckBox chkDR = (CheckBox)gridRow.Cells[0].FindControl("chkListedDR");
            bool bCheck = chkDR.Checked;
            Label lblDR = (Label)gridRow.Cells[2].FindControl("lblDocCode");
            string ListedDR = lblDR.Text.ToString();

            if ((bCheck == true))
            {
                // De-Activate Listed Doctor
                ListedDR LstDoc = new ListedDR();
                iReturn = LstDoc.Approve(sQryStr, ListedDR, 1, 3, Session["sf_name"].ToString());
            }
            else
            {
                //menu1.Status = "Enter all the values!!";
            }
        }

        if (iReturn != -1)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approved Successfully');</script>");
            FillDoc();
        }



    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdDoctor.Rows)
        {
            CheckBox chkDR = (CheckBox)gridRow.Cells[0].FindControl("chkListedDR");
            bool bCheck = chkDR.Checked;
            Label lblDR = (Label)gridRow.Cells[2].FindControl("lblDocCode");
            string ListedDR = lblDR.Text.ToString();

            if ((bCheck == true))
            {
                // De-Activate Listed Doctor
                ListedDR LstDoc = new ListedDR();
                iReturn = LstDoc.Reject(sQryStr, ListedDR, 0, 3, Session["sf_name"].ToString());
            }
            else
            {
                //menu1.Status = "Enter all the values!!";
            }
        }

        if (iReturn != -1)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Rejected Successfully');</script>");
            FillDoc();
        }
    }
  
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("~/BasicMaster.aspx");
    }
}