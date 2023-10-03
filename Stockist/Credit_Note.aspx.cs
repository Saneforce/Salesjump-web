using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using Newtonsoft.Json;
using System.Web.Services;
using System.Transactions;

public partial class Stockist_Credit_Note : System.Web.UI.Page
{
    public static DataSet ds;
    public static string Div_code = string.Empty;
    public static StockistMaster sm = new StockistMaster();
    public static string Stockist_Code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string Get_Inv_customer()
    {
        Div_code = HttpContext.Current.Session["div_code"].ToString();
        Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        ds = sm.Bind_Invoice_Cust(Div_code, Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod]
    public static string Get_cust_inv_no(string Retailer_ID)
    {
        Div_code = HttpContext.Current.Session["div_code"].ToString();
        Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        ds = sm.Bind_Cust_Inv_no(Div_code, Stockist_Code, Retailer_ID);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string GetProductDetails()
    {
        ds = sm.getprodDet(Div_code.TrimEnd(','), Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string getreate(string Div_Code, string Stockist_Code)
    {
        string State = HttpContext.Current.Session["State"].ToString();
        ds = sm.get_rate_new_bystk(Div_Code.TrimEnd(','), Stockist_Code, State);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string Get_invoice_pro_details(string inv_no)
    {
        ds = sm.Get_inv_Pro_details(Div_code.TrimEnd(','), Stockist_Code, inv_no);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Get_SalesMan()
    {
        ds = sm.Get_SalesMan_details(Div_code.TrimEnd(','), Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Get_Pre_credit_details(string Cust_ID)
    {
        ds = sm.Get_Pre_credit_details(Cust_ID,Div_code.TrimEnd(','), Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string View_credit_details(string Credit_no)
    {
        ds = sm.View_credit_details(Credit_no, Div_code.TrimEnd(','), Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    
    public class General_details
    {
        public string Inv_no { get; set; }
        public string Date { get; set; }
        public string Cust_id { get; set; }
        public string Cust_Name { get; set; }
        public string Amount { get; set; }
        public string Remarks { get; set; }
        public string Div_Code { get; set; }
        public string Sales_Man_ID { get; set; }
        public string Sf_Code { get; set; }
        public string Credit_note_no { get; set; }
    }

    public class Pro_details
    {
        public string ProductCode {get; set;}
        public string Productname {get; set;}
        public string Productunit {get; set;}
        public string Price { get; set;}
        public string Conv_qty { get; set;}
        public string Non_conv_qty { get; set; }
        public string Amt { get; set;}
        public string Inv_no { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static string Save_Credit(string Credit_Main, string Credit_Details)
    {
        string msg = string.Empty;
        int getreturn;
        using (var scope = new TransactionScope())
        {
            try
            {
                IList<General_details> general_details = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<General_details>>(Credit_Main);
                IList<Pro_details> pro_details = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Pro_details>>(Credit_Details);

                string sxmlGen_detils = "<ROOT>";
                for (int i = 0; i < general_details.Count; i++)
                {
                    sxmlGen_detils += "<Details Inv_Number=\"" + general_details[i].Inv_no + "\" Inv_Date=\"" + general_details[i].Date + "\" Cust_Code=\"" + general_details[i].Cust_id + "\" Cust_Name=\"" + general_details[i].Cust_Name + "\" Amt=\"" + general_details[i].Amount + "\" Remark=\"" + general_details[i].Remarks + "\" Div=\"" + general_details[i].Div_Code + "\" Sales_Man_ID=\"" + general_details[i].Sales_Man_ID + "\" Sf_Code=\"" + general_details[i].Sf_Code + "\" Credit_note_no=\"" + general_details[i].Credit_note_no + "\" />"; 
                }
                sxmlGen_detils += "</ROOT>";

                string sxmlPro_detils = "<ROOT>";
                for (int i = 0; i < pro_details.Count; i++)
                {
                    sxmlPro_detils += "<Prod Product_Code=\"" + pro_details[i].ProductCode + "\" Product_Name=\"" + pro_details[i].Productname + "\" Product_unit=\"" + pro_details[i].Productunit + "\" Price=\"" + pro_details[i].Price + "\" Quantity=\"" + pro_details[i].Conv_qty + "\" Amount=\"" + pro_details[i].Amt + "\" qty=\"" + pro_details[i].Non_conv_qty + "\" Inv_No=\""+pro_details[i].Inv_no + "\" />";
                }
                sxmlPro_detils += "</ROOT>";

                getreturn = sm.insert_Credit(sxmlGen_detils, sxmlPro_detils);
                scope.Complete();
                scope.Dispose();
            }
            catch (Exception ex)
            {
                scope.Dispose();
                throw ex;
            }
            return "success";
        }
    }
}
