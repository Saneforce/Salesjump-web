using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;

public partial class SampleDespatchHQStatus : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sfCode = string.Empty;
    SampleDespatch objSample = new SampleDespatch();
    SalesForce sf = new SalesForce();
    Product objProduct = new Product();

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            this.FillMasterList();
            menu.FindControl("btnBack").Visible = false;
        }
    }

    private void FillMasterList()
    {
        DataSet dsSalesForce = null;
        dsSalesForce = sf.UserList_getMR(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        }
    }



    protected void btnGo_Click(object sender, EventArgs e)
    {
        this.BindGrid(ddlFieldForce.SelectedValue,ddlMonth.SelectedValue,ddlYear.SelectedValue);
    }

    private void BindGrid(string sfCode, string strMonth, string strYear)
    {
        DataSet dsSampleDespatch = null;

        dsSampleDespatch = objSample.GetSampleDespatchStatus(sfCode, strMonth, strYear);
        if (dsSampleDespatch.Tables[0].Rows.Count > 0)
        {
            gvSampleDespatch.Visible = true;
            
            gvSampleDespatch.DataSource = dsSampleDespatch;
            gvSampleDespatch.DataBind();
        }
        else
        {
            gvSampleDespatch.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "GetMsg", "alert('No Records Found!');", true);
        }
    }
}