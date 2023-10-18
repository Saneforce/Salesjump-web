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

public partial class MasterFiles_Mas_Asset_Request : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string GetDetails(string divcode)
    {
        DCR dv = new DCR();
        DataTable ds = new DataTable();
        ds = dv.getDataTable("exec getAssetMaster '" + divcode + "'");
        return JsonConvert.SerializeObject(ds);
    }
    public class loc
    {

    }
}