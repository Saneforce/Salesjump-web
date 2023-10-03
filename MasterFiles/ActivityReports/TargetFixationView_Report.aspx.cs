using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;

public partial class TargetFixationView_Report : System.Web.UI.Page
{
    string strSfCode = string.Empty, strSfName = string.Empty, fromMonth = string.Empty, fromYear = string.Empty, toMonth = string.Empty, toYear = string.Empty;
    string strFromMonthYear = string.Empty, strToMonthYear = string.Empty;
    string strFromMonthName = string.Empty, strToMonthName = string.Empty;
    TargetFixation objTarget = new TargetFixation();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["sf_Code"] != null)
            {
                strSfCode = Convert.ToString(Request.QueryString["sf_Code"]);
            }
            if (Request.QueryString["sf_Name"] != null)
            {
                strSfName = Convert.ToString(Request.QueryString["sf_Name"]);
            }
            
            if (Request.QueryString["fromMonth"] != null)
            {
                fromMonth = Convert.ToString(Request.QueryString["fromMonth"]);
            }
            if (Request.QueryString["toMonth"] != null)
            {
                toMonth = Convert.ToString(Request.QueryString["toMonth"]);
            }

            if (Request.QueryString["fromMonthName"] != null)
            {
                strFromMonthName = Convert.ToString(Request.QueryString["fromMonthName"]);
            }
            if (Request.QueryString["toMonthName"] != null)
            {
                strToMonthName = Convert.ToString(Request.QueryString["toMonthName"]);
            }

            if (Request.QueryString["fromYear"] != null)
            {
                fromYear = Convert.ToString(Request.QueryString["fromYear"]);
            }
            if (Request.QueryString["toYear"] != null)
            {
                toYear = Convert.ToString(Request.QueryString["toYear"]);
            }

            if (fromMonth != string.Empty && fromYear != string.Empty)
            {
                strFromMonthYear = Convert.ToDateTime("01/" + fromMonth + "/" + fromYear).ToString("yyyyMMdd");
            }

            if (toMonth != string.Empty && toYear != string.Empty)
            {
                strToMonthYear = Convert.ToDateTime("01/" + toMonth + "/" + toYear).ToString("yyyyMMdd");
            }

            lblHeader.Text = "Target Fixation View " + strFromMonthName + " " + fromYear + " To " + strToMonthName + " " + toYear;
            lblFieldForce.Text = strSfName;

            this.BindGrid(strSfCode, strFromMonthYear, strToMonthYear);
        }
    }

    private void BindGrid(string sfCode, string fromMonthYear,string strToMonthYear)
    {
        DataSet dsTarget = null;
        dsTarget = objTarget.GetTargetFixationReport(sfCode, fromMonthYear, strToMonthYear);
        //gvTargetReport.DataSource = dsTarget;
        //gvTargetReport.DataBind();
    }
}