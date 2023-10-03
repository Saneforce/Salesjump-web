using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_frmStockSales : System.Web.UI.Page
{

    public string divCd = string.Empty;
    string sf_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        divCd = Session["Division_Code"].ToString().Replace(",", "");

        sf_code = Session["Sf_Code"].ToString();
        FillMRManagers();
    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();

        DataSet dsSalesForce = null;
        dsSalesForce = sf.SalesForceList(divCd, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFF.DataTextField = "sf_name";
            ddlFF.DataValueField = "sf_code";
            ddlFF.DataSource = dsSalesForce;
            ddlFF.DataBind();
            ddlFF.Items.Insert(0, "---Select Field Force---");
        }
    }
    [WebMethod(EnableSession = true)]
    public static string getDistributor(string div_code, string SFCode)
    { 
        SalesForce sf = new SalesForce();
        DataSet ds = null;
        ds = sf.GetStockName_Cus1(div_code, SFCode);

        return JsonConvert.SerializeObject(ds.Tables[0]); ;
    }

}