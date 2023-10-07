using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_ListedDoctor_RoutePlan : System.Web.UI.Page
{
    string sf_code = string.Empty;
    DataSet dsTerritory = null;
    DataTable dtDoctor;
    DataTable dtTerritory;
    DataSet dsAdmin = null;
    DataSet dsTerr = null;
    int iDRPlan = -1;
    DataSet dsTP = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string sTerrCode = string.Empty;
    string strTerrCode = string.Empty;
    string sSearch = string.Empty;
    string div_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        RoutePlan cp = new RoutePlan();

        int MissedCount = cp.getMissedDr(sf_code);
        if (MissedCount > 0)
        {
            lblmon.Text = MissedCount.ToString();
            lblmon.Visible = true;
            lblHead.Visible = true;
        }
        else
        {
            lblmon.Visible = false;
            lblHead.Visible = false;
        }
               
         
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
            GetWorkName();
            //LoadTerritory();
        }
    }
    private void GetWorkName()
    {
       
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {           
            menu1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            lblTerr.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            lblContent.Text = "Please select any " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " from " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " List";
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
        RoutePlan lstDR = new RoutePlan();
        dsTerritory = lstDR.FetchTerritoryName(sf_code);
        grdTerritory.DataSource = dsTerritory;
        grdTerritory.DataBind();
    }

    private void LoadTerritory()
    {
        ListedDR lstDR = new ListedDR();
        if (ViewState["terr_code"] != null)
        {
            sTerrCode = ViewState["terr_code"].ToString();
            sTerrCode = sTerrCode.Substring(0, sTerrCode.Length - 1);
            dsTerritory = lstDR.LoadTerritory(sf_code, Session["Territory_Code"].ToString().Trim(), sTerrCode);
        }
        else
        {
            dsTerritory = lstDR.LoadTerritory(sf_code, Session["Territory_Code"].ToString().Trim());
        }
        ddlTerritory.DataTextField = "Territory_Name";
        ddlTerritory.DataValueField = "Territory_Code";
        
        ddlTerritory.DataSource = dsTerritory;
        ddlTerritory.DataBind();
        FillColor();
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlTerritory.Items)
        {
            if (ColorItems.Text == "Missed DRs")
                ddlTerritory.Items[j].Attributes.Add("style", "background-color: Yellow");

              j = j + 1;
        }
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
            RoutePlan LstDoc = new RoutePlan();
            dtDoctor = LstDoc.get_ListedDoctor_Territory(sf_code, Terr_Code);

            if (dtDoctor != null)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dtDoctor;
                grdDoctor.DataBind();
                ViewState["Territory"] = dtDoctor;
                lblContent.Visible = false;
            }
        }

        RoutePlan LstTerr = new RoutePlan();
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

        RoutePlan LstDoc = new RoutePlan();
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

    private void LoadMissedDoctor(string Terr_Code)
    {
        RoutePlan LstDoc = new RoutePlan();
        dtDoctor = LstDoc.get_MissedDoctor(sf_code);

        if (dtDoctor != null)
        {
            GrdCopyMove.Visible = true;
            GrdCopyMove.DataSource = dtDoctor;
            GrdCopyMove.DataBind();
            ViewState["Doctor"] = dtDoctor;
        }
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

    protected void grdTerritory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTerritory_Code = (Label)e.Row.FindControl("lblTerritory_Code");
            LinkButton lnkbutTerr = (LinkButton)e.Row.FindControl("lnkbutTerr");
            RoutePlan rp = new RoutePlan();
            int icount = rp.getTerrDR_count(Session["sf_code"].ToString(), lblTerritory_Code.Text);
            if(icount > 0)
            {
                if (ViewState["terr_code"] != null)
                {
                    ViewState["terr_code"] = ViewState["terr_code"].ToString() + lblTerritory_Code.Text + ",";
                }
                else
                {
                    ViewState["terr_code"] = lblTerritory_Code.Text + ",";
                }
            }

            lnkbutTerr.Text = lnkbutTerr.Text + "(" + icount + ")";

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {

            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[1].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " List";


            }
        }
    }

    protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTerritory.SelectedValue.ToString().Length > 0) 
        {
            pnlMove.Visible = true;
            if (ddlTerritory.SelectedValue.ToString() != "999")
                LoadDoctor(ddlTerritory.SelectedValue.ToString());
            else
                LoadMissedDoctor(ddlTerritory.SelectedValue.ToString());
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;

        //if (rdoMove.Checked == true)
        if (ViewState["iDRPlan"] != null)
        {
            if (ViewState["iDRPlan"].ToString() == "0") // Single Plan
            {
                foreach (GridViewRow gridRow in grdDoctor.Rows)
                {
                    //CheckBox chkCopyMove = (CheckBox)gridRow.Cells[1].FindControl("chkCopyMove");
                    CheckBox chkRemove = (CheckBox)gridRow.Cells[1].FindControl("chkRemove");
                    Label lblDocCode = (Label)gridRow.Cells[1].FindControl("lblDocCode");
                    Label lblDocName = (Label)gridRow.Cells[1].FindControl("lblDocName");
                    Label lblTerritoryName = (Label)gridRow.Cells[1].FindControl("lblTerritoryName");
                    Label lblPlanNo = (Label)gridRow.Cells[1].FindControl("lblPlanNo");

                    RoutePlan lstDR = new RoutePlan();

                    if (chkRemove.Checked)
                    {
                        //dsTerr = lstDR.GetTerritoryCode(sf_code, lblDocCode);
                        //if (dsTerr.Tables[0].Rows.Count > 0)
                        //{
                        //    string value = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        //    string[] strStateSplit = value.Split(',');
                        //    foreach (string strstate in strStateSplit)
                        //    {
                        //        if (strstate != "")
                        //        {
                        //            dsDoc.Tables[0].DefaultView.RowFilter = "Territory_Code in ('" + strstate + "')";
                        //            DataTable dt = dsDoc.Tables[0].DefaultView.ToTable();
                        //            txtTerritory.Text += dt.Rows[0].ItemArray.GetValue(1).ToString() + ", ";
                        //        }
                        //    }
                        //}
                        iReturn = lstDR.Remove_CallPlan_Single(lblDocCode.Text.ToString(), Session["sf_code"].ToString());
                    }
                    else
                    {
                        iReturn = lstDR.Std_WorkPlan(Session["Territory_Code"].ToString(), lblDocCode.Text.ToString(), Session["sf_code"].ToString());
                    }
                    if (iReturn < 0)
                    {
                        break;
                    }
                }
            }
            //else if (rdoCopy.Checked == true)
            else if (ViewState["iDRPlan"].ToString() == "1") // Multiple Plan
             {
                iReturn = 0;
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
                        RoutePlan lstDR = new RoutePlan();
                        //iReturn = lstDR.Copy_WorkPlan(Session["Territory_Code"].ToString(), lblDocCode.Text.ToString(), Session["sf_code"].ToString());

                        iReturn = lstDR.Std_WorkPlan_Multiple(Session["Territory_Code"].ToString(), lblDocCode.Text.ToString(), Session["sf_code"].ToString());
                        
                    }
                    else
                    {
                        if (chkRemove.Checked)
                        {
                            RoutePlan lstDR = new RoutePlan();
                            dsTerr = lstDR.GetTerritoryCode(Session["sf_code"].ToString(), lblDocCode.Text);
                            if (dsTerr.Tables[0].Rows.Count > 0)
                            {
                                strTerrCode = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            }

                            if(strTerrCode.IndexOf(Session["Territory_Code"].ToString()) != -1)
                            {
                                sSearch = Session["Territory_Code"].ToString() + ",";
                                strTerrCode = strTerrCode.Replace(sSearch,"");
                            }

                            iReturn = lstDR.Remove_CallPlan(strTerrCode, lblDocCode.Text.ToString(), Session["sf_code"].ToString());
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
        RoutePlan cp = new RoutePlan();

        int MissedCount = cp.getMissedDr(sf_code);
        if (MissedCount > 0)
        {
            lblmon.Text = MissedCount.ToString();
            lblmon.Visible = true;
            lblHead.Visible = true;
        }
        else
        {
            lblmon.Visible = false;
            lblHead.Visible = false;
        }
        FillTerritory();

    }

    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblColor = (Label)e.Row.FindControl("lblColor");
            if (lblColor.Text == "Y")
                e.Row.BackColor = System.Drawing.Color.Yellow;

            Label lblTerritoryCode = (Label)e.Row.FindControl("lblTerritoryCode");
            Label lblTerritoryName = (Label)e.Row.FindControl("lblTerritoryName");
            Territory terr = new Territory();
            dsTerr  = terr.getTerritory_Det(Session["sf_code"].ToString(), lblTerritoryCode.Text);

            if (dsTerr.Tables[0].Rows.Count > 0)
                lblTerritoryName.Text = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

            //lnkbutTerr.Text = lnkbutTerr.Text + "(" + icount + ")";
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[4].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " Name";
            }
        }
    }


    protected void GrdCopyMove_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Label lblTerritoryCode = (Label)e.Row.FindControl("lblTerritoryCode");
            Label lblTerritoryName_CopyMove = (Label)e.Row.FindControl("lblTerritoryName_CopyMove");
            Territory terr = new Territory();
            dsTerr = terr.getTerritory_Det(Session["sf_code"].ToString(), ddlTerritory.SelectedValue);

            if (dsTerr.Tables[0].Rows.Count > 0)
                lblTerritoryName_CopyMove.Text = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

            //lnkbutTerr.Text = lnkbutTerr.Text + "(" + icount + ")";
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[3].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " Name";
            }
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
            dr["SLVNo"] = lblPlanNo_CopyMove.Text;
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
                dr["SLVNo"] = lblPlanNo_CopyMove.Text;
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
                            if (rowDR["SLVNo"].ToString().Trim() == lblPlanNo_CopyMove.Text.ToString().Trim())
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