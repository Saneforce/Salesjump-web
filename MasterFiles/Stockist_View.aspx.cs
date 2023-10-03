using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Stockist_View : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsStockist = null;
    DataSet dsDivision = null;
    DataSet dsReport = null;
    string divcode = string.Empty;
    string stockist_code = string.Empty;
    string sf_code = string.Empty;   
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
      //  Session["backurl"] = "StockistList.aspx";
        divcode = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            
            FillStockist();
            FillSF_Alpha();
            menu1.Title = this.Page.Title;
           menu1.FindControl("btnBack").Visible = false;
          
        }

    }
    private void FillStockist()
    {        
        Stockist sk = new Stockist(); 
        dsStockist = sk. getStockist_View(divcode);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }
        else
        {
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }

    }
    protected void grdStockist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdStockist.PageIndex = e.NewPageIndex;
        FillStockist();

    }
    private void FillSF_Alpha()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getStockistview_Alphabet(divcode);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsStockist;
            dlAlpha.DataBind();
        }
    }
    private void FillStockist(string sAlpha)
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getStockistview_Alphabat(divcode, sAlpha);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }      
    }
    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        string sCmd = e.CommandArgument.ToString();
        if (sCmd == "All")
        {
            FillStockist();
        }
        else
        {
            FillStockist(sCmd);
        }
        //grdSalesForce.EditIndex = -1;
        //Fill the SalesForce Grid
        //FillSalesForce();
    }
}