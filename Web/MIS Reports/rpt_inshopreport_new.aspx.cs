using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_rpt_inshopreport_new : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string month = string.Empty;
    string year = string.Empty;
    string subdiv_code = string.Empty;
    DataTable dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["SFCode"].ToString();
        sfname = Request.QueryString["SFName"].ToString();
        month = Request.QueryString["FYear"].ToString();
        year = Request.QueryString["FMonth"].ToString();
        subdiv_code = Request.QueryString["SubDiv"].ToString();
        lblHead.Text = "Inshop Sale Report";
        lblsf_name.Text = sfname;
        Fillsummary();

    }
    private void Fillsummary()
    {

        DataSet dsGV = new DataSet();
        DataSet dsGc = new DataSet();
        DB_EReporting db_ER = new DB_EReporting();
        //test dc = new test();

        dsGV = db_ER.Exec_DataSet("exec sp_inshop_rpt '" + divcode + "','" + sfCode + "','" + month + "','" + year + "'");
        if (dsGV.Tables[0].Rows.Count > 0)
        {
            dsGV.Tables[0].Columns.RemoveAt(0);
            dsGV.Tables[0].Columns.RemoveAt(2);
            dsGV.Tables[0].Columns.RemoveAt(3);
            
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