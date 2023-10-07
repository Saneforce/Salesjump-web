using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using Newtonsoft.Json;
using Bus_EReport;


public partial class MIS_Reports_sundywrk_aprval : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
   [WebMethod(EnableSession =true)]
   public static string getData()
    {
        SalesForce dv = new SalesForce();
        DataSet dsProd = dv.get_sunday_aprvl();
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Approvaldata(string SF_Code, string sundate)
    {
        string err = "";
        int iReturn = -1;

        try
        {
            SalesForce hod = new SalesForce();
            iReturn = hod.get_sunday_wrk_aprvl(SF_Code, sundate);
            if (iReturn > 0)
            {
                err = "Sucess";
            }

        }
        catch (Exception ex)
        {
            err = "Error";
        }
        return err;
    }
    [WebMethod(EnableSession = true)]
    public static string RejectData(string SF_Code, string sundate)
    {
        string err = "";
        int iReturn = -1;

        try
        {
            SalesForce hod = new SalesForce();
            iReturn = hod.get_sunday_wrk_reject(SF_Code, sundate);
            if (iReturn > 0)
            {
                err = "Sucess";
            }

        }
        catch (Exception ex)
        {
            err = "Error";
        }
        return err;
    }
}