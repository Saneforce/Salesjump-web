using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_BulkEditChemCat : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsChemCat = null;
    string div_code = string.Empty;
    int i;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "ChemistCategoryList.aspx";
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillChemCat();
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
    private void FillChemCat()
    {
        Chemist chem = new Chemist();
        dsChemCat = chem.getChemCat(div_code);
        if (dsChemCat.Tables[0].Rows.Count > 0)
        {
            grdChemCat.Visible = true;
            grdChemCat.DataSource = dsChemCat;
            grdChemCat.DataBind();
        }
        else
        {
            btnSubmit.Visible = false;
            grdChemCat.DataSource = dsChemCat;
            grdChemCat.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string chem_cat_code = string.Empty;
        string chem_cat_sname = string.Empty;
        string chem_cat_name = string.Empty;

        Chemist chem = new Chemist();

        int iReturn = -1;
        bool err = false;

        foreach (GridViewRow gridRow in grdChemCat.Rows)
        {
            Label lblChemCatCode = (Label)gridRow.Cells[1].FindControl("lblChemCatCode");
            chem_cat_code = lblChemCatCode.Text;
            TextBox txtChem_Cat_SName = (TextBox)gridRow.Cells[1].FindControl("txtChem_Cat_SName");
            chem_cat_sname = txtChem_Cat_SName.Text;
            TextBox txtChemCatName = (TextBox)gridRow.Cells[1].FindControl("txtChemCatName");
            chem_cat_name = txtChemCatName.Text;

            iReturn = chem.RecordUpdate_Chem_code(Convert.ToInt16(chem_cat_code), chem_cat_sname, chem_cat_name, div_code);

            if (iReturn > 0)
                err = false;

            if ((iReturn == -2))
            {
                txtChemCatName.Focus();
                err = true;
                break;
            }

            if ((iReturn == -3))
            {
                txtChem_Cat_SName.Focus();
                err = true;
                break;
            }
        }
        if (err == false)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ChemistCategoryList.aspx';</script>");
        }
        else if (err == true)
        {
            if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Name Already Exist');</script>");

            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
            }
        }
    }
}