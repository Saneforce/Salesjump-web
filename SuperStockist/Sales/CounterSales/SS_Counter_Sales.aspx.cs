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
using DBase_EReport;

public partial class SuperStockist_Sales_CounterSales_SS_Counter_Sales : System.Web.UI.Page
{
    public static string Order_ID = string.Empty;
    public static DataSet ds = null;
    public static StockistMaster sm = new StockistMaster();
    public static string Stockist_Code = string.Empty;
    public static string Div_Code = string.Empty;
    public static string State = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        string value = Session["div_code"].ToString();
        Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        Div_Code = HttpContext.Current.Session["div_code"].ToString();
        State = HttpContext.Current.Session["State"].ToString();
    }

    public class CustomerDetails
    {
        public string Cust_code { get; set; }
        public string Cust_name { get; set; }
        public string Cust_Address { get; set; }

    }
    [WebMethod(EnableSession = true)]
    public static CustomerDetails[] GetCustomerDetails(string Mobile_No)
    {
        List<CustomerDetails> Cust_Details = new List<CustomerDetails>();
        // ds = sm.GetCustDetailsByMobNo(Mobile_No);
        ds = GetCustDetailsByMobNo(Mobile_No);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            CustomerDetails details = new CustomerDetails();
            details.Cust_code = row["ListedDrCode"].ToString();
            details.Cust_name = row["ListedDr_Name"].ToString();
            details.Cust_Address = row["Address"].ToString();
            Cust_Details.Add(details);
        }
        return Cust_Details.ToArray();
    }
    public static DataSet GetCustDetailsByMobNo(string Mobile_no)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;
        // strQry = " select ListedDrCode,ListedDr_Name,Isnull(ListedDr_Address1,'')+','+Isnull(ListedDr_Address2,'')+','+Isnull(ListedDr_Address3,'')+','+Isnull(ListedDr_PinCode,'') +','+Isnull(ListedDr_Mobile,'') as Address from  Mas_ListedDr where ListedDr_Mobile = '" + Mobile_no + "' ";
        string strQry = "EXEC get_SS_cust_by_mblno '" + Mobile_no + "'";
        //string strQry = "EXEC get_cust_by_mblno '" + Mobile_no + "'";

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
        //Tax_mas = dc.view_tax_Mas(Div_code.TrimEnd(','));
        Tax_mas = view_tax_Mas(Div_code.TrimEnd(','));

        foreach (DataRow row in Tax_mas.Tables[0].Rows)
        {
            Tax_Details ffd = new Tax_Details();
            ffd.Exp_code = row["Tax_Val"].ToString();
            ffd.Exp_name = row["Tax_Name"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }
    public static DataSet view_tax_Mas(string div_code)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        // strQry = "select (cast(Tax_Name as varchar)+'('+cast (Value as varchar)+Tax_Type+')')Tax_Name,Value as Tax_Val from Tax_Master where Division_Code='" + div_code + "'";
        string strQry = "select '0' as Tax_Id,'--Select--' as Tax_Name, '0' as Tax_Val union all select Tax_Id, (cast(Tax_Name as varchar)+'('+cast (Value as varchar)+Tax_Type+')')Tax_Name,Value as Tax_Val from Tax_Master where Division_Code='" + div_code + "'";
        try
        {
            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }
    public class Holodayss
    {
        public string label { get; set; }
        public string value { get; set; }
        public string id { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static Holodayss[] GetPorduct()
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        List<Holodayss> HDay = new List<Holodayss>();
        DataSet dsAlowtype = null;
        Holiday hd = new Holiday();

        //dsAlowtype = hd.GetProdNew(div_code.TrimEnd(','));
        dsAlowtype = GetProdNew(div_code.TrimEnd(','));

        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            Holodayss d = new Holodayss();
            d.label = row["Product_Detail_Name"].ToString();
            d.value = row["Product_Detail_Name"].ToString();
            d.id = row["Product_Detail_Code"].ToString();
            HDay.Add(d);
        }
        return HDay.ToArray();
    }
    public static DataSet GetProdNew(string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsHoliday = null;
        string strQry = "select Product_Detail_Code,Product_Detail_Name from Mas_Product_Detail where Division_Code='" + divcode + "'  and Product_Active_Flag='0'";
        try
        {
            dsHoliday = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsHoliday;
    }
    //[WebMethod(EnableSession = true)]
    //public static List<ListItem> GetProducts()
    //{
    //    string Div_code = HttpContext.Current.Session["div_code"].ToString();
    //    string StockistCode = HttpContext.Current.Session["Sf_Code"].ToString();
    //    List<ListItem> pro = new List<ListItem>();
    //    DataSet ds = new DataSet();
    //    Holiday hd = new Holiday();
    //    ds = hd.GetProdNew(Div_code.TrimEnd(','));
    //    foreach (DataRow row in ds.Tables[0].Rows)
    //    {
    //        pro.Add(new ListItem
    //        {
    //            Value = row["Product_Detail_Code"].ToString(),
    //            Text = row["Product_Detail_Name"].ToString(),

    //        });
    //    }
    //    return pro;
    //}

    [WebMethod(EnableSession = true)]
    public static string GetProductDetails()
    {
        //ds = sm.getprodDet(Div_Code.TrimEnd(','), Stockist_Code);
        ds = getprodDet(Div_Code.TrimEnd(','), Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getprodDet(string Div_Code, string Stockist_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        //string strQry = "exec sp_GetProductTaxDetails '" + Div_Code + "','" + Stockist_Code + "'";
        string strQry = "exec sp_GetSS_ProductTaxDetails_dms '" + Div_Code + "','" + Stockist_Code + "'";
        //string strQry = "exec sp_GetProductTaxDetails_dms '" + Div_Code + "','" + Stockist_Code + "'";

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
    public static List<string> GetProductName(string prefix)
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        List<string> pl = new List<string>();
        DataSet dspono = null;
        StockistMaster sm = new StockistMaster();
        //dspono = sm.getbyletter(prefix, Div_Code.TrimEnd(','));
        dspono = getbyletter(prefix, Div_Code.TrimEnd(','));
        if (dspono.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in dspono.Tables[0].Rows)
            {

                pl.Add(row["Product_Detail_Name"].ToString());
            }
        }
        return pl;
    }
    public static DataSet getbyletter(string letter, string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        string strQry = "select Product_Detail_Code,Product_Detail_Name from Mas_Product_Detail where Division_Code = '" + divcode + "'  and Product_Active_Flag = '0' and Product_Detail_Name like '%" + letter + "%' ";

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
    public static string SaveDate(string Counter_head, string Counter_details, string Counter_tax, string no)
    {
        DB_EReporting db = new DB_EReporting();

        string itemid;
        var msg = string.Empty;
        SqlTransaction tran;
        Product prd = new Product();
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        tran = con.BeginTransaction();
        try
        {
            string sqlQry = string.Empty;
            string Div_code = HttpContext.Current.Session["div_code"].ToString();
            string Sf_Type = HttpContext.Current.Session["sf_type"].ToString();
            string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();

            IList<Trans_Invoice_Head> general_details = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Trans_Invoice_Head>>(Counter_head);
            IList<Trans_Invoice_Details> pro_details = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Trans_Invoice_Details>>(Counter_details);
            IList<Trans_Invoice_Tax_Details> tax_details = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Trans_Invoice_Tax_Details>>(Counter_tax);

            SqlCommand cmd = new SqlCommand();
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            string cus_code = general_details[0].cus_code;
            sqlQry = "exec Sp_CounterSalesListedDr_Insert  '" + general_details[0].dis_code + "','" + general_details[0].cus_name + "','" + general_details[0].Cust_Address + "','" + general_details[0].Customer_No + "','" + Div_code.TrimEnd(',') + "','" + general_details[0].remark + "'";
            ds = db_ER.Exec_DataSet(sqlQry);
            if (ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "" && ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != null)
            {
                cus_code = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                // Hid_Cust.Value= ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }

            sqlQry = "exec  sp_ssCounter_OrderNo '" + Sf_Code + "'";
            //sqlQry = "exec sp_Counter_OrderNo  '" + Sf_Code + "'";
            ds = db_ER.Exec_DataSet(sqlQry);
            if (ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "" && ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != null)
            {
                Order_ID = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                msg = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }

            sqlQry = "SELECT isnull(max(Trans_Count_Slno)+1,'1')Trans_Count_Slno from Trans_CounterSales_Head";
            string invid_code = db.Exec_Scalar_s(sqlQry);
            //cmd = new SqlCommand(sqlQry);
            //cmd.Connection = con;
            //cmd.Transaction = tran;
            //int invid_code = Convert.ToInt32(cmd.ExecuteScalar());

            sqlQry = "insert into Trans_CounterSales_Head(Trans_Count_Slno,Dis_Code,Dis_Name,Cus_Code,Cus_Name,Cust_Mobie_No,Cust_Address,Order_Date,Pay_Term,Sub_Total,Tax_Total,Dis_total,Total,Remarks,Order_No,Division_Code,SGST,CGST,Ref_No)" +
                "values('" + invid_code + "','" + general_details[0].dis_code + "','" + general_details[0].dis_name + "','" + cus_code + "','" + general_details[0].cus_name + "','" + general_details[0].Customer_No + "','" + general_details[0].Cust_Address + "', " +
               "'" + general_details[0].order_date + "','" + general_details[0].pay_term + "','" + general_details[0].sub_total + "','" + general_details[0].tax_total + "','" + general_details[0].Dis_total + "','" + general_details[0].total + "','" + general_details[0].remark + "'" +
               ",'" + Order_ID + "','" + general_details[0].Div_Code.TrimEnd(',') + "','" + general_details[0].sgst_total + "','" + general_details[0].cgst_total + "','" + general_details[0].Ref_No + "')";
            //cmd = new SqlCommand(sqlQry);
            //cmd.Connection = con;
            //cmd.Transaction = tran;

            //cmd.Parameters.AddWithValue("@Trans_Count_Slno", invid_code);
            //cmd.Parameters.AddWithValue("@Dis_Code", general_details[0].dis_code);
            //cmd.Parameters.AddWithValue("@Dis_Name", general_details[0].dis_name);
            //cmd.Parameters.AddWithValue("@Cus_Code", cus_code);
            //cmd.Parameters.AddWithValue("@Cus_Name", general_details[0].cus_name);
            //cmd.Parameters.AddWithValue("@Cust_Mobie_No", general_details[0].Customer_No);
            //cmd.Parameters.AddWithValue("@Cust_Address", general_details[0].Cust_Address);
            //cmd.Parameters.AddWithValue("@Order_Date", general_details[0].order_date);
            //cmd.Parameters.AddWithValue("@Pay_Term", general_details[0].pay_term);
            //cmd.Parameters.AddWithValue("@Sub_Total", general_details[0].sub_total);
            //cmd.Parameters.AddWithValue("@Tax_Total", general_details[0].tax_total);
            //cmd.Parameters.AddWithValue("@Dis_total", general_details[0].Dis_total);
            //cmd.Parameters.AddWithValue("@Total", general_details[0].total);
            //cmd.Parameters.AddWithValue("@Remarks", general_details[0].remark);
            //cmd.Parameters.AddWithValue("@Order_No", Order_ID);
            //cmd.Parameters.AddWithValue("@Div_Code", general_details[0].Div_Code.TrimEnd(','));
            //cmd.Parameters.AddWithValue("@SGST", general_details[0].sgst_total);
            //cmd.Parameters.AddWithValue("@CGST", general_details[0].cgst_total);
            //cmd.Parameters.AddWithValue("@Ref_No", general_details[0].Ref_No);

            //cmd.ExecuteNonQuery();
            int head = db.ExecQry(sqlQry);
            itemid = invid_code;

            for (int i = 0; i < pro_details.Count; i++)
            {
                sqlQry = "insert into Trans_CounterSales_Details(Trans_Count_Slno,Trans_Order_No,Product_Code,Product_Name,Price,Discount,Free,Quantity,Amount,Unit,offer_pro_code,offer_pro_name,offer_pro_unit,Tax,Con_Fac,con_Qty,Dis_val)" +
                    "values('" + itemid + "','" + Order_ID + "','" + pro_details[i].Product_code + "','" + pro_details[i].Product_name + "','" + pro_details[i].Price + "','" + pro_details[i].Discount + "','" + pro_details[i].Free + "','" + pro_details[i].Quantity + "'" +
                    ",'" + pro_details[i].Amount + "','" + pro_details[i].Unit + "','" + pro_details[i].Off_Pro_Code + "','" + pro_details[i].Off_Pro_Name + "','" + pro_details[i].Off_Pro_Unit + "','" + pro_details[i].Tax + "','" + pro_details[i].Con_Fac + "'" +
                    ",'" + pro_details[i].con_qty + "','" + pro_details[i].Dis_val + "')";
                //cmd = new SqlCommand(sqlQry);
                //cmd.Connection = con;
                //cmd.Transaction = tran;
                //cmd.Parameters.AddWithValue("@Trans_Count_Slno", itemid);
                //cmd.Parameters.AddWithValue("@Trans_Order_No", Order_ID);
                //cmd.Parameters.AddWithValue("@Product_Code", pro_details[i].Product_code);
                //cmd.Parameters.AddWithValue("@Product_Name", pro_details[i].Product_name);
                //cmd.Parameters.AddWithValue("@Price", pro_details[i].Price);
                //cmd.Parameters.AddWithValue("@Discount", pro_details[i].Discount);
                //cmd.Parameters.AddWithValue("@Free", pro_details[i].Free);
                //cmd.Parameters.AddWithValue("@Quantity", pro_details[i].Quantity);
                //cmd.Parameters.AddWithValue("@Amount", pro_details[i].Amount);
                //cmd.Parameters.AddWithValue("@Unit", pro_details[i].Unit);
                //cmd.Parameters.AddWithValue("@offer_pro_code", pro_details[i].Off_Pro_Code);
                //cmd.Parameters.AddWithValue("@offer_pro_name", pro_details[i].Off_Pro_Name);
                //cmd.Parameters.AddWithValue("@offer_pro_unit", pro_details[i].Off_Pro_Unit);
                //cmd.Parameters.AddWithValue("@Tax", pro_details[i].Tax); 
                //cmd.Parameters.AddWithValue("@Con_Fac", pro_details[i].Con_Fac);
                //cmd.Parameters.AddWithValue("@con_Qty", pro_details[i].con_qty);
                //cmd.Parameters.AddWithValue("@Dis_val", pro_details[i].Dis_val);
                //cmd.ExecuteNonQuery();
                int details = db.ExecQry(sqlQry);

                //int ledIns = prd.Insert_Trans_Stock_Ledger(Div_code.TrimEnd(','), general_details[0].order_date, general_details[0].dis_code, pro_details[i].Product_code,"", pro_details[i].con_qty, "0", "0", "", "", general_details[0].dis_name, "", "Counter", "");
                int ledIns = Insert_Trans_Stock_Ledger(Div_code.TrimEnd(','), general_details[0].order_date, general_details[0].dis_code, pro_details[i].Product_code, "", pro_details[i].con_qty, "0", "0", "", "", general_details[0].dis_name, "", "Counter", "");

                //DataSet dsCurSkt = prd.Select_Trans_CurrStock_Distwise(Div_code.TrimEnd(','), general_details[0].dis_code, pro_details[i].Product_code);

                //if (dsCurSkt.Tables[0].Rows.Count > 0)
                //{
                //    decimal entry_Stock = Convert.ToDecimal(pro_details[i].con_qty);
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
                //            int num = prd.Update_Trans_CurrStock_Batchwise_Transfer1(Div_code.TrimEnd(','), general_details[0].dis_code, pro_details[i].Product_code, row["BatchNo"].ToString(), upVal.ToString("0.00"), "Good", pro_details[i].con_qty.ToString(), "-");

                //            string Trn_No = Convert.ToString(invid_code);
                //            int ledIns = prd.Insert_Trans_Stock_Ledger(Div_code.TrimEnd(','), general_details[0].order_date, general_details[0].dis_code, pro_details[i].Product_code, row["BatchNo"].ToString(), upVal.ToString("0.00"), "0", "0", "", Trn_No, general_details[0].dis_name, "", "Counter","");
                //        }
                //    }
                //}

                int tax = 0;
                for (int k = 0; k < tax_details.Count; k++)
                {
                    sqlQry = "insert into Trans_CounterSales_Tax_Details(Trans_Count_Slno,.Trans_Order_No,Tax_Code,Tax_Name,Tax_Amt)" +
                        "values('" + itemid + "','" + tax_details[k].Product_code + "','" + tax_details[k].taxCode + "','" + tax_details[k].tax_Name + "','" + tax_details[k].value + "')";
                    //cmd = new SqlCommand(sqlQry);
                    //cmd.Connection = con;
                    //cmd.Transaction = tran;
                    //cmd.Parameters.AddWithValue("@Trans_Count_Slno", itemid);
                    //cmd.Parameters.AddWithValue("@Trans_Order_No", tax_details[k].Product_code);
                    //cmd.Parameters.AddWithValue("@Tax_Code", tax_details[k].taxCode);
                    //cmd.Parameters.AddWithValue("@Tax_Name", tax_details[k].tax_Name);
                    //cmd.Parameters.AddWithValue("@Tax_Amt", tax_details[k].value);
                    //cmd.ExecuteNonQuery();
                    tax = db.ExecQry(sqlQry);
                }
            }
            msg = "Record Update Success";
            tran.Commit();
            con.Close();
        }
        catch (Exception exp)
        {
            if (tran != null)
            {
                tran.Rollback();
                msg = exp.Message.ToString() + "\nTransaction Rolledback, Tim didn't make it.";
                throw exp;
            }
        }
        return Order_ID;
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
    public class MainTransInv
    {
        public List<Trans_Invoice_Head> MTED = new List<Trans_Invoice_Head>();
        public List<Trans_Invoice_Details> TED = new List<Trans_Invoice_Details>();
    }

    public class Trans_Invoice_Head
    {
        public string order_no { get; set; }
        public string Customer_No { get; set; }
        public string Cust_Address { get; set; }
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
        public string sgst_total { get; set; }
        public string cgst_total { get; set; }
        public string Ref_No { get; set; }
    }


    public class Trans_Invoice_Details
    {
        public string Trans_order_no { get; set; }
        public string Product_code { get; set; }
        public string Product_name { get; set; }
        public string Price { get; set; }
        public string Discount { get; set; }
        public string Free { get; set; }
        public string Quantity { get; set; }
        public string Amount { get; set; }
        public string Unit { get; set; }
        public string Off_Pro_Code { get; set; }
        public string Off_Pro_Name { get; set; }
        public string Off_Pro_Unit { get; set; }
        public string Tax { get; set; }
        public string con_qty { get; set; }
        public string Con_Fac { get; set; }
        public string Dis_val { get; set; }
    }


    public class Trans_Invoice_Tax_Details
    {
        public string Product_code { get; set; }
        public string taxCode { get; set; }
        public string tax_Name { get; set; }
        public float value { get; set; }
    }

    public class Trans_Out_standing_Details
    {
        public string Cus_Code { get; set; }
        public string Cus_Name { get; set; }
        public decimal value { get; set; }
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
    public static string getratenew(string Div_Code, string Stockist_Code)
    {
        string State = HttpContext.Current.Session["State"].ToString();
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        // ds = sm.get_rate_new_bystk(Div_Code.TrimEnd(','), Stockist_Code, State);

        ds = get_rate_new_bystk(Div_Code.TrimEnd(','), Stockist_Code, State);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet get_rate_new_bystk(string Div_Code, string Stockist_Code, string State)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec getSS_rate_new '" + Div_Code + "','" + Stockist_Code + "','" + State + "'";
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
    public static string getscheme(string date, string Div_Code, string Stockist_Code)
    {
        ds = Get_Sec_scheme(Stockist_Code, Div_Code.TrimEnd(','), date);
        //ds = sm.Get_Sec_scheme(Stockist_Code, Div_Code.TrimEnd(','), date);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet Get_Sec_scheme(string Stockist_Code, string Div_Code, string date)
    {

        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;

        try
        {
            string strQry = "EXEC get_secondary_scheme '" + Stockist_Code + "','" + Div_Code + "','" + date + "'";
            //strQry = "select ms.*,mp.Sale_Erp_Code,Sample_Erp_Code from mas_scheme ms left join mas_product_detail mp on ms.Product_Code=mp.Product_Detail_Code where ms.Division_Code='" + Div_Code + "' and  CHARINDEX(','+cast('" + Stockist_Code + "' as varchar)+',',','+Stockist_Code+',')>0 and cast(convert(varchar, Effective_To, 101) as datetime) >= cast(convert(varchar,'" + date + "' ,101) as datetime)   and cast(convert(varchar, Effective_From, 101) as datetime) <= cast(convert(varchar, '" + date + "', 101) as datetime) order by Product_Code,cast(scheme as int) desc ";
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return ds;

    }


    [WebMethod(EnableSession = true)]
    public static string Get_Product_unit()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        //ds = sm.gets_Product_unit_details(div_code, Stockist_Code);
        ds = gets_Product_unit_details(div_code, Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet gets_Product_unit_details(string Div_Code, string Stockist_Code)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec get_SS_pro_unit '" + Div_Code + "','" + Stockist_Code + "'";
        //string strQry = "Exec get_Hap_pro_unit '" + Div_Code + "','" + Stockist_Code + "'";

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
    public static string Get_All_Stock()
    {
        ds = getStockBydid(Div_Code.TrimEnd(','), Stockist_Code);
        // ds = sm.getStockBydid(Div_Code.TrimEnd(','), Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getStockBydid(string Div_Code, string Stockist_Code)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec Sp_getStockBydid '" + Div_Code + "','" + Stockist_Code + "'";

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

        string strQry = "Exec getss_tax_Details '" + div + "','" + stk + "','" + state + "'";
        //string strQry = "Exec get_tax_Details '" + div + "','" + stk + "','" + state + "'";

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
    public static string GetcountersaleDetail(string Order_Id)
    {
        DataSet dsListedDR = null;
        dsListedDR = Getcountersale(Div_Code, Stockist_Code, Order_Id);
        return JsonConvert.SerializeObject(dsListedDR.Tables[0]);
    }
    public static DataSet Getcountersale(string div_code, string stockist_code, string order_id)
    {
        string strQry = "";
        DataSet dsAdmin = new DataSet();
        DB_EReporting dbER = new DB_EReporting();

        strQry = " exec GetCounterSale '" + div_code + "','" + stockist_code + "','" + order_id + "'";
        try
        {
            dsAdmin = dbER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }
}