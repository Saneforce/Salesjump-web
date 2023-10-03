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

public partial class MasterFiles_ReadDeleteMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string GetReadDeleteData(string divcode, string sfcode, int trans)
    {
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        ds = SFD.ReadDeleteMailData(divcode.TrimEnd(','), sfcode, trans);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}