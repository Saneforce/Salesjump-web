using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;

public partial class MIS_Reports_Distributor_Closing_Stock : System.Web.UI.Page
{
    string div_code = string.Empty;

    DataSet dsTP = null;
    DataSet dsSalesForce = null;

    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
    string FDT = string.Empty;
    string TDT = string.Empty;
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
        if (sf_type == "1")
        {
            sf_code = Session["sf_code_MR"].ToString();
            div_code = Session["div_code"].ToString();
            
            //fillsubdivision();
            Distributor.Visible = false;
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
            filldistributor();
            Distributor.Visible = false;
            seldist.Visible = false;


            if (subdiv.Items.Count > 0)
            {
                subdiv.SelectedIndex = 1;
                subdiv_SelectedIndexChanged(sender, e);
            }
        }

    }
    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (subdiv.SelectedValue != "0")
        {
            Distributor.Visible = true;
            seldist.Visible = true;
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
        else
        {
            ddlFieldForce.DataSource = null;
            ddlFieldForce.Items.Clear();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", "0"));


            ddlSF.DataSource = null;
            ddlSF.Items.Clear();
            ddlSF.Items.Insert(0, new ListItem("--Select--", "0"));


            Distributor.DataSource = null;
            Distributor.Items.Clear();
            Distributor.Items.Insert(0, new ListItem("--Select--", "0"));
        }

    }
    private void fillsubdivision()
    {

        SalesForce sd = new SalesForce();
        // dsSalesForce = sd.Getsubdivisionwise(div_code);

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
    private void getddlSF_Code_MR()
    {

        Distributor.DataSource = null;
        Distributor.Items.Clear();
        Distributor.Items.Insert(0, new ListItem("--Select--", "0"));

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
            Distributor.Items.Insert(0, new ListItem("--All Distributors--", ""));


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

        //SalesForce sf = new SalesForce();
        DataTable dt = new DataTable();
        dsSalesForce = SalesForceList(sf_code, div_code);
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
    public DataSet SalesForceList(string sf_code, string divcode, string Sub_Div_Code = "0", string Alpha = "1", string stcode = "0")
    {

        DataSet dt = new DataSet();

        string ConnectionString = Globals.ConnString;
        SqlConnection con = new SqlConnection(ConnectionString);
        //SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        SqlCommand cmd = new SqlCommand("EXEC getHyrSFList '" + sf_code + "','" + divcode + "','" + Sub_Div_Code + "','" + Alpha + "'," + stcode + "", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        con.Close();
        return dt;
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

        }
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
    //protected void btnGo_Click(object sender, EventArgs e)
    //{
    //    string sURL = string.Empty;

       

    //    sURL = "rpt_retail_lost_purchase_sel_month.aspx?&fdate=" + FDT.SelectedValue.ToString() + "&tdate=" + TDT.SelectedValue.ToString() + "&dist_code=" + Distributor.SelectedValue.ToString();
    //    string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
    //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "pop", newWin, true);

    //}

}