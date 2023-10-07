using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Http;
using Razorpay.Api;
using System.Net;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
/// <summary>
/// Summary description for PaymentService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class PaymentService : System.Web.Services.WebService
{

    public PaymentService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public string Get_Order_ID(string Order_Amt,string Stk, string Div)
    {
        try
        {
            string orderId = string.Empty;
            Random randomObj = new Random();
            string transactionId = randomObj.Next(10000000, 100000000).ToString();

            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select Stockist_Code,KeyID,Key_Secret from Mas_Stockist ms inner join Mas_Payment_Keys pk on ms.Payment_Key_ID=pk.Sl_No where Stockist_Code='" + Stk + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            string Key_id = (ds.Tables[0].Rows[0]["KeyID"]).ToString();
            string secret_key = (ds.Tables[0].Rows[0]["Key_Secret"]).ToString();

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(Key_id, secret_key);
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", Order_Amt);  // Amount will in paise
            options.Add("receipt", transactionId);
            options.Add("currency", "INR");
            options.Add("payment_capture", "1"); // 1 - automatic  , 0 - manual
                                                 //options.Add("notes", "-- You can put any notes here --");
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            orderId = orderResponse["id"].ToString();
            return orderId;
        }
        catch
        {
            return "44";
        }
    }
}
