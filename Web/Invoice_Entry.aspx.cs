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
using DBase_EReport;

public partial class Invoice_Entry : System.Web.UI.Page
{
    string dis_code = string.Empty;
    string cus_code = string.Empty;
    string or_date = string.Empty;
    DataSet dsSf = null;
    DataSet dsSf_or = null;
    DataSet tax = null;
    public static string div_code = string.Empty;
    DataSet dsTP = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Convert.ToString(Session["div_code"]);
        if (!this.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Or_Date"]) && !string.IsNullOrEmpty(Request.QueryString["Dis_code"]))
            {

                or_date = Request.QueryString["Or_Date"];
                dis_code = Request.QueryString["Dis_code"];
                cus_code = Request.QueryString["Cus_code"];
                InvoiceNumber.Value = Request.QueryString["Or_Date"];
                Txt_in_date.Value = DateTime.Now.ToString("dd/MM/yyyy");
                DateTime date = Convert.ToDateTime(Request.QueryString["Or_Date"]);
                InvoiceNumber.Value = date.ToString("dd/MM/yyyy");
                DCR dc = new DCR();
                dsSf = dc.view_total_order_view_in(dis_code, or_date, cus_code);
                if (dsSf.Tables[0].Rows.Count > 0)
                {
                    Hid_orderno.Value = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Hid_Sf_Name.Value = dsSf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    Txt_fldForce.Value = dsSf.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    Hid_Dis_Name.Value = dsSf.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    Txt_dist.Value = dsSf.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    Hid_Cus_Name.Value = dsSf.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    Txt_Retailer.Value = dsSf.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                }


                dsSf_or = dc.view_total_order_view_in_order(dis_code, or_date, cus_code);
                if (dsSf_or.Tables[0].Rows.Count > 0)
                {

                    rptOrders.DataSource = dsSf_or;
                    rptOrders.DataBind();
                    if (dsSf_or.Tables[0].Rows.Count == 0)
                    {
                        Control FooterTemplate = rptOrders.Controls[rptOrders.Controls.Count - 1].Controls[0];
                        FooterTemplate.FindControl("trEmpty").Visible = true;
                    }


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

        dcrr dc = new dcrr();
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
        string itemid;
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
            //sqlQry = "SELECT isnull(max(Trans_Inv_Slno)+1,'1')Trans_Inv_Slno from Trans_Invoice_Head";
			sqlQry="declare @FDT varchar(50),@sl varchar(50),@invsfsl varchar(150),@MxD1 varchar(200),@erpcode varchar(20) " +

                      "set @FDT = CONVERT(date, GETDATE())" +
                      "if (@FDT >= CAST(CAST(YEAR(@FDT) as varchar) + '-04-01' as date))" +
                       "begin select @sl = CAST(YEAR(@FDT) as varchar) + '-' + CAST(((YEAR(@FDT)) + 1) as varchar)" +
                       "end else begin select @sl = CAST((YEAR(@FDT) - 1) as varchar) + '-' + CAST(((YEAR(@FDT))) as varchar) end " +

                       "select @erpcode = Erp_Code from mas_stockist where stockist_code = '"+ JMTE.MTED[0].dis_code + "'" +

                        "SELECT @invsfsl = isnull(InvSlNo, 0) + 1 FROM Mas_SF_SlNo where SF_Code = '"+ JMTE.MTED[0].sf_code + "'" +

                       "update Mas_SF_SlNo set InvSlNo = @invsfsl where SF_Code = '"+ JMTE.MTED[0].sf_code+"'" +

                       "SELECT @erpcode + '-' + @sl + '-INV-' + cast(@invsfsl as varchar)";
            cmd = new SqlCommand(sqlQry);
            cmd.Connection = con;
            cmd.Transaction = tran;
            string invid_code = Convert.ToString(cmd.ExecuteScalar());

            sqlQry = "insert into Trans_Invoice_Head(Trans_Inv_Slno,Sf_Code,Sf_Name,Dis_Code,Dis_Name,Cus_Code,Cus_Name,Order_Date,Invoice_Date,Delivery_Date,Pay_Term,Pay_Due,Ship_Method,Ship_Term,Sub_Total,Tax_Total,Total,Adv_Paid,Amt_Due,Remarks,Dis_total,Order_No,Division_Code)values(@Trans_Inv_Slno,@Sf_Code,@Sf_Name,@Dis_Code,@Dis_Name,@Cus_Code,@Cus_Name,@Order_Date,@Invoice_Date,@Delivery_Date,@Pay_Term,@Pay_Due,@Ship_Method,@Ship_Term,@Sub_Total,@Tax_Total,@Total,@Adv_Paid,@Amt_Due,@Remarks,@Dis_total,@Order_No,@div)";
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
		    cmd.Parameters.AddWithValue("@div", div_code);


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


                sqlQry = "update  Trans_order_Details set Trans_Inv_Slno = @Trans_Inv_Slno where Trans_Order_No=@Trans_Order_No";
                cmd = new SqlCommand(sqlQry);
                cmd.Connection = con;
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@Trans_Inv_Slno", itemid);
                cmd.Parameters.AddWithValue("@Trans_Order_No", JMTE.TED[i].Trans_order_no);               
                cmd.ExecuteNonQuery();


                for (int k = 0; k < JMTE.TED[i].taxDtls.Count; k++)
                {
					if (JMTE.TED[i].taxDtls[k].taxCode != "0")
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
                }

				  Product prd = new Product();
                DateTime dtgrn = DateTime.ParseExact(JMTE.MTED[0].inv_date, "dd/MM/yyyy", null);
                string strgrn = dtgrn.ToString("MM-dd-yyyy");
                DataSet dsCurSkt = prd.Select_Trans_CurrStock_Distwise(Div_code, JMTE.MTED[0].dis_code, JMTE.TED[i].Product_code);

                if (dsCurSkt.Tables[0].Rows.Count > 0)
                {
                    decimal entry_Stock = Convert.ToDecimal(JMTE.TED[i].Quantity);
                    foreach (DataRow row in dsCurSkt.Tables[0].Rows)
                    {
                        if ("Good" == "Good")
                        {
                            decimal tot = Convert.ToDecimal(row["GStock"].ToString()) - entry_Stock;
                            decimal upVal = 0;
                            if (tot >= 0)
                            {
                                upVal = entry_Stock;
                                entry_Stock = 0;
                            }
                            else
                            {
                                upVal = Convert.ToDecimal(row["GStock"].ToString());
                                entry_Stock = Math.Abs(tot);
                            }
                            
                            int num = prd.Update_Trans_CurrStock_Batchwise_Transfer1(Div_code, JMTE.MTED[0].dis_code, JMTE.TED[i].Product_code, row["BatchNo"].ToString(), upVal.ToString("0.00"), "Good", JMTE.MTED[0].oqty.ToString(), "-");
                            
                            string Trn_No = Convert.ToString(itemid);
                            int ledIns = prd.Insert_Trans_Stock_Ledger(Div_code, strgrn, JMTE.MTED[0].dis_code, JMTE.TED[i].Product_code, row["BatchNo"].ToString(), upVal.ToString("0.00"),"0", "0", "", Trn_No, JMTE.MTED[0].dis_name,"", "INV");


                        }
                        
                    }
                }
				


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
    public static string Get_AllValues(int Cus_code)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        CurrStock prd = new CurrStock();

        string osAmt = string.Empty;
        string curr_out = string.Empty;
        DataSet dsGoods = null;
        dsGoods = prd.Get_Stock_Out_standing(Cus_code,Div_Code,"");
        if (dsGoods.Tables[0].Rows.Count > 0)
        {
            osAmt = Convert.ToDecimal(dsGoods.Tables[0].Rows[0]["Out_standing_Amt"]).ToString() +"," + Convert.ToDecimal(dsGoods.Tables[0].Rows[0]["Credit_Limit"]).ToString();
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
		
		
		
		 public DataSet Get_Stock_Out_standing(int Cus_code, string Div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            //strQry = "select isnull(a.Out_standing_Amt,0)Out_standing_Amt,isnull(b.Credit_Limit,0)Credit_Limit from " +
            //         "Order_Collection_Details a right outer join Mas_ListedDr b on a.Cust_Code=b.ListedDrCode " +
            //         "where b.Division_Code='" + Div_code + "' and  b.ListedDrCode='" + Cus_code + "'";
            string strQry = "EXEC check_retailer_outstanding '" + Cus_code + "','" + Div_code + "' ";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
		
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
	
	public class dcrr
    {
        public DataSet view_Pay_Mas(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            // strQry = "select Code,Name from Mas_Payment_Type where Division_Code='" + div_code + "' and Active_Flag=0";
            string strQry = "select Code, Name from Mas_Payment_Type where charindex('," + div_code + ",', ',' + cast(Division_Code as varchar) + ',') > 0 and Active_Flag=0";
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
    }

}