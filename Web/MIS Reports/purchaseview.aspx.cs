using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class purchaseview : System.Web.UI.Page
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
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
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
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "1" || sf_type == "2")
        {
            sf_code = Session["sf_code_MR"].ToString();
            div_code = Session["div_code"].ToString();
          
            //fillsubdivision();
            //Distributor.Visible = false;
            //Label1.Visible = false;
        }
        else
        {
            //sf_code = Session["sf_code_MR"].ToString();
            //div_code = Session["div_code"].ToString();
            //FillYear();
            //fillsubdivision();
            //Distributor.Visible = false;
            //Label1.Visible = false;

        }

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
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.FindControl("btnBack").Visible = false;

            
            fillsubdivision();
           // Distributor.Visible = false;
           // Label1.Visible = false;


			if (subdiv.Items.Count > 0)
            {
                subdiv.SelectedIndex = 1;
                subdiv_SelectedIndexChanged(sender, e);
            }
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
    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        Distributor.Visible = true;
        Label1.Visible = true;
       // filldistributor();
        if (sf_type == "3")
        {
            filldistributor();
        }
        else if (sf_type == "2")
        {
            filldistributor();
        }
        else if (sf_type == "1")
        {
            getddlSF_Code_MR();
        }



    }
    private void filldistributor()
    {
        //SalesForce sd = new SalesForce();
        //dsSalesForce = sd.GetStockist_subdivisionwisee(div_code, subdiv.SelectedValue);

		 Distributor.DataSource = null;
        Distributor.Items.Clear();
        Distributor.Items.Insert(0, new ListItem("--Select--", "select"));

        SalesForce sd = new SalesForce();
        if (sf_type == "3")
        {
            dsSalesForce = sd.GetStockist_subdivisionwise(div_code, subdiv.SelectedValue);
        }
        else
        {
            dsSalesForce = sd.GetStockist_subdivisionwise(sf_code: sf_code,divcode:div_code, subdivision: subdiv.SelectedValue);
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            Distributor.DataTextField = "Stockist_Name";
            Distributor.DataValueField = "Stockist_code";
            Distributor.DataSource = dsSalesForce;
            Distributor.DataBind();
            Distributor.Items.Insert(0, new ListItem("--Select--", "select"));

        }
    }
    private void getddlSF_Code_MR()
    {

		Distributor.DataSource = null;
        Distributor.Items.Clear();
        Distributor.Items.Insert(0, new ListItem("--Select--", "select"));

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.GetDistNamewise_MR(div_code, subdiv.SelectedValue, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            Distributor.DataTextField = "Stockist_Name";
            Distributor.DataValueField = "Stockist_Code";
            Distributor.DataSource = dsSalesForce;
            Distributor.DataBind();

            Distributor.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

   
    protected void btnGo_Click(object sender, EventArgs e)
    {

        string date = Request.Form["TextBox1"];


        if (Request.Form["TextBox1"] == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Date');</script>");
        }
else if (Distributor.SelectedItem.Text=="--Select--" )
{
ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Distributor');</script>");

}
else if (subdiv.SelectedItem.Text=="--Select--" )
{
ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Subdivision');</script>");

}
        else
        {

            string sURL = string.Empty;


            sURL = "rptpurchaseview.aspx?&DATE=" + date + "&Dist_Name=" + Distributor.SelectedItem.Text + "&subdivision=" + subdiv.SelectedValue.ToString() + "&Distributor=" + Distributor.SelectedValue.ToString();

            string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "pop", newWin, true);
        }
    }
}

