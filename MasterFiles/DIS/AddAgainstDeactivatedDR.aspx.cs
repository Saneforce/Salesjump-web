using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_ListedDoctor_AddAgainstDeactivatedDR : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsListedDR = null;
    string Listed_DR_Code = string.Empty;
    string Listed_DR_Name = string.Empty;
    string Listed_DR_Address = string.Empty;
    string Listed_DR_Catg = string.Empty;
    string Listed_DR_Spec = string.Empty;
    string Listed_DR_Class = string.Empty;
    string Listed_DR_Qual = string.Empty;
    string Listed_DR_Terr = string.Empty;
    string Catg_Code = string.Empty;
    string Spec_Code = string.Empty;
    string Doc_ClsCode = string.Empty;
    string Qual_Code = string.Empty; 
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    string dr_code = string.Empty;
    string div_code = string.Empty;
    int i;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iCnt = -1;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            if (Request.QueryString["dr_code"] != null)
            {
                dr_code = Request.QueryString["dr_code"].ToString();
            }
            //menu1.Visible = true;
            //menu1.FindControl("btnBack").Visible = true;
            UserControl_MR_Menu Usc_MR =
         (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            Usc_MR.FindControl("btnBack").Visible = false;
            btnBack.Visible = true;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                                   "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                    "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
            

        }
        else
        {
            sf_code = Session["sf_code"].ToString();
            
         //   menu1.Visible = false;
            UserControl_MenuUserControl Usc_Menu =
          (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            if (Request.QueryString["dr_code"] != null)
            {
                dr_code = Request.QueryString["dr_code"].ToString();
            }
            Divid.Controls.Add(Usc_Menu);
            Usc_Menu.FindControl("btnBack").Visible = false;
            Usc_Menu.Title = this.Page.Title;
            btnBack.Visible = true;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                         "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                          "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
        }
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
           
          //  menu1.Title = this.Page.Title;
            if (Request.QueryString["dr_code"] != null)
            {
                dr_code = Request.QueryString["dr_code"].ToString();
            }
            FillListedDR();
            FillOrgDR();
        }
        ShowHideTerritory();
    }

    private void ShowHideTerritory()
    {
        ListedDR lstDR = new ListedDR();
        iCnt = lstDR.Single_Multi_Select_Territory(div_code);
        ViewState["ShowHideTerritory"] = iCnt.ToString();
        if (iCnt == 1)
        {
            grdListedDR.Columns[6].Visible = false;
            grdListedDR.Columns[7].Visible = true;
        }
        else
        {
            grdListedDR.Columns[6].Visible = true;
            grdListedDR.Columns[7].Visible = false;
        }
    }
    protected void ChkTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        string name1 = "";
        string id1 = "";
        GridViewRow gv1 = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList chkst = (CheckBoxList)gv1.FindControl("ChkTerritory");
        TextBox txtstate = (TextBox)gv1.FindControl("txtTerritory");
        HiddenField hdnStateId = (HiddenField)gv1.FindControl("hdnTerritoryId");
        txtstate.Text = "";
        hdnStateId.Value = "";

        if (chkst.Items[0].Text == "ALL" && chkst.Items[0].Selected == true)
        {
            for (int i = 0; i < chkst.Items.Count; i++)
            {
                chkst.Items[i].Selected = true;
            }
        }

        int countSelected = chkst.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        if (countSelected == chkst.Items.Count - 1)
        {
            for (int i = 0; i < chkst.Items.Count; i++)
            {

                chkst.Items[i].Selected = false;
            }

        }

        for (int i = 0; i < chkst.Items.Count; i++)
        {
            if (chkst.Items[i].Selected)
            {
                if (chkst.Items[i].Text != "ALL")
                {
                    name1 += chkst.Items[i].Text + ",";
                    id1 += chkst.Items[i].Value + ",";
                }
            }
        }

        if (name1 == "")
        {
            name1 = "----Select----";
        }

        txtstate.Text = name1.TrimEnd(',');
        hdnStateId.Value = id1.TrimEnd(',');
        //chkst.Attributes.Add("onclick", "checkAll(this);");


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
    protected void grdListedDR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[6].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                //LinkButton LnkHeaderText = e.Row.Cells[7].Controls[0] as LinkButton;
                //LnkHeaderText.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();             
            }
           
            
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBoxList chkst = (CheckBoxList)e.Row.FindControl("ChkTerritory");
            TextBox txtstate = (TextBox)e.Row.FindControl("txtTerritory");
            HiddenField hdnStateId = (HiddenField)e.Row.FindControl("hdnTerritoryId");

            ListedDR lstDR = new ListedDR();
            dsListedDR = lstDR.FetchTerritory(sf_code);

            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                chkst.DataTextField = "Territory_Name";
                chkst.DataValueField = "Territory_Code";
                chkst.DataSource = dsListedDR;
                chkst.DataBind();
            }
        }
    }
    private void FillListedDR()
    {
        ListedDR lstDR = new ListedDR();
        //dsListedDR = lstDR.getListedDr(sf_code, dr_code);
        dsListedDR = lstDR.getTopListedDR();
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            grdListedDR.Visible = true;
            grdListedDR.DataSource = dsListedDR;
            grdListedDR.DataBind();
        }
    }

    private void FillOrgDR()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.getListedDr(sf_code, dr_code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            grdOrgDR.Visible = true;
            grdOrgDR.DataSource = dsListedDR;
            grdOrgDR.DataBind();
        }
    }

    protected DataSet FillTerritory()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchTerritory(sf_code);
        return dsListedDR;
    }

    protected DataSet FillCategory()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchCategory(sf_code);
        return dsListedDR;
    }

    protected DataSet FillSpeciality()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchSpeciality(sf_code);
        return dsListedDR;
    }

    protected DataSet FillClass()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchClass(sf_code);
        return dsListedDR;
    }
    protected DataSet FillQualification()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchQualification(sf_code);
        return dsListedDR;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        int slvno = -1;
        foreach (GridViewRow gridRow in grdListedDR.Rows)
        {
            TextBox txt_ListedDR_Name = (TextBox)gridRow.Cells[0].FindControl("ListedDR_Name");
            Listed_DR_Name = txt_ListedDR_Name.Text.ToString();
            TextBox txt_ListedDR_Address1 = (TextBox)gridRow.Cells[1].FindControl("ListedDR_Address1");
            Listed_DR_Address = txt_ListedDR_Address1.Text.ToString();
            DropDownList ddl_Catg = (DropDownList)gridRow.Cells[2].FindControl("ddlCatg");
            Listed_DR_Catg = ddl_Catg.SelectedValue.ToString();
            string Cat_SName = ddl_Catg.SelectedItem.Text;
            DropDownList ddl_Spec = (DropDownList)gridRow.Cells[3].FindControl("ddlspcl");            
            Listed_DR_Spec = ddl_Spec.SelectedValue.ToString();
            string Spec_SName = ddl_Spec.SelectedItem.Text;
            DropDownList ddl_Qual = (DropDownList)gridRow.Cells[4].FindControl("ddlQual");
            Listed_DR_Qual = ddl_Qual.SelectedValue.ToString();
            string Qual_SName = ddl_Qual.SelectedItem.Text;
            DropDownList ddl_Class = (DropDownList)gridRow.Cells[5].FindControl("ddlClass");
            Listed_DR_Class = ddl_Class.SelectedValue.ToString();
            string Cls_SName = ddl_Class.SelectedItem.Text;


            if (ViewState["ShowHideTerritory"].ToString() == "1")
            {
                HiddenField hdnStateId = (HiddenField)gridRow.Cells[7].FindControl("hdnTerritoryId");
                CheckBoxList chkst = (CheckBoxList)gridRow.Cells[7].FindControl("ChkTerritory");
                for (int i = 0; i < chkst.Items.Count; i++)
                {
                    if (chkst.Items[i].Selected)
                    {
                        if (chkst.Items[i].Text != "ALL")
                        {
                            Listed_DR_Terr += chkst.Items[i].Value + ",";
                        }
                    }
                }

            }
            else
            {
                DropDownList ddl_Terr = (DropDownList)gridRow.Cells[6].FindControl("ddlTerr");
                Listed_DR_Terr = ddl_Terr.SelectedValue.ToString();
            }

            if ((Listed_DR_Name.Trim().Length > 0) && (Listed_DR_Address.Trim().Length > 0) && (Listed_DR_Catg.Trim().Length > 0) && (Listed_DR_Spec.Trim().Length > 0) && (Listed_DR_Qual.Trim().Length > 0) && (Listed_DR_Class.Trim().Length > 0) && (Listed_DR_Terr.Trim().Length > 0))
            {
                ListedDR lstDR = new ListedDR();
                //DeActivate Doctor
                iReturn = lstDR.DeActivate(dr_code);
                if (iReturn != -1)
                {
                    //Get SLVNo for that doctor
                    slvno = lstDR.AddVsDeActivate(dr_code);
                 
                    // Add New Listed Doctor
                    iReturn = lstDR.RecordAdd(Listed_DR_Name, Listed_DR_Address, Listed_DR_Catg, Listed_DR_Spec, Listed_DR_Qual, Listed_DR_Class, Listed_DR_Terr, Session["sf_code"].ToString(),slvno, Cat_SName, Spec_SName, Cls_SName, Qual_SName);
                }
            }
            else
            {
                //menu1.Status = "Enter all the values!!";
            }
        }

        if (iReturn != -1)
        {
           // menu1.Status = "Listed Doctor Created Successfully!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
            FillListedDR();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        FillListedDR();
    }
  

}