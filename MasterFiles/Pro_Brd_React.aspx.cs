using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Pro_Brd_React : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsProBrd = null;
    int ProBrdCode = 0;
    string divcode = string.Empty;
    string Product_Brd_SName = string.Empty;
    string ProBrdName = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ProductBrandList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            FillProBrd();
            //menu1.Title = this.Page.Title;
        }
    }
    private void FillProBrd()
    {
        Product dv = new Product();
        dsProBrd = dv.getProBrd_React(divcode);
        if (dsProBrd.Tables[0].Rows.Count > 0)
        {
            grdProBrd.Visible = true;
            grdProBrd.DataSource = dsProBrd;
            grdProBrd.DataBind();
        }
        else
        {
            grdProBrd.DataSource = dsProBrd;
            grdProBrd.DataBind();
        }
    }
    protected void grdProBrd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            ProBrdCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate

            Product dv = new Product();
            int iReturn = dv.Brd_ReActivate(ProBrdCode);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Reactivate');</script>");
            }
            FillProBrd();
        }
    }
}