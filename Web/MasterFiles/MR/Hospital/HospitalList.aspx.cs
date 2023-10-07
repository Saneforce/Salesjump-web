using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_Hospital_HospitalList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsHospital = null;
    DataSet dsChemist = null;
    Hospital hos = new Hospital();
    string div_code = string.Empty;
    string sfCode = string.Empty;
    string sCmd = string.Empty;
    string Territory_Code = string.Empty;
    string Hospital_Code = string.Empty;
    string Hospital_Name = string.Empty;
    string Hospital_Contact = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int search = 0;
    DataSet dsSalesForce = null;
    string strAdd = string.Empty;
    string strEdit = string.Empty;
    string strView = string.Empty;
    string strDeact = string.Empty;
    DataSet dsTerritory = null;
    DataSet dsListedDR = null;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sfCode = Session["sf_code"].ToString();
        }          
      
        if (!Page.IsPostBack)
        {
           
           
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
           
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            btnQAdd.Focus();
            if (Session["sf_type"].ToString() == "1")
            {
                sfCode = Session["sf_code"].ToString();
                //menu1.Visible = true;
                //menu1.Title = this.Page.Title;
                UserControl_MR_Menu Usc_MR =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;
                pnlAdmin.Visible = false;
                Usc_MR.FindControl("btnBack").Visible = false;
                //menu1.FindControl("btnBack").Visible = false;
                btnBack.Visible = false;
                FillHospital();
            }
            else
            {

                getddlSF_Code();
                pnlAdmin.Visible = true;
                UserControl_MenuUserControl Usc_Menu =
                (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(Usc_Menu);
                Usc_Menu.Title = this.Page.Title;
                btnBack.Visible = false;
                //menu1.Visible = false;
                Session["backurl"] = "HospitalList.aspx";
                //menu1.FindControl("btnBack").Visible = false;
                Usc_Menu.FindControl("btnBack").Visible = false;
                FillHospital();
            }
            Session["GetCmdArgChar"] = "All";      
            FillHospital_Alpha();
            FillTerritory();
            ButtonDisable();
            GetHQ();
            getWorkName();
        }
        else
        {
            if (Session["sf_type"].ToString() == "1")
            {
                UserControl_MR_Menu Usc_MR1 =
                        (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR1);
                Usc_MR1.Title = this.Page.Title;
                Usc_MR1.FindControl("btnBack").Visible = false;
                btnBack.Visible = false;
            }
            else
            {
                UserControl_MenuUserControl Usc_Menu =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(Usc_Menu);
                Usc_Menu.Title = this.Page.Title;
                Session["backurl"] = "HospitalList.aspx";
                Usc_Menu.FindControl("btnBack").Visible = false;
                btnBack.Visible = false;
            }
        }

    }
    private void getWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            string str = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            ddSrch.Items.Add(new ListItem(str, "3"));
        }
    }
    private void getddlSF_Code()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getSF_Code(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlSFCode.DataTextField = "Sf_Name";
            ddlSFCode.DataValueField = "Sf_Code";
            ddlSFCode.DataSource = dsTerritory;
            ddlSFCode.DataBind();

            if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
            {
                ddlSFCode.SelectedIndex = 1;
                sfCode = ddlSFCode.SelectedValue.ToString();
                Session["sf_code"] = sfCode;
            }

        }

    }
    private void GetHQ()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getTerrritoryView(sfCode);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            //lblTerrritory.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>HQ :</span>" +
            //    "<span style='font-weight: bold;color:Red'>  " + dsTerritory.Tables[0].Rows[0]["Sf_HQ"].ToString() + "</span>";

            Session["sf_HQ"] = dsTerritory.Tables[0].Rows[0]["Sf_HQ"].ToString();
            Session["sfName"] = dsTerritory.Tables[0].Rows[0]["sfName"].ToString();
            Session["sf_Designation_Short_Name"] = dsTerritory.Tables[0].Rows[0]["sf_Designation_Short_Name"].ToString();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["sf_code"] = null;
        Response.Redirect("~/BasicMaster.aspx");
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
    private void FillHospital()
    {
        Hospital hosp = new Hospital();
        for (int i = 1; i < ddlSFCode.Items.Count; i++)
        {
            sfCode = Session["sf_code"].ToString();
            if (ddlSFCode.Items[i].Value == sfCode)
            {
                ddlSFCode.SelectedIndex = i;

            }
        }
        dsHospital = hosp.getHospital(sfCode);
        if (dsHospital.Tables[0].Rows.Count > 0)
        {
            grdHospital.Visible = true;
            dlAlpha.Visible = true;
            grdHospital.DataSource = dsHospital;
            grdHospital.DataBind();
        }
        else
        {
            dlAlpha.Visible = false;
            grdHospital.DataSource = dsHospital;
            grdHospital.DataBind();
        }
    }

    protected void grdHospital_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            string Hospital_Code = Convert.ToString(e.CommandArgument);

            //Deactivate
            Hospital hosp = new Hospital();
            int iReturn = hosp.DeActivate(Hospital_Code);
            if (iReturn > 0)
            {
                //menu1.Status = "Hospital has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
                //menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillHospital();
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
        sCmd = Session["GetCmdArgChar"].ToString();
        search = Convert.ToInt32(ddSrch.SelectedValue);

        if (sCmd == "All")
        {
            dtGrid = hosp.getHospitallist_DataTable(sfCode);
        }
        else if (sCmd != "")
        {
            dtGrid = hosp.getHospitallistAlpha_DataTable(sfCode, sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dtGrid = hosp.getDTHospital_Nam(sfCode, txtsearch.Text);
        }
        else if (ddSrch.SelectedIndex != -1)
        {
            if (search == 1)
            {
                dtGrid = hosp.getHospitallist_DataTable(sfCode);
            }
            else if (search == 3)
            {
                dtGrid = hosp.getDTTerr(sfCode, ddSrc2.SelectedValue);
            }
        }
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
    protected void grdHospital_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdHospital.PageIndex = e.NewPageIndex;
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillHospital();
        }
        else if (sCmd != "")
        {
            FillHospital(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dsHospital = hos.getListedHospforName(sfCode, txtsearch.Text);
            if (dsHospital.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdHospital.Visible = true;
                grdHospital.DataSource = dsHospital;
                grdHospital.DataBind();
            }
            else
            {
                grdHospital.DataSource = dsHospital;
                grdHospital.DataBind();
                dlAlpha.Visible = false;

            }
        }
        else if (ddSrc2.SelectedIndex != -1)
        {
            Search();
        }


    }
    protected void btnQAdd_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("HospitalCreation.aspx");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEdit_Hospital.aspx");
    }
    protected void btnDeAc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Hospital_DeActivate.aspx");
    }
    private void FillHospital_Alpha()
    {
        Hospital hos = new Hospital();
        dsHospital = hos.getHospitaldet(sfCode);
        if (dsHospital.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsHospital;
            dlAlpha.DataBind();
        }
    }
    private void FillHospital(string sAlpha)
    {
        Hospital hos = new Hospital();
        dsHospital = hos.getHospitaldet_Alphabet(sfCode, sAlpha);
        if (dsHospital.Tables[0].Rows.Count > 0)
        {
            grdHospital.Visible = true;
            grdHospital.DataSource = dsHospital;
            grdHospital.DataBind();
        }
    }
    protected void grdHospital_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected DataSet FillTerritory()
    {
        Chemist Chem = new Chemist();
        dsChemist = Chem.get_Territory(sfCode);
        return dsChemist;
    }
    protected void grdHospital_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdHospital.EditIndex = -1;        
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillHospital();
        }
        else if (sCmd != "")
        {
            FillHospital(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dsHospital = hos.getListedHospforName(sfCode, txtsearch.Text);
            if (dsHospital.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdHospital.Visible = true;
                grdHospital.DataSource = dsHospital;
                grdHospital.DataBind();
            }
            else
            {
                grdHospital.DataSource = dsHospital;
                grdHospital.DataBind();
                dlAlpha.Visible = false;

            }
        }
        else if (ddSrc2.SelectedIndex != -1)
        {
            Search();
        }
    }

    protected void grdHospital_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdHospital.EditIndex = e.NewEditIndex;
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillHospital();
        }
        else if (sCmd != "")
        {
            sCmd = Session["GetCmdArgChar"].ToString();
            FillHospital(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dsHospital = hos.getListedHospforName(sfCode, txtsearch.Text);
            if (dsHospital.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdHospital.Visible = true;
                grdHospital.DataSource = dsHospital;
                grdHospital.DataBind();
            }
            else
            {
                grdHospital.DataSource = dsHospital;
                grdHospital.DataBind();
                dlAlpha.Visible = false;

            }
        }
        else if (ddSrc2.SelectedIndex != -1)
        {
            Search();
        }
        TextBox ctrl = (TextBox)grdHospital.Rows[e.NewEditIndex].Cells[2].FindControl("txtHospitalName");
        ctrl.Focus();
    }

    protected void grdHospital_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdHospital.EditIndex = -1;
        int iIndex = e.RowIndex;
        UpdateHospital(iIndex);
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillHospital();
        }
        else if (sCmd != "")
        {
            FillHospital(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dsHospital = hos.getListedHospforName(sfCode, txtsearch.Text);
            if (dsHospital.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdHospital.Visible = true;
                grdHospital.DataSource = dsHospital;
                grdHospital.DataBind();
            }
            else
            {
                grdHospital.DataSource = dsHospital;
                grdHospital.DataBind();
                dlAlpha.Visible = false;

            }
        }
        else if (ddSrc2.SelectedIndex != -1)
        {
            Search();
        }
    }
    private void UpdateHospital(int eIndex)
    {
        System.Threading.Thread.Sleep(time);
        Label lblHospital_Code = (Label)grdHospital.Rows[eIndex].Cells[1].FindControl("Hospital_Code");
        Hospital_Code = lblHospital_Code.Text; 
        TextBox txt_HospitalName = (TextBox)grdHospital.Rows[eIndex].Cells[2].FindControl("txtHospitalName");
        Hospital_Name = txt_HospitalName.Text;
        TextBox txt_Contact = (TextBox)grdHospital.Rows[eIndex].Cells[3].FindControl("txtContact");
        Hospital_Contact = txt_Contact.Text;
        DropDownList ddl_terr = (DropDownList)grdHospital.Rows[eIndex].Cells[4].FindControl("ddlterr");
        Territory_Code = ddl_terr.SelectedValue.ToString();
        Hospital hos = new Hospital();
        int iReturn = hos.RecordUpdate_Hos(Hospital_Code, Hospital_Name, Hospital_Contact, Territory_Code, Session["sf_code"].ToString());
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Hospital Name Already Exist');</script>");

        }
       
    }
    protected void grdHospital_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList Territory = (DropDownList)e.Row.FindControl("ddlterr");
            if (Territory != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Territory.SelectedIndex = Territory.Items.IndexOf(Territory.Items.FindByText(row["territory_Name"].ToString()));
            }
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
               // e.Row .Cells [4].Text =dsTerritory .Tables [0].Rows [0]["wrk_area_Name"].ToString ();
                LinkButton LnkHeaderText = e.Row.Cells[4].Controls[0] as LinkButton;
                LnkHeaderText.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        
        }
    }
    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        string sCmd = e.CommandArgument.ToString();
        Session["GetCmdArgChar"] = sCmd;

        if (sCmd == "All")
        {
            grdHospital.PageIndex = 0;
            FillHospital();
        }
        else
        {
            grdHospital.PageIndex = 0;
            FillHospital(sCmd);
        }

    }
    protected void ButtonDisable()
    {
        try
        {
            if (Session["sf_type"].ToString() == "1")
            {
                AdminSetup adm = new AdminSetup();
                dsSalesForce = adm.Get_Admin_FieldForce_Setup(Session["sf_code"].ToString(),div_code);
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    strAdd = dsSalesForce.Tables[0].Rows[0]["Class_Add_Option"].ToString();
                    if (strAdd == "1")
                    {
                        btnQAdd.Visible = false;
                    }
                    strEdit = dsSalesForce.Tables[0].Rows[0]["Class_Edit_Option"].ToString();
                    if (strEdit == "1")
                    {
                        btnEdit.Visible = false;
                        grdHospital.Columns[5].Visible = false;
                    }
                    strDeact = dsSalesForce.Tables[0].Rows[0]["Class_Deactivate_Option"].ToString();
                    if (strDeact == "1")
                    {
                        btnDeAc.Visible = false;
                        grdHospital.Columns[7].Visible = false;
                    }

                    strView = dsSalesForce.Tables[0].Rows[0]["Class_View_Option"].ToString();
                    if (strView == "1")
                    {
                        grdHospital.Columns[6].Visible = false;
                    }
                    strView = dsSalesForce.Tables[0].Rows[0]["Class_Reactivate_Option"].ToString();
                    if (strView == "1")
                    {
                        btnReAc.Visible = false;
                    }

                }
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void btnReAc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Hospital_ReActivate.aspx");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            sfCode = ddlSFCode.SelectedValue;
            Session["sf_code"] = sfCode;
            FillHospital();
            GetHQ();
            FillHospital_Alpha();

        }
        catch (Exception ex)
        {

        }
    }
    protected void ddSrch_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        search = Convert.ToInt32(ddSrch.SelectedValue);
        txtsearch.Text = string.Empty;
        grdHospital.PageIndex = 0;
        if (search == 2)
        {
            txtsearch.Visible = true;
            Btnsrc.Visible = true;
            ddSrc2.Visible = false;

        }
        else
        {
            txtsearch.Visible = false;
            Btnsrc.Visible = true;
            ddSrc2.Visible = true;
       
        }

        if (search == 1)
        {
            txtsearch.Visible = false;
            Btnsrc.Visible = false;
            ddSrc2.Visible = false;
            FillHospital();
        }

        if (search == 3)
        {
            FillTerr();
        
        }

    }

    private void FillTerr()
    {
        Hospital hsp = new Hospital();
        dsHospital = hsp.getTerritory(sfCode);
        if (dsHospital.Tables[0].Rows.Count > 0)
        {
            ddSrc2.DataTextField = "Territory_Name";
            ddSrc2.DataValueField = "Territory_Code";
            ddSrc2.DataSource = dsHospital;
            ddSrc2.DataBind();


        }
    
    }

    protected void ddSrc2_OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Btnsrc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetcmdArgChar"] = string.Empty;
        grdHospital.PageIndex = 0;
        Search();
    }

    private void Search()
    {
        Hospital hsli = new Hospital();
        search = Convert.ToInt32(ddSrch.SelectedValue);
        if (search == 1)
        {
            FillHospital();
        
        }

        if (search == 2)
        {
            dsHospital = hsli.getListedHospforName(sfCode, txtsearch.Text);
            if (dsHospital.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdHospital.Visible = true;
                grdHospital.DataSource = dsHospital;
                grdHospital.DataBind();
            }
            else
            {
                grdHospital.DataSource = dsHospital;
                grdHospital.DataBind();
                dlAlpha.Visible = false;
            
            }
        }

        if (search == 3)
        {
            dsHospital = hsli.getListedChemforTerr(sfCode, ddSrc2.SelectedValue);
            if (dsHospital.Tables[0].Rows.Count > 0)
            {

                dlAlpha.Visible = true;
                grdHospital.Visible = true;
                grdHospital.DataSource = dsHospital;
                grdHospital.DataBind();
            }
            else
            {
                grdHospital.DataSource = dsHospital;
                grdHospital.DataBind();
                dlAlpha.Visible = false;
            
            }
        
        
        }
        
    
    }
}