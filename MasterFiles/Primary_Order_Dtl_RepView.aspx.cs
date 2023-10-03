using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Primary_Order_Dtl_RepView : System.Web.UI.Page
{
    #region "Declaration"
    string div_code = string.Empty;
    public string sf_code = string.Empty;
    public string sf_type = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsTP = null;
    public static string sub_division = string.Empty;
    #endregion

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
        sub_division = Session["sub_division"].ToString();
        if (!Page.IsPostBack)
        {
            fillsubdivision();
            FillMRManagers("0");
        }
    }

    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code, sub_division);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlsubdiv.DataTextField = "subdivision_name";
            ddlsubdiv.DataValueField = "subdivision_code";
            ddlsubdiv.DataSource = dsSalesForce;
            ddlsubdiv.DataBind();
            ddlsubdiv.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlsubdiv.SelectedValue.ToString() != "0")
        {
            FillMRManagers(ddlsubdiv.SelectedValue.ToString());
        }
        else
        {
            FillMRManagers(ddlsubdiv.SelectedValue.ToString());
        }
    }

    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();


        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code, ddlsubdiv.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("---Select Field Force---", "0"));

        }
    }

}