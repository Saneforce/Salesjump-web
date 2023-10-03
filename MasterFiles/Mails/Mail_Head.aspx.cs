using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Bus_EReport;

public partial class MasterFiles_Mails_Mail_Head : System.Web.UI.Page
{
    DataSet dsMail = null;
    DataSet dsFrom = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string sf_Type = string.Empty;
    string HO_ID = string.Empty;
    DataSet dsUserList = new DataSet();
    string sLevel = string.Empty;
    string temp_code = string.Empty;
    string mail_to_sf_code = string.Empty;
    string temp_Name = string.Empty;
    string mail_to_sf_Name = string.Empty;
    string mail_cc_sf_code = string.Empty;
    string strSF_Name = string.Empty;
    string mail_bcc_sf_code = string.Empty;
    SalesForce sf = new SalesForce();
    DataSet dsSalesForce = null;
    string strMail_CC = string.Empty;
    string sf_Name = string.Empty;
    string strMail_To = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string strFileDateTime = string.Empty;
    string div_Name = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_Type = Session["sf_type"].ToString();

        // Modified bySridevi  - 08Oct
        if (Session["div_code"] != null)
        {
            if (Session["div_code"].ToString() != "")
            {
                div_code = Session["div_code"].ToString();
                div_Name = Session["sf_name"].ToString();
            }
        }
        else if (sf_Type == "3")
        {
            div_code = Session["division_code"].ToString();
            div_Name = Session["div_Name"].ToString();
        }

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        // Ends Here

        HO_ID = Session["HO_ID"].ToString();

        string productName = Request.QueryString["ProductName"];
        lblSfName.Text = div_Name;
        // Changes done by Priya --Start
        if (sf_Type == "2" || sf_Type == "1")
        {
            lblSfName.Text = div_Name + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
        }
        else
        {
            string division_Name = Session["div_Name"].ToString();
            lblSfName.Text = division_Name;
        }

