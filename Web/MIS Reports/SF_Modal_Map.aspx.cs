using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Services;
using Bus_EReport;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_SF_Modal_Map : System.Web.UI.Page
{
    public string lat = string.Empty;
    public string lng=string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        lat = Request.QueryString["lat"].ToString();
        lng = Request.QueryString["lng"].ToString();
    }
}