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



public partial class MasterFiles_MGR_DCR_DCRIndex : System.Web.UI.Page
{
    public string sf_code = string.Empty;
    string sCurDate = string.Empty;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;

    DateTime dtDCR;
    DateTime start_date;
    DataSet dsDCR = null;
    DataSet dsTerritory = null;
    DataSet dsSF = null;
    DataSet dsWT = null;
    DataSet dsHoliday = null;
    DataSet dsLeave = null;
    string sFile = string.Empty;
    DataSet dsMgr = null;
    TreeNode node1 = new TreeNode("Root");
    int HQ = 0;
    string[] sParentNode;
    string childnode;
    string parentnode;
    string FieldWork_Ind = string.Empty;
    string ButtonAccess = string.Empty;
    string strWeekoff = string.Empty;
    string isHQ = string.Empty;
    int iWeekOffind = -1;
    int iReturn = -1;
    string sf_name = string.Empty;
    string emp_id = string.Empty;
    string employee_id = string.Empty;
    DataSet dsmgrindex = null;
    bool isAutopost = false;
    bool isReject = false;
    bool isEntry = false;
    bool isD = false;
    bool isCh = false;
    bool isSt = false;
    bool isHo = false;
    bool isUl = false;
    bool isRe = false;
    bool isLeave = false;
    bool ishq = false;
    int doc_disp = -1;
    int sess_dcr = -1;
    int time_dcr = -1;
    int UnLstDr_reqd = -1;
    int prod_qty_dcr = -1;
    int prod_sel = -1;
    int pob = -1;
    int prod_mand_dcr = -1;
    int iHoliday = -1;
    int iWeekOff = -1;
    int iDelayInd = -1;
    int iDelayHolInd = -1;
    int No_of_Days_Delay = -1;
    int dmon = -1;
    int dyear = -1;
    int diffdays = -1;
    int Qtyerr = 0;
    int iReturn_Det_val = 0;
    int max_doc_dcr_count = 0;
    int max_chem_dcr_count = 0;
    int max_stk_dcr_count = 0;
    int max_unlst_dcr_count = 0;
    int max_hos_dcr_count = 0;
    string strCase = string.Empty;
    int sess_m_dcr = -1;
    int time_m_dcr = -1;
    int iReturn_Det = -1;
    int iAppNeed = -1;
    DateTime headdate;
    bool chk = false;
    int imaxremarks = -1;
    string sf_type = string.Empty;
    string sWorkType = string.Empty;
    string WorkType = string.Empty;
    bool isEdit = false;
    string state_code = string.Empty;
    DateTime tdate;
    DataSet dshead = null;
    DataSet dsadmin = null;

    string IPAdd = string.Empty;
    string EntryMode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (!Page.IsPostBack)
        {
            AdminSetup dv = new AdminSetup();
            dsadmin = dv.getAdminSetup_MGR(div_code);
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
                txtRemarkDesc.MaxLength = imaxremarks;
                //NewbtnClear.Attributes.Add("style", "display:none");
              //  ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + NewbtnClear.ClientID + "') .style.display='none';", true);
                FillColor();
                FillTerrColor();
            }        
            ViewState["AddProdExists"] = "";
 
