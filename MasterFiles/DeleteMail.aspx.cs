using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_DeleteMail : System.Web.UI.Page
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

    [WebMethod]
    public static string GetDeleteData(string divcode, string sfcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        ds = SFD.ViewDeleteData(divcode.TrimEnd(','), sfcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static int PermanentDeleteData(string divcode, int trans)
    {
        SalesForce SFD1 = new SalesForce();
        int ds1 = -1;
        ds1 = SFD1.PermamentDeleterow(divcode.TrimEnd(','), trans);
        return ds1;
    }

    [WebMethod]
    public static string UpdateDelMsgData(string data)
    {
        string msg = string.Empty;
        Bus_EReport.SalesForce.updateImportantData Data = JsonConvert.DeserializeObject<Bus_EReport.SalesForce.updateImportantData>(data);
        SalesForce dss = new SalesForce();
        msg = dss.updateDelMessageData(Data);
        return msg;
    }
}