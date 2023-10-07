using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_ListedDoctor_ListedDrEdit : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsListedDR = null;
    bool bsrch = false;
    DataSet dsCatgType = null;
    DataSet dsTerritory = null;
    string Listed_DR_Code = string.Empty;
    string Listed_DR_Name = string.Empty;
    string Listed_DR_Address = string.Empty;
    string Listed_DR_Catg = string.Empty;
    string Listed_DR_Spec = string.Empty;
    string Listed_DR_Class = string.Empty;
    string Listed_DR_Qual = string.Empty;
    string div_code = string.Empty;
   
    string Catg_Code = string.Empty;
    string Spec_Code = string.Empty;
    string Doc_ClsCode = string.Empty;
    string Qual_Code = string.Empty; 
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    int i;
    int iCnt;
    int iReturn = -1;
    DataSet dsDR = null;
    int search = 0;
    DataSet dsDoc = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;

    DataSet dsSalesForce = null;
    string strName = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
       // sf_code = Session["sf_code"].ToString();
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
            UserControl_MenuUserControl Admin =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(Admin);
            Divid.FindControl("btnBack").Visible=false;
            Admin.Title = this.Page.Title;            
            Session["backurl"] = "LstDoctorList.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                                  "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                   "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
        }
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "LstDoctorList.aspx";
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            getWorkName();
            AdminSetup adm = new AdminSetup();

            dsSalesForce = adm.Get_Admin_FieldForce_Setup(sf_code, div_code);
            if (Session["sf_type"].ToString() == "1")
            {
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    strName = dsSalesForce.Tables[0].Rows[0]["Doc_Name_Chg"].ToString();

                    if (strName == "1")
                    {
                        ListItem approveItem = CblDoctorCode.Items.FindByValue("ListedDr_Name");
                        approveItem.Enabled = false;

                        approveItem.Attributes.Add("class", "border");


                    }
                }
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
            grdDoctor.Columns[3].HeaderText = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            ddlSrch.Items.Add(new ListItem(str, "6", true));
        //    CblDoctorCode.Items.Add(new ListItem(" " +dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString(), "Territory_Code", true));
            CblDoctorCode.Items[0].Text = " " +dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
       
        }
    }
    protected void grdListedDR_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight_clr'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    private void ShowHideTerritory()
    {
        ListedDR lstDR = new ListedDR();
        iCnt = lstDR.Single_Multi_Select_Territory(div_code);
        ViewState["ShowHideTerritory"] = iCnt.ToString();
        if (iCnt == 1)
        {
            grdDoctor.Columns[3].Visible = false;
            grdDoctor.Columns[15].Visible = true;
        }
        else
        {
            grdDoctor.Columns[3].Visible = true;
            grdDoctor.Columns[15].Visible = false;
        }
    }
    
    private void FillDoctor()
    {
        ListedDR LstDoc = new ListedDR();
        dsListedDR = LstDoc.getListedDoctor(sf_code, div_code);
        
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
            btnUpdate.Visible = true;
            grdDoctor.DataSource = dsListedDR;
            grdDoctor.DataBind();            
            foreach (GridViewRow gridRow in grdDoctor.Rows)
            {
                DropDownList ddlTerr = (DropDownList)gridRow.Cells[3].FindControl("Territory_Code");
                DropDownList ddlCatg = (DropDownList)gridRow.Cells[4].FindControl("Doc_Cat_Code");
                DropDownList ddlSpec = (DropDownList)gridRow.Cells[5].FindControl("Doc_Special_Code");
                DropDownList ddlQual = (DropDownList)gridRow.Cells[6].FindControl("Doc_QuaCode");
                DropDownList ddlClass = (DropDownList)gridRow.Cells[7].FindControl("Doc_ClsCode");
               

                Label lblDoctorCode = (Label)gridRow.Cells[1].FindControl("lblDoctorCode");
                Listed_DR_Code = lblDoctorCode.Text.ToString();

                TextBox txtTerritory = (TextBox)gridRow.Cells[1].FindControl("txtTerritory");
                CheckBoxList ChkTerritory = (CheckBoxList)gridRow.FindControl("ChkTerritory");

                DataSet dsCatgType = LstDoc.getDoctor_Terr_Catg_Spec_Class_Qual(Listed_DR_Code, sf_code,div_code);
                if (dsCatgType.Tables[0].Rows.Count > 0)
                {
                    //ddlTerr.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    ddlCatg.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlSpec.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    ddlClass.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    ddlQual.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                    string value = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    string[] strStateSplit = value.Split(',');
                    foreach (string strstate in strStateSplit)
                    {
                        if (strstate != "")
                        {
                            dsDoc.Tables[0].DefaultView.RowFilter = "Territory_Code in ('" + strstate + "')";
                            DataTable dt = dsDoc.Tables[0].DefaultView.ToTable();
                            txtTerritory.Text += dt.Rows[0].ItemArray.GetValue(1).ToString() + ", ";
                        }


                        string[] strchkstate;
                        strchkstate = txtTerritory.Text.Split(',');
                        foreach (string chkst in strchkstate)
                        {
                            for (int iIndex = 0; iIndex < ChkTerritory.Items.Count; iIndex++)
                            {
                                if (chkst.Trim() == ChkTerritory.Items[iIndex].Text.Trim())
                                {
                                    ChkTerritory.Items[iIndex].Selected = true;

                                }
                            }
                        }
                    }

                }
            }
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

    protected void btnOk_Click(object sender, EventArgs e)
    {
        int count = 0;
        System.Threading.Thread.Sleep(time);
        btnSave.Visible = true;
        btnUpdate.Visible = true;
        CblDoctorCode.Enabled = false;
        for (i = 0; i < CblDoctorCode.Items.Count; i++)
        {
            if (CblDoctorCode.Items[i].Selected == true)
            {
                count += 1;
                bsrch = true;
            }

        }
        if (count > 5)
        {

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Cannot enter more than 5 field ');</script>");
            grdDoctor.Visible = false;
            btnUpdate.Visible = false;
            btnSave.Visible = false;
        }
        else
        {

            if (bsrch == true)
            {
                tblDoctor.Visible = true;

                search = Convert.ToInt32(ddlSrch.SelectedValue);
                ListedDR LstDoc = new ListedDR();

                if (search == 1)
                {
                    dsDoc = LstDoc.getListedDoctor(sf_code, div_code);
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
                else if (search == 2)
                {
                    dsDoc = LstDoc.getListedDoctorforSpl(sf_code, ddlSrc2.SelectedValue);
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
                else if (search == 3)
                {
                    dsDoc = LstDoc.getListedDoctorforCat(sf_code, ddlSrc2.SelectedValue);
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
                else if (search == 4)
                {
                    dsDoc = LstDoc.getListedDoctorforQual(sf_code, ddlSrc2.SelectedValue);
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
                else if (search == 5)
                {
                    dsDoc = LstDoc.getListedDoctorforClass(sf_code, ddlSrc2.SelectedValue);
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
                else if (search == 6)
                {
                    dsDoc = LstDoc.getListedDoctorforTerr(sf_code, ddlSrc2.SelectedValue);
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
                else if (search == 7)
                {
                    dsDoc = LstDoc.getListedDoctorforName(sf_code, txtsearch.Text);
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

                ShowHideTerritory();
                if (CblDoctorCode.Items[0].Selected == true || CblDoctorCode.Items[1].Selected == true || CblDoctorCode.Items[2].Selected == true || CblDoctorCode.Items[3].Selected == true || CblDoctorCode.Items[4].Selected == true)
                {
                    foreach (GridViewRow gridRow in grdDoctor.Rows)
                    {

                        Label lblDoctorCode = (Label)gridRow.Cells[1].FindControl("lblDoctorCode");
                        Listed_DR_Code = lblDoctorCode.Text.ToString();

                        DropDownList ddlCatg = (DropDownList)gridRow.Cells[4].FindControl("Doc_Cat_Code");
                        DropDownList ddlSpec = (DropDownList)gridRow.Cells[5].FindControl("Doc_Special_Code");
                        DropDownList ddlQual = (DropDownList)gridRow.Cells[6].FindControl("Doc_QuaCode");
                        DropDownList ddlClass = (DropDownList)gridRow.Cells[7].FindControl("Doc_ClsCode");
                        if (CblDoctorCode.Items[0].Selected == true || CblDoctorCode.Items[1].Selected == true || CblDoctorCode.Items[2].Selected == true || CblDoctorCode.Items[3].Selected == true || CblDoctorCode.Items[4].Selected == true)
                        {
                            DataSet dsCatgType = LstDoc.getDoctor_Terr_Catg_Spec_Class_Qual(Listed_DR_Code, sf_code, div_code);
                            if (dsCatgType.Tables[0].Rows.Count > 0)
                            {
                                ddlCatg.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                ddlSpec.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                ddlClass.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                ddlQual.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                            }
                            if (CblDoctorCode.Items[0].Selected == true)
                            {

                                if (ViewState["ShowHideTerritory"].ToString() != "1")
                                {

                                    DropDownList ddlTerr = (DropDownList)gridRow.Cells[3].FindControl("Territory_Code");

                                    if (dsCatgType.Tables[0].Rows.Count > 0)
                                        ddlTerr.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                }
                                else
                                {
                                    TextBox txtTerritory = (TextBox)gridRow.Cells[15].FindControl("txtTerritory");
                                    CheckBoxList ChkTerritory = (CheckBoxList)gridRow.Cells[15].FindControl("ChkTerritory");

                                    if (dsCatgType.Tables[0].Rows.Count > 0)
                                    {
                                        string value = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        string[] strStateSplit = value.Split(',');
                                        foreach (string strstate in strStateSplit)
                                        {
                                            if (strstate != "")
                                            {
                                                dsListedDR.Tables[0].DefaultView.RowFilter = "Territory_Code in ('" + strstate + "')";
                                                DataTable dt = dsListedDR.Tables[0].DefaultView.ToTable();
                                                txtTerritory.Text += dt.Rows[0].ItemArray.GetValue(1).ToString() + ", ";
                                            }


                                            string[] strchkstate;
                                            strchkstate = txtTerritory.Text.Split(',');
                                            foreach (string chkst in strchkstate)
                                            {
                                                for (int iIndex = 0; iIndex < ChkTerritory.Items.Count; iIndex++)
                                                {
                                                    if (chkst.Trim() == ChkTerritory.Items[iIndex].Text.Trim())
                                                    {
                                                        ChkTerritory.Items[iIndex].Selected = true;

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                for (i = 3; i < ((grdDoctor.Columns.Count)); i++)
                {
                    grdDoctor.Columns[i].Visible = false;
                }
                Territory terr = new Territory();
                dsTerritory = terr.getWorkAreaName(div_code);
                for (int j = 0; j < CblDoctorCode.Items.Count; j++)
                {
                    for (i = 3; i < grdDoctor.Columns.Count; i++)
                    {
                        if (CblDoctorCode.Items[j].Selected == true)
                        {
                            if (grdDoctor.Columns[i].HeaderText.Trim() == CblDoctorCode.Items[j].Text.Trim())
                            {
                                if (grdDoctor.Columns[i].HeaderText.Trim() == dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString())
                                {
                                    ShowHideTerritory();
                                }
                                else
                                {
                                    grdDoctor.Columns[i].Visible = true;
                                }
                            }
                        }

                    }
                }


            }
            else
            {              
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select atleast one field to edit');</script>");
                btnSave.Visible = false;
                btnUpdate.Visible = false;
            }
        }

    }
    protected void btnClr_Click(object sender, EventArgs e)
    {
        for (i = 0; i < CblDoctorCode.Items.Count; i++)
        {
            CblDoctorCode.Items[i].Enabled = true;
            CblDoctorCode.Items[i].Selected = false;
            AdminSetup adm = new AdminSetup();

            dsSalesForce = adm.Get_Admin_FieldForce_Setup(sf_code, div_code);

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {

                strName = dsSalesForce.Tables[0].Rows[0]["Doc_Name_Chg"].ToString();
                if (strName == "1")
                {
                    ListItem approveItem = CblDoctorCode.Items.FindByValue("ListedDr_Name");
                    approveItem.Enabled = false;

                    approveItem.Attributes.Add("class", "border");


                }
            }
        }

        grdDoctor.DataSource = null;
        grdDoctor.DataBind();
        tblDoctor.Visible = false;
        CblDoctorCode.Enabled = true;
        btnSave.Visible = false;
        btnUpdate.Visible = false;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string cntrl = string.Empty;
        string prod_code = string.Empty;
        string strTextBox = string.Empty;
        bool bSDP = false;
        string sSDP = string.Empty;

        foreach (GridViewRow gridRow in grdDoctor.Rows)
        {
            string Listed_DR_Terr = string.Empty;
            for (i = 0; i < CblDoctorCode.Items.Count; i++)
            {
                if (CblDoctorCode.Items[i].Selected == true)
                {
                    cntrl = CblDoctorCode.Items[i].Value.ToString();

                    if ((i != 0) && (i != 1) && (i != 2) && (i != 3) && (i != 4) )
                    {
                        TextBox sTextBox = (TextBox)gridRow.Cells[1].FindControl(cntrl);
                        if ((i == 6) || (i == 7))
                        {
                            if (sTextBox.Text != "")
                            {
                                string stxt = sTextBox.Text.ToString().Substring(3, 2) + "/" + sTextBox.Text.ToString().Substring(0, 2) + "/" + sTextBox.Text.ToString().Substring(6, 4);
                                Label lblDoctorCode = (Label)gridRow.Cells[1].FindControl("lblDoctorCode");
                                Listed_DR_Code = lblDoctorCode.Text.ToString();
                                strTextBox = strTextBox + CblDoctorCode.Items[i].Value + "= '" + stxt + "',";
                            }

                        }
                        else
                        {
                            Label lblDoctorCode = (Label)gridRow.Cells[1].FindControl("lblDoctorCode");
                            Listed_DR_Code = lblDoctorCode.Text.ToString();
                            strTextBox = strTextBox + CblDoctorCode.Items[i].Value + "= '" + sTextBox.Text + "',";
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {                          
                          

                            if (ViewState["ShowHideTerritory"].ToString() == "1")
                            {
                                bSDP = true;
                                //CblDoctorCode.Items[i].Value = "SDP";
                                HiddenField hdnStateId = (HiddenField)gridRow.Cells[15].FindControl("hdnTerritoryId");
                                CheckBoxList chkst = (CheckBoxList)gridRow.Cells[15].FindControl("ChkTerritory");
                                for (int k = 0; k < chkst.Items.Count; k++)
                                {
                                    if (chkst.Items[k].Selected)
                                    {
                                        if (chkst.Items[k].Text != "ALL")
                                        {
                                            Listed_DR_Terr += chkst.Items[k].Value + ",";
                                        }
                                    }
                                }

                                Label lblDoctorCode = (Label)gridRow.Cells[1].FindControl("lblDoctorCode");
                                Listed_DR_Code = lblDoctorCode.Text.ToString();
                                strTextBox = strTextBox + CblDoctorCode.Items[i].Value + "= '" + Listed_DR_Terr + "',";
                                sSDP = Listed_DR_Terr;
                                
                            }
                            else
                            {
                                DropDownList sDDL = (DropDownList)gridRow.Cells[1].FindControl(cntrl);
                                string stxt = sDDL.SelectedValue.ToString();
                                Label lblDoctorCode = (Label)gridRow.Cells[1].FindControl("lblDoctorCode");
                                Listed_DR_Code = lblDoctorCode.Text.ToString();
                                strTextBox = strTextBox + CblDoctorCode.Items[i].Value + "= '" + stxt + "',";
                                sSDP = stxt;
                            }
                        }
                        else
                        {
                            DropDownList sDDL = (DropDownList)gridRow.Cells[1].FindControl(cntrl);
                            string stxt = sDDL.SelectedValue.ToString();
                            Label lblDoctorCode = (Label)gridRow.Cells[1].FindControl("lblDoctorCode");
                            Listed_DR_Code = lblDoctorCode.Text.ToString();
                            strTextBox = strTextBox + CblDoctorCode.Items[i].Value + "= '" + stxt + "',";
                            if (CblDoctorCode.Items[i].Value.Trim() == "Doc_Special_Code")
                            {

                                strTextBox = strTextBox + "Doc_Spec_ShortName = '" + sDDL.SelectedItem.Text.ToString() + "',";

                            }
                            if (CblDoctorCode.Items[i].Value.Trim() == "Doc_Cat_Code")
                            {

                                strTextBox = strTextBox + "Doc_Cat_ShortName = '" + sDDL.SelectedItem.Text.ToString() + "',";

                            }
                            if (CblDoctorCode.Items[i].Value.Trim() == "Doc_QuaCode")
                            {

                                strTextBox = strTextBox + "Doc_Qua_Name = '" + sDDL.SelectedItem.Text.ToString() + "',";

                            }
                            if (CblDoctorCode.Items[i].Value.Trim() == "Doc_ClsCode")
                            {

                                strTextBox = strTextBox + "Doc_Class_ShortName = '" + sDDL.SelectedItem.Text.ToString() + "',";

                            }


                            sSDP = stxt;


                        }
                    }
                }
            }

            if (strTextBox.Trim().Length > 0)
            {
               
                strTextBox  = strTextBox + " LastUpdt_Date = getdate() ";
                ListedDR lstDR = new ListedDR();              
                iReturn = lstDR.BulkEdit(strTextBox, Listed_DR_Code);                   
                strTextBox = "";
            }
        }

        if (iReturn > 0)
        {
           // menu1.Status = "Listed Doctor detail(s) have been updated Successfully";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ListedDrEdit.aspx'</script>");
        }

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
    protected void ddlSrc2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        btnUpdate_Click(sender, e);
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

    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[3].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

                e.Row.Cells[15].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
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
    protected void Check()
    {
        CheckBoxList chkst = (CheckBoxList)grdDoctor.Rows[0].FindControl("ChkTerritory");
        if (chkst.Items[0].Text == "ALL" && chkst.Items[0].Selected == true)
        {
            for (int i = 0; i < chkst.Items.Count; i++)
            {

                chkst.Items[i].Selected = true;
                //chkst.Items[i].Selected = true;            

            }
        }
    }

    protected void UnCheck()
    {
        CheckBoxList chkst = (CheckBoxList)grdDoctor.Rows[0].FindControl("ChkTerritory");
        int countSelected = chkst.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        if (countSelected == 13)
        {
            for (int i = 0; i < chkst.Items.Count; i++)
            {

                chkst.Items[i].Selected = false;
                //chkst.Items[i].Selected = true; 
            }

        }

    }


   
}