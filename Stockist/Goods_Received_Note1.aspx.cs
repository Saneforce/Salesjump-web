using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.Transactions;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using DBase_EReport;

public partial class MasterFiles_Goods_Received_Note : System.Web.UI.Page
{
    string Div_Code = string.Empty;
    string SF_Code = string.Empty;
    string Sub_DivCode = string.Empty;
    string mode = string.Empty;
    string grn_no = string.Empty;
    string grn_dt = string.Empty;
    string supp_code = string.Empty;
	public static DataSet ds = null;
    public static StockistMaster sm = new StockistMaster();
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        //    mode = Request.QueryString["Mode"].ToString();
        //    hdnmode.Value = mode;
        //    if (mode == "1")
        //    {
        //        grn_no = Request.QueryString["GRN_No"].ToString();
        //        hdngrn_no.Value = grn_no;
        //        grn_dt = Request.QueryString["GRN_Date"].ToString();
        //        hdngrn_date.Value = grn_dt;
        //        supp_code = Request.QueryString["Supp_Code"].ToString();
        //        hdnsupp_code.Value = supp_code;
        //    }
        }
    }


    public class Distributor
    {
        public string disName { get; set; }
        public string disCode { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static Distributor[] GetDistributor()
    {

        //string DDiv_code= Session["div_code"].ToString();
        //string DSf_Code= Session["Sf_Code"].ToString();
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string DSub_DivCode = "0";
        List<Distributor> distributor = new List<Distributor>();
        DataSet dsDistributor = null;
        Bus_EReport.Stockist stk = new Bus_EReport.Stockist();
        dsDistributor = stk.GetStockist_subdivisionwise(divcode: DDiv_code.TrimEnd(','), subdivision: DSub_DivCode, sf_code: DSf_Code);
        if (dsDistributor.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in dsDistributor.Tables[0].Rows)
            {
                Distributor dis = new Distributor();
                dis.disCode = row["Stockist_code"].ToString();
                dis.disName = row["Stockist_Name"].ToString();
                distributor.Add(dis);
            }
        }
        return distributor.ToArray();
    }

    public class OrderNO
    {
        public string Order_NO { get; set; }

    }
    [WebMethod(EnableSession = true)]
    public static string GetOrderNo()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string DSub_DivCode = string.Empty;
        List<OrderNO> orderno = new List<OrderNO>();
        DataSet dspono = GetPOoRDERS(Stockist_Code, Div_Code);;
       StockistMaster sm = new StockistMaster();
        return JsonConvert.SerializeObject(dspono.Tables[0]);
    }
    public static DataSet GetPOoRDERS(string Stockist_Code, string Div_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;
        string strQry = "select tp.Trans_Sl_No, tp.Order_Date,sap_code,convert(varchar,isnull(th.Order_Flag,5) ) Order_Flag from Trans_PriOrder_Head tp left join Trans_Order_Head th on th.Trans_Sl_No =tp.Trans_Sl_No";
         strQry +=" where tp.Stockist_Code =  '" + Stockist_Code + "' and tp.Order_Value> 0 and tp.Division_Code = '" + Div_Code + "' and tp.Order_Flag < 1 and tp.Order_Flag <> 2 order by Trans_Sl_No  ";
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
    public static string GetSuppilerByOrder()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DataSet ds = null;
        StockistMaster sm = new StockistMaster();
        //ds = sm.getsuppilerbyorder(Div_Code.TrimEnd(','), Stockist_Code);
        
        ds = getsuppilerbyorder(Div_Code.TrimEnd(','), Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getsuppilerbyorder(string div_code, string stk)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;
        // strQry = "select HO_ID,Name from Mas_HO_ID_Creation mhic join Trans_PriOrder_head tpd on mhic.HO_ID = tpd.Division_Code where tpd.Trans_Sl_No = '" + orderid + "' and HO_Active_Flag = '0'";

        //strQry = "select distinct(HO_ID),Name from Mas_HO_ID_Creation mhic join Trans_PriOrder_Details tpd on mhic.HO_ID = tpd.Division_Code where Trans_Sl_No = '"+ orderid + "'";

        string strQry = "EXEC get_com_sup_stk_Details '" + div_code + "','" + stk + "'";

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
    public static string  GetProduct(string Orderid)
    {
        string PDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        //StockistMaster sm = new StockistMaster();
        DataSet ds = new DataSet();
        //ds = sm.getproductname(Orderid, PDiv_code.TrimEnd(','), Stockist_Code);
        ds = getproductname(Orderid, PDiv_code.TrimEnd(','), Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet getproductname(string Orderid, string div_code, string Stockist_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsState = null;
        string strQry = "EXEC get_grn_pro_details '" + Orderid + "','" + div_code + "','" + Stockist_Code + "'";
        try
        {
            dsState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsState;
    }
    public class Tax_Details
    {
        public string Exp_code { get; set; }
        public string Exp_name { get; set; }
        public string Tax_Id { get; set; }
    }


    [WebMethod(EnableSession = true)]
    public static Tax_Details[] GetTAXType()
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
            ffd.Tax_Id = row["Tax_Id"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }


    public class CurrStock
    {
        public string Dist_Code { get; set; }
        public string Prod_Code { get; set; }
        public string GStock { get; set; }
        public string DStock { get; set; }
        public string BatchNo { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static CurrStock[] GetCurrStock()
    {
        string PDiv_code = HttpContext.Current.Session["div_code"].ToString();
        List<CurrStock> product = new List<CurrStock>();
        DataSet dsProduct = null;
        Product p = new Product();

        dsProduct = p.getCurrentStock_Batch(PDiv_code.TrimEnd(','));
        if (dsProduct != null)
        {
            foreach (DataRow row in dsProduct.Tables[0].Rows)
            {
                CurrStock pro = new CurrStock();
                pro.Dist_Code = row["Dist_Code"].ToString();
                pro.Prod_Code = row["Prod_Code"].ToString();
                pro.GStock = row["GStock"].ToString();
                pro.DStock = row["DStock"].ToString();
                pro.BatchNo = row["BatchNo"].ToString();
                product.Add(pro);
            }
        }
        return product.ToArray();
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

    public class XMLHelper
    {
        public static string Serialize(object dataToSerialize)
        {
            if (dataToSerialize == null) return null;

            using (StringWriter stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(dataToSerialize.GetType());
                serializer.Serialize(stringwriter, dataToSerialize);
                return stringwriter.ToString();
            }
        }
        public static T Deserialize<T>(string xmlText)
        {
            if (String.IsNullOrWhiteSpace(xmlText)) return default(T);

            using (StringReader stringReader = new System.IO.StringReader(xmlText))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }
    }



    //public class MainTransGRN
    //{
    //    public List<Trans_Head> TransH = new List<Trans_Head>();
    //    public List<Trans_Details> TransD = new List<Trans_Details>();
    //}


    public class Trans_GRN_Head
    {
        public string GRN_No { get; set; }
        public string GRN_Date { get; set; }
        public string Entry_Date { get; set; }
        public string Supp_Code { get; set; }
        public string Supp_Name { get; set; }
        public string Po_No { get; set; }
        public string Challan_No { get; set; }
        public string Dispatch_Date { get; set; }
        public string Received_Location { get; set; }
        public string Received_Name { get; set; }
        public string Received_By { get; set; }
        public string Authorized_By { get; set; }
        public string remarks { get; set; }
        public string goodsTot { get; set; }
        public string taxTot { get; set; }
        public string netTot { get; set; }

    }

    public class Trans_GRN_Details
    {
        public string PCode { get; set; }
        public string PDetails { get; set; }
        public string UOM { get; set; }
        public string UOM_Name { get; set; }
        public string Batch_No { get; set; }
        public string Erp_Code { get; set; }
        public string POQTY { get; set; }
        public string Price { get; set; }
        public string mfgDate { get; set; }
        public string Good { get; set; }
        public string Damaged { get; set; }
        public string Gross_Value { get; set; }
        public string Net_Value { get; set; }
        public string SGood { get; set; }
        public string SDamaged { get; set; }
        public string Offer_pro_code { get; set; }  
        public string Offer_pro_name { get; set; }
        public string offer_pro_unit { get; set; }
        public string con_factor { get; set; }
        public string Remarks { get; set; }
        public string Tax { get; set; }
        public string free { get; set; }
        public string dis { get; set; }
        public string dis_val { get; set; }
    }
    public class Trans_GRN_Tax_Details
    {
        public string Tax_Code { get; set; }
        public string Tax_Name { get; set; }
        public string Tax_Value { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static string SaveDate(string Head,string Details, string Tax,string SaveMode)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string SF_Code = HttpContext.Current.Session["sf_code"].ToString();
        string Sub_DivCode = "0";//HttpContext.Current.Session["subdivision_code"].ToString();
        //MainTransGRN GRN = new MainTransGRN();
        //JSonHelper helper = new JSonHelper();
        //GRN = helper.ConverJSonToObject<MainTransGRN>(data);
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        string msg = string.Empty;
        int iReturn = -1;
        Product prd = new Product();
        string sqlQry = string.Empty;
        SqlCommand cmd = new SqlCommand();
        using (var scope = new TransactionScope())
        {
            try
            {

                IList<Trans_GRN_Head> GRN_Head = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Trans_GRN_Head>>(Head);
                IList<Trans_GRN_Details> GRN_details = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Trans_GRN_Details>>(Details);
                IList<Trans_GRN_Tax_Details> GRN_tax_details = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Trans_GRN_Tax_Details>>(Tax);


                DateTime dtgrn = Convert.ToDateTime(GRN_Head[0].GRN_Date);
                string strgrn = dtgrn.ToString("MM-dd-yyy");

                DateTime dtentry = Convert.ToDateTime(GRN_Head[0].Entry_Date);
                string strentry = dtentry.ToString("MM-dd-yyy");

                DateTime dtdispatch = Convert.ToDateTime(GRN_Head[0].Dispatch_Date);
                string strdispatch = dtdispatch.ToString("MM-dd-yyy");


                //DateTime dtgrn = DateTime.ParseExact(GRN_Head[0].GRN_Date, "yyyy/MM/dd", null);
                //string strgrn = dtgrn.ToString("MM-dd-yyyy");

                //DateTime dtentry = DateTime.ParseExact(GRN_Head[0].Entry_Date, "dd/MM/yyyy", null);
                //string strentry = dtentry.ToString("MM-dd-yyyy");

                //DateTime dtdispatch = DateTime.ParseExact(GRN_Head[0].Dispatch_Date, "dd/MM/yyyy", null);
                //string strdispatch = dtdispatch.ToString("MM-dd-yyyy");


                DataSet dsGoods = null;
                if (SaveMode == "1")
                {
                    dsGoods = prd.Get_GoodsReceived(Div_Code.TrimEnd(','), GRN_Head[0].GRN_No.ToString(), strgrn, GRN_Head[0].Supp_Code.ToString());
                }
                else
                {
                dsGoods = prd.Get_GoodsReceived_withoutNo(Div_Code.TrimEnd(','), strgrn, GRN_Head[0].Supp_Code.ToString(), GRN_Head[0].Received_Location.ToString(),GRN_Head[0].Po_No);
                    if (dsGoods.Tables[0].Rows.Count > 0)
                    {
                        scope.Dispose();
                        msg = "Record  Already Exist Can't Add..!!";
                        return msg;
                    }
                }

                if (dsGoods.Tables[0].Rows.Count <= 0)
                {
                    int Trans_No = prd.Insert_GoodsReceived(Div_Code.TrimEnd(','), SF_Code, GRN_Head[0].GRN_No.ToString(), strgrn, GRN_Head[0].Supp_Code.ToString(), GRN_Head[0].Supp_Name.ToString(), GRN_Head[0].Challan_No.ToString(), GRN_Head[0].Po_No.ToString(), strentry, strdispatch, GRN_Head[0].Received_Location.ToString(), GRN_Head[0].Received_By.ToString(), GRN_Head[0].Authorized_By.ToString(), Sub_DivCode, GRN_Head[0].remarks.ToString(), GRN_Head[0].goodsTot.ToString(), GRN_Head[0].taxTot.ToString(), GRN_Head[0].netTot.ToString(), GRN_Head[0].Received_Name.ToString());

                    sqlQry = "update Trans_priorder_head set Order_Flag='1' where Order_No='"+ GRN_Head[0].Po_No + "' and Division_Code='"+Div_Code.TrimEnd(',') + "' and Stockist_Code='"+ SF_Code + "'";
                    DB_EReporting db = new DB_EReporting();

                    //cmd = new SqlCommand(sqlQry);
                    //cmd.Connection = con;
                    //con.Open();
                    //con.Close();
                    int invid_code = db.ExecQry(sqlQry);


                    
                    for (int i = 0; i < GRN_details.Count; i++)
                    {
                        decimal poQty = GRN_details[i].POQTY == "" ? 0 : Convert.ToDecimal(GRN_details[i].POQTY);
                        if (poQty > 0)
                        {

                            //DateTime mfgdt = DateTime.ParseExact(GRN_details[i].mfgDate, "dd/MM/yyyy", null);
                          // string strmfgdt = mfgdt.ToString("MM-dd-yyyy");

                            DateTime mfgdt = Convert.ToDateTime(GRN_details[i].mfgDate);
                           string strmfgdt = dtgrn.ToString("MM-dd-yyy");
                
                            int Dtls_No = Insert_GoodsReceived_Details(Trans_No.ToString(), GRN_details[i].PCode.ToString(), GRN_details[i].PDetails.ToString(), 
                                GRN_details[i].UOM.ToString(), GRN_details[i].Batch_No.ToString(), GRN_details[i].POQTY.ToString(), GRN_details[i].Price.ToString(),
                                GRN_details[i].Good.ToString(), GRN_details[i].Damaged.ToString(), GRN_details[i].Gross_Value.ToString(), GRN_details[i].Net_Value.ToString(), 
                                GRN_details[i].UOM_Name.ToString(), strmfgdt, GRN_details[i].Remarks.ToString(), GRN_details[i].Offer_pro_code.ToString(), 
                                GRN_details[i].Offer_pro_name.ToString(), GRN_details[i].offer_pro_unit.ToString(), GRN_details[i].con_factor.ToString(), 
                                GRN_details[i].free.ToString(), GRN_details[i].dis.ToString(), GRN_details[i].dis_val.ToString(),GRN_details[i].Tax.ToString());
                             //int Dtls_No = prd.Insert_GoodsReceived_Details(Trans_No.ToString(), GRN_details[i].PCode.ToString(), GRN_details[i].PDetails.ToString(), 
                             //   GRN_details[i].UOM.ToString(), GRN_details[i].Batch_No.ToString(), GRN_details[i].POQTY.ToString(), GRN_details[i].Price.ToString(),
                             //   GRN_details[i].Good.ToString(), GRN_details[i].Damaged.ToString(), GRN_details[i].Gross_Value.ToString(), GRN_details[i].Net_Value.ToString(), 
                             //   GRN_details[i].UOM_Name.ToString(), strmfgdt, GRN_details[i].Remarks.ToString(), GRN_details[i].Offer_pro_code.ToString(), 
                             //   GRN_details[i].Offer_pro_name.ToString(), GRN_details[i].offer_pro_unit.ToString(), GRN_details[i].con_factor.ToString(), 
                             //   GRN_details[i].free.ToString(), GRN_details[i].dis.ToString(), GRN_details[i].dis_val.ToString(),GRN_details[i].Tax.ToString());

                            DataSet dsLedger = null;
                            DataSet dsCurrStock = null;

                            dsLedger = prd.Select_Trans_Stock_Ledger(Div_Code.TrimEnd(','), GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), strgrn, GRN_details[i].Batch_No.ToString());
                            if (dsLedger.Tables[0].Rows.Count <= 0)
                            {
                                //int num = prd.Insert_Trans_Stock_Ledger(Div_Code.TrimEnd(','), strgrn, GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), GRN_details[i].Batch_No.ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) / Convert.ToDecimal(GRN_details[i].Good)).ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].Damaged)).ToString(), "1", "", ("GRN" + Trans_No), GRN_Head[0].Supp_Name.ToString(), GRN_Head[0].Received_Name.ToString(), "GRN");
                                int num = Insert_Trans_Stock_Ledger(Div_Code.TrimEnd(','), strgrn, GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), GRN_details[i].Batch_No.ToString(), Convert.ToDecimal(GRN_details[i].Good).ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].Damaged)).ToString(), "1", "", ("GRN" + Trans_No), GRN_Head[0].Supp_Name.ToString(), GRN_Head[0].Received_Name.ToString(), "GRN","0");
                            }
                            else
                            {
                                string ldrid = dsLedger.Tables[0].Rows[0][0].ToString();
                                //  int num = prd.Update_Trans_Stock_Ledger(Div_Code.TrimEnd(','), ldrid, GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), strgrn, (Convert.ToDecimal(GRN_details[i].Erp_Code) / Convert.ToDecimal(GRN_details[i].SGood)).ToString(),(Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].SDamaged)).ToString(), GRN_details[i].Batch_No.ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].Good)).ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].Damaged)).ToString());
                                int num = prd.Update_Trans_Stock_Ledger(Div_Code.TrimEnd(','), ldrid, GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), strgrn,Convert.ToDecimal(GRN_details[i].SGood).ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].SDamaged)).ToString(), GRN_details[i].Batch_No.ToString(), Convert.ToDecimal(GRN_details[i].Good).ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].Damaged)).ToString());
                            }

                            dsCurrStock = prd.Select_Trans_CurrStock_Batchwise(Div_Code.TrimEnd(','), GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), GRN_details[i].Batch_No.ToString());

                            if (dsCurrStock.Tables[0].Rows.Count <= 0)
                            {
                                //int num = prd.Insert_Trans_CurrStock_Batchwise(Div_Code.TrimEnd(','), GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), GRN_details[i].Batch_No.ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) / Convert.ToDecimal(GRN_details[i].Good)).ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].Damaged)).ToString());
                                int num = prd.Insert_Trans_CurrStock_Batchwise(Div_Code.TrimEnd(','), GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), GRN_details[i].Batch_No.ToString(),Convert.ToDecimal(GRN_details[i].Good).ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].Damaged)).ToString());
                            }
                            else
                            {
                                string slno = dsCurrStock.Tables[0].Rows[0][0].ToString();
                                //int num = prd.Update_Trans_CurrStock_Batchwise(Div_Code.TrimEnd(','), slno, GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), GRN_details[i].Batch_No.ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) / Convert.ToDecimal(GRN_details[i].SGood)).ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].SDamaged)).ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].Good)).ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].Damaged)).ToString());
                                 int num = prd.Update_Trans_CurrStock_Batchwise(Div_Code.TrimEnd(','), slno, GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), GRN_details[i].Batch_No.ToString(),Convert.ToDecimal(GRN_details[i].SGood).ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].SDamaged)).ToString(),Convert.ToDecimal(GRN_details[i].Good).ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].Damaged)).ToString());
                            }

                            for (int j = 0; j < GRN_tax_details.Count; j++)
                            {
                                if (GRN_tax_details[j].Tax_Code.ToString() != "0")
                                {
                                    iReturn = prd.Insert_GoodsReceived_Tax(Dtls_No.ToString(), Trans_No.ToString(), GRN_tax_details[j].Tax_Code.ToString(), GRN_tax_details[j].Tax_Name.ToString(), GRN_tax_details[j].Tax_Value.ToString());
                                }
                            }
                        }                           
                    }
                }
                else
                {
                    string tran_id = dsGoods.Tables[0].Rows[0][0].ToString();
                    int Trans_No = prd.Update_GoodsReceived(Div_Code.TrimEnd(','), SF_Code, GRN_Head[0].GRN_No.ToString(), strgrn, GRN_Head[0].Supp_Code.ToString(), GRN_Head[0].Supp_Name.ToString(), GRN_Head[0].Challan_No.ToString(), GRN_Head[0].Po_No.ToString(), strentry, strdispatch, GRN_Head[0].Received_Location.ToString(), GRN_Head[0].Received_By.ToString(), GRN_Head[0].Authorized_By.ToString(), Sub_DivCode, tran_id, GRN_Head[0].remarks.ToString(), GRN_Head[0].goodsTot.ToString(), GRN_Head[0].taxTot.ToString(), GRN_Head[0].netTot.ToString(), GRN_Head[0].Received_Name.ToString());

                    iReturn = prd.Delete_GoodsReceived_Tax(tran_id);
                    iReturn = prd.Delect_GoodsReceived_Details(tran_id);

                    DataSet dsUpdateLedger = null;

                    dsUpdateLedger = prd.Select_Stock_Ledger(Div_Code.TrimEnd(','), GRN_Head[0].GRN_No.ToString());
                    foreach (DataRow ul in dsUpdateLedger.Tables[0].Rows)
                    {
                        int num = prd.Update_Trans_CurrStock_Batchwise(Div_Code.TrimEnd(','), "0", ul["Dist_Code"].ToString(), ul["Prod_Code"].ToString(), ul["BatchNo"].ToString(), ul["GStock"].ToString(), ul["DStock"].ToString(), "0", "0");
                    }

                    iReturn = prd.Delect_Stock_Ladger(Div_Code.TrimEnd(','), GRN_Head[0].GRN_No.ToString());

                    for (int i = 0; i < GRN_details.Count; i++)
                    {
                        decimal poQty = GRN_details[i].POQTY == "" ? 0 : Convert.ToDecimal(GRN_details[i].POQTY);
                        if (poQty > 0)
                        {
                            //DateTime mfgdt = DateTime.ParseExact(GRN_details[i].mfgDate, "dd/MM/yyyy", null);
                            //string strmfgdt = mfgdt.ToString("MM-dd-yyyy");

                            DateTime mfgdt = Convert.ToDateTime(GRN_details[i].mfgDate);
                            string strmfgdt = dtgrn.ToString("MM-dd-yyy");

                            int Dtls_No = prd.Insert_GoodsReceived_Details(tran_id, GRN_details[i].PCode.ToString(), GRN_details[i].PDetails.ToString(), GRN_details[i].UOM.ToString(), GRN_details[i].Batch_No.ToString(), GRN_details[i].POQTY.ToString(), GRN_details[i].Price.ToString(), GRN_details[i].Good.ToString(), GRN_details[i].Damaged.ToString(), GRN_details[i].Gross_Value.ToString(), GRN_details[i].Net_Value.ToString(), GRN_details[i].UOM_Name.ToString(), strmfgdt, GRN_details[i].Remarks.ToString(), GRN_details[i].Offer_pro_code.ToString(), GRN_details[i].Offer_pro_name.ToString(),GRN_details[i].offer_pro_unit.ToString(), GRN_details[i].con_factor.ToString(),GRN_details[i].free.ToString(), GRN_details[i].dis.ToString(), GRN_details[i].dis_val.ToString(),GRN_details[i].Tax.ToString());

                            // DataSet dsLedger = null;
                            DataSet dsCurrStock = null;

                            //dsLedger = prd.Select_Trans_Stock_Ledger(Div_Code, GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), strgrn, GRN_details[i].Batch_No.ToString());
                            //if (dsLedger.Tables[0].Rows.Count <= 0)
                            //{
                           // int num_led = prd.Insert_Trans_Stock_Ledger(Div_Code.TrimEnd(','), strgrn, GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), GRN_details[i].Batch_No.ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) / Convert.ToDecimal(GRN_details[i].Good)).ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].Damaged)).ToString(), "1", "", ("GRN" + tran_id), GRN_Head[0].Supp_Name.ToString(), GRN_Head[0].Received_Name.ToString(), "GRN","0");
                            int num_led =Insert_Trans_Stock_Ledger(Div_Code.TrimEnd(','), strgrn, GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), GRN_details[i].Batch_No.ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) / Convert.ToDecimal(GRN_details[i].Good)).ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].Damaged)).ToString(), "1", "", ("GRN" + tran_id), GRN_Head[0].Supp_Name.ToString(), GRN_Head[0].Received_Name.ToString(), "GRN", "0");
                            //}
                            //else
                            //{
                            //    string ldrid = dsLedger.Tables[0].Rows[0][0].ToString();
                            //    int num = prd.Update_Trans_Stock_Ledger(Div_Code, ldrid, GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), strgrn, GRN_details[i].SGood.ToString(), GRN_details[i].SDamaged.ToString(), GRN_details[i].Batch_No.ToString(), GRN_details[i].Good.ToString(), GRN_details[i].Damaged.ToString());
                            //}

                            dsCurrStock = prd.Select_Trans_CurrStock_Batchwise(Div_Code.TrimEnd(','), GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), GRN_details[i].Batch_No.ToString());

                            if (dsCurrStock.Tables[0].Rows.Count <= 0)
                            {
                                int num = prd.Insert_Trans_CurrStock_Batchwise(Div_Code.TrimEnd(','), GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), GRN_details[i].Batch_No.ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) / Convert.ToDecimal(GRN_details[i].Good)).ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].Damaged)).ToString());
                            }
                            else
                            {
                                string slno = dsCurrStock.Tables[0].Rows[0][0].ToString();
                                int num = prd.Update_Trans_CurrStock_Batchwise(Div_Code.TrimEnd(','), slno, GRN_Head[0].Received_Location.ToString(), GRN_details[i].PCode.ToString(), GRN_details[i].Batch_No.ToString(), "0", "0", (Convert.ToDecimal(GRN_details[i].Erp_Code) / Convert.ToDecimal(GRN_details[i].Good)).ToString(), (Convert.ToDecimal(GRN_details[i].Erp_Code) * Convert.ToDecimal(GRN_details[i].Damaged)).ToString());
                            }

                            for (int j = 0; j < GRN_tax_details.Count; j++)
                            {
                                if (GRN_tax_details[j].Tax_Code.ToString() != "0")
                                {
                                    iReturn = prd.Insert_GoodsReceived_Tax(Dtls_No.ToString(), tran_id, GRN_tax_details[j].Tax_Code.ToString(), GRN_tax_details[j].Tax_Name.ToString(), GRN_tax_details[j].Tax_Value.ToString());
                                }
                            }
                        }
                    }
                }
                scope.Complete();
                scope.Dispose();
                msg = "Success";
            }
            catch (Exception ex)
            {
                scope.Dispose();
                msg = ex.Message;             
            }
        }

        return msg;
    }

    public static int Insert_GoodsReceived_Details(string Trans_No, string Pro_Code, string Pro_Name, string uom, string batchno, string qty, string price, string good, string damage, string groosval, string netval, string uom_name, string mfgDate, string remarks, string Offer_pro_code, string Offer_pro_name, string offer_pro_unit, string con_factor, string free, string dis, string dis_val, string Tax)
    {
        int iReturn = -1;
        int iSlNo = -1;
        try
        {
            DB_EReporting db = new DB_EReporting();

            string strQry = "SELECT ISNULL(MAX(Trans_Dtls_Sl_No),0)+1 FROM Trans_GRN_Details";
            iSlNo = db.Exec_Scalar(strQry);

            strQry = " INSERT INTO Trans_GRN_Details (Trans_Dtls_Sl_No,Trans_Sl_No,PCode,PDetails,UOM,Batch_No,POQTY,Price,Good,Damaged,Gross_Value,Net_Value,uom_name,mfgdate,remark,Offer_pro_code,Offer_pro_name,Cnv_qty,Off_pro_unit_name,Off_pro_unit_code,Tax,free,dis,dis_val) " +
                     " VALUES ('" + iSlNo + "','" + Trans_No + "','" + Pro_Code + "','" + Pro_Name + "','" + uom + "','" + batchno + "','" + qty + "','" + price + "','" + good + "','" + damage + "','" + groosval + "','" + netval + "','" + uom_name + "','" + mfgDate + "','" + remarks + "','" + Offer_pro_code + "','" + Offer_pro_name + "','" + con_factor + "','" + offer_pro_unit + "','0','" + Tax + "','" + free + "','" + dis + "','" + dis_val + "')";

            iReturn = db.ExecQry(strQry);
            iReturn = iSlNo;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return iReturn;
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

    //[WebMethod(EnableSession = true)]
    //public static string Get_AllValues(string grnNo, string grnDate, string grnSuppcode)
    //{
    //    string Div_Code = HttpContext.Current.Session["div_code"].ToString();
    //    DateTime dtgrn = DateTime.ParseExact(grnDate, "dd/MM/yyyy", null);
    //    string strgrn = dtgrn.ToString("MM-dd-yyyy");
    //    Product prd = new Product();
    //    MainTransGRN MTG = new MainTransGRN();
    //    Trans_Head THEA;
    //    Trans_Details TDET;
    //    Trans_Tax_Details TTDET;
    //    string tranId = string.Empty;
    //    DataSet dsGoods = null;
    //    dsGoods = prd.Get_GoodsReceived(Div_Code.TrimEnd(','), grnNo, strgrn, grnSuppcode);
    //    if (dsGoods.Tables[0].Rows.Count > 0)
    //    {
    //        DataSet dsDetails = null;
    //        dsDetails = prd.Get_GoodsReceived_Details1(Div_Code.TrimEnd(','), dsGoods.Tables[0].Rows[0][0].ToString(), strgrn, grnSuppcode);
    //        DataSet dsTax = null;
    //        dsTax = prd.Get_GoodsReceived_Tax_Details(Div_Code.TrimEnd(','), dsGoods.Tables[0].Rows[0][0].ToString());


    //        THEA = new Trans_Head();
    //        THEA.GRN_No = dsGoods.Tables[0].Rows[0]["GRN_No"].ToString();
    //        tranId = dsGoods.Tables[0].Rows[0]["Trans_Sl_No"].ToString();
    //        THEA.GRN_Date = Convert.ToDateTime(dsGoods.Tables[0].Rows[0]["GRN_Date"]).ToString("dd/MM/yyyy");
    //        THEA.Entry_Date = Convert.ToDateTime(dsGoods.Tables[0].Rows[0]["Entry_Date"]).ToString("dd/MM/yyyy");
    //        THEA.Supp_Code = dsGoods.Tables[0].Rows[0]["Supp_Code"].ToString();
    //        THEA.Supp_Name = dsGoods.Tables[0].Rows[0]["Supp_Name"].ToString();
    //        THEA.Po_No = dsGoods.Tables[0].Rows[0]["Po_No"].ToString();
    //        THEA.Challan_No = dsGoods.Tables[0].Rows[0]["Challan_No"].ToString();
    //        THEA.Dispatch_Date = Convert.ToDateTime(dsGoods.Tables[0].Rows[0]["Dispatch_Date"]).ToString("dd/MM/yyyy");
    //        THEA.Received_Location = dsGoods.Tables[0].Rows[0]["Received_Location"].ToString();
    //        THEA.Received_By = dsGoods.Tables[0].Rows[0]["Received_By"].ToString();
    //        THEA.Authorized_By = dsGoods.Tables[0].Rows[0]["Authorized_By"].ToString();
    //        THEA.remarks = dsGoods.Tables[0].Rows[0]["remarks"].ToString();
    //        MTG.TransH.Add(THEA);

    //        for (int i = 0; i < dsDetails.Tables[0].Rows.Count; i++)
    //        {
    //            DataRow drow = dsDetails.Tables[0].Rows[i];
    //            TDET = new Trans_Details();
    //            TDET.PCode = drow["PCode"].ToString();
    //            TDET.PDetails = drow["PDetails"].ToString();
    //            TDET.UOM_Name = drow["UOM_Name"].ToString();
    //            TDET.UOM = drow["UOM"].ToString();
    //            TDET.Batch_No = drow["Batch_No"].ToString();
    //            TDET.POQTY = drow["POQTY"].ToString();
    //            TDET.Price = drow["Price"].ToString();
    //            TDET.Good = drow["Good"].ToString();
    //            TDET.Remarks = drow["remark"].ToString();
    //            TDET.Damaged = drow["Damaged"].ToString();
    //            TDET.Gross_Value = drow["Gross_Value"].ToString();
    //            TDET.Net_Value = drow["Net_Value"].ToString();
    //            TDET.mfgDate = Convert.ToDateTime(drow["mfgdate"]).ToString("dd/MM/yyyy");
    //            TDET.Remarks= drow["remark"].ToString();
    //            TDET.Erp_Code = drow["Sample_Erp_Code"].ToString();
    //            MTG.TransD.Add(TDET);
    //            DataRow[] txtRows = null;
    //            txtRows = dsTax.Tables[0].Select("Trans_Dtls_Sl_No='" + drow["Trans_Dtls_Sl_No"].ToString() + "'");
    //            if (txtRows != null)
    //            {
    //                foreach (DataRow trow in txtRows)
    //                {
    //                    TTDET = new Trans_Tax_Details();
    //                    TTDET.Tax_Code = trow["Tax_Code"].ToString();
    //                    TTDET.Tax_Name = trow["Tax_Name"].ToString();
    //                    TTDET.Tax_Value = trow["Tax_Value"].ToString();
    //                    MTG.TransD[i].taxDtls.Add(TTDET);

    //                }
    //            }
    //        }


    //    }
    //    JSonHelper helper = new JSonHelper();
    //    String jsonResult = helper.ConvertObjectToJSon(MTG);
    //    return jsonResult;
    //}

    [WebMethod]
    public static List<string> GetPONumber(string suppCode)
    {
        List<string> Emp = new List<string>();

        Expense eps = new Expense();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsPO = eps.GetPONumbers(Div_code.TrimEnd(','), suppCode);

        foreach (DataRow ro in dsPO.Tables[0].Rows)
        {
            Emp.Add(ro["Order_No"].ToString());
        }

        return Emp;
    }

    [WebMethod(EnableSession = true)]
    public static List<POQTYS> GetPOQTY(string data)
    {

        Expense eps = new Expense();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsPO = eps.GetPOQTYValues(Div_code.TrimEnd(','), data);

        List<POQTYS> pq = new List<POQTYS>();
        foreach (DataRow ro in dsPO.Tables[0].Rows)
        {
            POQTYS p = new POQTYS();
            p.Product_Code = ro["Product_Code"].ToString();
            p.CQty = ro["CQty"].ToString();
            p.PQty = ro["PQty"].ToString();
            p.value = ro["value"].ToString();
            pq.Add(p);
        }

        return pq;
    }


    public class POQTYS
    {
        public string Product_Code { get; set; }
        public string CQty { get; set; }
        public string PQty { get; set; }
        public string value { get; set; }
    }


    [WebMethod(EnableSession = true)]
    public static string getratenew(string Div_Code, string Stockist_Code)
    {
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        string State = HttpContext.Current.Session["State"].ToString();
        ds = sm.get_rate_new_bystk(Div_Code.TrimEnd(','), Stockist_Code, State);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
	
	[WebMethod(EnableSession = true)]
    public static string getscheme(string date, string Div_Code, string Stockist_Code)
    {
        ds = sm.getschemebystk(Stockist_Code, Div_Code.TrimEnd(','), date);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string SavePurchaseOrder(string HeadData, string DetailsData, string TaxData , string Pono_No)
    {
        DataSet ds = new DataSet();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Exp_date = string.Empty;
        StockistMaster sm = new StockistMaster();
        string Trans_Sl_No = string.Empty;
        using (var scope = new TransactionScope())
        {
            try
            {
                IList<Head> general_details = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Head>>(HeadData);
                IList<Purchase_Order> Product_details = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Purchase_Order>>(DetailsData);
                //IList<Tax_details> Tax_items = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Tax_details>>(TaxData);

                string Head = "<ROOT>";
                for (int i = 0; i < general_details.Count; i++)
                {
                    Head += "<Head Sf_Code=\"" + general_details[i].sf_code + "\" Stk_Code=\"" + general_details[i].stk_code + "\" Order_Date=\"" + general_details[i].order_date + "\"  Order_Value=\"" + general_details[i].Gross_tot + "\"  Div_Code=\"" + general_details[i].div_code + "\"  Sup_No=\"" + general_details[i].sup_no + "\"  Sup_Name=\"" + general_details[i].sup_name + "\" Bill_Add=\"" + general_details[i].bill_add + "\" Ship_Add=\"" + general_details[i].ship_add + "\"  Exp_Date=\"" + general_details[i].exp_date + "\" Com_Add=\"" + general_details[i].com_add + "\" Sub_Total=\"" + general_details[i].sub_tot + "\" Tax_Total=\"" + general_details[i].Tax_Total + "\" Dis_Total=\"" + general_details[i].dis_tot + "\" Pono_No=\"" + Pono_No + "\"   />";
                }
                Head += "</ROOT>";

                string Details = "<ROOT>";
                for (int i = 0; i < Product_details.Count; i++)
                {
                    Details += "<Prod PCode=\"" + Product_details[i].PCd + "\" PName=\"" + Product_details[i].PName + "\" Qty=\"" + Product_details[i].Qty_c + "\"  Rate=\"" + Product_details[i].Rate + "\" Free=\"" + Product_details[i].free + "\"  Dis=\"" + Product_details[i].dis + "\"  Dis_value=\"" + Product_details[i].dis_value + "\" Off_Pro_Code=\"" + Product_details[i].Off_Pro_code + "\" Off_Pro_Name=\"" + Product_details[i].Off_Pro_name + "\" Off_Pro_Unit=\"" + Product_details[i].Off_Pro_Unit + "\" Productunitcode=\"" + Product_details[i].umo_unit + "\" tax=\"" + Product_details[i].Tax_value + "\" con_fac=\"" + Product_details[i].con_fac + "\" />";
                }
                Details += "</ROOT>";

                string Tax_Details = "<ROOT>";
                //for (int k = 0; k < Tax_items.Count; k++)
                //{
                //    Tax_Details += "<Tax_details pro_code=\"" + Tax_items[k].pro_code + "\" Tax_Code=\"" + Tax_items[k].Tax_Code + "\" Tax_Amt=\"" + Tax_items[k].Tax_Amt + "\" Tax_Name=\"" + Tax_items[k].Tax_Name + "\" Tax_Per=\"" + Tax_items[k].Tax_Per + "\" umo_code=\"" + Tax_items[k].umo_code + "\"  />";
                //}
                Tax_Details += "</ROOT>";

                ds = Save_Primary(Stockist_Code, div_code, Head, Details, Tax_Details, sf_code, Pono_No);
                Trans_Sl_No = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                scope.Complete();
                scope.Dispose();
            }
            catch (Exception ex)
            {
                scope.Dispose();
                throw ex;
            }
        }
        return Trans_Sl_No;
    }
    public static DataSet Save_Primary(string Stockist_Code, string div_code, string Head, string Details, string Tax_Details, string sf_code ,string pono_no)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = "Exec [svPending_PrimaryOrder] '" + Stockist_Code + "','" + div_code + "','" + Head + "','" + Details + "','" + Tax_Details + "','" + sf_code + "','"+pono_no+"'";

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
    public class Head
    {
        public string sf_code { get; set; }
        public string stk_code { get; set; }
        public string order_date { get; set; }
        public string Gross_tot { get; set; }
        public string sub_tot { get; set; }
        public string dis_tot { get; set; }
        public string Tax_Total { get; set; }
        public string div_code { get; set; }
        public string sup_no { get; set; }
        public string sup_name { get; set; }
        public string bill_add { get; set; }
        public string ship_add { get; set; }
        public string exp_date { get; set; }
        public string com_add { get; set; }
    }

    public class Purchase_Order
    {
        public string PCd { get; set; }
        public string PName { get; set; }
        public string Qty_c { get; set; }
        public string Rate { get; set; }
        public string free { get; set; }
        public string dis { get; set; }
        public string dis_value { get; set; }
        public string Off_Pro_name { get; set; }
        public string Off_Pro_code { get; set; }
        public string Off_Pro_Unit { get; set; }
        public string umo_unit { get; set; }
        public string Tax_value { get; set; }
        public string con_fac { get; set; }
    }
}