using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_Chemist_ChemistList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsChemists = null;
    DataSet dsListedDR = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    DataSet dsChemist = null;
    int search = 0;
    string Chemists_Name = string.Empty;
    string sCmd = string.Empty;
    string Chemists_Address1 = string.Empty;
    string Chemists_Contact = string.Empty;
    string Chemists_Phone = string.Empty;
    string Chemists_Terr = string.Empty;
    string Territory_Code = string.Empty;
    string Chemists_Code = string.Empty;
    DataSet dsTerritory = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsSalesForce = null;
    string strAdd = string.Empty;
    string strEdit = string.Empty;
    string strView = string.Empty;
    string strDeact = string.Empty;
    int time;
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
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            btnQAdd.Focus();
            if (Session["sf_type"].ToString() == "1")
            {
                sfCode = Session["sf_code"].ToString();
               // menu1.Visible = true;
               // menu1.Title = this.Page.Title;
                UserControl_MR_Menu Usc_MR =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;
                pnlAdmin.Visible = false;
                Usc_MR.FindControl("btnBack").Visible = false;
               // menu1.FindControl("btnBack").Visible = false;
                btnBack.Visible = false;
                FillChemists();
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
              //  menu1.Visible = false;
                Session["backurl"] = "ChemistList.aspx";
                Usc_Menu.FindControl("btnBack").Visible = false;
             //   menu1.FindControl("btnBack").Visible = false;
                FillChemists();
            }          
            
            Session["GetCmdArgChar"] = "All";        
            FillChemists_Alpha();           
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
                Session["backurl"] = "ChemistList.aspx";
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
    private void FillChemists()
    {
        Chemist chem = new Chemist();
        for (int i = 1; i < ddlSFCode.Items.Count; i++)
        {
            sfCode = Session["sf_code"].ToString();
            if (ddlSFCode.Items[i].Value == sfCode)
            {
                ddlSFCode.SelectedIndex = i;

            }
        }

        dsChemists = chem.getChemists(sfCode);
        if (dsChemists.Tables[0].Rows.Count > 0)
        {
            dlAlpha.Visible = true;
            grdChemist.Visible = true;
            grdChemist.DataSource = dsChemists;
            grdChemist.DataBind();
        }
        else
        {
            dlAlpha.Visible = false;
            grdChemist.DataSource = dsChemists;
            grdChemist.DataBind();
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
        Chemist chem = new Chemist();
        sCmd = Session["GetCmdArgChar"].ToString();
        search = Convert.ToInt32(ddSrch.SelectedValue);
        if (sCmd == "All")
        {
            dtGrid = chem.getChemistslist_DataTable(sfCode);
        }
        else if (sCmd != "")
        {
            dtGrid = chem.getChemistsAlpha_DataTable(sfCode, sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dtGrid = chem.getDTChemist_Nam(sfCode, txtsearch.Text);
        }
        else if (ddSrch.SelectedIndex != -1)
        {
            if (search == 1)
            {
                dtGrid = chem.getChemistslist_DataTable(sfCode);
            }
            else if (search == 3)
            {
                dtGrid = chem.getDTTerr(sfCode, ddSrc2.SelectedValue);
            }

        }
        return dtGrid;       
    }
    protected void grdChemist_Sorting(object sender, GridViewSortEventArgs e)
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
        grdChemist.DataSource = sortedView;
        grdChemist.DataBind();
    }
    protected void grdChemist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            string Chemist_Code = Convert.ToString(e.CommandArgument);

            //Deactivate
            Chemist chem = new Chemist();
            int iReturn = chem.DeActivate(Chemist_Code);
            if (iReturn > 0)
            {
               // menu1.Status = "Chemists has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
               // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillChemists();
        }
    }
    protected void grdChemist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdChemist.PageIndex = e.NewPageIndex;
        sCmd = Session["GetCmdArgChar"].ToString();
        Chemist chlis = new Chemist();
        if (sCmd == "All")
        {
            FillChemists();
        }
        else if (txtsearch.Text != "")
        {
            dsChemist = chlis.getListedChemforName(sfCode, txtsearch.Text);
            if (dsChemist.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdChemist.Visible = true;
                grdChemist.DataSource = dsChemist;
                grdChemist.DataBind();
            }
            else
            {
                grdChemist.DataSource = dsChemist;
                grdChemist.DataBind();
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
        Response.Redirect("ChemistCreation.aspx");
    }
    protected void btnDAdd_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ListedDr_DetailAdd.aspx");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEdit_Chemists.aspx");
    }
    protected void btnDeAc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Chemists_DeActivate.aspx");
    }

    private void FillChemists_Alpha()
    {
        Chemist chem = new Chemist();
        dsChemist = chem.getChemist_Alphabet(sfCode);
        if (dsChemist.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsChemist;
            dlAlpha.DataBind();
        }        
    }
    private void FillChemists(string sAlpha)
    {
        Chemist chem = new Chemist();
        dsChemist = chem.getChemist_Alphabet(sfCode, sAlpha);
        if (dsChemist.Tables[0].Rows.Count > 0)
        {
            grdChemist.Visible = true;
            grdChemist.DataSource = dsChemist;
            grdChemist.DataBind();
        }
    }
    protected void grdChemist_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        sCmd = e.CommandArgument.ToString();
        Session["GetCmdArgChar"] = sCmd;

        if (sCmd == "All")
        {
            grdChemist.PageIndex = 0;
            FillChemists();
        }
        else
        {
            grdChemist.PageIndex = 0;
            FillChemists(sCmd);
        }        
    }
    protected void grdChemist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {       
        grdChemist.EditIndex = -1;
        sCmd = Session["GetCmdArgChar"].ToString();
        Chemist chlis = new Chemist();
        if (sCmd == "All")
        {
            FillChemists();
        }
        else if (sCmd != "")
        {
            FillChemists(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dsChemist = chlis.getListedChemforName(sfCode, txtsearch.Text);
            if (dsChemist.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdChemist.Visible = true;
                grdChemist.DataSource = dsChemist;
                grdChemist.DataBind();
            }
            else
            {
                grdChemist.DataSource = dsChemist;
                grdChemist.DataBind();
                dlAlpha.Visible = false;
            }
        }
        else if (ddSrc2.SelectedIndex != -1)
        {
            Search();
        }

    }

    protected void grdChemist_RowEditing(object sender, GridViewEditEventArgs e)
    {        
        grdChemist.EditIndex = e.NewEditIndex;
        sCmd = Session["GetCmdArgChar"].ToString();
        Chemist chlis = new Chemist();
        if (sCmd == "All")
        {
            FillChemists();
        }
        else if (sCmd != "")
        {
            FillChemists(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dsChemist = chlis.getListedChemforName(sfCode, txtsearch.Text);
            if (dsChemist.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdChemist.Visible = true;
                grdChemist.DataSource = dsChemist;
                grdChemist.DataBind();
            }
            else
            {
                grdChemist.DataSource = dsChemist;
                grdChemist.DataBind();
                dlAlpha.Visible = false;
            }
        }
        else if (ddSrc2.SelectedIndex != -1)
        {
            Search();
        }

        TextBox ctrl = (TextBox)grdChemist.Rows[e.NewEditIndex].Cells[2].FindControl("txtChemName");
        ctrl.Focus();
    }

    protected void grdChemist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdChemist.EditIndex = -1;
        int iIndex = e.RowIndex;
        UpdateChemist(iIndex);
        Chemist chlis = new Chemist();
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillChemists();
        }
        else if (sCmd != "")
        {
            FillChemists(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dsChemist = chlis.getListedChemforName(sfCode, txtsearch.Text);
            if (dsChemist.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdChemist.Visible = true;
                grdChemist.DataSource = dsChemist;
                grdChemist.DataBind();
            }
            else
            {
                grdChemist.DataSource = dsChemist;
                grdChemist.DataBind();
                dlAlpha.Visible = false;
            }
        }
        else if (ddSrc2.SelectedIndex != -1)
        {
            Search();
        }
    }
    private void UpdateChemist(int eIndex)
    {
        Label lbl_Chemists_Code = (Label)grdChemist.Rows[eIndex].Cells[1].FindControl("Chemists_Code");
        Chemists_Code = lbl_Chemists_Code.Text;
        TextBox txt_ChemName = (TextBox)grdChemist.Rows[eIndex].Cells[2].FindControl("txtChemName");
        Chemists_Name = txt_ChemName.Text;
        TextBox txt_Contact = (TextBox)grdChemist.Rows[eIndex].Cells[3].FindControl("txtContact");
        Chemists_Contact = txt_Contact.Text;
        DropDownList ddl_terr = (DropDownList)grdChemist.Rows[eIndex].Cells[4].FindControl("ddlterr");
        Territory_Code = ddl_terr.SelectedValue.ToString();

        Chemist chem = new Chemist();
        int iReturn = chem.RecordUpdate_Chem(Chemists_Code, Chemists_Name, Chemists_Contact, Territory_Code, Session["sf_code"].ToString());
        if (iReturn > 0)
        {       
           ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Chemist Name Already Exist');</script>");
        }
    }
    protected DataSet FillTerritory()
    {
        Chemist Chem = new Chemist();
        dsChemist = Chem.get_Territory(sfCode);
        return dsChemist;
    }
    protected void grdChemist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList Territory = (DropDownList)e.Row.FindControl("ddlterr");
            if (Territory != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Territory.SelectedIndex = Territory.Items.IndexOf(Territory.Items.FindByText(row["Territory_Name"].ToString()));
            }
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
             //   e.Row .Cells [4].Text =dsTerritory .Tables [0].Rows [0]["wrk_area_Name"].ToString ();
                LinkButton LnkHeaderText = e.Row.Cells[4].Controls[0] as LinkButton;
                LnkHeaderText.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            sfCode = ddlSFCode.SelectedValue;
            Session["sf_code"] = sfCode;
            FillChemists();
            GetHQ();
            FillChemists_Alpha();

        }
        catch (Exception ex)
        {

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
                    strAdd = dsSalesForce.Tables[0].Rows[0]["Chemist_Add_Option"].ToString();
                    if (strAdd == "1")
                    {
                        btnQAdd.Visible = false;
                    }
                    strEdit = dsSalesForce.Tables[0].Rows[0]["Chemist_Edit_Option"].ToString();
                    if (strEdit == "1")
                    {
                        btnEdit.Visible = false;
                        grdChemist.Columns[5].Visible = false;
                    }
                    strDeact = dsSalesForce.Tables[0].Rows[0]["Chemist_Deactivate_Option"].ToString();
                    if (strDeact == "1")
                    {
                        btnDeAc.Visible = false;
                        grdChemist.Columns[7].Visible = false;
                    }

                    strView = dsSalesForce.Tables[0].Rows[0]["Chemist_View_Option"].ToString();
                    if (strView == "1")
                    {
                        grdChemist.Columns[6].Visible = false;
                    }
                    strView = dsSalesForce.Tables[0].Rows[0]["Chemist_Reactivate_Option"].ToString();
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
        Response.Redirect("Chemists_ReActivate.aspx");
    }
    protected void ddSrch_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        search = Convert.ToInt32(ddSrch.SelectedValue);
        txtsearch.Text = string.Empty;
        grdChemist.PageIndex = 0;

        if (search == 2)
        {
            txtsearch.Visible = true;
            Btnsrc.Visible = true;
            ddSrc2.Visible = false;
        }
        else
        {
            txtsearch.Visible = false;
            ddSrc2.Visible = true;
            Btnsrc.Visible = true;
        }
        if (search == 1)
        {
            txtsearch.Visible = false;
            ddSrc2.Visible = false;
            Btnsrc.Visible = false;
            FillChemists();

        }
        
        if (search == 3)
        {
            FillTerr();
        }
    

    }

    private void FillTerr()
    {
        Chemist ch = new Chemist();
        dsChemist = ch.getTerritory(sfCode);
        if(dsChemist.Tables [0].Rows .Count > 0)
        {
            ddSrc2.DataTextField = "Territory_Name";
            ddSrc2.DataValueField = "Territory_Code";
            ddSrc2 .DataSource =dsChemist ;
            ddSrc2 .DataBind ();

        }
    
    }
    protected void ddSrc2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Btnsrc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetcmdArgChar"] = string.Empty;
        grdChemist.PageIndex = 0;
        Search();
    }

    private void Search()
    {
        Chemist chlis = new Chemist();
        search = Convert.ToInt32(ddSrch.SelectedValue);
        if (search == 1)
        {

            FillChemists();
        }
        if (search == 2)
        {

            dsChemist = chlis.getListedChemforName(sfCode, txtsearch.Text);
            if (dsChemist.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdChemist.Visible = true;
                grdChemist.DataSource = dsChemist;
                grdChemist.DataBind();
            }
            else
            {
                grdChemist.DataSource = dsChemist;
                grdChemist.DataBind();
                dlAlpha.Visible = false;
            }

        }
        if (search == 3)
        {

            dsChemist = chlis.getListedChemforTerr(sfCode, ddSrc2.SelectedValue);
            if (dsChemist.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdChemist.Visible = true;
                grdChemist.DataSource = dsChemist;
                grdChemist.DataBind();
            }
            else
            {
                grdChemist.DataSource = dsChemist;
                grdChemist.DataBind();
                dlAlpha.Visible = false;
            }
        }
        
    
    
    }

}