            Loaddcr("0");
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
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
            txtRemarkDesc.MaxLength = imaxremarks;
            FillColor();
            FillTerrColor();
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
    private void Loaddcr(string val)
    {
        // Session["backurl"] = "LstDoctorList.aspx";
        lblText.Text = "<span style='font-weight: bold;color:Black; font-size:15px; font-Names:Verdana '>" + "Daily Calls Entry For :- " + "</span>" + "<span style='font-weight: bold;color:DarkGreen; font-size:15px; font-Names:Verdana '>" + Session["sf_name"].ToString() + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"] + "</span>" + "&nbsp;&nbsp; ";
        ddlWorkType.Enabled = true;
        btnFieldWork.Visible = false;
        lblTerrHQ.Visible = false;
        lblSDP.Visible = false;
        btnSave.Visible = false;
        btnSubmit.Visible = false;
        ViewState["heading"] = "";
       // NewbtnClear.Attributes.Add("style", "display:none");
     //   ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + NewbtnClear.ClientID + "') .style.display='block';", true);
        ddlTerrHQ.Visible = false;
        ddlSDP.Visible = false;
        btnGo.Visible = false;
        lblHeader.Text = "";
        lblNote.Visible = false;
        lblReject.Text = "";
        lblCurDate.Text = "";
        pnlTree.Visible = false;
        PnlRemarks.Visible = false;
        txtRemarkDesc.Text = "";
        FillWorkType();
        FillTerrHQ();
        UnListedDR LstDR = new UnListedDR();
        dsHoliday = LstDR.getState(sf_code);
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            state_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        }
        FillSF();
        if (lblInfo.Text != "Cannot Enter DCR for Future Date !!!")
        {
            Bind_Header();
            BindGrid();
            GetWorkName();
            if (grvWorkArea.Rows.Count > 0)
            {
                lblTerrHQ.Visible = true;
                ddlWorkType.Enabled = false;
                pnlTree.Visible = true;
                ddlTerrHQ.Visible = true;
                btnGo.Visible = true;
                btnFieldWork.Visible = true;
                // NewbtnClear.Attributes.Add("style", "display:block");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + NewbtnClear.ClientID + "') .style.display='block';", true);
                btnSubmit.Visible = false;
            }
            else
            {
                ddlWorkType.SelectedIndex = 0;
                ddlWorkType.Enabled = true;
                lblInfo.Text = "Select the WorkType   ...";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + NewbtnClear.ClientID + "') .style.display='none';", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + NewbtnClear.ClientID + "') .style.display='none';", true);
        }
        if (isReject == true)
        {
            DCR_New dr = new DCR_New();
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

            }

