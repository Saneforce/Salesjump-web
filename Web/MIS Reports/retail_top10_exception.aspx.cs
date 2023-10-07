using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;


public partial class MIS_Reports_retail_top10_exception : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    DataSet dsSf = null;
    string tot_dcr_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_type = string.Empty;

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

        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();


        if (Session["sf_type"].ToString() == "1")
        {
            //UserControl_MR_Menu c1 =
            //    (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            ViewState["sf_type"] = "";
            SalesForce sf = new SalesForce();
            dsSf = sf.getReportingTo(sf_code);
            if (dsSf.Tables[0].Rows.Count > 0)
            {
                sf_code = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }

            if (!Page.IsPostBack)
            {

            }



        }

        else if (Session["sf_type"].ToString() == "2")
        {
            //UserControl_MGR_Menu c1 =
            //(UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            ViewState["sf_type"] = "";
            if (!Page.IsPostBack)
            {

            }

        }
        else
        {
            ViewState["sf_type"] = "admin";
            //UserControl_MenuUserControl c1 =
            //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //Divid.Controls.Add(c1);
            //c1.Title = "Monthly DCR Analysis";
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;


        }


        if (!Page.IsPostBack)
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
            fillsubdivision();
 if (subdiv.Items.Count > 0)
            {
                subdiv.SelectedIndex = 1;
                subdiv_SelectedIndexChanged(sender, e);
            }
        }

    }
    private void fillroute()
    {

		route.DataSource = null;
        route.Items.Clear();
        route.Items.Insert(0, new ListItem("--All--", "0"));

        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getretailer_distributor(div_code,Distributor.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            route.DataTextField = "Territory_Name";
            route.DataValueField = "Territory_Code";
            route.DataSource = dsSalesForce;
            route.DataBind();
            route.Items.Insert(0, new ListItem("--All--", "0"));

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
    private void filldistributor()
    {
		Distributor.DataSource = null;
        Distributor.Items.Clear();
        Distributor.Items.Insert(0, new ListItem("--Select--", "0"));  

        SalesForce sd = new SalesForce();
//        dsSalesForce = sd.GetStockist_subdivisionwise(div_code, subdiv.SelectedValue);
 if (sf_type == "3")
            dsSalesForce = sd.GetStockist_subdivisionwise(div_code, subdiv.SelectedValue);
        else
          dsSalesForce = sd.GetStockist_subdivisionwise(div_code, subdiv.SelectedValue, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            Distributor.DataTextField = "Stockist_Name";
            Distributor.DataValueField = "Stockist_code";
            Distributor.DataSource = dsSalesForce;
            Distributor.DataBind();
            Distributor.Items.Insert(0, new ListItem("--Select--", "0")); 


        }
       
    }
    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {
         if (subdiv.SelectedValue != "0")
        {
            filldistributor();
        }
        else
        {
            Distributor.DataSource = null;
            Distributor.Items.Clear();
            Distributor.Items.Insert(0, new ListItem("--Selete--", "0"));  
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        string sURL = string.Empty;
        sURL = "rpt_retail_top10_exception.aspx?FYear=" + ddlFYear.SelectedValue.ToString() + "&viewtop=" + topdrop.SelectedItem.Text + "&route=" + route.SelectedValue.ToString() + "&stockist_code=" + Distributor.SelectedValue.ToString();


        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

    }
    protected void Distributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Distributor.SelectedValue != "0")
        {
            fillroute();
        }
        else
        {
            route.DataSource = null;
            route.Items.Clear();
            route.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
}