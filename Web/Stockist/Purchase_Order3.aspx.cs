using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using Bus_EReport;
using System.Globalization;
using Newtonsoft.Json;
using System.Transactions;
using DBase_EReport;

public partial class Stockist_Purchase_Order3 : System.Web.UI.Page
{
    string divcode = string.Empty;
    string Stockist_Code = string.Empty;
    public static DataSet ds = null;
    string words = string.Empty;
    public static StockistMaster sm = new StockistMaster();
    public static SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        Stockist_Code = Session["Sf_Code"].ToString();
        divcode = Convert.ToString(Session["div_code"]);
        divcode = divcode.TrimEnd(',');
    }

    [WebMethod(EnableSession = true)]
    public static string Dis_stk_sstk_Details()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string Sf_type = HttpContext.Current.Session["sf_type"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        ds = sm.getstockistdetails(div_code, Sf_Code, Sf_type);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public class CompanyDetails
    {
        public string HO_ID { get; set; }
        public string Name { get; set; }
        public string Division_Add1 { get; set; }
        public string Division_Add2 { get; set; }
        public string Division_City { get; set; }
        public string Division_Pincode { get; set; }
        public string State_Code { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static CompanyDetails[] DisplayCompanyDetails()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string stk_code = HttpContext.Current.Session["Sf_Code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        StockistMaster sm = new StockistMaster();
        List<CompanyDetails> Comdetails = new List<CompanyDetails>();
       //ds = sm.getcompanydetails(div_code, stk_code);
        ds = getcompanydetails(div_code, stk_code);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            CompanyDetails cd = new CompanyDetails();
            cd.HO_ID = (row["HO_ID"]).ToString();
            cd.Name = (row["Name"]).ToString();
            cd.Division_Add1 = (row["Division_Add1"]).ToString();
            cd.State_Code = (row["State_Code"]).ToString();
            Comdetails.Add(cd);
        }

        return Comdetails.ToArray();
    }
    public static DataSet getcompanydetails(string div_code, string stk_code)
    {
        DataSet dsSF = null;
        DB_EReporting db = new DB_EReporting();

        string strQry = "EXEC get_com_sup_stk_Details '" + div_code + "','" + stk_code + "'";
        try
        {
            dsSF = db.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;

    }
    [WebMethod(EnableSession = true)]
    public static string DisplayProduct()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string Stk_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        div_code = div_code.TrimEnd(',');
        ds = sm.getallproductdetails(div_code, Stk_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public class ProductDetails
    {
        public string Product_Detail_Code { get; set; }
        public string Product_Detail_Name { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static string SavePurchaseOrder(string HeadData, string DetailsData,string TaxData)
    {
        DataSet ds = new DataSet();
        string Exp_date = string.Empty;
        StockistMaster sm = new StockistMaster();
        string Trans_Sl_No = string.Empty;
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string SFcode = Stockist_Code;
        div_code = div_code.TrimEnd(',');
        string sf_code = HttpContext.Current.Session["Sf_Code"].ToString();
        using (var scope = new TransactionScope())
        {
            try
            {
                IList<Head> general_details = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Head>>(HeadData);
                IList<Purchase_Order> Product_details = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Purchase_Order>>(DetailsData);
                IList<Tax_details> Tax_items = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Tax_details>>(TaxData);

                string Head = "<ROOT>";
                for (int i = 0; i < general_details.Count; i++)
                {
                    Head += "<Head Sf_Code=\"" + general_details[i].sf_code + "\" Stk_Code=\"" + general_details[i].stk_code + "\" Order_Date=\"" + general_details[i].order_date + "\"  Order_Value=\"" + general_details[i].Gross_tot + "\"  Div_Code=\"" + general_details[i].div_code + "\"  Sup_No=\"" + general_details[i].sup_no + "\"  Sup_Name=\"" + general_details[i].sup_name + "\" Bill_Add=\"" + general_details[i].bill_add + "\" Ship_Add=\"" + general_details[i].ship_add + "\"  Exp_Date=\"" + general_details[i].exp_date + "\" Com_Add=\"" + general_details[i].com_add + "\" Sub_Total=\"" + general_details[i].sub_tot + "\" Tax_Total=\"" + general_details[i].Tax_Total + "\" Dis_Total=\"" + general_details[i].dis_tot + "\"  />";
                }
                Head += "</ROOT>";

                string Details = "<ROOT>";
                for (int i = 0; i < Product_details.Count; i++)
                {
                    Details += "<Prod PCode=\"" + Product_details[i].PCd + "\" PName=\"" + Product_details[i].PName + "\" Qty=\"" + Product_details[i].Qty + "\"  c_Qty=\"" + Product_details[i].Qty_c + "\"  Rate=\"" + Product_details[i].Rate + "\"  Rate_in_peice=\"" + Product_details[i].Rate_in_peice + "\" Free=\"" + Product_details[i].free + "\"  Dis=\"" + Product_details[i].Discount + "\"  Dis_value=\"" + Product_details[i].dis_value + "\" Off_Pro_Code=\"" + Product_details[i].Of_Pro_Code + "\" Off_Pro_Name=\"" + Product_details[i].Of_Pro_Name + "\" Off_Pro_Unit=\"" + Product_details[i].Of_Pro_Unit + "\"  Productunitcode=\"" + Product_details[i].umo_unit + "\" tax=\"" + Product_details[i].Tax_value + "\" con_fac=\"" + Product_details[i].con_fac + "\"  produnit=\"" + Product_details[i].produnit + "\" eQty=\"" + Product_details[i].eQty + "\" />";
                }
                Details += "</ROOT>";

                string Tax_Details = "<ROOT>";
                for (int k = 0; k < Tax_items.Count; k++)
                {
                    Tax_Details += "<Tax_details pro_code=\"" + Tax_items[k].pro_code + "\" Tax_Code=\"" + Tax_items[k].Tax_Code + "\" Tax_Amt=\"" + Tax_items[k].Tax_Amt + "\" Tax_Name=\"" + Tax_items[k].Tax_Name + "\" Tax_Per=\"" + Tax_items[k].Tax_Per + "\" umo_code=\"" + Tax_items[k].umo_code + "\"  />";
                }
                Tax_Details += "</ROOT>";

                ds = Save_Primary(Stockist_Code,div_code,Head,Details,Tax_Details,sf_code);
                Trans_Sl_No= ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
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

    public static DataSet Save_Primary(string Stockist_Code, string div_code, string Head,string Details,string Tax_Details,string sf_code)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = "Exec [svPrimaryOrder] '" + Stockist_Code + "','" + div_code + "','" + Head + "','" + Details + "','" + Tax_Details + "','" + sf_code + "'";

        try
        {
            ds = db_ER.Exec_DataSet(strQry);            
        }
        catch (Exception ex)
        {
            throw  ex;
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
        public string Qty { get; set; }
        public string Qty_c { get; set; }
        public string Rate { get; set; }
        public string Rate_in_peice { get; set; }
        public string free { get; set; }
        public string Discount { get; set; }
        public string dis_value { get; set; }
        public string Of_Pro_Name { get; set; }
        public string Of_Pro_Code { get; set; }
        public string Of_Pro_Unit { get; set; }
        public string Productunit { get; set; }
        public string umo_unit { get; set; }
        public string Tax_value { get; set; }
        public string con_fac { get; set; }
		public string produnit { get; set; }
        public string eQty { get; set; }
    }

    public class Tax_details
    {
        public string pro_code { get; set; }
        public string Tax_Code { get; set; }
        public float Tax_Amt { get; set; }
        public string Tax_Name { get; set; }
        public float Tax_Per { get; set; }
        public string umo_code { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static string getscheme(string date, string Div_Code, string Stockist_Code)
    {
        ds = sm.getschemebystk(Stockist_Code, Div_Code.TrimEnd(','), date);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string getratenew(string Div_Code, string Stockist_Code)
    {
        string State = HttpContext.Current.Session["State"].ToString();
        ds = get_rate_For_Primary(Div_Code.TrimEnd(','), Stockist_Code, State);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Get_Category()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        ds = sm.gets_cat_details(div_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Get_Product_unit()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        ds = gets_Product_unit_details(div_code, Stockist_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
     public static DataSet gets_Product_unit_details(string Div_Code, string Stockist_Code)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "Exec get_Hap_pro_unit '" + Div_Code + "','" + Stockist_Code + "'";

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
    public static string getorder(string Order_id)
    {
        string dat = string.Empty;
        string trans_sl_no = string.Empty;
        //Stockist_Sales ss = new Stockist_Sales(); 
        DataSet ds = get_sec_order(Order_id.TrimEnd(','));
        dat += "{";
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (trans_sl_no != dr["Trans_Sl_No"].ToString())
            {
                dat += "\"mainOrder\":[{\"StkERP\":\"" + dr["ERP_Code"] + "\",\"Plant\":\"" + dr["Plant_id"] + "\",\"Trans_sl_no\":\"" + dr["Trans_Sl_No"] + "\",\"OrdItems\":[";
                string add = string.Empty;
                foreach (DataRow drr in ds.Tables[0].Rows)
                {
                    if (dr["Trans_Sl_No"].ToString() == drr["Trans_Sl_No"].ToString())
                    {
                        add += "{\"EPCd\":\"" + drr["Sale_Erp_Code"] + "\",\"OQty\":\"" + drr["CQty"] + "\"},";
                    }
                }
                dat += add.TrimEnd(',') + "]}],";
            }
            trans_sl_no = dr["Trans_Sl_No"].ToString();
        }
        dat = dat.TrimEnd(',') + "}";
        return dat.TrimEnd(',');
    }
    public static DataSet get_sec_order(string Order_id)
    {
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "select ph.Trans_Sl_No,ERP_Code,Plant_id,Product_code,CQty,Sale_Erp_Code from Trans_PriOrder_Head ph inner join Trans_PriOrder_Details pd on ph.Trans_Sl_No = pd.Trans_Sl_No inner join Mas_Stockist ms on ms.Stockist_Code = ph.Stockist_Code inner join Mas_Product_Detail p on p.Product_Detail_Code=pd.Product_Code where ph.Trans_Sl_No in(" + Order_id + ") and sap_code =0";
        //select ph.Trans_Sl_No,ERP_Code,Plant_id,Product_code,CQty,Sale_Erp_Code from Trans_PriOrder_Head ph inner join Trans_PriOrder_Details pd on ph.Trans_Sl_No = pd.Trans_Sl_No inner join Mas_Stockist ms on ms.Stockist_Code = ph.Stockist_Code where ph.Trans_Sl_No in(" + Order_id + ")";
        //strQry = "Exec Get_Sap_order '" + Order_id + "'";
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
    public static string Update_sap(string Sap_code, string Pri_Order_id)
    {
        DCR dc = new DCR();
        string msg = string.Empty;
        DataSet ds = new DataSet();
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        SqlCommand cmd = new SqlCommand("EXEC Sp_update_Purchase_sap_code '" + Sap_code + "','" + Pri_Order_id + "','" + Stockist_Code + "'", conn);
        conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        conn.Close();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Get_Product_Tax()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string state_code = HttpContext.Current.Session["State"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        //StockistMaster sm = new StockistMaster();
        //ds = sm.gets_Product_Tax_details(div_code, Stockist_Code, state_code);
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
    [WebMethod]
    public static string GetProducts(string div)
    {
        //Product Rut = new Product();
        string stk = HttpContext.Current.Session["Sf_Code"].ToString();
        string state_code = HttpContext.Current.Session["State"].ToString();
       // DataSet ds = Rut.getProdall(div.Replace(",", ""), state_code, stk);
        DataSet ds = getProdall(div.Replace(",", ""), state_code, stk);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getProdall(string divcode, string state_code, string stk)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsProCat = null;

        string strQry = "EXEC get_product_details_view '" + divcode + "','" + state_code + "','" + stk + "'";

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
    [WebMethod(EnableSession = true)]
    public static string Get_Product_Cat_Details()
    {
        string Stockist_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        div_code = div_code.TrimEnd(',');
        DataSet ds = new DataSet();
        //StockistMaster sm = new StockistMaster();
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
    [WebMethod]
    public static string getPrimaryOrderValue(string urlparam, string trans_sl_no)
    {
        ds = getPrimaryOrderApi(urlparam, trans_sl_no);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getPrimaryOrderApi(string urlparam, string trans_sl_no)
    {

        string strQry = string.Empty;
        urlparam = "https://hapi.sanfmcg.com/api/sap/PrimaryOrder?value=" + urlparam;
        DB_EReporting db_ER = new DB_EReporting();
        DataSet ds = null;
        strQry = "EXEC get_PrimaryOrder_API '" + urlparam + "','" + trans_sl_no + "'";
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

    public static DataSet get_rate_For_Primary(string Div_Code, string Stockist_Code, string State)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = "Exec Get_rate_new_primary '" + Div_Code + "','" + Stockist_Code + "','" + State + "'";

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
}