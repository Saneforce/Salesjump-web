using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_Product_Reactivate : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsProd = null;
    string div_code = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "ProductList.aspx";
            FillProd();
            //menu1.Title = this.Page.Title;
           
        }
    }
    private void FillProd()
    {
        Product dv = new Product();
        dsProd = dv.getProd_Re(div_code);
        if (dsProd.Tables[0].Rows.Count > 0)
        {
            grdProduct.Visible = true;
            grdProduct.DataSource = dsProd;
            grdProduct.DataBind();
        }
        else
        {
            grdProduct.DataSource = dsProd;
            grdProduct.DataBind();
        }
    }
    protected void grdProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            ProdCode = Convert.ToString(e.CommandArgument);

            //Deactivate
            Product dv = new Product();
            int iReturn = dv.ReActivate(ProdCode);
            if (iReturn > 0)
            {
                //menu1.Status = "Product has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Reactivate');</script>");
            }
            FillProd();
        }
    }
    protected void grdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdProduct.PageIndex = e.NewPageIndex;
        FillProd();
    }
}