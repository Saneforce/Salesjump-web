using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;


public partial class MIS_Reports_Zonewiseperformance : System.Web.UI.Page
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
        if (sf_type == "1")
        {
            sf_code = Session["sf_code_MR"].ToString();
            div_code = Session["div_code"].ToString();
            FillYear();
            
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
           
            FillYear();



            string url = HttpContext.Current.Request.Url.AbsoluteUri.Replace("http://", "");
            string[] words = url.Split('.');
            string shortna = words[0];
            if (shortna == "www") shortna = words[1];
            if (Session["CmpIDKey"] != null && Session["CmpIDKey"].ToString() != "") { shortna = Session["CmpIDKey"].ToString(); }
            string filename = shortna + "_logo.png";
            string dynamicFolderPath = "../limg/";//which used to create                                       dynamic folder
            string path = dynamicFolderPath + filename.ToString();
            lblpath.Text = path;
            Filldesination();
        }

    }





    private void Filldesination()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getsfDesignation(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            Dropdesignation.DataTextField = "Designation_Short_Name";
            Dropdesignation.DataValueField = "Designation_Code";
            Dropdesignation.DataSource = dsSalesForce;
            Dropdesignation.DataBind();
            Dropdesignation.SelectedIndex = 0;
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