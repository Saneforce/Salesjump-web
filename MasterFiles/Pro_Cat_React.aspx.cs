using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Pro_Cat_React : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsProCat = null;
    int ProCatCode = 0;
    string divcode = string.Empty;
    string Product_Cat_SName = string.Empty;
    string ProCatName = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ProductCategoryList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {           
            FillProCat();
            //menu1.Title = this.Page.Title;
        }

    }
    private void FillProCat()
    {
        Product dv = new Product();
        dsProCat = dv.getProCat_React(divcode);
        if (dsProCat.Tables[0].Rows.Count > 0)
        {
            grdProCat.Visible = true;
            grdProCat.DataSource = dsProCat;
            grdProCat.DataBind();
        }
        else
        {
            grdProCat.DataSource = dsProCat;
            grdProCat.DataBind();
        }
    }
    protected void grdProCat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            ProCatCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Product dv = new Product();
            int iReturn = dv.ReActivate(ProCatCode);
            if (iReturn > 0)
            {
                //menu1.Status = "Product Category has been Reactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Reactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Reactivate');</script>");
            }
            FillProCat();
        }
    }
}