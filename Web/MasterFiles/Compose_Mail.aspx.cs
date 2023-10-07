using Bus_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Compose_Mail : System.Web.UI.Page
{

    string sf_type = string.Empty;
    public static string sfcode = string.Empty;
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
        sfcode = Session["sf_code"].ToString();
    }

    //[WebMethod]
    //public static List<string> GetAutoCompleteData(string divcode, string username)
    //{

    //   // divcode = HttpContext.Current.Session["div_code"].ToString();
    //    List<string> result = new List<string>();
    //    DataSet ds = null;
    //    SalesForce sm = new SalesForce();
    //    ds = sm.getusrmail_All(divcode, username);
    //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //    {
    //        result.Add(ds.Tables[0].Rows[i]["Sf_Name"].ToString());
    //    }
    //    return result;
    //    //return  JsonConvert.SerializeObject(ds.Tables[0]);

    //}

    [WebMethod]
    public static string GetAutoCompleteData(string divcode)
    {

        // divcode = HttpContext.Current.Session["div_code"].ToString();
        List<string> result = new List<string>();
        DataSet ds = null;
        SalesForce sm = new SalesForce();
        ds = sm.GetFieldForceDetails(divcode, sfcode);
        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{
        //    result.Add(ds.Tables[0].Rows[i]["Sf_Name"].ToString());
        //}
        return JsonConvert.SerializeObject(ds.Tables[0]);
       // return result;
        //return  JsonConvert.SerializeObject(ds.Tables[0]);

    }

    [WebMethod]
    public static string saveData(string data)
    {
        string msg = string.Empty;
        Bus_EReport.SalesForce.SaveComposeMailData Data = JsonConvert.DeserializeObject<Bus_EReport.SalesForce.SaveComposeMailData>(data);
        SalesForce dss = new SalesForce();
        msg = dss.saveNewComposeMailData(Data);
        return msg;
    }


    [WebMethod]
    public static string GetReadData(string divcode, string sfcode, int trans)
    {
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        ds = SFD.ReadMailData(divcode.TrimEnd(','), sfcode, trans);
        //ds = SFD.ReadSentMailData(divcode.TrimEnd(','), sfcode, trans);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string ForwardMsgSaveData(string data)
    {
        string msg = string.Empty;
        Bus_EReport.SalesForce.SaveComposeMailData Data = JsonConvert.DeserializeObject<Bus_EReport.SalesForce.SaveComposeMailData>(data);
        SalesForce dss = new SalesForce();
        msg = dss.saveForwardMailData(Data);
        return msg;
    }

}