using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_Order_Detail_View : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    int imissed_dr = -1;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
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
        //div_code = Session["div_code"].ToString();
        if (sf_type == "1")
        {
            sf_code = Session["sf_code_MR"].ToString();
            div_code = Session["div_code"].ToString();
            FillYear();
            //fillsubdivision();
            ddl_dis.Visible = false;
            Label1.Visible = false;
        }
        if (sf_type == "3")
        {
            div_code = Session["Division_Code"].ToString().Replace(",", "");
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.FindControl("btnBack").Visible = false;
            fillsalesforce();
            fillsubdivision();
            filldis();
            FillYear();
            fillRoute();
            Label4.Visible = false;
            salesforcelist.Visible = false;
            Label1.Visible = false;
            ddl_Route.Visible = false;
            Label2.Visible = false;
            ddl_dis.Visible = false;

			if (subdiv.Items.Count > 0)
            {
                subdiv.SelectedIndex = 1;
                subdiv_SelectedIndexChanged(sender, e);
            }
        }

    }
    private void fillsalesforce()
    {

		salesforcelist.DataSource = null;
        salesforcelist.Items.Clear();
        salesforcelist.Items.Insert(0, new ListItem("--Select--", "0"));

        SalesForce sd = new SalesForce();
	  
		 if (sf_type == "3")
        {
            dsSalesForce = sd.SalesForceList(div_code, sf_code);
        }
        else
        {
            dsSalesForce = sd.SalesForceList(div_code, sf_code);
           }
        //dsSalesForce = sd.feildforceelist(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            salesforcelist.DataTextField = "Sf_Name";
            salesforcelist.DataValueField = "Sf_Code";
            salesforcelist.DataSource = dsSalesForce;
            salesforcelist.DataBind();
            salesforcelist.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
		//dsSalesForce = sd.Getsubdivisionwise(div_code);
		if (sf_type == "3")
            dsSalesForce = sd.Getsubdivisionwise(div_code);
        else
            dsSalesForce = sd.Getsubdivisionwise_sfcode(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            subdiv.DataTextField = "subdivision_name";
            subdiv.DataValueField = "subdivision_code";
            subdiv.DataSource = dsSalesForce;
            subdiv.DataBind();
            subdiv.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    public void FillFO()
    {


		salesforcelist.DataSource = null;
        salesforcelist.Items.Clear();
        salesforcelist.Items.Insert(0, new ListItem("--Select--", "0"));

	 if (subdiv.SelectedValue != "0")
        {
        SalesForce sf = new SalesForce();

		 DataView dv = new DataView();
        //dsSalesForce = sd.feildforceelist(div_code);
        if (sf_type == "3")
        {
            dsSalesForce = sf.SalesForceList(div_code, sf_code);
        }
        else
        {
            dsSalesForce = sf.SalesForceList(div_code, sf_code);
            dv = dsSalesForce.Tables[0].DefaultView;
            dv.RowFilter = "sf_type='1'";
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {

            salesforcelist.DataTextField = "Sf_Name";
            salesforcelist.DataValueField = "Sf_Code";
            salesforcelist.DataSource = dv;
            salesforcelist.DataBind();
            salesforcelist.Items.Insert(0, new ListItem("--Select--", "0"));
        }
	}
    }
    private void filldis()
    {

 ddl_dis.DataSource = null;
        ddl_dis.Items.Clear();
        ddl_dis.Items.Insert(0, new ListItem("--Select--", "0"));

        if (salesforcelist.SelectedValue != "0")
        {
        Order sd = new Order();
        dsSalesForce = sd.view_stockist_feildforcewise(div_code,salesforcelist.SelectedValue,subdiv.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddl_dis.DataTextField = "Stockist_Name";
            ddl_dis.DataValueField = "Stockist_Code";
            ddl_dis.DataSource = dsSalesForce;
            ddl_dis.DataBind();
            ddl_dis.Items.Insert(0, new ListItem("--Select--", "0"));

        }
}
    }
  private void fillRoute()
    {


        ddl_Route.DataSource = null;
        ddl_Route.Items.Clear();
        ddl_Route.Items.Insert(0, new ListItem("--Select--", "0"));
if (ddl_dis.SelectedValue != "0")
        {

        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getroute_distributor_Fieldforcewise(div_code,ddl_dis.SelectedValue,salesforcelist.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddl_Route.DataTextField = "Territory_Name";
            ddl_Route.DataValueField = "Territory_Code";
            ddl_Route.DataSource = dsSalesForce;
            ddl_Route.DataBind();


        }
}
    }
  private void getddlSF_Code_MR()
  {

 ddl_dis.DataSource = null;
        ddl_dis.Items.Clear();
        ddl_dis.Items.Insert(0, new ListItem("--Select--", "0"));

        if (salesforcelist.SelectedValue != "0")
        {
      SalesForce sf = new SalesForce();
      dsSalesForce = sf.GetDistNamewise_MR(div_code, subdiv.SelectedValue, sf_code);

      if (dsSalesForce.Tables[0].Rows.Count > 0)
      {
          ddl_dis.DataTextField = "Stockist_Name";
          ddl_dis.DataValueField = "Stockist_Code";
          ddl_dis.DataSource = dsSalesForce;
          ddl_dis.DataBind();
          ddl_dis.Items.Insert(0, new ListItem("--Select--", "0"));
      }
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
                //ddlTYear.Items.Add(k.ToString());
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
                //ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }

        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
        //ddlTMonth.SelectedValue = DateTime.Now.Month.ToString();

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {

        int FYear = Convert.ToInt32(ddlFYear.SelectedValue);

        int FMonth = Convert.ToInt32(ddlFMonth.SelectedValue);




        string sURL = string.Empty;


         
       sURL = "rpt_Order_Detail_View.aspx?&FMonth=" + ddlFMonth.SelectedValue.ToString() + "&FYear=" + ddlFYear.SelectedValue.ToString() + "&Distributor=" + ddl_dis.SelectedValue + "&Route=" + ddl_Route.SelectedValue + "&Fo_Name=" + salesforcelist.SelectedItem.ToString() + "&Dis=" + ddl_dis.SelectedItem.ToString();

        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
       ScriptManager.RegisterClientScriptBlock(this, GetType(), "pop", newWin, true);
    }
    protected void ddl_dis_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Visible = true;
        ddl_Route.Visible = true;

 if (ddl_dis.SelectedValue != "0")
        {
        SalesForce sd = new SalesForce();
                dsSalesForce = sd.Getroute_distributor_Fieldforcewise(div_code,ddl_dis.SelectedValue,salesforcelist.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddl_Route.DataTextField = "Territory_Name";
            ddl_Route.DataValueField = "Territory_Code";
            ddl_Route.DataSource = dsSalesForce;
            ddl_Route.DataBind();



        }
    }
       

        else
        {
            ddl_Route.DataSource = null;
            ddl_Route.Items.Clear();
            ddl_Route.Items.Insert(0, new ListItem("--Select--", "0"));

        }

    }
    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label4.Visible = true;
        salesforcelist.Visible = true;

 if (subdiv.SelectedValue != "0")
        {
        if (sf_type == "3")
        {
            fillsalesforce();
        }
        else if (sf_type == "2")
        {
            FillFO();
        }
        else if (sf_type == "1")
        {
            getddlSF_Code_MR();
        }
 }
        else
        {
            salesforcelist.DataSource = null;
            salesforcelist.Items.Clear();
            salesforcelist.Items.Insert(0, new ListItem("--Select--", "0"));

            ddl_dis.DataSource = null;
            ddl_dis.Items.Clear();
            ddl_dis.Items.Insert(0, new ListItem("--Select--", "0"));


            ddl_Route.DataSource = null;
            ddl_Route.Items.Clear();
            ddl_Route.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    protected void salesforcelist_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label2.Visible = true;
        ddl_dis.Visible = true;

if (salesforcelist.SelectedValue != "0")
        {
        if (sf_type == "3")
        {
            filldis();
        }
        else if (sf_type == "2")
        {
            filldis();
        }
        else if (sf_type == "1")
        {
            getddlSF_Code_MR();
        }
 }
        else
        {
            ddl_dis.DataSource = null;
            ddl_dis.Items.Clear();
            ddl_dis.Items.Insert(0, new ListItem("--Select--", "0"));


            ddl_Route.DataSource = null;
            ddl_Route.Items.Clear();
            ddl_Route.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
}
