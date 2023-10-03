using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.IO;
using System.Drawing;

public partial class MasterFiles_MR_Territory : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsTerritory = null;
    DataSet dsListedDR = null;
    DataTable dsTerritory1 = null;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string Territory_Code = string.Empty;
    string Territory_Type = string.Empty;
    string Territory_Name = string.Empty;
    string code = string.Empty;
    string strSF_Index = string.Empty;
    string Territory_SName = string.Empty;
    string min = string.Empty;
    string population = string.Empty;
    int i;
    string sCmd = string.Empty;
    string div_code;
    string div_code1;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsSalesForce = null;
    string strAdd = string.Empty;
    string strEdit = string.Empty;
    string strView = string.Empty;
    string strDeact = string.Empty;
    string sf_code_MR = string.Empty;
    #endregion
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

        try
        {
            div_code = Session["div_code"].ToString();
            div_code1 = Session["Division_Code"].ToString();
            sf_code = Session["sf_code"].ToString();
        }
        catch
        {
            div_code = Session["Division_Code"].ToString();
        }

        //Session["Div_code"] = div_code.ToString();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            btnNew.Focus();
            if (Session["sf_type"].ToString() == "1")
            {
                sf_code = Session["sf_code_MR"].ToString();
                //menu1.Visible = true;
                //UserControl_MR_Menu Usc_MR =
                //(UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                //Divid.Controls.Add(Usc_MR);
                Territory terr = new Territory();
                dsTerritory = terr.getSF_Code_distributor_code(sf_code, div_code);
                if (dsTerritory.Tables[0].Rows.Count > 0)
                {

                    ddlSFCode.DataTextField = "stockist_Name";
                    ddlSFCode.DataValueField = "Stockist_Code";
                    ddlSFCode.DataSource = dsTerritory;
                    ddlSFCode.DataBind();

                    //if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
                    //{
                    //    ddlSFCode.SelectedIndex = 1;
                    //    Session["sf_code"] = ddlSFCode.SelectedValue;
                    //}
                    ViewTerritory();

                }

                else
                {
                    ddlSFCode.SelectedIndex = 0;
                }

            }
            else
            {
                fillsubdivision();

                Alpha.Visible = false;
                salesforcelist.Visible = false;
                ddlSFCode.Visible = false;
                pnlAdmin.Visible = true;

                ViewTerritory();

            }

            GetHQ();

            ButtonDisable();
            FillSF_Alpha();
        }
        else
        {

        }
    }
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    private void fillsalesforce()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.feildforceelist_SF(div_code, subdiv.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            salesforcelist.DataTextField = "Sf_Name";
            salesforcelist.DataValueField = "Sf_Code";
            salesforcelist.DataSource = dsSalesForce;
            salesforcelist.DataBind();
            salesforcelist.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    private void FillReporting()
    {

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_getMR(div_code, sf_code, subdiv.SelectedValue.ToString(), Territory.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFilter.DataTextField = "Sf_Name";
            ddlFilter.DataValueField = "Sf_Code";
            ddlFilter.DataSource = dsSalesForce;
            ddlFilter.DataBind();
            ddlFilter.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            ddlFilter.Items.Clear();
            ddlFilter.DataSource = null;
            ddlFilter.DataBind();
            ddlFilter.Items.Insert(0, new ListItem("--Select--", "0"));
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

    protected void btnallowance_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Allowance_Types_Updation.aspx?TerrHq=" + Territory.SelectedValue.ToString() + "&TerrHqName=" + Territory.SelectedItem.ToString() + "");
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
            fillsalesforce();
        }
        else
        {
            dsListedDR = Lstdr.getListeddr_Alphabet(ddlvar, div_code1);
        }
        if (dsListedDR != null)
        {
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {

                salesforcelist.Visible = true;
                salesforcelist.DataSource = dsListedDR;
                salesforcelist.DataBind();
            }
        }

    }

    private void ViewTerritory()
    {
        //   Territory terr = new Territory();
        for (int i = 1; i < ddlSFCode.Items.Count; i++)
        {
            string str = Session["sf_code"].ToString();
            if (ddlSFCode.Items[i].Value == str)
            {
                ddlSFCode.SelectedIndex = i;

            }
        }
        //
        //  dsTerritory = terr.getTerritorydiv(Session["sf_code"].ToString(), div_code);
        //  if (dsTerritory.Tables[0].Rows.Count > 0)
        //  {
        //      grdTerritory.Visible = true;
        //      grdTerritory.DataSource = dsTerritory;
        //      grdTerritory.DataBind();
        //      foreach (GridViewRow row in grdTerritory.Rows)
        //      {
        //
        //          LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
        //          Label lblimg = (Label)row.FindControl("lblimg");
        //         Label lblListedDRCnt = (Label)row.FindControl("lblListedDRCnt");
        //         // Label lblChemistsCnt = (Label)row.FindControl("lblChemistsCnt");
        //          if ((lblListedDRCnt.Text != "0" ))
        //          {
        //             lnkdeact.Visible = false;
        //              lblimg.Visible = true;
        //          }


        //      }
        //  }
        //  else
        //  {
        //      grdTerritory.DataSource = dsTerritory;
        //      grdTerritory.DataBind();
        //  }



        Territory terr = new Territory();
        dsTerritory = terr.getTerritorySF(Territory.SelectedValue.ToString(), div_code, subdiv.SelectedValue.ToString());
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            grdTerritory.Visible = true;
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
            foreach (GridViewRow row in grdTerritory.Rows)
            {
                Label lblListedDRCnt = (Label)row.FindControl("lblListedDRCnt");
                Label lblChemistsCnt = (Label)row.FindControl("lblChemistsCnt");


                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");


                if ((lblListedDRCnt.Text != "0"))
                {
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
            //  ViewTerritory();
        }
        else
        {
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
            btnAdd.Enabled = true;
            btnNew.Enabled = true;
        }


    }

    private void getddlSF_Code()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.GetDistNamewise(div_code, salesforcelist.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlSFCode.DataTextField = "stockist_Name";
            ddlSFCode.DataValueField = "Stockist_Code";
            ddlSFCode.DataSource = dsSalesForce;
            ddlSFCode.DataBind();

            if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
            {
                ddlSFCode.SelectedIndex = 0;
                //Session["sf_code"] = ddlSFCode.SelectedValue;
                //Session["sf_Name"] = ddlSFCode.SelectedItem.ToString();
            }

        }
        else
        {
            ddlSFCode.SelectedIndex = 0;
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

        if (ddlSFCode.SelectedItem.ToString() == "---ALL---")
        {
            grdTerritory.EditIndex = e.NewEditIndex;
            //Fill the Division Grid
            btnSubmit_Click(sender, e);
            //Setting the focus to the textbox "Division Name"        
            TextBox ctrl = (TextBox)grdTerritory.Rows[e.NewEditIndex].Cells[2].FindControl("txtTerritory_Name");
            ctrl.Focus();


        }
        else
        {
            grdTerritory.EditIndex = e.NewEditIndex;
            //Fill the Division Grid
            ViewTerritory();
            //Setting the focus to the textbox "Division Name"        
            TextBox ctrl = (TextBox)grdTerritory.Rows[e.NewEditIndex].Cells[2].FindControl("txtTerritory_Name");
            ctrl.Focus();
        }
        //This will get invoke when the user clicks "Inline Edit" link

    }

    protected void grdTerritory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (ddlSFCode.SelectedItem.ToString() == "---ALL---")
        {
            grdTerritory.EditIndex = -1;
            int iIndex = e.RowIndex;
            UpdateTerritory(iIndex);
            btnSubmit_Click(sender, e);
        }
        else
        {
            grdTerritory.EditIndex = -1;
            int iIndex = e.RowIndex;
            UpdateTerritory(iIndex);
            ViewTerritory();
        }

    }

    protected void grdTerritory_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if ((e.Row.RowState & DataControlRowState.Edit) > 0)
        {


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
        sCmd = Session["GetCmdArgChar"].ToString();
        // if (ddlFilter.SelectedItem.ToString() == "--Select--")
        ////if (ddlSFCode.SelectedItem.ToString() == "---ALL---")
        //  {
        //      grdTerritory.PageIndex = e.NewPageIndex;
        //      btnSubmit_Click(sender, e);
        //  }
        //  else
        //  {


        //      btnGo_Click(sender,e);
        //      if (sCmd == "All")
        //      {
        //          FillSalesForce();
        //      }
        //      else
        //      {
        //          FillSalesForce(sCmd);
        //      }

        //  }


        if (ddlFilter.SelectedIndex > 0 )
        {
            FillSalesForce_Reporting();
        }
        else if (sCmd == "All")
        {
            FillSalesForce();
        }

        else if (sCmd != "")
        {
            FillSalesForce(sCmd);
        }
        

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
        TextBox txt_Territory_Population = (TextBox)grdTerritory.Rows[eIndex].Cells[7].FindControl("txtRoutePopulation");
        population = txt_Territory_Population.Text;
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
        btnAdd.Enabled = true;
        btnNew.Enabled = true;
        FillReporting();
        lblFilter.Visible = true;
        ddlFilter.Visible = true;
        Button1.Visible = true;
        System.Threading.Thread.Sleep(time);
        try
        {


            Territory terr = new Territory();
            dsTerritory = terr.getTerritorySF(Territory.SelectedValue.ToString(), div_code, subdiv.SelectedValue.ToString());
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {

                grdTerritory.Visible = true;
                grdTerritory.DataSource = dsTerritory;
                grdTerritory.DataBind();
                foreach (GridViewRow row in grdTerritory.Rows)
                {

                    Label lblListedDRCnt = (Label)row.FindControl("lblListedDRCnt");
                    Label lblChemistsCnt = (Label)row.FindControl("lblChemistsCnt");

                    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                    Label lblimg = (Label)row.FindControl("lblimg");


                    if ((lblListedDRCnt.Text != "0"))
                    {
                        lnkdeact.Visible = false;
                        lblimg.Visible = true;
                    }


                }
                FillSF_Alpha();
                Session["GetCmdArgChar"] = "All";
            }
            else
            {
                grdTerritory.DataSource = dsTerritory;
                grdTerritory.DataBind();
                btnAdd.Enabled = true;
                btnNew.Enabled = true;
                lblFilter.Visible = true;
                ddlFilter.Visible = true;
                Button1.Visible = true;
            }


        }
        catch (Exception ex)
        {

        }
    }
    private void GetHQ()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getTerrritoryView(Session["sf_code"].ToString());
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            //lblTerrritory.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>HQ :</span>" +
            //    "<span style='font-weight: bold;color:Red'>  " + dsTerritory.Tables[0].Rows[0]["Sf_HQ"].ToString() + "</span>";

            //Session["sf_HQ"] = dsTerritory.Tables[0].Rows[0]["Sf_HQ"].ToString();
            Session["sfName"] = dsTerritory.Tables[0].Rows[0]["sfName"].ToString();
            //Session["sf_Designation_Short_Name"] = dsTerritory.Tables[0].Rows[0]["sf_Designation_Short_Name"].ToString();
        }
    }

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

                        grdTerritory.Columns[6].Visible = false;
                    }
                    strDeact = dsSalesForce.Tables[0].Rows[0]["Territory_Deactivate_Option"].ToString();
                    if (strDeact == "1")
                    {

                        grdTerritory.Columns[7].Visible = false;
                    }



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
        Context.Items.Add("sf_code", ddlSFCode.SelectedValue.ToString());
        Session["Terr_code_ms"] = Territory.SelectedValue.ToString();
        Server.Transfer("Territory_Detail.aspx");
    }
    protected void grdTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        DataTable dsProd1 = null;
        Territory LstDoc = new Territory();
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Route.xls"));
            Response.ContentType = "application/ms-excel";
            dsProd1 = LstDoc.getTerritorydiv1_Ex(div_code);
            DataTable dt = dsProd1;
            string str = string.Empty;
            foreach (DataColumn dtcol in dt.Columns)
            {
                Response.Write(str + dtcol.ColumnName);
                str = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in dt.Rows)
            {
                str = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Response.Write(str + Convert.ToString(dr[j]));
                    str = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void salesforcelist_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlSFCode.Visible = true;
        getddlSF_Code();
    }
    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Alpha.Visible = true;
        //salesforcelist.Visible = true;
        //fillsalesforce();



        Territorys dv = new Territorys();
        // dsTerritory = dv.TerritorygetSubDiv(div_code);
        dsTerritory = dv.TerritorygetSF_Code(div_code, sf_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            Territory.DataSource = dsTerritory;
            Territory.DataTextField = "Territory_name";
            Territory.DataValueField = "Territory_code";
            Territory.DataBind();

        }
    }
    protected void grdTerritory_RowCommand1(object sender, GridViewCommandEventArgs e)
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


    private void FillSF_Alpha()
    {
        Territory terr = new Territory();
        dsSalesForce = terr.getTerritory_Alphabet_List(div_code, Territory.SelectedValue.ToString(), subdiv.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsSalesForce;
            dlAlpha.DataBind();
        }
    }


    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        string sCmd = e.CommandArgument.ToString();
        Session["GetCmdArgChar"] = sCmd;

        if (sCmd == "All")
        {
            grdTerritory.PageIndex = 0;
            FillSalesForce();
        }
        else
        {
            grdTerritory.PageIndex = 0;
            FillSalesForce(sCmd);
        }
    }

    private void FillSalesForce()
    {
        try
        {
            Territory terr = new Territory();
            dsTerritory = terr.getTerritorySF(Territory.SelectedValue.ToString(), div_code, subdiv.SelectedValue.ToString(),"", ddlFilter.SelectedValue.ToString());
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                grdTerritory.Visible = true;
                grdTerritory.DataSource = dsTerritory;
                grdTerritory.DataBind();
                foreach (GridViewRow row in grdTerritory.Rows)
                {
                    Label lblListedDRCnt = (Label)row.FindControl("lblListedDRCnt");
                    Label lblChemistsCnt = (Label)row.FindControl("lblChemistsCnt");


                    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                    Label lblimg = (Label)row.FindControl("lblimg");


                    if ((lblListedDRCnt.Text != "0"))
                    {
                        lnkdeact.Visible = false;
                        lblimg.Visible = true;
                    }
                }
            }
            else
            {
                grdTerritory.DataSource = dsTerritory;
                grdTerritory.DataBind();
                btnAdd.Enabled = true;
                btnNew.Enabled = true;
            }


        }
        catch (Exception ex)
        {

        }
    }

    private void FillSalesForce(string sAlpha)
    {
        try
        {
            Territory terr = new Territory();
            dsTerritory = terr.getTerritorySF(Territory.SelectedValue.ToString(), div_code, subdiv.SelectedValue.ToString(), sAlpha);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                grdTerritory.Visible = true;
                grdTerritory.DataSource = dsTerritory;
                grdTerritory.DataBind();
                foreach (GridViewRow row in grdTerritory.Rows)
                {
                    Label lblListedDRCnt = (Label)row.FindControl("lblListedDRCnt");
                    Label lblChemistsCnt = (Label)row.FindControl("lblChemistsCnt");


                    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                    Label lblimg = (Label)row.FindControl("lblimg");


                    if ((lblListedDRCnt.Text != "0"))
                    {
                        lnkdeact.Visible = false;
                        lblimg.Visible = true;
                    }
                }
            }
            else
            {
                grdTerritory.DataSource = dsTerritory;
                grdTerritory.DataBind();
                btnAdd.Enabled = true;
                btnNew.Enabled = true;
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        if (ddlFilter.SelectedItem.ToString() == "")
        {

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Any One');</script>");
        }
        else
        {
            if (ddlFilter.SelectedIndex > 0)
            {
                //grdTerritory.PageIndex = 0;
                FillSalesForce_Reporting();
                //txtsearch.Text = string.Empty;
                //Session["GetCmdArgChar"] = string.Empty;
                //if (ddlSrc.SelectedIndex != -1)
                //{
                // ddlSrc.SelectedIndex = 0;
                //}
            }
            else
            {
                btnSubmit_Click(sender, e);
            }
        }
    }

    private void FillSalesForce_Reporting()
    {
        string sReport = ddlFilter.SelectedValue.ToString();

        btnAdd.Enabled = true;
        btnNew.Enabled = true;
        System.Threading.Thread.Sleep(time);
        try
        {


            Territory terr = new Territory();
            dsTerritory = terr.getTerritorySF(Territory.SelectedValue.ToString(), div_code, subdiv.SelectedValue.ToString(), "", sReport);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                grdTerritory.Visible = true;
                grdTerritory.DataSource = dsTerritory;
                grdTerritory.DataBind();
                foreach (GridViewRow row in grdTerritory.Rows)
                {

                    Label lblListedDRCnt = (Label)row.FindControl("lblListedDRCnt");
                    Label lblChemistsCnt = (Label)row.FindControl("lblChemistsCnt");

                    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                    Label lblimg = (Label)row.FindControl("lblimg");


                    if ((lblListedDRCnt.Text != "0"))
                    {
                        lnkdeact.Visible = false;
                        lblimg.Visible = true;
                    }


                }
                FillSF_Alpha();
                Session["GetCmdArgChar"] = "All";
            }
            else
            {
                grdTerritory.DataSource = dsTerritory;
                grdTerritory.DataBind();
                btnAdd.Enabled = true;
                btnNew.Enabled = true;
            }


        }
        catch (Exception ex)
        {

        }



    }

}


// FillReporting();