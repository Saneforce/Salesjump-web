using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_ProductView : System.Web.UI.Page
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
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
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
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    } 
    private void FillProd()
    {
        Product dv = new Product();
        if (ddlSrch.SelectedValue == "1")
        {
            dsProd = dv.getProdall(div_code);
        }
        else if (ddlSrch.SelectedValue == "2" && val != "")
        {
            dsProd = dv.getProdforname(div_code, val);
        }
        else if (ddlSrch.SelectedValue == "3" && val != "")
        {
            dsProd = dv.getProdforcat(div_code, val);
        }
        else if (ddlSrch.SelectedValue == "4" && val != "")
        {
            dsProd = dv.getProdforgrp(div_code, val);
        }
        else if (ddlSrch.SelectedValue == "5" && val != "")
        {
            dsProd = dv.getProdforbrd(div_code, val);
        }
        else if (ddlSrch.SelectedValue == "6" && val != "")
        {
            dsProd = dv.getProdforSubdiv(div_code, val);
        }
        else if (ddlSrch.SelectedValue == "7" && val != "")
        {
            dsProd = dv.getProdforState(div_code, val);
        }

       
        else
        {
            dsProd = dv.getProdall(div_code);
        }

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
    private void FillProd(string sAlpha)
    {
        Product dv = new Product();
        dsProd = dv.getProd(div_code, sAlpha);
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
  
    // Sorting
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        Product dv = new Product();
        dtGrid = dv.getProductallList_DataTable(div_code);
        sCmd = Session["Char"].ToString();
        if (sCmd == "All")
        {
            dtGrid = dv.getProductallList_DataTable(div_code);
        }
        else if (sCmd != "")
        {

            dtGrid = dv.getProductlist_DataTable(div_code, sCmd);
        }
        else if (TxtSrch.Text != "")
        {
            dtGrid = dv.getDTProduct_Nam(ddlSrch.SelectedValue, TxtSrch.Text, Session["div_code"].ToString());
        }
        else if (ddlProCatGrp.SelectedIndex > 0)
        {
            search = ddlSrch.SelectedValue.ToString();

            if (search == "3")
            {
                dtGrid = dv.getDTProduct_Cat(div_code, ddlProCatGrp.SelectedValue);
            }
            else if (search == "4")
            {
                dtGrid = dv.getDTProduct_Grp(div_code, ddlProCatGrp.SelectedValue);
            }
            else if (search == "5")
            {
                dtGrid = dv.getDTProduct_Brd(div_code, ddlProCatGrp.SelectedValue);
            }
            else if (search == "6")
            {
                dtGrid = dv.getDTProduct_Sbdiv(div_code, ddlProCatGrp.SelectedValue);
            }
            else if (search == "7")
            {
                dtGrid = dv.getDTProduct_State(div_code, ddlProCatGrp.SelectedValue);
            }
            
        }
        return dtGrid;
    }

    protected void grdProduct_Sorting(object sender, GridViewSortEventArgs e)
    {

        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }
        DataView sortedView = new DataView(BindGridView());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        grdProduct.DataSource = sortedView;
        grdProduct.DataBind();

    }
  
    protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProCatGrp.Visible = true;
        int search = Convert.ToInt32(ddlSrch.SelectedValue);
        TxtSrch.Text = string.Empty;
        if (search == 2)
        {
            TxtSrch.Visible = true;
            Btnsrc.Visible = true;
            ddlProCatGrp.Visible = false;
        }
        else
        {
            TxtSrch.Visible = false;
            ddlProCatGrp.Visible = true;
            Btnsrc.Visible = true;
        }
        if (search == 1)
        {
            TxtSrch.Visible = false;
            ddlProCatGrp.Visible = false;
            Btnsrc.Visible = false;
            FillProd();

        }
        if (search == 3)
        {
            FillCategory();
        }
        if (search == 4)
        {
            FillGroup();
        }
        if (search == 5)
        {
            FillBrand();
        }
        if (search == 6)
        {
            FillSubdiv();
        }
        if (search == 7)
        {
            FillState(div_code);
        }
        val = "";
        FillProd();
    }
   
    private void FillCategory()
    {
        Product prd = new Product();
        dsProduct = prd.getProductCategory(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlProCatGrp.DataTextField = "Product_Cat_Name";
            ddlProCatGrp.DataValueField = "Product_Cat_Code";
            ddlProCatGrp.DataSource = dsProduct;
            ddlProCatGrp.DataBind();
        }
    }
    private void FillGroup()
    {
        Product prd = new Product();
        dsProduct = prd.getProductGroup(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlProCatGrp.DataTextField = "Product_Grp_Name";
            ddlProCatGrp.DataValueField = "Product_Grp_Code";
            ddlProCatGrp.DataSource = dsProduct;
            ddlProCatGrp.DataBind();
        }
    }

    private void FillBrand()
    {
        Product prd = new Product();
        dsProduct = prd.getProductBrand(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlProCatGrp.DataTextField = "Product_Brd_Name";
            ddlProCatGrp.DataValueField = "Product_Brd_Code";
            ddlProCatGrp.DataSource = dsProduct;
            ddlProCatGrp.DataBind();
        }
    }
    private void FillSubdiv()
    {
        Product prd = new Product();
        dsProduct = prd.getSubdiv(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlProCatGrp.DataTextField = "subdivision_name";
            ddlProCatGrp.DataValueField = "subdivision_code";
            ddlProCatGrp.DataSource = dsProduct;
            ddlProCatGrp.DataBind();
        }
    }
    //Changes done by Priya
    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getState(state_cd);
            ddlProCatGrp.DataTextField = "statename";
            ddlProCatGrp.DataValueField = "state_code";
            ddlProCatGrp.DataSource = dsState;
            ddlProCatGrp.DataBind();
        }
    }
    protected void ddlProCatGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        val = ddlProCatGrp.SelectedValue;
        FillProd();

    }
    protected void Btnsrc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["Char"] = string.Empty;
        grdProduct.PageIndex = 0;
        Search();

    }

    private void Search()
    {
        search = ddlSrch.SelectedValue.ToString();
        if (search == "2")
        {
            Product prd = new Product();
            // FindProduct(ddlSrch.SelectedValue, TxtSrch.Text, Session["div_code"].ToString());
            dsProduct = prd.FindProduct(ddlSrch.SelectedValue, TxtSrch.Text, Session["div_code"].ToString());
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                
                grdProduct.Visible = true;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
            }
            else
            {
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
              
            }
        }
    }
}