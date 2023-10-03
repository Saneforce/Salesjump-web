using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Globalization;

public partial class locfinder : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string SFCode = string.Empty;
    string div_code = string.Empty;
    string sf_type = string.Empty;
    string sSFstr = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        SFCode = Session["SF_Code"].ToString();
        if (!Page.IsPostBack)
        {
            FillReporting();
        }
    }
    private void FillReporting()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceList_Track(div_code, SFCode);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            selSF.DataTextField = "Sf_Name";
            selSF.DataValueField = "Sf_Code";
            selSF.DataSource = dsSalesForce;
            selSF.DataBind();
        }
        for (int il = 0; il < dsSalesForce.Tables[0].Rows.Count; il++)
        {
            sSFstr += "{\"id\":\"" + dsSalesForce.Tables[0].Rows[il]["Sf_Code"].ToString() + "\",\"pId\":\"" + dsSalesForce.Tables[0].Rows[il]["Reporting_To_SF"].ToString() + "\",\"name\":\"" + dsSalesForce.Tables[0].Rows[il]["Sf_Name"].ToString() + "\"}";

            if (il < dsSalesForce.Tables[0].Rows.Count - 1) sSFstr += ",";
        }
        string ScriptValues = "datas=[" + sSFstr + "];genTreeView();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DatasLoad", ScriptValues, true);

    }
}