            if (dsMgr.Tables[0].Rows.Count > 0)
            {
                WorkType = dsMgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                ddlWorkType.SelectedValue = WorkType;
                DCR_New WT = new DCR_New();
                dsWT = WT.DCR_get_WorkType(div_code, ddlWorkType.SelectedValue.ToString(),sf_type);
                if (dsMgr.Tables[0].Rows.Count > 0)
                {
                    FieldWork_Ind = dsWT.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    ButtonAccess = dsWT.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }
                if (FieldWork_Ind != "F")
                {
                    if ((FieldWork_Ind == "N") || (FieldWork_Ind == "H") || (FieldWork_Ind == "W"))
                    {
                        Bind_Header();
                        PnlRemarks.Visible = true;
                        //btnSave.Visible = true;
                        btnSubmit.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + NewbtnClear.ClientID + "') .style.display='block';", true);
                        lblInfo.Visible = false;
                        lblTerrHQ.Visible = false;
                        ddlTerrHQ.Visible = false;
                        ddlSDP.Visible = false;
                        lblSDP.Visible = false;
                        pnlTree.Visible = false;
                        btnGo.Visible = false;
                        btnFieldWork.Visible = false;
                        //NewbtnClear.Attributes.Add("style", "display:block");
                    }
                    //else if (FieldWork_Ind == "L")
                    //{
                    //    Response.Redirect("~/MasterFiles/MGR/Leave_Form_Mgr.aspx");
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + NewbtnClear.ClientID + "') .style.display='block';", true);
                   // NewbtnClear.Attributes.Add("style", "display:block");
                    PnlRemarks.Visible = false;
                    btnSave.Visible = false;
                    btnSubmit.Visible = false;
                    pnlTerr.Visible = true;
                    lblTerrHQ.Visible = true;
                    ddlTerrHQ.Visible = true;
                    lblInfo.Visible = true;
                    lblInfo.Text = "Select the HQ";
                }
            }
        }
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

        ds.ReadXml(Server.MapPath(sFile));

        if (ds != null && ds.HasChanges())
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlWorkType.SelectedValue = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                ddlSDP.SelectedValue = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
              //  txtRemarkDesc.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                headdate = Convert.ToDateTime(ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
                ds.WriteXml(Server.MapPath(sFile));
                DCR_New dc = new DCR_New();
                chk = dc.chkxml(sf_type, headdate, div_code, sf_code);
                lblInfo.Text = "";
                ViewState["Header"] = (DataSet)ds;
            }
        }
        else
        {
            ddlWorkType.SelectedValue = "0";
            ddlSDP.SelectedValue = "0";

        }

    }

    protected void BindGrid()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        //ds.ReadXml(Server.MapPath("DailCalls.xml"));

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_WorkArea.xml";

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
            ViewState["WorkArea"] = (DataSet)ds;
            grvWorkArea.DataSource = ds;
            grvWorkArea.DataBind();
        }
        else
        {
            grvWorkArea.DataBind();
        }
    }
    private string GetCaseColor(string caseSwitch)
    {
        switch (caseSwitch)
        {
            case "1":
                strCase = "#FFFFCC";
                break;
            case "2":
                strCase = "#CCCC66";
                break;
            case "3":
                strCase = "#6699FF";
                break;
            case "4":
                strCase = "#CCFFFF";
                break;
            case "5":
                strCase = "#CCFFCC";
                break;
            case "6":
                strCase = "#F7819F";
                break;
            case "7":
                strCase = "#0B610B";
                break;
            case "8":
                strCase = "#FF4000";
                break;
            case "9":
                strCase = "#FE2E64";
                break;
            case "10":
                strCase = "#9AFE2E";
                break;
            case "11":
                strCase = "#F2F5A9";
                break;
            case "12":
                strCase = "#F5A9D0";
                break;
            default:
                strCase = "#F2F5A9";
                break;
        }
        return strCase;
    }
    private void FillSDP(string sfcode)
    {
        DCR_New dc = new DCR_New();
        dsDCR = dc.getSDP(sfcode);
        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            ddlSDP.DataTextField = "Territory_Name";
            ddlSDP.DataValueField = "Territory_Code";
            ddlSDP.DataSource = dsDCR;
            ddlSDP.DataBind();
        }
    }
    private void GetWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            lblSDP.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
        }
    }
    private void FillWorkType()
    {
        DCR_New dc = new DCR_New();
        dsDCR = dc.getWorkType(div_code);
        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            ddlWorkType.DataTextField = "Worktype_Name_M";
            ddlWorkType.DataValueField = "WorkType_Code_M";
            ddlWorkType.DataSource = dsDCR;
            ddlWorkType.DataBind();
            FillColor();
        }
    }
    private void FillColor()
    {
        int j = 0;
        foreach (ListItem ColorItems in ddlWorkType.Items)
        {
            DCR_New WT = new DCR_New();
            dsWT = WT.DCR_get_WorkType(div_code, ColorItems.Value.ToString(),sf_type);
            if (dsWT.Tables[0].Rows.Count > 0)
            {
                FieldWork_Ind = dsWT.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (FieldWork_Ind == "F")
                    ddlWorkType.Items[j].Attributes.Add("style", "background-color: Yellow");
            }
            j = j + 1;
        }
    }
    private void FillTerrHQ()
    {
        DCR_New dc = new DCR_New();
        // Added by Sridevi for Audit Manager
        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();
        // Check if the manager has a team
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {
            dsDCR = dc.getTerrHQ_DCR(sf_code);
        }
        else
        {
            // Fetch Managers Audit Team
            DataTable dt = ds.getAuditManagerTeam_GetMR_DCR(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsDCR = dsmgrsf;
        }
     
     //   dsDCR = dc.getTerrHQ_DCR(sf_code);
        if (dsDCR.Tables[0].Rows.Count > 0)
        {
            ddlTerrHQ.DataTextField = "Sf_Name";
            ddlTerrHQ.DataValueField = "Sf_Code";
            ddlTerrHQ.DataSource = dsDCR;
            ddlTerrHQ.DataBind();
            FillTerrColor();
        }
    }
    private void FillTerrColor()
    {
        int j = 0;
        foreach (ListItem ColorItems in ddlTerrHQ.Items)
        {
            DCR_New dc = new DCR_New();
            string sta = string.Empty;

            dsWT = dc.getSfStatus(ColorItems.Value.ToString());
            if (dsWT.Tables[0].Rows.Count > 0)
            {
                sta = dsWT.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (sta == "1")
                {
                    ddlTerrHQ.Items[j].Attributes.Add("style", "background-color: Red");
                }
            }
            j = j + 1;
        }
    }

    private void FillSDP_DCU_Count()
    {
        int j = 0;
        foreach (ListItem ColorItems in ddlSDP.Items)
        {
            DCR_New dc = new DCR_New();
            string doc_cnt = string.Empty;
            string che_cnt = string.Empty;
            string un_cnt = string.Empty;

            dsWT = dc.getDocCount(ddlTerrHQ.SelectedValue.ToString(), ColorItems.Value.ToString());

            if (dsWT.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt16(dsWT.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) > 0)
                {
                    doc_cnt = dsWT.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                else
                {
                    doc_cnt = "0";
                }
            }

            dsWT = dc.getCheCount(ddlTerrHQ.SelectedValue.ToString(), ColorItems.Value.ToString());

            if (dsWT.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt16(dsWT.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) > 0)
                {
                    che_cnt = dsWT.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                else
                {
                    che_cnt = "0";
                }
            }
            dsWT = dc.getUnCount(ddlTerrHQ.SelectedValue.ToString(), ColorItems.Value.ToString());

            if (dsWT.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt16(dsWT.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) > 0)
                {
                    un_cnt = dsWT.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                else
                {
                    un_cnt = "0";
                }
            }
            if (j == 0)
            {
                ddlSDP.Items[j].Text = ddlSDP.Items[j].Text + " [ D ]" + " [ C ]" + " [ U ]";
            }
            else
            {
                ddlSDP.Items[j].Text = ddlSDP.Items[j].Text + " [ " + doc_cnt + " ]" + " [ " + che_cnt + " ]" + " [ " + un_cnt + " ]";
            }

            j = j + 1;
        }
    }

    protected void ddlWorkType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWorkType.SelectedIndex > 0)
        {
            DCR_New WT = new DCR_New();
            dsWT = WT.DCR_get_WorkType(div_code, ddlWorkType.SelectedValue.ToString(),sf_type);
            if (dsWT.Tables[0].Rows.Count > 0)
            {
                FieldWork_Ind = dsWT.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                ButtonAccess = dsWT.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            }
            
            if (FieldWork_Ind != "F")
            {
                if((FieldWork_Ind == "N") || (FieldWork_Ind == "H") || (FieldWork_Ind == "W"))
                {
                    PnlRemarks.Visible = true;
                    //btnSave.Visible = true;
                    btnSubmit.Visible = true;
                    lblInfo.Visible = false;
                    lblTerrHQ.Visible = false;
                    ddlTerrHQ.Visible = false;
                    ddlSDP.Visible = false;
                    lblSDP.Visible = false;
                    pnlTree.Visible = false;
                    btnGo.Visible = false;
                    btnFieldWork.Visible = false;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + NewbtnClear.ClientID + "') .style.display='block';", true);
                }
                else if (FieldWork_Ind == "L")
                {
                    Response.Redirect("~/MasterFiles/MGR/Leave_Form_Mgr.aspx?LeaveFrom=" + lblCurDate.Text);
                }
            }
            else
            {
                PnlRemarks.Visible = false;
                btnSave.Visible = false;
                btnSubmit.Visible = false;
                pnlTerr.Visible = true;
                lblTerrHQ.Visible = true;
                ddlTerrHQ.Visible = true;
                lblInfo.Visible = true;
                lblInfo.Text = "Select the HQ";
            }
            
            //Creating Header

            sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Header.xml";
            dshead = (DataSet)ViewState["Header"];

            if (dshead != null && dshead.HasChanges())
            {
                if (dshead.Tables[0].Rows.Count > 0)
                {
                    dshead.Tables[0].Rows[0]["worktype"] = ddlWorkType.SelectedValue.ToString();
                    dshead.Tables[0].Rows[0]["sdp"] = "0";
                    dshead.Tables[0].Rows[0]["remarks"] = "";
                    dshead.Tables[0].Rows[0]["date"] = DateTime.Now.ToString();
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
                xmlsdp.InnerText = "0";
                xmlrem.InnerText = "";
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
            ViewState["heading"] = "(Re-Entry For Rejection)";
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
                ViewState["heading"] = "(Edit)";
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
                    ViewState["heading"] = "(Delay - Release)";
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
                        ViewState["heading"] = "(Missed - Date)";
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

    private void AutoCreateDCR(string autocreatedcrdate, string sWorkType, string dcrdate,bool isAutopost,string Work_Type_Name)
    {
        // Create DCR only with Header records and no detail records
        DCR_New dc1 = new DCR_New();
        dsMgr = dc1.getsf_dtls(sf_code, div_code);
        bool isrejedit = false;
        bool isdelay = false;
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

        iReturn = dc1.RecordAdd_Header(sf_code, sf_name, emp_id, employee_id, autocreatedcrdate, sWorkType, "0", "", txtRemarkDesc.Text, "0", dcrdate, isrejedit, isdelay, "1", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), Work_Type_Name, sf_type, IPAdd, EntryMode);
        int iReturn_upd = dc1.Update_Header(sf_code, iReturn, isrejedit, dcrdate, div_code, autocreatedcrdate, isdelay);
        if ((iAppNeed == 0) || (isAutopost == true))
        {
            int iretmain = dc1.Create_DCRHead_Trans(sf_code, iReturn);
        }

    }

    private void AutoCreateDCR_Leave(string autocreatedcrdate, string sWorkType, string dcrdate, bool isAutopost, string Work_Type_Name)
    {
        // Create DCR only with Header records and no detail records
        DCR_New dc1 = new DCR_New();
        dsMgr = dc1.getsf_dtls(sf_code, div_code);
        bool isrejedit = false;
        bool isdelay = false;
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

        iReturn = dc1.RecordAdd_Header(sf_code, sf_name, emp_id, employee_id, autocreatedcrdate, sWorkType, "0", "", "", "0", dcrdate, isrejedit, isdelay, "1", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), Work_Type_Name, sf_type, IPAdd, EntryMode);
        int iReturn_upd = dc1.Update_Header(sf_code, iReturn, isrejedit, dcrdate, div_code, autocreatedcrdate, isdelay);
        if (isAutopost == true)
        {
            int iretmain = dc1.Create_DCRHead_Trans(sf_code, iReturn);
        }

    }

    protected void ddlTerrHQ_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTerrHQ.SelectedIndex > 0)
        {
            isHQ = "HQ";
            ddlWorkType.Enabled = false;
            lblSDP.Visible = true;
            ddlSDP.Visible = true;
            ddlSDP.Enabled = true;
            FillSDP(ddlTerrHQ.SelectedValue.ToString());
            FillSDP_DCU_Count();


            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                lblInfo.Text = "Select the " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
            lblInfo.Visible = true;

            parentnode = "<div style='color:#DF0174;'>&nbsp;&nbsp;<b>" + ddlTerrHQ.SelectedItem.Text + "</b> &nbsp;</div>";
            // bindgrid(parentnode, ddlTerrHQ.SelectedValue.ToString(), isHQ);
            // Creates an XML for Listed Doctor

            //sFile = sf_code + sCurDate + "ListedDR.xml";
            sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_WorkArea.xml";

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Server.MapPath(sFile));

            XmlElement parentelement = xmldoc.CreateElement("DCR");


            XmlElement xmlnode = xmldoc.CreateElement("node");
            XmlElement xmlhq = xmldoc.CreateElement("hq");
            XmlElement xmlishq = xmldoc.CreateElement("ishq");

            XmlElement xmlsdp = xmldoc.CreateElement("sdp");
            XmlElement xmlcolor = xmldoc.CreateElement("color");


            xmlnode.InnerText = parentnode;
            xmlhq.InnerText = ddlTerrHQ.SelectedValue.ToString();
            xmlishq.InnerText = isHQ;

            xmlsdp.InnerText = ddlSDP.SelectedValue.ToString();
            xmlcolor.InnerText = "";


            parentelement.AppendChild(xmlnode);
            parentelement.AppendChild(xmlhq);
            parentelement.AppendChild(xmlishq);
            parentelement.AppendChild(xmlsdp);
            parentelement.AppendChild(xmlcolor);

            xmldoc.DocumentElement.AppendChild(parentelement);
            //xmldoc.Save(Server.MapPath("DailCalls.xml"));
            xmldoc.Save(Server.MapPath(sFile));

            //BindGrid();
        }
    }
    protected void ddlSDP_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (ddlSDP.SelectedIndex > 0)
        {
          
            isHQ = "";
            ViewState["ChildNode"] = ddlSDP.SelectedItem.Text;
            pnlTree.Visible = true;
            lblInfo.Visible = false;

            childnode = "<div style='color:#0101DF;'><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ddlSDP.SelectedItem.Text + "</b>&nbsp;</div>";
            // bindgrid(childnode, ddlTerrHQ.SelectedValue.ToString(),isHQ);
            btnGo.Visible = true;
            btnFieldWork.Visible = true;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + NewbtnClear.ClientID + "') .style.display='block';", true);

            // Creates an XML for Listed Doctor

            //sFile = sf_code + sCurDate + "ListedDR.xml";
            sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_WorkArea.xml";

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Server.MapPath(sFile));

            XmlElement parentelement = xmldoc.CreateElement("DCR");

               
            XmlElement xmlnode = xmldoc.CreateElement("node");
            XmlElement xmlhq = xmldoc.CreateElement("hq");
            XmlElement xmlishq = xmldoc.CreateElement("ishq");
            XmlElement xmlsdp = xmldoc.CreateElement("sdp");
            XmlElement xmlcolor = xmldoc.CreateElement("color");

              
            xmlnode.InnerText = childnode;
            xmlhq.InnerText = ddlTerrHQ.SelectedValue.ToString();
            xmlishq.InnerText = isHQ;
            xmlsdp.InnerText = ddlSDP.SelectedValue.ToString();
            string str = GetCaseColor(Convert.ToString(grvWorkArea.Rows.Count));
            xmlcolor.InnerText = str;

             
            parentelement.AppendChild(xmlnode);
            parentelement.AppendChild(xmlhq);
            parentelement.AppendChild(xmlishq);
            parentelement.AppendChild(xmlsdp);
            parentelement.AppendChild(xmlcolor);

            xmldoc.DocumentElement.AppendChild(parentelement);
            //xmldoc.Save(Server.MapPath("DailCalls.xml"));
            xmldoc.Save(Server.MapPath(sFile));

            BindGrid();
        }
        

    }
    protected void grvWorkArea_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblcol = (Label)e.Row.FindControl("lblcol");
            TextBox txtColor = (TextBox)e.Row.FindControl("txtColor");
            txtColor.BackColor = System.Drawing.Color.FromName(lblcol.Text);
        }
        
    }

   
    protected void grvWorkArea_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindGrid();
        DataSet dt = grvWorkArea.DataSource as DataSet;
        //   dt.Tables[0].Rows[grvWorkArea.Rows[e.RowIndex].DataItemIndex].Delete();
        int rowIndex = Convert.ToInt32(e.RowIndex);
        if (dt.Tables[0].Rows.Count > 0)
        {
            if (dt.Tables[0].Rows[rowIndex]["isHQ"].ToString() == "HQ")
            {
                int cnt = 0;
                for (int i = rowIndex; i < dt.Tables[0].Rows.Count; i++)
                {
                    if (dt.Tables[0].Rows[i]["HQ"].ToString() == dt.Tables[0].Rows[rowIndex]["HQ"].ToString())
                        cnt = cnt + 1;
                }

                for (int i = 0; i < cnt; i++)
                {
                    if (dt.Tables[0].Rows[rowIndex]["HQ"].ToString() == dt.Tables[0].Rows[rowIndex]["HQ"].ToString())
                    {
                        dt.Tables[0].Rows.Remove(dt.Tables[0].Rows[rowIndex]);
                    }
                }
            }
            else
            {
                dt.Tables[0].Rows.Remove(dt.Tables[0].Rows[rowIndex]);
            }
            grvWorkArea.DataSource = dt;
            grvWorkArea.DataBind();
        }
        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_WorkArea.xml";
        dt.WriteXml(Server.MapPath(sFile));
        BindGrid();

      
    }


   
    private void ClearXML()
    {

        //Delete the Listed Doctor XML file
        //string FilePath = Server.MapPath("DailCalls.xml");
        //sFile = sf_code + sCurDate + "ListedDR.xml";

        string sFileHeader = sf_code + "_" + ViewState["curdate"].ToString() + "_Header.xml";
        string headerFilePath = Server.MapPath(sFileHeader);
        if (File.Exists(headerFilePath))
            File.Delete(headerFilePath);

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_WorkArea.xml";
        string FilePath = Server.MapPath(sFile);
        if (File.Exists(FilePath))
            File.Delete(FilePath);

       
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

        txtRemarkDesc.Text = "";
        Bind_Header();
        BindGrid();
       
    }
    private void removexml()
    {
        //Delete the Listed Doctor XML file
        //string FilePath = Server.MapPath("DailCalls.xml");
        //sFile = sf_code + sCurDate + "ListedDR.xml";

        string sFileHeader = sf_code + "_" + ViewState["curdate"].ToString() + "_Header.xml";
        string strHead = AppDomain.CurrentDomain.BaseDirectory + "MasterFiles\\MR\\DCR\\" + sFileHeader;
        if (File.Exists(strHead))
            File.Delete(strHead);
        //string headerFilePath = Server.MapPath(sFileHeader);
        //if (File.Exists(headerFilePath))
        //    File.Delete(headerFilePath);

        sFile = sf_code + "_" + ViewState["curdate"].ToString() + "_ListedDR.xml";
        string strLdr = AppDomain.CurrentDomain.BaseDirectory + "MasterFiles\\MR\\DCR\\" + sFile;
        if (File.Exists(strLdr))
            File.Delete(strLdr);
        //string FilePath = Server.MapPath(sFile);
        //if (File.Exists(FilePath))
        //    File.Delete(FilePath);

        //Delete the Chemists XML file
        //FilePath = Server.MapPath("Chem_DCR.xml");
        string sChemFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Chem.xml";
        string strChem = AppDomain.CurrentDomain.BaseDirectory + "MasterFiles\\MR\\DCR\\" + sChemFile;
        if (File.Exists(strChem))
            File.Delete(strChem);
        //string chemFilePath = Server.MapPath(sChemFile);
        //if (File.Exists(chemFilePath))
        //    File.Delete(chemFilePath);

        //Delete the Stockiest XML file
        string sStockFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Stockiest.xml";
        //string stockFilePath = Server.MapPath(sStockFile);
        //if (File.Exists(stockFilePath))
        //    File.Delete(stockFilePath);
        string strStk = AppDomain.CurrentDomain.BaseDirectory + "MasterFiles\\MR\\DCR\\" + sStockFile;
        if (File.Exists(strStk))
            File.Delete(strStk);

        //Delete the Hospital XML file
        string sHosFile = sf_code + "_" + ViewState["curdate"].ToString() + "_Hospital.xml";
        //string hosFilePath = Server.MapPath(sHosFile);
        //if (File.Exists(hosFilePath))
        //    File.Delete(hosFilePath);
        string strHos = AppDomain.CurrentDomain.BaseDirectory + "MasterFiles\\MR\\DCR\\" + sHosFile;
        if (File.Exists(strHos))
            File.Delete(strHos);

        //Delete the Un-:isted XML file                
        string sUnLstFile = sf_code + "_" + ViewState["curdate"].ToString() + "UnLstDR.xml";
        //string unlstFilePath = Server.MapPath(sUnLstFile);
        //if (File.Exists(unlstFilePath))
        //    File.Delete(unlstFilePath);
        string strUnlst = AppDomain.CurrentDomain.BaseDirectory + "MasterFiles\\MR\\DCR\\" + sUnLstFile;
        if (File.Exists(strUnlst))
            File.Delete(strUnlst);
    }
    protected void NewbtnClear_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        ClearXML();
        removexml();
        Loaddcr("0");
        lblInfo.Visible = true;
        lblInfo.Text = "Select the WorkType   ...";
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {

        DCR_New dv = new DCR_New();
        int iRet = dv.RecordDelMGRWorkArea(sf_code, lblCurDate.Text);
        int cnt = 0;
        foreach (GridViewRow gridRow in grvWorkArea.Rows)
        {
            Label lblhq = (Label)gridRow.Cells[1].FindControl("lblHQ");
            Label lblsdp = (Label)gridRow.Cells[3].FindControl("lblsdp");
            Label lblcol = (Label)gridRow.Cells[4].FindControl("lblcol");
            cnt = cnt + 1;
            // Create MGR Work Area Details   
            if (lblsdp.Text != "0")
            {

                int iReturn = dv.RecordInsertMGRWorkArea(sf_code, lblhq.Text, lblsdp.Text, lblcol.Text, lblCurDate.Text, ddlWorkType.SelectedValue.ToString());
            }
        }
        if (cnt > 0)
        {
            string heading = string.Empty;
            heading = lblReject.Text;
            Response.Redirect("~/MasterFiles/MR/DCR/DCR_New.aspx?scurdate=" + ViewState["scurdate"].ToString() + "&head=" + ViewState["heading"].ToString());
        }
        else
        {
            ddlTerrHQ.SelectedValue = "0";
            ddlSDP.Visible = false;
            lblSDP.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Select atleast one HQ');", true);
        }
    }
    protected void btnFieldWork_Click(object sender, EventArgs e)
    {
        DCR_New dv = new DCR_New();
        int iRet = dv.RecordDelMGRWorkArea(sf_code, lblCurDate.Text);
        int cnt = 0;
        foreach (GridViewRow gridRow in grvWorkArea.Rows)
        {
            Label lblhq = (Label)gridRow.Cells[1].FindControl("lblHQ");
            Label lblsdp = (Label)gridRow.Cells[3].FindControl("lblsdp");
            Label lblcol = (Label)gridRow.Cells[4].FindControl("lblcol");
            cnt = cnt + 1;
            // Create MGR Work Area Details   
            if (lblsdp.Text != "0")
            {

                int iReturn = dv.RecordInsertMGRWorkArea(sf_code, lblhq.Text, lblsdp.Text, lblcol.Text, lblCurDate.Text, ddlWorkType.SelectedValue.ToString());
            }
        }
        if (cnt > 0)
        {
            string heading = string.Empty;
            heading = lblReject.Text;
            Response.Redirect("~/MasterFiles/MR/DCR/DCR_New.aspx?scurdate=" + ViewState["scurdate"].ToString() + "&head=" + ViewState["heading"].ToString());
        }
        else
        {
            ddlTerrHQ.SelectedValue = "0";
            ddlSDP.Visible = false;
            lblSDP.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Select atleast one HQ');", true);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
         Response.Redirect("~/MGR_Home.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        dtDCR = Convert.ToDateTime( ViewState["scurdate"].ToString());
        lblCurDate.Text = dtDCR.ToString("MM/dd/yyyy");
        Bind_Header();
        AutoCreateDCR(lblCurDate.Text, ddlWorkType.SelectedValue.ToString(), dtDCR.ToString("dd/MM/yyyy"), false, ddlWorkType.SelectedItem.Text.ToString());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('DCR Saved successfully!');", true);
       // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DCR Saved successfully!');</script>");
        Loaddcr("0");
    }
    protected void btnSubmit_Click(object sender, EventArgs e) 
    {
        dtDCR = Convert.ToDateTime(ViewState["scurdate"].ToString());
        lblCurDate.Text = dtDCR.ToString("MM/dd/yyyy");
        Bind_Header();
        AutoCreateDCR(lblCurDate.Text,ddlWorkType.SelectedValue.ToString(), dtDCR.ToString("dd/MM/yyyy"),false, ddlWorkType.SelectedItem.Text.ToString());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('DCR Submitted successfully!');", true);
        lblInfo.Visible = true;
        lblInfo.Text = "Select the WorkType   ...";
       // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DCR Submitted successfully!');</script>");
        Loaddcr("0");
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBlock", "document.getElementById('" + NewbtnClear.ClientID + "') .style.display='none';", true);
        //NewbtnClear.Attributes.Add("style", "display:none");
    }
}