using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Bus_EReport;

public partial class MasterFiles_RetailersDetailsSTKwise : System.Web.UI.Page
{
    #region "Declaration"
    string div_code = string.Empty;
  public  string sf_code = string.Empty;
    string sf_name = string.Empty;
    DataSet dsSalesForce = new DataSet();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["SFCode"].ToString();
        sf_name = Request.QueryString["SFName"].ToString();
        Label3.Text = sf_name;
        // hdsfName.Value = sf_name;
        loadData();
    }

        private void loadData()
        {
           SalesForce SF = new SalesForce();
         
            DataSet dsCounts = new DataSet();          
                dsCounts = SF.RetailersDetailsSTKwise(div_code,sf_code); 
            if (dsCounts.Tables.Count > 0)
            {                
                RetailerGrd.DataSource = dsCounts;
                RetailerGrd.DataBind();
            }
          
        }


    }
