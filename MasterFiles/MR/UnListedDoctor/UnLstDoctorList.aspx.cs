using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_UnListedDoctor_UnLstDoctorList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsTerritory = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    DataSet dsListedDR = null;
    DataSet dsUnListedDR = null;
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
    string Catg_Code = string.Empty;
    string Spec_Code = string.Empty;    
    string Doc_ClsCode = string.Empty;
    string Doc_QuaCode = string.Empty;
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
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
        div_code = Session["div_code"].ToString();
        //sfCode = Session["sf_code"].ToString();
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
                //menu1.Visible = true;
                //menu1.Title = this.Page.Title;
                pnlAdmin.Visible = false;
                UserControl_MR_Menu Usc_MR =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;
               //menu1.FindControl("btnBack").Visible = false;
                Usc_MR.FindControl("btnBack").Visible = false;
                btnBack.Visible = false;
                FillDoc();

            }
            else
            {              

                pnlAdmin.Visible = true;                
                UserControl_MenuUserControl Usc_Menu =
                (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(Usc_Menu);
                Session["backurl"] = "LstDoctorList.aspx";              
                Usc_Menu.FindControl("btnBack").Visible = false;
                Usc_Menu.Title = this.Page.Title;
                btnBack.Visible = false;
                getddlSF_Code();
                FillDoc();               
            }
           
            Session["GetCmdArgChar"] = "All";
            FillDoc_Alpha();
            GetHQ();
            ButtonDisable();
            getWorkName();
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;            
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
                Session["backurl"] = "UnLstDoctorList.aspx";
                Usc_Menu.FindControl("btnBack").Visible = false;
                btnBack.Visible = false;
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
    private void getWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            string str = "Doctor " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
            ddlSrch.Items.Add(new ListItem(str, "6"));
        }
    }
    private void FillDoc()
    {
        UnListedDR LstDoc = new UnListedDR();
        for (int i = 1; i < ddlSFCode.Items.Count; i++)
        {
            sfCode = Session["sf_code"].ToString();
            if (ddlSFCode.Items[i].Value == sfCode)
            {
                ddlSFCode.SelectedIndex = i;

            }
        }
        dsDoc = LstDoc.getListedDr(sfCode);
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
        UnListedDR LstDoc = new UnListedDR();
        sCmd = Session["GetCmdArgChar"].ToString();
        search = Convert.ToInt32(ddlSrch.SelectedValue);

        if (sCmd == "All")
        {
            dtGrid = LstDoc.getUnlistedDoctorList_DataTable(sfCode);
        }
        else if (sCmd != "")
        {
            //FillDoc(sCmd);
            dtGrid = LstDoc.getDoctorUnlistAlphabet_Datatable(sfCode, sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dtGrid = LstDoc.getListedDrforName_Datatable(sfCode, txtsearch.Text);
        }
        else if (ddlSrc2.SelectedIndex != -1)
        {
            if (search == 1)
            {
                dtGrid = LstDoc.getUnlistedDoctorList_DataTable(sfCode);              
            }
            else if (search == 2)
            {
                dtGrid = LstDoc.getListedDrforSpl_Datatable(sfCode, ddlSrc2.SelectedValue);
            }
            else if (search == 3)
            {
                dtGrid = LstDoc.getListedDrforCat_Datatable(sfCode, ddlSrc2.SelectedValue);
            }
            else if (search == 4)
            {
                dtGrid = LstDoc.getListedDrforQual_Datatable(sfCode, ddlSrc2.SelectedValue);
            }
            else if (search == 5)
            {
                dtGrid = LstDoc.getListedDrforClass_Datable(sfCode, ddlSrc2.SelectedValue);
            }
            else if (search == 6)
            {
                dtGrid = LstDoc.getListedDrforTerr_Datatable(sfCode, ddlSrc2.SelectedValue);
            }
        }
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

    protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
    {
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        txtsearch.Text = string.Empty;
        grdDoctor.PageIndex = 0;

        if (search == 7)
        {
            txtsearch.Visible = true;
            Btnsrc.Visible = true;
            ddlSrc2.Visible = false;
        }
        else
        {
            txtsearch.Visible = false;
            ddlSrc2.Visible = true;
            Btnsrc.Visible = true;
        }
        if (search == 1)
        {
            txtsearch.Visible = false;
            ddlSrc2.Visible = false;
            Btnsrc.Visible = false;
            FillDoc();
        }
        if (search == 2)
        {
            FillSpl();
        }
        if (search == 3)
        {
            FillCat();
        }
        if (search == 4)
        {
            FillQualification();
        }
        if (search == 5)
        {
            FillClass();
        }
        if (search == 6)
        {
            FillTerritory();
        }
    }
    private void FillCat()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchCategory(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_Cat_Name";
            ddlSrc2.DataValueField = "Doc_Cat_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillSpl()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchSpeciality(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_Special_Name";
            ddlSrc2.DataValueField = "Doc_Special_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillQualification()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchQualification(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_QuaName";
            ddlSrc2.DataValueField = "Doc_QuaCode";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }     

    }
    private void FillClass()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchClass(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_ClsName";
            ddlSrc2.DataValueField = "Doc_ClsCode";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillTerritory()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchTerritory(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Territory_Name";
            ddlSrc2.DataValueField = "Territory_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }        

    }

    protected void grdDoctor_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            string drcode = Convert.ToString(e.CommandArgument);
            //Deactivate
            UnListedDR dv = new UnListedDR();
            int iReturn = dv.DeActivate(drcode);
            if (iReturn > 0)
            {
                //menu1.Status = "UnListed Doctor has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
               // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillDoc();
        }
    }

    protected void ddlSrc2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdDoctor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDoctor.PageIndex = e.NewPageIndex;
        sCmd = Session["GetCmdArgChar"].ToString();
        UnListedDR LstDoc = new UnListedDR();        

        if (sCmd == "All")
        {            
            FillDoc();
        }
        else if (sCmd != "")
        {
            
            FillDoc(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dsDoc = LstDoc.getListedDrforName(sfCode, txtsearch.Text);
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
        else if (ddlSrc2.SelectedIndex != -1)
        {
            Search();
        }

    }
    protected void Btnsrc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetcmdArgChar"] = string.Empty;
        grdDoctor.PageIndex = 0;
        Search();
    }

    private void Search()
    {
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        UnListedDR LstDoc = new UnListedDR();

        if (search == 1)
        {
           FillDoc();
        }
        if (search == 2)
        {
            txtsearch.Text = string.Empty;
            dsDoc = LstDoc.getListedDrforSpl(sfCode, ddlSrc2.SelectedValue);
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
        if (search == 3)
        {
            txtsearch.Text = string.Empty;
            dsDoc = LstDoc.getListedDrforCat(sfCode, ddlSrc2.SelectedValue);
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
        if (search == 4)
        {
            txtsearch.Text = string.Empty;
            dsDoc = LstDoc.getListedDrforQual(sfCode, ddlSrc2.SelectedValue);
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
        if (search == 5)
        {
            txtsearch.Text = string.Empty;
            dsDoc = LstDoc.getListedDrforClass(sfCode, ddlSrc2.SelectedValue);
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
        if (search == 6)
        {
            txtsearch.Text = string.Empty;
            dsDoc = LstDoc.getListedDrforTerr(sfCode, ddlSrc2.SelectedValue);
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
        if (search == 7)
        {
            dsDoc = LstDoc.getListedDrforName(sfCode, txtsearch.Text);
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
    }
    protected void btnQAdd_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("UnListedDrCreation.aspx");
    }
    protected void btnDAdd_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("UnListedDr_DetailAdd.aspx");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("UnListedDrEdit.aspx");
    }
    protected void btnDeAc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("UnListedDrDeactivate.aspx");
    }

    protected void grdDoctor_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdDoctor.EditIndex = -1;
        sCmd = Session["GetCmdArgChar"].ToString();
        UnListedDR LstDoc = new UnListedDR();

        if (sCmd == "All")
        {
            FillDoc();
        }
        else if (sCmd != "")
        {

            FillDoc(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dsDoc = LstDoc.getListedDrforName(sfCode, txtsearch.Text);
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
        else if (ddlSrc2.SelectedIndex != -1)
        {
            Search();
        }
    }

    protected void grdDoctor_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdDoctor.EditIndex = e.NewEditIndex;
        sCmd = Session["GetCmdArgChar"].ToString();
        UnListedDR LstDoc = new UnListedDR();

        if (sCmd == "All")
        {
            FillDoc();
        }
        else if (sCmd != "")
        {

            FillDoc(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dsDoc = LstDoc.getListedDrforName(sfCode, txtsearch.Text);
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
        else if (ddlSrc2.SelectedIndex != -1)
        {
            Search();
        }
        TextBox ctrl = (TextBox)grdDoctor.Rows[e.NewEditIndex].Cells[2].FindControl("txtDocName");
        ctrl.Focus();
    }

    protected void grdDoctor_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdDoctor.EditIndex = -1;
        int iIndex = e.RowIndex;
        UpdateDoctor(iIndex);
        sCmd = Session["GetCmdArgChar"].ToString();
        UnListedDR LstDoc = new UnListedDR();

        if (sCmd == "All")
        {
            FillDoc();
        }
        else if (sCmd != "")
        {

            FillDoc(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dsDoc = LstDoc.getListedDrforName(sfCode, txtsearch.Text);
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
        else if (ddlSrc2.SelectedIndex != -1)
        {
            Search();
        }
    }
    private void UpdateDoctor(int eIndex)
    {
        System.Threading.Thread.Sleep(time);
        Label lblDoc_Code = (Label)grdDoctor.Rows[eIndex].Cells[1].FindControl("lblDocCode");
        Listed_DR_Code = lblDoc_Code.Text;
        TextBox txt_ListedDR_Name = (TextBox)grdDoctor.Rows[eIndex].Cells[2].FindControl("txtDocName");
        Listed_DR_Name = txt_ListedDR_Name.Text;
        DropDownList ddl_Terr = (DropDownList)grdDoctor.Rows[eIndex].Cells[3].FindControl("ddlterr");
        Listed_DR_Terr = ddl_Terr.SelectedValue.ToString();
        DropDownList ddl_Class = (DropDownList)grdDoctor.Rows[eIndex].Cells[4].FindControl("ddlDocClass");
        Listed_DR_Class = ddl_Class.SelectedValue.ToString();
        DropDownList ddl_Catg = (DropDownList)grdDoctor.Rows[eIndex].Cells[5].FindControl("ddlDocCat");
        Listed_DR_Catg = ddl_Catg.SelectedValue.ToString();
        DropDownList ddl_Spec = (DropDownList)grdDoctor.Rows[eIndex].Cells[6].FindControl("ddlDocSpec");
        Listed_DR_Spec = ddl_Spec.SelectedValue.ToString();
        DropDownList ddl_Qual = (DropDownList)grdDoctor.Rows[eIndex].Cells[7].FindControl("ddlDocQua");
        Listed_DR_Qual = ddl_Qual.SelectedValue.ToString();

        UnListedDR lstDR = new UnListedDR();
        int iReturn = lstDR.RecordUpdate_In(Listed_DR_Name, Listed_DR_Catg, Listed_DR_Spec, Listed_DR_Qual, Listed_DR_Class, Listed_DR_Terr, Listed_DR_Code, Session["sf_code"].ToString());
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Doctor Name Already Exist');</script>");

        }

    }
    protected DataSet Doc_Territory()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchTerritory(sfCode);
        return dsListedDR;
    }

    protected DataSet Doc_Category()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchCategory(sfCode);
        return dsListedDR;
    }
   
    protected DataSet Doc_Speciality()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchSpeciality(sfCode);
        return dsListedDR;
    }

    protected DataSet Doc_Class()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchClass(sfCode);
        return dsListedDR;
    }
    protected DataSet Doc_Qualification()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchQualification(sfCode);
        return dsListedDR;
    }
    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList Territory = (DropDownList)e.Row.FindControl("ddlterr");
            DropDownList Qualification = (DropDownList)e.Row.FindControl("ddlDocQua");
            DropDownList Catgory = (DropDownList)e.Row.FindControl("ddlDocCat");
            DropDownList Class = (DropDownList)e.Row.FindControl("ddlDocClass");
            DropDownList Speciality = (DropDownList)e.Row.FindControl("ddlDocSpec");
            if (Territory != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Territory.SelectedIndex = Territory.Items.IndexOf(Territory.Items.FindByText(row["territory_Name"].ToString()));
            }
            if (Qualification != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Qualification.SelectedIndex = Qualification.Items.IndexOf(Qualification.Items.FindByText(row["Doc_QuaName"].ToString()));
            }
            if (Catgory != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Catgory.SelectedIndex = Catgory.Items.IndexOf(Catgory.Items.FindByText(row["Doc_Cat_Name"].ToString()));
            }
            if (Class != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Class.SelectedIndex = Class.Items.IndexOf(Class.Items.FindByText(row["Doc_ClsName"].ToString()));
            }
            if (Speciality != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Speciality.SelectedIndex = Speciality.Items.IndexOf(Speciality.Items.FindByText(row["Doc_Special_Name"].ToString()));
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
                LinkButton LnkHeaderText = e.Row.Cells[7].Controls[0] as LinkButton;
                LnkHeaderText.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        }
    }

    //alpha
    private void FillDoc_Alpha()
    {
        UnListedDR Lstdr = new UnListedDR();
        dsUnListedDR = Lstdr.getDoctorUnlist_Alphabet(sfCode);
        if (dsUnListedDR.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsUnListedDR;
            dlAlpha.DataBind();
        }
    }
    private void FillDoc(string sAlpha)
    {
        UnListedDR Lstdr = new UnListedDR();
        dsUnListedDR = Lstdr.getDoctorUnlist_Alphabet(sfCode, sAlpha);
        if (dsUnListedDR.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsUnListedDR;
            grdDoctor.DataBind();
        }
    }
    protected void grdDoctor_RowCreated(object sender, GridViewRowEventArgs e)
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
        string sCmd = e.CommandArgument.ToString();
        Session["GetCmdArgChar"] = sCmd;

        if (sCmd == "All")
        {
            grdDoctor.PageIndex = 0;
            FillDoc();
        }
        else
        {
            grdDoctor.PageIndex = 0;
            FillDoc(sCmd);
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            sfCode = ddlSFCode.SelectedValue;
            Session["sf_code"] = sfCode;
            FillDoc();
            GetHQ();
            FillDoc_Alpha();

        }
        catch (Exception ex)
        {

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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["sf_code"] = null;
        Response.Redirect("~/BasicMaster.aspx");
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
                    strAdd = dsSalesForce.Tables[0].Rows[0]["NewDoctor_Entry_Option"].ToString();
                    if (strAdd == "1")
                    {
                        btnQAdd.Visible = false;
                    }
                    strEdit = dsSalesForce.Tables[0].Rows[0]["NewDoctor_Edit_Option"].ToString();
                    if (strEdit == "1")
                    {
                        btnEdit.Visible = false;
                        grdDoctor.Columns[8].Visible = false;
                        grdDoctor.Columns[10].Visible = false;
                    }
                    strDeact = dsSalesForce.Tables[0].Rows[0]["NewDoctor_Deact_Option"].ToString();
                    if (strDeact == "1")
                    {
                        btnDeAc.Visible = false;
                        grdDoctor.Columns[11].Visible = false;
                    }

                    strView = dsSalesForce.Tables[0].Rows[0]["NewDoctor_View_Option"].ToString();
                    if (strView == "1")
                    {
                        grdDoctor.Columns[9].Visible = false;
                    }
                    strView = dsSalesForce.Tables[0].Rows[0]["NewDoctor_ReAct_Option"].ToString();
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
        Response.Redirect("UnListedDrReactivate.aspx");
    } 

}