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
using DBase_EReport;

public partial class SuperStockist_Sales_CreditNote_SS_Credit_Note : System.Web.UI.Page
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
        ds = Bind_Invoice_Cust(Div_code, Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet Bind_Invoice_Cust(string Div_code, string Stockist_Code)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = "EXEC Bind_ss_Inv_cust   '" + Div_code + "','" + Stockist_Code + "'";
        //string strQry = "EXEC Bind_Inv_cust '" + Div_code + "','" + Stockist_Code + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }

    [WebMethod]
    public static string Get_cust_inv_no(string Retailer_ID)
    {
        Div_code = HttpContext.Current.Session["div_code"].ToString();
        Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        ds = Bind_Cust_Inv_no(Div_code, Stockist_Code, Retailer_ID);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet Bind_Cust_Inv_no(string Div_code, string Stockist_Code, string Retailer_ID)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "EXEC Bind_Cust_Invoice_no '" + Div_code + "','" + Stockist_Code + "','" + Retailer_ID + "'";

        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }

    [WebMethod(EnableSession = true)]
    public static string GetProductDetails()
    {
        Div_code = HttpContext.Current.Session["div_code"].ToString();
        Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        ds = getprodDet(Div_code.TrimEnd(','), Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static  DataSet getprodDet(string Div_Code, string Stockist_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        string strQry = "exec sp_Get_SS_ProductTaxDetails '" + Div_Code + "','" + Stockist_Code + "'";
        //string strQry = "exec sp_GetProductTaxDetails '" + Div_Code + "','" + Stockist_Code + "'";

        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    [WebMethod(EnableSession = true)]
    public static string getreate(string Div_Code, string Stockist_Code)
    {
        string State = HttpContext.Current.Session["State"].ToString();
        ds = get_rate_new_bystk(Div_Code.TrimEnd(','), Stockist_Code, State);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet get_rate_new_bystk(string Div_Code, string Stockist_Code, string State)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec get_SS_rate_new '" + Div_Code + "','" + Stockist_Code + "','" + State + "'";
        //string strQry = "Exec getrate_new '" + Div_Code + "','" + Stockist_Code + "','" + State + "'";

        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    [WebMethod(EnableSession = true)]
    public static string Get_invoice_pro_details(string inv_no)
    {
       
        ds = sm.Get_inv_Pro_details(Div_code.TrimEnd(','), Stockist_Code, inv_no);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Get_inv_Pro_details(string Div_code, string Stockist_Code, string inv_no)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = "EXEC Bind_ss_Inv_Pro_details '" + Div_code + "','" + Stockist_Code + "','" + inv_no + "'";
        //string strQry = "EXEC Bind_Inv_Pro_details '" + Div_code + "','" + Stockist_Code + "','" + inv_no + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    [WebMethod(EnableSession = true)]
    public static string Get_SalesMan()
    {
        ds = Get_SalesMan_details(Div_code.TrimEnd(','), Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public  static DataSet Get_SalesMan_details(string Div_code, string Stockist_Code)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "EXEC Bind_SS_Sales_Man_details '" + Div_code + "','" + Stockist_Code + "'";
        //string strQry = "EXEC Bind_Sales_Man_details '" + Div_code + "','" + Stockist_Code + "'";

        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    [WebMethod(EnableSession = true)]
    public static string Get_Pre_credit_details(string Cust_ID)
    {
        ds = Get_Pre_credit_details(Cust_ID, Div_code.TrimEnd(','), Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static  DataSet Get_Pre_credit_details(string Cust_ID, string Div_Code, string Stockist_Code)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string  strQry = "Exec Get_Pre_credit_details '" + Cust_ID + "','" + Stockist_Code + "','" + Div_Code + "'";

        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    [WebMethod(EnableSession = true)]
    public static string View_credit_details(string Credit_no)
    {
        ds = View_credit_details(Credit_no, Div_code.TrimEnd(','), Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet View_credit_details(string Credit_no, string Div_Code, string Stockist_Code)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string  strQry = "Exec Sp_View_credit_details '" + Credit_no + "'";

        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
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
        public string ProductCode { get; set; }
        public string Productname { get; set; }
        public string Productunit { get; set; }
        public string Price { get; set; }
        public string Conv_qty { get; set; }
        public string Non_conv_qty { get; set; }
        public string Amt { get; set; }
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
                    sxmlPro_detils += "<Prod Product_Code=\"" + pro_details[i].ProductCode + "\" Product_Name=\"" + pro_details[i].Productname + "\" Product_unit=\"" + pro_details[i].Productunit + "\" Price=\"" + pro_details[i].Price + "\" Quantity=\"" + pro_details[i].Conv_qty + "\" Amount=\"" + pro_details[i].Amt + "\" qty=\"" + pro_details[i].Non_conv_qty + "\" Inv_No=\"" + pro_details[i].Inv_no + "\" />";
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