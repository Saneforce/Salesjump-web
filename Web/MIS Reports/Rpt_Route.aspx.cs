using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using System.Globalization;

public partial class MIS_Reports_Rpt_Route : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string mon = string.Empty;
    string year = string.Empty;
    string[] Words =null;

    string startDate = string.Empty;
	string sf_type = string.Empty;
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
        div_code = Session["div_code"].ToString();
        if ( !IsPostBack)
            {
            fillddl();
        }
    }
    private void fillddl ()
    {
        SalesForce SF = new SalesForce();
        DataSet dsCounts = new DataSet();
        dsCounts = SF.get_salesforce(div_code);
        ddlff.DataSource = dsCounts;
        ddlff.DataTextField = "Sf_Name";
        ddlff.DataValueField = "Sf_Code";
        ddlff.DataBind();
        ddlff.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    /*protected void btnview_click(object sender, EventArgs e)
    {
         startDate = Convert.ToString(Request.Form["startDate"]);   
      
         Words = startDate.Split(new char[] { ' ' });
         mon = Words[0];
         year = Words[1];
        if (  ddlff.SelectedValue=="")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "<script type=text/javascript> alert('pls Select field Force!') </script>");
        }
        if (startDate=="")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "<script type=text/javascript> alert('pls Date') </script>");
        }
      //  DateTime sdate = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        Response.Redirect("rpt_route_availvisi.aspx?&sfcode=" + ddlff.SelectedValue + "&month" + mon.ToString()+ "&year"+ year.ToString());
    }*/

}