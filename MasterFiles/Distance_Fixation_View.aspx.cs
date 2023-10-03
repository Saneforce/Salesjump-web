using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MasterFiles_Allowance_Fixation_View : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sfCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sfCode = Session["sf_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
                     
        }
    }
}