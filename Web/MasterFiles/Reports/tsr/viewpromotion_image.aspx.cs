using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using Bus_EReport;
using Newtonsoft.Json;


public partial class MasterFiles_Reports_viewpromotion_image : System.Web.UI.Page
{
    public static string Ekey = string.Empty;
    public static string sf = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Ekey = Request.QueryString["Ekey"].ToString(); 
        sf = Request.QueryString["sfn"].ToString();
        Label2.Text = "FieldForce Name: " + sf;
    }
    [WebMethod]
    public static string promotionimg()
    {
        Product SFD = new Product();
        DataSet ds = SFD.fillpromotiondtl_img(Ekey);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}