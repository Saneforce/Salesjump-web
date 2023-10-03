using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_Doc_Qua_Trans : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocQua = null;
    DataSet dsDocQual = null;
    string div_code = string.Empty;
    string Doc_QuaName = string.Empty;
    string Doc_QuaSName = string.Empty;
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
            Session["backurl"] = "DoctorQualificationList.aspx";
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

    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        Doctor dc = new Doctor();
        dsDocQua = dc.getlistQua_count(ddlTrans_From.SelectedValue);
        dsDocQual = dc.getUnlistQua_count(ddlTrans_From.SelectedValue);

        if (dsDocQua.Tables[0].Rows.Count > 0 || dsDocQual.Tables[0].Rows.Count > 0)
        {
            pnlCount.Visible = true;
            //  ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Listed Doctor Count: " + dsDocCat.Tables[0].Rows[0][0] + "\\n\\nUnListed Doctor Count: " + dsDocCategory.Tables[0].Rows[0][0] + " '),ConfirmMessage();</script>");
            lblDrcount.Text = dsDocQua.Tables[0].Rows[0][0].ToString();
            lblUndrcount.Text = dsDocQual.Tables[0].Rows[0][0].ToString();

        }
    }
    private void FillTransfer_From()
    {
        Doctor dc = new Doctor();
        dsDocQua = dc.getDocQua_Trans(div_code);
        if (dsDocQua.Tables[0].Rows.Count > 0)
        {
            ddlTrans_From.DataTextField = "Doc_QuaName";
            ddlTrans_From.DataValueField = "Doc_QuaCode";
            ddlTrans_From.DataSource = dsDocQua;
            ddlTrans_From.DataBind();
        }
    }
    private void FillTransfer_To()
    {
        Doctor dc = new Doctor();
        dsDocQua = dc.getQua_to(div_code, ddlTrans_From.SelectedItem.Text);
        if (dsDocQua.Tables[0].Rows.Count > 0)
        {
            ddlTrans_To.DataTextField = "Doc_QuaName";
            ddlTrans_To.DataValueField = "Doc_QuaCode";
            ddlTrans_To.DataSource = dsDocQua;
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
        iReturn = dc.Update_DocQua_Drs(ddlTrans_From.SelectedValue, ddlTrans_To.SelectedValue,ddlTrans_From.SelectedItem.Text, ddlTrans_To.SelectedItem.Text, chkdel);
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