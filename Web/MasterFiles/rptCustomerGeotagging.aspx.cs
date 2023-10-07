using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Globalization;


public partial class MasterFiles_rptCustomerGeotagging : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hidn_sf_code.Value = Request.QueryString["SF_Code"].ToString();
        //hFYear.Value = Request.QueryString["FYear"].ToString();
        //hFMonth.Value = Request.QueryString["FMonth"].ToString();
        //DateTimeFormatInfo mfi = new DateTimeFormatInfo();
        //string strFmonth = mfi.GetMonthName(Convert.ToInt32(Request.QueryString["FMonth"].ToString())).ToString(); //August
        //lblyear.Text = strFmonth.Substring(0, 3) + " - " + Request.QueryString["FYear"].ToString() ;
        lblyear.Text="Field Force Name : "+ Request.QueryString["SF_Name"].ToString();
    }

    [WebMethod]
    public static List<distributor> GetGEORetailers(string SF_Code)
    {
        ListedDR stk = new ListedDR();
        List<distributor> HDay = new List<distributor>();
        string divcode = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsAlowtype = null;
        dsAlowtype = stk.GetGEORetailers(divcode,  SF_Code,"","","");
        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            distributor d = new distributor();
            d.custCode = row["Cust_Code"].ToString();
            d.custName = row["ListedDr_Name"].ToString();
            d.routeCode = row["Territory_Name"].ToString();
            d.lat = row["lat"].ToString();
            d.log = row["long"].ToString();
            d.createDate = row["ListedDr_Created_Date"] == DBNull.Value ? "" : Convert.ToDateTime(row["ListedDr_Created_Date"]).ToString("dd/MM/yyyy");
            d.flg = row["ListedDr_Active_Flag"].ToString();
            d.curAddress = row["ListedDr_Address1"].ToString();
            HDay.Add(d);
        }
        return HDay;
    }

    public class distributor
    {
        public string custCode { get; set; }
        public string custName { get; set; }
        public string routeCode { get; set; }
        public string lat { get; set; }
        public string log { get; set; }
        public string createDate { get; set; }
        public string flg { get; set; }
        public string curAddress { get; set; }
    }    
    [WebMethod(EnableSession = true)]
    public static string untagRetailers(string custCode)
    {
        string err = "";
        int iReturn = -1;
        ListedDR ldr = new ListedDR();
        try
        {

            string div_code = HttpContext.Current.Session["div_code"].ToString();            
            iReturn = ldr.untagRetailers_confirm(custCode);

            if (iReturn > 0)
            {
                err = "Sucess";
            }
            else
            {
                err = "Error";
            }
        }
        catch (Exception ex)
        {
            err = "Error";
        }
        return err;
    }
}