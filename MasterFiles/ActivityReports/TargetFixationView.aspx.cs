using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;

public partial class TargetFixationView : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    SampleDespatch objSample = new SampleDespatch();
    SalesForce sf = new SalesForce();

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();

        if (!IsPostBack)
        {
            this.FillMasterList();
           // menu.FindControl("btnBack").Visible = false;
        }
    }

    private void FillMasterList()
    {
        dsSalesForce = sf.UserList_getMR(div_code, sfCode);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        }
    }
}