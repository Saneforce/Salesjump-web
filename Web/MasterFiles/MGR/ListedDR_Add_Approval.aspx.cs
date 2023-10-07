using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MGR_ListedDR_Add_Approval : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsListedDR = null;
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    string ListedDrCode = string.Empty;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string sQryStr = string.Empty;
    string sfcode = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        sQryStr = Request.QueryString["sfcode"].ToString();
       // MR_Code = sQryStr.Substring(0, sQryStr.IndexOf('-'));
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "ListedDR_Add_Approve.aspx";
           // menu1.Title = this.Page.Title;          
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
        dsDoc = LstDoc.getListedDr_Add_approve(sQryStr);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {           
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
    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[9].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();           

            }
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
                ListedDR LstDoc = new ListedDR();
              //  iReturn = LstDoc.Approve(sQryStr, ListedDR, 1, 3);
                iReturn = LstDoc.ApproveAdd(sQryStr, ListedDR, 0, 2, Session["sf_name"].ToString());
                if (iReturn > 0)
                {
                  // menu1.Status = "Listed Doctor has been Approved Successfully";
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Listed Doctor has been Approved Successfully');</script>");
                }
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
                iReturn = LstDoc.Reject_Approve(sQryStr, ListedDR, 4, 2, Session["sf_name"].ToString());
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("MGR_Index.aspx");
    }
   
}