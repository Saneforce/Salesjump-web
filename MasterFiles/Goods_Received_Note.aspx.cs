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

public partial class MasterFiles_Goods_Received_Note : System.Web.UI.Page
{
    string Div_Code = string.Empty;
    string SF_Code = string.Empty;
    string Sub_DivCode = string.Empty;
    string mode = string.Empty;
    string grn_no = string.Empty;
    string grn_dt = string.Empty;
    string supp_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            mode = Request.QueryString["Mode"].ToString();
            hdnmode.Value = mode;
            if (mode == "1")
            {
                grn_no = Request.QueryString["GRN_No"].ToString();
                hdngrn_no.Value = grn_no;
                grn_dt = Request.QueryString["GRN_Date"].ToString();
                hdngrn_date.Value = grn_dt;
                supp_code = Request.QueryString["Supp_Code"].ToString();
                hdnsupp_code.Value = supp_code;
            }
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
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string DSub_DivCode = "0";
        List<Distributor> distributor = new List<Distributor>();
        DataSet dsDistributor = null;
        Stockist stk = new Stockist();
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
    public class Supplierss
    {
        public string disName { get; set; }
        public string disCode { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static Supplierss[] GetSupplier()
    {
        string DDiv_code = HttpContext.Current.Session["div_code"].ToString();
        string DSf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string DSub_DivCode = string.Empty;
        List<Supplierss> distributor = new List<Supplierss>();
        DataSet dsDistributor = null;
        Stockist stk = new Stockist();
        dsDistributor = stk.GetSuppliers(DDiv_code.TrimEnd(','));
        if (dsDistributor.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in dsDistributor.Tables[0].Rows)
            {
                Supplierss dis = new Supplierss();
                dis.disCode = row["S_No"].ToString();
                dis.disName = row["S_Name"].ToString();
                distributor.Add(dis);
            }
        }
        return distributor.ToArray();
    }




    public class Products
    {
        public string pName { get; set; }
        public string pCode { get; set; }
        public string pUOM { get; set; }
        public string pUOM_Name { get; set; }
        public string Erp_Code { get; set; }
    }


