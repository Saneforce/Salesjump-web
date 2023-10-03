using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using Bus_EReport;
using System.Data;

public partial class MasterFiles_SalesMan_List : System.Web.UI.Page
{
    public static DataTable dt = new DataTable();
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
        else if (sf_type == "4" || sf_type == "5")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession=true)]
    public static string getSalesMan(string divcode,string SF)
    {
        DSM dsm = new DSM();
        DataSet ds = new DataSet();
        if(SF=="admin")
        {
            SF = null;
            ds = dsm.getSalesmanList(divcode,SF);
        }
        else
        {
            ds = dsm.getSalesmanList(divcode, SF);
        }
        return JsonConvert.SerializeObject(ds.Tables[0]); ;
    }

    [WebMethod(EnableSession=true)]
    public static int SetNewStatus(string SF, string stus)
    {
        DSM dsm = new DSM();
        int iReturn = dsm.DeActivate(SF, stus);
        return iReturn;
    }
}