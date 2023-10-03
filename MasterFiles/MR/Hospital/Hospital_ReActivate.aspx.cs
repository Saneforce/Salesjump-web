using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MR_Hospital_Hospital_ReActivate : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsHospital = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        //sfCode = Session["sf_code"].ToString();
        if (Session["sf_type"].ToString() == "1")
        {
            sfCode = Session["sf_code"].ToString();
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
            sfCode = Session["sf_code"].ToString();
            UserControl_MenuUserControl c1 =
         (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            btnBack.Visible = false;
            //menu1.Visible = false;
            Session["backurl"] = "HospitalList.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                              "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
        }
        if (!Page.IsPostBack)
        {
            FillHospital();
            //menu1.Title = this.Page.Title;
            Session["backurl"] = "HospitalList.aspx";
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
    protected void grdHospital_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                //  e.Row.Cells[9].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                LinkButton LnkHeaderText = e.Row.Cells[5].Controls[0] as LinkButton;
                LnkHeaderText.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        }

    }
    private void FillHospital()
    {
        Hospital hosp = new Hospital();
        dsHospital = hosp.getHospital_ReAct(sfCode);
        if (dsHospital.Tables[0].Rows.Count > 0)
        {
            btnSave.Visible = true;
            grdHospital.Visible = true;
            grdHospital.DataSource = dsHospital;
            grdHospital.DataBind();
        }
        else
        {
            grdHospital.DataSource = dsHospital;
            grdHospital.DataBind();
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
        Hospital hosp = new Hospital();
        dtGrid = hosp.getHospitallist_DataTable_ReAct(sfCode);
        return dtGrid;
    }
    protected void grdHospital_Sorting(object sender, GridViewSortEventArgs e)
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
        grdHospital.DataSource = sortedView;
        grdHospital.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdHospital.Rows)
        {
            CheckBox chkDR = (CheckBox)gridRow.Cells[0].FindControl("chkHospital");
            bool bCheck = chkDR.Checked;
            Label lblHospital_code = (Label)gridRow.Cells[2].FindControl("lblHospitalCode");
            string Hospital_Code = lblHospital_code.Text.ToString();

            if ((Hospital_Code.Trim().Length > 0) && (bCheck == true))
            {
                // De-Activate Hospital                
                Hospital hosp = new Hospital();
                iReturn = hosp.ReActivate(Hospital_Code);
            }
            else
            {
                //menu1.Status = "Enter all the values!!";
            }
        }

        if (iReturn != -1)
        {            
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Re-Activated Successfully');</script>");
            FillHospital();
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Server.Transfer("HospitalList.aspx");
        }
        catch (Exception ex)
        {

        }
    }


}