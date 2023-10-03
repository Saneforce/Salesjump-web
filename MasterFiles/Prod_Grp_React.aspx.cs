using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Prod_Grp_React : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsProGrp = null;
    int ProGrpCode = 0;
    string divcode = string.Empty;
    string Product_Grp_SName = string.Empty;
    string ProGrpName = string.Empty;
    string Product_Grp_Code = string.Empty;
    string div_code = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ProductGroupList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            FillProGrp();
        }

    }
    private void FillProGrp()
    {
        Product dv = new Product();
        dsProGrp = dv.getProGrp_React(divcode);
        if (dsProGrp.Tables[0].Rows.Count > 0)
        {
            grdProGrp.Visible = true;
            grdProGrp.DataSource = dsProGrp;
            grdProGrp.DataBind();
        }
        else
        {
            grdProGrp.DataSource = dsProGrp;
            grdProGrp.DataBind();
        }
    }
    protected void grdProGrp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            ProGrpCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Product dv = new Product();
            int iReturn = dv.ReActivateGrp(ProGrpCode);
            if (iReturn > 0)
            {
                //menu1.Status = "Product Group has been Reactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Reactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Reactivate');</script>");
            }
            FillProGrp();
        }
    }
}