using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Drawing.Imaging;

public partial class MIS_Reports_Expense_Report_New : System.Web.UI.Page
{
    string divCode = string.Empty;
	string sf_type = string.Empty;
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
        divCode = Session["div_code"].ToString();
		sf_type = Session["sf_type"].ToString();
		sub_division = Session["sub_division"].ToString();
        if (!Page.IsPostBack)
        {
            filldivision();
            filldist();
            BindDate();
        }
    }
    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();
        dsTP = tp.Get_TP_Edit_Year(divCode);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlyr.Items.Add(k.ToString());
            }
            ddlyr.Text = DateTime.Now.Year.ToString();
            //ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
        }
    }
    public void filldivision()
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.Getsubdivisionwise(divCode,sub_division);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldiv.DataTextField = "subdivision_name";
            ddldiv.DataValueField = "subdivision_code";
            ddldiv.DataSource = ds;
            ddldiv.DataBind();
            ddldiv.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    public void filldist()
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.SalesForceList(divCode, "admin", ddldiv.SelectedValue);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldist.DataTextField = "sf_name";
            ddldist.DataValueField = "sf_code";
            ddldist.DataSource = ds;
            ddldist.DataBind();
            ddldist.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    protected void ddldiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldist();
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            if (DBase_EReport.Global.ExpenseType!="0")
            {
                string sURL = "expenserpt_view.aspx?SF_code=" + ddldist.SelectedValue + "&div_code=" + divCode +
                   "&subdiv=" + ddldiv.SelectedItem.Value + "&SF_Name=" + ddldist.SelectedItem.Text + "&Mnth=" + ddlmnth.SelectedValue.ToString() + "&Yr=" + ddlyr.SelectedValue.ToString() + "&MnthName=" + ddlmnth.SelectedItem.Text + "&Type=" + ddltyp.SelectedValue.ToString();
                string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }
            else
            {
                string sURL = "Rpt_Expense_New.aspx?SF_code=" + ddldist.SelectedValue + "&div_code=" + divCode +
                   "&subdiv=" + ddldiv.SelectedItem.Value + "&SF_Name=" + ddldist.SelectedItem.Text + "&Mnth=" + ddlmnth.SelectedValue.ToString() + "&Yr=" + ddlyr.SelectedValue.ToString() + "&MnthName=" + ddlmnth.SelectedItem.Text + "&Type=" + ddltyp.SelectedValue.ToString();
                string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }

        }
        catch (Exception)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select!!!');</script>");
        }
    }

}