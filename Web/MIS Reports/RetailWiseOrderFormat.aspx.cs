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

public partial class MIS_Reports_SampleReport : System.Web.UI.Page
{
    #region "Declaration"
    string div_code = string.Empty;
   static string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSalesForce = null;
   static DataSet dds = null;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if(!IsPostBack)
        {
            //ddlfieldFunction();

        }
        
    }

    //protected void ddlfieldFunction()
    //{
    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.UserList_getMR(div_code, sf_code);
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
         dds = SFD.UserList_getMR(divcode,sf_code);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    protected void ddlfieldforce_SelectedIndexChanged(object sender, EventArgs e)
    {
        //TourPlan tour = new TourPlan();
        //dsSalesForce = tour.get_TPPlanRoute(ddlfieldforce.SelectedValue.ToString(), div_code);
        //if (dsSalesForce.Tables[0].Rows.Count > 0)
        //{
        //    ddlRoute.DataTextField = "Territory_Name";
        //    ddlRoute.DataValueField = "Territory_Code";
        //    ddlRoute.DataSource = dsSalesForce;
        //    ddlRoute.DataBind();
        //    ddlRoute.Items.Insert(0, new ListItem("----Select Route---", "0"));
        //}
        //else
        //{
        //    ddlRoute.DataSource = null;
        //    ddlRoute.Items.Clear();
        //    ddlRoute.Items.Insert(0, new ListItem("----Select Route---", "0"));

        //}
    }

    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        //TourPlan tour = new TourPlan();
        //dsSalesForce = tour.get_TPPlanRetailers(ddlfieldforce.SelectedValue.ToString(), div_code, ddlRoute.SelectedValue.ToString());
        //if (dsSalesForce.Tables[0].Rows.Count > 0)
        //{
        //    ddlRetailer.DataTextField = "ListedDr_Name";
        //    ddlRetailer.DataValueField = "ListedDrCode";
        //    ddlRetailer.DataSource = dsSalesForce;
        //    ddlRetailer.DataBind();
        //    ddlRetailer.Items.Insert(0, new ListItem("----Select Retailer---", "0"));
        //}
        //else
        //{
        //    ddlRetailer.DataSource = null;
        //    ddlRetailer.Items.Clear();
        //    ddlRetailer.Items.Insert(0, new ListItem("----Select Retailer---", "0"));
        //}

    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    string url;
        //    url = "ReportFinal.aspx?FieldForce=" + ddlfieldforce.SelectedValue.ToString() + "&Route=" + ddlRoute.SelectedValue.ToString() +
        //        "&Retailer=" + ddlRetailer.SelectedValue.ToString() + "&FromDate=" + HiddenField1.Value.ToString() + "&EndDate=" + HiddenField2.Value.ToString();
        //    string newWin = "window.open('" + url + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
        //    ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
        //}
        //catch (Exception)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select!!!');</script>");
        //}
        //Response.Redirect(url);
        //Response.Redirect("ReportFinal.aspx");

    }

    [WebMethod]
    public static string GetRoute(string divcode,string sf_code)
    {
        TourPlan tour = new TourPlan();
        dds = tour.get_TPPlanRoute(divcode,sf_code);
        //dds = tour.get_TPPlanRoute(sf_code,divcode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }

    [WebMethod]
    public static string GetRetailer(string sf_code, string div_code, string routecode)
    {
        TourPlan tour = new TourPlan();
        dds = tour.get_TPPlanRetailers(sf_code, div_code,routecode);
        return JsonConvert.SerializeObject(dds.Tables[0]);
    }   
}