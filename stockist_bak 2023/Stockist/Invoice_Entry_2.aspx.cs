using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Transactions;
using System.Collections;
using DBase_EReport;


public partial class Invoice_Entry_2 : System.Web.UI.Page
{
    string dis_code = string.Empty; 
    public static DataSet ds = null;
    string cus_code = string.Empty;
    string or_date = string.Empty;
    DataSet dsSf = null;
    DataSet dsSf_or = null;
    DataSet tax = null;
    string div_code = string.Empty;
    DataSet dsTP = null;
    public static string Div_Code = string.Empty;
    public static StockistMaster sm = new StockistMaster();
    public static string Stockist_Code = string.Empty;
    public static string Sf_Type = string.Empty;
    public static SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Convert.ToString(Session["div_code"]);
        Div_Code = Convert.ToString(Session["div_code"]);
        Div_Code = Div_Code.TrimEnd(',');
        Stockist_Code = Convert.ToString(Session["Sf_Code"]);
        div_code = div_code.TrimEnd(',');
        Sf_Type = Convert.ToString(Session["sf_type"]);
    }

    public class Tax_Details
    {
        public string Exp_code { get; set; }
        public string Exp_name { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static Tax_Details[] GetAllType()
    {

        DCR dc = new DCR();
        List<Tax_Details> FFD = new List<Tax_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        DataSet Tax_mas = null;
        Tax_mas = dc.view_tax_Mas(Div_code.TrimEnd(','));
        foreach (DataRow row in Tax_mas.Tables[0].Rows)
        {
            Tax_Details ffd = new Tax_Details();
            ffd.Exp_code = row["Tax_Val"].ToString();
            ffd.Exp_name = row["Tax_Name"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }

    public class Trans_Details
    {
        public string Trans_code { get; set; }
        public string Trans_name { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static Trans_Details[] GetTransType()
    {
        DCR dc = new DCR();
        List<Trans_Details> FFD = new List<Trans_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        DataSet Trans_mas = null;
        Trans_mas = dc.view_Trans_Mas(Div_code.TrimEnd(','));
        foreach (DataRow row in Trans_mas.Tables[0].Rows)
        {
            Trans_Details ffd = new Trans_Details();
            ffd.Trans_code = row["Trans_Code"].ToString();
            ffd.Trans_name = row["Trans_Name"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }

    public class Pay_Details
    {
        public string Pay_code { get; set; }
        public string Pay_name { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static Pay_Details[] GetPayType()
    {
        DCR dc = new DCR();
        List<Pay_Details> FFD = new List<Pay_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        DataSet Trans_mas = null;
        Trans_mas = dc.view_Pay_Mas(Div_code.TrimEnd(','));
        foreach (DataRow row in Trans_mas.Tables[0].Rows)
        {
            Pay_Details ffd = new Pay_Details();
            ffd.Pay_code = row["Code"].ToString();
            ffd.Pay_name = row["Name"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static string SaveInvoice(string General_details, string Pro_details, string Tax_details, string new_order_no)
    {
        string msg = string.Empty;
        string ord_date = string.Empty;
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Type = HttpContext.Current.Session["sf_type"].ToString();
        StockistMaster sm = new StockistMaster();
        Product prd = new Product();
        DataSet ds = new DataSet();
        Order Ord = new Order();
        string Invoice_no = string.Empty;
        if (conn != null && conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        SqlCommand cmd = new SqlCommand();
        string sqlQry1 = "SELECT isnull(max(trans_inv_no)+1,'1')trans_inv_no from Trans_Invoice_Head";
        cmd = new SqlCommand(sqlQry1);
        cmd.Connection = conn;
        int invid_code = Convert.ToInt32(cmd.ExecuteScalar());
        using (var scope = new TransactionScope())
        {
            try
            {
                IList<General_details> general_details = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<General_details>>(General_details);
                IList<Pro_details> pro_details = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Pro_details>>(Pro_details);
                IList<Tax_details> tax_details = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Tax_details>>(Tax_details);

                string sxmlGen_detils = "<ROOT>";
                for (int i = 0; i < general_details.Count; i++)
                {
                    sxmlGen_detils += "<Details Sf_Code=\"" + general_details[i].sf_code + "\" Sf_Name=\"" + general_details[i].sf_name + "\" Dis_Code=\"" + general_details[i].dis_code + "\" Dis_Name=\"" + general_details[i].dis_name + "\" Cust_Code=\"" + general_details[i].cus_code + "\" Cust_Name=\"" + general_details[i].cus_name + "\" Order_Date=\"" + general_details[i].order_date + "\" Inv_Date=\"" + general_details[i].inv_date + "\" Del_Date=\"" + general_details[i].del_date + "\" Pay_Term=\"" + general_details[i].pay_term + "\" Pay_Due=\"" + general_details[i].pay_due + "\" Ship_Method=\"" + general_details[i].ship_mtd + "\" Ship_Term=\"" + general_details[i].ship_term + "\" Sub_Total=\"" + general_details[i].sub_total + "\" Tax_Total=\"" + general_details[i].tax_total + "\" Total=\"" + general_details[i].total + "\" Adv_Paid=\"" + general_details[i].adv_paid + "\" Amt_Due=\"" + general_details[i].Amt_due + "\" Remark=\"" + general_details[i].remark + "\"  Dis_total=\"" + general_details[i].Dis_total + "\" Order_No=\"" + general_details[i].order_no + "\" Div_Code=\"" + general_details[i].Div_Code + "\" Advance_amount=\"" + general_details[i].Advance_amount + "\" adjust_amt=\"" + general_details[i].adjust_amt + "\" Reference_no=\"" + general_details[i].Reference_no + "\" CGST=\"" + general_details[i].CGST + "\" SGST=\"" + general_details[i].SGST + "\" IGST=\"" + general_details[i].IGST + "\" TCS=\"" + general_details[i].TCS + "\" TDS=\"" + general_details[i].TDS + "\"  />";
                }
                sxmlGen_detils += "</ROOT>";

                string sxmlpro_detils = "<ROOT>";
                for (int i = 0; i < pro_details.Count; i++)
                {
                    if (pro_details[i].Trans_order_no == "0")
                    {
                        // sxmlpro_detils += "<Products Trans_Order_No=\"" + new_order_no + "\"  Product_code=\"" + pro_details[i].Product_code + "\" Product_name=\"" + pro_details[i].Product_name + "\" Price=\"" + pro_details[i].Price + "\" Discount=\"" + pro_details[i].Discount + "\" Free=\"" + pro_details[i].Free + "\" Quantity=\"" + pro_details[i].Cov_Quantity + "\"  Amount=\"" + pro_details[i].Amount + "\" Over_ALL_Amt=\"" + pro_details[i].Over_Tot + "\" Pro_Unit=\"" + pro_details[i].Product_Unit + "\"  Offer_Pro_Code=\"" + pro_details[i].Offer_Product_Code + "\"  Offer_Pro_Name=\"" + pro_details[i].Offer_Product_Name + "\" Offer_Pro_Unit=\"" + pro_details[i].Offer_Product_Unit + "\" Qty1=\"" + pro_details[i].Non_Cov_Quantity + "\" Tax=\"" + pro_details[i].Tax + "\" Sl_No=\"" + i + "\" Conversion_fac=\"" + pro_details[i].Con_Fav + "\" CGST=\"" + pro_details[i].CGST + "\" SGST=\"" + pro_details[i].SGST + "\" IGST=\"" + pro_details[i].IGST + "\" Unit_Code=\"" + pro_details[i].Unit_Code + "\" />";
                        sxmlpro_detils += "<Products Trans_Order_No=\"" + new_order_no + "\"  Product_code=\"" + pro_details[i].Product_code + "\" Product_name=\"" + pro_details[i].Product_name + "\" Price=\"" + pro_details[i].Price + "\" Discount=\"" + pro_details[i].Discount + "\" Free=\"" + pro_details[i].Free + "\" Quantity=\"" + pro_details[i].Cov_Quantity + "\"  Amount=\"" + pro_details[i].Amount + "\" Over_ALL_Amt=\"" + pro_details[i].Over_Tot + "\" Pro_Unit=\"" + pro_details[i].Product_Unit + "\"  Offer_Pro_Code=\"" + pro_details[i].Offer_Product_Code + "\"  Offer_Pro_Name=\"" + pro_details[i].Offer_Product_Name + "\" Offer_Pro_Unit=\"" + pro_details[i].Offer_Product_Unit + "\" Qty1=\"" + pro_details[i].Non_Cov_Quantity + "\" Tax=\"" + pro_details[i].Tax + "\" Sl_No=\"" + i + "\" Conversion_fac=\"" + pro_details[i].Con_Fav + "\"  Unit_Code=\"" + pro_details[i].Unit_Code + "\" />";
                    }
                    else
                    {
                        sxmlpro_detils += "<Products Trans_Order_No=\"" + pro_details[i].Trans_order_no + "\"  Product_code=\"" + pro_details[i].Product_code + "\" Product_name=\"" + pro_details[i].Product_name + "\" Price=\"" + pro_details[i].Price + "\" Discount=\"" + pro_details[i].Discount + "\" Free=\"" + pro_details[i].Free + "\" Quantity=\"" + pro_details[i].Cov_Quantity + "\"  Amount=\"" + pro_details[i].Amount + "\" Over_ALL_Amt=\"" + pro_details[i].Over_Tot + "\" Pro_Unit=\"" + pro_details[i].Product_Unit + "\"  Offer_Pro_Code=\"" + pro_details[i].Offer_Product_Code + "\"  Offer_Pro_Name=\"" + pro_details[i].Offer_Product_Name + "\" Offer_Pro_Unit=\"" + pro_details[i].Offer_Product_Unit + "\" Qty1=\"" + pro_details[i].Non_Cov_Quantity + "\" Tax=\"" + pro_details[i].Tax + "\" Sl_No=\"" + i + "\" Conversion_fac=\"" + pro_details[i].Con_Fav + "\"  Unit_Code=\"" + pro_details[i].Unit_Code + "\" />";
                    }
                    //int ledIns = prd.Insert_Trans_Stock_Ledger(Div_code.TrimEnd(','), general_details[0].inv_date, general_details[0].dis_code, pro_details[i].Product_code, "", pro_details[i].Cov_Quantity, "0", "0", "", "", general_details[0].dis_name, "", "INV", "");
                    int ledIns = Insert_Trans_Stock_Ledger(Div_code.TrimEnd(','), general_details[0].inv_date, general_details[0].dis_code, pro_details[i].Product_code, "", pro_details[i].Cov_Quantity, "0", "0", "", "", general_details[0].dis_name, "", "INV", "");

                    //DateTime dtgrn = DateTime.ParseExact(general_details[0].inv_date, "dd/MM/yyyy", null);
                    //string strgrn = dtgrn.ToString("MM-dd-yyyy");
                    //DataSet dsCurSkt = prd.Select_Trans_CurrStock_Distwise(Div_code.TrimEnd(','), general_details[0].dis_code, pro_details[i].Product_code);

                    //if (dsCurSkt.Tables[0].Rows.Count > 0)
                    //{
                    //    decimal entry_Stock = Convert.ToDecimal(pro_details[i].Cov_Quantity);
                    //    // decimal entry_Stock = Convert.ToDecimal(pro_details[i].Quantity);
                    //    foreach (DataRow row in dsCurSkt.Tables[0].Rows)
                    //    {
                    //        if (entry_Stock > 0)
                    //        {
                    //            decimal tot = Convert.ToDecimal(row["GStock"].ToString()) - entry_Stock;
                    //            decimal upVal = 0;
                    //            if (tot >= 0)
                    //            {
                    //                upVal = entry_Stock;
                    //                entry_Stock = 0;
                    //            }
                    //            else
                    //            {
                    //                upVal = Convert.ToDecimal(row["GStock"].ToString());
                    //                entry_Stock = Math.Abs(tot);
                    //            }

                    //           // int num = prd.Update_Trans_CurrStock_Batchwise_Transfer1(Div_code.TrimEnd(','), general_details[0].dis_code, pro_details[i].Product_code, row["BatchNo"].ToString(), upVal.ToString("0.00"), "Good", pro_details[i].Quantity.ToString(), "-");
                    //            int num = prd.Update_Trans_CurrStock_Batchwise_Transfer1(Div_code.TrimEnd(','), general_details[0].dis_code, pro_details[i].Product_code, row["BatchNo"].ToString(), upVal.ToString("0.00"), "Good", pro_details[i].Cov_Quantity.ToString(), "-");

                    //            string Trn_No = Convert.ToString(invid_code);
                    //            int ledIns = prd.Insert_Trans_Stock_Ledger(Div_code.TrimEnd(','), general_details[0].inv_date, general_details[0].dis_code, pro_details[i].Product_code, row["BatchNo"].ToString(), upVal.ToString("0.00"), "0", "0", "", Trn_No, general_details[0].dis_name, "", "INV","");
                    //        }
                    //    }
                    //}
                }
                sxmlpro_detils += "</ROOT>";

                string sxmltax_details = "<ROOT>";
                for (int i = 0; i < tax_details.Count; i++)
                {
                    sxmltax_details += "<Tax Tax_Code=\"" + tax_details[i].Tax_Code + "\" Tax_Name=\"" + tax_details[i].Tax_Name + "\" Tax_Amt=\"" + tax_details[i].Tax_Amt + "\" pro_code=\"" + tax_details[i].pro_code + "\" Tax_Per=\"" + tax_details[i].Tax_Per + "\" umo_code=\"" + tax_details[i].umo_code + "\" />";
                }
                sxmltax_details += "</ROOT>";

                Invoice_no = Save_invoice_Data(sxmlGen_detils, sxmlpro_detils, sxmltax_details);
                scope.Complete();
                scope.Dispose();
            }
            catch (Exception ex)
            {
                scope.Dispose();
                throw ex;
            }
            return Invoice_no;
        }
    }
    public static int Insert_Trans_Stock_Ledger(string Div_Code, string Ledg_Date, string Dist_Code, string Prod_Code, string BatchNo, string GStock, string DStock,
           string CalType, string Reason, string Ref, string fromRef, string toRef, string EntryBy, string Price)
    {
        int iReturn = -1;
        int iSlNo = -1;
        try
        {
            DB_EReporting db = new DB_EReporting();

            string strQry = "SELECT ISNULL(MAX(Ledger_ID),0)+1 FROM Trans_Stock_Ledger";
            iSlNo = db.Exec_Scalar(strQry);

            strQry = " INSERT INTO Trans_Stock_Ledger (Ledger_ID,Ledg_Date,Dist_Code,Prod_Code,BatchNo,GStock,DStock,CalType,Reason,Ref,Division_Code,FrmRef,ToRef,EntryBy,Price) " +
                     " VALUES ('" + iSlNo + "','" + Ledg_Date + "','" + Dist_Code + "','" + Prod_Code + "','" + BatchNo + "','" + GStock + "','" + DStock + "','" + CalType + "','" + Reason + "','" + Ref + "','" + Div_Code + "','" + fromRef + "','" + toRef + "','" + EntryBy + "','" + Price + "')";
            iReturn = db.ExecQry(strQry);
            iReturn = iSlNo;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return iReturn;
    }

    public static string Save_invoice_Data(string general_details, string Product_Details,string Tax_details)
    {
        DB_EReporting db_ER = new DB_EReporting();
        string Invoice_no = string.Empty;
        string strQry = "EXEC Sp_Invoice_Order '" + general_details + "','" + Product_Details + "','" + Tax_details + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
            Invoice_no = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            if (Invoice_no == "saved")
                Invoice_no = "invoice Generated Succesfully";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Invoice_no;
    }

    public class General_details
    {
        public string order_no { get; set; }
        public string sf_code { get; set; }
        public string sf_name { get; set; }
        public string dis_code { get; set; }
        public string dis_name { get; set; }
        public string cus_code { get; set; }
        public string cus_name { get; set; }
        public string order_date { get; set; }
        public string inv_date { get; set; }
        public string del_date { get; set; }
        public string pay_term { get; set; }
        public string pay_due { get; set; }
        public string ship_mtd { get; set; }
        public string ship_term { get; set; }
        public float sub_total { get; set; }
        public float tax_total { get; set; }
        public float total { get; set; }
        public float adv_paid { get; set; }
        public float Amt_due { get; set; }
        public string remark { get; set; }
        public float Dis_total { get; set; }
        public string oqty { get; set; }
        public string Div_Code { get; set; }
        public string Advance_amount { get; set; }
        public string adjust_amt { get; set; }
        public string Reference_no { get; set; }
        public float IGST { get; set; }
        public float SGST { get; set; }
        public float CGST { get; set; }
        public float TCS { get; set; }
        public float TDS { get; set; }

    }

    public class Pro_details
    {
        public string Trans_order_no { get; set; }
        public string order_no { get; set; }
        public string Product_code { get; set; }
        public string Product_name { get; set; }
        public string Price { get; set; }
        public string Discount { get; set; }
        public string Free { get; set; }
        public string Non_Cov_Quantity { get; set; }
        public string sale_q { get; set; }
        public string Amount { get; set; }
        public string Over_Tot { get; set; }
        public string Product_Unit { get; set; }
        public string Offer_Product_Code { get; set; }
        public string Offer_Product_Name { get; set; }
        public string Offer_Product_Unit { get; set; }
        public string Cov_Quantity { get; set; }
        public string Tax { get; set; }
        public string Con_Fav { get; set; }
        public float IGST { get; set; }
        public float SGST { get; set; }
        public float CGST { get; set; }
        public float Unit_Code { get; set; }

    }

    public class Tax_details
    {
        public string Tax_Code { get; set; }
        public string Tax_Name { get; set; }
        public float Tax_Amt { get; set; }
        public float Tax_Per { get; set; }
        public string pro_code { get; set; }
        public string umo_code { get; set; }
    }

    public class Trans_Out_standing_Details
    {
        public string Cus_Code { get; set; }
        public string Cus_Name { get; set; }
        public decimal value { get; set; }
    }


    [WebMethod(EnableSession = true)]
    public static string Get_AllValues(int Cus_code)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code"].ToString();
        Product prd = new Product();

        string osAmt = string.Empty;
        string curr_out = string.Empty;
        DataSet dsGoods = null;
        dsGoods = prd.Get_Stock_Out_standing(Cus_code, Div_Code.TrimEnd(','), sf_code);
        if (dsGoods.Tables[0].Rows.Count > 0)
        {
            osAmt = Convert.ToDecimal(dsGoods.Tables[0].Rows[0]["Out_standing_Amt"]).ToString() + "," + Convert.ToDecimal(dsGoods.Tables[0].Rows[0]["Credit_Limit"]).ToString() + "," + Convert.ToDecimal(dsGoods.Tables[0].Rows[0]["Credit_Amt"]).ToString();
            curr_out = Convert.ToDecimal(dsGoods.Tables[0].Rows[0]["Credit_Limit"]).ToString();
        }
        else
        {
            //osAmt = "0,0";
            //curr_out = "0";
        }
        return osAmt;

    }
    public class CurrStock
    {
        public string Dist_Code { get; set; }
        public string Prod_Code { get; set; }
        public string GStock { get; set; }
        public string DStock { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static CurrStock[] GetCurrStock()
    {
        string PDiv_code = HttpContext.Current.Session["div_code"].ToString();
        List<CurrStock> product = new List<CurrStock>();
        DataSet dsProduct = null;
        Product p = new Product();

        dsProduct = p.getCurrentStock(PDiv_code.TrimEnd(','));
        if (dsProduct != null)
        {
            foreach (DataRow row in dsProduct.Tables[0].Rows)
            {
                CurrStock pro = new CurrStock();
                pro.Dist_Code = row["Dist_Code"].ToString();
                pro.Prod_Code = row["Prod_Code"].ToString();
                pro.GStock = row["GStock"].ToString();
                pro.DStock = row["DStock"].ToString();
                product.Add(pro);
            }
        }
        return product.ToArray();
    }

    public class ProductDetails
    {
        public string label { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static List<ListItem> GetProducts()
    {
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string StockistCode = HttpContext.Current.Session["Sf_Code"].ToString();
        List<ListItem> pro = new List<ListItem>();
        DataSet ds = new DataSet();
        Holiday hd = new Holiday();
        ds = hd.GetProdNew(Div_code.TrimEnd(','));
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            pro.Add(new ListItem
            {
                Value = row["Product_Detail_Code"].ToString(),
                Text = row["Product_Detail_Name"].ToString(),

            });
        }
        return pro;
    }


    [WebMethod(EnableSession = true)]
    public static string  bindretailer()
    {
        string Sf_Type = HttpContext.Current.Session["sf_type"].ToString();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string StockistCode = HttpContext.Current.Session["Sf_Code"].ToString();
        StockistMaster sm = new StockistMaster();
        DataSet ds = new DataSet();
       // ds = sm.getretailerdetails(StockistCode, Div_code.TrimEnd(','), Sf_Type);
        ds = getretailerdetailsmyorder(StockistCode, Div_code.TrimEnd(','));
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }
public static DataSet getretailerdetailsmyorder(string scode, string divcode)
		{
			DB_EReporting db_ER = new DB_EReporting();
			DataSet ds = null;

			try
			{
				string strQry = "EXEC spGetRetailerName_Invoice '" + scode + "','" + divcode + "'";
				ds = db_ER.Exec_DataSet(strQry);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return ds;
		}

    [WebMethod(EnableSession = true)]
    public static string GetPendingOrder(string Retailer_ID)
    {
        ds = sm.Getpendingorder(Retailer_ID, Stockist_Code, Div_Code, Sf_Type);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string GetPendingOrder_details(string Retailer_ID)
    {
        ds = sm.Getpendingorder_details(Retailer_ID, Stockist_Code, Div_Code, Sf_Type);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }   

    public class Pending_Product
    {

        public string Trans_Order_No { get; set; }
        public string Product_Code { get; set; }
        public string Product_Name { get; set; }
        public string Rate { get; set; }
        public string Discount { get; set; }
        public string Free { get; set; }
        public string Quantity { get; set; }
        public string Amount { get; set; }
        public string date { get; set; }
        public string Tax_Id { get; set; }
        public string Tax_Name { get; set; }
        public string Tax_Val { get; set; }
        public string unit { get; set; }
    }

    public class allProduct
    {
        public string product_Detail_Code { get; set; }
        public string Product_Detail_Name { get; set; }
        public string MRP_Price { get; set; }
        public string Distributor_Price { get; set; }
        public string Retailor_Price { get; set; }
        public string Product_Sale_Unit { get; set; }
        public string Tax_Id { get; set; }
        public string Tax_Name { get; set; }
        public string Tax_Val { get; set; }
        public string Sale_Erp_Code { get; set; }
        public string Sample_Erp_Code { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static string GetProductDetails()
    {
        ds = sm.getprodDet(Div_Code.TrimEnd(','), Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Get_All_Stock()
    {
        ds = sm.getStockBydid(Div_Code.TrimEnd(','), Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    //[WebMethod(EnableSession = true)]
    //public static string Get_Batch_Stock()
    //{
    //    ds = sm.Get_Batchwise_stock(Div_Code.TrimEnd(','), Stockist_Code);
    //    return JsonConvert.SerializeObject(ds.Tables[0]);
    //}

    [WebMethod(EnableSession = true)]
    public static string getratenew(string Div_Code, string Stockist_Code)
    {
        string State = HttpContext.Current.Session["State"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        ds = sm.get_rate_new_bystk(Div_Code.TrimEnd(','), Stockist_Code, State);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }


    [WebMethod(EnableSession = true)]
    public static string getscheme(string date, string Div_Code, string Stockist_Code)
    {
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        ds = sm.Get_Sec_scheme(Stockist_Code, Div_Code.TrimEnd(','), date);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Get_Adv_Credit_amt_details(string Retailer_ID)
    {
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        ds = sm.Get_Adv_Credit_amt(Stockist_Code, Div_Code.TrimEnd(','), Retailer_ID);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Get_Product_unit()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        ds = gets_Product_unit_details(div_code,Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet gets_Product_unit_details(string Div_Code, string stk_code)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec get_Hap_pro_unit '" + Div_Code + "','" + stk_code + "'";

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
    public static string Get_Product_Tax()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string state_code = HttpContext.Current.Session["State"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
       // ds = sm.gets_Product_Tax_details(div_code, Stockist_Code, state_code);
        ds = gets_Product_Tax_details(div_code, Stockist_Code, state_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet gets_Product_Tax_details(string div, string stk, string state)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec get_tax_Details '" + div + "','" + stk + "','" + state + "'";

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
    public static string GetCustWise_price()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        string state_code = HttpContext.Current.Session["State"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        string strQry = string.Empty;
        DB_EReporting db_ER = new DB_EReporting();
        strQry = "EXEC sp_get_CustWise_Price_Details '" + div_code + "','" + Stockist_Code + "','" + state_code + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    
    [WebMethod(EnableSession = true)]
    public static string Get_Product_Cat_Details()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        //ds = sm.Get_Product_Cat_Details(div_code, Stockist_Code);
        ds = Get_Product_Cat_Details(div_code, Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Get_Product_Cat_Details(string Div_Code, string stk)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;
        string strQry = "EXEC sp_get_cat_Details '" + Div_Code + "','" + stk + "'";
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
    public static string Get_Route()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        Territory Rut = new Territory();
        ds = Rut.getTerritory(Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    [WebMethod(EnableSession = true)]
    public static string GetWise_price(string retailercode)
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        string state_code = HttpContext.Current.Session["State"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        string strQry = string.Empty;
        DB_EReporting db_ER = new DB_EReporting();
        strQry = "EXEC sp_get_Price_Details '" + div_code + "','" + Stockist_Code + "','" + state_code + "','" + retailercode + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
}
