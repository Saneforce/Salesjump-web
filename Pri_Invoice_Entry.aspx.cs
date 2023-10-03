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

public partial class Pri_Invoice_Entry : System.Web.UI.Page
{
    string dis_code = string.Empty;
    string cus_code = string.Empty;
    string or_date = string.Empty;
    DataSet dsSf = null;
    DataSet dsSf_or = null;
    DataSet tax = null;
    string div_code = string.Empty;
    DataSet dsTP = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Convert.ToString(Session["div_code"]);
        if (!this.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Or_Date"]) && !string.IsNullOrEmpty(Request.QueryString["Ord_code"]))
            {

                or_date = Request.QueryString["Or_Date"];
                dis_code = Request.QueryString["Dis_code"];
                cus_code = Request.QueryString["Ord_code"];
                InvoiceNumber.Value = Request.QueryString["Or_Date"];
                Txt_in_date.Value = DateTime.Now.ToString("dd/MM/yyyy");
                DateTime date = Convert.ToDateTime(Request.QueryString["Or_Date"]);
                InvoiceNumber.Value = date.ToString("dd/MM/yyyy");
                DCR dc = new DCR();
                dsSf = dc.view_total_Pri_order_view_in(dis_code, or_date, cus_code);
                {
                    Hid_orderno.Value = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Hid_Sf_Name.Value = dsSf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    Txt_fldForce.Value = dsSf.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    Hid_Dis_Name.Value = dsSf.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    Txt_dist.Value = dsSf.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    Txt_Retailer.Value = dsSf.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                    Hid_Cus_Name.Value = dsSf.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                }


                dsSf_or = dc.view_total_Pri_order_view_in_order(dis_code, or_date, cus_code);
                if (dsSf_or.Tables[0].Rows.Count > 0)
                {

                    rptOrders.DataSource = dsSf_or;
                    rptOrders.DataBind();



                }

            }

        }

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
        Tax_mas = dc.view_tax_Mas(Div_code);

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
        Trans_mas = dc.view_Trans_Mas(Div_code);

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
        Trans_mas = dc.view_Pay_Mas(Div_code);

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
    public static string SaveDate(string data, string savemode)
    {
        int itemid;
        var msg = string.Empty;
        SqlTransaction tran;
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        tran = con.BeginTransaction();
        try
        {

            string sqlQry = string.Empty;
            string Div_code = HttpContext.Current.Session["div_code"].ToString();
            string Sf_Type = HttpContext.Current.Session["sf_type"].ToString();
            MainTransInv JMTE = new MainTransInv();

            JSonHelper helper = new JSonHelper();
            JMTE = helper.ConverJSonToObject<MainTransInv>(data);

            SqlCommand cmd = new SqlCommand();
            sqlQry = "SELECT isnull(max(Trans_Inv_Slno)+1,'1')Trans_Inv_Slno from Trans_Invoice_Head";
            cmd = new SqlCommand(sqlQry);
            cmd.Connection = con;
            cmd.Transaction = tran;
            int invid_code = Convert.ToInt32(cmd.ExecuteScalar());

            sqlQry = "insert into Trans_Invoice_Head(Trans_Inv_Slno,Sf_Code,Sf_Name,Dis_Code,Dis_Name,Cus_Code,Cus_Name,Order_Date,Invoice_Date,Delivery_Date,Pay_Term,Pay_Due,Ship_Method,Ship_Term,Sub_Total,Tax_Total,Total,Adv_Paid,Amt_Due,Remarks,Dis_total,Order_No,Division_Code)values(@Trans_Inv_Slno,@Sf_Code,@Sf_Name,@Dis_Code,@Dis_Name,@Cus_Code,@Cus_Name,@Order_Date,@Invoice_Date,@Delivery_Date,@Pay_Term,@Pay_Due,@Ship_Method,@Ship_Term,@Sub_Total,@Tax_Total,@Total,@Adv_Paid,@Amt_Due,@Remarks,@Dis_total,@Order_No,@Division_Code)";
            cmd = new SqlCommand(sqlQry);
            cmd.Connection = con;
            cmd.Transaction = tran;
            DateTime orderdate = Convert.ToDateTime(JMTE.MTED[0].order_date);
            string ord_date = orderdate.ToString("yyyy-MM-dd");
            DateTime inv = Convert.ToDateTime(JMTE.MTED[0].inv_date);
            string inv_da = inv.ToString("yyyy-MM-dd");
            DateTime del = Convert.ToDateTime(JMTE.MTED[0].del_date);
            string del_da = inv.ToString("yyyy-MM-dd");
            DateTime due = Convert.ToDateTime(JMTE.MTED[0].del_date);
            string due_da = inv.ToString("yyyy-MM-dd");

            cmd.Parameters.AddWithValue("@Trans_Inv_Slno", invid_code);
            cmd.Parameters.AddWithValue("@Sf_Code", JMTE.MTED[0].sf_code);
            cmd.Parameters.AddWithValue("@Sf_Name", JMTE.MTED[0].sf_name);
            cmd.Parameters.AddWithValue("@Dis_Code", JMTE.MTED[0].dis_code);
            cmd.Parameters.AddWithValue("@Dis_Name", JMTE.MTED[0].dis_name);
            cmd.Parameters.AddWithValue("@Cus_Code", JMTE.MTED[0].cus_code);
            cmd.Parameters.AddWithValue("@Cus_Name", JMTE.MTED[0].cus_name);
            cmd.Parameters.AddWithValue("@Order_Date", ord_date);
            cmd.Parameters.AddWithValue("@Invoice_Date", inv_da);
            cmd.Parameters.AddWithValue("@Delivery_Date", del_da);
            cmd.Parameters.AddWithValue("@Pay_Term", JMTE.MTED[0].pay_term);
            cmd.Parameters.AddWithValue("@Pay_Due", due_da);
            cmd.Parameters.AddWithValue("@Ship_Method", JMTE.MTED[0].ship_mtd);
            cmd.Parameters.AddWithValue("@Ship_Term", JMTE.MTED[0].ship_term);
            cmd.Parameters.AddWithValue("@Sub_Total", JMTE.MTED[0].sub_total);
            cmd.Parameters.AddWithValue("@Tax_Total", JMTE.MTED[0].tax_total);
            cmd.Parameters.AddWithValue("@Total", JMTE.MTED[0].total);
            cmd.Parameters.AddWithValue("@Adv_Paid", JMTE.MTED[0].adv_paid);
            cmd.Parameters.AddWithValue("@Amt_Due", JMTE.MTED[0].Amt_due);
            cmd.Parameters.AddWithValue("@Remarks", JMTE.MTED[0].remark);
			cmd.Parameters.AddWithValue("@Dis_total", JMTE.MTED[0].Dis_total);
            cmd.Parameters.AddWithValue("@Order_No", JMTE.MTED[0].order_no);
            cmd.Parameters.AddWithValue("@Division_Code", Div_code);

            cmd.ExecuteNonQuery();
            itemid = invid_code;


            for (int i = 0; i < JMTE.TED.Count; i++)
            {

                sqlQry = "insert into Trans_Invoice_Details(Trans_Inv_Slno,Trans_Order_No,Product_Code,Product_Name,Price,Discount,Free,Quantity,Amount)values(@Trans_Inv_Slno,@Trans_Order_No,@Product_Code,@Product_Name,@Price,@Discount,@Free,@Quantity,@Amount)";
                cmd = new SqlCommand(sqlQry);
                cmd.Connection = con;
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@Trans_Inv_Slno", itemid);
                cmd.Parameters.AddWithValue("@Trans_Order_No", JMTE.TED[i].Trans_order_no);
                cmd.Parameters.AddWithValue("@Product_Code", JMTE.TED[i].Product_code);
                cmd.Parameters.AddWithValue("@Product_Name", JMTE.TED[i].Product_name);
                cmd.Parameters.AddWithValue("@Price", JMTE.TED[i].Price);
                cmd.Parameters.AddWithValue("@Discount", JMTE.TED[i].Discount);
                cmd.Parameters.AddWithValue("@Free", JMTE.TED[i].Free);
                cmd.Parameters.AddWithValue("@Quantity", JMTE.TED[i].Quantity);
                cmd.Parameters.AddWithValue("@Amount", JMTE.TED[i].Amount);
                cmd.ExecuteNonQuery();


                for (int k = 0; k < JMTE.TED[i].taxDtls.Count; k++)
                {

                    sqlQry = "insert into Trans_Invoice_Tax_Details(Trans_Inv_Slno,Trans_Order_No,Tax_Code,Tax_Name,Tax_Amt)values(@Trans_Inv_Slno,@Trans_Order_No,@Tax_Code,@Tax_Name,@Tax_Amt)";
                    cmd = new SqlCommand(sqlQry);
                    cmd.Connection = con;
                    cmd.Transaction = tran;
                    cmd.Parameters.AddWithValue("@Trans_Inv_Slno", itemid);
                    cmd.Parameters.AddWithValue("@Trans_Order_No", JMTE.TED[i].Trans_order_no);
                    cmd.Parameters.AddWithValue("@Tax_Code", JMTE.TED[i].taxDtls[k].taxCode);
                    cmd.Parameters.AddWithValue("@Tax_Name", JMTE.TED[i].taxDtls[k].tax_Name);
                    cmd.Parameters.AddWithValue("@Tax_Amt", JMTE.TED[i].taxDtls[k].value);


                    cmd.ExecuteNonQuery();
                }

                //  Product prd = new Product();
                //DateTime dtgrn = DateTime.ParseExact(JMTE.MTED[0].inv_date, "dd/MM/yyyy", null);
                //string strgrn = dtgrn.ToString("MM-dd-yyyy");
                //DataSet dsCurSkt = prd.Select_Trans_CurrStock_Distwise(Div_code, JMTE.MTED[0].dis_code, JMTE.TED[i].Product_code);

                //if (dsCurSkt.Tables[0].Rows.Count > 0)
                //{
                //    decimal entry_Stock = Convert.ToDecimal(JMTE.TED[i].Quantity);
                //    foreach (DataRow row in dsCurSkt.Tables[0].Rows)
                //    {
                //        if ("Good" == "Good")
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
                            
                //            int num = prd.Update_Trans_CurrStock_Batchwise_Transfer1(Div_code, JMTE.MTED[0].dis_code, JMTE.TED[i].Product_code, row["BatchNo"].ToString(), upVal.ToString("0.00"), "Good", JMTE.MTED[0].oqty.ToString(), "-");
                            
                //            string Trn_No = Convert.ToString(itemid);
                //            int ledIns = prd.Insert_Trans_Stock_Ledger(Div_code, strgrn, JMTE.MTED[0].dis_code, JMTE.TED[i].Product_code, row["BatchNo"].ToString(), upVal.ToString("0.00"),"0", "0", "", Trn_No, JMTE.MTED[0].dis_name,"", "INV");


                //            ////DataSet dsSkt = prd.Select_Trans_CurrStock_Distwise(Div_Code, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString());
                //            ////if (dsSkt.Tables[0].Rows.Count > 0)
                //            ////{
                //            ////    int numup = prd.Update_Trans_CurrStock_Batchwise_Transfer(Div_Code, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "Good", "+");
                //            ////   int ledInsIn = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "0", "1", "", "ISS");

                //            ////}
                //            ////else
                //            ////{
                //            ////    int numup = prd.Insert_Trans_CurrStock_Batchwise(Div_Code, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "0");
                //            ////    int ledInsIn = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "0", "1", "", "ISS");
                //            ////}
                //        }
                        
                //    }
                //}
				


            }


            msg = "Record Update Success";
            tran.Commit();
            con.Close();
        }



        catch (Exception exp)
        {
            if (tran != null)
                tran.Rollback();
            msg = exp.Message.ToString() + "\nTransaction Rolledback, Tim didn't make it.";
        }




        return msg;

    }

