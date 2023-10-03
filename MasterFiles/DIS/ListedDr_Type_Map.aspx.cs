using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MR_ListedDoctor_ListedDr_Type_Map : System.Web.UI.Page
{
    DataSet dsListedDR = null;
    bool bsrch = false;
    DataSet dsCatgType = null;
    string sf_code = string.Empty;
    string Listed_DR_Code = string.Empty;
    string doctype = string.Empty;
    string div_code = string.Empty;
    DataSet dsDR = null;
    DataSet dsTerritory = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsDoc = null;
    int iReturn = -1;
    int time;
    int search = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
        (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            //menu1.Visible = true;
            //menu1.FindControl("btnBack").Visible = true;

            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                              "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
            btnBack.Visible = false;

        }
        else
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MenuUserControl c1 =
          (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            Divid.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            //menu1.Visible = false;
            Session["backurl"] = "LstDoctorList.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                          "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                           "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
        }
        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            Session["backurl"] = "LstDoctorList.aspx";
            Filldoc();
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

    private void Filldoc()
    {
        ListedDR LstDoc = new ListedDR();
        dsListedDR = LstDoc.getListedDr_map(sf_code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {

            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsListedDR;
            grdDoctor.DataBind();
        }
        else
        {
            grdDoctor.DataSource = dsListedDR;
            grdDoctor.DataBind();

        }
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnUpdate_Click(sender, e);
    }
    protected void ddlSrc2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
    {
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        if (search == 7)
        {
            txtsearch.Visible = true;

            ddlSrc2.Visible = false;
        }
        else
        {
            txtsearch.Visible = false;
            ddlSrc2.Visible = true;

        }

        if (search == 1)
        {
            ddlSrc2.Visible = false;
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
            FillQual();
        }
        if (search == 5)
        {
            FillCls();
        }
        if (search == 6)
        {
            FillTerr();
        }

    }
    private void FillCat()
    {
        ListedDR lstDR = new ListedDR();
        dsDR = lstDR.FetchCategory(sf_code);
        if (dsDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_Cat_SName";
            ddlSrc2.DataValueField = "Doc_Cat_Code";
            ddlSrc2.DataSource = dsDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillSpl()
    {
        ListedDR lstDR = new ListedDR();
        dsDR = lstDR.FetchSpeciality(sf_code);
        if (dsDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_Special_SName";
            ddlSrc2.DataValueField = "Doc_Special_Code";
            ddlSrc2.DataSource = dsDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillQual()
    {
        ListedDR lstDR = new ListedDR();
        dsDR = lstDR.FetchQualification(sf_code);
        if (dsDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_QuaName";
            ddlSrc2.DataValueField = "Doc_QuaCode";
            ddlSrc2.DataSource = dsDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillCls()
    {
        ListedDR lstDR = new ListedDR();
        dsDR = lstDR.FetchClass(sf_code);
        if (dsDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_ClsSName";
            ddlSrc2.DataValueField = "Doc_ClsCode";
            ddlSrc2.DataSource = dsDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillTerr()
    {
        ListedDR lstDR = new ListedDR();
        dsDR = lstDR.FetchTerritory(sf_code);
        if (dsDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Territory_Name";
            ddlSrc2.DataValueField = "Territory_Code";
            ddlSrc2.DataSource = dsDR;
            ddlSrc2.DataBind();
        }

    }
    
    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlType = (DropDownList)e.Row.FindControl("ddlType");
            if (ddlType != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByText(row["Doc_Type"].ToString()));
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

            }
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        ListedDR LstDoc = new ListedDR();
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        btnSave.Visible = true;
        btnUpdate.Visible = true;
        if (search == 1)
        {

            Filldoc();
        }
        if (search == 2)
        {
            dsDoc = LstDoc.getListedDrforSpl_Camp(sf_code, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                btnUpdate.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 3)
        {

            dsDoc = LstDoc.getListedDrforCat_Camp(sf_code, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                btnUpdate.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 4)
        {
            dsDoc = LstDoc.getListedDrforQual_Camp(sf_code, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                btnUpdate.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 5)
        {
            dsDoc = LstDoc.getListedDrforClass_Camp(sf_code, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                btnUpdate.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 6)
        {

            dsDoc = LstDoc.getListedDrforTerr_Camp(sf_code, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                btnUpdate.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 7)
        {

            dsDoc = LstDoc.getListedDrforName_Camp(sf_code, txtsearch.Text);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                btnSave.Visible = false;
                btnUpdate.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }

        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        foreach (GridViewRow gridRow in grdDoctor.Rows)
        {
            Label lbl_ListedDrcode = (Label)gridRow.Cells[1].FindControl("lblDocCode");
            Listed_DR_Code = lbl_ListedDrcode.Text.ToString();
            DropDownList ddl_DocType = (DropDownList)gridRow.Cells[2].FindControl("ddlType");
            doctype = ddl_DocType.SelectedValue.ToString();
            ListedDR lstDr = new ListedDR();
            iReturn = lstDr.Update_doctype(Listed_DR_Code, doctype);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            }

        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Server.Transfer("LstDoctorList.aspx");
        }
        catch (Exception ex)
        {

        }
    }
}