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

public partial class MIS_Reports_rpt_SOB_POB_Order_Report_ : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        hFYear.Value = Request.QueryString["FYear"].ToString();
        hFMonth.Value = Request.QueryString["FMonth"].ToString();
        hSubDiv.Value = Request.QueryString["SubDiv"].ToString();
        hsfCode.Value = Request.QueryString["SfCode"].ToString();
        hdesignation.Value = Request.QueryString["designation"].ToString();
        try
        {
            hdivCode.Value = Session["Div_Code"].ToString();
        }
        catch
        {
            hdivCode.Value = Session["Division_Code"].ToString();
        }
        Label1.Text = "SOB POB Orders for From " + Request.QueryString["FYear"].ToString() + " To " + Request.QueryString["FMonth"].ToString();
        Label2.Text = "Team : " + Request.QueryString["sfName"].ToString();
    }
    public class FFO
    {
        public string sfCode { get; set; }
        public string sfName { get; set; }
    }

    [WebMethod]
    public static List<FFO> GetFieldForce(string sfCode)
    {

        List<FFO> Lists = new List<FFO>();
        SalesForce sf = new SalesForce();
        string divcode = HttpContext.Current.Session["div_code"].ToString();
      //  string sfCode = HttpContext.Current.Session["sf_code"].ToString();
        DataSet dsSalesForce = sf.SalesForceList(divcode, sfCode, "0");

        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            FFO list = new FFO();
            list.sfCode = row["sf_code"].ToString();
            list.sfName = row["sf_name"].ToString();
            Lists.Add(list);
        }
        return Lists.ToList();
    }

    public class PriVal
    {
        public string type { get; set; }
        public string sfCode { get; set; }
        public string sfName { get; set; }
        public string rtCode { get; set; }
        public string rtName { get; set; }
        public string sDate { get; set; }
        public string RoteName { get; set; }
        public string Working_with { get; set; }
        public string POBVal { get; set; }
        public string SOB_Val { get; set; }
        public string instrument_type { get; set; }
        public string POB { get; set; }
        public string Emp_Code { get; set; }
        public string Address1 { get; set; }

        public string SOP_Qty { get; set; }
        public string POB_Qty { get; set; }
        public string Phone_Ooder { get; set; }
        
    }


    [WebMethod]
    public static List<PriVal> GetPrimaryDt(string subDiv, string FYear, string FMonth, string sfCode, string Designation)
    {

        List<PriVal> Lists = new List<PriVal>();
        Order sf = new Order();
        string divcode = HttpContext.Current.Session["div_code"].ToString();


        DateTime dt = DateTime.ParseExact(FYear, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime dt2 = DateTime.ParseExact(FMonth, "dd/MM/yyyy", CultureInfo.InvariantCulture);


        DataSet dsSalesForce = sf.GetDistVal(divcode, dt.ToString("yyyy/MM/dd"), dt2.ToString("yyyy/MM/dd"), sfCode, Designation);

        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            PriVal list = new PriVal();
            list.type = row["TYP"].ToString();
            list.sfCode = row["sf_code"].ToString();
            list.sfName = row["sf_name"].ToString();
            list.rtCode = row["Trans_Detail_Info_Code"].ToString();
            list.rtName = row["ListedDr_Name"].ToString();
            list.Working_with = row["Worked_with_Name"].ToString();
            list.RoteName = row["SDP_Name"].ToString();

            list.Address1 = row["ListedDr_Address1"].ToString();
            list.Emp_Code = row["Employee_Id"].ToString();

            list.sDate = row["Activity_Date"].ToString();
            list.POBVal = row["POB_Value"].ToString();
            list.SOB_Val = row["SOB_Val"].ToString();

            list.instrument_type = row["instrument_type"].ToString();
            list.POB = row["POB"].ToString();
            list.SOP_Qty = row["SOB_Qty"].ToString();
            list.POB_Qty = row["POB_Qty"].ToString();
            list.Phone_Ooder = row["phoneOrder"].ToString();
            Lists.Add(list);
        }
        return Lists.ToList();
    }

}