using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Mail_Folder_Trans : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsMail = null;
    DataSet dsFolderMail = null;
    string div_code = string.Empty;
    string Doc_Cat_SName = string.Empty;
    string DocCatName = string.Empty;
    string Doc_Cat_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int transFrom = 0;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            ddlTrans_From.Focus();
            Session["backurl"] = "Mail_Folder_Creation.aspx";
            menu1.Title = this.Page.Title;
            FillTransfer_From();
            FillTransfer_To();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
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

    private void FillTransfer_From()
    {
        AdminSetup adm = new AdminSetup();
        dsMail = adm.getMail_TransFrom(div_code);
        if (dsMail.Tables[0].Rows.Count > 0)
        {
            ddlTrans_From.DataTextField = "Move_MailFolder_Name";
            ddlTrans_From.DataValueField = "Move_MailFolder_Id";
            ddlTrans_From.DataSource = dsMail;
            ddlTrans_From.DataBind();
        }

    }
    private void FillTransfer_To()
    {
        AdminSetup adm = new AdminSetup();
        dsMail = adm.getMail_TransTo(div_code ,ddlTrans_From.SelectedItem.Text);
        if (dsMail.Tables[0].Rows.Count > 0)
        {
            ddlTrans_To.DataTextField = "Move_MailFolder_Name";
            ddlTrans_To.DataValueField = "Move_MailFolder_Id";
            ddlTrans_To.DataSource = dsMail;
            ddlTrans_To.DataBind();

        }
    }
    protected void ddlTrans_From_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillTransfer_To();
    }
    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        AdminSetup adm = new AdminSetup();
        dsMail =adm.getMailcount(ddlTrans_From.SelectedValue);
        if (dsMail.Tables[0].Rows.Count > 0)
        {
            pnlCount.Visible = true;
            lblmovedcount.Text = dsMail.Tables[0].Rows[0][0].ToString();
        }

    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        if (txtconformmessageValue.Value == "Yes")
        {
            string chkdel = Chkdelete.Text;
            if (Chkdelete.Checked == true)
            {
                chkdel = "1";
            }
            else
            {
                chkdel = "0";
            }
            AdminSetup adm = new AdminSetup();
            iReturn = adm.Updatmail(ddlTrans_From.SelectedValue, ddlTrans_To.SelectedValue, ddlTrans_From.SelectedItem.Text, ddlTrans_To.SelectedItem.Text, chkdel,div_code);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Transfered Successfully');</script>");
                pnlCount.Visible = false;
                ddlTrans_From.SelectedIndex = -1;
                ddlTrans_To.SelectedIndex = -1;

            }
            FillTransfer_From();
            FillTransfer_To();
        }

    }
}