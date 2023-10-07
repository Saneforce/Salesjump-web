using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
public partial class MIS_Reports_channel_wise : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsdiv = new DataSet();
    DataSet dsTP = null;
    DataSet dsDivision = null;
    string strMultiDiv = string.Empty;
    string div_code = string.Empty;
    public string sf_code = string.Empty;
    DataSet dsSf = null;
    public string sf_type = string.Empty;
	public static string sub_division = string.Empty;
	
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
       // sf_type = Session["sf_type"].ToString();
        div_code = Session["div_code"].ToString();
        sub_division = Session["sub_division"].ToString();
        if (!Page.IsPostBack)
        {
            fillsubdivision();
            fillsalesforce();
                  }
    }
    private void fillsalesforce()
    {
        ddlFieldForce.DataSource = null;
        ddlFieldForce.Items.Clear();
        ddlFieldForce.Items.Insert(0, new ListItem("--Select--", "0"));
        SalesForce sd = new SalesForce();

        dsSalesForce = sd.feildforceelist(div_code,sub_division);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataValueField = "Sf_Code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getchannelwise(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlchl.DataTextField = "Doc_Special_Name";
            ddlchl.DataValueField = "Doc_Special_Code";
            ddlchl.DataSource = dsSalesForce;
            ddlchl.DataBind();
            ddlchl.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        try
        {
            string StartDate = Convert.ToDateTime(Request.Form["txtFrom1"]).ToString("yyyy-MM-dd");
            string EndDate = Convert.ToDateTime(Request.Form["txtFrom"]).ToString("yyyy-MM-dd");

          

            string sURL = "rpt_TotalOrders_Chennel_wise.aspx?Channel=" + ddlchl.SelectedValue + "&div_code=" + div_code + "&FromDate=" + StartDate + "&ToDate=" + EndDate + "&Type=" + sf_type +"&Sf_Code=" +ddlFieldForce.SelectedValue;
            string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
            ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

        }
        catch (Exception)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Date!!!');</script>");
        }
    }

}