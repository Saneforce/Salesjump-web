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

public partial class MasterFiles_StarViewMail : System.Web.UI.Page
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
    public static string GetStarData(string divcode, string sfcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        ds = SFD.ReadStarData(divcode.TrimEnd(','), sfcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}