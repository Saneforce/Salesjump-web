using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;


public partial class MIS_Reports_Attendace_view : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
	DataSet dsDivision= null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string sCurrentDate = string.Empty;
    public string sf_code = string.Empty;
    DataSet dsSf = null;
    public string sf_type = string.Empty;
    int mode = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
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
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
		sub_division = Session["sub_division"].ToString();
        if (sf_type == "1")
        {
            sf_code = Session["sf_code_MR"].ToString();
            div_code = Session["div_code"].ToString();
            FillYear();
            fillsubdivision();
            //fillsubdivision();
         
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
            FillYear();



       string url = HttpContext.Current.Request.Url.AbsoluteUri.Replace("http://", "");
        string[] words = url.Split('.');
        string shortna = words[0];
        if (shortna == "www") shortna = words[1];
		if(Session["CmpIDKey"]!=null && Session["CmpIDKey"].ToString()!=""){shortna =Session["CmpIDKey"].ToString();}
        string filename = shortna + "_logo.png";
        string dynamicFolderPath = "../limg/";//which used to create                                       dynamic folder
        string path = dynamicFolderPath + filename.ToString();
         lblpath.Text=path;
         fillsubdivision();
            Fillfeildforce();
        }

    }

    private void fillsubdivision()
    {
        SalesForce sd = new SalesForce();
        dsSalesForce = sd.Getsubdivisionwise(div_code,sub_division);
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

        if (subdiv.SelectedValue.ToString() != "0")
        {
            FillState(div_code);
            FillMRManagers();
        }
        else
        {
            FillMRManagers();
        }
    }
      private void FillState(string div_code)
    {
        SalesForce dv = new SalesForce();
        ddlstate.Items.Clear();
        dsDivision = dv.getsubdiv_States(div_code, sf_code, subdiv.SelectedValue);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlstate.DataTextField = "StateName";
            ddlstate.DataValueField = "State_code";
            ddlstate.DataSource = dsDivision;
            ddlstate.DataBind();
            ddlstate.Items.Insert(0, new ListItem("--Select--", "0"));

        }

    }
    protected void ddlstate_SelectIndexchanged(object sender, EventArgs e)
    {
        FillMRManagers();
    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        ddlFieldForce.Items.Clear();
        dsSalesForce = sf.SalesForceList(div_code, sf_code, subdiv.SelectedValue,"1",ddlstate.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            //ddlSF.DataTextField = "Desig_Color";
            //ddlSF.DataValueField = "sf_code";
            //ddlSF.DataSource = dsSalesForce;
            //ddlSF.DataBind(); 


        }
    }
    private void Fillfeildforce()
    {

        ddlFieldForce.DataSource = null;
        ddlFieldForce.Items.Clear();
        ddlFieldForce.Items.Insert(0, new ListItem("--Select--", "0"));



        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceList(div_code, sf_code,sub_division);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", "0"));

           


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
    protected void btnGo_Click(object sender, EventArgs e)
    {

      
    }
}