        if (!Page.IsPostBack)
        {
            //  Session["WinPostBack"] = "1";
            Session["Inbox"] = "Inbox";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowInbox();", true);

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //  AdminMailVisible();

            FillInbox();
       
            //FillMoveFolder();
            FillEMail();

            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
            dsTP = tp.Get_HOID_TP_Edit_Year(sf_Type, div_code);
            if (div_code != "")
            {
                dsTP = tp.Get_HOID_TP_Edit_Year(sf_Type, div_code);

                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                    {
                        ddlYr.Items.Add(k.ToString());
                        ddlMon.SelectedValue = DateTime.Now.Month.ToString();
                        ddlYr.SelectedValue = DateTime.Now.Year.ToString();
                    }
                }
            }
            else
            {
                string dt = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString() + "-" + DateTime.Now.Minute;
                ddlMon.SelectedValue = DateTime.Now.Month.ToString();
                ddlYr.Items.Add(DateTime.Now.Year.ToString());
            }


        }
        
        DeleteDisable(); 
    }
   
    private void FillAddressBook()
    {
        FillDesignation();
        //ss ADded by Sridevi - To load address book only once.. 

        DataTable dt1 = new DataTable();

        DataTable dtMR = new DataTable();
        DataSet dsmgrsf = new DataSet();
        SalesForce sf = new SalesForce();
        DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);

        if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
        {
            if (Session["sf_type"].ToString() == "1")
            {
                DCR dc = new DCR();
                dtMR = sf.getMail_MRJointWork(div_code, sf_code, 0);
                dsUserList.Tables.Add(dtMR);
                if (dsUserList.Tables[0].Rows.Count > 0)
                {
                    gvFF.DataSource = dsUserList;
                    gvFF.DataBind();
                }
                else
                {
                    gvFF.DataSource = dsUserList;
                    gvFF.DataBind();
                    lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;

                }
            }
            else if (Session["sf_code"].ToString() == "admin")
            {
                // Modified by Sridevi - 9- Oct -Starts                 
                if (div_code.Contains(','))
                {
                    div_code = div_code.Substring(0, div_code.Length - 1);
                }
                dtMR = sf.getAddressBookWithoutAdmin(div_code, sf_code, 0);
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                if (div_code.Contains(','))
                    div_code = div_code.Substring(0, div_code.Length - 1);
                dtMR = sf.getAddressBookMgr(div_code, sf_code, 0);
                // dt1 = sf.getMail_MRJointWork(div_code, sf_code, 0);
            }

        }
        else
        {
            dtMR = sf.getAuditManagerTeam_mail(div_code, sf_code, 0);
            //dsTP = dsmgrsf;

            //gvFF.DataSource = dt;
            //gvFF.DataBind();
            lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;

        }

        if (dtMR.Rows.Count > 0)
        {
            gvFF.DataSource = dtMR;
            gvFF.DataBind();
        }
        else
        {
            gvFF.DataSource = dtMR;
            gvFF.DataBind();
            lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
        }
        FillgridColor();
        // Ends - Loaded Address Book by Sridevi - 09 - Oct -15
    }
    private void MailCount()
    {
        AdminSetup admin = new AdminSetup();

        int Count;
        Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

        if (sf_Type != "3")
        {

            if (Count == 0)
            {
                btnHome.Visible = true;
                imgHome.Visible = true;
            }
            else
            {
                btnHome.Visible = false;
                imgHome.Visible = false;
            }
        }
        else
        {
            imgHome.Visible = true;
            btnHome.Visible = true;
        }
    }

    protected void Disable_Control() // Added BY Sridevi - to disable background
    {
        btnInbox.Enabled = false;
        btnCompose.Enabled = false;
        btnSentItem.Enabled = false;
        btnView.Enabled = false;
        btnHome.Enabled = false;
        lnkClear.Enabled = false;
        lnkAttach.Enabled = false;
        lnkRemoveCC.Enabled = false;
        pnlMoveFolder.Enabled = false;
        lnkButton.Enabled = false;

    }

    protected void Enable_Control() // Added BY Sridevi - to disable background
    {
        // enable all controls that are disabled

        btnInbox.Enabled = true;
        btnCompose.Enabled = true;
        btnSentItem.Enabled = true;
        btnView.Enabled = true;
        btnHome.Enabled = true;
        lnkClear.Enabled = true;
        lnkAttach.Enabled = true;
        lnkRemoveCC.Enabled = true;
        pnlMoveFolder.Enabled = true;
        lnkButton.Enabled = true;

    }

    protected void AdminMailVisible()
    {
        try
        {

            foreach (GridViewRow Row in gvFF.Rows)
            {
                Label lblMail_SF_Name = (Label)Row.FindControl("lblMail_SF_Name");
                if (Session["sf_name"].ToString() == lblMail_SF_Name.Text)
                {
                    gvFF.Visible = true;
                }
                else
                {
                    gvFF.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            
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

    protected void chkLevelAll_CheckedChanged(object sender, EventArgs e)
    {
        //FillUserList("");      
        if (chkLevelAll.Checked)
        {
            foreach (ListItem item in chkDesgn.Items)
            {
                if (chkLevelAll.Checked == true)
                {
                    item.Selected = true;
                    chkMR.Checked = true;
                }
            }

            btnGo_Click(sender, e);
            foreach (GridViewRow row in gvFF.Rows)
            {
               
                CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
                chkSf.Checked = true;
                for (int i = 0; i < gvFF.Rows.Count; i++)
                 {
                     int j = i + 1;
                     lblSelectedCount.Text = "No.of Filed Force Selected : " + j;
                 }
            }
                     
        }
        else
        {
            foreach (GridViewRow row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
                chkSf.Checked = false;
                lblSelectedCount.Text = "No.of Filed Force Selected1 : " + 0;
            }
            foreach (ListItem items in chkDesgn.Items)
            {
                items.Selected = false;
                chkMR.Checked = false;
            }
        }

        //AlignList();
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");

        //FillgridColor();//Commented By Sridevi - as this is put in page load need not be put everywhere…

    }

    private void FillgridColor()
    {
        foreach (GridViewRow grid_row in gvFF.Rows)
        {
            Label lblBackColor = (Label)grid_row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            grid_row.BackColor = System.Drawing.Color.FromName(bcolor);
        }
    }
  

    //protected void chkLevel1_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkLevel1.Checked)
    //    {
    //        chkLevelAll.Checked = false;
    //        chkLevel2.Checked = false;
    //        chkLevel3.Checked = false;
    //        chkLevel4.Checked = false;

    //        FillUserList(chkLevel1.Text);
    //    }
    //}

    //protected void chkLevel2_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkLevel2.Checked)
    //    {
    //        chkLevel1.Checked = false;
    //        chkLevelAll.Checked = false;
    //        chkLevel3.Checked = false;
    //        chkLevel4.Checked = false;

    //        FillUserList(chkLevel2.Text);
    //    }
    //}

    //protected void chkLevel3_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkLevel3.Checked)
    //    {
    //        chkLevelAll.Checked = false;
    //        chkLevel2.Checked = false;
    //        chkLevel1.Checked = false;
    //        chkLevel4.Checked = false;

    //        FillUserList(chkLevel3.Text);
    //    }
    //}

    //protected void chkLevel4_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkLevel4.Checked)
    //    {
    //        chkLevel1.Checked = false;
    //        chkLevelAll.Checked = false;
    //        chkLevel3.Checked = false;
    //        chkLevel2.Checked = false;

    //        FillUserList(chkLevel4.Text);
    //    }
    //}

    protected void chkMR_OnCheckChanged(object sender, EventArgs e)
    {
        btnGo_Click(sender, e);
        if (chkMR.Checked == false)
        {
            chkLevelAll.Checked = false;
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        string sDes = string.Empty;
        string strMR;
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");

        if (chkMR.Checked == true)
        {
            sDes = "8,";
        }
        else
        {
            sDes = "0";
        }

        foreach (ListItem item in chkDesgn.Items)
        {
            if (item.Selected)
            {
                sDes = sDes + item.Value.ToString() + ",";                
            }
        }

        if(sDes.Length > 0)
            sDes = sDes.Substring(0,(sDes.Length - 1));
        
        // Commented Out - By Sridevi as this has to be done only at the page load once.. I have put this code in Page Load

        //AdminSetup adsp = new AdminSetup();
        //DataTable dt = new DataTable();

        //if (Session["sf_type"].ToString() == "1")
        //{
        //    DCR dc = new DCR();
        //    dsUserList = dc.LoadMailWorkwith(sf_code,div_code);
        //    if (dsUserList.Tables[0].Rows.Count > 0)
        //    {
        //        gvFF.DataSource = dsUserList;
        //        gvFF.DataBind();
        //    }
        //    else
        //    {
        //        gvFF.DataSource = dsUserList;
        //        gvFF.DataBind();
        //        lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;

        //    }
        //}
        //else 
        //{
        //    //DataTable dt=new DataTable();
        //    SalesForce sf=new SalesForce();
        //    if (Session["sf_code"].ToString() == "admin") // Modified by Sridevi - 9- Oct -Starts
        //    {
        //        dt = sf.getAddressBookWithoutAdmin(div_code, sf_code, 0);
        //    }
        //    else
        //    {
        //        if (div_code.Contains(','))
        //            div_code = div_code.Substring(0, div_code.Length - 1);
        //        dt = sf.getAddressBookMgr(div_code, "admin", 0);
        //    } // Modified by Sridevi - 9 Oct - Ends
        //    if (dt.Rows.Count > 0)
        //    {
        //        gvFF.DataSource = dt;
        //        gvFF.DataBind();
        //    }
        //    else
        //    {
        //        gvFF.DataSource = dt;
        //        gvFF.DataBind();
        //        lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;

        //    }
            
        //}

        // Ends

        //foreach (GridViewRow row in gvFF.Rows)
        //{
        //    CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
        //    chkSf.Checked = true;
        //    for (int i = 0; i < gvFF.Rows.Count; i++)
        //    {
        //        int j = i + 1;
        //        lblSelectedCount.Text = "No.of Filed Force Selected : " + j;
        //    }
        //}

      
        // FillgridColor(); Commented By Sridevi - as this is put in page load need not be put everywhere...
    }

    protected void chkMR_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        chkDesgn_OnSelectedIndexChanged(sender, e);
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
    }
    protected void chkDesgn_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow grid_row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
            Label lblSf_Name = (Label)grid_row.FindControl("lblSf_Name");

            if (lblSf_Name.Text.Contains("admin"))
            {
                if (chkLevelAll.Checked == true)
                {
                    chkSf.Checked = true;
                }
            }           
            
        }

        foreach (ListItem item in chkDesgn.Items)
        {
            if (item.Selected == true)
            {
                foreach (GridViewRow grid_row in gvFF.Rows)
                {
                    Label lblDesignation_Code = (Label)grid_row.FindControl("lblDesignation_Code");
                    CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
                    if (item.Value == lblDesignation_Code.Text)
                    {
                        chkSf.Checked = true;
                    }
                }
            }
            else
            {
                foreach (GridViewRow grid_row in gvFF.Rows)
                {
                    Label lblDesignation_Code = (Label)grid_row.FindControl("lblDesignation_Code");
                    CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
                    if (item.Value == lblDesignation_Code.Text)
                    {
                        chkSf.Checked = false;
                    }
                }
            }
        }

        int i = 0;
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            if (chkSf.Checked == true)
                i = i + 1;
        }

        lblSelectedCount.Text = "No.of Filed Force Selected : " + i;

    }
    
    

    //private void FillUserList(string sLevel)
    //{
    //    SalesForce sf = new SalesForce();

    //    dsUserList = sf.UserListMailAddress(div_code, "admin", sLevel);
    //    if (dsUserList.Tables[0].Rows.Count > 0)
    //    {
    //        //chkFF.DataTextField = "sf_name";
    //        //chkFF.DataValueField = "sf_mail";
    //        //chkFF.DataSource = dsUserList;
    //        //chkFF.DataBind();

    //        gvFF.DataSource = dsUserList;
    //        gvFF.DataBind();
    //    }
       

        
    //}

    protected void btnFontBold_Click(object sender, EventArgs e)
    {
        if (ViewState["font_bold"] != null)
        {
            if (ViewState["font_bold"].ToString() == "1")
            {
                ViewState["font_bold"] = "2";
                txtMsg.Font.Bold = false;
            }
            else if (ViewState["font_bold"].ToString() == "2")
            {
                ViewState["font_bold"] = "1";
                txtMsg.Font.Bold = true;
            }
        }
        else
        {
            ViewState["font_bold"] = "1";
            txtMsg.Font.Bold = true;
        }

        pnlCompose.Style.Add("visibility", "visible");
        ViewState["pnlCompose"] = "true";
    }

    protected void btnFontItalic_Click(object sender, EventArgs e)
    {
        if (ViewState["font_italic"] != null)
        {
            if (ViewState["font_italic"].ToString() == "1")
            {
                ViewState["font_italic"] = "2";
                txtMsg.Font.Italic = false;
            }
            else if (ViewState["font_italic"].ToString() == "2")
            {
                ViewState["font_italic"] = "1";
                txtMsg.Font.Italic = true;
            }
        }
        else
        {
            ViewState["font_italic"] = "1";
            txtMsg.Font.Italic = true;
        }

        pnlCompose.Style.Add("visibility", "visible");
        ViewState["pnlCompose"] = "true";

    }

    protected void btnFontUnderline_Click(object sender, EventArgs e)
    {
        if (ViewState["font_ul"] != null)
        {
            if (ViewState["font_ul"].ToString() == "1")
            {
                ViewState["font_ul"] = "2";
                txtMsg.Font.Underline = false;
            }
            else if (ViewState["font_ul"].ToString() == "2")
            {
                ViewState["font_ul"] = "1";
                txtMsg.Font.Underline = true;
            }
        }
        else
        {
            ViewState["font_ul"] = "1";
            txtMsg.Font.Underline = true;
        }

        pnlCompose.Style.Add("visibility", "visible");
        ViewState["pnlCompose"] = "true";

    }
    
    protected void imgAddressBook_Click(object sender, EventArgs e)
    {
        FillAddressBook();
        // Added by Sridevi - To Clear for the new mail
        if((txtAddr.Text.Length == 0)&&(txtAddr1.Text.Length == 0)&&(txtAddr2.Text.Length == 0))
        {
            lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
            foreach (GridViewRow row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
                chkSf.Checked = false;
            }
        }
        // End by Sridevi - 10-Oct-15
        foreach (ListItem item in chkDesgn.Items)
        {
            chkLevelAll.Checked = true;
            chkMR.Checked = true;
            item.Selected = true;
        }

        rdoadr.SelectedValue= "0";
        ddlFFType_SelectedIndexChanged(sender, e);
        btnGo_Click(sender, e);
        //FillUserList("");
        //AlignList();

        // FillgridColor();Commented By Sridevi - as this is put in page load need not be put everywhere...

        //  lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;Commented By Sridevi - as this is put in page load need not be put everywhere...

        ViewState["from"] = "To";
        pnlCompose.Style.Add("visibility", "visible");
        pnlpopup.Style.Add("visibility", "visible");
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        
        grdSent.Style.Add("visibility", "hidden");
        ViewState["pnlCompose"] = "true";
        ViewState["pnlpopup"] = "true";
        if (rdoadr.SelectedValue.ToString() == "0")
        {
            chkLevelAll.Enabled = true;
            //chkMR.Visible = true;
            chkDesgn.Enabled = true;
            ddlFFType.Visible = false;
            ddlAlpha.Visible = false;
            ddlFieldForce.Visible = false;
            chkLevelAll.Visible = true;
            chkDesgn.Visible = true;
            Label3.Visible = true;
            lblFF.Visible = false;
        }
        else
        {
            chkLevelAll.Enabled = false;
            chkDesgn.Enabled = false;
            lblFF.Visible = true;
            ddlFFType.Visible = true;
            ddlAlpha.Visible = true;
            ddlFieldForce.Visible = true;
            chkLevelAll.Visible = false;
            chkDesgn.Visible = false;
            Label3.Visible = false;
            foreach (ListItem item in chkDesgn.Items)
            {
                chkLevelAll.Checked = false;
                chkMR.Checked = false;
                item.Selected = false;
            }
        }

        foreach (ListItem item in chkDesgn.Items)
        {
            chkLevelAll.Checked = false;
            chkMR.Checked = false;
            item.Selected = false;
        }
        Disable_Control();
        FillgridColor();
    }

    protected void rdoadr_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoadr.SelectedValue.ToString() == "0")
        {       
            chkLevelAll.Enabled = true;
            ddlFFType.SelectedValue = "0";
            chkDesgn.Enabled = true;            
            ddlFFType.Visible = false;
            ddlAlpha.Visible = false;
            ddlFieldForce.Visible = false;
            chkLevelAll.Visible = true;
            chkDesgn.Visible = true;
            //chkMR.Visible = true;
            chkMR.Checked = false;           
            Label3.Visible = true;
            lblFF.Visible = false;

            // New changes added by saravanan Start

            foreach (ListItem item in chkDesgn.Items)
            {
                chkLevelAll.Checked = false;               
                item.Selected = false;
            }
            btnGo_Click(sender, e);

            foreach (GridViewRow row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
                chkSf.Checked = false;
                lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
                
            }

            // New changes added by saravanan End
        }
        else
        {
            chkLevelAll.Enabled = false;
            chkDesgn.Enabled = false;
            lblFF.Visible = true;
            ddlFFType.Visible = true;
           
            ddlFieldForce.Visible = true;
            chkLevelAll.Visible = false;
            chkDesgn.Visible = false;
            Label3.Visible = false;

            // New changes added by saravanan Start

            foreach (GridViewRow row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
                chkSf.Checked = false;
                lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;

            }

            // New changes added by saravanan End           

            // lblSelectedCount.Text = "No.of Filed Force Selected : " + 0; //Commented By Sridevi - as this is put in page load need not be put everywhere…
        }

       
        btn_Go(sender, e);

        // FillgridColor();Commented By Sridevi - as this is put in page load need not be put everywhere…

        ddlFFType.SelectedValue = "2";
        ddlFieldForce.SelectedValue = "0";
        FillManagers();
        
        //ddlFFType_SelectedIndexChanged(sender, e);
       
    }
    protected void btnReply_Onclick(object sender, EventArgs e)
    {
        try
        {
            gvInbox.Style.Add("visibility", "hidden");
            grdSent.Style.Add("visibility", "hidden");
            grdView.Style.Add("visibility", "hidden");
          
            pnlFolder.Style.Add("visibility", "hidden");
            //pnlFolder
            PanelDisable();
            foreach (GridViewRow row in grdSent.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
                Label lblslNo = (Label)row.FindControl("lblslNo");
                if (ChkBoxRows.Checked == true)
                {
                    btnDelete.Enabled = true;
                    btnDelete.ForeColor = System.Drawing.Color.Black;
                    ddlMoved.Enabled = true;
                    btnForward.Enabled = true;
                    btnForward.ForeColor = System.Drawing.Color.Black;
                    //Response.Redirect("Mail_Head.aspx?inbox_id=" + lblslNo.Text + "");
                    //Request.QueryString["inbox_id"] = lblslNo.Text;
                    OpenPopup(lblslNo.Text);
                    imgbtnReplyViewMail_Click(sender, e);
                }

            }

            foreach (GridViewRow row in grdView.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
                Label lblslNo = (Label)row.FindControl("lblslNo");
                if (ChkBoxRows.Checked == true)
                {
                    btnDelete.Enabled = true;
                    btnDelete.ForeColor = System.Drawing.Color.Black;
                    ddlMoved.Enabled = true;
                    btnForward.Enabled = true;
                    btnForward.ForeColor = System.Drawing.Color.Black;
                    //Response.Redirect("Mail_Head.aspx?inbox_id=" + lblslNo.Text + "");
                    //Request.QueryString["inbox_id"] = lblslNo.Text;
                    OpenPopup(lblslNo.Text);
                    imgbtnReplyViewMail_Click(sender, e);
                }

            }

            foreach (GridViewRow row in gvInbox.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
                Label lblslNo = (Label)row.FindControl("lblslNo");
                if (ChkBoxRows.Checked == true)
                {
                    btnDelete.Enabled = true;
                    btnDelete.ForeColor = System.Drawing.Color.Black;
                    ddlMoved.Enabled = true;
                    btnForward.Enabled = true;
                    btnForward.ForeColor = System.Drawing.Color.Black;
                    //Response.Redirect("Mail_Head.aspx?inbox_id=" + lblslNo.Text + "");
                    //Request.QueryString["inbox_id"] = lblslNo.Text;
                    OpenPopup(lblslNo.Text);
                    imgbtnReplyViewMail_Click(sender, e);
                }

            }

            foreach (GridViewRow row in grdFolder.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("cbChkId");
                Label lblslNo = (Label)row.FindControl("lblslNo");
                if (ChkBoxRows.Checked == true)
                {
                    btnDelete.Enabled = true;
                    btnDelete.ForeColor = System.Drawing.Color.Black;
                    ddlMoved.Enabled = true;
                    btnForward.Enabled = true;
                    btnForward.ForeColor = System.Drawing.Color.Black;
                    //Response.Redirect("Mail_Head.aspx?inbox_id=" + lblslNo.Text + "");
                    //Request.QueryString["inbox_id"] = lblslNo.Text;
                    OpenPopup(lblslNo.Text);
                    imgbtnReplyViewMail_Click(sender, e);
                }

            }
            pnlViewInbox.Style.Add("visibility", "hidden");
            pnlViewMailInbox.Style.Add("visibility", "hidden");
        }
        catch (Exception ex)
        {

        }
    }
    protected void imgbtnReplyViewMail_Click(object sender, EventArgs e)
    {
        ViewState["from"] = "To";
        ViewState["mail_to_sf_Name"] = "";
        pnlCompose.Style.Add("visibility", "visible");
        //pnlpopup.Style.Add("visibility", "visible");
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        gvInbox.Style.Add("visibility", "hidden");
        grdSent.Style.Add("visibility", "hidden");
        grdView.Style.Add("visibility", "hidden");
        ViewState["pnlCompose"] = "true";
       // ViewState["pnlpopup"] = "true";

        txtAddr.Text = lblViewFrom.Text;
        txtAddr1.Text = ""; //CC
        txtAddr2.Text = ""; //BCC
        txtSub.Text = "Re: " + lblViewSub.Text;
        txtMsg.Text = "\n\n----------------------------\n" + lblMailBody.Text;       
        txtMsg.Focus();
        //OpenPopup(ViewState["inbox_id"].ToString());
        txtMsg.Text = "\n\n\n\n-------------------------------------------\n Message \nFrom:" + lblViewFrom.Text + "\nTo:" + lblViewTo.Text + "\nSent:" + lblViewSent.Text + "\n-------------------------------------------\n\n" + lblMailBody.Text;
        ViewState["mail_to_sf_Name"] = txtAddr.Text+ ",";
      //  ViewState["mail_to_sf_code"] = dsFrom.Tables[0].Rows[0]["mail_sf_from"].ToString();
       // txtMsg.Focus();
        ViewState["mail_to_sf_code"] = ViewState["mail_sf_from"].ToString() + ",";
            //ViewState["strMail_To"].ToString();
        //ViewState["mail_cc_sf_code"] = ViewState["strMail_CC"].ToString();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowMail();", true);
    }

    protected void imgbtnFwdViewMail_Click(object sender, EventArgs e)
    {
        ViewState["from"] = "To";
        pnlCompose.Style.Add("visibility", "visible");
        //pnlpopup.Style.Add("visibility", "visible");
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        gvInbox.Style.Add("visibility", "hidden");
        grdSent.Style.Add("visibility", "hidden");
        grdView.Style.Add("visibility", "hidden");
        ViewState["pnlCompose"] = "true";
      //  ViewState["pnlpopup"] = "true";
        txtAddr.Text = "";
      //  txtAddr1.Text = lblViewCC.Text; //CC
        txtAddr1.Text = "";
        txtAddr2.Text = ""; //BCC
        txtSub.Text = "Fw: " + lblViewSub.Text;
        txtMsg.Text = "\n\n\n\n-------------------------------------------\nForwarded Message \nFrom:" + lblViewFrom.Text + "\nTo:" + lblViewTo.Text + "\nSent:" + lblViewSent.Text + "\n-------------------------------------------\n\n" + lblMailBody.Text;
        txtMsg.Focus();
        //ViewState["mail_to_sf_code"] = ViewState["strMail_To"].ToString();
        //ViewState["mail_cc_sf_code"] = ViewState["strMail_CC"].ToString();

        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowMail();", true);
    }

    protected void imgbtnDeleteViewMail_Click(object sender, EventArgs e)
    {
        if (ViewState["inbox_id"] != null)
        {
            AdminSetup adm = new AdminSetup();
            int iRet = adm.ChangeMailStatus(sf_code, Convert.ToInt32(ViewState["inbox_id"].ToString()), -1,"");

            txtAddr.Text = "";
            txtSub.Text = "";
            txtMsg.Text = "";
            txtAddr1.Text = "";
            txtAddr2.Text = "";
            foreach (ListItem item in chkFF.Items)
            {
                item.Selected = false;
            }

            pnlViewInbox.Style.Add("visibility", "hidden");
            pnlViewMailInbox.Style.Add("visibility", "hidden");
            pnlInbox.Style.Add("visibility", "hidden");
            pnlCompose.Style.Add("visibility", "hidden");
            pnlpopup.Style.Add("visibility", "hidden");
            pnlSent.Style.Add("visibility", "hidden");
            pnlViewMail.Style.Add("visibility", "hidden");
            btnDelete_Onclick(sender, e);

          //  Response.Write("Mail has been deleted successfully");
        }
    }

    protected void Onchecked_Changed(object sender,EventArgs e)
    {

    }

    protected void imgbtnMovedToViewMail_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in chkFF.Items)
        {
            item.Selected = false;
        }

        ViewState["from"] = "To";
        pnlCompose.Style.Add("visibility", "visible");
        pnlpopup.Style.Add("visibility", "visible");
        ViewState["pnlCompose"] = "true";
        ViewState["pnlpopup"] = "true";
    }

    protected void imgComposeCC_Click(object sender, EventArgs e)
    {
        FillAddressBook();
        // Added by Sridevi - To Clear for the new mail
        if ((txtAddr.Text.Length == 0) && (txtAddr1.Text.Length == 0) && (txtAddr2.Text.Length == 0))
        {
            lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
            foreach (GridViewRow row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
                chkSf.Checked = false;
            }
        }
        // End by Sridevi - 10-Oct-15
        foreach (ListItem item in chkDesgn.Items)
        {
            chkLevelAll.Checked = true;
            chkMR.Checked = true;
            item.Selected = true;
        }

        rdoadr.SelectedValue = "0";
        ddlFFType_SelectedIndexChanged(sender, e);
        btnGo_Click(sender, e);
        //FillUserList("");
        //AlignList();

        // FillgridColor();Commented By Sridevi - as this is put in page load need not be put everywhere...

        //  lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;Commented By Sridevi - as this is put in page load need not be put everywhere...

        ViewState["from"] = "CC";
        pnlCompose.Style.Add("visibility", "visible");
        pnlpopup.Style.Add("visibility", "visible");
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");

        grdSent.Style.Add("visibility", "hidden");
        ViewState["pnlCompose"] = "true";
        ViewState["pnlpopup"] = "true";
        if (rdoadr.SelectedValue.ToString() == "0")
        {
            chkLevelAll.Enabled = true;
            //chkMR.Visible = true;
            chkDesgn.Enabled = true;
            ddlFFType.Visible = false;
            ddlAlpha.Visible = false;
            ddlFieldForce.Visible = false;
            chkLevelAll.Visible = true;
            chkDesgn.Visible = true;
            Label3.Visible = true;
            lblFF.Visible = false;
        }
        else
        {
            chkLevelAll.Enabled = false;
            chkDesgn.Enabled = false;
            lblFF.Visible = true;
            ddlFFType.Visible = true;
            ddlAlpha.Visible = true;
            ddlFieldForce.Visible = true;
            chkLevelAll.Visible = false;
            chkDesgn.Visible = false;
            Label3.Visible = false;
            foreach (ListItem item in chkDesgn.Items)
            {
                chkLevelAll.Checked = false;
                chkMR.Checked = false;
                item.Selected = false;
            }
        }

        foreach (ListItem item in chkDesgn.Items)
        {
            chkLevelAll.Checked = false;
            chkMR.Checked = false;
            item.Selected = false;
        }
        Disable_Control();
        FillgridColor();
    }

    protected void imgRemoveBCC_Click(object sender, EventArgs e)
    {
        pnlCompose.Style.Add("visibility", "visible");
        ViewState["pnlCompose"] = "true";

        if (ViewState["bcc"] != null)
        {
            if (ViewState["bcc"].ToString() == "1")
            {
                ViewState["bcc"] = "2";
                lnkRemoveCC.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Add Bcc&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                TrBCC.Visible = false;
            }
            else
            {
                ViewState["bcc"] = "1";
                lnkRemoveCC.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Remove Bcc&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                TrBCC.Visible = true;
            }
        }
        else
        {
            ViewState["bcc"] = "1";
            lnkRemoveCC.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Add Bcc&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            TrBCC.Visible = false;
        }

    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "sf_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }

    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFF.Text = "Field Force";
        FillManagers();
        //FillUserList("");
        
        // Commented by Sridevi   - Grid must not be null


        //if (ddlFFType.SelectedValue.ToString() == "2" || ddlFFType.SelectedValue.ToString() == "3")
        //{
        //    //FillColor();
        //    //FillgridColor();
        //    gvFF.DataSource = null;
        //    gvFF.DataBind();
        //}


    }

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkLevelAll.Checked = false;
        foreach (ListItem item in chkDesgn.Items)
        {
            item.Selected = false;
        }
        if (ddlFieldForce.SelectedValue.ToString().Trim().Length > 0)
        {
            ViewState["dsSalesForce"] = null;
            ViewState["dsDoctor"] = null;
            FillSalesForce();
            //AlignList();
           
            
        }
        else if (ddlFFType.SelectedItem.Text == "Division")
        {
            //FillDividion();
            dsUserList = sf.UserList_Self(ddlFieldForce.SelectedValue, "admin");

            if (dsUserList.Tables[0].Rows.Count > 0)
            {
                gvFF.Visible = true;
                gvFF.DataSource = dsUserList;
                gvFF.DataBind();

            }
        }
        int i = 0;
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            if(chkSf.Checked == true)
                i = i + 1;
        }
        lblSelectedCount.Text = "No.of Filed Force Selected : " + i;

        //FillgridColor();//Commented By Sridevi - as this is put in page load need not be put everywhere…
    }

    private void FillDividion()
    {
        AdminSetup Ad = new AdminSetup();
        dsSalesForce = Ad.GetDivision(ddlFieldForce.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            //chkFF.DataTextField = "sf_name";
            //chkFF.DataValueField = "sf_Code";
            //chkFF.DataSource = dsSalesForce;
            //chkFF.DataBind();
            gvFF.DataSource = dsSalesForce;
            gvFF.DataBind();
        }
        else
        {
            gvFF.DataSource = dsSalesForce;
            gvFF.DataBind();
            for (int i = 0; i < gvFF.Rows.Count; i++)
            {
                lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
            }
        }
    }
    private void FillSalesForce()
    {
        DataTable dt=new DataTable();


        if (ddlFieldForce.SelectedValue == "0")
        {
            foreach (GridViewRow grid_row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
                chkSf.Checked = false;
            }
        }
        if (Session["sf_type"].ToString() == "3")
        {
            if (div_code.Contains(','))
                div_code = div_code.Substring(0, div_code.Length - 1);
        }
        dsUserList = sf.UserList_get_SelfMail(div_code, ddlFieldForce.SelectedValue);
      

        foreach (GridViewRow grid_row in gvFF.Rows)
        {
            Label lblsf_Code = (Label)grid_row.FindControl("lblsf_Code");
            CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
            for (int i = 0; i < dsUserList.Tables[0].Rows.Count; i++)
            {
                if (dsUserList.Tables[0].Rows[i]["sf_code"].ToString() == lblsf_Code.Text)
                {
                    chkSf.Checked = true;
                }
            }
        }              
       
    }


    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "2")
        {
            ddlAlpha.Visible = false;
            if (Session["sf_type"].ToString() == "3")
            {
                if (div_code.Contains(','))
                    div_code = div_code.Substring(0, div_code.Length - 1);
            }
            dsSalesForce = sf.sp_UserList_Hierarchy_Mail(div_code, sf_code);
        }
        else if (ddlFFType.SelectedValue.ToString() == "1")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(div_code, sf_code);
        }
        //else if (ddlFFType.SelectedValue.ToString() == "3")
        //{
        //    //dsSalesForce = sf.UserList_HQ(div_code, "admin");
        //    ddlAlpha.Visible = false;
        //    Division dv = new Division();
        //    dsSalesForce = dv.getMailDivision(div_code);
        //    if (dsSalesForce.Tables[0].Rows.Count > 0)
        //    {
        //        ddlFieldForce.DataTextField = "sf_name";
        //        ddlFieldForce.DataValueField = "sf_code";
        //        ddlFieldForce.DataSource = dsSalesForce;
        //        ddlFieldForce.DataBind();
        //    }
           
            
        //}

        //if (ddlFFType.SelectedValue.ToString() == "2" || ddlFFType.SelectedValue.ToString() == "3")
        if (ddlFFType.SelectedValue.ToString() == "2")
        {
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                ddlFieldForce.DataTextField = "sf_name";
                ddlFieldForce.DataValueField = "sf_code";
                ddlFieldForce.DataSource = dsSalesForce;
                ddlFieldForce.DataBind();
            }
           
        }

        if (Session["sf_type"].ToString() == "1")
        {
            ddlFieldForce.Visible = false;
            ddlFFType.Visible = false;
            lblFF.Visible = false;
        }
        

        
        //AlignList();
    }

    //private void FillColor()
    //{
    //    int j = 0;

    //    foreach (ListItem ColorItems in ddlSF.Items)
    //    {
    //        //ddlFieldForce.Items[j].Selected = true;

    //        if (ColorItems.Text == "Level1")
    //            //ColorItems.Attributes.Add("style", "background-color: Wheat");
    //            ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Wheat");

    //        if (ColorItems.Text == "Level2")
    //            //ColorItems.Attributes.Add("style", "background-color: Blue");
    //            ddlFieldForce.Items[j].Attributes.Add("style", "background-color: LightGreen");

    //        if (ColorItems.Text == "Level3")
    //            //ColorItems.Attributes.Add("style", "background-color: Cyan");
    //            ddlFieldForce.Items[j].Attributes.Add("style", "background-color: Pink");

    //        if (ColorItems.Text == "Level4")
    //            //ColorItems.Attributes.Add("style", "background-color: Lavendar");
    //            ddlFieldForce.Items[j].Attributes.Add("style", "background-color: #FBAED2");

    //        j = j + 1;

    //    }
    //}

    protected void imgComposeBCC_Click(object sender, EventArgs e)
    {
        FillAddressBook();
        // Added by Sridevi - To Clear for the new mail
        if ((txtAddr.Text.Length == 0) && (txtAddr1.Text.Length == 0) && (txtAddr2.Text.Length == 0))
        {
            lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
            foreach (GridViewRow row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
                chkSf.Checked = false;
            }
        }
        // End by Sridevi - 10-Oct-15
        foreach (ListItem item in chkDesgn.Items)
        {
            chkLevelAll.Checked = true;
            chkMR.Checked = true;
            item.Selected = true;
        }

        rdoadr.SelectedValue = "0";
        ddlFFType_SelectedIndexChanged(sender, e);
        btnGo_Click(sender, e);
        //FillUserList("");
        //AlignList();

        // FillgridColor();Commented By Sridevi - as this is put in page load need not be put everywhere...

        //  lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;Commented By Sridevi - as this is put in page load need not be put everywhere...

        ViewState["from"] = "BCC";
        pnlCompose.Style.Add("visibility", "visible");
        pnlpopup.Style.Add("visibility", "visible");
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");

        grdSent.Style.Add("visibility", "hidden");
        ViewState["pnlCompose"] = "true";
        ViewState["pnlpopup"] = "true";
        if (rdoadr.SelectedValue.ToString() == "0")
        {
            chkLevelAll.Enabled = true;
            //chkMR.Visible = true;
            chkDesgn.Enabled = true;
            ddlFFType.Visible = false;
            ddlAlpha.Visible = false;
            ddlFieldForce.Visible = false;
            chkLevelAll.Visible = true;
            chkDesgn.Visible = true;
            Label3.Visible = true;
            lblFF.Visible = false;
        }
        else
        {
            chkLevelAll.Enabled = false;
            chkDesgn.Enabled = false;
            lblFF.Visible = true;
            ddlFFType.Visible = true;
            ddlAlpha.Visible = true;
            ddlFieldForce.Visible = true;
            chkLevelAll.Visible = false;
            chkDesgn.Visible = false;
            Label3.Visible = false;
            foreach (ListItem item in chkDesgn.Items)
            {
                chkLevelAll.Checked = false;
                chkMR.Checked = false;
                item.Selected = false;
            }
        }

        foreach (ListItem item in chkDesgn.Items)
        {
            chkLevelAll.Checked = false;
            chkMR.Checked = false;
            item.Selected = false;
        }
        Disable_Control();
        FillgridColor();
    }

    protected void ImgAttachment_Click(object sender, EventArgs e)
    {
        PnlAttachment.Style.Add("visibility", "hidden");
        ViewState["PnlAttachment"] = "";
        ViewState["from"] = "";
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlpopup.Style.Add("visibility", "hidden");
        pnlCompose.Style.Add("visibility", "hidden");
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowMail();", true);
    }

    protected void lbFileDel_Onclick(object sender, EventArgs e)
    {
        lblAttachment.Text = "";
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");

    }
    protected void btn_Go(object sender, EventArgs e)
    {

        lblAttachment.Text = FileUpload1.FileName;
        if (lblAttachment.Text != "")
        {
            strFileDateTime = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
            Session["strFileDateTime"] = strFileDateTime;
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/MasterFiles/Mails/Attachment/" + strFileDateTime + FileUpload1.FileName));
        }
       
        PnlAttachment.Style.Add("visibility", "hidden");
        pnlCompose.Style.Add("visibility", "visible");
        ViewState["PnlAttachment"] = "";
        //ViewState["from"] = "";
    }

    //protected void btnCount_Count(object sender, EventArgs e)
    //{
    //    //int Startcount = txtMsg.Text.Length;
    //    //int Endcount = 5000;
    //    //int count = 0;
    //    //count = Endcount - Startcount;      

    //    //lblCount.Text = "Number: " + count.ToString();
    //    //ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowMail();", true);
    //    string str = txtMsg.Text.Replace('\t', '|');
    //    txtMsg.Text = txtMsg.Text.Replace('\t', '|');

    //    //DataTable dt = new DataTable("Table1");
    //    //dt.Columns.Add("MyStringColumn", typeof(string));
    //    //dt.Columns.Add("MyTextBoxColumn", typeof(TextBox));
    //    //dt.Rows.Add("Row 1 Text", txtMsg.Text);
    //}
    protected void imgAddressClose_Click(object sender, EventArgs e)
    {
        
        string sel_ff = string.Empty;
        if (ViewState["from"] != null)
        {
            if (ViewState["from"].ToString() == "To")
            {
                txtAddr.Text = "";
            }
            else if (ViewState["from"].ToString() == "CC")
            {
                txtAddr1.Text = "";
            }
            else if (ViewState["from"].ToString() == "BCC")
            {
                txtAddr2.Text = "";
            }
           
            foreach (GridViewRow row in gvFF.Rows)
            {
                CheckBox ChkboxRows = (CheckBox)row.FindControl("chkSf");
                Label lblSf_Name = (Label)row.FindControl("lblSf_Name");
                Label lblsf_mail = (Label)row.FindControl("lblsf_mail");

                if (ChkboxRows.Checked==true)
                {
                    sel_ff = sel_ff + lblSf_Name.Text;
                    sel_ff = sel_ff.Replace("&nbsp;", "");
                    sel_ff = sel_ff + " , ";

                    temp_code = lblsf_mail.Text.ToString().Substring(0, lblsf_mail.Text.ToString().IndexOf('-'));
                    temp_Name = lblSf_Name.Text.ToString().Replace("&nbsp;", "");
                    mail_to_sf_code = mail_to_sf_code + temp_code + ",";
                    mail_to_sf_Name = mail_to_sf_Name + temp_Name + ",";

                    //ViewState["mail_to_sf_Name"] = mail_to_sf_Name;
                    //strSF_Name = ViewState["mail_to_sf_Name"].ToString();
                }
            }
        }

        if (ViewState["from"].ToString() == "To")
        {
            txtAddr.Text = sel_ff;
            ViewState["mail_to_sf_code"] = mail_to_sf_code;
            ViewState["mail_to_sf_Name"] = mail_to_sf_Name;
            
        }
        else if (ViewState["from"].ToString() == "CC")
        {
            txtAddr1.Text = sel_ff;
            ViewState["mail_cc_sf_code"] = mail_to_sf_code;
            ViewState["mail_to_sf_NameCC"] = mail_to_sf_Name;
            
        }
        else if (ViewState["from"].ToString() == "BCC")
        {
            txtAddr2.Text = sel_ff;
            ViewState["mail_bcc_sf_code"] = mail_to_sf_code;
            ViewState["mail_to_sf_NameBCC"] = mail_to_sf_Name;
            
        }

        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlpopup.Style.Add("visibility", "hidden");
        ViewState["pnlpopup"] = "";
        ViewState["from"] = "";
        Enable_Control();
        //gvInbox.Visible = false;
        //FillUserList("");
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowMail();", true);
    }

    protected void imgViewMail_Click(object sender, EventArgs e)
    {
        AdminSetup adm = new AdminSetup();
              
        //gv1.Visible = false;
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");

        // Modified by Sridevi - 10-10-2015 - starts
        if (Session["Inbox"] != null)
        {
            if (Session["Inbox"].ToString() == "Inbox")// Modified by Sridevi - To add ToString()
            {
                int iReturn = adm.ChangeMailStatus(sf_code, Convert.ToInt32(ViewState["inbox_id"].ToString()), 10, Session["sf_name"].ToString());
                
               // ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowInbox();", true);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowInbox();", true);
                FillInbox();
            }
        }
        else if (Session["SentItem"] != null)
        {

            if (Session["SentItem"].ToString() == "SentItme")// Modified by Sridevi - To add ToString()
            {
               

               // ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "SentMail();", true);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "SentMail();", true);
                FillSentEMail();
            }
        }
        else if (Session["Viewed"] != null)
        {
            if (Session["Viewed"].ToString() == "Viewed")// Modified by Sridevi - To add ToString()
            {
                
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ViewedMail();", true);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ViewedMail();", true);
                FillViewedMail();
            }
        }
        // Modified by Sridevi - 10-10-2015 - Ends    
        MailCount();
    }

    protected void lnkbtnClose_Click(object sender, EventArgs e)
    {
        try
        {
            imgViewMail_Click(sender, e);
        }
        catch (Exception ex)
        {

        }

    }
    

    private void AlignList()
    {
        string cur_sf_code = string.Empty;
        string cur_sf_level = string.Empty;
        //foreach (GridViewRow row in gvInbox.Rows)
        //{
        //    CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
        //    //CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");

        //    if (item.Text != "---Select Clear---")
        //    {
        //        cur_sf_code = item.Value.ToString().Substring(0, item.Value.ToString().IndexOf('-'));
        //        cur_sf_level = item.Value.ToString().Substring(item.Value.ToString().IndexOf('-') + 1, (item.Value.ToString().Length - cur_sf_code.Length) - 1);
        //    }
        //    if (cur_sf_level == "Level1")
        //    {
        //        //item.Text = "&nbsp;" + item.Text;
        //        item.Text = item.Text;
        //        item.Attributes.Add("style", "background-color: Wheat");

        //    }
        //    if (cur_sf_level == "Level2")
        //    {
        //        //item.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + item.Text;
        //        item.Text = item.Text;
        //        item.Attributes.Add("style", "background-color: LightGreen");

        //    }
        //    if (cur_sf_level == "Level3")
        //    {
        //        //item.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + item.Text;
        //        item.Text = item.Text;
        //        item.Attributes.Add("style", "background-color: Pink");

        //    }
        //    if (cur_sf_level == "Level4")
        //    {
        //        //item.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + item.Text;
        //        item.Text = item.Text;
        //        item.Attributes.Add("style", "background-color: White");
        //    }
        //}

        foreach (ListItem item in chkFF.Items)
        {
            if (item.Text != "---Select Clear---")
            {
                cur_sf_code = item.Value.ToString().Substring(0, item.Value.ToString().IndexOf('-'));
                cur_sf_level = item.Value.ToString().Substring(item.Value.ToString().IndexOf('-') + 1, (item.Value.ToString().Length - cur_sf_code.Length) - 1);
            }
            if (cur_sf_level == "Level1")
            {
                //item.Text = "&nbsp;" + item.Text;
                item.Text = item.Text;
                item.Attributes.Add("style", "background-color: Wheat");

            }
            if (cur_sf_level == "Level2")
            {
                //item.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + item.Text;
                item.Text = item.Text;
                item.Attributes.Add("style", "background-color: LightGreen");

            }
            if (cur_sf_level == "Level3")
            {
                //item.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + item.Text;
                item.Text = item.Text;
                item.Attributes.Add("style", "background-color: Pink");

            }
            if (cur_sf_level == "Level4")
            {
                //item.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + item.Text;
                item.Text = item.Text;
                item.Attributes.Add("style", "background-color: White");
            }
        }
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            //ddlFieldForce.Items[j].Selected = true;

            j = j + 1;

        }
    }

    private void FillViewedMail()
    {
        txtboxSearch.Visible = true;
        lblSubjectSearch.Visible = true;
        imgSearch.Visible = true;

        //if (Session["sf_type"] == "3")
        //{
        //    sf_code = Session["HO_ID"].ToString();
        //}
        //else
        //{
        //    sf_code = Session["sf_code"].ToString();
        //}

        AdminSetup adm = new AdminSetup();
        dsMail = adm.getMailInbox(sf_code, div_code, "view", "", ddlMon.SelectedValue.ToString(), ddlYr.SelectedValue.ToString(), "Mail_Sent_Time", "Desc",txtboxSearch.Text);
        if (dsMail.Tables[0].Rows.Count > 0)
        {
            //gv1.Visible = false;
            grdView.Style.Add("margin-top", "0px");
            grdView.Style.Add("margin-left", "0px");
            grdView.DataSource = dsMail;
            grdView.DataBind();
             foreach (GridViewRow row in grdView.Rows)
            {
                if (dsMail.Tables[0].Rows.Count > 0)
                {
                    Label lblSubject = (Label)row.FindControl("lblMail_subject");
                    lblSubject.Text = dsMail.Tables[0].Rows[row.RowIndex]["Mail_subject"].ToString();
                    lblSubject.Text = lblSubject.Text.Replace("asdf", "'");
                }
            }
        }
        else
        {
            grdView.Style.Add("margin-top", "250px");
            grdView.Style.Add("margin-left", "500px");
            grdView.DataSource = null;
            grdView.DataBind();
        }        
    }

    private void FillSentEMail()
    {
        txtboxSearch.Visible = true;
        lblSubjectSearch.Visible = true;
        imgSearch.Visible = true;

        //if (Session["sf_type"] == "3")
        //{
        //    sf_code = Session["HO_ID"].ToString();
        //}
        //else
        //{
        //    sf_code = Session["sf_code"].ToString();
        //}

        AdminSetup adm = new AdminSetup();
        dsMail = adm.getMailInbox(sf_code, div_code, "sent", "", ddlMon.SelectedValue.ToString(), ddlYr.SelectedValue.ToString(), "Mail_Sent_Time", "Desc",txtboxSearch.Text);
        if (dsMail.Tables[0].Rows.Count > 0)
        {
            grdSent.Style.Add("margin-top", "0px");
            grdSent.Style.Add("margin-left", "0px");
            grdSent.DataSource = dsMail;
            grdSent.DataBind();

            foreach (GridViewRow row in grdSent.Rows)
            {
                if (dsMail.Tables[0].Rows.Count > 0)
                {
                    Label lblSubject = (Label)row.FindControl("lblMail_subject");
                    lblSubject.Text= dsMail.Tables[0].Rows[row.RowIndex]["Mail_subject"].ToString();
                    lblSubject.Text = lblSubject.Text.Replace("asdf", "'");
                }
            }
             
            
        }
        else
        {
            grdSent.Style.Add("margin-top", "250px");
            grdSent.Style.Add("margin-left", "500px");
            grdSent.DataSource = null;
            grdSent.DataBind();
        }
    }

    private void FillInbox()
    {
        txtboxSearch.Visible = false;
        lblSubjectSearch.Visible = false;
        imgSearch.Visible = false;
        MailCount();  

        AdminSetup adm = new AdminSetup();
        dsMail = adm.getMailInbox(sf_code, div_code, "inbox", "", ddlMon.SelectedValue.ToString(), ddlYr.SelectedValue.ToString(), "Mail_Sent_Time", "Desc","");
        if (dsMail.Tables[0].Rows.Count > 0)
        {
            //gv1.Visible = true;
            gvInbox.Style.Add("margin-top", "20px");
            gvInbox.Style.Add("margin-left", "0px");
            gvInbox.DataSource = dsMail;
            gvInbox.DataBind();

            foreach (GridViewRow row in gvInbox.Rows)
            {
                if (dsMail.Tables[0].Rows.Count > 0)
                {
                    Label lblSubject = (Label)row.FindControl("lblMail_subject");
                    lblSubject.Text = dsMail.Tables[0].Rows[row.RowIndex]["Mail_subject"].ToString();
                    lblSubject.Text = lblSubject.Text.Replace("asdf", "'");
                }
            }
      

        }
        else
        {
            gvInbox.Style.Add("margin-top", "250px");
            gvInbox.Style.Add("margin-left", "500px");
            gvInbox.DataSource = null;
            gvInbox.DataBind();
        }
    }

    private void GetMovedFolder(string FolderName)
    {
        AdminSetup adm = new AdminSetup();
        dsMail = adm.getMailInbox(sf_code, div_code, "Flder", FolderName, ddlMon.SelectedValue.ToString(), ddlYr.SelectedValue.ToString(), "Mail_Sent_Time", "Desc","");
        if (dsMail.Tables[0].Rows.Count > 0)
        {
            //gv1.Visible = true;
            grdFolder.Style.Add("margin-top", "0px");
            grdFolder.Style.Add("margin-left", "0px");
            grdFolder.DataSource = dsMail;
            grdFolder.DataBind();

            foreach (GridViewRow row in grdFolder.Rows)
            {
                if (dsMail.Tables[0].Rows.Count > 0)
                {
                    Label lblSubject = (Label)row.FindControl("lblMail_subject");
                    lblSubject.Text = dsMail.Tables[0].Rows[row.RowIndex]["Mail_subject"].ToString();
                    lblSubject.Text = lblSubject.Text.Replace("asdf", "'");
                }
            }

        }
        else
        {
            grdFolder.Style.Add("margin-top", "250px");
            grdFolder.Style.Add("margin-left", "500px");
            grdFolder.DataSource = null;
            grdFolder.DataBind();
        }
    }

    private void FillDesignation()
    {

        DataTable dt = new DataTable();
        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            AdminSetup adm = new AdminSetup();
            if (Session["sf_type"].ToString() == "3")
            {
                if (div_code.Contains(','))
                    div_code = div_code.Substring(0, div_code.Length - 1);
            }

            dt = sf.getAddressBookDesign(div_code, "admin", 0);
            if (dt.Rows.Count > 0)
            {
                chkDesgn.DataTextField = "Designation_Short_Name";
                chkDesgn.DataValueField = "Designation_Code";
                chkDesgn.DataSource = dt;
                chkDesgn.DataBind();
            }
        }
        else if (Session["sf_type"].ToString() == "1")
        {
            
            DCR dc = new DCR();
            dt = dc.LoadMailWorkwithDes(sf_code);
            if (dt.Rows.Count > 0)
            {
                chkDesgn.DataTextField = "Designation_Short_Name";
                chkDesgn.DataValueField = "Designation_Code";
                chkDesgn.DataSource = dt;
                chkDesgn.DataBind();
            }
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            
            SalesForce sf = new SalesForce();
             DataSet dsmgrsf = new DataSet();          
            DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
            if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
            {
            dt = sf.getAddressBookDesign(div_code, sf_code, 0);
            }
            else
            {
                // Fetch Managers Audit Team
                dt = sf.getAuditManagerTeam_mail(div_code, sf_code, 0);
                DataView view = new DataView(dt);
                dt = view.ToTable(true, "Designation_Short_Name", "Designation_Code");

            }
            if (dt.Rows.Count > 0)
            {
                chkDesgn.DataTextField = "Designation_Short_Name";
                chkDesgn.DataValueField = "Designation_Code";
                chkDesgn.DataSource = dt;
                chkDesgn.DataBind();
            }
        }
    }
    private void FillMoveFolder()
    {
        AdminSetup adm = new AdminSetup();
        dsMail = adm.getMoved(div_code);
        if (dsMail.Tables[0].Rows.Count > 0)
        {
            ddlMoved.DataTextField = "Move_MailFolder_Name";
            ddlMoved.DataValueField = "Move_MailFolder_Id";
            ddlMoved.DataSource = dsMail;
            ddlMoved.DataBind();

            //ddlMovedTo.DataTextField = "Move_MailFolder_Name";
            //ddlMovedTo.DataValueField = "Move_MailFolder_Id";
            //ddlMovedTo.DataSource = dsMail;
            //ddlMovedTo.DataBind();
            
        }
        ddlMoved.Enabled = false; 
    }    

   
    private void FillEMail()
    {

        AdminSetup adm = new AdminSetup();
        dsMail = adm.getMail(div_code);
        if (dsMail.Tables[0].Rows.Count > 0)
        {
            grdClickFolder.DataSource = dsMail;
            grdClickFolder.DataBind();           

        }
    }   

    //protected void gv1_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        e.Row.Attributes["onmouseover"] = "this.style.cursor = 'pointer';this.style.textDecoration='underline';";
    //        e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
    //        e.Row.ToolTip = "Click to view mail";
    //        //e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.gv1, "Select$" + e.Row.RowIndex);
    //    }
    //}

    //protected void OnSelectedIndexChanged1(object sender, EventArgs e)
    //{
    //    //foreach (GridViewRow row in gv1.Rows)
    //    //{
    //    //    if (row.RowIndex == gv1.SelectedIndex)
    //    //    {
    //    //        row.BackColor = System.Drawing.Color.AliceBlue;
    //    //        row.ToolTip = string.Empty;

    //    //        HiddenField mailid = (HiddenField)row.FindControl("hdnslNo");
    //    //        AdminSetup ast = new AdminSetup();
    //    //        dsMail = ast.ViewMail(Convert.ToInt32(mailid.Value.ToString()));
    //    //        if (dsMail.Tables[0].Rows.Count > 0)
    //    //        {
    //    //            DivView.Visible = true;
    //    //            lblFrm.Text = dsMail.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //    //            lblTo.Text = dsMail.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
    //    //            lblCC.Text = ""; // dsMail.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //    //            lblSub.Text = dsMail.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
    //    //            lblSentDt.Text = dsMail.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
    //    //            ViewState["ViewMail"] = "true";
    //    //            ViewState["dsMail"] = dsMail;
    //    //        }

    //    //    }
    //    //    else
    //    //    {
    //    //    }
    //    //}

       

    //}

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cbSelectAll_OnCheckedChanged(object sender, EventArgs e)
    {
        PanelDisable();
        CheckBox ChkBoxHeader = (CheckBox)gvInbox.HeaderRow.FindControl("cbSelectAll");
        foreach (GridViewRow row in gvInbox.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
                btnDelete.Enabled = true;
                btnDelete.ForeColor = System.Drawing.Color.Black;
                ddlMoved.Enabled = true;                
            }
            else
            {
                ChkBoxRows.Checked = false;
                btnDelete.Enabled = false;
                ddlMoved.Enabled = false;
                btnDelete.ForeColor = System.Drawing.Color.Gray;                
            }
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowInbox();", true);     
    }

    protected void grdcbSelectAll_OnCheckedChanged(object sender, EventArgs e)
    {
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlCompose.Style.Add("visibility", "hidden");
        pnlpopup.Style.Add("visibility", "hidden");
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "SentMail();", true); 
        CheckBox ChkBoxHeader = (CheckBox)grdSent.HeaderRow.FindControl("cbSelectAll");
        foreach (GridViewRow row in grdSent.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
                btnDelete.Enabled = true;
                btnReply.Enabled = true;             
               
                btnDelete.ForeColor = System.Drawing.Color.Black;
                ddlMoved.Enabled = true;
                //btnForward.Enabled = true;
                //btnForward.ForeColor = System.Drawing.Color.Black;
                
            }
            else
            {
                ChkBoxRows.Checked = false;
                btnDelete.Enabled = false;
                btnReply.Enabled = false;
                //btnForward.Enabled = false;
                btnDelete.ForeColor = System.Drawing.Color.Gray;
                btnReply.ForeColor = System.Drawing.Color.Gray;

            }
        }
       
    }

    protected void btnForward_Onclick(object sender, EventArgs e)
    {
        try
        {
            gvInbox.Style.Add("visibility", "hidden");
            grdSent.Style.Add("visibility", "hidden");
            grdView.Style.Add("visibility", "hidden");
          
            pnlFolder.Style.Add("visibility", "hidden");
            PanelDisable();
            foreach (GridViewRow row in grdSent.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
                Label lblslNo = (Label)row.FindControl("lblslNo");
                if (ChkBoxRows.Checked == true)
                {
                    btnDelete.Enabled = true;
                    btnDelete.ForeColor = System.Drawing.Color.Black;
                    ddlMoved.Enabled = true;
                    btnForward.Enabled = true;
                    btnForward.ForeColor = System.Drawing.Color.Black;
                    //Response.Redirect("Mail_Head.aspx?inbox_id=" + lblslNo.Text + "");
                    //Request.QueryString["inbox_id"] = lblslNo.Text;
                    OpenPopup(lblslNo.Text);
                    imgbtnFwdViewMail_Click(sender, e);
                }

            }

            foreach (GridViewRow row in grdView.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
                Label lblslNo = (Label)row.FindControl("lblslNo");
                if (ChkBoxRows.Checked == true)
                {
                    btnDelete.Enabled = true;
                    btnDelete.ForeColor = System.Drawing.Color.Black;
                    ddlMoved.Enabled = true;
                    btnForward.Enabled = true;
                    btnForward.ForeColor = System.Drawing.Color.Black;
                    //Response.Redirect("Mail_Head.aspx?inbox_id=" + lblslNo.Text + "");
                    //Request.QueryString["inbox_id"] = lblslNo.Text;
                    OpenPopup(lblslNo.Text);
                    imgbtnFwdViewMail_Click(sender, e);
                }

            }

            foreach (GridViewRow row in gvInbox.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
                Label lblslNo = (Label)row.FindControl("lblslNo");
                if (ChkBoxRows.Checked == true)
                {
                    btnDelete.Enabled = true;
                    btnDelete.ForeColor = System.Drawing.Color.Black;
                    ddlMoved.Enabled = true;
                    btnForward.Enabled = true;
                    btnForward.ForeColor = System.Drawing.Color.Black;
                    //Response.Redirect("Mail_Head.aspx?inbox_id=" + lblslNo.Text + "");
                    //Request.QueryString["inbox_id"] = lblslNo.Text;
                    OpenPopup(lblslNo.Text);
                    imgbtnFwdViewMail_Click(sender, e);
                }

            }

            foreach (GridViewRow row in grdFolder.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("cbChkId");
                Label lblslNo = (Label)row.FindControl("lblslNo");
                if (ChkBoxRows.Checked == true)
                {
                    btnDelete.Enabled = true;
                    btnDelete.ForeColor = System.Drawing.Color.Black;
                    ddlMoved.Enabled = true;
                    btnForward.Enabled = true;
                    btnForward.ForeColor = System.Drawing.Color.Black;
                    //Response.Redirect("Mail_Head.aspx?inbox_id=" + lblslNo.Text + "");
                    //Request.QueryString["inbox_id"] = lblslNo.Text;
                    OpenPopup(lblslNo.Text);
                    imgbtnFwdViewMail_Click(sender, e);
                }

            }
            pnlViewInbox.Style.Add("visibility", "hidden");
            pnlViewMailInbox.Style.Add("visibility", "hidden");
           
        }
        catch (Exception ex)
        {

        }
    }
    private void PanelDisable()
    {
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlCompose.Style.Add("visibility", "hidden");
        pnlpopup.Style.Add("visibility", "hidden");
    }

    protected void grdchkId_OnCheckedChanged(object sender, EventArgs e)
    {
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlCompose.Style.Add("visibility", "hidden");
        pnlpopup.Style.Add("visibility", "hidden");
       
        foreach (GridViewRow row in grdSent.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
            if (ChkBoxRows.Checked == true)
            {
                btnDelete.Enabled = true;
                btnDelete.ForeColor = System.Drawing.Color.Black;
                ddlMoved.Enabled = true;
                btnForward.Enabled = true;
                btnForward.ForeColor = System.Drawing.Color.Black;
                btnReply.Enabled = true;
                btnReply.ForeColor = System.Drawing.Color.Black;
                ddlMoved.Enabled = true;

            }

        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "SentMail();", true); 
    }

    protected void grdViewcbSelectAll_OnCheckedChanged(object sender, EventArgs e)
    {
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");

        CheckBox ChkBoxHeader = (CheckBox)grdView.HeaderRow.FindControl("cbSelectAll");
        foreach (GridViewRow row in grdView.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
                btnDelete.Enabled = true;
                btnForward.Enabled = true;
                btnDelete.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                ChkBoxRows.Checked = false;
                btnDelete.Enabled = false;
                //btnForward.Enabled = false;
                btnDelete.ForeColor = System.Drawing.Color.Gray;

            }
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ViewedMail();", true); 
    }

    protected void grdViewchkId_OnCheckedChanged(object sender, EventArgs e)
    {
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlCompose.Style.Add("visibility", "hidden");
        pnlpopup.Style.Add("visibility", "hidden");

        foreach (GridViewRow row in grdView.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
            if (ChkBoxRows.Checked == true)
            {
                btnDelete.Enabled = true;
                btnDelete.ForeColor = System.Drawing.Color.Black;
                ddlMoved.Enabled = true;
                btnForward.Enabled = true;
                btnForward.ForeColor = System.Drawing.Color.Black;
                btnReply.Enabled = true;
                btnReply.ForeColor = System.Drawing.Color.Black;
                ddlMoved.Enabled = true;
            }

        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ViewedMail();", true);
    }



    protected void btnCompose_Onclick(object sender, EventArgs e)
    {
            System.Threading.Thread.Sleep(time);
            lnkClear_Click(sender, e);
            pnlViewInbox.Style.Add("visibility", "hidden");
            pnlViewMailInbox.Style.Add("visibility", "hidden");
            pnlpopup.Style.Add("visibility", "hidden");
            pnlCompose.Style.Add("visibility", "visible");
            pnlViewMail.Style.Add("visibility", "hidden");
            gvInbox.Style.Add("visibility", "hidden");
            grdSent.Style.Add("visibility", "hidden");
            grdView.Style.Add("visibility", "hidden");          
            pnlFolder.Style.Add("visibility", "hidden");

            ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowMail();", true);

     
    }
    protected void gvFF_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSf = (CheckBox)sender;
        GridViewRow row1 = (GridViewRow)chkSf.Parent.Parent;
        row1.Focus();
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        int count = 0;
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkSf");
            if (ChkBoxRows.Checked == true)
            {
                count++;
                lblSelectedCount.Text = "No.of Filed Force Selected : " + count;
            }
        }

        //FillColor();

        //FillgridColor();//Commented By Sridevi - as this is put in page load need not be put everywhere…
 
    }
    protected void chkId_OnCheckedChanged(object sender, EventArgs e)
    {
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlCompose.Style.Add("visibility", "hidden");
        pnlpopup.Style.Add("visibility", "hidden");        

        //CheckBox ChkBoxHeader = (CheckBox)gvInbox.HeaderRow.FindControl("cbSelectAll");
        foreach (GridViewRow row in gvInbox.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
            if (ChkBoxRows.Checked == true)            
            {              
                btnDelete.Enabled = true;
                btnDelete.ForeColor = System.Drawing.Color.Black;
                ddlMoved.Enabled = true;
                btnReply.Enabled = true;
                btnReply.ForeColor = System.Drawing.Color.Black;
                btnForward.Enabled = true;
                btnForward.ForeColor = System.Drawing.Color.Black;
            }
            
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowInbox();", true); 

    }
    private void DeleteEnable()
    {
        btnDelete.Enabled = true;
        btnDelete.ForeColor = System.Drawing.Color.Black;
    }

    private void DeleteDisable()
    {
        btnDelete.Enabled = false;
        btnDelete.ForeColor = System.Drawing.Color.Gray;
        btnReply.Enabled = false;
        btnReply.ForeColor = System.Drawing.Color.Gray;
        btnForward.Enabled = false;
        btnForward.ForeColor = System.Drawing.Color.Gray;
        ddlMoved.Enabled = false;
    }


    protected void gvInbox_RowDataBound(object sender, GridViewRowEventArgs e)
    {        
       if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblslNo = (Label)e.Row.FindControl("lblslNo");

            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
            //e.Row.Attributes.Add("style", "cursor:pointer;");
            e.Row.ToolTip = "Click Subject to view mail";           
           // e.Row.Attributes.Add("onclick", "location='Mail_Head.aspx?inbox_id=" + lblslNo.Text + "'");            
        }

       if (e.Row.RowType == DataControlRowType.DataRow)
       {
           AdminSetup adm = new AdminSetup();
           Label lblslNo = (Label)e.Row.FindControl("lblslNo");

           DataSet dsMailAttach = adm.getMailAttach(lblslNo.Text);
           string str = dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString();
           if (str == string.Empty)
           {
               e.Row.Cells[4].Visible = false;               
           }
       }
    }

    protected void grdClickFolder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
            pnlViewInbox.Style.Add("visibility", "hidden");
            pnlViewMailInbox.Style.Add("visibility", "hidden");
            pnlSent.Style.Add("visibility", "hidden");
            gvInbox.Style.Add("visibility", "hidden");
            Session["SentItem"] = null;
            Session["Viewed"] = null;
            Session["Inbox"] = null;
            PanelDisable();
            
            if (e.CommandName == "Folder")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                Image image = (Image)FindControl("imgFolder");
                string lectureId = lnkView.CommandArgument; 
                GetMovedFolder(lnkView.Text);
                //lnkView.BackColor = System.Drawing.Color.Gray;
                //image.ImageUrl = "../../images/Closed.ICO";
                //image.BackColor = System.Drawing.Color.Gray;
                //gvInbox.Visible = false;
                pnlFolder.Style.Add("visibility", "visible");
                pnlInbox.Style.Add("visibility", "visible");
                ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowFolder();", true);
            }

    }
    protected void grdClickFolder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblslNo = (Label)e.Row.FindControl("lblslNo");
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#ceedfc'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
            //e.Row.Attributes.Add("style", "cursor:pointer;");
            e.Row.ToolTip = "Click to view mail";            
        }
    }

    protected void gvInbox_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewMail")
        {
            int slno = Convert.ToInt16(e.CommandArgument);
            ViewState["inbox_id"] = slno.ToString();
            OpenPopup(slno.ToString());
        }
    }

    protected void grdFolder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewMail")
        {
            int slno = Convert.ToInt16(e.CommandArgument);
           // ViewState["inbox_id"] = slno.ToString();
            OpenPopup(slno.ToString());
        }
    }

    protected void grdView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewMail")
        {
            int slno = Convert.ToInt16(e.CommandArgument);
           // ViewState["inbox_id"] = slno.ToString();
            OpenPopup(slno.ToString());
        }
    }
    protected void grdSent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewMail")
        {
            int slno = Convert.ToInt16(e.CommandArgument);
          //  ViewState["inbox_id"] = slno.ToString();
            OpenPopup(slno.ToString());
        }
    }
 
    
    protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblslNo = (Label)e.Row.FindControl("lblslNo");
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");            
            //e.Row.Attributes.Add("style", "cursor:pointer;");
            e.Row.ToolTip = "Click Subject to view mail";
            //e.Row.Attributes.Add("onclick", "location='Mail_Head.aspx?inbox_id=" + lblslNo.Text + "'");
            //ddlMovedTo.Enabled = true;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            AdminSetup adm = new AdminSetup();
            Label lblslNo = (Label)e.Row.FindControl("lblslNo");

            DataSet dsMailAttach = adm.getMailAttach(lblslNo.Text);
            string str = dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString();
            if (str == string.Empty)
            {
                e.Row.Cells[4].Visible = false;
            }
        } 

    }

    protected void grdFF_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblBackColor = (Label)e.Row.FindControl("lblsf_color");
                Label lblSFType = (Label)e.Row.FindControl("lblsf_Type");

                if (lblBackColor != null)
                {
                    if (lblBackColor.Text == "-Level1")/*level 1*/
                    {
                        if (lblSFType.Text == "1")
                        {
                            e.Row.BackColor = System.Drawing.Color.FromName("#37C8FF");
                        }
                        else
                        {
                            e.Row.BackColor = System.Drawing.Color.FromName("#BADCF7");
                        }
                    }
                    else if (lblBackColor.Text == "-Level2")/*level 2*/
                    {
                        if (lblSFType.Text == "1")
                        {
                            e.Row.BackColor = System.Drawing.Color.FromName("#718FC7");
                        }
                        else
                        {
                            e.Row.BackColor = System.Drawing.Color.FromName("#ccffcc");
                        }
                    }
                    else if (lblBackColor.Text == "-Level3")/*level 3*/
                    {
                        if (lblSFType.Text == "1")
                        {
                            e.Row.BackColor = System.Drawing.Color.FromName("#e0ffff");
                        }
                        else
                        {
                            e.Row.BackColor = System.Drawing.Color.FromName("#ffffcc");
                        }
                    }
                    else if (lblBackColor.Text == "-Level4")/*level 4*/
                    {
                        if (lblSFType.Text == "1")
                        {
                            e.Row.BackColor = System.Drawing.Color.FromName("#fff0f5");
                        }
                        else
                        {
                            e.Row.BackColor = System.Drawing.Color.FromName("e0ffff");
                        }
                    }

                }
              }
            
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlMovedTo_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        AdminSetup adm = new AdminSetup();
        if (Request.QueryString["inbox_id"] != null)
        {
            //int iReturn = adm.ChangeMailFolder(sf_code, Convert.ToInt32(Request.QueryString["inbox_id"].ToString()),ddlMovedTo.SelectedItem.Text, 12);
        }

        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mail has been Moved successfully');</script>");
    }

    protected void ddlMoved_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        AdminSetup adm = new AdminSetup();
        foreach (GridViewRow row in gvInbox.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
            Label lblSlno = (Label)row.FindControl("lblslNo");
            if (ChkBoxRows.Checked == true)
            {
                int iReturn = adm.ChangeMailFolder(sf_code, Convert.ToInt16(lblSlno.Text),ddlMoved.SelectedItem.Text, 12);               
            }
            FillInbox();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowInbox();", true); 
        }

        foreach (GridViewRow row in grdSent.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
            Label lblslNo = (Label)row.FindControl("lblslNo");
            if (ChkBoxRows.Checked == true)
            {                
                int iReturn = adm.ChangeMailFolder(sf_code, Convert.ToInt16(lblslNo.Text), ddlMoved.SelectedItem.Text, 12); 
            }
            FillSentEMail();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "SentMail();", true);
        }

        foreach (GridViewRow row in grdView.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
            Label lblslNo = (Label)row.FindControl("lblslNo");
            if (ChkBoxRows.Checked == true)
            {                
                int iReturn = adm.ChangeMailFolder(sf_code, Convert.ToInt16(lblslNo.Text), ddlMoved.SelectedItem.Text, 12); 
            }
            FillViewedMail();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ViewedMail();", true);
        }

        foreach (GridViewRow row in grdFolder.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("cbChkId");
            Label lblslNo = (Label)row.FindControl("lblslNo");
            if (ChkBoxRows.Checked == true)
            {
                int iReturn = adm.ChangeMailFolder(sf_code, Convert.ToInt16(lblslNo.Text), ddlMoved.SelectedItem.Text, 12);
            }
            FillEMail();
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ViewedMail();", true);
        }
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlViewInbox.Style.Add("visibility", "hidden");
        
        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mail has been Moved successfully');</script>");
        
    }

    protected void ddlMon_OnSelectedIndex(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        PanelDisable();
        DropDown(sender, e);
       
    }

    protected void imgSearch_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        PanelDisable();
        DropDown(sender, e);        
    }

    protected void ddlYr_OnSelectedIndex(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        PanelDisable();
        DropDown(sender, e);
    }

    private void DropDown(object sender,EventArgs e)
    {
        if (Session["SentItem"] != null)
        {
            if (Session["SentItem"].ToString() == "SentItme")// Modified by Sridevi - To add ToString()
            {
               // btnSentItem_Click(sender, e);
                LoadSent();
            }
        }
        else if (Session["Viewed"] != null)
        {
            if (Session["Viewed"].ToString() == "Viewed")// Modified by Sridevi - To add ToString()
            {
               // btnView_Click(sender, e);
                LoadView();
            }
        }
        else if (Session["Inbox"] != null)
        {
            if (Session["Inbox"].ToString() == "Inbox")// Modified by Sridevi - To add ToString()
            {
                btnInbox_Click(sender, e);
            }
        }
    }

    protected void grdFolder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label lblslNo = (Label)e.Row.FindControl("lblslNo");
        //    e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00'");
        //    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
        //    e.Row.Attributes.Add("style", "cursor:pointer;");
        //    e.Row.ToolTip = "Click to view mail";
        //    e.Row.Attributes.Add("onclick", "location='Mail_Head.aspx?inbox_id=" + lblslNo.Text + "'");
        //    Label lblSF_Code = (Label)e.Row.FindControl("lblSF_Code");
        //    SalesForce sf = new SalesForce();
        //    DataSet dssf = sf.getSfName(lblSF_Code.Text);
        //    if (dssf.Tables[0].Rows.Count > 0)
        //    {
        //        lblSF_Code.Text = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
        //    }
        //    ddlMovedTo.Enabled = true;
        //}

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    AdminSetup adm = new AdminSetup();
        //    Label lblslNo = (Label)e.Row.FindControl("lblslNo");

        //    DataSet dsMailAttach = adm.getMailAttach(lblslNo.Text);
        //    string str = dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString();
        //    if (str == string.Empty)
        //    {
        //        e.Row.Cells[5].Visible = false;
        //    }
        //}  

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblslNo = (Label)e.Row.FindControl("lblslNo");
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
            e.Row.Attributes.Add("style", "cursor:pointer;");
            e.Row.ToolTip = "Click Subject to view mail";
          //  e.Row.Attributes.Add("onclick", "location='Mail_Head.aspx?inbox_id=" + lblslNo.Text + "'");
            //ddlMovedTo.Enabled = true;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            AdminSetup adm = new AdminSetup();
            Label lblslNo = (Label)e.Row.FindControl("lblslNo");

            DataSet dsMailAttach = adm.getMailAttach(lblslNo.Text);
            string str = dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString();
            if (str == string.Empty)
            {
                e.Row.Cells[4].Visible = false;
            }
        } 
    }

    protected void grdFoldercbSelected_OnCheckedChanged(object sender, EventArgs e)
    {
        pnlViewMail.Style.Add("visibility", "hidden");
        PanelDisable();
        CheckBox ChkBoxHeader = (CheckBox)grdFolder.HeaderRow.FindControl("cbSelectAll");
        foreach (GridViewRow row in grdFolder.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("cbChkId");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
                btnDelete.Enabled = true;
                btnForward.Enabled = true;
                btnDelete.ForeColor = System.Drawing.Color.Black;
                ddlMoved.Enabled = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
                btnDelete.Enabled = false;
                //btnForward.Enabled = false;
                btnDelete.ForeColor = System.Drawing.Color.Gray;

            }
        }
       
    }

    protected void grdFoldercbChkId_OnCheckedChanged(object sender, EventArgs e)
    {
        pnlViewMail.Style.Add("visibility", "hidden");
        PanelDisable();
        foreach (GridViewRow row in grdFolder.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("cbChkId");
            if (ChkBoxRows.Checked == true)
            {
                btnDelete.Enabled = true;
                btnDelete.ForeColor = System.Drawing.Color.Black;
                ddlMoved.Enabled = true;
                btnForward.Enabled = true;
                btnForward.ForeColor = System.Drawing.Color.Black;
                btnReply.Enabled = true;
                btnReply.ForeColor = System.Drawing.Color.Black;
                ddlMoved.Enabled = true;
            }

        }
    }
    protected void grdSent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblslNo = (Label)e.Row.FindControl("lblslNo");
            Label lblOpenMailId = (Label)e.Row.FindControl("lblslNo");
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFF00'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
            //e.Row.Attributes.Add("style", "cursor:pointer;");
            e.Row.ToolTip = "Click Subject to view mail";

         //   e.Row.Attributes.Add("onclick", "location='Mail_Head.aspx?inbox_id=" + lblslNo.Text + "'");
            Label lblSF_Code = (Label)e.Row.FindControl("lblSF_Code");
            SalesForce sf = new SalesForce();
            DataSet dssf = sf.getSfName(lblSF_Code.Text);
            if (dssf.Tables[0].Rows.Count > 0)
            {
                lblSF_Code.Text = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            }
            //ddlMovedTo.Enabled = true;
        }
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            AdminSetup adm = new AdminSetup();
            Label lblslNo = (Label)e.Row.FindControl("lblslNo");

            DataSet dsMailAttach = adm.getMailAttach(lblslNo.Text);
            string str = dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString();
            if (str == string.Empty)
            {
                e.Row.Cells[5].Visible = false;
            }
        }
       
    }

    private void OpenPopup(string sid)
    {
        //pnlSent.Style.Add("visibility", "visibility");        
        pnlViewInbox.Style.Add("visibility", "visible");
        pnlViewMailInbox.Style.Add("visibility", "visible");
        pnlInbox.Style.Add("visibility", "visible");
        ViewState["pnlViewInbox"] = "true";
        ViewState["pnlInbox"] = "true";
        AdminSetup ast = new AdminSetup();
        if (Request.QueryString["inbox_id"] != null)
        {
            dsFrom = ast.ViewMail(Convert.ToInt32(Request.QueryString["inbox_id"].ToString()));
        }
        else
        {
            dsFrom = ast.ViewMail(Convert.ToInt32(sid));
        }
        if (dsFrom.Tables[0].Rows.Count > 0)
        {
            //DivView.Visible = true;
            lblViewFrom.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            lblViewTo.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            lblViewCC.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            lblViewSub.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            //New chanages added by saravanan start 

            lblViewSub.Text = lblViewSub.Text.Replace("asdf", "'");
            lblViewSent.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            lblMailBody.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            lblMailBody.Text = lblMailBody.Text.Replace("asdf", "'");

            //New chanages added by saravanan End 
            ViewState["mail_sf_from"] = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            ViewState["strMail_To"] = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
            ViewState["strMail_CC"] = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
            //if (lblViewCC.Text != "")
            //{
            //    ViewState["mail_to_sf_NameCC"] = lblViewCC.Text;
            //}
            //ViewState["mail_to_sf_Name"] = lblViewFrom.Text;
            //ViewState["mail_to_sf_Name"] = lblViewTo.Text;
            strSF_Name = Session["sf_name"].ToString();

            AdminSetup adm = new AdminSetup();
            ViewState["inbox_id"] = sid;
            DataSet dsMailAttach = adm.getMailAttach(sid);
            if (dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString() != "")
            {
                aTagAttach.HRef = "~/" + dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString();
                string[] str = dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString().Split('/');
                lblViewAttach.Text = str[3];
                imgViewAttach.Visible = true;
            }
        }

        //txtboxSearch.Visible = false;

    }

    //protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gv1, "Select$" + e.Row.RowIndex);
    //        e.Row.ToolTip = "Click to select this row.";
    //    }
    //    //int iReturn = -1;
    //    //if (e.Row.RowType == DataControlRowType.DataRow)
    //    //{
    //    //    HiddenField mailid = (HiddenField)e.Row.FindControl("hdnslNo");
    //    //    AdminSetup ast = new AdminSetup();
    //    //    dsMail = ast.ViewMail(Convert.ToInt32(mailid.Value.ToString()));
    //    //    if (dsMail.Tables[0].Rows.Count > 0)
    //    //    {
    //    //        DivView.Visible = true;
    //    //        lblFrm.Text = dsMail.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //    //        lblTo.Text = dsMail.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
    //    //        lblCC.Text = ""; // dsMail.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //    //        lblSub.Text = dsMail.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
    //    //        lblSentDt.Text = dsMail.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
    //    //    }
    //    //}
    //}

    //protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    int i = Convert.ToInt32(e.CommandArgument);
    //    GridViewRow gvrow = gv1.Rows[i];
    //}

    protected void ddlFontName_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtMsg.Font.Name = ddlFontName.SelectedItem.Text;
        pnlCompose.Style.Add("visibility", "visible");
        pnlpopup.Style.Add("visibility", "hidden");
        ViewState["pnlCompose"] = "true";
    }

    protected void ddlFontSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtMsg.Font.Size = Convert.ToInt32(ddlFontSize.SelectedItem.Text);
        pnlCompose.Style.Add("visibility", "visible");
        ViewState["pnlCompose"] = "true";
    }

    protected void lnkAttach_Click(object sender, EventArgs e)
    {
        PnlAttachment.Style.Add("visibility", "visibility");
        pnlCompose.Style.Add("visibility", "visible");
        ViewState["PnlAttachment"] = "true";
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlpopup.Style.Add("visibility", "hidden");
        pnlCompose.Style.Add("visibility", "hidden");
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowMail();", true);
    }

    protected void lnkRemoveCC_Click(object sender, EventArgs e)
    {
        pnlCompose.Style.Add("visibility", "visible");
        ViewState["pnlCompose"] = "true";
        
        if (ViewState["cc"] != null)
        {
            if (ViewState["cc"].ToString() == "1")
            {
                ViewState["cc"] = "2";
                lnkRemoveCC.Text = "<span style='margin-left:10px'>" + "Add Cc" + "</span>";
                lnkRemoveCC.Style.Add("margin-left", "20px");
                TrCC.Visible = false;
            }
            else
            {
                ViewState["cc"] = "1";
                lnkRemoveCC.Text = "<span style='margin-left:10px'>" + "Remove Cc" + "</span>";
                
                TrCC.Visible = true;
            }
        }
        else
        {
            ViewState["cc"] = "1";
            lnkRemoveCC.Text = "<span style='margin-left:10px'>" + "Add Cc" + "</span>";          
            TrCC.Visible = false;
        }
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlpopup.Style.Add("visibility", "hidden");
        pnlCompose.Style.Add("visibility", "hidden");
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowMail();", true);
    }

    protected void lnkClear_Click(object sender, EventArgs e)
    {
        txtAddr.Text = "";
        txtAddr1.Text = "";
        txtAddr2.Text = "";
        txtMsg.Text = "";
        txtSub.Text = "";
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlpopup.Style.Add("visibility", "hidden");
        pnlCompose.Style.Add("visibility", "hidden");
        pnlViewMail.Style.Add("visibility", "hidden");
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowMail();", true);

    }
    protected void gvInbox_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInbox.PageIndex = e.NewPageIndex;
        FillInbox();
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlCompose.Style.Add("visibility", "hidden");
        // New Changes done by saravanan Start 
        pnlpopup.Style.Add("visibility", "hidden");
        // New Changes done by sararavanan End
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowInbox();", true);
    }
    protected void grdView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdView.PageIndex = e.NewPageIndex;
        FillViewedMail();
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlCompose.Style.Add("visibility", "hidden");
        // New Changes done by saravanan Start 
        pnlpopup.Style.Add("visibility", "hidden");
        // New Changes done by sararavanan End
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ViewedMail();", true); 
    }

    protected void lnkButton_Click(object sender, EventArgs e)
    {
        string DivCode = string.Empty;
        string to_sf_code = string.Empty;
        DataSet dsMailCompose = null;
        string cc_sf_code = string.Empty;
        string bcc_sf_code = string.Empty;
        string tobcc_sf_name=string.Empty;
        string toCC_sf_name=string.Empty;
        string Attachpath=string.Empty;

        string filename = lblAttachment.Text;
        //FileUpload2.FileName=FileUpload1.FileName;
        if (filename != "")
        {
            //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/MasterFiles/Mails/Attachment/" + filename));            
            Attachpath = "MasterFiles/Mails/Attachment/" + Session["strFileDateTime"].ToString() + filename;   // Modified by Sridevi - To add ToString()         
        }

        
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);            
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        string cur_sf_code = string.Empty;
        string cur_sf_level = string.Empty;
        Boolean blnMail = false;

        //foreach (ListItem item in chkFF.Items)
        //{
        //    if (item.Selected)
        //    {
        //        blnMail = true;
        //        cur_sf_code = item.Value.ToString().Substring(0, item.Value.ToString().IndexOf('-'));
        //        cur_sf_level = item.Value.ToString().Substring(item.Value.ToString().IndexOf('-') + 1, (item.Value.ToString().Length - cur_sf_code.Length) - 1);
        //        AdminSetup adm = new AdminSetup();
        //        dsMailCompose = adm.ComposeMail(sf_code, cur_sf_code, txtSub.Text.Trim(), txtMsg.Text.Trim(), "", txtAddr1.Text.Trim(), txtAddr2.Text.Trim(), div_code, Request.ServerVariables["REMOTE_ADDR"].ToString(), to_sf_code, "", "");
        //    }
        //}

        if (ViewState["mail_to_sf_code"] != null)
        {
            blnMail = true;

            if (ViewState["mail_cc_sf_code"] != null)
                cc_sf_code = ViewState["mail_cc_sf_code"].ToString();

            if (ViewState["mail_bcc_sf_code"] != null)
                bcc_sf_code = ViewState["mail_bcc_sf_code"].ToString();

            if(ViewState["mail_to_sf_NameBCC"]!=null)
                tobcc_sf_name=ViewState["mail_to_sf_NameBCC"].ToString();

            if(ViewState["mail_to_sf_NameCC"] !=null)
                toCC_sf_name=ViewState["mail_to_sf_NameCC"].ToString();

            string strSF_Name = "";
            if (sf_Type == "3")
            {
                strSF_Name = Session["Corporate"].ToString();
            }
            else
            {
                strSF_Name = Session["sf_name"].ToString();
            }

            AdminSetup adm = new AdminSetup();
            //dsMailCompose = adm.ComposeMail(sf_code, ViewState["mail_to_sf_code"].ToString(), txtSub.Text.Trim(), txtMsg.Text.Trim(), Attachpath, cc_sf_code, bcc_sf_code, div_code, Request.ServerVariables["REMOTE_ADDR"].ToString(), to_sf_code, "", "", Session["sf_name"].ToString(), ViewState["mail_to_sf_Name"].ToString());
            dsMailCompose = adm.ComposeMail(sf_code, ViewState["mail_to_sf_code"].ToString(), txtSub.Text.Trim(), txtMsg.Text.Trim(), Attachpath, cc_sf_code, bcc_sf_code, div_code, Request.ServerVariables["REMOTE_ADDR"].ToString(), toCC_sf_name, tobcc_sf_name, strSF_Name, ViewState["mail_to_sf_Name"].ToString());
            if (ViewState["inbox_id"] != null)
            {
                int iRet = adm.ChangeMailStatus(sf_code, Convert.ToInt32(ViewState["inbox_id"].ToString()), 10,"");
            }
        }

        if (blnMail)
        {
            txtAddr.Text = "";
            txtSub.Text = "";
            txtMsg.Text = "";
            txtAddr1.Text  = "";
            txtAddr2.Text = "";
            foreach (ListItem item in chkFF.Items)
            {
                item.Selected = false;
            }

            pnlViewInbox.Style.Add("visibility", "hidden");
            pnlViewMailInbox.Style.Add("visibility", "hidden");
            pnlInbox.Style.Add("visibility", "hidden");
            pnlCompose.Style.Add("visibility", "hidden");
            pnlpopup.Style.Add("visibility", "hidden");
            pnlSent.Style.Add("visibility", "hidden");
            pnlViewMail.Style.Add("visibility", "hidden");

            FillInbox();
            FillViewedMail();
            FillSentEMail();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowInbox();", true);
            btnInbox_Click(sender, e);
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mail has been sent successfully');</script>");
            
           
        }
    }

    protected void btnInbox_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        FillInbox();
        Session["Inbox"] = "Inbox";
        Session["SentItem"] = null;
        Session["Viewed"] = null;
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlpopup.Style.Add("visibility", "hidden");
        pnlCompose.Style.Add("visibility", "hidden");
        gvInbox.Style.Add("visibility", "visible");
        grdSent.Style.Add("visibility", "hidden");
        grdView.Style.Add("visibility", "hidden");          
        //btnInbox.Style.Add("background-color", "Gray");        
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowInbox();", true);        
    }
    protected void btnSentItem_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        txtboxSearch.Text = "";
        LoadSent();
       
    }
    private void LoadSent()
    {
        FillSentEMail();
        grdSent.Style.Add("visibility", "visible");//  Modified by Sridevi - 10-10-2015
        gvInbox.Style.Add("visibility", "hidden");
        grdView.Style.Add("visibility", "hidden");
        //grdSent.Attributes.Add("style", "display:block");
        Session["Inbox"] = null;
        Session["Viewed"] = null;
        Session["SentItem"] = "SentItme";
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlSent.Style.Add("visibility", "visible");
        pnlpopup.Style.Add("visibility", "hidden");
        pnlCompose.Style.Add("visibility", "hidden");
        pnlFolder.Style.Add("visibility", "hidden");


        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "SentMail();", true);
    }
    protected void grdSent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSent.PageIndex = e.NewPageIndex;
        FillSentEMail();

        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlCompose.Style.Add("visibility", "hidden");
        // New Changes done by saravanan Start 
        pnlpopup.Style.Add("visibility", "hidden");
        // New Changes done by sararavanan End
        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "SentMail();", true);
    }

    protected void btnDelete_Onclick(Object sender, EventArgs e)
    {
        try
        {
             AdminSetup adm = new AdminSetup();
             PanelDisable();             
             foreach (GridViewRow row in gvInbox.Rows)
             {
                 CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
                 Label lblslNo = (Label)row.FindControl("lblslNo");
                 if (ChkBoxRows.Checked == true)
                 {
                    
                     int iReturn = adm.ChangeMailStatus(sf_code, Convert.ToInt32(lblslNo.Text), 11,"");                     
                 }
                 FillInbox();
                 ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowInbox();", true);
             }
           
             foreach (GridViewRow row in grdSent.Rows)
             {
                 CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
                 Label lblslNo = (Label)row.FindControl("lblslNo");
                 if (ChkBoxRows.Checked == true)
                 {                     
                     int iReturn = adm.ChangeMailStatus(sf_code, Convert.ToInt32(lblslNo.Text), 11,"");                    
                 }
                 FillSentEMail();
                 ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "SentMail();", true);
             }

             foreach (GridViewRow row in grdView.Rows)
             {
                 CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkId");
                 Label lblslNo = (Label)row.FindControl("lblslNo");
                 if (ChkBoxRows.Checked == true)
                 {                     
                     int iReturn = adm.ChangeMailStatus(sf_code, Convert.ToInt32(lblslNo.Text), 11,"");
                 }
                 FillViewedMail();
                 ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ViewedMail();", true);
             }

             foreach (GridViewRow row in grdFolder.Rows)
             {
                 CheckBox ChkBoxRows = (CheckBox)row.FindControl("cbChkId");
                 Label lblslNo = (Label)row.FindControl("lblslNo");
                 if (ChkBoxRows.Checked == true)
                 {
                     int iReturn = adm.ChangeMailStatus(sf_code, Convert.ToInt32(lblslNo.Text), 11,"");
                 }
                 FillEMail();
                 //ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ViewedMail();", true);
             }


             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mail has been Deleted successfully');</script>");

        }
        catch (Exception ex)
        {

        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        txtboxSearch.Text = "";
        LoadView();
    }
    private void LoadView()
    {
        FillViewedMail();
        grdSent.Style.Add("visibility", "hidden");//  Modified by Sridevi - 10-10-2015
        gvInbox.Style.Add("visibility", "hidden");
        grdView.Style.Add("visibility", "visible");
        Session["Inbox"] = null;
        Session["SentItem"] = null;
        Session["Viewed"] = "Viewed";
        pnlViewInbox.Style.Add("visibility", "hidden");
        pnlViewMailInbox.Style.Add("visibility", "hidden");
        pnlpopup.Style.Add("visibility", "hidden");
        pnlCompose.Style.Add("visibility", "hidden");
        pnlViewMail.Style.Add("visibility", "visible");
        pnlFolder.Style.Add("visibility", "hidden");

        ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ViewedMail();", true);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        ListedDR LstDoc = new ListedDR();
        DCR dr = new DCR();
        AdminSetup adm = new AdminSetup();
        TourPlan tp = new TourPlan();

        DataSet dsDoc,dsDcr,dsAdmin,dsTP=new DataSet();

        dsDoc = LstDoc.getListedDr_RejectList(sf_code);
        dsDcr = dr.get_DCR_Rejected_Approval(sf_code);
        dsAdmin = adm.getLeave_Reject(sf_code, 1);
        dsTP = tp.get_TP_Rejected_Approval(sf_code);

        if (dsDoc.Tables[0].Rows.Count > 0 || dsDcr.Tables[0].Rows.Count > 0 || dsAdmin.Tables[0].Rows.Count > 0 || dsTP.Tables[0].Rows.Count > 0)
        {
           if (sf_Type == "1")
        {
            Response.Redirect("~/Default1.aspx");
        }
        else if (sf_Type == "2")
        {
            Response.Redirect("~/Default3.aspx");
        }
        else if (sf_Type =="3")
        {
            Session["div_code"] = div_code;
            Response.Redirect("~/Default2.aspx");
        }
        }
        else if (sf_Type == "1")
        {
            Response.Redirect("~/Default1.aspx");
        }
        else if (sf_Type == "2")
        {
            Response.Redirect("~/Default3.aspx");
        }
        else if (sf_Type =="3")
        {
            Session["div_code"] = div_code;
            Response.Redirect("~/Default2.aspx");
        }
       
    }

    protected void OnClick_ShCut(object sender, EventArgs e)
    {
        if (sf_code.StartsWith("MR"))
        {
            Response.Redirect("~/Default_MR.aspx");
        }
        else if (sf_code.StartsWith("MGR"))
        {
            Response.Redirect("~/MGR_Home.aspx");
        }
        else if (sf_Type =="3")
        {
            Session["div_code"] = div_code;
            Response.Redirect("~/BasicMaster.aspx");
        }
    }

    protected void txtMsg_TextChanged(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "LimtCharacters(this,5000,'lblCount');", true);
    }

    private void textBox1_Leave(object sender, System.EventArgs e)
    {

    }
   
    
}