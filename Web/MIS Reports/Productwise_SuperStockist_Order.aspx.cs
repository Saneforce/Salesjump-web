using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_Productwise_SuperStockist_Order : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
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
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);            
            FillMRManagers();

        }
    }
    
    private void FillMRManagers()
    {

        try
        {
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
            //if (sf_type == "3")
            //{
            //    dsSalesForce.Tables[0].Rows[0].Delete();
            //}
            //else
            //{

            //}
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                ddlFieldForce.DataTextField = "sf_name";
                ddlFieldForce.DataValueField = "sf_code";
                ddlFieldForce.DataSource = dsSalesForce;
                ddlFieldForce.DataBind();

                

            }
          
        }
        catch (Exception)
        {

        }
    }

    
}