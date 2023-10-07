using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;

public partial class Master : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = Session["Title_DIS"].ToString();
        Label2.Text = Session["UserName"].ToString();
    }
}
