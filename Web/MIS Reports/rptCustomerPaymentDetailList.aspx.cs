using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MIS_Reports_rptCustomerPaymentDetailList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
   {

        hFYear.Value = Request.QueryString["FDate"].ToString();
        hFMonth.Value = Request.QueryString["TDate"].ToString();
        HCustCode.Value = Request.QueryString["custCode"].ToString();
        hsfName.Value = Request.QueryString["sfName"].ToString();
        lblHead.Text = "Customer Payment Details on " + Request.QueryString["FDate"].ToString() + " To " + Request.QueryString["TDate"].ToString();
        Label1.Text = "Field Force :" + Request.QueryString["sfName"].ToString();
    }
    public class PaymentDtls
    {
        public string custCode { get; set; }
        public string custName { get; set; }
        public string RouteName { get; set; }
        public string Distname { get; set; }
        
        public string Amount { get; set; }
        public string Pay_Mode { get; set; }
        public string Pay_Date { get; set; }
        public string Remarks { get; set; }
    }

    [WebMethod]
    public static List<PaymentDtls> GetDetails(string custCode, string FDate, string TDate)
    {

        DateTime fdt = DateTime.ParseExact(FDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime tdt = DateTime.ParseExact(TDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        List<PaymentDtls> Lists = new List<PaymentDtls>();
        Order ord = new Order();
        DataSet dsSalesForce = ord.get_Payment_Detals_CustomerWise(custCode, fdt.ToString("yyyy-MM-dd"), tdt.ToString("yyyy-MM-dd"));

        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            PaymentDtls list = new PaymentDtls();
            list.custCode = row["cust_id"].ToString();
            list.custName = row["cus_name"].ToString();
            list.RouteName = row["territory_name"].ToString();
            list.Distname = row["stockist_name"].ToString();            
            list.Amount = row["Amount"].ToString();
            list.Pay_Mode = row["Pay_Mode"].ToString();
            list.Pay_Date = row["Pay_Date"].ToString();
            list.Remarks = row["Remarks"].ToString();
            Lists.Add(list);
        }
        return Lists.ToList();
    }
}