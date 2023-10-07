using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_Territory : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsTerritory = null;
    DataSet dsListedDR = null;
    string sf_code = string.Empty;
    string Territory_Code = string.Empty;
    string Territory_Type = string.Empty;
    string Territory_Name = string.Empty;
    string code = string.Empty;
    string strSF_Index = string.Empty;
    string Territory_SName = string.Empty;
    string min = string.Empty;
    int i;
    string div_code;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsSalesForce = null;
    string strAdd = string.Empty;
    string strEdit = string.Empty;
    string strView = string.Empty;
    string strDeact = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["Division_Code"].ToString().Replace(",","");
        Session["Div_code"] = div_code.ToString();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            btnNew.Focus();
            if (Session["sf_type"].ToString() == "3")
            {
                UserControl_DIS_Menu c3 =
                            (UserControl_DIS_Menu)LoadControl("~/UserControl/DIS_Menu.ascx");
                Divid.Controls.Add(c3);
                getddlSF_Code();
            }
            else
            if (Session["sf_type"].ToString() == "1")
            {
                sf_code = Session["sf_code"].ToString();
                //menu1.Visible = true;
                UserControl_MR_Menu Usc_MR =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Territory terr = new Territory();
                dsTerritory = terr.getWorkAreaName(div_code);
                if (dsTerritory.Tables[0].Rows.Count > 0)
                {
                    btnNew.Text = "Add" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
                    //btnEdit.Text = "Edit all" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
                  //btnTranfer.Text = "Transfer" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
                    //menu1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "List";
                    Usc_MR.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "List";
                }
                // Usc_MR.Title = this.Page.Title;
                pnlAdmin.Visible = false;
                //menu1.FindControl("btnBack").Visible = false;
                Usc_MR.FindControl("btnBack").Visible = false;
                // btnBack.Visible = false;
                ViewTerritory();
            }
            else
            {
                getddlSF_Code();
                pnlAdmin.Visible = true;
                // menu1.Visible = false;
                UserControl_MenuUserControl c1 =
                (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                Session["backurl"] = "Territory.aspx";
                c1.FindControl("btnBack").Visible = false;
                Territory terr = new Territory();
                dsTerritory = terr.getWorkAreaName(div_code);
                if (dsTerritory.Tables[0].Rows.Count > 0)
                {
                    btnNew.Text = "Add" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
                    //btnEdit.Text = "Edit all" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
                    //btnTranfer.Text = "Transfer" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
                    //menu1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "List";
                    c1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "List";
                }
                //menu1.FindControl("btnBack").Visible = false;               
                ViewTerritory();

            }

            GetHQ();
            // GetWorkName();
            ButtonDisable();
        }
        else
        {
            if (Session["sf_type"].ToString() == "1")
            {
                UserControl_MR_Menu Usc_MR =
                    (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
            }
            else
            {
                UserControl_DIS_Menu c3 =
                             (UserControl_DIS_Menu)LoadControl("~/UserControl/DIS_Menu.ascx");
                Divid.Controls.Add(c3);
            }
           
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
    private void GetWorkName()
    {
        UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            btnNew.Text = "Add" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
            //btnEdit.Text = "Edit all" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
            //btnTranfer.Text = "Transfer" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
            //menu1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "List";
            c1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "List";
        }
    }

    private void GetddlValue()
    {
        Territory terr = new Territory();
        string value = Request.QueryString["SF_Index"];
        dsTerritory = terr.getTerritory(value);
        for (int i = 0; i < ddlSFCode.Items.Count; i++)
        {


            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                grdTerritory.Visible = true;
                grdTerritory.DataSource = dsTerritory;
                grdTerritory.DataBind();

            }
            else
            {
                grdTerritory.DataSource = dsTerritory;
                grdTerritory.DataBind();

            }
        }
    }

    protected void Alpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ddlvar = Alpha.SelectedItem.ToString();
        ListedDR Lstdr = new ListedDR();
        if (ddlvar == "---ALL---")
        {
            getddlSF_Code();
        }
        else
        {
            dsListedDR = Lstdr.getListeddr_Alphabet(ddlvar, div_code);
        }
        if (dsListedDR != null)
        {
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {

                ddlSFCode.Visible = true;
                ddlSFCode.DataSource = dsListedDR;
                ddlSFCode.DataBind();
            }
        }

    }

    private void ViewTerritory()
    {
        Territory terr = new Territory();
        for (int i = 1; i < ddlSFCode.Items.Count; i++)
        {
            string str = Session["sf_code"].ToString();
            if (ddlSFCode.Items[i].Value == str)
            {
                ddlSFCode.SelectedIndex = i;

            }
        }

        dsTerritory = terr.getTerritory(Session["sf_code"].ToString());
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            grdTerritory.Visible = true;
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
            foreach (GridViewRow row in grdTerritory.Rows)
            {
                //LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                //Label lblimg = (Label)row.FindControl("lblimg");
                Label lblListedDRCnt = (Label)row.FindControl("lblListedDRCnt");
                Label lblChemistsCnt = (Label)row.FindControl("lblChemistsCnt");
                //Label lblUnListedDRCnt = (Label)row.FindControl("lblUnListedDRCnt");
                //if (Convert.ToInt32(dsTerritory.Tables[0].Rows[row.RowIndex][4].ToString()) > 0 || Convert.ToInt32(dsTerritory.Tables[0].Rows[row.RowIndex][5].ToString()) > 0 || Convert.ToInt32(dsTerritory.Tables[0].Rows[row.RowIndex][7].ToString()) > 0)
                if ((lblListedDRCnt.Text != "0") || (lblChemistsCnt.Text != "0"))
                {
                    // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                    //lnkdeact.Visible = false;
                    //lblimg.Visible = true;
                }
            }
        }
        else
        {
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
        }


    }

    private void getddlSF_Code()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getSF_dis(Session["sf_name"].ToString());
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlSFCode.DataTextField = "DSM_name";
            ddlSFCode.DataValueField = "DSM_code";
            ddlSFCode.DataSource = dsTerritory;
            ddlSFCode.DataBind();

            if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
            {
                ddlSFCode.SelectedIndex = 1;
                Session["sf_code"] = ddlSFCode.SelectedValue;
            }

        }

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("TerritoryCreation.aspx");
    }

    protected void grdTerritory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdTerritory.EditIndex = -1;
        //Fill the Division Grid
        ViewTerritory();
    }

    protected void grdTerritory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdTerritory.EditIndex = e.NewEditIndex;
        //Fill the Division Grid
        ViewTerritory();
        //Setting the focus to the textbox "Division Name"        
        TextBox ctrl = (TextBox)grdTerritory.Rows[e.NewEditIndex].Cells[2].FindControl("txtTerritory_Name");
        ctrl.Focus();
    }

    protected void grdTerritory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdTerritory.EditIndex = -1;
        int iIndex = e.RowIndex;
        UpdateTerritory(iIndex);
        ViewTerritory();
    }

    protected void grdTerritory_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if ((e.Row.RowState & DataControlRowState.Edit) > 0)
        {

            //DropDownList ddlQual = new DropDownList();
            //ddlQual = (DropDownList)e.Row.FindControl("ddlDist_Territory");
            //if (ddlQual != null)
            //{
            //    string sfname = Session["sf_code"].ToString();
            //    Territory terr = new Territory();
            //    DataSet ds = terr.getTerritory_dm(sfname);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        string sfcode1 = ds.Tables[0].Rows[0]["Reporting_To_SF"].ToString();

            //        DataSet ds1 = terr.getTerritory_dc(sfcode1);
            //        if (ds1.Tables[0].Rows.Count > 0)
            //        {
            //            ddlQual.DataSource = ds1;
            //            ddlQual.DataTextField = "sf_Name";
            //            ddlQual.DataValueField = "sf_Name";
            //            ddlQual.DataBind();
            //        }
            //    }
            DropDownList ddlQual = new DropDownList();
            ddlQual = (DropDownList)e.Row.FindControl("ddlDist_Territory");

            if (ddlQual != null)
            {
                string sfname = Session["sf_code"].ToString();
                Territory terr = new Territory();
                DataSet ds = terr.getTerritory_dm(sfname);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlQual.DataSource = ds;
                    ddlQual.DataTextField = "Distributor_Name";
                    ddlQual.DataValueField = "Distributor_Code";
                    ddlQual.DataBind();
                }
            }
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{


            //    DropDownList State_Type = (DropDownList)e.Row.FindControl("ddlDist_Territory");
            //    if (State_Type != null)
            //    {
            //        DataRowView row = (DataRowView)e.Row.DataItem;

            //        State_Type.SelectedIndex = State_Type.Items.IndexOf(State_Type.Items.FindByText(row["sf_Name"].ToString()));

            //    }
            //}
        }
    }


    protected void grdTerritory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            Territory_Code = Convert.ToString(e.CommandArgument);

            //Deactivate
            Territory Terr = new Territory();
            int iReturn = Terr.DeActivate(Territory_Code);
            if (iReturn > 0)
            {
                // menu1.Status = "Territory has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            ViewTerritory();
        }
    }

    protected void grdTerritory_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdTerritory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTerritory.PageIndex = e.NewPageIndex;

        ViewTerritory();

    }

    private void UpdateTerritory(int eIndex)
    {
        Label lbl_Territory_Code = (Label)grdTerritory.Rows[eIndex].Cells[1].FindControl("lblTerritorys_Code");
        Territory_Code = lbl_Territory_Code.Text;
        Label txt_Territorys_Code = (Label)grdTerritory.Rows[eIndex].Cells[2].FindControl("lblTerritory_Code");
        code = txt_Territorys_Code.Text;
        TextBox txt_Territory_Name = (TextBox)grdTerritory.Rows[eIndex].Cells[3].FindControl("txtTerritory_Name");
        Territory_Name = txt_Territory_Name.Text;
        DropDownList ddl_Territory_Type = (DropDownList)grdTerritory.Rows[eIndex].Cells[4].FindControl("ddlDist_Territory");
        Territory_Type = ddl_Territory_Type.SelectedValue.ToString();
        TextBox txt_Territory_Sname = (TextBox)grdTerritory.Rows[eIndex].Cells[5].FindControl("txtTerritory_Target");
        Territory_SName = txt_Territory_Sname.Text;
        TextBox txt_Territory_min = (TextBox)grdTerritory.Rows[eIndex].Cells[6].FindControl("txtTerritory_minprod");
        min = txt_Territory_min.Text;
        // Update Territory
        Territory Terr = new Territory();
        //int iReturn = Terr.RecordUpdate(Territory_Code, code, Territory_Name, Territory_Type, Territory_SName, min);
        if (iReturn > 0)
        {
            //menu1.Status = "Territory Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            //menu1.Status = "Territory exist with the same short name!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Exist with the same short Name');</script>");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Territory_BulkEdit.aspx");
    }
    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("TransferTerritory.aspx");
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
        Territory terr = new Territory();
        dtGrid = terr.getTerritorylist_DataTable(Session["sf_code"].ToString());
        return dtGrid;
    }

    protected void grdTerritory_Sorting(object sender, GridViewSortEventArgs e)
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
        grdTerritory.DataSource = dtGrid;
        grdTerritory.DataBind();

        foreach (GridViewRow row in grdTerritory.Rows)
        {
            //LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
            //Label lblimg = (Label)row.FindControl("lblimg");

            if (Convert.ToInt32(dtGrid.Rows[row.RowIndex][4].ToString()) > 0 || Convert.ToInt32(dtGrid.Rows[row.RowIndex][5].ToString()) > 0 || Convert.ToInt32(dtGrid.Rows[row.RowIndex][7].ToString()) > 0)
            //  if((lblListedDRCnt.Text != "0") || (lblChemistsCnt.Text != "0") || (lblUnListedDRCnt.Text != "0"))
            {
                // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                //lnkdeact.Visible = false;
                //lblimg.Visible = true;
            }
        }
    }
    protected void btnSlNo_Gen_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Territory_SlNo_Gen.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Session["sf_code"] = ddlSFCode.SelectedValue.ToString();
            ViewTerritory();
            GetHQ();

        }
        catch (Exception ex)
        {

        }
    }
    private void GetHQ()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getTerrritoryViewDIS(Session["sf_name"].ToString());
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            //lblTerrritory.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>HQ :</span>" +
            //    "<span style='font-weight: bold;color:Red'>  " + dsTerritory.Tables[0].Rows[0]["Sf_HQ"].ToString() + "</span>";

            //Session["sf_HQ"] = dsTerritory.Tables[0].Rows[0]["Sf_HQ"].ToString();
            Session["sfName"] = dsTerritory.Tables[0].Rows[0]["sfName"].ToString();
            //Session["sf_Designation_Short_Name"] = dsTerritory.Tables[0].Rows[0]["sf_Designation_Short_Name"].ToString();
        }
    }
    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    System.Threading.Thread.Sleep(time);
    //    Session["sf_code"] = null;
    //    Response.Redirect("~/BasicMaster.aspx");
    //}
    protected void ButtonDisable()
    {
        try
        {

            if (Session["sf_type"].ToString() == "1")
            {
                AdminSetup adm = new AdminSetup();
                dsSalesForce = adm.Get_Admin_FieldForce_Setup(Session["sf_code"].ToString(), div_code);
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    strAdd = dsSalesForce.Tables[0].Rows[0]["Territory_Add_Option"].ToString();
                    if (strAdd == "1")
                    {
                        btnNew.Visible = false;
                        grdTerritory.Columns[5].Visible = false;
                    }
                    strEdit = dsSalesForce.Tables[0].Rows[0]["Territory_Edit_Option"].ToString();
                    if (strEdit == "1")
                    {
                        //btnEdit.Visible = false;
                        //btnTranfer.Visible = false;
                        grdTerritory.Columns[6].Visible = false;
                    }
                    strDeact = dsSalesForce.Tables[0].Rows[0]["Territory_Deactivate_Option"].ToString();
                    if (strDeact == "1")
                    {

                        grdTerritory.Columns[7].Visible = false;
                    }


                    //strView = dsSalesForce.Tables[0].Rows[0]["Territory_View_Option"].ToString();
                    //if (strView == "1")
                    //{

                    //}
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Territory_Detail.aspx");
    }
}