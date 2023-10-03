using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;



public partial class MIS_Reports_Retailer_accountstatement : System.Web.UI.Page
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
            // menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.FindControl("btnBack").Visible = false;

            //FillYear();  Calendar1.Visible = false;

            fillsubdivision();
            Fillfeildforce();
            filldistributor();
            fillroute();
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
    private void filldistributor()
    {
        Distributor.DataSource = null;
        Distributor.Items.Clear();
        Distributor.Items.Insert(0, new ListItem("--Select--", "0"));

        product.DataSource = null;
        product.Items.Clear();
        product.Items.Insert(0, new ListItem("--Select--", "0"));

        SalesForce sf = new SalesForce();

        Order sd = new Order();
        dsSalesForce = sd.view_stockist_feildforcewise(div_code, ddlFieldForce.SelectedValue, subdiv.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            Distributor.DataTextField = "Stockist_Name";
            Distributor.DataValueField = "Stockist_code";
            Distributor.DataSource = dsSalesForce;
            Distributor.DataBind();
            Distributor.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    private void Fillfeildforce()
    {
        ddlFieldForce.DataSource = null;
        ddlFieldForce.Items.Clear();
        ddlFieldForce.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlSF.DataSource = null;
        ddlSF.Items.Clear();
        ddlSF.Items.Insert(0, new ListItem("--Select--", "0"));
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceList(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
            ddlSF.Items.Insert(0, new ListItem("--Select--", "0"));


        }
        FillColor();
    }
    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            // ddlFieldForce.Items[j].Selected = true;

            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }
    private void fillroute()
    {

        product.DataSource = null;
        product.Items.Clear();
        product.Items.Insert(0, new ListItem("--Select--", "0"));

        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getretailer_distributor(div_code, Distributor.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            product.DataTextField = "Territory_Name";
            product.DataValueField = "Territory_Code";
            product.DataSource = dsSalesForce;
            product.DataBind();
            product.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }


    private void fillretailer()
    {

        ddretailer.DataSource = null;
        ddretailer.Items.Clear();
        ddretailer.Items.Insert(0, new ListItem("--Select--", "0"));

        SalesForce sd = new SalesForce();
        dsSalesForce = sd.GetRetailerName_Distributorwise(Distributor.SelectedValue,product.SelectedValue,div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddretailer.DataTextField = "ListedDr_Name";
            ddretailer.DataValueField = "ListedDrCode";
            ddretailer.DataSource = dsSalesForce;
            ddretailer.DataBind();
            ddretailer.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }



    protected void btnGo_Click(object sender, EventArgs e)
    {



        string sURL = string.Empty;

        string date = Request.Form["TextBox1"];
        sURL = "rpt_retailer_potential_report.aspx?&DATE=" + date + " &subdivision=" + subdiv.SelectedValue.ToString() + "&Route_code=" + product.SelectedValue.ToString() + "&Route_name=" + product.SelectedItem.Text + "&stockist_code= " + Distributor.SelectedValue.ToString() + "&stockist_name=" + Distributor.SelectedItem.Text;


        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "pop", newWin, true);

    }
    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (subdiv.SelectedValue != "0")
        {

            Fillfeildforce();
        }
        else
        {
            ddlFieldForce.DataSource = null;
            ddlFieldForce.Items.Clear();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", "0"));


            ddlSF.DataSource = null;
            ddlSF.Items.Clear();
            ddlSF.Items.Insert(0, new ListItem("--Select--", "0"));

            product.DataSource = null;
            product.Items.Clear();
            product.Items.Insert(0, new ListItem("--Select--", "0"));

            Distributor.DataSource = null;
            Distributor.Items.Clear();
            Distributor.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    protected void route_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (product.SelectedValue != "0")
        {
            fillretailer();
        }
        else
        {
            ddretailer.DataSource = null;
            ddretailer.Items.Clear();
            ddretailer.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    protected void Distributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Distributor.SelectedValue != "0")
        {
            fillroute();
        }
        else
        {
            product.DataSource = null;
            product.Items.Clear();
            product.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedValue != "0")
        {
            filldistributor();
        }
        else
        {
            Distributor.DataSource = null;
            Distributor.Items.Clear();
            Distributor.Items.Insert(0, new ListItem("--Select--", "0"));

            product.DataSource = null;
            product.Items.Clear();
            product.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }
}
