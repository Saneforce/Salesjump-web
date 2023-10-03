using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_SalesForce : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsSubDivision = null;
    DataSet dsSF = null;
    DataSet dstype = null;
    DataSet dsState = null;
    DataSet dsSall = null;
    string ddlTerritory = string.Empty;
    string sState = string.Empty;
    string strSfCode = string.Empty;
    string div_code = string.Empty;
    string sDesSName = string.Empty;
    string division_code = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string sfcode = string.Empty;
    string sf_hq = string.Empty;
    string sfreason = string.Empty;
    DataSet dsSalesForce = null;
    string subdivision_code = string.Empty;
    string sf_type = string.Empty;
    string sfname = string.Empty;
    string sub_division = string.Empty;
    string reporting_to = string.Empty;
    bool isManager = false;
    bool isCreate = true;
    string divvalue = string.Empty;
    int iIndex = -1;
    string sChkLocation = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string usrname = string.Empty;
    string desname = string.Empty;
    string state = string.Empty;
    string Reporting_To_SF = string.Empty;
    string state_code = string.Empty;
    string type = string.Empty;
    string Effective_Date = string.Empty;
    string joiningdate = string.Empty;
    string UserDefin = string.Empty;
    string Sf_emp_id = string.Empty;
    string Vac_sfcode = string.Empty;
    string from_division = string.Empty;
    string from_designation = string.Empty;
    string Designation_Name = string.Empty;
    string Promote_Mode = string.Empty;
    string terr = string.Empty;
    string terri = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "SalesForceList.aspx";
        sfcode = Request.QueryString["sfcode"];
        sfreason = Request.QueryString["sfreason"];
        sf_hq = Request.QueryString["sf_hq"];
        sf_type = Request.QueryString["sf_type"];
        sfname = Request.QueryString["sfname"];
        usrname = Request.QueryString["sfusername"];
        desname = Request.QueryString["desgname"];
        state = Request.QueryString["state"];
        Reporting_To_SF = Request.QueryString["Reporting_To_SF"];
        state_code = Request.QueryString["state_code"];
        Designation_Name = Request.QueryString["Designation_Name"];
        Promote_Mode = Request.QueryString["Promote_Mode"];

        div_code = Session["div_code"].ToString();

        txtUserName.Enabled = false;
        RblSta.Enabled = false;
        if (!Page.IsPostBack)
        {
            //menu1.Title = "FieldForce Master";
            FillState(div_code);
            //CalendarExtender5.SelectedDate = DateTime.Today;
            txtcnfdate.Text = "";
            FillType();
            ViewState["Reporting_To"] = "";
            ViewState["Rep_StartDate"] = "";
            //menu1.FindControl("btnBack").Visible = true;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getDesignation_div(div_code);
            txtDesignation.DataTextField = "Name";
            txtDesignation.DataValueField = "Designation_Code";
            txtDesignation.DataSource = dsSalesForce;
            txtDesignation.DataBind();
            Username();
            if ((sfcode != "") && (sfcode != null))
            {
                LoadData(sfcode);
                isCreate = false;
                ddlFieldForceType.Enabled = false;
                //ddlTerritoryName.Enabled = false;
                //ddlState.Enabled = false;
                //txtDesignation.Enabled = false;
                getLastDCR();
                lblLastDCRDate.Visible = true;
                lblLastDCRDate.Style.Add("Color", "Red");
                txtDCRDate.Visible = true;

            }
            if ((state != "") && (state != null))
            {
                RblSta.SelectedValue = "1";
                ddlFieldForceType.Enabled = false;
                txtTPDCRStartDate.Enabled = false;
            }
            if ((sfname != "") && (sfname != null))
            {

                getLastDCR();
                RblSta.SelectedValue = "0";
                btnSubmit.Text = "Activate";
                btnSave.Text = "Activate";
                ddlFieldForceType.Enabled = false;
                lblLastDCRDate.Visible = true;
                txtDCRDate.Visible = true;

                rblVacantBlock.Visible = true;
                lblVacantBlock.Visible = true;
                txtJoingingDate.Text = "";
                txtTPDCRStartDate.Text = "";
                lblJoingingDate.Style.Add("Color", "Magenta");
                lblTPDCRStartDate.Style.Add("Color", "Magenta");
                lblJoingingDate.Font.Bold = true;
                lblTPDCRStartDate.Font.Bold = true;
            }

            if ((sf_hq != "") && (sf_hq != null) || (Reporting_To_SF != "") && (Reporting_To_SF != null)) // To Vacant
            {

                Disableall();
                getLastDCR();
                RblSta.SelectedValue = "1";
                lblLastDCRDate.Visible = true;
                lblLastDCRDate.Style.Add("Color", "Red");
                lblTPDCRStartDate.Text = "*Resigned Date";
                lblTPDCRStartDate.Style.Add("Color", "Red");
                rblVacantBlock.Visible = true;
                lblVacantBlock.Visible = true;
                lblVacantBlock.Style.Add("Color", "Red");
                txtDCRDate.Visible = true;

                btnSubmit.Text = "Click here to make vacant";
                btnSubmit.Style.Add("width", "175px");
                btnSave.Text = "Click here to make vacant";
                btnSave.Style.Add("width", "175px");
                btnSubmit.Attributes.Add("OnClick", "javaScript: return Vacant();");

                ViewState["to_vacant"] = "1";

                lblVacantBlock.Text = "Status Reason";
                lbleffective.Visible = true;
                txteffe.Visible = true;
                txtTPDCRStartDate.Text = txtDCRDate.Text;

            }

            if ((usrname != "") && (usrname != null) || (desname != "") && (desname != null))
            {

                Disableall();

                SalesForce sal = new SalesForce();
                dsSalesForce = sal.getLastDCR(sfcode);
                if (dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "")
                {

                    DateTime ldcrdate = Convert.ToDateTime(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    txteffe.Text = ldcrdate.ToString("dd/MM/yyyy");
                }

                txteffe.Enabled = false;
                txtTPDCRStartDate.Enabled = false;

                lblReporting.Style.Add("Color", "Red");
                ddlReporting.Enabled = true;
                lblDesignation.Style.Add("Color", "Red");
                txtDesignation.Focus();
                txtDesignation.Enabled = true;
                lbleffective.Visible = true;
                txteffe.Visible = true;

                if ((usrname != "") && (usrname != null))
                {
                    txtPassword.Enabled = true;
                    btnSubmit.Text = "Promote";
                    btnSave.Text = "Promote";
                    btnSubmit.Attributes.Add("OnClick", "javaScript: return Promote();");

                    Session["backurl"] = "Salesforce_Promo_DePromo.aspx";
                    ViewState["promote"] = "1";
                }
                if ((desname != "") && (desname != null))
                {
                    txtPassword.Enabled = true;
                    btnSubmit.Text = "De-Promote";
                    btnSave.Text = "De-Promote";
                    btnSubmit.Attributes.Add("OnClick", "javaScript: return DePromote();");

                    Session["backurl"] = "Salesforce_Promo_DePromo.aspx";
                    ViewState["depromote"] = "1";
                }

            }
            if ((sf_type != "") && (sf_type != null))
            {

                Disableall();
                RblSta.SelectedValue = "2";
                lblReason.Visible = true;
                lblReason.Style.Add("Color", "Magenta");
                txtReason.Visible = true;
                txtReason.Focus();
                btnSubmit.Text = "Click here to Block";
                btnSubmit.Style.Add("width", "175px");
                btnSave.Text = "Click here to Block";
                btnSave.Style.Add("width", "175px");
                txtReason.Focus();
                btnSubmit.Attributes.Add("OnClick", "javaScript: return Block();");
                txtTPDCRStartDate.Enabled = false;
            }
            if (sfreason != "" && sfreason != null)
            {
                Disableall();
                txtReason.Enabled = false;
                txtReason.Visible = true;
                lblReason.Visible = true;
                lblReason.Style.Add("Color", "Magenta");
                btnSubmit.Text = "Activate";
                btnSave.Text = "Activate";
            }


            if (Promote_Mode == "1")
            {
                //menu1.Title = "Base Level to Manager Promotion";
                Vac_sfcode = Session["Vac_sfcode"].ToString();
                ViewState["Vac_sfcode"] = Vac_sfcode;
                SalesForce sal = new SalesForce();
                dsSall = sal.getVac_info(Vac_sfcode);
                if (dsSall.Tables[0].Rows.Count > 0)
                {
                    txtDOB.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtDOW.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtMobile.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    txtEMail.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    txtAddress1.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    txtAddress2.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    txtCityPin.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    txtPerAddress1.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    txtPerAddress2.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    txtPerCityPin.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                    txtPhone.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                    txtPassword.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();

                    string strPassword;
                    strPassword = txtPassword.Text;
                    txtPassword.Attributes.Add("value", strPassword);

                    from_division = dsSall.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                    ViewState["from_division"] = from_division;
                    from_designation = dsSall.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                    ddlStatus.SelectedValue = dsSall.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();

                    ViewState["from_designation"] = from_designation;
                }

                txtFieldForceName.Text = sfname;

                txtJoingingDate.Text = Session["joiningdate"].ToString();

                lbluserdefi.Visible = true;
                lbluserdefi.Text = Session["UserDefin"].ToString();

                lbluserdefi.Font.Bold = true;
                lbluserdefi.Style.Add("Color", "Red");
                txtEmployeeID.Text = Session["Sf_emp_id"].ToString();

                txtTPDCRStartDate.Text = Session["Effiective_Date"].ToString();
                lbloldusername.Visible = true;
                RblSta.Visible = false;
                Label7.Visible = false;


            }
            else if (Promote_Mode == "0")
            {
                //menu1.Title = "Base Level to Manager Promotion";

                Vac_sfcode = Session["Vac_sfcode"].ToString();
                ViewState["Vac_sfcode"] = Vac_sfcode;
                SalesForce sal = new SalesForce();
                dsSall = sal.getVac_info(Vac_sfcode);
                if (dsSall.Tables[0].Rows.Count > 0)
                {
                    txtDOB.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtDOW.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtMobile.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    txtEMail.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    txtAddress1.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    txtAddress2.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    txtCityPin.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    txtPerAddress1.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    txtPerAddress2.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    txtPerCityPin.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                    txtPhone.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                    txtPassword.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();


                    string strPassword;
                    strPassword = txtPassword.Text;
                    txtPassword.Attributes.Add("value", strPassword);

                    from_division = dsSall.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                    ViewState["from_division"] = from_division;
                    from_designation = dsSall.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                    ViewState["from_designation"] = from_designation;
                    ddlState.SelectedValue = dsSall.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                    txtHQ.Text = dsSall.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                    ddlReporting.SelectedValue = dsSall.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                    subdivision_code = dsSall.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                    ddlStatus.SelectedValue = dsSall.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                    txtDesignation.Enabled = true;


                }


                txtFieldForceName.Text = Session["Fieldforce_Name"].ToString();


                ddlFieldForceType.SelectedValue = "2";
                ddlFieldForceType.Enabled = false;
                txtJoingingDate.Text = Session["joiningdate"].ToString();

                lbluserdefi.Visible = true;
                lbluserdefi.Text = Session["UserDefin"].ToString();

                lbluserdefi.Font.Bold = true;
                lbluserdefi.Style.Add("Color", "Red");
                txtEmployeeID.Text = Session["Sf_emp_id"].ToString();

                ddlFieldForceType.Enabled = false;
                txtTPDCRStartDate.Text = Session["Effiective_Date"].ToString();
                lbloldusername.Visible = true;
                lblTPDCRStartDate.Style.Add("Color", "Magenta");
                lblTPDCRStartDate.Font.Bold = true;
                lblDesignation.Style.Add("Color", "Magenta");
                lblDesignation.Font.Bold = true;

                loaddes();
            }

            if (state_code != "" && state_code != null)
            {
                Disableall();
                txtJoingingDate.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                txtTPDCRStartDate.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                txtTPDCRStartDate.Enabled = false;
                UsrDfd_UserName.Enabled = false;
                lblJoingingDate.Style.Add("Color", "Black");
                lblTPDCRStartDate.Style.Add("Color", "Black");
                lblJoingingDate.Font.Bold = false;
                lblTPDCRStartDate.Font.Bold = false;
                txtPassword.Enabled = true;
            }

            if ((Designation_Name != "") && (Designation_Name != null))
            {
                //menu1.Title = "Base Level to Manager Promotion";

                btnSubmit.Text = "Click here to Promote";
                btnSave.Text = "Click here to Promote";
                btnSubmit.Attributes.Add("OnClick", "javaScript: return BaseLevelPromote();");
                lblmode.Visible = true;
                rdoMode.Visible = true;
                UsrDfd_UserName.Enabled = false;
                lbleffective.Text = "Promoted Effecitve Date";
                lbleffective.Style.Add("Color", "Red");

            }

            if (ddlState.SelectedIndex == 0)
            {
                string strtxtUser = txtUserName.Text.Remove(txtUserName.Text.Length - 1, 1);
                txtUserName.Text = strtxtUser;

            }

            FillCheckBoxList();
            FillCheckBoxList1();


        }
    }
    private void getLastDCR()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getLastDCR(sfcode);
        if (dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "")
        {
            DateTime ldcrdate = Convert.ToDateTime(dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            ldcrdate = ldcrdate.AddDays(-1);

            txtDCRDate.Text = ldcrdate.ToString("dd/MM/yyyy");
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

    private void FillCheckBoxList1()
    {
        //List of States are loaded into the checkbox list from Division Class
        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubDiv(div_code);
        chkboxLocation.DataTextField = "subdivision_name";
        chkboxLocation.DataSource = dsSubDivision;
        chkboxLocation.DataBind();
        string[] subdiv;
        if (subdivision_code != "")
        {
            iIndex = -1;
            subdiv = subdivision_code.Split(',');
            foreach (string st in subdiv)
            {
                for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
                {
                    if (st == chkboxLocation.Items[iIndex].Value)
                    {
                        chkboxLocation.Items[iIndex].Selected = true;
                        chkboxLocation.Items[iIndex].Attributes.Add("style", "Color: #8A2BE2;font-weight:Bold");
                    }
                }
            }
        }
    }

    private void Disableall()
    {

        txtAddress1.Enabled = false;
        txtAddress2.Enabled = false;
        txtCityPin.Enabled = false;
        txtDOB.Enabled = false;
        txtDOW.Enabled = false;
        RblSta.Enabled = false;
        txtEMail.Enabled = false;
        txtEmployeeID.Enabled = false;
        txtFieldForceName.Enabled = false;
        txtHQ.Enabled = false;
        txtJoingingDate.Enabled = false;
        txtMobile.Enabled = false;
        txtPassword.Enabled = false;
        txtPerAddress1.Enabled = false;
        txtPerAddress2.Enabled = false;
        txtPerCityPin.Enabled = false;
        txtPhone.Enabled = false;
        txtPhone1.Enabled = false;
        txtPhone2.Enabled = false;

        txtUserName.Enabled = false;
        ddlFieldForceType.Enabled = false;
        ddlReporting.Enabled = false;
        txtDesignation.Enabled = true;

        //ddlState.Enabled = false;
        chkboxLocation.Enabled = false;


    }

    private void LoadData(string sfcode)
    {
        SalesForce sf = new SalesForce();
        dsSF = sf.getSalesForce(sfcode);
        if (dsSF.Tables[0].Rows.Count > 0)
        {
            txtFieldForceName.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(); //sf_name 
            txtUserName.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(); //Sf_UserName 
            ViewState["usernamecheck"] = txtUserName.Text;
            txtPassword.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(2).ToString(); //Sf_Password  

            string strPassword;
            strPassword = txtPassword.Text;
            txtPassword.Attributes.Add("value", strPassword);
            ddlTerritoryName.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(35).ToString();
            ddlTerritoryName.Enabled = false;
            ddlState.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(6).ToString(); //state_code 
            ViewState["statecheck"] = ddlState.SelectedValue;
            txtAddress1.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(8).ToString(); // sf_contact_address1 
            txtAddress2.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(9).ToString(); // sf_contact_address2 
            txtCityPin.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(10).ToString(); // sf_contact_citypin 
            txtEMail.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(11).ToString(); // sf_email 
            txtMobile.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(12).ToString(); // sf_mobile 
            txtPerAddress1.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(14).ToString(); // sf_perm_address1 
            txtPerAddress2.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(15).ToString(); // sf_perm_address2 
            txtPerCityPin.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(16).ToString(); // sf_perm_citypin 
            txtPhone.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(17).ToString(); // sf_perm_contact 
            txtHQ.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(19).ToString(); // sf_hq 
            txtJoingingDate.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(3).ToString(); // Joining Date 
            txtTPDCRStartDate.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(7).ToString(); // TP DCR Date
            ViewState["Rep_StartDate"] = txtTPDCRStartDate.Text;
            txtDCRDate.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(23).ToString();
            txtDOB.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(13).ToString(); // DOB
            txtDOW.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(32).ToString(); // DOW
            ddlReporting.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(4).ToString(); //Reporting To 
            ViewState["Reporting_To"] = ddlReporting.SelectedValue;
            txtEmployeeID.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(25).ToString(); // DOB
            //txtShortName.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(28).ToString(); // DOB
            ddlFieldForceType.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(28).ToString(); // DOB
            loaddes();
            txtDesignation.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(27).ToString(); // DOB
            ViewState["checkdesig"] = txtDesignation.SelectedValue;
            ViewState["cur_des"] = txtDesignation.SelectedValue.ToString();

            subdivision_code = dsSF.Tables[0].Rows[0].ItemArray.GetValue(29).ToString(); // Sub Division
            rblVacantBlock.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(30).ToString();
            txtReason.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(31).ToString();
            division_code = dsSF.Tables[0].Rows[0].ItemArray.GetValue(22).ToString();
            UsrDfd_UserName.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(33).ToString();
            FFType.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(34) == DBNull.Value ? "1" : dsSF.Tables[0].Rows[0].ItemArray.GetValue(34).ToString();
            terri = dsSF.Tables[0].Rows[0].ItemArray.GetValue(35).ToString();
            ddlStatus.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(36).ToString();
            txtcnfdate.Text = dsSF.Tables[0].Rows[0].ItemArray.GetValue(37).ToString();


            if (FFType.SelectedValue.ToString() == "2")
            {
                txtcnfdate.Visible = true;
            }
        }
    }


    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getState(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
        }
    }

    private void FillType()
    {
        SalesForce sf = new SalesForce();

        dsSF = sf.getSFType(div_code);

        if (dsSF.Tables[0].Rows.Count > 0)
        {
            ddlReporting.DataTextField = "sf_name";
            ddlReporting.DataValueField = "Sf_Code";
            ddlReporting.DataSource = dsSF;
            ddlReporting.DataBind();
        }
    }
    private void GetState()
    {
        SalesForce sf = new SalesForce();

        dsSF = sf.getSFStateType(ddlState.SelectedIndex.ToString(), div_code);

        if (dsSF.Tables[0].Rows.Count > 0)
        {
            ddlReporting.DataTextField = "sf_name";
            ddlReporting.DataValueField = "Sf_Code";
            ddlReporting.DataSource = dsSF;
            ddlReporting.DataBind();
        }
    }

    private void ResetALL()
    {
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtCityPin.Text = "";

        txtDesignation.SelectedIndex = -1;
        txtDOB.Text = "";
        txtDOW.Text = "";
        txtEMail.Text = "";
        txtEmployeeID.Text = "";
        txtFieldForceName.Text = "";
        txtHQ.Text = "";
        txtJoingingDate.Text = "";
        txtMobile.Text = "";
        txtPassword.Text = "";
        txtPerAddress1.Text = "";
        txtPerAddress2.Text = "";
        txtPerCityPin.Text = "";
        txtPhone.Text = "";

        txtTPDCRStartDate.Text = "";
        txtUserName.Text = "";
        UsrDfd_UserName.Text = "";
        ddlState.SelectedIndex = -1;
        ddlReporting.SelectedIndex = -1;
        ddlFieldForceType.SelectedIndex = -1;
        ddl_fftype.SelectedIndex = 1;
        ddlTerritoryName.SelectedIndex = -1;
        for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
        {
            chkboxLocation.Items[iIndex].Selected = false;
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {


        if (txtJoingingDate.Text != "" && txtTPDCRStartDate.Text != "")
        {
            DateTime joingdate = Convert.ToDateTime(txtJoingingDate.Text);
            DateTime startdate = Convert.ToDateTime(txtTPDCRStartDate.Text);

            if (joingdate > startdate)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Report Starting Date must be greater than Joining Date');</script>");
                txtTPDCRStartDate.Text = "";
                txtTPDCRStartDate.Focus();
            }
            else
            {

                System.Threading.Thread.Sleep(time);
                SalesForce sf = new SalesForce();
                int iReturn = -1;
                DateTime dtDOB;
                DateTime dtDOW;
                string strDOB = string.Empty;
                string strDOW = string.Empty;
                string strConfirmdt = string.Empty;
                     
                if (txtDOB.Text != "")
                {
                    dtDOB = Convert.ToDateTime(txtDOB.Text);
                    strDOB = dtDOB.Month + "-" + dtDOB.Day + "-" + dtDOB.Year;
                }
                else
                {
                    strDOB = "";
                }

                if (txtcnfdate.Text != "")
                {
                    DateTime dtcnf = Convert.ToDateTime(txtcnfdate.Text);
                   strConfirmdt = dtcnf.Month + "-" + dtcnf.Day + "-" + dtcnf.Year;
                }
                else
                {
                    strConfirmdt = "";
                }



                if (txtDOW.Text != "")
                {
                    dtDOW = Convert.ToDateTime(txtDOW.Text.ToString());
                    strDOW = dtDOW.Month + "-" + dtDOW.Day + "-" + dtDOW.Year;

                }
                else
                {
                    strDOW = "";
                }

                for (int i = 0; i < chkboxLocation.Items.Count; i++)
                {
                    if (chkboxLocation.Items[i].Selected)
                    {
                        sChkLocation = sChkLocation + chkboxLocation.Items[i].Value + ",";
                    }
                }

                if (Convert.ToInt32(ddlFieldForceType.SelectedValue) == 2)
                {
                    isManager = true;
                    reporting_to = ddlReporting.SelectedValue.ToString();
                }

                if ((sf_hq != "") && (sf_hq != null) || (Reporting_To_SF != "") && (Reporting_To_SF != null))
                {
                    if ((Reporting_To_SF != "") && (Reporting_To_SF != null))
                    {
                        iReturn = sf.Activate(sfcode);
                    }

                    iReturn = sf.vbRecordUpdate(sfcode, rblVacantBlock.SelectedValue, txtDCRDate.Text, txtTPDCRStartDate.Text, txteffe.Text);
                }
                else if ((sf_type != "") && (sf_type != null))
                {
                    iReturn = sf.Block(sfcode, txtReason.Text, div_code);
                }
                else if ((sfreason != "") && (sfreason != null))
                {

                    iReturn = sf.Activate(sfcode);
                    if (iReturn > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Activated Successfully');window.location='BlockSFList.aspx';</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Activate');</script>");
                    }
                }
                else if ((sfcode != "") && (sfcode != null))// Update SalesForce
                {
                    Designation Desig = new Designation();
                    DataSet ds = Desig.getDesignationEd(txtDesignation.SelectedValue, div_code);
                    if (ds.Tables[0].Rows.Count > 0)
                        sDesSName = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    // Added by Sridevi - Rep Starting Date Check
                    if (ViewState["Rep_StartDate"] != null)
                    {
                        DateTime ext_Rep_date;
                        DateTime Upd_Rep_date;
                        bool isRepSt = false;
                        ext_Rep_date = Convert.ToDateTime(ViewState["Rep_StartDate"].ToString());
                        Upd_Rep_date = Convert.ToDateTime(txtTPDCRStartDate.Text);
                        if (ext_Rep_date != Upd_Rep_date)
                        {
                            if (Upd_Rep_date < ext_Rep_date)
                            {
                                SalesForce ds1 = new SalesForce();
                                bool isD = false;
                                bool isT = false;
                                isD = ds1.IsDcrStarted(sfcode);
                                if (isD == false)
                                {
                                    isT = ds1.IsTpStarted(sfcode);
                                    if (isT == true)
                                    {
                                        isRepSt = true;
                                    }
                                }
                                else
                                {
                                    isRepSt = true;
                                }
                            }
                            if (isRepSt == true)
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reporting already Started. Kindly Enter Valid Report Start Date');</script>");
                            }
                            else
                            {
                                iReturn = sf.Rep_StDateRecordUpdate(sfcode, Upd_Rep_date);
                            }
                        }
                    }
                    // Rep StartDate Check ENds Here

                    int checkstate = Convert.ToInt16(ViewState["statecheck"]);
                    int checkdes = Convert.ToInt16(ViewState["checkdesig"]);
                    if ((Convert.ToInt16(ddlState.SelectedValue) == checkstate) && (Convert.ToInt16(txtDesignation.SelectedValue) == checkdes))
                    {
                        txtUserName.Text = ViewState["usernamecheck"].ToString();

                    }
                    //Giri get Territory


                    iReturn = sf.RecordUpdate(sfcode, txtFieldForceName.Text, txtUserName.Text, txtPassword.Text, Convert.ToDateTime(txtJoingingDate.Text), ddlReporting.SelectedValue, ddlState.SelectedValue, Convert.ToDateTime(txtTPDCRStartDate.Text), txtAddress1.Text, txtAddress2.Text, txtCityPin.Text, txtEMail.Text, txtMobile.Text, strDOB, strDOW, txtPerAddress1.Text, txtPerAddress2.Text, txtPerCityPin.Text, txtPhone.Text, txtHQ.Text, division_code, Convert.ToInt32(ddlFieldForceType.SelectedValue), txtEmployeeID.Text, "", txtDesignation.SelectedValue, sChkLocation, UsrDfd_UserName.Text, FFType.SelectedValue, sDesSName, ddlTerritoryName.SelectedItem.ToString(), ddlTerritoryName.SelectedValue, strConfirmdt);

                    if (iReturn == -99)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate UserDefined UserName');</script>");
                    }
                    else
                    {
                        //Added by Sridevi to reset the Approval Manager Details if Reporting is modified
                        if (ViewState["Reporting_To"].ToString() != ddlReporting.SelectedValue.ToString())
                        {
                            SalesForce ssf = new SalesForce();
                            int ret = ssf.RecordUpdate_App(sfcode, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue);
                        }
                    }

                }

                else // Create SalesForce
                {

                    division_code = Session["div_code"].ToString() + ",";
                    // Added by sri - to pass designation short name
                    Designation Desig = new Designation();
                    DataSet ds = Desig.getDesignationEd(txtDesignation.SelectedValue, div_code);
                    if (ds.Tables[0].Rows.Count > 0)
                        sDesSName = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                    strSfCode = sf.RecordAdd(txtFieldForceName.Text, txtUserName.Text, txtPassword.Text, Convert.ToDateTime(txtJoingingDate.Text), ddlReporting.SelectedValue, ddlState.SelectedValue, Convert.ToDateTime(txtTPDCRStartDate.Text), txtAddress1.Text, txtAddress2.Text, txtCityPin.Text, txtEMail.Text, txtMobile.Text, strDOB, strDOW, txtPerAddress1.Text, txtPerAddress2.Text, txtPerCityPin.Text, txtPhone.Text, txtHQ.Text, division_code, Convert.ToInt32(ddlFieldForceType.SelectedValue), txtEmployeeID.Text, "", txtDesignation.SelectedValue, sChkLocation, sDesSName, UsrDfd_UserName.Text, FFType.SelectedValue, ddlTerritoryName.SelectedItem.ToString(), ddlTerritoryName.SelectedValue, strConfirmdt);

                    //Create Approval Manager Table
                    if (strSfCode != "Dup")
                    {
                        if ((strSfCode != "") && (strSfCode != null))
                        {
                            iReturn = sf.RecordAddApprovalMgr(strSfCode, txtFieldForceName.Text, txtHQ.Text, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, ddlReporting.SelectedValue, Session["div_code"].ToString());
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate UserDefined UserName');</script>");
                    }
                }

                if (iReturn > 0)
                {
                    if ((sf_hq != "") && (sf_hq != null) || (Reporting_To_SF != "") && (Reporting_To_SF != null))
                    {
                        SalesForce sfvac = new SalesForce();
                        dsSalesForce = sfvac.getSalesForce_ReportingTo(Session["div_code"].ToString(), sfcode);
                        if (dsSalesForce.Tables[0].Rows.Count > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SalesForce made Vacant Successfully');</script>");



                            Response.Redirect("~/MasterFiles/MapReportingStructure.aspx?reporting_to=" + sfcode);



                        }
                        else
                        {
                            if ((Designation_Name != "") && (Designation_Name != null))
                            {
                                if (rdoMode.SelectedValue.ToString() == "0")
                                {
                                    System.Threading.Thread.Sleep(time);
                                    Session["joiningdate"] = txtJoingingDate.Text;
                                    Session["Fieldforce_Name"] = txtFieldForceName.Text;
                                    Session["UserDefin"] = UsrDfd_UserName.Text;
                                    Session["Sf_emp_id"] = txtEmployeeID.Text;
                                    Session["Vac_sfcode"] = sfcode;
                                    Session["Effiective_Date"] = txteffe.Text;
                                    iReturn = sf.RecordUdpate_forBase_Manag(sfcode, div_code);
                                    Response.Redirect("SalesForce.aspx?Promote_Mode=" + rdoMode.SelectedValue.ToString());
                                }
                                else
                                {
                                    System.Threading.Thread.Sleep(time);
                                    Session["joiningdate"] = txtJoingingDate.Text;
                                    Session["UserDefin"] = UsrDfd_UserName.Text;
                                    Session["Sf_emp_id"] = txtEmployeeID.Text;
                                    Session["Vac_sfcode"] = sfcode;
                                    Session["Effiective_Date"] = txteffe.Text;
                                    iReturn = sf.RecordUdpate_forBase_Manag(sfcode, div_code);
                                    Response.Redirect("SalesForce.aspx?sfcode=" + ddlrepla.SelectedValue.ToString() + "&Promote_Mode=" + rdoMode.SelectedValue.ToString() + "&sfname=" + txtFieldForceName.Text);
                                }
                            }

                            //menu1.Status = "SalesForce made Vacant Successfully ";
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SalesForce made Vacant Successfully');window.location='SalesForceList.aspx';</script>");


                        }
                    }
                    else if ((usrname != "") && (usrname != null) || (desname != "") && (desname != null)) // Promo / De-Promo
                    {
                        SalesForce sfvac = new SalesForce();
                        if ((usrname != "") && (usrname != null))
                        {
                            //Create Promotion Dtls Table

                            iReturn = sf.PromoteSf(sfcode, txtFieldForceName.Text, ViewState["cur_des"].ToString(), txtDesignation.SelectedValue, ddlReporting.SelectedValue, div_code, txteffe.Text);
                        }
                        if ((desname != "") && (desname != null))
                        {
                            //Create DePromotion Dtls Table
                            iReturn = sf.DePromoteSf(sfcode, txtFieldForceName.Text, ViewState["cur_des"].ToString(), txtDesignation.SelectedValue, ddlReporting.SelectedValue, div_code, txteffe.Text);
                        }
                        dsSalesForce = sfvac.getSalesForce_ReportingTo(Session["div_code"].ToString(), sfcode);
                        if (dsSalesForce.Tables[0].Rows.Count > 0)
                        {
                            if ((usrname != "") && (usrname != null))
                            {
                                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SalesForce Promoted Successfully');</script>");
                                //Response.Redirect("~/MasterFiles/Promote.aspx?reporting_to=" + sfcode);                        
                                Response.Write("<script>alert('SalesForce Promoted Successfully') ; location.href='Promote.aspx?reporting_to=" + sfcode + "'</script>");
                            }
                            if ((desname != "") && (desname != null))
                            {
                                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SalesForce De-Promoted Successfully');</script>");
                                //Response.Redirect("~/MasterFiles/DePromote.aspx?reporting_to=" + sfcode);

                                Response.Write("<script>alert('SalesForce De-Promoted Successfully') ; location.href='DePromote.aspx?reporting_to=" + sfcode + "'</script>");
                            }

                        }
                        else
                        {
                            if ((usrname != "") && (usrname != null))
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SalesForce Promoted Successfully');window.location='Salesforce_Promo_DePromo.aspx';</script>");
                            }
                            if ((desname != "") && (desname != null))
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SalesForce De-Promoted Successfully');window.location='Salesforce_Promo_DePromo.aspx';</script>");
                            }
                        }
                    }
                    else if ((sfname != "") && (sfname != null))
                    {
                        SalesForce sfAc = new SalesForce();
                        iReturn = sf.RecordUpdate(sfcode, txtFieldForceName.Text, txtUserName.Text, txtPassword.Text, Convert.ToDateTime(txtJoingingDate.Text), ddlReporting.SelectedValue, ddlState.SelectedValue, Convert.ToDateTime(txtTPDCRStartDate.Text), txtAddress1.Text, txtAddress2.Text, txtCityPin.Text, txtEMail.Text, txtMobile.Text, strDOB, strDOW, txtPerAddress1.Text, txtPerAddress2.Text, txtPerCityPin.Text, txtPhone.Text, txtHQ.Text, division_code, Convert.ToInt32(ddlFieldForceType.SelectedValue), txtEmployeeID.Text, "", txtDesignation.SelectedValue, sChkLocation, UsrDfd_UserName.Text, FFType.SelectedValue, sDesSName, ddlTerritoryName.SelectedItem.ToString(), ddlTerritoryName.SelectedValue, strConfirmdt);
                        iReturn = sfAc.VacActivate(sfcode);
                        if (iReturn > 0)
                        {
                            //  dsSalesForce = sfAc.getActiveReportingTo(sfcode);
                            dsSalesForce = sfAc.getSalesForce_ReMap_ReportingTo(div_code, sfcode, ddlReporting.SelectedValue);
                            if (dsSalesForce.Tables[0].Rows.Count > 0)
                            {
                                //if (rdoMode == "1")
                                //{
                                //    iReturn = sf.Transfer_RecordInsert(sfname, ViewState["Vac_sfcode"].ToString(), sfcode, Convert.ToInt16(ViewState["from_designation"].ToString()), Convert.ToInt16(txtDesignation.SelectedValue), ViewState["from_division"].ToString(), div_code);

                                //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Transferred Successfully');</script>");
                                //}

                                if (Promote_Mode == "1")
                                {
                                    iReturn = sf.RecordUpdate(ViewState["Vac_sfcode"].ToString(), sfcode);

                                    iReturn = sf.Transfer_RecordInsert(sfname, ViewState["Vac_sfcode"].ToString(), sfcode, Convert.ToInt16(ViewState["from_designation"].ToString()), Convert.ToInt16(txtDesignation.SelectedValue), ViewState["from_division"].ToString(), div_code);

                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Promoted Successfully');</script>");
                                }

                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Activated Successfully');</script>");
                                Response.Redirect("~/MasterFiles/ReMapReportingStructure.aspx?reporting_to=" + sfcode + "&reporting_sf=" + ddlReporting.SelectedValue);
                                //Response.Write("<script>alert('Activated Successfully') ; location.href=ReMapReportingStructure.aspx?reporting_to="+ sfcode + "'</script>");
                                //Response.Redirect("ReMapReportingStructure.aspx?reporting_to=" + sfcode);
                            }
                            else
                            {


                                if (Promote_Mode == "1")
                                {
                                    iReturn = sf.RecordUpdate(ViewState["Vac_sfcode"].ToString(), sfcode);

                                    iReturn = sf.Transfer_RecordInsert(sfname, ViewState["Vac_sfcode"].ToString(), sfcode, Convert.ToInt16(ViewState["from_designation"].ToString()), Convert.ToInt16(txtDesignation.SelectedValue), ViewState["from_division"].ToString(), div_code);

                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Promoted Successfully');window.location='SalesForceList.aspx';</script>");
                                }

                                //menu1.Status = "SalesForce Activated Successfully";
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Activated Successfully');window.location='VacantSFList.aspx';</script>");
                            }
                        }
                        else
                        {
                            //menu1.Status = "Unable to Activate";
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Activate');</script>");
                        }
                    }

                    else if ((sf_type != "") && (sf_type != null))
                    {
                        //menu1.Status = "SalesForce Blocked Successfully ";
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Blocked Successfully');window.location='SalesForceList.aspx';</script>");
                    }
                    else if ((sfcode != "") && (sfcode != null))// Update SalesForce
                    {
                        //menu1.Status = "SalesForce updated Successfully ";
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='SalesForceList.aspx';</script>");
                    }
                    else
                    {


                        if (Promote_Mode == "0")
                        {
                            iReturn = sf.RecordUpdate(ViewState["Vac_sfcode"].ToString(), strSfCode);

                            iReturn = sf.Transfer_RecordInsert(txtFieldForceName.Text, ViewState["Vac_sfcode"].ToString(), strSfCode, Convert.ToInt16(ViewState["from_designation"].ToString()), Convert.ToInt16(txtDesignation.SelectedValue), ViewState["from_division"].ToString(), div_code);
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Promoted Successfully');window.location='SalesForceList.aspx';</script>");

                        }

                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                        ResetALL();
                    }
                }
                else if (iReturn == -2)
                {
                    //menu1.Status = "SalesForce already Exist";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exist');</script>");
                }

            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();

        string st = ddlState.SelectedValue;
        dsState = sf.getProd(st, div_code);
        ddlTerritoryName.DataTextField = "Territory_name";
        ddlTerritoryName.DataValueField = "Territory_code";
        ddlTerritoryName.DataSource = dsState;
        ddlTerritoryName.DataBind();
        //ddlTerritoryName.Visible = true;
        ddlTerritoryName.Enabled = true;
        Username();
        //GetState();
        //FillType();
        if (txtDesignation.SelectedIndex == 0)
        {
            string strtxtUser = txtUserName.Text.Remove(txtUserName.Text.Length - 1, 1);
            txtUserName.Text = strtxtUser;
            //Modified by Sridevi - Userdefined user name to restrict auto edit 
            if ((sfcode == "") || (sfcode == null))
            {
                UsrDfd_UserName.Text = txtUserName.Text;
            }
        }
    }
    protected void txtDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        Username();
        //FillType();
        if (txtDesignation.SelectedIndex == 0)
        {
            string strtxtUser = txtUserName.Text.Remove(txtUserName.Text.Length - 1, 1);
            txtUserName.Text = strtxtUser;
            //Modified by Sridevi - Userdefined user name to restrict auto edit 
            if ((sfcode == "") || (sfcode == null))
            {
                UsrDfd_UserName.Text = txtUserName.Text;
            }
        }
    }

    protected void Username()
    {
        DataSet ds = new DataSet();
        SalesForce SF = new SalesForce();
        if (ddlState.SelectedIndex == 0)
        {
            txtDesignation.Enabled = true;
            txtDesignation.SelectedIndex = 0;
        }
        else
        {
            txtDesignation.Enabled = true;
            //ddlReporting.Enabled = true;
        }



        ds = SF.GetUserName(div_code, ddlState.SelectedValue, txtDesignation.SelectedValue);
        txtUserName.Text = ds.Tables[0].Rows[0]["Division_SName"].ToString() + ds.Tables[1].Rows[0]["ShortName"].ToString() +
                           ds.Tables[2].Rows[0]["Designation_Short_Name"].ToString() + ds.Tables[4].Rows[0]["Number"].ToString();
        //Modified by Sridevi - Userdefined user name to restrict auto edit 
        if ((sfcode == "") || (sfcode == null))
        {
            UsrDfd_UserName.Text = txtUserName.Text;
        }


    }
    protected void ddlFieldForceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFieldForceType.SelectedIndex == 1)
        {
            SalesForce sf = new SalesForce();
            dsSF = sf.getDesignation_MR(div_code);
            if (dsSF.Tables[0].Rows.Count > 0)
            {
                txtDesignation.DataSource = dsSF;
                txtDesignation.DataBind();
            }
            lblTerritory.Visible = true;
            ddlTerritoryName.Visible = true;
            //lblTitle_LocationDtls.Visible = true;
            //ChkAll.Visible = true;
            //CheckBoxList1.Visible = true;
        }
        else if (ddlFieldForceType.SelectedIndex == 2)
        {
            SalesForce sf = new SalesForce();
            dsSF = sf.getDesignation_Manager(div_code);
            if (dsSF.Tables[0].Rows.Count > 0)
            {
                txtDesignation.DataSource = dsSF;
                txtDesignation.DataBind();
            }
            lblTerritory.Visible = false;
            ddlTerritoryName.Visible = false;
            //lblTitle_LocationDtls.Visible = false;
            //ChkAll.Visible = false;
            //CheckBoxList1.Visible = false;

        }
    }
    private void loaddes()
    {
        DataSet dsddl = new DataSet();
        if (ddlFieldForceType.SelectedValue.ToString() == "1")
        {
            SalesForce sf = new SalesForce();
            dsddl = sf.getDesignation_MR(div_code);
            if (dsddl.Tables[0].Rows.Count > 0)
            {
                txtDesignation.DataSource = dsddl;
                txtDesignation.DataBind();
            }
            lblTerritory.Visible = true;
            ddlTerritoryName.Visible = true;
            //lblTitle_LocationDtls.Visible = true;
            //ChkAll.Visible = true;
            //CheckBoxList1.Visible = true;

        }
        else if (ddlFieldForceType.SelectedValue.ToString() == "2")
        {
            SalesForce sf = new SalesForce();
            dsddl = sf.getDesignation_Manager(div_code);
            if (dsddl.Tables[0].Rows.Count > 0)
            {
                txtDesignation.DataSource = dsddl;
                txtDesignation.DataBind();
            }
            lblTerritory.Visible = false;
            ddlTerritoryName.Visible = false;
            //lblTitle_LocationDtls.Visible = false;
            //ChkAll.Visible = false;
            //CheckBoxList1.Visible = false;
        }
    }

    protected void rdoMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoMode.SelectedValue.ToString() == "0")
        {
            ddlrepla.Visible = false;
            lblreplace.Visible = false;
        }
        else
        {
            ddlrepla.Visible = true;
            lblreplace.Visible = true;
            FillVacantManagers();
        }
    }

    private void FillVacantManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getVacantManagersonly(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlrepla.DataTextField = "sf_name";
            ddlrepla.DataValueField = "Sf_Code";
            ddlrepla.DataSource = dsSalesForce;
            ddlrepla.DataBind();
        }
    }

    private void FillCheckBoxList()
    {
        //List of States are loaded into the checkbox list from Division Class

        SalesForce sf = new SalesForce();
        dsState = sf.getStateProd(div_code);
        ddlTerritoryName.DataTextField = "Territory_name";
        ddlTerritoryName.DataValueField = "Territory_code";
        ddlTerritoryName.DataSource = dsState;
        ddlTerritoryName.DataBind();

    }
    protected void ddlTerritoryName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsState = sf.getterr(sfcode, ddlTerritoryName.SelectedValue, div_code);
        if (dsState.Tables[0].Rows.Count > 0)
        {

            dsSF = sf.getSalesForce(sfcode);
            if (dsSF.Tables[0].Rows.Count > 0)
            {
                ddlTerritoryName.SelectedValue = dsSF.Tables[0].Rows[0].ItemArray.GetValue(35).ToString();
                //MessageBox.Show("First Change Stockist Territory!!!");
                Label7.Visible = true;
                Label7.Text = "First Change Stockist Territory!!!";

            }
        }
        else
        {
            Label7.Visible = false;
        }


    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        SalesForce sf = new SalesForce();
        Int16 sfStatus = Convert.ToInt16(ddlStatus.SelectedValue);
        int iReturn = sf.DeActivate(sfcode, sfStatus);
        if (iReturn > 0)
        {
            // menu1.Status = "SalesForce has been Deactivated Successfully";
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            ddlStatus.SelectedValue = sfStatus.ToString();

        }
        else
        {
            //menu1.Status = "Unable to Deactivate";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            ddlStatus.SelectedValue = "0";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
        }


    }
}