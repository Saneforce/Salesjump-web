using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_Default : System.Web.UI.Page
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
     public static string sf_type = string.Empty;
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
            lblpath.Text = path;
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

    private void Fillfeildforce()
    {
        ddlFieldForce.DataSource = null;
        ddlFieldForce.Items.Clear();
        ddlFieldForce.Items.Insert(0, new ListItem("--Select--", "0"));
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceList(div_code,"sf_code", subdiv.SelectedValue);
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
        // ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
    }

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //       //SalesForce sf = new SalesForce();
    //       // dsSf = sf.CheckSFType(ddlFieldForce.SelectedValue.ToString());

    //       // if (dsSf.Tables[0].Rows.Count > 0)
    //       // {
    //       //     if (ViewState["sf_type"].ToString() != "admin")
    //       //         sf_type = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //       // }
    //        /*
    //        if (ddlMR.SelectedIndex != -1 && ddlMR.SelectedIndex != 0)
    //        {
    //            string sURL = "Rpt_My_Day_Plan_View.aspx?sf_code=" + ddlMR.SelectedValue + "&div_code=" + div_code + "&cur_month=" + ddlMonth.SelectedValue + "&cur_year=" + ddlYear.SelectedItem.Text +
    //                "&Mode=" + rbnList.SelectedItem.Value + "&Sf_Name=" + ddlMR.SelectedItem.Text + "&Date=" + StartDate;
    //            string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
    //            ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

    //        }
    //        else
    //        {*/
    //        string sURL = "rpt_orderwise_count.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&div_code=" + div_code + "&cur_year=" + ddlFYear.SelectedItem.Text +
    //            "&subdivision=" + subdiv.SelectedItem.Value + "&Sf_Name=" + ddlFieldForce.SelectedItem.Text + "&Type=" + sf_type;
    //        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
    //        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

    //        //}

    //    }
    //    catch (Exception)
    //    {
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Date!!!');</script>");
    //    }
    //}

    protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (subdiv.SelectedValue.ToString() != "0")
        {

            FillMRManagers(subdiv.SelectedValue.ToString());
        }
        else
        {
            FillMRManagers(subdiv.SelectedValue.ToString());
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string sURL = "rpt_orderwise_count.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&div_code=" + div_code + "&cur_year=" + ddlFYear.SelectedItem.Text +
              "&subdivision=" + subdiv.SelectedItem.Value + "&Sf_Name=" + ddlFieldForce.SelectedItem.Text + "&Type=" + sf_type;
        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "pop", newWin, true);
    }
}