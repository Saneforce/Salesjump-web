using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Activities.Statements;
using System.Text;
using System.Transactions;

public partial class MasterFiles_Stock_Adj_Entry : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsProd = null;
    DataSet dsTP = null;
    string sState = string.Empty;
    string div_code = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string prod_code = string.Empty;
    string prod_name = string.Empty;
    string stk_type;
    decimal ret_amt;
    decimal qty_amt;
    decimal val_amt;
    string res_txt;
    string effective_from = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string mode = string.Empty;
    string grn_no = string.Empty;
    string grn_dt = string.Empty;
    string supp_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillMRManagers();
            mode = Request.QueryString["Mode"].ToString();
            hdnmode.Value = mode;
            if (mode == "1")
            {
                grn_no = Request.QueryString["Adj_No"].ToString();
                hdntransno.Value = grn_no;
                Stk_Loc.SelectedValue = Request.QueryString["Stk_Code"].ToString();
                Stk_Loc.Enabled = false;
                
            }

          
           
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

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

        dsProduct = p.getproductname_stock(PDiv_code.TrimEnd(','));
        if (dsProduct != null)
        {
            foreach (DataRow row in dsProduct.Tables[0].Rows)
            {
                Products pro = new Products();
                pro.pName = row["Product_Detail_Name"].ToString();
                pro.pCode = row["Product_Detail_Code"].ToString();
                //pro.pUOM = row["BatchNo"].ToString();
                //pro.pUOM_Name = row["Move_MailFolder_Name"].ToString();

                //pro.pUMO = row["Stockist_Name"].ToString();
                product.Add(pro);
            }
        }
        return product.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static Products[] GetSno()
    {
        string PDiv_code = HttpContext.Current.Session["div_code"].ToString();
        List<Products> product = new List<Products>();
        DataSet dsProduct = null;
        Product p = new Product();

        dsProduct = p.getSlno_stock_Adj(PDiv_code.TrimEnd(','));
        if (dsProduct != null)
        {
            foreach (DataRow row in dsProduct.Tables[0].Rows)
            {
                Products pro = new Products();
                //pro.pName = row["Product_Detail_Name"].ToString();
                //pro.pCode = row["Product_Detail_Code"].ToString();
                //pro.pUOM = row["MRP_Price"].ToString();
                pro.pUOM_Name = "AdjTAR" + row["slno"].ToString();

                //pro.pUMO = row["Stockist_Name"].ToString();
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
        using (var scope = new System.Transactions.TransactionScope())
        {
            try
            {
                DateTime dtgrn = DateTime.ParseExact(GRN.TransH[0].transDate, "dd/MM/yyyy", null);
                string strgrn = dtgrn.ToString("MM-dd-yyyy");


                DataSet dsStock = null;
                dsStock = prd.Get_adj_Head(Div_Code, GRN.TransH[0].transNo.ToString(), strgrn, GRN.TransH[0].stkloc_Nm.ToString());

                if (dsStock.Tables[0].Rows.Count <= 0)
                {
                    string Trans_No = prd.Insert_adj_Head(Div_Code, SF_Code, GRN.TransH[0].transNo.ToString(), strgrn, GRN.TransH[0].stkloc.ToString(), GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransH[0].Auth_Nm.ToString());
                    for (int i = 0; i < GRN.TransD.Count; i++)
                    {
                        string val = GRN.TransD[i].pqty == "" ? "0" : GRN.TransD[i].pqty;
                        if (Convert.ToDecimal(val) > 0)
                        {
                            string irtn = prd.Insert_adj_Details(Trans_No, GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pName.ToString(), GRN.TransD[i].pType.ToString(), GRN.TransD[i].pType_Name.ToString(), GRN.TransD[i].pType1.ToString(), GRN.TransD[i].pType_Name1.ToString(), GRN.TransD[i].pqty.ToString(), GRN.TransD[i].preason.ToString(), Div_Code, GRN.TransD[i].pbtype.ToString(), GRN.TransD[i].pbtype_Name.ToString(), GRN.TransD[i].pbtype1.ToString(), GRN.TransD[i].pbtype_Name1.ToString());

                            DataSet dsCurSkt = prd.Select_Trans_Curradj_Distwise1(Div_Code, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString());


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
                                        //int num = prd.Update_Trans_Curradj_Batchwise_Transfer1(Div_Code, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString(), upVal.ToString("0.00"), "Good", GRN.TransD[i].oqty.ToString(), "-");
                                        int num = prd.Update_Trans_CurrStock_Batchwise_Adj1(Div_Code, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString(), upVal.ToString("0.00"), "Damage", GRN.TransD[i].oqty.ToString(), "-");

                                        int ledIns = prd.Insert_Trans_Stock_Ledger_adj(Div_Code, strgrn, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString(), "0", upVal.ToString("0.00"), "0", "", Trans_No, GRN.TransD[0].pbtype.ToString(), GRN.TransD[0].pbtype1.ToString(), "Adj");
                                        //int ledIns = prd.Insert_Trans_adj_Ledger(Div_Code, strgrn, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString(), upVal.ToString("0.00"), "0", "1", "", "Adj");
                                        //int ledIns = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), "0", upVal.ToString("0.00"), "0", "", Trans_No, GRN.TransH[0].stkloc_Nm.ToString(), "", "Adj");

                                        //DataSet dsSkt = prd.Select_Trans_Curradj_Distwise1(Div_Code, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString());
                                        //if (dsSkt.Tables[0].Rows.Count > 0)
                                        //{
                                        //    int numup = prd.Update_Trans_Curradj_Batchwise_Transfer(Div_Code, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString(), upVal.ToString("0.00"), "Good", "+");
                                        //    //int ledInsIn = prd.Insert_Trans_adj_Ledger(Div_Code, strgrn, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString(), upVal.ToString("0.00"), "0", "1", "", "ISS");
                                        //    int ledInsIn = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "0", "0", "", Trans_No, GRN.TransH[0].stkloc_Nm.ToString(), "", "Adj");
                                        //}
                                        //else
                                        //{
                                        //    int numup = prd.Insert_Trans_Curradj_Batchwise(Div_Code, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString(), upVal.ToString("0.00"), "0");
                                        //    //int ledInsIn = prd.Insert_Trans_adj_Ledger(Div_Code, strgrn, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString(), upVal.ToString("0.00"), "0", "1", "", "ISS");
                                        //    int ledInsIn = prd.Insert_Trans_Stock_Ledger(Div_Code, strgrn, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), row["BatchNo"].ToString(), upVal.ToString("0.00"), "0", "0", "", Trans_No, GRN.TransH[0].stkloc_Nm.ToString(), "", "Adj");
                                        //}
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
                                        int num = prd.Update_Trans_CurrStock_Batchwise_Adj1(Div_Code, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString(), upVal.ToString("0.00"), "Good", GRN.TransD[i].oqty.ToString(), "-");

                                        int ledIns = prd.Insert_Trans_Stock_Ledger_adj(Div_Code, strgrn, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString(), upVal.ToString("0.00"), "0", "0", "", Trans_No, GRN.TransD[0].pbtype.ToString(), GRN.TransD[0].pbtype1.ToString(), "Adj");
                                       
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
                    string Trans_No = prd.Update_adj_Head(tran_id, GRN.TransH[0].stkloc.ToString(), GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransH[0].Auth_Nm.ToString());
                    iReturn = prd.Delete_adj_Details(tran_id);

                    DataSet dsUpdateLedger = null;

                    dsUpdateLedger = prd.Select_Stock_Ledger(Div_Code, tran_id);
                    foreach (DataRow ul in dsUpdateLedger.Tables[0].Rows)
                    {
                        int num = 0;
                        if (ul["CalType"].ToString() == "0")
                        {
                            if (Convert.ToDecimal(ul["GStock"]) > 0)
                            {
                                num = prd.Update_Trans_CurrStock_Batchwise_Adj1(Div_Code, ul["Dist_Code"].ToString(), ul["Prod_Code"].ToString(), ul["BatchNo"].ToString(), ul["GStock"].ToString(), "Good", "0", "+");
                                //num = prd.Update_Trans_CurrStock_Batchwise_Adj1(Div_Code, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString(), upVal.ToString("0.00"), "Good", GRN.TransD[i].oqty.ToString(), "-");
                            }
                            else
                            {
                                num = prd.Update_Trans_CurrStock_Batchwise_Adj1(Div_Code, ul["Dist_Code"].ToString(), ul["Prod_Code"].ToString(), ul["BatchNo"].ToString(), ul["DStock"].ToString(), "Damage", "0", "+");
                            }
                        }

                    }
                    iReturn = prd.Delect_Stock_Ladger(Div_Code, tran_id);

                    for (int i = 0; i < GRN.TransD.Count; i++)
                    {
                        string val = GRN.TransD[i].pqty == "" ? "0" : GRN.TransD[i].pqty;
                        if (Convert.ToDecimal(val) > 0)
                        {

                            string irtn = prd.Insert_adj_Details(tran_id, GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pName.ToString(), GRN.TransD[i].pType.ToString(), GRN.TransD[i].pType_Name.ToString(), GRN.TransD[i].pType1.ToString(), GRN.TransD[i].pType_Name1.ToString(), GRN.TransD[i].pqty.ToString(), GRN.TransD[i].preason.ToString(), Div_Code, GRN.TransD[i].pbtype.ToString(), GRN.TransD[i].pbtype_Name.ToString(), GRN.TransD[i].pbtype1.ToString(), GRN.TransD[i].pbtype_Name1.ToString());
                            DataSet dsCurSkt = prd.Select_Trans_Curradj_Distwise1(Div_Code, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString());


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
                                        
                                        int num = prd.Update_Trans_CurrStock_Batchwise_Adj1(Div_Code, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString(), upVal.ToString("0.00"), "Damage","0", "-");

                                        int ledIns = prd.Insert_Trans_Stock_Ledger_adj(Div_Code, strgrn, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString(), "0", upVal.ToString("0.00"), "0", "", Trans_No, GRN.TransD[0].pbtype.ToString(), GRN.TransD[0].pbtype1.ToString(), "Adj");
                                       
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
                                        int num = prd.Update_Trans_CurrStock_Batchwise_Adj1(Div_Code, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString(), upVal.ToString("0.00"), "Good", "0", "-");

                                        int ledIns = prd.Insert_Trans_Stock_Ledger_adj(Div_Code, strgrn, GRN.TransH[0].stkloc_Nm.ToString(), GRN.TransD[i].pCode.ToString(), GRN.TransD[i].pbtype.ToString(), upVal.ToString("0.00"), "0", "0", "", Trans_No, GRN.TransD[0].pbtype.ToString(), GRN.TransD[0].pbtype1.ToString(), "Adj");

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
        public string stkloc { get; set; }
        public string stkloc_Nm { get; set; }
        public string Auth_Nm { get; set; }

    }

    public class Trans_Details
    {
        public string pCode { get; set; }
        public string pName { get; set; }
        public string pType { get; set; }
        public string pType_Name { get; set; }
        public string pbtype { get; set; }
        public string pbtype_Name { get; set; }
        public string pType1 { get; set; }
        public string pType_Name1 { get; set; }
        public string pbtype1 { get; set; }
        public string pbtype_Name1 { get; set; }
        public string oqty { get; set; }
        public string pqty { get; set; }
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
        dsGoods = prd.Get_Stock_HeadVal(TransSlNo);
        if (dsGoods.Tables[0].Rows.Count > 0)
        {
            DataSet dsDetails = null;
            dsDetails = prd.Get_Stock_DetailsVal(TransSlNo);



            THEA = new Trans_Head();
            THEA.transNo = dsGoods.Tables[0].Rows[0]["Adj_Trans_No"].ToString();
            //THEA.transNo = dsGoods.Tables[0].Rows[0]["Adj_SlNo"].ToString();
            THEA.transDate = Convert.ToDateTime(dsGoods.Tables[0].Rows[0]["Adj_Date"]).ToString("dd/MM/yyyy");
            THEA.stkloc = dsGoods.Tables[0].Rows[0]["Loc_Dist_Code"].ToString();
            THEA.stkloc_Nm = dsGoods.Tables[0].Rows[0]["Loc_Dist_Name"].ToString();
            THEA.Auth_Nm = dsGoods.Tables[0].Rows[0]["Authorised"].ToString();
            MTG.TransH.Add(THEA);

            for (int i = 0; i < dsDetails.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dsDetails.Tables[0].Rows[i];
                TDET = new Trans_Details();
                TDET.pCode = drow["Prod_Code"].ToString();
                TDET.pName = drow["Prod_Name"].ToString();
                TDET.pType = drow["From_Type"].ToString();
                TDET.pType1 = drow["To_Type"].ToString();
                TDET.pqty = drow["QTY"].ToString();
                TDET.preason = drow["Reason"].ToString();
                TDET.pbtype = drow["From_Batch"].ToString();
                TDET.pbtype1 = drow["To_Batch"].ToString();
                MTG.TransD.Add(TDET);
            }
        }
        JSonHelper helper = new JSonHelper();
        String jsonResult = helper.ConvertObjectToJSon(MTG);
        return jsonResult;
    }


    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        Product dv = new Product();
        dtGrid = dv.getProductRatelist_DataTable(div_code);
        return dtGrid;
    }
   
    private void FillMRManagers()
    {
        string sub = string.Empty;
        try
        {
            SalesForce sf = new SalesForce();
            dsDivision = sf.GetStockist_subdivisionwise(div_code, sub, "admin");
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                Stk_Loc.DataTextField = "stockist_Name";
                Stk_Loc.DataValueField = "Stockist_Code";
                Stk_Loc.DataSource = dsDivision;
                Stk_Loc.DataBind();
                Stk_Loc.Items.Insert(0, new ListItem("---Select---", "0"));


            }

        }
        catch (Exception)
        {

        }
    }

    public class Products_Stock
    {
        public string pName { get; set; }
        public string pCode { get; set; }
        public string pstk { get; set; }
        //public string pUOM_Name { get; set; }
    }


    [WebMethod(EnableSession = true)]
    public static Products_Stock[] GetProduct_stk()
    {
        string PDiv_code = HttpContext.Current.Session["div_code"].ToString();
        List<Products_Stock> product_stk = new List<Products_Stock>();
        DataSet dsProduct = null;
        Product p = new Product();

        dsProduct = p.getproductname_stock(PDiv_code.TrimEnd(','));
        if (dsProduct != null)
        {
            foreach (DataRow row in dsProduct.Tables[0].Rows)
            {
                Products_Stock pro = new Products_Stock();
                pro.pName = row["Product_Detail_Name"].ToString();
                pro.pCode = row["Product_Detail_Code"].ToString();
                //pro.pUOM = row["Unit_code"].ToString();
                //pro.pUOM_Name = row["Move_MailFolder_Name"].ToString();

                //pro.pUMO = row["Stockist_Name"].ToString();
                product_stk.Add(pro);
            }
        }
        return product_stk.ToArray();
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

    //currbatch
    public class currbatch
    {
        public string Dist_Code { get; set; }
        public string Prod_Code { get; set; }
        public string Batch { get; set; }
        
    }

    [WebMethod(EnableSession = true)]
    public static currbatch[] GetCurrbat()
    {
        string PDiv_code = HttpContext.Current.Session["div_code"].ToString();
        List<currbatch> product1 = new List<currbatch>();
        DataSet dsProduct = null;
        Product p = new Product();

        dsProduct = p.getCurrentBatch(PDiv_code.TrimEnd(','));
        if (dsProduct != null)
        {
            foreach (DataRow row in dsProduct.Tables[0].Rows)
            {
                currbatch pro = new currbatch();
                pro.Dist_Code = row["Dist_Code"].ToString();
                pro.Prod_Code = row["Prod_Code"].ToString();
                pro.Batch = row["BatchNo"].ToString();
             
                product1.Add(pro);
            }
        }
        return product1.ToArray();
    }

    
}