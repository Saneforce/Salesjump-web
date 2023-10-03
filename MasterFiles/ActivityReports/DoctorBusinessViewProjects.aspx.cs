using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;

public partial class DoctorBusinessViewProjects : System.Web.UI.Page
{
    string strSfCode = string.Empty, monthYear = string.Empty;
   
    DCRBusinessEntry objBusiness = new DCRBusinessEntry();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["sfCode"] != null)
            {
                strSfCode = Convert.ToString(Request.QueryString["sfCode"]);
            }
            if (Request.QueryString["monthYear"] != null)
            {
                monthYear = Convert.ToString(Request.QueryString["monthYear"]);
            }


            this.BindGrid(strSfCode, monthYear);
        }
    }

    private void BindGrid(string sfCode, string monthYear)
    {
        DataSet dsTarget = null;
        string[] strMonthYear = monthYear.Split('-');
        string strMonth = string.Empty, strYear = string.Empty;
        if (strMonthYear.Length > 1)
        {
            strMonth = strMonthYear[0];
            strYear = strMonthYear[1];
        }
        dsTarget = objBusiness.GetDCRBusinessProducts(sfCode, strMonth, strYear);
        gvProjects.DataSource = dsTarget;
        gvProjects.DataBind();
    }
    
}