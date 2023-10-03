using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_Doc_Class_Trans : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocCls = null;
    DataSet dsDocClass = null;
    string div_code = string.Empty;
    string Doc_ClsSName = string.Empty;
    string Doc_ClsName = string.Empty;
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
            Session["backurl"] = "DoctorClassList.aspx";
            menu1.Title = this.Page.Title;
            FillTransfer_From();
            FillTransfer_To();
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
    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        Doctor dc = new Doctor();
        dsDocCls = dc.getlistCls_count(ddlTrans_From.SelectedValue);
        dsDocClass = dc.getUnlistCls_count(ddlTrans_From.SelectedValue);

        if (dsDocCls.Tables[0].Rows.Count > 0 || dsDocClass.Tables[0].Rows.Count > 0)
        {
            pnlCount.Visible = true;
            //  ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Listed Doctor Count: " + dsDocCat.Tables[0].Rows[0][0] + "\\n\\nUnListed Doctor Count: " + dsDocCategory.Tables[0].Rows[0][0] + " '),ConfirmMessage();</script>");
            lblDrcount.Text = dsDocCls.Tables[0].Rows[0][0].ToString();
            lblUndrcount.Text = dsDocClass.Tables[0].Rows[0][0].ToString();

        }
    }
    private void FillTransfer_From()
    {
        Doctor dc = new Doctor();
        dsDocCls = dc.getDocCls_Trans(div_code);
        if (dsDocCls.Tables[0].Rows.Count > 0)
        {
            ddlTrans_From.DataTextField = "Doc_ClsSName";
            ddlTrans_From.DataValueField = "Doc_ClsCode";
            ddlTrans_From.DataSource = dsDocCls;
            ddlTrans_From.DataBind();
        }
    }
    private void FillTransfer_To()
    {
        Doctor dc = new Doctor();
        dsDocCls = dc.getCls_to(div_code, ddlTrans_From.SelectedItem.Text);
        if (dsDocCls.Tables[0].Rows.Count > 0)
        {
            ddlTrans_To.DataTextField = "Doc_ClsSName";
            ddlTrans_To.DataValueField = "Doc_ClsCode";
            ddlTrans_To.DataSource = dsDocCls;
            ddlTrans_To.DataBind();
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        string chkdel = Chkdelete.Text;
        if (Chkdelete.Checked == true)
        {
            chkdel = "1";
        }
        else
        {
            chkdel = "0";
        }
        Doctor dc = new Doctor();
        iReturn = dc.Update_DocClass_Drs(ddlTrans_From.SelectedValue, ddlTrans_To.SelectedValue,ddlTrans_From.SelectedItem.Text, ddlTrans_To.SelectedItem.Text, chkdel);
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
    protected void ddlTrans_From_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillTransfer_To();
    }
}