using Bus_EReport;
using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_Merchand_summary_new : System.Web.UI.Page
{
    string sf_type = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsTP = null;
    DataSet dsSalesForce = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        fillsubdivision();
        fillfieldforce();
    }
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    private void fillfieldforce()
    {
        loclass sd = new loclass();
        dsSalesForce = sd.Getstockistwise(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlstockist.DataTextField = "User_Name";
            ddlstockist.DataValueField = "sf_code";
            ddlstockist.DataSource = dsSalesForce;
            ddlstockist.DataBind();
            ddlstockist.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlstockist.Items.Insert(1, new ListItem("admin", "admin"));

        }
    }
    public class loclass
    {
        public DataSet Getstockistwise(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            string strQry = "select Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as User_Name,sf_code from mas_salesforce where sf_status=0 and division_code='"+ divcode + ",' order by sf_name";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
    }
}