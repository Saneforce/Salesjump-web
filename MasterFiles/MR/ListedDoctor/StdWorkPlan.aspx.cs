using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_ListedDoctor_StdWorkPlan : System.Web.UI.Page
{
    string sf_code = string.Empty;
    DataSet dsTerritory = null;
    DataTable dtDoctor;
    DataTable dtTerritory;
    DataSet dsAdmin = null;
    DataSet dsTerr = null;
    int iDRPlan = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;

            FillTerritory();
           // LblUser.Text = "Welcome " + Session["sf_name"];

            AdminSetup adm = new AdminSetup();
            dsAdmin = adm.getAdminSetup();
            if (dsAdmin.Tables[0].Rows.Count > 0)
            {
                ViewState["iDRPlan"] = Convert.ToInt32(dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());                
            }

            //LoadTerritory();
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

    private void FillTerritory()
    {
        CallPlan lstDR = new CallPlan();
        dsTerritory = lstDR.FetchTerritoryName(sf_code);
        grdTerritory.DataSource = dsTerritory;
        grdTerritory.DataBind();
    }

    private void LoadTerritory()
    {
        ListedDR lstDR = new ListedDR();
        dsTerritory = lstDR.LoadTerritory(sf_code, Session["Territory_Code"].ToString().Trim());
        ddlTerritory.DataTextField = "Territory_Name";
        ddlTerritory.DataValueField = "Territory_Code";
        ddlTerritory.DataSource = dsTerritory;
        ddlTerritory.DataBind();
    }


    private void FillDoc(string Terr_Code)
    {
        //ListedDR LstDoc = new ListedDR();
        //dsTerritory = LstDoc.getListedDoctorr_Territory(sf_code, Terr_Code);
        //if (dsTerritory.Tables[0].Rows.Count > 0)
        //{
        //    grdDoctor.Visible = true;
        //    grdDoctor.DataSource = dsTerritory;
        //    grdDoctor.DataBind();
        //    pnlDoctor.Visible=true;
        //}

        if (ViewState["Territory"] != null)
        {
            GrdCopyMove.DataSource = (DataTable)ViewState["Territory"];// retrieving datatable from viewstate  
            GrdCopyMove.DataBind();
        }
        else
        {
            CallPlan LstDoc = new CallPlan();            
            dtDoctor = LstDoc.get_ListedDoctor_Territory(sf_code, Terr_Code);

            if (dtDoctor != null)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dtDoctor;
                grdDoctor.DataBind();
                ViewState["Territory"] = dtDoctor;
            }
        }

        CallPlan  LstTerr = new CallPlan();
        dsTerr = LstTerr.GetTerritoryName(sf_code, Terr_Code);
        if (dsTerr.Tables[0].Rows.Count > 0)
        {
            lblrt.Text = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();                
        }
        pnlDoctor.Visible = true;
    }

    private void LoadDoctor(string Terr_Code)
    {
        // commented out as this is restricting to fetch data based on the route selected(always going to "if")
        //if (ViewState["Doctor"] != null)
        //{
        //    GrdCopyMove.DataSource = (DataTable)ViewState["Doctor"];// retrieving datatable from viewstate  
        //    GrdCopyMove.DataBind(); 
        //}
        //else
        //{

            CallPlan LstDoc = new CallPlan();
            //dsTerritory = LstDoc.getListedDoctorr_Territory(sf_code, Terr_Code);
            dtDoctor = LstDoc.get_ListedDoctor_Territory(sf_code, Terr_Code);

            if (dtDoctor != null)
            {
                GrdCopyMove.Visible = true;
                GrdCopyMove.DataSource = dtDoctor;
                GrdCopyMove.DataBind();
                ViewState["Doctor"] = dtDoctor;
            }
         //}
    }

    protected void grdTerritory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Territory")
        {
            string Territory_Code = Convert.ToString(e.CommandArgument);
            
            Session["Territory_Code"] = Territory_Code;
            pnlImgCopyMove.Visible = true;
            pnlMove.Visible = true;
            ViewState["Doctor"] = null;
            ViewState["Territory"] = null;
            GrdCopyMove.DataSource = null;
            GrdCopyMove.DataBind();
           // lblHeader.Text = "";
            lblrt.Text = Territory_Code;
            FillDoc(Territory_Code);
            LoadTerritory();
        }
    }

    protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTerritory.SelectedValue.ToString().Length > 0)
        {
            pnlMove.Visible = true;
            LoadDoctor(ddlTerritory.SelectedValue.ToString());
        }
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;

        //if (rdoMove.Checked == true)
        if (ViewState["iDRPlan"] != null)
        {
            if (ViewState["iDRPlan"].ToString() == "0")
            {
                foreach (GridViewRow gridRow in grdDoctor.Rows)
                {
                    //CheckBox chkCopyMove = (CheckBox)gridRow.Cells[1].FindControl("chkCopyMove");
                    CheckBox chkRemove = (CheckBox)gridRow.Cells[1].FindControl("chkRemove");
                    Label lblDocCode = (Label)gridRow.Cells[1].FindControl("lblDocCode");
                    Label lblDocName = (Label)gridRow.Cells[1].FindControl("lblDocName");
                    Label lblTerritoryName = (Label)gridRow.Cells[1].FindControl("lblTerritoryName");
                    Label lblPlanNo = (Label)gridRow.Cells[1].FindControl("lblPlanNo");

                    CallPlan lstDR = new CallPlan();

                    if (chkRemove.Checked)
                    {
                        iReturn = lstDR.Remove_CallPlan(Session["Territory_Code"].ToString(), lblDocCode.Text.ToString(), Session["sf_code"].ToString(), Convert.ToInt32(lblPlanNo.Text));
                    }
                    else
                    {
                        iReturn = lstDR.Std_WorkPlan(Session["Territory_Code"].ToString(), lblDocCode.Text.ToString(), Session["sf_code"].ToString(), Convert.ToInt32(lblPlanNo.Text));
                    }
                    if (iReturn < 0)
                    {
                        break;
                    }
                }
            }
            //else if (rdoCopy.Checked == true)
            else if (ViewState["iDRPlan"].ToString() == "1")
            {
                foreach (GridViewRow gridRow in grdDoctor.Rows)
                {
                    CheckBox chkCopyMove = (CheckBox)gridRow.Cells[1].FindControl("chkCopyMove");
                    CheckBox chkRemove = (CheckBox)gridRow.Cells[1].FindControl("chkRemove");
                    Label lblDocCode = (Label)gridRow.Cells[1].FindControl("lblDocCode");
                    Label lblDocName = (Label)gridRow.Cells[1].FindControl("lblDocName");
                    Label lblTerritoryName = (Label)gridRow.Cells[1].FindControl("lblTerritoryName");
                    Label lblTerritoryCode = (Label)gridRow.Cells[1].FindControl("lblTerritoryCode");
                    Label lblPlanNo = (Label)gridRow.Cells[1].FindControl("lblPlanNo");

                    if (lblTerritoryCode.Text.Trim() != Session["Territory_Code"].ToString())
                    {
                        CallPlan lstDR = new CallPlan();
                        iReturn = lstDR.Copy_WorkPlan(Session["Territory_Code"].ToString(), lblDocCode.Text.ToString(), Session["sf_code"].ToString());
                    }
                    else
                    {
                        if (chkRemove.Checked)
                        {
                            CallPlan lstDR = new CallPlan();
                            iReturn = lstDR.Remove_CallPlan(Session["Territory_Code"].ToString(), lblDocCode.Text.ToString(), Session["sf_code"].ToString(), Convert.ToInt32(lblPlanNo.Text));
                        }
                        else
                        {
                            CallPlan lstDR = new CallPlan();
                            //iReturn = lstDR.Std_WorkPlan(Session["Territory_Code"].ToString(), lblDocCode.Text.ToString(), Session["sf_code"].ToString());
                            iReturn = lstDR.Std_WorkPlan(Session["Territory_Code"].ToString(), lblDocCode.Text.ToString(), Session["sf_code"].ToString(), Convert.ToInt32(lblPlanNo.Text));
                        }
                    }

                    if (iReturn < 0)
                    {
                        break;
                    }
                }
            }
        }
        //Response.Write("Territory has been successfully updated");
       // lblHeader.Text = "Route Plan has been successfully updated";
       // menu1.Status = "Route Plan has been updated successfully";
        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated successfully');</script>");
        pnlDoctor.Visible = false;
        pnlImgCopyMove.Visible = false;
        pnlMove.Visible = false;

        FillTerritory();

    }

    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblColor = (Label)e.Row.FindControl("lblColor");
            if (lblColor.Text == "Y")
                e.Row.BackColor = System.Drawing.Color.Yellow;
        }
    }

    protected void chkCopyMove_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkCopyMove = (CheckBox)sender;
        GridViewRow gridRow = (GridViewRow)chkCopyMove.Parent.Parent;
        //string sTest = GrdCopyMove.DataKeys[gr.RowIndex].Value.ToString();

        Label lblDocCode_CopyMove = (Label)gridRow.Cells[1].FindControl("lblDocCode_CopyMove");
        Label lblDocName_CopyMove = (Label)gridRow.Cells[1].FindControl("lblDocName_CopyMove");
        Label lblTerritoryName_CopyMove = (Label)gridRow.Cells[1].FindControl("lblTerritoryName_CopyMove");
        Label lblPlanNo_CopyMove = (Label)gridRow.Cells[1].FindControl("lblPlanNo_CopyMove");
        if (chkCopyMove.Checked == true)
        {
            dtTerritory = (DataTable)ViewState["Territory"];
            DataRow dr = dtTerritory.NewRow();
            dr["ListedDrCode"] = lblDocCode_CopyMove.Text;
            dr["ListedDr_Name"] = lblDocName_CopyMove.Text;
            dr["territory_name"] = lblTerritoryName_CopyMove.Text;
            dr["territory_code"] = ddlTerritory.SelectedValue.ToString().Trim();
            dr["plan_no"] = lblPlanNo_CopyMove.Text;
            dr["color"] = "Y";
            
            dtTerritory.Rows.Add(dr);

            dtDoctor = (DataTable)ViewState["Doctor"];

            if (ViewState["iDRPlan"] != null)
            {
                if (ViewState["iDRPlan"].ToString() == "0")
                {
                    List<DataRow> rows_to_remove = new List<DataRow>();

                    foreach (DataRow rowDR in dtDoctor.Rows)
                    {
                        if (rowDR["ListedDrCode"].ToString().Trim() == lblDocCode_CopyMove.Text.ToString().Trim())
                        //if (rowDR["plan_no"].ToString().Trim() == lblPlanNo_CopyMove.Text.ToString().Trim())
                        {
                            rows_to_remove.Add(rowDR);
                        }
                    }

                    foreach (DataRow row in rows_to_remove)
                    {
                        dtDoctor.Rows.Remove(row);
                        dtDoctor.AcceptChanges();
                    }

                }
            }
        }


        grdDoctor.Visible = true;
        grdDoctor.DataSource = dtTerritory;
        grdDoctor.DataBind();
        ViewState["Territory"] = dtTerritory;


        GrdCopyMove.Visible = true;
        GrdCopyMove.DataSource = dtDoctor;
        GrdCopyMove.DataBind();
        ViewState["Doctor"] = dtDoctor;

        pnlMove.Visible = true;

        //lblmsg.Text = "Hello";
    } 

    protected void imgCopyMove_Click(object sender, ImageClickEventArgs e)
    {
        int i = 0;
        foreach (GridViewRow gridRow in GrdCopyMove.Rows)
        {
            CheckBox chkCopyMove = (CheckBox)gridRow.Cells[1].FindControl("chkCopyMove");
            Label lblDocCode_CopyMove = (Label)gridRow.Cells[1].FindControl("lblDocCode_CopyMove");
            Label lblDocName_CopyMove = (Label)gridRow.Cells[1].FindControl("lblDocName_CopyMove");
            Label lblTerritoryName_CopyMove = (Label)gridRow.Cells[1].FindControl("lblTerritoryName_CopyMove");
            Label lblPlanNo_CopyMove = (Label)gridRow.Cells[1].FindControl("lblPlanNo_CopyMove");
            if (chkCopyMove.Checked == true)
            {
                dtTerritory = (DataTable)ViewState["Territory"];
                DataRow dr = dtTerritory.NewRow();
                dr["ListedDrCode"] = lblDocCode_CopyMove.Text;
                dr["ListedDr_Name"] = lblDocName_CopyMove.Text;
                dr["territory_name"] = lblTerritoryName_CopyMove.Text;
                dr["territory_code"] = ddlTerritory.SelectedValue.ToString().Trim();
                dr["plan_no"] = lblPlanNo_CopyMove.Text;
                dtTerritory.Rows.Add(dr);

                dtDoctor = (DataTable)ViewState["Doctor"];

                //if (rdoMove.Checked == true)
                if (ViewState["iDRPlan"] != null)
                {
                    if (ViewState["iDRPlan"].ToString() == "0")
                    {
                        List<DataRow> rows_to_remove = new List<DataRow>();

                        foreach (DataRow rowDR in dtDoctor.Rows)
                        {
                            //if (rowDR["ListedDrCode"].ToString().Trim() == lblDocCode_CopyMove.Text.ToString().Trim())
                            if (rowDR["plan_no"].ToString().Trim() == lblPlanNo_CopyMove.Text.ToString().Trim())
                            {
                                rows_to_remove.Add(rowDR);
                            }
                        }

                        foreach (DataRow row in rows_to_remove)
                        {
                            dtDoctor.Rows.Remove(row);
                            dtDoctor.AcceptChanges();
                        }

                    }
                }
            }

            i = i + 1;
        }

        grdDoctor.Visible = true;
        grdDoctor.DataSource = dtTerritory;
        grdDoctor.DataBind();
        ViewState["Territory"] = dtTerritory;


        GrdCopyMove.Visible = true;
        GrdCopyMove.DataSource = dtDoctor;
        GrdCopyMove.DataBind();
        ViewState["Doctor"] = dtDoctor;

        pnlMove.Visible = true;
    }
}