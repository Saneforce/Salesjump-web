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

public partial class MIS_Reports_Monthly_POB_Summary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string GetMnthly_Pob(string Div, string Year)
    {
        DataSet ds = new DataSet();
        SalesForce Ad = new SalesForce();
        ds = Ad.GetMonthly_POB_Summary(Div, Year);
        return JsonConvert.SerializeObject(ds.Tables[0]);         
    }
	 [WebMethod]
    public static bindyear[] BindDate(string divcode)
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();
        dsTP = tp.Get_TP_Edit_Year(divcode);
        List<bindyear> sf = new List<bindyear>();
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                bindyear rt = new bindyear();
                rt.value = k.ToString();
                rt.text = k.ToString();
                sf.Add(rt);
            }
        }
        return sf.ToArray();
    }
    public class bindyear
    {
        public string value { get; set; }
        public string text { get; set; }
    }
}