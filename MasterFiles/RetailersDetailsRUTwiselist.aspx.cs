using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using Bus_EReport;

public partial class MasterFiles_RetailersDetailsRUTwiselist : System.Web.UI.Page
{
    #region "Declaration"
    string div_code = string.Empty;
     public string territorycode = string.Empty;
    public string sfCode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Request.QueryString["SFcode"].ToString();
        territorycode = Request.QueryString["TerritoryCode"].ToString();
        Label3.Text = Request.QueryString["TerritoryName"].ToString(); 
        loadData();
    }
    private void loadData()
    {
        SalesForce SF = new SalesForce();
        
            DataSet dsCounts = new DataSet();
            dsCounts = SF.RetailersDetailsRUTwiselist(territorycode, div_code);
            if (dsCounts.Tables.Count > 0)
            {
                RetailerGrd.DataSource = dsCounts;
                RetailerGrd.DataBind();
            }
        
    }
}