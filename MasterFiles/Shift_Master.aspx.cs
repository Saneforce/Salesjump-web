using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Services;
using Bus_EReport;
using System.Data;


public partial class MasterFiles_Shift_Master : System.Web.UI.Page
{
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

    }

    [WebMethod(EnableSession = true)]
    public static string getShift(string divcode)
    {
        CallPlan ast = new CallPlan();
        DataSet ds = ast.getAllShift(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }


    [WebMethod(EnableSession = true)]
    public static int SetNewStatus(string SF, string stus)
    {
        CallPlan ast = new CallPlan();
        int iReturn = ast.DeActivate(SF, stus);
        return iReturn;
    }

    [WebMethod(EnableSession = true)]
    public static string saveshift(string data)
    {
        string msg = string.Empty;
        Bus_EReport.CallPlan.SaveShift Data = JsonConvert.DeserializeObject<Bus_EReport.CallPlan.SaveShift>(data);
        CallPlan dsm = new CallPlan();
        msg = dsm.saveShiftTime(Data);
        return msg;
    }

    [WebMethod(EnableSession = true)]
    public static string getShiftID(string divcode)
    {
        CallPlan cp = new CallPlan();
        DataSet ds = cp.getShiftID(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string getshift1(string divcode, string scode)
    {
        CallPlan cp = new CallPlan();
        DataSet ds = cp.getShift(scode, divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    //[WebMethod]
    //public static string GetHQDetails(string divcode)
    //{
    //    SalesForce SFD = new SalesForce();
    //    DataSet ds = SFD.getAllSFHQ(divcode);
    //    return JsonConvert.SerializeObject(ds.Tables[0]);
    //}
    //[WebMethod]
    //public static string GetDeptDetails(string divcode)
    //{
    //    SalesForce SFD = new SalesForce();
    //    DataSet ds = SFD.getAllSF_Dept(divcode);
    //    return JsonConvert.SerializeObject(ds.Tables[0]);
    //}
}