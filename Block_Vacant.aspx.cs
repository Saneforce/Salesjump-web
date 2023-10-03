using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class Block_Vacant : System.Web.UI.Page
{
    DataSet dsAdmin = null;
    string division_code = string.Empty;
    string sf_type = string.Empty;
    string sf_code = string.Empty;
	public static string type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
		type =  Request.QueryString["type"];
        if (sf_type == "3")
        {
            division_code = Session["division_code"].ToString();
        }
        else
        {
            division_code = Session["div_code"].ToString() + ',';
        }
        sf_code = Session["sf_code"].ToString();
        AdminSetup adm = new AdminSetup();
        dsAdmin = adm.Get_Block_Reason(division_code, sf_code);

        if (dsAdmin.Tables[0].Rows.Count > 0)
        {
            lblreason.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Index.aspx");
    }
}