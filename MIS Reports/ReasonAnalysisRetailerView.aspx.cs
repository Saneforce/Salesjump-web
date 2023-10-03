using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

using System.Data;
using Bus_EReport;
public partial class MIS_Reports_ReasonAnalysisRetailerView : System.Web.UI.Page
{

    string DivCode = string.Empty;
    string SFCode = string.Empty;
    string FYear = string.Empty;
    string FMonth = string.Empty;
    string Remark = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        DivCode = Session["div_code"].ToString();
        SFCode = Request.QueryString["SFCode"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        Remark = Request.QueryString["Remarks"].ToString();
        if (Remark == string.Empty)
        {
            Label1.Text = " Not specified";
        }
        else
        {
            Label1.Text = " "+Remark;
        }
		Label2.Text = " "+ Request.QueryString["SFName"].ToString();
		string  monthName = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName( Convert.ToInt16(FMonth));

		lblhead.Text = "Retailer wise Reason Analysis for "+ monthName +"-"+FYear;

        GetRetailrs();
    }

    private void GetRetailrs()
    {
        DCR_New dcn = new DCR_New();
        DataSet dsPro = dcn.getRemarksRetailers(DivCode, SFCode, FYear, FMonth, Remark, "0");
        GVRetailer.DataSource = dsPro;
        GVRetailer.DataBind();
    }
}