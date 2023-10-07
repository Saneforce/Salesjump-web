using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
public partial class MIS_Reports_Rpt_PrimaryOrderSFdetail : System.Web.UI.Page
{
    DataSet ds = null;
    string SF_Code = string.Empty;
    string Sf_Name = string.Empty;
    string Stockist_Name = string.Empty;
    string Trans_Sl_No = string.Empty;
    string order_value = string.Empty;
    string div = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        SF_Code = Request.QueryString["Sf_Code"].ToString();
        Sf_Name = Request.QueryString["Sf_Name"].ToString();
        Stockist_Name = Request.QueryString["Stockist_Name"].ToString();
        Trans_Sl_No = Request.QueryString["Trans_Sl_No"].ToString();
        order_value = Request.QueryString["order_value"].ToString();
        if (Request.QueryString["div"].ToString() != "0")
        {
            div = Request.QueryString["div"].ToString();
        }
        else
        {
            div = Session["div_code"].ToString();
        }     
        fillgvdata();
    }
    private void fillgvdata()
    {
        SalesForce sf = new SalesForce();

        ds = sf.priordercnfdetail(div, SF_Code, Trans_Sl_No);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvdata.DataSource = ds;
            gvdata.DataBind();
        }
    }
}