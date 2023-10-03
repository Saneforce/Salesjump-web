using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Chem_Cat_React : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsChemCat = null;
    int ChemCatCode = 0;
    string divcode = string.Empty;
    string Chem_Cat_SName = string.Empty;
    string ChemCatName = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "ChemistCategoryList.aspx";
        if (!Page.IsPostBack)
        {
            FillChemCat();
            menu1.Title = this.Page.Title;

        }
    }
    private void FillChemCat()
    {

        Chemist chem = new Chemist();
        dsChemCat = chem.getChemCat_Re(divcode);
        if (dsChemCat.Tables[0].Rows.Count > 0)
        {
            grdChemCat.Visible = true;
            grdChemCat.DataSource = dsChemCat;
            grdChemCat.DataBind();
        }
        else
        {

            grdChemCat.DataSource = dsChemCat;
            grdChemCat.DataBind();
        }
    }
    protected void grdChemCat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            ChemCatCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Chemist chem = new Chemist();
            int iReturn = chem.ReActivate_Chemcat(ChemCatCode);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Reactivate');</script>");
            }
            FillChemCat();
        }
    }
    protected void grdChemCat_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdChemCat.PageIndex = e.NewPageIndex;
        FillChemCat();
    }
}