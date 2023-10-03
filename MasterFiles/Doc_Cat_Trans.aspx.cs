using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_Doc_Cat_Trans : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocCat = null;
    DataSet dsDocCategory = null;
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
            Session["backurl"] = "DoctorCategoryList.aspx";
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
        dsDocCat = dc.getDocCat_count(ddlTrans_From.SelectedValue);
        dsDocCategory = dc.getUnDoc_Count(ddlTrans_From.SelectedValue);

        if (dsDocCat.Tables[0].Rows.Count > 0 || dsDocCategory.Tables[0].Rows.Count > 0)
        {
            pnlCount.Visible = true;
            //  ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Listed Doctor Count: " + dsDocCat.Tables[0].Rows[0][0] + "\\n\\nUnListed Doctor Count: " + dsDocCategory.Tables[0].Rows[0][0] + " '),ConfirmMessage();</script>");
            lblDrcount.Text = dsDocCat.Tables[0].Rows[0][0].ToString();
            lblUndrcount.Text = dsDocCategory.Tables[0].Rows[0][0].ToString();

        }
    }

    
    private void FillTransfer_From()
    {
        Doctor dc = new Doctor();
        dsDocCat = dc.getDocCat_trans(div_code);
        if (dsDocCat.Tables[0].Rows.Count > 0)
        {
            ddlTrans_From.DataTextField = "Doc_Cat_SName";
            ddlTrans_From.DataValueField = "Doc_Cat_Code";
            ddlTrans_From.DataSource = dsDocCat;
            ddlTrans_From.DataBind();
        }
    }
    private void FillTransfer_To()
    {
        Doctor dc = new Doctor();
        dsDocCat = dc.getDocCat_Transfer(div_code, ddlTrans_From.SelectedItem.Text);
        if (dsDocCat.Tables[0].Rows.Count > 0)
        {
            ddlTrans_To.DataTextField = "Doc_Cat_SName";
            ddlTrans_To.DataValueField = "Doc_Cat_Code";
            ddlTrans_To.DataSource = dsDocCat;
            ddlTrans_To.DataBind();
        }
    }
    protected void ddlTrans_From_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillTransfer_To();
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
            Doctor dc = new Doctor();
            iReturn = dc.Update_DocCat_Drs(ddlTrans_From.SelectedValue, ddlTrans_To.SelectedValue,ddlTrans_From.SelectedItem.Text,ddlTrans_To.SelectedItem.Text, chkdel);
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