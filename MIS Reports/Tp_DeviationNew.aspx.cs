using Bus_EReport;
using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_Tp_DeviationNew : System.Web.UI.Page
{
    string sf_type = string.Empty;
    DataSet dsSalesForce = null;
    public static string div_code = string.Empty;
    string sf_code = string.Empty;
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillMRManagers("0");
        }
    }
    private void FillMRManagers(string Sub_Div_Code)
    {
        //deviation sf = new deviation();
        //dsSalesForce = sf.MgrGet_MgrOnly(div_code, "0");
		SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code, Sub_Div_Code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, "---Select Field Force---");

        }
    }
    [WebMethod(EnableSession = true)]
    public static string getdata(string SF_Code, string date)
    {
        deviation SFD = new deviation();
        DataSet ds = SFD.getdeviation(SF_Code, date, div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public class deviation
    {

        public DataSet MgrGet_MgrOnly(string div, string sub="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            string strQry = "exec get_SubDivMagDetails1 '" + div + "','" + sub + "'";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet getdeviation(string sfcode, string dt,string div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            string strQry = "exec sp_tpDeviation_daywise '" + sfcode + "','"+ div + "','" + dt + "'";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
    }

}