using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_Doc_Spec_Trans : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocSpe = null;
    DataSet dsDocSpec = null;

    string div_code = string.Empty;
    string Doc_Special_SName = string.Empty;
    string Doc_Special_Name = string.Empty;
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
            Session["backurl"] = "DoctorSpecialityList.aspx";
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
        Doctor dc = new Doctor();
        dsDocSpe = dc.getDocSpe_Trans(div_code);
        if (dsDocSpe.Tables[0].Rows.Count > 0)
        {
            ddlTrans_From.DataTextField = "Doc_Special_SName";
            ddlTrans_From.DataValueField = "Doc_Special_Code";
            ddlTrans_From.DataSource = dsDocSpe;
            ddlTrans_From.DataBind();
        }

    }
    private void FillTransfer_To()
    {
        Doctor dc = new Doctor();
        dsDocSpe = dc.getSpec_to(div_code, ddlTrans_From.SelectedItem.Text);
        if (dsDocSpe.Tables[0].Rows.Count > 0)
        {
            ddlTrans_To.DataTextField = "Doc_Special_SName";
            ddlTrans_To.DataValueField = "Doc_Special_Code";
            ddlTrans_To.DataSource = dsDocSpe;
            ddlTrans_To.DataBind();
        }
    }


    protected void btnTransfer_Click(object sender, EventArgs e)
    {
       
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        Doctor dc = new Doctor();
        dsDocSpe = dc.getlistSpec_count(ddlTrans_From.SelectedValue);
        dsDocSpec = dc.getUnlistSpec_count(ddlTrans_From.SelectedValue);

        if (dsDocSpe.Tables[0].Rows.Count > 0 || dsDocSpec.Tables[0].Rows.Count > 0)
        {
            pnlCount.Visible = true;
            //  ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Listed Doctor Count: " + dsDocCat.Tables[0].Rows[0][0] + "\\n\\nUnListed Doctor Count: " + dsDocCategory.Tables[0].Rows[0][0] + " '),ConfirmMessage();</script>");
            lblDrcount.Text = dsDocSpe.Tables[0].Rows[0][0].ToString();
            lblUndrcount.Text = dsDocSpec.Tables[0].Rows[0][0].ToString();

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
        iReturn = dc.Update_DocSpec_Drs(ddlTrans_From.SelectedValue, ddlTrans_To.SelectedValue,ddlTrans_From.SelectedItem.Text,ddlTrans_To.SelectedItem.Text, chkdel);
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