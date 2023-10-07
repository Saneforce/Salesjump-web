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

public partial class MasterFiles_SalesForce_HQ_Creation : System.Web.UI.Page
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
    public static string getHQ(string divcode)
    {
        SalesForce ast = new SalesForce();
        DataSet ds = ast.getAllSFHQ(divcode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }


    [WebMethod(EnableSession = true)]
    public static int SetNewStatus(string SF, string stus)
    {
        SalesForce ast = new SalesForce();
        int iReturn = ast.sfHQDeActivate(SF, stus);
        return iReturn;
    }
    
}