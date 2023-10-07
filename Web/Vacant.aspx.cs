using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class Vacant : System.Web.UI.Page
{
    DataSet dsAdmin = null;
    string division_code = string.Empty;
    string sf_type = string.Empty;
    string sf_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
       
     
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Index.aspx");
    }
}