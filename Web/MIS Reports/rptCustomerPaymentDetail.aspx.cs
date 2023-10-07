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

public partial class MIS_Reports_rptCustomerPaymentDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hSubDiv.Value = Request.QueryString["SubDiv"].ToString();
        hFYear.Value = Request.QueryString["FYear"].ToString();
        hFMonth.Value = Request.QueryString["FMonth"].ToString();
        HSFCode.Value = Request.QueryString["sfCode"].ToString();        
        hsfName.Value = Request.QueryString["sfName"].ToString();
        lblHead.Text = "Customer Payment Details on " + Request.QueryString["FYear"].ToString() + " To " + Request.QueryString["FMonth"].ToString();
        Label1.Text ="Field Force :"+ Request.QueryString["sfName"].ToString();
    }
    public class PaymentDtls
    {
        public string custCode { get; set; }
        public string custName { get; set; }
        public string RouteName { get; set; }
        public string Distname { get; set; }
        public string cnt { get; set; }
        public string Amt { get; set; }       
    }

    [WebMethod]
    public static List<PaymentDtls> GetDetails(string SF_Code, string FDate, string TDate)
    {

        DateTime fdt = DateTime.ParseExact(FDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime tdt = DateTime.ParseExact(TDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        List<PaymentDtls> Lists = new List<PaymentDtls>();
        Order ord = new Order();
        DataSet dsSalesForce = ord.get_Payment_Detals_SFWise(SF_Code, fdt.ToString("yyyy-MM-dd"), tdt.ToString("yyyy-MM-dd"));

        foreach (DataRow row in dsSalesForce.Tables[0].Rows)
        {
            PaymentDtls list = new PaymentDtls();
            list.custCode = row["cust_id"].ToString();
            list.custName = row["cus_name"].ToString();
            list.RouteName = row["territory_name"].ToString();
            list.Distname = row["stockist_name"].ToString();
            list.cnt = row["cnt"].ToString();
            list.Amt = row["Amt"].ToString();
            Lists.Add(list);
        }
        return Lists.ToList();
    }
}