using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Doc_Cat_React : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocCat = null;
    int DocCatCode = 0;
    string divcode = string.Empty;
    string Doc_Cat_SName = string.Empty;
    string DocCatName = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "DoctorCategoryList.aspx";
        if (!Page.IsPostBack)
        {
            FillDocCat();
            menu1.Title = this.Page.Title;
           
        }
    }
    private void FillDocCat()
    {
        Doctor dv = new Doctor();
        dsDocCat = dv.getDocCat_Re(divcode);
        if (dsDocCat.Tables[0].Rows.Count > 0)
        {
            grdDocCat.Visible = true;
            grdDocCat.DataSource = dsDocCat;
            grdDocCat.DataBind();
        }
        else
        {
           
            grdDocCat.DataSource = dsDocCat;
            grdDocCat.DataBind();
        }
    }
    protected void grdDocCat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            DocCatCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Doctor dv = new Doctor();
            int iReturn = dv.ReActivate(DocCatCode);
            if (iReturn > 0)
            {
                //menu1.Status = "Doctor Category has been Reactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Reactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Reactivate');</script>");
            }
            FillDocCat();
        }
    }
    protected void grdDocCat_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDocCat.PageIndex = e.NewPageIndex;
        FillDocCat();
    }
}