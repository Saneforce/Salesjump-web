using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MGR_Convert_Unlistto_Listeddr : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSalesforce = null;
    DataSet dsDoctor = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        if(!Page.IsPostBack)
        {
         
            FillManagers();
        }
        if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
           (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.Title = Page.Title;
            c1.FindControl("btnBack").Visible = false;           

        }
        else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            UserControl_MenuUserControl c1 =
           (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = Page.Title;
            c1.FindControl("btnBack").Visible = false;
           
        }
       
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();

        // Check if the manager has a team
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {
            dsSalesforce = sf.SalesForceList_New(div_code, sf_code);
        }
        else
        {
            // Fetch Managers Audit Team
            DataTable dt = ds.getAuditManagerTeam_GetMR(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesforce = dsmgrsf;
        }
      
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();

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
    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillDoc();
    }

    private void FillDoc()
    {
        Doctor dc = new Doctor();
        dsDoctor = dc.getUnListDoctorMgr_list(ddlFieldForce.SelectedValue.ToString());

        if (dsDoctor.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
            btnConvert.Visible = true;
            grdDoctor.DataSource = dsDoctor;
            grdDoctor.DataBind();
        }
        else
        {
            btnConvert.Visible = false;
            grdDoctor.DataSource = dsDoctor;
            grdDoctor.DataBind();
        }
    }
    protected void btnConvert_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;     
        foreach (GridViewRow gridRow in grdDoctor.Rows)
        {
            CheckBox chkDR = (CheckBox)gridRow.Cells[0].FindControl("chkListedDR");
            bool bCheck = chkDR.Checked;
            Label lblDR = (Label)gridRow.Cells[2].FindControl("lblDocCode");
            string ListedDR = lblDR.Text.ToString();

            if ((ListedDR.Trim().Length > 0) && (bCheck == true))
            {               
                ListedDR lstDR = new ListedDR();
                iReturn = lstDR.ConvertDoctors(ListedDR, ddlFieldForce.SelectedValue.ToString());
            }
            else
            {
                //menu1.Status = "Enter all the values!!";
            }
        }

        if (iReturn != -1)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Transfered Successfully');</script>");
            FillDoc();
        }
    }
}