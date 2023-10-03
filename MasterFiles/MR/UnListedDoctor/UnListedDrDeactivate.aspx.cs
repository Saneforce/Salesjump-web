using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_UnListedDoctor_UnListedDrDeactivate : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    int search = 0;
    DataSet dsListedDR = null;
    DataSet dsUnListedDR = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsTerritory = null;
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
            Session["backurl"] = "UnLstDoctorList.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                           "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                            "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
        }
        if (!Page.IsPostBack)
        {
            FillDoc();
            
            Session["backurl"] = "UnLstDoctorList.aspx";
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            getWorkName();
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
            ddlSrch.Items.Add(new ListItem(str, "6", true));
        }
    }
    private void FillDoc()
    {
        UnListedDR LstDoc = new UnListedDR();
        dsDoc = LstDoc.getListedDr(sfCode);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            btnSave.Visible = true;
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
        dtGrid = LstDoc.getUnlistedDoctorList_DataTable(sfCode);
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

        if (search == 7)        {
           
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

    protected void ddlSrc2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
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
                // De-Activate Listed Doctor
                UnListedDR lstDR = new UnListedDR();
                iReturn = lstDR.DeActivate(ListedDR);
            }
            else
            {
                //menu1.Status = "Enter all the values!!";
            }
        }

        if (iReturn != -1)
        {
          //  menu1.Status = "UnListed Doctor De-Activated Successfully!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('De-Activated Successfully');</script>");
            FillDoc();
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Server.Transfer("UnLstDoctorList.aspx");
        }
        catch (Exception ex)
        {

        }
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
              //  e.Row.Cells[4].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                LinkButton LnkHeaderText = e.Row.Cells[4].Controls[0] as LinkButton;
                LnkHeaderText.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        }
    }
    protected void Btnsrc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
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
                btnSave.Visible = true;
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
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
                btnSave.Visible = true;
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
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
                btnSave.Visible = true;
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
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
                btnSave.Visible = true;
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
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
                btnSave.Visible = true;
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 7)
        {
            dsDoc = LstDoc.getListedDrforName(sfCode, txtsearch.Text);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                btnSave.Visible = true;
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
    }
}