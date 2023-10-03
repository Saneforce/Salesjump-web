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


public partial class MasterFiles_Stock_Transfer : System.Web.UI.Page
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
                grn_no = Request.QueryString["Code"].ToString();
                hdntransno.Value = grn_no;
            }
        }

    }

    public class Distributor
    {
        public string disName { get; set; }
        public string disCode { get; set; }
        public string wType { get; set; }
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
                dis.wType = row["type"].ToString();
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
                //pro.pUMO = row["Stockist_Name"].ToString();
                product.Add(pro);
            }
        }
        return product.ToArray();
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


    [WebMethod(EnableSession = true)]
    public static string SaveDate(string data)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        string SF_Code = HttpContext.Current.Session["sf_code"].ToString();

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
                DateTime dtgrn = DateTime.ParseExact(GRN.TransH[0].transDate, "dd/MM/yyyy", null);
                string strgrn = dtgrn.ToString("MM-dd-yyyy");


                DataSet dsStock = null;
                dsStock = prd.Get_Stock_Transfer_Head(Div_Code, GRN.TransH[0].transNo.ToString(), strgrn, GRN.TransH[0].stockistFrom.ToString(), GRN.TransH[0].stockistTo.ToString());

                if (dsStock.Tables[0].Rows.Count <= 0)
                {
                    string Trans_No = prd.Insert_Stock_Transfer_Head(Div_Code, SF_Code, GRN.TransH[0].transNo.ToString(), strgrn, GRN.TransH[0].stockistFrom.ToString(), GRN.TransH[0].stockistFrom_Nm.ToString(), GRN.TransH[0].stockistTo.ToString(), GRN.TransH[0].stockistTo_Nm.ToString());
                    for (int i = 0; i < GRN.TransD.Count; i++)
                    {
                        string val = GRN.TransD[i].pqty == "" ? "0" : GRN.TransD[i].pqty;
                        if (Convert.ToDecimal(val) > 0)
                        {
                            string irtn = prd.Insert_Stock_Transfer_Details(Trans_No, GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pName.ToString(), GRN.TransD[i].pType.ToString(), GRN.TransD[i].pType_Name.ToString(), GRN.TransD[i].pqty.ToString(), GRN.TransD[i].preason.ToString());

                            DataSet dsCurSkt = prd.Select_Trans_CurrStock_Distwise(Div_Code, GRN.TransH[0].stockistFrom.ToString(), GRN.TransD[i].pCode.ToString());

                            if (dsCurSkt.Tables[0].Rows.Count > 0)
                            {
                                decimal entry_Stock = Convert.ToDecimal(GRN.TransD[i].pqty.ToString());
                                foreach (DataRow row in dsCurSkt.Tables[0].Rows)
                                {
                                    if (GRN.TransD[i].pType_Name.ToString() == "Good")
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

                                        int num = prd.Update_Trans_CurrStock_Batchwise_Transfer(Div_Code, GRN.TransH[0].stockistFrom.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "Good", GRN.TransD[i].oqty.ToString(), "-");


                                        int ledIns = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stockistFrom.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "0", "", "", Trans_No,"","","TRS");


                                        DataSet dsSkt = prd.Select_Trans_CurrStock_Distwise(Div_Code, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString());
                                        if (dsSkt.Tables[0].Rows.Count > 0)
                                        {
                                            int numup = prd.Update_Trans_CurrStock_Batchwise_Transfer(Div_Code, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "Good", GRN.TransD[i].oqty.ToString(), "+");
                                            int ledInsIn = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "0", "1", "", Trans_No, "", "", "TRS");

                                        }
                                        else
                                        {
                                            int numup = prd.Insert_Trans_CurrStock_Batchwise(Div_Code, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "0");
                                            int ledInsIn = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "0", "1", "", Trans_No, "", "", "TRS");
                                        }
                                    }
                                    else
                                    {
                                        decimal tot = Convert.ToDecimal(row["DStock"].ToString()) - entry_Stock;
                                        decimal upVal = 0;
                                        if (tot >= 0)
                                        {
                                            upVal = entry_Stock;
                                            entry_Stock = 0;
                                        }
                                        else
                                        {
                                            upVal = Convert.ToDecimal(row["DStock"].ToString());
                                            entry_Stock = Math.Abs(tot);
                                        }
                                        int num = prd.Update_Trans_CurrStock_Batchwise_Transfer(Div_Code, GRN.TransH[0].stockistFrom.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "Damage", GRN.TransD[i].oqty.ToString(), "-");

                                        int ledIns = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stockistFrom.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), "0", upVal.ToString("0.00"), "0", "", Trans_No, "", "", "TRS");

                                        DataSet dsSkt = prd.Select_Trans_CurrStock_Distwise(Div_Code, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString());
                                        if (dsSkt.Tables[0].Rows.Count > 0)
                                        {
                                            int numup = prd.Update_Trans_CurrStock_Batchwise_Transfer(Div_Code, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "Damage", GRN.TransD[i].oqty.ToString(), "+");
                                            int ledInsIn = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), "0", upVal.ToString("0.00"), "1", "", Trans_No, "", "", "TRS");
                                        }
                                        else
                                        {
                                            int numup = prd.Insert_Trans_CurrStock_Batchwise(Div_Code, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), "0", upVal.ToString("0.00"));
                                            int ledInsIn = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), "0", upVal.ToString("0.00"), "1", "", Trans_No, "", "", "TRS");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                msg = "Stock Low Can't Update";
                                scope.Dispose();
                            }

                        }
                    }
                }
                else
                {
                    string tran_id = dsStock.Tables[0].Rows[0][0].ToString();
                    string Trans_No = prd.Update_Stock_Transfer_Head(tran_id, GRN.TransH[0].stockistFrom.ToString(), GRN.TransH[0].stockistTo.ToString());
                    iReturn = prd.Delete_Stock_Transfer_Details(tran_id);

                    DataSet dsUpdateLedger = null;

                    dsUpdateLedger = prd.Select_Stock_Ledger(Div_Code, tran_id);
                    foreach (DataRow ul in dsUpdateLedger.Tables[0].Rows)
                    {
                        int num = 0;
                        if( ul["CalType"].ToString()=="1")
                        {
                            if( Convert.ToDecimal(ul["GStock"])>0)
                            {
                                num = prd.Update_Trans_CurrStock_Batchwise_Transfer(Div_Code, ul["Dist_Code"].ToString(), ul["Prod_Code"].ToString(),  ul["BatchNo"].ToString(), ul["GStock"].ToString(),"Good" , "0", "-");
                            }
                            else
                            {
                                num = prd.Update_Trans_CurrStock_Batchwise_Transfer(Div_Code, ul["Dist_Code"].ToString(), ul["Prod_Code"].ToString(), ul["BatchNo"].ToString(), ul["DStock"].ToString(), "Damage", "0", "-");
                            }
                        }
                        else
                        {
                            if (Convert.ToDecimal(ul["GStock"]) > 0)
                            {
                                num = prd.Update_Trans_CurrStock_Batchwise_Transfer(Div_Code, ul["Dist_Code"].ToString(), ul["Prod_Code"].ToString(), ul["BatchNo"].ToString(), ul["GStock"].ToString(), "Good", "0", "+");
                            }
                            else
                            {
                                num = prd.Update_Trans_CurrStock_Batchwise_Transfer(Div_Code, ul["Dist_Code"].ToString(), ul["Prod_Code"].ToString(), ul["BatchNo"].ToString(), ul["DStock"].ToString(), "Damage", "0", "+");
                            }
                        }
                    }
                    iReturn = prd.Delect_Stock_Ladger(Div_Code, tran_id);

                    for (int i = 0; i < GRN.TransD.Count; i++)
                    {
                        string val = GRN.TransD[i].pqty == "" ? "0" : GRN.TransD[i].pqty;
                        if (Convert.ToDecimal(val) > 0)
                        {
                            string irtn = prd.Insert_Stock_Transfer_Details(tran_id, GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pName.ToString(), GRN.TransD[i].pType.ToString(), GRN.TransD[i].pType_Name.ToString(), GRN.TransD[i].pqty.ToString(), GRN.TransD[i].preason.ToString());


                            DataSet dsCurSkt = prd.Select_Trans_CurrStock_Distwise(Div_Code, GRN.TransH[0].stockistFrom.ToString(), GRN.TransD[i].pCode.ToString());

                            if (dsCurSkt.Tables[0].Rows.Count > 0)
                            {
                                decimal entry_Stock = Convert.ToDecimal(GRN.TransD[i].pqty.ToString());
                                foreach (DataRow row in dsCurSkt.Tables[0].Rows)
                                {
                                    if (GRN.TransD[i].pType_Name.ToString() == "Good")
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

                                        int num = prd.Update_Trans_CurrStock_Batchwise_Transfer(Div_Code, GRN.TransH[0].stockistFrom.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "Good", "0", "-");
                                        
                                        int ledIns = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stockistFrom.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "0", "0", "", tran_id, "", "", "TRS");


                                        DataSet dsSkt = prd.Select_Trans_CurrStock_Distwise(Div_Code, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString());
                                        if (dsSkt.Tables[0].Rows.Count > 0)
                                        {
                                            int numup = prd.Update_Trans_CurrStock_Batchwise_Transfer(Div_Code, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "Good", "0", "+");
                                            int ledInsIn = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "0", "1", "", tran_id, "", "", "TRS");

                                        }
                                        else
                                        {
                                            int numup = prd.Insert_Trans_CurrStock_Batchwise(Div_Code, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "0");
                                            int ledInsIn = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "0", "1", "", tran_id, "", "", "TRS");
                                        }
                                    }
                                    else
                                    {
                                        decimal tot = Convert.ToDecimal(row["DStock"].ToString()) - entry_Stock;
                                        decimal upVal = 0;
                                        if (tot >= 0)
                                        {
                                            upVal = entry_Stock;
                                            entry_Stock = 0;
                                        }
                                        else
                                        {
                                            upVal = Convert.ToDecimal(row["DStock"].ToString());
                                            entry_Stock = Math.Abs(tot);
                                        }
                                        int num = prd.Update_Trans_CurrStock_Batchwise_Transfer(Div_Code, GRN.TransH[0].stockistFrom.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "Damage", "0", "-");

                                        int ledIns = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stockistFrom.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), "0", upVal.ToString("0.00"), "0", "", tran_id, "", "", "TRS");

                                        DataSet dsSkt = prd.Select_Trans_CurrStock_Distwise(Div_Code, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString());
                                        if (dsSkt.Tables[0].Rows.Count > 0)
                                        {
                                            int numup = prd.Update_Trans_CurrStock_Batchwise_Transfer(Div_Code, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "Damage", "0", "+");
                                            int ledInsIn = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), "0", upVal.ToString("0.00"), "1", "", tran_id, "", "", "TRS");
                                        }
                                        else
                                        {
                                            int numup = prd.Insert_Trans_CurrStock_Batchwise(Div_Code, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), "0", upVal.ToString("0.00"));
                                            int ledInsIn = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stockistTo.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), "0", upVal.ToString("0.00"), "1", "", tran_id, "", "", "TRS");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                msg = "Stock Low Can't Update";
                                scope.Dispose();
                            }

                        }
                    }
                }
                scope.Complete();
                scope.Dispose();
                msg = "Update Successfully..!!";
            }
            catch (SqlException e)
            {
                if (e.Number == 2627)
                {
                    msg = "Transfer No. Already Exist.!!";
                }
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

    public class MainTransGRN
    {
        public List<Trans_Head> TransH = new List<Trans_Head>();
        public List<Trans_Details> TransD = new List<Trans_Details>();
    }


    public class Trans_Head
    {
        public string transNo { get; set; }
        public string transDate { get; set; }
        public string stockistFrom { get; set; }
        public string stockistFrom_Nm { get; set; }
        public string stockistTo { get; set; }
        public string stockistTo_Nm { get; set; }
    }

    public class Trans_Details
    {
        public string pCode { get; set; }
        public string pName { get; set; }
        public string pType { get; set; }
        public string pType_Name { get; set; }
        public string pqty { get; set; }
        public string oqty { get; set; }
        public string preason { get; set; }

    }


    [WebMethod(EnableSession = true)]
    public static string Get_AllValues(string TransSlNo)
    {
        string Div_Code = HttpContext.Current.Session["div_code"].ToString();
        Product prd = new Product();
        MainTransGRN MTG = new MainTransGRN();
        Trans_Head THEA;
        Trans_Details TDET;

        string tranId = string.Empty;
        DataSet dsGoods = null;
        dsGoods = prd.Get_Stock_Transfer_HeadVal(TransSlNo);
        if (dsGoods.Tables[0].Rows.Count > 0)
        {
            DataSet dsDetails = null;
            dsDetails = prd.Get_Stock_Transfer_DetailsVal(TransSlNo);



            THEA = new Trans_Head();
            THEA.transNo = dsGoods.Tables[0].Rows[0]["Transfer_No"].ToString();
            tranId = dsGoods.Tables[0].Rows[0]["Trans_SlNo"].ToString();
            THEA.transDate = Convert.ToDateTime(dsGoods.Tables[0].Rows[0]["Transfer_Date"]).ToString("dd/MM/yyyy");
            THEA.stockistFrom = dsGoods.Tables[0].Rows[0]["Transfer_From"].ToString();
            THEA.stockistFrom_Nm = dsGoods.Tables[0].Rows[0]["Transfer_From_Nm"].ToString();
            THEA.stockistTo = dsGoods.Tables[0].Rows[0]["Transfer_To"].ToString();
            THEA.stockistTo_Nm = dsGoods.Tables[0].Rows[0]["Transfer_To_Nm"].ToString();

            MTG.TransH.Add(THEA);

            for (int i = 0; i < dsDetails.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dsDetails.Tables[0].Rows[i];
                TDET = new Trans_Details();
                TDET.pCode = drow["PCode"].ToString();
                TDET.pName = drow["PName"].ToString();
                TDET.pType = drow["PType"].ToString();
                TDET.pqty = drow["QTY"].ToString();
                TDET.preason = drow["Reason"].ToString();
                MTG.TransD.Add(TDET);
            }
        }
        JSonHelper helper = new JSonHelper();
        String jsonResult = helper.ConvertObjectToJSon(MTG);
        return jsonResult;
    }

}