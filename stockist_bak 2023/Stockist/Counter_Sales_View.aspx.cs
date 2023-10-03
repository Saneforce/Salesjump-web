using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Globalization;


public partial class Stockist_Counter_Sales_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public class CounterDetails
    {
        public string Order_No { get; set; }
        public string Cus_Name { get; set; }
        public string Order_Date { get; set; }
        public string Pay_Term { get; set; }
        public string Total { get; set; }
        public string Price { get; set; }
        public string Discount { get; set; }

        public string detailtotal { get; set; }
        public string Product_Name { get; set; }


        public string Amount { get; set; }


    }


    [WebMethod(EnableSession = true)]
    public static CounterDetails[] GetCounterDetails(string FDT, string TDT)
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        List<CounterDetails> Counter_Sales = new List<CounterDetails>();
        ds = sm.getallCounterSalesDetails(Stockist_Code,FDT,TDT,div_code);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            CounterDetails cs = new CounterDetails();
            cs.Order_No = (row["Order_No"]).ToString();
            cs.Cus_Name = (row["Cus_Name"]).ToString();
            cs.Order_Date = (row["Order_Date"]).ToString();
            cs.Pay_Term = (row["Pay_Term"]).ToString();
            cs.Total = (row["Total"]).ToString();
       //     cs.Price = (row["Price"]).ToString();
            cs.Discount = (row["Dis_total"]).ToString();
           // cs.detailtotal = (row["total"]).ToString();
           // cs.Product_Name = (row["Product_Name"]).ToString();
            //cs.Amount = (row["Amount"]).ToString();
            Counter_Sales.Add(cs);
        }

        return Counter_Sales.ToArray();
    }
}