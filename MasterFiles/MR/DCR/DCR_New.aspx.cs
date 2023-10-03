using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Xml;
using System.Drawing;
using System.Configuration;

public partial class MasterFiles_MR_DCR_DCR_New : System.Web.UI.Page
{
    public string sf_code = string.Empty;
    string sCurDate = string.Empty;
    DateTime dtDCR;
    DataSet dsWT = null;
    DataSet dsDCR = null;
    DataSet dsDCRProd = null;
    DataSet dsDCRGift = null;
    DataSet dsDCRChe = null;
    DataSet dsDCRStk = null;
    DataSet dsDCRHos = null;
    DataSet dsDCRUnLst = null;
    DataSet dshead = null;
    DataSet dsadmin = null;
    DataSet dsSF = null;
    DataSet dsSalesForce = null;
    DataSet dsMgr = null;
    DataSet dsHoliday = null;
    DataSet dsLeave = null;
    DataSet dsListedDR = null;
    DataSet dsxml = null;
    DateTime tdate;
    DateTime headdate;
    DateTime start_date;
    bool chk = false;
    string FieldWork_Ind = string.Empty;
    string ButtonAccess = string.Empty;
    string strWeekoff = string.Empty;
    int iWeekOffind = -1;
    bool isReject = false;
    bool isEntry = false;
    bool isD = false;
    bool isCh = false;
    bool isSt = false;
    bool isHo = false;
    bool isUl = false;
    bool isRe = false;
    bool isLeave = false;
    int Qtyerr = 0;
    int iReturn_Det_val = 0;
    int max_doc_dcr_count = 0;
    int max_chem_dcr_count = 0;
    int max_stk_dcr_count = 0;
    int max_unlst_dcr_count = 0;
    int max_hos_dcr_count = 0;
    int iReturn = -1;
    int sess_m_dcr = -1;
    int time_m_dcr = -1;
    int iReturn_Det = -1;
    string sf_name = string.Empty;
    string emp_id = string.Empty;
    string employee_id = string.Empty;
    string Prod_Detail = string.Empty;
    string Prod_Detail_Code = string.Empty;

    string Gift_Detail = string.Empty;
    string Gift_Detail_Code = string.Empty;
    string sProd = string.Empty;
    string sFile = string.Empty;
    string WorkType = string.Empty;
    string PlanNo = string.Empty;
    string state_code = string.Empty;
    string Add_Prod_Detail = string.Empty;
    string Add_Prod_Code = string.Empty;
    string Add_Gift_Detail = string.Empty;
    string Add_Gift_Code = string.Empty;
    bool isAutopost = false;
    int doc_disp = -1;
    int sess_dcr = -1;
    int time_dcr = -1;
    int UnLstDr_reqd = -1;
    int prod_qty_dcr = -1;
    int prod_sel = -1;
    int pob = -1;
    int prod_mand_dcr = -1;
    bool isEdit = false;
    string div_code = string.Empty;

    bool stpchg = false;

    DCR_New dc = new DCR_New();
    DataTable dtUnsrc = null;
    DataTable dtLstsrc = null;
    DataTable dtChesrc = null;

    DataSet dsDCR1 = new DataSet();
    DataSet dsDCRNew = new DataSet();
    int iHoliday = -1;
    int iWeekOff = -1;
    int iDelayInd = -1;
    int iDelayHolInd = -1;
    int No_of_Days_Delay = -1;
    int dmon = -1;
    int dyear = -1;
    int iAppNeed = -1;
    int diffdays = -1;
    int imaxremarks = -1;
    string sWorkType = string.Empty;
    string sf_type = string.Empty;
    string lhead = string.Empty;
    string mdate = string.Empty;
    string mcurdate = string.Empty;
    int remarks_dcr = -1;
    int new_chem = -1;
    int new_uldr = -1;

    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;

    DataTable dtLstDocColor = new DataTable();
    DataTable dtChemColor = new DataTable();
    DataTable dtUnLstDocColor = new DataTable();
    DataTable dtWorkType = new DataTable();

    DataSet dsWorkTypeSettings = new DataSet();

    string IPAdd = string.Empty;
    string EntryMode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        div_code = Session["div_code"].ToString();
        hdnsftype.Value = sf_type;
        //This is for Manager
        if (sf_type == "2")
        {
            mcurdate = Request.QueryString["scurdate"];

            lhead = Request.QueryString["head"];
        }

        //Added a datatable with column for Work Type Color by Sridevi on 09/16/15
        dtWorkType.Columns.Add("wt_code", typeof(string));

        if (!Page.IsPostBack)
        {
            //Assign Admin Setup from DB to Dataset
            AdminSetup dv = new AdminSetup();
            if (sf_type == "1")
            {
                dsadmin = dv.getAdminSetup(div_code);
            }
            else
            {
                mcurdate = Request.QueryString["scurdate"];
                lhead = Request.QueryString["head"];

                dsadmin = dv.getAdminSetup_MGR(div_code);
            }

            ViewState["AdminSetup"] = dsadmin;

            if (dsadmin.Tables[0].Rows.Count > 0)
            {
                sess_dcr = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString());
                time_dcr = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString());
                max_doc_dcr_count = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString());
                max_chem_dcr_count = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString());
                max_unlst_dcr_count = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(8).ToString());
                max_stk_dcr_count = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(9).ToString());
                max_hos_dcr_count = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString());
                doc_disp = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString());
                Session["doc_disp"] = doc_disp.ToString();
                UnLstDr_reqd = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
                prod_qty_dcr = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(13).ToString());
                prod_sel = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(14).ToString());
                pob = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString());
                sess_m_dcr = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(16).ToString());
                time_m_dcr = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(17).ToString());
                prod_mand_dcr = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString());

                iDelayInd = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(22).ToString());
                iDelayHolInd = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(23).ToString());
                No_of_Days_Delay = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(24).ToString());

                iHoliday = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(25).ToString());
                iWeekOff = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(26).ToString());
                iAppNeed = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(27).ToString());
                imaxremarks = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(19).ToString());
                lblCount.Text = imaxremarks.ToString();
                remarks_dcr = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(28).ToString());
                new_chem = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(29).ToString());
                new_uldr = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(30).ToString());
                if (new_chem == 1)
                    btnNewChe.Attributes.Add("style", "display:block");
                else
                    btnNewChe.Attributes.Add("style", "display:none");
                if (new_uldr == 1)
                    btnUnNew.Attributes.Add("style", "display:block");
                else
                    btnUnNew.Attributes.Add("style", "display:none");

                txtRemarkDesc.MaxLength = imaxremarks;

                if (dsadmin.Tables[0].Rows.Count > 0)
                {
                    GrdAdmStp.DataSource = dsadmin;
                    GrdAdmStp.DataBind();
                    GrdAdmStp.Attributes.Add("style", "display:none");

                }
            }
            
            UnListedDR LstDR = new UnListedDR();
            dsHoliday = LstDR.getState(sf_code);
            if (dsHoliday.Tables[0].Rows.Count > 0)
            {
                state_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            ViewState["state_code"] = state_code;
            ViewState["clear"] = "0";
            //fetch the work type settings from DB and store it into Local dataset by Sridevi on 09/16/15

            dsWorkTypeSettings = dc.DCR_get_WorkType(div_code, sf_type);
            if (dsWorkTypeSettings.Tables[0].Rows.Count > 0)
            {               
                ViewState["dsWorkTypeSettings"] = dsWorkTypeSettings;
                grdWorkType.DataSource = dsWorkTypeSettings;
                grdWorkType.DataBind();
                grdWorkType.Attributes.Add("style", "display:none");

            }
            int csdf = grdWorkType.Rows.Count;
            ddlSDP.CssClass = "DropDownListCssClass";
            ddlcheMR.CssClass = "DropDownListCssClass";
            ddlUnMR.CssClass = "DropDownListCssClass";
            lblInfo.CssClass = "lblinfo";
   
            //lblSDP.Attributes.Add("style", "display:none");
            //ddlSDP.Attributes.Add("style", "display:none");
            Loaddcr("0");
            GetWorkName();
            ShowHideColumns();
            //if (((ddlSDP.Items.Count > 0) && (sf_type == "1")))
            //{
            //    DcrColor();
            //}
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        } // End of Post Back
        else
        {
            ddlSDP.CssClass = "DropDownListCssClass";
            ddlcheMR.CssClass = "DropDownListCssClass";
            ddlUnMR.CssClass = "DropDownListCssClass";
            lblInfo.CssClass = "lblinfo";
         
            state_code = ViewState["state_code"].ToString();
            dsadmin = (DataSet)ViewState["AdminSetup"];
            if (dsadmin.Tables[0].Rows.Count > 0)
            {
                sess_dcr = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString());
                time_dcr = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString());
                max_doc_dcr_count = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString());
                max_chem_dcr_count = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString());
                max_unlst_dcr_count = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(8).ToString());
                max_stk_dcr_count = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(9).ToString());
                max_hos_dcr_count = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString());
                doc_disp = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString());
                Session["doc_disp"] = doc_disp.ToString();
                UnLstDr_reqd = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString());
                prod_qty_dcr = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(13).ToString());
                prod_sel = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(14).ToString());
                pob = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString());
                sess_m_dcr = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(16).ToString());
                time_m_dcr = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(17).ToString());
                prod_mand_dcr = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString());

                iDelayInd = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(22).ToString());
                iDelayHolInd = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(23).ToString());
                No_of_Days_Delay = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(24).ToString());

                iHoliday = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(25).ToString());
                iWeekOff = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(26).ToString());
                iAppNeed = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(27).ToString());
                imaxremarks = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(19).ToString());
                remarks_dcr = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(28).ToString());
                new_chem = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(29).ToString());
                new_uldr = Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(30).ToString());

                txtRemarkDesc.MaxLength = imaxremarks;
            }
        }
        // To get IP ADDRESS         
        IPAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(IPAdd))
            IPAdd = Request.ServerVariables["REMOTE_ADDR"];

        // To find from where the application runs ( Mobile Browser or Computer)
        if (Request.Browser.IsMobileDevice)
        {
            EntryMode = "Mob";
        }
        else
        {
            EntryMode = "Desk";
        }
        // Ends

        if (ddlWorkType.Enabled == true)
            FillColor(); // Modified this function to fetch the data from datatable instead of DB by Sridevi on 09/16/15
        //if (sf_type == "2")
        //    FillMgrDocColor();
        lblReason.Text = "";

        //Included hidden control to hide Product Panel
        if (hidProdClose.Value == "1")
            pnlProduct.Attributes.Add("style", "display:none");

        //Included hidden control to hide Gift Panel
        if (hidGiftClose.Value == "1")
            pnlGift.Attributes.Add("style", "display:none");

        //Included hidden control to hide UnLst Dr Product Panel
        if (hidUnlstProdClose.Value == "1")
            pnlProduct_Unlst.Attributes.Add("style", "display:none");

        //Included hidden control to hide UnLst  Dr Gift Panel
        if (hdnUnLstgift.Value == "1")
            pnlGiftUnlst.Attributes.Add("style", "display:none");

        //Included hidden control to hide Remarks Panel
        if (hdnRemarks.Value == "1")
            pnlRemarks.Attributes.Add("style", "display:none");

        if (hdnChe.Value == "1")
            PnlChem.Attributes.Add("style", "display:none");

        if (hdnUnlst.Value == "1")
            NPnlUnLst.Attributes.Add("style", "display:none");
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

    private void GetWorkName()
    {
        DataSet dsTerritory = null;
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            lblSDP.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_SName"] + " Name";
        }
    }

    private void Loaddcr(string val)
    {
        // Session["backurl"] = "LstDoctorList.aspx";
        lblText.Text = "<span style='font-weight: bold;color:Black; font-size:15px; font-Names:Verdana '>" + "Daily Calls Entry For :- " + "</span>" + "<span style='font-weight: bold;color:DarkGreen; font-size:15px; font-Names:Verdana '>" + Session["sf_name"].ToString() + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"] + "</span>" + "&nbsp;&nbsp; ";

        lblInfo.Attributes.Add("style", "display:block");
        pnlTab.Attributes.Add("style", "display:none");
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + pnlTab1.ClientID + "') .style.display='none';", true);
        pnlMultiView.Attributes.Add("style", "display:none");

        pnlMultiView.Enabled = true;
        pnlTab.Enabled = true;
        pnlTop.Enabled = true;
        pnlTab1.Enabled = true;
        hdncheedit.Value = "";
        hdnnewpnlchem.Value = "";
        hdnundocedit.Value = "";
        hdnNewUnDoc.Value = "";
        hdndocedit.Value = "";
        hdnstkedit.Value = "";
        hdnhosedit.Value = "";
        hdnfwind.Value = "";
        //btnListedDR.Attributes.Add("style", "display:none");
        //btnChemists.Attributes.Add("style", "display:none");
        //btnStockist.Attributes.Add("style", "display:none");
        //btnNonListedDR.Attributes.Add("style", "display:none");
        //btnRemarks.Attributes.Add("style", "display:none");
        //btnPreview.Attributes.Add("style", "display:none");
        //btnHospital.Attributes.Add("style", "display:none");

        lblHeader.Text = "";
        lblReject.Text = "";
        lblCurDate.Text = "";
        txtRemarkDesc.Text = "";
        txtRemarks.Text = "";
        ViewState["scurdate"] = "";
        ViewState["isReject"] = "";
        ViewState["curdate"] = "";

        ViewState["AddProdExists"] = "";
        ViewState["AddGiftExists"] = "";
        ViewState["UnlstAddProdExists"] = "";
        ViewState["UnlstAddGiftExists"] = "";
        ViewState["CheEdit"] = "";
        ViewState["HosEdit"] = "";
        ViewState["StkEdit"] = "";
        ViewState["LDEdit"] = "";
        ViewState["UnLDEdit"] = "";
        ViewState["isEntry"] = "";
        ViewState["Count"] = "0";
        ViewState["RecExist"] = "0";
        ViewState["EditRecExist"] = "0";
        ViewState["Start"] = "0";
        ViewState["xmlExist"] = "0";
        ViewState["CurrentTable"] = null;
        ViewState["CurrentTableGift"] = null;
        ViewState["CurrentTableGiftUnlst"] = null;
        ViewState["CurrentTableUnlst"] = null;
  
        lblNote.Visible = false;
        lblReason.Visible = false;

        if (val == "0")
        {
            FillWorkType(sf_type);
            if (sf_type == "1")
            {
                FillSDP();
            }
        }
        if (sf_type == "2")
        {

            lblCurDate.Text = mcurdate;

            ViewState["scurdate"] = lblCurDate.Text;
            dtDCR = Convert.ToDateTime(mcurdate);
            ViewState["curdate"] = dtDCR.Day.ToString() + dtDCR.Month.ToString() + dtDCR.Year.ToString();
            lblHeader.Text = dtDCR.ToString("dd/MM/yyyy") + " - " + dtDCR.DayOfWeek.ToString();
            lblReject.Text = lhead;
            lblCurDate.Text = dtDCR.ToString("MM/dd/yyyy");
            Session["mcurdate"] = lblCurDate.Text;
            ViewState["Start"] = DateTime.Now.ToString();
            if (lhead == "(Re-Entry For Rejection)")
            {
                isReject = true;
                ViewState["isReject"] = "true";
            }
            else if (lhead == "(Edit)")
            {
                isReject = true;
                isEdit = true;
                ViewState["isReject"] = "true";
            }
            else if (lhead == "(Delay - Release)")
            {
                isReject = true;
                ViewState["isReject"] = "delay";
            }
            else if (lhead == "(Missed - Date)")
            {
                isReject = true;
                ViewState["isReject"] = "delay";
            }
            else
            {
                ViewState["isReject"] = "false";
            }
            FillAllTerr();
        }
        if (sf_type == "1")
        {
            ddlWorkType.SelectedIndex = 0;
            ddlSDP.SelectedIndex = 0;
            ddlWorkType.Enabled = true;
            ddlWorkType.Focus();
            FillSF();
        }
        if (val == "0")
        {
            if (((ddlSDP.Items.Count > 0) && (sf_type == "1")) || (sf_type == "2"))
            {
                FillProd();
                FillChem();
                FillStockiest();
                FillHospital();
                FillGift();
                FillListedDoctor();
                FillUnListedDoctor();
                FillCategory();
                FillSpeciality();
                FillClass();
                FillQualification();
                LoadSF();
            }          
        }
    
      
        //if (val == "1")
        //{
        //    clearww();
        //}
        if (lblInfo.Text != "Cannot Enter DCR for Future Date !!!")
        {
            Bind_Header();
            BindGrid("0");
            BindGrid_Chem("0");
            BindGrid_UnListedDR("0");
            BindGrid_Stockiest("0");
            BindGrid_Hospital("0");

            if (isEdit == true)
            {
                if (ViewState["clear"].ToString() != "1")
                {
                CreateXml();
                }
            }
            if (sf_type == "1")
            {
                DCR_New ds = new DCR_New();
                stpchg = ds.chkdcrstpchg(sf_type, lblCurDate.Text, div_code, sf_code);
                if (gvDCR.Rows.Count != 0 || grdChem.Rows.Count != 0 || grdUnLstDR.Rows.Count != 0 || GridHospital.Rows.Count != 0 || GridStk.Rows.Count != 0)
                {
                    if (stpchg == true)
                    {
                        pnlMultiView.Enabled = false;
                        pnlMultiView.Enabled = false;
                        pnlTab.Enabled = false;

                        ddlWorkType.Enabled = false;
                        ddlSDP.Enabled = false;
                        btnSave.Enabled = false;
                        btnSubmit.Enabled = false;
                        pnlMultiView.Attributes.Add("style", "display:block");
                        pnlTab.Attributes.Add("style", "display:block");
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + pnlTab1.ClientID + "') .style.display='block';", true);
                        precolor();
                        FillDoc();
                        Preview_Chem();
                        Preview_Stk();
                        Fill_Review();
                        FillUnlstDoc();
                        Preview_Hos();
                        ViewState["RecExist"] = "1";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('DCR Setup has been changed!!. Please Clear the DCR and Re-Enter');", true);
                    }
                    else if (chk == true)
                    {
                        pnlMultiView.Enabled = false;
                        pnlMultiView.Enabled = false;
                        pnlTab.Enabled = false;

                        ddlWorkType.Enabled = false;
                        ddlSDP.Enabled = false;
                        btnSave.Enabled = false;
                        btnSubmit.Enabled = false;
                        pnlMultiView.Attributes.Add("style", "display:block");
                        pnlTab.Attributes.Add("style", "display:block");
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + pnlTab1.ClientID + "') .style.display='block';", true);

                        precolor();
                        FillDoc();
                        Preview_Chem();
                        Preview_Stk();
                        Fill_Review();
                        FillUnlstDoc();
                        Preview_Hos();
                        ViewState["xmlExist"] = "1";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('DCR Setup has been changed!!. Please Clear the DCR and Re-Enter');", true);
                    }
                }
                if (val == "1")
                    FillColor();

            }
        }
        pnlProduct.Attributes.Add("style", "display:none");
        pnlGift.Attributes.Add("style", "display:none");
        pnlProduct_Unlst.Attributes.Add("style", "display:none");
        pnlGiftUnlst.Attributes.Add("style", "display:none");
        pnlRemarks.Attributes.Add("style", "display:none");
        PnlChem.Attributes.Add("style", "display:none");
        NPnlUnLst.Attributes.Add("style", "display:none");
        if (sf_type == "2")
        {
            DCR_New  dr = new DCR_New();
            dsMgr = dr.getMgrWorkAreaDtls(sf_code, lblCurDate.Text);
            if (dsMgr.Tables[0].Rows.Count > 0)
            {
                WorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                ViewState["worktype"] = WorkType;
                ddlWorkType.SelectedValue = WorkType;
                lblInfo.Attributes.Add("style", "display:none");
                ddlWorkType.Enabled = false;
                ddlSDP.Attributes.Add("style", "display:none");
                lblSDP.Attributes.Add("style", "display:none");
                pnlTab.Attributes.Add("style", "display:block");
                pnlTab1.Visible = true;
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + pnlTab1.ClientID + "') .style.display='block';", true);
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + pnlTab1.ClientID + "') .style.display='block';", true);
                pnlMultiView.Attributes.Add("style", "display:block");
                //MultiView1.Visible = true;
                LoadFF();
                loaddata();
            }
        }
        else if (sf_type == "1")
        {
            if (isReject == true)
            {
                DCR_New  dr = new DCR_New();
                if (isEdit == false)
                {
                    dsMgr = dr.getRejectedDCR(sf_code, lblCurDate.Text);
                    string strDate = lblCurDate.Text.ToString().Substring(3, 2) + "-" + lblCurDate.Text.ToString().Substring(0, 2) + "-" + lblCurDate.Text.ToString().Substring(6, 4);
                    lblReason.Text = "" + strDate + " " + "*** " + "DCR has been Rejected by your Line Manager" + "<br>" + "<br>" + " Reason for Rejection: " + dsMgr.Tables[0].Rows[0]["ReasonforRejection"].ToString();
                    lblNote.Visible = true;
                    lblReason.Visible = true;
                }
                else
                {
                    dsMgr = dr.getDCREdit(sf_code, lblCurDate.Text);
                    // Bindxml(sf_code, lblCurDate.Text);
                }
                if (dsMgr.Tables[0].Rows.Count > 0)
                {
                    if (ViewState["RecExist"].ToString() != "1" && ViewState["xmlExist"].ToString() != "1")
                    {
                        if (ViewState["clear"].ToString() != "1")
                        {
                            WorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            PlanNo = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                            txtRemarkDesc.Text = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

                            ddlWorkType.SelectedValue = WorkType;
                            ddlSDP.SelectedValue = PlanNo;
                            ddlSDP.Enabled = true;
                        }
                        else
                        {
                            lblSDP.Attributes.Add("style", "display:none");
                            ddlSDP.Attributes.Add("style", "display:none");
                        }
                        btnRemarks.Attributes.Add("style", "display:block");
                       // btnPreview.Attributes.Add("style", "display:block");

                        if ((ddlWorkType.SelectedIndex > 0)||((ddlWorkType.SelectedIndex > 0) && (ddlSDP.SelectedIndex > 0)) ||((sf_type == "2") && (ddlWorkType.SelectedIndex > 0)))
                        {
                            //MultiView1.Visible = true;
                            pnlMultiView.Attributes.Add("style", "display:block");
                            pnlTab.Attributes.Add("style", "display:block");
                            pnlTab1.Visible = true;
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + pnlTab1.ClientID + "') .style.display='block';", true);
                            lblInfo.Attributes.Add("style", "display:none");
                            loaddata();
                        }
                    }
                }
            }
            else
            {
                if (val == "0")
                {
                    if(ViewState["RecExist"].ToString() != "1" && ViewState["xmlExist"].ToString() !="1")
                        loaddata();
                }
                if (val == "1")
                {
                    lblSDP.Attributes.Add("style", "display:none");
                    ddlSDP.Attributes.Add("style", "display:none");
                    // MultiView1.Visible = false;
                    pnlMultiView.Attributes.Add("style", "display:none");
                    pnlTab.Attributes.Add("style", "display:none");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + pnlTab1.ClientID + "') .style.display='none';", true);
                    lblInfo.Visible = true;
                    if (lblInfo.Text != "Cannot Enter DCR for Future Date !!!")
                    {
                        lblInfo.Text = "Select the WorkType...";
                    }
                }
            }
        }
    }

    private void loaddata()
    {
        if (ddlWorkType.SelectedValue.ToString() != "0")
        {
            //MultiView1.Visible = true;
            pnlMultiView.Attributes.Add("style", "display:block");
            btnListedDR.Attributes.Add("style", "display:none");
            btnChemists.Attributes.Add("style", "display:none");
            btnStockist.Attributes.Add("style", "display:none");
            btnHospital.Attributes.Add("style", "display:none");
            btnNonListedDR.Attributes.Add("style", "display:none");
            btnRemarks.Attributes.Add("style", "display:block");
           // btnPreview.Attributes.Add("style", "display:block");

            pnlTab.Attributes.Add("style", "display:block");
            pnlTab1.Visible = true;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + pnlTab1.ClientID + "') .style.display='block';", true);
            lblInfo.Attributes.Add("style", "display:none");

            //Filter the Dataset based on the Work Type selected from the dropdown by Sridevi on 09/16/15
            if (ddlWorkType.SelectedIndex > 0)
            {
                DataTable dtWTSettings = WorkType_Selection(ddlWorkType.SelectedValue.ToString().Trim());
                if (dtWTSettings != null)
                {
                    if (dtWTSettings.Rows.Count > 0)
                    {
                        FieldWork_Ind = dtWTSettings.Rows[0].ItemArray.GetValue(0).ToString();
                        ButtonAccess = dtWTSettings.Rows[0].ItemArray.GetValue(1).ToString();
                    }
                    string[] but;
                    if (ButtonAccess != "")
                    {
                        but = ButtonAccess.Split(',');
                        foreach (string st in but)
                        {
                            if (st == "R")
                            {
                                isRe = true;
                                btnRemarks.Attributes.Add("style", "display:block");

                            }
                            if (st == "D")
                            {
                                isD = true;
                                isEntry = true;
                                ViewState["isEntry"] = "true";
                                btnListedDR.Attributes.Add("style", "display:block");

                            }
                            if (st == "C")
                            {
                                isCh = true;
                                isEntry = true;
                                ViewState["isEntry"] = "true";
                                btnChemists.Attributes.Add("style", "display:block");

                            }
                            if (st == "U")
                            {
                                isUl = true;
                                isEntry = true;
                                ViewState["isEntry"] = "true";
                                btnNonListedDR.Attributes.Add("style", "display:block");

                            }
                            if (st == "H")
                            {
                                isHo = true;
                                isEntry = true;
                                ViewState["isEntry"] = "true";
                                btnHospital.Attributes.Add("style", "display:block");

                            }

                            if (st == "S")
                            {
                                isSt = true;
                                isEntry = true;
                                ViewState["isEntry"] = "true";
                                btnStockist.Attributes.Add("style", "display:block");

                            }
                        }
                    }
                    if (isD == true)
                    {
                      
                        ldrcolor();
                    }
                    else if (isCh == true)
                    {
                       
                        checolor();

                    }
                    else if (isSt == true)
                    {
                        
                        stkcolor();

                    }
                    else if (isUl == true)
                    {
                    
                        udrcolor();
                    }
                    else if (isHo == true)
                    {
                       
                        hoscolor();
                    }
                    else if (isRe == true)
                    {
                       
                        remcolor();
                    }

                    if (gvDCR.Rows.Count > 0)
                    {
                      hdnbutname.Value = "0";
                      ldrcolor();
                    }
                    else if (grdChem.Rows.Count > 0)
                    {
                        hdnbutname.Value = "1";
                        checolor();
                    }
                    else if (GridStk.Rows.Count > 0)
                    {
                        hdnbutname.Value = "2";
                        stkcolor();
                    }
                    else if (grdUnLstDR.Rows.Count > 0)
                    {
                        hdnbutname.Value = "3";
                        udrcolor();
                    }
                    else if (GridHospital.Rows.Count > 0)
                    {
                        hdnbutname.Value = "4";
                        hoscolor();
                    }

                    if (isEntry == true)
                    {
                        hdnfwind.Value = "F";
                        if (sf_type == "1")
                        {
                            lblSDP.Attributes.Add("style", "display:block");
                            ddlSDP.Attributes.Add("style", "display:block");
                        }
                        else
                        {
                            lblSDP.Attributes.Add("style", "display:none");
                            ddlSDP.Attributes.Add("style", "display:none");
                        }
                    }
                    else
                    {
                        lblSDP.Attributes.Add("style", "display:none");
                        ddlSDP.Attributes.Add("style", "display:none");                       
                        remcolor();
                    }
                }
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetListedDR(string prefixText)
    {
        //String Terr_code = HttpContext.Current.Session["Territory"].ToString();
        DataTable dt = new DataTable();
        DCR_New dc = new DCR_New();
        string sfcode = HttpContext.Current.Session["sf_code"].ToString();
        string sftype = HttpContext.Current.Session["sf_type"].ToString();
        string sListed_DR = string.Empty;
        string sListed_DR_Code = string.Empty;
        //int iListed_DR = -1;

        if (sftype == "1")
        {
            dt = dc.getListedDoctor(sfcode, prefixText, HttpContext.Current.Session["doc_disp"].ToString());
        }
        else if (sftype == "2")
        {
            dt = dc.getListedDoctorMGR(sfcode, prefixText, HttpContext.Current.Session["doc_disp"].ToString(), HttpContext.Current.Session["mcurdate"].ToString());
        }

        List<string> ListedDR = new List<string>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            
            string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString());
            ListedDR.Add(item);
         
        }
        return ListedDR;
    }


   

   
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetChemists(string prefixText)
    {
        //String Terr_code = HttpContext.Current.Session["Territory"].ToString();
        DataTable dt = new DataTable();
        DCR_New dc = new DCR_New();
        string sfcode = HttpContext.Current.Session["sf_code"].ToString();
        string sftype = HttpContext.Current.Session["sf_type"].ToString();
        string sListed_DR = string.Empty;
        string sListed_DR_Code = string.Empty;
        //int iListed_DR = -1;
        if (sftype == "1")
        {
            dt = dc.getChe_src(sfcode, prefixText);
        }
        else if (sftype == "2")
        {
            dt = dc.getChe_srcMGR(sfcode, prefixText, HttpContext.Current.Session["mcurdate"].ToString());
        }
        List<string> ListedDR = new List<string>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            
            string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString());
            ListedDR.Add(item);
           
        }
        return ListedDR;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetUNListedDR(string prefixText)
    {
        //String Terr_code = HttpContext.Current.Session["Territory"].ToString();
        DataTable dt = new DataTable();
        DCR_New dc = new DCR_New();
        string sfcode = HttpContext.Current.Session["sf_code"].ToString();
        string sftype = HttpContext.Current.Session["sf_type"].ToString();
        string sListed_DR = string.Empty;
        string sListed_DR_Code = string.Empty;
        //int iListed_DR = -1;
        if (sftype == "1")
        {
            dt = dc.getUnDoctor(sfcode, prefixText);
        }
        else if (sftype == "2")
        {
            dt = dc.getUnDoctor_MGR(sfcode, prefixText, HttpContext.Current.Session["mcurdate"].ToString());
        }
        List<string> ListedDR = new List<string>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
          
            string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString());
            ListedDR.Add(item);
          
        }
        return ListedDR;
    }
    private void butvissetup()
    {
        
        btnListedDR.Attributes.Add("style", "display:none");
        btnChemists.Attributes.Add("style", "display:none");
        btnStockist.Attributes.Add("style", "display:none");
        btnNonListedDR.Attributes.Add("style", "display:none");
        btnHospital.Attributes.Add("style", "display:none");

        btnPreview.Attributes.Add("style", "display:none");
        btnRemarks.Attributes.Add("style", "display:block");
        DataTable dtWTSettings = WorkType_Selection(ddlWorkType.SelectedValue.ToString().Trim());
        if (dtWTSettings != null)
        {
            if (dtWTSettings.Rows.Count > 0)
            {
                FieldWork_Ind = dtWTSettings.Rows[0].ItemArray.GetValue(0).ToString();
                ButtonAccess = dtWTSettings.Rows[0].ItemArray.GetValue(1).ToString();
            }

            string[] but;
            if (ButtonAccess != "")
            {
                but = ButtonAccess.Split(',');
                foreach (string st in but)
                {
                    if (st == "R"){
                        btnRemarks.Attributes.Add("style", "display:block");
                        ViewState["isEntry"] = "";
                    }
                    if (st == "D")
                    {
                        ViewState["isEntry"] = "true";
                        btnListedDR.Attributes.Add("style", "display:block");
                    }
                    if (st == "C")
                    {
                        ViewState["isEntry"] = "true";
                        btnChemists.Attributes.Add("style", "display:block");
                    }
                    if (st == "U")
                    {
                        ViewState["isEntry"] = "true";
                        btnNonListedDR.Attributes.Add("style", "display:block");
                    }
                    if (st == "H")
                    {
                        ViewState["isEntry"] = "true";
                        btnHospital.Attributes.Add("style", "display:block");
                    }
                    if (st == "S")
                    {
                        ViewState["isEntry"] = "true";
                        btnStockist.Attributes.Add("style", "display:block");
                    }
                }
            }

        }
    }
    private void ldrcolor()
    {
        butvissetup();

        pnlMultiView.Attributes.Add("style", "display:block");
        pnlTab.Attributes.Add("style", "display:block");
        if (sf_type == "1")
        {
            if (ViewState["isEntry"].ToString() == "true")
            {
                lblSDP.Attributes.Add("style", "display:block");
                ddlSDP.Attributes.Add("style", "display:block");
                ddlSDP.CssClass = "DropDownListCssClass";
            }
            else
            {
                lblSDP.Attributes.Add("style", "display:none");
                ddlSDP.Attributes.Add("style", "display:none");
            }
        }
        else
        {
            lblSDP.Attributes.Add("style", "display:none");
            ddlSDP.Attributes.Add("style", "display:none");
        }
        pnlTabLstDoc.Attributes.Add("style", "display:block");

        pnlTabChem.Attributes.Add("style", "display:none");
        PnlTabStk.Attributes.Add("style", "display:none");
        PnlTabUnLst.Attributes.Add("style", "display:none");
        PnlTabHos.Attributes.Add("style", "display:none");
        PnlTabRem.Attributes.Add("style", "display:none");
        PnlTabPrev.Attributes.Add("style", "display:none");

        btnListedDR.BackColor = Color.Green;
        btnChemists.BackColor = Color.White;
        btnStockist.BackColor = Color.White;
        btnHospital.BackColor = Color.White;
        btnRemarks.BackColor = Color.White;
        btnNonListedDR.BackColor = Color.White;
        btnPreview.BackColor = Color.White;
        btnListedDR.ForeColor = Color.White;
        btnChemists.ForeColor = Color.Black;
        btnStockist.ForeColor = Color.Black;
        btnHospital.ForeColor = Color.Black;
        btnRemarks.ForeColor = Color.Black;
        btnNonListedDR.ForeColor = Color.Black;
        btnPreview.ForeColor = Color.Black;
    }
    private void checolor()
    {
        butvissetup();
        pnlMultiView.Attributes.Add("style", "display:block");
        pnlTab.Attributes.Add("style", "display:block");
        if (sf_type == "1")
        {
            if (ViewState["isEntry"].ToString() == "true")
            {
                lblSDP.Attributes.Add("style", "display:block");
                ddlSDP.Attributes.Add("style", "display:block");
                ddlSDP.CssClass = "DropDownListCssClass";
            }
            else
            {
                lblSDP.Attributes.Add("style", "display:none");
                ddlSDP.Attributes.Add("style", "display:none");
            }
        }
        else
        {
            lblSDP.Attributes.Add("style", "display:none");
            ddlSDP.Attributes.Add("style", "display:none");
        }

        pnlTabChem.Attributes.Add("style", "display:block");

        pnlTabLstDoc.Attributes.Add("style", "display:none");
        PnlTabStk.Attributes.Add("style", "display:none");
        PnlTabUnLst.Attributes.Add("style", "display:none");
        PnlTabHos.Attributes.Add("style", "display:none");
        PnlTabRem.Attributes.Add("style", "display:none");
        PnlTabPrev.Attributes.Add("style", "display:none");

        btnChemists.BackColor = Color.Green;
        btnListedDR.BackColor = Color.White;
        btnStockist.BackColor = Color.White;
        btnHospital.BackColor = Color.White;
        btnRemarks.BackColor = Color.White;
        btnNonListedDR.BackColor = Color.White;
        btnPreview.BackColor = Color.White;
        btnListedDR.ForeColor = Color.Black;
        btnChemists.ForeColor = Color.White;
        btnStockist.ForeColor = Color.Black;
        btnHospital.ForeColor = Color.Black;
        btnRemarks.ForeColor = Color.Black;
        btnNonListedDR.ForeColor = Color.Black;
        btnPreview.ForeColor = Color.Black;

    }
    private void stkcolor()
    {
        butvissetup();
        pnlMultiView.Attributes.Add("style", "display:block");
        pnlTab.Attributes.Add("style", "display:block");
        if (sf_type == "1")
        {
            if (ViewState["isEntry"].ToString() == "true")
            {
                lblSDP.Attributes.Add("style", "display:block");
                ddlSDP.Attributes.Add("style", "display:block");
                ddlSDP.CssClass = "DropDownListCssClass";
            }
            else
            {
                lblSDP.Attributes.Add("style", "display:none");
                ddlSDP.Attributes.Add("style", "display:none");
            }
        }
        else
        {
            lblSDP.Attributes.Add("style", "display:none");
            ddlSDP.Attributes.Add("style", "display:none");
        }

        PnlTabStk.Attributes.Add("style", "display:block");

        pnlTabChem.Attributes.Add("style", "display:none");
        pnlTabLstDoc.Attributes.Add("style", "display:none");
        PnlTabUnLst.Attributes.Add("style", "display:none");
        PnlTabHos.Attributes.Add("style", "display:none");
        PnlTabRem.Attributes.Add("style", "display:none");
        PnlTabPrev.Attributes.Add("style", "display:none");

        btnChemists.BackColor = Color.White;
        btnListedDR.BackColor = Color.White;
        btnStockist.BackColor = Color.Green;
        btnHospital.BackColor = Color.White;
        btnRemarks.BackColor = Color.White;
        btnNonListedDR.BackColor = Color.White;
        btnPreview.BackColor = Color.White;
        btnListedDR.ForeColor = Color.Black;
        btnChemists.ForeColor = Color.Black;
        btnStockist.ForeColor = Color.White;
        btnHospital.ForeColor = Color.Black;
        btnRemarks.ForeColor = Color.Black;
        btnNonListedDR.ForeColor = Color.Black;
        btnPreview.ForeColor = Color.Black;
    }
    private void udrcolor()
    {
        butvissetup();
        pnlMultiView.Attributes.Add("style", "display:block");
        pnlTab.Attributes.Add("style", "display:block");
        if (sf_type == "1")
        {
            if (ViewState["isEntry"].ToString() == "true")
            {
                lblSDP.Attributes.Add("style", "display:block");
                ddlSDP.Attributes.Add("style", "display:block");
                ddlSDP.CssClass = "DropDownListCssClass";
            }
            else
            {
                lblSDP.Attributes.Add("style", "display:none");
                ddlSDP.Attributes.Add("style", "display:none");
            }
        }
        else
        {
            lblSDP.Attributes.Add("style", "display:none");
            ddlSDP.Attributes.Add("style", "display:none");
        }

        PnlTabUnLst.Attributes.Add("style", "display:block");

        pnlTabChem.Attributes.Add("style", "display:none");
        PnlTabStk.Attributes.Add("style", "display:none");
        pnlTabLstDoc.Attributes.Add("style", "display:none");
        PnlTabHos.Attributes.Add("style", "display:none");
        PnlTabRem.Attributes.Add("style", "display:none");
        PnlTabPrev.Attributes.Add("style", "display:none");

        btnChemists.BackColor = Color.White;
        btnListedDR.BackColor = Color.White;
        btnStockist.BackColor = Color.White;
        btnHospital.BackColor = Color.White;
        btnRemarks.BackColor = Color.White;
        btnNonListedDR.BackColor = Color.Green;
        btnPreview.BackColor = Color.White;
        btnListedDR.ForeColor = Color.Black;
        btnChemists.ForeColor = Color.Black;
        btnStockist.ForeColor = Color.Black;
        btnHospital.ForeColor = Color.Black;
        btnRemarks.ForeColor = Color.Black;
        btnNonListedDR.ForeColor = Color.White;
        btnPreview.ForeColor = Color.Black;

    }
    private void hoscolor()
    {
        butvissetup();
        pnlMultiView.Attributes.Add("style", "display:block");
        pnlTab.Attributes.Add("style", "display:block");
        if (sf_type == "1")
        {
            if (ViewState["isEntry"].ToString() == "true")
            {
                lblSDP.Attributes.Add("style", "display:block");
                ddlSDP.Attributes.Add("style", "display:block");
                ddlSDP.CssClass = "DropDownListCssClass";
            }
            else
            {
                lblSDP.Attributes.Add("style", "display:none");
                ddlSDP.Attributes.Add("style", "display:none");
            }
        }
        else
        {
            lblSDP.Attributes.Add("style", "display:none");
            ddlSDP.Attributes.Add("style", "display:none");
        }

        PnlTabHos.Attributes.Add("style", "display:block");

        pnlTabChem.Attributes.Add("style", "display:none");
        PnlTabStk.Attributes.Add("style", "display:none");
        PnlTabUnLst.Attributes.Add("style", "display:none");
        pnlTabLstDoc.Attributes.Add("style", "display:none");
        PnlTabRem.Attributes.Add("style", "display:none");
        PnlTabPrev.Attributes.Add("style", "display:none");

        btnChemists.BackColor = Color.White;
        btnListedDR.BackColor = Color.White;
        btnStockist.BackColor = Color.White;
        btnHospital.BackColor = Color.Green;
        btnRemarks.BackColor = Color.White;
        btnNonListedDR.BackColor = Color.White;
        btnPreview.BackColor = Color.White;
        btnListedDR.ForeColor = Color.Black;
        btnChemists.ForeColor = Color.Black;
        btnStockist.ForeColor = Color.Black;
        btnHospital.ForeColor = Color.White;
        btnRemarks.ForeColor = Color.Black;
        btnNonListedDR.ForeColor = Color.Black;
        btnPreview.ForeColor = Color.Black;
    }
    private void remcolor()
    {
        butvissetup();
        pnlMultiView.Attributes.Add("style", "display:block");
        pnlTab.Attributes.Add("style", "display:block");

        if (sf_type == "1")
        {
            if (ViewState["isEntry"].ToString() == "true")
            {
                lblSDP.Attributes.Add("style", "display:block");
                ddlSDP.Attributes.Add("style", "display:block");
                ddlSDP.CssClass = "DropDownListCssClass";
            }
            else
            {
                lblSDP.Attributes.Add("style", "display:none");
                ddlSDP.Attributes.Add("style", "display:none");
            }
        }
        else
        {
            lblSDP.Attributes.Add("style", "display:none");
            ddlSDP.Attributes.Add("style", "display:none");
        }

        PnlTabRem.Attributes.Add("style", "display:block");

        pnlTabChem.Attributes.Add("style", "display:none");
        PnlTabStk.Attributes.Add("style", "display:none");
        PnlTabUnLst.Attributes.Add("style", "display:none");
        PnlTabHos.Attributes.Add("style", "display:none");
        pnlTabLstDoc.Attributes.Add("style", "display:none");
        PnlTabPrev.Attributes.Add("style", "display:none");

        btnChemists.BackColor = Color.White;
        btnListedDR.BackColor = Color.White;
        btnStockist.BackColor = Color.White;
        btnHospital.BackColor = Color.White;
        btnRemarks.BackColor = Color.Green;
        btnNonListedDR.BackColor = Color.White;
        btnPreview.BackColor = Color.White;
        btnListedDR.ForeColor = Color.Black;
        btnChemists.ForeColor = Color.Black;
        btnStockist.ForeColor = Color.Black;
        btnHospital.ForeColor = Color.Black;
        btnRemarks.ForeColor = Color.White;
        btnNonListedDR.ForeColor = Color.Black;
        btnPreview.ForeColor = Color.Black;
    }
    private void precolor()
    {
        butvissetup();
        pnlMultiView.Attributes.Add("style", "display:block");
        pnlTab.Attributes.Add("style", "display:block");

        if (sf_type == "1")
        {
            if (ViewState["isEntry"].ToString() == "true")
            {
                lblSDP.Attributes.Add("style", "display:block");
                ddlSDP.Attributes.Add("style", "display:block");
                ddlSDP.CssClass = "DropDownListCssClass";
            }
            else
            {
                lblSDP.Attributes.Add("style", "display:none");
                ddlSDP.Attributes.Add("style", "display:none");
            }
        }
        else
        {
            lblSDP.Attributes.Add("style", "display:none");
            ddlSDP.Attributes.Add("style", "display:none");
        }

        PnlTabPrev.Attributes.Add("style", "display:block");

        pnlTabChem.Attributes.Add("style", "display:none");
        PnlTabStk.Attributes.Add("style", "display:none");
        PnlTabUnLst.Attributes.Add("style", "display:none");
        PnlTabHos.Attributes.Add("style", "display:none");
        pnlTabLstDoc.Attributes.Add("style", "display:none");
        PnlTabRem.Attributes.Add("style", "display:none");

        btnChemists.BackColor = Color.White;
        btnListedDR.BackColor = Color.White;
        btnStockist.BackColor = Color.White;
        btnHospital.BackColor = Color.White;
        btnRemarks.BackColor = Color.White;
        btnPreview.BackColor = Color.Green;
        btnNonListedDR.BackColor = Color.White;

        btnListedDR.ForeColor = Color.Black;
        btnChemists.ForeColor = Color.Black;
        btnStockist.ForeColor = Color.Black;
        btnHospital.ForeColor = Color.Black;
        btnRemarks.ForeColor = Color.Black;
        btnNonListedDR.ForeColor = Color.Black;
        btnPreview.ForeColor = Color.White;

    }


    private void FillSF()
    {
        int iiholind = 0;
        int iileave = 0;
        DCR_New dc = new DCR_New();
        // Check for any Rejected DCR
        dsSF = dc.getDCREntryDate_Reject(sf_code);
        if (dsSF.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "")
        {
            isReject = true;
            sCurDate = dsSF.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            ViewState["scurdate"] = sCurDate;
            ViewState["isReject"] = "true";
            dtDCR = Convert.ToDateTime(sCurDate);
            // dtDCR = dtDCR.AddDays(1);
            ViewState["curdate"] = dtDCR.Day.ToString() + dtDCR.Month.ToString() + dtDCR.Year.ToString();


            lblHeader.Text = dtDCR.ToString("dd/MM/yyyy") + " - " + dtDCR.DayOfWeek.ToString();
            lblReject.Text = "&nbsp;&nbsp; (Re-Entry For Rejection)";
            lblCurDate.Text = dtDCR.ToString("MM/dd/yyyy");

            // Check for any leave request for the day..  
            dsLeave = dc.getLeave(sf_code, lblCurDate.Text);
            if (dsLeave.Tables[0].Rows.Count > 0)
            {
                //Create DCR For Leave
                iileave = 1;
                if (dsLeave.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "0")
                {
                    isAutopost = true;
                }
                else if (dsLeave.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "2")
                {
                    isAutopost = false;
                }

                dsMgr = dc.getWorkTypeCode_MR("L", sf_type, div_code);
                if (dsMgr.Tables[0].Rows.Count > 0)
                    sWorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                AutoCreateDCR_Leave(lblCurDate.Text, sWorkType, dtDCR.ToString("dd/MM/yyyy"), isAutopost, "Leave");
                Loaddcr("1");
            }

            //If Autopost Holiday is true
            if ((iHoliday == 1) && (iileave == 0))// Holiday  - to Autopost
            {
                //Check the current date belongs to Holiday list - by Sridevi on 06/05/15
                iiholind = AutoPost_Holiday(sCurDate);
                if (iiholind == 1)
                {
                    isAutopost = true;
                    dsMgr = dc.getWorkTypeCode_MR("H", sf_type, div_code);
                    if (dsMgr.Tables[0].Rows.Count > 0)
                        sWorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    AutoCreateDCR(lblCurDate.Text, sWorkType, dtDCR.ToString("dd/MM/yyyy"), isAutopost, "Holiday");
                    Loaddcr("1");
                }

            }
            //If Autopost Holiday is true
            if ((iWeekOff == 1) && (iiholind == 0) && (iileave == 0))
            {
                //Check the current date belongs to Week Off -  by Sridevi on 06/05/15
                string wday = string.Empty;
                wday = dtDCR.DayOfWeek.ToString();

                int weekoffindicator = AutoPost_WeekOff(wday);

                if (weekoffindicator == 1)
                {
                    isAutopost = true;
                    dsMgr = dc.getWorkTypeCode_MR("W", sf_type, div_code);//Week Off
                    if (dsMgr.Tables[0].Rows.Count > 0)
                        sWorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    AutoCreateDCR(lblCurDate.Text, sWorkType, dtDCR.ToString("dd/MM/yyyy"), isAutopost, "Week-Off");
                    Loaddcr("1");
                }
            }
        }
        else
        {
            // Check for any Edit DCR
            dsSF = dc.getDCREntryDate_Edit(sf_code);
            if (dsSF.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "")
            {
                isReject = true;
                isEdit = true;
                ViewState["isReject"] = "true";
                sCurDate = dsSF.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                ViewState["scurdate"] = sCurDate;
                dtDCR = Convert.ToDateTime(sCurDate);
                // dtDCR = dtDCR.AddDays(1);
                ViewState["curdate"] = dtDCR.Day.ToString() + dtDCR.Month.ToString() + dtDCR.Year.ToString();

                lblHeader.Text = dtDCR.ToString("dd/MM/yyyy") + " - " + dtDCR.DayOfWeek.ToString();
                lblReject.Text = "&nbsp;&nbsp; (Edit)";
                lblCurDate.Text = dtDCR.ToString("MM/dd/yyyy");

                // Check for any leave request for the day..  
                dsLeave = dc.getLeave(sf_code, lblCurDate.Text);
                if (dsLeave.Tables[0].Rows.Count > 0)
                {
                    //Create DCR For Leave
                    iileave = 1;
                    if (dsLeave.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "0")
                    {
                        isAutopost = true;
                    }
                    else if (dsLeave.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "2")
                    {
                        isAutopost = false;
                    }

                    dsMgr = dc.getWorkTypeCode_MR("L", sf_type, div_code);
                    if (dsMgr.Tables[0].Rows.Count > 0)
                        sWorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    AutoCreateDCR_Leave(lblCurDate.Text, sWorkType, dtDCR.ToString("dd/MM/yyyy"), isAutopost, "Leave");
                    Loaddcr("1");
                }

               
               
            }
            else
            {
                // Check for any Delay release

                dsSF = dc.getDCREntryDelay_Release(sf_code);
                if (dsSF.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "")
                {
                    //isReject = true;
                    ViewState["isReject"] = "delay";
                    sCurDate = dsSF.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    ViewState["scurdate"] = sCurDate;
                    dtDCR = Convert.ToDateTime(sCurDate);
                    // dtDCR = dtDCR.AddDays(1);
                    ViewState["curdate"] = dtDCR.Day.ToString() + dtDCR.Month.ToString() + dtDCR.Year.ToString();

                    lblHeader.Text = dtDCR.ToString("dd/MM/yyyy") + " - " + dtDCR.DayOfWeek.ToString();
                    lblReject.Text = "&nbsp;&nbsp; (Delay - Release)";
                    lblCurDate.Text = dtDCR.ToString("MM/dd/yyyy");

                    // Check for any leave request for the day..  
                    dsLeave = dc.getLeave(sf_code, lblCurDate.Text);
                    if (dsLeave.Tables[0].Rows.Count > 0)
                    {
                        //Create DCR For Leave
                        iileave = 1;
                        if (dsLeave.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "0")
                        {
                            isAutopost = true;
                        }
                        else if (dsLeave.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "2")
                        {
                            isAutopost = false;
                        }

                        dsMgr = dc.getWorkTypeCode_MR("L", sf_type, div_code);
                        if (dsMgr.Tables[0].Rows.Count > 0)
                            sWorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        AutoCreateDCR_Leave(lblCurDate.Text, sWorkType, dtDCR.ToString("dd/MM/yyyy"), isAutopost, "Leave");
                        Loaddcr("1");
                    }

                    //If Autopost Holiday is true
                    if ((iHoliday == 1) && (iileave == 0))// Holiday  - to Autopost
                    {
                        //Check the current date belongs to Holiday list - by Sridevi on 06/05/15
                        iiholind = AutoPost_Holiday(sCurDate);
                        if (iiholind == 1)
                        {
                            isAutopost = true;
                            dsMgr = dc.getWorkTypeCode_MR("H", sf_type, div_code);
                            if (dsMgr.Tables[0].Rows.Count > 0)
                                sWorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                            AutoCreateDCR(lblCurDate.Text, sWorkType, dtDCR.ToString("dd/MM/yyyy"), isAutopost, "Holiday");
                            Loaddcr("1");
                        }

                    }
                    //If Autopost Holiday is true
                    if ((iWeekOff == 1) && (iiholind == 0) && (iileave == 0))
                    {
                        //Check the current date belongs to Week Off -  by Sridevi on 06/05/15
                        string wday = string.Empty;
                        wday = dtDCR.DayOfWeek.ToString();

                        int weekoffindicator = AutoPost_WeekOff(wday);

                        if (weekoffindicator == 1)
                        {
                            isAutopost = true;
                            dsMgr = dc.getWorkTypeCode_MR("W", sf_type, div_code);//Week Off
                            if (dsMgr.Tables[0].Rows.Count > 0)
                                sWorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                            AutoCreateDCR(lblCurDate.Text, sWorkType, dtDCR.ToString("dd/MM/yyyy"), isAutopost, "Week-Off");
                            Loaddcr("1");
                        }
                    }
                }
                else
                {
                    // Check for Missed Dates(Added by Sridevi - 26 Feb 2016)
                    dsSF = dc.getDCR_Apps_MissedDate(sf_code);
                    if (dsSF.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "")
                    {
                        //isReject = true;
                        ViewState["isReject"] = "delay";
                        sCurDate = dsSF.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        ViewState["scurdate"] = sCurDate;
                        dtDCR = Convert.ToDateTime(sCurDate);
                        // dtDCR = dtDCR.AddDays(1);
                        ViewState["curdate"] = dtDCR.Day.ToString() + dtDCR.Month.ToString() + dtDCR.Year.ToString();

                        lblHeader.Text = dtDCR.ToString("dd/MM/yyyy") + " - " + dtDCR.DayOfWeek.ToString();
                        lblReject.Text = "&nbsp;&nbsp; (Missed - Date)";
                        lblCurDate.Text = dtDCR.ToString("MM/dd/yyyy");

                        // Check for any leave request for the day..  
                        dsLeave = dc.getLeave(sf_code, lblCurDate.Text);
                        if (dsLeave.Tables[0].Rows.Count > 0)
                        {
                            //Create DCR For Leave
                            iileave = 1;
                            if (dsLeave.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "0")
                            {
                                isAutopost = true;
                            }
                            else if (dsLeave.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "2")
                            {
                                isAutopost = false;
                            }

                            dsMgr = dc.getWorkTypeCode_MR("L", sf_type, div_code);
                            if (dsMgr.Tables[0].Rows.Count > 0)
                                sWorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                            AutoCreateDCR_Leave(lblCurDate.Text, sWorkType, dtDCR.ToString("dd/MM/yyyy"), isAutopost, "Leave");
                            Loaddcr("1");
                        }

                        //If Autopost Holiday is true
                        if ((iHoliday == 1) && (iileave == 0))// Holiday  - to Autopost
                        {
                            //Check the current date belongs to Holiday list - by Sridevi on 06/05/15
                            iiholind = AutoPost_Holiday(sCurDate);
                            if (iiholind == 1)
                            {
                                isAutopost = true;
                                dsMgr = dc.getWorkTypeCode_MR("H", sf_type, div_code);
                                if (dsMgr.Tables[0].Rows.Count > 0)
                                    sWorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                AutoCreateDCR(lblCurDate.Text, sWorkType, dtDCR.ToString("dd/MM/yyyy"), isAutopost, "Holiday");
                                Loaddcr("1");
                            }

                        }
                        //If Autopost Holiday is true
                        if ((iWeekOff == 1) && (iiholind == 0) && (iileave == 0))
                        {
                            //Check the current date belongs to Week Off -  by Sridevi on 06/05/15
                            string wday = string.Empty;
                            wday = dtDCR.DayOfWeek.ToString();

                            int weekoffindicator = AutoPost_WeekOff(wday);

                            if (weekoffindicator == 1)
                            {
                                isAutopost = true;
                                dsMgr = dc.getWorkTypeCode_MR("W", sf_type, div_code);//Week Off
                                if (dsMgr.Tables[0].Rows.Count > 0)
                                    sWorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                AutoCreateDCR(lblCurDate.Text, sWorkType, dtDCR.ToString("dd/MM/yyyy"), isAutopost, "Week-Off");
                                Loaddcr("1");
                            }
                        }
                    }
                    else 
                    {
                        dsDCR = dc.getDCRDate(sf_code);
                        if (dsDCR.Tables[0].Rows.Count > 0)
                        {

                            tdate = DateTime.Now;
                            sCurDate = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            ViewState["scurdate"] = sCurDate;
                            ViewState["isReject"] = "false";
                            dtDCR = Convert.ToDateTime(sCurDate);
                            dmon = dtDCR.Month;
                            dyear = dtDCR.Year;

                            ViewState["curdate"] = dtDCR.Day.ToString() + dtDCR.Month.ToString() + dtDCR.Year.ToString();

                            lblHeader.Text = dtDCR.ToString("dd/MM/yyyy") + " - " + dtDCR.DayOfWeek.ToString();
                            lblCurDate.Text = dtDCR.ToString("MM/dd/yyyy");
                            // Cannot enter dcr for future date...

                            if (tdate < dtDCR)
                            {
                                PnlInfo.Visible = true;
                                lblInfo.Visible = true;
                                ddlWorkType.Enabled = false;
                                ddlSDP.Enabled = false;
                                ViewState["curdate"] = dtDCR.Day.ToString() + dtDCR.Month.ToString() + dtDCR.Year.ToString();

                                lblInfo.Text = "Cannot Enter DCR for Future Date !!!";
                            }
                            else
                            {
                                // Check for any Delay ...
                                if (iDelayInd == 1)
                                {
                                    if (dtDCR < tdate) // Delay exists
                                    {
                                        TimeSpan t = tdate - dtDCR;
                                        diffdays = (int)t.TotalDays;

                                        if (No_of_Days_Delay >= diffdays)
                                        {
                                            // do nothing
                                        }
                                        else
                                        {

                                            if (iDelayHolInd == 1)
                                            {
                                                DateTime ddate;
                                                ddate = tdate.AddDays(-No_of_Days_Delay);
                                                int cnt = 0;
                                                while (ddate < tdate)
                                                {
                                                    string delaydate = string.Empty;
                                                    delaydate = ddate.ToString();


                                                    //Check the delay date belongs to Holiday list - by Sridevi on 06/05/15
                                                    TourPlan tp1 = new TourPlan();
                                                    dsHoliday = tp1.getHolidays(state_code, div_code, delaydate);
                                                    if (dsHoliday.Tables[0].Rows.Count > 0)
                                                    {
                                                        DateTime hdate;
                                                        hdate = Convert.ToDateTime(delaydate);
                                                        delaydate = hdate.ToString("MM/dd/yyyy");
                                                        delaydate = delaydate.Replace('-', '/');
                                                        if (delaydate == dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
                                                        {
                                                            cnt = cnt + 1;
                                                        }
                                                    }

                                                    ddate = ddate.AddDays(1);
                                                }
                                                No_of_Days_Delay = No_of_Days_Delay + cnt;
                                            }
                                            // insert into dcr delay dtls table
                                            while (No_of_Days_Delay < diffdays)
                                            {
                                                int ileave = 0;
                                                int iholind = 0;
                                                int weekoffind = 0;
                                                // Check for any leave request for the day..  
                                                dsLeave = dc.getLeave(sf_code, lblCurDate.Text);
                                                if (dsLeave.Tables[0].Rows.Count > 0)
                                                {
                                                    //Create DCR For Leave
                                                    ileave = 1;
                                                    if (dsLeave.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "0")
                                                    {
                                                        isAutopost = true;
                                                    }
                                                    else if (dsLeave.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "2")
                                                    {
                                                        isAutopost = false;
                                                    }
                                                    dsMgr = dc.getWorkTypeCode_MR("L", sf_type, div_code);
                                                    if (dsMgr.Tables[0].Rows.Count > 0)
                                                        sWorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                                    AutoCreateDCR_Leave(lblCurDate.Text, sWorkType, dtDCR.ToString("dd/MM/yyyy"), isAutopost, "Leave");
                                                }
                                                if ((iHoliday == 1) && (ileave == 0))// Holiday  - to Autopost //If Autopost Holiday is true
                                                {
                                                    //Check the current date belongs to Holiday list - by Sridevi on 06/05/15
                                                    iholind = AutoPost_Holiday(sCurDate);
                                                    if (iholind == 1)
                                                    {
                                                        isAutopost = true;
                                                        dsMgr = dc.getWorkTypeCode_MR("H", sf_type, div_code);
                                                        if (dsMgr.Tables[0].Rows.Count > 0)
                                                            sWorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                                        AutoCreateDCR(lblCurDate.Text, sWorkType, dtDCR.ToString("dd/MM/yyyy"), isAutopost, "Holiday");
                                                    }
                                                }
                                                if ((iWeekOff == 1) && (iholind == 0) && (ileave == 0))
                                                {
                                                    //Check the current date belongs to Week Off -  by Sridevi on 06/05/15
                                                    string wday = string.Empty;
                                                    wday = dtDCR.DayOfWeek.ToString();

                                                    weekoffind = AutoPost_WeekOff(wday);

                                                    if (weekoffind == 1)
                                                    {
                                                        isAutopost = true;
                                                        dsMgr = dc.getWorkTypeCode_MR("W", sf_type, div_code);//Week Off
                                                        if (dsMgr.Tables[0].Rows.Count > 0)
                                                            sWorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                                        AutoCreateDCR(lblCurDate.Text, sWorkType, dtDCR.ToString("dd/MM/yyyy"), isAutopost, "Week-Off");
                                                    }

                                                }
                                                if (ileave == 0 && iholind == 0 && weekoffind == 0)
                                                {
                                                    //Create into Delay dtls table 
                                                    iReturn = dc.RecordAdd_DelayDtls(sf_code, dmon, dyear, dtDCR.ToString("MM/dd/yyyy"), dtDCR, div_code);
                                                }
                                                dtDCR = dtDCR.AddDays(1);
                                                TimeSpan dt = tdate - dtDCR;
                                                diffdays = (int)dt.TotalDays;

                                                sCurDate = dtDCR.ToString("dd/MM/yyyy");
                                                ViewState["scurdate"] = sCurDate;

                                                ViewState["curdate"] = dtDCR.Day.ToString() + dtDCR.Month.ToString() + dtDCR.Year.ToString();

                                                lblHeader.Text = dtDCR.ToString("dd/MM/yyyy") + " - " + dtDCR.DayOfWeek.ToString();
                                                lblCurDate.Text = dtDCR.ToString("MM/dd/yyyy");
                                            }

                                        }
                                    }
                                }
                                // Check for any leave request for the day..  
                                dsLeave = dc.getLeave(sf_code, lblCurDate.Text);
                                if (dsLeave.Tables[0].Rows.Count > 0)
                                {
                                    //Create DCR For Leave
                                    iileave = 1;
                                    if (dsLeave.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "0")
                                    {
                                        isAutopost = true;
                                    }
                                    else if (dsLeave.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "2")
                                    {
                                        isAutopost = false;
                                    }

                                    dsMgr = dc.getWorkTypeCode_MR("L", sf_type, div_code);
                                    if (dsMgr.Tables[0].Rows.Count > 0)
                                        sWorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                    AutoCreateDCR_Leave(lblCurDate.Text, sWorkType, dtDCR.ToString("dd/MM/yyyy"), isAutopost, "Leave");
                                    Loaddcr("1");
                                }

                                //If Autopost Holiday is true
                                if ((iHoliday == 1) && (iileave == 0))// Holiday  - to Autopost
                                {
                                    //Check the current date belongs to Holiday list - by Sridevi on 06/05/15
                                    iiholind = AutoPost_Holiday(sCurDate);
                                    if (iiholind == 1)
                                    {
                                        isAutopost = true;
                                        dsMgr = dc.getWorkTypeCode_MR("H", sf_type, div_code);
                                        if (dsMgr.Tables[0].Rows.Count > 0)
                                            sWorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                        AutoCreateDCR(lblCurDate.Text, sWorkType, dtDCR.ToString("dd/MM/yyyy"), isAutopost, "Holiday");
                                        Loaddcr("1");
                                    }

                                }
                                //If Autopost Holiday is true
                                if ((iWeekOff == 1) && (iiholind == 0) && (iileave == 0))
                                {
                                    //Check the current date belongs to Week Off -  by Sridevi on 06/05/15
                                    string wday = string.Empty;
                                    wday = dtDCR.DayOfWeek.ToString();

                                    int weekoffindicator = AutoPost_WeekOff(wday);

                                    if (weekoffindicator == 1)
                                    {
                                        isAutopost = true;
                                        dsMgr = dc.getWorkTypeCode_MR("W", sf_type, div_code);//Week Off
                                        if (dsMgr.Tables[0].Rows.Count > 0)
                                            sWorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                        AutoCreateDCR(lblCurDate.Text, sWorkType, dtDCR.ToString("dd/MM/yyyy"), isAutopost, "Week-Off");
                                        Loaddcr("1");
                                    }
                                }
                            }// end of else of future date check
                        } // end 
                    } // end of else of Missed Dates
                }// end of else of Delay release
            }//end of else of isedit
        }//end of else of isreject
    }
    private int AutoPost_Holiday(string ddate)
    {
        TourPlan tp1 = new TourPlan();
        int Holint = 0;


        dsHoliday = tp1.getHolidays(state_code, div_code, ddate);
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            DateTime hdate;
            hdate = Convert.ToDateTime(ddate);
            ddate = hdate.ToString("MM/dd/yyyy");
            ddate = ddate.Replace('-', '/');

            // Create DCR only with Header records and no detail records
            if (ddate == dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
            {
                Holint = 1;
            }
        }
        return Holint;
    }
    private int AutoPost_WeekOff(string wday)
    {
        iWeekOffind = 0;
        TourPlan tp = new TourPlan();
        dsHoliday = tp.get_WeekOff(sf_code);
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            strWeekoff = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            string[] weekoff;
            if (strWeekoff != "")
            {
                weekoff = strWeekoff.Split(',');
                foreach (string st in weekoff)
                {
                    if (st == "0")
                    {
                        if (wday == "Sunday")
                        {
                            iWeekOffind = 1;
                        }
                        //Sunday
                    }
                    if (st == "1")
                    {
                        if (wday == "Monday")
                        {
                            iWeekOffind = 1;
                        }
                        //Monday
                    }
                    if (st == "2")
                    {
                        if (wday == "Tuesday")
                        {
                            iWeekOffind = 1;
                        }
                        //Tuesday
                    }
                    if (st == "3")
                    {
                        if (wday == "Wednesday")
                        {
                            iWeekOffind = 1;
                        }
                        //Wednesday
                    }
                    if (st == "4")
                    {
                        if (wday == "Thursday")
                        {
                            iWeekOffind = 1;
                        }
                        //Thursday
                    }
                    if (st == "5")
                    {
                        if (wday == "Friday")
                        {
                            iWeekOffind = 1;
                        }
                        //Friday
                    }
                    if (st == "6")
                    {
                        if (wday == "Saturday")
                        {
                            iWeekOffind = 1;
                        }
                        //Saturday
                    }
                }
            }
            else
            {
                iWeekOffind = 0;
            }
        }
        return iWeekOffind;
    }

    private void AutoCreateDCR(string autocreatedcrdate, string sWorkType, string dcrdate, bool isAutopost,string Work_Type_Name)
    {
        // Create DCR only with Header records and no detail records
        DCR_New  dc1 = new DCR_New();
        bool delrel = false;
        bool isreentry = false;
        dsMgr = dc1.getsf_dtls(sf_code, div_code);
        if (dsMgr.Tables[0].Rows.Count > 0)
        {
            sf_name = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            emp_id = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            employee_id = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

        }
        if ((ViewState["isReject"].ToString() == "true") || (ViewState["isReject"].ToString() == "delay"))
        {
            isreentry = true;
        }
        if (ViewState["isReject"].ToString() == "delay")
        {
            delrel = true;
        }

        iReturn = dc.RecordAdd_Header(sf_code, sf_name, emp_id, employee_id, autocreatedcrdate, sWorkType, "0", "", "", "0", dcrdate, isreentry, delrel, "1", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), Work_Type_Name,sf_type,IPAdd,EntryMode);
        int iReturn_upd = dc.Update_Header(sf_code, iReturn, isreentry, dcrdate, div_code, autocreatedcrdate, delrel);
        if ((iAppNeed == 0) || (isAutopost == true))
        {
            int iretmain = dc1.Create_DCRHead_Trans(sf_code, iReturn);
        }

    }

    private void AutoCreateDCR_Leave(string autocreatedcrdate, string sWorkType, string dcrdate, bool isAutopost, string Work_Type_Name)
    {
        // Create DCR only with Header records and no detail records
        DCR_New dc1 = new DCR_New();
        bool delrel = false;
        bool isreentry = false;
        dsMgr = dc1.getsf_dtls(sf_code, div_code);
        if (dsMgr.Tables[0].Rows.Count > 0)
        {
            sf_name = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            emp_id = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            employee_id = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

        }
        if ((ViewState["isReject"].ToString() == "true") || (ViewState["isReject"].ToString() == "delay"))
        {
            isreentry = true;
        }
        if (ViewState["isReject"].ToString() == "delay")
        {
            delrel = true;
        }
        iReturn = dc1.RecordAdd_Header(sf_code, sf_name, emp_id, employee_id, autocreatedcrdate, sWorkType, "0", "", "", "0", dcrdate, isreentry, delrel, "1", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), Work_Type_Name,sf_type, IPAdd, EntryMode);
        int iReturn_upd = dc1.Update_Header(sf_code, iReturn, isreentry, dcrdate, div_code, autocreatedcrdate, delrel);
        if (isAutopost == true)
        {
            int iretmain = dc1.Create_DCRHead_Trans(sf_code, iReturn);
        }

    }

    private void FillWorkType(string sf_type)
    {
        DCR_New dc = new DCR_New();

        dsDCR = dc.getWorkType_MRDCR(div_code,sf_type);
        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            ddlWorkType.DataTextField = "Worktype_Name_B";
            ddlWorkType.DataValueField = "WorkType_Code_B";
            ddlWorkType.DataSource = dsDCR;
            ddlWorkType.DataBind();
            //FillColor();
        }
    }

    private void FillColor()
    {

        //Added the color in work type dropdown without going to server by Sridevi on 09/16/15
        if (ViewState["dtWorkType"] != null)
        {
            dtWorkType = (DataTable)ViewState["dtWorkType"];
            foreach (DataRow dataWTRow in dtWorkType.Rows)
            {
                int wt_sno = 0;
                ddlWorkType.BackColor = Color.White;
                foreach (ListItem lstItem in ddlWorkType.Items)
                {
                    if (lstItem.Value.Trim() == dataWTRow["wt_code"].ToString().Trim())
                        ddlWorkType.Items[wt_sno].Attributes.Add("style", "background-color:Yellow");

                    wt_sno = wt_sno + 1;
                }
            }
        }
        else
        {
            //Added a datarow for dtWorkType by Sridevi on 09/16/15
            //dtWorkType.Rows.Clear();
            //dtWorkType = null;
            int j = 0;
            foreach (ListItem ColorItems in ddlWorkType.Items)
            {
                DCR_New  WT = new DCR_New();
                dsWT = WT.DCR_get_WorkType(div_code, ColorItems.Value.ToString(), sf_type);
                if (dsWT.Tables[0].Rows.Count > 0)
                {
                    FieldWork_Ind = dsWT.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    if (FieldWork_Ind == "F")
                    {
                        ddlWorkType.Items[j].Attributes.Add("style", "background-color: Yellow");
                        //Added a datarow for dtWorkType by Sridevi on 09/16/15
                        DataRow dtrow = dtWorkType.NewRow();                       // Create New Row
                        dtrow["wt_code"] = ColorItems.Value.ToString();           // Bind Data to Columns                        
                        dtWorkType.Rows.Add(dtrow);
                    }
                }
                j = j + 1;
            }
            //Added a datarow for dtWorkType by Sridevi on 09/16/15
            ViewState["dtWorkType"] = dtWorkType;
        }
    }

    private void FillSDP()
    {
        DCR_New dc = new DCR_New();
        dsDCR = dc.getSDP(sf_code);
        if (dsDCR.Tables[0].Rows.Count == 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Territory must be created prior to DCR');window.location='../Territory/TerritoryCreation.aspx';", true);
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Territory must be created prior to DCR');window.location='../Territory/TerritoryCreation.aspx';</script>");
            //Response.Redirect("../Territory/TerritoryCreation.aspx");
            //// menu1.Status = "Territory must be created prior to Doctor creation";
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Territory must be created prior to DCR');</script>");
        }
        else
        {
            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                ddlSDP.DataTextField = "Territory_Name";
                ddlSDP.DataValueField = "Territory_Code";
                ddlSDP.DataSource = dsDCR;
                ddlSDP.DataBind();


                ddlTerr.DataTextField = "Territory_Name";
                ddlTerr.DataValueField = "Territory_Code";
                ddlTerr.DataSource = dsDCR;
                ddlTerr.DataBind();


                ddlTerr_Un.DataTextField = "Territory_Name";
                ddlTerr_Un.DataValueField = "Territory_Code";
                ddlTerr_Un.DataSource = dsDCR;
                ddlTerr_Un.DataBind();

            }
        }
    }

    private void FillAllTerr()
    {
        DCR_New dc = new DCR_New();
        dsDCR = dc.getAllTerr(sf_code, lblCurDate.Text);

        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            ddlTerr.DataTextField = "Territory_Name";
            ddlTerr.DataValueField = "Territory_Code";
            ddlTerr.DataSource = dsDCR;
            ddlTerr.DataBind();

            ddlAllTerr.DataTextField = "Territory_Name";
            ddlAllTerr.DataValueField = "Territory_Code";
            ddlAllTerr.DataSource = dsDCR;
            ddlAllTerr.DataBind();

            ddlsfterr.DataTextField = "SF_Code";
            ddlsfterr.DataValueField = "Territory_Code";
            ddlsfterr.DataSource = dsDCR;
            ddlsfterr.DataBind();

            ddlTerr_Un.DataTextField = "Territory_Name";
            ddlTerr_Un.DataValueField = "Territory_Code";
            ddlTerr_Un.DataSource = dsDCR;
            ddlTerr_Un.DataBind();

            ViewState["allterr"] = dsDCR;
        }
        else
        {
            ViewState["allterr"] = null;
        }
    }

    private void FillTerr(string sf_code)
    {
        DCR_New dc = new DCR_New();
        dsDCR = dc.getSDP(sf_code);

        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            ddlTerr.DataTextField = "Territory_Name";
            ddlTerr.DataValueField = "Territory_Code";
            ddlTerr.DataSource = dsDCR;
            ddlTerr.DataBind();
        }
    }
    private void FillTerrUn(string sf_code)
    {
        DCR_New dc = new DCR_New();
        dsDCR = dc.getSDP(sf_code);

        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            ddlTerr_Un.DataTextField = "Territory_Name";
            ddlTerr_Un.DataValueField = "Territory_Code";
            ddlTerr_Un.DataSource = dsDCR;
            ddlTerr_Un.DataBind();
        }

    }
    private void LoadFF()
    {
        DCR_New dc = new DCR_New();
        dsDCR = dc.getMR(sf_code, lblCurDate.Text);

        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            ddlcheMR.DataTextField = "sf_name";
            ddlcheMR.DataValueField = "sf_code";
            ddlcheMR.DataSource = dsDCR;
            ddlcheMR.DataBind();


            ddlUnMR.DataTextField = "sf_name";
            ddlUnMR.DataValueField = "sf_code";
            ddlUnMR.DataSource = dsDCR;
            ddlUnMR.DataBind();
        }
    }
    private void LoadSF()
    {
        DCR_New dc = new DCR_New();
        DataSet dsmgrsf = new DataSet();
        if (sf_type == "1")
        {
            //   dsDCR = dc.LoadWorkwith(sf_code);
            SalesForce ds = new SalesForce();
            DataTable dt = ds.getMRJointWork(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsDCR = dsmgrsf;

        }
        else if (sf_type == "2")
        {
            SalesForce ds = new SalesForce();
            DataTable dt = ds.getManagerJointWork(div_code, sf_code, 0, Session["mcurdate"].ToString(), sf_code);
            dsmgrsf.Tables.Add(dt);
            dsDCR = dsmgrsf;
        }
        chkFieldForce.Items.Clear();
        chkChemWW.Items.Clear();
        chkUnLstDR.Items.Clear();
        StkChkBox.Items.Clear();
        ChkChem.Items.Clear();
        ChkUn.Items.Clear();
        ChkHos.Items.Clear();
        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dataRow in dsDCR.Tables[0].Rows)
            {
                ListItem liTerr = new ListItem();
                liTerr.Value = dataRow["sf_code"].ToString();
                liTerr.Text = dataRow["sf_name"].ToString();

                //chkFieldForce.Items.Add(dataRow["sf_name"].ToString());
                chkFieldForce.Items.Add(liTerr);
                chkChemWW.Items.Add(liTerr);
                chkUnLstDR.Items.Add(liTerr);
                StkChkBox.Items.Add(liTerr);
                ChkChem.Items.Add(liTerr);
                ChkUn.Items.Add(liTerr);
                ChkHos.Items.Add(liTerr);
            }
        }
       
        chkFieldForce.Items[0].Selected = true;
        chkChemWW.Items[0].Selected = true;
        chkUnLstDR.Items[0].Selected = true;
        StkChkBox.Items[0].Selected = true;
        ChkChem.Items[0].Selected = true;
        ChkUn.Items[0].Selected = true;
        ChkHos.Items[0].Selected = true;

    }
    private void clearww()
    {
        foreach (ListItem item in chkFieldForce.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in chkChemWW.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in chkUnLstDR.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in StkChkBox.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in ChkChem.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in ChkUn.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in ChkHos.Items)
        {
            item.Selected = false;
        }
        chkFieldForce.Items[0].Selected = true;
        chkChemWW.Items[0].Selected = true;
        chkUnLstDR.Items[0].Selected = true;
        StkChkBox.Items[0].Selected = true;
        ChkChem.Items[0].Selected = true;
        ChkUn.Items[0].Selected = true;
        ChkHos.Items[0].Selected = true;

    }
    //Added this function for Work Type selection from the dropdown by Sridevi on 09/16/15
    private DataTable WorkType_Selection(string work_type)
    {
        DataTable dtWT = null;
        if (ViewState["dsWorkTypeSettings"] != null)
        {
            dsWorkTypeSettings = (DataSet)ViewState["dsWorkTypeSettings"];
            if (dsWorkTypeSettings.Tables[0].Rows.Count > 0)
            {
                //Filter the Dataset based on the Work Type selected from the dropdown by Sridevi on 09/16/15
                dsWorkTypeSettings.Tables[0].DefaultView.RowFilter = "WorkType_Code = '" + work_type + "' ";
                DataView dvWTSettings = dsWorkTypeSettings.Tables[0].DefaultView;
                dtWT = dvWTSettings.ToTable();
            }
        }

        return dtWT;
    }
   
   
    private void Create_Head(string sdp)
    {
        //Creating Header
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Header.xml";
        dshead = (DataSet)ViewState["Header"];

        if (dshead != null && dshead.HasChanges())
        {
            if (dshead.Tables[0].Rows.Count > 0)
            {
                dshead.Tables[0].Rows[0]["worktype"] = ddlWorkType.SelectedValue.ToString();
                dshead.Tables[0].Rows[0]["sdp"] = sdp;
                dshead.Tables[0].Rows[0]["remarks"] = txtRemarkDesc.Text;
                dshead.WriteXml(Server.MapPath(sFile));
                ViewState["Header"] = (DataSet)dshead;
            }
        }
        else
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Server.MapPath(sFile));

            XmlElement parentelement = xmldoc.CreateElement("DCR");

            XmlElement xmlworktype = xmldoc.CreateElement("worktype");
            XmlElement xmlsdp = xmldoc.CreateElement("sdp");
            XmlElement xmlrem = xmldoc.CreateElement("remarks");
            XmlElement xmldate = xmldoc.CreateElement("date");

            xmlworktype.InnerText = ddlWorkType.SelectedValue.ToString();
            xmlsdp.InnerText = sdp;
            xmlrem.InnerText = txtRemarkDesc.Text;
            xmldate.InnerText = DateTime.Now.ToString();

            parentelement.AppendChild(xmlworktype);
            parentelement.AppendChild(xmlsdp);
            parentelement.AppendChild(xmlrem);
            parentelement.AppendChild(xmldate);

            xmldoc.DocumentElement.AppendChild(parentelement);

            xmldoc.Save(Server.MapPath(sFile));
            Bind_Header();
        }
    }
    //protected void ddlGift_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtGift.Text = "1";
    //}

    //protected void ddlUnLstDR_Gift_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtUnLstDR_GQty.Text = "1";
    //}

    private void FillMgrDocColor()
    {
        foreach (ListItem lstItem in lstListDR.Items)
        {
            dsDCR = dc.getdoctercolor(sf_code, lblCurDate.Text, lstItem.Value.ToString());
            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                string str = dsDCR.Tables[0].Rows[0][0].ToString();
                lstItem.Attributes.Add("style", "background-color:" + str + "");
            }
        }
    }
    private void FillDocColor()
    {
        int i;
        dsDCR = dc.getTerrListedDoctor(sf_code, ddlSDP.SelectedValue.ToString());
        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dataRow in dsDCR.Tables[0].Rows)
            {
                ListItem liTerr = new ListItem();
                liTerr.Value = dataRow["ListedDrCode"].ToString();
                liTerr.Text = dataRow["ListedDr_Name"].ToString();

                //chkFieldForce.Items.Add(dataRow["sf_name"].ToString());
                //chkFieldForce.Items.Add(liTerr);
                i = 0;
                lstListDR.BackColor = Color.White;
                foreach (ListItem lstItem in lstListDR.Items)
                {
                    if (lstItem.Value == liTerr.Value)
                    {
                        lstListDR.Items[i].Attributes.Add("style", "background-color:LightGreen");
                    }

                    i = i + 1;
                }
            }
        }
    }

    private void FillProd()
    {
        DCR_New dc = new DCR_New();
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSfDivision(sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            string value = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            string[] strDivSplit = value.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    //if (sf_type == "1")
                    //{
                       dsDCRProd = dc.getProducts(sf_code, strdiv.ToString());
                    //}
                    //else
                    //{
                    //    dsDCRProd = dc.getProducts_MGR(sf_code, lblCurDate.Text);
                    //}

                    if (dsDCRProd.Tables[0].Rows.Count > 0)
                    {
                        ddlProd1.DataTextField = "Product_Detail_Name";
                        ddlProd1.DataValueField = "Product_Detail_Code";
                        ddlProd1.DataSource = dsDCRProd;
                        ddlProd1.DataBind();

                        ddlProd2.DataTextField = "Product_Detail_Name";
                        ddlProd2.DataValueField = "Product_Detail_Code";
                        ddlProd2.DataSource = dsDCRProd;
                        ddlProd2.DataBind();

                        ddlProd3.DataTextField = "Product_Detail_Name";
                        ddlProd3.DataValueField = "Product_Detail_Code";
                        ddlProd3.DataSource = dsDCRProd;
                        ddlProd3.DataBind();


                        // Un-Listed Doctor - Product

                        ddlUnLstDR_Prod1.DataTextField = "Product_Detail_Name";
                        ddlUnLstDR_Prod1.DataValueField = "Product_Detail_Code";
                        ddlUnLstDR_Prod1.DataSource = dsDCRProd;
                        ddlUnLstDR_Prod1.DataBind();

                        ddlUnLstDR_Prod2.DataTextField = "Product_Detail_Name";
                        ddlUnLstDR_Prod2.DataValueField = "Product_Detail_Code";
                        ddlUnLstDR_Prod2.DataSource = dsDCRProd;
                        ddlUnLstDR_Prod2.DataBind();

                        ddlUnLstDR_Prod3.DataTextField = "Product_Detail_Name";
                        ddlUnLstDR_Prod3.DataValueField = "Product_Detail_Code";
                        ddlUnLstDR_Prod3.DataSource = dsDCRProd;
                        ddlUnLstDR_Prod3.DataBind();

                        //unlisted new

                        ddlN_unProd1.DataTextField = "Product_Detail_Name";
                        ddlN_unProd1.DataValueField = "Product_Detail_Code";
                        ddlN_unProd1.DataSource = dsDCRProd;
                        ddlN_unProd1.DataBind();

                        ddlN_unProd2.DataTextField = "Product_Detail_Name";
                        ddlN_unProd2.DataValueField = "Product_Detail_Code";
                        ddlN_unProd2.DataSource = dsDCRProd;
                        ddlN_unProd2.DataBind();

                        ddlN_unProd3.DataTextField = "Product_Detail_Name";
                        ddlN_unProd3.DataValueField = "Product_Detail_Code";
                        ddlN_unProd3.DataSource = dsDCRProd;
                        ddlN_unProd3.DataBind();

                        ViewState["Prod"] = dsDCRProd;
                    }
                }
            }
        }
    }


    protected void FillCategory()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchCategory(sf_code);
        ddlCate_Un.DataTextField = "Doc_Cat_Name";
        ddlCate_Un.DataValueField = "Doc_Cat_Code";
        ddlCate_Un.DataSource = dsListedDR;
        ddlCate_Un.DataBind();
    }

    protected void FillSpeciality()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchSpeciality(sf_code);
        ddlSpec_Un.DataTextField = "Doc_Special_Name";
        ddlSpec_Un.DataValueField = "Doc_Special_Code";
        ddlSpec_Un.DataSource = dsListedDR;
        ddlSpec_Un.DataBind();
    }

    protected void FillClass()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchClass(sf_code);
        ddlClass_Un.DataTextField = "Doc_ClsName";
        ddlClass_Un.DataValueField = "Doc_ClsCode";
        ddlClass_Un.DataSource = dsListedDR;
        ddlClass_Un.DataBind();
    }
    protected void FillQualification()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchQualification(sf_code);
        ddlQual_Un.DataTextField = "Doc_QuaName";
        ddlQual_Un.DataValueField = "Doc_QuaCode";
        ddlQual_Un.DataSource = dsListedDR;
        ddlQual_Un.DataBind();
    }
    private void FillGift()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSfDivision(sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            string value = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            string[] strDivSplit = value.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    DCR_New dc = new DCR_New();
                    //if (sf_type == "1")
                    //{
                        dsDCRGift = dc.getGift(sf_code, strdiv.ToString());
                    //}
                    //else
                    //{
                    //    dsDCRGift = dc.getGift_MGR(sf_code, lblCurDate.Text);
                    //}

                    if (dsDCRGift.Tables[0].Rows.Count > 0)
                    {
                        ddlGift.DataTextField = "Gift_Name";
                        ddlGift.DataValueField = "Gift_Code";
                        ddlGift.DataSource = dsDCRGift;
                        ddlGift.DataBind();


                        ddlUnLstDR_Gift.DataTextField = "Gift_Name";
                        ddlUnLstDR_Gift.DataValueField = "Gift_Code";
                        ddlUnLstDR_Gift.DataSource = dsDCRGift;
                        ddlUnLstDR_Gift.DataBind();


                        ddlN_ungift.DataTextField = "Gift_Name";
                        ddlN_ungift.DataValueField = "Gift_Code";
                        ddlN_ungift.DataSource = dsDCRGift;
                        ddlN_ungift.DataBind();


                        ViewState["Gift"] = dsDCRGift;
                    }
                }
            }
        }
    }

    protected void FillChem()
    {
        DCR_New dc = new DCR_New();
        DataSet dsDCR_dup_che = null;
        if (sf_type == "1")
        {
            //dsDCRChe = dc.getChemists(sf_code);
            dsDCRChe = dc.getTerrChemists_New(sf_code);
            dsDCR_dup_che = dc.getTerrChemists_New_dup(sf_code);
        }
        else
        {
            //dsDCRChe = dc.getChemists(sf_code, lblCurDate.Text);
            dsDCRChe = dc.getChemists_New(sf_code, lblCurDate.Text);
            mgrchelist.DataTextField = "color_code";
            mgrchelist.DataValueField = "Chemists_Code";
            mgrchelist.DataSource = dsDCRChe;
            mgrchelist.DataBind();
        }
        if (dsDCRChe.Tables[0].Rows.Count > 0)
        {
            //ddlChemists.DataTextField = "Chemists_Name";
            //ddlChemists.DataValueField = "Chemists_Code";
            //ddlChemists.DataSource = dsDCRChe;
            //ddlChemists.DataBind();

            lstChe.DataTextField = "Chemists_Name";
            lstChe.DataValueField = "Chemists_Code";
            lstChe.DataSource = dsDCRChe;
            lstChe.DataBind();

            ddl_terr_che.DataTextField = "Territory_Code";
            ddl_terr_che.DataValueField = "Chemists_Code";
            ddl_terr_che.DataSource = dsDCR_dup_che;
            ddl_terr_che.DataBind();

            ddlchelist.DataTextField = "Chemists_Name";
            ddlchelist.DataValueField = "Chemists_Code";
            ddlchelist.DataSource = dsDCR_dup_che;
            ddlchelist.DataBind();

          
            
            ViewState["Che"] = dsDCRChe;
        }
    }

    private void FillStockiest()
    {
        DCR_New dc = new DCR_New();
        if (sf_type == "1")
        {
            dsDCRStk = dc.getStockiest(sf_code,div_code);
        }
        else
        {
            //dsDCRStk = dc.getStockiest(sf_code, lblCurDate.Text);
            dsDCRStk = dc.getStockist_New(sf_code, lblCurDate.Text);
        }
        if (dsDCRStk.Tables[0].Rows.Count > 0)
        {
            StkDDL.DataTextField = "Stockist_Name";
            StkDDL.DataValueField = "Stockist_Code";
            StkDDL.DataSource = dsDCRStk;
            StkDDL.DataBind();

            //ViewState["Stk"] = dsDCRStk;

        }

    }

    private void FillHospital()
    {
        DCR_New dc = new DCR_New();
        if (sf_type == "1")
        {
            dsDCRHos = dc.getHospital(sf_code);
        }
        else
        {
            dsDCRHos = dc.getHospital(sf_code, lblCurDate.Text);
        }
        if (dsDCRHos.Tables[0].Rows.Count > 0)
        {
            HosDDL.DataTextField = "Hospital_Name";
            HosDDL.DataValueField = "Hospital_Code";
            HosDDL.DataSource = dsDCRHos;
            HosDDL.DataBind();

            //ViewState["Hos"] = dsDCRHos;
        }
    }

    private void FillListedDoctor()
    {
        DCR_New dc = new DCR_New();
        DataSet dsDCR_dup = null;
        //dsDCR = dc.getTerrListedDoctor(sf_code,ddlSDP.SelectedValue.ToString());
        if (sf_type == "1")
        {
            dsDCR = dc.getTerrListedDoctor_New(sf_code, doc_disp);

            dsDCR_dup = dc.getTerrListedDoctor_New_DupSet(sf_code, doc_disp);
            //FillDocColor();
        }
        else
        {
           // dsDCR = dc.getTerrListedDoctor_Mgr(sf_code, doc_disp, lblCurDate.Text);
            dsDCR = dc.getTerrListedDoctor_Mgr_New(sf_code, doc_disp, lblCurDate.Text);
            mgrdoclist.DataTextField = "color_code";
            mgrdoclist.DataValueField = "ListedDrCode";
            mgrdoclist.DataSource = dsDCR;
            mgrdoclist.DataBind();
        }
        if (dsDCR.Tables[0].Rows.Count == 0)
        {
            if (sf_type == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Doctor must be created prior to DCR');window.location='../ListedDoctor/ListedDRCreation.aspx';", true);
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Doctor must be created prior to DCR');", true);
            }
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Doctor must be created prior to DCR');window.location='../ListedDoctor/ListedDRCreation.aspx';</script>");
            //Response.Redirect("../Territory/TerritoryCreation.aspx");
            //// menu1.Status = "Territory must be created prior to Doctor creation";
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Territory must be created prior to DCR');</script>");
        }
        else
        {

            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                lstListDR.DataTextField = "ListedDr_Name";
                lstListDR.DataValueField = "ListedDrCode";
                lstListDR.DataSource = dsDCR;
                lstListDR.DataBind();

                ddl_terr_doc.DataTextField = "Territory_Code";
                ddl_terr_doc.DataValueField = "ListedDrCode";
                ddl_terr_doc.DataSource = dsDCR_dup;
                ddl_terr_doc.DataBind();

                ddldoclist.DataTextField = "ListedDr_Name";
                ddldoclist.DataValueField = "ListedDrCode";
                ddldoclist.DataSource = dsDCR_dup;
                ddldoclist.DataBind();

               
                
                ViewState["lstdoc"] = dsDCR;
               
            }
        }
        //if (sf_type == "2")
        //    FillMgrDocColor();
    }

    private void FillUnListedDoctor()
    {
        DCR_New dc = new DCR_New();
        DataSet dsDCRUnLst_dup = null;

        if (sf_type == "1")
        {
            // dsDCRUnLst = dc.getTerrUnListedDoctor(sf_code);
            //dsDCRUnLst = dc.getTerrUnListedDoctorSrc(sf_code, ddlSDP.SelectedValue.ToString());
            dsDCRUnLst = dc.getTerrUnListedDoctorSrc_New(sf_code);
            dsDCRUnLst_dup = dc.getTerrUnListedDoctorSrc_New_dup(sf_code);
        }
        else
        {
            //dsDCRUnLst = dc.getTerrUnListedDoctor_MgrNew(sf_code, lblCurDate.Text);
            dsDCRUnLst = dc.getTerrUnListedDoctor_MgrNew_proc(sf_code, lblCurDate.Text);
            mgrundoclist.DataTextField = "color_code";
            mgrundoclist.DataValueField = "UnListedDrCode";
            mgrundoclist.DataSource = dsDCRUnLst;
            mgrundoclist.DataBind();
        }
        if (dsDCRUnLst.Tables[0].Rows.Count > 0)
        {
            UnLstDr.DataTextField = "UnListedDr_Name";
            UnLstDr.DataValueField = "UnListedDrCode";
            UnLstDr.DataSource = dsDCRUnLst;
            UnLstDr.DataBind();

            ddl_terr_Undoc.DataTextField = "Territory_Code";
            ddl_terr_Undoc.DataValueField = "UnListedDrCode";
            ddl_terr_Undoc.DataSource = dsDCRUnLst_dup;
            ddl_terr_Undoc.DataBind();

            ddlundoclist.DataTextField = "UnListedDr_Name";
            ddlundoclist.DataValueField = "UnListedDrCode";
            ddlundoclist.DataSource = dsDCRUnLst_dup;
            ddlundoclist.DataBind();

          
         

            ViewState["undoc"] = dsDCRUnLst; 
           }
    }


    protected DataSet Populate_Products()
    {

        return ViewState["Prod"] as DataSet;

    }

    protected DataSet Populate_Gift()
    {

        return ViewState["Gift"] as DataSet;

    }



    protected void btnGo_Click(object sender, EventArgs e)
    {
        //if (ViewState["LDEdit"].ToString() == "")
        //{
        //    txtRemarks.Text = "";
        //}
        //if (remarks_dcr == 1)
        //{         
        //    pnlRemarks.Visible = true;
        //    pnlMultiView.Enabled = false;
        //    pnlMultiView.Enabled = false;
        //    pnlTab.Enabled = false;
        //    pnlTop.Enabled = false;
        //    pnlTab1.Enabled = false;

        //}
        //else
        //{
           
        //}        

            PopulateDCR();
    }
    protected void Bind_Header()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        //ds.ReadXml(Server.MapPath("Stockiest.xml"));

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Header.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            
                //Start writer
                XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);


                //Start XM DOcument
                dr_writer.WriteStartDocument(true);
                dr_writer.Formatting = Formatting.Indented;
                dr_writer.Indentation = 2;

                //ROOT Element
                dr_writer.WriteStartElement("DCR");
                dr_writer.WriteEndElement();
                //End XML Document
                dr_writer.WriteEndDocument();
                //Close writer
                dr_writer.Close();
        }     
        try
        {
            ds.ReadXml(Server.MapPath(sFile));
           
            if (ds != null && ds.HasChanges())
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlWorkType.SelectedValue = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    if (ddlWorkType.SelectedValue != "0")
                        hdnwtvalue.Value = "1";
                    ddlSDP.SelectedValue = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtRemarkDesc.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    lblInfo.Text = "";
                    headdate = Convert.ToDateTime(ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
                    DCR_New dc = new DCR_New();
                    chk = dc.chkxml(sf_type, headdate, div_code, sf_code);
                    ViewState["Header"] = (DataSet)ds;
                }
            }
            else
            {
                //ddlWorkType.SelectedValue = "0";
                //ddlSDP.SelectedValue = "0";
                ViewState["Header"] = (DataSet)ds;
            }
            
        }
        catch(Exception ex)
        {
            if (ex.Message.ToString() == "Root element is missing.")
            {
               ClearXML();
            }
            else
                throw ex;
        }
    }

    protected void BindGrid(string mode)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        //ds.ReadXml(Server.MapPath("DailCalls.xml"));

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_ListedDR.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }
        try
        {
            ds.ReadXml(Server.MapPath(sFile));
            if (ds != null && ds.HasChanges())
            {
                if (ddlWorkType.SelectedItem.Text == "Field Work")
                {
                    ddlWorkType.Enabled = false;
                }
                ViewState["Count"] = "1";
                gvDCR.DataSource = ds;
                gvDCR.DataBind();
            }
            else
            {
                gvDCR.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.ToString() == "Root element is missing.")
            {
                ClearXML();
            }
            else
                throw ex;
        }
        if (mode == "0")
        {
            if (hdndocedit.Value == "")
            {
               // ddlSes.SelectedIndex = 0;
                ddlTime.SelectedIndex = 0;
                ddlmin.SelectedIndex = 0;
                ddlProd1.SelectedIndex = 0;
                ddlProd2.SelectedIndex = 0;
                ddlProd3.SelectedIndex = 0;
                txtProd1.Text = "";
                txtProd2.Text = "";
                txtProd3.Text = "";
                txtListDR.Text = "";
                lstListDR.SelectedIndex = -1;
                ddlGift.SelectedIndex = 0;
                txtGift.Text = "";
            }
        }      
    }

    protected void BindGrid_Stockiest(string mode)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        //ds.ReadXml(Server.MapPath("Stockiest.xml"));

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Stockiest.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }
        try
        {
            ds.ReadXml(Server.MapPath(sFile));

            if (ds != null && ds.HasChanges())
            {
                if (ddlWorkType.SelectedItem.Text == "Field Work")
                {
                    ddlWorkType.Enabled = false;
                }
                ViewState["Count"] = "1";
                GridStk.DataSource = ds;
                GridStk.DataBind();
            }
            else
            {
                GridStk.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.ToString() == "Root element is missing.")
            {
                ClearXML();
            }
            else
                throw ex;
        }
        if (mode == "0")
        {
            if (hdnstkedit.Value == "")
           {
                StkPOB.Text = "";
                StkDDL.SelectedIndex = 0;
                StkVisitType.SelectedIndex = 0;
            }
        }
    }

    protected void BindGrid_Hospital(string mode)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        //ds.ReadXml(Server.MapPath("Hospital.xml"));
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Hospital.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }
        try
        {
            ds.ReadXml(Server.MapPath(sFile));

            if (ds != null && ds.HasChanges())
            {
                if (ddlWorkType.SelectedItem.Text == "Field Work")
                {
                    ddlWorkType.Enabled = false;
                }
                ViewState["Count"] = "1";
                GridHospital.DataSource = ds;
                GridHospital.DataBind();
                //  grdPreview_Hosp.DataSource = ds;
                //grdPreview_Hosp.DataBind();
            }
            else
            {
                GridHospital.DataBind();
                //grdPreview_Hosp.DataBind();
                //Label12.Text = "No entry available";
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.ToString() == "Root element is missing.")
            {
                ClearXML();
            }
            else
                throw ex;
        }
        if (mode == "0")
        {
            if (hdnhosedit.Value == "")
            {
                HosDDL.SelectedIndex = 0;
                txtHospPOB.Text = "";
            }
        }
    }

    protected void BindGrid_UnListedDR(string mode)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        //ds.ReadXml(Server.MapPath("UnLstDR.xml"));
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "UnLstDR.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }
        try
        {
            ds.ReadXml(Server.MapPath(sFile));
            if (ds != null && ds.HasChanges())
            {
                if (ddlWorkType.SelectedItem.Text == "Field Work")
                {
                    ddlWorkType.Enabled = false;
                }
                ViewState["Count"] = "1";
                grdUnLstDR.DataSource = ds;
                grdUnLstDR.DataBind();
                //  grdPreview_UnLstDoc.DataSource = ds;
                //grdPreview_UnLstDoc.DataBind();
            }
            else
            {
                grdUnLstDR.DataBind();
                //grdPreview_UnLstDoc.DataBind();
                //Label10.Text = "No entry available";
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.ToString() == "Root element is missing.")
            {
                ClearXML();
            }
            else
                throw ex;
        }
        if (mode == "0")
        {
            if (hdnundocedit.Value == "")
            {
               // ddlUnLstDR_Session.SelectedIndex = 0;
                ddlMinute.SelectedIndex = 0;
                ddlSec.SelectedIndex = 0;
                ddlUnLstDR_Prod1.SelectedIndex = 0;
                ddlUnLstDR_Prod2.SelectedIndex = 0;
                ddlUnLstDR_Prod3.SelectedIndex = 0;
                ddlUnLstDR_Gift.SelectedIndex = 0;
                UnLstDr.SelectedIndex = -1;
                txtUnLstDR_Prod_Qty1.Text = "";
                txtUnLstDR_Prod_Qty2.Text = "";
                txtUnLstDR_Prod_Qty3.Text = "";
                txtUnLstDR_GQty.Text = "";

                UnLstTxtDR.Text = "";
                UnLstDr.SelectedIndex = -1;
                Untxt_Dr.Text = "";
            }
        }
    }


    protected void   btnChemGo_Click(object sender, EventArgs e)
    {
       CreateChemist("0");
    }

    protected void BindGrid_Chem(string mode)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Chem.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }
        try
        {
            ds.ReadXml(Server.MapPath(sFile));

            //ds.ReadXml(Server.MapPath("Chem_DCR.xml"));
            if (ds != null && ds.HasChanges())
            {
                if (ddlWorkType.SelectedItem.Text == "Field Work")
                {
                    ddlWorkType.Enabled = false;
                }
                ViewState["Count"] = "1";
                grdChem.DataSource = ds;
                grdChem.DataBind();
            }
            else
            {
                grdChem.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.ToString() == "Root element is missing.")
            {
                ClearXML();
            }
            else
                throw ex;
        }
        if (mode == "0")
        {
           //if (ViewState["CheEdit"].ToString() == "")
            if (hdncheedit.Value == "")
            {
                lstChe.SelectedIndex = -1;
                txtPOBNo.Text = "";
                txtPnlChe.Text = "";
                txtPChe.Text = "";
            }
        }
    }

    //protected void lnkEdit_Click(object sender, CommandEventArgs e)
    //{
    //    var lb = (LinkButton)sender;
    //    var row = (GridViewRow)lb.NamingContainer;
    //    if (row != null)
    //    {
    //        row.BackColor = Color.LimeGreen;
    //        //row.Enabled = false;
    //        grdChem.Enabled = false;
    //        Label lblChem_Code = row.FindControl("lblChem_Code") as Label;
    //        Label lblChem_Name = row.FindControl("lblChemists") as Label;
    //        Label lblPOBNo = row.FindControl("lblPOBNo") as Label;
    //        Label lblWW = row.FindControl("lblWW") as Label;
    //        Label lblTerr_Code = row.FindControl("lblTerr_Code") as Label;
    //        Label lblnew = row.FindControl("lblnew") as Label;
    //        Label lblsf_code = row.FindControl("lblsf_code") as Label;

    //        if (lblnew.Text.Trim() != "New")
    //        {
    //            if (lblChem_Code.Text != "")
    //            {
    //                txtPChe.Text = lblChem_Code.Text.Trim();
    //                lstChe.SelectedValue = txtPChe.Text.Trim();
    //            }
    //            else
    //            {
    //                lstChe.SelectedIndex = -1;
    //            }
    //            if (lblChem_Name.Text != "")
    //            {
    //                txtPnlChe.Text = lblChem_Name.Text.Trim();
    //                lstChe.SelectedItem.Text = txtPnlChe.Text.Trim();
    //            }
    //            else
    //            {
    //                lstChe.SelectedIndex = -1;
    //            }


    //            txtPOBNo.Text = lblPOBNo.Text.Trim();
    //            txtChemWW.Text = lblWW.Text.Trim();
    //            ddlSDP.SelectedValue = lblTerr_Code.Text.Trim();

    //            int iWW = 0;
    //            if (txtChemWW.Text.IndexOf(",") != -1)
    //            {
    //                string[] strWWSplit = txtChemWW.Text.Split(',');
    //                chkChemWW.ClearSelection();
    //                foreach (string strWW in strWWSplit)
    //                {
    //                    if (strWW != "")
    //                    {
    //                        iWW = iWW + 1;

    //                        for (int i = 0; i < chkChemWW.Items.Count; i++)
    //                        {
    //                            if (chkChemWW.Items[i].Text.Trim().Length >= 9)
    //                            {
    //                                if (strWW.Trim() == chkChemWW.Items[i].Text.Substring(0, 9).Trim())
    //                                    chkChemWW.Items[i].Selected = true;
    //                            }
    //                            else
    //                            {
    //                                if (strWW.Trim() == chkChemWW.Items[i].Text)
    //                                    chkChemWW.Items[i].Selected = true;
    //                            }
    //                        }
    //                    }
    //                }
    //            }

    //            txtChemWW.Text = iWW.ToString();
    //            ViewState["CheEdit"] = row.RowIndex;
    //        }
    //        else
    //        {
    //            PnlChem.Attributes.Add("style", "display:block");
    //            pnlMultiView.Enabled = false;
    //            pnlMultiView.Enabled = false;
    //            pnlTab.Enabled = false;
    //            pnlTop.Enabled = false;
    //            pnlTab1.Enabled = false;
    //            NewtxtChem.Text = lblChem_Name.Text;
    //            txtChemNPOB.Text = lblPOBNo.Text;
    //            ddlcheMR.SelectedValue = lblsf_code.Text.Trim();
    //            ddlTerr.SelectedValue = lblTerr_Code.Text.Trim();
    //            ViewState["CheEdit"] = row.RowIndex;
    //        }
    //    }
    //    checolor();
    //}


    protected void grdChem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        checolor();
        BindGrid_Chem("0");
        DataSet ds = grdChem.DataSource as DataSet;
        ds.Tables[0].Rows[grdChem.Rows[e.RowIndex].DataItemIndex].Delete();
        //ds.WriteXml(Server.MapPath("DailCalls.xml"));
        //sFile = sf_code + sCurDate + "ListedDR.xml";
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Chem.xml";
        ds.WriteXml(Server.MapPath(sFile));
        BindGrid_Chem("0");
        // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Deleted Successfully')", true);
    }


    // Grid Events for Listed Doctor
    protected void lnkLstDrEdit_Click(object sender, CommandEventArgs e)
    {
        var lb = (LinkButton)sender;
        var row = (GridViewRow)lb.NamingContainer;
        if (row != null)
        {
            row.BackColor = Color.LimeGreen;
            //row.Enabled = false;
            gvDCR.Enabled = false;
            Label lblSess_Code = row.FindControl("lblSess_Code") as Label;
            Label lblmin = row.FindControl("lblmin") as Label;
            Label lblseconds = row.FindControl("lblseconds") as Label;
            Label lblDR = row.FindControl("lblDR") as Label;
            Label lblDR_Code = row.FindControl("lblDR_Code") as Label;
            Label lblWorkWith = row.FindControl("lblWorkWith") as Label;
            Label lblProduct1 = row.FindControl("lblProd1_Code") as Label;
            Label lblQty1 = row.FindControl("lblQty1") as Label;
            Label lblProd_POB1 = row.FindControl("lblProd_POB1") as Label;
            Label lblProduct2 = row.FindControl("lblProd2_Code") as Label;
            Label lblQty2 = row.FindControl("lblQty2") as Label;
            Label lblProd_POB2 = row.FindControl("lblProd_POB2") as Label;
            Label lblProduct3 = row.FindControl("lblProd3_Code") as Label;
            Label lblQty3 = row.FindControl("lblQty3") as Label;
            Label lblProd_POB3 = row.FindControl("lblProd_POB3") as Label;
            Label lblGift = row.FindControl("lblGift_Code") as Label;
            Label lblGQty = row.FindControl("lblGQty") as Label;

            Label lblAddProd = row.FindControl("lblAddProd") as Label;
            Label lblAddGift = row.FindControl("lblAddGift") as Label;
            Label lblremarks = row.FindControl("lblremarks") as Label;

            if (lblAddProd.Text != "")
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("Col1", typeof(string)));
                dt.Columns.Add(new DataColumn("Col2", typeof(string)));
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Col1"] = string.Empty;
                dr["Col2"] = string.Empty;

                dt.Rows.Add(dr);

                grvProduct.DataSource = dt;
                grvProduct.DataBind();


                string[] addprod = lblAddProd.Text.Split('#');
                int rowIndex = 0;
                foreach (string aprod in addprod)
                {
                    //Levox~1$ # LAPP~2$#
                    if (aprod != "")
                    {
                        string prodcode = aprod.Substring(0, aprod.IndexOf("~")); //aprod.EndsWith('~');
                        //string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - aprod.IndexOf("~")));
                        string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - (prodcode.Length + 2)));
                        DataRow drCurrentRow = null;
                        DropDownList ddlProductAdd = (DropDownList)grvProduct.Rows[rowIndex].Cells[1].FindControl("ddlProductAdd");
                        TextBox txtProdQty = (TextBox)grvProduct.Rows[rowIndex].Cells[2].FindControl("txtProdQty");

                        drCurrentRow = dt.NewRow();
                        drCurrentRow["RowNumber"] = rowIndex;

                        ddlProductAdd.SelectedValue = prodcode;
                        txtProdQty.Text = Qty;
                        dt.Rows[rowIndex]["Col1"] = prodcode;
                        dt.Rows[rowIndex]["Col2"] = Qty;
                        rowIndex++;
                        dt.Rows.Add(drCurrentRow);
                        grvProduct.DataSource = dt;
                        grvProduct.DataBind();
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    rowIndex = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlProductAdd =
                          (DropDownList)grvProduct.Rows[rowIndex].Cells[1].FindControl("ddlProductAdd");
                        TextBox txtProdQty =
                          (TextBox)grvProduct.Rows[rowIndex].Cells[2].FindControl("txtProdQty");

                        ddlProductAdd.SelectedValue = dt.Rows[i]["Col1"].ToString();
                        txtProdQty.Text = dt.Rows[i]["Col2"].ToString();
                        rowIndex++;
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    ViewState["CurrentTable"] = dt;
                    ViewState["AddProdExists"] = "Yes";
                }

            }
            if (lblAddGift.Text != "")
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("Col1", typeof(string)));
                dt.Columns.Add(new DataColumn("Col2", typeof(string)));
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Col1"] = string.Empty;
                dr["Col2"] = string.Empty;

                dt.Rows.Add(dr);

                grdGift.DataSource = dt;
                grdGift.DataBind();


                string[] addgift = lblAddGift.Text.Split('#');
                int rowIndex = 0;
                foreach (string agift in addgift)
                {

                    if (agift != "")
                    {
                        string giftcode = agift.Substring(0, agift.IndexOf("~"));
                        string Qty = agift.Substring(agift.IndexOf("~") + 1, (agift.Length - (giftcode.Length + 2)));

                        DataRow drCurrentRow = null;
                        DropDownList ddlGiftAdd = (DropDownList)grdGift.Rows[rowIndex].Cells[1].FindControl("ddlGiftAdd");
                        TextBox txtGiftQty = (TextBox)grdGift.Rows[rowIndex].Cells[2].FindControl("txtGiftQty");

                        drCurrentRow = dt.NewRow();
                        drCurrentRow["RowNumber"] = rowIndex;

                        ddlGiftAdd.SelectedValue = giftcode;
                        txtGiftQty.Text = Qty;
                        dt.Rows[rowIndex]["Col1"] = giftcode;
                        dt.Rows[rowIndex]["Col2"] = Qty;
                        rowIndex++;
                        dt.Rows.Add(drCurrentRow);
                        grdGift.DataSource = dt;
                        grdGift.DataBind();
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    rowIndex = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlGiftAdd = (DropDownList)grdGift.Rows[rowIndex].Cells[1].FindControl("ddlGiftAdd");
                        TextBox txtGiftQty = (TextBox)grdGift.Rows[rowIndex].Cells[2].FindControl("txtGiftQty");

                        ddlGiftAdd.SelectedValue = dt.Rows[i]["Col1"].ToString();
                        txtGiftQty.Text = dt.Rows[i]["Col2"].ToString();
                        rowIndex++;
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    ViewState["CurrentTableGift"] = dt;
                    ViewState["AddGiftExists"] = "Yes";
                }

            }
            if (sess_dcr == 0)
            {
                if (lblSess_Code.Text != "")
                    ddlSes.SelectedValue = lblSess_Code.Text.Trim();
            }
            if (time_dcr == 0)
            {
                if (lblmin.Text != "")
                    ddlTime.SelectedValue = lblmin.Text.Trim();
                if (lblseconds.Text != "")
                    ddlmin.SelectedValue = lblseconds.Text.Trim();
            }
            if (lblDR_Code.Text != "")
            {
                txtListDRCode.Text = lblDR_Code.Text.Trim();
                lstListDR.SelectedValue = txtListDRCode.Text.Trim();
            }
            else
            {
                lstListDR.SelectedIndex = -1;
            }
            if (lblDR.Text != "")
            {
                txtListDR.Text = lblDR.Text.Trim();
                lstListDR.SelectedItem.Text = txtListDR.Text.Trim();
            }
            else
            {
                lstListDR.SelectedIndex = -1;
            }

            if (lblWorkWith.Text != "")
                txtFieldForce.Text = lblWorkWith.Text.Trim();

            txtRemarks.Text = lblremarks.Text.Trim();

            if (lblProduct1.Text != "")
            {
                ddlProd1.SelectedValue = lblProduct1.Text;
                txtProd1.Text = lblQty1.Text.Trim();
                txtProdPOB1.Text = lblProd_POB1.Text.Trim();
            }
            if (lblProduct2.Text != "")
            {
                ddlProd2.SelectedValue = lblProduct2.Text;
                txtProd2.Text = lblQty2.Text.Trim();
                txtProdPOB2.Text = lblProd_POB2.Text.Trim();
            }
            if (lblProduct3.Text != "")
            {
                ddlProd3.SelectedValue = lblProduct3.Text;
                txtProd3.Text = lblQty3.Text.Trim();
                txtProdPOB3.Text = lblProd_POB3.Text.Trim();
            }
            if (lblGift.Text != "")
            {
                ddlGift.SelectedValue = lblGift.Text.Trim();
                txtGift.Text = lblGQty.Text.Trim();
            }
            int iWW = 0;
            if (txtFieldForce.Text.IndexOf(",") != -1)
            {
                string[] strWWSplit = txtFieldForce.Text.Split(',');
                chkFieldForce.ClearSelection();
                foreach (string strWW in strWWSplit)
                {
                    if (strWW != "")
                    {
                        iWW = iWW + 1;

                        for (int i = 0; i < chkFieldForce.Items.Count; i++)
                        {
                            if (chkFieldForce.Items[i].Text.Trim().Length >= 9)
                            {
                                if (strWW.Trim() == chkFieldForce.Items[i].Text.Substring(0, 9).Trim())
                                    chkFieldForce.Items[i].Selected = true;
                            }
                            else
                            {
                                if (strWW.Trim() == chkFieldForce.Items[i].Text)
                                    chkFieldForce.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }

            txtFieldForce.Text = iWW.ToString();
            ViewState["LDEdit"] = row.RowIndex;
        }
        ldrcolor();
    }

    protected void gvDCR_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ldrcolor();
        BindGrid("0");
        DataSet ds = gvDCR.DataSource as DataSet;
        ds.Tables[0].Rows[gvDCR.Rows[e.RowIndex].DataItemIndex].Delete();
        //ds.WriteXml(Server.MapPath("DailCalls.xml"));
        //sFile = sf_code + sCurDate + "ListedDR.xml";
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_ListedDR.xml";
        ds.WriteXml(Server.MapPath(sFile));
        BindGrid("0");
        //  ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Deleted Successfully')", true);
    }



    protected void gvDCR_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (sess_dcr == 1)
        {
            gvDCR.Columns[0].Visible = false;
            tblDoc.Rows[0].Cells[0].Visible = false;
            tblDoc.Rows[1].Cells[0].Visible = false;
        }
        else
        {
            gvDCR.Columns[0].Visible = true;
            tblDoc.Rows[0].Cells[0].Visible = true;
            tblDoc.Rows[1].Cells[0].Visible = true;
        }
        if (time_dcr == 1)
        {
            gvDCR.Columns[1].Visible = false;
            tblDoc.Rows[0].Cells[1].Visible = false;
            tblDoc.Rows[1].Cells[1].Visible = false;
        }
        else
        {
            gvDCR.Columns[1].Visible = true;
            tblDoc.Rows[0].Cells[1].Visible = true;
            tblDoc.Rows[1].Cells[1].Visible = true;
        }
        if (prod_sel == 0)
        {
            gvDCR.Columns[4].Visible = false;
        }
        else
        {
            gvDCR.Columns[4].Visible = true;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblProduct1 = (Label)e.Row.FindControl("lblProduct1");
            Label lblQty1 = (Label)e.Row.FindControl("lblQty1");
            Label lblProd_POB1 = (Label)e.Row.FindControl("lblProd_POB1");

            Label lblProduct2 = (Label)e.Row.FindControl("lblProduct2");
            Label lblQty2 = (Label)e.Row.FindControl("lblQty2");
            Label lblProd_POB2 = (Label)e.Row.FindControl("lblProd_POB2");

            Label lblProduct3 = (Label)e.Row.FindControl("lblProduct3");
            Label lblQty3 = (Label)e.Row.FindControl("lblQty3");
            Label lblProd_POB3 = (Label)e.Row.FindControl("lblProd_POB3");

            Label lblLDProducts = (Label)e.Row.FindControl("lblLDProducts");
            Label lblAddProdDtl = (Label)e.Row.FindControl("lblAddProdDtl");
            lblLDProducts.Text = "";

            Label lblGift = (Label)e.Row.FindControl("lblGift");
            Label lblGQty = (Label)e.Row.FindControl("lblGQty");

            Label lblLDGift = (Label)e.Row.FindControl("lblLDGift");
            Label lblAddGiftDtl = (Label)e.Row.FindControl("lblAddGiftDtl");

            if (lblProduct1.Text.Trim() != "")
            {

                lblLDProducts.Text = "&nbsp;" + lblLDProducts.Text + lblProduct1.Text.ToString().Replace("~", "").Trim();

                if (lblQty1.Text.Trim() != "")
                {
                    lblLDProducts.Text = lblLDProducts.Text + "(" + lblQty1.Text.ToString() + ")";
                }
                else
                {
                    lblLDProducts.Text = lblLDProducts.Text;
                }
                if (lblProd_POB1.Text.Trim() != "")
                {
                    lblLDProducts.Text = lblLDProducts.Text + "(" + lblProd_POB1.Text.ToString() + ")";
                }
            }

            if (lblProduct2.Text.Trim() != "")
            {

                lblLDProducts.Text = lblLDProducts.Text + "&nbsp;&nbsp;&nbsp;&nbsp;" + lblProduct2.Text.ToString().Replace("~", "").Trim();
                if (lblQty2.Text.Trim() != "")
                {
                    lblLDProducts.Text = lblLDProducts.Text + "(" + lblQty2.Text.ToString() + ")";
                }
                else
                {
                    lblLDProducts.Text = lblLDProducts.Text;
                }
                if (lblProd_POB2.Text.Trim() != "")
                {
                    lblLDProducts.Text = lblLDProducts.Text + "(" + lblProd_POB2.Text.ToString() + ")";
                }
            }
            if (lblProduct3.Text.Trim() != "")
            {

                lblLDProducts.Text = lblLDProducts.Text + "&nbsp;&nbsp;&nbsp;&nbsp;" + lblProduct3.Text.ToString().Replace("~", "").Trim();
                if (lblQty3.Text.Trim() != "")
                {
                    lblLDProducts.Text = lblLDProducts.Text + "(" + lblQty3.Text.ToString() + ")";
                }
                else
                {
                    lblLDProducts.Text = lblLDProducts.Text;
                }
                if (lblProd_POB3.Text.Trim() != "")
                {
                    lblLDProducts.Text = lblLDProducts.Text + "(" + lblProd_POB3.Text.ToString() + ")";
                }
            }
            if (lblAddProdDtl.Text.Length > 0)
            {
                string[] addprod = lblAddProdDtl.Text.Split('#');
                string prodmore = string.Empty;

                foreach (string aprod in addprod)
                {
                    if (aprod != "")
                    {
                        string prodcode = aprod.Substring(0, aprod.IndexOf("~"));
                        string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - (prodcode.Length + 2)));
                        prodmore = prodmore + prodcode;
                        if (Qty.Length > 0)
                        {
                            prodmore = prodmore + " ( " + Qty + " ) " + "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                        else
                        {
                            prodmore = prodmore +  "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                    }
                }
                lblLDProducts.Text = lblLDProducts.Text + "&nbsp;&nbsp;&nbsp;&nbsp;" + prodmore;
            }
            if (lblGift.Text.Trim() != "")
            {
                lblLDGift.Text = lblGift.Text.ToString().Replace("~", "").Trim();
                if (lblGQty.Text.Trim() != "")
                {
                    lblLDGift.Text = lblLDGift.Text + "(" + lblGQty.Text.ToString() + ")";
                }
                else
                {
                    lblLDGift.Text = lblLDGift.Text;
                }
            }
            if (lblAddGiftDtl.Text.Length > 0)
            {
                lblAddGiftDtl.Text = lblAddGiftDtl.Text.Replace("~", " ( ").Trim();
                lblAddGiftDtl.Text = lblAddGiftDtl.Text.Replace("$", " ) ").Trim();
                lblAddGiftDtl.Text = lblAddGiftDtl.Text.Replace("#", "&nbsp;&nbsp;&nbsp;&nbsp;").Trim();
                lblLDGift.Text = lblLDGift.Text + "&nbsp;&nbsp;&nbsp;&nbsp;" + lblAddGiftDtl.Text;
            }
           
        }
    }



    // Grid Events for Stockiest
    protected void lnkEditStk_Click(object sender, CommandEventArgs e)
    {
        var lb = (LinkButton)sender;
        var row = (GridViewRow)lb.NamingContainer;
        if (row != null)
        {
            row.BackColor = Color.LimeGreen;
            //row.Enabled = false;
            GridStk.Enabled = false;
            Label lblStk_Code = row.FindControl("lblstk_Code") as Label;
            Label lblPOBNo = row.FindControl("lblPOB") as Label;
            Label lblWW = row.FindControl("lblStkWW") as Label;
            Label lblVisit_Code = row.FindControl("lblvisit_Code") as Label;

            StkDDL.SelectedValue = lblStk_Code.Text.Trim();
            StkPOB.Text = lblPOBNo.Text.Trim();
            txtStk.Text = lblWW.Text.Trim();
            StkVisitType.SelectedValue = lblVisit_Code.Text.Trim();

            int iWW = 0;
            if (txtStk.Text.IndexOf(",") != -1)
            {
                string[] strWWSplit = txtStk.Text.Split(',');
                StkChkBox.ClearSelection();
                foreach (string strWW in strWWSplit)
                {
                    if (strWW != "")
                    {
                        iWW = iWW + 1;
                        for (int i = 0; i < StkChkBox.Items.Count; i++)
                        {
                            if (StkChkBox.Items[i].Text.Trim().Length >= 9)
                            {
                                if (strWW.Trim() == StkChkBox.Items[i].Text.Substring(0, 9).Trim())
                                    StkChkBox.Items[i].Selected = true;
                            }
                            else
                            {
                                if (strWW.Trim() == StkChkBox.Items[i].Text)
                                    StkChkBox.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }

            txtStk.Text = iWW.ToString();
            ViewState["StkEdit"] = row.RowIndex;
        }
        stkcolor();
    }

    protected void GridStk_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        stkcolor();
        BindGrid_Stockiest("0");
        DataSet ds = GridStk.DataSource as DataSet;
        ds.Tables[0].Rows[GridStk.Rows[e.RowIndex].DataItemIndex].Delete();
        //ds.WriteXml(Server.MapPath("Stockiest.xml"));
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Stockiest.xml";
        ds.WriteXml(Server.MapPath(sFile));
        BindGrid_Stockiest("0");
    }


    // Grid Events for Hospital
    protected void lnkEditHos_Click(object sender, CommandEventArgs e)
    {
        var lb = (LinkButton)sender;
        var row = (GridViewRow)lb.NamingContainer;
        if (row != null)
        {
            row.BackColor = Color.LimeGreen;
            //row.Enabled = false;
            GridHospital.Enabled = false;
            Label lblChem_Code = row.FindControl("lblHos_Code") as Label;
            Label lblPOBNo = row.FindControl("lblPOB") as Label;
            Label lblWW = row.FindControl("lblHosWW") as Label;

            HosDDL.SelectedValue = lblChem_Code.Text.Trim();
            txtHospPOB.Text = lblPOBNo.Text.Trim();
            txtHos.Text = lblWW.Text.Trim();

            int iWW = 0;
            if (txtHos.Text.IndexOf(",") != -1)
            {
                string[] strWWSplit = txtHos.Text.Split(',');
                ChkHos.ClearSelection();
                foreach (string strWW in strWWSplit)
                {
                    if (strWW != "")
                    {
                        iWW = iWW + 1;
                        for (int i = 0; i < ChkHos.Items.Count; i++)
                        {
                            if (ChkHos.Items[i].Text.Trim().Length >= 9)
                            {
                                if (strWW.Trim() == ChkHos.Items[i].Text.Substring(0, 9).Trim())
                                    ChkHos.Items[i].Selected = true;
                            }
                            else
                            {
                                if (strWW.Trim() == ChkHos.Items[i].Text)
                                    ChkHos.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }

            txtHos.Text = iWW.ToString();
            ViewState["HosEdit"] = row.RowIndex;
        }
        hoscolor();
    }

    protected void GridHospital_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        hoscolor();
        BindGrid_Hospital("0");
        DataSet ds = GridHospital.DataSource as DataSet;
        ds.Tables[0].Rows[GridHospital.Rows[e.RowIndex].DataItemIndex].Delete();
        //ds.WriteXml(Server.MapPath("Hospital.xml"));
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Hospital.xml";
        ds.WriteXml(Server.MapPath(sFile));
        BindGrid_Hospital("0");
        //  ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Deleted Successfully')", true);
    }

    // Grid Events for Unlisted Doctor

    protected void lnkUnLstDrEdit_Click(object sender, CommandEventArgs e)
    {
        var lb = (LinkButton)sender;
        var row = (GridViewRow)lb.NamingContainer;
        if (row != null)
        {
            row.BackColor = Color.LimeGreen;
            //row.Enabled = false;
            grdUnLstDR.Enabled = false;

            Label lblSession = row.FindControl("lblSess_Code") as Label;
            Label lblTime = row.FindControl("lblUnLstDR_Time") as Label;
            Label lblmin = row.FindControl("lblmin") as Label;
            Label lblsec = row.FindControl("lblsec") as Label;
            Label lblDR = row.FindControl("lblUnLstDR_Code") as Label;
            Label lblDRName = row.FindControl("lblUnLstDR_DR") as Label;
            Label lblWorkWith = row.FindControl("lblUnLstDR_WorkWith") as Label;
            Label lblProduct1 = row.FindControl("lblProd1_Code") as Label;
            Label lblQty1 = row.FindControl("lblUnLstDR_Qty1") as Label;
            Label lblProd_POB1 = row.FindControl("lblUnLstDR_POB1") as Label;
            Label lblProduct2 = row.FindControl("lblProd2_Code") as Label;
            Label lblQty2 = row.FindControl("lblUnLstDR_Qty2") as Label;
            Label lblProd_POB2 = row.FindControl("lblUnLstDR_POB2") as Label;
            Label lblProduct3 = row.FindControl("lblProd3_Code") as Label;
            Label lblQty3 = row.FindControl("lblUnLstDR_Qty3") as Label;
            Label lblProd_POB3 = row.FindControl("lblUnLstDR_POB3") as Label;
            Label lblGift = row.FindControl("lblGift_Code") as Label;
            Label lblGQty = row.FindControl("lblUnLstDR_GQty") as Label;
            Label lblAddProd = row.FindControl("lblAddProd") as Label;
            Label lblAddGift = row.FindControl("lblAddGift") as Label;

            Label lblterr = row.FindControl("lblterr") as Label;
            Label lblspe = row.FindControl("lblspe") as Label;
            Label lblcat = row.FindControl("lblcat") as Label;
            Label lblclass = row.FindControl("lblclass") as Label;
            Label lblqual = row.FindControl("lblqual") as Label;
            Label lbladd = row.FindControl("lbladd") as Label;
            Label lblnew = row.FindControl("lblnew") as Label;
            Label lblsfcode = row.FindControl("lblsfcode") as Label;

            if (lblnew.Text.Trim() != "New")
            {

                if (lblAddProd.Text != "")
                {
                    DataTable dt = new DataTable();
                    DataRow dr = null;
                    dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                    dt.Columns.Add(new DataColumn("Col1", typeof(string)));
                    dt.Columns.Add(new DataColumn("Col2", typeof(string)));
                    dr = dt.NewRow();
                    dr["RowNumber"] = 1;
                    dr["Col1"] = string.Empty;
                    dr["Col2"] = string.Empty;

                    dt.Rows.Add(dr);

                    grvProductUnlst.DataSource = dt;
                    grvProductUnlst.DataBind();


                    string[] addprod = lblAddProd.Text.Split('#');
                    int rowIndex = 0;
                    foreach (string aprod in addprod)
                    {
                        if (aprod != "")
                        {
                            string prodcode = aprod.Substring(0, aprod.IndexOf("~"));
                            string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - (prodcode.Length + 2)));
                            DataRow drCurrentRow = null;
                            DropDownList ddlProductUnlstAdd = (DropDownList)grvProductUnlst.Rows[rowIndex].Cells[1].FindControl("ddlProductUnlstAdd");
                            TextBox txtProdUnlstQty = (TextBox)grvProductUnlst.Rows[rowIndex].Cells[2].FindControl("txtProdUnlstQty");

                            drCurrentRow = dt.NewRow();
                            drCurrentRow["RowNumber"] = rowIndex;

                            ddlProductUnlstAdd.SelectedValue = prodcode;
                            txtProdUnlstQty.Text = Qty;
                            dt.Rows[rowIndex]["Col1"] = prodcode;
                            dt.Rows[rowIndex]["Col2"] = Qty;
                            rowIndex++;
                            dt.Rows.Add(drCurrentRow);
                            grvProductUnlst.DataSource = dt;
                            grvProductUnlst.DataBind();
                        }
                    }

                    if (dt.Rows.Count > 0)
                    {
                        rowIndex = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DropDownList ddlProductUnlstAdd =
                              (DropDownList)grvProductUnlst.Rows[rowIndex].Cells[1].FindControl("ddlProductUnlstAdd");
                            TextBox txtProdUnlstQty =
                              (TextBox)grvProductUnlst.Rows[rowIndex].Cells[2].FindControl("txtProdUnlstQty");

                            ddlProductUnlstAdd.SelectedValue = dt.Rows[i]["Col1"].ToString();
                            txtProdUnlstQty.Text = dt.Rows[i]["Col2"].ToString();
                            rowIndex++;
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        ViewState["CurrentTableUnlst"] = dt;
                        ViewState["UnlstAddProdExists"] = "Yes";
                    }

                }
                if (lblAddGift.Text != "")
                {
                    DataTable dt = new DataTable();
                    DataRow dr = null;
                    dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                    dt.Columns.Add(new DataColumn("Col1", typeof(string)));
                    dt.Columns.Add(new DataColumn("Col2", typeof(string)));
                    dr = dt.NewRow();
                    dr["RowNumber"] = 1;
                    dr["Col1"] = string.Empty;
                    dr["Col2"] = string.Empty;

                    dt.Rows.Add(dr);

                    grdGiftUnlst.DataSource = dt;
                    grdGiftUnlst.DataBind();


                    string[] addgift = lblAddGift.Text.Split('#');
                    int rowIndex = 0;
                    foreach (string agift in addgift)
                    {

                        if (agift != "")
                        {
                            string giftcode = agift.Substring(0, agift.IndexOf("~"));
                            string Qty = agift.Substring(agift.IndexOf("~") + 1, (agift.Length - (giftcode.Length + 2)));

                            DataRow drCurrentRow = null;
                            DropDownList ddlGiftUnlstAdd = (DropDownList)grdGiftUnlst.Rows[rowIndex].Cells[1].FindControl("ddlGiftUnlstAdd");
                            TextBox txtGiftUnlstQty = (TextBox)grdGiftUnlst.Rows[rowIndex].Cells[2].FindControl("txtGiftUnlstQty");

                            drCurrentRow = dt.NewRow();
                            drCurrentRow["RowNumber"] = rowIndex;

                            ddlGiftUnlstAdd.SelectedValue = giftcode;
                            txtGiftUnlstQty.Text = Qty;
                            dt.Rows[rowIndex]["Col1"] = giftcode;
                            dt.Rows[rowIndex]["Col2"] = Qty;
                            rowIndex++;
                            dt.Rows.Add(drCurrentRow);
                            grdGiftUnlst.DataSource = dt;
                            grdGiftUnlst.DataBind();
                        }
                    }

                    if (dt.Rows.Count > 0)
                    {
                        rowIndex = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DropDownList ddlGiftUnlstAdd = (DropDownList)grdGiftUnlst.Rows[rowIndex].Cells[1].FindControl("ddlGiftUnlstAdd");
                            TextBox txtGiftUnlstQty = (TextBox)grdGiftUnlst.Rows[rowIndex].Cells[2].FindControl("txtGiftUnlstQty");

                            ddlGiftUnlstAdd.SelectedValue = dt.Rows[i]["Col1"].ToString();
                            txtGiftUnlstQty.Text = dt.Rows[i]["Col2"].ToString();
                            rowIndex++;
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        ViewState["CurrentTableGiftUnlst"] = dt;
                        ViewState["UnAddGiftExists"] = "Yes";
                    }

                }
                if (sess_dcr == 0)
                {
                    if (lblSession.Text != "")
                        ddlUnLstDR_Session.SelectedValue = lblSession.Text.Trim();
                }
                if (time_dcr == 0)
                {
                    if (lblmin.Text != "")
                        ddlMinute.SelectedValue = lblmin.Text.Trim();
                    if (lblsec.Text != "")
                        ddlSec.SelectedValue = lblsec.Text.Trim();
                }
                if (lblDR.Text != "")
                {
                    UnLstTxtDR.Text = lblDR.Text.Trim();
                    UnLstDr.SelectedValue = UnLstTxtDR.Text.Trim();
                }
                else
                {
                    UnLstDr.SelectedIndex = -1;
                }
                if (lblDRName.Text != "")
                {
                    Untxt_Dr.Text = lblDRName.Text.Trim();
                    UnLstDr.SelectedItem.Text = Untxt_Dr.Text.Trim();
                }
                else
                {
                    UnLstDr.SelectedIndex = -1;
                }
                if (lblWorkWith.Text != "")
                    txtUnLstDR_SF.Text = lblWorkWith.Text.Trim();
                if (lblProduct1.Text != "")
                {
                    ddlUnLstDR_Prod1.SelectedValue = lblProduct1.Text.Trim();
                    txtUnLstDR_Prod_Qty1.Text = lblQty1.Text.Trim();
                    txtUnLstDR_Prod_POB1.Text = lblProd_POB1.Text.Trim();
                }
                if (lblProduct2.Text != "")
                {
                    ddlUnLstDR_Prod2.SelectedValue = lblProduct2.Text.Trim();
                    txtUnLstDR_Prod_Qty2.Text = lblQty2.Text.Trim();
                    txtUnLstDR_Prod_POB2.Text = lblProd_POB2.Text.Trim();
                }
                if (lblProduct3.Text != "")
                {
                    ddlUnLstDR_Prod3.SelectedValue = lblProduct3.Text.Trim();
                    txtUnLstDR_Prod_Qty3.Text = lblQty3.Text.Trim();
                    txtUnLstDR_Prod_POB3.Text = lblProd_POB3.Text.Trim();
                }
                if (lblGift.Text != "")
                {
                    ddlUnLstDR_Gift.SelectedValue = lblGift.Text.Trim();
                    txtUnLstDR_GQty.Text = lblGQty.Text.Trim();
                }

                int iWW = 0;
                if (txtUnLstDR_SF.Text.IndexOf(",") != -1)
                {
                    string[] strWWSplit = txtUnLstDR_SF.Text.Split(',');
                    chkUnLstDR.ClearSelection();
                    foreach (string strWW in strWWSplit)
                    {
                        if (strWW != "")
                        {
                            iWW = iWW + 1;

                            for (int i = 0; i < chkUnLstDR.Items.Count; i++)
                            {
                                if (chkUnLstDR.Items[i].Text.Trim().Length >= 9)
                                {
                                    if (strWW.Trim() == chkUnLstDR.Items[i].Text.Substring(0, 9).Trim())
                                        chkUnLstDR.Items[i].Selected = true;
                                }
                                else
                                {
                                    if (strWW.Trim() == chkUnLstDR.Items[i].Text)
                                        chkUnLstDR.Items[i].Selected = true;
                                }
                            }
                        }
                    }
                }
                txtUnLstDR_SF.Text = iWW.ToString();
            }
            else
            {
                if (sess_dcr == 0)
                {
                    if (lblSession.Text != "")
                        ddlN_unsess.SelectedValue = lblSession.Text.Trim();
                }
                if (time_dcr == 0)
                {
                    if (lblmin.Text != "")
                        ddlN_untime.SelectedValue = lblmin.Text.Trim();
                    if (lblsec.Text != "")
                        ddlN_unmin.SelectedValue = lblsec.Text.Trim();
                }
                if (lblDRName.Text != "")
                    txtUnDr.Text = lblDRName.Text.Trim();
                if (lblWorkWith.Text != "")
                    txtUn.Text = lblWorkWith.Text.Trim();
                if (lblProduct1.Text != "")
                {
                    ddlN_unProd1.SelectedValue = lblProduct1.Text.Trim();
                    txtN_UQty1.Text = lblQty1.Text.Trim();
                    //  txtN_UQty1.Text = lblProd_POB1.Text.Trim();
                }
                if (lblProduct2.Text != "")
                {
                    ddlN_unProd2.SelectedValue = lblProduct2.Text.Trim();
                    txtN_UQty2.Text = lblQty2.Text.Trim();
                    //txtUnLstDR_Prod_POB2.Text = lblProd_POB2.Text.Trim();
                }
                if (lblProduct3.Text != "")
                {
                    ddlN_unProd3.SelectedValue = lblProduct3.Text.Trim();
                    txtN_UQty3.Text = lblQty3.Text.Trim();
                    // txtUnLstDR_Prod_POB3.Text = lblProd_POB3.Text.Trim();
                }
                if (lblGift.Text != "")
                {
                    ddlN_ungift.SelectedValue = lblGift.Text.Trim();
                    txtN_GQty.Text = lblGQty.Text.Trim();
                }
                if (lblsfcode.Text != "")
                {
                    ddlUnMR.SelectedValue = lblsfcode.Text.Trim();
                }
                if (lblterr.Text != "")
                {
                    ddlTerr_Un.SelectedValue = lblterr.Text.Trim();
                }
                if (lblspe.Text != "")
                {
                    ddlSpec_Un.SelectedValue = lblspe.Text.Trim();
                }
                if (lblcat.Text != "")
                {
                    ddlCate_Un.SelectedValue = lblcat.Text.Trim();
                }
                if (lblclass.Text != "")
                {
                    ddlClass_Un.SelectedValue = lblclass.Text.Trim();
                }
                if (lblqual.Text != "")
                {
                    ddlQual_Un.SelectedValue = lblqual.Text.Trim();
                }
                if (lbladd.Text != "")
                {
                    txtUnDrAddr.Text = lbladd.Text;
                }
                NPnlUnLst.Visible = true;
                pnlMultiView.Enabled = false;
                pnlMultiView.Enabled = false;
                pnlTab.Enabled = false;
                pnlTop.Enabled = false;
                pnlTab1.Enabled = false;
            }

            ViewState["UnLDEdit"] = row.RowIndex;
        }
        udrcolor();
    }
    protected void grdUnLstDR_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        udrcolor();
        BindGrid_UnListedDR("0");
        DataSet ds = grdUnLstDR.DataSource as DataSet;
        ds.Tables[0].Rows[grdUnLstDR.Rows[e.RowIndex].DataItemIndex].Delete();
        //ds.WriteXml(Server.MapPath("UnLstDR.xml"));
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "UnLstDR.xml";
        ds.WriteXml(Server.MapPath(sFile));
        BindGrid_UnListedDR("0");
        //  ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        //   ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Deleted Successfully')", true);
    }

    protected void grdUnLstDR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (sess_dcr == 1)
        {
            grdUnLstDR.Columns[0].Visible = false;
            tblUnlstDr.Rows[0].Cells[0].Visible = false;
            tblUnlstDr.Rows[1].Cells[0].Visible = false;
        }
        else
        {
            grdUnLstDR.Columns[0].Visible = true;
            tblUnlstDr.Rows[0].Cells[0].Visible = true;
            tblUnlstDr.Rows[1].Cells[0].Visible = true;
        }
        if (time_dcr == 1)
        {
            grdUnLstDR.Columns[1].Visible = false;
            tblUnlstDr.Rows[0].Cells[1].Visible = false;
            tblUnlstDr.Rows[1].Cells[1].Visible = false;
        }
        else
        {
            grdUnLstDR.Columns[1].Visible = true;
            tblUnlstDr.Rows[0].Cells[1].Visible = true;
            tblUnlstDr.Rows[1].Cells[1].Visible = true;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblUnLstDR_Product1 = (Label)e.Row.FindControl("lblUnLstDR_Product1");
            Label lblUnLstDR_Qty1 = (Label)e.Row.FindControl("lblUnLstDR_Qty1");
            Label lblUnLstDR_POB1 = (Label)e.Row.FindControl("lblUnLstDR_POB1");

            Label lblUnLstDR_Product2 = (Label)e.Row.FindControl("lblUnLstDR_Product2");
            Label lblUnLstDR_Qty2 = (Label)e.Row.FindControl("lblUnLstDR_Qty2");
            Label lblUnLstDR_POB2 = (Label)e.Row.FindControl("lblUnLstDR_POB2");

            Label lblUnLstDR_Product3 = (Label)e.Row.FindControl("lblUnLstDR_Product3");
            Label lblUnLstDR_Qty3 = (Label)e.Row.FindControl("lblUnLstDR_Qty3");
            Label lblUnLstDR_POB3 = (Label)e.Row.FindControl("lblUnLstDR_POB3");

            Label lblUnProd = (Label)e.Row.FindControl("lblUnProd");
            lblUnProd.Text = "";

            Label lblAddProdDtl = (Label)e.Row.FindControl("lblAddProdDtl");
            Label lblAddGiftDtl = (Label)e.Row.FindControl("lblAddGiftDtl");

            Label lblUnLstDR_Gift = (Label)e.Row.FindControl("lblUnLstDR_Gift");
            Label lblUnLstDR_GQty = (Label)e.Row.FindControl("lblUnLstDR_GQty");

            Label lblUngift = (Label)e.Row.FindControl("lblUngift");
            lblUngift.Text = "";

            if (lblUnLstDR_Product1.Text.Trim() != "")
            {

                lblUnProd.Text = "&nbsp;" + lblUnProd.Text + lblUnLstDR_Product1.Text.ToString().Replace("~", "").Trim();
                if (lblUnLstDR_Qty1.Text.Trim() != "")
                {
                    lblUnProd.Text = lblUnProd.Text + "(" + lblUnLstDR_Qty1.Text.ToString() + ")";
                }
                else
                {
                    lblUnProd.Text = lblUnProd.Text;
                }
                if (lblUnLstDR_POB1.Text.Trim() != "")
                {
                    lblUnProd.Text = lblUnProd.Text + "(" + lblUnLstDR_POB1.Text.ToString() + ")";
                }
            }
            if (lblUnLstDR_Product2.Text.Trim() != "")
            {

                lblUnProd.Text = lblUnProd.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblUnLstDR_Product2.Text.ToString().Replace("~", "").Trim();
                if (lblUnLstDR_Qty2.Text.Trim() != "")
                {
                    lblUnProd.Text = lblUnProd.Text + "(" + lblUnLstDR_Qty2.Text.ToString() + ")";
                }
                else
                {
                    lblUnProd.Text = lblUnProd.Text;
                }
                if (lblUnLstDR_POB2.Text.Trim() != "")
                {
                    lblUnProd.Text = lblUnProd.Text + "(" + lblUnLstDR_POB2.Text.ToString() + ")";
                }
            }
            if (lblUnLstDR_Product3.Text.Trim() != "")
            {

                lblUnProd.Text = lblUnProd.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblUnLstDR_Product3.Text.ToString().Replace("~", "").Trim();
                if (lblUnLstDR_Qty3.Text.Trim() != "")
                {
                    lblUnProd.Text = lblUnProd.Text + "(" + lblUnLstDR_Qty3.Text.ToString() + ")";
                }
                else
                {
                    lblUnProd.Text = lblUnProd.Text;
                }
                if (lblUnLstDR_POB3.Text.Trim() != "")
                {
                    lblUnProd.Text = lblUnProd.Text + "(" + lblUnLstDR_POB3.Text.ToString() + ")";
                }
            }

            if (lblAddProdDtl.Text.Length > 0)
            {
                string[] addprod = lblAddProdDtl.Text.Split('#');
                string prodmore = string.Empty;

                foreach (string aprod in addprod)
                {
                    if (aprod != "")
                    {
                        string prodcode = aprod.Substring(0, aprod.IndexOf("~"));
                        string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - (prodcode.Length + 2)));
                        prodmore = prodmore + prodcode;
                        if (Qty.Length > 0)
                        {
                            prodmore = prodmore + " ( " + Qty + " ) " + "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                        else
                        {
                            prodmore = prodmore + "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                    }
                }
                lblUnProd.Text = lblUnProd.Text + "&nbsp;&nbsp;&nbsp;&nbsp;" + prodmore;
            }
            if (lblUnLstDR_Gift.Text.Trim() != "")
            {
                lblUngift.Text = lblUnLstDR_Gift.Text.ToString().Replace("~", "").Trim();
                if (lblUnLstDR_GQty.Text.Trim() != "")
                {
                    lblUngift.Text = lblUngift.Text + "(" + lblUnLstDR_GQty.Text.ToString() + ")";
                }
                else
                {
                    lblUngift.Text = lblUngift.Text;
                }
            }
            if (lblAddGiftDtl.Text.Length > 0)
            {
                lblAddGiftDtl.Text = lblAddGiftDtl.Text.Replace("~", " ( ").Trim();
                lblAddGiftDtl.Text = lblAddGiftDtl.Text.Replace("$", " ) ").Trim();
                lblAddGiftDtl.Text = lblAddGiftDtl.Text.Replace("#", "&nbsp;&nbsp;&nbsp;&nbsp;").Trim();
                lblUngift.Text = lblUngift.Text + "&nbsp;&nbsp;&nbsp;&nbsp;" + lblAddGiftDtl.Text;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        CreateDCR("0");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        txtRemarkDesc.Text = "";
        RevPreview.Text = "";
        ClearXML();
        DCR_New ds = new DCR_New();
        int iret = ds.Clear_Header(sf_code, lblCurDate.Text);
        if (iret > 0)
        {
            if (sf_type == "2")
                Response.Redirect("~/MasterFiles/MGR/DCR/DCRIndex.aspx");
        }

       // //if (ViewState["RecExist"].ToString() == "1")
       // //{
       //     //if (ViewState["isReject"].ToString() != "true")
       //     //{
       //         DCR_New ds = new DCR_New();
       //         int iret = ds.Clear_Header(sf_code, lblCurDate.Text);
       //         if (iret > 0)
       //         {
       //             if (sf_type == "2")
       //                 Response.Redirect("~/MasterFiles/MGR/DCR/DCRIndex.aspx");
       //         }
       //     //}
       //// }
        if ((ViewState["xmlExist"].ToString() == "1") || (ViewState["RecExist"].ToString() == "1"))
        {
            pnlMultiView.Enabled = true;
            pnlMultiView.Enabled = true;
            pnlTab.Enabled = true;

            ddlWorkType.Enabled = true;
            ddlSDP.Enabled = true;
            btnSave.Enabled = true;
            btnSubmit.Enabled = true;
        }
        ViewState["clear"] = "1";
        Loaddcr("1");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       System.Threading.Thread.Sleep(time);
       if (ddlWorkType.SelectedValue.ToString() != "0")
       {
            DataTable dtWTSettings = WorkType_Selection(ddlWorkType.SelectedValue.ToString().Trim());
            if (dtWTSettings != null)
            {
                if (dtWTSettings.Rows.Count > 0)
                {
                    FieldWork_Ind = dtWTSettings.Rows[0].ItemArray.GetValue(0).ToString();
                    ButtonAccess = dtWTSettings.Rows[0].ItemArray.GetValue(1).ToString();
                }

                string[] but;
                if (ButtonAccess != "")
                {
                    but = ButtonAccess.Split(',');
                    foreach (string st in but)
                    {
                        if ((st == "D") || (st == "C") || (st == "U") || (st == "H") || (st == "S"))
                        {
                            ViewState["isEntry"] = "true";
                        }
                        else
                        {
                            ViewState["isEntry"] = "";
                        }
                    }
                 }
            }
       }
     
       if (ViewState["isEntry"].ToString() == "true")
       {
           if (gvDCR.Rows.Count != 0 || grdChem.Rows.Count != 0 || grdUnLstDR.Rows.Count != 0 || GridHospital.Rows.Count != 0 || GridStk.Rows.Count != 0)
           {
               CreateDCR("1");
           }
           else
           {                
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Daily Call Visit Details!!');</script>");
               // ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + pnlTab1.ClientID + "') .style.display='block';", true);
                loaddata();

           }
       }
       else
       {
           CreateDCR("1");
       }
    }
    private void CreateDCR(string vConf)
    {
        DCR_New dc = new DCR_New();
        bool isrejedit = false;
        bool isdelay = false;
        bool isemptyldr = false;
        bool isemptyche = false;
        bool isemptystk = false;
        bool isemptyunlst = false;
        bool isemptyhos = false;
        if (ddlSDP.SelectedValue == "0")
            ddlSDP.SelectedItem.Text = "";

        DCR_New  dr = new DCR_New();
        dsMgr = dr.getsf_dtls(sf_code, div_code);
        if (dsMgr.Tables[0].Rows.Count > 0)
        {

            sf_name = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            emp_id = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            employee_id = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

        }
        if ((ViewState["isReject"].ToString() == "true") || (ViewState["isReject"].ToString() == "delay"))
        {
            isrejedit = true;
        }
        if (ViewState["isReject"].ToString() == "delay")
        {
            isdelay = true;
        }
        if (ViewState["isEntry"].ToString() != "true")
        {
            // Added by Sridevi to create Header - 02/Oct/15
            Create_Head(ddlSDP.SelectedValue.ToString());
        }
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Header.xml";
        dshead = (DataSet)ViewState["Header"];

        if (dshead != null && dshead.HasChanges())
        {
            if (dshead.Tables[0].Rows.Count > 0)
            {
                start_date = Convert.ToDateTime(dshead.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
            }
        }
      
        if (sf_type == "1")
        {
            iReturn = dc.RecordAdd_Header(sf_code, sf_name, emp_id, employee_id, lblCurDate.Text, ddlWorkType.SelectedValue.ToString(), ddlSDP.SelectedValue.ToString(), ddlSDP.SelectedItem.ToString(), txtRemarkDesc.Text, "0", ViewState["scurdate"].ToString(), isrejedit, isdelay, vConf, start_date.ToString("MM/dd/yyyy HH:mm:ss.fff"), ddlWorkType.SelectedItem.Text.ToString(), sf_type, IPAdd, EntryMode);
        }
        else
        {
            start_date = Convert.ToDateTime(ViewState["Start"].ToString());
            iReturn = dc.RecordAdd_Header(sf_code, sf_name, emp_id, employee_id, lblCurDate.Text, ddlWorkType.SelectedValue.ToString(), "0", "", txtRemarkDesc.Text, "0", ViewState["scurdate"].ToString(), isrejedit, isdelay, vConf, start_date.ToString("MM/dd/yyyy HH:mm:ss.fff"), ddlWorkType.SelectedItem.Text.ToString(), sf_type, IPAdd, EntryMode);
        }
       

        ViewState["iReturn"] = iReturn;

        if (iReturn > 0)
        {
            iReturn_Det = 1;
            //if ((ddlWorkType.SelectedItem.Text.Trim() == "Field Work") || (ddlWorkType.SelectedItem.Text.Trim() == "Leave"))
            //{
            if (ViewState["isEntry"].ToString() == "true")
            {

                if (gvDCR.Rows.Count > 0)
                {
                    //Listed Doctor
                    foreach (GridViewRow gridRow in gvDCR.Rows)
                    {
                        Label lblSession = (Label)gridRow.Cells[0].FindControl("lblSession");


                        Label lblTime = (Label)gridRow.Cells[1].FindControl("lblTime");

                        Label lblDR = (Label)gridRow.Cells[2].FindControl("lblDR");
                        Label lblWorkWith = (Label)gridRow.Cells[3].FindControl("lblWorkWith");
                        Label lblProduct1 = (Label)gridRow.Cells[4].FindControl("lblProduct1");

                        Label lblQty1 = (Label)gridRow.Cells[4].FindControl("lblQty1");
                        Label lblProd_POB1 = (Label)gridRow.Cells[4].FindControl("lblProd_POB1");
                        Label lblProduct2 = (Label)gridRow.Cells[4].FindControl("lblProduct2");
                        Label lblQty2 = (Label)gridRow.Cells[4].FindControl("lblQty2");
                        Label lblProd_POB2 = (Label)gridRow.Cells[4].FindControl("lblProd_POB2");
                        Label lblProduct3 = (Label)gridRow.Cells[4].FindControl("lblProduct3");
                        Label lblQty3 = (Label)gridRow.Cells[4].FindControl("lblQty3");
                        Label lblProd_POB3 = (Label)gridRow.Cells[4].FindControl("lblProd_POB3");
                        Label lblGift = (Label)gridRow.Cells[5].FindControl("lblGift");
                        Label lblGQty = (Label)gridRow.Cells[5].FindControl("lblGQty");
                        Label lblDR_Code = (Label)gridRow.Cells[6].FindControl("lblDR_Code");
                        //Added by sri - to include code
                        Label sess_code = (Label)gridRow.Cells[7].FindControl("lblSess_Code");
                        Label lblmin = (Label)gridRow.Cells[8].FindControl("lblmin");
                        Label lblseconds = (Label)gridRow.Cells[9].FindControl("lblseconds");
                        Label lblProd1_Code = (Label)gridRow.Cells[10].FindControl("lblProd1_Code");
                        Label lblProd2_Code = (Label)gridRow.Cells[11].FindControl("lblProd2_Code");
                        Label lblProd3_Code = (Label)gridRow.Cells[12].FindControl("lblProd3_Code");

                        Label lblAddProd = (Label)gridRow.Cells[13].FindControl("lblAddProd");
                        Label lblAddProdDtl = (Label)gridRow.Cells[14].FindControl("lblAddProdDtl");
                        Label lblGift_Code = (Label)gridRow.Cells[15].FindControl("lblGift_Code");
                        Label lblAddGift = (Label)gridRow.Cells[16].FindControl("lblAddGift");
                        Label lblAddGiftDtl = (Label)gridRow.Cells[17].FindControl("lblAddGiftDtl");
                        Label lblrmks = (Label)gridRow.Cells[18].FindControl("lblremarks");
                        Label lblww_code = (Label)gridRow.Cells[19].FindControl("lblww_code");

                        if (lblProduct1.Text.ToString().Trim().Length > 0)
                        {
                            Prod_Detail = lblProduct1.Text.Trim() + "~" + lblQty1.Text + "$" + lblProd_POB1.Text;
                            Prod_Detail_Code = lblProd1_Code.Text.Trim() + "~" + lblQty1.Text + "$" + lblProd_POB1.Text;
                        }

                        if (lblProduct2.Text.ToString().Trim().Length > 0)
                        {

                            Prod_Detail = Prod_Detail + "#" + lblProduct2.Text.Trim() + "~" + lblQty2.Text + "$" + lblProd_POB2.Text;
                            Prod_Detail_Code = Prod_Detail_Code + "#" + lblProd2_Code.Text.Trim() + "~" + lblQty2.Text + "$" + lblProd_POB2.Text;
                        }

                        if (lblProduct3.Text.ToString().Trim().Length > 0)
                        {

                            Prod_Detail = Prod_Detail + "#" + lblProduct3.Text.Trim() + "~" + lblQty3.Text + "$" + lblProd_POB3.Text;
                            Prod_Detail_Code = Prod_Detail_Code + "#" + lblProd3_Code.Text.Trim() + "~" + lblQty3.Text + "$" + lblProd_POB3.Text;
                        }

                        Add_Prod_Detail = lblAddProdDtl.Text.Trim();
                        Add_Prod_Code = lblAddProd.Text.Trim();
                        Add_Gift_Detail = lblAddGiftDtl.Text.Trim();
                        Add_Gift_Code = lblAddGift.Text.Trim();

                        iReturn_Det_val = dc.RecordAdd_Detail(sf_code, iReturn, 1, lblDR_Code.Text, lblSession.Text, lblTime.Text, lblww_code.Text, lblWorkWith.Text, Prod_Detail, lblGift_Code.Text, lblGift.Text, lblGQty.Text, ddlSDP.SelectedValue.ToString(), vConf, sess_code.Text, lblmin.Text, lblseconds.Text, Prod_Detail_Code, Gift_Detail_Code, Add_Prod_Detail, Add_Prod_Code, Add_Gift_Detail, Add_Gift_Code, lblDR.Text, lblrmks.Text);

                        if (iReturn_Det_val <= 0)
                        {
                            break;
                        }
                    }

                }
                else
                {
                    isemptyldr = true;
                }
                if (grdChem.Rows.Count > 0)
                {
                    //Chemists
                    foreach (GridViewRow gridRow in grdChem.Rows)
                    {
                        iReturn_Det = -1;
                        int Che_Code = 0;
                        Label lblChemists = (Label)gridRow.Cells[0].FindControl("lblChemists");
                        Label lblWW = (Label)gridRow.Cells[1].FindControl("lblWW");
                        Label lblPOBNo = (Label)gridRow.Cells[2].FindControl("lblPOBNo");
                        Label lblChem_Code = (Label)gridRow.Cells[3].FindControl("lblChem_Code");
                        Label lblTerr_Code = (Label)gridRow.Cells[4].FindControl("lblTerr_Code");
                        Label lblnew = (Label)gridRow.Cells[5].FindControl("lblnew");
                        Label lblsf_code = (Label)gridRow.Cells[6].FindControl("lblsf_code");

                        Label lblww_code = (Label)gridRow.Cells[7].FindControl("ww_code");

                        if (lblnew.Text == "New" && lblChem_Code.Text == "")
                        {
                            Chemist cs = new Chemist();
                            if (sf_type == "2")
                            {
                                Che_Code = cs.RecordAdd_DcrChem(lblChemists.Text, "", "", "", lblTerr_Code.Text, "", lblsf_code.Text,lblCurDate.Text,"DCR");
                            }
                            else
                            {
                                Che_Code = cs.RecordAdd_DcrChem(lblChemists.Text, "", "", "", lblTerr_Code.Text, "", sf_code,lblCurDate.Text,"DCR");
                            }
                            if (Che_Code == -2)
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('New Chemists Already Exists!');</script>");
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + pnlTab1.ClientID + "') .style.display='block';", true);
                            }
                            else
                            {
                                lblChem_Code.Text = Che_Code.ToString();
                            }
                        }
                        if (Che_Code != -2)
                        {
                            iReturn_Det = dc.RecordAdd_Detail_Chem(sf_code, iReturn, 2, lblChem_Code.Text, lblPOBNo.Text, lblww_code.Text, lblWW.Text, ddlSDP.SelectedValue.ToString(), vConf, lblChemists.Text);

                            if (iReturn_Det <= 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                }
                else
                {
                    isemptyche = true;
                }
                if (GridStk.Rows.Count > 0)
                {
                    //Stockiest
                    foreach (GridViewRow gridRow in GridStk.Rows)
                    {
                        iReturn_Det = -1;
                        Label lblStockist = (Label)gridRow.Cells[0].FindControl("lblStockist");
                        Label lblStkWW = (Label)gridRow.Cells[1].FindControl("lblStkWW");
                        Label lblPOB = (Label)gridRow.Cells[2].FindControl("lblPOB");
                        Label lblStkVT = (Label)gridRow.Cells[3].FindControl("lblStkVT");
                        Label lblstk_Code = (Label)gridRow.Cells[4].FindControl("lblstk_Code");
                        Label lblvisit_Code = (Label)gridRow.Cells[5].FindControl("lblvisit_Code");
                        Label lblww_code = (Label)gridRow.Cells[6].FindControl("lblww_code");

                        iReturn_Det = dc.RecordAdd_Detail_Stockiest(sf_code, iReturn, 3, lblstk_Code.Text, lblPOB.Text, lblww_code.Text, lblStkWW.Text, ddlSDP.SelectedValue.ToString(), lblvisit_Code.Text, vConf, lblStockist.Text);

                        if (iReturn_Det <= 0)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    isemptystk = true;
                }
                if (GridHospital.Rows.Count > 0)
                {
                    //Hospital
                    foreach (GridViewRow gridRow in GridHospital.Rows)
                    {
                        iReturn_Det = -1;
                        Label lblHospital = (Label)gridRow.Cells[0].FindControl("lblHospital");
                        Label lblHosWW = (Label)gridRow.Cells[1].FindControl("lblHosWW");
                        Label lblPOB = (Label)gridRow.Cells[2].FindControl("lblPOB");
                        Label lblHos_Code = (Label)gridRow.Cells[3].FindControl("lblHos_Code");
                        Label lblww_code = (Label)gridRow.Cells[4].FindControl("lblww_code");

                        iReturn_Det = dc.RecordAdd_Detail_Hosp(sf_code, iReturn, 5, lblHos_Code.Text, lblPOB.Text, lblww_code.Text, lblHosWW.Text, ddlSDP.SelectedValue.ToString(), vConf, lblHospital.Text);

                        if (iReturn_Det <= 0)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    isemptyhos = true;
                }
                if (grdUnLstDR.Rows.Count > 0)
                {
                    //Un-Listed Doctor
                    foreach (GridViewRow gridRow in grdUnLstDR.Rows)
                    {
                        iReturn_Det = -1;
                        int UnDr_Code = -1;
                        Label lblSession = (Label)gridRow.Cells[0].FindControl("lblUnLstDR_Session");
                        Label lblTime = (Label)gridRow.Cells[1].FindControl("lblUnLstDR_Time");
                        Label lblDR = (Label)gridRow.Cells[2].FindControl("lblUnLstDR_DR");
                        Label lblWorkWith = (Label)gridRow.Cells[3].FindControl("lblUnLstDR_WorkWith");
                        Label lblProduct1 = (Label)gridRow.Cells[4].FindControl("lblUnLstDR_Product1");
                        Label lblQty1 = (Label)gridRow.Cells[4].FindControl("lblUnLstDR_Qty1");
                        Label lblProd_POB1 = (Label)gridRow.Cells[4].FindControl("lblUnLstDR_POB1");
                        Label lblProduct2 = (Label)gridRow.Cells[4].FindControl("lblUnLstDR_Product2");
                        Label lblQty2 = (Label)gridRow.Cells[4].FindControl("lblUnLstDR_Qty2");
                        Label lblProd_POB2 = (Label)gridRow.Cells[4].FindControl("lblUnLstDR_POB2");
                        Label lblProduct3 = (Label)gridRow.Cells[4].FindControl("lblUnLstDR_Product3");
                        Label lblQty3 = (Label)gridRow.Cells[4].FindControl("lblUnLstDR_Qty3");
                        Label lblProd_POB3 = (Label)gridRow.Cells[4].FindControl("lblUnLstDR_POB3");
                        Label lblGift = (Label)gridRow.Cells[5].FindControl("lblUnLstDR_Gift");
                        Label lblGQty = (Label)gridRow.Cells[5].FindControl("lblUnLstDR_GQty");
                        Label lblDR_Code = (Label)gridRow.Cells[6].FindControl("lblUnLstDR_Code");

                        //Added by sri - to include code
                        Label sess_code = (Label)gridRow.Cells[7].FindControl("lblSess_Code");
                        Label time_code = (Label)gridRow.Cells[8].FindControl("lblTime_Code");
                        Label lblProd1_Code = (Label)gridRow.Cells[9].FindControl("lblProd1_Code");
                        Label lblProd2_Code = (Label)gridRow.Cells[10].FindControl("lblProd2_Code");
                        Label lblProd3_Code = (Label)gridRow.Cells[11].FindControl("lblProd3_Code");

                        Label lblAddProd = (Label)gridRow.Cells[12].FindControl("lblAddProd");
                        Label lblAddProdDtl = (Label)gridRow.Cells[13].FindControl("lblAddProdDtl");
                        Label lblGift_Code = (Label)gridRow.Cells[14].FindControl("lblGift_Code");
                        Label lblAddGift = (Label)gridRow.Cells[15].FindControl("lblAddGift");
                        Label lblAddGiftDtl = (Label)gridRow.Cells[16].FindControl("lblAddGiftDtl");
                        Label lblmin = (Label)gridRow.Cells[17].FindControl("lblmin");
                        Label lblsec = (Label)gridRow.Cells[18].FindControl("lblsec");
                        Label lblterr = (Label)gridRow.Cells[19].FindControl("lblterr");
                        Label lblspe = (Label)gridRow.Cells[20].FindControl("lblspe");
                        Label lblcat = (Label)gridRow.Cells[21].FindControl("lblcat");
                        Label lblclass = (Label)gridRow.Cells[22].FindControl("lblclass");
                        Label lblqual = (Label)gridRow.Cells[23].FindControl("lblqual");
                        Label lbladd = (Label)gridRow.Cells[24].FindControl("lbladd");
                        Label lblnew = (Label)gridRow.Cells[25].FindControl("lblnew");
                        Label lblsf_code = (Label)gridRow.Cells[26].FindControl("lblsfcode");
                        Label lblww_code = (Label)gridRow.Cells[27].FindControl("lblww_code");

                        if (lblProduct1.Text.ToString().Trim().Length > 0)
                        {
                            Prod_Detail = lblProduct1.Text.Trim() + "~" + lblQty1.Text + "$" + lblProd_POB1.Text;
                            Prod_Detail_Code = lblProd1_Code.Text.Trim() + "~" + lblQty1.Text + "$" + lblProd_POB1.Text;
                        }

                        if (lblProduct2.Text.ToString().Trim().Length > 0)
                        {

                            Prod_Detail = Prod_Detail + "#" + lblProduct2.Text.Trim() + "~" + lblQty2.Text + "$" + lblProd_POB2.Text;
                            Prod_Detail_Code = Prod_Detail_Code + "#" + lblProd2_Code.Text.Trim() + "~" + lblQty2.Text + "$" + lblProd_POB2.Text;
                        }

                        if (lblProduct3.Text.ToString().Trim().Length > 0)
                        {

                            Prod_Detail = Prod_Detail + "#" + lblProduct3.Text.Trim() + "~" + lblQty3.Text + "$" + lblProd_POB3.Text;
                            Prod_Detail_Code = Prod_Detail_Code + "#" + lblProd3_Code.Text.Trim() + "~" + lblQty3.Text + "$" + lblProd_POB3.Text;
                        }

                        Add_Prod_Detail = lblAddProdDtl.Text.Trim();
                        Add_Prod_Code = lblAddProd.Text.Trim();
                        Add_Gift_Detail = lblAddGiftDtl.Text.Trim();
                        Add_Gift_Code = lblAddGift.Text.Trim();

                        if (lblnew.Text == "New" && lblDR_Code.Text == "")
                        {
                            UnListedDR undr = new UnListedDR();
                            if (sf_type == "2")
                            {
                                UnDr_Code = undr.RecordAdd_DcrUn(lblDR.Text, lbladd.Text, lblcat.Text, lblspe.Text, lblqual.Text, lblclass.Text, lblterr.Text, lblsf_code.Text, lblCurDate.Text,"DCR");
                            }
                            else
                            {
                                UnDr_Code = undr.RecordAdd_DcrUn(lblDR.Text, lbladd.Text, lblcat.Text, lblspe.Text, lblqual.Text, lblclass.Text, lblterr.Text, sf_code, lblCurDate.Text,"DCR");
                            }
                            if (UnDr_Code == -2)
                            {
                                
                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('New Un-ListedDoctor Already Exists!');</script>");
                                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + pnlTab1.ClientID + "') .style.display='block';", true);
                               
                            }
                            else
                            {
                                lblDR_Code.Text = UnDr_Code.ToString();
                            }
                        }
                        if (UnDr_Code != -2)
                        {
                            iReturn_Det = dc.RecordAdd_Detail_Unlst(sf_code, iReturn, 4, lblDR_Code.Text, lblSession.Text, lblTime.Text, lblww_code.Text, lblWorkWith.Text, Prod_Detail, lblGift_Code.Text, lblGift.Text, lblGQty.Text, ddlSDP.SelectedValue.ToString(), vConf, sess_code.Text, lblmin.Text, lblsec.Text, Prod_Detail_Code, Gift_Detail_Code, Add_Prod_Detail, Add_Prod_Code, Add_Gift_Detail, Add_Gift_Code, lblDR.Text);

                            if (iReturn_Det <= 0)
                            {
                                break;
                            }
                        }
                        else
                            break;
                    }

                }
                else
                {
                    isemptyunlst = true;
                }
            }
            if (vConf == "0")
            {
                Fill_Review();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DCR Saved successfully!');</script>");
                loaddata();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('DCR Saved successfully!')",true);                 
            }
            else if ((vConf == "1") && (iReturn_Det > 0))
            {
                if (isemptyche == false || isemptyldr == false || isemptystk == false || isemptyhos == false || isemptyunlst == false)
                {
                    //Update Main table - Confirmed - 0 to 1
                    int iReturn_upd = dc.Update_Header(sf_code, iReturn, isrejedit, ViewState["scurdate"].ToString(), div_code, lblCurDate.Text, isdelay);
                    if (iReturn_upd > 0)
                    {
                        if (iAppNeed == 0)
                        {
                            int iretmain = dc.Create_DCRHead_Trans(sf_code, iReturn);
                            if (iretmain > 0)
                                removexml();
                        }
                        if (sf_type == "1")
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DCR Submitted successfully!');</script>");
                            ViewState["clear"] = "0";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('DCR Submitted successfully!')", true);
                            
                            Loaddcr("1");
                           

                        }
                        else
                        {
                            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('DCR Submitted successfully!') ; location.href='../../MGR/DCR/DCRIndex.aspx';", true);
                            // Response.Write("<script>alert('DCR Submitted successfully') ; location.href='../../MGR/DCR/DCRIndex.aspx';</script>");
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DCR Submitted successfully');window.location='../../MGR/DCR/DCRIndex.aspx';</script>");
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Daily Call Visit Details!!');</script>");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + pnlTab1.ClientID + "') .style.display='block';", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Please Enter Daily Call Visit Details!!')", true);
                }

            }
        }

    }
    private void removexml()
    {
        //Delete the Listed Doctor XML file
        //string FilePath = Server.MapPath("DailCalls.xml");
        //sFile = sf_code + sCurDate + "ListedDR.xml";

        string sFileHeader = sf_code + "_" + ViewState["curdate"].ToString() + "_Header.xml";
        string headerFilePath = Server.MapPath(sFileHeader);
        if (File.Exists(headerFilePath))
            File.Delete(headerFilePath);

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_ListedDR.xml";
        string FilePath = Server.MapPath(sFile);
        if (File.Exists(FilePath))
            File.Delete(FilePath);

        //Delete the Chemists XML file
        //FilePath = Server.MapPath("Chem_DCR.xml");
        string sChemFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Chem.xml";
        string chemFilePath = Server.MapPath(sChemFile);
        if (File.Exists(chemFilePath))
            File.Delete(chemFilePath);

        //Delete the Stockiest XML file
        string sStockFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Stockiest.xml";
        string stockFilePath = Server.MapPath(sStockFile);
        if (File.Exists(stockFilePath))
            File.Delete(stockFilePath);

        //Delete the Hospital XML file
        string sHosFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Hospital.xml";
        string hosFilePath = Server.MapPath(sHosFile);
        if (File.Exists(hosFilePath))
            File.Delete(hosFilePath);


        //Delete the Un-:isted XML file                
        string sUnLstFile = sf_code + "_" + ViewState["curdate"].ToString() + "UnLstDR.xml";
        string unlstFilePath = Server.MapPath(sUnLstFile);
        if (File.Exists(unlstFilePath))
            File.Delete(unlstFilePath);
    }
    private void ClearXML()
    {

        string sFileHeader = sf_code + "_" + ViewState["curdate"].ToString() + "_Header.xml";
        string headerFilePath = Server.MapPath(sFileHeader);
        if (File.Exists(headerFilePath))
            File.Delete(headerFilePath);

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_ListedDR.xml";
        string FilePath = Server.MapPath(sFile);
        if (File.Exists(FilePath))
            File.Delete(FilePath);

        //Delete the Chemists XML file
        //FilePath = Server.MapPath("Chem_DCR.xml");
        string sChemFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Chem.xml";
        string chemFilePath = Server.MapPath(sChemFile);
        if (File.Exists(chemFilePath))
            File.Delete(chemFilePath);

        //Delete the Stockiest XML file
        string sStockFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Stockiest.xml";
        string stockFilePath = Server.MapPath(sStockFile);
        if (File.Exists(stockFilePath))
            File.Delete(stockFilePath);

        //Delete the Hospital XML file
        string sHosFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Hospital.xml";
        string hosFilePath = Server.MapPath(sHosFile);
        if (File.Exists(hosFilePath))
            File.Delete(hosFilePath);


        //Delete the Un-:isted XML file                
        string sUnLstFile = sf_code + "_" + ViewState["curdate"].ToString() + "UnLstDR.xml";
        string unlstFilePath = Server.MapPath(sUnLstFile);
        if (File.Exists(unlstFilePath))
            File.Delete(unlstFilePath);

        // Creates an XML for Header

        //Start writer
        //XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath("DailCalls.xml"), System.Text.Encoding.UTF8);
        XmlTextWriter dr_writerHeader = new XmlTextWriter(Server.MapPath(sFileHeader), System.Text.Encoding.UTF8);

        //Start XM DOcument
        dr_writerHeader.WriteStartDocument(true);
        dr_writerHeader.Formatting = Formatting.Indented;
        dr_writerHeader.Indentation = 2;

        //ROOT Element
        dr_writerHeader.WriteStartElement("DCR");
        dr_writerHeader.WriteEndElement();
        //End XML Document
        dr_writerHeader.WriteEndDocument();
        //Close writer
        dr_writerHeader.Close();



        // Creates an XML for Listed Doctor

        //Start writer
        //XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath("DailCalls.xml"), System.Text.Encoding.UTF8);
        XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

        //Start XM DOcument
        dr_writer.WriteStartDocument(true);
        dr_writer.Formatting = Formatting.Indented;
        dr_writer.Indentation = 2;

        //ROOT Element
        dr_writer.WriteStartElement("DCR");
        dr_writer.WriteEndElement();
        //End XML Document
        dr_writer.WriteEndDocument();
        //Close writer
        dr_writer.Close();


        // Creates an XML for Chemists
        //Start writer
        XmlTextWriter writer = new XmlTextWriter(Server.MapPath(sChemFile), System.Text.Encoding.UTF8);

        //Start XM DOcument
        writer.WriteStartDocument(true);
        writer.Formatting = Formatting.Indented;
        writer.Indentation = 2;

        //ROOT Element
        writer.WriteStartElement("DCR");
        writer.WriteEndElement();
        //End XML Document
        writer.WriteEndDocument();
        //Close writer
        writer.Close();

        //Stockiest
        XmlTextWriter Stock_writer = new XmlTextWriter(Server.MapPath(sStockFile), System.Text.Encoding.UTF8);

        //Start XM DOcument
        Stock_writer.WriteStartDocument(true);
        Stock_writer.Formatting = Formatting.Indented;
        Stock_writer.Indentation = 2;

        //ROOT Element
        Stock_writer.WriteStartElement("DCR");
        Stock_writer.WriteEndElement();
        //End XML Document
        Stock_writer.WriteEndDocument();
        //Close writer
        Stock_writer.Close();


        //Hospital
        XmlTextWriter Hos_writer = new XmlTextWriter(Server.MapPath(sHosFile), System.Text.Encoding.UTF8);

        //Start XM DOcument
        Hos_writer.WriteStartDocument(true);
        Hos_writer.Formatting = Formatting.Indented;
        Hos_writer.Indentation = 2;

        //ROOT Element
        Hos_writer.WriteStartElement("DCR");
        Hos_writer.WriteEndElement();
        //End XML Document
        Hos_writer.WriteEndDocument();
        //Close writer
        Hos_writer.Close();

        //Un-Listed Doctor
        XmlTextWriter UnLstDR_writer = new XmlTextWriter(Server.MapPath(sUnLstFile), System.Text.Encoding.UTF8);

        //Start XM DOcument
        UnLstDR_writer.WriteStartDocument(true);
        UnLstDR_writer.Formatting = Formatting.Indented;
        UnLstDR_writer.Indentation = 2;

        //ROOT Element
        UnLstDR_writer.WriteStartElement("DCR");
        UnLstDR_writer.WriteEndElement();
        //End XML Document
        UnLstDR_writer.WriteEndDocument();
        //Close writer
        UnLstDR_writer.Close();
        txtRemarkDesc.Text = "";
        Bind_Header();
        BindGrid("0");
        BindGrid_Chem("0");
        BindGrid_UnListedDR("0");
        BindGrid_Stockiest("0");
        BindGrid_Hospital("0");
    }
    private void ResponseWriteEx(string output, Color color)
    {
        //Response.Write(String.Format(@"<span style=""color: #{0}"">{1}</span>", System.Drawing.ColorTranslator.ToHtml(color), output));
    }

    protected void btnUnLstDR_Go_Click(object sender, EventArgs e)
    {
       
        CreateUnlisted("0");
    }

    protected void btnStkGo_Click(object sender, EventArgs e)
    {
        // Added by Sridevi to get WW -Code
        hdnStkWWCode.Value = "";
        for (int i = 0; i < StkChkBox.Items.Count; i++)
        {
            if (StkChkBox.Items[i].Selected)
            {
                hdnStkWWCode.Value = hdnStkWWCode.Value + StkChkBox.Items[i].Value.ToString() + "$$";
            }
        }
        hdnStkWWCode.Value = hdnStkWWCode.Value.Substring(0, hdnStkWWCode.Value.Length - 2);
        
        //ends
      //  if (ViewState["StkEdit"].ToString() != "")
        if(hdnstkedit.Value !="")
        {
            BindGrid_Stockiest("0");
            DataSet ds = (DataSet)GridStk.DataSource;
            int i = GridStk.Rows[Convert.ToInt16(hdnstkedit.Value.ToString())-1].DataItemIndex;
         
            ds.Tables[0].Rows[i]["stockiest_code"] = StkDDL.SelectedValue.ToString();
            ds.Tables[0].Rows[i]["stockiest"] = StkDDL.SelectedItem.Text.ToString();
            ds.Tables[0].Rows[i]["pob"] = StkPOB.Text.ToString();
            ds.Tables[0].Rows[i]["stockww"] = HdnStk.Value.ToString();
            ds.Tables[0].Rows[i]["ww_code"] = hdnStkWWCode.Value.ToString();

            if (StkVisitType.SelectedIndex > 0)
            {
                ds.Tables[0].Rows[i]["visit"] = StkVisitType.SelectedItem.Text.ToString();
                ds.Tables[0].Rows[i]["visit_code"] = StkVisitType.SelectedValue.ToString();
            }
            else
            {
                ds.Tables[0].Rows[i]["visit"] = "";
                ds.Tables[0].Rows[i]["visit_code"] = "0";

            }

            sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Stockiest.xml";
            ds.WriteXml(Server.MapPath(sFile));
            GridStk.Enabled = true;
            hdnstkedit.Value = "";
            BindGrid_Stockiest("0");
          
        }
        else
        {

            // Added by Sridevi to create Header - 02/Oct/15
            Create_Head(ddlSDP.SelectedValue.ToString());

            sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Stockiest.xml";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Server.MapPath(sFile));

            XmlElement parentelement = xmldoc.CreateElement("DCR");

            XmlElement xmlstockiest = xmldoc.CreateElement("stockiest");
            XmlElement xmlstockiestww = xmldoc.CreateElement("stockww");
            XmlElement xmlpob = xmldoc.CreateElement("pob");
            XmlElement xmlvisit = xmldoc.CreateElement("visit");
            XmlElement xmlstockiest_code = xmldoc.CreateElement("stockiest_code");
            XmlElement xmlvisit_code = xmldoc.CreateElement("visit_code");
            XmlElement xmlww_code = xmldoc.CreateElement("ww_code");

            xmlstockiest.InnerText = StkDDL.SelectedItem.Text.ToString();
            xmlstockiest_code.InnerText = StkDDL.SelectedValue.ToString();
            xmlstockiestww.InnerText = HdnStk.Value.ToString(); //txtChemWW.Text.ToString();
            xmlpob.InnerText = StkPOB.Text.ToString();
            xmlww_code.InnerText = hdnStkWWCode.Value.ToString();

            if (StkVisitType.SelectedIndex > 0)
            {
                xmlvisit.InnerText = StkVisitType.SelectedItem.Text.ToString();
                xmlvisit_code.InnerText = StkVisitType.SelectedValue.ToString();
            }
            else
            {
                xmlvisit.InnerText = "";
                xmlvisit_code.InnerText = "0";
            }
            parentelement.AppendChild(xmlstockiest);
            parentelement.AppendChild(xmlstockiestww);
            parentelement.AppendChild(xmlpob);
            parentelement.AppendChild(xmlvisit);
            parentelement.AppendChild(xmlstockiest_code);
            parentelement.AppendChild(xmlvisit_code);
            parentelement.AppendChild(xmlww_code);

            xmldoc.DocumentElement.AppendChild(parentelement);
            //xmldoc.Save(Server.MapPath("Stockiest.xml"));
            xmldoc.Save(Server.MapPath(sFile));
            //ViewState["cur_panel"] = "2";

            BindGrid_Stockiest("0");
        }
        stkcolor();
    }

    protected void btnHosGo_Click(object sender, EventArgs e)
    {
        // Added by Sridevi to get WW -Code
        hdnHosww_code.Value = "";
        for (int i = 0; i < ChkHos.Items.Count; i++)
        {
            if (ChkHos.Items[i].Selected)
            {
                hdnHosww_code.Value = hdnHosww_code.Value + ChkHos.Items[i].Value.ToString() + "$$";
            }
        }
        hdnHosww_code.Value = hdnHosww_code.Value.Substring(0, hdnHosww_code.Value.Length - 2);

        //ends
        if (hdnhosedit.Value != "")
        {        
            BindGrid_Hospital("0");
            DataSet ds = (DataSet)GridHospital.DataSource;
            int i = GridHospital.Rows[Convert.ToInt16(hdnhosedit.Value.ToString())-1].DataItemIndex;

            ds.Tables[0].Rows[i]["hospital_code"] = HosDDL.SelectedValue.ToString();
            ds.Tables[0].Rows[i]["hospital"] = HosDDL.SelectedItem.Text.ToString();
            ds.Tables[0].Rows[i]["pob"] = txtHospPOB.Text.ToString();
            ds.Tables[0].Rows[i]["hosww"] = HdnHos.Value.ToString();
            ds.Tables[0].Rows[i]["ww_code"] = hdnHosww_code.Value.ToString();

            sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Hospital.xml";
            ds.WriteXml(Server.MapPath(sFile));
            hdnhosedit.Value = "";
            GridHospital.Enabled = true;
            BindGrid_Hospital("0");
         
        }
        else
        {     
            // Added by Sridevi to create Header - 02/Oct/15
            Create_Head(ddlSDP.SelectedValue.ToString());

            XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(Server.MapPath("Hospital.xml"));

            sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Hospital.xml";
            xmldoc.Load(Server.MapPath(sFile));

            XmlElement parentelement = xmldoc.CreateElement("DCR");

            XmlElement xmlhospital = xmldoc.CreateElement("hospital");
            XmlElement xmlhospitalww = xmldoc.CreateElement("hosww");
            XmlElement xmlpob = xmldoc.CreateElement("pob");
            XmlElement xmlhospital_code = xmldoc.CreateElement("hospital_code");
            XmlElement xmlww_code = xmldoc.CreateElement("ww_code");

            xmlhospital.InnerText = HosDDL.SelectedItem.Text.ToString();
            xmlhospital_code.InnerText = HosDDL.SelectedValue.ToString();
            xmlhospitalww.InnerText = HdnHos.Value.ToString(); //txtHosWW.Text.ToString();
            xmlpob.InnerText = txtHospPOB.Text.ToString();
            xmlww_code.InnerText = hdnHosww_code.Value.ToString();

            parentelement.AppendChild(xmlhospital);
            parentelement.AppendChild(xmlhospitalww);
            parentelement.AppendChild(xmlpob);
            parentelement.AppendChild(xmlhospital_code);
            parentelement.AppendChild(xmlww_code);

            xmldoc.DocumentElement.AppendChild(parentelement);
            xmldoc.Save(Server.MapPath(sFile));
            
            GridHospital.Enabled = true;
            BindGrid_Hospital("0");              
        }
        hoscolor();
    }

    protected void ddlcheMR_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds1 = (DataSet)ViewState["allterr"];
        DataSet ds2 = new DataSet();

        ds2 = ds1.Clone();
        if (ddlcheMR.SelectedIndex > 0)
        {
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                if (ds1.Tables[0].Rows[i].ItemArray[0].ToString() == "ALL" || ds1.Tables[0].Rows[i].ItemArray[0].ToString() == ddlcheMR.SelectedValue.Trim())
                {
                    ds2.Tables[0].ImportRow(ds1.Tables[0].Rows[i]);
                }
            }
            ddlTerr.DataTextField = "Territory_Name";
            ddlTerr.DataValueField = "Territory_Code";
            ddlTerr.DataSource = ds2;
            ddlTerr.DataBind();
            lblcheMR.Visible = true;
            ddlcheMR.Visible = true;
            PnlChem.Attributes.Add("style", "display:block");
            pnlMultiView.Enabled = false;
            pnlTab.Enabled = false;
            pnlTop.Enabled = false;
            pnlTab1.Enabled = false;
            checolor();
        }
        else
        {
            ddlTerr.DataTextField = "Territory_Name";
            ddlTerr.DataValueField = "Territory_Code";
            ddlTerr.DataSource = ds2;
            ddlTerr.DataBind();
        }
    }
    protected void ddlUnMR_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds1 = (DataSet)ViewState["allterr"];
        DataSet ds2 = new DataSet();

        ds2 = ds1.Clone();
        if (ddlUnMR.SelectedIndex > 0)
        {
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                if (ds1.Tables[0].Rows[i].ItemArray[0].ToString() == "ALL" || ds1.Tables[0].Rows[i].ItemArray[0].ToString() == ddlUnMR.SelectedValue.Trim())
                {
                    ds2.Tables[0].ImportRow(ds1.Tables[0].Rows[i]);
                }
            }
            ddlTerr_Un.DataTextField = "Territory_Name";
            ddlTerr_Un.DataValueField = "Territory_Code";
            ddlTerr_Un.DataSource = ds2;
            ddlTerr_Un.DataBind();
        }
        else
        {
            ddlTerr_Un.DataTextField = "Territory_Name";
            ddlTerr_Un.DataValueField = "Territory_Code";
            ddlTerr_Un.DataSource = ds2;
            ddlTerr_Un.DataBind();
        }
        //if (ddlUnMR.SelectedIndex > 0)
        //{
        //    FillTerrUn(ddlUnMR.SelectedValue.ToString());
        //}
    }



    protected void btnPreview_Click(object sender, EventArgs e)
    {
       // hdnbutname.Value = "6";
        lblInfo.Text = "";
        precolor();
        FillDoc();
        Preview_Chem();
        Preview_Stk();
        Fill_Review();
        FillUnlstDoc();
        Preview_Hos();   
        // Fill_Review();
    }


    protected void btnProdAdd_Click(object sender, EventArgs e)
    {
            hidProdClose.Value = "";
            lblInfo.Text = "";
            pnlProduct.Attributes.Add("style", "display:block");
            pnlProduct.CssClass = "pnladd";
            pnlMultiView.Enabled = false;
            pnlTab.Enabled = false;
            pnlTop.Enabled = false;
            pnlTab1.Enabled = false;
            if (hdndocedit.Value != "")
            {
                BindGrid("0");
                DataSet ds = (DataSet)gvDCR.DataSource;

                int j = gvDCR.Rows[Convert.ToInt16(hdndocedit.Value.ToString()) - 1].DataItemIndex;
                gvDCR.Rows[j].BackColor = Color.LimeGreen;
                string lblAddProd = ds.Tables[0].Rows[j]["AddProdCode"].ToString();
                string lblAddGift = ds.Tables[0].Rows[j]["AddGiftCode"].ToString();
                if (ViewState["AddProdExists"].ToString() != "Yes")
                {
                    if (lblAddProd != "")
                    {
                        DataTable dt = new DataTable();
                        DataRow dr = null;
                        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                        dt.Columns.Add(new DataColumn("Col1", typeof(string)));
                        dt.Columns.Add(new DataColumn("Col2", typeof(string)));
                        dr = dt.NewRow();
                        dr["RowNumber"] = 1;
                        dr["Col1"] = string.Empty;
                        dr["Col2"] = string.Empty;

                        dt.Rows.Add(dr);

                        grvProduct.DataSource = dt;
                        grvProduct.DataBind();


                        string[] addprod = lblAddProd.Split('#');
                        int rowIndex = 0;
                        foreach (string aprod in addprod)
                        {
                            //Levox~1$ # LAPP~2$#
                            if (aprod != "")
                            {
                                string prodcode = aprod.Substring(0, aprod.IndexOf("~")); //aprod.EndsWith('~');
                                //string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - aprod.IndexOf("~")));
                                string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - (prodcode.Length + 2)));
                                DataRow drCurrentRow = null;
                                DropDownList ddlProductAdd = (DropDownList)grvProduct.Rows[rowIndex].Cells[1].FindControl("ddlProductAdd");
                                TextBox txtProdQty = (TextBox)grvProduct.Rows[rowIndex].Cells[2].FindControl("txtProdQty");

                                drCurrentRow = dt.NewRow();
                                drCurrentRow["RowNumber"] = rowIndex;

                                ddlProductAdd.SelectedValue = prodcode;
                                txtProdQty.Text = Qty;
                                dt.Rows[rowIndex]["Col1"] = prodcode;
                                dt.Rows[rowIndex]["Col2"] = Qty;
                                rowIndex++;
                                dt.Rows.Add(drCurrentRow);
                                grvProduct.DataSource = dt;
                                grvProduct.DataBind();
                            }
                        }

                        if (dt.Rows.Count > 0)
                        {
                            rowIndex = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                DropDownList ddlProductAdd =
                                  (DropDownList)grvProduct.Rows[rowIndex].Cells[1].FindControl("ddlProductAdd");
                                TextBox txtProdQty =
                                  (TextBox)grvProduct.Rows[rowIndex].Cells[2].FindControl("txtProdQty");

                                ddlProductAdd.SelectedValue = dt.Rows[i]["Col1"].ToString();
                                txtProdQty.Text = dt.Rows[i]["Col2"].ToString();
                                rowIndex++;
                            }
                        }
                        if (dt.Rows.Count > 0)
                        {
                            ViewState["CurrentTable"] = dt;
                            ViewState["AddProdExists"] = "Yes";
                        }
                        else
                        {
                            ViewState["CurrentTable"] = dt;
                            ViewState["AddProdExists"] = "";
                        }
                    }
                }
            }
            else
            {
                SetPreviousData();
            }
            FirstGridViewRow();
            ldrcolor();
    }

    //protected void btnProductAdd_Click(object sender, EventArgs e)
    //{
    //    AddNewRowToGrid();
    //}

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {

        AddNewRow();
    }


    private void FirstGridViewRow()
    {
        if (ViewState["AddProdExists"].ToString() != "Yes")
        {
            DataTable dt = new DataTable();

            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Col1"] = "0";
            dr["Col2"] = string.Empty;

            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;

            grvProduct.DataSource = dt;
            grvProduct.DataBind();
        }

    }


    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlProductAdd =
                      (DropDownList)grvProduct.Rows[rowIndex].Cells[1].FindControl("ddlProductAdd");
                    TextBox txtProdQty =
                      (TextBox)grvProduct.Rows[rowIndex].Cells[2].FindControl("txtProdQty");

                    ddlProductAdd.SelectedValue = dt.Rows[i]["Col1"].ToString();
                    txtProdQty.Text = dt.Rows[i]["Col2"].ToString();
                    rowIndex++;
                }
                ViewState["AddProdExists"] = "Yes";
            }
            else
            {
                FirstGridViewRow();
            }
        }
    }

    protected void grvProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowData();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count >= 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();

                ViewState["CurrentTable"] = dt;
                grvProduct.DataSource = dt;
                grvProduct.DataBind();

                //for (int i = 0; i < grvProduct.Rows.Count-1; i++)
                //{
                //    grvProduct.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                //}
                SetPreviousData();

            }
        }
    }

    private void SetRowData()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList ddlProductAdd =
                      (DropDownList)grvProduct.Rows[rowIndex].Cells[1].FindControl("ddlProductAdd");
                    TextBox txtProdQty =
                      (TextBox)grvProduct.Rows[rowIndex].Cells[2].FindControl("txtProdQty");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Col1"] = ddlProductAdd.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = txtProdQty.Text;
                    rowIndex++;
                }
                ViewState["CurrentTable"] = dtCurrentTable;
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }

    private void AddNewRow()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList ddlProductAdd =
                      (DropDownList)grvProduct.Rows[rowIndex].Cells[1].FindControl("ddlProductAdd");
                    TextBox txtProdQty =
                      (TextBox)grvProduct.Rows[rowIndex].Cells[2].FindControl("txtProdQty");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = ddlProductAdd.SelectedValue.ToString();
                    dtCurrentTable.Rows[i - 1]["Col2"] = txtProdQty.Text;
                    rowIndex++;

                }
                dtCurrentTable.Rows.Add(drCurrentRow);

                if (dtCurrentTable.Rows.Count > 0)
                {
                    ViewState["AddProdExists"] = "Yes";
                }

                ViewState["CurrentTable"] = dtCurrentTable;
                grvProduct.DataSource = dtCurrentTable;
                grvProduct.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData();
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {

        // Duplicate Check
        SetRowData();
        int ierr = 0;
        DataTable dt = (DataTable)ViewState["CurrentTable"];
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((ddlProd1.SelectedValue == dt.Rows[i]["Col1"].ToString()) || (ddlProd2.SelectedValue == dt.Rows[i]["Col1"].ToString()) || (ddlProd3.SelectedValue == dt.Rows[i]["Col1"].ToString()))
                    {
                        ierr = 1;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Duplicate Product');", true);
                        break;
                    }
                    else
                    {
                        for (int j = i + 1; j < dt.Rows.Count; j++)
                        {
                            if ((dt.Rows[j]["Col1"].ToString() == dt.Rows[i]["Col1"].ToString()) && dt.Rows[i]["Col1"].ToString()!="0")
                            {
                                ierr = 1;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Duplicate Product');", true);
                                break;
                            }
                        }
                    }
                }
            }
        }
        if (ierr == 0)
        {      
            pnlProduct.Attributes.Add("style", "display:none");
            pnlMultiView.Enabled = true;
            pnlTab.Enabled = true;
            pnlTab1.Enabled = true;
            pnlTop.Enabled = true;
            if (hdndocedit.Value != "")
            {
                gvDCR.Enabled = false;
            }
            ldrcolor();
            // ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + pnlTab1.ClientID + "') .enabled = 'true';", true);
        }
    }

    protected void btnRmkDone_Click(object sender, EventArgs e)
    {
        //EnableDCR();

        if (txtRemarks.Text == "")
        {
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Remarks');</script>");
            //ScriptManager.RegisterStartupScript(this,this.GetType(),  "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Remarks');</script>",true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Please Enter Remarks');", true);
            // Response.Write("<script>alert('Please Enter Remarks') ; </script>");
        }
        else
        {
            pnlMultiView.Enabled = true;
            pnlMultiView.Enabled = true;
            pnlTab.Enabled = true;
            pnlTop.Enabled = true;
            pnlTab1.Enabled = true;

            if (hdndocedit.Value == "")
            {
                if (hdnValue.Value != "")
                    txtListDRCode.Text = hdnValue.Value;
            }
            PopulateDCR();
        }
    }

    //Enables DCR Creation by Sridevi on 08/15/15
    //private void EnableDCR()
    //{
    //   
    //    if (remarks_dcr == 0)
    //    {

    //    }
    //    else
    //        PopulateDCR();
    //}

    private void PopulateDCR()
    {
        // Added by Sridevi to get WW -Code

        txtWWCode.Value = "";
        for (int i = 0; i < chkFieldForce.Items.Count; i++)
        {
            if (chkFieldForce.Items[i].Selected)
            {
                txtWWCode.Value = txtWWCode.Value + chkFieldForce.Items[i].Value.ToString() + "$$";
            }
        }
        txtWWCode.Value = txtWWCode.Value.Substring(0, txtWWCode.Value.Length - 2);
        //ends

        // Edit Doctor
        if (hdndocedit.Value != "")
        {
            BindGrid("0");
            DataSet ds = (DataSet)gvDCR.DataSource;
            int i = gvDCR.Rows[Convert.ToInt16(hdndocedit.Value.ToString())-1].DataItemIndex;

            if (ddlSes.SelectedIndex > 0)
            {
                ds.Tables[0].Rows[i]["session"] = ddlSes.SelectedItem.Text.ToString();
                ds.Tables[0].Rows[i]["sess_code"] = ddlSes.SelectedValue.ToString();
            }
            else
            {
                ds.Tables[0].Rows[i]["session"] = "";
                ds.Tables[0].Rows[i]["sess_code"] = "0";
            }
            if (ddlTime.SelectedIndex > 0)
            {
                ds.Tables[0].Rows[i]["minute"] = ddlTime.SelectedValue.ToString();
            }
            else
            {
                ds.Tables[0].Rows[i]["minute"] = "";
            }
            if (ddlmin.SelectedIndex > 0)
            {
                ds.Tables[0].Rows[i]["seconds"] = ddlmin.SelectedValue.ToString();
            }
            else
            {
                ds.Tables[0].Rows[i]["seconds"] = "";
            }
            if (ddlTime.SelectedIndex > 0) 
            {
                ds.Tables[0].Rows[i]["time"] = ddlTime.SelectedItem.Text.ToString() + ":" + ddlmin.SelectedItem.ToString();
            }
            else
            {
                ds.Tables[0].Rows[i]["time"] = "";
            }
            ds.Tables[0].Rows[i]["dr_code"] = txtListDRCode.Text.Trim();
            ds.Tables[0].Rows[i]["drcode"] = txtListDR.Text.Trim();
            ds.Tables[0].Rows[i]["workwith"] = txtSFCode.Value.Trim();
            ds.Tables[0].Rows[i]["ww_code"] = txtWWCode.Value.Trim();

            if (ddlProd1.SelectedIndex > 0)
            {
                ds.Tables[0].Rows[i]["prod1"] = ddlProd1.SelectedItem.Text.ToString();
                ds.Tables[0].Rows[i]["prod1_code"] = ddlProd1.SelectedValue;
                ds.Tables[0].Rows[i]["qty1"] = txtProd1.Text.Trim();
                ds.Tables[0].Rows[i]["prod_pob1"] = txtProdPOB1.Text.Trim();
            }
            else
            {
                ds.Tables[0].Rows[i]["prod1"] = "";
                ds.Tables[0].Rows[i]["prod1_code"] = "0";
                ds.Tables[0].Rows[i]["qty1"] = "";
                ds.Tables[0].Rows[i]["prod_pob1"] = "";
            }
            if (ddlProd2.SelectedIndex > 0)
            {

                ds.Tables[0].Rows[i]["prod2"] = ddlProd2.SelectedItem.Text.ToString();
                ds.Tables[0].Rows[i]["prod2_code"] = ddlProd2.SelectedValue;
                ds.Tables[0].Rows[i]["qty2"] = txtProd2.Text.Trim();
                ds.Tables[0].Rows[i]["prod_pob2"] = txtProdPOB2.Text.Trim();

            }
            else
            {
                ds.Tables[0].Rows[i]["prod2"] = "";
                ds.Tables[0].Rows[i]["prod2_code"] = "0";
                ds.Tables[0].Rows[i]["qty2"] = "";
                ds.Tables[0].Rows[i]["prod_pob2"] = "";
            }
            if (ddlProd3.SelectedIndex > 0)
            {

                ds.Tables[0].Rows[i]["prod3"] = ddlProd3.SelectedItem.Text.ToString();
                ds.Tables[0].Rows[i]["prod3_code"] = ddlProd3.SelectedValue;
                ds.Tables[0].Rows[i]["qty3"] = txtProd3.Text.Trim();
                ds.Tables[0].Rows[i]["prod_pob3"] = txtProdPOB3.Text.Trim();

            }
            else
            {
                ds.Tables[0].Rows[i]["prod3"] = "";
                ds.Tables[0].Rows[i]["prod3_code"] = "0";
                ds.Tables[0].Rows[i]["qty3"] = "";
                ds.Tables[0].Rows[i]["prod_pob3"] = "";
            }
            if (ddlGift.SelectedIndex > 0)
            {
                ds.Tables[0].Rows[i]["gift"] = ddlGift.SelectedItem.Text.Trim();
                ds.Tables[0].Rows[i]["gift_code"] = ddlGift.SelectedValue.Trim();
                ds.Tables[0].Rows[i]["gqty"] = txtGift.Text.Trim();
            }
            else
            {
                ds.Tables[0].Rows[i]["gift"] = "";
                ds.Tables[0].Rows[i]["gift_code"] = "0";
                ds.Tables[0].Rows[i]["gqty"] = "";
            }

            //Additional Products if any

            if (ddlProd1.SelectedIndex > 0  && ddlProd2.SelectedIndex > 0  && ddlProd3.SelectedIndex > 0 )
            {

                string sProdName = string.Empty;
                string sQty = string.Empty;
                string sProducts = string.Empty;
                string sProdcode = string.Empty;
                string sProductcodes = string.Empty;
                if (ViewState["AddProdExists"].ToString() != "Yes")
                {
                    string lblAddProd = ds.Tables[0].Rows[i]["AddProdCode"].ToString();

                    if (lblAddProd != "")
                    {
                        DataTable dt = new DataTable();
                        DataRow dr = null;
                        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                        dt.Columns.Add(new DataColumn("Col1", typeof(string)));
                        dt.Columns.Add(new DataColumn("Col2", typeof(string)));
                        dr = dt.NewRow();
                        dr["RowNumber"] = 1;
                        dr["Col1"] = string.Empty;
                        dr["Col2"] = string.Empty;

                        dt.Rows.Add(dr);

                        grvProduct.DataSource = dt;
                        grvProduct.DataBind();


                        string[] addprod = lblAddProd.Split('#');
                        int rowIndex = 0;
                        foreach (string aprod in addprod)
                        {
                            //Levox~1$ # LAPP~2$#
                            if (aprod != "")
                            {
                                string prodcode = aprod.Substring(0, aprod.IndexOf("~")); //aprod.EndsWith('~');
                                //string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - aprod.IndexOf("~")));
                                string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - (prodcode.Length + 2)));
                                DataRow drCurrentRow = null;
                                DropDownList ddlProductAdd = (DropDownList)grvProduct.Rows[rowIndex].Cells[1].FindControl("ddlProductAdd");
                                TextBox txtProdQty = (TextBox)grvProduct.Rows[rowIndex].Cells[2].FindControl("txtProdQty");

                                drCurrentRow = dt.NewRow();
                                drCurrentRow["RowNumber"] = rowIndex;

                                ddlProductAdd.SelectedValue = prodcode;
                                txtProdQty.Text = Qty;
                                dt.Rows[rowIndex]["Col1"] = prodcode;
                                dt.Rows[rowIndex]["Col2"] = Qty;
                                rowIndex++;
                                dt.Rows.Add(drCurrentRow);
                                grvProduct.DataSource = dt;
                                grvProduct.DataBind();
                            }
                        }
                        if (dt.Rows.Count > 0)
                        {
                            rowIndex = 0;
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                DropDownList ddlProductAdd =
                                  (DropDownList)grvProduct.Rows[rowIndex].Cells[1].FindControl("ddlProductAdd");
                                TextBox txtProdQty =
                                  (TextBox)grvProduct.Rows[rowIndex].Cells[2].FindControl("txtProdQty");

                                ddlProductAdd.SelectedValue = dt.Rows[j]["Col1"].ToString();
                                txtProdQty.Text = dt.Rows[j]["Col2"].ToString();
                                rowIndex++;
                            }
                        }
                    }
                }
                foreach (GridViewRow gridRow in grvProduct.Rows)
                {
                    DropDownList ddlProductAdd = (DropDownList)gridRow.Cells[1].FindControl("ddlProductAdd");
                    sProdName = ddlProductAdd.SelectedItem.Text.ToString();
                    sProdcode = ddlProductAdd.SelectedValue.Trim();

                    TextBox txtProdQty = (TextBox)gridRow.Cells[2].FindControl("txtProdQty");
                    sQty = txtProdQty.Text.ToString();

                    if (ddlProductAdd.SelectedIndex != 0)
                    {
                        if (sProdName.Trim().Length > 0)
                        {
                            sProducts = sProducts + sProdName + "~" + sQty + "$" + "#";
                            sProductcodes = sProductcodes + sProdcode + "~" + sQty + "$" + "#";
                        }
                    }
                }
                ds.Tables[0].Rows[i]["AddProdCode"] = sProductcodes;
                ds.Tables[0].Rows[i]["AddProd"] = sProducts;

                if (sProducts.Trim().Length > 0)
                {
                    ViewState["AddProdExists"] = "";
                }

            }

            //Additional Gifts if any
            if (ddlGift.SelectedIndex > 0 && txtGift.Text.Trim().Length > 0)
            {
                string sGiftName = string.Empty;
                string sGiftQty = string.Empty;
                string sGifts = string.Empty;
                string sGiftcodedtls = string.Empty;
                string sGiftcode = string.Empty;
                if (ViewState["AddGiftExists"].ToString() != "Yes")
                {
                    string lblAddGift = ds.Tables[0].Rows[i]["AddGiftCode"].ToString();
                    if (lblAddGift != "")
                    {
                        DataTable dt = new DataTable();
                        DataRow dr = null;
                        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                        dt.Columns.Add(new DataColumn("Col1", typeof(string)));
                        dt.Columns.Add(new DataColumn("Col2", typeof(string)));
                        dr = dt.NewRow();
                        dr["RowNumber"] = 1;
                        dr["Col1"] = string.Empty;
                        dr["Col2"] = string.Empty;

                        dt.Rows.Add(dr);

                        grdGift.DataSource = dt;
                        grdGift.DataBind();


                        string[] addgift = lblAddGift.Split('#');
                        int rowIndex = 0;
                        foreach (string agift in addgift)
                        {

                            if (agift != "")
                            {
                                string giftcode = agift.Substring(0, agift.IndexOf("~"));
                                string Qty = agift.Substring(agift.IndexOf("~") + 1, (agift.Length - (giftcode.Length + 2)));

                                DataRow drCurrentRow = null;
                                DropDownList ddlGiftAdd = (DropDownList)grdGift.Rows[rowIndex].Cells[1].FindControl("ddlGiftAdd");
                                TextBox txtGiftQty = (TextBox)grdGift.Rows[rowIndex].Cells[2].FindControl("txtGiftQty");

                                drCurrentRow = dt.NewRow();
                                drCurrentRow["RowNumber"] = rowIndex;

                                ddlGiftAdd.SelectedValue = giftcode;
                                txtGiftQty.Text = Qty;
                                dt.Rows[rowIndex]["Col1"] = giftcode;
                                dt.Rows[rowIndex]["Col2"] = Qty;
                                rowIndex++;
                                dt.Rows.Add(drCurrentRow);
                                grdGift.DataSource = dt;
                                grdGift.DataBind();
                            }
                        }

                        if (dt.Rows.Count > 0)
                        {
                            rowIndex = 0;
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                DropDownList ddlGiftAdd = (DropDownList)grdGift.Rows[rowIndex].Cells[1].FindControl("ddlGiftAdd");
                                TextBox txtGiftQty = (TextBox)grdGift.Rows[rowIndex].Cells[2].FindControl("txtGiftQty");

                                ddlGiftAdd.SelectedValue = dt.Rows[j]["Col1"].ToString();
                                txtGiftQty.Text = dt.Rows[j]["Col2"].ToString();
                                rowIndex++;
                            }
                        }
                    }
                }
                foreach (GridViewRow gridRow in grdGift.Rows)
                {
                    DropDownList ddlGiftAdd =
                     (DropDownList)gridRow.Cells[1].FindControl("ddlGiftAdd");
                    sGiftName = ddlGiftAdd.SelectedItem.Text.ToString();
                    sGiftcode = ddlGiftAdd.SelectedValue.ToString();
                    TextBox txtGiftQty =
                      (TextBox)gridRow.Cells[2].FindControl("txtGiftQty");
                    sGiftQty = txtGiftQty.Text.ToString();
                    if (ddlGiftAdd.SelectedIndex != 0)
                    {
                        if (sGiftName.Trim().Length > 0)
                        {
                            sGifts = sGifts + sGiftName + "~" + sGiftQty + "$" + "#"; ;
                            sGiftcodedtls = sGiftcodedtls + sGiftcode + "~" + sGiftQty + "$" + "#"; ;
                        }
                    }
                }
                ds.Tables[0].Rows[i]["AddGift"] = sGifts;
                ds.Tables[0].Rows[i]["AddGiftCode"] = sGiftcodedtls;

                if (sGifts.Trim().Length > 0)
                {
                    ViewState["AddGiftExists"] = "";

                }

            }
            ds.Tables[0].Rows[i]["remarks"] = txtRemarks.Text;
            sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_ListedDR.xml";
            ds.WriteXml(Server.MapPath(sFile));
            hdndocedit.Value = "";
            gvDCR.Enabled = true;
            BindGrid("0");

        }
        else
        {
            if (hdnValue.Value != "")
                txtListDRCode.Text = hdnValue.Value;

            // Creates an XML for Listed Doctor

            //sFile = sf_code + sCurDate + "ListedDR.xml";

            // Added by Sridevi to create Header - 02/Oct/15
            Create_Head(ddlSDP.SelectedValue.ToString());

            sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_ListedDR.xml";

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Server.MapPath(sFile));

            XmlElement parentelement = xmldoc.CreateElement("DCR");

            XmlElement xmlsession = xmldoc.CreateElement("session");
            XmlElement xmltime = xmldoc.CreateElement("time");

            XmlElement xmlDR = xmldoc.CreateElement("drcode");
            XmlElement xmlworkwith = xmldoc.CreateElement("workwith");

            XmlElement xmlprod1 = xmldoc.CreateElement("prod1");
            XmlElement xmlqty1 = xmldoc.CreateElement("qty1");
            XmlElement xmlprod_pob1 = xmldoc.CreateElement("prod_pob1");
            XmlElement xmlprod2 = xmldoc.CreateElement("prod2");
            XmlElement xmlqty2 = xmldoc.CreateElement("qty2");
            XmlElement xmlprod_pob2 = xmldoc.CreateElement("prod_pob2");
            XmlElement xmlprod3 = xmldoc.CreateElement("prod3");
            XmlElement xmlqty3 = xmldoc.CreateElement("qty3");
            XmlElement xmlprod_pob3 = xmldoc.CreateElement("prod_pob3");
            XmlElement xmlAddProd = xmldoc.CreateElement("AddProd");
            XmlElement xmlAddProdCode = xmldoc.CreateElement("AddProdCode");
            XmlElement xmlgift = xmldoc.CreateElement("gift");
            XmlElement xmlgqty = xmldoc.CreateElement("gqty");
            XmlElement xmlAddGift = xmldoc.CreateElement("AddGift");
            XmlElement xmlAddGiftCode = xmldoc.CreateElement("AddGiftCode");
            XmlElement xmldr_code = xmldoc.CreateElement("dr_code");
            XmlElement xmlsf_code = xmldoc.CreateElement("sf_code");
            XmlElement xmlsess_code = xmldoc.CreateElement("sess_code");
            XmlElement xmlminute = xmldoc.CreateElement("minute");
            XmlElement xmlseconds = xmldoc.CreateElement("seconds");
            XmlElement xmlprod1_code = xmldoc.CreateElement("prod1_code");
            XmlElement xmlprod2_code = xmldoc.CreateElement("prod2_code");
            XmlElement xmlprod3_code = xmldoc.CreateElement("prod3_code");
            XmlElement xmlgiftcode = xmldoc.CreateElement("gift_code");
            XmlElement xmlremarks = xmldoc.CreateElement("remarks");
            XmlElement xmlww_code = xmldoc.CreateElement("ww_code");
            if (ddlSes.SelectedIndex > 0)
            {
                xmlsession.InnerText = ddlSes.SelectedItem.Text.ToString();
                xmlsess_code.InnerText = ddlSes.SelectedValue.Trim();
            }
            else
            {
                xmlsession.InnerText = "";
                xmlsess_code.InnerText = "0";
            }

            if (ddlTime.SelectedIndex > 0)
            {
                xmltime.InnerText = ddlTime.SelectedItem.Text.ToString() + ":" + ddlmin.SelectedItem.ToString();
                xmlminute.InnerText = ddlTime.SelectedItem.Text.ToString();
            }
            else
            {
                xmltime.InnerText = "";
                xmlminute.InnerText = "";
            }
            if (ddlmin.SelectedIndex > 0)
            {
                xmlseconds.InnerText = ddlmin.SelectedItem.Text.ToString();
            }
            else
            {
                xmlseconds.InnerText = "";
            }


            xmlDR.InnerText = txtListDR.Text.Trim();// ddlListedDR.SelectedItem.Text.ToString();

            if (txtListDRCode.Text.Trim().Length > 0)
            {
                xmldr_code.InnerText = txtListDRCode.Text.Trim();//ddlListedDR.SelectedValue.ToString();
            }
         

            xmlworkwith.InnerText = txtSFCode.Value.ToString(); //txtFieldForce.Text.ToString();
            xmlsf_code.InnerText = txtSFCode.Value.ToString(); //txtFieldForce.Text.ToString();
            xmlww_code.InnerText = txtWWCode.Value.ToString();

            if (ddlProd1.SelectedItem.Text.ToString() == "-Product-")
            {
                xmlprod1.InnerText = "";
                xmlprod1_code.InnerText = "0";
                xmlqty1.InnerText = "";
                xmlprod_pob1.InnerText = "";
            }
            else
            {

                xmlprod1.InnerText = ddlProd1.SelectedItem.Text.ToString();
                xmlprod1_code.InnerText = ddlProd1.SelectedValue.Trim();
                xmlqty1.InnerText = txtProd1.Text;
                xmlprod_pob1.InnerText = txtProdPOB1.Text;

            }

            if (ddlProd2.SelectedItem.Text.ToString() == "-Product-")
            {
                xmlprod2.InnerText = "";
                xmlprod2_code.InnerText = "0";
                xmlqty2.InnerText = "";
                xmlprod_pob2.InnerText = "";
            }
            else
            {

                xmlprod2.InnerText = ddlProd2.SelectedItem.Text.ToString();
                xmlprod2_code.InnerText = ddlProd2.SelectedValue.Trim();
                xmlqty2.InnerText = txtProd2.Text;
                xmlprod_pob2.InnerText = txtProdPOB2.Text;

            }

            if (ddlProd3.SelectedItem.Text.ToString() == "-Product-")
            {
                xmlprod3.InnerText = "";
                xmlqty3.InnerText = "";
                xmlprod3_code.InnerText = "0";
                xmlprod_pob3.InnerText = "";
            }
            else
            {
                xmlprod3.InnerText = ddlProd3.SelectedItem.Text.ToString();
                xmlprod3_code.InnerText = ddlProd3.SelectedValue.Trim();
                xmlqty3.InnerText = txtProd3.Text;
                xmlprod_pob3.InnerText = txtProdPOB3.Text;

            }

            if (ddlProd1.SelectedIndex > 0 && ddlProd2.SelectedIndex > 0 &&  ddlProd3.SelectedIndex > 0 )
            {

                string sProdName = string.Empty;
                string sQty = string.Empty;
                string sProducts = string.Empty;
                string sProdcode = string.Empty;
                string sProductcodes = string.Empty;

                foreach (GridViewRow gridRow in grvProduct.Rows)
                {
                    DropDownList ddlProductAdd = (DropDownList)gridRow.Cells[1].FindControl("ddlProductAdd");
                    sProdName = ddlProductAdd.SelectedItem.Text.ToString();
                    sProdcode = ddlProductAdd.SelectedValue.Trim();
                    TextBox txtProdQty = (TextBox)gridRow.Cells[2].FindControl("txtProdQty");
                    sQty = txtProdQty.Text.ToString();
                    if (ddlProductAdd.SelectedIndex != 0)
                    {
                        if (sProdName.Trim().Length > 0)
                        {
                            sProducts = sProducts + sProdName + "~" + sQty + "$" + "#";
                            sProductcodes = sProductcodes + sProdcode + "~" + sQty + "$" + "#";
                        }
                    }

                }
                xmlAddProd.InnerText = sProducts;
                xmlAddProdCode.InnerText = sProductcodes;

                if (sProducts.Trim().Length > 0)
                {
                    ViewState["AddProdExists"] = "";
                }
            }


            if (ddlGift.SelectedItem.Text.ToString() == "-Select-")
            {
                xmlgift.InnerText = "";
                xmlgqty.InnerText = "";
                xmlgiftcode.InnerText = "0";
            }
            else
            {
                xmlgift.InnerText = ddlGift.SelectedItem.Text.ToString();
                xmlgqty.InnerText = txtGift.Text;
                xmlgiftcode.InnerText = ddlGift.SelectedValue.Trim();
            }
            if (ddlGift.SelectedIndex > 0)
            {

                //   FirstGridViewRow_Gift();
                string sGiftName = string.Empty;
                string sGiftQty = string.Empty;
                string sGifts = string.Empty;
                string sGiftcodedtls = string.Empty;
                string sGiftcode = string.Empty;
                foreach (GridViewRow gridRow in grdGift.Rows)
                {
                    DropDownList ddlGiftAdd =
                     (DropDownList)gridRow.Cells[1].FindControl("ddlGiftAdd");
                    sGiftName = ddlGiftAdd.SelectedItem.Text.ToString();
                    sGiftcode = ddlGiftAdd.SelectedValue.ToString();
                    TextBox txtGiftQty =
                      (TextBox)gridRow.Cells[2].FindControl("txtGiftQty");
                    sGiftQty = txtGiftQty.Text.ToString();
                    if (ddlGiftAdd.SelectedIndex != 0)
                    {
                        if (sGiftName.Trim().Length > 0)
                        {
                            sGifts = sGifts + sGiftName + "~" + sGiftQty + "$" + "#"; ;
                            sGiftcodedtls = sGiftcodedtls + sGiftcode + "~" + sGiftQty + "$" + "#"; ;
                        }
                    }
                }
                xmlAddGift.InnerText = sGifts;
                xmlAddGiftCode.InnerText = sGiftcodedtls;

                if (sGifts.Trim().Length > 0)
                {
                    ViewState["AddGiftExists"] = "";
                }

            }

            xmlremarks.InnerText = txtRemarks.Text;

            parentelement.AppendChild(xmlsession);
            parentelement.AppendChild(xmltime);
            parentelement.AppendChild(xmlDR);
            parentelement.AppendChild(xmlworkwith);
            parentelement.AppendChild(xmlprod1);
            parentelement.AppendChild(xmlqty1);
            parentelement.AppendChild(xmlprod_pob1);
            parentelement.AppendChild(xmlprod2);
            parentelement.AppendChild(xmlqty2);
            parentelement.AppendChild(xmlprod_pob2);
            parentelement.AppendChild(xmlprod3);
            parentelement.AppendChild(xmlqty3);
            parentelement.AppendChild(xmlprod_pob3);
            parentelement.AppendChild(xmlAddProd);
            parentelement.AppendChild(xmlAddProdCode);
            parentelement.AppendChild(xmlgift);
            parentelement.AppendChild(xmlgqty);
            parentelement.AppendChild(xmlAddGift);
            parentelement.AppendChild(xmlAddGiftCode);
            parentelement.AppendChild(xmldr_code);
            parentelement.AppendChild(xmlsf_code);
            parentelement.AppendChild(xmlsess_code);
            parentelement.AppendChild(xmlminute);
            parentelement.AppendChild(xmlseconds);
            parentelement.AppendChild(xmlprod1_code);
            parentelement.AppendChild(xmlprod2_code);
            parentelement.AppendChild(xmlprod3_code);
            parentelement.AppendChild(xmlgiftcode);
            parentelement.AppendChild(xmlremarks);
            parentelement.AppendChild(xmlww_code);

            xmldoc.DocumentElement.AppendChild(parentelement);      
            xmldoc.Save(Server.MapPath(sFile));
            BindGrid("0");
            hdnValue.Value = "";
              
        }
        ViewState["CurrentTable"] = null;
        ViewState["CurrentTableGift"] = null;         
        ldrcolor();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlProduct.Attributes.Add("style", "display:none");
       
        pnlMultiView.Enabled = true;
        pnlTab.Enabled = true;
        pnlTab1.Enabled = true;
        pnlTop.Enabled = true;

        ldrcolor();
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + pnlTab1.ClientID + "') .enabled = 'true';", true);
    }

    //Additional Gifts
    protected void btnGiftAdd_Click(object sender, EventArgs e)
    {
            lblInfo.Text = "";
            hidGiftClose.Value = "";
            pnlGift.Attributes.Add("style", "display:block");
            pnlGift.CssClass = "pnladd";
            pnlMultiView.Enabled = false;
            pnlTab.Enabled = false;
            pnlTop.Enabled = false;
            pnlTab1.Enabled = false;
            if (hdndocedit.Value != "")
            {
                BindGrid("0");
                DataSet ds = (DataSet)gvDCR.DataSource;

                int j = gvDCR.Rows[Convert.ToInt16(hdndocedit.Value.ToString()) - 1].DataItemIndex;
                gvDCR.Rows[j].BackColor = Color.LimeGreen;

                string lblAddGift = ds.Tables[0].Rows[j]["AddGiftCode"].ToString();
                if (ViewState["AddGiftExists"].ToString() != "Yes")
                {
                    if (lblAddGift != "")
                    {
                        DataTable dt = new DataTable();
                        DataRow dr = null;
                        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                        dt.Columns.Add(new DataColumn("Col1", typeof(string)));
                        dt.Columns.Add(new DataColumn("Col2", typeof(string)));
                        dr = dt.NewRow();
                        dr["RowNumber"] = 1;
                        dr["Col1"] = string.Empty;
                        dr["Col2"] = string.Empty;

                        dt.Rows.Add(dr);

                        grdGift.DataSource = dt;
                        grdGift.DataBind();


                        string[] addgift = lblAddGift.Split('#');
                        int rowIndex = 0;
                        foreach (string agift in addgift)
                        {

                            if (agift != "")
                            {
                                string giftcode = agift.Substring(0, agift.IndexOf("~"));
                                string Qty = agift.Substring(agift.IndexOf("~") + 1, (agift.Length - (giftcode.Length + 2)));

                                DataRow drCurrentRow = null;
                                DropDownList ddlGiftAdd = (DropDownList)grdGift.Rows[rowIndex].Cells[1].FindControl("ddlGiftAdd");
                                TextBox txtGiftQty = (TextBox)grdGift.Rows[rowIndex].Cells[2].FindControl("txtGiftQty");

                                drCurrentRow = dt.NewRow();
                                drCurrentRow["RowNumber"] = rowIndex;

                                ddlGiftAdd.SelectedValue = giftcode;
                                txtGiftQty.Text = Qty;
                                dt.Rows[rowIndex]["Col1"] = giftcode;
                                dt.Rows[rowIndex]["Col2"] = Qty;
                                rowIndex++;
                                dt.Rows.Add(drCurrentRow);
                                grdGift.DataSource = dt;
                                grdGift.DataBind();
                            }
                        }

                        if (dt.Rows.Count > 0)
                        {
                            rowIndex = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                DropDownList ddlGiftAdd = (DropDownList)grdGift.Rows[rowIndex].Cells[1].FindControl("ddlGiftAdd");
                                TextBox txtGiftQty = (TextBox)grdGift.Rows[rowIndex].Cells[2].FindControl("txtGiftQty");

                                ddlGiftAdd.SelectedValue = dt.Rows[i]["Col1"].ToString();
                                txtGiftQty.Text = dt.Rows[i]["Col2"].ToString();
                                rowIndex++;
                            }
                        }
                        if (dt.Rows.Count > 0)
                        {
                            ViewState["CurrentTableGift"] = dt;
                            ViewState["AddGiftExists"] = "Yes";
                        }

                    }
                }
            }
            else
            {
                SetPreviousData_Gift();
            }
           
            FirstGridViewRow_Gift();
            ldrcolor();
       
    }

    protected void ButtonAddGift_Click(object sender, EventArgs e)
    {

        AddNewRow_Gift();
    }

      private void FirstGridViewRow_Gift()
    {
        if (ViewState["AddGiftExists"].ToString() != "Yes")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Col1"] = "0";
            dr["Col2"] = "1";
            dt.Rows.Add(dr);

            ViewState["CurrentTableGift"] = dt;

            grdGift.DataSource = dt;
            grdGift.DataBind();
        }
    }

    private void SetPreviousData_Gift()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableGift"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableGift"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlGiftAdd =
                      (DropDownList)grdGift.Rows[rowIndex].Cells[1].FindControl("ddlGiftAdd");
                    TextBox txtGiftQty =
                      (TextBox)grdGift.Rows[rowIndex].Cells[2].FindControl("txtGiftQty");

                    ddlGiftAdd.SelectedValue = dt.Rows[i]["Col1"].ToString();
                    txtGiftQty.Text = dt.Rows[i]["Col2"].ToString();
                    rowIndex++;
                }
                ViewState["AddGiftExists"] = "Yes";
            }
        }
    }

    protected void grdGift_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowDataGift();
        if (ViewState["CurrentTableGift"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableGift"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTableGift"] = dt;
                grdGift.DataSource = dt;
                grdGift.DataBind();

                //for (int i = 0; i < grdGift.Rows.Count - 1; i++)
                //{
                //    grdGift.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                //}
                SetPreviousData_Gift();
            }
        }
    }

    private void SetRowDataGift()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTableGift"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableGift"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList ddlGiftAdd =
                      (DropDownList)grdGift.Rows[rowIndex].Cells[1].FindControl("ddlGiftAdd");
                    TextBox txtGiftQty =
                      (TextBox)grdGift.Rows[rowIndex].Cells[2].FindControl("txtGiftQty");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Col1"] = ddlGiftAdd.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = txtGiftQty.Text;
                    rowIndex++;
                }

                ViewState["CurrentTableGift"] = dtCurrentTable;
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }

    private void AddNewRow_Gift()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTableGift"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableGift"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList ddlGiftAdd =
                      (DropDownList)grdGift.Rows[rowIndex].Cells[1].FindControl("ddlGiftAdd");
                    TextBox txtGiftQty =
                      (TextBox)grdGift.Rows[rowIndex].Cells[2].FindControl("txtGiftQty");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Col1"] = ddlGiftAdd.SelectedValue.ToString();
                    dtCurrentTable.Rows[i - 1]["Col2"] = txtGiftQty.Text;
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);

                if (dtCurrentTable.Rows.Count > 0)
                {
                    ViewState["AddGiftExists"] = "Yes";
                }

                ViewState["CurrentTableGift"] = dtCurrentTable;

                grdGift.DataSource = dtCurrentTable;
                grdGift.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData_Gift();
    }

    protected void btnGiftDone_Click(object sender, EventArgs e)
    {

        SetRowDataGift();
        int ierr = 0;
        DataTable dt = (DataTable)ViewState["CurrentTableGift"];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (ddlGift.SelectedValue == dt.Rows[i]["Col1"].ToString())
                {
                    ierr = 1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Duplicate Input');", true);
                    break;
                }
                else
                {
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["Col1"].ToString() == dt.Rows[i]["Col1"].ToString())
                        {
                            ierr = 1;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Duplicate Input');", true);
                            break;
                        }
                    }
                }
            }
        }
        if (ierr == 0)
        {
                pnlMultiView.Enabled = true;
                pnlTab.Enabled = true;
                pnlTab1.Enabled = true;
                pnlTop.Enabled = true;           
                pnlGift.Attributes.Add("style", "display:none");
                if (hdndocedit.Value != "")
                {
                    gvDCR.Enabled = false;
                }
            ldrcolor();
        }

    }
    protected void btnGiftCnl_Click(object sender, EventArgs e)
    {
        pnlGift.Attributes.Add("style", "display:none");
      
        pnlMultiView.Enabled = true;
        pnlTab.Enabled = true;
        pnlTab1.Enabled = true;
        pnlTop.Enabled = true;
       
        ldrcolor();
    }

    //Unlisted Doctor Additional Products

    protected void btnUnProdAdd_Click(object sender, EventArgs e)
    {
            hidUnlstProdClose.Value = "";
            lblInfo.Text = "";

            pnlProduct_Unlst.Attributes.Add("style", "display:block");
            pnlProduct_Unlst.CssClass = "pnladd";
            pnlMultiView.Enabled = false;
            pnlMultiView.Enabled = false;
            pnlTab.Enabled = false;
            pnlTop.Enabled = false;
            if ((hdnundocedit.Value != "") || (hdnNewUnDoc.Value != ""))
            {
                BindGrid_UnListedDR("0");
                int j = -1;
                DataSet ds = (DataSet)grdUnLstDR.DataSource;
                if (hdnundocedit.Value != "")
                    j = grdUnLstDR.Rows[Convert.ToInt16(hdnundocedit.Value.ToString()) - 1].DataItemIndex;
                else if (hdnNewUnDoc.Value != "")
                    j = grdUnLstDR.Rows[Convert.ToInt16(hdnNewUnDoc.Value.ToString()) - 1].DataItemIndex;
                grdUnLstDR.Rows[j].BackColor = Color.LimeGreen;
                string lblAddProd = ds.Tables[0].Rows[j]["AddProdCode"].ToString();
                //string lblAddGift = ds.Tables[0].Rows[j]["AddGiftCode"].ToString();
                if (ViewState["UnlstAddProdExists"].ToString() != "Yes")
                {
                    if (lblAddProd != "")
                    {
                        DataTable dt = new DataTable();
                        DataRow dr = null;
                        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                        dt.Columns.Add(new DataColumn("Col1", typeof(string)));
                        dt.Columns.Add(new DataColumn("Col2", typeof(string)));
                        dr = dt.NewRow();
                        dr["RowNumber"] = 1;
                        dr["Col1"] = string.Empty;
                        dr["Col2"] = string.Empty;

                        dt.Rows.Add(dr);

                        grvProductUnlst.DataSource = dt;
                        grvProductUnlst.DataBind();


                        string[] addprod = lblAddProd.Split('#');
                        int rowIndex = 0;
                        foreach (string aprod in addprod)
                        {
                            if (aprod != "")
                            {
                                string prodcode = aprod.Substring(0, aprod.IndexOf("~"));
                                string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - (prodcode.Length + 2)));
                                DataRow drCurrentRow = null;
                                DropDownList ddlProductUnlstAdd = (DropDownList)grvProductUnlst.Rows[rowIndex].Cells[1].FindControl("ddlProductUnlstAdd");
                                TextBox txtProdUnlstQty = (TextBox)grvProductUnlst.Rows[rowIndex].Cells[2].FindControl("txtProdUnlstQty");

                                drCurrentRow = dt.NewRow();
                                drCurrentRow["RowNumber"] = rowIndex;

                                ddlProductUnlstAdd.SelectedValue = prodcode;
                                txtProdUnlstQty.Text = Qty;
                                dt.Rows[rowIndex]["Col1"] = prodcode;
                                dt.Rows[rowIndex]["Col2"] = Qty;
                                rowIndex++;
                                dt.Rows.Add(drCurrentRow);
                                grvProductUnlst.DataSource = dt;
                                grvProductUnlst.DataBind();
                            }
                        }

                        if (dt.Rows.Count > 0)
                        {
                            rowIndex = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                DropDownList ddlProductUnlstAdd =
                                  (DropDownList)grvProductUnlst.Rows[rowIndex].Cells[1].FindControl("ddlProductUnlstAdd");
                                TextBox txtProdUnlstQty =
                                  (TextBox)grvProductUnlst.Rows[rowIndex].Cells[2].FindControl("txtProdUnlstQty");

                                ddlProductUnlstAdd.SelectedValue = dt.Rows[i]["Col1"].ToString();
                                txtProdUnlstQty.Text = dt.Rows[i]["Col2"].ToString();
                                rowIndex++;
                            }
                        }
                        if (dt.Rows.Count > 0)
                        {
                            ViewState["CurrentTableUnlst"] = dt;
                            ViewState["UnlstAddProdExists"] = "Yes";
                        }
                        else
                        {
                            ViewState["CurrentTableUnlst"] = dt;
                            ViewState["UnlstAddProdExists"] = "";
                        }
                    }
                }
            }
            else
            {
                SetPreviousDataUnlst();
            }
            FirstGridViewRowUnlst();
            udrcolor();
       
    }

    //protected void btnProductAdd_Click(object sender, EventArgs e)
    //{
    //    AddNewRowToGrid();
    //}

    protected void ButtonAddUnlst_Click(object sender, EventArgs e)
    {

        AddNewRow_UnLst();
    }

    private void FirstGridViewRowUnlst()
    {
        if (ViewState["UnlstAddProdExists"].ToString() != "Yes")
        {

            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Col1"] = "0";
            dr["Col2"] = string.Empty;
            dt.Rows.Add(dr);

            ViewState["CurrentTableUnlst"] = dt;
            grvProductUnlst.DataSource = dt;
            grvProductUnlst.DataBind();
        }
    }

    private void SetPreviousDataUnlst()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableUnlst"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableUnlst"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlUnLstDR_Prod1uctAdd =
                      (DropDownList)grvProductUnlst.Rows[rowIndex].Cells[1].FindControl("ddlProductUnlstAdd");
                    TextBox txtUnLstDR_Prod_QtyQty =
                      (TextBox)grvProductUnlst.Rows[rowIndex].Cells[2].FindControl("txtProdUnlstQty");

                    ddlUnLstDR_Prod1uctAdd.SelectedValue = dt.Rows[i]["Col1"].ToString();
                    txtUnLstDR_Prod_QtyQty.Text = dt.Rows[i]["Col2"].ToString();
                    rowIndex++;
                }
                ViewState["UnlstAddProdExists"] = "Yes";
            }
            else
            {
                FirstGridViewRowUnlst();
            }
        }
    }

    protected void grvProductUnlst_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowData();
        if (ViewState["CurrentTableUnlst"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableUnlst"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTableUnlst"] = dt;
                grvProductUnlst.DataSource = dt;
                grvProductUnlst.DataBind();

                //for (int i = 0; i < grvProductUnlst.Rows.Count - 1; i++)
                //{
                //    grvProductUnlst.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                //}
                SetPreviousDataUnlst();
            }
        }
    }

    private void SetRowDataUnlst()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTableUnlst"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableUnlst"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList ddlUnLstDR_Prod1uctAdd =
                      (DropDownList)grvProductUnlst.Rows[rowIndex].Cells[1].FindControl("ddlProductUnlstAdd");
                    TextBox txtUnLstDR_Prod_QtyQty =
                      (TextBox)grvProductUnlst.Rows[rowIndex].Cells[2].FindControl("txtProdUnlstQty");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Col1"] = ddlUnLstDR_Prod1uctAdd.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = txtUnLstDR_Prod_QtyQty.Text;
                    rowIndex++;
                }

                ViewState["CurrentTableUnlst"] = dtCurrentTable;
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }

    private void AddNewRow_UnLst()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTableUnlst"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableUnlst"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList ddlUnLstDR_Prod1uctAdd =
                      (DropDownList)grvProductUnlst.Rows[rowIndex].Cells[1].FindControl("ddlProductUnlstAdd");
                    TextBox txtUnLstDR_Prod_QtyQty =
                      (TextBox)grvProductUnlst.Rows[rowIndex].Cells[2].FindControl("txtProdUnlstQty");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = ddlUnLstDR_Prod1uctAdd.SelectedValue.ToString();
                    dtCurrentTable.Rows[i - 1]["Col2"] = txtUnLstDR_Prod_QtyQty.Text;
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTableUnlst"] = dtCurrentTable;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    ViewState["UnlstAddProdExists"] = "Yes";
                }
                grvProductUnlst.DataSource = dtCurrentTable;
                grvProductUnlst.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataUnlst();
    }

    protected void btnDoneUnlst_Click(object sender, EventArgs e)
    {

        SetRowDataUnlst();
        int ierr = 0;
        DataTable dt = (DataTable)ViewState["CurrentTableUnlst"];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((ddlUnLstDR_Prod1.SelectedValue == dt.Rows[i]["Col1"].ToString()) || (ddlUnLstDR_Prod2.SelectedValue == dt.Rows[i]["Col1"].ToString()) || (ddlUnLstDR_Prod3.SelectedValue == dt.Rows[i]["Col1"].ToString()))
                {
                    ierr = 1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Duplicate Product');", true);
                    break;
                }
                else
                {
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["Col1"].ToString() == dt.Rows[i]["Col1"].ToString())
                        {
                            ierr = 1;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Duplicate Product');", true);
                            break;
                        }
                    }
                }
            }
        }
        if (ierr == 0)
        {
            pnlMultiView.Enabled = true;
            pnlTab.Enabled = true;
            pnlTab1.Enabled = true;
            pnlTop.Enabled = true;
            pnlProduct_Unlst.Attributes.Add("style", "display:none");
            if ((hdnundocedit.Value != "") || (hdnNewUnDoc.Value != ""))
                grdUnLstDR.Enabled = false;
            udrcolor();
        }

    }
    protected void btnCancelUnlst_Click(object sender, EventArgs e)
    {
        pnlProduct_Unlst.Attributes.Add("style", "display:none");
        pnlMultiView.Enabled = true;
        pnlTab.Enabled = true;
        pnlTab1.Enabled = true;
        pnlTop.Enabled = true;
        udrcolor();
    }

    //Unlisted Doctor Additional Gift


    //Additional GiftUnlsts
    protected void btnUnGiftAdd_Click(object sender, EventArgs e)
    {

        if (ddlUnLstDR_Gift.SelectedIndex > 0 && txtUnLstDR_GQty.Text.Trim().Length > 0)
        {
            hdnUnLstgift.Value = "";
            pnlGiftUnlst.Attributes.Add("style", "display:block");
            pnlGiftUnlst.CssClass = "pnladd";
           
            pnlMultiView.Enabled = false;
            pnlMultiView.Enabled = false;
            pnlTab.Enabled = false;
            pnlTop.Enabled = false;
            if ((hdnundocedit.Value != "") || (hdnNewUnDoc.Value != ""))
            {
                BindGrid_UnListedDR("0");
                int j = -1;
                DataSet ds = (DataSet)grdUnLstDR.DataSource;
                if (hdnundocedit.Value != "")
                    j = grdUnLstDR.Rows[Convert.ToInt16(hdnundocedit.Value.ToString()) - 1].DataItemIndex;
                else if (hdnNewUnDoc.Value != "")
                    j = grdUnLstDR.Rows[Convert.ToInt16(hdnNewUnDoc.Value.ToString()) - 1].DataItemIndex;
                grdUnLstDR.Rows[j].BackColor = Color.LimeGreen;
                //string lblAddProd = ds.Tables[0].Rows[j]["AddProdCode"].ToString();
                string lblAddGift = ds.Tables[0].Rows[j]["AddGiftCode"].ToString();
                if (ViewState["UnlstAddGiftExists"].ToString() != "Yes")
                {
                    if (lblAddGift != "")
                    {
                        DataTable dt = new DataTable();
                        DataRow dr = null;
                        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                        dt.Columns.Add(new DataColumn("Col1", typeof(string)));
                        dt.Columns.Add(new DataColumn("Col2", typeof(string)));
                        dr = dt.NewRow();
                        dr["RowNumber"] = 1;
                        dr["Col1"] = string.Empty;
                        dr["Col2"] = string.Empty;

                        dt.Rows.Add(dr);

                        grdGiftUnlst.DataSource = dt;
                        grdGiftUnlst.DataBind();


                        string[] addgift = lblAddGift.Split('#');
                        int rowIndex = 0;
                        foreach (string agift in addgift)
                        {

                            if (agift != "")
                            {
                                string giftcode = agift.Substring(0, agift.IndexOf("~"));
                                string Qty = agift.Substring(agift.IndexOf("~") + 1, (agift.Length - (giftcode.Length + 2)));

                                DataRow drCurrentRow = null;
                                DropDownList ddlGiftUnlstAdd = (DropDownList)grdGiftUnlst.Rows[rowIndex].Cells[1].FindControl("ddlGiftUnlstAdd");
                                TextBox txtGiftUnlstQty = (TextBox)grdGiftUnlst.Rows[rowIndex].Cells[2].FindControl("txtGiftUnlstQty");

                                drCurrentRow = dt.NewRow();
                                drCurrentRow["RowNumber"] = rowIndex;

                                ddlGiftUnlstAdd.SelectedValue = giftcode;
                                txtGiftUnlstQty.Text = Qty;
                                dt.Rows[rowIndex]["Col1"] = giftcode;
                                dt.Rows[rowIndex]["Col2"] = Qty;
                                rowIndex++;
                                dt.Rows.Add(drCurrentRow);
                                grdGiftUnlst.DataSource = dt;
                                grdGiftUnlst.DataBind();
                            }
                        }

                        if (dt.Rows.Count > 0)
                        {
                            rowIndex = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                DropDownList ddlGiftUnlstAdd = (DropDownList)grdGiftUnlst.Rows[rowIndex].Cells[1].FindControl("ddlGiftUnlstAdd");
                                TextBox txtGiftUnlstQty = (TextBox)grdGiftUnlst.Rows[rowIndex].Cells[2].FindControl("txtGiftUnlstQty");

                                ddlGiftUnlstAdd.SelectedValue = dt.Rows[i]["Col1"].ToString();
                                txtGiftUnlstQty.Text = dt.Rows[i]["Col2"].ToString();
                                rowIndex++;
                            }
                        }
                        if (dt.Rows.Count > 0)
                        {
                            ViewState["CurrentTableGiftUnlst"] = dt;
                            ViewState["UnlstAddGiftExists"] = "Yes";
                        }
                        else
                        {
                            ViewState["CurrentTableGiftUnlst"] = dt;
                            ViewState["UnlstAddGiftExists"] = "";
                        }
                    }
                }
            }
            else
            {
                SetPreviousData_GiftUnlst();
            }
            FirstGridViewRow_GiftUnlst();
            udrcolor();
        }
    }

    //protected void btnGiftUnlstAdd_Click(object sender, EventArgs e)
    //{
    //    AddNewRow_GiftUnlstToGrid();
    //}

    protected void ButtonAddGiftUnlst_Click(object sender, EventArgs e)
    {

        AddNewRow_GiftUnlst();
    }

    //protected void grdGiftUnlst_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    grdGiftUnlst.DeleteRow(e.RowIndex);
    //}

    private void FirstGridViewRow_GiftUnlst()
    {
        if (ViewState["UnlstAddGiftExists"].ToString() != "Yes")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Col1"] = "0";
            dr["Col2"] = string.Empty;
            dt.Rows.Add(dr);

            ViewState["CurrentTableGiftUnlst"] = dt;

            grdGiftUnlst.DataSource = dt;
            grdGiftUnlst.DataBind();
        }
    }

    private void SetPreviousData_GiftUnlst()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableGiftUnlst"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableGiftUnlst"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlGiftUnlstAdd =
                      (DropDownList)grdGiftUnlst.Rows[rowIndex].Cells[1].FindControl("ddlGiftUnlstAdd");
                    TextBox txtGiftUnlstQty =
                      (TextBox)grdGiftUnlst.Rows[rowIndex].Cells[2].FindControl("txtGiftUnlstQty");

                    ddlGiftUnlstAdd.SelectedValue = dt.Rows[i]["Col1"].ToString();
                    txtGiftUnlstQty.Text = dt.Rows[i]["Col2"].ToString();
                    rowIndex++;
                }
            }
        }
    }

    protected void grdGiftUnlst_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowDataGiftUnlst();
        if (ViewState["CurrentTableGiftUnlst"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableGiftUnlst"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTableGiftUnlst"] = dt;
                grdGiftUnlst.DataSource = dt;
                grdGiftUnlst.DataBind();

                //for (int i = 0; i < grdGiftUnlst.Rows.Count - 1; i++)
                //{
                //    grdGiftUnlst.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                //}
                SetPreviousData_GiftUnlst();
            }
        }
    }

    private void SetRowDataGiftUnlst()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTableGiftUnlst"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableGiftUnlst"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList ddlGiftUnlstAdd =
                      (DropDownList)grdGiftUnlst.Rows[rowIndex].Cells[1].FindControl("ddlGiftUnlstAdd");
                    TextBox txtGiftUnlstQty =
                      (TextBox)grdGiftUnlst.Rows[rowIndex].Cells[2].FindControl("txtGiftUnlstQty");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Col1"] = ddlGiftUnlstAdd.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = txtGiftUnlstQty.Text;
                    rowIndex++;
                }

                ViewState["CurrentTableGiftUnlst"] = dtCurrentTable;
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }

    private void AddNewRow_GiftUnlst()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTableGiftUnlst"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableGiftUnlst"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList ddlGiftUnlstAdd =
                      (DropDownList)grdGiftUnlst.Rows[rowIndex].Cells[1].FindControl("ddlGiftUnlstAdd");
                    TextBox txtGiftUnlstQty =
                      (TextBox)grdGiftUnlst.Rows[rowIndex].Cells[2].FindControl("txtGiftUnlstQty");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = ddlGiftUnlstAdd.SelectedValue.ToString();
                    dtCurrentTable.Rows[i - 1]["Col2"] = txtGiftUnlstQty.Text;
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTableGiftUnlst"] = dtCurrentTable;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    ViewState["UnlstAddGiftExists"] = "Yes";
                }


                grdGiftUnlst.DataSource = dtCurrentTable;
                grdGiftUnlst.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData_GiftUnlst();
    }

    protected void btnGiftUnlstDone_Click(object sender, EventArgs e)
    {

        SetRowDataGiftUnlst();
        int ierr = 0;
        DataTable dt = (DataTable)ViewState["CurrentTableGiftUnlst"];
        if (dt.Rows.Count > 0)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (ddlUnLstDR_Gift.SelectedValue == dt.Rows[i]["Col1"].ToString())
                {
                    ierr = 1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Duplicate Input');", true);
                    break;
                }
                else
                {
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["Col1"].ToString() == dt.Rows[i]["Col1"].ToString())
                        {
                            ierr = 1;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Duplicate Input');", true);
                            break;
                        }
                    }
                }
            }
        }
        if (ierr == 0)
        {
            pnlGiftUnlst.Attributes.Add("style", "display:none");
            pnlMultiView.Enabled = true;
            pnlTab.Enabled = true;
            pnlTab1.Enabled = true;
            pnlTop.Enabled = true;
            if ((hdnundocedit.Value != "") || (hdnNewUnDoc.Value != ""))
                grdUnLstDR.Enabled = false;
            udrcolor();
        }


    }
    protected void btnGiftUnlstCnl_Click(object sender, EventArgs e)
    {
        

        pnlGiftUnlst.Attributes.Add("style", "display:none");
        pnlMultiView.Enabled = true;
        pnlTab.Enabled = true;
        pnlTab1.Enabled = true;
        pnlTop.Enabled = true;
        udrcolor();
    }

    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    if (sf_type == "1")
    //    {
    //        // Added by Sridevi to create Header - 02/Oct/15
    //        Create_Head(ddlSDP.SelectedValue.ToString());

    //        Response.Redirect("~/Default_MR.aspx");
    //    }
    //    else
    //    {
    //        Response.Redirect("~/MasterFiles/MGR/DCR/DCRIndex.aspx");
    //    }
    //}

    private void ShowHideColumns()
    {
        if (prod_sel <= 3)
        {
            if (prod_sel == 1)
            {
                ddlProd2.Visible = false;
                txtProd2.Visible = false;
                txtProdPOB2.Visible = false;

                ddlProd3.Visible = false;
                txtProd3.Visible = false;
                txtProdPOB3.Visible = false;

                btnProdAdd.Visible = false;

                ddlUnLstDR_Prod2.Visible = false;
                txtUnLstDR_Prod_Qty2.Visible = false;
                txtUnLstDR_Prod_POB2.Visible = false;

                ddlUnLstDR_Prod3.Visible = false;
                txtUnLstDR_Prod_Qty3.Visible = false;
                txtUnLstDR_Prod_POB3.Visible = false;

                btnProdAdd.Visible = false;
                BtnUnProdAdd.Visible = false;

                if (pob == 1)
                {
                    txtProdPOB1.Visible = true;
                    ddlProd1.Width = 275;
                    txtProd1.Width = 50;
                    txtProdPOB1.Width = 50;

                    txtUnLstDR_Prod_POB1.Visible = true;

                    ddlUnLstDR_Prod1.Width = 275;
                    txtUnLstDR_Prod_Qty1.Width = 50;
                    txtUnLstDR_Prod_POB1.Width = 50;

                }
                else
                {
                    ddlProd1.Width = 300;
                    txtProd1.Width = 75;

                    ddlUnLstDR_Prod1.Width = 300;
                    txtUnLstDR_Prod_Qty1.Width = 75;
                }
            }
            else if (prod_sel == 2)
            {
                ddlProd3.Visible = false;
                txtProd3.Visible = false;
                txtProdPOB3.Visible = false;
                btnProdAdd.Visible = false;

                ddlUnLstDR_Prod3.Visible = false;
                txtUnLstDR_Prod_Qty3.Visible = false;
                txtUnLstDR_Prod_POB3.Visible = false;
                BtnUnProdAdd.Visible = false;

                if (pob == 1)
                {
                    txtProdPOB1.Visible = true;
                    txtProdPOB2.Visible = true;

                    ddlProd1.Width = 100;
                    txtProd1.Width = 37;
                    txtProdPOB1.Width = 50;

                    ddlProd2.Width = 100;
                    txtProd2.Width = 38;
                    txtProdPOB2.Width = 50;

                    txtUnLstDR_Prod_POB1.Visible = true;
                    txtUnLstDR_Prod_POB2.Visible = true;

                    ddlUnLstDR_Prod1.Width = 100;
                    txtUnLstDR_Prod_Qty1.Width = 38;
                    txtUnLstDR_Prod_POB1.Width = 50;

                    ddlUnLstDR_Prod2.Width = 100;
                    txtUnLstDR_Prod_Qty2.Width = 38;
                    txtUnLstDR_Prod_POB2.Width = 50;
                }
                else
                {
                    ddlProd1.Width = 150;
                    txtProd1.Width = 37;
                    ddlProd2.Width = 150;
                    txtProd2.Width = 38;


                    ddlUnLstDR_Prod1.Width = 150;
                    txtUnLstDR_Prod_Qty1.Width = 37;


                    ddlUnLstDR_Prod2.Width = 150;
                    txtUnLstDR_Prod_Qty2.Width = 37;

                }
            }
            else if (prod_sel == 3)
            {
                btnProdAdd.Visible = false;
                BtnUnProdAdd.Visible = false;

                if (pob == 1)
                {
                    txtProdPOB1.Visible = true;
                    txtProdPOB2.Visible = true;
                    txtProdPOB3.Visible = true;

                    ddlProd1.Width = 80;
                    txtProd1.Width = 13;
                    txtProdPOB1.Width = 22;
                    ddlProd2.Width = 80;
                    txtProd2.Width = 13;
                    txtProdPOB2.Width = 22;
                    ddlProd3.Width = 80;
                    txtProd3.Width = 13;
                    txtProdPOB3.Width = 22;


                    txtUnLstDR_Prod_POB1.Visible = true;
                    txtUnLstDR_Prod_POB2.Visible = true;
                    txtUnLstDR_Prod_POB3.Visible = true;


                    ddlUnLstDR_Prod1.Width = 80;
                    txtUnLstDR_Prod_Qty1.Width = 13;
                    txtProdPOB1.Width = 22;

                    ddlUnLstDR_Prod2.Width = 80;
                    txtUnLstDR_Prod_Qty2.Width = 13;
                    txtProdPOB2.Width = 22;

                    ddlUnLstDR_Prod3.Width = 80;
                    txtUnLstDR_Prod_Qty3.Width = 13;
                    txtProdPOB3.Width = 22;

                }
            }
            else
            {
                tblDoc.Rows[0].Cells[4].Visible = false;
                tblDoc.Rows[1].Cells[4].Visible = false;


                tblUnlstDr.Rows[0].Cells[4].Visible = false;
                tblUnlstDr.Rows[1].Cells[4].Visible = false;
            }
        }
        else
        {
            if (pob == 1)
            {
                txtProdPOB1.Visible = true;
                txtProdPOB2.Visible = true;
                txtProdPOB3.Visible = true;

                ddlProd1.Width = 80;
                txtProd1.Width = 13;
                txtProdPOB1.Width = 22;

                ddlProd2.Width = 80;
                txtProd2.Width = 13;
                txtProdPOB2.Width = 22;

                ddlProd3.Width = 80;
                txtProd3.Width = 13;
                txtProdPOB3.Width = 22;

                txtUnLstDR_Prod_POB1.Visible = true;
                txtUnLstDR_Prod_POB2.Visible = true;
                txtUnLstDR_Prod_POB3.Visible = true;


                ddlUnLstDR_Prod1.Width = 80;
                txtUnLstDR_Prod_Qty1.Width = 13;
                txtProdPOB1.Width = 22;

                ddlUnLstDR_Prod2.Width = 80;
                txtUnLstDR_Prod_Qty2.Width = 13;
                txtProdPOB2.Width = 22;

                ddlUnLstDR_Prod3.Width = 80;
                txtUnLstDR_Prod_Qty3.Width = 13;
                txtProdPOB3.Width = 22;

            }
        }

        if ((sess_dcr == 1) && (time_dcr == 1))
        {
            tblDoc.Rows[0].Cells[0].Visible = false;
            tblDoc.Rows[1].Cells[0].Visible = false;
            tblDoc.Rows[0].Cells[1].Visible = false;
            tblDoc.Rows[1].Cells[1].Visible = false;

            tblUnlstDr.Rows[0].Cells[0].Visible = false;
            tblUnlstDr.Rows[1].Cells[0].Visible = false;
            tblUnlstDr.Rows[0].Cells[1].Visible = false;
            tblUnlstDr.Rows[1].Cells[1].Visible = false;

            //txtListDR.Style.Add("width", "145px");
            //tdDoc.Style.Add("width", "155px");
            //pnlList.Style.Add("left", "2px");
            //pnlLstSF.Style.Add("left", "202px");

            //UnLstDr.Style.Add("width", "200px");
            //pnlUnLstDR_SF.Style.Add("left", "171px");
        }
        else if (((sess_dcr == 0) && (time_dcr != 0)) || ((sess_dcr != 0) && (time_dcr == 0)))
        {
            if (sess_dcr == 0)
            {
                tblDoc.Rows[0].Cells[0].Visible = false;
                tblDoc.Rows[1].Cells[0].Visible = false;

                tblUnlstDr.Rows[0].Cells[0].Visible = false;
                tblUnlstDr.Rows[1].Cells[0].Visible = false;
            }

            else if (time_dcr == 0)
            {

                tblDoc.Rows[0].Cells[1].Visible = false;
                tblDoc.Rows[1].Cells[1].Visible = false;

                tblUnlstDr.Rows[0].Cells[1].Visible = false;
                tblUnlstDr.Rows[1].Cells[1].Visible = false;

            }

            //txtListDR.Style.Add("width","115px");
            //tdDoc.Style.Add("width", "125px");
            //pnlList.Style.Add("left", "72px");
            //pnlLstSF.Style.Add("left", "206px");

            //UnLstDr.Style.Add("width", "115px");
            //pnlUnLstDR_SF.Style.Add("left", "206px");
        }

    }
    private void Fill_Review()
    {
        if (txtRemarkDesc.Text.Length > 0)
        {
            //  DataSet ds = null;
            tblPreview_Remarks.Visible = true;
            RevPreview.Text = txtRemarkDesc.Text;
            //if (ViewState["isEntry"].ToString() != "true")
            //{
            //    // Added by Sridevi to create Header - 02/Oct/15
            //    Create_Head(ddlSDP.SelectedValue.ToString());
            //}
            //if (ViewState["clear"].ToString() != "1")
            //{
            //    sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Header.xml";
            //    dshead = (DataSet)ViewState["Header"];

            //    if (dshead != null && dshead.HasChanges())
            //    {
            //        if (dshead.Tables[0].Rows.Count > 0)
            //        {
            //            sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Header.xml";
            //            ds = (DataSet)ViewState["Header"];
            //            ds.Tables[0].Rows[0]["worktype"] = ddlWorkType.SelectedValue.ToString();
            //            ds.Tables[0].Rows[0]["sdp"] = ddlSDP.SelectedValue.ToString();
            //            ds.Tables[0].Rows[0]["remarks"] = txtRemarkDesc.Text;
            //            ds.WriteXml(Server.MapPath(sFile));
            //            ViewState["Header"] = (DataSet)ds;
            //        }
            //    }
            //}
        }
        else
        {
            RevPreview.Text = "";
        }
    }

    private void FillDoc()
    {

        DataSet ds = new DataSet();
        //DataTable dt = new DataTable();

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_ListedDR.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }

        ds.ReadXml(Server.MapPath(sFile));
        if (ds != null && ds.HasChanges())
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblld.Visible = true;
                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

                TableCell tc_Ses = new TableCell();
                tc_Ses.BorderStyle = BorderStyle.Solid;
                tc_Ses.BorderWidth = 1;
                tc_Ses.Width = 30;
                Literal lit_Ses = new Literal();
                lit_Ses.Text = "Ses";
                tc_Ses.Controls.Add(lit_Ses);
                tr_header.Cells.Add(tc_Ses);
                //tr_header.BackColor = System.Drawing.Color.Pink;        
                tr_header.BackColor = System.Drawing.Color.FromName("#FDD7E4");
                TableCell tc_Time = new TableCell();
                tc_Time.BorderStyle = BorderStyle.Solid;
                tc_Time.BorderWidth = 1;
                tc_Time.Width = 40;
                Literal lit_Time = new Literal();
                lit_Time.Text = "<center>Time</center>";
                tc_Time.Controls.Add(lit_Time);
                tr_header.Cells.Add(tc_Time);

                TableCell tc_DR_Name = new TableCell();
                tc_DR_Name.BorderStyle = BorderStyle.Solid;
                tc_DR_Name.BorderWidth = 1;
                tc_DR_Name.Width = 170;
                Literal lit_DR_Name = new Literal();
                lit_DR_Name.Text = "<center>Listed Doctor Name</center>";
                tc_DR_Name.Controls.Add(lit_DR_Name);
                tr_header.Cells.Add(tc_DR_Name);

                TableCell tc_ww = new TableCell();
                tc_ww.BorderStyle = BorderStyle.Solid;
                tc_ww.BorderWidth = 1;
                tc_ww.Width = 300;
                Literal lit_ww = new Literal();
                lit_ww.Text = "<center>Worked With</center>";
                tc_ww.Controls.Add(lit_ww);
                tr_header.Cells.Add(tc_ww);

                TableCell tc_prod = new TableCell();
                tc_prod.BorderStyle = BorderStyle.Solid;
                tc_prod.BorderWidth = 1;
                tc_prod.Width = 460;
                //   tc_prod.ColumnSpan = 6;
                Literal lit_prod = new Literal();
                lit_prod.Text = "<center>Product Promoted / Sampled</center>";
                tc_prod.Controls.Add(lit_prod);
                tr_header.Cells.Add(tc_prod);

                TableCell tc_Gift = new TableCell();
                tc_Gift.BorderStyle = BorderStyle.Solid;
                tc_Gift.BorderWidth = 1;
                tc_Gift.Width = 240;
                Literal lit_Gift = new Literal();
                lit_Gift.Text = "<center>Input</center>";
                tc_Gift.Controls.Add(lit_Gift);
                tr_header.Cells.Add(tc_Gift);

                TableCell tc_remarks = new TableCell();
                tc_remarks.BorderStyle = BorderStyle.Solid;
                tc_remarks.BorderWidth = 1;
                tc_remarks.Width = 240;
                Literal lit_remarks = new Literal();
                lit_remarks.Text = "<center>Remarks</center>";
                tc_remarks.Controls.Add(lit_remarks);
                tr_header.Cells.Add(tc_remarks);

                tbl.Rows.Add(tr_header);

                //Details Section
                foreach (DataRow drFF in ds.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_Ses = new TableCell();
                    Literal lit_det_Ses = new Literal();
                    lit_det_Ses.Text = "&nbsp;" + drFF["session"].ToString();
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);
                    tr_det.Cells.Add(tc_det_Ses);

                    TableCell tc_det_time = new TableCell();
                    Literal lit_det_time = new Literal();
                    lit_det_time.Text = "&nbsp;" + drFF["time"].ToString();
                    tc_det_time.BorderStyle = BorderStyle.Solid;
                    tc_det_time.BorderWidth = 1;
                    tc_det_time.Controls.Add(lit_det_time);
                    tr_det.Cells.Add(tc_det_time);

                    TableCell tc_det_doc_name = new TableCell();
                    Literal lit_det_doc_name = new Literal();
                    lit_det_doc_name.Text = "&nbsp;" + drFF["drcode"].ToString();
                    tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                    tc_det_doc_name.BorderWidth = 1;
                    tc_det_doc_name.Controls.Add(lit_det_doc_name);
                    tr_det.Cells.Add(tc_det_doc_name);

                    TableCell tc_det_ww = new TableCell();
                    Literal lit_det_ww = new Literal();
                    lit_det_ww.Text = "&nbsp;" + drFF["workwith"].ToString();
                    tc_det_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_ww.BorderWidth = 1;
                    tc_det_ww.Controls.Add(lit_det_ww);
                    tr_det.Cells.Add(tc_det_ww);

                    TableCell tc_det_prod1 = new TableCell();
                    Literal lit_det_prod1 = new Literal();

                    string[] addprod = drFF["AddProd"].ToString().Split('#');
                    string prodmore = string.Empty;

                    foreach (string aprod in addprod)
                    {
                        if (aprod != "")
                        {
                            string prodcode = aprod.Substring(0, aprod.IndexOf("~"));
                            string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - (prodcode.Length + 2)));
                            prodmore = prodmore + prodcode;
                            if (Qty.Length > 0)
                            {
                                prodmore = prodmore + " ( " + Qty + " ) " + " / ";
                            }
                            else
                            {
                                prodmore = prodmore + " / ";
                            }
                        }
                    }
                    drFF["AddProd"] = prodmore;

                    //drFF["AddProd"] = drFF["AddProd"].ToString().Replace("~", " ( ").Trim();
                    //drFF["AddProd"] = drFF["AddProd"].ToString().Replace("$", " ) ").Trim();
                    //drFF["AddProd"] = drFF["AddProd"].ToString().Replace("#", " / ").Trim();
                    if (drFF["prod1"].ToString().Length > 0)
                    {
                        if (drFF["qty1"].ToString().Length > 0)
                        {
                            drFF["prod1"] = drFF["prod1"].ToString().Replace("~", "").Trim() + " ( " + drFF["qty1"].ToString() + " ) ";
                        }
                        else
                        {
                            drFF["prod1"] = drFF["prod1"].ToString().Replace("~", "").Trim();
                        }

                    }
                    if (drFF["prod2"].ToString().Length > 0)
                    {
                        if (drFF["qty2"].ToString().Length > 0)
                        {
                            drFF["prod2"] = "/" + drFF["prod2"].ToString().Replace("~", "").Trim() + " ( " + drFF["qty2"].ToString() + " ) ";
                        }
                        else
                        {
                            drFF["prod2"] = "/" + drFF["prod2"].ToString().Replace("~", "").Trim();
                        }

                    }
                    if (drFF["prod3"].ToString().Length > 0)
                    {
                        if (drFF["qty3"].ToString().Length > 0)
                        {
                            drFF["prod3"] = "/" + drFF["prod3"].ToString().Replace("~", "").Trim() + " ( " + drFF["qty3"].ToString() + " ) ";
                        }
                        else
                        {
                            drFF["prod3"] = "/" + drFF["prod3"].ToString().Replace("~", "").Trim();
                        }
                    }
                    lit_det_prod1.Text = "&nbsp;" + drFF["prod1"].ToString() + "&nbsp;" + drFF["prod2"].ToString() + "&nbsp;" + drFF["prod3"].ToString() + "&nbsp;" + "/" + drFF["AddProd"].ToString();
                    tc_det_prod1.BorderStyle = BorderStyle.Solid;
                    tc_det_prod1.BorderWidth = 1;
                    tc_det_prod1.Width = 120;
                    tc_det_prod1.Controls.Add(lit_det_prod1);
                    tc_det_prod1.HorizontalAlign = HorizontalAlign.Left;
                    tr_det.Cells.Add(tc_det_prod1);


                    TableCell tc_det_gift = new TableCell();
                    Literal lit_det_gift = new Literal();
                    drFF["AddGift"] = drFF["AddGift"].ToString().Replace("~", " ( ").Trim();
                    drFF["AddGift"] = drFF["AddGift"].ToString().Replace("$", " ) ").Trim();
                    drFF["AddGift"] = drFF["AddGift"].ToString().Replace("#", " / ").Trim();
                    if (drFF["gqty"].ToString().Length > 0)
                    {
                        drFF["gift"] = drFF["gift"].ToString().Replace("~", "").Trim() + " ( " + drFF["gqty"].ToString() + " ) ";
                    }
                    else
                    {
                        drFF["gift"] = drFF["gift"].ToString().Replace("~", "").Trim();
                    }
                    if (drFF["gift"].ToString() != "")
                    {
                        lit_det_gift.Text = "&nbsp;" + drFF["gift"].ToString() + " / " + "&nbsp;" + drFF["AddGift"].ToString();
                    }
                    tc_det_gift.BorderStyle = BorderStyle.Solid;
                    tc_det_gift.BorderWidth = 1;
                    tc_det_gift.Controls.Add(lit_det_gift);
                    tc_det_gift.HorizontalAlign = HorizontalAlign.Left;
                    tr_det.Cells.Add(tc_det_gift);

                    TableCell tc_det_rm = new TableCell();
                    Literal lit_det_rm = new Literal();
                    lit_det_rm.Text = "&nbsp;" + drFF["remarks"].ToString();
                    tc_det_rm.BorderStyle = BorderStyle.Solid;
                    tc_det_rm.BorderWidth = 1;
                    tc_det_rm.Controls.Add(lit_det_rm);
                    tr_det.Cells.Add(tc_det_rm);


                    tbl.Rows.Add(tr_det);
                }
            }
            else
            {
                lblld.Visible = false;
            }

        }
        else
        {
            lblld.Visible = false;
        }

    }



    private void Preview_Chem()
    {
        DataSet ds = new DataSet();

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Chem.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }

        ds.ReadXml(Server.MapPath(sFile));

        if (ds != null && ds.HasChanges())
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblch.Visible = true;
                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

                TableCell tc_Ses = new TableCell();
                tc_Ses.BorderStyle = BorderStyle.Solid;
                tc_Ses.BorderWidth = 1;
                tc_Ses.Width = 300;
                Literal lit_Ses = new Literal();
                lit_Ses.Text = "Chemists Name";
                tc_Ses.Controls.Add(lit_Ses);
                tr_header.Cells.Add(tc_Ses);
                // tr_header.BackColor = System.Drawing.Color.Pink;
                tr_header.BackColor = System.Drawing.Color.FromName("#FDD7E4");
                TableCell tc_ww = new TableCell();
                tc_ww.BorderStyle = BorderStyle.Solid;
                tc_ww.BorderWidth = 1;
                tc_ww.Width = 300;
                Literal lit_ww = new Literal();
                lit_ww.Text = "<center>Worked With</center>";
                tc_ww.Controls.Add(lit_ww);
                tr_header.Cells.Add(tc_ww);

                TableCell tc_prod = new TableCell();
                tc_prod.BorderStyle = BorderStyle.Solid;
                tc_prod.BorderWidth = 1;
                tc_prod.Width = 420;
                Literal lit_prod = new Literal();
                lit_prod.Text = "<center>POB</center>";
                tc_prod.Controls.Add(lit_prod);
                tr_header.Cells.Add(tc_prod);

                tblChem.Rows.Add(tr_header);

                //Details Section
                foreach (DataRow drFF in ds.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_Ses = new TableCell();
                    Literal lit_det_Ses = new Literal();
                    lit_det_Ses.Text = "&nbsp;" + drFF["chemists"].ToString();
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);
                    tr_det.Cells.Add(tc_det_Ses);

                    TableCell tc_det_doc_name = new TableCell();
                    Literal lit_det_doc_name = new Literal();
                    lit_det_doc_name.Text = "&nbsp;" + drFF["chemww"].ToString();
                    tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                    tc_det_doc_name.BorderWidth = 1;
                    tc_det_doc_name.Controls.Add(lit_det_doc_name);
                    tr_det.Cells.Add(tc_det_doc_name);

                    TableCell tc_det_ww = new TableCell();
                    Literal lit_det_ww = new Literal();
                    lit_det_ww.Text = "&nbsp;" + drFF["POBNo"].ToString();
                    tc_det_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_ww.BorderWidth = 1;
                    tc_det_ww.Controls.Add(lit_det_ww);
                    tr_det.Cells.Add(tc_det_ww);

                    tblChem.Rows.Add(tr_det);
                }
            }
            else
            {
                lblch.Visible = false;
            }

        }
        else
        {
            lblch.Visible = false;
        }

    }

    private void Preview_Stk()
    {
        DataSet ds = new DataSet();

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Stockiest.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }

        ds.ReadXml(Server.MapPath(sFile));

        if (ds != null && ds.HasChanges())
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblst.Visible = true;
                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

                TableCell tc_Ses = new TableCell();
                tc_Ses.BorderStyle = BorderStyle.Solid;
                tc_Ses.BorderWidth = 1;
                tc_Ses.Width = 300;
                Literal lit_Ses = new Literal();
                lit_Ses.Text = "Stockiest Name";
                tc_Ses.Controls.Add(lit_Ses);
                tr_header.Cells.Add(tc_Ses);
                //tr_header.BackColor = System.Drawing.Color.Pink;
                tr_header.BackColor = System.Drawing.Color.FromName("#FDD7E4");
                TableCell tc_ww = new TableCell();
                tc_ww.BorderStyle = BorderStyle.Solid;
                tc_ww.BorderWidth = 1;
                tc_ww.Width = 300;
                Literal lit_ww = new Literal();
                lit_ww.Text = "<center>Worked With</center>";
                tc_ww.Controls.Add(lit_ww);
                tr_header.Cells.Add(tc_ww);

                TableCell tc_prod = new TableCell();
                tc_prod.BorderStyle = BorderStyle.Solid;
                tc_prod.BorderWidth = 1;
                tc_prod.Width = 420;
                Literal lit_prod = new Literal();
                lit_prod.Text = "<center>POB</center>";
                tc_prod.Controls.Add(lit_prod);
                tr_header.Cells.Add(tc_prod);

                TableCell tc_visit = new TableCell();
                tc_visit.BorderStyle = BorderStyle.Solid;
                tc_visit.BorderWidth = 1;
                tc_visit.Width = 420;
                Literal lit_visit = new Literal();
                lit_visit.Text = "<center>Visit</center>";
                tc_visit.Controls.Add(lit_visit);
                tr_header.Cells.Add(tc_visit);

                tblstk.Rows.Add(tr_header);

                //Details Section
                foreach (DataRow drFF in ds.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_Ses = new TableCell();
                    Literal lit_det_Ses = new Literal();
                    lit_det_Ses.Text = "&nbsp;" + drFF["stockiest"].ToString();
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);
                    tr_det.Cells.Add(tc_det_Ses);

                    TableCell tc_det_doc_name = new TableCell();
                    Literal lit_det_doc_name = new Literal();
                    lit_det_doc_name.Text = "&nbsp;" + drFF["stockww"].ToString();
                    tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                    tc_det_doc_name.BorderWidth = 1;
                    tc_det_doc_name.Controls.Add(lit_det_doc_name);
                    tr_det.Cells.Add(tc_det_doc_name);

                    TableCell tc_det_ww = new TableCell();
                    Literal lit_det_ww = new Literal();
                    lit_det_ww.Text = "&nbsp;" + drFF["pob"].ToString();
                    tc_det_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_ww.BorderWidth = 1;
                    tc_det_ww.Controls.Add(lit_det_ww);
                    tr_det.Cells.Add(tc_det_ww);

                    TableCell tc_det_vst = new TableCell();
                    Literal lit_det_vst = new Literal();
                    lit_det_vst.Text = "&nbsp;" + drFF["visit"].ToString();
                    tc_det_vst.BorderStyle = BorderStyle.Solid;
                    tc_det_vst.BorderWidth = 1;
                    tc_det_vst.Controls.Add(lit_det_vst);
                    tr_det.Cells.Add(tc_det_vst);

                    tblstk.Rows.Add(tr_det);
                }
            }
            else
            {
                lblst.Visible = false;
            }
        }
        else
        {
            lblst.Visible = false;
        }

    }

    private void FillUnlstDoc()
    {

        DataSet ds = new DataSet();
        //DataTable dt = new DataTable();

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "UnLstDR.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }

        ds.ReadXml(Server.MapPath(sFile));
        if (ds != null && ds.HasChanges())
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblunls.Visible = true;
                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

                TableCell tc_Ses = new TableCell();
                tc_Ses.BorderStyle = BorderStyle.Solid;
                tc_Ses.BorderWidth = 1;
                tc_Ses.Width = 30;
                Literal lit_Ses = new Literal();
                lit_Ses.Text = "Ses";
                tc_Ses.Controls.Add(lit_Ses);
                tr_header.Cells.Add(tc_Ses);
                //tr_header.BackColor = System.Drawing.Color.Pink;
                tr_header.BackColor = System.Drawing.Color.FromName("#FDD7E4");

                TableCell tc_Time = new TableCell();
                tc_Time.BorderStyle = BorderStyle.Solid;
                tc_Time.BorderWidth = 1;
                tc_Time.Width = 40;
                Literal lit_Time = new Literal();
                lit_Time.Text = "<center>Time</center>";
                tc_Time.Controls.Add(lit_Time);
                tr_header.Cells.Add(tc_Time);

                TableCell tc_DR_Name = new TableCell();
                tc_DR_Name.BorderStyle = BorderStyle.Solid;
                tc_DR_Name.BorderWidth = 1;
                tc_DR_Name.Width = 300;
                Literal lit_DR_Name = new Literal();
                lit_DR_Name.Text = "<center>UnListed Doctor Name</center>";
                tc_DR_Name.Controls.Add(lit_DR_Name);
                tr_header.Cells.Add(tc_DR_Name);

                TableCell tc_ww = new TableCell();
                tc_ww.BorderStyle = BorderStyle.Solid;
                tc_ww.BorderWidth = 1;
                tc_ww.Width = 300;
                Literal lit_ww = new Literal();
                lit_ww.Text = "<center>Worked With</center>";
                tc_ww.Controls.Add(lit_ww);
                tr_header.Cells.Add(tc_ww);

                TableCell tc_prod = new TableCell();
                tc_prod.BorderStyle = BorderStyle.Solid;
                tc_prod.BorderWidth = 1;
                tc_prod.Width = 420;
                // tc_prod.ColumnSpan = 6;
                Literal lit_prod = new Literal();
                lit_prod.Text = "<center>Product Promoted / Sampled</center>";
                tc_prod.Controls.Add(lit_prod);
                tr_header.Cells.Add(tc_prod);

                TableCell tc_Gift = new TableCell();
                tc_Gift.BorderStyle = BorderStyle.Solid;
                tc_Gift.BorderWidth = 1;
                tc_Gift.Width = 200;
                Literal lit_Gift = new Literal();
                lit_Gift.Text = "<center>Input</center>";
                tc_Gift.Controls.Add(lit_Gift);
                tr_header.Cells.Add(tc_Gift);

                tblunlst.Rows.Add(tr_header);

                //Details Section
                foreach (DataRow drFF in ds.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_Ses = new TableCell();
                    Literal lit_det_Ses = new Literal();
                    lit_det_Ses.Text = "&nbsp;" + drFF["session"].ToString();
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);
                    tr_det.Cells.Add(tc_det_Ses);

                    TableCell tc_det_time = new TableCell();
                    Literal lit_det_time = new Literal();
                    lit_det_time.Text = "&nbsp;" + drFF["time"].ToString();
                    tc_det_time.BorderStyle = BorderStyle.Solid;
                    tc_det_time.BorderWidth = 1;
                    tc_det_time.Controls.Add(lit_det_time);
                    tr_det.Cells.Add(tc_det_time);

                    TableCell tc_det_doc_name = new TableCell();
                    Literal lit_det_doc_name = new Literal();
                    lit_det_doc_name.Text = "&nbsp;" + drFF["drcode"].ToString();
                    tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                    tc_det_doc_name.BorderWidth = 1;
                    tc_det_doc_name.Controls.Add(lit_det_doc_name);
                    tr_det.Cells.Add(tc_det_doc_name);

                    TableCell tc_det_ww = new TableCell();
                    Literal lit_det_ww = new Literal();
                    lit_det_ww.Text = "&nbsp;" + drFF["workwith"].ToString();
                    tc_det_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_ww.BorderWidth = 1;
                    tc_det_ww.Controls.Add(lit_det_ww);
                    tr_det.Cells.Add(tc_det_ww);

                    TableCell tc_det_prod1 = new TableCell();
                    Literal lit_det_prod1 = new Literal();
                    string[] addprod = drFF["AddProd"].ToString().Split('#');
                    string prodmore = string.Empty;

                    foreach (string aprod in addprod)
                    {
                        if (aprod != "")
                        {
                            string prodcode = aprod.Substring(0, aprod.IndexOf("~"));
                            string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - (prodcode.Length + 2)));
                            prodmore = prodmore + prodcode;
                            if (Qty.Length > 0)
                            {
                                prodmore = prodmore + " ( " + Qty + " ) " + " / ";
                            }
                            else
                            {
                                prodmore = prodmore +  " / ";
                            }
                        }
                    }
                    drFF["AddProd"] = prodmore;
                    //drFF["AddProd"] = drFF["AddProd"].ToString().Replace("~", " ( ").Trim();
                    //drFF["AddProd"] = drFF["AddProd"].ToString().Replace("$", " ) ").Trim();
                    //drFF["AddProd"] = drFF["AddProd"].ToString().Replace("#", " / ").Trim();
                    if (drFF["prod1"].ToString().Length > 0)
                    {
                        if (drFF["qty1"].ToString().Length > 0)
                        {
                            drFF["prod1"] = drFF["prod1"].ToString().Replace("~", "").Trim() + " ( " + drFF["qty1"].ToString() + " ) ";
                        }
                        else
                        {
                            drFF["prod1"] = drFF["prod1"].ToString().Replace("~", "").Trim();
                        }

                    }
                    if (drFF["prod2"].ToString().Length > 0)
                    {
                        if (drFF["qty2"].ToString().Length > 0)
                        {
                            drFF["prod2"] = "/" + drFF["prod2"].ToString().Replace("~", "").Trim() + " ( " + drFF["qty2"].ToString() + " ) ";
                        }
                        else
                        {
                            drFF["prod2"] = "/" + drFF["prod2"].ToString().Replace("~", "").Trim();
                        }

                    }
                    if (drFF["prod3"].ToString().Length > 0)
                    {
                        if (drFF["qty3"].ToString().Length > 0)
                        {
                            drFF["prod3"] = "/" + drFF["prod3"].ToString().Replace("~", "").Trim() + " ( " + drFF["qty3"].ToString() + " ) ";
                        }
                        else
                        {
                            drFF["prod3"] = "/" + drFF["prod3"].ToString().Replace("~", "").Trim();
                        }
                    }
                    lit_det_prod1.Text = "&nbsp;" + drFF["prod1"].ToString() + "&nbsp;" + drFF["prod2"].ToString() + "&nbsp;" + drFF["prod3"].ToString() + "&nbsp;" + "/" + drFF["AddProd"].ToString();
                    tc_det_prod1.BorderStyle = BorderStyle.Solid;
                    tc_det_prod1.BorderWidth = 1;
                    tc_det_prod1.Width = 120;
                    tc_det_prod1.Controls.Add(lit_det_prod1);
                    tc_det_prod1.HorizontalAlign = HorizontalAlign.Left;
                    tr_det.Cells.Add(tc_det_prod1);


                    TableCell tc_det_gift = new TableCell();
                    Literal lit_det_gift = new Literal();
                    drFF["AddGift"] = drFF["AddGift"].ToString().Replace("~", " ( ").Trim();
                    drFF["AddGift"] = drFF["AddGift"].ToString().Replace("$", " ) ").Trim();
                    drFF["AddGift"] = drFF["AddGift"].ToString().Replace("#", " / ").Trim();
                    if (drFF["gqty"].ToString().Length > 0)
                    {
                        drFF["gift"] = drFF["gift"].ToString().Replace("~", "").Trim() + " ( " + drFF["gqty"].ToString() + " ) ";
                    }
                    else
                    {
                        drFF["gift"] = drFF["gift"].ToString().Replace("~", "").Trim();
                    }
                    if (drFF["gift"].ToString() != "")
                    {
                        lit_det_gift.Text = "&nbsp;" + drFF["gift"].ToString() + " / " + "&nbsp;" + drFF["AddGift"].ToString();
                    }
                    tc_det_gift.BorderStyle = BorderStyle.Solid;
                    tc_det_gift.BorderWidth = 1;
                    tc_det_gift.Controls.Add(lit_det_gift);
                    tc_det_gift.HorizontalAlign = HorizontalAlign.Left;
                    tr_det.Cells.Add(tc_det_gift);

                    tblunlst.Rows.Add(tr_det);
                }
            }
            else
            {
                lblunls.Visible = false;
            }

        }
        else
        {
            lblunls.Visible = false;
        }
    }
    private void Preview_Hos()
    {
        DataSet ds = new DataSet();

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Hospital.xml";

        string FilePath = Server.MapPath(sFile);
        if (!File.Exists(FilePath))
        {
            //Start writer
            XmlTextWriter dr_writer = new XmlTextWriter(Server.MapPath(sFile), System.Text.Encoding.UTF8);

            //Start XM DOcument
            dr_writer.WriteStartDocument(true);
            dr_writer.Formatting = Formatting.Indented;
            dr_writer.Indentation = 2;

            //ROOT Element
            dr_writer.WriteStartElement("DCR");
            dr_writer.WriteEndElement();
            //End XML Document
            dr_writer.WriteEndDocument();
            //Close writer
            dr_writer.Close();
        }

        ds.ReadXml(Server.MapPath(sFile));

        if (ds != null && ds.HasChanges())
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblhos.Visible = true;
                TableRow tr_header = new TableRow();
                tr_header.BorderStyle = BorderStyle.Solid;
                tr_header.BorderWidth = 1;

                TableCell tc_Ses = new TableCell();
                tc_Ses.BorderStyle = BorderStyle.Solid;
                tc_Ses.BorderWidth = 1;
                tc_Ses.Width = 300;
                Literal lit_Ses = new Literal();
                lit_Ses.Text = "Hospital Name";
                tc_Ses.Controls.Add(lit_Ses);
                tr_header.Cells.Add(tc_Ses);
                // tr_header.BackColor = System.Drawing.Color.Pink;
                tr_header.BackColor = System.Drawing.Color.FromName("#FDD7E4");
                TableCell tc_ww = new TableCell();
                tc_ww.BorderStyle = BorderStyle.Solid;
                tc_ww.BorderWidth = 1;
                tc_ww.Width = 300;
                Literal lit_ww = new Literal();
                lit_ww.Text = "<center>Worked With</center>";
                tc_ww.Controls.Add(lit_ww);
                tr_header.Cells.Add(tc_ww);

                TableCell tc_prod = new TableCell();
                tc_prod.BorderStyle = BorderStyle.Solid;
                tc_prod.BorderWidth = 1;
                tc_prod.Width = 420;
                Literal lit_prod = new Literal();
                lit_prod.Text = "<center>POB</center>";
                tc_prod.Controls.Add(lit_prod);
                tr_header.Cells.Add(tc_prod);


                tblhos.Rows.Add(tr_header);

                //Details Section
                foreach (DataRow drFF in ds.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_Ses = new TableCell();
                    Literal lit_det_Ses = new Literal();
                    lit_det_Ses.Text = "&nbsp;" + drFF["hospital"].ToString();
                    tc_det_Ses.BorderStyle = BorderStyle.Solid;
                    tc_det_Ses.BorderWidth = 1;
                    tc_det_Ses.Controls.Add(lit_det_Ses);

                    tr_det.Cells.Add(tc_det_Ses);

                    TableCell tc_det_doc_name = new TableCell();
                    Literal lit_det_doc_name = new Literal();
                    lit_det_doc_name.Text = "&nbsp;" + drFF["hosww"].ToString();
                    tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                    tc_det_doc_name.BorderWidth = 1;
                    tc_det_doc_name.Controls.Add(lit_det_doc_name);
                    tr_det.Cells.Add(tc_det_doc_name);

                    TableCell tc_det_ww = new TableCell();
                    Literal lit_det_ww = new Literal();
                    lit_det_ww.Text = "&nbsp;" + drFF["pob"].ToString();
                    tc_det_ww.BorderStyle = BorderStyle.Solid;
                    tc_det_ww.BorderWidth = 1;
                    tc_det_ww.Controls.Add(lit_det_ww);
                    tr_det.Cells.Add(tc_det_ww);

                    tblhos.Rows.Add(tr_det);
                }
            }
            else
            {
                lblhos.Visible = false;
            }
        }
        else
        {
            lblhos.Visible = false;
        }
    }
    protected void btnNewChe_Click(object sender, EventArgs e)
    {
        //lblInfo.Text = "";
        //checolor();
        //if (grdChem.Rows.Count == max_chem_dcr_count)
        //{
        //    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Cannot enter more than " + max_chem_dcr_count + " Chemists' );</script>");
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Cannot enter more than " + max_chem_dcr_count + " Chemists')", true);
        //    //Response.Write("Cannot enter more than " + max_chem_dcr_count  + " Chemists ");
        //}
        //else
        //{
        //    NewtxtChem.Text = "";
        //    txtChemNPOB.Text = "";
        //    txtChe.Text = "SELF";
        //    if (sf_type == "1")
        //    {
        //        ddlTerr.SelectedIndex = 0;
        //    }
        //    else
        //    {
        //        lblcheMR.Visible = true;
        //        ddlcheMR.Visible = true;
        //        ddlcheMR.SelectedIndex = 0;
        //    }
        //    PnlChem.Attributes.Add("style", "display:block");
        //    pnlMultiView.Enabled = false;
        //    pnlMultiView.Enabled = false;
        //    pnlTab.Enabled = false;
        //    pnlTop.Enabled = false;
        //    pnlTab1.Enabled = false;
        //}
    }
    protected void btnUnNew_Click(object sender, EventArgs e)
    {
        lblInfo.Text = "";
        udrcolor();
        if (grdUnLstDR.Rows.Count == max_unlst_dcr_count)
        {
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Cannot enter more than " + max_chem_dcr_count + " Chemists' );</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Cannot enter more than " + max_unlst_dcr_count + " Un Listed Doctor(s)' )", true);
            //Response.Write("Cannot enter more than " + max_chem_dcr_count  + " Chemists ");

        }
        else
        {
            txtUnDr.Text = "";
            txtUnDrAddr.Text = "";

            txtUn.Text = "SELF";
            if (sf_type == "1")
            {
                ddlTerr_Un.SelectedIndex = 0;
            }
            else
            {
                lblUnMR.Visible = true;
                ddlUnMR.Visible = true;
                //ddlTerr_Un.SelectedIndex = 0;
                ddlUnMR.SelectedIndex = 0;
            }

            ddlQual_Un.SelectedIndex = 0;
            ddlSpec_Un.SelectedIndex = 0;
            ddlCate_Un.SelectedIndex = 0;
            ddlClass_Un.SelectedIndex = 0;
            ddlN_unProd1.SelectedIndex = 0;
            ddlN_unProd2.SelectedIndex = 0;
            ddlN_unProd3.SelectedIndex = 0;
            txtN_UQty1.Text = "";
            txtN_UQty2.Text = "";
            txtN_UQty3.Text = "";
            ddlN_unsess.SelectedIndex = 0;
            ddlN_untime.SelectedIndex = 0;

            NPnlUnLst.Visible = true;
            pnlMultiView.Enabled = false;
            pnlMultiView.Enabled = false;
            pnlTab.Enabled = false;
            pnlTop.Enabled = false;
            pnlTab1.Enabled = false;
        }
    }
    protected void btnChe_Click(object sender, EventArgs e)
    {
        CreateChemist("1");
    }

    private void CreateChemist(string mode)
    {
        // Added by Sridevi to get WW -Code
        if (mode == "0")
        {
            hdnChemWW_Code.Value = "";
            for (int i = 0; i < chkChemWW.Items.Count; i++)
            {
                if (chkChemWW.Items[i].Selected)
                {
                    hdnChemWW_Code.Value = hdnChemWW_Code.Value + chkChemWW.Items[i].Value.ToString() + "$$";
                }
            }
            hdnChemWW_Code.Value = hdnChemWW_Code.Value.Substring(0, hdnChemWW_Code.Value.Length - 2);
        }
        else if (mode == "1")
        {
            ChehdnId.Value = "";
            for (int i = 0; i < ChkChem.Items.Count; i++)
            {
                if (ChkChem.Items[i].Selected)
                {
                    ChehdnId.Value = ChehdnId.Value + ChkChem.Items[i].Value.ToString() + "$$";
                }
            }
            ChehdnId.Value = ChehdnId.Value.Substring(0, ChehdnId.Value.Length - 2);
        }
        //ends     
       // if (ViewState["CheEdit"].ToString() != "")
        if((hdncheedit.Value !="") || (hdnnewpnlchem.Value !=""))
        {
            BindGrid_Chem("0");
            int i = -1;
            DataSet ds = (DataSet)grdChem.DataSource;
          //  int i = grdChem.Rows[Convert.ToInt16(ViewState["CheEdit"].ToString())].DataItemIndex;
            if(hdncheedit.Value !="")
                i = grdChem.Rows[Convert.ToInt16(hdncheedit.Value.ToString())-1].DataItemIndex;
            else if (hdnnewpnlchem.Value !="")
                 i = grdChem.Rows[Convert.ToInt16(hdnnewpnlchem.Value.ToString())-1].DataItemIndex;
            if (mode == "0")
            {
                ds.Tables[0].Rows[i]["chem_code"] = txtPChe.Text.Trim();
                ds.Tables[0].Rows[i]["chemists"] = txtPnlChe.Text.Trim();
                ds.Tables[0].Rows[i]["POBNo"] = txtPOBNo.Text.ToString();
                ds.Tables[0].Rows[i]["chemww"] = txtChemWWSF.Value.ToString();
                ds.Tables[0].Rows[i]["terr_code"] = ddlSDP.SelectedValue.ToString();
                ds.Tables[0].Rows[i]["ww_code"] = hdnChemWW_Code.Value;
            }
            else if (mode == "1")
            {           
                ds.Tables[0].Rows[i]["new"] = "New";
                ds.Tables[0].Rows[i]["chemists"] = NewtxtChem.Text.Trim();
                ds.Tables[0].Rows[i]["POBNo"] = txtChemNPOB.Text.ToString();
                ds.Tables[0].Rows[i]["chemww"] = txtChe.Text.ToString();
                ds.Tables[0].Rows[i]["ww_code"] = ChehdnId.Value;
                ds.Tables[0].Rows[i]["terr_code"] = ddlTerr.SelectedValue.ToString();
            }
           
            sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Chem.xml";
            ds.WriteXml(Server.MapPath(sFile));
            grdChem.Enabled = true;
            hdncheedit.Value = "";
            hdnnewpnlchem.Value = "";
            BindGrid_Chem("0");

            if (mode == "1")
            {
                PnlChem.Attributes.Add("style", "display:none");
                pnlMultiView.Enabled = true;
                pnlMultiView.Enabled = true;
                pnlTab.Enabled = true;
                pnlTop.Enabled = true;
                pnlTab1.Enabled = true;
            }        
        }
        else
        {
           
            // Added by Sridevi to create Header - 02/Oct/15
            Create_Head(ddlSDP.SelectedValue.ToString());

            if (chehdnValue.Value != "")
                txtPChe.Text = chehdnValue.Value;
            sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Chem.xml";

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Server.MapPath(sFile));

            XmlElement parentelement = xmldoc.CreateElement("DCR");

            XmlElement xmlsf_code = xmldoc.CreateElement("sf_code");
            XmlElement xmlchemists = xmldoc.CreateElement("chemists");
            XmlElement xmlchemww = xmldoc.CreateElement("chemww");
            XmlElement xmlpobno = xmldoc.CreateElement("POBNo");

            XmlElement xmlchem_code = xmldoc.CreateElement("chem_code");
            XmlElement xmlterr_code = xmldoc.CreateElement("terr_code");
            XmlElement xmlnew = xmldoc.CreateElement("new");
            XmlElement xmlww_code = xmldoc.CreateElement("ww_code");
            if (mode == "0")
            {
                chehdnValue.Value = "";
                xmlsf_code.InnerText = sf_code;
                xmlchemists.InnerText = txtPnlChe.Text.Trim();
                xmlchem_code.InnerText = txtPChe.Text.Trim();
                xmlchemww.InnerText = txtChemWWSF.Value.ToString(); //txtChemWW.Text.ToString();
                xmlpobno.InnerText = txtPOBNo.Text.ToString();
                xmlterr_code.InnerText = ddlSDP.SelectedValue.ToString();
                xmlnew.InnerText = "";
                xmlww_code.InnerText = hdnChemWW_Code.Value.ToString();
            }
            else if (mode == "1")
            {
                if (sf_type == "1")
                {
                    xmlsf_code.InnerText = sf_code;
                }
                else
                {                        
                    xmlsf_code.InnerText = ddlcheMR.SelectedValue.ToString();                       
                }
                xmlchemists.InnerText = NewtxtChem.Text;
                xmlchem_code.InnerText = "";
                xmlnew.InnerText = "New";
                xmlchemww.InnerText = txtChe.Text.ToString();
                xmlpobno.InnerText = txtChemNPOB.Text.ToString();
                xmlww_code.InnerText = ChehdnId.Value;

                if (ddlTerr.SelectedIndex > 0)
                {
                    xmlterr_code.InnerText = ddlTerr.SelectedValue.ToString();
                }
            }
            parentelement.AppendChild(xmlsf_code);
            parentelement.AppendChild(xmlchemists);
            parentelement.AppendChild(xmlchemww);
            parentelement.AppendChild(xmlpobno);

            parentelement.AppendChild(xmlchem_code);
            parentelement.AppendChild(xmlterr_code);
            parentelement.AppendChild(xmlnew);
            parentelement.AppendChild(xmlww_code);

            xmldoc.DocumentElement.AppendChild(parentelement);
                  
            xmldoc.Save(Server.MapPath(sFile));
            ViewState["cur_panel"] = "2";
            BindGrid_Chem("0");
            chehdnValue.Value = "";
            if (mode == "1")
            {
                PnlChem.Attributes.Add("style", "display:none");
            }
       }
       checolor();
    }
    protected void btnNun_Click(object sender, EventArgs e)
    {
        CreateUnlisted("1");
    }
    private void CreateUnlisted(string mode)
    {
        int ierr = 0;
        if (mode == "0")
        {
            // Added by Sridevi to get WW -Code
            hdnunww_code.Value = "";
            for (int i = 0; i < chkUnLstDR.Items.Count; i++)
            {
                if (chkUnLstDR.Items[i].Selected)
                {
                    hdnunww_code.Value = hdnunww_code.Value + chkUnLstDR.Items[i].Value.ToString() + "$$";
                }
            }
            hdnunww_code.Value = hdnunww_code.Value.Substring(0, hdnunww_code.Value.Length - 2);
        }
        else if (mode == "1")
        {
            unhidDr.Value = "";
            for (int i = 0; i < ChkUn.Items.Count; i++)
            {
                if (ChkChem.Items[i].Selected)
                {
                    unhidDr.Value = unhidDr.Value + ChkChem.Items[i].Value.ToString() + "$$";
                }
            }
            unhidDr.Value = unhidDr.Value.Substring(0, unhidDr.Value.Length - 2);
        }
        //ends     
       // if (ViewState["CheEdit"].ToString() != "")
        if ((hdnundocedit.Value != "") || (hdnNewUnDoc.Value != ""))
        {
            BindGrid_UnListedDR("0");
            int i = -1;
            DataSet ds = (DataSet)grdUnLstDR.DataSource;
            if (hdnundocedit.Value != "")
                i = grdUnLstDR.Rows[Convert.ToInt16(hdnundocedit.Value.ToString()) - 1].DataItemIndex;
            else if (hdnNewUnDoc.Value != "")
                i = grdUnLstDR.Rows[Convert.ToInt16(hdnNewUnDoc.Value.ToString()) - 1].DataItemIndex;
            
            if (mode == "0")
            {
                if (ddlUnLstDR_Session.SelectedIndex > 0)
                {
                    ds.Tables[0].Rows[i]["session"] = ddlUnLstDR_Session.SelectedItem.Text.ToString();
                    ds.Tables[0].Rows[i]["sess_code"] = ddlUnLstDR_Session.SelectedValue.ToString();
                }
                else
                {
                    ds.Tables[0].Rows[i]["session"] = "";
                    ds.Tables[0].Rows[i]["sess_code"] = "0";
                }
            }
            else if (mode == "1")
            {
                if (ddlN_unsess.SelectedIndex > 0)
                {
                    ds.Tables[0].Rows[i]["session"] = ddlN_unsess.SelectedItem.Text.ToString();
                    ds.Tables[0].Rows[i]["sess_code"] = ddlN_unsess.SelectedValue.ToString();
                }
                else
                {
                    ds.Tables[0].Rows[i]["session"] = "";
                    ds.Tables[0].Rows[i]["sess_code"] = "0";
                }
            }
            if (mode == "0")
            {

                if (ddlMinute.SelectedIndex > 0)
                {
                    ds.Tables[0].Rows[i]["min"] = ddlMinute.SelectedValue.ToString();
                }
                else
                {
                    ds.Tables[0].Rows[i]["min"] = "";
                }
            }
            else if (mode == "1")
            {
                if (ddlN_untime.SelectedIndex > 0)
                {
                    ds.Tables[0].Rows[i]["min"] = ddlN_untime.SelectedValue.ToString();
                }
                else
                {
                    ds.Tables[0].Rows[i]["min"] = "";
                }
            }
            if (mode == "0")
            {

                if (ddlSec.SelectedIndex > 0)
                {
                    ds.Tables[0].Rows[i]["sec"] = ddlSec.SelectedValue.ToString();
                }
                else
                {
                    ds.Tables[0].Rows[i]["sec"] = "";
                }
            }
            else if (mode == "1")
            {
                if (ddlN_unmin.SelectedIndex > 0)
                {
                    ds.Tables[0].Rows[i]["sec"] = ddlN_unmin.SelectedValue.ToString();
                }
                else
                {
                    ds.Tables[0].Rows[i]["sec"] = "";
                }
            }
            if (mode == "0")
            {
                if (ddlMinute.SelectedIndex > 0)
                {
                    ds.Tables[0].Rows[i]["time"] = ddlMinute.SelectedItem.Text.ToString() + ":" + ddlSec.SelectedItem.ToString();
                }
                else
                {
                    ds.Tables[0].Rows[i]["time"] = "";
                }
            }
            else if (mode == "1")
            {
                if (ddlN_untime.SelectedIndex > 0)
                {
                    ds.Tables[0].Rows[i]["time"] = ddlN_untime.SelectedItem.Text.ToString() + ":" + ddlN_unmin.SelectedItem.ToString();
                }
                else
                {
                    ds.Tables[0].Rows[i]["time"] = "";
                }
            }
            if (mode == "0")
            {
                ds.Tables[0].Rows[i]["dr_code"] = UnLstTxtDR.Text.Trim();
                ds.Tables[0].Rows[i]["drcode"] = Untxt_Dr.Text.Trim();
            }
            else if (mode == "1")
            {
                if (txtUnDr.Text.Length > 0)
                {
                    ds.Tables[0].Rows[i]["dr_code"] = "";
                    ds.Tables[0].Rows[i]["drcode"] = txtUnDr.Text.Trim();
                }
                else
                {
                    ds.Tables[0].Rows[i]["dr_code"] = "";
                    ds.Tables[0].Rows[i]["drcode"] = "";
                }

            }
            if (mode == "0")
            {
                ds.Tables[0].Rows[i]["workwith"] = txtUnLstDR_WW.Value.Trim();
                ds.Tables[0].Rows[i]["ww_code"] = hdnunww_code.Value.Trim();

            }
            else if (mode == "1")
            {
                ds.Tables[0].Rows[i]["workwith"] = txtUn.Text.Trim();
                ds.Tables[0].Rows[i]["ww_code"] = unhidDr.Value.Trim();
            }
            if (mode == "0")
            {
                if (ddlUnLstDR_Prod1.SelectedIndex > 0)
                {
                    ds.Tables[0].Rows[i]["prod1"] = ddlUnLstDR_Prod1.SelectedItem.Text.ToString();
                    ds.Tables[0].Rows[i]["prod1_code"] = ddlUnLstDR_Prod1.SelectedValue.Trim();
                    ds.Tables[0].Rows[i]["qty1"] = txtUnLstDR_Prod_Qty1.Text.Trim();
                    ds.Tables[0].Rows[i]["prod_pob1"] = txtUnLstDR_Prod_POB1.Text.Trim();
                }
                else
                {
                    ds.Tables[0].Rows[i]["prod1"] = "";
                    ds.Tables[0].Rows[i]["prod1_code"] = "0";
                    ds.Tables[0].Rows[i]["qty1"] = "";
                    ds.Tables[0].Rows[i]["prod_pob1"] = "";
                }
                if (ddlUnLstDR_Prod2.SelectedIndex > 0)
                {
                    ds.Tables[0].Rows[i]["prod2"] = ddlUnLstDR_Prod2.SelectedItem.Text.ToString();
                    ds.Tables[0].Rows[i]["prod2_code"] = ddlUnLstDR_Prod2.SelectedValue.Trim();
                    ds.Tables[0].Rows[i]["qty2"] = txtUnLstDR_Prod_Qty2.Text.Trim();
                    ds.Tables[0].Rows[i]["prod_pob2"] = txtUnLstDR_Prod_POB2.Text.Trim();
                }
                else
                {
                    ds.Tables[0].Rows[i]["prod2"] = "";
                    ds.Tables[0].Rows[i]["prod2_code"] = "0";
                    ds.Tables[0].Rows[i]["qty2"] = "";
                    ds.Tables[0].Rows[i]["prod_pob2"] = "";
                }
                if (ddlUnLstDR_Prod3.SelectedIndex > 0)
                {
                    ds.Tables[0].Rows[i]["prod3"] = ddlUnLstDR_Prod3.SelectedItem.Text.ToString();
                    ds.Tables[0].Rows[i]["prod3_code"] = ddlUnLstDR_Prod3.SelectedValue.Trim();
                    ds.Tables[0].Rows[i]["qty3"] = txtUnLstDR_Prod_Qty3.Text.Trim();
                    ds.Tables[0].Rows[i]["prod_pob3"] = txtUnLstDR_Prod_POB3.Text.Trim();
                }
                else
                {
                    ds.Tables[0].Rows[i]["prod3"] = "";
                    ds.Tables[0].Rows[i]["prod3_code"] = "0";
                    ds.Tables[0].Rows[i]["qty3"] = "";
                    ds.Tables[0].Rows[i]["prod_pob3"] = "";
                }
                if (ddlUnLstDR_Gift.SelectedIndex > 0)
                {
                    ds.Tables[0].Rows[i]["gift"] = ddlUnLstDR_Gift.SelectedItem.Text.Trim();
                    ds.Tables[0].Rows[i]["gift_code"] = ddlUnLstDR_Gift.SelectedValue.Trim();
                    ds.Tables[0].Rows[i]["gqty"] = txtUnLstDR_GQty.Text.Trim();
                }
                else
                {
                    ds.Tables[0].Rows[i]["gift"] = "";
                    ds.Tables[0].Rows[i]["gift_code"] = "0";
                    ds.Tables[0].Rows[i]["gqty"] = "";
                }

                //Additional Products if any

                if (ddlUnLstDR_Prod1.SelectedIndex > 0 && ddlUnLstDR_Prod2.SelectedIndex > 0  && ddlUnLstDR_Prod3.SelectedIndex > 0 )
                {

                    string sProdName = string.Empty;
                    string sQty = string.Empty;
                    string sProducts = string.Empty;
                    string sProdcode = string.Empty;
                    string sProductcodes = string.Empty;
                    string lblAddProd = ds.Tables[0].Rows[i]["AddProdCode"].ToString();
                   
                    if (ViewState["UnlstAddProdExists"].ToString() != "Yes")
                    {
                        if (lblAddProd != "")
                        {
                            DataTable dt = new DataTable();
                            DataRow dr = null;
                            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
                            dt.Columns.Add(new DataColumn("Col2", typeof(string)));
                            dr = dt.NewRow();
                            dr["RowNumber"] = 1;
                            dr["Col1"] = string.Empty;
                            dr["Col2"] = string.Empty;

                            dt.Rows.Add(dr);

                            grvProductUnlst.DataSource = dt;
                            grvProductUnlst.DataBind();


                            string[] addprod = lblAddProd.Split('#');
                            int rowIndex = 0;
                            foreach (string aprod in addprod)
                            {
                                if (aprod != "")
                                {
                                    string prodcode = aprod.Substring(0, aprod.IndexOf("~"));
                                    string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - (prodcode.Length + 2)));
                                    DataRow drCurrentRow = null;
                                    DropDownList ddlProductUnlstAdd = (DropDownList)grvProductUnlst.Rows[rowIndex].Cells[1].FindControl("ddlProductUnlstAdd");
                                    TextBox txtProdUnlstQty = (TextBox)grvProductUnlst.Rows[rowIndex].Cells[2].FindControl("txtProdUnlstQty");

                                    drCurrentRow = dt.NewRow();
                                    drCurrentRow["RowNumber"] = rowIndex;

                                    ddlProductUnlstAdd.SelectedValue = prodcode;
                                    txtProdUnlstQty.Text = Qty;
                                    dt.Rows[rowIndex]["Col1"] = prodcode;
                                    dt.Rows[rowIndex]["Col2"] = Qty;
                                    rowIndex++;
                                    dt.Rows.Add(drCurrentRow);
                                    grvProductUnlst.DataSource = dt;
                                    grvProductUnlst.DataBind();
                                }
                            }

                            if (dt.Rows.Count > 0)
                            {
                                rowIndex = 0;
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    DropDownList ddlProductUnlstAdd =
                                      (DropDownList)grvProductUnlst.Rows[rowIndex].Cells[1].FindControl("ddlProductUnlstAdd");
                                    TextBox txtProdUnlstQty =
                                      (TextBox)grvProductUnlst.Rows[rowIndex].Cells[2].FindControl("txtProdUnlstQty");

                                    ddlProductUnlstAdd.SelectedValue = dt.Rows[j]["Col1"].ToString();
                                    txtProdUnlstQty.Text = dt.Rows[j]["Col2"].ToString();
                                    rowIndex++;
                                }
                            }
                        }
                    }
                    foreach (GridViewRow gridRow in grvProductUnlst.Rows)
                    {
                        DropDownList ddlProductAdd = (DropDownList)gridRow.Cells[1].FindControl("ddlProductUnlstAdd");
                        sProdName = ddlProductAdd.SelectedItem.Text.ToString();
                        sProdcode = ddlProductAdd.SelectedValue.Trim();
                        TextBox txtProdQty = (TextBox)gridRow.Cells[2].FindControl("txtProdUnlstQty");
                        sQty = txtProdQty.Text.ToString();
                        if (ddlProductAdd.SelectedIndex != 0)
                        {
                            if (sProdName.Trim().Length > 0)
                            {
                                sProducts = sProducts + sProdName + "~" + sQty + "$" + "#";
                                sProductcodes = sProductcodes + sProdcode + "~" + sQty + "$" + "#";
                            }
                        }

                    }
                    ds.Tables[0].Rows[i]["AddProdCode"] = sProductcodes;
                    ds.Tables[0].Rows[i]["AddProd"] = sProducts;

                    if (sProducts.Trim().Length > 0)
                    {
                        ViewState["UnlstAddProdExists"] = "";
                    }
                }

                //Additional Gifts if any
                if (ddlUnLstDR_Gift.SelectedIndex > 0)
                {
                    string sGiftName = string.Empty;
                    string sGiftQty = string.Empty;
                    string sGifts = string.Empty;
                    string sGiftcodedtls = string.Empty;
                    string sGiftcode = string.Empty;
                    string lblAddGift = ds.Tables[0].Rows[i]["AddGiftCode"].ToString();
                    if (ViewState["UnlstAddGiftExists"].ToString() != "Yes")
                    {
                        if (lblAddGift != "")
                        {
                            DataTable dt = new DataTable();
                            DataRow dr = null;
                            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
                            dt.Columns.Add(new DataColumn("Col2", typeof(string)));
                            dr = dt.NewRow();
                            dr["RowNumber"] = 1;
                            dr["Col1"] = string.Empty;
                            dr["Col2"] = string.Empty;

                            dt.Rows.Add(dr);

                            grdGiftUnlst.DataSource = dt;
                            grdGiftUnlst.DataBind();


                            string[] addgift = lblAddGift.Split('#');
                            int rowIndex = 0;
                            foreach (string agift in addgift)
                            {

                                if (agift != "")
                                {
                                    string giftcode = agift.Substring(0, agift.IndexOf("~"));
                                    string Qty = agift.Substring(agift.IndexOf("~") + 1, (agift.Length - (giftcode.Length + 2)));

                                    DataRow drCurrentRow = null;
                                    DropDownList ddlGiftUnlstAdd = (DropDownList)grdGiftUnlst.Rows[rowIndex].Cells[1].FindControl("ddlGiftUnlstAdd");
                                    TextBox txtGiftUnlstQty = (TextBox)grdGiftUnlst.Rows[rowIndex].Cells[2].FindControl("txtGiftUnlstQty");

                                    drCurrentRow = dt.NewRow();
                                    drCurrentRow["RowNumber"] = rowIndex;

                                    ddlGiftUnlstAdd.SelectedValue = giftcode;
                                    txtGiftUnlstQty.Text = Qty;
                                    dt.Rows[rowIndex]["Col1"] = giftcode;
                                    dt.Rows[rowIndex]["Col2"] = Qty;
                                    rowIndex++;
                                    dt.Rows.Add(drCurrentRow);
                                    grdGiftUnlst.DataSource = dt;
                                    grdGiftUnlst.DataBind();
                                }
                            }

                            if (dt.Rows.Count > 0)
                            {
                                rowIndex = 0;
                                for (int j= 0; j < dt.Rows.Count; j++)
                                {
                                    DropDownList ddlGiftUnlstAdd = (DropDownList)grdGiftUnlst.Rows[rowIndex].Cells[1].FindControl("ddlGiftUnlstAdd");
                                    TextBox txtGiftUnlstQty = (TextBox)grdGiftUnlst.Rows[rowIndex].Cells[2].FindControl("txtGiftUnlstQty");

                                    ddlGiftUnlstAdd.SelectedValue = dt.Rows[j]["Col1"].ToString();
                                    txtGiftUnlstQty.Text = dt.Rows[j]["Col2"].ToString();
                                    rowIndex++;
                                }
                            }
                        }
                    }
                    foreach (GridViewRow gridRow in grdGiftUnlst.Rows)
                    {
                        DropDownList ddlGiftAdd =
                         (DropDownList)gridRow.Cells[1].FindControl("ddlGiftUnlstAdd");
                        sGiftName = ddlGiftAdd.SelectedItem.Text.ToString();
                        sGiftcode = ddlGiftAdd.SelectedValue.ToString();
                        TextBox txtGiftQty =
                          (TextBox)gridRow.Cells[2].FindControl("txtGiftUnlstQty");
                        sGiftQty = txtGiftQty.Text.ToString();
                        if (ddlGiftAdd.SelectedIndex != 0)
                        {
                            if (sGiftName.Trim().Length > 0)
                            {
                                sGifts = sGifts + sGiftName + "~" + sGiftQty + "$" + "#"; ;
                                sGiftcodedtls = sGiftcodedtls + sGiftcode + "~" + sGiftQty + "$" + "#"; ;
                            }
                        }
                    }
                    ds.Tables[0].Rows[i]["AddGift"] = sGifts;
                    ds.Tables[0].Rows[i]["AddGiftCode"] = sGiftcodedtls;

                    if (sGifts.Trim().Length > 0)
                    {
                        ViewState["UnlstAddGiftExists"] = "";
                    }

                }
            }
            else if (mode == "1")
            {
                if (ddlN_unProd1.SelectedIndex > 0)
                {
                    ds.Tables[0].Rows[i]["prod1"] = ddlN_unProd1.SelectedItem.Text.ToString();
                    ds.Tables[0].Rows[i]["prod1_code"] = ddlN_unProd1.SelectedValue.Trim();
                    ds.Tables[0].Rows[i]["qty1"] = txtN_UQty1.Text.Trim();
                    //ds.Tables[0].Rows[i]["prod_pob1"] = txtN_UQty1.Text.Trim();
                }
                else
                {
                    ds.Tables[0].Rows[i]["prod1"] = "";
                    ds.Tables[0].Rows[i]["prod1_code"] = "0";
                    ds.Tables[0].Rows[i]["qty1"] = "";
                    ds.Tables[0].Rows[i]["prod_pob1"] = "";
                }
                if (ddlN_unProd2.SelectedIndex > 0)
                {
                    ds.Tables[0].Rows[i]["prod2"] = ddlN_unProd2.SelectedItem.Text.ToString();
                    ds.Tables[0].Rows[i]["prod2_code"] = ddlN_unProd2.SelectedValue.Trim();
                    ds.Tables[0].Rows[i]["qty2"] = txtN_UQty2.Text.Trim();
                    // ds.Tables[0].Rows[i]["prod_pob2"] = txtN_UQty2.Text.Trim();
                }
                else
                {
                    ds.Tables[0].Rows[i]["prod2"] = "";
                    ds.Tables[0].Rows[i]["prod2_code"] = "0";
                    ds.Tables[0].Rows[i]["qty2"] = "";
                    ds.Tables[0].Rows[i]["prod_pob2"] = "";
                }
                if (ddlN_unProd3.SelectedIndex > 0)
                {
                    ds.Tables[0].Rows[i]["prod3"] = ddlN_unProd3.SelectedItem.Text.ToString();
                    ds.Tables[0].Rows[i]["prod3_code"] = ddlN_unProd3.SelectedValue.Trim();
                    ds.Tables[0].Rows[i]["qty3"] = txtN_UQty3.Text.Trim();
                    // ds.Tables[0].Rows[i]["prod_pob3"] = txtN_UQty3.Text.Trim();
                }
                else
                {
                    ds.Tables[0].Rows[i]["prod3"] = "";
                    ds.Tables[0].Rows[i]["prod3_code"] = "0";
                    ds.Tables[0].Rows[i]["qty3"] = "";
                    ds.Tables[0].Rows[i]["prod_pob3"] = "";
                }
                if (ddlN_ungift.SelectedIndex > 0)
                {
                    ds.Tables[0].Rows[i]["gift"] = ddlN_ungift.SelectedItem.Text.Trim();
                    ds.Tables[0].Rows[i]["gift_code"] = ddlN_ungift.SelectedValue.Trim();
                    ds.Tables[0].Rows[i]["gqty"] = txtN_GQty.Text.Trim();
                }
                else
                {
                    ds.Tables[0].Rows[i]["gift"] = "";
                    ds.Tables[0].Rows[i]["gift_code"] = "0";
                    ds.Tables[0].Rows[i]["gqty"] = "";
                }
            }
            if (mode == "1")
            {
                if (sf_type == "2")
                {
                    ds.Tables[0].Rows[i]["sfcode"] = ddlUnMR.SelectedValue.Trim();
                }
                ds.Tables[0].Rows[i]["terr"] = ddlTerr_Un.SelectedValue.Trim();
                ds.Tables[0].Rows[i]["spec"] = ddlSpec_Un.SelectedValue.Trim();
                ds.Tables[0].Rows[i]["cat"] = ddlCate_Un.SelectedValue.Trim();
                ds.Tables[0].Rows[i]["class"] = ddlClass_Un.SelectedValue.Trim();
                ds.Tables[0].Rows[i]["qual"] = ddlQual_Un.SelectedValue.Trim();

            }
            else if (mode == "0")
            {
                ds.Tables[0].Rows[i]["terr"] = "0";
                ds.Tables[0].Rows[i]["spec"] = "0";
                ds.Tables[0].Rows[i]["cat"] = "0";
                ds.Tables[0].Rows[i]["class"] = "0";
                ds.Tables[0].Rows[i]["qual"] = "0";
                ds.Tables[0].Rows[i]["sfcode"] = sf_code;
            }
            if (ierr == 0)
            {
                sFile = sf_code + "_" + ViewState["curdate"].ToString() + "UnLstDR.xml";
                ds.WriteXml(Server.MapPath(sFile));
                hdnundocedit.Value = "";
                hdnNewUnDoc.Value = "";
                grdUnLstDR.Enabled = true;
                BindGrid_UnListedDR("0");
                if (mode == "1")
                {
                    NPnlUnLst.Attributes.Add("style", "display:none");
                    pnlMultiView.Enabled = true;
                    pnlTab.Enabled = true;
                    pnlTop.Enabled = true;
                    pnlTab1.Enabled = true;
                }
            }

        }
        else
        {

            if (UnDrhdnValue.Value != "")
                UnLstTxtDR.Text = UnDrhdnValue.Value;
            // Added by Sridevi to create Header - 02/Oct/15
            Create_Head(ddlSDP.SelectedValue.ToString());

            XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(Server.MapPath("UnLstDR.xml"));
            sFile = sf_code + "_" + ViewState["curdate"].ToString() + "UnLstDR.xml";
            xmldoc.Load(Server.MapPath(sFile));

            XmlElement parentelement = xmldoc.CreateElement("DCR");

            XmlElement xmlsession = xmldoc.CreateElement("session");
            XmlElement xmltime = xmldoc.CreateElement("time");
            XmlElement xmlmin = xmldoc.CreateElement("min");
            XmlElement xmlsec = xmldoc.CreateElement("sec");
            XmlElement xmlDR = xmldoc.CreateElement("drcode");
            XmlElement xmlworkwith = xmldoc.CreateElement("workwith");
            XmlElement xmlprod1 = xmldoc.CreateElement("prod1");
            XmlElement xmlqty1 = xmldoc.CreateElement("qty1");
            XmlElement xmlprod_pob1 = xmldoc.CreateElement("prod_pob1");
            XmlElement xmlprod2 = xmldoc.CreateElement("prod2");
            XmlElement xmlqty2 = xmldoc.CreateElement("qty2");
            XmlElement xmlprod_pob2 = xmldoc.CreateElement("prod_pob2");
            XmlElement xmlprod3 = xmldoc.CreateElement("prod3");
            XmlElement xmlqty3 = xmldoc.CreateElement("qty3");
            XmlElement xmlprod_pob3 = xmldoc.CreateElement("prod_pob3");
            XmlElement xmlgift = xmldoc.CreateElement("gift");
            XmlElement xmlgqty = xmldoc.CreateElement("gqty");
            XmlElement xmldr_code = xmldoc.CreateElement("dr_code");
            XmlElement xmlsf_code = xmldoc.CreateElement("sf_code");

            XmlElement xmlsess_code = xmldoc.CreateElement("sess_code");
            XmlElement xmltime_code = xmldoc.CreateElement("time_code");

            XmlElement xmlprod1_code = xmldoc.CreateElement("prod1_code");
            XmlElement xmlprod2_code = xmldoc.CreateElement("prod2_code");
            XmlElement xmlprod3_code = xmldoc.CreateElement("prod3_code");
            XmlElement xmlgiftcode = xmldoc.CreateElement("gift_code");
            XmlElement xmlAddProdCode = xmldoc.CreateElement("AddProdCode");
            XmlElement xmlAddProd = xmldoc.CreateElement("AddProd");

            XmlElement xmlAddGiftCode = xmldoc.CreateElement("AddGiftCode");
            XmlElement xmlAddGift = xmldoc.CreateElement("AddGift");

            XmlElement xmlterr = xmldoc.CreateElement("terr");
            XmlElement xmlspec = xmldoc.CreateElement("spec");
            XmlElement xmlcat = xmldoc.CreateElement("cat");
            XmlElement xmlclass = xmldoc.CreateElement("class");
            XmlElement xmlqual = xmldoc.CreateElement("qual");
            XmlElement xmladd = xmldoc.CreateElement("add");
            XmlElement xmlnew = xmldoc.CreateElement("new");
            XmlElement xmlsfcode = xmldoc.CreateElement("sfcode");
            XmlElement xmlww_code = xmldoc.CreateElement("ww_code");

            if (mode == "0")
            {
                if (ddlUnLstDR_Session.SelectedIndex > 0)
                {
                    xmlsession.InnerText = ddlUnLstDR_Session.SelectedItem.Text.ToString();
                    xmlsess_code.InnerText = ddlUnLstDR_Session.SelectedValue.Trim();
                }
                else
                {
                    xmlsession.InnerText = "";
                    xmlsess_code.InnerText = "0";
                }
                if (ddlMinute.SelectedIndex > 0)
                {
                    xmltime.InnerText = ddlMinute.SelectedItem.Text.ToString() + ":" + ddlSec.SelectedItem.ToString();
                    xmlmin.InnerText = ddlMinute.SelectedItem.Text.ToString();
                }
                else
                {
                    xmltime.InnerText = "";
                    xmlmin.InnerText = "";
                }
                if (ddlSec.SelectedIndex > 0)
                {
                    xmlsec.InnerText = ddlSec.SelectedItem.Text.ToString();
                }
                else
                {
                    xmlsec.InnerText = "";
                }
                if (UnLstTxtDR.Text.Trim().Length > 0)
                {
                    xmlDR.InnerText = Untxt_Dr.Text.Trim();
                    xmldr_code.InnerText = UnLstTxtDR.Text.Trim();
                }
                else
                {
                    xmlDR.InnerText = "";
                    xmldr_code.InnerText = "0";
                }
                xmlworkwith.InnerText = txtUnLstDR_WW.Value.ToString(); //txtFieldForce.Text.ToString();
                xmlsf_code.InnerText = txtUnLstDR_WW.Value.ToString();
                xmlww_code.InnerText = hdnunww_code.Value.ToString();

                if (ddlUnLstDR_Prod1.SelectedItem.Text.ToString() == "-Product-")
                {
                    xmlprod1.InnerText = "";
                    xmlprod1_code.InnerText = "0";
                    xmlqty1.InnerText = "";
                    xmlprod_pob1.InnerText = "";
                }
                else
                {
                    xmlprod1.InnerText = ddlUnLstDR_Prod1.SelectedItem.Text.ToString() + "~";
                    xmlprod1_code.InnerText = ddlUnLstDR_Prod1.SelectedValue;
                    xmlqty1.InnerText = txtUnLstDR_Prod_Qty1.Text;
                    xmlprod_pob1.InnerText = txtUnLstDR_Prod_POB1.Text;
                }

                if (ddlUnLstDR_Prod2.SelectedItem.Text.ToString() == "-Product-")
                {
                    xmlprod2.InnerText = "";
                    xmlprod2_code.InnerText = "0";
                    xmlqty2.InnerText = "";
                    xmlprod_pob2.InnerText = "";
                }
                else
                {
                    xmlprod2.InnerText = ddlUnLstDR_Prod2.SelectedItem.Text.ToString() + "~";
                    xmlprod2_code.InnerText = ddlUnLstDR_Prod2.SelectedValue.Trim();
                    xmlqty2.InnerText = txtUnLstDR_Prod_Qty2.Text;
                    xmlprod_pob2.InnerText = txtUnLstDR_Prod_POB2.Text;
                }

                if (ddlUnLstDR_Prod3.SelectedItem.Text.ToString() == "-Product-")
                {
                    xmlprod3.InnerText = "";
                    xmlprod3_code.InnerText = "0";
                    xmlqty3.InnerText = "";
                    xmlprod_pob3.InnerText = "";
                }
                else
                {
                    xmlprod3.InnerText = ddlUnLstDR_Prod3.SelectedItem.Text.ToString() + "~";
                    xmlprod3_code.InnerText = ddlUnLstDR_Prod3.SelectedValue.Trim();
                    xmlqty3.InnerText = txtUnLstDR_Prod_Qty3.Text;
                    xmlprod_pob3.InnerText = txtUnLstDR_Prod_POB3.Text;
                }

                if (ddlUnLstDR_Prod1.SelectedIndex > 0  && ddlUnLstDR_Prod2.SelectedIndex > 0 && ddlUnLstDR_Prod3.SelectedIndex > 0 )
                {

                    string sProdName = string.Empty;
                    string sQty = string.Empty;
                    string sProducts = string.Empty;
                    string sProdcode = string.Empty;
                    string sProductcodes = string.Empty;

                    foreach (GridViewRow gridRow in grvProductUnlst.Rows)
                    {
                        DropDownList ddlProductUnlstAdd = (DropDownList)gridRow.Cells[1].FindControl("ddlProductUnlstAdd");
                        sProdName = ddlProductUnlstAdd.SelectedItem.Text.ToString();
                        sProdcode = ddlProductUnlstAdd.SelectedValue.Trim();
                        TextBox txtProdUnlstQty = (TextBox)gridRow.Cells[2].FindControl("txtProdUnlstQty");
                        sQty = txtProdUnlstQty.Text.ToString();
                        if (ddlProductUnlstAdd.SelectedIndex != 0)
                        {
                            if (sProdName.Trim().Length > 0)
                            {
                                sProducts = sProducts + sProdName + "~" + sQty + "$" + "#";
                                sProductcodes = sProductcodes + sProdcode + "~" + sQty + "$" + "#";
                            }
                        }
                             
                    }
                    xmlAddProd.InnerText = sProducts;
                    xmlAddProdCode.InnerText = sProductcodes;

                    if (sProducts.Trim().Length > 0)
                    {
                        ViewState["UnlstAddProdExists"] = "";
                    }
                }


                if (ddlUnLstDR_Gift.SelectedItem.Text.ToString() == "-Select-")
                {
                    xmlgift.InnerText = "";
                    xmlgiftcode.InnerText = "0";
                    xmlgqty.InnerText = "";
                }
                else
                {
                    xmlgift.InnerText = ddlUnLstDR_Gift.SelectedItem.Text.ToString() + "~";
                    xmlgiftcode.InnerText = ddlUnLstDR_Gift.SelectedValue.Trim();
                    xmlgqty.InnerText = txtUnLstDR_GQty.Text;
                }

                if (ddlUnLstDR_Gift.SelectedIndex > 0 )
                {
                    string sGiftName = string.Empty;
                    string sGiftQty = string.Empty;
                    string sGifts = string.Empty;
                    string sGiftcodedtls = string.Empty;
                    string sGiftcode = string.Empty;
                    foreach (GridViewRow gridRow in grdGiftUnlst.Rows)
                    {
                        DropDownList ddlGiftAdd =
                            (DropDownList)gridRow.Cells[1].FindControl("ddlGiftUnlstAdd");
                        sGiftName = ddlGiftAdd.SelectedItem.Text.ToString();
                        sGiftcode = ddlGiftAdd.SelectedValue.ToString();
                        TextBox txtGiftQty =
                            (TextBox)gridRow.Cells[2].FindControl("txtGiftUnlstQty");
                        sGiftQty = txtGiftQty.Text.ToString();
                        if (ddlGiftAdd.SelectedIndex != 0)
                        {
                            if (sGiftName.Trim().Length > 0)
                            {
                                sGifts = sGifts + sGiftName + "~" + sGiftQty + "$" + "#"; ;
                                sGiftcodedtls = sGiftcodedtls + sGiftcode + "~" + sGiftQty + "$" + "#"; ;
                            }
                        }

                    }
                    xmlAddGift.InnerText = sGifts;
                    xmlAddGiftCode.InnerText = sGiftcodedtls;

                    if (sGifts.Trim().Length > 0)
                    {
                        ViewState["AddGiftExists"] = "";
                    }

                }
                xmladd.InnerText = "";
                xmlterr.InnerText = "0";
                xmlspec.InnerText = "0";
                xmlcat.InnerText = "0";
                xmlclass.InnerText = "0";
                xmlqual.InnerText = "0";
                xmlnew.InnerText = "";
                xmlsfcode.InnerText = sf_code;
            }
            else if (mode == "1")
            {
                if (ddlN_unsess.SelectedIndex > 0)
                {
                    xmlsession.InnerText = ddlN_unsess.SelectedItem.Text.ToString();
                    xmlsess_code.InnerText = ddlN_unsess.SelectedValue.Trim();
                }
                else
                {
                    xmlsession.InnerText = "";
                    xmlsess_code.InnerText = "0";
                }
                if (ddlN_untime.SelectedIndex > 0)
                {
                    xmltime.InnerText = ddlN_untime.SelectedItem.Text.ToString() + ":" + ddlN_unmin.SelectedItem.ToString();
                    xmlmin.InnerText = ddlN_untime.SelectedItem.Text.ToString();
                }
                else
                {
                    xmltime.InnerText = "";
                    xmlmin.InnerText = "";
                }
                if (ddlN_unmin.SelectedIndex > 0)
                {
                    xmlsec.InnerText = ddlN_unmin.SelectedItem.Text.ToString();
                }
                else
                {
                    xmlsec.InnerText = "";
                }

                if (txtUnDr.Text.Length > 0)
                {
                    xmlDR.InnerText = txtUnDr.Text;
                    xmldr_code.InnerText = "";
                }
                else
                {
                    xmlDR.InnerText = "";
                    xmldr_code.InnerText = "";
                }

                //chkFieldForce.Items.Add

                xmlworkwith.InnerText = txtUn.Text;
                xmlsf_code.InnerText = txtUn.Text;
                xmlww_code.InnerText = unhidDr.Value.ToString();
                if (ddlN_unProd1.SelectedItem.Text.ToString() == "-Product-")
                {
                    xmlprod1.InnerText = "";
                    xmlprod1_code.InnerText = "0";
                    xmlqty1.InnerText = "";
                    xmlprod_pob1.InnerText = "";
                }
                else
                {
                    xmlprod1.InnerText = ddlN_unProd1.SelectedItem.Text.ToString() + "~";
                    xmlprod1_code.InnerText = ddlN_unProd1.SelectedValue;
                    xmlqty1.InnerText = txtN_UQty1.Text;
                    // xmlprod_pob1.InnerText = txtN_UQty1.Text;
                }

                if (ddlN_unProd2.SelectedItem.Text.ToString() == "-Product-")
                {
                    xmlprod2.InnerText = "";
                    xmlprod2_code.InnerText = "0";
                    xmlqty2.InnerText = "";
                    xmlprod_pob2.InnerText = "";
                }
                else
                {
                    xmlprod2.InnerText = ddlN_unProd2.SelectedItem.Text.ToString() + "~";
                    xmlprod2_code.InnerText = ddlN_unProd2.SelectedValue.Trim();
                    xmlqty2.InnerText = txtN_UQty2.Text;
                    //  xmlprod_pob2.InnerText = txtN_UQty2.Text;
                }

                if (ddlN_unProd3.SelectedItem.Text.ToString() == "-Product-")
                {
                    xmlprod3.InnerText = "";
                    xmlprod3_code.InnerText = "0";
                    xmlqty3.InnerText = "";
                    xmlprod_pob3.InnerText = "";
                }
                else
                {
                    xmlprod3.InnerText = ddlN_unProd3.SelectedItem.Text.ToString() + "~";
                    xmlprod3_code.InnerText = ddlN_unProd3.SelectedValue.Trim();
                    xmlqty3.InnerText = txtN_UQty3.Text;
                    // xmlprod_pob3.InnerText = txtN_UQty3.Text;
                }


                if (ddlN_ungift.SelectedItem.Text.ToString() == "-Select-")
                {
                    xmlgift.InnerText = "";
                    xmlgiftcode.InnerText = "0";
                    xmlgqty.InnerText = "";
                }
                else
                {
                    xmlgift.InnerText = ddlN_ungift.SelectedItem.Text.ToString() + "~";
                    xmlgiftcode.InnerText = ddlN_ungift.SelectedValue.Trim();
                    xmlgqty.InnerText = txtN_GQty.Text;
                }
                if (txtUnDrAddr.Text.Length > 0)
                {
                    xmladd.InnerText = txtUnDrAddr.Text.Trim();
                }
                else
                {
                    xmladd.InnerText = "";
                }
                if (sf_type == "2")
                {
                    xmlsfcode.InnerText = ddlUnMR.SelectedValue.Trim();
                }
                xmlterr.InnerText = ddlTerr_Un.SelectedValue.Trim();
                xmlspec.InnerText = ddlSpec_Un.SelectedValue.Trim();
                xmlcat.InnerText = ddlCate_Un.SelectedValue.Trim();
                xmlclass.InnerText = ddlClass_Un.SelectedValue.Trim();
                xmlqual.InnerText = ddlQual_Un.SelectedValue.Trim();
                xmlnew.InnerText = "New";
            }
            if (ierr == 0)
            {
                parentelement.AppendChild(xmlsession);
                parentelement.AppendChild(xmltime);
                parentelement.AppendChild(xmlmin);
                parentelement.AppendChild(xmlsec);
                parentelement.AppendChild(xmlDR);
                parentelement.AppendChild(xmlworkwith);
                parentelement.AppendChild(xmlprod1);
                parentelement.AppendChild(xmlqty1);
                parentelement.AppendChild(xmlprod_pob1);
                parentelement.AppendChild(xmlprod2);
                parentelement.AppendChild(xmlqty2);
                parentelement.AppendChild(xmlprod_pob2);
                parentelement.AppendChild(xmlprod3);
                parentelement.AppendChild(xmlqty3);
                parentelement.AppendChild(xmlprod_pob3);

                parentelement.AppendChild(xmlgift);
                parentelement.AppendChild(xmlgqty);
                parentelement.AppendChild(xmldr_code);
                parentelement.AppendChild(xmlsess_code);
                parentelement.AppendChild(xmltime_code);
                parentelement.AppendChild(xmlprod1_code);
                parentelement.AppendChild(xmlprod2_code);
                parentelement.AppendChild(xmlprod3_code);
                parentelement.AppendChild(xmlgiftcode);
                parentelement.AppendChild(xmlAddProd);
                parentelement.AppendChild(xmlAddProdCode);
                parentelement.AppendChild(xmlAddGift);
                parentelement.AppendChild(xmlAddGiftCode);

                parentelement.AppendChild(xmlterr);
                parentelement.AppendChild(xmlspec);
                parentelement.AppendChild(xmlcat);
                parentelement.AppendChild(xmlclass);
                parentelement.AppendChild(xmlqual);
                parentelement.AppendChild(xmladd);
                parentelement.AppendChild(xmlnew);
                parentelement.AppendChild(xmlsfcode);
                parentelement.AppendChild(xmlww_code);

                xmldoc.DocumentElement.AppendChild(parentelement);
                //xmldoc.Save(Server.MapPath("UnLstDR.xml"));
                xmldoc.Save(Server.MapPath(sFile));

                BindGrid_UnListedDR("0");

                UnDrhdnValue.Value = "";
                if (mode == "1")
                {
                    NPnlUnLst.Attributes.Add("style", "display:none");
                    pnlMultiView.Enabled = true;
                    pnlTab.Enabled = true;
                    pnlTop.Enabled = true;
                    pnlTab1.Enabled = true;
                }
            }        
        }
        ViewState["CurrentTableGiftUnlst"] = null;
        ViewState["CurrentTableUnlst"] = null;
        udrcolor();
    }
    //protected void btnNunCancel_Click(object sender, EventArgs e)
    //{
    //    NPnlUnLst.Attributes.Add("style", "display:none");
    //    pnlMultiView.Enabled = true;
    //    pnlMultiView.Enabled = true;
    //    pnlTab.Enabled = true;
    //    pnlTop.Enabled = true;
    //    pnlTab1.Enabled = true;
    //    if (ViewState["UnLDEdit"].ToString() != "")
    //    {
    //        grdUnLstDR.Enabled = true;
    //        BindGrid_UnListedDR("0");
    //        ViewState["UnLDEdit"] = "";
    //    }
    //    udrcolor();
    //}

    //protected void ChkUn_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string name = "";
    //    string id = "";

    //    CheckBoxList ChkUn = (CheckBoxList)PnlChem.FindControl("ChkUn");
    //    TextBox txtUn = (TextBox)PnlChem.FindControl("txtUn");
    //    HiddenField unhidDr = (HiddenField)PnlChem.FindControl("unhidDr");

    //    unhidDr.Value = "";

    //    for (int i = 0; i < ChkUn.Items.Count; i++)
    //    {
    //        if (ChkUn.Items[i].Selected)
    //        {
    //            if (ChkUn.Items[i].Text.Length > 9)
    //            {
    //                name += ChkUn.Items[i].Text.Substring(0, 9) + ",";
    //            }
    //            else
    //            {
    //                name += ChkUn.Items[i].Text + ",";
    //            }
    //            id += ChkUn.Items[i].Value + "$$";
    //        }
    //    }

    //    txtUn.Text = name.TrimEnd(',');

    //    unhidDr.Value = id;
    //    unhidDr.Value = unhidDr.Value.Substring(0, unhidDr.Value.Length - 2);
    //}


    private void CreateXml()
    {
        if (ViewState["EditRecExist"].ToString() == "0")
        {
            CreateHeader();
            CreateListedDr();
            CreateChem();
            CreateStk();
            CreateUnLstdDr();
            CreateHos();
        }
    }

    private void CreateHeader()
    {
        DCR_New dc = new DCR_New();
        DataSet dsHdr = new DataSet();

        //Creating Header
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Header.xml";

        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Server.MapPath(sFile));

        //Verify the XML for child records (i.e., Empty (or) Not) by Sridevi on 09/15/15
        dsHdr.ReadXml(Server.MapPath(sFile));
        if (!(dsHdr != null && dsHdr.HasChanges()))
        {
            XmlElement parentelement = xmldoc.CreateElement("DCR");

            XmlElement xmlworktype = xmldoc.CreateElement("worktype");
            XmlElement xmlsdp = xmldoc.CreateElement("sdp");
            XmlElement xmlrem = xmldoc.CreateElement("remarks");
            XmlElement xmldate = xmldoc.CreateElement("date");


            dsxml = dc.get_Trans_Head(sf_code, lblCurDate.Text);

            if (dsxml.Tables[0].Rows.Count > 0)
            {
                ViewState["EditRecExist"] = "1";
                xmlworktype.InnerText = dsxml.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                xmlsdp.InnerText = dsxml.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                xmlrem.InnerText = dsxml.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                if (dsxml.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "")
                    xmldate.InnerText = dsxml.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                else
                    xmldate.InnerText = DateTime.Now.ToString();
            }

            parentelement.AppendChild(xmlworktype);
            parentelement.AppendChild(xmlsdp);
            parentelement.AppendChild(xmlrem);
            parentelement.AppendChild(xmldate);

            xmldoc.DocumentElement.AppendChild(parentelement);

            xmldoc.Save(Server.MapPath(sFile));
            Bind_Header();
        }
    }

    private void CreateListedDr()
    {
        DataSet dsdoc = new DataSet();
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_ListedDR.xml";

        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Server.MapPath(sFile));

        //Verify the XML for child records (i.e., Empty (or) Not) by Sridevi on 09/15/15
        dsdoc.ReadXml(Server.MapPath(sFile));
        if (!(dsdoc != null && dsdoc.HasChanges()))
        {
            dsxml = dc.get_Lst_Trans(sf_code, lblCurDate.Text);

            if (dsxml.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drFF in dsxml.Tables[0].Rows)
                {
                    //sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_ListedDR.xml";

                    //XmlDocument xmldoc = new XmlDocument();
                    //xmldoc.Load(Server.MapPath(sFile));

                    XmlElement parentelement = xmldoc.CreateElement("DCR");

                    XmlElement xmlsession = xmldoc.CreateElement("session");
                    XmlElement xmltime = xmldoc.CreateElement("time");

                    XmlElement xmlDR = xmldoc.CreateElement("drcode");
                    XmlElement xmlworkwith = xmldoc.CreateElement("workwith");

                    XmlElement xmlprod1 = xmldoc.CreateElement("prod1");
                    XmlElement xmlqty1 = xmldoc.CreateElement("qty1");
                    XmlElement xmlprod_pob1 = xmldoc.CreateElement("prod_pob1");
                    XmlElement xmlprod2 = xmldoc.CreateElement("prod2");
                    XmlElement xmlqty2 = xmldoc.CreateElement("qty2");
                    XmlElement xmlprod_pob2 = xmldoc.CreateElement("prod_pob2");
                    XmlElement xmlprod3 = xmldoc.CreateElement("prod3");
                    XmlElement xmlqty3 = xmldoc.CreateElement("qty3");
                    XmlElement xmlprod_pob3 = xmldoc.CreateElement("prod_pob3");
                    XmlElement xmlAddProd = xmldoc.CreateElement("AddProd");
                    XmlElement xmlAddProdCode = xmldoc.CreateElement("AddProdCode");
                    XmlElement xmlgift = xmldoc.CreateElement("gift");
                    XmlElement xmlgqty = xmldoc.CreateElement("gqty");
                    XmlElement xmlAddGift = xmldoc.CreateElement("AddGift");
                    XmlElement xmlAddGiftCode = xmldoc.CreateElement("AddGiftCode");
                    XmlElement xmldr_code = xmldoc.CreateElement("dr_code");
                    XmlElement xmlsf_code = xmldoc.CreateElement("sf_code");
                    XmlElement xmlsess_code = xmldoc.CreateElement("sess_code");
                    XmlElement xmlminute = xmldoc.CreateElement("minute");
                    XmlElement xmlseconds = xmldoc.CreateElement("seconds");
                    XmlElement xmlprod1_code = xmldoc.CreateElement("prod1_code");
                    XmlElement xmlprod2_code = xmldoc.CreateElement("prod2_code");
                    XmlElement xmlprod3_code = xmldoc.CreateElement("prod3_code");
                    XmlElement xmlgiftcode = xmldoc.CreateElement("gift_code");
                    XmlElement xmlremarks = xmldoc.CreateElement("remarks");
                    XmlElement xmlww_code = xmldoc.CreateElement("ww_code");

                    xmlsession.InnerText = drFF["Session"].ToString();
                    xmlsess_code.InnerText = drFF["Session_Code"].ToString();
                    xmltime.InnerText = drFF["Time"].ToString();
                    xmlminute.InnerText = drFF["Minutes"].ToString();

                    xmlseconds.InnerText = drFF["Seconds"].ToString();
                    xmlDR.InnerText = drFF["Trans_Detail_Name"].ToString();
                    xmldr_code.InnerText = drFF["Trans_Detail_Info_Code"].ToString();
                    xmlworkwith.InnerText = drFF["Worked_with_Name"].ToString();
                    xmlww_code.InnerText = drFF["Worked_with_code"].ToString();
                    xmlsf_code.InnerText = drFF["Worked_with_Name"].ToString();
                    //13~2$#13~4$#4$
                    string prod = drFF["Product_Code"].ToString();
                    string[] addprod = prod.Split('#');
                    int index = 0;
                    foreach (string aprod in addprod)
                    {
                        //Levox~1$ # LAPP~2$#
                        if (aprod != "")
                        {
                            index = index + 1;
                            string prodcode = aprod.Substring(0, aprod.IndexOf("~")); //aprod.EndsWith('~');
                            string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - (prodcode.Length + 2)));
                            // string Qty = aprod.Substring(aprod.IndexOf("~") + 1, aprod.IndexOf("$"));

                            if (index == 1)
                            {
                                xmlprod1.InnerText = prodcode;
                                xmlprod1_code.InnerText = prodcode;
                                xmlqty1.InnerText = Qty;
                                xmlprod_pob1.InnerText = "";
                            }
                            else if (index == 2)
                            {
                                xmlprod2.InnerText = prodcode;
                                xmlprod2_code.InnerText = prodcode;
                                xmlqty2.InnerText = Qty;
                                xmlprod_pob2.InnerText = "";
                            }
                            else if (index == 3)
                            {
                                xmlprod3.InnerText = prodcode;
                                xmlprod3_code.InnerText = prodcode;
                                xmlqty3.InnerText = Qty;
                                xmlprod_pob3.InnerText = "";
                            }
                        }
                    }
                    if (index == 1)
                    {
                        xmlprod2.InnerText = "0";
                        xmlprod2_code.InnerText = "0";
                        xmlqty2.InnerText = "";
                        xmlprod_pob2.InnerText = "";
                    }
                    if (index == 2)
                    {
                        xmlprod3.InnerText = "0";
                        xmlprod3_code.InnerText = "0";
                        xmlqty3.InnerText = "";
                        xmlprod_pob3.InnerText = "";
                    }

                    //13~2$#13~4$#4$
                    string proddet = drFF["Product_Detail"].ToString();
                    string[] addproddet = proddet.Split('#');
                    int indexdet = 0;
                    foreach (string aprod in addproddet)
                    {
                        //Levox~1$ # LAPP~2$#
                        if (aprod != "")
                        {
                            indexdet = indexdet + 1;
                            string proddetail = aprod.Substring(0, aprod.IndexOf("~")); //aprod.EndsWith('~');

                            if (indexdet == 1)
                            {
                                xmlprod1.InnerText = proddetail;

                            }
                            else if (indexdet == 2)
                            {
                                xmlprod2.InnerText = proddetail;

                            }
                            else if (indexdet == 3)
                            {
                                xmlprod3.InnerText = proddetail;

                            }
                        }
                    }
                    if (indexdet == 1)
                    {
                        xmlprod2.InnerText = "";

                    }
                    if (indexdet == 2)
                    {
                        xmlprod3.InnerText = "";

                    }

                    xmlAddProd.InnerText = drFF["Additional_Prod_Dtls"].ToString();
                    xmlAddProdCode.InnerText = drFF["Additional_Prod_Code"].ToString();

                    xmlgift.InnerText = drFF["Gift_Name"].ToString();
                    xmlgqty.InnerText = drFF["Gift_Qty"].ToString();
                    xmlgiftcode.InnerText = drFF["Gift_Code"].ToString();

                    xmlAddGift.InnerText = drFF["Additional_Gift_Dtl"].ToString();
                    xmlAddGiftCode.InnerText = drFF["Additional_Gift_Code"].ToString();
                    xmlremarks.InnerText = drFF["Activity_Remarks"].ToString();

                    parentelement.AppendChild(xmlsession);
                    parentelement.AppendChild(xmltime);
                    parentelement.AppendChild(xmlDR);
                    parentelement.AppendChild(xmlworkwith);
                    parentelement.AppendChild(xmlprod1);
                    parentelement.AppendChild(xmlqty1);
                    parentelement.AppendChild(xmlprod_pob1);
                    parentelement.AppendChild(xmlprod2);
                    parentelement.AppendChild(xmlqty2);
                    parentelement.AppendChild(xmlprod_pob2);
                    parentelement.AppendChild(xmlprod3);
                    parentelement.AppendChild(xmlqty3);
                    parentelement.AppendChild(xmlprod_pob3);
                    parentelement.AppendChild(xmlAddProd);
                    parentelement.AppendChild(xmlAddProdCode);
                    parentelement.AppendChild(xmlgift);
                    parentelement.AppendChild(xmlgqty);
                    parentelement.AppendChild(xmlAddGift);
                    parentelement.AppendChild(xmlAddGiftCode);
                    parentelement.AppendChild(xmldr_code);
                    parentelement.AppendChild(xmlsf_code);
                    parentelement.AppendChild(xmlsess_code);
                    parentelement.AppendChild(xmlminute);
                    parentelement.AppendChild(xmlseconds);
                    parentelement.AppendChild(xmlprod1_code);
                    parentelement.AppendChild(xmlprod2_code);
                    parentelement.AppendChild(xmlprod3_code);
                    parentelement.AppendChild(xmlgiftcode);
                    parentelement.AppendChild(xmlremarks);
                    parentelement.AppendChild(xmlww_code);

                    xmldoc.DocumentElement.AppendChild(parentelement);
                    //xmldoc.Save(Server.MapPath("DailCalls.xml"));
                    xmldoc.Save(Server.MapPath(sFile));
                }
                BindGrid("0");
            }
        }
    }

    private void CreateChem()
    {
        DataSet dschem = new DataSet();
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Chem.xml";

        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Server.MapPath(sFile));

        //Verify the XML for child records (i.e., Empty (or) Not) by Sridevi on 09/15/15
        dschem.ReadXml(Server.MapPath(sFile));
        if (!(dschem != null && dschem.HasChanges()))
        {
            dsxml = dc.get_Che_Trans(sf_code, lblCurDate.Text);

            if (dsxml.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drFF in dsxml.Tables[0].Rows)
                {

                    XmlElement parentelement = xmldoc.CreateElement("DCR");

                    XmlElement xmlsf_code = xmldoc.CreateElement("sf_code");
                    XmlElement xmlchemists = xmldoc.CreateElement("chemists");
                    XmlElement xmlchemww = xmldoc.CreateElement("chemww");
                    XmlElement xmlpobno = xmldoc.CreateElement("POBNo");

                    XmlElement xmlchem_code = xmldoc.CreateElement("chem_code");
                    XmlElement xmlterr_code = xmldoc.CreateElement("terr_code");
                    XmlElement xmlnew = xmldoc.CreateElement("new");
                    XmlElement xmlww_code = xmldoc.CreateElement("ww_code");

                    xmlsf_code.InnerText = sf_code;
                    xmlchemists.InnerText = drFF["Trans_Detail_Name"].ToString();
                    xmlchem_code.InnerText = drFF["Trans_Detail_Info_Code"].ToString();
                    xmlchemww.InnerText = drFF["Worked_with_Name"].ToString();
                    xmlpobno.InnerText = drFF["POB"].ToString();
                    xmlterr_code.InnerText = drFF["SDP"].ToString();
                    xmlnew.InnerText = "";
                    xmlww_code.InnerText = drFF["Worked_with_code"].ToString();

                    parentelement.AppendChild(xmlsf_code);
                    parentelement.AppendChild(xmlchemists);
                    parentelement.AppendChild(xmlchemww);
                    parentelement.AppendChild(xmlpobno);

                    parentelement.AppendChild(xmlchem_code);
                    parentelement.AppendChild(xmlterr_code);
                    parentelement.AppendChild(xmlnew);
                    parentelement.AppendChild(xmlww_code);

                    xmldoc.DocumentElement.AppendChild(parentelement);

                    xmldoc.Save(Server.MapPath(sFile));
                }
                BindGrid_Chem("0");
            }
        }
    }

    private void CreateStk()
    {
        DataSet dsStock = new DataSet();
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Stockiest.xml";
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Server.MapPath(sFile));

        //Verify the XML for child records (i.e., Empty (or) Not) by Sridevi on 09/15/15
        dsStock.ReadXml(Server.MapPath(sFile));
        if (!(dsStock != null && dsStock.HasChanges()))
        {
            dsxml = dc.get_Stk_Trans(sf_code, lblCurDate.Text);

            if (dsxml.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drFF in dsxml.Tables[0].Rows)
                {
                    XmlElement parentelement = xmldoc.CreateElement("DCR");

                    XmlElement xmlstockiest = xmldoc.CreateElement("stockiest");
                    XmlElement xmlstockiestww = xmldoc.CreateElement("stockww");
                    XmlElement xmlpob = xmldoc.CreateElement("pob");
                    XmlElement xmlvisit = xmldoc.CreateElement("visit");
                    XmlElement xmlstockiest_code = xmldoc.CreateElement("stockiest_code");
                    XmlElement xmlvisit_code = xmldoc.CreateElement("visit_code");
                    XmlElement xmlww_code = xmldoc.CreateElement("ww_code");

                    xmlstockiest.InnerText = drFF["Trans_Detail_Name"].ToString();
                    xmlstockiest_code.InnerText = drFF["Trans_Detail_Info_Code"].ToString();
                    xmlstockiestww.InnerText = drFF["Worked_with_Name"].ToString();
                    xmlpob.InnerText = drFF["POB"].ToString();
                    xmlvisit.InnerText = drFF["Visit_Type"].ToString();
                    xmlvisit_code.InnerText = drFF["Visit_Type"].ToString();
                    xmlww_code.InnerText = drFF["Worked_with_code"].ToString();

                    parentelement.AppendChild(xmlstockiest);
                    parentelement.AppendChild(xmlstockiestww);
                    parentelement.AppendChild(xmlpob);
                    parentelement.AppendChild(xmlvisit);
                    parentelement.AppendChild(xmlstockiest_code);
                    parentelement.AppendChild(xmlvisit_code);
                    parentelement.AppendChild(xmlww_code);

                    xmldoc.DocumentElement.AppendChild(parentelement);

                    xmldoc.Save(Server.MapPath(sFile));
                }
                BindGrid_Stockiest("0");
            }
        }
    }

    private void CreateHos()
    {
        DataSet dsHosp = new DataSet();
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Hospital.xml";

        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Server.MapPath(sFile));

        //Verify the XML for child records (i.e., Empty (or) Not) by Sridevi on 09/15/15
        dsHosp.ReadXml(Server.MapPath(sFile));
        if (!(dsHosp != null && dsHosp.HasChanges()))
        {
            dsxml = dc.get_Hos_Trans(sf_code, lblCurDate.Text);

            if (dsxml.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drFF in dsxml.Tables[0].Rows)
                {

                    XmlElement parentelement = xmldoc.CreateElement("DCR");

                    XmlElement xmlhospital = xmldoc.CreateElement("hospital");
                    XmlElement xmlhospitalww = xmldoc.CreateElement("hosww");
                    XmlElement xmlpob = xmldoc.CreateElement("pob");
                    XmlElement xmlhospital_code = xmldoc.CreateElement("hospital_code");
                    XmlElement xmlww_code = xmldoc.CreateElement("ww_code");

                    xmlhospital.InnerText = drFF["Trans_Detail_Name"].ToString();
                    xmlhospital_code.InnerText = drFF["Trans_Detail_Info_Code"].ToString();
                    xmlhospitalww.InnerText = drFF["Worked_with_Name"].ToString();
                    xmlpob.InnerText = drFF["POB"].ToString();
                    xmlww_code.InnerText = drFF["Worked_with_code"].ToString();

                    parentelement.AppendChild(xmlhospital);
                    parentelement.AppendChild(xmlhospitalww);
                    parentelement.AppendChild(xmlpob);
                    parentelement.AppendChild(xmlhospital_code);
                    parentelement.AppendChild(xmlww_code);

                    xmldoc.DocumentElement.AppendChild(parentelement);
                    xmldoc.Save(Server.MapPath(sFile));

                }
                BindGrid_Hospital("0");
            }
        }
    }

    private void CreateUnLstdDr()
    {
        DataSet dsUnDoc = new DataSet();
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "UnLstDR.xml";

        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Server.MapPath(sFile));

        //Verify the XML for child records (i.e., Empty (or) Not) by Sridevi on 09/15/15
        dsUnDoc.ReadXml(Server.MapPath(sFile));
        if (!(dsUnDoc != null && dsUnDoc.HasChanges()))
        {

            dsxml = dc.get_UnLst_Trans(sf_code, lblCurDate.Text);

            if (dsxml.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drFF in dsxml.Tables[0].Rows)
                {

                    XmlElement parentelement = xmldoc.CreateElement("DCR");

                    XmlElement xmlsession = xmldoc.CreateElement("session");
                    XmlElement xmltime = xmldoc.CreateElement("time");
                    XmlElement xmlmin = xmldoc.CreateElement("min");
                    XmlElement xmlsec = xmldoc.CreateElement("sec");
                    XmlElement xmlDR = xmldoc.CreateElement("drcode");
                    XmlElement xmlworkwith = xmldoc.CreateElement("workwith");
                    XmlElement xmlprod1 = xmldoc.CreateElement("prod1");
                    XmlElement xmlqty1 = xmldoc.CreateElement("qty1");
                    XmlElement xmlprod_pob1 = xmldoc.CreateElement("prod_pob1");
                    XmlElement xmlprod2 = xmldoc.CreateElement("prod2");
                    XmlElement xmlqty2 = xmldoc.CreateElement("qty2");
                    XmlElement xmlprod_pob2 = xmldoc.CreateElement("prod_pob2");
                    XmlElement xmlprod3 = xmldoc.CreateElement("prod3");
                    XmlElement xmlqty3 = xmldoc.CreateElement("qty3");
                    XmlElement xmlprod_pob3 = xmldoc.CreateElement("prod_pob3");
                    XmlElement xmlgift = xmldoc.CreateElement("gift");
                    XmlElement xmlgqty = xmldoc.CreateElement("gqty");
                    XmlElement xmldr_code = xmldoc.CreateElement("dr_code");
                    XmlElement xmlsf_code = xmldoc.CreateElement("sf_code");

                    XmlElement xmlsess_code = xmldoc.CreateElement("sess_code");
                    XmlElement xmltime_code = xmldoc.CreateElement("time_code");

                    XmlElement xmlprod1_code = xmldoc.CreateElement("prod1_code");
                    XmlElement xmlprod2_code = xmldoc.CreateElement("prod2_code");
                    XmlElement xmlprod3_code = xmldoc.CreateElement("prod3_code");
                    XmlElement xmlgiftcode = xmldoc.CreateElement("gift_code");
                    XmlElement xmlAddProdCode = xmldoc.CreateElement("AddProdCode");
                    XmlElement xmlAddProd = xmldoc.CreateElement("AddProd");
                    XmlElement xmlAddGiftCode = xmldoc.CreateElement("AddGiftCode");
                    XmlElement xmlAddGift = xmldoc.CreateElement("AddGift");

                    XmlElement xmlterr = xmldoc.CreateElement("terr");
                    XmlElement xmlspec = xmldoc.CreateElement("spec");
                    XmlElement xmlcat = xmldoc.CreateElement("cat");
                    XmlElement xmlclass = xmldoc.CreateElement("class");
                    XmlElement xmlqual = xmldoc.CreateElement("qual");
                    XmlElement xmladd = xmldoc.CreateElement("add");
                    XmlElement xmlnew = xmldoc.CreateElement("new");
                    XmlElement xmlsfcode = xmldoc.CreateElement("sfcode");
                    XmlElement xmlww_code = xmldoc.CreateElement("ww_code");
                    xmlsession.InnerText = drFF["Session"].ToString();
                    xmlsess_code.InnerText = drFF["Session_Code"].ToString();
                    xmltime.InnerText = drFF["Time"].ToString();
                    xmlmin.InnerText = drFF["Minutes"].ToString();

                    xmlsec.InnerText = drFF["Seconds"].ToString();
                    xmlDR.InnerText = drFF["Trans_Detail_Name"].ToString();
                    xmldr_code.InnerText = drFF["Trans_Detail_Info_Code"].ToString();
                    xmlworkwith.InnerText = drFF["Worked_with_Name"].ToString();
                    xmlww_code.InnerText = drFF["Worked_with_code"].ToString();
                    xmlsf_code.InnerText = drFF["Worked_with_Name"].ToString();
                    //13~2$#13~4$#4$
                    string prod = drFF["Product_Code"].ToString();
                    string[] addprod = prod.Split('#');
                    int index = 0;
                    foreach (string aprod in addprod)
                    {
                        //Levox~1$ # LAPP~2$#
                        if (aprod != "")
                        {
                            index = index + 1;
                            string prodcode = aprod.Substring(0, aprod.IndexOf("~")); //aprod.EndsWith('~');
                            string Qty = aprod.Substring(aprod.IndexOf("~") + 1, (aprod.Length - (prodcode.Length + 2)));
                            // string Qty = aprod.Substring(aprod.IndexOf("~") + 1, aprod.IndexOf("$"));

                            if (index == 1)
                            {
                                xmlprod1.InnerText = prodcode;
                                xmlprod1_code.InnerText = prodcode;
                                xmlqty1.InnerText = Qty;
                                xmlprod_pob1.InnerText = "";
                            }
                            else if (index == 2)
                            {
                                xmlprod2.InnerText = prodcode;
                                xmlprod2_code.InnerText = prodcode;
                                xmlqty2.InnerText = Qty;
                                xmlprod_pob2.InnerText = "";
                            }
                            else if (index == 3)
                            {
                                xmlprod3.InnerText = prodcode;
                                xmlprod3_code.InnerText = prodcode;
                                xmlqty3.InnerText = Qty;
                                xmlprod_pob3.InnerText = "";
                            }
                        }
                    }
                    if (index == 1)
                    {
                        xmlprod2.InnerText = "0";
                        xmlprod2_code.InnerText = "0";
                        xmlqty2.InnerText = "";
                        xmlprod_pob2.InnerText = "";
                    }
                    if (index == 2)
                    {
                        xmlprod3.InnerText = "0";
                        xmlprod3_code.InnerText = "0";
                        xmlqty3.InnerText = "";
                        xmlprod_pob3.InnerText = "";
                    }

                    //13~2$#13~4$#4$
                    string proddet = drFF["Product_Detail"].ToString();
                    string[] addproddet = proddet.Split('#');
                    int indexdet = 0;
                    foreach (string aprod in addproddet)
                    {
                        //Levox~1$ # LAPP~2$#
                        if (aprod != "")
                        {
                            indexdet = indexdet + 1;
                            string proddetail = aprod.Substring(0, aprod.IndexOf("~")); //aprod.EndsWith('~');

                            if (indexdet == 1)
                            {
                                xmlprod1.InnerText = proddetail;

                            }
                            else if (indexdet == 2)
                            {
                                xmlprod2.InnerText = proddetail;

                            }
                            else if (indexdet == 3)
                            {
                                xmlprod3.InnerText = proddetail;

                            }
                        }
                    }
                    if (indexdet == 1)
                    {
                        xmlprod2.InnerText = "";

                    }
                    if (indexdet == 2)
                    {
                        xmlprod3.InnerText = "";
                    }

                    xmlAddProd.InnerText = drFF["Additional_Prod_Dtls"].ToString();
                    xmlAddProdCode.InnerText = drFF["Additional_Prod_Code"].ToString();
                    xmlgift.InnerText = drFF["Gift_Name"].ToString();

                    xmlgqty.InnerText = drFF["Gift_Qty"].ToString();
                    xmlgiftcode.InnerText = drFF["Gift_Code"].ToString();

                    xmlAddGift.InnerText = drFF["Additional_Gift_Dtl"].ToString();
                    xmlAddGiftCode.InnerText = drFF["Additional_Gift_Code"].ToString();

                    xmladd.InnerText = "";
                    xmlterr.InnerText = drFF["SDP"].ToString();
                    xmlspec.InnerText = "0";
                    xmlcat.InnerText = "0";
                    xmlclass.InnerText = "0";
                    xmlqual.InnerText = "0";
                    xmlnew.InnerText = "";
                    xmlsfcode.InnerText = sf_code;


                    parentelement.AppendChild(xmlsession);
                    parentelement.AppendChild(xmltime);
                    parentelement.AppendChild(xmlDR);
                    parentelement.AppendChild(xmlworkwith);
                    parentelement.AppendChild(xmlprod1);
                    parentelement.AppendChild(xmlqty1);
                    parentelement.AppendChild(xmlprod_pob1);
                    parentelement.AppendChild(xmlprod2);
                    parentelement.AppendChild(xmlqty2);
                    parentelement.AppendChild(xmlprod_pob2);
                    parentelement.AppendChild(xmlprod3);
                    parentelement.AppendChild(xmlqty3);
                    parentelement.AppendChild(xmlprod_pob3);
                    parentelement.AppendChild(xmlAddProd);
                    parentelement.AppendChild(xmlAddProdCode);
                    parentelement.AppendChild(xmlgift);
                    parentelement.AppendChild(xmlgqty);
                    parentelement.AppendChild(xmlAddGift);
                    parentelement.AppendChild(xmlAddGiftCode);
                    parentelement.AppendChild(xmldr_code);
                    parentelement.AppendChild(xmlsf_code);
                    parentelement.AppendChild(xmlsess_code);
                    parentelement.AppendChild(xmltime_code);
                    parentelement.AppendChild(xmlmin);
                    parentelement.AppendChild(xmlsec);
                    parentelement.AppendChild(xmlprod1_code);
                    parentelement.AppendChild(xmlprod2_code);
                    parentelement.AppendChild(xmlprod3_code);
                    parentelement.AppendChild(xmlgiftcode);

                    parentelement.AppendChild(xmlterr);
                    parentelement.AppendChild(xmlspec);
                    parentelement.AppendChild(xmlcat);
                    parentelement.AppendChild(xmlclass);
                    parentelement.AppendChild(xmlqual);
                    parentelement.AppendChild(xmladd);
                    parentelement.AppendChild(xmlnew);
                    parentelement.AppendChild(xmlsfcode);
                    parentelement.AppendChild(xmlww_code);

                    xmldoc.DocumentElement.AppendChild(parentelement);
                    //xmldoc.Save(Server.MapPath("DailCalls.xml"));
                    xmldoc.Save(Server.MapPath(sFile));
                }
                BindGrid_UnListedDR("0");
            }
        }
    }
}
