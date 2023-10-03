using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_rpt_Rout_Names : System.Web.UI.Page
{
    DataSet dsProduct = null;
    DataSet dsProd = null;
    string sCmd = string.Empty;
    string div_code = string.Empty;
    string ProdBrdCode = string.Empty;
    string RouteCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string RouteName = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
    string Terr_Name = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        RouteCode = Request.QueryString["Route_Code"];
        RouteName = Request.QueryString["Route_Name"];
        Terr_Name = Request.QueryString["Terr_Hq_Name"];
        

        if (!Page.IsPostBack)
        {
            FillDis();

            lblProd.Text = "Retailer Details " + "<span style='color:red'> " + "(Route Name : " + RouteName + ")" + "</span";
           
        }
    }

    private void FillDis()
    {
        ListedDR ldr = new ListedDR();
        dsProd = ldr.getListedDr_new(Terr_Name, RouteCode);

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