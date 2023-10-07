using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_rpt_Uotlet_Placement_Achived : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string month = string.Empty;
    string year = string.Empty;
    DataTable dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["SF_Code"].ToString();
        sfname = Request.QueryString["SF_Name"].ToString();
        month = Request.QueryString["Month"].ToString();
        year = Request.QueryString["Year"].ToString();
        lblHead.Text = "Outlet Placement Target ";
        lblsf_name.Text = sfname;
        Loadmethod();
    }

    private void Loadmethod()
    {
        DataSet dsGV = new DataSet();
        DB_EReporting db_ER = new DB_EReporting();
        dsGV = db_ER.Exec_DataSet("exec sp_outletplcmnt_target '" + divcode + "','" + sfCode + "','" + month + "','" + year + "'");
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            dsGV.Tables[0].Columns.RemoveAt(0);
            dsGV.Tables[0].Columns.RemoveAt(1);
            gvtotalorder.DataSource = dsGV;
            dt = dsGV.Tables[0];
            gvtotalorder.DataBind();
        }
        else
        {
            gvtotalorder.DataSource = null;
            gvtotalorder.DataBind();
        }
    }
}