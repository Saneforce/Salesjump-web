using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_ChemistCategory : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsChemCat = null;
    string divcode = string.Empty;
    string Chem_Cat_SName = string.Empty;
    string ChemCatName = string.Empty;
    string Chem_Cat_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ChemistCategoryList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        Chem_Cat_Code = Request.QueryString["Cat_Code"];
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            menu1.FindControl("btnBack").Visible = true;

            if (Chem_Cat_Code != "" && Chem_Cat_Code != null)
            {
                Chemist chem = new Chemist();
                dsChemCat = chem.getChemCat_code(divcode, Chem_Cat_Code);
                if (dsChemCat.Tables[0].Rows.Count > 0)
                {
                    txtChem_Cat_SName.Text = dsChemCat.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtChemCatName.Text = dsChemCat.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }
            }
        }
        txtChem_Cat_SName.Focus();

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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Chemist chem = new Chemist();
        Chem_Cat_SName = txtChem_Cat_SName.Text;
        ChemCatName = txtChemCatName.Text;

        if (Chem_Cat_Code == null)
        {
            int iReturn = chem.RecordAdd_chem(divcode, Chem_Cat_SName, ChemCatName);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Resetall();
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
                txtChemCatName.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
                txtChem_Cat_SName.Focus();
            }
        }
        else
        {
            int ChemCatCode = Convert.ToInt16(Chem_Cat_Code);
            int iReturn = chem.RecordUpdate_Chem_code(ChemCatCode, Chem_Cat_SName, ChemCatName, divcode);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ChemistCategoryList.aspx';</script>");
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
                txtChemCatName.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
                txtChem_Cat_SName.Focus();
            }
        }
    }
    private void Resetall()
    {
        txtChem_Cat_SName.Text = "";
        txtChemCatName.Text = "";
    }
}