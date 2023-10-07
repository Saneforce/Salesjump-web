using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_AdminSetupMGR : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsadmin = null;
    string div_code = string.Empty;
    DataSet dsTerr = new DataSet();
    string Doc_MulPlan = string.Empty;
    string DCR_TPBased = string.Empty;
    string DCR_DocCnt = string.Empty;
    string DCR_CheCnt = string.Empty;
    string DCR_StkCnt= string.Empty;
    string DCR_UnLstDocCnt = string.Empty;
    string DCR_ProdSelCnt = string.Empty;
    string DCR_ProdQtyZero = string.Empty;
    string DCR_Sess = string.Empty;
    string DCR_Time = string.Empty;
    int RemarksLength = -1;
    string MaxDocCnt = string.Empty;
    DataSet dsTerritory=new DataSet();
    string MaxCheCnt = string.Empty;
    string MaxStkCnt = string.Empty;
    int iIndex = -1;
    string doc_code = string.Empty;
    string terr_code = string.Empty;
    int UnLstDr_reqd = -1;
    string territory_code = string.Empty;
    string[] terr_cd;
    int pob = -1;
    int terr_sl_no = 0;
    int doc_disp = -1;
    int sess_dcr = 1;
    int time_dcr = 1;
    int prod_Qty_zero = -1;
    int prod_selection = -1;
    int max_dcr_prod = -1;
    int sess_mand_dcr = 1;
    int time_mand_dcr = 1;
    bool isValid = false;
    int iDelayedSystem = 0;
    int iApprovalSystem = 0;
    int iHolidayCalc = 0;
    int iDelayAllowDays = 0;
    int iHolidayStatus = 0;
    int iSundayStatus = 0;
    int iDrRem = 0;
    int iNewChem = 0;
    int iNewUn = 0;
   
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
          

            //rdbASM.DataTextField = "Designation_Short_Name";
            //rdbASM.DataValueField = "Designation_Code";
            //rdbASM.DataSource = dsDesign;
            //rdbASM.DataBind();

            AdminSetup dv = new AdminSetup();
            dsadmin = dv.getAdminSetup_MGR(div_code);
            GetWorkName();

            if (dsadmin.Tables[0].Rows.Count > 0)
            {
                //txtDRAllowed.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //txtChemAllowed.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                //txtStkAllowed.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                //if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() == "0")
                //{
                //    rdoMultiDRNo.Checked = true;
                //}
                //else
                //{
                //    rdoMultiDRYes.Checked = true;
                //}
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() == "1")
                {
                    rdoFFDCRTimeYes.Checked = true;
                }
                else
                {
                    rdoFFDCRTimeNo.Checked = true;
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() == "1")
                {
                    rdoFFDCRYes.Checked = true;
                }
                else
                {
                    rdoFFDCRNo.Checked = true;
                }
                txtChemAllow.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                txtDRAllow.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                txtUNLAllow.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                txtStkAllow.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                txtHosAllow.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
               
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "1")
                {
                    rdoDCRNone.Checked = true;
                }
                else
                {
                    rdoDCRNone.Checked = false;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "2")
                {
                    rdoDCRSVLNo.Checked = true;
                }
                else
                {
                    rdoDCRSVLNo.Checked = false;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "3")
                {
                    rdoDCRSpeciality.Checked = true;
                }
                else
                {
                    rdoDCRSpeciality.Checked = false;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "4")
                {
                    rdoDCRCategory.Checked = true;
                }
                else
                {
                    rdoDCRCategory.Checked = false;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "5")
                {
                    rdoClass.Checked = true;
                }
                else
                {
                    rdoClass.Checked = false;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "6")
                {
                    rdoCampaign.Checked = true;
                }
                else
                {
                    rdoCampaign.Checked = false;
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString() == "1")
                {
                    rdoFFUNLYes.Checked = true;
                }
                else
                {
                    rdoFFUNLNo.Checked = true;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(13).ToString() == "1")
                {
                    rdoFFDCRQtyYes.Checked = true;
                }
                else
                {
                    rdoFFDCRQtyNo.Checked = true;
                }
 
                //if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() == "0")
                //{
                //    rdoFFDCRQtyYes.Checked = true;
                //}
                //else
                //{
                //    rdoFFDCRQtyNo.Checked = true;
                //}

                //txtFFRemarks.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                //if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString() == "0")
                //{
                //    rdoDCRTP.Checked = true;
                //}
                //else
                //{
                //    rdoDCRWTP.Checked = true;
                //}
                //txtNoofTourPlan.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                txtFFProd.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString() == "1")
                {
                    rdopobprod.Checked = true;
                    rdoFFPOBYes.Checked = true;
                    rdoFFPOBNo.Checked = false;
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString() == "2")
                {
                    rdopobdoc.Checked = true;
                    rdoFFPOBYes.Checked = true;
                    rdoFFPOBNo.Checked = false;
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString() == "3")
                {
                    rdopobdocrx.Checked = true;
                    rdoFFPOBYes.Checked = true;
                    rdoFFPOBNo.Checked = false;
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString() == "0")
                {
                    rdopobprod.Checked = false;
                    rdopobdoc.Checked = false;
                    rdopobdocrx.Checked = false;
                    rdoFFPOBYes.Checked = false;
                    rdoFFPOBNo.Checked = true;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() == "1")
                {
                    rdoSessMYes.Checked = true;
                }
                else
                {
                    rdoSessMNo.Checked = true;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(17).ToString() == "1")
                {
                    rdoTimeMYes.Checked = true;
                }
                else
                {
                    rdoTimeMNo.Checked = true;
                }
                txtMaxProd.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
               // Session["TPView"] = txtNoofTourPlan.Text;


                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(22).ToString() == "0")
                {
                    rdoDlyNo.Checked = true;
                }
                else
                {
                    rdoDlyYes.Checked = true;
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(23).ToString() == "0")
                {
                    rdoDlyHolidayNo.Checked = true;
                }
                else
                {
                    rdoDlyHoliday.Checked = true;
                }

                txtNoDaysDly.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(24).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(25).ToString() == "0")
                {
                    rdoAutoHldNo.Checked = true;
                }
                else
                {
                    rdoAutoHldYes.Checked = true;
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(26).ToString() == "0")
                {
                    rdoAutoSunNo.Checked = true;
                }
                else
                {
                    rdoAutoSunYes.Checked = true;
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(27).ToString() == "0")
                {
                    rdoAprNo.Checked = true;
                }
                else
                {
                    rdoAprYes.Checked = true;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(28).ToString() == "0")
                {
                    rdoDRNo.Checked = true;
                }
                else
                {
                    rdoDRYes.Checked = true;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(29).ToString() == "0")
                {
                    rdoCheNo.Checked = true;
                }
                else
                {
                    rdoCheYes.Checked = true;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() == "0")
                {
                    rdoUnNo.Checked = true;
                }
                else
                {
                    rdoUnYes.Checked = true;
                }

                txtFFRemarks.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();

                DataSet dsDesign = new DataSet();
                Designation design = new Designation();
                dsDesign = design.getDesignationMGR(div_code);

                Designation Desig = new Designation();
                dsadmin = Desig.getDesignationMGR(div_code);             

                if (dsDesign.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drdoctor in dsDesign.Tables[0].Rows)
                    {
                        TableRow tr_det_sno = new TableRow();
                        TableCell tc_det_SNo = new TableCell();
                        
                        Literal lit_det_SNo = new Literal();
                        lit_det_SNo.Text = drdoctor["Designation_Short_Name"].ToString();
                        tc_det_SNo.Attributes.Add("Class", "tbldetail_Data");
                        tc_det_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_SNo.BorderWidth = 0;
                        tc_det_SNo.Controls.Add(lit_det_SNo);
                        tr_det_sno.Cells.Add(tc_det_SNo);

                       // tbl.Rows.Add(tr_det_sno);
                    }
                    DataSet dsDesignation = new DataSet();
                    //Designation design = new Designation();
                    dsDesignation = design.getDesignationMGR(div_code);
                    if (dsDesignation.Tables[0].Rows.Count > 0)
                    {
                        gvDesignation.DataSource = dsDesignation;
                        gvDesignation.DataBind();
                    }

                    foreach (GridViewRow row in gvDesignation.Rows)
                    {
                        CheckBox ChkBoxId = (CheckBox)row.FindControl("chkId");
                        CheckBox ChkBoxNo = (CheckBox)row.FindControl("chkNo");
                        Label lblDesignation = (Label)row.FindControl("lblDesignation");
                        if (dsadmin.Tables[0].Rows[row.RowIndex]["Designation_Short_Name"].ToString() == lblDesignation.Text)
                        {
                            if (dsadmin.Tables[0].Rows[row.RowIndex]["TP_Approval_Sys"].ToString() == "1")
                            {
                                ChkBoxId.Checked = true;
                            }
                            else
                            {
                                ChkBoxNo.Checked = true;
                            }
                        }
                    }
                   
                   
                    
                }
            }

        }

       if (rdoDCRTP.Checked)
        {

            gvDesignation.Visible = true;
        }
        else if (rdoDCRWTP.Checked)
        {

            gvDesignation.Visible = false;
        }
    }
    protected void chkNo_OnCheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvDesignation.Rows)
        {
            CheckBox ChkBoxId = (CheckBox)row.FindControl("chkId");
            CheckBox ChkBoxNo = (CheckBox)row.FindControl("chkNo");
            if (ChkBoxNo.Checked == true)
            {
                ChkBoxId.Checked = false;
            }

        }
    }

    protected void chkId_OnCheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvDesignation.Rows)
        {
            CheckBox ChkBoxId = (CheckBox)row.FindControl("chkId");
            CheckBox ChkBoxNo = (CheckBox)row.FindControl("chkNo");
            if (ChkBoxId.Checked == true)
            {

                ChkBoxNo.Checked = false;
            }

        }
    }
    private void GetWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            //for (int i = 0; i < ddlWorkingAreaList.Items.Count; i++)
            //{
            //    if (ddlWorkingAreaList.Items[i].Text == dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString())
            //    {
            //        ddlWorkingAreaList.SelectedValue = i.ToString();
            //    }
            //}
        }
    }

  
    protected void rdoDly_CheckedChanged(object sender, EventArgs e)
    {
        DelaySystemDisable();
    }
    protected void rdoDlyNo_CheckedChanged(object sender, EventArgs e)
    {
        DelaySystemDisable();
    }
    private void DelaySystemDisable()
    {
        if (rdoDlyNo.Checked == true)
        {
            rdoDlyHoliday.Enabled = false;
            rdoDlyHolidayNo.Enabled = false;
            txtNoDaysDly.Enabled = false;
        }
        else
        {
            rdoDlyHoliday.Enabled = true;
            rdoDlyHolidayNo.Enabled = true;
            txtNoDaysDly.Enabled = true;
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        isValid = true;
        //if (rdoMultiDRYes.Checked == true)
        //{
        //    Doc_MulPlan = "1";
        //}
        //if (rdoMultiDRNo.Checked == true)
        //{
        //    Doc_MulPlan = "0";           
        //}
        if (rdoFFUNLYes.Checked)
        {
            UnLstDr_reqd = 1;
        }
        if (rdoFFUNLNo.Checked)
        {
            UnLstDr_reqd = 0;
        }
        if (rdoFFDCRQtyYes.Checked)
        {
            prod_Qty_zero  = 1;
        }
        if (rdoFFDCRQtyNo.Checked)
        {
            prod_Qty_zero = 0;
        }
     
        if (rdoDCRNone.Checked)
        {
            doc_disp = 1;
        }
        else if (rdoDCRSVLNo.Checked)
        {
            doc_disp = 2;
        }
        else if (rdoDCRSpeciality.Checked)
        {
            doc_disp = 3;
        }
        else if (rdoDCRCategory.Checked)
        {
            doc_disp = 4;
        }
        else if (rdoClass.Checked)
        {
            doc_disp = 5;
        }
        else if (rdoCampaign.Checked)
        {
            doc_disp = 6;
        }


        if (rdoFFDCRYes.Checked)
        {
            sess_dcr = 1;
        }
        else
        {
            sess_dcr = 0;
        }
        if (rdoFFDCRTimeYes.Checked)
        {
            time_dcr  = 1;
        }
        else
        {
            time_dcr  = 0;
        }
        
        if (rdoSessMYes.Checked)
        {
            sess_mand_dcr = 1;
        }
        else
        {
            sess_mand_dcr = 0;
        }

        if (rdoTimeMYes.Checked)
        {
            time_mand_dcr = 1;
        }
        else
        {
            time_mand_dcr = 0;
        }

        if (rdoFFPOBYes.Checked)
        {
            if (rdopobprod.Checked)
            {
                pob = 1;
            }
            else if (rdopobdoc.Checked)
            {
                pob = 2;
            }
            else if (rdopobdocrx.Checked)
            {
                pob = 3;
            }
        }
        else
        {
            pob = 0;
        }

        if(txtFFProd.Text.Length >0)
            prod_selection = Convert.ToInt16(txtFFProd.Text);

        if (txtFFRemarks.Text.Length > 0)
            RemarksLength = Convert.ToInt16(txtFFRemarks.Text) ;
      
        if(txtMaxProd.Text.Length> 0)
        max_dcr_prod = Convert.ToInt16(txtMaxProd.Text);

        //IF Remove Session is selected then Mandatory should be "No"
        if (rdoFFDCRYes.Checked)
        {
            if (rdoSessMYes.Checked)
            {
                isValid = false;
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Session Mandatory option should be set No If Remove Session option is enabled.Please resubmit');</script>");
                rdoSessMNo.Checked = true;
            }
        }

        if (rdoFFDCRTimeYes.Checked)
        {
            if (rdoTimeMYes.Checked)
            {
                isValid = false;
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Time Mandatory option should be set No If Remove Time option is enabled.Please Resubmit');</script>");
                rdoTimeMNo.Checked = true;
            }
        }
        //Maximum Product Seslected
        if (txtFFProd.Text.Trim().Length > 0)
        {
            if (txtMaxProd.Text.Trim().Length > 0)
            {
                if ((Convert.ToInt16(txtFFProd.Text.Trim())) < (Convert.ToInt16(txtMaxProd.Text.Trim())))
                {
                    isValid = false;
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('No. of Products should be less than or equal to Max products.');</script>");
                    txtMaxProd.Focus();
                }
            }
        }

        //Approval System added by Sridevi on 06/04/2015
        if (rdoAprYes.Checked)
            iApprovalSystem = 1;
        else
            iApprovalSystem = 0;

        //Delayed System added by Sridevi on 06/04/2015
        if (rdoDlyYes.Checked)
            iDelayedSystem = 1;
        else
            iDelayedSystem = 0;

        if (rdoDlyHoliday.Checked)
            iHolidayCalc = 1;
        else
            iHolidayCalc = 0;

        if (txtNoDaysDly.Text.Trim().Length > 0)
            iDelayAllowDays = Convert.ToInt16(txtNoDaysDly.Text);

        if (rdoAutoHldYes.Checked)
            iHolidayStatus = 1;
        else
            iHolidayStatus = 0;

        if (rdoAutoSunYes.Checked)
            iSundayStatus = 1;
        else
            iSundayStatus = 0;

        //New Che
        if (rdoCheYes.Checked)
            iNewChem = 1;
        else
            iNewChem = 0;

        //New Un
        if (rdoUnYes.Checked)
            iNewUn = 1;
        else
            iNewUn = 0;

        //Doc Rem
        if (rdoDRYes.Checked)
            iDrRem = 1;
        else
            iDrRem = 0;


        if (isValid)
        {
            // Update Setup           
            AdminSetup dv = new AdminSetup();
            int iReturn = dv.RecordUpdate_MGR(Convert.ToInt32(txtDRAllow.Text.Trim()), Convert.ToInt32(txtChemAllow.Text.Trim()), Convert.ToInt32(txtStkAllow.Text.Trim()), Convert.ToInt32(txtUNLAllow.Text.Trim()), Convert.ToInt32(txtHosAllow.Text.Trim()), doc_disp, sess_dcr, time_dcr, UnLstDr_reqd, prod_Qty_zero, prod_selection, pob, sess_mand_dcr, time_mand_dcr, max_dcr_prod, iDelayedSystem, iHolidayCalc, iDelayAllowDays, iHolidayStatus, iSundayStatus, iApprovalSystem, div_code, RemarksLength, iDrRem, iNewChem, iNewUn);

            DataSet dsDesign = new DataSet();
            Designation design = new Designation();
            dsDesign = design.getDesignationMGR(div_code);

            //if (dsDesign.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow drdoctor in dsDesign.Tables[0].Rows)
            //    {
            //        foreach (ListItem r in rdbList.Items)
            //        {

            string strGMValue = string.Empty;
            string strSMValue = string.Empty;
            string strDMValue = string.Empty;
            string strSZValue = string.Empty;
            string strZSMValue = string.Empty;
            string strRSMValue = string.Empty;
            string strASMValue = string.Empty;
            string strDGMValue = string.Empty;

            int strChkId;
            foreach (GridViewRow row in gvDesignation.Rows)
            {
                CheckBox ChkBoxId = (CheckBox)row.FindControl("chkId");
                CheckBox ChkBoxNo = (CheckBox)row.FindControl("chkNo");
                Label lblDesignation = (Label)row.FindControl("lblDesignation");
                if (ChkBoxId.Checked == true)
                {
                    strChkId = 1;
                }
                else
                {
                    strChkId = 0;
                }

                iReturn = dv.RecordUpdate_DesigMR(strChkId, lblDesignation.Text);
            }          
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Setup has been updated Successfully');</script>");
            
        }
    } 
}