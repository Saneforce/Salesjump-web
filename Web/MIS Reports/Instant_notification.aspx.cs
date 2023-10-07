using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Bus_EReport;
using Newtonsoft.Json;
using System.Web.Services;
using DBase_EReport;
using System.Net;
public partial class MIS_Reports_Instant_notification : System.Web.UI.Page
{

    string sf_code = string.Empty;
    string div_code = string.Empty;
    string sf_Type = string.Empty;
    string sf_type = string.Empty;
    string HO_ID = string.Empty;
    DataSet dsSalesForce = null;
    SalesForce sf = new SalesForce();
    DataSet dsUserList = null;
    string sf_Name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    public static string trans_sl_no = string.Empty;
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();
        sf_Type = Session["sf_type"].ToString();
        div_code = Convert.ToString(Session["div_code"]);
        trans_sl_no = Request.QueryString["Trans_sl_no"];

        if (sf_Type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            sf_Type = Session["div_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            hdnslno.Value = trans_sl_no;
            FillAddressBook();
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.FindControl("btnBack").Visible = false;
            lblSelectedCount.Text = "No.of Fieldforce Selected : " + "<span style='color:red'>" + 0 + "</span>";
        }
        FillgridColor();

    }

    private void FillAddressBook()
    {
        FillDesignation();

        DataTable dt1 = new DataTable();
        if (Session["sf_type"].ToString() == "1")
        {

        }
        else
        {
            SalesForce sf = new SalesForce();
            if (Session["sf_code"].ToString() == "admin")
            {
                if (div_code.Contains(','))
                    div_code = div_code.Substring(0, div_code.Length - 1);
                dt1 = sf.getAddressBookWithoutAdmin(div_code, sf_code, 0);
            }
            else
            {
                if (div_code.Contains(','))
                    div_code = div_code.Substring(0, div_code.Length - 1);
                dt1 = sf.getAddressBookMgr(div_code, sf_code, 0);
            }
            if (dt1.Rows.Count > 0)
            {
                gvFF.DataSource = dt1;
                gvFF.DataBind();
            }
            else
            {
                gvFF.DataSource = dt1;
                gvFF.DataBind();
                lblSelectedCount.Text = "No.of Fieldforce Selected : " + "<span style='color:red'>" + 0 + "</span>";

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

    }

    protected void chkLevelAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkLevelAll.Checked)
        {
            foreach (ListItem item in chkDesgn.Items)
            {
                if (chkLevelAll.Checked == true)
                {
                    item.Selected = true;
                    //chkMR.Checked = true;
                }
            }
            foreach (GridViewRow row in gvFF.Rows)
            {

                CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
                chkSf.Checked = true;
                for (int i = 0; i < gvFF.Rows.Count; i++)
                {
                    int j = i + 1;
                    lblSelectedCount.Text = "No.of Fieldforce Selected : " + "<span style='color:red'>" + j + "</span>";
                }
            }

        }

        else
        {
            foreach (GridViewRow row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
                chkSf.Checked = false;
                lblSelectedCount.Text = "No.of Fieldforce Selected : " + "<span style='color:red'>" + 0 + "</span>";
            }
            foreach (ListItem items in chkDesgn.Items)
            {
                items.Selected = false;
                //chkMR.Checked = false;
            }
        }

    }
    //protected void chkMR_OnCheckChanged(object sender, EventArgs e)
    //{
    //    if (chkMR.Checked == false)
    //    {
    //        chkLevelAll.Checked = false;
    //    }
    //}

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
					chkLevelAll.Checked = false;
                }
            }
        }
		
		int list = chkDesgn.Items.Count;       
        int selCount = 0;
        for (int j = 0; j < chkDesgn.Items.Count; j++) {
            if (chkDesgn.Items[j].Selected)
                selCount++;
        }
        if (list == selCount)
        {
            chkLevelAll.Checked = true;
        }
        else
        {
            chkLevelAll.Checked = false;
        }
		
        int i = 0;
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            if (chkSf.Checked == true)
                i = i + 1;
        }
        lblSelectedCount.Text = "No.of Fieldforce Selected : " + "<span style='color:red'>" + i + "</span>";
    }

    private void FillUserList(string sLevel)
    {
        SalesForce sf = new SalesForce();
        dsUserList = sf.UserListMailAddress(div_code, "admin", sLevel);
        if (dsUserList.Tables[0].Rows.Count > 0)
        {
            gvFF.DataSource = dsUserList;
            gvFF.DataBind();
        }
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
            //chkMR.Checked = false;
            Label3.Visible = true;
            lblFF.Visible = false;

            foreach (ListItem item in chkDesgn.Items)
            {
                chkLevelAll.Checked = false;
                item.Selected = false;
            }
            foreach (GridViewRow row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
                chkSf.Checked = false;
                lblSelectedCount.Text = "No.of Fieldforce Selected : " + "<span style='color:red'>" + 0 + "</span>";
            }
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

            foreach (GridViewRow row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
                chkSf.Checked = false;
                lblSelectedCount.Text = "No.of Fieldforce Selected : " + "<span style='color:red'>" + 0 + "</span>";
            }
        }
        ddlFFType.SelectedValue = "2";
        ddlFieldForce.SelectedValue = "0";
        FillManagers();
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

    protected void gvFF_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSf = (CheckBox)sender;
        GridViewRow row1 = (GridViewRow)chkSf.Parent.Parent;
        row1.Focus();
        int count = 0;
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkSf");
            if (ChkBoxRows.Checked == true)
            {
                count++;
                lblSelectedCount.Text = "No.of Fieldforce Selected : " + "<span style='color:red'>" + count + "</span>";
            }
        }
    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFF.Text = "Field Force";
        FillManagers();
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

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkLevelAll.Checked = false;
        foreach (ListItem item in chkDesgn.Items)
        {
            item.Selected = false;
        }
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            chkSf.Checked = false;
        }
        if (ddlFieldForce.SelectedValue.ToString().Trim().Length > 0)
        {
            ViewState["dsSalesForce"] = null;
            ViewState["dsDoctor"] = null;

            FillSalesForce();
        }
        int i = 0;
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            if (chkSf.Checked == true)
                i = i + 1;
        }
        lblSelectedCount.Text = "No.of Fieldforce Selected : " + "<span style='color:red'>" + i + "</span>";

    }

    private void FillSalesForce()
    {
        DataTable dt = new DataTable();

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

    private void FillgridColor()
    {
        foreach (GridViewRow grid_row in gvFF.Rows)
        {
            Label lblBackColor = (Label)grid_row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            grid_row.BackColor = System.Drawing.Color.FromName(bcolor);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string Trans_Sl_No = string.Empty;
        string msg = string.Empty;
        string SF_Code = string.Empty;
        string SF_Codemsg = string.Empty;
        string SF_Name = string.Empty;
        string stdate = string.Empty;
        string Edate = string.Empty;
       

        int iReturn = -1;

        SaleForce sf = new SaleForce();
        //startdate=DateTime.Parse(txtstartdate.Text+' '+ DateTime.Now.TimeOfDay);
        //enddate = DateTime.Parse(txtEndDate.Text + ' ' + DateTime.Now.TimeOfDay);
        //stdate=startdate.ToString("yyyy-MM-dd hh:mm:ss");
       //Edate = enddate.ToString("yyyy-MM-dd hh:mm:ss");

        msg = txtmsg.Text;
        List<string> termsList = new List<string>();
        foreach (GridViewRow grid_row in gvFF.Rows)
        {
            Label lblsf_Code = (Label)grid_row.FindControl("lblsf_Code");
            CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
            Label lblSf_Name = (Label)grid_row.FindControl("lblSf_Name");

            if (chkSf.Checked)
            {
                SF_Codemsg = lblsf_Code.Text.ToString();
                SF_Code = SF_Code + lblsf_Code.Text.ToString() + ',';
                SF_Name = SF_Name + lblSf_Name.Text.ToString() + ',';
                termsList.Add(SF_Codemsg);
            }

        }
        string host = HttpContext.Current.Request.Url.Authority;
        if (termsList.Count > 0)
        {
            for (int i = 0; i < termsList.Count; i++)
            {

                var url = "http://www." + host + "/server/native_Db_V13.php?axn=web/notification_push&sfcode=" + termsList[i] + "&msg=" + msg + "&title=Web Notification&event_id=%23eventTODO";//Paste ur url here  

                WebRequest request = HttpWebRequest.Create(url);

                WebResponse response = request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream());

                string responseText = reader.ReadToEnd();
            }

            txtmsg.Text = txtmsg.Text.Replace("'", "~");
            if (trans_sl_no == "" || trans_sl_no == null)
            {
                iReturn = sf.AddNotification(div_code, SF_Code, SF_Name, txtmsg.Text); //from date getting insert as today date
            }
            else
            {
                iReturn = sf.Notification_update(SF_Code, SF_Name, txtmsg.Text,trans_sl_no, div_code);//from date getting updated as today date
            }
            if (iReturn > 0)
            {
                // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submitted Successfully');</script>");               
                msgbox.Value = "confirm";
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Submitted Successfully')", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Callfunction", "Confirm()", true);
                
            }

            foreach (GridViewRow row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
                chkSf.Checked = false;
                lblSelectedCount.Text = "No.of Fieldforce Selected : " + "<span style='color:red'>" + 0 + "</span>";
                txtmsg.Text = string.Empty;
            }
          
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select The Fieldforce');</script>");
        }

        
    }

    protected void lnk_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Options/Mob_App_Setting.aspx");

    }
    [WebMethod(EnableSession = true)]
    public static string fillData()
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        //SalesForce sf = new SalesForce();
        DataSet ds = new DataSet();
        if (trans_sl_no == "" || trans_sl_no == null)
        {
            ds = getNotification(Div_Code.Trim(','));
        }
        else
        {
            ds = getNotification(Div_Code.Trim(','), trans_sl_no);

        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getNotification(string div)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsSF = null;

        string strQry = "select SF_Code,SF_Name,Notification_Message,convert(varchar,Notification_Sent_Date,103) Notification_To_Date from Mas_ins_Notification where Division_Code='" + div + "' ";

        try
        {
            dsSF = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;

    }
    public static DataSet getNotification(string div, string trans_sl_no)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsSF = null;

        string strQry = "select SF_Code,SF_Name,Notification_Message,convert(varchar, Notification_Sent_Date, 103) Notification_To_Date from " +
                         " Mas_ins_Notification where Trans_Sl_No =" + trans_sl_no + " and Division_Code ='" + div + "'";

        try
        {
            dsSF = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;

    }
    public class SaleForce
    {
        public int AddNotification(string div_code, string SF_Code, string SF_Name, string Notification_Message)
        {
            int iReturn = -1;
            int Trans_Sl_No = -1;

            try
            {
                if (div_code.Contains(","))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                DB_EReporting db = new DB_EReporting();
                string strQry = "SELECT ISNULL(max(Trans_Sl_No),0)+1 FROM mas_ins_notification ";
                Trans_Sl_No = db.Exec_Scalar(strQry);

                string strQury = "INSERT INTO mas_ins_notification (Trans_Sl_No,Division_Code,SF_Code,SF_Name,Notification_Message,Notification_Sent_Date)" +
                         "values('" + Trans_Sl_No + "','" + div_code + "' , '" + SF_Code + "', '" + SF_Name + "' ,'" + Notification_Message + "' ,getdate())";

                //string strQury = "INSERT INTO Mas_Notification (Trans_Sl_No,Division_Code,SF_Code,SF_Name,Notification_Message,Notification_Sent_Date,Notification_From_Date,Notification_To_Date)" +
                //"values('" + Trans_Sl_No + "','" + div_code + "' , '" + SF_Code + "', '" + SF_Name + "' ,N'" + Notification_Message + "' ,getdate() , '" + "' ,getdate() , '" + "' ,'" + enddate + "')";

                iReturn = db.ExecQry(strQury);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int Notification_update(string SF_Code, string SF_Name, string Notification_Message, string trans_sl_no, string div)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();
           // string strQry = "Exec Notification_update '" + SF_Code + "','" + SF_Name + "' , '" + Notification_Message + "',getdate(),'" + trans_sl_no + "' ,'" + div + "'";
            string strQry = "delete from  Mas_AppNotifyList where DivCode='" + div + "' and trans_sl_no='" + trans_sl_no + "'  " +
                            "update mas_ins_notification set SF_Code='" + SF_Code + "' ,SF_Name='" + SF_Name + "',Notification_Message='" + Notification_Message + "',Notification_Sent_Date=getdate() " +
                            "where Trans_Sl_No = '" + trans_sl_no + "' and Division_Code = '" + div + "'";
            try
            {
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
    }
    [WebMethod(EnableSession = true)]
    public static string geturldata()
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string host = HttpContext.Current.Request.Url.Authority;
        return host;
    }

}
