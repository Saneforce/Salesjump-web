using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_UnListedDoctor_UnListedDrEdit : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsListedDR = null;
    DataSet dsTerritory = null;
    bool bsrch = false;
    DataSet dsCatgType = null;
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
    string Doc_QuaCode = string.Empty;
    string Terr_Code = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    int i;
    int iReturn = -1;
    DataSet dsDR = null;
    int search = 0;
    DataSet dsDoc = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sf_type"].ToString() == "1")
        {
            div_code = Session["div_code"].ToString();
            sf_code = Session["sf_code"].ToString();
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
            sf_code = Session["sf_code"].ToString();
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
            ddlSrch.Items.Add(new ListItem(str, "6"));
        }

    }
    private void FillDoctor()
    {
        UnListedDR LstDoc = new UnListedDR();
        dsListedDR = LstDoc.getListedDoctor(sf_code);

        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
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

                DataSet dsCatgType = LstDoc.getDoctor_Terr_Catg_Spec_Class_Qual(Listed_DR_Code, sf_code);
                if (dsCatgType.Tables[0].Rows.Count > 0)
                {
                    ddlTerr.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    ddlCatg.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlSpec.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    ddlClass.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    ddlQual.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                }
            }
        }
    }

    protected DataSet FillTerritory()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchTerritory(sf_code);
        return dsListedDR;
    }

    protected DataSet FillCategory()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchCategory(sf_code);
        return dsListedDR;
    }

    protected DataSet FillSpeciality()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchSpeciality(sf_code);
        return dsListedDR;
    }

    protected DataSet FillClass()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchClass(sf_code);
        return dsListedDR;
    }

    protected DataSet FillQualification()
    {
        UnListedDR lstDR = new UnListedDR();
        dsListedDR = lstDR.FetchQualification(sf_code);
        return dsListedDR;
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int count = 0;
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

        if (count > 4)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not Select More than 4 Field');</script>");
            btnUpdate.Visible = false;
            btnSave.Visible = false;
        }

        else
        {
            if (bsrch == true)
            {
                tblDoctor.Visible = true;

                search = Convert.ToInt32(ddlSrch.SelectedValue);
                UnListedDR LstDoc = new UnListedDR();
                if (search == 1)
                {
                    dsDoc = LstDoc.getListedDoctor(sf_code);
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
                if (search == 2)
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
                if (search == 3)
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
                if (search == 4)
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
                if (search == 5)
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
                if (search == 6)
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
                if (search == 7)
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
                foreach (GridViewRow gridRow in grdDoctor.Rows)
                {
                    DropDownList ddlTerr = (DropDownList)gridRow.Cells[3].FindControl("Territory_Code");
                    DropDownList ddlCatg = (DropDownList)gridRow.Cells[4].FindControl("Doc_Cat_Code");
                    DropDownList ddlSpec = (DropDownList)gridRow.Cells[5].FindControl("Doc_Special_Code");
                    DropDownList ddlQual = (DropDownList)gridRow.Cells[6].FindControl("Doc_QuaCode");
                    DropDownList ddlClass = (DropDownList)gridRow.Cells[7].FindControl("Doc_ClsCode");

                    Label lblDoctorCode = (Label)gridRow.Cells[1].FindControl("lblDoctorCode");
                    Listed_DR_Code = lblDoctorCode.Text.ToString();

                    DataSet dsCatgType = LstDoc.getDoctor_Terr_Catg_Spec_Class_Qual(Listed_DR_Code, sf_code);
                    if (dsCatgType.Tables[0].Rows.Count > 0)
                    {
                        ddlTerr.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        ddlCatg.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        ddlSpec.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                        ddlClass.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                        ddlQual.SelectedValue = dsCatgType.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    }
                }

                for (i = 3; i < ((grdDoctor.Columns.Count)); i++)
                {
                    grdDoctor.Columns[i].Visible = false;
                }

                for (int j = 0; j < CblDoctorCode.Items.Count; j++)
                {
                    for (i = 3; i < grdDoctor.Columns.Count; i++)
                    {
                        if (CblDoctorCode.Items[j].Selected == true)
                        {
                            if (grdDoctor.Columns[i].HeaderText.Trim() == CblDoctorCode.Items[j].Text.Trim())
                            {
                                grdDoctor.Columns[i].Visible = true;
                            }
                        }
                        //else
                        //{
                        //    if (grdDoctor.Columns[i].HeaderText.Trim() == "Product Name")
                        //    {
                        //        //grdDoctor.Columns[i].
                        //    }
                        //}
                    }
                }

                //if (CblDoctorCode.Items[0].Selected == false)
                //{
                //    grdDoctor.Columns[2].Visible = true;
                //}
                //else
                //{
                //    grdDoctor.Columns[2].Visible = false;
                //}

                //if (CblDoctorCode.Items[1].Selected == false)
                //{
                //    grdDoctor.Columns[3].Visible = true;
                //}
                //else
                //{
                //    grdDoctor.Columns[3].Visible = false;
                //}
            }
            else
            {
                //  menu1.Status = "Please select atleast one field to edit";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select atleast one field to edit');</script>");
            }
        }
    }


    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DropDownList ddlCatg = (DropDownList)e.Row.FindControl("Product_Cat_Code");
        //    if (ddlCatg != null)
        //    {
        //        DataRowView row = (DataRowView)e.Row.DataItem;
        //        ddlCatg.SelectedIndex = ddlCatg.Items.IndexOf(ddlCatg.Items.FindByValue(row["Product_Cat_Code"].ToString()));
        //    }
        //}
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[3].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

            }
        }
    }

    protected void btnClr_Click(object sender, EventArgs e)
    {
        for (i = 0; i < CblDoctorCode.Items.Count; i++)
        {
            CblDoctorCode.Items[i].Enabled = true;
            CblDoctorCode.Items[i].Selected = false;
        }
        CblDoctorCode.Enabled = true;
        btnSave.Visible = false;
        btnUpdate.Visible = false;
        grdDoctor.DataSource = null;
        grdDoctor.DataBind();
        tblDoctor.Visible = false;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string cntrl = string.Empty;
        string prod_code = string.Empty;
        string strTextBox = string.Empty;

        foreach (GridViewRow gridRow in grdDoctor.Rows)
        {
            for (i = 0; i < CblDoctorCode.Items.Count; i++)
            {
                if (CblDoctorCode.Items[i].Selected == true)
                {
                    cntrl = CblDoctorCode.Items[i].Value.ToString();
                    
                    if ((i != 0) && (i != 1) && (i != 2) && (i != 3) && (i != 4))
                    {
                        TextBox sTextBox = (TextBox)gridRow.Cells[1].FindControl(cntrl);
                        string stxt = sTextBox.Text.ToString().Substring(3, 2) + "/" + sTextBox.Text.ToString().Substring(0, 2) + "/" + sTextBox.Text.ToString().Substring(6, 4);
                        Label lblDoctorCode = (Label)gridRow.Cells[1].FindControl("lblDoctorCode");
                        Listed_DR_Code = lblDoctorCode.Text.ToString();
                        strTextBox = strTextBox + CblDoctorCode.Items[i].Value + "= '" + stxt + "',";
                    }
                    else                  
                    {
                        DropDownList sDDL = (DropDownList)gridRow.Cells[1].FindControl(cntrl);
                        string stxt = sDDL.SelectedValue.ToString();
                        Label lblDoctorCode = (Label)gridRow.Cells[1].FindControl("lblDoctorCode");
                        Listed_DR_Code = lblDoctorCode.Text.ToString();
                        strTextBox = strTextBox + CblDoctorCode.Items[i].Value + "= '" + stxt + "',";
                        //  }
                    }
                }
            }

            if (strTextBox.Trim().Length > 0)
            {
                //strTextBox = strTextBox.Substring(0, strTextBox.Length - 1);
                strTextBox = strTextBox + " LastUpdt_Date = getdate() ";
                UnListedDR lstDR = new UnListedDR();
                iReturn = lstDR.BulkEdit(strTextBox, Listed_DR_Code);
                strTextBox = "";
            }
        }

        if (iReturn > 0)
        {
            //menu1.Status = "UnListed Doctor detail(s) have been updated Successfully";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
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
        UnListedDR lstDR = new UnListedDR();
        dsDR = lstDR.FetchCategory(sf_code);
        if (dsDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_Cat_Name";
            ddlSrc2.DataValueField = "Doc_Cat_Code";
            ddlSrc2.DataSource = dsDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillSpl()
    {
        UnListedDR lstDR = new UnListedDR();
        dsDR = lstDR.FetchSpeciality(sf_code);
        if (dsDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_Special_Name";
            ddlSrc2.DataValueField = "Doc_Special_Code";
            ddlSrc2.DataSource = dsDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillQual()
    {
        UnListedDR lstDR = new UnListedDR();
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
        UnListedDR lstDR = new UnListedDR();
        dsDR = lstDR.FetchClass(sf_code);
        if (dsDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_ClsName";
            ddlSrc2.DataValueField = "Doc_ClsCode";
            ddlSrc2.DataSource = dsDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillTerr()
    {
        UnListedDR lstDR = new UnListedDR();
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
            Server.Transfer("UnLstDoctorList.aspx");
        }
        catch (Exception ex)
        {

        }
    }

}