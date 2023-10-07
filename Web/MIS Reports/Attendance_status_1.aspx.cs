using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Bus_EReport;
using System.Data;
using Newtonsoft.Json;

public partial class MIS_Reports_Attendance_status_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string getDivision(string divcode)
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.Getsubdivisionwise(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod]
    public static string getFieldForce(string divcode, string sfcode, string subdiv)
    {
        SalesForce sd = new SalesForce();
        DataSet ds = new DataSet();
        ds = sd.SalesForceList(divcode, sfcode, subdiv);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}