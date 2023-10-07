using Bus_EReport;
using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_Retailerwise_distsales : System.Web.UI.Page
{
    string sf_type = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsTP = null;
    DataSet dsSalesForce = null;
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
        sf_type = Session["sf_type"].ToString();
        if (!Page.IsPostBack)
        {
            //TourPlan tp = new TourPlan();
            //dsTP = tp.Get_TP_Edit_Year(div_code);
            //if (dsTP.Tables[0].Rows.Count > 0)
            //{
            //    for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            //    {
            //        ddlFYear.Items.Add(k.ToString());
            //        ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
            //    }
            //}
        }
        fillsubdivision();
        fillstockist();
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
    private void fillstockist()
    {
        loclass sd = new loclass();
        dsSalesForce = sd.Getstockistwise(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlstockist.DataTextField = "stockist_name";
            ddlstockist.DataValueField = "stockist_code";
            ddlstockist.DataSource = dsSalesForce;
            ddlstockist.DataBind();
            ddlstockist.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }

    public class loclass
    {
        public DataSet Getstockistwise(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            string strQry = "select stockist_code,stockist_name from mas_stockist where division_code='"+ divcode + "' and stockist_active_flag=0 order by stockist_name";

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