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

public partial class MIS_Reports_SecondaryOrder_Summary : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    public static string divcode = string.Empty;
    public static string sfcode1 = string.Empty;
    public static string fdt = string.Empty;
    public static string tdt = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //sfcode1 = Session["sf_code"].ToString();
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            //ddlfieldFunction();   
            sfcode1 = Session["sf_code"].ToString();
        }
    }
    //protected void ddlfieldFunction()
    //{
    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.UserList_getMR(divcode);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlfieldforce.DataTextField = "sf_name";
    //        ddlfieldforce.DataValueField = "sf_code";
    //        ddlfieldforce.DataSource = dsSalesForce;
    //        ddlfieldforce.DataBind();
    //        ddlfieldforce.Items.Insert(0, new ListItem("---Select Base Level---", "0"));
    //    }
    //    else
    //    {
    //        ddlfieldforce.DataSource = null;
    //        ddlfieldforce.Items.Clear();
    //        ddlfieldforce.Items.Insert(0, new ListItem("---Select Base Level---", "0"));

    //    }
    //}

    [WebMethod]
    public static string getMGR(string divcode)
    {
        SalesForce SFD = new SalesForce();
        DataSet dds = SFD.UserList_getMR(divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string GetData(string SF, string Div, string Mn, string Yr)
    {
        divcode = Div;
        sfcode1 = SF;
        fdt = Mn;
        tdt = Yr;
        SalesForce SFD = new SalesForce();
        DataSet ds = new DataSet();
        ds = SFD.GetFieldForceDetails(SF, Div.TrimEnd(','), Mn, Yr);
        //ds = SFD.GetFieldForceDetails(SF, Div, Mn, Yr);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}