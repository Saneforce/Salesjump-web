using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_OrderwithSale : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    int mode = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
	protected override void OnPreInit(EventArgs e)
    {
		base.OnPreInit(e);
		sf_type = Session["sf_type"].ToString();
    	if (sf_type == "3")
		{
    		this.MasterPageFile = "~/Master.master";
    	}
    	else if(sf_type == "2")
		{
    		this.MasterPageFile = "~/Master_MGR.master";
  		}
 		else if(sf_type == "1")
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
       
          FillYear();
          fillsubdivision();
        }

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
     private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());
                
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
              
            }
        }

        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
      

    }
protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {
 fillsalesforce();

    }

    private void fillsalesforce()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.SalesForceList(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            salesforcelist.DataTextField = "Sf_Name";
            salesforcelist.DataValueField = "Sf_Code";
            salesforcelist.DataSource = dsSalesForce;
            salesforcelist.DataBind();
            salesforcelist.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {

       
        string sURL = string.Empty;

        string date = Request.Form["TextBox1"];
      
        sURL = "rptorderwithsale.aspx?&subdivision="+subdiv.SelectedValue.ToString()+"&month=" + ddlFMonth.SelectedValue.ToString() + "&year="+ddlFYear.SelectedValue.ToString()+" &sfcode=" + salesforcelist.SelectedValue.ToString() + "&sfname=" + salesforcelist.SelectedItem.Text;


        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "pop", newWin, true);
      //Response.Redirect("rptorderwithsale.aspx?&subdivision="+subdiv.SelectedValue.ToString()+"&month=" + ddlFMonth.SelectedValue.ToString() + "&year="+ddlFYear.SelectedValue.ToString()+" &sfcode=" + salesforcelist.SelectedValue.ToString() + "&sfname=" + salesforcelist.SelectedItem.Text);
      
    }
}
