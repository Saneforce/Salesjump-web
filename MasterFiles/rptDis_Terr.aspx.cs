using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_rptDis_Terr : System.Web.UI.Page
{
    DataSet dsProduct = null;
    DataSet dsProd = null;
    string sCmd = string.Empty;
    string div_code = string.Empty;
    string ProdBrdCode = string.Empty;
    string DisCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string DisName = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        DisCode = Request.QueryString["Stockist_Code"];
        DisName = Request.QueryString["Stockist_Name"];

        if (!Page.IsPostBack)
        {
            FillDis();

            lblProd.Text = "Route Details " + "<span style='color:red'> " + "(Distributor : " + DisName + ")" + "</span";
        }
    }

    private void FillDis()
    {
        Product dv = new Product();
        dsProd = dv.getRou_Detail(div_code, DisCode);

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