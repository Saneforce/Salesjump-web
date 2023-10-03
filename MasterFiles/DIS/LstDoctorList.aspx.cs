using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_ListedDoctor_LstDoctorList : System.Web.UI.Page
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
    string cus_code = string.Empty;
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
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iCnt = -1;
    string sf_code = string.Empty;
    string TerrCode = string.Empty;
    string strAdd = string.Empty;
    string strEdit = string.Empty;
    string strDeact = string.Empty;
    string strView = string.Empty;
    string ClsSName = string.Empty;
    string QuaSName = string.Empty;
    string CatSName = string.Empty;
    string SpecSName = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["Division_Code"].ToString().Replace(",", "");
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            btnQAdd.Focus();
            if (Session["sf_type"].ToString() == "3")
            {
                UserControl_DIS_Menu c3 =
                            (UserControl_DIS_Menu)LoadControl("~/UserControl/DIS_Menu.ascx");
                Divid.Controls.Add(c3);
                getddlSF_Code();
                getddlterr();
            }
            else
            if (Session["sf_type"].ToString() == "1")
            {
                sfCode = Session["sf_code"].ToString();
                pnlAdmin.Visible = false;
                //menu1.FindControl("btnBack").Visible = false;
                UserControl_MR_Menu Usc_MR =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;
                Usc_MR.FindControl("btnBack").Visible = false;
                btnBack.Visible = false;
                //lblSelec1.Visible = true;
                //pnlselect.Visible = true;

            }
            else
            {
              
                pnlAdmin.Visible = true;               
                UserControl_MenuUserControl Usc_Menu =
                (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(Usc_Menu);
                Divid.FindControl("btnBack").Visible = false;
                Usc_Menu.Title = this.Page.Title;
                Session["backurl"] = "LstDoctorList.aspx";
                Usc_Menu.FindControl("btnBack").Visible = false;              
                getddlSF_Code();
                getddlterr();
                lblSelect.Visible = true;
                pnlselect.Visible = true;
                
            }
            Session["GetCmdArgChar"] = "All";
            FillDoc_Alpha();
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
                UserControl_DIS_Menu c3 =
                            (UserControl_DIS_Menu)LoadControl("~/UserControl/DIS_Menu.ascx");
                Divid.Controls.Add(c3);
                Session["backurl"] = "LstDoctorList.aspx";
                c3.FindControl("btnBack").Visible = false;
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
            string str = "" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
            ddlSrch.Items.Add(new ListItem(str, "6", true));
        }
    }
    private void FillDoc()
    {
       lblSelect.Visible = false;
        lblSelec1.Visible = false;   
        ListedDR LstDoc = new ListedDR();
        //for (int i = 1; i < ddlSFCode.Items.Count; i++)
        //{
        //    //sfCode = Session["sf_code"].ToString();
        //    //if (ddlSFCode.Items[i].Value == sfCode)
        //    //{
        //   sfCode=ddlSFCode.Items[i].Value.ToString();

        //   // }
        //}

        //for (int i = 1; i < ddlroutecode.Items.Count; i++)
        //{
        //    //Territory_Code = Session["RouteCode"].ToString();
        //    //if (ddlroutecode.Items[i].Value == Territory_Code)
        //    //{
        //    //    ddlroutecode.SelectedIndex = i;
        //    Territory_Code = ddlroutecode.Items[i].Value.ToString();
        //    //}
        //}
        sfCode = ddlSFCode.SelectedValue;
        Territory_Code = ddlroutecode.SelectedValue;
        dsDoc = LstDoc.getListedDr_new(sfCode,Territory_Code);
        grdDoctor.DataSource = dsDoc;
        grdDoctor.DataBind();
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
            dlAlpha.Visible = true;
        }
        else
            dlAlpha.Visible = false;
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
        ListedDR LstDoc = new ListedDR();
        sCmd = Session["GetCmdArgChar"].ToString();
        search = Convert.ToInt32(ddlSrch.SelectedValue);

        if (sCmd == "All")
        {
            dtGrid = LstDoc.getListedDoctorList_DataTable(sfCode);
        }
        else if (sCmd != "")
        {
            dtGrid = LstDoc.getDoctorlistAlphabet_Datatable(sfCode, sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dtGrid = LstDoc.getListedDrforName_Datatable(sfCode, txtsearch.Text);
        }
        else if (ddlSrch.SelectedIndex != -1)
        {
            if (search == 1)
            {
                dtGrid = LstDoc.getListedDoctorList_DataTable(sfCode);
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
                dtGrid = LstDoc.getListedDrforClass_Datatable(sfCode, ddlSrc2.SelectedValue);
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
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchCategory(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_Cat_SName";
            ddlSrc2.DataValueField = "Doc_Cat_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillSpl()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchSpeciality(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_Special_SName";
            ddlSrc2.DataValueField = "Doc_Special_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillQualification()
    {
        ListedDR lstDR = new ListedDR();
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
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchClass(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_ClsSName";
            ddlSrc2.DataValueField = "Doc_ClsCode";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillTerritory()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchTerritory(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Territory_Name";
            ddlSrc2.DataValueField = "Territory_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }

    protected void grdDoctor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDoctor.PageIndex = e.NewPageIndex;
        sCmd = Session["GetCmdArgChar"].ToString();
        ListedDR LstDoc = new ListedDR();

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
                dlAlpha.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
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
        ListedDR LstDoc = new ListedDR();
        search = Convert.ToInt32(ddlSrch.SelectedValue);

        if (search == 1)
        {

            FillDoc();
        }
        if (search == 2)
        {
            dsDoc = LstDoc.getListedDrforSpl(sfCode, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                dlAlpha.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 3)
        {

            dsDoc = LstDoc.getListedDrforCat(sfCode, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                dlAlpha.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 4)
        {
            dsDoc = LstDoc.getListedDrforQual(sfCode, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                dlAlpha.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 5)
        {
            dsDoc = LstDoc.getListedDrforClass(sfCode, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                dlAlpha.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 6)
        {

            dsDoc = LstDoc.getListedDrforTerr(sfCode, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                dlAlpha.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
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
                dlAlpha.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }

        }
    }
    protected void btnQAdd_Click(object sender, EventArgs e)
    {
       
            Session["sf_Name"] = ddlSFCode.SelectedItem.ToString();
            Session["Terr_Name"] = ddlroutecode.SelectedItem.ToString();
            Session["Sf_code"] = ddlSFCode.SelectedValue;
            Session["Terr_Code"] = ddlroutecode.SelectedValue;
            System.Threading.Thread.Sleep(time);
            Response.Redirect("ListedDr_DetailAdd.aspx");
       
       
       
    }
    protected void btnDAdd_Click(object sender, EventArgs e)
    {
      
            Session["sf_Name"] = ddlSFCode.SelectedItem.ToString();
            Session["Terr_Name"] = ddlroutecode.SelectedItem.ToString();
            Session["Sf_code"] = ddlSFCode.SelectedValue;
            Session["Terr_Code"] = ddlroutecode.SelectedValue;
            System.Threading.Thread.Sleep(time);
            Response.Redirect("ListedDRCreation1.aspx");
      
      
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ListedDrEdit.aspx");
    }
    protected void btnDeAc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ListedDrDeactivate.aspx");
    }
    protected void btnReAc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ListedDrReactivate.aspx");
    }
    protected void grdDoctor_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected void grdDoctor_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdDoctor.EditIndex = -1;
        sCmd = Session["GetCmdArgChar"].ToString();
        ListedDR LstDoc = new ListedDR();

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
        ListedDR LstDoc = new ListedDR();

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
        ListedDR LstDoc = new ListedDR();

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
        Label lblDoc_Code = (Label)grdDoctor.Rows[eIndex].Cells[1].FindControl("lblDocCode");
        doc_code = lblDoc_Code.Text;
        TextBox txt_ListedDR_Name = (TextBox)grdDoctor.Rows[eIndex].Cells[2].FindControl("txtDocName");
        Listed_DR_Name = txt_ListedDR_Name.Text;
        DropDownList ddl_Terr = (DropDownList)grdDoctor.Rows[eIndex].Cells[3].FindControl("ddlterr");
        Listed_DR_Terr = ddl_Terr.SelectedValue.ToString();
        DropDownList ddl_Class = (DropDownList)grdDoctor.Rows[eIndex].Cells[4].FindControl("ddlDocClass");
        Listed_DR_Class = ddl_Class.SelectedValue.ToString();
        ClsSName = ddl_Class.SelectedItem.Text;
        DropDownList ddl_Catg = (DropDownList)grdDoctor.Rows[eIndex].Cells[5].FindControl("ddlDocCat");
        Listed_DR_Catg = ddl_Catg.SelectedValue.ToString();
        CatSName = ddl_Catg.SelectedItem.Text;
        DropDownList ddl_Spec = (DropDownList)grdDoctor.Rows[eIndex].Cells[6].FindControl("ddlDocSpec");
        Listed_DR_Spec = ddl_Spec.SelectedValue.ToString();
        SpecSName = ddl_Spec.SelectedItem.Text;
        DropDownList ddl_Qual = (DropDownList)grdDoctor.Rows[eIndex].Cells[7].FindControl("ddlDocQua");
        Listed_DR_Qual = ddl_Qual.SelectedValue.ToString();
        QuaSName = ddl_Qual.SelectedItem.Text;

        ListedDR lstDR = new ListedDR();

        int iReturn = lstDR.RecordUpdateDoctor(Listed_DR_Name, Listed_DR_Terr, Listed_DR_Class, Listed_DR_Catg, Listed_DR_Spec, Listed_DR_Qual, doc_code, CatSName, SpecSName, ClsSName, QuaSName, Session["sf_code"].ToString());
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Customer Name Already Exist');</script>");

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

    //alpha
    private void FillDoc_Alpha()
    {
        ListedDR Lstdr = new ListedDR();
        dsListedDR = Lstdr.getDoctorlist_Alphabet(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsListedDR;
            dlAlpha.DataBind();
        }
    }
    private void FillDoc(string sAlpha)
    {
        ListedDR Lstdr = new ListedDR();
        dsListedDR = Lstdr.getDoctorlist_Alphabet(sfCode, sAlpha);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsListedDR;
            grdDoctor.DataBind();
        }
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
                Territory.SelectedIndex = Territory.Items.IndexOf(Territory.Items.FindByText(row["Territory_Name"].ToString()));
            }
            if (Qualification != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Qualification.SelectedIndex = Qualification.Items.IndexOf(Qualification.Items.FindByText(row["Doc_Qua_Name"].ToString()));
            }
            if (Catgory != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Catgory.SelectedIndex = Catgory.Items.IndexOf(Catgory.Items.FindByText(row["Doc_Cat_ShortName"].ToString()));
            }
            if (Class != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Class.SelectedIndex = Class.Items.IndexOf(Class.Items.FindByText(row["Doc_Class_ShortName"].ToString()));
            }
            if (Speciality != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Speciality.SelectedIndex = Speciality.Items.IndexOf(Speciality.Items.FindByText(row["Doc_Spec_ShortName"].ToString()));
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
           
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);        
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
              //  e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                //LinkButton LnkHeaderText = e.Row.Cells[7].Controls[0] as LinkButton;
                //LnkHeaderText.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();              
      

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
        dsTerritory = terr.getSF_Code_dis_DIS(Session["sf_name"].ToString());
       
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlSFCode.DataTextField = "stockist_Name";
            ddlSFCode.DataValueField = "Distributor_Code";
            ddlSFCode.DataSource = dsTerritory;
            ddlSFCode.DataBind();
            ddlSFCode.SelectedIndex = 1;
            //if (Session["Sf_code"] == null || Session["Sf_code"].ToString() == "admin")
            //{
            //    ddlSFCode.SelectedIndex = 1;
            //    sfCode = ddlSFCode.SelectedValue.ToString();
            //    Session["Sf_code"] = sfCode;
            //}
        }
    }
    private void getddlterr()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getSF_Code_Route(div_code, ddlSFCode.SelectedItem.Value.ToString());

       
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlroutecode.DataTextField = "Territory_Name";
            ddlroutecode.DataValueField = "Territory_Code";
            ddlroutecode.DataSource = dsTerritory;
            ddlroutecode.DataBind();
            //Session["Route"] = ddlroutecode.SelectedItem.Text;
           // Session["RouteCode"] = ddlroutecode.SelectedValue;
            //Session["Stockist_Name"] = ddlSFCode.SelectedItem.Text;
            //if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
           // {
                ddlroutecode.SelectedIndex = 1;
                //sfCode = ddlroutecode.SelectedValue.ToString();
                //Session["sf_code"] = sfCode;

            //}
        }
        lblSelect.Visible = false;
        lblSelec1.Visible = false;   
        submit_ok();
    }
    public void submit_ok()
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            sfCode = ddlSFCode.SelectedValue;
            Session["Sf_code"] = sfCode;
            FillDoc();
            GetHQ();
            FillDoc_Alpha();

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        submit_ok();
    }
    private void GetHQ()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getTerrritoryView(sfCode);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            //lblTerrritory.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>HQ :</span>" +
            //    "<span style='font-weight: bold;color:Red'>  " + dsTerritory.Tables[0].Rows[0]["Sf_HQ"].ToString() + "</span>";
          
            //Session["Stockist_Name"] = dsTerritory.Tables[0].Rows[0]["sfName"].ToString();
          
        }
    }

    protected void Alpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ddlvar = Alpha.SelectedItem.ToString();

        // string ddltext = ddlStockist.SelectedItem.ToString();
        ListedDR Lstdr = new ListedDR();
        if (ddlvar == "---ALL---")
        {
            getddlSF_Code();
        }
        else
        {
            //ddlStockist.Items.Add("---Select the Stockist---");
            dsListedDR = Lstdr.getListeddr_Alphabet(ddlvar, div_code);
        }
        if (dsListedDR != null)
        {

            // ddlStockist.Items = dsStockist["Stockist_Name"];
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
            AdminSetup adm = new AdminSetup();
            dsSalesForce = adm.Get_Admin_FieldForce_Setup(Session["sf_code"].ToString(), div_code);
            if (Session["sf_type"].ToString() == "1")
            {
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    strAdd = dsSalesForce.Tables[0].Rows[0]["ListedDr_Add_Option"].ToString();
                    if (strAdd == "1")
                    {
                        btnQAdd.Visible = false;
                        btnDAdd.Visible = false;
                        grdDoctor.Columns[11].Visible = false;
                    }
                    strEdit = dsSalesForce.Tables[0].Rows[0]["ListedDr_Edit_Option"].ToString();
                    if (strEdit == "1")
                    {
                        //btnEdit.Visible = false;
                        grdDoctor.Columns[8].Visible = false;
                        grdDoctor.Columns[10].Visible = false;
                    }
                    strDeact = dsSalesForce.Tables[0].Rows[0]["ListedDr_Deactivate_Option"].ToString();
                    if (strDeact == "1")
                    {
                        //btnDeAc.Visible = false;
                        grdDoctor.Columns[11].Visible = false;
                    }

                    strView = dsSalesForce.Tables[0].Rows[0]["ListedDr_View_Option"].ToString();
                    if (strView == "1")
                    {
                        grdDoctor.Columns[9].Visible = false;
                    }
                    strView = dsSalesForce.Tables[0].Rows[0]["ListedDr_Reactivate_Option"].ToString();
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


    protected void btnSlNoChg_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ListedDR_SlNo_Gen.aspx");
    }
    protected void btntypemap_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ListedDr_Type_Map.aspx");
    }
    protected void btnpromap_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        //Response.Redirect("Listeddr_Prod_Map.aspx");
        Response.Redirect("Listeddr_Prod_Map_New.aspx");
    }

    protected void ddlSFCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlroutecode.Visible = true;
        Territory terr = new Territory();
        dsTerritory = terr.getSF_Code_Route(div_code,ddlSFCode.SelectedItem.Value.ToString());

            ddlroutecode.DataTextField = "Territory_Name";
            ddlroutecode.DataValueField = "Territory_Code";
            ddlroutecode.DataSource = dsTerritory;
            ddlroutecode.DataBind();
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
            Session["Route"] = ddlroutecode.SelectedItem.Text;
            Session["RouteCode"] = ddlroutecode.SelectedValue;
            Session["Stockist_Name"] = ddlSFCode.SelectedItem.Text;
            ddlroutecode.SelectedIndex = 1;
            if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
            {
                ddlroutecode.SelectedIndex = 1;
                sfCode = ddlSFCode.SelectedValue.ToString();
                Session["sf_code"] = sfCode;
                
            }
        }
    }
}