    public class JSonHelper
    {
        public string ConvertObjectToJSon<T>(T obj)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, obj);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;

        }
        public T ConverJSonToObject<T>(string jsonString)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)serializer.ReadObject(ms);
            return obj;
        }
    }

    public class MainTransInv
    {
        public List<Trans_Invoice_Head> MTED = new List<Trans_Invoice_Head>();
        public List<Trans_Invoice_Details> TED = new List<Trans_Invoice_Details>();

    }

    public class Trans_Invoice_Head
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

        public List<Trans_Invoice_Tax_Details> taxDtls { get; set; }
        public Trans_Invoice_Details()
        {
            taxDtls = new List<Trans_Invoice_Tax_Details>(0);
        }

    }
    public class Trans_Invoice_Tax_Details
    {

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


    [WebMethod(EnableSession = true)]
    public static string Get_AllValues(string Cus_code)
    {
      

        string osAmt = string.Empty;
       
        osAmt = "0,0";
    
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

    public class Holodayss
    {
        public string label { get; set; }
        public string value { get; set; }
        public string id { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static Holodayss[] GetHolidays()
    {
        string div_code = "";
        try
        {
            div_code = HttpContext.Current.Session["div_code"].ToString();
        }
        catch
        {
            div_code = HttpContext.Current.Session["Division_Code"].ToString();
        }


        List<Holodayss> HDay = new List<Holodayss>();
        DataSet dsAlowtype = null;
        Holiday hd = new Holiday();

        dsAlowtype = hd.GetProdNew(div_code);

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

}