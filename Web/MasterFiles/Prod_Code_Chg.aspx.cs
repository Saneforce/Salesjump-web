using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Prod_Code_Chg : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsProduct = null;
    DataSet dsProd = null;
    DataSet dsDivision = null;
    DataSet dsState = null;
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string sCmd = string.Empty;
    string div_code = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
 
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ProductList.aspx";
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            Session["Char"] = "All";
            FillProd();
            Session["backurl"] = "ProductList.aspx";
            menu1.Title = this.Page.Title;
            
        }
    }
    private void FillProd()
    {
        Product dv = new Product();
        dsProd = dv.getProdall(div_code);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
  
        string Prod_code = string.Empty;
        string Prod_ExistCode = string.Empty;
  
        Product dv = new Product();
        int iReturn = -1;
     
        foreach (GridViewRow gridRow in grdProduct.Rows)
        {
            Label ExistProdCode = (Label)gridRow.Cells[1].FindControl("lblProdCode");
            Prod_ExistCode = ExistProdCode.Text.ToString();
            TextBox Prodcode = (TextBox)gridRow.Cells[1].FindControl("txtProd");
            Prod_code = Prodcode.Text.ToString();
            if (Prod_code != "")
            {
               iReturn = dv.RecordUpdateProductCode(Prod_ExistCode, Prod_code, div_code);
                if (iReturn > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ProductList.aspx';</script>");
                }
                else if (iReturn == -2)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Code Already Exist');</script>");
                }
            }

        }
    }
}