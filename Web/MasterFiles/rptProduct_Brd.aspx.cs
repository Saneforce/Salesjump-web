using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_rptProduct_Brd : System.Web.UI.Page
{
    DataSet dsProduct = null;
    DataSet dsProd = null;
    string sCmd = string.Empty;
    string div_code = string.Empty;
    string ProdBrdCode = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        ProdBrdCode = Request.QueryString["Product_Brd_Code"];
        ProdName = Request.QueryString["Product_Brd_Name"];

        if (!Page.IsPostBack)
        {
            FillProd();

            lblProd.Text = "Product Details " + "<span style='color:red'> " + "(Brand: " + ProdName + ")" + "</span";
        }
    }

    private void FillProd()
    {
        Product dv = new Product();
        dsProd = dv.getProdforbrd(div_code, ProdBrdCode);

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
}