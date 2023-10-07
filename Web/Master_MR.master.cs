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
        try
         {
            Label1.Text = Session["Title_MR"].ToString() + "-" + " MR";
        }
        catch
        {
            Label1.Text = Session["sf_Name"].ToString() + "-" + " MR";
        }
     
    }
    
}
