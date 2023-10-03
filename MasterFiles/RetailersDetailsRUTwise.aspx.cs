using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;  

public partial class MasterFiles_RetailersDetailsRUTwise : System.Web.UI.Page
{

    #region "Declaration"
    string div_code = string.Empty;
    string Stockist_Code = string.Empty;
    public string sfCode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        Stockist_Code = Request.QueryString["StockistCode"].ToString();
        sfCode = Request.QueryString["SFcode"].ToString();
        Label3.Text = Request.QueryString["StockistName"].ToString();
        //hdsfName.Value = sf_name;
        loadData();

    }
    private void loadData()
    {
        SalesForce SF = new SalesForce();
       

            DataSet dsCounts = new DataSet();
            dsCounts = SF.RetailersDetailsRUTwise(Stockist_Code,div_code,sfCode);
            if (dsCounts.Tables.Count > 0)
            {
                RetailerGrd.DataSource = dsCounts;
                RetailerGrd.DataBind();
            }
        
    }

}