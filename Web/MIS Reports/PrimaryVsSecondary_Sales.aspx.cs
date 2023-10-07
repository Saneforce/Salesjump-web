using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Drawing.Imaging;

public partial class MIS_Reports_PrimaryVsSecondary_Sales : System.Web.UI.Page
{
    string div_code = string.Empty;
    public string sf_type = string.Empty;
    public string sf_code = string.Empty;
    DataSet dsTP = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsSalesForce = null;
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
        div_code = Session["div_code"].ToString();
        sf_type = Session["sf_type"].ToString();
		sub_division = Session["sub_division"].ToString();
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
            if (Session["CmpIDKey"] != null && Session["CmpIDKey"].ToString() != "") { shortna = Session["CmpIDKey"].ToString(); }
            string filename = shortna + "_logo.png";
            string dynamicFolderPath = "../limg/";//which used to create                                       dynamic folder
            string path = dynamicFolderPath + filename.ToString();
           // lblpath.Text = path;
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
            ddldiv.DataTextField = "subdivision_name";
            ddldiv.DataValueField = "subdivision_code";
            ddldiv.DataSource = dsSalesForce;
            ddldiv.DataBind();
            ddldiv.Items.Insert(0, new ListItem("--Select--", "0"));

        }
    }

    private void Fillfeildforce()
    {
        ddlFieldForce.DataSource = null;
        ddlFieldForce.Items.Clear();
        ddlFieldForce.Items.Insert(0, new ListItem("--Select--", "0"));
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceList(div_code, sf_code, ddldiv.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceList(div_code, sf_code, Sub_Div_Code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
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
    }


    protected void ddldiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldiv.SelectedValue.ToString() != "0")
        {

            FillMRManagers(ddldiv.SelectedValue.ToString());
        }
        else
        {
            FillMRManagers(ddldiv.SelectedValue.ToString());
        }
    }

}