using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_Stock_and_Sales_Analysis : System.Web.UI.Page
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
            sf_code = Session["Title_MR"].ToString();
            div_code = Session["div_code"].ToString();
            FillYear();

        }
        else
        {


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

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            FillYear();
            fillsubdivision();
            // Distributor.Visible=false;
            //  Label1.Visible=false;

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
        //Label1.Visible=true;

        if (sf_type == "3")
        {
            Fillfeildforce();
        }
        else if (sf_type == "2")
        {
            Fillfeildforce();
        }
        else if (sf_type == "1")
        {
            Fillfeildforce();
            getddlSF_Code_MR();
        }



    }
    private void filldistributor()
    {
        Distributor.DataSource = null;
        Distributor.Items.Clear();
        Distributor.Items.Insert(0, new ListItem("--Select--", "0"));
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
    private void getddlSF_Code_MR()
    {
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
        ddltmnth.SelectedValue = DateTime.Now.Month.ToString();


    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldistributor();
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {

        int FYear = Convert.ToInt32(ddlFYear.SelectedValue);

        int FMonth = Convert.ToInt32(ddlFMonth.SelectedValue);




        string sURL = string.Empty;


        sURL = "rpt_stock_second_sales.aspx?&FMonth=" + ddlFMonth.SelectedValue.ToString() + "&Sf_Name="+ ddlFieldForce.SelectedItem.Text + "&SfCode="+ ddlFieldForce.SelectedValue.ToString() + "&FYear=" + ddlFYear.SelectedValue.ToString() + "&Dist_Name=" + Distributor.SelectedItem.Text + "&subdivision=" + subdiv.SelectedValue.ToString() + "&Distcode=" + Distributor.SelectedValue.ToString();

        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "pop", newWin, true);
    }
}
