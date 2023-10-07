using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;
using iTextSharp.tool.xml;

using System.Web.Services;
using System.Globalization;

public partial class MIS_Reports_rptDSRCallsDetails : System.Web.UI.Page
{
    string divcode = string.Empty;
    string sfCode = string.Empty;
    string fYear = string.Empty;
    string fMonth = string.Empty;
    string sfName = string.Empty;
    string types = string.Empty;
    DataSet dsRetailer = null;
    protected void Page_Load(object sender, EventArgs e)
    {
       // divcode = Session["DivCode"].ToString();
        sfCode = Request.QueryString["SF_Code"].ToString();
        fYear = Request.QueryString["Year"].ToString();
        fMonth = Request.QueryString["months"].ToString();
        sfName = Request.QueryString["SF_Name"].ToString();
        types = Request.QueryString["types"].ToString();

        lblhead.Text = "Name : " + sfName;
        DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        string strFmonth = mfi.GetMonthName(Convert.ToInt32(fMonth)).ToString(); //August  
        if (types == "RP")
        {
            lblhead1.Text = "Repeat Purchase Retailers of " + strFmonth.Substring(0, 3) + " - " + fYear;
        }
        else if (types == "NR")
        {
            lblhead1.Text = "New Retailers of " + strFmonth.Substring(0, 3) + " - " + fYear;
        }
        else if (types == "PC")
        {
            lblhead1.Text = "Productive Calls Retailers of " + strFmonth.Substring(0, 3) + " - " + fYear;
        }
        else
        {
            lblhead1.Text = "Actual Calls Retailers of " + strFmonth.Substring(0, 3) + " - " + fYear;
        }
        //DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        //string strFmonth = mfi.GetMonthName(Convert.ToInt32(Request.QueryString["FMonth"].ToString())).ToString(); //August        
        //lblyear.Text = strFmonth.Substring(0, 3) + " - " + Request.QueryString["FYear"].ToString() + " To " + strTmonth.Substring(0, 3) + " - " + Request.QueryString["TYear"].ToString(); ;
        getData();
    }

    private void getData()
    {
        Order ord = new Order();
        dsRetailer = new DataSet();
        dsRetailer = ord.GetCallDSR(sfCode, fYear, fMonth, types);
        dgvRetailers.DataSource = dsRetailer;
        dgvRetailers.DataBind();
    }
}