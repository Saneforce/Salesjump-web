using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Chem_Cat_Trans : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsChemCat = null;
    DataSet dsChemCategory = null;
    string div_code = string.Empty;
    string Chem_Cat_SName = string.Empty;
    string ChemCatName = string.Empty;
    string Chem_Cat_code = string.Empty;
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
            Session["backurl"] = "ChemistCategoryList.aspx";
            menu1.Title = this.Page.Title;
            FillTransfer_From();
            FillTransfer_To();
            ServerStartTime = DateTime.Now;
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
        Chemist chem = new Chemist();
        dsChemCat = chem.getChemCat_trans(div_code);
        if (dsChemCat.Tables[0].Rows.Count > 0)
        {
            ddlTrans_From.DataTextField = "Chem_Cat_SName";
            ddlTrans_From.DataValueField = "Cat_Code";
            ddlTrans_From.DataSource = dsChemCat;
            ddlTrans_From.DataBind();
        }

    }

    private void FillTransfer_To()
    {
        Chemist chem = new Chemist();
        dsChemCat = chem.getChemCat_Transfer(div_code, ddlTrans_From.SelectedItem.Text);
        if(dsChemCat.Tables[0].Rows.Count > 0)
        {
            ddlTrans_To.DataTextField = "Chem_Cat_SName";
            ddlTrans_To.DataValueField = "Cat_Code";
            ddlTrans_To.DataSource = dsChemCat;
            ddlTrans_To.DataBind();
        }
    }

    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        Chemist chem = new Chemist();
        dsChemCat = chem.getChemCat_count(ddlTrans_From.SelectedValue);
        if (dsChemCat.Tables[0].Rows.Count > 0)
        {
            pnlCount.Visible = true;
            lblDrcount.Text = dsChemCat.Tables[0].Rows[0][0].ToString();
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

            Chemist chem = new Chemist();
            iReturn = chem.Update_ChemCat_Chemists(ddlTrans_From.SelectedValue, ddlTrans_To.SelectedValue , chkdel);

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