    [WebMethod(EnableSession = true)]
    public static Products[] GetProduct()
    {
        string PDiv_code = HttpContext.Current.Session["div_code"].ToString();
        List<Products> product = new List<Products>();
        DataSet dsProduct = null;
        Product p = new Product();

        dsProduct = p.getproductname(PDiv_code.TrimEnd(','));
        if (dsProduct != null)
        {
            foreach (DataRow row in dsProduct.Tables[0].Rows)
            {
                Products pro = new Products();
                pro.pName = row["Product_Detail_Name"].ToString();
                pro.pCode = row["Product_Detail_Code"].ToString();
                pro.pUOM = row["Unit_code"].ToString();
                pro.pUOM_Name = row["Move_MailFolder_Name"].ToString();
                pro.Erp_Code = row["Sample_Erp_Code"].ToString(); 
                //pro.pUMO = row["Stockist_Name"].ToString();
                product.Add(pro);
            }
        }
        return product.ToArray();
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
        Tax_mas = dc.view_tax_Mas(Div_code);
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



    public class MainTransGRN
    {
        public List<Trans_Head> TransH = new List<Trans_Head>();
        public List<Trans_Details> TransD = new List<Trans_Details>();
    }


    public class Trans_Head
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

    public class Trans_Details
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
        public List<Trans_Tax_Details> taxDtls { get; set; }
        public Trans_Details()
        {
            taxDtls = new List<Trans_Tax_Details>(0);
        }
    }
    public class Trans_Tax_Details
    {
        public string Tax_Code { get; set; }
        public string Tax_Name { get; set; }
        public string Tax_Value { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static string SaveDate(string data, string SaveUpdate)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string SF_Code = HttpContext.Current.Session["sf_code"].ToString();
        string Sub_DivCode = "0";//HttpContext.Current.Session["subdivision_code"].ToString();
        MainTransGRN GRN = new MainTransGRN();
        JSonHelper helper = new JSonHelper();
        GRN = helper.ConverJSonToObject<MainTransGRN>(data);
        string msg = string.Empty;
        int iReturn = -1;
        Product prd = new Product();
        using (var scope = new TransactionScope())
        {
            try
            {
                DateTime dtgrn = DateTime.ParseExact(GRN.TransH[0].GRN_Date, "dd/MM/yyyy", null);
                string strgrn = dtgrn.ToString("MM-dd-yyyy");

                DateTime dtentry = DateTime.ParseExact(GRN.TransH[0].Entry_Date, "dd/MM/yyyy", null);
                string strentry = dtentry.ToString("MM-dd-yyyy");

                DateTime dtdispatch = DateTime.ParseExact(GRN.TransH[0].Dispatch_Date, "dd/MM/yyyy", null);
                string strdispatch = dtdispatch.ToString("MM-dd-yyyy");


                DataSet dsGoods = null;
                if (SaveUpdate == "1")
                {
                    dsGoods = prd.Get_GoodsReceived(Div_Code, GRN.TransH[0].GRN_No.ToString(), strgrn, GRN.TransH[0].Supp_Code.ToString());
                }
                else
                {
                    dsGoods = prd.Get_GoodsReceived_withoutNo(Div_Code, strgrn, GRN.TransH[0].Supp_Code.ToString(),GRN.TransH[0].Received_Location.ToString());
                    if (dsGoods.Tables[0].Rows.Count > 0)
                    {
                        scope.Dispose();
                        msg = "Record  Already Exist Can't Add..!!";
                        return msg;
                    }
                }

                if (dsGoods.Tables[0].Rows.Count <= 0)
                {
                    int Trans_No = prd.Insert_GoodsReceived(Div_Code, SF_Code, GRN.TransH[0].GRN_No.ToString(), strgrn, GRN.TransH[0].Supp_Code.ToString(), GRN.TransH[0].Supp_Name.ToString(), GRN.TransH[0].Challan_No.ToString(), GRN.TransH[0].Po_No.ToString(), strentry, strdispatch, GRN.TransH[0].Received_Location.ToString(), GRN.TransH[0].Received_By.ToString(), GRN.TransH[0].Authorized_By.ToString(), Sub_DivCode, GRN.TransH[0].remarks.ToString(), GRN.TransH[0].goodsTot.ToString(), GRN.TransH[0].taxTot.ToString(), GRN.TransH[0].netTot.ToString(), GRN.TransH[0].Received_Name.ToString());

                    for (int i = 0; i < GRN.TransD.Count; i++)
                    {
                       decimal poQty = GRN.TransD[i].POQTY == "" ? 0 : Convert.ToDecimal(GRN.TransD[i].POQTY);
                        if ( poQty> 0)
                        {

                            DateTime mfgdt = DateTime.ParseExact(GRN.TransD[i].mfgDate, "dd/MM/yyyy", null);
                            string strmfgdt = mfgdt.ToString("MM-dd-yyyy");

                            //int Dtls_No = prd.Insert_GoodsReceived_Details(Trans_No.ToString(), GRN.TransD[i].PCode.ToString(), GRN.TransD[i].PDetails.ToString(), GRN.TransD[i].UOM.ToString(), GRN.TransD[i].Batch_No.ToString(), GRN.TransD[i].POQTY.ToString(), GRN.TransD[i].Price.ToString(), GRN.TransD[i].Good.ToString(), GRN.TransD[i].Damaged.ToString(), GRN.TransD[i].Gross_Value.ToString(), GRN.TransD[i].Net_Value.ToString(), GRN.TransD[i].UOM_Name.ToString(), strmfgdt);


                            DataSet dsLedger = null;
                            DataSet dsCurrStock = null;

                            dsLedger = prd.Select_Trans_Stock_Ledger(Div_Code, GRN.TransH[0].Received_Location.ToString(), GRN.TransD[i].PCode.ToString(), strgrn, GRN.TransD[i].Batch_No.ToString());
                            if (dsLedger.Tables[0].Rows.Count <= 0)
                            {
                                int num = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].Received_Location.ToString(), GRN.TransD[i].PCode.ToString(), GRN.TransD[i].Batch_No.ToString(),(Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal(GRN.TransD[i].Good)).ToString(),(Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal(GRN.TransD[i].Damaged)).ToString(), "1", "", ("GRN" + Trans_No), GRN.TransH[0].Supp_Name.ToString(), GRN.TransH[0].Received_Name.ToString(), "GRN");
                            }
                            else
                            {
                                string ldrid = dsLedger.Tables[0].Rows[0][0].ToString();
                                int num = prd.Update_Trans_Stock_Ledger(Div_Code, ldrid, GRN.TransH[0].Received_Location.ToString(), GRN.TransD[i].PCode.ToString(), strgrn,(Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal(GRN.TransD[i].SGood)).ToString(),(Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal(GRN.TransD[i].SDamaged)).ToString(), GRN.TransD[i].Batch_No.ToString(), (Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal(GRN.TransD[i].Good)).ToString(),(Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal(GRN.TransD[i].Damaged)).ToString());
                            }

                            dsCurrStock = prd.Select_Trans_CurrStock_Batchwise(Div_Code, GRN.TransH[0].Received_Location.ToString(), GRN.TransD[i].PCode.ToString(), GRN.TransD[i].Batch_No.ToString());

                            if (dsCurrStock.Tables[0].Rows.Count <= 0)
                            {
                                int num = prd.Insert_Trans_CurrStock_Batchwise(Div_Code, GRN.TransH[0].Received_Location.ToString(), GRN.TransD[i].PCode.ToString(), GRN.TransD[i].Batch_No.ToString(),(Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal(GRN.TransD[i].Good)).ToString(),(Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal(GRN.TransD[i].Damaged)).ToString());
                            }
                            else
                            {
                                string slno = dsCurrStock.Tables[0].Rows[0][0].ToString();
                                int num = prd.Update_Trans_CurrStock_Batchwise(Div_Code, slno, GRN.TransH[0].Received_Location.ToString(), GRN.TransD[i].PCode.ToString(), GRN.TransD[i].Batch_No.ToString(), (Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal(GRN.TransD[i].SGood)).ToString(),(Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal(GRN.TransD[i].SDamaged)).ToString(), (Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal( GRN.TransD[i].Good)).ToString(),(Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal( GRN.TransD[i].Damaged)).ToString());
                            }


                            for (int j = 0; j < GRN.TransD[i].taxDtls.Count; j++)
                            {
                                if (GRN.TransD[i].taxDtls[j].Tax_Code.ToString() != "0")
                                {
                                   // iReturn = prd.Insert_GoodsReceived_Tax(Dtls_No.ToString(), Trans_No.ToString(), GRN.TransD[i].taxDtls[j].Tax_Code.ToString(), GRN.TransD[i].taxDtls[j].Tax_Name.ToString(), GRN.TransD[i].taxDtls[j].Tax_Value.ToString());
                                }
                            }
                        }
                    }
                }
                else
                {
                    string tran_id = dsGoods.Tables[0].Rows[0][0].ToString();
                    int Trans_No = prd.Update_GoodsReceived(Div_Code, SF_Code, GRN.TransH[0].GRN_No.ToString(), strgrn, GRN.TransH[0].Supp_Code.ToString(), GRN.TransH[0].Supp_Name.ToString(), GRN.TransH[0].Challan_No.ToString(), GRN.TransH[0].Po_No.ToString(), strentry, strdispatch, GRN.TransH[0].Received_Location.ToString(), GRN.TransH[0].Received_By.ToString(), GRN.TransH[0].Authorized_By.ToString(), Sub_DivCode, tran_id, GRN.TransH[0].remarks.ToString(), GRN.TransH[0].goodsTot.ToString(), GRN.TransH[0].taxTot.ToString(), GRN.TransH[0].netTot.ToString(), GRN.TransH[0].Received_Name.ToString());

                    iReturn = prd.Delete_GoodsReceived_Tax(tran_id);
                    iReturn = prd.Delect_GoodsReceived_Details(tran_id);


                    DataSet dsUpdateLedger = null;


                    dsUpdateLedger = prd.Select_Stock_Ledger(Div_Code, GRN.TransH[0].GRN_No.ToString());
                    foreach (DataRow ul in dsUpdateLedger.Tables[0].Rows)
                    {
                        int num = prd.Update_Trans_CurrStock_Batchwise(Div_Code, "0", ul["Dist_Code"].ToString(), ul["Prod_Code"].ToString(), ul["BatchNo"].ToString(), ul["GStock"].ToString(), ul["DStock"].ToString(), "0", "0");
                    }

                    iReturn = prd.Delect_Stock_Ladger(Div_Code, GRN.TransH[0].GRN_No.ToString());

                    for (int i = 0; i < GRN.TransD.Count; i++)
                    {
                        decimal poQty = GRN.TransD[i].POQTY == "" ? 0 : Convert.ToDecimal(GRN.TransD[i].POQTY);
                        if ( poQty> 0)
                        {
                            DateTime mfgdt = DateTime.ParseExact(GRN.TransD[i].mfgDate, "dd/MM/yyyy", null);
                            string strmfgdt = mfgdt.ToString("MM-dd-yyyy");

                            //int Dtls_No = prd.Insert_GoodsReceived_Details(tran_id, GRN.TransD[i].PCode.ToString(), GRN.TransD[i].PDetails.ToString(), GRN.TransD[i].UOM.ToString(), GRN.TransD[i].Batch_No.ToString(), GRN.TransD[i].POQTY.ToString(), GRN.TransD[i].Price.ToString(), GRN.TransD[i].Good.ToString(), GRN.TransD[i].Damaged.ToString(), GRN.TransD[i].Gross_Value.ToString(), GRN.TransD[i].Net_Value.ToString(), GRN.TransD[i].UOM_Name.ToString(), strmfgdt);

                            // DataSet dsLedger = null;
                            DataSet dsCurrStock = null;

                            //dsLedger = prd.Select_Trans_Stock_Ledger(Div_Code, GRN.TransH[0].Received_Location.ToString(), GRN.TransD[i].PCode.ToString(), strgrn, GRN.TransD[i].Batch_No.ToString());
                            //if (dsLedger.Tables[0].Rows.Count <= 0)
                            //{
                            int num_led = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].Received_Location.ToString(), GRN.TransD[i].PCode.ToString(), GRN.TransD[i].Batch_No.ToString(),(Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal( GRN.TransD[i].Good)).ToString(),(Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal( GRN.TransD[i].Damaged)).ToString(), "1", "", ("GRN" + tran_id),GRN.TransH[0].Supp_Name.ToString(),GRN.TransH[0].Received_Name.ToString(),"GRN");
                            //}
                            //else
                            //{
                            //    string ldrid = dsLedger.Tables[0].Rows[0][0].ToString();
                            //    int num = prd.Update_Trans_Stock_Ledger(Div_Code, ldrid, GRN.TransH[0].Received_Location.ToString(), GRN.TransD[i].PCode.ToString(), strgrn, GRN.TransD[i].SGood.ToString(), GRN.TransD[i].SDamaged.ToString(), GRN.TransD[i].Batch_No.ToString(), GRN.TransD[i].Good.ToString(), GRN.TransD[i].Damaged.ToString());
                            //}

                            dsCurrStock = prd.Select_Trans_CurrStock_Batchwise(Div_Code, GRN.TransH[0].Received_Location.ToString(), GRN.TransD[i].PCode.ToString(), GRN.TransD[i].Batch_No.ToString());

                            if (dsCurrStock.Tables[0].Rows.Count <= 0)
                            {
                                int num = prd.Insert_Trans_CurrStock_Batchwise(Div_Code, GRN.TransH[0].Received_Location.ToString(), GRN.TransD[i].PCode.ToString(), GRN.TransD[i].Batch_No.ToString(), (Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal( GRN.TransD[i].Good)).ToString(), (Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal(GRN.TransD[i].Damaged)).ToString());
                            }
                            else
                            {
                                string slno = dsCurrStock.Tables[0].Rows[0][0].ToString();
                                int num = prd.Update_Trans_CurrStock_Batchwise(Div_Code, slno, GRN.TransH[0].Received_Location.ToString(), GRN.TransD[i].PCode.ToString(), GRN.TransD[i].Batch_No.ToString(), "0", "0", (Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal(GRN.TransD[i].Good)).ToString(), (Convert.ToDecimal(GRN.TransD[i].Erp_Code) * Convert.ToDecimal(GRN.TransD[i].Damaged)).ToString());
                            }

                            for (int j = 0; j < GRN.TransD[i].taxDtls.Count; j++)
                            {
                                if (GRN.TransD[i].taxDtls[j].Tax_Code.ToString() != "0")
                                {
                                    //iReturn = prd.Insert_GoodsReceived_Tax(Dtls_No.ToString(), tran_id, GRN.TransD[i].taxDtls[j].Tax_Code.ToString(), GRN.TransD[i].taxDtls[j].Tax_Name.ToString(), GRN.TransD[i].taxDtls[j].Tax_Value.ToString());
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
                msg = ex.Message;
                scope.Dispose();
            }
            finally
            {
                scope.Dispose();
            }
        }

        return msg;
    }

    [WebMethod(EnableSession = true)]
    public static string Get_AllValues(string grnNo, string grnDate, string grnSuppcode)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        DateTime dtgrn = DateTime.ParseExact(grnDate, "dd/MM/yyyy", null);
        string strgrn = dtgrn.ToString("MM-dd-yyyy");
        Product prd = new Product();
        MainTransGRN MTG = new MainTransGRN();
        Trans_Head THEA;
        Trans_Details TDET;
        Trans_Tax_Details TTDET;
        string tranId = string.Empty;
        DataSet dsGoods = null;
        dsGoods = prd.Get_GoodsReceived(Div_Code, grnNo, strgrn, grnSuppcode);
        if (dsGoods.Tables[0].Rows.Count > 0)
        {
            DataSet dsDetails = null;
            dsDetails = prd.Get_GoodsReceived_Details(Div_Code, dsGoods.Tables[0].Rows[0][0].ToString());
            DataSet dsTax = null;
            dsTax = prd.Get_GoodsReceived_Tax_Details(Div_Code, dsGoods.Tables[0].Rows[0][0].ToString());


            THEA = new Trans_Head();
            THEA.GRN_No = dsGoods.Tables[0].Rows[0]["GRN_No"].ToString();
            tranId = dsGoods.Tables[0].Rows[0]["Trans_Sl_No"].ToString();
            THEA.GRN_Date = Convert.ToDateTime(dsGoods.Tables[0].Rows[0]["GRN_Date"]).ToString("dd/MM/yyyy");
            THEA.Entry_Date = Convert.ToDateTime(dsGoods.Tables[0].Rows[0]["Entry_Date"]).ToString("dd/MM/yyyy");
            THEA.Supp_Code = dsGoods.Tables[0].Rows[0]["Supp_Code"].ToString();
            THEA.Supp_Name = dsGoods.Tables[0].Rows[0]["Supp_Name"].ToString();
            THEA.Po_No = dsGoods.Tables[0].Rows[0]["Po_No"].ToString();
            THEA.Challan_No = dsGoods.Tables[0].Rows[0]["Challan_No"].ToString();
            THEA.Dispatch_Date = Convert.ToDateTime(dsGoods.Tables[0].Rows[0]["Dispatch_Date"]).ToString("dd/MM/yyyy");
            THEA.Received_Location = dsGoods.Tables[0].Rows[0]["Received_Location"].ToString();
            THEA.Received_By = dsGoods.Tables[0].Rows[0]["Received_By"].ToString();
            THEA.Authorized_By = dsGoods.Tables[0].Rows[0]["Authorized_By"].ToString();
            THEA.remarks = dsGoods.Tables[0].Rows[0]["remarks"].ToString();
            MTG.TransH.Add(THEA);

            for (int i = 0; i < dsDetails.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dsDetails.Tables[0].Rows[i];
                TDET = new Trans_Details();
                TDET.PCode = drow["PCode"].ToString();
                TDET.PDetails = drow["PDetails"].ToString();
                TDET.UOM = drow["UOM"].ToString();
                TDET.Batch_No = drow["Batch_No"].ToString();
                TDET.POQTY = drow["POQTY"].ToString();
                TDET.Price = drow["Price"].ToString();
                TDET.Good = drow["Good"].ToString();
                TDET.Damaged = drow["Damaged"].ToString();
                TDET.Gross_Value = drow["Gross_Value"].ToString();
                TDET.Net_Value = drow["Net_Value"].ToString();
                TDET.mfgDate = Convert.ToDateTime(drow["mfgdate"]).ToString("dd/MM/yyyy");
                MTG.TransD.Add(TDET);
                DataRow[] txtRows = null;
                txtRows = dsTax.Tables[0].Select("Trans_Dtls_Sl_No='" + drow["Trans_Dtls_Sl_No"].ToString() + "'");
                if (txtRows != null)
                {
                    foreach (DataRow trow in txtRows)
                    {
                        TTDET = new Trans_Tax_Details();
                        TTDET.Tax_Code = trow["Tax_Code"].ToString();
                        TTDET.Tax_Name = trow["Tax_Name"].ToString();
                        TTDET.Tax_Value = trow["Tax_Value"].ToString();
                        MTG.TransD[i].taxDtls.Add(TTDET);

                    }
                }
            }


        }
        JSonHelper helper = new JSonHelper();
        String jsonResult = helper.ConvertObjectToJSon(MTG);
        return jsonResult;
    }

    [WebMethod]
    public static List<string> GetPONumber(string suppCode)
    {
        List<string> Emp = new List<string>();

        Expense eps = new Expense();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        DataSet dsPO = eps.GetPONumbers(Div_code, suppCode);

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
        DataSet dsPO = eps.GetPOQTYValues(Div_code, data);

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


}