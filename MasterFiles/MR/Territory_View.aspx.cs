using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Collections;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_MR_RptAutoExpense : System.Web.UI.Page
{
    string sfcode = "";
    string divCode = "";
    Distance_calculation Exp = new Distance_calculation();
    protected void Page_Load(object sender, EventArgs e)
    {

        sfcode = Request.QueryString["sfCode"].ToString();
        divCode = Request.QueryString["divCode"].ToString();
        DataTable ds = Exp.getFieldForce(divCode, sfcode);
        hqId.InnerText = ds.Rows[0]["sf_hq"].ToString();
        DataTable t1 = Exp.getTerritory(divCode, sfcode);
        if (t1.Rows.Count > 0)
        {

            grdExpMain.Visible = true;
            grdExpMain.DataSource = t1;
            grdExpMain.DataBind();

        }
        else
        {
            grdExpMain.DataSource = t1;
            grdExpMain.DataBind();
        }
      
    }
 
}