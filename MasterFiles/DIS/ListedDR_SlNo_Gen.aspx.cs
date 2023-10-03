using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MR_ListedDoctor_ListedDR_SlNo_Gen : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    DataSet dsListedDR = null;
    DataSet dsTerritory = null;
    DataSet dsSalesForce = null;
    int search = 0;
    string Listed_DR_Code = string.Empty;
    string Listed_DR_Name = string.Empty;
    string Listed_DR_Address = string.Empty;
    string Listed_DR_Catg = string.Empty;
    string Listed_DR_Spec = string.Empty;
    string Listed_DR_Class = string.Empty;
    string Listed_DR_Qual = string.Empty;
    string sCmd = string.Empty;
    string Listed_DR_Terr = string.Empty;
    string Territory = string.Empty;
    string Category = string.Empty;
    string Spec = string.Empty;
    string Qual = string.Empty;
    string Class = string.Empty;
    string doc_code = string.Empty;
    string Territory_Code = string.Empty;
    string SlNo = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iCnt = -1;
    string sf_code = string.Empty;
    string strAdd = string.Empty;
    string strEdit = string.Empty;
    string strDeact = string.Empty;
    string strView = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            div_code = Session["div_code"].ToString();

            if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
            {
                sfCode = Session["sf_code"].ToString();
            }

            if (Session["sf_type"].ToString() == "1")
            {
                sf_code = Session["sf_code"].ToString();
                UserControl_MR_Menu Usc_MR =
             (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;              
                lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                                "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                 "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
                btnBack.Visible = false;

            }
            else
            {
                sf_code = Session["sf_code"].ToString();
                UserControl_MenuUserControl c1 =
              (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                Divid.FindControl("btnBack").Visible = false;
                c1.Title = this.Page.Title;               
                Session["backurl"] = "LstDoctorList.aspx";
                lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                                  "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                   "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
            }
            if (!Page.IsPostBack)
            {                
                Session["backurl"] = "LstDoctorList.aspx";
                ServerStartTime = DateTime.Now;
                base.OnPreInit(e);
                FillDoc();
                Get_SlNo();


            }
        }
        catch (Exception ex)
        {
            Response.Redirect(ex.Message);
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
       
        dsDoc = LstDoc.getListedDr_SlNO(sfCode);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {

            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
        }
        else
        {
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();

        }
    }
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
    //sorting
    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        ListedDR LstDoc = new ListedDR();
        dtGrid = LstDoc.getListedDoctorList_DataTable(sfCode);
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {       
        foreach (GridViewRow gridRow in grdDoctor.Rows)
        {
            DropDownList ddlSlNo = (DropDownList)gridRow.Cells[1].FindControl("ddlSlNo");

            Label ListedDrCode = (Label)gridRow.Cells[0].FindControl("lblDocCode");
            Listed_DR_Code = ListedDrCode.Text;
            // Update Division
            ListedDR dv = new ListedDR();
            int iReturn = dv.Update_LdDoctorSno(Listed_DR_Code, ddlSlNo.SelectedValue.ToString());
            if (iReturn > 0)
            {
                //menu1.Status = "Sl No Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sl No Updated Successfully');</script>");
            }
            else if (iReturn == -2)
            {
                //  menu1.Status = "SlNo could not be updated!!";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SlNo could not be updated');</script>");
            }
        }
    }  

    protected DataSet Get_SlNo()
    {
        ListedDR LstDoc = new ListedDR();
        dsListedDR = LstDoc.getSlNO(sfCode);   
        return dsListedDR;
    }


    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            DropDownList ddlSlNO = (DropDownList)e.Row.FindControl("ddlSlNo");
            string slno = lblSNo.Text;
            if (ddlSlNO != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlSlNO.SelectedValue = slno;       
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                //    e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                LinkButton LnkHeaderText = e.Row.Cells[6].Controls[0] as LinkButton;
                LnkHeaderText.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        }
    }
  
    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Server.Transfer("LstDoctorList.aspx");
        }
        catch (Exception ex)
        {

        }
    }
}