using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Audit_Team : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsSF = null;
    DataSet dsSalesForce1 = null;
    DataSet dsState = null;
    string sState = string.Empty;
    string div_code = string.Empty;
    string mainvalue = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string sf_code = string.Empty;
    string sf_name = string.Empty;
    string usr_name = string.Empty;
    string division_code = string.Empty;
    int state = -1;
    int div_count = 0;
    int ismultidivision = 0;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string sCmd = string.Empty;
    string hq = string.Empty;
    string search = string.Empty;
    string state_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        Session["backurl"] = "SalesForceList.aspx";
        if (!Page.IsPostBack)
        {
            
            menu1.Title = this.Page.Title;

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillSalesForce();
        }

    }
    protected void grdSalesForce_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.FillSalesForcelist_Mgr(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
           
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();

        }

    }
   
}