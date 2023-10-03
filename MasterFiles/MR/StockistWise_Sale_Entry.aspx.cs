using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;


public partial class MasterFiles_MR_StockistWise_Sale_Entry : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsStockist = null;
    DataSet dsDivision = null;
    DataSet dsReport = null;
    DataSet dsProd = null;
    string stockist_code = string.Empty;
    string divcode = string.Empty;
    string SF_Name = string.Empty;
    string stockist_name = string.Empty;
    string[] stockistname;
    string sStockist = string.Empty;
    string stockist_Address = string.Empty;
    string stockist_ContactPerson = string.Empty;
    string stockist_Designation = string.Empty;
    string stockist_mobilno = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string sChkSalesforce = string.Empty;
    string ReportingMGR = string.Empty;
    int iIndex = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        stockist_code = Request.QueryString["Stockist_Code"];
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "LstDoctorList.aspx";
            menu1.Title = this.Page.Title;
            FillStockist_Name(divcode);
            FillStockist_StockistName();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
    }

    // Filter by Stockist_Name 
    private void FillStockist_Name(string divcode)
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getStockist_Name_SE();
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlStockist.DataTextField = "Stockist_Name";
          //ddlStockist.DataValueField = "Stockist_Code";
            ddlStockist.DataSource = dsStockist;
            ddlStockist.DataBind();
        }
    }
    private void FillStockist_StockistName()
    {
        string stockist_code = ddlStockist.SelectedValue.ToString();
        Stockist sk = new Stockist();
        dsStockist = sk.getStockistCreate_StockistName_SE();
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlStockist.Visible = true;
            ddlStockist.DataSource = dsStockist;
            ddlStockist.DataBind();
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        tblRate.Visible = true;
        FillProd();
    }
    private void FillProd()
    {
        Product dv = new Product();
        //dsProd = dv.getProdRate(ddlState.SelectedValue.ToString(), divcode);
        dsProd = dv.getProductRate(ddlYear.SelectedValue.ToString(), divcode);
        if (dsProd.Tables[0].Rows.Count > 0)
        {
            grdProdRate.Visible = true;
            grdProdRate.DataSource = dsProd;
            grdProdRate.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
    }
}